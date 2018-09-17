using System;

public class dbo_AccountCodeClass
{
    private String m_Account_Type_ID;
    private String m_Account_Code;
    private String m_Account_Name;

    public dbo_AccountCodeClass() { }

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