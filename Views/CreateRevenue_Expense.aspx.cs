using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreateRevenue_Expense : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Amount");
            dt.Columns.Add("Description");

            dt.Rows.Add("100.00", "-");
            dt.Rows.Add("100.00", "-");

            GridViewInstallation.DataSource = dt;
            GridViewInstallation.DataBind();

            GridView1.DataSource = dt;
            GridView1.DataBind();
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