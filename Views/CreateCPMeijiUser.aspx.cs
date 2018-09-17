#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
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
    string User_ID_View =string.Empty;
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetUpDrowDownList();
            showPanel("pnlGrid");

            btnSearchSubmit_Click(sender, e);
        }
        else
        {
            if (!(String.IsNullOrEmpty(txtCP_Password.Text.Trim())))
            {
                txtCP_Password.Attributes["value"] = txtCP_Password.Text;
            }
        }
    }

    public void btnSave_Click(object sender, System.EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            if (btnCP_Save.Text == "แก้ไข")
            {
                GetDetailsDataToForm(txtCP_User_ID.Text, "Edit");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                if (txtCP_Password.Text == string.Empty)
                {
                    txtCP_Password.Text = hdfPasswrod.Value;
                }

                Validate("CPValidation");

                if (Page.IsValid)
                {
                    //string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(hdfUser_ID.Value);

                    Regex r = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])");
                    string Pass_Old = user_class.Password.ToString();
                    List<dbo_PasswordHistoryClass> item = dbo_PasswordHistoryDataClass.Search(txtCP_UserName.Text);
                    dbo_PasswordHistoryClass pass = item.OrderByDescending(f => f.Last_Password_Change_Or_Reset).Take(3).FirstOrDefault(f => f.Password == txtCP_Password.Text);

                    string cntUsername = CheckUsername.Check_Username(txtCP_UserName.Text, txtCP_User_ID.Text);
                    string cntUserID = CheckUsername.Check_UserID(txtCP_User_ID.Text);
                    bool flag = false;

                    if (Pass_Old.ToString() != txtCP_Password.Text)
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
                    //    if (Pass_Old.ToString() != txtCP_Password.Text)
                    //    {
                    //        if (pass != null)
                    //        {
                    //            flag = true;
                    //            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //            //Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
                    //        }
                    //    }
                    //}

                    if (txtCP_Password.Text.Length < 8)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสผ่านควรมีอย่างน้อย 8 ตัวอักษร");
                    }
                    else if (!r.IsMatch(txtCP_Password.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("รหัสผ่านควรประกอบด้วยตัวเลขและตัวอักษร");
                    }
                    //else if (pass != null)
                    //{
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
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        // Show("รหัสพนักงานไม่สามารถซ้ำได้");
                        Show("เนื่องจากรหัสพนักงาน " + txtCP_User_ID.Text + " ถูกสร้างขึ้นแล้ว ระบบจะทำการสร้างรหัสพนักงานให้ใหม่");
                        txtCP_User_ID.Text = GenerateID.UserID_CP();
                    }
                    else if (cntUsername != "0")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("ชื่อผู้ใช้งานไม่สามารถซ้ำได้");
                    }
                    else
                    {
                        if (btnSaveMode.Value == "บันทึก")
                        {
                            InsertRecord();
                            if (hdfPasswrod.Value != txtCP_Password.Text)
                            {
                                dbo_PasswordHistoryClass password = new dbo_PasswordHistoryClass();
                                password.Last_Password_Change_Or_Reset = DateTime.Now;
                                password.Password = txtCP_Password.Text;
                                password.User_ID = txtCP_UserName.Text;
                                dbo_PasswordHistoryDataClass.Add(password);
                            }

                        }
                        else
                        {
                            UpdateRecord();
                            if (hdfPasswrod.Value != txtCP_Password.Text)
                            {
                                dbo_PasswordHistoryClass password = new dbo_PasswordHistoryClass();
                                password.Last_Password_Change_Or_Reset = DateTime.Now;
                                password.Password = txtCP_Password.Text;
                                password.User_ID = txtCP_UserName.Text;

                                dbo_PasswordHistoryDataClass.Add(password);
                            }
                        }

                        SearchSubmit();

                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
                }
            }
        }
        catch(Exception ex)
        {
            logger.Debug(ex.Message);
        }
       
    }

    public void btnCancel_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        showPanel("pnlGrid");

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    public void btnAddNew_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        // ShowFormPanel(string.Empty, string.Empty);
        GetDetailsDataToForm(string.Empty, string.Empty);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    public void btnSearchSubmit_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SearchSubmit();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    public void btnSearchCancel_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SetUpDrowDownList();
        showPanel("pnlGrid");

        if (GridViewUser.Rows.Count > 0)
        {
            List<dbo_UserClass> dt = new List<dbo_UserClass>();
            GridViewUser.DataSource = dt;
            GridViewUser.DataBind();
        }


        GridViewUser.Visible = false;
        pnlNoRec.Visible = false;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void txtCP_Last_Name_Eng_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        string user_name = string.Format("{0}.{1}", txtCP_First_Name_Eng.Text, (txtCP_Last_Name_Eng.Text.Length > 3 ? txtCP_Last_Name_Eng.Text.Substring(0, 3) : txtCP_Last_Name_Eng.Text));
        txtCP_UserName.Text = user_name;
    }
    #endregion

    #region Methods
    private void showPanel(string panelName)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        pnlGrid.Visible = false;
        pnlCPMeiji.Visible = false;

        switch (panelName)
        {
            case "pnlGrid":
                pnlGrid.Visible = true;
                break;
            case "pnlCPMeiji":
                pnlCPMeiji.Visible = true;
                break;
        }
    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        //  Dictionary<string, string> role = dbo_ItemDataClass.GetRoleDropDown();
        //Dictionary<string, string> role = dbo_ItemDataClass.GetDropDown("09");

        List<dbo_RoleClass> role = dbo_RoleDataClass.Search(string.Empty, string.Empty, "CP Meiji");
        role.Insert(0, new dbo_RoleClass { Role_ID = string.Empty, Role_Name = "==ระบุ==" });

        ddlSearchRole.DataSource = role;
        ddlSearchRole.DataBind();

        Dictionary<string, string> positon = dbo_ItemDataClass.GetDropDown("04");
        ddlSearchPosition.DataSource = positon;
        ddlSearchPosition.DataBind();

        List<dbo_UserClass> user = dbo_UserDataClass.Search("", "", "", "", "", "CP Meiji", "", "", null, string.Empty, string.Empty);
        dbo_UserClass u = new dbo_UserClass();
        u.First_Name = "==ระบุ==";
        u.User_ID = string.Empty;

        user.Insert(0, u);
        ddlSearchFirst_Name.DataSource = user;
        ddlSearchFirst_Name.DataBind();

        ddlSearchState.ClearSelection();
        ddlSearchStatus.ClearSelection();

        Dictionary<string, string> title = dbo_ItemDataClass.GetDropDown("03");
        Dictionary<string, string> UserGroup = dbo_ItemDataClass.GetDropDown("1601");
        Dictionary<string, string> approve = dbo_ItemDataClass.GetDropDown("1801");
        Dictionary<string, string> showdashboard = dbo_ItemDataClass.GetDropDown("1901");

        ddlCP_TitleID.DataSource = title;
        ddlCP_TitleID.DataBind();
        ddlCP_Position.DataSource = positon;
        ddlCP_Position.DataBind();

        ddlCP_UserRole.DataSource = role;
        ddlCP_UserRole.DataBind();

        Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");

        //ddlSearchState.DataSource = region.Where(f => f.Key != string.Empty);
        ddlSearchState.DataSource = region;
        ddlSearchState.DataBind();

        //ddlLocation_Region.DataSource = region;
        ddlLocation_Region.DataSource = region.Where(f => f.Key != string.Empty);
        ddlLocation_Region.DataBind();

    }

    private void GetDetailsDataToForm(string id, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            showPanel("pnlCPMeiji");

            txtCP_FirstName.Text = string.Empty;
            txtCP_LastName.Text = string.Empty;
            txtCP_First_Name_Eng.Text = string.Empty;
            txtCP_Last_Name_Eng.Text = string.Empty;
            txtCP_Phone.Text = string.Empty;
            txtCP_Mobile.Text = string.Empty;
            txtCP_UserName.Text = " ";

            txtCP_Password.Text = "";
            txtCP_Password.Text = null;

            txtCP_Email.Text = string.Empty;


            ddlCP_TitleID.ClearSelection();
            ddlCP_Position.ClearSelection();
            ddlCP_UserRole.ClearSelection();
            ddlCP_ShowDashboard.ClearSelection();
            ddlCP_Status.ClearSelection();
            txtCP_User_ID.Enabled = false;
            ddlLocation_Region.ClearSelection();

            hdfPasswrod.Value = string.Empty;



            if (!string.IsNullOrEmpty(id))
            {
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(id);

                txtCP_User_ID.Text = user_class.User_ID;
                txtCP_FirstName.Text = user_class.First_Name;
                txtCP_LastName.Text = user_class.Last_Name;
                txtCP_First_Name_Eng.Text = user_class.First_Name_Eng;
                txtCP_Last_Name_Eng.Text = user_class.Last_Name_Eng;
                txtCP_Phone.Text = user_class.Home_Phone_No;
                txtCP_Mobile.Text = user_class.Mobile;
                txtCP_Email.Text = user_class.Email;
                txtCP_UserName.Text = user_class.Username;
                txtCP_Password.Text = user_class.Password;
                hdfPasswrod.Value = user_class.Password;

                if (user_class.Title_ID != null)
                {
                    if (ddlCP_TitleID.Items.FindByText(user_class.Title_ID.Trim()) != null)
                        ddlCP_TitleID.Items.FindByText(user_class.Title_ID.Trim()).Selected = true;
                }

                if (user_class.Region != null)
                {
                    foreach (string region in user_class.Region.Split(',').ToList())
                    {
                        if (ddlLocation_Region.Items.FindByText(region) != null)
                            ddlLocation_Region.Items.FindByText(region).Selected = true;

                    }
                }

                if (user_class.Position != null)
                {
                    if (ddlCP_Position.Items.FindByText(user_class.Position.Trim()) != null)
                        ddlCP_Position.Items.FindByText(user_class.Position.Trim()).Selected = true;
                }
                if (user_class.Role_ID != null)
                {
                    //if (ddlCP_UserRole.Items.FindByText(user_class.Role_ID.Trim()) != null)
                    //    ddlCP_UserRole.Items.FindByText(user_class.Role_ID.Trim()).Selected = true;
                    if (ddlCP_UserRole.Items.FindByValue(user_class.Role_ID.Trim()) != null)
                        ddlCP_UserRole.Items.FindByValue(user_class.Role_ID.Trim()).Selected = true;
                }
                if (user_class.ShowDashboard != null)
                {
                    if (ddlCP_ShowDashboard.Items.FindByText(user_class.ShowDashboard.Trim()) != null)
                        ddlCP_ShowDashboard.Items.FindByText(user_class.ShowDashboard.Trim()).Selected = true;
                }

                if (user_class.Status != null)
                {
                    if (ddlCP_Status.Items.FindByText(user_class.Status.Trim()) != null)
                        ddlCP_Status.Items.FindByText(user_class.Status.Trim()).Selected = true;
                }
            }

            bool enable = Mode != "View";

            txtCP_UserName.Enabled = enable;
            txtCP_Password.Enabled = enable;
            ddlCP_Status.Enabled = enable;
            // ddlCP_Approval.Enabled = enable;

            txtCP_FirstName.Enabled = enable;
            txtCP_LastName.Enabled = enable;
            txtCP_First_Name_Eng.Enabled = enable;
            txtCP_Last_Name_Eng.Enabled = enable;
            txtCP_Phone.Enabled = enable;
            txtCP_Mobile.Enabled = enable;
            txtCP_Email.Enabled = enable;
            ddlCP_UserRole.Enabled = enable;
            ddlCP_ShowDashboard.Enabled = enable;
            ddlCP_TitleID.Enabled = enable;
            ddlCP_Position.Enabled = enable;

            ddlLocation_Region.Enabled = enable;
            if (Mode == "View")
            {
                btnCP_Save.Visible = true;
                btnCP_Save.Text = "แก้ไข";
                btnCP_Cancel.Text = "กลับไปหน้าค้นหา";
                btnSaveMode.Value = "แก้ไข";
                LabelPageHeader.Text = "รายละเอียดข้อมูล User";
            }
            else if (Mode == "Edit")
            {
                btnCP_Save.Visible = true;
                btnCP_Save.Text = "บันทึก";
                btnCP_Cancel.Text = "ยกเลิก";
                btnSaveMode.Value = "แก้ไข";
                LabelPageHeader.Text = "แก้ไขข้อมูล User";
            }
            else if (string.IsNullOrEmpty(Mode))
            {
                btnCP_Save.Visible = true;
                btnCP_Save.Text = "บันทึก";
                btnCP_Cancel.Text = "ยกเลิก";
                btnSaveMode.Value = "บันทึก";
                txtCP_User_ID.Text = GenerateID.UserID_CP();
                LabelPageHeader.Text = "สร้างข้อมูล User";
            }
        }
        catch (Exception)
        {

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

    private void InsertRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_UserClass clsdbo_User = new dbo_UserClass();

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

        SetDataCP(clsdbo_User);

        bool success = false;
        success = dbo_UserDataClass.Add(clsdbo_User, User_ID);

        if (success)
        {
            Show("บันทึกสำเร็จ!");
        }
    }

    private void UpdateRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_UserClass oclsdbo_User = new dbo_UserClass();
        // dbo_UserClass clsdbo_User = new dbo_UserClass();

        oclsdbo_User.User_ID = txtCP_User_ID.Text;
        oclsdbo_User = dbo_UserDataClass.Select_Record(oclsdbo_User.User_ID);

        Validate("CPValidation");

        SetDataCP(oclsdbo_User);

        if (IsValid)
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            bool success = dbo_UserDataClass.Update(oclsdbo_User, User_ID);

            if (success)
            {
                bool flag = false;
                String strString = "0401,0402,0403,0404,0405"; 
                String[] myArr = strString.Split(',');
                foreach(var d in myArr)
                {
                    if(d == oclsdbo_User.Item_Value_ID)
                    {
                        flag = true;
                    }
                }
                if(flag ==true)
                {
                    //Show("gg");
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                    List<dbo_AgentClass> _cv_code = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == oclsdbo_User.User_ID.Trim() || f.GM_ID == oclsdbo_User.User_ID.Trim() || f.SD_ID == oclsdbo_User.User_ID.Trim() || f.SM_ID == oclsdbo_User.User_ID.Trim() || f.APV_ID == oclsdbo_User.User_ID.Trim()).Select(f => f));

                    if (_cv_code.Count > 0)
                    {

                        foreach (var d in _cv_code)
                        {
                            dbo_AgentClass oclsdbo_Agent = new dbo_AgentClass();
                            // dbo_AgentClass clsdbo_Agent = new dbo_AgentClass();
                            oclsdbo_Agent.CV_Code = d.CV_Code;
                            oclsdbo_Agent = dbo_AgentDataClass.Select_Record(oclsdbo_Agent.CV_Code);

                            if (d.APV_ID == oclsdbo_User.User_ID.Trim())
                            {
                                oclsdbo_Agent.APV_ID = string.Empty;
                                dbo_AgentDataClass.Update(oclsdbo_Agent, User_ID);
                                //Show("APV_ID");

                            }
                            else if(d.DM_ID == oclsdbo_User.User_ID.Trim())
                            {
                                oclsdbo_Agent.DM_ID = string.Empty;
                                dbo_AgentDataClass.Update(oclsdbo_Agent, User_ID);
                                //Show("DM_ID");
                            }
                            else if (d.GM_ID == oclsdbo_User.User_ID.Trim())
                            {
                                oclsdbo_Agent.GM_ID = string.Empty;
                                dbo_AgentDataClass.Update(oclsdbo_Agent, User_ID);
                                //Show("GM_ID");
                            }
                            else if (d.SD_ID == oclsdbo_User.User_ID.Trim())
                            {
                                oclsdbo_Agent.SD_ID = string.Empty;
                                dbo_AgentDataClass.Update(oclsdbo_Agent, User_ID);
                                //Show("SD_ID");
                            }
                            else if (d.SM_ID == oclsdbo_User.User_ID.Trim())
                            {
                                oclsdbo_Agent.SM_ID = string.Empty;
                                dbo_AgentDataClass.Update(oclsdbo_Agent, User_ID);
                                //Show("SM_ID");
                            }
                        }
                    }
                }

  
               

                Show("บันทึกสำเร็จ");
            }
            else
            {
                Show("error");
            }
        }
    }

    private void SetDataCP(dbo_UserClass clsdbo_User)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            if (string.IsNullOrEmpty(txtCP_User_ID.Text))
            {
                clsdbo_User.User_ID = null;
            }
            else
            {
                clsdbo_User.User_ID = txtCP_User_ID.Text;
            }

            if (string.IsNullOrEmpty(txtCP_UserName.Text))
            {
                clsdbo_User.Username = null;
            }
            else
            {
                clsdbo_User.Username = txtCP_UserName.Text.Trim();
            }


            if (string.IsNullOrEmpty(txtCP_Password.Text))
            {
                clsdbo_User.Password = null;
            }
            else
            {
                clsdbo_User.Password = txtCP_Password.Text;
            }

            if (string.IsNullOrEmpty(txtCP_FirstName.Text))
            {
                clsdbo_User.First_Name = null;
            }
            else
            {
                clsdbo_User.First_Name = txtCP_FirstName.Text;
            }
            if (string.IsNullOrEmpty(txtCP_LastName.Text))
            {
                clsdbo_User.Last_Name = null;
            }
            else
            {
                clsdbo_User.Last_Name = txtCP_LastName.Text;
            }

            if (string.IsNullOrEmpty(txtCP_First_Name_Eng.Text))
            {
                clsdbo_User.First_Name_Eng = null;
            }
            else
            {
                clsdbo_User.First_Name_Eng = txtCP_First_Name_Eng.Text;
            }
            if (string.IsNullOrEmpty(txtCP_Last_Name_Eng.Text))
            {
                clsdbo_User.Last_Name_Eng = null;
            }
            else
            {
                clsdbo_User.Last_Name_Eng = txtCP_Last_Name_Eng.Text;
            }

            if (string.IsNullOrEmpty(txtCP_Phone.Text))
            {
                clsdbo_User.Home_Phone_No = null;
            }
            else
            {
                clsdbo_User.Home_Phone_No = txtCP_Phone.Text;
            }
            if (string.IsNullOrEmpty(txtCP_Mobile.Text))
            {
                clsdbo_User.Mobile = null;
            }
            else
            {
                clsdbo_User.Mobile = txtCP_Mobile.Text;
            }

            if (string.IsNullOrEmpty(txtCP_Email.Text))
            {
                clsdbo_User.Email = null;
            }
            else
            {
                clsdbo_User.Email = txtCP_Email.Text;
            }

            if (ddlCP_TitleID.SelectedIndex == 0)
            {
                clsdbo_User.Title_ID = null;
            }
            else
            {
                clsdbo_User.Title_ID = ddlCP_TitleID.SelectedValue;
            }

            if (ddlCP_Position.SelectedIndex == 0)
            {
                clsdbo_User.Position = null;
            }
            else
            {
                clsdbo_User.Position = ddlCP_Position.SelectedValue;
            }

            if (ddlCP_UserRole.SelectedIndex == 0)
            {
                clsdbo_User.Role_ID = null;
            }
            else
            {
                clsdbo_User.Role_ID = ddlCP_UserRole.SelectedValue;
            }


            if (ddlCP_ShowDashboard.SelectedIndex == 0)
            {
                clsdbo_User.ShowDashboard = null;
            }
            else
            {
                clsdbo_User.ShowDashboard = ddlCP_ShowDashboard.SelectedValue;
            }
            if (ddlCP_Status.SelectedIndex == 0)
            {
                clsdbo_User.Status = null;
            }
            else
            {
                clsdbo_User.Status = ddlCP_Status.SelectedValue;
            }

            List<ListItem> selected = new List<ListItem>();

            foreach (ListItem item in ddlLocation_Region.Items)
            {
                if (item.Selected) selected.Add(item);
            }

            string doc = string.Empty;
            bool flag = false;
            foreach (ListItem str in selected)
            {
                if (selected.Count == 1)
                {
                    doc = str.Text;
                }
                else
                {
                    if (!flag)
                    {
                        doc = str.Text;
                        flag = true;
                    }
                    else
                    {
                        doc += "," + str.Text;
                    }
                }
            }
            clsdbo_User.Region = doc;


            clsdbo_User.User_Group_ID = "CP Meiji";

        }
        catch (Exception)
        {

        }
    }

    private void SearchSubmit()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string user_id = (ddlSearchFirst_Name.SelectedIndex == 0 ? string.Empty : ddlSearchFirst_Name.SelectedItem.Value);
            string role = (ddlSearchRole.SelectedIndex == 0 ? string.Empty : ddlSearchRole.SelectedItem.Value);
            string PositionKey = (ddlSearchPosition.SelectedIndex == 0 ? string.Empty : ddlSearchPosition.SelectedItem.Value);
            string region = string.Empty;

            foreach (ListItem item in ddlSearchState.Items)
            {
                if (item.Selected && item.Text != "==ระบุ==")
                {
                    if (string.IsNullOrEmpty(region))
                    {
                        region = item.Text;
                    }
                    else
                    {
                        region = region + "," + item.Text;
                    }
                }
            }

            List<dbo_UserClass> dt = dbo_UserDataClass.Search(user_id, string.Empty, PositionKey, string.Empty, ddlSearchStatus.Text, "CP Meiji", role, string.Empty, null, string.Empty, region);

            if (dt.Count > 0)
            {
                GridViewUser.DataSource = dt;
                GridViewUser.DataBind();
                showPanel("pnlGrid");

                GridViewUser.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewUser.Visible = false;
                pnlNoRec.Visible = true;
            }
        }
        catch (Exception)
        {

        }
    }
    #endregion

    #region GridView Row Command
    protected void GridViewUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            if (e.CommandName == "View")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                User_ID_View = lnkView.CommandArgument;
                hdfUser_ID.Value = lnkView.CommandArgument;
                GetDetailsDataToForm(User_ID_View.Trim(), "View");
            }

        }
        catch (Exception)
        {

        }
    }

    protected void GridViewUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        LinkButton lnkView = (LinkButton)GridViewUser.Rows[e.RowIndex].FindControl("lnkBDelete");
        string User_ID = lnkView.CommandArgument;

        bool sucess = dbo_UserDataClass.Delete(User_ID);
        if (sucess)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");
            SearchSubmit();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ไม่สามารถลบข้อมูล , กรุณาติดต่อผู้ดูแลระบบ");
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
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewUser.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewUser.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewUser.PageIndex)
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
            int currentPage = GridViewUser.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewUser.PageCount.ToString();

        }
    }
    #endregion

}