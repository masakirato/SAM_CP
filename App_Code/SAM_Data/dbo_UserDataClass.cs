using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;

public class dbo_UserDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public static Count_SP Get_Count_SP(string user_id)
    {
        logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        Count_SP count_sp = new Count_SP();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[Get_Count_SP]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;



        selectCommand.Parameters.AddWithValue("@user_id", user_id);


        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                count_sp.sp_total = reader["sp_total"] is DBNull ? 0 : (int)reader["sp_total"];
                count_sp.sp_this_month = reader["sp_this_month"] is DBNull ? 0 : (int)reader["sp_this_month"];
                count_sp.sp_resign = reader["sp_resign"] is DBNull ? 0 : (int)reader["sp_resign"];
                count_sp.sp_customer_total = reader["sp_customer_total"] is DBNull ? 0 : (int)reader["sp_customer_total"];
            }
            else
            {
                count_sp = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return count_sp;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return count_sp;

    }

    public static string Encrypt(string value)
    {
        logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string m_Password = string.Empty;
            Paddedwall.CryptoLib.Crypto cr = new Paddedwall.CryptoLib.Crypto();
            m_Password = cr.Encrypt(value, Paddedwall.CryptoLib.Crypto.CryptoTypes.encTypeRijndael);


            return m_Password;
        }
        catch (Exception ex) { logger.Error(ex.Message); }
        {
            return string.Empty;
        }

    }

    public static string Decrypt(string value)
    {
        //logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string m_Password = string.Empty;
            Paddedwall.CryptoLib.Crypto cr = new Paddedwall.CryptoLib.Crypto();
            m_Password = cr.Decrypt(value, Paddedwall.CryptoLib.Crypto.CryptoTypes.encTypeRijndael);


            return m_Password;
        }
        catch (Exception ex) { logger.Error(ex.Message); }
        {
            return string.Empty;
        }

    }

    public static dbo_UserClass VerifyPassword(string Username, string Password)
    {
        logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        string encrypt_password = string.Empty;

        dbo_UserClass clsdbo_User = new dbo_UserClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[VerifyUserPassword]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;

        //logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + Password);

        string m_Password = Encrypt(Password);

        logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + m_Password + " " + Password);

        clsdbo_User.Password = m_Password;



        if (!string.IsNullOrEmpty(Username))
        {
            selectCommand.Parameters.AddWithValue("@Username", Username);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Username", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(clsdbo_User.Password))
        {
            selectCommand.Parameters.AddWithValue("@Password", clsdbo_User.Password);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Password", DBNull.Value);
        }



        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_User.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_User.First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString();
                clsdbo_User.Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString();
                clsdbo_User.Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString();
                clsdbo_User.Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString();
                clsdbo_User.Position = reader["Position"] is DBNull ? null : reader["Position"].ToString();
                clsdbo_User.Section = reader["Section"] is DBNull ? null : reader["Section"].ToString();
                clsdbo_User.Division = reader["Division"] is DBNull ? null : reader["Division"].ToString();
                clsdbo_User.Manager = reader["Manager"] is DBNull ? null : reader["Manager"].ToString();
                clsdbo_User.User_Group_ID = reader["User_Group_ID"] is DBNull ? null : reader["User_Group_ID"].ToString();
                clsdbo_User.Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString();
                clsdbo_User.Email = reader["Email"] is DBNull ? null : reader["Email"].ToString();
                clsdbo_User.Username = reader["Username"] is DBNull ? null : reader["Username"].ToString();
                clsdbo_User.Password = string.Empty;
                clsdbo_User.Approval_Status_ID = reader["Approval_Status_ID"] is DBNull ? null : reader["Approval_Status_ID"].ToString();
                clsdbo_User.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
                clsdbo_User.CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString();

                clsdbo_User.Birthdate = reader["Birthdate"] is DBNull ? null : (DateTime?)reader["Birthdate"];
                clsdbo_User.ID_Card_No = reader["ID_Card_No"] is DBNull ? null : reader["ID_Card_No"].ToString();
                clsdbo_User.Home_House_No = reader["Home_House_No"] is DBNull ? null : reader["Home_House_No"].ToString();
                clsdbo_User.Home_Village = reader["Home_Village"] is DBNull ? null : reader["Home_Village"].ToString();
                clsdbo_User.Home_Village_No = reader["Home_Village_No"] is DBNull ? null : reader["Home_Village_No"].ToString();
                clsdbo_User.Home_Alley = reader["Home_Alley"] is DBNull ? null : reader["Home_Alley"].ToString();
                clsdbo_User.Home_Road = reader["Home_Road"] is DBNull ? null : reader["Home_Road"].ToString();
                clsdbo_User.Home_Sub_district = reader["Home_Sub_district"] is DBNull ? null : reader["Home_Sub_district"].ToString();
                clsdbo_User.Home_District = reader["Home_District"] is DBNull ? null : reader["Home_District"].ToString();
                clsdbo_User.Home_Province = reader["Home_Province"] is DBNull ? null : reader["Home_Province"].ToString();
                clsdbo_User.Home_Postal_ID = reader["Home_Postal_ID"] is DBNull ? null : reader["Home_Postal_ID"].ToString();

                clsdbo_User.Join_Date = reader["Join_Date"] is DBNull ? null : (DateTime?)reader["Join_Date"];



                clsdbo_User.Resign_Date = reader["Resign_Date"] is DBNull ? null : (DateTime?)reader["Resign_Date"];
                clsdbo_User.Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString();
                clsdbo_User.Payment_Type = reader["Payment_Type"] is DBNull ? null : reader["Payment_Type"].ToString();
                clsdbo_User.Credit_Term = reader["Credit_Term"] is DBNull ? null : (Byte?)reader["Credit_Term"];
                clsdbo_User.Credit_Limit = reader["Credit_Limit"] is DBNull ? null : (Byte?)reader["Credit_Limit"];
                clsdbo_User.Applied_Document = reader["Applied_Document"] is DBNull ? null : reader["Applied_Document"].ToString();
                //clsdbo_User.Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString();
                //clsdbo_User.Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString();
                clsdbo_User.Title_ID = reader["Title_ID"] is DBNull ? null : reader["Title_ID"].ToString();
                clsdbo_User.Present_House_No = reader["Present_House_No"] is DBNull ? null : reader["Present_House_No"].ToString();
                clsdbo_User.Present_Village = reader["Present_Village"] is DBNull ? null : reader["Present_Village"].ToString();
                clsdbo_User.Present_Village_No = reader["Present_Village_No"] is DBNull ? null : reader["Present_Village_No"].ToString();
                clsdbo_User.Present_Alley = reader["Present_Alley"] is DBNull ? null : reader["Present_Alley"].ToString();
                clsdbo_User.Present_Road = reader["Present_Road"] is DBNull ? null : reader["Present_Road"].ToString();
                clsdbo_User.Present_Sub_District = reader["Present_Sub_District"] is DBNull ? null : reader["Present_Sub_District"].ToString();
                clsdbo_User.Present_District = reader["Present_District"] is DBNull ? null : reader["Present_District"].ToString();
                clsdbo_User.Present_Province = reader["Present_Province"] is DBNull ? null : reader["Present_Province"].ToString();
                clsdbo_User.Present_Postal_ID = reader["Present_Postal_ID"] is DBNull ? null : reader["Present_Postal_ID"].ToString();
            }
            else
            {
                clsdbo_User = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_User;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_User;

    }
    [Obsolete]
    public static List<dbo_UserClass> SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[UserSelectAll]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        List<dbo_UserClass> item = new List<dbo_UserClass>();
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
                    item.Add(new dbo_UserClass()
                    {
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString()
                  ,
                        First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString()
                  ,
                        Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString()
                  ,
                        Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString()
                  ,
                        Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString()
                  ,
                        Position = reader["Position"] is DBNull ? null : reader["Position"].ToString()
                  ,
                        Section = reader["Section"] is DBNull ? null : reader["Section"].ToString()
                  ,
                        Division = reader["Division"] is DBNull ? null : reader["Division"].ToString()
                  ,
                        Manager = reader["Manager"] is DBNull ? null : reader["Manager"].ToString()
                  ,
                        User_Group_ID = reader["User_Group_ID"] is DBNull ? null : reader["User_Group_ID"].ToString()
                  ,
                        Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString()
                  ,
                        Email = reader["Email"] is DBNull ? null : reader["Email"].ToString()
                  ,
                        Username = reader["Username"] is DBNull ? null : reader["Username"].ToString()
                  ,
                        Password = reader["Password"] is DBNull ? null : reader["Password"].ToString()
                  ,
                        Approval_Status_ID = reader["Approval_Status_ID"] is DBNull ? null : reader["Approval_Status_ID"].ToString()
                  ,
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString()
                  ,
                        CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString()
                  ,
                        Birthdate = reader["Birthdate"] is DBNull ? null : (DateTime?)reader["Birthdate"]
                  ,
                        ID_Card_No = reader["ID_Card_No"] is DBNull ? null : reader["ID_Card_No"].ToString()
                  ,
                        Home_House_No = reader["Home_House_No"] is DBNull ? null : reader["Home_House_No"].ToString()
                  ,
                        Home_Village = reader["Home_Village"] is DBNull ? null : reader["Home_Village"].ToString()
                  ,
                        Home_Village_No = reader["Home_Village_No"] is DBNull ? null : reader["Home_Village_No"].ToString()
                  ,
                        Home_Alley = reader["Home_Alley"] is DBNull ? null : reader["Home_Alley"].ToString()
                  ,
                        Home_Road = reader["Home_Road"] is DBNull ? null : reader["Home_Road"].ToString()
                  ,
                        Home_Sub_district = reader["Home_Sub_district"] is DBNull ? null : reader["Home_Sub_district"].ToString()
                  ,
                        Home_District = reader["Home_District"] is DBNull ? null : reader["Home_District"].ToString()
                  ,
                        Home_Province = reader["Home_Province"] is DBNull ? null : reader["Home_Province"].ToString()
                  ,
                        Home_Postal_ID = reader["Home_Postal_ID"] is DBNull ? null : reader["Home_Postal_ID"].ToString()
                  ,
                        Join_Date = reader["Join_Date"] is DBNull ? null : (DateTime?)reader["Join_Date"]
                  ,
                        Resign_Date = reader["Resign_Date"] is DBNull ? null : (DateTime?)reader["Resign_Date"]
                  ,
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString()
                  ,
                        Payment_Type = reader["Payment_Type"] is DBNull ? null : reader["Payment_Type"].ToString()
                  ,
                        Credit_Term = reader["Credit_Term"] is DBNull ? null : (Byte?)reader["Credit_Term"]
                  ,
                        Credit_Limit = reader["Credit_Limit"] is DBNull ? null : (Byte?)reader["Credit_Limit"]
                  ,
                        Applied_Document = reader["Applied_Document"] is DBNull ? null : reader["Applied_Document"].ToString()
                  ,
                        Title_ID = reader["Title_ID"] is DBNull ? null : reader["Title_ID"].ToString()
                  ,
                        Present_House_No = reader["Present_House_No"] is DBNull ? null : reader["Present_House_No"].ToString()
                  ,
                        Present_Village = reader["Present_Village"] is DBNull ? null : reader["Present_Village"].ToString()
                  ,
                        Present_Village_No = reader["Present_Village_No"] is DBNull ? null : reader["Present_Village_No"].ToString()
                  ,
                        Present_Alley = reader["Present_Alley"] is DBNull ? null : reader["Present_Alley"].ToString()
                  ,
                        Present_Road = reader["Present_Road"] is DBNull ? null : reader["Present_Road"].ToString()
                  ,
                        Present_Sub_District = reader["Present_Sub_District"] is DBNull ? null : reader["Present_Sub_District"].ToString()
                  ,
                        Present_District = reader["Present_District"] is DBNull ? null : reader["Present_District"].ToString()
                  ,
                        Present_Province = reader["Present_Province"] is DBNull ? null : reader["Present_Province"].ToString()
                  ,
                        Present_Postal_ID = reader["Present_Postal_ID"] is DBNull ? null : reader["Present_Postal_ID"].ToString()
                  ,
                        ShowDashboard = reader["ShowDashboard"] is DBNull ? null : reader["ShowDashboard"].ToString()
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

    public static List<dbo_UserClass> Search(string User_ID, string First_Name, string Position,
        string Division, string Status, string User_Group_ID, string Role_ID, string CV_Code, DateTime? Join_Date
        , string user_name, string region)
    {
        logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        //   logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value == null ? string.Empty : System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "UserSearch";
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
        if (!string.IsNullOrEmpty(First_Name))
        {
            selectCommand.Parameters.AddWithValue("@First_Name", First_Name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@First_Name", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Position))
        {
            selectCommand.Parameters.AddWithValue("@Position", Position);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Position", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(User_Group_ID))
        {
            selectCommand.Parameters.AddWithValue("@User_Group_ID", User_Group_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_Group_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(Role_ID))
        {
            selectCommand.Parameters.AddWithValue("@Role_ID", Role_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Role_ID", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(CV_Code))
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }



        if (!string.IsNullOrEmpty(user_name))
        {
            selectCommand.Parameters.AddWithValue("@User_Name", user_name);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@User_Name", DBNull.Value);
        }



        if (Join_Date.HasValue)
        {
            selectCommand.Parameters.AddWithValue("@Join_Date", Join_Date);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Join_Date", DBNull.Value);
        }



        switch (Status)
        {
            case "Active":
                selectCommand.Parameters.AddWithValue("@Status", true);
                break;
            case "In active":
                selectCommand.Parameters.AddWithValue("@Status", false);
                break;
            default:
                selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                break;
        }


        if (!string.IsNullOrEmpty(region))
        {
            selectCommand.Parameters.AddWithValue("@Region", region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }


        List<dbo_UserClass> item = new List<dbo_UserClass>();
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
                    item.Add(new dbo_UserClass()
                    {
                        User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString()
                  ,
                        First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString()
                  ,
                        Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString()
                  ,
                        Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString()
                  ,
                        Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString()
                  ,
                        Position = reader["Position"] is DBNull ? null : reader["Position"].ToString()
                  ,
                        Section = reader["Section"] is DBNull ? null : reader["Section"].ToString()
                  ,
                        Division = reader["Division"] is DBNull ? null : reader["Division"].ToString()
                  ,
                        Manager = reader["Manager"] is DBNull ? null : reader["Manager"].ToString()
                  ,
                        User_Group_ID = reader["User_Group_ID"] is DBNull ? null : reader["User_Group_ID"].ToString()
                  ,
                        Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString()
                  ,
                        Email = reader["Email"] is DBNull ? null : reader["Email"].ToString()
                  ,
                        Username = reader["Username"] is DBNull ? null : reader["Username"].ToString()
                  ,
                        Password = reader["Password"] is DBNull ? null : reader["Password"].ToString()
                  ,
                        Approval_Status_ID = reader["Approval_Status_ID"] is DBNull ? null : reader["Approval_Status_ID"].ToString()
                  ,
                        Status = reader["Status"] is DBNull ? null : reader["Status"].ToString()
                  ,
                        CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString()
                  ,
                        Birthdate = reader["Birthdate"] is DBNull ? null : (DateTime?)reader["Birthdate"]
                  ,
                        ID_Card_No = reader["ID_Card_No"] is DBNull ? null : reader["ID_Card_No"].ToString()
                  ,
                        Home_House_No = reader["Home_House_No"] is DBNull ? null : reader["Home_House_No"].ToString()
                  ,
                        Home_Village = reader["Home_Village"] is DBNull ? null : reader["Home_Village"].ToString()
                  ,
                        Home_Village_No = reader["Home_Village_No"] is DBNull ? null : reader["Home_Village_No"].ToString()
                  ,
                        Home_Alley = reader["Home_Alley"] is DBNull ? null : reader["Home_Alley"].ToString()
                  ,
                        Home_Road = reader["Home_Road"] is DBNull ? null : reader["Home_Road"].ToString()
                  ,
                        Home_Sub_district = reader["Home_Sub_district"] is DBNull ? null : reader["Home_Sub_district"].ToString()
                  ,
                        Home_District = reader["Home_District"] is DBNull ? null : reader["Home_District"].ToString()
                  ,
                        Home_Province = reader["Home_Province"] is DBNull ? null : reader["Home_Province"].ToString()
                  ,
                        Home_Postal_ID = reader["Home_Postal_ID"] is DBNull ? null : reader["Home_Postal_ID"].ToString()
                  ,
                        Join_Date = reader["Join_Date"] is DBNull ? null : (DateTime?)reader["Join_Date"]
                  ,
                        Resign_Date = reader["Resign_Date"] is DBNull ? null : (DateTime?)reader["Resign_Date"]
                  ,
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString()
                  ,
                        Payment_Type = reader["Payment_Type"] is DBNull ? null : reader["Payment_Type"].ToString()
                  ,
                        Credit_Term = reader["Credit_Term"] is DBNull ? null : (Byte?)reader["Credit_Term"]
                  ,
                        Credit_Limit = reader["Credit_Limit"] is DBNull ? null : (Byte?)reader["Credit_Limit"]
                  ,
                        Applied_Document = reader["Applied_Document"] is DBNull ? null : reader["Applied_Document"].ToString()
                  ,
                        Title_ID = reader["Title_ID"] is DBNull ? null : reader["Title_ID"].ToString()
                  ,
                        Present_House_No = reader["Present_House_No"] is DBNull ? null : reader["Present_House_No"].ToString()
                  ,
                        Present_Village = reader["Present_Village"] is DBNull ? null : reader["Present_Village"].ToString()
                  ,
                        Present_Village_No = reader["Present_Village_No"] is DBNull ? null : reader["Present_Village_No"].ToString()
                  ,
                        Present_Alley = reader["Present_Alley"] is DBNull ? null : reader["Present_Alley"].ToString()
                  ,
                        Present_Road = reader["Present_Road"] is DBNull ? null : reader["Present_Road"].ToString()
                  ,
                        Present_Sub_District = reader["Present_Sub_District"] is DBNull ? null : reader["Present_Sub_District"].ToString()
                  ,
                        Present_District = reader["Present_District"] is DBNull ? null : reader["Present_District"].ToString()
                  ,
                        Present_Province = reader["Present_Province"] is DBNull ? null : reader["Present_Province"].ToString()
                  ,
                        Present_Postal_ID = reader["Present_Postal_ID"] is DBNull ? null : reader["Present_Postal_ID"].ToString()
                  ,
                        ShowDashboard = reader["ShowDashboard"] is DBNull ? null : reader["ShowDashboard"].ToString()
                        ,
                        AgentName = reader["AgentName"] is DBNull ? null : reader["AgentName"].ToString(),

                        Region = reader["Region"] is DBNull ? null : reader["Region"].ToString()
                        ,
                        FullName = reader["FullName"] is DBNull ? null : reader["FullName"].ToString()


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

    public static dbo_UserClass Select_Record(string User_ID)
    {
        logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_UserClass clsdbo_User = new dbo_UserClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[UserSelect]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@User_ID", User_ID);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_User.User_ID = reader["User_ID"] is DBNull ? null : reader["User_ID"].ToString();
                clsdbo_User.First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString();
                clsdbo_User.Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString();
                clsdbo_User.Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString();
                clsdbo_User.Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString();
                clsdbo_User.Position = reader["Position"] is DBNull ? null : reader["Position"].ToString();
                clsdbo_User.Section = reader["Section"] is DBNull ? null : reader["Section"].ToString();
                clsdbo_User.Division = reader["Division"] is DBNull ? null : reader["Division"].ToString();
                clsdbo_User.Manager = reader["Manager"] is DBNull ? null : reader["Manager"].ToString();
                clsdbo_User.User_Group_ID = reader["User_Group_ID"] is DBNull ? null : reader["User_Group_ID"].ToString();
                clsdbo_User.Role_ID = reader["Role_ID"] is DBNull ? null : reader["Role_ID"].ToString();
                clsdbo_User.Email = reader["Email"] is DBNull ? null : reader["Email"].ToString();

                clsdbo_User.Username = reader["Username"] is DBNull ? null : reader["Username"].ToString();


                string m_password = reader["Password"] is DBNull ? null : reader["Password"].ToString();


                clsdbo_User.Password = Decrypt(m_password);

                clsdbo_User.Approval_Status_ID = reader["Approval_Status_ID"] is DBNull ? null : reader["Approval_Status_ID"].ToString();
                clsdbo_User.Status = reader["Status"] is DBNull ? null : reader["Status"].ToString();
                clsdbo_User.CV_CODE = reader["CV_CODE"] is DBNull ? null : reader["CV_CODE"].ToString();

                clsdbo_User.Birthdate = reader["Birthdate"] is DBNull ? null : (DateTime?)reader["Birthdate"];
                clsdbo_User.ID_Card_No = reader["ID_Card_No"] is DBNull ? null : reader["ID_Card_No"].ToString();
                clsdbo_User.Home_House_No = reader["Home_House_No"] is DBNull ? null : reader["Home_House_No"].ToString();
                clsdbo_User.Home_Village = reader["Home_Village"] is DBNull ? null : reader["Home_Village"].ToString();
                clsdbo_User.Home_Village_No = reader["Home_Village_No"] is DBNull ? null : reader["Home_Village_No"].ToString();
                clsdbo_User.Home_Alley = reader["Home_Alley"] is DBNull ? null : reader["Home_Alley"].ToString();
                clsdbo_User.Home_Road = reader["Home_Road"] is DBNull ? null : reader["Home_Road"].ToString();
                clsdbo_User.Home_Sub_district = reader["Home_Sub_district"] is DBNull ? null : reader["Home_Sub_district"].ToString();
                clsdbo_User.Home_District = reader["Home_District"] is DBNull ? null : reader["Home_District"].ToString();
                clsdbo_User.Home_Province = reader["Home_Province"] is DBNull ? null : reader["Home_Province"].ToString();
                clsdbo_User.Home_Postal_ID = reader["Home_Postal_ID"] is DBNull ? null : reader["Home_Postal_ID"].ToString();

                clsdbo_User.Join_Date = reader["Join_Date"] is DBNull ? null : (DateTime?)reader["Join_Date"];
                clsdbo_User.Resign_Date = reader["Resign_Date"] is DBNull ? null : (DateTime?)reader["Resign_Date"];
                clsdbo_User.Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString();
                clsdbo_User.Payment_Type = reader["Payment_Type"] is DBNull ? null : reader["Payment_Type"].ToString();
                clsdbo_User.Credit_Term = reader["Credit_Term"] is DBNull ? null : (Byte?)reader["Credit_Term"];
                clsdbo_User.Credit_Limit = reader["Credit_Limit"] is DBNull ? null : (Byte?)reader["Credit_Limit"];
                clsdbo_User.Applied_Document = reader["Applied_Document"] is DBNull ? null : reader["Applied_Document"].ToString();


                //clsdbo_User.Created_By = reader["Created_By"] is DBNull ? null : reader["Created_By"].ToString();
                //clsdbo_User.Last_Modified_By = reader["Last_Modified_By"] is DBNull ? null : reader["Last_Modified_By"].ToString();


                clsdbo_User.Title_ID = reader["Title_ID"] is DBNull ? null : reader["Title_ID"].ToString();
                clsdbo_User.Present_House_No = reader["Present_House_No"] is DBNull ? null : reader["Present_House_No"].ToString();
                clsdbo_User.Present_Village = reader["Present_Village"] is DBNull ? null : reader["Present_Village"].ToString();
                clsdbo_User.Present_Village_No = reader["Present_Village_No"] is DBNull ? null : reader["Present_Village_No"].ToString();
                clsdbo_User.Present_Alley = reader["Present_Alley"] is DBNull ? null : reader["Present_Alley"].ToString();
                clsdbo_User.Present_Road = reader["Present_Road"] is DBNull ? null : reader["Present_Road"].ToString();
                clsdbo_User.Present_Sub_District = reader["Present_Sub_District"] is DBNull ? null : reader["Present_Sub_District"].ToString();
                clsdbo_User.Present_District = reader["Present_District"] is DBNull ? null : reader["Present_District"].ToString();
                clsdbo_User.Present_Province = reader["Present_Province"] is DBNull ? null : reader["Present_Province"].ToString();
                clsdbo_User.Present_Postal_ID = reader["Present_Postal_ID"] is DBNull ? null : reader["Present_Postal_ID"].ToString();
                clsdbo_User.ShowDashboard = reader["ShowDashboard"] is DBNull ? null : reader["ShowDashboard"].ToString();
                clsdbo_User.First_Name_Eng = reader["First_Name_Eng"] is DBNull ? null : reader["First_Name_Eng"].ToString();
                clsdbo_User.Last_Name_Eng = reader["Last_Name_Eng"] is DBNull ? null : reader["Last_Name_Eng"].ToString();
                clsdbo_User.AgentName = reader["AgentName"] is DBNull ? null : reader["AgentName"].ToString();

                clsdbo_User.Route = reader["Route"] is DBNull ? null : reader["Route"].ToString();
                clsdbo_User.Region = reader["Region"] is DBNull ? null : reader["Region"].ToString();
                clsdbo_User.Item_Value_ID = reader["Item_Value_ID"] is DBNull ? null : reader["Item_Value_ID"].ToString();


            }
            else
            {
                clsdbo_User = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_User;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_User;
    }

    public static bool Add(dbo_UserClass clsdbo_User, string Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[UserInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_User.User_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_ID", clsdbo_User.User_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_ID", DBNull.Value);
        }
        if (clsdbo_User.First_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@First_Name", clsdbo_User.First_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@First_Name", DBNull.Value);
        }
        if (clsdbo_User.First_Name_Eng != null)
        {
            insertCommand.Parameters.AddWithValue("@First_Name_Eng", clsdbo_User.First_Name_Eng);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@First_Name_Eng", DBNull.Value);
        }
        if (clsdbo_User.Last_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Last_Name", clsdbo_User.Last_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Name", string.Empty);
        }
        if (clsdbo_User.Last_Name_Eng != null)
        {
            insertCommand.Parameters.AddWithValue("@Last_Name_Eng", clsdbo_User.Last_Name_Eng);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Name_Eng", DBNull.Value);
        }
        if (clsdbo_User.Home_Phone_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Phone_No", clsdbo_User.Home_Phone_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Phone_No", DBNull.Value);
        }
        if (clsdbo_User.Mobile != null)
        {
            insertCommand.Parameters.AddWithValue("@Mobile", clsdbo_User.Mobile);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Mobile", DBNull.Value);
        }
        if (clsdbo_User.Position != null)
        {
            insertCommand.Parameters.AddWithValue("@Position", clsdbo_User.Position);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Position", DBNull.Value);
        }
        if (clsdbo_User.Section != null)
        {
            insertCommand.Parameters.AddWithValue("@Section", clsdbo_User.Section);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Section", DBNull.Value);
        }
        if (clsdbo_User.Division != null)
        {
            insertCommand.Parameters.AddWithValue("@Division", clsdbo_User.Division);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Division", DBNull.Value);
        }
        if (clsdbo_User.Manager != null)
        {
            insertCommand.Parameters.AddWithValue("@Manager", clsdbo_User.Manager);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Manager", DBNull.Value);
        }
        if (clsdbo_User.User_Group_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@User_Group_ID", clsdbo_User.User_Group_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@User_Group_ID", DBNull.Value);
        }
        if (clsdbo_User.Role_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Role_ID", clsdbo_User.Role_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Role_ID", DBNull.Value);
        }
        if (clsdbo_User.Email != null)
        {
            insertCommand.Parameters.AddWithValue("@Email", clsdbo_User.Email);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Email", DBNull.Value);
        }
        if (clsdbo_User.Username != null)
        {
            insertCommand.Parameters.AddWithValue("@Username", clsdbo_User.Username);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Username", DBNull.Value);
        }

        string m_Password = Encrypt(clsdbo_User.Password);
        if (clsdbo_User.Password != null)
        {
            insertCommand.Parameters.AddWithValue("@Password", m_Password);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Password", DBNull.Value);
        }
        if (clsdbo_User.Approval_Status_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Approval_Status_ID", clsdbo_User.Approval_Status_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Approval_Status_ID", DBNull.Value);
        }




        switch (clsdbo_User.Status)
        {
            case "Active":
                insertCommand.Parameters.AddWithValue("@Status", true);
                break;
            case "In active":
                insertCommand.Parameters.AddWithValue("@Status", false);
                break;
            default:
                insertCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                break;
        }

        if (clsdbo_User.CV_CODE != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_CODE", clsdbo_User.CV_CODE);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_CODE", DBNull.Value);
        }
        if (clsdbo_User.Birthdate != null)
        {
            insertCommand.Parameters.AddWithValue("@Birthdate", clsdbo_User.Birthdate);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Birthdate", DBNull.Value);
        }
        if (clsdbo_User.ID_Card_No != null)
        {
            insertCommand.Parameters.AddWithValue("@ID_Card_No", clsdbo_User.ID_Card_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@ID_Card_No", DBNull.Value);
        }
        if (clsdbo_User.Home_House_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_House_No", clsdbo_User.Home_House_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_House_No", DBNull.Value);
        }
        if (clsdbo_User.Home_Village != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Village", clsdbo_User.Home_Village);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Village", DBNull.Value);
        }
        if (clsdbo_User.Home_Village_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Village_No", clsdbo_User.Home_Village_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Village_No", DBNull.Value);
        }
        if (clsdbo_User.Home_Alley != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Alley", clsdbo_User.Home_Alley);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Alley", DBNull.Value);
        }
        if (clsdbo_User.Home_Road != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Road", clsdbo_User.Home_Road);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Road", DBNull.Value);
        }
        if (clsdbo_User.Home_Sub_district != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Sub_district", clsdbo_User.Home_Sub_district);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Sub_district", DBNull.Value);
        }
        if (clsdbo_User.Home_District != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_District", clsdbo_User.Home_District);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_District", DBNull.Value);
        }
        if (clsdbo_User.Home_Province != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Province", clsdbo_User.Home_Province);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Province", DBNull.Value);
        }
        if (clsdbo_User.Home_Postal_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Postal_ID", clsdbo_User.Home_Postal_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Postal_ID", DBNull.Value);
        }
        if (clsdbo_User.Join_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Join_Date", clsdbo_User.Join_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Join_Date", DBNull.Value);
        }
        if (clsdbo_User.Resign_Date.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Resign_Date", clsdbo_User.Resign_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Resign_Date", DBNull.Value);
        }
        if (clsdbo_User.Price_Group_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", clsdbo_User.Price_Group_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }
        if (clsdbo_User.Payment_Type != null)
        {
            insertCommand.Parameters.AddWithValue("@Payment_Type", clsdbo_User.Payment_Type);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Payment_Type", DBNull.Value);
        }
        if (clsdbo_User.Credit_Term.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Credit_Term", clsdbo_User.Credit_Term);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_Term", DBNull.Value);
        }

        if (clsdbo_User.Credit_Limit.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Credit_Limit", clsdbo_User.Credit_Limit);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Credit_Limit", DBNull.Value);
        }

        if (Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }
        if (clsdbo_User.Last_Modified_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_By", clsdbo_User.Last_Modified_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
        }
        if (clsdbo_User.Title_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Title_ID", clsdbo_User.Title_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Title_ID", DBNull.Value);
        }
        if (clsdbo_User.Present_House_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_House_No", clsdbo_User.Present_House_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_House_No", DBNull.Value);
        }
        if (clsdbo_User.Present_Village != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_Village", clsdbo_User.Present_Village);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_Village", DBNull.Value);
        }
        if (clsdbo_User.Present_Village_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_Village_No", clsdbo_User.Present_Village_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_Village_No", DBNull.Value);
        }
        if (clsdbo_User.Present_Alley != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_Alley", clsdbo_User.Present_Alley);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_Alley", DBNull.Value);
        }
        if (clsdbo_User.Present_Road != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_Road", clsdbo_User.Present_Road);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_Road", DBNull.Value);
        }
        if (clsdbo_User.Present_Sub_District != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_Sub_District", clsdbo_User.Present_Sub_District);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_Sub_District", DBNull.Value);
        }
        if (clsdbo_User.Present_District != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_District", clsdbo_User.Present_District);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_District", DBNull.Value);
        }
        if (clsdbo_User.Present_Province != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_Province", clsdbo_User.Present_Province);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_Province", DBNull.Value);
        }
        if (clsdbo_User.Present_Postal_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Present_Postal_ID", clsdbo_User.Present_Postal_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Present_Postal_ID", DBNull.Value);
        }

        if (clsdbo_User.ShowDashboard != null)
        {
            insertCommand.Parameters.AddWithValue("@ShowDashboard", clsdbo_User.ShowDashboard);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@ShowDashboard", DBNull.Value);
        }



        if (clsdbo_User.Route != null)
        {
            insertCommand.Parameters.AddWithValue("@Route", clsdbo_User.Route);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Route", DBNull.Value);
        }

        if (clsdbo_User.Applied_Document != null)
        {
            insertCommand.Parameters.AddWithValue("@Applied_Document", clsdbo_User.Applied_Document);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Applied_Document", DBNull.Value);
        }

        if (clsdbo_User.Region != null)
        {
            insertCommand.Parameters.AddWithValue("@Region", clsdbo_User.Region);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Region", DBNull.Value);
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


    public static bool Update(dbo_UserClass newdbo_UserClass, string Last_Modified_By)
    {

        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[UserUpdate]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;

        try
        {


            //if (newdbo_UserClass.User_ID != null)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewUser_ID", newdbo_UserClass.User_ID);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewUser_ID", DBNull.Value);
            //}
            if (newdbo_UserClass.First_Name != null)
            {
                updateCommand.Parameters.AddWithValue("@NewFirst_Name", newdbo_UserClass.First_Name);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewFirst_Name", DBNull.Value);
            }
            if (newdbo_UserClass.Last_Name != null)
            {
                updateCommand.Parameters.AddWithValue("@NewLast_Name", newdbo_UserClass.Last_Name);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewLast_Name", string.Empty);
            }
            if (newdbo_UserClass.Home_Phone_No != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Phone_No", newdbo_UserClass.Home_Phone_No);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Phone_No", DBNull.Value);
            }
            if (newdbo_UserClass.Mobile != null)
            {
                updateCommand.Parameters.AddWithValue("@NewMobile", newdbo_UserClass.Mobile);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewMobile", DBNull.Value);
            }
            if (newdbo_UserClass.Position != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPosition", newdbo_UserClass.Position);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPosition", DBNull.Value);
            }
            if (newdbo_UserClass.Section != null)
            {
                updateCommand.Parameters.AddWithValue("@NewSection", newdbo_UserClass.Section);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewSection", DBNull.Value);
            }
            if (newdbo_UserClass.Division != null)
            {
                updateCommand.Parameters.AddWithValue("@NewDivision", newdbo_UserClass.Division);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewDivision", DBNull.Value);
            }
            if (newdbo_UserClass.Manager != null)
            {
                updateCommand.Parameters.AddWithValue("@NewManager", newdbo_UserClass.Manager);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewManager", DBNull.Value);
            }
            if (newdbo_UserClass.User_Group_ID != null)
            {
                updateCommand.Parameters.AddWithValue("@NewUser_Group_ID", newdbo_UserClass.User_Group_ID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewUser_Group_ID", DBNull.Value);
            }
            if (newdbo_UserClass.Role_ID != null)
            {
                updateCommand.Parameters.AddWithValue("@NewRole_ID", newdbo_UserClass.Role_ID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewRole_ID", DBNull.Value);
            }
            if (newdbo_UserClass.Email != null)
            {
                updateCommand.Parameters.AddWithValue("@NewEmail", newdbo_UserClass.Email);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewEmail", DBNull.Value);
            }
            if (newdbo_UserClass.Username != null)
            {
                updateCommand.Parameters.AddWithValue("@NewUsername", newdbo_UserClass.Username);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewUsername", DBNull.Value);
            }


            string m_Password = Encrypt(newdbo_UserClass.Password);


            if (newdbo_UserClass.Password != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPassword", m_Password);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPassword", DBNull.Value);
            }
            if (newdbo_UserClass.Approval_Status_ID != null)
            {
                updateCommand.Parameters.AddWithValue("@NewApproval_Status_ID", newdbo_UserClass.Approval_Status_ID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewApproval_Status_ID", DBNull.Value);
            }



            if (newdbo_UserClass.Status != null)
            {
                bool status = false;

                if (newdbo_UserClass.Status == "Active")
                    status = true;


                updateCommand.Parameters.AddWithValue("@NewStatus", status);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewStatus", DBNull.Value);
            }



            if (!string.IsNullOrEmpty(newdbo_UserClass.CV_CODE.Trim()))
            {
                updateCommand.Parameters.AddWithValue("@NewCV_CODE", newdbo_UserClass.CV_CODE.Trim());
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCV_CODE", DBNull.Value);
            }
            if (newdbo_UserClass.Birthdate.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewBirthdate", newdbo_UserClass.Birthdate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewBirthdate", DBNull.Value);
            }
            if (newdbo_UserClass.ID_Card_No != null)
            {
                updateCommand.Parameters.AddWithValue("@NewID_Card_No", newdbo_UserClass.ID_Card_No);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewID_Card_No", DBNull.Value);
            }
            if (newdbo_UserClass.Home_House_No != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_House_No", newdbo_UserClass.Home_House_No);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_House_No", DBNull.Value);
            }
            if (newdbo_UserClass.Home_Village != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Village", newdbo_UserClass.Home_Village);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Village", DBNull.Value);
            }
            if (newdbo_UserClass.Home_Village_No != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Village_No", newdbo_UserClass.Home_Village_No);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Village_No", DBNull.Value);
            }
            if (newdbo_UserClass.Home_Alley != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Alley", newdbo_UserClass.Home_Alley);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Alley", DBNull.Value);
            }
            if (newdbo_UserClass.Home_Road != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Road", newdbo_UserClass.Home_Road);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Road", DBNull.Value);
            }
            if (newdbo_UserClass.Home_Sub_district != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Sub_district", newdbo_UserClass.Home_Sub_district);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Sub_district", DBNull.Value);
            }
            if (newdbo_UserClass.Home_District != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_District", newdbo_UserClass.Home_District);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_District", DBNull.Value);
            }
            if (newdbo_UserClass.Home_Province != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Province", newdbo_UserClass.Home_Province);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Province", DBNull.Value);
            }
            if (newdbo_UserClass.Home_Postal_ID != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Postal_ID", newdbo_UserClass.Home_Postal_ID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHome_Postal_ID", DBNull.Value);
            }
            if (newdbo_UserClass.Join_Date.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewJoin_Date", newdbo_UserClass.Join_Date);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewJoin_Date", DBNull.Value);
            }
            if (newdbo_UserClass.Resign_Date.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewResign_Date", newdbo_UserClass.Resign_Date);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewResign_Date", DBNull.Value);
            }

            //if (newdbo_UserClass.Price_Group_ID != null)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", newdbo_UserClass.Price_Group_ID);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", DBNull.Value);
            //}


            if (newdbo_UserClass.Payment_Type != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPayment_Type", newdbo_UserClass.Payment_Type);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPayment_Type", DBNull.Value);
            }



            //if (newdbo_UserClass.Credit_Term.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewCredit_Term", newdbo_UserClass.Credit_Term);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewCredit_Term", DBNull.Value);
            //}
            //if (newdbo_UserClass.Credit_Limit.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewCredit_Limit", newdbo_UserClass.Credit_Limit);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewCredit_Limit", DBNull.Value);
            //}



            if (newdbo_UserClass.Applied_Document != null)
            {
                updateCommand.Parameters.AddWithValue("@NewApplied_Document", newdbo_UserClass.Applied_Document);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewApplied_Document", DBNull.Value);
            }
            if (newdbo_UserClass.Created_By != null)
            {
                updateCommand.Parameters.AddWithValue("@NewCreated_By", newdbo_UserClass.Created_By);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCreated_By", DBNull.Value);
            }
            if (Last_Modified_By != null)
            {
                updateCommand.Parameters.AddWithValue("@NewLast_Modified_By", Last_Modified_By);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewLast_Modified_By", DBNull.Value);
            }
            if (newdbo_UserClass.Title_ID != null)
            {
                updateCommand.Parameters.AddWithValue("@NewTitle_ID", newdbo_UserClass.Title_ID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewTitle_ID", DBNull.Value);
            }
            if (newdbo_UserClass.Present_House_No != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_House_No", newdbo_UserClass.Present_House_No);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_House_No", DBNull.Value);
            }
            if (newdbo_UserClass.Present_Village != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Village", newdbo_UserClass.Present_Village);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Village", DBNull.Value);
            }
            if (newdbo_UserClass.Present_Village_No != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Village_No", newdbo_UserClass.Present_Village_No);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Village_No", DBNull.Value);
            }
            if (newdbo_UserClass.Present_Alley != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Alley", newdbo_UserClass.Present_Alley);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Alley", DBNull.Value);
            }
            if (newdbo_UserClass.Present_Road != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Road", newdbo_UserClass.Present_Road);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Road", DBNull.Value);
            }
            if (newdbo_UserClass.Present_Sub_District != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Sub_District", newdbo_UserClass.Present_Sub_District);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Sub_District", DBNull.Value);
            }
            if (newdbo_UserClass.Present_District != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_District", newdbo_UserClass.Present_District);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_District", DBNull.Value);
            }
            if (newdbo_UserClass.Present_Province != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Province", newdbo_UserClass.Present_Province);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Province", DBNull.Value);
            }
            if (newdbo_UserClass.Present_Postal_ID != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Postal_ID", newdbo_UserClass.Present_Postal_ID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPresent_Postal_ID", DBNull.Value);
            }
            if (newdbo_UserClass.User_ID != null)
            {
                updateCommand.Parameters.AddWithValue("@OldUser_ID", newdbo_UserClass.User_ID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
            }


            if (newdbo_UserClass.First_Name_Eng != null)
            {
                updateCommand.Parameters.AddWithValue("@First_Name_Eng", newdbo_UserClass.First_Name_Eng);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@First_Name_Eng", DBNull.Value);
            }
            if (newdbo_UserClass.Last_Name_Eng != null)
            {
                updateCommand.Parameters.AddWithValue("@Last_Name_Eng", newdbo_UserClass.Last_Name_Eng);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Last_Name_Eng", DBNull.Value);
            }
            if (newdbo_UserClass.ShowDashboard != null)
            {
                updateCommand.Parameters.AddWithValue("@ShowDashboard", newdbo_UserClass.ShowDashboard);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ShowDashboard", DBNull.Value);
            }

            if (newdbo_UserClass.Route != null)
            {
                updateCommand.Parameters.AddWithValue("@Route", newdbo_UserClass.Route);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Route", DBNull.Value);
            }


            if (newdbo_UserClass.Region != null)
            {
                updateCommand.Parameters.AddWithValue("@Region", newdbo_UserClass.Region);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Region", DBNull.Value);
            }


            //if (Last_Modified_By != null)
            //{
            //    updateCommand.Parameters.AddWithValue("@Last_Modified_By", Last_Modified_By);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
            //}



            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

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


    public static bool UpdatePassword(dbo_UserClass newdbo_UserClass, string Last_Modified_By)
    {
        //logger.Info(System.Web.HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "[UpdatePassword]";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;

        try
        {

            string m_Password = Encrypt(newdbo_UserClass.Password);


            if (newdbo_UserClass.Password != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPassword", m_Password);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPassword", DBNull.Value);
            }


            if (newdbo_UserClass.User_ID != null)
            {
                updateCommand.Parameters.AddWithValue("@OldUser_ID", newdbo_UserClass.User_ID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
            }


            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

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

    public static bool Delete(string User_ID)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[dbo].[UserDelete_ByUser_ID]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;


        if (User_ID != null)
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", User_ID);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
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

    //[Obsolete]
    //public static bool Delete(dbo_UserClass clsdbo_User)
    //{
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string deleteProcedure = "[dbo].[UserDelete]";
    //    SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
    //    deleteCommand.CommandType = CommandType.StoredProcedure;
    //    if (clsdbo_User.User_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldUser_ID", clsdbo_User.User_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldUser_ID", DBNull.Value);
    //    }
    //    if (clsdbo_User.First_Name != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldFirst_Name", clsdbo_User.First_Name);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldFirst_Name", DBNull.Value);
    //    }
    //    if (clsdbo_User.Last_Name != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldLast_Name", clsdbo_User.Last_Name);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldLast_Name", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_Phone_No != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Phone_No", clsdbo_User.Home_Phone_No);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Phone_No", DBNull.Value);
    //    }
    //    if (clsdbo_User.Mobile != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldMobile", clsdbo_User.Mobile);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldMobile", DBNull.Value);
    //    }
    //    if (clsdbo_User.Position != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPosition", clsdbo_User.Position);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPosition", DBNull.Value);
    //    }
    //    if (clsdbo_User.Section != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldSection", clsdbo_User.Section);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldSection", DBNull.Value);
    //    }
    //    if (clsdbo_User.Division != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldDivision", clsdbo_User.Division);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldDivision", DBNull.Value);
    //    }
    //    if (clsdbo_User.Manager != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldManager", clsdbo_User.Manager);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldManager", DBNull.Value);
    //    }
    //    if (clsdbo_User.User_Group_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldUser_Group_ID", clsdbo_User.User_Group_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldUser_Group_ID", DBNull.Value);
    //    }
    //    if (clsdbo_User.Role_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldRole_ID", clsdbo_User.Role_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldRole_ID", DBNull.Value);
    //    }
    //    if (clsdbo_User.Email != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldEmail", clsdbo_User.Email);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldEmail", DBNull.Value);
    //    }
    //    if (clsdbo_User.Username != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldUsername", clsdbo_User.Username);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldUsername", DBNull.Value);
    //    }
    //    if (clsdbo_User.Password != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPassword", clsdbo_User.Password);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPassword", DBNull.Value);
    //    }
    //    if (clsdbo_User.Approval_Status_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldApproval_Status_ID", clsdbo_User.Approval_Status_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldApproval_Status_ID", DBNull.Value);
    //    }
    //    if (clsdbo_User.Status != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldStatus", clsdbo_User.Status);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldStatus", DBNull.Value);
    //    }
    //    if (clsdbo_User.CV_CODE != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCV_CODE", clsdbo_User.CV_CODE);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCV_CODE", DBNull.Value);
    //    }
    //    //if (clsdbo_User.Birthdate.HasValue == true)
    //    //{
    //    //    deleteCommand.Parameters.AddWithValue("@OldBirthdate", clsdbo_User.Birthdate);
    //    //}
    //    //else
    //    //{
    //    //    deleteCommand.Parameters.AddWithValue("@OldBirthdate", DBNull.Value);
    //    //}
    //    if (clsdbo_User.ID_Card_No != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldID_Card_No", clsdbo_User.ID_Card_No);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldID_Card_No", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_House_No != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_House_No", clsdbo_User.Home_House_No);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_House_No", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_Village != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Village", clsdbo_User.Home_Village);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Village", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_Village_No != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Village_No", clsdbo_User.Home_Village_No);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Village_No", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_Alley != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Alley", clsdbo_User.Home_Alley);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Alley", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_Road != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Road", clsdbo_User.Home_Road);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Road", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_Sub_district != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Sub_district", clsdbo_User.Home_Sub_district);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Sub_district", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_District != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_District", clsdbo_User.Home_District);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_District", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_Province != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Province", clsdbo_User.Home_Province);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Province", DBNull.Value);
    //    }
    //    if (clsdbo_User.Home_Postal_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Postal_ID", clsdbo_User.Home_Postal_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldHome_Postal_ID", DBNull.Value);
    //    }
    //    //if (clsdbo_User.Join_Date.HasValue == true)
    //    //{
    //    //    deleteCommand.Parameters.AddWithValue("@OldJoin_Date", clsdbo_User.Join_Date);
    //    //}
    //    //else
    //    //{
    //    //    deleteCommand.Parameters.AddWithValue("@OldJoin_Date", DBNull.Value);
    //    //}
    //    //if (clsdbo_User.Resign_Date.HasValue == true)
    //    //{
    //    //    deleteCommand.Parameters.AddWithValue("@OldResign_Date", clsdbo_User.Resign_Date);
    //    //}
    //    //else
    //    //{
    //    //    deleteCommand.Parameters.AddWithValue("@OldResign_Date", DBNull.Value);
    //    //}
    //    if (clsdbo_User.Price_Group_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPrice_Group_ID", clsdbo_User.Price_Group_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPrice_Group_ID", DBNull.Value);
    //    }
    //    if (clsdbo_User.Payment_Type != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPayment_Type", clsdbo_User.Payment_Type);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPayment_Type", DBNull.Value);
    //    }
    //    if (clsdbo_User.Credit_Term.HasValue == true)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCredit_Term", clsdbo_User.Credit_Term);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCredit_Term", DBNull.Value);
    //    }
    //    if (clsdbo_User.Credit_Limit.HasValue == true)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCredit_Limit", clsdbo_User.Credit_Limit);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCredit_Limit", DBNull.Value);
    //    }
    //    if (clsdbo_User.Applied_Document != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldApplied_Document", clsdbo_User.Applied_Document);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldApplied_Document", DBNull.Value);
    //    }
    //    if (clsdbo_User.Created_By != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCreated_By", clsdbo_User.Created_By);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldCreated_By", DBNull.Value);
    //    }
    //    if (clsdbo_User.Last_Modified_By != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldLast_Modified_By", clsdbo_User.Last_Modified_By);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldLast_Modified_By", DBNull.Value);
    //    }
    //    if (clsdbo_User.Title_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldTitle_ID", clsdbo_User.Title_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldTitle_ID", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_House_No != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_House_No", clsdbo_User.Present_House_No);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_House_No", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_Village != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Village", clsdbo_User.Present_Village);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Village", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_Village_No != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Village_No", clsdbo_User.Present_Village_No);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Village_No", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_Alley != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Alley", clsdbo_User.Present_Alley);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Alley", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_Road != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Road", clsdbo_User.Present_Road);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Road", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_Sub_District != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Sub_District", clsdbo_User.Present_Sub_District);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Sub_District", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_District != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_District", clsdbo_User.Present_District);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_District", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_Province != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Province", clsdbo_User.Present_Province);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Province", DBNull.Value);
    //    }
    //    if (clsdbo_User.Present_Postal_ID != null)
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Postal_ID", clsdbo_User.Present_Postal_ID);
    //    }
    //    else
    //    {
    //        deleteCommand.Parameters.AddWithValue("@OldPresent_Postal_ID", DBNull.Value);
    //    }
    //    deleteCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
    //    deleteCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
    //    try
    //    {
    //        connection.Open();
    //        deleteCommand.ExecuteNonQuery();
    //        int count = System.Convert.ToInt32(deleteCommand.Parameters["@ReturnValue"].Value);
    //        if (count > 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    catch (SqlException ex)
    //    {
    //        return false;
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //}
}

