using System;
public class dbo_OrderAndDeliveryCycleValueClass
{
    private String m_Order_Cycle_ID;
    private String m_CV_CODE;
    private String m_Order_Cycle_Date;
    private String m_Order_Cycle_Hour;
    private String m_Order_Cycle_Minute;
    private String m_Delivery_Cycle_Date;
    private String m_Route;
    private int? m_OrderAndDeliveryCycleValue_ID;
    private String m_WindowTime;
    private String m_WindowTime_ddl;
    private String m_Order_Cycle_Date_Name;
    private String m_Delivery_Cycle_Date_Name;


    public dbo_OrderAndDeliveryCycleValueClass() { }


    public int? OrderAndDeliveryCycleValue_ID
    {
        get
        {
            return m_OrderAndDeliveryCycleValue_ID;
        }
        set
        {
            m_OrderAndDeliveryCycleValue_ID = value;
        }
    }
    public String WindowTime
    {
        get
        {
            return m_WindowTime;
        }
        set
        {
            m_WindowTime = value;
        }
    }

    public String WindowTime_Full
    {
        get
        {
            if (string.IsNullOrEmpty(m_WindowTime_ddl))
            {
                return m_Order_Cycle_ID + " | " + m_WindowTime;
            }
            else { return m_WindowTime_ddl; }
        }
        set
        {
            m_WindowTime_ddl = value;
        }
    }


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

    public String CV_CODE
    {
        get
        {
            return m_CV_CODE;
        }
        set
        {
            m_CV_CODE = value;
        }
    }

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

    public String Order_Cycle_Hour
    {
        get
        {
            return m_Order_Cycle_Hour;
        }
        set
        {
            m_Order_Cycle_Hour = value;
        }
    }

    public String Order_Cycle_Minute
    {
        get
        {
            return m_Order_Cycle_Minute;
        }
        set
        {
            m_Order_Cycle_Minute = value;
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

    public String Route
    {
        get
        {
            return m_Route;
        }
        set
        {
            m_Route = value;
        }
    }
    public String Order_Cycle_Date_Name
    {
        get
        {
            return m_Order_Cycle_Date_Name;
        }
        set
        {
            m_Order_Cycle_Date_Name = value;
        }
    }
    public String Delivery_Cycle_Date_Name
    {
        get
        {
            return m_Delivery_Cycle_Date_Name;
        }
        set
        {
            m_Delivery_Cycle_Date_Name = value;
        }
    }
}


