using System;
public class dbo_DepositDetailClass
{
    private String m_Deposit_Detail_ID;
    private String m_Clearing_No;
    private String m_Product_ID;
    private Nullable<Decimal> m_Price;
    private Nullable<Int16> m_Total_Qty;
    private Nullable<Int16> m_Deposit_Qty;
    private Nullable<Boolean> m_Deposit_Return_flag;
    private Nullable<Decimal> m_Selling_Price;
    private Nullable<Int16> m_Sales_Qty;
    private Nullable<Decimal> m_Sales_Amount;
    private Nullable<Decimal> m_Commission;
    private Nullable<Int16> m_Point;
    private String m_Product_List_ID;
    private Nullable<Int16> m_Return_Qty;

    public dbo_DepositDetailClass() { }

    public String Deposit_Detail_ID
    {
        get
        {
            return m_Deposit_Detail_ID;
        }
        set
        {
            m_Deposit_Detail_ID = value;
        }
    }

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

    public Nullable<Decimal> Price
    {
        get
        {
            return m_Price;
        }
        set
        {
            m_Price = value;
        }
    }

    public Nullable<Int16> Total_Qty
    {
        get
        {
            return m_Total_Qty;
        }
        set
        {
            m_Total_Qty = value;
        }
    }

    public Nullable<Int16> Deposit_Qty
    {
        get
        {
            return m_Deposit_Qty;
        }
        set
        {
            m_Deposit_Qty = value;
        }
    }

    public Nullable<Boolean> Deposit_Return_flag
    {
        get
        {
            return m_Deposit_Return_flag;
        }
        set
        {
            m_Deposit_Return_flag = value;
        }
    }

    public Nullable<Decimal> Selling_Price
    {
        get
        {
            return m_Selling_Price;
        }
        set
        {
            m_Selling_Price = value;
        }
    }

    public Nullable<Int16> Sales_Qty
    {
        get
        {
            return m_Sales_Qty;
        }
        set
        {
            m_Sales_Qty = value;
        }
    }

    public Nullable<Decimal> Sales_Amount
    {
        get
        {
            return m_Sales_Amount;
        }
        set
        {
            m_Sales_Amount = value;
        }
    }

    public Nullable<Decimal> Commission
    {
        get
        {
            return m_Commission;
        }
        set
        {
            m_Commission = value;
        }
    }

    public Nullable<Int16> Point
    {
        get
        {
            return m_Point;
        }
        set
        {
            m_Point = value;
        }
    }

    public String Product_List_ID
    {
        get
        {
            return m_Product_List_ID;
        }
        set
        {
            m_Product_List_ID = value;
        }
    }

    public Nullable<Int16> Return_Qty
    {
        get
        {
            return m_Return_Qty;
        }
        set
        {
            m_Return_Qty = value;
        }
    }

}


