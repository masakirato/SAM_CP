using System;
public class dbo_ProductListClass
{
    private String m_Product_List_ID;
    private String m_Price_Group_ID;
    private String m_Product_ID;
    private String m_Product_Name;
    private Nullable<Decimal> m_CP_Meiji_Price;
    private Nullable<Decimal> m_Price;
    private Nullable<Int16> m_Point;


    // private Nullable<Boolean> m_Exclude_Vat;


    private Nullable<Int16> m_Vat;

    private Nullable<DateTime> m_Start_Effective_Date;
    private Nullable<DateTime> m_End_Effective_Date;

    private Nullable<Decimal> m_SP_Price;
    private Nullable<Decimal> m_Agent_Price;
    private Nullable<DateTime> m_Product_Effective_Date;


    
    private String m_Price_Group_Name;
    private int m_datediff;
    private String m_CV_CODE;
    private String m_Price_Group_Type;
    private Nullable<Boolean> m_StandardPrice;
    private String m_Unit_of_Item;
    private Nullable<Int16> m_Size;

    public dbo_ProductListClass() { }

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

    public String Price_Group_ID
    {
        get
        {
            return m_Price_Group_ID;
        }
        set
        {
            m_Price_Group_ID = value;
        }
    }



    public int datediff
    {
        get
        {
            return m_datediff;
        }
        set
        {
            m_datediff = value;
        }
    }

    public String Price_Group_Name
    {
        get
        {
            return m_Price_Group_Name;
        }
        set
        {
            m_Price_Group_Name = value;
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

    public Nullable<Int16> Point
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

    public Nullable<Int16> Vat
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

    public Nullable<DateTime> Product_Effective_Date
    {
        get
        {
            return m_Product_Effective_Date;
        }
        set
        {
            m_Product_Effective_Date = value;
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
    public String Price_Group_Type
    {
        get
        {
            return m_Price_Group_Type;
        }
        set
        {
            m_Price_Group_Type = value;
        }
    }
    public Nullable<Boolean> StandardPrice
    {
        get
        {
            return m_StandardPrice;
        }
        set
        {
            m_StandardPrice = value;
        }
    }
    public String Unit_of_Item
    {
        get
        {
            return m_Unit_of_Item;
        }
        set
        {
            m_Unit_of_Item = value;
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
}



