using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
public class dbo_ReceiveDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    [Obsolete]
    public static DataTable _GetReceiving()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Billing_ID");
        dt.Columns.Add("Invoice_Date");
        dt.Columns.Add("Invoice_No");
        dt.Columns.Add("Receiving_Date");
        dt.Columns.Add("Invoice_Net_Value");
        dt.Columns.Add("Invoice_VAT");
        dt.Columns.Add("Invoice_Total");
        dt.Columns.Add("Order_Status");
        dt.Columns.Add("Invoice_Status");

        dt.Rows.Add("B-000001", DateTime.Now, "PO-0003", DateTime.Now, CommonDataClass.Invoice_Net_Value, "7.00", CommonDataClass.Invoice_Total, "ซีพี-เมจิ รับข้อมูลแล้ว", string.Empty);


        return dt;
    }

    [Obsolete]
    public static DataTable GetReceiving(string Invoice_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[GetReceiving]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);

        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Invoice_No))
        {
            selectCommand.Parameters.AddWithValue("@Invoice_No", Invoice_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_No", DBNull.Value);
        }


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

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ReceiveSelectAll]";
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

    [Obsolete]
    public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ReceiveSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Receive I D")
        {
            selectCommand.Parameters.AddWithValue("@Receive_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Receive_ID", DBNull.Value);
        }
        if (sField == "Billing I D")
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
        }
        if (sField == "Invoice No")
        {
            selectCommand.Parameters.AddWithValue("@Invoice_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_No", DBNull.Value);
        }
        if (sField == "Invoice Date")
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Date", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Date", DBNull.Value);
        }
        if (sField == "Invoice Net Value")
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Net_Value", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Net_Value", DBNull.Value);
        }
        if (sField == "Invoice V A T")
        {
            selectCommand.Parameters.AddWithValue("@Invoice_VAT", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_VAT", DBNull.Value);
        }
        if (sField == "Invoice Total")
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Total", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Total", DBNull.Value);
        }
        if (sField == "Ref Invoice No")
        {
            selectCommand.Parameters.AddWithValue("@Ref_Invoice_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Ref_Invoice_No", DBNull.Value);
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

    public static dbo_ReceiveClass Select_Record(dbo_ReceiveClass clsdbo_ReceivePara)
    {
        dbo_ReceiveClass clsdbo_Receive = new dbo_ReceiveClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ReceiveSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Invoice_No", clsdbo_ReceivePara.Invoice_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Receive.Receive_ID = reader["Receive_ID"] is DBNull ? null : reader["Receive_ID"].ToString();
                clsdbo_Receive.Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString();
                clsdbo_Receive.Invoice_No = reader["Invoice_No"] is DBNull ? null : reader["Invoice_No"].ToString();
                clsdbo_Receive.Invoice_Date = reader["Invoice_Date"] is DBNull ? null : (DateTime?)reader["Invoice_Date"];
                clsdbo_Receive.Invoice_Net_Value = reader["Invoice_Net_Value"] is DBNull ? null : (Decimal?)reader["Invoice_Net_Value"];
                clsdbo_Receive.Invoice_VAT = reader["Invoice_VAT"] is DBNull ? null : (Decimal?)reader["Invoice_VAT"];
                clsdbo_Receive.Invoice_Total = reader["Invoice_Total"] is DBNull ? null : (Decimal?)reader["Invoice_Total"];
                clsdbo_Receive.Ref_Invoice_No = reader["Ref_Invoice_No"] is DBNull ? null : reader["Ref_Invoice_No"].ToString();
            }
            else
            {
                clsdbo_Receive = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Receive;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Receive;
    }

    public static bool Add(dbo_ReceiveClass clsdbo_Receive, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[ReceiveInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Receive.Receive_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Receive_ID", clsdbo_Receive.Receive_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Receive_ID", DBNull.Value);
        }
        if (clsdbo_Receive.Billing_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Billing_ID", clsdbo_Receive.Billing_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_No", clsdbo_Receive.Invoice_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_No", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Date", clsdbo_Receive.Invoice_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Date", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_Net_Value.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Net_Value", clsdbo_Receive.Invoice_Net_Value);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Net_Value", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_VAT.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_VAT", clsdbo_Receive.Invoice_VAT);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_VAT", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Total", clsdbo_Receive.Invoice_Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Total", DBNull.Value);
        }
        if (clsdbo_Receive.Ref_Invoice_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Ref_Invoice_No", clsdbo_Receive.Ref_Invoice_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Ref_Invoice_No", DBNull.Value);
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

    public static bool Update(dbo_ReceiveClass olddbo_ReceiveClass,
           dbo_ReceiveClass newdbo_ReceiveClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[ReceiveUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_ReceiveClass.Receive_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewReceive_ID", newdbo_ReceiveClass.Receive_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewReceive_ID", DBNull.Value);
        }
        if (newdbo_ReceiveClass.Billing_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_ID", newdbo_ReceiveClass.Billing_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_ID", DBNull.Value);
        }
        if (newdbo_ReceiveClass.Invoice_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_No", newdbo_ReceiveClass.Invoice_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_No", DBNull.Value);
        }
        if (newdbo_ReceiveClass.Invoice_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Date", newdbo_ReceiveClass.Invoice_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Date", DBNull.Value);
        }
        if (newdbo_ReceiveClass.Invoice_Net_Value.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Net_Value", newdbo_ReceiveClass.Invoice_Net_Value);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Net_Value", DBNull.Value);
        }
        if (newdbo_ReceiveClass.Invoice_VAT.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_VAT", newdbo_ReceiveClass.Invoice_VAT);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_VAT", DBNull.Value);
        }
        if (newdbo_ReceiveClass.Invoice_Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Total", newdbo_ReceiveClass.Invoice_Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Total", DBNull.Value);
        }
        if (newdbo_ReceiveClass.Ref_Invoice_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRef_Invoice_No", newdbo_ReceiveClass.Ref_Invoice_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRef_Invoice_No", DBNull.Value);
        }
        if (olddbo_ReceiveClass.Receive_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldReceive_ID", olddbo_ReceiveClass.Receive_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldReceive_ID", DBNull.Value);
        }
        if (olddbo_ReceiveClass.Billing_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldBilling_ID", olddbo_ReceiveClass.Billing_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldBilling_ID", DBNull.Value);
        }
        if (olddbo_ReceiveClass.Invoice_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_No", olddbo_ReceiveClass.Invoice_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_No", DBNull.Value);
        }
        if (olddbo_ReceiveClass.Invoice_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_Date", olddbo_ReceiveClass.Invoice_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_Date", DBNull.Value);
        }
        if (olddbo_ReceiveClass.Invoice_Net_Value.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_Net_Value", olddbo_ReceiveClass.Invoice_Net_Value);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_Net_Value", DBNull.Value);
        }
        if (olddbo_ReceiveClass.Invoice_VAT.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_VAT", olddbo_ReceiveClass.Invoice_VAT);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_VAT", DBNull.Value);
        }
        if (olddbo_ReceiveClass.Invoice_Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_Total", olddbo_ReceiveClass.Invoice_Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldInvoice_Total", DBNull.Value);
        }
        if (olddbo_ReceiveClass.Ref_Invoice_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldRef_Invoice_No", olddbo_ReceiveClass.Ref_Invoice_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldRef_Invoice_No", DBNull.Value);
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

    public static bool Delete(dbo_ReceiveClass clsdbo_Receive)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[ReceiveDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Receive.Receive_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldReceive_ID", clsdbo_Receive.Receive_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldReceive_ID", DBNull.Value);
        }
        if (clsdbo_Receive.Billing_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_ID", clsdbo_Receive.Billing_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_ID", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_No", clsdbo_Receive.Invoice_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_No", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Date", clsdbo_Receive.Invoice_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Date", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_Net_Value.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Net_Value", clsdbo_Receive.Invoice_Net_Value);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Net_Value", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_VAT.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_VAT", clsdbo_Receive.Invoice_VAT);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_VAT", DBNull.Value);
        }
        if (clsdbo_Receive.Invoice_Total.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Total", clsdbo_Receive.Invoice_Total);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Total", DBNull.Value);
        }
        if (clsdbo_Receive.Ref_Invoice_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRef_Invoice_No", clsdbo_Receive.Ref_Invoice_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRef_Invoice_No", DBNull.Value);
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

