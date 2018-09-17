using System;
public class dbo_DepositClass
{
    private String m_Clearing_No;
    private Nullable<Int16> m_Net_Sales_Qty;
    private Nullable<Decimal> m_Net_Sales_Amount;
    private Nullable<Decimal> m_Total_Commission;
    private Nullable<Int16> m_Tota_Point;

    public dbo_DepositClass() { }

    public String Clearing_No
    {
        get
        {
            return m_Clearing_No;
        }
        set
        {
            m_Clearing_No = value;
        }
    }

    public Nullable<Int16> Net_Sales_Qty
    {
        get
        {
            return m_Net_Sales_Qty;
        }
        set
        {
            m_Net_Sales_Qty = value;
        }
    }

    public Nullable<Decimal> Net_Sales_Amount
    {
        get
        {
            return m_Net_Sales_Amount;
        }
        set
        {
            m_Net_Sales_Amount = value;
        }
    }

    public Nullable<Decimal> Total_Commission
    {
        get
        {
            return m_Total_Commission;
        }
        set
        {
            m_Total_Commission = value;
        }
    }

    public Nullable<Int16> Tota_Point
    {
        get
        {
            return m_Tota_Point;
        }
        set
        {
            m_Tota_Point = value;
        }
    }

}


