using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Collections.Generic;

public class dbo_CountStockDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CountStockSelectAll]";
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

    public static List<dbo_CountStockClass> Search(DateTime? Count_Date, String Count_No, String status, String CV_Code)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CountStockSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Count_No))
        {
            selectCommand.Parameters.AddWithValue("@Count_No", Count_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Count_No", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (Count_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Count_Date", Count_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Count_Date", DBNull.Value);
        }

        switch (status)
        {
            case "รอการคอนเฟิร์ม":
                selectCommand.Parameters.AddWithValue("@status", "1");
                break;
            case "คอนเฟิร์มแล้ว":
                selectCommand.Parameters.AddWithValue("@status", "2");
                break;
            case "ยกเลิกการนับ":
                selectCommand.Parameters.AddWithValue("@status", "3");
                break;
            default:
                selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                break;
        }

        List<dbo_CountStockClass> item = new List<dbo_CountStockClass>();
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
                    item.Add(new dbo_CountStockClass()
                    {
                        Count_No = reader["Count_No"] is DBNull ? null : reader["Count_No"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        Count_Date = reader["Count_Date"] is DBNull ? null : (DateTime?)reader["Count_Date"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        //Stock_on_Hand = reader["Stock_on_Hand"] is DBNull ? null : (Int32?)reader["Stock_on_Hand"],
                        //Count_Quantity = reader["Count_Quantity"] is DBNull ? null : (Int32?)reader["Count_Quantity"],
                        //Diff_Quantity = reader["Diff_Quantity"] is DBNull ? null : (Int16?)reader["Diff_Quantity"]
                        Stock_on_Hand = reader["Stock_on_Hand"] is DBNull ? 0 : (Int32?)reader["Stock_on_Hand"],
                        Count_Quantity = reader["Count_Quantity"] is DBNull ? 0 : (Int32?)reader["Count_Quantity"],
                        Diff_Quantity = reader["Diff_Quantity"] is DBNull ? 0 : (Int16?)reader["Diff_Quantity"]
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

    public static dbo_CountStockClass Select_Record(String Count_No)
    {
        dbo_CountStockClass clsdbo_CountStock = new dbo_CountStockClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CountStockSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Count_No", Count_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CountStock.Count_No = reader["Count_No"] is DBNull ? null : reader["Count_No"].ToString();
                clsdbo_CountStock.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_CountStock.Count_Date = reader["Count_Date"] is DBNull ? null : (DateTime?)reader["Count_Date"];
                clsdbo_CountStock.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
                clsdbo_CountStock.Stock_on_Hand = reader["Stock_on_Hand"] is DBNull ? null : (Int32?)reader["Stock_on_Hand"];
                clsdbo_CountStock.Count_Quantity = reader["Count_Quantity"] is DBNull ? null : (Int32?)reader["Count_Quantity"];
                clsdbo_CountStock.Diff_Quantity = reader["Diff_Quantity"] is DBNull ? null : (Int16?)reader["Diff_Quantity"];
            }
            else
            {
                clsdbo_CountStock = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_CountStock;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CountStock;
    }

    public static bool Add(dbo_CountStockClass clsdbo_CountStock, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[CountStockInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CountStock.Count_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Count_No", clsdbo_CountStock.Count_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Count_No", DBNull.Value);
        }
        if (clsdbo_CountStock.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_CountStock.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (clsdbo_CountStock.Count_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Count_Date", clsdbo_CountStock.Count_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Count_Date", DBNull.Value);
        }

        switch (clsdbo_CountStock.Status)
        {
            case "รอการคอนเฟิร์ม":
                insertCommand.Parameters.AddWithValue("@Status", "1");

                break;
            case "คอนเฟิร์มแล้ว":
                insertCommand.Parameters.AddWithValue("@Status", "2");

                break;
            case "ยกเลิกการนับ":
                insertCommand.Parameters.AddWithValue("@Status", "3");

                break;
            default:
                insertCommand.Parameters.AddWithValue("@Status", clsdbo_CountStock.Status);

                break;
        }


        if (clsdbo_CountStock.Stock_on_Hand.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Stock_on_Hand", clsdbo_CountStock.Stock_on_Hand);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Stock_on_Hand", DBNull.Value);
        }
        if (clsdbo_CountStock.Count_Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Count_Quantity", clsdbo_CountStock.Count_Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Count_Quantity", DBNull.Value);
        }
        if (clsdbo_CountStock.Diff_Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Diff_Quantity", clsdbo_CountStock.Diff_Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Diff_Quantity", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Created_By))
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

    public static bool Update(dbo_CountStockClass newdbo_CountStockClass, String Last_Modified_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[CountStockUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CountStockClass.Count_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCount_No", newdbo_CountStockClass.Count_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCount_No", DBNull.Value);
        }


        switch (newdbo_CountStockClass.Status)
        {
            case "รอการคอนเฟิร์ม":

                updateCommand.Parameters.AddWithValue("@NewStatus", "1");
                break;
            case "คอนเฟิร์มแล้ว":

                updateCommand.Parameters.AddWithValue("@NewStatus", "2");
                break;
            case "ยกเลิกการนับ":

                updateCommand.Parameters.AddWithValue("@NewStatus", "3");
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewStatus", DBNull.Value);
                break;
        }


        if (newdbo_CountStockClass.Stock_on_Hand.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStock_on_Hand", newdbo_CountStockClass.Stock_on_Hand);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStock_on_Hand", DBNull.Value);
        }
        if (newdbo_CountStockClass.Count_Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCount_Quantity", newdbo_CountStockClass.Count_Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCount_Quantity", DBNull.Value);
        }
        if (newdbo_CountStockClass.Diff_Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDiff_Quantity", newdbo_CountStockClass.Diff_Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDiff_Quantity", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Last_Modified_By))
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

    public static bool Delete(dbo_CountStockClass clsdbo_CountStock)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[CountStockDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CountStock.Count_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_No", clsdbo_CountStock.Count_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_No", DBNull.Value);
        }
        if (clsdbo_CountStock.CV_Code != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", clsdbo_CountStock.CV_Code);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", DBNull.Value);
        }
        if (clsdbo_CountStock.Count_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_Date", clsdbo_CountStock.Count_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_Date", DBNull.Value);
        }
        if (clsdbo_CountStock.Status != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldStatus", clsdbo_CountStock.Status);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStatus", DBNull.Value);
        }
        if (clsdbo_CountStock.Stock_on_Hand.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_on_Hand", clsdbo_CountStock.Stock_on_Hand);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_on_Hand", DBNull.Value);
        }
        if (clsdbo_CountStock.Count_Quantity.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_Quantity", clsdbo_CountStock.Count_Quantity);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_Quantity", DBNull.Value);
        }
        if (clsdbo_CountStock.Diff_Quantity.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldDiff_Quantity", clsdbo_CountStock.Diff_Quantity);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldDiff_Quantity", DBNull.Value);
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

