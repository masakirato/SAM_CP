using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Collections.Generic;

public class dbo_OtherRequisitionDetailDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[OtherRequisitionDetailSelectAll]";
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

    public static List<dbo_OtherRequisitionDetailClass> Search(String Requisition_No)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[OtherRequisitionDetailSearchbyRequistionNo]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;


        if (!string.IsNullOrEmpty(Requisition_No))
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", Requisition_No);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
       


        List<dbo_OtherRequisitionDetailClass> item = new List<dbo_OtherRequisitionDetailClass>();
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
                    item.Add(new dbo_OtherRequisitionDetailClass()
                    {
                       

                       Other_Requisition_Detail_ID=reader["Other_Requisition_Detail_ID"] is DBNull ? null : reader["Other_Requisition_Detail_ID"].ToString(),
                       Requisition_No= reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString(),
                       Product_ID=reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString(),
                       Price=reader["Price"] is DBNull ? null : (Decimal?)reader["Price"],
                       Vat=reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"],
                       Stock_on_Hand= reader["Stock_on_Hand"] is DBNull ? null : (Int32?)reader["Stock_on_Hand"],
                       Requisition_Qty= reader["Requisition_Qty"] is DBNull ? null : (Int16?)reader["Requisition_Qty"]

                             
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

    /*public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[OtherRequisitionDetailSearch]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        if (sField == "Other Requisition Detail I D")
        {
            selectCommand.Parameters.AddWithValue("@Other_Requisition_Detail_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Other_Requisition_Detail_ID", DBNull.Value);
        }
        if (sField == "Requisition No")
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (sField == "Product I D")
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (sField == "Price")
        {
            selectCommand.Parameters.AddWithValue("@Price", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Price", DBNull.Value);
        }
        if (sField == "Vat")
        {
            selectCommand.Parameters.AddWithValue("@Vat", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (sField == "Stock On Hand")
        {
            selectCommand.Parameters.AddWithValue("@Stock_on_Hand", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Stock_on_Hand", DBNull.Value);
        }
        if (sField == "Requisition Qty")
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Qty", sValue);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Requisition_Qty", DBNull.Value);
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
    }*/

    public static dbo_OtherRequisitionDetailClass Select_Record(dbo_OtherRequisitionDetailClass clsdbo_OtherRequisitionDetailPara)
    {
        dbo_OtherRequisitionDetailClass clsdbo_OtherRequisitionDetail = new dbo_OtherRequisitionDetailClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[OtherRequisitionDetailSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Other_Requisition_Detail_ID", clsdbo_OtherRequisitionDetailPara.Other_Requisition_Detail_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_OtherRequisitionDetail.Other_Requisition_Detail_ID = reader["Other_Requisition_Detail_ID"] is DBNull ? null : reader["Other_Requisition_Detail_ID"].ToString();
                clsdbo_OtherRequisitionDetail.Requisition_No = reader["Requisition_No"] is DBNull ? null : reader["Requisition_No"].ToString();
                clsdbo_OtherRequisitionDetail.Product_ID = reader["Product_ID"] is DBNull ? null : reader["Product_ID"].ToString();
                clsdbo_OtherRequisitionDetail.Price = reader["Price"] is DBNull ? null : (Decimal?)reader["Price"];
                clsdbo_OtherRequisitionDetail.Vat = reader["Vat"] is DBNull ? null : (Byte?)reader["Vat"];
                clsdbo_OtherRequisitionDetail.Stock_on_Hand = reader["Stock_on_Hand"] is DBNull ? null : (Int32?)reader["Stock_on_Hand"];
                clsdbo_OtherRequisitionDetail.Requisition_Qty = reader["Requisition_Qty"] is DBNull ? null : (Int16?)reader["Requisition_Qty"];
            }
            else
            {
                clsdbo_OtherRequisitionDetail = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_OtherRequisitionDetail;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_OtherRequisitionDetail;
    }

    public static bool Add(dbo_OtherRequisitionDetailClass clsdbo_OtherRequisitionDetail, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[OtherRequisitionDetailInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_OtherRequisitionDetail.Other_Requisition_Detail_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Other_Requisition_Detail_ID", clsdbo_OtherRequisitionDetail.Other_Requisition_Detail_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Other_Requisition_Detail_ID", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Requisition_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", clsdbo_OtherRequisitionDetail.Requisition_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_No", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Product_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", clsdbo_OtherRequisitionDetail.Product_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Price.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Price", clsdbo_OtherRequisitionDetail.Price);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Vat.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Vat", clsdbo_OtherRequisitionDetail.Vat);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Vat", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Stock_on_Hand.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Stock_on_Hand", clsdbo_OtherRequisitionDetail.Stock_on_Hand);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Stock_on_Hand", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Requisition_Qty.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Qty", clsdbo_OtherRequisitionDetail.Requisition_Qty);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Requisition_Qty", DBNull.Value);
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

    public static bool Update(dbo_OtherRequisitionDetailClass newdbo_OtherRequisitionDetailClass)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[OtherRequisitionDetailUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_OtherRequisitionDetailClass.Other_Requisition_Detail_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOther_Requisition_Detail_ID", newdbo_OtherRequisitionDetailClass.Other_Requisition_Detail_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOther_Requisition_Detail_ID", DBNull.Value);
        }
        if (newdbo_OtherRequisitionDetailClass.Requisition_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", newdbo_OtherRequisitionDetailClass.Requisition_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_No", DBNull.Value);
        }
        if (newdbo_OtherRequisitionDetailClass.Product_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", newdbo_OtherRequisitionDetailClass.Product_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_ID", DBNull.Value);
        }
        if (newdbo_OtherRequisitionDetailClass.Price.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", newdbo_OtherRequisitionDetailClass.Price);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice", DBNull.Value);
        }
        if (newdbo_OtherRequisitionDetailClass.Vat.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewVat", newdbo_OtherRequisitionDetailClass.Vat);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewVat", DBNull.Value);
        }
        if (newdbo_OtherRequisitionDetailClass.Stock_on_Hand.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStock_on_Hand", newdbo_OtherRequisitionDetailClass.Stock_on_Hand);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStock_on_Hand", DBNull.Value);
        }
        if (newdbo_OtherRequisitionDetailClass.Requisition_Qty.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Qty", newdbo_OtherRequisitionDetailClass.Requisition_Qty);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRequisition_Qty", DBNull.Value);
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

    public static bool Delete(dbo_OtherRequisitionDetailClass clsdbo_OtherRequisitionDetail)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[OtherRequisitionDetailDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_OtherRequisitionDetail.Other_Requisition_Detail_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldOther_Requisition_Detail_ID", clsdbo_OtherRequisitionDetail.Other_Requisition_Detail_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldOther_Requisition_Detail_ID", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Requisition_No != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", clsdbo_OtherRequisitionDetail.Requisition_No);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_No", DBNull.Value);
        }
        /*if (clsdbo_OtherRequisitionDetail.Product_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", clsdbo_OtherRequisitionDetail.Product_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldProduct_ID", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Price.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldPrice", clsdbo_OtherRequisitionDetail.Price);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldPrice", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Vat.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", clsdbo_OtherRequisitionDetail.Vat);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldVat", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Stock_on_Hand.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_on_Hand", clsdbo_OtherRequisitionDetail.Stock_on_Hand);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldStock_on_Hand", DBNull.Value);
        }
        if (clsdbo_OtherRequisitionDetail.Requisition_Qty.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Qty", clsdbo_OtherRequisitionDetail.Requisition_Qty);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRequisition_Qty", DBNull.Value);
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

