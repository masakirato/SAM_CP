using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewsMockup_DefaultReporting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {



                List<dbo_UserClass> users = dbo_UserDataClass.Search("", "", "", "", "", "", "", "", null, "", "");

                DataSet ds = new DataSet();
                ds.Tables.Add(new DataTable());
                ds.Tables[0].Columns.Add("FullName");
                ds.Tables[0].Rows.Add("Sompong");

                ds.Tables.Add(new DataTable());


               // List<dbo_OrderingDetailClass> detials = dbo_OrderingDetailDataClass.Search(string.Empty);
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search("", "", "", "", "", "", "", "", "", "", "","");
                List<dbo_OrderingClass> ordering = new List<dbo_OrderingClass>();

                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("~/ViewsMockup/RT_PO_DOC001.rpt"));

                crystalReport.SetDataSource(agent);
                //crystalReport.SetDataSource(agent);
                //crystalReport.SetDataSource(ordering);


                CrystalReportViewer1.ReportSource = crystalReport;
            }
            catch (Exception)
            {

            }
        }
    }
}