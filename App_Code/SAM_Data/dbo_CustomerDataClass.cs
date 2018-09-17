using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;

public class dbo_CustomerDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



    public static List<dbo_CustomerClass> SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CustomerSelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        List<dbo_CustomerClass> item = new List<dbo_CustomerClass>();

        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_CustomerClass()
                    {
                        Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString()
               ,
                        Customer_Type = reader["Customer_Type"] is DBNull ? null : reader["Customer_Type"].ToString()
               ,
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString()
               ,
                        Residence_Type_ID = reader["Residence_Type_ID"] is DBNull ? null : reader["Residence_Type_ID"].ToString()
               ,
                        First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString()
               ,
                        Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString()
               ,
                        Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString()
               ,
                        Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString()
               ,
                        Contact_Name = reader["Contact_Name"] is DBNull ? null : reader["Contact_Name"].ToString()
               ,
                        Birthday = reader["Birthday"] is DBNull ? null : ((DateTime?)reader["Birthday"])
               ,
                        Home_House_No = reader["Home_House_No"] is DBNull ? null : reader["Home_House_No"].ToString()
               ,
                        Home_Tower = reader["Home_Tower"] is DBNull ? null : reader["Home_Tower"].ToString()
               ,
                        Home_Village = reader["Home_Village"] is DBNull ? null : reader["Home_Village"].ToString()
               ,
                        Home_Village_No = reader["Home_Village_No"] is DBNull ? null : reader["Home_Village_No"].ToString()
               ,
                        Home_Alley = reader["Home_Alley"] is DBNull ? null : reader["Home_Alley"].ToString()
               ,
                        Home_Road = reader["Home_Road"] is DBNull ? null : reader["Home_Road"].ToString()
               ,
                        Home_Sub_district = reader["Home_Sub_district"] is DBNull ? null : reader["Home_Sub_district"].ToString()
               ,
                        Home_District = reader["Home_District"] is DBNull ? null : reader["Home_District"].ToString()
               ,
                        Home_Province = reader["Home_Province"] is DBNull ? null : reader["Home_Province"].ToString()
               ,
                        Home_Postal_ID = reader["Home_Postal_ID"] is DBNull ? null : reader["Home_Postal_ID"].ToString()
               ,
                        Shipment_House_No = reader["Shipment_House_No"] is DBNull ? null : reader["Shipment_House_No"].ToString()
               ,
                        Shipment_Tower = reader["Shipment_Tower"] is DBNull ? null : reader["Shipment_Tower"].ToString()
               ,
                        Shipment_Village = reader["Shipment_Village"] is DBNull ? null : reader["Shipment_Village"].ToString()
               ,
                        Shipment_Village_No = reader["Shipment_Village_No"] is DBNull ? null : reader["Shipment_Village_No"].ToString()
               ,
                        Shipment_Alley = reader["Shipment_Alley"] is DBNull ? null : reader["Shipment_Alley"].ToString()
               ,
                        Shipment_Road = reader["Shipment_Road"] is DBNull ? null : reader["Shipment_Road"].ToString()
               ,
                        Shipment_Sub_district = reader["Shipment_Sub_district"] is DBNull ? null : reader["Shipment_Sub_district"].ToString()
               ,
                        Shipment_District = reader["Shipment_District"] is DBNull ? null : reader["Shipment_District"].ToString()
               ,
                        Shipment_Province = reader["Shipment_Province"] is DBNull ? null : reader["Shipment_Province"].ToString()
               ,
                        Shipment_Postal_ID = reader["Shipment_Postal_ID"] is DBNull ? null : reader["Shipment_Postal_ID"].ToString()
               ,
                        Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString()
               ,
                        SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString()
               ,
                        SP_Name = reader["SP_Name"] is DBNull ? null : reader["SP_Name"].ToString()
               ,
                        Payment_Type = reader["Payment_Type"] is DBNull ? null : reader["Payment_Type"].ToString()
               ,
                        Billing_Type = reader["Billing_Type"] is DBNull ? null : reader["Billing_Type"].ToString()
               ,
                        Billing_Day_of_Week = reader["Billing_Day_of_Week"] is DBNull ? null : reader["Billing_Day_of_Week"].ToString()
               ,
                        Due_Billing_Day_of_Week = reader["Due_Billing_Day_of_Week"] is DBNull ? null : reader["Due_Billing_Day_of_Week"].ToString()
               ,
                        Billing_Day_of_Month = reader["Billing_Day_of_Month"] is DBNull ? null : reader["Billing_Day_of_Month"].ToString()
               ,
                        Due_Billing_Day_of_Month = reader["Due_Billing_Day_of_Month"] is DBNull ? null : reader["Due_Billing_Day_of_Month"].ToString()
               ,
                        Billing_Day_of_Other = reader["Billing_Day_of_Other"] is DBNull ? null : reader["Billing_Day_of_Other"].ToString()
               ,
                        Due_Billing_Day_of_Other = reader["Due_Billing_Day_of_Other"] is DBNull ? null : reader["Due_Billing_Day_of_Other"].ToString()
               ,
                        Credit_Term = reader["Credit_Term"] is DBNull ? null : reader["Credit_Term"].ToString()
               ,
                        Credit_Limit = reader["Credit_Limit"] is DBNull ? null : (Int32?)reader["Credit_Limit"]
               ,
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString()
               ,
                        Replace_Sales = reader["Replace_Sales"] is DBNull ? null : reader["Replace_Sales"].ToString()
               ,
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString()

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

    public static List<dbo_CustomerClass> Search(string First_Name, string Customer_Type, string Customer_ID, string Residence_Type_ID, string Home_House_No, string Mobile, string SP_ID, string Status, string CV_Code)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "CustomerSearch";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        switch (Customer_Type)
        {
            case "ทั่วไป":
                selectCommand.Parameters.AddWithValue("@Customer_Type", true);
                break;
            case "สมาชิก":
                selectCommand.Parameters.AddWithValue("@Customer_Type", false);
                break;
            default:
                selectCommand.Parameters.AddWithValue("@Customer_Type", DBNull.Value);
                break;
        }



        if (!string.IsNullOrEmpty(First_Name))
        {
            selectCommand.Parameters.AddWithValue("@First_Name", First_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@First_Name", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Customer_ID))
        {
            selectCommand.Parameters.AddWithValue("@Customer_ID", Customer_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Customer_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Residence_Type_ID))
        {
            selectCommand.Parameters.AddWithValue("@Residence_Type_ID", Residence_Type_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Residence_Type_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }

        switch (Status)
        {
            case "ยังติดต่ออยู่":
                selectCommand.Parameters.AddWithValue("@Status", 1);
                break;
            case "ขาดการติดต่อ":
                selectCommand.Parameters.AddWithValue("@Status", 2);
                break;
            case "ระงับการส่งชั่วคราว":
                selectCommand.Parameters.AddWithValue("@Status", 3);
                break;
            default:
                selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                break;
        }


        if (Mobile != null)
        {
            selectCommand.Parameters.AddWithValue("@Mobile", Mobile);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Mobile", DBNull.Value);
        }

        if (Home_House_No != null)
        {
            selectCommand.Parameters.AddWithValue("@Home_House_No", Home_House_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Home_House_No", DBNull.Value);
        }

        if (CV_Code != null)
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }


        List<dbo_CustomerClass> item = new List<dbo_CustomerClass>();

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
                    item.Add(new dbo_CustomerClass()
                    {
                        Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString()
               ,
                        Customer_Type = reader["Customer_Type"] is DBNull ? null : reader["Customer_Type"].ToString()
               ,
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString()
               ,
                        Residence_Type_ID = reader["Residence_Type_ID"] is DBNull ? null : reader["Residence_Type_ID"].ToString()
               ,
                        First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString()
               ,
                        Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString()
               ,
                        Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString()
               ,
                        Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString()
               ,
                        Contact_Name = reader["Contact_Name"] is DBNull ? null : reader["Contact_Name"].ToString()
               ,
                        Birthday = reader["Birthday"] is DBNull ? null : ((DateTime?)reader["Birthday"])
               ,
                        Home_House_No = reader["Home_House_No"] is DBNull ? null : reader["Home_House_No"].ToString()
               ,
                        Home_Tower = reader["Home_Tower"] is DBNull ? null : reader["Home_Tower"].ToString()
               ,
                        Home_Village = reader["Home_Village"] is DBNull ? null : reader["Home_Village"].ToString()
               ,
                        Home_Village_No = reader["Home_Village_No"] is DBNull ? null : reader["Home_Village_No"].ToString()
               ,
                        Home_Alley = reader["Home_Alley"] is DBNull ? null : reader["Home_Alley"].ToString()
               ,
                        Home_Road = reader["Home_Road"] is DBNull ? null : reader["Home_Road"].ToString()
               ,
                        Home_Sub_district = reader["Home_Sub_district"] is DBNull ? null : reader["Home_Sub_district"].ToString()
               ,
                        Home_District = reader["Home_District"] is DBNull ? null : reader["Home_District"].ToString()
               ,
                        Home_Province = reader["Home_Province"] is DBNull ? null : reader["Home_Province"].ToString()
               ,
                        Home_Postal_ID = reader["Home_Postal_ID"] is DBNull ? null : reader["Home_Postal_ID"].ToString()
               ,
                        Shipment_House_No = reader["Shipment_House_No"] is DBNull ? null : reader["Shipment_House_No"].ToString()
               ,
                        Shipment_Tower = reader["Shipment_Tower"] is DBNull ? null : reader["Shipment_Tower"].ToString()
               ,
                        Shipment_Village = reader["Shipment_Village"] is DBNull ? null : reader["Shipment_Village"].ToString()
               ,
                        Shipment_Village_No = reader["Shipment_Village_No"] is DBNull ? null : reader["Shipment_Village_No"].ToString()
               ,
                        Shipment_Alley = reader["Shipment_Alley"] is DBNull ? null : reader["Shipment_Alley"].ToString()
               ,
                        Shipment_Road = reader["Shipment_Road"] is DBNull ? null : reader["Shipment_Road"].ToString()
               ,
                        Shipment_Sub_district = reader["Shipment_Sub_district"] is DBNull ? null : reader["Shipment_Sub_district"].ToString()
               ,
                        Shipment_District = reader["Shipment_District"] is DBNull ? null : reader["Shipment_District"].ToString()
               ,
                        Shipment_Province = reader["Shipment_Province"] is DBNull ? null : reader["Shipment_Province"].ToString()
               ,
                        Shipment_Postal_ID = reader["Shipment_Postal_ID"] is DBNull ? null : reader["Shipment_Postal_ID"].ToString()
               ,
                        Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString()
               ,
                        SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString()
               ,
                        SP_Name = reader["SP_Name"] is DBNull ? null : reader["SP_Name"].ToString()
               ,
                        Payment_Type = reader["Payment_Type"] is DBNull ? null : reader["Payment_Type"].ToString()
               ,
                        Billing_Type = reader["Billing_Type"] is DBNull ? null : reader["Billing_Type"].ToString()
               ,
                        Billing_Day_of_Week = reader["Billing_Day_of_Week"] is DBNull ? null : reader["Billing_Day_of_Week"].ToString()
               ,
                        Due_Billing_Day_of_Week = reader["Due_Billing_Day_of_Week"] is DBNull ? null : reader["Due_Billing_Day_of_Week"].ToString()
               ,
                        Billing_Day_of_Month = reader["Billing_Day_of_Month"] is DBNull ? null : reader["Billing_Day_of_Month"].ToString()
               ,
                        Due_Billing_Day_of_Month = reader["Due_Billing_Day_of_Month"] is DBNull ? null : reader["Due_Billing_Day_of_Month"].ToString()
               ,
                        Billing_Day_of_Other = reader["Billing_Day_of_Other"] is DBNull ? null : reader["Billing_Day_of_Other"].ToString()
               ,
                        Due_Billing_Day_of_Other = reader["Due_Billing_Day_of_Other"] is DBNull ? null : reader["Due_Billing_Day_of_Other"].ToString()
               ,
                        Credit_Term = reader["Credit_Term"] is DBNull ? null : reader["Credit_Term"].ToString()
               ,
                        Credit_Limit = reader["Credit_Limit"] is DBNull ? null : (Int32?)reader["Credit_Limit"]
               ,
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString()
               ,
                        Replace_Sales = reader["Replace_Sales"] is DBNull ? null : reader["Replace_Sales"].ToString()
               ,
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString()
                        ,
                        Active_status = reader["Active_Status"] is DBNull ? null : reader["Active_Status"].ToString()

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

    public static dbo_CustomerClass Select_Record(string Customer_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_CustomerClass clsdbo_Customer = new dbo_CustomerClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CustomerSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (!string.IsNullOrEmpty(Customer_ID))
        {
            selectCommand.Parameters.AddWithValue("@Customer_ID", Customer_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Customer_ID", DBNull.Value);
        }


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Customer.Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString();
                clsdbo_Customer.Customer_Type = reader["Customer_Type"] is DBNull ? null : reader["Customer_Type"].ToString();
                clsdbo_Customer.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
                clsdbo_Customer.Residence_Type_ID = reader["Residence_Type_ID"] is DBNull ? null : reader["Residence_Type_ID"].ToString();
                clsdbo_Customer.First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString();
                clsdbo_Customer.Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString();
                clsdbo_Customer.Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString();
                clsdbo_Customer.Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString();
                clsdbo_Customer.Contact_Name = reader["Contact_Name"] is DBNull ? null : reader["Contact_Name"].ToString();
                clsdbo_Customer.Birthday = reader["Birthday"] is DBNull ? null : ((DateTime?)reader["Birthday"]);
                clsdbo_Customer.Home_House_No = reader["Home_House_No"] is DBNull ? null : reader["Home_House_No"].ToString();
                clsdbo_Customer.Home_Tower = reader["Home_Tower"] is DBNull ? null : reader["Home_Tower"].ToString();
                clsdbo_Customer.Home_Village = reader["Home_Village"] is DBNull ? null : reader["Home_Village"].ToString();
                clsdbo_Customer.Home_Village_No = reader["Home_Village_No"] is DBNull ? null : reader["Home_Village_No"].ToString();
                clsdbo_Customer.Home_Alley = reader["Home_Alley"] is DBNull ? null : reader["Home_Alley"].ToString();
                clsdbo_Customer.Home_Road = reader["Home_Road"] is DBNull ? null : reader["Home_Road"].ToString();
                clsdbo_Customer.Home_Sub_district = reader["Home_Sub_district"] is DBNull ? null : reader["Home_Sub_district"].ToString();
                clsdbo_Customer.Home_District = reader["Home_District"] is DBNull ? null : reader["Home_District"].ToString();
                clsdbo_Customer.Home_Province = reader["Home_Province"] is DBNull ? null : reader["Home_Province"].ToString();
                clsdbo_Customer.Home_Postal_ID = reader["Home_Postal_ID"] is DBNull ? null : reader["Home_Postal_ID"].ToString();
                clsdbo_Customer.Shipment_House_No = reader["Shipment_House_No"] is DBNull ? null : reader["Shipment_House_No"].ToString();
                clsdbo_Customer.Shipment_Tower = reader["Shipment_Tower"] is DBNull ? null : reader["Shipment_Tower"].ToString();
                clsdbo_Customer.Shipment_Village = reader["Shipment_Village"] is DBNull ? null : reader["Shipment_Village"].ToString();
                clsdbo_Customer.Shipment_Village_No = reader["Shipment_Village_No"] is DBNull ? null : reader["Shipment_Village_No"].ToString();
                clsdbo_Customer.Shipment_Alley = reader["Shipment_Alley"] is DBNull ? null : reader["Shipment_Alley"].ToString();
                clsdbo_Customer.Shipment_Road = reader["Shipment_Road"] is DBNull ? null : reader["Shipment_Road"].ToString();
                clsdbo_Customer.Shipment_Sub_district = reader["Shipment_Sub_district"] is DBNull ? null : reader["Shipment_Sub_district"].ToString();
                clsdbo_Customer.Shipment_District = reader["Shipment_District"] is DBNull ? null : reader["Shipment_District"].ToString();
                clsdbo_Customer.Shipment_Province = reader["Shipment_Province"] is DBNull ? null : reader["Shipment_Province"].ToString();
                clsdbo_Customer.Shipment_Postal_ID = reader["Shipment_Postal_ID"] is DBNull ? null : reader["Shipment_Postal_ID"].ToString();
                clsdbo_Customer.Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString();
                clsdbo_Customer.SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString();
                clsdbo_Customer.SP_Name = reader["SP_Name"] is DBNull ? null : reader["SP_Name"].ToString();
                clsdbo_Customer.Payment_Type = reader["Payment_Type"] is DBNull ? null : reader["Payment_Type"].ToString();
                clsdbo_Customer.Billing_Type = reader["Billing_Type"] is DBNull ? null : reader["Billing_Type"].ToString();
                clsdbo_Customer.Billing_Day_of_Week = reader["Billing_Day_of_Week"] is DBNull ? null : reader["Billing_Day_of_Week"].ToString();
                clsdbo_Customer.Due_Billing_Day_of_Week = reader["Due_Billing_Day_of_Week"] is DBNull ? null : reader["Due_Billing_Day_of_Week"].ToString();
                clsdbo_Customer.Billing_Day_of_Month = reader["Billing_Day_of_Month"] is DBNull ? null : reader["Billing_Day_of_Month"].ToString();
                clsdbo_Customer.Due_Billing_Day_of_Month = reader["Due_Billing_Day_of_Month"] is DBNull ? null : reader["Due_Billing_Day_of_Month"].ToString();
                clsdbo_Customer.Billing_Day_of_Other = reader["Billing_Day_of_Other"] is DBNull ? null : reader["Billing_Day_of_Other"].ToString();
                clsdbo_Customer.Due_Billing_Day_of_Other = reader["Due_Billing_Day_of_Other"] is DBNull ? null : reader["Due_Billing_Day_of_Other"].ToString();
                clsdbo_Customer.Credit_Term = reader["Credit_Term"] is DBNull ? null : reader["Credit_Term"].ToString();
                clsdbo_Customer.Credit_Limit = reader["Credit_Limit"] is DBNull ? null : (Int32?)reader["Credit_Limit"];
                clsdbo_Customer.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_Customer.Replace_Sales = reader["Replace_Sales"] is DBNull ? null : reader["Replace_Sales"].ToString();
                clsdbo_Customer.Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString();
                clsdbo_Customer.ReceiveDate_Hour = reader["Receive_From_Hour"] is DBNull ? null : reader["Receive_From_Hour"].ToString();
                clsdbo_Customer.ReceiveDate_Minute = reader["Receive_From_Minute"] is DBNull ? null : reader["Receive_From_Minute"].ToString();
                clsdbo_Customer.ReceiveToDate_Hour = reader["Receive_To_Hour"] is DBNull ? null : reader["Receive_To_Hour"].ToString();
                clsdbo_Customer.ReceiveToDate_Minute = reader["Receive_To_Minute"] is DBNull ? null : reader["Receive_To_Minute"].ToString();
                clsdbo_Customer.Active_status = reader["Active_status"] is DBNull ? null : reader["Active_status"].ToString();
                clsdbo_Customer.Member_Date = reader["Member_Date"] is DBNull ? null : ((DateTime?)reader["Member_Date"]);
                clsdbo_Customer.Shop_Name = reader["Shop_Name"] is DBNull ? null : reader["Shop_Name"].ToString();
            }
            else
            {
                clsdbo_Customer = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Customer;
        }

        finally
        {
            connection.Close();
        }
        return clsdbo_Customer;
    }

    public static bool Add(dbo_CustomerClass clsdbo_Customer, String Created_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "CustomerInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Customer.Customer_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Customer_ID", clsdbo_Customer.Customer_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Customer_ID", DBNull.Value);
        }


        switch (clsdbo_Customer.Customer_Type)
        {
            case "ทั่วไป":
                insertCommand.Parameters.AddWithValue("@Customer_Type", true);
                break;
            case "สมาชิก":
                insertCommand.Parameters.AddWithValue("@Customer_Type", false);
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Customer_Type", DBNull.Value);
                break;
        }

        switch (clsdbo_Customer.Status)
        {
            case "ยังติดต่ออยู่":
                insertCommand.Parameters.AddWithValue("@Status", 1);
                break;
            case "ขาดการติดต่อ":
                insertCommand.Parameters.AddWithValue("@Status", 2);
                break;
            case "ระงับการส่งชั่วคราว":
                insertCommand.Parameters.AddWithValue("@Status", 3);
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                break;
        }

        if (clsdbo_Customer.Residence_Type_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Residence_Type_ID", clsdbo_Customer.Residence_Type_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Residence_Type_ID", DBNull.Value);
        }
        if (clsdbo_Customer.First_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@First_Name", clsdbo_Customer.First_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@First_Name", DBNull.Value);
        }
        if (clsdbo_Customer.Last_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Last_Name", clsdbo_Customer.Last_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Name", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Phone_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Phone_No", clsdbo_Customer.Home_Phone_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Phone_No", DBNull.Value);
        }
        if (clsdbo_Customer.Mobile != null)
        {
            insertCommand.Parameters.AddWithValue("@Mobile", clsdbo_Customer.Mobile);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Mobile", DBNull.Value);
        }
        if (clsdbo_Customer.Contact_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Contact_Name", clsdbo_Customer.Contact_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Contact_Name", DBNull.Value);
        }
        if (clsdbo_Customer.Birthday != null)
        {
            insertCommand.Parameters.AddWithValue("@Birthday", clsdbo_Customer.Birthday);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Birthday", DBNull.Value);
        }
        if (clsdbo_Customer.Home_House_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_House_No", clsdbo_Customer.Home_House_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_House_No", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Tower != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Tower", clsdbo_Customer.Home_Tower);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Tower", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Village != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Village", clsdbo_Customer.Home_Village);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Village", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Village_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Village_No", clsdbo_Customer.Home_Village_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Village_No", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Alley != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Alley", clsdbo_Customer.Home_Alley);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Alley", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Road != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Road", clsdbo_Customer.Home_Road);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Road", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Sub_district != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Sub_district", clsdbo_Customer.Home_Sub_district);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Sub_district", DBNull.Value);
        }
        if (clsdbo_Customer.Home_District != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_District", clsdbo_Customer.Home_District);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_District", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Province != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Province", clsdbo_Customer.Home_Province);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Province", DBNull.Value);
        }
        if (clsdbo_Customer.Home_Postal_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Postal_ID", clsdbo_Customer.Home_Postal_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Postal_ID", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_House_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_House_No", clsdbo_Customer.Shipment_House_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_House_No", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_Tower != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Tower", clsdbo_Customer.Shipment_Tower);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Tower", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_Village != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Village", clsdbo_Customer.Shipment_Village);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Village", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_Village_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Village_No", clsdbo_Customer.Shipment_Village_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Village_No", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_Alley != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Alley", clsdbo_Customer.Shipment_Alley);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Alley", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_Road != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Road", clsdbo_Customer.Shipment_Road);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Road", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_Sub_district != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Sub_district", clsdbo_Customer.Shipment_Sub_district);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Sub_district", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_District != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_District", clsdbo_Customer.Shipment_District);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_District", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_Province != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Province", clsdbo_Customer.Shipment_Province);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Province", DBNull.Value);
        }
        if (clsdbo_Customer.Shipment_Postal_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Postal_ID", clsdbo_Customer.Shipment_Postal_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shipment_Postal_ID", DBNull.Value);
        }
        if (clsdbo_Customer.Remarks != null)
        {
            insertCommand.Parameters.AddWithValue("@Remarks", clsdbo_Customer.Remarks);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Remarks", DBNull.Value);
        }
        if (clsdbo_Customer.SP_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@SP_ID", clsdbo_Customer.SP_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (clsdbo_Customer.SP_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@SP_Name", clsdbo_Customer.SP_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SP_Name", DBNull.Value);
        }


        switch (clsdbo_Customer.Payment_Type)
        {
            case "เงินสด":
                insertCommand.Parameters.AddWithValue("@Payment_Type", 0);
                break;
            case "เครดิต":
                insertCommand.Parameters.AddWithValue("@Payment_Type", 1);
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Payment_Type", DBNull.Value);
                break;
        }

        switch (clsdbo_Customer.Billing_Type)
        {
            case "บิลชนบิล":
                insertCommand.Parameters.AddWithValue("@Billing_Type", '1');
                break;
            case "รายสัปดาห์":
                insertCommand.Parameters.AddWithValue("@Billing_Type", '2');
                break;
            case "รายเดือน":
                insertCommand.Parameters.AddWithValue("@Billing_Type", '3');
                break;
            case "วางบิลอื่นๆ":
                insertCommand.Parameters.AddWithValue("@Billing_Type", '4');
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Billing_Type", DBNull.Value);
                break;
        }


        switch (clsdbo_Customer.Billing_Day_of_Week)
        {
            case "อาทิตย์":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Week", '1');
                break;
            case "จันทร์":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Week", '2');
                break;
            case "อังคาร":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Week", '3');
                break;
            case "พุธ":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Week", '4');
                break;
            case "พฤหัสบดี":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Week", '5');
                break;
            case "ศุกร์":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Week", '6');
                break;
            case "เสาร์":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Week", '7');
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Week", DBNull.Value);
                break;
        }




        switch (clsdbo_Customer.Due_Billing_Day_of_Week)
        {
            case "อาทิตย์":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Week", '1');
                break;
            case "จันทร์":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Week", '2');
                break;
            case "อังคาร":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Week", '3');
                break;
            case "พุธ":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Week", '4');
                break;
            case "พฤหัสบดี":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Week", '5');
                break;
            case "ศุกร์":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Week", '6');
                break;
            case "เสาร์":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Week", '7');
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Week", DBNull.Value);
                break;
        }





        //if (clsdbo_Customer.Billing_Day_of_Month != null)
        //{
        //    insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", clsdbo_Customer.Billing_Day_of_Month);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", DBNull.Value);
        //}


        switch (clsdbo_Customer.Billing_Day_of_Month)
        {
            case "ไม่มีการวางบิล":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "1");
                break;
            case "1":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "2");
                break;
            case "2":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "3");
                break;
            case "3":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "4");
                break;
            case "4":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "5");
                break;
            case "5":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "6");
                break;
            case "6":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "7");
                break;
            case "7":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "8");
                break;
            case "8":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "9");
                break;
            case "9":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "10");
                break;
            case "10":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "11");
                break;
            case "11":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "12");
                break;
            case "12":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "13");
                break;
            case "13":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "14");
                break;
            case "14":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "15");
                break;
            case "15":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "16");
                break;
            case "16":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "17");
                break;
            case "17":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "18");
                break;
            case "18":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "19");
                break;
            case "19":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "20");
                break;
            case "20":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "21");
                break;
            case "21":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "22");
                break;
            case "22":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "23");
                break;
            case "23":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "24");
                break;
            case "24":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "25");
                break;
            case "25":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "26");
                break;
            case "26":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "27");
                break;
            case "27":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "28");
                break;
            case "28":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "29");
                break;
            case "29":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "30");
                break;
            case "30":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "31");
                break;
            case "31":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "32");
                break;
            case "วันสุดท้ายของเดือน":
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", "33");
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Billing_Day_of_Month", DBNull.Value);
                break;
        }
        switch (clsdbo_Customer.Due_Billing_Day_of_Month)
        {
            case "1":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "1");
                break;
            case "2":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "2");
                break;
            case "3":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "3");
                break;
            case "4":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "4");
                break;
            case "5":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "5");
                break;
            case "6":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "6");
                break;
            case "7":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "7");
                break;
            case "8":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "8");
                break;
            case "9":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "9");
                break;
            case "10":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "10");
                break;
            case "11":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "11");
                break;
            case "12":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "12");
                break;
            case "13":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "13");
                break;
            case "14":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "14");
                break;
            case "15":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "15");
                break;
            case "16":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "16");
                break;
            case "17":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "17");
                break;
            case "18":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "18");
                break;
            case "19":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "19");
                break;
            case "20":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "20");
                break;
            case "21":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "21");
                break;
            case "22":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "22");
                break;
            case "23":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "23");
                break;
            case "24":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "24");
                break;
            case "25":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "25");
                break;
            case "26":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "26");
                break;
            case "27":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "27");
                break;
            case "28":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "28");
                break;
            case "29":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "29");
                break;
            case "30":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "30");
                break;
            case "31":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "31");
                break;
            case "วันสุดท้ายของเดือน":
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", "32");
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", DBNull.Value);
                break;
        }


        //if (clsdbo_Customer.Due_Billing_Day_of_Month != null)
        //{
        //    insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", clsdbo_Customer.Due_Billing_Day_of_Month);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Month", DBNull.Value);
        //}




        if (clsdbo_Customer.Billing_Day_of_Other != null)
        {
            insertCommand.Parameters.AddWithValue("@Billing_Day_of_Other", clsdbo_Customer.Billing_Day_of_Other);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Billing_Day_of_Other", DBNull.Value);
        }

        if (clsdbo_Customer.Due_Billing_Day_of_Other != null)
        {
            insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Other", clsdbo_Customer.Due_Billing_Day_of_Other);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Due_Billing_Day_of_Other", DBNull.Value);
        }

        if (clsdbo_Customer.Credit_Term != null)
        {
            insertCommand.Parameters.AddWithValue("@Credit_Term", clsdbo_Customer.Credit_Term);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_Term", DBNull.Value);
        }

        if (clsdbo_Customer.Credit_Limit.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Credit_Limit", clsdbo_Customer.Credit_Limit);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_Limit", DBNull.Value);
        }
        if (clsdbo_Customer.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_Customer.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }


        //if (clsdbo_Customer.Replace_Sales != null)
        //{
        //    insertCommand.Parameters.AddWithValue("@Replace_Sales", clsdbo_Customer.Replace_Sales);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@Replace_Sales", DBNull.Value);
        //}
        //if (clsdbo_Customer.Price_Group_ID != null)
        //{
        //    insertCommand.Parameters.AddWithValue("@Price_Group_ID", clsdbo_Customer.Price_Group_ID);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        //}


        if (clsdbo_Customer.ReceiveDate_Hour != null)
            insertCommand.Parameters.AddWithValue("@ReceiveProductDate_Hour", clsdbo_Customer.ReceiveDate_Hour);
        else
            insertCommand.Parameters.AddWithValue("@ReceiveProductDate_Hour", DBNull.Value);

        if (clsdbo_Customer.ReceiveDate_Minute != null)
            insertCommand.Parameters.AddWithValue("@ReceiveProductDate_Minute", clsdbo_Customer.ReceiveDate_Minute);
        else
            insertCommand.Parameters.AddWithValue("@ReceiveProductDate_Minute", DBNull.Value);

        if (clsdbo_Customer.ReceiveToDate_Hour != null)
            insertCommand.Parameters.AddWithValue("@ReceiveProductToDate_Hour", clsdbo_Customer.ReceiveToDate_Hour);
        else
            insertCommand.Parameters.AddWithValue("@ReceiveProductToDate_Hour", DBNull.Value);

        if (clsdbo_Customer.ReceiveToDate_Minute != null)
            insertCommand.Parameters.AddWithValue("@ReceiveProductTODate_Minute", clsdbo_Customer.ReceiveToDate_Minute);
        else
            insertCommand.Parameters.AddWithValue("@ReceiveProductTODate_Minute", DBNull.Value);
        if (clsdbo_Customer.Active_status != null)
        {
            insertCommand.Parameters.AddWithValue("@Active_Status", clsdbo_Customer.Active_status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Active_Status", DBNull.Value);
        }

        if (clsdbo_Customer.Member_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@Member_Date", clsdbo_Customer.Member_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Member_Date", DBNull.Value);
        }
        if (clsdbo_Customer.Shop_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Shop_Name", clsdbo_Customer.Shop_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Shop_Name", DBNull.Value);
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

    public static bool Update(dbo_CustomerClass newdbo_CustomerClass, String Last_Modified_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "CustomerUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;

        // @NewCustomer_ID varchar(10)
        if (newdbo_CustomerClass.Customer_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCustomer_ID", newdbo_CustomerClass.Customer_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCustomer_ID", DBNull.Value);
        }
        // ,@NewCustomer_Type bit
        switch (newdbo_CustomerClass.Customer_Type)
        {
            case "ทั่วไป":
                updateCommand.Parameters.AddWithValue("@NewCustomer_Type", true);
                break;
            case "สมาชิก":
                updateCommand.Parameters.AddWithValue("@NewCustomer_Type", false);
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewCustomer_Type", DBNull.Value);
                break;
        }
        // ,@NewResidence_Type_ID varchar(4)
        if (newdbo_CustomerClass.Residence_Type_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewResidence_Type_ID", newdbo_CustomerClass.Residence_Type_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewResidence_Type_ID", DBNull.Value);
        }
        // ,@NewFirst_Name nvarchar(100)
        if (newdbo_CustomerClass.First_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewFirst_Name", newdbo_CustomerClass.First_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewFirst_Name", DBNull.Value);
        }




        // ,@NewLast_Name nvarchar(100)
        if (newdbo_CustomerClass.Last_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Name", newdbo_CustomerClass.Last_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Name", DBNull.Value);
        }
        //,@NewHome_Phone_No varchar(11)
        if (newdbo_CustomerClass.Home_Phone_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Phone_No", newdbo_CustomerClass.Home_Phone_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Phone_No", DBNull.Value);
        }


        //  ,@NewMobile varchar(12)
        if (newdbo_CustomerClass.Mobile != null)
        {
            updateCommand.Parameters.AddWithValue("@NewMobile", newdbo_CustomerClass.Mobile);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewMobile", DBNull.Value);
        }

        // ,@NewContact_Name nvarchar(100)
        if (newdbo_CustomerClass.Contact_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewContact_Name", newdbo_CustomerClass.Contact_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewContact_Name", DBNull.Value);
        }

        //,@NewBirthday date
        if (newdbo_CustomerClass.Birthday != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBirthday", newdbo_CustomerClass.Birthday);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBirthday", DBNull.Value);
        }

        switch (newdbo_CustomerClass.Status)
        {
            case "ยังติดต่ออยู่":
                updateCommand.Parameters.AddWithValue("@NewStatus", 1);
                break;
            case "ขาดการติดต่อ":
                updateCommand.Parameters.AddWithValue("@NewStatus", 2);
                break;
            case "ระงับการส่งชั่วคราว":
                updateCommand.Parameters.AddWithValue("@NewStatus", 3);
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewStatus", DBNull.Value);
                break;
        }
        switch (newdbo_CustomerClass.Active_status)
        {
            case "Active":
                updateCommand.Parameters.AddWithValue("@Active_Status", 1);
                break;
            case "In active":
                updateCommand.Parameters.AddWithValue("@Active_Status", 0);
                break;
            default:
                updateCommand.Parameters.AddWithValue("@Active_Status", DBNull.Value);
                break;
        }
        if (newdbo_CustomerClass.Home_House_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_House_No", newdbo_CustomerClass.Home_House_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_House_No", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_Tower != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Tower", newdbo_CustomerClass.Home_Tower);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Tower", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_Village != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Village", newdbo_CustomerClass.Home_Village);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Village", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_Village_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Village_No", newdbo_CustomerClass.Home_Village_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Village_No", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_Alley != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Alley", newdbo_CustomerClass.Home_Alley);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Alley", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_Road != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Road", newdbo_CustomerClass.Home_Road);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Road", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_Sub_district != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Sub_district", newdbo_CustomerClass.Home_Sub_district);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Sub_district", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_District != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_District", newdbo_CustomerClass.Home_District);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_District", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_Province != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Province", newdbo_CustomerClass.Home_Province);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Province", DBNull.Value);
        }
        if (newdbo_CustomerClass.Home_Postal_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Postal_ID", newdbo_CustomerClass.Home_Postal_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Postal_ID", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_House_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_House_No", newdbo_CustomerClass.Shipment_House_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_House_No", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_Tower != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Tower", newdbo_CustomerClass.Shipment_Tower);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Tower", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_Village != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Village", newdbo_CustomerClass.Shipment_Village);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Village", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_Village_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Village_No", newdbo_CustomerClass.Shipment_Village_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Village_No", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_Alley != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Alley", newdbo_CustomerClass.Shipment_Alley);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Alley", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_Road != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Road", newdbo_CustomerClass.Shipment_Road);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Road", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_Sub_district != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Sub_district", newdbo_CustomerClass.Shipment_Sub_district);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Sub_district", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_District != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_District", newdbo_CustomerClass.Shipment_District);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_District", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_Province != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Province", newdbo_CustomerClass.Shipment_Province);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Province", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shipment_Postal_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Postal_ID", newdbo_CustomerClass.Shipment_Postal_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewShipment_Postal_ID", DBNull.Value);
        }
        if (newdbo_CustomerClass.Remarks != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRemarks", newdbo_CustomerClass.Remarks);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRemarks", DBNull.Value);
        }
        if (newdbo_CustomerClass.SP_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSP_ID", newdbo_CustomerClass.SP_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSP_ID", DBNull.Value);
        }
        if (newdbo_CustomerClass.SP_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSP_Name", newdbo_CustomerClass.SP_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSP_Name", DBNull.Value);
        }




        ////////////////

        switch (newdbo_CustomerClass.Payment_Type)
        {
            case "เงินสด":
                updateCommand.Parameters.AddWithValue("@NewPayment_Type", 0);
                break;
            case "เครดิต":
                updateCommand.Parameters.AddWithValue("@NewPayment_Type", 1);
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewPayment_Type", DBNull.Value);
                break;
        }

        switch (newdbo_CustomerClass.Billing_Type)
        {
            case "บิลชนบิล":
                updateCommand.Parameters.AddWithValue("@NewBilling_Type", '1');
                break;
            case "รายสัปดาห์":
                updateCommand.Parameters.AddWithValue("@NewBilling_Type", '2');
                break;
            case "รายเดือน":
                updateCommand.Parameters.AddWithValue("@NewBilling_Type", '3');
                break;
            case "วางบิลอื่นๆ":
                updateCommand.Parameters.AddWithValue("@NewBilling_Type", '4');
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewBilling_Type", DBNull.Value);
                break;
        }


        switch (newdbo_CustomerClass.Billing_Day_of_Week)
        {
            case "อาทิตย์":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", '1');
                break;
            case "จันทร์":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", '2');
                break;
            case "อังคาร":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", '3');
                break;
            case "พุธ":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", '4');
                break;
            case "พฤหัสบดี":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", '5');
                break;
            case "ศุกร์":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", '6');
                break;
            case "เสาร์":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", '7');
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", DBNull.Value);
                break;
        }




        switch (newdbo_CustomerClass.Due_Billing_Day_of_Week)
        {
            case "อาทิตย์":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", '1');
                break;
            case "จันทร์":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", '2');
                break;
            case "อังคาร":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", '3');
                break;
            case "พุธ":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", '4');
                break;
            case "พฤหัสบดี":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", '5');
                break;
            case "ศุกร์":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", '6');
                break;
            case "เสาร์":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", '7');
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", DBNull.Value);
                break;
        }



        ////////////////////






        //if (newdbo_CustomerClass.Payment_Type != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewPayment_Type", newdbo_CustomerClass.Payment_Type);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewPayment_Type", DBNull.Value);
        //}
        //if (newdbo_CustomerClass.Billing_Type != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewBilling_Type", newdbo_CustomerClass.Billing_Type);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewBilling_Type", DBNull.Value);
        //}
        //if (newdbo_CustomerClass.Billing_Day_of_Week != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", newdbo_CustomerClass.Billing_Day_of_Week);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Week", DBNull.Value);
        //}
        //if (newdbo_CustomerClass.Due_Billing_Day_of_Week != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", newdbo_CustomerClass.Due_Billing_Day_of_Week);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Week", DBNull.Value);
        //}




        switch (newdbo_CustomerClass.Billing_Day_of_Month)
        {
            case "ไม่มีการวางบิล":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", '1');
                break;
            case "1":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "2");
                break;
            case "2":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "3");
                break;
            case "3":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "4");
                break;
            case "4":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "5");
                break;
            case "5":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "6");
                break;
            case "6":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "7");
                break;
            case "7":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "8");
                break;
            case "8":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "9");
                break;
            case "9":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "10");
                break;
            case "10":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "11");
                break;
            case "11":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "12");
                break;
            case "12":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "13");
                break;
            case "13":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "14");
                break;
            case "14":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "15");
                break;
            case "15":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "16");
                break;
            case "16":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "17");
                break;
            case "17":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "18");
                break;
            case "18":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "19");
                break;
            case "19":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "20");
                break;
            case "20":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "21");
                break;
            case "21":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "22");
                break;
            case "22":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "23");
                break;
            case "23":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "24");
                break;
            case "24":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "25");
                break;
            case "25":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "26");
                break;
            case "26":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "27");
                break;
            case "27":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "28");
                break;
            case "28":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "29");
                break;
            case "29":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "30");
                break;
            case "30":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "31");
                break;
            case "31":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "32");
                break;
            case "วันสุดท้ายของเดือน":
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", "33");
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", DBNull.Value);
                break;
        }
        switch (newdbo_CustomerClass.Due_Billing_Day_of_Month)
        {
            case "1":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "1");
                break;
            case "2":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "2");
                break;
            case "3":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "3");
                break;
            case "4":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "4");
                break;
            case "5":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "5");
                break;
            case "6":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "6");
                break;
            case "7":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "7");
                break;
            case "8":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "8");
                break;
            case "9":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "9");
                break;
            case "10":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "10");
                break;
            case "11":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "11");
                break;
            case "12":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "12");
                break;
            case "13":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "13");
                break;
            case "14":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "14");
                break;
            case "15":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "15");
                break;
            case "16":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "16");
                break;
            case "17":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "17");
                break;
            case "18":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "18");
                break;
            case "19":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "19");
                break;
            case "20":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "20");
                break;
            case "21":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "21");
                break;
            case "22":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "22");
                break;
            case "23":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "23");
                break;
            case "24":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "24");
                break;
            case "25":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "25");
                break;
            case "26":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "26");
                break;
            case "27":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "27");
                break;
            case "28":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "28");
                break;
            case "29":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "29");
                break;
            case "30":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "30");
                break;
            case "31":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "31");
                break;
            case "วันสุดท้ายของเดือน":
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", "32");
                break;
            default:
                updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", DBNull.Value);
                break;
        }




        //if (newdbo_CustomerClass.Billing_Day_of_Month != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", newdbo_CustomerClass.Billing_Day_of_Month);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Month", DBNull.Value);
        //}
        //if (newdbo_CustomerClass.Due_Billing_Day_of_Month != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", newdbo_CustomerClass.Due_Billing_Day_of_Month);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Month", DBNull.Value);
        //}
        if (newdbo_CustomerClass.Billing_Day_of_Other != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Other", newdbo_CustomerClass.Billing_Day_of_Other);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_Day_of_Other", DBNull.Value);
        }
        if (newdbo_CustomerClass.Due_Billing_Day_of_Other != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Other", newdbo_CustomerClass.Due_Billing_Day_of_Other);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDue_Billing_Day_of_Other", DBNull.Value);
        }

        if (newdbo_CustomerClass.Credit_Term != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Term", newdbo_CustomerClass.Credit_Term);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Term", DBNull.Value);
        }

        if (newdbo_CustomerClass.Credit_Limit.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Limit", newdbo_CustomerClass.Credit_Limit);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Limit", DBNull.Value);
        }
        if (newdbo_CustomerClass.CV_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", newdbo_CustomerClass.CV_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", DBNull.Value);
        }


        //if (newdbo_CustomerClass.Replace_Sales != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewReplace_Sales", newdbo_CustomerClass.Replace_Sales);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewReplace_Sales", DBNull.Value);
        //}



        //if (newdbo_CustomerClass.Price_Group_ID != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", newdbo_CustomerClass.Price_Group_ID);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", DBNull.Value);
        //}

        if (Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
        }

        if (newdbo_CustomerClass.ReceiveDate_Hour != null)
            updateCommand.Parameters.AddWithValue("@ReceiveProductDate_Hour", newdbo_CustomerClass.ReceiveDate_Hour);
        else
            updateCommand.Parameters.AddWithValue("@ReceiveProductDate_Hour", DBNull.Value);

        if (newdbo_CustomerClass.ReceiveDate_Minute != null)
            updateCommand.Parameters.AddWithValue("@ReceiveProductDate_Minute", newdbo_CustomerClass.ReceiveDate_Minute);
        else
            updateCommand.Parameters.AddWithValue("@ReceiveProductDate_Minute", DBNull.Value);

        if (newdbo_CustomerClass.ReceiveToDate_Hour != null)
            updateCommand.Parameters.AddWithValue("@ReceiveProductToDate_Hour", newdbo_CustomerClass.ReceiveToDate_Hour);
        else
            updateCommand.Parameters.AddWithValue("@ReceiveProductToDate_Hour", DBNull.Value);

        if (newdbo_CustomerClass.ReceiveToDate_Minute != null)
            updateCommand.Parameters.AddWithValue("@ReceiveProductTODate_Minute", newdbo_CustomerClass.ReceiveToDate_Minute);
        else
            updateCommand.Parameters.AddWithValue("@ReceiveProductTODate_Minute", DBNull.Value);

        /*if (newdbo_CustomerClass.Active_status != null)
            updateCommand.Parameters.AddWithValue("@Active_Status", newdbo_CustomerClass.Active_status);
        else
            updateCommand.Parameters.AddWithValue("@Active_Status", DBNull.Value);*/

        if (newdbo_CustomerClass.Member_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@Member_Date", newdbo_CustomerClass.Member_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Member_Date", DBNull.Value);
        }
        if (newdbo_CustomerClass.Shop_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@Shop_Name", newdbo_CustomerClass.Shop_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Shop_Name", DBNull.Value);
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

    public static bool Delete(string customerID, string userID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[CustomerDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (customerID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCustomer_ID", customerID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCustomer_ID", DBNull.Value);
        }

        deleteCommand.Parameters.AddWithValue("@Last_Modified_By", userID);
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

