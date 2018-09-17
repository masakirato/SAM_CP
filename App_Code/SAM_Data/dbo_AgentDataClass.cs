using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using log4net;
using System.Web;


public class RPT_AgentClass
{
    private String m_CV_Code;
    private String m_Prefix_ID;
    private String m_First_Name;
    private String m_Last_Name;
    private String m_Agent_Type_ID;
    private String m_Home_Phone_No;
    private String m_Mobile;
    private String m_Tax_ID;
    private String m_Email;
    private String m_Fax;
    private String m_Concession_Area;
    private String m_Owner_First_Name;
    private String m_Owner_Last_Name;
    private String m_Owner_Phone_No1;
    private String m_Owner_Phone_No2;
    private String m_Contact_First_Name;
    private String m_Contact_Last_Name;
    private String m_Contact_Phone_No1;
    private String m_Contact_Phone_No2;

    private String m_SD_ID;
    private String m_SM_ID;
    private String m_DM_ID;
    private String m_APV_ID;
    private String m_GM_ID;


    private String m_SD_ID_FullName;
    private String m_SM_ID_FullName;
    private String m_DM_ID_FullName;
    private String m_APV_ID_FullName;
    private String m_GM_ID_FullName;


    private String m_Location_House_No;
    private String m_Location_Village;
    private String m_Location_Village_No;
    private String m_Location_Alley;
    private String m_Location_Road;
    private String m_Location_Sub_district;
    private String m_Location_District;
    private String m_Location_Province;
    private String m_Location_Postal_ID;
    private String m_Invoice_House_No;
    private String m_Invoice_Village;
    private String m_Invoice_Village_No;
    private String m_Invoice_Alley;
    private String m_Invoice_Road;
    private String m_Invoice_Sub_district;
    private String m_Invoice_District;
    private String m_Invoice_Province;
    private String m_Invoice_Postal_ID;
    private Nullable<DateTime> m_Start_Effective_Date;
    private Nullable<DateTime> m_First_Order_Date;
    private Nullable<Boolean> m_Status;
    private Nullable<DateTime> m_Go_out_of_business_Date;
    private String m_Applied_Document;
    private String m_Other_Document;
    private Nullable<Int16> m_Small_Case;
    private Nullable<Int16> m_Large_Case;
    private Nullable<Decimal> m_Pledge_Amount;
    private Nullable<Int16> m_Room_Size;
    private Nullable<Decimal> m_Cash_Deposit;
    private Nullable<Decimal> m_Bank_Guarantee;
    private String m_Bank_ID;
    private String m_Term_of_payment;
    private String m_Remarks;
    private String m_Product_Group_ID;
    private String m_Price_Group_ID;
    private String m_Grade;
    private Nullable<DateTime> m_Grade_Effective_Date;
    private String m_ItemValue;

    private String m_Location_Region;
    private String m_Invoice_Region;

    private String m_AgentName;

    public RPT_AgentClass() { }


    public String SD_ID_FullName
    {
        get
        {
            m_SD_ID_FullName = dbo_UserDataClass.Select_Record(SD_ID).FullName;
            return m_SD_ID_FullName;
        }
        set
        {
            m_SD_ID_FullName = value;
        }
    }
    public String SM_ID_FullName
    {
        get
        {
            m_SM_ID_FullName = dbo_UserDataClass.Select_Record(SM_ID).FullName;
            return m_SM_ID_FullName;
        }
        set
        {
            m_SM_ID_FullName = value;
        }
    }
    public String DM_ID_FullName
    {
        get
        {
            m_DM_ID_FullName = dbo_UserDataClass.Select_Record(DM_ID).FullName;
            return m_DM_ID_FullName;
        }
        set
        {
            m_DM_ID_FullName = value;
        }
    }
    public String APV_ID_FullName
    {
        get
        {
            m_APV_ID_FullName = dbo_UserDataClass.Select_Record(APV_ID).FullName;
            return m_APV_ID_FullName;
        }
        set
        {
            m_APV_ID_FullName = value;
        }
    }
    public String GM_ID_FullName
    {
        get
        {
            m_GM_ID_FullName = dbo_UserDataClass.Select_Record(GM_ID).FullName;
            return m_GM_ID_FullName;
        }
        set
        {
            m_GM_ID_FullName = value;
        }
    }



    public String AgentName
    {
        get
        {
            if (string.IsNullOrEmpty(m_AgentName))
            {
                m_AgentName = m_Prefix_ID + " " + m_First_Name + " " + m_Last_Name;
            }

            return m_AgentName;
        }
        set
        {
            m_AgentName = value;
        }
    }


    public String Location_Region
    {
        get
        {
            return m_Location_Region;
        }
        set
        {
            m_Location_Region = value;
        }
    }

    public String Invoice_Region
    {
        get
        {
            return m_Invoice_Region;
        }
        set
        {
            m_Invoice_Region = value;
        }
    }


    public String CV_Code
    {
        get
        {
            return m_CV_Code == null ? string.Empty : m_CV_Code;
            //return m_CV_Code;
        }
        set
        {
            m_CV_Code = value;
        }
    }

    public String Prefix_ID
    {
        get
        {
            return m_Prefix_ID;
        }
        set
        {
            m_Prefix_ID = value;
        }
    }

    public String First_Name
    {
        get
        {
            return m_First_Name;
        }
        set
        {
            m_First_Name = value;
        }
    }

    public String Last_Name
    {
        get
        {
            return m_Last_Name;
        }
        set
        {
            m_Last_Name = value;
        }
    }

    public String Agent_Type_ID
    {
        get
        {
            return m_Agent_Type_ID;
        }
        set
        {
            m_Agent_Type_ID = value;
        }
    }

    public String Home_Phone_No
    {
        get
        {
            return m_Home_Phone_No;
        }
        set
        {
            m_Home_Phone_No = value;
        }
    }

    public String Mobile
    {
        get
        {
            return m_Mobile;
        }
        set
        {
            m_Mobile = value;
        }
    }

    public String Tax_ID
    {
        get
        {
            return m_Tax_ID;
        }
        set
        {
            m_Tax_ID = value;
        }
    }

    public String Email
    {
        get
        {
            return m_Email;
        }
        set
        {
            m_Email = value;
        }
    }

    public String Fax
    {
        get
        {
            return m_Fax;
        }
        set
        {
            m_Fax = value;
        }
    }

    public String Concession_Area
    {
        get
        {
            return m_Concession_Area;
        }
        set
        {
            m_Concession_Area = value;
        }
    }

    public String Owner_First_Name
    {
        get
        {
            return m_Owner_First_Name;
        }
        set
        {
            m_Owner_First_Name = value;
        }
    }

    public String Owner_Last_Name
    {
        get
        {
            return m_Owner_Last_Name;
        }
        set
        {
            m_Owner_Last_Name = value;
        }
    }

    public String Owner_Phone_No1
    {
        get
        {
            return m_Owner_Phone_No1;
        }
        set
        {
            m_Owner_Phone_No1 = value;
        }
    }

    public String Owner_Phone_No2
    {
        get
        {
            return m_Owner_Phone_No2;
        }
        set
        {
            m_Owner_Phone_No2 = value;
        }
    }

    public String Contact_First_Name
    {
        get
        {
            return m_Contact_First_Name;
        }
        set
        {
            m_Contact_First_Name = value;
        }
    }

    public String Contact_Last_Name
    {
        get
        {
            return m_Contact_Last_Name;
        }
        set
        {
            m_Contact_Last_Name = value;
        }
    }

    public String Contact_Phone_No1
    {
        get
        {
            return m_Contact_Phone_No1;
        }
        set
        {
            m_Contact_Phone_No1 = value;
        }
    }

    public String Contact_Phone_No2
    {
        get
        {
            return m_Contact_Phone_No2;
        }
        set
        {
            m_Contact_Phone_No2 = value;
        }
    }

    public String SD_ID
    {
        get
        {
            return m_SD_ID;
        }
        set
        {
            m_SD_ID = value;
        }
    }

    public String SM_ID
    {
        get
        {
            return m_SM_ID;
        }
        set
        {
            m_SM_ID = value;
        }
    }

    public String DM_ID
    {
        get
        {
            return m_DM_ID;
        }
        set
        {
            m_DM_ID = value;
        }
    }

    public String GM_ID
    {
        get
        {
            return m_GM_ID;
        }
        set
        {
            m_GM_ID = value;
        }
    }

    public String APV_ID
    {
        get
        {
            return m_APV_ID;
        }
        set
        {
            m_APV_ID = value;
        }
    }

    public String Location_House_No
    {
        get
        {
            return m_Location_House_No;
        }
        set
        {
            m_Location_House_No = value;
        }
    }

    public String Location_Village
    {
        get
        {
            return m_Location_Village;
        }
        set
        {
            m_Location_Village = value;
        }
    }

    public String Location_Village_No
    {
        get
        {
            return m_Location_Village_No;
        }
        set
        {
            m_Location_Village_No = value;
        }
    }

    public String Location_Alley
    {
        get
        {
            return m_Location_Alley;
        }
        set
        {
            m_Location_Alley = value;
        }
    }

    public String Location_Road
    {
        get
        {
            return m_Location_Road;
        }
        set
        {
            m_Location_Road = value;
        }
    }

    public String Location_Sub_district
    {
        get
        {
            return m_Location_Sub_district;
        }
        set
        {
            m_Location_Sub_district = value;
        }
    }

    public String Location_District
    {
        get
        {
            return m_Location_District;
        }
        set
        {
            m_Location_District = value;
        }
    }

    public String Location_Province
    {
        get
        {
            return m_Location_Province;
        }
        set
        {
            m_Location_Province = value;
        }
    }

    public String Location_Postal_ID
    {
        get
        {
            return m_Location_Postal_ID;
        }
        set
        {
            m_Location_Postal_ID = value;
        }
    }

    public String Invoice_House_No
    {
        get
        {
            return m_Invoice_House_No;
        }
        set
        {
            m_Invoice_House_No = value;
        }
    }

    public String Invoice_Village
    {
        get
        {
            return m_Invoice_Village;
        }
        set
        {
            m_Invoice_Village = value;
        }
    }

    public String Invoice_Village_No
    {
        get
        {
            return m_Invoice_Village_No;
        }
        set
        {
            m_Invoice_Village_No = value;
        }
    }

    public String Invoice_Alley
    {
        get
        {
            return m_Invoice_Alley;
        }
        set
        {
            m_Invoice_Alley = value;
        }
    }

    public String Invoice_Road
    {
        get
        {
            return m_Invoice_Road;
        }
        set
        {
            m_Invoice_Road = value;
        }
    }

    public String Invoice_Sub_district
    {
        get
        {
            return m_Invoice_Sub_district;
        }
        set
        {
            m_Invoice_Sub_district = value;
        }
    }

    public String Invoice_District
    {
        get
        {
            return m_Invoice_District;
        }
        set
        {
            m_Invoice_District = value;
        }
    }

    public String Invoice_Province
    {
        get
        {
            return m_Invoice_Province;
        }
        set
        {
            m_Invoice_Province = value;
        }
    }

    public String Invoice_Postal_ID
    {
        get
        {
            return m_Invoice_Postal_ID;
        }
        set
        {
            m_Invoice_Postal_ID = value;
        }
    }

    public Nullable<DateTime> Start_Effective_Date
    {
        get
        {
            return m_Start_Effective_Date;
        }
        set
        {
            m_Start_Effective_Date = value;
        }
    }

    public Nullable<DateTime> First_Order_Date
    {
        get
        {
            return m_First_Order_Date;
        }
        set
        {
            m_First_Order_Date = value;
        }
    }

    public Nullable<Boolean> Status
    {
        get
        {
            return m_Status;
        }
        set
        {
            m_Status = value;
        }
    }

    public Nullable<DateTime> Go_out_of_business_Date
    {
        get
        {
            return m_Go_out_of_business_Date;
        }
        set
        {
            m_Go_out_of_business_Date = value;
        }
    }

    public String Applied_Document
    {
        get
        {
            return m_Applied_Document;
        }
        set
        {
            m_Applied_Document = value;
        }
    }

    public String Other_Document
    {
        get
        {
            return m_Other_Document;
        }
        set
        {
            m_Other_Document = value;
        }
    }

    public Nullable<Int16> Small_Case
    {
        get
        {
            return m_Small_Case;
        }
        set
        {
            m_Small_Case = value;
        }
    }

    public Nullable<Int16> Large_Case
    {
        get
        {
            return m_Large_Case;
        }
        set
        {
            m_Large_Case = value;
        }
    }

    public Nullable<Decimal> Pledge_Amount
    {
        get
        {
            return m_Pledge_Amount;
        }
        set
        {
            m_Pledge_Amount = value;
        }
    }

    public Nullable<Int16> Room_Size
    {
        get
        {
            return m_Room_Size;
        }
        set
        {
            m_Room_Size = value;
        }
    }

    public Nullable<Decimal> Cash_Deposit
    {
        get
        {
            return m_Cash_Deposit;
        }
        set
        {
            m_Cash_Deposit = value;
        }
    }

    public Nullable<Decimal> Bank_Guarantee
    {
        get
        {
            return m_Bank_Guarantee;
        }
        set
        {
            m_Bank_Guarantee = value;
        }
    }

    public String Bank_ID
    {
        get
        {
            return m_Bank_ID;
        }
        set
        {
            m_Bank_ID = value;
        }
    }

    public String Term_of_payment
    {
        get
        {
            return m_Term_of_payment;
        }
        set
        {
            m_Term_of_payment = value;
        }
    }

    public String Remarks
    {
        get
        {
            return m_Remarks;
        }
        set
        {
            m_Remarks = value;
        }
    }

    public String Product_Group_ID
    {
        get
        {
            return m_Product_Group_ID;
        }
        set
        {
            m_Product_Group_ID = value;
        }
    }

    public String Price_Group_ID
    {
        get
        {
            return m_Price_Group_ID;
        }
        set
        {
            m_Price_Group_ID = value;
        }
    }

    public String Grade
    {
        get
        {
            return m_Grade;
        }
        set
        {
            m_Grade = value;
        }
    }

    public Nullable<DateTime> Grade_Effective_Date
    {
        get
        {
            return m_Grade_Effective_Date;
        }
        set
        {
            m_Grade_Effective_Date = value;
        }
    }

    public String ItemValue
    {
        get
        {
            return m_ItemValue;
        }
        set
        {
            m_ItemValue = value;
        }
    }
}





public class dbo_AgentDataClass
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static Dictionary<string, string> GetAgentEmp()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetAgentEmp]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();

        Dictionary<string, string> listOfProvince = new Dictionary<string, string>();
        listOfProvince.Add(string.Empty, "==ระบุ==");


        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);

                foreach (DataRow i in dt.Rows)
                {
                    listOfProvince.Add(i["User_ID"].ToString(), i["First_Name"].ToString());
                }

            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return listOfProvince;
        }
        finally
        {
            connection.Close();
        }
        return listOfProvince;
    }

    public static List<String> GetAgentProvince()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[GetAgentProvince]";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();


        List<String> listOfProvince = new List<string>();
        listOfProvince.Add("==ระบุ==");

        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);

                foreach (DataRow i in dt.Rows)
                {
                    listOfProvince.Add(i["Invoice_Province"].ToString());
                }

            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return listOfProvince;
        }
        finally
        {
            connection.Close();
        }
        return listOfProvince;
    }

    public static DataTable SelectAll()
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "[dbo].[AgentSelectAll]";
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





    public static List<dbo_AgentClass> Search(string CV_Code, string Prefix_ID, string Agent_Type_ID, string ConcessionArea, string SP, string SM,
       string DM, string GM, string APV, string Region, string Status, string Grade)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "AgentSearch";
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
        if (!string.IsNullOrEmpty(Prefix_ID))
        {
            selectCommand.Parameters.AddWithValue("@Prefix_ID", Prefix_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Prefix_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Agent_Type_ID))
        {
            selectCommand.Parameters.AddWithValue("@Agent_Type_ID", Agent_Type_ID);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Agent_Type_ID", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(ConcessionArea))
        {
            selectCommand.Parameters.AddWithValue("@ConcessionArea", ConcessionArea);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@ConcessionArea", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(SP))
        {
            selectCommand.Parameters.AddWithValue("@SP", SP);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SP", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(SM))
        {
            selectCommand.Parameters.AddWithValue("@SM", SM);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@SM", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(DM))
        {
            selectCommand.Parameters.AddWithValue("@DM", DM);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@DM", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(GM))
        {
            selectCommand.Parameters.AddWithValue("@GM", GM);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@GM", DBNull.Value);
        }


        if (!string.IsNullOrEmpty(APV))
        {
            selectCommand.Parameters.AddWithValue("@APV", APV);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@APV", DBNull.Value);
        }
        if (!string.IsNullOrEmpty(Region))
        {
            selectCommand.Parameters.AddWithValue("@Region", Region);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
        }

        switch (Status)
        {
            case "ดำเนินธุรกิจอยู่":
                selectCommand.Parameters.AddWithValue("@Status", true);
                break;
            case "ยกเลิกกิจการ":
                selectCommand.Parameters.AddWithValue("@Status", false);
                break;
            default:
                selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                break;
        }


        //if (!string.IsNullOrEmpty(Status))
        //{
        //    selectCommand.Parameters.AddWithValue("@Status", Status);
        //}
        //else
        //{
        //    selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        //}


        if (!string.IsNullOrEmpty(Grade))
        {
            selectCommand.Parameters.AddWithValue("@Grade", Grade);
        }
        else
        {
            selectCommand.Parameters.AddWithValue("@Grade", DBNull.Value);
        }
        List<dbo_AgentClass> item = new List<dbo_AgentClass>();
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

                    item.Add(new dbo_AgentClass()
                    {
                        CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
                        Prefix_ID = reader["Prefix_ID"] is DBNull ? null : reader["Prefix_ID"].ToString(),
                        First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
                        Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
                        Agent_Type_ID = reader["Agent_Type_ID"] is DBNull ? null : reader["Agent_Type_ID"].ToString(),
                        Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString(),
                        Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString(),
                        Tax_ID = reader["Tax_ID"] is DBNull ? null : reader["Tax_ID"].ToString(),
                        Email = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
                        Fax = reader["Fax"] is DBNull ? null : reader["Fax"].ToString(),
                        Concession_Area = reader["Concession_Area"] is DBNull ? null : reader["Concession_Area"].ToString(),
                        Owner_First_Name = reader["Owner_First_Name"] is DBNull ? null : reader["Owner_First_Name"].ToString(),
                        Owner_Last_Name = reader["Owner_Last_Name"] is DBNull ? null : reader["Owner_Last_Name"].ToString(),
                        Owner_Phone_No1 = reader["Owner_Phone_No1"] is DBNull ? null : reader["Owner_Phone_No1"].ToString(),
                        Owner_Phone_No2 = reader["Owner_Phone_No2"] is DBNull ? null : reader["Owner_Phone_No2"].ToString(),
                        Contact_First_Name = reader["Contact_First_Name"] is DBNull ? null : reader["Contact_First_Name"].ToString(),
                        Contact_Last_Name = reader["Contact_Last_Name"] is DBNull ? null : reader["Contact_Last_Name"].ToString(),
                        Contact_Phone_No1 = reader["Contact_Phone_No1"] is DBNull ? null : reader["Contact_Phone_No1"].ToString(),
                        Contact_Phone_No2 = reader["Contact_Phone_No2"] is DBNull ? null : reader["Contact_Phone_No2"].ToString(),
                        SD_ID = reader["SD_ID"] is DBNull ? null : reader["SD_ID"].ToString().Trim(),
                        SM_ID = reader["SM_ID"] is DBNull ? null : reader["SM_ID"].ToString().Trim(),
                        DM_ID = reader["DM_ID"] is DBNull ? null : reader["DM_ID"].ToString().Trim(),
                        Location_House_No = reader["Location_House_No"] is DBNull ? null : reader["Location_House_No"].ToString(),
                        Location_Village = reader["Location_Village"] is DBNull ? null : reader["Location_Village"].ToString(),
                        Location_Village_No = reader["Location_Village_No"] is DBNull ? null : reader["Location_Village_No"].ToString(),
                        Location_Alley = reader["Location_Alley"] is DBNull ? null : reader["Location_Alley"].ToString(),
                        Location_Road = reader["Location_Road"] is DBNull ? null : reader["Location_Road"].ToString(),
                        Location_Sub_district = reader["Location_Sub_district"] is DBNull ? null : reader["Location_Sub_district"].ToString(),
                        Location_District = reader["Location_District"] is DBNull ? null : reader["Location_District"].ToString(),
                        Location_Province = reader["Location_Province"] is DBNull ? null : reader["Location_Province"].ToString(),
                        Location_Postal_ID = reader["Location_Postal_ID"] is DBNull ? null : reader["Location_Postal_ID"].ToString(),
                        Invoice_House_No = reader["Invoice_House_No"] is DBNull ? null : reader["Invoice_House_No"].ToString(),
                        Invoice_Village = reader["Invoice_Village"] is DBNull ? null : reader["Invoice_Village"].ToString(),
                        Invoice_Village_No = reader["Invoice_Village_No"] is DBNull ? null : reader["Invoice_Village_No"].ToString(),
                        Invoice_Alley = reader["Invoice_Alley"] is DBNull ? null : reader["Invoice_Alley"].ToString(),
                        Invoice_Road = reader["Invoice_Road"] is DBNull ? null : reader["Invoice_Road"].ToString(),
                        Invoice_Sub_district = reader["Invoice_Sub_district"] is DBNull ? null : reader["Invoice_Sub_district"].ToString(),
                        Invoice_District = reader["Invoice_District"] is DBNull ? null : reader["Invoice_District"].ToString(),
                        Invoice_Province = reader["Invoice_Province"] is DBNull ? null : reader["Invoice_Province"].ToString(),
                        Invoice_Postal_ID = reader["Invoice_Postal_ID"] is DBNull ? null : reader["Invoice_Postal_ID"].ToString(),
                        Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Start_Effective_Date"]).Value,
                        First_Order_Date = reader["First_Order_Date"] is DBNull ? (DateTime?)(DateTime?)null : ((DateTime?)reader["First_Order_Date"]).Value,
                        Status = reader["Status"] is DBNull ? null : (Boolean?)reader["Status"],
                        Go_out_of_business_Date = reader["Go_out_of_business_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Go_out_of_business_Date"]).Value,
                        Applied_Document = reader["Applied_Document"] is DBNull ? null : reader["Applied_Document"].ToString(),
                        Other_Document = reader["Other_Document"] is DBNull ? null : reader["Other_Document"].ToString(),
                        Small_Case = reader["Small_Case"] is DBNull ? null : (Int16?)reader["Small_Case"],
                        Large_Case = reader["Large_Case"] is DBNull ? null : (Int16?)reader["Large_Case"],
                        Pledge_Amount = reader["Pledge_Amount"] is DBNull ? null : (Decimal?)reader["Pledge_Amount"],
                        Room_Size = reader["Room_Size"] is DBNull ? null : (Int16?)reader["Room_Size"],
                        Cash_Deposit = reader["Cash_Deposit"] is DBNull ? null : (Decimal?)reader["Cash_Deposit"],
                        Bank_Guarantee = reader["Bank_Guarantee"] is DBNull ? null : (Decimal?)reader["Bank_Guarantee"],
                        Bank_ID = reader["Bank_ID"] is DBNull ? null : reader["Bank_ID"].ToString(),


                        Term_of_payment = reader["Term_of_payment"] is DBNull ? null : reader["Term_of_payment"].ToString(),


                        Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString(),
                        Product_Group_ID = reader["Product_Group_ID"] is DBNull ? null : reader["Product_Group_ID"].ToString(),
                        Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString(),
                        Grade = reader["Grade"] is DBNull ? null : reader["Grade"].ToString(),
                        Grade_Effective_Date = reader["Grade_Effective_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Grade_Effective_Date"]).Value,


                        Location_Region = reader["Location_Region"] is DBNull ? null : reader["Location_Region"].ToString(),
                        Invoice_Region = reader["Invoice_Region"] is DBNull ? null : reader["Invoice_Region"].ToString(),
                        GM_ID = reader["GM_ID"] is DBNull ? null : reader["GM_ID"].ToString().Trim(),
                        APV_ID = reader["APV_ID"] is DBNull ? null : reader["APV_ID"].ToString().Trim(),
                        Location_ID = reader["Location_ID"] is DBNull ? null : reader["Location_ID"].ToString()

                    }
                        );
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

        }
        finally
        {
            connection.Close();
        }

        // search ==> List<dbo_AgentClass> 



        //  item ==> rpt_agent


        return item;
    }




    // public static List<RPT_AgentClass> Search1(string CV_Code, string Prefix_ID, string Agent_Type_ID, string ConcessionArea, string SP, string SM,
    //string DM, string GM, string APV, string Region, string Status, string Grade)
    // {

    //     SqlConnection connection = SAMDataClass.GetConnection();
    //     string selectProcedure = "AgentSearch";
    //     SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
    //     selectCommand.CommandType = CommandType.StoredProcedure;

    //     if (!string.IsNullOrEmpty(CV_Code))
    //     {
    //         selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
    //     }
    //     if (!string.IsNullOrEmpty(Prefix_ID))
    //     {
    //         selectCommand.Parameters.AddWithValue("@Prefix_ID", Prefix_ID);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@Prefix_ID", DBNull.Value);
    //     }
    //     if (!string.IsNullOrEmpty(Agent_Type_ID))
    //     {
    //         selectCommand.Parameters.AddWithValue("@Agent_Type_ID", Agent_Type_ID);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@Agent_Type_ID", DBNull.Value);
    //     }
    //     if (!string.IsNullOrEmpty(ConcessionArea))
    //     {
    //         selectCommand.Parameters.AddWithValue("@ConcessionArea", ConcessionArea);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@ConcessionArea", DBNull.Value);
    //     }
    //     if (!string.IsNullOrEmpty(SP))
    //     {
    //         selectCommand.Parameters.AddWithValue("@SP", SP);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@SP", DBNull.Value);
    //     }
    //     if (!string.IsNullOrEmpty(SM))
    //     {
    //         selectCommand.Parameters.AddWithValue("@SM", SM);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@SM", DBNull.Value);
    //     }
    //     if (!string.IsNullOrEmpty(DM))
    //     {
    //         selectCommand.Parameters.AddWithValue("@DM", DM);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@DM", DBNull.Value);
    //     }
    //     if (!string.IsNullOrEmpty(GM))
    //     {
    //         selectCommand.Parameters.AddWithValue("@GM", GM);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@GM", DBNull.Value);
    //     }


    //     if (!string.IsNullOrEmpty(APV))
    //     {
    //         selectCommand.Parameters.AddWithValue("@APV", APV);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@APV", DBNull.Value);
    //     }
    //     if (!string.IsNullOrEmpty(Region))
    //     {
    //         selectCommand.Parameters.AddWithValue("@Region", Region);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@Region", DBNull.Value);
    //     }

    //     switch (Status)
    //     {
    //         case "ดำเนินธุรกิจอยู่":
    //             selectCommand.Parameters.AddWithValue("@Status", true);
    //             break;
    //         case "ยกเลิกกิจการ":
    //             selectCommand.Parameters.AddWithValue("@Status", false);
    //             break;
    //         default:
    //             selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
    //             break;
    //     }


    //     //if (!string.IsNullOrEmpty(Status))
    //     //{
    //     //    selectCommand.Parameters.AddWithValue("@Status", Status);
    //     //}
    //     //else
    //     //{
    //     //    selectCommand.Parameters.AddWithValue("@Status", DBNull.Value);
    //     //}


    //     if (!string.IsNullOrEmpty(Grade))
    //     {
    //         selectCommand.Parameters.AddWithValue("@Grade", Grade);
    //     }
    //     else
    //     {
    //         selectCommand.Parameters.AddWithValue("@Grade", DBNull.Value);
    //     }
    //     List<RPT_AgentClass> item = new List<RPT_AgentClass>();
    //     DataTable dt = new DataTable();
    //     try
    //     {
    //         connection.Open();
    //         SqlDataReader reader1 = selectCommand.ExecuteReader();
    //         if (reader1.HasRows)
    //         {
    //             dt.Load(reader1);

    //             foreach (DataRow reader in dt.Rows)
    //             {

    //                 item.Add(new RPT_AgentClass()
    //                 {
    //                     CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
    //                     Prefix_ID = reader["Prefix_ID"] is DBNull ? null : reader["Prefix_ID"].ToString(),
    //                     First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
    //                     Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
    //                     Agent_Type_ID = reader["Agent_Type_ID"] is DBNull ? null : reader["Agent_Type_ID"].ToString(),
    //                     Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString(),
    //                     Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString(),
    //                     Tax_ID = reader["Tax_ID"] is DBNull ? null : reader["Tax_ID"].ToString(),
    //                     Email = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
    //                     Fax = reader["Fax"] is DBNull ? null : reader["Fax"].ToString(),
    //                     Concession_Area = reader["Concession_Area"] is DBNull ? null : reader["Concession_Area"].ToString(),
    //                     Owner_First_Name = reader["Owner_First_Name"] is DBNull ? null : reader["Owner_First_Name"].ToString(),
    //                     Owner_Last_Name = reader["Owner_Last_Name"] is DBNull ? null : reader["Owner_Last_Name"].ToString(),
    //                     Owner_Phone_No1 = reader["Owner_Phone_No1"] is DBNull ? null : reader["Owner_Phone_No1"].ToString(),
    //                     Owner_Phone_No2 = reader["Owner_Phone_No2"] is DBNull ? null : reader["Owner_Phone_No2"].ToString(),
    //                     Contact_First_Name = reader["Contact_First_Name"] is DBNull ? null : reader["Contact_First_Name"].ToString(),
    //                     Contact_Last_Name = reader["Contact_Last_Name"] is DBNull ? null : reader["Contact_Last_Name"].ToString(),
    //                     Contact_Phone_No1 = reader["Contact_Phone_No1"] is DBNull ? null : reader["Contact_Phone_No1"].ToString(),
    //                     Contact_Phone_No2 = reader["Contact_Phone_No2"] is DBNull ? null : reader["Contact_Phone_No2"].ToString(),
    //                     SD_ID = reader["SD_ID"] is DBNull ? null : reader["SD_ID"].ToString().Trim(),
    //                     SM_ID = reader["SM_ID"] is DBNull ? null : reader["SM_ID"].ToString().Trim(),
    //                     DM_ID = reader["DM_ID"] is DBNull ? null : reader["DM_ID"].ToString().Trim(),
    //                     Location_House_No = reader["Location_House_No"] is DBNull ? null : reader["Location_House_No"].ToString(),
    //                     Location_Village = reader["Location_Village"] is DBNull ? null : reader["Location_Village"].ToString(),
    //                     Location_Village_No = reader["Location_Village_No"] is DBNull ? null : reader["Location_Village_No"].ToString(),
    //                     Location_Alley = reader["Location_Alley"] is DBNull ? null : reader["Location_Alley"].ToString(),
    //                     Location_Road = reader["Location_Road"] is DBNull ? null : reader["Location_Road"].ToString(),
    //                     Location_Sub_district = reader["Location_Sub_district"] is DBNull ? null : reader["Location_Sub_district"].ToString(),
    //                     Location_District = reader["Location_District"] is DBNull ? null : reader["Location_District"].ToString(),
    //                     Location_Province = reader["Location_Province"] is DBNull ? null : reader["Location_Province"].ToString(),
    //                     Location_Postal_ID = reader["Location_Postal_ID"] is DBNull ? null : reader["Location_Postal_ID"].ToString(),
    //                     Invoice_House_No = reader["Invoice_House_No"] is DBNull ? null : reader["Invoice_House_No"].ToString(),
    //                     Invoice_Village = reader["Invoice_Village"] is DBNull ? null : reader["Invoice_Village"].ToString(),
    //                     Invoice_Village_No = reader["Invoice_Village_No"] is DBNull ? null : reader["Invoice_Village_No"].ToString(),
    //                     Invoice_Alley = reader["Invoice_Alley"] is DBNull ? null : reader["Invoice_Alley"].ToString(),
    //                     Invoice_Road = reader["Invoice_Road"] is DBNull ? null : reader["Invoice_Road"].ToString(),
    //                     Invoice_Sub_district = reader["Invoice_Sub_district"] is DBNull ? null : reader["Invoice_Sub_district"].ToString(),
    //                     Invoice_District = reader["Invoice_District"] is DBNull ? null : reader["Invoice_District"].ToString(),
    //                     Invoice_Province = reader["Invoice_Province"] is DBNull ? null : reader["Invoice_Province"].ToString(),
    //                     Invoice_Postal_ID = reader["Invoice_Postal_ID"] is DBNull ? null : reader["Invoice_Postal_ID"].ToString(),
    //                     Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? DateTime.MinValue : ((DateTime)reader["Start_Effective_Date"]),
    //                     First_Order_Date = reader["First_Order_Date"] is DBNull ? DateTime.MinValue : ((DateTime)reader["First_Order_Date"]),
    //                     Status = reader["Status"] is DBNull ? false : (Boolean)reader["Status"],
    //                     Go_out_of_business_Date = reader["Go_out_of_business_Date"] is DBNull ? DateTime.MinValue : ((DateTime?)reader["Go_out_of_business_Date"]).Value,
    //                     Applied_Document = reader["Applied_Document"] is DBNull ? null : reader["Applied_Document"].ToString(),
    //                     Other_Document = reader["Other_Document"] is DBNull ? null : reader["Other_Document"].ToString(),
    //                     Small_Case = reader["Small_Case"] is DBNull ? Int16.Parse("0") : (Int16)reader["Small_Case"],
    //                     Large_Case = reader["Large_Case"] is DBNull ? Int16.Parse("0") : (Int16)reader["Large_Case"],
    //                     Pledge_Amount = reader["Pledge_Amount"] is DBNull ? 0 : (Decimal)reader["Pledge_Amount"],
    //                     Room_Size = reader["Room_Size"] is DBNull ? Int16.Parse("0") : (Int16)reader["Room_Size"],
    //                     Cash_Deposit = reader["Cash_Deposit"] is DBNull ? 0 : (Decimal)reader["Cash_Deposit"],
    //                     Bank_Guarantee = reader["Bank_Guarantee"] is DBNull ? 0 : (Decimal)reader["Bank_Guarantee"],
    //                     Bank_ID = reader["Bank_ID"] is DBNull ? null : reader["Bank_ID"].ToString(),


    //                     Term_of_payment = reader["Term_of_payment"] is DBNull ? null : reader["Term_of_payment"].ToString(),


    //                     Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString(),
    //                     Product_Group_ID = reader["Product_Group_ID"] is DBNull ? null : reader["Product_Group_ID"].ToString(),
    //                     Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString(),
    //                     Grade = reader["Grade"] is DBNull ? null : reader["Grade"].ToString(),
    //                     Grade_Effective_Date = reader["Grade_Effective_Date"] is DBNull ? (DateTime.MinValue) : ((DateTime)reader["Grade_Effective_Date"]),


    //                     Location_Region = reader["Location_Region"] is DBNull ? null : reader["Location_Region"].ToString(),
    //                     Invoice_Region = reader["Invoice_Region"] is DBNull ? null : reader["Invoice_Region"].ToString(),
    //                     GM_ID = reader["GM_ID"] is DBNull ? null : reader["GM_ID"].ToString().Trim(),
    //                     APV_ID = reader["APV_ID"] is DBNull ? null : reader["APV_ID"].ToString().Trim()
    //                 }
    //                     );
    //             }


    //         }
    //         reader1.Close();
    //     }
    //     catch (SqlException ex)
    //     {
    //         return item;
    //     }
    //     catch (Exception ex)
    //     {

    //     }
    //     finally
    //     {
    //         connection.Close();
    //     }
    //     return item;
    // }



    //[Obsolete]
    //public static List<dbo_AgentClass> Search(string CV_Code, string First_Name, string Agent_Type_ID,
    //    DateTime? Start_Effective_Date, DateTime? End_Effective_Date, string Invoice_Province)
    //{
    //    SqlConnection connection = SAMDataClass.GetConnection();
    //    string selectProcedure = "AgentSearch";
    //    SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
    //    selectCommand.CommandType = CommandType.StoredProcedure;


    //    if (!string.IsNullOrEmpty(CV_Code))
    //    {
    //        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
    //    }
    //    else
    //    {
    //        selectCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
    //    }
    //    if (!string.IsNullOrEmpty(First_Name))
    //    {
    //        selectCommand.Parameters.AddWithValue("@First_Name", First_Name);
    //    }
    //    else
    //    {
    //        selectCommand.Parameters.AddWithValue("@First_Name", DBNull.Value);
    //    }
    //    if (!string.IsNullOrEmpty(Agent_Type_ID))
    //    {
    //        selectCommand.Parameters.AddWithValue("@Agent_Type_ID", int.Parse(Agent_Type_ID));
    //    }
    //    else
    //    {
    //        selectCommand.Parameters.AddWithValue("@Agent_Type_ID", DBNull.Value);
    //    }





    //    if (Start_Effective_Date.HasValue)
    //    {
    //        selectCommand.Parameters.AddWithValue("@Start_Effective_Date", Start_Effective_Date);
    //    }
    //    else
    //    {
    //        selectCommand.Parameters.AddWithValue("@Start_Effective_Date", DateTime.Now.AddYears(-100));
    //    }


    //    if (End_Effective_Date.HasValue)
    //    {
    //        selectCommand.Parameters.AddWithValue("@End_Effective_Date", End_Effective_Date);
    //    }
    //    else
    //    {
    //        selectCommand.Parameters.AddWithValue("@End_Effective_Date", DateTime.Now.AddYears(100));
    //    }




    //    if (!string.IsNullOrEmpty(Invoice_Province))
    //    {
    //        selectCommand.Parameters.AddWithValue("@Invoice_Province", Invoice_Province);
    //    }
    //    else
    //    {
    //        selectCommand.Parameters.AddWithValue("@Invoice_Province", DBNull.Value);
    //    }

    //    List<dbo_AgentClass> item = new List<dbo_AgentClass>();
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        connection.Open();
    //        SqlDataReader reader1 = selectCommand.ExecuteReader();
    //        if (reader1.HasRows)
    //        {
    //            dt.Load(reader1);

    //            foreach (DataRow reader in dt.Rows)
    //            {

    //                item.Add(new dbo_AgentClass()
    //                {
    //                    CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString(),
    //                    Prefix_ID = reader["Prefix_ID"] is DBNull ? null : reader["Prefix_ID"].ToString(),
    //                    First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString(),
    //                    Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString(),
    //                    Agent_Type_ID = reader["Agent_Type_ID"] is DBNull ? null : reader["Agent_Type_ID"].ToString(),
    //                    Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString(),
    //                    Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString(),
    //                    Tax_ID = reader["Tax_ID"] is DBNull ? null : reader["Tax_ID"].ToString(),
    //                    Email = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
    //                    Fax = reader["Fax"] is DBNull ? null : reader["Fax"].ToString(),
    //                    Concession_Area = reader["Concession_Area"] is DBNull ? null : reader["Concession_Area"].ToString(),
    //                    Owner_First_Name = reader["Owner_First_Name"] is DBNull ? null : reader["Owner_First_Name"].ToString(),
    //                    Owner_Last_Name = reader["Owner_Last_Name"] is DBNull ? null : reader["Owner_Last_Name"].ToString(),
    //                    Owner_Phone_No1 = reader["Owner_Phone_No1"] is DBNull ? null : reader["Owner_Phone_No1"].ToString(),
    //                    Owner_Phone_No2 = reader["Owner_Phone_No2"] is DBNull ? null : reader["Owner_Phone_No2"].ToString(),
    //                    Contact_First_Name = reader["Contact_First_Name"] is DBNull ? null : reader["Contact_First_Name"].ToString(),
    //                    Contact_Last_Name = reader["Contact_Last_Name"] is DBNull ? null : reader["Contact_Last_Name"].ToString(),
    //                    Contact_Phone_No1 = reader["Contact_Phone_No1"] is DBNull ? null : reader["Contact_Phone_No1"].ToString(),
    //                    Contact_Phone_No2 = reader["Contact_Phone_No2"] is DBNull ? null : reader["Contact_Phone_No2"].ToString(),
    //                    SD_ID = reader["SD_ID"] is DBNull ? null : reader["SD_ID"].ToString(),
    //                    SM_ID = reader["SM_ID"] is DBNull ? null : reader["SM_ID"].ToString(),
    //                    DM_ID = reader["DM_ID"] is DBNull ? null : reader["DM_ID"].ToString(),
    //                    Location_House_No = reader["Location_House_No"] is DBNull ? null : reader["Location_House_No"].ToString(),
    //                    Location_Village = reader["Location_Village"] is DBNull ? null : reader["Location_Village"].ToString(),
    //                    Location_Village_No = reader["Location_Village_No"] is DBNull ? null : reader["Location_Village_No"].ToString(),
    //                    Location_Alley = reader["Location_Alley"] is DBNull ? null : reader["Location_Alley"].ToString(),
    //                    Location_Road = reader["Location_Road"] is DBNull ? null : reader["Location_Road"].ToString(),
    //                    Location_Sub_district = reader["Location_Sub_district"] is DBNull ? null : reader["Location_Sub_district"].ToString(),
    //                    Location_District = reader["Location_District"] is DBNull ? null : reader["Location_District"].ToString(),
    //                    Location_Province = reader["Location_Province"] is DBNull ? null : reader["Location_Province"].ToString(),
    //                    Location_Postal_ID = reader["Location_Postal_ID"] is DBNull ? null : reader["Location_Postal_ID"].ToString(),
    //                    Invoice_House_No = reader["Invoice_House_No"] is DBNull ? null : reader["Invoice_House_No"].ToString(),
    //                    Invoice_Village = reader["Invoice_Village"] is DBNull ? null : reader["Invoice_Village"].ToString(),
    //                    Invoice_Village_No = reader["Invoice_Village_No"] is DBNull ? null : reader["Invoice_Village_No"].ToString(),
    //                    Invoice_Alley = reader["Invoice_Alley"] is DBNull ? null : reader["Invoice_Alley"].ToString(),
    //                    Invoice_Road = reader["Invoice_Road"] is DBNull ? null : reader["Invoice_Road"].ToString(),
    //                    Invoice_Sub_district = reader["Invoice_Sub_district"] is DBNull ? null : reader["Invoice_Sub_district"].ToString(),
    //                    Invoice_District = reader["Invoice_District"] is DBNull ? null : reader["Invoice_District"].ToString(),
    //                    Invoice_Province = reader["Invoice_Province"] is DBNull ? null : reader["Invoice_Province"].ToString(),
    //                    Invoice_Postal_ID = reader["Invoice_Postal_ID"] is DBNull ? null : reader["Invoice_Postal_ID"].ToString(),
    //                    Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Start_Effective_Date"]).Value,
    //                    First_Order_Date = reader["First_Order_Date"] is DBNull ? (DateTime?)(DateTime?)null : ((DateTime?)reader["First_Order_Date"]).Value,
    //                    Status = reader["Status"] is DBNull ? null : (Boolean?)reader["Status"],
    //                    Go_out_of_business_Date = reader["Go_out_of_business_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Go_out_of_business_Date"]).Value,
    //                    Applied_Document = reader["Applied_Document"] is DBNull ? null : reader["Applied_Document"].ToString(),
    //                    Other_Document = reader["Other_Document"] is DBNull ? null : reader["Other_Document"].ToString(),
    //                    Small_Case = reader["Small_Case"] is DBNull ? null : (Int16?)reader["Small_Case"],
    //                    Large_Case = reader["Large_Case"] is DBNull ? null : (Int16?)reader["Large_Case"],
    //                    Pledge_Amount = reader["Pledge_Amount"] is DBNull ? null : (Decimal?)reader["Pledge_Amount"],
    //                    Room_Size = reader["Room_Size"] is DBNull ? null : (Int16?)reader["Room_Size"],
    //                    Cash_Deposit = reader["Cash_Deposit"] is DBNull ? null : (Decimal?)reader["Cash_Deposit"],
    //                    Bank_Guarantee = reader["Bank_Guarantee"] is DBNull ? null : (Decimal?)reader["Bank_Guarantee"],
    //                    Bank_ID = reader["Bank_ID"] is DBNull ? null : reader["Bank_ID"].ToString(),
    //                    Term_of_payment = reader["Term_of_payment"] is DBNull ? null : (Byte?)reader["Term_of_payment"],
    //                    Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString(),
    //                    Product_Group_ID = reader["Product_Group_ID"] is DBNull ? null : reader["Product_Group_ID"].ToString(),
    //                    Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString(),
    //                    Grade = reader["Grade"] is DBNull ? null : reader["Grade"].ToString(),
    //                    Grade_Effective_Date = reader["Grade_Effective_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Grade_Effective_Date"]).Value,

    //                }
    //                    );
    //            }




    //        }
    //        reader1.Close();
    //    }
    //    catch (SqlException ex)
    //    {
    //        return item;
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //    return item;
    //}



    public static dbo_AgentClass Select_Record(string CV_Code)
    {
        dbo_AgentClass clsdbo_Agent = new dbo_AgentClass();
        SqlConnection connection = SAMDataClass.GetConnection();
        string selectProcedure = "AgentSelect";
        SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        selectCommand.CommandType = CommandType.StoredProcedure;
        selectCommand.Parameters.AddWithValue("@CV_Code", CV_Code);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsdbo_Agent.CV_Code = reader["CV_Code"] is DBNull ? null : reader["CV_Code"].ToString();
                clsdbo_Agent.Prefix_ID = reader["Prefix_ID"] is DBNull ? null : reader["Prefix_ID"].ToString();
                clsdbo_Agent.First_Name = reader["First_Name"] is DBNull ? null : reader["First_Name"].ToString();
                clsdbo_Agent.Last_Name = reader["Last_Name"] is DBNull ? null : reader["Last_Name"].ToString();
                clsdbo_Agent.Agent_Type_ID = reader["Agent_Type_ID"] is DBNull ? null : reader["Agent_Type_ID"].ToString();
                clsdbo_Agent.Home_Phone_No = reader["Home_Phone_No"] is DBNull ? null : reader["Home_Phone_No"].ToString();
                clsdbo_Agent.Mobile = reader["Mobile"] is DBNull ? null : reader["Mobile"].ToString();
                clsdbo_Agent.Tax_ID = reader["Tax_ID"] is DBNull ? null : reader["Tax_ID"].ToString();
                clsdbo_Agent.Email = reader["Email"] is DBNull ? null : reader["Email"].ToString();
                clsdbo_Agent.Fax = reader["Fax"] is DBNull ? null : reader["Fax"].ToString();
                clsdbo_Agent.Concession_Area = reader["Concession_Area"] is DBNull ? null : reader["Concession_Area"].ToString();
                clsdbo_Agent.Owner_First_Name = reader["Owner_First_Name"] is DBNull ? null : reader["Owner_First_Name"].ToString();
                clsdbo_Agent.Owner_Last_Name = reader["Owner_Last_Name"] is DBNull ? null : reader["Owner_Last_Name"].ToString();
                clsdbo_Agent.Owner_Phone_No1 = reader["Owner_Phone_No1"] is DBNull ? null : reader["Owner_Phone_No1"].ToString();
                clsdbo_Agent.Owner_Phone_No2 = reader["Owner_Phone_No2"] is DBNull ? null : reader["Owner_Phone_No2"].ToString();
                clsdbo_Agent.Contact_First_Name = reader["Contact_First_Name"] is DBNull ? null : reader["Contact_First_Name"].ToString();
                clsdbo_Agent.Contact_Last_Name = reader["Contact_Last_Name"] is DBNull ? null : reader["Contact_Last_Name"].ToString();
                clsdbo_Agent.Contact_Phone_No1 = reader["Contact_Phone_No1"] is DBNull ? null : reader["Contact_Phone_No1"].ToString();
                clsdbo_Agent.Contact_Phone_No2 = reader["Contact_Phone_No2"] is DBNull ? null : reader["Contact_Phone_No2"].ToString();
                clsdbo_Agent.SD_ID = reader["SD_ID"] is DBNull ? null : reader["SD_ID"].ToString();
                clsdbo_Agent.SM_ID = reader["SM_ID"] is DBNull ? null : reader["SM_ID"].ToString();
                clsdbo_Agent.DM_ID = reader["DM_ID"] is DBNull ? null : reader["DM_ID"].ToString();
                clsdbo_Agent.Location_House_No = reader["Location_House_No"] is DBNull ? null : reader["Location_House_No"].ToString();
                clsdbo_Agent.Location_Village = reader["Location_Village"] is DBNull ? null : reader["Location_Village"].ToString();
                clsdbo_Agent.Location_Village_No = reader["Location_Village_No"] is DBNull ? null : reader["Location_Village_No"].ToString();
                clsdbo_Agent.Location_Alley = reader["Location_Alley"] is DBNull ? null : reader["Location_Alley"].ToString();
                clsdbo_Agent.Location_Road = reader["Location_Road"] is DBNull ? null : reader["Location_Road"].ToString();
                clsdbo_Agent.Location_Sub_district = reader["Location_Sub_district"] is DBNull ? null : reader["Location_Sub_district"].ToString();
                clsdbo_Agent.Location_District = reader["Location_District"] is DBNull ? null : reader["Location_District"].ToString();
                clsdbo_Agent.Location_Province = reader["Location_Province"] is DBNull ? null : reader["Location_Province"].ToString();
                clsdbo_Agent.Location_Postal_ID = reader["Location_Postal_ID"] is DBNull ? null : reader["Location_Postal_ID"].ToString();
                clsdbo_Agent.Invoice_House_No = reader["Invoice_House_No"] is DBNull ? null : reader["Invoice_House_No"].ToString();
                clsdbo_Agent.Invoice_Village = reader["Invoice_Village"] is DBNull ? null : reader["Invoice_Village"].ToString();
                clsdbo_Agent.Invoice_Village_No = reader["Invoice_Village_No"] is DBNull ? null : reader["Invoice_Village_No"].ToString();
                clsdbo_Agent.Invoice_Alley = reader["Invoice_Alley"] is DBNull ? null : reader["Invoice_Alley"].ToString();
                clsdbo_Agent.Invoice_Road = reader["Invoice_Road"] is DBNull ? null : reader["Invoice_Road"].ToString();
                clsdbo_Agent.Invoice_Sub_district = reader["Invoice_Sub_district"] is DBNull ? null : reader["Invoice_Sub_district"].ToString();
                clsdbo_Agent.Invoice_District = reader["Invoice_District"] is DBNull ? null : reader["Invoice_District"].ToString();
                clsdbo_Agent.Invoice_Province = reader["Invoice_Province"] is DBNull ? null : reader["Invoice_Province"].ToString();
                clsdbo_Agent.Invoice_Postal_ID = reader["Invoice_Postal_ID"] is DBNull ? null : reader["Invoice_Postal_ID"].ToString();
                clsdbo_Agent.Start_Effective_Date = reader["Start_Effective_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Start_Effective_Date"]).Value;
                clsdbo_Agent.First_Order_Date = reader["First_Order_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["First_Order_Date"]).Value;
                clsdbo_Agent.Status = reader["Status"] is DBNull ? null : (Boolean?)reader["Status"];
                clsdbo_Agent.Go_out_of_business_Date = reader["Go_out_of_business_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Go_out_of_business_Date"]).Value;
                clsdbo_Agent.Applied_Document = reader["Applied_Document"] is DBNull ? null : reader["Applied_Document"].ToString();
                clsdbo_Agent.Other_Document = reader["Other_Document"] is DBNull ? null : reader["Other_Document"].ToString();
                clsdbo_Agent.Small_Case = reader["Small_Case"] is DBNull ? null : (Int16?)reader["Small_Case"];
                clsdbo_Agent.Large_Case = reader["Large_Case"] is DBNull ? null : (Int16?)reader["Large_Case"];
                clsdbo_Agent.Pledge_Amount = reader["Pledge_Amount"] is DBNull ? null : (Decimal?)reader["Pledge_Amount"];
                clsdbo_Agent.Room_Size = reader["Room_Size"] is DBNull ? null : (Int16?)reader["Room_Size"];
                clsdbo_Agent.Cash_Deposit = reader["Cash_Deposit"] is DBNull ? null : (Decimal?)reader["Cash_Deposit"];
                clsdbo_Agent.Bank_Guarantee = reader["Bank_Guarantee"] is DBNull ? null : (Decimal?)reader["Bank_Guarantee"];
                clsdbo_Agent.Bank_ID = reader["Bank_ID"] is DBNull ? null : reader["Bank_ID"].ToString();
                clsdbo_Agent.Term_of_payment = reader["Term_of_payment"] is DBNull ? null : reader["Term_of_payment"].ToString();
                clsdbo_Agent.Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString();
                clsdbo_Agent.Product_Group_ID = reader["Product_Group_ID"] is DBNull ? null : reader["Product_Group_ID"].ToString();
                clsdbo_Agent.Price_Group_ID = reader["Price_Group_ID"] is DBNull ? null : reader["Price_Group_ID"].ToString();
                clsdbo_Agent.Grade = reader["Grade"] is DBNull ? null : reader["Grade"].ToString();
                clsdbo_Agent.Grade_Effective_Date = reader["Grade_Effective_Date"] is DBNull ? (DateTime?)null : ((DateTime?)reader["Grade_Effective_Date"]).Value;
                clsdbo_Agent.Location_Region = reader["Location_Region"] is DBNull ? null : reader["Location_Region"].ToString();
                clsdbo_Agent.Invoice_Region = reader["Invoice_Region"] is DBNull ? null : reader["Invoice_Region"].ToString();
                clsdbo_Agent.GM_ID = reader["GM_ID"] is DBNull ? null : reader["GM_ID"].ToString();
                clsdbo_Agent.APV_ID = reader["APV_ID"] is DBNull ? null : reader["APV_ID"].ToString();
            }
            else
            {
                clsdbo_Agent = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            logger.Error(ex.Message);
            return clsdbo_Agent;
        }
        finally
        {
            connection.Close();
        }
        return clsdbo_Agent;
    }

    public static bool Add(dbo_AgentClass clsdbo_Agent, String Created_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string insertProcedure = "[AgentInsert]";
        SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        if (clsdbo_Agent.CV_Code != null)
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", clsdbo_Agent.CV_Code);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@CV_Code", DBNull.Value);
        }
        if (clsdbo_Agent.Prefix_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Prefix_ID", clsdbo_Agent.Prefix_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Prefix_ID", DBNull.Value);
        }
        if (clsdbo_Agent.First_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@First_Name", clsdbo_Agent.First_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@First_Name", DBNull.Value);
        }
        if (clsdbo_Agent.Last_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Last_Name", clsdbo_Agent.Last_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Last_Name", string.Empty);
        }
        if (clsdbo_Agent.Agent_Type_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Agent_Type_ID", clsdbo_Agent.Agent_Type_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Agent_Type_ID", DBNull.Value);
        }
        if (clsdbo_Agent.Home_Phone_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Home_Phone_No", clsdbo_Agent.Home_Phone_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Home_Phone_No", DBNull.Value);
        }
        if (clsdbo_Agent.Mobile != null)
        {
            insertCommand.Parameters.AddWithValue("@Mobile", clsdbo_Agent.Mobile);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Mobile", DBNull.Value);
        }
        if (clsdbo_Agent.Tax_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Tax_ID", clsdbo_Agent.Tax_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Tax_ID", DBNull.Value);
        }
        if (clsdbo_Agent.Email != null)
        {
            insertCommand.Parameters.AddWithValue("@Email", clsdbo_Agent.Email);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Email", DBNull.Value);
        }
        if (clsdbo_Agent.Fax != null)
        {
            insertCommand.Parameters.AddWithValue("@Fax", clsdbo_Agent.Fax);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Fax", DBNull.Value);
        }
        if (clsdbo_Agent.Concession_Area != null)
        {
            insertCommand.Parameters.AddWithValue("@Concession_Area", clsdbo_Agent.Concession_Area);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Concession_Area", DBNull.Value);
        }
        if (clsdbo_Agent.Owner_First_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Owner_First_Name", clsdbo_Agent.Owner_First_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Owner_First_Name", DBNull.Value);
        }
        if (clsdbo_Agent.Owner_Last_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Owner_Last_Name", clsdbo_Agent.Owner_Last_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Owner_Last_Name", DBNull.Value);
        }
        if (clsdbo_Agent.Owner_Phone_No1 != null)
        {
            insertCommand.Parameters.AddWithValue("@Owner_Phone_No1", clsdbo_Agent.Owner_Phone_No1);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Owner_Phone_No1", DBNull.Value);
        }
        if (clsdbo_Agent.Owner_Phone_No2 != null)
        {
            insertCommand.Parameters.AddWithValue("@Owner_Phone_No2", clsdbo_Agent.Owner_Phone_No2);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Owner_Phone_No2", DBNull.Value);
        }
        if (clsdbo_Agent.Contact_First_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Contact_First_Name", clsdbo_Agent.Contact_First_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Contact_First_Name", DBNull.Value);
        }
        if (clsdbo_Agent.Contact_Last_Name != null)
        {
            insertCommand.Parameters.AddWithValue("@Contact_Last_Name", clsdbo_Agent.Contact_Last_Name);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Contact_Last_Name", DBNull.Value);
        }
        if (clsdbo_Agent.Contact_Phone_No1 != null)
        {
            insertCommand.Parameters.AddWithValue("@Contact_Phone_No1", clsdbo_Agent.Contact_Phone_No1);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Contact_Phone_No1", DBNull.Value);
        }
        if (clsdbo_Agent.Contact_Phone_No2 != null)
        {
            insertCommand.Parameters.AddWithValue("@Contact_Phone_No2", clsdbo_Agent.Contact_Phone_No2);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Contact_Phone_No2", DBNull.Value);
        }
        if (clsdbo_Agent.SD_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@SD_ID", clsdbo_Agent.SD_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SD_ID", DBNull.Value);
        }
        if (clsdbo_Agent.SM_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@SM_ID", clsdbo_Agent.SM_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@SM_ID", DBNull.Value);
        }
        if (clsdbo_Agent.DM_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@DM_ID", clsdbo_Agent.DM_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@DM_ID", DBNull.Value);
        }
        if (clsdbo_Agent.Location_House_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_House_No", clsdbo_Agent.Location_House_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_House_No", DBNull.Value);
        }
        if (clsdbo_Agent.Location_Village != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_Village", clsdbo_Agent.Location_Village);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_Village", DBNull.Value);
        }
        if (clsdbo_Agent.Location_Village_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_Village_No", clsdbo_Agent.Location_Village_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_Village_No", DBNull.Value);
        }
        if (clsdbo_Agent.Location_Alley != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_Alley", clsdbo_Agent.Location_Alley);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_Alley", DBNull.Value);
        }
        if (clsdbo_Agent.Location_Road != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_Road", clsdbo_Agent.Location_Road);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_Road", DBNull.Value);
        }
        if (clsdbo_Agent.Location_Sub_district != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_Sub_district", clsdbo_Agent.Location_Sub_district);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_Sub_district", DBNull.Value);
        }
        if (clsdbo_Agent.Location_District != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_District", clsdbo_Agent.Location_District);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_District", DBNull.Value);
        }
        if (clsdbo_Agent.Location_Province != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_Province", clsdbo_Agent.Location_Province);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_Province", DBNull.Value);
        }
        if (clsdbo_Agent.Location_Postal_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_Postal_ID", clsdbo_Agent.Location_Postal_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_Postal_ID", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_House_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_House_No", clsdbo_Agent.Invoice_House_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_House_No", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_Village != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Village", clsdbo_Agent.Invoice_Village);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Village", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_Village_No != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Village_No", clsdbo_Agent.Invoice_Village_No);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Village_No", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_Alley != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Alley", clsdbo_Agent.Invoice_Alley);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Alley", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_Road != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Road", clsdbo_Agent.Invoice_Road);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Road", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_Sub_district != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Sub_district", clsdbo_Agent.Invoice_Sub_district);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Sub_district", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_District != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_District", clsdbo_Agent.Invoice_District);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_District", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_Province != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Province", clsdbo_Agent.Invoice_Province);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Province", DBNull.Value);
        }
        if (clsdbo_Agent.Invoice_Postal_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Postal_ID", clsdbo_Agent.Invoice_Postal_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Postal_ID", DBNull.Value);
        }
        if (clsdbo_Agent.Start_Effective_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@Start_Effective_Date", clsdbo_Agent.Start_Effective_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Start_Effective_Date", DBNull.Value);
        }
        if (clsdbo_Agent.First_Order_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@First_Order_Date", clsdbo_Agent.First_Order_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@First_Order_Date", DBNull.Value);
        }
        if (clsdbo_Agent.Status.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Status", clsdbo_Agent.Status);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Status", DBNull.Value);
        }
        if (clsdbo_Agent.Go_out_of_business_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@Go_out_of_business_Date", clsdbo_Agent.Go_out_of_business_Date);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Go_out_of_business_Date", DBNull.Value);
        }
        if (clsdbo_Agent.Applied_Document != null)
        {
            insertCommand.Parameters.AddWithValue("@Applied_Document", clsdbo_Agent.Applied_Document);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Applied_Document", DBNull.Value);
        }
        if (clsdbo_Agent.Other_Document != null)
        {
            insertCommand.Parameters.AddWithValue("@Other_Document", clsdbo_Agent.Other_Document);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Other_Document", DBNull.Value);
        }
        if (clsdbo_Agent.Small_Case.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Small_Case", clsdbo_Agent.Small_Case);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Small_Case", DBNull.Value);
        }
        if (clsdbo_Agent.Large_Case.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Large_Case", clsdbo_Agent.Large_Case);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Large_Case", DBNull.Value);
        }
        if (clsdbo_Agent.Pledge_Amount.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Pledge_Amount", clsdbo_Agent.Pledge_Amount);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Pledge_Amount", DBNull.Value);
        }
        if (clsdbo_Agent.Room_Size.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Room_Size", clsdbo_Agent.Room_Size);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Room_Size", DBNull.Value);
        }
        if (clsdbo_Agent.Cash_Deposit.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Cash_Deposit", clsdbo_Agent.Cash_Deposit);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Cash_Deposit", DBNull.Value);
        }
        if (clsdbo_Agent.Bank_Guarantee.HasValue == true)
        {
            insertCommand.Parameters.AddWithValue("@Bank_Guarantee", clsdbo_Agent.Bank_Guarantee);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Bank_Guarantee", DBNull.Value);
        }
        if (clsdbo_Agent.Bank_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Bank_ID", clsdbo_Agent.Bank_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Bank_ID", DBNull.Value);
        }



        if (clsdbo_Agent.Term_of_payment != null)
        {
            insertCommand.Parameters.AddWithValue("@Term_of_payment", clsdbo_Agent.Term_of_payment);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Term_of_payment", DBNull.Value);
        }
        if (clsdbo_Agent.Remarks != null)
        {
            insertCommand.Parameters.AddWithValue("@Remarks", clsdbo_Agent.Remarks);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Remarks", DBNull.Value);
        }
        if (clsdbo_Agent.Product_Group_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Product_Group_ID", clsdbo_Agent.Product_Group_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Product_Group_ID", DBNull.Value);
        }
        if (clsdbo_Agent.Price_Group_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", clsdbo_Agent.Price_Group_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        }
        if (clsdbo_Agent.Grade != null)
        {
            insertCommand.Parameters.AddWithValue("@Grade", clsdbo_Agent.Grade);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Grade", DBNull.Value);
        }


        if (clsdbo_Agent.Grade_Effective_Date != null)
        {
            insertCommand.Parameters.AddWithValue("@Grade_Effective_Date", System.Convert.ToDateTime(clsdbo_Agent.Grade_Effective_Date));
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Grade_Effective_Date", DBNull.Value);
        }


        if (clsdbo_Agent.Location_Region != null)
        {
            insertCommand.Parameters.AddWithValue("@Location_Region", clsdbo_Agent.Location_Region);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Location_Region", DBNull.Value);
        }

        if (clsdbo_Agent.Invoice_Region != null)
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Region", clsdbo_Agent.Invoice_Region);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Invoice_Region", DBNull.Value);
        }

        if (Created_By != null)
        {
            insertCommand.Parameters.AddWithValue("@Created_By", Created_By);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@Created_By", DBNull.Value);
        }


        if (clsdbo_Agent.GM_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@GM_ID", clsdbo_Agent.GM_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@GM_ID", DBNull.Value);
        }

        if (clsdbo_Agent.APV_ID != null)
        {
            insertCommand.Parameters.AddWithValue("@APV_ID", clsdbo_Agent.APV_ID);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@APV_ID", DBNull.Value);
        }

        //if (clsdbo_Agent.invoi != null)
        //{
        //    insertCommand.Parameters.AddWithValue("@Price_Group_ID", clsdbo_Agent.Price_Group_ID);
        //}
        //else
        //{
        //    insertCommand.Parameters.AddWithValue("@Price_Group_ID", DBNull.Value);
        //}




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

    public static bool Update(dbo_AgentClass newdbo_AgentClass, string Last_Modified_By)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string updateProcedure = "AgentUpdate";
        SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
        updateCommand.CommandType = CommandType.StoredProcedure;


        if (newdbo_AgentClass.CV_Code != null)
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", newdbo_AgentClass.CV_Code);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCV_Code", DBNull.Value);
        }
        if (newdbo_AgentClass.Prefix_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPrefix_ID", newdbo_AgentClass.Prefix_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrefix_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.First_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewFirst_Name", newdbo_AgentClass.First_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewFirst_Name", DBNull.Value);
        }
        if (newdbo_AgentClass.Last_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Name", newdbo_AgentClass.Last_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLast_Name", string.Empty);
        }
        if (newdbo_AgentClass.Agent_Type_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewAgent_Type_ID", newdbo_AgentClass.Agent_Type_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewAgent_Type_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.Home_Phone_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Phone_No", newdbo_AgentClass.Home_Phone_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewHome_Phone_No", DBNull.Value);
        }
        if (newdbo_AgentClass.Mobile != null)
        {
            updateCommand.Parameters.AddWithValue("@NewMobile", newdbo_AgentClass.Mobile);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewMobile", DBNull.Value);
        }
        if (newdbo_AgentClass.Tax_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewTax_ID", newdbo_AgentClass.Tax_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTax_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.Email != null)
        {
            updateCommand.Parameters.AddWithValue("@NewEmail", newdbo_AgentClass.Email);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewEmail", DBNull.Value);
        }
        if (newdbo_AgentClass.Fax != null)
        {
            updateCommand.Parameters.AddWithValue("@NewFax", newdbo_AgentClass.Fax);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewFax", DBNull.Value);
        }
        if (newdbo_AgentClass.Concession_Area != null)
        {
            updateCommand.Parameters.AddWithValue("@NewConcession_Area", newdbo_AgentClass.Concession_Area);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewConcession_Area", DBNull.Value);
        }
        if (newdbo_AgentClass.Owner_First_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOwner_First_Name", newdbo_AgentClass.Owner_First_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOwner_First_Name", DBNull.Value);
        }
        if (newdbo_AgentClass.Owner_Last_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOwner_Last_Name", newdbo_AgentClass.Owner_Last_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOwner_Last_Name", DBNull.Value);
        }
        if (newdbo_AgentClass.Owner_Phone_No1 != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOwner_Phone_No1", newdbo_AgentClass.Owner_Phone_No1);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOwner_Phone_No1", DBNull.Value);
        }
        if (newdbo_AgentClass.Owner_Phone_No2 != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOwner_Phone_No2", newdbo_AgentClass.Owner_Phone_No2);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOwner_Phone_No2", DBNull.Value);
        }
        if (newdbo_AgentClass.Contact_First_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewContact_First_Name", newdbo_AgentClass.Contact_First_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewContact_First_Name", DBNull.Value);
        }
        if (newdbo_AgentClass.Contact_Last_Name != null)
        {
            updateCommand.Parameters.AddWithValue("@NewContact_Last_Name", newdbo_AgentClass.Contact_Last_Name);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewContact_Last_Name", DBNull.Value);
        }
        if (newdbo_AgentClass.Contact_Phone_No1 != null)
        {
            updateCommand.Parameters.AddWithValue("@NewContact_Phone_No1", newdbo_AgentClass.Contact_Phone_No1);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewContact_Phone_No1", DBNull.Value);
        }
        if (newdbo_AgentClass.Contact_Phone_No2 != null)
        {
            updateCommand.Parameters.AddWithValue("@NewContact_Phone_No2", newdbo_AgentClass.Contact_Phone_No2);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewContact_Phone_No2", DBNull.Value);
        }
        if (newdbo_AgentClass.SD_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSD_ID", newdbo_AgentClass.SD_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSD_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.SM_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewSM_ID", newdbo_AgentClass.SM_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSM_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.DM_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewDM_ID", newdbo_AgentClass.DM_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewDM_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_House_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_House_No", newdbo_AgentClass.Location_House_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_House_No", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_Village != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Village", newdbo_AgentClass.Location_Village);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Village", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_Village_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Village_No", newdbo_AgentClass.Location_Village_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Village_No", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_Alley != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Alley", newdbo_AgentClass.Location_Alley);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Alley", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_Road != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Road", newdbo_AgentClass.Location_Road);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Road", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_Sub_district != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Sub_district", newdbo_AgentClass.Location_Sub_district);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Sub_district", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_District != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_District", newdbo_AgentClass.Location_District);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_District", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_Province != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Province", newdbo_AgentClass.Location_Province);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Province", DBNull.Value);
        }
        if (newdbo_AgentClass.Location_Postal_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Postal_ID", newdbo_AgentClass.Location_Postal_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLocation_Postal_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_House_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_House_No", newdbo_AgentClass.Invoice_House_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_House_No", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_Village != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Village", newdbo_AgentClass.Invoice_Village);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Village", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_Village_No != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Village_No", newdbo_AgentClass.Invoice_Village_No);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Village_No", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_Alley != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Alley", newdbo_AgentClass.Invoice_Alley);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Alley", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_Road != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Road", newdbo_AgentClass.Invoice_Road);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Road", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_Sub_district != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Sub_district", newdbo_AgentClass.Invoice_Sub_district);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Sub_district", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_District != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_District", newdbo_AgentClass.Invoice_District);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_District", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_Province != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Province", newdbo_AgentClass.Invoice_Province);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Province", DBNull.Value);
        }
        if (newdbo_AgentClass.Invoice_Postal_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Postal_ID", newdbo_AgentClass.Invoice_Postal_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewInvoice_Postal_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.Start_Effective_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@NewStart_Effective_Date", newdbo_AgentClass.Start_Effective_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStart_Effective_Date", DBNull.Value);
        }
        if (newdbo_AgentClass.First_Order_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@NewFirst_Order_Date", newdbo_AgentClass.First_Order_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewFirst_Order_Date", DBNull.Value);
        }
        if (newdbo_AgentClass.Status.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewStatus", newdbo_AgentClass.Status);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewStatus", DBNull.Value);
        }
        if (newdbo_AgentClass.Go_out_of_business_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@NewGo_out_of_business_Date", newdbo_AgentClass.Go_out_of_business_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewGo_out_of_business_Date", DBNull.Value);
        }
        if (newdbo_AgentClass.Applied_Document != null)
        {
            updateCommand.Parameters.AddWithValue("@NewApplied_Document", newdbo_AgentClass.Applied_Document);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewApplied_Document", DBNull.Value);
        }
        if (newdbo_AgentClass.Other_Document != null)
        {
            updateCommand.Parameters.AddWithValue("@NewOther_Document", newdbo_AgentClass.Other_Document);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewOther_Document", DBNull.Value);
        }
        if (newdbo_AgentClass.Small_Case.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewSmall_Case", newdbo_AgentClass.Small_Case);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewSmall_Case", DBNull.Value);
        }
        if (newdbo_AgentClass.Large_Case.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewLarge_Case", newdbo_AgentClass.Large_Case);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewLarge_Case", DBNull.Value);
        }
        if (newdbo_AgentClass.Pledge_Amount.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewPledge_Amount", newdbo_AgentClass.Pledge_Amount);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPledge_Amount", DBNull.Value);
        }
        if (newdbo_AgentClass.Room_Size.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewRoom_Size", newdbo_AgentClass.Room_Size);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRoom_Size", DBNull.Value);
        }
        if (newdbo_AgentClass.Cash_Deposit.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewCash_Deposit", newdbo_AgentClass.Cash_Deposit);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewCash_Deposit", DBNull.Value);
        }
        if (newdbo_AgentClass.Bank_Guarantee.HasValue == true)
        {
            updateCommand.Parameters.AddWithValue("@NewBank_Guarantee", newdbo_AgentClass.Bank_Guarantee);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBank_Guarantee", DBNull.Value);
        }
        if (newdbo_AgentClass.Bank_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewBank_ID", newdbo_AgentClass.Bank_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewBank_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.Term_of_payment != null)
        {
            updateCommand.Parameters.AddWithValue("@NewTerm_of_payment", newdbo_AgentClass.Term_of_payment);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewTerm_of_payment", DBNull.Value);
        }
        if (newdbo_AgentClass.Remarks != null)
        {
            updateCommand.Parameters.AddWithValue("@NewRemarks", newdbo_AgentClass.Remarks);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewRemarks", DBNull.Value);
        }
        if (newdbo_AgentClass.Product_Group_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_Group_ID", newdbo_AgentClass.Product_Group_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewProduct_Group_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.Price_Group_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", newdbo_AgentClass.Price_Group_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewPrice_Group_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.Grade != null)
        {
            updateCommand.Parameters.AddWithValue("@NewGrade", newdbo_AgentClass.Grade);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewGrade", DBNull.Value);
        }


        if (newdbo_AgentClass.Grade_Effective_Date != null)
        {
            updateCommand.Parameters.AddWithValue("@NewGrade_Effective_Date", newdbo_AgentClass.Grade_Effective_Date);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@NewGrade_Effective_Date", DBNull.Value);
        }


        if (newdbo_AgentClass.Location_Region != null)
        {
            updateCommand.Parameters.AddWithValue("@Location_Region", newdbo_AgentClass.Location_Region);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Location_Region", DBNull.Value);
        }

        if (newdbo_AgentClass.Invoice_Region != null)
        {
            updateCommand.Parameters.AddWithValue("@Invoice_Region", newdbo_AgentClass.Invoice_Region);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Invoice_Region", DBNull.Value);
        }

        if (Last_Modified_By != null)
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", Last_Modified_By);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@Last_Modified_By", DBNull.Value);
        }

        if (newdbo_AgentClass.GM_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@GM_ID", newdbo_AgentClass.GM_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@GM_ID", DBNull.Value);
        }
        if (newdbo_AgentClass.APV_ID != null)
        {
            updateCommand.Parameters.AddWithValue("@APV_ID", newdbo_AgentClass.APV_ID);
        }
        else
        {
            updateCommand.Parameters.AddWithValue("@APV_ID", DBNull.Value);
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
        catch (Exception exx)
        {
            string err = exx.Message;
            connection.Close();
            return false;
        }
        //finally
        //{
        //    connection.Close();
        //}
    }








    public static bool Delete(string CV_Code)
    {
        SqlConnection connection = SAMDataClass.GetConnection();
        string deleteProcedure = "[AgentDelete]";
        SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
        deleteCommand.CommandType = CommandType.StoredProcedure;
        if (!string.IsNullOrEmpty(CV_Code))
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", CV_Code);
        }
        else
        {
            deleteCommand.Parameters.AddWithValue("@OldCV_Code", DBNull.Value);
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

