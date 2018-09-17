#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_ReportAllEmployee : System.Web.UI.Page
{
    #region Private Variables
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidForm())
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                string Region = ddlSearchRegion.SelectedItem.ToString();
                string AgentName = ddlAgentName.SelectedValue;
                string SPName = ddl_SPName.SelectedValue;
                string strJoinDate_From = JoinDate_From.Text;
                string strJoinDate_To = JoinDate_To.Text;
                string WorkingAge_From = txt_WorkingAge_From.Text;
                string WorkingAge_To = txt_WorkingAge_To.Text;
                string ApprovalStatus = ddlApprovalStatus.SelectedValue;
                string Position = ddl_Position.SelectedValue;

                if (Region == "เลือกทั้งหมด")
                {
                    Region = string.Empty;
                }
                if (AgentName == "เลือกทั้งหมด")
                {
                    AgentName = string.Empty;
                }
                if (SPName == "เลือกทั้งหมด")
                {
                    SPName = string.Empty;
                }
                if (ApprovalStatus == "เลือกทั้งหมด")
                {
                    ApprovalStatus = string.Empty;
                }

                if (Position == "เลือกทั้งหมด")
                {
                    Position = string.Empty;
                }

                List<RPT_SUMM_SP_BY_AGENT_41215> rt_RPT_Emp = new List<RPT_SUMM_SP_BY_AGENT_41215>();
                rt_RPT_Emp = Reports.RPT_SUMM_SP_BY_AGENT_41215(Region, AgentName, ApprovalStatus, Position, strJoinDate_From, strJoinDate_To
                    , WorkingAge_From, WorkingAge_To, SPName, string.Empty);

                if (rt_RPT_Emp.Count > 0)
                {
                    string User_ID = Request.Cookies["User_ID"].Value;
                    string url = "../Report/RT_ShowReportEmployee.aspx?RPT=ReportAllEmployee&Region=" + ddlSearchRegion.SelectedItem.ToString()
                        + "&RegionName=" + ddlSearchRegion.SelectedItem.ToString()
                        + "&AgentName=" + ddlAgentName.SelectedValue
                        + "&AgentFullName=" + ddlAgentName.SelectedItem.ToString()
                        + "&SPName=" + ddl_SPName.SelectedValue
                        + "&SPFullName=" + ddl_SPName.SelectedItem.ToString() 
                        + "&JoinDate_From=" + JoinDate_From.Text 
                        + "&JoinDate_To=" + JoinDate_To.Text
                        + "&WorkingAge_From=" + txt_WorkingAge_From.Text 
                        + "&WorkingAge_To=" + txt_WorkingAge_To.Text
                        + "&ApprovalStatus=" + ddlApprovalStatus.SelectedValue 
                        + "&Position=" + ddl_Position.SelectedValue
                        + "&PositionFullName=" + ddl_Position.SelectedItem.ToString();

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
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //JoinDate_From.Text = DateTime.Now.ToShortDateString();
                JoinDate_To.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlAgentName.Items.Clear();
        ddlSearchRegion.Items.Clear();
        ddl_SPName.Items.Clear();
        //
        SetUpDrowDownList();

        txt_WorkingAge_From.Text = string.Empty;
        txt_WorkingAge_To.Text = string.Empty;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlSearchRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSearchRegion.SelectedValue.ToString() != null)
            {
                if (ddlSearchRegion.SelectedIndex == 0)
                {
                    string User_ID = Request.Cookies["User_ID"].Value;
                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if(cv_code1.Count > 0)
                    {
                        ddlAgentName.DataSource = cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        String strString = user_class.Region;
                        String[] myArr = strString.Split(',');
                        Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");
                        String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();
                        //ddlAgentName
                        //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                        ddlAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }

                  
                }
                else
                {
                    string User_ID = Request.Cookies["User_ID"].Value;
                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ddlSearchRegion.SelectedValue.ToString(), "Active", string.Empty);
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if (cv_code1.Count > 0)
                    {
                        ddlAgentName.DataSource = cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        ddlAgentName.DataSource = agent.OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }

                      
                }

                if (ddl_SPName.Items.Count > 0)
                {
                    ddl_SPName.Items.Clear();
                    ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlAgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlAgentName.SelectedIndex == 0)
            {
                String User_ID = Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                //ddlAgentName
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] agent_tmp = agent.Where(f => myArr.Contains(f.Location_Region)).OrderBy(f => f.AgentName).Select(f => f.CV_Code).ToArray();

                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);
                users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์").OrderBy(f => f.AgentName);

                //ddl_SPName.DataSource = users.Where(f => agent_tmp.Contains(f.CV_CODE)).OrderBy(f => f.FullName_ddl);
                //ddl_SPName.DataBind();
                ddl_SPName.Items.Clear();
                ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
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
            logger.Error(ex.Message);
        }
    }
    #endregion

    #region Methods
    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            if (user_class.User_Group_ID == "CP Meiji")
            {
                //ddlSearchRegion
                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");

                ddlSearchRegion.DataSource = region.Where(f => myArr.Contains(f.Value));
                ddlSearchRegion.DataBind();
                ddlSearchRegion.Items.Insert(0, "เลือกทั้งหมด");

                if (ddlSearchRegion.SelectedIndex == 0)
                {
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();

                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));
                    if (cv_code1.Count != 0)
                    {
                        ddlAgentName.DataSource = cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                        ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        //ddlAgentName
                        ddlAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                        ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                   
                }

                //ddl_Position
                Dictionary<string, string> CP_Position = dbo_ItemDataClass.GetDropDown("05");
                ddl_Position.DataSource = CP_Position;
                ddl_Position.DataBind();
                ddl_Position.Items.RemoveAt(0);
                ddl_Position.Items.Insert(0, "เลือกทั้งหมด");
                ddl_Position.SelectedIndex = 0;

            }
            else
            {
                //ddlSearchRegion
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] rg = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim()).Select(f => f.Location_Region).ToArray();

                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown_Report("07");
                ddlSearchRegion.DataSource = region.Where(f => rg.Contains(f.Value));
                ddlSearchRegion.DataBind();
                ddlSearchRegion.Enabled = false;
                //ddlAgentName
                //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
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


                //ddl_Position
                Dictionary<string, string> Agent_Position = dbo_ItemDataClass.GetDropDown("05");
                ddl_Position.DataSource = Agent_Position;
                ddl_Position.DataBind();
                ddl_Position.Items.RemoveAt(0);
                ddl_Position.Items.Insert(0, "เลือกทั้งหมด");
                ddl_Position.SelectedIndex = 0;


            }
            //ddlApprovalStatus
            List<dbo_UserClass> ApprovalStatus = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                  , string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);

            ddlApprovalStatus.DataSource = ApprovalStatus.GroupBy(f => f.Status).Select(f => f.First());
            ddlApprovalStatus.DataBind();
            ddlApprovalStatus.Items.Insert(0, "เลือกทั้งหมด");
            ddlApprovalStatus.SelectedIndex = 0;

            ddl_SPName.SelectedIndex = 0;


            //JoinDate_From.Text = DateTime.Now.ToShortDateString();
            JoinDate_To.Text = DateTime.Now.ToShortDateString();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private bool IsValidForm()
    {

        bool ret = true;

        if (this.JoinDate_From.Text.Trim() != "" && this.JoinDate_To.Text.Trim() != "")
        {
            if (DateTime.Parse(this.JoinDate_To.Text) < DateTime.Parse(this.JoinDate_From.Text))
            {
                Messages.Show("โปรดระบุช่วงวันที่ ให้ถูกต้อง", this.Page);

                ret = false;
            }
        }

        return ret;
    }
    #endregion
}