using System;
public class dbo_ReceiveClass
{
    private String m_Receive_ID;
    private String m_Billing_ID;
    private String m_Invoice_No;
    private Nullable<DateTime> m_Invoice_Date;
    private Nullable<Decimal> m_Invoice_Net_Value;
    private Nullable<Decimal> m_Invoice_VAT;
    private Nullable<Decimal> m_Invoice_Total;
    private String m_Ref_Invoice_No;

    private String m_Order_Status;
    private String m_Invoice_Status;

    public dbo_ReceiveClass() { }

    public String Receive_ID
    {
        get
        {
            return m_Receive_ID;
        }
        set
        {
            m_Receive_ID = value;
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

    public String Invoice_No
    {
        get
        {
            return m_Invoice_No;
        }
        set
        {
            m_Invoice_No = value;
        }
    }

    public Nullable<DateTime> Invoice_Date
    {
        get
        {
            return m_Invoice_Date;
        }
        set
        {
            m_Invoice_Date = value;
        }
    }

    public Nullable<Decimal> Invoice_Net_Value
    {
        get
        {
            return m_Invoice_Net_Value;
        }
        set
        {
            m_Invoice_Net_Value = value;
        }
    }

    public Nullable<Decimal> Invoice_VAT
    {
        get
        {
            return m_Invoice_VAT;
        }
        set
        {
            m_Invoice_VAT = value;
        }
    }

    public Nullable<Decimal> Invoice_Total
    {
        get
        {
            return m_Invoice_Total;
        }
        set
        {
            m_Invoice_Total = value;
        }
    }

    public String Ref_Invoice_No
    {
        get
        {
            return m_Ref_Invoice_No;
        }
        set
        {
            m_Ref_Invoice_No = value;
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

    public String Invoice_Status
    {
        get
        {
            return m_Invoice_Status;
        }
        set
        {
            m_Invoice_Status = value;
        }
    }

}


