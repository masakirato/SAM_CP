using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_ProductListDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static List<dbo_ProductListClass> _Get_ProductList()
    {
        List<dbo_ProductListClass> productList = new List<dbo_ProductListClass>();

        //productList.Add(new dbo_ProductListClass()
        //{
        //    Start_Effective_Date = DateTime.Now.AddDays(-30).ToShortDateString(),
        //    End_Effective_Date = DateTime.Now.AddDays(30).ToShortDateString(),
        //    Product_ID = "72001069",
        //    Price = Decimal.Parse("10.00"),
        //    Product_Name = "PM 180 โกลด์ แอดวานซ์จืด",
        //    Vat = Byte.Parse("7")
        //});

        //productList.Add(new dbo_ProductListClass()
        //{
        //    Start_Effective_Date = DateTime.Now.AddDays(-30).ToShortDateString(),
        //    End_Effective_Date = DateTime.Now.AddDays(30).ToShortDateString(),
        //    Product_ID = "72001070",
        //    Price = Decimal.Parse("10.00"),
        //    Product_Name = "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง",
        //    Vat = Byte.Parse("7")
        //});

        //productList.Add(new dbo_ProductListClass()
        //{
        //    Start_Effective_Date = DateTime.Now.AddDays(-30).ToShortDateString(),
        //    End_Effective_Date = DateTime.Now.AddDays(30).ToShortDateString(),
        //    Product_ID = "72000720",
        //    Price = Decimal.Parse("10.00"),
        //    Product_Name = "PM 200 เมล่อนญี่ปุ่น",
        //    Vat = Byte.Parse("7")
        //});

        return productList;
    }

    public static Dictionary<string, string> GetProductIDNotInProductList(string key)
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetProductIDNotInProductList";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Price_Group_ID", key);

        DataTable dt = new DataTable();

        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);
                unit.Add("==ระบุ==", "==ระบุ==");
                foreach (DataRow row in dt.Rows)
                {
                    unit.Add(row["Product_ID"].ToString().Trim() + " " + row["Product_Name"].ToString(), row["Product_ID"].ToString().Trim());
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


    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ProductListSelectAll]";
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

    public static List<dbo_ProductListClass> Search(String Product_List_ID, String Price_Group_ID, String Product_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[ProductListSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        if (Product_List_ID != null)
        {
            selectCommand.Parameters.AddWithValue("@Product_List_ID", Product_List_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_List_ID", DBNull.Value);
        }
        if (Price_Group_ID != null)
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", Price_Group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }
        if (Product_ID != null)
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", Product_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }

        //if (Price_Group_Type != null)
        //{
        //    selectCommand.Parameters.AddWithValue("@Price_Group_Type", Price_Group_Type);
        //}
        //else
        //{
        //    selectCommand.Parameters.AddWithValue("@Price_Group_Type", DBNull.Value);
        //}



        List<dbo_ProductListClass> item = new List<dbo_ProductListClass>();


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
                    item.Add(new dbo_ProductListClass()
                    {
                        Product_List_ID = reader["Product_List_ID"] is DBNull ? null : reader["Product_List_ID"].ToString(),
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString(),
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"],
                        Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"],
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        //  Exclude_Vat = reader["Exclude_Vat"] is DBNull ? null : (Boolean?)reader["Exclude_Vat"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? null : (DateTime?)reader["Start_Effective_Date"],
                        End_Effective_Date = reader["End_Effective_Date"] is DBNull ? null : (DateTime?)reader["End_Effective_Date"],
                        CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString(),
                        Price_Group_Type = reader["Price_Group_Type"] is DBNull ? null : reader["Price_Group_Type"].ToString(),
                        StandardPrice = reader["StandardPrice"] is DBNull ? false : (Boolean)reader["StandardPrice"]
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



    public static List<dbo_ProductListClass> Get_Price_From_Group_Price(string product_id, string CV_Code, DateTime? receiving_date)
    {
        dbo_ProductListClass clsdbo_ProductList = new dbo_ProductListClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetProductListByPriceGroupID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(product_id))
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", product_id);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (receiving_date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@OrderDate", receiving_date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@OrderDate", DBNull.Value);
        }

        DataTable dt = new DataTable();

        List<dbo_ProductListClass> listofproductList = new List<dbo_ProductListClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();

            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {

                    listofproductList.Add(new dbo_ProductListClass()
                    {
                        //Product_List_ID = reader["Product_List_ID"] is DBNull ? null : reader["Product_List_ID"].ToString()
                        //,
                        //Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString()
                        //,
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString()
                            //,
                            //Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString()
                            //,
                            //CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"]
                            //,
                            //Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"]
                            //,
                            //Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"]
                            //,
                            //Exclude_Vat = reader["Exclude_Vat"] is DBNull ? null : (Boolean?)reader["Exclude_Vat"]
                            //,
                            //Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"]
                            //,
                            //Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? null : (DateTime?)reader["Start_Effective_Date"]
                            //,
                            //End_Effective_Date = reader["End_Effective_Date"] is DBNull ? null : (DateTime?)reader["End_Effective_Date"]
                            //,
                            //CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString()
                            //,
                            //SP_Price = reader["SP_Price"] is DBNull ? null : (Decimal?)reader["SP_Price"]
                            //,
                         ,
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"]
                         ,
                        datediff = reader["DATEDIFF"] is DBNull ? 0 : (int)reader["DATEDIFF"]


                    });


                }
            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return listofproductList;
        }
        finally
        {
            connection.Close();
        }
        return listofproductList;
    }




    public static List<dbo_ProductListClass> GetProductListByPriceGroupID(string Price_Group_ID)
    {
        dbo_ProductListClass clsdbo_ProductList = new dbo_ProductListClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetProductListByPriceGroupID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Price_Group_ID))
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", Price_Group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }



        DataTable dt = new DataTable();

        List<dbo_ProductListClass> listofproductList = new List<dbo_ProductListClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();

            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {

                    listofproductList.Add(new dbo_ProductListClass()
                    {

                        Product_List_ID = reader["Product_List_ID"] is DBNull ? null : reader["Product_List_ID"].ToString()
                        ,
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString()
                        ,
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString()
                        ,
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString()
                        ,
                        CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"]
                        ,
                        Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"]
                        ,
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"]
                        ,

                        //      Exclude_Vat = reader["Exclude_Vat"] is DBNull ? null : (Boolean?)reader["Exclude_Vat"]
                        //  ,

                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"]
                        ,
                        Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? null : (DateTime?)reader["Start_Effective_Date"]
                        ,
                        End_Effective_Date = reader["End_Effective_Date"] is DBNull ? null : (DateTime?)reader["End_Effective_Date"]
                        ,
                        CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString()
                        ,
                        SP_Price = reader["SP_Price"] is DBNull ? null : (Decimal?)reader["SP_Price"]
                        ,
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"]
                        ,
                        Unit_of_Item = reader["Unit_of_Item"] is DBNull ? null : reader["Unit_of_Item"].ToString()
                        ,
                        Size = reader["Size"] is DBNull ? null : (Int16?)reader["Size"]
                    });


                }
            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return listofproductList;
        }
        finally
        {
            connection.Close();
        }
        return listofproductList;
    }




    public static dbo_ProductListClass Select_Record(string Product_List_ID)
    {
        dbo_ProductListClass clsdbo_ProductList = new dbo_ProductListClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "ProductListSelect";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Product_List_ID", Product_List_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_ProductList.Product_List_ID = reader["Product_List_ID"] is DBNull ? null : reader["Product_List_ID"].ToString();
                clsdbo_ProductList.Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString();
                clsdbo_ProductList.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_ProductList.Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString();
                clsdbo_ProductList.CP_Meiji_Price = reader["CP_Meiji_Price"] is DBNull ? null : (Decimal?)reader["CP_Meiji_Price"];
                clsdbo_ProductList.Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"];
                clsdbo_ProductList.Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"];


                //clsdbo_ProductList.Exclude_Vat = reader["Exclude_Vat"] is DBNull ? null : (Boolean?)reader["Exclude_Vat"];


                clsdbo_ProductList.Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"];
                clsdbo_ProductList.Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? null : (DateTime?)reader["Start_Effective_Date"];
                clsdbo_ProductList.End_Effective_Date = reader["End_Effective_Date"] is DBNull ? null : (DateTime?)reader["End_Effective_Date"];
                clsdbo_ProductList.Product_Effective_Date = reader["Product_Effective_Date"] is DBNull ? null : (DateTime?)reader["Product_Effective_Date"];
                clsdbo_ProductList.Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"];
                clsdbo_ProductList.SP_Price = reader["SP_Price"] is DBNull ? null : (Decimal?)reader["SP_Price"];



                clsdbo_ProductList.CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString();
                clsdbo_ProductList.Price_Group_Type = reader["Price_Group_Type"] is DBNull ? null : reader["Price_Group_Type"].ToString();
            }
            else
            {
                clsdbo_ProductList = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_ProductList;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_ProductList;
    }

    public static bool Add(dbo_ProductListClass clsdbo_ProductList, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "ProductListInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_ProductList.Product_List_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_List_ID", clsdbo_ProductList.Product_List_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_List_ID", DBNull.Value);
        }
        if (clsdbo_ProductList.Price_Group_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", clsdbo_ProductList.Price_Group_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }
        if (clsdbo_ProductList.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_ProductList.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (clsdbo_ProductList.Product_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_Name", clsdbo_ProductList.Product_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_Name", DBNull.Value);
        }
        if (clsdbo_ProductList.CP_Meiji_Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@CP_Meiji_Price", clsdbo_ProductList.CP_Meiji_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CP_Meiji_Price", DBNull.Value);
        }
        if (clsdbo_ProductList.Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Price", clsdbo_ProductList.Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price", DBNull.Value);
        }
        if (clsdbo_ProductList.Point.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Point", clsdbo_ProductList.Point);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Point", DBNull.Value);
        }
        //if (clsdbo_ProductList.Exclude_Vat.HasValue == true)
        //{
        //    insertCommand.Parameters.AddWithValue("@Exclude_Vat", clsdbo_ProductList.Exclude_Vat);
        //}
        //else
        //{
        insertCommand.Parameters.AddWithValue("@Exclude_Vat", DBNull.Value);
        //}
        if (clsdbo_ProductList.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_ProductList.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (clsdbo_ProductList.Start_Effective_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@Start_Effective_Date", clsdbo_ProductList.Start_Effective_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Start_Effective_Date", DBNull.Value);
        }
        if (clsdbo_ProductList.End_Effective_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@End_Effective_Date", clsdbo_ProductList.End_Effective_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@End_Effective_Date", DBNull.Value);
        }
        if (clsdbo_ProductList.CV_CODE != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_CODE", clsdbo_ProductList.CV_CODE);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
        }


        if (clsdbo_ProductList.SP_Price != null)
        {
            insertCommand.Parameters.AddWithValue("@SP_Price", clsdbo_ProductList.SP_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SP_Price", DBNull.Value);
        }
        if (clsdbo_ProductList.Agent_Price != null)
        {
            insertCommand.Parameters.AddWithValue("@Agent_Price", clsdbo_ProductList.Agent_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Agent_Price", DBNull.Value);
        }
        if (clsdbo_ProductList.Product_Effective_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_Effective_Date", clsdbo_ProductList.Product_Effective_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_Effective_Date", DBNull.Value);
        }

        if (Created_By != null)
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

    public static bool Update(dbo_ProductListClass newdbo_ProductListClass, String Last_Modified_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "ProductListUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_ProductListClass.Product_List_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_List_ID", newdbo_ProductListClass.Product_List_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_List_ID", DBNull.Value);
        }
        if (newdbo_ProductListClass.Price_Group_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", newdbo_ProductListClass.Price_Group_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", DBNull.Value);
        }
        if (newdbo_ProductListClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_ProductListClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        if (newdbo_ProductListClass.Product_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_Name", newdbo_ProductListClass.Product_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_Name", DBNull.Value);
        }
        if (newdbo_ProductListClass.CP_Meiji_Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCP_Meiji_Price", newdbo_ProductListClass.CP_Meiji_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCP_Meiji_Price", DBNull.Value);
        }
        if (newdbo_ProductListClass.Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", newdbo_ProductListClass.Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", DBNull.Value);
        }
        if (newdbo_ProductListClass.Point.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", newdbo_ProductListClass.Point);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", DBNull.Value);
        }


        //if (newdbo_ProductListClass.Exclude_Vat.HasValue == true)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewExclude_Vat", newdbo_ProductListClass.Exclude_Vat);
        //}
        //else
        //{
        updateCommand.Parameters.AddWithValue("@NewExclude_Vat", DBNull.Value);
        //  }




        if (newdbo_ProductListClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_ProductListClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        if (newdbo_ProductListClass.Start_Effective_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@NewStart_Effective_Date", newdbo_ProductListClass.Start_Effective_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStart_Effective_Date", DBNull.Value);
        }


        if (newdbo_ProductListClass.End_Effective_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@NewEnd_Effective_Date", newdbo_ProductListClass.End_Effective_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewEnd_Effective_Date", DBNull.Value);
        }

        if (newdbo_ProductListClass.Product_Effective_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@Product_Effective_Date", newdbo_ProductListClass.Product_Effective_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Product_Effective_Date", DBNull.Value);
        }



        if (newdbo_ProductListClass.CV_CODE != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_CODE", newdbo_ProductListClass.CV_CODE);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_CODE", DBNull.Value);
        }





        if (newdbo_ProductListClass.Agent_Price.HasValue)
        {
            updateCommand.Parameters.AddWithValue("@Agent_Price", newdbo_ProductListClass.Agent_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Agent_Price", DBNull.Value);
        }


        if (newdbo_ProductListClass.SP_Price.HasValue)
        {
            updateCommand.Parameters.AddWithValue("@SP_Price", newdbo_ProductListClass.SP_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@SP_Price", DBNull.Value);
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

    public static bool Delete(string Product_List_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[ProductListDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (Product_List_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_List_ID", Product_List_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_List_ID", DBNull.Value);
        }


        //if (clsdbo_ProductList.Price_Group_ID != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPrice_Group_ID", clsdbo_ProductList.Price_Group_ID);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPrice_Group_ID", DBNull.Value);
        //}
        //if (clsdbo_ProductList.Product_ID != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldProduct_ID", clsdbo_ProductList.Product_ID);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldProduct_ID", DBNull.Value);
        //}
        //if (clsdbo_ProductList.Product_Name != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldProduct_Name", clsdbo_ProductList.Product_Name);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldProduct_Name", DBNull.Value);
        //}
        //if (clsdbo_ProductList.CP_Meiji_Price.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCP_Meiji_Price", clsdbo_ProductList.CP_Meiji_Price);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCP_Meiji_Price", DBNull.Value);
        //}
        //if (clsdbo_ProductList.Price.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPrice", clsdbo_ProductList.Price);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPrice", DBNull.Value);
        //}
        //if (clsdbo_ProductList.Point.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPoint", clsdbo_ProductList.Point);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldPoint", DBNull.Value);
        //}
        //if (clsdbo_ProductList.Exclude_Vat.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldExclude_Vat", clsdbo_ProductList.Exclude_Vat);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldExclude_Vat", DBNull.Value);
        //}
        //if (clsdbo_ProductList.Vat.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldVat", clsdbo_ProductList.Vat);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        //}
        //if (clsdbo_ProductList.Start_Effective_Date.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldStart_Effective_Date", clsdbo_ProductList.Start_Effective_Date);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldStart_Effective_Date", DBNull.Value);
        //}
        //if (clsdbo_ProductList.End_Effective_Date.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldEnd_Effective_Date", clsdbo_ProductList.End_Effective_Date);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldEnd_Effective_Date", DBNull.Value);
        //}

        //if (clsdbo_ProductList.CV_CODE != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCV_CODE", clsdbo_ProductList.CV_CODE);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCV_CODE", DBNull.Value);
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

