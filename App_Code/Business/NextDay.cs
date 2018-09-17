using System;

public class NextDay
{
    private String m_Order_Cycle_Date;
    private String m_Delivery_Cycle_Date;
    private int m_next_day;

    public String Order_Cycle_Date
    {
        get
        {
            return m_Order_Cycle_Date;
        }
        set
        {
            m_Order_Cycle_Date = value;
        }
    }

    public String Delivery_Cycle_Date
    {
        get
        {
            return m_Delivery_Cycle_Date;
        }
        set
        {
            m_Delivery_Cycle_Date = value;
        }
    }

    public int nextday
    {
        get
        {
            return m_next_day;
        }
        set
        {
            m_next_day = value;
        }
    }
}