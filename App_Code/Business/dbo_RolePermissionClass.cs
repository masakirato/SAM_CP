using System;
public class dbo_RolePermissionClass
{
    private String m_Role_Permission_ID;
    private String m_Role_ID;
    private String m_Function_Name;

    public dbo_RolePermissionClass() { }

    public String Role_Permission_ID
    {
        get
        {
            return m_Role_Permission_ID;
        }
        set
        {
            m_Role_Permission_ID = value;
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

    public String Function_Name
    {
        get
        {
            return m_Function_Name;
        }
        set
        {
            m_Function_Name = value;
        }
    }

}


