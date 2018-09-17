using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_BenefitDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[BenefitSelectAll]";
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

    public static List<dbo_BenefitClass> Search(string User_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[BenefitSearch]";
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


        List<dbo_BenefitClass> item = new List<dbo_BenefitClass>();

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
                    item.Add(new dbo_BenefitClass()
                    {
                        Benefit_ID = reader["Benefit_ID"] is DBNull ? null : reader["Benefit_ID"].ToString()
                        ,
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString()
                        ,
                        Benefit_Date = reader["Benefit_Date"] is DBNull ? null : (DateTime?)reader["Benefit_Date"]
                        ,
                        Benefit_Name = reader["Benefit_Name"] is DBNull ? null : reader["Benefit_Name"].ToString()
                        ,
                        Beneficiary = reader["Beneficiary"] is DBNull ? null : reader["Beneficiary"].ToString()
                        ,
                        Relationship = reader["Relationship"] is DBNull ? null : reader["Relationship"].ToString()
                        ,
                        Benefit_Amount = reader["Benefit_Amount"] is DBNull ? null : (Decimal?)reader["Benefit_Amount"]
                        ,
                        End_Date = reader["End_Date"] is DBNull ? null : (DateTime?)reader["End_Date"]
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

    public static dbo_BenefitClass Select_Record(dbo_BenefitClass clsdbo_BenefitPara)
    {
        dbo_BenefitClass clsdbo_Benefit = new dbo_BenefitClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[BenefitSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@Benefit_ID", clsdbo_BenefitPara.Benefit_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Benefit.Benefit_ID = reader["Benefit_ID"] is DBNull ? null : reader["Benefit_ID"].ToString();
                clsdbo_Benefit.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_Benefit.Benefit_Date = reader["Benefit_Date"] is DBNull ? null : (DateTime?)reader["Benefit_Date"];
                clsdbo_Benefit.Benefit_Name = reader["Benefit_Name"] is DBNull ? null : reader["Benefit_Name"].ToString();
                clsdbo_Benefit.Beneficiary = reader["Beneficiary"] is DBNull ? null : reader["Beneficiary"].ToString();
                clsdbo_Benefit.Relationship = reader["Relationship"] is DBNull ? null : reader["Relationship"].ToString();
                clsdbo_Benefit.Benefit_Amount = reader["Benefit_Amount"] is DBNull ? null : (Decimal?)reader["Benefit_Amount"];
                clsdbo_Benefit.End_Date = reader["End_Date"] is DBNull ? null : (DateTime?)reader["End_Date"];
            }
            else
            {
                clsdbo_Benefit = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Benefit;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Benefit;
    }

    public static bool Add(dbo_BenefitClass clsdbo_Benefit, string Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[dbo].[BenefitInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Benefit.Benefit_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Benefit_ID", clsdbo_Benefit.Benefit_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Benefit_ID", DBNull.Value);
        }
        if (clsdbo_Benefit.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_Benefit.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (clsdbo_Benefit.Benefit_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Benefit_Date", clsdbo_Benefit.Benefit_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Benefit_Date", DBNull.Value);
        }
        if (clsdbo_Benefit.Benefit_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Benefit_Name", clsdbo_Benefit.Benefit_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Benefit_Name", DBNull.Value);
        }
        if (clsdbo_Benefit.Beneficiary != null)
        {
            insertCommand.Parameters.AddWithValue("@Beneficiary", clsdbo_Benefit.Beneficiary);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Beneficiary", DBNull.Value);
        }
        if (clsdbo_Benefit.Relationship != null)
        {
            insertCommand.Parameters.AddWithValue("@Relationship", clsdbo_Benefit.Relationship);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Relationship", DBNull.Value);
        }
        if (clsdbo_Benefit.Benefit_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Benefit_Amount", clsdbo_Benefit.Benefit_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Benefit_Amount", DBNull.Value);
        }
        if (clsdbo_Benefit.End_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@End_Date", clsdbo_Benefit.End_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@End_Date", DBNull.Value);
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

    public static bool Update(dbo_BenefitClass newdbo_BenefitClass, string Last_Modified_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[BenefitUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;
        if (newdbo_BenefitClass.Benefit_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBenefit_ID", newdbo_BenefitClass.Benefit_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBenefit_ID", DBNull.Value);
        }
        if (newdbo_BenefitClass.User_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", newdbo_BenefitClass.User_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewUser_ID", DBNull.Value);
        }
        if (newdbo_BenefitClass.Benefit_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewBenefit_Date", newdbo_BenefitClass.Benefit_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBenefit_Date", DBNull.Value);
        }
        if (newdbo_BenefitClass.Benefit_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBenefit_Name", newdbo_BenefitClass.Benefit_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBenefit_Name", DBNull.Value);
        }
        if (newdbo_BenefitClass.Beneficiary != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBeneficiary", newdbo_BenefitClass.Beneficiary);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBeneficiary", DBNull.Value);
        }
        if (newdbo_BenefitClass.Relationship != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRelationship", newdbo_BenefitClass.Relationship);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRelationship", DBNull.Value);
        }
        if (newdbo_BenefitClass.Benefit_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewBenefit_Amount", newdbo_BenefitClass.Benefit_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBenefit_Amount", DBNull.Value);
        }
        if (newdbo_BenefitClass.End_Date.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewEnd_Date", newdbo_BenefitClass.End_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewEnd_Date", DBNull.Value);
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

    public static bool Delete(dbo_BenefitClass clsdbo_Benefit)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[BenefitDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Benefit.Benefit_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBenefit_ID", clsdbo_Benefit.Benefit_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBenefit_ID", DBNull.Value);
        }
        /*if (clsdbo_Benefit.User_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", clsdbo_Benefit.User_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
        }*/
        //if (clsdbo_Benefit.Benefit_Date.HasValue == true)
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldBenefit_Date", clsdbo_Benefit.Benefit_Date);
        //}
        //else
        //{
        //    deleteCommand.Parameters.AddWithValue("@OldBenefit_Date", DBNull.Value);
        //}
        /*if (clsdbo_Benefit.Benefit_Name != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBenefit_Name", clsdbo_Benefit.Benefit_Name);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBenefit_Name", DBNull.Value);
        }
        if (clsdbo_Benefit.Beneficiary != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldBeneficiary", clsdbo_Benefit.Beneficiary);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBeneficiary", DBNull.Value);
        }
        if (clsdbo_Benefit.Relationship != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldRelationship", clsdbo_Benefit.Relationship);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldRelationship", DBNull.Value);
        }
        if (clsdbo_Benefit.Benefit_Amount.HasValue == true)
        {
            deleteCommand.Parameters.AddWithValue("@OldBenefit_Amount", clsdbo_Benefit.Benefit_Amount);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldBenefit_Amount", DBNull.Value);
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

