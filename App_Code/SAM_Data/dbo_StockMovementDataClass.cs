using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;


public class dbo_StockMovementDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[StockMovementSelectAll]";
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
        catch (SqlException)
        {
            return dt;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }


    public static DataTable Search(String Stock_Movement_ID, String CV_CODE, DateTime? Date, String Product_List_ID, String Movement_Type)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[StockMovementSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Stock_Movement_ID))
        {
            selectCommand.Parameters.AddWithValue("@Stock_Movement_ID", Stock_Movement_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Stock_Movement_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_CODE))
        {
            selectCommand.Parameters.AddWithValue("@CV_CODE", CV_CODE);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
        }
        if (Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Date", Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Date", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Product_List_ID))
        {
            selectCommand.Parameters.AddWithValue("@Product_List_ID", Product_List_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_List_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Movement_Type))
        {
            selectCommand.Parameters.AddWithValue("@Movement_Type", Movement_Type);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Movement_Type", DBNull.Value);
        }

        List<dbo_StockMovementClass> item = new List<dbo_StockMovementClass>();
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
                    item.Add(new dbo_StockMovementClass()
                    {
                        Stock_Movement_ID = System.Convert.ToString(reader["Stock_Movement_ID"]),
                        CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString(),
                        Date = reader["Date"] is DBNull ? null : (DateTime?)reader["Date"],
                        Product_List_ID = reader["Product_List_ID"] is DBNull ? null : reader["Product_List_ID"].ToString(),
                        Movement_Type = reader["Movement_Type"] is DBNull ? null : reader["Movement_Type"].ToString(),
                        //Qty = reader["Qty"] is DBNull ? null : (Byte?)reader["Qty"],
                        Qty = (reader["Qty"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Qty"].ToString())),

                });

                }
            }
            reader1.Close();
        }
        catch (SqlException)
        {
            return dt;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static dbo_StockMovementClass Select_Record(String Stock_Movement_ID)
    {
        dbo_StockMovementClass clsdbo_StockMovement = new dbo_StockMovementClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[StockMovementSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Stock_Movement_ID", Stock_Movement_ID);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_StockMovement.Stock_Movement_ID = System.Convert.ToString(reader["Stock_Movement_ID"]);
                clsdbo_StockMovement.CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString();
                clsdbo_StockMovement.Date = reader["Date"] is DBNull ? null : (DateTime?)reader["Date"];
                clsdbo_StockMovement.Product_List_ID = reader["Product_List_ID"] is DBNull ? null : reader["Product_List_ID"].ToString();
                clsdbo_StockMovement.Movement_Type = reader["Movement_Type"] is DBNull ? null : reader["Movement_Type"].ToString();
                //clsdbo_StockMovement.Qty = reader["Qty"] is DBNull ? null : (Byte?)reader["Qty"];
                clsdbo_StockMovement.Qty = (reader["Qty"] is DBNull ? Int16.Parse("0") : Int16.Parse(reader["Qty"].ToString()));
                /*
                 clsdbo_StockMovement.Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"];
                 clsdbo_StockMovement.Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString();
                 clsdbo_StockMovement.Last_Modified_Date = reader["Last_Modified_Date"] is DBNull ? null : (DateTime?)reader["Last_Modified_Date"];
                 clsdbo_StockMovement.Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString();
             */
            }
            else
            {
                clsdbo_StockMovement = null;
            }
            reader.Close();
        }
        catch (SqlException)
        {
            return clsdbo_StockMovement;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_StockMovement;
    }

    public static bool Add(dbo_StockMovementClass clsdbo_StockMovement)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[StockMovementInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;

        //insertCommand.Parameters.AddWithValue("@Stock_Movement_ID", clsdbo_StockMovement.Stock_Movement_ID);



        if (clsdbo_StockMovement.CV_CODE != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_CODE", clsdbo_StockMovement.CV_CODE);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
        }
        if (clsdbo_StockMovement.Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Date", clsdbo_StockMovement.Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Date", DBNull.Value);
        }

        if (clsdbo_StockMovement.Product_List_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_List_ID", clsdbo_StockMovement.Product_List_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_List_ID", DBNull.Value);
        }



        switch (clsdbo_StockMovement.Movement_Type)
        {
            case "รับสินค้า":
                insertCommand.Parameters.AddWithValue("@Movement_Type", "1");
                break;
            case "เบิกSP":
                insertCommand.Parameters.AddWithValue("@Movement_Type", "2");
                break;
            case "เบิกอื่นๆ":
                insertCommand.Parameters.AddWithValue("@Movement_Type", "3");
                break;
                //เผื่ออนาคตมีการปรับ Report stock_movement
            //case "คืนสินค้า":
            //    insertCommand.Parameters.AddWithValue("@Movement_Type", "4");
            //    break;

        }




        //if (clsdbo_StockMovement.Movement_Type != null)
        //{
        //  insertCommand.Parameters.AddWithValue("@Movement_Type", clsdbo_StockMovement.Movement_Type);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@Movement_Type", DBNull.Value);
        //}







        if (clsdbo_StockMovement.Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Qty", clsdbo_StockMovement.Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Qty", DBNull.Value);
        }

        /*
        if (clsdbo_StockMovement.Created_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Created_Date", clsdbo_StockMovement.Created_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_Date", DBNull.Value);
        }
        if (clsdbo_StockMovement.Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", clsdbo_StockMovement.Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }
        if (clsdbo_StockMovement.Last_Modified_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_Date", clsdbo_StockMovement.Last_Modified_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_Date", DBNull.Value);
        }
        if (clsdbo_StockMovement.Last_Modified_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_By", clsdbo_StockMovement.Last_Modified_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
        }
         */


        if (clsdbo_StockMovement.Ref_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Ref_No", clsdbo_StockMovement.Ref_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Ref_No", DBNull.Value);
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

    public static bool Update(
           dbo_StockMovementClass newdbo_StockMovementClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[StockMovementUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        updateCommand.Parameters.AddWithValue("@NewStock_Movement_ID", newdbo_StockMovementClass.Stock_Movement_ID);
        if (newdbo_StockMovementClass.CV_CODE != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_CODE", newdbo_StockMovementClass.CV_CODE);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_CODE", DBNull.Value);
        }
        if (newdbo_StockMovementClass.Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDate", newdbo_StockMovementClass.Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDate", DBNull.Value);
        }
        if (newdbo_StockMovementClass.Product_List_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_List_ID", newdbo_StockMovementClass.Product_List_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_List_ID", DBNull.Value);
        }
        if (newdbo_StockMovementClass.Movement_Type != null)
        {
            updateCommand.Parameters.AddWithValue("@NewMovement_Type", newdbo_StockMovementClass.Movement_Type);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewMovement_Type", DBNull.Value);
        }
        if (newdbo_StockMovementClass.Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewQty", newdbo_StockMovementClass.Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewQty", DBNull.Value);
        }
        /*
        if (newdbo_StockMovementClass.Created_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_Date", newdbo_StockMovementClass.Created_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_Date", DBNull.Value);
        }
        if (newdbo_StockMovementClass.Created_By != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_By", newdbo_StockMovementClass.Created_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_By", DBNull.Value);
        }
        if (newdbo_StockMovementClass.Last_Modified_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_Date", newdbo_StockMovementClass.Last_Modified_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_Date", DBNull.Value);
        }
        if (newdbo_StockMovementClass.Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_By", newdbo_StockMovementClass.Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_By", DBNull.Value);
        }

        */

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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Delete(String Stock_Movement_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[StockMovementDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        deleteCommand.Parameters.AddWithValue("@OldStock_Movement_ID", Stock_Movement_ID);


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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool DeleteByRefNo(String Ref_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[StockMovementDeleteByRefNo]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        deleteCommand.Parameters.AddWithValue("@OldRef_No", Ref_No);


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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }
}

