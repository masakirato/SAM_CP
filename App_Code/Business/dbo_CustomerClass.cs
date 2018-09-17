using System;
public class dbo_CustomerClass
{
    private String m_Customer_ID;
    private String m_Customer_Type;
    private String m_Status;
    private String m_Residence_Type_ID;
    private String m_First_Name;
    private String m_Last_Name;
    private String m_Full_Name;
    private String m_Home_Phone_No;
    private String m_Mobile;
    private String m_Contact_Name;
    private Nullable<DateTime> m_Birthday;
    private String m_Home_House_No;
    private String m_Home_Tower;
    private String m_Home_Village;
    private String m_Home_Village_No;
    private String m_Home_Alley;
    private String m_Home_Road;
    private String m_Home_Sub_district;
    private String m_Home_District;
    private String m_Home_Province;
    private String m_Home_Postal_ID;
    private String m_Shipment_House_No;
    private String m_Shipment_Tower;
    private String m_Shipment_Village;
    private String m_Shipment_Village_No;
    private String m_Shipment_Alley;
    private String m_Shipment_Road;
    private String m_Shipment_Sub_district;
    private String m_Shipment_District;
    private String m_Shipment_Province;
    private String m_Shipment_Postal_ID;
    private String m_Remarks;
    private String m_SP_ID;
    private String m_SP_Name;
    private String m_Payment_Type;
    private String m_Billing_Type;
    private String m_Billing_Day_of_Week;
    private String m_Due_Billing_Day_of_Week;
    private String m_Billing_Day_of_Month;
    private String m_Due_Billing_Day_of_Month;
    private String m_Billing_Day_of_Other;
    private String m_Due_Billing_Day_of_Other;
    private String m_Credit_Term;
    private Nullable<Int32> m_Credit_Limit;
    private String m_CV_Code;
    private String m_Replace_Sales;
    private String m_Price_Group_ID;
    private String m_ReceiveDate_Hour;
    private String m_ReceiveDate_Minute;
    private String m_ReceiveToDate_Hour;
    private String m_ReceiveToDate_Minute;
    private String m_Active_Status;
    private String m_Shop_Name;
    private Nullable<DateTime> m_Member_Date;

    public dbo_CustomerClass() { }

    public String Customer_ID
    {
        get
        {
            return m_Customer_ID;
        }
        set
        {
            m_Customer_ID = value;
        }
    }

    public String Customer_Type
    {
        get
        {
            return m_Customer_Type;
        }
        set
        {
            m_Customer_Type = value;
        }
    }

    public String Status
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

    public String Residence_Type_ID
    {
        get
        {
            return m_Residence_Type_ID;
        }
        set
        {
            m_Residence_Type_ID = value;
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


    public String Full_Name
    {
        get
        {
            if (string.IsNullOrEmpty(m_Full_Name))
                return m_First_Name + ' ' + m_Last_Name;
            else
                return m_Full_Name;
        }
        set
        {
            m_Full_Name = value;
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

    public String Contact_Name
    {
        get
        {
            return m_Contact_Name;
        }
        set
        {
            m_Contact_Name = value;
        }
    }

    public Nullable<DateTime> Birthday
    {
        get
        {
            return m_Birthday;
        }
        set
        {
            m_Birthday = value;
        }
    }

    public String Home_House_No
    {
        get
        {
            return m_Home_House_No;
        }
        set
        {
            m_Home_House_No = value;
        }
    }

    public String Home_Tower
    {
        get
        {
            return m_Home_Tower;
        }
        set
        {
            m_Home_Tower = value;
        }
    }

    public String Home_Village
    {
        get
        {
            return m_Home_Village;
        }
        set
        {
            m_Home_Village = value;
        }
    }

    public String Home_Village_No
    {
        get
        {
            return m_Home_Village_No;
        }
        set
        {
            m_Home_Village_No = value;
        }
    }

    public String Home_Alley
    {
        get
        {
            return m_Home_Alley;
        }
        set
        {
            m_Home_Alley = value;
        }
    }

    public String Home_Road
    {
        get
        {
            return m_Home_Road;
        }
        set
        {
            m_Home_Road = value;
        }
    }

    public String Home_Sub_district
    {
        get
        {
            return m_Home_Sub_district;
        }
        set
        {
            m_Home_Sub_district = value;
        }
    }

    public String Home_District
    {
        get
        {
            return m_Home_District;
        }
        set
        {
            m_Home_District = value;
        }
    }

    public String Home_Province
    {
        get
        {
            return m_Home_Province;
        }
        set
        {
            m_Home_Province = value;
        }
    }

    public String Home_Postal_ID
    {
        get
        {
            return m_Home_Postal_ID;
        }
        set
        {
            m_Home_Postal_ID = value;
        }
    }

    public String Shipment_House_No
    {
        get
        {
            return m_Shipment_House_No;
        }
        set
        {
            m_Shipment_House_No = value;
        }
    }

    public String Shipment_Tower
    {
        get
        {
            return m_Shipment_Tower;
        }
        set
        {
            m_Shipment_Tower = value;
        }
    }

    public String Shipment_Village
    {
        get
        {
            return m_Shipment_Village;
        }
        set
        {
            m_Shipment_Village = value;
        }
    }

    public String Shipment_Village_No
    {
        get
        {
            return m_Shipment_Village_No;
        }
        set
        {
            m_Shipment_Village_No = value;
        }
    }

    public String Shipment_Alley
    {
        get
        {
            return m_Shipment_Alley;
        }
        set
        {
            m_Shipment_Alley = value;
        }
    }

    public String Shipment_Road
    {
        get
        {
            return m_Shipment_Road;
        }
        set
        {
            m_Shipment_Road = value;
        }
    }

    public String Shipment_Sub_district
    {
        get
        {
            return m_Shipment_Sub_district;
        }
        set
        {
            m_Shipment_Sub_district = value;
        }
    }

    public String Shipment_District
    {
        get
        {
            return m_Shipment_District;
        }
        set
        {
            m_Shipment_District = value;
        }
    }

    public String Shipment_Province
    {
        get
        {
            return m_Shipment_Province;
        }
        set
        {
            m_Shipment_Province = value;
        }
    }

    public String Shipment_Postal_ID
    {
        get
        {
            return m_Shipment_Postal_ID;
        }
        set
        {
            m_Shipment_Postal_ID = value;
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

    public String SP_ID
    {
        get
        {
            return m_SP_ID;
        }
        set
        {
            m_SP_ID = value;
        }
    }

    public String SP_Name
    {
        get
        {
            return m_SP_Name;
        }
        set
        {
            m_SP_Name = value;
        }
    }

    public String Payment_Type
    {
        get
        {
            return m_Payment_Type;
        }
        set
        {
            m_Payment_Type = value;
        }
    }

    public String Billing_Type
    {
        get
        {
            return m_Billing_Type;
        }
        set
        {
            m_Billing_Type = value;
        }
    }

    public String Billing_Day_of_Week
    {
        get
        {
            return m_Billing_Day_of_Week;
        }
        set
        {
            m_Billing_Day_of_Week = value;
        }
    }

    public String Due_Billing_Day_of_Week
    {
        get
        {
            return m_Due_Billing_Day_of_Week;
        }
        set
        {
            m_Due_Billing_Day_of_Week = value;
        }
    }

    public String Billing_Day_of_Month
    {
        get
        {
            return m_Billing_Day_of_Month;
        }
        set
        {
            m_Billing_Day_of_Month = value;
        }
    }

    public String Due_Billing_Day_of_Month
    {
        get
        {
            return m_Due_Billing_Day_of_Month;
        }
        set
        {
            m_Due_Billing_Day_of_Month = value;
        }
    }

    public String Billing_Day_of_Other
    {
        get
        {
            return m_Billing_Day_of_Other;
        }
        set
        {
            m_Billing_Day_of_Other = value;
        }
    }

    public String Due_Billing_Day_of_Other
    {
        get
        {
            return m_Due_Billing_Day_of_Other;
        }
        set
        {
            m_Due_Billing_Day_of_Other = value;
        }
    }

    public String Credit_Term
    {
        get
        {
            return m_Credit_Term;
        }
        set
        {
            m_Credit_Term = value;
        }
    }

    public Nullable<Int32> Credit_Limit
    {
        get
        {
            return m_Credit_Limit;
        }
        set
        {
            m_Credit_Limit = value;
        }
    }

    public String CV_Code
    {
        get
        {
            return m_CV_Code == null ? string.Empty : m_CV_Code;
           // return m_CV_Code;
        }
        set
        {
            m_CV_Code = value;
        }
    }

    public String Replace_Sales
    {
        get
        {
            return m_Replace_Sales;
        }
        set
        {
            m_Replace_Sales = value;
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

    public String ReceiveDate_Hour
    {
        get
        {
            return m_ReceiveDate_Hour;
        }
        set
        {
            m_ReceiveDate_Hour = value;
        }
    }

    public String ReceiveDate_Minute
    {
        get
        {
            return m_ReceiveDate_Minute;
        }
        set
        {
            m_ReceiveDate_Minute = value;
        }
    }

    public String ReceiveToDate_Hour
    {
        get
        {
            return m_ReceiveToDate_Hour;
        }
        set
        {
            m_ReceiveToDate_Hour = value;
        }
    }

    public String ReceiveToDate_Minute
    {
        get
        {
            return m_ReceiveToDate_Minute;
        }
        set
        {
            m_ReceiveToDate_Minute = value;
        }
    }

    public String Active_status
    {
        get
        {
            return m_Active_Status;
        }
        set
        {
            m_Active_Status = value;
        }
    }
    public Nullable<DateTime> Member_Date
    {
        get
        {
            return m_Member_Date;
        }
        set
        {
            m_Member_Date = value;
        }
    }
    public String Shop_Name
    {
        get
        {
            return m_Shop_Name;
        }
        set
        {
            m_Shop_Name = value;
        }
    }
}


