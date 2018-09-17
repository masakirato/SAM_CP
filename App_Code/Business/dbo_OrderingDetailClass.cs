using System;
public class dbo_OrderingDetailClass
{
    private String m_Ordering_Detail_ID;
    private String m_PO_Number;
    private String m_Product_ID;
    private Nullable<Decimal> m_Price;
    private Nullable<Decimal> m_Vat;
    private Nullable<Int16> m_Stock_on_hand;
    private Nullable<Int16> m_Suggest_Quantity;
    private Nullable<Int16> m_Quantity;
    private Nullable<Decimal> m_Sub_Total;
    private Nullable<Decimal> m_Vat_Amount;
    private Nullable<Decimal> m_Total;
    private Nullable<Int16> m_Point;
    private String m_Product_Name;
    private String m_Unit_of_Item;
    private Nullable<Byte> m_Packing_Size;
    public dbo_OrderingDetailClass() { }

    public String Ordering_Detail_ID
    {
        get
        {
            return m_Ordering_Detail_ID;
        }
        set
        {
            m_Ordering_Detail_ID = value;
        }
    }

    public String PO_Number
    {
        get
        {
            return m_PO_Number;
        }
        set
        {
            m_PO_Number = value;
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

    public Nullable<Decimal> Vat
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

    public Nullable<Int16> Stock_on_hand
    {
        get
        {
            return m_Stock_on_hand;
        }
        set
        {
            m_Stock_on_hand = value;
        }
    }

    public Nullable<Int16> Suggest_Quantity
    {
        get
        {
            return m_Suggest_Quantity;
        }
        set
        {
            m_Suggest_Quantity = value;
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

    public Nullable<Decimal> Sub_Total
    {
        get
        {
            return m_Sub_Total;
        }
        set
        {
            m_Sub_Total = value;
        }
    }

    public Nullable<Decimal> Vat_Amount
    {
        get
        {
            return m_Vat_Amount;
        }
        set
        {
            m_Vat_Amount = value;
        }
    }

    public Nullable<Decimal> Total
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
    public Nullable<Byte> Packing_Size
    {
        get
        {
            return m_Packing_Size;
        }
        set
        {
            m_Packing_Size = value;
        }
    }
    public String Unit_of_Item
    {
        get
        {
            return m_Unit_of_Item;
        }
        set
        {
            m_Unit_of_Item = value;
        }
    }
}


