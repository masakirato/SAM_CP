using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
public class dbo_DeductDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static List<dbo_DeductClass> GetDeduct()
    {
        List<dbo_DeductClass> item = new List<dbo_DeductClass>();
        item.Add(new dbo_DeductClass() { Clearing_No = "212689CT17051501", Deduct_Amount = 100, Deduct_Detail = "ค่าประกันสินค้า", Deduct_ID = "1" });
        item.Add(new dbo_DeductClass() { Clearing_No = "212689CT17051501", Deduct_Amount = 100, Deduct_Detail = "จ่ายยอดค้างชำระสินค้าเก่า", Deduct_ID = "2" });

        return item;
    }


    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[DeductSelectAll]";
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

    public static List<dbo_DeductClass> Search(String Clearing_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[DeductSearch]";
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

        List<dbo_DeductClass> item = new List<dbo_DeductClass>();
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
                    item.Add(new dbo_DeductClass()
                    {
                        Deduct_ID = reader["Deduct_ID"] is DBNull ? null : reader["Deduct_ID"].ToString(),
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Deduct_Detail = reader["Deduct_Detail"] is DBNull ? null : reader["Deduct_Detail"].ToString(),
                        Deduct_Amount = reader["Deduct_Amount"] is DBNull ? null : (Decimal?)reader["Deduct_Amount"],
                        Account_Code = reader["Account_Code"] is DBNull ? null : reader["Account_Code"].ToString()
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

    public static dbo_DeductClass Select_Record(String Deduct_ID)
    {
        dbo_DeductClass clsdbo_Deduct = new dbo_DeductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[DeductSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Deduct_ID", Deduct_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Deduct.Deduct_ID = reader["Deduct_ID"] is DBNull ? null : reader["Deduct_ID"].ToString();
                clsdbo_Deduct.Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString();
                clsdbo_Deduct.Deduct_Detail = reader["Deduct_Detail"] is DBNull ? null : reader["Deduct_Detail"].ToString();
                clsdbo_Deduct.Deduct_Amount = reader["Deduct_Amount"] is DBNull ? null : (Decimal?)reader["Deduct_Amount"];
            }
            else
            {
                clsdbo_Deduct = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Deduct;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Deduct;
    }

    public static bool Add(dbo_DeductClass clsdbo_Deduct)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[DeductInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Deduct.Deduct_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Deduct_ID", clsdbo_Deduct.Deduct_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Deduct_ID", DBNull.Value);
        }
        if (clsdbo_Deduct.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_Deduct.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (clsdbo_Deduct.Deduct_Detail != null)
        {
            insertCommand.Parameters.AddWithValue("@Deduct_Detail", clsdbo_Deduct.Deduct_Detail);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Deduct_Detail", DBNull.Value);
        }
        if (clsdbo_Deduct.Deduct_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Deduct_Amount", clsdbo_Deduct.Deduct_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Deduct_Amount", DBNull.Value);
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

    public static bool Update(
           dbo_DeductClass newdbo_DeductClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[DeductUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_DeductClass.Deduct_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDeduct_ID", newdbo_DeductClass.Deduct_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDeduct_ID", DBNull.Value);
        }
        if (newdbo_DeductClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", newdbo_DeductClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", DBNull.Value);
        }
        if (newdbo_DeductClass.Deduct_Detail != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDeduct_Detail", newdbo_DeductClass.Deduct_Detail);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDeduct_Detail", DBNull.Value);
        }
        if (newdbo_DeductClass.Deduct_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDeduct_Amount", newdbo_DeductClass.Deduct_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDeduct_Amount", DBNull.Value);
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

    public static bool Delete(String Deduct_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[DeductDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        if (Deduct_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldDeduct_ID", Deduct_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldDeduct_ID", DBNull.Value);
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

