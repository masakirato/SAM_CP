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
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Report_ReportViewer : System.Web.UI.Page
{
    #region Private Variable
    ConnectionManager connDB = new ConnectionManager();
    private string RPTName;
    private string PRM;
    private string Region;
    private string AgentName;
    private string Month_From;
    private string Month_To;
    private string Year;
    private string ProductGroup;
    private string Size;
    private string Unit;
    private string SPName;
    private string CV_Code;
    private string ProductName;
    private string ProductID;
    private string InvoiceDate;
    private string InvoiceDateTo;
    private string CountStockDate_From;
    private string CountStockDate_To;
    private string RequisitionName;
    private string Reason;
    private string User_ID;
    private string ResidenceType;
    private string Status;
    private string CustomerType;
    private string CustomerName;
    string CustomerID;
    string PaymentType;
    string MonthGroup;

    dbo_UserClass user_class;
    dbo_AgentClass _agent;
    #endregion

    #region Control Events
    protected void Page_Init(object sender, EventArgs e)
    {
        GenerateReport();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {         
            //GenerateReport();
        }
        else
        {
            //if (Session["cryRpt"] != null)
            //{
            //    cryRpt = (ReportDocument)Session["cryRpt"];

            //    ReportViewer.ReportSource = cryRpt;
            //    ReportViewer.RefreshReport();
            //}         
        }
    }

    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    if (Session["cryRpt"] != null)
    //    {
    //        Session.Remove("cryRpt");
    //    }

    //    Session.Add("cryRpt", cryRpt);
    //}

    #endregion

    #region Methods
    private void GenerateReport()
    {
        RPTName = Request.QueryString["RPT"];
        PRM = Request.QueryString["PRM"];
        Region = Request.QueryString["Region"];
        AgentName = Request.QueryString["AgentName"];
        Month_From = Request.QueryString["Month_From"];
        Month_To = Request.QueryString["Month_To"];
        Year = Request.QueryString["Year"];
        ProductGroup = Request.QueryString["ProductGroup"];
        Size = Request.QueryString["Size"];
        Unit = Request.QueryString["Unit"];
        SPName = Request.QueryString["SPName"];
        CV_Code = Request.QueryString["CV_Code"];
        ProductName = Request.QueryString["ProductName"];
        ProductID = Request.QueryString["ProductID"];
        InvoiceDate = Request.QueryString["InvoiceDate"];
        InvoiceDateTo = Request.QueryString["InvoiceDateTo"];
        CountStockDate_From = Request.QueryString["CountStockDate_From"];
        CountStockDate_To = Request.QueryString["CountStockDate_To"];
        RequisitionName = Request.QueryString["RequisitionName"];
        Reason = Request.QueryString["Reason"];
        User_ID = Request.QueryString["User_ID"];
        ResidenceType = Request.QueryString["ResidenceType"];
        Status = Request.QueryString["Status"];
        CustomerType = Request.QueryString["CustomerType"];
        CustomerName = Request.QueryString["CustomerName"];
        CustomerID = Request.QueryString["CustomerID"];
        PaymentType = Request.QueryString["PaymentType"];
        MonthGroup = Request.QueryString["MonthGroup"];

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
        if (RequisitionName == "เลือกทั้งหมด")
        {
            RequisitionName = string.Empty;
        }
        if (Reason == "เลือกทั้งหมด")
        {
            Reason = string.Empty;
        }

        if (CustomerType == "เลือกทั้งหมด")
        {
            CustomerType = string.Empty;
        }
        else if (CustomerType == "สมาชิก")
        {
            CustomerType = "1";
        }
        else if (CustomerType == "ทั่วไป")
        {
            CustomerType = "0";
        }
        if (ResidenceType == "เลือกทั้งหมด")
        {
            ResidenceType = string.Empty;
        }
        if (Status == "เลือกทั้งหมด")
        {
            Status = string.Empty;
        }
        else if (Status == "ยังติดต่ออยู่")
        {
            Status = "1";
        }
        else if (Status == "ระงับการส่งชั่วคราว")
        {
            Status = "2";
        }
        else if (Status == "ขาดการติดต่อ")
        {
            Status = "3";
        }

        if (PaymentType == "เลือกทั้งหมด")
        {
            PaymentType = string.Empty;
        }
        else if (PaymentType == "เงินสด")
        {
            PaymentType = "0";
        }
        else if (PaymentType == "เครดิต")
        {
            PaymentType = "1";
        }

        if (CustomerID == "เลือกทั้งหมด")
        {
            CustomerID = string.Empty;
        }
        if (MonthGroup == "เลือกทั้งหมด")
        {
            MonthGroup = string.Empty;
        }
        string datetime = DateTime.Now.ToShortDateString();
        string Time = DateTime.Now.ToShortTimeString();

        switch (RPTName)
        {
            case "RPT_Order":
                ReportViewer.ID = "รายงานยอดซื้อประจำปี_" + datetime.Replace("/",string.Empty)+"_"+ Time.Replace(":",string.Empty);
                DisplayReportOrders();
                break;
            case "ReportOrdersGroupProducts":
                ReportViewer.ID = "รายงานยอดสั่งซื้อรายสินค้า_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty); 
                DisplayReportOrdersGroupProducts();
                break;
            case "ReportOrdersYearlyTarget":
                ReportViewer.ID = "รายงานยอดสั่งซื้อเทียบเป้ารายปี_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportOrdersYearlyTarget();
                break;
            case "ReportCustomerPayoutType":
                ReportViewer.ID = "รายงานประเภทการจ่ายเงินของลูกค้า_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportCustomerPayoutType();
                break;

            case "ReportSummaryPickingSPList":
                ReportViewer.ID = "รายงานสรุปเบิกสินค้ารายพนักงาน_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportSummaryPickingSPList();
                break;
            case "ReportProductMovement":
                ReportViewer.ID = "รายงานการเคลื่อนไหวสินค้า_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportProductMovement();
                break;          
            case "ReportProductAdjustStock":
                ReportViewer.ID = "รายงานการปรับสต๊อกสินค้า_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportProductAdjustStock();
                break;

            case "ReportSummaryPickingOtherProduct":
                ReportViewer.ID = "รายงานสรุปการเบิกสินค้าอื่นๆ_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportSummaryPickingOtherProduct();
                break;

            case "ReportCustomerType":
                ReportViewer.ID = "รายงานสรุปข้อมูลสมาชิก_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportCustomerType();
                break;

            case "ReportCustomerDetails":
                ReportViewer.ID = "รายงานรายละเอียดลูกค้า_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportCustomerDetails();
                break;
            case "ReportCustomerBirthday":
                ReportViewer.ID = "รายงานวันเกิดลูกค้าในแต่ละเดือน_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                DisplayReportCustomerBirthday();
                break;
        }
    }

    private void DisplayReportOrders()
    {
        ReportDocument cryRpt = new ReportDocument();
        List<RPT_AnnualReport_4121> rt_RPT_Order = new List<RPT_AnnualReport_4121>();
        //rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);

        user_class = dbo_UserDataClass.Select_Record(User_ID);
        List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

        if (user_class.User_Group_ID == "CP Meiji")
        {
            string region = user_class.Region;
            string[] regions = region.Split(',');

            List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));
            List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

            if (cv_code1.Count > 0)
            {
                if (AgentName == string.Empty)
                {
                    foreach (dbo_AgentClass _cv in cv_code1)
                    {
                        List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(Region, _cv.CV_Code, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);

                        foreach (RPT_AnnualReport_4121 rpt in _inrpt)
                        {
                            rt_RPT_Order.Add(rpt);
                        }

                    }
                }
                else
                {
                    foreach (dbo_AgentClass _cv in cv_code1)
                    {
                        List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);

                        foreach (RPT_AnnualReport_4121 rpt in _inrpt)
                        {
                            rt_RPT_Order.Add(rpt);
                        }

                    }
                }

                   
            }
            else
            {

                if (Region == string.Empty)
                {
                    foreach (dbo_AgentClass _cv in cv_code2)
                    {
                        List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(_cv.Location_ID, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);

                        foreach (RPT_AnnualReport_4121 rpt in _inrpt)
                        {
                            rt_RPT_Order.Add(rpt);
                        }

                    }
                }
                else
                {
                    foreach (dbo_AgentClass _cv in cv_code2)
                    {
                        List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);

                        foreach (RPT_AnnualReport_4121 rpt in _inrpt)
                        {
                            rt_RPT_Order.Add(rpt);
                        }

                    }
                }
                   
            }

           
        }
        else
        {
            rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, Year, ProductGroup, Size, Unit, string.Empty);
        }
        
        foreach (var d in rt_RPT_Order)
        {

            if (Region == "")
            {
                d.paramRegion_Name = "เลือกทั้งหมด";
            }
            else
            {
                d.paramRegion_Name = CV_RegionName(Region);
            }
            if (AgentName != "")
            {
                //user_class = dbo_UserDataClass.Select_Record(AgentName);
                //d.pramAgent_Name = user_class.AgentName;
                _agent = dbo_AgentDataClass.Select_Record(AgentName);
                d.pramAgent_Name = _agent.AgentName;
            }
            else
            {
                d.pramAgent_Name = "เลือกทั้งหมด";
            }
            d.pramMonth_From = CV_Month(Month_From);
            d.pramMonth_To = CV_Month(Month_To);
            d.pramYear = Year;

            if (ProductGroup == "")
            {
                d.pramProduct_Group = "เลือกทั้งหมด";
            }
            else
            {
                d.pramProduct_Group = ProductGroup;
            }

            if (Size == "")
            {
                d.pramSize = "เลือกทั้งหมด";
            }
            else
            {
                d.pramSize = Size;
            }

            if (Unit == "จำนวนเงิน")
            {
                d.pramUnitType = "บาท";
            }
            else if (Unit == "จำนวนหน่วย")
            {
                d.pramUnitType = "หน่วย";
            }
        }

        cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrders.rpt"));
        cryRpt.SetDataSource(rt_RPT_Order);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();       
    }

    private void DisplayReportOrdersGroupProducts()
    {
        ReportDocument cryRpt = new ReportDocument();
        List<RPT_SO_BY_PRODUCT_4122> rt_RPT_SO_BY_PRODUCT = new List<RPT_SO_BY_PRODUCT_4122>();
        rt_RPT_SO_BY_PRODUCT = Reports.RPT_SO_BY_PRODUCT_4122(Region, AgentName, InvoiceDate, InvoiceDateTo, ProductGroup, Size, ProductID, ProductName, string.Empty, string.Empty);
        if (rt_RPT_SO_BY_PRODUCT.Count > 0)
        {
            foreach (RPT_SO_BY_PRODUCT_4122 r in rt_RPT_SO_BY_PRODUCT)
            {
                r.paramRegion = Request.QueryString["Region"];
                r.paramAgent = Request.QueryString["AgentFullName"];
                if (Request.QueryString["InvoiceDate"] != "")
                {
                    r.paramDateFrom = Request.QueryString["InvoiceDate"];
                }
                else
                {
                    r.paramDateFrom = "-";
                }
                if (Request.QueryString["InvoiceDateTo"] != "")
                {
                    r.paramDateTo = Request.QueryString["InvoiceDateTo"];
                }
                else
                {
                    r.paramDateTo = "-";
                }
                r.paramProductGroup = Request.QueryString["ProductGroup"];
                r.paramSize = Request.QueryString["Size"];
                r.paramProductName = Request.QueryString["ProductName"];
            }
        }

        cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrdersGroupProducts.rpt"));
        cryRpt.SetDataSource(rt_RPT_SO_BY_PRODUCT);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }

    private void DisplayReportOrdersYearlyTarget()
    {
        ReportDocument cryRpt = new ReportDocument();
        List<RPT_SO_TARGET_YEAR_4125> rt_RPT_TARGET_YEAR_4125 = new List<RPT_SO_TARGET_YEAR_4125>();
        rt_RPT_TARGET_YEAR_4125 = Reports.RPT_SO_TARGET_YEAR_4125(Region, AgentName, Year, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        if (rt_RPT_TARGET_YEAR_4125.Count > 0)
        {
            foreach (RPT_SO_TARGET_YEAR_4125 r in rt_RPT_TARGET_YEAR_4125)
            {
                r.paramRegion = Request.QueryString["Region"];
                r.paramAgent = Request.QueryString["AgentFullName"];
                r.paramYear = Request.QueryString["Year"];
            }
        }

        cryRpt.Load(Server.MapPath("~/Report/RT_ReportOrdersYearlyTarget.rpt"));
        cryRpt.SetDataSource(rt_RPT_TARGET_YEAR_4125);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }

    private void DisplayReportSummaryPickingSPList()
    {
        ReportDocument cryRpt = new ReportDocument();
        List<RPT_SUMM_RQ_SP_4126> rt_RPT_SUMM_RQ_SP = new List<RPT_SUMM_RQ_SP_4126>();
        rt_RPT_SUMM_RQ_SP = Reports.RPT_SUMM_RQ_SP_4126(Region, AgentName, SPName, InvoiceDate, InvoiceDateTo, ProductGroup, Size, string.Empty, string.Empty, string.Empty);

        if (rt_RPT_SUMM_RQ_SP.Count > 0)
        {
            foreach (RPT_SUMM_RQ_SP_4126 r in rt_RPT_SUMM_RQ_SP)
            {
                r.paramRegion = Request.QueryString["Region"];
                r.paramAgent = Request.QueryString["AgentFullName"];
                r.paramSP = Request.QueryString["SPFullName"];
                if (Request.QueryString["InvoiceDate"] != "")
                {
                    r.paramDateFrom = Request.QueryString["InvoiceDate"];
                }
                else
                {
                    r.paramDateFrom = "-";
                }
                if (Request.QueryString["InvoiceDateTo"] != "")
                {
                    r.paramDateTo = Request.QueryString["InvoiceDateTo"];
                }
                else
                {
                    r.paramDateTo = "-";
                }
                r.paramProductGroup = Request.QueryString["ProductGroup"];
                r.paramSize = Request.QueryString["Size"];
            }
        }

        cryRpt.Load(Server.MapPath("~/Report/RT_ReportSummaryPickingSPList.rpt"));
        cryRpt.SetDataSource(rt_RPT_SUMM_RQ_SP);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();
       
        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();             

    }
    private void DisplayReportProductMovement()
    {
        ReportDocument cryRpt = new ReportDocument();
        List<RPT_STOCK_MOV_4127> rt_RPT_STOCK_MOV = new List<RPT_STOCK_MOV_4127>();
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

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }

    private void DisplayReportProductAdjustStock()
    {
        ReportDocument cryRpt = new ReportDocument();
        List<RPT_ADJ_STOCK__4128> rt_RPT_ADJ_STOCK = new List<RPT_ADJ_STOCK__4128>();

        string User_ID = Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
        List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

        if (user_class.User_Group_ID == "CP Meiji")
        {
            string region = user_class.Region;
            string[] regions = region.Split(',');
            //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

            List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));
            foreach (dbo_AgentClass _cv in cv_code2)
            {
                //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);


                foreach (RPT_ADJ_STOCK__4128 rpt in _inrpt)
                {
                    rt_RPT_ADJ_STOCK.Add(rpt);
                }

            }
        }
        else
        {
            rt_RPT_ADJ_STOCK = Reports.RPT_ADJ_STOCK__4128(string.Empty, CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        if (rt_RPT_ADJ_STOCK.Count > 0)
        {
            foreach (RPT_ADJ_STOCK__4128 r in rt_RPT_ADJ_STOCK)
            {
                if (Request.QueryString["CountStockDate_From"] != "")
                {
                    r.paramDateFrom = Request.QueryString["CountStockDate_From"];
                }
                else
                {
                    r.paramDateFrom = "-";
                }
                if (Request.QueryString["CountStockDate_To"] != "")
                {
                    r.paramDateTo = Request.QueryString["CountStockDate_To"];
                }
                else
                {
                    r.paramDateTo = "-";
                }
                r.paramProductGroup = Request.QueryString["ProductGroup"];
                r.paramSize = Request.QueryString["Size"];
            }
        }

        cryRpt.Load(Server.MapPath("~/Report/RT_ReportProductAdjustStock.rpt"));
        cryRpt.SetDataSource(rt_RPT_ADJ_STOCK);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }
    private void DisplayReportSummaryPickingOtherProduct()
    {
        ReportDocument cryRpt = new ReportDocument();
        List<RPT_SUMM_RQ_OTHER__4129> rt_RPT_SUMM_RQ_OTHER = new List<RPT_SUMM_RQ_OTHER__4129>();
        string User_ID = Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
        if (user_class.User_Group_ID == "CP Meiji")
        {
            rt_RPT_SUMM_RQ_OTHER = Reports.RPT_SUMM_RQ_OTHER__4129(string.Empty, string.Empty, CountStockDate_From, CountStockDate_To
            , ProductName, RequisitionName, Reason, string.Empty, string.Empty, string.Empty);
        }
        else
        {
            rt_RPT_SUMM_RQ_OTHER = Reports.RPT_SUMM_RQ_OTHER__4129(string.Empty, user_class.CV_CODE,CountStockDate_From, CountStockDate_To
            , ProductName, RequisitionName, Reason, string.Empty, string.Empty, string.Empty);
        }
        if (rt_RPT_SUMM_RQ_OTHER.Count > 0)
        {
            foreach (RPT_SUMM_RQ_OTHER__4129 r in rt_RPT_SUMM_RQ_OTHER)
            {
                if (Request.QueryString["CountStockDate_From"] != "")
                {
                    r.paramDateFrom = Request.QueryString["CountStockDate_From"];
                }
                else
                {
                    r.paramDateFrom = "-";
                }
                if (Request.QueryString["CountStockDate_To"] != "")
                {
                    r.paramDateTo = Request.QueryString["CountStockDate_To"];
                }
                else
                {
                    r.paramDateTo = "-";
                }
                r.paramProductGroup = Request.QueryString["product_name"];
                r.paramSP = Request.QueryString["requisition_name"];
                r.paramReason = Request.QueryString["Reason"];
            }
        }

        cryRpt.Load(Server.MapPath("~/Report/RT_ReportSummaryPickingOtherProduct.rpt"));
        cryRpt.SetDataSource(rt_RPT_SUMM_RQ_OTHER);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }

    private void DisplayReportCustomerBirthday()
    {
        ReportDocument cryRpt = new ReportDocument();

        List<RPT_Customer_BIRTH_DATE_41213> rt_RPT_BIRTH_DATE = new List<RPT_Customer_BIRTH_DATE_41213>();
        rt_RPT_BIRTH_DATE = Reports.RPT_Customer_BIRTH_DATE_41213(Region, AgentName, MonthGroup, string.Empty
            , string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        foreach (var d in rt_RPT_BIRTH_DATE)
        {
            d.paramRegion_Name = Request.QueryString["RegionName"];
            d.paramCV_Name = Request.QueryString["AgentFullName"];
            d.paramBirth_Month = Request.QueryString["MonthGroup"];
        }
        cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerBirthday.rpt"));
        cryRpt.SetDataSource(rt_RPT_BIRTH_DATE);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }

    private void DisplayReportCustomerDetails()
    {
        ReportDocument cryRpt = new ReportDocument();

        List<RPT_Customer_INFO_41211> rt_RPT_Customer_INFO = new List<RPT_Customer_INFO_41211>();

        rt_RPT_Customer_INFO = Reports.RPT_Customer_INFO_41211(Region, AgentName, CustomerType, CustomerName
            , SPName, ResidenceType, Status, string.Empty, string.Empty, string.Empty);

        if (rt_RPT_Customer_INFO.Count > 0)
        {
            foreach (RPT_Customer_INFO_41211 r in rt_RPT_Customer_INFO)
            {
                r.paramRegion = Request.QueryString["Region"];
                r.paramAgent = Request.QueryString["AgentFullName"];
                r.paramCustomerType = Request.QueryString["CustomerType"];
                r.paramCustomerName = Request.QueryString["CustomerName"];
                r.paramSPName = Request.QueryString["SPFullName"];
                r.paramResidenceType = Request.QueryString["ResidenceTypeFullName"];
                r.paramStatus = Request.QueryString["Status"];
            }
        }

        cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerDetails.rpt"));
        cryRpt.SetDataSource(rt_RPT_Customer_INFO);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }

    private void DisplayReportCustomerType()
    {
        ReportDocument cryRpt = new ReportDocument();
        List<RPT_SUMM_MEMBER__41210> rt_RPT_SUMM_MEMBER = new List<RPT_SUMM_MEMBER__41210>();

        rt_RPT_SUMM_MEMBER = Reports.RPT_SUMM_MEMBER__41210(Region, AgentName, string.Empty, string.Empty, string.Empty
            , string.Empty, string.Empty, string.Empty, string.Empty, User_ID);

        cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerType.rpt"));
        cryRpt.SetDataSource(rt_RPT_SUMM_MEMBER);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }

    private void DisplayReportCustomerPayoutType()
    {
        #region Old
        //ReportDocument cryRpt = new ReportDocument();
        //List<RPT_Customer_PAY_TYPE_41212> rt_RPT_PAY_TYPE = new List<RPT_Customer_PAY_TYPE_41212>();
        //rt_RPT_PAY_TYPE = Reports.RPT_Customer_PAY_TYPE_41212(Region, AgentName, CustomerID, CustomerName, SPName, string.Empty, Status, PaymentType, string.Empty, string.Empty, string.Empty);

        //foreach (var d in rt_RPT_PAY_TYPE)
        //{
        //    d.paramRegion_Name = Request.QueryString["Region"];
        //    d.paramCV_Name = Request.QueryString["AgentName"];
        //    d.Customer_Name = Request.QueryString["CustomerName"];
        //    d.paramSP_Name = Request.QueryString["SPName"];
        //    d.paramStatus = Request.QueryString["Status"];
        //    d.paramCustomer_ID = Request.QueryString["CustomerID"];
        //    d.paramPayType = Request.QueryString["PaymentType"];
        //}
        //cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerPayoutType.rpt"));
        //cryRpt.SetDataSource(rt_RPT_PAY_TYPE);

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();    
        #endregion
        ReportDocument cryRpt = new ReportDocument();

        List<RPT_Customer_PAY_TYPE_41212> rt_RPT_PAY_TYPE = new List<RPT_Customer_PAY_TYPE_41212>();
        rt_RPT_PAY_TYPE = Reports.RPT_Customer_PAY_TYPE_41212(Region, AgentName, CustomerID, CustomerName, SPName, string.Empty, Status, PaymentType, string.Empty, string.Empty, string.Empty);
        foreach (var d in rt_RPT_PAY_TYPE)
        {
            d.paramRegion_Name = Request.QueryString["Region"];
            d.paramCV_Name = Request.QueryString["AgentFullName"];
            //if(d.paramCV_Name != "")
            //{
            //    user_class = dbo_UserDataClass.Select_Record(d.paramCV_Name);
            //    d.paramCV_Name = user_class.AgentName;
            //}
            d.paramCustomer_Name = Request.QueryString["CustomerName"];
            d.paramSP_Name = Request.QueryString["SPFullName"];
            d.paramStatus = Request.QueryString["Status"];
            d.paramCustomer_ID = Request.QueryString["CustomerID"];
            d.paramPayType = Request.QueryString["PaymentTypeName"];
        }
        cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerPayoutType.rpt"));
        cryRpt.SetDataSource(rt_RPT_PAY_TYPE);

        ReportViewer.ToolPanelView = ToolPanelViewType.None;
        ReportViewer.HasCrystalLogo = false;
        //this.ReportViewer.PrintMode = PrintMode.ActiveX;
        ReportViewer.ReportSource = cryRpt;
        ReportViewer.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)).ToArray());
        //Response.Flush();
        //Response.Close();        
    }

    public DataSet GetDataReportOrdersGroupProducts()
    {
        DataSet ds = new DataSet();
        connDB.openConnection();

        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Region", ""));
        parameters.Add(new SqlParameter("@CV_Code", ""));
        parameters.Add(new SqlParameter("@StartMonth", ""));
        parameters.Add(new SqlParameter("@EndMonth", ""));
        parameters.Add(new SqlParameter("@Year", ""));
        parameters.Add(new SqlParameter("@PGroup", ""));
        parameters.Add(new SqlParameter("@Size", ""));
        parameters.Add(new SqlParameter("@Unit", ""));
        parameters.Add(new SqlParameter("@Temp0", ""));
        parameters.Add(new SqlParameter("@UserID", ""));

        ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), System.Data.CommandType.StoredProcedure, "RPT_SO_BY_PRODUCT_4122", parameters.ToArray());

        return ds;
    }
    public DataSet GetDataReportCustomerPayoutType()
    {
        DataSet ds = new DataSet();
        connDB.openConnection();

        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Region", ""));
        parameters.Add(new SqlParameter("@CV_Code", ""));
        parameters.Add(new SqlParameter("@CustID", ""));
        parameters.Add(new SqlParameter("@CustName", ""));
        parameters.Add(new SqlParameter("@SP", ""));
        parameters.Add(new SqlParameter("@Status", ""));
        parameters.Add(new SqlParameter("@PayType", ""));
        parameters.Add(new SqlParameter("@Temp0", ""));
        parameters.Add(new SqlParameter("@Temp1", ""));
        parameters.Add(new SqlParameter("@UserID", ""));

        ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), System.Data.CommandType.StoredProcedure, "RPT_Customer_PAY_TYPE_41212", parameters.ToArray());

        return ds;
    }
    public DataSet GetDataReportSummaryPickingSPList()
    {
        DataSet ds = new DataSet();
        connDB.openConnection();

        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Region", ""));
        parameters.Add(new SqlParameter("@CV_Code", ""));
        parameters.Add(new SqlParameter("@SP_ID", ""));
        parameters.Add(new SqlParameter("@StartDate", null));
        parameters.Add(new SqlParameter("@EndDate", null));
        parameters.Add(new SqlParameter("@PGroup", ""));
        parameters.Add(new SqlParameter("@Size", ""));
        parameters.Add(new SqlParameter("@Temp0", ""));
        parameters.Add(new SqlParameter("@Temp1", ""));
        parameters.Add(new SqlParameter("@UserID", ""));

        ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), System.Data.CommandType.StoredProcedure, "RPT_SUMM_RQ_SP_4126", parameters.ToArray());

        return ds;
    }
    #endregion

    #region CV
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
  
    #endregion
}