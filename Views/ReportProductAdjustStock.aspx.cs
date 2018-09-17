﻿#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_ReportProductAdjustStock : System.Web.UI.Page
{
    #region Private Variables
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private string CV_Code;
    private string AgentName;
    private string ProductGroup;
    private string Size;
    private string ProductName;
    private string ProductID;
    private string strCountStockDate_From;
    private string strCountStockDate_To;
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

                List<RPT_ADJ_STOCK__4128> rt_RPT_ADJ_STOCK = new List<RPT_ADJ_STOCK__4128>();
                string User_ID = Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                strCountStockDate_From = CountStockDate_From.Text;
                strCountStockDate_To = CountStockDate_To.Text;
                ProductGroup = ddl_ProductGroup.SelectedValue;
                Size = ddl_Size.SelectedValue;
                CV_Code = user_class.CV_CODE;

                if (ProductGroup == "เลือกทั้งหมด")
                {
                    ProductGroup = string.Empty;
                }                  
                if (Size == "เลือกทั้งหมด")
                {
                    Size = string.Empty;
                }
                             
                if (user_class.User_Group_ID == "CP Meiji")
                {
                    string region = user_class.Region;
                    string[] regions = region.Split(',');
                    //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));
                    foreach (dbo_AgentClass _cv in cv_code2)
                    {
                        //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                        List<RPT_ADJ_STOCK__4128> _inrpt =Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, strCountStockDate_From, strCountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                        

                        foreach (RPT_ADJ_STOCK__4128 rpt in _inrpt)
                        {
                            rt_RPT_ADJ_STOCK.Add(rpt);
                        }

                    }
                }
                else
                {
                    rt_RPT_ADJ_STOCK = Reports.RPT_ADJ_STOCK__4128(string.Empty, CV_Code, strCountStockDate_From, strCountStockDate_To
                     , ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                }

                if (rt_RPT_ADJ_STOCK.Count > 0)
                {                
                    string url = "../Report/ReportViewer.aspx?RPT=ReportProductAdjustStock&CountStockDate_From=" + CountStockDate_From.Text 
                        + "&CountStockDate_To=" + CountStockDate_To.Text
                        + "&ProductGroup=" + ddl_ProductGroup.SelectedValue 
                        + "&Size=" + ddl_Size.SelectedValue
                        + "&CV_Code=" + CV_Code;

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
                CountStockDate_From.Text = DateTime.Now.ToShortDateString();
                CountStockDate_To.Text = DateTime.Now.ToShortDateString();
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
            ddl_Size.SelectedIndex = 0;

            CountStockDate_From.Text = DateTime.Now.ToShortDateString();
            CountStockDate_To.Text = DateTime.Now.ToShortDateString();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    private bool IsValidForm()
    {
        bool ret = true;

        if (this.CountStockDate_From.Text.Trim() != "" && this.CountStockDate_To.Text.Trim() != "")
        {
            if (DateTime.Parse(this.CountStockDate_To.Text) < DateTime.Parse(this.CountStockDate_From.Text))
            {
                Messages.Show("โปรดระบุช่วงวันที่ ให้ถูกต้อง", this.Page);

                ret = false;
            }
        }

        return ret;
    }
    #endregion
}