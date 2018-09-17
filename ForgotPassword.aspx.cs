using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPassword : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


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

    private void TrySendMail()
    {
        try
        {

            string encrypt_user_name = dbo_UserDataClass.Encrypt(txtUserName.Text);

            var fromAddress = "sam.cpmeiji@gmail.com";
            // var fromAddress = "panida@th.fujitsu.com";
            var toAddress = txtEmail.Text;
            //   Text!234
            const string fromPassword = "samcpmeiji1234";
            // const string fromPassword = "Text!234";
            string subject = string.Format("รีเซ็ตรหัสผ่านระบบ SAM ({0})", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            txtUserName.Text = "212689";

            string user_name = dbo_UserDataClass.Encrypt(txtUserName.Text);

            string baseurl = GetBaseUrl();
            string url = string.Format("{0}/changepwd?username={1}", baseurl, user_name);



            // string url = string.Format("http://192.168.104.89:8083/sam/changepwd?username={0}", user_name);


            string body = "กรุณากดลิงค์ข้างล่างเพื่อทำการรีเซ็ตรหัสผ่าน\n";
            body += url + "\n";
            // body += "https://sam.cpmeiji.com/changepwd \n";
            body += "หากพบข้อสงสัยกรุณาติดต่อเจ้าหน้าที่ CP-Meiji\n";
            body += "เบอร์โทรศัพท์: 02-4321234 \n";


            var smtp = new System.Net.Mail.SmtpClient();
            {
                //NetworkCredential network = new NetworkCredential();
                //network.UserName = fromAddress;
                //network.Password = fromPassword;
                //  network.Domain = "g07\\panida";

                //smtp.Host = "42469a7f-a45c-4d66-901e-25406d9c7da3@th.fujitsu.com";
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;

            }
            smtp.Send(fromAddress, toAddress, subject, body);


            Show("กรุณาตรวจสอบอีเมล์ของคุณ ระบบได้ทำการส่งอีเมล์พร้อมลิงค์ในการรีเซ็ตรหัสผ่านแล้ว กรุณาดำเนินการภายใน 24 ชั่วโมง");

            Response.Redirect("../Login.aspx");


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            Show(ex.Message.Trim());
        }
        

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
            , string.Empty, string.Empty, string.Empty, string.Empty, null, txtUserName.Text, string.Empty);


            logger.Info("users.Count " + users.Count);



            if (users.Count > 0)
            {
                if (users[0].Email == null)
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("อีเมล์ของคุณไม่มีในระบบ กรุณาตรวจสอบข้อมูลอีกครั้ง");
                }
                else
                {
                    logger.Info("users[0].Email " + users[0].Email);
                    logger.Info("txtEmail.Text " + txtEmail.Text);

                    if (users[0].Email != txtEmail.Text)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("อีเมล์ที่ระบุไม่ถูกต้อง กรุณาตรวจสอบข้อมูลอีกครั้ง");
                    }
                    else
                    {
                        try
                        {
                            string encrypt_user_name = dbo_UserDataClass.Encrypt(txtUserName.Text);

                            logger.Info("encrypt_user_name " + encrypt_user_name);


                            var fromAddress = "samadmin@cpmeiji.com"; // "fthnew01@gmail.com";
                            var toAddress = txtEmail.Text;

                            const string fromPassword = "44313450";// "fth!2017";
                            string subject = string.Format("รีเซ็ตรหัสผ่านระบบ SAM ({0})", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                            string user_name = dbo_UserDataClass.Encrypt(txtUserName.Text);

                            string baseurl = GetBaseUrl();
                            string url = string.Format("{0}changepwd?username={1}", baseurl, user_name);

                            logger.Info("url " + url);

                            // string url = string.Format("https://sam.cpmeiji.com/changepwd?username={0}", user_name);


                            string body = "กรุณากดลิงค์ข้างล่างเพื่อทำการรีเซ็ตรหัสผ่าน\n";
                            body += url + "\n";
                            // body += "https://sam.cpmeiji.com/changepwd \n";
                            body += "หากพบข้อสงสัยกรุณาติดต่อเจ้าหน้าที่ CP-Meiji\n";
                            body += "เบอร์โทรศัพท์: 02-4321234 \n";


                            var smtp = new System.Net.Mail.SmtpClient();
                            {
                                //smtp.Host = "42469a7f-a45c-4d66-901e-25406d9c7da3@th.fujitsu.com";
                                smtp.Host = "imsva-rlocal.cpf.co.th";// "smtp.gmail.com";
                                smtp.Port = 25;// 587;

                                //   smtp.EnableSsl = true;
                                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);


                                smtp.Timeout = 20000;
                            }



                            logger.Info("-->smtp.Send(fromAddress, toAddress, subject, body);");
                            logger.Info("fromAddress " + fromAddress);
                            logger.Info("toAddress " + toAddress);
                            logger.Info("fromPassword " + fromPassword);


                            smtp.Send(fromAddress, toAddress, subject, body);
                            logger.Info("<--smtp.Send(fromAddress, toAddress, subject, body);");

                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                            Show("กรุณาตรวจสอบอีเมล์ของคุณ ระบบได้ทำการส่งอีเมล์พร้อมลิงค์ในการรีเซ็ตรหัสผ่านแล้ว กรุณาดำเนินการภายใน 24 ชั่วโมง");

                        }
                        catch (Exception ex)
                        {
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                            logger.Error(ex.Message);
                            string inex = ex.InnerException.Message.ToString();
                            //Show(ex.InnerException.ToString());
                            //Show(inex.Length.ToString()+","+ex.Message.Length);
                            Show(ex.Message.Trim());
                        }

                    }
                }

            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ชื่อผู้ใช้งานไม่ถูกต้อง กรุณาตรวจสอบข้อมูลอีกครั้ง");
            }

        }
        catch (Exception ex)
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            logger.Error(ex.Message);
            Show(ex.Message.Trim());
        }

        
        #region
        // SendMail();
        //        string script = @"
        //        swal(   {   title: ""กรุณาตรวจสอบอีเมล์ของคุณ!"",
        //                    text: ""ระบบได้ทำการส่งอีเมล์พร้อมลิงค์ในการรีเซ็ตรหัสผ่านแล้ว \nกรุณาดำเนินการภายใน 24 ชั่วโมง"",
        //                    html: true
        //                });";


        //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);



        //        Response.Redirect("../Login.aspx");
        #endregion
    }

    protected void SendMail()
    {
        var fromAddress = "panida@th.fujitsu.com";// Gmail Address from where you send the mail
       // var toAddress = "sompong.a.kumboonma@gmail.com";
        var toAddress = "keng.masaki@gmail.com";
        const string fromPassword = "Text!234";//Password of your gmail address
        string subject = string.Format("รีเซ็ตรหัสผ่านระบบ SAM ({0})", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));


        string user_name = dbo_UserDataClass.Encrypt(txtUserName.Text);

        string url = string.Format("https://sam.cpmeiji.com/changepwd?username={0}", user_name);


        string body = "กรุณากดลิงค์ข้างล่างเพื่อทำการรีเซ็ตรหัสผ่าน\n";

        body += url + "\n";

        //body += "https://sam.cpmeiji.com/changepwd+username?+ \n";
        body += "หากพบข้อสงสัยกรุณาติดต่อเจ้าหน้าที่ CP-Meiji\n";
        body += "เบอร์โทรศัพท์: 02-4321234 \n";




        var smtp = new System.Net.Mail.SmtpClient();
        {
            smtp.Host = "42469a7f-a45c-4d66-901e-25406d9c7da3@th.fujitsu.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
            smtp.Timeout = 20000;
        }
        smtp.Send(fromAddress, toAddress, subject, body);
    }




    protected void btnCancel_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        Response.Redirect("Login.aspx");
    }

    public void Show(string message)
    {
        try
        {
            logger.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info(message);

            string script;
            string cleanMessage = message.Replace("'", "\'");
            // Page page = HttpContext.Current.CurrentHandler as Page;
            if (message == "กรุณาตรวจสอบอีเมล์ของคุณ ระบบได้ทำการส่งอีเมล์พร้อมลิงค์ในการรีเซ็ตรหัสผ่านแล้ว กรุณาดำเนินการภายใน 24 ชั่วโมง")
            {
                script = string.Format("alert('{0}');window.location ='Login.aspx';", cleanMessage);
            }
            else
            {
                script = string.Format("alert('{0}');", cleanMessage);
            }
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
}