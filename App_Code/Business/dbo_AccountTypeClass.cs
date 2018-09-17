using System;

public class dbo_AccountTypeClass
{
    private String m_Account_ID;
    private String m_Account_Type_ID;
    private String m_Account_Type;

    public dbo_AccountTypeClass() { }

    public String Account_ID
    {
        get
        {
            return m_Account_ID;
        }
        set
        {
            m_Account_ID = value;
        }
    }

    public String Account_Type_ID
    {
        get
        {
            return m_Account_Type_ID;
        }
        set
        {
            m_Account_Type_ID = value;
        }
    }

    public String Account_Type
    {
        get
        {
            return m_Account_Type;
        }
        set
        {
            m_Account_Type = value;
        }
    }
}