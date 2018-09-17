using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
public class dbo_PriceGroupDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static Dictionary<string, string> GetAssignPriceGroup(string Price_Group_Id)
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetAssignPriceGroup]";

        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Price_Group_Id))
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", Price_Group_Id);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }


        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);

                // unit.Add(string.Empty, "==ระบุ==");

                foreach (DataRow row in dt.Rows)
                {
                    unit.Add(row["CV_Code"].ToString(), row["CV_Code"].ToString() + " " + row["Prefix_ID"].ToString());
                }
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return unit;
        }
        finally
        {
            connection.Close();
        }
        return unit;

    }


    public static Dictionary<string, string> GetAssignPriceGroupSP(String Price_Group_Id, String CV_Code)
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetAssignPriceGroupSP]";

        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Price_Group_Id))
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", Price_Group_Id);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);

                // unit.Add(string.Empty, "==ระบุ==");

                foreach (DataRow row in dt.Rows)
                {
                    unit.Add(row["User_ID"].ToString(), row["FullName"].ToString());
                }
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return unit;
        }
        finally
        {
            connection.Close();
        }
        return unit;

    }




    public static Dictionary<string, string> GetPriceGroup()
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetPriceGroup]";
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
                unit.Add(string.Empty, "==ระบุ==");
                foreach (DataRow row in dt.Rows)
                {
                    unit.Add(row["Price_Group_ID"].ToString(), row["Price_Group_Name"].ToString());
                }
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return unit;
        }
        finally
        {
            connection.Close();
        }
        return unit;
    }

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[PriceGroupSelectAll]";
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



    public static List<dbo_PriceGroupClass> Search(string Price_Group_Name, string Price_Group_Type)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "PriceGroupSearch";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        if (!string.IsNullOrEmpty(Price_Group_Name))
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_Name", Price_Group_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_Name", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Price_Group_Type))
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_Type", Price_Group_Type);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price_Group_Type", DBNull.Value);
        }

        List<dbo_PriceGroupClass> item = new List<dbo_PriceGroupClass>();

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
                    item.Add(new dbo_PriceGroupClass()
                    {
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString(),
                        Price_Group_Name = reader["Price_Group_Name"] is DBNull ? null : reader["Price_Group_Name"].ToString(),
                        Price_Group_Type = reader["Price_Group_Type"] is DBNull ? null : (reader["Price_Group_Type"].ToString() == "0" ? "เอเยนต์" : "สาว"),
                        StandardPrice = reader["StandardPrice"] is DBNull ? false : (Boolean)reader["StandardPrice"],
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString()
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

    public static dbo_PriceGroupClass Select_Record(string Price_Group_ID)
    {
        dbo_PriceGroupClass clsdbo_PriceGroup = new dbo_PriceGroupClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "PriceGroupSelect";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Price_Group_ID", Price_Group_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_PriceGroup.Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString();
                clsdbo_PriceGroup.Price_Group_Name = reader["Price_Group_Name"] is DBNull ? null : reader["Price_Group_Name"].ToString();
                clsdbo_PriceGroup.Price_Group_Type = reader["Price_Group_Type"] is DBNull ? null : (reader["Price_Group_Type"].ToString() == "0" ? "เอเยนต์" : "สาว");
                clsdbo_PriceGroup.StandardPrice = reader["StandardPrice"] is DBNull ? false : (Boolean)reader["StandardPrice"];
            }
            else
            {
                clsdbo_PriceGroup = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_PriceGroup;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_PriceGroup;
    }

    public static bool Add(dbo_PriceGroupClass clsdbo_PriceGroup, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[PriceGroupInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_PriceGroup.Price_Group_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", clsdbo_PriceGroup.Price_Group_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }
        if (clsdbo_PriceGroup.Price_Group_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_Name", clsdbo_PriceGroup.Price_Group_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_Name", DBNull.Value);
        }
        if (clsdbo_PriceGroup.Price_Group_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_Type", clsdbo_PriceGroup.Price_Group_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_Type", DBNull.Value);
        }


        if (clsdbo_PriceGroup.StandardPrice != null)
        {
            insertCommand.Parameters.AddWithValue("@StandardPrice", clsdbo_PriceGroup.StandardPrice);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@StandardPrice", DBNull.Value);
        }

        if (Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }

        if (Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", Created_By);
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

    public static bool Update(dbo_PriceGroupClass newdbo_PriceGroupClass, String Last_Modified_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "PriceGroupUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;

        if (newdbo_PriceGroupClass.Price_Group_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", newdbo_PriceGroupClass.Price_Group_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", DBNull.Value);
        }

        if (newdbo_PriceGroupClass.Price_Group_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_Name", newdbo_PriceGroupClass.Price_Group_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_Name", DBNull.Value);
        }

        if (newdbo_PriceGroupClass.StandardPrice != null)
        {
            updateCommand.Parameters.AddWithValue("@StandardPrice", newdbo_PriceGroupClass.StandardPrice);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@StandardPrice", DBNull.Value);
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

    public static bool Delete(String Price_Group_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[PriceGroupDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;

        if (Price_Group_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldPrice_Group_ID", Price_Group_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPrice_Group_ID", DBNull.Value);
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

