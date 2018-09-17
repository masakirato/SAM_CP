using System;
public class dbo_ClearingClass
{
    private String m_Clearing_No;
    private Nullable<DateTime> m_Clearing_Date;
    private Nullable<DateTime> m_Commission_requisition_date;
    private String m_User_ID;
    private Nullable<Decimal> m_Cash_Payment;
    private Nullable<Decimal> m_Actual_Payment;
    private Nullable<Decimal> m_Balance_Outstanding;
    private Nullable<Int16> m_Net_Sales_Qty;
    private Nullable<Decimal> m_Net_Sales_Amount;
    private Nullable<Decimal> m_Credit_Amount;
    private Nullable<Decimal> m_SP_Cash;
    private Nullable<Decimal> m_Cash_Payment_Amount;
    private Nullable<Decimal> m_Cheque_Payment_Amount;
    private Nullable<Decimal> m_Transfer_Payment_Amount;
    private Nullable<Decimal> m_Sub_Total;
    private Nullable<Decimal> m_Total;
    private Nullable<Decimal> m_Discount;
    private Nullable<Int16> m_Today_Points;
    private Nullable<Decimal> m_Today_Commission;
    private Nullable<Decimal> m_This_Month_Sales_Amount;
    private Nullable<Decimal> m_Today_Deposit_Amount;
    private Nullable<Decimal> m_Total_Credit_Amount;
    private Nullable<Decimal> m_Total_Balance_Outstanding;
    private Nullable<Decimal> m_Total_Commission;
    private Nullable<Decimal> m_This_Month_Points;
    private Nullable<Decimal> m_Total_Deposit;


    private String m_Commission_Requisition_Status;
    private Nullable<Decimal> m_Commission;

    private String m_Requisition_No;


    private Nullable<Decimal> m_Net_Total;
    private Nullable<Decimal> m_Debt_Payment;
    private Nullable<Decimal> m_Debt_Balance;
    private Nullable<Decimal> m_Today_Return_Amount;

    private String m_Status;
    private Nullable<Decimal> m_Debt_Total;

    private Nullable<Decimal> m_Requisition_Amount;
    private Nullable<Decimal> m_Commission_Balance_Outstanding;


    public Nullable<Decimal> Commission_Balance_Outstanding
    {
        get
        {
            return m_Commission_Balance_Outstanding;
        }
        set
        {
            m_Commission_Balance_Outstanding = value;
        }
    }

    public Nullable<Decimal> Net_Total
    {
        get
        {
            return m_Net_Total;
        }
        set
        {
            m_Net_Total = value;
        }
    }

    public Nullable<Decimal> Debt_Total
    {
        get
        {
            return m_Debt_Total;
        }
        set
        {
            m_Debt_Total = value;
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

    public Nullable<Decimal> Debt_Payment
    {
        get
        {
            return m_Debt_Payment;
        }
        set
        {
            m_Debt_Payment = value;
        }
    }



    public Nullable<Decimal> Debt_Balance
    {
        get
        {
            return m_Debt_Balance;
        }
        set
        {
            m_Debt_Balance = value;
        }
    }



    public Nullable<Decimal> Today_Return_Amount
    {
        get
        {
            return m_Today_Return_Amount;
        }
        set
        {
            m_Today_Return_Amount = value;
        }
    }



    public dbo_ClearingClass() { }

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



    public String SP_Name
    {
        get
        {
            if (!string.IsNullOrEmpty(m_User_ID))
            {
                return dbo_UserDataClass.Select_Record(m_User_ID).FullName;
            }
            return string.Empty;
        }
    }

    public String Status
    {
        get
        {
            return m_Status;
        }
        set
        {
            m_Status = value;
        }
    }

    public String Commission_Requisition_Status
    {
        get
        {
            return m_Commission_Requisition_Status;
        }
        set
        {
            m_Commission_Requisition_Status = value;
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

    public Nullable<DateTime> Clearing_Date
    {
        get
        {
            return m_Clearing_Date;
        }
        set
        {
            m_Clearing_Date = value;
        }
    }

    public Nullable<DateTime> Commission_requisition_date
    {
        get
        {
            return m_Commission_requisition_date;
        }
        set
        {
            m_Commission_requisition_date = value;
        }
    }


    public String User_ID
    {
        get
        {
            return m_User_ID;
        }
        set
        {
            m_User_ID = value;
        }
    }

    public Nullable<Decimal> Cash_Payment
    {
        get
        {
            return m_Cash_Payment;
        }
        set
        {
            m_Cash_Payment = value;
        }
    }

    public Nullable<Decimal> Actual_Payment
    {
        get
        {
            return m_Actual_Payment;
        }
        set
        {
            m_Actual_Payment = value;
        }
    }

    public Nullable<Decimal> Balance_Outstanding
    {
        get
        {
            return m_Balance_Outstanding;
        }
        set
        {
            m_Balance_Outstanding = value;
        }
    }

    public Nullable<Int16> Net_Sales_Qty
    {
        get
        {
            return m_Net_Sales_Qty;
        }
        set
        {
            m_Net_Sales_Qty = value;
        }
    }

    public Nullable<Decimal> Net_Sales_Amount
    {
        get
        {
            return m_Net_Sales_Amount;
        }
        set
        {
            m_Net_Sales_Amount = value;
        }
    }

    public Nullable<Decimal> Credit_Amount
    {
        get
        {
            return m_Credit_Amount;
        }
        set
        {
            m_Credit_Amount = value;
        }
    }

    public Nullable<Decimal> SP_Cash
    {
        get
        {
            return m_SP_Cash;
        }
        set
        {
            m_SP_Cash = value;
        }
    }

    public Nullable<Decimal> Cash_Payment_Amount
    {
        get
        {
            return m_Cash_Payment_Amount;
        }
        set
        {
            m_Cash_Payment_Amount = value;
        }
    }

    public Nullable<Decimal> Cheque_Payment_Amount
    {
        get
        {
            return m_Cheque_Payment_Amount;
        }
        set
        {
            m_Cheque_Payment_Amount = value;
        }
    }

    public Nullable<Decimal> Transfer_Payment_Amount
    {
        get
        {
            return m_Transfer_Payment_Amount;
        }
        set
        {
            m_Transfer_Payment_Amount = value;
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

    public Nullable<Decimal> Discount
    {
        get
        {
            return m_Discount;
        }
        set
        {
            m_Discount = value;
        }
    }

    public Nullable<Int16> Today_Points
    {
        get
        {
            return m_Today_Points;
        }
        set
        {
            m_Today_Points = value;
        }
    }

    public Nullable<Decimal> Today_Commission
    {
        get
        {
            return m_Today_Commission;
        }
        set
        {
            m_Today_Commission = value;
        }
    }

    public Nullable<Decimal> This_Month_Sales_Amount
    {
        get
        {
            return m_This_Month_Sales_Amount;
        }
        set
        {
            m_This_Month_Sales_Amount = value;
        }
    }

    public Nullable<Decimal> Today_Deposit_Amount
    {
        get
        {
            return m_Today_Deposit_Amount;
        }
        set
        {
            m_Today_Deposit_Amount = value;
        }
    }

    public Nullable<Decimal> Total_Credit_Amount
    {
        get
        {
            return m_Total_Credit_Amount;
        }
        set
        {
            m_Total_Credit_Amount = value;
        }
    }

    public Nullable<Decimal> Total_Balance_Outstanding
    {
        get
        {
            return m_Total_Balance_Outstanding;
        }
        set
        {
            m_Total_Balance_Outstanding = value;
        }
    }

    public Nullable<Decimal> Total_Commission
    {
        get
        {
            return m_Total_Commission;
        }
        set
        {
            m_Total_Commission = value;
        }
    }

    public Nullable<Decimal> This_Month_Points
    {
        get
        {
            return m_This_Month_Points;
        }
        set
        {
            m_This_Month_Points = value;
        }
    }

    public Nullable<Decimal> Total_Deposit
    {
        get
        {
            return m_Total_Deposit;
        }
        set
        {
            m_Total_Deposit = value;
        }
    }

}


