using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;

public class dbo_RoleDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RoleSelectAll]";
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

    public static List<dbo_RoleClass> Search(String Role_ID, String Role_Name, String RoleType)
    {

        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RoleSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (Role_ID != null)
        {
            selectCommand.Parameters.AddWithValue("@Role_ID", Role_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Role_ID", DBNull.Value);
        }
        if (Role_Name != null)
        {
            selectCommand.Parameters.AddWithValue("@Role_Name", Role_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Role_Name", DBNull.Value);
        }
        if (RoleType != null)
        {
            selectCommand.Parameters.AddWithValue("@RoleType", RoleType);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@RoleType", DBNull.Value);
        }
        List<dbo_RoleClass> item = new List<dbo_RoleClass>();
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
                    item.Add(new dbo_RoleClass()
                    {
                        Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString(),
                        Role_Name = reader["Role_Name"] is DBNull ? null : reader["Role_Name"].ToString(),
                        Role_Type = reader["RoleType"] is DBNull ? null : reader["RoleType"].ToString()
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

    public static dbo_RoleClass Select_Record(string Role_ID)
    {
        dbo_RoleClass clsdbo_Role = new dbo_RoleClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RoleSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Role_ID", Role_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Role.Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString();
                clsdbo_Role.Role_Name = reader["Role_Name"] is DBNull ? null : reader["Role_Name"].ToString();
            }
            else
            {
                clsdbo_Role = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Role;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Role;
    }

    public static bool Add(dbo_RoleClass clsdbo_Role, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[RoleInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Role.Role_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Role_ID", clsdbo_Role.Role_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Role_ID", DBNull.Value);
        }
        if (clsdbo_Role.Role_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Role_Name", clsdbo_Role.Role_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Role_Name", DBNull.Value);
        }
        if (clsdbo_Role.Role_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@RoleType", clsdbo_Role.Role_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@RoleType", DBNull.Value);
        }
        if (Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
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

    public static bool Update(dbo_RoleClass newdbo_RoleClass, String Last_Modified_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "RoleUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_RoleClass.Role_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRole_ID", newdbo_RoleClass.Role_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRole_ID", DBNull.Value);
        }
        if (newdbo_RoleClass.Role_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRole_Name", newdbo_RoleClass.Role_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRole_Name", DBNull.Value);
        }
        if (newdbo_RoleClass.Role_Type != null)
        {
            updateCommand.Parameters.AddWithValue("@RoleType", newdbo_RoleClass.Role_Type);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@RoleType", DBNull.Value);
        }
        if (Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
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

    public static bool Delete(dbo_RoleClass clsdbo_Role)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[RoleDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Role.Role_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRole_ID", clsdbo_Role.Role_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRole_ID", DBNull.Value);
        }

        //if (clsdbo_Role.Role_Name != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldRole_Name", clsdbo_Role.Role_Name);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldRole_Name", DBNull.Value);
        //}


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

