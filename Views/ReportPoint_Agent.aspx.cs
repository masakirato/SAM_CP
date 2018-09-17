#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_ReportPoint_Agent : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetUpDrowDownList();
        }
    }

    protected void ddlAgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            String User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            if (ddlAgentName.SelectedIndex == 0)
            {               
                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                //ddlAgentName
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] agent_tmp = agent.Where(f => myArr.Contains(f.Location_Region)).OrderBy(f => f.AgentName).Select(f => f.CV_Code).ToArray();
                String[] agent_tmp1 = agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f.CV_Code).ToArray();

                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);
                users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์").OrderBy(f => f.AgentName);

                if(agent_tmp1.Length > 0)
                {
                    ddl_SPName.DataSource = users.Where(f => agent_tmp1.Contains(f.CV_CODE)).OrderBy(f => f.FullName_ddl);
                    ddl_SPName.DataBind();
                    ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
                }
                else
                {
                    ddl_SPName.DataSource = users.Where(f => agent_tmp.Contains(f.CV_CODE)).OrderBy(f => f.FullName_ddl);
                    ddl_SPName.DataBind();
                    ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
                }
               
            }
            else
            {
                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, string.Empty, ddlAgentName.SelectedValue, null, string.Empty, string.Empty);

                users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์").OrderBy(f => f.AgentName);

                ddl_SPName.DataSource = users.OrderBy(f => f.FullName_ddl);
                ddl_SPName.DataBind();
                ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        SetUpDrowDownList();
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

            string AgentName = ddlAgentName.SelectedValue;
            string SPName = ddl_SPName.SelectedValue;
            if (AgentName == "เลือกทั้งหมด")
            {
                AgentName = string.Empty;
            }
            if (SPName == "เลือกทั้งหมด")
            {
                SPName = string.Empty;
            }

            List<RPT_AGENT_POINT_41226> rt_RPT_AGENT_POINT = new List<RPT_AGENT_POINT_41226>();
            rt_RPT_AGENT_POINT = Reports.RPT_AGENT_POINT_41226(string.Empty, AgentName, SPName, ddlMonthGroup_From.SelectedValue, ddlMonthGroup_To.SelectedValue, txt_YearGroup_From.Text, txt_YearGroup_To.Text, string.Empty, string.Empty, string.Empty);

            if (rt_RPT_AGENT_POINT.Count > 0)
            {
                string url = "../Report/RT_ShowReportPointPDF.aspx?RPT=ReportPoint_Agent&AgentName=" + ddlAgentName.SelectedValue
                + "&AgentFullName=" + ddlAgentName.SelectedItem.ToString()
                + "&SPName=" + ddl_SPName.SelectedValue
                + "&SPFullName=" + ddl_SPName.SelectedItem.ToString()
                + "&MonthGroup_From=" + ddlMonthGroup_From.SelectedValue
                + "&YearGroup_From=" + txt_YearGroup_From.Text
                + "&MonthGroup_To=" + ddlMonthGroup_To.SelectedValue
                + "&YearGroup_To=" + txt_YearGroup_To.Text;


                string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
            }
            else
            {
                Messages.Show("ไม่พบข้อมูล กรุณาเลือกเงื่อนไขอีกครั้ง", this.Page);
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion

    #region Method
    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            String User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            if (user_class.User_Group_ID == "CP Meiji")
            {
                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                //ddlAgentName
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] agent_tmp1 = agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f.CV_Code).ToArray();

                if(agent_tmp1.Length > 0)
                {
                    ddlAgentName.DataSource = agent.Where(f => agent_tmp1.Contains(f.CV_Code)).OrderBy(f => f.AgentName);
                    ddlAgentName.DataBind();
                    ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                }
                else
                {
                    ddlAgentName.DataSource = agent.Where(f => myArr.Contains(f.Location_Region)).OrderBy(f => f.AgentName);
                    ddlAgentName.DataBind();
                    ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                }
               


                if (ddlAgentName.SelectedIndex == 0)
                {
                   
                    String[] agent_tmp = agent.Where(f => myArr.Contains(f.Location_Region)).OrderBy(f => f.AgentName).Select(f => f.CV_Code).ToArray();
                    
                    List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                                    , string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);
                    users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์").OrderBy(f => f.AgentName);

                    if(agent_tmp1.Length > 0)
                    {
                        ddl_SPName.DataSource = users.Where(f => agent_tmp1.Contains(f.CV_CODE)).OrderBy(f => f.FullName_ddl);
                        ddl_SPName.DataBind();
                        ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
                        ddl_SPName.SelectedIndex = 0;
                    }
                    else
                    {
                        ddl_SPName.DataSource = users.Where(f => agent_tmp.Contains(f.CV_CODE)).OrderBy(f => f.FullName_ddl);
                        ddl_SPName.DataBind();
                        ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
                        ddl_SPName.SelectedIndex = 0;
                    }
                   
                }
            }
            else
            {

                //ddlAgentName
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                ddlAgentName.DataSource = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim());
                ddlAgentName.DataBind();
                ddlAgentName.Enabled = false;

                //ddl_SPName
                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                  , string.Empty, string.Empty, string.Empty, ddlAgentName.SelectedValue, null, string.Empty, string.Empty);

                users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์").OrderBy(f => f.AgentName);

                ddl_SPName.DataSource = users.OrderBy(f => f.FullName_ddl);
                ddl_SPName.DataBind();
                ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");


            }

            ddlMonthGroup_From.SelectedIndex = 0;
            ddlMonthGroup_To.SelectedIndex = 0;

            CultureInfo ThaiCulture = new CultureInfo("th-TH");
            DateTime DtNow = new DateTime();
            DtNow = DateTime.Now;
            txt_YearGroup_From.Text = (DtNow.ToString("yyyy", ThaiCulture));
            txt_YearGroup_To.Text = (DtNow.ToString("yyyy", ThaiCulture));


        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }
    #endregion    
}