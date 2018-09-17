using System;
public class dbo_StockClass
{
    private String m_Stock_on_Hand_ID;
    private String m_CV_Code;
    private Nullable<DateTime> m_Date;
    private String m_Product_ID;
    private Nullable<Int16> m_Stock_Begin;
    private Nullable<Int16> m_Stock_In;
    private Nullable<Int16> m_Stock_Out;
    private Nullable<Int16> m_Stock_End;

    public dbo_StockClass() { }

    public String Stock_on_Hand_ID
    {
        get
        {
            return m_Stock_on_Hand_ID;
        }
        set
        {
            m_Stock_on_Hand_ID = value;
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

    public Nullable<DateTime> Date
    {
        get
        {
            return m_Date;
        }
        set
        {
            m_Date = value;
        }
    }

    public String Product_ID
    {
        get
        {
            return m_Product_ID;
        }
        set
        {
            m_Product_ID = value;
        }
    }

    public Nullable<Int16> Stock_Begin
    {
        get
        {
            return m_Stock_Begin;
        }
        set
        {
            m_Stock_Begin = value;
        }
    }

    public Nullable<Int16> Stock_In
    {
        get
        {
            return m_Stock_In;
        }
        set
        {
            m_Stock_In = value;
        }
    }

    public Nullable<Int16> Stock_Out
    {
        get
        {
            return m_Stock_Out;
        }
        set
        {
            m_Stock_Out = value;
        }
    }

    public Nullable<Int16> Stock_End
    {
        get
        {
            return m_Stock_End;
        }
        set
        {
            m_Stock_End = value;
        }
    }

}


