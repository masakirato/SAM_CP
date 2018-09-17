using System;
public class dbo_PriceGroupClass
{
    private String m_Price_Group_ID;
    private String m_Price_Group_Name;
    private String m_Price_Group_Type;
    private Nullable<Boolean> m_StandardPrice;
    private String m_CV_Code;

    public dbo_PriceGroupClass() { }


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


}


