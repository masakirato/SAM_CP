using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;


public class dbo_CommissionRequisitionDtlDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public static DataTable SelectAll()
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionRequisitionDtlSelectAll]";
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

    public static List<dbo_CommissionRequisitionDtlClass> Search(String CV_Code)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CommissionRequisitionDtlSearch]";
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

        List<dbo_CommissionRequisitionDtlClass> item = new List<dbo_CommissionRequisitionDtlClass>();
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
                    item.Add(new dbo_CommissionRequisitionDtlClass()
                    {
                        // Commission_requisition_no = reader["Commission_requisition_no"] is DBNull ? null : reader["Commission_requisition_no"].ToString(),

                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                         

                        //Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"],
                        //Requisition_Amount = reader["Requisition_Amount"] is DBNull ? null : (Decimal?)reader["Requisition_Amount"],

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

    public static dbo_CommissionRequisitionDtlClass Select_Record(dbo_CommissionRequisitionDtlClass clsdbo_CommissionRequisitionDtlPara)
    {
        dbo_CommissionRequisitionDtlClass clsdbo_CommissionRequisitionDtl = new dbo_CommissionRequisitionDtlClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionRequisitionDtlSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Commission_requisition_no", clsdbo_CommissionRequisitionDtlPara.Commission_requisition_no);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_CommissionRequisitionDtl.Commission_requisition_no = reader["Commission_requisition_no"] is DBNull ? null : reader["Commission_requisition_no"].ToString();
                clsdbo_CommissionRequisitionDtl.Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString();
                clsdbo_CommissionRequisitionDtl.Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"];
                clsdbo_CommissionRequisitionDtl.Requisition_Amount = reader["Requisition_Amount"] is DBNull ? null : (Decimal?)reader["Requisition_Amount"];
            }
            else
            {
                clsdbo_CommissionRequisitionDtl = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            return clsdbo_CommissionRequisitionDtl;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_CommissionRequisitionDtl;
    }

    public static bool Add(dbo_CommissionRequisitionDtlClass clsdbo_CommissionRequisitionDtl, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[CommissionRequisitionDtlInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CommissionRequisitionDtl.Commission_requisition_no != null)
        {
            insertCommand.Parameters.AddWithValue("@Commission_requisition_no", clsdbo_CommissionRequisitionDtl.Commission_requisition_no);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Commission_requisition_no", DBNull.Value);
        }
        if (clsdbo_CommissionRequisitionDtl.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_CommissionRequisitionDtl.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (clsdbo_CommissionRequisitionDtl.Commission.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Commission", clsdbo_CommissionRequisitionDtl.Commission);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Commission", DBNull.Value);
        }
        if (clsdbo_CommissionRequisitionDtl.Requisition_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Amount", clsdbo_CommissionRequisitionDtl.Requisition_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Amount", DBNull.Value);
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

    public static bool Update(dbo_CommissionRequisitionDtlClass olddbo_CommissionRequisitionDtlClass,
           dbo_CommissionRequisitionDtlClass newdbo_CommissionRequisitionDtlClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[CommissionRequisitionDtlUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CommissionRequisitionDtlClass.Commission_requisition_no != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCommission_requisition_no", newdbo_CommissionRequisitionDtlClass.Commission_requisition_no);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCommission_requisition_no", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionDtlClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", newdbo_CommissionRequisitionDtlClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionDtlClass.Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCommission", newdbo_CommissionRequisitionDtlClass.Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCommission", DBNull.Value);
        }
        if (newdbo_CommissionRequisitionDtlClass.Requisition_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Amount", newdbo_CommissionRequisitionDtlClass.Requisition_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Amount", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionDtlClass.Commission_requisition_no != null)
        {
            updateCommand.Parameters.AddWithValue("@OldCommission_requisition_no", olddbo_CommissionRequisitionDtlClass.Commission_requisition_no);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCommission_requisition_no", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionDtlClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldClearing_No", olddbo_CommissionRequisitionDtlClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldClearing_No", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionDtlClass.Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldCommission", olddbo_CommissionRequisitionDtlClass.Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldCommission", DBNull.Value);
        }
        if (olddbo_CommissionRequisitionDtlClass.Requisition_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@OldRequisition_Amount", olddbo_CommissionRequisitionDtlClass.Requisition_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldRequisition_Amount", DBNull.Value);
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

    public static bool Delete(dbo_CommissionRequisitionDtlClass clsdbo_CommissionRequisitionDtl)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[CommissionRequisitionDtlDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_CommissionRequisitionDtl.Commission_requisition_no != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_requisition_no", clsdbo_CommissionRequisitionDtl.Commission_requisition_no);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_requisition_no", DBNull.Value);
        }
        if (clsdbo_CommissionRequisitionDtl.Clearing_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldClearing_No", clsdbo_CommissionRequisitionDtl.Clearing_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldClearing_No", DBNull.Value);
        }
        if (clsdbo_CommissionRequisitionDtl.Commission.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission", clsdbo_CommissionRequisitionDtl.Commission);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission", DBNull.Value);
        }
        if (clsdbo_CommissionRequisitionDtl.Requisition_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Amount", clsdbo_CommissionRequisitionDtl.Requisition_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Amount", DBNull.Value);
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

