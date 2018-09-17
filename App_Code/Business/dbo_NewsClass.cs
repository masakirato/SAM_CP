using System;
using System.IO;
public class dbo_NewsClass
{
    private String m_News_ID;
    private String m_Subject;
    private String m_Content;
    private Nullable<DateTime> m_Start_Date;
    private Nullable<DateTime> m_End_Date;
    private String m_NewsType;
    private String m_CV_Code;
    private String m_Status;
    private Byte[] m_Photo;
    private MemoryStream m_Photo_MemoryStream;

    public dbo_NewsClass() { }


    public MemoryStream Photo_MemoryStream
    {
        get
        {
            return m_Photo_MemoryStream;
        }
        set
        {
            m_Photo_MemoryStream = value;
        }
    }


    public String News_ID
    {
        get
        {
            return m_News_ID;
        }
        set
        {
            m_News_ID = value;
        }
    }

    public String Subject
    {
        get
        {
            return m_Subject;
        }
        set
        {
            m_Subject = value;
        }
    }

    public String Content
    {
        get
        {
            return m_Content;
        }
        set
        {
            m_Content = value;
        }
    }

    public Nullable<DateTime> Start_Date
    {
        get
        {
            return m_Start_Date;
        }
        set
        {
            m_Start_Date = value;
        }
    }

    public Nullable<DateTime> End_Date
    {
        get
        {
            return m_End_Date;
        }
        set
        {
            m_End_Date = value;
        }
    }

    public String NewsType
    {
        get
        {
            return m_NewsType;
        }
        set
        {
            m_NewsType = value;
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

    public Byte[] Photo
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

}


