using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;

public class dbo_RequisitionClearingDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static List<dbo_RequisitionClass> GetRequisitionWithClearing()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetRequisitionWithClearing]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        List<dbo_RequisitionClass> item = new List<dbo_RequisitionClass>();

        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_RequisitionClass()
                    {
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"],
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        SP_Name = reader["Full_Name"] is DBNull ? null : reader["Full_Name"].ToString()
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

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[RequisitionClearingSelectAll]";
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

    public static List<dbo_RequisitionClearingClass> Search(string Clearing_No, string Requisition_No, string SP_ID
        , DateTime? Requisition_Begin_Date, DateTime? Requisition_Begin_End, DateTime? Clearing_Begin_Date, DateTime? Clearing_Begin_End, string CV_Code
        )
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionClearingSearch]";
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
        if (!string.IsNullOrEmpty(Requisition_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(SP_ID))
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", SP_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP_ID", DBNull.Value);
        }

        if (Requisition_Begin_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Begin_Date", Requisition_Begin_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Begin_Date", DateTime.Now.AddDays(-30));
        }
        if (Requisition_Begin_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Begin_End", Requisition_Begin_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Begin_End", DateTime.Now.AddDays(30));
        }
        if (Clearing_Begin_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Begin_Date", Clearing_Begin_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Begin_Date", DateTime.Now.AddDays(-30));
        }
        if (Clearing_Begin_End.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Begin_End", Clearing_Begin_End.Value.AddMinutes(1439));
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Clearing_Begin_End", DateTime.Now.AddDays(30));
        }


        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }



        List<dbo_RequisitionClearingClass> item = new List<dbo_RequisitionClearingClass>();


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
                    item.Add(new dbo_RequisitionClearingClass()
                    {
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"],
                        Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"]
                        
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

    public static dbo_RequisitionClearingClass Select_Record(String Clearing_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_RequisitionClearingClass clsdbo_RequisitionClearing = new dbo_RequisitionClearingClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionClearingSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);

        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_RequisitionClearing.Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString();
                clsdbo_RequisitionClearing.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
                clsdbo_RequisitionClearing.Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"];
                clsdbo_RequisitionClearing.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
            }
            else
            {
                clsdbo_RequisitionClearing = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_RequisitionClearing;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_RequisitionClearing;
    }

    public static List<dbo_RequisitionClearingClass> getrq(string Clearing_No)
    {
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionClearingSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Clearing_No", Clearing_No);
        DataTable dt = new DataTable();

        List<dbo_RequisitionClearingClass> item = new List<dbo_RequisitionClearingClass>();

        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_RequisitionClearingClass()
                    {
                        
                   
                   Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                    
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

    public static List<dbo_RequisitionClearingClass> SearchBySPID(string SP_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionClearingSearchBySPID]";
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

        List<dbo_RequisitionClearingClass> item = new List<dbo_RequisitionClearingClass>();


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
                    item.Add(new dbo_RequisitionClearingClass()
                    {
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        //User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        //Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        //Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"],
                        //Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"]
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

    public static List<dbo_RequisitionClearingClass> SearchByReqNo(string Req_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[RequisitionClearingSearchByReqNo]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!string.IsNullOrEmpty(Req_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Req_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }

        List<dbo_RequisitionClearingClass> item = new List<dbo_RequisitionClearingClass>();


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
                    item.Add(new dbo_RequisitionClearingClass()
                    {
                        Clearing_No = reader["Clearing_No"] is DBNull ? null : reader["Clearing_No"].ToString(),
                        //User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString(),
                        //Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                        //Requisition_Date = reader["Requisition_Date"] is DBNull ? null : (DateTime?)reader["Requisition_Date"],
                        //Clearing_Date = reader["Clearing_Date"] is DBNull ? null : (DateTime?)reader["Clearing_Date"]
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

    public static bool Add(dbo_RequisitionClearingClass clsdbo_RequisitionClearing)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[RequisitionClearingInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_RequisitionClearing.Clearing_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", clsdbo_RequisitionClearing.Clearing_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Clearing_No", DBNull.Value);
        }
        if (clsdbo_RequisitionClearing.Requisition_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_RequisitionClearing.Requisition_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (clsdbo_RequisitionClearing.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_RequisitionClearing.User_ID);
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

    public static bool Update(dbo_RequisitionClearingClass olddbo_RequisitionClearingClass,
           dbo_RequisitionClearingClass newdbo_RequisitionClearingClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[RequisitionClearingUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_RequisitionClearingClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", newdbo_RequisitionClearingClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewClearing_No", DBNull.Value);
        }
        if (newdbo_RequisitionClearingClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", newdbo_RequisitionClearingClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", DBNull.Value);
        }
        if (olddbo_RequisitionClearingClass.Clearing_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldClearing_No", olddbo_RequisitionClearingClass.Clearing_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldClearing_No", DBNull.Value);
        }
        if (olddbo_RequisitionClearingClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@OldRequisition_No", olddbo_RequisitionClearingClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldRequisition_No", DBNull.Value);
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

    public static bool Delete(String Clearing_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[RequisitionClearingDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (!string.IsNullOrEmpty(Clearing_No))
        {
            deleteCommand.Parameters.AddWithValue("@OldClearing_No", Clearing_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldClearing_No", DBNull.Value);
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

