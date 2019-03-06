using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System;
using System.Web;

namespace EzzeChargeServices._MyConnection
{
    /// <summary>
    /// Provides access to sql database.
    /// </summary>
    public class SqlHelper
    {
        static SqlConnection CONNECTION = new SqlConnection(ConfigurationManager.AppSettings["_CONSTR"].ToString());
        /// <summary>
        /// Reset the connection.
        /// </summary>
        static void ResetConnection()
        {
            if (CONNECTION.State != ConnectionState.Open)
            {
                CONNECTION.Open();
            }
        }
        public SqlConnection GetConnection { get { return CONNECTION; } }
        ///<summary>
        /// Execute the give procedure with provided parameter collection.
        /// </summary>
        /// <param name="ProcedureName">String name of the procedure.</param>
        /// <param name="param">Collection of the SQL Parameter</param>
        /// <param name="AddOutputParameters">Optionally add default output parameters to current parameter collection.</param>
        /// <returns>Returns the associated Sql Command object.</returns>    
        protected static SqlCommand ExecuteProcedure(string ProcedureName, SqlParameter[] param, bool AddOutputParameters)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                COMMAND.Parameters.AddRange(param);
                if (AddOutputParameters == true)
                {
                    COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                }
                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                return COMMAND;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }

        protected DataTable ExecuteProcedureReturnDataTable(string ProcedureName, string[,] ParamValue)
        {
            SqlCommand COMMAND = new SqlCommand();
            COMMAND.CommandText = ProcedureName;
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            COMMAND.Connection = MYCON;
            COMMAND.CommandTimeout = 0;
            COMMAND.CommandType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
            for (int i = 0; i < param.Length; i++)
            {
                param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
            }
            COMMAND.Parameters.AddRange(param);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(COMMAND);
            da.Fill(dt);
            return dt;
        }
        ///<summary>
        /// Execute the give procedure with provided parameter collection.
        /// </summary>
        /// <param name="ProcedureName">String name of the procedure.</param>
        /// <param name="ParamValue">Collection of the SQL Parameter as two dimensional array.</param>
        /// <param name="AddOutputParameters">Optionally add default output parameters to current parameter collection.</param>
        /// <returns>Returns the associated Sql Command object.</returns>    
        protected static SqlCommand ExecuteProcedure(string ProcedureName, string[,] ParamValue, bool AddOutputParameters)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);
                if (AddOutputParameters == true)
                {
                    COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                }
                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                return COMMAND;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }
        public static MEMBERS.SQLReturnValue ExecuteProcWithMessage(string ProcedureName, string[,] ParamValue, bool AddOutputParameters)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);
                if (AddOutputParameters == true)
                {
                    COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                }
                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                MEMBERS.SQLReturnValue M = new MEMBERS.SQLReturnValue();
                M.MessageFromSQL = COMMAND.Parameters["@OUTMESSAGE"].Value.ToString();
                M.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
                return M;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }
        public static MEMBERS.SQLReturnValue ExecuteProcWithMessageValue(string ProcedureName, string[,] ParamValue, bool AddOutputParameters)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
               
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);
                if (AddOutputParameters == true)
                {

                    COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                }
                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                MEMBERS.SQLReturnValue M = new MEMBERS.SQLReturnValue();
                M.MessageFromSQL = COMMAND.Parameters["@OUTMESSAGE"].Value.ToString();
                M.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
                return M;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }
        public static MEMBERS.SQLReturnValue ExecuteProcWithMessageValue2(string ProcedureName, string[,] ParamValue, bool AddOutputParameters)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
               
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);
                if (AddOutputParameters == true)
                {
                    COMMAND.Parameters.Add("@OUTMESSAGE1", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                }
                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                MEMBERS.SQLReturnValue M = new MEMBERS.SQLReturnValue();
                M.MessageFromSQL1 = COMMAND.Parameters["@OUTMESSAGE1"].Value.ToString();
                M.MessageFromSQL = COMMAND.Parameters["@OUTMESSAGE"].Value.ToString();
                M.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
                return M;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }

        /// <summary>
        /// Executes the procedure which contains the data from the sql table.
        /// </summary>
        /// <param name="ProcedureName">String Name of the procedure.</param>
        /// <param name="param">Collection of parameters.</param>
        /// <returns>Datatable with data from server.</returns>
        protected static DataTable ExecuteProcedure(string ProcedureName, SqlParameter[] param)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(ExecuteProcedure(ProcedureName, param, false));
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Executes the procedure and return output value and message from sql server.
        /// </summary>
        /// <param name="ProcedureName">Name of procedure</param>
        /// <param name="param">Collection of sql parameters</param>
        /// <returns></returns>
        protected static MEMBERS.SQLReturnValue ExecuteProcedureReturnValue(string ProcedureName, SqlParameter[] param)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                MEMBERS.SQLReturnValue returnval = new MEMBERS.SQLReturnValue();
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                COMMAND.Parameters.AddRange(param);
                ///Adds the output parameter
                COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                ///Retrive value from output parameters to return value structure.
                returnval.MessageFromSQL = COMMAND.Parameters["@OUTMESSAGE"].Value.ToString();
                returnval.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());

               
                return returnval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }

        /// <summary>
        /// Executes the procedure and return output value and message from sql server.
        /// </summary>
        /// <param name="ProcedureName">Name of procedure</param>
        /// <param name="ParamValue">Collection of sql parameters as two dimentional array.</param>
        /// <returns>Returns Return values from sql procedure.</returns>
        protected static MEMBERS.SQLReturnValue ExecuteProcedureReturnValue(string ProcedureName, string[,] ParamValue)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                MEMBERS.SQLReturnValue returnval = new MEMBERS.SQLReturnValue();
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);
                ///Adds the output parameter
                COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                ///Retrive value from output parameters to return value structure.
                returnval.MessageFromSQL = COMMAND.Parameters["@OUTMESSAGE"].Value.ToString();
                returnval.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
               
                return returnval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }

        /// <summary>
        /// Executes the procedure and return output value and message from sql server.
        /// </summary>
        /// <param name="ProcedureName">Name of procedure</param>
        /// <param name="param">Collection of sql parameters</param>
        /// <returns></returns>
        protected static MEMBERS.SQlReturnInteger ExecuteProcedureReturnInteger(string ProcedureName, SqlParameter[] param)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                MEMBERS.SQlReturnInteger returnval = new MEMBERS.SQlReturnInteger();
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
               
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                COMMAND.Parameters.AddRange(param);
                ///Adds the output parameter
                COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                ///Retrive value from output parameters to return value structure.
                returnval.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());

                
                return returnval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }
        /// <summary>
        /// Executes the procedure and return output value and message from sql server.
        /// </summary>
        /// <param name="ProcedureName">Name of procedure</param>
        /// <param name="ParamValue">Collection of sql parameters as two dimentional array.</param>
        /// <returns>Returns Return values from sql procedure.</returns>
        protected static MEMBERS.SQlReturnInteger ExecuteProcedureReturnInteger(string ProcedureName, string[,] ParamValue)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
                MEMBERS.SQlReturnInteger returnval = new MEMBERS.SQlReturnInteger();
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
               
                COMMAND.Connection = MYCON;
                COMMAND.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);
                ///Adds the output parameter
                COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;

                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                COMMAND.ExecuteNonQuery();
                MYCON.Close();
                ///Retrive value from output parameters to return value structure.
                returnval.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
                
                return returnval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }
        

        /// <summary>
        /// Executes SQL Query.
        /// </summary>
        /// <param name="Query">String Sql Statement</param>
        /// <returns>Returns datatable associated with the SQL Query.</returns>
        protected static DataTable ExecuteQuery(string Query)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
               
                SqlDataAdapter da = new SqlDataAdapter(Query, MYCON);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }
        /// <summary>
        /// This is extended version of selected query.
        /// Here user has to pass query with parameters. ie. "select column_name from table_name where column_name=@parameter".
        /// And second parameter will be two dimensional array contains first argument as passed in query ie. @parameter in above example.
        /// </summary>
        /// <param name="QueryWithParameters">Any valid selected statement with parameter name.</param>
        /// <param name="ParameterValue">An two dimensional array of object containing parameter names and its value.ie. 
        /// 1st element @parametername and 2nd parameter will be value.</param>
        /// <returns>Returns datatable with result.</returns>
        public DataTable GetDataUsingQuery(string QueryWithParameters, object[,] ParameterValue)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            cmd.Connection = MYCON;
            SqlParameter[] param = new SqlParameter[ParameterValue.GetUpperBound(0) + 1];
            for (int i = 0; i < param.Length; i++)
            {
                param[i] = new SqlParameter("@" + ParameterValue[i, 0].ToString(), ParameterValue[i, 1]);
            }
            cmd.CommandText = QueryWithParameters;
            cmd.Parameters.AddRange(param);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public static DataTable GetDataUsingQuery(string Query)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
               
                SqlDataAdapter da = new SqlDataAdapter(Query, MYCON);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }


        /// <summary>
        /// Adds Parameter and its value to SqlParameter Array collection.
        /// </summary>
        /// <param name="ParamValue">Two dimentional array with First => Parameter, Second => Value.</param>
        /// <returns>Returns Sql Parameter collection containing parameter and its values.</returns>
        protected static SqlParameter[] AddParameterAndExecute(string[,] ParamValue)
        {
            SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
            for (int i = 0; i < param.Length; i++)
            {
                param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
            }
            return param;
        }
        protected static DataTable ExecuteProcedureWithReturnDatatable(string ProcedureName, SqlParameter[] param)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(ExecuteProcedure(ProcedureName, param, false));
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int DML(string q)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            int i = 0;
            try
            {
                
                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                SqlCommand cmd = new SqlCommand(q,MYCON);
                i = cmd.ExecuteNonQuery();
                MYCON.Close();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                MYCON.Close();
            }
            return i;
        }

        

        protected static DataTable ExecuteProcedureVirendra(string ProcedureName, string[,] ParamValue)
        {
            SqlConnection MYCON = new SqlConnection(CONNECTION.ConnectionString);
            try
            {
               
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddRange(param);
                cmd.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandText = ProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
                cmd.Connection = MYCON;
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MYCON.Close();
            }
        }
    }
}