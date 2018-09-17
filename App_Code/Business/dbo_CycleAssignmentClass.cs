using System;
public class dbo_CycleAssignmentClass
{
    private String m_Order_Cycle_ID;
    private String m_CV_Code;
    private String m_AgentName;


    public dbo_CycleAssignmentClass() { }

    public String Order_Cycle_ID
    {
        get
        {
            return m_Order_Cycle_ID;
        }
        set
        {
            m_Order_Cycle_ID = value;
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


    public String AgentName
    {
        get
        {
            return m_AgentName;
        }
        set
        {
            m_AgentName = value;
        }
    }
}


