using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ReportOrdersMonthlyTarget : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetUpDrowDownList();
        }

    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            //ddlAgentName
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
            dbo_AgentClass a = new dbo_AgentClass();
            a.Prefix_ID = "เลือกทั้งหมด";
            a.CV_Code = string.Empty;
            agent.Insert(0, a);
            ddlAgentName.DataSource = agent;
            ddlAgentName.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('MyPage.aspx?Param=" + Param1.ToString() + "');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../Report_From/ViewsReport.aspx?Param=" + "1" + "');", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}