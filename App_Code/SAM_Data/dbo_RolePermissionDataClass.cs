using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_RolePermissionDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static List<MenuNode> GetMenuNode(int Parent_MenuID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetMenuNode";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Parent_MenuID", Parent_MenuID);

        List<MenuNode> item = new List<MenuNode>();
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);

                foreach (DataRow row in dt.Rows)
                {
                    item.Add(new MenuNode()
                    {
                        Function_ID = row["Function_ID"].ToString(),
                        Function_Name = row["Function_Name"].ToString(),
                        Parent_MenuID = int.Parse(row["Parent_MenuID"].ToString()),
                        MenuID = int.Parse(row["MenuID"].ToString())
                    });
                }

            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return null;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }


    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RolePermissionSelectAll]";
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


    public static dbo_RolePermissionClass GetRolePermissionByRole_IDAndFunctionName(string Role_ID, string Function_Name)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetRolePermissionByRole_IDAndFunctionName";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Role_ID))
        {
            selectCommand.Parameters.AddWithValue("@Role_ID", Role_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Role_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Function_Name))
        {
            selectCommand.Parameters.AddWithValue("@Function_Name", Function_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Function_Name", DBNull.Value);
        }

        dbo_RolePermissionClass clsdbo_RolePermission = new dbo_RolePermissionClass();

        List<dbo_RolePermissionClass> item = new List<dbo_RolePermissionClass>();

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_RolePermission.Role_Permission_ID = reader["Role_Permission_ID"] is DBNull ? null : reader["Role_Permission_ID"].ToString();
                clsdbo_RolePermission.Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString();
                clsdbo_RolePermission.Function_Name = reader["Function_Name"] is DBNull ? null : reader["Function_Name"].ToString();
            }
            else
            {
                clsdbo_RolePermission = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_RolePermission;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_RolePermission;
    }




    public static List<dbo_RolePermissionClass> Search(string Role_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RolePermissionSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!string.IsNullOrEmpty(Role_ID))
        {
            selectCommand.Parameters.AddWithValue("@Role_ID", Role_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Role_ID", DBNull.Value);
        }



        List<dbo_RolePermissionClass> item = new List<dbo_RolePermissionClass>();

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
                    item.Add(new dbo_RolePermissionClass()
                    {
                        Role_Permission_ID = reader["Role_Permission_ID"] is DBNull ? null : reader["Role_Permission_ID"].ToString()
                        ,
                        Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString()
                        ,
                        Function_Name = reader["Function_Name"] is DBNull ? null : reader["Function_Name"].ToString()
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

    public static dbo_RolePermissionClass Select_Record(dbo_RolePermissionClass clsdbo_RolePermissionPara)
    {
        dbo_RolePermissionClass clsdbo_RolePermission = new dbo_RolePermissionClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RolePermissionSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Role_Permission_ID", clsdbo_RolePermissionPara.Role_Permission_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_RolePermission.Role_Permission_ID = reader["Role_Permission_ID"] is DBNull ? null : reader["Role_Permission_ID"].ToString();
                clsdbo_RolePermission.Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString();
                clsdbo_RolePermission.Function_Name = reader["Function_Name"] is DBNull ? null : reader["Function_Name"].ToString();
            }
            else
            {
                clsdbo_RolePermission = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_RolePermission;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_RolePermission;
    }

    public static bool Add(dbo_RolePermissionClass clsdbo_RolePermission, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[RolePermissionInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;

        if (clsdbo_RolePermission.Role_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Role_ID", clsdbo_RolePermission.Role_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Role_ID", DBNull.Value);
        }
        
        
        if (clsdbo_RolePermission.Function_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Function_Name", clsdbo_RolePermission.Function_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Function_Name", DBNull.Value);
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

    public static bool Update(dbo_RolePermissionClass olddbo_RolePermissionClass,
           dbo_RolePermissionClass newdbo_RolePermissionClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[RolePermissionUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_RolePermissionClass.Role_Permission_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRole_Permission_ID", newdbo_RolePermissionClass.Role_Permission_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRole_Permission_ID", DBNull.Value);
        }
        if (newdbo_RolePermissionClass.Role_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRole_ID", newdbo_RolePermissionClass.Role_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRole_ID", DBNull.Value);
        }
        if (newdbo_RolePermissionClass.Function_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewFunction_Name", newdbo_RolePermissionClass.Function_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewFunction_Name", DBNull.Value);
        }
        if (olddbo_RolePermissionClass.Role_Permission_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldRole_Permission_ID", olddbo_RolePermissionClass.Role_Permission_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldRole_Permission_ID", DBNull.Value);
        }
        if (olddbo_RolePermissionClass.Role_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldRole_ID", olddbo_RolePermissionClass.Role_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldRole_ID", DBNull.Value);
        }
        if (olddbo_RolePermissionClass.Function_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@OldFunction_Name", olddbo_RolePermissionClass.Function_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldFunction_Name", DBNull.Value);
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

    public static bool Delete(dbo_RolePermissionClass clsdbo_RolePermission)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[RolePermissionDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        if (clsdbo_RolePermission.Role_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRole_ID", clsdbo_RolePermission.Role_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRole_ID", DBNull.Value);
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

