#region Using
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_EditNews : System.Web.UI.Page
{
    private string PRM;
    #region Private Variable
    CultureInfo ThaiCulture = new CultureInfo("th-TH");
    string NewsID = string.Empty;
    string User_ID = string.Empty;
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["News_ID"] != null)
            NewsID = (string)Session["News_ID"];

        if (Request.Cookies["User_ID"] == null)
            Response.Redirect("../Login.aspx");
        else
            User_ID = Request.Cookies["User_ID"].Value;

        //if (Session["fulFileUploadEdit"] != null)
        //{
        //    fulFileUploadEdit = (FileUpload)Session["fulFileUploadEdit"];
        //    Session.Remove("fulFileUpload");
        //}

        //if (Session["fulImgUploadEdit"] != null)
        //{
        //    fulImgUploadEdit = (FileUpload)Session["fulImgUploadEdit"];
        //    Session.Remove("fulImgUploadEdit");
        //}


        if (!IsPostBack)
        {
            loadDDL();
            LoadData();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Page.Validate("EditNewsValidation");

        PRM = Request.QueryString["PRM"];
        if (IsValid)
        {
            DateTime startDate = Convert.ToDateTime(txtStart_DateEdit.Text);
            DateTime endDate = Convert.ToDateTime(txtEnd_DateEdit.Text);
            DateTime today = DateTime.Today;
            if (startDate < today && PRM == null)
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
                string newsType = ddlNewsTypeEdit.SelectedValue;
                string agentName = ddlAgentNameEdit.SelectedValue;
                string subject = txtSubjectEdit.Text;
                string content = txtContentEdit.Text;
                string videolink = txtVideoEdit.Text;

                string username = User_ID;

                string fileName = string.Empty;
                string contenttype = string.Empty;
                string imgName = string.Empty;
                string imgType = string.Empty;
                Byte[] fileBytes = null;
                Byte[] imgBytes = null;
               

                if (fulFileUploadEdit.HasFile)
                {
                    string filePath = fulFileUploadEdit.PostedFile.FileName;
                    fileName = Path.GetFileName(filePath);
                    string fileExt = Path.GetExtension(fileName);
                    contenttype = Common.Common.ContentType(fileExt);

                    Stream fs = fulFileUploadEdit.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    fileBytes = br.ReadBytes((Int32)fs.Length);
                }
                if (fulImgUploadEdit.HasFile)
                {
                    string imgPath = fulImgUploadEdit.PostedFile.FileName;
                    imgName = Path.GetFileName(imgPath);
                    string imgExt = Path.GetExtension(imgName);
                    imgType = Common.Common.ContentType(imgExt);

                    Stream imgST = fulImgUploadEdit.PostedFile.InputStream;
                    BinaryReader imgBR = new BinaryReader(imgST);
                    imgBytes = imgBR.ReadBytes((Int32)imgST.Length);
                }

                bool status = dbo_NewsDataClass.UpdateNews(NewsID, newsType, agentName, subject, content, fileName, contenttype, fileBytes, videolink, startDate, endDate, imgName, imgType, imgBytes, username);

                Session["News_ID"] = NewsID;
                //Session["Save_Edit"] = "Save_Edit";
                //Response.Redirect("../Views/DetailsNews.aspx");
                //Show("บันทึกสำเร็จ");
                //System.Threading.Thread.Sleep(500);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //Response.Redirect("../Views/NewsList.aspx");
                
                string script  = string.Format("alert('{0}');window.location ='NewsList.aspx';", "บันทึกสำเร็จ");
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
           
            }
        }
        else
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["News_ID"] = NewsID;
        //Response.Redirect("../Views/DetailsNews.aspx");
        Response.Redirect("../Views/NewsList.aspx");

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
    #endregion

    #region Methods
    private void loadDDL()
    {
        DataSet ds = dbo_NewsDataClass.GetAgent();

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlAgentNameEdit.DataSource = ds.Tables[0];
            ddlAgentNameEdit.DataTextField = "Agent_Name";
            ddlAgentNameEdit.DataValueField = "Agent_Name";
            ddlAgentNameEdit.DataBind();

            ddlAgentNameEdit.Items.Insert(0, new ListItem("==ไม่ระบุ==", string.Empty));
            ddlAgentNameEdit.SelectedIndex = 0;
        }
    }

    private void LoadData()
    {
        DataSet NewsDetails = dbo_NewsDataClass.GetNewsDetails(NewsID);

        ddlNewsTypeEdit.SelectedValue = NewsDetails.Tables[0].Rows[0]["News_Type"].ToString();
        ddlAgentNameEdit.SelectedValue = NewsDetails.Tables[0].Rows[0]["Agent_Name"].ToString();
        txtSubjectEdit.Text = NewsDetails.Tables[0].Rows[0]["Subject"].ToString();
        txtContentEdit.Text = NewsDetails.Tables[0].Rows[0]["Content"].ToString();
        txtVideoEdit.Text = NewsDetails.Tables[0].Rows[0]["VDO_Hyperlink"].ToString();
        txtStart_DateEdit.Text = Convert.ToDateTime(NewsDetails.Tables[0].Rows[0]["Start_Date"].ToString()).ToString("dd/MM/yyyy");
        txtEnd_DateEdit.Text = Convert.ToDateTime(NewsDetails.Tables[0].Rows[0]["End_Date"].ToString()).ToString("dd/MM/yyyy");

        if (!string.IsNullOrEmpty(NewsDetails.Tables[0].Rows[0]["Content_FileName"].ToString())) lblOldFileUploadEdit.Text = NewsDetails.Tables[0].Rows[0]["Content_FileName"].ToString();
        if (!string.IsNullOrEmpty(NewsDetails.Tables[0].Rows[0]["Photo_Name"].ToString())) lblOldImgUploadEdit.Text = NewsDetails.Tables[0].Rows[0]["Photo_Name"].ToString();
    }

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

    protected void txtStart_DateEdit_TextChanged(object sender, EventArgs e)
    {
        DateTime startDate = Convert.ToDateTime(txtStart_DateEdit.Text);
        DateTime today = DateTime.Today;
        if (startDate < today)
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ไม่สามารถระบุวันที่เริ่มย้อนหลังได้");
            txtStart_DateEdit.Text = string.Empty;
        }
    }

    protected void txtEnd_DateEdit_TextChanged(object sender, EventArgs e)
    {
        DateTime startDate = Convert.ToDateTime(txtStart_DateEdit.Text);
        DateTime endDate = Convert.ToDateTime(txtEnd_DateEdit.Text);
        DateTime today = DateTime.Today;

        if (endDate < today)
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ไม่สามารถระบุวันที่สิ้นสุดย้อนหลังวันปัจุบันได้");
            txtEnd_DateEdit.Text = string.Empty;
        }
        else if (endDate < startDate)
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ไม่สามารถระบุวันที่สิ้นสุดย้อนหลังวันที่เริ่มได้");
            txtEnd_DateEdit.Text = string.Empty;
        }
    }



   
}