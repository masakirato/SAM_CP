using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;
public class dbo_OrderAndDeliveryCycleValueDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[OrderAndDeliveryCycleValueSelectAll]";
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


    public static List<dbo_OrderAndDeliveryCycleValueClass> GetWindowTime(string CV_Code)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetWindowTime";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }


        List<dbo_OrderAndDeliveryCycleValueClass> item = new List<dbo_OrderAndDeliveryCycleValueClass>();


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
                    item.Add(new dbo_OrderAndDeliveryCycleValueClass()
                    {
                        Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString(),
                        CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString(),
                        Order_Cycle_Date = reader["Order_Cycle_Date"] is DBNull ? null : reader["Order_Cycle_Date"].ToString(),
                        Order_Cycle_Hour = reader["Order_Cycle_Hour"] is DBNull ? null : reader["Order_Cycle_Hour"].ToString(),
                        WindowTime = reader["WindowTime"] is DBNull ? null : reader["WindowTime"].ToString()
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


    public static List<dbo_OrderAndDeliveryCycleValueClass> Search(string Order_Cycle_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "OrderAndDeliveryCycleValueSearch";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Order_Cycle_ID))
        {
            selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", Order_Cycle_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", DBNull.Value);
        }


        List<dbo_OrderAndDeliveryCycleValueClass> item = new List<dbo_OrderAndDeliveryCycleValueClass>();


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
                    item.Add(new dbo_OrderAndDeliveryCycleValueClass()
                    {
                        Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString(),
                        CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString(),
                        Order_Cycle_Date = reader["Order_Cycle_Date"] is DBNull ? null : reader["Order_Cycle_Date"].ToString(),
                        Order_Cycle_Hour = reader["Order_Cycle_Hour"] is DBNull ? null : reader["Order_Cycle_Hour"].ToString(),
                        Order_Cycle_Minute = reader["Order_Cycle_Minute"] is DBNull ? null : reader["Order_Cycle_Minute"].ToString(),
                        Delivery_Cycle_Date = reader["Delivery_Cycle_Date"] is DBNull ? null : reader["Delivery_Cycle_Date"].ToString(),
                        Route = reader["Route"] is DBNull ? null : reader["Route"].ToString(),
                        OrderAndDeliveryCycleValue_ID = reader["OrderAndDeliveryCycleValue_ID"] is DBNull ? null : (int?)reader["OrderAndDeliveryCycleValue_ID"]
                        ,
                        Order_Cycle_Date_Name = reader["Order_Cycle_Date_Name"] is DBNull ? null : reader["Order_Cycle_Date_Name"].ToString()
                        ,
                        Delivery_Cycle_Date_Name = reader["Delivery_Cycle_Date_Name"] is DBNull ? null : reader["Delivery_Cycle_Date_Name"].ToString()
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

    public static dbo_OrderAndDeliveryCycleValueClass Select_Record(dbo_OrderAndDeliveryCycleValueClass clsdbo_OrderAndDeliveryCycleValuePara)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_OrderAndDeliveryCycleValueClass clsdbo_OrderAndDeliveryCycleValue = new dbo_OrderAndDeliveryCycleValueClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[OrderAndDeliveryCycleValueSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", clsdbo_OrderAndDeliveryCycleValuePara.Order_Cycle_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString();
                clsdbo_OrderAndDeliveryCycleValue.CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString();
                clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Date = reader["Order_Cycle_Date"] is DBNull ? null : reader["Order_Cycle_Date"].ToString();
                clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Hour = reader["Order_Cycle_Hour"] is DBNull ? null : reader["Order_Cycle_Hour"].ToString();
                clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Minute = reader["Order_Cycle_Minute"] is DBNull ? null : reader["Order_Cycle_Minute"].ToString();
                clsdbo_OrderAndDeliveryCycleValue.Delivery_Cycle_Date = reader["Delivery_Cycle_Date"] is DBNull ? null : reader["Delivery_Cycle_Date"].ToString();
                clsdbo_OrderAndDeliveryCycleValue.Route = reader["Route"] is DBNull ? null : reader["Route"].ToString();
                clsdbo_OrderAndDeliveryCycleValue.OrderAndDeliveryCycleValue_ID = reader["OrderAndDeliveryCycleValue_ID"] is DBNull ? null : (int?)reader["OrderAndDeliveryCycleValue_ID"];
            }
            else
            {
                clsdbo_OrderAndDeliveryCycleValue = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_OrderAndDeliveryCycleValue;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_OrderAndDeliveryCycleValue;
    }

    public static bool Add(dbo_OrderAndDeliveryCycleValueClass clsdbo_OrderAndDeliveryCycleValue, String Created_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "OrderAndDeliveryCycleValueInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_ID", clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_ID", DBNull.Value);
        }
        //if (clsdbo_OrderAndDeliveryCycleValue.CV_CODE != null)
        //{
        //    insertCommand.Parameters.AddWithValue("@CV_CODE", clsdbo_OrderAndDeliveryCycleValue.CV_CODE);
        //}
        //else
        //{
        insertCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
        //}
        if (clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_Date", clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_Date", DBNull.Value);
        }
        if (clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Hour != null)
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_Hour", clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Hour);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_Hour", DBNull.Value);
        }
        if (clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Minute != null)
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_Minute", clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Minute);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_Minute", DBNull.Value);
        }
        if (clsdbo_OrderAndDeliveryCycleValue.Delivery_Cycle_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@Delivery_Cycle_Date", clsdbo_OrderAndDeliveryCycleValue.Delivery_Cycle_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Delivery_Cycle_Date", DBNull.Value);
        }

        if (clsdbo_OrderAndDeliveryCycleValue.Route != null)
        {
            insertCommand.Parameters.AddWithValue("@Route", clsdbo_OrderAndDeliveryCycleValue.Route);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Route", DBNull.Value);
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

    public static bool Update(dbo_OrderAndDeliveryCycleValueClass newdbo_OrderAndDeliveryCycleValueClass, String Last_Modified_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[OrderAndDeliveryCycleValueUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;


        if (newdbo_OrderAndDeliveryCycleValueClass.Order_Cycle_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_ID", newdbo_OrderAndDeliveryCycleValueClass.Order_Cycle_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_ID", DBNull.Value);
        }


        //if (newdbo_OrderAndDeliveryCycleValueClass.CV_CODE != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewCV_CODE", newdbo_OrderAndDeliveryCycleValueClass.CV_CODE);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewCV_CODE", DBNull.Value);
        //}


        if (newdbo_OrderAndDeliveryCycleValueClass.Order_Cycle_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Date", newdbo_OrderAndDeliveryCycleValueClass.Order_Cycle_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Date", DBNull.Value);
        }
        if (newdbo_OrderAndDeliveryCycleValueClass.Order_Cycle_Hour != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Hour", newdbo_OrderAndDeliveryCycleValueClass.Order_Cycle_Hour);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Hour", DBNull.Value);
        }
        if (newdbo_OrderAndDeliveryCycleValueClass.Order_Cycle_Minute != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Minute", newdbo_OrderAndDeliveryCycleValueClass.Order_Cycle_Minute);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Minute", DBNull.Value);
        }
        if (newdbo_OrderAndDeliveryCycleValueClass.Delivery_Cycle_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDelivery_Cycle_Date", newdbo_OrderAndDeliveryCycleValueClass.Delivery_Cycle_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDelivery_Cycle_Date", DBNull.Value);
        }


        if (newdbo_OrderAndDeliveryCycleValueClass.Route != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRoute", newdbo_OrderAndDeliveryCycleValueClass.Route);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRoute", DBNull.Value);
        }


        //if (newdbo_OrderAndDeliveryCycleValueClass.Route != null)
        //{
        //    updateCommand.Parameters.AddWithValue("@NewRoute", newdbo_OrderAndDeliveryCycleValueClass.Route);
        //}
        //else
        //{
        //    updateCommand.Parameters.AddWithValue("@NewRoute", DBNull.Value);
        //}

        if (newdbo_OrderAndDeliveryCycleValueClass.OrderAndDeliveryCycleValue_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OrderAndDeliveryCycleValue_ID", newdbo_OrderAndDeliveryCycleValueClass.OrderAndDeliveryCycleValue_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OrderAndDeliveryCycleValue_ID", DBNull.Value);
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

    public static bool Delete(int? OrderAndDeliveryCycleValue_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[OrderAndDeliveryCycleValueDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        //if (clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_ID != null)
        //{
        deleteCommand.Parameters.AddWithValue("@OrderAndDeliveryCycleValue_ID", OrderAndDeliveryCycleValue_ID);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_ID", DBNull.Value);
        //}

        //if (clsdbo_OrderAndDeliveryCycleValue.CV_CODE != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCV_CODE", clsdbo_OrderAndDeliveryCycleValue.CV_CODE);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldCV_CODE", DBNull.Value);
        //}




        //if (clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Date != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Date", clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Date);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Date", DBNull.Value);
        //}
        //if (clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Hour != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Hour", clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Hour);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Hour", DBNull.Value);
        //}
        //if (clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Minute != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Minute", clsdbo_OrderAndDeliveryCycleValue.Order_Cycle_Minute);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Minute", DBNull.Value);
        //}
        //if (clsdbo_OrderAndDeliveryCycleValue.Delivery_Cycle_Date != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldDelivery_Cycle_Date", clsdbo_OrderAndDeliveryCycleValue.Delivery_Cycle_Date);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldDelivery_Cycle_Date", DBNull.Value);
        //}
        //if (clsdbo_OrderAndDeliveryCycleValue.Route != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldRoute", clsdbo_OrderAndDeliveryCycleValue.Route);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldRoute", DBNull.Value);
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

