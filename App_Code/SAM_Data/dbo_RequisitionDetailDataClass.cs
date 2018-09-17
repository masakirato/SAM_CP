using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;

public class dbo_RequisitionDetailDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static List<dbo_RequisitionDetailClass> GetRequisitionDetailByProductID(string ProductID)
    {
        List<dbo_RequisitionDetailClass> listOfRequisitionDetail = new List<dbo_RequisitionDetailClass>();

        if (ProductID == "91")
        {
            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "Merge",
                Product_ID = "",
                Product_Name = "180 CC. (49 ขวด/ลัง)",
                Unit_of_item = "",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });

            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "1",
                Product_ID = "72001069",
                Product_Name = "PM 180 โกลด์ แอดวานซ์จืด",
                Unit_of_item = "ขวด",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });

            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "2",
                Product_ID = "72001070",
                Product_Name = "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง",
                Unit_of_item = "ขวด",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });

            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "Merge",
                Product_ID = "",
                Product_Name = "200 CC. (49 ขวด/ลัง)",
                Unit_of_item = "",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });

            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "3",
                Product_ID = "72000720",
                Product_Name = "PM 200 เมล่อนญี่ปุ่น",
                Unit_of_item = "ขวด",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });

            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "4",
                Product_ID = "72000722",
                Product_Name = "PM 200 กลิ่นแตงโม",
                Unit_of_item = "ขวด",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });


            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "5",
                Product_ID = "72000723",
                Product_Name = "PM 200 ช็อคโกแลต",
                Unit_of_item = "ขวด",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });

        }
        else if (ProductID == "92")
        {
            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "Merge",
                Product_ID = "",
                Product_Name = "85 CC. (49 ขวด/ลัง)",
                Unit_of_item = "",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });

            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "1",
                Product_ID = "72000789",
                Product_Name = "DY 85 ส้ม",
                Unit_of_item = "ขวด",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });

            listOfRequisitionDetail.Add(new dbo_RequisitionDetailClass
            {
                Requisition_No = "1",
                Requisition_Detail_ID = "2",
                Product_ID = "72000790",
                Product_Name = "DY 85 สตรอเบอร์รี่",
                Unit_of_item = "ขวด",
                Price = Decimal.Parse("14.26"),
                Previous_Balance_Qty = 5,
                Suggestion_Qty = 10,
                Sub_Total_Qty = 0,
                Requisition_Qty = 0,
                Total_Qty = 0,
                Total = 0,
            });
        }


        return listOfRequisitionDetail;
    }
    /*

    Product_ID	Product_Name
     * 72001069	PM 180 โกลด์ แอดวานซ์จืด
72001070	PM 180 โกลด์ แอดวานซ์น้ำผึ้ง
     
72000720	PM 200 เมล่อนญี่ปุ่น
72000722	PM 200 กลิ่นแตงโม
72000723	PM 200 ช็อคโกแลต
     * 
     * 
72000789	DY 85 ส้ม
72000790	DY 85 สตรอเบอร์รี่
     * 
Product_ID	Product_Name	Price
72001069	180 CC.	14.26
72001070	180 CC.	14.26
72000720	200 CC.	8.32
72000722	200 CC.	8.18
72000723	200 CC.	8.32
72000789	85 CC.	4.03
72000790	85 CC.	4.03
    */



    public static List<dbo_ProductClass> GetRequisitionByProductGroupID(String Requisition_No, String Product_group_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetRequisitionProductGroupID";
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
                        Quantity_in__carte = reader["Quantity_in__carte"] is DBNull ? null : (Byte?)reader["Quantity_in__carte"],
                        Packing_Size = reader["Packing_Size"] is DBNull ? null : (Byte?)reader["Packing_Size"],

                        Requisition_Qty = reader["SUM_Requisition_Qty"] is DBNull ? 0 : (Int16?)reader["SUM_Requisition_Qty"],
                        Total_Qty = reader["SUM_Total_Qty"] is DBNull ? 0 : (Int16?)reader["SUM_Total_Qty"]

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


    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RequisitionDetailSelectAll]";
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

    public static List<dbo_RequisitionDetailClass> Search(String Requisition_No, String Product_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionDetailSearch]";
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
        if (!string.IsNullOrEmpty(Product_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", Product_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }


        List<dbo_RequisitionDetailClass> item = new List<dbo_RequisitionDetailClass>();
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
                    item.Add(new dbo_RequisitionDetailClass()
                    {
                        Requisition_Detail_ID = reader["Requisition_Detail_ID"] is DBNull ? null : reader["Requisition_Detail_ID"].ToString(),
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        Time_No = reader["Time_No"] is DBNull ? null : reader["Time_No"].ToString(),
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Previous_Balance_Qty = reader["Previous_Balance_Qty"] is DBNull ? null : (Int16?)reader["Previous_Balance_Qty"],
                        Suggestion_Qty = reader["Suggestion_Qty"] is DBNull ? null : (Int16?)reader["Suggestion_Qty"],
                        Sub_Total_Qty = reader["Sub_Total_Qty"] is DBNull ? null : (Int16?)reader["Sub_Total_Qty"],
                        Requisition_Qty = reader["Requisition_Qty"] is DBNull ? null : (Int16?)reader["Requisition_Qty"],
                        Total_Qty = reader["Total_Qty"] is DBNull ? null : (Int16?)reader["Total_Qty"],
                        Selling_Price = reader["Selling_Price"] is DBNull ? null : (Decimal?)reader["Selling_Price"],
                        Total_Price = reader["Total_Price"] is DBNull ? null : (Decimal?)reader["Total_Price"],
                        Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"],
                        Point = reader["Point"] is DBNull ? null : (Int16?)reader["Point"],
                        Deposit_Qty = reader["Deposit_Qty"] is DBNull ? null : (Int16?)reader["Deposit_Qty"],
                        Return_Qty = reader["Return_Qty"] is DBNull ? null : (Int16?)reader["Return_Qty"]

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
    public static List<dbo_RequisitionDetailClass> SearchByTimeNo(String Requisition_No, String Time_No)
    {


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionDetailSearchbyTimeNo]";
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


        List<dbo_RequisitionDetailClass> item = new List<dbo_RequisitionDetailClass>();
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
                    item.Add(new dbo_RequisitionDetailClass()
                    {
                        Requisition_Detail_ID = reader["Requisition_Detail_ID"] is DBNull ? null : reader["Requisition_Detail_ID"].ToString(),
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        Time_No = reader["Time_No"] is DBNull ? null : reader["Time_No"].ToString(),
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Previous_Balance_Qty = reader["Previous_Balance_Qty"] is DBNull ? null : (Int16?)reader["Previous_Balance_Qty"],
                        Suggestion_Qty = reader["Suggestion_Qty"] is DBNull ? null : (Int16?)reader["Suggestion_Qty"],
                        Sub_Total_Qty = reader["Sub_Total_Qty"] is DBNull ? null : (Int16?)reader["Sub_Total_Qty"],
                        Requisition_Qty = reader["Requisition_Qty"] is DBNull ? null : (Int16?)reader["Requisition_Qty"],
                        Total_Qty = reader["Total_Qty"] is DBNull ? null : (Int16?)reader["Total_Qty"],
                        Selling_Price = reader["Selling_Price"] is DBNull ? null : (Decimal?)reader["Selling_Price"],
                        Total_Price = reader["Total_Price"] is DBNull ? null : (Decimal?)reader["Total_Price"],
                        Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"],
                        //Point = reader["Point"] is DBNull ? null : (Int16?)reader["Point"],
                        Deposit_Qty = reader["Deposit_Qty"] is DBNull ? null : (Int16?)reader["Deposit_Qty"]
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
    public static dbo_RequisitionDetailClass Select_Record(dbo_RequisitionDetailClass clsdbo_RequisitionDetailPara)
    {
        dbo_RequisitionDetailClass clsdbo_RequisitionDetail = new dbo_RequisitionDetailClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RequisitionDetailSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Requisition_Detail_ID", clsdbo_RequisitionDetailPara.Requisition_Detail_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_RequisitionDetail.Requisition_Detail_ID = reader["Requisition_Detail_ID"] is DBNull ? null : reader["Requisition_Detail_ID"].ToString();
                clsdbo_RequisitionDetail.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
                clsdbo_RequisitionDetail.Time_No = reader["Time_No"] is DBNull ? null : reader["Time_No"].ToString();
                clsdbo_RequisitionDetail.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_RequisitionDetail.Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"];
                clsdbo_RequisitionDetail.Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"];
                clsdbo_RequisitionDetail.Previous_Balance_Qty = reader["Previous_Balance_Qty"] is DBNull ? null : (Int16?)reader["Previous_Balance_Qty"];
                clsdbo_RequisitionDetail.Suggestion_Qty = reader["Suggestion_Qty"] is DBNull ? null : (Int16?)reader["Suggestion_Qty"];
                clsdbo_RequisitionDetail.Sub_Total_Qty = reader["Sub_Total_Qty"] is DBNull ? null : (Int16?)reader["Sub_Total_Qty"];
                clsdbo_RequisitionDetail.Requisition_Qty = reader["Requisition_Qty"] is DBNull ? null : (Int16?)reader["Requisition_Qty"];
                clsdbo_RequisitionDetail.Total_Qty = reader["Total_Qty"] is DBNull ? null : (Int16?)reader["Total_Qty"];
                clsdbo_RequisitionDetail.Selling_Price = reader["Selling_Price"] is DBNull ? null : (Decimal?)reader["Selling_Price"];
                clsdbo_RequisitionDetail.Total_Price = reader["Total_Price"] is DBNull ? null : (Decimal?)reader["Total_Price"];
                clsdbo_RequisitionDetail.Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"];
                clsdbo_RequisitionDetail.Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"];
                clsdbo_RequisitionDetail.Deposit_Qty = reader["Deposit_Qty"] is DBNull ? null : (Byte?)reader["Deposit_Qty"];
            }
            else
            {
                clsdbo_RequisitionDetail = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_RequisitionDetail;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_RequisitionDetail;
    }

    public static bool Add(dbo_RequisitionDetailClass clsdbo_RequisitionDetail, String Created_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "RequisitionDetailInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_RequisitionDetail.Requisition_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Detail_ID", clsdbo_RequisitionDetail.Requisition_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Detail_ID", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Requisition_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_RequisitionDetail.Requisition_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Time_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Time_No", clsdbo_RequisitionDetail.Time_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Time_No", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_RequisitionDetail.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Price", clsdbo_RequisitionDetail.Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_RequisitionDetail.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Previous_Balance_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Previous_Balance_Qty", clsdbo_RequisitionDetail.Previous_Balance_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Previous_Balance_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Suggestion_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Suggestion_Qty", clsdbo_RequisitionDetail.Suggestion_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Suggestion_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Sub_Total_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Sub_Total_Qty", clsdbo_RequisitionDetail.Sub_Total_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sub_Total_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Requisition_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Qty", clsdbo_RequisitionDetail.Requisition_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Total_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Qty", clsdbo_RequisitionDetail.Total_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Selling_Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Selling_Price", clsdbo_RequisitionDetail.Selling_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Selling_Price", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Total_Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Price", clsdbo_RequisitionDetail.Total_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Price", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Commission.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Commission", clsdbo_RequisitionDetail.Commission);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Commission", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Point.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Point", clsdbo_RequisitionDetail.Point);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Point", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Deposit_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Deposit_Qty", clsdbo_RequisitionDetail.Deposit_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Deposit_Qty", DBNull.Value);
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

    public static bool Update(dbo_RequisitionDetailClass newdbo_RequisitionDetailClass)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[RequisitionDetailUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_RequisitionDetailClass.Requisition_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Detail_ID", newdbo_RequisitionDetailClass.Requisition_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Detail_ID", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", newdbo_RequisitionDetailClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Time_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewTime_No", newdbo_RequisitionDetailClass.Time_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTime_No", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_RequisitionDetailClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", newdbo_RequisitionDetailClass.Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_RequisitionDetailClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Previous_Balance_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPrevious_Balance_Qty", newdbo_RequisitionDetailClass.Previous_Balance_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrevious_Balance_Qty", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Suggestion_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSuggestion_Qty", newdbo_RequisitionDetailClass.Suggestion_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSuggestion_Qty", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Sub_Total_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSub_Total_Qty", newdbo_RequisitionDetailClass.Sub_Total_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSub_Total_Qty", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Requisition_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Qty", newdbo_RequisitionDetailClass.Requisition_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Qty", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Total_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Qty", newdbo_RequisitionDetailClass.Total_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Qty", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Selling_Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSelling_Price", newdbo_RequisitionDetailClass.Selling_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSelling_Price", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Total_Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Price", newdbo_RequisitionDetailClass.Total_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Price", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCommission", newdbo_RequisitionDetailClass.Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCommission", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Point.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", newdbo_RequisitionDetailClass.Point);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Deposit_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDeposit_Qty", newdbo_RequisitionDetailClass.Deposit_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDeposit_Qty", DBNull.Value);
        }
        if (newdbo_RequisitionDetailClass.Return_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewReturn_Qty", newdbo_RequisitionDetailClass.Return_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewReturn_Qty", DBNull.Value);
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

    public static bool Delete(dbo_RequisitionDetailClass clsdbo_RequisitionDetail)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[RequisitionDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_RequisitionDetail.Requisition_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Detail_ID", clsdbo_RequisitionDetail.Requisition_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Detail_ID", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Requisition_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", clsdbo_RequisitionDetail.Requisition_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Time_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldTime_No", clsdbo_RequisitionDetail.Time_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTime_No", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Product_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", clsdbo_RequisitionDetail.Product_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Price.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPrice", clsdbo_RequisitionDetail.Price);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPrice", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Vat.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", clsdbo_RequisitionDetail.Vat);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Previous_Balance_Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPrevious_Balance_Qty", clsdbo_RequisitionDetail.Previous_Balance_Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPrevious_Balance_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Suggestion_Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldSuggestion_Qty", clsdbo_RequisitionDetail.Suggestion_Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSuggestion_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Sub_Total_Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldSub_Total_Qty", clsdbo_RequisitionDetail.Sub_Total_Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSub_Total_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Requisition_Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Qty", clsdbo_RequisitionDetail.Requisition_Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Total_Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Qty", clsdbo_RequisitionDetail.Total_Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Qty", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Selling_Price.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldSelling_Price", clsdbo_RequisitionDetail.Selling_Price);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSelling_Price", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Total_Price.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Price", clsdbo_RequisitionDetail.Total_Price);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Price", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Commission.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission", clsdbo_RequisitionDetail.Commission);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission", DBNull.Value);
        }
        if (clsdbo_RequisitionDetail.Point.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPoint", clsdbo_RequisitionDetail.Point);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPoint", DBNull.Value);
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
    public static bool DeletebyTimeNo(String Requisition_No, String Time_No)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[RequisitionDetailDeletebyTimeNo]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        if (Requisition_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", Requisition_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", DBNull.Value);
        }
        if (Time_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldTime_No", Time_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTime_No", DBNull.Value);
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

