using System;
public class dbo_RequisitionClass
{
    private String m_Requisition_No;
    private String m_Time_No;
    private String m_User_ID;
    private Nullable<DateTime> m_Requisition_Date;
    private Nullable<DateTime> m_Transaction_Date;
    private Nullable<Int32> m_Grand_Total_Qty;
    private Nullable<Decimal> m_Grand_Total_Amount;
    private Nullable<Decimal> m_Total_Commission;
    private Nullable<Int16> m_Tota_Point;
    private String m_Status;
    private String m_Replace_Sales;
    private String m_SP_Name;

    private Nullable<Int16> m_Stock_END;


    public Nullable<Int16> Stock_END
    {
        get
        {
            return m_Stock_END;
        }
        set
        {
            m_Stock_END = value;
        }
    }



    public dbo_RequisitionClass() { }

    public String Requisition_No
    {
        get
        {
            return m_Requisition_No;
        }
        set
        {
            m_Requisition_No = value;
        }
    }

    public String SP_Name
    {
        get
        {
            m_SP_Name = dbo_UserDataClass.Select_Record(User_ID).FullName;
            return m_SP_Name;
        }
        set
        {
            m_SP_Name = value;
        }
    }

    public String Time_No
    {
        get
        {
            return m_Time_No;
        }
        set
        {
            m_Time_No = value;
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

    public Nullable<DateTime> Requisition_Date
    {
        get
        {
            return m_Requisition_Date;
        }
        set
        {
            m_Requisition_Date = value;
        }
    }

    public Nullable<DateTime> Transaction_Date
    {
        get
        {
            return m_Transaction_Date;
        }
        set
        {
            m_Transaction_Date = value;
        }
    }

    public Nullable<Int32> Grand_Total_Qty
    {
        get
        {
            return m_Grand_Total_Qty;
        }
        set
        {
            m_Grand_Total_Qty = value;
        }
    }

    public Nullable<Decimal> Grand_Total_Amount
    {
        get
        {
            return m_Grand_Total_Amount;
        }
        set
        {
            m_Grand_Total_Amount = value;
        }
    }

    public Nullable<Decimal> Total_Commission
    {
        get
        {
            return m_Total_Commission;
        }
        set
        {
            m_Total_Commission = value;
        }
    }

    public Nullable<Int16> Tota_Point
    {
        get
        {
            return m_Tota_Point;
        }
        set
        {
            m_Tota_Point = value;
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
}


