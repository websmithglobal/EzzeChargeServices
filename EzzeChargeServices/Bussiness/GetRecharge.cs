using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Text.RegularExpressions;
using System.Data;
using SRService;

namespace EzzeChargeServices.Bussiness
{
    public class GetRecharge : SqlHelper
    {
        MEMBERS.SQLReturnValue M;
        public Bussiness.AuthResponse ALLRecharge(DataClass.DataDoRecharge obj)
        {
           
            string[,] param = {{"OPCOD",obj.OperatorCode.ToString()},{"RCTYPE",obj.RecType.ToString()},{"NUMTORECH",obj.NumberToRecharge.ToString()},
                           {"AMTTORECH",obj.Amount.ToString()},{"MAINID",obj.Mainid.ToString()},{"REQVIA",obj.Via},{"ConIP",MRServiceClass.IpAddress()},{"OPTIONALPARAM",string.Empty}};
            M = ExecuteProcWithMessageValue("ADD_REQUEST_TO_RECHARGE", param, true);
            string[] REQ = M.MessageFromSQL.Split(';');
            string API = string.Empty, API1 = string.Empty;
            string ReqId = string.Empty, StatusCode = string.Empty; string TxnID = string.Empty;
            if (M.ValueFromSQL == 1)
            {
                if (REQ[0].Split(':')[2].StartsWith("C"))//----Cyberplate
                {
                    if (REQ.Length == 4)
                    {
                        API = REQ[1].Split('|')[0];//-----REQUEST URL
                        API1 = REQ[1].Split('|')[1];//-----PAY URL
                    }
                }
                else
                {
                    if (REQ.Length == 2)
                    {
                        API = REQ[1];//-----URL
                        API = API.Replace("#DATE#", DateTime.Now.ToString("MM.dd.yyyy hh:mm:ss"));
                    }
                }
            }
            if (API != string.Empty)
            {
                string LocalID = string.Empty;
                if (REQ[0].Split(':')[2].StartsWith("S"))
                {
                    #region Simple Recharge
                    string RESULT1 = string.Empty;
                    try
                    {
                        LocalID = REQ[0].Split(':')[2].StartsWith("S") ? REQ[0].Split(':')[2].Remove(0, 1) : REQ[0].Split(':')[2];  //---RechargeId
                        System.Net.WebClient Client1 = new System.Net.WebClient();
                        System.IO.Stream Output1 = Client1.OpenRead(API);
                        System.IO.StreamReader str1 = new System.IO.StreamReader(Output1);
                        RESULT1 = str1.ReadToEnd();
                        str1.Close();
                    }
                    catch
                    {
                        return new Bussiness.AuthResponse(0, "Could not Connect to Server.Pleas Try After Some times.");
                    }
                    string SRechargeAccountID = Regex.Split(API, "transid=")[1];

                    if (RESULT1 == "1210" || RESULT1 == "1211" || RESULT1 == "1206") //means request accepted. 1200 status code for 100% accepted.                
                    {
                        RechargeModule.Recharge rch = new RechargeModule.Recharge();
                        rch.TxId = LocalID;
                        rch.rechargeid = Int64.Parse(SRechargeAccountID);
                        rch.Status = 'f';
                        rch.RechargeVia = 1;
                        MEMBERS.SQLReturnValue V = RechargeModule.UpdateRequests(rch);
                    }
                    #endregion
                }
                else if (REQ[0].Split(':')[2].StartsWith("O"))
                {
                    #region OneAfter
                    string RESULT1 = string.Empty;
                    try
                    {

                        LocalID = REQ[0].Split(':')[2].StartsWith("O") ? REQ[0].Split(':')[2].Remove(0, 1) : REQ[0].Split(':')[2];  //---RechargeId
                        System.Net.WebClient Client1 = new System.Net.WebClient();
                        System.IO.Stream Output1 = Client1.OpenRead(API);
                        System.IO.StreamReader str1 = new System.IO.StreamReader(Output1);
                        RESULT1 = str1.ReadToEnd();
                        str1.Close();
                        string strLog1 = "INSERT INTO LogDetailResponse(logdata) Values('JIO: " + RESULT1 + " Mob:" + obj.NumberToRecharge + "')";
                        SqlHelper.DML(strLog1);
                    }
                    catch
                    {
                        return new Bussiness.AuthResponse(0, "Could not Connect to Server.Pleas Try After Some times.");
                    }
                    RechargeModule.Recharge rch = new RechargeModule.Recharge();

                    ReqId = Regex.Split(RESULT1, "<reqid>", RegexOptions.IgnoreCase)[1];
                    ReqId = Regex.Split(ReqId, "</reqid>", RegexOptions.IgnoreCase)[0];

                    StatusCode = Regex.Split(RESULT1, "<ec>", RegexOptions.IgnoreCase)[1];
                    StatusCode = Regex.Split(StatusCode, "</ec>", RegexOptions.IgnoreCase)[0];

                    if (StatusCode == "1000")
                    {
                        TxnID = Regex.Split(RESULT1, "<field1>", RegexOptions.IgnoreCase)[1];
                        TxnID = Regex.Split(TxnID, "</field1>", RegexOptions.IgnoreCase)[0];

                        rch.TxId = TxnID;
                        rch.rechargeid = Int64.Parse(LocalID);
                        rch.Status = 'S';
                        rch.RechargeVia = 1;
                        MEMBERS.SQLReturnValue V = RechargeModule.UpdateRequests(rch);

                    }
                    else if (StatusCode == "1002" || StatusCode == "1003" || StatusCode == "1004" || StatusCode == "1005" || StatusCode == "1006" || StatusCode == "1007" || StatusCode == "1008" || StatusCode == "1009" || StatusCode == "1011" || StatusCode == "1012" || StatusCode == "1013" || StatusCode == "1014" || StatusCode == "1015" || StatusCode == "1016" || StatusCode == "1017")
                    {

                        rch.TxId = "NA";
                        rch.rechargeid = Int64.Parse(LocalID);
                        rch.Status = 'f';
                        rch.RechargeVia = 1;
                        MEMBERS.SQLReturnValue V = RechargeModule.UpdateRequests(rch);
                    }

                    #endregion
                }
                else if (REQ[0].Split(':')[2].StartsWith("X"))
                {
                    #region PAY2CELL
                    string RESULT1 = string.Empty;
                    DataTable dt = new DataTable();
                    try
                    {
                        LocalID = REQ[0].Split(':')[2].StartsWith("X") ? REQ[0].Split(':')[2].Remove(0, 1) : REQ[0].Split(':')[2];  //---RechargeId
                        string[] VAL = REQ[1].Split('|');
                        SRService.Information info = new Information();
                        SRService.SRServiceSoapClient client = new SRService.SRServiceSoapClient();
                        MRServiceClass MrJson = new MRServiceClass();
                        string X = client.DoRecharge(new SRService.Information
                        {
                            Amount = Convert.ToDecimal(VAL[1]),
                            ApiCredentials = new SRService.Credentials { Password = VAL[5], UserName = VAL[6] },
                            MobileToRecharge = VAL[0],
                            OperatorCode = int.Parse(VAL[2]),
                            ServiceCode = int.Parse(VAL[4]),
                            TypeOfRecharge = Convert.ToByte(VAL[3].ToString()),

                        });
                        dt = MrJson.JsonStringToDataTable(X);
                        if (dt.Rows.Count > 0)
                        {
                            string SResponse = dt.Rows[0]["DoRechargeResponse"].ToString();
                            if (!string.IsNullOrEmpty(SResponse))
                            {
                                string[] SplitMyCode = SResponse.Split(':');
                                if (SplitMyCode[1] == "SR113")
                                {
                                    RechargeModule.Recharge objsuc = new RechargeModule.Recharge();
                                    objsuc.TxId = dt.Rows[0]["ReferenceID"].ToString();
                                    SqlHelper.DML("UPDATE RECHARGE SET accountref='" + objsuc.TxId + "' WHERE accountref=" + LocalID);
                                    
                                }
                                else
                                {
                                    RechargeModule.Recharge objsuc = new RechargeModule.Recharge();
                                    objsuc.TxId = dt.Rows[0]["ReferenceID"].ToString();
                                    SqlHelper.DML("UPDATE RECHARGE SET accountref='" + objsuc.TxId + "' WHERE accountref=" + LocalID);
                                    
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                       
                    }
                    #endregion
                }
                else
                {
                    #region Our_API

                    LocalID = REQ[0].Split(':')[2];  //---RechargeId

                    System.Net.WebClient Client = new System.Net.WebClient();
                    System.IO.Stream Output = Client.OpenRead(API);
                    System.IO.StreamReader str = new System.IO.StreamReader(Output);
                    string RESULT = str.ReadToEnd();
                    str.Close();

                    ReqId = Regex.Split(RESULT, "<requestID>", RegexOptions.IgnoreCase)[1];
                    ReqId = Regex.Split(ReqId, "</requestID>", RegexOptions.IgnoreCase)[0];

                    StatusCode = Regex.Split(RESULT, "<statusCode>", RegexOptions.IgnoreCase)[1];
                    StatusCode = Regex.Split(StatusCode, "</statusCode>", RegexOptions.IgnoreCase)[0];

                    if (StatusCode.Trim() == "10008") //means request accepted. 10008 status code for 100% accepted.
                    {
                        string[,] prm = { { "id", LocalID }, { "accountid", ReqId } };
                        RechargeModule.UPDATERECHARGEVIAAPI(prm);
                    }
                    else// call refund procedure.
                    {
                        RechargeModule.Recharge rch = new RechargeModule.Recharge();
                        rch.TxId = StatusCode;
                        rch.rechargeid = Int64.Parse(LocalID);
                        rch.Status = 'f';
                        int K = RechargeModule.UpdateRequests(rch).ValueFromSQL;
                    }
                    #endregion
                }
                return new Bussiness.AuthResponse(1, REQ[0].ToString());
            }
            else
            { return new Bussiness.AuthResponse(1, REQ[0].ToString()); }
        }
    }
}