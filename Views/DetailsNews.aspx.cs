#region Using
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_DetailsNews : System.Web.UI.Page
{
    #region Private Variable
    CultureInfo ThaiCulture = new CultureInfo("th-TH");
    string NewsID = string.Empty;
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["News_ID"] != null)
        {
            NewsID = (string)Session["News_ID"];
        }

        //NewsID = Request.QueryString["NewsID"];

        if (!IsPostBack)
        {
            loadDDL();
            LoadData();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Session["News_ID"] = NewsID;
        Response.Redirect("../Views/EditNews.aspx?PRM=Edit");

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnBackTo_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Views/NewsList.aspx");

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
    #endregion

    #region Method
    private void loadDDL()
    {
        DataSet ds = dbo_NewsDataClass.GetAgent();

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlAgentName.DataSource = ds.Tables[0];
            ddlAgentName.DataTextField = "Agent_Name";
            ddlAgentName.DataValueField = "Agent_Name";
            ddlAgentName.DataBind();

            ddlAgentName.Items.Insert(0, new ListItem("==ไม่ระบุ==", string.Empty));
            ddlAgentName.SelectedIndex = 0;
        }
    }

    private void LoadData()
    {
        DataSet NewsDetails = dbo_NewsDataClass.GetNewsDetails(NewsID);

        ddlNewsType.SelectedValue = NewsDetails.Tables[0].Rows[0]["News_Type"].ToString();
        ddlAgentName.SelectedValue = NewsDetails.Tables[0].Rows[0]["Agent_Name"].ToString();
        txtSubject.Text = NewsDetails.Tables[0].Rows[0]["Subject"].ToString();
        txtContent.Text = NewsDetails.Tables[0].Rows[0]["Content"].ToString();
        txtDownloadFile.Text = NewsDetails.Tables[0].Rows[0]["Content_FileName"].ToString();
        txtVideo.Text = NewsDetails.Tables[0].Rows[0]["VDO_Hyperlink"].ToString();
        txtStart_Date.Text = Convert.ToDateTime(NewsDetails.Tables[0].Rows[0]["Start_Date"].ToString()).ToString("dd/MM/yyyy");
        txtEnd_Date.Text = Convert.ToDateTime(NewsDetails.Tables[0].Rows[0]["End_Date"].ToString()).ToString("dd/MM/yyyy");

        if (!DBNull.Value.Equals(NewsDetails.Tables[0].Rows[0]["Photo"]))
        {
            byte[] bytes = (Byte[])NewsDetails.Tables[0].Rows[0]["Photo"];
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgFile.ImageUrl = "data:image/png;base64," + base64String;
        }
    }
    #endregion
}