using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_NewsPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {


                string News_ID = Request.QueryString["News_ID"];
                hdfNEWS_ID.Value = News_ID;
                // Session.Remove("News_ID");

                dbo_NewsClass2 _news = dbo_NewsDataClass2.Select_Record(News_ID);

                if (!string.IsNullOrEmpty(_news.Content_FileName))
                {
                    lnkBContentFile.Text = _news.Content_FileName;
                    lnkBContentFile.CommandArgument = News_ID;
                    
                }
                else
                {
                    lnkBContentFile.Visible = false;
                }

                if (_news.Photo_MemoryStream != null)
                {
                    imgNewsPhoto.ImageUrl = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(_news.Photo_MemoryStream, 0, _news.Photo_MemoryStream.Length));
                    
                }
                txtSubjectDiv.InnerHtml = _news.Subject;
                // txtSubjectH1.Text = 
                txtContentDiv.InnerHtml = _news.Content;

                txtStart_Effective_Date.Text = _news.Start_Date.Value.ToShortDateString();
                txtEnd_Effective_Date.Text = _news.End_Date.Value.ToShortDateString();

                // linkVidoes.Attributes.Add("href", _news.VDO_Hyperlink);
                linkVidoes.HRef = _news.VDO_Hyperlink;
                linkVidoes.InnerText = _news.VDO_Hyperlink;
                gg.Src = _news.VDO_Hyperlink;

                dbo_ReadNewsClass readnew = new dbo_ReadNewsClass();

                readnew.User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                readnew.News_ID = News_ID;
                readnew.Read_Date = DateTime.Now;

                dbo_ReadNewsDataClass.Add(readnew);
            }
            catch (Exception ex)
            {

            }
        }
    }

    protected void lnkBContentFile_Command(object sender, CommandEventArgs e)
    {
        try
        {
            dbo_NewsClass2 _news = dbo_NewsDataClass2.Select_Record(hdfNEWS_ID.Value);

            if (string.IsNullOrEmpty(_news.Content_FileName) || string.IsNullOrEmpty(_news.Content_FileType))
            {

            }
            else
            {

                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = _news.Content_FileType;
                Response.AddHeader("content-disposition", "attachment;filename=" + _news.Content_FileName);
                Response.BinaryWrite(_news.Content_File);
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception ex)
        {

        }
    }
}