using System;
public class dbo_BillingDetailClass
{
    private String m_Billing_Detail_ID;
    private String m_Billing_ID;
    private String m_Item_Position;
    private String m_Material_No;
    private Nullable<Int16> m_Qty;
    private String m_UOM;
    private Nullable<Decimal> m_Net_Value;
    private Nullable<Decimal> m_Vat;
    private Nullable<Decimal> m_Total;

    public dbo_BillingDetailClass() { }

    public String Billing_Detail_ID
    {
        get
        {
            return m_Billing_Detail_ID;
        }
        set
        {
            m_Billing_Detail_ID = value;
        }
    }

    public String Billing_ID
    {
        get
        {
            return m_Billing_ID;
        }
        set
        {
            m_Billing_ID = value;
        }
    }

    public String Item_Position
    {
        get
        {
            return m_Item_Position;
        }
        set
        {
            m_Item_Position = value;
        }
    }

    public String Material_No
    {
        get
        {
            return m_Material_No;
        }
        set
        {
            m_Material_No = value;
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

    public String UOM
    {
        get
        {
            return m_UOM;
        }
        set
        {
            m_UOM = value;
        }
    }

    public Nullable<Decimal> Net_Value
    {
        get
        {
            return m_Net_Value;
        }
        set
        {
            m_Net_Value = value;
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

}


