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

public partial class Report_RT_ShowReportSalesPDF : System.Web.UI.Page
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
        string SPName = Request.QueryString["SPName"];
        string RequisitionDate_From = Request.QueryString["RequisitionDate_From"];
        string RequisitionDate_To = Request.QueryString["RequisitionDate_To"];
        string ProductGroup = Request.QueryString["ProductGroup"];
        string Size = Request.QueryString["Size"];

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
        if (ProductGroup == "เลือกทั้งหมด")
        {
            ProductGroup = string.Empty;
        }
        if (Size == "เลือกทั้งหมด")
        {
            Size = string.Empty;
        }

        

        GenerateReport(RPT, PRM, Region, AgentName, SPName, RequisitionDate_From, RequisitionDate_To, ProductGroup, Size);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string RPT = Request.QueryString["RPT"];
            //string PRM = Request.QueryString["PRM"];
            //string Region = Request.QueryString["Region"];
            //string AgentName = Request.QueryString["AgentName"];  
            //string SPName = Request.QueryString["SPName"];
            //string RequisitionDate_From = Request.QueryString["RequisitionDate_From"];
            //string RequisitionDate_To = Request.QueryString["RequisitionDate_To"];
            //string ProductGroup = Request.QueryString["ProductGroup"];
            //string Size = Request.QueryString["Size"];

            //if (Region=="เลือกทั้งหมด")
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
            //if (ProductGroup == "เลือกทั้งหมด")
            //{
            //    ProductGroup = string.Empty;
            //}
            //if (Size == "เลือกทั้งหมด")
            //{
            //    Size = string.Empty;
            //}

            //GenerateReport(RPT, PRM, Region, AgentName, SPName, RequisitionDate_From, RequisitionDate_To, ProductGroup, Size);

        }

    }
    protected void GenerateReport(string RPT, string PRM, string Region, string AgentName, string SPName, string RequisitionDate_From, string RequisitionDate_To, string ProductGroup, string Size)
    {
        try
        {
            ReportDocument cryRpt = new ReportDocument();

            switch (RPT)
            {
                case "ReportSaleSummaryByProductGroup":
                    ReportViewer1.ID = "รายงานสรุปยอดขายแยกตามกลุ่มสินค้า_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    
                    List<RPT_SUMM_SO_PG_41214> rt_RPT_Order = new List<RPT_SUMM_SO_PG_41214>();
                   // rt_RPT_Order = Reports.RPT_SUMM_SO_PG_41214(string.Empty,string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    rt_RPT_Order = Reports.RPT_SUMM_SO_PG_41214(Region, AgentName,SPName
                        , RequisitionDate_From, RequisitionDate_To, ProductGroup,Size,string.Empty,string.Empty,string.Empty);


                    if (rt_RPT_Order.Count > 0)
                    {
                        foreach (RPT_SUMM_SO_PG_41214 r in rt_RPT_Order)
                        {
                            r.paramRegion_Name = Request.QueryString["Region"];
                            r.paramCV_Name = Request.QueryString["AgentFullName"];
                            r.paramSP_ID = Request.QueryString["SPName"]; 
                            r.paramSP_Name = Request.QueryString["SPFullName"].Replace(Request.QueryString["SPName"],"");
                            r.paramStartDate = Request.QueryString["RequisitionDate_From"]; 
                            r.paramEndDate = Request.QueryString["RequisitionDate_To"]; 
                            r.paramProd_Group = Request.QueryString["ProductGroup"]; 
                            r.paramSize = Request.QueryString["Size"]; ;
                        }
                    }
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportSaleSummaryByProductGroup.rpt"));
                    cryRpt.SetDataSource(rt_RPT_Order);
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
}