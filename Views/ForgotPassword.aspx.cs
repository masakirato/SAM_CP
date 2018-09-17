using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {

        string user_id = txtUserName.Text;

        dbo_UserClass user = dbo_UserDataClass.Select_Record(user_id);

        //dbo_UserDataClass.Search()

       // ScriptManager.RegisterStartupScript(this, this.GetType(), "ลืมรหัสผ่าน", "openModal();", true);
    }
}