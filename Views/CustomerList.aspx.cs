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

public partial class Views_CustomerList : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            txtCV_Code.Attributes.Add("onchange", "myApp.showPleaseWait();");

            ddlBillingType.Attributes.Add("onchange", "myApp.showPleaseWait();");
            ddlHome_District.Attributes.Add("onchange", "myApp.showPleaseWait();");
            ddlHome_Province.Attributes.Add("onchange", "myApp.showPleaseWait();");
            ddlHome_Sub_district.Attributes.Add("onchange", "myApp.showPleaseWait();");
            ddlShipment_District.Attributes.Add("onchange", "myApp.showPleaseWait();");
            ddlShipment_Province.Attributes.Add("onchange", "myApp.showPleaseWait();");
            ddlShipment_Sub_district.Attributes.Add("onchange", "myApp.showPleaseWait();");
            ddlSP.Attributes.Add("onchange", "myApp.showPleaseWait();");

            //this.grdCustomer.Attributes.Add("style", "word-break:break-all; word-wrap:break-word"); 

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

            hdnCV_CODE.Value = clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code;
            hdnAgent_Name.Value = clsdbo_Agent == null ? string.Empty : clsdbo_Agent.AgentName;

            SetUpDrowDownList();
            showPanel("pnlGrid");
            //SetEmptyUserGrid();

            btnSearch_Click(sender, e);
            ddlSearchCustomerStatus.SelectedIndex = 1;
        }
       
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        GetDetailsDataToForm(string.Empty, string.Empty);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    public void btnSave_Click(object sender, System.EventArgs e)
    {
       
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (btnSave.Text == "แก้ไข")
        {
            GetDetailsDataToForm(txtCustomer_ID.Text, "Edit");

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        else
        {
            Validate("CustomerValidation");

            if (IsValid)
            {
                string cntCustomerID = CheckUsername.Check_CustomerID(txtCustomer_ID.Text);
                if (cntCustomerID != "0" && btnSaveMode.Value == "บันทึก")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("รหัสลูกค้าไม่สามารถซ้ำได้");
                }
                else
                {
                    if (btnSaveMode.Value == "บันทึก")
                    {
                        List<dbo_UserClass> users = dbo_UserDataClass.Search(txtCustomer_ID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtCV_Code.Text, null, string.Empty, string.Empty);

                        InsertRecord();

                        btnSearch_Click(sender, e);

                    }
                    else
                    {
                        UpdateRecord();
                        #region 
                        //SetUpDrowDownList();
                        //showPanel("pnlGrid");
                        //txtSearchFirst_Name.Text = string.Empty;
                        //ddlSearchCustomerType.ClearSelection();
                        //txtSearchCustomerID.Text = string.Empty;
                        //ddlSearchResidence_Type_ID.ClearSelection();
                        //txtSearchHome_House_No.Text = string.Empty;
                        //txtSearchMobile.Text = string.Empty;
                        //ddlSearchSP.ClearSelection();
                        //ddlSearchCustomerStatus.ClearSelection();
                        #endregion
                        btnSearch_Click(sender, e);
                    }

                    pnlForm.Visible = false;
                    pnlGrid.Visible = true;

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
    }

    public void btnCancel_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        //ShowGridPanel();
        SetUpDrowDownList();
        showPanel("pnlGrid");
        SetEmptyUserGrid();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        string sp = (ddlSearchSP.SelectedIndex == 0 ? string.Empty : ddlSearchSP.SelectedItem.Value);
        string type = (ddlSearchCustomerType.SelectedIndex == 0 ? string.Empty : ddlSearchCustomerType.SelectedItem.Value);
        string status = (ddlSearchCustomerStatus.SelectedIndex == 0 ? string.Empty : ddlSearchCustomerStatus.SelectedItem.Value);
        string resident = (ddlSearchResidence_Type_ID.SelectedIndex == 0 ? string.Empty : ddlSearchResidence_Type_ID.SelectedItem.Value);
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

        List<dbo_CustomerClass> customer = dbo_CustomerDataClass.Search(txtSearchFirst_Name.Text, type, txtSearchCustomerID.Text, resident, txtSearchHome_House_No.Text, txtSearchMobile.Text, sp, status, user_class.CV_CODE);


        List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        if (user_class.User_Group_ID == "CP Meiji")
        {
            List<string> cv_code1 = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f.CV_Code));
            if (cv_code1.Count != 0)
            {
                //List<dbo_CustomerClass> customer_ = new List<dbo_CustomerClass>(customer.Where(f => cv_code1.Contains(f.CV_Code)).Select(f => f));
                List<dbo_CustomerClass> customer_ = (from p in customer where cv_code1.Any(f => f.Contains(p.CV_Code)) select p).OrderBy(f => f.Customer_ID).ToList();
                grdCustomer.DataSource = customer_;
                grdCustomer.DataBind();
            }
            else
            {
                string region = user_class.Region;

                string[] regions = region.Split(',');

                List<string> cv_code_ = new List<string>();

                foreach (string in_region in regions)
                {
                    List<string> cv_code2 = new List<string>(agent.Where(f => f.Location_Region == in_region).Select(f => f.CV_Code));
                    foreach (string _cv in cv_code2)
                    {
                        cv_code_.Add(_cv);
                    }
                }
                List<dbo_CustomerClass> customer_ = (from p in customer where cv_code_.Any(f => f.Contains(p.CV_Code)) select p).OrderBy(f => f.Customer_ID).ToList();
                if (customer_.Count > 0)
                {
                    grdCustomer.DataSource = customer_;
                    grdCustomer.DataBind();
                    grdCustomer.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    grdCustomer.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }
        }
        else
        {
            List<dbo_CustomerClass> customer_ = new List<dbo_CustomerClass>(customer.Where(f => f.CV_Code == user_class.CV_CODE).Select(f => f));
            if (customer_.Count > 0)
            {
                grdCustomer.DataSource = customer_;
                grdCustomer.DataBind();

                grdCustomer.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                grdCustomer.Visible = false;
                pnlNoRec.Visible = true;
            }
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnCancelSearch_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SetUpDrowDownList();
        showPanel("pnlGrid");
        SetEmptyUserGrid();

        grdCustomer.Visible = false;
        pnlNoRec.Visible = false;
       // ddlSearchCustomerStatus.SelectedIndex = 1;
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnCopyAddress_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        txtShipment_House_No.Text = txtHome_House_No.Text;
        txtShipment_Village.Text = txtHome_Village.Text;
        txtShipment_Village_No.Text = txtHome_Village_No.Text;
        txtShipment_Alley.Text = txtHome_Alley.Text;
        txtShipment_Road.Text = txtHome_Road.Text;
        txtShipment_Tower.Text = txtHome_Tower.Text;
        txtShipment_Postal_ID.Text = txtHome_Postal_ID.Text;

        ddlShipment_Sub_district.ClearSelection();
        ddlShipment_District.ClearSelection();
        ddlShipment_Province.ClearSelection();

        if (ddlShipment_Province.Items.FindByText(ddlHome_Province.SelectedValue) != null)
        {
            ddlShipment_Province.Items.FindByText(ddlHome_Province.SelectedValue).Selected = true;
            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlShipment_Province.Text);
            List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                         .Select(grp => grp.First())
                         .ToList();


            dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
            tmp_tambol.Insert(0, first_);
            ddlShipment_District.DataSource = tmp_tambol;
            ddlShipment_District.DataBind();
        }

        if (ddlShipment_District.Items.FindByText(ddlHome_District.SelectedValue) != null)
        {
            ddlShipment_District.Items.FindByText(ddlHome_District.SelectedValue).Selected = true;

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlShipment_District.Text, ddlShipment_Province.Text);

            List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                         .Select(grp => grp.First())
                         .ToList();


            dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
            tmp_tambol.Insert(0, first_);
            ddlShipment_Sub_district.DataSource = tmp_tambol;
            ddlShipment_Sub_district.DataBind();


        }


        if (ddlShipment_Sub_district.Items.FindByText(ddlHome_Sub_district.SelectedValue) != null)
            ddlShipment_Sub_district.Items.FindByText(ddlHome_Sub_district.SelectedValue).Selected = true;

        txtShipment_Postal_ID.Text = txtHome_Postal_ID.Text;
    }

    protected void ddlHome_Province_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlHome_Province.Text);

        List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                     .Select(grp => grp.First())
                     .ToList();


        dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
        tmp_tambol.Insert(0, first_);
        ddlHome_District.DataSource = tmp_tambol;
        ddlHome_District.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlHome_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlHome_District.Text, ddlHome_Province.Text);

        List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                     .Select(grp => grp.First())
                     .ToList();


        dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
        tmp_tambol.Insert(0, first_);


        ddlHome_Sub_district.DataSource = tmp_tambol;
        ddlHome_Sub_district.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlShipment_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlShipment_District.Text, ddlShipment_Province.Text);

        List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                     .Select(grp => grp.First())
                     .ToList();


        dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
        tmp_tambol.Insert(0, first_);
        ddlShipment_Sub_district.DataSource = tmp_tambol;
        ddlShipment_Sub_district.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

    }

    protected void ddlShipment_Province_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlShipment_Province.Text);

        List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                     .Select(grp => grp.First())
                     .ToList();

        dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
        tmp_tambol.Insert(0, first_);
        ddlShipment_District.DataSource = tmp_tambol;
        ddlShipment_District.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlSP_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        txtSP_ID.Text = ddlSP.SelectedValue;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlBillingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        ddlBilling_Day_of_Week.Visible = false;
        ddlBilling_Day_of_Month.Visible = false;
        ddlDue_Billing_Day_of_Week.Visible = false;
        ddlDue_Billing_Day_of_Month.Visible = false;

        txtBilling_Day_Other.Visible = false;
        txtDue_Billing_Day_Other.Visible = false;
        txtBilling.Visible = false;
        txtDue_Billing.Visible = false;


        switch (ddlBillingType.SelectedValue)
        {
            case "บิลชนบิล":
                /*ddlBilling_Day_of_Week.Visible = false;
                ddlDue_Billing_Day_of_Week.Visible = false;
                ddlBilling_Day_of_Month.Visible = false;
                ddlDue_Billing_Day_of_Month.Visible = false;
                txtBilling_Day_Other.Visible = false;
                txtDue_Billing_Day_Other.Visible = false;*/
                txtBilling.Visible = true;
                txtDue_Billing.Visible = true;
                txtBilling.Enabled = false;
                txtDue_Billing.Enabled = false;
                break;
            case "รายสัปดาห์":
                /*ddlBilling_Day_of_Month.Visible = false;
                ddlDue_Billing_Day_of_Month.Visible = false;
                txtBilling_Day_Other.Visible = false;
                txtDue_Billing_Day_Other.Visible = false;*/
                ddlBilling_Day_of_Week.Visible = true;
                ddlDue_Billing_Day_of_Week.Visible = true;
                if (btnSave.Text == "แก้ไข")
                {
                    ddlBilling_Day_of_Week.Enabled = false;
                    ddlDue_Billing_Day_of_Week.Enabled = false;
                }
                if (btnSave.Text == "บันทึก")
                {
                    ddlBilling_Day_of_Week.Enabled = true;
                    ddlDue_Billing_Day_of_Week.Enabled = true;
                }
                break;
            case "รายเดือน":
                /*ddlBilling_Day_of_Week.Visible = false;
                ddlDue_Billing_Day_of_Week.Visible = false;*/
                ddlBilling_Day_of_Month.Visible = true;
                ddlDue_Billing_Day_of_Month.Visible = true;
                if (btnSave.Text == "แก้ไข")
                {
                    ddlBilling_Day_of_Month.Enabled = false;
                    ddlDue_Billing_Day_of_Month.Enabled = false;
                }
                if (btnSave.Text == "บันทึก")
                {
                    ddlBilling_Day_of_Month.Enabled = true;
                    ddlDue_Billing_Day_of_Month.Enabled = true;
                }
                //txtBilling_Day_Other.Visible = false;
                //txtDue_Billing_Day_Other.Visible = false;
                break;
            case "วางบิลอื่นๆ":
                /*ddlBilling_Day_of_Week.Visible = false;
                ddlDue_Billing_Day_of_Week.Visible = false;
                ddlBilling_Day_of_Month.Visible = false;
                ddlDue_Billing_Day_of_Month.Visible = false;*/
                txtBilling_Day_Other.Visible = true;
                txtDue_Billing_Day_Other.Visible = true;
                if (btnSave.Text == "แก้ไข")
                {
                    txtBilling_Day_Other.Enabled = false;
                    txtDue_Billing_Day_Other.Enabled = false;
                }
                if (btnSave.Text == "บันทึก")
                {
                    txtBilling_Day_Other.Enabled = true;
                    txtDue_Billing_Day_Other.Enabled = true;
                }
                break;
            default:
                /*ddlBilling_Day_of_Week.Visible = false;
                ddlDue_Billing_Day_of_Week.Visible = false;
                ddlBilling_Day_of_Month.Visible = false;
                ddlDue_Billing_Day_of_Month.Visible = false;
                txtBilling_Day_Other.Visible = true;
                txtDue_Billing_Day_Other.Visible = true;
                txtBilling_Day_Other.Enabled = false;
                txtDue_Billing_Day_Other.Enabled = false;*/
                txtBilling.Visible = true;
                txtDue_Billing.Visible = true;
                txtBilling.Enabled = false;
                txtDue_Billing.Enabled = false;
                break;
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

    }

    protected void ddlShipment_Sub_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlShipment_District.Text, ddlShipment_Province.Text);

            txtShipment_Postal_ID.Text = tambol.FirstOrDefault(f => f.Sub_district == ddlShipment_Sub_district.Text).Postal_ID;

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlHome_Sub_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlHome_District.Text, ddlHome_Province.Text);

            txtHome_Postal_ID.Text = tambol.FirstOrDefault(f => f.Sub_district == ddlHome_Sub_district.Text).Postal_ID;

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void txtCV_Code_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            dbo_AgentClass agent = dbo_AgentDataClass.Select_Record(txtCV_Code.Text);
            if (agent == null)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show("ไม่พบ CV Code ในระบบ");
                txtCV_Code.Text = string.Empty;

            }
            else
            {
                txtCustomer_ID.Text = GenerateID.Customer_ID(agent.CV_Code);
                txtAgentName.Text = agent.AgentName;


                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtCV_Code.Text, null, string.Empty, string.Empty);
                //List<dbo_UserClass> users = dbo_UserDataClass.Search("", "", "", "", "", "CP Meiji", "", "", null, string.Empty, string.Empty);
                dbo_UserClass fir = new dbo_UserClass();

                fir.User_ID = string.Empty;
                fir.FullName = "==ระบุ==";
                users.Insert(0, fir);

                ddlSP.DataSource = users.Where(f => f.Status == "Active" || f.FullName == "==ระบุ==");
                ddlSP.DataBind();

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
        }
        catch (Exception)
        {

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

        }
    }

    private void showPanel(string panelName)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlForm.Visible = false;
        pnlGrid.Visible = false;

        switch (panelName)
        {
            case "pnlForm":
                pnlForm.Visible = true;
                break;
            case "pnlGrid":
                pnlGrid.Visible = true;
                break;
        }
    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        Dictionary<string, string> resident = dbo_ItemDataClass.GetDropDown_New("15");
        ddlSearchResidence_Type_ID.DataSource = resident;
        ddlSearchResidence_Type_ID.DataBind();
        ddlResidentType_ID.DataSource = resident;
        ddlResidentType_ID.DataBind();

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
        dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

        List<dbo_UserClass> user = dbo_UserDataClass.Search("", "", "", "", "Active", "Agent", "", user_class.CV_CODE, null, string.Empty, string.Empty);
        dbo_UserClass u = new dbo_UserClass();
        u.FullName = "==ระบุ==";
        u.User_ID = string.Empty;
        user.Insert(0, u);

        //  Dictionary<string, string> sp = 
        if (user_class.CV_CODE != "")
        {
            ddlSearchSP.DataSource = user.Where(f => f.Position == "สาวส่งนม" || f.FullName == "==ระบุ==");
            ddlSearchSP.DataBind();
        }
        else
        {
            ddlSearchSP.DataSource = user.Where(f => f.Position == "สาวส่งนม" || f.FullName == "==ระบุ==");
            ddlSearchSP.DataBind();
        }

        List<dbo_TambolClass> tambol = dbo_TambolDataClass.SelectAll();
        List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Province)
                     .Select(grp => grp.First())
                     .ToList();

        dbo_TambolClass first_ = new dbo_TambolClass() { Province = "==ระบุ==" };
        tmp_tambol.Insert(0, first_);
        ddlHome_Province.DataSource = tmp_tambol;
        ddlHome_Province.DataBind();

        List<dbo_TambolClass> disti = new List<dbo_TambolClass>();
        disti.Add(new dbo_TambolClass() { District = "==ระบุ==", Sub_district = "==ระบุ==" });
        ddlHome_District.DataSource = disti;
        ddlHome_District.DataBind();

        ddlShipment_Province.DataSource = tmp_tambol;
        ddlShipment_Province.DataBind();

        ddlHome_Sub_district.DataSource = disti;
        ddlHome_Sub_district.DataBind();

        ddlShipment_District.DataSource = disti;
        ddlShipment_District.DataBind();

        ddlShipment_Sub_district.DataSource = disti;
        ddlShipment_Sub_district.DataBind();


        txtCV_Code.Text = clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code;
        txtAgentName.Text = clsdbo_Agent == null ? string.Empty : clsdbo_Agent.AgentName;

        Dictionary<string, string> creditterm = dbo_ItemDataClass.GetDropDown_New("17");
        ddlCredit_Term.DataSource = creditterm;
        ddlCredit_Term.DataBind();

    }

    private void GetDetailsDataToForm(string id, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            showPanel("pnlForm");

            txtFirst_Name.Text = "";
            txtLast_Name.Text = "";
            txtHome_Phone_No.Text = "";
            txtMobile.Text = "";
            txtContact_Name.Text = "";
            txtBirthday.Text = "";
            txtHome_House_No.Text = "";
            txtHome_Tower.Text = "";
            txtHome_Village.Text = "";
            txtHome_Village_No.Text = "";
            txtHome_Alley.Text = "";
            txtHome_Road.Text = "";
            txtHome_Postal_ID.Text = "";
            txtShipment_House_No.Text = "";
            txtShipment_Tower.Text = "";
            txtShipment_Village.Text = "";
            txtShipment_Village_No.Text = "";
            txtShipment_Alley.Text = "";
            txtShipment_Road.Text = "";
            txtShipment_Postal_ID.Text = "";
            txtSP_ID.Text = "";
            txtAgentName.Text = "";

            txtBilling_Day_Other.Text = string.Empty;
            txtDue_Billing_Day_Other.Text = string.Empty;


            ddlCredit_Term.ClearSelection();
            ddlCustomerType.ClearSelection();
            ddlCustomerStatus.ClearSelection();
            ddlResidentType_ID.ClearSelection();
            ddlHome_Sub_district.ClearSelection();
            ddlHome_District.ClearSelection();
            ddlHome_Province.ClearSelection();
            ddlShipment_Sub_district.ClearSelection();
            ddlShipment_District.ClearSelection();
            ddlShipment_Province.ClearSelection();
            ddlSP.ClearSelection();
            ddlPaymentType.ClearSelection();
            ddlBillingType.ClearSelection();
            ddlBilling_Day_of_Week.ClearSelection();
            ddlDue_Billing_Day_of_Week.ClearSelection();
            ddlBilling_Day_of_Month.ClearSelection();
            ddlDue_Billing_Day_of_Month.ClearSelection();

            ddlBilling_Day_of_Week.Visible = false;
            ddlBilling_Day_of_Month.Visible = false;
            ddlDue_Billing_Day_of_Week.Visible = false;
            ddlDue_Billing_Day_of_Month.Visible = false;

            ddlReceiveProduct_Hour.ClearSelection();
            ddlReceiveProduct_Minute.ClearSelection();
            ddlReceiveToProduct_Hour.ClearSelection();
            ddlReceiveToProduct_Minute.ClearSelection();

            ddlStatus.ClearSelection();
            txtMemberDate.Text = "";
            txtShopName.Text = "";

            txtBilling_Day_Other.Visible = true;
            txtDue_Billing_Day_Other.Visible = true;
            txtBilling_Day_Other.Enabled = false;
            txtDue_Billing_Day_Other.Enabled = false;

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            if (user_class.User_Group_ID == "CP Meiji")
            {
                hdnCV_CODE.Value = "";
                txtCV_Code.Text = string.Empty;
                txtAgentName.Text = string.Empty;
                txtCustomer_ID.Text = string.Empty;
            }


            if (!string.IsNullOrEmpty(hdnCV_CODE.Value))
            {
                txtCV_Code.Text = hdnCV_CODE.Value;
                txtAgentName.Text = hdnAgent_Name.Value;
                txtCustomer_ID.Text = GenerateID.Customer_ID(hdnCV_CODE.Value);

                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtCV_Code.Text, null, string.Empty, string.Empty);

                dbo_UserClass fir = new dbo_UserClass();

                fir.User_ID = string.Empty;
                fir.FullName = "==ระบุ==";
                users.Insert(0, fir);

                ddlSP.DataSource = users.Where(f => f.Status == "Active" || f.FullName == "==ระบุ==");
                ddlSP.DataBind();
            }
            else
            {
                txtCV_Code.Enabled = true;     
            }



            if (!string.IsNullOrEmpty(id))
            {
                dbo_CustomerClass clsdbo_Customer = new dbo_CustomerClass();
                clsdbo_Customer.Customer_ID = id;
                clsdbo_Customer = dbo_CustomerDataClass.Select_Record(id);

                txtCV_Code.Text = clsdbo_Customer.CV_Code;
                dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(txtCV_Code.Text);
                txtAgentName.Text = clsdbo_Agent == null ? string.Empty : clsdbo_Agent.AgentName;
                txtCustomer_ID.Text = GenerateID.Customer_ID(hdnCV_CODE.Value);

                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtCV_Code.Text, null, string.Empty, string.Empty);

                dbo_UserClass fir = new dbo_UserClass();

                fir.User_ID = string.Empty;
                fir.FullName = "==ระบุ==";
                users.Insert(0, fir);

                ddlSP.DataSource = users.Where(f => f.Status == "Active" || f.FullName == "==ระบุ==");
                ddlSP.DataBind();


                txtCustomer_ID.Text = System.Convert.ToString(clsdbo_Customer.Customer_ID);
                txtFirst_Name.Text = System.Convert.ToString(clsdbo_Customer.First_Name);
                txtLast_Name.Text = System.Convert.ToString(clsdbo_Customer.Last_Name);
                txtHome_Phone_No.Text = System.Convert.ToString(clsdbo_Customer.Home_Phone_No);
                txtMobile.Text = System.Convert.ToString(clsdbo_Customer.Mobile);
                txtContact_Name.Text = System.Convert.ToString(clsdbo_Customer.Contact_Name);

                txtCV_Code.Enabled = false;

                if (clsdbo_Customer.Birthday.HasValue)
                    txtBirthday.Text = clsdbo_Customer.Birthday.Value.ToShortDateString();
                if (clsdbo_Customer.Member_Date.HasValue)
                    txtMemberDate.Text = clsdbo_Customer.Member_Date.Value.ToShortDateString();

                txtHome_House_No.Text = System.Convert.ToString(clsdbo_Customer.Home_House_No);
                txtHome_Tower.Text = System.Convert.ToString(clsdbo_Customer.Home_Tower);
                txtHome_Village.Text = System.Convert.ToString(clsdbo_Customer.Home_Village);
                txtHome_Village_No.Text = System.Convert.ToString(clsdbo_Customer.Home_Village_No);
                txtHome_Alley.Text = System.Convert.ToString(clsdbo_Customer.Home_Alley);
                txtHome_Road.Text = System.Convert.ToString(clsdbo_Customer.Home_Road);
                txtHome_Postal_ID.Text = System.Convert.ToString(clsdbo_Customer.Home_Postal_ID);
                txtShipment_House_No.Text = System.Convert.ToString(clsdbo_Customer.Shipment_House_No);
                txtShipment_Tower.Text = System.Convert.ToString(clsdbo_Customer.Shipment_Tower);
                txtShipment_Village.Text = System.Convert.ToString(clsdbo_Customer.Shipment_Village);
                txtShipment_Village_No.Text = System.Convert.ToString(clsdbo_Customer.Shipment_Village_No);
                txtShipment_Alley.Text = System.Convert.ToString(clsdbo_Customer.Shipment_Alley);
                txtShipment_Road.Text = System.Convert.ToString(clsdbo_Customer.Shipment_Road);
                txtShipment_Postal_ID.Text = System.Convert.ToString(clsdbo_Customer.Shipment_Postal_ID);
                //txtSP_ID.Text = System.Convert.ToString(clsdbo_Customer.SP_ID);
                txtCV_Code.Text = System.Convert.ToString(clsdbo_Customer.CV_Code);


                txtBilling_Day_Other.Text = clsdbo_Customer.Billing_Day_of_Other;
                txtDue_Billing_Day_Other.Text = clsdbo_Customer.Due_Billing_Day_of_Other;
                txtRemarks.Text = clsdbo_Customer.Remarks;
                txtShopName.Text = System.Convert.ToString(clsdbo_Customer.Shop_Name);

                if (clsdbo_Customer.Home_Province != null)
                {

                    if (ddlHome_Province.Items.FindByText(clsdbo_Customer.Home_Province.Trim()) != null)
                        ddlHome_Province.Items.FindByText(clsdbo_Customer.Home_Province.Trim()).Selected = true;


                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlHome_Province.Text);
                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                                 .Select(grp => grp.First())
                                 .ToList();

                    dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlHome_District.DataSource = tmp_tambol;
                    ddlHome_District.DataBind();
                }

                if (clsdbo_Customer.Home_District != null)
                {
                    if (ddlHome_District.Items.FindByText(clsdbo_Customer.Home_District.Trim()) != null)
                        ddlHome_District.Items.FindByText(clsdbo_Customer.Home_District.Trim()).Selected = true;


                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlHome_District.Text, ddlHome_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                                 .Select(grp => grp.First())
                                 .ToList();


                    dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);


                    ddlHome_Sub_district.DataSource = tmp_tambol;
                    ddlHome_Sub_district.DataBind();
                }

                if (clsdbo_Customer.Home_Sub_district != null)
                {

                    if (ddlHome_Sub_district.Items.FindByText(clsdbo_Customer.Home_Sub_district.Trim()) != null)
                        ddlHome_Sub_district.Items.FindByText(clsdbo_Customer.Home_Sub_district.Trim()).Selected = true;

                }


                if (clsdbo_Customer.Shipment_Province != null)
                {

                    if (ddlShipment_Province.Items.FindByText(clsdbo_Customer.Shipment_Province.Trim()) != null)
                        ddlShipment_Province.Items.FindByText(clsdbo_Customer.Shipment_Province.Trim()).Selected = true;

                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlShipment_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                                 .Select(grp => grp.First())
                                 .ToList();


                    dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlShipment_District.DataSource = tmp_tambol;
                    ddlShipment_District.DataBind();

                }

                if (clsdbo_Customer.Shipment_District != null)
                {

                    if (ddlShipment_District.Items.FindByText(clsdbo_Customer.Shipment_District.Trim()) != null)
                        ddlShipment_District.Items.FindByText(clsdbo_Customer.Shipment_District.Trim()).Selected = true;


                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlShipment_District.Text, ddlShipment_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                                 .Select(grp => grp.First())
                                 .ToList();


                    dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlShipment_Sub_district.DataSource = tmp_tambol;
                    ddlShipment_Sub_district.DataBind();

                }

                if (clsdbo_Customer.Shipment_Sub_district != null)
                {

                    if (ddlShipment_Sub_district.Items.FindByText(clsdbo_Customer.Shipment_Sub_district.Trim()) != null)
                        ddlShipment_Sub_district.Items.FindByText(clsdbo_Customer.Shipment_Sub_district.Trim()).Selected = true;
                }


                if (clsdbo_Customer.Customer_Type != null)
                {
                    if (ddlCustomerType.Items.FindByText(clsdbo_Customer.Customer_Type.Trim()) != null)
                        ddlCustomerType.Items.FindByText(clsdbo_Customer.Customer_Type.Trim()).Selected = true;
                }


                if (clsdbo_Customer.Residence_Type_ID != null)
                {
                    if (ddlResidentType_ID.Items.FindByText(clsdbo_Customer.Residence_Type_ID.Trim()) != null)
                        ddlResidentType_ID.Items.FindByText(clsdbo_Customer.Residence_Type_ID.Trim()).Selected = true;
                }

                if (clsdbo_Customer.SP_ID != null)
                {
                    if (ddlSP.Items.FindByValue(clsdbo_Customer.SP_ID.Trim()) != null)
                    {
                        //ddlSP.Items.FindByValue(clsdbo_Customer.SP_ID.Trim()).Selected = true;
                        ddlSP.SelectedIndex = ddlSP.Items.IndexOf(ddlSP.Items.FindByValue(clsdbo_Customer.SP_ID.Trim()));
                        //ddlSP.SelectedIndex = 5;
                        txtSP_ID.Text = clsdbo_Customer.SP_ID;
                    }
                }


                if (clsdbo_Customer.Credit_Term != null)
                {
                    if (ddlCredit_Term.Items.FindByText(clsdbo_Customer.Credit_Term.Trim()) != null)
                        ddlCredit_Term.Items.FindByText(clsdbo_Customer.Credit_Term.Trim()).Selected = true;
                }

                if (clsdbo_Customer.Payment_Type != null)
                {
                    if (ddlPaymentType.Items.FindByText(clsdbo_Customer.Payment_Type.Trim()) != null)
                        ddlPaymentType.Items.FindByText(clsdbo_Customer.Payment_Type.Trim()).Selected = true;
                }


                if (clsdbo_Customer.Billing_Type != null)
                {
                    if (ddlBillingType.Items.FindByText(clsdbo_Customer.Billing_Type.Trim()) != null)
                        ddlBillingType.Items.FindByText(clsdbo_Customer.Billing_Type.Trim()).Selected = true;


                }


                if (clsdbo_Customer.Billing_Day_of_Week != null)
                {
                    if (ddlBilling_Day_of_Week.Items.FindByText(clsdbo_Customer.Billing_Day_of_Week.Trim()) != null)
                        ddlBilling_Day_of_Week.Items.FindByText(clsdbo_Customer.Billing_Day_of_Week.Trim()).Selected = true;
                }
                if (clsdbo_Customer.Due_Billing_Day_of_Week != null)
                {
                    if (ddlDue_Billing_Day_of_Week.Items.FindByText(clsdbo_Customer.Due_Billing_Day_of_Week.Trim()) != null)
                        ddlDue_Billing_Day_of_Week.Items.FindByText(clsdbo_Customer.Due_Billing_Day_of_Week.Trim()).Selected = true;
                }
                if (clsdbo_Customer.Billing_Day_of_Month != null)
                {
                    if (ddlBilling_Day_of_Month.Items.FindByText(clsdbo_Customer.Billing_Day_of_Month.Trim()) != null)
                        ddlBilling_Day_of_Month.Items.FindByText(clsdbo_Customer.Billing_Day_of_Month.Trim()).Selected = true;
                }

                if (clsdbo_Customer.Due_Billing_Day_of_Month != null)
                {
                    if (ddlDue_Billing_Day_of_Month.Items.FindByText(clsdbo_Customer.Due_Billing_Day_of_Month.Trim()) != null)
                        ddlDue_Billing_Day_of_Month.Items.FindByText(clsdbo_Customer.Due_Billing_Day_of_Month.Trim()).Selected = true;
                }
                if (clsdbo_Customer.Billing_Day_of_Other != null)
                {
                    if (ddlBilling_Day_of_Week.Items.FindByText(clsdbo_Customer.Billing_Day_of_Other.Trim()) != null)
                        ddlBilling_Day_of_Week.Items.FindByText(clsdbo_Customer.Billing_Day_of_Other.Trim()).Selected = true;
                }
                if (clsdbo_Customer.Due_Billing_Day_of_Other != null)
                {
                    if (ddlDue_Billing_Day_of_Week.Items.FindByText(clsdbo_Customer.Due_Billing_Day_of_Other.Trim()) != null)
                        ddlDue_Billing_Day_of_Week.Items.FindByText(clsdbo_Customer.Due_Billing_Day_of_Other.Trim()).Selected = true;
                }
                /*if (clsdbo_Customer.SP_ID != null)
                {
                    if (ddlSP.Items.FindByValue(clsdbo_Customer.SP_ID.Trim()) != null)
                        ddlSP.Items.FindByValue(clsdbo_Customer.SP_ID.Trim()).Selected = true;
                }*/

                if (clsdbo_Customer.ReceiveDate_Hour != null)
                {
                    if (ddlReceiveProduct_Hour.Items.FindByValue(clsdbo_Customer.ReceiveDate_Hour.Trim()) != null)
                        ddlReceiveProduct_Hour.Items.FindByValue(clsdbo_Customer.ReceiveDate_Hour.Trim()).Selected = true;
                }

                if (clsdbo_Customer.ReceiveDate_Minute != null)
                {
                    if (ddlReceiveProduct_Minute.Items.FindByValue(clsdbo_Customer.ReceiveDate_Minute.Trim()) != null)
                        ddlReceiveProduct_Minute.Items.FindByValue(clsdbo_Customer.ReceiveDate_Minute.Trim()).Selected = true;
                }

                if (clsdbo_Customer.ReceiveToDate_Hour != null)
                {
                    if (ddlReceiveToProduct_Hour.Items.FindByValue(clsdbo_Customer.ReceiveToDate_Hour.Trim()) != null)
                        ddlReceiveToProduct_Hour.Items.FindByValue(clsdbo_Customer.ReceiveToDate_Hour.Trim()).Selected = true;
                }

                if (clsdbo_Customer.ReceiveToDate_Minute != null)
                {
                    if (ddlReceiveToProduct_Minute.Items.FindByValue(clsdbo_Customer.ReceiveToDate_Minute.Trim()) != null)
                        ddlReceiveToProduct_Minute.Items.FindByValue(clsdbo_Customer.ReceiveToDate_Minute.Trim()).Selected = true;
                }
                if (clsdbo_Customer.Active_status != null)
                {

                    if (ddlStatus.Items.FindByText(clsdbo_Customer.Active_status) != null)
                        ddlStatus.Items.FindByText(clsdbo_Customer.Active_status).Selected = true;
                }
                if (clsdbo_Customer.Status != null)
                {

                    if (ddlCustomerStatus.Items.FindByText(clsdbo_Customer.Status) != null)
                        ddlCustomerStatus.Items.FindByText(clsdbo_Customer.Status).Selected = true;
                }
            }

            bool enable = Mode != "View";


            txtCustomer_ID.Enabled = false;

            //txtBilling_Day_Other.Enabled = enable;
            //txtDue_Billing_Day_Other.Enabled = enable;

            txtFirst_Name.Enabled = enable;
            txtLast_Name.Enabled = enable;
            txtHome_Phone_No.Enabled = enable;
            txtMobile.Enabled = enable;
            txtContact_Name.Enabled = enable;
            txtBirthday.Enabled = enable;
            txtHome_House_No.Enabled = enable;
            txtHome_Tower.Enabled = enable;
            txtHome_Village.Enabled = enable;
            txtHome_Village_No.Enabled = enable;
            txtHome_Alley.Enabled = enable;
            txtHome_Road.Enabled = enable;
            txtHome_Postal_ID.Enabled = enable;
            txtShipment_House_No.Enabled = enable;
            txtShipment_Tower.Enabled = enable;
            txtShipment_Village.Enabled = enable;
            txtShipment_Village_No.Enabled = enable;
            txtShipment_Alley.Enabled = enable;
            txtShipment_Road.Enabled = enable;
            txtShipment_Postal_ID.Enabled = enable;
            txtRemarks.Enabled = enable;

            //txtSP_ID.Enabled = enable;
            ddlCredit_Term.Enabled = enable;


            ddlCustomerType.Enabled = enable;
            ddlCustomerStatus.Enabled = enable;
            ddlResidentType_ID.Enabled = enable;
            ddlHome_Sub_district.Enabled = enable;
            ddlHome_District.Enabled = enable;
            ddlHome_Province.Enabled = enable;
            ddlShipment_Sub_district.Enabled = enable;
            ddlShipment_District.Enabled = enable;
            ddlShipment_Province.Enabled = enable;
            ddlSP.Enabled = enable;
            ddlPaymentType.Enabled = enable;
            ddlBillingType.Enabled = enable;
            //ddlBilling_Day_of_Week.Enabled = enable;
            //ddlDue_Billing_Day_of_Week.Enabled = enable;

            //ddlBilling_Day_of_Month.Enabled = enable;
            //ddlDue_Billing_Day_of_Month.Enabled = enable;

            ddlReceiveProduct_Hour.Enabled = enable;
            ddlReceiveProduct_Minute.Enabled = enable;
            ddlReceiveToProduct_Hour.Enabled = enable;
            ddlReceiveToProduct_Minute.Enabled = enable;
            ddlStatus.Enabled = enable;
            txtMemberDate.Enabled = enable;
            txtShopName.Enabled = enable;



            if (Mode == "View")
            {
                btnSave.Visible = true;
                btnSave.Text = "แก้ไข";
                btnCancel.Text = "กลับไปหน้าค้นหา";
                btnSaveMode.Value = "บันทึก";
            }
            else if (Mode == "Edit")
            {
                btnSave.Visible = true;
                btnSave.Text = "บันทึก";
                btnCancel.Text = "ยกเลิก";
                btnSaveMode.Value = "แก้ไข";
            }
            else if (string.IsNullOrEmpty(Mode))
            {
                btnSave.Visible = true;
                btnSave.Text = "บันทึก";
                btnCancel.Text = "ยกเลิก";
                btnSaveMode.Value = "บันทึก";

                // LabelPageHeader.Text = "สร้างข้อมูล User";


                //if (!string.IsNullOrEmpty(ViewState["CV_Code"].ToString()))
                //{
                //    txtCustomer_ID.Text = GenerateID.Customer_ID(txtCV_Code.Text);
                //}

            }
            ddlBillingType_SelectedIndexChanged(null, null);

        }
        catch (Exception ex)
        {

        }
    }

    private void SetEmptyUserGrid()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

        if(grdCustomer.Rows.Count > 0)
        {
            List<dbo_CustomerClass> customer = dbo_CustomerDataClass.Search("--", "--", "--", "--", "--", "--", "--", "--", user_class.CV_CODE);
            grdCustomer.DataSource = customer;
            grdCustomer.DataBind();
        }
        


        txtSearchFirst_Name.Text = string.Empty;
        ddlSearchCustomerType.ClearSelection();
        txtSearchCustomerID.Text = string.Empty;
        ddlSearchResidence_Type_ID.ClearSelection();
        txtSearchHome_House_No.Text = string.Empty;
        txtSearchMobile.Text = string.Empty;
        ddlSearchSP.ClearSelection();
        //ddlSearchCustomerStatus.ClearSelection();
        ddlSearchCustomerStatus.SelectedIndex = 1;

    }

    private void InsertRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_CustomerClass clsdbo_Customer = new dbo_CustomerClass();
        SetData(clsdbo_Customer);
        bool success = false;
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        clsdbo_Customer.Active_status = "1";
        success = dbo_CustomerDataClass.Add(clsdbo_Customer, User_ID);
        if (success)
        {
            Show("บันทึกข้อมูลสำเร็จ!");

            GetDetailsDataToForm(txtCustomer_ID.Text, "Edit");

            // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", Messages.MessageBox(), true);
        }
        else
        {
            Show("error");
        }





        /*

        dbo_UserClass clsdbo_User = new dbo_UserClass();
        SetDataAgent(clsdbo_User);

        if (IsValid)
        {
            bool success = false;
            success = dbo_UserDataClass.Add(clsdbo_User);
            if (success)
            {


                //string script = @"swal(""บันทึกสำเร็จ!"", """", ""success"")";
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);


                Show("บันทึกข้อมูลสำเร็จ!");

                // GetDetailsDataToForm(string.Empty, string.Empty);
            }

        }
        else
        {
            string script = @"swal({
                            title: ""กรุณาระบุข้อมูลให้ครบ"",
                            text: ""ข้อมูลไม่ครบ"",
                            type: ""error"",
                            confirmButtonText: ""ตกลง""
                        });";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }

        */

        /*
        Validate();

        if (IsValid)
        {
            dbo_CustomerClass clsdbo_Customer = new dbo_CustomerClass();
            SetData(clsdbo_Customer);
            bool success = false;
            success = dbo_CustomerDataClass.Add(clsdbo_Customer);
            if (success)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", Messages.MessageBox(), true);
            }
            else
            {
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", Messages.MessageBox(), true);
            }

        }
        else
        {
            string script = @"swal({
                    title: ""กรุณาระบุข้อมูลให้ครบ"",
                    text: ""ข้อมูลไม่ครบ"",
                    type: ""error"",
                    confirmButtonText: ""ตกลง""
                });";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        */
    }

    private void UpdateRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_CustomerClass clsdbo_Customer = new dbo_CustomerClass();
        clsdbo_Customer = dbo_CustomerDataClass.Select_Record(txtCustomer_ID.Text);
        SetData(clsdbo_Customer);
        bool success = false;
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        success = dbo_CustomerDataClass.Update(clsdbo_Customer, User_ID);


        if (success)
        {
            Show("บันทึกสำเร็จ!");
            //GetDetailsDataToForm(txtCustomer_ID.Text, "Edit");
        }
        else
        {
            Show("error");
        }


        /*
        dbo_UserClass oclsdbo_User = new dbo_UserClass();

        oclsdbo_User.User_ID = txtUser_ID.Text;
        oclsdbo_User = dbo_UserDataClass.Select_Record(oclsdbo_User.User_ID);

        // Validate("AgentValidation");


        SetDataAgent(oclsdbo_User);


        //if (IsValid)
        //{
        bool success = false;
        success = dbo_UserDataClass.Update(oclsdbo_User);

        if (success)
        {
            Show("บันทึกข้อมูลสำเร็จ!");
        }


        */

        /*
        dbo_CustomerClass clsdbo_Customer = new dbo_CustomerClass();
        clsdbo_Customer = dbo_CustomerDataClass.Select_Record(txtCustomer_ID.Text);
        SetData(clsdbo_Customer);

        dbo_CustomerDataClass.Update(clsdbo_Customer);
        */
    }

    private void SetData(dbo_CustomerClass clsdbo_Customer)
    {
        if (string.IsNullOrEmpty(txtCustomer_ID.Text))
        {
            clsdbo_Customer.Customer_ID = null;
        }
        else
        {
            clsdbo_Customer.Customer_ID = txtCustomer_ID.Text;
        }
        if (string.IsNullOrEmpty(txtFirst_Name.Text))
        {
            clsdbo_Customer.First_Name = null;
        }
        else
        {
            clsdbo_Customer.First_Name = txtFirst_Name.Text;
        }
        if (string.IsNullOrEmpty(txtLast_Name.Text))
        {
            clsdbo_Customer.Last_Name = null;
        }
        else
        {
            clsdbo_Customer.Last_Name = txtLast_Name.Text;
        }
        if (string.IsNullOrEmpty(txtHome_Phone_No.Text))
        {
            clsdbo_Customer.Home_Phone_No = null;
        }
        else
        {
            clsdbo_Customer.Home_Phone_No = txtHome_Phone_No.Text;
        }
        if (string.IsNullOrEmpty(txtMobile.Text))
        {
            clsdbo_Customer.Mobile = null;
        }
        else
        {
            clsdbo_Customer.Mobile = txtMobile.Text;
        }
        if (string.IsNullOrEmpty(txtContact_Name.Text))
        {
            clsdbo_Customer.Contact_Name = null;
        }
        else
        {
            clsdbo_Customer.Contact_Name = txtContact_Name.Text;
        }

        if (string.IsNullOrEmpty(txtHome_House_No.Text))
        {
            clsdbo_Customer.Home_House_No = null;
        }
        else
        {
            clsdbo_Customer.Home_House_No = txtHome_House_No.Text;
        }
        if (string.IsNullOrEmpty(txtHome_Tower.Text))
        {
            clsdbo_Customer.Home_Tower = null;
        }
        else
        {
            clsdbo_Customer.Home_Tower = txtHome_Tower.Text;
        }
        if (string.IsNullOrEmpty(txtHome_Village.Text))
        {
            clsdbo_Customer.Home_Village = null;
        }
        else
        {
            clsdbo_Customer.Home_Village = txtHome_Village.Text;
        }
        if (string.IsNullOrEmpty(txtHome_Village_No.Text))
        {
            clsdbo_Customer.Home_Village_No = null;
        }
        else
        {
            clsdbo_Customer.Home_Village_No = txtHome_Village_No.Text;
        }
        if (string.IsNullOrEmpty(txtHome_Alley.Text))
        {
            clsdbo_Customer.Home_Alley = null;
        }
        else
        {
            clsdbo_Customer.Home_Alley = txtHome_Alley.Text;
        }
        if (string.IsNullOrEmpty(txtHome_Road.Text))
        {
            clsdbo_Customer.Home_Road = null;
        }
        else
        {
            clsdbo_Customer.Home_Road = txtHome_Road.Text;
        }
        if (string.IsNullOrEmpty(txtHome_Postal_ID.Text))
        {
            clsdbo_Customer.Home_Postal_ID = null;
        }
        else
        {
            clsdbo_Customer.Home_Postal_ID = txtHome_Postal_ID.Text;
        }
        if (string.IsNullOrEmpty(txtShipment_House_No.Text))
        {
            clsdbo_Customer.Shipment_House_No = null;
        }
        else
        {
            clsdbo_Customer.Shipment_House_No = txtShipment_House_No.Text;
        }
        if (string.IsNullOrEmpty(txtShipment_Tower.Text))
        {
            clsdbo_Customer.Shipment_Tower = null;
        }
        else
        {
            clsdbo_Customer.Shipment_Tower = txtShipment_Tower.Text;
        }
        if (string.IsNullOrEmpty(txtShipment_Village.Text))
        {
            clsdbo_Customer.Shipment_Village = null;
        }
        else
        {
            clsdbo_Customer.Shipment_Village = txtShipment_Village.Text;
        }
        if (string.IsNullOrEmpty(txtShipment_Village_No.Text))
        {
            clsdbo_Customer.Shipment_Village_No = null;
        }
        else
        {
            clsdbo_Customer.Shipment_Village_No = txtShipment_Village_No.Text;
        }
        if (string.IsNullOrEmpty(txtShipment_Alley.Text))
        {
            clsdbo_Customer.Shipment_Alley = null;
        }
        else
        {
            clsdbo_Customer.Shipment_Alley = txtShipment_Alley.Text;
        }
        if (string.IsNullOrEmpty(txtShipment_Road.Text))
        {
            clsdbo_Customer.Shipment_Road = null;
        }
        else
        {
            clsdbo_Customer.Shipment_Road = txtShipment_Road.Text;
        }

        if (string.IsNullOrEmpty(txtShipment_Postal_ID.Text))
        {
            clsdbo_Customer.Shipment_Postal_ID = null;
        }
        else
        {
            clsdbo_Customer.Shipment_Postal_ID = txtShipment_Postal_ID.Text;
        }

        if (string.IsNullOrEmpty(txtSP_ID.Text))
        {
            clsdbo_Customer.SP_ID = null;
        }
        else
        {
            clsdbo_Customer.SP_ID = txtSP_ID.Text;
        }


        if (ddlCredit_Term.SelectedIndex == 0)
        {
            clsdbo_Customer.Credit_Term = null;
        }
        else
        {
            clsdbo_Customer.Credit_Term = ddlCredit_Term.SelectedValue;
        }



        if (string.IsNullOrEmpty(txtCV_Code.Text))
        {
            clsdbo_Customer.CV_Code = null;
        }
        else
        {
            clsdbo_Customer.CV_Code = txtCV_Code.Text;
        }

        if (string.IsNullOrEmpty(txtBirthday.Text))
        {
            clsdbo_Customer.Birthday = null;
        }
        else
        {
            clsdbo_Customer.Birthday = Convert.ToDateTime(txtBirthday.Text);
        }



        if (ddlHome_District.SelectedIndex == 0)
        {
            clsdbo_Customer.Home_District = null;
        }
        else
        {
            clsdbo_Customer.Home_District = ddlHome_District.SelectedValue;
        }

        if (ddlHome_Sub_district.SelectedIndex == 0)
        {
            clsdbo_Customer.Home_Sub_district = null;
        }
        else
        {
            clsdbo_Customer.Home_Sub_district = ddlHome_Sub_district.SelectedValue;
        }

        if (ddlHome_Province.SelectedIndex == 0)
        {
            clsdbo_Customer.Home_Province = null;
        }
        else
        {
            clsdbo_Customer.Home_Province = ddlHome_Province.SelectedValue;
        }

        if (ddlShipment_District.SelectedIndex == 0)
        {
            clsdbo_Customer.Shipment_District = null;
        }
        else
        {
            clsdbo_Customer.Shipment_District = ddlShipment_District.SelectedValue;
        }

        if (ddlShipment_Sub_district.SelectedIndex == 0)
        {
            clsdbo_Customer.Shipment_Sub_district = null;
        }
        else
        {
            clsdbo_Customer.Shipment_Sub_district = ddlShipment_Sub_district.SelectedValue;
        }

        if (ddlShipment_Province.SelectedIndex == 0)
        {
            clsdbo_Customer.Shipment_Province = null;
        }
        else
        {
            clsdbo_Customer.Shipment_Province = ddlShipment_Province.SelectedValue;
        }

        //clsdbo_Customer.Status = ddlCustomerStatus.Text;
        if (ddlCustomerStatus.SelectedIndex == 0)
        {
            clsdbo_Customer.Status = null;
        }
        else
        {
            clsdbo_Customer.Status = ddlCustomerStatus.SelectedValue;
        }

        if (ddlResidentType_ID.SelectedIndex == 0)
        {
            clsdbo_Customer.Residence_Type_ID = null;
        }
        else
        {
            clsdbo_Customer.Residence_Type_ID = ddlResidentType_ID.SelectedValue;
        }

        if (ddlCustomerType.SelectedIndex == 0)
        {
            clsdbo_Customer.Customer_Type = null;
        }
        else
        {
            clsdbo_Customer.Customer_Type = ddlCustomerType.SelectedValue;
        }


        if (ddlPaymentType.SelectedIndex == 0)
        {
            clsdbo_Customer.Payment_Type = null;
        }
        else
        {
            clsdbo_Customer.Payment_Type = ddlPaymentType.SelectedValue;
        }

        if (ddlBillingType.SelectedIndex == 0)
        {
            clsdbo_Customer.Billing_Type = null;
        }
        else
        {
            clsdbo_Customer.Billing_Type = ddlBillingType.SelectedValue;
            if (ddlBillingType.SelectedIndex == 1)
            {
                clsdbo_Customer.Billing_Day_of_Week = null;
                clsdbo_Customer.Billing_Day_of_Month = null;
                clsdbo_Customer.Billing_Day_of_Other = null;
                clsdbo_Customer.Due_Billing_Day_of_Week = null;
                clsdbo_Customer.Due_Billing_Day_of_Month = null;
                clsdbo_Customer.Due_Billing_Day_of_Other = null;
            }
            else if (ddlBillingType.SelectedIndex == 2)
            {
                if (ddlBilling_Day_of_Week.SelectedIndex == 0)
                {
                    clsdbo_Customer.Billing_Day_of_Week = null;
                }
                else
                {
                    clsdbo_Customer.Billing_Day_of_Week = ddlBilling_Day_of_Week.SelectedValue;
                }
                clsdbo_Customer.Billing_Day_of_Month = null;
                clsdbo_Customer.Billing_Day_of_Other = null;
                if (ddlDue_Billing_Day_of_Week.SelectedIndex == 0)
                {
                    clsdbo_Customer.Due_Billing_Day_of_Week = null;
                }
                else
                {
                    clsdbo_Customer.Due_Billing_Day_of_Week = ddlDue_Billing_Day_of_Week.SelectedValue;
                }
                clsdbo_Customer.Due_Billing_Day_of_Month = null;
                clsdbo_Customer.Due_Billing_Day_of_Other = null;
            }
            else if (ddlBillingType.SelectedIndex == 3)
            {
                clsdbo_Customer.Billing_Day_of_Week = null;
                if (ddlBilling_Day_of_Month.SelectedIndex == 0)
                {
                    clsdbo_Customer.Billing_Day_of_Month = null;
                }
                else
                {
                    clsdbo_Customer.Billing_Day_of_Month = ddlBilling_Day_of_Month.SelectedValue;
                }
                clsdbo_Customer.Billing_Day_of_Other = null;
                clsdbo_Customer.Due_Billing_Day_of_Week = null;
                if (ddlDue_Billing_Day_of_Month.SelectedIndex == 0)
                {
                    clsdbo_Customer.Due_Billing_Day_of_Month = null;
                }
                else
                {
                    clsdbo_Customer.Due_Billing_Day_of_Month = ddlDue_Billing_Day_of_Month.SelectedValue;
                }
                clsdbo_Customer.Due_Billing_Day_of_Other = null;
            }
            else if (ddlBillingType.SelectedIndex == 4)
            {
                clsdbo_Customer.Billing_Day_of_Week = null;
                clsdbo_Customer.Billing_Day_of_Month = null;
                if (string.IsNullOrEmpty(txtBilling_Day_Other.Text))
                {
                    clsdbo_Customer.Billing_Day_of_Other = null;
                }
                else
                {
                    clsdbo_Customer.Billing_Day_of_Other = txtBilling_Day_Other.Text;
                }
                clsdbo_Customer.Due_Billing_Day_of_Week = null;
                clsdbo_Customer.Due_Billing_Day_of_Month = null;
                if (string.IsNullOrEmpty(txtDue_Billing_Day_Other.Text))
                {
                    clsdbo_Customer.Due_Billing_Day_of_Other = null;
                }
                else
                {
                    clsdbo_Customer.Due_Billing_Day_of_Other = txtDue_Billing_Day_Other.Text;
                }
            }
            else
            {
                clsdbo_Customer.Billing_Day_of_Week = null;
                clsdbo_Customer.Billing_Day_of_Month = null;
                clsdbo_Customer.Billing_Day_of_Other = null;
                clsdbo_Customer.Due_Billing_Day_of_Week = null;
                clsdbo_Customer.Due_Billing_Day_of_Month = null;
                clsdbo_Customer.Due_Billing_Day_of_Other = null;
            }
        }



        if (string.IsNullOrEmpty(txtRemarks.Text))
        {
            clsdbo_Customer.Remarks = null;
        }
        else
        {
            clsdbo_Customer.Remarks = txtRemarks.Text;
        }

        if (ddlSP.SelectedIndex == 0)
        {
            clsdbo_Customer.SP_ID = null;
        }
        else
        {
            clsdbo_Customer.SP_ID = ddlSP.SelectedValue;
            clsdbo_Customer.SP_Name = ddlSP.SelectedItem.Text;
        }
        if (ddlStatus.SelectedIndex == 0)
        {
            clsdbo_Customer.Active_status = null;
        }
        else
        {
            clsdbo_Customer.Active_status = ddlStatus.SelectedValue;
        }
        if (string.IsNullOrEmpty(txtMemberDate.Text))
        {
            clsdbo_Customer.Member_Date = null;
        }
        else
        {
            clsdbo_Customer.Member_Date = Convert.ToDateTime(txtMemberDate.Text);
        }
        if (string.IsNullOrEmpty(txtShopName.Text))
        {
            clsdbo_Customer.Shop_Name = null;
        }
        else
        {
            clsdbo_Customer.Shop_Name = txtShopName.Text;
        }
        clsdbo_Customer.ReceiveDate_Hour = ddlReceiveProduct_Hour.SelectedValue;
        clsdbo_Customer.ReceiveDate_Minute = ddlReceiveProduct_Minute.SelectedValue;
        clsdbo_Customer.ReceiveToDate_Hour = ddlReceiveToProduct_Hour.SelectedValue;
        clsdbo_Customer.ReceiveToDate_Minute = ddlReceiveToProduct_Minute.SelectedValue;
    }
    #endregion             

    #region GridView Row Command
    protected void grdCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            if (e.CommandName == "View")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string Customer_ID = lnkView.CommandArgument;
                GetDetailsDataToForm(Customer_ID, "View");
            }
            else if (e.CommandName == "_Edit")
            {
                LinkButton lnkView1 = (LinkButton)e.CommandSource;
                string Customer_ID = lnkView1.CommandArgument;

                GetDetailsDataToForm(Customer_ID, "Edit");
            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void grdCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int index = e.RowIndex;
            LinkButton lnkB_Customer_ID = (LinkButton)grdCustomer.Rows[e.RowIndex].FindControl("lnkB_Customer_ID");
            dbo_CustomerClass clsCustomer = new dbo_CustomerClass();
            clsCustomer.Customer_ID = lnkB_Customer_ID.Text;
            clsCustomer.Active_status = "0";
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            if (dbo_CustomerDataClass.Delete(clsCustomer.Customer_ID, User_ID))
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show("ลบข้อมูลสำเร็จ");
            }

            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            string sp = (ddlSearchSP.SelectedIndex == 0 ? string.Empty : ddlSearchSP.SelectedItem.Value);
            string type = (ddlSearchCustomerType.SelectedIndex == 0 ? string.Empty : ddlSearchCustomerType.SelectedItem.Value);
            string status = (ddlSearchCustomerStatus.SelectedIndex == 0 ? string.Empty : ddlSearchCustomerStatus.SelectedItem.Value);
            string resident = (ddlSearchResidence_Type_ID.SelectedIndex == 0 ? string.Empty : ddlSearchResidence_Type_ID.SelectedItem.Value);
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            List<dbo_CustomerClass> customer = dbo_CustomerDataClass.Search(txtSearchFirst_Name.Text, type, txtSearchCustomerID.Text, resident, txtSearchHome_House_No.Text, txtSearchMobile.Text, sp, status, user_class.CV_CODE);
            
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            if (user_class.User_Group_ID == "CP Meiji")
            {
                List<string> cv_code1 = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f.CV_Code));
                if (cv_code1.Count != 0)
                {
                    //List<dbo_CustomerClass> customer_ = new List<dbo_CustomerClass>(customer.Where(f => cv_code1.Contains(f.CV_Code)).Select(f => f));
                    List<dbo_CustomerClass> customer_ = (from p in customer where cv_code1.Any(f => f.Contains(p.CV_Code)) select p).OrderBy(f => f.Customer_ID).ToList();
                    if (customer_.Count > 0)
                    {
                        grdCustomer.DataSource = customer_;
                        grdCustomer.DataBind();

                        grdCustomer.Visible = true;
                        pnlNoRec.Visible = false;
                    }
                    else
                    {
                        grdCustomer.Visible = false;
                        pnlNoRec.Visible = true;
                    }
                }
                else
                {
                    string region = user_class.Region;

                    string[] regions = region.Split(',');

                    List<string> cv_code_ = new List<string>();

                    foreach (string in_region in regions)
                    {
                        List<string> cv_code2 = new List<string>(agent.Where(f => f.Location_Region == in_region).Select(f => f.CV_Code));
                        foreach (string _cv in cv_code2)
                        {
                            cv_code_.Add(_cv);
                        }
                    }
                    List<dbo_CustomerClass> customer_ = (from p in customer where cv_code_.Any(f => f.Contains(p.CV_Code)) select p).OrderBy(f => f.Customer_ID).ToList();
                    if (customer_.Count > 0)
                    {
                        grdCustomer.DataSource = customer_;
                        grdCustomer.DataBind();

                        grdCustomer.Visible = true;
                        pnlNoRec.Visible = false;
                    }
                    else
                    {
                        grdCustomer.Visible = false;
                        pnlNoRec.Visible = true;
                    }
                }
            }
            else
            {
                List<dbo_CustomerClass> customer_ = new List<dbo_CustomerClass>(customer.Where(f => f.CV_Code == user_class.CV_CODE).Select(f => f));
 
                if (customer_.Count > 0)
                {
                    grdCustomer.DataSource = customer_;
                    grdCustomer.DataBind();

                    grdCustomer.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    grdCustomer.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdCustomer.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        grdCustomer.PageIndex = pageList.SelectedIndex;
        btnSearch_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void grdCustomer_DataBound(object sender, EventArgs e)
    {
        GridViewRow pagerRow = grdCustomer.BottomPagerRow;

        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {
            for (int i = 0; i < grdCustomer.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                if (i == grdCustomer.PageIndex)
                {
                    item.Selected = true;
                }
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = grdCustomer.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + grdCustomer.PageCount.ToString();

        }
    }
    #endregion  
   
}