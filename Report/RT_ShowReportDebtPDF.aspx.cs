using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using log4net;

public partial class Report_RT_ShowReportDebtPDF : System.Web.UI.Page
{
    private string RPT;
    private string PRM;
    private string Region;
    private string AgentName;
    private string SPName;
    private string DebtID;
    private string DebtName;
    private string DebtDate_From;
    private string DebtDate_To;
    private string Status;
    private string ddlType;
    private string CN_DN;
    private string InvoiceDate;
    private string InvoiceDateTo;


    // private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerateReport();

        }
    }

    private void GenerateReport()
    {
        try
        {
            RPT = Request.QueryString["RPT"];
            PRM = Request.QueryString["PRM"];
            Region = Request.QueryString["Region"];
            AgentName = Request.QueryString["AgentName"];
            SPName = Request.QueryString["SPName"];
            DebtID = Request.QueryString["DebtID"];
            DebtName = Request.QueryString["DebtName"];
            DebtDate_From = Request.QueryString["DebtDate_From"];
            DebtDate_To = Request.QueryString["DebtDate_To"];
            Status = Request.QueryString["Status"];
            ddlType = Request.QueryString["ddlType"];
            CN_DN = Request.QueryString["CN_DN"];
            InvoiceDate = Request.QueryString["InvoiceDate"];
            InvoiceDateTo = Request.QueryString["InvoiceDateTo"];


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
            dbo_UserClass user_class;
            ReportDocument cryRpt = new ReportDocument();

            switch (RPT)
            {
                case "ReportReduceDebt_CustomerAndSP":

                    List<RPT_SP_DEBT_41221> rt_RPT_CUSTOMER_SP = new List<RPT_SP_DEBT_41221>();
                    rt_RPT_CUSTOMER_SP = Reports.RPT_SP_DEBT_41221(Region, AgentName, SPName, DebtID, DebtName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty);
                    foreach (var d in rt_RPT_CUSTOMER_SP)
                    {
                        d.paramRegion_Name = Request.QueryString["Region"];
                        d.pramAgent_Name = Request.QueryString["AgentName"];
                        if (d.pramAgent_Name != "")
                        {
                            user_class = dbo_UserDataClass.Select_Record(d.pramAgent_Name);
                            d.pramAgent_Name = user_class.AgentName;
                        }
                        else
                        {
                            d.pramAgent_Name = "เลือกทั้งหมด";
                        }
                        d.pramSP_Name = Request.QueryString["SPName"];
                        d.pramDebt_StartDate = Request.QueryString["DebtDate_From"];
                        d.pramDebt_EndDate = Request.QueryString["DebtDate_To"];
                        d.pramStatus = Request.QueryString["Status"];
                        if (d.pramStatus == "")
                        {
                            d.pramStatus = "เลือกทั้งหมด";
                        }
                    }

                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_CustomerAndSP.rpt"));
                    cryRpt.SetDataSource(rt_RPT_CUSTOMER_SP);

                    //CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                    //CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                    //CrystalReportViewer1.ReportSource = cryRpt;


                    break;
                case "ReportReduceDebt_SPAndAgent":

                    List<RPT_AGENT_DEBT_41222> rt_RPT_AGENT_DEBT = new List<RPT_AGENT_DEBT_41222>();
                    rt_RPT_AGENT_DEBT = Reports.RPT_AGENT_DEBT_41222(Region, AgentName, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                    foreach (var d in rt_RPT_AGENT_DEBT)
                    {
                        d.pramDebt_StartDate = Request.QueryString["DebtDate_From"];
                        d.pramDebt_EndDate = Request.QueryString["DebtDate_To"];
                        d.pramStatus = Request.QueryString["Status"];
                    }
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_SPAndAgent.rpt"));
                    cryRpt.SetDataSource(rt_RPT_AGENT_DEBT);
                    break;

                case "ReportReduceDebt_CustomerAndAgent":

                    List<RPT_CUSTOMER_DEBT_41223> rt_RPT_CUSTOMER_DEBT = new List<RPT_CUSTOMER_DEBT_41223>();

                    rt_RPT_CUSTOMER_DEBT = Reports.RPT_CUSTOMER_DEBT_41223(Region, AgentName, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_CustomerAndAgent.rpt"));
                    cryRpt.SetDataSource(rt_RPT_CUSTOMER_DEBT);
                    break;

                case "ReportReduceDebt":

                    List<RPT_DN_CN_41224> rt_RPT_DN_CN = new List<RPT_DN_CN_41224>();
                    rt_RPT_DN_CN = Reports.RPT_DN_CN_41224(Region, AgentName, string.Empty, InvoiceDate, InvoiceDateTo, ddlType, CN_DN, Status, string.Empty);
                    foreach (var d in rt_RPT_DN_CN)
                    {
                        d.paramRegion_Name = Request.QueryString["Region"];
                        d.pramAgent_Name = Request.QueryString["AgentName"];
                        if (d.pramAgent_Name != "")
                        {
                            user_class = dbo_UserDataClass.Select_Record(d.pramAgent_Name);
                            d.pramAgent_Name = user_class.AgentName;
                        }
                        else
                        {
                            d.pramAgent_Name = "เลือกทั้งหมด";
                        }
                        d.pramDocNo = Request.QueryString["DebtID"];
                        d.pramType = Request.QueryString["ddlType"];
                        if (d.pramType == "")
                        {
                            d.pramType = "เลือกทั้งหมด";
                        }
                        d.pramDebt_StartDate = Request.QueryString["InvoiceDate"];
                        d.pramDebt_EndDate = Request.QueryString["InvoiceDateTo"];
                        d.pramStatus = Request.QueryString["Status"];
                        if (d.pramStatus == "")
                        {
                            d.pramStatus = "เลือกทั้งหมด";
                        }
                    }

                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt.rpt"));
                    cryRpt.SetDataSource(rt_RPT_DN_CN);
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

        }

    }
}