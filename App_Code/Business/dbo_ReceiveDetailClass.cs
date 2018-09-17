using System;
public class dbo_ReceiveDetailClass
{
    private String m_Receive_Detail_ID;
    private String m_Billing_Detail_ID;
    private String m_Material_No;
    private Nullable<Int16> m_Qty;
    private String m_UOM;
    private Nullable<Decimal> m_Net_Value;
    private Nullable<Decimal> m_Vat;
    private Nullable<Decimal> m_Total;

    public dbo_ReceiveDetailClass() { }

    public String Receive_Detail_ID
    {
        get
        {
            return m_Receive_Detail_ID;
        }
        set
        {
            m_Receive_Detail_ID = value;
        }
    }

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


