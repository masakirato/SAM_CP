#region Using
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_NewsList : System.Web.UI.Page
{
    private string PRM;
    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PRM = Request.QueryString["PRM"];

        if (Session["CreateNews_Status"] != null)
        {
            LoadData("");
            Session.Remove("CreateNews_Status");
        }

        if (!IsPostBack)
        {
            if (Session["Save_Edit"] != null)
            {
                Show("บันทึกสำเร็จ");
                Session.Remove("Save_Edit");
            }
            else if (Session["SaveNew"] != null)
            {
                Show("บันทึกสำเร็จ");
                Session.Remove("SaveNew");
            }
                btnOK_Click(sender, e);
          
        }
    }

    protected void btnCreatNews_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        Response.Redirect("../Views/CreateNews.aspx");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        LoadData(txtNewsSearch.Text.ToString());
        //System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();
        txtNewsSearch.Text = string.Empty;
        txtCV_Code.Text = string.Empty;
        gdvNews.Visible = false;
        pnlNoRec.Visible = false;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
    #endregion

    #region Methods
    private void LoadData(string searchText)
    {
        DataSet ds = dbo_NewsDataClass.GetNews(searchText,txtCV_Code.Text);

        if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
        {
           
            gdvNews.DataSource = ds;
            gdvNews.DataBind();

            gdvNews.Visible = true;
            pnlNoRec.Visible = false;
        }
        else
        {
            gdvNews.Visible = false;
            pnlNoRec.Visible = true;
        }
    }

    private void ClearData()
    {

        if(gdvNews.Rows.Count > 0)
        {
            gdvNews.DataSource = null;
            gdvNews.DataBind();
        }
      
    }

    public void Show(string message)
    {
        try
        {
            string cleanMessage = message.Replace("'", "\'");
            string script = string.Format("alert('{0}');", cleanMessage);

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region GridView Events
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = gdvNews.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        gdvNews.PageIndex = pageList.SelectedIndex;
        btnOK_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void gdvNews_DataBound(object sender, EventArgs e)
    {
        GridViewRow pagerRow = gdvNews.BottomPagerRow;

        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {
            for (int i = 0; i < gdvNews.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                if (i == gdvNews.PageIndex)
                {
                    item.Selected = true;
                }
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = gdvNews.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + gdvNews.PageCount.ToString();

        }
    }

    protected void gdvNews_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
            {
                LinkButton lnkBDeleteNews = (LinkButton)e.Row.FindControl("lnkBDeleteNews");
                LinkButton lnkBSubject = (LinkButton)e.Row.FindControl("lnkBSubject");
                if (string.IsNullOrEmpty(lnkBSubject.Text))
                {
                    lnkBDeleteNews.Enabled = false;
                    lnkBDeleteNews.CssClass = "btn disabled";
                }
            }
        }
    }

    protected void gdvNews_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)gdvNews.Rows[e.RowIndex];
        string lblID = ((HiddenField)row.FindControl("hdfNews_ID")).Value.ToString();
        bool status = dbo_NewsDataClass.DeleteNews(lblID);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        Show("ลบข้อมูลสำเร็จ");
        LoadData("");
    }

    protected void gdvNews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DownloadContentFile":
                {
                    int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
                    string newsID = ((HiddenField)gdvNews.Rows[RowIndex].FindControl("hdfNews_ID")).Value.ToString();
                    DataSet dsDownloadFile = dbo_NewsDataClass.GetDownloadFile(newsID);

                    Byte[] bytes = (Byte[])dsDownloadFile.Tables[0].Rows[0]["Content_File"];
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = dsDownloadFile.Tables[0].Rows[0]["Content_FileType"].ToString();
                    Response.AddHeader("content-disposition", "attachment;filename=" + dsDownloadFile.Tables[0].Rows[0]["Content_FileName"].ToString());
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                    break;
                }
            case "OpenDetailsNews":
                {
                    
                    int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
                    int PageIndex = gdvNews.PageIndex;
                    var calindex = (RowIndex) % 10;
                    string newsID = ((HiddenField)gdvNews.Rows[calindex].FindControl("hdfNews_ID")).Value.ToString();

                    Session["News_ID"] = newsID;
                    Response.Redirect("../Views/DetailsNews.aspx");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    #endregion
   
}