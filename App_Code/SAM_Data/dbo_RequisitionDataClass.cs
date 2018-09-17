using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;
public class dbo_RequisitionDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    [Obsolete]
    public static List<dbo_RequisitionClass> GetRequisition()
    {
        List<dbo_RequisitionClass> item = new List<dbo_RequisitionClass>();
        item.Add(new dbo_RequisitionClass() { SP_Name = "ชัยวุฒิ บัวทอง", Requisition_Date = DateTime.Now.AddHours(-5), Requisition_No = "212689GT17051401", Time_No = "1" });
        item.Add(new dbo_RequisitionClass() { SP_Name = "ชัยวุฒิ บัวทอง", Requisition_Date = DateTime.Now, Requisition_No = "212689GT17051401", Time_No = "2" });
        //item.Add(new dbo_RequisitionClass() { SP_Name = "ชัยวุฒิ", Requisition_Date = DateTime.Now, Requisition_No = "3", Time_No = "1" });
        //item.Add(new dbo_RequisitionClass() { SP_Name = "ชัยวุฒิ", Requisition_Date = DateTime.Now, Requisition_No = "4", Time_No = "1" });
        return item;
    }

    [Obsolete]
    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RequisitionSelectAll]";
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



    public static List<dbo_RequisitionClass> Search(DateTime? StartDate, DateTime? EndDate, string User_ID, string CV_Code)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionSearch2]";
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

        if (StartDate.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@StartDate", StartDate.Value.Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@StartDate", DateTime.Now.AddYears(-10));
        }
        if (EndDate.HasValue)
        {
            //selectCommand.Parameters.AddWithValue("@EndDate", EndDate.Value.AddDays(1).AddTicks(-1));
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate.Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DateTime.Now.AddYears(10));
        }
        if (string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }



        DataTable dt = new DataTable();

        List<dbo_RequisitionClass> item = new List<dbo_RequisitionClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);


                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_RequisitionClass()
                    {
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        Time_No = reader["Time_No"] is DBNull ? null : reader["Time_No"].ToString(),
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"],
                        Transaction_Date = reader["Transaction_Date"] is DBNull ? null : (DateTime?)reader["Transaction_Date"],
                        Grand_Total_Qty = reader["Grand_Total_Qty"] is DBNull ? null : (Int32?)reader["Grand_Total_Qty"],
                        Grand_Total_Amount = reader["Grand_Total_Amount"] is DBNull ? null : (Decimal?)reader["Grand_Total_Amount"],
                        Total_Commission = reader["Total_Commission"] is DBNull ? null : (Decimal?)reader["Total_Commission"],
                        Tota_Point = reader["Tota_Point"] is DBNull ? null : (Int16?)reader["Tota_Point"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Replace_Sales = reader["Replace_Sales"] is DBNull ? null : reader["Replace_Sales"].ToString()

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


    public static List<dbo_RequisitionClass> Search(string Requisition_No, string Time_No, string User_ID, DateTime? Requisition_Date)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Requisition_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Time_No))
        {
            selectCommand.Parameters.AddWithValue("@Time_No", Time_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Time_No", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (Requisition_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Date", Requisition_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Date", DBNull.Value);
        }




        DataTable dt = new DataTable();

        List<dbo_RequisitionClass> item = new List<dbo_RequisitionClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);


                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_RequisitionClass()
                    {

                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        Time_No = reader["Time_No"] is DBNull ? null : reader["Time_No"].ToString(),
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"],
                        Transaction_Date = reader["Transaction_Date"] is DBNull ? null : (DateTime?)reader["Transaction_Date"],
                        Grand_Total_Qty = reader["Grand_Total_Qty"] is DBNull ? null : (Int32?)reader["Grand_Total_Qty"],
                        Grand_Total_Amount = reader["Grand_Total_Amount"] is DBNull ? null : (Decimal?)reader["Grand_Total_Amount"],
                        Total_Commission = reader["Total_Commission"] is DBNull ? null : (Decimal?)reader["Total_Commission"],
                        Tota_Point = reader["Tota_Point"] is DBNull ? null : (Int16?)reader["Tota_Point"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Replace_Sales = reader["Replace_Sales"] is DBNull ? null : reader["Replace_Sales"].ToString()


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


    public static List<dbo_ProductClass> GetRequisitionByProductGroupID(String User_ID, DateTime? Requisition_Date, String Product_group_ID)
    {
        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetRequisitionByProductGroupID";
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
        if (Requisition_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Date", Requisition_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Date", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
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

                String prevPacking_Size = string.Empty;
                String prevUnitItem = string.Empty;
                String prevProduct = string.Empty;
                Int16? prevSize = -1;

                int index = 1;
                foreach (DataRow reader in dt.Rows)
                {

                    if ((prevSize != (Int16?)reader["Size"] || prevPacking_Size != (reader["Packing_Size"]).ToString() || prevUnitItem != (reader["Unit_of_item_ID"]).ToString()) && Product_group_ID != "อื่นๆ")
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

                            SP_Price = reader["SP_Price"] is DBNull ? null : (Decimal?)reader["SP_Price"],
                            CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],


                            Point = reader["Point"] is DBNull ? 0 : (Byte?)reader["Point"],


                            Vat = reader["Vat"] is DBNull ? 0 : (Byte?)reader["Vat"],
                            Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"],
                            Order_No = 0,
                            Packing_Size = 0,
                            Status = null,
                            Total_Qty = reader["Total_Qty"] is DBNull ? 0 : (Int16?)reader["Total_Qty"],
                            Deposit_Qty = reader["Deposit_Qty"] is DBNull ? 0 : (Int16?)reader["Deposit_Qty"],
                            Sub_Total_Qty = reader["Sub_Total_Qty"] is DBNull ? 0 : (Int16?)reader["Sub_Total_Qty"],
                            Suggestion_Qty = reader["Suggestion_Qty"] is DBNull ? 0 : (Int16?)reader["Suggestion_Qty"],
                            Requisition_Qty = reader["Requisition_Qty"] is DBNull ? 0 : (Int16?)reader["Requisition_Qty"],
                            Previous_Balance_Qty = reader["Previous_Balance_Qty"] is DBNull ? 0 : (Int16?)reader["Previous_Balance_Qty"],
                            Requisition_Detail_ID = reader["Requisition_Detail_ID"] is DBNull ? null : reader["Requisition_Detail_ID"].ToString(),
                            Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? DateTime.Now.AddYears(-1) : (DateTime?)reader["Start_Effective_Date"],
                            Stock_END = reader["Stock_End"] is DBNull ? 0 : (Int16?)reader["Stock_End"],
                            
                        });
                    }

                    prevPacking_Size = (reader["Packing_Size"]).ToString();
                    prevUnitItem = (reader["Unit_of_item_ID"]).ToString();
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

    public static List<dbo_ProductClass> GetRequisitionByProductGroupID(String User_ID, String Requisition_No, String Product_group_ID, String Time_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetRequisitionByProductGroupID_View";
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
        if (!string.IsNullOrEmpty(Product_group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", Product_group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_group_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Time_No))
        {
            selectCommand.Parameters.AddWithValue("@Time_No", Time_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Time_No", DBNull.Value);
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

                String prevPacking_Size = string.Empty;
                String prevUnitItem = string.Empty;
                String prevProduct = string.Empty; ;
                Int16? prevSize = -1;

                int index = 1;
                foreach (DataRow reader in dt.Rows)
                {

                    if ((prevSize != (Int16?)reader["Size"] || prevPacking_Size != (reader["Packing_Size"]).ToString() || prevUnitItem != (reader["Unit_of_item_ID"]).ToString()) && Product_group_ID != "อื่นๆ")
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

                            SP_Price = reader["SP_Price"] is DBNull ? null : (Decimal?)reader["SP_Price"],
                            CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],


                            Point = reader["Point"] is DBNull ? 0 : (Byte?)reader["Point"],


                            Vat = reader["Vat"] is DBNull ? 0 : (Byte?)reader["Vat"],
                            Order_No = 0,
                            Packing_Size = 0,
                            Status = null,
                            Total_Qty = reader["Total_Qty"] is DBNull ? 0 : (Int16?)reader["Total_Qty"],
                            Deposit_Qty = reader["Deposit_Qty"] is DBNull ? 0 : (Int16?)reader["Deposit_Qty"],
                            Sub_Total_Qty = reader["Sub_Total_Qty"] is DBNull ? 0 : (Int16?)reader["Sub_Total_Qty"],
                            Suggestion_Qty = reader["Suggestion_Qty"] is DBNull ? 0 : (Int16?)reader["Suggestion_Qty"],
                            Requisition_Qty = reader["Requisition_Qty"] is DBNull ? 0 : (Int16?)reader["Requisition_Qty"],
                            Previous_Balance_Qty = reader["Previous_Balance_Qty"] is DBNull ? 0 : (Int16?)reader["Previous_Balance_Qty"],
                            Requisition_Detail_ID = reader["Requisition_Detail_ID"] is DBNull ? null : reader["Requisition_Detail_ID"].ToString(),
                            Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? DateTime.Now.AddYears(-1) : (DateTime?)reader["Start_Effective_Date"]
                        });
                    }

                    prevPacking_Size = (reader["Packing_Size"]).ToString();
                    prevUnitItem = (reader["Unit_of_item_ID"]).ToString();
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
        catch (Exception ex)
        {

        }
        finally
        {
            connection.Close();
        }
        return item;
    }


    public static List<dbo_ProductClass> SelectRequisitionByProductGroupID(String Product_group_ID, String Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "SelectRequisitionByProductGroupID";
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
        if (!string.IsNullOrEmpty(Requisition_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }


        //if (!string.IsNullOrEmpty(CV_Code))
        //{
        //    selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        //}
        //else
        //{
        //    selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        //}
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
                        Photo = reader["Photo"] is DBNull ? null : (byte[])reader["Photo"],

                        //   Exclude_Vat = reader["Exclude_Vat"] is DBNull ? null : (Boolean?)reader["Exclude_Vat"],



                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Order_No = reader["Order_No"] is DBNull ? null : (Byte?)reader["Order_No"],
                        Quantity_in__carte = reader["Quantity_in__carte"] is DBNull ? null : (Byte?)reader["Quantity_in__carte"],
                        Packing_Size = reader["Packing_Size"] is DBNull ? null : (Byte?)reader["Packing_Size"],

                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),

                        Quantity = reader["Quantity"] is DBNull ? 0 : (Int32?)reader["Quantity"],


                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                        //    Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"]

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

    public static List<dbo_RequisitionClass> SelectByRequisitionNo(String Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_RequisitionClass> clsdbo_Requisition = new List<dbo_RequisitionClass>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionSelectByRequisitionNo]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        DataTable dt = new DataTable();

        try
        {
            connection.Open();
            SqlDataReader reader1
                = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    clsdbo_Requisition.Add(new dbo_RequisitionClass()
                    {
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        Time_No = reader["Time_No"] is DBNull ? null : reader["Time_No"].ToString(),
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"],
                        Transaction_Date = reader["Transaction_Date"] is DBNull ? null : (DateTime?)reader["Transaction_Date"],
                        Grand_Total_Qty = reader["Grand_Total_Qty"] is DBNull ? null : (Int32?)reader["Grand_Total_Qty"],
                        Grand_Total_Amount = reader["Grand_Total_Amount"] is DBNull ? null : (Decimal?)reader["Grand_Total_Amount"],
                        Total_Commission = reader["Total_Commission"] is DBNull ? null : (Decimal?)reader["Total_Commission"],
                        Tota_Point = reader["Tota_Point"] is DBNull ? null : (Int16?)reader["Tota_Point"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Replace_Sales = reader["Replace_Sales"] is DBNull ? null : reader["Replace_Sales"].ToString()
                    });
                }
            }
            
            reader1.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Requisition;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Requisition;
    }


    public static dbo_RequisitionClass Select_Record(String Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_RequisitionClass clsdbo_Requisition = new dbo_RequisitionClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Requisition.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
                clsdbo_Requisition.Time_No = reader["Time_No"] is DBNull ? null : reader["Time_No"].ToString();
                clsdbo_Requisition.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_Requisition.Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"];
                clsdbo_Requisition.Transaction_Date = reader["Transaction_Date"] is DBNull ? null : (DateTime?)reader["Transaction_Date"];
                clsdbo_Requisition.Grand_Total_Qty = reader["Grand_Total_Qty"] is DBNull ? null : (Int32?)reader["Grand_Total_Qty"];
                clsdbo_Requisition.Grand_Total_Amount = reader["Grand_Total_Amount"] is DBNull ? null : (Decimal?)reader["Grand_Total_Amount"];
                clsdbo_Requisition.Total_Commission = reader["Total_Commission"] is DBNull ? null : (Decimal?)reader["Total_Commission"];
                clsdbo_Requisition.Tota_Point = reader["Tota_Point"] is DBNull ? null : (Int16?)reader["Tota_Point"];
                clsdbo_Requisition.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
                clsdbo_Requisition.Replace_Sales = reader["Replace_Sales"] is DBNull ? null : reader["Replace_Sales"].ToString();
            }
            else
            {
                clsdbo_Requisition = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Requisition;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Requisition;
    }

    public static dbo_RequisitionClass Select_Record(String Requisition_No, String Time_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_RequisitionClass clsdbo_Requisition = new dbo_RequisitionClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionSelect_TimeNo]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        selectCommand.Parameters.AddWithValue("@Time_No", Time_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Requisition.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
                clsdbo_Requisition.Time_No = reader["Time_No"] is DBNull ? null : reader["Time_No"].ToString();
                clsdbo_Requisition.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_Requisition.Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"];
                clsdbo_Requisition.Transaction_Date = reader["Transaction_Date"] is DBNull ? null : (DateTime?)reader["Transaction_Date"];
                clsdbo_Requisition.Grand_Total_Qty = reader["Grand_Total_Qty"] is DBNull ? null : (Int32?)reader["Grand_Total_Qty"];
                clsdbo_Requisition.Grand_Total_Amount = reader["Grand_Total_Amount"] is DBNull ? null : (Decimal?)reader["Grand_Total_Amount"];
                clsdbo_Requisition.Total_Commission = reader["Total_Commission"] is DBNull ? null : (Decimal?)reader["Total_Commission"];
                clsdbo_Requisition.Tota_Point = reader["Tota_Point"] is DBNull ? null : (Int16?)reader["Tota_Point"];
                clsdbo_Requisition.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
                clsdbo_Requisition.Replace_Sales = reader["Replace_Sales"] is DBNull ? null : reader["Replace_Sales"].ToString();
            }
            else
            {
                clsdbo_Requisition = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Requisition;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Requisition;
    }

    public static bool Add(dbo_RequisitionClass clsdbo_Requisition, String Created_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "RequisitionInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Requisition.Requisition_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_Requisition.Requisition_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (clsdbo_Requisition.Time_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Time_No", clsdbo_Requisition.Time_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Time_No", DBNull.Value);
        }
        if (clsdbo_Requisition.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_Requisition.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (clsdbo_Requisition.Requisition_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Date", clsdbo_Requisition.Requisition_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Date", DBNull.Value);
        }
        if (clsdbo_Requisition.Transaction_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Transaction_Date", clsdbo_Requisition.Transaction_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Transaction_Date", DBNull.Value);
        }
        if (clsdbo_Requisition.Grand_Total_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Grand_Total_Qty", clsdbo_Requisition.Grand_Total_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Grand_Total_Qty", DBNull.Value);
        }
        if (clsdbo_Requisition.Grand_Total_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Grand_Total_Amount", clsdbo_Requisition.Grand_Total_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Grand_Total_Amount", DBNull.Value);
        }
        if (clsdbo_Requisition.Total_Commission.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Commission", clsdbo_Requisition.Total_Commission);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Commission", DBNull.Value);
        }
        if (clsdbo_Requisition.Tota_Point.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Tota_Point", clsdbo_Requisition.Tota_Point);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Tota_Point", DBNull.Value);
        }
        if (clsdbo_Requisition.Status != null)
        {
            insertCommand.Parameters.AddWithValue("@Status", clsdbo_Requisition.Status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }
        if (clsdbo_Requisition.Replace_Sales != null)
        {
            insertCommand.Parameters.AddWithValue("@Replace_Sales", clsdbo_Requisition.Replace_Sales);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Replace_Sales", DBNull.Value);
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

    public static bool Update(dbo_RequisitionClass newdbo_RequisitionClass)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);


        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[RequisitionUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_RequisitionClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", newdbo_RequisitionClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", DBNull.Value);
        }
        if (newdbo_RequisitionClass.Time_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewTime_No", newdbo_RequisitionClass.Time_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTime_No", DBNull.Value);
        }
        if (newdbo_RequisitionClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", newdbo_RequisitionClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", DBNull.Value);
        }
        if (newdbo_RequisitionClass.Requisition_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Date", newdbo_RequisitionClass.Requisition_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Date", DBNull.Value);
        }
        if (newdbo_RequisitionClass.Transaction_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTransaction_Date", newdbo_RequisitionClass.Transaction_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTransaction_Date", DBNull.Value);
        }
        if (newdbo_RequisitionClass.Grand_Total_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewGrand_Total_Qty", newdbo_RequisitionClass.Grand_Total_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewGrand_Total_Qty", DBNull.Value);
        }
        if (newdbo_RequisitionClass.Grand_Total_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewGrand_Total_Amount", newdbo_RequisitionClass.Grand_Total_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewGrand_Total_Amount", DBNull.Value);
        }
        if (newdbo_RequisitionClass.Total_Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Commission", newdbo_RequisitionClass.Total_Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Commission", DBNull.Value);
        }
        if (newdbo_RequisitionClass.Tota_Point.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTota_Point", newdbo_RequisitionClass.Tota_Point);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTota_Point", DBNull.Value);
        }
        if (newdbo_RequisitionClass.Status != null)
        {
            updateCommand.Parameters.AddWithValue("@NewStatus", newdbo_RequisitionClass.Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStatus", DBNull.Value);
        }

        if (newdbo_RequisitionClass.Replace_Sales != null)
        {
            updateCommand.Parameters.AddWithValue("@NewReplace_Sales", newdbo_RequisitionClass.Replace_Sales);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewReplace_Sales", DBNull.Value);
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

    public static bool Delete(dbo_RequisitionClass clsdbo_Requisition)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[RequisitionDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Requisition.Requisition_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", clsdbo_Requisition.Requisition_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", DBNull.Value);
        }
        if (clsdbo_Requisition.Time_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldTime_No", clsdbo_Requisition.Time_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTime_No", DBNull.Value);
        }
        /*if (clsdbo_Requisition.User_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", clsdbo_Requisition.User_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
        }
        if (clsdbo_Requisition.Requisition_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Date", clsdbo_Requisition.Requisition_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Date", DBNull.Value);
        }
        if (clsdbo_Requisition.Transaction_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTransaction_Date", clsdbo_Requisition.Transaction_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTransaction_Date", DBNull.Value);
        }
        if (clsdbo_Requisition.Grand_Total_Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldGrand_Total_Qty", clsdbo_Requisition.Grand_Total_Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldGrand_Total_Qty", DBNull.Value);
        }
        if (clsdbo_Requisition.Grand_Total_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldGrand_Total_Amount", clsdbo_Requisition.Grand_Total_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldGrand_Total_Amount", DBNull.Value);
        }
        if (clsdbo_Requisition.Total_Commission.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Commission", clsdbo_Requisition.Total_Commission);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Commission", DBNull.Value);
        }
        if (clsdbo_Requisition.Tota_Point.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTota_Point", clsdbo_Requisition.Tota_Point);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTota_Point", DBNull.Value);
        }
        if (clsdbo_Requisition.Status != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldStatus", clsdbo_Requisition.Status);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStatus", DBNull.Value);
        }*/
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

