using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
namespace EzzeChargeServices.Bussiness
{
    public class RechargeModule : SqlHelper
    {
        public struct Recharge
        {
            public string RCNoRMALCODE { get; set; }
            public int Mainid { get; set; }
            public int AmtToRech { get; set; }
            public string NumToRech { get; set; }
            public int rctype { get; set; }
            public int OPCODE { get; set; }
            public string Via { get; set; }
            public int Slabid { get; set; }
            public string SlabNm { get; set; }
            public int Slabcommiid { get; set; }
            public string slabfor { get; set; }
            public float commi { get; set; }
            public int UserLevel { get; set; }
            public int Stateid { get; set; }
            public int AllUser { get; set; }
            public string TxId { get; set; }
            public char Status { get; set; }
            public Int64 rechargeid { get; set; }
            public string RouteTitle { get; set; }
            public int RechMethod { get; set; }
            public int ApiId { get; set; }
            public int MachineId { get; set; }
            public int SmartId { get; set; }
            public string AmtSpecific { get; set; }

            public int RangeFrom { get; set; }
            public int RangeTo { get; set; }
            public int RouteId { get; set; }
            public int RechargeVia { get; set; }
            public string UserMobile { get; set; }
            public string Transpwd { get; set; }
            public string OldTranspwd { get; set; }
            public string keyword { get; set; }
            public string Action { get; set; }
            public string TranPassword { get; set; }
            public int UpperID { get; set; }
            public int ARCHIVEID { get; set; }
            public string RerouteType { get; set; }
            public string ReRouteID { get; set; }
            public string RerouteULevel { get; set; }

            public string Myflag { get; set; }
            public int nagativechrgid { get; set; }
            public bool isnagativechrg { get; set; }
        }
        public static MEMBERS.SQlReturnInteger UPDATERECHARGEVIAAPI(string[,] param)
        {
            return ExecuteProcedureReturnInteger("ADDREQUEST_VIA_API", param);
        }
        public static MEMBERS.SQLReturnValue DORECHARGE(Recharge obj)
        {
            string[,] param = {{"opcod",obj.OPCODE.ToString()},
                           {"rctype",obj.rctype.ToString()},
                           {"numtorech",obj.NumToRech},
                           {"amttorech",obj.AmtToRech.ToString()},
                           {"mainid",obj.Mainid.ToString()},
                           {"REQVIA",obj.Via},
                           {"TRANPWD", obj.TranPassword},
                           {"OPTIONALPARAM",string.Empty}
                          };
            return ExecuteProcWithMessageValue("ADD_REQUEST_TO_RECHARGE", param, true);
        }
        public static MEMBERS.SQLReturnValue UpdateRequests(Recharge obj)
        {
            string[,] param = {{"TRANSID",obj.TxId.ToString()},
                           {"STATUS",obj.Status.ToString()},
                           {"ID",obj.rechargeid.ToString()},
                           {"VIA",obj.RechargeVia.ToString()}
                          };
            return ExecuteProcWithMessageValue("UPDATE_REQUEST", param, true);
        }
    }
}