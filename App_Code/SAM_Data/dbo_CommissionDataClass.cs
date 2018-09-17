using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
public class dbo_CommissionDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionSelectAll]";
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

    public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Requisition No")
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (sField == "Commission")
        {
            selectCommand.Parameters.AddWithValue("@Commission", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Commission", DBNull.Value);
        }
        if (sField == "Point")
        {
            selectCommand.Parameters.AddWithValue("@Point", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Point", DBNull.Value);
        }
        if (sField == "Commission Requisition Status")
        {
            selectCommand.Parameters.AddWithValue("@Commission_Requisition_Status", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Commission_Requisition_Status", DBNull.Value);
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

    /*
    public static dbo_CommissionClass Select_Record(String Requisition_No)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_CommissionClass clsdbo_Commission = new dbo_CommissionClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[CommissionSelect]";
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
                clsdbo_Commission.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
                clsdbo_Commission.Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"];
                clsdbo_Commission.Point = reader["Point"] is DBNull ? null : (Int16?)reader["Point"];
                clsdbo_Commission.Commission_Requisition_Status = reader["Commission_Requisition_Status"] is DBNull ? null : (Byte?)reader["Commission_Requisition_Status"];
            }
            else
            {
                clsdbo_Commission = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Commission;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Commission;
    }
    */
    public static List<dbo_CommissionClass> Select_Record(String Requisition_No)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);

        List<dbo_CommissionClass> item = new List<dbo_CommissionClass>();
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
                    item.Add(new dbo_CommissionClass()
                    {
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"],
                        Point = reader["Point"] is DBNull ? null : (Int16?)reader["Point"],
                        Commission_Requisition_Status = reader["Commission_Requisition_Status"] is DBNull ? null : (Byte?)reader["Commission_Requisition_Status"],
                        Created_Date = reader["Created_Date"] is DBNull ? null : (DateTime?)reader["Created_Date"],
                        Commission_Balance_Outstanding = reader["Commission_Balance_Outstanding"] is DBNull ? null : (Decimal?)reader["Commission_Balance_Outstanding"],
                        
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

    public static dbo_CommissionClass SumPointBySP(String User_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionSumPointBySP]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@User_ID", User_ID);

       dbo_CommissionClass item = new dbo_CommissionClass();
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
                    item.Commission = reader["Point"] is DBNull ? null : (Int32?)reader["Point"];
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

    public static dbo_CommissionClass SumBalancetBySP(String User_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[CommissionSumBalanceBySP]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@User_ID", User_ID);

        dbo_CommissionClass item = new dbo_CommissionClass();
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
                    item.Commission = reader["Commission"] is DBNull ? null : (Decimal?)reader["Commission"];
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

    public static bool Add(dbo_CommissionClass clsdbo_Commission, String Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[CommissionInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Commission.Requisition_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_Commission.Requisition_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (clsdbo_Commission.Commission.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Commission", clsdbo_Commission.Commission);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Commission", DBNull.Value);
        }
        if (clsdbo_Commission.Point.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Point", clsdbo_Commission.Point);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Point", DBNull.Value);
        }
        if (clsdbo_Commission.Commission_Requisition_Status.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Status", clsdbo_Commission.Commission_Requisition_Status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Status", DBNull.Value);
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

    public static bool Update(dbo_CommissionClass newdbo_CommissionClass, String Last_Modified_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[CommissionUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_CommissionClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", newdbo_CommissionClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", DBNull.Value);
        }
        if (newdbo_CommissionClass.Commission.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCommission", newdbo_CommissionClass.Commission);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCommission", DBNull.Value);
        }
        if (newdbo_CommissionClass.Point.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", newdbo_CommissionClass.Point);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPoint", DBNull.Value);
        }
        if (newdbo_CommissionClass.Commission_Requisition_Status.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Status", newdbo_CommissionClass.Commission_Requisition_Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Status", DBNull.Value);
        }
        if (newdbo_CommissionClass.Commission_Balance_Outstanding.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@Commission_Balance_Outstanding", newdbo_CommissionClass.Commission_Balance_Outstanding);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Commission_Balance_Outstanding", DBNull.Value);
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Delete(dbo_CommissionClass clsdbo_Commission)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[CommissionDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Commission.Requisition_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_Commission.Requisition_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }


        /*
        if (clsdbo_Commission.Commission.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission", clsdbo_Commission.Commission);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission", DBNull.Value);
        }
        if (clsdbo_Commission.Point.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPoint", clsdbo_Commission.Point);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPoint", DBNull.Value);
        }
        if (clsdbo_Commission.Commission_Requisition_Status.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_Requisition_Status", clsdbo_Commission.Commission_Requisition_Status);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCommission_Requisition_Status", DBNull.Value);
        }
        */



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

