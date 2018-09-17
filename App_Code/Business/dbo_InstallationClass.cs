using System;
public class dbo_InstallationClass
{
    private String m_Installation_ID;
    private String m_User_ID;
    private String m_Installation_Detail;
    private String m_Installation_Type;
    private String m_Description;
    private Nullable<Decimal> m_Installation_Amount;
    private Nullable<DateTime> m_Transaction_Date;
    private Nullable<DateTime> m_Due_Date;
    private Nullable<Decimal> m_Balance_Amount;
    private Nullable<Decimal> m_Payment_Amount;


    public dbo_InstallationClass() { }

    public String Installation_ID
    {
        get
        {
            return m_Installation_ID;
        }
        set
        {
            m_Installation_ID = value;
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

    public String Installation_Detail
    {
        get
        {
            return m_Installation_Detail;
        }
        set
        {
            m_Installation_Detail = value;
        }
    }

    public String Installation_Type
    {
        get
        {
            return m_Installation_Type;
        }
        set
        {
            m_Installation_Type = value;
        }
    }

    public String Description
    {
        get
        {
            return m_Description;
        }
        set
        {
            m_Description = value;
        }
    }

    public Nullable<Decimal> Installation_Amount
    {
        get
        {
            return m_Installation_Amount;
        }
        set
        {
            m_Installation_Amount = value;
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

    public Nullable<DateTime> Due_Date
    {
        get
        {
            return m_Due_Date;
        }
        set
        {
            m_Due_Date = value;
        }
    }

/*    public Nullable<Decimal> Balance_Amount
    {
        get
        {
            return m_Balance_Amount;
        }
        set
        {
            m_Balance_Amount = value;
        }
    }
      public Nullable<Decimal> Paid
    {
        get
        {
            return m_Installation_Amount - m_Balance_Amount;
        }
        //set
        //{
        //    m_Balance_Amount = value;
        //}
    }*/

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
    public Nullable<Decimal> Balance_Amount
    {
        get
        {
            return m_Installation_Amount - m_Payment_Amount;
        }
        set
        {
            m_Balance_Amount = value;
        }
    }

}


