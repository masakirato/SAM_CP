using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using log4net;
public class dbo_AccountTypeDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region Newwwwwwwwwwww
    public static List<dbo_AccountTypeClass> GetAccountType_New()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetAccountType";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Mode", "GetAccountType");
        DataTable dt = new DataTable();

        List<dbo_AccountTypeClass> item = new List<dbo_AccountTypeClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_AccountTypeClass()
                    {
                        /// Account_ID	Account_Type_ID	Account_Type	
                        /// รายรับ	    01	            ธนาคารกสิกรไทย	
                        /// รายรับ	    03	            ธนาคารทหารไทย	
                        /// รายรับ	    04	            ธนาคารกรุงไทย	
                        /// รายรับ	    02	            ธนาคารไทยพาณิชย์	

                        Account_ID = reader["Account_ID"] is DBNull ? null : reader["Account_ID"].ToString(),
                        Account_Type_ID = reader["Account_Type_ID"] is DBNull ? null : reader["Account_Type_ID"].ToString(),
                        Account_Type = reader["Account_Type"] is DBNull ? null : reader["Account_Type"].ToString()
                    });
                }
            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static List<dbo_AccountCodeClass> GetAccountCode_New(string accountTypeID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetAccountType";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Mode", "GetAccountCode");
        selectCommand.Parameters.AddWithValue("@Account_Type_ID", accountTypeID);
        DataTable dt = new DataTable();

        List<dbo_AccountCodeClass> item = new List<dbo_AccountCodeClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_AccountCodeClass()
                    {
                        /// Account_Type_ID	Account_Code	Account_Name	
                        /// 01	            0101	        11223344556677	
                        /// 01	            0102	        11223344556677	
                        /// 02	            0202	        11223344556677	

                        Account_Type_ID = reader["Account_Type_ID"] is DBNull ? null : reader["Account_Type_ID"].ToString(),
                        Account_Code = reader["Account_Code"] is DBNull ? null : reader["Account_Code"].ToString(),
                        Account_Name = reader["Account_Name"] is DBNull ? null : reader["Account_Name"].ToString()
                    });
                }
            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static List<dbo_AccountCodeClass> GetAccountRevenue()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetAccountRevenue";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        DataTable dt = new DataTable();

        List<dbo_AccountCodeClass> item = new List<dbo_AccountCodeClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_AccountCodeClass()
                    {
                        /// Account_Type_ID	Account_Code	Account_Name	
                        /// 01	            0101	        11223344556677	
                        /// 01	            0102	        11223344556677	
                        /// 02	            0202	        11223344556677	

                        Account_Type_ID = reader["Account_Type_ID"] is DBNull ? null : reader["Account_Type_ID"].ToString(),
                        Account_Code = reader["Account_Code"] is DBNull ? null : reader["Account_Code"].ToString(),
                        Account_Name = reader["Account_Name"] is DBNull ? null : reader["Account_Name"].ToString()
                    });
                }
            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }
    public static List<dbo_AccountCodeClass> GetAccountExpense()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetAccountExpense";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        DataTable dt = new DataTable();

        List<dbo_AccountCodeClass> item = new List<dbo_AccountCodeClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_AccountCodeClass()
                    {
                        /// Account_Type_ID	Account_Code	Account_Name	
                        /// 01	            0101	        11223344556677	
                        /// 01	            0102	        11223344556677	
                        /// 02	            0202	        11223344556677	

                        Account_Type_ID = reader["Account_Type_ID"] is DBNull ? null : reader["Account_Type_ID"].ToString(),
                        Account_Code = reader["Account_Code"] is DBNull ? null : reader["Account_Code"].ToString(),
                        Account_Name = reader["Account_Name"] is DBNull ? null : reader["Account_Name"].ToString()
                    });
                }
            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static Boolean InsertAccountType_New(dbo_AccountTypeClass clsAccountType, string userName)
    {
        /// Account_ID	Account_Type_ID	Account_Type	
        /// รายรับ	    01	            ธนาคารกสิกรไทย	
        /// รายรับ	    03	            ธนาคารทหารไทย	
        /// รายรับ	    04	            ธนาคารกรุงไทย	
        /// รายรับ	    02	            ธนาคารไทยพาณิชย์	

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "InsertAccountType";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "InsAccountType");
        insertCommand.Parameters.AddWithValue("@Account_ID", clsAccountType.Account_ID);
        insertCommand.Parameters.AddWithValue("@InsertParam", clsAccountType.Account_Type);
        insertCommand.Parameters.AddWithValue("@InsertBy", userName);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static Boolean InsertAccountCode_New(dbo_AccountCodeClass clsAccountCode, string userName,string Account_ID)
    {
        /// Account_Type_ID	Account_Code	Account_Name	
        /// 01	            0101	        11223344556677	
        /// 01	            0102	        11223344556677	
        /// 02	            0202	        11223344556677	

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "InsertAccountType";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "InsAccountCode");
        insertCommand.Parameters.AddWithValue("@Account_Type_ID", clsAccountCode.Account_Type_ID);
        insertCommand.Parameters.AddWithValue("@InsertParam", clsAccountCode.Account_Name);
        insertCommand.Parameters.AddWithValue("@InsertBy", userName);
        insertCommand.Parameters.AddWithValue("@Account_ID", Account_ID);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static Boolean UpdateAccountType_New(dbo_AccountTypeClass clsAccountType, string userName)
    {
        /// Account_ID	Account_Type_ID	Account_Type	
        /// รายรับ	    01	            ธนาคารกสิกรไทย	
        /// รายรับ	    03	            ธนาคารทหารไทย	
        /// รายรับ	    04	            ธนาคารกรุงไทย	
        /// รายรับ	    02	            ธนาคารไทยพาณิชย์	

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "UpdateAccountType";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "UpdAccountType");
        insertCommand.Parameters.AddWithValue("@Account_ID", clsAccountType.Account_ID);
        insertCommand.Parameters.AddWithValue("@Account_Type_ID", clsAccountType.Account_Type_ID);
        insertCommand.Parameters.AddWithValue("@UpdateBY", userName);
        insertCommand.Parameters.AddWithValue("@UpdateParam", clsAccountType.Account_Type);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static Boolean UpdateAccountCode_New(dbo_AccountCodeClass clsAccountCode, string userName)
    {
        /// Account_Type_ID	Account_Code	Account_Name	
        /// 01	            0101	        11223344556677	
        /// 01	            0102	        11223344556677	
        /// 02	            0202	        11223344556677	

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "UpdateAccountType";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "UpdAccountCode");
        insertCommand.Parameters.AddWithValue("@Account_Code", clsAccountCode.Account_Code);
        insertCommand.Parameters.AddWithValue("@UpdateBY", userName);
        insertCommand.Parameters.AddWithValue("@UpdateParam", clsAccountCode.Account_Name);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static Boolean DeleteAccountType_New(string accountTypeID)
    {
        /// Account_ID	Account_Type_ID	Account_Type	
        /// รายรับ	    01	            ธนาคารกสิกรไทย	
        /// รายรับ	    03	            ธนาคารทหารไทย	
        /// รายรับ	    04	            ธนาคารกรุงไทย	
        /// รายรับ	    02	            ธนาคารไทยพาณิชย์	

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "DeleteAccountType";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "DelAccountType");
        insertCommand.Parameters.AddWithValue("@Account_Type_ID", accountTypeID);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static Boolean DeleteAccountCode_New(string accountCode)
    {
        /// Account_Type_ID	Account_Code	Account_Name	
        /// 01	            0101	        11223344556677	
        /// 01	            0102	        11223344556677	
        /// 02	            0202	        11223344556677	

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "DeleteAccountType";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "DelAccountCode");
        insertCommand.Parameters.AddWithValue("@Account_Code", accountCode);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return false;
        }
        finally
        {
            connection.Close();

        }
    }
    #endregion

    #region New Warut Edit
    public static DataSet GetAccountType()
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();

        try
        {
            connDB.openConnection();
            string sqlStr = "GetAccountType";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[1];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetAccountType";
            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);

            if (ds.Tables[0].Rows.Count == 0)
            {
                DataSet dsAccountType = new DataSet();
                DataTable dtAccountType = new DataTable();
                dtAccountType.Columns.Add("Account_ID");
                dtAccountType.Columns.Add("Account_Type_ID");
                dtAccountType.Columns.Add("Account_Type");

                DataRow drItem = dtAccountType.NewRow();
                dtAccountType.Rows.Add(drItem);
                dsAccountType.Tables.Add(dtAccountType);

                ds = dsAccountType;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return ds;
    }

    public static DataSet GetAccountCode(string accountTypeID)
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();

        try
        {
            connDB.openConnection();
            string sqlStr = "GetAccountType";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[2];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetAccountCode";

            spParameter[1] = new SqlParameter("@Account_Type_ID", SqlDbType.VarChar, 2);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = accountTypeID;
            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);


            if (ds.Tables[0].Rows.Count == 0)
            {
                DataSet dsAccountCode = new DataSet();
                DataTable dtAccountCode = new DataTable();
                dtAccountCode.Columns.Add("Account_Type_ID");
                dtAccountCode.Columns.Add("Account_Code");
                dtAccountCode.Columns.Add("Account_Name");

                DataRow drItem = dtAccountCode.NewRow();
                dtAccountCode.Rows.Add(drItem);
                dsAccountCode.Tables.Add(dtAccountCode);

                ds = dsAccountCode;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return ds;
    }

    public static Boolean InsertAccountType(string account_ID, string account_Type, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "InsertAccountType";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[4];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "InsAccountType";

            spParameter[1] = new SqlParameter("@Account_ID", SqlDbType.VarChar, 7);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = account_ID;

            spParameter[2] = new SqlParameter("@InsertParam", SqlDbType.VarChar, 100);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = account_Type;

            spParameter[3] = new SqlParameter("@InsertBy", SqlDbType.VarChar, 10);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = userName;

            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean InsertAccountCode(string accountTypeID, string accountName, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "InsertAccountType";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[4];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "InsAccountCode";

            spParameter[1] = new SqlParameter("@Account_Type_ID", SqlDbType.VarChar, 2);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = accountTypeID;

            spParameter[2] = new SqlParameter("@InsertParam", SqlDbType.VarChar, 100);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = accountName;

            spParameter[3] = new SqlParameter("@InsertBy", SqlDbType.VarChar, 10);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = userName;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean UpdateAccountType(string accountID, string accountTypeID, string accountType, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "UpdateAccountType";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[5];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "UpdAccountType";

            spParameter[1] = new SqlParameter("@Account_ID", SqlDbType.VarChar, 7);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = accountID;

            spParameter[2] = new SqlParameter("@Account_Type_ID", SqlDbType.VarChar, 2);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = accountTypeID;

            spParameter[3] = new SqlParameter("@UpdateBY", SqlDbType.VarChar, 10);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = userName;

            spParameter[4] = new SqlParameter("@UpdateParam", SqlDbType.VarChar, 100);
            spParameter[4].Direction = ParameterDirection.Input;
            spParameter[4].Value = accountType;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean UpdateAccountCode(string accountCode, string accountName, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "UpdateAccountType";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[4];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "UpdAccountCode";

            spParameter[1] = new SqlParameter("@Account_Code", SqlDbType.VarChar, 4);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = accountCode;

            spParameter[2] = new SqlParameter("@UpdateBY", SqlDbType.VarChar, 10);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = userName;

            spParameter[3] = new SqlParameter("@UpdateParam", SqlDbType.VarChar, 100);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = accountName;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean DeleteAccountType(string accountTypeID)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "DeleteAccountType";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[2];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "DelAccountType";

            spParameter[1] = new SqlParameter("@Account_Type_ID", SqlDbType.VarChar, 2);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = accountTypeID;
            #endregion

            int rowsAffectedItem = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean DeleteAccountCode(string accountCode)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "DeleteAccountType";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[2];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "DelAccountCode";

            spParameter[1] = new SqlParameter("@Account_Code", SqlDbType.VarChar, 4);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = accountCode;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }
    #endregion
}