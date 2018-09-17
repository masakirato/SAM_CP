using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_StockOnHand : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            if (user_class.User_Group_ID != "Agent")
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);
            }
            else
            {
                dbo_AgentClass agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);
                label_agent_name.Text = agent.AgentName;

                show_grid(user_class.CV_CODE);

                lblValue.Text = System.Convert.ToString(string.Format("{0:#,##0.00}", Grand_Total));
            }
        }
    }
    decimal Grand_Total = 0;
    private void show_grid(string CV_Code)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            Session.Remove("GetProduct_Quantity_tab_1");
            List<dbo_ProductClass> item1 = dbo_StockDataClass.GetStockonHandByProductGroupID("นมสดพาสเจอร์ไรส์", CV_Code);
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewStock_Tab1.DataSource = item1;
            GridViewStock_Tab1.DataBind();

            Grand_Total = item1.Sum(f => Convert.ToDecimal(f.Net_Value));

            Session.Remove("GetProduct_Quantity_tab_2");
            List<dbo_ProductClass> item2 = dbo_StockDataClass.GetStockonHandByProductGroupID("นมเปรี้ยว", CV_Code);
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewStock_Tab2.DataSource = item2;
            GridViewStock_Tab2.DataBind();
            Grand_Total = Grand_Total + item2.Sum(f => Convert.ToDecimal(f.Net_Value)); ;

            Session.Remove("GetProduct_Quantity_tab_3");
            List<dbo_ProductClass> item3 = dbo_StockDataClass.GetStockonHandByProductGroupID("โยเกิร์ตเมจิ", CV_Code);
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewStock_Tab3.DataSource = item3;
            GridViewStock_Tab3.DataBind();
            Grand_Total = Grand_Total + item3.Sum(f => Convert.ToDecimal(f.Net_Value)); ;

            Session.Remove("GetProduct_Quantity_tab_4");
            List<dbo_ProductClass> item4 = dbo_StockDataClass.GetStockonHandByProductGroupID("นมเปรี้ยวไพเกน", CV_Code);
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewStock_Tab4.DataSource = item4;
            GridViewStock_Tab4.DataBind();
            Grand_Total = Grand_Total + item4.Sum(f => Convert.ToDecimal(f.Net_Value)); ;

            Session.Remove("GetProduct_Quantity_tab_5");
            List<dbo_ProductClass> item5 = dbo_StockDataClass.GetStockonHandByProductGroupID("อื่นๆ", CV_Code);
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewStock_Tab5.DataSource = item5;
            GridViewStock_Tab5.DataBind();
            Grand_Total = Grand_Total + item5.Sum(f => Convert.ToDecimal(f.Net_Value)); ;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }


    protected void GridViewStock_Tab1_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_1"];
            Session.Remove("GetProduct_Quantity_tab_1");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab1.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 5;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {

                }

            }
        }
        catch (Exception)
        {

        }
    }

    protected void GridViewStock_Tab2_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_2"];
            Session.Remove("GetProduct_Quantity_tab_2");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab2.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 5;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {

                }

            }
        }
        catch (Exception)
        {

        }
    }

    protected void GridViewStock_Tab3_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_3"];
            Session.Remove("GetProduct_Quantity_tab_3");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab3.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 5;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {

                }

            }
        }
        catch (Exception)
        {

        }
    }

    protected void GridViewStock_Tab4_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_4"];
            Session.Remove("GetProduct_Quantity_tab_4");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab4.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 5;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {

                }

            }
        }
        catch (Exception)
        {

        }
    }

    protected void GridViewStock_Tab5_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_5"];
            Session.Remove("GetProduct_Quantity_tab_5");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab5.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 5;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {

                }

            }
        }
        catch (Exception)
        {

        }
    }


}