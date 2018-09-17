using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;
public class dbo_CreditDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    [Obsolete]
    public static List<dbo_CreditClass> GetCredit()
    {
        List<dbo_CreditClass> creditList = new List<dbo_CreditClass>();
        creditList.Add(new dbo_CreditClass
        {
            Customer_Name = "วุฒิชัย ไป่กุกเต",
            Credit_Date = DateTime.Now,
            Credit_Amount = 100,
            Total_Payment_Amount = 50,
            Balance_Outstanding_Amount = 50,
            Status = "ค้างชำระ"
        });

        creditList.Add(new dbo_CreditClass
        {
            Customer_Name = "สมชาย สายลม",
            Credit_Date = DateTime.Now,
            Credit_Amount = 100,
            Total_Payment_Amount = 100,
            Balance_Outstanding_Amount = 0,
            Status = "ชำระครบแล้ว"
        });

        return creditList;
    }
    [Obsolete]
    public static List<dbo_CreditClass> SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CreditSelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        List<dbo_CreditClass> item = new List<dbo_CreditClass>();
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
                    item.Add(new dbo_CreditClass()
                    {
                        Credit_ID = reader["Credit_ID"] is DBNull ? null : reader["Credit_ID"].ToString(),
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString(),
                        Credit_Date = reader["Credit_Date"] is DBNull ? null : (DateTime?)reader["Credit_Date"],
                        Credit_Amount = reader["Credit_Amount"] is DBNull ? null : (Decimal?)reader["Credit_Amount"],
                        Total_Payment_Amount = reader["Total_Payment_Amount"] is DBNull ? null : (Decimal?)reader["Total_Payment_Amount"],
                        Balance_Outstanding_Amount = reader["Balance_Outstanding_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding_Amount"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString()

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

    public static List<dbo_CreditClass> Search(String Clearing_No , string Customer_ID , DateTime? Credit_Date , string status,string SP_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CreditSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Clearing_No))
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Customer_ID))
        {
            selectCommand.Parameters.AddWithValue("@Customer_ID", Customer_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Customer_ID", DBNull.Value);
        }

        if (Credit_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Credit_Date", Credit_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Credit_Date", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(status))
        {
            selectCommand.Parameters.AddWithValue("@status", status);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@status", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(SP_ID ))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }

        DataTable dt = new DataTable();

        List<dbo_CreditClass> item = new List<dbo_CreditClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_CreditClass()
                    {
                        Credit_ID = reader["Credit_ID"] is DBNull ? null : reader["Credit_ID"].ToString(),
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString(),
                        Credit_Date = reader["Credit_Date"] is DBNull ? null : (DateTime?)reader["Credit_Date"],
                        Credit_Amount = reader["Credit_Amount"] is DBNull ? null : (Decimal?)reader["Credit_Amount"],
                        Total_Payment_Amount = reader["Total_Payment_Amount"] is DBNull ? null : (Decimal?)reader["Total_Payment_Amount"],
                        Balance_Outstanding_Amount = reader["Balance_Outstanding_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding_Amount"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Customer_Name = reader["Customer_Name"] is DBNull ? null : reader["Customer_Name"].ToString()

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

    public static dbo_CreditClass Select_Record(String Credit_ID)
    {
        dbo_CreditClass clsdbo_Credit = new dbo_CreditClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CreditSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Credit_ID", Credit_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Credit.Credit_ID = reader["Credit_ID"] is DBNull ? null : reader["Credit_ID"].ToString();
                clsdbo_Credit.Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString();
                clsdbo_Credit.Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString();
                clsdbo_Credit.Credit_Date = reader["Credit_Date"] is DBNull ? null : (DateTime?)reader["Credit_Date"];
                clsdbo_Credit.Credit_Amount = reader["Credit_Amount"] is DBNull ? null : (Decimal?)reader["Credit_Amount"];
                clsdbo_Credit.Total_Payment_Amount = reader["Total_Payment_Amount"] is DBNull ? null : (Decimal?)reader["Total_Payment_Amount"];
                clsdbo_Credit.Balance_Outstanding_Amount = reader["Balance_Outstanding_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding_Amount"];
                clsdbo_Credit.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
            }
            else
            {
                clsdbo_Credit = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Credit;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Credit;
    }

    public static bool Add(dbo_CreditClass clsdbo_Credit)
    {

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[CreditInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Credit.Credit_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Credit_ID", clsdbo_Credit.Credit_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_ID", DBNull.Value);
        }
        if (clsdbo_Credit.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_Credit.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (clsdbo_Credit.Customer_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Customer_ID", clsdbo_Credit.Customer_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Customer_ID", DBNull.Value);
        }
        if (clsdbo_Credit.Credit_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Credit_Date", clsdbo_Credit.Credit_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_Date", DBNull.Value);
        }
        if (clsdbo_Credit.Credit_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Credit_Amount", clsdbo_Credit.Credit_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_Amount", DBNull.Value);
        }
        if (clsdbo_Credit.Total_Payment_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Payment_Amount", clsdbo_Credit.Total_Payment_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Payment_Amount", DBNull.Value);
        }
        if (clsdbo_Credit.Balance_Outstanding_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Balance_Outstanding_Amount", clsdbo_Credit.Balance_Outstanding_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Balance_Outstanding_Amount", DBNull.Value);
        }
        if (clsdbo_Credit.Status != null)
        {
            insertCommand.Parameters.AddWithValue("@Status", clsdbo_Credit.Status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Status", DBNull.Value);
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

    public static bool Update( dbo_CreditClass newdbo_CreditClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[CreditUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CreditClass.Credit_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_ID", newdbo_CreditClass.Credit_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_ID", DBNull.Value);
        }
        if (newdbo_CreditClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", newdbo_CreditClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", DBNull.Value);
        }
        if (newdbo_CreditClass.Customer_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCustomer_ID", newdbo_CreditClass.Customer_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCustomer_ID", DBNull.Value);
        }
        if (newdbo_CreditClass.Credit_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Date", newdbo_CreditClass.Credit_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Date", DBNull.Value);
        }
        if (newdbo_CreditClass.Credit_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Amount", newdbo_CreditClass.Credit_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_Amount", DBNull.Value);
        }
        if (newdbo_CreditClass.Total_Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Payment_Amount", newdbo_CreditClass.Total_Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Payment_Amount", DBNull.Value);
        }
        if (newdbo_CreditClass.Balance_Outstanding_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewBalance_Outstanding_Amount", newdbo_CreditClass.Balance_Outstanding_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBalance_Outstanding_Amount", DBNull.Value);
        }
        if (newdbo_CreditClass.Status != null)
        {
            updateCommand.Parameters.AddWithValue("@NewStatus", newdbo_CreditClass.Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStatus", DBNull.Value);
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

    public static bool Delete(String Credit_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[CreditDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (Credit_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCredit_ID", Credit_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCredit_ID", DBNull.Value);
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

