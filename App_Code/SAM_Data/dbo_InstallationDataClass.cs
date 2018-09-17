using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_InstallationDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static List<dbo_InstallationClass> SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[InstallationSelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        List<dbo_InstallationClass> item = new List<dbo_InstallationClass>();

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
                    item.Add(new dbo_InstallationClass()
                    {
                        Installation_ID = reader["Installation_ID"] is DBNull ? null : reader["Installation_ID"].ToString(),
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Installation_Detail = reader["Installation_Detail"] is DBNull ? null : reader["Installation_Detail"].ToString(),
                        Installation_Type = reader["Installation_Type"] is DBNull ? null : reader["Installation_Type"].ToString(),
                        Description = reader["Description"] is DBNull ? null : reader["Description"].ToString(),
                        Installation_Amount = reader["Installation_Amount"] is DBNull ? null : (Decimal?)reader["Installation_Amount"],
                        Transaction_Date = reader["Transaction_Date"] is DBNull ? null : (DateTime?)reader["Transaction_Date"],
                        Due_Date = reader["Due_Date"] is DBNull ? null : (DateTime?)reader["Due_Date"],
                        Balance_Amount = reader["Balance_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Amount"],
                        Payment_Amount = reader["Payment_Amount"] is DBNull ? null : (Decimal?)reader["Payment_Amount"]
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

    public static List<dbo_InstallationClass> Search(string User_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[InstallationSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (!string.IsNullOrEmpty(User_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }

        List<dbo_InstallationClass> item = new List<dbo_InstallationClass>();
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
                    item.Add(new dbo_InstallationClass()
                    {
                        Installation_ID = reader["Installation_ID"] is DBNull ? null : reader["Installation_ID"].ToString(),
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString(),
                        Installation_Detail = reader["Installation_Detail"] is DBNull ? null : reader["Installation_Detail"].ToString(),
                        Installation_Type = reader["Installation_Type"] is DBNull ? null : reader["Installation_Type"].ToString(),
                        Description = reader["Description"] is DBNull ? null : reader["Description"].ToString(),
                        Installation_Amount = reader["Installation_Amount"] is DBNull ? null : (Decimal?)reader["Installation_Amount"],
                        Transaction_Date = reader["Transaction_Date"] is DBNull ? null : (DateTime?)reader["Transaction_Date"],
                        Due_Date = reader["Due_Date"] is DBNull ? null : (DateTime?)reader["Due_Date"],
                        Balance_Amount = reader["Balance_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Amount"],
                        Payment_Amount = reader["Payment_Amount"] is DBNull ? null : (Decimal?)reader["Payment_Amount"]
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

    public static dbo_InstallationClass Select_Record(dbo_InstallationClass clsdbo_InstallationPara)
    {
        dbo_InstallationClass clsdbo_Installation = new dbo_InstallationClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[InstallationSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Installation_ID", clsdbo_InstallationPara.Installation_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Installation.Installation_ID = reader["Installation_ID"] is DBNull ? null : reader["Installation_ID"].ToString();
                clsdbo_Installation.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_Installation.Installation_Detail = reader["Installation_Detail"] is DBNull ? null : reader["Installation_Detail"].ToString();
                clsdbo_Installation.Installation_Type = reader["Installation_Type"] is DBNull ? null : reader["Installation_Type"].ToString();
                clsdbo_Installation.Description = reader["Description"] is DBNull ? null : reader["Description"].ToString();
                clsdbo_Installation.Installation_Amount = reader["Installation_Amount"] is DBNull ? null : (Decimal?)reader["Installation_Amount"];
                clsdbo_Installation.Transaction_Date = reader["Transaction_Date"] is DBNull ? null : (DateTime?)reader["Transaction_Date"];
                clsdbo_Installation.Due_Date = reader["Due_Date"] is DBNull ? null : (DateTime?)reader["Due_Date"];
                clsdbo_Installation.Balance_Amount = reader["Balance_Amount"] is DBNull ? null : (Decimal?)reader["Balance_Amount"];
                clsdbo_Installation.Payment_Amount = reader["Payment_Amount"] is DBNull ? null : (Decimal?)reader["Payment_Amount"];
            }
            else
            {
                clsdbo_Installation = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Installation;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Installation;
    }

    public static bool Add(dbo_InstallationClass clsdbo_Installation, string Created_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[InstallationInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Installation.Installation_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Installation_ID", clsdbo_Installation.Installation_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Installation_ID", DBNull.Value);
        }
        if (clsdbo_Installation.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_Installation.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (clsdbo_Installation.Installation_Detail != null)
        {
            insertCommand.Parameters.AddWithValue("@Installation_Detail", clsdbo_Installation.Installation_Detail);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Installation_Detail", DBNull.Value);
        }
        if (clsdbo_Installation.Installation_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@Installation_Type", clsdbo_Installation.Installation_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Installation_Type", DBNull.Value);
        }
        if (clsdbo_Installation.Description != null)
        {
            insertCommand.Parameters.AddWithValue("@Description", clsdbo_Installation.Description);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Description", DBNull.Value);
        }
        if (clsdbo_Installation.Installation_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Installation_Amount", clsdbo_Installation.Installation_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Installation_Amount", DBNull.Value);
        }
        if (clsdbo_Installation.Transaction_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Transaction_Date", clsdbo_Installation.Transaction_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Transaction_Date", DBNull.Value);
        }
        if (clsdbo_Installation.Due_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Due_Date", clsdbo_Installation.Due_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Due_Date", DBNull.Value);
        }
        if (clsdbo_Installation.Balance_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Balance_Amount", clsdbo_Installation.Balance_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Balance_Amount", DBNull.Value);
        }
        if (clsdbo_Installation.Payment_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Payment_Amount", clsdbo_Installation.Payment_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Payment_Amount", DBNull.Value);
        }
        if (Created_By != null)
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

    public static bool Update(dbo_InstallationClass newdbo_InstallationClass, string Last_Modified_By)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[InstallationUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_InstallationClass.Installation_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInstallation_ID", newdbo_InstallationClass.Installation_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInstallation_ID", DBNull.Value);
        }
        if (newdbo_InstallationClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", newdbo_InstallationClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", DBNull.Value);
        }
        if (newdbo_InstallationClass.Installation_Detail != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInstallation_Detail", newdbo_InstallationClass.Installation_Detail);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInstallation_Detail", DBNull.Value);
        }
        if (newdbo_InstallationClass.Installation_Type != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInstallation_Type", newdbo_InstallationClass.Installation_Type);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInstallation_Type", DBNull.Value);
        }
        if (newdbo_InstallationClass.Description != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDescription", newdbo_InstallationClass.Description);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDescription", DBNull.Value);
        }
        if (newdbo_InstallationClass.Installation_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewInstallation_Amount", newdbo_InstallationClass.Installation_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInstallation_Amount", DBNull.Value);
        }
        if (newdbo_InstallationClass.Transaction_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewTransaction_Date", newdbo_InstallationClass.Transaction_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTransaction_Date", DBNull.Value);
        }
        if (newdbo_InstallationClass.Due_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewDue_Date", newdbo_InstallationClass.Due_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDue_Date", DBNull.Value);
        }
        if (newdbo_InstallationClass.Balance_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewBalance_Amount", newdbo_InstallationClass.Balance_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBalance_Amount", DBNull.Value);
        }
        if (newdbo_InstallationClass.Payment_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Amount", newdbo_InstallationClass.Payment_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPayment_Amount", DBNull.Value);
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

    public static bool Delete(dbo_InstallationClass clsdbo_Installation)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[InstallationDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Installation.Installation_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldInstallation_ID", clsdbo_Installation.Installation_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInstallation_ID", DBNull.Value);
        }
        /*if (clsdbo_Installation.User_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", clsdbo_Installation.User_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
        }
        if (clsdbo_Installation.Installation_Detail != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldInstallation_Detail", clsdbo_Installation.Installation_Detail);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInstallation_Detail", DBNull.Value);
        }
        if (clsdbo_Installation.Installation_Type != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldInstallation_Type", clsdbo_Installation.Installation_Type);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInstallation_Type", DBNull.Value);
        }
        if (clsdbo_Installation.Installation_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldInstallation_Amount", clsdbo_Installation.Installation_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldInstallation_Amount", DBNull.Value);
        }
        if (clsdbo_Installation.Transaction_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldTransaction_Date", clsdbo_Installation.Transaction_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldTransaction_Date", DBNull.Value);
        }
        if (clsdbo_Installation.Due_Date.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldDue_Date", clsdbo_Installation.Due_Date);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldDue_Date", DBNull.Value);
        }
        if (clsdbo_Installation.Balance_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldBalance_Amount", clsdbo_Installation.Balance_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBalance_Amount", DBNull.Value);
        }
        if (clsdbo_Installation.Payment_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPayment_Amount", clsdbo_Installation.Payment_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPayment_Amount", DBNull.Value);
        }*/
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

