using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Count_SP
/// </summary>
public class Count_SP
{
    private int m_sp_total;
    private int m_sp_this_month;
    private int m_sp_resign;
    private int m_customer_total;

    public int sp_total
    {
        get
        {
            return m_sp_total;
        }
        set
        {
            m_sp_total = value;
        }
    }

    public int sp_this_month
    {
        get
        {
            return m_sp_this_month;
        }
        set
        {
            m_sp_this_month = value;
        }
    }

    public int sp_resign
    {
        get
        {
            return m_sp_resign;
        }
        set
        {
            m_sp_resign = value;
        }
    }

    public int sp_customer_total
    {
        get
        {
            return m_customer_total;
        }
        set
        {
            m_customer_total = value;
        }
    }
}