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

public partial class Views_ReportCommissions : System.Web.UI.Page
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
            if (IsValidForm())
            {
                string User_ID = Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                string AgentName = user_class.CV_CODE;
                string SPName = ddl_SPName.SelectedValue;
                string ProductGroup = ddl_ProductGroup.SelectedValue;
                string Size = ddl_Size.SelectedValue;
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

                if (ProductGroup == "เลือกทั้งหมด")
                {
                    ProductGroup = string.Empty;
                }
                if (Size == "เลือกทั้งหมด")
                {
                    Size = string.Empty;
                }

                List<RPT_SP_COMMISSION_41216> rt_RPT_Comm = new List<RPT_SP_COMMISSION_41216>();
                rt_RPT_Comm = Reports.RPT_SP_COMMISSION_41216(string.Empty, AgentName, SPName, RequisitionDate_From.Text, RequisitionDate_To.Text, ProductGroup, Size, Lebel_SP, string.Empty, string.Empty);

                if (rt_RPT_Comm.Count > 0)
                {
                    string url = "../Report/RT_ShowReportEmployee.aspx?RPT=ReportCommissions&SPName=" + ddl_SPName.SelectedValue
                                   + "&RequisitionDate_From=" + RequisitionDate_From.Text
                                   + "&RequisitionDate_To=" + RequisitionDate_To.Text
                                   + "&ProductGroup=" + ddl_ProductGroup.SelectedValue
                                   + "&Size=" + ddl_Size.SelectedValue
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
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                RequisitionDate_From.Text = DateTime.Now.ToShortDateString();
                RequisitionDate_To.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddl_ProductGroup.Items.Clear();
        ddl_Size.Items.Clear();
        ddl_SPName.Items.Clear();
        //
        SetUpDrowDownList();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddl_ProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddl_ProductGroup.SelectedIndex > 0)
            {
                List<dbo_ProductClass> Product_Size = dbo_ProductDataClass.Search(string.Empty, string.Empty, ddl_ProductGroup.SelectedValue, null, string.Empty).GroupBy(f => f.Size).Select(grb => grb.First()).OrderBy(f => f.Size).ToList();
                //List<dbo_ProductClass> Product_Size = dbo_ProductDataClass.Search(string.Empty, string.Empty, ddl_Size.SelectedValue.ToString(), null, string.Empty).GroupBy(g => g.Product_group_ID).Select(grb0 => grb0.First()).GroupBy(f => f.Size).Select(grb => grb.First()).ToList();

                ddl_Size.DataSource = Product_Size;
                ddl_Size.DataBind();
                ddl_Size.Items.Insert(0, "เลือกทั้งหมด");
            }
            else
            {
                ddl_Size.Items.Clear();
                ddl_Size.Items.Insert(0, "เลือกทั้งหมด");
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

                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                //ddlAgentName
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] agent_tmp1 = agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f.CV_Code).ToArray();
                String[] agent_tmp = agent.Where(f => myArr.Contains(f.Location_Region)).OrderBy(f => f.AgentName).Select(f => f.CV_Code).ToArray();

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

                //ddl_SPName
                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                  , string.Empty, string.Empty, string.Empty, user_class.CV_CODE.Trim(), null, string.Empty, string.Empty);

                users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์").OrderBy(f => f.AgentName);

                ddl_SPName.DataSource = users.OrderBy(f => f.FullName_ddl);
                ddl_SPName.DataBind();
                ddl_SPName.Items.Insert(0, "เลือกทั้งหมด");

            }

            //ddl_ProductGroup     
            List<dbo_ProductClass> ProductGroup = dbo_ProductDataClass.Search(string.Empty, string.Empty, string.Empty, null, string.Empty).GroupBy(f => f.Product_group_ID).Select(grb => grb.First()).ToList();
            dbo_ProductClass b = new dbo_ProductClass();
            dbo_ProductClass c1 = new dbo_ProductClass();
            b.Product_group_ID = "เลือกทั้งหมด";
            c1.Product_group_ID = "อื่นๆ";
            ProductGroup.Insert(0, b);
            ProductGroup.Insert(ProductGroup.Count, c1);
            ddl_ProductGroup.DataSource = ProductGroup;
            ddl_ProductGroup.DataBind();

            ddl_Size.Items.Insert(0, "เลือกทั้งหมด");
            ddl_Size.SelectedIndex = 0;

            RequisitionDate_From.Text = DateTime.Now.ToShortDateString();
            RequisitionDate_To.Text = DateTime.Now.ToShortDateString();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private bool IsValidForm()
    {

        bool ret = true;

        if (this.RequisitionDate_From.Text.Trim() != "" && this.RequisitionDate_To.Text.Trim() != "")
        {
            if (DateTime.Parse(this.RequisitionDate_To.Text) < DateTime.Parse(this.RequisitionDate_From.Text))
            {
                Messages.Show("โปรดระบุช่วงวันที่ ให้ถูกต้อง", this.Page);

                ret = false;
            }
        }

        return ret;
    }
    #endregion
}