#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_CreateSalesTarget : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            SetupDropDownList();

            btnSearchSubmit_Click(sender, e);
        }
    }

    public void ButtonNew_Click(object sender, System.EventArgs e)
    {

        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
           
     
            GridViewSaleTarget.ShowFooter = true;
            String CV_Code = ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue;
            String Month = ddlMonth.SelectedIndex == 0 ? string.Empty : ddlMonth.SelectedValue;
            String Quarter = ddlQuater.SelectedIndex == 0 ? string.Empty : ddlQuater.SelectedValue;

            //List<dbo_SalesTargetClass> item_value = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);

            ////List<dbo_SalesTargetClass> item_value = dbo_SalesTargetDataClass.Search(ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue);
            //GridViewSaleTarget.DataSource = item_value;
            //GridViewSaleTarget.DataBind();

            //if (item_value.Count == 0)
            //{
            //    item_value.Add(new dbo_SalesTargetClass());
            //    GridViewSaleTarget.DataSource = item_value;
            //    GridViewSaleTarget.DataBind();

            //    GridViewSaleTarget.Rows[0].Visible = false;

            //    GridViewSaleTarget.Visible = true;
            //    pnlNoRec.Visible = false;
            //}
            //else
            //{
            //    GridViewSaleTarget.DataSource = item_value;
            //    GridViewSaleTarget.DataBind();
            //}


            List<dbo_SalesTargetClass> SalesTarget = new List<dbo_SalesTargetClass>();

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

            if (user_class.User_Group_ID == "CP Meiji")
            {
                if (CV_Code == "")
                {
                    string region = user_class.Region;
                    string[] regions = region.Split(',');
                    //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                    List<string> item_CVCode = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.CV_Code));
                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));

                    if (item_CVCode.Count > 0)
                    {
                        //หา SalesTarget  จาก  ผู้รับผิดชอบ
                        foreach (string cvcode in item_CVCode)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(cvcode, Month, Quarter, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }
                    else
                    {
                        //หา SalesTarget  จาก  region
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(_cv.CV_Code, Month, Quarter, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }

                }
                else
                {
                    SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                }

            }
            else
            {
                //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
            }

            if (SalesTarget.Count == 0)
            {
                SalesTarget.Add(new dbo_SalesTargetClass());
                GridViewSaleTarget.DataSource = SalesTarget;
                GridViewSaleTarget.DataBind();

                GridViewSaleTarget.Rows[0].Visible = false;

                GridViewSaleTarget.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewSaleTarget.DataSource = SalesTarget;
                GridViewSaleTarget.DataBind();
            }



            if (GridViewSaleTarget.Rows.Count > 0)
            {
                btnAddNew.Enabled = true;
            }
            else
            {
                btnAddNew.Enabled = false;
            }

            DropDownList ddlFooterAgent = (DropDownList)GridViewSaleTarget.FooterRow.FindControl("ddlFooterAgent");
            //List<dbo_AgentClass> cv_code = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            TextBox _txtFooterYear = (TextBox)GridViewSaleTarget.FooterRow.FindControl("txtFooterYear");

            _txtFooterYear.Focus();


            //ddlFooterAgent.DataSource = cv_code;
            //ddlFooterAgent.DataTextField = "AgentName";
            //ddlFooterAgent.DataValueField = "CV_Code";
            //ddlFooterAgent.DataBind();


        

            //string User_ID = Request.Cookies["User_ID"].Value;
            //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);


            if (user_class.User_Group_ID == "CP Meiji")
            {
                //ddlSearchRegion
                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");

                //ddlSearchRegion.DataSource = region.Where(f => myArr.Contains(f.Value));
                //ddlSearchRegion.DataBind();
                //ddlSearchRegion.Items.Insert(0, "เลือกทั้งหมด");


                String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();
                //ddlAgentName
                 agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                if (cv_code1.Count > 0)
                {
                    //ddlSearchAgentName ข้อมูลจากผู้รับผิดชอบ
                    ddlFooterAgent.DataSource = cv_code1;
                    ddlFooterAgent.DataTextField = "AgentName";
                    ddlFooterAgent.DataValueField = "CV_Code";
                    ddlFooterAgent.DataBind();
                    ddlFooterAgent.Items.Insert(0, "เลือกทั้งหมด");
                }
                else
                {
                    //ddlSearchAgentName ตาม Region
                    ddlFooterAgent.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                    ddlFooterAgent.DataTextField = "AgentName";
                    ddlFooterAgent.DataValueField = "CV_Code";
                    ddlFooterAgent.DataBind();
                    ddlFooterAgent.Items.Insert(0, "เลือกทั้งหมด");
                }

            }

            else
            {
                //ddlSearchRegion
                agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] rg = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim()).Select(f => f.Location_Region).ToArray();

                //ddlAgentName
                //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                ddlFooterAgent.DataSource = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim());
                ddlFooterAgent.DataTextField = "AgentName";
                ddlFooterAgent.DataValueField = "CV_Code";
                ddlFooterAgent.DataBind();
                ddlFooterAgent.Enabled = false;

            }



        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            // List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search(ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue);
            //  (string CV_Code, string Month, string Quarter, string Year)

            string CV_Code = (ddlSearchAgentName.SelectedValue == "เลือกทั้งหมด" ? string.Empty : ddlSearchAgentName.SelectedValue);
            string Month = (ddlMonth.SelectedIndex == 0 ? string.Empty : ddlMonth.SelectedValue);
            string Quater = (ddlQuater.SelectedIndex == 0 ? string.Empty : ddlQuater.SelectedValue);

            List<dbo_SalesTargetClass> SalesTarget = new List<dbo_SalesTargetClass>();

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

            if (user_class.User_Group_ID == "CP Meiji")
            {
                if(CV_Code == "")
                {
                    string region = user_class.Region;
                    string[] regions = region.Split(',');
                    //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                    List<string> item_CVCode = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.CV_Code));
                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));

                    if(item_CVCode.Count > 0)
                    {
                        //หา SalesTarget  จาก  ผู้รับผิดชอบ
                        foreach (string cvcode in item_CVCode)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(cvcode, Month, Quater, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }
                    else
                    {
                        //หา SalesTarget  จาก  region
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(_cv.CV_Code, Month, Quater, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }
                    
                }
                else
                {
                    SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                }
               
            }
            else
            {
                //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
            }

           

            GridViewSaleTarget.ShowFooter = false;
            GridViewSaleTarget.EditIndex = -1;

            if (SalesTarget.Count > 0)
            {
                GridViewSaleTarget.DataSource = SalesTarget;
                GridViewSaleTarget.DataBind();

                GridViewSaleTarget.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewSaleTarget.Visible = false;
                pnlNoRec.Visible = true;
            }

            if (GridViewSaleTarget.Rows.Count > 0)
            {
                btnAddNew.Enabled = true;
            }
            else
            {
                btnAddNew.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        ddlSearchAgentName.ClearSelection();
        ddlMonth.ClearSelection();
        ddlQuater.ClearSelection();
        txtSearchYear.Text = string.Empty;

        if (GridViewSaleTarget.Rows.Count > 0)
        {
            List<dbo_SalesTargetClass> itm = new List<dbo_SalesTargetClass>();
            GridViewSaleTarget.DataSource = itm;
            GridViewSaleTarget.DataBind();
        }

        btnAddNew.Enabled = false;

        GridViewSaleTarget.Visible = false;
        pnlNoRec.Visible = false;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSaveUpload_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
       
        try
        {
            bool succes = false;
            bool isvalid = true;
            string _Month = string.Empty;
            int Count_No_CVcode = 0;
            foreach (GridViewRow currentRow in grdCSV.Rows)
            {
                Label lbl_CV_Code = (Label)currentRow.FindControl("lblItemAgent");
                Label lblItemYear = (Label)currentRow.FindControl("lblItemYear");
                Label lblItemMonth = (Label)currentRow.FindControl("lblItemMonth");
                Label lblItemSales_Target = (Label)currentRow.FindControl("lblItemSales_Target");


                dbo_SalesTargetClass target = dbo_SalesTargetDataClass.Select_Record(lbl_CV_Code.Text, lblItemYear.Text, lblItemMonth.Text);

                if (target != null)
                {
                    logger.Debug("target.Sales_Target_ID " + target.Sales_Target_ID);
                    dbo_SalesTargetDataClass.Delete(target.Sales_Target_ID);


                    //Show("ไม่สามารถระบุเป้ายอดขายซ้ำได้");
                    //isvalid = false;
                    //break;
                }

            }
            
            if (isvalid)
            {
                foreach (GridViewRow currentRow in grdCSV.Rows)
                {
                    Label lbl_CV_Code = (Label)currentRow.FindControl("lblItemAgent");
                    Label lblItemYear = (Label)currentRow.FindControl("lblItemYear");
                    Label lblItemMonth = (Label)currentRow.FindControl("lblItemMonth");
                    Label lblItemSales_Target = (Label)currentRow.FindControl("lblItemSales_Target");

                    dbo_SalesTargetClass st = new dbo_SalesTargetClass();

                    st.Sales_Target_ID = GenerateID.Sales_Target_ID();
                    st.CV_Code = lbl_CV_Code.Text;
                    st.Year = lblItemYear.Text;
                    st.Month = lblItemMonth.Text;

                    st.Sales_Target = Decimal.Parse(lblItemSales_Target.Text.Replace(",", string.Empty));

                    //bool succes = false;
                    string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

                    if (st.CV_Code != string.Empty)
                    {
                        succes = dbo_SalesTargetDataClass.Add(st);
                    }
                    else
                    {
                        _Month += " " + CV_Month(st.Month);
                        Count_No_CVcode++;
                    }
                   

                    //System.Threading.Thread.Sleep(500);
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    //Show("บันทึกสำเร็จ!");

                }

            }
            if(succes==true)
            {
                if(_Month != "")
                {
                    string error_message = "บันทึกสำเร็จ!,มีข้อมูลที่ไม่มี CV_Code จำนวน " + Count_No_CVcode.ToString() +" ระบบจะไม่นำข้อมูลที่ไม่มี CV_Code เข้าสู่ระบบ";
                    Show(error_message);     
                    logger.Debug("NO CV_Code = " + Count_No_CVcode.ToString());
                   // Show("บันทึกสำเร็จ!");
                }
                else
                {
                    Show("บันทึกสำเร็จ!");
                } 
                btnSaveUpload.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            FileUpload1 = new FileUpload();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlForm.Visible = true;
        pnlGrid.Visible = false;
        FileUpload1 = new FileUpload();
        btnSaveUpload.Enabled = false;
        List<dbo_SalesTargetClass> list_Of_saletarget = new List<dbo_SalesTargetClass>();
        grdCSV.DataSource = list_Of_saletarget;
        grdCSV.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnUploadCSV_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            if(FileUpload1.HasFile)
            {
                //  FileUpload1 = new FileUpload();
                logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
                UploadAndProcessFile();
            }
            else
            {
                if(grdCSV.Rows.Count > 0)
                {
                    List<dbo_SalesTargetClass> list_Of_saletarget = new List<dbo_SalesTargetClass>();
                    grdCSV.DataSource = list_Of_saletarget;
                    grdCSV.DataBind();
                }
                btnSaveUpload.Enabled = false;
                Show("กรุณาเลือกไฟล์ที่จะทำการอัพโหลดก่อนเรียกดูข้อมูล");
            }
           

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }
    #endregion

    #region Methods
    private void SetupDropDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            #region Old
            //dbo_AgentClass cv_ = new dbo_AgentClass();
            //cv_.AgentName = "==ระบุ==";
            //cv_.CV_AgentName = "==ระบุ==";
            //cv_.CV_Code = string.Empty;

            //List<dbo_AgentClass> cv_code = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //cv_code.Insert(0, cv_);

            //ddlSearchAgentName.DataSource = cv_code;
            //ddlSearchAgentName.DataBind();

            //ddlSearchAgentName.ClearSelection();

            #endregion

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);


            if (user_class.User_Group_ID == "CP Meiji")
            {
                //ddlSearchRegion
                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");

                //ddlSearchRegion.DataSource = region.Where(f => myArr.Contains(f.Value));
                //ddlSearchRegion.DataBind();
                //ddlSearchRegion.Items.Insert(0, "เลือกทั้งหมด");


                    String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();
                    //ddlAgentName
                    List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                    List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                    if (cv_code1.Count > 0)
                    {
                    //ddlSearchAgentName ข้อมูลจากผู้รับผิดชอบ
                    ddlSearchAgentName.DataSource = cv_code1;
                    ddlSearchAgentName.DataBind();
                    ddlSearchAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }
                    else
                    {
                    //ddlSearchAgentName ตาม Region
                    ddlSearchAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                    ddlSearchAgentName.DataBind();
                    ddlSearchAgentName.Items.Insert(0, "เลือกทั้งหมด");
                    }

                }
         
            else
            {
                //ddlSearchRegion
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] rg = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim()).Select(f => f.Location_Region).ToArray();

                //ddlAgentName
                //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                ddlSearchAgentName.DataSource = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim());
                ddlSearchAgentName.DataBind();
                ddlSearchAgentName.Enabled = false;

            }



        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void InitialSalesTarget()
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            GridViewSaleTarget.EditIndex = -1;
            GridViewSaleTarget.ShowFooter = false;

            String CV_Code = ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue;
            String Month = ddlMonth.SelectedIndex == 0 ? string.Empty : ddlMonth.SelectedValue;
            String Quarter = ddlQuater.SelectedIndex == 0 ? string.Empty : ddlQuater.SelectedValue;
            
            //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search(CV_Code, Month, Quarter, txtSearchYear.Text);
            //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search(ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue);


            List<dbo_SalesTargetClass> SalesTarget = new List<dbo_SalesTargetClass>();

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

            if (user_class.User_Group_ID == "CP Meiji")
            {
                if (CV_Code == "")
                {
                    string region = user_class.Region;
                    string[] regions = region.Split(',');
                    //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                    List<string> item_CVCode = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.CV_Code));
                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));

                    if (item_CVCode.Count > 0)
                    {
                        //หา SalesTarget  จาก  ผู้รับผิดชอบ
                        foreach (string cvcode in item_CVCode)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(cvcode, Month, Quarter, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }
                    else
                    {
                        //หา SalesTarget  จาก  region
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(_cv.CV_Code, Month, Quarter, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }

                }
                else
                {
                    SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                }

            }
            else
            {
                //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
            }



            GridViewSaleTarget.DataSource = SalesTarget;
            GridViewSaleTarget.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    public void Show(string message)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        logger.Info(message);
        try
        {
            string cleanMessage = message.Replace("'", "\'");
            string script = string.Format("alert('{0}');", cleanMessage);

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void UploadAndProcessFile()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (FileUpload1.HasFile)
        {
            try
            {
                string CSVFilePath = FileUpload1.PostedFile.FileName;

                string csvFilePath = "c:\\saletarget" + "\\" + CSVFilePath;

                FileUpload1.SaveAs(csvFilePath);

                string ReadCSV = File.ReadAllText(csvFilePath);

                List<dbo_SalesTargetClass> list_Of_saletarget = new List<dbo_SalesTargetClass>();
                grdCSV.DataSource = list_Of_saletarget;
                grdCSV.DataBind();
                bool success = true;
                for (int i = 1; i < ReadCSV.Split('\n').Length; i++)
                // foreach (string csvRow in ReadCSV.Split('\n'))
                {
                    string csvRow = ReadCSV.Split('\n')[i];
                    if (!string.IsNullOrEmpty(csvRow))
                    {
                        string Month = csvRow.Split(',')[2];
                        string Year = csvRow.Split(',')[1];

                        int parse_year = int.Parse(Year);

                        if (Month == "1" || Month == "2" || Month == "3" || Month == "4" || Month == "5" || Month == "6" || Month == "7" || Month == "8" || Month == "9")
                        {
                            Month = "0" + Month;
                        }


                        if ((Month == "01" || Month == "02" || Month == "03" || Month == "04" || Month == "05" || Month == "06" ||
                            Month == "07" || Month == "08" || Month == "09" || Month == "10" || Month == "11" || Month == "12") && (parse_year >= 2540))
                        {

                            list_Of_saletarget.Add(new dbo_SalesTargetClass
                            {
                                CV_Code = csvRow.Split(',')[0],
                                Year = Year,
                                Month = Month,
                                Sales_Target = decimal.Parse(csvRow.Split(',')[3])
                            });
                        }
                        else
                        {
                            success = false;
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            Show("กรุณาตรวจสอบข้อมูลเดือน/ปี ให้ถูกต้อง");
                            break;
                        }
                    }
                }

                if (success)
                {
                    grdCSV.DataSource = list_Of_saletarget;
                    grdCSV.DataBind();
                   
                }

            }
            catch (Exception ex)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาตรวจสอบข้อมูลให้ถูกต้อง");
                logger.Error(ex.Message);
            }
            FileUpload1 = new FileUpload();
            if (grdCSV.Rows.Count > 0)
            {
                btnSaveUpload.Enabled = true;
            }
            else
            {
                btnSaveUpload.Enabled = false;
            }
        }

    }
    #endregion

    #region GridView Events
    protected void GridViewSaleTarget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && GridViewSaleTarget.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddlAgentName = (DropDownList)e.Row.FindControl("ddlAgentName");

            //List<dbo_AgentClass> cv_code = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //ddlAgentName.DataSource = cv_code;
            //ddlAgentName.DataTextField = "AgentName";
            //ddlAgentName.DataValueField = "CV_Code";
            //ddlAgentName.DataBind();
            //ddlAgentName.Items.FindByText((e.Row.FindControl("lblItemAgent") as Label).Text).Selected = true;

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);


            if (user_class.User_Group_ID == "CP Meiji")
            {
                //ddlSearchRegion
                String strString = user_class.Region;
                String[] myArr = strString.Split(',');
                Dictionary<string, string> region = dbo_ItemDataClass.GetDropDown("07");

                //ddlSearchRegion.DataSource = region.Where(f => myArr.Contains(f.Value));
                //ddlSearchRegion.DataBind();
                //ddlSearchRegion.Items.Insert(0, "เลือกทั้งหมด");


                String[] region_tmp = region.Where(f => myArr.Contains(f.Value)).Select(f => f.Value).ToArray();
                //ddlAgentName
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));

                if (cv_code1.Count > 0)
                {
                    //ddlSearchAgentName ข้อมูลจากผู้รับผิดชอบ
                    ddlAgentName.DataSource = cv_code1;
                    ddlAgentName.DataTextField = "AgentName";
                    ddlAgentName.DataValueField = "CV_Code";
                    ddlAgentName.DataBind();
                    ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                }
                else
                {
                    //ddlSearchAgentName ตาม Region
                    ddlAgentName.DataSource = agent.Where(f => region_tmp.Contains(f.Location_Region)).OrderBy(f => f.CV_AgentName);
                    ddlAgentName.DataTextField = "AgentName";
                    ddlAgentName.DataValueField = "CV_Code";
                    ddlAgentName.DataBind();
                    ddlAgentName.Items.Insert(0, "เลือกทั้งหมด");
                }

            }

            else
            {
                //ddlSearchRegion
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                String[] rg = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim()).Select(f => f.Location_Region).ToArray();

                //ddlAgentName
                //List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);
                ddlAgentName.DataSource = agent.Where(f => f.CV_Code == user_class.CV_CODE.Trim());
                ddlAgentName.DataTextField = "AgentName";
                ddlAgentName.DataValueField = "CV_Code";
                ddlAgentName.DataBind();
                ddlAgentName.Enabled = false;

            }

            ddlAgentName.Items.FindByText((e.Row.FindControl("lblItemAgent") as Label).Text).Selected = true;
        }

    }

    protected void GridViewSaleTarget_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            GridViewSaleTarget.EditIndex = -1;
            GridViewSaleTarget.ShowFooter = false;

            String CV_Code = ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue;
            String Month = ddlMonth.SelectedIndex == 0 ? string.Empty : ddlMonth.SelectedValue;
            String Quarter = ddlQuater.SelectedIndex == 0 ? string.Empty : ddlQuater.SelectedValue;
            //_txtEditSales_Target
            //TextBox _txtEditSales_Target = (TextBox)GridViewSaleTarget.S.FindControl("txtEditSales_Target");

            //List<dbo_SalesTargetClass> item_value = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);

            List<dbo_SalesTargetClass> SalesTarget = new List<dbo_SalesTargetClass>();

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

            if (user_class.User_Group_ID == "CP Meiji")
            {
                if (CV_Code == "")
                {
                    string region = user_class.Region;
                    string[] regions = region.Split(',');
                    //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                    List<string> item_CVCode = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.CV_Code));
                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));

                    if (item_CVCode.Count > 0)
                    {
                        //หา SalesTarget  จาก  ผู้รับผิดชอบ
                        foreach (string cvcode in item_CVCode)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(cvcode, Month, Quarter, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }
                    else
                    {
                        //หา SalesTarget  จาก  region
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(_cv.CV_Code, Month, Quarter, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }

                }
                else
                {
                    SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                }

            }
            else
            {
                //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
            }


            GridViewSaleTarget.DataSource = SalesTarget;
            GridViewSaleTarget.DataBind();
            
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewSaleTarget_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            int index = e.RowIndex;
            Label _Sales_Target_ID = (Label)GridViewSaleTarget.Rows[e.RowIndex].FindControl("lblSales_Target_ID");


            dbo_SalesTargetClass saletarget = dbo_SalesTargetDataClass.Select_Record(_Sales_Target_ID.Text);

            logger.Debug("saletarget.Actual_Sales " + saletarget.Actual_Sales);

            if (saletarget.Actual_Sales > 0)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ไม่สามารถลบข้อมูลได้");
            }
            else
            {
                dbo_SalesTargetDataClass.Delete(_Sales_Target_ID.Text);

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ลบข้อมูลสำเร็จ");

                GridViewSaleTarget.ShowFooter = false;


                String CV_Code = ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue;
                String Month = ddlMonth.SelectedIndex == 0 ? string.Empty : ddlMonth.SelectedValue;
                String Quarter = ddlQuater.SelectedIndex == 0 ? string.Empty : ddlQuater.SelectedValue;

                // List<dbo_SalesTargetClass> item_value = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);

                List<dbo_SalesTargetClass> SalesTarget = new List<dbo_SalesTargetClass>();

                string User_ID = Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                if (user_class.User_Group_ID == "CP Meiji")
                {
                    if (CV_Code == "")
                    {
                        string region = user_class.Region;
                        string[] regions = region.Split(',');
                        //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                        List<string> item_CVCode = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.CV_Code));
                        List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));

                        if (item_CVCode.Count > 0)
                        {
                            //หา SalesTarget  จาก  ผู้รับผิดชอบ
                            foreach (string cvcode in item_CVCode)
                            {
                                //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                                //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                                List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(cvcode, Month, Quarter, txtSearchYear.Text);

                                foreach (dbo_SalesTargetClass rpt in item1)
                                {
                                    SalesTarget.Add(rpt);
                                }

                            }
                        }
                        else
                        {
                            //หา SalesTarget  จาก  region
                            foreach (dbo_AgentClass _cv in cv_code2)
                            {
                                //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                                //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                                List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(_cv.CV_Code, Month, Quarter, txtSearchYear.Text);

                                foreach (dbo_SalesTargetClass rpt in item1)
                                {
                                    SalesTarget.Add(rpt);
                                }

                            }
                        }

                    }
                    else
                    {
                        SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                    }

                }
                else
                {
                    //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                    SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                }


                GridViewSaleTarget.DataSource = SalesTarget;
                GridViewSaleTarget.DataBind();
            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewSaleTarget_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewSaleTarget.EditIndex = e.NewEditIndex;
            //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search(ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue);
            String CV_Code = ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue;
            String Month = ddlMonth.SelectedIndex == 0 ? string.Empty : ddlMonth.SelectedValue;
            String Quarter = ddlQuater.SelectedIndex == 0 ? string.Empty : ddlQuater.SelectedValue;
           // List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);


            List<dbo_SalesTargetClass> SalesTarget = new List<dbo_SalesTargetClass>();

            string  User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

            if (user_class.User_Group_ID == "CP Meiji")
            {
                if (CV_Code == "")
                {
                    string region = user_class.Region;
                    string[] regions = region.Split(',');
                    //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                    List<string> item_CVCode = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.CV_Code));
                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));

                    if (item_CVCode.Count > 0)
                    {
                        //หา SalesTarget  จาก  ผู้รับผิดชอบ
                        foreach (string cvcode in item_CVCode)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(cvcode, Month, Quarter, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }
                    else
                    {
                        //หา SalesTarget  จาก  region
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                            //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                            List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(_cv.CV_Code, Month, Quarter, txtSearchYear.Text);

                            foreach (dbo_SalesTargetClass rpt in item1)
                            {
                                SalesTarget.Add(rpt);
                            }

                        }
                    }

                }
                else
                {
                    SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                }

            }
            else
            {
                //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
            }

            GridViewSaleTarget.DataSource = SalesTarget;
            GridViewSaleTarget.DataBind();

            ViewState["NewEditIndex"] = e.NewEditIndex;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewSaleTarget_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int RowIndex = Convert.ToInt32((e.RowIndex).ToString());
            Label _Sales_Target_ID = (Label)GridViewSaleTarget.Rows[RowIndex].FindControl("lblSales_Target_ID");

            DropDownList _ddlAgentName = (DropDownList)GridViewSaleTarget.Rows[RowIndex].FindControl("ddlAgentName");
            TextBox _txtEditYear = (TextBox)GridViewSaleTarget.Rows[RowIndex].FindControl("txtEditYear");
            DropDownList _ddlEditMonth = (DropDownList)GridViewSaleTarget.Rows[RowIndex].FindControl("ddlEditMonth");
            TextBox _txtEditSales_Target = (TextBox)GridViewSaleTarget.Rows[RowIndex].FindControl("txtEditSales_Target");


            dbo_SalesTargetClass st = dbo_SalesTargetDataClass.Select_Record(_Sales_Target_ID.Text);


            //st =

            // _ddlAgentName.SelectedValue, _txtEditYear.Text, _ddlEditMonth.Text);

            //st.Sales_Target_ID = GenerateID.Sales_Target_ID();
            //st.CV_Code = (_ddlAgentName.SelectedIndex == 0 ? null : _ddlAgentName.SelectedValue);
            //st.Year = _txtEditYear.Text;
            //st.Month = (_ddlEditMonth.SelectedIndex == 0 ? null : _ddlEditMonth.SelectedValue);
            if (_txtEditSales_Target.Text == "")
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกเป้ายอดขาย");
            }
            else
            {
                st.Sales_Target = Decimal.Parse(_txtEditSales_Target.Text.Replace(",", string.Empty));

                bool success = false;

                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                success = dbo_SalesTargetDataClass.Update(st);


                if (success)
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("บันทึกสำเร็จ!");
                }

                String CV_Code = ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue;
                String Month = ddlMonth.SelectedIndex == 0 ? string.Empty : ddlMonth.SelectedValue;
                String Quarter = ddlQuater.SelectedIndex == 0 ? string.Empty : ddlQuater.SelectedValue;
                GridViewSaleTarget.EditIndex = -1;
                //List<dbo_SalesTargetClass> item_value = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                List<dbo_SalesTargetClass> SalesTarget = new List<dbo_SalesTargetClass>();

                 User_ID = Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                if (user_class.User_Group_ID == "CP Meiji")
                {
                    if (CV_Code == "")
                    {
                        string region = user_class.Region;
                        string[] regions = region.Split(',');
                        //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                        List<string> item_CVCode = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.CV_Code));
                        List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));

                        if (item_CVCode.Count > 0)
                        {
                            //หา SalesTarget  จาก  ผู้รับผิดชอบ
                            foreach (string cvcode in item_CVCode)
                            {
                                //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                                //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                                List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(cvcode, Month, Quarter, txtSearchYear.Text);

                                foreach (dbo_SalesTargetClass rpt in item1)
                                {
                                    SalesTarget.Add(rpt);
                                }

                            }
                        }
                        else
                        {
                            //หา SalesTarget  จาก  region
                            foreach (dbo_AgentClass _cv in cv_code2)
                            {
                                //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                                //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                                List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(_cv.CV_Code, Month, Quarter, txtSearchYear.Text);

                                foreach (dbo_SalesTargetClass rpt in item1)
                                {
                                    SalesTarget.Add(rpt);
                                }

                            }
                        }

                    }
                    else
                    {
                        SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                    }

                }
                else
                {
                    //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                    SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                }


                GridViewSaleTarget.DataSource = SalesTarget;
                GridViewSaleTarget.DataBind();

                // InitialSalesTarget();
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewSaleTarget_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            if (e.CommandName == "AddNew")
            {
                DropDownList _ddlFooterAgent = (DropDownList)GridViewSaleTarget.FooterRow.FindControl("ddlFooterAgent");
                TextBox _txtFooterYear = (TextBox)GridViewSaleTarget.FooterRow.FindControl("txtFooterYear");
                DropDownList _ddlFooterMonth = (DropDownList)GridViewSaleTarget.FooterRow.FindControl("ddlFooterMonth");
                TextBox _txtFooterSales_Target = (TextBox)GridViewSaleTarget.FooterRow.FindControl("txtFooterSales_Target");


                dbo_SalesTargetClass st = new dbo_SalesTargetClass();

                if (_txtFooterYear.Text == "")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณาระบุปี");
                }
                else if (_ddlFooterMonth.SelectedIndex == 0)
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณาระบุเดือน");
                }
                else if (_txtFooterSales_Target.Text == "")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณากรอกเป้ายอดขาย");
                }
                else
                {

                    dbo_SalesTargetClass target = new dbo_SalesTargetClass();
                    target = dbo_SalesTargetDataClass.Select_Record(_ddlFooterAgent.SelectedValue, _txtFooterYear.Text, _ddlFooterMonth.Text);
                    if (target != null)
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("ไม่สามารถระบุเป้ายอดขายซ้ำได้");
                    }
                    else
                    {
                        st.Sales_Target_ID = GenerateID.Sales_Target_ID();
                        st.CV_Code = _ddlFooterAgent.SelectedValue;
                        st.Year = _txtFooterYear.Text;
                        st.Month = _ddlFooterMonth.SelectedValue;
                        st.Sales_Target = Decimal.Parse(_txtFooterSales_Target.Text.Replace(",", string.Empty));


                        bool succes = false;
                        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                        succes = dbo_SalesTargetDataClass.Add(st);

                        if (succes)
                        {
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            Show("บันทึกสำเร็จ!");
                        }

                        String CV_Code = ddlSearchAgentName.SelectedIndex == 0 ? string.Empty : ddlSearchAgentName.SelectedValue;
                        String Month = ddlMonth.SelectedIndex == 0 ? string.Empty : ddlMonth.SelectedValue;
                        String Quarter = ddlQuater.SelectedIndex == 0 ? string.Empty : ddlQuater.SelectedValue;
                        GridViewSaleTarget.ShowFooter = false;


                        //List<dbo_SalesTargetClass> item_value = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);


                        List<dbo_SalesTargetClass> SalesTarget = new List<dbo_SalesTargetClass>();

                        User_ID = Request.Cookies["User_ID"].Value;
                        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
                        List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Active", string.Empty);

                        if (user_class.User_Group_ID == "CP Meiji")
                        {
                            if (CV_Code == "")
                            {
                                string region = user_class.Region;
                                string[] regions = region.Split(',');
                                //string[] CVCode_tmp = agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f.CV_Code).ToArray();

                                List<string> item_CVCode = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.CV_Code));
                                List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => regions.Contains(f.Location_Region)).Select(f => f));

                                if (item_CVCode.Count > 0)
                                {
                                    //หา SalesTarget  จาก  ผู้รับผิดชอบ
                                    foreach (string cvcode in item_CVCode)
                                    {
                                        //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                                        //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                                        List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(cvcode, Month, Quarter, txtSearchYear.Text);

                                        foreach (dbo_SalesTargetClass rpt in item1)
                                        {
                                            SalesTarget.Add(rpt);
                                        }

                                    }
                                }
                                else
                                {
                                    //หา SalesTarget  จาก  region
                                    foreach (dbo_AgentClass _cv in cv_code2)
                                    {
                                        //List<RPT_CUSTOMER_DEBT_41223> _inrpt = Reports.RPT_CUSTOMER_DEBT_41223(_cv.Location_Region, _cv.CV_Code, SPName, DebtDate_From, DebtDate_To, Status, string.Empty, string.Empty, string.Empty, string.Empty);
                                        //List<RPT_ADJ_STOCK__4128> _inrpt = Reports.RPT_ADJ_STOCK__4128(_cv.Location_Region, _cv.CV_Code, CountStockDate_From, CountStockDate_To, ProductGroup, Size, string.Empty, string.Empty, string.Empty, string.Empty);
                                        List<dbo_SalesTargetClass> item1 = dbo_SalesTargetDataClass.Search2(_cv.CV_Code, Month, Quarter, txtSearchYear.Text);

                                        foreach (dbo_SalesTargetClass rpt in item1)
                                        {
                                            SalesTarget.Add(rpt);
                                        }

                                    }
                                }

                            }
                            else
                            {
                                SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                            }

                        }
                        else
                        {
                            //List<dbo_SalesTargetClass> item = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quater, txtSearchYear.Text);
                            SalesTarget = dbo_SalesTargetDataClass.Search2(CV_Code, Month, Quarter, txtSearchYear.Text);
                        }


                        GridViewSaleTarget.DataSource = SalesTarget;
                        GridViewSaleTarget.DataBind();

                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        // InitialSalesTarget();
                    }
                }
            }
            else if (e.CommandName == "Delete")
            {
                //LinkButton lnkView = (LinkButton)e.CommandSource;
                //string Sales_Target_ID = lnkView.CommandArgument;

                //dbo_SalesTargetDataClass.Delete(Sales_Target_ID);

                //Show("ลบข้อมูลสำเร็จ");

                //InitialSalesTarget();


            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion

    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewSaleTarget.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewSaleTarget.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewSaleTarget_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewSaleTarget.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");
        

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewSaleTarget.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewSaleTarget.PageIndex)
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
            int currentPage = GridViewSaleTarget.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewSaleTarget.PageCount.ToString();

        }
    }

    private string CV_Month(string Month)
    {
        string strMonth = "";

        if (Month == "01")
        {
            strMonth = "มกราคม";
        }
        else if (Month == "02")
        {
            strMonth = "กุมภาพันธ์";
        }
        else if (Month == "03")
        {
            strMonth = "มีนาคม";
        }
        else if (Month == "04")
        {
            strMonth = "เมษายน";
        }
        else if (Month == "05")
        {
            strMonth = "พฤษภาคม";
        }
        else if (Month == "06")
        {
            strMonth = "มิถุนายน";
        }
        else if (Month == "07")
        {
            strMonth = "กรกฎาคม";
        }
        else if (Month == "08")
        {
            strMonth = "สิงหาคม";
        }
        else if (Month == "09")
        {
            strMonth = "กันยายน";
        }
        else if (Month == "10")
        {
            strMonth = "ตุลาคม";
        }
        else if (Month == "11")
        {
            strMonth = "พฤศจิกายน";
        }
        else if (Month == "12")
        {
            strMonth = "ธันวาคม";
        }

        return strMonth;


    }
}