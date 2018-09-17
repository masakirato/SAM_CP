using System;
public class dbo_CommissionRequisitionDtlClass
{
    private String m_Commission_requisition_no;
    private String m_Clearing_No;
    private Nullable<Decimal> m_Commission;
    private Nullable<Decimal> m_Requisition_Amount;

    public dbo_CommissionRequisitionDtlClass() { }

    public String Commission_requisition_no
    {
        get
        {
            return m_Commission_requisition_no;
        }
        set
        {
            m_Commission_requisition_no = value;
        }
    }

    public String Clearing_No
    {
        get
        {
            return m_Clearing_No;
        }
        set
        {
            m_Clearing_No = value;
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

    public Nullable<Decimal> Requisition_Amount
    {
        get
        {
            return m_Requisition_Amount;
        }
        set
        {
            m_Requisition_Amount = value;
        }
    }

}


