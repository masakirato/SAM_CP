using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreditList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("Credit_Date");
            dt.Columns.Add("Credit_Amount");
            dt.Columns.Add("Status");


            dt.Rows.Add("ลูกค้า 1", "24/04/2560", "1000.00", "Active");
            dt.Rows.Add("ลูกค้า 1", "24/04/2560", "1000.00", "Active");

            GridViewCredit.DataSource = dt;
            GridViewCredit.DataBind();


        }
    }

    protected void GridViewCredit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridViewCredit_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewCredit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridViewCredit_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridViewCredit_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridViewCredit_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public void btnCreditPayment_Click(object sender, System.EventArgs e)
    {

    }
}