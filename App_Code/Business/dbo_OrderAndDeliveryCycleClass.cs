using System;
public class dbo_OrderAndDeliveryCycleClass
{
    private String m_Order_Cycle_ID;
    private String m_Order_Cycle_Name;
    private String m_Last_Modified_By;

    public dbo_OrderAndDeliveryCycleClass() { }

    public String Order_Cycle_ID
    {
        get
        {
            return m_Order_Cycle_ID;
        }
        set
        {
            m_Order_Cycle_ID = value;
        }
    }

    public String Order_Cycle_Name
    {
        get
        {
            return m_Order_Cycle_Name;
        }
        set
        {
            m_Order_Cycle_Name = value;
        }
    }

    public String Last_Modified_By
    {
        get
        {
            return m_Last_Modified_By;
        }
        set
        {
            m_Last_Modified_By = value;
        }
    }

}


