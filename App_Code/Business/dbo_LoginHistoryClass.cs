using System;
public class dbo_LoginHistoryClass
{
    private String m_User_ID;
    private Nullable<DateTime> m_Login_Time;
    private String  m_Status;

    public dbo_LoginHistoryClass() { }

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

    public Nullable<DateTime> Login_Time
    {
        get
        {
            return m_Login_Time;
        }
        set
        {
            m_Login_Time = value;
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


