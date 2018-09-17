using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ClearingList : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                //List<dbo_ClearingClass> itemClearing = dbo_ClearingDataClass.GetClearingList();
                //GridViewClearing.DataSource = itemClearing;
                //GridViewClearing.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    protected void ButtonAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CreateClearing.aspx");
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }


    protected void GridViewClearing_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string PO_Number = lnkView.CommandArgument;

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

}