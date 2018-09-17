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

public partial class Views_CreateNews : System.Web.UI.Page
{
    #region Private Variable
    CultureInfo ThaiCulture = new CultureInfo("th-TH");
    string User_ID = string.Empty;
    List<dbo_AgentClass> agent = new List<dbo_AgentClass>();
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["User_ID"] == null)
            Response.Redirect("../Login.aspx");
        else
            User_ID = Request.Cookies["User_ID"].Value;

        if (Session["fulFileUpload"] != null)
        {
            fulFileUpload = (FileUpload)Session["fulFileUpload"];
            Session.Remove("fulFileUpload");
        }

        if (Session["fulImgUpload"] != null)
        {
            fulImgUpload = (FileUpload)Session["fulImgUpload"];
            Session.Remove("fulImgUpload");
        }

        if (!IsPostBack)
        {
            ddlNewsType.SelectedIndex = 0;
            pnlAgent.Visible = false;
            ddlNewsType.Attributes.Add("onchange", "myApp.showPleaseWait();");
            loadDDL();
            ddlAgentName.Enabled = false;
            txtCV_Code.Enabled = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Page.Validate("NewsValidation");

        if (IsValid)
        {
            DateTime startDate = Convert.ToDateTime(txtStart_Date.Text);
            DateTime endDate = Convert.ToDateTime(txtEnd_Date.Text);
            DateTime today = DateTime.Today;
            if (startDate < today)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ไม่สามารถระบุวันที่เริ่มย้อนหลังได้");
            }
            else if (endDate < today)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ไม่สามารถระบุวันที่สิ้นสุดย้อนหลังได้");
            }
            else if (startDate > endDate)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ไม่สามารถระบุวันที่เริ่มมากกว่าวันที่สิ้นสุดได้");
            }
            else
            {

                string subject = txtSubject.Text;
                string newsType = ddlNewsType.SelectedValue.ToString();
                string contents = txtContent.Text;
                string agentName = string.Empty;
                string cvCode = string.Empty;

                if (!string.IsNullOrEmpty(ddlAgentName.SelectedValue.ToString()))
                {
                    agentName = ddlAgentName.SelectedItem.ToString();
                    cvCode = ddlAgentName.SelectedValue.ToString();
                }

                string vdoHyperLink = txtVideo.Text;
                string userName = User_ID;

                string fileName = string.Empty;
                string contenttype = string.Empty;
                string imgName = string.Empty;
                string imgType = string.Empty;
                Byte[] fileBytes = null;
                Byte[] imgBytes = null;


                if (fulFileUpload.HasFile)
                {
                    string filePath = fulFileUpload.PostedFile.FileName;
                    fileName = Path.GetFileName(filePath);
                    string fileExt = Path.GetExtension(fileName);
                    contenttype = Common.Common.ContentType(fileExt);

                    Stream fs = fulFileUpload.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    fileBytes = br.ReadBytes((Int32)fs.Length);
                }
                if (fulImgUpload.HasFile)
                {
                    string imgPath = fulImgUpload.PostedFile.FileName;
                    imgName = Path.GetFileName(imgPath);
                    string imgExt = Path.GetExtension(imgName);
                    imgType = Common.Common.ContentType(imgExt);

                    Stream imgST = fulImgUpload.PostedFile.InputStream;
                    BinaryReader imgBR = new BinaryReader(imgST);
                    imgBytes = imgBR.ReadBytes((Int32)imgST.Length);
                }

                bool status = false;

                if (newsType == "ทั่วไป")
                {
                    string newsID = generateNewsID();
                    status = dbo_NewsDataClass.InsertNews(newsID, newsType, agentName, subject, contents, fileName, contenttype, fileBytes,
                  vdoHyperLink, startDate, endDate, imgName, imgType, imgBytes, userName, ddlAgentName.SelectedValue);
                }
                else
                {
                    List<ListItem> selected = new List<ListItem>();

                    foreach (ListItem item in ddlAgentName.Items)
                    {
                        string newsID = generateNewsID();
                        if (item.Selected)
                        {
                            dbo_AgentClass agent = dbo_AgentDataClass.Select_Record(item.Value);

                            selected.Add(item);
                            status = dbo_NewsDataClass.InsertNews(newsID, newsType, agent.AgentName, subject, contents, fileName, contenttype, fileBytes,
                            vdoHyperLink, startDate, endDate, imgName, imgType, imgBytes, userName, item.Value);
                        }

                    }
                }

                if (status)
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                   // Show("บันทึกสำเร็จ!");

                    Session["CreateNews_Status"] = "true";
                    Session["SaveNew"] = "SaveNew";
                    Response.Redirect("../Views/NewsList.aspx");

                    Session.Remove("fulFileUpload");
                    Session.Remove("fulImgUpload");
                }
            }
        }
        else
        {
            //            string script = @"swal({
            //                    title: ""กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน"",
            //                    text: """",
            //                    type: ""error"",
            //                    confirmButtonText: ""ตกลง""
            //                });";
            //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน');", true);

            if (fulFileUpload.HasFile)
            {
                Session["fulFileUpload"] = fulFileUpload;
                lblfulFileUpload.Text = fulFileUpload.FileName;
            }

            if (fulImgUpload.HasFile)
            {
                Session["fulImgUpload"] = fulImgUpload;
                lblfulImgUpload.Text = fulImgUpload.FileName;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Views/NewsList.aspx");

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlNewsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNewsType.SelectedValue == "ทั่วไป")
        {
            ddlAgentName.ClearSelection();
            //ddlAgentName.SelectedIndex = 0;
            ddlAgentName.Enabled = false;
            txtCV_Code.Enabled = false;
            pnlAgent.Visible = false;
        }
        else
        {
            pnlAgent.Visible = true;
            ddlAgentName.Enabled = true;
            txtCV_Code.Enabled = true;
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void txtCV_Code_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //List<ListItem> selected = new List<ListItem>();
            //foreach (ListItem item in ddlAgentName.Items)
            //{
            //    if (item.Selected) selected.Add(item);
            //}
            //
            //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(
            //  txtCV_Code.Text
            //  , string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
            //  string.Empty, "ดำเนินธุรกิจอยู่", string.Empty);

            //ddlAgentName.DataSource = agent;
            //ddlAgentName.DataBind();

            //foreach (ListItem item in ddlAgentName.Items)
            //{
            //    if (selected.Contains(item))
            //        item.Selected = true;
            //}
        }
        catch (Exception)
        {

        }

    }
    #endregion

    #region Methods
    private void loadDDL()
    {
       agent = dbo_AgentDataClass.Search(
              string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
              string.Empty, "ดำเนินธุรกิจอยู่", string.Empty);

        //agent.Insert(0, new dbo_AgentClass { AgentName = "==ระบุ==", CV_Code = string.Empty });
        ddlAgentName.DataSource = agent;
        ddlAgentName.DataBind();
        //DataSet ds = dbo_NewsDataClass.GetAgent();

        //if (ds.Tables[0].Rows.Count > 0)
        //{

        //    ddlAgentName.DataSource = ds.Tables[0];
        //    ddlAgentName.DataTextField = "Agent_Name";
        //    ddlAgentName.DataValueField = "CV_Code";
        //    ddlAgentName.DataBind();

        //    ddlAgentName.Items.Insert(0, new ListItem("==ไม่ระบุ==", string.Empty));
        //    ddlAgentName.SelectedIndex = 0;
        //}
    }

    private String generateNewsID()
    {
        string id = string.Empty;
        string firstid = "NW";
        string yearid = DateTime.Now.ToString("yy", ThaiCulture);
        string monthid = String.Format("{0:MM}", DateTime.Now);
        string dayid = String.Format("{0:dd}", DateTime.Now);
        string formatid = firstid + yearid + monthid + dayid;
        int lastedid = dbo_NewsDataClass.GetNewsLastedID(formatid) + 1;
        string runid = lastedid.ToString("0000");
        id = formatid + runid;

        return id;
    }

    protected void ctvfulFileUpload_ServerValidate(object source, ServerValidateEventArgs args)
    {
        double fileMB = (fulFileUpload.FileBytes.Length / 1024f);
        if (fileMB > 100000) //Cannot upload file over 10 MB
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void ctvfulImgUpload_ServerValidate(object source, ServerValidateEventArgs args)
    {
        double imgMB = (fulImgUpload.FileBytes.Length / 1024f);
        if (imgMB > 100000) //Cannot upload file over 10 MB
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    #region Hide example code download file and picture from Database
    //protected void btnDownload_Click(object sender, EventArgs e)
    //{
    //    string id = "NW001";
    //    byte[] bytes;
    //    string fileName, newsID;
    //    string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            cmd.CommandText = "select News_ID,News_Type,CV_Code,Subject, Content, Content_FileName, Content_File, VDO_Hyperlink, Start_Date, End_Date, Photo_Name, Photo, Created_By, Last_Modified_By from News where News_ID=@Id";
    //            cmd.Parameters.AddWithValue("@Id", id);
    //            cmd.Connection = con;
    //            con.Open();
    //            using (SqlDataReader sdr = cmd.ExecuteReader())
    //            {
    //                sdr.Read();
    //                bytes = (byte[])sdr["Content_File"];
    //                newsID = sdr["News_ID"].ToString();
    //                fileName = sdr["Content_FileName"].ToString();
    //            }
    //            con.Close();
    //        }
    //    }
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.Charset = "";
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "Textfile/txt";
    //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
    //    Response.BinaryWrite(bytes);
    //    Response.Flush();
    //    Response.End();
    //}
    //protected void btnDownloadImd_Click(object sender, EventArgs e)
    //{
    //    string id = "NW001";
    //    byte[] bytes;
    //    string fileName, newsID;
    //    string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            cmd.CommandText = "select News_ID,News_Type,CV_Code,Subject, Content, Content_FileName, Content_File, VDO_Hyperlink, Start_Date, End_Date, Photo_Name, Photo, Created_By, Last_Modified_By from News where News_ID=@Id";
    //            cmd.Parameters.AddWithValue("@Id", id);
    //            cmd.Connection = con;
    //            con.Open();
    //            using (SqlDataReader sdr = cmd.ExecuteReader())
    //            {
    //                sdr.Read();
    //                bytes = (byte[])sdr["Photo"];
    //                newsID = sdr["News_ID"].ToString();
    //                fileName = sdr["Photo_Name"].ToString();
    //            }
    //            con.Close();
    //        }
    //    }
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.Charset = "";
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "image/jpg";
    //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
    //    Response.BinaryWrite(bytes);
    //    Response.Flush();
    //    Response.End();
    //}
    #endregion

    #region Hide example code for show video on youtube
    //protected void btnShowVideo_Click(object sender, EventArgs e)
    //{
    //    string videoURL = txtVideo.Text.ToString();
    //    string VideoID = videoURL.Substring(videoURL.LastIndexOf("v=") + 2);
    //    LabelShowYouTubeVideo.Text = "<object width='425' height='355'><param name='movie' value='http://www.youtube.com/v/" + VideoID + "'></param><param name='wmode' value='transparent'></param><embed src='http://www.youtube.com/v/" + VideoID + "' type='application/x-shockwave-flash' wmode='transparent' width='420' height='315'></embed></object>";
    //}
    #endregion

    public void Show(string message)
    {
        try
        {


            string cleanMessage = message.Replace("'", "\'");
            // Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            //if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            //{
            //  page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {

        }
        //}
    }
    #endregion   
}