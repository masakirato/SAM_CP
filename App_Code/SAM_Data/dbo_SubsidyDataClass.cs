using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class dbo_SubsidyDataClass
{
    public static List<dbo_SubsidyClass> GetSubsidy()
    {
        List<dbo_SubsidyClass> Subsidy = new List<dbo_SubsidyClass>();
        Subsidy.Add(new dbo_SubsidyClass { Clearing_No = "212689CT17051501", Subsidy_Amount = 50, Subsidy_Detail = "ค่าน้ำแข็ง", Subsidy_ID = "1" });
        Subsidy.Add(new dbo_SubsidyClass { Clearing_No = "212689CT17051501", Subsidy_Amount = 100, Subsidy_Detail = "ค่าน้ำมัน", Subsidy_ID = "2" });
        Subsidy.Add(new dbo_SubsidyClass { Clearing_No = "212689CT17051501", Subsidy_Amount = 20, Subsidy_Detail = "ค่าปะยาง", Subsidy_ID = "3" });
        return Subsidy;
    }


    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[SubsidySelectAll]";
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
            return dt;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static List<dbo_SubsidyClass> Search(String Clearing_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[SubsidySearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;




        if (!string.IsNullOrEmpty(Clearing_No))
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }


        List<dbo_SubsidyClass> item = new List<dbo_SubsidyClass>();

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
                    item.Add(new dbo_SubsidyClass()
                    {
                        Subsidy_ID = reader["Subsidy_ID"] is DBNull ? null : reader["Subsidy_ID"].ToString(),
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Subsidy_Detail = reader["Subsidy_Detail"] is DBNull ? null : reader["Subsidy_Detail"].ToString(),
                        Subsidy_Amount = reader["Subsidy_Amount"] is DBNull ? null : (Decimal?)reader["Subsidy_Amount"],
                        Account_Code = reader["Account_Code"] is DBNull ? null : reader["Account_Code"].ToString()
                    });
                }
            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            return item;
        }
        catch (Exception ex)
        {

        }
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static dbo_SubsidyClass Select_Record(String Subsidy_ID)
    {
        dbo_SubsidyClass clsdbo_Subsidy = new dbo_SubsidyClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[SubsidySelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Subsidy_ID", Subsidy_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Subsidy.Subsidy_ID = reader["Subsidy_ID"] is DBNull ? null : reader["Subsidy_ID"].ToString();
                clsdbo_Subsidy.Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString();
                clsdbo_Subsidy.Subsidy_Detail = reader["Subsidy_Detail"] is DBNull ? null : reader["Subsidy_Detail"].ToString();
                clsdbo_Subsidy.Subsidy_Amount = reader["Subsidy_Amount"] is DBNull ? null : (Decimal?)reader["Subsidy_Amount"];
            }
            else
            {
                clsdbo_Subsidy = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            return clsdbo_Subsidy;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Subsidy;
    }

    public static bool Add(dbo_SubsidyClass clsdbo_Subsidy)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[SubsidyInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Subsidy.Subsidy_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Subsidy_ID", clsdbo_Subsidy.Subsidy_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Subsidy_ID", DBNull.Value);
        }
        if (clsdbo_Subsidy.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_Subsidy.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (clsdbo_Subsidy.Subsidy_Detail != null)
        {
            insertCommand.Parameters.AddWithValue("@Subsidy_Detail", clsdbo_Subsidy.Subsidy_Detail);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Subsidy_Detail", DBNull.Value);
        }
        if (clsdbo_Subsidy.Subsidy_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Subsidy_Amount", clsdbo_Subsidy.Subsidy_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Subsidy_Amount", DBNull.Value);
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

    public static bool Update(
           dbo_SubsidyClass newdbo_SubsidyClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[SubsidyUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_SubsidyClass.Subsidy_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSubsidy_ID", newdbo_SubsidyClass.Subsidy_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSubsidy_ID", DBNull.Value);
        }
        if (newdbo_SubsidyClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", newdbo_SubsidyClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", DBNull.Value);
        }
        if (newdbo_SubsidyClass.Subsidy_Detail != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSubsidy_Detail", newdbo_SubsidyClass.Subsidy_Detail);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSubsidy_Detail", DBNull.Value);
        }
        if (newdbo_SubsidyClass.Subsidy_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSubsidy_Amount", newdbo_SubsidyClass.Subsidy_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSubsidy_Amount", DBNull.Value);
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

    public static bool Delete(String Subsidy_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[SubsidyDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (Subsidy_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSubsidy_ID", Subsidy_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSubsidy_ID", DBNull.Value);
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
}

