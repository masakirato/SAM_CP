using System;
using System.Data;
using System.Data.SqlClient;
using log4net;

public class dbo_ReceiveDetailDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ReceiveDetailSelectAll]";
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
        string selectProcedure = "[dbo].[ReceiveDetailSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Receive Detail I D")
        {
            selectCommand.Parameters.AddWithValue("@Receive_Detail_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Receive_Detail_ID", DBNull.Value);
        }
        if (sField == "Billing Detail I D")
        {
            selectCommand.Parameters.AddWithValue("@Billing_Detail_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_Detail_ID", DBNull.Value);
        }
        if (sField == "Material No")
        {
            selectCommand.Parameters.AddWithValue("@Material_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Material_No", DBNull.Value);
        }
        if (sField == "Qty")
        {
            selectCommand.Parameters.AddWithValue("@Qty", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Qty", DBNull.Value);
        }
        if (sField == "U O M")
        {
            selectCommand.Parameters.AddWithValue("@UOM", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UOM", DBNull.Value);
        }
        if (sField == "Net Value")
        {
            selectCommand.Parameters.AddWithValue("@Net_Value", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Net_Value", DBNull.Value);
        }
        if (sField == "Vat")
        {
            selectCommand.Parameters.AddWithValue("@Vat", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (sField == "Total")
        {
            selectCommand.Parameters.AddWithValue("@Total", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Total", DBNull.Value);
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

    public static dbo_ReceiveDetailClass Select_Record(dbo_ReceiveDetailClass clsdbo_ReceiveDetailPara)
    {
        dbo_ReceiveDetailClass clsdbo_ReceiveDetail = new dbo_ReceiveDetailClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ReceiveDetailSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Billing_Detail_ID", clsdbo_ReceiveDetailPara.Billing_Detail_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_ReceiveDetail.Receive_Detail_ID = reader["Receive_Detail_ID"] is DBNull ? null : reader["Receive_Detail_ID"].ToString();
                clsdbo_ReceiveDetail.Billing_Detail_ID = reader["Billing_Detail_ID"] is DBNull ? null : reader["Billing_Detail_ID"].ToString();
                clsdbo_ReceiveDetail.Material_No = reader["Material_No"] is DBNull ? null : reader["Material_No"].ToString();
                clsdbo_ReceiveDetail.Qty = reader["Qty"] is DBNull ? null : (Int16?)reader["Qty"];
                clsdbo_ReceiveDetail.UOM = reader["UOM"] is DBNull ? null : reader["UOM"].ToString();
                clsdbo_ReceiveDetail.Net_Value = reader["Net_Value"] is DBNull ? null : (Decimal?)reader["Net_Value"];
                clsdbo_ReceiveDetail.Vat = reader["Vat"] is DBNull ? null : (Decimal?)reader["Vat"];
                clsdbo_ReceiveDetail.Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"];
            }
            else
            {
                clsdbo_ReceiveDetail = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_ReceiveDetail;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_ReceiveDetail;
    }

    public static bool Add(dbo_ReceiveDetailClass clsdbo_ReceiveDetail, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "ReceiveDetailInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_ReceiveDetail.Receive_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Receive_Detail_ID", clsdbo_ReceiveDetail.Receive_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Receive_Detail_ID", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Billing_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Billing_Detail_ID", clsdbo_ReceiveDetail.Billing_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Billing_Detail_ID", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Material_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Material_No", clsdbo_ReceiveDetail.Material_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Material_No", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Qty", clsdbo_ReceiveDetail.Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Qty", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.UOM != null)
        {
            insertCommand.Parameters.AddWithValue("@UOM", clsdbo_ReceiveDetail.UOM);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@UOM", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Net_Value.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Net_Value", clsdbo_ReceiveDetail.Net_Value);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Net_Value", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_ReceiveDetail.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total", clsdbo_ReceiveDetail.Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total", DBNull.Value);
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

    public static bool Update(dbo_ReceiveDetailClass olddbo_ReceiveDetailClass,
           dbo_ReceiveDetailClass newdbo_ReceiveDetailClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[ReceiveDetailUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_ReceiveDetailClass.Receive_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewReceive_Detail_ID", newdbo_ReceiveDetailClass.Receive_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewReceive_Detail_ID", DBNull.Value);
        }
        if (newdbo_ReceiveDetailClass.Billing_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_Detail_ID", newdbo_ReceiveDetailClass.Billing_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_Detail_ID", DBNull.Value);
        }
        if (newdbo_ReceiveDetailClass.Material_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewMaterial_No", newdbo_ReceiveDetailClass.Material_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewMaterial_No", DBNull.Value);
        }
        if (newdbo_ReceiveDetailClass.Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewQty", newdbo_ReceiveDetailClass.Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewQty", DBNull.Value);
        }
        if (newdbo_ReceiveDetailClass.UOM != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUOM", newdbo_ReceiveDetailClass.UOM);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUOM", DBNull.Value);
        }
        if (newdbo_ReceiveDetailClass.Net_Value.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Value", newdbo_ReceiveDetailClass.Net_Value);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Value", DBNull.Value);
        }
        if (newdbo_ReceiveDetailClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_ReceiveDetailClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        if (newdbo_ReceiveDetailClass.Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", newdbo_ReceiveDetailClass.Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", DBNull.Value);
        }
        if (olddbo_ReceiveDetailClass.Receive_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldReceive_Detail_ID", olddbo_ReceiveDetailClass.Receive_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldReceive_Detail_ID", DBNull.Value);
        }
        if (olddbo_ReceiveDetailClass.Billing_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldBilling_Detail_ID", olddbo_ReceiveDetailClass.Billing_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldBilling_Detail_ID", DBNull.Value);
        }
        if (olddbo_ReceiveDetailClass.Material_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldMaterial_No", olddbo_ReceiveDetailClass.Material_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldMaterial_No", DBNull.Value);
        }
        if (olddbo_ReceiveDetailClass.Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldQty", olddbo_ReceiveDetailClass.Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldQty", DBNull.Value);
        }
        if (olddbo_ReceiveDetailClass.UOM != null)
        {
            updateCommand.Parameters.AddWithValue("@OldUOM", olddbo_ReceiveDetailClass.UOM);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldUOM", DBNull.Value);
        }
        if (olddbo_ReceiveDetailClass.Net_Value.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldNet_Value", olddbo_ReceiveDetailClass.Net_Value);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldNet_Value", DBNull.Value);
        }
        if (olddbo_ReceiveDetailClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldVat", olddbo_ReceiveDetailClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        }
        if (olddbo_ReceiveDetailClass.Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldTotal", olddbo_ReceiveDetailClass.Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldTotal", DBNull.Value);
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

    public static bool Delete(dbo_ReceiveDetailClass clsdbo_ReceiveDetail)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[ReceiveDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_ReceiveDetail.Receive_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldReceive_Detail_ID", clsdbo_ReceiveDetail.Receive_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldReceive_Detail_ID", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Billing_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_Detail_ID", clsdbo_ReceiveDetail.Billing_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_Detail_ID", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Material_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldMaterial_No", clsdbo_ReceiveDetail.Material_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldMaterial_No", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldQty", clsdbo_ReceiveDetail.Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldQty", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.UOM != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUOM", clsdbo_ReceiveDetail.UOM);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUOM", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Net_Value.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldNet_Value", clsdbo_ReceiveDetail.Net_Value);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldNet_Value", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Vat.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", clsdbo_ReceiveDetail.Vat);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        }
        if (clsdbo_ReceiveDetail.Total.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal", clsdbo_ReceiveDetail.Total);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal", DBNull.Value);
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

