using System;
using System.Collections.Generic;

public class MenuNode
{
    private int m_Parent_MenuID;

    private String m_Function_Name;

    private String m_Function_ID;

    private int m_MenuID;

    private List<MenuNode> m_ListOfMenuNode;

    public int Parent_MenuID
    {
        get
        {
            return m_Parent_MenuID;
        }
        set
        {
            m_Parent_MenuID = value;
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

    public String Function_ID
    {
        get
        {
            return m_Function_ID;
        }
        set
        {
            m_Function_ID = value;
        }
    }

    public int MenuID
    {
        get
        {
            return m_MenuID;
        }
        set
        {
            m_MenuID = value;
        }
    }

    public List<MenuNode> ListOfMenuNode
    {
        get
        {
            return m_ListOfMenuNode;
        }
        set
        {
            m_ListOfMenuNode = value;
        }
    }
}