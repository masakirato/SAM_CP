using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using log4net;



public partial class Login : System.Web.UI.Page
{
    //ConnectionManager conMgm = new ConnectionManager();

    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Page.IsPostBack)
        //{
        //    conMgm.openConnection();
        //    divError.Visible = false;
        //}
        //ConnectionManager conMgm = new ConnectionManager();
        /////SqlCommand com;
        ////com.CommandType = System.Data.CommandType.Text;
        ////conMgm.openConnection();
        //DataSet ds = SqlHelper.ExecuteDataset(conMgm.getConnectionString(), System.Data.CommandType.Text, @"Select * from M_ROLE");
        //if (ds.Tables.Count > 0)
        //{
        //    String a = "";
        //}


        if (!IsPostBack)
        {
            try
            {


            }
            catch (Exception ex) { logger.Error(ex.Message); }
            {

            }

        }
    }




    protected void btnLogin_Click(object sender, EventArgs e)
    {
        logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {

            if (Session["LOGIN_USER_ID"] != null)
            {
                Session.Remove("LOGIN_USER_ID");
            }

            List<dbo_LoginHistoryClass> item = dbo_LoginHistoryDataClass.Search(txtUser.Text);
            logger.Info("item.Count " + item.Count);




            //if (item.Count >= 3)
            if (item.Count >= 5)
            {
                item.OrderByDescending(f => f.Login_Time);

                TimeSpan? result = DateTime.Now - item[0].Login_Time;
                //  int hours = result.Hours;
                int minutes = result.Value.Minutes;


                //if (minutes >= 10)
                if (minutes >= 1)
                {
                    dbo_LoginHistoryClass login = new dbo_LoginHistoryClass();
                    login.Status = "Invalid Password(reset)";
                    login.User_ID = txtUser.Text;


                    dbo_LoginHistoryDataClass.Update(login);

                    login.Status = "reset";
                    login.Login_Time = DateTime.Now;
                    dbo_LoginHistoryDataClass.Add(login);


                }
                else
                {
                    //Show("Lockout effective period 10 minutes");
                    Show("Lockout effective period 1 minutes");
                    return;
                }

            }


            string Username = string.Empty;
            string Password = string.Empty;

            Username = txtUser.Text;
            Password = txtPassword.Text;

            logger.Info("Username " + Username + " Password " + Password);
            dbo_UserClass user_class = dbo_UserDataClass.VerifyPassword(Username, Password);
            int days = 0;
            if (user_class != null)
            {
                if (user_class.Status == "Active")
                {
                    List<dbo_PasswordHistoryClass> history = dbo_PasswordHistoryDataClass.Search(user_class.Username).OrderBy(f => f.Last_Password_Change_Or_Reset).ToList();

                    if (history.Count < 1)
                    {
                        dbo_PasswordHistoryClass password = new dbo_PasswordHistoryClass();
                        password.Last_Password_Change_Or_Reset = DateTime.Now;
                        password.Password = Password;
                        password.User_ID = Username;
                        dbo_PasswordHistoryDataClass.Add(password);
                    }



                    logger.Info("history.Count " + history.Count);

                    if (history.Count != 0)
                    {
                        DateTime? his_date = history[history.Count - 1].Last_Password_Change_Or_Reset;
                        TimeSpan? result_ = DateTime.Now - his_date;
                        days = result_.Value.Days;

                        logger.Info("days " + days);
                    }
                    else
                    {
                        days = 0;
                    }

                    //TimeSpan? result_ = DateTime.Now - his_date;

                    //int days = result_.Value.Days;

                    int cal_expire = 90 - days;

                    if (cal_expire <= 0)
                    {
                        // Show("รหัสผ่านของท่านหมดอายุ");

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM",
                          "alert('รหัสผ่านของท่านหมดอายุ');window.location.href='ForgotPassword.aspx'", true);

                    }


                    Request.Cookies.Remove("User_ID");


                    if (Request.Cookies["User_ID"] != null && !string.IsNullOrEmpty(Request.Cookies["User_ID"].Value))
                    {
                        Response.Cookies.Set(Request.Cookies["User_ID"]);
                    }
                    else
                    {
                        Response.Cookies.Set(new HttpCookie("User_ID", user_class.User_ID));
                    }

                    Response.Cookies["User_ID"].Expires = DateTime.Now.AddDays(1);


                    dbo_LoginHistoryClass login = new dbo_LoginHistoryClass();
                    login.Status = "Invalid Password(reset)";
                    login.User_ID = txtUser.Text;

                    dbo_LoginHistoryDataClass.Update(login);

                    //login.User_ID = txtUser.Text;
                    login.Status = "Success";
                    login.Login_Time = DateTime.Now;

                    dbo_LoginHistoryDataClass.Add(login);

                    if (cal_expire <= 15)
                    {
                        //  Show(string.Format("รหัสผ่านจะหมดอายุภายใน {0} วัน กรุณาเปลี่ยนรหัสผ่านใหม่ก่อนวันหมดอายุ", cal_expire));

                        //  string script = string.Format("window.location.href='Views/Home.aspx'", "");

                        //==========================Old=====================================
                        //string baseurl = GetBaseUrl();
                        //string url = string.Format("{0}changepwd?username={1}", baseurl, Username);
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM",
                        //    "alert('รหัสผ่านจะหมดอายุภายใน " + cal_expire.ToString() + " วัน กรุณาเปลี่ยนรหัสผ่านใหม่ก่อนวันหมดอายุ');window.location.href='Views/Home.aspx'", true);

                        Session["LOGIN_USER_ID"] = user_class.User_ID;
                        string baseurl = GetBaseUrl();
                        string url = string.Format("{0}Views/Home.aspx", baseurl);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM",
                          "alert('รหัสผ่านจะหมดอายุภายใน " + cal_expire.ToString() + " วัน กรุณาเปลี่ยนรหัสผ่านใหม่ก่อนวันหมดอายุ');window.location.href='Views/Home.aspx'", true);
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM",
                        //    "alert('รหัสผ่านจะหมดอายุภายใน " + cal_expire.ToString() + " วัน กรุณาเปลี่ยนรหัสผ่านใหม่ก่อนวันหมดอายุ');window.location.href='" + url + "'", true);

                    }
                    else
                    {

                        Session["LOGIN_USER_ID"] = user_class.User_ID;
                        Response.Redirect("Views/Home.aspx");
                    }
                }
                else
                {
                    Show("ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบข้อมูลอีกครั้ง");
                }
            }
            else
            {

                Show("ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบข้อมูลอีกครั้ง");

                dbo_LoginHistoryClass login = new dbo_LoginHistoryClass();

                login.User_ID = txtUser.Text;
                login.Status = "Invalid Password";
                login.Login_Time = DateTime.Now;

                dbo_LoginHistoryDataClass.Add(login);
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }


    private string GetBaseUrl()
    {
        string Authority = Request.Url.GetLeftPart(UriPartial.Authority).TrimStart('/').TrimEnd('/');
        string ApplicationPath = Request.ApplicationPath.TrimStart('/').TrimEnd('/');

        // add trailing slashes if necessary  
        if (Authority.Length > 0)
        {
            Authority += "/";
        }

        if (ApplicationPath.Length > 0)
        {
            ApplicationPath += "/";
        }

        string url = string.Format("{0}{1}", Authority, ApplicationPath);
        return url;
    }


    public void Show(string message)
    {
        logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
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

    #region Method
    private bool Authenticate()
    {
        try
        {
            //StringBuilder strSQL = new StringBuilder();
            //strSQL.AppendFormat(@"Select * from M_USER Where username='{0}' and password ='{1}'", txtUser.Value.Trim(), txtPassword.Value.Trim());
            //DataSet ds = SqlHelper.ExecuteDataset(conMgm.getConnection(), CommandType.Text, strSQL.ToString());

            //if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        catch (Exception ex)
        {

        }

        return false;
    }
    #endregion

    protected void LinkButtonForgotPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgotPassword.aspx");
    }
}