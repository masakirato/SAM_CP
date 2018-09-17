using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreditPaymentList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<dbo_CreditClass> creditList = dbo_CreditDataClass.GetCredit();
            GridViewCredit.DataSource = creditList;
            GridViewCredit.DataBind();
        }
    }



    protected void GridViewCredit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowCredit")
        {
            int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
            GridViewCredit.Rows[RowIndex].Cells[2].ColumnSpan = 6;

            GridViewCredit.Rows[RowIndex].Cells[3].Visible = false;
            GridViewCredit.Rows[RowIndex].Cells[4].Visible = false;
            GridViewCredit.Rows[RowIndex].Cells[5].Visible = false;
            GridViewCredit.Rows[RowIndex].Cells[6].Visible = false;
            GridViewCredit.Rows[RowIndex].Cells[7].Visible = false;
          
            Button btn = (Button)GridViewCredit.Rows[RowIndex].FindControl("btnCustomerName");
            Label _lblCustomerName = (Label)GridViewCredit.Rows[RowIndex].FindControl("lblCustomerName");


            if (btn.Text == "จ่ายหนี้เครดิต")
            {
                GridView gv = (GridView)GridViewCredit.Rows[RowIndex].FindControl("GridViewCustomer");
                _lblCustomerName.Visible = false;

                List<dbo_CreditPaymentClass> creditList = dbo_CreditPaymentDataClass.GetCreditPayment();

                gv.DataSource = creditList;
                gv.DataBind();
                btn.Text = "ตกลง";

            }

        }
    }

    protected void GridViewCredit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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

    protected void GridViewCustomer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridViewCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridViewCustomer_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridViewCustomer_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridViewCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridViewCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}