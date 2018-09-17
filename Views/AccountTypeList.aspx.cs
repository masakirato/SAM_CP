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

public partial class Views_AccountTypeList : System.Web.UI.Page
{
    #region Private Variable
    int current_index = -1;
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                List<dbo_AccountTypeClass> item = dbo_AccountTypeDataClass.GetAccountType_New();
                if (item.Count > 0)
                {
                    gdvAccountType.DataSource = item;
                    gdvAccountType.DataBind();

                    gdvAccountType.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    gdvAccountType.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void btnCreateAccountType_Click(object sender, EventArgs e)
    {
        try
        {
            gdvAccountType.ShowFooter = true;
            gdvAccountType.EditIndex = -1;
            gdvAccountType.DataSource = dbo_AccountTypeDataClass.GetAccountType_New();
            gdvAccountType.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void btnCreateAccountCode_Click(object sender, EventArgs e)
    {
        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)gdvAccountType.Rows[current_index + 1].FindControl("gdvAccountCode");
            Label lblAccountType_ID = (Label)gdvAccountType.Rows[current_index].FindControl("lblAccountType_ID");

            gv.ShowFooter = true;
            gv.EditIndex = -1;

            List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountCode_New(lblAccountType_ID.Text);

            if (item_value.Count == 0)
            {
                item_value.Add(new dbo_AccountCodeClass());
                gv.DataSource = item_value;
                gv.DataBind();
                gv.Rows[0].Visible = false;
            }
            else
            {
                gv.DataSource = item_value;
                gv.DataBind();
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }
    #endregion

    #region Method
    public void Show(string message)
    {
        try
        {
            string cleanMessage = message.Replace("'", "\'");
            string script = string.Format("alert('{0}');", cleanMessage);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }
    #endregion

    #region AccountType
    protected void gdvAccountType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gdvAccountType.EditIndex = -1;
            gdvAccountType.ShowFooter = false;

            List<dbo_AccountTypeClass> item = dbo_AccountTypeDataClass.GetAccountType_New();
            gdvAccountType.DataSource = item;
            gdvAccountType.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    protected void gdvAccountType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditAccountCode")
            {
                int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());

                List<dbo_AccountTypeClass> item1 = dbo_AccountTypeDataClass.GetAccountType_New();

                if (item1.Count < gdvAccountType.Rows.Count && RowIndex > int.Parse(ViewState["current_index"].ToString()))
                {
                    RowIndex--;
                }

                current_index = RowIndex;
                ViewState["current_index"] = current_index;
                LinkButton currbtn = (LinkButton)gdvAccountType.Rows[RowIndex].FindControl("lnkB_SetAccountCode");

                //Label lblAccountType_ID = (Label)gdvAccountType.Rows[RowIndex].FindControl("lblAccountType_ID");
                string accountTypeID = item1[RowIndex].Account_Type_ID;

                if (currbtn.Text == "แก้ไขรหัสบัญชี")
                {
                    dbo_AccountTypeClass cycle = new dbo_AccountTypeClass() { };

                    List<dbo_AccountTypeClass> item = dbo_AccountTypeDataClass.GetAccountType_New();

                    item.Insert(RowIndex + 1, new dbo_AccountTypeClass() { });
                    gdvAccountType.DataSource = item;
                    gdvAccountType.DataBind();
                    currbtn = (LinkButton)gdvAccountType.Rows[RowIndex].FindControl("lnkB_SetAccountCode");
                    currbtn.Text = "ปิด";

                    //List<dbo_AccountCodeClass> Account_Code = dbo_AccountTypeDataClass.GetAccountCode_New(lblAccountType_ID.Text);
                    List<dbo_AccountCodeClass> Account_Code = dbo_AccountTypeDataClass.GetAccountCode_New(accountTypeID);

                    GridView gv = (GridView)gdvAccountType.Rows[RowIndex + 1].FindControl("gdvAccountCode");
                    Button newbutton1 = (Button)gdvAccountType.Rows[RowIndex + 1].FindControl("btnCreateAccountCode");
                    LinkButton Cycle_Name = (LinkButton)gdvAccountType.Rows[RowIndex + 1].FindControl("lnkB_SetAccountCode");

                    gv.DataSource = Account_Code;
                    gv.DataBind();
                    gv.Visible = true;
                    newbutton1.Visible = true;
                    Cycle_Name.Visible = false;

                    gdvAccountType.Rows[RowIndex + 1].Cells[3].ColumnSpan = 6;
                    gdvAccountType.Rows[RowIndex + 1].Cells[0].Visible = false;
                    gdvAccountType.Rows[RowIndex + 1].Cells[1].Visible = false;
                    gdvAccountType.Rows[RowIndex + 1].Cells[2].Visible = false;
                    gdvAccountType.Rows[RowIndex + 1].Cells[4].Visible = false;
                    gdvAccountType.Rows[RowIndex + 1].Cells[5].Visible = false;
                    gdvAccountType.Rows[RowIndex + 1].Cells[6].Visible = false;

                    Label currlbl = (Label)gdvAccountType.Rows[RowIndex + 1].FindControl("lblOrder");
                    currlbl.Visible = false;
                    currbtn.Visible = true;

                    for (int i = RowIndex + 1; i < gdvAccountType.Rows.Count; i++)
                    {
                        Label lbl_Amount = (Label)gdvAccountType.Rows[i].FindControl("lblOrder");
                        lbl_Amount.Text = i.ToString();
                    }
                }
                else
                {
                    List<dbo_AccountTypeClass> item = dbo_AccountTypeDataClass.GetAccountType_New();
                    gdvAccountType.DataSource = item;
                    gdvAccountType.DataBind();
                }

            }
            else if (e.CommandName == "AddNew")
            {
                TextBox txtNewAccountType_Name = (TextBox)gdvAccountType.FooterRow.FindControl("txtNewAccountType_Name");
                DropDownList ddlFooterAccount_ID = (DropDownList)gdvAccountType.FooterRow.FindControl("ddlFooterAccount_ID");
                if (txtNewAccountType_Name.Text.Trim() != string.Empty)
                {
                    dbo_AccountTypeClass cycle = new dbo_AccountTypeClass()
                    {
                        Account_Type = txtNewAccountType_Name.Text,
                        Account_ID = ddlFooterAccount_ID.Text
                    };

                    bool success = false;
                    string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    success = dbo_AccountTypeDataClass.InsertAccountType_New(cycle, User_ID);

                    if (success)
                    {
                        gdvAccountType.ShowFooter = false;
                        List<dbo_AccountTypeClass> item = dbo_AccountTypeDataClass.GetAccountType_New();
                        gdvAccountType.DataSource = item;
                        gdvAccountType.DataBind();
                    }

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                    Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    protected void gdvAccountType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = e.RowIndex;
            Label lblAccountType_ID = (Label)gdvAccountType.Rows[e.RowIndex].FindControl("lblAccountType_ID");
            if (dbo_AccountTypeDataClass.DeleteAccountType_New(lblAccountType_ID.Text.ToString()))
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show("ลบข้อมูลสำเร็จ");
            }

            List<dbo_AccountTypeClass> item = dbo_AccountTypeDataClass.GetAccountType_New();
            gdvAccountType.DataSource = item;
            gdvAccountType.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void gdvAccountType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gdvAccountType.EditIndex = e.NewEditIndex;
            gdvAccountType.ShowFooter = false;
            List<dbo_AccountTypeClass> item = dbo_AccountTypeDataClass.GetAccountType_New();
            gdvAccountType.DataSource = item;
            gdvAccountType.DataBind();
            LinkButton currbtn = (LinkButton)gdvAccountType.Rows[e.NewEditIndex].FindControl("lnkB_SetAccountCode");
            currbtn.Visible = false;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    protected void gdvAccountType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int RowIndex = Convert.ToInt32((e.RowIndex).ToString());
            Label lblAccountType_ID = (Label)gdvAccountType.Rows[RowIndex].FindControl("lblAccountType_ID");
            TextBox txtEditAccountType_Name = (TextBox)gdvAccountType.Rows[RowIndex].FindControl("txtEditAccountType_Name");
            DropDownList ddlAccount_ID = (DropDownList)gdvAccountType.Rows[RowIndex].FindControl("ddlAccount_ID");
            if (txtEditAccountType_Name.Text.Trim() != string.Empty)
            {
                dbo_AccountTypeClass cycle = new dbo_AccountTypeClass()
                {
                    Account_Type_ID = lblAccountType_ID.Text,
                    Account_Type = txtEditAccountType_Name.Text,
                    Account_ID = ddlAccount_ID.Text
                };
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                bool success = false;
                success = dbo_AccountTypeDataClass.UpdateAccountType_New(cycle, User_ID);
                if (success)
                {
                    gdvAccountType.EditIndex = -1;
                    gdvAccountType.ShowFooter = false;
                    gdvAccountType.DataSource = dbo_AccountTypeDataClass.GetAccountType_New();
                    gdvAccountType.DataBind();
                }

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void gdvAccountType_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = gdvAccountType.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        gdvAccountType.PageIndex = pageList.SelectedIndex;
        List<dbo_AccountTypeClass> item = dbo_AccountTypeDataClass.GetAccountType_New();
        if (item.Count > 0)
        {
            gdvAccountType.DataSource = item;
            gdvAccountType.DataBind();

            gdvAccountType.Visible = true;
            pnlNoRec.Visible = false;
        }
        else
        {
            gdvAccountType.Visible = false;
            pnlNoRec.Visible = true;
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void gdvAccountType_DataBound(object sender, EventArgs e)
    {
        GridViewRow pagerRow = gdvAccountType.BottomPagerRow;

        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {
            for (int i = 0; i < gdvAccountType.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                if (i == gdvAccountType.PageIndex)
                {
                    item.Selected = true;
                }
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = gdvAccountType.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + gdvAccountType.PageCount.ToString();

        }
    }
    #endregion

    #region AccountCode
    protected void gdvAccountCode_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)gdvAccountType.Rows[current_index + 1].FindControl("gdvAccountCode");
            Label lblAccountType_ID = (Label)gdvAccountType.Rows[current_index].FindControl("lblAccountType_ID");
            gv.EditIndex = -1;
            gv.ShowFooter = false;

            List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountCode_New(lblAccountType_ID.Text);
            gv.DataSource = item_value;
            gv.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    protected void gdvAccountCode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AddNewAccountCode")
            {
                int current_index = int.Parse(ViewState["current_index"].ToString());
                GridView gv = (GridView)gdvAccountType.Rows[current_index + 1].FindControl("gdvAccountCode");
                Label lblAccountType_ID = (Label)gdvAccountType.Rows[current_index].FindControl("lblAccountType_ID");
                TextBox txtFooterAccountCode_Name = (TextBox)gv.FooterRow.FindControl("txtFooterAccountCode_Name");
                Label lblAccount_ID = (Label)gdvAccountType.Rows[current_index].FindControl("lblAccount_ID");

                if (!string.IsNullOrEmpty(txtFooterAccountCode_Name.Text))
                {
                    dbo_AccountCodeClass cycle = new dbo_AccountCodeClass()
                    {
                        Account_Type_ID = lblAccountType_ID.Text,
                        Account_Name = txtFooterAccountCode_Name.Text
                    };
                    bool success = false;
                    string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    success = dbo_AccountTypeDataClass.InsertAccountCode_New(cycle, User_ID, lblAccount_ID.Text);

                    if (success)
                    {
                        gv.ShowFooter = false;
                        List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountCode_New(lblAccountType_ID.Text);
                        gv.DataSource = item_value;
                        gv.DataBind();
                    }
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    protected void gdvAccountCode_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)gdvAccountType.Rows[current_index + 1].FindControl("gdvAccountCode");
            Label lblAccountType_ID = (Label)gdvAccountType.Rows[current_index].FindControl("lblAccountType_ID");

            int index = e.RowIndex;
            Label lblAccountCode_ID = (Label)gv.Rows[e.RowIndex].FindControl("lblAccountCode_ID");

            if (dbo_AccountTypeDataClass.DeleteAccountCode_New(lblAccountCode_ID.Text.ToString()))
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ลบข้อมูลสำเร็จ");
            }

            List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountCode_New(lblAccountType_ID.Text);
            gv.DataSource = item_value;
            gv.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void gdvAccountCode_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)gdvAccountType.Rows[current_index + 1].FindControl("gdvAccountCode");
            Label lblAccountType_ID = (Label)gdvAccountType.Rows[current_index].FindControl("lblAccountType_ID");

            gv.EditIndex = e.NewEditIndex;
            gv.ShowFooter = false;
            List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountCode_New(lblAccountType_ID.Text);
            gv.DataSource = item_value;
            gv.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    protected void gdvAccountCode_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)gdvAccountType.Rows[current_index + 1].FindControl("gdvAccountCode");
            Label lblAccountType_ID = (Label)gdvAccountType.Rows[current_index].FindControl("lblAccountType_ID");

            TextBox txtEditAccountCode_Name = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditAccountCode_Name");
            Label lblAccountCode_ID = (Label)gv.Rows[e.RowIndex].FindControl("lblAccountCode_ID");
            if (!string.IsNullOrEmpty(txtEditAccountCode_Name.Text))
            {

                dbo_AccountCodeClass cycle = new dbo_AccountCodeClass()
                {
                    Account_Type_ID = lblAccountType_ID.Text,
                    Account_Name = txtEditAccountCode_Name.Text,
                    Account_Code = lblAccountCode_ID.Text
                };

                bool success = false;
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                success = dbo_AccountTypeDataClass.UpdateAccountCode_New(cycle, User_ID);

                if (success)
                {
                    gv.ShowFooter = false;
                    gv.EditIndex = -1;

                    List<dbo_AccountCodeClass> item_value = dbo_AccountTypeDataClass.GetAccountCode_New(lblAccountType_ID.Text);
                    gv.DataSource = item_value;
                    gv.DataBind();
                }

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }


    }

    protected void gdvAccountCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion
    
}