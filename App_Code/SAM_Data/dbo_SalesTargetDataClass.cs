using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
public class dbo_SalesTargetDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[SalesTargetSelectAll]";
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
            return dt;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static List<dbo_SalesTargetClass> Search(string CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "SalesTargetSearch";
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


        List<dbo_SalesTargetClass> item = new List<dbo_SalesTargetClass>();

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
                        new dbo_SalesTargetClass()
                        {
                            Sales_Target_ID = reader["Sales_Target_ID"] is DBNull ? string.Empty : reader["Sales_Target_ID"].ToString(),
                            CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                            Year = reader["Year"] is DBNull ? null : reader["Year"].ToString(),
                            Month = reader["Month"] is DBNull ? null : reader["Month"].ToString(),
                            Sales_Target = reader["Sales_Target"] is DBNull ? 0 : (Decimal?)reader["Sales_Target"],
                            Actual_Sales = reader["Actual_Sales"] is DBNull ? 0 : (Decimal?)reader["Actual_Sales"],
                            AgentName = reader["AgentName"] is DBNull ? string.Empty : reader["AgentName"].ToString()
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


    public static List<dbo_SalesTargetClass> Search2(string CV_Code, string Month, string Quarter, string Year)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "SalesTargetSearch2";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if(CV_Code =="เลือกทั้งหมด")
        {
            CV_Code = string.Empty;
        }

        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Month))
        {
            selectCommand.Parameters.AddWithValue("@Month", Month);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Month", DBNull.Value);
        }


        if (!string.IsNullOrEmpty(Quarter))
        {
            selectCommand.Parameters.AddWithValue("@Quarter", Quarter);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Quarter", DBNull.Value);
        }


        if (!string.IsNullOrEmpty(Year))
        {
            selectCommand.Parameters.AddWithValue("@Year", Year);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }



        List<dbo_SalesTargetClass> item = new List<dbo_SalesTargetClass>();

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
                        new dbo_SalesTargetClass()
                        {
                            Sales_Target_ID = reader["Sales_Target_ID"] is DBNull ? string.Empty : reader["Sales_Target_ID"].ToString(),
                            CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                            Year = reader["Year"] is DBNull ? null : reader["Year"].ToString(),
                            Month = reader["Month"] is DBNull ? null : reader["Month"].ToString(),
                            Sales_Target = reader["Sales_Target"] is DBNull ? 0 : (Decimal?)reader["Sales_Target"],
                            Actual_Sales = reader["Actual_Sales"] is DBNull ? 0 : (Decimal?)reader["Actual_Sales"],
                            AgentName = reader["AgentName"] is DBNull ? string.Empty : reader["AgentName"].ToString(),
                            MonthName = reader["MonthName"] is DBNull ? string.Empty : reader["MonthName"].ToString()
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


    public static List<dbo_SalesTargetClass> Search(string CV_Code, string Month, string Quarter, string Year)
    {
        /*
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "SalesTargetSearch2";
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
        if (!string.IsNullOrEmpty(Month))
        {
            selectCommand.Parameters.AddWithValue("@Month", Month);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Month", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Quarter))
        {
            selectCommand.Parameters.AddWithValue("@Quarter", Quarter);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Quarter", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Year))
        {
            selectCommand.Parameters.AddWithValue("@Year", Year);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }

        List<dbo_SalesTargetClass> item = new List<dbo_SalesTargetClass>();

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
            }


    */
        return null;
    }
    public static dbo_SalesTargetClass GetCurrentTarget(String CV_Code , DateTime? Date_of_create_order_or_PO_Date)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_SalesTargetClass clsdbo_SalesTarget = new dbo_SalesTargetClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "Get_Current_Target";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        //string s = DateTime.Now.ToString();
        //string now = DateTime.Parse(s).ToString("yyyyMMdd");

        //string year = now.Substring(0, 4);
        //string month = now.Substring(4, 2);
        //string date = now.Substring(4, 2);

        string s = Date_of_create_order_or_PO_Date.ToString();
        string now = DateTime.Parse(s).ToString("yyyyMMdd");

        string year = now.Substring(0, 4);
        string month = now.Substring(4, 2);
        string date = now.Substring(4, 2);

        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        selectCommand.Parameters.AddWithValue("@year", year);
        selectCommand.Parameters.AddWithValue("@month", month);
        

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {

                clsdbo_SalesTarget.Sales_Target = reader["Sales_Target"] is DBNull ? null : (Decimal?)reader["Sales_Target"];
                clsdbo_SalesTarget.Actual_Sales = reader["Actual_Sales"] is DBNull ? null : (Decimal?)reader["Actual_Sales"];
                clsdbo_SalesTarget.Sales_Target_Quarter = reader["Sales_Target_Quarter"] is DBNull ? null : (Decimal?)reader["Sales_Target_Quarter"];
                clsdbo_SalesTarget.Actual_Sales_Quarter = reader["Actual_Sales_Quarter"] is DBNull ? null : (Decimal?)reader["Actual_Sales_Quarter"];
                clsdbo_SalesTarget.Sales_Target_Year = reader["Sales_Target_Year"] is DBNull ? null : (Decimal?)reader["Sales_Target_Year"];
                clsdbo_SalesTarget.Actual_Sales_Year = reader["Actual_Sales_Year"] is DBNull ? null : (Decimal?)reader["Actual_Sales_Year"];
                clsdbo_SalesTarget.Actual_PO = reader["Actual_PO"] is DBNull ? null : (Decimal?)reader["Actual_PO"];
            }
            else
            {
                clsdbo_SalesTarget = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_SalesTarget;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_SalesTarget;
    }


    public static dbo_SalesTargetClass Select_Record(String Sales_Target_ID)
    {
        dbo_SalesTargetClass clsdbo_SalesTarget = new dbo_SalesTargetClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[SalesTargetSelectByID]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Sales_Target_ID))
        {
            selectCommand.Parameters.AddWithValue("@Sales_Target_ID", Sales_Target_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Sales_Target_ID", DBNull.Value);
        }



        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_SalesTarget.Sales_Target_ID = reader["Sales_Target_ID"] is DBNull ? null : reader["Sales_Target_ID"].ToString();
                clsdbo_SalesTarget.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_SalesTarget.Year = reader["Year"] is DBNull ? null : reader["Year"].ToString();
                clsdbo_SalesTarget.Month = reader["Month"] is DBNull ? null : reader["Month"].ToString();
                clsdbo_SalesTarget.Sales_Target = reader["Sales_Target"] is DBNull ? null : (Decimal?)reader["Sales_Target"];
                clsdbo_SalesTarget.Actual_Sales = reader["Actual_Sales"] is DBNull ? null : (Decimal?)reader["Actual_Sales"];
            }
            else
            {
                clsdbo_SalesTarget = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_SalesTarget;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_SalesTarget;
    }

    public static dbo_SalesTargetClass Select_Record(String CV_Code, String Year, String Month)
    {
        dbo_SalesTargetClass clsdbo_SalesTarget = new dbo_SalesTargetClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[SalesTargetSelect]";
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
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@Year", Year);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@Month", Month);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Month", DBNull.Value);
        }




        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_SalesTarget.Sales_Target_ID = reader["Sales_Target_ID"] is DBNull ? null : reader["Sales_Target_ID"].ToString();
                clsdbo_SalesTarget.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_SalesTarget.Year = reader["Year"] is DBNull ? null : reader["Year"].ToString();
                clsdbo_SalesTarget.Month = reader["Month"] is DBNull ? null : reader["Month"].ToString();
                clsdbo_SalesTarget.Sales_Target = reader["Sales_Target"] is DBNull ? null : (Decimal?)reader["Sales_Target"];
                clsdbo_SalesTarget.Actual_Sales = reader["Actual_Sales"] is DBNull ? null : (Decimal?)reader["Actual_Sales"];
            }
            else
            {
                clsdbo_SalesTarget = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_SalesTarget;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_SalesTarget;
    }

    public static bool Add(dbo_SalesTargetClass clsdbo_SalesTarget)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[SalesTargetInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_SalesTarget.Sales_Target_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Sales_Target_ID", clsdbo_SalesTarget.Sales_Target_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sales_Target_ID", DBNull.Value);
        }
        if (clsdbo_SalesTarget.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_SalesTarget.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (clsdbo_SalesTarget.Year != null)
        {
            insertCommand.Parameters.AddWithValue("@Year", clsdbo_SalesTarget.Year);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Year", DBNull.Value);
        }
        if (clsdbo_SalesTarget.Month != null)
        {
            insertCommand.Parameters.AddWithValue("@Month", clsdbo_SalesTarget.Month);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Month", DBNull.Value);
        }
        if (clsdbo_SalesTarget.Sales_Target.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Sales_Target", clsdbo_SalesTarget.Sales_Target);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sales_Target", DBNull.Value);
        }
        if (clsdbo_SalesTarget.Actual_Sales.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Actual_Sales", clsdbo_SalesTarget.Actual_Sales);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Actual_Sales", DBNull.Value);
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

    public static bool Update(dbo_SalesTargetClass newdbo_SalesTargetClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[SalesTargetUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_SalesTargetClass.Sales_Target_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSales_Target_ID", newdbo_SalesTargetClass.Sales_Target_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSales_Target_ID", DBNull.Value);
        }
        if (newdbo_SalesTargetClass.CV_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", newdbo_SalesTargetClass.CV_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", DBNull.Value);
        }
        if (newdbo_SalesTargetClass.Year != null)
        {
            updateCommand.Parameters.AddWithValue("@NewYear", newdbo_SalesTargetClass.Year);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewYear", DBNull.Value);
        }
        if (newdbo_SalesTargetClass.Month != null)
        {
            updateCommand.Parameters.AddWithValue("@NewMonth", newdbo_SalesTargetClass.Month);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewMonth", DBNull.Value);
        }
        if (newdbo_SalesTargetClass.Sales_Target.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSales_Target", newdbo_SalesTargetClass.Sales_Target);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSales_Target", DBNull.Value);
        }
        if (newdbo_SalesTargetClass.Actual_Sales.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewActual_Sales", newdbo_SalesTargetClass.Actual_Sales);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewActual_Sales", DBNull.Value);
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
            return false;
        }
        finally
        {
            connection.Close();
        }
    }
    /*
    public static bool Delete(dbo_SalesTargetClass clsdbo_SalesTarget)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[SalesTargetDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_SalesTarget.Sales_Target_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSales_Target_ID", clsdbo_SalesTarget.Sales_Target_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSales_Target_ID", DBNull.Value);
        }
        if (clsdbo_SalesTarget.CV_Code != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", clsdbo_SalesTarget.CV_Code);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", DBNull.Value);
        }
        if (clsdbo_SalesTarget.Year != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldYear", clsdbo_SalesTarget.Year);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldYear", DBNull.Value);
        }
        if (clsdbo_SalesTarget.Month != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldMonth", clsdbo_SalesTarget.Month);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldMonth", DBNull.Value);
        }
        if (clsdbo_SalesTarget.Sales_Target.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldSales_Target", clsdbo_SalesTarget.Sales_Target);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSales_Target", DBNull.Value);
        }
        if (clsdbo_SalesTarget.Actual_Sales.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldActual_Sales", clsdbo_SalesTarget.Actual_Sales);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldActual_Sales", DBNull.Value);
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
            return false;
        }
        finally
        {
            connection.Close();
        }
    }*/
    public static bool Delete(string Sales_Target_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[SalesTargetDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (!string.IsNullOrEmpty(Sales_Target_ID))
        {
            deleteCommand.Parameters.AddWithValue("@Sales_Target_ID", Sales_Target_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@Sales_Target_ID", DBNull.Value);
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
            return false;
        }
        finally
        {
            connection.Close();
        }
    }
}

