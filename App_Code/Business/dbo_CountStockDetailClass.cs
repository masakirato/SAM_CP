using System;
public class dbo_CountStockDetailClass
{
    private String m_Count_Stock_Detail_ID;
    private String m_Count_No;
    private String m_Product_ID;
    private Nullable<Int16> m_Quantity;
    private Nullable<Int16> m_Count_Quantity;
    private Nullable<Int16> m_Diff_Quantity;
    private String m_Remark;

    public dbo_CountStockDetailClass() { }

    public String Count_Stock_Detail_ID
    {
        get
        {
            return m_Count_Stock_Detail_ID;
        }
        set
        {
            m_Count_Stock_Detail_ID = value;
        }
    }

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

    public Nullable<Int16> Quantity
    {
        get
        {
            return m_Quantity;
        }
        set
        {
            m_Quantity = value;
        }
    }

    public Nullable<Int16> Count_Quantity
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

    public String Remark
    {
        get
        {
            return m_Remark;
        }
        set
        {
            m_Remark = value;
        }
    }

}


