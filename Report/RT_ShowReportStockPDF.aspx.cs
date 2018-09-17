using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using log4net;

public partial class Report_RT_ShowReportStockPDF : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string RPT = Request.QueryString["RPT"];
            string PRM = Request.QueryString["PRM"];
            string PRM1 = Request.QueryString["PRM1"];
            string Region = Request.QueryString["Region"];
            string AgentName = Request.QueryString["AgentName"];
            string Month_From = Request.QueryString["Month_From"];
            string Month_To = Request.QueryString["Month_To"];
            string Year = Request.QueryString["Year"];
            string ProductGroup = Request.QueryString["ProductGroup"];
            string Size = Request.QueryString["Size"];
            string Unit = Request.QueryString["Unit"];
            string SPName = Request.QueryString["SPName"];
            string InvoiceDate = Request.QueryString["InvoiceDate"];
            string InvoiceDateTo = Request.QueryString["InvoiceDateTo"];
            string CountStockDate_From = Request.QueryString["CountStockDate_From"];
            string CountStockDate_To = Request.QueryString["CountStockDate_To"];
            string productname = Request.QueryString["productname"];
            string RequisitionName = Request.QueryString["RequisitionName"];
            string Reason = Request.QueryString["Reason"];
            string CV_Code = Request.QueryString["CV_Code"];

            if (Region == "เลือกทั้งหมด")
            {
                Region = string.Empty;
            }
            if (AgentName == "เลือกทั้งหมด")
            {
                AgentName = string.Empty;
            }
            if (ProductGroup == "เลือกทั้งหมด")
            {
                ProductGroup = string.Empty;
            }
            if (Size == "เลือกทั้งหมด")
            {
                Size = string.Empty;
            }
            if (Unit == "เลือกทั้งหมด")
            {
                Unit = string.Empty;
            }
            if (SPName == "เลือกทั้งหมด")
            {
                SPName = string.Empty;
            }
            if (productname == "เลือกทั้งหมด")
            {
                productname = string.Empty;
            }
            if (Reason == "เลือกทั้งหมด")
            {
                Reason = string.Empty;
            }
            if (RequisitionName == "เลือกทั้งหมด")
            {
                RequisitionName = string.Empty;
            }


            GenerateReport(RPT, PRM, Region, AgentName, SPName, InvoiceDate, InvoiceDateTo, ProductGroup, Size
                , CountStockDate_From, CountStockDate_To, productname, RequisitionName, Reason, PRM1, CV_Code);

        }
    }
    protected void GenerateReport(string RPT, string PRM, string Region, string AgentName, string SPName, string InvoiceDate, string InvoiceDateTo, string ProductGroup, string Size, string CountStockDate_From, string CountStockDate_To, string productname, string RequisitionName, string Reason, string PRM1, string CV_Code)
    {
        try
        {
            ReportDocument cryRpt = new ReportDocument();

            switch (RPT)
            {
                case "ReportSummaryPickingSPList":

                    List<RPT_SUMM_RQ_SP_4126> rt_RPT_SUMM_RQ_SP = new List<RPT_SUMM_RQ_SP_4126>();
                    //rt_RPT_SUMM_RQ_SP = Reports.RPT_SUMM_RQ_SP_4126(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,string.Empty);
                    rt_RPT_SUMM_RQ_SP = Reports.RPT_SUMM_RQ_SP_4126(Region, AgentName, SPName, InvoiceDate, InvoiceDateTo, ProductGroup, Size, string.Empty, string.Empty, string.Empty);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportSummaryPickingSPList.rpt"));
                    cryRpt.SetDataSource(rt_RPT_SUMM_RQ_SP);
                    break;

                case "ReportProductMovement":

                    List<RPT_STOCK_MOV_4127> rt_RPT_STOCK_MOV = new List<RPT_STOCK_MOV_4127>();

                    // rt_RPT_STOCK_MOV = Reports.RPT_STOCK_MOV_4127(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty); 
                    rt_RPT_STOCK_MOV = Reports.RPT_STOCK_MOV_4127(string.Empty, CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                    foreach (var d in rt_RPT_STOCK_MOV)
                    {

                        d.pramCV_Code = CV_Code;
                        if (d.pramCV_Code == "")
                        {
                            d.pramCV_Code = "ทั้งหมด";
                        }
                       
                        if (CV_Code == "")
                        {
                            d.pramAgent_Name = "ทั้งหมด";
                        }
                        else
                        {
                            dbo_AgentClass _agent = dbo_AgentDataClass.Select_Record(CV_Code);
                            d.pramAgent_Name = _agent.AgentName;
                        }
                        d.pramDate_From = CountStockDate_From;
                        d.pramDate_To = CountStockDate_To;
                        d.pramProduct_group = ProductGroup;
                        if (d.pramProduct_group == "")
                        {
                            d.pramProduct_group = "เลือกทั้งหมด";
                        }
                        d.pramSize = Size;
                        if (d.pramSize == "")
                        {
                            d.pramSize = "เลือกทั้งหมด";
                        }
                    }

                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportProductMovement.rpt"));
                    cryRpt.SetDataSource(rt_RPT_STOCK_MOV);
                    break;
                case "ReportProductAdjustStock":

                    List<RPT_ADJ_STOCK__4128> rt_RPT_ADJ_STOCK = new List<RPT_ADJ_STOCK__4128>();

                    //rt_RPT_ADJ_STOCK = Reports.RPT_ADJ_STOCK__4128(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);  
                    rt_RPT_ADJ_STOCK = Reports.RPT_ADJ_STOCK__4128(string.Empty, CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportProductAdjustStock.rpt"));
                    cryRpt.SetDataSource(rt_RPT_ADJ_STOCK);
                    break;
                case "ReportSummaryPickingOtherProduct":
                    List<RPT_SUMM_RQ_OTHER__4129> rt_RPT_SUMM_RQ_OTHER = new List<RPT_SUMM_RQ_OTHER__4129>();
                    //rt_RPT_SUMM_RQ_OTHER = Reports.RPT_SUMM_RQ_OTHER__4129(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    rt_RPT_SUMM_RQ_OTHER = Reports.RPT_SUMM_RQ_OTHER__4129(string.Empty, CV_Code, CountStockDate_From, CountStockDate_To, productname, RequisitionName, Reason, string.Empty, string.Empty, string.Empty);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportSummaryPickingOtherProduct.rpt"));
                    cryRpt.SetDataSource(rt_RPT_SUMM_RQ_OTHER);
                    break;

                case "Clearing_No":

                    List<RPT_CLR_SALE_SUM_FORM_1_3> RPT_FORM_1_3 = new List<RPT_CLR_SALE_SUM_FORM_1_3>();
                    RPT_FORM_1_3 = Reports.RPT_CLR_SALE_SUM_FORM_1_3(PRM);

                    cryRpt.Load(Server.MapPath("~/ViewsMockup/RT_SalesSummaryByProduct.rpt"));
                    cryRpt.SetDataSource(RPT_FORM_1_3);
                    break;
                case "Requisition_No":

                    List<RPT_Get_Requisition_No> rt_rqt = new List<RPT_Get_Requisition_No>();
                    rt_rqt = Reports.RPT_Get_Requisition_No_Search(PRM, string.Empty);
                    int rqt_clm_tmp = rt_rqt.Where(f => f.clm_6 != 0 || f.clm_7 != 0 || f.clm_8 != 0 || f.clm_9 != 0 || f.clm_10 != 0).Count();

                    if (rqt_clm_tmp != 0)
                    {
                        cryRpt.Load(Server.MapPath("~/ViewsMockup/RT_Requisition_DOC002.rpt"));
                        cryRpt.SetDataSource(rt_rqt);
                    }
                    else
                    {
                        cryRpt.Load(Server.MapPath("~/ViewsMockup/RT_Requisition_DOC001.rpt"));
                        cryRpt.SetDataSource(rt_rqt);
                    }

                    break;
                //Requisition_No1
                case "Requisition_No1":

                    //List<RPT_Get_Requisition_No> rt_rqt = new List<RPT_Get_Requisition_No>();
                    rt_rqt = Reports.RPT_Get_Requisition_No_Search(PRM1, string.Empty);
                    int rqt_clm_tmp1 = rt_rqt.Where(f => f.clm_6 != 0 || f.clm_7 != 0 || f.clm_8 != 0 || f.clm_9 != 0 || f.clm_10 != 0).Count();

                    if (rqt_clm_tmp1 != 0)
                    {
                        cryRpt.Load(Server.MapPath("~/ViewsMockup/RT_Requisition_DOC002.rpt"));
                        cryRpt.SetDataSource(rt_rqt);
                    }
                    else
                    {
                        cryRpt.Load(Server.MapPath("~/ViewsMockup/RT_Requisition_DOC001.rpt"));
                        cryRpt.SetDataSource(rt_rqt);
                    }

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