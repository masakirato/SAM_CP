using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using log4net;

public partial class Report_RT_ShowCustomerPDF : System.Web.UI.Page
{
    // private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string RPT;
    string PRM;
    string Region;
    string AgentName;
    string CustomerType;
    string CustomerName;
    string SPName;
    string ResidenceType;
    string Status;
    string User_ID;
    string CustomerID;
    string PaymentType;
    string MonthGroup;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerateReport();
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

        //string User_ID = Request.Cookies["User_ID"].Value;

        dbo_UserClass user_class;

        try
        {
            ReportDocument cryRpt = new ReportDocument();

            switch (RPT)
            {
                case "ReportCustomerDetails":

                    List<RPT_Customer_INFO_41211> rt_RPT_Customer_INFO = new List<RPT_Customer_INFO_41211>();

                    rt_RPT_Customer_INFO = Reports.RPT_Customer_INFO_41211(Region, AgentName, CustomerType, CustomerName, SPName, ResidenceType, Status, string.Empty, string.Empty, string.Empty);          
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerDetails.rpt"));
                    cryRpt.SetDataSource(rt_RPT_Customer_INFO);
                    break;
                case "ReportCustomerPayoutType":
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
                        d.Customer_Name = Request.QueryString["CustomerName"];
                        d.paramSP_Name = Request.QueryString["SPFullName"];
                        d.paramStatus = Request.QueryString["Status"];
                        d.paramCustomer_ID = Request.QueryString["CustomerID"];
                        d.paramPayType = Request.QueryString["PaymentTypeName"];
                    }
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerPayoutType.rpt"));
                    cryRpt.SetDataSource(rt_RPT_PAY_TYPE);
                    break;
                case "ReportCustomerBirthday":

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
                    break;
                case "ReportCustomerType":

                    List<RPT_SUMM_MEMBER__41210> rt_RPT_SUMM_MEMBER = new List<RPT_SUMM_MEMBER__41210>();
                    rt_RPT_SUMM_MEMBER = Reports.RPT_SUMM_MEMBER__41210(string.Empty, AgentName, string.Empty, string.Empty, string.Empty, string.Empty, MonthGroup, string.Empty, string.Empty, User_ID);
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportCustomerType.rpt"));
                    cryRpt.SetDataSource(rt_RPT_SUMM_MEMBER);
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

    private void DisplayReportCustomerPayoutType()
    {
        //ReportDocument cryRpt = new ReportDocument();
        
    }
}