using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class dbo_ReadNewsDataClass
{

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ReadNewsSelectAll]";
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
        catch (SqlException)
        {
            return dt;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static List<dbo_ReadNewsClass> Search(String News_ID, String User_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[ReadNewsSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        if (!string.IsNullOrEmpty(News_ID))
        {
            selectCommand.Parameters.AddWithValue("@News_ID", News_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@News_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }


        DataTable dt = new DataTable();
        List<dbo_ReadNewsClass> item = new List<dbo_ReadNewsClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_ReadNewsClass()
                    {
                        News_ID = reader["News_ID"] is DBNull ? null : reader["News_ID"].ToString(),
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Read_Date = reader["Read_Date"] is DBNull ? null : (DateTime?)reader["Read_Date"]
                    });
                }


            }
            reader1.Close();
        }
        catch (SqlException)
        {
            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static dbo_ReadNewsClass Select_Record(String News_ID)
    {
        dbo_ReadNewsClass clsdbo_ReadNews = new dbo_ReadNewsClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[ReadNewsSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@News_ID", News_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_ReadNews.News_ID = reader["News_ID"] is DBNull ? null : reader["News_ID"].ToString();
                clsdbo_ReadNews.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_ReadNews.Read_Date = reader["Read_Date"] is DBNull ? null : (DateTime?)reader["Read_Date"];
            }
            else
            {
                clsdbo_ReadNews = null;
            }
            reader.Close();
        }
        catch (SqlException)
        {
            return clsdbo_ReadNews;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_ReadNews;
    }

    public static bool Add(dbo_ReadNewsClass clsdbo_ReadNews)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[ReadNewsInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_ReadNews.News_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@News_ID", clsdbo_ReadNews.News_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@News_ID", DBNull.Value);
        }
        if (clsdbo_ReadNews.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_ReadNews.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (clsdbo_ReadNews.Read_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Read_Date", clsdbo_ReadNews.Read_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Read_Date", DBNull.Value);
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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Update(dbo_ReadNewsClass olddbo_ReadNewsClass,
           dbo_ReadNewsClass newdbo_ReadNewsClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[ReadNewsUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_ReadNewsClass.News_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewNews_ID", newdbo_ReadNewsClass.News_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNews_ID", DBNull.Value);
        }
        if (newdbo_ReadNewsClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", newdbo_ReadNewsClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", DBNull.Value);
        }
        if (newdbo_ReadNewsClass.Read_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRead_Date", newdbo_ReadNewsClass.Read_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRead_Date", DBNull.Value);
        }
        if (olddbo_ReadNewsClass.News_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldNews_ID", olddbo_ReadNewsClass.News_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldNews_ID", DBNull.Value);
        }
        if (olddbo_ReadNewsClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldUser_ID", olddbo_ReadNewsClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
        }
        if (olddbo_ReadNewsClass.Read_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldRead_Date", olddbo_ReadNewsClass.Read_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldRead_Date", DBNull.Value);
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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Delete(dbo_ReadNewsClass clsdbo_ReadNews)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[ReadNewsDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_ReadNews.News_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldNews_ID", clsdbo_ReadNews.News_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldNews_ID", DBNull.Value);
        }
        if (clsdbo_ReadNews.User_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", clsdbo_ReadNews.User_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
        }
        if (clsdbo_ReadNews.Read_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldRead_Date", clsdbo_ReadNews.Read_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRead_Date", DBNull.Value);
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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }
}

