using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ItemValueList : System.Web.UI.Page
{
    int current_index = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<dbo_ItemClass> item = dbo_ItemDataClass.GetItem_New();
            if (item.Count > 0)
            {
                gdvItem.DataSource = item;
                gdvItem.DataBind();

                gdvItem.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                gdvItem.Visible = false;
                pnlNoRec.Visible = true;
            }
        }
    }

    protected void btnCreateItem_Click(object sender, EventArgs e)
    {
        gdvItem.ShowFooter = true;
        gdvItem.EditIndex = -1;
        gdvItem.DataSource = dbo_ItemDataClass.GetItem_New();
        gdvItem.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnCreateItemValue_Click(object sender, EventArgs e)
    {
        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)gdvItem.Rows[current_index + 1].FindControl("gdvItemValue");
            Label item_id = (Label)gdvItem.Rows[current_index].FindControl("lblItem_ID");
            gv.ShowFooter = true;
            gv.EditIndex = -1;

            List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(item_id.Text);

            if (item_value.Count == 0)
            {
                item_value.Add(new dbo_ItemValueClass());
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

        }
    }

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

        }
    }

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = gdvItem.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        gdvItem.PageIndex = pageList.SelectedIndex;
        List<dbo_ItemClass> item = dbo_ItemDataClass.GetItem_New();
        if (item.Count > 0)
        {
            gdvItem.DataSource = item;
            gdvItem.DataBind();

            gdvItem.Visible = true;
            pnlNoRec.Visible = false;
        }
        else
        {
            gdvItem.Visible = false;
            pnlNoRec.Visible = true;
        }        

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void gdvItem_DataBound(object sender, EventArgs e)
    {
        GridViewRow pagerRow = gdvItem.BottomPagerRow;

        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {
            for (int i = 0; i < gdvItem.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                if (i == gdvItem.PageIndex)
                {
                    item.Selected = true;
                }
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = gdvItem.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + gdvItem.PageCount.ToString();

        }
    }

    #region Item
    protected void gdvItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvItem.EditIndex = -1;
        gdvItem.ShowFooter = false;

        List<dbo_ItemClass> item = dbo_ItemDataClass.GetItem_New();
        gdvItem.DataSource = item;
        gdvItem.DataBind();
    }

    protected void gdvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditItemValue")
        {
            int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());

            List<dbo_ItemClass> item1 = dbo_ItemDataClass.GetItem_New();

            if (item1.Count < gdvItem.Rows.Count && RowIndex > int.Parse(ViewState["current_index"].ToString()))
            {
                RowIndex--;
            }

            current_index = RowIndex;
            ViewState["current_index"] = current_index;
            LinkButton currbtn = (LinkButton)gdvItem.Rows[RowIndex].FindControl("lnkB_SetItem_Value");

            //Label lblItem_ID = (Label)gdvItem.Rows[RowIndex].FindControl("lblItem_ID");
            string ItemID = item1[RowIndex].Item_ID;

            if (currbtn.Text == "แก้ไข Value")
            {
                dbo_ItemClass cycle = new dbo_ItemClass() { };

                List<dbo_ItemClass> item = dbo_ItemDataClass.GetItem_New();

                item.Insert(RowIndex + 1, new dbo_ItemClass() { });
                gdvItem.DataSource = item;
                gdvItem.DataBind();
                currbtn = (LinkButton)gdvItem.Rows[RowIndex].FindControl("lnkB_SetItem_Value");
                currbtn.Text = "ปิด";

                //List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(lblItem_ID.Text);
                List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(ItemID);

                GridView gv = (GridView)gdvItem.Rows[RowIndex + 1].FindControl("gdvItemValue");
                Button newbutton1 = (Button)gdvItem.Rows[RowIndex + 1].FindControl("btnCreateItemValue");
                LinkButton Cycle_Name = (LinkButton)gdvItem.Rows[RowIndex + 1].FindControl("lnkB_SetItem_Value");

                gv.DataSource = item_value;
                gv.DataBind();
                gv.Visible = true;
                newbutton1.Visible = true;
                Cycle_Name.Visible = false;

                gdvItem.Rows[RowIndex + 1].Cells[2].ColumnSpan = 5;
                gdvItem.Rows[RowIndex + 1].Cells[0].Visible = false;
                gdvItem.Rows[RowIndex + 1].Cells[1].Visible = false;
                gdvItem.Rows[RowIndex + 1].Cells[3].Visible = false;
                gdvItem.Rows[RowIndex + 1].Cells[4].Visible = false;
                gdvItem.Rows[RowIndex + 1].Cells[5].Visible = false;

                Label currlbl = (Label)gdvItem.Rows[RowIndex + 1].FindControl("lblOrder");
                currlbl.Visible = false;
                currbtn.Visible = true;

                for (int i = RowIndex + 1; i < gdvItem.Rows.Count; i++)
                {
                    Label lbl_Amount = (Label)gdvItem.Rows[i].FindControl("lblOrder");
                    lbl_Amount.Text = i.ToString();
                }
            }
            else
            {
                List<dbo_ItemClass> item = dbo_ItemDataClass.GetItem_New();
                gdvItem.DataSource = item;
                gdvItem.DataBind();
            }

        }
        else if (e.CommandName == "AddNew")
        {
            TextBox txtNewItem_Name = (TextBox)gdvItem.FooterRow.FindControl("txtNewItem_Name");
            if (txtNewItem_Name.Text.Trim() != string.Empty)
            {
                dbo_ItemClass cycle = new dbo_ItemClass() { Item_Name = txtNewItem_Name.Text };

                bool success = false;
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                success = dbo_ItemDataClass.InsertItem_New(cycle, User_ID);

                if (success)
                {
                    gdvItem.ShowFooter = false;
                    List<dbo_ItemClass> item = dbo_ItemDataClass.GetItem_New();
                    gdvItem.DataSource = item;
                    gdvItem.DataBind();
                }
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void gdvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = e.RowIndex;
            Label lblItem_ID = (Label)gdvItem.Rows[e.RowIndex].FindControl("lblItem_ID");
            if (dbo_ItemDataClass.DeleteItem_New(lblItem_ID.Text.ToString()))
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ลบข้อมูลสำเร็จ");
            }

            List<dbo_ItemClass> item = dbo_ItemDataClass.GetItem_New();
            gdvItem.DataSource = item;
            gdvItem.DataBind();
        }
        catch (Exception)
        {

        }
    }

    protected void gdvItem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvItem.EditIndex = e.NewEditIndex;
        gdvItem.ShowFooter = false;
        List<dbo_ItemClass> item = dbo_ItemDataClass.GetItem_New();
        gdvItem.DataSource = item;
        gdvItem.DataBind();
        LinkButton currbtn = (LinkButton)gdvItem.Rows[e.NewEditIndex].FindControl("lnkB_SetItem_Value");
        currbtn.Visible = false;
    }

    protected void gdvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int RowIndex = Convert.ToInt32((e.RowIndex).ToString());
            Label lblItem_ID = (Label)gdvItem.Rows[RowIndex].FindControl("lblItem_ID");
            TextBox txtEditItem_Name = (TextBox)gdvItem.Rows[RowIndex].FindControl("txtEditItem_Name");
            if (txtEditItem_Name.Text.Trim() != string.Empty)
            {
                dbo_ItemClass cycle = new dbo_ItemClass() { Item_ID = lblItem_ID.Text, Item_Name = txtEditItem_Name.Text };
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                bool success = false;
                success = dbo_ItemDataClass.UpdateItem_New(cycle, User_ID);
                if (success)
                {
                    gdvItem.EditIndex = -1;
                    gdvItem.ShowFooter = false;
                    gdvItem.DataSource = dbo_ItemDataClass.GetItem_New();
                    gdvItem.DataBind();
                }
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน"); 
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void gdvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion

    #region Item Value
    protected void gdvItemValue_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        int current_index = int.Parse(ViewState["current_index"].ToString());
        GridView gv = (GridView)gdvItem.Rows[current_index + 1].FindControl("gdvItemValue");
        Label lblItem_ID = (Label)gdvItem.Rows[current_index].FindControl("lblItem_ID");
        gv.EditIndex = -1;
        gv.ShowFooter = false;

        List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(lblItem_ID.Text);
        gv.DataSource = item_value;
        gv.DataBind();
    }

    protected void gdvItemValue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddNewItemValue")
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)gdvItem.Rows[current_index + 1].FindControl("gdvItemValue");
            Label lblItem_ID = (Label)gdvItem.Rows[current_index].FindControl("lblItem_ID");
            TextBox txtFooterItemValue_Name = (TextBox)gv.FooterRow.FindControl("txtFooterItemValue_Name");

            if (!string.IsNullOrEmpty(txtFooterItemValue_Name.Text))
            {
                dbo_ItemValueClass cycle = new dbo_ItemValueClass()
                {
                    Item_ID = lblItem_ID.Text,
                    Item_Value_Name = txtFooterItemValue_Name.Text
                };
                bool success = false;
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                success = dbo_ItemDataClass.InsertItemValue_New(cycle, User_ID);

                if (success)
                {
                    gv.ShowFooter = false;
                    List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(lblItem_ID.Text);
                    gv.DataSource = item_value;
                    gv.DataBind();
                }
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void gdvItemValue_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)gdvItem.Rows[current_index + 1].FindControl("gdvItemValue");
            Label lblItem_ID = (Label)gdvItem.Rows[current_index].FindControl("lblItem_ID");

            int index = e.RowIndex;
            Label lblItemValue_ID = (Label)gv.Rows[e.RowIndex].FindControl("lblItemValue_ID");

            if (dbo_ItemDataClass.DeleteItemValue_New(lblItemValue_ID.Text.ToString()))
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ลบข้อมูลสำเร็จ");
            }

            List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(lblItem_ID.Text);
            gv.DataSource = item_value;
            gv.DataBind();
        }
        catch (Exception)
        {

        }
    }

    protected void gdvItemValue_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int current_index = int.Parse(ViewState["current_index"].ToString());
        GridView gv = (GridView)gdvItem.Rows[current_index + 1].FindControl("gdvItemValue");
        Label lblItem_ID = (Label)gdvItem.Rows[current_index].FindControl("lblItem_ID");

        gv.EditIndex = e.NewEditIndex;
        gv.ShowFooter = false;
        List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(lblItem_ID.Text);
        gv.DataSource = item_value;
        gv.DataBind();
    }

    protected void gdvItemValue_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int current_index = int.Parse(ViewState["current_index"].ToString());
        GridView gv = (GridView)gdvItem.Rows[current_index + 1].FindControl("gdvItemValue");
        Label lblItem_ID = (Label)gdvItem.Rows[current_index].FindControl("lblItem_ID");

        TextBox txtEditItemValue_Name = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditItemValue_Name");
        Label lblItemValue_ID = (Label)gv.Rows[e.RowIndex].FindControl("lblItemValue_ID");
        if (!string.IsNullOrEmpty(txtEditItemValue_Name.Text))
        {

            dbo_ItemValueClass cycle = new dbo_ItemValueClass()
            {
                Item_ID = lblItem_ID.Text,
                Item_Value_Name = txtEditItemValue_Name.Text,
                Item_Value_ID = lblItemValue_ID.Text
            };

            bool success = false;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            success = dbo_ItemDataClass.UpdateItemValue_New(cycle, User_ID);

            if (success)
            {
                gv.ShowFooter = false;
                gv.EditIndex = -1;

                //List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(lblItemValue_ID.Text);
                List<dbo_ItemValueClass> item_value = dbo_ItemDataClass.GetItemValue_New(lblItem_ID.Text);
                gv.DataSource = item_value;
                gv.DataBind();
                //gv.Visible = true;

            }
        }
        else
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
        }
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void gdvItemValue_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion
   
}