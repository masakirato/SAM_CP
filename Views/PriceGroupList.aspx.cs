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

public partial class Views_PriceGroupList : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public static bool isChangedAssignPrice;
    #endregion

    #region Control Evenst
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowPriceGroup();
        }
        else
        {
            if (isChangedAssignPrice == false)
            {
                ViewState["CurrentIndex"] = ddlCurrent.SelectedValue;
                ViewState["NewIndex"] = ddlNew.SelectedValue;
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        ShowPriceGroup();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        dbo_PriceGroupClass item = dbo_PriceGroupDataClass.Select_Record(txtSearchPriceGroupID.Text);
        if (user_class.User_Group_ID == "Agent" && item.StandardPrice==true)
        {

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ไม่สามารถเพิ่มสินค้าในราคามาตรฐานได้");


        }
        else
        {
            ShowProductListDetails(string.Empty, string.Empty);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void ShowProductList_Click(object sender, EventArgs e)
    {
        // ShowProductList();
    }

    protected void ShowPriceGroupFooter_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        GridViewPrice_Group.EditIndex = -1;
        GridViewPrice_Group.ShowFooter = true;

        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

        ViewState["Price_Group_Type"] = ddlPrice_Group_Type.SelectedValue;

        if (user_class.User_Group_ID == "Agent")
        {
            List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
            List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
            foreach (dbo_PriceGroupClass d in dt)
            {
                if (d.StandardPrice == true)
                {
                    dtData.Add(d);
                }
                else
                {
                    if (d.CV_Code == user_class.CV_CODE)
                    {
                        dtData.Add(d);
                    }
                }
            }

            if (dtData.Count > 0)
            {
                GridViewPrice_Group.DataSource = dtData;//dt.Where(f => f.CV_Code == user_class.CV_CODE || f.StandardPrice == true);
                GridViewPrice_Group.DataBind();

                GridViewPrice_Group.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewPrice_Group.Visible = false;
                pnlNoRec.Visible = true;
            }
        }
        else
        {
            List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
            if (dt.Count > 0)
            {
                GridViewPrice_Group.DataSource = dt;
                GridViewPrice_Group.DataBind();

                GridViewPrice_Group.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewPrice_Group.Visible = false;
                pnlNoRec.Visible = true;
            }
        }

        if (user_class.User_Group_ID == "Agent")
        {
            foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
            {

                CheckBox chk = (CheckBox)currentRow.FindControl("chkItemStandard");
                LinkButton lnk = (LinkButton)currentRow.FindControl("lnkB_Product_List");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        //lnk.Visible = false;
                    }
                }
            }
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlGrid.Visible = true;
        pnlForm.Visible = false;

        dbo_PriceGroupClass item = dbo_PriceGroupDataClass.Select_Record(txtPrice_Group_ID.Text);
        ShowProductList(item);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

    }

    protected void ButtonBackToPriceGroup_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        ShowPriceGroup();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    public void btnSave_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            if (btnSave.Text == "แก้ไข")
            {
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                dbo_PriceGroupClass item = dbo_PriceGroupDataClass.Select_Record(txtSearchPriceGroupID.Text);
                if (user_class.User_Group_ID == "Agent" && item.StandardPrice == true)
                {

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ไม่สามารถแก้ไขสินค้าในราคามาตรฐานได้");


                }
                else
                {
                    ShowProductListDetails(txtProduct_List_ID.Text, "Edit");

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                }
            }
            else
            {
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

                if (txtPriceGroupType.Text == "เอเยนต์")
                {
                    Validate("AgentValidation");
                }
                else
                {
                    Validate("SPValidation");
                }


                if (Page.IsValid)
                {
                    if (btnSaveMode.Value == "บันทึก")
                    {
                        InsertRecord();
                    }
                    else
                    {
                        UpdateRecord();
                    }

                    pnlGrid.Visible = true;
                    pnlForm.Visible = false;

                    dbo_PriceGroupClass item = dbo_PriceGroupDataClass.Select_Record(txtPrice_Group_ID.Text);
                    ShowProductList(item);

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
            logger.Error(ex.Message);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        finally 
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        }
    }

    protected void btnAddAssign_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        showPanel("pnlAssign");

        try
        {
            if (ddlPrice_Group_Type.Enabled == true)
            {
                lblCPriceGroupType.Text = "เอเยนต์";
                lblNPriceGroupType.Text = "เอเยนต์";
            }
            else
            {
                lblCPriceGroupType.Text = "สาว";
                lblNPriceGroupType.Text = "สาว";
            }
            //if (ddlPrice_Group_Type.SelectedValue == "0")
            if (lblCPriceGroupType.Text == "เอเยนต์")
            {
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "0");
                List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
                foreach (dbo_PriceGroupClass d in current)
                {
                    if (d.StandardPrice == true)
                    {
                        dtData.Add(d);
                    }
                    else
                    {

                        if (user_class.User_Group_ID == "Agent")
                        {
                            if (d.CV_Code == user_class.CV_CODE)
                            {
                                dtData.Add(d);
                            }
                        }
                        else
                        {
                            dtData.Add(d);
                        }

                    }
                }

                //current.Insert(0, new dbo_PriceGroupClass() { Price_Group_ID = string.Empty, Price_Group_Name = "==ระบุ==" });
                ddlNew.DataSource = dtData;//current;
                ddlNew.DataBind();

                //current.Insert(current.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });
                dtData.Insert(dtData.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });
                ddlCurrent.DataSource = dtData;//current;
                ddlCurrent.DataBind();

                //ddlNew.DataSource = current;
                //ddlNew.DataBind();

                string price_group_id = ddlCurrent.SelectedValue;

                Dictionary<string, string> item = dbo_PriceGroupDataClass.GetAssignPriceGroup(ddlCurrent.SelectedValue);
                lstBPrimary.DataSource = item;
                lstBPrimary.DataBind();

                lsbBSecondary.DataSource = item;
                lsbBSecondary.DataBind();
            }
            else
            {
                //List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "1");
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "1");
                List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
                foreach (dbo_PriceGroupClass d in current)
                {
                    if (d.StandardPrice == true)
                    {
                        dtData.Add(d);
                    }
                    else
                    {
                        //if (d.CV_Code == user_class.CV_CODE)
                        //{
                        //    dtData.Add(d);
                        //}

                        if (user_class.User_Group_ID == "Agent")
                        {
                            if (d.CV_Code == user_class.CV_CODE)
                            {
                                dtData.Add(d);
                            }
                        }
                        else
                        {
                            dtData.Add(d);
                        }
                    }
                }


                //current.Insert(0, new dbo_PriceGroupClass() { Price_Group_ID = string.Empty, Price_Group_Name = "==ระบุ==" });
                ddlNew.DataSource = dtData;//current;
                ddlNew.DataBind();

                //current.Insert(current.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });
                dtData.Insert(dtData.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });
                ddlCurrent.DataSource = dtData;//current;
                ddlCurrent.DataBind();


                //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

                string price_group_id = ddlCurrent.SelectedValue;

                Dictionary<string, string> item = dbo_PriceGroupDataClass.GetAssignPriceGroupSP(ddlCurrent.SelectedValue, clsdbo_Agent.CV_Code);
                lstBPrimary.DataSource = item;
                lstBPrimary.DataBind();
                // Dictionary<string, string> item1 = new Dictionary<string, string>();
                lsbBSecondary.DataSource = item;
                lsbBSecondary.DataBind();
                //lsbBSecondary.DataSource = null;
                //lsbBSecondary.DataBind();
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnAddOne_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            // List<dbo_PriceGroupAssignmentClass> item = new List<dbo_PriceGroupAssignmentClass>();

            Dictionary<string, string> itemsecondary = new Dictionary<string, string>();

            foreach (ListItem list_item in lsbBSecondary.Items)
            {

                itemsecondary.Add(list_item.Value, list_item.Text);
            }

            //if (item == null)
            //{
            //    item = new List<dbo_PriceGroupAssignmentClass>();
            //}


            String primary_item = String.Empty;
            ListItem temp_li = null;



            foreach (ListItem li in lstBPrimary.Items)
            {
                if (li.Selected == true)
                {
                    if (lsbBSecondary.Items.FindByValue(li.Value) == null)
                    {
                        itemsecondary.Add(li.Value, li.Text);
                        temp_li = li;
                        isChangedAssignPrice = true;
                    }


                }
            }


            if (temp_li != null)
            {
                lstBPrimary.Items.Remove(temp_li);
            }

            lsbBSecondary.DataSource = itemsecondary;
            lsbBSecondary.DataBind();


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void btnAssignCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        showPanel("pnlAssign");

        try
        {
            //if (ddlPrice_Group_Type.SelectedValue == "0")
            if (lblCPriceGroupType.Text == "เอเยนต์")
            {


                /*List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "0");

                current.Insert(0, new dbo_PriceGroupClass() { Price_Group_ID = string.Empty, Price_Group_Name = "==ระบุ==" });
                current.Insert(current.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });

                ddlCurrent.DataSource = current;
                ddlCurrent.DataBind();

                ddlNew.DataSource = current;
                ddlNew.DataBind();

                string price_group_id = ddlCurrent.SelectedValue;*/

                isChangedAssignPrice = false;

                Dictionary<string, string> item = dbo_PriceGroupDataClass.GetAssignPriceGroup(ddlCurrent.SelectedValue);
                lstBPrimary.DataSource = item;
                lstBPrimary.DataBind();
                Dictionary<string, string> item1 = dbo_PriceGroupDataClass.GetAssignPriceGroup(ddlNew.SelectedValue);
                lsbBSecondary.DataSource = item1;
                lsbBSecondary.DataBind();
                //Dictionary<string, string> item1 = new Dictionary<string, string>();
                //lsbBSecondary.DataSource = item1;
                //lsbBSecondary.DataBind();

            }
            else
            {
                /*List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "1");

                current.Insert(0, new dbo_PriceGroupClass() { Price_Group_ID = string.Empty, Price_Group_Name = "==ระบุ==" });

                current.Insert(current.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });
                ddlCurrent.DataSource = current;
                ddlCurrent.DataBind();

                ddlNew.DataSource = current;
                ddlNew.DataBind();*/

                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

                string price_group_id = ddlCurrent.SelectedValue;
                isChangedAssignPrice = false;
                Dictionary<string, string> item = dbo_PriceGroupDataClass.GetAssignPriceGroupSP(ddlCurrent.SelectedValue, clsdbo_Agent.CV_Code);
                lstBPrimary.DataSource = item;
                lstBPrimary.DataBind();
                Dictionary<string, string> item1 = dbo_PriceGroupDataClass.GetAssignPriceGroupSP(ddlNew.SelectedValue, clsdbo_Agent.CV_Code);
                lsbBSecondary.DataSource = item1;
                lsbBSecondary.DataBind();

                //lsbBSecondary.DataSource = null;
                //lsbBSecondary.DataBind();
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

    protected void btnShowGrid_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        isChangedAssignPrice = false;
        ShowPriceGroup();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnAssignSave_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            char Price_Group_Type;
            if (isChangedAssignPrice == true)
            {
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                if (lblCPriceGroupType.Text == "เอเยนต์")
                {
                    Price_Group_Type = '0';
                }
                else
                {
                    Price_Group_Type = '1';
                }
                if (!string.IsNullOrEmpty(ddlNew.SelectedValue) && ddlNew.SelectedValue != "--")
                {
                    foreach (ListItem list_item in lsbBSecondary.Items)
                    {
                        dbo_PriceGroupAssignmentDataClass.Delete(list_item.Value);
                        dbo_PriceGroupAssignmentDataClass.Add(new dbo_PriceGroupAssignmentClass() { Assign_To = list_item.Value, Price_Group_ID = ddlNew.SelectedValue }, Price_Group_Type, User_ID);
                    }
                }
                if (ddlCurrent.SelectedValue != "--")
                {
                    foreach (ListItem li in lstBPrimary.Items)
                    {
                        dbo_PriceGroupAssignmentDataClass.Delete(li.Value);
                        dbo_PriceGroupAssignmentDataClass.Add(new dbo_PriceGroupAssignmentClass() { Assign_To = li.Value, Price_Group_ID = ddlCurrent.SelectedValue }, Price_Group_Type, User_ID);

                        //if (li.Attributes.Count > 0)
                        //{
                        //    dbo_PriceGroupAssignmentDataClass.Add(new dbo_PriceGroupAssignmentClass() { Assign_To = li.Value, Price_Group_ID = ddlCurrent.SelectedValue }, User_ID);
                        //}
                    }
                }
                else
                {
                    foreach (ListItem li in lstBPrimary.Items)
                    {
                        dbo_PriceGroupAssignmentDataClass.Delete(li.Value);

                    }
                }
                //if (!string.IsNullOrEmpty(ddlCurrent.SelectedValue) && ddlCurrent.SelectedValue == "--")
                //{
                //    foreach (ListItem li in lstBPrimary.Items)
                //    {
                //        dbo_PriceGroupAssignmentDataClass.Delete(li.Value);
                //        if (li.Attributes.Count > 0)
                //        {
                //            dbo_PriceGroupAssignmentDataClass.Add(new dbo_PriceGroupAssignmentClass() { Assign_To = li.Value, Price_Group_ID = ddlCurrent.SelectedValue }, User_ID);
                //        }
                //    }
                //}
                isChangedAssignPrice = false;

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("บันทึกสำเร็จ");
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

    protected void btnRemoveOne_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        //if (ddlCurrent.SelectedIndex > 0)
        //{
        try
        {
            /*if (ddlCurrent.SelectedIndex == ddlCurrent.Items.Count - 1)
            {

            }
            else
            {
                ddlCurrent.SelectedIndex = ddlCurrent.Items.Count - 1;
                ddlCurrent_SelectedIndexChanged(this, e);
            }*/



            //List<dbo_PriceGroupAssignmentClass> item = new List<dbo_PriceGroupAssignmentClass>();

            Dictionary<string, string> itemsecondary = new Dictionary<string, string>();

            foreach (ListItem list_item in lstBPrimary.Items)
            {
                itemsecondary.Add(list_item.Value, list_item.Text);
            }

            /*if (item == null)
            {
                item = new List<dbo_PriceGroupAssignmentClass>();
            }*/


            String primary_item = String.Empty;
            ListItem temp_li = null;

            foreach (ListItem li in lsbBSecondary.Items)
            {
                if (li.Selected == true)
                {
                    //li.Attributes.Add("removeThis", "true");
                    itemsecondary.Add(li.Value, li.Text);

                    temp_li = li;
                    isChangedAssignPrice = true;
                }
            }


            lsbBSecondary.Items.Remove(temp_li);


            lstBPrimary.DataSource = itemsecondary;
            lstBPrimary.DataBind();
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

        //}
        //else
        //{
        //    Show("กรุณาระบุประเภทกลุ่มราคา");
        //}
    }

    protected void btnAddAll_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {


            //List<dbo_PriceGroupAssignmentClass> item = new List<dbo_PriceGroupAssignmentClass>();

            Dictionary<string, string> itemsecondary = new Dictionary<string, string>();

            foreach (ListItem list_item in lsbBSecondary.Items)
            {

                itemsecondary.Add(list_item.Value, list_item.Text);
            }

            /*if (item == null)
            {
                item = new List<dbo_PriceGroupAssignmentClass>();
            }*/


            String primary_item = String.Empty;
            ListItem temp_li = null;



            foreach (ListItem li in lstBPrimary.Items)
            {
                //if (li.Selected == true)
                //{
                if (lsbBSecondary.Items.FindByValue(li.Value) == null)
                {
                    itemsecondary.Add(li.Value, li.Text);
                    temp_li = li;
                    isChangedAssignPrice = true;

                }
                // }
            }


            if (ddlCurrent.SelectedIndex > 0)
            {
                lstBPrimary.Items.Clear();
            }


            lsbBSecondary.DataSource = itemsecondary;
            lsbBSecondary.DataBind();


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

    protected void btnRemoveAll_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            /*if (ddlCurrent.SelectedIndex == ddlCurrent.Items.Count - 1)
            {

            }
            else
            {
                ddlCurrent.SelectedIndex = ddlCurrent.Items.Count - 1;
                ddlCurrent_SelectedIndexChanged(this, e);
            }*/



            //List<dbo_PriceGroupAssignmentClass> item = new List<dbo_PriceGroupAssignmentClass>();

            Dictionary<string, string> itemsecondary = new Dictionary<string, string>();

            foreach (ListItem list_item in lstBPrimary.Items)
            {
                itemsecondary.Add(list_item.Value, list_item.Text);
            }

            /*if (item == null)
            {
                item = new List<dbo_PriceGroupAssignmentClass>();
            }*/


            String primary_item = String.Empty;
            ListItem temp_li = null;

            foreach (ListItem li in lsbBSecondary.Items)
            {
                //if (li.Selected == true)
                //{
                // li.Attributes.Add("removeThis", "true");
                itemsecondary.Add(li.Value, li.Text);

                temp_li = li;
                isChangedAssignPrice = true;
                //}
            }




            lsbBSecondary.Items.Clear();


            lstBPrimary.DataSource = itemsecondary;
            lstBPrimary.DataBind();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            ddlPrice_Group_Type.ClearSelection();
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

            string con = (user_class.User_Group_ID.ToUpper() == "CP MEIJI".ToUpper() ? "0" : "1");

            ddlPrice_Group_Type.Items.FindByValue(con).Selected = true;
            txtPrice_Group_Name.Text = string.Empty;
            ShowPriceGroup();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
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

    protected void ddlProductID_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        //dbo_ProductListClass Product_List = dbo_ProductListDataClass.GetProductListByPriceGroupID(txtPrice_Group_ID.Text);// .GetProductIDNotInProductList(txtPrice_Group_ID.Text);
        try
        {


            if (txtPriceGroupType.Text == "เอเยนต์")
            {
                string ProductID = ddlProductID.SelectedValue;
                dbo_ProductClass value = dbo_ProductDataClass.Select_Record(ProductID);
                txtCP_Meiji_Price.Text = System.Convert.ToString(value.Agent_Price);
                txtVat.Text = System.Convert.ToString(value.Vat);
            }
            else
            {
                string ProductID = ddlSPProductID.SelectedValue;
                dbo_ProductClass value = dbo_ProductDataClass.Select_Record(ProductID);
                txtSPCPMeijiPrice.Text = System.Convert.ToString(value.CP_Meiji_Price);
                txtSPPrice.Text = System.Convert.ToString(value.SP_Price);
                txtSPVat.Text = System.Convert.ToString(value.Vat);
                txtSPPoint.Text = System.Convert.ToString(value.Point);
            }

            txtProduct_Name.Text = ddlProductID.SelectedItem.Text;
            txtSPProduct_Name.Text = ddlSPProductID.SelectedItem.Text;

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

    protected void ddlCurrent_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            if (isChangedAssignPrice == false)
            {
                string ddlText = ddlNew.Text;

                //if (ddlPrice_Group_Type.SelectedValue == "0")
                if (lblCPriceGroupType.Text == "เอเยนต์")
                {
                    //string price_group_id = ddlCurrent.SelectedValue;

                    Dictionary<string, string> item = dbo_PriceGroupDataClass.GetAssignPriceGroup(ddlCurrent.SelectedValue);
                    lstBPrimary.DataSource = item;
                    lstBPrimary.DataBind();

                    /*List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "0");
                    dbo_PriceGroupClass i = current.FirstOrDefault(f => f.Price_Group_ID == price_group_id);
                    current.Remove(i);
                    current.Insert(0, new dbo_PriceGroupClass() { Price_Group_ID = string.Empty, Price_Group_Name = "==ระบุ==" });
                    current.Insert(current.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });

                    ddlNew.DataSource = current;
                    ddlNew.DataBind();*/


                }
                else
                {
                    //string price_group_id = ddlCurrent.SelectedValue;

                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                    dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

                    Dictionary<string, string> item = dbo_PriceGroupDataClass.GetAssignPriceGroupSP(ddlCurrent.SelectedValue, clsdbo_Agent.CV_Code);
                    lstBPrimary.DataSource = item;
                    lstBPrimary.DataBind();

                    /*List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "1");
                    dbo_PriceGroupClass i = current.FirstOrDefault(f => f.Price_Group_ID == price_group_id);
                    current.Remove(i);
                    current.Insert(0, new dbo_PriceGroupClass() { Price_Group_ID = string.Empty, Price_Group_Name = "==ระบุ==" });
                    current.Insert(current.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });

                    ddlNew.DataSource = current;
                    ddlNew.DataBind();*/

                }
                //ddlNew.Text = ddlText;
            }

            else
            {
                Show("ข้อมูลกำหนดกลุ่มราคามีการเปลี่ยนเปลี่ยน กรุณาบันทึกข้อมูลก่อนดำเนินการต่อ");
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

    protected void ddlNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            if (isChangedAssignPrice == false)
            {
                //string ddlText = ddlCurrent.Text;

                //if (ddlPrice_Group_Type.SelectedValue == "0")
                if (lblCPriceGroupType.Text == "เอเยนต์")
                {
                    string id = ddlNew.SelectedValue;
                    Dictionary<string, string> item = dbo_PriceGroupDataClass.GetAssignPriceGroup(ddlNew.SelectedValue);
                    lsbBSecondary.DataSource = item;
                    lsbBSecondary.DataBind();

                    /*List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "0");
                    dbo_PriceGroupClass i = current.FirstOrDefault(f => f.Price_Group_ID == id);
                    current.Remove(i);
                    current.Insert(0, new dbo_PriceGroupClass() { Price_Group_ID = string.Empty, Price_Group_Name = "==ระบุ==" });
                    current.Insert(current.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });

                    ddlCurrent.DataSource = current;
                    ddlCurrent.DataBind();*/
                }
                else
                {
                    string id = ddlNew.SelectedValue;

                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                    dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

                    Dictionary<string, string> item = dbo_PriceGroupDataClass.GetAssignPriceGroupSP(ddlNew.SelectedValue, clsdbo_Agent.CV_Code);
                    lsbBSecondary.DataSource = item;
                    lsbBSecondary.DataBind();

                    /*List<dbo_PriceGroupClass> current = dbo_PriceGroupDataClass.Search(string.Empty, "1");
                    dbo_PriceGroupClass i = current.FirstOrDefault(f => f.Price_Group_ID == id);
                    current.Remove(i);
                    current.Insert(0, new dbo_PriceGroupClass() { Price_Group_ID = string.Empty, Price_Group_Name = "==ระบุ==" });
                    current.Insert(current.Count, new dbo_PriceGroupClass() { Price_Group_ID = "--", Price_Group_Name = "ยังไม่กำหนดกลุ่มราคา" });

                    ddlCurrent.DataSource = current;
                    ddlCurrent.DataBind();*/
                }

                //ddlCurrent.Text = ddlText;
            }
            else
            {
                Show("ข้อมูลกำหนดกลุ่มราคามีการเปลี่ยนเปลี่ยน กรุณาบันทึกข้อมูลก่อนดำเนินการต่อ");
                //string GetPreviousValue = ddlCurrent.Items[CurrentPreviousIndex].Text;
                // ddlCurrent.Items.IndexOf(ViewState["CurrentIndex"]);

                ddlNew.SelectedValue = ddlNew.Items.FindByValue(ViewState["NewIndex"].ToString()).Value;
                //ddlCurrent.SelectedIndex = ViewState["CurrentIndex"];
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

    protected void ddlPrice_Group_Type_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lstBPrimary_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void chkItemStandard_CheckedChanged(object sender, EventArgs e)
    {
        /*int NewEditIndex = (int)ViewState["NewEditIndex"];


        CheckBox editchk = (CheckBox)GridViewPrice_Group.Rows[NewEditIndex].FindControl("chkEditStandard");

        if (editchk.Checked)
        {
            foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
            {
                CheckBox chk = (CheckBox)currentRow.FindControl("chkItemStandard");

                if (currentRow.RowIndex != NewEditIndex)
                {
                    chk.Checked = false;
                }
            }
        }*/
    }

    protected void chkFooterStandard_CheckedChanged(object sender, EventArgs e)
    {
        /*CheckBox editchk = (CheckBox)GridViewPrice_Group.FooterRow.FindControl("chkFooterStandard");

        if (editchk.Checked)
        {
            foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
            {
                CheckBox chk = (CheckBox)currentRow.FindControl("chkItemStandard");
                chk.Checked = false;
            }
        }*/
    }

    #endregion

    #region GridView Events
    protected void GridViewPrice_Group_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        GridViewPrice_Group.EditIndex = -1;
        GridViewPrice_Group.ShowFooter = false;


        try
        {


            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

            ViewState["Price_Group_Type"] = ddlPrice_Group_Type.SelectedValue;

            if (user_class.User_Group_ID == "Agent")
            {
                List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
                List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
                foreach (dbo_PriceGroupClass d in dt)
                {
                    if (d.StandardPrice == true)
                    {
                        dtData.Add(d);
                    }
                    else
                    {
                        if (d.CV_Code == user_class.CV_CODE)
                        {
                            dtData.Add(d);
                        }
                    }
                }

                GridViewPrice_Group.DataSource = dtData;//dt.Where(f => f.CV_Code == user_class.CV_CODE || f.StandardPrice == true);
                GridViewPrice_Group.DataBind();
            }
            else
            {
                List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
                GridViewPrice_Group.DataSource = dt;
                GridViewPrice_Group.DataBind();
            }
            if (user_class.User_Group_ID == "Agent")
            {
                foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
                {

                    CheckBox chk2 = (CheckBox)currentRow.FindControl("chkItemStandard");
                    LinkButton lnk = (LinkButton)currentRow.FindControl("lnkB_Product_List");
                    if (chk2 != null)
                    {
                        if (chk2.Checked)
                        {
                            //lnk.Visible = false;
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
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        /*
        List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ViewState["Price_Group_Type"].ToString());
        GridViewPrice_Group.DataSource = dt;
        GridViewPrice_Group.DataBind();
        */

    }

    protected void GridViewPrice_Group_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (e.CommandName == "EditProductList")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string _txtPrice_Group_ID = lnkView.CommandArgument;

            dbo_PriceGroupClass item = dbo_PriceGroupDataClass.Select_Record(_txtPrice_Group_ID);
            ShowProductList(item);

        }
        else if (e.CommandName == "AddNew")
        {

            // add new 
            dbo_PriceGroupClass clsdbo_PriceGroup = new dbo_PriceGroupClass();

            int RowIndex = Convert.ToInt32((GridViewPrice_Group.Rows.Count + 1).ToString());
            string GroupPriceType = string.Empty;

            dbo_PriceGroupClass oclsdbo_PriceGroup = new dbo_PriceGroupClass();
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            //int max__txtPrice_Group_ID = 0;
            CheckBox chk = null;
            TextBox _txtPrice_Group_Name = (TextBox)GridViewPrice_Group.FooterRow.FindControl("txtNewPrice_Group_Name");
            Label _txtNewGroupPriceType = (Label)GridViewPrice_Group.FooterRow.FindControl("txtNewGroupPriceType");
            bool isUpdated = true;

            if (_txtNewGroupPriceType.Text == "เอเยนต์")
            {
                GroupPriceType = "0";//"เอเยนต์";
            }
            else
            {
                GroupPriceType = "1";// "สาว"; 
            }
            if (!string.IsNullOrEmpty(_txtPrice_Group_Name.Text))
            {
                CheckBox chk1 = (CheckBox)GridViewPrice_Group.FooterRow.FindControl("chkFooterStandard");
                if (chk1.Checked == true)
                {

                    foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
                    {
                        if (currentRow.RowIndex == RowIndex)
                        {
                            // TextBox _txtPrice_Group_ID = max__txtPrice_Group_ID + 1;

                        }
                        else
                        {
                            chk = (CheckBox)currentRow.FindControl("chkItemStandard");
                            if (chk.Checked == true)
                            {
                                System.Threading.Thread.Sleep(500);
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                                Show("มีการกำหนดราคามาตรฐานแล้ว");
                                isUpdated = false;
                                break;
                            }
                        }


                    }
                }

                if (isUpdated == true)
                {
                    chk = (CheckBox)GridViewPrice_Group.FooterRow.FindControl("chkFooterStandard");
                    string _Price_Group_ID = GenerateID.Price_Group_ID();
                    clsdbo_PriceGroup.Price_Group_ID = _Price_Group_ID;
                    clsdbo_PriceGroup.Price_Group_Name = _txtPrice_Group_Name.Text;
                    clsdbo_PriceGroup.Price_Group_Type = GroupPriceType;
                    //clsdbo_PriceGroup.Price_Group_Type = ddlPrice_Group_Type.SelectedValue;
                    clsdbo_PriceGroup.StandardPrice = chk.Checked;

                    dbo_PriceGroupDataClass.Add(clsdbo_PriceGroup, User_ID);

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("บันทึกสำเร็จ");


                    GridViewPrice_Group.ShowFooter = false;

                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                    dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);


                    ViewState["Price_Group_Type"] = ddlPrice_Group_Type.SelectedValue;

                    if (user_class.User_Group_ID == "Agent")
                    {
                        List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
                        List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
                        foreach (dbo_PriceGroupClass d in dt)
                        {
                            if (d.StandardPrice == true)
                            {
                                dtData.Add(d);
                            }
                            else
                            {
                                if (d.CV_Code == user_class.CV_CODE)
                                {
                                    dtData.Add(d);
                                }
                            }
                        }

                        GridViewPrice_Group.DataSource = dtData;//dt.Where(f => f.CV_Code == user_class.CV_CODE || f.StandardPrice == true);
                        GridViewPrice_Group.DataBind();
                    }
                    else
                    {
                        List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
                        GridViewPrice_Group.DataSource = dt;
                        GridViewPrice_Group.DataBind();
                    }
                    if (user_class.User_Group_ID == "Agent")
                    {
                        foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
                        {

                            CheckBox chk2 = (CheckBox)currentRow.FindControl("chkItemStandard");
                            LinkButton lnk = (LinkButton)currentRow.FindControl("lnkB_Product_List");
                            if (chk2 != null)
                            {
                                if (chk2.Checked)
                                {
                                    //lnk.Visible = false;
                                }
                            }
                        }
                    }

                    /*
                    List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ViewState["Price_Group_Type"].ToString());
                    GridViewPrice_Group.DataSource = dt;
                    GridViewPrice_Group.DataBind();
                      */
                }
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }
        }
        else if (e.CommandName == "_Delete")
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            if (user_class.User_Group_ID == "Agent")
            {
                foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
                {
                    CheckBox chk2 = (CheckBox)currentRow.FindControl("chkItemStandard");
                    if (chk2 != null)
                    {
                        if (chk2.Checked)
                        {
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            Show("ไม่สามารถลบราคามาตรฐานได้");
                        }
                    }
                }
                
            }
            else
            {

                LinkButton lnkView = (LinkButton)e.CommandSource;
                string PriceGroup = lnkView.CommandArgument;

                dbo_PriceGroupDataClass.Delete(PriceGroup);

                List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ViewState["Price_Group_Type"].ToString());
                GridViewPrice_Group.DataSource = dt;
                GridViewPrice_Group.DataBind();

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                //dbo_ProductListDataClass.Delete(Product_ID);

                //dbo_PriceGroupClass item = dbo_PriceGroupDataClass.Select_Record(txtSearchPriceGroupID.Text);
                //ShowProductList(item);
            }
        }
    }

    protected void grdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            if (e.CommandName == "EditProductList")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string Product_List_ID = lnkView.CommandArgument;

                ShowProductListDetails(Product_List_ID, "Edit");
            }
            else if (e.CommandName == "View")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string Product_ID = lnkView.CommandArgument;


                ShowProductListDetails(Product_ID, "View");
                //Session["Product_ID"] = Product_ID;
                //Response.Redirect("~/Views/Product_List.aspx", false);

            }
            else if (e.CommandName == "_Delete")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string Product_ID = lnkView.CommandArgument;
                
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                dbo_PriceGroupClass item1 = dbo_PriceGroupDataClass.Select_Record(txtSearchPriceGroupID.Text);
                if (user_class.User_Group_ID == "Agent" && item1.StandardPrice == true)
                {

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ไม่สามารถลบสินค้าในราคามาตรฐานได้");


                }
                else
                {
                    dbo_ProductListDataClass.Delete(Product_ID);

                    dbo_PriceGroupClass item = dbo_PriceGroupDataClass.Select_Record(txtSearchPriceGroupID.Text);
                    ShowProductList(item);
                }
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

    protected void GridViewPrice_Group_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        bool stdFlag = false;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        if (user_class.User_Group_ID == "Agent")
        {         
                CheckBox chk = (CheckBox)GridViewPrice_Group.Rows[e.RowIndex].FindControl("chkItemStandard");
                if (chk != null && chk.Checked == true)
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ไม่สามารถลบราคามาตรฐานได้");
                    stdFlag = true;
                    
                }
            
        }
        if (stdFlag==false)
        {
            int index = e.RowIndex;
            Label lblPrice_Group_ID = (Label)GridViewPrice_Group.Rows[e.RowIndex].FindControl("lblPrice_Group_ID");


            dbo_PriceGroupClass pricegroup = dbo_PriceGroupDataClass.Select_Record(lblPrice_Group_ID.Text);

            List<dbo_ProductListClass> value = dbo_ProductListDataClass.GetProductListByPriceGroupID(lblPrice_Group_ID.Text);


            List<dbo_PriceGroupAssignmentClass> item1 = dbo_PriceGroupAssignmentDataClass.Search(lblPrice_Group_ID.Text);

            foreach (dbo_PriceGroupAssignmentClass assign in item1)
            {
                dbo_PriceGroupAssignmentDataClass.Delete(assign.Assign_To);
            }


            foreach (dbo_ProductListClass _value in value)
            {
                dbo_ProductListDataClass.Delete(_value.Product_ID);
            }

            dbo_PriceGroupDataClass.Delete(lblPrice_Group_ID.Text);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");

            //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);


            ViewState["Price_Group_Type"] = ddlPrice_Group_Type.SelectedValue;

            if (user_class.User_Group_ID == "Agent")
            {
                List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
                List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
                foreach (dbo_PriceGroupClass d in dt)
                {
                    if (d.StandardPrice == true)
                    {
                        dtData.Add(d);
                    }
                    else
                    {
                        if (d.CV_Code == user_class.CV_CODE)
                        {
                            dtData.Add(d);
                        }
                    }
                }

                GridViewPrice_Group.DataSource = dtData;//dt.Where(f => f.CV_Code == user_class.CV_CODE || f.StandardPrice == true);
                GridViewPrice_Group.DataBind();
            }
            else
            {
                List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
                GridViewPrice_Group.DataSource = dt;
                GridViewPrice_Group.DataBind();
            }


            /*
            List<dbo_PriceGroupClass> item = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ViewState["Price_Group_Type"].ToString());
            GridViewPrice_Group.DataSource = item;
            GridViewPrice_Group.DataBind();
             * */
        }
    }

    protected void GridViewPrice_Group_RowEditing(object sender, GridViewEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        GridViewPrice_Group.ShowFooter = false;
        GridViewPrice_Group.EditIndex = e.NewEditIndex;
        
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

        ViewState["Price_Group_Type"] = ddlPrice_Group_Type.SelectedValue;

        if (user_class.User_Group_ID == "Agent")
        {
            List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
            List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
            foreach (dbo_PriceGroupClass d in dt)
            {
                if (d.StandardPrice == true)
                {
                    dtData.Add(d);





                }
                else
                {
                    if (d.CV_Code == user_class.CV_CODE)
                    {
                        dtData.Add(d);
                    }
                }
            }

            GridViewPrice_Group.DataSource = dtData;//dt.Where(f => f.CV_Code == user_class.CV_CODE || f.StandardPrice == true);
            GridViewPrice_Group.DataBind();

            if (user_class.User_Group_ID == "Agent")
            {

                    CheckBox chk = (CheckBox)GridViewPrice_Group.Rows[e.NewEditIndex].FindControl("chkEditStandard");
                    if (chk != null && chk.Checked == true)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("ไม่สามารถแก้ไขราคามาตรฐานได้");
                        chk.Enabled = false;
                        TextBox txt = (TextBox)GridViewPrice_Group.Rows[e.NewEditIndex].FindControl("txtEditPrice_Group_Name");
                        txt.Enabled = false;
                    }

                    /*LinkButton lnk = (LinkButton)currentRow.FindControl("lnkB_Product_List");
                    TextBox txt = (TextBox)currentRow.FindControl("txtEditPrice_Group_Name");
                    if (chk != null)
                    {
                        chk.Enabled = false;
                        //lnk.Visible  = false;
                        txt.Enabled = false;


                    }
                    CheckBox chk2 = (CheckBox)currentRow.FindControl("chkItemStandard");
                    if (chk2 != null)
                    {
                        if (chk2.Checked)
                        {
                            lnk.Visible = false;
                        }
                    }*/
                    
                }


        }
        else
        {
            List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
            GridViewPrice_Group.DataSource = dt;
            GridViewPrice_Group.DataBind();
            
        }

        ViewState["NewEditIndex"] = e.NewEditIndex;

    }

    protected void GridViewPrice_Group_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_PriceGroupClass oclsdbo_PriceGroup = new dbo_PriceGroupClass();
        int RowIndex = Convert.ToInt32((e.RowIndex).ToString());

        TextBox _txtPrice_ = null;
        Label _txtGroupPriceType_ = null;
        _txtPrice_ = (TextBox)GridViewPrice_Group.Rows[RowIndex].FindControl("txtEditPrice_Group_Name");
        _txtGroupPriceType_ = (Label)GridViewPrice_Group.Rows[RowIndex].FindControl("lblGroupPriceType");
        bool isUpdated = true;

        if (!string.IsNullOrEmpty(_txtPrice_.Text))
        {
            CheckBox chk1 = (CheckBox)GridViewPrice_Group.Rows[RowIndex].FindControl("chkEditStandard");
            if (chk1.Checked == true)
            {
                foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
                {
                    //Label _txtPrice_Group_ID = null;
                    //TextBox _txtPrice_Group_Name = null;
                    //Label _LabelPrice_Group_Name = null;
                    //Label _txtGroupPriceType = null;
                    //Label _LabelGroupPriceType = null;

                    CheckBox chk = null;

                    if (currentRow.RowIndex == RowIndex)
                    {
                        //_txtPrice_Group_ID = (Label)GridViewPrice_Group.Rows[RowIndex].FindControl("lblPrice_Group_ID");

                        //_txtPrice_Group_Name = (TextBox)GridViewPrice_Group.Rows[RowIndex].FindControl("txtEditPrice_Group_Name");
                        //_txtGroupPriceType = (Label)GridViewPrice_Group.Rows[RowIndex].FindControl("lblGroupPriceType");
                        //chk = (CheckBox)GridViewPrice_Group.Rows[RowIndex].FindControl("chkEditStandard");

                    }
                    else
                    {
                        //_txtPrice_Group_ID = (Label)currentRow.FindControl("lblPrice_Group_ID");
                        //_LabelPrice_Group_Name = (Label)currentRow.FindControl("LabelPrice_Group_Name");
                        //_LabelGroupPriceType = (Label)currentRow.FindControl("lblGroupPriceType");
                        chk = (CheckBox)currentRow.FindControl("chkItemStandard");
                        if (chk.Checked == true)
                        {
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            Show("มีการกำหนดราคามาตรฐานแล้ว");
                            isUpdated = false;
                            break;
                        }
                    }

                    /*oclsdbo_PriceGroup = dbo_PriceGroupDataClass.Select_Record(_txtPrice_Group_ID.Text);

                    oclsdbo_PriceGroup.Price_Group_ID = _txtPrice_Group_ID.Text;

                    oclsdbo_PriceGroup.Price_Group_Name = (currentRow.RowIndex == RowIndex ? _txtPrice_Group_Name.Text : _LabelPrice_Group_Name.Text);
                    oclsdbo_PriceGroup.Price_Group_Type = (currentRow.RowIndex == RowIndex ? _txtGroupPriceType.Text : _LabelGroupPriceType.Text);
                    oclsdbo_PriceGroup.StandardPrice = chk.Checked;
                    string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    dbo_PriceGroupDataClass.Update(oclsdbo_PriceGroup, User_ID);*/
                }
            }
            if (isUpdated == true)
            {
                Label _txtPrice_Group_ID = null;
                TextBox _txtPrice_Group_Name = null;
                //Label _LabelPrice_Group_Name = null;
                Label _txtGroupPriceType = null;
                //  Label _LabelGroupPriceType = null;
                CheckBox _chk = null;

                _txtPrice_Group_ID = (Label)GridViewPrice_Group.Rows[RowIndex].FindControl("lblPrice_Group_ID");

                _txtPrice_Group_Name = (TextBox)GridViewPrice_Group.Rows[RowIndex].FindControl("txtEditPrice_Group_Name");
                _txtGroupPriceType = (Label)GridViewPrice_Group.Rows[RowIndex].FindControl("lblGroupPriceType");
                _chk = (CheckBox)GridViewPrice_Group.Rows[RowIndex].FindControl("chkEditStandard");

                oclsdbo_PriceGroup = dbo_PriceGroupDataClass.Select_Record(_txtPrice_Group_ID.Text);

                oclsdbo_PriceGroup.Price_Group_ID = _txtPrice_Group_ID.Text;

                oclsdbo_PriceGroup.Price_Group_Name = _txtPrice_Group_Name.Text;
                oclsdbo_PriceGroup.Price_Group_Type = _txtGroupPriceType.Text;
                oclsdbo_PriceGroup.StandardPrice = _chk.Checked;
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                dbo_PriceGroupDataClass.Update(oclsdbo_PriceGroup, User_ID);
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

                GridViewPrice_Group.EditIndex = -1;
                GridViewPrice_Group.ShowFooter = false;


                if (user_class.User_Group_ID == "Agent")
                {
                    List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
                    List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
                    foreach (dbo_PriceGroupClass d in dt)
                    {
                        if (d.StandardPrice == true)
                        {
                            dtData.Add(d);
                        }
                        else
                        {
                            if (d.CV_Code == user_class.CV_CODE)
                            {
                                dtData.Add(d);
                            }
                        }
                    }

                    if (dtData.Count > 0)
                    {
                        GridViewPrice_Group.DataSource = dtData;//dt.Where(f => f.CV_Code == user_class.CV_CODE || f.StandardPrice == true);
                        GridViewPrice_Group.DataBind();

                        GridViewPrice_Group.Visible = true;
                        pnlNoRec.Visible = false;
                    }
                    else
                    {
                        GridViewPrice_Group.Visible = false;
                        pnlNoRec.Visible = true;
                    }
                }
                else
                {
                    List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);

                    if (dt.Count > 0)
                    {
                        GridViewPrice_Group.DataSource = dt;
                        GridViewPrice_Group.DataBind();

                        GridViewPrice_Group.Visible = true;
                        pnlNoRec.Visible = false;
                    }
                    else
                    {
                        GridViewPrice_Group.Visible = false;
                        pnlNoRec.Visible = true;
                    }
                }

                if (user_class.User_Group_ID == "Agent")
                {
                    foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
                    {

                        CheckBox chk = (CheckBox)currentRow.FindControl("chkItemStandard");
                        LinkButton lnk = (LinkButton)currentRow.FindControl("lnkB_Product_List");
                        if (chk != null)
                        {
                            if (chk.Checked)
                            {
                                //lnk.Visible = false;
                            }
                        }
                    }
                }



                /*
                GridViewPrice_Group.EditIndex = -1;
                GridViewPrice_Group.ShowFooter = false;
                List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ViewState["Price_Group_Type"].ToString());
                GridViewPrice_Group.DataSource = dt;
                GridViewPrice_Group.DataBind();
                */

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
        }
        else
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
        }

    }

    protected void GridViewPrice_Group_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewPrice_Group.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewPrice_Group.PageIndex = pageList.SelectedIndex;
        ShowPriceGroup();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewPrice_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewPrice_Group.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewPrice_Group.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewPrice_Group.PageIndex)
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
            int currentPage = GridViewPrice_Group.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewPrice_Group.PageCount.ToString();

        }
    }
    #endregion

    #region Method
    private void ShowPriceGroup()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlPriceGroup.Visible = true;
        pnlGrid.Visible = false;
        pnlForm.Visible = false;
        pnlAssign.Visible = false;

        try
        {


            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

            string con = (user_class.User_Group_ID.ToUpper() == "CP MEIJI".ToUpper() ? "0" : "1");

            //ddlPrice_Group_Type.Items.FindByValue(con).Selected = true;
            //if (ddlPrice_Group_Type.SelectedValue == "1" && user_class.Position == "สาวส่งนม")
            if (con == "1")
            //if (ddlPrice_Group_Type.Enabled == false )
            {
                ddlPrice_Group_Type.SelectedValue = "1";
                ddlPrice_Group_Type.Enabled = false;
            }

            //string con = ddlPrice_Group_Type.SelectedValue;
            ViewState["Price_Group_Type"] = ddlPrice_Group_Type.SelectedValue;

            if (user_class.User_Group_ID == "Agent")
            {
                List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);
                List<dbo_PriceGroupClass> dtData = new List<dbo_PriceGroupClass>();
                foreach (dbo_PriceGroupClass d in dt)
                {
                    if (d.StandardPrice == true)
                    {
                        dtData.Add(d);
                    }
                    else
                    {
                        if (d.CV_Code == user_class.CV_CODE)
                        {
                            dtData.Add(d);
                        }
                    }
                }

                if (dtData.Count > 0)
                {
                    GridViewPrice_Group.DataSource = dtData;//dt.Where(f => f.CV_Code == user_class.CV_CODE || f.StandardPrice == true);
                    GridViewPrice_Group.DataBind();

                    GridViewPrice_Group.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    GridViewPrice_Group.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }
            else
            {
                List<dbo_PriceGroupClass> dt = dbo_PriceGroupDataClass.Search(txtPrice_Group_Name.Text, ddlPrice_Group_Type.SelectedValue);

                if (dt.Count > 0)
                {
                    GridViewPrice_Group.DataSource = dt;
                    GridViewPrice_Group.DataBind();

                    GridViewPrice_Group.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    GridViewPrice_Group.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }

            if (user_class.User_Group_ID == "Agent")
            {
                foreach (GridViewRow currentRow in GridViewPrice_Group.Rows)
                {

                    CheckBox chk = (CheckBox)currentRow.FindControl("chkItemStandard");
                    LinkButton lnk = (LinkButton)currentRow.FindControl("lnkB_Product_List");
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            //lnk.Visible = false;
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
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    private void ShowProductList(dbo_PriceGroupClass item)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            pnlPriceGroup.Visible = false;
            pnlGrid.Visible = true;
            pnlForm.Visible = false;

            txtSearchPriceGroupID.Text = item.Price_Group_ID;
            txtSearchPriceGroupName.Text = item.Price_Group_Name;

            txtPrice_Group_ID.Text = item.Price_Group_ID;
            txtPriceGroupName.Text = item.Price_Group_Name;
            txtPriceGroupType.Text = item.Price_Group_Type;

            List<dbo_ProductListClass> listofproductList = dbo_ProductListDataClass.GetProductListByPriceGroupID(item.Price_Group_ID);

            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

            //if (user_class.User_Group_ID.ToUpper() == "CP MEIJI".ToUpper())
            if (txtPriceGroupType.Text == "เอเยนต์")
            {

                grdProduct_Agent.DataSource = null;
                grdProduct_Agent.DataBind();

                grdProduct_Agent.DataSource = listofproductList;
                grdProduct_Agent.DataBind();
                grdProduct_Agent.Visible = true;
                grdProduct_SP.Visible = false;
            }
            else
            {
                grdProduct_SP.DataSource = listofproductList;
                grdProduct_SP.DataBind();
                grdProduct_Agent.Visible = false;
                grdProduct_SP.Visible = true;
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

    private void ShowProductListDetails(string Product_List_ID, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            pnlPriceGroup.Visible = false;
            pnlGrid.Visible = false;
            pnlForm.Visible = true;

            Dictionary<string, string> product = dbo_ProductListDataClass.GetProductIDNotInProductList(txtPrice_Group_ID.Text);
            ddlProductID.DataSource = product;
            ddlProductID.DataBind();

            ddlSPProductID.DataSource = product;
            ddlSPProductID.DataBind();

            txtProduct_ID.Text = string.Empty;
            txtProduct_Name.Text = string.Empty;
            txtCP_Meiji_Price.Text = string.Empty;

            txtVat.Text = string.Empty;
            txtStart_Effective_Date.Text = string.Empty;
            txtEnd_Effective_Date.Text = string.Empty;
            txtStartSaleDate.Text = string.Empty;

            txtProduct_ID.Visible = false;
            ddlProductID.Visible = true;

            txtSPProduct_ID.Text = string.Empty;
            txtSPProduct_Name.Text = string.Empty;
            txtSPCPMeijiPrice.Text = string.Empty;
            txtSPPrice.Text = string.Empty;
            txtSPVat.Text = string.Empty;
            txtSPPoint.Text = string.Empty;
            txtSPStartSaleDate.Text = string.Empty;
            txtSPStart_Effective_Date.Text = string.Empty;
            txtSPEnd_Effective_Date.Text = string.Empty;



            dbo_PriceGroupClass price_group = dbo_PriceGroupDataClass.Select_Record(txtPrice_Group_ID.Text);
            txtSPPrice_Group_ID.Text = price_group.Price_Group_ID;
            txtSPPriceGroupName.Text = price_group.Price_Group_Name;
            txtSPPriceGroupType.Text = price_group.Price_Group_Type;
            txtPrice_Group_ID.Text = price_group.Price_Group_ID;
            txtPriceGroupName.Text = price_group.Price_Group_Name;
            txtPriceGroupType.Text = price_group.Price_Group_Type;
            ddlSPProductID.ClearSelection();

            if (!string.IsNullOrEmpty(Product_List_ID))
            {
                dbo_ProductListClass clsdbo_ProductList = dbo_ProductListDataClass.Select_Record(Product_List_ID);

                txtProduct_List_ID.Text = System.Convert.ToString(clsdbo_ProductList.Product_List_ID);
                txtSPProduct_List_ID.Text = System.Convert.ToString(clsdbo_ProductList.Product_List_ID);

                txtPrice_Group_ID.Text = System.Convert.ToString(clsdbo_ProductList.Price_Group_ID);
                txtProduct_ID.Text = System.Convert.ToString(clsdbo_ProductList.Product_ID);


                txtProduct_ID.Visible = true;
                ddlProductID.Visible = false;

                ddlSPProductID.Visible = false;
                txtSPProduct_ID.Visible = true;

                txtProduct_Name.Text = System.Convert.ToString(clsdbo_ProductList.Product_Name);
                txtCP_Meiji_Price.Text = System.Convert.ToString(clsdbo_ProductList.Agent_Price);
                txtSPProduct_ID.Text = clsdbo_ProductList.Product_ID;


                if (clsdbo_ProductList.Product_ID != null)
                {
                    ddlSPProductID.Items.FindByText(clsdbo_ProductList.Product_ID.Trim() + " " + clsdbo_ProductList.Product_Name.Trim()).Selected = true;
                    ddlProductID.Items.FindByText(clsdbo_ProductList.Product_ID.Trim() + " " + clsdbo_ProductList.Product_Name.Trim()).Selected = true;
                    txtProduct_ID.Text = clsdbo_ProductList.Product_ID.Trim() + " " + clsdbo_ProductList.Product_Name.Trim();
                    txtSPProduct_ID.Text = clsdbo_ProductList.Product_ID.Trim() + " " + clsdbo_ProductList.Product_Name.Trim();

                }





                txtSPProduct_Name.Text = clsdbo_ProductList.Product_Name;
                txtVat.Text = (clsdbo_ProductList.Vat == null ? string.Empty : clsdbo_ProductList.Vat.ToString()); // System.Convert.ToString();

                txtSPCPMeijiPrice.Text = clsdbo_ProductList.CP_Meiji_Price.ToString();
                txtSPPrice.Text = clsdbo_ProductList.SP_Price.ToString();
                txtSPVat.Text = (clsdbo_ProductList.Vat == null ? string.Empty : clsdbo_ProductList.Vat.ToString());
                txtSPPoint.Text = clsdbo_ProductList.Point.ToString();




                txtStart_Effective_Date.Text = (clsdbo_ProductList.Start_Effective_Date.HasValue ? clsdbo_ProductList.Start_Effective_Date.Value.ToShortDateString() : string.Empty);
                txtEnd_Effective_Date.Text = (clsdbo_ProductList.End_Effective_Date.HasValue ? clsdbo_ProductList.End_Effective_Date.Value.ToShortDateString() : string.Empty);
                txtStartSaleDate.Text = (clsdbo_ProductList.Product_Effective_Date.HasValue ? clsdbo_ProductList.Product_Effective_Date.Value.ToShortDateString() : string.Empty);



                txtSPStart_Effective_Date.Text = (clsdbo_ProductList.Start_Effective_Date.HasValue ? clsdbo_ProductList.Start_Effective_Date.Value.ToShortDateString() : string.Empty);
                txtSPEnd_Effective_Date.Text = (clsdbo_ProductList.End_Effective_Date.HasValue ? clsdbo_ProductList.End_Effective_Date.Value.ToShortDateString() : string.Empty);
                txtSPStartSaleDate.Text = (clsdbo_ProductList.Product_Effective_Date.HasValue ? clsdbo_ProductList.Product_Effective_Date.Value.ToShortDateString() : string.Empty);
            }
            else
            {
                ddlProductID.Visible = true;
                txtProduct_ID.Visible = false;
                ddlSPProductID.Visible = true;
                txtSPProduct_ID.Visible = false;
            }

            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

            if (txtPriceGroupType.Text == "เอเยนต์")
            //if (user_class.User_Group_ID.ToUpper() == "CP MEIJI".ToUpper())
            {
                pnlAgent.Visible = true;
                pnlSP.Visible = false;

            }
            else
            {
                pnlAgent.Visible = false;
                pnlSP.Visible = true;
            }


            bool enable = Mode != "View";


            ddlProductID.Enabled = enable;

            txtProduct_Name.Enabled = false;
            txtSPProduct_Name.Enabled = false;

            txtCP_Meiji_Price.Enabled = enable;
            txtVat.Enabled = enable;
            txtStartSaleDate.Enabled = enable;
            txtStart_Effective_Date.Enabled = enable;
            txtEnd_Effective_Date.Enabled = enable;
            ddlSPProductID.Enabled = enable;
            txtSPCPMeijiPrice.Enabled = enable;
            txtSPPrice.Enabled = enable;
            txtSPVat.Enabled = enable;
            txtSPPoint.Enabled = enable;
            txtSPStartSaleDate.Enabled = enable;
            txtSPStart_Effective_Date.Enabled = enable;
            txtSPEnd_Effective_Date.Enabled = enable;






            if (Mode == "View")
            {
                btnSave.Visible = true;
                btnSave.Text = "แก้ไข";
                ButtonCancel.Text = "กลับไปหน้าค้นหา";
                btnSaveMode.Value = "แก้ไข";

            }
            else if (Mode == "Edit")
            {
                btnSave.Visible = true;
                btnSave.Text = "บันทึก";
                ButtonCancel.Text = "ยกเลิก";
                btnSaveMode.Value = "แก้ไข";

            }
            else if (string.IsNullOrEmpty(Mode))
            {
                btnSave.Visible = true;
                btnSave.Text = "บันทึก";
                ButtonCancel.Text = "ยกเลิก";
                btnSaveMode.Value = "บันทึก";
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

    private void UpdateRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        //  Validate();

        //if (IsValid)
        //{
        bool success = false;

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_ProductListClass clsdbo_ProductList = new dbo_ProductListClass();
        clsdbo_ProductList = dbo_ProductListDataClass.Select_Record(txtProduct_List_ID.Text);

        SetData(clsdbo_ProductList);

        success = dbo_ProductListDataClass.Update(clsdbo_ProductList, User_ID);



        if (success)
        {
            Show("บันทึกสำเร็จ");
            // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", Messages.MessageBox(), true);
        }
        else
        {

        }
        //        }
        //        else
        //        {
        //            string script = @"swal({
        //                            title: ""กรุณาระบุข้อมูลให้ครบ"",
        //                            text: ""ข้อมูลไม่ครบ"",
        //                            type: ""error"",
        //                            confirmButtonText: ""ตกลง""
        //                        });";
        //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        //        }


    }

    private void InsertRecord()
    {
        if (IsValid)
        {
            dbo_ProductListClass clsdbo_ProductList = new dbo_ProductListClass();
            bool success = false;

            SetData(clsdbo_ProductList);

            clsdbo_ProductList.Product_List_ID = GenerateID.Product_List_ID();
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            success = dbo_ProductListDataClass.Add(clsdbo_ProductList, User_ID);

            if (success)
            {
                Show("บันทึกสำเร็จ");
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", Messages.MessageBox(), true);
            }
            else
            {
                Show("error");
            }
        }
        //        else
        //        {

        //            string script = @"swal({
        //                            title: ""กรุณาระบุข้อมูลให้ครบ"",
        //                            text: ""ข้อมูลไม่ครบ"",
        //                            type: ""error"",
        //                            confirmButtonText: ""ตกลง""
        //                        });";
        //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        //        }


        // ShowProductListDetails(string.Empty, string.Empty);
    }

    private void SetData(dbo_ProductListClass clsdbo_ProductList)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

        try
        {
            //if (user_class.User_Group_ID.ToUpper() == "CP MEIJI".ToUpper())
            if (txtPriceGroupType.Text == "เอเยนต์") // Agent
            {
                if (string.IsNullOrEmpty(txtCP_Meiji_Price.Text))
                {
                    clsdbo_ProductList.Agent_Price = null;
                }
                else
                {
                    clsdbo_ProductList.Agent_Price = System.Convert.ToDecimal(txtCP_Meiji_Price.Text);
                }

                if (string.IsNullOrEmpty(txtProduct_List_ID.Text))
                {
                    clsdbo_ProductList.Product_List_ID = null;
                }
                else
                {
                    clsdbo_ProductList.Product_List_ID = txtProduct_List_ID.Text;
                }

                /*
                if (string.IsNullOrEmpty(txtProduct_Name.Text))
                {
                    clsdbo_ProductList.Product_Name = null;
                }
                else
                {
                    clsdbo_ProductList.Product_Name = txtProduct_Name.Text;
                }
                */
                if (string.IsNullOrEmpty(txtProduct_Name.Text))
                {
                    clsdbo_ProductList.Product_Name = null;
                }
                else
                {
                    logger.Debug("ddlProductID.SelectedValue " + ddlProductID.SelectedValue);
                    clsdbo_ProductList.Product_Name = dbo_ProductDataClass.Select_Record(ddlProductID.SelectedValue).Product_Name;
                }


                /*if (string.IsNullOrEmpty(txtCP_Meiji_Price.Text))
                {
                    clsdbo_ProductList.CP_Meiji_Price = null;
                }
                else
                {
                    clsdbo_ProductList.CP_Meiji_Price = System.Convert.ToDecimal(txtCP_Meiji_Price.Text);
                }*/
                if (string.IsNullOrEmpty(txtVat.Text))
                {
                    clsdbo_ProductList.Vat = null;
                }
                else
                {
                    clsdbo_ProductList.Vat = System.Convert.ToByte(txtVat.Text);
                }
                //if (string.IsNullOrEmpty(txtPoint.Text))
                //{
                //    clsdbo_ProductList.Point = null;
                //}
                //else
                //{
                //    clsdbo_ProductList.Point = System.Convert.ToByte(txtPoint.Text);
                //}

                if (!string.IsNullOrEmpty(txtPrice_Group_ID.Text))
                {
                    clsdbo_ProductList.Price_Group_ID = txtPrice_Group_ID.Text;
                }
                else
                {
                    clsdbo_ProductList.Price_Group_ID = null;
                }

                if (string.IsNullOrEmpty(txtStartSaleDate.Text))
                {
                    clsdbo_ProductList.Product_Effective_Date = null;
                }
                else
                {
                    clsdbo_ProductList.Product_Effective_Date = DateTime.Parse(txtStartSaleDate.Text);
                }

                if (string.IsNullOrEmpty(txtStart_Effective_Date.Text))
                {
                    clsdbo_ProductList.Start_Effective_Date = null;
                }
                else
                {
                    clsdbo_ProductList.Start_Effective_Date = DateTime.Parse(txtStart_Effective_Date.Text);
                }

                if (string.IsNullOrEmpty(txtEnd_Effective_Date.Text))
                {
                    clsdbo_ProductList.End_Effective_Date = null;
                }
                else
                {
                    clsdbo_ProductList.End_Effective_Date = DateTime.Parse(txtEnd_Effective_Date.Text);
                }

                if (ddlProductID.SelectedIndex > 0)
                {
                    clsdbo_ProductList.Product_ID = ddlProductID.SelectedValue;
                }
                // clsdbo_ProductList.Product_ID = ddlProductID.SelectedItem.Text;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSPProduct_List_ID.Text))
                {
                    clsdbo_ProductList.Product_List_ID = txtSPProduct_List_ID.Text;
                }
                else
                {
                    clsdbo_ProductList.Product_List_ID = null;
                }

                if (!string.IsNullOrEmpty(txtPrice_Group_ID.Text))
                {
                    clsdbo_ProductList.Price_Group_ID = txtPrice_Group_ID.Text;
                }
                else
                {
                    clsdbo_ProductList.Price_Group_ID = null;
                }

                if (ddlSPProductID.SelectedIndex == 0)
                {
                    clsdbo_ProductList.Product_ID = null;
                }
                else
                {
                    clsdbo_ProductList.Product_ID = ddlSPProductID.SelectedItem.Text;
                }

                /*
                if (string.IsNullOrEmpty(txtSPProduct_Name.Text))
                {
                    clsdbo_ProductList.Product_Name = null;
                }
                else
                {
                    clsdbo_ProductList.Product_Name = txtSPProduct_Name.Text;
                }
                */
                if (string.IsNullOrEmpty(txtSPProduct_Name.Text))
                {
                    clsdbo_ProductList.Product_Name = null;
                }
                else
                {
                    logger.Debug("ddlSPProductID.SelectedValue " + ddlSPProductID.SelectedValue);
                    clsdbo_ProductList.Product_Name = dbo_ProductDataClass.Select_Record(ddlSPProductID.SelectedValue).Product_Name;
                }



                if (string.IsNullOrEmpty(txtSPCPMeijiPrice.Text))
                {
                    clsdbo_ProductList.CP_Meiji_Price = null;
                }
                else
                {
                    clsdbo_ProductList.CP_Meiji_Price = System.Convert.ToDecimal(txtSPCPMeijiPrice.Text);
                }

                if (string.IsNullOrEmpty(txtSPPrice.Text))
                {
                    clsdbo_ProductList.SP_Price = null;
                }
                else
                {
                    clsdbo_ProductList.SP_Price = System.Convert.ToDecimal(txtSPPrice.Text);
                }

                if (string.IsNullOrEmpty(txtSPPoint.Text))
                {
                    clsdbo_ProductList.Point = null;
                }
                else
                {
                    clsdbo_ProductList.Point = System.Convert.ToByte(txtSPPoint.Text);
                }

                if (string.IsNullOrEmpty(txtSPVat.Text))
                {
                    clsdbo_ProductList.Vat = null;
                }
                else
                {
                    clsdbo_ProductList.Vat = System.Convert.ToByte(txtSPVat.Text);
                }

                if (string.IsNullOrEmpty(txtSPStartSaleDate.Text))
                {
                    clsdbo_ProductList.Product_Effective_Date = null;
                }
                else
                {
                    clsdbo_ProductList.Product_Effective_Date = DateTime.Parse(txtSPStartSaleDate.Text);
                }

                if (string.IsNullOrEmpty(txtSPStart_Effective_Date.Text))
                {
                    clsdbo_ProductList.Start_Effective_Date = null;
                }
                else
                {
                    clsdbo_ProductList.Start_Effective_Date = DateTime.Parse(txtSPStart_Effective_Date.Text);
                }
                if (string.IsNullOrEmpty(txtSPEnd_Effective_Date.Text))
                {
                    clsdbo_ProductList.End_Effective_Date = null;
                }
                else
                {
                    clsdbo_ProductList.End_Effective_Date = DateTime.Parse(txtSPEnd_Effective_Date.Text);
                }
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

    private void showPanel(string panelName)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlSP.Visible = false;
        pnlForm.Visible = false;
        pnlGrid.Visible = false;
        pnlAgent.Visible = false;
        pnlAssign.Visible = false;
        pnlPriceGroup.Visible = false;

        switch (panelName)
        {
            case "pnlSP":
                pnlSP.Visible = true;
                break;
            case "pnlForm":
                pnlForm.Visible = true;
                break;
            case "pnlGrid":
                pnlGrid.Visible = true;
                break;
            case "pnlAgent":
                pnlAgent.Visible = true;
                break;
            case "pnlAssign":
                pnlAssign.Visible = true;
                break;
            case "pnlPriceGroup":
                pnlPriceGroup.Visible = true;
                break;
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

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion
}