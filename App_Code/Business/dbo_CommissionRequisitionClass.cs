using System;
public class dbo_CommissionRequisitionClass
{
    private String m_Commission_requisition_no;
    private Nullable<DateTime> m_Commission_Requisition_Date;
    private String m_Last_Modified_By;
    private Nullable<Decimal> m_Requisition_Amount;
    private Nullable<Decimal> m_Commission_Balance_Outstanding;
    private Nullable<Decimal> m_Total_Balance_Outstanding;
    private Nullable<Decimal> m_Total_Credit_Amount;
    private Nullable<DateTime> m_Created_Date;
    private Nullable<DateTime> m_Last_Modified_Date;
    private String m_Created_By;

    public dbo_CommissionRequisitionClass() { }

    public String Commission_requisition_no
    {
        get
        {
            return m_Commission_requisition_no;
        }
        set
        {
            m_Commission_requisition_no = value;
        }
    }

    public Nullable<DateTime> Commission_Requisition_Date
    {
        get
        {
            return m_Commission_Requisition_Date;
        }
        set
        {
            m_Commission_Requisition_Date = value;
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

    public Nullable<Decimal> Requisition_Amount
    {
        get
        {
            return m_Requisition_Amount;
        }
        set
        {
            m_Requisition_Amount = value;
        }
    }

    public Nullable<Decimal> Commission_Balance_Outstanding
    {
        get
        {
            return m_Commission_Balance_Outstanding;
        }
        set
        {
            m_Commission_Balance_Outstanding = value;
        }
    }

    public Nullable<Decimal> Total_Balance_Outstanding
    {
        get
        {
            return m_Total_Balance_Outstanding;
        }
        set
        {
            m_Total_Balance_Outstanding = value;
        }
    }

    public Nullable<Decimal> Total_Credit_Amount
    {
        get
        {
            return m_Total_Credit_Amount;
        }
        set
        {
            m_Total_Credit_Amount = value;
        }
    }

    public Nullable<DateTime> Created_Date
    {
        get
        {
            return m_Created_Date;
        }
        set
        {
            m_Created_Date = value;
        }
    }

    public Nullable<DateTime> Last_Modified_Date
    {
        get
        {
            return m_Last_Modified_Date;
        }
        set
        {
            m_Last_Modified_Date = value;
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

}


