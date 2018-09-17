using CrystalDecisions.CrystalReports.Engine;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_From_ViewsReport : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string RPT = Request.QueryString["RPT"];
            string PRM = Request.QueryString["PRM"];

            string Region = Request.QueryString["Region"];
            string AgentName = Request.QueryString["AgentName"];
            string Month_From = Request.QueryString["Month_From"];
            string Month_To = Request.QueryString["Month_To"];
            string Year = Request.QueryString["Year"];
            string ProductGroup = Request.QueryString["ProductGroup"];
            string Size = Request.QueryString["Size"];
            string Unit = Request.QueryString["Unit"];
            string SPName = Request.QueryString["SPName"];
            string CV_Code = Request.QueryString["CV_Code"];
            string ProductName = Request.QueryString["ProductName"];
            string Temp1 = Request.QueryString["Temp1"];

            GenerateReport(RPT, PRM, Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, SPName, CV_Code, ProductName, Temp1);
        }
    }

         protected void GenerateReport(string RPT, string PRM, string Region, string AgentName, string Month_From, string Month_To, string Year, string ProductGroup, string Size, string Unit, string SPName, string CV_Code, string ProductName, string Temp1)
    {
        try
        {
            ReportDocument cryRpt = new ReportDocument();
            string User_ID = Request.Cookies["User_ID"].Value;

            switch (RPT)
            {
                case "Requisition_No":


                    List<RPT_Get_Requisition_No> rt_rqt = new List<RPT_Get_Requisition_No>();
                    dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(CV_Code);
                    //dbo_RequisitionClearingClass rq_cl = dbo_RequisitionClearingDataClass.Search(string.Empty, string.Empty, string.Empty,null,null,null,null,string.Empty);
                    List<dbo_RequisitionClearingClass> item = dbo_RequisitionClearingDataClass.Search(string.Empty, string.Empty, string.Empty, null, null, null, null, CV_Code);


                    if (PRM == null)
                    {
                        rt_rqt = Reports.RPT_Get_Requisition_No_Search(PRM, SPName.Substring(0, 11));



                        logger.Debug("SPName " + SPName);

                        if (SPName != "==ระบุ==")
                        {

                            // foreach (var d in rt_rqt)
                            // {
                            //     d.AgentName = AgentName;
                            //     d.sp_name = SPName.Substring(11);
                            //     d.User_SP = SPName.Substring(0, 10);
                            //     d.Invoice_Address = clsdbo_Agent.Adress_Agent;
                            //     d.Tax_ID = clsdbo_Agent.Tax_ID;
                            // }

                            logger.Debug("before sending report");
                            cryRpt.Load(Server.MapPath("~/Report_From/RT_Requisition_DOC001.rpt"));
                            cryRpt.SetDataSource(rt_rqt);

                        }
                        else
                        {
                            // Response.Redirect("กรุณาเลือกพนักงานขาย");
                            string script = "alert(\"กรุณาเลือกพนักงานขาย\");";
                            ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                        }

                    }
                    else
                    {
                        rt_rqt = Reports.RPT_Get_Requisition_No_Search(PRM, string.Empty);
                        int rqt_clm_tmp = rt_rqt.Where(f => f.clm_6 != 0 || f.clm_7 != 0 || f.clm_8 != 0 || f.clm_9 != 0 || f.clm_10 != 0).Count();

                        if (rqt_clm_tmp != 0)
                        {
                            cryRpt.Load(Server.MapPath("~/Report_From/RT_Requisition_DOC002.rpt"));
                            cryRpt.SetDataSource(rt_rqt);
                        }
                        else
                        {
                            cryRpt.Load(Server.MapPath("~/Report_From/RT_Requisition_DOC001.rpt"));
                            cryRpt.SetDataSource(rt_rqt);
                        }


                    }

                    break;
                case "PO_Number":
                    List<RPT_Get_PO_Number> rt_po = new List<RPT_Get_PO_Number>();
                    rt_po = Reports.RPT_Get_PO_Number_Search(PRM);
                    cryRpt.Load(Server.MapPath("~/Report_From/RT_PO_DOC001.rpt"));
                    cryRpt.SetDataSource(rt_po);
                    break;
                case "Clearing_No":
                    List<RPT_Get_Clearing_No> rt_clearing = new List<RPT_Get_Clearing_No>();
                    rt_clearing = Reports.RPT_Get_Clearing_No(PRM);
                    cryRpt.Load(Server.MapPath("~/Report_From/RT_Clearing_DOC002.rpt"));
                    cryRpt.SetDataSource(rt_clearing);

                    ReportDocument subRepDoc = cryRpt.Subreports["RPT_Subsidy.rpt"];
                    List<RPT_Get_Clearing_No_Subsidy> rt_subsidy = new List<RPT_Get_Clearing_No_Subsidy>();
                    rt_subsidy = Reports.RPT_Get_Clearing_No_Subsidy(PRM);
                    subRepDoc.SetDataSource(rt_subsidy);

                    ReportDocument subRepDoc1 = cryRpt.Subreports["RPT_Deduct.rpt"];
                    List<RPT_Get_Clearing_No_Deduct> rt_deduct = new List<RPT_Get_Clearing_No_Deduct>();
                    rt_deduct = Reports.RPT_Get_Clearing_No_Deduct(PRM);
                    subRepDoc1.SetDataSource(rt_deduct);
                    break;

                case "StockOnHandList":
                    if (Temp1 == "Temp1")
                    {
                        List<RPT_COUNT_STOCK_FORM_5> rt_StockOnHandList = new List<RPT_COUNT_STOCK_FORM_5>();
                        rt_StockOnHandList = Reports.RPT_COUNT_STOCK_FORM_5(null, CV_Code, null, null, null, null, null, null, null, null);
                        cryRpt.Load(Server.MapPath("~/Report_From/RT_CountStock.rpt"));
                        cryRpt.SetDataSource(rt_StockOnHandList);
                    }
                    else
                    {
                        List<RPT_COUNT_STOCK_FORM_5> rt_StockOnHandList = new List<RPT_COUNT_STOCK_FORM_5>();
                        rt_StockOnHandList = Reports.RPT_COUNT_STOCK_FORM_5(null, null, PRM, null, null, null, null, null, null, null);
                        cryRpt.Load(Server.MapPath("~/Report_From/RT_CountStock.rpt"));
                        cryRpt.SetDataSource(rt_StockOnHandList);
                    }


                    break;

                case "CommissionReqList":
                    // List<RPT_COUNT_STOCK_FORM_5> rt_StockOnHandList = new List<RPT_COUNT_STOCK_FORM_5>();
                    List<RPT_RQ_CASH_FORM_4> rt_RQ_CASH_FORM_4 = new List<global::RPT_RQ_CASH_FORM_4>();
                    rt_RQ_CASH_FORM_4 = Reports.RPT_RQ_CASH_FORM_4(null, null, PRM, null, null, null, null, null, null, User_ID);
                    cryRpt.Load(Server.MapPath("~/Report_From/RT_CM_Requisition.rpt"));

                    cryRpt.SetDataSource(rt_RQ_CASH_FORM_4);

                    break;
                case "RPT_Order":
                    //List<RPT_Get_Clearing_No> rt_clearing = new List<RPT_Get_Clearing_No>();
                    //List<RPT_AnnualReport_4121> rt_RPT_Order = new List<RPT_AnnualReport_4121>();
                    //rt_clearing = Reports.RPT_Get_Clearing_No(PRM);
                    //rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region,AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrders.rpt"));
                    //cryRpt.SetDataSource(rt_RPT_Order);
                    break;

                case "ReportOrdersGroupProducts":
                    //List<RPT_AnnualReport_4121> rt_RPT_Order = new List<RPT_AnnualReport_4121>();

                    //rt_clearing = Reports.RPT_Get_Clearing_No(PRM);
                    //rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit);

                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrdersGroupProducts.rpt"));
                    //cryRpt.SetDataSource(rt_RPT_Order);
                    break;

                case "ReportOrdersYearlyTarget":

                    //List<RPT_AnnualReport_4121> rt_RPT_Order = new List<RPT_AnnualReport_4121>();

                    //rt_clearing = Reports.RPT_Get_Clearing_No(PRM);
                    //rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit);

                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrdersYearlyTarget.rpt"));
                    //cryRpt.SetDataSource(rt_RPT_Order);
                    break;
                case "Other_Requisition_No":

                    List<RPT_Get_OtherRequisition_No> rt_other = new List<RPT_Get_OtherRequisition_No>();
                    rt_other = Reports.RPT_Get_OtherRequisition_No(PRM);
                    cryRpt.Load(Server.MapPath("~/Report_From/RT_OtherRequisition_DOC.rpt"));
                    cryRpt.SetDataSource(rt_other);

                    break;
            }

            // List<RT_PO_DOC> rt_doc = new List<RT_PO_DOC>();

            
            if(cryRpt != null)
            {

                BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
                Response.Flush();
                Response.Close();
            }
            else
            {
                
            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            //throw ex;
        }
    }
}