using System;
public class dbo_PriceGroupAssignmentClass
{
    private String m_Price_Group_ID;
    private String m_Assign_To;
    private String m_Price_Group_Type;
    public dbo_PriceGroupAssignmentClass() { }

    public String Price_Group_ID
    {
        get
        {
            return m_Price_Group_ID;
        }
        set
        {
            m_Price_Group_ID = value;
        }
    }

    public String Assign_To
    {
        get
        {
            return m_Assign_To;
        }
        set
        {
            m_Assign_To = value;
        }
    }
    public String Price_Group_Type
    {
        get
        {
            return m_Price_Group_Type;
        }
        set
        {
            m_Price_Group_Type = value;
        }
    }
}


