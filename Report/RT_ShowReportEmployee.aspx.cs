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

public partial class Report_RT_ShowReportEmployee : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    dbo_UserClass user_class;
    dbo_AgentClass _agent;
    string datetime = DateTime.Now.ToShortDateString();
    string Time = DateTime.Now.ToShortTimeString();
    protected void Page_Init(object sender, EventArgs e)
    {
        string RPT = Request.QueryString["RPT"];
        string PRM = Request.QueryString["PRM"];
        string Region = Request.QueryString["Region"];
        string AgentName = Request.QueryString["AgentName"];
        string SPName = Request.QueryString["SPName"];
        string JoinDate_From = Request.QueryString["JoinDate_From"];
        string JoinDate_To = Request.QueryString["JoinDate_To"];
        string WorkingAge_From = Request.QueryString["WorkingAge_From"];
        string WorkingAge_To = Request.QueryString["WorkingAge_To"];
        string ApprovalStatus = Request.QueryString["ApprovalStatus"];
        string Position = Request.QueryString["Position"];

        string RequisitionDate_From = Request.QueryString["RequisitionDate_From"];
        string RequisitionDate_To = Request.QueryString["RequisitionDate_To"];
        string ProductGroup = Request.QueryString["ProductGroup"];
        string Size = Request.QueryString["Size"];
        string User_ID = Request.QueryString["User_ID"];

        string Clearing_Date_From = Request.QueryString["Clearing_Date_From"];
        string Clearing_Date_TO = Request.QueryString["Clearing_Date_TO"];

        string Lebel_SP = Request.QueryString["Lebel_SP"];

        if (Lebel_SP == "เลือกทั้งหมด")
        {
            Lebel_SP = string.Empty;
        }

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
        if (ApprovalStatus == "เลือกทั้งหมด")
        {
            ApprovalStatus = string.Empty;
        }
        if (Position == "เลือกทั้งหมด")
        {
            Position = string.Empty;
        }
        if (ProductGroup == "เลือกทั้งหมด")
        {
            ProductGroup = string.Empty;
        }
        if (Size == "เลือกทั้งหมด")
        {
            Size = string.Empty;
        }

        GenerateReport(RPT, PRM, Region, AgentName, SPName, JoinDate_From, JoinDate_To, WorkingAge_From, WorkingAge_To, ApprovalStatus, Position
            , RequisitionDate_From, RequisitionDate_To, ProductGroup, Size,
            Clearing_Date_From, Clearing_Date_TO, User_ID, Lebel_SP);
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
            //string JoinDate_From = Request.QueryString["JoinDate_From"];
            //string JoinDate_To = Request.QueryString["JoinDate_To"];
            //string WorkingAge_From = Request.QueryString["WorkingAge_From"];
            //string WorkingAge_To = Request.QueryString["WorkingAge_To"];
            //string ApprovalStatus = Request.QueryString["ApprovalStatus"];
            //string Position = Request.QueryString["Position"];

            //string RequisitionDate_From = Request.QueryString["RequisitionDate_From"];
            //string RequisitionDate_To = Request.QueryString["RequisitionDate_To"];
            //string ProductGroup = Request.QueryString["ProductGroup"];
            //string Size = Request.QueryString["Size"];
            //string User_ID = Request.QueryString["User_ID"];

            //string Clearing_Date_From = Request.QueryString["Clearing_Date_From"];
            //string Clearing_Date_TO = Request.QueryString["Clearing_Date_TO"];

            //string Lebel_SP = Request.QueryString["Lebel_SP"];

            //if(Lebel_SP =="เลือกทั้งหมด")
            //{
            //    Lebel_SP = string.Empty;
            //}

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
            //if (ApprovalStatus == "เลือกทั้งหมด")
            //{
            //    ApprovalStatus = string.Empty;
            //}
            //if (Position == "เลือกทั้งหมด")
            //{
            //    Position = string.Empty;
            //}
            //if (ProductGroup == "เลือกทั้งหมด")
            //{
            //    ProductGroup = string.Empty;
            //}
            //if (Size == "เลือกทั้งหมด")
            //{
            //    Size = string.Empty;
            //}

            //GenerateReport(RPT, PRM, Region, AgentName, SPName, JoinDate_From, JoinDate_To, WorkingAge_From, WorkingAge_To, ApprovalStatus, Position
            //    , RequisitionDate_From, RequisitionDate_To, ProductGroup, Size,
            //    Clearing_Date_From, Clearing_Date_TO, User_ID, Lebel_SP);

        }
    }
    protected void GenerateReport(string RPT, string PRM, string Region, string AgentName, string SPName, string JoinDate_From, string JoinDate_To, string WorkingAge_From, string WorkingAge_To, string ApprovalStatus, string Position,
        string RequisitionDate_From, string RequisitionDate_To, string ProductGroup, string Size, string Clearing_Date_From, string Clearing_Date_TO, string User_ID ,string Lebel_SP)
    {
        try
        {
            ReportDocument cryRpt = new ReportDocument();

            switch (RPT)
            {
                case "ReportAllEmployee":
                    ReportViewer1.ID = "รายงานจำนวนพนักงานทั้งหมดของศูนย์_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    List<RPT_SUMM_SP_BY_AGENT_41215> rt_RPT_Emp = new List<RPT_SUMM_SP_BY_AGENT_41215>();
                    rt_RPT_Emp = Reports.RPT_SUMM_SP_BY_AGENT_41215(Region, AgentName, ApprovalStatus, Position, JoinDate_From, JoinDate_To
                        , WorkingAge_From, WorkingAge_To, SPName, string.Empty);

                    if (rt_RPT_Emp.Count > 0)
                    {
                        foreach (RPT_SUMM_SP_BY_AGENT_41215 r in rt_RPT_Emp) {
                            r.paramRegion_Name = Request.QueryString["RegionName"];
                            r.paramCV_Name = Request.QueryString["AgentFullName"];
                            r.paramSP_ID = Request.QueryString["SPName"];
                            if ((Request.QueryString["SPFullName"] != null) && (Request.QueryString["SPName"] != null))
                            {
                                r.paramSP_Name = Request.QueryString["SPFullName"].Replace(Request.QueryString["SPName"],"");
                            }
                            r.paramStartDate_From = Request.QueryString["JoinDate_From"];
                            r.paramStartDate_To = Request.QueryString["JoinDate_To"];
                            r.paramJoinDate_From = Request.QueryString["WorkingAge_From"];
                            r.paramJoinDate_To = Request.QueryString["WorkingAge_To"];

                            r.paramStatus = Request.QueryString["ApprovalStatus"];
                            r.paramPosition = Request.QueryString["PositionFullName"];
                        }
                    }

                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportAllEmployee.rpt"));
                    cryRpt.SetDataSource(rt_RPT_Emp);
                    break;
                case "ReportCommissions":
                    ReportViewer1.ID = "รายงานค่าคอมมิชชั่น_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    List<RPT_SP_COMMISSION_41216> rt_RPT_Comm = new List<RPT_SP_COMMISSION_41216>();
                    rt_RPT_Comm = Reports.RPT_SP_COMMISSION_41216(string.Empty,AgentName, SPName, RequisitionDate_From, RequisitionDate_To, ProductGroup, Size, Lebel_SP, string.Empty, string.Empty);
                    foreach (var d in rt_RPT_Comm)
                    {
                       
                        if(AgentName == "")
                        {
                            d.paramCV_Code = "เลือกทั้งหมด";
                            d.paramCV_Name = "เลือกทั้งหมด";
                        }
                        else
                        {
                            d.paramCV_Code = AgentName;
                            //user_class = dbo_UserDataClass.Select_Record(AgentName);
                            //d.paramCV_Name = user_class.AgentName;
                            _agent = dbo_AgentDataClass.Select_Record(AgentName);
                            d.paramCV_Name = _agent.AgentName;
                        }              
                        d.paramStartDate = RequisitionDate_From;
                        d.paramEndDate = RequisitionDate_To;                       
                        if (SPName == "")
                        {
                            d.paramSP_ID = "เลือกทั้งหมด";
                        }
                        else
                        {
                            d.paramSP_ID = SPName;
                        } 
                        if (Lebel_SP == "")
                        {
                            d.paramSP_Name = "เลือกทั้งหมด";
                        }
                        else
                        {
                            d.paramSP_Name = Lebel_SP.Substring(11);
                        }  

                        if (ProductGroup == "")
                        {
                            d.paramProduct_Group = "เลือกทั้งหมด";
                        }
                        else
                        {
                            d.paramProduct_Group = ProductGroup;
                        }
                       
                        if (Size == "")
                        {
                            d.paramSize = "เลือกทั้งหมด";
                        }
                        else
                        {
                            d.paramSize = Size;
                        }
                    }
                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportCommissions.rpt"));
                    cryRpt.SetDataSource(rt_RPT_Comm);
                    break;
                case "ReportWithdrawalCommissions":

                    ReportViewer1.ID = "รายงานการเบิกค่าคอมมิชชั่น_" + datetime.Replace("/", string.Empty) + "_" + Time.Replace(":", string.Empty);
                    List<RPT_REQUES_COMMISSION_41217> rt_RPT_RqComm = new List<RPT_REQUES_COMMISSION_41217>(); 
                    rt_RPT_RqComm = Reports.RPT_REQUES_COMMISSION_41217(string.Empty,AgentName, SPName, Clearing_Date_From, Clearing_Date_TO, RequisitionDate_From, RequisitionDate_To,Lebel_SP, string.Empty, string.Empty);

                    foreach(var d in rt_RPT_RqComm)
                    { 
                        
                        if (AgentName == "")
                        {
                            d.paramCV_Code = "เลือกทั้งหมด";
                            d.paramCV_Name = "เลือกทั้งหมด";
                        }
                        else
                        {
                            d.paramCV_Code = AgentName;

                            //user_class = dbo_UserDataClass.Select_Record(AgentName);
                            //d.paramCV_Name = user_class.AgentName;
                            _agent = dbo_AgentDataClass.Select_Record(AgentName);
                            d.paramCV_Name = _agent.AgentName;
                        }
                        
                        d.paramCR_StartDate = Clearing_Date_From;
                        d.paramCR_EndDate = Clearing_Date_TO;
                        d.paramRQ_StartDate = RequisitionDate_From;
                        d.paramRQ_EndDate = RequisitionDate_To;
                        
                        if(SPName == "")
                        {
                            d.paramSP_ID = "เลือกทั้งหมด";
                        }
                        else
                        {
                            d.paramSP_ID = SPName;
                        } 
                        if (Lebel_SP == "")
                        {
                            d.paramSP_Name = "เลือกทั้งหมด";
                        }
                        else
                        {
                            d.paramSP_Name = Lebel_SP.Substring(11);
                        }

                    }

                    cryRpt.Load(Server.MapPath("~/Report/RT_ReportWithdrawalCommissions.rpt"));
                    cryRpt.SetDataSource(rt_RPT_RqComm);
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
           // logger.Error(ex.Message);
        }
    }
}