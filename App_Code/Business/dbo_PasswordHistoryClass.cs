using System;
public class dbo_PasswordHistoryClass
{
    private String m_User_ID;
    private String m_Password;
    private Nullable<DateTime> m_Last_Password_Change_Or_Reset;

    public dbo_PasswordHistoryClass() { }

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

    public String Password
    {
        get
        {
            return m_Password;
        }
        set
        {
            m_Password = value;
        }
    }

    public Nullable<DateTime> Last_Password_Change_Or_Reset
    {
        get
        {
            return m_Last_Password_Change_Or_Reset;
        }
        set
        {
            m_Last_Password_Change_Or_Reset = value;
        }
    }

}


