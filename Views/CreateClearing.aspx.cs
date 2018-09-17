#region Using
using log4net;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_CreateClearing : System.Web.UI.Page
{
    #region Private Variables
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    bool flag;
    #endregion

    #region Control Events

    #region General
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            try
            {

                if (Session["Create_Credit"] != null)
                {
                    Session.Remove("Create_Credit");
                }
                if (Session["Create_PaymentCredit"] != null)
                {
                    Session.Remove("Create_PaymentCredit");
                }
                if (Session["Create_Deduct"] != null)
                {
                    Session.Remove("Create_Deduct");
                }
                if (Session["Create_Subsid"] != null)
                {
                    Session.Remove("Create_Subsid");
                }

                txtDiscount.Attributes.Add("onchange", "myApp.showPleaseWait();");
                txtActualPayment.Attributes.Add("onchange", "myApp.showPleaseWait();");

                txt_ClearingDate_From.Text = DateTime.Now.ToShortDateString();
                txt_ClearingDate_To.Text = DateTime.Now.ToShortDateString();
                txt_RequisitionDate_From.Text = DateTime.Now.ToShortDateString();
                txt_RequisitionDate_TO.Text = DateTime.Now.ToShortDateString();


                DateTime? Requisition_Begin_Date = (string.IsNullOrEmpty(txt_RequisitionDate_From.Text) ? (DateTime?)null : DateTime.Parse(txt_RequisitionDate_From.Text));
                DateTime? Requisition_Begin_End = (string.IsNullOrEmpty(txt_RequisitionDate_TO.Text) ? (DateTime?)null : DateTime.Parse(txt_RequisitionDate_TO.Text));
                DateTime? Clearing_Begin_Date = (string.IsNullOrEmpty(txt_ClearingDate_From.Text) ? (DateTime?)null : DateTime.Parse(txt_ClearingDate_From.Text));
                DateTime? Clearing_Begin_End = (string.IsNullOrEmpty(txt_ClearingDate_To.Text) ? (DateTime?)null : DateTime.Parse(txt_ClearingDate_To.Text));


                // ButtonConfirmClearing.Attributes.Add("OnClientClick", "myApp.showPleaseWait();");

                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                if (user_class.User_Group_ID == "Agent")
                {
                    dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);
                    ViewState["CV_Code"] = clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code;

                    //List<dbo_RequisitionClearingClass> item = dbo_RequisitionClearingDataClass.Search(string.Empty, string.Empty, string.Empty, null, null, null, null, user_class.CV_CODE);
                    List<dbo_RequisitionClearingClass> item = dbo_RequisitionClearingDataClass.Search(string.Empty, string.Empty, string.Empty, Requisition_Begin_Date, Requisition_Begin_End, Clearing_Begin_Date, Clearing_Begin_End, user_class.CV_CODE);
                    if (item.Count > 0)
                    {
                        GridViewClearing.DataSource = item;
                        GridViewClearing.DataBind();

                        pnlNoRec.Visible = false;
                        GridViewClearing.Visible = true;

                    }
                    else
                    {
                        pnlNoRec.Visible = true;
                        GridViewClearing.Visible = false;
                    }
                    hdfResign.Value = "0";

                    Dictionary<string, DateTime?> user_id_clearing_date = new Dictionary<string, DateTime?>();
                    foreach (GridViewRow row in GridViewClearing.Rows)
                    {
                        Label lblUser_ID = (Label)row.FindControl("lblUser_ID");

                        if (!user_id_clearing_date.ContainsKey(lblUser_ID.Text))
                        {
                            user_id_clearing_date.Add(lblUser_ID.Text,
                                item.Where(f => f.User_ID == lblUser_ID.Text && f.Status != "ยังไม่เคลียร์เงิน").Max(f => f.Requisition_Date) == null ? (DateTime?)null :
                                item.Where(f => f.User_ID == lblUser_ID.Text && f.Status != "ยังไม่เคลียร์เงิน").Max(f => f.Requisition_Date).Value
                                );
                        }
                        else
                        {

                        }
                    }

                    foreach (GridViewRow row in GridViewClearing.Rows)
                    {
                        LinkButton Label_Status = (LinkButton)row.FindControl("lnkBClearing_No");
                        CheckBox chk = (CheckBox)row.FindControl("chkClearing");
                       // LinkButton _lnkB = (LinkButton)row.FindControl("lnkB");

  

                        if (Label_Status.Text == string.Empty)
                        {
                            chk.Enabled = true;
                        }
                        else
                        {
                            chk.Enabled = false;
                        }

                        Label lblUser_ID = (Label)row.FindControl("lblUser_ID");
                        Label lblClearing_Date = (Label)row.FindControl("Label_Requisition_Date");
                        LinkButton lnkBPrintClearing = (LinkButton)row.FindControl("lnkBPrintClearing");
                        LinkButton lnkBCreditPayment = (LinkButton)row.FindControl("lnkBCreditPayment");
                        Label Label_Status_ = (Label)row.FindControl("Label_Status");
                        HiddenField hdfUser_ID_SP_ = (HiddenField)row.FindControl("hdfUser_ID_SP");
                        LinkButton lnkB_Delete = (LinkButton)row.FindControl("lnkB");

                        if (user_id_clearing_date[lblUser_ID.Text].HasValue)
                        {
                            if (user_id_clearing_date[lblUser_ID.Text].Value.Date == DateTime.Parse(lblClearing_Date.Text).Date)
                            {
                                if (Label_Status_.Text == "เคลียร์เงินยังไม่ครบ")
                                {
                                    lnkBPrintClearing.Visible = true;
                                    //lnkBCreditPayment.Visible = true;
                                }
                                else
                                {
                                    lnkB_Delete.Visible = false;
                                }
                                List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(string.Empty, string.Empty, null, string.Empty, hdfUser_ID_SP_.Value).Where(f => f.Status == "ค้างชำระ").ToList();
                                //listofitem.Where(f => f.Status == "ค้างชำระ");
                                if (listofitem.Count > 0)
                                {
                                    lnkBCreditPayment.Visible = true;
                                }

                            }
                        }

                    }

                    /*
                    foreach (GridViewRow row in GridViewClearing.Rows)
                    {
                        LinkButton Label_Status = (LinkButton)row.FindControl("lnkBClearing_No");
                        CheckBox chk = (CheckBox)row.FindControl("chkClearing");

                        if (Label_Status.Text == string.Empty)
                        {
                            chk.Enabled = true;
                        }
                        else
                        {
                            chk.Enabled = false;
                        }
                    }
                    */
                    //List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                    //  , string.Empty, string.Empty, string.Empty, clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code, null, string.Empty, string.Empty).Where(f => f.Status == "Active" && (f.Position == "ซุปเปอร์ไวซ์" || f.Position == "สาวส่งนม")).ToList();

                    List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
          , string.Empty, string.Empty, string.Empty, clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code, null, string.Empty, string.Empty).Where(f => f.Status == "Active" && (f.Position == "ซุปเปอร์ไวซ์เซอร์" || f.Position == "สาวส่งนม")).ToList();


                    ddl_SPName.DataSource = users;
                    ddl_SPName.DataBind();
                    ddl_SPName.Items.Insert(0, new ListItem("==ระบุ==", "0"));
                    ddl_SPName.SelectedIndex = 0;

                    pnlStep.Visible = false;
                    pnlResign.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);
                }
               
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }

    protected void TxtId_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            getAllRow();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void txt_Deposit_Qty_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {


            decimal all_sum_TotalAmount = 0;
            int all_sum_Total = 0;

            foreach (GridViewRow currentRow in GridViewClearing_1.Rows)
            {

                Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");

                if (!string.IsNullOrEmpty(lbl_Net_Sales_Qty_.Text) && lbl_Net_Sales_Qty_.Text.Trim() != "0")
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");

                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");

                    Label_TotalSales_Qty_.Text = string.IsNullOrEmpty(Label_TotalSales_Qty_.Text) ? "0" : Label_TotalSales_Qty_.Text;
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");

                    if (txt.Text == "")
                    {
                        txt.Text = "0";
                    }

                    if (txt_Deposit_QtyReturn_.Text == "")
                    {
                        txt_Deposit_QtyReturn_.Text = "0";
                    }

                    if (((int.Parse(txt.Text) + (int.Parse(txt_Deposit_QtyReturn_.Text))) > (int.Parse(lbl_Net_Sales_Qty_.Text))))
                    {
                        Show("จำนวนฝาก + จำนวนคืน ต้องไม่เกินจำนวนเบิก");
                        TextBox txtClear = (TextBox)sender;
                        txtClear.Text = "0";
                        txtClear.Focus();
                        //return;
                    }

                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) - int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) - int.Parse(string.IsNullOrEmpty(txt_Deposit_QtyReturn_.Text) ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();
                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    all_sum_Total += (int.Parse(Label_Sales_Qty_.Text));
                    all_sum_TotalAmount += decimal.Parse(Label_TotalSales_Qty_.Text);
                }

            }

            foreach (GridViewRow currentRow in GridViewClearing_2.Rows)
            {

                Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");

                if (!string.IsNullOrEmpty(lbl_Net_Sales_Qty_.Text) && lbl_Net_Sales_Qty_.Text.Trim() != "0")
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");

                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");

                    Label_TotalSales_Qty_.Text = string.IsNullOrEmpty(Label_TotalSales_Qty_.Text) ? "0" : Label_TotalSales_Qty_.Text;
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");

                    if (txt.Text == "")
                    {
                        txt.Text = "0";
                    }

                    if (txt_Deposit_QtyReturn_.Text == "")
                    {
                        txt_Deposit_QtyReturn_.Text = "0";
                    }

                    if (((int.Parse(txt.Text) + (int.Parse(txt_Deposit_QtyReturn_.Text))) > (int.Parse(lbl_Net_Sales_Qty_.Text))))
                    {
                        Show("จำนวนฝาก + จำนวนคืน ต้องไม่เกินจำนวนเบิก");
                        TextBox txtClear = (TextBox)sender;
                        txtClear.Text = "0";
                        txtClear.Focus();
                        //return;
                    }

                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) - int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) - int.Parse(string.IsNullOrEmpty(txt_Deposit_QtyReturn_.Text) ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();
                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    all_sum_Total += (int.Parse(Label_Sales_Qty_.Text));
                    all_sum_TotalAmount += decimal.Parse(Label_TotalSales_Qty_.Text);
                }

            }

            foreach (GridViewRow currentRow in GridViewClearing_3.Rows)
            {

                Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");

                if (!string.IsNullOrEmpty(lbl_Net_Sales_Qty_.Text) && lbl_Net_Sales_Qty_.Text.Trim() != "0")
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");

                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");

                    Label_TotalSales_Qty_.Text = string.IsNullOrEmpty(Label_TotalSales_Qty_.Text) ? "0" : Label_TotalSales_Qty_.Text;
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");

                    if (txt.Text == "")
                    {
                        txt.Text = "0";
                    }

                    if (txt_Deposit_QtyReturn_.Text == "")
                    {
                        txt_Deposit_QtyReturn_.Text = "0";
                    }

                    if (((int.Parse(txt.Text) + (int.Parse(txt_Deposit_QtyReturn_.Text))) > (int.Parse(lbl_Net_Sales_Qty_.Text))))
                    {
                        Show("จำนวนฝาก + จำนวนคืน ต้องไม่เกินจำนวนเบิก");
                        TextBox txtClear = (TextBox)sender;
                        txtClear.Text = "0";
                        txtClear.Focus();
                        //return;
                    }

                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) - int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) - int.Parse(string.IsNullOrEmpty(txt_Deposit_QtyReturn_.Text) ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();
                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    all_sum_Total += (int.Parse(Label_Sales_Qty_.Text));
                    all_sum_TotalAmount += decimal.Parse(Label_TotalSales_Qty_.Text);
                }

            }

            foreach (GridViewRow currentRow in GridViewClearing_4.Rows)
            {

                Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");

                if (!string.IsNullOrEmpty(lbl_Net_Sales_Qty_.Text) && lbl_Net_Sales_Qty_.Text.Trim() != "0")
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");

                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");

                    Label_TotalSales_Qty_.Text = string.IsNullOrEmpty(Label_TotalSales_Qty_.Text) ? "0" : Label_TotalSales_Qty_.Text;
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");

                    if (txt.Text == "")
                    {
                        txt.Text = "0";
                    }

                    if (txt_Deposit_QtyReturn_.Text == "")
                    {
                        txt_Deposit_QtyReturn_.Text = "0";
                    }

                    if (((int.Parse(txt.Text) + (int.Parse(txt_Deposit_QtyReturn_.Text))) > (int.Parse(lbl_Net_Sales_Qty_.Text))))
                    {
                        Show("จำนวนฝาก + จำนวนคืน ต้องไม่เกินจำนวนเบิก");
                        TextBox txtClear = (TextBox)sender;
                        txtClear.Text = "0";
                        txtClear.Focus();
                        //return;
                    }

                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) - int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) - int.Parse(string.IsNullOrEmpty(txt_Deposit_QtyReturn_.Text) ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();
                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    all_sum_Total += (int.Parse(Label_Sales_Qty_.Text));
                    all_sum_TotalAmount += decimal.Parse(Label_TotalSales_Qty_.Text);
                }

            }

            foreach (GridViewRow currentRow in GridViewClearing_5.Rows)
            {

                Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");

                if (!string.IsNullOrEmpty(lbl_Net_Sales_Qty_.Text) && lbl_Net_Sales_Qty_.Text.Trim() != "0")
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");

                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");

                    Label_TotalSales_Qty_.Text = string.IsNullOrEmpty(Label_TotalSales_Qty_.Text) ? "0" : Label_TotalSales_Qty_.Text;
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");

                    if (txt.Text == "")
                    {
                        txt.Text = "0";
                    }

                    if (txt_Deposit_QtyReturn_.Text == "")
                    {
                        txt_Deposit_QtyReturn_.Text = "0";
                    }

                    if (((int.Parse(txt.Text) + (int.Parse(txt_Deposit_QtyReturn_.Text))) > (int.Parse(lbl_Net_Sales_Qty_.Text))))
                    {
                        Show("จำนวนฝาก + จำนวนคืน ต้องไม่เกินจำนวนเบิก");
                        TextBox txtClear = (TextBox)sender;
                        txtClear.Text = "0";
                        txtClear.Focus();
                        //return;
                    }

                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) - int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) - int.Parse(string.IsNullOrEmpty(txt_Deposit_QtyReturn_.Text) ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();
                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    all_sum_Total += (int.Parse(Label_Sales_Qty_.Text));
                    all_sum_TotalAmount += decimal.Parse(Label_TotalSales_Qty_.Text);
                }

            }

            TextboxTotalAmount.Text = all_sum_Total.ToString("#,##0");
            TextboxTotal.Text = all_sum_TotalAmount.ToString("#,##0.#0");


            logger.Debug("all_sum_Total " + all_sum_Total.ToString(""));
            logger.Debug("all_sum_TotalAmount " + all_sum_TotalAmount.ToString());

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {

        if (hdfResign.Value == "1")
        {
            GetPaymentDetailResign();
            if (Convert.ToDecimal(txtDiscount.Text) > Convert.ToDecimal(txtTotal.Text))
            {
                Show("ไม่สามารถระบุส่วนลดมากกว่ายอดเงินควรนำส่ง " + txtTotal.Text + " ได้!");
                txtNetTotal.Text = txtTotal.Text;
                txtDiscount.Text = "0";
                txtActualPayment.Text = "0";
            }
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        else
        {
            GetPaymentDetail();
            if(Convert.ToDecimal(txtDiscount.Text) > Convert.ToDecimal(txtTotal.Text))
            {
                Show("ไม่สามารถระบุส่วนลดมากกว่ายอดเงินควรนำส่ง "+ txtTotal.Text +" ได้!");
                txtNetTotal.Text = txtTotal.Text;
                txtDiscount.Text = "0";
                txtActualPayment.Text = "0";
            }
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }

        //ButtonDeduct_Click(null, null);
    }

    protected void txtActualPayment_TextChanged(object sender, EventArgs e)
    {
        if (hdfResign.Value == "1")
        {
            GetPaymentDetailResign_01();
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        else
        {
            //GetPaymentDetail();
            GetPaymentDetail_01();
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }

        //ButtonDeduct_Click(null, null);
    }

    protected void txtFooterCredit_Date_TextChanged(object sender, EventArgs e)
    {
        try
        {


            TextBox txtFooterCredit_Date = (TextBox)GridViewCredit.FooterRow.FindControl("txtFooterCredit_Date");


            DateTime temp_date = DateTime.Parse(txtFooterCredit_Date.Text);


            if ((DateTime.Now.Date - temp_date.Date).TotalDays > 1)
            {
                Show("ระบุวันที่ย้อนหลังได้ 1 วัน");
                //txtFooterCredit_Date.Text = string.Empty;
                txtFooterCredit_Date.Text = DateTime.Now.ToShortDateString();
            }
            else if (temp_date > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                Show("ไม่สามารถระบุวันที่ค้างชำระเกินวันปัจจุบันได้");
                txtFooterCredit_Date.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void ButtonAddNew_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        hdfPriceExpire.Value = string.Empty;
        bool isUserconflict = false;

        bool first_found = false;

        try
        {
            hdfRequisition_No.Value = string.Empty;

            foreach (GridViewRow currentRow in GridViewClearing.Rows)
            {
                CheckBox chk = (CheckBox)currentRow.FindControl("chkClearing");


                if (chk.Checked)
                {
                    if (!first_found)
                    {
                        Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");
                        Label lnkB_Requisition_No = (Label)currentRow.FindControl("lnkB_Requisition_No");
                        hdfUser_ID.Value = lblUser_ID.Text;
                        hdfRequisition_No.Value = lnkB_Requisition_No.Text;
                        first_found = true;
                    }
                    else
                    {
                        Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");

                        if (hdfUser_ID.Value != lblUser_ID.Text)
                        {
                            isUserconflict = true;
                        }
                        else
                        {
                            Label lnkB_Requisition_No = (Label)currentRow.FindControl("lnkB_Requisition_No");
                            hdfRequisition_No.Value = hdfRequisition_No.Value + lnkB_Requisition_No.Text;
                        }
                    }
                }

            }

            if (isUserconflict)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(1000);
                Show("ต้องระบุ SP ให้ตรงกัน");
            }
            else if (!first_found)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(1000);
                Show("ต้องระบุเลขที่ใบเบิก");
            }
            else
            {
                ButtonSave.Visible = true;
                ButtonSaveAndNext.Visible = true;
                ButtonSaveAndNext.Text = "บันทึกและหน้าถัดไป";
                ButtonSave.Text = "บันทึก";

                hdfClearing_Status.Value = "1";

                GetDetailsDataToForm(string.Empty, string.Empty);



            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        DateTime? Requisition_Begin_Date = (string.IsNullOrEmpty(txt_RequisitionDate_From.Text) ? (DateTime?)null : DateTime.Parse(txt_RequisitionDate_From.Text));
        DateTime? Requisition_Begin_End = (string.IsNullOrEmpty(txt_RequisitionDate_TO.Text) ? (DateTime?)null : DateTime.Parse(txt_RequisitionDate_TO.Text));
        DateTime? Clearing_Begin_Date = (string.IsNullOrEmpty(txt_ClearingDate_From.Text) ? (DateTime?)null : DateTime.Parse(txt_ClearingDate_From.Text));
        DateTime? Clearing_Begin_End = (string.IsNullOrEmpty(txt_ClearingDate_To.Text) ? (DateTime?)null : DateTime.Parse(txt_ClearingDate_To.Text));

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

        try
        {
            List<dbo_RequisitionClearingClass> item = dbo_RequisitionClearingDataClass.Search(
                txtSearchClearing_No.Text, txtSearchRequisition_No.Text, ddl_SPName.SelectedValue,
                Requisition_Begin_Date, Requisition_Begin_End, Clearing_Begin_Date, Clearing_Begin_End, user_class.CV_CODE);

            if (item.Count > 0)
            {
                GridViewClearing.DataSource = item;
                GridViewClearing.DataBind();

                pnlNoRec.Visible = false;
                GridViewClearing.Visible = true;
            }
            else
            {
                pnlNoRec.Visible = true;
                GridViewClearing.Visible = false;
            }


            Dictionary<string, DateTime?> user_id_clearing_date = new Dictionary<string, DateTime?>();
            foreach (GridViewRow row in GridViewClearing.Rows)
            {
                Label lblUser_ID = (Label)row.FindControl("lblUser_ID");

                if (!user_id_clearing_date.ContainsKey(lblUser_ID.Text))
                {
                    user_id_clearing_date.Add(lblUser_ID.Text,
                        item.Where(f => f.User_ID == lblUser_ID.Text && f.Status != "ยังไม่เคลียร์เงิน").Max(f => f.Requisition_Date) == null ? (DateTime?)null :
                        item.Where(f => f.User_ID == lblUser_ID.Text && f.Status != "ยังไม่เคลียร์เงิน").Max(f => f.Requisition_Date).Value
                        );
                }
                else
                {

                }
            }

            foreach (GridViewRow row in GridViewClearing.Rows)
            {
                LinkButton Label_Status = (LinkButton)row.FindControl("lnkBClearing_No");
                CheckBox chk = (CheckBox)row.FindControl("chkClearing");

                if (Label_Status.Text == string.Empty)
                {
                    chk.Enabled = true;
                }
                else
                {
                    chk.Enabled = false;
                }

                Label lblUser_ID = (Label)row.FindControl("lblUser_ID");
                Label lblClearing_Date = (Label)row.FindControl("Label_Requisition_Date");
                LinkButton lnkBPrintClearing = (LinkButton)row.FindControl("lnkBPrintClearing");
                LinkButton lnkBCreditPayment = (LinkButton)row.FindControl("lnkBCreditPayment");
                Label Label_Status_ = (Label)row.FindControl("Label_Status");
                HiddenField hdfUser_ID_SP_ = (HiddenField)row.FindControl("hdfUser_ID_SP");
                LinkButton lnkB_Delete = (LinkButton)row.FindControl("lnkB");





                if (user_id_clearing_date[lblUser_ID.Text].HasValue)
                {
                    if (user_id_clearing_date[lblUser_ID.Text].Value.Date == DateTime.Parse(lblClearing_Date.Text).Date)
                    {
                        if (Label_Status_.Text == "เคลียร์เงินยังไม่ครบ")
                        {
                            lnkBPrintClearing.Visible = true;
                            //lnkBCreditPayment.Visible = true;
                        }

                        if (Label_Status_.Text == "เคลียร์เงินครบแล้ว")
                        {
                            lnkB_Delete.Visible = false;
                        }


                        List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(string.Empty, string.Empty, null, string.Empty, hdfUser_ID_SP_.Value).Where(f => f.Status == "ค้างชำระ").ToList();
                        listofitem.Where(f => f.Status == "ค้างชำระ");
                        if (listofitem.Count > 0)
                        {
                            lnkBCreditPayment.Visible = true;
                        }
                    }
                }

            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        ddl_SPName.ClearSelection();
        txtSearchRequisition_No.Text = string.Empty;
        txtSearchClearing_No.Text = string.Empty;

        txt_RequisitionDate_From.Text = DateTime.Now.ToShortDateString();
        txt_RequisitionDate_TO.Text = DateTime.Now.ToShortDateString();

        txt_ClearingDate_From.Text = DateTime.Now.ToShortDateString();
        txt_ClearingDate_To.Text = DateTime.Now.ToShortDateString();

        if (GridViewClearing.Rows.Count > 0)
        {
            List<dbo_RequisitionClearingClass> item = new List<dbo_RequisitionClearingClass>();
            GridViewClearing.DataSource = item;
            GridViewClearing.DataBind();
        }


        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void chkClearing_CheckedChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);


        int count = 0;

        foreach (GridViewRow currentRow in GridViewClearing.Rows)
        {
            CheckBox chk = (CheckBox)currentRow.FindControl("chkClearing");
            if (chk.Checked)
            {
                count++;
            }
        }

        if (count > 1)
        {
            Show("ไม่สามารถเลือกใบเบิกได้มากกว่า 1 ใบได้");


            foreach (GridViewRow currentRow in GridViewClearing.Rows)
            {
                CheckBox chk = (CheckBox)currentRow.FindControl("chkClearing");
                if (chk.Checked)
                {
                    chk.Checked = false;
                }
            }

        }

        // bool found_check = false;
        /*
        foreach (GridViewRow currentRow in GridViewClearing.Rows)
        {
            CheckBox chk = (CheckBox)currentRow.FindControl("chkClearing");
            //if (!found_check)
            //{
            if (chk.Checked)
            {
                Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");
                Label lnkB_Requisition_No = (Label)currentRow.FindControl("lnkB_Requisition_No");
                hdfUser_ID.Value = lblUser_ID.Text;
                hdfRequisition_No.Value = lnkB_Requisition_No.Text;
                // found_check = true;
            }

            
            //}
            //else
            //{
            //    Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");
            //    LinkButton lnkB_Requisition_No = (LinkButton)currentRow.FindControl("lnkB_Requisition_No");


            //    if (lblUser_ID.Text != hdfUser_ID.Value)
            //    {
            //        chk.Enabled = false;
            //    }


            //}
        }
        */

        //if (!found_check)
        //{
        //    List<dbo_RequisitionClearingClass> item = dbo_RequisitionClearingDataClass.Search(string.Empty, string.Empty);
        //    GridViewClearing.DataSource = item;
        //    GridViewClearing.DataBind();
        //}
    }

    protected void btnConfirmPayment_Click(object sender, EventArgs e)
    {
        try
        {
            List<dbo_CreditPaymentClass> creditpayment = dbo_CreditPaymentDataClass.Search("", txtClearing_No.Text)
                .Where(f => f.Last_Modified_Date.HasValue != true).ToList();


            foreach (dbo_CreditPaymentClass payment in creditpayment)
            {

                logger.Debug("payment.Payment_No " + payment.Payment_Amount);
                decimal? init_payment = payment.Payment_Amount;

                dbo_CreditClass credit = dbo_CreditDataClass.Select_Record(payment.Credit_ID);


                List<dbo_DebtClass> debt_list = dbo_DebtDataClass.Search(string.Empty, credit.Customer_ID);
                dbo_DebtClass last_debt_pay = null;
                foreach (dbo_DebtClass debt in debt_list.Where(f => f.Balance_Outstanding_Amount > 0))
                {
                    logger.Debug("debt_list debt: " + init_payment);

                    if (init_payment > 0)
                    {
                        decimal? cal_amount = init_payment - debt.Balance_Outstanding_Amount;

                        logger.Debug("debt: cal_amount " + cal_amount + " : initail_amount " + init_payment + " : debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                        if (cal_amount > 0)
                        {
                            debt.Total_Payment_Amount += (debt.Balance_Outstanding_Amount);
                            init_payment -= debt.Balance_Outstanding_Amount;
                            debt.Balance_Outstanding_Amount = 0;

                            logger.Debug("debt: debt.Total_Payment_Amount " + debt.Total_Payment_Amount);
                            logger.Debug("debt: debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                        }
                        else
                        {
                            debt.Total_Payment_Amount += init_payment;
                            debt.Balance_Outstanding_Amount -= init_payment;

                            logger.Debug("debt: debt.Total_Payment_Amount " + debt.Total_Payment_Amount);
                            logger.Debug("debt: debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                            init_payment = 0;
                        }

                        dbo_DebtDataClass.Update(debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        last_debt_pay = debt;

                    }

                }


                payment.Last_Modified_Date = DateTime.Now;
                switch (payment.Payment_Method)
                {
                    case "เงินสด":
                        payment.Payment_Method = "1";
                        break;
                    case "เช็ค":
                        payment.Payment_Method = "2";
                        break;
                    case "โอน":
                        payment.Payment_Method = "3";
                        break;
                }
                dbo_CreditPaymentDataClass.Update(payment);


            }

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            System.Threading.Thread.Sleep(1000);


            hdfUser_ID.Value = string.Empty;
            txtClearing_No.Text = string.Empty;

            btnConfirmPayment.Visible = false;
            btnConfirmPaymentBackToGrid.Visible = false;

            ShowStep0();

            List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
            GridViewCreditPayment.DataSource = listofitem;
            GridViewCreditPayment.DataBind();

            // pnlGrid.Visible = true ;

            // ShowStep3();

            btnBackToCredit.Visible = true;
            ButtonSubsidy.Visible = true;

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnConfirmPaymentBackToGrid_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            System.Threading.Thread.Sleep(1000);


            hdfUser_ID.Value = string.Empty;
            txtClearing_No.Text = string.Empty;

            btnConfirmPayment.Visible = false;
            btnConfirmPaymentBackToGrid.Visible = false;

            ShowStep0();

            List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
            GridViewCreditPayment.DataSource = listofitem;
            GridViewCreditPayment.DataBind();

            // pnlGrid.Visible = true ;

            // ShowStep3();

            btnBackToCredit.Visible = true;
            ButtonSubsidy.Visible = true;

            btnSearch_Click(null, null);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion

    #region Button Step 1
    private string findDepositNotZero()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        string message = string.Empty;
        try
        {
            List<DepositCheckPriceDiff> item = dbo_ClearingDataClass.GetDepositCheckPriceDiff(hdfUser_ID.Value, txtRequisition_No.Text);

            logger.Debug("item.Count " + item.Count);

            logger.Debug("GridViewClearing_1");
            foreach (GridViewRow currentRow in GridViewClearing_1.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");
                Label product_id = (Label)currentRow.FindControl("Label_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    logger.Debug("product_id.Text " + product_id.Text);
                    DepositCheckPriceDiff pricediff = item.FirstOrDefault(f => f.Product_ID == product_id.Text);
                    if (pricediff.Selling_Price != pricediff.CP_Meiji_Price_Today ||
                        pricediff.Selling_Price != pricediff.CP_Meiji_Price_Next_Day ||
                        pricediff.Price != pricediff.SP_Price_Today ||
                        pricediff.Price != pricediff.SP_Price_Next_Day)
                    {
                        message += " " + pricediff.product_name;
                    }
                }
            }

            logger.Debug("GridViewClearing_2");
            foreach (GridViewRow currentRow in GridViewClearing_2.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");
                Label product_id = (Label)currentRow.FindControl("Label_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    logger.Debug("product_id.Text " + product_id.Text);
                    DepositCheckPriceDiff pricediff = item.FirstOrDefault(f => f.Product_ID == product_id.Text);
                    if (pricediff.Selling_Price != pricediff.CP_Meiji_Price_Today || pricediff.Selling_Price != pricediff.CP_Meiji_Price_Next_Day ||
                        pricediff.Price != pricediff.SP_Price_Today || pricediff.Price != pricediff.SP_Price_Next_Day)
                    {
                        message += " " + pricediff.product_name;
                    }
                }
            }

            logger.Debug("GridViewClearing_3");
            foreach (GridViewRow currentRow in GridViewClearing_3.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");
                Label product_id = (Label)currentRow.FindControl("Label_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    logger.Debug("product_id.Text " + product_id.Text);
                    DepositCheckPriceDiff pricediff = item.FirstOrDefault(f => f.Product_ID == product_id.Text);
                    if (pricediff.Selling_Price != pricediff.CP_Meiji_Price_Today || pricediff.Selling_Price != pricediff.CP_Meiji_Price_Next_Day ||
                        pricediff.Price != pricediff.SP_Price_Today || pricediff.Price != pricediff.SP_Price_Next_Day)
                    {
                        message += " " + pricediff.product_name;
                    }
                }
            }

            logger.Debug("GridViewClearing_4");
            foreach (GridViewRow currentRow in GridViewClearing_4.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");
                Label product_id = (Label)currentRow.FindControl("Label_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    logger.Debug("product_id.Text " + product_id.Text);
                    DepositCheckPriceDiff pricediff = item.FirstOrDefault(f => f.Product_ID == product_id.Text);
                    if (pricediff.Selling_Price != pricediff.CP_Meiji_Price_Today || pricediff.Selling_Price != pricediff.CP_Meiji_Price_Next_Day ||
                        pricediff.Price != pricediff.SP_Price_Today || pricediff.Price != pricediff.SP_Price_Next_Day)
                    {
                        message += " " + pricediff.product_name;
                    }
                }
            }

            logger.Debug("GridViewClearing_5");
            foreach (GridViewRow currentRow in GridViewClearing_5.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");
                Label product_id = (Label)currentRow.FindControl("Label_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    logger.Debug("product_id.Text " + product_id.Text);
                    DepositCheckPriceDiff pricediff = item.FirstOrDefault(f => f.Product_ID == product_id.Text);
                    if (pricediff.Selling_Price != pricediff.CP_Meiji_Price_Today || pricediff.Selling_Price != pricediff.CP_Meiji_Price_Next_Day ||
                        pricediff.Price != pricediff.SP_Price_Today || pricediff.Price != pricediff.SP_Price_Next_Day)
                    {
                        message += " " + pricediff.product_name;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        return message;
    }

    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            if (ButtonSave.Text == "แก้ไข")
            {
                GetDetailsDataToForm(txtClearing_No.Text, "Edit");
            }
            else
            {

                string message = findDepositNotZero();

                if (!string.IsNullOrEmpty(message))
                {
                    Show("ราคาของสินค้า " + message + " จะมีการเปลี่ยนแปลง กรุณาคืนของเข้าสต๊อกเอเยนต์");
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

                    btnSearch_Click(null, null);

                    ShowStep0();
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    public void ButtonSaveAndNext_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            logger.Debug("ButtonSave.Text " + ButtonSave.Text);

            if (ButtonSave.Text == "แก้ไข")
            {

            }
            else
            {

                if (hdfResign.Value == "1")
                {
                    if (CountDeposit() > 0)
                    {
                        Show("กรุณาคืนสินค้าให้ครบ");
                    }
                    else
                    {
                        dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

                        txtDiscount.Text = "0";
                        txtActualPayment.Text = "0";

                        txtActualPayment.Enabled = true;
                        txtDiscount.Enabled = true;

                        ButtonSaveClearing.Text = "บันทึก";
                        ButtonConfirmClearing.Visible = true;
                        ButtonSaveClearing.Visible = false;

                        GetPaymentDetailResign();
                    }
                }
                else
                {

                    logger.Debug("btnSaveMode.Value " + btnSaveMode.Value);

                    string message = findDepositNotZero();

                    if (!string.IsNullOrEmpty(message))
                    {
                        Show("ราคาของสินค้า " + message + " จะมีการเปลี่ยนแปลง กรุณาคืนของเข้าสต๊อกเอเยนต์");
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
                        btnSearch_Click(null, null);

                        ShowStep2();
                    }


                }


            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    private int CountDeposit()
    {
        int count = 0;
        foreach (GridViewRow currentRow in GridViewClearing_1.Rows)
        {
            Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

            if (lbl_Unit_of_item.Text != string.Empty)
            {
                TextBox txt_Deposit_Qty = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt_Deposit_Qty.Text))
                {
                    count += int.Parse(txt_Deposit_Qty.Text);
                }
            }
        }

        foreach (GridViewRow currentRow in GridViewClearing_2.Rows)
        {
            Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

            if (lbl_Unit_of_item.Text != string.Empty)
            {
                TextBox txt_Deposit_Qty = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt_Deposit_Qty.Text))
                {
                    count += int.Parse(txt_Deposit_Qty.Text);
                }
            }
        }

        foreach (GridViewRow currentRow in GridViewClearing_3.Rows)
        {
            Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

            if (lbl_Unit_of_item.Text != string.Empty)
            {
                TextBox txt_Deposit_Qty = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt_Deposit_Qty.Text))
                {
                    count += int.Parse(txt_Deposit_Qty.Text);
                }
            }
        }

        foreach (GridViewRow currentRow in GridViewClearing_4.Rows)
        {
            Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

            if (lbl_Unit_of_item.Text != string.Empty)
            {
                TextBox txt_Deposit_Qty = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt_Deposit_Qty.Text))
                {
                    count += int.Parse(txt_Deposit_Qty.Text);
                }
            }
        }

        foreach (GridViewRow currentRow in GridViewClearing_5.Rows)
        {
            Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

            if (lbl_Unit_of_item.Text != string.Empty)
            {
                TextBox txt_Deposit_Qty = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt_Deposit_Qty.Text))
                {
                    count += int.Parse(txt_Deposit_Qty.Text);
                }
            }
        }
        return count;
    }

    protected void ButtonDipositCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        System.Threading.Thread.Sleep(1000);
        ShowStep0();

        hdfResign.Value = "0";
    }

    #endregion

    #region Button Step 2
    public void btnCreditPayment_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        //ShowStep3();

        try
        {
            #region test
            //if (Session["Create_Credit"] != null)
            //{
            //    // btnCreditPaymentNext.Attributes.Add("OnClientClick", "javascript:return Confirm(); return true;");
            //    //OnClientClick="myApp.showPleaseWait(); return true;
            //   // return confirm('ยืนยันการเคลียร์เงินใช่หรือไม่?') ? myApp.showPleaseWait() : false; "
            //   btnCreditPaymentNext.OnClientClick = "return Confirm(); myApp.showPleaseWait(); return true;";
            //   // btnCreditPaymentNext.OnClientClick = "return Confirm()?myApp.showPleaseWait(): false;";

            //    string confirmValue = Request.Form["confirm_value"];

            //    Show(confirmValue.ToString());

            //    if (confirmValue == "Yes")
            //    {
            //        if (Create_Credit())
            //        {
            //            ShowStep3();
            //            List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
            //            GridViewCreditPayment.DataSource = listofitem;
            //            GridViewCreditPayment.DataBind();
            //        }
            //        confirmValue="";
            //    }
            //    else
            //    {

            //        if (Session["Create_Credit"] != null)
            //        {

            //            Session.Remove("Create_Credit");
            //        }
            //        ShowStep3();
            //        List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
            //        GridViewCreditPayment.DataSource = listofitem;
            //        GridViewCreditPayment.DataBind();
            //        confirmValue = "";
            //    }


            //    //if (Create_Credit())
            //    //{

            //    //    ShowStep3();
            //    //    List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
            //    //    GridViewCreditPayment.DataSource = listofitem;
            //    //    GridViewCreditPayment.DataBind();
            //    //}


            //}
            //else
            //{
            //    btnCreditPaymentNext.OnClientClick = "myApp.showPleaseWait(); return true;";
            //    if (Session["Create_Credit"] != null)
            //    {

            //        Session.Remove("Create_Credit");
            //    }
            //    ShowStep3();
            //    List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
            //    GridViewCreditPayment.DataSource = listofitem;
            //    GridViewCreditPayment.DataBind();
            //}
            #endregion


            if (Session["Create_Credit"] != null)
            {

                if (Add_Credit())
                {
                    if (Session["Create_Credit"] != null)
                    {

                        Session.Remove("Create_Credit");
                    }
                    ShowStep3();
                    List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
                    GridViewCreditPayment.DataSource = listofitem;
                    GridViewCreditPayment.DataBind();

                }
            }
            else
            {
                if (Session["Create_Credit"] != null)
                {

                    Session.Remove("Create_Credit");
                }
                ShowStep3();
                List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
                GridViewCreditPayment.DataSource = listofitem;
                GridViewCreditPayment.DataBind();
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnCreditPaymentBack_Click(object sender, EventArgs e)
    {
        if (Session["Create_Credit"] != null)
        {

            Session.Remove("Create_Credit");
        }
        btnCreditPaymentNext.Text = "ถัดไป";
        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        ShowStep1();
    }

    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);



        try
        {

            dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
            string User_ID = hdfUser_ID.Value;
            string replace_sale = requ.Replace_Sales;

            List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value);
            GridViewCredit.ShowFooter = true;

            //ขายแทน
            if (!string.IsNullOrEmpty(replace_sale.Trim()))
            {
                List<dbo_CreditClass> listofitem_1 = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, replace_sale);
                listofitem = listofitem.Union(listofitem_1).ToList();
            }

            if (listofitem.Count == 0)
            {
                listofitem.Add(new dbo_CreditClass());
                GridViewCredit.DataSource = listofitem;
                GridViewCredit.DataBind();
                GridViewCredit.Rows[0].Visible = false;
            }
            else
            {
                GridViewCredit.DataSource = listofitem;
                GridViewCredit.DataBind();
            }

            // string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

            //dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
            //string User_ID = hdfUser_ID.Value;
            //string replace_sale = requ.Replace_Sales;

            // DropDownList ddl = (DropDownList)row.Cells[2].FindControl("ddlFooterCustomerName");

            DropDownList ddl = (DropDownList)GridViewCredit.FooterRow.FindControl("ddlFooterCustomerName");
            List<dbo_CustomerClass> customers = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, User_ID, string.Empty, string.Empty).Where(f => f.Status == "ยังติดต่ออยู่").ToList();

            //User ขายแทน
            if (!string.IsNullOrEmpty(replace_sale.Trim()))
            {
                List<dbo_CustomerClass> cus_replace = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, replace_sale, string.Empty, string.Empty).Where(f => f.Status == "ยังติดต่ออยู่").ToList();
                customers = customers.Union(cus_replace).ToList();
            }

            customers.Insert(0, new dbo_CustomerClass() { Customer_ID = "-1", Full_Name = "==ระบุ==" });

            ddl.DataSource = customers;
            ddl.DataBind();

            TextBox txtFooterCredit_Date = (TextBox)GridViewCredit.FooterRow.FindControl("txtFooterCredit_Date");

            txtFooterCredit_Date.Text = DateTime.Now.ToShortDateString();


            Session["Create_Credit"] = "Create_SS";
            btnCreditPaymentNext.Text = "บันทึกและหน้าถัดไป";
            // flag = true;
            //ddl.datas
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnAddNewCreditPayment_Click(object sender, EventArgs e)
    {
        try
        {
            int current_index = (int)ViewState["RowIndexCreditPayment"];
            GridView gv = (GridView)GridViewCreditPayment.Rows[current_index + 1].FindControl("GridViewCustomer");

            gv.ShowFooter = true;
            gv.EditIndex = -1;

            Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[current_index].FindControl("lblCredit_ID");

            List<dbo_CreditPaymentClass> item_value = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);

            if (item_value.Count == 0)
            {
                item_value.Add(new dbo_CreditPaymentClass());
                gv.DataSource = item_value;
                gv.DataBind();
                gv.Rows[0].Visible = false;
            }
            else
            {
                gv.DataSource = item_value;
                gv.DataBind();
            }

            Dictionary<string, string> bank = dbo_ItemDataClass.GetDropDown("12");

            //  bank.Remove(string.Empty);
            //bank.FirstOrDefault(f => f.Key == string.Empty).Value = "ระบุ";


            DropDownList ddl = (DropDownList)gv.FooterRow.FindControl("ddlFooterbank");
            ddl.DataSource = bank;
            ddl.DataBind();


            TextBox txtFooterPaymentDate = (TextBox)gv.FooterRow.FindControl("txtFooterPaymentDate");
            txtFooterPaymentDate.Text = DateTime.Now.ToShortDateString();

            // TextBox _txtNewEnd_Date = (TextBox)GridViewBenefit.FooterRow.FindControl("txtNewEnd_Date");
            Session["Create_PaymentCredit"] = "Create_SS";

            ButtonSubsidy.Text = "บันทึกและถัดไป";


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }

    }
    #endregion

    #region Button Step 3
    protected void btnBackToCredit_Click(object sender, EventArgs e)
    {

        if (Session["Create_Credit"] != null)
        {

            Session.Remove("Create_Credit");
        }
        if (Session["Create_PaymentCredit"] != null)
        {
            Session.Remove("Create_PaymentCredit");
        }

        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        ShowStep2();
    }

    public void ButtonSubsidy_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (Session["Create_PaymentCredit"] != null)
            {
                if (Add_PaymentCredit())
                {
                    if (Session["Create_PaymentCredit"] != null)
                    {

                        Session.Remove("Create_PaymentCredit");
                    }
                    logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    ShowStep4();
                }

            }
            else
            {

                if (Session["Create_PaymentCredit"] != null)
                {

                    Session.Remove("Create_PaymentCredit");
                }
                logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                ShowStep4();
            }

        }
        catch (Exception ex)
        {
            logger.Debug(ex.Message);
        }

        #region Old
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        //System.Threading.Thread.Sleep(1000);
        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        //ShowStep4();
        #endregion
    }

    protected void btnSearchCreditPayment_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        PaymentSearchSubmit();

        if (Session["Create_PaymentCredit"] != null)
        {
            Session.Remove("Create_PaymentCredit");
        }
        ButtonSubsidy.Text = "ถัดไป";
    }

    protected void btnSearchCreditPaymentCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        DropDownListCustomer.ClearSelection();
        txtSearchCreditPaymentDate.Text = string.Empty;
        ddlSearchCreditPaymentStatus.ClearSelection();

        List<dbo_CreditClass> listofitem = new List<dbo_CreditClass>();
        GridViewCreditPayment.DataSource = listofitem;
        GridViewCreditPayment.DataBind();

        GridViewCreditPayment.Visible = false;
        pnlNoCreditPayment.Visible = false;

        if (Session["Create_PaymentCredit"] != null)
        {
            Session.Remove("Create_PaymentCredit");
        }
        ButtonSubsidy.Text = "ถัดไป";

        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
    #endregion

    #region Button Step 4
    public void ButtonDeduct_Click(object sender, System.EventArgs e)
    {

        try
        {

            #region old
            //if (Session["Create_Deduct"] != null)
            //{       
            //    if(Add_Deduct())
            //    {
            //        if (Session["Create_Deduct"] != null)
            //        {
            //           Session.Remove("Create_Deduct");   
            //        }
            //    }
            //}
            //if (Session["Create_Subsid"] != null)
            //{

            //    if(Add_Subsidy())
            //    {
            //        if (Session["Create_Subsid"] != null)
            //        {
            //           Session.Remove("Create_Subsid");

            //        }
            //    }
            //}
            #endregion

            if (Session["Create_Subsid"] != null && Session["Create_Deduct"] != null)
            {
                if (Add_Subsidy())
                {
                    if (Session["Create_Subsid"] != null)
                    {
                        Session.Remove("Create_Subsid");

                    }

                    if (Add_Deduct())
                    {
                        if (Session["Create_Deduct"] != null)
                        {
                            Session.Remove("Create_Deduct");
                        }
                    }
                }
            }
            else if(Session["Create_Subsid"] != null && Session["Create_Deduct"] == null)
            {
                if (Add_Subsidy())
                {
                    if (Session["Create_Subsid"] != null)
                    {
                        Session.Remove("Create_Subsid");

                    }
                }
            }
            else if(Session["Create_Deduct"] != null && Session["Create_Subsid"] == null)
            {
                if (Add_Deduct())
                {
                    if (Session["Create_Deduct"] != null)
                    {
                        Session.Remove("Create_Deduct");
                    }
                }
            }

            if (Session["Create_Subsid"] == null && Session["Create_Deduct"] == null)
            {
                #region
                logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

                ShowStep5();
                txtDiscount.Focus();

                dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

                txtDiscount.Text = "0";
                txtActualPayment.Text = "0";


                //if (clearing.Status == "1")
                //{
                txtActualPayment.Enabled = true;
                txtDiscount.Enabled = true;

                ButtonSaveClearing.Text = "บันทึก";
                ButtonConfirmClearing.Visible = true;
                ButtonSaveClearing.Visible = true;

                GetPaymentDetail();
                //}
                //  else 
                if (clearing.Status == "2")
                {
                    txtActualPayment.Enabled = false;
                    txtDiscount.Enabled = false;

                    ButtonSaveClearing.Visible = false;
                    ButtonConfirmClearing.Visible = false;

                    GetPaymentDetail();
                }
                #endregion
            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnBackDeduct_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        ShowStep3();

        if (Session["Create_Deduct"] != null)
        {
            Session.Remove("Create_Deduct");
        }
        if (Session["Create_Subsid"] != null)
        {
            Session.Remove("Create_Subsid");
        }
        ButtonDeduct.Text = "ถัดไป";
    }

    protected void btnAddNewGridViewSubsidy_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_SubsidyClass> list_subsidy = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
            GridViewSubsidy.ShowFooter = true;
            GridViewSubsidy.EditIndex = -1;
            if (list_subsidy.Count == 0)
            {
                list_subsidy.Add(new dbo_SubsidyClass());

                GridViewSubsidy.DataSource = list_subsidy;
                GridViewSubsidy.DataBind();
                GridViewSubsidy.Rows[0].Visible = false;
            }
            else
            {
                GridViewSubsidy.DataSource = list_subsidy;
                GridViewSubsidy.DataBind();
            }

            List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountExpense();
            //dbo_AccountTypeDataClass.GetAccountCode_New("05");


            item_value.Insert(0, (new dbo_AccountCodeClass() { Account_Code = string.Empty, Account_Name = "==ระบุ==" }));
            DropDownList ddl = (DropDownList)GridViewSubsidy.FooterRow.FindControl("ddlFooterDetail");
            ddl.DataSource = item_value;
            ddl.DataBind();

            Session["Create_Subsid"] = "Create_SS";
            ButtonDeduct.Text = "บันทึกและถัดไป";

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }

    }

    protected void btnAddNewGridViewDeduct_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_DeductClass> list_deduct = dbo_DeductDataClass.Search(txtClearing_No.Text);
            GridViewDeduct.ShowFooter = true;
            GridViewDeduct.EditIndex = -1;

            if (list_deduct.Count == 0)
            {
                list_deduct.Add(new dbo_DeductClass());

                GridViewDeduct.DataSource = list_deduct;
                GridViewDeduct.DataBind();
                GridViewDeduct.Rows[0].Visible = false;
            }
            else
            {
                GridViewDeduct.DataSource = list_deduct;
                GridViewDeduct.DataBind();
            }

            List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountRevenue();
            // GetAccountCode_New("04");

            item_value.Insert(0, (new dbo_AccountCodeClass() { Account_Code = string.Empty, Account_Name = "==ระบุ==" }));
            DropDownList ddl = (DropDownList)GridViewDeduct.FooterRow.FindControl("ddlFooterDetail");
            ddl.DataSource = item_value;
            ddl.DataBind();

            Session["Create_Deduct"] = "Create_SS";

            ButtonDeduct.Text = "บันทึกและถัดไป";


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }
    #endregion

    #region Button Step 5
    public void ButtonSaveClearing_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            if (ButtonSaveClearing.Text == "แก้ไข")
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(1000);
                GetDetailsDataToForm(txtClearing_No.Text, "View");
                ButtonSave_Click(null, null);
            }
            else
            {
                List<dbo_DebtClass> search_dep = new List<dbo_DebtClass>();
                dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

                dbo_DepositClass deposit = dbo_DepositDataClass.Select_Record(txtClearing_No.Text);
                List<dbo_DepositDetailClass> depositdetail = dbo_DepositDetailDataClass.Search(txtClearing_No.Text, string.Empty);
                List<dbo_DepositDetailClass> depositdetail_month = dbo_DepositDetailDataClass.Search(string.Empty, DateTime.Now.Month.ToString());
                List<dbo_ClearingClass> list_clearing_this_month = dbo_ClearingDataClass.Search(string.Empty, null, null, hdfUser_ID.Value).Where(f => f.Clearing_Date.Value.Month == DateTime.Now.Month).OrderBy(g => g.Clearing_No).ToList();

                dbo_RequisitionClearingClass reqClearing = dbo_RequisitionClearingDataClass.Select_Record(txtClearing_No.Text);

                List<dbo_CreditClass> list_credit = dbo_CreditDataClass.Search("", "", null, "", reqClearing.User_ID);
                dbo_DepositClass depositSumNetSales = dbo_DepositDataClass.SumNetSalesBySPID(reqClearing.User_ID);
                dbo_CommissionClass commisionPoint = dbo_CommissionDataClass.SumPointBySP(reqClearing.User_ID);
                dbo_CommissionClass commisionSumTotal = dbo_CommissionDataClass.SumBalancetBySP(reqClearing.User_ID);
                dbo_RevenueExpenseClass revenue = dbo_RevenueExpenseDataClass.SumBySP(reqClearing.User_ID);

                string empty_requ = string.Empty;
                foreach (GridViewRow row in GridViewClearing.Rows)
                {
                    LinkButton Label_Status = (LinkButton)row.FindControl("lnkBClearing_No");
                    Label lnkB_Requisition_No = (Label)row.FindControl("lnkB_Requisition_No");
                    Label lblUser_ID = (Label)row.FindControl("lblUser_ID");

                    if (string.IsNullOrEmpty(Label_Status.Text) && lblUser_ID.Text == hdfUser_ID.Value)
                    {
                        empty_requ = lnkB_Requisition_No.Text;
                    }
                }

                if (hdfRequisition_No.Value == string.Empty)
                {
                    search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date).ToList();
                }
                else if (hdfRequisition_No.Value.Length > 16)
                {
                    search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date)
                  .Where(f => f.Requisition_No != hdfRequisition_No.Value.Substring(0, 16) && f.Requisition_No != hdfRequisition_No.Value.Substring(16, 16)).ToList();
                }
                else
                {
                    dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));
                    // dbo_RequisitionClass requi1 = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));
                    search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date)
                   .Where(f => f.Requisition_No != hdfRequisition_No.Value && f.Requisition_No != empty_requ).ToList();
                }

                decimal? calSumDebt = search_dep.Sum(f => f.Balance_Outstanding_Amount);

                if (txtActualPayment.Text == "")
                {
                    txtActualPayment.Text = "0";
                }

                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0";
                }

                if (txtDebtPayment.Text == "")
                {
                    txtDebtPayment.Text = "0";
                }

                clearing.Actual_Payment = decimal.Parse(txtActualPayment.Text.Replace(",", string.Empty));
                clearing.Balance_Outstanding = decimal.Parse(txtBalanceOutstanding.Text.Replace(",", string.Empty));
                clearing.Sub_Total = decimal.Parse(txtSubTotal.Text.Replace(",", string.Empty));
                clearing.Discount = decimal.Parse(txtDiscount.Text.Replace(",", string.Empty));
                clearing.Net_Sales_Amount = decimal.Parse(txtNet_Sales_Amount.Text.Replace(",", string.Empty));
                clearing.Net_Sales_Qty = Int16.Parse(txtNet_Sales_Qty.Text);
                clearing.SP_Cash = decimal.Parse(txtSP_Cash.Text.Replace(",", string.Empty));
                clearing.Today_Points = Int16.Parse(txtTodayPoints.Text);

                clearing.Cash_Payment_Amount = decimal.Parse(txtCashPaymentAmount.Text.Replace(",", string.Empty));
                clearing.Cheque_Payment_Amount = decimal.Parse(txtChequePaymentAmount.Text.Replace(",", string.Empty));
                clearing.Transfer_Payment_Amount = decimal.Parse(txtTransferPaymentAmount.Text.Replace(",", string.Empty));
                //clearing.Total_Deposit = Int16.Parse(depositdetail.Sum(f => f.Sales_Qty).ToString()); //decimal.Parse((depositdetail.Sum(f => f.Deposit_Qty * f.Price)).ToString());

                clearing.Total_Commission = commisionSumTotal.Commission;//deposit.Total_Commission;
                clearing.Today_Commission = decimal.Parse(txtTodayCommission.Text.Replace(",", string.Empty));

                clearing.Total_Deposit = revenue.Amount;
                clearing.Today_Deposit_Amount = decimal.Parse((depositdetail.Sum(f => f.Deposit_Qty * f.Selling_Price)).ToString());//Int16.Parse(depositdetail.Sum(f => f.Sales_Qty).ToString());
                clearing.Today_Return_Amount = decimal.Parse((depositdetail.Sum(f => f.Return_Qty * f.Selling_Price)).ToString());

                clearing.This_Month_Points = commisionPoint.Commission;// REMARK - Use commission coz field type;//decimal.Parse((list_clearing_this_month.Sum(f => f.Today_Points) + deposit.Tota_Point).ToString());
                clearing.This_Month_Sales_Amount = depositSumNetSales.Net_Sales_Amount;
                //clearing.This_Month_Sales_Amount = list_clearing_this_month.Sum(f => f.Net_Sales_Amount).Value + deposit.Net_Sales_Amount;
                //clearing.Total_Balance_Outstanding = list_clearing_this_month.Sum(f => f.Balance_Outstanding).Value + decimal.Parse(txtBalanceOutstanding.Text);

                clearing.Net_Total = decimal.Parse(txtNetTotal.Text.Replace(",", string.Empty));
                clearing.Debt_Payment = decimal.Parse(txtDebtPayment.Text.Replace(",", string.Empty));
                clearing.Debt_Balance = decimal.Parse(txtDebtBalance.Text.Replace(",", string.Empty));
                clearing.Total = decimal.Parse(txtTotal.Text.Replace(",", string.Empty));
                clearing.Credit_Amount = decimal.Parse(txtCredit_Amount.Text.Replace(",", string.Empty));
                clearing.Total_Credit_Amount = decimal.Parse(list_credit.Sum(f => f.Balance_Outstanding_Amount).ToString());
                clearing.Total_Balance_Outstanding = clearing.Credit_Amount + clearing.Balance_Outstanding;
                clearing.Debt_Total = calSumDebt.Value;

                dbo_ClearingDataClass.Update(clearing);

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(1000);
                Show("บันทึกสำเร็จ!");

                ShowStep0();
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void ButtonConfirmClearing_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(HttpContext.Current.Request.Cookies["User_ID"].Value);

        logger.Debug("hdfResign.Value " + hdfResign.Value);


        if (hdfResign.Value == "1")
        {
            #region Resign Function
            if (decimal.Parse(txtDebtBalance.Text.Replace(",", string.Empty)) == 0)
            {
                dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

                clearing.Actual_Payment += decimal.Parse(txtActualPayment.Text.Replace(",", string.Empty));
                clearing.Balance_Outstanding = decimal.Parse(txtBalanceOutstanding.Text.Replace(",", string.Empty));
                clearing.Net_Sales_Qty += Int16.Parse(txtNet_Sales_Qty.Text);
                clearing.Net_Sales_Amount += decimal.Parse(txtNet_Sales_Amount.Text.Replace(",", string.Empty));
                clearing.Discount += decimal.Parse(txtDiscount.Text.Replace(",", string.Empty));
                clearing.Debt_Payment += decimal.Parse(txtDebtPayment.Text.Replace(",", string.Empty));
                clearing.Debt_Balance = decimal.Parse(txtDebtBalance.Text.Replace(",", string.Empty));

                dbo_ClearingDataClass.Update(clearing);

                dbo_DebtClass debt_ = new dbo_DebtClass();
                debt_.Debt_ID = GenerateID.Debt_ID(string.Empty);
                debt_.CV_Code = user_class.CV_CODE;
                debt_.Debt_Amount = decimal.Parse(txtNet_Sales_Amount.Text.Replace(",", string.Empty));
                debt_.Debt_Date = DateTime.Now;
                debt_.SP_ID = hdfUser_ID.Value;
                debt_.Balance_Outstanding_Amount = decimal.Parse(txtNet_Sales_Amount.Text.Replace(",", string.Empty));
                debt_.Total_Payment_Amount = 0;
                debt_.Requisition_No = txtRequisition_No.Text;
                dbo_DebtDataClass.Add(debt_, HttpContext.Current.Request.Cookies["User_ID"].Value);

                decimal? init_payment = decimal.Parse(txtActualPayment.Text.Replace(",", string.Empty));

                init_payment += decimal.Parse(txtDiscount.Text.Replace(",", string.Empty));
                logger.Debug("init_payment " + init_payment);

                List<dbo_DebtClass> debt_list = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty);

                logger.Debug("debt_list.Count " + debt_list.Count);

                dbo_DebtClass last_debt_pay = null;

                foreach (dbo_DebtClass debt in debt_list.Where(f => f.Balance_Outstanding_Amount > 0))
                {
                    logger.Debug("debt_list debt: " + init_payment);

                    if (init_payment > 0)
                    {
                        decimal? cal_amount = init_payment - debt.Balance_Outstanding_Amount;

                        logger.Debug("debt: cal_amount " + cal_amount + " : initail_amount " + init_payment + " : debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                        dbo_FIFOPaymentClass fifo = new dbo_FIFOPaymentClass();
                        fifo.SP_ID = hdfUser_ID.Value;
                        fifo.Requisition_No = debt.Requisition_No;
                        fifo.CV_CODE = user_class.CV_CODE;
                        fifo.Debt_Date = DateTime.Now;
                        fifo.Created_By = HttpContext.Current.Request.Cookies["User_ID"].Value;

                        if (cal_amount > 0)
                        {
                            debt.Total_Payment_Amount += (debt.Balance_Outstanding_Amount);
                            init_payment -= debt.Balance_Outstanding_Amount;
                            debt.Balance_Outstanding_Amount = 0;
                            fifo.Payment_Amount = debt.Balance_Outstanding_Amount;

                            logger.Debug("debt: debt.Total_Payment_Amount " + debt.Total_Payment_Amount);
                            logger.Debug("debt: debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                        }
                        else
                        {
                            debt.Total_Payment_Amount += init_payment;
                            debt.Balance_Outstanding_Amount -= init_payment;
                            fifo.Payment_Amount = init_payment;
                            logger.Debug("debt: debt.Total_Payment_Amount " + debt.Total_Payment_Amount);
                            logger.Debug("debt: debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                            init_payment = 0;
                        }

                        dbo_DebtDataClass.Update(debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        last_debt_pay = debt;
                    }
                }

                #region Grid Clearing 1-5
                foreach (GridViewRow currentRow in GridViewClearing_1.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");

                        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Label_Product_ID.Text);
                        logger.Debug("prev_stock.Count " + prev_stock.Count);


                        if (prev_stock.Count > 0)
                        {
                            dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                            logger.Debug("Label_Product_ID.Text " + Label_Product_ID.Text);
                            logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                            logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                            short Stock_In = short.Parse((short.Parse(txt_Deposit_QtyReturn.Text) - short.Parse(hdfOldReturn_Qty.Value)).ToString());
                            logger.Debug("Stock_In " + Stock_In);
                            stock.Stock_In += Stock_In;
                            stock.Stock_End = short.Parse((stock.Stock_Begin + (stock.Stock_In - stock.Stock_Out)).ToString());
                            stock.Product_ID = Label_Product_ID.Text;

                            dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }

                    }
                }

                foreach (GridViewRow currentRow in GridViewClearing_2.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");

                        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Label_Product_ID.Text);
                        logger.Debug("prev_stock.Count " + prev_stock.Count);


                        if (prev_stock.Count > 0)
                        {
                            dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                            logger.Debug("Label_Product_ID.Text " + Label_Product_ID.Text);
                            logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                            logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                            short Stock_In = short.Parse((short.Parse(txt_Deposit_QtyReturn.Text) - short.Parse(hdfOldReturn_Qty.Value)).ToString());
                            logger.Debug("Stock_In " + Stock_In);
                            stock.Stock_In += Stock_In;
                            stock.Stock_End = short.Parse((stock.Stock_Begin + (stock.Stock_In - stock.Stock_Out)).ToString());
                            stock.Product_ID = Label_Product_ID.Text;

                            dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }

                    }
                }

                foreach (GridViewRow currentRow in GridViewClearing_3.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");

                        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Label_Product_ID.Text);
                        logger.Debug("prev_stock.Count " + prev_stock.Count);


                        if (prev_stock.Count > 0)
                        {
                            dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                            logger.Debug("Label_Product_ID.Text " + Label_Product_ID.Text);
                            logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                            logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                            short Stock_In = short.Parse((short.Parse(txt_Deposit_QtyReturn.Text) - short.Parse(hdfOldReturn_Qty.Value)).ToString());
                            logger.Debug("Stock_In " + Stock_In);
                            stock.Stock_In += Stock_In;
                            stock.Stock_End = short.Parse((stock.Stock_Begin + (stock.Stock_In - stock.Stock_Out)).ToString());
                            stock.Product_ID = Label_Product_ID.Text;

                            dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }

                    }
                }

                foreach (GridViewRow currentRow in GridViewClearing_4.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");

                        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Label_Product_ID.Text);
                        logger.Debug("prev_stock.Count " + prev_stock.Count);


                        if (prev_stock.Count > 0)
                        {
                            dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                            logger.Debug("Label_Product_ID.Text " + Label_Product_ID.Text);
                            logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                            logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                            short Stock_In = short.Parse((short.Parse(txt_Deposit_QtyReturn.Text) - short.Parse(hdfOldReturn_Qty.Value)).ToString());
                            logger.Debug("Stock_In " + Stock_In);
                            stock.Stock_In += Stock_In;
                            stock.Stock_End = short.Parse((stock.Stock_Begin + (stock.Stock_In - stock.Stock_Out)).ToString());
                            stock.Product_ID = Label_Product_ID.Text;

                            dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }

                    }
                }

                foreach (GridViewRow currentRow in GridViewClearing_5.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label Label_Product_ID = (Label)currentRow.FindControl("Label_Product_ID");

                        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Label_Product_ID.Text);
                        logger.Debug("prev_stock.Count " + prev_stock.Count);


                        if (prev_stock.Count > 0)
                        {
                            dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                            logger.Debug("Label_Product_ID.Text " + Label_Product_ID.Text);
                            logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                            logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                            short Stock_In = short.Parse((short.Parse(txt_Deposit_QtyReturn.Text) - short.Parse(hdfOldReturn_Qty.Value)).ToString());
                            logger.Debug("Stock_In " + Stock_In);
                            stock.Stock_In += Stock_In;
                            stock.Stock_End = short.Parse((stock.Stock_Begin + (stock.Stock_In - stock.Stock_Out)).ToString());
                            stock.Product_ID = Label_Product_ID.Text;

                            dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }

                    }
                }
                #endregion

                string Post_No = GenerateID.Post_No(user_class.CV_CODE);

                if (txtSP_Cash.Text.Trim() != "")
                {
                    decimal spCash = decimal.Parse(txtSP_Cash.Text);

                    if (spCash > 0)
                    {
                        dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                        rev.Post_No = Post_No;
                        rev.CV_Code = user_class.CV_CODE;
                        rev.Amount = spCash;
                        rev.Account_Code = "4011";
                        rev.Account_No = GenerateID.RV(user_class.CV_CODE);
                        rev.Remark = "เงินสด SP " + rev.Account_No;
                        rev.Post_Date = DateTime.Now;
                        rev.User_ID = hdfUser_ID.Value;
                        dbo_RevenueExpenseDataClass.AddSP(rev);
                    }
                }

                if (txtDiscount.Text.Trim() != "")
                {
                    decimal discount = decimal.Parse(txtDiscount.Text);
                    if (discount > 0)
                    {
                        dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                        rev.Post_No = Post_No;
                        rev.CV_Code = user_class.CV_CODE;
                        rev.Amount = discount;
                        rev.Account_Code = "5081";
                        rev.Account_No = GenerateID.EP(user_class.CV_CODE);
                        rev.Remark = "ส่วนลด " + rev.Account_No;
                        rev.Post_Date = DateTime.Now;
                        rev.User_ID = hdfUser_ID.Value;
                        dbo_RevenueExpenseDataClass.AddSP(rev);
                    }
                }

                // List<dbo_RequisitionClearingClass> req_cl = dbo_RequisitionClearingDataClass.SearchBySPID(hdfUser_ID.Value);


                List<dbo_RequisitionClass> req = dbo_RequisitionDataClass.Search(null, null, hdfUser_ID.Value, user_class.CV_CODE).Where(f => f.Status != "3").ToList();
                foreach (dbo_RequisitionClass r in req)
                {
                    r.Status = "3";
                    dbo_RequisitionDataClass.Update(r);
                }

                UpdateRecord();

                // Commisision
                //List<dbo_DepositDetailClass> lstDeptDetail = dbo_DepositDetailDataClass.Search(clearing.Clearing_No.ToString(), "");
                List<dbo_CommissionClass> comm = dbo_CommissionDataClass.Select_Record(txtRequisition_No.Text);
                dbo_DepositClass deposit = dbo_DepositDataClass.Select_Record(txtClearing_No.Text);
                //Decimal? sumCom = comm.Sum(f => f.Commission);
                // Decimal? sumPoint = comm.Sum(f => f.Point);
                comm[0].Commission_Balance_Outstanding += (deposit.Total_Commission - comm[0].Commission);
                comm[0].Commission += (deposit.Total_Commission - comm[0].Commission);
                comm[0].Point = deposit.Tota_Point;
                dbo_CommissionDataClass.Update(comm[0], HttpContext.Current.Request.Cookies["User_ID"].Value);


                // ADDITIONAL
                dbo_ClearingClass clearingUpdate = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

                dbo_RequisitionClearingClass reqClearing = dbo_RequisitionClearingDataClass.Select_Record(txtClearing_No.Text);
                List<dbo_DepositDetailClass> depositdetail = dbo_DepositDetailDataClass.Search(txtClearing_No.Text, string.Empty);
                List<dbo_CreditClass> list_credit = dbo_CreditDataClass.Search("", "", null, "", reqClearing.User_ID);
                dbo_CommissionClass commisionPoint = dbo_CommissionDataClass.SumPointBySP(reqClearing.User_ID);
                dbo_DepositClass depositSumNetSales = dbo_DepositDataClass.SumNetSalesBySPID(reqClearing.User_ID);
                dbo_CommissionClass commisionSumTotal = dbo_CommissionDataClass.SumBalancetBySP(reqClearing.User_ID);
                dbo_RevenueExpenseClass revenue = dbo_RevenueExpenseDataClass.SumBySP(reqClearing.User_ID);

                clearingUpdate.Credit_Amount = decimal.Parse(txtCredit_Amount.Text.Replace(",", string.Empty));
                clearingUpdate.Balance_Outstanding = decimal.Parse(txtBalanceOutstanding.Text.Replace(",", string.Empty));

                clearingUpdate.Today_Deposit_Amount = decimal.Parse((depositdetail.Sum(f => f.Deposit_Qty * f.Selling_Price)).ToString());//Int16.Parse(depositdetail.Sum(f => f.Sales_Qty).ToString());
                clearingUpdate.Today_Return_Amount = decimal.Parse((depositdetail.Sum(f => f.Return_Qty * f.Selling_Price)).ToString());
                clearingUpdate.Total_Credit_Amount = decimal.Parse(list_credit.Sum(f => f.Balance_Outstanding_Amount).ToString());
                clearingUpdate.Total_Balance_Outstanding = clearing.Credit_Amount + clearing.Balance_Outstanding;
                clearingUpdate.This_Month_Points = commisionPoint.Commission;// REMARK - Use commission coz field type;
                clearingUpdate.This_Month_Sales_Amount = depositSumNetSales.Net_Sales_Amount;
                clearingUpdate.Total_Deposit = revenue.Amount;
                clearingUpdate.Total_Commission = commisionSumTotal.Commission;//deposit.Total_Commission;
                clearingUpdate.Today_Commission += decimal.Parse(txtTodayCommission.Text.Replace(",", string.Empty));
                clearingUpdate.Today_Points += Int16.Parse(txtTodayPoints.Text);

                dbo_ClearingDataClass.Update(clearingUpdate);
                // END ADDITIONAL

                string Req_1 = string.Empty;// hdfRequisition_No.Value.Substring(0, 16);
                string Req_2 = string.Empty;//hdfRequisition_No.Value.Substring(16, 16);

                if (hdfRequisition_No.Value.Length > 16)
                {
                    Req_1 = hdfRequisition_No.Value.Substring(0, 16);
                    Req_2 = hdfRequisition_No.Value.Substring(16, 16);
                }
                else
                {
                    Req_1 = hdfRequisition_No.Value;
                }

                List<dbo_DepositDetailClass> list_deposit = dbo_DepositDetailDataClass.Search(txtClearing_No.Text, string.Empty);

                foreach (dbo_DepositDetailClass deposit_detail in list_deposit)
                {

                    short? deposit_qty = short.Parse((deposit_detail.Deposit_Qty).ToString());

                    if (!string.IsNullOrEmpty(Req_2))
                    {
                        List<dbo_RequisitionDetailClass> second_req = dbo_RequisitionDetailDataClass.Search(Req_2, deposit_detail.Product_ID).OrderBy(f => f.Time_No).ToList();

                        for (int x2 = second_req.Count - 1; x2 >= 0; x2--)
                        {
                            second_req[x2].Deposit_Qty = 0;
                            dbo_RequisitionDetailDataClass.Update(second_req[x2]);
                        }
                    }


                    List<dbo_RequisitionDetailClass> first_req = dbo_RequisitionDetailDataClass.Search(Req_1, deposit_detail.Product_ID).OrderBy(f => f.Time_No).ToList();

                    for (int x1 = first_req.Count - 1; x1 >= 0; x1--)
                    {
                        first_req[x1].Deposit_Qty = 0;
                        dbo_RequisitionDetailDataClass.Update(first_req[x1]);
                    }
                }

                // End

                string RQ_N01 = string.Empty;
                string RQ_No2 = string.Empty;
                List<dbo_RequisitionClearingClass> rq_no = dbo_RequisitionClearingDataClass.getrq(clearing.Clearing_No.ToString());
                string[] RQ_No = rq_no.Select(f => f.Requisition_No).ToArray();
                int RQ_No_tmp = 0;

                foreach (string RQNO in RQ_No)
                {
                    if (RQ_N01 == "")
                    {
                        RQ_N01 = RQNO;
                    }
                    else
                    {
                        RQ_No2 = RQNO;
                    }
                }

                string url = "../Report_From/ViewsReport.aspx?RPT=Clearing_No&PRM=" + clearing.Clearing_No.ToString();
                //string url = "../Report/ClearingViewer.aspx?RPT=RPT_ClearingSummaryInfo&PRM=" + clearing.Clearing_No.ToString() + "&RQ_N01=" + RQ_N01 + "&RQ_N02=" + RQ_No2;
                string url1 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Clearing_No&PRM=" + clearing.Clearing_No.ToString();
                string url2 = "";
                string url3 = "";
                if (RQ_No_tmp == 1)
                {
                    url2 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No&PRM=" + RQ_N01;
                }
                else
                {
                    url2 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No&PRM=" + RQ_N01;
                    url3 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No1&PRM1=" + RQ_No2;
                }
                string s = "";

                s += "window.open('" + url2 + "','popup_window2', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                if (RQ_No2 != "")
                {
                    s += "window.open('" + url3 + "','popup_window3', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                }
                s += "window.open('" + url + "', 'popup_window', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                s += "window.open('" + url1 + "','popup_window1', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);

                ShowStep0();

                btnSearch_Click(sender, e);

                //logger.Debug("--");
            }
            else
            {
                Show("กรุณาจ่ายเงินให้ครบ");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(1000);

            }
            #endregion
        }
        else
        {
            #region Normal Function
            try
            {
                dbo_CountStockClass stock1 = dbo_CountStockDataClass.Search(null, string.Empty, string.Empty, user_class.CV_CODE).FirstOrDefault(f => f.Status == "รอการคอนเฟิร์ม");

                if (stock1 != null)
                {
                    logger.Debug(stock1.Status + " " + stock1.Count_No);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    System.Threading.Thread.Sleep(1000);
                    Show("ระหว่างการนับสต๊อก ไม่สามารถยืนยันได้");
                }
                else
                {
                    List<dbo_DebtClass> search_dep = new List<dbo_DebtClass>();
                    dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

                    dbo_DepositClass deposit = dbo_DepositDataClass.Select_Record(txtClearing_No.Text);

                    List<dbo_DepositDetailClass> depositdetail = dbo_DepositDetailDataClass.Search(txtClearing_No.Text, string.Empty);

                    List<dbo_DepositDetailClass> depositdetail_month = dbo_DepositDetailDataClass.Search(string.Empty, DateTime.Now.Month.ToString());

                    List<dbo_ClearingClass> list_clearing_this_month = dbo_ClearingDataClass.Search(string.Empty, null
                        , null, hdfUser_ID.Value).Where(f => f.Clearing_Date.Value.Month == DateTime.Now.Month).OrderBy(g => g.Clearing_No).ToList();

                    dbo_RequisitionClearingClass reqClearing = dbo_RequisitionClearingDataClass.Select_Record(txtClearing_No.Text);

                    List<dbo_CreditClass> list_credit = dbo_CreditDataClass.Search("", "", null, "", reqClearing.User_ID);

                    dbo_DepositClass depositSumNetSales = dbo_DepositDataClass.SumNetSalesBySPID(reqClearing.User_ID);

                    dbo_CommissionClass commisionPoint = dbo_CommissionDataClass.SumPointBySP(reqClearing.User_ID);

                    dbo_CommissionClass commisionSumTotal = dbo_CommissionDataClass.SumBalancetBySP(reqClearing.User_ID);

                    dbo_RevenueExpenseClass revenue = dbo_RevenueExpenseDataClass.SumBySP(reqClearing.User_ID);

                    List<dbo_ClearingClass> com = dbo_ClearingDataClass.Get_Commission(string.Empty, DateTime.Now.Date.AddYears(-1)
                        , DateTime.Now.Date.AddDays(1), hdfUser_ID.Value, user_class.CV_CODE);

                    string empty_requ = string.Empty;
                    foreach (GridViewRow row in GridViewClearing.Rows)
                    {
                        LinkButton Label_Status = (LinkButton)row.FindControl("lnkBClearing_No");
                        Label lnkB_Requisition_No = (Label)row.FindControl("lnkB_Requisition_No");
                        Label lblUser_ID = (Label)row.FindControl("lblUser_ID");

                        if (string.IsNullOrEmpty(Label_Status.Text) && lblUser_ID.Text == hdfUser_ID.Value)
                        {
                            empty_requ = lnkB_Requisition_No.Text;
                        }
                    }

                    if (hdfRequisition_No.Value == string.Empty)
                    {
                        search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date).ToList();
                    }
                    else if (hdfRequisition_No.Value.Length > 16)
                    {
                        search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date)
                      .Where(f => f.Requisition_No != hdfRequisition_No.Value.Substring(0, 16) && f.Requisition_No != hdfRequisition_No.Value.Substring(16, 16)).ToList();
                    }
                    else
                    {
                        dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));
                        // dbo_RequisitionClass requi1 = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));
                        search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date)
                       .Where(f => f.Requisition_No != hdfRequisition_No.Value && f.Requisition_No != empty_requ).ToList();
                    }

                    decimal? calSumDebt = search_dep.Sum(f => f.Balance_Outstanding_Amount);

                    decimal? balance = com.Sum(f => f.Commission_Balance_Outstanding).Value;

                    if (txtActualPayment.Text == "")
                        txtActualPayment.Text = "0";

                    if (txtDiscount.Text == "")
                        txtDiscount.Text = "0";

                    if (txtDebtPayment.Text == "")
                        txtDebtPayment.Text = "0";

                    clearing.Actual_Payment = decimal.Parse(txtActualPayment.Text.Replace(",", string.Empty));
                    clearing.Balance_Outstanding = decimal.Parse(txtBalanceOutstanding.Text.Replace(",", string.Empty));
                    clearing.Sub_Total = decimal.Parse(txtSubTotal.Text.Replace(",", string.Empty));
                    clearing.Discount = decimal.Parse(txtDiscount.Text.Replace(",", string.Empty));
                    clearing.Net_Sales_Amount = decimal.Parse(txtNet_Sales_Amount.Text.Replace(",", string.Empty));
                    clearing.Net_Sales_Qty = Int16.Parse(txtNet_Sales_Qty.Text);
                    clearing.SP_Cash = decimal.Parse(txtSP_Cash.Text.Replace(",", string.Empty));
                    clearing.Today_Points = Int16.Parse(txtTodayPoints.Text);

                    clearing.Cash_Payment_Amount = decimal.Parse(txtCashPaymentAmount.Text.Replace(",", string.Empty));
                    clearing.Cheque_Payment_Amount = decimal.Parse(txtChequePaymentAmount.Text.Replace(",", string.Empty));
                    clearing.Transfer_Payment_Amount = decimal.Parse(txtTransferPaymentAmount.Text.Replace(",", string.Empty));

                    clearing.Total_Commission = commisionSumTotal.Commission;//deposit.Total_Commission;
                    clearing.Today_Commission = decimal.Parse(txtTodayCommission.Text.Replace(",", string.Empty));

                    clearing.Total_Balance_Outstanding = list_clearing_this_month.Sum(f => f.Balance_Outstanding).Value + decimal.Parse(txtBalanceOutstanding.Text);
                    clearing.Net_Total = decimal.Parse(txtNetTotal.Text.Replace(",", string.Empty));
                    clearing.Debt_Payment = decimal.Parse(txtDebtPayment.Text.Replace(",", string.Empty));
                    clearing.Debt_Balance = decimal.Parse(txtDebtBalance.Text.Replace(",", string.Empty));
                    clearing.Total = decimal.Parse(txtTotal.Text.Replace(",", string.Empty));
                    clearing.Credit_Amount = decimal.Parse(txtCredit_Amount.Text.Replace(",", string.Empty));

                    clearing.Total_Deposit = revenue.Amount;
                    clearing.Today_Deposit_Amount = decimal.Parse((depositdetail.Sum(f => f.Deposit_Qty * f.Selling_Price)).ToString());//Int16.Parse(depositdetail.Sum(f => f.Sales_Qty).ToString());
                    clearing.Today_Return_Amount = decimal.Parse((depositdetail.Sum(f => f.Return_Qty * f.Selling_Price)).ToString());

                    clearing.This_Month_Points = commisionPoint.Commission;// REMARK - Use commission coz field type;
                    clearing.This_Month_Sales_Amount = depositSumNetSales.Net_Sales_Amount;

                    clearing.Total_Credit_Amount = decimal.Parse(list_credit.Sum(f => f.Balance_Outstanding_Amount).ToString());
                    clearing.Total_Balance_Outstanding = clearing.Credit_Amount + clearing.Balance_Outstanding;
                    clearing.Status = "2"; //used
                    clearing.Debt_Total = calSumDebt.Value;

                    List<dbo_RequisitionClass> lis_req = new List<dbo_RequisitionClass>();

                    if (hdfRequisition_No.Value.Length > 16)
                    {
                        dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));
                        dbo_RequisitionClass requi1 = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));
                        lis_req.Add(requi);
                        lis_req.Add(requi1);
                    }
                    else
                    {
                        dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value);
                        lis_req.Add(requ);
                    }

                    foreach (dbo_RequisitionClass Requisition_No in lis_req)
                    {
                        List<dbo_DebtClass> debt_list = dbo_DebtDataClass.Search(hdfUser_ID.Value, "").OrderBy(f => f.Created_Date).ToList();
                        foreach (dbo_DebtClass debt in debt_list)
                        {
                            if (debt.Requisition_No == Requisition_No.Requisition_No)
                            {
                                logger.Debug("debt.Debt_ID " + debt.Debt_ID);
                                dbo_DebtDataClass.Delete(debt.Debt_ID);
                            }
                        }
                    }


                    // hdfRequisition_No.Value = "201567GT60102703201567GT60102704";
                    string Req_1 = string.Empty;// hdfRequisition_No.Value.Substring(0, 16);
                    string Req_2 = string.Empty;//hdfRequisition_No.Value.Substring(16, 16);

                    // txtClearing_No.Text = "201567CT60102702";


                    if (hdfRequisition_No.Value.Length > 16)
                    {
                        Req_1 = hdfRequisition_No.Value.Substring(0, 16);
                        Req_2 = hdfRequisition_No.Value.Substring(16, 16);

                        dbo_RequisitionClass requ1 = dbo_RequisitionDataClass.Select_Record(Req_1);
                        lis_req.Add(requ1);
                        dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(Req_2);
                        lis_req.Add(requ);
                    }
                    else
                    {
                        Req_1 = hdfRequisition_No.Value;
                        dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(Req_1);
                        lis_req.Add(requ);
                    }

                    List<dbo_DepositDetailClass> list_deposit = dbo_DepositDetailDataClass.Search(txtClearing_No.Text, string.Empty);

                    foreach (dbo_DepositDetailClass deposit_detail in list_deposit)
                    {
                        logger.Debug("deposit_detail.Product_ID " + deposit_detail.Product_ID + " : " + deposit_detail.Deposit_Qty);

                        short? deposit_qty = short.Parse((deposit_detail.Deposit_Qty).ToString());

                        if (!string.IsNullOrEmpty(Req_2))
                        {
                            List<dbo_RequisitionDetailClass> second_req = dbo_RequisitionDetailDataClass.Search(Req_2, deposit_detail.Product_ID).OrderBy(f => f.Time_No).ToList();

                            logger.Debug("== second_req == deposit_qty " + deposit_qty);

                            for (int x2 = second_req.Count - 1; x2 >= 0; x2--)
                            {
                                logger.Debug("deposit_qty " + deposit_qty);
                                if (deposit_qty > 0)
                                {
                                    short? Total_Qty = short.Parse((second_req[x2].Requisition_Qty + second_req[x2].Previous_Balance_Qty).ToString());
                                    logger.Debug("Total_Qty " + Total_Qty);
                                    if (deposit_qty - Total_Qty > 0)
                                    {
                                        second_req[x2].Deposit_Qty = Total_Qty;
                                        dbo_RequisitionDetailDataClass.Update(second_req[x2]);
                                        deposit_qty -= Total_Qty;
                                    }
                                    else
                                    {
                                        second_req[x2].Deposit_Qty = deposit_qty;
                                        dbo_RequisitionDetailDataClass.Update(second_req[x2]);
                                        deposit_qty = 0;
                                    }
                                }
                            }
                        }

                        logger.Debug("== first_req == deposit_qty " + deposit_qty);

                        List<dbo_RequisitionDetailClass> first_req = dbo_RequisitionDetailDataClass.Search(Req_1, deposit_detail.Product_ID).OrderBy(f => f.Time_No).ToList();
                        for (int x1 = first_req.Count - 1; x1 >= 0; x1--)
                        {
                            logger.Debug("deposit_qty " + deposit_qty);
                            if (deposit_qty > 0)
                            {
                                short? Total_Qty = short.Parse((first_req[x1].Requisition_Qty + first_req[x1].Previous_Balance_Qty).ToString());
                                logger.Debug("Total_Qty " + Total_Qty);

                                if (deposit_qty - Total_Qty > 0)
                                {
                                    first_req[x1].Deposit_Qty = Total_Qty;
                                    dbo_RequisitionDetailDataClass.Update(first_req[x1]);
                                    deposit_qty -= Total_Qty;
                                }
                                else
                                {
                                    // last 
                                    first_req[x1].Deposit_Qty = deposit_qty;

                                    dbo_RequisitionDetailDataClass.Update(first_req[x1]);
                                    deposit_qty = 0;
                                }
                            }
                        }

                        /*update stock return qty*/
                        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, deposit_detail.Product_ID);

                        if (prev_stock.Count > 0)
                        {
                            dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                            stock.Stock_In += deposit_detail.Return_Qty;
                            stock.Stock_End = short.Parse((stock.Stock_Begin + (stock.Stock_In - stock.Stock_Out)).ToString());

                            stock.Product_ID = deposit_detail.Product_ID;

                            dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }
                    }


                    foreach (dbo_DepositDetailClass deposit_detail in list_deposit)
                    {
                        logger.Debug("deposit_detail.Product_ID " + deposit_detail.Product_ID + " : " + deposit_detail.Return_Qty);

                        short? return_qty = short.Parse((deposit_detail.Return_Qty).ToString());

                        if (!string.IsNullOrEmpty(Req_2))
                        {
                            List<dbo_RequisitionDetailClass> second_req = dbo_RequisitionDetailDataClass.Search(Req_2, deposit_detail.Product_ID).OrderBy(f => f.Time_No).ToList();

                            logger.Debug("== second_req == return_qty " + return_qty);

                            for (int x2 = second_req.Count - 1; x2 >= 0; x2--)
                            {
                                logger.Debug("return_qty " + return_qty);
                                if (return_qty > 0)
                                {
                                    short? req2DepositQty = second_req[x2].Deposit_Qty == null ? 0 : second_req[x2].Deposit_Qty;
                                    short? Total_Qty = short.Parse(((second_req[x2].Requisition_Qty + second_req[x2].Previous_Balance_Qty) - (req2DepositQty)).ToString());
                                    logger.Debug("Total_Qty " + Total_Qty);
                                    if (return_qty - Total_Qty > 0)
                                    {
                                        second_req[x2].Return_Qty = Total_Qty;
                                        dbo_RequisitionDetailDataClass.Update(second_req[x2]);
                                        return_qty -= Total_Qty;
                                    }
                                    else
                                    {
                                        second_req[x2].Return_Qty = return_qty;
                                        dbo_RequisitionDetailDataClass.Update(second_req[x2]);
                                        return_qty = 0;
                                    }
                                }
                            }
                        }

                        logger.Debug("== first_req == return_qty " + return_qty);

                        List<dbo_RequisitionDetailClass> first_req = dbo_RequisitionDetailDataClass.Search(Req_1, deposit_detail.Product_ID).OrderBy(f => f.Time_No).ToList();
                        for (int x1 = first_req.Count - 1; x1 >= 0; x1--)
                        {
                            logger.Debug("return_qty " + return_qty);
                            if (return_qty > 0)
                            {
                                if (first_req[x1].Deposit_Qty == null)
                                {
                                    first_req[x1].Deposit_Qty = 0;
                                }
                                short? req1DepositQty = first_req[x1].Deposit_Qty == null ? 0 : first_req[x1].Deposit_Qty;
                                short? Total_Qty = short.Parse(((first_req[x1].Requisition_Qty + first_req[x1].Previous_Balance_Qty) - (first_req[x1].Deposit_Qty)).ToString());
                                logger.Debug("Total_Qty " + Total_Qty);

                                if (return_qty - Total_Qty > 0)
                                {
                                    first_req[x1].Return_Qty = Total_Qty;
                                    dbo_RequisitionDetailDataClass.Update(first_req[x1]);
                                    return_qty -= Total_Qty;
                                }
                                else
                                {
                                    // last 
                                    first_req[x1].Return_Qty = return_qty;
                                    dbo_RequisitionDetailDataClass.Update(first_req[x1]);
                                    return_qty = 0;
                                }
                            }
                        }

                    }

                    List<dbo_DebtClass> debt_req = new List<dbo_DebtClass>();

                    List<dbo_RequisitionClass> list_req_debt = dbo_RequisitionDataClass.Search(Req_1, string.Empty, hdfUser_ID.Value, null);
                    // dbo_RequisitionDataClass.Search(Req_1, null, hdfUser_ID.Value, user_class.CV_CODE);

                    dbo_DebtClass de = new dbo_DebtClass();

                    foreach (dbo_RequisitionClass requistion in list_req_debt)
                    {
                        List<dbo_RequisitionDetailClass> first_req_ = dbo_RequisitionDetailDataClass.Search(Req_1, string.Empty);
                        decimal? Debt_Amount = 0;
                        foreach (dbo_RequisitionDetailClass detail in first_req_.Where(f => f.Time_No == requistion.Time_No).ToList())
                        {
                            short? sum_total = short.Parse((detail.Previous_Balance_Qty + detail.Requisition_Qty).ToString());
                            short? Deposit = detail.Deposit_Qty == null ? 0 : detail.Deposit_Qty;
                            short? Return = detail.Return_Qty == null ? 0 : detail.Return_Qty;
                            Debt_Amount += ((sum_total - (Deposit + Return)) * detail.Selling_Price);
                        }

                        dbo_DebtClass debt = dbo_DebtDataClass.SelectByRequisitionNo(requistion.Requisition_No);
                        if (debt != null)
                        {
                            de = new dbo_DebtClass();
                            de.Debt_ID = debt.Debt_ID;//GenerateID.Debt_ID(string.Empty);
                            de.Debt_Amount = debt.Debt_Amount + Debt_Amount;
                            de.Balance_Outstanding_Amount = de.Debt_Amount;
                            de.CV_Code = user_class.CV_CODE;
                            de.Debt_Date = DateTime.Now;
                            de.SP_ID = hdfUser_ID.Value;
                            de.Requisition_No = Req_1;
                            dbo_DebtDataClass.Update(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
                            //debt_req.Add(de);
                        }
                        else
                        {
                            de = new dbo_DebtClass();
                            de.Debt_ID = GenerateID.Debt_ID(string.Empty);
                            de.Debt_Amount = Debt_Amount;
                            de.Balance_Outstanding_Amount = de.Debt_Amount;
                            de.CV_Code = user_class.CV_CODE;
                            de.Debt_Date = DateTime.Now;
                            de.SP_ID = hdfUser_ID.Value;
                            de.Requisition_No = Req_1;
                            dbo_DebtDataClass.Add(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
                            //debt_req.Add(de);
                        }
                    }

                    if (!string.IsNullOrEmpty(Req_2))
                    {
                        list_req_debt = dbo_RequisitionDataClass.Search(Req_2, string.Empty, hdfUser_ID.Value, null);

                        foreach (dbo_RequisitionClass requistion in list_req_debt)
                        {

                            List<dbo_RequisitionDetailClass> second_req_ = dbo_RequisitionDetailDataClass.Search(Req_2, string.Empty);
                            decimal? Debt_Amount_2 = 0;
                            foreach (dbo_RequisitionDetailClass detail in second_req_.Where(f => f.Time_No == requistion.Time_No).ToList())
                            {
                                short? sum_total = short.Parse((detail.Previous_Balance_Qty + detail.Requisition_Qty).ToString());
                                short? Deposit = detail.Deposit_Qty == null ? 0 : detail.Deposit_Qty;
                                short? Return = detail.Return_Qty == null ? 0 : detail.Return_Qty;
                                Debt_Amount_2 += ((sum_total - (Deposit + Return)) * detail.Selling_Price);
                            }

                            dbo_DebtClass debt = dbo_DebtDataClass.SelectByRequisitionNo(requistion.Requisition_No);
                            if (debt != null)
                            {
                                de = new dbo_DebtClass();
                                de.Debt_ID = debt.Debt_ID;//GenerateID.Debt_ID(string.Empty);
                                de.Debt_Amount = debt.Debt_Amount + Debt_Amount_2;
                                de.Balance_Outstanding_Amount = de.Debt_Amount;
                                de.CV_Code = user_class.CV_CODE;
                                de.Debt_Date = DateTime.Now;
                                de.SP_ID = hdfUser_ID.Value;
                                de.Requisition_No = Req_2;
                                dbo_DebtDataClass.Update(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
                                //debt_req.Add(de);
                            }
                            else
                            {
                                de = new dbo_DebtClass();
                                de.Debt_ID = GenerateID.Debt_ID(string.Empty);
                                de.Debt_Amount = Debt_Amount_2;
                                de.Balance_Outstanding_Amount = de.Debt_Amount;
                                de.CV_Code = user_class.CV_CODE;
                                de.Debt_Date = DateTime.Now;
                                de.SP_ID = hdfUser_ID.Value;
                                de.Requisition_No = Req_2;
                                dbo_DebtDataClass.Add(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
                                //debt_req.Add(de);
                            }
                        }
                    }

                    dbo_ClearingDataClass.Update(clearing);

                    logger.Debug("--deduct_list--");
                    string Post_No = GenerateID.Post_No(user_class.CV_CODE);
                    logger.Debug("Post_No " + Post_No);
                    List<dbo_DeductClass> deduct_list = dbo_DeductDataClass.Search(txtClearing_No.Text);

                    foreach (dbo_DeductClass deduct in deduct_list)
                    {
                        logger.Debug("deduct " + deduct.Deduct_ID);
                        dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                        rev.Post_No = Post_No;
                        rev.CV_Code = user_class.CV_CODE;
                        rev.Amount = deduct.Deduct_Amount;
                        rev.Account_Code = deduct.Account_Code;
                        rev.Account_No = GenerateID.RV(user_class.CV_CODE);
                        rev.Remark = "เงินหักอื่นๆ " + txtClearing_No.Text;
                        rev.Post_Date = DateTime.Now;
                        rev.User_ID = reqClearing.User_ID;
                        bool succes = false;
                        succes = dbo_RevenueExpenseDataClass.AddSP(rev);
                    }

                    List<dbo_SubsidyClass> subsidy_list = dbo_SubsidyDataClass.Search(txtClearing_No.Text);

                    foreach (dbo_SubsidyClass subsidy in subsidy_list)
                    {
                        logger.Debug("subsidy " + subsidy.Subsidy_ID);
                        dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                        rev.Post_No = Post_No;
                        rev.CV_Code = user_class.CV_CODE;
                        rev.Amount = subsidy.Subsidy_Amount;
                        rev.Account_Code = subsidy.Account_Code;
                        rev.Account_No = GenerateID.EP(user_class.CV_CODE);
                        rev.Remark = "เงินช่วยเหลือ " + txtClearing_No.Text;
                        rev.Post_Date = DateTime.Now;
                        rev.User_ID = reqClearing.User_ID;
                        bool succes = false;
                        succes = dbo_RevenueExpenseDataClass.AddSP(rev);
                    }

                    List<dbo_DebtClass> listdebtcust = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date).ToList();
                    List<dbo_CreditClass> credit_list = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value);

                    foreach (dbo_CreditClass credit in credit_list)
                    {
                        logger.Debug("credit.Credit_ID  " + credit.Credit_ID);
                        dbo_DebtClass debt = new dbo_DebtClass();
                        debt.Debt_ID = GenerateID.Debt_ID(string.Empty);
                        debt.CV_Code = user_class.CV_CODE;
                        debt.Debt_Amount = credit.Credit_Amount;
                        debt.Debt_Date = DateTime.Now;
                        debt.Customer_ID = credit.Customer_ID;
                        debt.Balance_Outstanding_Amount = credit.Credit_Amount;
                        debt.Total_Payment_Amount = 0;
                        debt.Requisition_No = string.Empty;

                        dbo_DebtDataClass.Add(debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
                    }

                    /////
                    decimal? initail_amount = (clearing.Actual_Payment + clearing.Discount + clearing.Credit_Amount);

                    if (subsidy_list.Count > 0)
                    {
                        decimal? subsidyTotal = subsidy_list.Sum(f => f.Subsidy_Amount);
                        if (subsidyTotal > 0)
                        {
                            initail_amount = initail_amount + subsidyTotal;
                        }
                    }

                    if (deduct_list.Count > 0)
                    {
                        decimal? deductTotal = deduct_list.Sum(f => f.Deduct_Amount);
                        if (deductTotal > 0)
                        {
                            initail_amount = initail_amount - deductTotal;
                        }

                        if (initail_amount < 0)
                            initail_amount = 0;
                    }

                    if (clearing.Cash_Payment_Amount > 0)
                    {
                        initail_amount = initail_amount - clearing.Cash_Payment_Amount;


                        if (initail_amount < 0)
                            initail_amount = 0;
                    }

                    foreach (dbo_RequisitionClass Requisition_No in lis_req)
                    {
                        debt_req = new List<dbo_DebtClass>();
                        dbo_DebtClass debt_ret = dbo_DebtDataClass.SelectByRequisitionNo(Requisition_No.Requisition_No);
                        debt_req.Add(debt_ret);

                        foreach (dbo_DebtClass debt in debt_req)
                        {
                            logger.Debug("debt: " + initail_amount);

                            decimal? cal_amount = initail_amount - debt.Balance_Outstanding_Amount;

                            logger.Debug("debt: cal_amount " + cal_amount + " : initail_amount " + initail_amount + " : debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                            dbo_FIFOPaymentClass fifo = new dbo_FIFOPaymentClass();
                            fifo.SP_ID = hdfUser_ID.Value;
                            fifo.Requisition_No = debt.Requisition_No;
                            fifo.CV_CODE = user_class.CV_CODE;
                            fifo.Debt_Date = DateTime.Now;
                            fifo.Created_By = HttpContext.Current.Request.Cookies["User_ID"].Value;

                            if (cal_amount >= 0)
                            {
                                debt.Total_Payment_Amount += (debt.Balance_Outstanding_Amount);
                                initail_amount -= debt.Balance_Outstanding_Amount;
                                fifo.Payment_Amount = debt.Balance_Outstanding_Amount;
                                debt.Balance_Outstanding_Amount = 0;

                                logger.Debug("debt: debt.Total_Payment_Amount " + debt.Total_Payment_Amount);
                                logger.Debug("debt: debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                                List<dbo_RequisitionClass> requ_list = dbo_RequisitionDataClass.Search(debt.Requisition_No, string.Empty, hdfUser_ID.Value, null).OrderBy(f => f.Time_No).ToList();
                                for (int o = requ_list.Count - 1; o >= 0; o--)
                                {
                                    logger.Debug("Time_No " + requ_list[o].Time_No + " : " + requ_list[o].Status);
                                    if (requ_list[o].Status == "1" || requ_list[o].Status == "2")
                                    {
                                        requ_list[o].Status = "3";
                                        dbo_RequisitionDataClass.Update(requ_list[o]);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                debt.Total_Payment_Amount += initail_amount;
                                debt.Balance_Outstanding_Amount -= initail_amount;
                                fifo.Payment_Amount = initail_amount;

                                logger.Debug("debt: debt.Total_Payment_Amount " + debt.Total_Payment_Amount);
                                logger.Debug("debt: debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                                initail_amount = 0;

                                List<dbo_RequisitionClass> requ_list = dbo_RequisitionDataClass.Search(debt.Requisition_No, string.Empty, hdfUser_ID.Value, null).OrderBy(f => f.Time_No).ToList();
                                for (int o = requ_list.Count - 1; o >= 0; o--)
                                {
                                    logger.Debug("Time_No " + requ_list[o].Time_No + " : " + requ_list[o].Status);

                                    if (requ_list[o].Status == "1")
                                    {
                                        requ_list[o].Status = "2";
                                        dbo_RequisitionDataClass.Update(requ_list[o]);
                                        break;
                                    }
                                }
                            }

                            dbo_DebtDataClass.Update(debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
                            dbo_FIFOPaymentDataClass.Add(fifo);

                        }
                    }

                    if (initail_amount > 0)
                    {
                        List<dbo_DebtClass> listDebtAll = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).Where(f => f.Balance_Outstanding_Amount > 0).OrderBy(f => f.Created_Date).ToList();

                        foreach (dbo_DebtClass debt in listDebtAll)
                        {
                            if (initail_amount > 0)
                            {
                                dbo_DebtClass deItem = new dbo_DebtClass();
                                deItem.Debt_ID = debt.Debt_ID;//GenerateID.Debt_ID(string.Empty);
                                //deItem.Total_Payment_Amount = debt.Debt_Amount + Debt_Amount_2;

                                if (initail_amount >= debt.Balance_Outstanding_Amount)
                                {
                                    decimal? remainInt = initail_amount - debt.Balance_Outstanding_Amount;
                                    deItem.Total_Payment_Amount = debt.Total_Payment_Amount + debt.Balance_Outstanding_Amount;
                                    deItem.Balance_Outstanding_Amount = 0;
                                    deItem.Debt_Date = debt.Debt_Date;
                                    deItem.Debt_Amount = debt.Debt_Amount;
                                    initail_amount = remainInt;

                                    dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(debt.Requisition_No);
                                    if (requ != null)
                                    {
                                        requ.Status = "3";
                                        dbo_RequisitionDataClass.Update(requ);

                                        dbo_FIFOPaymentClass fifo = new dbo_FIFOPaymentClass();
                                        fifo.SP_ID = hdfUser_ID.Value;
                                        fifo.Requisition_No = debt.Requisition_No;
                                        fifo.CV_CODE = user_class.CV_CODE;
                                        fifo.Debt_Date = DateTime.Now;
                                        fifo.Created_By = HttpContext.Current.Request.Cookies["User_ID"].Value;
                                        fifo.Payment_Amount = debt.Balance_Outstanding_Amount;
                                        dbo_FIFOPaymentDataClass.Add(fifo);
                                    }
                                }
                                else
                                {
                                    deItem.Balance_Outstanding_Amount = debt.Balance_Outstanding_Amount - initail_amount; ;
                                    deItem.Total_Payment_Amount = debt.Total_Payment_Amount + initail_amount;
                                    deItem.Debt_Date = debt.Debt_Date;
                                    deItem.Debt_Amount = debt.Debt_Amount;
                                    initail_amount = 0;

                                    dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(debt.Requisition_No);
                                    if (requ != null)
                                    {
                                        requ.Status = "2";
                                        dbo_RequisitionDataClass.Update(requ);

                                        dbo_FIFOPaymentClass fifo = new dbo_FIFOPaymentClass();
                                        fifo.SP_ID = hdfUser_ID.Value;
                                        fifo.Requisition_No = debt.Requisition_No;
                                        fifo.CV_CODE = user_class.CV_CODE;
                                        fifo.Debt_Date = DateTime.Now;
                                        fifo.Created_By = HttpContext.Current.Request.Cookies["User_ID"].Value;
                                        fifo.Payment_Amount = initail_amount;
                                        dbo_FIFOPaymentDataClass.Add(fifo);
                                    }
                                }

                                dbo_DebtDataClass.Update(deItem, HttpContext.Current.Request.Cookies["User_ID"].Value);
                            }
                        }
                    }

                    logger.Debug("initail_amount -- " + initail_amount);

                    if (hdfRequisition_No.Value.Length > 16)
                    {
                        dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));
                        dbo_RequisitionClass requi1 = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));
                        List<dbo_CommissionClass> comm = dbo_CommissionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));
                        List<dbo_CommissionClass> comm1 = dbo_CommissionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));
                        List<dbo_RequisitionDetailClass> one_req = dbo_RequisitionDetailDataClass.Search(hdfRequisition_No.Value.Substring(0, 16), "");
                        List<dbo_RequisitionDetailClass> second_req = dbo_RequisitionDetailDataClass.Search(hdfRequisition_No.Value.Substring(16, 16), "");

                        //Point
                        dbo_PriceGroupAssignmentClass priceGroup = new dbo_PriceGroupAssignmentClass();
                        priceGroup.Assign_To = requi.User_ID;
                        dbo_PriceGroupAssignmentClass priceGroupRet = dbo_PriceGroupAssignmentDataClass.SelectByAssignTo(priceGroup);
                        //

                        decimal? comm_Amount_1 = 0;
                        int? point_total_1 = 0;

                        foreach (dbo_RequisitionDetailClass item_req1 in one_req)
                        {
                            decimal? comPrice = item_req1.Selling_Price - item_req1.Price;
                            int? dep_qty = item_req1.Deposit_Qty == null ? 0 : item_req1.Deposit_Qty;
                            int? ret_qty = item_req1.Return_Qty == null ? 0 : item_req1.Return_Qty;
                            int? remain_qty = (item_req1.Requisition_Qty + item_req1.Previous_Balance_Qty) - (dep_qty + ret_qty);

                            if (comPrice > 0)
                            {
                                comm_Amount_1 += remain_qty * comPrice;
                            }

                            List<dbo_ProductListClass> listProduct = dbo_ProductListDataClass.Search("", priceGroupRet.Price_Group_ID, item_req1.Product_ID);
                            if (listProduct.Count > 0)
                            {
                                short? plPoint = listProduct[0].Point == null ? 0 : listProduct[0].Point;
                                point_total_1 += remain_qty * plPoint;
                            }
                        }

                        decimal? comm_Amount_2 = 0;
                        int? point_total_2 = 0;

                        foreach (dbo_RequisitionDetailClass item_req2 in second_req)
                        {
                            decimal? comPrice = item_req2.Selling_Price - item_req2.Price;
                            int? dep_qty = item_req2.Deposit_Qty == null ? 0 : item_req2.Deposit_Qty;
                            int? ret_qty = item_req2.Return_Qty == null ? 0 : item_req2.Return_Qty;
                            int? remain_qty = (item_req2.Requisition_Qty + item_req2.Previous_Balance_Qty) - (dep_qty + ret_qty);

                            if (comPrice > 0)
                            {
                                comm_Amount_2 += remain_qty * comPrice;
                            }

                            List<dbo_ProductListClass> listProduct = dbo_ProductListDataClass.Search("", priceGroupRet.Price_Group_ID, item_req2.Product_ID);
                            if (listProduct.Count > 0)
                            {
                                short? plPoint = listProduct[0].Point == null ? 0 : listProduct[0].Point;
                                point_total_2 += remain_qty * plPoint;
                            }
                        }

                        // Delete Commission
                        if (comm.Count > 0)
                            dbo_CommissionDataClass.Delete(comm[0]);

                        if (comm1.Count > 0)
                            dbo_CommissionDataClass.Delete(comm1[0]);

                        dbo_CommissionClass commInt1 = new dbo_CommissionClass();
                        commInt1.Requisition_No = requi.Requisition_No;
                        commInt1.Commission = comm_Amount_1;
                        commInt1.Point = (short?)point_total_1;
                        commInt1.Commission_Requisition_Status = 1;//
                        dbo_CommissionDataClass.Add(commInt1, HttpContext.Current.Request.Cookies["User_ID"].Value);

                        dbo_CommissionClass commInt2 = new dbo_CommissionClass();
                        commInt2.Requisition_No = requi1.Requisition_No;
                        commInt2.Commission = comm_Amount_2;
                        commInt2.Point = (short?)point_total_2;
                        commInt2.Commission_Requisition_Status = 1;//
                        dbo_CommissionDataClass.Add(commInt2, HttpContext.Current.Request.Cookies["User_ID"].Value);

                    }
                    else
                    {
                        List<dbo_CommissionClass> comm = dbo_CommissionDataClass.Select_Record(hdfRequisition_No.Value);
                        comm[0].Commission = deposit.Total_Commission;
                        comm[0].Point = deposit.Tota_Point;
                        comm[0].Commission_Balance_Outstanding = deposit.Total_Commission;
                        dbo_CommissionDataClass.Update(comm[0], HttpContext.Current.Request.Cookies["User_ID"].Value);
                    }

                    List<dbo_ClearingClass> list_clearing = dbo_ClearingDataClass.Search(string.Empty
                        , null, null, hdfUser_ID.Value).Where(f => f.Balance_Outstanding > 0).OrderBy(g => g.Clearing_No).ToList();

                    initail_amount = decimal.Parse(txtDebtPayment.Text.Replace(",", string.Empty));

                    logger.Debug("initail_amount " + initail_amount);
                    decimal? init_clear = initail_amount;

                    foreach (dbo_ClearingClass cle_ in list_clearing)
                    {
                        if (init_clear > 0)
                        {
                            decimal? cal_clear = init_clear - cle_.Balance_Outstanding;
                            List<dbo_RequisitionClearingClass> item_clear = dbo_RequisitionClearingDataClass.Search(cle_.Clearing_No, string.Empty
                                , string.Empty, null, null, null, null, user_class.CV_CODE);

                            logger.Debug("cal_clear " + cal_clear);

                            if (cal_clear >= 0)
                            {
                                init_clear -= cle_.Balance_Outstanding;
                                cle_.Balance_Outstanding = 0;
                                dbo_ClearingDataClass.Update(cle_);
                            }
                            else
                            {
                                cle_.Balance_Outstanding -= init_clear;
                                dbo_ClearingDataClass.Update(cle_);
                                init_clear = 0;
                            }
                        }
                    }

                    logger.Debug("customer payment");

                    List<dbo_CreditPaymentClass> creditpayment = dbo_CreditPaymentDataClass.Search("", txtClearing_No.Text);

                    foreach (dbo_CreditPaymentClass payment in creditpayment)
                    {
                        logger.Debug("payment.Payment_No " + payment.Payment_Amount);
                        decimal? init_payment = payment.Payment_Amount;

                        dbo_CreditClass credit = dbo_CreditDataClass.Select_Record(payment.Credit_ID);

                        List<dbo_DebtClass> debt_list = dbo_DebtDataClass.Search(string.Empty, credit.Customer_ID);

                        dbo_DebtClass last_debt_pay = null;

                        foreach (dbo_DebtClass debt in debt_list.Where(f => f.Balance_Outstanding_Amount > 0))
                        {
                            logger.Debug("debt_list debt: " + init_payment);

                            if (init_payment > 0)
                            {
                                decimal? cal_amount = init_payment - debt.Balance_Outstanding_Amount;

                                logger.Debug("debt: cal_amount " + cal_amount + " : initail_amount " + init_payment + " : debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                                if (cal_amount > 0)
                                {
                                    debt.Total_Payment_Amount += (debt.Balance_Outstanding_Amount);
                                    init_payment -= debt.Balance_Outstanding_Amount;
                                    debt.Balance_Outstanding_Amount = 0;

                                    logger.Debug("debt: debt.Total_Payment_Amount " + debt.Total_Payment_Amount);
                                    logger.Debug("debt: debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                                }
                                else
                                {
                                    debt.Total_Payment_Amount += init_payment;
                                    debt.Balance_Outstanding_Amount -= init_payment;

                                    logger.Debug("debt: debt.Total_Payment_Amount " + debt.Total_Payment_Amount);
                                    logger.Debug("debt: debt.Balance_Outstanding_Amount " + debt.Balance_Outstanding_Amount);

                                    init_payment = 0;
                                }

                                dbo_DebtDataClass.Update(debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
                                last_debt_pay = debt;
                            }
                        }


                        logger.Debug("init_payment " + init_payment);

                        if (init_payment > 0 && last_debt_pay != null)
                        {
                            last_debt_pay.Total_Payment_Amount += init_payment;
                            last_debt_pay.Balance_Outstanding_Amount -= init_payment;

                            dbo_DebtDataClass.Update(last_debt_pay, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }

                        payment.Last_Modified_Date = DateTime.Now;
                        //   when '1' then 'เงินสด' when '2' then 'เช็ค' when '3' then 'โอน' 
                        switch (payment.Payment_Method)
                        {
                            case "เงินสด":
                                payment.Payment_Method = "1";
                                break;
                            case "เช็ค":
                                payment.Payment_Method = "2";
                                break;
                            case "โอน":
                                payment.Payment_Method = "3";
                                break;
                        }


                        dbo_CreditPaymentDataClass.Update(payment);
                    }

                    // Insert Account
                    if (txtSP_Cash.Text.Trim() != "")
                    {
                        decimal spCash = decimal.Parse(txtSP_Cash.Text);

                        if (spCash > 0)
                        {
                            dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                            rev.Post_No = Post_No;
                            rev.CV_Code = user_class.CV_CODE;
                            rev.Amount = spCash;
                            rev.Account_Code = "4011";
                            rev.Account_No = GenerateID.RV(user_class.CV_CODE);
                            rev.Remark = "เงินสด SP " + rev.Account_No;
                            rev.Post_Date = DateTime.Now;
                            rev.User_ID = reqClearing.User_ID;
                            dbo_RevenueExpenseDataClass.AddSP(rev);
                        }
                    }

                    if (txtCashPaymentAmount.Text.Trim() != "" || txtChequePaymentAmount.Text.Trim() != "" || txtTransferPaymentAmount.Text.Trim() != "")
                    {
                        decimal total = 0;

                        if (txtCashPaymentAmount.Text.Trim() != "")
                        {
                            total += decimal.Parse(txtCashPaymentAmount.Text.Trim());
                        }

                        if (txtChequePaymentAmount.Text.Trim() != "")
                        {
                            total += decimal.Parse(txtChequePaymentAmount.Text.Trim());
                        }

                        if (txtTransferPaymentAmount.Text.Trim() != "")
                        {
                            total += decimal.Parse(txtTransferPaymentAmount.Text.Trim());
                        }

                        if (total > 0)
                        {
                            dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                            rev.Post_No = Post_No;
                            rev.CV_Code = user_class.CV_CODE;
                            rev.Amount = total;
                            rev.Account_Code = "4012";
                            rev.Account_No = GenerateID.RV(user_class.CV_CODE);
                            rev.Remark = "โอน " + rev.Account_No;
                            rev.Post_Date = DateTime.Now;
                            rev.User_ID = reqClearing.User_ID;
                            dbo_RevenueExpenseDataClass.AddSP(rev);
                        }
                    }

                    if (txtDebtPayment.Text.Trim() != "")
                    {
                        decimal debtPayment = decimal.Parse(txtDebtPayment.Text);
                        if (debtPayment > 0)
                        {
                            dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                            rev.Post_No = Post_No;
                            rev.CV_Code = user_class.CV_CODE;
                            rev.Amount = debtPayment;
                            rev.Account_Code = "4016";
                            rev.Account_No = GenerateID.RV(user_class.CV_CODE);
                            rev.Remark = "ชำระหนี้เงินสด " + rev.Account_No;
                            rev.Post_Date = DateTime.Now;
                            rev.User_ID = reqClearing.User_ID;
                            dbo_RevenueExpenseDataClass.AddSP(rev);
                        }
                    }

                    if (txtBalanceOutstanding.Text.Trim() != "")
                    {
                        decimal balanceOut = decimal.Parse(txtBalanceOutstanding.Text);
                        if (balanceOut > 0)
                        {
                            dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                            rev.Post_No = Post_No;
                            rev.CV_Code = user_class.CV_CODE;
                            rev.Amount = balanceOut;
                            rev.Account_Code = "5061";
                            rev.Account_No = GenerateID.EP(user_class.CV_CODE);
                            rev.Remark = "ค้างชำระ " + rev.Account_No;
                            rev.Post_Date = DateTime.Now;
                            rev.User_ID = reqClearing.User_ID;
                            dbo_RevenueExpenseDataClass.AddSP(rev);
                        }
                    }

                    if (txtDiscount.Text.Trim() != "")
                    {
                        decimal discount = decimal.Parse(txtDiscount.Text);
                        if (discount > 0)
                        {
                            dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                            rev.Post_No = Post_No;
                            rev.CV_Code = user_class.CV_CODE;
                            rev.Amount = discount;
                            rev.Account_Code = "5081";
                            rev.Account_No = GenerateID.EP(user_class.CV_CODE);
                            rev.Remark = "ส่วนลด " + rev.Account_No;
                            rev.Post_Date = DateTime.Now;
                            rev.User_ID = reqClearing.User_ID;
                            dbo_RevenueExpenseDataClass.AddSP(rev);
                        }
                    }

                    string RQ_N01 = string.Empty;
                    string RQ_No2 = string.Empty;
                    List<dbo_RequisitionClearingClass> rq_no = dbo_RequisitionClearingDataClass.getrq(clearing.Clearing_No.ToString());
                    string[] RQ_No = rq_no.Select(f => f.Requisition_No).ToArray();
                    int RQ_No_tmp = 0;

                    foreach (string RQNO in RQ_No)
                    {
                        if (RQ_N01 == "")
                        {
                            RQ_N01 = RQNO;
                        }
                        else
                        {
                            RQ_No2 = RQNO;
                        }
                    }

                    string url = "../Report_From/ViewsReport.aspx?RPT=Clearing_No&PRM=" + clearing.Clearing_No.ToString();
                    //mage PDF
                    //string url = "../Report/ClearingViewer.aspx?RPT=RPT_ClearingSummaryInfo&PRM=" + clearing.Clearing_No.ToString() + "&RQ_N01=" + RQ_N01 + "&RQ_N02=" + RQ_No2;
                    string url1 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Clearing_No&PRM=" + clearing.Clearing_No.ToString();
                    string url2 = "";
                    string url3 = "";
                    if (RQ_No_tmp == 1)
                    {
                        url2 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No&PRM=" + RQ_N01;
                    }
                    else
                    {
                        url2 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No&PRM=" + RQ_N01;
                        url3 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No1&PRM1=" + RQ_No2;
                    }
                    string s = "";
                    s += "window.open('" + url2 + "','popup_window2', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                    if (RQ_No2 != "")
                    {
                        s += "window.open('" + url3 + "','popup_window3', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                    }
                    s += "window.open('" + url + "', 'popup_window', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                    s += "window.open('" + url1 + "','popup_window1', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";


                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);

                    ShowStep0();

                    btnSearch_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            #endregion
        }
    }

    public void ButtonCancel_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        System.Threading.Thread.Sleep(1000);
        ShowStep0();
    }
    #endregion

    #region Button Step
    protected void ButtonStep1_Click(object sender, EventArgs e)
    {
        if (Session["Create_Credit"] != null)
        {
            Session.Remove("Create_Credit");
        }

        if (Session["Create_PaymentCredit"] != null)
        {
            Session.Remove("Create_PaymentCredit");
        }
        btnCreditPaymentNext.Text = "ถัดไป";
        //ButtonSubsidy.Text = "ถัดไป";
        ShowStep1();
    }

    protected void ButtonStep2_Click(object sender, EventArgs e)
    {
        if (Session["Create_Credit"] != null)
        {
            Session.Remove("Create_Credit");
        }
        if (Session["Create_PaymentCredit"] != null)
        {
            Session.Remove("Create_PaymentCredit");
        }
        //btnCreditPaymentNext.Text = "ถัดไป";
        ButtonSubsidy.Text = "ถัดไป";
        ShowStep2();
    }

    protected void ButtonStep3_Click(object sender, EventArgs e)
    {

        if (Session["Create_Deduct"] != null)
        {
            Session.Remove("Create_Deduct");
        }
        if (Session["Create_Subsid"] != null)
        {
            Session.Remove("Create_Subsid");
        }
        ButtonDeduct.Text = "ถัดไป";
        ShowStep3();
    }

    protected void ButtonStep4_Click(object sender, EventArgs e)
    {
        ShowStep4();
    }

    protected void ButtonStep5_Click(object sender, EventArgs e)
    {
        ShowStep5();
    }
    #endregion

    #endregion

    #region GridView Data Bound
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewClearing.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewClearing.PageIndex = pageList.SelectedIndex;
        btnSearch_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewClearing_1_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_1"];
            Session.Remove("GetProduct_Quantity_tab_1");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewClearing_1.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 10;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;
                    row.Cells[8].Visible = false;
                    row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox txt_Deposit_Qty = (TextBox)row.FindControl("txt_Deposit_Qty");
                    TextBox txt_Deposit_QtyReturn = (TextBox)row.FindControl("txt_Deposit_QtyReturn");

                    Label lbl_PricePerUnit = (Label)row.FindControl("lbl_PricePerUnit");
                    Label Label_Net_Sales_Qty = (Label)row.FindControl("Label_Net_Sales_Qty");

                    if (ButtonSave.Text == "แก้ไข" || string.IsNullOrEmpty(lbl_PricePerUnit.Text) || Label_Net_Sales_Qty.Text == "0")
                    {
                        txt_Deposit_Qty.Enabled = false;
                        txt_Deposit_QtyReturn.Enabled = false;
                    }
                    else
                    {
                        txt_Deposit_Qty.Enabled = true;
                        txt_Deposit_QtyReturn.Enabled = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewClearing_2_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {


            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_2"];
            Session.Remove("GetProduct_Quantity_tab_2");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewClearing_2.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 10;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;
                    row.Cells[8].Visible = false;
                    row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox txt_Deposit_Qty = (TextBox)row.FindControl("txt_Deposit_Qty");
                    TextBox txt_Deposit_QtyReturn = (TextBox)row.FindControl("txt_Deposit_QtyReturn");

                    Label lbl_PricePerUnit = (Label)row.FindControl("lbl_PricePerUnit");
                    Label Label_Net_Sales_Qty = (Label)row.FindControl("Label_Net_Sales_Qty");

                    if (ButtonSave.Text == "แก้ไข" || string.IsNullOrEmpty(lbl_PricePerUnit.Text) || Label_Net_Sales_Qty.Text == "0")
                    {
                        txt_Deposit_Qty.Enabled = false;
                        txt_Deposit_QtyReturn.Enabled = false;
                    }
                    else
                    {
                        txt_Deposit_Qty.Enabled = true;
                        txt_Deposit_QtyReturn.Enabled = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewClearing_3_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_3"];
            Session.Remove("GetProduct_Quantity_tab_3");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewClearing_3.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 10;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;
                    row.Cells[8].Visible = false;
                    row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox txt_Deposit_Qty = (TextBox)row.FindControl("txt_Deposit_Qty");
                    TextBox txt_Deposit_QtyReturn = (TextBox)row.FindControl("txt_Deposit_QtyReturn");

                    Label lbl_PricePerUnit = (Label)row.FindControl("lbl_PricePerUnit");
                    Label Label_Net_Sales_Qty = (Label)row.FindControl("Label_Net_Sales_Qty");

                    if (ButtonSave.Text == "แก้ไข" || string.IsNullOrEmpty(lbl_PricePerUnit.Text) || Label_Net_Sales_Qty.Text == "0")
                    {
                        txt_Deposit_Qty.Enabled = false;
                        txt_Deposit_QtyReturn.Enabled = false;
                    }
                    else
                    {
                        txt_Deposit_Qty.Enabled = true;
                        txt_Deposit_QtyReturn.Enabled = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewClearing_4_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_4"];
            Session.Remove("GetProduct_Quantity_tab_4");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewClearing_4.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 10;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;
                    row.Cells[8].Visible = false;
                    row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox txt_Deposit_Qty = (TextBox)row.FindControl("txt_Deposit_Qty");
                    TextBox txt_Deposit_QtyReturn = (TextBox)row.FindControl("txt_Deposit_QtyReturn");

                    Label lbl_PricePerUnit = (Label)row.FindControl("lbl_PricePerUnit");
                    Label Label_Net_Sales_Qty = (Label)row.FindControl("Label_Net_Sales_Qty");

                    if (ButtonSave.Text == "แก้ไข" || string.IsNullOrEmpty(lbl_PricePerUnit.Text) || Label_Net_Sales_Qty.Text == "0")
                    {
                        txt_Deposit_Qty.Enabled = false;
                        txt_Deposit_QtyReturn.Enabled = false;
                    }
                    else
                    {
                        txt_Deposit_Qty.Enabled = true;
                        txt_Deposit_QtyReturn.Enabled = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewClearing_5_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_5"];
            Session.Remove("GetProduct_Quantity_tab_5");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewClearing_5.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");

                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 10;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;
                    row.Cells[8].Visible = false;
                    row.Cells[9].Visible = false;
                    // row.Cells[10].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    TextBox txt_Deposit_Qty = (TextBox)row.FindControl("txt_Deposit_Qty");
                    TextBox txt_Deposit_QtyReturn = (TextBox)row.FindControl("txt_Deposit_QtyReturn");

                    Label lbl_PricePerUnit = (Label)row.FindControl("lbl_PricePerUnit");
                    Label Label_Net_Sales_Qty = (Label)row.FindControl("Label_Net_Sales_Qty");

                    if (ButtonSave.Text == "แก้ไข" || string.IsNullOrEmpty(lbl_PricePerUnit.Text) || Label_Net_Sales_Qty.Text == "0")
                    {
                        txt_Deposit_Qty.Enabled = false;
                        txt_Deposit_QtyReturn.Enabled = false;
                    }
                    else
                    {
                        txt_Deposit_Qty.Enabled = true;
                        txt_Deposit_QtyReturn.Enabled = true;
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

    #region Methods
    private void show_grid(string User_ID, string Clearing_No, DateTime? pricedate)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_ProductClass> item1 = dbo_ClearingDataClass.GetDepositByProductGroupID(User_ID, Clearing_No, pricedate, "นมสดพาสเจอร์ไรส์", hdfRequisition_No.Value);

            // hdfPriceExpire

            foreach (dbo_ProductClass product in item1)
            {
                if (product.End_Effective_Date.HasValue)
                {
                    if (product.End_Effective_Date.Value.Date == DateTime.Now.Date)
                    {
                        hdfPriceExpire.Value += " " + product.Product_Name;
                    }
                }

            }


            Session["GetProduct_Quantity_tab_1"] = item1;
            if (item1.Count > 0)
            {
                GridViewClearing_1.DataSource = item1;
                GridViewClearing_1.DataBind();
                clearingTab1.Visible = true;
            }
            else
            {
                clearingTab1.Visible = false;
            }

            List<dbo_ProductClass> item2 = dbo_ClearingDataClass.GetDepositByProductGroupID(User_ID, Clearing_No, pricedate, "นมเปรี้ยว", hdfRequisition_No.Value);

            foreach (dbo_ProductClass product in item2)
            {
                if (product.End_Effective_Date.HasValue)
                {
                    if (product.End_Effective_Date.Value.Date == DateTime.Now.Date)
                    {
                        hdfPriceExpire.Value += " " + product.Product_Name;
                    }
                }
            }
            Session["GetProduct_Quantity_tab_2"] = item2;
            if (item2.Count > 0)
            {
                GridViewClearing_2.DataSource = item2;
                GridViewClearing_2.DataBind();
                clearingTab2.Visible = true;
            }
            else
            {
                clearingTab2.Visible = false;
            }

            List<dbo_ProductClass> item3 = dbo_ClearingDataClass.GetDepositByProductGroupID(User_ID, Clearing_No, pricedate, "โยเกิร์ตเมจิ", hdfRequisition_No.Value);

            foreach (dbo_ProductClass product in item3)
            {
                if (product.End_Effective_Date.HasValue)
                {
                    if (product.End_Effective_Date.Value.Date == DateTime.Now.Date)
                    {
                        hdfPriceExpire.Value += " " + product.Product_Name;
                    }
                }
            }

            Session["GetProduct_Quantity_tab_3"] = item3;
            if (item3.Count > 0)
            {
                GridViewClearing_3.DataSource = item3;
                GridViewClearing_3.DataBind();
                clearingTab3.Visible = true;
            }
            else
            {
                clearingTab3.Visible = false;
            }

            List<dbo_ProductClass> item4 = dbo_ClearingDataClass.GetDepositByProductGroupID(User_ID, Clearing_No, pricedate, "นมเปรี้ยวไพเกน", hdfRequisition_No.Value);
            foreach (dbo_ProductClass product in item4)
            {
                if (product.End_Effective_Date.HasValue)
                {
                    if (product.End_Effective_Date.Value.Date == DateTime.Now.Date)
                    {
                        hdfPriceExpire.Value += " " + product.Product_Name;
                    }
                }
            }

            Session["GetProduct_Quantity_tab_4"] = item4;
            if (item4.Count > 0)
            {
                GridViewClearing_4.DataSource = item4;
                GridViewClearing_4.DataBind();
                clearingTab4.Visible = true;
            }
            else
            {
                clearingTab4.Visible = false;
            }

            List<dbo_ProductClass> item5 = dbo_ClearingDataClass.GetDepositByProductGroupID(User_ID, Clearing_No, pricedate, "อื่นๆ", hdfRequisition_No.Value);

            foreach (dbo_ProductClass product in item5)
            {
                if (product.End_Effective_Date.HasValue)
                {
                    if (product.End_Effective_Date.Value.Date == DateTime.Now.Date)
                    {
                        hdfPriceExpire.Value += " " + product.Product_Name;
                    }
                }
            }
            Session["GetProduct_Quantity_tab_5"] = item5;
            if (item5.Count > 0)
            {
                GridViewClearing_5.DataSource = item5;
                GridViewClearing_5.DataBind();
                clearingTab5.Visible = true;
            }
            else
            {
                clearingTab5.Visible = false;
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

        int all_sum = 0;

        try
        {
            foreach (GridViewRow currentRow in GridViewClearing_1.Rows)
            {
                //int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    all_sum += int.Parse(txt.Text);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            foreach (GridViewRow currentRow in GridViewClearing_2.Rows)
            {
                //int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    all_sum += int.Parse(txt.Text);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            foreach (GridViewRow currentRow in GridViewClearing_3.Rows)
            {
                //int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    all_sum += int.Parse(txt.Text);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            foreach (GridViewRow currentRow in GridViewClearing_4.Rows)
            {
                //int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    all_sum += int.Parse(txt.Text);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            foreach (GridViewRow currentRow in GridViewClearing_5.Rows)
            {
                //int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    all_sum += int.Parse(txt.Text);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


        try
        {

            TextboxTotalAmount.Text = all_sum.ToString("#,##0");

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    private void ShowStep0()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        pnlStep.Visible = false;

        ButtonStep1.CssClass = "btn-danger btn-circle btn-lg";
        ButtonStep2.CssClass = "btn-danger btn-circle btn-lg";
        ButtonStep3.CssClass = "btn-danger btn-circle btn-lg";
        ButtonStep4.CssClass = "btn-danger btn-circle btn-lg";
        ButtonStep5.CssClass = "btn-danger btn-circle btn-lg";
        // ButtonStep6.CssClass = "btn-danger btn-circle btn-lg";

        pnlGrid.Visible = true;

        pnlStep1.Visible = false;
        pnlStep2.Visible = false;
        pnlStep3.Visible = false;
        pnlStep4.Visible = false;
        pnlStep5.Visible = false;
        //   pnlStep6.Visible = false;
        //pnlFooter.Visible = true;

        pnlResign.Visible = false;
    }

    private void ShowStep1()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            pnlStep.Visible = true;

            ButtonStep1.CssClass = "btn-info btn-circle btn-lg";
            ButtonStep2.CssClass = "btn-danger btn-circle btn-lg";
            ButtonStep3.CssClass = "btn-danger btn-circle btn-lg";
            ButtonStep4.CssClass = "btn-danger btn-circle btn-lg";
            ButtonStep5.CssClass = "btn-danger btn-circle btn-lg";


            //ButtonStep6.CssClass = "btn-danger btn-circle btn-lg";
            pnlStep1.Visible = true;
            //pnlFooter.Visible = true;
            pnlStep2.Visible = false;
            pnlStep3.Visible = false;
            pnlStep4.Visible = false;
            pnlStep5.Visible = false;
            //  pnlStep6.Visible = false;
            pnlGrid.Visible = false;

            ButtonStep1.Enabled = false;
            ButtonStep2.Enabled = false;
            ButtonStep3.Enabled = false;
            ButtonStep4.Enabled = false;
            ButtonStep5.Enabled = false;


            if (hdfResign.Value == "1")
            {
                ButtonSave.Visible = false;
                pnlStep.Visible = false;
                pnlResign.Visible = true;

                ButtonResignStep1.CssClass = "btn-info btn-circle btn-lg";
                ButtonResignStep2.CssClass = "btn-danger btn-circle btn-lg";
                ButtonResignStep1.Enabled = false;
                ButtonResignStep2.Enabled = false;
            }
            else
            {
                ButtonSave.Visible = true;
            }
        }
        catch (Exception)
        {

        }
    }

    private void ShowStep2()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        ButtonStep1.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep2.CssClass = "btn-info btn-circle btn-lg";
        ButtonStep3.CssClass = "btn-danger btn-circle btn-lg";
        ButtonStep4.CssClass = "btn-danger btn-circle btn-lg";
        ButtonStep5.CssClass = "btn-danger btn-circle btn-lg";
        //ButtonStep6.CssClass = "btn-danger btn-circle btn-lg";

        ButtonStep1.Enabled = true;
        ButtonStep2.Enabled = false;
        ButtonStep3.Enabled = false;
        ButtonStep4.Enabled = false;
        ButtonStep5.Enabled = false;
        //  ButtonStep6.Enabled = false;

        pnlStep1.Visible = false;
        pnlStep2.Visible = true;
        pnlStep3.Visible = false;
        pnlStep4.Visible = false;
        pnlStep5.Visible = false;
        // pnlStep6.Visible = false;

        ButtonStep1.Enabled = true;
        ButtonStep2.Enabled = false;
        ButtonStep3.Enabled = false;
        ButtonStep4.Enabled = false;
        ButtonStep5.Enabled = false;

        try
        {
            dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
            string replace_sale = requ.Replace_Sales;

            GridViewCredit.ShowFooter = false;
            List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value);
            
            // ขายแทน
            if (!string.IsNullOrEmpty(replace_sale.Trim()))
            {
                List<dbo_CreditClass> listofitem_1 = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, replace_sale); // Remove hdfUser_ID
                listofitem = listofitem.Union(listofitem_1).ToList();
            }


            GridViewCredit.DataSource = listofitem;
            GridViewCredit.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    private void ShowStep3()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        ButtonStep1.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep2.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep3.CssClass = "btn-info btn-circle btn-lg";
        ButtonStep4.CssClass = "btn-danger btn-circle btn-lg";
        ButtonStep5.CssClass = "btn-danger btn-circle btn-lg";
        //ButtonStep6.CssClass = "btn-danger btn-circle btn-lg";

        ButtonStep1.Enabled = false;
        ButtonStep2.Enabled = true;
        ButtonStep3.Enabled = false;
        ButtonStep4.Enabled = false;
        ButtonStep5.Enabled = false;
        //  ButtonStep6.Enabled = false;


        pnlStep1.Visible = false;
        pnlStep2.Visible = false;
        pnlStep3.Visible = true;
        pnlStep4.Visible = false;
        pnlStep5.Visible = false;

        ButtonStep1.Enabled = false;
        ButtonStep2.Enabled = true;
        ButtonStep3.Enabled = false;
        ButtonStep4.Enabled = false;
        ButtonStep5.Enabled = false;
        try
        {
            dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);

            string replace_sale = requ.Replace_Sales;
            string User_ID = hdfUser_ID.Value;
            List<dbo_CustomerClass> customers = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, User_ID, string.Empty, string.Empty);

            //ขายแทน
            if (!string.IsNullOrEmpty(replace_sale.Trim()))
            {
                List<dbo_CustomerClass> customers_1 = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, replace_sale, string.Empty, string.Empty);
                customers = customers.Union(customers_1).ToList();
            }

            customers.Insert(0, new dbo_CustomerClass() { Customer_ID = "-1", Full_Name = "==ระบุ==" });

            DropDownListCustomer.DataSource = customers;
            DropDownListCustomer.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    private void ShowStep4()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        ButtonStep1.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep2.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep3.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep4.CssClass = "btn-info btn-circle btn-lg";
        ButtonStep5.CssClass = "btn-danger btn-circle btn-lg";
        //ButtonStep6.CssClass = "btn-danger btn-circle btn-lg";

        ButtonStep1.Enabled = false;
        ButtonStep2.Enabled = false;
        ButtonStep3.Enabled = true;
        ButtonStep4.Enabled = false;
        ButtonStep5.Enabled = false;
        //  ButtonStep6.Enabled = false;

        pnlStep1.Visible = false;
        pnlStep2.Visible = false;
        pnlStep3.Visible = false;
        pnlStep4.Visible = true;
        pnlStep5.Visible = false;

        ButtonStep1.Enabled = false;
        ButtonStep2.Enabled = false;
        ButtonStep3.Enabled = true;
        ButtonStep4.Enabled = false;
        ButtonStep5.Enabled = false;

        try
        {
            List<dbo_SubsidyClass> list_subsidy = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
            GridViewSubsidy.ShowFooter = false;
            if (list_subsidy.Count == 0)
            {
                list_subsidy.Add(new dbo_SubsidyClass());

                GridViewSubsidy.DataSource = list_subsidy;
                GridViewSubsidy.DataBind();
                GridViewSubsidy.Rows[0].Visible = false;
            }
            else
            {
                GridViewSubsidy.DataSource = list_subsidy;
                GridViewSubsidy.DataBind();
            }

            List<dbo_DeductClass> list_deduct = dbo_DeductDataClass.Search(txtClearing_No.Text);
            GridViewDeduct.ShowFooter = false;
            if (list_deduct.Count == 0)
            {
                list_deduct.Add(new dbo_DeductClass());

                GridViewDeduct.DataSource = list_deduct;
                GridViewDeduct.DataBind();
                GridViewDeduct.Rows[0].Visible = false;
            }
            else
            {
                GridViewDeduct.DataSource = list_deduct;
                GridViewDeduct.DataBind();
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void ShowStep5()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        ButtonStep1.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep2.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep3.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep4.CssClass = "btn-success btn-circle btn-lg";
        ButtonStep5.CssClass = "btn-info btn-circle btn-lg";
        // ButtonStep6.CssClass = "btn-danger btn-circle btn-lg";

        ButtonStep1.Enabled = false;
        ButtonStep2.Enabled = false;
        ButtonStep3.Enabled = false;
        ButtonStep4.Enabled = true;
        ButtonStep5.Enabled = false;
        //ButtonStep6.Enabled = false;

        pnlGrid.Visible = false;
        pnlStep1.Visible = false;
        pnlStep2.Visible = false;
        pnlStep3.Visible = false;
        pnlStep4.Visible = false;
        pnlStep5.Visible = true;

        ButtonStep1.Enabled = false;
        ButtonStep2.Enabled = false;
        ButtonStep3.Enabled = false;
        ButtonStep4.Enabled = true;
        ButtonStep5.Enabled = false;

        pnlResign.Visible = false;
    }

    private void GetDetailsDataToForm(string id, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        ShowStep1();

        try
        {

            if (Mode == "View")
            {
                btnSaveMode.Value = "แก้ไข";
                ButtonSave.Text = "แก้ไข";
            }
            else if (Mode == "Edit")
            {
                ButtonSave.Text = "บันทึก";
                btnSaveMode.Value = "แก้ไข";
            }
            else if (string.IsNullOrEmpty(Mode))
            {
                ButtonSave.Text = "บันทึก";
                btnSaveMode.Value = "บันทึก";
            }

            bool enable = Mode != "View";

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(hdfUser_ID.Value);

            if (!string.IsNullOrEmpty(id))
            {
                dbo_DepositClass depo = dbo_DepositDataClass.Select_Record(id);
                txtClearing_No.Text = depo.Clearing_No;
                dbo_RequisitionClearingClass clea = dbo_RequisitionClearingDataClass.Select_Record(txtClearing_No.Text);
                // dbo
                dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(id);

                txtDiscount.Text = clearing.Discount.HasValue ? clearing.Discount.Value.ToString("#,##0.##") : "0";
                logger.Debug("txtDiscount.Text " + txtDiscount.Text);
                txtActualPayment.Text = clearing.Actual_Payment.HasValue ? clearing.Actual_Payment.Value.ToString("#,##0.##") : "0";
                logger.Debug("txtActualPayment.Text " + txtActualPayment.Text);
                txtSP_Name.Text = clea.SP_Name;
                txtRequisition_Date.Text = clea.Requisition_Date.Value.ToShortDateString();
                txtClearing_Date.Text = clea.Clearing_Date.Value.ToShortDateString();

                txtClearing_No.Text = clearing.Clearing_No;

                TextboxTotal.Text = depo.Net_Sales_Amount.Value.ToString("#,##0.#0");
                TextboxTotalAmount.Text = depo.Net_Sales_Qty.Value.ToString("#,##0");

                if (hdfRequisition_No.Value == string.Empty)
                {
                    List<dbo_RequisitionClearingClass> item_clear = dbo_RequisitionClearingDataClass.Search(clea.Clearing_No, string.Empty, string.Empty, null, null, null, null, user_class.CV_CODE);

                    foreach (dbo_RequisitionClearingClass rq_cl in item_clear)
                    {
                        hdfRequisition_No.Value += rq_cl.Requisition_No;
                    }
                }

                if (hdfRequisition_No.Value.Length > 16)
                {
                    dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));

                    dbo_RequisitionClass requi1 = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));

                    txtRequisition_No.Text = requi.Requisition_No + "," + requi1.Requisition_No;
                    txtRequisition_Date.Text = requi.Requisition_Date.Value.ToShortDateString() + "," + requi1.Requisition_Date.Value.ToShortDateString();
                }
                else
                {
                    dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value);
                    txtRequisition_No.Text = requi.Requisition_No;
                    txtRequisition_Date.Text = requi.Requisition_Date.Value.ToShortDateString();
                }

                show_grid(hdfUser_ID.Value, id, DateTime.Now);
            }
            else
            {
                txtClearing_No.Text = GenerateID.Clearing_No(user_class.CV_CODE);
                txtSP_Name.Text = user_class.FullName;

                if (hdfRequisition_No.Value.Length > 16)
                {
                    dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));

                    dbo_RequisitionClass requi1 = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));

                    txtRequisition_No.Text = requi.Requisition_No + "," + requi1.Requisition_No;
                    txtRequisition_Date.Text = requi.Requisition_Date.Value.ToShortDateString() + "," + requi1.Requisition_Date.Value.ToShortDateString();
                }
                else
                {
                    dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value);
                    txtRequisition_No.Text = requi.Requisition_No;
                    txtRequisition_Date.Text = requi.Requisition_Date.Value.ToShortDateString();
                }

                txtClearing_Date.Text = DateTime.Now.ToShortDateString();
                TextboxTotalAmount.Text = "0";
                TextboxTotal.Text = "0";
                show_grid(hdfUser_ID.Value, string.Empty, DateTime.Now);

                txt_Deposit_Qty_TextChanged(null, null);


                if (hdfPriceExpire.Value != string.Empty)
                {
                    Show("ราคาของสินค้า " + hdfPriceExpire.Value + " หมดอายุ กรุณาเคลียร์เงินให้ครบทุกใบเบิก");
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }



    private void GetPaymentDetailResign()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        ShowStep5();
        try
        {

            if (hdfResign.Value == "1")
            {
                txtCredit_Amount.Text = "0";
                txtSP_Cash.Text = "0";
                txtCashPaymentAmount.Text = "0";
                txtChequePaymentAmount.Text = "0";
                txtTransferPaymentAmount.Text = "0";
                txtTodayPoints.Text = "0";
                txtTodayCommission.Text = "0";

                decimal? Discount = decimal.Parse(txtDiscount.Text);
                decimal? Actual_Payment = decimal.Parse(txtActualPayment.Text);

                decimal? depo_Net_Sales_Amount = 0;
                int? depo_Net_Sales_Qty = 0;
                int? sales_point = 0;
                decimal? comm_Amount = 0;

                int calQty = 0;

                foreach (GridViewRow currentRow in GridViewClearing_1.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;
                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }

                foreach (GridViewRow currentRow in GridViewClearing_2.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;

                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }

                foreach (GridViewRow currentRow in GridViewClearing_3.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;

                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }

                foreach (GridViewRow currentRow in GridViewClearing_4.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;

                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }

                foreach (GridViewRow currentRow in GridViewClearing_5.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;

                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(hdfUser_ID.Value);

                txtSubTotal.Text = depo_Net_Sales_Amount.Value.ToString("#,##0.#0");

                decimal? NetTotal = depo_Net_Sales_Amount - Discount;
                decimal? DebtPayment = (Actual_Payment - NetTotal).Value > 0 ? (Actual_Payment - NetTotal) : 0;

                txtDebtPayment.Text = DebtPayment.Value.ToString("#,##0.#0");
                txtNet_Sales_Qty.Text = depo_Net_Sales_Qty.Value.ToString();
                txtNet_Sales_Amount.Text = depo_Net_Sales_Amount.Value.ToString("#,##0.#0");

                decimal? calBalance = depo_Net_Sales_Amount - Discount - Actual_Payment;
                txtBalanceOutstanding.Text = (calBalance > 0 ? calBalance.Value.ToString("#,##0.#0") : "0.00");

                dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

                List<dbo_DebtClass> search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date).ToList();

                decimal? calSumDebt = search_dep.Sum(f => f.Balance_Outstanding_Amount);

                lblDebtBalance.Text = string.Format("หนี้เงินสดคงค้าง (หนี้ค้างทั้งหมด {0} บาท)", calSumDebt.Value.ToString("#,##0.#0"));

                txtNetTotal.Text = (depo_Net_Sales_Amount - Discount).Value.ToString("#,##0.#0");
                txtTotal.Text = (depo_Net_Sales_Amount).Value.ToString("#,##0.#0");
                //txtDebtBalance.Text = clearing.Debt_Balance.Value.ToString("#,##0.#0");
                txtDebtBalance.Text = (calSumDebt - DebtPayment).Value.ToString("#,##0.#0");
                txtSP_Cash.Text = depo_Net_Sales_Amount.Value.ToString("#,##0.#0");
                txtTodayPoints.Text = sales_point.Value.ToString("#,##0");
                txtTodayCommission.Text = comm_Amount.Value.ToString("#,##0.#0");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    private void GetPaymentDetailResign_01()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        ShowStep5();
        try
        {

            if (hdfResign.Value == "1")
            {
                txtCredit_Amount.Text = "0";
                txtSP_Cash.Text = "0";
                txtCashPaymentAmount.Text = "0";
                txtChequePaymentAmount.Text = "0";
                txtTransferPaymentAmount.Text = "0";
                txtTodayPoints.Text = "0";
                txtTodayCommission.Text = "0";

                decimal? Discount = decimal.Parse(txtDiscount.Text);
                decimal? Actual_Payment = decimal.Parse(txtActualPayment.Text);

                decimal? depo_Net_Sales_Amount = 0;
                int? depo_Net_Sales_Qty = 0;
                int? sales_point = 0;
                decimal? comm_Amount = 0;

                int calQty = 0;

                foreach (GridViewRow currentRow in GridViewClearing_1.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;
                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }

                foreach (GridViewRow currentRow in GridViewClearing_2.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;

                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }

                foreach (GridViewRow currentRow in GridViewClearing_3.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;

                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }

                foreach (GridViewRow currentRow in GridViewClearing_4.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;

                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }

                foreach (GridViewRow currentRow in GridViewClearing_5.Rows)
                {
                    Label lbl_Unit_of_item = (Label)currentRow.FindControl("lbl_Unit_of_item");

                    if (lbl_Unit_of_item.Text != string.Empty)
                    {
                        HiddenField hdfOldDeposit_Qty = (HiddenField)currentRow.FindControl("hdfOldDeposit_Qty");
                        HiddenField hdfOldReturn_Qty = (HiddenField)currentRow.FindControl("hdfOldReturn_Qty");
                        TextBox txt_Deposit_QtyReturn = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");

                        Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                        Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");

                        calQty = int.Parse(hdfOldDeposit_Qty.Value) - (int.Parse(txt_Deposit_QtyReturn.Text) - int.Parse(hdfOldReturn_Qty.Value));

                        logger.Debug("hdfOldDeposit_Qty.Value " + hdfOldDeposit_Qty.Value);
                        logger.Debug("txt_Deposit_QtyReturn.Text " + txt_Deposit_QtyReturn.Text);
                        logger.Debug("hdfOldReturn_Qty.Value " + hdfOldReturn_Qty.Value);

                        logger.Debug("calQty " + calQty);

                        depo_Net_Sales_Amount += (calQty * decimal.Parse(lbl_PricePerUnit.Text));
                        depo_Net_Sales_Qty += calQty;

                        sales_point += (calQty * int.Parse(Label_Point.Text));
                        comm_Amount += (calQty * (decimal.Parse(lbl_PricePerUnit.Text) - decimal.Parse(Label_SP_Price.Text)));
                    }

                }
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(hdfUser_ID.Value);

                txtSubTotal.Text = depo_Net_Sales_Amount.Value.ToString("#,##0.#0");

                decimal? NetTotal = depo_Net_Sales_Amount - Discount;
                decimal? DebtPayment = (Actual_Payment - NetTotal).Value > 0 ? (Actual_Payment - NetTotal) : 0;

                txtDebtPayment.Text = DebtPayment.Value.ToString("#,##0.#0");
                txtNet_Sales_Qty.Text = depo_Net_Sales_Qty.Value.ToString();
                txtNet_Sales_Amount.Text = depo_Net_Sales_Amount.Value.ToString("#,##0.#0");

                decimal? calBalance = depo_Net_Sales_Amount - Discount - Actual_Payment;
                txtBalanceOutstanding.Text = (calBalance > 0 ? calBalance.Value.ToString("#,##0.#0") : "0.00");

                dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

                List<dbo_DebtClass> search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date).ToList();

                decimal? calSumDebt = search_dep.Sum(f => f.Balance_Outstanding_Amount);

                lblDebtBalance.Text = string.Format("หนี้เงินสดคงค้าง (หนี้ค้างทั้งหมด {0} บาท)", calSumDebt.Value.ToString("#,##0.#0"));

                txtNetTotal.Text = (depo_Net_Sales_Amount - Discount).Value.ToString("#,##0.#0");
                txtTotal.Text = (depo_Net_Sales_Amount).Value.ToString("#,##0.#0");
                //txtDebtBalance.Text = clearing.Debt_Balance.Value.ToString("#,##0.#0");
                txtDebtBalance.Text = (calSumDebt - DebtPayment).Value.ToString("#,##0.#0");
                txtSP_Cash.Text = depo_Net_Sales_Amount.Value.ToString("#,##0.#0");
                txtTodayPoints.Text = sales_point.Value.ToString("#,##0");
                txtTodayCommission.Text = comm_Amount.Value.ToString("#,##0.#0");


                if (Actual_Payment > NetTotal + calSumDebt)
                {
                    Show("ไม่สามารถระบุยอดเงินเกินจำนวนหนี้ได้ ยอดหนี้ทั้งหมดของคุณคือ " + (NetTotal + calSumDebt).Value.ToString("#,##0.#0") + " บาท กรุณากดปุ่ม OK เพื่อเคลียยอดค้างชำระทั้งหมด");
                    txtActualPayment.Text = (NetTotal + calSumDebt).ToString();
                    GetPaymentDetailResign();

                }


            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void GetPaymentDetail()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        ShowStep5();

        try
        {

            dbo_DepositClass depo = dbo_DepositDataClass.Select_Record(txtClearing_No.Text);
            decimal? Net_Sales_Amount = depo.Net_Sales_Amount;
            decimal? Discount = decimal.Parse(txtDiscount.Text);
            decimal? Actual_Payment = decimal.Parse(txtActualPayment.Text);


            //textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");



            List<dbo_CreditClass> credits = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value);
            decimal sum_CashPaymentAmount = 0;
            decimal sum_ChequePaymentAmount = 0;
            decimal sum_TransferPaymentAmount = 0;

            sum_CashPaymentAmount = dbo_CreditPaymentDataClass.Search(string.Empty, txtClearing_No.Text).Where(g => g.Payment_Method == "เงินสด").Sum(f => f.Payment_Amount).Value;
            sum_ChequePaymentAmount = dbo_CreditPaymentDataClass.Search(string.Empty, txtClearing_No.Text).Where(g => g.Payment_Method == "เช็ค").Sum(f => f.Payment_Amount).Value;
            sum_TransferPaymentAmount = dbo_CreditPaymentDataClass.Search(string.Empty, txtClearing_No.Text).Where(g => g.Payment_Method == "โอน").Sum(f => f.Payment_Amount).Value;

            List<dbo_SubsidyClass> subsidys = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
            List<dbo_DeductClass> deduct = dbo_DeductDataClass.Search(txtClearing_No.Text);

            decimal? sum_cash = credits.Sum(f => f.Credit_Amount).HasValue ? credits.Sum(f => f.Credit_Amount) : 0;
            decimal? SP_Cash = Net_Sales_Amount - sum_cash;
            decimal? SubTotal = SP_Cash + sum_CashPaymentAmount;
            decimal? sum_Subsidy = subsidys.Sum(f => f.Subsidy_Amount).Value;
            decimal? sum_Deduct = deduct.Sum(f => f.Deduct_Amount).Value;
            decimal? Total = SP_Cash + sum_CashPaymentAmount - sum_Subsidy + sum_Deduct;
            decimal? NetTotal = Total - Discount;


            txtCredit_Amount.Text = sum_cash.Value.ToString("#,##0.#0");
            txtSP_Cash.Text = SP_Cash.Value.ToString("#,##0.#0");
            txtNet_Sales_Qty.Text = depo.Net_Sales_Qty.Value.ToString();
            txtNet_Sales_Amount.Text = Net_Sales_Amount.Value.ToString("#,##0.#0");
            txtCashPaymentAmount.Text = sum_CashPaymentAmount.ToString("#,##0.#0");
            txtChequePaymentAmount.Text = sum_ChequePaymentAmount.ToString("#,##0.#0");
            txtTransferPaymentAmount.Text = sum_TransferPaymentAmount.ToString("#,##0.#0");
            txtSubTotal.Text = (SP_Cash + sum_CashPaymentAmount).Value.ToString("#,##0.#0");
            txtTotal.Text = (SP_Cash + sum_CashPaymentAmount - sum_Subsidy + sum_Deduct).Value.ToString("#,##0.#0");
            txtNetTotal.Text = (Total - Discount).Value.ToString("#,##0.#0");
            grdSubsidy.DataSource = subsidys;
            grdSubsidy.DataBind();
            grdDeduct.DataSource = deduct;
            grdDeduct.DataBind();
            List<dbo_DebtClass> search_dep = new List<dbo_DebtClass>();


            logger.Debug("hdfRequisition_No.Value " + hdfRequisition_No.Value);

            string empty_requ = string.Empty;
            foreach (GridViewRow row in GridViewClearing.Rows)
            {
                LinkButton Label_Status = (LinkButton)row.FindControl("lnkBClearing_No");
                Label lnkB_Requisition_No = (Label)row.FindControl("lnkB_Requisition_No");
                Label lblUser_ID = (Label)row.FindControl("lblUser_ID");

                if (string.IsNullOrEmpty(Label_Status.Text) && lblUser_ID.Text == hdfUser_ID.Value)
                {
                    empty_requ = lnkB_Requisition_No.Text;
                }

            }


            if (hdfRequisition_No.Value == string.Empty)
            {
                search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date).ToList();
            }
            else if (hdfRequisition_No.Value.Length > 16)
            {

                search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date)
              .Where(f => f.Requisition_No != hdfRequisition_No.Value.Substring(0, 16) && f.Requisition_No != hdfRequisition_No.Value.Substring(16, 16)).ToList();

            }
            else
            {
                dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));
                // dbo_RequisitionClass requi1 = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));
                search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date)
               .Where(f => f.Requisition_No != hdfRequisition_No.Value && f.Requisition_No != empty_requ).ToList();

            }

            dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);
            txtclearing_Status.Value = clearing.Status;

            //string script = string.Format("alert('{0}');", hdfClearing_Status_1.Value);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
            decimal? calSumDebt = search_dep.Sum(f => f.Balance_Outstanding_Amount);
            hdfcalSumDebt.Value = calSumDebt.Value.ToString();
            decimal? calSumDebt_01 = search_dep.Sum(f => f.Balance_Outstanding_Amount);
            hdfsearch_dep_count.Value = search_dep.Count.ToString();
            //hdfclearingDebt_Balance.Value= clearing.Debt_Balance.Value.ToString();

            if (clearing.Status == "2")
            {
                if (clearing.Debt_Total == null)
                {
                    clearing.Debt_Total = 0;
                }
                lblDebtBalance.Text = string.Format("หนี้เงินสดคงค้าง (หนี้ค้างทั้งหมด {0} บาท)", clearing.Debt_Total.Value.ToString("#,##0.#0"));//string.Format("หนี้เงินสดคงค้าง (หนี้ค้างทั้งหมด {0} บาท)", calSumDebt.Value.ToString("#,##0.#0"));

                if (clearing.Debt_Balance == null)
                {
                    clearing.Debt_Balance = 0;
                }

            }
            else if (clearing.Status == "1")
            {
                lblDebtBalance.Text = string.Format("หนี้เงินสดคงค้าง (หนี้ค้างทั้งหมด {0} บาท)", calSumDebt.Value.ToString("#,##0.#0"));

            }

            decimal? DebtPayment = (Actual_Payment - NetTotal).Value > 0 ? (Actual_Payment - NetTotal) : 0;
            txtDebtPayment.Text = DebtPayment.Value.ToString("#,##0.#0");
            decimal? calBalance = Total - Discount - Actual_Payment;
            txtBalanceOutstanding.Text = (calBalance > 0 ? calBalance.Value.ToString("#,##0.#0") : "0.00");
            /*
            decimal? DebtBalance = (DebtPayment > 0 ?
                search_dep[search_dep.Count - 1].Balance_Outstanding_Amount - DebtPayment :
                search_dep[search_dep.Count - 1].Balance_Outstanding_Amount);
            */

            if (clearing.Status != "2")
            {
                if (search_dep.Count > 0)
                {
                    decimal? DebtBalance = (DebtPayment > 0 ?
                       0 :
                       search_dep[search_dep.Count - 1].Balance_Outstanding_Amount);

                    txtDebtBalance.Text = (calSumDebt - DebtPayment).Value.ToString("#,##0.#0");

                    // txtDebtBalance.Text = DebtPayment > 0 ? calSumDebt - DebtPayment


                    //(calSumDebt - search_dep[search_dep.Count - 1].Balance_Outstanding_Amount - DebtPayment).Value.ToString("#,##0.##")
                    //:
                    //(calSumDebt - search_dep[search_dep.Count - 1].Balance_Outstanding_Amount).Value.ToString("#,##0.##"); 

                }
                else
                {
                    txtDebtBalance.Text = "0";
                }
            }
            else
            {
                txtDebtBalance.Text = clearing.Debt_Balance.Value.ToString("#,##0.#0");
            }


            txtTodayPoints.Text = depo.Tota_Point.ToString();
            txtTodayCommission.Text = depo.Total_Commission.Value.ToString("#,##0.#0");

            if (Actual_Payment > NetTotal + calSumDebt_01)
            {
                //Show("ไม่สามารถระบุยอดเงินเกินจำนวนหนี้ได้ ยอดหนี้ทั้งหมดของคุณคือ " + (NetTotal + calSumDebt_01).Value.ToString("#,##0.#0") + " บาท กรุณากดปุ่ม OK เพื่อเคลียยอดค้างชำระทั้งหมด");
                txtActualPayment.Text = (NetTotal + calSumDebt_01).ToString();
                GetPaymentDetail();

            }

            // txtDiscount.Attributes.Add("onblur", "javascript:return UpdateField(" + txtTotal.ClientID + "," + txtDiscount.ClientID + "," + txtNetTotal.ClientID + ");");


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    private void GetPaymentDetail_01()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        ShowStep5();

        try
        {

            dbo_DepositClass depo = dbo_DepositDataClass.Select_Record(txtClearing_No.Text);
            decimal? Net_Sales_Amount = depo.Net_Sales_Amount;
            decimal? Discount = decimal.Parse(txtDiscount.Text);
            decimal? Actual_Payment = decimal.Parse(txtActualPayment.Text);


            //textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");



            List<dbo_CreditClass> credits = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value);
            decimal sum_CashPaymentAmount = 0;
            decimal sum_ChequePaymentAmount = 0;
            decimal sum_TransferPaymentAmount = 0;

            sum_CashPaymentAmount = dbo_CreditPaymentDataClass.Search(string.Empty, txtClearing_No.Text).Where(g => g.Payment_Method == "เงินสด").Sum(f => f.Payment_Amount).Value;
            sum_ChequePaymentAmount = dbo_CreditPaymentDataClass.Search(string.Empty, txtClearing_No.Text).Where(g => g.Payment_Method == "เช็ค").Sum(f => f.Payment_Amount).Value;
            sum_TransferPaymentAmount = dbo_CreditPaymentDataClass.Search(string.Empty, txtClearing_No.Text).Where(g => g.Payment_Method == "โอน").Sum(f => f.Payment_Amount).Value;

            List<dbo_SubsidyClass> subsidys = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
            List<dbo_DeductClass> deduct = dbo_DeductDataClass.Search(txtClearing_No.Text);

            decimal? sum_cash = credits.Sum(f => f.Credit_Amount).HasValue ? credits.Sum(f => f.Credit_Amount) : 0;
            decimal? SP_Cash = Net_Sales_Amount - sum_cash;
            decimal? SubTotal = SP_Cash + sum_CashPaymentAmount;
            decimal? sum_Subsidy = subsidys.Sum(f => f.Subsidy_Amount).Value;
            decimal? sum_Deduct = deduct.Sum(f => f.Deduct_Amount).Value;
            decimal? Total = SP_Cash + sum_CashPaymentAmount - sum_Subsidy + sum_Deduct;
            decimal? NetTotal = Total - Discount;


            txtCredit_Amount.Text = sum_cash.Value.ToString("#,##0.#0");
            txtSP_Cash.Text = SP_Cash.Value.ToString("#,##0.#0");
            txtNet_Sales_Qty.Text = depo.Net_Sales_Qty.Value.ToString();
            txtNet_Sales_Amount.Text = Net_Sales_Amount.Value.ToString("#,##0.#0");
            txtCashPaymentAmount.Text = sum_CashPaymentAmount.ToString("#,##0.#0");
            txtChequePaymentAmount.Text = sum_ChequePaymentAmount.ToString("#,##0.#0");
            txtTransferPaymentAmount.Text = sum_TransferPaymentAmount.ToString("#,##0.#0");
            txtSubTotal.Text = (SP_Cash + sum_CashPaymentAmount).Value.ToString("#,##0.#0");
            txtTotal.Text = (SP_Cash + sum_CashPaymentAmount - sum_Subsidy + sum_Deduct).Value.ToString("#,##0.#0");
            txtNetTotal.Text = (Total - Discount).Value.ToString("#,##0.#0");
            grdSubsidy.DataSource = subsidys;
            grdSubsidy.DataBind();
            grdDeduct.DataSource = deduct;
            grdDeduct.DataBind();
            List<dbo_DebtClass> search_dep = new List<dbo_DebtClass>();


            logger.Debug("hdfRequisition_No.Value " + hdfRequisition_No.Value);

            string empty_requ = string.Empty;
            foreach (GridViewRow row in GridViewClearing.Rows)
            {
                LinkButton Label_Status = (LinkButton)row.FindControl("lnkBClearing_No");
                Label lnkB_Requisition_No = (Label)row.FindControl("lnkB_Requisition_No");
                Label lblUser_ID = (Label)row.FindControl("lblUser_ID");

                if (string.IsNullOrEmpty(Label_Status.Text) && lblUser_ID.Text == hdfUser_ID.Value)
                {
                    empty_requ = lnkB_Requisition_No.Text;
                }

            }


            if (hdfRequisition_No.Value == string.Empty)
            {
                search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date).ToList();
            }
            else if (hdfRequisition_No.Value.Length > 16)
            {

                search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date)
              .Where(f => f.Requisition_No != hdfRequisition_No.Value.Substring(0, 16) && f.Requisition_No != hdfRequisition_No.Value.Substring(16, 16)).ToList();

            }
            else
            {
                dbo_RequisitionClass requi = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(0, 16));
                // dbo_RequisitionClass requi1 = dbo_RequisitionDataClass.Select_Record(hdfRequisition_No.Value.Substring(16, 16));
                search_dep = dbo_DebtDataClass.Search(hdfUser_ID.Value, string.Empty).OrderBy(f => f.Created_Date)
               .Where(f => f.Requisition_No != hdfRequisition_No.Value && f.Requisition_No != empty_requ).ToList();

            }

            dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);
            txtclearing_Status.Value = clearing.Status;

            //string script = string.Format("alert('{0}');", hdfClearing_Status_1.Value);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
            decimal? calSumDebt = search_dep.Sum(f => f.Balance_Outstanding_Amount);
            hdfcalSumDebt.Value = calSumDebt.Value.ToString();
            decimal? calSumDebt_01 = search_dep.Sum(f => f.Balance_Outstanding_Amount);
            hdfsearch_dep_count.Value = search_dep.Count.ToString();
            //hdfclearingDebt_Balance.Value= clearing.Debt_Balance.Value.ToString();

            if (clearing.Status == "2")
            {
                if (clearing.Debt_Total == null)
                {
                    clearing.Debt_Total = 0;
                }
                lblDebtBalance.Text = string.Format("หนี้เงินสดคงค้าง (หนี้ค้างทั้งหมด {0} บาท)", clearing.Debt_Total.Value.ToString("#,##0.#0"));//string.Format("หนี้เงินสดคงค้าง (หนี้ค้างทั้งหมด {0} บาท)", calSumDebt.Value.ToString("#,##0.#0"));

                if (clearing.Debt_Balance == null)
                {
                    clearing.Debt_Balance = 0;
                }

            }
            else if (clearing.Status == "1")
            {
                lblDebtBalance.Text = string.Format("หนี้เงินสดคงค้าง (หนี้ค้างทั้งหมด {0} บาท)", calSumDebt.Value.ToString("#,##0.#0"));

            }

            decimal? DebtPayment = (Actual_Payment - NetTotal).Value > 0 ? (Actual_Payment - NetTotal) : 0;
            txtDebtPayment.Text = DebtPayment.Value.ToString("#,##0.#0");
            decimal? calBalance = Total - Discount - Actual_Payment;
            txtBalanceOutstanding.Text = (calBalance > 0 ? calBalance.Value.ToString("#,##0.#0") : "0.00");
            /*
            decimal? DebtBalance = (DebtPayment > 0 ?
                search_dep[search_dep.Count - 1].Balance_Outstanding_Amount - DebtPayment :
                search_dep[search_dep.Count - 1].Balance_Outstanding_Amount);
            */

            if (clearing.Status != "2")
            {
                if (search_dep.Count > 0)
                {
                    decimal? DebtBalance = (DebtPayment > 0 ?
                       0 :
                       search_dep[search_dep.Count - 1].Balance_Outstanding_Amount);

                    txtDebtBalance.Text = (calSumDebt - DebtPayment).Value.ToString("#,##0.#0");

                    // txtDebtBalance.Text = DebtPayment > 0 ? calSumDebt - DebtPayment


                    //(calSumDebt - search_dep[search_dep.Count - 1].Balance_Outstanding_Amount - DebtPayment).Value.ToString("#,##0.##")
                    //:
                    //(calSumDebt - search_dep[search_dep.Count - 1].Balance_Outstanding_Amount).Value.ToString("#,##0.##"); 

                }
                else
                {
                    txtDebtBalance.Text = "0";
                }
            }
            else
            {
                txtDebtBalance.Text = clearing.Debt_Balance.Value.ToString("#,##0.#0");
            }


            txtTodayPoints.Text = depo.Tota_Point.ToString();
            txtTodayCommission.Text = depo.Total_Commission.Value.ToString("#,##0.#0");

            if (Actual_Payment > NetTotal + calSumDebt_01)
            {
                Show("ไม่สามารถระบุยอดเงินเกินจำนวนหนี้ได้ ยอดหนี้ทั้งหมดของคุณคือ " + (NetTotal + calSumDebt_01).Value.ToString("#,##0.#0") + " บาท กรุณากดปุ่ม OK เพื่อเคลียยอดค้างชำระทั้งหมด");
                txtActualPayment.Text = (NetTotal + calSumDebt_01).ToString();
                GetPaymentDetail();

            }

            // txtDiscount.Attributes.Add("onblur", "javascript:return UpdateField(" + txtTotal.ClientID + "," + txtDiscount.ClientID + "," + txtNetTotal.ClientID + ");");


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void Show(string message)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            logger.Info(message);

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

    private void InsertRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(hdfUser_ID.Value);

            string[] Requisition_No_arr = txtRequisition_No.Text.Split(',');

            dbo_DepositClass deposit = new dbo_DepositClass();
            List<dbo_DepositDetailClass> list_deposit = SetDepositDetailData(txtClearing_No.Text);

            deposit.Tota_Point = Int16.Parse(list_deposit.Sum(f => f.Point).ToString());
            deposit.Total_Commission = list_deposit.Sum(f => f.Commission);
            deposit.Clearing_No = txtClearing_No.Text;
            deposit.Net_Sales_Qty = short.Parse(TextboxTotalAmount.Text.Replace(",", string.Empty));
            deposit.Net_Sales_Amount = decimal.Parse(TextboxTotal.Text.Replace(",", string.Empty));

            dbo_DepositDataClass.Add(deposit);

            foreach (dbo_DepositDetailClass deposit_detail in list_deposit)
            {
                dbo_DepositDetailDataClass.Add(deposit_detail);
            }

            foreach (string Requisition_No in Requisition_No_arr)
            {
                dbo_RequisitionClearingClass reqcle = new dbo_RequisitionClearingClass();
                reqcle.Clearing_Date = DateTime.Now;
                reqcle.Clearing_No = txtClearing_No.Text;
                reqcle.Requisition_No = Requisition_No;
                reqcle.User_ID = hdfUser_ID.Value;

                dbo_RequisitionClearingDataClass.Add(reqcle);
            }

            dbo_ClearingClass clearing = new dbo_ClearingClass();
            clearing.Clearing_No = txtClearing_No.Text;
            clearing.Actual_Payment = 0;
            clearing.Discount = 0;
            clearing.Status = "1";

            dbo_ClearingDataClass.Add(clearing, HttpContext.Current.Request.Cookies["User_ID"].Value);

            btnSaveMode.Value = "แก้ไข";
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
            dbo_DepositClass deposit = dbo_DepositDataClass.Select_Record(txtClearing_No.Text);
            deposit.Net_Sales_Qty = short.Parse(TextboxTotalAmount.Text.Replace(",", string.Empty));
            deposit.Net_Sales_Amount = decimal.Parse(TextboxTotal.Text.Replace(",", string.Empty));
            List<dbo_DepositDetailClass> list_deposit = SetDepositDetailData(txtClearing_No.Text);
            deposit.Tota_Point = Int16.Parse(list_deposit.Sum(f => f.Point).ToString());
            deposit.Total_Commission = list_deposit.Sum(f => f.Commission);
            dbo_DepositDataClass.Update(deposit);


            dbo_DepositDetailDataClass.Delete(txtClearing_No.Text);


            foreach (dbo_DepositDetailClass deposit_detail in list_deposit)
            {
                dbo_DepositDetailDataClass.Add(deposit_detail);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private List<dbo_DepositDetailClass> SetDepositDetailData(string Clearing_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_DepositDetailClass> item_detail = new List<dbo_DepositDetailClass>();
        int index = 1;



        try
        {


            foreach (GridViewRow currentRow in GridViewClearing_1.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                Label lbl = (Label)currentRow.FindControl("Label_Net_Sales_Qty");


                if (!string.IsNullOrEmpty(lbl.Text) && lbl.Text.Trim() != "0")
                {

                    Label lbl_ProductID = (Label)currentRow.FindControl("Label_Product_ID");

                    dbo_DepositDetailClass deposit_detail = new dbo_DepositDetailClass();

                    deposit_detail.Deposit_Detail_ID =
                    deposit_detail.Product_ID = lbl_ProductID.Text;


                    Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");
                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");
                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                    Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");
                    Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                    Label Label_Deposit_Detail_ID = (Label)currentRow.FindControl("Label_Deposit_Detail_ID");
                    logger.Debug("lbl_Net_Sales_Qty_.Text " + lbl_Net_Sales_Qty_.Text + " , txt.Text " + txt.Text + " , txt_Deposit_QtyReturn_.Text " + txt_Deposit_QtyReturn_.Text);

                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) -
                        int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) -
                        int.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();

                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    logger.Debug("Label_Sales_Qty_.Text " + Label_Sales_Qty_.Text + " , lbl_PricePerUnit.Text " + lbl_PricePerUnit.Text);


                    deposit_detail.Deposit_Detail_ID = string.IsNullOrEmpty(Label_Deposit_Detail_ID.Text) ? Clearing_No + index.ToString("00") : Label_Deposit_Detail_ID.Text;

                    deposit_detail.Clearing_No = txtClearing_No.Text;
                    deposit_detail.Deposit_Qty = Int16.Parse(txt.Text == string.Empty ? "0" : txt.Text);
                    deposit_detail.Sales_Qty = Int16.Parse(Label_Sales_Qty_.Text);
                    deposit_detail.Sales_Amount = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2);
                    deposit_detail.Total_Qty = Int16.Parse(lbl_Net_Sales_Qty_.Text);
                    deposit_detail.Selling_Price = decimal.Round(decimal.Parse(lbl_PricePerUnit.Text), 2);
                    deposit_detail.Price = decimal.Round(decimal.Parse(Label_SP_Price.Text), 2);

                    deposit_detail.Commission = (deposit_detail.Selling_Price - deposit_detail.Price) * deposit_detail.Sales_Qty;

                    int? point = (int.Parse(string.IsNullOrEmpty(Label_Point.Text) ? "0" : Label_Point.Text) * deposit_detail.Sales_Qty);

                    logger.Debug("Label_Point.Text " + Label_Point.Text);
                    deposit_detail.Point = Int16.Parse(point.ToString());
                    deposit_detail.Return_Qty = Int16.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text);




                    index++;
                    item_detail.Add(deposit_detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {


            foreach (GridViewRow currentRow in GridViewClearing_2.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                Label lbl = (Label)currentRow.FindControl("Label_Net_Sales_Qty");


                if (!string.IsNullOrEmpty(lbl.Text) && lbl.Text.Trim() != "0")
                {

                    Label lbl_ProductID = (Label)currentRow.FindControl("Label_Product_ID");

                    dbo_DepositDetailClass deposit_detail = new dbo_DepositDetailClass();

                    deposit_detail.Deposit_Detail_ID =
                    deposit_detail.Product_ID = lbl_ProductID.Text;


                    Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");
                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");
                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                    Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");
                    Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                    Label Label_Deposit_Detail_ID = (Label)currentRow.FindControl("Label_Deposit_Detail_ID");

                    logger.Debug("Label_Sales_Qty_.Text " + Label_Sales_Qty_.Text + " , lbl_PricePerUnit.Text " + lbl_PricePerUnit.Text);


                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) -
                        int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) -
                        int.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();



                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    deposit_detail.Deposit_Detail_ID = string.IsNullOrEmpty(Label_Deposit_Detail_ID.Text) ? Clearing_No + index.ToString("00") : Label_Deposit_Detail_ID.Text;

                    deposit_detail.Clearing_No = txtClearing_No.Text;
                    deposit_detail.Deposit_Qty = Int16.Parse(txt.Text == string.Empty ? "0" : txt.Text);
                    deposit_detail.Sales_Qty = Int16.Parse(Label_Sales_Qty_.Text);
                    deposit_detail.Sales_Amount = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2);
                    deposit_detail.Total_Qty = Int16.Parse(lbl_Net_Sales_Qty_.Text);
                    deposit_detail.Selling_Price = decimal.Round(decimal.Parse(lbl_PricePerUnit.Text), 2);
                    deposit_detail.Price = decimal.Round(decimal.Parse(Label_SP_Price.Text), 2);

                    deposit_detail.Commission = (deposit_detail.Selling_Price - deposit_detail.Price) * deposit_detail.Sales_Qty;

                    int? point = (int.Parse(string.IsNullOrEmpty(Label_Point.Text) ? "0" : Label_Point.Text) * deposit_detail.Sales_Qty);

                    logger.Debug("Label_Point.Text " + Label_Point.Text);
                    deposit_detail.Point = Int16.Parse(point.ToString());
                    deposit_detail.Return_Qty = Int16.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text);




                    index++;
                    item_detail.Add(deposit_detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {


            foreach (GridViewRow currentRow in GridViewClearing_3.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                Label lbl = (Label)currentRow.FindControl("Label_Net_Sales_Qty");


                if (!string.IsNullOrEmpty(lbl.Text) && lbl.Text.Trim() != "0")
                {

                    Label lbl_ProductID = (Label)currentRow.FindControl("Label_Product_ID");

                    dbo_DepositDetailClass deposit_detail = new dbo_DepositDetailClass();

                    deposit_detail.Deposit_Detail_ID =
                    deposit_detail.Product_ID = lbl_ProductID.Text;


                    Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");
                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");
                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                    Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");
                    Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                    Label Label_Deposit_Detail_ID = (Label)currentRow.FindControl("Label_Deposit_Detail_ID");

                    logger.Debug("Label_Sales_Qty_.Text " + Label_Sales_Qty_.Text + " , lbl_PricePerUnit.Text " + lbl_PricePerUnit.Text);

                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) -
                        int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) -
                        int.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();


                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    deposit_detail.Deposit_Detail_ID = string.IsNullOrEmpty(Label_Deposit_Detail_ID.Text) ? Clearing_No + index.ToString("00") : Label_Deposit_Detail_ID.Text;

                    deposit_detail.Clearing_No = txtClearing_No.Text;
                    deposit_detail.Deposit_Qty = Int16.Parse(txt.Text == string.Empty ? "0" : txt.Text);
                    deposit_detail.Sales_Qty = Int16.Parse(Label_Sales_Qty_.Text);
                    deposit_detail.Sales_Amount = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2);
                    deposit_detail.Total_Qty = Int16.Parse(lbl_Net_Sales_Qty_.Text);
                    deposit_detail.Selling_Price = decimal.Round(decimal.Parse(lbl_PricePerUnit.Text), 2);
                    deposit_detail.Price = decimal.Round(decimal.Parse(Label_SP_Price.Text), 2);

                    deposit_detail.Commission = (deposit_detail.Selling_Price - deposit_detail.Price) * deposit_detail.Sales_Qty;

                    int? point = (int.Parse(string.IsNullOrEmpty(Label_Point.Text) ? "0" : Label_Point.Text) * deposit_detail.Sales_Qty);

                    logger.Debug("Label_Point.Text " + Label_Point.Text);
                    deposit_detail.Point = Int16.Parse(point.ToString());
                    deposit_detail.Return_Qty = Int16.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text);




                    index++;
                    item_detail.Add(deposit_detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {


            foreach (GridViewRow currentRow in GridViewClearing_4.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                Label lbl = (Label)currentRow.FindControl("Label_Net_Sales_Qty");


                if (!string.IsNullOrEmpty(lbl.Text) && lbl.Text.Trim() != "0")
                {

                    Label lbl_ProductID = (Label)currentRow.FindControl("Label_Product_ID");

                    dbo_DepositDetailClass deposit_detail = new dbo_DepositDetailClass();

                    deposit_detail.Deposit_Detail_ID =
                    deposit_detail.Product_ID = lbl_ProductID.Text;


                    Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");
                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");
                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                    Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");
                    Label Label_Point = (Label)currentRow.FindControl("Label_Point");
                    Label Label_Deposit_Detail_ID = (Label)currentRow.FindControl("Label_Deposit_Detail_ID");

                    logger.Debug("Label_Sales_Qty_.Text " + Label_Sales_Qty_.Text + " , lbl_PricePerUnit.Text " + lbl_PricePerUnit.Text);


                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) -
                       int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) -
                       int.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();



                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    deposit_detail.Deposit_Detail_ID = string.IsNullOrEmpty(Label_Deposit_Detail_ID.Text) ? Clearing_No + index.ToString("00") : Label_Deposit_Detail_ID.Text;

                    deposit_detail.Clearing_No = txtClearing_No.Text;
                    deposit_detail.Deposit_Qty = Int16.Parse(txt.Text == string.Empty ? "0" : txt.Text);
                    deposit_detail.Sales_Qty = Int16.Parse(Label_Sales_Qty_.Text);
                    deposit_detail.Sales_Amount = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2);
                    deposit_detail.Total_Qty = Int16.Parse(lbl_Net_Sales_Qty_.Text);
                    deposit_detail.Selling_Price = decimal.Round(decimal.Parse(lbl_PricePerUnit.Text), 2);
                    deposit_detail.Price = decimal.Round(decimal.Parse(Label_SP_Price.Text), 2);

                    deposit_detail.Commission = (deposit_detail.Selling_Price - deposit_detail.Price) * deposit_detail.Sales_Qty;

                    int? point = (int.Parse(string.IsNullOrEmpty(Label_Point.Text) ? "0" : Label_Point.Text) * deposit_detail.Sales_Qty);

                    logger.Debug("Label_Point.Text " + Label_Point.Text);
                    deposit_detail.Point = Int16.Parse(point.ToString());
                    deposit_detail.Return_Qty = Int16.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text);




                    index++;
                    item_detail.Add(deposit_detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {


            foreach (GridViewRow currentRow in GridViewClearing_5.Rows)
            {

                TextBox txt = (TextBox)currentRow.FindControl("txt_Deposit_Qty");

                Label lbl = (Label)currentRow.FindControl("Label_Net_Sales_Qty");


                if (!string.IsNullOrEmpty(lbl.Text) && lbl.Text.Trim() != "0")
                {

                    Label lbl_ProductID = (Label)currentRow.FindControl("Label_Product_ID");

                    dbo_DepositDetailClass deposit_detail = new dbo_DepositDetailClass();

                    deposit_detail.Deposit_Detail_ID =
                    deposit_detail.Product_ID = lbl_ProductID.Text;


                    Label lbl_Net_Sales_Qty_ = (Label)currentRow.FindControl("Label_Net_Sales_Qty");
                    TextBox Label_Sales_Qty_ = (TextBox)currentRow.FindControl("Label_Sales_Qty");
                    TextBox txt_Deposit_QtyReturn_ = (TextBox)currentRow.FindControl("txt_Deposit_QtyReturn");
                    TextBox Label_TotalSales_Qty_ = (TextBox)currentRow.FindControl("Label_TotalSales_Qty");
                    Label lbl_PricePerUnit = (Label)currentRow.FindControl("lbl_PricePerUnit");
                    Label Label_SP_Price = (Label)currentRow.FindControl("Label_SP_Price");
                    Label Label_Point = (Label)currentRow.FindControl("Label_Point");


                    Label Label_Deposit_Detail_ID = (Label)currentRow.FindControl("Label_Deposit_Detail_ID");

                    logger.Debug("Label_Sales_Qty_.Text " + Label_Sales_Qty_.Text + " , lbl_PricePerUnit.Text " + lbl_PricePerUnit.Text);

                    Label_Sales_Qty_.Text = (int.Parse(lbl_Net_Sales_Qty_.Text) -
                       int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text) -
                       int.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text)).ToString();


                    Label_TotalSales_Qty_.Text = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2).ToString();

                    deposit_detail.Deposit_Detail_ID = string.IsNullOrEmpty(Label_Deposit_Detail_ID.Text) ? Clearing_No + index.ToString("00") : Label_Deposit_Detail_ID.Text;

                    deposit_detail.Clearing_No = txtClearing_No.Text;
                    deposit_detail.Deposit_Qty = Int16.Parse(txt.Text == string.Empty ? "0" : txt.Text);
                    deposit_detail.Sales_Qty = Int16.Parse(Label_Sales_Qty_.Text);
                    deposit_detail.Sales_Amount = decimal.Round((decimal.Parse(Label_Sales_Qty_.Text) * decimal.Parse(lbl_PricePerUnit.Text)), 2);
                    deposit_detail.Total_Qty = Int16.Parse(lbl_Net_Sales_Qty_.Text);
                    deposit_detail.Selling_Price = decimal.Round(decimal.Parse(lbl_PricePerUnit.Text), 2);
                    deposit_detail.Price = decimal.Round(decimal.Parse(Label_SP_Price.Text), 2);

                    deposit_detail.Commission = (deposit_detail.Selling_Price - deposit_detail.Price) * deposit_detail.Sales_Qty;

                    int? point = (int.Parse(string.IsNullOrEmpty(Label_Point.Text) ? "0" : Label_Point.Text) * deposit_detail.Sales_Qty);

                    logger.Debug("Label_Point.Text " + Label_Point.Text);
                    deposit_detail.Point = Int16.Parse(point.ToString());
                    deposit_detail.Return_Qty = Int16.Parse(txt_Deposit_QtyReturn_.Text == string.Empty ? "0" : txt_Deposit_QtyReturn_.Text);

                    index++;
                    item_detail.Add(deposit_detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        return item_detail;
    }

    private void PaymentSearchSubmit()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string Customer_ID_ = string.Empty;
            string status = string.Empty;
            if (DropDownListCustomer.SelectedIndex > 0)
            {
                Customer_ID_ = DropDownListCustomer.SelectedValue;
            }
            DateTime? Credit_Date = (string.IsNullOrEmpty(txtSearchCreditPaymentDate.Text) ? (DateTime?)null : DateTime.Parse(txtSearchCreditPaymentDate.Text));
            if (ddlSearchCreditPaymentStatus.SelectedIndex > 0)
            {
                status = ddlSearchCreditPaymentStatus.SelectedValue;
            }

            List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, hdfUser_ID.Value);

            dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
            string replace_sale = requ.Replace_Sales;

            //ขายแทน
            if (!string.IsNullOrEmpty(replace_sale.Trim()))
            {
                List<dbo_CreditClass> listofitem_1 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, replace_sale);
                listofitem = listofitem.Union(listofitem_1).ToList();
            }

            if (listofitem.Count > 0)
            {
                GridViewCreditPayment.DataSource = listofitem;
                GridViewCreditPayment.DataBind();

                GridViewCreditPayment.Visible = true;
                pnlNoCreditPayment.Visible = false;
            }
            else
            {
                GridViewCreditPayment.Visible = false;
                pnlNoCreditPayment.Visible = true;
            }

        }
        catch (Exception)
        {

        }
    }
    #endregion

    #region GridView Events
    protected void GridViewCredit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        GridViewCredit.EditIndex = -1;

        List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value);
        GridViewCredit.ShowFooter = false;

        dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
        string replace_sale = requ.Replace_Sales;

        //ขายแทน
        if (!string.IsNullOrEmpty(replace_sale.Trim()))
        {
            List<dbo_CreditClass> listofitem_1 = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, replace_sale); // Remove hdfUser_ID
            listofitem = listofitem.Union(listofitem_1).ToList();
        }


        GridViewCredit.DataSource = listofitem;
        GridViewCredit.DataBind();
        if (Session["Create_Credit"] != null)
        {

            Session.Remove("Create_Credit");
        }
        btnCreditPaymentNext.Text = "ถัดไป";
    }

    protected void GridViewCredit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (e.CommandName == "AddNew")
        {
            #region old
            //try
            //{
            //    dbo_CreditClass credit = new dbo_CreditClass();
            //    DropDownList ddl = (DropDownList)GridViewCredit.FooterRow.FindControl("ddlFooterCustomerName");
            //    TextBox txtCredit_Amount = (TextBox)GridViewCredit.FooterRow.FindControl("txtFooterCredit_Amount");
            //    TextBox txtFooterCredit_Date = (TextBox)GridViewCredit.FooterRow.FindControl("txtFooterCredit_Date");


            //    if (ddl.SelectedIndex > 0)
            //    {
            //        if (string.IsNullOrEmpty(txtCredit_Amount.Text))
            //        {
            //            System.Threading.Thread.Sleep(1000);
            //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            //            Show("กรุณาระบุจำนวนเงิน");
            //        }
            //        else
            //        {
            //            credit.Customer_ID = ddl.SelectedValue;
            //            credit.Clearing_No = txtClearing_No.Text;
            //            credit.Credit_ID = GenerateID.Credit_ID(ViewState["CV_Code"].ToString());
            //            credit.Credit_Amount = Decimal.Parse(txtCredit_Amount.Text);
            //            credit.Credit_Date = (string.IsNullOrEmpty(txtFooterCredit_Date.Text) ? (DateTime?)null : DateTime.Parse(txtFooterCredit_Date.Text));
            //            credit.Status = "1";
            //            credit.Balance_Outstanding_Amount = credit.Credit_Amount;
            //            credit.Total_Payment_Amount = 0;
            //            dbo_CreditDataClass.Add(credit);
            //            #region
            //            /*
            //            dbo_DebtClass debt = new dbo_DebtClass();
            //            debt.Debt_ID = GenerateID.Debt_ID(string.Empty);
            //            debt.CV_Code = ViewState["CV_Code"].ToString();
            //            debt.Debt_Amount = credit.Credit_Amount;
            //            debt.Debt_Date = DateTime.Now;
            //            debt.Customer_ID = credit.Customer_ID;
            //            debt.Balance_Outstanding_Amount = credit.Credit_Amount;
            //            debt.Total_Payment_Amount = 0;
            //            debt.Requisition_No = string.Empty;

            //            dbo_DebtDataClass.Add(debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
            //            */
            //            #endregion
            //            GridViewCredit.ShowFooter = false;
            //            List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value); // Remove hdfUser_ID
            //            GridViewCredit.DataSource = listofitem;
            //            GridViewCredit.DataBind();

            //            Show("บันทึกสำเร็จ!");
            //            System.Threading.Thread.Sleep(1000);
            //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

            //        }
            //    }
            //    else
            //    {
            //        System.Threading.Thread.Sleep(1000);
            //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            //        Show("กรุณาระบุลูกค้า");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //}
            #endregion
            Add_Credit();
        }
    }

    protected void GridViewCredit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            Label lnk = (Label)GridViewCredit.Rows[e.RowIndex].FindControl("lblCredit_ID");

            dbo_CreditClass credit = dbo_CreditDataClass.Select_Record(lnk.Text);

            #region
            /*
            decimal? initail_amount = credit.Balance_Outstanding_Amount;

            dbo_DebtClass last_debt = null;

            List<dbo_DebtClass> listdebtcust = dbo_DebtDataClass.Search(string.Empty, credit.Customer_ID).OrderBy(f => f.Created_Date).ToList();


            foreach (dbo_DebtClass debt in listdebtcust.Where(f => f.Balance_Outstanding_Amount > 0))
            {
                decimal? cal_amount = initail_amount - debt.Debt_Amount;

                if (cal_amount > 0)
                {
                    debt.Total_Payment_Amount = debt.Debt_Amount;
                }
                else
                {
                    debt.Total_Payment_Amount += initail_amount;
                }

                debt.Balance_Outstanding_Amount -= debt.Total_Payment_Amount;
                initail_amount -= debt.Total_Payment_Amount;

                dbo_DebtDataClass.Update(debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
                last_debt = debt;

            }

            if (initail_amount > 0 && last_debt != null)
            {
                last_debt.Total_Payment_Amount += initail_amount;
                last_debt.Balance_Outstanding_Amount -= initail_amount;

                dbo_DebtDataClass.Update(last_debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
            }

            */
            #endregion
            dbo_CreditDataClass.Delete(lnk.Text);

            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            ShowStep2();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewCredit_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            GridViewCredit.EditIndex = e.NewEditIndex;
            List<dbo_CreditClass> credit = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value);
            dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
            string replace_sale = requ.Replace_Sales;


            if (!string.IsNullOrEmpty(replace_sale.Trim()))
            {
                List<dbo_CreditClass> listofitem_1 = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, replace_sale); // Remove hdfUser_ID
                credit = credit.Union(listofitem_1).ToList();
            }
            //List<dbo_CreditClass> credit = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, string.Empty);
            GridViewCredit.DataSource = credit;
            GridViewCredit.DataBind();

            string User_ID = hdfUser_ID.Value;

            //DropDownList ddl = (DropDownList)GridViewCredit.Rows[e.NewEditIndex].FindControl("ddlEditCustomerName");
            //Label lblItemCustomerName = (Label)GridViewCredit.Rows[e.NewEditIndex].FindControl("lblEditCustomerName");


            //List<dbo_CustomerClass> customers = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, User_ID, string.Empty, string.Empty);

            //customers.Insert(0, new dbo_CustomerClass() { Customer_ID = "-1", Full_Name = "==ระบุ==" });

            //ddl.DataSource = customers;
            //ddl.DataBind();
            //ddl.ClearSelection();


            //ddl.Items.FindByText(lblItemCustomerName.Text).Selected = true;
            // Session["Edit_Credit"] = "Edit_SS";

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCredit_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        #region old
        try
        {
            Label lblCredit_ID = (Label)GridViewCredit.Rows[e.RowIndex].FindControl("lblCredit_ID");
            dbo_CreditClass credit = dbo_CreditDataClass.Select_Record(lblCredit_ID.Text);

            logger.Debug("lblCredit_ID.Text " + lblCredit_ID.Text);

            TextBox txtEditCredit_Amount = (TextBox)GridViewCredit.Rows[e.RowIndex].FindControl("txtEditCredit_Amount");

            if (!string.IsNullOrEmpty(txtEditCredit_Amount.Text))
            {

                Label lblOldCredit_ID = (Label)GridViewCredit.Rows[e.RowIndex].FindControl("lblOldCredit_ID");
                credit.Credit_Amount = decimal.Parse(lblOldCredit_ID.Text);


                decimal? calCredit = decimal.Parse(txtEditCredit_Amount.Text) - decimal.Parse(lblOldCredit_ID.Text);


                credit.Credit_Amount += calCredit;
                credit.Balance_Outstanding_Amount += calCredit;

                dbo_CreditDataClass.Update(credit);

                Label lblCustomer_ID = (Label)GridViewCredit.Rows[e.RowIndex].FindControl("lblCustomer_ID");


                GridViewCredit.EditIndex = -1;
                GridViewCredit.ShowFooter = false;
                List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value);
                dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
                string replace_sale = requ.Replace_Sales;

                //ขายแทน
                if (!string.IsNullOrEmpty(replace_sale.Trim()))
                {
                    List<dbo_CreditClass> listofitem_1 = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, replace_sale); // Remove hdfUser_ID
                    listofitem = listofitem.Union(listofitem_1).ToList();
                }

                GridViewCredit.DataSource = listofitem;
                GridViewCredit.DataBind();

                Show("บันทึกสำเร็จ!");
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกจำนวนเงินที่ค้างชำระ");
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        #endregion

    }

    protected void GridViewCredit_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

        }
        catch (Exception)
        {

        }
        //List<dbo_CreditClass> listofitem = dbo_CreditDataClass.SelectAll();

        //if (listofitem.Count == 0)
        //{
        //    listofitem.Add(new dbo_CreditClass());
        //}
        //int i = 0;


        //if (GridViewCredit.Rows.Count > 0)
        //{
        //    foreach (dbo_CreditClass item in listofitem)
        //    {

        //        GridViewRow row = GridViewCredit.Rows[i];



        //        DropDownList ddl = (DropDownList)row.Cells[2].FindControl("ddlFooterCustomerName");


        //        if (ddl != null)
        //        {
        //            List<dbo_CustomerClass> customers = dbo_CustomerDataClass.SelectAll();

        //            ddl.DataSource = customers;
        //            ddl.DataBind();
        //        }

        //        i++;
        //    }

        //}





        //for (int i = 0; i < GridViewCredit.Rows.Count; i++)
        //{
        //    GridViewRow row = GridViewCredit.Rows[i];

        //    DropDownList ddl = (DropDownList)row.FindControl("ddlFooterCustomerName");

        //    List<dbo_CustomerClass> customers = dbo_CustomerDataClass.SelectAll();


        //    ddl.DataSource = customers;
        //    ddl.DataBind();

        //}

    }

    protected void GridViewCreditPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {


            if (e.CommandName == "ShowCredit")
            {
                int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());


                ViewState["RowIndexCreditPayment"] = RowIndex;


                Button currbtn = (Button)GridViewCreditPayment.Rows[RowIndex].FindControl("btnCustomerName");
                //  Button btn = (Button)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("btnCustomerName");

                if (currbtn.Text == "จ่ายหนี้เครดิต")
                {
                    string Customer_ID_ = string.Empty;
                    string status = string.Empty;
                    if (DropDownListCustomer.SelectedIndex > 0)
                    {
                        Customer_ID_ = DropDownListCustomer.SelectedValue;
                    }
                    DateTime? Credit_Date = (string.IsNullOrEmpty(txtSearchCreditPaymentDate.Text) ? (DateTime?)null : DateTime.Parse(txtSearchCreditPaymentDate.Text));
                    if (ddlSearchCreditPaymentStatus.SelectedIndex > 0)
                    {
                        status = ddlSearchCreditPaymentStatus.SelectedValue;
                    }

                    List<dbo_CreditClass> creditList1 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, hdfUser_ID.Value);
                    //List<dbo_CreditClass> creditList1 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, string.Empty);

                    dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
                    string replace_sale = requ.Replace_Sales;

                    //ขายแทน
                    if (!string.IsNullOrEmpty(replace_sale.Trim()))
                    {
                        List<dbo_CreditClass> creditList1_01 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, replace_sale);
                        creditList1 = creditList1.Union(creditList1_01).ToList();
                    }


                    creditList1.Insert(RowIndex + 1, new dbo_CreditClass() { });


                    GridViewCreditPayment.DataSource = creditList1;
                    GridViewCreditPayment.DataBind();


                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[0].ColumnSpan = 7;

                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[1].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[2].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[3].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[4].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[5].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[6].Visible = false;

                    Button btn = (Button)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("btnCustomerName");

                    Label _lblCustomerName = (Label)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("lblCustomerName");
                    Button newbutton1 = (Button)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("btnAddNewCreditPayment");
                    currbtn = (Button)GridViewCreditPayment.Rows[RowIndex].FindControl("btnCustomerName");
                    currbtn.Text = "ปิด";



                    //if (btn.Text == "จ่ายหนี้เครดิต")
                    //{
                    GridView gv = (GridView)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("GridViewCustomer");
                    _lblCustomerName.Visible = false;

                    Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[RowIndex].FindControl("lblCredit_ID");

                    List<dbo_CreditPaymentClass> creditList = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);

                    gv.DataSource = creditList;
                    gv.DataBind();

                    foreach (GridViewRow row in gv.Rows)
                    {
                        //Label lblUser_ID = (Label)row.FindControl("lblUser_ID");
                        LinkButton _lnkBEdit = (LinkButton)row.FindControl("lnkBEdit");
                        Label _lblPayment_Date = (Label)row.FindControl("lblPayment_Date");

                        DateTime temp_date = Convert.ToDateTime(_lblPayment_Date.Text);

                        string test = DateTime.Now.AddDays(-1).ToShortDateString();

                        if (temp_date < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                        {
                            //Show("ไม่สามารถระบุวันที่ชำระเงิน ย้อนหลังได้");
                            //Tempdate.Text = DateTime.Now.ToShortDateString();
                            //ซ่อนปุ่ม Edit
                            //_lnkBEdit.Visible = false;
                        }


                    }

                    btn.Text = "ตกลง";
                    btn.Visible = false;
                    newbutton1.Visible = true;

                    //  }


                }
                else if (currbtn.Text == "ปิด")
                {
                    string Customer_ID_ = string.Empty;
                    string status = string.Empty;
                    if (DropDownListCustomer.SelectedIndex > 0)
                    {
                        Customer_ID_ = DropDownListCustomer.SelectedValue;
                    }
                    DateTime? Credit_Date = (string.IsNullOrEmpty(txtSearchCreditPaymentDate.Text) ? (DateTime?)null : DateTime.Parse(txtSearchCreditPaymentDate.Text));
                    if (ddlSearchCreditPaymentStatus.SelectedIndex > 0)
                    {
                        status = ddlSearchCreditPaymentStatus.SelectedValue;
                    }

                    List<dbo_CreditClass> creditList1 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, hdfUser_ID.Value);
                    //List<dbo_CreditClass> creditList1 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, string.Empty);

                    dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
                    string replace_sale = requ.Replace_Sales;

                    //ขายแทน
                    if (!string.IsNullOrEmpty(replace_sale.Trim()))
                    {
                        List<dbo_CreditClass> creditList1_01 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, replace_sale);
                        creditList1 = creditList1.Union(creditList1_01).ToList();
                    }



                    GridViewCreditPayment.DataSource = creditList1;
                    GridViewCreditPayment.DataBind();

                    if (Session["Create_PaymentCredit"] != null)
                    {
                        Session.Remove("Create_PaymentCredit");
                    }
                    ButtonSubsidy.Text = "ถัดไป";

                    #region
                    //  currbtn.Text = "จ่ายหนี้เครดิต";
                }


            }

            /*

                if (currbtn.Text == "ตกลง")
                {

                    string Customer_ID_ = string.Empty;
                    string status = string.Empty;
                    if (DropDownListCustomer.SelectedIndex > 0)
                    {
                        Customer_ID_ = DropDownListCustomer.SelectedValue;
                    }
                    DateTime? Credit_Date = (string.IsNullOrEmpty(txtSearchCreditPaymentDate.Text) ? (DateTime?)null : DateTime.Parse(txtSearchCreditPaymentDate.Text));
                    if (ddlSearchCreditPaymentStatus.SelectedIndex > 0)
                    {
                        status = ddlSearchCreditPaymentStatus.SelectedValue;
                    }

                    List<dbo_CreditClass> creditList1 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, hdfUser_ID.Value);






                    GridViewCreditPayment.DataSource = creditList1;
                    GridViewCreditPayment.DataBind();



                }
                else
                {



                    string Customer_ID_ = string.Empty;
                    string status = string.Empty;
                    if (DropDownListCustomer.SelectedIndex > 0)
                    {
                        Customer_ID_ = DropDownListCustomer.SelectedValue;
                    }
                    DateTime? Credit_Date = (string.IsNullOrEmpty(txtSearchCreditPaymentDate.Text) ? (DateTime?)null : DateTime.Parse(txtSearchCreditPaymentDate.Text));
                    if (ddlSearchCreditPaymentStatus.SelectedIndex > 0)
                    {
                        status = ddlSearchCreditPaymentStatus.SelectedValue;
                    }

                    List<dbo_CreditClass> creditList1 = dbo_CreditDataClass.Search(string.Empty, Customer_ID_, Credit_Date, status, hdfUser_ID.Value);









                    creditList1.Insert(RowIndex + 1, new dbo_CreditClass() { });


                    GridViewCreditPayment.DataSource = creditList1;
                    GridViewCreditPayment.DataBind();


                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[0].ColumnSpan = 6;

                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[1].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[2].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[3].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[4].Visible = false;
                    GridViewCreditPayment.Rows[RowIndex + 1].Cells[5].Visible = false;
                    //GridViewCreditPayment.Rows[RowIndex + 1].Cells[6].Visible = false;
                    //GridViewCreditPayment.Rows[RowIndex + 1].Cells[7].Visible = false;

                    Button btn = (Button)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("btnCustomerName");
                    Label _lblCustomerName = (Label)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("lblCustomerName");
                    Button newbutton1 = (Button)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("btnAddNewCreditPayment");


                    if (btn.Text == "จ่ายหนี้เครดิต")
                    {
                        GridView gv = (GridView)GridViewCreditPayment.Rows[RowIndex + 1].FindControl("GridViewCustomer");
                        _lblCustomerName.Visible = false;

                        Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[RowIndex].FindControl("lblCredit_ID");

                        List<dbo_CreditPaymentClass> creditList = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);

                        gv.DataSource = creditList;
                        gv.DataBind();
                        btn.Text = "ตกลง";
                        newbutton1.Visible = true;

                    }
            
                }*/
            #endregion
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCreditPayment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewCreditPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridViewCreditPayment_RowEditing(object sender, GridViewEditEventArgs e)
    {



        //System.Threading.Thread.Sleep(1000);
        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewCreditPayment_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewCreditPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

        }

    }

    protected void txtFooterPaymentDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //GridView lolXX = (GridView)GridViewCreditPayment.Rows[0].Cells[0].FindControl("GridViewCustomer");

            TextBox Tempdate = (TextBox)sender;
            #region
            //GridView test = (GridView)GridViewCreditPayment.TemplateControl.FindControl("GridViewCustomer");
            //TextBox txtFooterCredit_Date = (TextBox)GridViewCredit.FooterRow.FindControl("txtFooterCredit_Date");
            // GridView lol = (GridView)GridViewCreditPayment.FindControl("GridViewCustomer");
            //TextBox txtFooterPaymentDate = (TextBox)test.FooterRow.FindControl("txtFooterPaymentDate");
            //TextBox txtFooterPaymentDate = (TextBox)GridViewCustomer.FooterRow.FindControl("txtFooterPaymentDate");
            //TextBox txtFooterPaymentDate =(TextBox)GridviewCus
            #endregion
            DateTime temp_date = Convert.ToDateTime(Tempdate.Text);

            if (temp_date < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                Show("ไม่สามารถระบุวันที่ชำระเงิน ย้อนหลังได้");
                Tempdate.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }




    protected void GridViewCustomer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int current_index = int.Parse(ViewState["RowIndexCreditPayment"].ToString());

            GridView gv = (GridView)GridViewCreditPayment.Rows[current_index + 1].FindControl("GridViewCustomer");
            Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[current_index].FindControl("lblCredit_ID");

            gv.EditIndex = -1;
            gv.ShowFooter = false;

            List<dbo_CreditPaymentClass> item_value = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);
            gv.DataSource = item_value;
            gv.DataBind();


            if (Session["Create_PaymentCredit"] != null)
            {
                Session.Remove("Create_PaymentCredit");
            }
            ButtonSubsidy.Text = "ถัดไป";
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


        #region
        /*
        int current_index = int.Parse(ViewState["current_index"].ToString());
        GridView gv = (GridView)GridViewCycle.Rows[current_index + 1].FindControl("grdNewValue");
        Label cycle_id = (Label)GridViewCycle.Rows[current_index].FindControl("lblOrder_Cycle_ID");
        gv.EditIndex = -1;
        gv.ShowFooter = false;


        List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle_id.Text);
        gv.DataSource = item_value;
        gv.DataBind();
         */
        #endregion
    }

    protected void GridViewCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {

            int current_index = int.Parse(ViewState["RowIndexCreditPayment"].ToString());

            GridView gv = (GridView)GridViewCreditPayment.Rows[current_index + 1].FindControl("GridViewCustomer");
            Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[current_index].FindControl("lblCredit_ID");

            LinkButton lnk = (LinkButton)gv.Rows[e.RowIndex].FindControl("lnkBDelete");
            Label amount = (Label)gv.Rows[e.RowIndex].FindControl("lblPayment_Amount");

            dbo_CreditClass credit = dbo_CreditDataClass.Select_Record(lblCredit_ID.Text);
            credit.Balance_Outstanding_Amount += decimal.Parse(amount.Text);
            credit.Total_Payment_Amount -= decimal.Parse(amount.Text);
            credit.Status = "1";

            dbo_CreditDataClass.Update(credit);

            dbo_CreditPaymentDataClass.Delete(lnk.CommandArgument);

            List<dbo_CreditPaymentClass> item_value = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);
            gv.DataSource = item_value;
            gv.DataBind();

            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);


            btnSearchCreditPayment_Click(sender, e);

            // ShowStep2();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewCustomer_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int current_index = int.Parse(ViewState["RowIndexCreditPayment"].ToString());
            GridView gv = (GridView)GridViewCreditPayment.Rows[current_index + 1].FindControl("GridViewCustomer");
            Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[current_index].FindControl("lblCredit_ID");

            gv.EditIndex = e.NewEditIndex;
            gv.ShowFooter = false;


            List<dbo_CreditPaymentClass> item_value = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);
            gv.DataSource = item_value;
            gv.DataBind();


            Dictionary<string, string> bank = dbo_ItemDataClass.GetDropDown("12");
            //  bank.Remove(string.Empty);
            DropDownList ddl = (DropDownList)gv.Rows[e.NewEditIndex].FindControl("ddlEditbank");
            ddl.DataSource = bank;
            ddl.DataBind();
            HiddenField hdfBank = (HiddenField)gv.Rows[e.NewEditIndex].FindControl("hdfEditbank");
            ddl.Items.FindByText(hdfBank.Value).Selected = true;


            DropDownList ddl_payment_method = (DropDownList)gv.Rows[e.NewEditIndex].FindControl("ddlEditpaymentmethod");
            HiddenField hdfEditPaymenyMethod = (HiddenField)gv.Rows[e.NewEditIndex].FindControl("hdfEditPaymenyMethod");

            ddl_payment_method.Items.FindByText(hdfEditPaymenyMethod.Value).Selected = true;

            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCustomer_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int current_index = int.Parse(ViewState["RowIndexCreditPayment"].ToString());
            GridView gv = (GridView)GridViewCreditPayment.Rows[current_index + 1].FindControl("GridViewCustomer");
            Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[current_index].FindControl("lblCredit_ID");

            TextBox _txtItemPayment_Date = (TextBox)gv.Rows[e.RowIndex].FindControl("txtItemPayment_Date");
            TextBox _txtPayment_Amount = (TextBox)gv.Rows[e.RowIndex].FindControl("txtPayment_Amount");
            DropDownList _ddlEditpaymentmethod = (DropDownList)gv.Rows[e.RowIndex].FindControl("ddlEditpaymentmethod");
            DropDownList _ddlEditbank = (DropDownList)gv.Rows[e.RowIndex].FindControl("ddlEditbank");
            TextBox _txtEditCheque_No = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditCheque_No");
            TextBox _txtEditDate = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditDate");
            CheckBox _chkEditClearing_Cheque = (CheckBox)gv.Rows[e.RowIndex].FindControl("chkEditClearing_Cheque");
            Label _lblPayment_No = (Label)gv.Rows[e.RowIndex].FindControl("lblPayment_No");

            if (string.IsNullOrEmpty(_txtPayment_Amount.Text) || string.IsNullOrEmpty(_txtItemPayment_Date.Text))
            {
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }
            else
            {
                dbo_CreditPaymentClass payment = dbo_CreditPaymentDataClass.Select_Record(_lblPayment_No.Text);

                payment.Bank = _ddlEditbank.SelectedIndex > 0 ? _ddlEditbank.SelectedValue : string.Empty;
                payment.Cheque_No = _txtEditCheque_No.Text;
                payment.Clearing_Cheque = _chkEditClearing_Cheque.Checked;

                payment.Date = string.IsNullOrEmpty(_txtEditDate.Text) ? (DateTime?)null : DateTime.Parse(_txtEditDate.Text);
                payment.Payment_Date = string.IsNullOrEmpty(_txtItemPayment_Date.Text) ? (DateTime?)null : DateTime.Parse(_txtItemPayment_Date.Text);
                payment.Payment_Method = _ddlEditpaymentmethod.SelectedValue;


                payment.Payment_Amount = decimal.Parse(_txtPayment_Amount.Text);

                dbo_CreditClass Prev_credit = dbo_CreditDataClass.Select_Record(lblCredit_ID.Text);
                HiddenField hdfHiddenField = (HiddenField)gv.Rows[e.RowIndex].FindControl("hdfOldPayment_Amount");

                decimal? calpayment = decimal.Parse(_txtPayment_Amount.Text) - decimal.Parse(hdfHiddenField.Value);


                Prev_credit.Total_Payment_Amount += calpayment;
                string Balance_Outstanding_Amount = Prev_credit.Balance_Outstanding_Amount.Value.ToString("#,##0.#0");
                string Balance_Outstanding_Amount_01 = Prev_credit.Balance_Outstanding_Amount.Value.ToString();
                string Payment_Amount = Prev_credit.Total_Payment_Amount.Value.ToString("#,##0.#0");
                string Payment_Amount_01 = Prev_credit.Total_Payment_Amount.Value.ToString();

                Prev_credit.Balance_Outstanding_Amount -= calpayment;



                if (Prev_credit.Balance_Outstanding_Amount >= 0)
                {
                    if (Prev_credit.Balance_Outstanding_Amount == 0)
                    {
                        Prev_credit.Status = "2";
                    }
                    dbo_CreditDataClass.Update(Prev_credit);
                    dbo_CreditPaymentDataClass.Update(payment);

                    gv.ShowFooter = false;
                    gv.EditIndex = -1;
                    List<dbo_CreditPaymentClass> item_value = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);
                    gv.DataSource = item_value;
                    gv.DataBind();

                    Show("บันทึกสำเร็จ!");
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                    //btnSearchCreditPayment_Click(sender, e);
                    btnSearchCreditPayment_Click(null, null);
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //Show("กรุณาระบุข้อมูลให้ถูกต้อง");
                   decimal Balance_Outstanding = Convert.ToDecimal(_txtPayment_Amount.Text) + Prev_credit.Balance_Outstanding_Amount.Value;

                    Show("ไม่สามารถระบุ จำนวนเงินมากกว่าจำนวนเงินค้างชำระ " + string.Format("{0:#,#.00}", Balance_Outstanding)  + " บาทได้!");
                    _txtPayment_Amount.Text = Balance_Outstanding.ToString();

                    //Show(Prev_credit.Balance_Outstanding_Amount.Value.ToString() +","+ gg);
                    //if(gv.Rows.Count > 1)
                    // {
                    //     Show("ไม่สามารถระบุ จำนวนเงินมากกว่าจำนวนเงินค้างชำระจำนวน " + Balance_Outstanding_Amount + " บาทได้");
                    //     _txtPayment_Amount.Text = Balance_Outstanding_Amount_01;
                    // }
                    //else
                    // {
                    //     Show("ไม่สามารถระบุ จำนวนเงินมากกว่าจำนวนเงินค้างชำระจำนวน " + Payment_Amount + " บาทได้");
                    //     _txtPayment_Amount.Text = Payment_Amount_01;
                    // }

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //lnkBEdit
       
  

    }

    protected void GridViewCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        if (e.CommandName == "AddNew")
        {
            try
            {
                #region Old
                //int current_index = (int)ViewState["RowIndexCreditPayment"];

                //string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                //dbo_CreditPaymentClass credit = new dbo_CreditPaymentClass();
                //// credit.Payment_No = GenerateID.Payment_No(user_class.CV_CODE);


                //GridView gv = (GridView)GridViewCreditPayment.Rows[current_index + 1].FindControl("GridViewCustomer");
                //Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[current_index].FindControl("lblCredit_ID");

                //DropDownList _ddlFooterbank = (DropDownList)gv.FooterRow.FindControl("ddlFooterbank");
                //TextBox _txtFooterCheque_Name = (TextBox)gv.FooterRow.FindControl("txtFooterCheque_Name");
                //TextBox _txtFooterPaymentAmount = (TextBox)gv.FooterRow.FindControl("txtFooterPaymentAmount");
                //CheckBox _chkFooterClearing_Cheque = (CheckBox)gv.FooterRow.FindControl("chkFooterClearing_Cheque");
                //DropDownList _ddlFooterpaymentmethod = (DropDownList)gv.FooterRow.FindControl("ddlFooterpaymentmethod");
                //TextBox _txtFooterPaymentDate = (TextBox)gv.FooterRow.FindControl("txtFooterPaymentDate");
                //TextBox _txtFooterDate = (TextBox)gv.FooterRow.FindControl("txtFooterDate");



                //if (string.IsNullOrEmpty(_txtFooterPaymentDate.Text) || string.IsNullOrEmpty(_txtFooterPaymentAmount.Text))
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //    Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
                //}
                //else
                //{
                //    credit.Payment_No = GenerateID.Payment_No(user_class.CV_CODE);
                //    credit.Bank = (_ddlFooterbank.SelectedIndex == 0 ? null : _ddlFooterbank.SelectedValue);
                //    credit.Credit_ID = lblCredit_ID.Text;
                //    credit.Payment_Amount = decimal.Parse(_txtFooterPaymentAmount.Text);
                //    credit.Payment_Method = _ddlFooterpaymentmethod.SelectedValue;
                //    credit.Payment_Date = (!string.IsNullOrEmpty(_txtFooterPaymentDate.Text) ? DateTime.Parse(_txtFooterPaymentDate.Text) : (DateTime?)null);
                //    credit.Date = (!string.IsNullOrEmpty(_txtFooterDate.Text) ? DateTime.Parse(_txtFooterDate.Text) : (DateTime?)null);
                //    credit.Cheque_No = _txtFooterCheque_Name.Text;


                //    if (!btnConfirmPaymentBackToGrid.Visible)
                //    {
                //        credit.Clearing_No = txtClearing_No.Text;
                //    }


                //    dbo_CreditClass Prev_credit = dbo_CreditDataClass.Select_Record(lblCredit_ID.Text);

                //    Prev_credit.Total_Payment_Amount += credit.Payment_Amount;
                //    Prev_credit.Balance_Outstanding_Amount = Prev_credit.Credit_Amount - Prev_credit.Total_Payment_Amount;



                //    if (Prev_credit.Balance_Outstanding_Amount >= 0)
                //    {
                //        dbo_CreditPaymentDataClass.Add(credit);
                //        if (Prev_credit.Balance_Outstanding_Amount == 0)
                //        {
                //            Prev_credit.Status = "2";

                //        }

                //        dbo_CreditDataClass.Update(Prev_credit);
                //        gv.ShowFooter = false;
                //        List<dbo_CreditPaymentClass> item_value = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);
                //        gv.DataSource = item_value;
                //        gv.DataBind();

                //        Show("บันทึกสำเร็จ!");
                //        System.Threading.Thread.Sleep(1000);
                //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                //        btnSearchCreditPayment_Click(sender, e);

                //    }
                //    else
                //    {
                //        System.Threading.Thread.Sleep(1000);
                //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //        Show("กรุณาระบุข้อมูลให้ถูกต้อง");
                //    }
                //}
                #endregion
                Add_PaymentCredit();

                #region
                /*

                decimal? initail_amount = credit.Payment_Amount;
                dbo_DebtClass last_debt = null;


                List<dbo_DebtClass> listdebtcust = dbo_DebtDataClass.Search(string.Empty, Prev_credit.Customer_ID).OrderBy(f => f.Created_Date).ToList();


                foreach (dbo_DebtClass debt in listdebtcust.Where(f => f.Balance_Outstanding_Amount > 0))
                {
                    if (initail_amount > 0)
                    {
                        decimal? cal_amount = initail_amount - debt.Balance_Outstanding_Amount;

                        if (cal_amount > 0)
                        {
                            debt.Total_Payment_Amount += (debt.Balance_Outstanding_Amount);
                            initail_amount -= debt.Balance_Outstanding_Amount;
                            debt.Balance_Outstanding_Amount = 0;
                        }
                        else
                        {
                            debt.Total_Payment_Amount += initail_amount;
                            debt.Balance_Outstanding_Amount -= initail_amount;
                            initail_amount = 0;
                        }

                        dbo_DebtDataClass.Update(debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        last_debt = debt;
                    }
                }

                if (initail_amount > 0 && last_debt != null)
                {
                    last_debt.Total_Payment_Amount += initail_amount;
                    last_debt.Balance_Outstanding_Amount -= initail_amount;

                    dbo_DebtDataClass.Update(last_debt, HttpContext.Current.Request.Cookies["User_ID"].Value);
                }

                */

                #endregion

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }

    protected void GridViewSubsidy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_SubsidyClass> list_subsidy = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
        GridViewSubsidy.ShowFooter = false;
        GridViewSubsidy.DataSource = list_subsidy;
        GridViewSubsidy.EditIndex = -1;
        GridViewSubsidy.DataBind();

     

        if (Session["Create_Subsid"] != null)
        {
            //Session.Remove("Create_Deduct");
            Session.Remove("Create_Subsid");
            //ButtonDeduct.Text = "ถัดไป";
        }

        if (Session["Create_Subsid"] == null && Session["Create_Deduct"] == null)
        {
            ButtonDeduct.Text = "ถัดไป";
        }

    }

    protected void GridViewSubsidy_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (e.CommandName == "AddNew")
        {
            try
            {
                #region Old
                //dbo_CreditClass credit = new dbo_CreditClass();
                //DropDownList _ddlFooterDetail = (DropDownList)GridViewSubsidy.FooterRow.FindControl("ddlFooterDetail");
                //TextBox _txtFooterSubsidy_Amount = (TextBox)GridViewSubsidy.FooterRow.FindControl("txtFooterSubsidy_Amount");

                //string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                //if (_ddlFooterDetail.SelectedIndex > 0)
                //{
                //    if (string.IsNullOrEmpty(_txtFooterSubsidy_Amount.Text))
                //    {
                //        System.Threading.Thread.Sleep(1000);
                //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //        Show("กรุณาระบุจำนวนเงิน");
                //    }
                //    else
                //    {
                //        dbo_SubsidyClass subsidy = new dbo_SubsidyClass();
                //        subsidy.Clearing_No = txtClearing_No.Text;
                //        subsidy.Subsidy_ID = GenerateID.Subsidy_ID(user_class.CV_CODE);
                //        subsidy.Subsidy_Detail = _ddlFooterDetail.SelectedValue;
                //        subsidy.Subsidy_Amount = decimal.Parse(_txtFooterSubsidy_Amount.Text);

                //        dbo_SubsidyDataClass.Add(subsidy);

                //        GridViewSubsidy.ShowFooter = false;
                //        List<dbo_SubsidyClass> listofitem = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
                //        GridViewSubsidy.DataSource = listofitem;
                //        GridViewSubsidy.DataBind();

                //        Show("บันทึกสำเร็จ");
                //        System.Threading.Thread.Sleep(1000);
                //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //    }
                //}
                //else
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //    Show("กรุณาระบุรายละเอียด");
                //}
                #endregion
                Add_Subsidy();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }

    protected void GridViewSubsidy_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            LinkButton lnk = (LinkButton)GridViewSubsidy.Rows[e.RowIndex].FindControl("lnkBDelete");
            dbo_SubsidyDataClass.Delete(lnk.CommandArgument);


            List<dbo_SubsidyClass> listofitem = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
            GridViewSubsidy.DataSource = listofitem;
            GridViewSubsidy.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void GridViewSubsidy_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_SubsidyClass> list_subsidy = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
            GridViewSubsidy.ShowFooter = false;
            GridViewSubsidy.EditIndex = e.NewEditIndex;
            GridViewSubsidy.DataSource = list_subsidy;
            GridViewSubsidy.DataBind();

            List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountExpense();

            //GetAccountCode_New("05");


            item_value.Insert(0, (new dbo_AccountCodeClass() { Account_Code = string.Empty, Account_Name = "==ระบุ==" }));
            DropDownList ddl = (DropDownList)GridViewSubsidy.Rows[e.NewEditIndex].FindControl("ddlEditDetail");
            ddl.DataSource = item_value;
            ddl.DataBind();

            HiddenField hdfEditDetail = (HiddenField)GridViewSubsidy.Rows[e.NewEditIndex].FindControl("hdfEditDetail");
            ddl.Items.FindByText(hdfEditDetail.Value).Selected = true;

            // ddl.Text = 
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void GridViewSubsidy_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {

            Label Subsidy_ID_ = (Label)GridViewSubsidy.Rows[e.RowIndex].FindControl("lblSubsidy_ID");
            dbo_SubsidyClass subsi = dbo_SubsidyDataClass.Select_Record(Subsidy_ID_.Text);

            DropDownList ddlEditDetail_ = (DropDownList)GridViewSubsidy.Rows[e.RowIndex].FindControl("ddlEditDetail");
            TextBox txtEditSubsidy_Amount_ = (TextBox)GridViewSubsidy.Rows[e.RowIndex].FindControl("txtEditSubsidy_Amount");


            if (ddlEditDetail_.SelectedIndex > 0 && !string.IsNullOrEmpty(txtEditSubsidy_Amount_.Text))
            {

                subsi.Subsidy_Detail = ddlEditDetail_.SelectedIndex > 0 ? ddlEditDetail_.SelectedValue : string.Empty;
                subsi.Subsidy_Amount = string.IsNullOrEmpty(txtEditSubsidy_Amount_.Text) ? (decimal?)null : decimal.Parse(txtEditSubsidy_Amount_.Text);


                dbo_SubsidyDataClass.Update(subsi);

                GridViewSubsidy.EditIndex = -1;
                GridViewSubsidy.ShowFooter = false;
                List<dbo_SubsidyClass> listofitem = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
                GridViewSubsidy.DataSource = listofitem;
                GridViewSubsidy.DataBind();

                Show("บันทึกสำเร็จ");
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุข้อมูลให้ครบถ้วน");
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewSubsidy_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridViewDeduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        GridViewDeduct.EditIndex = -1;
        GridViewDeduct.ShowFooter = false;
        List<dbo_DeductClass> deduct = dbo_DeductDataClass.Search(txtClearing_No.Text);
        GridViewDeduct.DataSource = deduct;
        GridViewDeduct.DataBind();

         if( Session["Create_Deduct"] != null)
        {
            Session.Remove("Create_Deduct");
            //Session.Remove("Create_Subsid");
            // ButtonDeduct.Text = "ถัดไป";
        }

        if(Session["Create_Subsid"] == null && Session["Create_Deduct"] == null)
        {
            ButtonDeduct.Text = "ถัดไป";
        }
       


    }

    protected void GridViewDeduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddNew")
        {
            try
            {
                #region Old
                //DropDownList _ddlFooterDetail = (DropDownList)GridViewDeduct.FooterRow.FindControl("ddlFooterDetail");
                //TextBox _txtFooterDeduct_Amount = (TextBox)GridViewDeduct.FooterRow.FindControl("txtFooterDeduct_Amount");

                //string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                //if (_ddlFooterDetail.SelectedIndex > 0)
                //{

                //    if (string.IsNullOrEmpty(_txtFooterDeduct_Amount.Text))
                //    {
                //        System.Threading.Thread.Sleep(1000);
                //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //        Show("กรุณาระบุจำนวนเงิน");
                //    }
                //    else
                //    {

                //        dbo_DeductClass deduct = new dbo_DeductClass();
                //        deduct.Clearing_No = txtClearing_No.Text;
                //        deduct.Deduct_ID = GenerateID.Deduct_ID(user_class.CV_CODE);
                //        deduct.Deduct_Detail = _ddlFooterDetail.SelectedValue;
                //        deduct.Deduct_Amount = decimal.Parse(_txtFooterDeduct_Amount.Text);

                //        dbo_DeductDataClass.Add(deduct);

                //        GridViewDeduct.ShowFooter = false;
                //        List<dbo_DeductClass> listofitem = dbo_DeductDataClass.Search(txtClearing_No.Text);
                //        GridViewDeduct.DataSource = listofitem;
                //        GridViewDeduct.DataBind();

                //        System.Threading.Thread.Sleep(1000);
                //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                //    }

                //}
                //else
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //    Show("กรุณาระบุรายละเอียด");
                //}
                #endregion
                Add_Deduct();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

    }

    protected void GridViewDeduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            LinkButton lnk = (LinkButton)GridViewDeduct.Rows[e.RowIndex].FindControl("lnkBDelete");
            dbo_DeductDataClass.Delete(lnk.CommandArgument);


            List<dbo_DeductClass> listofitem = dbo_DeductDataClass.Search(txtClearing_No.Text);
            GridViewDeduct.DataSource = listofitem;
            GridViewDeduct.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void GridViewDeduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            GridViewDeduct.EditIndex = e.NewEditIndex;
            GridViewDeduct.ShowFooter = false;
            List<dbo_DeductClass> deduct_item = dbo_DeductDataClass.Search(txtClearing_No.Text);
            GridViewDeduct.DataSource = deduct_item;
            GridViewDeduct.DataBind();


            List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountRevenue();
            // GetAccountCode_New("04");
            item_value.Insert(0, (new dbo_AccountCodeClass() { Account_Code = string.Empty, Account_Name = "==ระบุ==" }));
            DropDownList ddl = (DropDownList)GridViewDeduct.Rows[e.NewEditIndex].FindControl("ddlEditDetail");

            ddl.DataSource = item_value;
            ddl.DataBind();

            HiddenField hdfEditDetail = (HiddenField)GridViewDeduct.Rows[e.NewEditIndex].FindControl("hdfEditDetail");
            ddl.Items.FindByText(hdfEditDetail.Value).Selected = true;


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewDeduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);


        try
        {
            DropDownList ddlEditDetail_ = (DropDownList)GridViewDeduct.Rows[e.RowIndex].FindControl("ddlEditDetail");
            TextBox txtCredit_Amount_ = (TextBox)GridViewDeduct.Rows[e.RowIndex].FindControl("txtCredit_Amount");

            if (ddlEditDetail_.SelectedIndex > 0 && !string.IsNullOrEmpty(txtCredit_Amount_.Text))
            {
                Label lblDeduct_ID = (Label)GridViewDeduct.Rows[e.RowIndex].FindControl("lblDeduct_ID");

                dbo_DeductClass deduct = dbo_DeductDataClass.Select_Record(lblDeduct_ID.Text);


                deduct.Deduct_Amount = string.IsNullOrEmpty(txtCredit_Amount_.Text) ? (decimal?)null : decimal.Parse(txtCredit_Amount_.Text);
                deduct.Deduct_Detail = ddlEditDetail_.SelectedIndex > 0 ? ddlEditDetail_.SelectedValue : string.Empty;

                dbo_DeductDataClass.Update(deduct);


                GridViewDeduct.EditIndex = -1;
                GridViewDeduct.ShowFooter = false;


                List<dbo_DeductClass> deduct_item = dbo_DeductDataClass.Search(txtClearing_No.Text);
                GridViewDeduct.DataSource = deduct_item;
                GridViewDeduct.DataBind();

                Show("บันทึกสำเร็จ!");
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุข้อมูลที่จำเป็นให้ครบถ้วน");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewDeduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridViewClearing_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (e.CommandName == "View")
        {
            try
            {
                int Index = int.Parse(e.CommandArgument.ToString());

                string Clearing_No = string.Empty;
                string Requistion = string.Empty;

                GridViewRow currentRow = GridViewClearing.Rows[Index];
                Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");
                LinkButton lnkView = (LinkButton)currentRow.FindControl("lnkBClearing_No");

                txtClearing_No.Text = lnkView.Text;


                hdfRequisition_No.Value = string.Empty;
                foreach (GridViewRow row in GridViewClearing.Rows)
                {
                    LinkButton lnkView1 = (LinkButton)row.FindControl("lnkBClearing_No");

                    if (lnkView1.Text == lnkView.Text)
                    {
                        logger.Debug("lnkView1.Text " + lnkView1.Text);
                        Label lnkB_Requisition_No = (Label)row.FindControl("lnkB_Requisition_No");
                        hdfRequisition_No.Value += lnkB_Requisition_No.Text;
                    }
                }

                hdfUser_ID.Value = lblUser_ID.Text;
                dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);
                txtDiscount.Text = clearing.Discount.Value.ToString("#,##0.#0");
                txtActualPayment.Text = clearing.Actual_Payment.Value.ToString("#,##0.#0");
                txtActualPayment.Enabled = false;
                txtDiscount.Enabled = false;
                hdfClearing_Status.Value = clearing.Status;

                if (clearing.Status == "1")
                {
                    ButtonSaveClearing.Text = "แก้ไข";
                    ButtonConfirmClearing.Visible = false;
                    ButtonSaveClearing.Visible = true;

                    ButtonSave.Visible = true;
                    ButtonSaveAndNext.Text = "บันทึกและหน้าถัดไป";

                    GetPaymentDetail();
                }
                else if (clearing.Status == "2")
                {
                    ButtonSaveClearing.Visible = false;
                    ButtonConfirmClearing.Visible = false;
                    ButtonSave.Visible = false;
                    ButtonSaveAndNext.Text = "หน้าถัดไป";

                    GetPaymentDetail();
                    // GetDetailsDataToForm(txtClearing_No.Text, "View");
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }
        else if (e.CommandName == "ClearingResign")
        {
            hdfResign.Value = "1";
            pnlStep.Visible = false;
            pnlResign.Visible = true;

            int Index = int.Parse(e.CommandArgument.ToString());
            GridViewRow currentRow = GridViewClearing.Rows[Index];
            Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");
            LinkButton lnkView = (LinkButton)currentRow.FindControl("lnkBClearing_No");
            txtClearing_No.Text = lnkView.Text;

            hdfUser_ID.Value = lblUser_ID.Text;
            hdfRequisition_No.Value = string.Empty;


            ButtonSaveClearing.Text = "แก้ไข";
            ButtonConfirmClearing.Visible = false;
            ButtonSaveClearing.Visible = true;

            ButtonSave.Visible = true;
            ButtonSaveAndNext.Text = "บันทึกและหน้าถัดไป";

            dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(txtClearing_No.Text);

            txtDiscount.Text = clearing.Discount.Value.ToString("#,##0.#0");
            txtActualPayment.Text = clearing.Actual_Payment.Value.ToString("#,##0.#0");
            txtActualPayment.Enabled = false;
            txtDiscount.Enabled = false;

            hdfClearing_Status.Value = clearing.Status;
            GetPaymentDetail();
        }
        else if (e.CommandName == "ClearingCreditPayment")
        {
            int Index = int.Parse(e.CommandArgument.ToString());
            GridViewRow currentRow = GridViewClearing.Rows[Index];
            LinkButton lnkView = (LinkButton)currentRow.FindControl("lnkBClearing_No");
            Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");
            hdfUser_ID.Value = lblUser_ID.Text;

            txtClearing_No.Text = lnkView.Text;

            // btnConfirmPayment.Visible = true;
            btnConfirmPaymentBackToGrid.Visible = true;

            pnlGrid.Visible = false;

            ShowStep3();

            btnBackToCredit.Visible = false;
            ButtonSubsidy.Visible = false;


        }
        else if (e.CommandName == "Print")
        {
            try
            {
                int Index = int.Parse(e.CommandArgument.ToString());

                GridViewRow currentRow = GridViewClearing.Rows[Index];

                LinkButton lnkView = (LinkButton)currentRow.FindControl("lnkBClearing_No");
                Label lnkView1 = (Label)currentRow.FindControl("lnkB_Requisition_No");

                string RQ_N01 = string.Empty;
                string RQ_No2 = string.Empty;
                List<dbo_RequisitionClearingClass> rq_no = dbo_RequisitionClearingDataClass.getrq(lnkView.Text);
                string[] RQ_No = rq_no.Select(f => f.Requisition_No).ToArray();

                foreach (string RQNO in RQ_No)
                {
                    if (RQ_N01 == "")
                    {
                        RQ_N01 = RQNO;
                    }
                    else
                    {
                        RQ_No2 = RQNO;
                    }
                }

                string url = "../Report_From/ViewsReport.aspx?RPT=Clearing_No&PRM=" + lnkView.Text;
                //mage PDF
                //string url = "../Report/ClearingViewer.aspx?RPT=RPT_ClearingSummaryInfo&PRM=" + lnkView.Text + "&RQ_N01=" + RQ_N01 + "&RQ_N02=" + RQ_No2;
                string url1 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Clearing_No&PRM=" + lnkView.Text;
                string url2 = "";
                string url3 = "";
                if (lnkView.Text == "")
                {
                    url2 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No&PRM=" + lnkView1.Text;
                }
                else
                {
                    url2 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No&PRM=" + RQ_N01;
                    url3 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Requisition_No1&PRM1=" + RQ_No2;
                }

                if (lnkView.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    System.Threading.Thread.Sleep(1000);
                    string script = "alert(\"ไม่พบข้อมูลการเคลียร์เงิน\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                }
                else
                {


                    string s = "window.open('" + url2 + "','popup_window2', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                    if (RQ_No2 != "")
                    {
                        s += "window.open('" + url3 + "','popup_window3', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                    }

                    s += "window.open('" + url + "', 'popup_window', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                    s += "window.open('" + url1 + "','popup_window1', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";

                    // string s  = "window.open('" + url + "', 'popup_window', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";
                    s += "window.open('" + url1 + "','popup_window1', 'location=1,status=1,scrollbars=1,width=1024,height=768,left=100,top=100,resizable=yes');";

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    protected void GridViewClearing_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int Index = e.RowIndex;


            //string Clearing_No = string.Empty;
            //string Requistion = string.Empty;

            GridViewRow currentRow = GridViewClearing.Rows[Index];
            //Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");
            LinkButton lnkView = (LinkButton)currentRow.FindControl("lnkBClearing_No");

            Label uSr_id = (Label)currentRow.FindControl("lblUser_ID");
            dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(lnkView.Text);

            if (clearing.Status == "1")
            {

                // 1. Delete deposit 
                // 2. Delete deposit detail
                // 3. Commission
                // 4. Debt

                dbo_DepositDetailDataClass.Delete(lnkView.Text);

                dbo_DepositDataClass.Delete(lnkView.Text);

                dbo_RequisitionClearingDataClass.Delete(lnkView.Text);

                dbo_ClearingDataClass.Delete(lnkView.Text);

                List<dbo_CreditClass> credit_list = dbo_CreditDataClass.Search(lnkView.Text, string.Empty, null, string.Empty, uSr_id.Text);

                foreach (dbo_CreditClass credit in credit_list)
                {
                    dbo_CreditDataClass.Delete(credit.Credit_ID);
                }

                List<dbo_CreditPaymentClass> payment_list = dbo_CreditPaymentDataClass.Search(string.Empty, lnkView.Text);

                foreach (dbo_CreditPaymentClass payment in payment_list)
                {
                    dbo_CreditPaymentDataClass.Delete(payment.Payment_No);
                }


                List<dbo_SubsidyClass> subsidy_list = dbo_SubsidyDataClass.Search(lnkView.Text);
                foreach (dbo_SubsidyClass subsidy in subsidy_list)
                {
                    dbo_SubsidyDataClass.Delete(subsidy.Subsidy_ID);
                }

                List<dbo_DeductClass> deduct_list = dbo_DeductDataClass.Search(lnkView.Text);
                foreach (dbo_DeductClass deduct in deduct_list)
                {
                    dbo_DeductDataClass.Delete(deduct.Deduct_ID);
                }

                btnSearch_Click(null, null);

                Show("ลบข้อมูลสำเร็จ");

            }
            else
            {
                Show("ใบเคลียร์เงินยืนยันแล้ว ไม่สามารถลบได้");
            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewClearing_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridViewClearing_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox tx1 = e.Row.FindControl("txt_Deposit_Qty") as TextBox;
            TextBox sale_qty = e.Row.FindControl("Label_Sales_Qty") as TextBox;
            TextBox total_sale_qty = e.Row.FindControl("Label_TotalSales_Qty") as TextBox;
            Label price = e.Row.FindControl("lbl_PricePerUnit") as Label;
            Label net_sale_qty = e.Row.FindControl("Label_Net_Sales_Qty") as Label;
            TextBox txt2 = e.Row.FindControl("txt_Deposit_QtyReturn") as TextBox;
            Label depositCheck = e.Row.FindControl("Label_CheckDeposit_Qty") as Label;
            Label returnCheck = e.Row.FindControl("Label_CheckReturn_Qty") as Label;

            //tx1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            tx1.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            tx1.Attributes.Add("ondrop", "javascript:return false;");
            tx1.Attributes.Add("onpaste", "javascript:return false;");
            string script = string.Format("javascript:return UpdateField_txt_Deposit_Qty({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            tx1.Attributes.Add("onblur", script);
            string script2 = string.Format("javascript:return ClearValue_txt_Deposit_Qty({0});", tx1.ClientID);
            tx1.Attributes.Add("onFocus", script2);

            //txt2.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt2.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            txt2.Attributes.Add("ondrop", "javascript:return false;");
            txt2.Attributes.Add("onpaste", "javascript:return false;");
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, hdfResign.Value);
            string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7},{8});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID
                , net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, returnCheck.ClientID, hdfResign.Value);
            txt2.Attributes.Add("onblur", script3);
            string script4 = string.Format("javascript:return ClearValue_txt_Deposit_QtyReturn({0});", txt2.ClientID);
            txt2.Attributes.Add("onFocus", script4);
        }
    }

    protected void GridViewClearing_2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox tx1 = e.Row.FindControl("txt_Deposit_Qty") as TextBox;
            TextBox sale_qty = e.Row.FindControl("Label_Sales_Qty") as TextBox;
            TextBox total_sale_qty = e.Row.FindControl("Label_TotalSales_Qty") as TextBox;
            Label price = e.Row.FindControl("lbl_PricePerUnit") as Label;
            Label net_sale_qty = e.Row.FindControl("Label_Net_Sales_Qty") as Label;
            TextBox txt2 = e.Row.FindControl("txt_Deposit_QtyReturn") as TextBox;
            Label depositCheck = e.Row.FindControl("Label_CheckDeposit_Qty") as Label;
            Label returnCheck = e.Row.FindControl("Label_CheckReturn_Qty") as Label;

            //tx1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            tx1.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            tx1.Attributes.Add("ondrop", "javascript:return false;");
            tx1.Attributes.Add("onpaste", "javascript:return false;");
            string script = string.Format("javascript:return UpdateField_txt_Deposit_Qty({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            tx1.Attributes.Add("onblur", script);
            string script2 = string.Format("javascript:return ClearValue_txt_Deposit_Qty({0});", tx1.ClientID);
            tx1.Attributes.Add("onFocus", script2);

            //txt2.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt2.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            txt2.Attributes.Add("ondrop", "javascript:return false;");
            txt2.Attributes.Add("onpaste", "javascript:return false;");
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, hdfResign.Value);
            string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7},{8});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID
                , net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, returnCheck.ClientID, hdfResign.Value);
            txt2.Attributes.Add("onblur", script3);
            string script4 = string.Format("javascript:return ClearValue_txt_Deposit_QtyReturn({0});", txt2.ClientID);
            txt2.Attributes.Add("onFocus", script4);
        }
    }

    protected void GridViewClearing_3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox tx1 = e.Row.FindControl("txt_Deposit_Qty") as TextBox;
            TextBox sale_qty = e.Row.FindControl("Label_Sales_Qty") as TextBox;
            TextBox total_sale_qty = e.Row.FindControl("Label_TotalSales_Qty") as TextBox;
            Label price = e.Row.FindControl("lbl_PricePerUnit") as Label;
            Label net_sale_qty = e.Row.FindControl("Label_Net_Sales_Qty") as Label;
            TextBox txt2 = e.Row.FindControl("txt_Deposit_QtyReturn") as TextBox;
            Label depositCheck = e.Row.FindControl("Label_CheckDeposit_Qty") as Label;
            Label returnCheck = e.Row.FindControl("Label_CheckReturn_Qty") as Label;

            //tx1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            tx1.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            tx1.Attributes.Add("ondrop", "javascript:return false;");
            tx1.Attributes.Add("onpaste", "javascript:return false;");
            string script = string.Format("javascript:return UpdateField_txt_Deposit_Qty({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            tx1.Attributes.Add("onblur", script);
            string script2 = string.Format("javascript:return ClearValue_txt_Deposit_Qty({0});", tx1.ClientID);
            tx1.Attributes.Add("onFocus", script2);

            //txt2.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt2.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            txt2.Attributes.Add("ondrop", "javascript:return false;");
            txt2.Attributes.Add("onpaste", "javascript:return false;");
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, hdfResign.Value);
            string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7},{8});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID
                , net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, returnCheck.ClientID, hdfResign.Value);
            txt2.Attributes.Add("onblur", script3);
            string script4 = string.Format("javascript:return ClearValue_txt_Deposit_QtyReturn({0});", txt2.ClientID);
            txt2.Attributes.Add("onFocus", script4);
        }
    }

    protected void GridViewClearing_4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox tx1 = e.Row.FindControl("txt_Deposit_Qty") as TextBox;
            TextBox sale_qty = e.Row.FindControl("Label_Sales_Qty") as TextBox;
            TextBox total_sale_qty = e.Row.FindControl("Label_TotalSales_Qty") as TextBox;
            Label price = e.Row.FindControl("lbl_PricePerUnit") as Label;
            Label net_sale_qty = e.Row.FindControl("Label_Net_Sales_Qty") as Label;
            TextBox txt2 = e.Row.FindControl("txt_Deposit_QtyReturn") as TextBox;
            Label depositCheck = e.Row.FindControl("Label_CheckDeposit_Qty") as Label;
            Label returnCheck = e.Row.FindControl("Label_CheckReturn_Qty") as Label;

            //tx1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            tx1.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            tx1.Attributes.Add("ondrop", "javascript:return false;");
            tx1.Attributes.Add("onpaste", "javascript:return false;");
            string script = string.Format("javascript:return UpdateField_txt_Deposit_Qty({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            tx1.Attributes.Add("onblur", script);
            string script2 = string.Format("javascript:return ClearValue_txt_Deposit_Qty({0});", tx1.ClientID);
            tx1.Attributes.Add("onFocus", script2);

            //txt2.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt2.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            txt2.Attributes.Add("ondrop", "javascript:return false;");
            txt2.Attributes.Add("onpaste", "javascript:return false;");
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, hdfResign.Value);
            string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7},{8});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID
                , net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, returnCheck.ClientID, hdfResign.Value);
            txt2.Attributes.Add("onblur", script3);
            string script4 = string.Format("javascript:return ClearValue_txt_Deposit_QtyReturn({0});", txt2.ClientID);
            txt2.Attributes.Add("onFocus", script4);
        }
    }

    protected void GridViewClearing_5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox tx1 = e.Row.FindControl("txt_Deposit_Qty") as TextBox;
            TextBox sale_qty = e.Row.FindControl("Label_Sales_Qty") as TextBox;
            TextBox total_sale_qty = e.Row.FindControl("Label_TotalSales_Qty") as TextBox;
            Label price = e.Row.FindControl("lbl_PricePerUnit") as Label;
            Label net_sale_qty = e.Row.FindControl("Label_Net_Sales_Qty") as Label;
            TextBox txt2 = e.Row.FindControl("txt_Deposit_QtyReturn") as TextBox;
            Label depositCheck = e.Row.FindControl("Label_CheckDeposit_Qty") as Label;
            Label returnCheck = e.Row.FindControl("Label_CheckReturn_Qty") as Label;

            //tx1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            tx1.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            tx1.Attributes.Add("ondrop", "javascript:return false;");
            tx1.Attributes.Add("onpaste", "javascript:return false;");
            string script = string.Format("javascript:return UpdateField_txt_Deposit_Qty({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            tx1.Attributes.Add("onblur", script);
            string script2 = string.Format("javascript:return ClearValue_txt_Deposit_Qty({0});", tx1.ClientID);
            tx1.Attributes.Add("onFocus", script2);

            //txt2.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt2.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
            txt2.Attributes.Add("ondrop", "javascript:return false;");
            txt2.Attributes.Add("onpaste", "javascript:return false;");
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID);
            //string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID, net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, hdfResign.Value);
            string script3 = string.Format("javascript:return UpdateField_txt_Deposit_QtyReturn({0},{1},{2},{3},{4},{5},{6},{7},{8});", tx1.ClientID, sale_qty.ClientID, total_sale_qty.ClientID, price.ClientID
                , net_sale_qty.ClientID, txt2.ClientID, depositCheck.ClientID, returnCheck.ClientID, hdfResign.Value);
            txt2.Attributes.Add("onblur", script3);
            string script4 = string.Format("javascript:return ClearValue_txt_Deposit_QtyReturn({0});", txt2.ClientID);
            txt2.Attributes.Add("onFocus", script4);

        }
    }

    protected void GridViewClearing_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewClearing.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewClearing.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewClearing.PageIndex)
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
            int currentPage = GridViewClearing.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewClearing.PageCount.ToString();

        }
    }
    public bool Add_Credit()
    {

        try
        {
            dbo_CreditClass credit = new dbo_CreditClass();
            DropDownList ddl = (DropDownList)GridViewCredit.FooterRow.FindControl("ddlFooterCustomerName");
            TextBox txtCredit_Amount = (TextBox)GridViewCredit.FooterRow.FindControl("txtFooterCredit_Amount");
            TextBox txtFooterCredit_Date = (TextBox)GridViewCredit.FooterRow.FindControl("txtFooterCredit_Date");

            if (ddl.SelectedIndex > 0)
            {
                if (string.IsNullOrEmpty(txtCredit_Amount.Text))
                {
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณาระบุจำนวนเงิน");

                    return false;
                }
                else
                {
                    credit.Customer_ID = ddl.SelectedValue;
                    credit.Clearing_No = txtClearing_No.Text;
                    credit.Credit_ID = GenerateID.Credit_ID(ViewState["CV_Code"].ToString());
                    credit.Credit_Amount = Decimal.Parse(txtCredit_Amount.Text);
                    credit.Credit_Date = (string.IsNullOrEmpty(txtFooterCredit_Date.Text) ? (DateTime?)null : DateTime.Parse(txtFooterCredit_Date.Text));
                    credit.Status = "1";
                    credit.Balance_Outstanding_Amount = credit.Credit_Amount;
                    credit.Total_Payment_Amount = 0;
                    dbo_CreditDataClass.Add(credit);

                    GridViewCredit.ShowFooter = false;

                    dbo_RequisitionClass requ = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
                    string replace_sale = requ.Replace_Sales;

                    List<dbo_CreditClass> listofitem = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, hdfUser_ID.Value); // Remove hdfUser_ID

                    if (!string.IsNullOrEmpty(replace_sale.Trim()))
                    {
                        List<dbo_CreditClass> listofitem_1 = dbo_CreditDataClass.Search(txtClearing_No.Text, string.Empty, null, string.Empty, replace_sale); // Remove hdfUser_ID
                        listofitem = listofitem.Union(listofitem_1).ToList();
                    }

                    GridViewCredit.DataSource = listofitem;
                    GridViewCredit.DataBind();

                    Show("บันทึกสำเร็จ!");
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                    if (Session["Create_Credit"] != null)
                    {

                        Session.Remove("Create_Credit");
                    }
                    btnCreditPaymentNext.Text = "ถัดไป";
                    return true;
                }
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุลูกค้า");
                return false;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            return false;
        }

    }

    public bool Add_PaymentCredit()
    {

        try
        {
            int current_index = (int)ViewState["RowIndexCreditPayment"];

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            dbo_CreditPaymentClass credit = new dbo_CreditPaymentClass();
            // credit.Payment_No = GenerateID.Payment_No(user_class.CV_CODE);

            GridView gv = (GridView)GridViewCreditPayment.Rows[current_index + 1].FindControl("GridViewCustomer");
            Label lblCredit_ID = (Label)GridViewCreditPayment.Rows[current_index].FindControl("lblCredit_ID");

            DropDownList _ddlFooterbank = (DropDownList)gv.FooterRow.FindControl("ddlFooterbank");
            TextBox _txtFooterCheque_Name = (TextBox)gv.FooterRow.FindControl("txtFooterCheque_Name");
            TextBox _txtFooterPaymentAmount = (TextBox)gv.FooterRow.FindControl("txtFooterPaymentAmount");
            CheckBox _chkFooterClearing_Cheque = (CheckBox)gv.FooterRow.FindControl("chkFooterClearing_Cheque");
            DropDownList _ddlFooterpaymentmethod = (DropDownList)gv.FooterRow.FindControl("ddlFooterpaymentmethod");
            TextBox _txtFooterPaymentDate = (TextBox)gv.FooterRow.FindControl("txtFooterPaymentDate");
            TextBox _txtFooterDate = (TextBox)gv.FooterRow.FindControl("txtFooterDate");



            if (string.IsNullOrEmpty(_txtFooterPaymentDate.Text) || string.IsNullOrEmpty(_txtFooterPaymentAmount.Text))
            {
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");

                return false;
            }
            else
            {
                credit.Payment_No = GenerateID.Payment_No(user_class.CV_CODE);
                credit.Bank = (_ddlFooterbank.SelectedIndex == 0 ? null : _ddlFooterbank.SelectedValue);
                credit.Credit_ID = lblCredit_ID.Text;
                credit.Payment_Amount = decimal.Parse(_txtFooterPaymentAmount.Text);
                credit.Payment_Method = _ddlFooterpaymentmethod.SelectedValue;
                credit.Payment_Date = (!string.IsNullOrEmpty(_txtFooterPaymentDate.Text) ? DateTime.Parse(_txtFooterPaymentDate.Text) : (DateTime?)null);
                credit.Date = (!string.IsNullOrEmpty(_txtFooterDate.Text) ? DateTime.Parse(_txtFooterDate.Text) : (DateTime?)null);
                credit.Cheque_No = _txtFooterCheque_Name.Text;


                if (!btnConfirmPaymentBackToGrid.Visible)
                {
                    credit.Clearing_No = txtClearing_No.Text;
                }


                dbo_CreditClass Prev_credit = dbo_CreditDataClass.Select_Record(lblCredit_ID.Text);

                Prev_credit.Total_Payment_Amount += credit.Payment_Amount;
                string Balance_Outstanding_Amount = Prev_credit.Balance_Outstanding_Amount.Value.ToString("#,##0.#0");
                string Balance_Outstanding_Amount_01 = Prev_credit.Balance_Outstanding_Amount.Value.ToString();
                Prev_credit.Balance_Outstanding_Amount = Prev_credit.Credit_Amount - Prev_credit.Total_Payment_Amount;



                if (Prev_credit.Balance_Outstanding_Amount >= 0)
                {
                    dbo_CreditPaymentDataClass.Add(credit);
                    if (Prev_credit.Balance_Outstanding_Amount == 0)
                    {
                        Prev_credit.Status = "2";

                    }

                    dbo_CreditDataClass.Update(Prev_credit);
                    gv.ShowFooter = false;
                    List<dbo_CreditPaymentClass> item_value = dbo_CreditPaymentDataClass.Search(lblCredit_ID.Text, string.Empty);
                    gv.DataSource = item_value;
                    gv.DataBind();

                    Show("บันทึกสำเร็จ!");
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);


                    if (Session["Create_PaymentCredit"] != null)
                    {

                        Session.Remove("Create_PaymentCredit");
                    }
                    // btnSearchCreditPayment_Click(sender, e);
                    btnSearchCreditPayment_Click(null, null);

                    ButtonSubsidy.Text = "ถัดไป";
                    return true;
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //Show("กรุณาระบุข้อมูลให้ถูกต้อง");
                    Show("ไม่สามารถระบุ จำนวนเงินมากกว่าจำนวนเงินค้างชำระ " + Balance_Outstanding_Amount + " บาทได้!");
                    _txtFooterPaymentAmount.Text = Balance_Outstanding_Amount_01;
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Debug(ex.Message);

            return false;
        }

    }

    public bool Add_Subsidy()
    {

        try
        {

            dbo_CreditClass credit = new dbo_CreditClass();
            DropDownList _ddlFooterDetail = (DropDownList)GridViewSubsidy.FooterRow.FindControl("ddlFooterDetail");
            TextBox _txtFooterSubsidy_Amount = (TextBox)GridViewSubsidy.FooterRow.FindControl("txtFooterSubsidy_Amount");

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            if (_ddlFooterDetail.SelectedIndex > 0)
            {
                if (string.IsNullOrEmpty(_txtFooterSubsidy_Amount.Text))
                {
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณาระบุจำนวนเงินช่วยเหลือ");

                    return false;
                }
                else
                {
                    dbo_SubsidyClass subsidy = new dbo_SubsidyClass();
                    subsidy.Clearing_No = txtClearing_No.Text;
                    subsidy.Subsidy_ID = GenerateID.Subsidy_ID(user_class.CV_CODE);
                    subsidy.Subsidy_Detail = _ddlFooterDetail.SelectedValue;
                    subsidy.Subsidy_Amount = decimal.Parse(_txtFooterSubsidy_Amount.Text);

                    dbo_SubsidyDataClass.Add(subsidy);

                    GridViewSubsidy.ShowFooter = false;
                    List<dbo_SubsidyClass> listofitem = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
                    GridViewSubsidy.DataSource = listofitem;
                    GridViewSubsidy.DataBind();

                    Show("บันทึกข้อมูลเงินช่วยเหลือ สำเร็จ!");
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                    if (Session["Create_Subsid"] != null)
                    {
                        Session.Remove("Create_Subsid");

                        //ButtonDeduct.Text = "ถัดไป";
                    }
                    if (Session["Create_Subsid"] == null && Session["Create_Deduct"] == null)
                    {
                        ButtonDeduct.Text = "ถัดไป";
                    }


                        return true;
                }
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุรายละเอียดเงินช่วยเหลือ");

                return false;
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);

            return false;
        }
    }

    public bool Add_Deduct()
    {
        try
        {
            DropDownList _ddlFooterDetail = (DropDownList)GridViewDeduct.FooterRow.FindControl("ddlFooterDetail");
            TextBox _txtFooterDeduct_Amount = (TextBox)GridViewDeduct.FooterRow.FindControl("txtFooterDeduct_Amount");

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            if (_ddlFooterDetail.SelectedIndex > 0)
            {

                if (string.IsNullOrEmpty(_txtFooterDeduct_Amount.Text))
                {
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณาระบุจำนวนเงินหักอื่นๆ");

                    return false;
                }
                else
                {

                    dbo_DeductClass deduct = new dbo_DeductClass();
                    deduct.Clearing_No = txtClearing_No.Text;
                    deduct.Deduct_ID = GenerateID.Deduct_ID(user_class.CV_CODE);
                    deduct.Deduct_Detail = _ddlFooterDetail.SelectedValue;
                    deduct.Deduct_Amount = decimal.Parse(_txtFooterDeduct_Amount.Text);

                    dbo_DeductDataClass.Add(deduct);

                    GridViewDeduct.ShowFooter = false;
                    List<dbo_DeductClass> listofitem = dbo_DeductDataClass.Search(txtClearing_No.Text);
                    GridViewDeduct.DataSource = listofitem;
                    GridViewDeduct.DataBind();

                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                    Show("บันทึกข้อมูลเงินหักอื่นๆ สำเร็จ!");

                    if(Session["Create_Deduct"] != null)
                    {
                        Session.Remove("Create_Deduct");
                    }

                    if (Session["Create_Subsid"] == null && Session["Create_Deduct"] == null)
                    {
                        ButtonDeduct.Text = "ถัดไป";
                    }


                    return true;
                }

            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุรายละเอียดเงินหักอื่นๆ");

                return false;
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);

            return false;
        }
    }

    #endregion

}