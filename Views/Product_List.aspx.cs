#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_Product_List : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtProduct_ID.Attributes.Add("onchange", "myApp.showPleaseWait();");
         //   AsyncFileUpload1.Attributes.Add("onchange", "myApp.showPleaseWait();");
            SetUpDrowDownList();
            //SetEmptyUserGrid();
            showPanel("pnlGrid");

            btnSearch_Click(sender, e);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SearchSubmit();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        GetDetailsDataToForm(string.Empty, string.Empty);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SetUpDrowDownList();
        SetEmptyUserGrid();
        showPanel("pnlGrid");

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        SearchSubmit();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (btnSave.Text == "แก้ไข")
        {
            GetDetailsDataToForm(txtProduct_ID.Text, "Edit");

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        else
        {

            Validate("ProductValidation");

            if (IsValid)
            {
                if (btnSaveMode.Value == "บันทึก")
                {
                    InsertRecord();

                }
                else
                {
                    UpdateRecord();
                }

                showPanel("pnlGrid");
                btnSearch_Click(sender, e);

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }
        }
    }

    protected void btnCancelSearch_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SetUpDrowDownList();
        SetEmptyUserGrid();
        showPanel("pnlGrid");

        grdProduct.Visible = false;
        pnlNoRec.Visible = false;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void txtProduct_ID_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            dbo_ProductClass product = dbo_ProductDataClass.Select_Record(txtProduct_ID.Text);
            if (product != null)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("รหัสสินค้าไม่สามารถซ้ำได้");
                txtProduct_ID.Text = string.Empty;
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion

    #region Method
    private void showPanel(string panelName)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlForm.Visible = false;
        pnlGrid.Visible = false;

        switch (panelName)
        {
            case "pnlForm":
                pnlForm.Visible = true;
                break;
            case "pnlGrid":
                pnlGrid.Visible = true;
                break;
        }
    }

    private void SetEmptyUserGrid()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if(grdProduct.Rows.Count > 0)
        {
            List<dbo_ProductClass> products = new List<dbo_ProductClass>();
            grdProduct.DataSource = products;
            grdProduct.DataBind();
        }

        txtSearchProduct_ID.Text = string.Empty;
        txtSearchProduct_Name.Text = string.Empty;
        ddlSearchProduct_Group.ClearSelection();
        txtSearchSize.Text = string.Empty;
        ddlSearchUnit_of_item_ID.Text = string.Empty;

    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        Dictionary<string, string> unit = dbo_ItemDataClass.GetDropDown("06");
        ddlSearchUnit_of_item_ID.DataSource = unit;
        ddlSearchUnit_of_item_ID.DataBind();

        ddlUnit_of_item_ID.DataSource = unit;
        ddlUnit_of_item_ID.DataBind();
    }

    private void GetDetailsDataToForm(string id, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        showPanel("pnlForm");

        txtProduct_ID.Text = string.Empty;
        txtProduct_Name.Text = string.Empty;
        txtSize.Text = string.Empty;
        txtCP_Meiji_Price.Text = string.Empty;
        txtPoint.Text = string.Empty;
        txtVat.Text = string.Empty;
        txtOrder_No.Text = string.Empty;
        txtPacking_Size.Text = string.Empty;
        txtSAPProductCode.Text = string.Empty;
        txtAgentPrice.Text = string.Empty;
        txtSPPrice.Text = string.Empty;

        ddlProduct_group_ID.ClearSelection();
        ddlUnit_of_item_ID.ClearSelection();
        ddlStatus.ClearSelection();
        imgProductPhoto.ImageUrl = String.Empty;

        try
        {
            if (!string.IsNullOrEmpty(id))
            {

                dbo_ProductClass products = dbo_ProductDataClass.Select_Record(id);

                txtProduct_ID.Text = products.Product_ID;
                txtProduct_Name.Text = products.Product_Name;
                txtVat.Text = products.Vat.ToString();
                txtOrder_No.Text = products.Order_No.ToString();
                txtPacking_Size.Text = products.Packing_Size.ToString();
                txtSize.Text = products.Size.ToString();
                txtCP_Meiji_Price.Text = products.CP_Meiji_Price.ToString();
                txtPoint.Text = products.Point.ToString();

                txtSAPProductCode.Text = products.SAP_Product_Code;
                txtAgentPrice.Text = products.Agent_Price.ToString();
                txtSPPrice.Text = products.SP_Price.ToString();



                if (ddlProduct_group_ID.Items.FindByText(products.Product_group_ID) != null)
                    ddlProduct_group_ID.Items.FindByText(products.Product_group_ID).Selected = true;

                if (ddlUnit_of_item_ID.Items.FindByText(products.Unit_of_item_ID) != null)
                    ddlUnit_of_item_ID.Items.FindByText(products.Unit_of_item_ID).Selected = true;

                if (ddlStatus.Items.FindByText(products.Status) != null)
                    ddlStatus.Items.FindByText(products.Status).Selected = true;

                if (products.Photo != null)
                {
                    string base64String = Convert.ToBase64String(products.Photo, 0, products.Photo.Length);
                    imgProductPhoto.ImageUrl = "data:image/jpeg;base64," + base64String;
                }


            }

            bool enable = Mode != "View";

            txtProduct_ID.Enabled = false;
            txtProduct_Name.Enabled = enable;
            txtSize.Enabled = enable;
            txtCP_Meiji_Price.Enabled = enable;
            txtPoint.Enabled = enable;
            txtVat.Enabled = enable;

            txtPacking_Size.Enabled = enable;
            ddlProduct_group_ID.Enabled = enable;
            ddlUnit_of_item_ID.Enabled = enable;
            txtSAPProductCode.Enabled = enable;
            txtSPPrice.Enabled = enable;
            txtImage.Enabled = enable;

            txtAgentPrice.Enabled = enable;
            ddlStatus.Enabled = enable;
            txtOrder_No.Enabled = enable;

            // AsyncFileUpload1.Enabled = enable;
            spanImage.Visible = enable;

            if (Mode == "View")
            {
                btnSave.Visible = true;
                btnSave.Text = "แก้ไข";
                btnCancel.Text = "กลับไปหน้าค้นหา";
                btnSaveMode.Value = "บันทึก";
                //AsyncFileUpload1.Enabled = false;
            }
            else if (Mode == "Edit")
            {
                btnSave.Visible = true;
                btnSave.Text = "บันทึก";
                btnCancel.Text = "ยกเลิก";
                btnSaveMode.Value = "แก้ไข";
                //AsyncFileUpload1.Enabled = true;
            }
            else if (string.IsNullOrEmpty(Mode))
            {

                btnSave.Visible = true;
                btnSave.Text = "บันทึก";
                btnCancel.Text = "ยกเลิก";
                btnSaveMode.Value = "บันทึก";
                txtProduct_ID.Enabled = true;
                //AsyncFileUpload1.Enabled = enable;
                imgProductPhoto.ImageUrl = "";
                //List<dbo_ProductClass> order_ = dbo_ProductDataClass.Search(string.Empty, string.Empty, 0, string.Empty);
                //txtOrder_No.Text = (order_.Max(f => f.Order_No).Value + 1).ToString();
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void SearchSubmit()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int? size = null;

            if (!string.IsNullOrEmpty(txtSearchSize.Text))
            {
                size = int.Parse(txtSearchSize.Text);
            }


            //List<dbo_ProductClass> products = dbo_ProductDataClass.Search(txtSearchProduct_ID.Text, txtSearchProduct_Name.Text
            //    , ddlSearchProduct_Group.Text, size, ddlSearchUnit_of_item_ID.SelectedValue).OrderBy(f => f.Product_ID).ToList();

            List<dbo_ProductClass> products = dbo_ProductDataClass.Search(txtSearchProduct_ID.Text, txtSearchProduct_Name.Text
                , ddlSearchProduct_Group.Text, size, ddlSearchUnit_of_item_ID.SelectedValue).ToList();
            //products.OrderBy(f => f.Product_ID);
            if (products.Count > 0)
            {
                grdProduct.DataSource = products;
                grdProduct.DataBind();

                grdProduct.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                grdProduct.Visible = false;
                pnlNoRec.Visible = true;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    private void InsertRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();

        SetData(clsdbo_Product);


        bool success = false;
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        success = dbo_ProductDataClass.Add(clsdbo_Product, User_ID);

        dbo_ProductListClass clsdbo_ProductList = new dbo_ProductListClass();


        List<dbo_PriceGroupClass> price_group = dbo_PriceGroupDataClass.Search(string.Empty, "0");
        dbo_PriceGroupClass price_group_id = price_group.FirstOrDefault(f => f.StandardPrice == true);

        clsdbo_ProductList.Price_Group_ID = price_group_id.Price_Group_ID;
        clsdbo_ProductList.Product_List_ID = GenerateID.Product_List_ID();
        var date = new DateTime(9456, 12, 31);
        clsdbo_ProductList.Agent_Price = clsdbo_Product.Agent_Price;
        clsdbo_ProductList.Product_Effective_Date = DateTime.Now;
        clsdbo_ProductList.Start_Effective_Date = DateTime.Now;
        clsdbo_ProductList.End_Effective_Date = date;
        clsdbo_ProductList.Product_ID = clsdbo_Product.Product_ID;
        clsdbo_ProductList.Product_Name = clsdbo_Product.Product_Name;
        clsdbo_ProductList.Point = clsdbo_Product.Point;
        clsdbo_ProductList.Vat = clsdbo_Product.Vat;
        //clsdbo_ProductList.CP_Meiji_Price = clsdbo_Product.CP_Meiji_Price;

        dbo_ProductListDataClass.Add(clsdbo_ProductList, User_ID);

        price_group = dbo_PriceGroupDataClass.Search(string.Empty, "1");
        price_group_id = price_group.FirstOrDefault(f => f.StandardPrice == true);


        if (price_group_id != null)
        {
            clsdbo_ProductList.Price_Group_ID = price_group_id.Price_Group_ID;
            clsdbo_ProductList.SP_Price = clsdbo_Product.SP_Price;
            clsdbo_ProductList.CP_Meiji_Price = clsdbo_Product.CP_Meiji_Price;
            clsdbo_ProductList.Agent_Price = null;
            clsdbo_ProductList.Product_List_ID = GenerateID.Product_List_ID();

            dbo_ProductListDataClass.Add(clsdbo_ProductList, User_ID);
        }

        if (success)
        {
            Show("บันทึกสำเร็จ");
        }
        else
        {
            Show("error");
        }
    }

    private void UpdateRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();

        clsdbo_Product = dbo_ProductDataClass.Select_Record(txtProduct_ID.Text);

        SetData(clsdbo_Product);

        bool success = false;
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        success = dbo_ProductDataClass.Update(clsdbo_Product, User_ID);


        List<dbo_ProductListClass> product_list = dbo_ProductListDataClass.Search(string.Empty, string.Empty, txtProduct_ID.Text);
        dbo_ProductListClass product_list_id = product_list.FirstOrDefault(f => f.StandardPrice == true && f.Price_Group_Type == "0");//Agent
        dbo_ProductListClass clsdbo_ProductList = new dbo_ProductListClass();
        if (product_list_id != null)
        {
            clsdbo_ProductList = dbo_ProductListDataClass.Select_Record(product_list_id.Product_List_ID);
            clsdbo_ProductList.Product_Name = clsdbo_Product.Product_Name;
            clsdbo_ProductList.Vat = clsdbo_Product.Vat;
            clsdbo_ProductList.Agent_Price = clsdbo_Product.Agent_Price;
            success = dbo_ProductListDataClass.Update(clsdbo_ProductList, User_ID);
        }

        dbo_ProductListClass product_list_id1 = product_list.FirstOrDefault(f => f.StandardPrice == true && f.Price_Group_Type == "1");//SP
        //dbo_ProductListClass clsdbo_ProductList = new dbo_ProductListClass();
        if (product_list_id1 != null)
        {
            clsdbo_ProductList = dbo_ProductListDataClass.Select_Record(product_list_id1.Product_List_ID);
            clsdbo_ProductList.Product_Name = clsdbo_Product.Product_Name;
            clsdbo_ProductList.Vat = clsdbo_Product.Vat;
            clsdbo_ProductList.SP_Price = clsdbo_Product.SP_Price;
            clsdbo_ProductList.CP_Meiji_Price = clsdbo_Product.CP_Meiji_Price;
            clsdbo_ProductList.Point = clsdbo_Product.Point;
            success = dbo_ProductListDataClass.Update(clsdbo_ProductList, User_ID);
        }

        if (success)
        {
            Show("บันทึกสำเร็จ");
        }
        //else
        //{
        //    Show("error");
        //}
    }

    private void SetData(dbo_ProductClass clsdbo_Product)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            if (string.IsNullOrEmpty(txtProduct_ID.Text))
            {
                clsdbo_Product.Product_ID = null;
            }
            else
            {
                clsdbo_Product.Product_ID = txtProduct_ID.Text;
            }
            if (string.IsNullOrEmpty(txtProduct_Name.Text))
            {
                clsdbo_Product.Product_Name = null;
            }
            else
            {
                clsdbo_Product.Product_Name = txtProduct_Name.Text;
            }
            if (string.IsNullOrEmpty(txtSize.Text))
            {
                clsdbo_Product.Size = null;
            }
            else
            {
                clsdbo_Product.Size = System.Convert.ToInt16(txtSize.Text);
            }

            if (string.IsNullOrEmpty(txtCP_Meiji_Price.Text))
            {
                clsdbo_Product.CP_Meiji_Price = null;
            }
            else
            {
                clsdbo_Product.CP_Meiji_Price = System.Convert.ToDecimal(txtCP_Meiji_Price.Text);
            }
            if (string.IsNullOrEmpty(txtPoint.Text))
            {
                clsdbo_Product.Point = null;
            }
            else
            {
                clsdbo_Product.Point = System.Convert.ToByte(txtPoint.Text);
            }
            if (string.IsNullOrEmpty(txtVat.Text))
            {
                clsdbo_Product.Vat = null;
            }
            else
            {
                clsdbo_Product.Vat = System.Convert.ToByte(txtVat.Text);
            }
            if (string.IsNullOrEmpty(txtOrder_No.Text))
            {
                clsdbo_Product.Order_No = null;
            }
            else
            {
                clsdbo_Product.Order_No = System.Convert.ToInt16(txtOrder_No.Text);
            }

            if (string.IsNullOrEmpty(txtPacking_Size.Text))
            {
                clsdbo_Product.Packing_Size = null;
            }
            else
            {
                clsdbo_Product.Packing_Size = System.Convert.ToByte(txtPacking_Size.Text);
            }

            if (string.IsNullOrEmpty(txtSAPProductCode.Text))
            {
                clsdbo_Product.SAP_Product_Code = null;
            }
            else
            {
                clsdbo_Product.SAP_Product_Code = txtSAPProductCode.Text;
            }


            if (string.IsNullOrEmpty(txtAgentPrice.Text))
            {
                clsdbo_Product.Agent_Price = null;
            }
            else
            {
                clsdbo_Product.Agent_Price = System.Convert.ToDecimal(txtAgentPrice.Text);
            }


            if (string.IsNullOrEmpty(txtSPPrice.Text))
            {
                clsdbo_Product.SP_Price = null;
            }
            else
            {
                clsdbo_Product.SP_Price = System.Convert.ToDecimal(txtSPPrice.Text);
            }




            clsdbo_Product.Product_group_ID = ddlProduct_group_ID.SelectedValue;
            clsdbo_Product.Unit_of_item_ID = ddlUnit_of_item_ID.SelectedValue;
            clsdbo_Product.Status = ddlStatus.SelectedValue;



            if (Session["FileBytes"] != null)
            {
                clsdbo_Product.Photo = (byte[])Session["FileBytes"];
                Session.Remove("FileBytes");
            }
            else
            {
                clsdbo_Product.Photo = null;
            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void FileUploadComplete(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            Validate("ProductValidation");

            if (Page.IsValid)
            {
                string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
                if (File.Exists("c:\\img\\" + filename))
                {
                    File.Delete("c:\\img\\" + filename);
                }
                //System.Threading.Thread.Sleep(500);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                AsyncFileUpload1.SaveAs("c:\\img\\" + filename);

                byte[] filebytes = AsyncFileUpload1.FileBytes;


                Bitmap originalBMP = new Bitmap(AsyncFileUpload1.FileContent);

                // Calculate the new image dimensions
                int origWidth = originalBMP.Width;
                int origHeight = originalBMP.Height;
                double sngRatio = double.Parse(origWidth.ToString()) / double.Parse(origHeight.ToString());
                int newWidth = 100;
                int newHeight = (int)Math.Round(float.Parse((newWidth / sngRatio).ToString()));

                // Create a new bitmap which will hold the previous resized bitmap
                Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
                // Create a graphic based on the new bitmap
                Graphics oGraphics = Graphics.FromImage(newBMP);

                // Set the properties for the new graphic file
                oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // Draw the new graphic based on the resized bitmap
                oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                // Save the new graphic file to the server
                newBMP.Save("c:\\img\\" + "tn_" + filename);

                // Once finished with the bitmap objects, we deallocate them.
                originalBMP.Dispose();
                newBMP.Dispose();
                oGraphics.Dispose();


                System.Drawing.Image img = System.Drawing.Image.FromFile("c:\\img\\" + "tn_" + filename);

                hdffilename.Value = "c:\\img\\" + "tn_" + filename;

                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytes = ms.ToArray();
                }


                if (Session["FileBytes"] != null)
                {
                    Session.Remove("FileBytes");
                }
                Session["FileBytes"] = bytes;

              
                // Show("complete");

                //string base64String = Convert.ToBase64String(filebytes, 0, filebytes.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                //imgProductPhoto.ImageUrl = "data:image/jpeg;base64," + base64String;
                imgProductPhoto.ImageUrl = hdffilename.Value;
                //   imgProductPhoto.ImageUrl = "~/Images/new1.jpg";


                //  hdfImage.Value = base64String;

                txtImage.Text = AsyncFileUpload1.FileName;
               
                //  UpdatePanelGrid.Update();



                //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "__doPostBack('', '');", true);

            }
            else
            {
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
                //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "__doPostBack('', '');", true);
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }

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
            logger.Error(ex.Message);
        }
        //}
    }
    #endregion

    #region GridView Row Command
    protected void grdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Product_ID = lnkView.CommandArgument;

            GetDetailsDataToForm(Product_ID, "View");
        }
        else if (e.CommandName == "_Edit")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Product_ID = lnkView.CommandArgument;
            GetDetailsDataToForm(Product_ID, "Edit");
        }
        else if (e.CommandName == "_Delete")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Product_ID = lnkView.CommandArgument;

            dbo_ProductDataClass.Delete(Product_ID);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ลบข้อมูลสำเร็จ");

            SearchSubmit();
        }
    }

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdProduct.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        grdProduct.PageIndex = pageList.SelectedIndex;
        btnSearch_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void grdProduct_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdProduct.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < grdProduct.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == grdProduct.PageIndex)
                {
                    item.Selected = true;
                }

                // Add the ListItem object to the Items collection of the 
                // DropDownList.
                pageList.Items.Add(item);
            }
        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = grdProduct.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + grdProduct.PageCount.ToString();

        }
    }
    #endregion
}