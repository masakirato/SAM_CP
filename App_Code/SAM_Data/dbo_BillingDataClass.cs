using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web.UI.WebControls;

public class dbo_BillingDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[BillingSelectAll]";
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

    public static List<dbo_BillingClass> Search(String CV_Number, String Billing_ID, String Billing_Type, String Invoice_No, DateTime? Invoice_DateStart, DateTime? Invoice_DateEnd
        , String PO_No, String Invoice_Status)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[BillingSearch]";
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
        if (!string.IsNullOrEmpty(Billing_Type))
        {

            selectCommand.Parameters.AddWithValue("@Billing_Type", Billing_Type);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_Type", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Number))
        {
            selectCommand.Parameters.AddWithValue("@CV_Number", CV_Number);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Number", DBNull.Value);
        }

        if (Invoice_DateStart.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Invoice_DateStart", Invoice_DateStart.Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_DateStart", DateTime.Now.AddYears(-10));
        }
        if (Invoice_DateEnd.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Invoice_DateEnd", Invoice_DateEnd.Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_DateEnd", DateTime.Now.AddYears(10));
        }
        if (!string.IsNullOrEmpty(PO_No))
        {
            selectCommand.Parameters.AddWithValue("@PO_No", PO_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PO_No", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Invoice_Status))
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Status", Invoice_Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Status", DBNull.Value);
        }

        DataTable dt = new DataTable();
        List<dbo_BillingClass> item = new List<dbo_BillingClass>();

        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_BillingClass()
                    {
                        Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString(),
                        Billing_Type = reader["Billing_Type"] is DBNull ? null : reader["Billing_Type"].ToString(),
                        Order_Type = reader["Order_Type"] is DBNull ? null : reader["Order_Type"].ToString(),
                        CV_Number = reader["CV_Number"] is DBNull ? null : reader["CV_Number"].ToString(),
                        Invoice_No = reader["Invoice_No"] is DBNull ? null : reader["Invoice_No"].ToString(),
                        Invoice_Date = reader["Invoice_Date"] is DBNull ? null : (DateTime?)reader["Invoice_Date"],
                        PO_No = reader["PO_No"] is DBNull ? null : reader["PO_No"].ToString(),
                        PO_Date = reader["PO_Date"] is DBNull ? null : (DateTime?)reader["PO_Date"],
                        Net_Value = reader["Net_Value"] is DBNull ? null : (Decimal?)reader["Net_Value"],
                        Vat = reader["Vat"] is DBNull ? null : (Decimal?)reader["Vat"],
                        Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                        Ref_Invoice_No = reader["Ref_Invoice_No"] is DBNull ? null : reader["Ref_Invoice_No"].ToString(),
                        Invoice_Status = reader["Invoice_Status"] is DBNull ? null : reader["Invoice_Status"].ToString(),
                        Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"],
                        Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString(),
                        Last_Modified_Date = reader["Last_Modified_Date"] is DBNull ? null : (DateTime?)reader["Last_Modified_Date"],

                        Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString(),
                        Order_Status = reader["Order_Status"] is DBNull ? null : reader["Order_Status"].ToString(),
                        Billing_Type_Name = reader["Billing_Type_Name"] is DBNull ? null : reader["Billing_Type_Name"].ToString()
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return item;
    }



    public static List<dbo_BillingClass> Get_Billing(String user_id)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[Get_Billing]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@user_id", user_id);



        List<dbo_BillingClass> item = new List<dbo_BillingClass>();

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
                    item.Add(new dbo_BillingClass()
                    {
                        Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString(),
                        Billing_Type = reader["Billing_Type"] is DBNull ? null : reader["Billing_Type"].ToString(),
                        Order_Type = reader["Order_Type"] is DBNull ? null : reader["Order_Type"].ToString(),
                        CV_Number = reader["CV_Number"] is DBNull ? null : reader["CV_Number"].ToString(),
                        Invoice_No = reader["Invoice_No"] is DBNull ? null : reader["Invoice_No"].ToString(),
                        Invoice_Date = reader["Invoice_Date"] is DBNull ? null : (DateTime?)reader["Invoice_Date"],
                        PO_No = reader["PO_No"] is DBNull ? null : reader["PO_No"].ToString(),
                        PO_Date = reader["PO_Date"] is DBNull ? null : (DateTime?)reader["PO_Date"],
                        Net_Value = reader["Net_Value"] is DBNull ? null : (Decimal?)reader["Net_Value"],
                        Vat = reader["Vat"] is DBNull ? null : (Decimal?)reader["Vat"],
                        Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                        Ref_Invoice_No = reader["Ref_Invoice_No"] is DBNull ? null : reader["Ref_Invoice_No"].ToString(),
                        Invoice_Status = reader["Invoice_Status"] is DBNull ? null : reader["Invoice_Status"].ToString(),
                        Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"],
                        Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString(),
                        Last_Modified_Date = reader["Last_Modified_Date"] is DBNull ? null : (DateTime?)reader["Last_Modified_Date"],
                        Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString(),
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



    public static List<dbo_BillingClass> Search(String CV_Code, String Billing_Type)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[BillingSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (CV_Code != null)
        {
            selectCommand.Parameters.AddWithValue("@CV_Number", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Number", DBNull.Value);
        }
        if (Billing_Type != null)
        {
            selectCommand.Parameters.AddWithValue("@Billing_Type", Billing_Type);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_Type", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Invoice_No", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Invoice_DateStart", DateTime.Now.AddYears(-10));



        selectCommand.Parameters.AddWithValue("@Invoice_DateEnd", DateTime.Now.AddYears(10));
        selectCommand.Parameters.AddWithValue("@PO_No", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Invoice_Status", DBNull.Value);



        List<dbo_BillingClass> item = new List<dbo_BillingClass>();

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
                    item.Add(new dbo_BillingClass()
                    {
                        Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString(),
                        Billing_Type = reader["Billing_Type"] is DBNull ? null : reader["Billing_Type"].ToString(),
                        Order_Type = reader["Order_Type"] is DBNull ? null : reader["Order_Type"].ToString(),
                        CV_Number = reader["CV_Number"] is DBNull ? null : reader["CV_Number"].ToString(),
                        Invoice_No = reader["Invoice_No"] is DBNull ? null : reader["Invoice_No"].ToString(),
                        Invoice_Date = reader["Invoice_Date"] is DBNull ? null : (DateTime?)reader["Invoice_Date"],
                        PO_No = reader["PO_No"] is DBNull ? null : reader["PO_No"].ToString(),
                        PO_Date = reader["PO_Date"] is DBNull ? null : (DateTime?)reader["PO_Date"],
                        Net_Value = reader["Net_Value"] is DBNull ? null : (Decimal?)reader["Net_Value"],
                        Vat = reader["Vat"] is DBNull ? null : (Decimal?)reader["Vat"],
                        Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                        Ref_Invoice_No = reader["Ref_Invoice_No"] is DBNull ? null : reader["Ref_Invoice_No"].ToString(),
                        Invoice_Status = reader["Invoice_Status"] is DBNull ? null : reader["Invoice_Status"].ToString(),
                        Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"],
                        Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString(),
                        Last_Modified_Date = reader["Last_Modified_Date"] is DBNull ? null : (DateTime?)reader["Last_Modified_Date"],
                        Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString(),
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

    public static dbo_BillingClass Select_Record(String Billing_ID)
    {
        dbo_BillingClass clsdbo_Billing = new dbo_BillingClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[BillingSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Billing_ID", Billing_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Billing.Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString();
                clsdbo_Billing.Billing_Type = reader["Billing_Type"] is DBNull ? null : reader["Billing_Type"].ToString();
                clsdbo_Billing.Order_Type = reader["Order_Type"] is DBNull ? null : reader["Order_Type"].ToString();
                clsdbo_Billing.CV_Number = reader["CV_Number"] is DBNull ? null : reader["CV_Number"].ToString();
                clsdbo_Billing.Invoice_No = reader["Invoice_No"] is DBNull ? null : reader["Invoice_No"].ToString();
                clsdbo_Billing.Invoice_Date = reader["Invoice_Date"] is DBNull ? null : (DateTime?)reader["Invoice_Date"];
                clsdbo_Billing.PO_No = reader["PO_No"] is DBNull ? null : reader["PO_No"].ToString();
                clsdbo_Billing.PO_Date = reader["PO_Date"] is DBNull ? null : (DateTime?)reader["PO_Date"];
                clsdbo_Billing.Net_Value = reader["Net_Value"] is DBNull ? null : (Decimal?)reader["Net_Value"];
                clsdbo_Billing.Vat = reader["Vat"] is DBNull ? null : (Decimal?)reader["Vat"];
                clsdbo_Billing.Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"];
                clsdbo_Billing.Ref_Invoice_No = reader["Ref_Invoice_No"] is DBNull ? null : reader["Ref_Invoice_No"].ToString();
                clsdbo_Billing.Invoice_Status = reader["Invoice_Status"] is DBNull ? null : reader["Invoice_Status"].ToString();
                clsdbo_Billing.Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"];
                clsdbo_Billing.Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString();
                clsdbo_Billing.Last_Modified_Date = reader["Last_Modified_Date"] is DBNull ? null : (DateTime?)reader["Last_Modified_Date"];
                clsdbo_Billing.Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString();
                clsdbo_Billing.Order_Status = reader["Order_Status"] is DBNull ? null : reader["Order_Status"].ToString();

            }
            else
            {
                clsdbo_Billing = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Billing;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Billing;
    }

    public static bool Add(dbo_BillingClass clsdbo_Billing)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[BillingInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Billing.Billing_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Billing_ID", clsdbo_Billing.Billing_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
        }
        if (clsdbo_Billing.Billing_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@Billing_Type", clsdbo_Billing.Billing_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Billing_Type", DBNull.Value);
        }
        if (clsdbo_Billing.Order_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@Order_Type", clsdbo_Billing.Order_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_Type", DBNull.Value);
        }
        if (clsdbo_Billing.CV_Number != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Number", clsdbo_Billing.CV_Number);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Number", DBNull.Value);
        }
        if (clsdbo_Billing.Invoice_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_No", clsdbo_Billing.Invoice_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_No", DBNull.Value);
        }
        if (clsdbo_Billing.Invoice_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Date", clsdbo_Billing.Invoice_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Date", DBNull.Value);
        }
        if (clsdbo_Billing.PO_No != null)
        {
            insertCommand.Parameters.AddWithValue("@PO_No", clsdbo_Billing.PO_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@PO_No", DBNull.Value);
        }
        if (clsdbo_Billing.PO_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@PO_Date", clsdbo_Billing.PO_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@PO_Date", DBNull.Value);
        }
        if (clsdbo_Billing.Net_Value.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Net_Value", clsdbo_Billing.Net_Value);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Net_Value", DBNull.Value);
        }
        if (clsdbo_Billing.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_Billing.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (clsdbo_Billing.Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total", clsdbo_Billing.Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total", DBNull.Value);
        }
        if (clsdbo_Billing.Ref_Invoice_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Ref_Invoice_No", clsdbo_Billing.Ref_Invoice_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Ref_Invoice_No", DBNull.Value);
        }
        if (clsdbo_Billing.Invoice_Status != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Status", clsdbo_Billing.Invoice_Status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Status", DBNull.Value);
        }
        if (clsdbo_Billing.Created_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Created_Date", clsdbo_Billing.Created_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_Date", DBNull.Value);
        }
        if (clsdbo_Billing.Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", clsdbo_Billing.Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }
        if (clsdbo_Billing.Last_Modified_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_Date", clsdbo_Billing.Last_Modified_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_Date", DBNull.Value);
        }
        if (clsdbo_Billing.Last_Modified_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_By", clsdbo_Billing.Last_Modified_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
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

    public static bool Update(dbo_BillingClass newdbo_BillingClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[BillingUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_BillingClass.Billing_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_ID", newdbo_BillingClass.Billing_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_ID", DBNull.Value);
        }
        if (newdbo_BillingClass.Billing_Type != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_Type", newdbo_BillingClass.Billing_Type);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_Type", DBNull.Value);
        }
        if (newdbo_BillingClass.Order_Type != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Type", newdbo_BillingClass.Order_Type);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Type", DBNull.Value);
        }
        if (newdbo_BillingClass.CV_Number != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Number", newdbo_BillingClass.CV_Number);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Number", DBNull.Value);
        }
        if (newdbo_BillingClass.Invoice_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_No", newdbo_BillingClass.Invoice_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_No", DBNull.Value);
        }
        if (newdbo_BillingClass.Invoice_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Date", newdbo_BillingClass.Invoice_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Date", DBNull.Value);
        }
        if (newdbo_BillingClass.PO_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPO_No", newdbo_BillingClass.PO_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPO_No", DBNull.Value);
        }
        if (newdbo_BillingClass.PO_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPO_Date", newdbo_BillingClass.PO_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPO_Date", DBNull.Value);
        }
        if (newdbo_BillingClass.Net_Value.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Value", newdbo_BillingClass.Net_Value);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Value", DBNull.Value);
        }
        if (newdbo_BillingClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_BillingClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        if (newdbo_BillingClass.Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", newdbo_BillingClass.Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", DBNull.Value);
        }
        if (newdbo_BillingClass.Ref_Invoice_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRef_Invoice_No", newdbo_BillingClass.Ref_Invoice_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRef_Invoice_No", DBNull.Value);
        }
        if (newdbo_BillingClass.Invoice_Status != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Status", newdbo_BillingClass.Invoice_Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Status", DBNull.Value);
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
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Delete(dbo_BillingClass clsdbo_Billing)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[BillingDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Billing.Billing_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_ID", clsdbo_Billing.Billing_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_ID", DBNull.Value);
        }
        if (clsdbo_Billing.Billing_Type != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_Type", clsdbo_Billing.Billing_Type);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_Type", DBNull.Value);
        }
        if (clsdbo_Billing.Order_Type != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldOrder_Type", clsdbo_Billing.Order_Type);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldOrder_Type", DBNull.Value);
        }
        if (clsdbo_Billing.CV_Number != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Number", clsdbo_Billing.CV_Number);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Number", DBNull.Value);
        }
        if (clsdbo_Billing.Invoice_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_No", clsdbo_Billing.Invoice_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_No", DBNull.Value);
        }
        if (clsdbo_Billing.Invoice_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Date", clsdbo_Billing.Invoice_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Date", DBNull.Value);
        }
        if (clsdbo_Billing.PO_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldPO_No", clsdbo_Billing.PO_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPO_No", DBNull.Value);
        }
        if (clsdbo_Billing.PO_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPO_Date", clsdbo_Billing.PO_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPO_Date", DBNull.Value);
        }
        if (clsdbo_Billing.Net_Value.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldNet_Value", clsdbo_Billing.Net_Value);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldNet_Value", DBNull.Value);
        }
        if (clsdbo_Billing.Vat.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", clsdbo_Billing.Vat);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        }
        if (clsdbo_Billing.Total.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal", clsdbo_Billing.Total);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal", DBNull.Value);
        }
        if (clsdbo_Billing.Ref_Invoice_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRef_Invoice_No", clsdbo_Billing.Ref_Invoice_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRef_Invoice_No", DBNull.Value);
        }
        if (clsdbo_Billing.Invoice_Status != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Status", clsdbo_Billing.Invoice_Status);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_Status", DBNull.Value);
        }
        if (clsdbo_Billing.Created_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_Date", clsdbo_Billing.Created_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_Date", DBNull.Value);
        }
        if (clsdbo_Billing.Created_By != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_By", clsdbo_Billing.Created_By);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_By", DBNull.Value);
        }
        if (clsdbo_Billing.Last_Modified_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_Date", clsdbo_Billing.Last_Modified_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_Date", DBNull.Value);
        }
        if (clsdbo_Billing.Last_Modified_By != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_By", clsdbo_Billing.Last_Modified_By);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_By", DBNull.Value);
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

