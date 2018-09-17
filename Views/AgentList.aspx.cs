#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_AgentList : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(typeof(Views_AgentList));
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txt_CV_Code.Attributes.Add("onchange", "myApp.showPleaseWait();");
                txtSearchCV_CODE.Attributes.Add("onchange", "myApp.showPleaseWait();");

                ddlInvoice_Sub_district.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlInvoice_District.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlInvoice_Province.Attributes.Add("onchange", "myApp.showPleaseWait();");

                ddlLocation_Sub_district.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlLocation_District.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlLocation_Province.Attributes.Add("onchange", "myApp.showPleaseWait();");

                ddlSearchPrefix_ID.Attributes.Add("onchange", "myApp.showPleaseWait();");

                txtSmall_Case.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
                txtSmall_Case.Attributes.Add("ondrop", "javascript:return false;");
                txtSmall_Case.Attributes.Add("onpaste", "javascript:return false;");

                txtLarge_Case.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
                txtLarge_Case.Attributes.Add("ondrop", "javascript:return false;");
                txtLarge_Case.Attributes.Add("onpaste", "javascript:return false;");

                txtRoom_Size.Attributes.Add("onkeypress", "javascript:return IsNumeric(event);");
                txtRoom_Size.Attributes.Add("ondrop", "javascript:return false;");
                txtRoom_Size.Attributes.Add("onpaste", "javascript:return false;");

                txtPledge_Amount.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                txtPledge_Amount.Attributes.Add("ondrop", "javascript:return false;");
                txtPledge_Amount.Attributes.Add("onpaste", "javascript:return false;");

                txtCash_Deposit.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                txtCash_Deposit.Attributes.Add("ondrop", "javascript:return false;");
                txtCash_Deposit.Attributes.Add("onpaste", "javascript:return false;");

                txtBank_Guarantee.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
                txtBank_Guarantee.Attributes.Add("ondrop", "javascript:return false;");
                txtBank_Guarantee.Attributes.Add("onpaste", "javascript:return false;");

                this.grdAgent.Attributes.Add("style", "word-break:break-all; word-wrap:break-word"); 

                // initial_data();
                SetUpDrowDownList();
                SetEmptyUserGrid();
                showPanel("pnlGrid");

                btnSearchSubmit_Click(sender, e);
              
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            if (btnSave.Text == "แก้ไข")
            {
                GetDetailsDataToForm(txt_CV_Code.Text, "Edit");
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                Validate("Validation");

                if (Page.IsValid)
                {
                    if (btnSaveMode.Value == "บันทึก")
                    {
                        InsertRecord();

                        SetUpDrowDownList();
                        // GetDetailsDataToForm(txt_CV_Code.Text, "Edit");
                    }
                    else
                    {
                        UpdateRecord();
                        btnSearchSubmit_Click(null, null);
                    }

                    showPanel("pnlGrid");
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

    protected void ButtonCreateNew_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            GetDetailsDataToForm(string.Empty, string.Empty);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        int i = 0;
        try
        {
            string cv_code = txtSearchCV_CODE.Text;
            string prefix = ddlSearchPrefix_ID.SelectedIndex == 0 ? string.Empty : ddlSearchPrefix_ID.SelectedValue;
            string agent_type = ddlSearchAgentType.SelectedIndex == 0 ? string.Empty : ddlSearchAgentType.SelectedValue;
            string Area = txtSearchConcessionArea.Text;
            string sp = ddlSearchSP.SelectedIndex == 0 ? string.Empty : ddlSearchSP.SelectedValue;
            string sm = ddlSearchSM.SelectedIndex == 0 ? string.Empty : ddlSearchSM.SelectedValue;
            string dm = ddlSearchDM.SelectedIndex == 0 ? string.Empty : ddlSearchDM.SelectedValue;
            string gm = ddlSearchGM.SelectedIndex == 0 ? string.Empty : ddlSearchGM.SelectedValue;
            string apv = ddlSearchAPV.SelectedIndex == 0 ? string.Empty : ddlSearchAPV.SelectedValue;
            string status = ddlSearchStatus.SelectedIndex == 0 ? string.Empty : ddlSearchStatus.SelectedValue;
            string grade = ddlSearchGrade.SelectedIndex == 0 ? string.Empty : ddlSearchGrade.SelectedValue;

            string region = string.Empty;

            foreach (ListItem item in ddlSearchRegion.Items)
            {
                if (item.Selected)
                {
                    if (string.IsNullOrEmpty(region))
                    {
                        region = item.Value;
                    }
                    else
                    {
                        region = region + "," + item.Value;
                    }
                }
            }

            logger.Debug("prefix " + prefix);


            if (prefix == "")
            {
                prefix = string.Empty;
                List<dbo_AgentClass> agentList = new List<dbo_AgentClass>();

                if (txtSearchCV_CODE.Text.Trim() != "")
                {
                    for (i = 1; i < ddlSearchPrefix_ID.Items.Count; i++)
                    {
                        List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(ddlSearchPrefix_ID.Items[i].Value
                            , prefix, agent_type, Area, sp, sm, dm, gm, apv, region, status, grade);
                        foreach (dbo_AgentClass _agent in agent)
                        {
                            if (_agent.CV_Code == txtSearchCV_CODE.Text.Trim())
                            {
                                agentList.Add(_agent);
                            } 
                        }
                    }
                }
                else
                {
                    for (i = 1; i < ddlSearchPrefix_ID.Items.Count; i++)
                    {
                        List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(ddlSearchPrefix_ID.Items[i].Value
                            , prefix, agent_type, Area, sp, sm, dm, gm, apv, region, status, grade);
                        foreach (dbo_AgentClass _agent in agent)
                        {
                            agentList.Add(_agent);
                        }
                    }
                }

                if (agentList.Count > 0)
                {
                    grdAgent.DataSource = agentList;
                    grdAgent.DataBind();

                    grdAgent.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    grdAgent.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }
            else
            {
                //prefix = string.Empty;
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(cv_code, prefix, agent_type, Area, sp, sm, dm, gm, apv, region, status, grade);
                //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(prefix, prefix, agent_type, Area, sp, sm, dm, gm, apv, region, status, grade);

                if (agent.Count > 0)
                {
                    grdAgent.DataSource = agent;
                    grdAgent.DataBind();

                    grdAgent.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    grdAgent.Visible = false;
                    pnlNoRec.Visible = true;
                }

            }

            //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(cv_code, prefix, agent_type, Area, sp, sm, dm, gm, apv, region, status, grade);

            //grdAgent.DataSource = agent;
            //grdAgent.DataBind();

           

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

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {

        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            // initial_data();

            SetUpDrowDownList();
            SetEmptyUserGrid();
            showPanel("pnlGrid");

            grdAgent.Visible = false;
            pnlNoRec.Visible = false;

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    protected void ButtonCancelSave_Click(object sender, EventArgs e)
    {
        // initial_data();
        //SetUpDrowDownList();
        //SetEmptyUserGrid();
        showPanel("pnlGrid");

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnCopyAddress_Click(object sender, EventArgs e)
    {

        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            txtInvoice_House_No.Text = txtLocation_House_No.Text;
            txtInvoice_Village.Text = txtLocation_Village.Text;
            txtInvoice_Village_No.Text = txtLocation_Village_No.Text;
            txtInvoice_Alley.Text = txtLocation_Alley.Text;
            txtInvoice_Road.Text = txtLocation_Road.Text;
            txtInvoice_Postal_ID.Text = txtLocation_Postal_ID.Text;


            ddlInvoice_Sub_district.ClearSelection();
            ddlInvoice_District.ClearSelection();
            ddlInvoice_Province.ClearSelection();
            ddlInvoice_Region.ClearSelection();



            if (ddlInvoice_Province.Items.FindByText(ddlLocation_Province.SelectedValue) != null)
            {
                ddlInvoice_Province.Items.FindByText(ddlLocation_Province.SelectedValue).Selected = true;
                List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlInvoice_Province.Text);
                List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                             .Select(grp => grp.First())
                             .ToList();


                dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
                tmp_tambol.Insert(0, first_);
                ddlInvoice_District.DataSource = tmp_tambol;
                ddlInvoice_District.DataBind();
            }

            if (ddlInvoice_District.Items.FindByText(ddlLocation_District.SelectedValue) != null)
            {
                ddlInvoice_District.Items.FindByText(ddlLocation_District.SelectedValue).Selected = true;

                List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlInvoice_District.Text, ddlInvoice_Province.Text);

                List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                             .Select(grp => grp.First())
                             .ToList();


                dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
                tmp_tambol.Insert(0, first_);
                ddlInvoice_Sub_district.DataSource = tmp_tambol;
                ddlInvoice_Sub_district.DataBind();


            }


            if (ddlInvoice_Sub_district.Items.FindByText(ddlLocation_Sub_district.SelectedValue) != null)
                ddlInvoice_Sub_district.Items.FindByText(ddlLocation_Sub_district.SelectedValue).Selected = true;

            if (ddlInvoice_Region.Items.FindByValue(ddlLocation_Region.SelectedValue) != null)
                ddlInvoice_Region.Items.FindByValue(ddlLocation_Region.SelectedValue).Selected = true;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }




    }

    protected void txt_CV_Code_TextChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            dbo_AgentClass agent = dbo_AgentDataClass.Select_Record(txt_CV_Code.Text);
            if (agent != null)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("CV Code ไม่สามารถซ้ำได้");
                txt_CV_Code.Text = string.Empty;
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlInvoice_Province_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlInvoice_Province.Text);

            List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                         .Select(grp => grp.First())
                         .ToList();


            dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
            tmp_tambol.Insert(0, first_);
            ddlInvoice_District.DataSource = tmp_tambol;
            ddlInvoice_District.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void ddlInvoice_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlInvoice_District.Text, ddlInvoice_Province.Text);

            List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                         .Select(grp => grp.First())
                         .ToList();


            dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
            tmp_tambol.Insert(0, first_);


            ddlInvoice_Sub_district.DataSource = tmp_tambol;
            ddlInvoice_Sub_district.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }

        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void ddlLocation_Province_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlLocation_Province.Text);

            List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                         .Select(grp => grp.First())
                         .ToList();


            dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
            tmp_tambol.Insert(0, first_);
            ddlLocation_District.DataSource = tmp_tambol;
            ddlLocation_District.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void ddlLocation_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlLocation_District.Text, ddlLocation_Province.Text);

            List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                         .Select(grp => grp.First())
                         .ToList();

            dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
            tmp_tambol.Insert(0, first_);


            ddlLocation_Sub_district.DataSource = tmp_tambol;
            ddlLocation_Sub_district.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlSearchPrefix_ID_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            txtSearchCV_CODE.Text = string.Empty;
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            txtSearchCV_CODE.Text = ddlSearchPrefix_ID.SelectedValue;

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void ddlLocation_Sub_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlLocation_District.Text, ddlLocation_Province.Text);

            string postal_id = tambol.FirstOrDefault(f => f.Sub_district == ddlLocation_Sub_district.Text).Postal_ID;
            txtLocation_Postal_ID.Text = postal_id;

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlInvoice_Sub_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlInvoice_District.Text, ddlInvoice_Province.Text);

            string postal_id = tambol.FirstOrDefault(f => f.Sub_district == ddlInvoice_Sub_district.Text).Postal_ID;
            txtInvoice_Postal_ID.Text = postal_id;

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }
    #endregion

    #region Method
    [Obsolete]
    private void initial_data()
    {
        try
        {
            SetUpDrowDownList();

            showPanel("pnlGrid");

            List<dbo_AgentClass> itm = new List<dbo_AgentClass>();
            grdAgent.DataSource = itm;
            grdAgent.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    private void SetEmptyUserGrid()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            if(grdAgent.Rows.Count > 0)
            {
                List<dbo_AgentClass> itm = new List<dbo_AgentClass>();
                grdAgent.DataSource = itm;
                grdAgent.DataBind();
            }
          

            txtSearchCV_CODE.Text = string.Empty;
            ddlSearchPrefix_ID.ClearSelection();
            ddlSearchAgentType.ClearSelection();
            txtSearchConcessionArea.Text = string.Empty;
            ddlSearchSP.ClearSelection();
            ddlSearchSM.ClearSelection();
            ddlSearchDM.ClearSelection();
            ddlSearchGM.ClearSelection();
            ddlSearchAPV.ClearSelection();
            ddlSearchRegion.ClearSelection();
            ddlSearchStatus.ClearSelection();
            ddlSearchGrade.ClearSelection();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            Dictionary<string, string> title = dbo_ItemDataClass.GetDropDown("08");
            ddlPrefix_ID.DataSource = title;
            ddlPrefix_ID.DataBind();


            Dictionary<string, string> grade = dbo_ItemDataClass.GetDropDown("11");
            ddlSearchGrade.DataSource = grade;
            ddlSearchGrade.DataBind();

            ddlGrade.DataSource = grade;
            ddlGrade.DataBind();


            Dictionary<string, string> agenttype = dbo_ItemDataClass.GetDropDown("13");
            ddlSearchAgentType.DataSource = agenttype;
            ddlSearchAgentType.DataBind();
            ddlAgent_Type_ID.DataSource = agenttype;
            ddlAgent_Type_ID.DataBind();

            Dictionary<string, string> Term_of_payment = dbo_ItemDataClass.GetDropDown("14");
            ddlTerm_of_payment.DataSource = Term_of_payment;
            ddlTerm_of_payment.DataBind();



            Dictionary<string, string> bank = dbo_ItemDataClass.GetDropDown("12");
            ddlBank.DataSource = bank;
            ddlBank.DataBind();

            List<dbo_UserClass> user = dbo_UserDataClass.Search("", "", "", "", "Active", "CP Meiji", "", "", null, string.Empty, string.Empty);
            dbo_UserClass u = new dbo_UserClass();
            u.FullName = "==ระบุ==";
            u.User_ID = string.Empty;
            user.Insert(0, u);

            ddlSearchSP.DataSource = user.Where(f => f.Position == "พนักงานขาย" || f.FullName == "==ระบุ==");
            ddlSearchSP.DataBind();

            ddlSearchSM.DataSource = user.Where(f => f.Position == "ผู้จัดการแผนก" || f.FullName == "==ระบุ==");
            ddlSearchSM.DataBind();

            ddlSearchDM.DataSource = user.Where(f => f.Position == "ผู้จัดการฝ่าย" || f.FullName == "==ระบุ==");
            ddlSearchDM.DataBind();

            ddlSearchGM.DataSource = user.Where(f => f.Position == "ผู้จัดการทั่วไป" || f.FullName == "==ระบุ==");
            ddlSearchGM.DataBind();

            ddlSearchAPV.DataSource = user.Where(f => f.Position == "ผู้ช่วยกรรมการผู้จัดการ" || f.FullName == "==ระบุ==");
            ddlSearchAPV.DataBind();

            ddlSD_ID.DataSource = user.Where(f => f.Position == "พนักงานขาย" || f.FullName == "==ระบุ==");
            ddlSD_ID.DataBind();
            ddlSM_ID.DataSource = user.Where(f => f.Position == "ผู้จัดการแผนก" || f.FullName == "==ระบุ==");
            ddlSM_ID.DataBind();
            ddlDM_ID.DataSource = user.Where(f => f.Position == "ผู้จัดการฝ่าย" || f.FullName == "==ระบุ==");
            ddlDM_ID.DataBind();
            ddlManager.DataSource = user.Where(f => f.Position == "ผู้จัดการทั่วไป" || f.FullName == "==ระบุ==");
            ddlManager.DataBind();
            ddlAVP.DataSource = user.Where(f => f.Position == "ผู้ช่วยกรรมการผู้จัดการ" || f.FullName == "==ระบุ==");
            ddlAVP.DataBind();

            ddlGrade.DataSource = grade;
            ddlGrade.DataBind();


            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

            dbo_AgentClass a = new dbo_AgentClass();
            a.Prefix_ID = "==ระบุ==";
            a.CV_Code = string.Empty;
            //agent.Insert(0, a);

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            if (user_class.User_Group_ID == "CP Meiji")
            {
                List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));
                if (cv_code1.Count != 0)
                {
                    cv_code1.Insert(0, a);
                    ddlSearchPrefix_ID.DataSource = cv_code1;
                    ddlSearchPrefix_ID.DataBind();
                }
                else
                {
                    string region = user_class.Region;

                    string[] regions = region.Split(',');

                    List<dbo_AgentClass> cv_code_ = new List<dbo_AgentClass>();
                    cv_code_.Insert(0, a);

                    foreach (string in_region in regions)
                    {
                        List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => f.Location_Region == in_region).Select(f => f));
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            cv_code_.Add(_cv);
                        }
                    }
                    ddlSearchPrefix_ID.DataSource = cv_code_;
                    ddlSearchPrefix_ID.DataBind();

                }
            }
            else
            {
                List<dbo_AgentClass> cv_code = dbo_AgentDataClass.Search(user_class.CV_CODE, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                cv_code.Insert(0, a);
                ddlSearchPrefix_ID.DataSource = cv_code;
                ddlSearchPrefix_ID.DataBind();
            }

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.SelectAll();

            List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Province)
                         .Select(grp => grp.First())
                         .ToList();


            dbo_TambolClass first_ = new dbo_TambolClass() { Province = "==ระบุ==" };
            tmp_tambol.Insert(0, first_);


            List<dbo_TambolClass> disti = new List<dbo_TambolClass>();
            disti.Add(new dbo_TambolClass() { District = "==ระบุ==", Sub_district = "==ระบุ==" });

            ddlLocation_Province.DataSource = tmp_tambol;
            ddlLocation_Province.DataBind();

            ddlLocation_District.DataSource = disti;
            ddlLocation_District.DataBind();

            ddlLocation_Sub_district.DataSource = disti;
            ddlLocation_Sub_district.DataBind();

            ddlInvoice_Province.DataSource = tmp_tambol;
            ddlInvoice_Province.DataBind();

            ddlInvoice_District.DataSource = disti;
            ddlInvoice_District.DataBind();

            ddlInvoice_Sub_district.DataSource = disti;
            ddlInvoice_Sub_district.DataBind();

            Dictionary<string, string> region_ = dbo_ItemDataClass.GetDropDown("07");
            //ddlSearchRegion(new dbo_ItemDataClass() { region = "==ระบุ==" });
            ddlLocation_Region.DataSource = region_;
            ddlLocation_Region.DataBind();

            ddlInvoice_Region.DataSource = region_;
            ddlInvoice_Region.DataBind();

            //ddlSearchRegion.DataSource = region.Where(f => f.Key != string.Empty);
            ddlSearchRegion.DataSource = region_;
            ddlSearchRegion.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    private void GetDetailsDataToForm(string id, string Mode)
    {

        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        showPanel("pnlForm");

        txt_CV_Code.Text = string.Empty;
        txt_First_Name.Text = string.Empty;
        txtLast_Name.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtTax_ID.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtConcession_Area.Text = string.Empty;
        txtOwner_First_Name.Text = string.Empty;
        txtOwner_Last_Name.Text = string.Empty;
        txtOwner_Phone_No1.Text = string.Empty;
        txtOwner_Phone_No2.Text = string.Empty;
        txtContact_First_Name.Text = string.Empty;
        txtContact_Last_Name.Text = string.Empty;
        txtContact_Phone_No1.Text = string.Empty;
        txtContact_Phone_No2.Text = string.Empty;
        txtLocation_House_No.Text = string.Empty;
        txtLocation_Village.Text = string.Empty;
        txtLocation_Village_No.Text = string.Empty;
        txtLocation_Alley.Text = string.Empty;
        txtLocation_Road.Text = string.Empty;
        txtLocation_Postal_ID.Text = string.Empty;
        txtInvoice_House_No.Text = string.Empty;
        txtInvoice_Village.Text = string.Empty;
        txtInvoice_Village_No.Text = string.Empty;
        txtInvoice_Alley.Text = string.Empty;
        txtInvoice_Road.Text = string.Empty;
        txtInvoice_Postal_ID.Text = string.Empty;
        txtStart_Effective_Date.Text = string.Empty;
        txtFirst_Order_Date.Text = string.Empty;
        txtGo_out_of_business_Date.Text = string.Empty;
        txtOther_Document.Text = string.Empty;
        txtSmall_Case.Text = string.Empty;
        txtLarge_Case.Text = string.Empty;
        txtPledge_Amount.Text = string.Empty;
        txtRoom_Size.Text = string.Empty;
        txtCash_Deposit.Text = string.Empty;
        txtBank_Guarantee.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtGrade_Effective_Date.Text = string.Empty;
        txtHome_Phone_No.Text = string.Empty;


        ddlAVP.ClearSelection();
        ddlBank.ClearSelection();
        ddlSD_ID.ClearSelection();
        ddlSM_ID.ClearSelection();
        ddlDM_ID.ClearSelection();
        ddlManager.ClearSelection();

        ddlStatus.ClearSelection();
        ddlGrade.ClearSelection();
        ddlPrefix_ID.ClearSelection();
        ddlAgent_Type_ID.ClearSelection();
        ddlInvoice_Region.ClearSelection();
        ddlLocation_Region.ClearSelection();
        ddlTerm_of_payment.ClearSelection();
        ddlInvoice_Province.ClearSelection();
        ddlInvoice_District.ClearSelection();
        ddlLocation_District.ClearSelection();
        ddlLocation_Province.ClearSelection();
        ddlInvoice_Sub_district.ClearSelection();
        ddlLocation_Sub_district.ClearSelection();

        chkTab1.Checked = false;
        chkTab2.Checked = false;
        chkTab3.Checked = false;
        chkTab4.Checked = false;
        chkTab5.Checked = false;

        chk01.Checked = false;
        chk02.Checked = false;
        chk03.Checked = false;
        chk04.Checked = false;
        chk05.Checked = false;
        chk06.Checked = false;
        chk07.Checked = false;
        chk08.Checked = false;
        chk09.Checked = false;
        chk10.Checked = false;
        chk11.Checked = false;

        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(id);


                txt_CV_Code.Text = System.Convert.ToString(clsdbo_Agent.CV_Code);
                txt_First_Name.Text = System.Convert.ToString(clsdbo_Agent.First_Name);
                txtLast_Name.Text = System.Convert.ToString(clsdbo_Agent.Last_Name);
                txtMobile.Text = System.Convert.ToString(clsdbo_Agent.Mobile);
                txtHome_Phone_No.Text = System.Convert.ToString(clsdbo_Agent.Home_Phone_No);
                txtTax_ID.Text = System.Convert.ToString(clsdbo_Agent.Tax_ID);
                txtEmail.Text = System.Convert.ToString(clsdbo_Agent.Email);
                txtFax.Text = System.Convert.ToString(clsdbo_Agent.Fax);
                txtConcession_Area.Text = System.Convert.ToString(clsdbo_Agent.Concession_Area);
                txtOwner_First_Name.Text = System.Convert.ToString(clsdbo_Agent.Owner_First_Name);
                txtOwner_Last_Name.Text = System.Convert.ToString(clsdbo_Agent.Owner_Last_Name);
                txtOwner_Phone_No1.Text = System.Convert.ToString(clsdbo_Agent.Owner_Phone_No1);
                txtOwner_Phone_No2.Text = System.Convert.ToString(clsdbo_Agent.Owner_Phone_No2);
                txtContact_First_Name.Text = System.Convert.ToString(clsdbo_Agent.Contact_First_Name);
                txtContact_Last_Name.Text = System.Convert.ToString(clsdbo_Agent.Contact_Last_Name);
                txtContact_Phone_No1.Text = System.Convert.ToString(clsdbo_Agent.Contact_Phone_No1);
                txtContact_Phone_No2.Text = System.Convert.ToString(clsdbo_Agent.Contact_Phone_No2);
                txtLocation_House_No.Text = System.Convert.ToString(clsdbo_Agent.Location_House_No);
                txtLocation_Village.Text = System.Convert.ToString(clsdbo_Agent.Location_Village);
                txtLocation_Village_No.Text = System.Convert.ToString(clsdbo_Agent.Location_Village_No);
                txtLocation_Alley.Text = System.Convert.ToString(clsdbo_Agent.Location_Alley);
                txtLocation_Road.Text = System.Convert.ToString(clsdbo_Agent.Location_Road);

                txtLocation_Postal_ID.Text = System.Convert.ToString(clsdbo_Agent.Location_Postal_ID);
                txtInvoice_House_No.Text = System.Convert.ToString(clsdbo_Agent.Invoice_House_No);
                txtInvoice_Village.Text = System.Convert.ToString(clsdbo_Agent.Invoice_Village);
                txtInvoice_Village_No.Text = System.Convert.ToString(clsdbo_Agent.Invoice_Village_No);
                txtInvoice_Alley.Text = System.Convert.ToString(clsdbo_Agent.Invoice_Alley);
                txtInvoice_Road.Text = System.Convert.ToString(clsdbo_Agent.Invoice_Road);

                txtInvoice_Postal_ID.Text = System.Convert.ToString(clsdbo_Agent.Invoice_Postal_ID);

                txtOther_Document.Text = System.Convert.ToString(clsdbo_Agent.Other_Document);
                txtSmall_Case.Text = System.Convert.ToString(string.Format("{0:#,##0}", clsdbo_Agent.Small_Case));
                txtLarge_Case.Text = System.Convert.ToString(string.Format("{0:#,##0}", clsdbo_Agent.Large_Case));
                txtPledge_Amount.Text = System.Convert.ToString(string.Format("{0:#,##0.00}", clsdbo_Agent.Pledge_Amount));
                txtRoom_Size.Text = System.Convert.ToString(string.Format("{0:#,##0}", clsdbo_Agent.Room_Size));
                txtCash_Deposit.Text = System.Convert.ToString(string.Format("{0:#,##0.00}", clsdbo_Agent.Cash_Deposit));
                txtBank_Guarantee.Text = System.Convert.ToString(string.Format("{0:#,##0.00}", clsdbo_Agent.Bank_Guarantee));
                txtRemarks.Text = System.Convert.ToString(clsdbo_Agent.Remarks);

                if (clsdbo_Agent.Grade_Effective_Date.HasValue)
                    txtGrade_Effective_Date.Text = clsdbo_Agent.Grade_Effective_Date.Value.ToShortDateString();

                if (clsdbo_Agent.Start_Effective_Date.HasValue)
                    txtStart_Effective_Date.Text = clsdbo_Agent.Start_Effective_Date.Value.ToShortDateString();

                if (clsdbo_Agent.Go_out_of_business_Date.HasValue)
                    txtGo_out_of_business_Date.Text = clsdbo_Agent.Go_out_of_business_Date.Value.ToShortDateString();

                if (clsdbo_Agent.First_Order_Date.HasValue)
                    txtFirst_Order_Date.Text = clsdbo_Agent.First_Order_Date.Value.ToShortDateString();

                if (clsdbo_Agent.Go_out_of_business_Date.HasValue)
                    txtGo_out_of_business_Date.Text = clsdbo_Agent.Go_out_of_business_Date.Value.ToShortDateString();

                string status_ = (clsdbo_Agent.Status.Value ? "ดำเนินธุรกิจอยู่" : "ยกเลิกกิจการ");



                if (clsdbo_Agent.Prefix_ID != null)
                {
                    if (ddlPrefix_ID.Items.FindByText(clsdbo_Agent.Prefix_ID) != null)
                        ddlPrefix_ID.Items.FindByText(clsdbo_Agent.Prefix_ID).Selected = true;
                }


                if (clsdbo_Agent.Status.HasValue)
                {
                    if (ddlStatus.Items.FindByText(status_) != null)
                        ddlStatus.Items.FindByText(status_).Selected = true;
                }

                if (!string.IsNullOrEmpty(clsdbo_Agent.Invoice_Region))
                {
                    if (ddlInvoice_Region.Items.FindByText(clsdbo_Agent.Invoice_Region) != null)
                        ddlInvoice_Region.Items.FindByText(clsdbo_Agent.Invoice_Region).Selected = true;
                }

                if (!string.IsNullOrEmpty(clsdbo_Agent.Location_Region))
                {
                    if (ddlLocation_Region.Items.FindByText(clsdbo_Agent.Location_Region) != null)
                        ddlLocation_Region.Items.FindByText(clsdbo_Agent.Location_Region).Selected = true;
                }


                if (!string.IsNullOrEmpty(clsdbo_Agent.SD_ID))
                {
                    if (ddlSD_ID.Items.FindByValue(clsdbo_Agent.SD_ID) != null)
                        ddlSD_ID.Items.FindByValue(clsdbo_Agent.SD_ID).Selected = true;
                }

                if (!string.IsNullOrEmpty(clsdbo_Agent.SM_ID))
                {
                    if (ddlSM_ID.Items.FindByValue(clsdbo_Agent.SM_ID) != null)
                        ddlSM_ID.Items.FindByValue(clsdbo_Agent.SM_ID).Selected = true;
                }

                if (!string.IsNullOrEmpty(clsdbo_Agent.DM_ID))
                {
                    if (ddlDM_ID.Items.FindByValue(clsdbo_Agent.DM_ID) != null)
                        ddlDM_ID.Items.FindByValue(clsdbo_Agent.DM_ID).Selected = true;
                }


                if (!string.IsNullOrEmpty(clsdbo_Agent.APV_ID))
                {
                    if (ddlAVP.Items.FindByValue(clsdbo_Agent.APV_ID) != null)
                        ddlAVP.Items.FindByValue(clsdbo_Agent.APV_ID).Selected = true;
                }


                if (!string.IsNullOrEmpty(clsdbo_Agent.Bank_ID))
                {
                    if (ddlBank.Items.FindByValue(clsdbo_Agent.Bank_ID) != null)
                        ddlBank.Items.FindByValue(clsdbo_Agent.Bank_ID).Selected = true;
                }



                if (!string.IsNullOrEmpty(clsdbo_Agent.GM_ID))
                {
                    if (ddlManager.Items.FindByValue(clsdbo_Agent.GM_ID) != null)
                        ddlManager.Items.FindByValue(clsdbo_Agent.GM_ID).Selected = true;
                }

                if (clsdbo_Agent.Term_of_payment != null)
                {
                    if (ddlTerm_of_payment.Items.FindByText(clsdbo_Agent.Term_of_payment.ToString()) != null)
                        ddlTerm_of_payment.Items.FindByText(clsdbo_Agent.Term_of_payment.ToString()).Selected = true;
                }

                if (clsdbo_Agent.Grade != null)
                {
                    if (ddlGrade.Items.FindByText(clsdbo_Agent.Grade) != null)
                        ddlGrade.Items.FindByText(clsdbo_Agent.Grade).Selected = true;
                }

                if (clsdbo_Agent.Agent_Type_ID != null)
                {
                    if (ddlAgent_Type_ID.Items.FindByText(clsdbo_Agent.Agent_Type_ID) != null)
                        ddlAgent_Type_ID.Items.FindByText(clsdbo_Agent.Agent_Type_ID).Selected = true;
                }

                // ddlAgent_Type_ID.Text = clsdbo_Agent.Agent_Type_ID;


                if (clsdbo_Agent.Invoice_Province != null)
                {
                    if (ddlInvoice_Province.Items.FindByText(clsdbo_Agent.Invoice_Province.Trim()) != null)
                        ddlInvoice_Province.Items.FindByText(clsdbo_Agent.Invoice_Province.Trim()).Selected = true;

                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlInvoice_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                                 .Select(grp => grp.First())
                                 .ToList();

                    dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlInvoice_District.DataSource = tmp_tambol;
                    ddlInvoice_District.DataBind();

                }


                if (clsdbo_Agent.Invoice_District != null)
                {
                    if (ddlInvoice_District.Items.FindByText(clsdbo_Agent.Invoice_District.Trim()) != null)
                        ddlInvoice_District.Items.FindByText(clsdbo_Agent.Invoice_District.Trim()).Selected = true;

                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlInvoice_District.Text, ddlInvoice_Province.Text);
                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                                 .Select(grp => grp.First())
                                 .ToList();
                    dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);

                    ddlInvoice_Sub_district.DataSource = tmp_tambol;
                    ddlInvoice_Sub_district.DataBind();
                }

                if (clsdbo_Agent.Invoice_Sub_district != null)
                {
                    if (ddlInvoice_Sub_district.Items.FindByText(clsdbo_Agent.Invoice_Sub_district.Trim()) != null)
                        ddlInvoice_Sub_district.Items.FindByText(clsdbo_Agent.Invoice_Sub_district.Trim()).Selected = true;
                }

                if (clsdbo_Agent.Location_Province != null)
                {
                    if (ddlLocation_Province.Items.FindByText(clsdbo_Agent.Location_Province.Trim()) != null)
                        ddlLocation_Province.Items.FindByText(clsdbo_Agent.Location_Province.Trim()).Selected = true;

                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlLocation_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                                 .Select(grp => grp.First())
                                 .ToList();

                    dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlLocation_District.DataSource = tmp_tambol;
                    ddlLocation_District.DataBind();

                }


                if (clsdbo_Agent.Location_District != null)
                {

                    if (ddlLocation_District.Items.FindByText(clsdbo_Agent.Location_District.Trim()) != null)
                        ddlLocation_District.Items.FindByText(clsdbo_Agent.Location_District.Trim()).Selected = true;


                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlLocation_District.Text, ddlLocation_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                                 .Select(grp => grp.First())
                                 .ToList();

                    dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlLocation_Sub_district.DataSource = tmp_tambol;
                    ddlLocation_Sub_district.DataBind();
                }

                if (clsdbo_Agent.Location_Sub_district != null)
                {

                    if (ddlLocation_Sub_district.Items.FindByText(clsdbo_Agent.Location_Sub_district.Trim()) != null)
                        ddlLocation_Sub_district.Items.FindByText(clsdbo_Agent.Location_Sub_district.Trim()).Selected = true;
                }


                if (!string.IsNullOrEmpty(clsdbo_Agent.Product_Group_ID))
                {


                    foreach (string product in clsdbo_Agent.Product_Group_ID.Split(',').ToList())
                    {
                        if (product.Trim().Contains("นมสดพาสเจอร์ไรส์"))
                        {
                            chkTab1.Checked = true;
                        }
                        else if (product.Trim().Contains("นมเปรี้ยวไพเกน"))
                        {
                            chkTab4.Checked = true;
                        }
                        else if (product.Trim().Contains("นมเปรี้ยว"))
                        {
                            chkTab2.Checked = true;
                        }
                        else if (product.Trim().Contains("โยเกิร์ตเมจิ"))
                        {
                            chkTab3.Checked = true;
                        }
                        else if (product.Trim().Contains("อื่นๆ"))
                        {
                            chkTab5.Checked = true;
                        }

                    }

                }



                if (!string.IsNullOrEmpty(clsdbo_Agent.Applied_Document))
                {


                    string doc = clsdbo_Agent.Applied_Document;
                    string[] appli = doc.Split(',');

                    foreach (string in_doc in appli)
                    {
                        if (in_doc.Trim().Contains("ใบสมัคร"))
                        {
                            chk01.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("รูปถ่าย"))
                        {
                            chk02.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("ทะเบียนสมรส"))
                        {
                            chk03.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("สำเนาทะเบียนบ้าน"))
                        {
                            chk04.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("ใบเปลี่ยนชื่อ"))
                        {
                            chk05.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("สำเนาบัตรประชาชน"))
                        {
                            chk06.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("หนังสือรับรองบริษัท"))
                        {
                            chk07.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("สำเนาผู้เสียภาษี"))
                        {
                            chk08.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("ใบทะเบียนพาณิชย์"))
                        {
                            chk09.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("ภพ.20"))
                        {
                            chk10.Checked = true;
                        }
                        else if (in_doc.Trim().Contains("อื่นๆ"))
                        {
                            chk11.Checked = true;
                        }
                    }
                }
            }

            txt_CV_Code.Enabled = false;

            bool enable = Mode != "View";


            txt_First_Name.Enabled = enable;
            txtLast_Name.Enabled = enable;
            txtMobile.Enabled = enable;
            txtHome_Phone_No.Enabled = enable;
            txtTax_ID.Enabled = enable;
            txtEmail.Enabled = enable;
            txtFax.Enabled = enable;
            txtConcession_Area.Enabled = enable;
            txtOwner_First_Name.Enabled = enable;
            txtOwner_Last_Name.Enabled = enable;
            txtOwner_Phone_No1.Enabled = enable;
            txtOwner_Phone_No2.Enabled = enable;
            txtContact_First_Name.Enabled = enable;
            txtContact_Last_Name.Enabled = enable;
            txtContact_Phone_No1.Enabled = enable;
            txtContact_Phone_No2.Enabled = enable;
            txtLocation_House_No.Enabled = enable;
            txtLocation_Village.Enabled = enable;
            txtLocation_Village_No.Enabled = enable;
            txtLocation_Alley.Enabled = enable;
            txtLocation_Road.Enabled = enable;
            txtLocation_Postal_ID.Enabled = enable;
            txtInvoice_House_No.Enabled = enable;
            txtInvoice_Village.Enabled = enable;
            txtInvoice_Village_No.Enabled = enable;
            txtInvoice_Alley.Enabled = enable;
            txtInvoice_Road.Enabled = enable;
            txtInvoice_Postal_ID.Enabled = enable;
            txtStart_Effective_Date.Enabled = enable;
            txtFirst_Order_Date.Enabled = enable;
            txtOther_Document.Enabled = enable;
            txtSmall_Case.Enabled = enable;
            txtLarge_Case.Enabled = enable;
            txtPledge_Amount.Enabled = enable;
            txtRoom_Size.Enabled = enable;
            txtCash_Deposit.Enabled = enable;
            txtBank_Guarantee.Enabled = enable;
            txtRemarks.Enabled = enable;
            txtGrade_Effective_Date.Enabled = enable;

            ddlStatus.Enabled = enable;
            ddlAgent_Type_ID.Enabled = enable;
            ddlSD_ID.Enabled = enable;
            ddlSM_ID.Enabled = enable;
            ddlDM_ID.Enabled = enable;
            ddlManager.Enabled = enable;
            ddlGrade.Enabled = enable;
            txtGo_out_of_business_Date.Enabled = enable;

            ddlInvoice_Province.Enabled = enable;
            ddlInvoice_District.Enabled = enable;
            ddlInvoice_Sub_district.Enabled = enable;
            ddlLocation_District.Enabled = enable;
            ddlLocation_Sub_district.Enabled = enable;
            ddlLocation_Province.Enabled = enable;
            ddlInvoice_Region.Enabled = enable;
            ddlLocation_Region.Enabled = enable;
            ddlPrefix_ID.Enabled = enable;
            ddlAVP.Enabled = enable;
            ddlTerm_of_payment.Enabled = enable;
            ddlBank.Enabled = enable;



            chkTab1.Enabled = enable;
            chkTab2.Enabled = enable;
            chkTab3.Enabled = enable;
            chkTab4.Enabled = enable;
            chkTab5.Enabled = enable;

            chk01.Enabled = enable;
            chk02.Enabled = enable;
            chk03.Enabled = enable;
            chk04.Enabled = enable;
            chk05.Enabled = enable;
            chk06.Enabled = enable;
            chk07.Enabled = enable;
            chk08.Enabled = enable;
            chk09.Enabled = enable;
            chk10.Enabled = enable;
            chk11.Enabled = enable;



            //switch (Mode)
            //{
            //    case "":
            //        break;
            //    case "View":
            //        break;
            //    case "Edit":
            //        break;
            //}
            btnCopyAddress.Visible = true;

            if (Mode == "View")
            {
                btnSave.Visible = true;
                btnSave.Text = "แก้ไข";
                ButtonCancelSave.Text = "กลับไปหน้าค้นหา";
                btnSaveMode.Value = "แก้ไข";
                LabelPageHeader.Text = "รายละเอียดข้อมูล Agent";
                btnCopyAddress.Visible = false;
            }
            else if (Mode == "Edit")
            {
                btnSave.Visible = true;
                btnSave.Text = "บันทึก";
                ButtonCancelSave.Text = "ยกเลิก";
                btnSaveMode.Value = "แก้ไข";
                LabelPageHeader.Text = "แก้ไขข้อมูล Agent";
            }
            else if (string.IsNullOrEmpty(Mode))
            {
                txt_CV_Code.Enabled = true;
                btnSave.Visible = true;
                btnSave.Text = "บันทึก";
                ButtonCancelSave.Text = "ยกเลิก";
                btnSaveMode.Value = "บันทึก";
                LabelPageHeader.Text = "สร้างข้อมูล Agent";
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void showPanel(string panelName)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            pnlGrid.Visible = false;
            pnlForm.Visible = false;

            switch (panelName)
            {
                case "pnlGrid":
                    pnlGrid.Visible = true;
                    break;
                case "pnlForm":
                    pnlForm.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }



    }

    private void InsertRecord()
    {

        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);


            //Validate("Validation");


            //if (IsValid)
            //{
            dbo_AgentClass clsdbo_Agent = new dbo_AgentClass();
            bool success = false;
            SetData(clsdbo_Agent);


            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

            success = dbo_AgentDataClass.Add(clsdbo_Agent, User_ID);

            if (success)
            {

                Show("บันทึกสำเร็จ");

            }
            else
            {
                Show("error");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }

        //}
        //else
        //{
        //    Show("กรุณาระบุข้อมูลให้ครบ");

        //}
    }

    private void UpdateRecord()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            dbo_AgentClass oclsdbo_Agent = new dbo_AgentClass();
            // dbo_AgentClass clsdbo_Agent = new dbo_AgentClass();
            oclsdbo_Agent.CV_Code = txt_CV_Code.Text;
            oclsdbo_Agent = dbo_AgentDataClass.Select_Record(oclsdbo_Agent.CV_Code);

            SetData(oclsdbo_Agent);
            bool success = false;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            success = dbo_AgentDataClass.Update(oclsdbo_Agent, User_ID);

            if (success)
            {
                Show("บันทึกสำเร็จ!");
            }
            else
            {
                Show("error");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }


    }

    private void SetData(dbo_AgentClass clsdbo_Agent)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {


            if (ddlTerm_of_payment.SelectedIndex == 0)
            {
                clsdbo_Agent.Term_of_payment = null;
            }
            else
            {
                clsdbo_Agent.Term_of_payment = ddlTerm_of_payment.SelectedValue;
            }


            if (ddlGrade.SelectedIndex == 0)
            {
                clsdbo_Agent.Grade = null;
            }
            else
            {
                clsdbo_Agent.Grade = ddlGrade.SelectedValue;
            }


            if (ddlLocation_Region.SelectedIndex == 0)
            {
                clsdbo_Agent.Location_Region = null;
            }
            else
            {
                clsdbo_Agent.Location_Region = ddlLocation_Region.SelectedValue;
            }


            if (ddlInvoice_Region.SelectedIndex == 0)
            {
                clsdbo_Agent.Invoice_Region = null;
            }
            else
            {
                clsdbo_Agent.Invoice_Region = ddlInvoice_Region.SelectedValue;
            }



            if (ddlLocation_Sub_district.SelectedIndex == 0)
            {
                clsdbo_Agent.Location_Sub_district = null;
            }
            else
            {
                clsdbo_Agent.Location_Sub_district = ddlLocation_Sub_district.SelectedValue;
            }
            if (ddlLocation_District.SelectedIndex == 0)
            {
                clsdbo_Agent.Location_District = null;
            }
            else
            {
                clsdbo_Agent.Location_District = ddlLocation_District.SelectedValue;
            }
            if (ddlLocation_Province.SelectedIndex == 0)
            {
                clsdbo_Agent.Location_Province = null;
            }
            else
            {
                clsdbo_Agent.Location_Province = ddlLocation_Province.SelectedValue;
            }
            if (ddlInvoice_Sub_district.SelectedIndex == 0)
            {
                clsdbo_Agent.Invoice_Sub_district = null;
            }
            else
            {
                clsdbo_Agent.Invoice_Sub_district = ddlInvoice_Sub_district.SelectedValue;
            }
            if (ddlInvoice_District.SelectedIndex == 0)
            {
                clsdbo_Agent.Invoice_District = null;
            }
            else
            {
                clsdbo_Agent.Invoice_District = ddlInvoice_District.SelectedValue;
            }
            if (ddlInvoice_Province.SelectedIndex == 0)
            {
                clsdbo_Agent.Invoice_Province = null;
            }
            else
            {
                clsdbo_Agent.Invoice_Province = ddlInvoice_Province.SelectedValue;
            }

            if (string.IsNullOrEmpty(txt_CV_Code.Text))
            {
                clsdbo_Agent.CV_Code = null;
            }
            else
            {
                clsdbo_Agent.CV_Code = txt_CV_Code.Text;
            }

            if (string.IsNullOrEmpty(txt_First_Name.Text))
            {
                clsdbo_Agent.First_Name = null;
            }
            else
            {
                clsdbo_Agent.First_Name = txt_First_Name.Text;
            }
            if (string.IsNullOrEmpty(txtLast_Name.Text))
            {
                clsdbo_Agent.Last_Name = null;
            }
            else
            {
                clsdbo_Agent.Last_Name = txtLast_Name.Text;
            }


            if (ddlAgent_Type_ID.SelectedIndex == 0)
            {
                clsdbo_Agent.Agent_Type_ID = null;
            }
            else
            {
                clsdbo_Agent.Agent_Type_ID = ddlAgent_Type_ID.SelectedValue;
            }



            if (string.IsNullOrEmpty(txtMobile.Text))
            {
                clsdbo_Agent.Mobile = null;
            }
            else
            {
                clsdbo_Agent.Mobile = txtMobile.Text;
            }
            if (string.IsNullOrEmpty(txtTax_ID.Text))
            {
                clsdbo_Agent.Tax_ID = null;
            }
            else
            {
                clsdbo_Agent.Tax_ID = txtTax_ID.Text;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                clsdbo_Agent.Email = null;
            }
            else
            {
                clsdbo_Agent.Email = txtEmail.Text;
            }
            if (string.IsNullOrEmpty(txtFax.Text))
            {
                clsdbo_Agent.Fax = null;
            }
            else
            {
                clsdbo_Agent.Fax = txtFax.Text;
            }
            if (string.IsNullOrEmpty(txtConcession_Area.Text))
            {
                clsdbo_Agent.Concession_Area = null;
            }
            else
            {
                clsdbo_Agent.Concession_Area = txtConcession_Area.Text;
            }
            if (string.IsNullOrEmpty(txtOwner_First_Name.Text))
            {
                clsdbo_Agent.Owner_First_Name = null;
            }
            else
            {
                clsdbo_Agent.Owner_First_Name = txtOwner_First_Name.Text;
            }
            if (string.IsNullOrEmpty(txtOwner_Last_Name.Text))
            {
                clsdbo_Agent.Owner_Last_Name = null;
            }
            else
            {
                clsdbo_Agent.Owner_Last_Name = txtOwner_Last_Name.Text;
            }
            if (string.IsNullOrEmpty(txtOwner_Phone_No1.Text))
            {
                clsdbo_Agent.Owner_Phone_No1 = null;
            }
            else
            {
                clsdbo_Agent.Owner_Phone_No1 = txtOwner_Phone_No1.Text;
            }
            if (string.IsNullOrEmpty(txtOwner_Phone_No2.Text))
            {
                clsdbo_Agent.Owner_Phone_No2 = null;
            }
            else
            {
                clsdbo_Agent.Owner_Phone_No2 = txtOwner_Phone_No2.Text;
            }
            if (string.IsNullOrEmpty(txtContact_First_Name.Text))
            {
                clsdbo_Agent.Contact_First_Name = null;
            }
            else
            {
                clsdbo_Agent.Contact_First_Name = txtContact_First_Name.Text;
            }
            if (string.IsNullOrEmpty(txtContact_Last_Name.Text))
            {
                clsdbo_Agent.Contact_Last_Name = null;
            }
            else
            {
                clsdbo_Agent.Contact_Last_Name = txtContact_Last_Name.Text;
            }
            if (string.IsNullOrEmpty(txtContact_Phone_No1.Text))
            {
                clsdbo_Agent.Contact_Phone_No1 = null;
            }
            else
            {
                clsdbo_Agent.Contact_Phone_No1 = txtContact_Phone_No1.Text;
            }
            if (string.IsNullOrEmpty(txtContact_Phone_No2.Text))
            {
                clsdbo_Agent.Contact_Phone_No2 = null;
            }
            else
            {
                clsdbo_Agent.Contact_Phone_No2 = txtContact_Phone_No2.Text;
            }


            if (string.IsNullOrEmpty(txtLocation_House_No.Text))
            {
                clsdbo_Agent.Location_House_No = null;
            }
            else
            {
                clsdbo_Agent.Location_House_No = txtLocation_House_No.Text;
            }
            if (string.IsNullOrEmpty(txtLocation_Village.Text))
            {
                clsdbo_Agent.Location_Village = null;
            }
            else
            {
                clsdbo_Agent.Location_Village = txtLocation_Village.Text;
            }
            if (string.IsNullOrEmpty(txtLocation_Village_No.Text))
            {
                clsdbo_Agent.Location_Village_No = null;
            }
            else
            {
                clsdbo_Agent.Location_Village_No = txtLocation_Village_No.Text;
            }
            if (string.IsNullOrEmpty(txtLocation_Alley.Text))
            {
                clsdbo_Agent.Location_Alley = null;
            }
            else
            {
                clsdbo_Agent.Location_Alley = txtLocation_Alley.Text;
            }
            if (string.IsNullOrEmpty(txtLocation_Road.Text))
            {
                clsdbo_Agent.Location_Road = null;
            }
            else
            {
                clsdbo_Agent.Location_Road = txtLocation_Road.Text;
            }

            if (string.IsNullOrEmpty(txtLocation_Postal_ID.Text))
            {
                clsdbo_Agent.Location_Postal_ID = null;
            }
            else
            {
                clsdbo_Agent.Location_Postal_ID = txtLocation_Postal_ID.Text;
            }
            if (string.IsNullOrEmpty(txtInvoice_House_No.Text))
            {
                clsdbo_Agent.Invoice_House_No = null;
            }
            else
            {
                clsdbo_Agent.Invoice_House_No = txtInvoice_House_No.Text;
            }
            if (string.IsNullOrEmpty(txtInvoice_Village.Text))
            {
                clsdbo_Agent.Invoice_Village = null;
            }
            else
            {
                clsdbo_Agent.Invoice_Village = txtInvoice_Village.Text;
            }
            if (string.IsNullOrEmpty(txtInvoice_Village_No.Text))
            {
                clsdbo_Agent.Invoice_Village_No = null;
            }
            else
            {
                clsdbo_Agent.Invoice_Village_No = txtInvoice_Village_No.Text;
            }
            if (string.IsNullOrEmpty(txtInvoice_Alley.Text))
            {
                clsdbo_Agent.Invoice_Alley = null;
            }
            else
            {
                clsdbo_Agent.Invoice_Alley = txtInvoice_Alley.Text;
            }
            if (string.IsNullOrEmpty(txtInvoice_Road.Text))
            {
                clsdbo_Agent.Invoice_Road = null;
            }
            else
            {
                clsdbo_Agent.Invoice_Road = txtInvoice_Road.Text;
            }

            if (string.IsNullOrEmpty(txtInvoice_Postal_ID.Text))
            {
                clsdbo_Agent.Invoice_Postal_ID = null;
            }
            else
            {
                clsdbo_Agent.Invoice_Postal_ID = txtInvoice_Postal_ID.Text;
            }
            if (string.IsNullOrEmpty(txtStart_Effective_Date.Text))
            {
                clsdbo_Agent.Start_Effective_Date = null;
            }
            else
            {
                clsdbo_Agent.Start_Effective_Date = DateTime.Parse(txtStart_Effective_Date.Text);
            }
            if (string.IsNullOrEmpty(txtFirst_Order_Date.Text))
            {
                clsdbo_Agent.First_Order_Date = null;
            }
            else
            {
                clsdbo_Agent.First_Order_Date = DateTime.Parse(txtFirst_Order_Date.Text);
            }


            if (string.IsNullOrEmpty(txtGo_out_of_business_Date.Text))
            {
                clsdbo_Agent.Go_out_of_business_Date = null;
            }
            else
            {
                clsdbo_Agent.Go_out_of_business_Date = DateTime.Parse(txtGo_out_of_business_Date.Text);
            }

            if (string.IsNullOrEmpty(txtOther_Document.Text))
            {
                clsdbo_Agent.Other_Document = null;
            }
            else
            {
                clsdbo_Agent.Other_Document = txtOther_Document.Text;
            }
            if (string.IsNullOrEmpty(txtSmall_Case.Text))
            {
                clsdbo_Agent.Small_Case = null;
            }
            else
            {
                clsdbo_Agent.Small_Case = System.Convert.ToInt16(txtSmall_Case.Text.Replace(",", String.Empty));
            }
            if (string.IsNullOrEmpty(txtLarge_Case.Text))
            {
                clsdbo_Agent.Large_Case = null;
            }
            else
            {
                clsdbo_Agent.Large_Case = System.Convert.ToInt16(txtLarge_Case.Text.Replace(",", String.Empty));
            }
            if (string.IsNullOrEmpty(txtPledge_Amount.Text))
            {
                clsdbo_Agent.Pledge_Amount = null;
            }
            else
            {
                clsdbo_Agent.Pledge_Amount = System.Convert.ToDecimal(txtPledge_Amount.Text.Replace(",", String.Empty));
            }
            if (string.IsNullOrEmpty(txtRoom_Size.Text))
            {
                clsdbo_Agent.Room_Size = null;
            }
            else
            {
                clsdbo_Agent.Room_Size = System.Convert.ToInt16(txtRoom_Size.Text.Replace(",", String.Empty));
            }
            if (string.IsNullOrEmpty(txtCash_Deposit.Text))
            {
                clsdbo_Agent.Cash_Deposit = null;
            }
            else
            {
                clsdbo_Agent.Cash_Deposit = System.Convert.ToDecimal(txtCash_Deposit.Text.Replace(",", String.Empty));
            }
            if (string.IsNullOrEmpty(txtBank_Guarantee.Text))
            {
                clsdbo_Agent.Bank_Guarantee = null;
            }
            else
            {
                clsdbo_Agent.Bank_Guarantee = System.Convert.ToDecimal(txtBank_Guarantee.Text.Replace(",", String.Empty));
            }

            if (string.IsNullOrEmpty(txtRemarks.Text))
            {
                clsdbo_Agent.Remarks = null;
            }
            else
            {
                clsdbo_Agent.Remarks = txtRemarks.Text;
            }


            if (ddlBank.SelectedIndex == 0)
            {
                clsdbo_Agent.Bank_ID = null;
            }
            else
            {
                clsdbo_Agent.Bank_ID = ddlBank.SelectedValue;
            }

            if (string.IsNullOrEmpty(txtGrade_Effective_Date.Text))
            {
                clsdbo_Agent.Grade_Effective_Date = null;
            }
            else
            {
                clsdbo_Agent.Grade_Effective_Date = DateTime.Parse(txtGrade_Effective_Date.Text);
            }


            List<string> listofCheckbox = new List<string>();

            if (chk01.Checked)
            {
                listofCheckbox.Add("ใบสมัคร");
            }
            if (chk02.Checked)
            {
                listofCheckbox.Add("รูปถ่าย");
            }
            if (chk03.Checked)
            {
                listofCheckbox.Add("ทะเบียนสมรส");
            }
            if (chk04.Checked)
            {
                listofCheckbox.Add("สำเนาทะเบียนบ้าน");
            }
            if (chk05.Checked)
            {
                listofCheckbox.Add("ใบเปลี่ยนชื่อ");
            }
            if (chk06.Checked)
            {
                listofCheckbox.Add("สำเนาบัตรประชาชน");
            }
            if (chk07.Checked)
            {
                listofCheckbox.Add("หนังสือรับรองบริษัท");
            }
            if (chk08.Checked)
            {
                listofCheckbox.Add("สำเนาผู้เสียภาษี");
            }
            if (chk09.Checked)
            {
                listofCheckbox.Add("ใบทะเบียนพาณิชย์");
            }
            if (chk10.Checked)
            {
                listofCheckbox.Add("ภพ.20");
            }
            if (chk11.Checked)
            {
                string otherdoc = txtOther_Document.Text;
                listofCheckbox.Add(string.Format("อื่นๆ({0})", otherdoc));
            }

            string doc = string.Empty;
            bool flag = false;
            foreach (string str in listofCheckbox)
            {
                if (listofCheckbox.Count == 1)
                {
                    doc = str;
                }
                else
                {
                    if (!flag)
                    {
                        doc = str;
                        flag = true;
                    }
                    else
                    {
                        doc += "," + str;
                    }
                }
            }
            clsdbo_Agent.Applied_Document = doc;

            if (ddlGrade.SelectedIndex == 0)
            {
                clsdbo_Agent.Grade = null;
            }
            else
            {
                clsdbo_Agent.Grade = ddlGrade.SelectedValue;
            }

            if (ddlSD_ID.SelectedIndex == 0)
            {
                clsdbo_Agent.SD_ID = null;
            }
            else
            {
                clsdbo_Agent.SD_ID = ddlSD_ID.SelectedValue;
            }

            if (ddlSM_ID.SelectedIndex == 0)
            {
                clsdbo_Agent.SM_ID = null;
            }
            else
            {
                clsdbo_Agent.SM_ID = ddlSM_ID.SelectedValue;
            }

            if (ddlDM_ID.SelectedIndex == 0)
            {
                clsdbo_Agent.DM_ID = null;
            }
            else
            {
                clsdbo_Agent.DM_ID = ddlDM_ID.SelectedValue;
            }

            if (ddlManager.SelectedIndex == 0)
            {
                clsdbo_Agent.GM_ID = null;
            }
            else
            {
                clsdbo_Agent.GM_ID = ddlManager.SelectedValue;
            }


            if (ddlManager.SelectedIndex == 0)
            {
                clsdbo_Agent.GM_ID = null;
            }
            else
            {
                clsdbo_Agent.GM_ID = ddlManager.SelectedValue;
            }

            if (ddlAVP.SelectedIndex == 0)
            {
                clsdbo_Agent.APV_ID = null;
            }
            else
            {
                clsdbo_Agent.APV_ID = ddlAVP.SelectedValue;
            }



            clsdbo_Agent.Status = (ddlStatus.Text == "ดำเนินธุรกิจอยู่" ? true : false);


            if (ddlPrefix_ID.SelectedIndex == 0)
            {
                clsdbo_Agent.Prefix_ID = null;
            }
            else
            {
                clsdbo_Agent.Prefix_ID = ddlPrefix_ID.SelectedValue;
            }


            if (string.IsNullOrEmpty(txtHome_Phone_No.Text))
            {
                clsdbo_Agent.Home_Phone_No = null;
            }
            else
            {
                clsdbo_Agent.Home_Phone_No = txtHome_Phone_No.Text;
            }

            List<string> listofproduct = new List<string>();

            if (chkTab1.Checked)
            {
                listofproduct.Add("นมสดพาสเจอร์ไรส์");
            }
            if (chkTab2.Checked)
            {
                listofproduct.Add("นมเปรี้ยว");
            }
            if (chkTab3.Checked)
            {
                listofproduct.Add("โยเกิร์ตเมจิ");
            }
            if (chkTab4.Checked)
            {
                listofproduct.Add("นมเปรี้ยวไพเกน");
            }
            if (chkTab5.Checked)
            {
                listofproduct.Add("อื่นๆ");
            }


            string product = string.Empty;
            bool flag_product = false;


            foreach (string str in listofproduct)
            {
                if (listofproduct.Count == 1)
                {
                    product = str;
                }
                else
                {
                    if (!flag_product)
                    {
                        product = str;
                        flag_product = true;
                    }
                    else
                    {
                        product += "," + str;
                    }
                }
            }
            clsdbo_Agent.Product_Group_ID = product;





        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            Show(ex.Message);
        }

    }

    public void Show(string message)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

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
    #endregion

    #region GridView Row Command
    protected void grdAgent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            switch (e.CommandName)
            {
                case "View":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string CV_Code = lnkView.CommandArgument;
                    GetDetailsDataToForm(CV_Code, "View");

                    break;
                case "_Delete":

                    LinkButton lnk_delete = (LinkButton)e.CommandSource;
                    string CV_Codedelete = lnk_delete.CommandArgument;

                    List<dbo_UserClass> user = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, CV_Codedelete, null, string.Empty, string.Empty);

                    bool success = dbo_AgentDataClass.Delete(CV_Codedelete);
                    if (success)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                        Show("ลบข้อมูลสำเร็จ");

                        SetUpDrowDownList();
                        btnSearchSubmit_Click(sender,e);                       
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdAgent.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        grdAgent.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void grdAgent_DataBound(object sender, EventArgs e)
    {
        GridViewRow pagerRow = grdAgent.BottomPagerRow;

        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {
            for (int i = 0; i < grdAgent.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                if (i == grdAgent.PageIndex)
                {
                    item.Selected = true;
                }
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = grdAgent.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + grdAgent.PageCount.ToString();

        }
    }

    protected void grdAgent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            Label lbl_Status = e.Row.FindControl("lbl_Status") as Label;

            if (lbl_Status.Text != "")
            {
                if (lbl_Status.Text == "True")
                {
                    lbl_Status.Text = "ดำเนินธุรกิจอยู่";
                }
                else
                {
                    lbl_Status.Text = "ยกเลิกกิจการ";
                }
            }
        }
    }
    #endregion

    
    protected void txtSearchCV_CODE_TextChanged(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            if (txtSearchCV_CODE.Text != null)
            {
                if (ddlSearchPrefix_ID.Items.FindByValue(txtSearchCV_CODE.Text.Trim()) != null)
                {
                    //ddlSearchPrefix_ID.Items.FindByValue(txtSearchCV_CODE.Text.Trim()).Selected = true;
                    ddlSearchPrefix_ID.SelectedValue = txtSearchCV_CODE.Text;
                }
                else
                {
                    ddlSearchPrefix_ID.SelectedIndex = 0;
                }
            }

        }
        catch(Exception ex)
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        
    }

  
}