#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_Revenue_Expense : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
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

            btnSearchSubmit_Click(sender, e);
        }
        txtStartDate.Text = DateTime.Now.ToShortDateString();
        txtEndDate.Text = DateTime.Now.ToShortDateString();
    }

    protected void ButtonAddNew_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);


        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            string Post_No = GenerateID.Post_No(user_class.CV_CODE);

            List<dbo_RevenueExpenseClass> item_rev = dbo_RevenueExpenseDataClass.Search(Post_No, null, null, user_class.CV_CODE);

            if (item_rev.Count == 0)
            {
                pnlForm.Visible = true;
                pnlGrid.Visible = false;

                Textbox1.Text = "0";
                Textbox2.Text = "0";
                txtPost_Date.Text = DateTime.Now.ToShortDateString();
                txtPost_No.Text = GenerateID.Post_No(user_class.CV_CODE);

                List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, null, null, user_class.CV_CODE);

                List<dbo_RevenueExpenseClass> item_rv = item.Where(f => f.Account_No.Substring(6, 2) == "RV").ToList(); //รายรับ
                List<dbo_RevenueExpenseClass> item_ep = item.Where(f => f.Account_No.Substring(6, 2) == "EP").ToList(); //รายจ่าย

                GridViewRevenue.DataSource = item_rv;
                GridViewRevenue.DataBind();

                GridViewExpense.DataSource = item_ep;
                GridViewExpense.DataBind();                

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("คุณได้ทำการบันทึกรายรับรายจ่ายของวันนี้แล้ว กรุณากลับไปแก้ไขรายการเดิม");
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            DateTime? Begin = null;
            DateTime? End = null;


            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                Begin = DateTime.Parse(txtStartDate.Text);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                End = DateTime.Parse(txtEndDate.Text);
            }

            GridViewRevenue.ShowFooter = true;
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, Begin, End, user_class.CV_CODE);

            item = item.Where(f => f.Account_No.Substring(6, 2) == "RV").ToList();

            if (item.Count == 0)
            {
                item.Add(new dbo_RevenueExpenseClass());
                GridViewRevenue.DataSource = item;
                GridViewRevenue.DataBind();
                GridViewRevenue.Rows[0].Visible = false;
            }
            else
            {
                GridViewRevenue.DataSource = item;
                GridViewRevenue.DataBind();
            }

            DropDownList ddl = (DropDownList)GridViewRevenue.FooterRow.FindControl("ddlFooterDetail");

            List<dbo_AccountCodeClass> account = dbo_AccountTypeDataClass.GetAccountRevenue();

            ddl.DataSource = account;
            ddl.DataTextField = "Account_Name";
            ddl.DataValueField = "Account_Code";
            ddl.DataBind();

            TextBox txtFooterRevenue_Amount = (TextBox)GridViewRevenue.FooterRow.FindControl("txtFooterRevenue_Amount");
            txtFooterRevenue_Amount.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");           

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void btnAddNewExpense_Click(object sender, EventArgs e)
    {
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            DateTime? Begin = null;
            DateTime? End = null;


            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                Begin = DateTime.Parse(txtStartDate.Text);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                End = DateTime.Parse(txtEndDate.Text);
            }

            GridViewExpense.ShowFooter = true;
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, Begin, End, user_class.CV_CODE);

            item = item.Where(f => f.Account_No.Substring(6, 2) == "EP").ToList();

            if (item.Count == 0)
            {
                item.Add(new dbo_RevenueExpenseClass());
                GridViewExpense.DataSource = item;
                GridViewExpense.DataBind();
                GridViewExpense.Rows[0].Visible = false;
            }
            else
            {
                GridViewExpense.DataSource = item;
                GridViewExpense.DataBind();
            }

            DropDownList ddl = (DropDownList)GridViewExpense.FooterRow.FindControl("ddlFooterDetail");

            List<dbo_AccountCodeClass> account = dbo_AccountTypeDataClass.GetAccountExpense();

            ddl.DataSource = account;
            ddl.DataTextField = "Account_Name";
            ddl.DataValueField = "Account_Code";
            ddl.DataBind();

            TextBox txtFooter_Amount = (TextBox)GridViewExpense.FooterRow.FindControl("txtFooter_Amount");
            txtFooter_Amount.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");           

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

        try
        {
            DateTime? Post_Date_Begin = null;
            DateTime? Post_Date_End = null;
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                Post_Date_Begin = DateTime.Parse(txtStartDate.Text);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                Post_Date_End = DateTime.Parse(txtEndDate.Text);
            }
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.GetRevenueExpense(Post_Date_Begin, Post_Date_End, user_class.CV_CODE);

            if (item.Count > 0)
            {
                GridViewRevenue_Expense.DataSource = item;
                GridViewRevenue_Expense.DataBind();

                GridViewRevenue_Expense.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewRevenue_Expense.Visible = false;
                pnlNoRec.Visible = true;
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        txtStartDate.Text = DateTime.Now.ToShortDateString();
        txtEndDate.Text = DateTime.Now.ToShortDateString();
        if (GridViewRevenue_Expense.Rows.Count > 0)
        {
            List<dbo_RevenueExpenseClass> itm = new List<dbo_RevenueExpenseClass>();
            GridViewRevenue_Expense.DataSource = itm;
            GridViewRevenue_Expense.DataBind();
        }
        

        GridViewRevenue_Expense.Visible = false;
        pnlNoRec.Visible = false;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnBackToGrid_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlForm.Visible = false;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
    #endregion    

    #region Method
    private void InitialRevenue()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            GridViewRevenue.EditIndex = -1;
            GridViewRevenue.ShowFooter = false;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            DateTime? Begin = null;
            if (!string.IsNullOrEmpty(txtPost_Date.Text))
            {
                Begin = DateTime.Parse(txtPost_Date.Text);
            }
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, Begin, null, user_class.CV_CODE);

            item = item.Where(f => f.Account_No.Substring(6, 2) == "RV").ToList();

            GridViewRevenue.DataSource = item;
            GridViewRevenue.DataBind();

            Textbox1.Text = item.Sum(f => f.Amount).Value.ToString("#,##0.##");
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    private void InitialExpense()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            GridViewExpense.EditIndex = -1;
            GridViewExpense.ShowFooter = false;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            DateTime? Begin = null;
            if (!string.IsNullOrEmpty(txtPost_Date.Text))
            {
                Begin = DateTime.Parse(txtPost_Date.Text);
            }
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, Begin, null, user_class.CV_CODE);

            item = item.Where(f => f.Account_No.Substring(6, 2) == "EP").ToList();

            GridViewExpense.DataSource = item;
            GridViewExpense.DataBind();

            Textbox2.Text = item.Sum(f => f.Amount).Value.ToString("#,##0.##");

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
    #endregion

    #region GirdView Events
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewRevenue_Expense.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewRevenue_Expense.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewRevenue_Expense_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewRevenue_Expense.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewRevenue_Expense.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewRevenue_Expense.PageIndex)
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
            int currentPage = GridViewRevenue_Expense.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewRevenue_Expense.PageCount.ToString();

        }
    }

    protected void GridViewRevenue_Expense_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (e.CommandName == "_Delete")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Post_No = lnkView.CommandArgument;

            dbo_RevenueExpenseDataClass.DeletebyPostNo(Post_No);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");
            btnSearchSubmit_Click(null, null);
        }
        else if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Post_No = lnkView.CommandArgument;

            pnlForm.Visible = true;
            pnlGrid.Visible = false;

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(Post_No, null, null, user_class.CV_CODE);

            List<dbo_RevenueExpenseClass> item_rv = item.Where(f => f.Account_No.Substring(6, 2) == "RV").ToList();
            List<dbo_RevenueExpenseClass> item_ep = item.Where(f => f.Account_No.Substring(6, 2) == "EP").ToList();

            dbo_RevenueExpenseClass it = item.FirstOrDefault();
            txtPost_Date.Text = Convert.ToDateTime(it.Post_Date).ToString("dd/MM/yyyy");
            //txtPost_Date.Text = Convert.ToString(it.Post_Date);
            txtPost_No.Text = Post_No;
            Textbox1.Text = item_rv.Sum(f => f.Amount).Value.ToString("#,##0.##");
            Textbox2.Text = item_ep.Sum(f => f.Amount).Value.ToString("#,##0.##");

            GridViewRevenue.DataSource = item_rv;
            GridViewRevenue.DataBind();

            GridViewExpense.DataSource = item_ep;
            GridViewExpense.DataBind();
        }

    }

    // Revenue

    protected void GridViewRevenue_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            DateTime? Begin = null;

            if (!string.IsNullOrEmpty(txtPost_Date.Text))
            {
                Begin = DateTime.Parse(txtPost_Date.Text);
            }
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, Begin, null, user_class.CV_CODE);

            item = item.Where(f => f.Account_No.Substring(6, 2) == "RV").ToList();

            GridViewRevenue.ShowFooter = false;
            GridViewRevenue.EditIndex = -1;
            GridViewRevenue.DataSource = item;
            GridViewRevenue.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        /*
        List<dbo_SubsidyClass> list_subsidy = dbo_SubsidyDataClass.Search(txtClearing_No.Text);
        GridViewSubsidy.ShowFooter = false;
        GridViewSubsidy.DataSource = list_subsidy;
        GridViewSubsidy.EditIndex = -1;
        GridViewSubsidy.DataBind();
        */
    }

    protected void GridViewRevenue_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            DateTime? Begin = null;
            if (!string.IsNullOrEmpty(txtPost_Date.Text))
            {
                Begin = DateTime.Parse(txtPost_Date.Text);
            }
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, Begin, null, user_class.CV_CODE);

            item = item.Where(f => f.Account_No.Substring(6, 2) == "RV").ToList();

            GridViewRevenue.ShowFooter = false;
            GridViewRevenue.EditIndex = e.NewEditIndex;
            GridViewRevenue.DataSource = item;
            GridViewRevenue.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewRevenue_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int RowIndex = Convert.ToInt32((e.RowIndex).ToString());
            Label _Account_No = (Label)GridViewRevenue.Rows[RowIndex].FindControl("lblAccount_No");

            DropDownList _ddlEditDetail = (DropDownList)GridViewRevenue.Rows[RowIndex].FindControl("ddlEditDetail");
            TextBox _txtEditRevenue_Amount = (TextBox)GridViewRevenue.Rows[RowIndex].FindControl("txtEditRevenue_Amount");
            TextBox _txtEditRemark = (TextBox)GridViewRevenue.Rows[RowIndex].FindControl("txtEditRemark");
            dbo_RevenueExpenseClass st = new dbo_RevenueExpenseClass();
            st = dbo_RevenueExpenseDataClass.Select_Record(_Account_No.Text);

            st.Amount = _txtEditRevenue_Amount.Text == "" ? 0 : Decimal.Parse(_txtEditRevenue_Amount.Text.Replace(",", string.Empty));
            st.Remark = _txtEditRemark.Text;

            bool success = false;

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            success = dbo_RevenueExpenseDataClass.Update(st);


            if (success)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("บันทึกสำเร็จ!");
            }

            InitialRevenue();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewRevenue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && GridViewRevenue.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddlEditDetail = (DropDownList)e.Row.FindControl("ddlEditDetail");

            List<dbo_AccountCodeClass> account = dbo_AccountTypeDataClass.GetAccountRevenue();
            ddlEditDetail.DataSource = account;
            ddlEditDetail.DataTextField = "Account_Name";
            ddlEditDetail.DataValueField = "Account_Code";
            ddlEditDetail.DataBind();
            ddlEditDetail.Items.FindByText((e.Row.FindControl("lblItemAccount_Code") as Label).Text).Selected = true;
        }
    }
    
    protected void GridViewRevenue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (e.CommandName == "AddNew")
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            try
            {
                dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                rev.Post_No = txtPost_No.Text;//GenerateID.Post_No(user_class.CV_CODE);
                rev.CV_Code = user_class.CV_CODE;
                rev.Post_Date = DateTime.Now;

                TextBox txtFooterRevenue_Amount = (TextBox)GridViewRevenue.FooterRow.FindControl("txtFooterRevenue_Amount");
                rev.Amount = txtFooterRevenue_Amount.Text == "" ? 0 : decimal.Parse(txtFooterRevenue_Amount.Text);
                DropDownList _ddlFooterDetail = (DropDownList)GridViewRevenue.FooterRow.FindControl("ddlFooterDetail");
                rev.Account_Code = _ddlFooterDetail.SelectedValue;
                rev.Account_No = GenerateID.RV(user_class.CV_CODE);
                TextBox txtFooterRemark = (TextBox)GridViewRevenue.FooterRow.FindControl("txtFooterRemark");
                rev.Remark = txtFooterRemark.Text;

                bool succes = false;
                succes = dbo_RevenueExpenseDataClass.Add(rev);

                if (succes)
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("บันทึกสำเร็จ!");
                }

                GridViewRevenue.ShowFooter = false;

                InitialRevenue();
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        else if (e.CommandName == "_Delete")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Account_No = lnkView.CommandArgument;

            dbo_RevenueExpenseDataClass.Delete(Account_No);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");

            InitialRevenue();
        }
    }

    // Expense

    protected void GridViewExpense_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);


        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            DateTime? Begin = null;

            if (!string.IsNullOrEmpty(txtPost_Date.Text))
            {
                Begin = DateTime.Parse(txtPost_Date.Text);
            }
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, Begin, null, user_class.CV_CODE);

            item = item.Where(f => f.Account_No.Substring(6, 2) == "EP").ToList();

            GridViewExpense.ShowFooter = false;
            GridViewExpense.EditIndex = -1;
            GridViewExpense.DataSource = item;
            GridViewExpense.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }   

    protected void GridViewExpense_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            DateTime? Begin = null;
            if (!string.IsNullOrEmpty(txtPost_Date.Text))
            {
                Begin = DateTime.Parse(txtPost_Date.Text);
            }
            List<dbo_RevenueExpenseClass> item = dbo_RevenueExpenseDataClass.Search(txtPost_No.Text, Begin, null, user_class.CV_CODE);

            item = item.Where(f => f.Account_No.Substring(6, 2) == "EP").ToList();

            GridViewExpense.ShowFooter = false;
            GridViewExpense.EditIndex = e.NewEditIndex;
            GridViewExpense.DataSource = item;
            GridViewExpense.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewExpense_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int RowIndex = Convert.ToInt32((e.RowIndex).ToString());
            Label _Account_No = (Label)GridViewExpense.Rows[RowIndex].FindControl("lblAccount_No");

            DropDownList _ddlEditDetail = (DropDownList)GridViewExpense.Rows[RowIndex].FindControl("ddlEditDetail");
            TextBox _txtEditRevenue_Amount = (TextBox)GridViewExpense.Rows[RowIndex].FindControl("txtEdit_Amount");
            TextBox _txtEditRemark = (TextBox)GridViewExpense.Rows[RowIndex].FindControl("txtEditRemark");
            dbo_RevenueExpenseClass st = new dbo_RevenueExpenseClass();
            st = dbo_RevenueExpenseDataClass.Select_Record(_Account_No.Text);

            st.Amount = _txtEditRevenue_Amount.Text == "" ? 0 : Decimal.Parse(_txtEditRevenue_Amount.Text.Replace(",", string.Empty));
            st.Remark = _txtEditRemark.Text;

            bool success = false;

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            success = dbo_RevenueExpenseDataClass.Update(st);


            if (success)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("บันทึกสำเร็จ!");
            }

            InitialExpense();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && GridViewExpense.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddlEditDetail = (DropDownList)e.Row.FindControl("ddlEditDetail");

            List<dbo_AccountCodeClass> account = dbo_AccountTypeDataClass.GetAccountExpense();
            ddlEditDetail.DataSource = account;
            ddlEditDetail.DataTextField = "Account_Name";
            ddlEditDetail.DataValueField = "Account_Code";
            ddlEditDetail.DataBind();
            ddlEditDetail.Items.FindByText((e.Row.FindControl("lblItemAccount_Code") as Label).Text).Selected = true;
        }        
    }      

    protected void GridViewExpense_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (e.CommandName == "AddNew")
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            try
            {
                dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                rev.Post_No = txtPost_No.Text;// GenerateID.Post_No(user_class.CV_CODE);
                rev.CV_Code = user_class.CV_CODE;
                rev.Post_Date = DateTime.Now;

                TextBox txtFooterRevenue_Amount = (TextBox)GridViewExpense.FooterRow.FindControl("txtFooter_Amount");
                rev.Amount = txtFooterRevenue_Amount.Text == "" ? 0 : decimal.Parse(txtFooterRevenue_Amount.Text);
                DropDownList _ddlFooterDetail = (DropDownList)GridViewExpense.FooterRow.FindControl("ddlFooterDetail");
                rev.Account_Code = _ddlFooterDetail.SelectedValue;
                rev.Account_No = GenerateID.EP(user_class.CV_CODE);
                TextBox txtFooterRemark = (TextBox)GridViewExpense.FooterRow.FindControl("txtFooterRemark");
                rev.Remark = txtFooterRemark.Text;

                bool succes = false;
                succes = dbo_RevenueExpenseDataClass.Add(rev);

                if (succes)
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("บันทึกสำเร็จ!");
                }

                GridViewExpense.ShowFooter = false;

                InitialExpense();

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        else if (e.CommandName == "_Delete")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Account_No = lnkView.CommandArgument;

            dbo_RevenueExpenseDataClass.Delete(Account_No);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");

            InitialExpense();
        }
    }

    #endregion


  
}