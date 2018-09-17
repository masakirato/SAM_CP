using System;
public class dbo_OtherRequisitionDetailClass
{
    private String m_Other_Requisition_Detail_ID;
    private String m_Requisition_No;
    private String m_Product_ID;
    private Nullable<Decimal> m_Price;
    private Nullable<Byte> m_Vat;
    private Nullable<Int32> m_Stock_on_Hand;
    private Nullable<Int16> m_Requisition_Qty;
    private Nullable<Int16> m_Old_Qty;

    private String m_Stock_ID;

    public String Stock_ID
    {
        get
        {
            return m_Stock_ID;
        }
        set
        {
            m_Stock_ID = value;
        }
    }

    public dbo_OtherRequisitionDetailClass() { }

    public String Other_Requisition_Detail_ID
    {
        get
        {
            return m_Other_Requisition_Detail_ID;
        }
        set
        {
            m_Other_Requisition_Detail_ID = value;
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
    public Nullable<Int16> Old_Qty
    {
        get
        {
            return m_Old_Qty;
        }
        set
        {
            m_Old_Qty = value;
        }
    }
}


