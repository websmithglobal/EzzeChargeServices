using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;
using EzzeChargeServices._MyConnection;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for MEMBERS
/// </summary>
/// 

namespace EzzeChargeServices
{
    public sealed class MEMBERS : SqlHelper
    {
        /// <summary>
        /// Holds the procedural information.
        /// </summary>
        public struct SQLReturnValue
        {
            /// <summary>
            /// Integral value returned from SQL Procedure.
            /// </summary>
            public int ValueFromSQL;
            /// <summary>
            /// String message returned from SQL Procedure.
            /// </summary>
            public string MessageFromSQL;
            public string MessageFromSQL1;
        }
        public struct SqlReturnMessage
        {
            public string MessageFromSql;
        }
        /// <summary>
        /// Holds the procedures integeral output value.
        /// </summary>
        public struct SQlReturnInteger
        {
            /// <summary>
            /// Integral value returned from SQL Procedure.
            /// </summary>
            public int ValueFromSQL;
        }
    }
}