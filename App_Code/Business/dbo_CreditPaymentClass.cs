using System;
public class dbo_CreditPaymentClass
{
    private String m_Payment_No;
    private String m_Credit_ID;
    private Nullable<DateTime> m_Payment_Date;
    private Nullable<Decimal> m_Payment_Amount;
    private String m_Payment_Method;
    private String m_Bank;
    private String m_Cheque_No;
    private Nullable<DateTime> m_Date;
    private Nullable<Boolean> m_Clearing_Cheque;
    private String m_Clearing_No;
    private Nullable<DateTime> m_Last_Modified_Date;




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


    public String Clearing_No
    {
        get
        {
            return m_Clearing_No;
        }
        set
        {
            m_Clearing_No = value;
        }
    }


    public dbo_CreditPaymentClass() { }

    public String Payment_No
    {
        get
        {
            return m_Payment_No;
        }
        set
        {
            m_Payment_No = value;
        }
    }

    public String Credit_ID
    {
        get
        {
            return m_Credit_ID;
        }
        set
        {
            m_Credit_ID = value;
        }
    }

    public Nullable<DateTime> Payment_Date
    {
        get
        {
            return m_Payment_Date;
        }
        set
        {
            m_Payment_Date = value;
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

    public String Payment_Method
    {
        get
        {
            return m_Payment_Method;
        }
        set
        {
            m_Payment_Method = value;
        }
    }

    public String Bank
    {
        get
        {
            return m_Bank;
        }
        set
        {
            m_Bank = value;
        }
    }

    public String Cheque_No
    {
        get
        {
            return m_Cheque_No;
        }
        set
        {
            m_Cheque_No = value;
        }
    }

    public Nullable<DateTime> Date
    {
        get
        {
            return m_Date;
        }
        set
        {
            m_Date = value;
        }
    }

    public Nullable<Boolean> Clearing_Cheque
    {
        get
        {
            return m_Clearing_Cheque;
        }
        set
        {
            m_Clearing_Cheque = value;
        }
    }

}


