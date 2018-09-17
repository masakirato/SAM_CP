using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_BenefitList : System.Web.UI.Page
{

    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Benefit_Date");
                dt.Columns.Add("Benefit_Name");
                dt.Columns.Add("Beneficiary");
                dt.Columns.Add("Relationship");
                dt.Columns.Add("Benefit_Amount");

                dt.Rows.Add("26-04-2560", "ผลประโยชน์ 1", "สมพงษ์", "บุตร", "1000.00");
                dt.Rows.Add("26-04-2560", "ผลประโยชน์ 1", "สมพงษ์", "บุตร", "1000.00");

                GridViewBenefit.DataSource = dt;
                GridViewBenefit.DataBind();


            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

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