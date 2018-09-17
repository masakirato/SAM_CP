#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_OtherRequisitionList : System.Web.UI.Page
{
    #region Private Variables
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            txtRequisition_Date.Enabled = false;
            if (user_class.User_Group_ID != "Agent")
            {
                pnlForm.Visible = false;
                pnlGrid.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);

                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);
            }
            else
            {
                pnlForm.Visible = false;
                pnlGrid.Visible = true;
                SetUpDrowDownList();

                hdfPosition.Value = user_class.Position;

                btnSearchSubmit_Click(sender, e);
            }

        }

    }

    protected void TxtId_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        getAllRow();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(HttpContext.Current.Request.Cookies["User_ID"].Value);
        dbo_CountStockClass stock1 = dbo_CountStockDataClass.Search(null, string.Empty, string.Empty, user_class.CV_CODE).FirstOrDefault(f => f.Status == "รอการคอนเฟิร์ม");

        if (stock1 != null)
        {
            logger.Debug(stock1.Status + " " + stock1.Count_No);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ระหว่างการนับสต๊อก ไม่สามารถเบิกสินค้าได้");
        }
        else
        {
            GetDetailsDataToForm(string.Empty, string.Empty);
            //btnSave.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (btnSave.Text == "แก้ไข")
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(HttpContext.Current.Request.Cookies["User_ID"].Value);
            dbo_CountStockClass stock1 = dbo_CountStockDataClass.Search(null, string.Empty, string.Empty, user_class.CV_CODE).FirstOrDefault(f => f.Status == "รอการคอนเฟิร์ม");



            if (stock1 != null)
            {
                logger.Debug(stock1.Status + " " + stock1.Count_No);
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ระหว่างการนับสต๊อก ไม่สามารถเบิกสินค้าได้");
            }
            else
            {

                dbo_OtherRequisitionClass other = dbo_OtherRequisitionDataClass.Select_Record(txtRequisition_No.Text);
                if (Convert.ToDateTime(other.Requisition_Date.Value.ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    Show("ไม่สามารถทำการแก้ไขข้อมูลการเบิกอื่นๆย้อนหลังได้");
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //btnSave.Visible = false;
                }
                else
                {
                    GetDetailsDataToForm(txtRequisition_No.Text, "Edit");
                }


            }



        }
        else
        {
            if (txtRequisition_Date.Text == "")
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุวันที่เบิก");
            }
            else if (ddlReason.SelectedIndex == 0)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุเหตุผล");
            }
            else if (ddlReason.SelectedIndex == 4 && txtOtherReason.Text == "")
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุเหตุผลอื่นๆ");
            }
            else if (ddlUserSP.SelectedIndex == 0 && txtOtherReason2.Text == "")
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุคนที่เบิก");
            }
            else if (Convert.ToInt32(decimal.Parse(txtGrand_Total_Qty.Text.Replace(",", string.Empty))) < 1)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุจำนวนเบิกก่อนการกดบันทึก");
            }

            else
            {

                if (btnSaveMode.Value == "บันทึก")
                {

                    InsertRecord();

                }
                else
                {
                    UpdateRecord();
                }
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show("บันทึกสำเร็จ");

                pnlForm.Visible = false;
                pnlGrid.Visible = true;

                SearchSubmit();
            }

        }
    }

    protected void ddlUserSP_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        /*
        string User_ID = ddlUserSP.SelectedValue;

        DateTime dt = (!string.IsNullOrEmpty(txtRequisition_Date.Text) ? DateTime.Parse(txtRequisition_Date.Text) : DateTime.Now);

        txtGrand_Total_Amount.Text = "0";
        txtGrand_Total_Qty.Text = "0";


        show_grid(User_ID, dt, string.Empty);
        */
        //if (ddlUserSP.SelectedIndex > 0)
        //    btnSave.Visible = true;
        //else
        //    btnSave.Visible = false;
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(500);

        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SearchSubmit();
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SetUpDrowDownList();
        txt_FromDate.Text = DateTime.Now.ToShortDateString();
        txt_ToDate.Text = DateTime.Now.ToShortDateString();
        GridViewOtherRequistion.Visible = false;
        pnlNoRec.Visible = false;
        System.Threading.Thread.Sleep(500);

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        pnlForm.Visible = false;
        pnlGrid.Visible = true;

        SearchSubmit();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
    #endregion

    #region GridView DataBound
    protected void GridViewOtherRequistion_1_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {


            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_1"];
            Session.Remove("GetProduct_Quantity_tab_1");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewOtherRequistion_1.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 7;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    //row.Cells[7].Visible = false;
                    //row.Cells[8].Visible = false;
                    //row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("Label_PricePerUnit");

                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;
                        // _Amount.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewOtherRequistion_2_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_2"];
            Session.Remove("GetProduct_Quantity_tab_2");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewOtherRequistion_2.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 7;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    //row.Cells[7].Visible = false;
                    //row.Cells[8].Visible = false;
                    //row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("Label_PricePerUnit");

                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;
                        //  _Amount.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewOtherRequistion_3_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_3"];
            Session.Remove("GetProduct_Quantity_tab_3");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewOtherRequistion_3.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 7;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    //row.Cells[7].Visible = false;
                    //row.Cells[8].Visible = false;
                    //row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("Label_PricePerUnit");

                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;
                        //  _Amount.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewOtherRequistion_4_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_4"];
            Session.Remove("GetProduct_Quantity_tab_4");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewOtherRequistion_4.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 7;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    //row.Cells[7].Visible = false;
                    //row.Cells[8].Visible = false;
                    //row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("Label_PricePerUnit");

                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;
                        //   _Amount.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewOtherRequistion_5_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_5"];
            Session.Remove("GetProduct_Quantity_tab_5");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewOtherRequistion_5.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 7;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    //row.Cells[7].Visible = false;
                    //row.Cells[8].Visible = false;
                    //row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("Label_PricePerUnit");

                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;
                        //   _Amount.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }
    #endregion

    #region Method
    private void GetDetailsDataToForm(string id, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        pnlForm.Visible = true;
        pnlGrid.Visible = false;

        try
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

            if (Mode == "View")
            {
                btnSaveMode.Value = "แก้ไข";
                btnSave.Text = "แก้ไข";
                btnCancel.Text = "กลับไปหน้าค้นหา";
            }
            else if (Mode == "Edit")
            {
                btnSave.Text = "บันทึก";
                btnSaveMode.Value = "แก้ไข";
                btnCancel.Text = "ยกเลิก";
            }
            else if (string.IsNullOrEmpty(Mode))
            {
                btnSave.Text = "บันทึก";
                btnSaveMode.Value = "บันทึก";
                btnCancel.Text = "ยกเลิก";
            }

            bool enable = Mode != "View";

            if (btnSaveMode.Value == "แก้ไข")
            {
                txtRequisition_Date.Enabled = false;
                ddlReason.Enabled = false;
                ddlUserSP.Enabled = false;
                txtOtherReason.Enabled = false;
                txtOtherReason2.Enabled = false;
            }
            else
            {
                //txtRequisition_Date.Enabled = true;
                txtRequisition_Date.Enabled = false;
                ddlReason.Enabled = true;
                ddlUserSP.Enabled = true;
                txtOtherReason.Enabled = true;
                txtOtherReason2.Enabled = true;
            }

            ddlReason.ClearSelection();
            ddlUserSP.ClearSelection();

            if (!string.IsNullOrEmpty(id))
            {
                dbo_OtherRequisitionClass other = dbo_OtherRequisitionDataClass.Select_Record(id);

                txtCV_Code.Text = other.CV_Code;
                txtRequisition_No.Text = other.Requisition_No;
                txtAgentName.Text = dbo_AgentDataClass.Select_Record(other.CV_Code).AgentName;

                txtRequisition_Date.Text = other.Requisition_Date.Value.ToShortDateString();


                if (ddlReason.Items.FindByText(other.Reason) != null)
                    ddlReason.Items.FindByText(other.Reason).Selected = true;


                String name = dbo_UserDataClass.Select_Record(other.Requisition_Name).FullName_ddl;

                if (ddlUserSP.Items.FindByText(name) != null)
                {
                    ddlUserSP.Items.FindByText(name).Selected = true;
                }



                txtOtherReason.Text = other.Other_reason;
                txtOtherReason2.Text = other.Other_Requisition_Name;
                txtGrand_Total_Qty.Text = other.Grand_Total_Qty.ToString();
                txtGrand_Total_Amount.Text = other.Grand_Total_Amount.ToString();

                if (Mode == "View")
                {
                    show_grid_view(other.Requisition_Name, other.Requisition_Date, other.Requisition_No);
                }
                else
                {
                    show_grid(other.Requisition_Name, DateTime.Now, other.Requisition_No);
                }
                CheckOwnerPosition();
            }
            else
            {

                txtRequisition_Date.Text = DateTime.Now.ToShortDateString();
                txtRequisition_No.Text = GenerateID.Other_Requisition_No(user_class.CV_CODE);
                txtGrand_Total_Qty.Text = "0";
                txtGrand_Total_Amount.Text = "0";
                txtOtherReason.Text = "";
                txtOtherReason2.Text = "";
                show_grid(string.Empty, DateTime.Now, string.Empty);

                CheckOwnerPosition();

            }

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void show_grid(string User_ID, DateTime? pricedate, String Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

        try
        {
            Session.Remove("GetProduct_Quantity_tab_1");
            List<dbo_ProductClass> item1 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID(User_ID, pricedate, "นมสดพาสเจอร์ไรส์", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewOtherRequistion_1.DataSource = item1;
            GridViewOtherRequistion_1.DataBind();

            Session.Remove("GetProduct_Quantity_tab_2");
            List<dbo_ProductClass> item2 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID(User_ID, pricedate, "นมเปรี้ยว", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewOtherRequistion_2.DataSource = item2;
            GridViewOtherRequistion_2.DataBind();

            Session.Remove("GetProduct_Quantity_tab_3");
            List<dbo_ProductClass> item3 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID(User_ID, pricedate, "โยเกิร์ตเมจิ", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewOtherRequistion_3.DataSource = item3;
            GridViewOtherRequistion_3.DataBind();

            Session.Remove("GetProduct_Quantity_tab_4");
            List<dbo_ProductClass> item4 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID(User_ID, pricedate, "นมเปรี้ยวไพเกน", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewOtherRequistion_4.DataSource = item4;
            GridViewOtherRequistion_4.DataBind();

            Session.Remove("GetProduct_Quantity_tab_5");
            List<dbo_ProductClass> item5 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID(User_ID, pricedate, "อื่นๆ", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewOtherRequistion_5.DataSource = item5;
            GridViewOtherRequistion_5.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void show_grid_view(string User_ID, DateTime? pricedate, String Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

        try
        {
            Session.Remove("GetProduct_Quantity_tab_1");
            List<dbo_ProductClass> item1 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID_View(User_ID, pricedate, "นมสดพาสเจอร์ไรส์", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewOtherRequistion_1.DataSource = item1;
            GridViewOtherRequistion_1.DataBind();

            Session.Remove("GetProduct_Quantity_tab_2");
            List<dbo_ProductClass> item2 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID_View(User_ID, pricedate, "นมเปรี้ยว", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewOtherRequistion_2.DataSource = item2;
            GridViewOtherRequistion_2.DataBind();

            Session.Remove("GetProduct_Quantity_tab_3");
            List<dbo_ProductClass> item3 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID_View(User_ID, pricedate, "โยเกิร์ตเมจิ", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewOtherRequistion_3.DataSource = item3;
            GridViewOtherRequistion_3.DataBind();

            Session.Remove("GetProduct_Quantity_tab_4");
            List<dbo_ProductClass> item4 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID_View(User_ID, pricedate, "นมเปรี้ยวไพเกน", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewOtherRequistion_4.DataSource = item4;
            GridViewOtherRequistion_4.DataBind();

            Session.Remove("GetProduct_Quantity_tab_5");
            List<dbo_ProductClass> item5 = dbo_OtherRequisitionDataClass.GetOtherRequisitionByProductGroupID_View(User_ID, pricedate, "อื่นๆ", user_class.CV_CODE, Requisition_No);
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewOtherRequistion_5.DataSource = item5;
            GridViewOtherRequistion_5.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }


    private void CheckOwnerPosition()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
        if (user_class.Position != "เจ้าของ")
        {
            // txtGrand_Total_Amount.Style.Add("display", "none");
            try
            {
                foreach (GridViewRow currentRow in GridViewOtherRequistion_1.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("Label_PricePerUnit");
                    // lbl_Price_.Text = "0.00";

                    lbl_Price_.Style.Add("display", "none");

                    //Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    //lbl_Amount_.Text = "0.00";
                }

            }
            catch (Exception) { }

            try
            {
                foreach (GridViewRow currentRow in GridViewOtherRequistion_2.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("Label_PricePerUnit");
                    //lbl_Price_.Text = "0.00";
                    lbl_Price_.Style.Add("display", "none");
                    //Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    //lbl_Amount_.Text = "0.00";
                }

            }
            catch (Exception) { }

            try
            {
                foreach (GridViewRow currentRow in GridViewOtherRequistion_3.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("Label_PricePerUnit");
                    lbl_Price_.Text = "0.00";
                    //Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    //lbl_Amount_.Text = "0.00";
                }

            }
            catch (Exception) { }

            try
            {
                foreach (GridViewRow currentRow in GridViewOtherRequistion_4.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("Label_PricePerUnit");
                    //lbl_Price_.Text = "0.00";
                    lbl_Price_.Style.Add("display", "none");
                    //Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    //lbl_Amount_.Text = "0.00";
                }
            }
            catch (Exception) { }

            try
            {
                foreach (GridViewRow currentRow in GridViewOtherRequistion_5.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("Label_PricePerUnit");
                    // lbl_Price_.Text = "0.00";
                    lbl_Price_.Style.Add("display", "none");
                    //Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    //lbl_Amount_.Text = "0.00";
                }
            }
            catch (Exception) { }
        }
    }



    private List<dbo_OtherRequisitionDetailClass> GetListOfOtherRequisition(string Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_OtherRequisitionDetailClass> listofother = new List<dbo_OtherRequisitionDetailClass>();
        int index = 1;
        try
        {

            logger.Debug("GridViewOtherRequistion_1");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_1.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                {

                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;
                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text == "" ? "0" : txt.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);

                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            logger.Debug("GridViewOtherRequistion_2");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_2.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                {
                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;

                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text == "" ? "0" : txt.Text);
                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            logger.Debug("GridViewOtherRequistion_3");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_3.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                {

                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;

                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text);
                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            logger.Debug("GridViewOtherRequistion_4");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_4.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                {

                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;

                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text);
                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            logger.Debug("GridViewOtherRequistion_5");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_5.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                {

                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;

                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text);
                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        return listofother;
    }

    private List<dbo_OtherRequisitionDetailClass> GetListOfOtherRequisitionforUpdate(string Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_OtherRequisitionDetailClass> listofother = new List<dbo_OtherRequisitionDetailClass>();
        int index = 1;
        try
        {

            logger.Debug("GridViewOtherRequistion_1");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_1.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label __Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                if (__Label_Product_ID.Text != "Merge")
                {
                    //if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                    //{

                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;
                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text == "" ? "0" : txt.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);

                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);


                    if (detail.Requisition_Qty > 0)
                        listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            logger.Debug("GridViewOtherRequistion_2");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_2.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label __Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                if (__Label_Product_ID.Text != "Merge")
                {
                    //if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                    //{
                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;

                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text == "" ? "0" : txt.Text);
                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    if (detail.Requisition_Qty > 0)
                        listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            logger.Debug("GridViewOtherRequistion_3");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_3.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                {

                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;

                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text);
                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    if (detail.Requisition_Qty > 0)
                        listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            logger.Debug("GridViewOtherRequistion_4");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_4.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                {

                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;

                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text);
                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    if (detail.Requisition_Qty > 0)
                        listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            logger.Debug("GridViewOtherRequistion_5");
            foreach (GridViewRow currentRow in GridViewOtherRequistion_5.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && short.Parse(txt.Text) > 0)
                {

                    Label _Vat = (Label)currentRow.FindControl("lblVat");
                    Label _Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");
                    Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");
                    Label lblOther_Requisition_Detail_ID = (Label)currentRow.FindControl("lblOther_Requisition_Detail_ID");
                    Label lblStock_on_Hand_ID = (Label)currentRow.FindControl("lblStock_on_Hand_ID");
                    Label lblOld_Qty = (Label)currentRow.FindControl("lblOldQty");
                    Label lblStock = (Label)currentRow.FindControl("Label_Stock_on_hand");

                    dbo_OtherRequisitionDetailClass detail = new dbo_OtherRequisitionDetailClass();
                    detail.Requisition_No = Requisition_No;
                    detail.Stock_ID = lblStock_on_Hand_ID.Text;

                    if (string.IsNullOrEmpty(lblOther_Requisition_Detail_ID.Text))
                    {
                        detail.Other_Requisition_Detail_ID = Requisition_No + index.ToString("00");
                    }
                    else
                    {
                        detail.Other_Requisition_Detail_ID = lblOther_Requisition_Detail_ID.Text;
                    }

                    index++;
                    detail.Requisition_Qty = short.Parse(txt.Text);
                    detail.Vat = (string.IsNullOrEmpty(_Vat.Text) ? Byte.Parse("0") : Byte.Parse(_Vat.Text));
                    detail.Product_ID = _Label_Product_ID.Text;
                    detail.Price = Decimal.Parse(lblPrice.Text);
                    if (!string.IsNullOrEmpty(lblOld_Qty.Text))
                        detail.Old_Qty = short.Parse(lblOld_Qty.Text);
                    detail.Stock_on_Hand = Int32.Parse(lblStock.Text == "" ? "0" : lblStock.Text);

                    if (detail.Requisition_Qty > 0)
                        listofother.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        return listofother;
    }

    private void InsertRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            dbo_OtherRequisitionClass requisition = new dbo_OtherRequisitionClass();
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            requisition.Requisition_No = txtRequisition_No.Text;
            requisition.Reason = (ddlReason.SelectedIndex == 0 ? string.Empty : ddlReason.SelectedValue);
            requisition.CV_Code = user_class.CV_CODE;
            //requisition.Requisition_Date = DateTime.Now;
            requisition.Requisition_Date = Convert.ToDateTime(txtRequisition_Date.Text);
            requisition.Grand_Total_Amount = decimal.Parse(txtGrand_Total_Amount.Text.Replace(",", string.Empty));
            requisition.Grand_Total_Qty = Convert.ToInt32(decimal.Parse(txtGrand_Total_Qty.Text.Replace(",", string.Empty)));
            requisition.Requisition_Name = ddlUserSP.SelectedValue;
            requisition.Other_Requisition_Name = txtOtherReason2.Text;
            requisition.Other_reason = txtOtherReason.Text;

            dbo_OtherRequisitionDataClass.Add(requisition, HttpContext.Current.Request.Cookies["User_ID"].Value);

            logger.Debug("dbo_OtherRequisitionDataClass.Add(requisition);");

            List<dbo_OtherRequisitionDetailClass> listofother = GetListOfOtherRequisition(requisition.Requisition_No);
            foreach (dbo_OtherRequisitionDetailClass other in listofother)
            {
                dbo_OtherRequisitionDetailDataClass.Add(other, HttpContext.Current.Request.Cookies["User_ID"].Value);


                String Product_ID = other.Product_ID;

                List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);

                logger.Debug("prev_stock.Count " + prev_stock.Count);
                if (prev_stock.Count > 0)
                {
                    dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                    logger.Debug("stock.Date " + stock.Date);

                    stock.Stock_Out = short.Parse((stock.Stock_Out + other.Requisition_Qty).ToString());
                    stock.Stock_End = short.Parse((stock.Stock_End - other.Requisition_Qty).ToString());
                    //stock.Product_ID = Product_ID;

                    dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                }

                dbo_StockMovementClass movement = new dbo_StockMovementClass();
                movement.CV_CODE = user_class.CV_CODE;
                movement.Date = DateTime.Now;
                movement.Movement_Type = "เบิกอื่นๆ";
                movement.Product_List_ID = Product_ID;
                movement.Qty = short.Parse(other.Requisition_Qty.ToString());
                movement.Ref_No = requisition.Requisition_No;

                dbo_StockMovementDataClass.Add(movement);
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void UpdateRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {

            dbo_OtherRequisitionClass requisition = dbo_OtherRequisitionDataClass.Select_Record(txtRequisition_No.Text);
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            requisition.Requisition_No = txtRequisition_No.Text;
            requisition.Reason = (ddlReason.SelectedIndex == 0 ? string.Empty : ddlReason.SelectedValue);
            requisition.CV_Code = user_class.CV_CODE;
            //requisition.Requisition_Date = DateTime.Now;
            requisition.Requisition_Date = Convert.ToDateTime(txtRequisition_Date.Text);
            requisition.Grand_Total_Amount = decimal.Parse(txtGrand_Total_Amount.Text.Replace(",", string.Empty));
            requisition.Grand_Total_Qty = Convert.ToInt32(decimal.Parse(txtGrand_Total_Qty.Text.Replace(",", string.Empty)));
            requisition.Requisition_Name = ddlUserSP.SelectedValue;

            dbo_OtherRequisitionDataClass.Update(requisition);

            List<dbo_OtherRequisitionDetailClass> listDetail = dbo_OtherRequisitionDetailDataClass.Search(requisition.Requisition_No);
            if (listDetail.Count > 0)
            {
                foreach (dbo_OtherRequisitionDetailClass other in listDetail)
                {
                    String Product_ID = other.Product_ID;

                    List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);

                    logger.Debug("prev_stock.Count " + prev_stock.Count);
                    if (prev_stock.Count > 0)
                    {
                        dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                        logger.Debug("stock.Date " + stock.Date);

                        stock.Stock_Out = short.Parse((stock.Stock_Out - other.Requisition_Qty).ToString());
                        stock.Stock_End = short.Parse((stock.Stock_End + other.Requisition_Qty).ToString());
                        //stock.Product_ID = Product_ID;

                        dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                    }

                    dbo_OtherRequisitionDetailDataClass.Delete(other);
                }
            }

            List<dbo_OtherRequisitionDetailClass> listofother = GetListOfOtherRequisitionforUpdate(requisition.Requisition_No);
            //List<dbo_OtherRequisitionDetailClass> listofother = GetListOfOtherRequisition(requisition.Requisition_No);

            dbo_StockMovementDataClass.DeleteByRefNo(requisition.Requisition_No);

            foreach (dbo_OtherRequisitionDetailClass other in listofother)
            {
                //if (other.Old_Qty != null && other.Requisition_Qty == 0)
                //    dbo_OtherRequisitionDetailDataClass.Delete(other);
                //else if (other.Old_Qty > 0 && other.Requisition_Qty > 0)
                //    dbo_OtherRequisitionDetailDataClass.Update(other);
                //else if (other.Old_Qty == null && other.Requisition_Qty > 0)
                //    dbo_OtherRequisitionDetailDataClass.Add(other, HttpContext.Current.Request.Cookies["User_ID"].Value);

                //logger.Debug("other.Stock_ID " + other.Stock_ID);
                dbo_OtherRequisitionDetailDataClass.Add(other, HttpContext.Current.Request.Cookies["User_ID"].Value);


                String Product_ID = other.Product_ID;

                List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);

                logger.Debug("prev_stock.Count " + prev_stock.Count);
                if (prev_stock.Count > 0)
                {
                    dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                    logger.Debug("stock.Date " + stock.Date);

                    stock.Stock_Out = short.Parse((stock.Stock_Out + other.Requisition_Qty).ToString());
                    stock.Stock_End = short.Parse((stock.Stock_End - other.Requisition_Qty).ToString());

                    dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                }


                /*
                dbo_StockClass stock = dbo_StockDataClass.Select_Record(other.Stock_ID);
                if (stock != null)
                {
                    int? oldqty = other.Old_Qty == null ? 0 : other.Old_Qty;

                    if (other.Requisition_Qty > oldqty)
                    {
                        stock.Stock_Out -= short.Parse((other.Requisition_Qty - other.Old_Qty).ToString());
                        stock.Stock_End -= short.Parse((other.Requisition_Qty - other.Old_Qty).ToString());
                    }
                    else
                    {
                        stock.Stock_Out += short.Parse((other.Old_Qty - other.Requisition_Qty).ToString());
                        stock.Stock_End += short.Parse((other.Requisition_Qty - other.Old_Qty).ToString());
                    }
                    dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                }
                */


                if (other.Requisition_Qty > 0)
                {
                    dbo_StockMovementClass movement = new dbo_StockMovementClass();
                    movement.CV_CODE = user_class.CV_CODE;
                    movement.Date = DateTime.Now;
                    movement.Movement_Type = "เบิกอื่นๆ";
                    movement.Product_List_ID = other.Product_ID;
                    movement.Qty = short.Parse(other.Requisition_Qty.ToString());
                    movement.Ref_No = requisition.Requisition_No;

                    dbo_StockMovementDataClass.Add(movement);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void getAllRow()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        Decimal sum_cal_amount_price = 0;


        int sum_amount = 0;
        try
        {
            foreach (GridViewRow currentRow in GridViewOtherRequistion_1.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (int.Parse(txt.Text) < 0)
                {
                    if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                    {
                        amount = int.Parse(txt.Text);
                        Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");

                        Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                        Decimal cal_amount_price = (amount * Price);

                        sum_amount += amount;
                        sum_cal_amount_price += cal_amount_price;
                    }
                }
                else
                {
                    Show("ไม่สามารถกรอกจำนวนที่เบิกน้อยกว่าศูนย์");
                }
            }
        }
        catch (Exception)
        {

        }

        try
        {
            foreach (GridViewRow currentRow in GridViewOtherRequistion_2.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (int.Parse(txt.Text) < 0)
                {
                    if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                    {
                        amount = int.Parse(txt.Text);
                        Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");

                        Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                        Decimal cal_amount_price = (amount * Price);

                        sum_amount += amount;
                        sum_cal_amount_price += cal_amount_price;
                    }
                }
                else
                {
                    Show("ไม่สามารถกรอกจำนวนที่เบิกน้อยกว่าศูนย์");
                }
            }
        }
        catch (Exception)
        {

        }

        try
        {
            foreach (GridViewRow currentRow in GridViewOtherRequistion_3.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (int.Parse(txt.Text) < 0)
                {
                    if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                    {
                        amount = int.Parse(txt.Text);
                        Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");

                        Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                        Decimal cal_amount_price = (amount * Price);

                        sum_amount += amount;
                        sum_cal_amount_price += cal_amount_price;
                    }
                }
                else
                {
                    Show("ไม่สามารถกรอกจำนวนที่เบิกน้อยกว่าศูนย์");
                }
            }
        }
        catch (Exception)
        {

        }

        try
        {
            foreach (GridViewRow currentRow in GridViewOtherRequistion_4.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (int.Parse(txt.Text) < 0)
                {
                    if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                    {
                        amount = int.Parse(txt.Text);
                        Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");

                        Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                        Decimal cal_amount_price = (amount * Price);

                        sum_amount += amount;
                        sum_cal_amount_price += cal_amount_price;
                    }
                }
                else
                {
                    Show("ไม่สามารถกรอกจำนวนที่เบิกน้อยกว่าศูนย์");
                }
            }
        }
        catch (Exception)
        {

        }

        try
        {
            foreach (GridViewRow currentRow in GridViewOtherRequistion_5.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (int.Parse(txt.Text) < 0)
                {
                    if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                    {
                        amount = int.Parse(txt.Text);
                        Label lblPrice = (Label)currentRow.FindControl("Label_PricePerUnit");

                        Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                        Decimal cal_amount_price = (amount * Price);

                        sum_amount += amount;
                        sum_cal_amount_price += cal_amount_price;
                    }
                }
                else
                {
                    Show("ไม่สามารถกรอกจำนวนที่เบิกน้อยกว่าศูนย์");
                }
            }
        }
        catch (Exception)
        {

        }
        txtGrand_Total_Amount.Text = Decimal.Round(sum_cal_amount_price, 2).ToString("#,##0.##");
        txtGrand_Total_Qty.Text = sum_amount.ToString();


        //txtVat_amount.Text = Decimal.Round(sum_vat, 2).ToString("#,##0.##");
        //txtTotal_Amount_before_vat_included.Text = Decimal.Round(sum_cal_amount_price, 2).ToString("#,##0.##");
        //txtTotal_amount_after_vat_included.Text = (Decimal.Round(sum_vat, 2) + Decimal.Round(sum_cal_amount_price, 2)).ToString("#,##0.##");

    }

    private void SearchSubmit()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            DateTime? StartDate = null;
            DateTime? EndDate = null;

            if (!string.IsNullOrEmpty(txt_FromDate.Text))
            {
                StartDate = DateTime.Parse(txt_FromDate.Text);
            }
            if (!string.IsNullOrEmpty(txt_ToDate.Text))
            {
                EndDate = DateTime.Parse(txt_ToDate.Text);
            }

            String Requistion_Name = (ddlSearchUserSP.SelectedIndex == 0 ? string.Empty : ddlSearchUserSP.SelectedValue);
            String Reason = (ddlSearchReason.SelectedIndex == 0 ? string.Empty : ddlSearchReason.SelectedValue);
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            List<dbo_OtherRequisitionClass> item = dbo_OtherRequisitionDataClass.Search(StartDate, EndDate, Requistion_Name, Reason, user_class.CV_CODE);

            if (item.Count > 0)
            {
                GridViewOtherRequistion.DataSource = item;
                GridViewOtherRequistion.DataBind();

                GridViewOtherRequistion.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewOtherRequistion.Visible = false;
                pnlNoRec.Visible = true;
            }

            foreach (GridViewRow row in GridViewOtherRequistion.Rows)
            {
                Label Label_Requisition_Date = (Label)row.FindControl("Label_Requisition_Date");
                LinkButton lnkB_Delete = (LinkButton)row.FindControl("lnkB");
                string datenow = DateTime.Now.ToShortDateString();

                if (Convert.ToDateTime(Label_Requisition_Date.Text) < Convert.ToDateTime(datenow))
                {
                    lnkB_Delete.Visible = false;
                }



            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

            txtAgentName.Text = clsdbo_Agent.AgentName;
            txtCV_Code.Text = clsdbo_Agent.CV_Code;

            List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
           , string.Empty, string.Empty, string.Empty, clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code, null, string.Empty, string.Empty)
           .Where(f => f.Status == "Active").Where(f => f.Position == "ซุปเปอร์ไวซ์เซอร์" || f.Position == "สาวส่งนม").ToList();



            users.Insert(0, new dbo_UserClass() { FullName_ddl = "==ระบุ==", User_ID = string.Empty });

            ddlUserSP.DataSource = users;
            ddlUserSP.DataBind();
            ddlSearchUserSP.DataSource = users;
            ddlSearchUserSP.DataBind();

            txt_FromDate.Text = DateTime.Now.ToShortDateString();
            txt_ToDate.Text = DateTime.Now.ToShortDateString();

            ddlSearchReason.ClearSelection();

            List<dbo_OtherRequisitionClass> item = new List<dbo_OtherRequisitionClass>();
            GridViewOtherRequistion.DataSource = item;
            GridViewOtherRequistion.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void Show(string message)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        logger.Info(message);
        try
        {


            string cleanMessage = message.Replace("'", "\'");
            // Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            //if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            //{
            //  page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception)
        {

        }
        //}
    }
    #endregion

    #region GirdView Row Command
    protected void GridViewOtherRequistion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            switch (e.CommandName)
            {
                case "Print":

                    LinkButton lnkRequisition_No = (LinkButton)e.CommandSource;
                    string ORequisition_No_ = lnkRequisition_No.CommandArgument;

                    string url = "../Report_From/ViewsReport.aspx?RPT=Other_Requisition_No&PRM=" + ORequisition_No_;
                    string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    System.Threading.Thread.Sleep(2000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);



                    break;
                case "View":

                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string Requisition_No = lnkView.CommandArgument;

                    dbo_OtherRequisitionClass other = dbo_OtherRequisitionDataClass.Select_Record(Requisition_No);
                    if (Convert.ToDateTime(other.Requisition_Date.Value.ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    {
                        //Show("ไม่สามารถทำการแก้ไขข้อมูลการเบิกอื่นๆย้อนหลังได้");
                        //System.Threading.Thread.Sleep(500);
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        btnSave.Visible = false;

                    }
                    
                     GetDetailsDataToForm(Requisition_No, "View");




                    break;
                case "_Delete":
                    //int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
                    //LinkButton LinkButton_Requisition_No = (LinkButton)GridViewOtherRequistion.Rows[RowIndex].FindControl("LinkButton_Requisition_No");

                    LinkButton lnkView_ = (LinkButton)e.CommandSource;
                    string Requisition_No_ = lnkView_.CommandArgument;

                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

                    dbo_CountStockClass stockCheck = dbo_CountStockDataClass.Search(null, string.Empty, string.Empty, user_class.CV_CODE).FirstOrDefault(f => f.Status == "รอการคอนเฟิร์ม");

                    if (stockCheck != null)
                    {
                        logger.Debug(stockCheck.Status + " " + stockCheck.Count_No);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("ระหว่างการนับสต๊อก ไม่สามารถลบใบเบิกสินค้าได้");
                        return;
                    }

                    List<dbo_OtherRequisitionDetailClass> detail = dbo_OtherRequisitionDetailDataClass.Search(Requisition_No_);

                    foreach (dbo_OtherRequisitionDetailClass _detail in detail)
                    {
                        String Product_ID = _detail.Product_ID;

                        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);
                        if (prev_stock.Count > 0)
                        {
                            dbo_StockClass stock = prev_stock[prev_stock.Count - 1];
                            stock.Stock_Out = short.Parse((stock.Stock_Out - _detail.Requisition_Qty).ToString());
                            stock.Stock_End = short.Parse((stock.Stock_End + _detail.Requisition_Qty).ToString());

                            dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }
                        dbo_OtherRequisitionDetailDataClass.Delete(_detail);
                    }

                    //dbo_OtherRequisitionDetailDataClass.DeletebyTimeNo(LinkButton_Requisition_No.Text, Label_Time_No.Text);
                    dbo_OtherRequisitionClass req = new dbo_OtherRequisitionClass();
                    req = dbo_OtherRequisitionDataClass.Select_Record(Requisition_No_);
                    dbo_OtherRequisitionDataClass.Delete(req);

                    List<dbo_OtherRequisitionDetailClass> listofother = GetListOfOtherRequisitionforUpdate(Requisition_No_);

                    if (listofother.Count > 0)
                    {
                        dbo_StockMovementDataClass.DeleteByRefNo(Requisition_No_);
                    }
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                    Show("ลบข้อมูลสำเร็จ");

                    btnSearchSubmit_Click(null, null);
                    pnlGrid.Visible = true;
                    pnlForm.Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion

    #region GridView Row DataBound
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewOtherRequistion.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewOtherRequistion.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewOtherRequistion_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewOtherRequistion.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewOtherRequistion.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewOtherRequistion.PageIndex)
                {
                    item.Selected = true;
                }

                // Add the ListItem object to the Items collection of the 
                // DropDownList.
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = GridViewOtherRequistion.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewOtherRequistion.PageCount.ToString();

        }
    }

    protected void GridViewOtherRequistion_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label Label_PricePerUnit = e.Row.FindControl("Label_PricePerUnit") as Label;
            Label Label_Stock_on_hand = e.Row.FindControl("Label_Stock_on_hand") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");

        }
    }

    protected void GridViewOtherRequistion_2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label Label_PricePerUnit = e.Row.FindControl("Label_PricePerUnit") as Label;
            Label Label_Stock_on_hand = e.Row.FindControl("Label_Stock_on_hand") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");

        }
    }

    protected void GridViewOtherRequistion_3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label Label_PricePerUnit = e.Row.FindControl("Label_PricePerUnit") as Label;
            Label Label_Stock_on_hand = e.Row.FindControl("Label_Stock_on_hand") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");

        }
    }

    protected void GridViewOtherRequistion_4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label Label_PricePerUnit = e.Row.FindControl("Label_PricePerUnit") as Label;
            Label Label_Stock_on_hand = e.Row.FindControl("Label_Stock_on_hand") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");

        }
    }

    protected void GridViewOtherRequistion_5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label Label_PricePerUnit = e.Row.FindControl("Label_PricePerUnit") as Label;
            Label Label_Stock_on_hand = e.Row.FindControl("Label_Stock_on_hand") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + Label_PricePerUnit.ClientID + "," + Label_Stock_on_hand.ClientID + ");");

        }
    }
    #endregion


    protected void txtRequisition_Date_TextChanged(object sender, EventArgs e)
    {
        DateTime Requisition_Date = Convert.ToDateTime(txtRequisition_Date.Text);
        DateTime today = DateTime.Today;
        if (Requisition_Date < today)
        {
            //System.Threading.Thread.Sleep(500);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ไม่สามารถระบุวันที่ย้อนหลังได้");
            txtRequisition_Date.Text = DateTime.Now.ToShortDateString();
        }
    }

    protected void GridViewOtherRequistion_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }
}