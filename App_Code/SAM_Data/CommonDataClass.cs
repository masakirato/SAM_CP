using System;
using System.Threading;

public class CommonDataClass
{

    public static bool lock_flag;

   


    private String m_SP_Name;
    private String m_Requisition_No;
    private DateTime m_Requisition_Date;
    private DateTime m_Transaction_Date;


    private static Decimal m_Invoice_Net_Value;
    private static Decimal m_Invoice_Total;
    private static String m_Billing_ID;
    private static Decimal m_current_target;
    private static String m_User_ID;
    private static String m_PO;

    public static String PO
    {
        get { return "212689PO600601"; }
    }

    public static String MailPassword
    {
        get { return "samcpmeiji1234"; }
    }

    public static String User_ID
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



    private static string m_CV_CODE;

    public static string CV_CODE
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



    public static Decimal Current_Target
    {
        get
        {
            return m_current_target;
        }
        set
        {
            m_current_target = value;
        }
    }


    public String SP_Name
    {
        get
        {
            return "ชัยวุฒิ บัวทอง";
        }
        set
        {
            m_SP_Name = value;
        }
    }

    public String Requisition_No
    {
        get
        {
            return "212689GT17051401";
        }
        set
        {
            m_SP_Name = value;
        }
    }

    public DateTime Transaction_Date
    {
        get
        {
            return DateTime.Now;
        }
    }
    public DateTime Requisition_Date
    {
        get
        {
            return DateTime.Now;
        }
    }

    public static Decimal Invoice_Net_Value
    {
        get
        {
            return m_Invoice_Net_Value;
        }
        set
        {
            m_Invoice_Net_Value = value;
        }
    }

    public static Decimal Invoice_Total
    {
        get
        {
            return m_Invoice_Total;
        }
        set
        {
            m_Invoice_Total = value;
        }
    }

    public static String Billing_ID
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
}

//public delegate void lock_expire(string user);



public class lock_user
{
   // public event lock_expire on_lock_expire;

    private string user_id;

    public lock_user(string user_id)
    {
        this.user_id = user_id;


        Thread ctThread = new Thread(unlock_user);
        ctThread.Start();



        //if (on_lock_expire != null)
        //{
        //    on_lock_expire(user_id);
        //}
    }

    private void unlock_user()
    {

        System.Threading.Thread.Sleep(60000);


        dbo_LoginHistoryClass login = new dbo_LoginHistoryClass();
        login.Status = "Invalid Password(reset)";
        login.User_ID = user_id;
        dbo_LoginHistoryDataClass.Update(login);

        login.Status = "reset";
        login.Login_Time = DateTime.Now;
        dbo_LoginHistoryDataClass.Add(login);
    }

}