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

public partial class Views_ReportOrdersYearlyTarget : System.Web.UI.Page
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
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

            string Region = ddlSearchRegion.SelectedItem.ToString();
            string AgentName = ddlAgentName.SelectedValue;
            string Year = txt_Year.Text;

            if (Region == "เลือกทั้งหมด")
                Region = string.Empty;         

            if (AgentName == "เลือกทั้งหมด")
                AgentName = string.Empty;            

            List<RPT_SO_TARGET_YEAR_4125> rt_RPT_TARGET_YEAR_4125 = new List<RPT_SO_TARGET_YEAR_4125>();
            rt_RPT_TARGET_YEAR_4125 = Reports.RPT_SO_TARGET_YEAR_4125(Region, AgentName, Year, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            if (rt_RPT_TARGET_YEAR_4125.Count > 0)
            {
                string url = "../Report/ReportViewer.aspx?RPT=ReportOrdersYearlyTarget&Region=" + ddlSearchRegion.SelectedItem.ToString()
                     + "&AgentName=" + ddlAgentName.SelectedValue
                     + "&AgentFullName=" + ddlAgentName.SelectedItem.ToString()
                     + "&Year=" + txt_Year.Text;
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        SetUpDrowDownList();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlSearchRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            if (ddlSearchRegion.SelectedValue.ToString() != null)
            {
                if (ddlSearchRegion.SelectedIndex == 0)
                {
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    String[] agent_tmp1 = agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f.CV_Code).ToArray();

                    if(agent_tmp1.Length > 0)
                    {
                        ddlAgentName.DataSource = agent.Where(f => agent_tmp1.Contains(f.CV_Code)).OrderBy(f => f.CV_AgentName);
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
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ddlSearchRegion.SelectedValue.ToString(), "Active", string.Empty);
                    String[] agent_tmp1 = agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f.CV_Code).ToArray();
                    if(agent_tmp1.Length > 0)
                    {
                        ddlAgentName.DataSource = agent.Where(f => agent_tmp1.Contains(f.CV_Code)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        ddlAgentName.DataSource = agent.OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }

                    //ddlAgentName.DataSource = agent.OrderBy(f => f.CV_AgentName);
                    //ddlAgentName.DataBind();
                    //ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
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
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();
                    String[] agent_tmp1 = agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f.CV_Code).ToArray();
                   
                    if(agent_tmp1.Length > 0)
                    {
                        ddlAgentName.DataSource = agent.Where(f => agent_tmp1.Contains(f.CV_Code)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        ddlAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }

                   
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

            }

            CultureInfo ThaiCulture = new CultureInfo("th-TH");
            DateTime DtNow = new DateTime();
            DtNow = DateTime.Now;
            txt_Year.Text = (DtNow.ToString("yyyy", ThaiCulture));
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    private bool IsValidForm()
    {
        bool ret = true;

        //if (Int32.Parse(this.ddlMonth_From.Text) > Int32.Parse(this.ddlMonth_To.Text))
        //{
        //    Show("โปรดระบุเดือน ให้ถูกต้อง");

        //    ret = false;
        //}
        return ret;
    }
    #endregion
}