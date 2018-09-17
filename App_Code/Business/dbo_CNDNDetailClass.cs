using System;
public class dbo_CNDNDetailClass
{
    private int m_index;
    private String m_Item;

    private String m_CNDN_Detail_ID;
    private String m_SAM_CN_DN_No;

    private String m_Product_Name;
    private String m_Unit_of_item_ID;

    private Nullable<Decimal> m_Price;
    private Nullable<Byte> m_Vat;
    private Nullable<Int16> m_Quantity;
    private Nullable<Decimal> m_Sub_Total;
    private Nullable<Int16> m_Qty;

    private String m_Product_ID;


    private Nullable<Decimal> m_Agent_Price;
    public dbo_CNDNDetailClass() { }
       


    public String Item
    {
        get
        {
            return m_Item;
        }
        set
        {
            m_Item = value;
        }
    }

    public int index
    {
        get
        {
            return m_index;
        }
        set
        {
            m_index = value;
        }
    }

    public Nullable<Decimal> Agent_Price
    {
        get
        {
            return m_Agent_Price;
        }
        set
        {
            m_Agent_Price = value;
        }
    }
    
    public String CNDN_Detail_ID
    {
        get
        {
            return m_CNDN_Detail_ID;
        }
        set
        {
            m_CNDN_Detail_ID = value;
        }
    }

    public String SAM_CN_DN_No
    {
        get
        {
            return m_SAM_CN_DN_No;
        }
        set
        {
            m_SAM_CN_DN_No = value;
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

    public String Unit_of_item_ID
    {
        get
        {
            return m_Unit_of_item_ID;
        }
        set
        {
            m_Unit_of_item_ID = value;
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

    public Nullable<Int16> Qty
    {
        get
        {
            return m_Qty;
        }
        set
        {
            m_Qty = value;
        }
    }
}


