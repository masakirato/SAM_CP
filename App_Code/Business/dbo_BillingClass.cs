using System;
public class dbo_BillingClass
{
    private String m_Billing_ID;
    private String m_Billing_Type;
    private String m_Billing_Type_tmp;
    private String m_Order_Type;
    private String m_CV_Number;
    private String m_Invoice_No;
    private Nullable<DateTime> m_Invoice_Date;
    private String m_PO_No;
    private Nullable<DateTime> m_PO_Date;
    private Nullable<Decimal> m_Net_Value;
    private Nullable<Decimal> m_Vat;
    private Nullable<Decimal> m_Total;
    private String m_Ref_Invoice_No;
    private String m_Invoice_Status;
    private Nullable<DateTime> m_Created_Date;
    private String m_Created_By;
    private Nullable<DateTime> m_Last_Modified_Date;
    private String m_Last_Modified_By;
    private String m_Order_Status;
    private String m_Billing_Type_Name;



    public dbo_BillingClass() { }

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
    public String Billing_Type_Name
    {
        get
        {
            return m_Billing_Type_Name;
        }
        set
        {
            m_Billing_Type_Name = value;
        }
    }

    public String Order_Status
    {
        get
        {
            //m_Order_Status = (dbo_OrderingDataClass.Select_Record(m_PO_No) == null ? string.Empty : dbo_OrderingDataClass.Select_Record(m_PO_No).Order_Status);

            return m_Order_Status;
        }
        set
        {
            m_Order_Status = value;
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


    public String Billing_Type_tmp
    {
        get
        {
            if (Billing_Type == "ZDOM" || Billing_Type == "YDOM")
            {
                m_Billing_Type_tmp = "ใบแจ้งหนี้";
            }
            else if (Billing_Type == "ZDCN")
            {

                m_Billing_Type_tmp = "ใบลดหนี้";
            }
            else if (Billing_Type == "ZDDN")
            {
                m_Billing_Type_tmp = "ใบเพิ่มหนี้";
            }

            return m_Billing_Type_tmp;
        }
        set
        {
            m_Billing_Type_tmp = value;
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

    public String CV_Number
    {
        get
        {
            return m_CV_Number;
        }
        set
        {
            m_CV_Number = value;
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

    public String Created_By
    {
        get
        {
            return m_Created_By;
        }
        set
        {
            m_Created_By = value;
        }
    }

    public Nullable<DateTime> Last_Modified_Date
    {
        get
        {
            return m_Last_Modified_Date;
        }
        set
        {
            m_Last_Modified_Date = value;
        }
    }

    public String Last_Modified_By
    {
        get
        {
            return m_Last_Modified_By;
        }
        set
        {
            m_Last_Modified_By = value;
        }
    }

}


