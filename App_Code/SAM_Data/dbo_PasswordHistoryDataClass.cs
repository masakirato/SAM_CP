using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_PasswordHistoryDataClass
{
  
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[PasswordHistorySelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return dt;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static List<dbo_PasswordHistoryClass> Search(string User_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[PasswordHistorySearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }

        List<dbo_PasswordHistoryClass> item = new List<dbo_PasswordHistoryClass>();
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_PasswordHistoryClass()
                    {
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Password = reader["Password"] is DBNull ? null : dbo_UserDataClass.Decrypt(reader["Password"].ToString()),
                        Last_Password_Change_Or_Reset = reader["Last_Password_Change_Or_Reset"] is DBNull ? null : (DateTime?)reader["Last_Password_Change_Or_Reset"]
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

    public static dbo_PasswordHistoryClass Select_Record(dbo_PasswordHistoryClass clsdbo_PasswordHistoryPara)
    {
        dbo_PasswordHistoryClass clsdbo_PasswordHistory = new dbo_PasswordHistoryClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[PasswordHistorySelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@User_ID", clsdbo_PasswordHistoryPara.User_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_PasswordHistory.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_PasswordHistory.Password = reader["Password"] is DBNull ? null : reader["Password"].ToString();
                clsdbo_PasswordHistory.Last_Password_Change_Or_Reset = reader["Last Password_Change_Or_Reset"] is DBNull ? null : (DateTime?)reader["Last Password_Change_Or_Reset"];
            }
            else
            {
                clsdbo_PasswordHistory = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_PasswordHistory;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_PasswordHistory;
    }

    public static bool Add(dbo_PasswordHistoryClass clsdbo_PasswordHistory)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[PasswordHistoryInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_PasswordHistory.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_PasswordHistory.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }



        if (clsdbo_PasswordHistory.Password != null)
        {
            clsdbo_PasswordHistory.Password = dbo_UserDataClass.Encrypt(clsdbo_PasswordHistory.Password);

            insertCommand.Parameters.AddWithValue("@Password", clsdbo_PasswordHistory.Password);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Password", DBNull.Value);
        }



        if (clsdbo_PasswordHistory.Last_Password_Change_Or_Reset.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Last_Password_Change_Or_Reset", clsdbo_PasswordHistory.Last_Password_Change_Or_Reset);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Password_Change_Or_Reset", DBNull.Value);
        }
        insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
        insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
        try
        {
            connection.Open();
            insertCommand.ExecuteNonQuery();
            int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
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

    public static bool Update(dbo_PasswordHistoryClass olddbo_PasswordHistoryClass,
           dbo_PasswordHistoryClass newdbo_PasswordHistoryClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[PasswordHistoryUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_PasswordHistoryClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", newdbo_PasswordHistoryClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", DBNull.Value);
        }
        if (newdbo_PasswordHistoryClass.Password != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPassword", newdbo_PasswordHistoryClass.Password);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPassword", DBNull.Value);
        }
        if (newdbo_PasswordHistoryClass.Last_Password_Change_Or_Reset.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Password_Change_Or_Reset", newdbo_PasswordHistoryClass.Last_Password_Change_Or_Reset);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Password_Change_Or_Reset", DBNull.Value);
        }
        if (olddbo_PasswordHistoryClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldUser_ID", olddbo_PasswordHistoryClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
        }
        if (olddbo_PasswordHistoryClass.Password != null)
        {
            updateCommand.Parameters.AddWithValue("@OldPassword", olddbo_PasswordHistoryClass.Password);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldPassword", DBNull.Value);
        }
        if (olddbo_PasswordHistoryClass.Last_Password_Change_Or_Reset.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Password_Change_Or_Reset", olddbo_PasswordHistoryClass.Last_Password_Change_Or_Reset);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Password_Change_Or_Reset", DBNull.Value);
        }
        updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
        updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
        try
        {
            connection.Open();
            updateCommand.ExecuteNonQuery();
            int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
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

    public static bool Delete(dbo_PasswordHistoryClass clsdbo_PasswordHistory)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[PasswordHistoryDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_PasswordHistory.User_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", clsdbo_PasswordHistory.User_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
        }
        if (clsdbo_PasswordHistory.Password != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldPassword", clsdbo_PasswordHistory.Password);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPassword", DBNull.Value);
        }
        if (clsdbo_PasswordHistory.Last_Password_Change_Or_Reset.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Password_Change_Or_Reset", clsdbo_PasswordHistory.Last_Password_Change_Or_Reset);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Password_Change_Or_Reset", DBNull.Value);
        }
        deleteCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
        deleteCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
        try
        {
            connection.Open();
            deleteCommand.ExecuteNonQuery();
            int count = System.Convert.ToInt32(deleteCommand.Parameters["@ReturnValue"].Value);
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
}

