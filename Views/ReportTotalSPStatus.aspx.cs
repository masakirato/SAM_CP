using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ReportTotalSPStatus : System.Web.UI.Page
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
        {/*
            Dictionary<string, string> title = dbo_ItemDataClass.GetDropDown("08");
            ddlPrefix_ID.DataSource = title;
            ddlPrefix_ID.DataBind();


            Dictionary<string, string> grade = dbo_ItemDataClass.GetDropDown("11");
            ddlSearchGrade.DataSource = grade;
            ddlSearchGrade.DataBind();

            ddlGrade.DataSource = grade;
            ddlGrade.DataBind();


            Dictionary<string, string> agenttype = dbo_ItemDataClass.GetDropDown("13");
            ddlSearchAgentType.DataSource = agenttype;
            ddlSearchAgentType.DataBind();
            ddlAgent_Type_ID.DataSource = agenttype;
            ddlAgent_Type_ID.DataBind();

            Dictionary<string, string> Term_of_payment = dbo_ItemDataClass.GetDropDown("14");
            ddlTerm_of_payment.DataSource = Term_of_payment;
            ddlTerm_of_payment.DataBind();



            Dictionary<string, string> bank = dbo_ItemDataClass.GetDropDown("12");
            ddlBank.DataSource = bank;
            ddlBank.DataBind();



            List<dbo_UserClass> user = dbo_UserDataClass.Search("", "", "", "", "", "CP Meiji", "", "", null, string.Empty, string.Empty);
            dbo_UserClass u = new dbo_UserClass();
            u.FullName = "==ระบุ==";
            u.User_ID = string.Empty;
            user.Insert(0, u);

            ddlSearchSP.DataSource = user.Where(f => f.Position == "พนักงานขาย" || f.FullName == "==ระบุ==");
            ddlSearchSP.DataBind();

            ddlSearchSM.DataSource = user.Where(f => f.Position == "ผู้จัดการแผนก" || f.FullName == "==ระบุ==");
            ddlSearchSM.DataBind();

            ddlSearchDM.DataSource = user.Where(f => f.Position == "ผู้จัดการฝ่าย" || f.FullName == "==ระบุ==");
            ddlSearchDM.DataBind();

            ddlSearchGM.DataSource = user.Where(f => f.Position == "ผู้จัดการทั่วไป" || f.FullName == "==ระบุ==");
            ddlSearchGM.DataBind();

            ddlSearchAPV.DataSource = user.Where(f => f.Position == "ผู้ช่วยกรรมการผู้จัดการ" || f.FullName == "==ระบุ==");
            ddlSearchAPV.DataBind();



            ddlSD_ID.DataSource = user.Where(f => f.Position == "พนักงานขาย" || f.FullName == "==ระบุ==");
            ddlSD_ID.DataBind();
            ddlSM_ID.DataSource = user.Where(f => f.Position == "ผู้จัดการแผนก" || f.FullName == "==ระบุ==");
            ddlSM_ID.DataBind();
            ddlDM_ID.DataSource = user.Where(f => f.Position == "ผู้จัดการฝ่าย" || f.FullName == "==ระบุ==");
            ddlDM_ID.DataBind();
            ddlManager.DataSource = user.Where(f => f.Position == "ผู้จัดการทั่วไป" || f.FullName == "==ระบุ==");
            ddlManager.DataBind();
            ddlAVP.DataSource = user.Where(f => f.Position == "ผู้ช่วยกรรมการผู้จัดการ" || f.FullName == "==ระบุ==");
            ddlAVP.DataBind();

            ddlGrade.DataSource = grade;
            ddlGrade.DataBind();



            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);


            dbo_AgentClass a = new dbo_AgentClass();
            a.Prefix_ID = "==ระบุ==";
            a.CV_Code = string.Empty;
            agent.Insert(0, a);
            ddlSearchPrefix_ID.DataSource = agent;
            ddlSearchPrefix_ID.DataBind();


            List<dbo_TambolClass> tambol = dbo_TambolDataClass.SelectAll();

            List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Province)
                         .Select(grp => grp.First())
                         .ToList();


            dbo_TambolClass first_ = new dbo_TambolClass() { Province = "==ระบุ==" };
            tmp_tambol.Insert(0, first_);


            List<dbo_TambolClass> disti = new List<dbo_TambolClass>();
            disti.Add(new dbo_TambolClass() { District = "==ระบุ==", Sub_district = "==ระบุ==" });

            ddlLocation_Province.DataSource = tmp_tambol;
            ddlLocation_Province.DataBind();

            ddlLocation_District.DataSource = disti;
            ddlLocation_District.DataBind();

            ddlLocation_Sub_district.DataSource = disti;
            ddlLocation_Sub_district.DataBind();

            ddlInvoice_Province.DataSource = tmp_tambol;
            ddlInvoice_Province.DataBind();

            ddlInvoice_District.DataSource = disti;
            ddlInvoice_District.DataBind();

            ddlInvoice_Sub_district.DataSource = disti;
            ddlInvoice_Sub_district.DataBind();

            Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");
            //ddlSearchRegion(new dbo_ItemDataClass() { region = "==ระบุ==" });
            ddlLocation_Region.DataSource = region;
            ddlLocation_Region.DataBind();

            ddlInvoice_Region.DataSource = region;
            ddlInvoice_Region.DataBind();

            //ddlSearchRegion.DataSource = region.Where(f => f.Key != string.Empty);
            ddlSearchRegion.DataSource = region;
            ddlSearchRegion.DataBind();
            */
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('MyPage.aspx?Param=" + Param1.ToString() + "');", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../Report_From/ViewsReport.aspx?Param=" + "1" + "');", true);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}