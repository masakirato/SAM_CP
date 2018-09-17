using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
public class dbo_CycleAssignmentDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static Dictionary<string, string> GetAssignCycle(string Order_Cycle_ID)
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetAssignCycle]";

        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!string.IsNullOrEmpty(Order_Cycle_ID))
        {
            selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", Order_Cycle_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", DBNull.Value);
        }

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
                    unit.Add(row["CV_Code"].ToString(), row["Prefix_ID"].ToString());
                }
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return unit;
        }
        finally
        {
            connection.Close();
        }
        return unit;

    }





    public static List<dbo_CycleAssignmentClass> SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CycleAssignmentSelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        List<dbo_CycleAssignmentClass> item = new List<dbo_CycleAssignmentClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_CycleAssignmentClass()
                    {
                        Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString()
                        //,
                        // AgentName = reader["AgentName"] is DBNull ? null : reader["AgentName"].ToString()
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

    public static List<dbo_CycleAssignmentClass> Search(String Order_Cycle_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "CycleAssignmentSearch";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (!string.IsNullOrEmpty(Order_Cycle_ID))
        {
            selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", Order_Cycle_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", DBNull.Value);
        }

        List<dbo_CycleAssignmentClass> item = new List<dbo_CycleAssignmentClass>();

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
                    item.Add(new dbo_CycleAssignmentClass()
                    {
                        Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        AgentName = reader["AgentName"] is DBNull ? null : reader["AgentName"].ToString()
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

    public static dbo_CycleAssignmentClass Select_Record(String Order_Cycle_ID, String CV_Code)
    {
        dbo_CycleAssignmentClass clsdbo_CycleAssignment = new dbo_CycleAssignmentClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "CycleAssignmentSelect";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", Order_Cycle_ID);
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CycleAssignment.Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString();
                clsdbo_CycleAssignment.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_CycleAssignment.AgentName = reader["AgentName"] is DBNull ? null : reader["AgentName"].ToString();
            }
            else
            {
                clsdbo_CycleAssignment = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_CycleAssignment;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CycleAssignment;
    }

    public static bool Add(dbo_CycleAssignmentClass clsdbo_CycleAssignment,String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "CycleAssignmentInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CycleAssignment.Order_Cycle_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_ID", clsdbo_CycleAssignment.Order_Cycle_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_ID", DBNull.Value);
        }
        if (clsdbo_CycleAssignment.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_CycleAssignment.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
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

    public static bool Update(dbo_CycleAssignmentClass newdbo_CycleAssignmentClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "CycleAssignmentUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CycleAssignmentClass.Order_Cycle_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_ID", newdbo_CycleAssignmentClass.Order_Cycle_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_ID", DBNull.Value);
        }
        if (newdbo_CycleAssignmentClass.CV_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", newdbo_CycleAssignmentClass.CV_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", DBNull.Value);
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

    public static bool Delete(String CV_Code)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[CycleAssignmentDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        
        if (CV_Code != null)
        {
            deleteCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
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

