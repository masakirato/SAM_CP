using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using log4net;
public class dbo_OrderingDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static Dictionary<string, Int32?> GetOrderingSuggestion(string Product_group_ID, int dayofweek, string CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetOrderingSuggestion";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@dw", dayofweek);
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        Dictionary<string, Int32?> item = new Dictionary<string, Int32?>();

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
                    item.Add(reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString()
                        , reader["Suggest_Quantity"] is DBNull ? 0 : (Int32?)reader["Suggest_Quantity"]);
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



    [Obsolete]
    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[OrderingSelectAll]";
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
    public static List<OrderingList> GetOrderingList()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[GetOrderingList]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();

        List<OrderingList> OrderingList = null;

        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                OrderingList = new List<OrderingList>();
                dt.Load(reader);

                string prevProduct_List_ID = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    OrderingList.Add(new OrderingList()
                    {
                        Date_of_create_order_or_PO_Date = DateTime.Parse(row["Date_of_create_order_or_PO_Date"].ToString()),
                        Date_of_delivery_goods = null,
                        First_Name = row["First_Name"].ToString(),
                        Order_Status = row["Order_Status"].ToString(),
                        PO_Number = row["PO_Number"].ToString(),
                        Total_amount_after_vat_included = Decimal.Parse(row["Total_amount_after_vat_included"].ToString())
                    });

                }

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
        return OrderingList;
    }
    [Obsolete]
    public static List<Product_Quantity> _GetProduct_Quantity_Receiving(string CV_CODE, string Product_group_ID, string PO_Number)
    {
        List<Product_Quantity> ListOFProduct_Quantity = null;
        ListOFProduct_Quantity = new List<Product_Quantity>();

        switch (Product_group_ID)
        {
            case "91":

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "Merge",
                    Item_Value = "180 CC."
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    PO_Number = "PO-0003",
                    ItemNo = "1",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72001069",
                    Product_Name = "PM 180 โกลด์ แอดวานซ์จืด",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("14.26")
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    PO_Number = "PO-0003",
                    ItemNo = "2",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72001070",
                    Product_Name = "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("14.26")
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "Merge",
                    Item_Value = "200 CC."
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    PO_Number = "PO-0003",
                    ItemNo = "3",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72000723",
                    Product_Name = "PM 200 ช็อคโกแลต",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("14.26")
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    PO_Number = "PO-0003",
                    ItemNo = "4",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72000720",
                    Product_Name = "PM 200 เมล่อนญี่ปุ่น",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("14.26")
                });
                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    PO_Number = "PO-0003",
                    ItemNo = "5",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72000722",
                    Product_Name = "PM 200 กลิ่นแตงโม",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("14.26")
                });
                break;
            case "92":
                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "Merge",
                    Item_Value = "85 CC."
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    PO_Number = "PO-0003",
                    ItemNo = "1",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72000789",
                    Product_Name = "DY 85 ส้ม",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("14.26")
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    PO_Number = "PO-0003",
                    ItemNo = "2",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72000790",
                    Product_Name = "DY 85 สตรอเบอร์รี่",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("14.26")
                });

                break;
        }

        return ListOFProduct_Quantity;
    }

    [Obsolete]
    public static List<Product_Quantity> GetProduct_Quantity_Receiving(string CV_CODE, string Product_group_ID, string PO_Number)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ProductQuantityReceiving]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
        DataTable dt = new DataTable();

        List<Product_Quantity> ListOFProduct_Quantity = null;

        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                ListOFProduct_Quantity = new List<Product_Quantity>();
                dt.Load(reader);

                string prevProduct_List_ID = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    if (prevProduct_List_ID != row["Product_List_ID"].ToString())
                    {
                        ListOFProduct_Quantity.Add(new Product_Quantity()
                        {
                            ItemNo = "Merge",
                            Item_Value = row["Product_List_Name"].ToString(),
                        });
                    }

                    ListOFProduct_Quantity.Add(new Product_Quantity()
                    {
                        PO_Number = row["PO_Number"].ToString(),
                        ItemNo = row["ItemNo"].ToString(),
                        Item_Value = row["Item_Value"].ToString(),
                        Stock_End = int.Parse(row["Stock_End"].ToString()),
                        Stock_Out = int.Parse(row["Stock_Out"].ToString()),
                        Price = Decimal.Parse(row["Price"].ToString()),
                        Product_ID = row["Product_ID"].ToString(),
                        Product_Name = row["Product_Name"].ToString(),
                        Quantity = short.Parse((row["Quantity"].ToString() == string.Empty ? "0" : row["Quantity"].ToString())),
                        Total = Decimal.Parse((row["Total"].ToString() == string.Empty ? "0" : row["Total"].ToString())),

                    });

                    prevProduct_List_ID = row["Product_List_ID"].ToString();
                }

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
        return ListOFProduct_Quantity;
    }

    [Obsolete]
    public static List<Product_Quantity> _GetProduct_Quantity(string CV_CODE, string Product_group_ID)
    {
        List<Product_Quantity> ListOFProduct_Quantity = null;
        ListOFProduct_Quantity = new List<Product_Quantity>();


        switch (Product_group_ID)
        {
            case "91":
                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "Merge",
                    Item_Value = "180 CC. (49 ขวด/ลัง)",
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "1",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72001069",
                    Product_Name = "PM 180 โกลด์ แอดวานซ์จืด",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("0"),
                    Stock_SP = 1
                });
                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "2",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("14.26"),
                    Product_ID = "72001069",
                    Product_Name = "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("0"),
                    Stock_SP = 1
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "Merge",
                    Item_Value = "200 CC. (49 ขวด/ลัง)",
                });
                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "1",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("8.32"),
                    Product_ID = "72000723",
                    Product_Name = "PM 200 ช็อคโกแลต",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("0"),
                    Stock_SP = 1
                });
                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "1",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("8.32"),
                    Product_ID = "72000720",
                    Product_Name = "PM 200 เมล่อนญี่ปุ่น",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("0"),
                    Stock_SP = 1
                });
                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "1",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("8.18"),
                    Product_ID = "72000722",
                    Product_Name = "PM 200 กลิ่นแตงโม",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("0"),
                    Stock_SP = 1
                });
                break;
            case "92":

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "Merge",
                    Item_Value = "85 CC. (49 ขวด/ลัง)",
                });

                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "1",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("4.03"),
                    Product_ID = "72000789",
                    Product_Name = "DY 85 ส้ม",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("0"),
                    Stock_SP = 1
                });
                ListOFProduct_Quantity.Add(new Product_Quantity()
                {
                    ItemNo = "2",
                    Item_Value = "ขวด",
                    Stock_End = 90,
                    Stock_Out = 10,
                    Price = Decimal.Parse("4.03"),
                    Product_ID = "72000790",
                    Product_Name = "DY 85 สตรอเบอร์รี่",
                    Quantity = short.Parse("0"),
                    Total = Decimal.Parse("0"),
                    Stock_SP = 1
                });

                break;
        }



        return ListOFProduct_Quantity;
    }


    public static List<Product_Quantity> GetProduct_Quantity(string CV_CODE, string Product_group_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ProductQuantitySelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        DataTable dt = new DataTable();

        List<Product_Quantity> ListOFProduct_Quantity = null;

        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                ListOFProduct_Quantity = new List<Product_Quantity>();
                dt.Load(reader);

                string prevProduct_List_ID = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    if (prevProduct_List_ID != row["Product_List_ID"].ToString())
                    {
                        ListOFProduct_Quantity.Add(new Product_Quantity()
                        {
                            ItemNo = "Merge",
                            Item_Value = row["Product_List_Name"].ToString(),
                        });
                    }
                    ListOFProduct_Quantity.Add(new Product_Quantity()
                    {
                        ItemNo = row["ItemNo"].ToString(),
                        Item_Value = row["Item_Value"].ToString(),
                        Stock_End = int.Parse(row["Stock_End"].ToString()),
                        Stock_Out = int.Parse(row["Stock_Out"].ToString()),
                        Price = Decimal.Parse(row["Price"].ToString()),
                        Product_ID = row["Product_ID"].ToString(),
                        Product_Name = row["Product_Name"].ToString(),
                        Quantity = short.Parse(row["Quantity"].ToString()),
                        Total = Decimal.Parse(row["Total"].ToString()),
                    });
                    prevProduct_List_ID = row["Product_List_ID"].ToString();
                }

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
        return ListOFProduct_Quantity;
    }



    public static List<dbo_OrderingClass> Search(String Order_Status, String PO_Number,
        DateTime? Date_of_create_order_or_PO_Date_start_Date,
         DateTime? Date_of_create_order_or_PO_Date_end_Date,
         DateTime? Date_of_delivery_goods_start_date, DateTime? Date_of_delivery_goods_end_date, String CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "OrderingSearch";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!String.IsNullOrEmpty(PO_Number))
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
        }

        if (Date_of_create_order_or_PO_Date_start_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Date_of_create_order_or_PO_Date_start_Date", Date_of_create_order_or_PO_Date_start_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Date_of_create_order_or_PO_Date_start_Date", DateTime.Now.AddYears(-10));
        }
        if (Date_of_create_order_or_PO_Date_end_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Date_of_create_order_or_PO_Date_end_Date", Date_of_create_order_or_PO_Date_end_Date.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Date_of_create_order_or_PO_Date_end_Date", DateTime.Now.AddYears(10));
        }
        if (Date_of_delivery_goods_start_date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Date_of_delivery_goods_start_date", Date_of_delivery_goods_start_date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Date_of_delivery_goods_start_date", DateTime.Now.AddYears(-10));
        }
        if (Date_of_delivery_goods_end_date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Date_of_delivery_goods_end_date", Date_of_delivery_goods_end_date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Date_of_delivery_goods_end_date", DateTime.Now.AddYears(10));
        }

        if (!String.IsNullOrEmpty(Order_Status))
        {
            selectCommand.Parameters.AddWithValue("@Order_Status", Order_Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Order_Status", DBNull.Value);
        }
        if (!String.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }




        List<dbo_OrderingClass> item = new List<dbo_OrderingClass>();
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
                    item.Add(new dbo_OrderingClass()
                    {
                        PO_Number = reader["PO_Number"] is DBNull ? null : reader["PO_Number"].ToString(),
                        CV_Code_from_SAP = reader["CV_Code_from_SAP"] is DBNull ? null : reader["CV_Code_from_SAP"].ToString(),
                        Total_Amount_before_vat_included = reader["Total_Amount_before_vat_included"] is DBNull ? null : (Decimal?)reader["Total_Amount_before_vat_included"],
                        Vat_amount = reader["Vat_amount"] is DBNull ? null : (Decimal?)reader["Vat_amount"],
                        Total_amount_after_vat_included = reader["Total_amount_after_vat_included"] is DBNull ? null : (Decimal?)reader["Total_amount_after_vat_included"],
                        Date_of_create_order_or_PO_Date = reader["Date_of_create_order_or_PO_Date"] is DBNull ? null : (DateTime?)reader["Date_of_create_order_or_PO_Date"],
                        Date_of_CP_receive_transaction = reader["Date_of_CP_receive_transaction"] is DBNull ? null : (DateTime?)reader["Date_of_CP_receive_transaction"],
                        Date_of_delivery_goods = reader["Date_of_delivery_goods"] is DBNull ? null : (DateTime?)reader["Date_of_delivery_goods"],
                        Order_Status = reader["Order_Status"] is DBNull ? null : reader["Order_Status"].ToString(),
                        Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString()
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

        }
        finally
        {
            connection.Close();
        }
        return item;
    }


    public static List<dbo_OrderingClass> ExportPOSearch(String CV_Code, DateTime? Date_Begin, DateTime? Date_End,
        String AgentName, String WindowTime, String Status, String Region)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "ExportPOSearch";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (Date_Begin.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Date_Begin", Date_Begin);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Date_Begin", DateTime.Now.AddDays(-7));
        }
        if (Date_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Date_End", Date_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Date_End", DateTime.Now.AddDays(7));
        }
        if (AgentName != null)
        {
            selectCommand.Parameters.AddWithValue("@AgentName", AgentName);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@AgentName", DBNull.Value);
        }
        if (WindowTime != null)
        {
            selectCommand.Parameters.AddWithValue("@WindowTime", WindowTime);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@WindowTime", DBNull.Value);
        }

        if (Status != null)
        {
            selectCommand.Parameters.AddWithValue("@Status", Status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }
        if (CV_Code != null)
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (Region != null)
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }
        //if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value))
        //{
        //    selectCommand.Parameters.AddWithValue("@UserID", System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value);
        //}
        //else
        //{
        //    selectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
        //}


        List<dbo_OrderingClass> item = new List<dbo_OrderingClass>();
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
                    item.Add(new dbo_OrderingClass()
                    {
                        PO_Number = reader["PO_Number"] is DBNull ? null : reader["PO_Number"].ToString(),
                        CV_Code_from_SAP = reader["CV_Code_from_SAP"] is DBNull ? null : reader["CV_Code_from_SAP"].ToString(),
                        Total_Amount_before_vat_included = reader["Total_Amount_before_vat_included"] is DBNull ? null : (Decimal?)reader["Total_Amount_before_vat_included"],
                        Total_amount_after_vat_included = reader["Total_amount_after_vat_included"] is DBNull ? null : (Decimal?)reader["Total_amount_after_vat_included"],
                        Date_of_create_order_or_PO_Date = reader["Date_of_create_order_or_PO_Date"] is DBNull ? null : (DateTime?)reader["Date_of_create_order_or_PO_Date"],
                        Date_of_delivery_goods = reader["Date_of_delivery_goods"] is DBNull ? null : (DateTime?)reader["Date_of_delivery_goods"],
                        Cycle_Date = reader["Cycle_Date"] is DBNull ? null : reader["Cycle_Date"].ToString(),
                        Order_Status = reader["Order_Status"] is DBNull ? null : reader["Order_Status"].ToString(),
                        AgentName = reader["AgentName"] is DBNull ? null : reader["AgentName"].ToString(),
                        Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString(),
                        date_id = reader["date_id"] is DBNull ? null : (DateTime?)reader["date_id"]
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

    public static dbo_OrderingClass Select_Record(String PO_Number)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_OrderingClass clsdbo_Ordering = new dbo_OrderingClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "OrderingSelect";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Ordering.PO_Number = reader["PO_Number"] is DBNull ? null : reader["PO_Number"].ToString();
                clsdbo_Ordering.CV_Code_from_SAP = reader["CV_Code_from_SAP"] is DBNull ? null : reader["CV_Code_from_SAP"].ToString();
                clsdbo_Ordering.Total_Amount_before_vat_included = reader["Total_Amount_before_vat_included"] is DBNull ? null : (Decimal?)reader["Total_Amount_before_vat_included"];
                clsdbo_Ordering.Vat_amount = reader["Vat_amount"] is DBNull ? null : (Decimal?)reader["Vat_amount"];
                clsdbo_Ordering.Total_amount_after_vat_included = reader["Total_amount_after_vat_included"] is DBNull ? null : (Decimal?)reader["Total_amount_after_vat_included"];
                clsdbo_Ordering.Date_of_create_order_or_PO_Date = reader["Date_of_create_order_or_PO_Date"] is DBNull ? null : (DateTime?)reader["Date_of_create_order_or_PO_Date"];
                clsdbo_Ordering.Date_of_CP_receive_transaction = reader["Date_of_CP_receive_transaction"] is DBNull ? null : (DateTime?)reader["Date_of_CP_receive_transaction"];
                clsdbo_Ordering.Date_of_delivery_goods = reader["Date_of_delivery_goods"] is DBNull ? null : (DateTime?)reader["Date_of_delivery_goods"];
                clsdbo_Ordering.Order_Status = reader["Order_Status"] is DBNull ? null : reader["Order_Status"].ToString();
                clsdbo_Ordering.Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString();

            }
            else
            {
                clsdbo_Ordering = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Ordering;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Ordering;
    }

    public static bool Add(dbo_OrderingClass clsdbo_Ordering, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "OrderingInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Ordering.PO_Number != null)
        {
            insertCommand.Parameters.AddWithValue("@PO_Number", clsdbo_Ordering.PO_Number);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
        }
        if (clsdbo_Ordering.CV_Code_from_SAP != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code_from_SAP", clsdbo_Ordering.CV_Code_from_SAP);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code_from_SAP", DBNull.Value);
        }
        if (clsdbo_Ordering.Total_Amount_before_vat_included.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Amount_before_vat_included", clsdbo_Ordering.Total_Amount_before_vat_included);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Amount_before_vat_included", DBNull.Value);
        }
        if (clsdbo_Ordering.Vat_amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat_amount", clsdbo_Ordering.Vat_amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat_amount", DBNull.Value);
        }
        if (clsdbo_Ordering.Total_amount_after_vat_included.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_amount_after_vat_included", clsdbo_Ordering.Total_amount_after_vat_included);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_amount_after_vat_included", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(clsdbo_Ordering.User_ID))
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_Ordering.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }


        //if (clsdbo_Ordering.Date_of_create_order_or_PO_Date.HasValue == true)
        //{
        //    insertCommand.Parameters.AddWithValue("@Date_of_create_order_or_PO_Date", clsdbo_Ordering.Date_of_create_order_or_PO_Date);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@Date_of_create_order_or_PO_Date", DBNull.Value);
        //}
        if (clsdbo_Ordering.Date_of_CP_receive_transaction.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Date_of_CP_receive_transaction", clsdbo_Ordering.Date_of_CP_receive_transaction);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Date_of_CP_receive_transaction", DBNull.Value);
        }


        if (clsdbo_Ordering.Date_of_delivery_goods.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Date_of_delivery_goods", clsdbo_Ordering.Date_of_delivery_goods);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Date_of_delivery_goods", DBNull.Value);
        }


        //if (clsdbo_Ordering.Order_Status != null)
        //{
        //    insertCommand.Parameters.AddWithValue("@Order_Status", clsdbo_Ordering.Order_Status);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@Order_Status", DBNull.Value);
        //}

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

    public static bool Update(dbo_OrderingClass newdbo_OrderingClass, String Last_Modified_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "OrderingUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_OrderingClass.PO_Number != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPO_Number", newdbo_OrderingClass.PO_Number);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPO_Number", DBNull.Value);
        }
        if (newdbo_OrderingClass.CV_Code_from_SAP != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code_from_SAP", newdbo_OrderingClass.CV_Code_from_SAP);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code_from_SAP", DBNull.Value);
        }
        if (newdbo_OrderingClass.Total_Amount_before_vat_included.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Amount_before_vat_included", newdbo_OrderingClass.Total_Amount_before_vat_included);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Amount_before_vat_included", DBNull.Value);
        }
        if (newdbo_OrderingClass.Vat_amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat_amount", newdbo_OrderingClass.Vat_amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat_amount", DBNull.Value);
        }
        if (newdbo_OrderingClass.Total_amount_after_vat_included.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_amount_after_vat_included", newdbo_OrderingClass.Total_amount_after_vat_included);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_amount_after_vat_included", DBNull.Value);
        }
        if (newdbo_OrderingClass.Date_of_create_order_or_PO_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDate_of_create_order_or_PO_Date", newdbo_OrderingClass.Date_of_create_order_or_PO_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDate_of_create_order_or_PO_Date", DBNull.Value);
        }
        if (newdbo_OrderingClass.Date_of_CP_receive_transaction.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDate_of_CP_receive_transaction", newdbo_OrderingClass.Date_of_CP_receive_transaction);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDate_of_CP_receive_transaction", DBNull.Value);
        }
        if (newdbo_OrderingClass.Date_of_delivery_goods.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDate_of_delivery_goods", newdbo_OrderingClass.Date_of_delivery_goods);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDate_of_delivery_goods", DBNull.Value);
        }
        if (newdbo_OrderingClass.Order_Status != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Status", newdbo_OrderingClass.Order_Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Status", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Last_Modified_By))
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
        }
        
        //if (olddbo_OrderingClass.PO_Number != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldPO_Number", olddbo_OrderingClass.PO_Number);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldPO_Number", DBNull.Value);
        //}
        //if (olddbo_OrderingClass.CV_Code_from_SAP != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldCV_Code_from_SAP", olddbo_OrderingClass.CV_Code_from_SAP);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldCV_Code_from_SAP", DBNull.Value);
        //}
        //if (olddbo_OrderingClass.Total_Amount_before_vat_included.HasValue == true)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldTotal_Amount_before_vat_included", olddbo_OrderingClass.Total_Amount_before_vat_included);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldTotal_Amount_before_vat_included", DBNull.Value);
        //}
        //if (olddbo_OrderingClass.Vat_amount.HasValue == true)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldVat_amount", olddbo_OrderingClass.Vat_amount);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldVat_amount", DBNull.Value);
        //}
        //if (olddbo_OrderingClass.Total_amount_after_vat_included.HasValue == true)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldTotal_amount_after_vat_included", olddbo_OrderingClass.Total_amount_after_vat_included);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldTotal_amount_after_vat_included", DBNull.Value);
        //}
        //if (olddbo_OrderingClass.Date_of_create_order_or_PO_Date.HasValue == true)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldDate_of_create_order_or_PO_Date", olddbo_OrderingClass.Date_of_create_order_or_PO_Date);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldDate_of_create_order_or_PO_Date", DBNull.Value);
        //}
        //if (olddbo_OrderingClass.Date_of_CP_receive_transaction.HasValue == true)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldDate_of_CP_receive_transaction", olddbo_OrderingClass.Date_of_CP_receive_transaction);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldDate_of_CP_receive_transaction", DBNull.Value);
        //}
        //if (olddbo_OrderingClass.Date_of_delivery_goods.HasValue == true)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldDate_of_delivery_goods", olddbo_OrderingClass.Date_of_delivery_goods);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldDate_of_delivery_goods", DBNull.Value);
        //}
        //if (olddbo_OrderingClass.Order_Status != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@OldOrder_Status", olddbo_OrderingClass.Order_Status);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@OldOrder_Status", DBNull.Value);
        //}




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

    public static bool Delete(string PO_Number)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[OrderingDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        if (PO_Number != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldPO_Number", PO_Number);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPO_Number", DBNull.Value);
        }


        //if (clsdbo_Ordering.CV_Code_from_SAP != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCV_Code_from_SAP", clsdbo_Ordering.CV_Code_from_SAP);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCV_Code_from_SAP", DBNull.Value);
        //}
        //if (clsdbo_Ordering.Total_Amount_before_vat_included.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldTotal_Amount_before_vat_included", clsdbo_Ordering.Total_Amount_before_vat_included);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldTotal_Amount_before_vat_included", DBNull.Value);
        //}
        //if (clsdbo_Ordering.Vat_amount.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldVat_amount", clsdbo_Ordering.Vat_amount);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldVat_amount", DBNull.Value);
        //}
        //if (clsdbo_Ordering.Total_amount_after_vat_included.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldTotal_amount_after_vat_included", clsdbo_Ordering.Total_amount_after_vat_included);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldTotal_amount_after_vat_included", DBNull.Value);
        //}
        //if (clsdbo_Ordering.Date_of_create_order_or_PO_Date.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldDate_of_create_order_or_PO_Date", clsdbo_Ordering.Date_of_create_order_or_PO_Date);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldDate_of_create_order_or_PO_Date", DBNull.Value);
        //}
        //if (clsdbo_Ordering.Date_of_CP_receive_transaction.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldDate_of_CP_receive_transaction", clsdbo_Ordering.Date_of_CP_receive_transaction);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldDate_of_CP_receive_transaction", DBNull.Value);
        //}
        //if (clsdbo_Ordering.Date_of_delivery_goods.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldDate_of_delivery_goods", clsdbo_Ordering.Date_of_delivery_goods);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldDate_of_delivery_goods", DBNull.Value);
        //}
        //if (clsdbo_Ordering.Order_Status != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Status", clsdbo_Ordering.Order_Status);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Status", DBNull.Value);
        //}




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

