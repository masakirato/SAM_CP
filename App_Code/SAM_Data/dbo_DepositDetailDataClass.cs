using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
public class dbo_DepositDetailDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[DepositDetailSelectAll]";
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

    public static List<dbo_DepositDetailClass> Search(String Clearing_No, String Month)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[DepositDetailSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        List<dbo_DepositDetailClass> item = new List<dbo_DepositDetailClass>();

        if (!string.IsNullOrEmpty(Clearing_No))
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Month))
        {
            selectCommand.Parameters.AddWithValue("@Month", Month);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Month", DBNull.Value);
        }


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
                    item.Add(new dbo_DepositDetailClass()
                    {
                        Deposit_Detail_ID = reader["Deposit_Detail_ID"] is DBNull ? null : reader["Deposit_Detail_ID"].ToString(),
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                        Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"],
                        Total_Qty = reader["Total_Qty"] is DBNull ? null : (Int16?)reader["Total_Qty"],
                        Deposit_Qty = reader["Deposit_Qty"] is DBNull ? 0 : (Int16?)reader["Deposit_Qty"],
                        // Deposit_Return_flag = reader["Deposit_Return_flag"] is DBNull ? null : (Boolean?)reader["Deposit_Return_flag"],
                        Selling_Price = reader["Selling_Price"] is DBNull ? null : (Decimal?)reader["Selling_Price"],
                        Sales_Qty = reader["Sales_Qty"] is DBNull ? null : (Int16?)reader["Sales_Qty"],
                        //Sales_Amount = reader["Sales_Amount"] is DBNull ? null : (Decimal?)reader["Sales_Amount"],
                        Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"],
                        Point = reader["Point"] is DBNull ? null : (Int16?)reader["Point"],
                        // Product_List_ID = reader["Product_List_ID"] is DBNull ? null : reader["Product_List_ID"].ToString(),
                        Return_Qty = reader["Return_Qty"] is DBNull ? 0 : (Int16?)reader["Return_Qty"],
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

    public static dbo_DepositDetailClass Select_Record(dbo_DepositDetailClass clsdbo_DepositDetailPara)
    {
        dbo_DepositDetailClass clsdbo_DepositDetail = new dbo_DepositDetailClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[DepositDetailSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Deposit_Detail_ID", clsdbo_DepositDetailPara.Deposit_Detail_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_DepositDetail.Deposit_Detail_ID = reader["Deposit_Detail_ID"] is DBNull ? null : reader["Deposit_Detail_ID"].ToString();
                clsdbo_DepositDetail.Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString();
                clsdbo_DepositDetail.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_DepositDetail.Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"];
                clsdbo_DepositDetail.Total_Qty = reader["Total_Qty"] is DBNull ? null : (Int16?)reader["Total_Qty"];
                clsdbo_DepositDetail.Deposit_Qty = reader["Deposit_Qty"] is DBNull ? null : (Int16?)reader["Deposit_Qty"];
                clsdbo_DepositDetail.Deposit_Return_flag = reader["Deposit_Return_flag"] is DBNull ? null : (Boolean?)reader["Deposit_Return_flag"];
                clsdbo_DepositDetail.Selling_Price = reader["Selling_Price"] is DBNull ? null : (Decimal?)reader["Selling_Price"];
                clsdbo_DepositDetail.Sales_Qty = reader["Sales_Qty"] is DBNull ? null : (Int16?)reader["Sales_Qty"];
                clsdbo_DepositDetail.Sales_Amount = reader["Sales_Amount"] is DBNull ? null : (Decimal?)reader["Sales_Amount"];
                clsdbo_DepositDetail.Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"];
                clsdbo_DepositDetail.Point = reader["Point"] is DBNull ? null : (Byte?)reader["Point"];
                clsdbo_DepositDetail.Product_List_ID = reader["Product_List_ID"] is DBNull ? null : reader["Product_List_ID"].ToString();
                clsdbo_DepositDetail.Return_Qty = reader["Return_Qty"] is DBNull ? null : (Int16?)reader["Return_Qty"];
            }
            else
            {
                clsdbo_DepositDetail = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_DepositDetail;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_DepositDetail;
    }

    public static bool Add(dbo_DepositDetailClass clsdbo_DepositDetail)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[DepositDetailInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_DepositDetail.Deposit_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Deposit_Detail_ID", clsdbo_DepositDetail.Deposit_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Deposit_Detail_ID", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_DepositDetail.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_DepositDetail.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Price", clsdbo_DepositDetail.Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Total_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Qty", clsdbo_DepositDetail.Total_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Qty", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Deposit_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Deposit_Qty", clsdbo_DepositDetail.Deposit_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Deposit_Qty", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Deposit_Return_flag.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Deposit_Return_flag", clsdbo_DepositDetail.Deposit_Return_flag);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Deposit_Return_flag", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Selling_Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Selling_Price", clsdbo_DepositDetail.Selling_Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Selling_Price", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Sales_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Sales_Qty", clsdbo_DepositDetail.Sales_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sales_Qty", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Sales_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Sales_Amount", clsdbo_DepositDetail.Sales_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sales_Amount", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Commission.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Commission", clsdbo_DepositDetail.Commission);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Commission", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Point.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Point", clsdbo_DepositDetail.Point);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Point", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Product_List_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_List_ID", clsdbo_DepositDetail.Product_List_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_List_ID", DBNull.Value);
        }
        if (clsdbo_DepositDetail.Return_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Return_Qty", clsdbo_DepositDetail.Return_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Return_Qty", DBNull.Value);
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

    public static bool Update(dbo_DepositDetailClass newdbo_DepositDetailClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[DepositDetailUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_DepositDetailClass.Deposit_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDeposit_Detail_ID", newdbo_DepositDetailClass.Deposit_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDeposit_Detail_ID", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", newdbo_DepositDetailClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_DepositDetailClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", newdbo_DepositDetailClass.Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Total_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Qty", newdbo_DepositDetailClass.Total_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Qty", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Deposit_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDeposit_Qty", newdbo_DepositDetailClass.Deposit_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDeposit_Qty", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Deposit_Return_flag.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDeposit_Return_flag", newdbo_DepositDetailClass.Deposit_Return_flag);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDeposit_Return_flag", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Selling_Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSelling_Price", newdbo_DepositDetailClass.Selling_Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSelling_Price", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Sales_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSales_Qty", newdbo_DepositDetailClass.Sales_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSales_Qty", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Sales_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSales_Amount", newdbo_DepositDetailClass.Sales_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSales_Amount", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCommission", newdbo_DepositDetailClass.Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCommission", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Point.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", newdbo_DepositDetailClass.Point);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Product_List_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_List_ID", newdbo_DepositDetailClass.Product_List_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_List_ID", DBNull.Value);
        }
        if (newdbo_DepositDetailClass.Return_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewReturn_Qty", newdbo_DepositDetailClass.Return_Qty);
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

    public static bool Delete(String Clearing_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[DepositDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Clearing_No))
        {
            deleteCommand.Parameters.AddWithValue("@OldClearing_No", Clearing_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldClearing_No", DBNull.Value);
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

