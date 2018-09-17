using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_CNDNDetailDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static List<dbo_CNDNDetailClass> _GetCNDNDetail()
    {
        List<dbo_CNDNDetailClass> item = new List<dbo_CNDNDetailClass>();

        item.Add(new dbo_CNDNDetailClass() { Product_ID = "Merge", Product_Name = "180 CC. 49ขวด/ลัง", Unit_of_item_ID = "", Quantity = 0 });
        item.Add(new dbo_CNDNDetailClass() { Product_ID = "72001069", Product_Name = "PM 180 โกลด์ แอดวานซ์จืด", Unit_of_item_ID = "ขวด", Quantity = 10 });
        item.Add(new dbo_CNDNDetailClass() { Product_ID = "72001070", Product_Name = "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง", Unit_of_item_ID = "ขวด", Quantity = 0 });

        item.Add(new dbo_CNDNDetailClass() { Product_ID = "Merge", Product_Name = "200 CC. 49ขวด/ลัง", Unit_of_item_ID = "", Quantity = 0 });
        item.Add(new dbo_CNDNDetailClass() { Product_ID = "72000720", Product_Name = "PM 200 เมล่อนญี่ปุ่น", Unit_of_item_ID = "ขวด", Quantity = 0 });
        item.Add(new dbo_CNDNDetailClass() { Product_ID = "72000723", Product_Name = "PM 200 ช็อคโกแลต", Unit_of_item_ID = "ขวด", Quantity = 0 });
        item.Add(new dbo_CNDNDetailClass() { Product_ID = "72000724", Product_Name = "PM 200 กาแฟ", Unit_of_item_ID = "ขวด", Quantity = 0 });

        return item;
    }

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CNDNDetailSelectAll]";
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

    public static List<dbo_CNDNDetailClass> Search(String SAM_CN_DN_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CNDNDetailSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!String.IsNullOrEmpty(SAM_CN_DN_No))
        {
            selectCommand.Parameters.AddWithValue("@SAM_CN_DN_No", SAM_CN_DN_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SAM_CN_DN_No", DBNull.Value);
        }

        List<dbo_CNDNDetailClass> item = new List<dbo_CNDNDetailClass>();


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

                        new dbo_CNDNDetailClass() 
                        {
                            CNDN_Detail_ID = reader["CNDN_Detail_ID"] is DBNull ? null : reader["CNDN_Detail_ID"].ToString(),
                            SAM_CN_DN_No = reader["SAM_CN_DN_No"] is DBNull ? null : reader["SAM_CN_DN_No"].ToString(),
                            Product_ID=reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                            //Product_ID = reader["[Product_ID]"] is DBNull ? null : reader["[Product_ID]"].ToString(),
                            Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"],
                            Vat = reader["Vat"] is DBNull ? null : (byte?)reader["Vat"],
                            Quantity= reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"],
                            Sub_Total = reader["Sub_Total"] is DBNull ? null : (Decimal?)reader["Sub_Total"]
                            
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

  /*  public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CNDNDetailSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "C N D N Detail I D")
        {
            selectCommand.Parameters.AddWithValue("@CNDN_Detail_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CNDN_Detail_ID", DBNull.Value);
        }
        if (sField == "S A M C N D N No")
        {
            selectCommand.Parameters.AddWithValue("@SAM_CN_DN_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SAM_CN_DN_No", DBNull.Value);
        }
        if (sField == "Product I D")
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (sField == "Price")
        {
            selectCommand.Parameters.AddWithValue("@Price", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price", DBNull.Value);
        }
        if (sField == "Vat")
        {
            selectCommand.Parameters.AddWithValue("@Vat", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (sField == "Quantity")
        {
            selectCommand.Parameters.AddWithValue("@Quantity", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Quantity", DBNull.Value);
        }
        if (sField == "Sub Total")
        {
            selectCommand.Parameters.AddWithValue("@Sub_Total", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Sub_Total", DBNull.Value);
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
    */
    public static dbo_CNDNDetailClass Select_Record(String CNDN_Detail_ID)
    {
        dbo_CNDNDetailClass clsdbo_CNDNDetail = new dbo_CNDNDetailClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CNDNDetailSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@CNDN_Detail_ID", CNDN_Detail_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CNDNDetail.CNDN_Detail_ID = reader["CNDN_Detail_ID"] is DBNull ? null : reader["CNDN_Detail_ID"].ToString();
                clsdbo_CNDNDetail.SAM_CN_DN_No = reader["SAM_CN_DN_No"] is DBNull ? null : reader["SAM_CN_DN_No"].ToString();
                clsdbo_CNDNDetail.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_CNDNDetail.Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"];
                clsdbo_CNDNDetail.Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"];
                clsdbo_CNDNDetail.Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"];
                clsdbo_CNDNDetail.Sub_Total = reader["Sub_Total"] is DBNull ? null : (Decimal?)reader["Sub_Total"];
            }
            else
            {
                clsdbo_CNDNDetail = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_CNDNDetail;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CNDNDetail;
    }

    public static bool Add(dbo_CNDNDetailClass clsdbo_CNDNDetail, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[CNDNDetailInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CNDNDetail.CNDN_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@CNDN_Detail_ID", clsdbo_CNDNDetail.CNDN_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CNDN_Detail_ID", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.SAM_CN_DN_No != null)
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_No", clsdbo_CNDNDetail.SAM_CN_DN_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_No", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_CNDNDetail.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Price", clsdbo_CNDNDetail.Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_CNDNDetail.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Quantity", clsdbo_CNDNDetail.Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Quantity", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Sub_Total.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Sub_Total", clsdbo_CNDNDetail.Sub_Total);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sub_Total", DBNull.Value);
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

    public static bool Update(dbo_CNDNDetailClass newdbo_CNDNDetailClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[CNDNDetailUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CNDNDetailClass.CNDN_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCNDN_Detail_ID", newdbo_CNDNDetailClass.CNDN_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCNDN_Detail_ID", DBNull.Value);
        }
        if (newdbo_CNDNDetailClass.SAM_CN_DN_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_No", newdbo_CNDNDetailClass.SAM_CN_DN_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_No", DBNull.Value);
        }
        if (newdbo_CNDNDetailClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_CNDNDetailClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        if (newdbo_CNDNDetailClass.Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", newdbo_CNDNDetailClass.Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", DBNull.Value);
        }
        if (newdbo_CNDNDetailClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_CNDNDetailClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        if (newdbo_CNDNDetailClass.Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewQuantity", newdbo_CNDNDetailClass.Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewQuantity", DBNull.Value);
        }
        if (newdbo_CNDNDetailClass.Sub_Total.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSub_Total", newdbo_CNDNDetailClass.Sub_Total);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSub_Total", DBNull.Value);
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

    public static bool Delete(String CNDN_Detail_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[CNDNDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (CNDN_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCNDN_Detail_ID", CNDN_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCNDN_Detail_ID", DBNull.Value);
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

  /*  public static bool Delete(dbo_CNDNDetailClass clsdbo_CNDNDetail)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[CNDNDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CNDNDetail.CNDN_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCNDN_Detail_ID", clsdbo_CNDNDetail.CNDN_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCNDN_Detail_ID", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.SAM_CN_DN_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_No", clsdbo_CNDNDetail.SAM_CN_DN_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_No", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Product_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", clsdbo_CNDNDetail.Product_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Price.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPrice", clsdbo_CNDNDetail.Price);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPrice", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Vat.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", clsdbo_CNDNDetail.Vat);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Quantity.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldQuantity", clsdbo_CNDNDetail.Quantity);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldQuantity", DBNull.Value);
        }
        if (clsdbo_CNDNDetail.Sub_Total.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldSub_Total", clsdbo_CNDNDetail.Sub_Total);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSub_Total", DBNull.Value);
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
    }*/
}

