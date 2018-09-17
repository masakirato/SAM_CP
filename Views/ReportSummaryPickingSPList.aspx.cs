#region Using
using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_ReportSummaryPickingSPList : System.Web.UI.Page
{
    #region Private Variables
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private string Region;
    private string AgentName;
    private string ProductGroup;
    private string Size;
    private string SPName;
    private string ProductName;
    private string ProductID;
    private string InvoiceDate;
    private string InvoiceDateTo;
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
        //Thread.CurrentThread.CurrentUICulture = new CultureInfo("th-TH");
        //DateTimeFormatInfo myFI = new CultureInfo("th-TH", true).DateTimeFormat;
        //myFI.LongDatePattern = "dd/MM/yyyy";
        //this.cldInvoiceDate.Format = myFI.LongDatePattern;
        

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

                Region = ddlSearchRegion.SelectedItem.ToString();
                AgentName = ddlAgentName.SelectedValue;
                InvoiceDate = txtRequestDateFrom.Text;
                InvoiceDateTo = txtRequestDateTo.Text;
                ProductGroup = ddl_ProductGroup.SelectedValue;
                SPName = ddl_SPName.SelectedValue;
                Size = ddl_Size.SelectedValue;

                if (Region == "เลือกทั้งหมด")
                    Region = string.Empty;

                if (AgentName == "เลือกทั้งหมด")
                    AgentName = string.Empty;

                if (ProductGroup == "เลือกทั้งหมด")
                    ProductGroup = string.Empty;

                if (Size == "เลือกทั้งหมด")
                    Size = string.Empty;

                if (SPName == "เลือกทั้งหมด")
                    SPName = string.Empty;

                List<RPT_SUMM_RQ_SP_4126> rt_RPT_SUMM_RQ_SP = new List<RPT_SUMM_RQ_SP_4126>();                
                rt_RPT_SUMM_RQ_SP = Reports.RPT_SUMM_RQ_SP_4126(Region, AgentName, SPName, InvoiceDate, InvoiceDateTo, ProductGroup, Size, string.Empty, string.Empty, string.Empty);

                if (rt_RPT_SUMM_RQ_SP.Count > 0)
                {
                    string User_ID = Request.Cookies["User_ID"].Value;
                    //string url = "../Report/RT_ShowReportStockPDF.aspx?RPT=ReportSummaryPickingSPList&Region="
                    //    + ddlSearchRegion.SelectedValue + "&AgentName=" + ddlAgentName.SelectedValue + "&SPName=" + ddl_SPName.SelectedValue + "&InvoiceDate="
                    //    + txtRequestDateFrom.Text + "&InvoiceDateTo=" + txtRequestDateTo.Text + "&ProductGroup=" + ddl_ProductGroup.SelectedValue + "&Size=" + ddl_Size.SelectedValue;

                    string url = "../Report/ReportViewer.aspx?RPT=ReportSummaryPickingSPList&Region="
                       + ddlSearchRegion.SelectedItem.ToString() + "&AgentName=" + ddlAgentName.SelectedValue
                       + "&AgentFullName=" + ddlAgentName.SelectedItem.ToString() + "&SPName=" + ddl_SPName.SelectedValue
                       + "&SPFullName=" + ddl_SPName.SelectedItem.ToString() + "&InvoiceDate="
                       + txtRequestDateFrom.Text + "&InvoiceDateTo=" + txtRequestDateTo.Text + "&ProductGroup=" 
                       + ddl_ProductGroup.SelectedValue + "&Size=" + ddl_Size.SelectedValue;

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
                txtRequestDateFrom.Text = DateTime.Now.ToShortDateString();
                txtRequestDateTo.Text = DateTime.Now.ToShortDateString();
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
        ddlAgentName.Items.Clear();
        ddl_SPName.Items.Clear();
        //
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
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if (cv_code1.Count > 0)
                    {
                        ddlAgentName.DataSource =cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        String strString = user_class.Region;
                        String[] myArr = strString.Split(',');

                        Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");
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
            logger.Error(ex.Message);
        }
    }

    protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
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
                //ddlSearchRegion
                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");

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
                    ddl_SPName.SelectedIndex = 0;
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

            txtRequestDateFrom.Text = DateTime.Now.ToShortDateString();
            txtRequestDateTo.Text = DateTime.Now.ToShortDateString();


            ddl_ProductGroup.SelectedIndex = 0;
            ddl_Size.Items.Insert(0, "เลือกทั้งหมด");
            ddl_Size.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private bool IsValidForm()
    {
        bool ret = true;

        if (this.txtRequestDateFrom.Text.Trim() != "" && this.txtRequestDateTo.Text.Trim() != "")
        {
            if (DateTime.Parse(this.txtRequestDateTo.Text) < DateTime.Parse(this.txtRequestDateFrom.Text))
            {
                Messages.Show("โปรดระบุช่วงวันที่เบิกสินค้า ให้ถูกต้อง", this.Page);

                ret = false;
            }
        }

        return ret;
    }
    #endregion

  

}