using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
public class dbo_LoginHistoryDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[LoginHistorySelectAll]";
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

    public static List<dbo_LoginHistoryClass> Search(string User_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[LoginHistorySearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (User_ID != null)
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }


        DataTable dt = new DataTable();
        List<dbo_LoginHistoryClass> item = new List<dbo_LoginHistoryClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_LoginHistoryClass()
                    {
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Login_Time = reader["Login_Time"] is DBNull ? null : (DateTime?)reader["Login_Time"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString()

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

    public static dbo_LoginHistoryClass Select_Record(dbo_LoginHistoryClass clsdbo_LoginHistoryPara)
    {
        dbo_LoginHistoryClass clsdbo_LoginHistory = new dbo_LoginHistoryClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[LoginHistorySelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@User_ID", clsdbo_LoginHistoryPara.User_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_LoginHistory.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_LoginHistory.Login_Time = reader["Login_Time"] is DBNull ? null : (DateTime?)reader["Login_Time"];
                clsdbo_LoginHistory.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
            }
            else
            {
                clsdbo_LoginHistory = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_LoginHistory;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_LoginHistory;
    }

    public static bool Add(dbo_LoginHistoryClass clsdbo_LoginHistory)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[LoginHistoryInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_LoginHistory.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_LoginHistory.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (clsdbo_LoginHistory.Login_Time.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Login_Time", clsdbo_LoginHistory.Login_Time);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Login_Time", DBNull.Value);
        }
        if (clsdbo_LoginHistory != null)
        {
            insertCommand.Parameters.AddWithValue("@Status", clsdbo_LoginHistory.Status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Status", DBNull.Value);
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

    public static bool Update(dbo_LoginHistoryClass newdbo_LoginHistoryClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[LoginHistoryUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_LoginHistoryClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", newdbo_LoginHistoryClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", DBNull.Value);
        }
        if (newdbo_LoginHistoryClass.Login_Time.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewLogin_Time", newdbo_LoginHistoryClass.Login_Time);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLogin_Time", DBNull.Value);
        }
        if (newdbo_LoginHistoryClass != null)
        {
            updateCommand.Parameters.AddWithValue("@NewStatus", newdbo_LoginHistoryClass.Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStatus", DBNull.Value);
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

    public static bool Delete(dbo_LoginHistoryClass clsdbo_LoginHistory)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[LoginHistoryDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_LoginHistory.User_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", clsdbo_LoginHistory.User_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
        }
        if (clsdbo_LoginHistory.Login_Time.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldLogin_Time", clsdbo_LoginHistory.Login_Time);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldLogin_Time", DBNull.Value);
        }
        if (clsdbo_LoginHistory != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldStatus", clsdbo_LoginHistory.Status);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStatus", DBNull.Value);
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

