using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ReportDailyFinance : System.Web.UI.Page
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
            InvoiceDate.Text = DateTime.Now.ToShortDateString();
            InvoiceDateTo.Text = DateTime.Now.ToShortDateString();

        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

            List<RPT_SALE_AMT_DAILY_41220A> rt_RPT_AMT_DAILY = new List<RPT_SALE_AMT_DAILY_41220A>();
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

            string CV_Code = user_class.CV_CODE;

            if (user_class.User_Group_ID == "CP Meiji")
            {
                string region = user_class.Region;
                string[] regions = region.Split(',');
               
                List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));
                foreach (dbo_AgentClass _cv in cv_code2)
                {
                    List<RPT_SALE_AMT_DAILY_41220A> _inrpt = Reports.RPT_SALE_AMT_DAILY_41220A(string.Empty, _cv.CV_Code, InvoiceDate.Text, InvoiceDateTo.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                    foreach (RPT_SALE_AMT_DAILY_41220A rpt in _inrpt)
                    {
                        rt_RPT_AMT_DAILY.Add(rpt);
                    }

                }
            }
            else
            {
                rt_RPT_AMT_DAILY = Reports.RPT_SALE_AMT_DAILY_41220A(string.Empty,CV_Code, InvoiceDate.Text, InvoiceDateTo.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }

            if (rt_RPT_AMT_DAILY.Count > 0)
            {
                string url = "../Report/ReportViewer_01.aspx?RPT=ReportDailyFinance&InvoiceDate=" + InvoiceDate.Text
                + "&InvoiceDateTo=" + InvoiceDateTo.Text
                + "&CV_CODE=" + CV_Code;

                string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
            }
            else
            {
                Messages.Show("ไม่พบข้อมูล กรุณาเลือกเงื่อนไขอีกครั้ง", this.Page);
            }
        }
        catch(Exception ex)
        {

        }

        
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        SetUpDrowDownList();
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
}