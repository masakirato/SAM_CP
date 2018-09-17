using System;
public class dbo_RequisitionClearingClass
{
    private String m_Clearing_No;

    private String m_Requisition_No;
    private String m_Time_No;
    private String m_User_ID;
    private Nullable<DateTime> m_Requisition_Date;
    private String m_Status;
    private String m_SP_Name;

    private Nullable<DateTime> m_Clearing_Date;
    private Nullable<DateTime> m_Latest_Clearing_Date;

    public dbo_RequisitionClearingClass() { }

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
            if (m_User_ID == null)
            {
                m_User_ID = dbo_RequisitionDataClass.Select_Record(m_Requisition_No).User_ID;
            }


            m_SP_Name = dbo_UserDataClass.Select_Record(m_User_ID).FullName;

            return m_SP_Name;
        }
        set
        {
            m_SP_Name = value;
        }
    }

    public String Time_No
    {
        get
        {
            return m_Time_No;
        }
        set
        {
            m_Time_No = value;
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


    public Nullable<DateTime> Latest_Clearing_Date
    {
        get
        {
            return m_Latest_Clearing_Date;
        }
        set
        {
            m_Latest_Clearing_Date = value;
        }
    }

    public Nullable<DateTime> Requisition_Date
    {
        get
        {
            return string.IsNullOrEmpty(m_Requisition_No) ? (DateTime?)null : dbo_RequisitionDataClass.Select_Record(this.m_Requisition_No).Requisition_Date;


            //  return ;
        }
        set
        {
            m_Requisition_Date = value;
        }
    }

    public Nullable<DateTime> Clearing_Date
    {
        get
        {


            // m_Clearing_Date = dbo_ClearingDataClass.Select_Record(m_Clearing_No).Clearing_Date;

            return m_Clearing_Date;
        }
        set
        {
            m_Clearing_Date = value;
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

}


