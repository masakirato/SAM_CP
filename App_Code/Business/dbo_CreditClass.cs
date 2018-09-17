using System;
public class dbo_CreditClass
{
    private String m_Credit_ID;
    private String m_Clearing_No;
    private String m_Customer_ID;
    private String m_Customer_Name;
    private Nullable<DateTime> m_Credit_Date;
    private Nullable<Decimal> m_Credit_Amount;
    private Nullable<Decimal> m_Total_Payment_Amount;
    private Nullable<Decimal> m_Balance_Outstanding_Amount;
    private String m_Status;

    public dbo_CreditClass() { }

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

    public String Customer_Name
    {
        get
        {
            if (dbo_CustomerDataClass.Select_Record(m_Customer_ID) != null)
            {
                m_Customer_Name = dbo_CustomerDataClass.Select_Record(m_Customer_ID).Full_Name;

                return m_Customer_Name;
            }
            return string.Empty;
           
        }
        set
        {
            m_Customer_Name = value;
        }
    }

    public Nullable<DateTime> Credit_Date
    {
        get
        {
            return m_Credit_Date;
        }
        set
        {
            m_Credit_Date = value;
        }
    }

    public Nullable<Decimal> Credit_Amount
    {
        get
        {
            return m_Credit_Amount;
        }
        set
        {
            m_Credit_Amount = value;
        }
    }

    public Nullable<Decimal> Total_Payment_Amount
    {
        get
        {
            return m_Total_Payment_Amount;
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

}


