using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using log4net;
public class dbo_ItemDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #region Dev by A
    public static Dictionary<string, string> GetDropDown(string key)
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetItemValue";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Mode", "GetItemValue");
        selectCommand.Parameters.AddWithValue("@Item_ID", key);

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
                    unit.Add(row["Item_Value_ID"].ToString().Trim(), row["Item_Value"].ToString().Trim());
                }
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return null;
        }
        finally
        {
            connection.Close();
        }
        return unit;
    }

    public static Dictionary<string, string> GetDropDown_Report(string key)
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetItemValue";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Mode", "GetItemValue");
        selectCommand.Parameters.AddWithValue("@Item_ID", key);

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);
                unit.Add(string.Empty, "เลือกทั้งหมด");
                foreach (DataRow row in dt.Rows)
                {
                    unit.Add(row["Item_Value_ID"].ToString().Trim(), row["Item_Value"].ToString().Trim());
                }
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return null;
        }
        finally
        {
            connection.Close();
        }
        return unit;
    }







    public static Dictionary<string, string> GetRoleDropDown()
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetRole";
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
                    unit.Add(row["KEY"].ToString().Trim(), row["VALUE"].ToString().Trim());
                }

            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return null;
        }
        finally
        {
            connection.Close();
        }
        return unit;
    }

    public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ItemSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Item I D")
        {
            selectCommand.Parameters.AddWithValue("@Item_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Item_ID", DBNull.Value);
        }
        if (sField == "Item Name")
        {
            selectCommand.Parameters.AddWithValue("@Item_Name", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);
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

    public static dbo_ItemClass Select_Record(dbo_ItemClass clsdbo_ItemPara)
    {
        dbo_ItemClass clsdbo_Item = new dbo_ItemClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[ItemSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Item_ID", clsdbo_ItemPara.Item_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Item.Item_ID = reader["Item_ID"] is DBNull ? null : reader["Item_ID"].ToString();
                clsdbo_Item.Item_Name = reader["Item_Name"] is DBNull ? null : reader["Item_Name"].ToString();
            }
            else
            {
                clsdbo_Item = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Item;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Item;
    }

    public static bool Add(dbo_ItemClass clsdbo_Item)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[ItemInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Item.Item_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Item_ID", clsdbo_Item.Item_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Item_ID", DBNull.Value);
        }
        if (clsdbo_Item.Item_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Item_Name", clsdbo_Item.Item_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Item_Name", DBNull.Value);
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

    public static bool Update(dbo_ItemClass olddbo_ItemClass,
           dbo_ItemClass newdbo_ItemClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[dbo].[ItemUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_ItemClass.Item_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewItem_ID", newdbo_ItemClass.Item_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewItem_ID", DBNull.Value);
        }
        if (newdbo_ItemClass.Item_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewItem_Name", newdbo_ItemClass.Item_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewItem_Name", DBNull.Value);
        }
        if (olddbo_ItemClass.Item_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@OldItem_ID", olddbo_ItemClass.Item_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldItem_ID", DBNull.Value);
        }
        if (olddbo_ItemClass.Item_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@OldItem_Name", olddbo_ItemClass.Item_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@OldItem_Name", DBNull.Value);
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

    public static bool Delete(dbo_ItemClass clsdbo_Item)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[ItemDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Item.Item_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldItem_ID", clsdbo_Item.Item_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldItem_ID", DBNull.Value);
        }
        if (clsdbo_Item.Item_Name != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldItem_Name", clsdbo_Item.Item_Name);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldItem_Name", DBNull.Value);
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
    #endregion

    #region Newwwwwwwwwwww
    public static Dictionary<string, string> GetDropDown_New(string key)
    {
        Dictionary<string, string> unit = new Dictionary<string, string>();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetItemValue";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Mode", "GetItemValue");
        selectCommand.Parameters.AddWithValue("@Item_ID", key);

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);
                unit.Add("0", "==ระบุ==");
                foreach (DataRow row in dt.Rows)
                {
                    unit.Add(row["Item_Value_ID"].ToString().Trim(), row["Item_Value"].ToString().Trim());
                }
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return null;
        }
        finally
        {
            connection.Close();
        }
        return unit;
    }

    public static List<dbo_ItemClass> GetItem_New()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetItemValue";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Mode", "GetItem");
        DataTable dt = new DataTable();

        List<dbo_ItemClass> item = new List<dbo_ItemClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);


                foreach (DataRow reader in dt.Rows)
                {

                    item.Add(new dbo_ItemClass()
                    {
                        Item_ID = reader["Item_ID"] is DBNull ? null : reader["Item_ID"].ToString(),
                        Item_Name = reader["Item_Name"] is DBNull ? null : reader["Item_Name"].ToString()
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

    public static List<dbo_ItemValueClass> GetItemValue_New(string itemID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "GetItemValue";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Mode", "GetItemValue");
        selectCommand.Parameters.AddWithValue("@Item_ID", itemID);
        DataTable dt = new DataTable();

        List<dbo_ItemValueClass> item = new List<dbo_ItemValueClass>();
        try
        {
            connection.Open();
            SqlDataReader reader1 = selectCommand.ExecuteReader();
            if (reader1.HasRows)
            {
                dt.Load(reader1);
                foreach (DataRow reader in dt.Rows)
                {
                    item.Add(new dbo_ItemValueClass()
                    {
                        Item_ID = reader["Item_ID"] is DBNull ? null : reader["Item_ID"].ToString(),
                        Item_Value_ID = reader["Item_Value_ID"] is DBNull ? null : reader["Item_Value_ID"].ToString(),
                        Item_Value_Name = reader["Item_Value"] is DBNull ? null : reader["Item_Value"].ToString()
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

    public static Boolean InsertItem_New(dbo_ItemClass clsItem, string userName)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "InsertItemValue";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "InsItem");
        insertCommand.Parameters.AddWithValue("@InsertBy", userName);
        insertCommand.Parameters.AddWithValue("@InsertParam", clsItem.Item_Name);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
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

    public static Boolean InsertItemValue_New(dbo_ItemValueClass clsItemValue, string userName)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "InsertItemValue";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "InsItemValue");
        insertCommand.Parameters.AddWithValue("@Item_ID", clsItemValue.Item_ID);
        insertCommand.Parameters.AddWithValue("@InsertBy", userName);
        insertCommand.Parameters.AddWithValue("@InsertParam", clsItemValue.Item_Value_Name);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
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

    public static Boolean UpdateItem_New(dbo_ItemClass clsItem, string userName)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "UpdateItemValue";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "UpdItem");
        insertCommand.Parameters.AddWithValue("@Item_ID", clsItem.Item_ID);
        insertCommand.Parameters.AddWithValue("@UpdateBY", userName);
        insertCommand.Parameters.AddWithValue("@UpdateParam", clsItem.Item_Name);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
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

    public static Boolean UpdateItemValue_New(dbo_ItemValueClass clsItemValue, string userName)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "UpdateItemValue";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "UpdItemValue");
        insertCommand.Parameters.AddWithValue("@Item_Value_ID", clsItemValue.Item_Value_ID);
        insertCommand.Parameters.AddWithValue("@UpdateBY", userName);
        insertCommand.Parameters.AddWithValue("@UpdateParam", clsItemValue.Item_Value_Name);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
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

    public static Boolean DeleteItem_New(string itemID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "DeleteItemValue";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "DelItem");
        insertCommand.Parameters.AddWithValue("@Item_ID", itemID);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
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

    public static Boolean DeleteItemValue_New(string itemValueID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "DeleteItemValue";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.AddWithValue("@Mode", "DelItemValue");
        insertCommand.Parameters.AddWithValue("@Item_Value_ID", itemValueID);
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
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
    #endregion

    #region New Warut Edit
    public static DataSet GetItem()
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();

        try
        {
            connDB.openConnection();
            string sqlStr = "GetItemValue";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[1];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetItem";
            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);

            if (ds.Tables[0].Rows.Count == 0)
            {
                DataSet dsItem = new DataSet();
                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("Item_ID");
                dtItem.Columns.Add("Item_Name");

                DataRow drItem = dtItem.NewRow();
                dtItem.Rows.Add(drItem);
                dsItem.Tables.Add(dtItem);

                ds = dsItem;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return ds;
    }

    public static DataSet GetItemValue(string itemID)
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();

        try
        {
            connDB.openConnection();
            connDB.openConnection();
            string sqlStr = "GetItemValue";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[2];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetItemValue";

            spParameter[1] = new SqlParameter("@Item_ID", SqlDbType.VarChar, 2);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = itemID;
            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);

            if (ds.Tables[0].Rows.Count == 0)
            {
                DataSet dsItemValue = new DataSet();
                DataTable dtItemValue = new DataTable();
                dtItemValue.Columns.Add("Item_Value_ID");
                dtItemValue.Columns.Add("Item_Value");

                DataRow drItem = dtItemValue.NewRow();
                dtItemValue.Rows.Add(drItem);
                dsItemValue.Tables.Add(dtItemValue);

                ds = dsItemValue;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return ds;
    }

    public static Boolean InsertItem(string itemName, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "InsertItemValue";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[3];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "InsItem";

            spParameter[1] = new SqlParameter("@InsertBy", SqlDbType.VarChar, 10);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = userName;

            spParameter[2] = new SqlParameter("@InsertParam", SqlDbType.VarChar, 100);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = itemName;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean InsertItemValue(string itemID, string itemValue, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "InsertItemValue";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[4];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "InsItemValue";

            spParameter[1] = new SqlParameter("@Item_ID", SqlDbType.VarChar, 2);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = itemID;

            spParameter[2] = new SqlParameter("@InsertBy", SqlDbType.VarChar, 10);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = userName;

            spParameter[3] = new SqlParameter("@InsertParam", SqlDbType.VarChar, 100);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = itemValue;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }
        return status;
    }

    public static Boolean UpdateItem(string itemID, string itemName, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "UpdateItemValue";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[4];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "UpdItem";

            spParameter[1] = new SqlParameter("@Item_ID", SqlDbType.VarChar, 2);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = itemID;

            spParameter[2] = new SqlParameter("@UpdateBY", SqlDbType.VarChar, 10);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = userName;

            spParameter[3] = new SqlParameter("@UpdateParam", SqlDbType.VarChar, 100);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = itemName;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean UpdateItemValue(string itemValueID, string itemValue, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "UpdateItemValue";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[4];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "UpdItemValue";

            spParameter[1] = new SqlParameter("@Item_Value_ID", SqlDbType.VarChar, 4);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = itemValueID;

            spParameter[2] = new SqlParameter("@UpdateBY", SqlDbType.VarChar, 10);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = userName;

            spParameter[3] = new SqlParameter("@UpdateParam", SqlDbType.VarChar, 100);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = itemValue;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean DeleteItem(string itemID)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "DeleteItemValue";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[2];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "DelItem";

            spParameter[1] = new SqlParameter("@Item_ID", SqlDbType.VarChar, 2);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = itemID;
            #endregion

            int rowsAffectedItem = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean DeleteItemValue(string itemValueID)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "DeleteItemValue";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[2];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "DelItemValue";

            spParameter[1] = new SqlParameter("@Item_Value_ID", SqlDbType.VarChar, 4);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = itemValueID;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }
    #endregion
}

