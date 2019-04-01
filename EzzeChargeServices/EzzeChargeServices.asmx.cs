using EzzeChargeServices._MyConnection;
using EzzeChargeServices.Bussiness;
using EzzeChargeServices.DataClass;
using EzzeChargeServices.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace EzzeChargeServices
{
    [WebService(Namespace = "http://ezeecharge.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1, Name = "EzzeCharge")]
    [System.ComponentModel.ToolboxItem(false)]

    public class EzzeChargeServices : System.Web.Services.WebService
    {
        #region Private Members
        public DataClass.UserAuthntication SecureAuthentication;
        protected DataClass.UserLogin chkLogin;
        #endregion

        #region Serialize
        protected string Serialize(object Object)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(Object);
        }
        #endregion

        #region UserLoginAuth
        [WebMethod(Description = "Check authenticate parameters.")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CheckAuthentication()
        {
            if (SecureAuthentication != null)
            {
                if (string.IsNullOrEmpty(SecureAuthentication.UserName) || string.IsNullOrEmpty(SecureAuthentication.Password))
                {
                    return Serialize(new AuthResponse(0, "Authentication information not blank"));
                }
                if (SecureAuthentication.UserName.Length > 50 || SecureAuthentication.Password.Length > 50)
                {
                    return Serialize(new AuthResponse(0, "invalid field length,Max 50 char"));
                }
                try
                {
                    MEMBERS.SQLReturnValue M1;
                    M1 = CheckLoginReturnUserId(SecureAuthentication);
                    if (M1.ValueFromSQL > 0)
                    {
                        return Serialize(new AuthResponse(M1.ValueFromSQL, M1.MessageFromSQL)) + "}";
                    }
                    else
                        return Serialize(new AuthResponse(0, M1.ValueFromSQL == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("Authentication", ex.Message);
                    ErrorReport.LogError(ex, "Authentication");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        public void SystemLog(string funcation, string ErrorMsg)
        {
            int i = SqlHelper.DML("insert into Android_ApplicationLog(funcation,Msg)values('" + funcation + "','" + ErrorMsg + "')");
        }
        public MEMBERS.SQLReturnValue CheckLoginReturnUserId(DataClass.UserAuthntication Auth)
        {
            chkLogin = new DataClass.UserLogin();
            MEMBERS.SQLReturnValue M1;
            M1 = chkLogin.CheckUserLogin(Auth);
            return M1;
        }

        #endregion

        #region Get ALL Operators List
        [WebMethod(Description = "Get Operator with ServiceType =1 Mobile , 2= Postpaid , 3 = Dth")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetALLOperators()
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2017/07/04 06:57 PM 
                // Validation : Required Field,Check Authentication In Header, Field Length validation UserName,Password Greater Than 50
                // logical    : Get Operator list Operator and code with servicetype
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Get Mobile,DTH, POSTPAID List
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // Store P    : Class Query
                // OutPut     : OperatorName,Operatorcode,ServiceType list
                #endregion
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetOperators proc = new GetOperators();
                        return Serialize(proc.GetOperatorList()) + "}";
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));

                }
                catch (Exception ex)
                {
                    SystemLog("GetOperatorList", ex.Message);
                    ErrorReport.LogError(ex, "GetOperatorList");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Account Balance
        [WebMethod(Description = "Account Balance")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccountBalance(int Mainid)
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2017/07/04 07:00 PM
                // Validation : Requiredfield,Authentication, field Length validation UserName,Password Greater Than 50
                // logical    : Get Account Balance
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Get User Balance
                // Store P    : Class Query
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // OutPut     : return list
                #endregion

                if (string.IsNullOrEmpty(Mainid.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters is NULL"));
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetBalance proc = new GetBalance();
                        return Serialize(proc.GetACBalanceList(Mainid, SecureAuthentication.UserName, SecureAuthentication.Password));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("AccountBalance", ex.Message);
                    ErrorReport.LogError(ex, "AccountBalance");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region LedgerReport
        [WebMethod(Description = "Ledger Report")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetLedgerReport(int Mainid, int UserLevel)
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2017/07/04 07:05 PM 
                // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50
                // logical    : Get Ledger Report
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Get ALL Recharge Transaction Ledger report
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // Store P    : GET_LEDGER_INAPPS
                // OutPut     : Date,Remark,Credit,Debit,BeforeBal,AfterBal
                #endregion
                if (string.IsNullOrEmpty(Mainid.ToString()) || string.IsNullOrEmpty(UserLevel.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters Is NULL"));
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetLedgerReport proc = new GetLedgerReport();
                        return Serialize(proc.GetLedger(Mainid, UserLevel));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("LedgerReport", ex.Message);
                    ErrorReport.LogError(ex, "LedgerReport");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Recharge Report
        [WebMethod(Description = "Search Recharge Report")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetRechargeReport(RechargeReport obj)
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2017/07/04 07:06 PM 
                // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50
                // logical    : Get Recharge Report
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Get Recharge Transaction report
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // Store P    : GET_RECHRGREREPORT,GET_RECHRGREREPORT_ARCHIVE
                // OutPut     : REQDATE,OperatorName,Amount,Mobile,ReqVia,Via
                #endregion
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetRechargeReport proc = new GetRechargeReport();
                        return Serialize(proc.GetSearchRechargeReport(obj));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("RechargeReport", ex.Message);
                    ErrorReport.LogError(ex, "RechargeReport");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region RechargeTickets
        [WebMethod(Description = "Recharge Tickets ")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveRechargeTickets(DataRechargeTickets obj)
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2017/07/04 07:15 PM 
                // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50
                // logical    : Save Recharge Tickets
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Recharge Tickets Module
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // Store P    : ADD_TICKET
                // OutPut     : Code=1 Message = Recharge Tickets Save Successfully.
                #endregion
                if (string.IsNullOrEmpty(obj.Mainid.ToString()) || string.IsNullOrEmpty(obj.Userlevel.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters Is NULL"));
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        Bussiness.AuthResponse o = new AuthResponse();
                        return Serialize(new Bussiness.GetRechargeTickets().SaveRechargeTickets(obj)) + "}";
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("SaveRechargeTickets", ex.Message);
                    ErrorReport.LogError(ex, "SaveRechargeTickets");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Check Recharge Tickets Status
        [WebMethod(Description = "Check Recharge Tickets Status")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SearchRechargeTicketStatus(int Rechargeid, int Mainid)
        {
            #region Commenting
            // Date       : 2017/07/04 07:15 PM
            // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50
            // logical    : Check Recharge Tickets Status
            // Cosmatic   : Not applicable for service
            // Known Issue: None
            // P of Page  : Recharge Tickets Module
            // Log System : Log structure (funcation,Mesg,EntryDate)
            // Store P    : Class Query
            // OutPut     : Code=1 Message = Remark
            #endregion
            if (SecureAuthentication != null)
            {
                if (string.IsNullOrEmpty(Mainid.ToString()) || string.IsNullOrEmpty(Rechargeid.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters Is NULL"));
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        Bussiness.AuthResponse o = new AuthResponse();
                        return Serialize(new Bussiness.GetSearchRechargeTickets().SearchRechargeTickets(Rechargeid, Mainid)) + "}";
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("SearchRechargeTicketStatus", ex.Message);
                    ErrorReport.LogError(ex, "SearchRechargeTicketStatus");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Fund Transfer
        [WebMethod(Description = "Fund Transfer")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FundTransfer(int Mainid, string CustomerMobile, float Amount, string TranPwd)
        {
            #region Commenting
            // Date       : 2017/07/05 10:09 AM 
            // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50,Int Parse
            // logical    : Fund Transfer Sub level.
            // Cosmatic   : Not applicable for service
            // Known Issue: None
            // P of Page  : balance Fund Transfer
            // Log System : Log structure (funcation,Mesg,EntryDate)
            // Store P    : PROCESS_WITH_KEYWORDS1
            // OutPut     : Code=1 Message = Fund Transfer Successfully,your debit is 0.00 and user credit is 0.00
            #endregion
            long outMob;
            if (Int64.TryParse(CustomerMobile, out outMob) == false)
            {
                return Serialize(new AuthResponse(0, "Invalid Mobile Number")) + "}";
            }
            if (CustomerMobile.Length != 10)
            {
                return Serialize(new AuthResponse(0, "Invalid Mobile Number Length")) + "}";
            }
            if (string.IsNullOrEmpty(CustomerMobile) || string.IsNullOrEmpty(Amount.ToString()) || string.IsNullOrEmpty(TranPwd.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters Is NULL")) + "}";
            }
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        Bussiness.AuthResponse o = new AuthResponse();
                        return Serialize(new Bussiness.GetFundTransfer().GPRSTOWEB(Mainid, CustomerMobile, Amount, TranPwd)) + "}";
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("FundTransfer", ex.Message);
                    ErrorReport.LogError(ex, "FundTransfer");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Notification
        [WebMethod(Description = "Get Notification")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetNotification(int UserLevel)
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2017/07/05 10:30 AM 
                // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50,Int Parse
                // logical    : Get User Notification
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Get User level wise Notification
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // Store P    : Class Query
                // OutPut     : Notification Data all Level
                #endregion
                if (string.IsNullOrEmpty(UserLevel.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters Is NULL")) + "}";
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetNotification proc = new GetNotification();
                        return Serialize(proc.GetNotificationCall(UserLevel));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));

                }
                catch (Exception ex)
                {
                    SystemLog("Notification", ex.Message);
                    ErrorReport.LogError(ex, "Notification");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region ChangePassword
        [WebMethod(Description = "Change Login/Transaction Password")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ChangePassword(DataChangePassword obj)
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2017/07/05 10:40 AM 
                // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50,Int Parse
                // logical    : Change Password
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Change login password & transaction password
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // Store P    : UPDATE_ADMIN_TRANSPASS
                // OutPut     : PASSWORD UPDATE SUCCESSFULYY
                #endregion
                if (string.IsNullOrEmpty(obj.OldPassword.ToString()) || string.IsNullOrEmpty(obj.NewPassword) || string.IsNullOrEmpty(obj.PwdType.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters Is NULL")) + "}";
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        Bussiness.AuthResponse o = new AuthResponse();
                        return Serialize(new Bussiness.GetChangePassword().ChangePassword(obj)) + "}";
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("ChangePassword", ex.Message);
                    ErrorReport.LogError(ex, "ChangePassword");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region GetProfile
        [WebMethod(Description = "Get User Profile")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetProfile(int Mainid)
        {
            if (string.IsNullOrEmpty(Mainid.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters is NULL"));
            }
            try
            {
                int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                if (Output > 0)
                {
                    GetProfile proc = new GetProfile();
                    return Serialize(proc.GetMyProfile(Mainid, SecureAuthentication.UserName, SecureAuthentication.Password));
                }
                else
                    return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
            }
            catch (Exception ex)
            {
                SystemLog("GetProfile", ex.Message);
                ErrorReport.LogError(ex, "GetProfile");
                return Serialize(new AuthResponse(0, "internal server error"));
            }
        }
        #endregion

        #region EditProfile
        [WebMethod(Description = "Edit User Profile")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string EditProfile(DataEditProfile obj)
        {
            if (string.IsNullOrEmpty(obj.Mainid.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters is NULL"));
            }
            try
            {
                int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                if (Output > 0)
                {
                    Bussiness.AuthResponse o = new AuthResponse();
                    return Serialize(new Bussiness.GetEditProfile().EditMyProfile(obj)) + "}";
                }
                else
                    return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
            }
            catch (Exception ex)
            {
                SystemLog("EditProfile", ex.Message);
                ErrorReport.LogError(ex, "EditProfile");
                return Serialize(new AuthResponse(0, "internal server error"));
            }
        }
        #endregion

        #region Create Distributor
        [WebMethod(Description = "Create Distributor")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateDistributor(DataDistributor obj)
        {
            #region Commenting
            // Date       : 2017/07/05 10:60 AM 
            // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50,Int Parse
            // logical    : Create Distributor
            // Cosmatic   : Not applicable for service
            // Known Issue: None
            // P of Page  : Create SD under MD
            // Log System : Log structure (funcation,Mesg,EntryDate)
            // Store P    : SAVE_DIST
            // OutPut     : DEAR " NAME " YOUR ACCOUNT HAS BEEN CREATED AS DISTRIBUTOR.USERNAME IS 0000 AND PASSWORD IS 0000
            #endregion
            if (string.IsNullOrEmpty(obj.mainid.ToString()) || string.IsNullOrEmpty(obj.Mobile.ToString()) || string.IsNullOrEmpty(obj.Name.ToString()) || string.IsNullOrEmpty(obj.Userlevel.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters is NULL"));
            }
            try
            {
                if (SecureAuthentication != null)
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        Bussiness.AuthResponse o = new AuthResponse();
                        return Serialize(new Bussiness.GetDistributor().SAVE_DISTRIBUTOR(obj, SecureAuthentication.UserName, SecureAuthentication.Password));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                return Serialize(new AuthResponse(0, "Authentication information not provided."));
            }
            catch (Exception ex)
            {
                SystemLog("CreateDistributor", ex.Message);
                return Serialize(new AuthResponse(0, "internal server error"));
            }
        }
        #endregion

        #region GetDistributor
        [WebMethod(Description = "Get Distributor Users")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDistributorUsers(int Mainid, int Userlevel)
        {
            #region Commenting
            // Date       : 2017/07/05 11:20 AM 
            // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50,Int Parse
            // logical    : Distributor List
            // Cosmatic   : Not applicable for service
            // Known Issue: None
            // P of Page  : Get SD list to MD
            // Log System : Log structure (funcation,Mesg,EntryDate)
            // Store P    : Class Query
            // OutPut     : USERID,NAME
            #endregion
            if (string.IsNullOrEmpty(Mainid.ToString()) || string.IsNullOrEmpty(Userlevel.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters is NULL"));
            }
            if (SecureAuthentication != null)
            {
                int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                if (Output > 0)
                {
                    GetDistributorUsers proc = new GetDistributorUsers();
                    return Serialize(proc.GetDistUsers(Mainid, Userlevel, SecureAuthentication.UserName, SecureAuthentication.Password));
                }
                else
                    return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Create Retailer
        [WebMethod(Description = "Create Retailer")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateRetailer(DataRetailer obj)
        {
            #region Commenting
            // Date       : 2017/07/05 10:60 AM 
            // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50,Int Parse
            // logical    : Create Retailer
            // Cosmatic   : Not applicable for service
            // Known Issue: None
            // P of Page  : Create R under SD
            // Log System : Log structure (funcation,Mesg,EntryDate)
            // Store P    : SAVE_RETAILER
            // OutPut     : DEAR " NAME " YOUR ACCOUNT HAS BEEN CREATED AS DISTRIBUTOR.USERNAME IS 0000 AND PASSWORD IS 0000
            #endregion
            if (SecureAuthentication != null)
            {
                int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                if (Output > 0)
                {
                    Bussiness.AuthResponse o = new AuthResponse();
                    return Serialize(new Bussiness.GetRetailer().SAVE_RETAILER(obj, SecureAuthentication.UserName, SecureAuthentication.Password));
                }
                else
                    return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region DoRecharge
        [WebMethod(Description = "Recharge Request")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DoRecharge(DataDoRecharge obj)
        {
            #region Commenting
            // Date       : 2017/07/05 12:40 PM
            // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50,Int Parse
            // logical    : User Recharge for Mobile,DTH,POSTPAID
            // Cosmatic   : Not applicable for service
            // Known Issue: None
            // P of Page  : Recharge Module
            // Log System : Log structure (funcation,Mesg,EntryDate)
            // Store P    : ADD_REQUEST_TO_RECHARGE
            // OutPut     : REQUEST ACCEPTED
            #endregion
            long outMob;
            if (Int64.TryParse(obj.NumberToRecharge, out outMob) == false)
            {
                return Serialize(new AuthResponse(0, "Invalid Recharge Number")) + "}";
            }
            if (string.IsNullOrEmpty(obj.NumberToRecharge) || string.IsNullOrEmpty(obj.Amount.ToString()) || string.IsNullOrEmpty(obj.Via.ToString()) || string.IsNullOrEmpty(obj.OperatorCode.ToString()) || string.IsNullOrEmpty(obj.Mainid.ToString()) || string.IsNullOrEmpty(obj.RecType.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters Is NULL")) + "}";
            }

            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        Bussiness.AuthResponse o = new AuthResponse();
                        return Serialize(new Bussiness.GetRecharge().ALLRecharge(obj)) + "}";
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {

                    SystemLog("DoRecharge", ex.Message);
                    ErrorReport.LogError(ex, "DoRecharge");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Recharge Tickets Report
        [WebMethod(Description = "Search Recharge Tickets")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetRechargeTicketsReport(DataGetRechargeTickets obj)
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2017/07/05 12:50 PM 
                // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50
                // logical    : Get Recharge Tickets Report
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Get Recharge Transaction report
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // Store P    : GET_USERS_TICKETS
                // OutPut     : EntryDate,OperatorName,Number,Amount,Remarks,TranType,CloseDate,Reason
                #endregion

                if (string.IsNullOrEmpty(obj.Mainid.ToString()) || string.IsNullOrEmpty(obj.FromDate.ToString()) || string.IsNullOrEmpty(obj.ToDate.ToString()) || string.IsNullOrEmpty(obj.LiveData.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters Is NULL")) + "}";
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetRechargeTicketsReport proc = new GetRechargeTicketsReport();
                        return Serialize(proc.GetRechargeTickets(obj));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("RechargeTicketsReport", ex.Message);
                    ErrorReport.LogError(ex, "RechargeTicketsReport");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Update IMEI
        [WebMethod(Description = "Update IMEI")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateIMEI(DataIMEIClass obj)
        {
            if (string.IsNullOrEmpty(obj.Mainid.ToString()) || string.IsNullOrEmpty(obj.IMEI.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters is NULL"));
            }
            try
            {
                int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                if (Output > 0)
                {
                    GetIMEIClass proc = new GetIMEIClass();
                    return Serialize(proc.Save_IMEI(obj));
                }
                else
                    return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
            }
            catch (Exception ex)
            {
                SystemLog("UpdateIMEI", ex.Message);
                ErrorReport.LogError(ex, "UpdateIMEI");
                return Serialize(new AuthResponse(0, "internal server error"));
            }
        }
        #endregion

        #region Last30Txn
        [WebMethod(Description = "Last30Txn")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Last30Txn(int Mainid)
        {
            if (string.IsNullOrEmpty(Mainid.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters is NULL"));
            }
            try
            {
                int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                if (Output > 0)
                {
                    GetLast30txn proc = new GetLast30txn();
                    return Serialize(proc.GetLast30Txn(Mainid));
                }
                else
                    return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
            }
            catch (Exception ex)
            {
                SystemLog("Last30Txn", ex.Message);
                ErrorReport.LogError(ex, "Last30Txn");
                return Serialize(new AuthResponse(0, "internal server error"));
            }

        }
        #endregion

        #region Earn Commission
        [WebMethod(Description = "Earn Commission")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTodayEarnCommission(int Mainid)
        {
            if (SecureAuthentication != null)
            {
                if (string.IsNullOrEmpty(Mainid.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters is NULL"));
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetEarnCommi proc = new GetEarnCommi();
                        return Serialize(proc.GetEarnCommission(Mainid));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    ErrorReport.LogError(ex, "GetTodayEarnCommission");
                    return Serialize(new AuthResponse(0, ex.Message));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Fund Transfer Report
        [WebMethod(Description = "Fund Transfer Report")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetFundTransferReport(DataFundTransferReport obj)
        {
            if (SecureAuthentication != null)
            {

                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetFundTransferReport proc = new GetFundTransferReport();
                        return Serialize(proc.GetFundReport(obj));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("FundTransferReport", ex.Message);
                    ErrorReport.LogError(ex, "FundTransferReport");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region ROFFER
        [WebMethod(Description = "R-Offer Plan / View Plan / 1 = r-offer 2 = view plan")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ROFFER(DataOFFER obj)
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        string URL = string.Empty;
                        if (obj.ApiType == "1")
                        {
                            URL = "https://mplan.in/api/plans.php?apikey=" + obj.apikey + "&offer=" + obj.offer + "&tel=" + obj.mobile + "&operator=" + obj.operators + "";
                        }
                        else if (obj.ApiType == "2")
                        {
                            URL = "https://mplan.in/api/plans.php?apikey=" + obj.apikey + "&cricle=" + obj.Cricle + "&tel=" + obj.mobile + "&operator=" + obj.operators + "";
                        }
                        else if (obj.ApiType == "3")
                        {
                            URL = "https://mplan.in/api/dthplans.php?apikey=" + obj.apikey + "&operator=" + obj.operators + "";
                        }
                        System.Net.WebClient Client1 = new System.Net.WebClient();
                        System.IO.Stream Output1 = Client1.OpenRead(URL);
                        System.IO.StreamReader str1 = new System.IO.StreamReader(Output1);
                        string RESULT = str1.ReadToEnd();
                        str1.Close();
                        return RESULT;

                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("ROFFER", ex.Message);
                    return "ROFFER /  VIEWPLAN /  API ERROR..";
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region DTH INFO
        [WebMethod(Description = "DTH info")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DTHINFO(DataOFFER obj)
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        string URL = string.Empty;
                        URL = "https://www.mplan.in/api/Dthinfo.php?apikey=" + obj.apikey + "&offer=" + obj.offer + "&tel=" + obj.mobile + "&operator=" + obj.operators + "";
                        System.Net.WebClient Client1 = new System.Net.WebClient();
                        System.IO.Stream Output1 = Client1.OpenRead(URL);
                        System.IO.StreamReader str1 = new System.IO.StreamReader(Output1);
                        string RESULT = str1.ReadToEnd();
                        str1.Close();
                        return RESULT;
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("DTHINFO", ex.Message);
                    ErrorReport.LogError(ex, "DTHINFO");
                    return "DTHINFO /  VIEWPLAN /  API ERROR..";
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Account Balance With All User
        [WebMethod(Description = "Account Balance With All User")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetChildUser(int Mainid)
        {
            if (SecureAuthentication != null)
            {
                #region Commenting
                // Date       : 2018/10/22 07:00 PM
                // Validation : Requiredfield,Authentication, field Length validation UserName,Password Greater Than 50
                // logical    : Get Chiled level user with it's balancew
                // Cosmatic   : Not applicable for service
                // Known Issue: None
                // P of Page  : Get child user list with it's balance
                // Store P    : Class Query
                // Log System : Log structure (funcation,Mesg,EntryDate)
                // OutPut     : return list
                #endregion

                if (string.IsNullOrEmpty(Mainid.ToString()))
                {
                    return Serialize(new AuthResponse(0, "Parameters is NULL"));
                }
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        //GetBalance proc = new GetBalance();
                        Bussiness.GetChildUsers proc = new Bussiness.GetChildUsers();
                        return Serialize(proc.GetChildUser(Mainid));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("AccountBalance", ex.Message);
                    ErrorReport.LogError(ex, "AccountBalance");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region FundRequest
        [WebMethod(Description = "Fund Request")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FundRequest(DataFundRequest obj)
        {
            #region Commenting
            // Date       : 2018/10/23
            // Validation : Required field,Authentication, field Length validation UserName,Password Greater Than 50,Int Parse
            // logical    : Fund Request for balance
            // Cosmatic   : Not applicable for service
            // Known Issue: None
            // P of Page  : Fund Request
            // Log System : Log structure (funcation,Mesg,EntryDate)
            // Store P    : SAVE_FUNDREQUEST
            // OutPut     : Code=1 Message = Fund Request Save Successfully.
            #endregion
            if (string.IsNullOrEmpty(obj.MainId.ToString()) || string.IsNullOrEmpty(obj.FAmount.ToString()) || string.IsNullOrEmpty(obj.TxnPassword.ToString()))
            {
                return Serialize(new AuthResponse(0, "Parameters Is NULL")) + "}";
            }
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        Bussiness.AuthResponse o = new AuthResponse();
                        return Serialize(new Bussiness.GetFundRequest().SaveFundRequest(obj)) + "}";
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("FundRequest", ex.Message);
                    ErrorReport.LogError(ex, "FundRequest");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region GetLanguage
        [WebMethod(Description = "Get Language")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetLanguage()
        {
            if (SecureAuthentication != null)
            {

                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetLanguage proc = new GetLanguage();
                        return Serialize(proc.GetLanguageList());
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("Language", ex.Message);
                    ErrorReport.LogError(ex, "Language");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region GetDTHCharges
        [WebMethod(Description = "Get DTH Charges")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDTHCharges(int Mainid, int OperatorCode, int ConnectionType)
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetDTHCharges proc = new GetDTHCharges();
                        return Serialize(proc.GetChargesList(Mainid, OperatorCode, ConnectionType));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("GetCharges", ex.Message);
                    ErrorReport.LogError(ex, "GetCharges");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region DTHBooking
        [WebMethod(Description = "DTH Booking")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DTHBooking(DataDTHBooking obj)
        {
            if (SecureAuthentication != null)
            {

                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetDTHBooking proc = new GetDTHBooking();
                        return Serialize(proc.SaveDTHBooking(obj));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("DthBooking", ex.Message);
                    ErrorReport.LogError(ex, "DthBooking");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region DTHBookingReport
        [WebMethod(Description = "DTH Booking Report")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DTHBookingReport(DataDthBookingReport obj)
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetDthBookingReport proc = new GetDthBookingReport();
                        return Serialize(proc.GetDTHReport(obj));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("dthbookingreport", ex.Message);
                    ErrorReport.LogError(ex, "dthbookingreport");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }
        #endregion

        #region Get D2H Activation
        // Below all the API is created by kalpesh gajera

        [WebMethod(Description = "Get DTH Box Type Master")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDTHBoxType()
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetDTHBoxTypeMaster proc = new GetDTHBoxTypeMaster();
                        return Serialize(proc.GetDTHBoxType());
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("GetDTHBoxType", ex.Message);
                    ErrorReport.LogError(ex, "GetDTHBoxType");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }

        [WebMethod(Description = "Get All DTH Chanel Category List")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDTHCategory()
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetDTHCategoryMaster proc = new GetDTHCategoryMaster();
                        return Serialize(proc.GetDTHCategory());
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("GetDTHCategory", ex.Message);
                    ErrorReport.LogError(ex, "GetDTHCategory");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }

            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }

        [WebMethod(Description = "Get DTH Package By Operator")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDTHPackageByOperator(int OperatorCode)
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetPackageMaster proc = new GetPackageMaster();
                        return Serialize(proc.GetPackageListByOperator(OperatorCode));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("GetDTHPackageByOperator", ex.Message);
                    ErrorReport.LogError(ex, "GetDTHPackageByOperator");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }

        [WebMethod(Description = "Get DTH Channel List By Pack")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDTHChannelListByPack(int packid)
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetChannelPack proc = new GetChannelPack();
                        return Serialize(proc.GetChannelListByPack(packid));
                    }
                    else
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                }
                catch (Exception ex)
                {
                    SystemLog("GetDTHChannelListByPack", ex.Message);
                    ErrorReport.LogError(ex, "GetDTHChannelListByPack");
                    return Serialize(new AuthResponse(0, "internal server error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }

        [WebMethod(Description = "Get All DTH Chanel Link List")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDTHChannelLink()
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        GetDHTChannelLink proc = new GetDHTChannelLink();
                        return Serialize(proc.GetDTHChannelLink());
                    }
                    else
                    {
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                    }
                }
                catch (Exception ex)
                {
                    SystemLog("GetDTHChannelLink", ex.Message);
                    ErrorReport.LogError(ex, "GetDTHChannelLink");
                    return Serialize(new AuthResponse(0, "Internal Server Error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }

        [WebMethod(Description = "Get DTH Channel List By Searched Channel Name")]
        [SoapDocumentMethod(Binding = "EzzeCharge")]
        [SoapHeader("SecureAuthentication", Direction = SoapHeaderDirection.In)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDTHChannelListBySearch(ChannelApiParam obj)
        {
            if (SecureAuthentication != null)
            {
                try
                {
                    int Output = CheckLoginReturnUserId(SecureAuthentication).ValueFromSQL;
                    if (Output > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(obj.channelname))
                        {
                            GetChannelPack proc = new GetChannelPack();
                            return Serialize(proc.GetChannelListBySearch(obj));
                        }
                        else
                        {
                            return Serialize(new AuthResponse(0, "Please Enter Channel Name."));
                        }
                    }
                    else
                    {
                        return Serialize(new AuthResponse(0, Output == -1 ? "Authentication is NULL" : "Invalid Authentication"));
                    }
                       
                }
                catch (Exception ex)
                {
                    SystemLog("GetDTHChannelListBySearch", ex.Message);
                    ErrorReport.LogError(ex, "GetDTHChannelListBySearch");
                    return Serialize(new AuthResponse(0, "Internal server error"));
                }
            }
            return Serialize(new AuthResponse(0, "Authentication information not provided."));
        }

        #endregion

    }
}





