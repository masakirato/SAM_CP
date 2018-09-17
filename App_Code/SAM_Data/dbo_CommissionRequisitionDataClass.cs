using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class dbo_CommissionRequisitionDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionRequisitionSelectAll]";
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
        string selectProcedure = "[dbo].[CommissionRequisitionSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Commission Requisition No")
        {
            selectCommand.Parameters.AddWithValue("@Commission_requisition_no", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Commission_requisition_no", DBNull.Value);
        }
        if (sField == "Commission Requisition Date")
        {
            selectCommand.Parameters.AddWithValue("@Commission_Requisition_Date", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Commission_Requisition_Date", DBNull.Value);
        }
        if (sField == "Last Modified By")
        {
            selectCommand.Parameters.AddWithValue("@Last_Modified_By", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
        }
        if (sField == "Requisition Amount")
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Amount", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Amount", DBNull.Value);
        }
        if (sField == "Commission Balance Outstanding")
        {
            selectCommand.Parameters.AddWithValue("@Commission_Balance_Outstanding", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Commission_Balance_Outstanding", DBNull.Value);
        }
        if (sField == "Total Balance Outstanding")
        {
            selectCommand.Parameters.AddWithValue("@Total_Balance_Outstanding", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Total_Balance_Outstanding", DBNull.Value);
        }
        if (sField == "Total Credit Amount")
        {
            selectCommand.Parameters.AddWithValue("@Total_Credit_Amount", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Total_Credit_Amount", DBNull.Value);
        }
        if (sField == "Created Date")
        {
            selectCommand.Parameters.AddWithValue("@Created_Date", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Created_Date", DBNull.Value);
        }
        if (sField == "Last Modified Date")
        {
            selectCommand.Parameters.AddWithValue("@Last_Modified_Date", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Last_Modified_Date", DBNull.Value);
        }
        if (sField == "Created By")
        {
            selectCommand.Parameters.AddWithValue("@Created_By", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
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

    public static dbo_CommissionRequisitionClass Select_Record(dbo_CommissionRequisitionClass clsdbo_CommissionRequisitionPara)
    {
        dbo_CommissionRequisitionClass clsdbo_CommissionRequisition = new dbo_CommissionRequisitionClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionRequisitionSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Commission_requisition_no", clsdbo_CommissionRequisitionPara.Commission_requisition_no);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CommissionRequisition.Commission_requisition_no = reader["Commission_requisition_no"] is DBNull ? null : reader["Commission_requisition_no"].ToString();
                clsdbo_CommissionRequisition.Commission_Requisition_Date = reader["Commission_Requisition_Date"] is DBNull ? null : (DateTime?)reader["Commission_Requisition_Date"];
                clsdbo_CommissionRequisition.Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString();
                clsdbo_CommissionRequisition.Requisition_Amount = reader["Requisition_Amount"] is DBNull ? null : (Decimal?)reader["Requisition_Amount"];
                clsdbo_CommissionRequisition.Commission_Balance_Outstanding = reader["Commission_Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Commission_Balance_Outstanding"];
                clsdbo_CommissionRequisition.Total_Balance_Outstanding = reader["Total_Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Total_Balance_Outstanding"];
                clsdbo_CommissionRequisition.Total_Credit_Amount = reader["Total_Credit_Amount"] is DBNull ? null : (Decimal?)reader["Total_Credit_Amount"];
                clsdbo_CommissionRequisition.Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"];
                clsdbo_CommissionRequisition.Last_Modified_Date = reader["Last_Modified_Date"] is DBNull ? null : (DateTime?)reader["Last_Modified_Date"];
                clsdbo_CommissionRequisition.Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString();
            }
            else
            {
                clsdbo_CommissionRequisition = null;
            }
            reader.Close();
        }
        catch (SqlException)
        {
            return clsdbo_CommissionRequisition;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CommissionRequisition;
    }

    public static bool Add(dbo_CommissionRequisitionClass clsdbo_CommissionRequisition, String Created_By)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[CommissionRequisitionInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CommissionRequisition.Commission_requisition_no != null)
        {
            insertCommand.Parameters.AddWithValue("@Commission_requisition_no", clsdbo_CommissionRequisition.Commission_requisition_no);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Commission_requisition_no", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Commission_Requisition_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Commission_Requisition_Date", clsdbo_CommissionRequisition.Commission_Requisition_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Commission_Requisition_Date", DBNull.Value);
        }

        if (clsdbo_CommissionRequisition.Requisition_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Amount", clsdbo_CommissionRequisition.Requisition_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Amount", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Commission_Balance_Outstanding.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Commission_Balance_Outstanding", clsdbo_CommissionRequisition.Commission_Balance_Outstanding);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Commission_Balance_Outstanding", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Total_Balance_Outstanding.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Balance_Outstanding", clsdbo_CommissionRequisition.Total_Balance_Outstanding);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Balance_Outstanding", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Total_Credit_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Total_Credit_Amount", clsdbo_CommissionRequisition.Total_Credit_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Total_Credit_Amount", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Created_By))
        {
            insertCommand.Parameters.AddWithValue("@Created_By", Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }


        /*

        if (clsdbo_CommissionRequisition.Created_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Created_Date", clsdbo_CommissionRequisition.Created_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_Date", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Last_Modified_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_Date", clsdbo_CommissionRequisition.Last_Modified_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_Date", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", clsdbo_CommissionRequisition.Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Last_Modified_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_By", clsdbo_CommissionRequisition.Last_Modified_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
        }

        */



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

    public static bool Update(dbo_CommissionRequisitionClass olddbo_CommissionRequisitionClass,
           dbo_CommissionRequisitionClass newdbo_CommissionRequisitionClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[CommissionRequisitionUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CommissionRequisitionClass.Commission_requisition_no != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCommission_requisition_no", newdbo_CommissionRequisitionClass.Commission_requisition_no);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCommission_requisition_no", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Commission_Requisition_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCommission_Requisition_Date", newdbo_CommissionRequisitionClass.Commission_Requisition_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCommission_Requisition_Date", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_By", newdbo_CommissionRequisitionClass.Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_By", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Requisition_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Amount", newdbo_CommissionRequisitionClass.Requisition_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Amount", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Commission_Balance_Outstanding.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCommission_Balance_Outstanding", newdbo_CommissionRequisitionClass.Commission_Balance_Outstanding);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCommission_Balance_Outstanding", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Total_Balance_Outstanding.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Balance_Outstanding", newdbo_CommissionRequisitionClass.Total_Balance_Outstanding);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Balance_Outstanding", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Total_Credit_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Credit_Amount", newdbo_CommissionRequisitionClass.Total_Credit_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTotal_Credit_Amount", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Created_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_Date", newdbo_CommissionRequisitionClass.Created_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_Date", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Last_Modified_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_Date", newdbo_CommissionRequisitionClass.Last_Modified_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Modified_Date", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionClass.Created_By != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_By", newdbo_CommissionRequisitionClass.Created_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCreated_By", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Commission_requisition_no != null)
        {
            updateCommand.Parameters.AddWithValue("@OldCommission_requisition_no", olddbo_CommissionRequisitionClass.Commission_requisition_no);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCommission_requisition_no", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Commission_Requisition_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldCommission_Requisition_Date", olddbo_CommissionRequisitionClass.Commission_Requisition_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCommission_Requisition_Date", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Modified_By", olddbo_CommissionRequisitionClass.Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Modified_By", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Requisition_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldRequisition_Amount", olddbo_CommissionRequisitionClass.Requisition_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldRequisition_Amount", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Commission_Balance_Outstanding.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldCommission_Balance_Outstanding", olddbo_CommissionRequisitionClass.Commission_Balance_Outstanding);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCommission_Balance_Outstanding", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Total_Balance_Outstanding.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldTotal_Balance_Outstanding", olddbo_CommissionRequisitionClass.Total_Balance_Outstanding);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldTotal_Balance_Outstanding", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Total_Credit_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldTotal_Credit_Amount", olddbo_CommissionRequisitionClass.Total_Credit_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldTotal_Credit_Amount", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Created_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldCreated_Date", olddbo_CommissionRequisitionClass.Created_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCreated_Date", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Last_Modified_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Modified_Date", olddbo_CommissionRequisitionClass.Last_Modified_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldLast_Modified_Date", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionClass.Created_By != null)
        {
            updateCommand.Parameters.AddWithValue("@OldCreated_By", olddbo_CommissionRequisitionClass.Created_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCreated_By", DBNull.Value);
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

    public static bool Delete(dbo_CommissionRequisitionClass clsdbo_CommissionRequisition)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[CommissionRequisitionDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CommissionRequisition.Commission_requisition_no != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_requisition_no", clsdbo_CommissionRequisition.Commission_requisition_no);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_requisition_no", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Commission_Requisition_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_Requisition_Date", clsdbo_CommissionRequisition.Commission_Requisition_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_Requisition_Date", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Last_Modified_By != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_By", clsdbo_CommissionRequisition.Last_Modified_By);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_By", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Requisition_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Amount", clsdbo_CommissionRequisition.Requisition_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Amount", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Commission_Balance_Outstanding.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_Balance_Outstanding", clsdbo_CommissionRequisition.Commission_Balance_Outstanding);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_Balance_Outstanding", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Total_Balance_Outstanding.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Balance_Outstanding", clsdbo_CommissionRequisition.Total_Balance_Outstanding);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Balance_Outstanding", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Total_Credit_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Credit_Amount", clsdbo_CommissionRequisition.Total_Credit_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTotal_Credit_Amount", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Created_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_Date", clsdbo_CommissionRequisition.Created_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_Date", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Last_Modified_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_Date", clsdbo_CommissionRequisition.Last_Modified_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldLast_Modified_Date", DBNull.Value);
        }
        if (clsdbo_CommissionRequisition.Created_By != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_By", clsdbo_CommissionRequisition.Created_By);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCreated_By", DBNull.Value);
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

