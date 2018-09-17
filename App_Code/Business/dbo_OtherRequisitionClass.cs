using System;
public class dbo_OtherRequisitionClass
{
    private String m_Requisition_No;
    private String m_CV_Code;
    private Nullable<DateTime> m_Requisition_Date;
    private String m_Requisition_Name;
    private String m_Other_Requisition_Name;
    private String m_Reason;
    private String m_Other_reason;
    private Nullable<Int32> m_Grand_Total_Qty;
    private Nullable<Decimal> m_Grand_Total_Amount;
    private String m_Requisition_FullName;

    public dbo_OtherRequisitionClass() { }

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

    public String CV_Code
    {
        get
        {
            return m_CV_Code;
        }
        set
        {
            m_CV_Code = value;
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

    public String Requisition_Name
    {
        get
        {
            return m_Requisition_Name;
        }
        set
        {
            m_Requisition_Name = value;
        }
    }

    public String Other_Requisition_Name
    {
        get
        {
            return m_Other_Requisition_Name;
        }
        set
        {
            m_Other_Requisition_Name = value;
        }
    }

    public String Reason
    {
        get
        {
            return m_Reason;
        }
        set
        {
            m_Reason = value;
        }
    }

    public String Other_reason
    {
        get
        {
            return m_Other_reason;
        }
        set
        {
            m_Other_reason = value;
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
    public String Requisition_FullName
    {
        get
        {
            return m_Requisition_FullName;
        }
        set
        {
            m_Requisition_FullName = value;
        }
    }
}


