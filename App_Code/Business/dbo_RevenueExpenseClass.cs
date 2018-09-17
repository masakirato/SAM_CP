using System;
public class dbo_RevenueExpenseClass
{
    private Nullable<DateTime> m_Post_Date;
    private String m_Post_No;
    
    private String m_Account_No;
    private String m_RV_Account_No;
    private String m_EP_Account_No;

    private String m_Account_Code;
    private Nullable<Decimal> m_Amount;

    private Nullable<Decimal> m_RV_Amount;
    private Nullable<Decimal> m_EP_Amount;

    private String m_Remark;
    private String m_CV_Code;
    private String m_Account_Name;
    private String m_User_ID;

    public dbo_RevenueExpenseClass() { }


    public String RV_Account_No
    {
        get
        {
            return m_RV_Account_No;
        }
        set
        {
            m_RV_Account_No = value;
        }
    }

    public String EP_Account_No
    {
        get
        {
            return m_EP_Account_No;
        }
        set
        {
            m_EP_Account_No = value;
        }
    }


    public Nullable<Decimal> RV_Amount
    {
        get
        {
            return m_RV_Amount;
        }
        set
        {
            m_RV_Amount = value;
        }
    }


    public Nullable<Decimal> EP_Amount
    {
        get
        {
            return m_EP_Amount;
        }
        set
        {
            m_EP_Amount = value;
        }
    }

    public Nullable<DateTime> Post_Date
    {
        get
        {
            return m_Post_Date;
        }
        set
        {
            m_Post_Date = value;
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

    public String Post_No
    {
        get
        {
            return m_Post_No;
        }
        set
        {
            m_Post_No = value;
        }
    }

    public String Account_No
    {
        get
        {
            return m_Account_No;
        }
        set
        {
            m_Account_No = value;
        }
    }



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

    public Nullable<Decimal> Amount
    {
        get
        {
            return m_Amount;
        }
        set
        {
            m_Amount = value;
        }
    }

    public String Remark
    {
        get
        {
            return m_Remark;
        }
        set
        {
            m_Remark = value;
        }
    }
    public String Account_Name
    {
        get
        {
            return m_Account_Name;
        }
        set
        {
            m_Account_Name = value;
        }
    }

}


