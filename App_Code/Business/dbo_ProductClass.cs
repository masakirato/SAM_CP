using System;
public class dbo_ProductClass
{
    private String m_EAN;
    private String m_Product_ID;
    private String m_Product_Name;
    private Nullable<Byte> m_Point;
    private Nullable<Int16> m_Size;
    private String m_Unit_of_item_ID;
    private String m_Product_group_ID;
    private Nullable<Decimal> m_CP_Meiji_Price;
    private Nullable<Int16> m_Order_No;
    private Nullable<Byte> m_Quantity_in__carte;
    private Nullable<Int16> m_Packing_Size;
    private String m_Status;
    private Nullable<Int32> m_Quantity;
    private String m_SAP_Product_Code;
    private Nullable<Decimal> m_SP_Price;
    private Nullable<Decimal> m_Agent_Price;
    private Nullable<Int16> m_Qty;
    private String m_Billing_Detail_ID;
    private String m_Billing_ID;
    private Nullable<Decimal> m_Net_Value;
    private Nullable<Byte> m_Vat;
    private Nullable<Decimal> m_Total;
    private Nullable<Int16> m_Stock;
    private Nullable<Int16> m_Total_Qty;
    private Nullable<Int16> m_Count_Quantity;
    private Nullable<Int16> m_Diff_Quantity;
    private String m_Remark;
    private String m_Count_Stock_Detail_ID;
    private String m_Stock_on_Hand_ID;
    private Nullable<Int16> m_Return_Qty;
    private Nullable<Int16> m_Deposit_Qty;
    private Nullable<Int16> m_Sales_Qty;
    private Nullable<Decimal> m_Sales_Amount;
    private Nullable<Int16> m_Sub_Total_Qty;
    private Nullable<Int16> m_Suggestion_Qty;
    private Nullable<Int16> m_Previous_Balance_Qty;
    private String m_Deposit_Detail_ID;

    private String m_Other_Requisition_Detail_ID;
    private String m_Requisition_Detail_ID;



    private Nullable<DateTime> m_Start_Effective_Date;

    private Nullable<DateTime> m_End_Effective_Date;

    private String m_base64_Photo;

    private byte[] m_Photo;

    private Nullable<Int16> m_Stock_END;


    public Nullable<Int16> Stock_END
    {
        get
        {
            return m_Stock_END;
        }
        set
        {
            m_Stock_END = value;
        }
    }



    public String base64_Photo
    {
        get
        {
            if (m_Photo != null)
            {
                return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(m_Photo, 0, m_Photo.Length));
            }
            else
            {
                return string.Empty;
            }
        }
        //set
        //{
        //    m_Count_Stock_Detail_ID = value;
        //}
    }

    public bool IconVisible
    {
        get
        {
            if (!m_Start_Effective_Date.HasValue)
            {
                return false;
            }
            else
            {
                return ((DateTime.Now.Date - m_Start_Effective_Date.Value.Date).TotalDays < 30);
            }

        }
    }

    public Nullable<DateTime> Start_Effective_Date
    {
        get
        {
            return m_Start_Effective_Date;
        }
        set
        {
            m_Start_Effective_Date = value;
        }
    }

    public Nullable<DateTime> End_Effective_Date
    {
        get
        {
            return m_End_Effective_Date;
        }
        set
        {
            m_End_Effective_Date = value;
        }
    }

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

    public String Requisition_Detail_ID
    {
        get
        {
            return m_Requisition_Detail_ID;
        }
        set
        {
            m_Requisition_Detail_ID = value;
        }
    }

    public byte[] Photo
    {
        get
        {
            return m_Photo;
        }
        set
        {
            m_Photo = value;
        }
    }
    public Nullable<Int16> Return_Qty
    {
        get
        {
            return m_Return_Qty;
        }
        set
        {
            m_Return_Qty = value;
        }
    }
    public Nullable<Int16> Sub_Total_Qty
    {
        get
        {
            //if (m_Total_Qty == null) return 0;
            //return m_Total_Qty;
            return m_Sub_Total_Qty;
        }
        set
        {
            m_Sub_Total_Qty = value;
        }
    }
    public String Deposit_Detail_ID
    {
        get
        {
            return m_Deposit_Detail_ID;
        }
        set
        {
            m_Deposit_Detail_ID = value;
        }
    }
    public Nullable<Int16> Previous_Balance_Qty
    {
        get
        {
            return m_Previous_Balance_Qty;
        }
        set
        {
            m_Previous_Balance_Qty = value;
        }
    }
    public Nullable<Int16> Sales_Qty
    {
        get
        {
            return m_Sales_Qty;
        }
        set
        {
            m_Sales_Qty = value;
        }
    }

    public Nullable<Decimal> Sales_Amount
    {
        get
        {
            return m_Sales_Amount;
        }
        set
        {
            m_Sales_Amount = value;
        }
    }

    public Nullable<Int16> Suggestion_Qty
    {
        get
        {
            return m_Suggestion_Qty;
        }
        set
        {
            m_Suggestion_Qty = value;
        }
    }
    public Nullable<Int16> Deposit_Qty
    {
        get
        {
            return m_Deposit_Qty;
        }
        set
        {
            m_Deposit_Qty = value;
        }
    }

    public String Stock_on_Hand_ID
    {
        get
        {
            return m_Stock_on_Hand_ID;
        }
        set
        {
            m_Stock_on_Hand_ID = value;
        }
    }

    public String Count_Stock_Detail_ID
    {
        get
        {
            return m_Count_Stock_Detail_ID;
        }
        set
        {
            m_Count_Stock_Detail_ID = value;
        }
    }

    public String Remark
    {
        get
        {
            return m_Remark;
        }
        set
        {
            m_Remark = value;
        }
    }


    private Nullable<Int16> m_Requisition_Qty;


    private int m_index;


    public dbo_ProductClass() { }


    public Nullable<Int16> Diff_Quantity
    {
        get
        {
            return m_Diff_Quantity ;
        }
        set
        {
            m_Diff_Quantity = value;
        }
    }



    public Nullable<Int16> Count_Quantity
    {
        get
        {
            return m_Count_Quantity;
        }
        set
        {
            m_Count_Quantity = value;
        }
    }






    public Nullable<Int16> Stock
    {
        get
        {
            return m_Stock;
        }
        set
        {
            m_Stock = value;
        }
    }


    public Nullable<Decimal> SP_Price
    {
        get
        {
            return m_SP_Price;
        }
        set
        {
            m_SP_Price = value;
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

    public Nullable<Int32> Quantity
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


    public String SAP_Product_Code
    {
        get
        {
            return m_SAP_Product_Code;
        }
        set
        {
            m_SAP_Product_Code = value;
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

    public Nullable<Int16> Size
    {
        get
        {
            return m_Size;
        }
        set
        {
            m_Size = value;
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

    public String Product_group_ID
    {
        get
        {
            //m_CV_Code == null ? string.Empty : m_CV_Code;
            return m_Product_group_ID == null ? string.Empty : m_Product_group_ID;
        }
        set
        {
            m_Product_group_ID = value;
        }
    }

    public String EAN
    {
        get
        {
            return m_EAN;
        }
        set
        {
            m_EAN = value;
        }
    }

    public Nullable<Decimal> CP_Meiji_Price
    {
        get
        {
            return m_CP_Meiji_Price;
        }
        set
        {
            m_CP_Meiji_Price = value;
        }
    }

    public Nullable<Byte> Point
    {
        get
        {
            return m_Point;
        }
        set
        {
            m_Point = value;
        }
    }

    //public Nullable<Boolean> Exclude_Vat
    //{
    //    get
    //    {
    //        return m_Exclude_Vat;
    //    }
    //    set
    //    {
    //        m_Exclude_Vat = value;
    //    }
    //}

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

    public Nullable<Int16> Order_No
    {
        get
        {
            return m_Order_No;
        }
        set
        {
            m_Order_No = value;
        }
    }

    public Nullable<Byte> Quantity_in__carte
    {
        get
        {
            return m_Quantity_in__carte;
        }
        set
        {
            m_Quantity_in__carte = value;
        }
    }

    public Nullable<Int16> Packing_Size
    {
        get
        {
            return m_Packing_Size;
        }
        set
        {
            m_Packing_Size = value;
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

    //public Nullable<Decimal> Vat
    //{
    //    get
    //    {
    //        return m_Vat;
    //    }
    //    set
    //    {
    //        m_Vat = value;
    //    }
    //}

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

    public Nullable<Int16> Total_Qty
    {
        get
        {
            //if (m_Deposit_Qty == null) m_Deposit_Qty = 0;
            //if (m_Sub_Total_Qty == null) m_Sub_Total_Qty = 0;

            //if (m_Total_Qty == null)
            //{
            // m_Total_Qty = Int16.Parse((m_Deposit_Qty + Sub_Total_Qty).ToString());
            //}




            return m_Total_Qty;
        }
        set
        {
            m_Total_Qty = value;
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

}


