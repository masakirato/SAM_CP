using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class dbo_RevenueExpenseDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RevenueExpenseSelectAll]";
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



    public static List<dbo_RevenueExpenseClass> GetRevenueExpense(DateTime? Post_Date_Begin, DateTime? Post_Date_End, String CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetRevenueExpense]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (Post_Date_Begin.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Post_Date_Begin", Post_Date_Begin.Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Post_Date_Begin", DateTime.Now.AddDays(-7));
        }

        if (Post_Date_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Post_Date_End", Post_Date_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Post_Date_End", DateTime.Now.AddDays(7));
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        List<dbo_RevenueExpenseClass> item = new List<dbo_RevenueExpenseClass>();
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
                    item.Add(new dbo_RevenueExpenseClass()
                    {
                        Post_Date = reader["Post_Date"] is DBNull ? null : (DateTime?)reader["Post_Date"],
                        Post_No = reader["Post_No"] is DBNull ? null : reader["Post_No"].ToString(),

                        RV_Account_No = reader["RV_Account_No"] is DBNull ? null : reader["RV_Account_No"].ToString(),
                        RV_Amount = reader["RV_Amount"] is DBNull ? null : (Decimal?)reader["RV_Amount"],

                        EP_Account_No = reader["EP_Account_No"] is DBNull ? null : reader["EP_Account_No"].ToString(),
                        EP_Amount = reader["EP_Amount"] is DBNull ? null : (Decimal?)reader["EP_Amount"],

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



    public static List<dbo_RevenueExpenseClass> Search(String Post_No, DateTime? Post_Date_Begin, DateTime? Post_Date_End, String CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RevenueExpenseSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Post_No))
        {
            selectCommand.Parameters.AddWithValue("@Post_No", Post_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Post_No", DBNull.Value);
        }

        if (Post_Date_Begin.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Post_Date_Begin", Post_Date_Begin.Value);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Post_Date_Begin", DateTime.Now.AddDays(-7));
        }

        if (Post_Date_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Post_Date_End", Post_Date_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Post_Date_End", DateTime.Now.AddDays(7));
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        List<dbo_RevenueExpenseClass> item = new List<dbo_RevenueExpenseClass>();
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
                    item.Add(new dbo_RevenueExpenseClass()
                    {
                        Post_Date = reader["Post_Date"] is DBNull ? null : (DateTime?)reader["Post_Date"],
                        Post_No = reader["Post_No"] is DBNull ? null : reader["Post_No"].ToString(),
                        Account_No = reader["Account_No"] is DBNull ? null : reader["Account_No"].ToString(),
                        Account_Code = reader["Account_Code"] is DBNull ? null : reader["Account_Code"].ToString(),
                        Amount = reader["Amount"] is DBNull ? null : (Decimal?)reader["Amount"],
                        Remark = reader["Remark"] is DBNull ? null : reader["Remark"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        Account_Name = reader["Account_Name"] is DBNull ? null : reader["Account_Name"].ToString()
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
    /*
    public static dbo_RevenueExpenseClass Select_Record(dbo_RevenueExpenseClass clsdbo_RevenueExpensePara)
    {
        dbo_RevenueExpenseClass clsdbo_RevenueExpense = new dbo_RevenueExpenseClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RevenueExpenseSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Post_Date", clsdbo_RevenueExpensePara.Post_Date);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_RevenueExpense.Post_Date = reader["Post_Date"] is DBNull ? null : (DateTime?)reader["Post_Date"];
                clsdbo_RevenueExpense.Post_No = reader["Post_No"] is DBNull ? null : reader["Post_No"].ToString();
                clsdbo_RevenueExpense.Account_No = reader["Account_No"] is DBNull ? null : reader["Account_No"].ToString();
                clsdbo_RevenueExpense.Account_Code = reader["Account_Code"] is DBNull ? null : reader["Account_Code"].ToString();
                clsdbo_RevenueExpense.Amount = reader["Amount"] is DBNull ? null : (Decimal?)reader["Amount"];
                clsdbo_RevenueExpense.Remark = reader["Remark"] is DBNull ? null : reader["Remark"].ToString();
            }
            else
            {
                clsdbo_RevenueExpense = null;
            }
            reader.Close();
        }
        catch (SqlException)
        {
            return clsdbo_RevenueExpense;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_RevenueExpense;
    }*/

    public static dbo_RevenueExpenseClass Select_Record(string Account_No)
    {
        dbo_RevenueExpenseClass clsdbo_RevenueExpense = new dbo_RevenueExpenseClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RevenueExpenseSelectbyAccountNo]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Account_No", Account_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_RevenueExpense.Post_Date = reader["Post_Date"] is DBNull ? null : (DateTime?)reader["Post_Date"];
                clsdbo_RevenueExpense.Post_No = reader["Post_No"] is DBNull ? null : reader["Post_No"].ToString();
                clsdbo_RevenueExpense.Account_No = reader["Account_No"] is DBNull ? null : reader["Account_No"].ToString();
                clsdbo_RevenueExpense.Account_Code = reader["Account_Code"] is DBNull ? null : reader["Account_Code"].ToString();
                clsdbo_RevenueExpense.Amount = reader["Amount"] is DBNull ? null : (Decimal?)reader["Amount"];
                clsdbo_RevenueExpense.Remark = reader["Remark"] is DBNull ? null : reader["Remark"].ToString();
            }
            else
            {
                clsdbo_RevenueExpense = null;
            }
            reader.Close();
        }
        catch (SqlException)
        {
            return clsdbo_RevenueExpense;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_RevenueExpense;
    }

    public static dbo_RevenueExpenseClass SumBySP(string User_ID)
    {
        dbo_RevenueExpenseClass clsdbo_RevenueExpense = new dbo_RevenueExpenseClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RevenueExpenseSumBySP]";
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
                clsdbo_RevenueExpense.Amount = reader["Amount"] is DBNull ? null : (Decimal?)reader["Amount"];
            }
            else
            {
                clsdbo_RevenueExpense = null;
            }
            reader.Close();
        }
        catch (SqlException)
        {
            return clsdbo_RevenueExpense;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_RevenueExpense;
    }

    public static bool Add(dbo_RevenueExpenseClass clsdbo_RevenueExpense)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[RevenueExpenseInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_RevenueExpense.Post_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Post_Date", clsdbo_RevenueExpense.Post_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Post_Date", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Post_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Post_No", clsdbo_RevenueExpense.Post_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Post_No", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Account_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Account_No", clsdbo_RevenueExpense.Account_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Account_No", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Account_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@Account_Code", clsdbo_RevenueExpense.Account_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Account_Code", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Amount", clsdbo_RevenueExpense.Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Amount", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Remark != null)
        {
            insertCommand.Parameters.AddWithValue("@Remark", clsdbo_RevenueExpense.Remark);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Remark", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_RevenueExpense.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool AddSP(dbo_RevenueExpenseClass clsdbo_RevenueExpense)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[RevenueExpenseInsertAddSP]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_RevenueExpense.Post_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Post_Date", clsdbo_RevenueExpense.Post_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Post_Date", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Post_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Post_No", clsdbo_RevenueExpense.Post_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Post_No", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Account_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Account_No", clsdbo_RevenueExpense.Account_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Account_No", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Account_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@Account_Code", clsdbo_RevenueExpense.Account_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Account_Code", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Amount", clsdbo_RevenueExpense.Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Amount", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Remark != null)
        {
            insertCommand.Parameters.AddWithValue("@Remark", clsdbo_RevenueExpense.Remark);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Remark", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_RevenueExpense.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        if (clsdbo_RevenueExpense.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_RevenueExpense.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Update(dbo_RevenueExpenseClass newdbo_RevenueExpenseClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[RevenueExpenseUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_RevenueExpenseClass.Post_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPost_Date", newdbo_RevenueExpenseClass.Post_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPost_Date", DBNull.Value);
        }
        if (newdbo_RevenueExpenseClass.Post_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPost_No", newdbo_RevenueExpenseClass.Post_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPost_No", DBNull.Value);
        }
        if (newdbo_RevenueExpenseClass.Account_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewAccount_No", newdbo_RevenueExpenseClass.Account_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewAccount_No", DBNull.Value);
        }
        if (newdbo_RevenueExpenseClass.Account_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewAccount_Code", newdbo_RevenueExpenseClass.Account_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewAccount_Code", DBNull.Value);
        }
        if (newdbo_RevenueExpenseClass.Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewAmount", newdbo_RevenueExpenseClass.Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewAmount", DBNull.Value);
        }
        if (newdbo_RevenueExpenseClass.Remark != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRemark", newdbo_RevenueExpenseClass.Remark);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRemark", DBNull.Value);
        }
        /*if (olddbo_RevenueExpenseClass.Post_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldPost_Date", olddbo_RevenueExpenseClass.Post_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldPost_Date", DBNull.Value);
        }
        if (olddbo_RevenueExpenseClass.Post_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldPost_No", olddbo_RevenueExpenseClass.Post_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldPost_No", DBNull.Value);
        }
        if (olddbo_RevenueExpenseClass.Account_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldAccount_No", olddbo_RevenueExpenseClass.Account_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldAccount_No", DBNull.Value);
        }
        if (olddbo_RevenueExpenseClass.Account_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@OldAccount_Code", olddbo_RevenueExpenseClass.Account_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldAccount_Code", DBNull.Value);
        }
        if (olddbo_RevenueExpenseClass.Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldAmount", olddbo_RevenueExpenseClass.Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldAmount", DBNull.Value);
        }*/
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

    /*
    public static bool Delete(dbo_RevenueExpenseClass clsdbo_RevenueExpense)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[RevenueExpenseDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_RevenueExpense.Post_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPost_Date", clsdbo_RevenueExpense.Post_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPost_Date", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Post_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldPost_No", clsdbo_RevenueExpense.Post_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPost_No", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Account_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldAccount_No", clsdbo_RevenueExpense.Account_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldAccount_No", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Account_Code != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldAccount_Code", clsdbo_RevenueExpense.Account_Code);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldAccount_Code", DBNull.Value);
        }
        if (clsdbo_RevenueExpense.Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldAmount", clsdbo_RevenueExpense.Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldAmount", DBNull.Value);
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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }*/

    public static bool Delete(string Account_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[RevenueExpenseDeletebyAccountNo]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        if (Account_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@Account_No", Account_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@Account_No", DBNull.Value);
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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool DeletebyPostNo(string Post_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[RevenueExpenseDeletebyPostNo]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        if (Post_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@Post_No", Post_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@Post_No", DBNull.Value);
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

