using System;
public class dbo_ReadNewsClass
{
    private String m_News_ID;
    private String m_User_ID;
    private Nullable<DateTime> m_Read_Date;

    public dbo_ReadNewsClass() { }

    public String News_ID
    {
        get
        {
            return m_News_ID;
        }
        set
        {
            m_News_ID = value;
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

    public Nullable<DateTime> Read_Date
    {
        get
        {
            return m_Read_Date;
        }
        set
        {
            m_Read_Date = value;
        }
    }

}


