using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CheckUsername
/// </summary>
public class CheckUsername
{
	public static String Check_Username(string Username , string User_ID)
	{
		//
		// TODO: Add constructor logic here
		//        string now = GetNow();
        string cnt = string.Empty;

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "CheckUsername";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Username", Username);
        selectCommand.Parameters.AddWithValue("@User_ID", User_ID);

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                cnt = reader["Cnt"] is DBNull ? "0" : reader["Cnt"].ToString();

            }
            else
            {
                cnt = "0";
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        return cnt;
    }


    public static String Check_UserID(string User_ID)
    {
        //
        // TODO: Add constructor logic here
        //        string now = GetNow();
        string cnt = string.Empty;

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "CheckUserID";
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
                cnt = reader["Cnt"] is DBNull ? "0" : reader["Cnt"].ToString();

            }
            else
            {
                cnt = "0";
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        return cnt;
    }
    public static String Check_CustomerID(string Customer_ID)
    {
        //
        // TODO: Add constructor logic here
        //        string now = GetNow();
        string cnt = string.Empty;

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "CheckCustomerID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Customer_ID", Customer_ID);

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                cnt = reader["Cnt"] is DBNull ? "0" : reader["Cnt"].ToString();

            }
            else
            {
                cnt = "0";
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        return cnt;
    }
}