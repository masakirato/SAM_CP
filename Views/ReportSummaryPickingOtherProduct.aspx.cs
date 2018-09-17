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

public partial class Views_ReportSummaryPickingOtherProduct : System.Web.UI.Page
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

                string strCountStockDate_From = CountStockDate_From.Text;
                string strCountStockDate_To = CountStockDate_To.Text;
                string productname = ddl_productname.SelectedValue;
                string RequisitionName = ddl_RequisitionName.SelectedValue;
                string Reason = ddl_Reason.SelectedValue;
                string product_name = ddl_productname.SelectedItem.ToString();
                string requisition_name = ddl_RequisitionName.SelectedItem.ToString();

                if (productname == "เลือกทั้งหมด")
                    productname = string.Empty;

                if (RequisitionName == "เลือกทั้งหมด")
                    RequisitionName = string.Empty;

                if (Reason == "เลือกทั้งหมด")
                    Reason = string.Empty;

                List<RPT_SUMM_RQ_OTHER__4129> rt_RPT_SUMM_RQ_OTHER = new List<RPT_SUMM_RQ_OTHER__4129>();
                string User_IDS = Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_IDS);
                if (user_class.User_Group_ID == "CP Meiji")
                {
                rt_RPT_SUMM_RQ_OTHER = Reports.RPT_SUMM_RQ_OTHER__4129(string.Empty, string.Empty, strCountStockDate_From, strCountStockDate_To
                    , productname, RequisitionName, Reason, string.Empty, string.Empty, string.Empty);
                }
                else
                {
                    rt_RPT_SUMM_RQ_OTHER = Reports.RPT_SUMM_RQ_OTHER__4129(string.Empty, user_class.CV_CODE, strCountStockDate_From, strCountStockDate_To
                    , productname, RequisitionName, Reason, string.Empty, string.Empty, string.Empty);
                }
                if (rt_RPT_SUMM_RQ_OTHER.Count > 0)
                {

                    string User_ID = Request.Cookies["User_ID"].Value;
                    string url = "../Report/ReportViewer.aspx?RPT=ReportSummaryPickingOtherProduct&CountStockDate_From=" + CountStockDate_From.Text + "&CountStockDate_To=" + CountStockDate_To.Text
                        + "&ProductName=" + ddl_productname.SelectedValue + "&RequisitionName=" + ddl_RequisitionName.SelectedValue + "&Reason=" + ddl_Reason.SelectedValue + "&product_name=" + ddl_productname.SelectedItem + "&requisition_name=" + ddl_RequisitionName.SelectedItem;

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
                CountStockDate_From.Text = DateTime.Now.ToShortDateString();
                CountStockDate_To.Text = DateTime.Now.ToShortDateString();
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
    #endregion

    #region Methods
    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            String User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            //ddl_productname
            List<dbo_ProductClass> ProductName = dbo_ProductDataClass.Search(string.Empty, string.Empty, string.Empty, null, string.Empty).OrderBy(f => f.Product_group_ID).OrderBy(f => f.Product_Name).ToList();

            ddl_productname.DataSource = ProductName;
            ddl_productname.DataBind();
            ddl_productname.Items.Insert(0, "เลือกทั้งหมด");

            if (user_class.User_Group_ID == "CP Meiji")
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

                if (agent_tmp1.Length > 0)
                {
                    String[] user_tmp = users.Where(f => agent_tmp1.Contains(f.CV_CODE)).Select(f => f.User_ID).ToArray();
                    List<dbo_RequisitionClass> RequisitionName = dbo_RequisitionDataClass.Search(string.Empty, string.Empty, string.Empty, null);

                    ddl_RequisitionName.DataSource = RequisitionName.Where(f => user_tmp.Contains(f.User_ID)).GroupBy(f => f.SP_Name).Select(f => f.First()).OrderBy(f => f.SP_Name);
                    ddl_RequisitionName.DataBind();
                    ddl_RequisitionName.Items.Insert(0, "เลือกทั้งหมด");
                }
                else
                {
                    String[] user_tmp = users.Where(f => agent_tmp.Contains(f.CV_CODE)).Select(f => f.User_ID).ToArray();
                    List<dbo_RequisitionClass> RequisitionName = dbo_RequisitionDataClass.Search(string.Empty, string.Empty, string.Empty, null);

                    ddl_RequisitionName.DataSource = RequisitionName.Where(f =>user_tmp.Contains(f.User_ID)).GroupBy(f => f.SP_Name).Select(f => f.First()).OrderBy(f => f.SP_Name);
                    ddl_RequisitionName.DataBind();
                    ddl_RequisitionName.Items.Insert(0, "เลือกทั้งหมด");
                }
               
            }
            else
            {
                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                              , string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);
                users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" || f.Position == "ซุปเปอร์ไวซ์เซอร์").OrderBy(f => f.AgentName);
                String[] user_tmp = users.Where(f => user_class.CV_CODE.Contains(f.CV_CODE)).Select(f => f.User_ID).ToArray();
                List<dbo_RequisitionClass> RequisitionName = dbo_RequisitionDataClass.Search(string.Empty, string.Empty, string.Empty, null);

                ddl_RequisitionName.DataSource = RequisitionName.Where(f => user_tmp.Contains(f.User_ID)).GroupBy(f => f.SP_Name).Select(f => f.First()).OrderBy(f => f.SP_Name);
                ddl_RequisitionName.DataBind();
                ddl_RequisitionName.Items.Insert(0, "เลือกทั้งหมด");
            }
              

            ddl_Reason.SelectedIndex = 0;

            CountStockDate_From.Text = DateTime.Now.ToShortDateString();
            CountStockDate_To.Text = DateTime.Now.ToShortDateString();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private bool IsValidForm()
    {
        bool ret = true;

        if (this.CountStockDate_From.Text.Trim() != "" && this.CountStockDate_To.Text.Trim() != "")
        {
            if (DateTime.Parse(this.CountStockDate_To.Text) < DateTime.Parse(this.CountStockDate_From.Text))
            {
                Messages.Show("โปรดระบุช่วงวันที่ ให้ถูกต้อง", this.Page);

                ret = false;
            }
        }

        return ret;
    }
    #endregion

}