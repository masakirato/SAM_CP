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

public partial class Report_ReportViewer_01 : System.Web.UI.Page
{
    ConnectionManager connDB = new ConnectionManager();
    private string RPT;
    private string PRM;
    private string Region;
    private string AgentName;
    private string CustomerType;
    private string CustomerName;
    private string SPName;
    private string ResidenceType;
    private string Status;
    private string User_ID;
    private string CustomerID;
    private string PaymentType;
    private string MonthGroup;

    private string DebtID;
    private string DebtName;
    private string DebtDate_From;
    private string DebtDate_To;
    private string ddlType;
    private string CN_DN;
    private string InvoiceDate;
    private string InvoiceDateTo;

    string MonthGroupTo;
    string Year;
    string CV_CODE;

    dbo_UserClass user_class;
    dbo_AgentClass _agent;
    ReportDocument cryRpt = new ReportDocument();

    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
    }
    protected void GenerateReport()
    {

        RPT = Request.QueryString["RPT"];
        PRM = Request.QueryString["PRM"];
        Region = Request.QueryString["Region"];
        AgentName = Request.QueryString["AgentName"];
        CustomerType = Request.QueryString["CustomerType"];
        CustomerName = Request.QueryString["CustomerName"];
        SPName = Request.QueryString["SPName"];
        ResidenceType = Request.QueryString["ResidenceType"];
        Status = Request.QueryString["Status"];
        User_ID = Request.QueryString["User_ID"];
        CustomerID = Request.QueryString["CustomerID"];
        PaymentType = Request.QueryString["PaymentType"];
        MonthGroup = Request.QueryString["MonthGroup"];

        DebtID = Request.QueryString["DebtID"];
        DebtName = Request.QueryString["DebtName"];
        DebtDate_From = Request.QueryString["DebtDate_From"];
        DebtDate_To = Request.QueryString["DebtDate_To"];
        ddlType = Request.QueryString["ddlType"];
        CN_DN = Request.QueryString["CN_DN"];
        InvoiceDate = Request.QueryString["InvoiceDate"];
        InvoiceDateTo = Request.QueryString["InvoiceDateTo"];

        Year = Request.QueryString["Year"];
        MonthGroupTo = Request.QueryString["MonthGroupTo"];
        CV_CODE = Request.QueryString["CV_Code"];


        if (Region == "เลือกทั้งหมด")
        {
            Region = string.Empty;
        }
        if (AgentName == "เลือกทั้งหมด")
        {
            AgentName = string.Empty;
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
        if (SPName == "เลือกทั้งหมด")
        {
            SPName = string.Empty;
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
        else if (Status == "ชำระครบแล้ว")
        {
            Status = "2";
        }
        else if (Status == "ค้างชำระ")
        {
            Status = "1";
        }
        if (CustomerID == "เลือกทั้งหมด")
        {
            CustomerID = string.Empty;
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
        if (MonthGroup == "เลือกทั้งหมด")
        {
            MonthGroup = string.Empty;
        }
        string datetime = DateTime.Now.ToShortDateString();
        string Time = DateTime.Now.ToShortTimeString();

        //string User_ID = Request.Cookies["User_ID"].Value;

        try
        {
            //ReportDocument cryRpt = new ReportDocument();

            switch (RPT)
            {

                //report Customer
                case "ReportCustomerDetails":
                    DisplayReportCustomerDetails();
                    break;
                case "ReportCustomerPayoutType":
                    DisplayReportCustomerPayoutType();
                    break;
                case "ReportCustomerBirthday":
                    DisplayReportCustomerBirthday();
                    break;
                case "ReportCustomerType":
                    DisplayReportCustomerType();
                    break;

                //ReportDebt
                case "ReportReduceDebt_CustomerAndSP":
                    #region
                    //List<RPT_SP_DEBT_41221> rt_RPT_CUSTOMER_SP = new List<RPT_SP_DEBT_41221>();
                    //rt_RPT_CUSTOMER_SP = Reports.RPT_SP_DEBT_41221(Region, AgentName, SPName, DebtID, DebtName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty);
                    //foreach (var d in rt_RPT_CUSTOMER_SP)
                    //{
                    //    d.paramRegion_Name = Request.QueryString["Region"];
                    //    d.pramAgent_Name = Request.QueryString["AgentName"];
                    //    if (d.pramAgent_Name != "")
                    //    {
                    //        user_class = dbo_UserDataClass.Select_Record(d.pramAgent_Name);
                    //        d.pramAgent_Name = user_class.AgentName;
                    //    }
                    //    else
                    //    {
                    //        d.pramAgent_Name = "เลือกทั้งหมด";
                    //    }
                    //    d.pramSP_Name = Request.QueryString["SPName"];
                    //    d.pramDebt_StartDate = Request.QueryString["DebtDate_From"];
                    //    d.pramDebt_EndDate = Request.QueryString["DebtDate_To"];
                    //    d.pramStatus = Request.QueryString["Status"];
                    //    if (d.pramStatus == "")
                    //    {
                    //        d.pramStatus = "เลือกทั้งหมด";
                    //    }
                    //}

                    //cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_CustomerAndSP.rpt"));
                    //cryRpt.SetDataSource(rt_RPT_CUSTOMER_SP);

                    //CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                    //CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                    //CrystalReportViewer1.ReportSource = cryRpt;
                    #endregion
                    ReportViewer1.ID = "รายงานหนี้ลูกค้ากับพนักงาน_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    DisplayReportReduceDebt_CustomerAndSP();
                    break;
                case "ReportReduceDebt_SPAndAgent":
                    #region
                    //List<RPT_AGENT_DEBT_41222> rt_RPT_AGENT_DEBT = new List<RPT_AGENT_DEBT_41222>();
                    //rt_RPT_AGENT_DEBT = Reports.RPT_AGENT_DEBT_41222(Region, AgentName, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                    //foreach (var d in rt_RPT_AGENT_DEBT)
                    //{
                    //    d.pramDebt_StartDate = Request.QueryString["DebtDate_From"];
                    //    d.pramDebt_EndDate = Request.QueryString["DebtDate_To"];
                    //    d.pramStatus = Request.QueryString["Status"];
                    //}
                    //cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_SPAndAgent.rpt"));
                    //cryRpt.SetDataSource(rt_RPT_AGENT_DEBT);
                    #endregion
                    ReportViewer1.ID = "รายงานหนี้พนักงานกับเอเยนต์_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    DisplayReportReduceDebt_SPAndAgent();
                    break;
                case "ReportReduceDebt_CustomerAndAgent":
                    #region
                    //List<RPT_CUSTOMER_DEBT_41223> rt_RPT_CUSTOMER_DEBT = new List<RPT_CUSTOMER_DEBT_41223>();

                    //rt_RPT_CUSTOMER_DEBT = Reports.RPT_CUSTOMER_DEBT_41223(Region, AgentName, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                    //cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_CustomerAndAgent.rpt"));
                    //cryRpt.SetDataSource(rt_RPT_CUSTOMER_DEBT);
                    #endregion
                    ReportViewer1.ID = "รายงานหนี้ลูกค้ากับเอเยนต์_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    DisplayReportReduceDebt_CustomerAndAgent();
                    break;
                case "ReportReduceDebt":
                    #region
                    //List<RPT_DN_CN_41224> rt_RPT_DN_CN = new List<RPT_DN_CN_41224>();
                    //rt_RPT_DN_CN = Reports.RPT_DN_CN_41224(Region, AgentName, string.Empty, InvoiceDate, InvoiceDateTo, ddlType, CN_DN, Status, string.Empty);
                    //foreach (var d in rt_RPT_DN_CN)
                    //{
                    //    d.paramRegion_Name = Request.QueryString["Region"];
                    //    d.pramAgent_Name = Request.QueryString["AgentName"];
                    //    if (d.pramAgent_Name != "")
                    //    {
                    //        user_class = dbo_UserDataClass.Select_Record(d.pramAgent_Name);
                    //        d.pramAgent_Name = user_class.AgentName;
                    //    }
                    //    else
                    //    {
                    //        d.pramAgent_Name = "เลือกทั้งหมด";
                    //    }
                    //    d.pramDocNo = Request.QueryString["DebtID"];
                    //    d.pramType = Request.QueryString["ddlType"];
                    //    if (d.pramType == "")
                    //    {
                    //        d.pramType = "เลือกทั้งหมด";
                    //    }
                    //    d.pramDebt_StartDate = Request.QueryString["InvoiceDate"];
                    //    d.pramDebt_EndDate = Request.QueryString["InvoiceDateTo"];
                    //    d.pramStatus = Request.QueryString["Status"];
                    //    if (d.pramStatus == "")
                    //    {
                    //        d.pramStatus = "เลือกทั้งหมด";
                    //    }
                    //}

                    //cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt.rpt"));
                    //cryRpt.SetDataSource(rt_RPT_DN_CN);
                    #endregion
                    ReportViewer1.ID = "รายงานลดหนี้-เพิ่มหนี้_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    DisplayReportDN_CN();
                    break;
                //Report Income_Expenditure
                case "ReportExpenditure":
                    ReportViewer1.ID = "รายงานรายรับ-รายจ่ายรายเดือน_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    DisplayReportExpenditure();
                    break;
                case "ReportSummaryIncome":
                    ReportViewer1.ID = "รายงานสรุปรายรับ-รายจ่าย_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    DisplayReportSummaryIncome();
                    break;
                case "ReportFinanceClerk":
                    ReportViewer1.ID = "รายงานการเงินเสมียน_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    DisplayReportFinanceClerk();
                    break;
                case "ReportDailyFinance":
                    ReportViewer1.ID = "รายงานการเงินประจำวัน_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    DisplayReportDailyFinance();
                    break;
            }

            //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();
           
        }

        catch (Exception ex)
        {

        }

    }

    #region Display ReportReduceDebt
    private void DisplayReportReduceDebt_CustomerAndSP()
    {
        ReportDocument cryRpt = new ReportDocument();
        connDB = new ConnectionManager();
        List<RPT_SP_DEBT_41221> rt_RPT_CUSTOMER_SP = new List<RPT_SP_DEBT_41221>();
        rt_RPT_CUSTOMER_SP = Reports.RPT_SP_DEBT_41221(Region, AgentName, SPName, DebtID, DebtName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty);
        foreach (var d in rt_RPT_CUSTOMER_SP)
        {
            d.paramRegion_Name = Request.QueryString["Region"];
            if (d.paramRegion_Name == "")
            {
                d.paramRegion_Name = "เลือกทั้งหมด";
            }
           
            if (AgentName != "")
            {
                _agent = dbo_AgentDataClass.Select_Record(AgentName);
                d.pramAgent_Name = _agent.AgentName;   
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

        ReportViewer1.ToolPanelView = ToolPanelViewType.None;
        ReportViewer1.HasCrystalLogo = false;
        ReportViewer1.ReportSource = cryRpt;
        ReportViewer1.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        //Response.Flush();
        //Response.Close();
    }

    private void DisplayReportReduceDebt_SPAndAgent()
    {
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

        ReportViewer1.ToolPanelView = ToolPanelViewType.None;
        ReportViewer1.HasCrystalLogo = false;
        ReportViewer1.ReportSource = cryRpt;
        ReportViewer1.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        //Response.Flush();
        //Response.Close();
    }

    private void DisplayReportReduceDebt_CustomerAndAgent()
    {
        List<RPT_CUSTOMER_DEBT_41223> rt_RPT_CUSTOMER_DEBT = new List<RPT_CUSTOMER_DEBT_41223>();
        //rt_RPT_CUSTOMER_DEBT = Reports.RPT_CUSTOMER_DEBT_41223(Region, AgentName, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
        //cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_CustomerAndAgent.rpt"));
        //cryRpt.SetDataSource(rt_RPT_CUSTOMER_DEBT);

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
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
                List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);

                foreach (RPT_CUSTOMER_DEBT_41223 rpt in _inrpt)
                {
                    rt_RPT_CUSTOMER_DEBT.Add(rpt);
                }

            }
        }
        else
        {
            rt_RPT_CUSTOMER_DEBT = Reports.RPT_CUSTOMER_DEBT_41223(Region, AgentName, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // rt_RPT_CUSTOMER_DEBT = Reports.RPT_CUSTOMER_DEBT_41223(Region, AgentName, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
        cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_CustomerAndAgent.rpt"));
        cryRpt.SetDataSource(rt_RPT_CUSTOMER_DEBT);
        #region old
        ///////
        //List<RPT_CUSTOMER_DEBT_41223> rt_RPT_CUSTOMER_DEBT = new List<RPT_CUSTOMER_DEBT_41223>();
        //rt_RPT_CUSTOMER_DEBT = Reports.RPT_CUSTOMER_DEBT_41223(Region, AgentName, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
        //foreach (var d in rt_RPT_CUSTOMER_DEBT)
        //{
        //    d.pramDebt_StartDate = Request.QueryString["DebtDate_From"];
        //    d.pramDebt_EndDate = Request.QueryString["DebtDate_To"];
        //    d.pramStatus = Request.QueryString["Status"];
        //}
        //cryRpt.Load(Server.MapPath("~/Report/RT_ReportReduceDebt_CustomerAndAgent.rpt"));
        //cryRpt.SetDataSource(rt_RPT_CUSTOMER_DEBT);
        ///////

        #endregion

        ReportViewer1.ToolPanelView = ToolPanelViewType.None;
        ReportViewer1.HasCrystalLogo = false;
        ReportViewer1.ReportSource = cryRpt;
        ReportViewer1.RefreshReport();


        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        //Response.Flush();
        //Response.Close();
    }

    private void DisplayReportDN_CN()
    {
        //ReportDocument cryRpt = new ReportDocument();

        List<RPT_DN_CN_41224> rt_RPT_DN_CN = new List<RPT_DN_CN_41224>();
        rt_RPT_DN_CN = Reports.RPT_DN_CN_41224(Region, AgentName, string.Empty, InvoiceDate, InvoiceDateTo, ddlType, CN_DN, Status, string.Empty);
        foreach (var d in rt_RPT_DN_CN)
        {
            d.paramRegion_Name = Request.QueryString["Region"];
            if(d.paramRegion_Name == "")
            {
                d.paramRegion_Name = "เลือกทั้งหมด";
            }
            if (AgentName != "")
            {
                _agent = dbo_AgentDataClass.Select_Record(AgentName);
                d.pramAgent_Name = _agent.AgentName;
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

        ReportViewer1.ToolPanelView = ToolPanelViewType.None;
        ReportViewer1.HasCrystalLogo = false;
        ReportViewer1.ReportSource = cryRpt;
        ReportViewer1.RefreshReport();

        //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        //Response.Flush();
        //Response.Close();
    }
    #endregion

    #region Display Report Customer
    private void DisplayReportCustomerDetails()
    {
        List<RPT_Customer_INFO_41211> rt_RPT_Customer_INFO = new List<RPT_Customer_INFO_41211>();

        rt_RPT_Customer_INFO = Reports.RPT_Customer_INFO_41211(Region, AgentName, CustomerType, CustomerName, SPName, ResidenceType, Status, string.Empty, string.Empty, string.Empty);
        cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerDetails.rpt"));
        cryRpt.SetDataSource(rt_RPT_Customer_INFO);

        BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        Response.ClearContent();
        Response.ClearHeaders();
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        Response.Flush();
        Response.Close();
    }
    private void DisplayReportCustomerPayoutType()
    {
        List<RPT_Customer_PAY_TYPE_41212> rt_RPT_PAY_TYPE = new List<RPT_Customer_PAY_TYPE_41212>();
        rt_RPT_PAY_TYPE = Reports.RPT_Customer_PAY_TYPE_41212(Region, AgentName, CustomerID, CustomerName, SPName, string.Empty, Status, PaymentType, string.Empty, string.Empty, string.Empty);
        foreach (var d in rt_RPT_PAY_TYPE)
        {
            d.paramRegion_Name = Request.QueryString["Region"];
            if (d.paramRegion_Name == "")
            {
                d.paramRegion_Name = "เลือกทั้งหมด";
            }
            d.paramCV_Name = Request.QueryString["AgentName"];
            if (d.paramCV_Name != "")
            {
                user_class = dbo_UserDataClass.Select_Record(d.paramCV_Name);
                d.paramCV_Name = user_class.AgentName;
            }
            d.Customer_Name = Request.QueryString["CustomerName"];
            d.paramSP_Name = Request.QueryString["SPName"];
            d.paramStatus = Request.QueryString["Status"];
            d.paramCustomer_ID = Request.QueryString["CustomerID"];
            d.paramPayType = Request.QueryString["PaymentType"];
        }
        cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerPayoutType.rpt"));
        cryRpt.SetDataSource(rt_RPT_PAY_TYPE);

        BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        Response.ClearContent();
        Response.ClearHeaders();
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        Response.Flush();
        Response.Close();
    }
    private void DisplayReportCustomerBirthday()
    {
        List<RPT_Customer_BIRTH_DATE_41213> rt_RPT_BIRTH_DATE = new List<RPT_Customer_BIRTH_DATE_41213>();
        rt_RPT_BIRTH_DATE = Reports.RPT_Customer_BIRTH_DATE_41213(Region, AgentName, MonthGroup, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        foreach (var d in rt_RPT_BIRTH_DATE)
        {
            d.paramRegion_Name = Request.QueryString["Region"];
            if (d.paramRegion_Name == "")
            {
                d.paramRegion_Name = "เลือกทั้งหมด";
            }
            d.paramCV_Name = Request.QueryString["AgentName"];
            if (d.paramCV_Name != "")
            {
                user_class = dbo_UserDataClass.Select_Record(d.paramCV_Name);
                d.paramCV_Name = user_class.AgentName;
            }
            d.paramBirth_Month = Request.QueryString["MonthGroup"];
        }
        cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerBirthday.rpt"));
        cryRpt.SetDataSource(rt_RPT_BIRTH_DATE);

        BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        Response.ClearContent();
        Response.ClearHeaders();
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        Response.Flush();
        Response.Close();
    }
    private void DisplayReportCustomerType()
    {
        List<RPT_SUMM_MEMBER__41210> rt_RPT_SUMM_MEMBER = new List<RPT_SUMM_MEMBER__41210>();
        rt_RPT_SUMM_MEMBER = Reports.RPT_SUMM_MEMBER__41210(string.Empty, AgentName, string.Empty, string.Empty, string.Empty, string.Empty, MonthGroup, string.Empty, string.Empty, User_ID);
        cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerType.rpt"));
        cryRpt.SetDataSource(rt_RPT_SUMM_MEMBER);

        BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        Response.ClearContent();
        Response.ClearHeaders();
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        Response.Flush();
        Response.Close();
    }

    #endregion

    #region Report Income_Expenditure
    private void DisplayReportDailyFinance()
    {
        try
        {

            List<RPT_SALE_AMT_DAILY_41220A> rt_RPT_AMT_DAILY = new List<RPT_SALE_AMT_DAILY_41220A>();
            #region old
            //string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

            //if (user_class.User_Group_ID == "CP Meiji")
            //{
            //    string region = user_class.Region;

            //    string[] regions = region.Split(',');

            //    List<dbo_AgentClass> cv_code_ = new List<dbo_AgentClass>();

            //    foreach (string in_region in regions)
            //    {
            //        List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => f.Location_Region == in_region).Select(f => f));
            //        foreach (dbo_AgentClass _cv in cv_code2)
            //        {

            //            List<RPT_SALE_AMT_DAILY_41220A> _inrpt = Reports.RPT_SALE_AMT_DAILY_41220A(string.Empty, _cv.CV_Code , InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //            foreach (RPT_SALE_AMT_DAILY_41220A rpt in _inrpt)
            //            {
            //                rt_RPT_AMT_DAILY.Add(rpt);
            //            }
            //        }
            //    }

            //}
            //else
            //{

            //    rt_RPT_AMT_DAILY = Reports.RPT_SALE_AMT_DAILY_41220A(string.Empty, string.Empty, InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //}
            #endregion

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
        
            if (user_class.User_Group_ID == "CP Meiji")
            {
                string region = user_class.Region;
                string[] regions = region.Split(',');

                List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));
                foreach (dbo_AgentClass _cv in cv_code2)
                {
                    List<RPT_SALE_AMT_DAILY_41220A> _inrpt = Reports.RPT_SALE_AMT_DAILY_41220A(string.Empty, _cv.CV_Code, InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                    foreach (RPT_SALE_AMT_DAILY_41220A rpt in _inrpt)
                    {
                        rt_RPT_AMT_DAILY.Add(rpt);
                    }

                }
            }
            else
            {
                rt_RPT_AMT_DAILY = Reports.RPT_SALE_AMT_DAILY_41220A(string.Empty, CV_CODE, InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }

            foreach(var d in rt_RPT_AMT_DAILY )
            {
                d.Create_Date = DateTime.Now.ToShortDateString();
            }

            cryRpt.Load(Server.MapPath("~/Report/RT_ReportDailyFinance.rpt"));
            cryRpt.SetDataSource(rt_RPT_AMT_DAILY);

            ReportViewer1.ToolPanelView = ToolPanelViewType.None;
            ReportViewer1.HasCrystalLogo = false;
            ReportViewer1.ReportSource = cryRpt;
            ReportViewer1.RefreshReport();

            //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }
    private void DisplayReportFinanceClerk()
    {
        try
        {

            List<RPT_REV_EXP_DAILY_41220> rt_RPT_EXP_DAILY = new List<RPT_REV_EXP_DAILY_41220>();


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
                        List<RPT_REV_EXP_DAILY_41220> _inrpt = Reports.RPT_REV_EXP_DAILY_41220(string.Empty, _cv.CV_Code, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                        foreach (RPT_REV_EXP_DAILY_41220 rpt in _inrpt)
                        {
                            rt_RPT_EXP_DAILY.Add(rpt);
                        }
                    }
                }

                foreach (var d in rt_RPT_EXP_DAILY)
                {
                    // dbo_AgentClass _agent = dbo_AgentDataClass.Select_Record(d.paramCV_Code);
                    dbo_AgentClass _agent = agent.FirstOrDefault(f => f.CV_Code == d.CV_Code);
                    if (_agent != null)
                    {
                        d.paramCV_Name = _agent.AgentName;
                        d.paramPostDate_From = Request.QueryString["InvoiceDate"];
                        d.paramPostDate_To = Request.QueryString["InvoiceDateTo"];
                    }
                }

            }
            else
            {
                rt_RPT_EXP_DAILY = Reports.RPT_REV_EXP_DAILY_41220(string.Empty, CV_CODE, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                foreach (var d in rt_RPT_EXP_DAILY)
                {


                    // dbo_AgentClass _agent = dbo_AgentDataClass.Select_Record(d.paramCV_Code);
                    dbo_AgentClass _agent = agent.FirstOrDefault(f => f.CV_Code == d.CV_Code);

                    if (_agent != null)
                    {
                        d.paramCV_Name = _agent.AgentName;
                        d.paramPostDate_From = Request.QueryString["InvoiceDate"];
                        d.paramPostDate_To = Request.QueryString["InvoiceDateTo"];
                    }
                }
            }




            /*

            rt_RPT_EXP_DAILY = Reports.RPT_REV_EXP_DAILY_41220(string.Empty, CV_CODE, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            foreach (var d in rt_RPT_EXP_DAILY)
            {
                user_class = dbo_UserDataClass.Select_Record(CV_CODE);
                d.paramCV_Name = user_class.AgentName;
                d.paramPostDate_From = Request.QueryString["InvoiceDate"];
                d.paramPostDate_To = Request.QueryString["InvoiceDateTo"];
            }
            */
            cryRpt.Load(Server.MapPath("~/Report/RT_ReportFinanceClerk - B.rpt"));
            cryRpt.SetDataSource(rt_RPT_EXP_DAILY);

            ReportViewer1.ToolPanelView = ToolPanelViewType.None;
            ReportViewer1.HasCrystalLogo = false;
            ReportViewer1.ReportSource = cryRpt;
            ReportViewer1.RefreshReport();

            //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }
    private void DisplayReportSummaryIncome()
    {
        try
        {
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

                        List<RPT_SUMM_EXPENSE_41219> _inrpt = Reports.RPT_SUMM_EXPENSE_41219(string.Empty, _cv.CV_Code , InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

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

                    d.Temp0 = DateTime.Now.ToShortDateString();
                }

            }
            else
            {
                rt_RPT_EXPENSE = Reports.RPT_SUMM_EXPENSE_41219(string.Empty, CV_CODE, InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                foreach (var d in rt_RPT_EXPENSE)
                {
                    d.paramCV_Code = d.CV_Code;

                    // dbo_AgentClass _agent = dbo_AgentDataClass.Select_Record(d.paramCV_Code);
                    dbo_AgentClass _agent = agent.FirstOrDefault(f => f.CV_Code == d.CV_Code);

                    if (_agent != null)
                        d.paramCV_Name = _agent.AgentName;
                    d.Temp0 = DateTime.Now.ToShortDateString();
                }
            }



            /*
            rt_RPT_EXPENSE = Reports.RPT_SUMM_EXPENSE_41219(string.Empty, CV_CODE, InvoiceDate, InvoiceDateTo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            foreach (var d in rt_RPT_EXPENSE)
            {
                d.paramCV_Code = CV_CODE;
                user_class = dbo_UserDataClass.Select_Record(d.paramCV_Code);
                d.paramCV_Name = user_class.AgentName;
            }
            */



            cryRpt.Load(Server.MapPath("~/Report/RT_ReportSummaryIncome.rpt"));
            cryRpt.SetDataSource(rt_RPT_EXPENSE);

            ReportViewer1.ToolPanelView = ToolPanelViewType.None;
            ReportViewer1.HasCrystalLogo = false;
            ReportViewer1.ReportSource = cryRpt;
            ReportViewer1.RefreshReport();

            //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }
    private void DisplayReportExpenditure()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {

            List<RPT_EXPENSE_MONTHLY_41218> rt_RPT_EXPENSE_MONTHLY = new List<RPT_EXPENSE_MONTHLY_41218>();
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
                        //cv_code_.Add(_cv);
                        List<RPT_EXPENSE_MONTHLY_41218> _inrpt = Reports.RPT_EXPENSE_MONTHLY_41218(string.Empty, _cv.CV_Code, MonthGroup, MonthGroupTo, Year, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                        foreach (RPT_EXPENSE_MONTHLY_41218 rpt in _inrpt)
                        {
                            rt_RPT_EXPENSE_MONTHLY.Add(rpt);
                        }

                    }
                }


                foreach (var d in rt_RPT_EXPENSE_MONTHLY)
                {
                    d.paramCV_Code = d.CV_Code;


                    dbo_AgentClass _agent = dbo_AgentDataClass.Select_Record(d.paramCV_Code);
                    d.paramCV_Name = _agent.AgentName;

                    //user_class = dbo_UserDataClass.Select_Record(d.paramCV_Code);
                    //d.paramCV_Name = user_class.AgentName;



                    d.paramYear = Request.QueryString["Year"];
                    d.paramStartMonth = CV_Month(MonthGroup);
                    d.paramEndMonth = CV_Month(MonthGroupTo);
                }




            }
            else
            {
                rt_RPT_EXPENSE_MONTHLY = Reports.RPT_EXPENSE_MONTHLY_41218(string.Empty, CV_CODE, MonthGroup, MonthGroupTo, Year, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                foreach (var d in rt_RPT_EXPENSE_MONTHLY)
                {
                    d.paramCV_Code = CV_CODE;

                    dbo_AgentClass _agent = dbo_AgentDataClass.Select_Record(d.paramCV_Code);
                    d.paramCV_Name = _agent.AgentName;

                    d.paramYear = Request.QueryString["Year"];
                    d.paramStartMonth = CV_Month(MonthGroup);
                    d.paramEndMonth = CV_Month(MonthGroupTo);
                }
            }


            cryRpt.Load(Server.MapPath("~/Report/RT_ReportExpenditure.rpt"));
            cryRpt.SetDataSource(rt_RPT_EXPENSE_MONTHLY);

            ReportViewer1.ToolPanelView = ToolPanelViewType.None;
            ReportViewer1.HasCrystalLogo = false;
            ReportViewer1.ReportSource = cryRpt;
            ReportViewer1.RefreshReport();

            //BinaryReader stream = new BinaryReader(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }
    #endregion

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