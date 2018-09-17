using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_DeductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<dbo_DeductClass> item = dbo_DeductDataClass.GetDeduct();


            GridViewDeduct.DataSource = item;
            GridViewDeduct.DataBind();
        }
    }

    protected void GridViewDeduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridViewDeduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewDeduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridViewDeduct_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridViewDeduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridViewDeduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}