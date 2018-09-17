using System;
public class dbo_CNDNClass
{
    private String m_SAM_CN_DN_No;
    private String m_CV_Code;
    private String m_SAM_CN_DN_Type;
   
    private Nullable<Int16> m_SAM_CN_DN_Quantity;
    private String m_SAM_CN_DN_Status;
    private String m_Billing_ID;
    private String m_Invoice_No;
    private String m_Billing_Type;
    private String m_Order_Type;
    private String m_PO_No;

    private Nullable<DateTime> m_Invoice_Date;
    private Nullable<DateTime> m_PO_Date;
    private Nullable<DateTime> m_SAM_CN_DN_Date;

    private Nullable<DateTime> m_Created_Date;
    private String m_CNDN_No;
    private Nullable<DateTime> m_CNDN_Date;
    private String m_CNDN_ID;

    public dbo_CNDNClass() { }

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

    public String PO_No
    {
        get
        {
            return m_PO_No;
        }
        set
        {
            m_PO_No = value;
        }
    }

    public Nullable<DateTime> PO_Date
    {
        get
        {
            return m_PO_Date;
        }
        set
        {
            m_PO_Date = value;
        }
    }

    public String CV_Code
    {
        get
        {
            return m_CV_Code;
        }
        set
        {
            m_CV_Code = value;
        }
    }

    public String SAM_CN_DN_Type
    {
        get
        {
            return m_SAM_CN_DN_Type;
        }
        set
        {
            m_SAM_CN_DN_Type = value;
        }
    }

    public Nullable<DateTime> SAM_CN_DN_Date
    {
        get
        {
            return m_SAM_CN_DN_Date;
        }
        set
        {
            m_SAM_CN_DN_Date = value;
        }
    }

    public Nullable<Int16> SAM_CN_DN_Quantity
    {
        get
        {
            return m_SAM_CN_DN_Quantity;
        }
        set
        {
            m_SAM_CN_DN_Quantity = value;
        }
    }

    public String SAM_CN_DN_Status
    {
        get
        {
            return m_SAM_CN_DN_Status;
        }
        set
        {
            m_SAM_CN_DN_Status = value;
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

    public String Billing_Type
    {
        get
        {
            return m_Billing_Type;
        }
        set
        {
            m_Billing_Type = value;
        }
    }

    public String Order_Type
    {
        get
        {
            return m_Order_Type;
        }
        set
        {
            m_Order_Type = value;
        }
    }


    public Nullable<DateTime> Created_Date
    {
        get
        {
            return m_Created_Date;
        }
        set
        {
            m_Created_Date = value;
        }
    }

    public String CNDN_No
    {
        get
        {
            return m_CNDN_No;
        }
        set
        {
            m_CNDN_No = value;
        }
    }
    public Nullable<DateTime> CNDN_Date
    {
        get
        {
            return m_CNDN_Date;
        }
        set
        {
            m_CNDN_Date = value;
        }
    }
    public String CNDN_ID
    {
        get
        {
            return m_CNDN_ID;
        }
        set
        {
            m_CNDN_ID = value;
        }
    }
}


