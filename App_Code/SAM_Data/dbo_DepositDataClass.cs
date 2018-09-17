using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
public class dbo_DepositDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[DepositSelectAll]";
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

    public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[DepositSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Clearing No")
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (sField == "Net Sales Qty")
        {
            selectCommand.Parameters.AddWithValue("@Net_Sales_Qty", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Net_Sales_Qty", DBNull.Value);
        }
        if (sField == "Net Sales Amount")
        {
            selectCommand.Parameters.AddWithValue("@Net_Sales_Amount", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Net_Sales_Amount", DBNull.Value);
        }
        if (sField == "Total Commission")
        {
            selectCommand.Parameters.AddWithValue("@Total_Commission", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Total_Commission", DBNull.Value);
        }
        if (sField == "Tota Point")
        {
            selectCommand.Parameters.AddWithValue("@Tota_Point", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Tota_Point", DBNull.Value);
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

    public static dbo_DepositClass Select_Record(String Clearing_No)
    {
        dbo_DepositClass clsdbo_Deposit = new dbo_DepositClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[DepositSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Deposit.Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString();
                clsdbo_Deposit.Net_Sales_Qty = reader["Net_Sales_Qty"] is DBNull ? null : (Int16?)reader["Net_Sales_Qty"];
                clsdbo_Deposit.Net_Sales_Amount = reader["Net_Sales_Amount"] is DBNull ? null : (Decimal?)reader["Net_Sales_Amount"];
                clsdbo_Deposit.Total_Commission = reader["Total_Commission"] is DBNull ? null : (Decimal?)reader["Total_Commission"];
                clsdbo_Deposit.Tota_Point = reader["Tota_Point"] is DBNull ? null : (Int16?)reader["Tota_Point"];
            }
            else
            {
                clsdbo_Deposit = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Deposit;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Deposit;
    }

    public static dbo_DepositClass SumNetSalesBySPID(String User_ID)
    {
        dbo_DepositClass clsdbo_Deposit = new dbo_DepositClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[DepositSumNetSalesBySP]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Deposit.Net_Sales_Amount = reader["Net_Sales_Amount"] is DBNull ? null : (Decimal?)reader["Net_Sales_Amount"];
            }
            else
            {
                clsdbo_Deposit = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Deposit;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Deposit;
    }

    public static bool Add(dbo_DepositClass clsdbo_Deposit)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[DepositInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Deposit.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_Deposit.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (clsdbo_Deposit.Net_Sales_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Net_Sales_Qty", clsdbo_Deposit.Net_Sales_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Net_Sales_Qty", DBNull.Value);
        }
        if (clsdbo_Deposit.Net_Sales_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Net_Sales_Amount", clsdbo_Deposit.Net_Sales_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Net_Sales_Amount", DBNull.Value);
        }
        if (clsdbo_Deposit.Total_Commission.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Commission", clsdbo_Deposit.Total_Commission);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Commission", DBNull.Value);
        }
        if (clsdbo_Deposit.Tota_Point.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Tota_Point", clsdbo_Deposit.Tota_Point);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Tota_Point", DBNull.Value);
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

    public static bool Update(dbo_DepositClass newdbo_DepositClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[DepositUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_DepositClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", newdbo_DepositClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", DBNull.Value);
        }
        if (newdbo_DepositClass.Net_Sales_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Sales_Qty", newdbo_DepositClass.Net_Sales_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Sales_Qty", DBNull.Value);
        }
        if (newdbo_DepositClass.Net_Sales_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Sales_Amount", newdbo_DepositClass.Net_Sales_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewNet_Sales_Amount", DBNull.Value);
        }
        if (newdbo_DepositClass.Total_Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Commission", newdbo_DepositClass.Total_Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Commission", DBNull.Value);
        }
        if (newdbo_DepositClass.Tota_Point.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTota_Point", newdbo_DepositClass.Tota_Point);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTota_Point", DBNull.Value);
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
        string deleteProcedure = "[DepositDelete]";
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

