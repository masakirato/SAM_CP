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

public partial class Views_OrderAndDeliveryCycleList : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public static bool isChangedCycle;
    //public static int CurrentPreviousIndex;
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (!IsPostBack)
        {

            List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
            if (item.Count > 0)
            {
                GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID);
                GridViewCycle.DataBind();

                GridViewCycle.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewCycle.Visible = false;
                pnlNoRec.Visible = true;
            }

            item.Insert(0, new dbo_OrderAndDeliveryCycleClass() { Order_Cycle_ID = string.Empty, Order_Cycle_Name = "==ระบุ==" });
            ddlOrderCycle.DataSource = item;
            ddlOrderCycle.DataBind();

        }
        else
        {
            if (isChangedCycle == false)
            {
                ViewState["CurrentIndex"] = ddlCurrent.SelectedValue;
                ViewState["NewIndex"] = ddlNew.SelectedValue;
            }
        }
    }

    protected void ButtonCreateNew_Click(object sender, EventArgs e)
    {
        GridViewCycle.ShowFooter = true;
        GridViewCycle.EditIndex = -1;
        GridViewCycle.DataSource = dbo_OrderAndDeliveryCycleDataClass.SelectAll().OrderBy(f => f.Order_Cycle_ID);
        GridViewCycle.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnNewValue_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)GridViewCycle.Rows[current_index + 1].FindControl("grdNewValue");
            Label cycle_id = (Label)GridViewCycle.Rows[current_index].FindControl("lblOrder_Cycle_ID");
            //  gv.ShowHeaderWhenEmpty = false;
            gv.ShowFooter = true;
            gv.EditIndex = -1;

            List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle_id.Text);

            if (item_value.Count == 0)
            {
                item_value.Add(new dbo_OrderAndDeliveryCycleValueClass());
                gv.DataSource = item_value;
                gv.DataBind();
                gv.Rows[0].Visible = false;
            }
            else
            {
                gv.DataSource = item_value;
                gv.DataBind();
            }
        }
        catch (Exception)
        {

        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnAssignment_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlForm.Visible = true;
        pnlGrid.Visible = false;

        try
        {
            List<dbo_OrderAndDeliveryCycleClass> listoforder = dbo_OrderAndDeliveryCycleDataClass.SelectAll();

            ddlNew.DataSource = listoforder;
            ddlNew.DataBind();

            listoforder.Insert(listoforder.Count, new dbo_OrderAndDeliveryCycleClass() { Order_Cycle_Name = "ยังไม่กำหนดรอบสั่งรอบส่ง", Order_Cycle_ID = "--" });

            ddlCurrent.DataSource = listoforder;
            ddlCurrent.DataBind();

            Dictionary<string, string> item = dbo_CycleAssignmentDataClass.GetAssignCycle(ddlCurrent.SelectedValue);
            lstBPrimary.DataSource = item;
            lstBPrimary.DataBind();

            lsbBSecondary.DataSource = item;
            lsbBSecondary.DataBind();
        }
        catch (Exception)
        {

        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlCurrent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isChangedCycle == false)
        {
            string id = ddlCurrent.SelectedValue;

            //List<dbo_CycleAssignmentClass> item = dbo_CycleAssignmentDataClass.Search(id);

            Dictionary<string, string> item = dbo_CycleAssignmentDataClass.GetAssignCycle(ddlCurrent.SelectedValue);
            lstBPrimary.DataSource = item;
            lstBPrimary.DataBind();

        }
        else
        {
            Show("ข้อมูลกำหนดรอบสั่งรอบส่งมีการเปลี่ยนเปลี่ยน กรุณาบันทึกข้อมูลก่อนดำเนินการต่อ");
            //string GetPreviousValue = ddlCurrent.Items[CurrentPreviousIndex].Text;
            // ddlCurrent.Items.IndexOf(ViewState["CurrentIndex"]);
            if (ViewState["CurrentIndex"] != null)
            {
                ddlCurrent.SelectedValue = ddlCurrent.Items.FindByValue(ViewState["CurrentIndex"].ToString()).Value;
            }
            else
            {
                ddlCurrent.SelectedIndex = 1;
            }
            //ddlCurrent.SelectedIndex = ViewState["CurrentIndex"];
        }
    }

    protected void ddlNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isChangedCycle == false)
        {
            string id = ddlNew.SelectedValue;

            // List<dbo_CycleAssignmentClass> item = dbo_CycleAssignmentDataClass.Search(id);
            Dictionary<string, string> item = dbo_CycleAssignmentDataClass.GetAssignCycle(ddlNew.SelectedValue);
            lsbBSecondary.DataSource = item;
            lsbBSecondary.DataBind();

            /*

            List<dbo_OrderAndDeliveryCycleClass> listoforder = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
            dbo_OrderAndDeliveryCycleClass first_item = new dbo_OrderAndDeliveryCycleClass();
            first_item.Order_Cycle_ID = "";
            first_item.Order_Cycle_Name = "==ระบุ==";
            // listoforder.Insert(0, first_item);

            //listoforder.Insert(listoforder.Count, new dbo_OrderAndDeliveryCycleClass() { Order_Cycle_Name = "ยังไม่กำหนดรอบสั่งรอบส่ง", Order_Cycle_ID = "--" });


            string ddlText = ddlCurrent.Text;
            if (!string.IsNullOrEmpty(id))
            {
                dbo_OrderAndDeliveryCycleClass i = listoforder.FirstOrDefault(f => f.Order_Cycle_ID == id);
                // listoforder.Remove(i);
            }

            ddlCurrent.DataSource = listoforder;
            ddlCurrent.DataBind();

            ddlCurrent.Text = ddlText;*/
        }
        else
        {
            Show("ข้อมูลกำหนดรอบสั่งรอบส่งมีการเปลี่ยนเปลี่ยน กรุณาบันทึกข้อมูลก่อนดำเนินการต่อ");
            //string GetPreviousValue = ddlCurrent.Items[CurrentPreviousIndex].Text;
            // ddlCurrent.Items.IndexOf(ViewState["CurrentIndex"]);

            ddlNew.SelectedValue = ddlNew.Items.FindByValue(ViewState["NewIndex"].ToString()).Value;
            //ddlCurrent.SelectedIndex = ViewState["CurrentIndex"];
        }
    }

    protected void btnAddOne_Click(object sender, EventArgs e)
    {
        try
        {
            Dictionary<string, string> itemsecondary = new Dictionary<string, string>();


            foreach (ListItem list_item in lsbBSecondary.Items)
            {
                itemsecondary.Add(list_item.Value, list_item.Text);
            }

            String primary_item = String.Empty;
            ListItem temp_li = null;

            foreach (ListItem li in lstBPrimary.Items)
            {
                if (li.Selected == true)
                {
                    itemsecondary.Add(li.Value, li.Text);
                    temp_li = li;
                    isChangedCycle = true;
                }
            }

            lstBPrimary.Items.Remove(temp_li);

            lsbBSecondary.DataSource = itemsecondary;
            lsbBSecondary.DataBind();
        }
        catch (Exception)
        {
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (isChangedCycle == true)
            {
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;


                if (!string.IsNullOrEmpty(ddlNew.SelectedValue) && ddlNew.SelectedValue != "--")
                {
                    foreach (ListItem list_item in lsbBSecondary.Items)
                    {
                        dbo_CycleAssignmentDataClass.Delete(list_item.Value);
                        dbo_CycleAssignmentDataClass.Add(new dbo_CycleAssignmentClass()
                        {
                            CV_Code = list_item.Value,
                            Order_Cycle_ID = ddlNew.SelectedValue
                        }, User_ID);
                    }
                }
                if (ddlCurrent.SelectedValue != "--")
                {
                    foreach (ListItem li in lstBPrimary.Items)
                    {
                        dbo_CycleAssignmentDataClass.Delete(li.Value);
                        dbo_CycleAssignmentDataClass.Add(new dbo_CycleAssignmentClass()
                        {
                            CV_Code = li.Value,
                            Order_Cycle_ID = ddlCurrent.SelectedValue
                        }, User_ID);
                    }
                }
                else
                {
                    foreach (ListItem li in lstBPrimary.Items)
                    {
                        dbo_CycleAssignmentDataClass.Delete(li.Value);

                    }
                }
                isChangedCycle = false;              
                
            }

            Show("บันทึกสำเร็จ");
        }
        catch (Exception)
        {

        }
        finally
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlForm.Visible = true;
        pnlGrid.Visible = false;
        isChangedCycle = false;
        Dictionary<string, string> item = dbo_CycleAssignmentDataClass.GetAssignCycle(ddlCurrent.SelectedValue);
        lstBPrimary.DataSource = item;
        lstBPrimary.DataBind();

        Dictionary<string, string> itemNew = dbo_CycleAssignmentDataClass.GetAssignCycle(ddlNew.SelectedValue);
        lsbBSecondary.DataSource = itemNew;
        lsbBSecondary.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnShowGrid_Click(object sender, EventArgs e)
    {
        isChangedCycle = false;
        List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
        GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID);
        GridViewCycle.DataBind();


        pnlForm.Visible = false;
        pnlGrid.Visible = true;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnRemoveOne_Click(object sender, EventArgs e)
    {
        try
        {          
            Dictionary<string, string> itemsecondary = new Dictionary<string, string>();

            foreach (ListItem list_item in lstBPrimary.Items)
            {
                itemsecondary.Add(list_item.Value, list_item.Text);
            }

            String primary_item = String.Empty;
            ListItem temp_li = null;

            foreach (ListItem li in lsbBSecondary.Items)
            {
                if (li.Selected == true)
                {
                    // li.Attributes.Add("removeThis", "true");
                    itemsecondary.Add(li.Value, li.Text);

                    temp_li = li;
                    isChangedCycle = true;
                }
            }

            lsbBSecondary.Items.Remove(temp_li);
            lstBPrimary.DataSource = itemsecondary;
            lstBPrimary.DataBind();

        }
        catch (Exception)
        {

        }

    }

    protected void btnAddAll_Click(object sender, EventArgs e)
    {
        try
        {

            Dictionary<string, string> itemsecondary = new Dictionary<string, string>();

            foreach (ListItem list_item in lsbBSecondary.Items)
            {
                itemsecondary.Add(list_item.Value, list_item.Text);
            }

            String primary_item = String.Empty;
            ListItem temp_li = null;

            foreach (ListItem li in lstBPrimary.Items)
            {
                if (lsbBSecondary.Items.FindByValue(li.Value) == null)
                {
                    itemsecondary.Add(li.Value, li.Text);
                    temp_li = li;
                    isChangedCycle = true;
                }
            }

            lstBPrimary.Items.Clear();      

            lsbBSecondary.DataSource = itemsecondary;
            lsbBSecondary.DataBind();
         
        }
        catch (Exception)
        {

        }
    }

    protected void btnRemoveAll_Click(object sender, EventArgs e)
    {
        try
        {         
            Dictionary<string, string> itemsecondary = new Dictionary<string, string>();

            foreach (ListItem list_item in lstBPrimary.Items)
            {
                itemsecondary.Add(list_item.Value, list_item.Text);
            }

            String primary_item = String.Empty;
            ListItem temp_li = null;

            foreach (ListItem li in lsbBSecondary.Items)
            {
                itemsecondary.Add(li.Value, li.Text);

                temp_li = li;
                isChangedCycle = true;
            }

            lsbBSecondary.Items.Clear();

            lstBPrimary.DataSource = itemsecondary;
            lstBPrimary.DataBind();
        }
        catch (Exception)
        {

        }
    }

    protected void ddlOrderCycle_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearchOK_Click(object sender, EventArgs e)
    {

        if (ddlOrderCycle.SelectedIndex > 0)
        {
            List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.Search(ddlOrderCycle.Text);
            if (item.Count > 0)
            {
                GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID);
                GridViewCycle.DataBind();

                GridViewCycle.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewCycle.Visible = false;
                pnlNoRec.Visible = true;
            }
        }
        else
        {
            List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();          
            if (item.Count > 0)
            {
                GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID);
                GridViewCycle.DataBind();

                GridViewCycle.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewCycle.Visible = false;
                pnlNoRec.Visible = true;
            }
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
        if (item.Count > 0)
        {
            GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID);
            GridViewCycle.DataBind();

            GridViewCycle.Visible = true;
            pnlNoRec.Visible = false;
        }
        else
        {
            GridViewCycle.Visible = false;
            pnlNoRec.Visible = true;
        }

        item.Insert(0, new dbo_OrderAndDeliveryCycleClass() { Order_Cycle_ID = string.Empty, Order_Cycle_Name = "==ระบุ==" });
        ddlOrderCycle.DataSource = item;
        ddlOrderCycle.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
    #endregion

    #region GridView Events
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewCycle.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewCycle.PageIndex = pageList.SelectedIndex;

        List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
        if (item.Count > 0)
        {
            GridViewCycle.DataSource = item;//item.OrderBy(f => f.Order_Cycle_ID);
            GridViewCycle.DataBind();

            GridViewCycle.Visible = true;
            pnlNoRec.Visible = false;
        }
        else
        {
            GridViewCycle.Visible = false;
            pnlNoRec.Visible = true;
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewCycle_DataBound(object sender, EventArgs e)
    {
        GridViewRow pagerRow = GridViewCycle.BottomPagerRow;

        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {
            for (int i = 0; i < GridViewCycle.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                if (i == GridViewCycle.PageIndex)
                {
                    item.Selected = true;
                }
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = GridViewCycle.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewCycle.PageCount.ToString();

        }
    }

    protected void GridViewCycle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        GridViewCycle.EditIndex = -1;
        GridViewCycle.ShowFooter = false;


        List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
        GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID); ;
        GridViewCycle.DataBind();
    }

    int current_index = -1;

    protected void GridViewCycle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (e.CommandName == "EditCycle")
        {
            int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());

            List<dbo_OrderAndDeliveryCycleClass> item1 = dbo_OrderAndDeliveryCycleDataClass.SelectAll();

            if (item1.Count < GridViewCycle.Rows.Count && RowIndex > int.Parse(ViewState["current_index"].ToString()))
            {
                RowIndex--;
            }

            current_index = RowIndex;

            ViewState["current_index"] = current_index;

            LinkButton currbtn = (LinkButton)GridViewCycle.Rows[RowIndex].FindControl("lnkB_SetOrder_Cycle_Name");

            //Label cycle_id = (Label)GridViewCycle.Rows[RowIndex].FindControl("lblOrder_Cycle_ID");
            string orderCycleID = item1[RowIndex].Order_Cycle_ID;


            if (currbtn.Text == "แก้ไขรอบสั่งรอบส่ง")
            {
                dbo_OrderAndDeliveryCycleClass cycle = new dbo_OrderAndDeliveryCycleClass() { };

                List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();

                item.Insert(RowIndex + 1, new dbo_OrderAndDeliveryCycleClass() { });
                
                GridViewCycle.DataSource = item; 

                GridViewCycle.DataBind();
                currbtn = (LinkButton)GridViewCycle.Rows[RowIndex].FindControl("lnkB_SetOrder_Cycle_Name");
                currbtn.Text = "ปิด";


                //List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle_id.Text);
                List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(orderCycleID);

                GridView gv = (GridView)GridViewCycle.Rows[RowIndex + 1].FindControl("grdNewValue");
                Button newbutton1 = (Button)GridViewCycle.Rows[RowIndex + 1].FindControl("btnNewValue");
                LinkButton Cycle_Name = (LinkButton)GridViewCycle.Rows[RowIndex + 1].FindControl("lnkB_SetOrder_Cycle_Name");

                gv.DataSource = item_value;
                gv.DataBind();
                gv.Visible = true;
                newbutton1.Visible = true;
                Cycle_Name.Visible = false;

                GridViewCycle.Rows[RowIndex + 1].Cells[2].ColumnSpan = 5;
                GridViewCycle.Rows[RowIndex + 1].Cells[0].Visible = false;
                GridViewCycle.Rows[RowIndex + 1].Cells[1].Visible = false;
                GridViewCycle.Rows[RowIndex + 1].Cells[3].Visible = false;
                GridViewCycle.Rows[RowIndex + 1].Cells[4].Visible = false;
                GridViewCycle.Rows[RowIndex + 1].Cells[5].Visible = false;


                Label currlbl = (Label)GridViewCycle.Rows[RowIndex + 1].FindControl("lblOrder");
                currlbl.Visible = false;
                // 
                currbtn.Visible = true;

                for (int i = RowIndex + 1; i < GridViewCycle.Rows.Count; i++)
                {
                    Label lbl_Amount = (Label)GridViewCycle.Rows[i].FindControl("lblOrder");
                    lbl_Amount.Text = i.ToString();
                }
            }
            else
            {
                List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
                GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID); ;
                GridViewCycle.DataBind();
            }

        }
        else if (e.CommandName == "AddNew")
        {
            string _Order_Cycle_ID = GenerateID.Order_Cycle_ID();

            TextBox _Order_Cycle_Name = (TextBox)GridViewCycle.FooterRow.FindControl("txtNewOrder_Cycle_Name");
            if (_Order_Cycle_Name.Text.Trim() != string.Empty)
            {
                dbo_OrderAndDeliveryCycleClass cycle = new dbo_OrderAndDeliveryCycleClass() { Order_Cycle_ID = _Order_Cycle_ID, Order_Cycle_Name = _Order_Cycle_Name.Text };

                bool success = false;
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                success = dbo_OrderAndDeliveryCycleDataClass.Add(cycle, User_ID);

                if (success)
                {

                    //string script = @"swal(""บันทึกสำเร็จ!"", """", ""success"")";
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);

                    // Show("บันทึกสำเร็จ");

                    GridViewCycle.ShowFooter = false;


                    List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
                    GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID); ;
                    GridViewCycle.DataBind();
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

    protected void GridViewCycle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int index = e.RowIndex;
            Label _lblOrder_Cycle_ID = (Label)GridViewCycle.Rows[e.RowIndex].FindControl("lblOrder_Cycle_ID");

            dbo_OrderAndDeliveryCycleClass order = dbo_OrderAndDeliveryCycleDataClass.Select_Record(_lblOrder_Cycle_ID.Text);

            List<dbo_OrderAndDeliveryCycleValueClass> value = dbo_OrderAndDeliveryCycleValueDataClass.Search(_lblOrder_Cycle_ID.Text);


            foreach (dbo_OrderAndDeliveryCycleValueClass _value in value)
            {
                dbo_OrderAndDeliveryCycleValueDataClass.Delete(_value.OrderAndDeliveryCycleValue_ID);
            }


            dbo_OrderAndDeliveryCycleDataClass.Delete(order);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");

            List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
            GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID); ;
            GridViewCycle.DataBind();
        }
        catch (Exception)
        {

        }
    }

    protected void GridViewCycle_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        GridViewCycle.EditIndex = e.NewEditIndex;

        GridViewCycle.ShowFooter = false;
        List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
        GridViewCycle.DataSource = item.OrderBy(f => f.Order_Cycle_ID);
        GridViewCycle.DataBind();
        LinkButton currbtn = (LinkButton)GridViewCycle.Rows[e.NewEditIndex].FindControl("lnkB_SetOrder_Cycle_Name");
        currbtn.Visible = false;
    }

    protected void GridViewCycle_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int RowIndex = Convert.ToInt32((e.RowIndex).ToString());

            Label _Order_Cycle_ID = (Label)GridViewCycle.Rows[RowIndex].FindControl("lblOrder_Cycle_ID");

            TextBox _Order_Cycle_Name = (TextBox)GridViewCycle.Rows[RowIndex].FindControl("txtEditOrder_Cycle_Name");


            dbo_OrderAndDeliveryCycleClass cycle = new dbo_OrderAndDeliveryCycleClass() { Order_Cycle_ID = _Order_Cycle_ID.Text, Order_Cycle_Name = _Order_Cycle_Name.Text };

            bool success = false;
            success = dbo_OrderAndDeliveryCycleDataClass.Update(cycle);

            if (success)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                //string script = @"swal(""บันทึกสำเร็จ!"", """", ""success"")";
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
                Show("บันทึกสำเร็จ!");

                GridViewCycle.EditIndex = -1;
                GridViewCycle.ShowFooter = false;
                GridViewCycle.DataSource = dbo_OrderAndDeliveryCycleDataClass.SelectAll().OrderBy(f => f.Order_Cycle_ID);
                GridViewCycle.DataBind();
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewCycle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        e.Row.Cells[4].Focus();
    }

    protected void grdNewValue_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridViewCycle.EditIndex = -1;
        //GridViewCycle.ShowFooter = false;


        //List<dbo_OrderAndDeliveryCycleClass> item = dbo_OrderAndDeliveryCycleDataClass.SelectAll();
        //GridViewCycle.DataSource = item;
        //GridViewCycle.DataBind();

        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        int current_index = int.Parse(ViewState["current_index"].ToString());
        GridView gv = (GridView)GridViewCycle.Rows[current_index + 1].FindControl("grdNewValue");
        Label cycle_id = (Label)GridViewCycle.Rows[current_index].FindControl("lblOrder_Cycle_ID");
        gv.EditIndex = -1;
        gv.ShowFooter = false;


        List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle_id.Text);
        gv.DataSource = item_value;
        gv.DataBind();
    }

    protected void grdNewValue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (e.CommandName == "AddNewValue")
        {

            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)GridViewCycle.Rows[current_index + 1].FindControl("grdNewValue");
            Label cycle_id = (Label)GridViewCycle.Rows[current_index].FindControl("lblOrder_Cycle_ID");


            DropDownList _ddlFooterOrder_Cycle_Date = (DropDownList)gv.FooterRow.FindControl("ddlFooterOrder_Cycle_Date");
            DropDownList _ddlFooterOrder_Cycle_Hour = (DropDownList)gv.FooterRow.FindControl("ddlFooterOrder_Cycle_Hour");
            DropDownList _ddlFooterOrder_Cycle_Minute = (DropDownList)gv.FooterRow.FindControl("ddlFooterOrder_Cycle_Minute");
            DropDownList _ddlFooterDelivery_Cycle_Date = (DropDownList)gv.FooterRow.FindControl("ddlFooterDelivery_Cycle_Date");

            TextBox _txtRoute = (TextBox)gv.FooterRow.FindControl("txtFooterRoute");

            //if (!string.IsNullOrEmpty(_txtRoute.Text))
            //{

            dbo_OrderAndDeliveryCycleValueClass cycle = new dbo_OrderAndDeliveryCycleValueClass()
            {
                Order_Cycle_ID = cycle_id.Text,
                Order_Cycle_Date = _ddlFooterOrder_Cycle_Date.SelectedValue,
                Order_Cycle_Hour = _ddlFooterOrder_Cycle_Hour.SelectedValue,
                Order_Cycle_Minute = _ddlFooterOrder_Cycle_Minute.SelectedValue,
                Delivery_Cycle_Date = _ddlFooterDelivery_Cycle_Date.SelectedValue,
                Route = _txtRoute.Text
            };
            bool success = false;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            success = dbo_OrderAndDeliveryCycleValueDataClass.Add(cycle, User_ID);

            if (success)
            {
                // string script = @"swal(""บันทึกสำเร็จ!"", """", ""success"")";

                // Show("บันทึกสำเร็จ!");
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
                gv.ShowFooter = false;
                List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle_id.Text);
                gv.DataSource = item_value;
                gv.DataBind();
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            //}
            /*else
            {
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }*/
        }
    }

    protected void grdNewValue_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)GridViewCycle.Rows[current_index + 1].FindControl("grdNewValue");
            Label cycle_id = (Label)GridViewCycle.Rows[current_index].FindControl("lblOrder_Cycle_ID");


            int index = e.RowIndex;
            Label _lblOrderAndDeliveryCycleValue_ID = (Label)gv.Rows[e.RowIndex].FindControl("lblOrderAndDeliveryCycleValue_ID");

            dbo_OrderAndDeliveryCycleValueDataClass.Delete(int.Parse(_lblOrderAndDeliveryCycleValue_ID.Text));

            List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle_id.Text);
            gv.DataSource = item_value;
            gv.DataBind();
            // Show("ลบสำเร็จ");

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        }
        catch (Exception)
        {

        }
    }

    protected void grdNewValue_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {

            int current_index = int.Parse(ViewState["current_index"].ToString());
            GridView gv = (GridView)GridViewCycle.Rows[current_index + 1].FindControl("grdNewValue");
            Label cycle_id = (Label)GridViewCycle.Rows[current_index].FindControl("lblOrder_Cycle_ID");

            gv.EditIndex = e.NewEditIndex;
            gv.ShowFooter = false;
            List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle_id.Text);
            gv.DataSource = item_value;
            gv.DataBind();
        }
        catch (Exception)
        {

        }
    }

    protected void grdNewValue_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        int current_index = int.Parse(ViewState["current_index"].ToString());
        GridView gv = (GridView)GridViewCycle.Rows[current_index + 1].FindControl("grdNewValue");
        Label cycle_id = (Label)GridViewCycle.Rows[current_index].FindControl("lblOrder_Cycle_ID");
        DropDownList _ddlFooterOrder_Cycle_Date = (DropDownList)gv.Rows[e.RowIndex].FindControl("ddlEditOrder_Cycle_Date");
        DropDownList _ddlFooterOrder_Cycle_Hour = (DropDownList)gv.Rows[e.RowIndex].FindControl("ddlEditOrder_Cycle_Hour");
        DropDownList _ddlFooterOrder_Cycle_Minute = (DropDownList)gv.Rows[e.RowIndex].FindControl("ddlEditOrder_Cycle_Minute");
        DropDownList _ddlFooterDelivery_Cycle_Date = (DropDownList)gv.Rows[e.RowIndex].FindControl("ddlEditDelivery_Cycle_Date");

        TextBox _txtRoute = (TextBox)gv.Rows[e.RowIndex].FindControl("txtEditRoute");
        Label id = (Label)gv.Rows[e.RowIndex].FindControl("lblOrderAndDeliveryCycleValue_ID");
        //if (!string.IsNullOrEmpty(_txtRoute.Text))
        //{

        dbo_OrderAndDeliveryCycleValueClass cycle = new dbo_OrderAndDeliveryCycleValueClass()
        {
            Order_Cycle_ID = cycle_id.Text,
            Order_Cycle_Date = _ddlFooterOrder_Cycle_Date.SelectedValue,
            Order_Cycle_Hour = _ddlFooterOrder_Cycle_Hour.SelectedValue,
            Order_Cycle_Minute = _ddlFooterOrder_Cycle_Minute.SelectedValue,
            Delivery_Cycle_Date = _ddlFooterDelivery_Cycle_Date.SelectedValue,
            Route = _txtRoute.Text,
            OrderAndDeliveryCycleValue_ID = int.Parse(id.Text)
        };

        bool success = false;
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        success = dbo_OrderAndDeliveryCycleValueDataClass.Update(cycle, User_ID);

        if (success)
        {
            // string script = @"swal(""บันทึกสำเร็จ!"", """", ""success"")";

            // Show("บันทึกสำเร็จ!");
            // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
            gv.ShowFooter = false;
            gv.EditIndex = -1;

            List<dbo_OrderAndDeliveryCycleValueClass> item_value = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle_id.Text);
            gv.DataSource = item_value;
            gv.DataBind();
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void grdNewValue_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion  

    #region Method
    public void Show(string message)
    {
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
    
}