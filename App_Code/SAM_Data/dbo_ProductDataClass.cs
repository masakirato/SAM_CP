using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using log4net;

public class dbo_ProductDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    [Obsolete]
    public static List<dbo_ProductClass> _GetProduct()
    {
        List<dbo_ProductClass> listofProduct = new List<dbo_ProductClass>();

        listofProduct.Add(new dbo_ProductClass()
        {
            Product_ID = "72001069",
            Product_Name = "PM 180 โกลด์ แอดวานซ์จืด",
            Unit_of_item_ID = "ขวด"
        });
        listofProduct.Add(new dbo_ProductClass()
        {
            Product_ID = "72001070",
            Product_Name = "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง",
            Unit_of_item_ID = "ขวด"
        });
        listofProduct.Add(new dbo_ProductClass()
        {
            Product_ID = "72000720",
            Product_Name = "PM 200 เมล่อนญี่ปุ่น",
            Unit_of_item_ID = "ขวด"
        });
        listofProduct.Add(new dbo_ProductClass()
        {
            Product_ID = "72000723",
            Product_Name = "PM 200 ช็อคโกแลต",
            Unit_of_item_ID = "ขวด"
        });
        listofProduct.Add(new dbo_ProductClass()
        {
            Product_ID = "72000724",
            Product_Name = "PM 200 กาแฟ",
            Unit_of_item_ID = "ขวด"
        });


        return listofProduct;
    }

    [Obsolete]
    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ProductSelectAll]";
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

    public static List<dbo_ProductClass> Search(string Product_ID, string Product_Name, string Product_Group, int? Size, string Unit_of_item_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "ProductSearch";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        if (!string.IsNullOrEmpty(Product_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", Product_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Product_Name))
        {
            selectCommand.Parameters.AddWithValue("@Product_Name", Product_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_Name", DBNull.Value);
        }
        switch (Product_Group)
        {
            case "นมสดพาสเจอร์ไรส์":
                selectCommand.Parameters.AddWithValue("@Product_Group", "นมสดพาสเจอร์ไรส์");
                break;
            case "นมเปรี้ยว":
                selectCommand.Parameters.AddWithValue("@Product_Group", "นมเปรี้ยว");
                break;
            case "โยเกิร์ตเมจิ":
                selectCommand.Parameters.AddWithValue("@Product_Group", "โยเกิร์ตเมจิ");
                break;
            case "นมเปรี้ยวไพเกน":
                selectCommand.Parameters.AddWithValue("@Product_Group", "นมเปรี้ยวไพเกน");
                break;
            case "อื่นๆ":
                selectCommand.Parameters.AddWithValue("@Product_Group", "อื่นๆ");
                break;
            default:
                selectCommand.Parameters.AddWithValue("@Product_Group", DBNull.Value);
                break;
        }
        if (Size != null)
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }


        if (!string.IsNullOrEmpty(Unit_of_item_ID))
        {
            selectCommand.Parameters.AddWithValue("@Unit_of_item_ID", Unit_of_item_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Unit_of_item_ID", DBNull.Value);
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
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_ProductClass()
                    {
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"],
                        Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                        Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Order_No = reader["Order_No"] is DBNull ? null : (Int16?)reader["Order_No"],
                        Packing_Size = reader["Packing_Size"] is DBNull ? null : (Byte?)reader["Packing_Size"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"],
                        SAP_Product_Code = reader["SAP_Product_Code"] is DBNull ? null : reader["SAP_Product_Code"].ToString()
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
    public static List<dbo_ProductClass> Search(string Product_ID, string Product_Name, int? Size, string Unit_of_item_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "ProductSearch";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        if (!string.IsNullOrEmpty(Product_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", Product_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Product_Name))
        {
            selectCommand.Parameters.AddWithValue("@Product_Name", Product_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_Name", DBNull.Value);
        }

        if (Size != null)
        {
            selectCommand.Parameters.AddWithValue("@Size", Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }


        if (!string.IsNullOrEmpty(Unit_of_item_ID))
        {
            selectCommand.Parameters.AddWithValue("@Unit_of_item_ID", Unit_of_item_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Unit_of_item_ID", DBNull.Value);
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
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_ProductClass()
                    {
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"],
                        Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                        Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Order_No = reader["Order_No"] is DBNull ? null : (Int16?)reader["Order_No"],
                        Packing_Size = reader["Packing_Size"] is DBNull ? null : (Byte?)reader["Packing_Size"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"],
                        SAP_Product_Code = reader["SAP_Product_Code"] is DBNull ? null : reader["SAP_Product_Code"].ToString()
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
    [Obsolete]
    public static DataTable Search(dbo_ProductClass olddbo_ProductClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ProductSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        if (!string.IsNullOrEmpty(olddbo_ProductClass.Product_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", olddbo_ProductClass.Product_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(olddbo_ProductClass.Product_Name))
        {
            selectCommand.Parameters.AddWithValue("@Product_Name", olddbo_ProductClass.Product_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_Name", DBNull.Value);
        }
        if (olddbo_ProductClass.Size != null)
        {
            selectCommand.Parameters.AddWithValue("@Size", olddbo_ProductClass.Size);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(olddbo_ProductClass.Unit_of_item_ID))
        {
            selectCommand.Parameters.AddWithValue("@Unit_of_item_ID", olddbo_ProductClass.Unit_of_item_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Unit_of_item_ID", DBNull.Value);
        }



        selectCommand.Parameters.AddWithValue("@Packing_Size", DBNull.Value);



        selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@EAN", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@CP_Meiji_Price", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@Point", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@Exclude_Vat", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@Vat", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@Order_No", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@Quantity_in__carte", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);

        selectCommand.Parameters.AddWithValue("@SearchCondition", "Equals");
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
        string selectProcedure = "[dbo].[ProductSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Product I D")
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (sField == "Product Name")
        {
            selectCommand.Parameters.AddWithValue("@Product_Name", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_Name", DBNull.Value);
        }
        if (sField == "Size")
        {
            selectCommand.Parameters.AddWithValue("@Size", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }
        if (sField == "Unit Of Item I D")
        {
            selectCommand.Parameters.AddWithValue("@Unit_of_item_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Unit_of_item_ID", DBNull.Value);
        }
        if (sField == "Product Group I D")
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }
        if (sField == "E A N")
        {
            selectCommand.Parameters.AddWithValue("@EAN", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EAN", DBNull.Value);
        }
        if (sField == "C P Meiji Price")
        {
            selectCommand.Parameters.AddWithValue("@CP_Meiji_Price", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CP_Meiji_Price", DBNull.Value);
        }
        if (sField == "Point")
        {
            selectCommand.Parameters.AddWithValue("@Point", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Point", DBNull.Value);
        }
        if (sField == "Exclude Vat")
        {
            selectCommand.Parameters.AddWithValue("@Exclude_Vat", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Exclude_Vat", DBNull.Value);
        }
        if (sField == "Vat")
        {
            selectCommand.Parameters.AddWithValue("@Vat", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (sField == "Order No")
        {
            selectCommand.Parameters.AddWithValue("@Order_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Order_No", DBNull.Value);
        }
        if (sField == "Quantity In  Carte")
        {
            selectCommand.Parameters.AddWithValue("@Quantity_in__carte", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Quantity_in__carte", DBNull.Value);
        }
        if (sField == "Packing Size")
        {
            selectCommand.Parameters.AddWithValue("@Packing_Size", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Packing_Size", DBNull.Value);
        }
        if (sField == "Status")
        {
            selectCommand.Parameters.AddWithValue("@Status", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
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


    public static Dictionary<string, string> Get_Unit()
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[Get_Unit]";
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

                foreach (DataRow row in dt.Rows)
                {
                    unit.Add(row["Item_Value_ID"].ToString(), row["Item_Value"].ToString());
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
        return unit;
    }


    public static Dictionary<string, string> Product_Group()
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[Product_Group]";
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

                foreach (DataRow row in dt.Rows)
                {
                    unit.Add(row["Item_Value_ID"].ToString(), row["Item_Value"].ToString());
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
        return unit;
    }


    public static Dictionary<string, string> Get_Product_List_By_Product_Group(string product_group_id)
    {

        Dictionary<string, string> product_group = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[Get_Product_List_By_Product_Group]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Product_group_ID", product_group_id);

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);

                foreach (DataRow row in dt.Rows)
                {
                    product_group.Add(row["Product_List_ID"].ToString(), row["Product_Name"].ToString());
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
        return product_group;
    }

    public static List<dbo_ProductClass> GetProductByProductGroupID(String Product_group_ID, String CV_Code, String PO_Number, DateTime? PriceDate)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetProductByProductGroupID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(PO_Number))
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
        }


        if (PriceDate.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@PriceDate", PriceDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PriceDate", DBNull.Value);
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
                int index = 0;
                foreach (DataRow reader in dt.Rows)
                {

                    if (prevSize != (Int16?)reader["Size"] && Product_group_ID != "อื่นๆ")
                    {
                        item.Add(new dbo_ProductClass()
                        {
                            Product_ID = "Merge",

                            Product_Name = string.Format("{0} ({1}{2}/ลัง)", ((Int16?)reader["Size"]).ToString() + " CC.", (reader["Packing_Size"]).ToString(), (reader["Unit_of_item_ID"]).ToString())
                            ,
                            index = 1
                        });
                    }


                    item.Add(new dbo_ProductClass()
                    {
                        index = index++,

                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"],
                        Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                        Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                        Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"],
                        Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Order_No = reader["Order_No"] is DBNull ? null : (Int16?)reader["Order_No"],
                        Packing_Size = 0,
                        Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"],

                        Status = null,
                        Stock = reader["Stock"] is DBNull ? null : (Int16?)reader["Stock"],
                        CP_Meiji_Price = 0,
                        Suggestion_Qty = reader["Suggestion_Qty"] is DBNull ? null : (Int16?)reader["Suggestion_Qty"]

                        // Exclude_Vat = false



                        /*
                        
                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        Exclude_Vat = reader["Exclude_Vat"] is DBNull ? false : (Boolean?)reader["Exclude_Vat"],
                        Vat = reader["Vat"] is DBNull ? null : (Int16?)reader["Vat"],
                        Order_No = reader["Order_No"] is DBNull ? null : (Int16?)reader["Order_No"],
                        Packing_Size = reader["Packing_Size"] is DBNull ? null : (Byte?)reader["Packing_Size"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Quantity = reader["Quantity"] is DBNull ? 0 : (Int16?)reader["Quantity"],
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                        Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                        Stock = reader["Stock"] is DBNull ? 0 : (Int16?)reader["Stock"]
                        */



                    });


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
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static List<dbo_ProductClass> GetProductByProductGroupID(String Product_group_ID, String CV_Code, String PO_Number, DateTime? PriceDate, DateTime? DeliveryDate)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        //string selectProcedure = "GetProductByProductGroupID";
        string selectProcedure = "GetProductByProductGroupID_New";//"GetProductByProductGroupID_New";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(PO_Number))
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
        }


        if (PriceDate.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@PriceDate", PriceDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PriceDate", DBNull.Value);
        }


        if (DeliveryDate.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
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
                string prevProduct = "";
                int index = 1;

                foreach (DataRow reader in dt.Rows)
                {

                    if (prevSize != (Int16?)reader["Size"] && Product_group_ID != "อื่นๆ")
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

                    if (prevProduct != (string)reader["Product_ID"])
                    {

                        item.Add(new dbo_ProductClass()
                        {
                            index = index++,
                            Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                            Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                            Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"],
                            Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                            Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                            Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                            Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                            Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"],
                            Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                            Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                            Order_No = reader["Order_No"] is DBNull ? null : (Int16?)reader["Order_No"],
                            Packing_Size = 0,
                            Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"],
                            Status = null,
                            Stock = reader["Stock"] is DBNull ? null : (Int16?)reader["Stock"],
                            CP_Meiji_Price = 0,
                            Suggestion_Qty = reader["Suggestion_Qty"] is DBNull ? null : (Int16?)reader["Suggestion_Qty"],
                            Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? DateTime.Now.AddYears(-1) : (DateTime?)reader["Start_Effective_Date"]
                        });
                    }


                    prevSize = reader["Size"] is DBNull ? null : (Int16?)reader["Size"];
                    prevProduct = reader["Product_ID"] is DBNull ? null : (string)reader["Product_ID"];
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

    public static List<dbo_ProductClass> GetProductByProductGroupID_View(String Product_group_ID, String CV_Code, String PO_Number, DateTime? PriceDate, DateTime? DeliveryDate)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        //string selectProcedure = "GetProductByProductGroupID";
        string selectProcedure = "GetProductByProductGroupID_View";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(PO_Number))
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
        }


        if (PriceDate.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@PriceDate", PriceDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PriceDate", DBNull.Value);
        }


        if (DeliveryDate.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
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
                string prevProduct = "";
                int index = 1;
                foreach (DataRow reader in dt.Rows)
                {

                    if (prevSize != (Int16?)reader["Size"] && Product_group_ID != "อื่นๆ")
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

                    if (prevProduct != (string)reader["Product_ID"])
                    {
                        item.Add(new dbo_ProductClass()
                        {
                            index = index++,

                            Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                            Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                            Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"],
                            Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                            Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                            Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                            Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                            Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"],
                            Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                            Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                            Order_No = reader["Order_No"] is DBNull ? null : (Int16?)reader["Order_No"],
                            Packing_Size = 0,
                            Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"],

                            Status = null,
                            Stock = reader["Stock"] is DBNull ? null : (Int16?)reader["Stock"],
                            CP_Meiji_Price = 0,
                            Suggestion_Qty = reader["Suggestion_Qty"] is DBNull ? null : (Int16?)reader["Suggestion_Qty"]
                             ,
                            Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? DateTime.Now.AddYears(-1) : (DateTime?)reader["Start_Effective_Date"]

                        });
                    }


                    prevSize = reader["Size"] is DBNull ? null : (Int16?)reader["Size"];
                    prevProduct = reader["Product_ID"] is DBNull ? null : (string)reader["Product_ID"];
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

    public static List<dbo_ProductClass> SelectProductByProductGroupID(String Product_group_ID, String PO_Number, String CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "SelectProductByProductGroupID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(PO_Number))
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
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
                int index = 0;
                foreach (DataRow reader in dt.Rows)
                {

                    if (prevSize != (Int16?)reader["Size"] && Product_group_ID != "อื่นๆ")
                    {
                        item.Add(new dbo_ProductClass()
                        {
                            Product_ID = "Merge",
                            Product_Name = ((Int16?)reader["Size"]).ToString() + " CC."
                        });
                    }


                    item.Add(new dbo_ProductClass()
                    {
                        index = index++,
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"],
                        Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                        Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString(),
                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],


                        //  Exclude_Vat = reader["Exclude_Vat"] is DBNull ? null : (Boolean?)reader["Exclude_Vat"],


                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],


                        Order_No = reader["Order_No"] is DBNull ? null : (Int16?)reader["Order_No"],
                        Quantity_in__carte = reader["Quantity_in__carte"] is DBNull ? null : (Byte?)reader["Quantity_in__carte"],
                        Packing_Size = reader["Packing_Size"] is DBNull ? null : (Byte?)reader["Packing_Size"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Quantity = reader["Quantity"] is DBNull ? 0 : (Int16?)reader["Quantity"],


                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                        Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"]

                    });


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
        finally
        {
            connection.Close();
        }
        return item;
    }



    public static List<dbo_ProductClass> SelectBillingByProductGroupID(String Product_group_ID, String Billing_ID, String CV_Code, DateTime? OrderDate)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "SelectBillingByProductGroupID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Billing_ID))
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", Billing_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (OrderDate.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@OrderDate", OrderDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@OrderDate", DBNull.Value);
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
                            Start_Effective_Date = DateTime.Now.AddYears(-1)
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
                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],
                      //  Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],

                        Vat = 0,

                        Order_No = 0,
                        Packing_Size = 0,
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),


                        Qty = reader["Qty"] is DBNull ? 0 : (Int16?)reader["Qty"],

                        Billing_Detail_ID = reader["Billing_Detail_ID"] is DBNull ? null : reader["Billing_Detail_ID"].ToString(),
                        Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString(),



                        Net_Value = reader["Net_Value"] is DBNull ? null : (Decimal?)reader["Net_Value"],
                        Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"]

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


    public static dbo_ProductClass Select_Record(string Product_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[ProductSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        selectCommand.Parameters.AddWithValue("@Product_ID", Product_ID);



        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Product.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_Product.Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString();
                clsdbo_Product.Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"];
                clsdbo_Product.Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString();
                clsdbo_Product.Product_group_ID = reader["Product_group_ID"] is DBNull ? null : reader["Product_group_ID"].ToString();


                //clsdbo_Product.EAN = reader["EAN"] is DBNull ? null : reader["EAN"].ToString();


                clsdbo_Product.CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"];
                clsdbo_Product.Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"];
                clsdbo_Product.Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"];
                clsdbo_Product.Order_No = reader["Order_No"] is DBNull ? null : (Int16?)reader["Order_No"];
                clsdbo_Product.Packing_Size = reader["Packing_Size"] is DBNull ? null : (Byte?)reader["Packing_Size"];
                clsdbo_Product.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
                clsdbo_Product.Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"];
                clsdbo_Product.SP_Price = reader["SP_Price"] is DBNull ? null : (Decimal?)reader["SP_Price"];
                clsdbo_Product.SAP_Product_Code = reader["SAP_Product_Code"] is DBNull ? null : reader["SAP_Product_Code"].ToString();
                clsdbo_Product.Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"];


            }
            else
            {
                clsdbo_Product = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Product;
        }
        catch (Exception ex)
        {

        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Product;
    }

    public static bool Add(dbo_ProductClass clsdbo_Product, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "ProductInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;

        // 1
        if (clsdbo_Product.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_Product.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }

        //2
        if (clsdbo_Product.Product_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_Name", clsdbo_Product.Product_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_Name", DBNull.Value);
        }

        //3
        if (clsdbo_Product.Size.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Size", clsdbo_Product.Size);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Size", DBNull.Value);
        }

        //4
        if (clsdbo_Product.Unit_of_item_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Unit_of_item_ID", clsdbo_Product.Unit_of_item_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Unit_of_item_ID", DBNull.Value);
        }

        //5
        if (clsdbo_Product.Product_group_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_group_ID", clsdbo_Product.Product_group_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }

        //6
        if (clsdbo_Product.EAN != null)
        {
            insertCommand.Parameters.AddWithValue("@EAN", clsdbo_Product.EAN);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@EAN", DBNull.Value);
        }

        //7
        if (clsdbo_Product.CP_Meiji_Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@CP_Meiji_Price", clsdbo_Product.CP_Meiji_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CP_Meiji_Price", DBNull.Value);
        }

        //8
        if (clsdbo_Product.Point.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Point", clsdbo_Product.Point);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Point", DBNull.Value);
        }


        //if (clsdbo_Product.Exclude_Vat.HasValue == true)
        //{
        //    insertCommand.Parameters.AddWithValue("@Exclude_Vat", clsdbo_Product.Exclude_Vat);
        //}
        //else
        //{
        insertCommand.Parameters.AddWithValue("@Exclude_Vat", DBNull.Value);
        //}

        //10
        if (clsdbo_Product.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_Product.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }

        //11
        if (clsdbo_Product.Order_No.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Order_No", clsdbo_Product.Order_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_No", DBNull.Value);
        }

        //12
        if (clsdbo_Product.Quantity_in__carte.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Quantity_in__carte", clsdbo_Product.Quantity_in__carte);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Quantity_in__carte", DBNull.Value);
        }

        //13
        if (clsdbo_Product.Packing_Size.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Packing_Size", clsdbo_Product.Packing_Size);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Packing_Size", DBNull.Value);
        }

        if (clsdbo_Product.SAP_Product_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@SAP_Product_Code", clsdbo_Product.SAP_Product_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SAP_Product_Code", DBNull.Value);
        }

        if (clsdbo_Product.SP_Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@SP_Price", clsdbo_Product.SP_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SP_Price", DBNull.Value);
        }

        if (clsdbo_Product.Agent_Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Agent_Price", clsdbo_Product.Agent_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Agent_Price", DBNull.Value);
        }


        switch (clsdbo_Product.Status)
        {
            case "Active":
                insertCommand.Parameters.AddWithValue("@Status", true);
                break;
            case "In active":
                insertCommand.Parameters.AddWithValue("@Status", false);
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                break;
        }

        if (clsdbo_Product.Photo != null)
        {
            insertCommand.Parameters.AddWithValue("@Photo", clsdbo_Product.Photo);
        }
        else
        {
            insertCommand.Parameters.Add("@Photo", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            //insertCommand.Parameters.AddWithValue("@Photo",SqlDbType.VarBinary);
        }


        if (Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }




        //14
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


    public static bool Update(dbo_ProductClass newdbo_ProductClass, String Last_Modified_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "ProductUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;


        // 1
        if (newdbo_ProductClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_ProductClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        //2
        if (newdbo_ProductClass.Product_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_Name", newdbo_ProductClass.Product_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_Name", DBNull.Value);
        }
        //3
        if (newdbo_ProductClass.Size.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSize", newdbo_ProductClass.Size);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSize", DBNull.Value);
        }
        //4
        if (newdbo_ProductClass.Unit_of_item_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUnit_of_item_ID", newdbo_ProductClass.Unit_of_item_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUnit_of_item_ID", DBNull.Value);
        }



        //5
        if (newdbo_ProductClass.Product_group_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_group_ID", newdbo_ProductClass.Product_group_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_group_ID", DBNull.Value);
        }
        //6


        if (newdbo_ProductClass.CP_Meiji_Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCP_Meiji_Price", newdbo_ProductClass.CP_Meiji_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCP_Meiji_Price", DBNull.Value);
        }
        //7
        if (newdbo_ProductClass.Point.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", newdbo_ProductClass.Point);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", DBNull.Value);
        }
        //8
        if (newdbo_ProductClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_ProductClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        //9
        if (newdbo_ProductClass.Order_No.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_No", newdbo_ProductClass.Order_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_No", DBNull.Value);
        }
        //10
        if (newdbo_ProductClass.Packing_Size.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPacking_Size", newdbo_ProductClass.Packing_Size);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPacking_Size", DBNull.Value);
        }
        //11
        switch (newdbo_ProductClass.Status)
        {
            case "Active":
                updateCommand.Parameters.AddWithValue("@Status", true);
                break;
            case "In active":
                updateCommand.Parameters.AddWithValue("@Status", false);
                break;
            default:
                updateCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                break;
        }
        //12
        if (newdbo_ProductClass.SAP_Product_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@SAP_Product_Code", newdbo_ProductClass.SAP_Product_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@SAP_Product_Code", DBNull.Value);
        }
        //13
        if (newdbo_ProductClass.SP_Price.HasValue)
        {
            updateCommand.Parameters.AddWithValue("@SP_Price", newdbo_ProductClass.SP_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@SP_Price", DBNull.Value);
        }
        //14
        if (newdbo_ProductClass.Agent_Price.HasValue)
        {
            updateCommand.Parameters.AddWithValue("@Agent_Price", newdbo_ProductClass.Agent_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Agent_Price", DBNull.Value);
        }

        if (newdbo_ProductClass.Photo != null)
        {
            updateCommand.Parameters.AddWithValue("@Photo", newdbo_ProductClass.Photo);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Photo", System.Data.SqlTypes.SqlBinary.Null);
        }


        if (Last_Modified_By != null)
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

    public static bool Delete(string Product_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[ProductDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        if (Product_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", Product_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", DBNull.Value);
        }



        //if (clsdbo_Product.Product_Name != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldProduct_Name", clsdbo_Product.Product_Name);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldProduct_Name", DBNull.Value);
        //}
        //if (clsdbo_Product.Size.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldSize", clsdbo_Product.Size);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldSize", DBNull.Value);
        //}
        //if (clsdbo_Product.Unit_of_item_ID != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldUnit_of_item_ID", clsdbo_Product.Unit_of_item_ID);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldUnit_of_item_ID", DBNull.Value);
        //}
        //if (clsdbo_Product.Product_group_ID != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldProduct_group_ID", clsdbo_Product.Product_group_ID);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldProduct_group_ID", DBNull.Value);
        //}
        //if (clsdbo_Product.EAN != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldEAN", clsdbo_Product.EAN);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldEAN", DBNull.Value);
        //}
        //if (clsdbo_Product.CP_Meiji_Price.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCP_Meiji_Price", clsdbo_Product.CP_Meiji_Price);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCP_Meiji_Price", DBNull.Value);
        //}
        //if (clsdbo_Product.Point.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPoint", clsdbo_Product.Point);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPoint", DBNull.Value);
        //}
        //if (clsdbo_Product.Exclude_Vat.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldExclude_Vat", clsdbo_Product.Exclude_Vat);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldExclude_Vat", DBNull.Value);
        //}
        //if (clsdbo_Product.Vat.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldVat", clsdbo_Product.Vat);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        //}
        //if (clsdbo_Product.Order_No.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_No", clsdbo_Product.Order_No);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_No", DBNull.Value);
        //}
        //if (clsdbo_Product.Quantity_in__carte.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldQuantity_in__carte", clsdbo_Product.Quantity_in__carte);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldQuantity_in__carte", DBNull.Value);
        //}
        //if (clsdbo_Product.Packing_Size.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPacking_Size", clsdbo_Product.Packing_Size);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPacking_Size", DBNull.Value);
        //}
        //switch (clsdbo_Product.Status)
        //{
        //    case "Active":
        //        deleteCommand.Parameters.AddWithValue("@OldStatus", true);
        //        break;
        //    case "InActive":
        //        deleteCommand.Parameters.AddWithValue("@OldStatus", false);
        //        break;
        //    default:
        //        deleteCommand.Parameters.AddWithValue("@OldStatus", DBNull.Value);
        //        break;
        //}


        //if (clsdbo_Product.Status == true)
        //{

        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldStatus", DBNull.Value);
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

