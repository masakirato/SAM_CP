using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_NewsDataClass2
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static List<dbo_NewsClass2> Search(String News_Type, String Agent_Name)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[NewsSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(News_Type))
        {
            selectCommand.Parameters.AddWithValue("@News_Type", News_Type);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@News_Type", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Agent_Name))
        {
            selectCommand.Parameters.AddWithValue("@Agent_Name", Agent_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Agent_Name", DBNull.Value);
        }


        List<dbo_NewsClass2> item = new List<dbo_NewsClass2>();
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
                    item.Add(new dbo_NewsClass2()
                    {
                        News_ID = reader["News_ID"] is DBNull ? null : reader["News_ID"].ToString(),
                        News_Type = reader["News_Type"] is DBNull ? null : reader["News_Type"].ToString(),
                        Agent_Name = reader["Agent_Name"] is DBNull ? null : reader["Agent_Name"].ToString(),
                        Subject = reader["Subject"] is DBNull ? null : reader["Subject"].ToString(),
                        Content = reader["Content"] is DBNull ? null : reader["Content"].ToString(),
                        Content_FileName = reader["Content_FileName"] is DBNull ? null : reader["Content_FileName"].ToString(),
                        Content_FileType = reader["Content_FileType"] is DBNull ? null : reader["Content_FileType"].ToString(),
                        VDO_Hyperlink = reader["VDO_Hyperlink"] is DBNull ? null : reader["VDO_Hyperlink"].ToString(),
                        Start_Date = reader["Start_Date"] is DBNull ? null : (DateTime?)reader["Start_Date"],
                        End_Date = reader["End_Date"] is DBNull ? null : (DateTime?)reader["End_Date"],
                        Photo_Name = reader["Photo_Name"] is DBNull ? null : reader["Photo_Name"].ToString(),
                        Photo_MemoryStream = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"]
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static dbo_NewsClass2 Select_Record(String News_ID)
    {
        dbo_NewsClass2 clsdbo_News = new dbo_NewsClass2();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[NewsSelect]";
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
                clsdbo_News.News_ID = reader["News_ID"] is DBNull ? null : reader["News_ID"].ToString();
                clsdbo_News.News_Type = reader["News_Type"] is DBNull ? null : reader["News_Type"].ToString();
                clsdbo_News.Agent_Name = reader["Agent_Name"] is DBNull ? null : reader["Agent_Name"].ToString();
                clsdbo_News.Subject = reader["Subject"] is DBNull ? null : reader["Subject"].ToString();
                clsdbo_News.Content = reader["Content"] is DBNull ? null : reader["Content"].ToString();
                clsdbo_News.Content_FileName = reader["Content_FileName"] is DBNull ? null : reader["Content_FileName"].ToString();
                clsdbo_News.Content_FileType = reader["Content_FileType"] is DBNull ? null : reader["Content_FileType"].ToString();
                clsdbo_News.VDO_Hyperlink = reader["VDO_Hyperlink"] is DBNull ? null : reader["VDO_Hyperlink"].ToString();
                clsdbo_News.Start_Date = reader["Start_Date"] is DBNull ? null : (DateTime?)reader["Start_Date"];
                clsdbo_News.End_Date = reader["End_Date"] is DBNull ? null : (DateTime?)reader["End_Date"];
                clsdbo_News.Photo_Name = reader["Photo_Name"] is DBNull ? null : reader["Photo_Name"].ToString();
                clsdbo_News.Photo_MemoryStream = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"];
                clsdbo_News.Content_File = reader["Content_File"] is DBNull ? null : (Byte[])reader["Content_File"];

            }
            else
            {
                clsdbo_News = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_News;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_News;
    }



    /*
    public static bool Add(dbo_NewsClass clsdbo_News)
    {
        SqlConnection connection = SAM_DBDataClass.GetConnection();
        string insertProcedure = "[dbo].[NewsInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_News.News_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@News_ID", clsdbo_News.News_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@News_ID", DBNull.Value);
        }
        if (clsdbo_News.News_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@News_Type", clsdbo_News.News_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@News_Type", DBNull.Value);
        }
        if (clsdbo_News.Agent_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Agent_Name", clsdbo_News.Agent_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Agent_Name", DBNull.Value);
        }
        if (clsdbo_News.Subject != null)
        {
            insertCommand.Parameters.AddWithValue("@Subject", clsdbo_News.Subject);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Subject", DBNull.Value);
        }
        if (clsdbo_News.Content != null)
        {
            insertCommand.Parameters.AddWithValue("@Content", clsdbo_News.Content);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Content", DBNull.Value);
        }
        if (clsdbo_News.Content_FileName != null)
        {
            insertCommand.Parameters.AddWithValue("@Content_FileName", clsdbo_News.Content_FileName);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Content_FileName", DBNull.Value);
        }
        if (clsdbo_News.Content_FileType != null)
        {
            insertCommand.Parameters.AddWithValue("@Content_FileType", clsdbo_News.Content_FileType);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Content_FileType", DBNull.Value);
        }
        if (clsdbo_News.VDO_Hyperlink != null)
        {
            insertCommand.Parameters.AddWithValue("@VDO_Hyperlink", clsdbo_News.VDO_Hyperlink);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@VDO_Hyperlink", DBNull.Value);
        }
        if (clsdbo_News.Start_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Start_Date", clsdbo_News.Start_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Start_Date", DBNull.Value);
        }
        if (clsdbo_News.End_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@End_Date", clsdbo_News.End_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@End_Date", DBNull.Value);
        }
        if (clsdbo_News.Photo_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Photo_Name", clsdbo_News.Photo_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Photo_Name", DBNull.Value);
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
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Update(dbo_NewsClass olddbo_NewsClass,
           dbo_NewsClass newdbo_NewsClass)
    {
        SqlConnection connection = SAM_DBDataClass.GetConnection();
        string updateProcedure = "[dbo].[NewsUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_NewsClass.News_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewNews_ID", newdbo_NewsClass.News_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNews_ID", DBNull.Value);
        }
        if (newdbo_NewsClass.News_Type != null)
        {
            updateCommand.Parameters.AddWithValue("@NewNews_Type", newdbo_NewsClass.News_Type);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNews_Type", DBNull.Value);
        }
        if (newdbo_NewsClass.Agent_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewAgent_Name", newdbo_NewsClass.Agent_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewAgent_Name", DBNull.Value);
        }
        if (newdbo_NewsClass.Subject != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSubject", newdbo_NewsClass.Subject);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSubject", DBNull.Value);
        }
        if (newdbo_NewsClass.Content != null)
        {
            updateCommand.Parameters.AddWithValue("@NewContent", newdbo_NewsClass.Content);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewContent", DBNull.Value);
        }
        if (newdbo_NewsClass.Content_FileName != null)
        {
            updateCommand.Parameters.AddWithValue("@NewContent_FileName", newdbo_NewsClass.Content_FileName);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewContent_FileName", DBNull.Value);
        }
        if (newdbo_NewsClass.Content_FileType != null)
        {
            updateCommand.Parameters.AddWithValue("@NewContent_FileType", newdbo_NewsClass.Content_FileType);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewContent_FileType", DBNull.Value);
        }
        if (newdbo_NewsClass.VDO_Hyperlink != null)
        {
            updateCommand.Parameters.AddWithValue("@NewVDO_Hyperlink", newdbo_NewsClass.VDO_Hyperlink);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVDO_Hyperlink", DBNull.Value);
        }
        if (newdbo_NewsClass.Start_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStart_Date", newdbo_NewsClass.Start_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStart_Date", DBNull.Value);
        }
        if (newdbo_NewsClass.End_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewEnd_Date", newdbo_NewsClass.End_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewEnd_Date", DBNull.Value);
        }
        if (newdbo_NewsClass.Photo_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPhoto_Name", newdbo_NewsClass.Photo_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPhoto_Name", DBNull.Value);
        }
        if (olddbo_NewsClass.News_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldNews_ID", olddbo_NewsClass.News_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldNews_ID", DBNull.Value);
        }
        if (olddbo_NewsClass.News_Type != null)
        {
            updateCommand.Parameters.AddWithValue("@OldNews_Type", olddbo_NewsClass.News_Type);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldNews_Type", DBNull.Value);
        }
        if (olddbo_NewsClass.Agent_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@OldAgent_Name", olddbo_NewsClass.Agent_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldAgent_Name", DBNull.Value);
        }
        if (olddbo_NewsClass.Subject != null)
        {
            updateCommand.Parameters.AddWithValue("@OldSubject", olddbo_NewsClass.Subject);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldSubject", DBNull.Value);
        }
        if (olddbo_NewsClass.Content != null)
        {
            updateCommand.Parameters.AddWithValue("@OldContent", olddbo_NewsClass.Content);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldContent", DBNull.Value);
        }
        if (olddbo_NewsClass.Content_FileName != null)
        {
            updateCommand.Parameters.AddWithValue("@OldContent_FileName", olddbo_NewsClass.Content_FileName);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldContent_FileName", DBNull.Value);
        }
        if (olddbo_NewsClass.Content_FileType != null)
        {
            updateCommand.Parameters.AddWithValue("@OldContent_FileType", olddbo_NewsClass.Content_FileType);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldContent_FileType", DBNull.Value);
        }
        if (olddbo_NewsClass.VDO_Hyperlink != null)
        {
            updateCommand.Parameters.AddWithValue("@OldVDO_Hyperlink", olddbo_NewsClass.VDO_Hyperlink);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldVDO_Hyperlink", DBNull.Value);
        }
        if (olddbo_NewsClass.Start_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldStart_Date", olddbo_NewsClass.Start_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldStart_Date", DBNull.Value);
        }
        if (olddbo_NewsClass.End_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldEnd_Date", olddbo_NewsClass.End_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldEnd_Date", DBNull.Value);
        }
        if (olddbo_NewsClass.Photo_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@OldPhoto_Name", olddbo_NewsClass.Photo_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldPhoto_Name", DBNull.Value);
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
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Delete(dbo_NewsClass clsdbo_News)
    {
        SqlConnection connection = SAM_DBDataClass.GetConnection();
        string deleteProcedure = "[dbo].[NewsDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_News.News_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldNews_ID", clsdbo_News.News_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldNews_ID", DBNull.Value);
        }
        if (clsdbo_News.News_Type != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldNews_Type", clsdbo_News.News_Type);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldNews_Type", DBNull.Value);
        }
        if (clsdbo_News.Agent_Name != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldAgent_Name", clsdbo_News.Agent_Name);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldAgent_Name", DBNull.Value);
        }
        if (clsdbo_News.Subject != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSubject", clsdbo_News.Subject);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSubject", DBNull.Value);
        }
        if (clsdbo_News.Content != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldContent", clsdbo_News.Content);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldContent", DBNull.Value);
        }
        if (clsdbo_News.Content_FileName != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldContent_FileName", clsdbo_News.Content_FileName);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldContent_FileName", DBNull.Value);
        }
        if (clsdbo_News.Content_FileType != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldContent_FileType", clsdbo_News.Content_FileType);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldContent_FileType", DBNull.Value);
        }
        if (clsdbo_News.VDO_Hyperlink != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldVDO_Hyperlink", clsdbo_News.VDO_Hyperlink);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldVDO_Hyperlink", DBNull.Value);
        }
        if (clsdbo_News.Start_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldStart_Date", clsdbo_News.Start_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStart_Date", DBNull.Value);
        }
        if (clsdbo_News.End_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldEnd_Date", clsdbo_News.End_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldEnd_Date", DBNull.Value);
        }
        if (clsdbo_News.Photo_Name != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldPhoto_Name", clsdbo_News.Photo_Name);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPhoto_Name", DBNull.Value);
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
            return false;
        }
        finally
        {
            connection.Close();
        }
    }
    */



}

