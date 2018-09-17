using System;
public class dbo_StockMovementClass
{
    private String m_Stock_Movement_ID;
    private String m_CV_CODE;
    private Nullable<DateTime> m_Date;
    private String m_Product_List_ID;
    private String m_Movement_Type;
    private Nullable<Int16> m_Qty;
    private Nullable<DateTime> m_Created_Date;
    private String m_Created_By;
    private Nullable<DateTime> m_Last_Modified_Date;
    private String m_Last_Modified_By;
    private String m_Ref_No;

    public dbo_StockMovementClass() { }

    public String Stock_Movement_ID
    {
        get
        {
            return m_Stock_Movement_ID;
        }
        set
        {
            m_Stock_Movement_ID = value;
        }
    }
    public String CV_CODE
    {
        get
        {
            return m_CV_CODE;
        }
        set
        {
            m_CV_CODE = value;
        }
    }

    public Nullable<DateTime> Date
    {
        get
        {
            return m_Date;
        }
        set
        {
            m_Date = value;
        }
    }

    public String Product_List_ID
    {
        get
        {
            return m_Product_List_ID;
        }
        set
        {
            m_Product_List_ID = value;
        }
    }

    public String Movement_Type
    {
        get
        {
            return m_Movement_Type;
        }
        set
        {
            m_Movement_Type = value;
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

    public String Ref_No
    {
        get
        {
            return m_Ref_No;
        }
        set
        {
            m_Ref_No = value;
        }
    }

    //public Nullable<DateTime> Created_Date
    //{
    //    get
    //    {
    //        return m_Created_Date;
    //    }
    //    set
    //    {
    //        m_Created_Date = value;
    //    }
    //}

    //public String Created_By
    //{
    //    get
    //    {
    //        return m_Created_By;
    //    }
    //    set
    //    {
    //        m_Created_By = value;
    //    }
    //}

    //public Nullable<DateTime> Last_Modified_Date
    //{
    //    get
    //    {
    //        return m_Last_Modified_Date;
    //    }
    //    set
    //    {
    //        m_Last_Modified_Date = value;
    //    }
    //}

    //public String Last_Modified_By
    //{
    //    get
    //    {
    //        return m_Last_Modified_By;
    //    }
    //    set
    //    {
    //        m_Last_Modified_By = value;
    //    }
    //}

}


