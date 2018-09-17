﻿#region Using
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using System.Threading;
#endregion

public partial class Views_ReportOrders : System.Web.UI.Page
{
    #region Private Variables
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetUpDrowDownList();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidForm())
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                string Region = ddlSearchRegion.SelectedValue;
                string AgentName = ddlAgentName.SelectedValue;
                string Month_From = ddlMonth_From.SelectedValue;
                string Month_To = ddlMonth_To.SelectedValue;
                string ProductGroup = ddl_ProductGroup.SelectedValue;
                string Size = ddl_Size.SelectedValue;
                string UnitType = ddlUnit.SelectedValue;
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
                List<RPT_AnnualReport_4121> rt_RPT_Order = new List<RPT_AnnualReport_4121>();
                string User_ID = Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                if (user_class.User_Group_ID == "CP Meiji")
                {
                    string region = user_class.Region;
                    string[] regions = region.Split(',');

                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if(cv_code1.Count > 0)
                    {

                        if(AgentName == string.Empty)
                        {
                            foreach (dbo_AgentClass _cv in cv_code1)
                            {
                                List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(string.Empty, _cv.CV_Code, Month_From, Month_To, txt_Year.Text, ProductGroup, Size, UnitType, string.Empty);

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
                                List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(string.Empty, AgentName, Month_From, Month_To, txt_Year.Text, ProductGroup, Size, UnitType, string.Empty);

                                foreach (RPT_AnnualReport_4121 rpt in _inrpt)
                                {
                                    rt_RPT_Order.Add(rpt);
                                }

                            }
                        }
                        
                    }
                    else //เงื่อนไข CV ใน Region
                    {
                        if(Region == string.Empty)
                        {
                            foreach (dbo_AgentClass _cv in cv_code2)
                            {
                                List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(_cv.Location_ID, AgentName, Month_From, Month_To, txt_Year.Text, ProductGroup, Size, UnitType, string.Empty);

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
                                List<RPT_AnnualReport_4121> _inrpt = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, txt_Year.Text, ProductGroup, Size, UnitType, string.Empty);

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
                    rt_RPT_Order = Reports.RPT_AnnualReport_4121(Region, AgentName, Month_From, Month_To, txt_Year.Text, ProductGroup, Size, UnitType, string.Empty);
                }
            

                if (rt_RPT_Order.Count > 0)
                {
                   
                    string url = "../Report/ReportViewer.aspx?RPT=RPT_Order&Region=" + ddlSearchRegion.SelectedValue
                    + "&AgentName=" + ddlAgentName.SelectedValue
                    + "&Month_From=" + ddlMonth_From.SelectedValue
                    + "&Month_To=" + ddlMonth_To.SelectedValue
                    + "&Year=" + txt_Year.Text
                    + "&ProductGroup=" + ddl_ProductGroup.SelectedValue
                    + "&Size=" + ddl_Size.SelectedValue
                    + "&Unit=" + ddlUnit.SelectedValue;

                    string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
                }
                else
                {
                    Messages.Show("ไม่พบข้อมูล กรุณาเลือกเงื่อนไขอีกครั้ง", this.Page);
                }


            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                ddlMonth_From.SelectedIndex = 0;


            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddl_ProductGroup.Items.Clear();
        ddl_Size.Items.Clear();
        //
        SetUpDrowDownList();
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

    }

    protected void ddlSearchRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            if (ddlSearchRegion.SelectedValue.ToString() != null)
            {
                if (ddlSearchRegion.SelectedIndex == 0)
                {

                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if (cv_code1.Count > 0)
                    {

                        ddlAgentName.DataSource = cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        String strString = user_class.Region;
                        String[] myArr = strString.Split(',');

                        Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");
                        String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();
                        //ddlAgentName
                        //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);


                        ddlAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }

                   
                }
                else
                {
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ddlSearchRegion.SelectedValue.ToString(), "Active", string.Empty);
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if (cv_code1.Count > 0)
                    {
                        ddlAgentName.DataSource = cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                        ddlAgentName.DataSource = agent.OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                       
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }


    }

    protected void ddl_ProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_ProductGroup.SelectedIndex > 0)
            {
                List<dbo_ProductClass> Product_Size = dbo_ProductDataClass.Search(string.Empty, string.Empty, ddl_ProductGroup.SelectedValue, null, string.Empty).GroupBy(f => f.Size).Select(grb => grb.First()).OrderBy(f => f.Size).ToList();
                //List<dbo_ProductClass> Product_Size = dbo_ProductDataClass.Search(string.Empty, string.Empty, ddl_Size.SelectedValue.ToString(), null, string.Empty).GroupBy(g => g.Product_group_ID).Select(grb0 => grb0.First()).GroupBy(f => f.Size).Select(grb => grb.First()).ToList();

                ddl_Size.DataSource = Product_Size;
                ddl_Size.DataBind();
                ddl_Size.Items.Insert(0, "เลือกทั้งหมด");
            }
            else
            {
                ddl_Size.Items.Clear();
                ddl_Size.Items.Insert(0, "เลือกทั้งหมด");
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }
    #endregion

    #region Methods
    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);


            if (user_class.User_Group_ID == "CP Meiji")
            {
                //ddlSearchRegion
                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");

                ddlSearchRegion.DataSource = region.Where(f => myArr.Contains(f.Value));
                ddlSearchRegion.DataBind();
                ddlSearchRegion.Items.Insert(0, "เลือกทั้งหมด");

                if (ddlSearchRegion.SelectedIndex == 0)
                {

                    String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if (cv_code1.Count > 0)
                    {

                        ddlAgentName.DataSource = cv_code1;
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {

                        ddlAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                        ddlAgentName.DataBind();
                        ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }



                }
            }
            else
            {
                //ddlSearchRegion
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] rg = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim()).Select(f => f.Location_Region).ToArray();

                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown_Report("07");
                ddlSearchRegion.DataSource = region.Where(f => rg.Contains(f.Value));
                ddlSearchRegion.DataBind();
                ddlSearchRegion.Enabled = false;
                //ddlAgentName
                //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                ddlAgentName.DataSource = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim());
                ddlAgentName.DataBind();
                ddlAgentName.Enabled = false;

            }

            //ddl_ProductGroup     
            List<dbo_ProductClass> ProductGroup = dbo_ProductDataClass.Search(string.Empty, string.Empty, string.Empty, null, string.Empty).GroupBy(f => f.Product_group_ID).Select(grb => grb.First()).ToList();
            dbo_ProductClass b = new dbo_ProductClass();
            dbo_ProductClass c1 = new dbo_ProductClass();
            b.Product_group_ID = "เลือกทั้งหมด";
            c1.Product_group_ID = "อื่นๆ";
            ProductGroup.Insert(0, b);
            ProductGroup.Insert(ProductGroup.Count, c1);
            ddl_ProductGroup.DataSource = ProductGroup;
            ddl_ProductGroup.DataBind();

            ddl_Size.Items.Insert(0, "เลือกทั้งหมด");

            CultureInfo ThaiCulture = new CultureInfo("th-TH");
            DateTime DtNow = new DateTime();
            DtNow = DateTime.Now;
            txt_Year.Text = (DtNow.ToString("yyyy", ThaiCulture));

            ddlMonth_From.SelectedIndex = 0;
            ddlMonth_To.SelectedIndex = 0;
            ddl_Size.SelectedIndex = 0;
            ddlUnit.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    private bool IsValidForm()
    {
        bool ret = true;

        if (Int32.Parse(this.ddlMonth_From.Text) > Int32.Parse(this.ddlMonth_To.Text))
        {
            Messages.Show("โปรดระบุเดือน ให้ถูกต้อง", this.Page);

            ret = false;
        }
        return ret;
    }
    #endregion
}