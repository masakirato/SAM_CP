using System;
public class dbo_ItemValueClass
{
    private String m_Item_ID;
    private String m_Item_Value_ID;
    private String m_Item_Value_Name;

    public dbo_ItemValueClass() { }

    public String Item_ID
    {
        get
        {
            return m_Item_ID;
        }
        set
        {
            m_Item_ID = value;
        }
    }

    public String Item_Value_ID
    {
        get
        {
            return m_Item_Value_ID;
        }
        set
        {
            m_Item_Value_ID = value;
        }
    }

    public String Item_Value_Name
    {
        get
        {
            return m_Item_Value_Name;
        }
        set
        {
            m_Item_Value_Name = value;
        }
    }
}