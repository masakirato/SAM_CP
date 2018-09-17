using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Reports
/// </summary>
public class Reports
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //RT_PO_DOC
    public static List<RT_PO_DOC> Search(String PO_Number)
    {

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Get_PO_Doc";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);


        List<RT_PO_DOC> item = new List<RT_PO_DOC>();
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

                    item.Add(new RT_PO_DOC()
                    {
                        //CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        PO_Number_OR = reader["PO_Number"].ToString(),
                        Product_ID_PD = reader["Product_ID"].ToString(),
                        CV_Code_AG = reader["CV_Code_from_SAP"].ToString()

                        ,
                        Price_ORD = (decimal)reader["Price"]
                        ,
                        Vat_ORD = (decimal)reader["Vat"]
                        ,
                        Total_ORD = (decimal)reader["Total"]
                        ,
                        Vat_Amount_ORD = (decimal)reader["Vat_Amount"]
                        ,
                        Quantity_ORD = (Int16)reader["Quantity"]
                        ,
                        Sub_Total_ORD = (decimal)reader["Sub_Total"]
                        ,
                        Total_Amount_before_vat_included_OR = (decimal)reader["Total_Amount_before_vat_included"]
                        ,
                        Total_amount_after_vat_included_OR = (decimal)reader["Total_amount_after_vat_included"]

                    }
                        );
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
    //RPT_Get_Requisition_No
    public static List<RPT_Get_Requisition_No> RPT_Get_Requisition_No_Search(String Requisition_No, string SPName)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Get_Requisition_No";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (SPName == "==ระบุ==")
        {
            SPName = string.Empty;
        }

        if (!String.IsNullOrEmpty(Requisition_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SPName))
        {
            selectCommand.Parameters.AddWithValue("@SPName", SPName);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SPName", DBNull.Value);
        }



        List<RPT_Get_Requisition_No> item = new List<RPT_Get_Requisition_No>();
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
                    item.Add(new RPT_Get_Requisition_No()
                    {
                        AgentName = reader["AgentName"].ToString(),
                        clm_1 = (reader["clm_1"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_1"].ToString())),
                        clm_2 = (reader["clm_2"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_2"].ToString())),
                        clm_3 = (reader["clm_3"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_3"].ToString())),
                        clm_4 = (reader["clm_4"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_4"].ToString())),
                        clm_5 = (reader["clm_5"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_5"].ToString())),
                        clm_6 = (reader["clm_6"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_6"].ToString())),
                        clm_7 = (reader["clm_7"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_7"].ToString())),
                        clm_8 = (reader["clm_8"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_8"].ToString())),
                        clm_9 = (reader["clm_9"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_9"].ToString())),
                        clm_10 = (reader["clm_10"] is DBNull ? int.Parse("0") : int.Parse(reader["clm_10"].ToString())),
                        Deposit_Qty = (reader["Deposit_Qty"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Deposit_Qty"].ToString())),
                        Invoice_Address = reader["Invoice_Address"].ToString(),
                        Product_group_ID = reader["Product_group_ID"].ToString(),
                        Product_ID = reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"].ToString(),
                        Requisition_Date = reader["Requisition_Date"].ToString(),//reader["Requisition_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Requisition_Date"].ToString()),
                        Requisition_No = reader["Requisition_No"].ToString(),


                        //  Size = Int16.Parse(reader["Size"].ToString()),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),

                        sp_name = reader["sp_name"].ToString(),
                        Tax_ID = reader["Tax_ID"].ToString(),
                        User_Admin = reader["User_Admin"].ToString(),
                        User_ID = reader["User_ID"].ToString(),
                        User_Owner = reader["User_Owner"].ToString(),
                        User_SP = reader["User_SP"].ToString(),
                        User_Warehouse = reader["User_Warehouse"].ToString(),
                        Return_Qty = (reader["Return_Qty"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Return_Qty"].ToString())),

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
    public static List<RPT_Get_PO_Number> RPT_Get_PO_Number_Search(String PO_Number)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Get_PO_Number";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_Get_PO_Number> item = new List<RPT_Get_PO_Number>();
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
                    item.Add(new RPT_Get_PO_Number()
                    {
                        AgentName = reader["AgentName"].ToString(),


                        Date_of_CP_receive_transaction = reader["Date_of_CP_receive_transaction"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Date_of_CP_receive_transaction"].ToString()),
                        Date_of_create_order_or_PO_Date = reader["Date_of_create_order_or_PO_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Date_of_create_order_or_PO_Date"].ToString()),
                        Date_of_delivery_goods = reader["Date_of_delivery_goods"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Date_of_delivery_goods"].ToString()),

                        Address1 = reader["Address1"].ToString(),
                        Address2 = reader["Address2"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        Home_Phone_No = reader["Home_Phone_No"].ToString(),
                        Fax = reader["Fax"].ToString(),
                        Product_ID = reader["Product_ID"].ToString(),
                        Quantity = (reader["Quantity"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Quantity"].ToString())),
                        Total = (reader["Total"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total"].ToString())),
                        Price = (reader["Price"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Price"].ToString())),
                        Vat = (reader["Vat"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Vat"].ToString())),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Product_Name = reader["Product_Name"].ToString(),
                        Unit_of_item_ID = reader["Unit_of_item_ID"].ToString(),
                        PO_Number = reader["PO_Number"].ToString(),
                        Product_group_ID = reader["Product_group_ID"].ToString(),
                        Position = reader["Position"].ToString(),

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
    //RPT_Get_Clearing_No
    public static List<RPT_Get_Clearing_No> RPT_Get_Clearing_No(String Clearing_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Get_Clearing_No";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        //selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);

        if (!String.IsNullOrEmpty(Clearing_No))
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }

        List<RPT_Get_Clearing_No> item = new List<RPT_Get_Clearing_No>();
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
                    item.Add(new RPT_Get_Clearing_No()
                    {
                        AgentName = reader["AgentName"].ToString(),
                        Clearing_No = reader["Clearing_No"].ToString(),
                        Clearing_Date = reader["Clearing_Date"].ToString(),
                        User_ID = reader["User_ID"].ToString(),
                        Cash_Payment = (reader["Cash_Payment"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Cash_Payment"].ToString())),
                        Actual_Payment = (reader["Actual_Payment"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Actual_Payment"].ToString())),
                        Balance_Outstanding = (reader["Balance_Outstanding"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Balance_Outstanding"].ToString())),
                        Net_Sales_Qty = (reader["Net_Sales_Qty"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Net_Sales_Qty"].ToString())),
                        Net_Sales_Amount = (reader["Net_Sales_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Net_Sales_Amount"].ToString())),
                        Credit_Amount = (reader["Credit_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Credit_Amount"].ToString())),
                        SP_Cash = (reader["SP_Cash"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["SP_Cash"].ToString())),
                        Cash_Payment_Amount = (reader["Cash_Payment_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Cash_Payment_Amount"].ToString())),
                        Cheque_Payment_Amount = (reader["Cheque_Payment_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Cheque_Payment_Amount"].ToString())),
                        Transfer_Payment_Amount = (reader["Transfer_Payment_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Transfer_Payment_Amount"].ToString())),
                        Sub_Total = (reader["Sub_Total"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Sub_Total"].ToString())),
                        Total = (reader["Total"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total"].ToString())),
                        Discount = (reader["Discount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Discount"].ToString())),
                        Today_Points = (reader["Today_Points"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Today_Points"].ToString())),
                        Today_Commission = (reader["Today_Commission"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Today_Commission"].ToString())),
                        This_Month_Sales_Amount = (reader["This_Month_Sales_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["This_Month_Sales_Amount"].ToString())),
                        Today_Deposit_Amount = (reader["Today_Deposit_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Today_Deposit_Amount"].ToString())),
                        Total_Credit_Amount = (reader["Total_Credit_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Credit_Amount"].ToString())),
                        Total_Balance_Outstanding = (reader["Total_Balance_Outstanding"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Balance_Outstanding"].ToString())),
                        Total_Commission = (reader["Total_Commission"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Commission"].ToString())),
                        This_Month_Points = (reader["This_Month_Points"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["This_Month_Points"].ToString())),
                        Total_Deposit = (reader["Total_Deposit"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Deposit"].ToString())),
                        Phone = reader["Phone"].ToString(),

                        Total_Deduct = (reader["Total_Deduct"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Deduct"].ToString())),
                        Total_Subsidy = (reader["Total_Subsidy"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Subsidy"].ToString())),
                        Total_Credit1 = (reader["Total_Credit1"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Credit1"].ToString())),
                        Total_Credit2 = (reader["Total_Credit2"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Credit2"].ToString())),
                        Total_Credit3 = (reader["Total_Credit3"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Credit3"].ToString())),

                        Today_Return_Amount = (reader["Today_Return_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Today_Return_Amount"].ToString())),

                        SP = reader["SP"].ToString(),
                        ADMIN = reader["ADMIN"].ToString(),
                        OWNER = reader["OWNER"].ToString(),
                        Tax_ID = reader["Tax_ID"].ToString(),
                        Address = reader["Address"].ToString(),

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

    public static List<RPT_Get_OtherRequisition_No> RPT_Get_OtherRequisition_No(String Requisition_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Get_OtherRequisition_No";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!String.IsNullOrEmpty(Requisition_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }




        List<RPT_Get_OtherRequisition_No> item = new List<RPT_Get_OtherRequisition_No>();
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
                    item.Add(new RPT_Get_OtherRequisition_No()
                    {
                        AgentName = reader["AgentName"].ToString(),
                        Product_group_ID = reader["Product_group_ID"].ToString(),
                        Product_ID = reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"].ToString(),
                        Requisition_No = reader["Requisition_No"].ToString(),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Address = reader["Address"].ToString(),
                        Admin_Name = reader["Admin_Name"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        Fax = reader["Fax"].ToString(),
                        Reason = reader["Reason"].ToString(),
                        Home_Phone_No = reader["Home_Phone_No"].ToString(),
                        Owner_Name = reader["Owner_Name"].ToString(),
                        RQ_Date = reader["RQ_Date"].ToString(),
                        SP_ID = reader["SP_ID"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        Warehouse_Name = reader["Warehouse_Name"].ToString(),
                        Order_Product = reader["Order_Product"].ToString(),
                        Requisition_Qty = (reader["Requisition_Qty"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Requisition_Qty"].ToString())),
                        Unit = reader["Unit"].ToString()
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




    //RPT_Order_By_Product
    public static List<RPT_Order_By_Product> RPT_Order_By_Product(String Region, String CV_Code, DateTime? Invoice_Date_Begin, DateTime? Invoice_Date_End, String Product_group_ID, int? Size, String Product_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Order_By_Product";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (Invoice_Date_Begin.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Date_Begin", Invoice_Date_Begin);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Date_End", DateTime.Now.AddYears(-1));
        }
        if (Invoice_Date_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Date_End", Invoice_Date_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Date_End", DateTime.Now.AddYears(1));
        }
        if (!String.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }
        if (Size != null)
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Product_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", Product_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }

        List<RPT_Order_By_Product> item = new List<RPT_Order_By_Product>();
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
                    item.Add(new RPT_Order_By_Product()
                    {
                        Product_ID = reader["Product_ID"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        Unit_of_item_ID = reader["Unit_of_item_ID"].ToString(),
                        PO_Number = reader["PO_Number"].ToString(),
                        Product_Name = reader["Product_Name"].ToString(),
                        Product_group_ID = reader["Product_group_ID"].ToString(),

                        Quantity = (reader["Quantity"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Quantity"].ToString())),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Order_No = (reader["Order_No"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Order_No"].ToString())),

                        Total_amount_after_vat_included = (reader["Total_amount_after_vat_included"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_amount_after_vat_included"].ToString())),
                        Price = (reader["Price"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Price"].ToString())),
                        Total = (reader["Total"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total"].ToString())),
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
    //RPT_CustomerType
    public static List<RPT_CustomerType> RPT_CustomerType(String CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_CustomerType";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }


        List<RPT_CustomerType> item = new List<RPT_CustomerType>();
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
                    item.Add(new RPT_CustomerType()
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        First_Name = reader["First_Name"].ToString(),
                        Last_Name = reader["Last_Name"].ToString(),
                        Residence_Type_ID = reader["Residence_Type_ID"].ToString(),
                        Type_1 = (int)reader["Type_1"],
                        Type_2 = (int)reader["Type_2"],
                        Type_3 = (int)reader["Type_3"],
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
    //RPT_CLR_SALE_SUM_FORM_1_3
    public static List<RPT_CLR_SALE_SUM_FORM_1_3> RPT_CLR_SALE_SUM_FORM_1_3(string CRNO)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_CLR_SALE_SUM_FORM_1_3";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!String.IsNullOrEmpty(CRNO))
        {
            selectCommand.Parameters.AddWithValue("@CRNO", CRNO);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CRNO", DBNull.Value);
        }

        List<RPT_CLR_SALE_SUM_FORM_1_3> item = new List<RPT_CLR_SALE_SUM_FORM_1_3>();
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
                    item.Add(new RPT_CLR_SALE_SUM_FORM_1_3()
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        CLR_NO = reader["CLR_NO"].ToString(),
                        CreateDate = reader["CreateDate"] is DBNull ? null : reader["CreateDate"].ToString(),
                        SPNO = reader["SPNO"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        SP_Tel = reader["SP_Tel"].ToString(),
                        Product_Name = reader["Product_Name"].ToString(),
                        QTY = (reader["QTY"] is DBNull ? int.Parse("0") : int.Parse(reader["QTY"].ToString())),
                        Total_Amt = (reader["Total_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Amt"].ToString())),
                        Point = (reader["Point"] is DBNull ? int.Parse("0") : int.Parse(reader["Point"].ToString())),
                        Comm = (decimal)reader["Comm"],
                        PG_ID = reader["PG_ID"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
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
    //RPT_RQ_CASH_FORM_4
    public static List<RPT_RQ_CASH_FORM_4> RPT_RQ_CASH_FORM_4(string Region, string CV_Code, string RENO, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string Temp5, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_RQ_CASH_FORM_4";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(RENO))
        {
            selectCommand.Parameters.AddWithValue("@RENO", RENO);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@RENO", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp0))
        {
            selectCommand.Parameters.AddWithValue("@Temp0", Temp0);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp1))
        {
            selectCommand.Parameters.AddWithValue("@Temp1", Temp1);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp2))
        {
            selectCommand.Parameters.AddWithValue("@Temp2", Temp2);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp3))
        {
            selectCommand.Parameters.AddWithValue("@Temp3", Temp3);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp4))
        {
            selectCommand.Parameters.AddWithValue("@Temp4", Temp4);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp5))
        {
            selectCommand.Parameters.AddWithValue("@Temp5", Temp5);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(UserID))
        {
            selectCommand.Parameters.AddWithValue("@UserID", UserID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }


        List<RPT_RQ_CASH_FORM_4> item = new List<RPT_RQ_CASH_FORM_4>();
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
                    item.Add(new RPT_RQ_CASH_FORM_4()
                    {
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        CV_Name = reader["AG_Admin_Name"] is DBNull ? null : reader["CV_Name"].ToString(),
                        Address = reader["Address"] is DBNull ? null : reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"] is DBNull ? null : reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"] is DBNull ? null : reader["TAXID"].ToString(),
                        RENo = reader["RENo"] is DBNull ? null : reader["RENo"].ToString(),
                        CreateDate = reader["CreateDate"] is DBNull ? null : reader["CreateDate"].ToString(),
                        SPNO = reader["SPNO"] is DBNull ? null : reader["SPNO"].ToString(),
                        SP_Name = reader["SP_Name"] is DBNull ? null : reader["SP_Name"].ToString(),
                        SP_Tel = reader["SP_Tel"] is DBNull ? null : reader["SP_Tel"].ToString(),
                        StartDate = reader["StartDate"] is DBNull ? null : reader["StartDate"].ToString(),
                        EndDate = reader["EndDate"] is DBNull ? null : reader["EndDate"].ToString(),
                        Total_Amt = (reader["Total_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Total_Amt"].ToString())),
                        Comm_Balance = (reader["Comm_Balance"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Comm_Balance"].ToString())),
                        Outstanding_Amt = (reader["Outstanding_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Outstanding_Amt"].ToString())),
                        Remain_Amt = (reader["Remain_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Remain_Amt"].ToString())),
                        RQ_Name = reader["RQ_Name"] is DBNull ? null : reader["RQ_Name"].ToString(),
                        AG_Admin_Name = reader["AG_Admin_Name"] is DBNull ? null : reader["AG_Admin_Name"].ToString(),
                        AG_OWNER_Name = reader["AG_OWNER_Name"] is DBNull ? null : reader["AG_OWNER_Name"].ToString(),
                        Temp0 = reader["Temp0"] is DBNull ? null : reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"] is DBNull ? null : reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"] is DBNull ? null : reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"] is DBNull ? null : reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"] is DBNull ? null : reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"] is DBNull ? null : reader["Temp5"].ToString(),
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
    //RPT_Get_Clearing_No_Deduct
    public static List<RPT_COUNT_STOCK_FORM_5> RPT_COUNT_STOCK_FORM_5(string Region, string CV_Code, string CTNO, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string Temp5, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_COUNT_STOCK_FORM_5";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CTNO))
        {
            selectCommand.Parameters.AddWithValue("@CTNO", CTNO);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CTNO", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp0))
        {
            selectCommand.Parameters.AddWithValue("@Temp0", Temp0);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp1))
        {
            selectCommand.Parameters.AddWithValue("@Temp1", Temp1);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp2))
        {
            selectCommand.Parameters.AddWithValue("@Temp2", Temp2);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp3))
        {
            selectCommand.Parameters.AddWithValue("@Temp3", Temp3);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp4))
        {
            selectCommand.Parameters.AddWithValue("@Temp4", Temp4);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp5))
        {
            selectCommand.Parameters.AddWithValue("@Temp5", Temp5);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(UserID))
        {
            selectCommand.Parameters.AddWithValue("@UserID", UserID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_COUNT_STOCK_FORM_5> item = new List<RPT_COUNT_STOCK_FORM_5>();
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
                    item.Add(new RPT_COUNT_STOCK_FORM_5()
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        Address = reader["Address"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        CreateDate = reader["CreateDate"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["CreateDate"].ToString()), //reader["CreateDate"].ToString(),

                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        AG_Admin_Name = reader["AG_Admin_Name"].ToString(),
                        AG_OWNER_Name = reader["AG_OWNER_Name"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        RQ_Name = reader["RQ_Name"].ToString(),
                        Count_QTY = (reader["Count_QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Count_QTY"].ToString())), //reader["Count_QTY"] is DBNull ? "................" : reader["Count_QTY"].ToString(),
                        Diff_QTY = (reader["Diff_QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Diff_QTY"].ToString())),//reader["Diff_QTY"] is DBNull ? string.Empty : reader["Diff_QTY"].ToString(),
                        Product_ID = reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"].ToString(),
                        QTY = (reader["QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["QTY"].ToString())),//(reader["QTY"] is DBNull ? int.Parse("0") : int.Parse(reader["QTY"].ToString())),
                        Remark = reader["Remark"].ToString(),
                        Unit = reader["Unit"].ToString(),

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
    public static RPT_Get_Clearing_No_Deduct Select_Record_Deduct(string Clearing_No)
    {

        RPT_Get_Clearing_No_Deduct Get_Clearing_No_Deduct = new RPT_Get_Clearing_No_Deduct();

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RPT_Get_Clearing_No_Deduct]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);

        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                Get_Clearing_No_Deduct.Amount = (decimal)reader["Amount"];
                Get_Clearing_No_Deduct.Description = reader["Description"].ToString();
                Get_Clearing_No_Deduct.Type = reader["Type"].ToString();
                Get_Clearing_No_Deduct.Unit = reader["Unit"].ToString();
            }
            else
            {
                Get_Clearing_No_Deduct = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {

            return Get_Clearing_No_Deduct;
        }
        finally
        {
            connection.Close();
        }
        return Get_Clearing_No_Deduct;
    }
    //RPT_Get_Clearing_No_Payment
    public static RPT_Get_Clearing_No_Payment Select_Record_Payment(string Clearing_No)
    {

        RPT_Get_Clearing_No_Payment Get_Clearing_No_Payment = new RPT_Get_Clearing_No_Payment();

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RPT_Get_Clearing_No_Payment]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);

        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                Get_Clearing_No_Payment.Amount = (decimal)reader["Amount"];
                Get_Clearing_No_Payment.Description = reader["Description"].ToString();
                Get_Clearing_No_Payment.Type = reader["Type"].ToString();
                Get_Clearing_No_Payment.Unit = reader["Unit"].ToString();
            }
            else
            {
                Get_Clearing_No_Payment = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {

            return Get_Clearing_No_Payment;
        }
        finally
        {
            connection.Close();
        }
        return Get_Clearing_No_Payment;
    }
    //RPT_Get_Clearing_No_Subsidy
    public static RPT_Get_Clearing_No_Subsidy Select_Record_Subsidy(string Clearing_No)
    {

        RPT_Get_Clearing_No_Subsidy Get_Clearing_No_Subsidy = new RPT_Get_Clearing_No_Subsidy();

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RPT_Get_Clearing_No_Payment]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);

        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                Get_Clearing_No_Subsidy.Amount = (decimal)reader["Amount"];
                Get_Clearing_No_Subsidy.Description = reader["Description"].ToString();
                Get_Clearing_No_Subsidy.Type = reader["Type"].ToString();
                Get_Clearing_No_Subsidy.Unit = reader["Unit"].ToString();
            }
            else
            {
                Get_Clearing_No_Subsidy = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {

            return Get_Clearing_No_Subsidy;
        }
        finally
        {
            connection.Close();
        }
        return Get_Clearing_No_Subsidy;
    }
    //Report Order
    public static List<RPT_AnnualReport_4121> RPT_AnnualReport_4121(String Region, String CV_Code, string StartMonth, string EndMonth, string Year, string PGroup, string Size, string Unit, string User_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_AnnualReport_4121";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartMonth))
        {
            selectCommand.Parameters.AddWithValue("@StartMonth", StartMonth);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartMonth", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndMonth))
        {
            selectCommand.Parameters.AddWithValue("@EndMonth", EndMonth);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndMonth", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Year))
        {
            selectCommand.Parameters.AddWithValue("@Year", Year);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(PGroup))
        {
            selectCommand.Parameters.AddWithValue("@PGroup", PGroup);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PGroup", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Size))
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Unit))
        {
            selectCommand.Parameters.AddWithValue("@Unit", Unit);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Unit", DBNull.Value);
        }
        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_AnnualReport_4121> item = new List<RPT_AnnualReport_4121>();
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
                    item.Add(new RPT_AnnualReport_4121
                    {
                        Agent_Name = reader["Agent_Name"].ToString(),
                        Product_group_ID = reader["Product_group_id"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        CV_Code = reader["CV_Code"].ToString(),

                        M1 = reader["M1"] is DBNull ? 0 : (decimal)reader["M1"],
                        M2 = reader["M2"] is DBNull ? 0 : (decimal)reader["M2"],
                        M3 = reader["M3"] is DBNull ? 0 : (decimal)reader["M3"],
                        M4 = reader["M4"] is DBNull ? 0 : (decimal)reader["M4"],
                        M5 = reader["M5"] is DBNull ? 0 : (decimal)reader["M5"],
                        M6 = reader["M6"] is DBNull ? 0 : (decimal)reader["M6"],
                        M7 = reader["M7"] is DBNull ? 0 : (decimal)reader["M7"],
                        M8 = reader["M8"] is DBNull ? 0 : (decimal)reader["M8"],
                        M9 = reader["M9"] is DBNull ? 0 : (decimal)reader["M9"],
                        M10 = reader["M10"] is DBNull ? 0 : (decimal)reader["M10"],
                        M11 = reader["M11"] is DBNull ? 0 : (decimal)reader["M11"],
                        M12 = reader["M12"] is DBNull ? 0 : (decimal)reader["M12"],
                        Q1 = reader["Q1"] is DBNull ? 0 : (decimal)reader["Q1"],
                        Q2 = reader["Q2"] is DBNull ? 0 : (decimal)reader["Q2"],
                        Q3 = reader["Q3"] is DBNull ? 0 : (decimal)reader["Q3"],
                        Q4 = reader["Q4"] is DBNull ? 0 : (decimal)reader["Q4"],

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
    //Agent Point
    public static List<RPT_AGENT_POINT_41226> RPT_AGENT_POINT_41226(String Region, String CV_Code, string SP_ID, string StartMonth, string EndMont, string StartYear, string EndYear, string Temp0, string Temp1, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_AGENT_POINT_41226";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartMonth))
        {
            selectCommand.Parameters.AddWithValue("@StartMonth", StartMonth);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartMonth", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndMont))
        {
            selectCommand.Parameters.AddWithValue("@EndMonth", EndMont);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndMonth", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartYear))
        {
            selectCommand.Parameters.AddWithValue("@StartYear", StartYear);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartYear", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndYear))
        {
            selectCommand.Parameters.AddWithValue("@EndYear", EndYear);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndYear", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_AGENT_POINT_41226> item = new List<RPT_AGENT_POINT_41226>();
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
                    item.Add(new RPT_AGENT_POINT_41226
                    {
                        #region
                        //Agent_Name = reader["Agent_Name"].ToString(),
                        //Product_group_id = reader["Product_group_id"].ToString(),
                        //Region_Name = reader["Region_Name"].ToString(),
                        //Unit = reader["Unit"].ToString(),
                        //Month = (int)reader["Month"],
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //TOTAL = (decimal)reader["TOTAL"],
                        //Year = (int)reader["Year"],
                        //CV_Code = reader["CV_Code"].ToString(),
                        //Temp0 = reader["Temp0"].ToString(),
                        //Temp1 = reader["Temp1"].ToString(),
                        //Temp2 = reader["Temp2"].ToString(),
                        //Temp3 = reader["Temp3"].ToString(),
                        //Temp4 = reader["Temp4"].ToString(),
                        //Temp5 = reader["Temp5"].ToString(),
                        #endregion

                        Region = reader["Region"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        SP_ID = reader["SP_ID"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        ClearingYear = reader["ClearingYear"].ToString(),
                        ClearingMonth = reader["ClearingMonth"].ToString(),

                        //A200
                        A200_D1 = (Int32)reader["A200_D1"],
                        A200_D2 = (Int32)reader["A200_D2"],
                        A200_D3 = (Int32)reader["A200_D3"],
                        A200_D4 = (Int32)reader["A200_D4"],
                        A200_D5 = (Int32)reader["A200_D5"],
                        A200_D6 = (Int32)reader["A200_D6"],
                        A200_D7 = (Int32)reader["A200_D7"],
                        A200_D8 = (Int32)reader["A200_D8"],
                        A200_D9 = (Int32)reader["A200_D9"],
                        A200_D10 = (Int32)reader["A200_D10"],
                        A200_D11 = (Int32)reader["A200_D11"],
                        A200_D12 = (Int32)reader["A200_D12"],
                        A200_D13 = (Int32)reader["A200_D13"],
                        A200_D14 = (Int32)reader["A200_D14"],
                        A200_D15 = (Int32)reader["A200_D15"],
                        A200_D16 = (Int32)reader["A200_D16"],
                        A200_D17 = (Int32)reader["A200_D17"],
                        A200_D18 = (Int32)reader["A200_D18"],
                        A200_D19 = (Int32)reader["A200_D19"],
                        A200_D20 = (Int32)reader["A200_D20"],
                        A200_D21 = (Int32)reader["A200_D21"],
                        A200_D22 = (Int32)reader["A200_D22"],
                        A200_D23 = (Int32)reader["A200_D23"],
                        A200_D24 = (Int32)reader["A200_D24"],
                        A200_D25 = (Int32)reader["A200_D25"],
                        A200_D26 = (Int32)reader["A200_D26"],
                        A200_D27 = (Int32)reader["A200_D27"],
                        A200_D28 = (Int32)reader["A200_D28"],
                        A200_D29 = (Int32)reader["A200_D29"],
                        A200_D30 = (Int32)reader["A200_D30"],
                        A200_D31 = (Int32)reader["A200_D31"],
                        //END A200

                        // A450
                        A450_D1 = (Int32)reader["A450_D1"],
                        A450_D2 = (Int32)reader["A450_D2"],
                        A450_D3 = (Int32)reader["A450_D3"],
                        A450_D4 = (Int32)reader["A450_D4"],
                        A450_D5 = (Int32)reader["A450_D5"],
                        A450_D6 = (Int32)reader["A450_D6"],
                        A450_D7 = (Int32)reader["A450_D7"],
                        A450_D8 = (Int32)reader["A450_D8"],
                        A450_D9 = (Int32)reader["A450_D9"],
                        A450_D10 = (Int32)reader["A450_D10"],
                        A450_D11 = (Int32)reader["A450_D11"],
                        A450_D12 = (Int32)reader["A450_D12"],
                        A450_D13 = (Int32)reader["A450_D13"],
                        A450_D14 = (Int32)reader["A450_D14"],
                        A450_D15 = (Int32)reader["A450_D15"],
                        A450_D16 = (Int32)reader["A450_D16"],
                        A450_D17 = (Int32)reader["A450_D17"],
                        A450_D18 = (Int32)reader["A450_D18"],
                        A450_D19 = (Int32)reader["A450_D19"],
                        A450_D20 = (Int32)reader["A450_D20"],
                        A450_D21 = (Int32)reader["A450_D21"],
                        A450_D22 = (Int32)reader["A450_D22"],
                        A450_D23 = (Int32)reader["A450_D23"],
                        A450_D24 = (Int32)reader["A450_D24"],
                        A450_D25 = (Int32)reader["A450_D25"],
                        A450_D26 = (Int32)reader["A450_D26"],
                        A450_D27 = (Int32)reader["A450_D27"],
                        A450_D28 = (Int32)reader["A450_D28"],
                        A450_D29 = (Int32)reader["A450_D29"],
                        A450_D30 = (Int32)reader["A450_D30"],
                        A450_D31 = (Int32)reader["A450_D31"],

                        //END A450

                        //A2000
                        A2000_D1 = (Int32)reader["A2000_D1"],
                        A2000_D2 = (Int32)reader["A2000_D2"],
                        A2000_D3 = (Int32)reader["A2000_D3"],
                        A2000_D4 = (Int32)reader["A2000_D4"],
                        A2000_D5 = (Int32)reader["A2000_D5"],
                        A2000_D6 = (Int32)reader["A2000_D6"],
                        A2000_D7 = (Int32)reader["A2000_D7"],
                        A2000_D8 = (Int32)reader["A2000_D8"],
                        A2000_D9 = (Int32)reader["A2000_D9"],
                        A2000_D10 = (Int32)reader["A2000_D10"],
                        A2000_D11 = (Int32)reader["A2000_D11"],
                        A2000_D12 = (Int32)reader["A2000_D12"],
                        A2000_D13 = (Int32)reader["A2000_D13"],
                        A2000_D14 = (Int32)reader["A2000_D14"],
                        A2000_D15 = (Int32)reader["A2000_D15"],
                        A2000_D16 = (Int32)reader["A2000_D16"],
                        A2000_D17 = (Int32)reader["A2000_D17"],
                        A2000_D18 = (Int32)reader["A2000_D18"],
                        A2000_D19 = (Int32)reader["A2000_D19"],
                        A2000_D20 = (Int32)reader["A2000_D20"],
                        A2000_D21 = (Int32)reader["A2000_D21"],
                        A2000_D22 = (Int32)reader["A2000_D22"],
                        A2000_D23 = (Int32)reader["A2000_D23"],
                        A2000_D24 = (Int32)reader["A2000_D24"],
                        A2000_D25 = (Int32)reader["A2000_D25"],
                        A2000_D26 = (Int32)reader["A2000_D26"],
                        A2000_D27 = (Int32)reader["A2000_D27"],
                        A2000_D28 = (Int32)reader["A2000_D28"],
                        A2000_D29 = (Int32)reader["A2000_D29"],
                        A2000_D30 = (Int32)reader["A2000_D30"],
                        A2000_D31 = (Int32)reader["A2000_D31"],

                        //END A2000

                        //A5000
                        A5000_D1 = (Int32)reader["A5000_D1"],
                        A5000_D2 = (Int32)reader["A5000_D2"],
                        A5000_D3 = (Int32)reader["A5000_D3"],
                        A5000_D4 = (Int32)reader["A5000_D4"],
                        A5000_D5 = (Int32)reader["A5000_D5"],
                        A5000_D6 = (Int32)reader["A5000_D6"],
                        A5000_D7 = (Int32)reader["A5000_D7"],
                        A5000_D8 = (Int32)reader["A5000_D8"],
                        A5000_D9 = (Int32)reader["A5000_D9"],
                        A5000_D10 = (Int32)reader["A5000_D10"],
                        A5000_D11 = (Int32)reader["A5000_D11"],
                        A5000_D12 = (Int32)reader["A5000_D12"],
                        A5000_D13 = (Int32)reader["A5000_D13"],
                        A5000_D14 = (Int32)reader["A5000_D14"],
                        A5000_D15 = (Int32)reader["A5000_D15"],
                        A5000_D16 = (Int32)reader["A5000_D16"],
                        A5000_D17 = (Int32)reader["A5000_D17"],
                        A5000_D18 = (Int32)reader["A5000_D18"],
                        A5000_D19 = (Int32)reader["A5000_D19"],
                        A5000_D20 = (Int32)reader["A5000_D20"],
                        A5000_D21 = (Int32)reader["A5000_D21"],
                        A5000_D22 = (Int32)reader["A5000_D22"],
                        A5000_D23 = (Int32)reader["A5000_D23"],
                        A5000_D24 = (Int32)reader["A5000_D24"],
                        A5000_D25 = (Int32)reader["A5000_D25"],
                        A5000_D26 = (Int32)reader["A5000_D26"],
                        A5000_D27 = (Int32)reader["A5000_D27"],
                        A5000_D28 = (Int32)reader["A5000_D28"],
                        A5000_D29 = (Int32)reader["A5000_D29"],
                        A5000_D30 = (Int32)reader["A5000_D30"],
                        A5000_D31 = (Int32)reader["A5000_D31"],

                        //END A5000

                        //A350
                        A350_D1 = (Int32)reader["A350_D1"],
                        A350_D2 = (Int32)reader["A350_D2"],
                        A350_D3 = (Int32)reader["A350_D3"],
                        A350_D4 = (Int32)reader["A350_D4"],
                        A350_D5 = (Int32)reader["A350_D5"],
                        A350_D6 = (Int32)reader["A350_D6"],
                        A350_D7 = (Int32)reader["A350_D7"],
                        A350_D8 = (Int32)reader["A350_D8"],
                        A350_D9 = (Int32)reader["A350_D9"],
                        A350_D10 = (Int32)reader["A350_D10"],
                        A350_D11 = (Int32)reader["A350_D11"],
                        A350_D12 = (Int32)reader["A350_D12"],
                        A350_D13 = (Int32)reader["A350_D13"],
                        A350_D14 = (Int32)reader["A350_D14"],
                        A350_D15 = (Int32)reader["A350_D15"],
                        A350_D16 = (Int32)reader["A350_D16"],
                        A350_D17 = (Int32)reader["A350_D17"],
                        A350_D18 = (Int32)reader["A350_D18"],
                        A350_D19 = (Int32)reader["A350_D19"],
                        A350_D20 = (Int32)reader["A350_D20"],
                        A350_D21 = (Int32)reader["A350_D21"],
                        A350_D22 = (Int32)reader["A350_D22"],
                        A350_D23 = (Int32)reader["A350_D23"],
                        A350_D24 = (Int32)reader["A350_D24"],
                        A350_D25 = (Int32)reader["A350_D25"],
                        A350_D26 = (Int32)reader["A350_D26"],
                        A350_D27 = (Int32)reader["A350_D27"],
                        A350_D28 = (Int32)reader["A350_D28"],
                        A350_D29 = (Int32)reader["A350_D29"],
                        A350_D30 = (Int32)reader["A350_D30"],
                        A350_D31 = (Int32)reader["A350_D31"],

                        //END A350

                        //A180
                        A180_D1 = (Int32)reader["A180_D1"],
                        A180_D2 = (Int32)reader["A180_D2"],
                        A180_D3 = (Int32)reader["A180_D3"],
                        A180_D4 = (Int32)reader["A180_D4"],
                        A180_D5 = (Int32)reader["A180_D5"],
                        A180_D6 = (Int32)reader["A180_D6"],
                        A180_D7 = (Int32)reader["A180_D7"],
                        A180_D8 = (Int32)reader["A180_D8"],
                        A180_D9 = (Int32)reader["A180_D9"],
                        A180_D10 = (Int32)reader["A180_D10"],
                        A180_D11 = (Int32)reader["A180_D11"],
                        A180_D12 = (Int32)reader["A180_D12"],
                        A180_D13 = (Int32)reader["A180_D13"],
                        A180_D14 = (Int32)reader["A180_D14"],
                        A180_D15 = (Int32)reader["A180_D15"],
                        A180_D16 = (Int32)reader["A180_D16"],
                        A180_D17 = (Int32)reader["A180_D17"],
                        A180_D18 = (Int32)reader["A180_D18"],
                        A180_D19 = (Int32)reader["A180_D19"],
                        A180_D20 = (Int32)reader["A180_D20"],
                        A180_D21 = (Int32)reader["A180_D21"],
                        A180_D22 = (Int32)reader["A180_D22"],
                        A180_D23 = (Int32)reader["A180_D23"],
                        A180_D24 = (Int32)reader["A180_D24"],
                        A180_D25 = (Int32)reader["A180_D25"],
                        A180_D26 = (Int32)reader["A180_D26"],
                        A180_D27 = (Int32)reader["A180_D27"],
                        A180_D28 = (Int32)reader["A180_D28"],
                        A180_D29 = (Int32)reader["A180_D29"],
                        A180_D30 = (Int32)reader["A180_D30"],
                        A180_D31 = (Int32)reader["A180_D31"],

                        //END A180

                        //A830
                        A830_D1 = (Int32)reader["A830_D1"],
                        A830_D2 = (Int32)reader["A830_D2"],
                        A830_D3 = (Int32)reader["A830_D3"],
                        A830_D4 = (Int32)reader["A830_D4"],
                        A830_D5 = (Int32)reader["A830_D5"],
                        A830_D6 = (Int32)reader["A830_D6"],
                        A830_D7 = (Int32)reader["A830_D7"],
                        A830_D8 = (Int32)reader["A830_D8"],
                        A830_D9 = (Int32)reader["A830_D9"],
                        A830_D10 = (Int32)reader["A830_D10"],
                        A830_D11 = (Int32)reader["A830_D11"],
                        A830_D12 = (Int32)reader["A830_D12"],
                        A830_D13 = (Int32)reader["A830_D13"],
                        A830_D14 = (Int32)reader["A830_D14"],
                        A830_D15 = (Int32)reader["A830_D15"],
                        A830_D16 = (Int32)reader["A830_D16"],
                        A830_D17 = (Int32)reader["A830_D17"],
                        A830_D18 = (Int32)reader["A830_D18"],
                        A830_D19 = (Int32)reader["A830_D19"],
                        A830_D20 = (Int32)reader["A830_D20"],
                        A830_D21 = (Int32)reader["A830_D21"],
                        A830_D22 = (Int32)reader["A830_D22"],
                        A830_D23 = (Int32)reader["A830_D23"],
                        A830_D24 = (Int32)reader["A830_D24"],
                        A830_D25 = (Int32)reader["A830_D25"],
                        A830_D26 = (Int32)reader["A830_D26"],
                        A830_D27 = (Int32)reader["A830_D27"],
                        A830_D28 = (Int32)reader["A830_D28"],
                        A830_D29 = (Int32)reader["A830_D29"],
                        A830_D30 = (Int32)reader["A830_D30"],
                        A830_D31 = (Int32)reader["A830_D31"],

                        //END A830

                        //B140
                        B140_D1 = (Int32)reader["B140_D1"],
                        B140_D2 = (Int32)reader["B140_D2"],
                        B140_D3 = (Int32)reader["B140_D3"],
                        B140_D4 = (Int32)reader["B140_D4"],
                        B140_D5 = (Int32)reader["B140_D5"],
                        B140_D6 = (Int32)reader["B140_D6"],
                        B140_D7 = (Int32)reader["B140_D7"],
                        B140_D8 = (Int32)reader["B140_D8"],
                        B140_D9 = (Int32)reader["B140_D9"],
                        B140_D10 = (Int32)reader["B140_D10"],
                        B140_D11 = (Int32)reader["B140_D11"],
                        B140_D12 = (Int32)reader["B140_D12"],
                        B140_D13 = (Int32)reader["B140_D13"],
                        B140_D14 = (Int32)reader["B140_D14"],
                        B140_D15 = (Int32)reader["B140_D15"],
                        B140_D16 = (Int32)reader["B140_D16"],
                        B140_D17 = (Int32)reader["B140_D17"],
                        B140_D18 = (Int32)reader["B140_D18"],
                        B140_D19 = (Int32)reader["B140_D19"],
                        B140_D20 = (Int32)reader["B140_D20"],
                        B140_D21 = (Int32)reader["B140_D21"],
                        B140_D22 = (Int32)reader["B140_D22"],
                        B140_D23 = (Int32)reader["B140_D23"],
                        B140_D24 = (Int32)reader["B140_D24"],
                        B140_D25 = (Int32)reader["B140_D25"],
                        B140_D26 = (Int32)reader["B140_D26"],
                        B140_D27 = (Int32)reader["B140_D27"],
                        B140_D28 = (Int32)reader["B140_D28"],
                        B140_D29 = (Int32)reader["B140_D29"],
                        B140_D30 = (Int32)reader["B140_D30"],
                        B140_D31 = (Int32)reader["B140_D31"],
                        //END B140

                        // B145
                        B145_D1 = (Int32)reader["B145_D1"],
                        B145_D2 = (Int32)reader["B145_D2"],
                        B145_D3 = (Int32)reader["B145_D3"],
                        B145_D4 = (Int32)reader["B145_D4"],
                        B145_D5 = (Int32)reader["B145_D5"],
                        B145_D6 = (Int32)reader["B145_D6"],
                        B145_D7 = (Int32)reader["B145_D7"],
                        B145_D8 = (Int32)reader["B145_D8"],
                        B145_D9 = (Int32)reader["B145_D9"],
                        B145_D10 = (Int32)reader["B145_D10"],
                        B145_D11 = (Int32)reader["B145_D11"],
                        B145_D12 = (Int32)reader["B145_D12"],
                        B145_D13 = (Int32)reader["B145_D13"],
                        B145_D14 = (Int32)reader["B145_D14"],
                        B145_D15 = (Int32)reader["B145_D15"],
                        B145_D16 = (Int32)reader["B145_D16"],
                        B145_D17 = (Int32)reader["B145_D17"],
                        B145_D18 = (Int32)reader["B145_D18"],
                        B145_D19 = (Int32)reader["B145_D19"],
                        B145_D20 = (Int32)reader["B145_D20"],
                        B145_D21 = (Int32)reader["B145_D21"],
                        B145_D22 = (Int32)reader["B145_D22"],
                        B145_D23 = (Int32)reader["B145_D23"],
                        B145_D24 = (Int32)reader["B145_D24"],
                        B145_D25 = (Int32)reader["B145_D25"],
                        B145_D26 = (Int32)reader["B145_D26"],
                        B145_D27 = (Int32)reader["B145_D27"],
                        B145_D28 = (Int32)reader["B145_D28"],
                        B145_D29 = (Int32)reader["B145_D29"],
                        B145_D30 = (Int32)reader["B145_D30"],
                        B145_D31 = (Int32)reader["B145_D31"],
                        //END B145

                        //C100
                        C100_D1 = (Int32)reader["C100_D1"],
                        C100_D2 = (Int32)reader["C100_D2"],
                        C100_D3 = (Int32)reader["C100_D3"],
                        C100_D4 = (Int32)reader["C100_D4"],
                        C100_D5 = (Int32)reader["C100_D5"],
                        C100_D6 = (Int32)reader["C100_D6"],
                        C100_D7 = (Int32)reader["C100_D7"],
                        C100_D8 = (Int32)reader["C100_D8"],
                        C100_D9 = (Int32)reader["C100_D9"],
                        C100_D10 = (Int32)reader["C100_D10"],
                        C100_D11 = (Int32)reader["C100_D11"],
                        C100_D12 = (Int32)reader["C100_D12"],
                        C100_D13 = (Int32)reader["C100_D13"],
                        C100_D14 = (Int32)reader["C100_D14"],
                        C100_D15 = (Int32)reader["C100_D15"],
                        C100_D16 = (Int32)reader["C100_D16"],
                        C100_D17 = (Int32)reader["C100_D17"],
                        C100_D18 = (Int32)reader["C100_D18"],
                        C100_D19 = (Int32)reader["C100_D19"],
                        C100_D20 = (Int32)reader["C100_D20"],
                        C100_D21 = (Int32)reader["C100_D21"],
                        C100_D22 = (Int32)reader["C100_D22"],
                        C100_D23 = (Int32)reader["C100_D23"],
                        C100_D24 = (Int32)reader["C100_D24"],
                        C100_D25 = (Int32)reader["C100_D25"],
                        C100_D26 = (Int32)reader["C100_D26"],
                        C100_D27 = (Int32)reader["C100_D27"],
                        C100_D28 = (Int32)reader["C100_D28"],
                        C100_D29 = (Int32)reader["C100_D29"],
                        C100_D30 = (Int32)reader["C100_D30"],
                        C100_D31 = (Int32)reader["C100_D31"],


                        //END C100

                        //C155
                        C155_D1 = (Int32)reader["C155_D1"],
                        C155_D2 = (Int32)reader["C155_D2"],
                        C155_D3 = (Int32)reader["C155_D3"],
                        C155_D4 = (Int32)reader["C155_D4"],
                        C155_D5 = (Int32)reader["C155_D5"],
                        C155_D6 = (Int32)reader["C155_D6"],
                        C155_D7 = (Int32)reader["C155_D7"],
                        C155_D8 = (Int32)reader["C155_D8"],
                        C155_D9 = (Int32)reader["C155_D9"],
                        C155_D10 = (Int32)reader["C155_D10"],
                        C155_D11 = (Int32)reader["C155_D11"],
                        C155_D12 = (Int32)reader["C155_D12"],
                        C155_D13 = (Int32)reader["C155_D13"],
                        C155_D14 = (Int32)reader["C155_D14"],
                        C155_D15 = (Int32)reader["C155_D15"],
                        C155_D16 = (Int32)reader["C155_D16"],
                        C155_D17 = (Int32)reader["C155_D17"],
                        C155_D18 = (Int32)reader["C155_D18"],
                        C155_D19 = (Int32)reader["C155_D19"],
                        C155_D20 = (Int32)reader["C155_D20"],
                        C155_D21 = (Int32)reader["C155_D21"],
                        C155_D22 = (Int32)reader["C155_D22"],
                        C155_D23 = (Int32)reader["C155_D23"],
                        C155_D24 = (Int32)reader["C155_D24"],
                        C155_D25 = (Int32)reader["C155_D25"],
                        C155_D26 = (Int32)reader["C155_D26"],
                        C155_D27 = (Int32)reader["C155_D27"],
                        C155_D28 = (Int32)reader["C155_D28"],
                        C155_D29 = (Int32)reader["C155_D29"],
                        C155_D30 = (Int32)reader["C155_D30"],
                        C155_D31 = (Int32)reader["C155_D31"],

                        //END C155

                        //C330
                        C330_D1 = (Int32)reader["C330_D1"],
                        C330_D2 = (Int32)reader["C330_D2"],
                        C330_D3 = (Int32)reader["C330_D3"],
                        C330_D4 = (Int32)reader["C330_D4"],
                        C330_D5 = (Int32)reader["C330_D5"],
                        C330_D6 = (Int32)reader["C330_D6"],
                        C330_D7 = (Int32)reader["C330_D7"],
                        C330_D8 = (Int32)reader["C330_D8"],
                        C330_D9 = (Int32)reader["C330_D9"],
                        C330_D10 = (Int32)reader["C330_D10"],
                        C330_D11 = (Int32)reader["C330_D11"],
                        C330_D12 = (Int32)reader["C330_D12"],
                        C330_D13 = (Int32)reader["C330_D13"],
                        C330_D14 = (Int32)reader["C330_D14"],
                        C330_D15 = (Int32)reader["C330_D15"],
                        C330_D16 = (Int32)reader["C330_D16"],
                        C330_D17 = (Int32)reader["C330_D17"],
                        C330_D18 = (Int32)reader["C330_D18"],
                        C330_D19 = (Int32)reader["C330_D19"],
                        C330_D20 = (Int32)reader["C330_D20"],
                        C330_D21 = (Int32)reader["C330_D21"],
                        C330_D22 = (Int32)reader["C330_D22"],
                        C330_D23 = (Int32)reader["C330_D23"],
                        C330_D24 = (Int32)reader["C330_D24"],
                        C330_D25 = (Int32)reader["C330_D25"],
                        C330_D26 = (Int32)reader["C330_D26"],
                        C330_D27 = (Int32)reader["C330_D27"],
                        C330_D28 = (Int32)reader["C330_D28"],
                        C330_D29 = (Int32)reader["C330_D29"],
                        C330_D30 = (Int32)reader["C330_D30"],
                        C330_D31 = (Int32)reader["C330_D31"],

                        //END C330

                        //D90
                        D90_D1 = (Int32)reader["D90_D1"],
                        D90_D2 = (Int32)reader["D90_D2"],
                        D90_D3 = (Int32)reader["D90_D3"],
                        D90_D4 = (Int32)reader["D90_D4"],
                        D90_D5 = (Int32)reader["D90_D5"],
                        D90_D6 = (Int32)reader["D90_D6"],
                        D90_D7 = (Int32)reader["D90_D7"],
                        D90_D8 = (Int32)reader["D90_D8"],
                        D90_D9 = (Int32)reader["D90_D9"],
                        D90_D10 = (Int32)reader["D90_D10"],
                        D90_D11 = (Int32)reader["D90_D11"],
                        D90_D12 = (Int32)reader["D90_D12"],
                        D90_D13 = (Int32)reader["D90_D13"],
                        D90_D14 = (Int32)reader["D90_D14"],
                        D90_D15 = (Int32)reader["D90_D15"],
                        D90_D16 = (Int32)reader["D90_D16"],
                        D90_D17 = (Int32)reader["D90_D17"],
                        D90_D18 = (Int32)reader["D90_D18"],
                        D90_D19 = (Int32)reader["D90_D19"],
                        D90_D20 = (Int32)reader["D90_D20"],
                        D90_D21 = (Int32)reader["D90_D21"],
                        D90_D22 = (Int32)reader["D90_D22"],
                        D90_D23 = (Int32)reader["D90_D23"],
                        D90_D24 = (Int32)reader["D90_D24"],
                        D90_D25 = (Int32)reader["D90_D25"],
                        D90_D26 = (Int32)reader["D90_D26"],
                        D90_D27 = (Int32)reader["D90_D27"],
                        D90_D28 = (Int32)reader["D90_D28"],
                        D90_D29 = (Int32)reader["D90_D29"],
                        D90_D30 = (Int32)reader["D90_D30"],
                        D90_D31 = (Int32)reader["D90_D31"],

                        //END D90

                        //D110
                        D110_D1 = (Int32)reader["D110_D1"],
                        D110_D2 = (Int32)reader["D110_D2"],
                        D110_D3 = (Int32)reader["D110_D3"],
                        D110_D4 = (Int32)reader["D110_D4"],
                        D110_D5 = (Int32)reader["D110_D5"],
                        D110_D6 = (Int32)reader["D110_D6"],
                        D110_D7 = (Int32)reader["D110_D7"],
                        D110_D8 = (Int32)reader["D110_D8"],
                        D110_D9 = (Int32)reader["D110_D9"],
                        D110_D10 = (Int32)reader["D110_D10"],
                        D110_D11 = (Int32)reader["D110_D11"],
                        D110_D12 = (Int32)reader["D110_D12"],
                        D110_D13 = (Int32)reader["D110_D13"],
                        D110_D14 = (Int32)reader["D110_D14"],
                        D110_D15 = (Int32)reader["D110_D15"],
                        D110_D16 = (Int32)reader["D110_D16"],
                        D110_D17 = (Int32)reader["D110_D17"],
                        D110_D18 = (Int32)reader["D110_D18"],
                        D110_D19 = (Int32)reader["D110_D19"],
                        D110_D20 = (Int32)reader["D110_D20"],
                        D110_D21 = (Int32)reader["D110_D21"],
                        D110_D22 = (Int32)reader["D110_D22"],
                        D110_D23 = (Int32)reader["D110_D23"],
                        D110_D24 = (Int32)reader["D110_D24"],
                        D110_D25 = (Int32)reader["D110_D25"],
                        D110_D26 = (Int32)reader["D110_D26"],
                        D110_D27 = (Int32)reader["D110_D27"],
                        D110_D28 = (Int32)reader["D110_D28"],
                        D110_D29 = (Int32)reader["D110_D29"],
                        D110_D30 = (Int32)reader["D110_D30"],
                        D110_D31 = (Int32)reader["D110_D31"],

                        //END D110

                        //D135
                        D135_D1 = (Int32)reader["D135_D1"],
                        D135_D2 = (Int32)reader["D135_D2"],
                        D135_D3 = (Int32)reader["D135_D3"],
                        D135_D4 = (Int32)reader["D135_D4"],
                        D135_D5 = (Int32)reader["D135_D5"],
                        D135_D6 = (Int32)reader["D135_D6"],
                        D135_D7 = (Int32)reader["D135_D7"],
                        D135_D8 = (Int32)reader["D135_D8"],
                        D135_D9 = (Int32)reader["D135_D9"],
                        D135_D10 = (Int32)reader["D135_D10"],
                        D135_D11 = (Int32)reader["D135_D11"],
                        D135_D12 = (Int32)reader["D135_D12"],
                        D135_D13 = (Int32)reader["D135_D13"],
                        D135_D14 = (Int32)reader["D135_D14"],
                        D135_D15 = (Int32)reader["D135_D15"],
                        D135_D16 = (Int32)reader["D135_D16"],
                        D135_D17 = (Int32)reader["D135_D17"],
                        D135_D18 = (Int32)reader["D135_D18"],
                        D135_D19 = (Int32)reader["D135_D19"],
                        D135_D20 = (Int32)reader["D135_D20"],
                        D135_D21 = (Int32)reader["D135_D21"],
                        D135_D22 = (Int32)reader["D135_D22"],
                        D135_D23 = (Int32)reader["D135_D23"],
                        D135_D24 = (Int32)reader["D135_D24"],
                        D135_D25 = (Int32)reader["D135_D25"],
                        D135_D26 = (Int32)reader["D135_D26"],
                        D135_D27 = (Int32)reader["D135_D27"],
                        D135_D28 = (Int32)reader["D135_D28"],
                        D135_D29 = (Int32)reader["D135_D29"],
                        D135_D30 = (Int32)reader["D135_D30"],
                        D135_D31 = (Int32)reader["D135_D31"],

                        //END D135

                        //D450
                        D450_D1 = (Int32)reader["D450_D1"],
                        D450_D2 = (Int32)reader["D450_D2"],
                        D450_D3 = (Int32)reader["D450_D3"],
                        D450_D4 = (Int32)reader["D450_D4"],
                        D450_D5 = (Int32)reader["D450_D5"],
                        D450_D6 = (Int32)reader["D450_D6"],
                        D450_D7 = (Int32)reader["D450_D7"],
                        D450_D8 = (Int32)reader["D450_D8"],
                        D450_D9 = (Int32)reader["D450_D9"],
                        D450_D10 = (Int32)reader["D450_D10"],
                        D450_D11 = (Int32)reader["D450_D11"],
                        D450_D12 = (Int32)reader["D450_D12"],
                        D450_D13 = (Int32)reader["D450_D13"],
                        D450_D14 = (Int32)reader["D450_D14"],
                        D450_D15 = (Int32)reader["D450_D15"],
                        D450_D16 = (Int32)reader["D450_D16"],
                        D450_D17 = (Int32)reader["D450_D17"],
                        D450_D18 = (Int32)reader["D450_D18"],
                        D450_D19 = (Int32)reader["D450_D19"],
                        D450_D20 = (Int32)reader["D450_D20"],
                        D450_D21 = (Int32)reader["D450_D21"],
                        D450_D22 = (Int32)reader["D450_D22"],
                        D450_D23 = (Int32)reader["D450_D23"],
                        D450_D24 = (Int32)reader["D450_D24"],
                        D450_D25 = (Int32)reader["D450_D25"],
                        D450_D26 = (Int32)reader["D450_D26"],
                        D450_D27 = (Int32)reader["D450_D27"],
                        D450_D28 = (Int32)reader["D450_D28"],
                        D450_D29 = (Int32)reader["D450_D29"],
                        D450_D30 = (Int32)reader["D450_D30"],
                        D450_D31 = (Int32)reader["D450_D31"],

                        //END D450

                        //D500
                        D500_D1 = (Int32)reader["D500_D1"],
                        D500_D2 = (Int32)reader["D500_D2"],
                        D500_D3 = (Int32)reader["D500_D3"],
                        D500_D4 = (Int32)reader["D500_D4"],
                        D500_D5 = (Int32)reader["D500_D5"],
                        D500_D6 = (Int32)reader["D500_D6"],
                        D500_D7 = (Int32)reader["D500_D7"],
                        D500_D8 = (Int32)reader["D500_D8"],
                        D500_D9 = (Int32)reader["D500_D9"],
                        D500_D10 = (Int32)reader["D500_D10"],
                        D500_D11 = (Int32)reader["D500_D11"],
                        D500_D12 = (Int32)reader["D500_D12"],
                        D500_D13 = (Int32)reader["D500_D13"],
                        D500_D14 = (Int32)reader["D500_D14"],
                        D500_D15 = (Int32)reader["D500_D15"],
                        D500_D16 = (Int32)reader["D500_D16"],
                        D500_D17 = (Int32)reader["D500_D17"],
                        D500_D18 = (Int32)reader["D500_D18"],
                        D500_D19 = (Int32)reader["D500_D19"],
                        D500_D20 = (Int32)reader["D500_D20"],
                        D500_D21 = (Int32)reader["D500_D21"],
                        D500_D22 = (Int32)reader["D500_D22"],
                        D500_D23 = (Int32)reader["D500_D23"],
                        D500_D24 = (Int32)reader["D500_D24"],
                        D500_D25 = (Int32)reader["D500_D25"],
                        D500_D26 = (Int32)reader["D500_D26"],
                        D500_D27 = (Int32)reader["D500_D27"],
                        D500_D28 = (Int32)reader["D500_D28"],
                        D500_D29 = (Int32)reader["D500_D29"],
                        D500_D30 = (Int32)reader["D500_D30"],
                        D500_D31 = (Int32)reader["D500_D31"],

                        //END D500

                        #region Comment
                        //D1 = (int)reader["D1"],
                        //D2 = (int)reader["D2"],
                        //D3 = (int)reader["D3"],
                        //D4 = (int)reader["D4"],
                        //D5 = (int)reader["D5"],
                        //D6 = (int)reader["D6"],
                        //D7 = (int)reader["D7"],
                        //D8 = (int)reader["D8"],
                        //D9 = (int)reader["D9"],
                        //D10 = (int)reader["D10"],
                        //D11 = (int)reader["D11"],
                        //D12 = (int)reader["D12"],
                        //D13 = (int)reader["D13"],
                        //D14 = (int)reader["D14"],
                        //D15 = (int)reader["D15"],
                        //D16 = (int)reader["D16"],
                        //D17 = (int)reader["D17"],
                        //D18 = (int)reader["D18"],
                        //D19 = (int)reader["D19"],
                        //D20 = (int)reader["D20"],
                        //D21 = (int)reader["D21"],
                        //D22 = (int)reader["D22"],
                        //D23 = (int)reader["D23"],
                        //D24 = (int)reader["D24"],
                        //D25 = (int)reader["D25"],
                        //D26 = (int)reader["D26"],
                        //D27 = (int)reader["D27"],
                        //D28 = (int)reader["D28"],
                        //D29 = (int)reader["D29"],
                        //D30 = (int)reader["D30"],
                        //D31 = (int)reader["D31"],
                        //Product_group_ID = reader["Product_group_ID"].ToString(),                        
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //TOTAL_POINT = (int)reader["TOTAL_POINT"],
                        #endregion

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
    //SP_Point
    public static List<RPT_SP_POINT_41225> RPT_SP_POINT_41225(String Region, String CV_Code, string StartMonth, string EndMonth, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SP_POINT_41225";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(StartMonth))
        {
            selectCommand.Parameters.AddWithValue("@StartMonth", StartMonth);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartMonth", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndMonth))
        {
            selectCommand.Parameters.AddWithValue("@EndMonth", EndMonth);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndMont", DBNull.Value);
        }
        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SP_POINT_41225> item = new List<RPT_SP_POINT_41225>();
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
                    item.Add(new RPT_SP_POINT_41225
                    {
                        #region
                        //Agent_Name = reader["Agent_Name"].ToString(),
                        //Product_group_id = reader["Product_group_id"].ToString(),
                        //Region_Name = reader["Region_Name"].ToString(),
                        //Unit = reader["Unit"].ToString(),
                        //Month = (int)reader["Month"],
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //TOTAL = (decimal)reader["TOTAL"],
                        //Year = (int)reader["Year"],
                        //CV_Code = reader["CV_Code"].ToString(),
                        //Temp0 = reader["Temp0"].ToString(),
                        //Temp1 = reader["Temp1"].ToString(),
                        //Temp2 = reader["Temp2"].ToString(),
                        //Temp3 = reader["Temp3"].ToString(),
                        //Temp4 = reader["Temp4"].ToString(),
                        //Temp5 = reader["Temp5"].ToString(),
                        #endregion
                        Region = reader["Region"] is DBNull ? null : reader["Region"].ToString(),
                        Region_Name = reader["Region_Name"] is DBNull ? null : reader["Region_Name"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"] is DBNull ? null : reader["CV_Name"].ToString(),
                        Address = reader["Address"] is DBNull ? null : reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"] is DBNull ? null : reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"] is DBNull ? null : reader["TAXID"].ToString(),

                        SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString(),
                        PS_Name = reader["PS_Name"] is DBNull ? null : reader["PS_Name"].ToString(),
                        Date = reader["Date"] is DBNull ? null : reader["Date"].ToString(),
                        Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Sales_Qty = (reader["Sales_Qty"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Sales_Qty"].ToString())),
                        Point = (reader["Point"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Point"].ToString())),
                        Tota_Point = (reader["Tota_Point"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Tota_Point"].ToString())),

                        Temp0 = reader["Temp0"] is DBNull ? null : reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"] is DBNull ? null : reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"] is DBNull ? null : reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"] is DBNull ? null : reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"] is DBNull ? null : reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"] is DBNull ? null : reader["Temp5"].ToString(),

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
    //RPT_SUMM_SP_BY_AGENT_41215
    public static List<RPT_SUMM_SP_BY_AGENT_41215> RPT_SUMM_SP_BY_AGENT_41215(String Region, String CV_Code, string Status, string Position, string StartDate, string EndDate, string StartAge, string EndAge, string Temp0, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SUMM_SP_BY_AGENT_41215";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Status))
        {
            selectCommand.Parameters.AddWithValue("@Status", Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Position))
        {
            selectCommand.Parameters.AddWithValue("@Position", Position);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Position", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", "01/01/2520");
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", "01/01/2580");
        }
        if (!String.IsNullOrEmpty(StartAge))
        {
            selectCommand.Parameters.AddWithValue("@StartAge", StartAge);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartAge", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndAge))
        {
            selectCommand.Parameters.AddWithValue("@EndAge", EndAge);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndAge", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(Temp0))
        {
            selectCommand.Parameters.AddWithValue("@Temp0", Temp0);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SUMM_SP_BY_AGENT_41215> item = new List<RPT_SUMM_SP_BY_AGENT_41215>();
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
                    item.Add(new RPT_SUMM_SP_BY_AGENT_41215
                    {
                        #region
                        //Agent_Name = reader["Agent_Name"].ToString(),
                        //Product_group_id = reader["Product_group_id"].ToString(),
                        //Region_Name = reader["Region_Name"].ToString(),
                        //Unit = reader["Unit"].ToString(),
                        //Month = (int)reader["Month"],
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //TOTAL = (decimal)reader["TOTAL"],
                        //Year = (int)reader["Year"],
                        //CV_Code = reader["CV_Code"].ToString(),
                        //Temp0 = reader["Temp0"].ToString(),
                        //Temp1 = reader["Temp1"].ToString(),
                        //Temp2 = reader["Temp2"].ToString(),
                        //Temp3 = reader["Temp3"].ToString(),
                        //Temp4 = reader["Temp4"].ToString(),
                        //Temp5 = reader["Temp5"].ToString(),
                        #endregion
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        SP_ID = reader["SP_ID"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Join_Date = reader["Join_Date"].ToString(),
                        Position = reader["Position"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        Status = reader["Status"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
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
    //RPT_SUMM_SO_PG_41214
    public static List<RPT_SUMM_SO_PG_41214> RPT_SUMM_SO_PG_41214(String Region, string CV_Code, String SP_ID, string StartDate, string EndDate, string PGroup, string Size, string Temp0, string Temp1, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SUMM_SO_PG_41214";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }
        //@PGroup
        if (!String.IsNullOrEmpty(PGroup))
        {
            selectCommand.Parameters.AddWithValue("@PGroup", PGroup);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PGroup", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Size))
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SUMM_SO_PG_41214> item = new List<RPT_SUMM_SO_PG_41214>();
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
                    item.Add(new RPT_SUMM_SO_PG_41214
                    {
                        #region
                        //Agent_Name = reader["Agent_Name"].ToString(),
                        //Product_group_id = reader["Product_group_id"].ToString(),
                        //Region_Name = reader["Region_Name"].ToString(),
                        //Unit = reader["Unit"].ToString(),
                        //Month = (int)reader["Month"],
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //TOTAL = (decimal)reader["TOTAL"],
                        //Year = (int)reader["Year"],
                        //CV_Code = reader["CV_Code"].ToString(),
                        //Temp0 = reader["Temp0"].ToString(),
                        //Temp1 = reader["Temp1"].ToString(),
                        //Temp2 = reader["Temp2"].ToString(),
                        //Temp3 = reader["Temp3"].ToString(),
                        //Temp4 = reader["Temp4"].ToString(),
                        //Temp5 = reader["Temp5"].ToString(),
                        #endregion
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        //SP_ID = reader["SP_ID"].ToString(),
                        //Address = reader["Address"].ToString(),
                        //CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        //SP_Name = reader["SP_Name"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        //TAXID = reader["TAXID"].ToString(),
                        //Temp0 = reader["Temp0"].ToString(),
                        //Temp1 = reader["Temp1"].ToString(),
                        //Temp2 = reader["Temp2"].ToString(),
                        //Temp3 = reader["Temp3"].ToString(),
                        //Temp4 = reader["Temp4"].ToString(),
                        //Temp5 = reader["Temp5"].ToString(),
                        EndDate = reader["EndDate"].ToString(),
                        Prod_Group = reader["Prod_Group"].ToString(),
                        QTY = (int)reader["QTY"],
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Total_Amt = (decimal)reader["Total_Amt"],

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
    //RPT_SUMM_RQ_SP_4126
    public static List<RPT_SUMM_RQ_SP_4126> RPT_SUMM_RQ_SP_4126(string Region, string CV_Code, string SP_ID, string StartDate, string EndDate, string PGroup, string Size, string Temp0, string Temp1, string UserID)
    {
        //logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SUMM_RQ_SP_4126";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", "01/01/2559");
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", "01/01/2580");
        }
        //@PGroup	
        if (!String.IsNullOrEmpty(PGroup))
        {
            selectCommand.Parameters.AddWithValue("@PGroup", PGroup);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PGroup", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Size))
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SUMM_RQ_SP_4126> item = new List<RPT_SUMM_RQ_SP_4126>();
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
                    item.Add(new RPT_SUMM_RQ_SP_4126
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        SP_ID = reader["SP_ID"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        EndDate = reader["EndDate"].ToString(),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Total_Amt = (decimal)reader["Total_Amt"],
                        ProductGroup = reader["ProductGroup"].ToString(),
                        Product_Name = reader["Product_Name"].ToString(),
                        Proudct_ID = reader["Proudct_ID"].ToString(),
                        RQ_QTY = (reader["RQ_QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["RQ_QTY"].ToString())),
                        SP_Price = (decimal)reader["SP_Price"],

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
    //RPT_SUMM_RQ_OTHER__4129
    public static List<RPT_SUMM_RQ_OTHER__4129> RPT_SUMM_RQ_OTHER__4129(String Region, string CV_Code, string StartDate, string EndDate, string Prod_ID, string USRQ_ID, string Reason, string Temp0, string Temp1, string UserID)
    {
        // logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SUMM_RQ_OTHER__4129";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", "01/01/2559");
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", "01/01/2580");
        }
        if (!String.IsNullOrEmpty(Prod_ID))
        {
            selectCommand.Parameters.AddWithValue("@Prod_ID", Prod_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Prod_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(USRQ_ID))
        {
            selectCommand.Parameters.AddWithValue("@USRQ_ID", USRQ_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@USRQ_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Reason))
        {
            selectCommand.Parameters.AddWithValue("@Reason", Reason);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Reason", DBNull.Value);
        }
        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SUMM_RQ_OTHER__4129> item = new List<RPT_SUMM_RQ_OTHER__4129>();
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
                    item.Add(new RPT_SUMM_RQ_OTHER__4129
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        EndDate = reader["EndDate"].ToString(),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Product_Name = reader["Product_Name"].ToString(),
                        Proudct_ID = reader["Proudct_ID"].ToString(),
                        QTY = (reader["QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["QTY"].ToString())),
                        Reason = reader["Reason"].ToString(),
                        Remark = reader["Remark"].ToString(),
                        Request_Date = reader["Request_Date"].ToString(),
                        Unit = reader["Unit"].ToString(),
                        User_Name = reader["User_Name"].ToString(),

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
    //RPT_SUMM_MEMBER__41210
    public static List<RPT_SUMM_MEMBER__41210> RPT_SUMM_MEMBER__41210(String Region, string CV_Code, string Date, string Type, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SUMM_MEMBER__41210";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(Date))
        {
            selectCommand.Parameters.AddWithValue("@Date", Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Date", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Type))
        {
            selectCommand.Parameters.AddWithValue("@Type", Type);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Type", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);

        if (!String.IsNullOrEmpty(UserID))
        {
            selectCommand.Parameters.AddWithValue("@UserID", UserID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SUMM_MEMBER__41210> item = new List<RPT_SUMM_MEMBER__41210>();
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
                    item.Add(new RPT_SUMM_MEMBER__41210
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        //Address = reader["Address"].ToString(),
                        //CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        //TAXID = reader["TAXID"].ToString(),
                        //Temp0 = reader["Temp0"].ToString(),
                        //Temp1 = reader["Temp1"].ToString(),
                        //Temp2 = reader["Temp2"].ToString(),
                        //Temp3 = reader["Temp3"].ToString(),
                        //Temp4 = reader["Temp4"].ToString(),
                        //Temp5 = reader["Temp5"].ToString(),
                        Connecting = (int)reader["Connecting"],
                        DisConnected = (int)reader["DisConnected"],
                        DisConnect_Temporary = (int)reader["DisConnect_Temporary"],
                        Type = reader["Type"].ToString(),
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
    //RPT_SUMM_EXPENSE_41219
    public static List<RPT_SUMM_EXPENSE_41219> RPT_SUMM_EXPENSE_41219(String Region, string CV_Code, string StartDate, string EndDate, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SUMM_EXPENSE_41219";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SUMM_EXPENSE_41219> item = new List<RPT_SUMM_EXPENSE_41219>();
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
                    item.Add(new RPT_SUMM_EXPENSE_41219
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        ACC_ID = reader["ACC_ID"].ToString(),
                        ACC_Name = reader["ACC_Name"].ToString(),
                        Exp_Amt = (decimal)reader["Exp_Amt"],
                        //Exp_Date = reader["Exp_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Exp_Date"].ToString()),
                        Exp_Date = reader["Exp_Date"].ToString(),
                        Exp_No = reader["Exp_No"].ToString(),
                        Post_No = reader["Post_No"].ToString(),
                        Rev_Amt = (decimal)reader["Rev_Amt"],

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
    //RPT_STOCK_MOV_4127
    public static List<RPT_STOCK_MOV_4127> RPT_STOCK_MOV_4127(String Region, string CV_Code, string StartDate, string EndDate, string PGroup, string Size, string Temp0, string Temp1, string Temp2, string UserID)
    {
        //logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_STOCK_MOV_4127";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", "01/01/2559");
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", "01/01/2580");
        }
        if (!String.IsNullOrEmpty(PGroup))
        {
            selectCommand.Parameters.AddWithValue("@PGroup", PGroup);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PGroup", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Size))
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);


        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_STOCK_MOV_4127> item = new List<RPT_STOCK_MOV_4127>();
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
                    item.Add(new RPT_STOCK_MOV_4127
                    {
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"] is DBNull ? null : reader["CV_Name"].ToString(),
                        Address = reader["Address"] is DBNull ? null : reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"] is DBNull ? null : reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"] is DBNull ? null : reader["Region_Name"].ToString(),
                        TAXID = reader["TAXID"] is DBNull ? null : reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"] is DBNull ? null : reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"] is DBNull ? null : reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"] is DBNull ? null : reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"] is DBNull ? null : reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"] is DBNull ? null : reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"] is DBNull ? null : reader["Temp5"].ToString(),
                        //BF_QTY = (reader["BF_QTY"] is DBNull ? Int32.Parse("0") : Int32.Parse(reader["BF_QTY"].ToString())),//(int)reader["BF_QTY"],
                        BF_QTY = (reader["BF_QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["BF_QTY"].ToString())),//(int)reader["BF_QTY"],
                        CF_QTY = (reader["CF_QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["CF_QTY"].ToString())),
                        EndDate = reader["EndDate"] is DBNull ? null : reader["EndDate"].ToString(),
                        IN = (reader["IN"] is DBNull ? Int32.Parse("0") : Int32.Parse(reader["IN"].ToString())),
                        OUT_OTHER = (reader["OUT_OTHER"] is DBNull ? Int32.Parse("0") : Int32.Parse(reader["OUT_OTHER"].ToString())),
                        OUT_RQ = (reader["OUT_RQ"] is DBNull ? Int32.Parse("0") : Int32.Parse(reader["OUT_RQ"].ToString())),
                        ProductGroup = reader["ProductGroup"] is DBNull ? null : reader["ProductGroup"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Proudct_ID = reader["Proudct_ID"] is DBNull ? null : reader["Proudct_ID"].ToString(),
                        Remark = reader["Remark"] is DBNull ? null : reader["Remark"].ToString(),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        StartDate = reader["StartDate"] is DBNull ? null : reader["StartDate"].ToString(),

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
    //RPT_SP_DEBT_41221
    public static List<RPT_SP_DEBT_41221> RPT_SP_DEBT_41221(String Region, string CV_Code, string SP_ID, string Debt_ID, string Debt_Name, string StartDate, string EndDate, string Status, string Temp0, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SP_DEBT_41221";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Debt_ID))
        {
            selectCommand.Parameters.AddWithValue("@Debt_ID", Debt_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Debt_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Debt_Name))
        {
            selectCommand.Parameters.AddWithValue("@Debt_Name", Debt_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Debt_Name", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Status))
        {
            selectCommand.Parameters.AddWithValue("@Status", Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }
        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SP_DEBT_41221> item = new List<RPT_SP_DEBT_41221>();
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
                    item.Add(new RPT_SP_DEBT_41221
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Debt_Amt = (decimal)reader["Debt_Amt"],
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
                        Debt_EndDate = reader["Debt_EndDate"].ToString(),
                        Debt_ID = reader["Debt_ID"].ToString(),
                        Debt_Name = reader["Debt_Name"].ToString(),
                        Debt_StartDate = reader["Debt_StartDate"].ToString(),
                        Outstanding_Amt = (decimal)reader["Outstanding_Amt"],
                        Pay_Amt = (decimal)reader["Pay_Amt"],
                        SP_ID = reader["SP_ID"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        Status = reader["Status"].ToString(),
                        Debt_Date = reader["Debt_Date"].ToString(),
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
    //RPT_SP_COMMISSION_41216
    public static List<RPT_SP_COMMISSION_41216> RPT_SP_COMMISSION_41216(String Region, string CV_Code, string SP_ID, string StartDate, string EndDate, string PGroup, string Size, string Temp0, string Temp1, string UserID)
    {
        // logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SP_COMMISSION_41216";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(PGroup))
        {
            selectCommand.Parameters.AddWithValue("@PGroup", PGroup);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PGroup", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Size))
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp0))
        {
            selectCommand.Parameters.AddWithValue("@Temp0", Temp0);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        }


        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SP_COMMISSION_41216> item = new List<RPT_SP_COMMISSION_41216>();
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
                    item.Add(new RPT_SP_COMMISSION_41216
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Comm_Amt = (decimal)reader["Comm_Amt"],
                        EndDate = reader["EndDate"].ToString(),
                        Product_Group = reader["Product_Group"].ToString(),
                        Prod_ID = reader["Prod_ID"].ToString(),
                        Prod_Name = reader["Prod_Name"].ToString(),
                        QTY = (reader["QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["QTY"].ToString())),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        StartDate = reader["StartDate"].ToString(),

                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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

    //RPT_SO_TARGET_YEAR_4125
    public static List<RPT_SO_TARGET_YEAR_4125> RPT_SO_TARGET_YEAR_4125(String Region, string CV_Code, string Year, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string Temp5, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SO_TARGET_YEAR_4125";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Year))
        {
            selectCommand.Parameters.AddWithValue("@Year", Year);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SO_TARGET_YEAR_4125> item = new List<RPT_SO_TARGET_YEAR_4125>();
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

                    RPT_SO_TARGET_YEAR_4125 r = new RPT_SO_TARGET_YEAR_4125();
                    r.Region_Name = reader["Region_Name"].ToString();
                    r.CV_Code = reader["CV_Code"].ToString();
                    r.CV_Name = reader["CV_Name"].ToString();
                    r.DataType = reader["Type"].ToString();
                    r.Year = reader["Year"].ToString();

                    r.M1_Target = reader["M1_Target"] is DBNull ? 0 : (decimal)reader["M1_Target"];
                    r.M2_Target = reader["M2_Target"] is DBNull ? 0 : (decimal)reader["M2_Target"];
                    r.M3_Target = reader["M3_Target"] is DBNull ? 0 : (decimal)reader["M3_Target"];
                    r.M4_Target = reader["M4_Target"] is DBNull ? 0 : (decimal)reader["M4_Target"];
                    r.M5_Target = reader["M5_Target"] is DBNull ? 0 : (decimal)reader["M5_Target"];
                    r.M6_Target = reader["M6_Target"] is DBNull ? 0 : (decimal)reader["M6_Target"];
                    r.M7_Target = reader["M7_Target"] is DBNull ? 0 : (decimal)reader["M7_Target"];
                    r.M8_Target = reader["M8_Target"] is DBNull ? 0 : (decimal)reader["M8_Target"];
                    r.M9_Target = reader["M9_Target"] is DBNull ? 0 : (decimal)reader["M9_Target"];
                    r.M10_Target = reader["M10_Target"] is DBNull ? 0 : (decimal)reader["M10_Target"];
                    r.M11_Target = reader["M11_Target"] is DBNull ? 0 : (decimal)reader["M11_Target"];
                    r.M12_Target = reader["M12_Target"] is DBNull ? 0 : (decimal)reader["M12_Target"];
                    r.M1_Act = reader["M1_Act"] is DBNull ? 0 : (decimal)reader["M1_Act"];
                    r.M2_Act = reader["M2_Act"] is DBNull ? 0 : (decimal)reader["M2_Act"];
                    r.M3_Act = reader["M3_Act"] is DBNull ? 0 : (decimal)reader["M3_Act"];
                    r.M4_Act = reader["M4_Act"] is DBNull ? 0 : (decimal)reader["M4_Act"];
                    r.M5_Act = reader["M5_Act"] is DBNull ? 0 : (decimal)reader["M5_Act"];
                    r.M6_Act = reader["M6_Act"] is DBNull ? 0 : (decimal)reader["M6_Act"];
                    r.M7_Act = reader["M7_Act"] is DBNull ? 0 : (decimal)reader["M7_Act"];
                    r.M8_Act = reader["M8_Act"] is DBNull ? 0 : (decimal)reader["M8_Act"];
                    r.M9_Act = reader["M9_Act"] is DBNull ? 0 : (decimal)reader["M9_Act"];
                    r.M10_Act = reader["M10_Act"] is DBNull ? 0 : (decimal)reader["M10_Act"];
                    r.M11_Act = reader["M11_Act"] is DBNull ? 0 : (decimal)reader["M11_Act"];
                    r.M12_Act = reader["M12_Act"] is DBNull ? 0 : (decimal)reader["M12_Act"];
                    item.Add(r);

                    //item.Add(new RPT_SO_TARGET_YEAR_4125
                    //{
                    //    Region_Name = reader["Region_Name"].ToString(),
                    //    CV_Code = reader["CV_Code"].ToString(),
                    //    CV_Name = reader["CV_Name"].ToString(),
                    //    DataType = reader["Type"].ToString(),

                    //    if (DataType == "Target") {

                    //    }

                    //Address = reader["Address"].ToString(),
                    //CV_Phone = reader["CV_Phone"].ToString(),                        
                    //TAXID = reader["TAXID"].ToString(),
                    //Temp0 = reader["Temp0"].ToString(),
                    //Temp1 = reader["Temp1"].ToString(),
                    //Temp2 = reader["Temp2"].ToString(),
                    //Temp3 = reader["Temp3"].ToString(),
                    //Temp4 = reader["Temp4"].ToString(),
                    //Temp5 = reader["Temp5"].ToString(),
                    //Diff_Amount = (decimal)reader["Diff_Amount"],
                    //Diff_Percent = (int)reader["Diff_Percent"],
                    //Month = reader["Month"].ToString(),
                    //SO_Amount = (decimal)reader["SO_Amount"],
                    //Target = (decimal)reader["Target"],
                    //Year = reader["Year"].ToString(),
                    //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
                    //});
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
    //RPT_SO_TARGET_QUARTER_4124
    public static List<RPT_SO_TARGET_QUARTER_4124> RPT_SO_TARGET_QUARTER_4124(String Region, string CV_Code, string Quarter, string Year, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SO_TARGET_QUARTER_4124";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Quarter))
        {
            selectCommand.Parameters.AddWithValue("@Quarter", Quarter);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Quarter", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Year))
        {
            selectCommand.Parameters.AddWithValue("@Year", Year);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SO_TARGET_QUARTER_4124> item = new List<RPT_SO_TARGET_QUARTER_4124>();
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
                    item.Add(new RPT_SO_TARGET_QUARTER_4124
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Diff_Amount = (decimal)reader["Diff_Amount"],
                        Diff_Percent = (int)reader["Diff_Percent"],
                        Month = reader["Month"].ToString(),
                        SO_Amount = (decimal)reader["SO_Amount"],
                        Target = (decimal)reader["Target"],
                        Year = reader["Year"].ToString(),

                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_SO_TARGET_MONTH_4123
    public static List<RPT_SO_TARGET_MONTH_4123> RPT_SO_TARGET_MONTH_4123(String Region, string CV_Code, string Month, string Year, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SO_TARGET_MONTH_4123";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Month))
        {
            selectCommand.Parameters.AddWithValue("@Month", Month);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Month", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Year))
        {
            selectCommand.Parameters.AddWithValue("@Year", Year);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SO_TARGET_MONTH_4123> item = new List<RPT_SO_TARGET_MONTH_4123>();
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
                    item.Add(new RPT_SO_TARGET_MONTH_4123
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Region_Name = reader["Region_Name"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Diff_Amount = (decimal)reader["Diff_Amount"],
                        Diff_Percent = (int)reader["Diff_Percent"],
                        SO_Amount = (decimal)reader["SO_Amount"],
                        Target = (decimal)reader["Target"],
                        Year = reader["Year"].ToString(),


                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_SO_BY_PRODUCT_4122
    public static List<RPT_SO_BY_PRODUCT_4122> RPT_SO_BY_PRODUCT_4122(String Region, string CV_Code, string StartDate, string EndDate, string PGroup, string Size, string PID, string ProductName, string Temp1, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SO_BY_PRODUCT_4122";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", "01/01/2559");
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", "01/01/2580");
        }
        if (!String.IsNullOrEmpty(PGroup))
        {
            selectCommand.Parameters.AddWithValue("@PGroup", PGroup);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PGroup", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Size))
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(PID))
        {
            selectCommand.Parameters.AddWithValue("@PID", PID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(ProductName))
        {
            selectCommand.Parameters.AddWithValue("@Temp0", ProductName);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        }

        //selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SO_BY_PRODUCT_4122> item = new List<RPT_SO_BY_PRODUCT_4122>();
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
                    item.Add(new RPT_SO_BY_PRODUCT_4122
                    {

                        Region_Name = reader["Region_Name"].ToString(),
                        //Temp0 = reader["Temp0"].ToString(),
                        //Temp1 = reader["Temp1"].ToString(),
                        //Temp2 = reader["Temp2"].ToString(),
                        //Temp3 = reader["Temp3"].ToString(),
                        //Temp4 = reader["Temp4"].ToString(),
                        //Temp5 = reader["Temp5"].ToString(),
                        //Temp6 = reader["Temp6"].ToString(),
                        //Temp7 = reader["Temp7"].ToString(),
                        //Temp8 = reader["Temp5"].ToString(),
                        //Year = (int)reader["Year"],
                        Agent_Name = reader["Agent_Name"].ToString(),
                        CV_CODE = reader["CV_CODE"].ToString(),
                        //Month = (int)reader["Month"],
                        Product_group_id = reader["Product_group_id"].ToString(),
                        QTY = (int)reader["QTY"],
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Total_AMT = (decimal)reader["Total_AMT"],
                        Unit = reader["Unit"].ToString(),
                        PID = reader["PID"].ToString(),
                        PName = reader["PName"].ToString(),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_SALE_AMT_DAILY_41220A
    public static List<RPT_SALE_AMT_DAILY_41220A> RPT_SALE_AMT_DAILY_41220A(String Region, string CV_Code, string StartDate, string EndDate, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_SALE_AMT_DAILY_41220A";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", "1/01/2560");
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", "1/01/2560");
        }


        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_SALE_AMT_DAILY_41220A> item = new List<RPT_SALE_AMT_DAILY_41220A>();
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
                    item.Add(new RPT_SALE_AMT_DAILY_41220A
                    {

                        Region_Name = reader["Region_Name"].ToString(),
                        Temp0 = string.Empty,
                        Temp1 = string.Empty,
                        Temp2 = string.Empty,
                        Temp3 = string.Empty,
                        Temp4 = string.Empty,
                        Temp5 = string.Empty,
                        Size = string.Empty,
                        Address = string.Empty,
                        Product_Group = reader["Product_Group_ID"].ToString(),
                        Product_ID = string.Empty,
                        Product_Name = string.Empty,
                        SP_ID = reader["SP_ID"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        TAXID = string.Empty,
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["Agent_Name"].ToString(),
                        CV_Phone = string.Empty,

                        Sales_Amt = 0,
                        Cash_Amt = reader["SP_Cash"] is DBNull ? 0 : (decimal)reader["SP_Cash"],
                        Pay_Amt = reader["SP_Credit"] is DBNull ? 0 : (decimal)reader["SP_Credit"],
                        Confirm_Pay_Amt = reader["Actual_Pay"] is DBNull ? 0 : (decimal)reader["Actual_Pay"],
                        Current_Balance = reader["Balance_ost"] is DBNull ? 0 : (decimal)reader["Balance_ost"],
                        Commission = reader["Total_Commission"] is DBNull ? 0 : (decimal)reader["Total_Commission"],
                        REV_Agent = reader["Rev_Agent"] is DBNull ? 0 : (decimal)reader["Rev_Agent"],
                        Cash_deposit = reader["SP_Deposit"] is DBNull ? 0 : (decimal)reader["SP_Deposit"],
                        Car_Rent = reader["Car_Rent"] is DBNull ? 0 : (decimal)reader["Car_Rent"],
                        Installment = reader["Installation"] is DBNull ? 0 : (decimal)reader["Installation"],
                        Other = reader["Other"] is DBNull ? 0 : (decimal)reader["Other"],
                        Remark = reader["Remark"].ToString(),

                        // Sales_Amt= reader["SP_Cash"] is DBNull ? 0 : (decimal)reader["SP_Cash"],
                        //Create_Date = reader["Create_Date"].ToString(),

                        // Sales_Amt = (reader["Sales_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Sales_Amt"].ToString())),

                        SizeA1 = reader["SizeA1"] is DBNull ? 0 : (decimal)reader["SizeA1"],
                        SizeA2 = reader["SizeA2"] is DBNull ? 0 : (decimal)reader["SizeA2"],
                        SizeA3 = reader["SizeA3"] is DBNull ? 0 : (decimal)reader["SizeA3"],
                        SizeA4 = reader["SizeA4"] is DBNull ? 0 : (decimal)reader["SizeA4"],
                        SizeA5 = reader["SizeA5"] is DBNull ? 0 : (decimal)reader["SizeA5"],
                        SizeA6 = reader["SizeA6"] is DBNull ? 0 : (decimal)reader["SizeA6"],
                        SizeA7 = reader["SizeA7"] is DBNull ? 0 : (decimal)reader["SizeA7"],
                        SizeA8 = reader["SizeA8"] is DBNull ? 0 : (decimal)reader["SizeA8"],
                        SizeA9 = reader["SizeA9"] is DBNull ? 0 : (decimal)reader["SizeA9"],
                        SizeA10 = reader["SizeA10"] is DBNull ? 0 : (decimal)reader["SizeA10"],
                        SizeA11 = reader["SizeA11"] is DBNull ? 0 : (decimal)reader["SizeA11"],
                        SizeA12 = reader["SizeA12"] is DBNull ? 0 : (decimal)reader["SizeA12"],
                        SizeA13 = reader["SizeA13"] is DBNull ? 0 : (decimal)reader["SizeA13"],
                        SizeA14 = reader["SizeA14"] is DBNull ? 0 : (decimal)reader["SizeA14"],
                        SizeA15 = reader["SizeA15"] is DBNull ? 0 : (decimal)reader["SizeA15"],
                        SizeA16 = reader["SizeA16"] is DBNull ? 0 : (decimal)reader["SizeA16"],
                        SizeA17 = reader["SizeA17"] is DBNull ? 0 : (decimal)reader["SizeA17"],
                        SizeA18 = reader["SizeA18"] is DBNull ? 0 : (decimal)reader["SizeA18"],
                        SizeA19 = reader["SizeA19"] is DBNull ? 0 : (decimal)reader["SizeA19"],
                        SizeA20 = reader["SizeA20"] is DBNull ? 0 : (decimal)reader["SizeA20"],
                        SizeA21 = reader["SizeA21"] is DBNull ? 0 : (decimal)reader["SizeA21"],
                        SizeA22 = reader["SizeA22"] is DBNull ? 0 : (decimal)reader["SizeA22"],
                        SizeA23 = reader["SizeA23"] is DBNull ? 0 : (decimal)reader["SizeA23"],
                        SizeA24 = reader["SizeA24"] is DBNull ? 0 : (decimal)reader["SizeA24"],

                        Total_Other_Expense = reader["Total_Other_Expense"] is DBNull ? 0 : (decimal)reader["Total_Other_Expense"],

                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())), 
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_DN_CN_41224
    public static List<RPT_DN_CN_41224> RPT_DN_CN_41224(String Region, string CV_Code, string SP_ID, string StartDate, string EndDate, string Type, string DNCN_No, string Status, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_DN_CN_41224";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Type))
        {
            selectCommand.Parameters.AddWithValue("@Type", Type);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Type", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(DNCN_No))
        {
            selectCommand.Parameters.AddWithValue("@DNCN_No", DNCN_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@DNCN_No", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Status))
        {
            selectCommand.Parameters.AddWithValue("@Status", Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }
        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_DN_CN_41224> item = new List<RPT_DN_CN_41224>();
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
                    item.Add(new RPT_DN_CN_41224
                    {

                        Region_Name = reader["Region_Name"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        DocDate = reader["DocDate"].ToString(),
                        DocNo = reader["DocNo"].ToString(),
                        EndDate = reader["EndDate"].ToString(),
                        INV_Date = reader["INV_Date"].ToString(),
                        INV_NO = reader["INV_NO"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        Status = reader["Status"].ToString(),
                        Total = (decimal)reader["Total"],
                        Type = reader["Type"].ToString(),

                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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

    //RPT_Customer_PAY_TYPE_41212
    public static List<RPT_Customer_PAY_TYPE_41212> RPT_Customer_PAY_TYPE_41212(String Region, string CV_Code, string CustType, string CustName, string SP, string ResidentType, string Status, string PayType, string Temp0, string Temp1, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Customer_PAY_TYPE_41212";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CustType))
        {
            selectCommand.Parameters.AddWithValue("@CustID", CustType);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CustID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CustName))
        {
            selectCommand.Parameters.AddWithValue("@CustName", CustName);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CustName", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP))
        {
            selectCommand.Parameters.AddWithValue("@SP", SP);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Status))
        {
            selectCommand.Parameters.AddWithValue("@Status", Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(PayType))
        {
            selectCommand.Parameters.AddWithValue("@PayType", PayType);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PayType", DBNull.Value);
        }
        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_Customer_PAY_TYPE_41212> item = new List<RPT_Customer_PAY_TYPE_41212>();
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
                    item.Add(new RPT_Customer_PAY_TYPE_41212
                    {

                        Region_Name = reader["Region_Name"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Status = reader["Status"].ToString(),
                        Bill_Date = reader["Bill_Date"].ToString(),
                        Bill_INFO = reader["Bill_INFO"].ToString(),
                        Customer_Name = reader["Customer_Name"].ToString(),
                        Cust_ID = reader["Cust_ID"].ToString(),
                        PayType = reader["PayType"].ToString(),
                        Pay_Date = reader["Pay_Date"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),


                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_Customer_INFO_41211
    public static List<RPT_Customer_INFO_41211> RPT_Customer_INFO_41211(String Region, string CV_Code, string CustType, string CustName, string SP, string ResidentType, string Status, string Temp0, string Temp2, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Customer_INFO_41211";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CustType))
        {
            selectCommand.Parameters.AddWithValue("@CustType", CustType);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CustType", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CustName))
        {
            selectCommand.Parameters.AddWithValue("@CustName", CustName);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CustName", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP))
        {
            selectCommand.Parameters.AddWithValue("@SP", SP);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(ResidentType))
        {
            selectCommand.Parameters.AddWithValue("@ResidentType", ResidentType);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@ResidentType", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Status))
        {
            selectCommand.Parameters.AddWithValue("@Status", Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_Customer_INFO_41211> item = new List<RPT_Customer_INFO_41211>();
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
                    item.Add(new RPT_Customer_INFO_41211
                    {

                        Region_Name = reader["Region_Name"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        Status = reader["Status"].ToString(),
                        Customer_Name = reader["Customer_Name"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        Agent_Name = reader["Agent_Name"].ToString(),
                        Customer_ID = reader["Customer_ID"].ToString(),
                        CustType = reader["CustType"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Resident = reader["Resident"].ToString(),
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_CUSTOMER_DEBT_41223
    public static List<RPT_CUSTOMER_DEBT_41223> RPT_CUSTOMER_DEBT_41223(String Region, string CV_Code, string SP_ID, string StartDate, string EndDate, string Status, string Temp0, string Temp1, string Temp2, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_CUSTOMER_DEBT_41223";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(Status))
        {
            selectCommand.Parameters.AddWithValue("@Status", Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_CUSTOMER_DEBT_41223> item = new List<RPT_CUSTOMER_DEBT_41223>();
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
                    item.Add(new RPT_CUSTOMER_DEBT_41223
                    {

                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"] is DBNull ? null : reader["CV_Name"].ToString(),
                        Address = reader["Address"] is DBNull ? null : reader["Address"].ToString(),
                        CV_Phone = reader["CV_Phone"] is DBNull ? null : reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"] is DBNull ? null : reader["TAXID"].ToString(),
                        Debt_StartDate = reader["Debt_StartDate"] is DBNull ? null : reader["Debt_StartDate"].ToString(),
                        Debt_EndDate = reader["Debt_EndDate"] is DBNull ? null : reader["Debt_EndDate"].ToString(),
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Debt_Type = reader["Debt_Type"] is DBNull ? null : reader["Debt_Type"].ToString(),
                        SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString(),
                        Debt_ID = reader["Debt_ID"] is DBNull ? null : reader["Debt_ID"].ToString(),
                        Debt_Name = reader["Debt_Name"] is DBNull ? null : reader["Debt_Name"].ToString(),
                        SP_Peroid = reader["SP_Peroid"] is DBNull ? null : reader["SP_Peroid"].ToString(),
                        CT_Peroid = reader["CT_Peroid"] is DBNull ? null : reader["CT_Peroid"].ToString(),
                        Region_Name = reader["Region_Name"] is DBNull ? null : reader["Region_Name"].ToString(),
                        Temp0 = reader["Temp0"] is DBNull ? null : reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"] is DBNull ? null : reader["Temp1"].ToString(),

                        SP_Amt = (reader["SP_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["SP_Amt"].ToString())),
                        SP_Pay_Amt = (reader["SP_Pay_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["SP_Pay_Amt"].ToString())),
                        SP_OST_Amt = (reader["SP_OST_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["SP_OST_Amt"].ToString())),

                        CT_Amt = (reader["CT_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["CT_Amt"].ToString())),
                        CT_Pay_Amt = (reader["CT_Pay_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["CT_Pay_Amt"].ToString())),
                        CT_OST_Amt = (reader["CT_OST_Amt"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["CT_OST_Amt"].ToString())),

                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_Customer_BIRTH_DATE_41213
    public static List<RPT_Customer_BIRTH_DATE_41213> RPT_Customer_BIRTH_DATE_41213(String Region, string CV_Code, string Month, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string Temp5, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Customer_BIRTH_DATE_41213";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Month))
        {
            selectCommand.Parameters.AddWithValue("@Month", Month);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Month", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_Customer_BIRTH_DATE_41213> item = new List<RPT_Customer_BIRTH_DATE_41213>();
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
                    item.Add(new RPT_Customer_BIRTH_DATE_41213
                    {

                        Region_Name = reader["Region_Name"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Adress = reader["Adress"].ToString(),
                        Birth_Date = reader["Birth_Date"].ToString(),
                        Cust_ID = reader["Cust_ID"].ToString(),
                        Cust_Name = reader["Cust_Name"].ToString(),
                        Cust_Type = reader["Cust_Type"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
                    });
                }

            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            // logger.Error(ex.Message);
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
    //RPT_REV_EXP_DAILY_41220
    public static List<RPT_REV_EXP_DAILY_41220> RPT_REV_EXP_DAILY_41220(String Region, string CV_Code, string StartDate, string EndDate, string Temp0, string Temp1, string Temp2, string Temp3, string Temp4, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_REV_EXP_DAILY_41220";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_REV_EXP_DAILY_41220> item = new List<RPT_REV_EXP_DAILY_41220>();
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
                    item.Add(new RPT_REV_EXP_DAILY_41220
                    {
                        Region = reader["Region"] is DBNull ? null : reader["Region"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"] is DBNull ? null : reader["CV_Name"].ToString(),
                        Address = reader["Address"] is DBNull ? null : reader["Address"].ToString(),
                        Phone = reader["Phone"] is DBNull ? null : reader["Phone"].ToString(),
                        Tax_ID = reader["Tax_ID"] is DBNull ? null : reader["Tax_ID"].ToString(),
                        ACC_Type = reader["ACC_Type"] is DBNull ? null : reader["ACC_Type"].ToString(),
                        ACC_Code = reader["ACC_Code"] is DBNull ? null : reader["ACC_Code"].ToString(),
                        ACC_Name = reader["ACC_Name"] is DBNull ? null : reader["ACC_Name"].ToString(),
                        RV_EX_Amount = (reader["RV_EX_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["RV_EX_Amount"].ToString())),
                        SP_Cash = (reader["SP_Cash"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["SP_Cash"].ToString())),
                        Credit_Amount = (reader["Credit_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Credit_Amount"].ToString())),
                        Net_Sales_Amount = (reader["Net_Sales_Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Net_Sales_Amount"].ToString())),
                        Balance_Outstanding = (reader["Balance_Outstanding"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Balance_Outstanding"].ToString())),
                        Post_Date = reader["Post_Date"] is DBNull ? null : reader["Post_Date"].ToString(),
                        Remark = reader["Remark"] is DBNull ? null : reader["Remark"].ToString(),
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_REQUES_COMMISSION_41217
    public static List<RPT_REQUES_COMMISSION_41217> RPT_REQUES_COMMISSION_41217(String Region, string CV_Code, string SP_ID, string CR_StartDate, string CR_EndDate, string RQ_StartDate, string RQ_EndDate, string Temp0, string Temp1, string UserID)
    {
        //logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_REQUES_COMMISSION_41217";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CR_StartDate))
        {
            selectCommand.Parameters.AddWithValue("@CR_StartDate", CR_StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CR_StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CR_EndDate))
        {
            selectCommand.Parameters.AddWithValue("@CR_EndDate", CR_EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CR_EndDate", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(RQ_StartDate))
        {
            selectCommand.Parameters.AddWithValue("@RQ_StartDate", RQ_StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@RQ_StartDate", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(RQ_EndDate))
        {
            selectCommand.Parameters.AddWithValue("@RQ_EndDate", RQ_EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@RQ_EndDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Temp0))
        {
            selectCommand.Parameters.AddWithValue("@Temp0", Temp0);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_REQUES_COMMISSION_41217> item = new List<RPT_REQUES_COMMISSION_41217>();
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
                    item.Add(new RPT_REQUES_COMMISSION_41217
                    {
                        Region_Name = reader["Region_Name"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Clearing_Date = reader["Clearing_Date"].ToString(),
                        Commission_Requisition_Date = reader["Commission_Requisition_Date"].ToString(),
                        Comm_Amt = (decimal)reader["Comm_Amt"],
                        CR_EndDate = reader["CR_EndDate"].ToString(),
                        CR_StartDate = reader["CR_StartDate"].ToString(),
                        Requisition_Date = reader["Requisition_Date"].ToString(),
                        RQ_EndDate = reader["RQ_EndDate"].ToString(),
                        RQ_StartDate = reader["RQ_StartDate"].ToString(),
                        SP_ID = reader["SP_ID"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        Status = reader["Status"].ToString(),
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_EXPENSE_MONTHLY_41218
    public static List<RPT_EXPENSE_MONTHLY_41218> RPT_EXPENSE_MONTHLY_41218(String Region, string CV_Code, string Start_Month, string End_Month, string Year, string Temp0, string Temp1, string Temp2, string Temp3, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_EXPENSE_MONTHLY_41218";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Start_Month))
        {
            selectCommand.Parameters.AddWithValue("@Start_Month", Start_Month);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Start_Month", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(End_Month))
        {
            selectCommand.Parameters.AddWithValue("@End_Month", End_Month);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@End_Month", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Year))
        {
            selectCommand.Parameters.AddWithValue("@Year", Year);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_EXPENSE_MONTHLY_41218> item = new List<RPT_EXPENSE_MONTHLY_41218>();
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
                    item.Add(new RPT_EXPENSE_MONTHLY_41218
                    {
                        Region_Name = reader["Region_Name"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Apl = (decimal)reader["Apl"],
                        Aug = (decimal)reader["Aug"],
                        Dec = (decimal)reader["Dec"],
                        EndMonth = reader["EndMonth"].ToString(),
                        Exp_Date = reader["Exp_Date"].ToString(),
                        Exp_ID = reader["Exp_ID"].ToString(),
                        Exp_Name = reader["Exp_Name"].ToString(),
                        Feb = (decimal)reader["Feb"],
                        Jan = (decimal)reader["Jan"],
                        Jul = (decimal)reader["Jul"],
                        Jun = (decimal)reader["Jun"],
                        Mar = (decimal)reader["Mar"],
                        May = (decimal)reader["May"],
                        Nov = (decimal)reader["Nov"],
                        Oct = (decimal)reader["Oct"],
                        Sep = (decimal)reader["Sep"],
                        StartMonth = reader["StartMonth"].ToString(),
                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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

    //RPT_AGENT_DEBT_41222
    public static List<RPT_AGENT_DEBT_41222> RPT_AGENT_DEBT_41222(String Region, string CV_Code, string SP_ID, string StartDate, string EndDate, string Status, string Temp0, string Temp1, string Temp2, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_AGENT_DEBT_41222";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Status))
        {
            selectCommand.Parameters.AddWithValue("@Status", Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", "");
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_AGENT_DEBT_41222> item = new List<RPT_AGENT_DEBT_41222>();
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
                    item.Add(new RPT_AGENT_DEBT_41222
                    {
                        Region_Name = reader["Region_Name"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),
                        Address = reader["Address"].ToString(),
                        CV_Code = reader["CV_Code"].ToString(),
                        CV_Name = reader["CV_Name"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Debt_Amt = (decimal)reader["Debt_Amt"],
                        Debt_Date = reader["Debt_Date"].ToString(),
                        Debt_EndDate = reader["Debt_EndDate"].ToString(),
                        Debt_StartDate = reader["Debt_StartDate"].ToString(),
                        Outstanding_Amt = (decimal)reader["Outstanding_Amt"],
                        Pay_Amt = (decimal)reader["Pay_Amt"],
                        SP_ID = reader["SP_ID"].ToString(),
                        SP_Name = reader["SP_Name"].ToString(),
                        Status = reader["Status"].ToString(),

                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_ADJUST_CountSotck_4128
    public static List<RPT_ADJUST_CountSotck_4128> RPT_ADJUST_CountSotck_4128(String CV_Code, string StartCountDate, string EndCountDate, string PGroup, string Size, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_ADJUST_CountSotck_4128";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartCountDate))
        {
            selectCommand.Parameters.AddWithValue("@StartCountDate", StartCountDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartCountDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(EndCountDate))
        {
            selectCommand.Parameters.AddWithValue("@EndCountDate", EndCountDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndCountDate", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(PGroup))
        {
            selectCommand.Parameters.AddWithValue("@PGroup", PGroup);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PGroup", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Size))
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }

        //selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_ADJUST_CountSotck_4128> item = new List<RPT_ADJUST_CountSotck_4128>();
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
                    item.Add(new RPT_ADJUST_CountSotck_4128
                    {
                        CV_Code = reader["CV_Code"].ToString(),
                        Agent_Name = reader["Agent_Name"].ToString(),
                        Count_Date = reader["Count_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Count_Date"].ToString()),
                        Count_Quantity = (reader["Count_Quantity"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Count_Quantity"].ToString())),
                        Diff_Quantity = (reader["Diff_Quantity"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Diff_Quantity"].ToString())),
                        Product_group_id = reader["Product_group_id"].ToString(),
                        Quantity = (reader["Quantity"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Quantity"].ToString())),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Unit = reader["Unit"].ToString(),

                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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
    //RPT_ADJ_STOCK__4128
    public static List<RPT_ADJ_STOCK__4128> RPT_ADJ_STOCK__4128(string Region, String CV_Code, string StartDate, string EndDate, string PGroup, string Size, string Temp0, string Temp1, string Temp2, string UserID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_ADJ_STOCK__4128";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!String.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(StartDate))
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", "01/01/2559");
        }
        if (!String.IsNullOrEmpty(EndDate))
        {
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", "01/01/2580");
        }
        if (!String.IsNullOrEmpty(PGroup))
        {
            selectCommand.Parameters.AddWithValue("@PGroup", PGroup);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PGroup", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(Size))
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@Temp0", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp1", DBNull.Value);
        selectCommand.Parameters.AddWithValue("@Temp2", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp3", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp4", DBNull.Value);
        //selectCommand.Parameters.AddWithValue("@Temp5", DBNull.Value);

        if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        {
            selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        List<RPT_ADJ_STOCK__4128> item = new List<RPT_ADJ_STOCK__4128>();
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
                    item.Add(new RPT_ADJ_STOCK__4128
                    {
                        CV_Code = reader["CV_Code"].ToString(),

                        Unit = reader["Unit"].ToString(),
                        Address = reader["Address"].ToString(),
                        Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        Count_QTY = (reader["Count_QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Count_QTY"].ToString())),
                        CV_Name = reader["CV_Name"].ToString(),
                        CV_Phone = reader["CV_Phone"].ToString(),
                        Diff_QTY = (reader["Diff_QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Diff_QTY"].ToString())),
                        EndDate = reader["EndDate"].ToString(),
                        ProductGroup = reader["ProductGroup"].ToString(),
                        Product_Name = reader["Product_Name"].ToString(),
                        Proudct_ID = reader["Proudct_ID"].ToString(),
                        QTY = (reader["QTY"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["QTY"].ToString())),
                        Region_Name = reader["Region_Name"].ToString(),
                        Remark = reader["Remark"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        TAXID = reader["TAXID"].ToString(),
                        Temp0 = reader["Temp0"].ToString(),
                        Temp1 = reader["Temp1"].ToString(),
                        Temp2 = reader["Temp2"].ToString(),
                        Temp3 = reader["Temp3"].ToString(),
                        Temp4 = reader["Temp4"].ToString(),
                        Temp5 = reader["Temp5"].ToString(),


                        //Size = (reader["Size"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Size"].ToString())),
                        //Debt_Date = reader["Debt_Date"] is DBNull ? DateTime.MinValue : DateTime.Parse(reader["Debt_Date"].ToString()),
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

    public static List<RPT_Get_Clearing_No_Deduct> RPT_Get_Clearing_No_Deduct(String Clearing_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Get_Clearing_No_Deduct";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        //selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);

        if (!String.IsNullOrEmpty(Clearing_No))
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }

        List<RPT_Get_Clearing_No_Deduct> item = new List<RPT_Get_Clearing_No_Deduct>();
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
                    item.Add(new RPT_Get_Clearing_No_Deduct()
                    {
                        Type = reader["Type"].ToString(),
                        Description = reader["Description"].ToString(),
                        Amount = (reader["Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Amount"].ToString())),
                        Unit = reader["Unit"].ToString(),
                        Clearing_No = reader["Clearing_No"].ToString(),

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
    public static List<RPT_Get_Clearing_No_Subsidy> RPT_Get_Clearing_No_Subsidy(String Clearing_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "RPT_Get_Clearing_No_Subsidy";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        //selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);

        if (!String.IsNullOrEmpty(Clearing_No))
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }

        List<RPT_Get_Clearing_No_Subsidy> item = new List<RPT_Get_Clearing_No_Subsidy>();
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
                    item.Add(new RPT_Get_Clearing_No_Subsidy()
                    {
                        Type = reader["Type"].ToString(),
                        Description = reader["Description"].ToString(),
                        Amount = (reader["Amount"] is DBNull ? decimal.Parse("0") : decimal.Parse(reader["Amount"].ToString())),
                        Unit = reader["Unit"].ToString(),
                        Clearing_No = reader["Clearing_No"].ToString(),

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
}

