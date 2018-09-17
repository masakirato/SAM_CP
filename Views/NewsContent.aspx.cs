using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_NewsContent : System.Web.UI.Page
{
    CultureInfo ThaiCulture = new CultureInfo("th-TH");
    string NewsID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        NewsID = Request.QueryString["NewsID"];

        if (!IsPostBack)
        {
            LoadData();
        }

        this.btnClose.Attributes.Add("OnClick", "self.close()");
    }

    private void LoadData()
    {
        DataSet NewsDetails = dbo_NewsDataClass.GetNewsDetails(NewsID);

        txtSubject.Text = NewsDetails.Tables[0].Rows[0]["Subject"].ToString();
        txtContent.Text = NewsDetails.Tables[0].Rows[0]["Content"].ToString();
        txtStart_Date.Text = Convert.ToDateTime(NewsDetails.Tables[0].Rows[0]["Start_Date"].ToString()).ToString("dd/MM/yyyy");
        txtEnd_Date.Text = Convert.ToDateTime(NewsDetails.Tables[0].Rows[0]["End_Date"].ToString()).ToString("dd/MM/yyyy");

        if (!DBNull.Value.Equals(NewsDetails.Tables[0].Rows[0]["Photo"]))
        {
            byte[] Imgbytes = (Byte[])NewsDetails.Tables[0].Rows[0]["Photo"];
            string ImgStr = Convert.ToBase64String(Imgbytes, 0, Imgbytes.Length);
            imgFile.ImageUrl = "data:image/png;base64," + ImgStr;
        }

        lnkBAttachFile.Text = NewsDetails.Tables[0].Rows[0]["Content_FileName"].ToString();

        if (!string.IsNullOrEmpty(NewsDetails.Tables[0].Rows[0]["VDO_Hyperlink"].ToString()))
        {
            string videoURL = NewsDetails.Tables[0].Rows[0]["VDO_Hyperlink"].ToString();
            string VideoID = videoURL.Substring(videoURL.LastIndexOf("v=") + 2);
            lblShowYouTubeVideo.Text = "<object width='425' height='355'><param name='movie' value='http://www.youtube.com/v/" + VideoID + "'></param><param name='wmode' value='transparent'></param><embed src='http://www.youtube.com/v/" + VideoID + "' type='application/x-shockwave-flash' wmode='transparent' width='420' height='315'></embed></object>";
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }

    protected void lnkBAttachFile_Click(object sender, EventArgs e)
    {
        DataSet dsDownloadFile = dbo_NewsDataClass.GetDownloadFile(NewsID);

        Byte[] bytes = (Byte[])dsDownloadFile.Tables[0].Rows[0]["Content_File"];
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = dsDownloadFile.Tables[0].Rows[0]["Content_FileType"].ToString();
        Response.AddHeader("content-disposition", "attachment;filename=" + dsDownloadFile.Tables[0].Rows[0]["Content_FileName"].ToString());
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}