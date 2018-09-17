using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class changepwd : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (!IsPostBack)
        {
           
            string user_name = Request.QueryString["username"];
            logger.Debug("user_name " + user_name);
            txtUserName.Value = dbo_UserDataClass.Decrypt(user_name);
            logger.Debug("txtUserName.Value " + user_name);

           // txtUserName.Value = "201567";
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {


            Regex r = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])");

            List<dbo_PasswordHistoryClass> item = dbo_PasswordHistoryDataClass.Search(txtUserName.Value);

            dbo_PasswordHistoryClass pass = item.OrderByDescending(f => f.Last_Password_Change_Or_Reset).Take(3).FirstOrDefault(f => f.Password == txtNewPassword.Text);

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("รหัสผ่านไม่ตรงกับยืนยันรหัสผ่าน");
            }
            else if (txtNewPassword.Text.Length < 8)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("รหัสผ่านควรมีอย่างน้อย 8 ตัวอักษร");
            }
            else if (!r.IsMatch(txtNewPassword.Text))
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("รหัสผ่านควรประกอบด้วยตัวเลขและตัวอักษร");
            }
            else if (pass != null)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("รหัสผ่านต้องไม่ซ้ำกันกับ 3 ครั้งที่ผ่านมา");
            }


            else
            {

                //  Page.Validate("ValidatePassword");

                //if (Page.IsValid)
                //{


                string Username = string.Empty;
                string Password = string.Empty;



                Username = txtUserName.Value;
                logger.Debug("Username " + Username);
                Password = txtConfirmPassword.Text;
                logger.Debug("Password " + Password);
                dbo_UserClass oclsdbo_User = new dbo_UserClass();

                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
               , string.Empty, string.Empty, string.Empty, string.Empty, null, txtUserName.Value, string.Empty);

                logger.Debug("users.Count " + users.Count);

                if (users.Count > 0)
                {
                    oclsdbo_User = users[0];
                    //dbo_UserDataClass.Select_Record(CommonDataClass.User_ID);
                    oclsdbo_User.Password = txtConfirmPassword.Text;

                    //   string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

                    bool success = false;


                    // success = dbo_UserDataClass.Update(oclsdbo_User, User_ID);


                    success = dbo_UserDataClass.UpdatePassword(oclsdbo_User, Username);


                    if (success)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("เปลี่ยนรหัสผ่านสำเร็จ");

                        dbo_PasswordHistoryClass password = new dbo_PasswordHistoryClass();
                        password.Last_Password_Change_Or_Reset = DateTime.Now;
                        password.Password = txtNewPassword.Text;
                        password.User_ID = txtUserName.Value;
                        dbo_PasswordHistoryDataClass.Add(password);


                        Request.Cookies.Remove("User_ID");

                        if (Request.Cookies["User_ID"] != null && !string.IsNullOrEmpty(Request.Cookies["User_ID"].Value))
                        {
                            // Response.Cookies.Remove();
                            Response.Cookies.Set(Request.Cookies["User_ID"]);
                        }
                        else
                        {
                            Response.Cookies.Set(new HttpCookie("User_ID", oclsdbo_User.User_ID));
                        }

                        Response.Cookies["User_ID"].Expires = DateTime.Now.AddDays(1);


                        dbo_LoginHistoryClass login = new dbo_LoginHistoryClass();
                        login.Status = "Invalid Password(reset)";
                        login.User_ID = oclsdbo_User.Username;
                        dbo_LoginHistoryDataClass.Update(login);

                        login.Status = "Success";
                        login.Login_Time = DateTime.Now;

                        dbo_LoginHistoryDataClass.Add(login);

                        dbo_UserClass user_class = dbo_UserDataClass.VerifyPassword(Username, Password);
                        Session["LOGIN_USER_ID"] = user_class.User_ID;
                        //Response.Redirect("Views/Home.aspx");


                        //string script = @"swal(""เปลี่ยนรหัสผ่านสำเร็จ"");";
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ไม่พบชื่อผู้ใช้ในระบบ");
                }

            }
        }
        catch (Exception ex)
        {
            logger.Debug(ex.Message);
        }
    }

    /*
    private void UpdateRecord()
    {
        dbo_UserClass oclsdbo_User = new dbo_UserClass();
        dbo_UserClass clsdbo_User = new dbo_UserClass();

        oclsdbo_User.User_ID = System.Convert.ToString(Session["User_ID"]);
        oclsdbo_User = dbo_UserDataClass.Select_Record(CommonDataClass.User_ID);
        clsdbo_User.Password = txtConfirmPassword.Text;
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        bool success = false;

        success = dbo_UserDataClass.UpdatePassword(oclsdbo_User, User_ID);

        if (success)
        {
            Show("เปลี่ยนรหัสผ่านสำเร็จ");
        }
    }
    */

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtConfirmPassword.Text = string.Empty;
        txtNewPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
    }


    public void Show(string message)
    {
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        logger.Info(message);
        try
        {
            string script;
            string cleanMessage = message.Replace("'", "\'");
            if (message == "เปลี่ยนรหัสผ่านสำเร็จ")
            {
                script = string.Format("alert('{0}');window.location ='Views/Home.aspx';", cleanMessage);
            }
            else
            {
                script = string.Format("alert('{0}');", cleanMessage);
            }
            // Page page = HttpContext.Current.CurrentHandler as Page;
            //string script = string.Format("alert('{0}');", cleanMessage);
            //if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            //{
            //  page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {

        }
        //}
    }
}