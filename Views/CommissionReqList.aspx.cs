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

public partial class Views_CommissionReqList : System.Web.UI.Page
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
                // string id = GenerateID.Commission_requisition_no("201567");
                txtRequisition_Amount.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                txtRequisition_Amount.Attributes.Add("ondrop", "javascript:return false;");
                txtRequisition_Amount.Attributes.Add("onpaste", "javascript:return false;");

                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                if (user_class.User_Group_ID != "Agent")
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);
                }
                else
                {
                    SetUpDrowDownList();
                    //
                    btnSearchSubmit_Click(sender, e);
                }

            }
            txtSearchClearing_Date_Begin.Text = DateTime.Now.ToShortDateString();
            txtSearchClearing_Date_End.Text = DateTime.Now.ToShortDateString();
            txtRequistionStart.Text = DateTime.Now.ToShortDateString();
            txtRequistionEnd.Text = DateTime.Now.ToShortDateString();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);


            string user_id = ddlSearchSP.SelectedIndex == 0 ? "" : ddlSearchSP.SelectedValue;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(HttpContext.Current.Request.Cookies["User_ID"].Value);

            DateTime? begin = null;
            DateTime? end = null;

            if (!string.IsNullOrEmpty(txtSearchClearing_Date_Begin.Text))
            {
                begin = DateTime.Parse(txtSearchClearing_Date_Begin.Text);
            }
            if (!string.IsNullOrEmpty(txtSearchClearing_Date_End.Text))
            {
                end = DateTime.Parse(txtSearchClearing_Date_End.Text);
            }

            DateTime? RequisitionBegin = null;
            DateTime? RequisitionEnd = null;

            if (!string.IsNullOrEmpty(txtRequistionStart.Text))
            {
                RequisitionBegin = DateTime.Parse(txtRequistionStart.Text);
            }
            if (!string.IsNullOrEmpty(txtRequistionEnd.Text))
            {
                RequisitionEnd = DateTime.Parse(txtRequistionEnd.Text);
            }

            string status = ddlStatus.SelectedIndex == 0 ? string.Empty : ddlStatus.SelectedValue;

            List<dbo_ClearingClass> item = dbo_ClearingDataClass.ClearingCommissionSearch(txtClearing_No.Text, begin, end, RequisitionBegin, RequisitionEnd, user_id, txtRequisition_No.Text, status, user_class.CV_CODE);
            Session["item"] = item;

            if (item.Count > 0)
            {
                grdCommission.Visible = true;
                grdCommission.DataSource = item;
                grdCommission.DataBind();

                string prev_clearing_no = string.Empty;

                for (int i = grdCommission.Rows.Count - 1; i >= 0; i--)
                {
                    GridViewRow row = grdCommission.Rows[i];

                    Label txt = (Label)row.FindControl("lblClearing_No");
                    Label amount = (Label)row.FindControl("lblCommissionAmount");

                    if (prev_clearing_no != txt.Text)
                    {
                        amount.Visible = true;
                    }
                    else
                    {
                        amount.Visible = false;
                    }


                    prev_clearing_no = txt.Text;
                }
                pnlNoRec.Visible = false;
            }
            else
            {
                pnlNoRec.Visible = true;
                grdCommission.Visible = false;
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
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            
            SetUpDrowDownList();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {

            ddl_SPName.ClearSelection();
            txtClearing_Begin.Text = string.Empty;
            txtClearing_End.Text = string.Empty;

            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            pnlForm.Visible = true;
            pnlGrid.Visible = false;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                        , string.Empty, string.Empty, string.Empty, user_class.CV_CODE, null, string.Empty, string.Empty)
                        .Where(f => f.Status == "Active")
                        .Where(f => f.Position == "ซุปเปอร์ไวซ์เซอร์" || f.Position == "สาวส่งนม").ToList();


            ddl_SPName.DataSource = users;
            ddl_SPName.DataBind();
            ddl_SPName.Items.Insert(0, new ListItem("==ระบุ==", "0"));
            ddl_SPName.ClearSelection();

            txtClearing_Begin.Text = string.Empty;
            txtClearing_End.Text = string.Empty;

            txtTotalBalanceOutstanding.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            txtRequisition_Amount.Text = string.Empty;

            List<dbo_ClearingClass> item = new List<dbo_ClearingClass>();
            grdCommissionRequisition.DataSource = item;
            grdCommissionRequisition.DataBind();


            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    protected void btnSearch_2_Submit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            DateTime? begin = null;
            DateTime? end = null;

            if (!string.IsNullOrEmpty(txtClearing_Begin.Text))
            {
                begin = DateTime.Parse(txtClearing_Begin.Text);
            }
            if (!string.IsNullOrEmpty(txtClearing_End.Text))
            {
                end = DateTime.Parse(txtClearing_End.Text);
            }

            string user_id = ddl_SPName.SelectedIndex == 0 ? "" : ddl_SPName.SelectedValue;


            List<dbo_DebtClass> listofdebt = dbo_DebtDataClass.Search(user_id == string.Empty ? "--" : user_id, string.Empty);

            decimal? debt = 0;
            foreach (dbo_DebtClass debt_item in listofdebt)
            {
                debt += debt_item.Balance_Outstanding_Amount;
            }


            txtTotalBalanceOutstanding.Text = (debt == 0 ? string.Empty : debt.Value.ToString("#,##0.#0"));
            txtTotalAmount.Text = string.Empty;
            txtRequisition_Amount.Text = string.Empty;

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            List<dbo_ClearingClass> item = dbo_ClearingDataClass.Get_Commission(txtClearing_No.Text, begin, end, user_id, user_class.CV_CODE);
            Session["item_commission"] = item;

            grdCommissionRequisition.DataSource = item;
            grdCommissionRequisition.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnSearch_2_Cancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        btnAddNew_Click(null, null);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSaveSubmit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            if (txtRequisition_Amount.Text == string.Empty)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุจำนวนเงิน");
            }
            //else if (Convert.ToDecimal(txtRequisition_Amount.Text) > Convert.ToDecimal(txtTotalBalanceOutstanding.Text))
            //{
            //    System.Threading.Thread.Sleep(500);
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            //    Show("ไม่สามารถเบิกค่าคอมมิชชั่นเกินยอดหนี้คงค้างได้");
            //}
            else
                
            {

                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(HttpContext.Current.Request.Cookies["User_ID"].Value);

                dbo_CommissionRequisitionClass com_req = new dbo_CommissionRequisitionClass();
                com_req.Commission_requisition_no = GenerateID.Commission_requisition_no(user_class.CV_CODE);
                com_req.Commission_Requisition_Date = DateTime.Now;

                com_req.Requisition_Amount = decimal.Parse(string.IsNullOrEmpty(txtRequisition_Amount.Text) ? "0" : txtRequisition_Amount.Text);

                // string user_id = ddl_SPName.SelectedIndex == 0 ? string.Empty : ddl_SPName.SelectedValue;
                //List<dbo_DebtClass> listofdebt = dbo_DebtDataClass.Search(user_id, string.Empty);


                int index = 1;

                // decimal init_debt = decimal.Parse(txtTotalBalanceOutstanding.Text);

                // decimal? check_init = 0;


                decimal? cal_com = decimal.Parse(txtRequisition_Amount.Text);
                HiddenField hdf = new HiddenField();
                hdf.Value = string.Empty;
                string user_id = string.Empty;
                foreach (GridViewRow currentRow in grdCommissionRequisition.Rows)
                {

                    if (cal_com > 0)
                    {

                        CheckBox chk = (CheckBox)currentRow.FindControl("chkAll");
                        if (chk.Checked)
                        {

                            Label lblClearing_No = (Label)currentRow.FindControl("lblClearing_No");
                            logger.Debug("lblClearing_No.Text " + lblClearing_No.Text);
                            Label lblCommissionAmount = (Label)currentRow.FindControl("lblCommissionAmount");
                            logger.Debug("lblCommissionAmount.Text " + lblCommissionAmount.Text);
                            logger.Debug("cal_com " + cal_com);


                            hdf = (HiddenField)currentRow.FindControl("hdfUser_ID");

                            if (!string.IsNullOrEmpty(lblClearing_No.Text))
                            {

                                List<dbo_RequisitionClearingClass> rc = dbo_RequisitionClearingDataClass.Search(lblClearing_No.Text, string.Empty,
                                    string.Empty, null, null, null, null, user_class.CV_CODE);


                                //decimal? to_req = decimal.Parse(lblCommissionAmount.Text);

                                decimal? to_req = cal_com;

                                foreach (dbo_RequisitionClearingClass rc_ in rc)
                                {
                                    logger.Debug(rc_.Requisition_No);

                                    List<dbo_CommissionClass> com = dbo_CommissionDataClass.Select_Record(rc_.Requisition_No);

                                    logger.Debug("** " + com[0].Commission_Balance_Outstanding + " , " + to_req + " **");
                                    logger.Debug("com[0].Commission_Balance_Outstanding <= to_req");

                                    if (com[0].Commission_Balance_Outstanding <= to_req)
                                    {
                                        dbo_CommissionRequisitionDtlClass detail = new dbo_CommissionRequisitionDtlClass();

                                        detail.Clearing_No = lblClearing_No.Text;
                                        detail.Commission = decimal.Parse(lblCommissionAmount.Text);
                                        detail.Requisition_Amount = com[0].Commission_Balance_Outstanding;
                                        detail.Commission_requisition_no = com_req.Commission_requisition_no + index.ToString("00");
                                        index++;

                                        dbo_CommissionRequisitionDtlDataClass.Add(detail, HttpContext.Current.Request.Cookies["User_ID"].Value);



                                        if (com.Count > 0)
                                        {
                                            com[0].Commission_Requisition_Status = 3;
                                            logger.Debug("com[0].Commission_Requisition_Status " + com[0].Commission_Requisition_Status);
                                            to_req = to_req - com[0].Commission_Balance_Outstanding;
                                            logger.Debug("to_req update " + to_req);
                                            com[0].Commission_Balance_Outstanding = 0;
                                            logger.Debug("com[0].Commission_Balance_Outstanding " + com[0].Commission_Balance_Outstanding);
                                            dbo_CommissionDataClass.Update(com[0], HttpContext.Current.Request.Cookies["User_ID"].Value);
                                          //  break;
                                        }

                                    }
                                    else
                                    {
                                        dbo_CommissionRequisitionDtlClass detail = new dbo_CommissionRequisitionDtlClass();
                                        detail.Clearing_No = lblClearing_No.Text;
                                        detail.Commission = decimal.Parse(lblCommissionAmount.Text);
                                        detail.Requisition_Amount = to_req;
                                        detail.Commission_requisition_no = com_req.Commission_requisition_no + index.ToString("00");
                                        index++;
                                        dbo_CommissionRequisitionDtlDataClass.Add(detail, HttpContext.Current.Request.Cookies["User_ID"].Value);

                                        if (com.Count > 0)
                                        {
                                            com[0].Commission_Requisition_Status = 2;
                                            logger.Debug("com[0].Commission_Requisition_Status " + com[0].Commission_Requisition_Status);
                                            to_req = com[0].Commission_Balance_Outstanding - to_req;
                                            logger.Debug("to_req update " + to_req);

                                            // com[0].Commission_Balance_Outstanding = detail.Commission - detail.Requisition_Amount;
                                            com[0].Commission_Balance_Outstanding = com[0].Commission_Balance_Outstanding - detail.Requisition_Amount;
                                            logger.Debug("com[0].Commission_Balance_Outstanding " + com[0].Commission_Balance_Outstanding);
                                            dbo_CommissionDataClass.Update(com[0], HttpContext.Current.Request.Cookies["User_ID"].Value);
                                            break;
                                        }


                                    }
                                }
                            }


                            cal_com = cal_com - decimal.Parse(lblCommissionAmount.Text);
                        }

                    }

                }

                dbo_CommissionRequisitionClass total = dbo_ClearingDataClass.Get_Commission_Balance_Outstanding(hdf.Value);

                com_req.Total_Balance_Outstanding = total.Total_Balance_Outstanding;
                logger.Debug("com_req.Total_Balance_Outstanding " + com_req.Total_Balance_Outstanding);
                com_req.Commission_Balance_Outstanding = total.Commission_Balance_Outstanding;
                logger.Debug("com_req.Commission_Balance_Outstanding " + com_req.Commission_Balance_Outstanding);
                com_req.Total_Credit_Amount = total.Total_Credit_Amount;
                logger.Debug("com_req.Total_Credit_Amount " + com_req.Total_Credit_Amount);
                dbo_CommissionRequisitionDataClass.Add(com_req, HttpContext.Current.Request.Cookies["User_ID"].Value);

                //   Show("บันทึกสำเร็จ");

                string Post_No = GenerateID.Post_No(user_class.CV_CODE);
                dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                rev.Post_No = Post_No;
                rev.CV_Code = user_class.CV_CODE;
                rev.Amount = decimal.Parse(txtRequisition_Amount.Text);
                rev.Account_Code = "5071";
                rev.Account_No = GenerateID.EP(user_class.CV_CODE);
                rev.Remark = "";
                rev.Post_Date = DateTime.Now;
                rev.User_ID = user_id;
                dbo_RevenueExpenseDataClass.AddSP(rev);

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                string url = "../Report_From/ViewsReport.aspx?RPT=CommissionReqList&PRM=" + com_req.Commission_requisition_no;
                string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);


                btnSearch_2_Submit_Click(null, null);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void btnSaveCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SetUpDrowDownList();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        decimal sum = 0;
        foreach (GridViewRow currentRow in grdCommission.Rows)
        {

            CheckBox chk = (CheckBox)currentRow.FindControl("chkAll");
            if (chk.Checked)
            {
                Label lblCommissionAmount = (Label)currentRow.FindControl("lblCommissionAmount");
                if (lblCommissionAmount.Visible)
                {
                    sum += decimal.Parse(lblCommissionAmount.Text);
                }
            }

        }

        txtSum.Text = sum.ToString();
        //txtSum.Text = sum.ToString("#,##0.#0");
        
    }

    protected void chkAll_CheckedChanged1(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        bool foundCheck = false;

        try
        {

            decimal sum = 0;

            foreach (GridViewRow currentRow in grdCommissionRequisition.Rows)
            {
                CheckBox chk = (CheckBox)currentRow.FindControl("chkAll");
                if (chk.Checked)
                {
                    Label lblSPName = (Label)currentRow.FindControl("lblSPName");
                    Label lblCommissionAmount = (Label)currentRow.FindControl("lblCommissionAmount");

                    if (!foundCheck)
                    {
                        hdfChkUser_ID.Value = lblSPName.Text;
                        foundCheck = true;
                        Label lblUser_ID = (Label)currentRow.FindControl("lblUser_ID");

                        List<dbo_DebtClass> listofdebt = dbo_DebtDataClass.Search(lblUser_ID.Text == string.Empty ? "--" : lblUser_ID.Text, string.Empty);

                        decimal? debt = 0;
                        foreach (dbo_DebtClass debt_item in listofdebt)
                        {
                            debt += debt_item.Balance_Outstanding_Amount;
                        }

                        txtTotalBalanceOutstanding.Text = (debt == 0 ? "0" : debt.Value.ToString("#,##0.#0"));


                        sum += decimal.Parse(lblCommissionAmount.Text);


                        //List<dbo_ClearingClass> item = dbo_ClearingDataClass.Get_Commission(txtClearing_No.Text, null, null, string.Empty);

                        //txtTotalAmount.Text = item.Where(f => f.User_ID == lblUser_ID.Text).Sum(f => f.Commission).Value.ToString("#,##0.#0");

                    }
                    else
                    {
                        if (hdfChkUser_ID.Value != lblSPName.Text)
                        {
                            Show("กรุณาระบุข้อมูลให้ถูกต้อง");
                            chk.Checked = false;


                            txtTotalAmount.Text = string.Empty;
                            txtTotalBalanceOutstanding.Text = string.Empty;

                            break;
                        }
                        else
                        {
                            sum += decimal.Parse(lblCommissionAmount.Text);
                        }
                    }
                }
            }

            txtTotalAmount.Text = sum.ToString("#,##0.#0");

            if (!foundCheck)
            {
                hdfChkUser_ID.Value = string.Empty;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion

    #region GridView Events
    protected void grdCommission_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Requisition_No = lnkView.CommandArgument;
            //int Index = int.Parse(e.CommandArgument.ToString());
            //GridViewRow currentRow = grdCommission.Rows[Index];

            //LinkButton lnkView = (LinkButton)currentRow.FindControl("lnkRequisition_No");


            string url = "../Report_From/ViewsReport.aspx?RPT=CommissionReqList&PRM=" + Requisition_No;
            string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

            //string url1 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Clearing_No&PRM=" + lnkView.Text;
            //string s1 = "window.open('" + url1 + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);

        }
    }

    protected void grdCommission_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdCommission.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        grdCommission.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void grdCommission_DataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ClearingClass> item = (List<dbo_ClearingClass>)Session["item"];

        Session.Remove("item");

        try
        {
            for (int i = 0; i < item.Count; i++)
            {
                if (item[i].Clearing_No == "Merge")
                {
                    GridViewRow row = grdCommission.Rows[i];

                    Label txt = (Label)row.FindControl("lblSPName");

                    Decimal balance = item.Where(g => g.User_ID == item[i].User_ID)
                        .GroupBy(p => p.Clearing_No)
                        .Select(g => g.Last()).ToList().Sum(f => f.Commission).Value;

                    txt.Text = string.Format("ค่าคอมมิชชั่นคงเหลือ : " + balance);

                    row.Cells[0].Visible = false;
                    row.Cells[1].ColumnSpan = 8;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;

                }
            }
            txtSum.Text = string.Empty;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        // Retrieve the pager row.
        GridViewRow pagerRow = grdCommission.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < grdCommission.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem itemPage = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == grdCommission.PageIndex)
                {
                    itemPage.Selected = true;
                }

                // Add the ListItem object to the Items collection of the 
                // DropDownList.
                pageList.Items.Add(itemPage);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = grdCommission.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + grdCommission.PageCount.ToString();

        }

    }

    protected void grdCommissionRequisition_DataBound(object sender, EventArgs e)
    {
        try
        {

            List<dbo_ClearingClass> item = (List<dbo_ClearingClass>)Session["item_commission"];
            if (item != null)
            {



                Session.Remove("item_commission");


                for (int i = 0; i < item.Count; i++)
                {
                    if (item[i].Clearing_No == "Merge")
                    {
                        GridViewRow row = grdCommissionRequisition.Rows[i];

                        Label txt = (Label)row.FindControl("lblSPName");

                        txt.Text = "ค่าคอมมิชชั่นคงเหลือ : " + item.Where(f => f.User_ID == item[i].User_ID).Sum(f => f.Commission_Balance_Outstanding);


                        row.Cells[0].Visible = false;
                        row.Cells[1].ColumnSpan = 6;
                        row.Cells[2].Visible = false;
                        row.Cells[3].Visible = false;
                        row.Cells[4].Visible = false;
                        row.Cells[5].Visible = false;
                        //row.Cells[6].Visible = false;
                        //row.Cells[7].Visible = false;

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

    private void SetUpDrowDownList()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

            ViewState["CV_Code"] = clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code;


            pnlForm.Visible = false;
            pnlGrid.Visible = true;

            List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
           , string.Empty, string.Empty, string.Empty, clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code, null, string.Empty, string.Empty)
            .Where(f => f.Status == "Active")
            .Where(f => f.Position == "ซุปเปอร์ไวซ์เซอร์" || f.Position == "สาวส่งนม").ToList();


            users.Insert(0, new dbo_UserClass() { FullName = "==ระบุ==", User_ID = string.Empty });
            ddlSearchSP.DataSource = users;
            ddlSearchSP.DataBind();

            txtSum.Text = string.Empty;
            if (grdCommission.Rows.Count > 0)
            {
                List<dbo_ClearingClass> item = new List<dbo_ClearingClass>();
                grdCommission.DataSource = item;
                grdCommission.DataBind();
            }

            ddlStatus.SelectedIndex = 0;
            txtClearing_No.Text = string.Empty;
            txtRequisition_No.Text = string.Empty;
            txtSearchClearing_Date_Begin.Text = DateTime.Now.ToShortDateString();
            txtSearchClearing_Date_End.Text = DateTime.Now.ToShortDateString();
            txtRequistionStart.Text = DateTime.Now.ToShortDateString();
            txtRequistionEnd.Text = DateTime.Now.ToShortDateString();


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }
    #endregion
}