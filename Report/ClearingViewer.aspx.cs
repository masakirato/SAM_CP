#region Using
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Report_ClearingViewer : System.Web.UI.Page
{
    #region Private Variable
    ConnectionManager connDB = new ConnectionManager();
    private string RPTName;
    private string PRM;
    private string PRM1;
    private string RQ_N01;
    private string RQ_N02;

    ReportDocument cryRpt = new ReportDocument();
    string User_ID;
    #endregion

    #region Control Events
    protected void Page_Init(object sender, EventArgs e)
    {
        GenerateReport();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region Methods
    private void GenerateReport()
    {
        User_ID = Request.Cookies["User_ID"].Value;
        RPTName = Request.QueryString["RPT"];
        PRM = Request.QueryString["PRM"];
        PRM1 = Request.QueryString["PRM1"];
        RQ_N01 = Request.QueryString["RQ_N01"];
        RQ_N02 = Request.QueryString["RQ_N02"];

        switch (RPTName)
        {
            case "RPT_ClearingSummaryInfo":
                DisplayClearingSummaryInfo();
                break;
        }
    }

    private void DisplayClearingSummaryInfo()
    {
        string fileNameClearing1 = "";
        List<RPT_Get_Clearing_No> rt_clearing = new List<RPT_Get_Clearing_No>();
        rt_clearing = Reports.RPT_Get_Clearing_No(PRM);

        if(rt_clearing.Count > 0)
        {
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

            fileNameClearing1 = Server.MapPath("~/PDF/" + "RT_Clearing_DOC002_" + PRM + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".pdf");
            cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileNameClearing1);
        }
        // -
        string fileNameClearing2 = "";
        List<RPT_CLR_SALE_SUM_FORM_1_3> RPT_FORM_1_3 = new List<RPT_CLR_SALE_SUM_FORM_1_3>();
        RPT_FORM_1_3 = Reports.RPT_CLR_SALE_SUM_FORM_1_3(PRM);

        if (RPT_FORM_1_3.Count > 0)
        {
            cryRpt.Load(Server.MapPath("~/Report_From/RT_SalesSummaryByProduct.rpt"));
            cryRpt.SetDataSource(RPT_FORM_1_3);

            fileNameClearing2 = Server.MapPath("~/PDF/" + "RT_SalesSummaryByProduct_" + PRM + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".pdf");
            cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileNameClearing2);
        }


        // - RQ_N01
        string fileNameRequisition1 = "";
        List<RPT_Get_Requisition_No> rt_rqt = new List<RPT_Get_Requisition_No>();
        rt_rqt = Reports.RPT_Get_Requisition_No_Search(RQ_N01, string.Empty);

        if (rt_rqt.Count > 0)
        {
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

            fileNameRequisition1 = Server.MapPath("~/PDF/" + "RT_Requisition_" + PRM + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".pdf");
            cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileNameRequisition1);
        }



        // - RQ_N02
        string fileNameRequisition2 = "";
        if (RQ_N02 != "")
        {
            List<RPT_Get_Requisition_No> rt_rqt2 = new List<RPT_Get_Requisition_No>();
            rt_rqt2 = Reports.RPT_Get_Requisition_No_Search(RQ_N02, string.Empty);

            if (rt_rqt2.Count > 0)
            {
                int rqt_clm_tmp2 = rt_rqt2.Where(f => f.clm_6 != 0 || f.clm_7 != 0 || f.clm_8 != 0 || f.clm_9 != 0 || f.clm_10 != 0).Count();

                if (rqt_clm_tmp2 != 0)
                {
                    cryRpt.Load(Server.MapPath("~/Report_From/RT_Requisition_DOC002.rpt"));
                    cryRpt.SetDataSource(rt_rqt2);
                }
                else
                {
                    cryRpt.Load(Server.MapPath("~/Report_From/RT_Requisition_DOC001.rpt"));
                    cryRpt.SetDataSource(rt_rqt2);
                }
                fileNameRequisition2 = Server.MapPath("~/PDF/" + "RT_Requisition2_" + PRM + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".pdf");
                cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileNameRequisition2);
            }

        }

        // Create list
        var myList = new List<string>();

        // Convert to array
        var myArray = myList.ToArray();

        if (RQ_N02 != "")
        {
            //myList.Add(fileNameClearing2);
            //myList.Add(fileNameClearing1);
            //myList.Add(fileNameRequisition1);
            //myList.Add(fileNameRequisition2);
            if (fileNameClearing2 != "")
            {
                myList.Add(fileNameClearing2);
            }
            if (fileNameClearing1 != "")
            {
                myList.Add(fileNameClearing1);
            }
            if (fileNameRequisition1 != "")
            {
                myList.Add(fileNameRequisition1);
            }
            if (fileNameRequisition2 != "")
            {
                myList.Add(fileNameRequisition2);
            }
        }
        else
        {
            if(fileNameClearing2 != "")
            {
                myList.Add(fileNameClearing2);
            }
           if(fileNameClearing1 !="")
            {
                myList.Add(fileNameClearing1);
            }
           if(fileNameRequisition1 != "")
            {
                myList.Add(fileNameRequisition1);
            }
            
        }

        string fileName = Server.MapPath("~/PDF/" + "RT_ClearingSummaryInfo_" + PRM + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".pdf");

        string pdfPath = "";
        if (myList.Count > 1)
        {
            PDFMerger.MergePDFs(myList.ToList(), fileName);
            pdfPath = fileName;
            WebClient client = new WebClient();
            Byte[] FileBuffer = client.DownloadData(pdfPath);

            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }
        else if (myList.Count == 1)
        {
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-length", fileName);
            //Response.BinaryWrite(fileName);
            BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            Response.Flush();
            Response.Close();

        }
        else
        {
            Show("ไม่พบข้อมูลการเคลียเงิน");
        }

    }
    public void Show(string message)
    {
       // logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            //logger.Info(message);

            string cleanMessage = message.Replace("'", "\'");
            // Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            //if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            //{
            //  page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception)
        {

        }
        //}
    }
    #endregion
}