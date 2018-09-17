using log4net;
using System;
using System.Collections.Generic;
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

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    protected void Unnamed3_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ClearingNew");
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }
}