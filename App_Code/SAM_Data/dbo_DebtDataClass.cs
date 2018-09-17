using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;
public class dbo_DebtDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[DebtSelectAll]";
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

    public static List<dbo_DebtClass> Search(String SP_ID, String Customer_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[DebtSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;




        if (!string.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Customer_ID))
        {
            selectCommand.Parameters.AddWithValue("@Customer_ID", Customer_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Customer_ID", DBNull.Value);
        }


        List<dbo_DebtClass> item = new List<dbo_DebtClass>();

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
                    item.Add(new dbo_DebtClass()
                    {
                        Debt_ID = reader["Debt_ID"] is DBNull ? null : reader["Debt_ID"].ToString(),
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString(),
                        Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString(),
                        Debt_Date = reader["Debt_Date"] is DBNull ? null : (DateTime?)reader["Debt_Date"],
                        Debt_Amount = reader["Debt_Amount"] is DBNull ? null : (Decimal?)reader["Debt_Amount"],
                        Total_Payment_Amount = reader["Total_Payment_Amount"] is DBNull ? null : (Decimal?)reader["Total_Payment_Amount"],
                        Balance_Outstanding_Amount = reader["Balance_Outstanding_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding_Amount"],
                        Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"]
                        ,
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString()


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

    public static dbo_DebtClass SelectByRequisitionNo(string Requisition_No)
    {
        dbo_DebtClass clsdbo_Debt = new dbo_DebtClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[DebtSelectByRequisitionNo]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Debt.Debt_ID = reader["Debt_ID"] is DBNull ? null : reader["Debt_ID"].ToString();
                clsdbo_Debt.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_Debt.SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString();
                clsdbo_Debt.Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString();
                clsdbo_Debt.Debt_Date = reader["Debt_Date"] is DBNull ? null : (DateTime?)reader["Debt_Date"];
                clsdbo_Debt.Debt_Amount = reader["Debt_Amount"] is DBNull ? null : (Decimal?)reader["Debt_Amount"];
                clsdbo_Debt.Total_Payment_Amount = reader["Total_Payment_Amount"] is DBNull ? null : (Decimal?)reader["Total_Payment_Amount"];
                clsdbo_Debt.Balance_Outstanding_Amount = reader["Balance_Outstanding_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding_Amount"];
                clsdbo_Debt.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
            }
            else
            {
                clsdbo_Debt = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Debt;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Debt;
    }

    public static dbo_DebtClass Select_Record(dbo_DebtClass clsdbo_DebtPara)
    {
        dbo_DebtClass clsdbo_Debt = new dbo_DebtClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[DebtSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Debt_ID", clsdbo_DebtPara.Debt_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Debt.Debt_ID = reader["Debt_ID"] is DBNull ? null : reader["Debt_ID"].ToString();
                clsdbo_Debt.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_Debt.SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString();
                clsdbo_Debt.Customer_ID = reader["Customer_ID"] is DBNull ? null : reader["Customer_ID"].ToString();
                clsdbo_Debt.Debt_Date = reader["Debt_Date"] is DBNull ? null : (DateTime?)reader["Debt_Date"];
                clsdbo_Debt.Debt_Amount = reader["Debt_Amount"] is DBNull ? null : (Decimal?)reader["Debt_Amount"];
                clsdbo_Debt.Total_Payment_Amount = reader["Total_Payment_Amount"] is DBNull ? null : (Decimal?)reader["Total_Payment_Amount"];
                clsdbo_Debt.Balance_Outstanding_Amount = reader["Balance_Outstanding_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Outstanding_Amount"];
            }
            else
            {
                clsdbo_Debt = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Debt;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Debt;
    }

    public static bool Add(dbo_DebtClass clsdbo_Debt, String Created_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[DebtInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Debt.Debt_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Debt_ID", clsdbo_Debt.Debt_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Debt_ID", DBNull.Value);
        }
        if (clsdbo_Debt.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_Debt.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (clsdbo_Debt.SP_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@SP_ID", clsdbo_Debt.SP_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (clsdbo_Debt.Customer_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Customer_ID", clsdbo_Debt.Customer_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Customer_ID", DBNull.Value);
        }
        if (clsdbo_Debt.Debt_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Debt_Date", clsdbo_Debt.Debt_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Debt_Date", DBNull.Value);
        }
        if (clsdbo_Debt.Debt_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Debt_Amount", clsdbo_Debt.Debt_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Debt_Amount", DBNull.Value);
        }
        if (clsdbo_Debt.Total_Payment_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Payment_Amount", clsdbo_Debt.Total_Payment_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Payment_Amount", DBNull.Value);
        }
        if (clsdbo_Debt.Balance_Outstanding_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Balance_Outstanding_Amount", clsdbo_Debt.Balance_Outstanding_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Balance_Outstanding_Amount", DBNull.Value);
        }

        if (clsdbo_Debt.Requisition_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_Debt.Requisition_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Created_By))
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

    public static bool Update(dbo_DebtClass newdbo_DebtClass, String Last_Modified_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[DebtUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_DebtClass.Debt_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDebt_ID", newdbo_DebtClass.Debt_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDebt_ID", DBNull.Value);
        }
        if (newdbo_DebtClass.CV_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", newdbo_DebtClass.CV_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", DBNull.Value);
        }
        if (newdbo_DebtClass.SP_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSP_ID", newdbo_DebtClass.SP_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSP_ID", DBNull.Value);
        }
        if (newdbo_DebtClass.Customer_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCustomer_ID", newdbo_DebtClass.Customer_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCustomer_ID", DBNull.Value);
        }
        if (newdbo_DebtClass.Debt_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDebt_Date", newdbo_DebtClass.Debt_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDebt_Date", DBNull.Value);
        }
        if (newdbo_DebtClass.Debt_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDebt_Amount", newdbo_DebtClass.Debt_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDebt_Amount", DBNull.Value);
        }
        if (newdbo_DebtClass.Total_Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Payment_Amount", newdbo_DebtClass.Total_Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Payment_Amount", DBNull.Value);
        }
        if (newdbo_DebtClass.Balance_Outstanding_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewBalance_Outstanding_Amount", newdbo_DebtClass.Balance_Outstanding_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBalance_Outstanding_Amount", DBNull.Value);
        }
        if (Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", Last_Modified_By);
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

    public static bool Delete(String Debt_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);


        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[DebtDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (Debt_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldDebt_ID", Debt_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldDebt_ID", DBNull.Value);
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

