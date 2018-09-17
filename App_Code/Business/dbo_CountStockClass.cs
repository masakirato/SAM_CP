using System;
public class dbo_CountStockClass
{
    private String m_Count_No;
    private String m_CV_Code;
    private Nullable<DateTime> m_Count_Date;
    private String m_Status;
    private Nullable<Int32> m_Stock_on_Hand;
    private Nullable<Int32> m_Count_Quantity;
    private Nullable<Int16> m_Diff_Quantity;

    public dbo_CountStockClass() { }

    public String Count_No
    {
        get
        {
            return m_Count_No;
        }
        set
        {
            m_Count_No = value;
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

    public Nullable<DateTime> Count_Date
    {
        get
        {
            return m_Count_Date;
        }
        set
        {
            m_Count_Date = value;
        }
    }

    public String Status
    {
        get
        {
            return m_Status;
        }
        set
        {
            m_Status = value;
        }
    }

    public Nullable<Int32> Stock_on_Hand
    {
        get
        {
            return m_Stock_on_Hand;
        }
        set
        {
            m_Stock_on_Hand = value;
        }
    }

    public Nullable<Int32> Count_Quantity
    {
        get
        {
            return m_Count_Quantity;
        }
        set
        {
            m_Count_Quantity = value;
        }
    }

    public Nullable<Int16> Diff_Quantity
    {
        get
        {
            return m_Diff_Quantity;
        }
        set
        {
            m_Diff_Quantity = value;
        }
    }

}


