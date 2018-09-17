using System;
public class dbo_SubsidyClass
{
    private String m_Subsidy_ID;
    private String m_Clearing_No;
    private String m_Subsidy_Detail;
    private Nullable<Decimal> m_Subsidy_Amount;

    public dbo_SubsidyClass() { }
    private String m_Account_Code;

    public String Account_Code
    {
        get
        {
            return m_Account_Code;
        }
        set
        {
            m_Account_Code = value;
        }
    }
    public String Subsidy_ID
    {
        get
        {
            return m_Subsidy_ID;
        }
        set
        {
            m_Subsidy_ID = value;
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

    public String Subsidy_Detail
    {
        get
        {
            return m_Subsidy_Detail;
        }
        set
        {
            m_Subsidy_Detail = value;
        }
    }

    public Nullable<Decimal> Subsidy_Amount
    {
        get
        {
            return m_Subsidy_Amount;
        }
        set
        {
            m_Subsidy_Amount = value;
        }
    }

}


