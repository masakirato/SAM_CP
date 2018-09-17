using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ChangePassword : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        bool flag = false;

        try
        {
            if (!IsPostBack)
            {
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                dbo_UserClass user = dbo_UserDataClass.Select_Record(User_ID);

                txtUserName.Value = user.Username;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }


    public void Show(string message)
    {
        try
        {
            string script;
            string cleanMessage = message.Replace("'", "\'");
            // Page page = HttpContext.Current.CurrentHandler as Page;

            if (message == "เปลี่ยนรหัสผ่านสำเร็จ")
            {
                script = string.Format("alert('{0}');window.location ='Home.aspx';", cleanMessage);
            }
            else
            {
                script = string.Format("alert('{0}');", cleanMessage);
            }

            //string script = string.Format("alert('{0}');", cleanMessage);

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        //}
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
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
                //string script = @"swal(""กรุณาระบุรหัสผ่านอีกครั้ง"");";
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
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

                Username = CommonDataClass.User_ID;
                Password = txtConfirmPassword.Text;

                //dbo_UserClass user_class = dbo_UserDataClass.VerifyPassword(Username, Password);

                //if (user_class != null)
                //{


                dbo_UserClass oclsdbo_User = new dbo_UserClass();
                //dbo_UserClass clsdbo_User = new dbo_UserClass();

                //oclsdbo_User.User_ID = txtUserName.Value;

                List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
               , string.Empty, string.Empty, string.Empty, string.Empty, null, txtUserName.Value, string.Empty);


                if (users.Count > 0)
                {


                    oclsdbo_User = users[0];
                    //dbo_UserDataClass.Select_Record(CommonDataClass.User_ID);
                    oclsdbo_User.Password = txtConfirmPassword.Text;
                    string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                    bool success = false;
                    success = dbo_UserDataClass.UpdatePassword(oclsdbo_User, User_ID);

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

                        login.User_ID = oclsdbo_User.User_ID;
                        login.Status = "Success";
                        login.Login_Time = DateTime.Now;

                        dbo_LoginHistoryDataClass.Add(login);


                        //Response.Redirect("~/Views/Home.aspx");


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
                //UpdateRecord();
                //}
                //else
                //{
                //    Show("กรุณาตรวจสอบชื่อผู้ใช้งานและรหัสผ่านอีกครั้ง");
                //    //string script = @"swal(""กรุณาตรวจสอบชื่อผู้ใช้งานและรหัสผ่านอีกครั้ง."");";
                //    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
                //}
                //}
                //else
                //{
                //    Show("กรุณาตรวจสอบรหัสผ่านอีกครั้ง");
                //}
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
            dbo_UserClass oclsdbo_User = new dbo_UserClass();
            dbo_UserClass clsdbo_User = new dbo_UserClass();

            oclsdbo_User.User_ID = System.Convert.ToString(Session["User_ID"]);
            oclsdbo_User = dbo_UserDataClass.Select_Record(CommonDataClass.User_ID);
            clsdbo_User.Password = txtConfirmPassword.Text;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            bool success = false;
            success = dbo_UserDataClass.Update(oclsdbo_User, User_ID);

            if (success)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("เปลี่ยนรหัสผ่านสำเร็จ");
                //string script = @"swal(""เปลี่ยนรหัสผ่านสำเร็จ"");";
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtConfirmPassword.Text = string.Empty;
        txtNewPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
    }
}