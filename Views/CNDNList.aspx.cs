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

public partial class Views_CNDNList : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion 

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

                if (user_class.User_Group_ID != "Agent")
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);
                }
                else
                {
                    ViewState["CV_Code"] = clsdbo_Agent.CV_Code;
                }

                btnSearchSubmit_Click(sender, e);

                foreach (GridViewRow row in GridViewCNDN.Rows)
                {
                    Label _lbl_CNDN_Date = (Label)row.FindControl("lbl_CNDN_Date");
                    LinkButton _lnkBConfirmCNDN = (LinkButton)row.FindControl("lnkBConfirmCNDN");

                    if(_lbl_CNDN_Date.Text == "" || _lbl_CNDN_Date.Text ==null)
                    {
                        _lnkBConfirmCNDN.Visible = false;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }



    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            //ShowCNDNList();
            btnSearchSubmit_Click(null, null);
            pnlGrid.Visible = true;
            pnlForm.Visible = false;

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (btnSave.Text == "แก้ไข")
        {
            if (txtSAM_CN_DN_Status.Text == "คอนเฟิร์มแล้ว")
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("สถานะใบเพิ่มหนี้/ลดหนี้ถูกคอนเฟิร์มแล้ว ไม่สามรถแก้ไขใบเพิ่มหนี้/ลดหนี้ได้");
            }
            else
            {
                GetDetailsDataToForm(txtSAM_CN_DN_No.Text, "Edit");

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
        }
        else
        {
            //if (ddlSAM_CN_DN_Type.SelectedIndex > 0)
            //{
            if (btnSaveMode.Value == "บันทึก")
            {
                try
                {
                    string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                    dbo_BillingClass billing = dbo_BillingDataClass.Select_Record(hdnBilling_ID.Value);

                    dbo_CNDNClass cndn = new dbo_CNDNClass();

                    cndn.Billing_ID = billing.Billing_ID;

                    cndn.CV_Code = user_class.CV_CODE;


                    cndn.Invoice_No = billing.Invoice_No;
                    cndn.Invoice_Date = billing.Invoice_Date;
                    cndn.SAM_CN_DN_Quantity = short.Parse(txtSAM_CN_DN_Quantity.Text);
                    cndn.SAM_CN_DN_Status = "ยังไม่คอนเฟิร์ม";
                    cndn.SAM_CN_DN_No = txtSAM_CN_DN_No.Text;
                    cndn.SAM_CN_DN_Type = ddlSAM_CN_DN_Type.SelectedValue;
                    cndn.SAM_CN_DN_Date = DateTime.Now.Date;

                    dbo_CNDNDataClass.Add(cndn, HttpContext.Current.Request.Cookies["User_ID"].Value);

                    List<dbo_CNDNDetailClass> listofCNDN = SetCNDNDetail(cndn.SAM_CN_DN_No);

                    foreach (dbo_CNDNDetailClass cndndetail in listofCNDN)
                    {
                        dbo_CNDNDetailDataClass.Add(cndndetail, HttpContext.Current.Request.Cookies["User_ID"].Value);
                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            else
            {

                dbo_BillingClass billing = dbo_BillingDataClass.Select_Record(hdnBilling_ID.Value);

                dbo_CNDNClass cndn = dbo_CNDNDataClass.Select_Record(txtSAM_CN_DN_No.Text);

                cndn.Billing_ID = billing.Billing_ID;
                cndn.CV_Code = billing.CV_Number;
                cndn.Invoice_No = billing.Invoice_No;
                cndn.Invoice_Date = billing.Invoice_Date;

                cndn.SAM_CN_DN_Quantity = short.Parse(txtSAM_CN_DN_Quantity.Text);
                cndn.SAM_CN_DN_Status = "ยังไม่คอนเฟิร์ม";
                cndn.SAM_CN_DN_No = txtSAM_CN_DN_No.Text;
                cndn.SAM_CN_DN_Type = ddlSAM_CN_DN_Type.SelectedValue;
                cndn.SAM_CN_DN_Date = DateTime.Now.Date;

                dbo_CNDNDataClass.Update(cndn);


                List<dbo_CNDNDetailClass> listofCNDN = SetCNDNDetail(cndn.SAM_CN_DN_No);


                List<dbo_CNDNDetailClass> del_cndn_detail = dbo_CNDNDetailDataClass.Search(cndn.SAM_CN_DN_No);

                foreach (dbo_CNDNDetailClass cndn_detail in del_cndn_detail)
                {
                    dbo_CNDNDetailDataClass.Delete(cndn_detail.CNDN_Detail_ID);
                }



                foreach (dbo_CNDNDetailClass cndndetail in listofCNDN)
                {
                    dbo_CNDNDetailDataClass.Add(cndndetail, HttpContext.Current.Request.Cookies["User_ID"].Value);

                }


            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

            Show("บันทึกสำเร็จ");

            btnSearchSubmit_Click(null, null);
            pnlGrid.Visible = true;
            pnlForm.Visible = false;

        }
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            String Billing_ID = txtSearchBilling_ID.Text;
            String Invoice_No = txtSearchInvoice_No.Text;

            DateTime? BillingDate = (!string.IsNullOrEmpty(txtSearchBillingDate.Text) ? DateTime.Parse(txtSearchBillingDate.Text) : (DateTime?)null);
            DateTime? Invoice_Date = (!string.IsNullOrEmpty(txtSearchInvoice_Date.Text) ? DateTime.Parse(txtSearchInvoice_Date.Text) : (DateTime?)null);

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_CNDNClass> item = dbo_CNDNDataClass.Search(Billing_ID, BillingDate, Invoice_No, Invoice_Date, user_class.CV_CODE);

            if (item.Count > 0)
            {
                GridViewCNDN.DataSource = item;
                GridViewCNDN.DataBind();

                GridViewCNDN.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewCNDN.Visible = false;
                pnlNoRec.Visible = true;
            }


            foreach (GridViewRow row in GridViewCNDN.Rows)
            {
                Label _lbl_CNDN_Date = (Label)row.FindControl("lbl_CNDN_Date");
                LinkButton _lnkBConfirmCNDN = (LinkButton)row.FindControl("lnkBConfirmCNDN");

                if (_lbl_CNDN_Date.Text == "" || _lbl_CNDN_Date.Text == null)
                {
                    _lnkBConfirmCNDN.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            txtSearchBilling_ID.Text = string.Empty;
            txtSearchBillingDate.Text = string.Empty;
            txtSearchInvoice_No.Text = string.Empty;
            txtSearchInvoice_Date.Text = string.Empty;
            if(GridViewCNDN.Rows.Count > 0)
            {
                List<dbo_CNDNClass> itm = new List<dbo_CNDNClass>();
                GridViewCNDN.DataSource = itm;
                GridViewCNDN.DataBind();
                GridViewCNDN.Visible = false;
            }
           
            pnlNoRec.Visible = false;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlSAM_CN_DN_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

            if (ddlSAM_CN_DN_Type.SelectedValue == "CN")
            {
                txtSAM_CN_DN_No.Text = GenerateID.SAM_CN_No(user_class.CV_CODE);
                txtSAM_CN_DN_Date.Text = DateTime.Now.ToShortDateString();
            }
            else if (ddlSAM_CN_DN_Type.SelectedValue == "DN")
            {
                txtSAM_CN_DN_No.Text = GenerateID.SAM_DN_No(user_class.CV_CODE);
                txtSAM_CN_DN_Date.Text = DateTime.Now.ToShortDateString();
            }
            txtSAM_CN_DN_Status.Text = "ยังไม่คอนเฟิร์ม";
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    protected void TxtId_TextChanged(object sender, EventArgs e)
    {
        int all_sum = 0;
        // Decimal all_sum_dec = 0;
        try
        {
            foreach (GridViewRow currentRow in GridViewCNDN_1.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text) > 0)
                {
                    Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");
                    Label lbl_Total_Qty_ = (Label)currentRow.FindControl("lbl_Total_Qty");

                    all_sum += int.Parse((txt.Text).ToString());

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {


            foreach (GridViewRow currentRow in GridViewCNDN_2.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text) > 0)
                {
                    Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");
                    Label lbl_Total_Qty_ = (Label)currentRow.FindControl("lbl_Total_Qty");

                    all_sum += int.Parse((txt.Text).ToString());

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {


            foreach (GridViewRow currentRow in GridViewCNDN_3.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text) > 0)
                {
                    Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");
                    Label lbl_Total_Qty_ = (Label)currentRow.FindControl("lbl_Total_Qty");

                    all_sum += int.Parse((txt.Text).ToString());

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {


            foreach (GridViewRow currentRow in GridViewCNDN_4.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text) > 0)
                {
                    Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");
                    Label lbl_Total_Qty_ = (Label)currentRow.FindControl("lbl_Total_Qty");

                    all_sum += int.Parse((txt.Text).ToString());

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            foreach (GridViewRow currentRow in GridViewCNDN_5.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text) > 0)
                {
                    Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");
                    Label lbl_Total_Qty_ = (Label)currentRow.FindControl("lbl_Total_Qty");

                    all_sum += int.Parse((txt.Text).ToString());

                }
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            txtSAM_CN_DN_Quantity.Text = all_sum.ToString();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void chkCNDN_CheckedChanged(object sender, EventArgs e)
    {
        // hdnBilling_ID.Value = lnkBBilling_ID.Text;
        GetDetailsDataToForm(string.Empty, string.Empty);

        ddlSAM_CN_DN_Type_SelectedIndexChanged(null, null);
    }
    #endregion  

    #region GirdView Events
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewCNDN.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewCNDN.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewCNDN_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewCNDN.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewCNDN.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewCNDN.PageIndex)
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
            int currentPage = GridViewCNDN.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewCNDN.PageCount.ToString();

        }
    }

    protected void GridViewCNDN_1_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {


            List<dbo_CNDNDetailClass> listProduct_Quantity = (List<dbo_CNDNDetailClass>)Session["GetProduct_Quantity_tab_1"];
            Session.Remove("GetProduct_Quantity_tab_1");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewCNDN_1.Rows[i];

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
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("lbl_Agent_Price");


                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;

                        if (chkCNDN.Checked)
                        {
                            _Amount.Text = listProduct_Quantity[i].Qty.ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCNDN_2_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_CNDNDetailClass> listProduct_Quantity = (List<dbo_CNDNDetailClass>)Session["GetProduct_Quantity_tab_2"];
            Session.Remove("GetProduct_Quantity_tab_2");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewCNDN_2.Rows[i];

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
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("lbl_Agent_Price");


                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;

                        if (chkCNDN.Checked)
                        {
                            _Amount.Text = listProduct_Quantity[i].Qty.ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCNDN_3_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_CNDNDetailClass> listProduct_Quantity = (List<dbo_CNDNDetailClass>)Session["GetProduct_Quantity_tab_3"];
            Session.Remove("GetProduct_Quantity_tab_3");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewCNDN_3.Rows[i];

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
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("lbl_Agent_Price");


                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;

                        if (chkCNDN.Checked)
                        {
                            _Amount.Text = listProduct_Quantity[i].Qty.ToString();
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCNDN_4_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {


            List<dbo_CNDNDetailClass> listProduct_Quantity = (List<dbo_CNDNDetailClass>)Session["GetProduct_Quantity_tab_4"];
            Session.Remove("GetProduct_Quantity_tab_4");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewCNDN_4.Rows[i];

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
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("lbl_Agent_Price");


                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;

                        if (chkCNDN.Checked)
                        {
                            _Amount.Text = listProduct_Quantity[i].Qty.ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);

        }
    }

    protected void GridViewCNDN_5_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {


            List<dbo_CNDNDetailClass> listProduct_Quantity = (List<dbo_CNDNDetailClass>)Session["GetProduct_Quantity_tab_5"];
            Session.Remove("GetProduct_Quantity_tab_5");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewCNDN_5.Rows[i];

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
                    TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");

                    Label _lbl_Price = (Label)row.FindControl("lbl_Agent_Price");


                    if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                    {
                        _Amount.Enabled = false;
                    }
                    else
                    {
                        _Amount.Enabled = true;

                        if (chkCNDN.Checked)
                        {
                            _Amount.Text = listProduct_Quantity[i].Qty.ToString();
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCNDN_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.DataItem != null)
            {
                TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
                HiddenField hf_Qty_inv_ = e.Row.FindControl("hf_Qty_inv") as HiddenField;

                textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID +");");
                textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewCNDN_2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.DataItem != null)
            {
                TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
                HiddenField hf_Qty_inv_ = e.Row.FindControl("hf_Qty_inv") as HiddenField;

                textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");
                textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCNDN_3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.DataItem != null)
            {
                TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;

                HiddenField hf_Qty_inv_ = e.Row.FindControl("hf_Qty_inv") as HiddenField;

                textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");
                textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCNDN_4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.DataItem != null)
            {
                TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;

                HiddenField hf_Qty_inv_ = e.Row.FindControl("hf_Qty_inv") as HiddenField;

                textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");
                textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCNDN_5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.DataItem != null)
            {
                TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;

                HiddenField hf_Qty_inv_ = e.Row.FindControl("hf_Qty_inv") as HiddenField;

                textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");
                textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + hf_Qty_inv_.ClientID + ");");

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCNDN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        logger.Debug("e.CommandName " + e.CommandName);

        if (e.CommandName == "AddNewCNDN")
        {
            try
            {
                int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
                Label lnkBBilling_ID = (Label)GridViewCNDN.Rows[RowIndex].FindControl("lbl_PO_Number");
                LinkButton lbl_SAM_CN_DN_No = (LinkButton)GridViewCNDN.Rows[RowIndex].FindControl("lbl_SAM_CN_DN_No");

                if (!string.IsNullOrEmpty(lbl_SAM_CN_DN_No.Text))
                {
                    Show("คุณได้ทำการสร้างใบเพิ่มหนี้/ใบลดหนี้แล้ว กรุณากลับไปแก้ไขใบเดิม");
                }
                else
                {
                    hdnBilling_ID.Value = lnkBBilling_ID.Text;

                    GetDetailsDataToForm(string.Empty, string.Empty);

                    ddlSAM_CN_DN_Type_SelectedIndexChanged(null, null);
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }
        else if (e.CommandName == "ConfirmCNDN")
        {
            int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
            Label lnkBBilling_ID = (Label)GridViewCNDN.Rows[RowIndex].FindControl("lbl_CNDN_ID");

            logger.Debug(lnkBBilling_ID.Text);
            Session["Invoice_No"] = lnkBBilling_ID.Text;
            Response.Redirect("~/Views/ReceivingList.aspx");

            if (!string.IsNullOrEmpty(lnkBBilling_ID.Text))
            {
                Response.Redirect("ReceivingList.aspx");
            }
        }
        else if (e.CommandName == "View")
        {
            //  LinkButton lnkView = (LinkButton)e.CommandSource;

            int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
            LinkButton lbl_SAM_CN_DN_No = (LinkButton)GridViewCNDN.Rows[RowIndex].FindControl("lbl_SAM_CN_DN_No");
            Label lnkBBilling_ID = (Label)GridViewCNDN.Rows[RowIndex].FindControl("lbl_PO_Number");

            if (!string.IsNullOrEmpty(lbl_SAM_CN_DN_No.Text))
            {
                hdnBilling_ID.Value = lnkBBilling_ID.Text;

                logger.Debug("lnkBBilling_ID.Text " + lnkBBilling_ID.Text);
                GetDetailsDataToForm(lbl_SAM_CN_DN_No.Text, "View");
            }          
        }
        else if (e.CommandName == "_Delete")
        {
            try
            {
                int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
                LinkButton lbl_SAM_CN_DN_No = (LinkButton)GridViewCNDN.Rows[RowIndex].FindControl("lbl_SAM_CN_DN_No");

                dbo_CNDNClass cndn = new dbo_CNDNClass();

                cndn = dbo_CNDNDataClass.Select_Record(lbl_SAM_CN_DN_No.Text);
                if (cndn != null)
                {
                    if (cndn.SAM_CN_DN_Status == "ยังไม่คอนเฟิร์ม")
                    {
                        dbo_CNDNDataClass.Delete(lbl_SAM_CN_DN_No.Text);

                        List<dbo_CNDNDetailClass> detail = dbo_CNDNDetailDataClass.Search(lbl_SAM_CN_DN_No.Text);


                        foreach (dbo_CNDNDetailClass _detail in detail)
                        {
                            dbo_CNDNDetailDataClass.Delete(_detail.CNDN_Detail_ID);
                        }

                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("ลบข้อมูลสำเร็จ");

                        btnSearchSubmit_Click(null, null);
                        pnlGrid.Visible = true;
                        pnlForm.Visible = false;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("สถานะใบเพิ่มหนี้/ลดหนี้ถูกคอนเฟิร์มแล้ว ไม่สามรถลบใบเพิ่มหนี้/ลดหนี้ได้");
                    }
                }
                else
                {
                    Show("ไม่สามรถลบใบเพิ่มหนี้/ลดหนี้ได้ เนื่องจากไม่พบข้อมูล SAM CN/DN");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
        }

    }
    #endregion

    #region Methods
    private List<dbo_CNDNDetailClass> SetCNDNDetail(string Billing_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_CNDNDetailClass> listofreceive = new List<dbo_CNDNDetailClass>();
        int index = 1;

        try
        {
            foreach (GridViewRow currentRow in GridViewCNDN_1.Rows)
            {

                dbo_CNDNDetailClass detail = new dbo_CNDNDetailClass();


                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");


                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text.Trim()) > 0)
                {
                    Label lbl_Product_ID_ = (Label)currentRow.FindControl("lbl_Product_ID");
                    Label lbl_Vat_ = (Label)currentRow.FindControl("lbl_Vat");
                    Label lbl_Agent_Price_ = (Label)currentRow.FindControl("lbl_Agent_Price");

                    detail.CNDN_Detail_ID = Billing_ID + index.ToString("00");
                    detail.SAM_CN_DN_No = Billing_ID;
                    detail.Quantity = short.Parse(txt.Text);
                    detail.Product_ID = lbl_Product_ID_.Text;
                    detail.Vat = Byte.Parse(lbl_Vat_.Text);
                    detail.Price = Decimal.Parse(lbl_Agent_Price_.Text);
                    detail.Sub_Total = detail.Quantity * detail.Price;


                    index++;
                    listofreceive.Add(detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {


            foreach (GridViewRow currentRow in GridViewCNDN_2.Rows)
            {
                dbo_CNDNDetailClass detail = new dbo_CNDNDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");


                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text.Trim()) > 0)
                {
                    Label lbl_Product_ID_ = (Label)currentRow.FindControl("lbl_Product_ID");
                    Label lbl_Vat_ = (Label)currentRow.FindControl("lbl_Vat");
                    Label lbl_Agent_Price_ = (Label)currentRow.FindControl("lbl_Agent_Price");

                    detail.CNDN_Detail_ID = Billing_ID + index.ToString("00");
                    detail.SAM_CN_DN_No = Billing_ID;
                    detail.Quantity = short.Parse(txt.Text);
                    detail.Product_ID = lbl_Product_ID_.Text;
                    detail.Vat = Byte.Parse(lbl_Vat_.Text);
                    detail.Price = Decimal.Parse(lbl_Agent_Price_.Text);
                    detail.Sub_Total = detail.Quantity * detail.Price;

                    index++;
                    listofreceive.Add(detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            foreach (GridViewRow currentRow in GridViewCNDN_3.Rows)
            {
                dbo_CNDNDetailClass detail = new dbo_CNDNDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");


                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text.Trim()) > 0)
                {
                    Label lbl_Product_ID_ = (Label)currentRow.FindControl("lbl_Product_ID");
                    Label lbl_Vat_ = (Label)currentRow.FindControl("lbl_Vat");
                    Label lbl_Agent_Price_ = (Label)currentRow.FindControl("lbl_Agent_Price");

                    detail.CNDN_Detail_ID = Billing_ID + index.ToString("00");
                    detail.SAM_CN_DN_No = Billing_ID;
                    detail.Quantity = short.Parse(txt.Text);
                    detail.Product_ID = lbl_Product_ID_.Text;
                    detail.Vat = Byte.Parse(lbl_Vat_.Text);
                    detail.Price = Decimal.Parse(lbl_Agent_Price_.Text);
                    detail.Sub_Total = detail.Quantity * detail.Price;

                    index++;
                    listofreceive.Add(detail);

                    
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            foreach (GridViewRow currentRow in GridViewCNDN_4.Rows)
            {
                dbo_CNDNDetailClass detail = new dbo_CNDNDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");


                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text.Trim()) > 0)
                {
                    Label lbl_Product_ID_ = (Label)currentRow.FindControl("lbl_Product_ID");
                    Label lbl_Vat_ = (Label)currentRow.FindControl("lbl_Vat");
                    Label lbl_Agent_Price_ = (Label)currentRow.FindControl("lbl_Agent_Price");

                    detail.CNDN_Detail_ID = Billing_ID + index.ToString("00");
                    detail.SAM_CN_DN_No = Billing_ID;
                    detail.Quantity = short.Parse(txt.Text);
                    detail.Product_ID = lbl_Product_ID_.Text;
                    detail.Vat = Byte.Parse(lbl_Vat_.Text);
                    detail.Price = Decimal.Parse(lbl_Agent_Price_.Text);
                    detail.Sub_Total = detail.Quantity * detail.Price;

                    index++;
                    listofreceive.Add(detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            foreach (GridViewRow currentRow in GridViewCNDN_5.Rows)
            {
                dbo_CNDNDetailClass detail = new dbo_CNDNDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");


                if (!string.IsNullOrEmpty(txt.Text) && int.Parse(txt.Text.Trim()) > 0)
                {
                    Label lbl_Product_ID_ = (Label)currentRow.FindControl("lbl_Product_ID");
                    Label lbl_Vat_ = (Label)currentRow.FindControl("lbl_Vat");
                    Label lbl_Agent_Price_ = (Label)currentRow.FindControl("lbl_Agent_Price");

                    detail.CNDN_Detail_ID = Billing_ID + index.ToString("00");
                    detail.SAM_CN_DN_No = Billing_ID;
                    detail.Quantity = short.Parse(txt.Text);
                    detail.Product_ID = lbl_Product_ID_.Text;
                    detail.Vat = Byte.Parse(lbl_Vat_.Text);
                    detail.Price = Decimal.Parse(lbl_Agent_Price_.Text);
                    detail.Sub_Total = detail.Quantity * detail.Price;

                    index++;
                    listofreceive.Add(detail);
                }

            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        return listofreceive;
    }

    private void GetDetailsDataToForm(string id, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlForm.Visible = true;
        pnlGrid.Visible = false;

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);


        try
        {

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



            ddlSAM_CN_DN_Type.Enabled = enable;
            chkCNDN.Enabled = enable;

            logger.Debug("id " + id);

            if (!string.IsNullOrEmpty(id))
            {
                dbo_CNDNClass cndn = dbo_CNDNDataClass.Select_Record(id);
                txtSAM_CN_DN_No.Text = cndn.SAM_CN_DN_No;
                txtInvoice_No.Text = cndn.Invoice_No;
                txtInvoice_Date.Text = cndn.Invoice_Date.Value.ToShortDateString();
                ddlSAM_CN_DN_Type.ClearSelection();
                ddlSAM_CN_DN_Type.Items.FindByText(cndn.SAM_CN_DN_Type).Selected = true;
                txtSAM_CN_DN_Date.Text = cndn.SAM_CN_DN_Date.Value.ToShortDateString();
                txtSAM_CN_DN_Quantity.Text = cndn.SAM_CN_DN_Quantity.Value.ToString();
                txtSAM_CN_DN_Status.Text = cndn.SAM_CN_DN_Status;
                ddlSAM_CN_DN_Type.Enabled = false;

                if (Mode == "View")
                {
                    //
                    show_grid(cndn.SAM_CN_DN_No, cndn.SAM_CN_DN_Date, hdnBilling_ID.Value);

                }
                else
                {
                    show_grid(cndn.SAM_CN_DN_No, cndn.SAM_CN_DN_Date, hdnBilling_ID.Value);
                    //show_grid(cndn.SAM_CN_DN_No, DateTime.Now);
                }

            }
            else
            {

                dbo_BillingClass billing = dbo_BillingDataClass.Select_Record(hdnBilling_ID.Value);

                txtInvoice_No.Text = billing.Invoice_No;
                txtInvoice_Date.Text = billing.Invoice_Date.Value.ToShortDateString();
                ddlSAM_CN_DN_Type.ClearSelection();
                txtSAM_CN_DN_No.Text = string.Empty;
                txtSAM_CN_DN_Date.Text = string.Empty;
                txtSAM_CN_DN_Quantity.Text = string.Empty;
                txtSAM_CN_DN_Status.Text = string.Empty;
                //chkCNDN.Checked = false;
                show_grid(string.Empty, DateTime.Now, hdnBilling_ID.Value);
            }
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        //}
    }

    private void show_grid(string SAM_CN_DN_No, DateTime? pricedate, string Billing_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);


            List<dbo_CNDNDetailClass> item1 = dbo_CNDNDataClass.GetCNDNByProductGroupID("นมสดพาสเจอร์ไรส์", user_class.CV_CODE, SAM_CN_DN_No, pricedate, Billing_ID);
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewCNDN_1.DataSource = item1;
            GridViewCNDN_1.DataBind();

            List<dbo_CNDNDetailClass> item2 = dbo_CNDNDataClass.GetCNDNByProductGroupID("นมเปรี้ยว", user_class.CV_CODE, SAM_CN_DN_No, pricedate, Billing_ID);
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewCNDN_2.DataSource = item2;
            GridViewCNDN_2.DataBind();

            List<dbo_CNDNDetailClass> item3 = dbo_CNDNDataClass.GetCNDNByProductGroupID("โยเกิร์ตเมจิ", user_class.CV_CODE, SAM_CN_DN_No, pricedate, Billing_ID);
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewCNDN_3.DataSource = item3;
            GridViewCNDN_3.DataBind();

            List<dbo_CNDNDetailClass> item4 = dbo_CNDNDataClass.GetCNDNByProductGroupID("นมเปรี้ยวไพเกน", user_class.CV_CODE, SAM_CN_DN_No, pricedate, Billing_ID);
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewCNDN_4.DataSource = item4;
            GridViewCNDN_4.DataBind();

            List<dbo_CNDNDetailClass> item5 = dbo_CNDNDataClass.GetCNDNByProductGroupID("อื่นๆ", user_class.CV_CODE, SAM_CN_DN_No, pricedate, Billing_ID);
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewCNDN_5.DataSource = item5;
            GridViewCNDN_5.DataBind();


            TxtId_TextChanged(null, null);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion    
}