using log4net;
using System;
using System.Data;
using System.Data.SqlClient;

public class dbo_FIFOPaymentDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[FIFOPaymentSelectAll]";
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

    public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[FIFOPaymentSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "F I F O I D")
        {
            selectCommand.Parameters.AddWithValue("@FIFO_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@FIFO_ID", DBNull.Value);
        }
        if (sField == "C V C O D E")
        {
            selectCommand.Parameters.AddWithValue("@CV_CODE", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
        }
        if (sField == "S P I D")
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (sField == "Debt Date")
        {
            selectCommand.Parameters.AddWithValue("@Debt_Date", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Debt_Date", DBNull.Value);
        }
        if (sField == "Requisition No")
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (sField == "Payment Amount")
        {
            selectCommand.Parameters.AddWithValue("@Payment_Amount", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Payment_Amount", DBNull.Value);
        }
        if (sField == "Created Date")
        {
            selectCommand.Parameters.AddWithValue("@Created_Date", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Created_Date", DBNull.Value);
        }
        if (sField == "Created By")
        {
            selectCommand.Parameters.AddWithValue("@Created_By", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }
        if (sField == "Last Modified Date")
        {
            selectCommand.Parameters.AddWithValue("@Last_Modified_Date", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Last_Modified_Date", DBNull.Value);
        }
        if (sField == "Last Modified By")
        {
            selectCommand.Parameters.AddWithValue("@Last_Modified_By", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
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

    public static dbo_FIFOPaymentClass Select_Record(dbo_FIFOPaymentClass clsdbo_FIFOPaymentPara)
    {
        dbo_FIFOPaymentClass clsdbo_FIFOPayment = new dbo_FIFOPaymentClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[FIFOPaymentSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@FIFO_ID", clsdbo_FIFOPaymentPara.FIFO_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_FIFOPayment.FIFO_ID = reader["FIFO_ID"] is DBNull ? null : reader["FIFO_ID"].ToString();
                clsdbo_FIFOPayment.CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString();
                clsdbo_FIFOPayment.SP_ID = reader["SP_ID"] is DBNull ? null : reader["SP_ID"].ToString();
                clsdbo_FIFOPayment.Debt_Date = reader["Debt_Date"] is DBNull ? null : (DateTime?)reader["Debt_Date"];
                clsdbo_FIFOPayment.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
                clsdbo_FIFOPayment.Payment_Amount = reader["Payment_Amount"] is DBNull ? null : (Decimal?)reader["Payment_Amount"];
                clsdbo_FIFOPayment.Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"];
                clsdbo_FIFOPayment.Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString();
                clsdbo_FIFOPayment.Last_Modified_Date = reader["Last_Modified_Date"] is DBNull ? null : (DateTime?)reader["Last_Modified_Date"];
                clsdbo_FIFOPayment.Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString();
            }
            else
            {
                clsdbo_FIFOPayment = null;
            }
            reader.Close();
        }
        catch (SqlException)
        {
            return clsdbo_FIFOPayment;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_FIFOPayment;
    }

    public static bool Add(dbo_FIFOPaymentClass clsdbo_FIFOPayment)
    {


        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[FIFOPaymentInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        
        //if (clsdbo_FIFOPayment.FIFO_ID != null)
        //{
        //    insertCommand.Parameters.AddWithValue("@FIFO_ID", clsdbo_FIFOPayment.FIFO_ID);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@FIFO_ID", DBNull.Value);
        //}
        if (clsdbo_FIFOPayment.CV_CODE != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_CODE", clsdbo_FIFOPayment.CV_CODE);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.SP_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@SP_ID", clsdbo_FIFOPayment.SP_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Debt_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Debt_Date", clsdbo_FIFOPayment.Debt_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Debt_Date", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Requisition_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_FIFOPayment.Requisition_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Payment_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Payment_Amount", clsdbo_FIFOPayment.Payment_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Payment_Amount", DBNull.Value);
        }
        
        if (clsdbo_FIFOPayment.Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", clsdbo_FIFOPayment.Created_By);
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

    public static bool Update(dbo_FIFOPaymentClass olddbo_FIFOPaymentClass,
           dbo_FIFOPaymentClass newdbo_FIFOPaymentClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[FIFOPaymentUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_FIFOPaymentClass.FIFO_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewFIFO_ID", newdbo_FIFOPaymentClass.FIFO_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewFIFO_ID", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.CV_CODE != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_CODE", newdbo_FIFOPaymentClass.CV_CODE);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_CODE", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.SP_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSP_ID", newdbo_FIFOPaymentClass.SP_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSP_ID", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.Debt_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDebt_Date", newdbo_FIFOPaymentClass.Debt_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDebt_Date", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", newdbo_FIFOPaymentClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Amount", newdbo_FIFOPaymentClass.Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Amount", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.Created_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_Date", newdbo_FIFOPaymentClass.Created_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_Date", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.Created_By != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_By", newdbo_FIFOPaymentClass.Created_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_By", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.Last_Modified_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_Date", newdbo_FIFOPaymentClass.Last_Modified_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_Date", DBNull.Value);
        }
        if (newdbo_FIFOPaymentClass.Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_By", newdbo_FIFOPaymentClass.Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_By", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.FIFO_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldFIFO_ID", olddbo_FIFOPaymentClass.FIFO_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldFIFO_ID", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.CV_CODE != null)
        {
            updateCommand.Parameters.AddWithValue("@OldCV_CODE", olddbo_FIFOPaymentClass.CV_CODE);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCV_CODE", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.SP_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldSP_ID", olddbo_FIFOPaymentClass.SP_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldSP_ID", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.Debt_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldDebt_Date", olddbo_FIFOPaymentClass.Debt_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldDebt_Date", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldRequisition_No", olddbo_FIFOPaymentClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldRequisition_No", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldPayment_Amount", olddbo_FIFOPaymentClass.Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldPayment_Amount", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.Created_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldCreated_Date", olddbo_FIFOPaymentClass.Created_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCreated_Date", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.Created_By != null)
        {
            updateCommand.Parameters.AddWithValue("@OldCreated_By", olddbo_FIFOPaymentClass.Created_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCreated_By", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.Last_Modified_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Modified_Date", olddbo_FIFOPaymentClass.Last_Modified_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Modified_Date", DBNull.Value);
        }
        if (olddbo_FIFOPaymentClass.Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Modified_By", olddbo_FIFOPaymentClass.Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Modified_By", DBNull.Value);
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
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Delete(dbo_FIFOPaymentClass clsdbo_FIFOPayment)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[FIFOPaymentDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_FIFOPayment.FIFO_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldFIFO_ID", clsdbo_FIFOPayment.FIFO_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldFIFO_ID", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.CV_CODE != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_CODE", clsdbo_FIFOPayment.CV_CODE);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_CODE", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.SP_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSP_ID", clsdbo_FIFOPayment.SP_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSP_ID", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Debt_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldDebt_Date", clsdbo_FIFOPayment.Debt_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldDebt_Date", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Requisition_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", clsdbo_FIFOPayment.Requisition_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Payment_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPayment_Amount", clsdbo_FIFOPayment.Payment_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPayment_Amount", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Created_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_Date", clsdbo_FIFOPayment.Created_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_Date", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Created_By != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_By", clsdbo_FIFOPayment.Created_By);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_By", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Last_Modified_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_Date", clsdbo_FIFOPayment.Last_Modified_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_Date", DBNull.Value);
        }
        if (clsdbo_FIFOPayment.Last_Modified_By != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_By", clsdbo_FIFOPayment.Last_Modified_By);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_By", DBNull.Value);
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

