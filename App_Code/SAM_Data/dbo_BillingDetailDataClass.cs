using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
public class dbo_BillingDetailDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[BillingDetailSelectAll]";
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
        string selectProcedure = "[dbo].[BillingDetailSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Billing Detail I D")
        {
            selectCommand.Parameters.AddWithValue("@Billing_Detail_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_Detail_ID", DBNull.Value);
        }
        if (sField == "Billing I D")
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
        }
        if (sField == "Item Position")
        {
            selectCommand.Parameters.AddWithValue("@Item_Position", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Item_Position", DBNull.Value);
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

    public static dbo_BillingDetailClass Select_Record(String Billing_Detail_ID)
    {
        dbo_BillingDetailClass clsdbo_BillingDetail = new dbo_BillingDetailClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[BillingDetailSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Billing_Detail_ID", Billing_Detail_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_BillingDetail.Billing_Detail_ID = reader["Billing_Detail_ID"] is DBNull ? null : reader["Billing_Detail_ID"].ToString();
                clsdbo_BillingDetail.Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString();
                clsdbo_BillingDetail.Item_Position = reader["Item_Position"] is DBNull ? null : reader["Item_Position"].ToString();
                clsdbo_BillingDetail.Material_No = reader["Material_No"] is DBNull ? null : reader["Material_No"].ToString();
                clsdbo_BillingDetail.Qty = reader["Qty"] is DBNull ? null : (Int16?)reader["Qty"];
                clsdbo_BillingDetail.UOM = reader["UOM"] is DBNull ? null : reader["UOM"].ToString();
                clsdbo_BillingDetail.Net_Value = reader["Net_Value"] is DBNull ? null : (Decimal?)reader["Net_Value"];
                clsdbo_BillingDetail.Vat = reader["Vat"] is DBNull ? null : (Decimal?)reader["Vat"];
                clsdbo_BillingDetail.Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"];
            }
            else
            {
                clsdbo_BillingDetail = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_BillingDetail;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_BillingDetail;
    }

    public static bool Add(dbo_BillingDetailClass clsdbo_BillingDetail)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "BillingDetailInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_BillingDetail.Billing_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Billing_Detail_ID", clsdbo_BillingDetail.Billing_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Billing_Detail_ID", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Billing_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Billing_ID", clsdbo_BillingDetail.Billing_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Item_Position != null)
        {
            insertCommand.Parameters.AddWithValue("@Item_Position", clsdbo_BillingDetail.Item_Position);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Item_Position", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Material_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Material_No", clsdbo_BillingDetail.Material_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Material_No", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Qty", clsdbo_BillingDetail.Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Qty", DBNull.Value);
        }
        if (clsdbo_BillingDetail.UOM != null)
        {
            insertCommand.Parameters.AddWithValue("@UOM", clsdbo_BillingDetail.UOM);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@UOM", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Net_Value.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Net_Value", clsdbo_BillingDetail.Net_Value);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Net_Value", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_BillingDetail.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total", clsdbo_BillingDetail.Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total", DBNull.Value);
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Update(dbo_BillingDetailClass olddbo_BillingDetailClass,
           dbo_BillingDetailClass newdbo_BillingDetailClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[BillingDetailUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_BillingDetailClass.Billing_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_Detail_ID", newdbo_BillingDetailClass.Billing_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_Detail_ID", DBNull.Value);
        }
        if (newdbo_BillingDetailClass.Billing_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_ID", newdbo_BillingDetailClass.Billing_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_ID", DBNull.Value);
        }
        if (newdbo_BillingDetailClass.Item_Position != null)
        {
            updateCommand.Parameters.AddWithValue("@NewItem_Position", newdbo_BillingDetailClass.Item_Position);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewItem_Position", DBNull.Value);
        }
        if (newdbo_BillingDetailClass.Material_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewMaterial_No", newdbo_BillingDetailClass.Material_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewMaterial_No", DBNull.Value);
        }
        if (newdbo_BillingDetailClass.Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewQty", newdbo_BillingDetailClass.Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewQty", DBNull.Value);
        }
        if (newdbo_BillingDetailClass.UOM != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUOM", newdbo_BillingDetailClass.UOM);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUOM", DBNull.Value);
        }
        if (newdbo_BillingDetailClass.Net_Value.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Value", newdbo_BillingDetailClass.Net_Value);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Value", DBNull.Value);
        }
        if (newdbo_BillingDetailClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_BillingDetailClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        if (newdbo_BillingDetailClass.Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", newdbo_BillingDetailClass.Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.Billing_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldBilling_Detail_ID", olddbo_BillingDetailClass.Billing_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldBilling_Detail_ID", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.Billing_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldBilling_ID", olddbo_BillingDetailClass.Billing_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldBilling_ID", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.Item_Position != null)
        {
            updateCommand.Parameters.AddWithValue("@OldItem_Position", olddbo_BillingDetailClass.Item_Position);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldItem_Position", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.Material_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldMaterial_No", olddbo_BillingDetailClass.Material_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldMaterial_No", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldQty", olddbo_BillingDetailClass.Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldQty", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.UOM != null)
        {
            updateCommand.Parameters.AddWithValue("@OldUOM", olddbo_BillingDetailClass.UOM);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldUOM", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.Net_Value.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldNet_Value", olddbo_BillingDetailClass.Net_Value);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldNet_Value", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldVat", olddbo_BillingDetailClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        }
        if (olddbo_BillingDetailClass.Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldTotal", olddbo_BillingDetailClass.Total);
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

    public static bool Delete(dbo_BillingDetailClass clsdbo_BillingDetail)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[BillingDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_BillingDetail.Billing_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_Detail_ID", clsdbo_BillingDetail.Billing_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_Detail_ID", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Billing_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_ID", clsdbo_BillingDetail.Billing_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_ID", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Item_Position != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldItem_Position", clsdbo_BillingDetail.Item_Position);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldItem_Position", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Material_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldMaterial_No", clsdbo_BillingDetail.Material_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldMaterial_No", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldQty", clsdbo_BillingDetail.Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldQty", DBNull.Value);
        }
        if (clsdbo_BillingDetail.UOM != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUOM", clsdbo_BillingDetail.UOM);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUOM", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Net_Value.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldNet_Value", clsdbo_BillingDetail.Net_Value);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldNet_Value", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Vat.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", clsdbo_BillingDetail.Vat);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        }
        if (clsdbo_BillingDetail.Total.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal", clsdbo_BillingDetail.Total);
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

