using System;
public class dbo_DebtClass
{
    private String m_Debt_ID;
    private String m_CV_Code;
    private String m_SP_ID;
    private String m_Customer_ID;
    private Nullable<DateTime> m_Debt_Date;
    private Nullable<Decimal> m_Debt_Amount;
    private Nullable<Decimal> m_Total_Payment_Amount;
    private Nullable<Decimal> m_Balance_Outstanding_Amount;
    private Nullable<DateTime> m_Created_Date;
    private String m_Requisition_No;

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

    public dbo_DebtClass() { }

    public String Debt_ID
    {
        get
        {
            return m_Debt_ID;
        }
        set
        {
            m_Debt_ID = value;
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

    public Nullable<DateTime> Debt_Date
    {
        get
        {
            return m_Debt_Date;
        }
        set
        {
            m_Debt_Date = value;
        }
    }

    public Nullable<Decimal> Debt_Amount
    {
        get
        {
            return m_Debt_Amount;
        }
        set
        {
            m_Debt_Amount = value;
        }
    }

    public Nullable<Decimal> Total_Payment_Amount
    {
        get
        {
            return m_Total_Payment_Amount == null ? 0 : m_Total_Payment_Amount;
        }
        set
        {
            m_Total_Payment_Amount = value;
        }
    }

    public Nullable<Decimal> Balance_Outstanding_Amount
    {
        get
        {
            return m_Balance_Outstanding_Amount;
        }
        set
        {
            m_Balance_Outstanding_Amount = value;
        }
    }

}


