using System;
public class dbo_SalesTargetClass
{
    private String m_Sales_Target_ID;
    private String m_CV_Code;


    private String m_Year;
    private String m_Month;
    private Nullable<Decimal> m_Sales_Target;
    private Nullable<Decimal> m_Actual_Sales;
    private Nullable<Decimal> m_Actual_PO;

    private Nullable<Decimal> m_Actual_Sales_Quarter;
    private Nullable<Decimal> m_Sales_Target_Quarter;
    private Nullable<Decimal> m_Actual_Sales_Year;
    private Nullable<Decimal> m_Sales_Target_Year;
    private String m_AgentName;
    private String m_MonthName;

 //   private String m_AgentName;


    public dbo_SalesTargetClass() { }

    /*public String AgentName
    {
        get
        {
            return m_AgentName;
        }
        set
            {
            m_AgentName = value;
        }
       
    }*/

    public String AgentName
    {
        get
        {
            return m_AgentName;
    }
        set
        {
            m_AgentName = value;
        }
    }


    public String MonthName
    {
        get
        {
            return m_MonthName;
        }
        set
        {
            m_MonthName = value;
        }
    }
    public String Sales_Target_ID
    {
        get
        {
            return m_Sales_Target_ID;
        }
        set
        {
            m_Sales_Target_ID = value;
        }
    }

    public String CV_Code
    {
        get
        {
            return m_CV_Code;
        }
        set
        {
            m_CV_Code = value;
        }
    }

    public String Year
    {
        get
        {
            return m_Year;
        }
        set
        {
            m_Year = value;
        }
    }

    public String Month
    {
        get
        {
            return m_Month;
        }
        set
        {
            m_Month = value;
        }
    }

    public Nullable<Decimal> Sales_Target
    {
        get
        {
            return m_Sales_Target.HasValue ? m_Sales_Target : 0;
        }
        set
        {
            m_Sales_Target = value;
        }
    }

    public Nullable<Decimal> Actual_Sales
    {
        get
        {
            return m_Actual_Sales.HasValue ? m_Actual_Sales : 0;
        }
        set
        {
            m_Actual_Sales = value;
        }
    }

    public Nullable<Decimal> Actual_PO
    {
        get
        {
            return m_Actual_PO.HasValue ? m_Actual_PO : 0;
        }
        set
        {
            m_Actual_PO = value;
        }
    }


    public Nullable<Decimal> Actual_Sales_Quarter
    {
        get
        {
            return m_Actual_Sales_Quarter.HasValue ? m_Actual_Sales_Quarter : 0;
        }
        set
        {
            m_Actual_Sales_Quarter = value;
        }
    }

    public Nullable<Decimal> Sales_Target_Quarter
    {
        get
        {
            return m_Sales_Target_Quarter.HasValue ? m_Sales_Target_Quarter : 0;
        }
        set
        {
            m_Sales_Target_Quarter = value;
        }
    }


    public Nullable<Decimal> Actual_Sales_Year
    {
        get
        {
            return m_Actual_Sales_Year.HasValue ? m_Actual_Sales_Year : 0;
        }
        set
        {
            m_Actual_Sales_Year = value;
        }
    }

    public Nullable<Decimal> Sales_Target_Year
    {
        get
        {
            return m_Sales_Target_Year.HasValue ? m_Sales_Target_Year : 0;
        }
        set
        {
            m_Sales_Target_Year = value;
        }
    }
}


