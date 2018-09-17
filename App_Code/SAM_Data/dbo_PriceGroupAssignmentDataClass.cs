using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Collections.Generic;
using System.Web;


public class dbo_PriceGroupAssignmentDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[PriceGroupAssignmentSelectAll]";
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

    public static List<dbo_PriceGroupAssignmentClass> Search(String Price_Group_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[PriceGroupAssignmentSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Price_Group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", Price_Group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }

        List<dbo_PriceGroupAssignmentClass> item = new List<dbo_PriceGroupAssignmentClass>();
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
                    item.Add(new dbo_PriceGroupAssignmentClass()
                    {
                        Assign_To = reader["Assign_To"] is DBNull ? null : reader["Assign_To"].ToString(),
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString()
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

    public static dbo_PriceGroupAssignmentClass Select_Record(dbo_PriceGroupAssignmentClass clsdbo_PriceGroupAssignmentPara)
    {
        dbo_PriceGroupAssignmentClass clsdbo_PriceGroupAssignment = new dbo_PriceGroupAssignmentClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[PriceGroupAssignmentSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Price_Group_ID", clsdbo_PriceGroupAssignmentPara.Price_Group_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_PriceGroupAssignment.Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString();
                clsdbo_PriceGroupAssignment.Assign_To = reader["Assign_To"] is DBNull ? null : reader["Assign_To"].ToString();
            }
            else
            {
                clsdbo_PriceGroupAssignment = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_PriceGroupAssignment;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_PriceGroupAssignment;
    }

    public static dbo_PriceGroupAssignmentClass SelectByAssignTo(dbo_PriceGroupAssignmentClass clsdbo_PriceGroupAssignmentPara)
    {
        dbo_PriceGroupAssignmentClass clsdbo_PriceGroupAssignment = new dbo_PriceGroupAssignmentClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[PriceGroupAssignmentSelectByAssignTo]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@User_ID", clsdbo_PriceGroupAssignmentPara.Assign_To);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_PriceGroupAssignment.Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString();
                clsdbo_PriceGroupAssignment.Assign_To = reader["Assign_To"] is DBNull ? null : reader["Assign_To"].ToString();
            }
            else
            {
                clsdbo_PriceGroupAssignment = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_PriceGroupAssignment;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_PriceGroupAssignment;
    }

    public static bool Add(dbo_PriceGroupAssignmentClass clsdbo_PriceGroupAssignment, Char Price_Group_Type, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[PriceGroupAssignmentInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_PriceGroupAssignment.Price_Group_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", clsdbo_PriceGroupAssignment.Price_Group_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }
        if (clsdbo_PriceGroupAssignment.Assign_To != null)
        {
            insertCommand.Parameters.AddWithValue("@Assign_To", clsdbo_PriceGroupAssignment.Assign_To);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Assign_To", DBNull.Value);
        }
        if (Price_Group_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_Type", Price_Group_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_Type", DBNull.Value);
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

    public static bool Update(dbo_PriceGroupAssignmentClass olddbo_PriceGroupAssignmentClass,
           dbo_PriceGroupAssignmentClass newdbo_PriceGroupAssignmentClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[PriceGroupAssignmentUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_PriceGroupAssignmentClass.Price_Group_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", newdbo_PriceGroupAssignmentClass.Price_Group_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", DBNull.Value);
        }
        if (newdbo_PriceGroupAssignmentClass.Assign_To != null)
        {
            updateCommand.Parameters.AddWithValue("@NewAssign_To", newdbo_PriceGroupAssignmentClass.Assign_To);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewAssign_To", DBNull.Value);
        }
        if (olddbo_PriceGroupAssignmentClass.Price_Group_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldPrice_Group_ID", olddbo_PriceGroupAssignmentClass.Price_Group_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldPrice_Group_ID", DBNull.Value);
        }
        if (olddbo_PriceGroupAssignmentClass.Assign_To != null)
        {
            updateCommand.Parameters.AddWithValue("@OldAssign_To", olddbo_PriceGroupAssignmentClass.Assign_To);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldAssign_To", DBNull.Value);
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

    public static bool Delete(string Assign_To)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[PriceGroupAssignmentDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;


        //if (clsdbo_PriceGroupAssignment.Price_Group_ID != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPrice_Group_ID", clsdbo_PriceGroupAssignment.Price_Group_ID);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPrice_Group_ID", DBNull.Value);
        //}


        if (Assign_To != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldAssign_To", Assign_To);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldAssign_To", DBNull.Value);
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

