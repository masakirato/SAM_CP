#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_ReportReduceDebt_SPAndAgent : System.Web.UI.Page
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

    protected void ddlSearchRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            ddl_SPName.Items.Clear();
            ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");

            if (ddlSearchRegion.SelectedValue.ToString() != null)
            {
                if (ddlSearchRegion.SelectedIndex == 0)
                {
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if (cv_code1.Count > 0)
                    {
                        ddlAgentName.DataSource = cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        String strString = user_class.Region;
                        String[] myArr = strString.Split(',');

                        Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown_Report("07");
                        String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();


                        ddlAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
   
                }
                else
                {
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ddlSearchRegion.SelectedValue.ToString(), "Active", string.Empty);

                    ddlAgentName.DataSource = agent.OrderBy(f => f.CV_AgentName);
                    ddlAgentName.DataBind();
                    ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void ddlAgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            String User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            String strString = user_class.Region;
            String[] myArr = strString.Split(',');
            //ddlAgentName
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
            String[] agent_tmp = agent.Where(f => myArr.Contains(f.Location_Region)).OrderBy(f => f.AgentName).Select(f => f.CV_Code).ToArray();

            if (ddlAgentName.SelectedIndex == 0)
            {
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

                ddl_SPName.DataSource = users.Where(f => agent_tmp.Contains(f.CV_CODE)).OrderBy(f => f.FullName_ddl);
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
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        System.Threading.Thread.Sleep(500);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidForm())
            {


                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(500);

                string Region = ddlSearchRegion.SelectedItem.ToString();
                string AgentName = ddlAgentName.SelectedValue;
                string SPName = ddl_SPName.SelectedValue;
                string Status = ddl_Status.SelectedValue;


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
                if (Status == "เลือกทั้งหมด")
                {
                    Status = string.Empty;
                }

                List<RPT_AGENT_DEBT_41222> rt_RPT_AGENT_DEBT = new List<RPT_AGENT_DEBT_41222>();


                if (Status == "ชำระครบแล้ว")
                {
                    Status = "2";
                }
                else if (Status == "ค้างชำระ")
                {
                    Status = "1";
                }


                rt_RPT_AGENT_DEBT = Reports.RPT_AGENT_DEBT_41222(Region, AgentName, SPName, DebtDate_From.Text, DebtDate_To.Text, Status, string.Empty, string.Empty, string.Empty, string.Empty);

                if (rt_RPT_AGENT_DEBT.Count > 0)
                {
                    string User_ID = Request.Cookies["User_ID"].Value;
                    string url = "../Report/ReportViewer_01.aspx?RPT=ReportReduceDebt_SPAndAgent&Region=" + Region
                        + "&AgentName=" + ddlAgentName.SelectedValue
                        + "&SPName=" + ddl_SPName.SelectedValue
                        + "&DebtDate_From=" + DebtDate_From.Text
                        + "&DebtDate_To=" + DebtDate_To.Text
                        + "&Status=" + ddl_Status.SelectedValue;

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
    #endregion

    #region Method
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
                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown_Report("07");

                ddlSearchRegion.DataSource = region.Where(f => myArr.Contains(f.Value));
                ddlSearchRegion.DataBind();
                ddlSearchRegion.Items.Insert(0, "เลือกทั้งหมด");

                if (ddlSearchRegion.SelectedIndex == 0)
                {

                    String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if (cv_code1.Count > 0)
                    {
                        ddlAgentName.DataSource = cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        ddlAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }  

                    ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");
                }
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


            }

            ddl_SPName.SelectedIndex = 0;
            ddl_Status.SelectedIndex = 0;


            DebtDate_From.Text = DateTime.Now.ToShortDateString();
            DebtDate_To.Text = DateTime.Now.ToShortDateString();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }
    private bool IsValidForm()
    {

        bool ret = true;

        if (this.DebtDate_From.Text.Trim() != "" && this.DebtDate_To.Text.Trim() != "")
        {
            if (DateTime.Parse(this.DebtDate_To.Text) < DateTime.Parse(this.DebtDate_From.Text))
            {
                Messages.Show("โปรดระบุช่วงวันที่ ให้ถูกต้อง", this.Page);
                DebtDate_From.Text = DateTime.Now.ToShortDateString();
                DebtDate_To.Text = DateTime.Now.ToShortDateString();

                ret = false;
            }
        }

        return ret;
    }
    #endregion    
}