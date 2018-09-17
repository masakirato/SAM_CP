using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_ClearingDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static List<dbo_ProductClass> GetDepositByProductGroupID(String User_ID, String Clearing_No, DateTime? Price_Date, String Product_group_ID, String Requisition_No)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        // logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().GetParameters().ToString());

        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetDepositByProductGroupID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Clearing_No))
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (Price_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Price_Date", Price_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price_Date", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Requisition_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }


        List<dbo_ProductClass> item = new List<dbo_ProductClass>();
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);


                Int16? prevSize = -1;
                String prevPacking_Size = string.Empty;

                int index = 1;
                foreach (DataRow reader in dt.Rows)
                {

                    if ((prevSize != (Int16?)reader["Size"] || prevPacking_Size != (reader["Unit_of_item_ID"]).ToString()) && Product_group_ID != "อื่นๆ")
                    {
                        item.Add(new dbo_ProductClass()
                        {
                            Product_ID = "Merge",

                            Product_Name = string.Format("{0} ({1}{2}/ลัง)", ((Int16?)reader["Size"]).ToString() + " CC.", (reader["Packing_Size"]).ToString(), (reader["Unit_of_item_ID"]).ToString())
                            ,
                            index = 1
                        });

                        index = 1;
                    }


                    item.Add(new dbo_ProductClass()
                    {
                        index = index++,
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"],
                        Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                        Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                        SP_Price = reader["SP_Price"] is DBNull ? null : (Decimal?)reader["SP_Price"],


                        Vat = 0,
                        Order_No = 0,
                        Packing_Size = 0,
                        Status = null,
                        Total_Qty = reader["Total_Qty"] is DBNull ? 0 : (Int16?)reader["Total_Qty"],
                        Deposit_Qty = reader["Deposit_Qty"] is DBNull ? 0 : (Int16?)reader["Deposit_Qty"]
                        ,
                        Sales_Qty = reader["Sales_Qty"] is DBNull ? 0 : (Int16?)reader["Sales_Qty"],
                        Sales_Amount = reader["Sales_Amount"] is DBNull ? null : (Decimal?)reader["Sales_Amount"],
                        Return_Qty = reader["Return_Qty"] is DBNull ? 0 : (Int16?)reader["Return_Qty"],

                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],


                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        Deposit_Detail_ID = reader["Deposit_Detail_ID"] is DBNull ? null : reader["Deposit_Detail_ID"].ToString()


                        ,
                        Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"]
                        ,
                        End_Effective_Date = reader["End_Effective_Date"] is DBNull ? null : (DateTime?)reader["End_Effective_Date"]


                    });

                    prevPacking_Size = (reader["Unit_of_item_ID"]).ToString();
                    prevSize = reader["Size"] is DBNull ? null : (Int16?)reader["Size"];
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

    [Obsolete]
    public static List<dbo_ClearingClass> SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[ClearingSelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        List<dbo_ClearingClass> item = new List<dbo_ClearingClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_ClearingClass()
                    {
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"],
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Cash_Payment = reader["Cash_Payment"] is DBNull ? null : (Decimal?)reader["Cash_Payment"],
                        Actual_Payment = reader["Actual_Payment"] is DBNull ? null : (Decimal?)reader["Actual_Payment"],
                        Balance_Outstanding = reader["Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding"]
                    });
                }

            }
            reader1.Close();
        }
        catch (SqlException ex)
        {

            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }


    public static List<dbo_ClearingClass> ClearingCommissionSearch(String Clearing_No,
        DateTime? Clearing_Date_Begin, DateTime? Clearing_Date_End, DateTime? Requisition_Date_Begin, DateTime? Requisition_Date_End,
        String User_ID, String Commission_requisition_no, String Requisition_Status, String CV_Code
        )
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[ClearingCommissionSearch]";
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


        if (Clearing_Date_Begin.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_Begin", Clearing_Date_Begin.Value.Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_Begin", DateTime.Now.AddDays(-30));
        }
        if (Clearing_Date_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_End", Clearing_Date_End.Value.AddDays(1));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_End", DateTime.Now.AddDays(30));
        }


        // Requisition_Date_Begin, DateTime? Requisition_Date_End


        if (Requisition_Date_Begin.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Date_Begin", Requisition_Date_Begin);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Date_Begin", DBNull.Value);
        }
        if (Requisition_Date_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Date_End", Requisition_Date_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Date_End", DBNull.Value);
        }



        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }


        if (!string.IsNullOrEmpty(Commission_requisition_no))
        {
            selectCommand.Parameters.AddWithValue("@Commission_requisition_no", Commission_requisition_no);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Commission_requisition_no", DBNull.Value);
        }


        switch (Requisition_Status)
        {
            case "ยังไม่ได้เบิก":
                selectCommand.Parameters.AddWithValue("@Requisition_Status", 1);
                break;
            case "เบิกบางส่วน":
                selectCommand.Parameters.AddWithValue("@Requisition_Status", 2);
                break;
            case "เบิกแล้ว":
                selectCommand.Parameters.AddWithValue("@Requisition_Status", 3);
                break;
            default:
                selectCommand.Parameters.AddWithValue("@Requisition_Status", DBNull.Value);
                break;
        }


        if (!string.IsNullOrEmpty(Requisition_Status))
        {

        }
        else
        {

        }

        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }






        List<dbo_ClearingClass> item = new List<dbo_ClearingClass>();

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);


                string prev_User_ID = string.Empty;


                foreach (DataRow reader in dt.Rows)
                {

                    if (reader["User_ID"].ToString() != prev_User_ID)
                    {
                        item.Add(new dbo_ClearingClass()
                        {
                            Clearing_No = "Merge",
                            User_ID = reader["User_ID"].ToString(),
                            //Commission_Balance_Outstanding = (Decimal?)reader["Commission_Balance_Outstanding"]

                        });
                    }

                    item.Add(new dbo_ClearingClass()
                    {
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"],
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Commission_Requisition_Status = reader["Requisition_Status"] is DBNull ? null : reader["Requisition_Status"].ToString(),
                        Commission = reader["Requisition_Amount"] is DBNull ? null : (Decimal?)reader["Requisition_Amount"],
                        Requisition_No = reader["Commission_requisition_no"] is DBNull ? null : reader["Commission_requisition_no"].ToString(),
                        Commission_requisition_date = reader["Commission_requisition_date"] is DBNull ? null : (DateTime?)reader["Commission_requisition_date"],
                        Commission_Balance_Outstanding = null
                        //reader["Commission_Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Commission_Balance_Outstanding"],
                        //Today_Commission = reader["Today_Commission"] is DBNull ? null : (Decimal?)reader["Today_Commission"],

                        //Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString()
                        // ,
                        //Total_Commission = reader["Total_Commission"] is DBNull ? null : (Decimal?)reader["Total_Commission"]



                    });

                    prev_User_ID = reader["User_ID"].ToString();
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


    public static List<dbo_ClearingClass> Get_Commission(String Clearing_No, DateTime? Clearing_Date_Begin, DateTime? Clearing_Date_End, String User_ID, String CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[Get_Commission]";
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


        if (Clearing_Date_Begin.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_Begin", Clearing_Date_Begin);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_Begin", DateTime.Now.AddDays(-30));
        }
        if (Clearing_Date_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_End", Clearing_Date_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_End", DateTime.Now.AddDays(30));
        }
        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        List<dbo_ClearingClass> item = new List<dbo_ClearingClass>();

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);


                string prev_User_ID = string.Empty;
                foreach (DataRow reader in dt.Rows)
                {
                    if (prev_User_ID != reader["User_ID"].ToString())
                    {
                        item.Add(new dbo_ClearingClass()
                        {
                            Clearing_No = "Merge",
                            User_ID = reader["User_ID"].ToString()
                        });
                    }

                    item.Add(new dbo_ClearingClass()
                    {
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"],
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Commission = reader["Commission"] is DBNull ? 0 : (Decimal?)reader["Commission"],
                        // Requisition_Amount = reader["Requisition_Amount"] is DBNull ? 0 : (Decimal?)reader["Requisition_Amount"],
                        Commission_Balance_Outstanding = reader["Commission_Balance_Outstanding"] is DBNull ? 0 : (Decimal?)reader["Commission_Balance_Outstanding"],
                        Commission_Requisition_Status = reader["Commission_Requisition_Status"] is DBNull ? null : reader["Commission_Requisition_Status"].ToString(),
                        // Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString()

                    });
                    prev_User_ID = reader["User_ID"].ToString();
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

    public static List<dbo_ClearingClass> Search(String Clearing_No, DateTime? Clearing_Date_Begin, DateTime? Clearing_Date_End, String User_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[ClearingSearch]";
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


        if (Clearing_Date_Begin.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_Begin", Clearing_Date_Begin);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_Begin", DateTime.Now.AddDays(-30));
        }
        if (Clearing_Date_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_End", Clearing_Date_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Date_End", DateTime.Now.AddDays(30));
        }



        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }

        List<dbo_ClearingClass> item = new List<dbo_ClearingClass>();

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
                    item.Add(new dbo_ClearingClass()
                    {
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"],
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Cash_Payment = reader["Cash_Payment"] is DBNull ? null : (Decimal?)reader["Cash_Payment"],
                        Actual_Payment = reader["Actual_Payment"] is DBNull ? null : (Decimal?)reader["Actual_Payment"],
                        Balance_Outstanding = reader["Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding"],
                        Total_Commission = reader["Total_Commission"] is DBNull ? null : (Decimal?)reader["Total_Commission"],

                        Credit_Amount = reader["Credit_Amount"] is DBNull ? null : (Decimal?)reader["Credit_Amount"],

                        SP_Cash = reader["SP_Cash"] is DBNull ? null : (Decimal?)reader["SP_Cash"],
                        Cash_Payment_Amount = reader["Cash_Payment_Amount"] is DBNull ? null : (Decimal?)reader["Cash_Payment_Amount"],
                        Sub_Total = reader["Sub_Total"] is DBNull ? null : (Decimal?)reader["Sub_Total"],
                        Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                        Discount = reader["Discount"] is DBNull ? null : (Decimal?)reader["Discount"],
                        This_Month_Sales_Amount = reader["This_Month_Sales_Amount"] is DBNull ? null : (Decimal?)reader["This_Month_Sales_Amount"],

                        Today_Deposit_Amount = reader["Today_Deposit_Amount"] is DBNull ? null : (Int16?)reader["Today_Deposit_Amount"],
                        This_Month_Points = reader["This_Month_Points"] is DBNull ? null : (Decimal?)reader["This_Month_Points"],
                        Total_Deposit = reader["Total_Deposit"] is DBNull ? null : (Decimal?)reader["Total_Deposit"],

                        Net_Total = reader["Net_Total"] is DBNull ? null : (Decimal?)reader["Net_Total"],
                        Debt_Payment = reader["Debt_Payment"] is DBNull ? null : (Decimal?)reader["Debt_Payment"],
                        Debt_Balance = reader["Debt_Balance"] is DBNull ? null : (Decimal?)reader["Debt_Balance"],
                        Today_Return_Amount = reader["Today_Return_Amount"] is DBNull ? null : (Decimal?)reader["Today_Return_Amount"],


                        Net_Sales_Qty = reader["Net_Sales_Qty"] is DBNull ? null : (Int16?)reader["Net_Sales_Qty"],
                        Net_Sales_Amount = reader["Net_Sales_Amount"] is DBNull ? null : (Decimal?)reader["Net_Sales_Amount"],
                        Today_Commission = reader["Today_Commission"] is DBNull ? null : (Decimal?)reader["Today_Commission"],
                        Today_Points = reader["Today_Points"] is DBNull ? null : (Int16?)reader["Today_Points"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),


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



    public static dbo_CommissionRequisitionClass Get_Commission_Balance_Outstanding(String User_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_CommissionRequisitionClass clsdbo_CommissionRequisitionClass = new dbo_CommissionRequisitionClass();

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[Get_Commission_Balance_Outstanding]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CommissionRequisitionClass.Total_Balance_Outstanding = reader["Total_Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Total_Balance_Outstanding"];
                clsdbo_CommissionRequisitionClass.Total_Credit_Amount = reader["Total_Credit_Amount"] is DBNull ? null : (Decimal?)reader["Total_Credit_Amount"];
                clsdbo_CommissionRequisitionClass.Commission_Balance_Outstanding = reader["Commission_Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Commission_Balance_Outstanding"];
            }
            else
            {
                clsdbo_CommissionRequisitionClass = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return null;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CommissionRequisitionClass;
    }

    public static List<DepositCheckPriceDiff> GetDepositCheckPriceDiff(String User_ID, String Requisition_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetDepositCheckPriceDiff]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Requisition_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }

        List<DepositCheckPriceDiff> item = new List<DepositCheckPriceDiff>();

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
                    item.Add(new DepositCheckPriceDiff()
                    {
                        product_name = reader["product_name"] is DBNull ? null : reader["product_name"].ToString(),
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Selling_Price = reader["Selling_Price"] is DBNull ? null : (Decimal?)reader["Selling_Price"],
                        Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"],
                        CP_Meiji_Price_Next_Day = reader["CP_Meiji_Price_Next_Day"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price_Next_Day"],
                        CP_Meiji_Price_Today = reader["CP_Meiji_Price_Today"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price_Today"],
                        SP_Price_Next_Day = reader["SP_Price_Next_Day"] is DBNull ? null : (Decimal?)reader["SP_Price_Next_Day"],
                        SP_Price_Today = reader["SP_Price_Today"] is DBNull ? null : (Decimal?)reader["SP_Price_Today"],
                        Product_List_ID_Next_Day = reader["Product_List_ID_Next_Day"] is DBNull ? null : reader["Product_List_ID_Next_Day"].ToString(),
                        Product_List_ID_Today = reader["Product_List_ID_Today"] is DBNull ? null : reader["Product_List_ID_Today"].ToString(),
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

    /*
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"],
                        Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                        Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                        SP_Price = reader["SP_Price"] is DBNull ? null : (Decimal?)reader["SP_Price"],


                        Vat = 0,
                        Order_No = 0,
                        Packing_Size = 0,
                        Status = null,
                        Total_Qty = reader["Total_Qty"] is DBNull ? 0 : (Int16?)reader["Total_Qty"],
                        Deposit_Qty = reader["Deposit_Qty"] is DBNull ? 0 : (Int16?)reader["Deposit_Qty"]
                        ,
                        Sales_Qty = reader["Sales_Qty"] is DBNull ? 0 : (Int16?)reader["Sales_Qty"],
                        Sales_Amount = reader["Sales_Amount"] is DBNull ? null : (Decimal?)reader["Sales_Amount"],
                        Return_Qty = reader["Return_Qty"] is DBNull ? 0 : (Int16?)reader["Return_Qty"],

                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],


                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        Deposit_Detail_ID = reader["Deposit_Detail_ID"] is DBNull ? null : reader["Deposit_Detail_ID"].ToString()


                        ,
                        Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"]
                        ,
                        End_Effective_Date = reader["End_Effective_Date"] is DBNull ? null : (DateTime?)reader["End_Effective_Date"]

    */
    public static dbo_ClearingClass Select_Record(String Clearing_No)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_ClearingClass clsdbo_Clearing = new dbo_ClearingClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[ClearingSelect]";
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
                clsdbo_Clearing.Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString();
                clsdbo_Clearing.Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"];
                clsdbo_Clearing.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_Clearing.Cash_Payment = reader["Cash_Payment"] is DBNull ? null : (Decimal?)reader["Cash_Payment"];
                clsdbo_Clearing.Actual_Payment = reader["Actual_Payment"] is DBNull ? null : (Decimal?)reader["Actual_Payment"];
                clsdbo_Clearing.Balance_Outstanding = reader["Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding"];
                clsdbo_Clearing.Discount = reader["Discount"] is DBNull ? null : (Decimal?)reader["Discount"];

                clsdbo_Clearing.Net_Sales_Qty = reader["Net_Sales_Qty"] is DBNull ? null : (Int16?)reader["Net_Sales_Qty"];
                clsdbo_Clearing.Net_Sales_Amount = reader["Net_Sales_Amount"] is DBNull ? null : (Decimal?)reader["Net_Sales_Amount"];
                clsdbo_Clearing.Today_Commission = reader["Today_Commission"] is DBNull ? null : (Decimal?)reader["Today_Commission"];
                clsdbo_Clearing.Today_Points = reader["Today_Points"] is DBNull ? null : (Int16?)reader["Today_Points"];

                clsdbo_Clearing.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();

                clsdbo_Clearing.Debt_Total = reader["Debt_Total"] is DBNull ? null : (Decimal?)reader["Debt_Total"];
                clsdbo_Clearing.Debt_Balance = reader["Debt_Balance"] is DBNull ? null : (Decimal?)reader["Debt_Balance"];
                clsdbo_Clearing.Total_Balance_Outstanding = reader["Total_Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Total_Balance_Outstanding"];
                clsdbo_Clearing.Total_Credit_Amount = reader["Total_Credit_Amount"] is DBNull ? null : (Decimal?)reader["Total_Credit_Amount"];

            }
            else
            {
                clsdbo_Clearing = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Clearing;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Clearing;
    }

    public static bool Add(dbo_ClearingClass clsdbo_Clearing, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[ClearingInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Clearing.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_Clearing.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (clsdbo_Clearing.Clearing_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_Date", clsdbo_Clearing.Clearing_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_Date", DBNull.Value);
        }
        if (clsdbo_Clearing.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_Clearing.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (clsdbo_Clearing.Cash_Payment.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Cash_Payment", clsdbo_Clearing.Cash_Payment);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Cash_Payment", DBNull.Value);
        }
        if (clsdbo_Clearing.Actual_Payment.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Actual_Payment", clsdbo_Clearing.Actual_Payment);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Actual_Payment", DBNull.Value);
        }
        if (clsdbo_Clearing.Balance_Outstanding.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Balance_Outstanding", clsdbo_Clearing.Balance_Outstanding);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Balance_Outstanding", DBNull.Value);
        }
        if (clsdbo_Clearing.Net_Sales_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Net_Sales_Qty", clsdbo_Clearing.Net_Sales_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Net_Sales_Qty", DBNull.Value);
        }
        if (clsdbo_Clearing.Net_Sales_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Net_Sales_Amount", clsdbo_Clearing.Net_Sales_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Net_Sales_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.Credit_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Credit_Amount", clsdbo_Clearing.Credit_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.SP_Cash.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@SP_Cash", clsdbo_Clearing.SP_Cash);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SP_Cash", DBNull.Value);
        }
        if (clsdbo_Clearing.Cash_Payment_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Cash_Payment_Amount", clsdbo_Clearing.Cash_Payment_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Cash_Payment_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.Cheque_Payment_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Cheque_Payment_Amount", clsdbo_Clearing.Cheque_Payment_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Cheque_Payment_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.Transfer_Payment_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Transfer_Payment_Amount", clsdbo_Clearing.Transfer_Payment_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Transfer_Payment_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.Sub_Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Sub_Total", clsdbo_Clearing.Sub_Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sub_Total", DBNull.Value);
        }
        if (clsdbo_Clearing.Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total", clsdbo_Clearing.Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total", DBNull.Value);
        }
        if (clsdbo_Clearing.Discount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Discount", clsdbo_Clearing.Discount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Discount", DBNull.Value);
        }
        if (clsdbo_Clearing.Today_Points.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Today_Points", clsdbo_Clearing.Today_Points);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Today_Points", DBNull.Value);
        }
        if (clsdbo_Clearing.Today_Commission.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Today_Commission", clsdbo_Clearing.Today_Commission);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Today_Commission", DBNull.Value);
        }
        if (clsdbo_Clearing.This_Month_Sales_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@This_Month_Sales_Amount", clsdbo_Clearing.This_Month_Sales_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@This_Month_Sales_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.Today_Deposit_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Today_Deposit_Amount", clsdbo_Clearing.Today_Deposit_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Today_Deposit_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.Total_Credit_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Credit_Amount", clsdbo_Clearing.Total_Credit_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Credit_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.Total_Balance_Outstanding.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Balance_Outstanding", clsdbo_Clearing.Total_Balance_Outstanding);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Balance_Outstanding", DBNull.Value);
        }
        if (clsdbo_Clearing.Total_Commission.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Commission", clsdbo_Clearing.Total_Commission);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Commission", DBNull.Value);
        }
        if (clsdbo_Clearing.This_Month_Points.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@This_Month_Points", clsdbo_Clearing.This_Month_Points);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@This_Month_Points", DBNull.Value);
        }
        if (clsdbo_Clearing.Total_Deposit.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Deposit", clsdbo_Clearing.Total_Deposit);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Deposit", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Created_By))
        {
            insertCommand.Parameters.AddWithValue("@Created_By", Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }

        if (clsdbo_Clearing.Net_Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Net_Total", clsdbo_Clearing.Net_Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Net_Total", DBNull.Value);
        }
        if (clsdbo_Clearing.Debt_Payment.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Debt_Payment", clsdbo_Clearing.Debt_Payment);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Debt_Payment", DBNull.Value);
        }
        if (clsdbo_Clearing.Debt_Balance.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Debt_Balance", clsdbo_Clearing.Debt_Balance);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Debt_Balance", DBNull.Value);
        }
        if (clsdbo_Clearing.Today_Return_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Today_Return_Amount", clsdbo_Clearing.Today_Return_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Today_Return_Amount", DBNull.Value);
        }
        if (clsdbo_Clearing.Status != null)
        {
            insertCommand.Parameters.AddWithValue("@Status", clsdbo_Clearing.Status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Status", DBNull.Value);
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
        }
        finally
        {
            connection.Close();
        }
        return true;
    }
    public static bool Update(dbo_ClearingClass newdbo_ClearingClass)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[ClearingUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_ClearingClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", newdbo_ClearingClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", DBNull.Value);
        }
        if (newdbo_ClearingClass.Clearing_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_Date", newdbo_ClearingClass.Clearing_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_Date", DBNull.Value);
        }
        if (newdbo_ClearingClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", newdbo_ClearingClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", DBNull.Value);
        }
        if (newdbo_ClearingClass.Cash_Payment.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCash_Payment", newdbo_ClearingClass.Cash_Payment);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCash_Payment", DBNull.Value);
        }
        if (newdbo_ClearingClass.Actual_Payment.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewActual_Payment", newdbo_ClearingClass.Actual_Payment);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewActual_Payment", DBNull.Value);
        }
        if (newdbo_ClearingClass.Balance_Outstanding.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewBalance_Outstanding", newdbo_ClearingClass.Balance_Outstanding);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBalance_Outstanding", DBNull.Value);
        }
        if (newdbo_ClearingClass.Net_Sales_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Sales_Qty", newdbo_ClearingClass.Net_Sales_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Sales_Qty", DBNull.Value);
        }
        if (newdbo_ClearingClass.Net_Sales_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Sales_Amount", newdbo_ClearingClass.Net_Sales_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Sales_Amount", DBNull.Value);
        }
        if (newdbo_ClearingClass.Credit_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Amount", newdbo_ClearingClass.Credit_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Amount", DBNull.Value);
        }
        if (newdbo_ClearingClass.SP_Cash.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSP_Cash", newdbo_ClearingClass.SP_Cash);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSP_Cash", DBNull.Value);
        }
        if (newdbo_ClearingClass.Cash_Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCash_Payment_Amount", newdbo_ClearingClass.Cash_Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCash_Payment_Amount", DBNull.Value);
        }
        if (newdbo_ClearingClass.Cheque_Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCheque_Payment_Amount", newdbo_ClearingClass.Cheque_Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCheque_Payment_Amount", DBNull.Value);
        }
        if (newdbo_ClearingClass.Transfer_Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTransfer_Payment_Amount", newdbo_ClearingClass.Transfer_Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTransfer_Payment_Amount", DBNull.Value);
        }
        if (newdbo_ClearingClass.Sub_Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSub_Total", newdbo_ClearingClass.Sub_Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSub_Total", DBNull.Value);
        }
        if (newdbo_ClearingClass.Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", newdbo_ClearingClass.Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", DBNull.Value);
        }
        if (newdbo_ClearingClass.Discount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDiscount", newdbo_ClearingClass.Discount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDiscount", DBNull.Value);
        }
        if (newdbo_ClearingClass.Today_Points.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewToday_Points", newdbo_ClearingClass.Today_Points);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewToday_Points", DBNull.Value);
        }
        if (newdbo_ClearingClass.Today_Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewToday_Commission", newdbo_ClearingClass.Today_Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewToday_Commission", DBNull.Value);
        }
        if (newdbo_ClearingClass.This_Month_Sales_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewThis_Month_Sales_Amount", newdbo_ClearingClass.This_Month_Sales_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewThis_Month_Sales_Amount", DBNull.Value);
        }
        if (newdbo_ClearingClass.Today_Deposit_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewToday_Deposit_Amount", newdbo_ClearingClass.Today_Deposit_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewToday_Deposit_Amount", DBNull.Value);
        }
        if (newdbo_ClearingClass.Total_Credit_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Credit_Amount", newdbo_ClearingClass.Total_Credit_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Credit_Amount", DBNull.Value);
        }
        if (newdbo_ClearingClass.Total_Balance_Outstanding.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Balance_Outstanding", newdbo_ClearingClass.Total_Balance_Outstanding);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Balance_Outstanding", DBNull.Value);
        }
        if (newdbo_ClearingClass.Total_Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Commission", newdbo_ClearingClass.Total_Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Commission", DBNull.Value);
        }
        if (newdbo_ClearingClass.This_Month_Points.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewThis_Month_Points", newdbo_ClearingClass.This_Month_Points);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewThis_Month_Points", DBNull.Value);
        }
        if (newdbo_ClearingClass.Total_Deposit.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Deposit", newdbo_ClearingClass.Total_Deposit);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Deposit", DBNull.Value);
        }


        if (newdbo_ClearingClass.Net_Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@Net_Total", newdbo_ClearingClass.Net_Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Net_Total", DBNull.Value);
        }
        if (newdbo_ClearingClass.Debt_Payment.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@Debt_Payment", newdbo_ClearingClass.Debt_Payment);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Debt_Payment", DBNull.Value);
        }
        if (newdbo_ClearingClass.Debt_Balance.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@Debt_Balance", newdbo_ClearingClass.Debt_Balance);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Debt_Balance", DBNull.Value);
        }
        if (newdbo_ClearingClass.Today_Return_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@Today_Return_Amount", newdbo_ClearingClass.Today_Return_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Today_Return_Amount", DBNull.Value);
        }

        if (newdbo_ClearingClass.Status != null)
        {
            updateCommand.Parameters.AddWithValue("@Status", newdbo_ClearingClass.Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }

        if (newdbo_ClearingClass.Debt_Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@Debt_Total", newdbo_ClearingClass.Debt_Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Debt_Total", DBNull.Value);
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
    public static bool Delete(String Clearing_No)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[ClearingDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (!string.IsNullOrEmpty(Clearing_No))
        {
            deleteCommand.Parameters.AddWithValue("@OldClearing_No", Clearing_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldClearing_No", DBNull.Value);
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

public class DepositCheckPriceDiff
{
    public string product_name { get; set; }
    public string Product_ID { get; set; }
    public decimal? Selling_Price { get; set; }
    public decimal? CP_Meiji_Price_Today { get; set; }
    public decimal? CP_Meiji_Price_Next_Day { get; set; }
    public decimal? Price { get; set; }
    public decimal? SP_Price_Today { get; set; }
    public decimal? SP_Price_Next_Day { get; set; }
    public string Product_List_ID_Next_Day { get; set; }
    public string Product_List_ID_Today { get; set; }
}

