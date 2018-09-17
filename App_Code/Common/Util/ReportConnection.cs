using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

public static class ReportConnection
{
    private static String m_Report_DB_Name;
    private static String m_Report_DB_UID;
    private static String m_Report_DB_PWD;
    private static String m_Report_DB_ID;

    public static String Report_DB_Name
    {
        get
        {
            m_Report_DB_Name = ConfigurationSettings.AppSettings["DBNAME"];
            return m_Report_DB_Name;
        }
        set
        {
            m_Report_DB_Name = value;
        }
    }

    public static String Report_DB_UID
    {
        get
        {
            m_Report_DB_UID = ConfigurationSettings.AppSettings["DBUID"];
            return m_Report_DB_UID;
        }
        set
        {
            m_Report_DB_UID = value;
        }
    }

    public static String Report_DB_PWD
    {
        get
        {
            m_Report_DB_PWD = ConfigurationSettings.AppSettings["DBPWD"];
            return m_Report_DB_PWD;
        }
        set
        {
            m_Report_DB_PWD = value;
        }
    }

    public static String Report_DB_ID
    {
        get
        {
            m_Report_DB_ID = ConfigurationSettings.AppSettings["DBIP"];
            return m_Report_DB_ID;
        }
        set
        {
            m_Report_DB_ID = value;
        }
    }
}