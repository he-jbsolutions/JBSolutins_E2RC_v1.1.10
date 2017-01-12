using System.Data.SqlClient;
using System.Data;
using System;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;

namespace e2rcModel.DataAccessLayer
{
    public class DAL
    {
        // Variable declaration.
        private static string ConnectionString = string.Empty;

        // connection varibale initally assinged to NULL.
        private SqlConnection sqlConnection = null;

        public DAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection1"].ConnectionString;
        }

        // Function will send error detail to the concerned person.
        public void Error_SendMail(Exception ex)
        {
            LogError(ex);
     
            //MailMessage mail = new MailMessage();
            //mail.To.Add("jyoti.malwadkar@vidushigoc.com");

            //mail.From = new MailAddress(ConfigurationManager.AppSettings["Mailfrom"]);

            //mail.Subject = "E2RC Site error details";
            //mail.Body = "<span style='font-family: Calibri; font-size: 14px;color:#1F497D;'>Hi , <br/> <br/></br/> Error occurred in the E2RC website Application. <br/></br/> <br/></br/> Details are shown below:  <br/><br/>" +
            //    "<br/><br/>  <b>Exception Message:</b><br/>" + ex.Message + "<br/><br/><b>Stack Trace:</b> <br/> " + ex.StackTrace + " </span><span style='font-family: Calibri; font-size: 14px;color:#1F497D;'> <br/></br/> <br/></br/>Thanks for your support. <br/><br/> Website Error handler</span>";
            //mail.IsBodyHtml = true;
           
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = ConfigurationManager.AppSettings["MailServer"]; ;
            //smtp.Port = 25;
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Username"], ConfigurationManager.AppSettings["Password"]);
            //try
            //{
            //    smtp.Send(mail);
            //}
            //catch (Exception ex1)
            //{
            //    throw ex1;

            //}
            
        }

        private void LogError(Exception ex)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLog\\ErrorLog.txt", true))
                {
                    file.WriteLine("============================================ Error Details ============================================");
                    file.WriteLine(DateTime.Now.ToString());
                    file.WriteLine("");
                    file.WriteLine("Error Message :==> ");
                    file.WriteLine(ex.Message);
                    file.WriteLine("");
                    file.WriteLine("Error Stack Strace :==> ");
                    file.WriteLine(ex.StackTrace);
                    file.Close();
                }
            }
            catch (Exception ex1)
            {
                
                throw ex1;
            }
            
        }

        // Update function with Output parameter.
        public string Update(string ProcedureName, object[] parameters, object[] values, string strResultOut)
        {
            SqlCommand sqlCommand = GetSqlCommond(ProcedureName, parameters, values, false);
            if (sqlCommand != null)
            {
                try
                {
                    sqlCommand.Parameters.Add(strResultOut, SqlDbType.NVarChar, 500);
                    sqlCommand.Parameters[strResultOut].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    return (string)sqlCommand.Parameters[strResultOut].Value;
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    //return "Error occurred while updation.Please contact to support Team.";
                    return "";
                }
                finally
                {
                    SqlConnClose();
                }
            }
            return (string)sqlCommand.Parameters[strResultOut].Value;
        }
        public int Update(string ProcedureName, object[] parameters, object[] values, string strResultOut,int update)
        {
            SqlCommand sqlCommand = GetSqlCommond(ProcedureName, parameters, values, false);
            if (sqlCommand != null)
            {
                try
                {
                    sqlCommand.Parameters.Add(strResultOut,SqlDbType.Int);
                    sqlCommand.Parameters[strResultOut].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    return (int)sqlCommand.Parameters[strResultOut].Value;
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    //return "Error occurred while updation.Please contact to support Team.";
                    return 2;
                }
                finally
                {
                    SqlConnClose();
                }
            }
            return (int)sqlCommand.Parameters[strResultOut].Value;
        }

        // Update function need SPROC name, paramenter and their values.
        public bool Update(string ProcedureName, object[] parameters, object[] values)
        {
            SqlCommand sqlCommand = GetSqlCommond(ProcedureName, parameters, values, false);
            if (sqlCommand != null)
            {
                try
                {
                    return (sqlCommand.ExecuteNonQuery() > 0) ? true : false;
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    return false;
                }
                finally
                {
                    SqlConnClose();
                }
            }
            else
            {
                return false;
            }
        }

        // Insert function need SPROC name, paramenter and their values.
        public bool Insert(string ProcedureName, object[] parameters, object[] values)
        {
            SqlCommand sqlCommand = GetSqlCommond(ProcedureName, parameters, values, false);
            if (sqlCommand != null)
            {
                try
                {                   
                    return (sqlCommand.ExecuteNonQuery() > 0) ? true : false;
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    return false;
                }
                finally
                {
                    SqlConnClose();
                }
            }
            else
            {
                return false;
            }
        }

        // Delete function need SPROC name, paramenter and their values.
        public bool Delete(string ProcedureName, object[] parameters, object[] values)
        {
            SqlCommand sqlCommand = GetSqlCommond(ProcedureName, parameters, values, false);
            if (sqlCommand != null)
            {
                try
                {
                    return (sqlCommand.ExecuteNonQuery() > 0) ? true : false;
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    return false;
                }
                finally
                {
                    SqlConnClose();
                }
            }
            else
            {
                return false;
            }
        }

        // execute the store procedue depending on the parameter supplied and value. returns Dataset.
        public DataSet ExecuteStoredProcedure(string ProcedureName, object[] parameters, object[] values)
        {
            SqlCommand sqlCommand = GetSqlCommond(ProcedureName, parameters, values, true);
            if (sqlCommand != null)
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataSet);
                    return dataSet;
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    return null;

                }
            }
            else
            {
                return null;
            }
        }

        // Will execute the Query string and returns the resultant dataset.
        internal DataSet GetDataByQuery(string QueryString)
        {
            try
            {
                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(QueryString, ConnectionString);
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                Error_SendMail(ex);
                return null;
            }

        }

        // execute the SPROC and return result set as integer.
        internal object ExecuteScalar(string ProcedureName, object[] Parameters, object[] Values)
        {
            SqlCommand sqlCommand = GetSqlCommond(ProcedureName, Parameters, Values, false);

            if (sqlCommand != null)
            {
                try
                {
                    object o = sqlCommand.ExecuteScalar();
                    return o;
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    return -1;
                }
                finally
                {
                    SqlConnClose();
                }
            }
            else
            {
                return -1;
            }
        }

        // execute the SPROC
        private SqlCommand GetSqlCommond(string ProcedureName, object[] Parameters, object[] Values, bool IsDisconnected)
        {
            if (Parameters.Length == Values.Length)
            {
                if (IsDisconnected)
                {
                    sqlConnection = new SqlConnection(ConnectionString);
                }
                else
                {
                    SqlConnOpen();
                }
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(ProcedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    for (int index = 0; index < Parameters.Length; index++)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(Parameters[index].ToString(), Values[index]));
                    }

                    return sqlCommand;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        internal object ExecuteScalar(string QueryString)
        {
            sqlConnection = new SqlConnection(ConnectionString);
            SqlConnOpen();
            SqlCommand sqlCommand = new SqlCommand(QueryString, sqlConnection);
            if (sqlCommand != null)
            {
                try
                {
                    return (object)sqlCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    return -1;
                }
                finally
                {
                    SqlConnClose();
                }
            }
            else
            {
                return -1;
            }
        }

        // Check for SQL connection is open or not. retuns boolean value.
        private bool SqlConnOpen()
        {
            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
            }
            catch (SqlException ex)
            {
                Error_SendMail(ex);
                return false;
            }
            return true;
        }

        private void SqlConnClose()
        {
            sqlConnection.Close();
        }

        /// <summary>
        /// it is overloaded version and executie the stored procedure without parameters.
        /// </summary>
        /// <param name="ProcedureName">String : Name of the Stored procedure.</param>
        /// <returns> DataSet : return data from database in dataset.</returns>
        public DataSet ExecuteStoredProcedure(string ProcedureName)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ProcedureName, new SqlConnection(ConnectionString));
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(ProcedureName, ConnectionString);

                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                Error_SendMail(ex);
                return null;
            }
        }

        // executes the SPROC depending on parameter suppied.
        public object ExecuteStoredProcedure(string ProcedureName, object[] parameters, object[] values, string outParameter, string outParameterValue, SqlDbType sqlDbType)
        {
            SqlCommand sqlCommand = GetSqlCommond(ProcedureName, parameters, values, false);
            SqlParameter sqlParameter = new SqlParameter(outParameter, outParameterValue);

            sqlParameter.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(sqlParameter);

            sqlParameter.SqlDbType = sqlDbType;

            if (sqlCommand != null)
            {
                try
                {
                    sqlCommand.ExecuteNonQuery();

                    return sqlCommand.Parameters[outParameter].Value;
                }
                catch (Exception ex)
                {
                    Error_SendMail(ex);
                    return -1;
                }
                finally
                {
                    SqlConnClose();
                }
            }
            else
            {
                return -1;
            }
        }
    }
}
