using System;
public class dbo_BenefitClass
{
    private String m_Benefit_ID;
    private String m_User_ID;
    private Nullable<DateTime> m_Benefit_Date;
    private String m_Benefit_Name;
    private String m_Beneficiary;
    private String m_Relationship;
    private Nullable<Decimal> m_Benefit_Amount;
    private Nullable<DateTime> m_End_Date;

    public dbo_BenefitClass() { }

    public String Benefit_ID
    {
        get
        {
            return m_Benefit_ID;
        }
        set
        {
            m_Benefit_ID = value;
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

    public Nullable<DateTime> Benefit_Date
    {
        get
        {
            return m_Benefit_Date;
        }
        set
        {
            m_Benefit_Date = value;
        }
    }

    public String Benefit_Name
    {
        get
        {
            return m_Benefit_Name;
        }
        set
        {
            m_Benefit_Name = value;
        }
    }

    public String Beneficiary
    {
        get
        {
            return m_Beneficiary;
        }
        set
        {
            m_Beneficiary = value;
        }
    }

    public String Relationship
    {
        get
        {
            return m_Relationship;
        }
        set
        {
            m_Relationship = value;
        }
    }

    public Nullable<Decimal> Benefit_Amount
    {
        get
        {
            return m_Benefit_Amount;
        }
        set
        {
            m_Benefit_Amount = value;
        }
    }

    public Nullable<DateTime> End_Date
    {
        get
        {
            return m_End_Date;
        }
        set
        {
            m_End_Date = value;
        }
    }
}


