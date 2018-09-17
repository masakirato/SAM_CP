using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreateUserCPMeiji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Edit();
        }
    }



    private void Edit()
    {
        TextboxUserID.Text = "100001";
        txtFirst_Name.Text = "สมพงษ์";
        TextboxFirst_Name_Eng.Text = "Sompong";
        txtEmail.Text = "sompong@mail";

        TextboxUserID.Enabled = false;
        DropDownListTitle_ID.Enabled = false;
        txtFirst_Name.Enabled = false;
        txtLast_Name.Enabled = false;
        TextboxFirst_Name_Eng.Enabled = false;
        TextboxLast_Name_Eng.Enabled = false;
        txtHome_Phone_No.Enabled = false;
        txtMobile.Enabled = false;
        txtPosition.Enabled = false;
        txtSection.Enabled = false;
        txtDivision.Enabled = false;
        txtManager.Enabled = false;
        DropDownUserGroup.Enabled = false;
        DropDownListRole.Enabled = false;
        txtUser_Name.Enabled = false;
        txtPassword.Enabled = false;
        txtEmail.Enabled = false;
        txtApproval.Enabled = false;
        txtStatus.Enabled = false;
        DropDownListShowDashboard.Enabled = false;




    }

    public void btnSave_Click(object sender, System.EventArgs e)
    {

    }

    public void btnCancel_Click(object sender, System.EventArgs e)
    {

    }

}