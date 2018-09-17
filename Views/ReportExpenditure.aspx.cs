using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ReportExpenditure : System.Web.UI.Page
{
    //private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetUpDrowDownList();
        }

    }

    private void SetUpDrowDownList()
    {
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            CultureInfo ThaiCulture = new CultureInfo("th-TH");
            DateTime DtNow = new DateTime();
            DtNow = DateTime.Now;
            txt_Year.Text = (DtNow.ToString("yyyy", ThaiCulture));

            ddlMonthGroup.SelectedIndex = 0;
            ddlMonthGroupTo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            //logger.Error(ex);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);


            List<RPT_EXPENSE_MONTHLY_41218> rt_RPT_EXPENSE_MONTHLY = new List<RPT_EXPENSE_MONTHLY_41218>();
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
            if (user_class.User_Group_ID == "CP Meiji")
            {
                string region = user_class.Region;

                string[] regions = region.Split(',');

                List<dbo_AgentClass> cv_code_ = new List<dbo_AgentClass>();

                foreach (string in_region in regions)
                {
                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => f.Location_Region == in_region).Select(f => f));
                    foreach (dbo_AgentClass _cv in cv_code2)
                    {
                        //cv_code_.Add(_cv);
                        List<RPT_EXPENSE_MONTHLY_41218> _inrpt = Reports.RPT_EXPENSE_MONTHLY_41218(string.Empty, user_class.CV_CODE, ddlMonthGroup.SelectedValue, ddlMonthGroupTo.SelectedValue, txt_Year.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                        foreach (RPT_EXPENSE_MONTHLY_41218 rpt in _inrpt)
                        {
                            rt_RPT_EXPENSE_MONTHLY.Add(rpt);
                        }

                    }
                }

            }
            else
            {
                rt_RPT_EXPENSE_MONTHLY = Reports.RPT_EXPENSE_MONTHLY_41218(string.Empty, user_class.CV_CODE, ddlMonthGroup.SelectedValue, ddlMonthGroupTo.SelectedValue, txt_Year.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
              
            }



            if(rt_RPT_EXPENSE_MONTHLY.Count > 0)
            {
                string url = "../Report/ReportViewer_01.aspx?RPT=ReportExpenditure&MonthGroup=" + ddlMonthGroup.SelectedValue
               + "&MonthGroupTo=" + ddlMonthGroupTo.SelectedValue
               + "&Year=" + txt_Year.Text + "&CV_Code=" + user_class.CV_CODE;

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
            //logger.Error(ex);
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        SetUpDrowDownList();
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
}