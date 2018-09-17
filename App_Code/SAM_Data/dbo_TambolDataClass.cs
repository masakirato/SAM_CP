using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_TambolDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static List<dbo_TambolClass> SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[TambolSelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        List<dbo_TambolClass> item = new List<dbo_TambolClass>();


        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_TambolClass()
                    {
                        Sub_district = reader["Sub_district"] is DBNull ? null : reader["Sub_district"].ToString()
                            ,
                        District = reader["District"] is DBNull ? null : reader["District"].ToString()
                            ,
                        Province = reader["Province"] is DBNull ? null : reader["Province"].ToString()
                            ,
                        ID = System.Convert.ToInt32(reader["ID"])
                    });


                }

            }
            reader1.Close();
        }
        catch (SqlException ex)
        {
            return item;
        }
        finally
        {
            connection.Close();
        }
        return item;
    }

    public static List<dbo_TambolClass> Search(string District, string Province)
    {

        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[TambolSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(District))
        {
            selectCommand.Parameters.AddWithValue("@District", District);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@District", DBNull.Value);
        }


        if (!string.IsNullOrEmpty(Province))
        {
            selectCommand.Parameters.AddWithValue("@Province", Province);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Province", DBNull.Value);
        }


        DataTable dt = new DataTable();
        List<dbo_TambolClass> item = new List<dbo_TambolClass>();

        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);

                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_TambolClass()
                    {
                        Sub_district = reader["Sub_district"] is DBNull ? null : reader["Sub_district"].ToString()
                            ,
                        District = reader["District"] is DBNull ? null : reader["District"].ToString()
                            ,
                        Province = reader["Province"] is DBNull ? null : reader["Province"].ToString()
                            ,
                        ID = System.Convert.ToInt32(reader["ID"]),
                        Postal_ID = reader["Postal_ID"] is DBNull ? null : reader["Postal_ID"].ToString()
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

    public static dbo_TambolClass Select_Record(dbo_TambolClass clsdbo_TambolPara)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        dbo_TambolClass clsdbo_Tambol = new dbo_TambolClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[TambolSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@ID", clsdbo_TambolPara.ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Tambol.Sub_district = reader["Sub_district"] is DBNull ? null : reader["Sub_district"].ToString();
                clsdbo_Tambol.District = reader["District"] is DBNull ? null : reader["District"].ToString();
                clsdbo_Tambol.Province = reader["Province"] is DBNull ? null : reader["Province"].ToString();
                clsdbo_Tambol.ID = System.Convert.ToInt32(reader["ID"]);
            }
            else
            {
                clsdbo_Tambol = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            return clsdbo_Tambol;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Tambol;
    }
    [Obsolete]
    public static bool Add(dbo_TambolClass clsdbo_Tambol)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[TambolInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Tambol.Sub_district != null)
        {
            insertCommand.Parameters.AddWithValue("@Sub_district", clsdbo_Tambol.Sub_district);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Sub_district", DBNull.Value);
        }
        if (clsdbo_Tambol.District != null)
        {
            insertCommand.Parameters.AddWithValue("@District", clsdbo_Tambol.District);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@District", DBNull.Value);
        }
        if (clsdbo_Tambol.Province != null)
        {
            insertCommand.Parameters.AddWithValue("@Province", clsdbo_Tambol.Province);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Province", DBNull.Value);
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
            return false;
        }
        finally
        {
            connection.Close();
        }
    }
    [Obsolete]
    public static bool Update(dbo_TambolClass olddbo_TambolClass,
           dbo_TambolClass newdbo_TambolClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[TambolUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_TambolClass.Sub_district != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSub_district", newdbo_TambolClass.Sub_district);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSub_district", DBNull.Value);
        }
        if (newdbo_TambolClass.District != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDistrict", newdbo_TambolClass.District);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDistrict", DBNull.Value);
        }
        if (newdbo_TambolClass.Province != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProvince", newdbo_TambolClass.Province);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProvince", DBNull.Value);
        }
        if (olddbo_TambolClass.Sub_district != null)
        {
            updateCommand.Parameters.AddWithValue("@OldSub_district", olddbo_TambolClass.Sub_district);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldSub_district", DBNull.Value);
        }
        if (olddbo_TambolClass.District != null)
        {
            updateCommand.Parameters.AddWithValue("@OldDistrict", olddbo_TambolClass.District);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldDistrict", DBNull.Value);
        }
        if (olddbo_TambolClass.Province != null)
        {
            updateCommand.Parameters.AddWithValue("@OldProvince", olddbo_TambolClass.Province);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldProvince", DBNull.Value);
        }
        updateCommand.Parameters.AddWithValue("@OldID", olddbo_TambolClass.ID);
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
    [Obsolete]
    public static bool Delete(dbo_TambolClass clsdbo_Tambol)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[TambolDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Tambol.Sub_district != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldSub_district", clsdbo_Tambol.Sub_district);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldSub_district", DBNull.Value);
        }
        if (clsdbo_Tambol.District != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldDistrict", clsdbo_Tambol.District);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldDistrict", DBNull.Value);
        }
        if (clsdbo_Tambol.Province != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldProvince", clsdbo_Tambol.Province);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldProvince", DBNull.Value);
        }
        deleteCommand.Parameters.AddWithValue("@OldID", clsdbo_Tambol.ID);
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

