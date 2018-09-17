using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using log4net;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_RT_ShowReportPointPDF : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string datetime = DateTime.Now.ToShortDateString();
    string Time = DateTime.Now.ToShortTimeString();
    protected void Page_Init(object sender, EventArgs e)
    {
        string RPT = Request.QueryString["RPT"];
        string PRM = Request.QueryString["PRM"];
        string Region = Request.QueryString["Region"];
        string AgentName = Request.QueryString["AgentName"];
        string MonthGroup_From = Request.QueryString["MonthGroup_From"];
        string MonthGroup_To = Request.QueryString["MonthGroup_To"];
        string YearGroup_From = Request.QueryString["YearGroup_From"];
        string YearGroup_To = Request.QueryString["YearGroup_To"];
        string SPName = Request.QueryString["SPName"];

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

        GenerateReport(RPT, PRM, Region, AgentName, MonthGroup_From, MonthGroup_To, YearGroup_From, YearGroup_To, SPName);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string RPT = Request.QueryString["RPT"];
            //string PRM = Request.QueryString["PRM"];
            //string Region = Request.QueryString["Region"];
            //string AgentName = Request.QueryString["AgentName"];
            //string MonthGroup_From = Request.QueryString["MonthGroup_From"];
            //string MonthGroup_To = Request.QueryString["MonthGroup_To"];
            //string YearGroup_From = Request.QueryString["YearGroup_From"];
            //string YearGroup_To = Request.QueryString["YearGroup_To"];
            //string SPName = Request.QueryString["SPName"];

            //if (Region == "เลือกทั้งหมด")
            //{
            //    Region = string.Empty;
            //}
            //if (AgentName == "เลือกทั้งหมด")
            //{
            //    AgentName = string.Empty;
            //}
            //if (SPName == "เลือกทั้งหมด")
            //{
            //    SPName = string.Empty;
            //}

            //GenerateReport(RPT, PRM, Region, AgentName, MonthGroup_From, MonthGroup_To, YearGroup_From, YearGroup_To, SPName);

        }
    }
    protected void GenerateReport(string RPT, string PRM, string Region, string AgentName, string MonthGroup_From, string MonthGroup_To, string YearGroup_From, string YearGroup_To, string SPName)
    {
        try
        {
            ReportDocument cryRpt = new ReportDocument();
            string User_ID = Request.Cookies["User_ID"].Value;
            //TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            //TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            //ConnectionInfo crConnectionInfo = new ConnectionInfo();
            //Tables CrTables;

            switch (RPT)
            {
                case "ReportPoint_SP":

                    ReportViewer1.ID = "รายงานแต้ม_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    List<RPT_SP_POINT_41225> rt_RPT_SP_POINT = new List<RPT_SP_POINT_41225>();

                    rt_RPT_SP_POINT = Reports.RPT_SP_POINT_41225(Region, AgentName, MonthGroup_From, MonthGroup_To, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    // rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName,SPName,InvoiceDate,InvoiceDateTo,ProductGroup,Size);   
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportPoint.rpt"));
                    cryRpt.SetDataSource(rt_RPT_SP_POINT);
                    break;
                case "ReportPoint_Agent":
                    ReportViewer1.ID = "รายงานแต้มสำหรับเอเยนต์_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    List<RPT_AGENT_POINT_41226> rt_RPT_AGENT_POINT = new List<RPT_AGENT_POINT_41226>();
                    rt_RPT_AGENT_POINT = Reports.RPT_AGENT_POINT_41226(string.Empty, AgentName, SPName, MonthGroup_From, MonthGroup_To, YearGroup_From, YearGroup_To, string.Empty, string.Empty, string.Empty);

                    foreach (var d in rt_RPT_AGENT_POINT)
                    {
                        d.paramMonth_From = CV_Month(MonthGroup_From);
                        d.paramMonth_To = CV_Month(MonthGroup_To);
                        d.paramYear_From = YearGroup_From;
                        d.paramYear_To = YearGroup_To;
                    }
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportPoint_Agent.rpt"));
                    cryRpt.SetDataSource(rt_RPT_AGENT_POINT);
                    break;


            }

            //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();

            ReportViewer1.ToolPanelView = ToolPanelViewType.None;
            ReportViewer1.HasCrystalLogo = false;
            ReportViewer1.ReportSource = cryRpt;
            ReportViewer1.RefreshReport();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    private string CV_Month(string Month)
    {
        string strMonth = "";

        if (Month == "1")
        {
            strMonth = "มกราคม";
        }
        else if (Month == "2")
        {
            strMonth = "กุมภาพันธ์";
        }
        else if (Month == "3")
        {
            strMonth = "มีนาคม";
        }
        else if (Month == "4")
        {
            strMonth = "เมษายน";
        }
        else if (Month == "5")
        {
            strMonth = "พฤษภาคม";
        }
        else if (Month == "6")
        {
            strMonth = "มิถุนายน";
        }
        else if (Month == "7")
        {
            strMonth = "กรกฎาคม";
        }
        else if (Month == "8")
        {
            strMonth = "สิงหาคม";
        }
        else if (Month == "9")
        {
            strMonth = "กันยายน";
        }
        else if (Month == "10")
        {
            strMonth = "ตุลาคม";
        }
        else if (Month == "11")
        {
            strMonth = "พฤศจิกายน";
        }
        else if (Month == "12")
        {
            strMonth = "ธันวาคม";
        }

        return strMonth;


    }
}