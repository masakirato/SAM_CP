using System;
public class dbo_CommissionClass
{
    private String m_Requisition_No;
    private Nullable<Decimal> m_Commission;
    private Nullable<Int16> m_Point;
    private Nullable<Byte> m_Commission_Requisition_Status;
    private Nullable<DateTime> m_Created_Date;
    private Nullable<Decimal> m_Commission_Balance_Outstanding;


    public dbo_CommissionClass() { }

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

    public Nullable<Decimal> Commission
    {
        get
        {
            return m_Commission;
        }
        set
        {
            m_Commission = value;
        }
    }

    public Nullable<Int16> Point
    {
        get
        {
            return m_Point;
        }
        set
        {
            m_Point = value;
        }
    }

    public Nullable<Byte> Commission_Requisition_Status
    {
        get
        {
            return m_Commission_Requisition_Status;
        }
        set
        {
            m_Commission_Requisition_Status = value;
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
}


