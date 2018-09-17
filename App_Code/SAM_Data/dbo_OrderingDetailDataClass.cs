using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_OrderingDetailDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[OrderingDetailSelectAll]";
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

    public static List<dbo_OrderingDetailClass> Search(String PO_Number)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "OrderingDetailSearch";
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

        List<dbo_OrderingDetailClass> item = new List<dbo_OrderingDetailClass>();


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
                    item.Add(

                        new dbo_OrderingDetailClass()
                        {
                            Ordering_Detail_ID = reader["Ordering_Detail_ID"] is DBNull ? null : reader["Ordering_Detail_ID"].ToString(),
                            PO_Number = reader["PO_Number"] is DBNull ? null : reader["PO_Number"].ToString(),
                            Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                            Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"],
                            Vat = reader["Vat"] is DBNull ? null : (Decimal?)reader["Vat"],
                            Stock_on_hand = reader["Stock_on_hand"] is DBNull ? null : (Int16?)reader["Stock_on_hand"],
                            Suggest_Quantity = reader["Suggest_Quantity"] is DBNull ? null : (Int16?)reader["Suggest_Quantity"],
                            Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"],
                            Sub_Total = reader["Sub_Total"] is DBNull ? null : (Decimal?)reader["Sub_Total"],
                            Vat_Amount = reader["Vat_Amount"] is DBNull ? null : (Decimal?)reader["Vat_Amount"],
                            Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"],
                            Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"],
                            Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                            Packing_Size = reader["Packing_Size"] is DBNull ? null : (Byte?)reader["Packing_Size"],
                            Unit_of_Item = reader["Unit_of_Item"] is DBNull ? null : reader["Unit_of_Item"].ToString()
                        }


                        );

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

    // RPT_OrderingDetailClass Searc
    //public static List<RPT_OrderingDetailClass> Search1(String PO_Number)
    //{
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string selectProcedure = "OrderingDetailSearch";
    //    SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
    //    selectCommand.CommandType = CommandType.StoredProcedure;

    //    if (!String.IsNullOrEmpty(PO_Number))
    //    {
    //        selectCommand.Parameters.AddWithValue("@PO_Number", PO_Number);
    //    }
    //    else
    //    {
    //        selectCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
    //    }

    //    List<RPT_OrderingDetailClass> item = new List<RPT_OrderingDetailClass>();


    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        connection.Open();
    //        SqlDataReader reader1 = selectCommand.ExecuteReader();
    //        if (reader1.HasRows)
    //        {
    //            dt.Load(reader1);

    //            foreach (DataRow reader in dt.Rows)
    //            {
    //                item.Add(

    //                    new RPT_OrderingDetailClass()
    //                    {
    //                        Ordering_Detail_ID = reader["Ordering_Detail_ID"] is DBNull ? null : reader["Ordering_Detail_ID"].ToString(),
    //                        PO_Number = reader["PO_Number"] is DBNull ? null : reader["PO_Number"].ToString(),
    //                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
    //                        Price = reader["Price"] is DBNull ? 0 : (Decimal)reader["Price"],
    //                        Vat = reader["Vat"] is DBNull ? 0 : (Decimal)reader["Vat"],
    //                        Stock_on_hand = reader["Stock_on_hand"] is DBNull ? Int16.Parse("0") : (Int16)reader["Stock_on_hand"],
    //                        Suggest_Quantity = reader["Suggest_Quantity"] is DBNull ? Int16.Parse("0") : (Int16)reader["Suggest_Quantity"],
    //                        Quantity = reader["Quantity"] is DBNull ? Int16.Parse("0") : (Int16)reader["Quantity"],
    //                        Sub_Total = reader["Sub_Total"] is DBNull ? 0 : (Decimal)reader["Sub_Total"],
    //                        Vat_Amount = reader["Vat_Amount"] is DBNull ? 0 : (Decimal)reader["Vat_Amount"],
    //                        Total = reader["Total"] is DBNull ? 0 : (Decimal)reader["Total"],
    //                        Point = reader["Point"] is DBNull ?  Byte.Parse("0"): (Byte)reader["Point"],
    //                    }


    //                    );

    //            }

    //        }
    //        reader1.Close();
    //    }
    //    catch (SqlException ex)
    //    {
    //        return item;
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //    return item;
    //}


    // public static List<Product_Quantity>

    public static dbo_OrderingDetailClass Select_Record(String Ordering_Detail_ID)
    {
        dbo_OrderingDetailClass clsdbo_OrderingDetail = new dbo_OrderingDetailClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[OrderingDetailSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Ordering_Detail_ID", Ordering_Detail_ID);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_OrderingDetail.Ordering_Detail_ID = reader["Ordering_Detail_ID"] is DBNull ? null : reader["Ordering_Detail_ID"].ToString();
                clsdbo_OrderingDetail.PO_Number = reader["PO_Number"] is DBNull ? null : reader["PO_Number"].ToString();
                clsdbo_OrderingDetail.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_OrderingDetail.Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"];
                clsdbo_OrderingDetail.Vat = reader["Vat"] is DBNull ? null : (Decimal?)reader["Vat"];
                clsdbo_OrderingDetail.Stock_on_hand = reader["Stock_on_hand"] is DBNull ? null : (Int16?)reader["Stock_on_hand"];
                clsdbo_OrderingDetail.Suggest_Quantity = reader["Suggest_Quantity"] is DBNull ? null : (Int16?)reader["Suggest_Quantity"];
                clsdbo_OrderingDetail.Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"];
                clsdbo_OrderingDetail.Sub_Total = reader["Sub_Total"] is DBNull ? null : (Decimal?)reader["Sub_Total"];
                clsdbo_OrderingDetail.Vat_Amount = reader["Vat_Amount"] is DBNull ? null : (Decimal?)reader["Vat_Amount"];
                clsdbo_OrderingDetail.Total = reader["Total"] is DBNull ? null : (Decimal?)reader["Total"];
                clsdbo_OrderingDetail.Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"];
            }
            else
            {
                clsdbo_OrderingDetail = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_OrderingDetail;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_OrderingDetail;
    }

    public static bool Add(dbo_OrderingDetailClass clsdbo_OrderingDetail, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "OrderingDetailInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_OrderingDetail.Ordering_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Ordering_Detail_ID", clsdbo_OrderingDetail.Ordering_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Ordering_Detail_ID", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.PO_Number != null)
        {
            insertCommand.Parameters.AddWithValue("@PO_Number", clsdbo_OrderingDetail.PO_Number);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@PO_Number", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_OrderingDetail.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Price", clsdbo_OrderingDetail.Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_OrderingDetail.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Stock_on_hand.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Stock_on_hand", clsdbo_OrderingDetail.Stock_on_hand);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Stock_on_hand", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Suggest_Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Suggest_Quantity", clsdbo_OrderingDetail.Suggest_Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Suggest_Quantity", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Quantity", clsdbo_OrderingDetail.Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Quantity", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Sub_Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Sub_Total", clsdbo_OrderingDetail.Sub_Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sub_Total", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Vat_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat_Amount", clsdbo_OrderingDetail.Vat_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat_Amount", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total", clsdbo_OrderingDetail.Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total", DBNull.Value);
        }
        if (clsdbo_OrderingDetail.Point.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Point", clsdbo_OrderingDetail.Point);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Point", DBNull.Value);
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

    public static bool Update(dbo_OrderingDetailClass newdbo_OrderingDetailClass, String Last_Modified_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[OrderingDetailUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;

        if (newdbo_OrderingDetailClass.Ordering_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrdering_Detail_ID", newdbo_OrderingDetailClass.Ordering_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrdering_Detail_ID", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.PO_Number != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPO_Number", newdbo_OrderingDetailClass.PO_Number);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPO_Number", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_OrderingDetailClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", newdbo_OrderingDetailClass.Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_OrderingDetailClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Stock_on_hand.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStock_on_hand", newdbo_OrderingDetailClass.Stock_on_hand);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStock_on_hand", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Suggest_Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSuggest_Quantity", newdbo_OrderingDetailClass.Suggest_Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSuggest_Quantity", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewQuantity", newdbo_OrderingDetailClass.Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewQuantity", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Sub_Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSub_Total", newdbo_OrderingDetailClass.Sub_Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSub_Total", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Vat_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat_Amount", newdbo_OrderingDetailClass.Vat_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat_Amount", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", newdbo_OrderingDetailClass.Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal", DBNull.Value);
        }
        if (newdbo_OrderingDetailClass.Point.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", newdbo_OrderingDetailClass.Point);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", DBNull.Value);
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

    public static bool Delete(String Ordering_Detail_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[OrderingDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (Ordering_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldOrdering_Detail_ID", Ordering_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldOrdering_Detail_ID", DBNull.Value);
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

