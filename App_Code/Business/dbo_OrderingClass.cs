using System;
public class dbo_OrderingClass
{
    private String m_PO_Number;
    private String m_CV_Code_from_SAP;
    private Nullable<Decimal> m_Total_Amount_before_vat_included;
    private Nullable<Decimal> m_Vat_amount;
    private Nullable<Decimal> m_Total_amount_after_vat_included;
    private Nullable<DateTime> m_Date_of_create_order_or_PO_Date;
    private Nullable<DateTime> m_Date_of_CP_receive_transaction;
    private Nullable<DateTime> m_Date_of_delivery_goods;
    private String m_Order_Status;
    private String m_Created_By;
    private String m_OrderBy;

    private String m_User_ID;
    private String m_Cycle_Date;
    public dbo_OrderingClass() { }
    private String m_AgentName;
    private String m_Home_Phone_No;
    private Nullable<DateTime> m_date_id;

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
    public String Cycle_Date
    {
        get
        {
            return m_Cycle_Date;
        }
        set
        {
            m_Cycle_Date = value;
        }
    }

    public String AgentName
    {
        get
        {
            return m_AgentName;
            //(string.IsNullOrEmpty(m_Agent.Fax) ? string.Empty : m_Agent.Fax);
           // return   string.IsNullOrEmpty(dbo_AgentDataClass.Select_Record(this.m_CV_Code_from_SAP).AgentName) ? string.Empty : dbo_AgentDataClass.Select_Record(this.m_CV_Code_from_SAP).AgentName;
        }
        set
        {
            m_AgentName = value;
        }
    }

    /*public String Home_Phone_No
    {
        get
        {
            return dbo_AgentDataClass.Select_Record(this.m_CV_Code_from_SAP).Home_Phone_No;
        }
    }*/


    public String OrderBy
    {
        get
        {
            m_OrderBy = dbo_UserDataClass.Select_Record(Created_By).FullName;
            return m_OrderBy;
        }
        set
        {
            m_OrderBy = value;
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

    public String PO_Number
    {
        get
        {
            return m_PO_Number;
        }
        set
        {
            m_PO_Number = value;
        }
    }

    public String CV_Code_from_SAP
    {
        get
        {
            return m_CV_Code_from_SAP;
        }
        set
        {
            m_CV_Code_from_SAP = value;
        }
    }

    public Nullable<Decimal> Total_Amount_before_vat_included
    {
        get
        {
            return m_Total_Amount_before_vat_included;
        }
        set
        {
            m_Total_Amount_before_vat_included = value;
        }
    }

    public Nullable<Decimal> Vat_amount
    {
        get
        {
            return m_Vat_amount;
        }
        set
        {
            m_Vat_amount = value;
        }
    }

    public Nullable<Decimal> Total_amount_after_vat_included
    {
        get
        {
            return m_Total_amount_after_vat_included;
        }
        set
        {
            m_Total_amount_after_vat_included = value;
        }
    }

    public Nullable<DateTime> Date_of_create_order_or_PO_Date
    {
        get
        {
            return m_Date_of_create_order_or_PO_Date;
        }
        set
        {
            m_Date_of_create_order_or_PO_Date = value;
        }
    }

    public Nullable<DateTime> Date_of_CP_receive_transaction
    {
        get
        {
            return m_Date_of_CP_receive_transaction;
        }
        set
        {
            m_Date_of_CP_receive_transaction = value;
        }
    }

    public Nullable<DateTime> Date_of_delivery_goods
    {
        get
        {
            return m_Date_of_delivery_goods;
        }
        set
        {
            m_Date_of_delivery_goods = value;
        }
    }

    public String Order_Status
    {
        get
        {
            return m_Order_Status;
        }
        set
        {
            m_Order_Status = value;
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
    public Nullable<DateTime> date_id
    {
        get
        {
            return m_date_id;
        }
        set
        {
            m_date_id = value;
        }
    }
}


