
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public class GenerateID
{

    public static String PurchaseOrderNumber(string CV_Code)
    {
        string now = GetNow();


        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);

        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "PO_Number");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "PO" + year + month + index.ToString("000"); ;

        return _id;

        //  return "212689PO600601";
    }

    public static String Requisition_No(string CV_Code)
    {
        string now = GetNow();


        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);

        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Requisition_No");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        //int index = 0;

        //if (int.Parse(id) == 0)
        //{
        //    index = int.Parse(id) + 1;
        //}
        //else
        //{
        //    index = int.Parse(id);
        //}
        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        _id = CV_Code + "GT" + year + month + date + index.ToString("00"); ;

        return _id;

        //  return "212689PO600601";
    }

    public static String Other_Requisition_No(string CV_Code)
    {
        string now = GetNow();


        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);

        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Other_Requisition_No");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "GT" + year + month + date + index.ToString("00"); ;

        return _id;

        //  return "212689PO600601";
    }

    public static String Billing_ID(string CV_Code)
    {

        string now = GetNow();


        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);

        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Billing_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "INV" + year + month + index.ToString("000"); ;





        return _id;




    }

    public static String Receive_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Receive_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "RCV" + year + month + index.ToString("000"); ;


        return _id;
    }

    public static String Stock_on_Hand_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Stock_on_Hand_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + year + month + index.ToString("0000"); ;


        return _id;
    }

    public static String UserID_CP()
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "User_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", string.Empty);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = index.ToString("00000000"); ;


        return _id;
    }

    public static String UserID_Agent(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "User_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + index.ToString("0000"); ;


        return _id;
    }

    private static String GetNow()
    {
        //DateTime obj = DateTime.Parse(DateTime.Now.ToString(), CultureInfo.GetCultureInfo("en-US").DateTimeFormat);//en-US
        //string s = obj.ToString(CultureInfo.GetCultureInfo("th-TH").DateTimeFormat);

        string s = DateTime.Now.ToString();


        string now = DateTime.Parse(s).ToString("yyMMdd");
        return now;
    }

    public static String Order_Cycle_ID()
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);

        //string year = now.Substring(0, 2);
        //string date = now.Substring(2, 2);
        //string month = now.Substring(4, 2);

        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Order_Cycle_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", "");

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }


        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = "CO" + year + month + date + index.ToString("0000"); ;

        return _id;

    }

    public static String Price_Group_ID()
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);

        //string year = now.Substring(0, 2);
        //string date = now.Substring(2, 2);
        //string month = now.Substring(4, 2);

        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Price_Group_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", "");

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }


        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = "PG" + year + month + date + index.ToString("0000"); 

        return _id;

    }

    public static String Clearing_No(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Clearing_No");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "CT" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String Credit_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Credit_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "CR" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String Customer_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Customer_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + index.ToString("0000"); ;


        return _id;
    }

    public static String Installation_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Installation_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        //_id = CV_Code + index.ToString("000000"); 
        _id = "IN" + year + month + date + index.ToString("0000"); 

        return _id;

    }

    public static String Benefit_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Benefit_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        //_id = CV_Code + index.ToString("000000"); ;
        _id = "BE" + year + month + date + index.ToString("0000"); 

        return _id;

    }

    public static String Role_ID()
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Role_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", string.Empty);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        _id = index.ToString("00"); ;


        return _id;

    }

    public static String Product_List_ID()
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Product_List_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", string.Empty);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        decimal index = decimal.Parse(id) + 1;

        //int index = Convert.ToInt32(id) + 1;


        string _id = string.Empty;
        _id = index.ToString(); 


        return _id;

    }

    public static String SAM_DN_No(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "SAM_DN_No");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "DN" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String SAM_CN_No(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "SAM_CN_No");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "CN" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String Payment_No(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Payment_No");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "PT" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String Subsidy_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Subsidy_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }



        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = CV_Code + "SD" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String Deduct_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Deduct_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();
            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        _id = CV_Code + "DD" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String Count_No(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Count_No");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        _id = CV_Code + "ST" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String Debt_ID(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Debt_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        _id = CV_Code + "DB" + year + month + date + index.ToString("0000"); ;


        return _id;
    }

    public static String Commission_requisition_no(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Commission_requisiti");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        _id = CV_Code + "RE" + year + month + date + index.ToString("000"); ;


        return _id;
    }

    public static String Post_No(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Post_No");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        // int index = int.Parse(id) + 1;
        int index = 1;
        string _id = string.Empty;
        _id = CV_Code + "PS" + year + month + date + index.ToString("00"); ;


        return _id;
    }

    public static String RV(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "RV");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        _id = CV_Code + "RV" + year + month + date + index.ToString("00"); ;

        return _id;
    }

    public static String EP(string CV_Code)
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);
        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "EP");
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }

        int index = int.Parse(id) + 1;

        string _id = string.Empty;
        _id = CV_Code + "EP" + year + month + date + index.ToString("00"); ;

        return _id;
    }
    //[Debt_ID]

    public static String Sales_Target_ID()
    {
        string now = GetNow();
        string year = now.Substring(0, 2);
        string month = now.Substring(2, 2);
        string date = now.Substring(4, 2);

        //string year = now.Substring(0, 2);
        //string date = now.Substring(2, 2);
        //string month = now.Substring(4, 2);

        string id = string.Empty;


        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GET_ID";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        selectCommand.Parameters.AddWithValue("@Year", year);
        selectCommand.Parameters.AddWithValue("@Month", month);
        selectCommand.Parameters.AddWithValue("@Day", date);

        selectCommand.Parameters.AddWithValue("@Naming", "Sales_target_ID");
        selectCommand.Parameters.AddWithValue("@CV_Code", "");

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                id = reader["ID"] is DBNull ? "0" : reader["ID"].ToString();

            }
            else
            {
                id = "0";
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            //return clsdbo_OrderAndDeliveryCycle;
        }
        finally
        {
            connection.Close();
        }


        int index = int.Parse(id) + 1;



        string _id = string.Empty;
        _id = "ST" + year + month + date + index.ToString("0000");

        return _id;

    }

}