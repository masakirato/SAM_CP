using System;
using System.IO;
public class dbo_NewsClass2
{
    private String m_News_ID;
    private String m_News_Type;
    private String m_Agent_Name;
    private String m_Subject;
    private String m_Content;
    private String m_Content_FileName;
    private String m_Content_FileType;
    private String m_VDO_Hyperlink;
    private Nullable<DateTime> m_Start_Date;
    private Nullable<DateTime> m_End_Date;
    private String m_Photo_Name;
    private byte[] m_Photo_MemoryStream;

    private Byte[] m_Content_File;


    public byte[] Content_File
    {
        get
        {
            return m_Content_File;
        }
        set
        {
            m_Content_File = value;
        }
    }


    public dbo_NewsClass2() { }


    public byte[] Photo_MemoryStream
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

    public String News_Type
    {
        get
        {
            return m_News_Type;
        }
        set
        {
            m_News_Type = value;
        }
    }

    public String Agent_Name
    {
        get
        {
            return m_Agent_Name;
        }
        set
        {
            m_Agent_Name = value;
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

    public String Content_FileName
    {
        get
        {
            return m_Content_FileName;
        }
        set
        {
            m_Content_FileName = value;
        }
    }

    public String Content_FileType
    {
        get
        {
            return m_Content_FileType;
        }
        set
        {
            m_Content_FileType = value;
        }
    }

    public String VDO_Hyperlink
    {
        get
        {
            return m_VDO_Hyperlink;
        }
        set
        {
            m_VDO_Hyperlink = value;
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

    public String Photo_Name
    {
        get
        {
            return m_Photo_Name;
        }
        set
        {
            m_Photo_Name = value;
        }
    }

}


