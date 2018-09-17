using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_InstallationList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //,,,

            DataTable dt = new DataTable();
            dt.Columns.Add("Installation_Detail");
            dt.Columns.Add("Installation_Type");
            dt.Columns.Add("Description");
            dt.Columns.Add("Installation_Amount");
            dt.Columns.Add("Installation_Amount1");
            dt.Rows.Add("รายละเอียดการผ่อน 1", "ประเภท 1", "รายละเอียด 1", "400.00", "100.00");
            dt.Rows.Add("รายละเอียดการผ่อน 1", "ประเภท 1", "รายละเอียด 1", "400.00", "100.00");

            GridViewInstallation.DataSource = dt;
            GridViewInstallation.DataBind();
            ViewState["ShowFooter"] = false;
        }
        else
        {
            GridViewInstallation.ShowFooter = (bool)ViewState["ShowFooter"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Installation_Detail");
            dt.Columns.Add("Installation_Type");
            dt.Columns.Add("Description");
            dt.Columns.Add("Installation_Amount");
            dt.Columns.Add("Installation_Amount1");
            dt.Rows.Add("รายละเอียดการผ่อน 1", "ประเภท 1", "รายละเอียด 1", "400.00", "100.00");
            dt.Rows.Add("รายละเอียดการผ่อน 1", "ประเภท 1", "รายละเอียด 1", "400.00", "100.00");

            GridViewInstallation.DataSource = dt;
            GridViewInstallation.DataBind();
        }
    }

    public void ButtonNew_Click(object sender, System.EventArgs e)
    {
        ViewState["ShowFooter"] = true;
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}