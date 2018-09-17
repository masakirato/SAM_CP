using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;
//using System.Web;

public class dbo_StockDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[StockSelectAll]";
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

    public static List<dbo_StockClass> Search(String CV_Code, String Stock_on_Hand_ID, String Product_ID)
    {
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[StockSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!string.IsNullOrEmpty(Stock_on_Hand_ID))
        {
            selectCommand.Parameters.AddWithValue("@Stock_on_Hand_ID", Stock_on_Hand_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Stock_on_Hand_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Product_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", Product_ID);
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
        //if (Invoice_DateEnd.HasValue)
        //{
        //    selectCommand.Parameters.AddWithValue("@Invoice_DateEnd", Invoice_DateEnd.Value);
        //}
        //else
        //{
        selectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
        //}
        List<dbo_StockClass> item = new List<dbo_StockClass>();
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
                    item.Add(new dbo_StockClass()
                    {
                        Stock_on_Hand_ID = reader["Stock_on_Hand_ID"] is DBNull ? null : reader["Stock_on_Hand_ID"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        Date = reader["Date"] is DBNull ? null : (DateTime?)reader["Date"],
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Stock_Begin = reader["Stock_Begin"] is DBNull ? 0 : (Int16?)reader["Stock_Begin"],
                        Stock_In = reader["Stock_In"] is DBNull ? 0 : (Int16?)reader["Stock_In"],
                        Stock_Out = reader["Stock_Out"] is DBNull ? 0 : (Int16?)reader["Stock_Out"],
                        Stock_End = reader["Stock_End"] is DBNull ? 0 : (Int16?)reader["Stock_End"]
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

    public static dbo_StockClass Select_Record(String Stock_on_Hand_ID)
    {
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_StockClass clsdbo_Stock = new dbo_StockClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[StockSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Stock_on_Hand_ID", Stock_on_Hand_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Stock.Stock_on_Hand_ID = reader["Stock_on_Hand_ID"] is DBNull ? null : reader["Stock_on_Hand_ID"].ToString();
                clsdbo_Stock.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_Stock.Date = reader["Date"] is DBNull ? null : (DateTime?)reader["Date"];
                clsdbo_Stock.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_Stock.Stock_Begin = reader["Stock_Begin"] is DBNull ? null : (Int16?)reader["Stock_Begin"];
                clsdbo_Stock.Stock_In = reader["Stock_In"] is DBNull ? null : (Int16?)reader["Stock_In"];
                clsdbo_Stock.Stock_Out = reader["Stock_Out"] is DBNull ? null : (Int16?)reader["Stock_Out"];
                clsdbo_Stock.Stock_End = reader["Stock_End"] is DBNull ? null : (Int16?)reader["Stock_End"];
            }
            else
            {
                clsdbo_Stock = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Stock;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Stock;
    }

    public static bool Add(dbo_StockClass clsdbo_Stock, String Created_By)
    {
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[StockInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Stock.Stock_on_Hand_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Stock_on_Hand_ID", clsdbo_Stock.Stock_on_Hand_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Stock_on_Hand_ID", DBNull.Value);
        }
        if (clsdbo_Stock.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_Stock.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (clsdbo_Stock.Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Date", clsdbo_Stock.Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Date", DBNull.Value);
        }
        if (clsdbo_Stock.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_Stock.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (clsdbo_Stock.Stock_Begin.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Stock_Begin", clsdbo_Stock.Stock_Begin);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Stock_Begin", DBNull.Value);
        }
        if (clsdbo_Stock.Stock_In.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Stock_In", clsdbo_Stock.Stock_In);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Stock_In", DBNull.Value);
        }
        if (clsdbo_Stock.Stock_Out.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Stock_Out", clsdbo_Stock.Stock_Out);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Stock_Out", DBNull.Value);
        }
        if (clsdbo_Stock.Stock_End.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Stock_End", clsdbo_Stock.Stock_End);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Stock_End", DBNull.Value);
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

    public static bool Update(dbo_StockClass newdbo_StockClass, String Last_Modified_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[StockUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_StockClass.Stock_on_Hand_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewStock_on_Hand_ID", newdbo_StockClass.Stock_on_Hand_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStock_on_Hand_ID", DBNull.Value);
        }
        if (newdbo_StockClass.CV_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", newdbo_StockClass.CV_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", DBNull.Value);
        }
        if (newdbo_StockClass.Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDate", newdbo_StockClass.Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDate", DBNull.Value);
        }
        if (newdbo_StockClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_StockClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        if (newdbo_StockClass.Stock_Begin.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStock_Begin", newdbo_StockClass.Stock_Begin);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStock_Begin", DBNull.Value);
        }
        if (newdbo_StockClass.Stock_In.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStock_In", newdbo_StockClass.Stock_In);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStock_In", DBNull.Value);
        }
        if (newdbo_StockClass.Stock_Out.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStock_Out", newdbo_StockClass.Stock_Out);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStock_Out", DBNull.Value);
        }
        if (newdbo_StockClass.Stock_End.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStock_End", newdbo_StockClass.Stock_End);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStock_End", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Last_Modified_By))
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

    public static bool Delete(dbo_StockClass clsdbo_Stock)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[StockDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Stock.Stock_on_Hand_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_on_Hand_ID", clsdbo_Stock.Stock_on_Hand_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_on_Hand_ID", DBNull.Value);
        }
        if (clsdbo_Stock.CV_Code != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", clsdbo_Stock.CV_Code);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", DBNull.Value);
        }
        if (clsdbo_Stock.Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldDate", clsdbo_Stock.Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldDate", DBNull.Value);
        }
        if (clsdbo_Stock.Product_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", clsdbo_Stock.Product_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", DBNull.Value);
        }
        if (clsdbo_Stock.Stock_Begin.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_Begin", clsdbo_Stock.Stock_Begin);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_Begin", DBNull.Value);
        }
        if (clsdbo_Stock.Stock_In.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_In", clsdbo_Stock.Stock_In);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_In", DBNull.Value);
        }
        if (clsdbo_Stock.Stock_Out.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_Out", clsdbo_Stock.Stock_Out);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_Out", DBNull.Value);
        }
        if (clsdbo_Stock.Stock_End.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_End", clsdbo_Stock.Stock_End);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_End", DBNull.Value);
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



    public static List<dbo_ProductClass> GetStockByProductGroupID(String Product_group_ID, String CV_Code, String Count_No, DateTime? PriceDate)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetStockByProductGroupID";
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
        if (!string.IsNullOrEmpty(Count_No))
        {
            selectCommand.Parameters.AddWithValue("@Count_No", Count_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Count_No", DBNull.Value);
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
                String prevPacking_Size = string.Empty;

                int index = 0;
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
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                        //Quantity = reader["Stock_End"] is DBNull ? null : (Int16?)reader["Stock_End"],
                        Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"],
                        Count_Quantity = reader["Count_Quantity"] is DBNull ? null : (Int16?)reader["Count_Quantity"],
                        Diff_Quantity = reader["Diff_Quantity"] is DBNull ?Int16.Parse("0") : Int16.Parse(reader["Diff_Quantity"].ToString()),
                        Packing_Size = 0,
                        Status = null,
                        Stock = reader["Stock_End"] is DBNull ? null : (Int16?)reader["Stock_End"],
                        CP_Meiji_Price = 0,
                        Stock_on_Hand_ID = reader["Stock_on_Hand_ID"] is DBNull ? null : reader["Stock_on_Hand_ID"].ToString(),
                        Remark = reader["Remark"] is DBNull ? null : reader["Remark"].ToString()
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
            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static List<dbo_ProductClass> GetStockonHandByProductGroupID(String Product_group_ID, String CV_Code)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetStockonHandByProductGroupID";
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

                int index = 0;
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
                        Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                        //Quantity = reader["Stock_End"] is DBNull ? null : (Int16?)reader["Stock_End"],
                        //Count_Quantity = reader["Count_Quantity"] is DBNull ? null : (Int16?)reader["Count_Quantity"],
                        //Diff_Quantity = reader["Diff_Quantity"] is DBNull ? null : (Int16?)reader["Diff_Quantity"],
                        Packing_Size = 0,
                        Status = null,
                        Stock = reader["Stock_End"] is DBNull ? 0 : (Int16?)reader["Stock_End"],
                        CP_Meiji_Price = 0,
                        Stock_on_Hand_ID = reader["Stock_on_Hand_ID"] is DBNull ? null : reader["Stock_on_Hand_ID"].ToString(),
                        Net_Value = reader["Amount"] is DBNull ? null : (Decimal?)reader["Amount"]
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
            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }
}

