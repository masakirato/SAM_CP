using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;
public class dbo_CNDNDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    //public static List<dbo_CNDNClass> _GetlCNDN()
    //{
    //    List<dbo_CNDNClass> item = new List<dbo_CNDNClass>();
    //    item.Add(new dbo_CNDNClass() { Invoice_No = "INV20170519001", Invoice_Date = DateTime.Now.AddDays(-15).ToShortDateString(), SAM_CN_DN_Date = DateTime.Now.AddDays(-10).ToShortDateString(), SAM_CN_DN_Status = "ยังไม่คอนเฟิร์ม" });
    //    item.Add(new dbo_CNDNClass() { Invoice_No = "INV20170519002", Invoice_Date = DateTime.Now.AddDays(-15).ToShortDateString(), SAM_CN_DN_Date = DateTime.Now.AddDays(-10).ToShortDateString(), SAM_CN_DN_Status = "ยังไม่คอนเฟิร์ม" });


    //    return item;
    //}


    //public static dbo_CNDNClass _GetCNDN(string Billing_ID)
    //{
    //    List<dbo_CNDNClass> item = new List<dbo_CNDNClass>();
    //    item.Add(new dbo_CNDNClass() { Invoice_No = "INV20170519001", Invoice_Date = DateTime.Now.AddDays(-15).ToShortDateString(), SAM_CN_DN_Date = DateTime.Now.AddDays(-10).ToShortDateString(), SAM_CN_DN_Status = "ยังไม่คอนเฟิร์ม" });
    //    item.Add(new dbo_CNDNClass() { Invoice_No = "INV20170519002", Invoice_Date = DateTime.Now.AddDays(-15).ToShortDateString(), SAM_CN_DN_Date = DateTime.Now.AddDays(-10).ToShortDateString(), SAM_CN_DN_Status = "ยังไม่คอนเฟิร์ม" });

    //    return item.Find(f => f.Invoice_No == Billing_ID);
    //}

    public static List<dbo_CNDNDetailClass> GetCNDNByProductGroupID(String Product_group_ID, String CV_Code, String SAM_CN_DN_No, DateTime? PriceDate, String Billing_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetCNDNByProductGroupID";
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

        if (!string.IsNullOrEmpty(SAM_CN_DN_No))
        {
            selectCommand.Parameters.AddWithValue("@SAM_CN_DN_No", SAM_CN_DN_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SAM_CN_DN_No", DBNull.Value);
        }
        if (PriceDate.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@PriceDate", PriceDate);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@PriceDate", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Billing_ID))
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", Billing_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
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


                String prevPacking_Size = string.Empty;
                Int16? prevSize = -1;

                int index = 1;
                foreach (DataRow reader in dt.Rows)
                {

                    if ((prevSize != (Int16?)reader["Size"] || prevPacking_Size != (reader["Unit_of_item_ID"]).ToString()) && Product_group_ID != "อื่นๆ")
                    {
                        item.Add(new dbo_CNDNDetailClass()
                        {
                            Product_ID = "Merge",

                            Product_Name = string.Format("{0} ({1}{2}/ลัง)", ((Int16?)reader["Size"]).ToString() + " CC.", (reader["Packing_Size"]).ToString(), (reader["Unit_of_item_ID"]).ToString())
                            ,
                            index = 1
                        });
                        index = 1;
                    }


                    item.Add(new dbo_CNDNDetailClass()
                    {
                        index = index++,

                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Product_Name = reader["Product_Name"] is DBNull ? null : reader["Product_Name"].ToString(),
                        Unit_of_item_ID = reader["Unit_of_item_ID"] is DBNull ? null : reader["Unit_of_item_ID"].ToString(),
                        Quantity = reader["Quantity"] is DBNull ? null : (Int16?)reader["Quantity"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"]
                        ,
                        Qty = reader["Qty"] is DBNull ? null : (Int16?)reader["Qty"]
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


    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CNDNSelectAll]";
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

    public static List<dbo_CNDNClass> Search(String Billing_ID, DateTime? Billing_Date, String Invoice_No, DateTime? Invoice_Date, String CV_Code)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CNDNSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Billing_ID))
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", Billing_ID); //เลขที่ใบแจ้งหนี้
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Invoice_No))
        {
            selectCommand.Parameters.AddWithValue("@Invoice_No", Invoice_No);//เลขที่ใบเพิ่มหนี้/ลดหนี้
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_No", DBNull.Value);
        }
        if (Billing_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Billing_Date", Billing_Date.Value.Date);//วันที่ใบแจ้งหนี้
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Billing_Date", DBNull.Value);
        }
        if (Invoice_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Date", Invoice_Date.Value.Date);//วันที่ใบลดหนี้/เพิ่มหนี้
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Invoice_Date", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }



        List<dbo_CNDNClass> item = new List<dbo_CNDNClass>();
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
                    item.Add(new dbo_CNDNClass()
                    {
                        Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString(),
                        SAM_CN_DN_No = reader["SAM_CN_DN_No"] is DBNull ? null : reader["SAM_CN_DN_No"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        SAM_CN_DN_Type = reader["SAM_CN_DN_Type"] is DBNull ? null : reader["SAM_CN_DN_Type"].ToString(),
                        SAM_CN_DN_Quantity = reader["SAM_CN_DN_Quantity"] is DBNull ? null : (Int16?)reader["SAM_CN_DN_Quantity"],
                        SAM_CN_DN_Status = reader["SAM_CN_DN_Status"] is DBNull ? null : reader["SAM_CN_DN_Status"].ToString(),
                        Invoice_No = reader["Invoice_No"] is DBNull ? null : reader["Invoice_No"].ToString(),
                        Invoice_Date = reader["Invoice_Date"] is DBNull ? null : (DateTime?)reader["Invoice_Date"],
                        Billing_Type = reader["Billing_Type"] is DBNull ? null : reader["Billing_Type"].ToString(),
                        Order_Type = reader["Order_Type"] is DBNull ? null : reader["Order_Type"].ToString(),
                        Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"],
                        SAM_CN_DN_Date = reader["SAM_CN_DN_Date"] is DBNull ? null : (DateTime?)reader["SAM_CN_DN_Date"],
                        CNDN_No = reader["CNDN_No"] is DBNull ? null : reader["CNDN_No"].ToString(),
                        CNDN_Date = reader["CNDN_Date"] is DBNull ? null : (DateTime?)reader["CNDN_Date"],
                        CNDN_ID = reader["CNDN_ID"] is DBNull ? null : reader["CNDN_ID"].ToString(),
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

    public static dbo_CNDNClass Select_Record(String SAM_CN_DN_No)
    {
        dbo_CNDNClass clsdbo_CNDN = new dbo_CNDNClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CNDNSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@SAM_CN_DN_No", SAM_CN_DN_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CNDN.SAM_CN_DN_No = reader["SAM_CN_DN_No"] is DBNull ? null : reader["SAM_CN_DN_No"].ToString();
                clsdbo_CNDN.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_CNDN.SAM_CN_DN_Type = reader["SAM_CN_DN_Type"] is DBNull ? null : reader["SAM_CN_DN_Type"].ToString();
                clsdbo_CNDN.SAM_CN_DN_Date = reader["SAM_CN_DN_Date"] is DBNull ? null : (DateTime?)reader["SAM_CN_DN_Date"];
                clsdbo_CNDN.SAM_CN_DN_Quantity = reader["SAM_CN_DN_Quantity"] is DBNull ? null : (Int16?)reader["SAM_CN_DN_Quantity"];
                clsdbo_CNDN.SAM_CN_DN_Status = reader["SAM_CN_DN_Status"] is DBNull ? null : reader["SAM_CN_DN_Status"].ToString();
                clsdbo_CNDN.Billing_ID = reader["Billing_ID"] is DBNull ? null : reader["Billing_ID"].ToString();
                clsdbo_CNDN.Invoice_No = reader["Invoice_No"] is DBNull ? null : reader["Invoice_No"].ToString();
                clsdbo_CNDN.Invoice_Date = reader["Invoice_Date"] is DBNull ? null : (DateTime?)reader["Invoice_Date"];
            }
            else
            {
                clsdbo_CNDN = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_CNDN;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CNDN;
    }

    public static bool Add(dbo_CNDNClass clsdbo_CNDN, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[CNDNInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CNDN.SAM_CN_DN_No != null)
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_No", clsdbo_CNDN.SAM_CN_DN_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_No", DBNull.Value);
        }
        if (clsdbo_CNDN.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_CNDN.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (clsdbo_CNDN.SAM_CN_DN_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_Type", clsdbo_CNDN.SAM_CN_DN_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_Type", DBNull.Value);
        }
        if (clsdbo_CNDN.SAM_CN_DN_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_Date", clsdbo_CNDN.SAM_CN_DN_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_Date", DBNull.Value);
        }
        if (clsdbo_CNDN.SAM_CN_DN_Quantity.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_Quantity", clsdbo_CNDN.SAM_CN_DN_Quantity);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_Quantity", DBNull.Value);
        }
        if (clsdbo_CNDN.SAM_CN_DN_Status != null)
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_Status", clsdbo_CNDN.SAM_CN_DN_Status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SAM_CN_DN_Status", DBNull.Value);
        }
        if (clsdbo_CNDN.Billing_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Billing_ID", clsdbo_CNDN.Billing_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Billing_ID", DBNull.Value);
        }
        if (clsdbo_CNDN.Invoice_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_No", clsdbo_CNDN.Invoice_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_No", DBNull.Value);
        }
        if (clsdbo_CNDN.Invoice_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Date", clsdbo_CNDN.Invoice_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Date", DBNull.Value);
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

    public static bool Update(dbo_CNDNClass newdbo_CNDNClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[CNDNUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CNDNClass.SAM_CN_DN_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_No", newdbo_CNDNClass.SAM_CN_DN_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_No", DBNull.Value);
        }
        if (newdbo_CNDNClass.CV_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", newdbo_CNDNClass.CV_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", DBNull.Value);
        }
        if (newdbo_CNDNClass.SAM_CN_DN_Type != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_Type", newdbo_CNDNClass.SAM_CN_DN_Type);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_Type", DBNull.Value);
        }
        if (newdbo_CNDNClass.SAM_CN_DN_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_Date", newdbo_CNDNClass.SAM_CN_DN_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_Date", DBNull.Value);
        }
        if (newdbo_CNDNClass.SAM_CN_DN_Quantity.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_Quantity", newdbo_CNDNClass.SAM_CN_DN_Quantity);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_Quantity", DBNull.Value);
        }
        if (newdbo_CNDNClass.SAM_CN_DN_Status != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_Status", newdbo_CNDNClass.SAM_CN_DN_Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSAM_CN_DN_Status", DBNull.Value);
        }
        if (newdbo_CNDNClass.Billing_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_ID", newdbo_CNDNClass.Billing_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBilling_ID", DBNull.Value);
        }
        if (newdbo_CNDNClass.Invoice_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_No", newdbo_CNDNClass.Invoice_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_No", DBNull.Value);
        }
        if (newdbo_CNDNClass.Invoice_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Date", newdbo_CNDNClass.Invoice_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Date", DBNull.Value);
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

    public static bool Delete(string SAM_CN_DN_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[CNDNDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (SAM_CN_DN_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_No", SAM_CN_DN_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_No", DBNull.Value);
        }
        /*if (clsdbo_CNDN.CV_Code != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", clsdbo_CNDN.CV_Code);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", DBNull.Value);
        }
        if (clsdbo_CNDN.SAM_CN_DN_Type != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_Type", clsdbo_CNDN.SAM_CN_DN_Type);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_Type", DBNull.Value);
        }*/
        //if (clsdbo_CNDN.SAM_CN_DN_Date.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_Date", clsdbo_CNDN.SAM_CN_DN_Date);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_Date", DBNull.Value);
        //}
        /*if (clsdbo_CNDN.SAM_CN_DN_Quantity.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_Quantity", clsdbo_CNDN.SAM_CN_DN_Quantity);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_Quantity", DBNull.Value);
        }
        if (clsdbo_CNDN.SAM_CN_DN_Status != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_Status", clsdbo_CNDN.SAM_CN_DN_Status);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSAM_CN_DN_Status", DBNull.Value);
        }
        if (clsdbo_CNDN.Billing_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_ID", clsdbo_CNDN.Billing_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBilling_ID", DBNull.Value);
        }
        if (clsdbo_CNDN.Invoice_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_No", clsdbo_CNDN.Invoice_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInvoice_No", DBNull.Value);
        }*/
        //if (clsdbo_CNDN.Invoice_Date.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldInvoice_Date", clsdbo_CNDN.Invoice_Date);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldInvoice_Date", DBNull.Value);
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

