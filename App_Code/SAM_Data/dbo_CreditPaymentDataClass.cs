using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;
public class dbo_CreditPaymentDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    [Obsolete]
    public static List<dbo_CreditPaymentClass> GetCreditPayment()
    {
        List<dbo_CreditPaymentClass> creditList = new List<dbo_CreditPaymentClass>();
        creditList.Add(new dbo_CreditPaymentClass { Payment_Date = DateTime.Now, Payment_Amount = 100, Cheque_No = "10001", Date = DateTime.Now });
        return creditList;
    }

    [Obsolete]
    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CreditPaymentSelectAll]";
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

    public static List<dbo_CreditPaymentClass> Search(String Credit_ID, String Clearing_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CreditPaymentSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!string.IsNullOrEmpty(Credit_ID))
        {
            selectCommand.Parameters.AddWithValue("@Credit_ID", Credit_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Credit_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Clearing_No))
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }

        List<dbo_CreditPaymentClass> item = new List<dbo_CreditPaymentClass>();
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
                    item.Add(new dbo_CreditPaymentClass()
                    {
                        Payment_No = reader["Payment_No"] is DBNull ? null : reader["Payment_No"].ToString(),
                        Credit_ID = reader["Credit_ID"] is DBNull ? null : reader["Credit_ID"].ToString(),
                        Payment_Date = reader["Payment_Date"] is DBNull ? null : (DateTime?)reader["Payment_Date"],
                        Payment_Amount = reader["Payment_Amount"] is DBNull ? null : (Decimal?)reader["Payment_Amount"],
                        Payment_Method = reader["Payment_Method"] is DBNull ? null : reader["Payment_Method"].ToString(),


                        Bank = reader["Bank"] is DBNull ? null : reader["Bank"].ToString(),


                        Cheque_No = reader["Cheque_No"] is DBNull ? null : reader["Cheque_No"].ToString(),
                        Date = reader["Date"] is DBNull ? null : (DateTime?)reader["Date"],
                        Clearing_Cheque = reader["Clearing_Cheque"] is DBNull ? null : (Boolean?)reader["Clearing_Cheque"],
                        Last_Modified_Date = reader["Last_Modified_Date"] is DBNull ? null : (DateTime?)reader["Last_Modified_Date"]

                        
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

    public static dbo_CreditPaymentClass Select_Record(String Payment_No)
    {
        dbo_CreditPaymentClass clsdbo_CreditPayment = new dbo_CreditPaymentClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CreditPaymentSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Payment_No", Payment_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CreditPayment.Payment_No = reader["Payment_No"] is DBNull ? null : reader["Payment_No"].ToString();
                clsdbo_CreditPayment.Credit_ID = reader["Credit_ID"] is DBNull ? null : reader["Credit_ID"].ToString();
                clsdbo_CreditPayment.Payment_Date = reader["Payment_Date"] is DBNull ? null : (DateTime?)reader["Payment_Date"];
                clsdbo_CreditPayment.Payment_Amount = reader["Payment_Amount"] is DBNull ? null : (Decimal?)reader["Payment_Amount"];
                clsdbo_CreditPayment.Payment_Method = reader["Payment_Method"] is DBNull ? null : reader["Payment_Method"].ToString();
                clsdbo_CreditPayment.Bank = reader["Bank"] is DBNull ? null : reader["Bank"].ToString();
                clsdbo_CreditPayment.Cheque_No = reader["Cheque_No"] is DBNull ? null : reader["Cheque_No"].ToString();
                clsdbo_CreditPayment.Date = reader["Date"] is DBNull ? null : (DateTime?)reader["Date"];
                clsdbo_CreditPayment.Clearing_Cheque = reader["Clearing_Cheque"] is DBNull ? null : (Boolean?)reader["Clearing_Cheque"];
            }
            else
            {
                clsdbo_CreditPayment = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_CreditPayment;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CreditPayment;
    }

    public static bool Add(dbo_CreditPaymentClass clsdbo_CreditPayment)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[CreditPaymentInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CreditPayment.Payment_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Payment_No", clsdbo_CreditPayment.Payment_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Payment_No", DBNull.Value);
        }
        if (clsdbo_CreditPayment.Credit_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Credit_ID", clsdbo_CreditPayment.Credit_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_ID", DBNull.Value);
        }
        if (clsdbo_CreditPayment.Payment_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Payment_Date", clsdbo_CreditPayment.Payment_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Payment_Date", DBNull.Value);
        }
        if (clsdbo_CreditPayment.Payment_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Payment_Amount", clsdbo_CreditPayment.Payment_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Payment_Amount", DBNull.Value);
        }
        if (clsdbo_CreditPayment.Payment_Method != null)
        {
            insertCommand.Parameters.AddWithValue("@Payment_Method", clsdbo_CreditPayment.Payment_Method);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Payment_Method", DBNull.Value);
        }
        if (clsdbo_CreditPayment.Bank != null)
        {
            insertCommand.Parameters.AddWithValue("@Bank", clsdbo_CreditPayment.Bank);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Bank", DBNull.Value);
        }
        if (clsdbo_CreditPayment.Cheque_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Cheque_No", clsdbo_CreditPayment.Cheque_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Cheque_No", DBNull.Value);
        }
        if (clsdbo_CreditPayment.Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Date", clsdbo_CreditPayment.Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Date", DBNull.Value);
        }

        if (clsdbo_CreditPayment.Clearing_Cheque.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_Cheque", clsdbo_CreditPayment.Clearing_Cheque);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_Cheque", DBNull.Value);
        }
        if (clsdbo_CreditPayment.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_CreditPayment.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
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

    public static bool Update(dbo_CreditPaymentClass newdbo_CreditPaymentClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[CreditPaymentUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CreditPaymentClass.Payment_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_No", newdbo_CreditPaymentClass.Payment_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_No", DBNull.Value);
        }
        if (newdbo_CreditPaymentClass.Credit_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_ID", newdbo_CreditPaymentClass.Credit_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCredit_ID", DBNull.Value);
        }
        if (newdbo_CreditPaymentClass.Payment_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Date", newdbo_CreditPaymentClass.Payment_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Date", DBNull.Value);
        }
        if (newdbo_CreditPaymentClass.Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Amount", newdbo_CreditPaymentClass.Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Amount", DBNull.Value);
        }
        if (newdbo_CreditPaymentClass.Payment_Method != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Method", newdbo_CreditPaymentClass.Payment_Method);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Method", DBNull.Value);
        }
        if (newdbo_CreditPaymentClass.Bank != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBank", newdbo_CreditPaymentClass.Bank);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBank", DBNull.Value);
        }
        if (newdbo_CreditPaymentClass.Cheque_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCheque_No", newdbo_CreditPaymentClass.Cheque_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCheque_No", DBNull.Value);
        }
        if (newdbo_CreditPaymentClass.Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDate", newdbo_CreditPaymentClass.Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDate", DBNull.Value);
        }
        if (newdbo_CreditPaymentClass.Clearing_Cheque.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_Cheque", newdbo_CreditPaymentClass.Clearing_Cheque);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_Cheque", DBNull.Value);
        }

        if (newdbo_CreditPaymentClass.Last_Modified_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_Date", newdbo_CreditPaymentClass.Last_Modified_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_Date", DBNull.Value);
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

    public static bool Delete(String Payment_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[CreditPaymentDelete]";


        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;


        if (Payment_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldPayment_No", Payment_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPayment_No", DBNull.Value);
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

