#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_CreateAgentUser : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(typeof(Views_CreateAgentUser));
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            //ddlSearchStatus.SelectedIndex = 1;
            if (!IsPostBack)
            {
                
                ddlSearchAgentName.Attributes.Add("onchange", "myApp.showPleaseWait();");
                txtSearchCVCode.Attributes.Add("onchange", "myApp.showPleaseWait();");

                txtCV_Code.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlPosition.Attributes.Add("onchange", "myApp.showPleaseWait();");
                txtID_Card_No.Attributes.Add("onchange", "myApp.showPleaseWait();");

                ddlHome_Sub_district.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlHome_District.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlHome_Province.Attributes.Add("onchange", "myApp.showPleaseWait();");

                ddlShipment_Sub_district.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlShipment_Sub_district.Attributes.Add("onchange", "myApp.showPleaseWait();");
                ddlShipment_Province.Attributes.Add("onchange", "myApp.showPleaseWait();");

                //GridViewUser.Attributes.Add("style", "word-break:break-all; word-wrap:break-word"); 

                SetEmptyUserGrid();
                SetUpDrowDownList();

                showPanel("pnlGrid");

                ShowStep1();

                btnSearchSubmit_Click(sender, e);
            }
            else
            {
                if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
                {
                    txtPassword.Attributes["value"] = txtPassword.Text;
                }
            }
            
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void btnSave_Click(object sender, System.EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            if (btnSave.Text == "แก้ไข")
            {
                GetDetailsDataToForm(txtUser_ID.Text, "Edit");

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    txtPassword.Text = hdfPasswrod.Value;
                }
                Validate("AgentValidation");

                if (IsValid)
                {

                    //string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    dbo_UserClass user_class1 = dbo_UserDataClass.Select_Record(hdfUser_ID.Value);

                    Regex r = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])");
                    string Pass_Old = user_class1.Password.ToString();
                    List<dbo_PasswordHistoryClass> item = dbo_PasswordHistoryDataClass.Search(txtUser_Name.Text);
                    //List<dbo_PasswordHistoryClass> pass = item.OrderByDescending(f => f.Last_Password_Change_Or_Reset).Where(f => f.Password == txtPassword.Text).ToList();
                    dbo_PasswordHistoryClass pass = item.OrderByDescending(f => f.Last_Password_Change_Or_Reset).Take(3).FirstOrDefault(f => f.Password == txtPassword.Text);


                    string cntUsername = CheckUsername.Check_Username(txtUser_Name.Text, txtUser_ID.Text);
                    string cntUserID = CheckUsername.Check_UserID(txtUser_ID.Text);

                    bool flag = false;

                    if (Pass_Old.ToString() != txtPassword.Text)
                    {
                        if (pass != null)
                        {
                            flag = true;
                            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            //Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                        }
                    }
                    //else
                    //{
                    //    if (Pass_Old.ToString() != txtPassword.Text)
                    //    {
                    //        if (pass != null)
                    //        {
                    //            flag = true;
                    //            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //            //Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                    //        }
                    //    }
                    //}

                        if (txtPassword.Text.Length < 8)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสผ่านควรมีอย่างน้อย 8 ตัวอักษร");
                    }
                    else if (!r.IsMatch(txtPassword.Text))
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสผ่านควรประกอบด้วยตัวเลขและตัวอักษร");
                    }
                    //else if (pass != null && txtPassword.Text != hdfPasswrod.Value && hdfPasswrod.Value != "") //else if (pass.Count >= 3)
                    //{
                    //    System.Threading.Thread.Sleep(500);
                    //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //    Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                    //}
                    else if (flag == true)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                        flag = false;
                    }
                    else if (cntUserID != "0" && btnSaveMode.Value == "บันทึก")
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสพนักงานไม่สามารถซ้ำได้");
                    }
                    else if (cntUsername != "0")
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("ชื่อผู้ใช้งานไม่สามารถซ้ำได้");
                    }
                    else if (txtID_Card_No.Text.Count(char.IsDigit) != 13)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("หมายเลขบัตรประชาชนต้องประกอบด้วยตัวเลข 13 หลัก");
                    }
                    else
                    {
                        if (btnSaveMode.Value == "บันทึก")
                        {
                            List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);
                            dbo_UserClass id_card = users.Where(f => f.Status == "Active").FirstOrDefault(f => f.ID_Card_No == txtID_Card_No.Text);

                            if (id_card != null)
                            {
                                System.Threading.Thread.Sleep(500);
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                                Show(string.Format("เลขบัตรประชาชนซ้ำกับเลขบัตรประชาชนของพนักงาน {0}:สังกัด {1}", id_card.FullName, id_card.AgentName));
                            }
                            else
                            {
                                InsertRecord();

                                if (hdfPasswrod.Value != txtPassword.Text)
                                {
                                    dbo_PasswordHistoryClass password = new dbo_PasswordHistoryClass();
                                    password.Last_Password_Change_Or_Reset = DateTime.Now;
                                    password.Password = txtPassword.Text;
                                    password.User_ID = txtUser_Name.Text;

                                    dbo_PasswordHistoryDataClass.Add(password);
                                }

                                showPanel("pnlGrid");
                                SearchSubmit();

                                System.Threading.Thread.Sleep(500);
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "postBackByObject()", true);
                            }
                        }
                        else
                        {
                            List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);
                            dbo_UserClass id_card = users.Where(f => f.Status == "Active").FirstOrDefault(f => f.ID_Card_No == txtID_Card_No.Text && f.User_ID!=txtUser_ID.Text);

                            if (id_card != null )
                            {
                                System.Threading.Thread.Sleep(500);
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                                Show(string.Format("เลขบัตรประชาชนซ้ำกับเลขบัตรประชาชนของพนักงาน {0}:สังกัด {1}", id_card.FullName, id_card.AgentName));
                                txtID_Card_No.Text = string.Empty;
                            }
                            else
                            {
                                UpdateRecord();
                                if (hdfPasswrod.Value != txtPassword.Text)
                                {
                                    dbo_PasswordHistoryClass password = new dbo_PasswordHistoryClass();
                                    password.Last_Password_Change_Or_Reset = DateTime.Now;
                                    password.Password = txtPassword.Text;
                                    password.User_ID = txtUser_Name.Text;
                                    dbo_PasswordHistoryDataClass.Add(password);
                                }

                                showPanel("pnlGrid");
                                SearchSubmit();
                            }
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        }
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void btnCancel_Click(object sender, System.EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            showPanel("pnlGrid");

            SearchSubmit();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void btnAddNew_Click(object sender, System.EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            txtPassword.Attributes.Add("value", string.Empty);
            GetDetailsDataToForm(string.Empty, string.Empty);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void btnSearchSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            SearchSubmit();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void btnSearchCancel_Click(object sender, System.EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            SetEmptyUserGrid();
            SetUpDrowDownList();
            //ddlSearchStatus.SelectedIndex = 1;
            txtSearchFirstName.Text = string.Empty;
            txtSearchUser_ID.Text = string.Empty;

            GridViewUser.Visible = false;
            pnlNoRec.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnSaveAndNext_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            if (btnSave.Text == "แก้ไข")
            {
                // GetDetailsDataToForm(txtUser_ID.Text, "Edit");
            }
            else
            {
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    txtPassword.Text = hdfPasswrod.Value;
                }
                Validate("AgentValidation");

                if (IsValid)
                {

                    //string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(hdfUser_ID.Value);

                    Regex r = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])");
                    string Pass_Old = user_class.Password.ToString();
                    List<dbo_PasswordHistoryClass> item = dbo_PasswordHistoryDataClass.Search(txtUser_Name.Text);
                    //List<dbo_PasswordHistoryClass> pass = item.OrderByDescending(f => f.Last_Password_Change_Or_Reset).Where(f => f.Password == txtPassword.Text).ToList();
                    dbo_PasswordHistoryClass pass = item.OrderByDescending(f => f.Last_Password_Change_Or_Reset).Take(3).FirstOrDefault(f => f.Password == txtPassword.Text);
                    bool flag = false;
                    string cntUsername = CheckUsername.Check_Username(txtUser_Name.Text, txtUser_ID.Text);
                    string cntUserID = CheckUsername.Check_UserID(txtUser_ID.Text);
                    List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);


                    if (Pass_Old.ToString() != txtPassword.Text)
                    {
                        if (pass != null)
                        {
                            flag = true;
                            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            //Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                        }
                    }
                    //else
                    //{
                    //    if (Pass_Old.ToString() != txtPassword.Text)
                    //    {
                    //        if (pass != null)
                    //        {
                    //            flag = true;
                    //            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //            //Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                    //        }
                    //    }
                    //}



                    if (txtPassword.Text.Length < 8)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสผ่านควรมีอย่างน้อย 8 ตัวอักษร");
                    }
                    else if (!r.IsMatch(txtPassword.Text))
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสผ่านควรประกอบด้วยตัวเลขและตัวอักษร");
                    }
                    //else if (pass != null && txtPassword.Text != hdfPasswrod.Value && hdfPasswrod.Value != "") //else if (pass.Count >= 3)
                    //{
                    //    System.Threading.Thread.Sleep(500);
                    //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //    Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                    //}
                    else if (flag == true)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                        flag = false;
                    }
                    else if (cntUserID != "0" && btnSaveMode.Value == "บันทึก")
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสพนักงานไม่สามารถซ้ำได้");
                    }
                    else if (cntUsername != "0")
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("ชื่อผู้ใช้งานไม่สามารถซ้ำได้");
                    }
                    else if (txtID_Card_No.Text.Count(char.IsDigit) != 13)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("หมายเลขบัตรประชาชนต้องประกอบด้วยตัวเลข 13 หลัก");
                    }
                    else
                    {
                        if (btnSaveMode.Value == "บันทึก")
                        {
                            //dbo_UserClass id_card = users.FirstOrDefault(f => f.ID_Card_No == txtID_Card_No.Text);
                            dbo_UserClass id_card = users.Where(f => f.Status == "Active").FirstOrDefault(f => f.ID_Card_No == txtID_Card_No.Text);

                            //if (id_card != null && id_card.CV_CODE == txtCV_Code.Text)
                            if (id_card != null)
                            {
                                System.Threading.Thread.Sleep(500);
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                                Show(string.Format("เลขบัตรประชาชนซ้ำกับเลขบัตรประชาชนของพนักงาน {0}:สังกัด {1}", id_card.FullName, id_card.AgentName));
                            }
                            else
                            {
                                InsertRecord();

                                if (hdfPasswrod.Value != txtPassword.Text)
                                {
                                    dbo_PasswordHistoryClass password = new dbo_PasswordHistoryClass();
                                    password.Last_Password_Change_Or_Reset = DateTime.Now;
                                    password.Password = txtPassword.Text;
                                    password.User_ID = txtUser_Name.Text;

                                    dbo_PasswordHistoryDataClass.Add(password);
                                }
                                ShowStep2();
                                showPanel("pnlInstallation");
                                InitialInstallation();
                            }

                        }
                        else
                        {
                            dbo_UserClass id_card = users.Where(f => f.Status == "Active").FirstOrDefault(f => f.ID_Card_No == txtID_Card_No.Text && f.User_ID != txtUser_ID.Text);

                            if (id_card != null)
                            {
                                System.Threading.Thread.Sleep(500);
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                                Show(string.Format("เลขบัตรประชาชนซ้ำกับเลขบัตรประชาชนของพนักงาน {0}:สังกัด {1}", id_card.FullName, id_card.AgentName));
                                txtID_Card_No.Text = string.Empty;
                            }
                            else
                            {
                                UpdateRecord();
                                if (hdfPasswrod.Value != txtPassword.Text)
                                {
                                    dbo_PasswordHistoryClass password = new dbo_PasswordHistoryClass();
                                    password.Last_Password_Change_Or_Reset = DateTime.Now;
                                    password.Password = txtPassword.Text;
                                    password.User_ID = txtUser_Name.Text;
                                    dbo_PasswordHistoryDataClass.Add(password);
                                }

                                //showPanel("pnlGrid");
                                //SearchSubmit();
                                ShowStep2();
                                showPanel("pnlInstallation");
                                InitialInstallation();

                            }
                        }



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
            GridViewInstallation.ShowFooter = true;
            List<dbo_InstallationClass> item_value = dbo_InstallationDataClass.Search(txtUser_ID.Text);
            GridViewInstallation.DataSource = item_value;
            GridViewInstallation.DataBind();

            if (item_value.Count == 0)
            {
                item_value.Add(new dbo_InstallationClass());
                GridViewInstallation.DataSource = item_value;
                GridViewInstallation.DataBind();

                GridViewInstallation.Rows[0].Visible = false;

            }
            else
            {
                GridViewInstallation.DataSource = item_value;
                GridViewInstallation.DataBind();
            }

            Dictionary<string, string> type = dbo_ItemDataClass.GetDropDown("16");
            DropDownList ddl = (DropDownList)GridViewInstallation.FooterRow.FindControl("ddlNewInstallation_Type");
            ddl.DataSource = type;
            ddl.DataBind();


            TextBox _Transaction_Date = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewTransaction_Date");
            //_Transaction_Date.Text = DateTime.Now.ToString();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnNewBenefit_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            showPanel("pnlBenefit");
            ShowStep3();
            InitialBenefit();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnAddNewBenefit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {

            GridViewBenefit.ShowFooter = true;
            List<dbo_BenefitClass> item = dbo_BenefitDataClass.Search(txtUser_ID.Text);

            if (item.Count == 0)
            {
                item.Add(new dbo_BenefitClass());

                GridViewBenefit.DataSource = item;
                GridViewBenefit.DataBind();
                GridViewBenefit.Rows[0].Visible = false;
            }
            else
            {
                GridViewBenefit.DataSource = item;
                GridViewBenefit.DataBind();
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void btnCopyAddress_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            txtPresent_House_No.Text = txtHome_House_No.Text;
            txtPresent_Village.Text = txtHome_Village.Text;
            txtPresent_Village_No.Text = txtHome_Village_No.Text;
            txtPresent_Alley.Text = txtHome_Alley.Text;
            txtPresent_Road.Text = txtHome_Road.Text;
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

            txtPresent_Postal_ID.Text = txtHome_Postal_ID.Text;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void ddlSearchCVCode_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            txtSearchCVCode.Text = string.Empty;
            List <dbo_UserClass> user = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ddlSearchAgentName.SelectedValue, null, string.Empty, string.Empty);

            dbo_UserClass u = new dbo_UserClass();
            u.First_Name = "==ระบุ==";
            u.User_ID = string.Empty;

            user.Insert(0, u);

            //ddlSearchFirstName.DataSource = user;
            //ddlSearchFirstName.DataBind();


            txtSearchCVCode.Text = ddlSearchAgentName.SelectedValue;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlCV_Code_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            dbo_AgentClass agent = dbo_AgentDataClass.Select_Record(txtCV_Code.Text);

            txtAgent_Name.Text = agent.First_Name;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlHome_Province_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlHome_District_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlShipment_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlShipment_Province_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
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

    protected void ddlShipment_Sub_district_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlShipment_District.Text, ddlShipment_Province.Text);

            txtPresent_Postal_ID.Text = tambol.FirstOrDefault(f => f.Sub_district == ddlShipment_Sub_district.Text).Postal_ID;

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            if (btnSaveMode.Value == "บันทึก")
            {
                if (ddlPosition.SelectedItem.Text == "เจ้าของ")
                {
                    txtUser_Name.Text = txtCV_Code.Text;
                }
                else
                {
                    txtUser_Name.Text = txtUser_ID.Text;
                }
            }
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void txtSearchCVCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #region old
            //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            //List<dbo_UserClass> user = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);

            //dbo_UserClass u = new dbo_UserClass();
            //u.First_Name = "==ระบุ==";
            //u.User_ID = string.Empty;

            //user.Insert(0, u);
            #endregion

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            if (txtSearchCVCode.Text != null)
            {
                if (ddlSearchAgentName.Items.FindByValue(txtSearchCVCode.Text.Trim()) != null)
                {
                    //ddlSearchPrefix_ID.Items.FindByValue(txtSearchCV_CODE.Text.Trim()).Selected = true;
                    ddlSearchAgentName.SelectedValue = txtSearchCVCode.Text;
                }
                else
                {
                    ddlSearchAgentName.SelectedIndex = 0;
                }
            }


            //ddlSearchFirstName.DataSource = user;
            //ddlSearchFirstName.DataBind();
        }
        catch (Exception ex)
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            logger.Error(ex.Message);
        }

    }

    protected void txtCV_Code_TextChanged(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            dbo_AgentClass agent = dbo_AgentDataClass.Select_Record(txtCV_Code.Text);

            if (agent == null)
            {

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("CV Code ไม่ถูกต้อง");
                txtCV_Code.Text = string.Empty;
            }
            else
            {
                txtAgent_Name.Text = agent.AgentName;
                txtUser_ID.Text = GenerateID.UserID_Agent(txtCV_Code.Text);

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void txtNewPaid_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            TextBox _Installation_Amount = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewInstallation_Amount");
            TextBox _Payment_Amount = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewPaid");
            TextBox _Balance_Amount = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewBalance_Amount");


            Decimal ___Installation_Amount = Decimal.Parse(_Installation_Amount.Text.Replace(",", string.Empty));
            Decimal ___Payment_Amount = Decimal.Parse(_Payment_Amount.Text.Replace(",", string.Empty));
            Decimal ___Balance_Amount = Decimal.Parse(_Balance_Amount.Text.Replace(",", string.Empty));

            ___Balance_Amount = ___Installation_Amount - ___Payment_Amount;

            _Balance_Amount.Text = ___Balance_Amount.ToString();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void txtEditPaid_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int RowIndex = Convert.ToInt32(ViewState["NewEditIndex"]);

            TextBox _Installation_Amount = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditInstallation_Amount");
            TextBox _Payment_Amount = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditPaid");
            TextBox _Balance_Amount = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditBalance_Amount");


            Decimal __Installation_Amount = Decimal.Parse(_Installation_Amount.Text.Replace(",", string.Empty));
            Decimal __Payment_Amount = Decimal.Parse(_Payment_Amount.Text.Replace(",", string.Empty));
            Decimal __Balance_Amount = __Installation_Amount - __Payment_Amount; //Decimal.Parse(_Balance_Amount.Text.Replace(",", string.Empty));

            //__Balance_Amount = __Installation_Amount - __Payment_Amount;

            _Balance_Amount.Text = __Balance_Amount.ToString();
        }
        catch (Exception ex)
        {

            logger.Error(ex.Message);
        }
    }

    protected void txtID_Card_No_TextChanged(object sender, EventArgs e)
    {
        List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);
        dbo_UserClass id_card = users.Where(f => f.Status == "Active").FirstOrDefault(f => f.ID_Card_No == txtID_Card_No.Text && f.User_ID != txtUser_ID.Text);

        if (id_card != null)
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show(string.Format("เลขบัตรประชาชนซ้ำกับเลขบัตรประชาชนของพนักงาน {0}:สังกัด {1}", id_card.FullName, id_card.AgentName));
            txtID_Card_No.Text = string.Empty;
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void lblstep1_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblstep2.Attributes["class"] == "btn btn-primary btn-circle active-step" || (lblstep3.Attributes["class"] == "btn btn-primary btn-circle active-step"))
            {
                ShowStep1();
                showPanel("pnlForm");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void lblstep2_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblstep3.Attributes["class"] == "btn btn-primary btn-circle active-step")
            {
                ShowStep2();
                showPanel("pnlInstallation");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        // InitialInstallation();
    }

    #endregion

    #region Method
    private void SetEmptyUserGrid()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            if(GridViewUser.Rows.Count > 0)
            {
                List<dbo_UserClass> dt = new List<dbo_UserClass>();
                GridViewUser.DataSource = dt;
                GridViewUser.DataBind();
            }
          

            txtSearchCVCode.Text = string.Empty;
            ddlSearchAgentName.ClearSelection();

            ddlSearchPosition.ClearSelection();
            ddlSearchRole.ClearSelection();
            txtSearchStartDate.Text = string.Empty;
            // ddlSearchStatus.ClearSelection();
            ddlSearchStatus.SelectedIndex = 1;


            showPanel("pnlGrid");
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }



    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            Dictionary<string, string> positon = dbo_ItemDataClass.GetDropDown("05");

            List<dbo_RoleClass> role = dbo_RoleDataClass.Search(string.Empty, string.Empty, "Agent");
            role.Insert(0, new dbo_RoleClass { Role_ID = string.Empty, Role_Name = "==ระบุ==" });

            ddlSearchRole.DataSource = role;
            ddlSearchRole.DataBind();



            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            ViewState["CV_CODE"] = (user_class.CV_CODE == null ? string.Empty : user_class.CV_CODE);

            dbo_AgentClass cv_ = new dbo_AgentClass();
            cv_.AgentName = "==ระบุ==";
            cv_.CV_Code = string.Empty;

            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ดำเนินธุรกิจอยู่", string.Empty);

            if (user_class.User_Group_ID == "CP Meiji")
            {

                List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                logger.Debug("cv_code1.Count " + cv_code1.Count);


                if (cv_code1.Count != 0)
                {
                    cv_code1.Insert(0, cv_);
                    ddlSearchAgentName.DataSource = cv_code1;
                    ddlSearchAgentName.DataBind();
                    ddlSearchAgentName.ClearSelection();

                }
                else
                {
                    string region = user_class.Region;

                    logger.Debug("user_class.Region " + user_class.Region);


                    string[] regions = region.Split(',');

                    List<dbo_AgentClass> cv_code_ = new List<dbo_AgentClass>();
                    cv_code_.Insert(0, cv_);

                    foreach (string in_region in regions)
                    {
                        List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => f.Location_Region == in_region).Select(f => f));
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            cv_code_.Add(_cv);
                        }

                    }
                    ddlSearchAgentName.DataSource = cv_code_;
                    ddlSearchAgentName.DataBind();
                    ddlSearchAgentName.ClearSelection();
                }

            }
            else
            {
                List<dbo_AgentClass> cv_code = dbo_AgentDataClass.Search(user_class.CV_CODE, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                cv_code.Insert(0, cv_);
                ddlSearchAgentName.DataSource = cv_code;
                ddlSearchAgentName.DataBind();
                ddlSearchAgentName.ClearSelection();

            }



            if (user_class.CV_CODE != null)
            {
                logger.Debug("user_class.CV_CODE " + user_class.CV_CODE);
                ddlSearchAgentName.Items.FindByValue(user_class.CV_CODE).Selected = true;
                ddlSearchCVCode_SelectedIndexChanged(this, null);
            }

            if (string.IsNullOrEmpty(user_class.CV_CODE))
            {
                List<dbo_UserClass> user = new List<dbo_UserClass>();
                dbo_UserClass u = new dbo_UserClass();
                u.First_Name = "==ระบุ==";
                u.User_ID = string.Empty;

                user.Insert(0, u);
                //ddlSearchFirstName.DataSource = user;
                //ddlSearchFirstName.DataBind();
            }
            else
            {
                txtSearchCVCode.Text = user_class.CV_CODE;
                txtSearchCVCode.Enabled = false;
                ddlSearchAgentName.Enabled = false;
            }

            ddlSearchPosition.DataSource = positon;
            ddlSearchPosition.DataBind();


            ddlRole.DataSource = role;
            ddlRole.DataBind();

            Dictionary<string, string> title = dbo_ItemDataClass.GetDropDown("03");
            ddlTitle_ID.DataSource = title;
            ddlTitle_ID.DataBind();

            ddlPosition.DataSource = positon;
            ddlPosition.DataBind();

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

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    private void GetDetailsDataToForm(string id, string Mode)
    {

        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        ShowStep1();
        showPanel("pnlForm");


        ddlHome_Province.ClearSelection();
        ddlHome_District.ClearSelection();
        ddlHome_Sub_district.ClearSelection();
        ddlShipment_Province.ClearSelection();
        ddlShipment_District.ClearSelection();
        ddlShipment_Sub_district.ClearSelection();
        ddlPosition.ClearSelection();
        ddlTitle_ID.ClearSelection();
        ddlRole.ClearSelection();
        ddlPayment_Type.ClearSelection();
        ddlShowDashborad.ClearSelection();
        ddlApprove.ClearSelection();
        ddlStatus.ClearSelection();

        txtCV_Code.Text = txtSearchCVCode.Text;

        //if (!string.IsNullOrEmpty(txtCV_Code.Text) && (txtSearchCVCode.Enabled==true))
        if (txtSearchCVCode.Enabled == false)
        {
            dbo_AgentClass agen = dbo_AgentDataClass.Select_Record(txtCV_Code.Text);

            if (agen == null)
            {
                Show("CV Code ไม่ถูกต้อง");

                txtAgent_Name.Text = string.Empty;
            }
            else
            {
                txtPassword.Text = null;
                txtAgent_Name.Text = agen.AgentName;
                txtUser_ID.Text = GenerateID.UserID_Agent(txtCV_Code.Text);
                //    txtUser_Name.Text = txtUser_ID.Text;
                txtUser_Name.Text = " ";
                txtCV_Code.Enabled = false;
            }
        }
        else
        {
            txtCV_Code.Text = string.Empty;
            txtAgent_Name.Text = string.Empty;
            txtUser_ID.Text = "";
            txtUser_Name.Text = " ";
            // txtUser_Name.Text = "";
        }

        txtPassword.Text = "--";
        txtPassword.Text = null;
        // txtUser_ID.Text = null;

        txtFirst_Name.Text = string.Empty;
        txtLast_Name.Text = string.Empty;
        txtHome_Phone_No.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtBirthdate.Text = string.Empty;
        txtID_Card_No.Text = string.Empty;


        txtEmail.Text = string.Empty;
        txtHome_House_No.Text = string.Empty;
        txtHome_Village.Text = string.Empty;
        txtHome_Village_No.Text = string.Empty;
        txtHome_Alley.Text = string.Empty;
        txtHome_Road.Text = string.Empty;
        txtHome_Postal_ID.Text = string.Empty;
        txtPresent_House_No.Text = string.Empty;
        txtPresent_Village.Text = string.Empty;
        txtPresent_Village_No.Text = string.Empty;
        txtPresent_Alley.Text = string.Empty;
        txtPresent_Road.Text = string.Empty;
        txtPresent_Postal_ID.Text = string.Empty;
        txtJoin_Date.Text = string.Empty;
        txtResign_Date.Text = string.Empty;
        txtCredit_Term.Text = string.Empty;
        txtUser_ID.Enabled = true;

        chkApplied_1.Checked = false;
        chkApplied_2.Checked = false;
        chkApplied_3.Checked = false;
        chkApplied_4.Checked = false;
        hdfPasswrod.Value = string.Empty;


        try
        {


            if (!string.IsNullOrEmpty(id))
            {
                logger.Debug("id " + id);
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(id);
                dbo_AgentClass agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

                txtCV_Code.Text = agent.CV_Code;
                txtAgent_Name.Text = agent.AgentName;
                txtAgent_Name.Enabled = false;
                txtUser_ID.Enabled = false;



                txtUser_ID.Text = user_class.User_ID;
                txtFirst_Name.Text = user_class.First_Name;
                txtLast_Name.Text = user_class.Last_Name;
                txtHome_Phone_No.Text = user_class.Home_Phone_No;
                txtMobile.Text = user_class.Mobile;
                txtID_Card_No.Text = user_class.ID_Card_No;
                txtUser_Name.Text = user_class.Username;



                txtPassword.Text = user_class.Password;
                hdfPasswrod.Value = user_class.Password;

                txtPassword.Attributes.Add("value", user_class.Password);



                txtEmail.Text = user_class.Email;
                txtHome_House_No.Text = user_class.Home_House_No;
                txtHome_Village.Text = user_class.Home_Village;
                txtHome_Village_No.Text = user_class.Home_Village_No;
                txtHome_Alley.Text = user_class.Home_Alley;
                txtHome_Road.Text = user_class.Home_Road;
                txtHome_Postal_ID.Text = user_class.Home_Postal_ID;
                txtPresent_House_No.Text = user_class.Present_House_No;
                txtPresent_Village.Text = user_class.Present_Village;
                txtPresent_Village_No.Text = user_class.Present_Village_No;
                txtPresent_Alley.Text = user_class.Present_Alley;
                txtPresent_Road.Text = user_class.Present_Road;
                txtPresent_Postal_ID.Text = user_class.Present_Postal_ID;
                txtBirthdate.Text = (user_class.Birthdate.HasValue ? user_class.Birthdate.Value.ToShortDateString() : string.Empty);
                txtJoin_Date.Text = (user_class.Join_Date.HasValue ? user_class.Join_Date.Value.ToShortDateString() : string.Empty);
                txtResign_Date.Text = (user_class.Resign_Date.HasValue ? user_class.Resign_Date.Value.ToShortDateString() : string.Empty);


                txtRoute.Text = user_class.Route;
                txtCredit_Term.Text = user_class.Credit_Term.ToString();


                string doc = user_class.Applied_Document;

                string[] appli = doc.Split(',');

                foreach (string in_doc in appli)
                {
                    switch (in_doc)
                    {
                        case "ใบสมัคร":
                            chkApplied_1.Checked = true;
                            break;
                        case "รูปถ่าย":
                            chkApplied_2.Checked = true;
                            break;
                        case "สำเนาทะเบียนบ้าน":
                            chkApplied_3.Checked = true;
                            break;
                        case "สำเนาบัตรประชาชน":
                            chkApplied_4.Checked = true;
                            break;
                    }
                }


                if (user_class.Home_Province != null)
                {

                    if (ddlHome_Province.Items.FindByText(user_class.Home_Province.Trim()) != null)
                        ddlHome_Province.Items.FindByText(user_class.Home_Province.Trim()).Selected = true;


                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlHome_Province.Text);
                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                                 .Select(grp => grp.First())
                                 .ToList();

                    dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlHome_District.DataSource = tmp_tambol;
                    ddlHome_District.DataBind();
                }

                if (user_class.Home_District != null)
                {
                    if (ddlHome_District.Items.FindByText(user_class.Home_District.Trim()) != null)
                        ddlHome_District.Items.FindByText(user_class.Home_District.Trim()).Selected = true;


                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlHome_District.Text, ddlHome_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                                 .Select(grp => grp.First())
                                 .ToList();


                    dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);


                    ddlHome_Sub_district.DataSource = tmp_tambol;
                    ddlHome_Sub_district.DataBind();
                }

                if (user_class.Home_Sub_district != null)
                {

                    if (ddlHome_Sub_district.Items.FindByText(user_class.Home_Sub_district.Trim()) != null)
                        ddlHome_Sub_district.Items.FindByText(user_class.Home_Sub_district.Trim()).Selected = true;

                }



                if (user_class.Present_Province != null)
                {

                    if (ddlShipment_Province.Items.FindByText(user_class.Present_Province.Trim()) != null)
                        ddlShipment_Province.Items.FindByText(user_class.Present_Province.Trim()).Selected = true;

                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search("", ddlShipment_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.District)
                                 .Select(grp => grp.First())
                                 .ToList();


                    dbo_TambolClass first_ = new dbo_TambolClass() { District = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlShipment_District.DataSource = tmp_tambol;
                    ddlShipment_District.DataBind();

                }

                if (user_class.Present_District != null)
                {

                    if (ddlShipment_District.Items.FindByText(user_class.Present_District.Trim()) != null)
                        ddlShipment_District.Items.FindByText(user_class.Present_District.Trim()).Selected = true;


                    List<dbo_TambolClass> tambol = dbo_TambolDataClass.Search(ddlShipment_District.Text, ddlShipment_Province.Text);

                    List<dbo_TambolClass> tmp_tambol = tambol.GroupBy(f => f.Sub_district)
                                 .Select(grp => grp.First())
                                 .ToList();


                    dbo_TambolClass first_ = new dbo_TambolClass() { Sub_district = "==ระบุ==" };
                    tmp_tambol.Insert(0, first_);
                    ddlShipment_Sub_district.DataSource = tmp_tambol;
                    ddlShipment_Sub_district.DataBind();

                }

                if (user_class.Present_Sub_District != null)
                {

                    if (ddlShipment_Sub_district.Items.FindByText(user_class.Present_Sub_District.Trim()) != null)
                        ddlShipment_Sub_district.Items.FindByText(user_class.Present_Sub_District.Trim()).Selected = true;
                }

                if (user_class.Position != null)
                {

                    if (ddlPosition.Items.FindByText(user_class.Position.Trim()) != null)
                        ddlPosition.Items.FindByText(user_class.Position.Trim()).Selected = true;
                }

                if (user_class.Title_ID != null)
                {

                    if (ddlTitle_ID.Items.FindByText(user_class.Title_ID) != null)
                        ddlTitle_ID.Items.FindByText(user_class.Title_ID).Selected = true;
                }

                if (user_class.Role_ID != null)
                {

                    if (ddlRole.Items.FindByValue(user_class.Role_ID) != null)
                        ddlRole.Items.FindByValue(user_class.Role_ID).Selected = true;
                }

                if (user_class.Payment_Type != null)
                {

                    if (ddlPayment_Type.Items.FindByText(user_class.Payment_Type) != null)
                        ddlPayment_Type.Items.FindByText(user_class.Payment_Type).Selected = true;
                }

                if (user_class.ShowDashboard != null)
                {

                    if (ddlShowDashborad.Items.FindByText(user_class.ShowDashboard) != null)
                        ddlShowDashborad.Items.FindByText(user_class.ShowDashboard).Selected = true;
                }

                if (user_class.Approval_Status_ID != null)
                {

                    if (ddlApprove.Items.FindByText(user_class.Approval_Status_ID) != null)
                        ddlApprove.Items.FindByText(user_class.Approval_Status_ID).Selected = true;
                }

                if (user_class.Status != null)
                {

                    if (ddlStatus.Items.FindByText(user_class.Status) != null)
                        ddlStatus.Items.FindByText(user_class.Status).Selected = true;
                }

            }


            bool enable = Mode != "View";

            GridViewDetailInstallation.Visible = false;
            GridViewDetailBenefit.Visible = false;
            hinstallation.Visible = false;
            hbenefit.Visible = false;


            ddlHome_Province.Enabled = enable;
            ddlHome_District.Enabled = enable;
            ddlHome_Sub_district.Enabled = enable;
            ddlShipment_Province.Enabled = enable;
            ddlShipment_District.Enabled = enable;
            ddlShipment_Sub_district.Enabled = enable;
            ddlPosition.Enabled = enable;
            ddlTitle_ID.Enabled = enable;
            ddlRole.Enabled = enable;
            ddlPayment_Type.Enabled = enable;
            ddlShowDashborad.Enabled = enable;
            //ddlApprove.Enabled = enable;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class1 = dbo_UserDataClass.Select_Record(User_ID);
            if (user_class1.User_Group_ID == "CP Meiji")
            {
                ddlApprove.Enabled = enable;
            }
            else
            {
                ddlApprove.Enabled = false;
            }

            ddlStatus.Enabled = enable;

            txtCV_Code.Enabled = (Mode == string.Empty && string.IsNullOrEmpty(txtCV_Code.Text));

            txtUser_ID.Enabled = false;

            txtFirst_Name.Enabled = enable;
            txtLast_Name.Enabled = enable;
            txtHome_Phone_No.Enabled = enable;
            txtMobile.Enabled = enable;
            txtBirthdate.Enabled = enable;
            txtID_Card_No.Enabled = enable;
            txtUser_Name.Enabled = enable;
            txtPassword.Enabled = enable;
            txtEmail.Enabled = enable;
            txtHome_House_No.Enabled = enable;
            txtHome_Village.Enabled = enable;
            txtHome_Village_No.Enabled = enable;
            txtHome_Alley.Enabled = enable;
            txtHome_Road.Enabled = enable;
            ddlHome_Sub_district.Enabled = enable;
            ddlHome_District.Enabled = enable;
            ddlHome_Province.Enabled = enable;
            txtHome_Postal_ID.Enabled = enable;
            txtPresent_House_No.Enabled = enable;
            txtPresent_Village.Enabled = enable;
            txtPresent_Village_No.Enabled = enable;
            txtPresent_Alley.Enabled = enable;
            txtPresent_Road.Enabled = enable;
            ddlShipment_Sub_district.Enabled = enable;
            ddlShipment_District.Enabled = enable;
            ddlShipment_Province.Enabled = enable;
            txtPresent_Postal_ID.Enabled = enable;
            txtJoin_Date.Enabled = enable;
            txtResign_Date.Enabled = enable;
            txtCredit_Term.Enabled = enable;
            txtRoute.Enabled = enable;

            chkApplied_1.Enabled = enable;
            chkApplied_2.Enabled = enable;
            chkApplied_3.Enabled = enable;
            chkApplied_4.Enabled = enable;


            if (Mode == "View")
            {
                List<dbo_InstallationClass> item = dbo_InstallationDataClass.Search(txtUser_ID.Text);
                GridViewDetailInstallation.DataSource = item;
                GridViewDetailInstallation.DataBind();
                GridViewDetailInstallation.Visible = true;

                List<dbo_BenefitClass> itemBenafit = dbo_BenefitDataClass.Search(txtUser_ID.Text);
                GridViewDetailBenefit.DataSource = itemBenafit;
                GridViewDetailBenefit.DataBind();
                GridViewDetailBenefit.Visible = true;


                pnlStep.Visible = false;
                hinstallation.Visible = true;
                hbenefit.Visible = true;

                btnSave.Visible = true;

                btnSave.Text = "แก้ไข";
                btnSaveMode.Value = "บันทึก";

                btnSaveAndNext.Visible = false;
                btnCopyAddress.Visible = false;
                //LabelPageHeader.Text = "รายละเอียดข้อมูล User";
                LabelPageHeader.Text = "รายละเอียดข้อมูลพนักงานเอเยนต์";
            }
            else if (Mode == "Edit")
            {
                btnSave.Visible = true;
                btnSaveAndNext.Visible = true;
                btnSave.Text = "บันทึก";

                btnSaveMode.Value = "แก้ไข";
                btnCopyAddress.Visible = true;
                LabelPageHeader.Text = "แก้ไขข้อมูล User";
            }
            else if (string.IsNullOrEmpty(Mode))
            {
                btnSave.Visible = true;
                btnSaveAndNext.Visible = true;
                btnCopyAddress.Visible = true;
                btnSave.Text = "บันทึก";
                btnSaveMode.Value = "บันทึก";

                LabelPageHeader.Text = "สร้างข้อมูล User";
                // btnSave.Text = "บันทึก";
            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    private void InsertRecord()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            dbo_UserClass clsdbo_User = new dbo_UserClass();
            SetDataAgent(clsdbo_User);

            if (IsValid)
            {
                bool success = false;
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                success = dbo_UserDataClass.Add(clsdbo_User, User_ID);
                if (success)
                {


                    Show("บันทึกข้อมูลสำเร็จ!");

                }

            }
            else
            {
                //            string script = @"swal({
                //                                title: ""กรุณาระบุข้อมูลให้ครบ"",
                //                                text: ""ข้อมูลไม่ครบ"",
                //                                type: ""error"",
                //                                confirmButtonText: ""ตกลง""
                //                            });";
                //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }




    }

    private void UpdateRecord()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            dbo_UserClass oclsdbo_User = new dbo_UserClass();

            oclsdbo_User.User_ID = txtUser_ID.Text;
            oclsdbo_User = dbo_UserDataClass.Select_Record(oclsdbo_User.User_ID);

            SetDataAgent(oclsdbo_User);

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            //if (IsValid)
            //{


            if (oclsdbo_User.Status == "In active")
            {
                List<dbo_DebtClass> debt = dbo_DebtDataClass.Search(oclsdbo_User.User_ID, string.Empty);
                decimal? sum_balance = debt.Sum(f => f.Balance_Outstanding_Amount);

                List<dbo_ClearingClass> clr_list = dbo_ClearingDataClass.Search(string.Empty, null, null, oclsdbo_User.User_ID).OrderByDescending(f => f.Clearing_Date).ToList();

                dbo_ClearingClass clearing = (clr_list.Count == 0 ? null : clr_list[0]);

                if (clearing != null)
                {
                    dbo_DepositClass deposit = dbo_DepositDataClass.Select_Record(clearing.Clearing_No);

                    if (deposit.Net_Sales_Qty > 0 || sum_balance > 0)
                    {
                        Show(string.Format("พนักงาน รหัส {0} ไม่สามารถบันทึก In active ได้ เนื่องจาก มีหนี้คงค้าง {1} บาท และ ยอดของฝาก {2} ชิ้น", oclsdbo_User.User_ID, sum_balance, deposit.Net_Sales_Qty));
                    }
                    else
                    {
                        bool success = false;

                        if (!oclsdbo_User.Resign_Date.HasValue)
                        {
                            oclsdbo_User.Resign_Date = DateTime.Now.Date;
                        }

                        success = dbo_UserDataClass.Update(oclsdbo_User, User_ID);

                        if (success)
                        {
                            Show("บันทึกข้อมูลสำเร็จ!");
                        }
                    }
                }
                else
                {
                    bool success = false;

                    if (!oclsdbo_User.Resign_Date.HasValue)
                    {
                        oclsdbo_User.Resign_Date = DateTime.Now.Date;
                    }
                    success = dbo_UserDataClass.Update(oclsdbo_User, User_ID);

                    if (success)
                    {
                        Show("บันทึกข้อมูลสำเร็จ!");
                    }
                }
            }
            else
            {
                bool success = false;
                success = dbo_UserDataClass.Update(oclsdbo_User, User_ID);

                if (success)
                {
                    Show("บันทึกข้อมูลสำเร็จ!");
                }
            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    private void SetDataAgent(dbo_UserClass clsdbo_User)
    {

        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            clsdbo_User.User_Group_ID = "Agent";


            if (string.IsNullOrEmpty(txtCV_Code.Text))
            {
                clsdbo_User.CV_CODE = null;
            }
            else
            {
                clsdbo_User.CV_CODE = txtCV_Code.Text;
            }
            if (string.IsNullOrEmpty(txtUser_Name.Text))
            {
                clsdbo_User.Username = null;
            }
            else
            {
                clsdbo_User.Username = txtUser_Name.Text;
            }

            if (ddlTitle_ID.SelectedIndex == 0)
            {
                clsdbo_User.Title_ID = null;
            }
            else
            {
                clsdbo_User.Title_ID = ddlTitle_ID.SelectedValue;
            }
            if (ddlPosition.SelectedIndex == 0)
            {
                clsdbo_User.Position = null;
            }
            else
            {
                clsdbo_User.Position = ddlPosition.SelectedValue;
            }
            if (ddlRole.SelectedIndex == 0)
            {
                clsdbo_User.Role_ID = null;
            }
            else
            {
                clsdbo_User.Role_ID = ddlRole.SelectedValue;
            }
            ///if (ddlApprove.SelectedIndex == 0)
            //{
            //    clsdbo_User.Approval_Status_ID = null;
            //}
            //else
            //{
            clsdbo_User.Approval_Status_ID = ddlApprove.SelectedValue;
            //}
            if (ddlStatus.SelectedIndex == 0)
            {
                clsdbo_User.Status = null;
            }
            else
            {
                clsdbo_User.Status = ddlStatus.SelectedValue;
            }
            if (ddlShowDashborad.SelectedIndex == 0)
            {
                clsdbo_User.ShowDashboard = null;
            }
            else
            {
                clsdbo_User.ShowDashboard = ddlShowDashborad.SelectedValue;
            }

            if (ddlPayment_Type.SelectedIndex == 0)
            {
                clsdbo_User.Payment_Type = null;
            }
            else
            {
                clsdbo_User.Payment_Type = ddlPayment_Type.SelectedValue;
            }



            if (ddlHome_District.SelectedIndex == 0)
            {
                clsdbo_User.Home_District = null;
            }
            else
            {
                clsdbo_User.Home_District = ddlHome_District.SelectedValue;
            }

            if (ddlHome_Sub_district.SelectedIndex == 0)
            {
                clsdbo_User.Home_Sub_district = null;
            }
            else
            {
                clsdbo_User.Home_Sub_district = ddlHome_Sub_district.SelectedValue;
            }

            if (ddlHome_Province.SelectedIndex == 0)
            {
                clsdbo_User.Home_Province = null;
            }
            else
            {
                clsdbo_User.Home_Province = ddlHome_Province.SelectedValue;
            }

            if (ddlShipment_District.SelectedIndex == 0)
            {
                clsdbo_User.Present_District = null;
            }
            else
            {
                clsdbo_User.Present_District = ddlShipment_District.SelectedValue;
            }

            if (ddlShipment_Sub_district.SelectedIndex == 0)
            {
                clsdbo_User.Present_Sub_District = null;
            }
            else
            {
                clsdbo_User.Present_Sub_District = ddlShipment_Sub_district.SelectedValue;
            }

            if (ddlShipment_Province.SelectedIndex == 0)
            {
                clsdbo_User.Present_Province = null;
            }
            else
            {
                clsdbo_User.Present_Province = ddlShipment_Province.SelectedValue;
            }



            if (string.IsNullOrEmpty(txtUser_ID.Text))
            {
                clsdbo_User.User_ID = null;
            }
            else
            {
                clsdbo_User.User_ID = txtUser_ID.Text;
            }
            if (string.IsNullOrEmpty(txtFirst_Name.Text))
            {
                clsdbo_User.First_Name = null;
            }
            else
            {
                clsdbo_User.First_Name = txtFirst_Name.Text;
            }
            if (string.IsNullOrEmpty(txtLast_Name.Text))
            {
                clsdbo_User.Last_Name = string.Empty;
            }
            else
            {
                clsdbo_User.Last_Name = txtLast_Name.Text;
            }



            if (string.IsNullOrEmpty(txtHome_Phone_No.Text))
            {
                clsdbo_User.Home_Phone_No = null;
            }
            else
            {
                clsdbo_User.Home_Phone_No = txtHome_Phone_No.Text;
            }
            if (string.IsNullOrEmpty(txtMobile.Text))
            {
                clsdbo_User.Mobile = null;
            }
            else
            {
                clsdbo_User.Mobile = txtMobile.Text;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                clsdbo_User.Email = null;
            }
            else
            {
                clsdbo_User.Email = txtEmail.Text;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                clsdbo_User.Password = null;


            }
            else
            {
                clsdbo_User.Password = txtPassword.Text;
            }




            if (string.IsNullOrEmpty(txtBirthdate.Text))
            {
                clsdbo_User.Birthdate = null;
            }
            else
            {
                clsdbo_User.Birthdate = DateTime.Parse(txtBirthdate.Text);
            }
            if (string.IsNullOrEmpty(txtID_Card_No.Text))
            {
                clsdbo_User.ID_Card_No = null;
            }
            else
            {
                clsdbo_User.ID_Card_No = txtID_Card_No.Text;
            }
            if (string.IsNullOrEmpty(txtHome_House_No.Text))
            {
                clsdbo_User.Home_House_No = null;
            }
            else
            {
                clsdbo_User.Home_House_No = txtHome_House_No.Text;
            }
            if (string.IsNullOrEmpty(txtHome_Village.Text))
            {
                clsdbo_User.Home_Village = null;
            }
            else
            {
                clsdbo_User.Home_Village = txtHome_Village.Text;
            }
            if (string.IsNullOrEmpty(txtHome_Village_No.Text))
            {
                clsdbo_User.Home_Village_No = null;
            }
            else
            {
                clsdbo_User.Home_Village_No = txtHome_Village_No.Text;
            }
            if (string.IsNullOrEmpty(txtHome_Alley.Text))
            {
                clsdbo_User.Home_Alley = null;
            }
            else
            {
                clsdbo_User.Home_Alley = txtHome_Alley.Text;
            }
            if (string.IsNullOrEmpty(txtHome_Road.Text))
            {
                clsdbo_User.Home_Road = null;
            }
            else
            {
                clsdbo_User.Home_Road = txtHome_Road.Text;
            }
            if (string.IsNullOrEmpty(ddlHome_Sub_district.Text))
            {
                clsdbo_User.Home_Sub_district = null;
            }
            else
            {
                clsdbo_User.Home_Sub_district = ddlHome_Sub_district.Text;
            }
            if (string.IsNullOrEmpty(ddlHome_District.Text))
            {
                clsdbo_User.Home_District = null;
            }
            else
            {
                clsdbo_User.Home_District = ddlHome_District.Text;
            }
            if (string.IsNullOrEmpty(ddlHome_Province.Text))
            {
                clsdbo_User.Home_Province = null;
            }
            else
            {
                clsdbo_User.Home_Province = ddlHome_Province.Text;
            }



            if (string.IsNullOrEmpty(txtHome_Postal_ID.Text))
            {
                clsdbo_User.Home_Postal_ID = null;
            }
            else
            {
                clsdbo_User.Home_Postal_ID = txtHome_Postal_ID.Text;
            }
            if (string.IsNullOrEmpty(txtJoin_Date.Text))
            {
                clsdbo_User.Join_Date = null;
            }
            else
            {
                clsdbo_User.Join_Date = DateTime.Parse(txtJoin_Date.Text);
            }
            if (string.IsNullOrEmpty(txtResign_Date.Text))
            {
                clsdbo_User.Resign_Date = null;
            }
            else
            {
                clsdbo_User.Resign_Date = DateTime.Parse(txtResign_Date.Text);
            }

            if (string.IsNullOrEmpty(txtCredit_Term.Text))
            {
                clsdbo_User.Credit_Term = null;
            }
            else
            {
                clsdbo_User.Credit_Term = System.Convert.ToByte(txtCredit_Term.Text);
            }
            if (string.IsNullOrEmpty(txtPresent_House_No.Text))
            {
                clsdbo_User.Present_House_No = null;
            }
            else
            {
                clsdbo_User.Present_House_No = txtPresent_House_No.Text;
            }
            if (string.IsNullOrEmpty(txtPresent_Village.Text))
            {
                clsdbo_User.Present_Village = null;
            }
            else
            {
                clsdbo_User.Present_Village = txtPresent_Village.Text;
            }
            if (string.IsNullOrEmpty(txtPresent_Village_No.Text))
            {
                clsdbo_User.Present_Village_No = null;
            }
            else
            {
                clsdbo_User.Present_Village_No = txtPresent_Village_No.Text;
            }
            if (string.IsNullOrEmpty(txtPresent_Alley.Text))
            {
                clsdbo_User.Present_Alley = null;
            }
            else
            {
                clsdbo_User.Present_Alley = txtPresent_Alley.Text;
            }
            if (string.IsNullOrEmpty(txtPresent_Road.Text))
            {
                clsdbo_User.Present_Road = null;
            }
            else
            {
                clsdbo_User.Present_Road = txtPresent_Road.Text;
            }

            if (string.IsNullOrEmpty(ddlShipment_Sub_district.Text))
            {
                clsdbo_User.Present_Sub_District = null;
            }
            else
            {
                clsdbo_User.Present_Sub_District = ddlShipment_Sub_district.Text;
            }
            if (string.IsNullOrEmpty(ddlShipment_District.Text))
            {
                clsdbo_User.Present_District = null;
            }
            else
            {
                clsdbo_User.Present_District = ddlShipment_District.Text;
            }
            if (string.IsNullOrEmpty(ddlShipment_Province.Text))
            {
                clsdbo_User.Present_Province = null;
            }
            else
            {
                clsdbo_User.Present_Province = ddlShipment_Province.Text;
            }
            if (string.IsNullOrEmpty(txtPresent_Postal_ID.Text))
            {
                clsdbo_User.Present_Postal_ID = null;
            }
            else
            {
                clsdbo_User.Present_Postal_ID = txtPresent_Postal_ID.Text;
            }

            // clsdbo_User.User_Group_ID = txtVarArea.Text;



            List<string> listofCheckbox = new List<string>();

            if (chkApplied_1.Checked)
            {
                listofCheckbox.Add("ใบสมัคร");
            }
            if (chkApplied_2.Checked)
            {
                listofCheckbox.Add("รูปถ่าย");
            }
            if (chkApplied_3.Checked)
            {
                listofCheckbox.Add("สำเนาทะเบียนบ้าน");
            }
            if (chkApplied_4.Checked)
            {
                listofCheckbox.Add("สำเนาบัตรประชาชน");
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


            clsdbo_User.Applied_Document = doc;
            clsdbo_User.Created_By = HttpContext.Current.Request.Cookies["User_ID"].Value;



            if (string.IsNullOrEmpty(txtCredit_Term.Text))
            {
                clsdbo_User.Credit_Term = null;
            }
            else
            {
                clsdbo_User.Credit_Term = clsdbo_User.Credit_Term = Convert.ToByte(txtCredit_Term.Text);
            }


            clsdbo_User.Route = txtRoute.Text;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    private void SearchSubmit()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        int i = 0;
        try
        {
            string PositionKey = (ddlSearchPosition.SelectedIndex == 0 ? string.Empty : ddlSearchPosition.SelectedItem.Value);

            logger.Debug("PositionKey " + PositionKey);
            string user_id = txtSearchUser_ID.Text;

            string role = (ddlSearchRole.SelectedIndex == 0 ? string.Empty : ddlSearchRole.SelectedItem.Value);

            logger.Debug("role " + role);

            Nullable<DateTime> joindate = null;
            if (!string.IsNullOrEmpty(txtSearchStartDate.Text))
            {
                joindate = DateTime.Parse(txtSearchStartDate.Text);
            }

            logger.Debug("user_id " + user_id);


            if (user_id == "")
            {
                List<dbo_UserClass> userList = new List<dbo_UserClass>();

                logger.Debug("ddlSearchAgentName.Items.Count " + ddlSearchAgentName.Items.Count);


                if (!string.IsNullOrEmpty(txtSearchCVCode.Text.Trim()))
                {
                    List<dbo_UserClass> dt = dbo_UserDataClass.Search(txtSearchCVCode.Text, txtSearchFirstName.Text, PositionKey, string.Empty, ddlSearchStatus.Text, "Agent", role, txtSearchCVCode.Text, joindate, string.Empty, string.Empty);
                    foreach (dbo_UserClass _user in dt)
                    {
                        userList.Add(_user);
                    }
                }
                else
                {

                    for (i = 1; i < ddlSearchAgentName.Items.Count; i++)
                    {
                        List<dbo_UserClass> dt = dbo_UserDataClass.Search(ddlSearchAgentName.Items[i].Value, txtSearchFirstName.Text, PositionKey, string.Empty, ddlSearchStatus.Text, "Agent", role, txtSearchCVCode.Text, joindate, string.Empty, string.Empty);
                        foreach (dbo_UserClass _user in dt)
                        {
                            userList.Add(_user);
                        }
                    }
                }

                if (userList.Count > 0)
                {
                    GridViewUser.DataSource = userList;
                    GridViewUser.DataBind();
                    logger.Debug("GridViewUser.DataBind();");

                    GridViewUser.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    GridViewUser.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }
            else
            {
                List<dbo_UserClass> dt = dbo_UserDataClass.Search(user_id, txtSearchFirstName.Text, PositionKey, string.Empty, ddlSearchStatus.Text, "Agent", role, txtSearchCVCode.Text, joindate, string.Empty, string.Empty);

                if (dt.Count > 0)
                {
                    GridViewUser.DataSource = dt;
                    GridViewUser.DataBind();
                    logger.Debug("GridViewUser.DataBind();");

                    GridViewUser.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    GridViewUser.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }
            //GridViewUser.DataSource = dt;
            //GridViewUser.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void InitialInstallation()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            GridViewInstallation.EditIndex = -1;
            GridViewInstallation.ShowFooter = false;

            List<dbo_InstallationClass> item = dbo_InstallationDataClass.Search(txtUser_ID.Text);
            GridViewInstallation.DataSource = item;
            GridViewInstallation.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void InitialBenefit()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            GridViewBenefit.EditIndex = -1;
            GridViewBenefit.ShowFooter = false;


            List<dbo_BenefitClass> item = dbo_BenefitDataClass.Search(txtUser_ID.Text);

            GridViewBenefit.DataSource = item;
            GridViewBenefit.DataBind();
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

            pnlForm.Visible = false;
            pnlGrid.Visible = false;
            pnlInstallation.Visible = false;
            pnlBenefit.Visible = false;
            pnlStep.Visible = true;

            switch (panelName)
            {
                case "pnlForm":
                    pnlForm.Visible = true;
                    break;
                case "pnlGrid":
                    pnlGrid.Visible = true;
                    pnlStep.Visible = false;

                    break;
                case "pnlInstallation":
                    pnlInstallation.Visible = true;
                    break;
                case "pnlBenefit":
                    pnlBenefit.Visible = true;
                    break;
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
            string script = string.Format("alert('{0}');", cleanMessage);

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void ShowStep1()
    {
        try
        {
            lblstep1.Attributes.Remove("class");
            lblstep1.Attributes.Add("class", "btn btn-primary btn-circle active-step");

            lblstep2.Attributes.Remove("class");
            lblstep2.Attributes.Add("class", "btn btn-default btn-circle");

            lblstep3.Attributes.Remove("class");
            lblstep3.Attributes.Add("class", "btn btn-default btn-circle");
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    private void ShowStep2()
    {
        try
        {
            lblstep1.Attributes.Remove("class");
            lblstep1.Attributes.Add("class", "btn btn-success btn-circle");

            lblstep2.Attributes.Remove("class");
            lblstep2.Attributes.Add("class", "btn btn-primary btn-circle active-step");

            lblstep3.Attributes.Remove("class");
            lblstep3.Attributes.Add("class", "btn btn-default btn-circle");
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    private void ShowStep3()
    {

        try
        {
            lblstep1.Attributes.Remove("class");
            lblstep1.Attributes.Add("class", "btn btn-success btn-circle");

            lblstep2.Attributes.Remove("class");
            lblstep2.Attributes.Add("class", "btn btn-success btn-circle");

            lblstep3.Attributes.Remove("class");
            lblstep3.Attributes.Add("class", "btn btn-primary btn-circle active-step");
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion

    #region GridView Events
    protected void GridViewUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            logger.Debug("e.CommandName " + e.CommandName);

            if (e.CommandName == "View_User_ID")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string User_ID = lnkView.CommandArgument;
                hdfUser_ID.Value = lnkView.CommandArgument;
                GetDetailsDataToForm(User_ID, "View");
            }
            else if (e.CommandName == "_Edit")
            {
                LinkButton lnkView1 = (LinkButton)e.CommandSource;
                string User_ID1 = lnkView1.CommandArgument;
                GetDetailsDataToForm(User_ID1.Trim(), "Edit");

            }
            else if (e.CommandName == "_Delete")
            {

                LinkButton lnkView1 = (LinkButton)e.CommandSource;
                string User_ID1 = lnkView1.CommandArgument;



                bool success = dbo_UserDataClass.Delete(User_ID1);

                if (success)
                {
                    Show("ลบข้อมูลสำเร็จ!");

                    try
                    {

                        string PositionKey = (ddlSearchPosition.SelectedIndex == 0 ? string.Empty : ddlSearchPosition.SelectedItem.Value);

                        string user_id = txtSearchUser_ID.Text;

                        // (ddlSearchFirstName.SelectedIndex == 0 ? string.Empty : ddlSearchFirstName.SelectedItem.Value);

                        string role = (ddlSearchRole.SelectedIndex == 0 ? string.Empty : ddlSearchRole.SelectedItem.Value);

                        Nullable<DateTime> joindate = null;
                        if (!string.IsNullOrEmpty(txtSearchStartDate.Text))
                        {
                            joindate = DateTime.Parse(txtSearchStartDate.Text);
                        }

                        List<dbo_UserClass> dt = dbo_UserDataClass.Search(user_id, txtSearchFirstName.Text, PositionKey, string.Empty, ddlSearchStatus.Text, "Agent", role, txtSearchCVCode.Text, joindate, string.Empty, string.Empty);

                        GridViewUser.DataSource = dt;
                        GridViewUser.DataBind();

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }


                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    } 

    protected void GridViewBenefit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            GridViewBenefit.EditIndex = -1;
            GridViewBenefit.ShowFooter = false;

            List<dbo_BenefitClass> item = dbo_BenefitDataClass.Search(txtUser_ID.Text);  //   SelectAll();
            GridViewBenefit.DataSource = item;
            GridViewBenefit.DataBind();
            //InitialBenefit();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewInstallation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            GridViewInstallation.EditIndex = -1;
            GridViewInstallation.ShowFooter = false;

            List<dbo_InstallationClass> item = dbo_InstallationDataClass.Search(txtUser_ID.Text);  //   SelectAll();
            GridViewInstallation.DataSource = item;
            GridViewInstallation.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewBenefit_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            switch (e.CommandName)
            {
                case "AddNew":
                    string id = GenerateID.Benefit_ID(txtCV_Code.Text);

                    TextBox _Benefit_Name = (TextBox)GridViewBenefit.FooterRow.FindControl("txtNewBenefit_Name");
                    TextBox _txtNewBenefit_Date = (TextBox)GridViewBenefit.FooterRow.FindControl("txtNewBenefit_Date");
                    TextBox _txtNewBeneficiary = (TextBox)GridViewBenefit.FooterRow.FindControl("txtNewBeneficiary");
                    TextBox _txtNewRelationship = (TextBox)GridViewBenefit.FooterRow.FindControl("txtNewRelationship");
                    TextBox _txtNewBenefit_Amount = (TextBox)GridViewBenefit.FooterRow.FindControl("txtNewBenefit_Amount");
                    TextBox _txtNewEnd_Date = (TextBox)GridViewBenefit.FooterRow.FindControl("txtNewEnd_Date");


                    dbo_BenefitClass benefit = new dbo_BenefitClass();
                    benefit.Benefit_ID = id;
                    benefit.Benefit_Name = _Benefit_Name.Text;
                    benefit.User_ID = txtUser_ID.Text;
                    benefit.Beneficiary = _txtNewBeneficiary.Text;
                    benefit.Benefit_Amount = Decimal.Parse(_txtNewBenefit_Amount.Text);
                    benefit.Relationship = _txtNewRelationship.Text;

                    Nullable<DateTime> Transaction_Date_ = null;
                    if (!string.IsNullOrEmpty(_txtNewBenefit_Date.Text))
                    {
                        Transaction_Date_ = DateTime.Parse(_txtNewBenefit_Date.Text);
                    }
                    benefit.Benefit_Date = Transaction_Date_;

                    Transaction_Date_ = null;
                    if (!string.IsNullOrEmpty(_txtNewEnd_Date.Text))
                    {
                        Transaction_Date_ = DateTime.Parse(_txtNewEnd_Date.Text);
                    }
                    benefit.End_Date = Transaction_Date_;

                    bool succes = false;
                    string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    dbo_BenefitDataClass.Add(benefit, User_ID);
                    if (succes)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("บันทึกสำเร็จ!");
                    }
                    InitialBenefit();

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewInstallation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            switch (e.CommandName)
            {
                case "AddNew":
                    string id = GenerateID.Installation_ID(txtCV_Code.Text);

                    TextBox _Installation_Detail = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewInstallation_Detail");
                    DropDownList _Installation_Type = (DropDownList)GridViewInstallation.FooterRow.FindControl("ddlNewInstallation_Type");
                    TextBox _Description = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewDescription");

                    TextBox _Transaction_Date = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewTransaction_Date");
                    TextBox _Due_Date = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewDue_Date");

                    TextBox _Installation_Amount = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewInstallation_Amount");
                    TextBox _Payment_Amount = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewPaid");
                    TextBox _Balance_Amount = (TextBox)GridViewInstallation.FooterRow.FindControl("txtNewBalance_Amount");
                    dbo_InstallationClass installation = new dbo_InstallationClass();

                    installation.Installation_ID = GenerateID.Installation_ID(txtCV_Code.Text);
                    installation.User_ID = txtUser_ID.Text;
                    installation.Installation_Type = (_Installation_Type.SelectedIndex == 0 ? null : _Installation_Type.SelectedValue);
                    installation.Installation_Detail = (string.IsNullOrEmpty(_Installation_Detail.Text) ? null : _Installation_Detail.Text);
                    installation.Description = (string.IsNullOrEmpty(_Description.Text) ? null : _Description.Text);
                    installation.Installation_Amount = Decimal.Parse(_Installation_Amount.Text);
                    installation.Balance_Amount = Decimal.Parse(_Balance_Amount.Text);
                    installation.Payment_Amount = Decimal.Parse(_Payment_Amount.Text);


                    Nullable<DateTime> Transaction_Date_ = null;
                    if (!string.IsNullOrEmpty(_Transaction_Date.Text))
                    {
                        Transaction_Date_ = DateTime.Parse(_Transaction_Date.Text);
                    }
                    installation.Transaction_Date = Transaction_Date_;

                    Nullable<DateTime> Due_Date_ = null;
                    if (!string.IsNullOrEmpty(_Due_Date.Text))
                    {
                        Due_Date_ = DateTime.Parse(_Due_Date.Text);
                    }
                    installation.Due_Date = Due_Date_;


                    bool succes = false;
                    string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    succes = dbo_InstallationDataClass.Add(installation, User_ID);

                    if (succes)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("บันทึกสำเร็จ!");
                    }


                    InitialInstallation();

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    break;

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewInstallation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            int index = e.RowIndex;
            Label _Installation_ID = (Label)GridViewInstallation.Rows[e.RowIndex].FindControl("lblInstallation_ID");

            List<dbo_InstallationClass> user = dbo_InstallationDataClass.Search(_Installation_ID.Text);

            dbo_InstallationClass installation = new dbo_InstallationClass();

            installation.Installation_ID = _Installation_ID.Text;

            dbo_InstallationDataClass.Delete(installation);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");

            GridViewInstallation.ShowFooter = false;

            InitialInstallation();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewInstallation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            GridViewInstallation.EditIndex = e.NewEditIndex;
            List<dbo_InstallationClass> item_value = dbo_InstallationDataClass.Search(txtUser_ID.Text);
            GridViewInstallation.DataSource = item_value;
            GridViewInstallation.DataBind();

            Dictionary<string, string> type = dbo_ItemDataClass.GetDropDown("16");
            DropDownList ddl = (DropDownList)GridViewInstallation.Rows[e.NewEditIndex].FindControl("ddlEditInstallation_Type");
            ddl.DataSource = type;
            ddl.DataBind();
            HiddenField hdftype = (HiddenField)GridViewInstallation.Rows[e.NewEditIndex].FindControl("hdfEditInstallation_Type");
            ddl.Items.FindByText(hdftype.Value).Selected = true;




            /*
            DropDownList ddl = (DropDownList)gv.Rows[e.NewEditIndex].FindControl("ddlEditbank");
            ddl.DataSource = bank;
            ddl.DataBind();
            HiddenField hdfBank = (HiddenField)gv.Rows[e.NewEditIndex].FindControl("hdfEditbank");
            ddl.Items.FindByText(hdfBank.Value).Selected = true;
            */



            ViewState["NewEditIndex"] = e.NewEditIndex;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewInstallation_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int RowIndex = Convert.ToInt32((e.RowIndex).ToString());
            Label _Installation_ID = (Label)GridViewInstallation.Rows[RowIndex].FindControl("lblInstallation_ID");

            TextBox _Installation_Detail = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditInstallation_Detail");
            DropDownList _Installation_Type = (DropDownList)GridViewInstallation.Rows[RowIndex].FindControl("ddlEditInstallation_Type");
            TextBox _Description = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditDescription");
            TextBox _Installation_Amount = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditInstallation_Amount");
            TextBox _Transaction_Date = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditTransaction_Date");
            TextBox _Due_Date = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditDue_Date");
            TextBox _Payment_Amount = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditPaid");
            TextBox _Balance_Amount = (TextBox)GridViewInstallation.Rows[RowIndex].FindControl("txtEditBalance_Amount");

            dbo_InstallationClass installation = new dbo_InstallationClass();

            installation.Installation_ID = _Installation_ID.Text;
            installation.User_ID = txtUser_ID.Text;

            installation.Installation_Type = (_Installation_Type.SelectedIndex == 0 ? null : _Installation_Type.SelectedValue);
            installation.Installation_Detail = (string.IsNullOrEmpty(_Installation_Detail.Text) ? null : _Installation_Detail.Text);
            installation.Description = (string.IsNullOrEmpty(_Description.Text) ? null : _Description.Text);


            installation.Installation_Amount = Decimal.Parse(_Installation_Amount.Text.Replace(",", string.Empty));
            installation.Payment_Amount = Decimal.Parse(_Payment_Amount.Text.Replace(",", string.Empty));
            installation.Balance_Amount = Decimal.Parse(_Balance_Amount.Text.Replace(",", string.Empty));


            Nullable<DateTime> Transaction_Date_ = null;
            if (!string.IsNullOrEmpty(_Transaction_Date.Text))
            {
                Transaction_Date_ = DateTime.Parse(_Transaction_Date.Text);
            }
            installation.Transaction_Date = Transaction_Date_;

            Nullable<DateTime> Due_Date_ = null;
            if (!string.IsNullOrEmpty(_Due_Date.Text))
            {
                Due_Date_ = DateTime.Parse(_Due_Date.Text);
            }
            installation.Due_Date = Due_Date_;

            bool success = false;

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            success = dbo_InstallationDataClass.Update(installation, User_ID);

            if (success)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("บันทึกสำเร็จ!");
            }

            InitialInstallation();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewBenefit_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int RowIndex = Convert.ToInt32((e.RowIndex).ToString());


            Label _Benefit_ID = (Label)GridViewBenefit.Rows[RowIndex].FindControl("lblBenefit_ID");

            TextBox _Benefit_Name = (TextBox)GridViewBenefit.Rows[RowIndex].FindControl("txtEditBenefit_Name");
            TextBox _txtNewBenefit_Date = (TextBox)GridViewBenefit.Rows[RowIndex].FindControl("txtEditBenefit_Date");
            TextBox _txtNewBeneficiary = (TextBox)GridViewBenefit.Rows[RowIndex].FindControl("txtEditBeneficiary");
            TextBox _txtNewRelationship = (TextBox)GridViewBenefit.Rows[RowIndex].FindControl("txtEditRelationship");
            TextBox _txtNewBenefit_Amount = (TextBox)GridViewBenefit.Rows[RowIndex].FindControl("txtEditBenefit_Amount");
            TextBox _txtNewEnd_Date = (TextBox)GridViewBenefit.Rows[RowIndex].FindControl("txtEditEnd_Date");

            dbo_BenefitClass benefit = new dbo_BenefitClass();

            benefit.Benefit_ID = _Benefit_ID.Text;
            benefit.Benefit_Name = _Benefit_Name.Text;
            benefit.User_ID = txtUser_ID.Text;
            benefit.Beneficiary = _txtNewBeneficiary.Text;
            benefit.Benefit_Amount = Decimal.Parse(_txtNewBenefit_Amount.Text.Replace(",", string.Empty));
            benefit.Relationship = _txtNewRelationship.Text;

            Nullable<DateTime> Transaction_Date_ = null;
            if (!string.IsNullOrEmpty(_txtNewBenefit_Date.Text))
            {
                Transaction_Date_ = DateTime.Parse(_txtNewBenefit_Date.Text);
            }
            benefit.Benefit_Date = Transaction_Date_;

            Transaction_Date_ = null;
            if (!string.IsNullOrEmpty(_txtNewEnd_Date.Text))
            {
                Transaction_Date_ = DateTime.Parse(_txtNewEnd_Date.Text);
            }
            benefit.End_Date = Transaction_Date_;

            bool success = false;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            success = dbo_BenefitDataClass.Update(benefit, User_ID);

            if (success)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("บันทึกสำเร็จ!");
            }


            InitialBenefit();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewBenefit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            int index = e.RowIndex;
            Label _Benefit_ID = (Label)GridViewBenefit.Rows[e.RowIndex].FindControl("lblBenefit_ID");

            List<dbo_BenefitClass> user = dbo_BenefitDataClass.Search(_Benefit_ID.Text);

            dbo_BenefitClass benefit = new dbo_BenefitClass();

            benefit.Benefit_ID = _Benefit_ID.Text;

            dbo_BenefitDataClass.Delete(benefit);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");

            GridViewInstallation.ShowFooter = false;

            InitialBenefit();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void GridViewBenefit_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            GridViewBenefit.EditIndex = e.NewEditIndex;
            List<dbo_BenefitClass> item_value = dbo_BenefitDataClass.Search(txtUser_ID.Text);
            GridViewBenefit.DataSource = item_value;
            GridViewBenefit.DataBind();

            ViewState["NewEditIndex"] = e.NewEditIndex;
            //InitialBenefit();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewUser.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewUser.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewUser_DataBound(object sender, EventArgs e)
    {

        GridViewRow pagerRow = GridViewUser.BottomPagerRow;

        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {
            for (int i = 0; i < GridViewUser.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());
 
                if (i == GridViewUser.PageIndex)
                {
                    item.Selected = true;
                }
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = GridViewUser.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewUser.PageCount.ToString();

        }
    }
    #endregion

   
}