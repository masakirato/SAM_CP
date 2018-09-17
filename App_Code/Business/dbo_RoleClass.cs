using System;
public class dbo_RoleClass
{
    private String m_Role_ID;
    private String m_Role_Name;

    private String m_Role_Type;


    public dbo_RoleClass() { }


    public String Role_Type
    {
        get
        {
            return m_Role_Type;
        }
        set
        {
            m_Role_Type = value;
        }
    }


    public String Role_ID
    {
        get
        {
            return m_Role_ID;
        }
        set
        {
            m_Role_ID = value;
        }
    }

    public String Role_Name
    {
        get
        {
            return m_Role_Name;
        }
        set
        {
            m_Role_Name = value;
        }
    }

}


