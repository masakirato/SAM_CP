﻿using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ReportSummaryIncome : System.Web.UI.Page
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
            logger.Error(ex.Message);
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {

        try
        {

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);


            List<RPT_SUMM_EXPENSE_41219> rt_RPT_EXPENSE = new List<RPT_SUMM_EXPENSE_41219>();
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

                        List<RPT_SUMM_EXPENSE_41219> _inrpt = Reports.RPT_SUMM_EXPENSE_41219(string.Empty, user_class.CV_CODE, InvoiceDate.Text, InvoiceDateTo.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                        foreach (RPT_SUMM_EXPENSE_41219 rpt in _inrpt)
                        {
                            rt_RPT_EXPENSE.Add(rpt);
                        }

                    }
                }


                foreach (var d in rt_RPT_EXPENSE)
                {
                    d.paramCV_Code = d.CV_Code;

                    // dbo_AgentClass _agent = dbo_AgentDataClass.Select_Record(d.paramCV_Code);
                    dbo_AgentClass _agent = agent.FirstOrDefault(f => f.CV_Code == d.CV_Code);
                    if (_agent != null)
                        d.paramCV_Name = _agent.AgentName;
                }

            }
            else
            {
                rt_RPT_EXPENSE = Reports.RPT_SUMM_EXPENSE_41219(string.Empty, user_class.CV_CODE, InvoiceDate.Text, InvoiceDateTo.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                foreach (var d in rt_RPT_EXPENSE)
                {
                    d.paramCV_Code = d.CV_Code;

                    // dbo_AgentClass _agent = dbo_AgentDataClass.Select_Record(d.paramCV_Code);
                    dbo_AgentClass _agent = agent.FirstOrDefault(f => f.CV_Code == d.CV_Code);

                    if (_agent != null)
                        d.paramCV_Name = _agent.AgentName;

                }
            }


            if (rt_RPT_EXPENSE.Count > 0)
            {
                //string User_ID = Request.Cookies["User_ID"].Value;
                string url = "../Report/ReportViewer_01.aspx?RPT=ReportSummaryIncome&InvoiceDate=" + InvoiceDate.Text 
                    + "&InvoiceDateTo=" + InvoiceDateTo.Text 
                    + "&CV_CODE=" + user_class.CV_CODE;

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
            logger.Error(ex.Message);
        }
       
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        SetUpDrowDownList();
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
}