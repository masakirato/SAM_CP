using System;
public class dbo_DeductClass
{
    private String m_Deduct_ID;
    private String m_Clearing_No;
    private String m_Deduct_Detail;
    private Nullable<Decimal> m_Deduct_Amount;
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

    public dbo_DeductClass() { }

    public String Deduct_ID
    {
        get
        {
            return m_Deduct_ID;
        }
        set
        {
            m_Deduct_ID = value;
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

    public String Deduct_Detail
    {
        get
        {
            return m_Deduct_Detail;
        }
        set
        {
            m_Deduct_Detail = value;
        }
    }

    public Nullable<Decimal> Deduct_Amount
    {
        get
        {
            return m_Deduct_Amount;
        }
        set
        {
            m_Deduct_Amount = value;
        }
    }

}


