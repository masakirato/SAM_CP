using System;
public class dbo_AgentClass
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
    private String m_Location_ID;
    
    private String m_Invoice_Region;

    private String m_AgentName;

    private String m_CV_AgentName;

    private string m_Adress_Agent;

    public dbo_AgentClass() { }


    public string Adress_Agent
    {
        get
        {
            m_Adress_Agent = m_Location_House_No + " " +
            m_Location_Village + " " +
            m_Location_Village_No + " " +
            m_Location_Alley + " " +
            m_Location_Road + " " +
            m_Location_Sub_district + " " +
            m_Location_District + " " +
            m_Location_Province + " " +
            m_Location_Postal_ID;
            return m_Adress_Agent;
        }
        set
        {
            m_Adress_Agent = value;
        }
    }
    public String SD_ID_FullName
    {
        get
        {
            if (string.IsNullOrEmpty(SD_ID)) return string.Empty;

            try
            {
                m_SD_ID_FullName = dbo_UserDataClass.Select_Record(SD_ID).FullName;
                return m_SD_ID_FullName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

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
            if (string.IsNullOrEmpty(SM_ID)) return string.Empty;
            try
            {
                m_SM_ID_FullName = dbo_UserDataClass.Select_Record(SM_ID).FullName;
                return m_SM_ID_FullName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
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
            if (string.IsNullOrEmpty(DM_ID)) return string.Empty;
            try
            {


                m_DM_ID_FullName = dbo_UserDataClass.Select_Record(DM_ID).FullName;
                return m_DM_ID_FullName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
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
            if (string.IsNullOrEmpty(APV_ID)) return string.Empty;
            try
            {


                m_APV_ID_FullName = dbo_UserDataClass.Select_Record(APV_ID).FullName;
                return m_APV_ID_FullName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
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
            if (string.IsNullOrEmpty(GM_ID)) return string.Empty;
            try
            {


                m_GM_ID_FullName = dbo_UserDataClass.Select_Record(GM_ID).FullName;
                return m_GM_ID_FullName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        set
        {
            m_GM_ID_FullName = value;
        }
    }


    public String CV_AgentName
    {
        get
        {
            if (string.IsNullOrEmpty(m_CV_AgentName))
            {
                m_CV_AgentName = m_CV_Code + " " + m_Prefix_ID + " " + m_First_Name + " " + m_Last_Name;
            }

            return m_CV_AgentName;
        }
        set
        {
            m_CV_AgentName = value;
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

    public String Location_ID
    {
        get
        {
            return m_Location_ID;
        }
        set
        {
            m_Location_ID = value;
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


