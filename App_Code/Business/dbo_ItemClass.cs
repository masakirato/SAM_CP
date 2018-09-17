using System;
public class dbo_ItemClass
{
    private String m_Item_ID;
    private String m_Item_Name;

    public dbo_ItemClass() { }

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

    public String Item_Name
    {
        get
        {
            return m_Item_Name;
        }
        set
        {
            m_Item_Name = value;
        }
    }

}


