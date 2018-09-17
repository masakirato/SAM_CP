using System;
public class dbo_FIFOPaymentClass
{
    private String m_FIFO_ID;
    private String m_CV_CODE;
    private String m_SP_ID;
    private Nullable<DateTime> m_Debt_Date;
    private String m_Requisition_No;
    private Nullable<Decimal> m_Payment_Amount;
    private Nullable<DateTime> m_Created_Date;
    private String m_Created_By;
    private Nullable<DateTime> m_Last_Modified_Date;
    private String m_Last_Modified_By;

    public dbo_FIFOPaymentClass() { }

    public String FIFO_ID
    {
        get
        {
            return m_FIFO_ID;
        }
        set
        {
            m_FIFO_ID = value;
        }
    }

    public String CV_CODE
    {
        get
        {
            return m_CV_CODE;
        }
        set
        {
            m_CV_CODE = value;
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

    public Nullable<Decimal> Payment_Amount
    {
        get
        {
            return m_Payment_Amount;
        }
        set
        {
            m_Payment_Amount = value;
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

}


