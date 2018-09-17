using System;
public class dbo_RequisitionDetailClass
{
    private String m_Requisition_Detail_ID;
    private String m_Requisition_No;
    private String m_Time_No;
    private String m_Product_ID;
    private String m_Product_Name;
    private String m_Unit_of_item;
    private Nullable<Decimal> m_Price;
    private Nullable<Byte> m_Vat;
    private Nullable<Int16> m_Previous_Balance_Qty;
    private Nullable<Int16> m_Suggestion_Qty;
    private Nullable<Int16> m_Sub_Total_Qty;
    private Nullable<Int16> m_Requisition_Qty;
    private Nullable<Int16> m_Total_Qty;
    private Nullable<Decimal> m_Selling_Price;
    private Nullable<Decimal> m_Total_Price;
    private Nullable<Decimal> m_Commission;
    private Nullable<Int16> m_Point;
    private Nullable<Int16> m_Total;
    private Nullable<Int16> m_Old_Total;

    private Nullable<Int16> m_Deposit_Qty;
    private Nullable<Int16> m_Return_Qty;

    public dbo_RequisitionDetailClass() { }

    public String Requisition_Detail_ID
    {
        get
        {
            return m_Requisition_Detail_ID;
        }
        set
        {
            m_Requisition_Detail_ID = value;
        }
    }

    public String Requisition_No
    {
        get
        {
            return m_Requisition_No;
        }
        set
        {
            m_Requisition_No = value;
        }
    }

    public String Time_No
    {
        get
        {
            return m_Time_No;
        }
        set
        {
            m_Time_No = value;
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

    public String Product_Name
    {
        get
        {
            return m_Product_Name;
        }
        set
        {
            m_Product_Name = value;
        }
    }

    public String Unit_of_item
    {
        get
        {
            return m_Unit_of_item;
        }
        set
        {
            m_Unit_of_item = value;
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

    public Nullable<Byte> Vat
    {
        get
        {
            return m_Vat;
        }
        set
        {
            m_Vat = value;
        }
    }

    public Nullable<Int16> Previous_Balance_Qty
    {
        get
        {
            return m_Previous_Balance_Qty;
        }
        set
        {
            m_Previous_Balance_Qty = value;
        }
    }

    public Nullable<Int16> Suggestion_Qty
    {
        get
        {
            return m_Suggestion_Qty;
        }
        set
        {
            m_Suggestion_Qty = value;
        }
    }

    public Nullable<Int16> Sub_Total_Qty
    {
        get
        {
            return m_Sub_Total_Qty;
        }
        set
        {
            m_Sub_Total_Qty = value;
        }
    }

    public Nullable<Int16> Requisition_Qty
    {
        get
        {
            return m_Requisition_Qty;
        }
        set
        {
            m_Requisition_Qty = value;
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

    public Nullable<Int16> Old_Total_Qty
    {
        get
        {
            return m_Old_Total;
        }
        set
        {
            m_Old_Total = value;
        }
    }

    public Nullable<Int16> Total
    {
        get
        {
            return m_Total;
        }
        set
        {
            m_Total = value;
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

    public Nullable<Decimal> Total_Price
    {
        get
        {
            return m_Total_Price;
        }
        set
        {
            m_Total_Price = value;
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
}


