using System;
using System.Security.Cryptography;
using System.Text;
public class dbo_UserClass
{
    private String m_User_ID;


    private String m_First_Name;
    private String m_Last_Name;

    private String m_First_Name_Eng;
    private String m_Last_Name_Eng;


    private String m_Home_Phone_No;
    private String m_Mobile;
    private String m_Position;
    private String m_Section;
    private String m_Division;
    private String m_Manager;
    private String m_User_Group_ID;
    private String m_Role_ID;
    private String m_Email;
    private String m_Username;



    private String m_Password;



    private String m_Approval_Status_ID;
    private String m_Status;
    private String m_CV_CODE;
    private Nullable<DateTime> m_Birthdate;
    private String m_ID_Card_No;
    private String m_Home_House_No;
    private String m_Home_Village;
    private String m_Home_Village_No;
    private String m_Home_Alley;
    private String m_Home_Road;
    private String m_Home_Sub_district;
    private String m_Home_District;
    private String m_Home_Province;
    private String m_Home_Postal_ID;
    private Nullable<DateTime> m_Join_Date;
    private Nullable<DateTime> m_Resign_Date;
    private String m_Price_Group_ID;
    private String m_Payment_Type;
    private Nullable<Byte> m_Credit_Term;
    private Nullable<Byte> m_Credit_Limit;
    private String m_Applied_Document;
    private String m_Created_By;
    private String m_Last_Modified_By;
    private String m_Title_ID;
    private String m_Present_House_No;
    private String m_Present_Village;
    private String m_Present_Village_No;
    private String m_Present_Alley;
    private String m_Present_Road;
    private String m_Present_Sub_District;
    private String m_Present_District;
    private String m_Present_Province;
    private String m_Present_Postal_ID;
    private String m_ShowDashboard;

    private String m_Route;




    private String m_AgentName;

    private String m_FullName;

    private String m_FullName_ddl;




    private String m_Region;

    private String m_Item_Value_ID;


    public dbo_UserClass() { }


    public String Item_Value_ID
    {
        get
        {
            return m_Item_Value_ID;
        }
        set
        {
            m_Item_Value_ID = value;
        }
    }


    public String User_ID
    {
        get
        {
            return m_User_ID;
        }
        set
        {
            m_User_ID = value;
        }
    }


    public String Region
    {
        get
        {
            return m_Region;
        }
        set
        {
            m_Region = value;
        }
    }

    public String AgentName
    {
        get
        {
            return m_AgentName;
        }
        set
        {
            m_AgentName = value;
        }
    }


    public String Route
    {
        get
        {
            return m_Route;
        }
        set
        {
            m_Route = value;
        }
    }


    public String ShowDashboard
    {
        get
        {
            return m_ShowDashboard;
        }
        set
        {
            m_ShowDashboard = value;
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


    public String FullName
    {
        get
        {
            if (string.IsNullOrEmpty(m_FullName))
            {
                return m_Title_ID + " " + m_First_Name + " " + m_Last_Name;
            }
            else { return m_FullName; }


        }
        set
        {
            m_FullName = value;
        }
    }


    public String FullName_ddl
    {
        get
        {
            if (string.IsNullOrEmpty(m_FullName_ddl))
            {
                return m_User_ID + " " + m_Title_ID + " " + m_First_Name + " " + m_Last_Name;
            }
            else { return m_FullName_ddl; }


        }
        set
        {
            m_FullName_ddl = value;
        }
    }

    public String First_Name_Eng
    {
        get
        {
            return m_First_Name_Eng;
        }
        set
        {
            m_First_Name_Eng = value;
        }
    }

    public String Last_Name_Eng
    {
        get
        {
            return m_Last_Name_Eng;
        }
        set
        {
            m_Last_Name_Eng = value;
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

    public String Position
    {
        get
        {
            return m_Position;
        }
        set
        {
            m_Position = value;
        }
    }

    public String Section
    {
        get
        {
            return m_Section;
        }
        set
        {
            m_Section = value;
        }
    }

    public String Division
    {
        get
        {
            return m_Division;
        }
        set
        {
            m_Division = value;
        }
    }

    public String Manager
    {
        get
        {
            return m_Manager;
        }
        set
        {
            m_Manager = value;
        }
    }

    public String User_Group_ID
    {
        get
        {
            return m_User_Group_ID;
        }
        set
        {
            m_User_Group_ID = value;
        }
    }

    public String Role_ID
    {
        get
        {
            return m_Role_ID;
        }
        set
        {
            m_Role_ID = value;
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

    public String Username
    {
        get
        {
            return m_Username;
        }
        set
        {
            m_Username = value;
        }
    }

    public String Password
    {
        get
        {
            return m_Password;
        }
        set
        {

            m_Password = value;
            //if (value != null)
            //{
            //    Paddedwall.CryptoLib.Crypto cr = new Paddedwall.CryptoLib.Crypto();
            //    m_Password = cr.Encrypt(value, Paddedwall.CryptoLib.Crypto.CryptoTypes.encTypeRijndael);
            //}

        }
    }

    public String Approval_Status_ID
    {
        get
        {
            return m_Approval_Status_ID;
        }
        set
        {
            m_Approval_Status_ID = value;
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

    public String CV_CODE
    {
        get
        {

            return m_CV_CODE == null ? string.Empty : m_CV_CODE;
        }
        set
        {
            m_CV_CODE = value;
        }
    }

    public Nullable<DateTime> Birthdate
    {
        get
        {
            return m_Birthdate;
        }
        set
        {
            m_Birthdate = value;
        }
    }

    public String ID_Card_No
    {
        get
        {
            return m_ID_Card_No;
        }
        set
        {
            m_ID_Card_No = value;
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

    public Nullable<DateTime> Join_Date
    {
        get
        {
            return m_Join_Date;
        }
        set
        {
            m_Join_Date = value;
        }
    }

    public Nullable<DateTime> Resign_Date
    {
        get
        {
            return m_Resign_Date;
        }
        set
        {
            m_Resign_Date = value;
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

    public Nullable<Byte> Credit_Term
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

    public Nullable<Byte> Credit_Limit
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

    public String Created_By
    {
        get
        {
            return m_Created_By;
        }
        set
        {
            m_Created_By = value;
        }
    }

    public String Last_Modified_By
    {
        get
        {
            return m_Last_Modified_By;
        }
        set
        {
            m_Last_Modified_By = value;
        }
    }

    public String Title_ID
    {
        get
        {
            return m_Title_ID;
        }
        set
        {
            m_Title_ID = value;
        }
    }

    public String Present_House_No
    {
        get
        {
            return m_Present_House_No;
        }
        set
        {
            m_Present_House_No = value;
        }
    }

    public String Present_Village
    {
        get
        {
            return m_Present_Village;
        }
        set
        {
            m_Present_Village = value;
        }
    }

    public String Present_Village_No
    {
        get
        {
            return m_Present_Village_No;
        }
        set
        {
            m_Present_Village_No = value;
        }
    }

    public String Present_Alley
    {
        get
        {
            return m_Present_Alley;
        }
        set
        {
            m_Present_Alley = value;
        }
    }

    public String Present_Road
    {
        get
        {
            return m_Present_Road;
        }
        set
        {
            m_Present_Road = value;
        }
    }

    public String Present_Sub_District
    {
        get
        {
            return m_Present_Sub_District;
        }
        set
        {
            m_Present_Sub_District = value;
        }
    }

    public String Present_District
    {
        get
        {
            return m_Present_District;
        }
        set
        {
            m_Present_District = value;
        }
    }

    public String Present_Province
    {
        get
        {
            return m_Present_Province;
        }
        set
        {
            m_Present_Province = value;
        }
    }

    public String Present_Postal_ID
    {
        get
        {
            return m_Present_Postal_ID;
        }
        set
        {
            m_Present_Postal_ID = value;
        }
    }

}


