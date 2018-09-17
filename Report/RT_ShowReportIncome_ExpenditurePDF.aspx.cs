using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using log4net;
public partial class Report_RT_ShowReporIncome_ExpenditurePDF : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string RPT = Request.QueryString["RPT"];
            string PRM = Request.QueryString["PRM"];
            string MonthGroup = Request.QueryString["MonthGroup"];
            string MonthGroupTo = Request.QueryString["MonthGroupTo"];
            string Year = Request.QueryString["Year"];
            String CV_CODE = Request.QueryString["CV_Code"];

            string InvoiceDate = Request.QueryString["InvoiceDate"];
            string InvoiceDateTo = Request.QueryString["InvoiceDateTo"];

            GenerateReport(RPT, PRM, MonthGroup, MonthGroupTo, Year, InvoiceDate, InvoiceDateTo, CV_CODE);

        }
    }
    protected void GenerateReport(string RPT, string PRM, string MonthGroup, string MonthGroupTo, string Year, string InvoiceDate, string InvoiceDateTo, string CV_Code)
    {
        try
        {
            ReportDocument cryRpt = new ReportDocument();

            switch (RPT)
            {
                case "ReportExpenditure":

                     List<RPT_EXPENSE_MONTHLY_41218> rt_RPT_EXPENSE_MONTHLY = new List<RPT_EXPENSE_MONTHLY_41218>();
                     rt_RPT_EXPENSE_MONTHLY = Reports.RPT_EXPENSE_MONTHLY_41218(string.Empty, CV_Code, MonthGroup, MonthGroupTo, Year, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);  
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportExpenditure.rpt"));
                    cryRpt.SetDataSource(rt_RPT_EXPENSE_MONTHLY);
                    break;

                case "ReportSummaryIncome":

                    List<RPT_SUMM_EXPENSE_41219> rt_RPT_EXPENSE = new List<RPT_SUMM_EXPENSE_41219>();
                    rt_RPT_EXPENSE = Reports.RPT_SUMM_EXPENSE_41219(string.Empty, CV_Code, InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportSummaryIncome.rpt"));
                    cryRpt.SetDataSource(rt_RPT_EXPENSE);
                    break;
                case "ReportFinanceClerk":

                     List<RPT_REV_EXP_DAILY_41220> rt_RPT_EXP_DAILY = new List<RPT_REV_EXP_DAILY_41220>();

                     rt_RPT_EXP_DAILY = Reports.RPT_REV_EXP_DAILY_41220(string.Empty, CV_Code, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                   // rt_RPT_EXP_DAILY = Reports.RPT_REV_EXP_DAILY_41220(string.Empty, CV_Code, InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    // rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName,SPName,InvoiceDate,InvoiceDateTo,ProductGroup,Size);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportFinanceClerk.rpt"));
                    cryRpt.SetDataSource(rt_RPT_EXP_DAILY);
                    break;
                case "ReportDailyFinance":

                    List<RPT_SALE_AMT_DAILY_41220A> rt_RPT_AMT_DAILY = new List<RPT_SALE_AMT_DAILY_41220A>();
                    rt_RPT_AMT_DAILY = Reports.RPT_SALE_AMT_DAILY_41220A(string.Empty,string.Empty,InvoiceDate,InvoiceDateTo,string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,string.Empty);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportDailyFinance.rpt"));
                    cryRpt.SetDataSource(rt_RPT_AMT_DAILY);
                    break;
            }

            BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            Response.Flush();
            Response.Close();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
}