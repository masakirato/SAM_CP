using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;
public class dbo_OtherRequisitionDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static List<dbo_ProductClass> GetOtherRequisitionByProductGroupID(String User_ID, DateTime? Requisition_Date, String Product_group_ID, String CV_Code, string Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetOtherRequisitionByProductGroupID";
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
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
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
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Order_No = 0,
                        Packing_Size = 0,
                        Status = null,
                        Total_Qty = reader["Requisition_Qty"] is DBNull ? 0 : (Int16?)reader["Requisition_Qty"],
                        Stock = reader["Stock"] is DBNull ? null : (Int16?)reader["Stock"],
                        Stock_on_Hand_ID = reader["Stock_on_Hand_ID"] is DBNull ? null : reader["Stock_on_Hand_ID"].ToString(),
                        Other_Requisition_Detail_ID = reader["Other_Requisition_Detail_ID"] is DBNull ? null : reader["Other_Requisition_Detail_ID"].ToString()


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
    public static List<dbo_ProductClass> GetOtherRequisitionByProductGroupID_View(String User_ID, DateTime? Requisition_Date, String Product_group_ID, String CV_Code, string Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetOtherRequisitionByProductGroupID_View";
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
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
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
                        Agent_Price = reader["Agent_Price"] is DBNull ? null : (Decimal?)reader["Agent_Price"],
                        Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                        Order_No = 0,
                        Packing_Size = 0,
                        Status = null,
                        Total_Qty = reader["Requisition_Qty"] is DBNull ? null : (Int16?)reader["Requisition_Qty"],
                        Stock = reader["Stock"] is DBNull ? null : (Int16?)reader["Stock"],
                        Stock_on_Hand_ID = reader["Stock_on_Hand_ID"] is DBNull ? null : reader["Stock_on_Hand_ID"].ToString(),
                        Other_Requisition_Detail_ID = reader["Other_Requisition_Detail_ID"] is DBNull ? null : reader["Other_Requisition_Detail_ID"].ToString()


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
        string selectProcedure = "[dbo].[OtherRequisitionSelectAll]";
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

    public static List<dbo_OtherRequisitionClass> Search(DateTime? StartDate, DateTime? EndDate, String Requisition_Name, String Reason,String CV_Code)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[OtherRequisitionSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

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
           // selectCommand.Parameters.AddWithValue("@EndDate", EndDate.Value.AddDays(1).AddTicks(-1));
            selectCommand.Parameters.AddWithValue("@EndDate", EndDate.Value.Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@EndDate", DateTime.Now.AddYears(10));
        }
        if (!string.IsNullOrEmpty(Requisition_Name))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Name", Requisition_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Name", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Reason))
        {
            selectCommand.Parameters.AddWithValue("@Reason", Reason);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Reason", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }




        DataTable dt = new DataTable();
        List<dbo_OtherRequisitionClass> item = new List<dbo_OtherRequisitionClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_OtherRequisitionClass()
                    {
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"],
                        Requisition_Name = reader["Requisition_Name"] is DBNull ? null : reader["Requisition_Name"].ToString(),
                        Other_Requisition_Name = reader["Other_Requisition_Name"] is DBNull ? null : reader["Other_Requisition_Name"].ToString(),
                        Reason = reader["Reason"] is DBNull ? null : reader["Reason"].ToString(),
                        Other_reason = reader["Other_reason"] is DBNull ? null : reader["Other_reason"].ToString(),
                        Grand_Total_Qty = reader["Grand_Total_Qty"] is DBNull ? null : (Int32?)reader["Grand_Total_Qty"],
                        Grand_Total_Amount = reader["Grand_Total_Amount"] is DBNull ? null : (Decimal?)reader["Grand_Total_Amount"],
                        Requisition_FullName = reader["Requisition_FullName"] is DBNull ? null : reader["Requisition_FullName"].ToString()
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

    public static dbo_OtherRequisitionClass Select_Record(String Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_OtherRequisitionClass clsdbo_OtherRequisition = new dbo_OtherRequisitionClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[OtherRequisitionSelect]";
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
                clsdbo_OtherRequisition.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
                clsdbo_OtherRequisition.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_OtherRequisition.Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"];
                clsdbo_OtherRequisition.Requisition_Name = reader["Requisition_Name"] is DBNull ? null : reader["Requisition_Name"].ToString();
                clsdbo_OtherRequisition.Other_Requisition_Name = reader["Other_Requisition_Name"] is DBNull ? null : reader["Other_Requisition_Name"].ToString();
                clsdbo_OtherRequisition.Reason = reader["Reason"] is DBNull ? null : reader["Reason"].ToString();
                clsdbo_OtherRequisition.Other_reason = reader["Other_reason"] is DBNull ? null : reader["Other_reason"].ToString();
                clsdbo_OtherRequisition.Grand_Total_Qty = reader["Grand_Total_Qty"] is DBNull ? null : (Int32?)reader["Grand_Total_Qty"];
                clsdbo_OtherRequisition.Grand_Total_Amount = reader["Grand_Total_Amount"] is DBNull ? null : (Decimal?)reader["Grand_Total_Amount"];
            }
            else
            {
                clsdbo_OtherRequisition = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_OtherRequisition;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_OtherRequisition;
    }

    public static bool Add(dbo_OtherRequisitionClass clsdbo_OtherRequisition, String Created_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[OtherRequisitionInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_OtherRequisition.Requisition_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_OtherRequisition.Requisition_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_OtherRequisition.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Requisition_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Date", clsdbo_OtherRequisition.Requisition_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Date", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Requisition_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Name", clsdbo_OtherRequisition.Requisition_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Name", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Other_Requisition_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Other_Requisition_Name", clsdbo_OtherRequisition.Other_Requisition_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Other_Requisition_Name", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Reason != null)
        {
            insertCommand.Parameters.AddWithValue("@Reason", clsdbo_OtherRequisition.Reason);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Reason", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Other_reason != null)
        {
            insertCommand.Parameters.AddWithValue("@Other_reason", clsdbo_OtherRequisition.Other_reason);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Other_reason", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Grand_Total_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Grand_Total_Qty", clsdbo_OtherRequisition.Grand_Total_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Grand_Total_Qty", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Grand_Total_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Grand_Total_Amount", clsdbo_OtherRequisition.Grand_Total_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Grand_Total_Amount", DBNull.Value);
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

    public static bool Update(dbo_OtherRequisitionClass newdbo_OtherRequisitionClass)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[OtherRequisitionUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_OtherRequisitionClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", newdbo_OtherRequisitionClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", DBNull.Value);
        }


        if (newdbo_OtherRequisitionClass.CV_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", newdbo_OtherRequisitionClass.CV_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", DBNull.Value);
        }


        if (newdbo_OtherRequisitionClass.Requisition_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Date", newdbo_OtherRequisitionClass.Requisition_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Date", DBNull.Value);
        }
        if (newdbo_OtherRequisitionClass.Requisition_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Name", newdbo_OtherRequisitionClass.Requisition_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Name", DBNull.Value);
        }
        if (newdbo_OtherRequisitionClass.Other_Requisition_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOther_Requisition_Name", newdbo_OtherRequisitionClass.Other_Requisition_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOther_Requisition_Name", DBNull.Value);
        }
        if (newdbo_OtherRequisitionClass.Reason != null)
        {
            updateCommand.Parameters.AddWithValue("@NewReason", newdbo_OtherRequisitionClass.Reason);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewReason", DBNull.Value);
        }
        if (newdbo_OtherRequisitionClass.Other_reason != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOther_reason", newdbo_OtherRequisitionClass.Other_reason);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOther_reason", DBNull.Value);
        }
        if (newdbo_OtherRequisitionClass.Grand_Total_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewGrand_Total_Qty", newdbo_OtherRequisitionClass.Grand_Total_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewGrand_Total_Qty", DBNull.Value);
        }
        if (newdbo_OtherRequisitionClass.Grand_Total_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewGrand_Total_Amount", newdbo_OtherRequisitionClass.Grand_Total_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewGrand_Total_Amount", DBNull.Value);
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

    public static bool Delete(dbo_OtherRequisitionClass clsdbo_OtherRequisition)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[OtherRequisitionDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_OtherRequisition.Requisition_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", clsdbo_OtherRequisition.Requisition_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.CV_Code != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", clsdbo_OtherRequisition.CV_Code);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Requisition_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Date", clsdbo_OtherRequisition.Requisition_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Date", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Requisition_Name != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Name", clsdbo_OtherRequisition.Requisition_Name);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Name", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Other_Requisition_Name != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldOther_Requisition_Name", clsdbo_OtherRequisition.Other_Requisition_Name);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldOther_Requisition_Name", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Reason != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldReason", clsdbo_OtherRequisition.Reason);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldReason", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Grand_Total_Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldGrand_Total_Qty", clsdbo_OtherRequisition.Grand_Total_Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldGrand_Total_Qty", DBNull.Value);
        }
        if (clsdbo_OtherRequisition.Grand_Total_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldGrand_Total_Amount", clsdbo_OtherRequisition.Grand_Total_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldGrand_Total_Amount", DBNull.Value);
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

