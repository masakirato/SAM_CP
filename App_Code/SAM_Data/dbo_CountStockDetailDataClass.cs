using log4net;
using System;
using System.Data;
using System.Data.SqlClient;

public class dbo_CountStockDetailDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CountStockDetailSelectAll]";
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

    public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CountStockDetailSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Count Stock Detail I D")
        {
            selectCommand.Parameters.AddWithValue("@Count_Stock_Detail_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Count_Stock_Detail_ID", DBNull.Value);
        }
        if (sField == "Count No")
        {
            selectCommand.Parameters.AddWithValue("@Count_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Count_No", DBNull.Value);
        }
        if (sField == "Product I D")
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (sField == "Quantity")
        {
            selectCommand.Parameters.AddWithValue("@Quantity", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Quantity", DBNull.Value);
        }
        if (sField == "Count Quantity")
        {
            selectCommand.Parameters.AddWithValue("@Count_Quantity", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Count_Quantity", DBNull.Value);
        }
        if (sField == "Diff Quantity")
        {
            selectCommand.Parameters.AddWithValue("@Diff_Quantity", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Diff_Quantity", DBNull.Value);
        }
        selectCommand.Parameters.AddWithValue("@SearchCondition", sCondition);
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

    public static dbo_CountStockDetailClass Select_Record(dbo_CountStockDetailClass clsdbo_CountStockDetailPara)
    {
        dbo_CountStockDetailClass clsdbo_CountStockDetail = new dbo_CountStockDetailClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CountStockDetailSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Count_Stock_Detail_ID", clsdbo_CountStockDetailPara.Count_Stock_Detail_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CountStockDetail.Count_Stock_Detail_ID = reader["Count_Stock_Detail_ID"] is DBNull ? null : reader["Count_Stock_Detail_ID"].ToString();
                clsdbo_CountStockDetail.Count_No = reader["Count_No"] is DBNull ? null : reader["Count_No"].ToString();
                clsdbo_CountStockDetail.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_CountStockDetail.Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"];
                clsdbo_CountStockDetail.Count_Quantity = reader["Count_Quantity"] is DBNull ? null : (Int16?)reader["Count_Quantity"];
                clsdbo_CountStockDetail.Diff_Quantity = reader["Diff_Quantity"] is DBNull ? null : (Int16?)reader["Diff_Quantity"];
                clsdbo_CountStockDetail.Remark = reader["Remark"] is DBNull ? null : reader["Remark"].ToString();
            }
            else
            {
                clsdbo_CountStockDetail = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_CountStockDetail;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CountStockDetail;
    }

    public static bool Add(dbo_CountStockDetailClass clsdbo_CountStockDetail, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[CountStockDetailInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CountStockDetail.Count_Stock_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Count_Stock_Detail_ID", clsdbo_CountStockDetail.Count_Stock_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Count_Stock_Detail_ID", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Count_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Count_No", clsdbo_CountStockDetail.Count_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Count_No", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_CountStockDetail.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Quantity", clsdbo_CountStockDetail.Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Quantity", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Count_Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Count_Quantity", clsdbo_CountStockDetail.Count_Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Count_Quantity", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Diff_Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Diff_Quantity", clsdbo_CountStockDetail.Diff_Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Diff_Quantity", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Remark != null)
        {
            insertCommand.Parameters.AddWithValue("@Remark", clsdbo_CountStockDetail.Remark);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Remark", DBNull.Value);
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

    public static bool Update(dbo_CountStockDetailClass olddbo_CountStockDetailClass,
           dbo_CountStockDetailClass newdbo_CountStockDetailClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[CountStockDetailUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CountStockDetailClass.Count_Stock_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCount_Stock_Detail_ID", newdbo_CountStockDetailClass.Count_Stock_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCount_Stock_Detail_ID", DBNull.Value);
        }
        if (newdbo_CountStockDetailClass.Count_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCount_No", newdbo_CountStockDetailClass.Count_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCount_No", DBNull.Value);
        }
        if (newdbo_CountStockDetailClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_CountStockDetailClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        if (newdbo_CountStockDetailClass.Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewQuantity", newdbo_CountStockDetailClass.Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewQuantity", DBNull.Value);
        }
        if (newdbo_CountStockDetailClass.Count_Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCount_Quantity", newdbo_CountStockDetailClass.Count_Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCount_Quantity", DBNull.Value);
        }
        if (newdbo_CountStockDetailClass.Diff_Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDiff_Quantity", newdbo_CountStockDetailClass.Diff_Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDiff_Quantity", DBNull.Value);
        }
        if (newdbo_CountStockDetailClass.Remark != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRemark", newdbo_CountStockDetailClass.Remark);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRemark", DBNull.Value);
        }
        if (olddbo_CountStockDetailClass.Count_Stock_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldCount_Stock_Detail_ID", olddbo_CountStockDetailClass.Count_Stock_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCount_Stock_Detail_ID", DBNull.Value);
        }
        if (olddbo_CountStockDetailClass.Count_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldCount_No", olddbo_CountStockDetailClass.Count_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCount_No", DBNull.Value);
        }
        if (olddbo_CountStockDetailClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldProduct_ID", olddbo_CountStockDetailClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldProduct_ID", DBNull.Value);
        }
        if (olddbo_CountStockDetailClass.Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldQuantity", olddbo_CountStockDetailClass.Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldQuantity", DBNull.Value);
        }
        if (olddbo_CountStockDetailClass.Count_Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldCount_Quantity", olddbo_CountStockDetailClass.Count_Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCount_Quantity", DBNull.Value);
        }
        if (olddbo_CountStockDetailClass.Diff_Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldDiff_Quantity", olddbo_CountStockDetailClass.Diff_Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldDiff_Quantity", DBNull.Value);
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

    public static bool Delete(dbo_CountStockDetailClass clsdbo_CountStockDetail)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[CountStockDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CountStockDetail.Count_Stock_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_Stock_Detail_ID", clsdbo_CountStockDetail.Count_Stock_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_Stock_Detail_ID", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Count_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_No", clsdbo_CountStockDetail.Count_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_No", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Product_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", clsdbo_CountStockDetail.Product_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Quantity.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldQuantity", clsdbo_CountStockDetail.Quantity);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldQuantity", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Count_Quantity.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_Quantity", clsdbo_CountStockDetail.Count_Quantity);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCount_Quantity", DBNull.Value);
        }
        if (clsdbo_CountStockDetail.Diff_Quantity.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldDiff_Quantity", clsdbo_CountStockDetail.Diff_Quantity);
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
    public static bool DeletebyCountNo(string Count_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[CountStockDetailDeletebyCountNo]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (Count_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@Count_No", Count_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@Count_No", DBNull.Value);
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

