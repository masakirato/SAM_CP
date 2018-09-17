using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ReportWithdrawalCommissions : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetUpDrowDownList();
        }

    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            if (user_class.User_Group_ID == "CP Meiji")
            {

                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                //ddlAgentName
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] agent_tmp = agent.Where(f => myArr.Contains(f.Location_Region) && f.Status == true).OrderBy(f => f.AgentName).Select(f => f.CV_Code).ToArray();

                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);
                //users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์").OrderBy(f => f.AgentName);

                List<dbo_UserClass> user2 = new List<dbo_UserClass>(users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && (f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์")).OrderBy(f => f.AgentName).Select(f => f));

                ddl_SPName.DataSource = user2.Where(f => agent_tmp.Contains(f.CV_CODE)).OrderBy(f => f.FullName_ddl);
                ddl_SPName.DataBind();
                ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");

            }
            else
            {

                //ddl_SPName
                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                  , string.Empty, string.Empty, string.Empty, user_class.CV_CODE.Trim(), null, string.Empty, string.Empty);

                //users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && (f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์")).OrderBy(f => f.AgentName);
                List<dbo_UserClass> user2 = new List<dbo_UserClass>(users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && (f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์")).OrderBy(f => f.AgentName).Select(f => f));

                ddl_SPName.DataSource = user2.OrderBy(f => f.FullName_ddl);
                ddl_SPName.DataBind();
                ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");

            }

            txt_Clearing_Date_From.Text = DateTime.Now.ToShortDateString();
            txt_Clearing_Date_TO.Text = DateTime.Now.ToShortDateString();
            txt_Requisition_Date_From.Text = DateTime.Now.ToShortDateString();
            txt_Requisition_Date_To.Text = DateTime.Now.ToShortDateString();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {

        try
        {

            if (IsValidForm())
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(500);

                string User_ID = Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                string AgentName = user_class.CV_CODE;
                string SPName = ddl_SPName.SelectedValue;

                string Lebel_SP = ddl_SPName.SelectedItem.Text;
                if (Lebel_SP == "เลือกทั้งหมด")
                {
                    Lebel_SP = string.Empty;
                }
                if (AgentName == "เลือกทั้งหมด")
                {
                    AgentName = string.Empty;
                }
                if (SPName == "เลือกทั้งหมด")
                {
                    SPName = string.Empty;
                }


                List<RPT_REQUES_COMMISSION_41217> rt_RPT_RqComm = new List<RPT_REQUES_COMMISSION_41217>();
                rt_RPT_RqComm = Reports.RPT_REQUES_COMMISSION_41217(string.Empty, AgentName, SPName, txt_Clearing_Date_From.Text, txt_Clearing_Date_TO.Text, txt_Requisition_Date_From.Text, txt_Requisition_Date_To.Text, Lebel_SP, string.Empty, string.Empty);

                if (rt_RPT_RqComm.Count > 0)
                {

                    string url = "../Report/RT_ShowReportEmployee.aspx?RPT=ReportWithdrawalCommissions&SPName=" + ddl_SPName.SelectedValue
                    + "&Clearing_Date_From=" + txt_Clearing_Date_From.Text
                    + "&Clearing_Date_TO=" + txt_Clearing_Date_TO.Text
                    + "&RequisitionDate_From=" + txt_Requisition_Date_From.Text
                    + "&RequisitionDate_To=" + txt_Requisition_Date_To.Text
                    + "&AgentName=" + user_class.CV_CODE
                    + "&Lebel_SP=" + ddl_SPName.SelectedItem;

                    string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
                }
                else
                {
                    Messages.Show("ไม่พบข้อมูล กรุณาเลือกเงื่อนไขอีกครั้ง", this.Page);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(500);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        SetUpDrowDownList();
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        System.Threading.Thread.Sleep(500);
    }

    private bool IsValidForm()
    {

        bool ret = true;

        if (this.txt_Clearing_Date_From.Text.Trim() != "" && this.txt_Clearing_Date_TO.Text.Trim() != "")
        {
            if (DateTime.Parse(this.txt_Clearing_Date_TO.Text) < DateTime.Parse(this.txt_Clearing_Date_From.Text))
            {
                Messages.Show("โปรดระบุช่วงวันที่ ให้ถูกต้อง", this.Page);
                txt_Clearing_Date_From.Text = DateTime.Now.ToShortDateString();
                txt_Clearing_Date_TO.Text = DateTime.Now.ToShortDateString();
                ret = false;
            }
        }

       if (this.txt_Requisition_Date_From.Text.Trim() != "" && this.txt_Requisition_Date_To.Text.Trim() != "")
        {
            if (DateTime.Parse(this.txt_Requisition_Date_To.Text) < DateTime.Parse(this.txt_Requisition_Date_From.Text))
            {
                Messages.Show("โปรดระบุช่วงวันที่ ให้ถูกต้อง", this.Page);
                txt_Requisition_Date_From.Text = DateTime.Now.ToShortDateString();
                txt_Requisition_Date_To.Text = DateTime.Now.ToShortDateString();

                ret = false;
            }
        }

        return ret;
    }
}