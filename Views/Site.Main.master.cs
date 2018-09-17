using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site_Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
            string User_ID = string.Empty;

            if (Session["LOGIN_USER_ID"] == null)
            {
                Response.Redirect("../Login.aspx");
            }

            if (Request.Cookies["User_ID"] != null)
            {
                User_ID = Request.Cookies["User_ID"].Value;
            }

            //if (Request.Cookies["User_ID"] == null)
            //{
            //    Response.Redirect("../Login.aspx");
            //}
            //else
            //{
            //    User_ID = Request.Cookies["User_ID"].Value;
            //}

            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            if (currentPage != "")
            {
                string UserGroup = string.Empty;
                dbo_UserClass user_class = new dbo_UserClass();
                if (Session["user_class"] == null)
                {
                    user_class = dbo_UserDataClass.Select_Record(User_ID);
                    Session["user_class"] = user_class;
                    UserGroup = user_class.User_Group_ID;
                }
                else
                {
                    user_class = (dbo_UserClass)Session["user_class"];
                    UserGroup = user_class.User_Group_ID;
                }
                string CV_Code = user_class.CV_CODE;
                if(CV_Code == "")
                {
                    CV_Code = "ซีพี-เมจิ";
                }
                string Position = user_class.Position;
                string FullName = user_class.First_Name + " " + user_class.Last_Name;
                string EmpID = user_class.User_ID;

                //lbl_user_id.Text = string.Format("รหัสพนักงาน:{0} {1} ตำแหน่ง:{2}", EmpID, FullName, Position);
                lbl_user_id.Text = string.Format("{0} : {1}", EmpID, FullName);
                lbl_Position.Text = Position;
                //dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);               

                List<dbo_RolePermissionClass> permission = dbo_RolePermissionDataClass.Search(user_class.Role_ID);

                foreach (dbo_RolePermissionClass functionname in permission)
                {
                    string li_name = string.Format("li{0}", functionname.Function_Name);
                    try
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)menu.FindControl(li_name);

                        if (li == null)
                        {
                            string a = li_name;
                        }

                        li.Visible = true;
                        li.Parent.Visible = true;

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            //
        }
    }

    protected void logout_Click(object sender, EventArgs e)
    {
        CommonDataClass.User_ID = string.Empty;
        Session.Remove("user_class");
        Session.Remove("LOGIN_USER_ID");
        Response.Redirect("../Login.aspx");
    }


    [System.Web.Services.WebMethod]
    public static string MyMethod1(string Param1, string Param2)
    {
        try
        {
            //Do here server event  
            // Button1.Text = "";



            // List<dbo_UserClass> user = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);




            //   a();
            return "aaaaa";
        }
        catch (Exception)
        {
            //throw;
            return string.Empty;
        }
    }

}
