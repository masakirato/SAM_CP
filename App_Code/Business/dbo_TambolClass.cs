using System;
public class dbo_TambolClass
{
    private String m_Sub_district;
    private String m_District;
    private String m_Province;
    private Int32 m_ID;
    private String m_Postal_ID;

    public dbo_TambolClass() { }


    public String Postal_ID
    {
        get
        {
            return m_Postal_ID;
        }
        set
        {
            m_Postal_ID = value;
        }
    }

    public String Sub_district
    {
        get
        {
            return m_Sub_district;
        }
        set
        {
            m_Sub_district = value;
        }
    }

    public String District
    {
        get
        {
            return m_District;
        }
        set
        {
            m_District = value;
        }
    }

    public String Province
    {
        get
        {
            return m_Province;
        }
        set
        {
            m_Province = value;
        }
    }

    public Int32 ID
    {
        get
        {
            return m_ID;
        }
        set
        {
            m_ID = value;
        }
    }
}


