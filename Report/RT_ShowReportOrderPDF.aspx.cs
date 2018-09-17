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
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_RT_ShowReportOrderPDF : System.Web.UI.Page
{
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
            string ProductID = Request.QueryString["ProductID"];
            string InvoiceDate = Request.QueryString["InvoiceDate"];
            string InvoiceDateTo = Request.QueryString["InvoiceDateTo"];


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
            if (ProductName == "เลือกทั้งหมด")
            {
                ProductName = string.Empty;
            }
            if (ProductID == "เลือกทั้งหมด")
            {
                ProductID = string.Empty;
            }


            GenerateReport(RPT, PRM, Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, SPName, CV_Code, ProductName, InvoiceDate, InvoiceDateTo, ProductID);

        }
    }
    protected void GenerateReport(string RPT, string PRM, string Region, string AgentName, string Month_From, string Month_To, string Year, string ProductGroup, string Size, string Unit, string SPName, string CV_Code, string ProductName, string InvoiceDate, string InvoiceDateTo, string ProductID)
    {
        try
        {
            ReportDocument cryRpt = new ReportDocument();
            dbo_UserClass user_class;
            string User_ID = Request.Cookies["User_ID"].Value;

            switch (RPT)
            {
                case "RPT_Order":
                    List<RPT_AnnualReport_4121> rt_RPT_Order = new List<RPT_AnnualReport_4121>();
                    //rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);
         
                     user_class = dbo_UserDataClass.Select_Record(User_ID);
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                    if (user_class.User_Group_ID == "CP Meiji")
                    {
                        string region = user_class.Region;
                        string[] regions = region.Split(',');

                        List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);

                            foreach (RPT_AnnualReport_4121 rpt in _inrpt)
                            {
                                rt_RPT_Order.Add(rpt);
                            }

                        }
                    }
                    else
                    {
                        rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);
                    }

                    foreach (var d in rt_RPT_Order)
                    {
                        d.paramRegion_Name = CV_RegionName(Region);
                        if (Region == "")
                        {
                            d.paramRegion_Name = "เลือกทั้งหมด";
                        }
                        if (AgentName != "")
                        {
                            user_class = dbo_UserDataClass.Select_Record(AgentName);
                            d.pramAgent_Name = user_class.AgentName;
                        }
                        else
                        {
                            d.pramAgent_Name = "เลือกทั้งหมด";
                        }
                        d.pramMonth_From = CV_Month(Month_From);
                        d.pramMonth_To = CV_Month(Month_To);
                        d.pramYear = Year;
                        d.pramProduct_Group = ProductGroup;
                        if (ProductGroup == "")
                        {
                            d.pramProduct_Group = "เลือกทั้งหมด";
                        }
                        d.pramSize = Size;
                        if (Size == "")
                        {
                            d.pramSize = "เลือกทั้งหมด";
                        }
                        if(Unit == "จำนวนเงิน")
                        {
                            d.pramUnitType = "บาท";
                        }
                       else if(Unit == "จำนวนหน่วย")
                        {
                            d.pramUnitType = "หน่วย";
                        }                    
                    }

                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrders.rpt"));
                    cryRpt.SetDataSource(rt_RPT_Order);
                    break;

                case "ReportOrdersGroupProducts":
                    List<RPT_SO_BY_PRODUCT_4122> rt_RPT_SO_BY_PRODUCT = new List<RPT_SO_BY_PRODUCT_4122>();
                    rt_RPT_SO_BY_PRODUCT = Reports.RPT_SO_BY_PRODUCT_4122(Region, AgentName, InvoiceDate, InvoiceDateTo, ProductGroup, Size, ProductID, ProductName, string.Empty, string.Empty);
                    //rt_RPT_SO_BY_PRODUCT = Reports.RPT_SO_BY_PRODUCT_4122(string.Empty, AgentName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrdersGroupProducts.rpt"));
                    cryRpt.SetDataSource(rt_RPT_SO_BY_PRODUCT);
                    break;

                case "ReportOrdersYearlyTarget":

                    List<RPT_SO_TARGET_YEAR_4125> rt_RPT_TARGET_YEAR_4125 = new List<RPT_SO_TARGET_YEAR_4125>();
                    rt_RPT_TARGET_YEAR_4125 = Reports.RPT_SO_TARGET_YEAR_4125(Region, AgentName, Year, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                    if (rt_RPT_TARGET_YEAR_4125.Count > 0)
                    {
                        foreach (RPT_SO_TARGET_YEAR_4125 r in rt_RPT_TARGET_YEAR_4125) {
                            r.paramRegion = Request.QueryString["Region"]; ;
                            r.paramAgent = Request.QueryString["AgentFullName"]; ;
                            r.paramYear = Request.QueryString["Year"]; ;
                        }
                    }
                    
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrdersYearlyTarget.rpt"));
                    cryRpt.SetDataSource(rt_RPT_TARGET_YEAR_4125);
                    break;
            }

            // List<RT_PO_DOC> rt_doc = new List<RT_PO_DOC>();

            //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
            //Response.Flush();
            //Response.Close();

            ReportViewer.ToolPanelView = ToolPanelViewType.None;
            ReportViewer.HasCrystalLogo = false;
            //this.ReportViewer.PrintMode = PrintMode.ActiveX;
            ReportViewer.ReportSource = cryRpt;
            ReportViewer.RefreshReport();
        }
        catch (Exception ex)
        {
            //throw ex;
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
    private string CV_RegionName(string Region)
    {
        string strRegionName = "";

        if (Region == "0701")
        {
            strRegionName = "กรุงเทพฯ";
        }
        else if (Region == "0702")
        {
            strRegionName = "กลาง";
        }
        else if (Region == "0703")
        {
            strRegionName = "เหนือ";
        }
        else if (Region == "0704")
        {
            strRegionName = "อีสาน";
        }
        else if (Region == "0705")
        {
            strRegionName = "ตะวันออก";
        }
        else if (Region == "0706")
        {
            strRegionName = "ตะวันตก";
        }
        else if (Region == "0707")
        {
            strRegionName = "ใต้";
        }


        return strRegionName;

    }

}
