using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
public class dbo_OrderAndDeliveryCycleDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static List<NextDay> GetDelivery_Cycle_Date()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetDelivery_Cycle_Date";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        List<NextDay> item = new List<NextDay>();
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
                    item.Add(new NextDay()
                    {
                        Order_Cycle_Date = reader["Order_Cycle_Date"] is DBNull ? null : reader["Order_Cycle_Date"].ToString()
                        ,
                        Delivery_Cycle_Date = reader["Delivery_Cycle_Date"] is DBNull ? null : reader["Delivery_Cycle_Date"].ToString()
                        ,
                        nextday = reader["nextday"] is DBNull ? 0 : int.Parse(reader["nextday"].ToString())
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


    public static List<dbo_OrderAndDeliveryCycleClass> SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[OrderAndDeliveryCycleSelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();


        List<dbo_OrderAndDeliveryCycleClass> item = new List<dbo_OrderAndDeliveryCycleClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);


                foreach (DataRow reader in dt.Rows)
                {

                    item.Add(new dbo_OrderAndDeliveryCycleClass()
                    {
                        Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString(),
                        Order_Cycle_Name = reader["Order_Cycle_Name"] is DBNull ? null : reader["Order_Cycle_Name"].ToString()
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

    public static List<dbo_OrderAndDeliveryCycleClass> Search(String Order_Cycle_Name)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[OrderAndDeliveryCycleSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        //if (sField == "Order Cycle I D")
        //{
        //    selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", sValue);
        //}
        //else
        //{
        //    selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", DBNull.Value);
        //}



        if (Order_Cycle_Name != null)
        {
            selectCommand.Parameters.AddWithValue("@Order_Cycle_Name", Order_Cycle_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Order_Cycle_Name", DBNull.Value);
        }


        List<dbo_OrderAndDeliveryCycleClass> item = new List<dbo_OrderAndDeliveryCycleClass>();
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
                    item.Add(new dbo_OrderAndDeliveryCycleClass() 
                    {
                    
                      Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString(),
                      Order_Cycle_Name = reader["Order_Cycle_Name"] is DBNull ? null : reader["Order_Cycle_Name"].ToString()

                    
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

    public static dbo_OrderAndDeliveryCycleClass Select_Record(String Order_Cycle_ID)
    {
        dbo_OrderAndDeliveryCycleClass clsdbo_OrderAndDeliveryCycle = new dbo_OrderAndDeliveryCycleClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[OrderAndDeliveryCycleSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", Order_Cycle_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString();
                clsdbo_OrderAndDeliveryCycle.Order_Cycle_Name = reader["Order_Cycle_Name"] is DBNull ? null : reader["Order_Cycle_Name"].ToString();
            }
            else
            {
                clsdbo_OrderAndDeliveryCycle = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_OrderAndDeliveryCycle;
    }

    public static bool Add(dbo_OrderAndDeliveryCycleClass clsdbo_OrderAndDeliveryCycle, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "OrderAndDeliveryCycleInsert";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_ID", clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_ID", DBNull.Value);
        }
        if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_Name", clsdbo_OrderAndDeliveryCycle.Order_Cycle_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Order_Cycle_Name", DBNull.Value);
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

    public static bool Update(dbo_OrderAndDeliveryCycleClass newdbo_OrderAndDeliveryCycleClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "OrderAndDeliveryCycleUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;


        if (newdbo_OrderAndDeliveryCycleClass.Order_Cycle_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_ID", newdbo_OrderAndDeliveryCycleClass.Order_Cycle_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_ID", DBNull.Value);
        }

        if (newdbo_OrderAndDeliveryCycleClass.Order_Cycle_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Name", newdbo_OrderAndDeliveryCycleClass.Order_Cycle_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Name", DBNull.Value);
        }

        if (newdbo_OrderAndDeliveryCycleClass.Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", newdbo_OrderAndDeliveryCycleClass.Last_Modified_By);
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

    public static bool Delete(dbo_OrderAndDeliveryCycleClass clsdbo_OrderAndDeliveryCycle)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[OrderAndDeliveryCycleDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_ID", clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_ID", DBNull.Value);
        }

        //if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_Name != null)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Name", clsdbo_OrderAndDeliveryCycle.Order_Cycle_Name);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Name", DBNull.Value);
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









    //public static DataTable SelectAll()
    //{
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string selectProcedure = "[dbo].[OrderAndDeliveryCycleSelectAll]";
    //    SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
    //    selectCommand.CommandType = CommandType.StoredProcedure;
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        connection.Open();
    //        SqlDataReader reader = selectCommand.ExecuteReader();
    //        if (reader.HasRows)
    //        {
    //            dt.Load(reader);
    //        }
    //        reader.Close();
    //    }
    //    catch (SqlException ex)
    //    {
    //        return dt;
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //    return dt;
    //}

    //public static List<dbo_OrderAndDeliveryCycleClass> Search(String CV_Code)
    //{
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string selectProcedure = "OrderAndDeliveryCycleSearch";
    //    SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
    //    selectCommand.CommandType = CommandType.StoredProcedure;

    //    if (!String.IsNullOrEmpty(CV_Code))
    //    {
    //        selectCommand.Parameters.AddWithValue("@CV_CODE", CV_Code);
    //    }
    //    else
    //    {
    //        selectCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
    //    }


    //    List<dbo_OrderAndDeliveryCycleClass> item = new List<dbo_OrderAndDeliveryCycleClass>();
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
    //                item.Add(new dbo_OrderAndDeliveryCycleClass()
    //                {
    //                    Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString()
    //                ,
    //                    CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString()
    //                ,
    //                    Order_Cycle_Date = reader["Order_Cycle_Date"] is DBNull ? null : reader["Order_Cycle_Date"].ToString()
    //                ,
    //                    Order_Cycle_Hour = reader["Order_Cycle_Hour"] is DBNull ? null : reader["Order_Cycle_Hour"].ToString()
    //                ,
    //                    Order_Cycle_Minute = reader["Order_Cycle_Minute"] is DBNull ? null : reader["Order_Cycle_Minute"].ToString()
    //                ,
    //                    Delivery_Cycle_Date = reader["Delivery_Cycle_Date"] is DBNull ? null : reader["Delivery_Cycle_Date"].ToString()
    //                ,
    //                    Route = reader["Route"] is DBNull ? null : reader["Route"].ToString()

    //                });
    //            }


    //        }
    //        reader1.Close();
    //    }
    //    catch (SqlException ex)
    //    {
    //        return item;
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //    return item;
    //}

    //public static dbo_OrderAndDeliveryCycleClass Select_Record(dbo_OrderAndDeliveryCycleClass clsdbo_OrderAndDeliveryCyclePara)
    //{
    //    dbo_OrderAndDeliveryCycleClass clsdbo_OrderAndDeliveryCycle = new dbo_OrderAndDeliveryCycleClass();
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string selectProcedure = "[dbo].[OrderAndDeliveryCycleSelect]";
    //    SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
    //    selectCommand.CommandType = CommandType.StoredProcedure;
    //    selectCommand.Parameters.AddWithValue("@Order_Cycle_ID", clsdbo_OrderAndDeliveryCyclePara.Order_Cycle_ID);
    //    try
    //    {
    //        connection.Open();
    //        SqlDataReader reader
    //            = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
    //        if (reader.Read())
    //        {
    //            clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID = reader["Order_Cycle_ID"] is DBNull ? null : reader["Order_Cycle_ID"].ToString();
    //            clsdbo_OrderAndDeliveryCycle.CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString();
    //            clsdbo_OrderAndDeliveryCycle.Order_Cycle_Date = reader["Order_Cycle_Date"] is DBNull ? null : reader["Order_Cycle_Date"].ToString();
    //            clsdbo_OrderAndDeliveryCycle.Order_Cycle_Hour = reader["Order_Cycle_Hour"] is DBNull ? null : reader["Order_Cycle_Hour"].ToString();
    //            clsdbo_OrderAndDeliveryCycle.Order_Cycle_Minute = reader["Order_Cycle_Minute"] is DBNull ? null : reader["Order_Cycle_Minute"].ToString();
    //            clsdbo_OrderAndDeliveryCycle.Delivery_Cycle_Date = reader["Delivery_Cycle_Date"] is DBNull ? null : reader["Delivery_Cycle_Date"].ToString();
    //            clsdbo_OrderAndDeliveryCycle.Route = reader["Route"] is DBNull ? null : reader["Route"].ToString();
    //        }
    //        else
    //        {
    //            clsdbo_OrderAndDeliveryCycle = null;
    //        }
    //        reader.Close();
    //    }
    //    catch (SqlException ex)
    //    {
    //        return clsdbo_OrderAndDeliveryCycle;
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //    return clsdbo_OrderAndDeliveryCycle;
    //}

    //public static bool Add(dbo_OrderAndDeliveryCycleClass clsdbo_OrderAndDeliveryCycle)
    //{
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string insertProcedure = "OrderAndDeliveryCycleInsert";
    //    SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
    //    insertCommand.CommandType = CommandType.StoredProcedure;
    //    if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID != null)
    //    {
    //        insertCommand.Parameters.AddWithValue("@Order_Cycle_ID", clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID);
    //    }
    //    else
    //    {
    //        insertCommand.Parameters.AddWithValue("@Order_Cycle_ID", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.CV_CODE != null)
    //    {
    //        insertCommand.Parameters.AddWithValue("@CV_CODE", clsdbo_OrderAndDeliveryCycle.CV_CODE);
    //    }
    //    else
    //    {
    //        insertCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_Date != null)
    //    {
    //        insertCommand.Parameters.AddWithValue("@Order_Cycle_Date", clsdbo_OrderAndDeliveryCycle.Order_Cycle_Date);
    //    }
    //    else
    //    {
    //        insertCommand.Parameters.AddWithValue("@Order_Cycle_Date", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_Hour != null)
    //    {
    //        insertCommand.Parameters.AddWithValue("@Order_Cycle_Hour", clsdbo_OrderAndDeliveryCycle.Order_Cycle_Hour);
    //    }
    //    else
    //    {
    //        insertCommand.Parameters.AddWithValue("@Order_Cycle_Hour", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_Minute != null)
    //    {
    //        insertCommand.Parameters.AddWithValue("@Order_Cycle_Minute", clsdbo_OrderAndDeliveryCycle.Order_Cycle_Minute);
    //    }
    //    else
    //    {
    //        insertCommand.Parameters.AddWithValue("@Order_Cycle_Minute", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Delivery_Cycle_Date != null)
    //    {
    //        insertCommand.Parameters.AddWithValue("@Delivery_Cycle_Date", clsdbo_OrderAndDeliveryCycle.Delivery_Cycle_Date);
    //    }
    //    else
    //    {
    //        insertCommand.Parameters.AddWithValue("@Delivery_Cycle_Date", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Route != null)
    //    {
    //        insertCommand.Parameters.AddWithValue("@Route", clsdbo_OrderAndDeliveryCycle.Route);
    //    }
    //    else
    //    {
    //        insertCommand.Parameters.AddWithValue("@Route", DBNull.Value);
    //    }
    //    insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
    //    insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
    //    try
    //    {
    //        connection.Open();
    //        insertCommand.ExecuteNonQuery();
    //        int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
    //        if (count > 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    catch (SqlException ex)
    //    {
    //        return false;
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //}

    //public static bool Update(dbo_OrderAndDeliveryCycleClass olddbo_OrderAndDeliveryCycleClass,
    //       dbo_OrderAndDeliveryCycleClass newdbo_OrderAndDeliveryCycleClass)
    //{
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string updateProcedure = "[dbo].[OrderAndDeliveryCycleUpdate]";
    //    SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
    //    updateCommand.CommandType = CommandType.StoredProcedure;
    //    if (newdbo_OrderAndDeliveryCycleClass.Order_Cycle_ID != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_ID", newdbo_OrderAndDeliveryCycleClass.Order_Cycle_ID);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_ID", DBNull.Value);
    //    }
    //    if (newdbo_OrderAndDeliveryCycleClass.CV_CODE != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewCV_CODE", newdbo_OrderAndDeliveryCycleClass.CV_CODE);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewCV_CODE", DBNull.Value);
    //    }
    //    if (newdbo_OrderAndDeliveryCycleClass.Order_Cycle_Date != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Date", newdbo_OrderAndDeliveryCycleClass.Order_Cycle_Date);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Date", DBNull.Value);
    //    }
    //    if (newdbo_OrderAndDeliveryCycleClass.Order_Cycle_Hour != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Hour", newdbo_OrderAndDeliveryCycleClass.Order_Cycle_Hour);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Hour", DBNull.Value);
    //    }
    //    if (newdbo_OrderAndDeliveryCycleClass.Order_Cycle_Minute != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Minute", newdbo_OrderAndDeliveryCycleClass.Order_Cycle_Minute);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewOrder_Cycle_Minute", DBNull.Value);
    //    }
    //    if (newdbo_OrderAndDeliveryCycleClass.Delivery_Cycle_Date != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewDelivery_Cycle_Date", newdbo_OrderAndDeliveryCycleClass.Delivery_Cycle_Date);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewDelivery_Cycle_Date", DBNull.Value);
    //    }
    //    if (newdbo_OrderAndDeliveryCycleClass.Route != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewRoute", newdbo_OrderAndDeliveryCycleClass.Route);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@NewRoute", DBNull.Value);
    //    }
    //    if (olddbo_OrderAndDeliveryCycleClass.Order_Cycle_ID != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldOrder_Cycle_ID", olddbo_OrderAndDeliveryCycleClass.Order_Cycle_ID);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldOrder_Cycle_ID", DBNull.Value);
    //    }
    //    if (olddbo_OrderAndDeliveryCycleClass.CV_CODE != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldCV_CODE", olddbo_OrderAndDeliveryCycleClass.CV_CODE);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldCV_CODE", DBNull.Value);
    //    }
    //    if (olddbo_OrderAndDeliveryCycleClass.Order_Cycle_Date != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldOrder_Cycle_Date", olddbo_OrderAndDeliveryCycleClass.Order_Cycle_Date);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldOrder_Cycle_Date", DBNull.Value);
    //    }
    //    if (olddbo_OrderAndDeliveryCycleClass.Order_Cycle_Hour != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldOrder_Cycle_Hour", olddbo_OrderAndDeliveryCycleClass.Order_Cycle_Hour);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldOrder_Cycle_Hour", DBNull.Value);
    //    }
    //    if (olddbo_OrderAndDeliveryCycleClass.Order_Cycle_Minute != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldOrder_Cycle_Minute", olddbo_OrderAndDeliveryCycleClass.Order_Cycle_Minute);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldOrder_Cycle_Minute", DBNull.Value);
    //    }
    //    if (olddbo_OrderAndDeliveryCycleClass.Delivery_Cycle_Date != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldDelivery_Cycle_Date", olddbo_OrderAndDeliveryCycleClass.Delivery_Cycle_Date);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldDelivery_Cycle_Date", DBNull.Value);
    //    }
    //    if (olddbo_OrderAndDeliveryCycleClass.Route != null)
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldRoute", olddbo_OrderAndDeliveryCycleClass.Route);
    //    }
    //    else
    //    {
    //        updateCommand.Parameters.AddWithValue("@OldRoute", DBNull.Value);
    //    }
    //    updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
    //    updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
    //    try
    //    {
    //        connection.Open();
    //        updateCommand.ExecuteNonQuery();
    //        int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
    //        if (count > 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    catch (SqlException ex)
    //    {
    //        return false;
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //}

    //public static bool Delete(dbo_OrderAndDeliveryCycleClass clsdbo_OrderAndDeliveryCycle)
    //{
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string deleteProcedure = "[dbo].[OrderAndDeliveryCycleDelete]";
    //    SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
    //    deleteCommand.CommandType = CommandType.StoredProcedure;
    //    if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_ID", clsdbo_OrderAndDeliveryCycle.Order_Cycle_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_ID", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.CV_CODE != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCV_CODE", clsdbo_OrderAndDeliveryCycle.CV_CODE);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCV_CODE", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_Date != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Date", clsdbo_OrderAndDeliveryCycle.Order_Cycle_Date);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Date", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_Hour != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Hour", clsdbo_OrderAndDeliveryCycle.Order_Cycle_Hour);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Hour", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Order_Cycle_Minute != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Minute", clsdbo_OrderAndDeliveryCycle.Order_Cycle_Minute);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldOrder_Cycle_Minute", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Delivery_Cycle_Date != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldDelivery_Cycle_Date", clsdbo_OrderAndDeliveryCycle.Delivery_Cycle_Date);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldDelivery_Cycle_Date", DBNull.Value);
    //    }
    //    if (clsdbo_OrderAndDeliveryCycle.Route != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldRoute", clsdbo_OrderAndDeliveryCycle.Route);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldRoute", DBNull.Value);
    //    }
    //    deleteCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
    //    deleteCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
    //    try
    //    {
    //        connection.Open();
    //        deleteCommand.ExecuteNonQuery();
    //        int count = System.Convert.ToInt32(deleteCommand.Parameters["@ReturnValue"].Value);
    //        if (count > 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    catch (SqlException ex)
    //    {
    //        return false;
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //}





}

