using System;

public class OrderingList
{
    private String m_PO_Number;
    private Nullable<Decimal> m_Total_amount_after_vat_included;
    private Nullable<DateTime> m_Date_of_create_order_or_PO_Date;
    private Nullable<DateTime> m_Date_of_delivery_goods;
    private String m_Order_Status;
    private String m_First_Name;


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

    public Nullable<Decimal> Total_amount_after_vat_included
    {
        get
        {
            return m_Total_amount_after_vat_included;
        }
        set
        {
            m_Total_amount_after_vat_included = value;
        }
    }

    public Nullable<DateTime> Date_of_create_order_or_PO_Date
    {
        get
        {
            return m_Date_of_create_order_or_PO_Date;
        }
        set
        {
            m_Date_of_create_order_or_PO_Date = value;
        }
    }

    public Nullable<DateTime> Date_of_delivery_goods
    {
        get
        {
            return m_Date_of_delivery_goods;
        }
        set
        {
            m_Date_of_delivery_goods = value;
        }
    }

    public String Order_Status
    {
        get
        {
            return m_Order_Status;
        }
        set
        {
            m_Order_Status = value;
        }
    }

    public String First_Name
    {
        get
        {
            return m_First_Name;
        }
        set
        {
            m_First_Name = value;
        }
    }


}