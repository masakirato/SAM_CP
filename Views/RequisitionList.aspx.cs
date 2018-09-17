#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
#endregion

public partial class Views_RequisitionList : System.Web.UI.Page
{
    #region Private Var
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtSearchBeginRequisition_Date.Text = DateTime.Now.ToShortDateString();
                txtSearchEndTransaction_Date.Text = DateTime.Now.ToShortDateString();

                txtGrand_Total_Qty.Text = "0";
                txtGrand_Total_Amount.Text = "0";

                ddlUserSP.Attributes.Add("onchange", "myApp.showPleaseWait();");
                txtRequisition_Date.Attributes.Add("onchange", "myApp.showPleaseWait();");

                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                if (user_class.User_Group_ID == "Agent")
                {
                    SetUpDrowDownList();
                    btnSearch_Click(sender, e);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);
                }
                
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ButtonAddNew_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            dbo_CountStockClass stock = dbo_CountStockDataClass.Search(null, string.Empty, string.Empty, user_class.CV_CODE).FirstOrDefault(f => f.Status == "รอการคอนเฟิร์ม");

            if (stock != null)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ระหว่างการนับสต๊อก ไม่สามารถเบิกสินค้าได้");
            }
            else
            {
                GetDetailsDataToForm(string.Empty, string.Empty, string.Empty);
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void TxtId_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            int all_sum = 0;
            Decimal all_sum_dec = 0;

            foreach (GridViewRow currentRow in GridViewRequisition_1.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");

                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    if (int.Parse(lbl_Total_Qty_.Text) > 0)
                    {
                        Decimal tmp_dec = 0;

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        tmp_dec = Decimal.Parse(string.IsNullOrEmpty(lbl_PricePerUnit_.Text) ? "0" : lbl_PricePerUnit_.Text);

                        all_sum_dec += (tmp_dec * Decimal.Parse(string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? "0" : lbl_Total_Qty_.Text));
                        all_sum += string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? 0 : int.Parse(lbl_Total_Qty_.Text);

                    }
                }               
            }

            foreach (GridViewRow currentRow in GridViewRequisition_2.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");

                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    if (int.Parse(lbl_Total_Qty_.Text) > 0)
                    {
                        Decimal tmp_dec = 0;

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        tmp_dec = Decimal.Parse(string.IsNullOrEmpty(lbl_PricePerUnit_.Text) ? "0" : lbl_PricePerUnit_.Text);

                        all_sum_dec += (tmp_dec * Decimal.Parse(string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? "0" : lbl_Total_Qty_.Text));
                        all_sum += string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? 0 : int.Parse(lbl_Total_Qty_.Text);

                    }
                }
            }


            foreach (GridViewRow currentRow in GridViewRequisition_3.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");

                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    if (int.Parse(lbl_Total_Qty_.Text) > 0)
                    {

                        Decimal tmp_dec = 0;

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        tmp_dec = Decimal.Parse(string.IsNullOrEmpty(lbl_PricePerUnit_.Text) ? "0" : lbl_PricePerUnit_.Text);

                        all_sum_dec += (tmp_dec * Decimal.Parse(string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? "0" : lbl_Total_Qty_.Text));
                        all_sum += string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? 0 : int.Parse(lbl_Total_Qty_.Text);

                    }
                }
            }


            foreach (GridViewRow currentRow in GridViewRequisition_4.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");

                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    if (int.Parse(lbl_Total_Qty_.Text) > 0)
                    {

                        Decimal tmp_dec = 0;

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        tmp_dec = Decimal.Parse(string.IsNullOrEmpty(lbl_PricePerUnit_.Text) ? "0" : lbl_PricePerUnit_.Text);

                        all_sum_dec += (tmp_dec * Decimal.Parse(string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? "0" : lbl_Total_Qty_.Text));
                        all_sum += string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? 0 : int.Parse(lbl_Total_Qty_.Text);

                    }
                }
            }


            foreach (GridViewRow currentRow in GridViewRequisition_5.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");

                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    if (int.Parse(lbl_Total_Qty_.Text) > 0)
                    {

                        Decimal tmp_dec = 0;

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");
                        tmp_dec = Decimal.Parse(string.IsNullOrEmpty(lbl_PricePerUnit_.Text) ? "0" : lbl_PricePerUnit_.Text);

                        all_sum_dec += (tmp_dec * Decimal.Parse(string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? "0" : lbl_Total_Qty_.Text));
                        all_sum += string.IsNullOrEmpty(lbl_Total_Qty_.Text) ? 0 : int.Parse(lbl_Total_Qty_.Text);

                    }
                }
            }

            txtGrand_Total_Qty.Text = all_sum.ToString("#,##0");
            txtGrand_Total_Amount.Text = all_sum_dec.ToString("#,##0.#0");

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            if (btnSave.Text == "แก้ไข")
            {
                //dbo_RequisitionClass req = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text, txtTime_No.Text);
                dbo_RequisitionClass req = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
                if (req.Status != "ยังไม่เคลียร์เงิน")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ใบเบิกสินค้าได้ทำการเคลียร์เงินแล้ว ไม่สามารถแก้ไขข้อมูลได้");
                }
                else if (req.Status == "ยังไม่เคลียร์เงิน" && req.Time_No != txtTime_No.Text)
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ไม่สามารถแก้ไขใบเบิกสินค้า ที่ไม่ใช่การเบิกสินค้าครั้งล่าสุดได้");
                }
                else
                {
                    List<dbo_RequisitionClearingClass> listReqClearing = dbo_RequisitionClearingDataClass.SearchBySPID(ddlUserSP.SelectedValue);

                    foreach (dbo_RequisitionClearingClass item in listReqClearing)
                    {
                        //dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(item.Clearing_No);
                        if (item.Status == "1")
                        {
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            Show("ไม่สามารถแก้ไขใบเบิกได้ เนื่องจากกำลังทำการเคลียร์เงินอยู่");
                            //txtRequisition_No.Text = string.Empty;
                            //txtTime_No.Text = string.Empty;
                            //ddlUserSP.SelectedIndex = 0;
                            return;
                        }
                    }

                    //dbo_RequisitionClass req = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);
                    //if (req.Status != "ยังไม่เคลียร์เงิน")
                    //{
                    //    Show("ใบเบิกสินค้าได้ทำการเคลียร์เงินแล้ว ไม่สามารถแก้ไขข้อมูลได้");
                    //}
                    //else
                    //{
                    List<dbo_RequisitionClass> req_ = dbo_RequisitionDataClass.Search(null, null, req.User_ID, string.Empty);
                    dbo_RequisitionClass reqMax = req_.OrderByDescending(f => f.Requisition_No).First();
                    if (reqMax.Requisition_No == req.Requisition_No)
                    {
                        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                        dbo_CountStockClass stockCheck = dbo_CountStockDataClass.Search(null, string.Empty, string.Empty, user_class.CV_CODE).FirstOrDefault(f => f.Status == "รอการคอนเฟิร์ม");

                        if (stockCheck != null)
                        {
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            Show("ระหว่างการนับสต๊อก ไม่สามารถแก้ไขใบเบิกสินค้า");
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(500);
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                            GetDetailsDataToForm(txtRequisition_No.Text, txtTime_No.Text, "Edit");
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("ไม่สามารถแก้ไขใบเบิกสินค้า ที่ไม่ใช่การเบิกสินค้าครั้งล่าสุดได้");
                    }
                }
            }
            else
            {
                if (ddlUserSP.SelectedIndex > 0)
                {
                    if (btnSaveMode.Value == "บันทึก")
                    {
                        InsertRecord();
                    }
                    else
                    {
                        UpdateRecord();
                    }

                    pnlForm.Visible = false;
                    pnlGrid.Visible = true;

                    ddlUserSP.ClearSelection();
                    ddlUserRepresent.ClearSelection();

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("บันทึกสำเร็จ");
                    SearchSubmit();
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("กรุณาระบุผู้เบิกสินค้า");                   
                }
            }
        }
        catch
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        pnlForm.Visible = false;
        pnlGrid.Visible = true;

        ddlUserSP.ClearSelection();
        ddlUserRepresent.ClearSelection();

        System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        SearchSubmit();
        //System.Threading.Thread.Sleep(1000);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        try
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            ddl_SPName.ClearSelection();
            txtSearchBeginRequisition_Date.Text = DateTime.Now.ToShortDateString();
            txtSearchEndTransaction_Date.Text = DateTime.Now.ToShortDateString();

           if(GridViewSearchRequisition.Rows.Count > 0)
            {
                List<dbo_RequisitionClass> item = new List<dbo_RequisitionClass>();
                GridViewSearchRequisition.DataSource = item;
                GridViewSearchRequisition.DataBind();
                GridViewSearchRequisition.Visible = true;
            }
            
           
            pnlNoRec.Visible = false;

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void ddlUserSP_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            if (string.IsNullOrEmpty(txtRequisition_Date.Text))
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุวันที่เบิก");
                //  ddlUserSP.ClearSelection();
            }
            else if (!string.IsNullOrEmpty(ddlUserSP.SelectedValue))
            {
                txtGrand_Total_Qty.Text = "0";
                txtGrand_Total_Amount.Text = "0";

                List<dbo_RequisitionClass> req = dbo_RequisitionDataClass.Search(null, null, ddlUserSP.SelectedValue, "");
                DateTime? today = DateTime.Now.Date;
                //persons.GroupBy(x => x.PersonId).Select(x => x)
                List<string> reqList = req.Where(s => s.Status == "1")
                               .Select(s => s.Requisition_No)
                               .Distinct()
                               .ToList();
                //&& s.Requisition_Date < today.Value

                if (reqList.Count < 1)
                {
                    #region Generate REQ NO
                    DateTime? now = DateTime.Parse(txtRequisition_Date.Text);

                    //List<dbo_RequisitionClass> listofrequisition = dbo_RequisitionDataClass.Search("", "", ddlUserSP.SelectedValue, now).Where(f => f.Status == "ยังไม่เคลียร์เงิน").ToList();
                    List<dbo_RequisitionClass> listofrequisition = dbo_RequisitionDataClass.Search("", "", ddlUserSP.SelectedValue, now).Where(f => f.Status == "1").ToList();
                    //List<dbo_RequisitionClass> listofrequisition = dbo_RequisitionDataClass.Search("", "", ddlUserSP.SelectedValue, now).ToList();

                    if (listofrequisition.Count == 0)
                    {

                        List<dbo_RequisitionClearingClass> listReqClearing = dbo_RequisitionClearingDataClass.SearchBySPID(ddlUserSP.SelectedValue);

                        foreach (dbo_RequisitionClearingClass item in listReqClearing)
                        {
                            //dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(item.Clearing_No);
                            if (item.Status == "1")
                            {
                                Show("ไม่สามารถสร้างใบเบิกได้ เนื่องจากกำลังทำการเคลียร์เงินอยู่");
                                txtRequisition_No.Text = string.Empty;
                                txtTime_No.Text = string.Empty;
                                ddlUserSP.SelectedIndex = 0;
                                GetDetailsDataToForm(string.Empty, string.Empty, string.Empty);
                                return;
                            }
                        }

                        txtRequisition_No.Text = GenerateID.Requisition_No(ViewState["CV_Code"].ToString());
                        txtTime_No.Text = "1";
                    }
                    else
                    {

                        List<dbo_RequisitionClearingClass> listReqClearing = dbo_RequisitionClearingDataClass.SearchBySPID(ddlUserSP.SelectedValue);

                        foreach (dbo_RequisitionClearingClass item in listReqClearing)
                        {
                            //dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(item.Clearing_No);
                            if (item.Status == "1")
                            {
                                Show("ไม่สามารถสร้างใบเบิกได้ เนื่องจากกำลังทำการเคลียร์เงินอยู่");
                                txtRequisition_No.Text = string.Empty;
                                txtTime_No.Text = string.Empty;
                                ddlUserSP.SelectedIndex = 0;
                                GetDetailsDataToForm(string.Empty, string.Empty, string.Empty);
                                return;
                            }
                        }

                        int max = 1;
                        foreach (dbo_RequisitionClass item in listofrequisition)
                        {
                            if (int.Parse(item.Time_No) >= max)
                            {
                                max = int.Parse(item.Time_No);
                            }
                            txtRequisition_No.Text = item.Requisition_No;
                        }

                        txtTime_No.Text = (max + 1).ToString();
                        logger.Debug("txtTime_No.Text " + txtTime_No.Text);
                    }

                    String User_ID = ddlUserSP.SelectedValue;

                    DateTime? Requisition_Date = DateTime.Parse(txtRequisition_Date.Text);

                    List<dbo_ProductClass> item1 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "นมสดพาสเจอร์ไรส์");
                    Session["GetProduct_Quantity_tab_1"] = item1;
                    GridViewRequisition_1.DataSource = item1;
                    GridViewRequisition_1.DataBind();

                    List<dbo_ProductClass> item2 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "นมเปรี้ยว");
                    Session["GetProduct_Quantity_tab_2"] = item2;
                    GridViewRequisition_2.DataSource = item2;
                    GridViewRequisition_2.DataBind();

                    List<dbo_ProductClass> item3 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "โยเกิร์ตเมจิ");
                    Session["GetProduct_Quantity_tab_3"] = item3;
                    GridViewRequisition_3.DataSource = item3;
                    GridViewRequisition_3.DataBind();

                    List<dbo_ProductClass> item4 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "นมเปรี้ยวไพเกน");
                    Session["GetProduct_Quantity_tab_4"] = item4;
                    GridViewRequisition_4.DataSource = item4;
                    GridViewRequisition_4.DataBind();

                    List<dbo_ProductClass> item5 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "อื่นๆ");
                    Session["GetProduct_Quantity_tab_5"] = item5;
                    GridViewRequisition_5.DataSource = item5;
                    GridViewRequisition_5.DataBind();

                    //btnSaveMode.Value = "บันทึก";
                    //TxtId_TextChanged(null, null);
                    #endregion
                }
                else
                {
                    //List<string> reqCheck = req.Where(s => s.Status == "1" && s.Requisition_Date < DateTime.Parse(txtRequisition_Date.Text))
                    //           .Select(s => s.Requisition_No)
                    //           .Distinct()
                    //           .ToList();

                    List<string> reqCheck = req.Where(s => s.Status == "1" && s.Requisition_Date < DateTime.Parse(txtRequisition_Date.Text))
                              .Select(s => s.Requisition_No)
                              .Distinct()
                              .ToList();

                    List<string> reqCheckTemp = req.Where(s => s.Status == "1" && s.Requisition_Date < DateTime.Parse(txtRequisition_Date.Text))
                             .Select(s => s.Requisition_No)
                             .Distinct()
                             .ToList();

                    foreach (string strReq in reqCheckTemp)
                    {
                        List<dbo_RequisitionClearingClass> reqClearing = dbo_RequisitionClearingDataClass.SearchByReqNo(strReq);
                        if (reqClearing.Count > 0)
                        {
                            dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(reqClearing[0].Clearing_No);
                            if (clearing != null)
                            {
                                if ((clearing.Status == "2") || (clearing.Status == "3"))
                                {
                                    reqCheck.Remove(strReq);
                                }
                            }
                        }
                    }

                    if (reqCheck.Count > 0 )
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("มีใบเบิกสินค้าที่ยังไม่ได้ทำการเคลียร์เงิน 1 ใบ กรุณาทำการเคลียร์เงินก่อนทำการเบิกสินค้า");
                        GetDetailsDataToForm(string.Empty, string.Empty, string.Empty);
                    }
                    else
                    {
                        #region Generate REQ NO
                        DateTime? now = DateTime.Parse(txtRequisition_Date.Text);

                        //List<dbo_RequisitionClass> listofrequisition = dbo_RequisitionDataClass.Search("", "", ddlUserSP.SelectedValue, now).Where(f => f.Status == "ยังไม่เคลียร์เงิน").ToList();
                        List<dbo_RequisitionClass> listofrequisition = dbo_RequisitionDataClass.Search("", "", ddlUserSP.SelectedValue, now).Where(f => f.Status == "1").ToList();
                        //List<dbo_RequisitionClass> listofrequisition = dbo_RequisitionDataClass.Search("", "", ddlUserSP.SelectedValue, now).ToList();

                        if (listofrequisition.Count == 0)
                        {

                            List<dbo_RequisitionClearingClass> listReqClearing = dbo_RequisitionClearingDataClass.SearchBySPID(ddlUserSP.SelectedValue);

                            foreach (dbo_RequisitionClearingClass item in listReqClearing)
                            {
                                //dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(item.Clearing_No);
                                if (item.Status == "1")
                                {
                                    Show("ไม่สามารถสร้างใบเบิกได้ เนื่องจากกำลังทำการเคลียร์เงินอยู่");
                                    txtRequisition_No.Text = string.Empty;
                                    txtTime_No.Text = string.Empty;
                                    ddlUserSP.SelectedIndex = 0;
                                    GetDetailsDataToForm(string.Empty, string.Empty, string.Empty);
                                    return;
                                }
                            }

                            txtRequisition_No.Text = GenerateID.Requisition_No(ViewState["CV_Code"].ToString());
                            txtTime_No.Text = "1";
                        }
                        else
                        {

                            List<dbo_RequisitionClearingClass> listReqClearing = dbo_RequisitionClearingDataClass.SearchBySPID(ddlUserSP.SelectedValue);

                            foreach (dbo_RequisitionClearingClass item in listReqClearing)
                            {
                                //dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(item.Clearing_No);
                                if (item.Status == "1")
                                {
                                    Show("ไม่สามารถสร้างใบเบิกได้ เนื่องจากกำลังทำการเคลียร์เงินอยู่");
                                    txtRequisition_No.Text = string.Empty;
                                    txtTime_No.Text = string.Empty;
                                    ddlUserSP.SelectedIndex = 0;
                                    GetDetailsDataToForm(string.Empty, string.Empty, string.Empty);
                                    return;
                                }
                            }

                            int max = 1;
                            foreach (dbo_RequisitionClass item in listofrequisition)
                            {
                                if (int.Parse(item.Time_No) >= max)
                                {
                                    max = int.Parse(item.Time_No);
                                }
                                txtRequisition_No.Text = item.Requisition_No;
                            }

                            txtTime_No.Text = (max + 1).ToString();
                            logger.Debug("txtTime_No.Text " + txtTime_No.Text);
                        }

                        String User_ID = ddlUserSP.SelectedValue;

                        DateTime? Requisition_Date = DateTime.Parse(txtRequisition_Date.Text);

                        List<dbo_ProductClass> item1 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "นมสดพาสเจอร์ไรส์");
                        Session["GetProduct_Quantity_tab_1"] = item1;
                        GridViewRequisition_1.DataSource = item1;
                        GridViewRequisition_1.DataBind();

                        List<dbo_ProductClass> item2 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "นมเปรี้ยว");
                        Session["GetProduct_Quantity_tab_2"] = item2;
                        GridViewRequisition_2.DataSource = item2;
                        GridViewRequisition_2.DataBind();

                        List<dbo_ProductClass> item3 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "โยเกิร์ตเมจิ");
                        Session["GetProduct_Quantity_tab_3"] = item3;
                        GridViewRequisition_3.DataSource = item3;
                        GridViewRequisition_3.DataBind();

                        List<dbo_ProductClass> item4 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "นมเปรี้ยวไพเกน");
                        Session["GetProduct_Quantity_tab_4"] = item4;
                        GridViewRequisition_4.DataSource = item4;
                        GridViewRequisition_4.DataBind();

                        List<dbo_ProductClass> item5 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_Date, "อื่นๆ");
                        Session["GetProduct_Quantity_tab_5"] = item5;
                        GridViewRequisition_5.DataSource = item5;
                        GridViewRequisition_5.DataBind();

                        //btnSaveMode.Value = "บันทึก";
                        //TxtId_TextChanged(null, null);
                        #endregion
                        
                    }                                 
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    protected void txtRequisition_Date_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (txtRequisition_Date.Text != "")
        {

            DateTime tmp_date = DateTime.Parse(txtRequisition_Date.Text);
            DateTime dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            if (DateTime.Compare(dateNow, tmp_date) <= 0)
            {
                ddlUserSP_SelectedIndexChanged(null, null);
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ข้อมูลไม่ถูกต้อง");
                txtRequisition_Date.Text = DateTime.Now.ToShortDateString();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("กรุณาระบุวันที่เบิก");
            txtRequisition_Date.Text = DateTime.Now.ToShortDateString();
        }

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            List<RPT_Get_Requisition_No> rt_rqt1 = new List<RPT_Get_Requisition_No>();
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);
            string SPName = ddl_SPName.SelectedItem.ToString();
            if(ddl_SPName.SelectedIndex != 0)
            {
                rt_rqt1 = Reports.RPT_Get_Requisition_No_Search(string.Empty, SPName.Substring(0, 11));
            }
            else
            {
                rt_rqt1 = Reports.RPT_Get_Requisition_No_Search(string.Empty,string.Empty);
            }
            int rt_rqt_count = rt_rqt1.Select(f => f.clm_1).Count();

            if (ddl_SPName.SelectedValue != "")
            {
               
                if (rt_rqt_count > 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);                  

                    string url = "../Report_From/ViewsReport.aspx?RPT=Requisition_No&AgentName=" + clsdbo_Agent.AgentName.ToString() + "&SPName=" + SPName + "&CV_Code=" + user_class.CV_CODE;
                    string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    System.Threading.Thread.Sleep(1000);
                    Show("ไม่พบข้อมูล");

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                System.Threading.Thread.Sleep(1000);
                Show("กรุณาเลือกพนักงานขาย");
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
    }

    #endregion

    #region GridView Row DataBound
    protected void GridViewRequisition_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    ((HtmlGenericControl)e.Row.FindControl("divProvider")).Attributes.Add("onmouseover", "showImage('" + e.Row.FindControl("imgProduct").ClientID + "')");
        //    ((HtmlGenericControl)e.Row.FindControl("divProvider")).Attributes.Add("onmouseover", "hideImage('" + e.Row.FindControl("imgProduct").ClientID + "')");
        //}
       
        if (e.Row.DataItem != null)
        {
            //DateTime? Requisition_Date = Convert.ToDateTime(txtRequisition_Date.Text);
            //List<dbo_ProductClass> item1 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(ddlUserSP.SelectedValue, Requisition_Date, "นมสดพาสเจอร์ไรส์");

            
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label lbl_Sub_Total_Qty = e.Row.FindControl("lbl_Sub_Total_Qty") as Label;
            Label lbl_Previous_Balance_Qty_ = e.Row.FindControl("lbl_Previous_Balance_Qty") as Label;
            TextBox lbl_Total_Qty_ = e.Row.FindControl("lbl_Total_Qty") as TextBox;
            Label lbl_PricePerUnit_ = e.Row.FindControl("lbl_PricePerUnit") as Label;
            //Label lbl_stock_end_ = e.Row.FindControl("lbl_Stock_end") as Label;
            HiddenField hfEnd_Sock_ = e.Row.FindControl("hfEnd_Sock1") as HiddenField;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID +","+ hfEnd_Sock_.ClientID +");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");
        }
    }

    protected void GridViewRequisition_2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label lbl_Sub_Total_Qty = e.Row.FindControl("lbl_Sub_Total_Qty") as Label;
            Label lbl_Previous_Balance_Qty_ = e.Row.FindControl("lbl_Previous_Balance_Qty") as Label;
            TextBox lbl_Total_Qty_ = e.Row.FindControl("lbl_Total_Qty") as TextBox;

            Label lbl_PricePerUnit_ = e.Row.FindControl("lbl_PricePerUnit") as Label;


            //textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            //textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");
            //textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");
            HiddenField hfEnd_Sock_ = e.Row.FindControl("hfEnd_Sock1") as HiddenField;
            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");

        }
    }

    protected void GridViewRequisition_3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label lbl_Sub_Total_Qty = e.Row.FindControl("lbl_Sub_Total_Qty") as Label;
            Label lbl_Previous_Balance_Qty_ = e.Row.FindControl("lbl_Previous_Balance_Qty") as Label;
            TextBox lbl_Total_Qty_ = e.Row.FindControl("lbl_Total_Qty") as TextBox;

            Label lbl_PricePerUnit_ = e.Row.FindControl("lbl_PricePerUnit") as Label;


            //textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            //textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");
            //textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");
            HiddenField hfEnd_Sock_ = e.Row.FindControl("hfEnd_Sock1") as HiddenField;
            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");
        }
    }

    protected void GridViewRequisition_4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label lbl_Sub_Total_Qty = e.Row.FindControl("lbl_Sub_Total_Qty") as Label;
            Label lbl_Previous_Balance_Qty_ = e.Row.FindControl("lbl_Previous_Balance_Qty") as Label;
            TextBox lbl_Total_Qty_ = e.Row.FindControl("lbl_Total_Qty") as TextBox;

            Label lbl_PricePerUnit_ = e.Row.FindControl("lbl_PricePerUnit") as Label;


            //textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            //textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");
            //textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");
            HiddenField hfEnd_Sock_ = e.Row.FindControl("hfEnd_Sock1") as HiddenField;
            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");
        }
    }

    protected void GridViewRequisition_5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;
            Label lbl_Sub_Total_Qty = e.Row.FindControl("lbl_Sub_Total_Qty") as Label;
            Label lbl_Previous_Balance_Qty_ = e.Row.FindControl("lbl_Previous_Balance_Qty") as Label;
            TextBox lbl_Total_Qty_ = e.Row.FindControl("lbl_Total_Qty") as TextBox;

            Label lbl_PricePerUnit_ = e.Row.FindControl("lbl_PricePerUnit") as Label;


            //textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            //textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");
            //textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + ");");
            HiddenField hfEnd_Sock_ = e.Row.FindControl("hfEnd_Sock1") as HiddenField;
            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + lbl_Sub_Total_Qty.ClientID + "," + lbl_Previous_Balance_Qty_.ClientID + "," + lbl_Total_Qty_.ClientID + "," + lbl_PricePerUnit_.ClientID + "," + hfEnd_Sock_.ClientID + ");");
        }
    }
    #endregion

    #region GridView Row Command
    protected void GridViewSearchRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        logger.Info(e.CommandName);
        switch (e.CommandName)
        {
            case "View":

                LinkButton lnkView = (LinkButton)e.CommandSource;
                string Requisition_No = lnkView.CommandArgument;

                GridViewRow gRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lbl = (Label)gRow.FindControl("Label_Time_No");


                GetDetailsDataToForm(Requisition_No, lbl.Text, "View");

                break;
            case "Print":
                try
                {

                    LinkButton lnkRequisition_No = (LinkButton)e.CommandSource;
                    string Requisition_No_ = lnkRequisition_No.CommandArgument;

                    string url = "../Report_From/ViewsReport.aspx?RPT=Requisition_No&PRM=" + Requisition_No_;
                    string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";
                    
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    System.Threading.Thread.Sleep(2000);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);

                }
                catch (Exception)
                {

                }

                break;
            case "_Delete":
                try
                {
                    int RowIndex = Convert.ToInt32((e.CommandArgument).ToString());
                    LinkButton LinkButton_Requisition_No = (LinkButton)GridViewSearchRequisition.Rows[RowIndex].FindControl("LinkButton_Requisition_No");
                    Label Label_Time_No = (Label)GridViewSearchRequisition.Rows[RowIndex].FindControl("Label_Time_No");

                    dbo_RequisitionClass req = new dbo_RequisitionClass();

                    req = dbo_RequisitionDataClass.Select_Record(LinkButton_Requisition_No.Text); //requisition no & max time no.

                    if (req != null)
                    {
                        if (req.Status != "ยังไม่เคลียร์เงิน")
                        {
                            Show("ใบเบิกสินค้าได้ทำการเคลียร์เงินแล้ว ไม่สามารถลบได้");
                        }
                        else if (req.Status == "ยังไม่เคลียร์เงิน" && req.Time_No != Label_Time_No.Text)
                        {
                            Show("ไม่สามารถลบใบเบิกสินค้า ที่ไม่ใช่การเบิกสินค้าครั้งล่าสุดได้");
                        }
                        else
                        {
                            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
                            try
                            {
                                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                                dbo_CountStockClass stockCheck = dbo_CountStockDataClass.Search(null, string.Empty, string.Empty, user_class.CV_CODE).FirstOrDefault(f => f.Status == "รอการคอนเฟิร์ม");

                                if (stockCheck != null)
                                {
                                    logger.Debug(stockCheck.Status + " " + stockCheck.Count_No);
                                    Show("ระหว่างการนับสต๊อก ไม่สามารถลบใบเบิกสินค้า");
                                }
                                else
                                {

                                    //List<dbo_RequisitionClearingClass> clearing_class = dbo_RequisitionClearingDataClass.Search("", req.Requisition_No, "", null, null, null, null, user_class.CV_CODE);

                                    //if (clearing_class.Count > 0)
                                    //{
                                    //    if (clearing_class[0].Status != "ยังไม่เคลียร์เงิน")
                                    //    {
                                    //        Show("ไม่สามารถลบใบเบิกสินค้า เนื่องจากใบเบิกอยู่ระหว่างเคลียร์เงิน");
                                    //        return;
                                    //    }
                                    //}

                                    List<dbo_RequisitionClearingClass> listReqClearing = dbo_RequisitionClearingDataClass.SearchBySPID(req.User_ID);

                                    foreach (dbo_RequisitionClearingClass item in listReqClearing)
                                    {
                                        //dbo_ClearingClass clearing = dbo_ClearingDataClass.Select_Record(item.Clearing_No);
                                        if (item.Status == "1")
                                        {
                                            Show("ไม่สามารถลบใบเบิกสินค้า เนื่องจากกำลังทำการเคลียร์เงินอยู่");
                                            //txtRequisition_No.Text = string.Empty;
                                            //txtTime_No.Text = string.Empty;
                                            //ddlUserSP.SelectedIndex = 0;
                                            return;
                                        }
                                    }

                                    List<dbo_RequisitionClass> req_ = dbo_RequisitionDataClass.Search(null, null, req.User_ID, string.Empty);
                                    logger.Debug("req_.Count " + req_.Count);


                                    dbo_RequisitionClass reqMax = req_.OrderByDescending(f => f.Requisition_No).First();


                                    if (reqMax.Requisition_No == req.Requisition_No)
                                    {

                                        List<dbo_RequisitionDetailClass> detail = dbo_RequisitionDetailDataClass.SearchByTimeNo(LinkButton_Requisition_No.Text, Label_Time_No.Text);

                                        foreach (dbo_RequisitionDetailClass _detail in detail)
                                        {
                                            String Product_ID = _detail.Product_ID;

                                            List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);
                                            if (prev_stock.Count > 0)
                                            {
                                                dbo_StockClass stock = prev_stock[prev_stock.Count - 1];
                                                stock.Stock_Out = short.Parse((stock.Stock_Out - (_detail.Previous_Balance_Qty + _detail.Requisition_Qty)).ToString());
                                                stock.Stock_End = short.Parse((stock.Stock_End + (_detail.Previous_Balance_Qty + _detail.Requisition_Qty)).ToString());

                                                dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                                            }
                                        }

                                        dbo_DebtClass debt = dbo_DebtDataClass.SelectByRequisitionNo(txtRequisition_No.Text);
                                        if (debt != null)
                                        {
                                            List<dbo_RequisitionClass> items = dbo_RequisitionDataClass.SelectByRequisitionNo(req.Requisition_No);

                                            if (items.Count > 0)
                                            {
                                                if (items.Count == 1)
                                                {
                                                    dbo_DebtDataClass.Delete(debt.Debt_ID);
                                                }
                                                else
                                                {
                                                    if (debt.Debt_Amount != null && debt.Debt_Amount > 0)
                                                    {
                                                        dbo_DebtClass de = new dbo_DebtClass();
                                                        de.Debt_ID = debt.Debt_ID;

                                                        de.Debt_Amount = debt.Debt_Amount - req.Grand_Total_Amount;
                                                        de.Balance_Outstanding_Amount = debt.Balance_Outstanding_Amount - req.Grand_Total_Amount;
                                                        de.CV_Code = user_class.CV_CODE;
                                                        de.Debt_Date = DateTime.Now;
                                                        de.SP_ID = ddlUserSP.SelectedValue;

                                                        de.Requisition_No = txtRequisition_No.Text;

                                                        dbo_DebtDataClass.Update(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            List<dbo_DebtClass> listdebt = dbo_DebtDataClass.Search(req.User_ID, String.Empty);
                                            if (listdebt.Count > 0)
                                            {
                                                dbo_DebtClass maxDebt = listdebt.OrderByDescending(f => f.Debt_ID).First();
                                                logger.Debug("maxDebt.Debt_ID " + maxDebt.Debt_ID);
                                                dbo_DebtDataClass.Delete(maxDebt.Debt_ID);
                                            }
                                        }                                        

                                        if (req.Time_No == "1")
                                        {
                                            List<dbo_CommissionClass> comm = dbo_CommissionDataClass.Select_Record(LinkButton_Requisition_No.Text);
                                            if (comm.Count > 0)
                                            {
                                                dbo_CommissionClass maxComm = comm.OrderByDescending(f => f.Created_Date).First();
                                                dbo_CommissionDataClass.Delete(maxComm);
                                            }
                                        }
                                        else
                                        {
                                            List<dbo_CommissionClass> com = dbo_CommissionDataClass.Select_Record(req.Requisition_No);

                                            logger.Debug("com.Count " + com.Count);
                                            if (com.Count > 0)
                                            {
                                                com[0].Requisition_No = txtRequisition_No.Text;
                                                com[0].Commission = com[0].Commission - req.Total_Commission;
                                                com[0].Commission_Balance_Outstanding = com[0].Commission_Balance_Outstanding - req.Total_Commission;
                                                //
                                                int cPoint = (int)com[0].Point;
                                                int rPoint = (int)req.Tota_Point;
                                                cPoint = cPoint - rPoint;
                                                com[0].Point = (short?)cPoint;//(short)com[0].Point - (short)req.Tota_Point;
                                                // 
                                                com[0].Commission_Requisition_Status = 1;//
                                                dbo_CommissionDataClass.Update(com[0], HttpContext.Current.Request.Cookies["User_ID"].Value);
                                            }
                                        }

                                        dbo_StockMovementDataClass.DeleteByRefNo(LinkButton_Requisition_No.Text + '_' + Label_Time_No.Text);

                                        dbo_RequisitionDetailDataClass.DeletebyTimeNo(LinkButton_Requisition_No.Text, Label_Time_No.Text);
                                        dbo_RequisitionDataClass.Delete(req);

                                        Show("ลบข้อมูลสำเร็จ");

                                        pnlGrid.Visible = true;
                                        pnlForm.Visible = false;
                                        btnSearch_Click(null, null);
                                    }
                                    else
                                    {
                                        Show("ไม่สามารถลบใบเบิกสินค้า ที่ไม่ใช่การเบิกสินค้าครั้งล่าสุดได้");
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
                finally
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                }
                break;
        }
    }
    #endregion

    #region GridView Paging
    protected void GridViewSearchRequisition_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewSearchRequisition.PageIndex = e.NewPageIndex;
        GridViewSearchRequisition.DataBind();
        btnSearch_Click(sender, e);
    }
    #endregion

    #region Insert / Update

    private void InsertRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            dbo_RequisitionClass requistition = new dbo_RequisitionClass();
            requistition.Requisition_No = txtRequisition_No.Text;// GenerateID.Requisition_No(ViewState["CV_Code"].ToString());
            requistition.Requisition_Date = DateTime.Parse(txtRequisition_Date.Text);
            requistition.User_ID = ddlUserSP.SelectedValue;
            requistition.Time_No = txtTime_No.Text;
            requistition.Transaction_Date = DateTime.Now;
            requistition.Replace_Sales = ddlUserRepresent.SelectedItem.Value;

            requistition.Status = "1";

            requistition.Grand_Total_Amount = Decimal.Parse(txtGrand_Total_Amount.Text.Replace(",", string.Empty));
            requistition.Grand_Total_Qty = int.Parse(txtGrand_Total_Qty.Text.Replace(",", string.Empty));

            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(ddlUserSP.SelectedValue);

            List<dbo_RequisitionDetailClass> listofdetail = getRequisitionDetail(requistition.Requisition_No);

            logger.Debug("listofdetail.Count " + listofdetail.Count);


            foreach (dbo_RequisitionDetailClass detail in listofdetail)
            {
                dbo_RequisitionDetailDataClass.Add(detail, HttpContext.Current.Request.Cookies["User_ID"].Value);

                String Product_ID = detail.Product_ID;

                List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);

                logger.Debug("prev_stock.Count " + prev_stock.Count);

                if (prev_stock.Count > 0)
                {


                    dbo_StockClass stock = prev_stock[prev_stock.Count - 1];
                    logger.Debug("stock.Stock_on_Hand_ID " + stock.Stock_on_Hand_ID + " stock.Product_ID " + Product_ID);

                    //Old
                    //stock.Stock_Out = short.Parse((stock.Stock_Out + (detail.Previous_Balance_Qty + detail.Requisition_Qty)).ToString());
                    //stock.Stock_End = short.Parse((stock.Stock_End - (detail.Previous_Balance_Qty + detail.Requisition_Qty)).ToString());
                    //new 2018/05/25 by keng

                    stock.Stock_Out = short.Parse((stock.Stock_Out + (detail.Requisition_Qty)).ToString());
                    stock.Stock_End = short.Parse((stock.Stock_End - (detail.Requisition_Qty)).ToString());

                    stock.Product_ID = Product_ID;

                    dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                }
                else
                {

                }

                dbo_StockMovementClass movement = new dbo_StockMovementClass();
                movement.CV_CODE = user_class.CV_CODE;
                movement.Date = DateTime.Now;
                movement.Movement_Type = "เบิกSP";
                movement.Product_List_ID = Product_ID;
                //old
                //movement.Qty = short.Parse((detail.Previous_Balance_Qty + detail.Requisition_Qty).ToString());
                //New 2018/05/25 By Keng
                movement.Qty = short.Parse(( detail.Requisition_Qty).ToString());

                movement.Ref_No = requistition.Requisition_No + '_' + requistition.Time_No;

                dbo_StockMovementDataClass.Add(movement);
                logger.Debug("movement.Product_List_ID  " + movement.Product_List_ID);
            }

            requistition.Total_Commission = listofdetail.Sum(f => f.Commission);
            requistition.Tota_Point = Int16.Parse(listofdetail.Sum(f => f.Point).ToString());


            dbo_RequisitionClass debt_requis = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);

            dbo_RequisitionDataClass.Add(requistition, HttpContext.Current.Request.Cookies["User_ID"].Value);

            dbo_DebtClass debt = dbo_DebtDataClass.SelectByRequisitionNo(txtRequisition_No.Text);
            if (debt != null)
            {
                List<dbo_RequisitionClass> listReq = dbo_RequisitionDataClass.SelectByRequisitionNo(txtRequisition_No.Text);

                dbo_DebtClass de = new dbo_DebtClass();
                de.Debt_ID = debt.Debt_ID;//GenerateID.Debt_ID(string.Empty);

                de.Debt_Amount = listReq.Sum(f => f.Grand_Total_Amount);//requistition.Grand_Total_Amount;
                de.Balance_Outstanding_Amount = listReq.Sum(f => f.Grand_Total_Amount); //+ de.Debt_Amount;
                de.CV_Code = user_class.CV_CODE;
                de.Debt_Date = DateTime.Now;
                de.SP_ID = ddlUserSP.SelectedValue;

                de.Requisition_No = txtRequisition_No.Text;

                dbo_DebtDataClass.Update(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
            }
            else
            {
                dbo_DebtClass de = new dbo_DebtClass();
                de.Debt_ID = GenerateID.Debt_ID(string.Empty);

                de.Debt_Amount = requistition.Grand_Total_Amount;
                de.Balance_Outstanding_Amount = de.Debt_Amount;
                de.CV_Code = user_class.CV_CODE;
                de.Debt_Date = DateTime.Now;
                de.SP_ID = ddlUserSP.SelectedValue;

                de.Requisition_No = txtRequisition_No.Text;

                dbo_DebtDataClass.Add(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
            }

            List<dbo_CommissionClass> com = dbo_CommissionDataClass.Select_Record(txtRequisition_No.Text);

            logger.Debug("com.Count " + com.Count);
            if (com.Count > 0)
            {
                com[0].Requisition_No = txtRequisition_No.Text;
                com[0].Commission += requistition.Total_Commission;
                com[0].Commission_Balance_Outstanding += requistition.Total_Commission;
                com[0].Point += requistition.Tota_Point;
                com[0].Commission_Requisition_Status = 1;//
                dbo_CommissionDataClass.Update(com[0], HttpContext.Current.Request.Cookies["User_ID"].Value);
            }
            else
            {
                dbo_CommissionClass comm = new dbo_CommissionClass();
                comm.Requisition_No = txtRequisition_No.Text;
                comm.Commission = requistition.Total_Commission;
                comm.Point = requistition.Tota_Point;
                comm.Commission_Requisition_Status = 1;//
                dbo_CommissionDataClass.Add(comm, HttpContext.Current.Request.Cookies["User_ID"].Value);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void UpdateRecord()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(HttpContext.Current.Request.Cookies["User_ID"].Value);

            dbo_RequisitionClass requistition = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text, txtTime_No.Text);

            requistition.Requisition_No = txtRequisition_No.Text;
            requistition.Requisition_Date = DateTime.Parse(txtRequisition_Date.Text);
            requistition.User_ID = ddlUserSP.SelectedValue;
            requistition.Time_No = txtTime_No.Text;
            requistition.Transaction_Date = DateTime.Now;
            requistition.Replace_Sales = ddlUserRepresent.SelectedItem.Value;
            requistition.Status = "1";

            requistition.Grand_Total_Amount = Decimal.Parse(txtGrand_Total_Amount.Text.Replace(",", string.Empty));
            logger.Debug("requistition.Grand_Total_Amount " + requistition.Grand_Total_Amount);
            requistition.Grand_Total_Qty = int.Parse(txtGrand_Total_Qty.Text.Replace(",", string.Empty));
            logger.Debug("requistition.Grand_Total_Qty " + requistition.Grand_Total_Qty);

            List<dbo_RequisitionDetailClass> detailGet = dbo_RequisitionDetailDataClass.SearchByTimeNo(requistition.Requisition_No, requistition.Time_No);

            if (detailGet.Count > 0)
            {
                foreach (dbo_RequisitionDetailClass _detail in detailGet)
                {
                    String Product_ID = _detail.Product_ID;

                    List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);
                    if (prev_stock.Count > 0)
                    {
                        dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                        //Old
                        //stock.Stock_Out = short.Parse((stock.Stock_Out - (_detail.Previous_Balance_Qty + _detail.Requisition_Qty)).ToString());
                        //stock.Stock_End = short.Parse((stock.Stock_End + (_detail.Previous_Balance_Qty + _detail.Requisition_Qty)).ToString());

                        stock.Stock_Out = short.Parse((stock.Stock_Out - ( _detail.Requisition_Qty)).ToString());
                        stock.Stock_End = short.Parse((stock.Stock_End + ( _detail.Requisition_Qty)).ToString());

                        dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                    }
                }

                //List<dbo_DebtClass> debt = dbo_DebtDataClass.Search(requistition.User_ID, String.Empty);
                //dbo_DebtClass maxDebt = debt.OrderByDescending(f => f.Debt_ID).First();
                //logger.Debug("maxDebt.Debt_ID " + maxDebt.Debt_ID);

                //dbo_DebtDataClass.Delete(maxDebt.Debt_ID);

                if (requistition.Time_No == "1")
                {
                    List<dbo_CommissionClass> comm = dbo_CommissionDataClass.Select_Record(requistition.Requisition_No);
                    if (comm.Count > 0)
                    {
                        dbo_CommissionClass maxComm = comm.OrderByDescending(f => f.Created_Date).First();
                        dbo_CommissionDataClass.Delete(maxComm);
                    }
                }
                else
                {
                    List<dbo_CommissionClass> com = dbo_CommissionDataClass.Select_Record(requistition.Requisition_No);

                    logger.Debug("com.Count " + com.Count);
                    if (com.Count > 0)
                    {
                        com[0].Requisition_No = txtRequisition_No.Text;
                        com[0].Commission = com[0].Commission - requistition.Total_Commission;
                        com[0].Commission_Balance_Outstanding = com[0].Commission_Balance_Outstanding - requistition.Total_Commission;
                        //
                        int cPoint = (int)com[0].Point;
                        int rPoint = (int)requistition.Tota_Point;
                        cPoint = cPoint - rPoint;
                        com[0].Point = (short?)cPoint;//(short)com[0].Point - (short)req.Tota_Point;
                        // 
                        com[0].Commission_Requisition_Status = 1;//
                        dbo_CommissionDataClass.Update(com[0], HttpContext.Current.Request.Cookies["User_ID"].Value);
                    }
                }

                //List<dbo_CommissionClass> commDel = dbo_CommissionDataClass.Select_Record(requistition.Requisition_No);
                //dbo_CommissionClass maxComm = commDel.OrderByDescending(f => f.Created_Date).First();
                //dbo_CommissionDataClass.Delete(maxComm);

                dbo_RequisitionDetailDataClass.DeletebyTimeNo(requistition.Requisition_No, requistition.Time_No);

            }

            //Insert
            List<dbo_RequisitionDetailClass> listofdetail = getRequisitionDetail(requistition.Requisition_No);

            logger.Debug("listofdetail.Count " + listofdetail.Count);

            if (listofdetail.Count > 0)
            {
                dbo_StockMovementDataClass.DeleteByRefNo(requistition.Requisition_No + '_' + requistition.Time_No);
            }

            foreach (dbo_RequisitionDetailClass detail in listofdetail)
            {
                dbo_RequisitionDetailDataClass.Add(detail, HttpContext.Current.Request.Cookies["User_ID"].Value);

                String Product_ID = detail.Product_ID;

                List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);

                logger.Debug("prev_stock.Count " + prev_stock.Count);

                if (prev_stock.Count > 0)
                {
                    dbo_StockClass stock = prev_stock[prev_stock.Count - 1];
                    logger.Debug("stock.Stock_on_Hand_ID " + stock.Stock_on_Hand_ID + " stock.Product_ID " + Product_ID);

                    //Old
                    //stock.Stock_Out = short.Parse((stock.Stock_Out + (detail.Previous_Balance_Qty + detail.Requisition_Qty)).ToString());
                    //stock.Stock_End = short.Parse((stock.Stock_End - (detail.Previous_Balance_Qty + detail.Requisition_Qty)).ToString());
                    //New 2018/05/25 By Keng
                    stock.Stock_Out = short.Parse((stock.Stock_Out + (detail.Requisition_Qty)).ToString());
                    stock.Stock_End = short.Parse((stock.Stock_End - (detail.Requisition_Qty)).ToString());
                    stock.Product_ID = Product_ID;

                    dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                }

                dbo_StockMovementClass movement = new dbo_StockMovementClass();
                movement.CV_CODE = user_class.CV_CODE;
                movement.Date = DateTime.Now;
                movement.Movement_Type = "เบิกSP";
                movement.Product_List_ID = Product_ID;
                //Old
                //movement.Qty = short.Parse((detail.Previous_Balance_Qty + detail.Requisition_Qty).ToString());
                //New 2018/05/25 By Keng
                movement.Qty = short.Parse((detail.Requisition_Qty).ToString());

                movement.Ref_No = requistition.Requisition_No + '_' + requistition.Time_No;

                dbo_StockMovementDataClass.Add(movement);
                logger.Debug("movement.Product_List_ID  " + movement.Product_List_ID);
            }

            requistition.Total_Commission = listofdetail.Sum(f => f.Commission);
            requistition.Tota_Point = Int16.Parse(listofdetail.Sum(f => f.Point).ToString());

            dbo_RequisitionClass debt_requis = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text);

            dbo_RequisitionDataClass.Update(requistition);
            //dbo_RequisitionDataClass.Add(requistition, HttpContext.Current.Request.Cookies["User_ID"].Value);

            dbo_DebtClass debt = dbo_DebtDataClass.SelectByRequisitionNo(txtRequisition_No.Text);
            if (debt != null)
            {
                List<dbo_RequisitionClass> listReq = dbo_RequisitionDataClass.SelectByRequisitionNo(txtRequisition_No.Text);

                dbo_DebtClass de = new dbo_DebtClass();
                de.Debt_ID = debt.Debt_ID;//GenerateID.Debt_ID(string.Empty);

                de.Debt_Amount = listReq.Sum(f => f.Grand_Total_Amount);//requistition.Grand_Total_Amount;
                de.Balance_Outstanding_Amount = listReq.Sum(f => f.Grand_Total_Amount); //+ de.Debt_Amount;
                de.CV_Code = user_class.CV_CODE;
                de.Debt_Date = DateTime.Now;
                de.SP_ID = ddlUserSP.SelectedValue;

                de.Requisition_No = txtRequisition_No.Text;

                dbo_DebtDataClass.Update(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
            }
            else
            {
                dbo_DebtClass de = new dbo_DebtClass();
                de.Debt_ID = GenerateID.Debt_ID(string.Empty);

                de.Debt_Amount = requistition.Grand_Total_Amount;
                de.Balance_Outstanding_Amount = de.Debt_Amount;
                de.CV_Code = user_class.CV_CODE;
                de.Debt_Date = DateTime.Now;
                de.SP_ID = ddlUserSP.SelectedValue;

                de.Requisition_No = txtRequisition_No.Text;

                dbo_DebtDataClass.Add(de, HttpContext.Current.Request.Cookies["User_ID"].Value);
            }

            //dbo_DebtClass de = new dbo_DebtClass();
            //de.Debt_ID = GenerateID.Debt_ID(string.Empty);

            //de.Debt_Amount = requistition.Grand_Total_Amount;
            //de.Balance_Outstanding_Amount = de.Debt_Amount;
            //de.CV_Code = user_class.CV_CODE;
            //de.Debt_Date = DateTime.Now;
            //de.SP_ID = ddlUserSP.SelectedValue;

            //de.Requisition_No = txtRequisition_No.Text;

            //dbo_DebtDataClass.Add(de, HttpContext.Current.Request.Cookies["User_ID"].Value);

            List<dbo_CommissionClass> comIn = dbo_CommissionDataClass.Select_Record(txtRequisition_No.Text);

            logger.Debug("com.Count " + comIn.Count);
            if (comIn.Count > 0)
            {
                comIn[0].Requisition_No = txtRequisition_No.Text;
                comIn[0].Commission = comIn[0].Commission + requistition.Total_Commission;
                comIn[0].Commission_Balance_Outstanding = comIn[0].Commission_Balance_Outstanding + requistition.Total_Commission;
                comIn[0].Point += requistition.Tota_Point;
                comIn[0].Commission_Requisition_Status = 1;//
                dbo_CommissionDataClass.Update(comIn[0], HttpContext.Current.Request.Cookies["User_ID"].Value);
            }
            else
            {
                dbo_CommissionClass comm = new dbo_CommissionClass();
                comm.Requisition_No = txtRequisition_No.Text;
                comm.Commission = requistition.Total_Commission;
                comm.Commission_Balance_Outstanding = requistition.Total_Commission;
                comm.Point = requistition.Tota_Point;
                comm.Commission_Requisition_Status = 1;//
                dbo_CommissionDataClass.Add(comm, HttpContext.Current.Request.Cookies["User_ID"].Value);
            }
            // End Insert

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        #region Comment
        //logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        //try
        //{

        //    dbo_UserClass user_class = dbo_UserDataClass.Select_Record(HttpContext.Current.Request.Cookies["User_ID"].Value);

        //    dbo_RequisitionClass requistition = dbo_RequisitionDataClass.Select_Record(txtRequisition_No.Text, txtTime_No.Text);
        //    requistition.Requisition_No = txtRequisition_No.Text;

        //    requistition.Requisition_Date = DateTime.Parse(txtRequisition_Date.Text);

        //    requistition.User_ID = ddlUserSP.SelectedValue;
        //    requistition.Time_No = txtTime_No.Text;

        //    requistition.Transaction_Date = DateTime.Now;

        //    requistition.Replace_Sales = ddlUserRepresent.SelectedItem.Value;

        //    requistition.Status = "1";


        //    requistition.Grand_Total_Amount = Decimal.Parse(txtGrand_Total_Amount.Text.Replace(",", string.Empty));
        //    logger.Debug("requistition.Grand_Total_Amount " + requistition.Grand_Total_Amount);
        //    requistition.Grand_Total_Qty = int.Parse(txtGrand_Total_Qty.Text.Replace(",", string.Empty));
        //    logger.Debug("requistition.Grand_Total_Qty " + requistition.Grand_Total_Qty);

        //    List<dbo_RequisitionDetailClass> listofdetail = getRequisitionDetail(requistition.Requisition_No);

        //    requistition.Total_Commission = listofdetail.Sum(f => f.Commission);
        //    requistition.Tota_Point = Int16.Parse(listofdetail.Sum(f => f.Point).ToString());

        //    dbo_RequisitionDataClass.Update(requistition);

        //    foreach (dbo_RequisitionDetailClass detail in listofdetail)
        //    {
        //        dbo_RequisitionDetailDataClass.Update(detail);

        //        String Product_ID = detail.Product_ID;

        //        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);

        //        logger.Debug("prev_stock.Count " + prev_stock.Count);


        //        if (prev_stock.Count > 0)
        //        {

        //            dbo_StockClass stock = prev_stock[prev_stock.Count - 1];
        //            logger.Debug("stock.Stock_on_Hand_ID " + stock.Stock_on_Hand_ID + " stock.Product_ID " + Product_ID);

        //            stock.Stock_In += detail.Old_Total_Qty;
        //            stock.Stock_End += detail.Old_Total_Qty;

        //            dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);


        //            dbo_StockClass stock_update = dbo_StockDataClass.Select_Record(stock.Stock_on_Hand_ID);

        //            stock_update.Stock_Out = short.Parse((stock.Stock_Out + detail.Requisition_Qty).ToString());
        //            stock_update.Stock_End = short.Parse((stock.Stock_End - detail.Requisition_Qty).ToString());

        //            dbo_StockDataClass.Update(stock_update, HttpContext.Current.Request.Cookies["User_ID"].Value);
        //        }
        //        else
        //        {

        //        }

        //        dbo_StockMovementClass movement = new dbo_StockMovementClass();
        //        movement.CV_CODE = user_class.CV_CODE;
        //        movement.Date = DateTime.Now;
        //        movement.Movement_Type = "เบิกSP";
        //        movement.Product_List_ID = Product_ID;
        //        movement.Qty = byte.Parse(detail.Requisition_Qty.ToString());

        //        dbo_StockMovementDataClass.Add(movement);
        //        logger.Debug("movement.Product_List_ID  " + movement.Product_List_ID);
        //    }

        //    List<dbo_DebtClass> debt_list = dbo_DebtDataClass.Search(requistition.User_ID, "").OrderBy(f => f.Created_Date).ToList();

        //    logger.Debug("debt_list[debt_list.Count - 1].Debt_ID " + debt_list[debt_list.Count - 1].Debt_ID);
        //    dbo_DebtDataClass.Delete(debt_list[debt_list.Count - 1].Debt_ID);

        //    //string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        //    //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

        //    dbo_DebtClass de = new dbo_DebtClass();
        //    de.Debt_ID = GenerateID.Debt_ID(string.Empty);
        //    logger.Debug("de.Debt_ID " + de.Debt_ID);
        //    de.Debt_Amount = requistition.Grand_Total_Amount;
        //    de.Balance_Outstanding_Amount = de.Debt_Amount;
        //    de.CV_Code = user_class.CV_CODE;
        //    de.Debt_Date = DateTime.Now;
        //    de.SP_ID = ddlUserSP.SelectedValue;
        //    de.Requisition_No = txtRequisition_No.Text;

        //    dbo_DebtDataClass.Add(de, HttpContext.Current.Request.Cookies["User_ID"].Value);


        //    List<dbo_CommissionClass> com = dbo_CommissionDataClass.Select_Record(requistition.Requisition_No);
        //    if (com.Count > 0)
        //    {
        //        com[0].Requisition_No = txtRequisition_No.Text;
        //        com[0].Commission = requistition.Total_Commission;

        //        com[0].Point = requistition.Tota_Point;
        //        com[0].Commission_Requisition_Status = 1;//
        //        dbo_CommissionDataClass.Update(com[0], HttpContext.Current.Request.Cookies["User_ID"].Value);
        //    }

        //}
        //catch (Exception ex)
        //{
        //    logger.Error(ex.Message);
        //}
        #endregion
    }

    #endregion

    #region GridView Data Bound
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewSearchRequisition.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewSearchRequisition.PageIndex = pageList.SelectedIndex;
         btnSearch_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewSearchRequisition_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewSearchRequisition.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewSearchRequisition.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewSearchRequisition.PageIndex)
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
            int currentPage = GridViewSearchRequisition.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewSearchRequisition.PageCount.ToString();

        }
    }

    protected void GridViewRequisition_1_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_1"];
        Session.Remove("GetProduct_Quantity_tab_1");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewRequisition_1.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");

                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 10;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;
                row.Cells[9].Visible = false;
                // row.Cells[10].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                Label _lbl_Price = (Label)row.FindControl("lbl_PricePerUnit");
                TextBox _lbl_Total_Qty = (TextBox)row.FindControl("lbl_Total_Qty");
                Label _lbl_Sub_Total_Qty = (Label)row.FindControl("lbl_Sub_Total_Qty");
                Label _lbl_Previous_Balance_Qty = (Label)row.FindControl("lbl_Previous_Balance_Qty");
                HiddenField _hdf_End_Stock1 = (HiddenField)row.FindControl("hdf_End_Stock1");
                if (hdfStatus.Value == "" || hdfStatus.Value == "ยังไม่เคลียร์เงิน")
                {
                    _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Deposit_Qty.ToString();

                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty + listProduct_Quantity[i].Deposit_Qty).ToString();
                }

                if (btnSaveMode.Value == "แก้ไข")
                {
                    _Amount.Text = listProduct_Quantity[i].Requisition_Qty.ToString();
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Sub_Total_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Requisition_Qty + listProduct_Quantity[i].Sub_Total_Qty + listProduct_Quantity[i].Previous_Balance_Qty).ToString();

                    if (txtTime_No.Text == "1")
                    {
                        _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Previous_Balance_Qty.ToString();
                    }
                }
                else
                {
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Total_Qty.ToString();
                    //_lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty).ToString();

                    Int32 ReqQty = 0;

                    if (_Amount.Text != "")
                    {
                        ReqQty = Convert.ToInt32(_Amount.Text);
                    }

                    txtGrand_Total_Qty.Text = (Convert.ToInt32(txtGrand_Total_Qty.Text)
                        + (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)).ToString();

                    if (_lbl_Price.Text != "")
                    {
                        txtGrand_Total_Amount.Text = (Convert.ToDecimal(txtGrand_Total_Amount.Text)
                            + ((Convert.ToDecimal(_lbl_Price.Text) * (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)))).ToString();
                    }
                }

                if (_lbl_Total_Qty.Text != "")
                {
                    int TotalQty = int.Parse(_lbl_Total_Qty.Text);
                    _lbl_Total_Qty.Text = TotalQty.ToString("#,##0");
                }

                if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                {
                    _Amount.Enabled = false;

                }
                else
                {
                    _Amount.Enabled = true;
                }

               
            }
        }
    }

    protected void GridViewRequisition_2_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_2"];
        Session.Remove("GetProduct_Quantity_tab_2");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewRequisition_2.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");

                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 10;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;
                row.Cells[9].Visible = false;
                // row.Cells[10].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                Label _lbl_Price = (Label)row.FindControl("lbl_PricePerUnit");
                TextBox _lbl_Total_Qty = (TextBox)row.FindControl("lbl_Total_Qty");
                Label _lbl_Sub_Total_Qty = (Label)row.FindControl("lbl_Sub_Total_Qty");
                Label _lbl_Previous_Balance_Qty = (Label)row.FindControl("lbl_Previous_Balance_Qty");

                if (hdfStatus.Value == "" || hdfStatus.Value == "ยังไม่เคลียร์เงิน")
                {
                    _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Deposit_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty + listProduct_Quantity[i].Deposit_Qty).ToString();
                }

                if (btnSaveMode.Value == "แก้ไข")
                {
                    _Amount.Text = listProduct_Quantity[i].Requisition_Qty.ToString();
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Sub_Total_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Requisition_Qty + listProduct_Quantity[i].Sub_Total_Qty + listProduct_Quantity[i].Previous_Balance_Qty).ToString();

                    if (txtTime_No.Text == "1")
                    {
                        _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Previous_Balance_Qty.ToString();
                    }
                }
                else
                {
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Total_Qty.ToString();
                    //_lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty).ToString();

                    Int32 ReqQty = 0;

                    if (_Amount.Text != "")
                    {
                        ReqQty = Convert.ToInt32(_Amount.Text);
                    }

                    txtGrand_Total_Qty.Text = (Convert.ToInt32(txtGrand_Total_Qty.Text)
                        + (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)).ToString();

                    if (_lbl_Price.Text != "")
                    {
                        txtGrand_Total_Amount.Text = (Convert.ToDecimal(txtGrand_Total_Amount.Text)
                            + ((Convert.ToDecimal(_lbl_Price.Text) * (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)))).ToString();
                    }
                }

                if (_lbl_Total_Qty.Text != "")
                {
                    int TotalQty = int.Parse(_lbl_Total_Qty.Text);
                    _lbl_Total_Qty.Text = TotalQty.ToString("#,##0");
                }

                if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                {
                    _Amount.Enabled = false;

                }
                else
                {
                    _Amount.Enabled = true;
                }
            }
        }
    }

    protected void GridViewRequisition_3_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_3"];
        Session.Remove("GetProduct_Quantity_tab_3");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewRequisition_3.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");

                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 10;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;
                row.Cells[9].Visible = false;
                // row.Cells[10].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                Label _lbl_Price = (Label)row.FindControl("lbl_PricePerUnit");
                TextBox _lbl_Total_Qty = (TextBox)row.FindControl("lbl_Total_Qty");
                Label _lbl_Sub_Total_Qty = (Label)row.FindControl("lbl_Sub_Total_Qty");
                Label _lbl_Previous_Balance_Qty = (Label)row.FindControl("lbl_Previous_Balance_Qty");

                if (hdfStatus.Value == "" || hdfStatus.Value == "ยังไม่เคลียร์เงิน")
                {
                    _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Deposit_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty + listProduct_Quantity[i].Deposit_Qty).ToString();
                }

                if (btnSaveMode.Value == "แก้ไข")
                {
                    _Amount.Text = listProduct_Quantity[i].Requisition_Qty.ToString();
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Sub_Total_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Requisition_Qty + listProduct_Quantity[i].Sub_Total_Qty + listProduct_Quantity[i].Previous_Balance_Qty).ToString();

                    if (txtTime_No.Text == "1")
                    {
                        _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Previous_Balance_Qty.ToString();
                    }
                }
                else
                {
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Total_Qty.ToString();
                    //_lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty).ToString();

                    Int32 ReqQty = 0;

                    if (_Amount.Text != "")
                    {
                        ReqQty = Convert.ToInt32(_Amount.Text);
                    }

                    txtGrand_Total_Qty.Text = (Convert.ToInt32(txtGrand_Total_Qty.Text)
                        + (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)).ToString();

                    if (_lbl_Price.Text != "")
                    {
                        txtGrand_Total_Amount.Text = (Convert.ToDecimal(txtGrand_Total_Amount.Text)
                            + ((Convert.ToDecimal(_lbl_Price.Text) * (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)))).ToString();
                    }
                }

                if (_lbl_Total_Qty.Text != "")
                {
                    int TotalQty = int.Parse(_lbl_Total_Qty.Text);
                    _lbl_Total_Qty.Text = TotalQty.ToString("#,##0");
                }

                if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                {
                    _Amount.Enabled = false;

                }
                else
                {
                    _Amount.Enabled = true;
                }
            }
        }
    }

    protected void GridViewRequisition_4_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_4"];
        Session.Remove("GetProduct_Quantity_tab_4");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewRequisition_4.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");

                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 10;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;
                row.Cells[9].Visible = false;
                // row.Cells[10].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                Label _lbl_Price = (Label)row.FindControl("lbl_PricePerUnit");
                TextBox _lbl_Total_Qty = (TextBox)row.FindControl("lbl_Total_Qty");
                Label _lbl_Sub_Total_Qty = (Label)row.FindControl("lbl_Sub_Total_Qty");
                Label _lbl_Previous_Balance_Qty = (Label)row.FindControl("lbl_Previous_Balance_Qty");

                if (hdfStatus.Value == "" || hdfStatus.Value == "ยังไม่เคลียร์เงิน")
                {
                    _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Deposit_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty + listProduct_Quantity[i].Deposit_Qty).ToString();
                }

                if (btnSaveMode.Value == "แก้ไข")
                {
                    _Amount.Text = listProduct_Quantity[i].Requisition_Qty.ToString();
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Sub_Total_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Requisition_Qty + listProduct_Quantity[i].Sub_Total_Qty + listProduct_Quantity[i].Previous_Balance_Qty).ToString();

                    if (txtTime_No.Text == "1")
                    {
                        _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Previous_Balance_Qty.ToString();
                    }
                }
                else
                {
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Total_Qty.ToString();
                    //_lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty).ToString();

                    Int32 ReqQty = 0;

                    if (_Amount.Text != "")
                    {
                        ReqQty = Convert.ToInt32(_Amount.Text);
                    }

                    txtGrand_Total_Qty.Text = (Convert.ToInt32(txtGrand_Total_Qty.Text)
                        + (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)).ToString();

                    if (_lbl_Price.Text != "")
                    {
                        txtGrand_Total_Amount.Text = (Convert.ToDecimal(txtGrand_Total_Amount.Text)
                            + ((Convert.ToDecimal(_lbl_Price.Text) * (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)))).ToString();
                    }
                }

                if (_lbl_Total_Qty.Text != "")
                {
                    int TotalQty = int.Parse(_lbl_Total_Qty.Text);
                    _lbl_Total_Qty.Text = TotalQty.ToString("#,##0");
                }

                if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                {
                    _Amount.Enabled = false;

                }
                else
                {
                    _Amount.Enabled = true;
                }
            }
        }
    }

    protected void GridViewRequisition_5_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_5"];
        Session.Remove("GetProduct_Quantity_tab_5");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewRequisition_5.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");

                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 10;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;
                row.Cells[9].Visible = false;
                // row.Cells[10].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                Label _lbl_Price = (Label)row.FindControl("lbl_PricePerUnit");
                TextBox _lbl_Total_Qty = (TextBox)row.FindControl("lbl_Total_Qty");
                Label _lbl_Sub_Total_Qty = (Label)row.FindControl("lbl_Sub_Total_Qty");
                Label _lbl_Previous_Balance_Qty = (Label)row.FindControl("lbl_Previous_Balance_Qty");

                if (hdfStatus.Value == "" || hdfStatus.Value == "ยังไม่เคลียร์เงิน")
                {
                    _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Deposit_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty + listProduct_Quantity[i].Deposit_Qty).ToString();
                }

                if (btnSaveMode.Value == "แก้ไข")
                {
                    _Amount.Text = listProduct_Quantity[i].Requisition_Qty.ToString();
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Sub_Total_Qty.ToString();
                    _lbl_Total_Qty.Text = (listProduct_Quantity[i].Requisition_Qty + listProduct_Quantity[i].Sub_Total_Qty + listProduct_Quantity[i].Previous_Balance_Qty).ToString();

                    if (txtTime_No.Text == "1")
                    {
                        _lbl_Previous_Balance_Qty.Text = listProduct_Quantity[i].Previous_Balance_Qty.ToString();
                    }
                }
                else
                {
                    _lbl_Sub_Total_Qty.Text = listProduct_Quantity[i].Total_Qty.ToString();
                    //_lbl_Total_Qty.Text = (listProduct_Quantity[i].Total_Qty).ToString();

                    Int32 ReqQty = 0;

                    if (_Amount.Text != "")
                    {
                        ReqQty = Convert.ToInt32(_Amount.Text);
                    }

                    txtGrand_Total_Qty.Text = (Convert.ToInt32(txtGrand_Total_Qty.Text)
                        + (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)).ToString();

                    if (_lbl_Price.Text != "")
                    {
                        txtGrand_Total_Amount.Text = (Convert.ToDecimal(txtGrand_Total_Amount.Text)
                            + ((Convert.ToDecimal(_lbl_Price.Text) * (Convert.ToInt32(_lbl_Previous_Balance_Qty.Text) + ReqQty)))).ToString();
                    }
                }

                if (_lbl_Total_Qty.Text != "")
                {
                    int TotalQty = int.Parse(_lbl_Total_Qty.Text);
                    _lbl_Total_Qty.Text = TotalQty.ToString("#,##0");
                }

                if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                {
                    _Amount.Enabled = false;

                }
                else
                {
                    _Amount.Enabled = true;
                }
            }
        }
    }
    #endregion

    #region Methods
    public void Show(string message)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            logger.Info(message);

            string cleanMessage = message.Replace("'", "\'");
            // Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            //if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            //{
            //  page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception)
        {

        }
        //}
    }

    private List<dbo_RequisitionDetailClass> getRequisitionDetail(string Requisition_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_RequisitionDetailClass> listofdetail = new List<dbo_RequisitionDetailClass>();
        int index = 1;

        try
        {
            foreach (GridViewRow currentRow in GridViewRequisition_1.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");

                // logger.Debug(txt.Text + " : " + lbl_Total_Qty_.Text);

                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    lbl_Total_Qty_.Text = lbl_Total_Qty_.Text.Replace(",", "");

                    if (short.Parse(lbl_Total_Qty_.Text) > 0)
                    {
                        Label _lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        logger.Debug(_lbl_Product_ID.Text + " " + lbl_Total_Qty_.Text);
                        Label _lbl_Product_Name = (Label)currentRow.FindControl("lbl_Product_Name");

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");

                        Decimal SP_Price = Decimal.Parse(lbl_PricePerUnit_.Text);
                        Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");

                        Label lbl_Previous_Balance_Qty_ = (Label)currentRow.FindControl("lbl_Previous_Balance_Qty");
                        Label lbl_Suggestion_Qty_ = (Label)currentRow.FindControl("lbl_Suggestion_Qty");

                        Label Label_CP_Meiji_Price_ = (Label)currentRow.FindControl("Label_CP_Meiji_Price");
                        Label Label_Point_ = (Label)currentRow.FindControl("Label_Point");

                        //Label Label_Stock_End_ = (Label)currentRow.FindControl("Label_Point");

                        dbo_RequisitionDetailClass detail = new dbo_RequisitionDetailClass();
                        String timeno = txtTime_No.Text.PadLeft(2, '0');

                        /*
                        Label Label_Requisition_Detail_ID = (Label)currentRow.FindControl("Label_Requisition_Detail_ID");
                        logger.Debug("Label_Requisition_Detail_ID.Text " + Label_Requisition_Detail_ID.Text);

                        if (string.IsNullOrEmpty(Label_Requisition_Detail_ID.Text))
                        {
                            detail.Requisition_Detail_ID = Requisition_No + timeno + index.ToString("0000");
                        }
                        else
                        {
                            detail.Requisition_Detail_ID = Label_Requisition_Detail_ID.Text;
                        }*/
                        detail.Requisition_Detail_ID = Requisition_No + timeno + index.ToString("0000");

                        index++;

                        detail.Requisition_No = Requisition_No;
                        detail.Product_ID = _lbl_Product_ID.Text;
                        detail.Product_Name = _lbl_Product_Name.Text;
                        detail.Time_No = txtTime_No.Text;

                        detail.Requisition_Qty = short.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text);

                        detail.Total_Qty = short.Parse(lbl_Total_Qty_.Text);
                        detail.Previous_Balance_Qty = short.Parse(lbl_Previous_Balance_Qty_.Text);
                        detail.Suggestion_Qty = short.Parse(lbl_Suggestion_Qty_.Text);

                        detail.Total_Price = (detail.Previous_Balance_Qty + detail.Requisition_Qty) * SP_Price;
                        detail.Price = SP_Price;
                        detail.Sub_Total_Qty = (string.IsNullOrEmpty(lbl_Sub_Total_Qty_.Text) ? short.Parse("0") : short.Parse(lbl_Sub_Total_Qty_.Text));
                        //detail.Commission = (decimal.Parse(Label_CP_Meiji_Price_.Text) - decimal.Parse(lbl_PricePerUnit_.Text)) * (detail.Previous_Balance_Qty + detail.Total_Qty);
                        detail.Commission = (decimal.Parse(Label_CP_Meiji_Price_.Text) - decimal.Parse(lbl_PricePerUnit_.Text)) * (detail.Previous_Balance_Qty + detail.Requisition_Qty);

                        Label Label_Vat_ = (Label)currentRow.FindControl("Label_Vat");
                        detail.Vat = byte.Parse(Label_Vat_.Text);

                        //int? point = int.Parse(Label_Point_.Text) * (detail.Previous_Balance_Qty + detail.Total_Qty);
                        int? point = int.Parse(Label_Point_.Text) * (detail.Previous_Balance_Qty + detail.Requisition_Qty);

                        detail.Point = short.Parse(point.ToString());
                        detail.Selling_Price = decimal.Parse(Label_CP_Meiji_Price_.Text);

                        Label lbl_Old_Total_Qty = (Label)currentRow.FindControl("lbl_Old_Total_Qty");
                        detail.Old_Total_Qty = short.Parse(lbl_Old_Total_Qty.Text);

                        listofdetail.Add(detail);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            foreach (GridViewRow currentRow in GridViewRequisition_2.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {

                    lbl_Total_Qty_.Text = lbl_Total_Qty_.Text.Replace(",", "");

                    if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text) && short.Parse(lbl_Total_Qty_.Text) > 0)
                    {
                        Label _lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        logger.Debug(_lbl_Product_ID.Text + " " + lbl_Total_Qty_.Text);
                        Label _lbl_Product_Name = (Label)currentRow.FindControl("lbl_Product_Name");

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");

                        Decimal SP_Price = Decimal.Parse(lbl_PricePerUnit_.Text);
                        Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");

                        Label lbl_Previous_Balance_Qty_ = (Label)currentRow.FindControl("lbl_Previous_Balance_Qty");
                        Label lbl_Suggestion_Qty_ = (Label)currentRow.FindControl("lbl_Suggestion_Qty");

                        Label Label_CP_Meiji_Price_ = (Label)currentRow.FindControl("Label_CP_Meiji_Price");
                        Label Label_Point_ = (Label)currentRow.FindControl("Label_Point");


                        dbo_RequisitionDetailClass detail = new dbo_RequisitionDetailClass();
                        String timeno = txtTime_No.Text.PadLeft(2, '0');



                        Label Label_Requisition_Detail_ID = (Label)currentRow.FindControl("Label_Requisition_Detail_ID");
                        logger.Debug("Label_Requisition_Detail_ID.Text " + Label_Requisition_Detail_ID.Text);

                        if (string.IsNullOrEmpty(Label_Requisition_Detail_ID.Text))
                        {
                            detail.Requisition_Detail_ID = Requisition_No + timeno + index.ToString("0000");
                        }
                        else
                        {
                            detail.Requisition_Detail_ID = Label_Requisition_Detail_ID.Text;
                        }

                        index++;

                        detail.Requisition_No = Requisition_No;
                        detail.Product_ID = _lbl_Product_ID.Text;
                        detail.Product_Name = _lbl_Product_Name.Text;
                        detail.Time_No = txtTime_No.Text;


                        detail.Requisition_Qty = short.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text);
                        detail.Total_Qty = short.Parse(lbl_Total_Qty_.Text);
                        detail.Previous_Balance_Qty = short.Parse(lbl_Previous_Balance_Qty_.Text);
                        detail.Suggestion_Qty = short.Parse(lbl_Suggestion_Qty_.Text);

                        detail.Total_Price = (detail.Previous_Balance_Qty + detail.Requisition_Qty) * SP_Price;
                        detail.Price = SP_Price;
                        detail.Sub_Total_Qty = (string.IsNullOrEmpty(lbl_Sub_Total_Qty_.Text) ? short.Parse("0") : short.Parse(lbl_Sub_Total_Qty_.Text));
                        detail.Commission = (decimal.Parse(Label_CP_Meiji_Price_.Text) - decimal.Parse(lbl_PricePerUnit_.Text)) * (detail.Previous_Balance_Qty + detail.Total_Qty);

                        Label Label_Vat_ = (Label)currentRow.FindControl("Label_Vat");
                        detail.Vat = byte.Parse(Label_Vat_.Text);

                        int? point = int.Parse(Label_Point_.Text) * (detail.Previous_Balance_Qty + detail.Total_Qty);

                        detail.Point = short.Parse(point.ToString());
                        detail.Selling_Price = decimal.Parse(Label_CP_Meiji_Price_.Text);
                        Label lbl_Old_Total_Qty = (Label)currentRow.FindControl("lbl_Old_Total_Qty");
                        detail.Old_Total_Qty = short.Parse(lbl_Old_Total_Qty.Text);

                        listofdetail.Add(detail);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {

            foreach (GridViewRow currentRow in GridViewRequisition_3.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    lbl_Total_Qty_.Text = lbl_Total_Qty_.Text.Replace(",", "");

                    if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text) && short.Parse(lbl_Total_Qty_.Text) > 0)
                    {
                        Label _lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        logger.Debug(_lbl_Product_ID.Text + " " + lbl_Total_Qty_.Text);
                        Label _lbl_Product_Name = (Label)currentRow.FindControl("lbl_Product_Name");

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");

                        Decimal SP_Price = Decimal.Parse(lbl_PricePerUnit_.Text);
                        Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");

                        Label lbl_Previous_Balance_Qty_ = (Label)currentRow.FindControl("lbl_Previous_Balance_Qty");
                        Label lbl_Suggestion_Qty_ = (Label)currentRow.FindControl("lbl_Suggestion_Qty");

                        Label Label_CP_Meiji_Price_ = (Label)currentRow.FindControl("Label_CP_Meiji_Price");
                        Label Label_Point_ = (Label)currentRow.FindControl("Label_Point");


                        dbo_RequisitionDetailClass detail = new dbo_RequisitionDetailClass();
                        String timeno = txtTime_No.Text.PadLeft(2, '0');



                        Label Label_Requisition_Detail_ID = (Label)currentRow.FindControl("Label_Requisition_Detail_ID");
                        logger.Debug("Label_Requisition_Detail_ID.Text " + Label_Requisition_Detail_ID.Text);

                        if (string.IsNullOrEmpty(Label_Requisition_Detail_ID.Text))
                        {
                            detail.Requisition_Detail_ID = Requisition_No + timeno + index.ToString("0000");
                        }
                        else
                        {
                            detail.Requisition_Detail_ID = Label_Requisition_Detail_ID.Text;
                        }

                        index++;

                        detail.Requisition_No = Requisition_No;
                        detail.Product_ID = _lbl_Product_ID.Text;
                        detail.Product_Name = _lbl_Product_Name.Text;
                        detail.Time_No = txtTime_No.Text;


                        detail.Requisition_Qty = short.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text);
                        detail.Total_Qty = short.Parse(lbl_Total_Qty_.Text);
                        detail.Previous_Balance_Qty = short.Parse(lbl_Previous_Balance_Qty_.Text);
                        detail.Suggestion_Qty = short.Parse(lbl_Suggestion_Qty_.Text);

                        detail.Total_Price = (detail.Previous_Balance_Qty + detail.Requisition_Qty) * SP_Price;
                        detail.Price = SP_Price;
                        detail.Sub_Total_Qty = (string.IsNullOrEmpty(lbl_Sub_Total_Qty_.Text) ? short.Parse("0") : short.Parse(lbl_Sub_Total_Qty_.Text));
                        detail.Commission = (decimal.Parse(Label_CP_Meiji_Price_.Text) - decimal.Parse(lbl_PricePerUnit_.Text)) * (detail.Previous_Balance_Qty + detail.Total_Qty);

                        Label Label_Vat_ = (Label)currentRow.FindControl("Label_Vat");
                        detail.Vat = byte.Parse(Label_Vat_.Text);

                        int? point = int.Parse(Label_Point_.Text) * (detail.Previous_Balance_Qty + detail.Total_Qty);

                        detail.Point = short.Parse(point.ToString());
                        detail.Selling_Price = decimal.Parse(Label_CP_Meiji_Price_.Text);

                        Label lbl_Old_Total_Qty = (Label)currentRow.FindControl("lbl_Old_Total_Qty");
                        detail.Old_Total_Qty = short.Parse(lbl_Old_Total_Qty.Text);
                        listofdetail.Add(detail);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            foreach (GridViewRow currentRow in GridViewRequisition_4.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    lbl_Total_Qty_.Text = lbl_Total_Qty_.Text.Replace(",", "");

                    if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text) && short.Parse(lbl_Total_Qty_.Text) > 0)
                    {
                        Label _lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        logger.Debug(_lbl_Product_ID.Text + " " + lbl_Total_Qty_.Text);
                        Label _lbl_Product_Name = (Label)currentRow.FindControl("lbl_Product_Name");

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");

                        Decimal SP_Price = Decimal.Parse(lbl_PricePerUnit_.Text);
                        Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");

                        Label lbl_Previous_Balance_Qty_ = (Label)currentRow.FindControl("lbl_Previous_Balance_Qty");
                        Label lbl_Suggestion_Qty_ = (Label)currentRow.FindControl("lbl_Suggestion_Qty");

                        Label Label_CP_Meiji_Price_ = (Label)currentRow.FindControl("Label_CP_Meiji_Price");
                        Label Label_Point_ = (Label)currentRow.FindControl("Label_Point");


                        dbo_RequisitionDetailClass detail = new dbo_RequisitionDetailClass();
                        String timeno = txtTime_No.Text.PadLeft(2, '0');

                        Label Label_Requisition_Detail_ID = (Label)currentRow.FindControl("Label_Requisition_Detail_ID");
                        logger.Debug("Label_Requisition_Detail_ID.Text " + Label_Requisition_Detail_ID.Text);

                        if (string.IsNullOrEmpty(Label_Requisition_Detail_ID.Text))
                        {
                            detail.Requisition_Detail_ID = Requisition_No + timeno + index.ToString("0000");
                        }
                        else
                        {
                            detail.Requisition_Detail_ID = Label_Requisition_Detail_ID.Text;
                        }

                        index++;

                        detail.Requisition_No = Requisition_No;
                        detail.Product_ID = _lbl_Product_ID.Text;
                        detail.Product_Name = _lbl_Product_Name.Text;
                        detail.Time_No = txtTime_No.Text;


                        detail.Requisition_Qty = short.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text);
                        detail.Total_Qty = short.Parse(lbl_Total_Qty_.Text);
                        detail.Previous_Balance_Qty = short.Parse(lbl_Previous_Balance_Qty_.Text);
                        detail.Suggestion_Qty = short.Parse(lbl_Suggestion_Qty_.Text);

                        detail.Total_Price = (detail.Previous_Balance_Qty + detail.Requisition_Qty) * SP_Price;
                        detail.Price = SP_Price;
                        detail.Sub_Total_Qty = (string.IsNullOrEmpty(lbl_Sub_Total_Qty_.Text) ? short.Parse("0") : short.Parse(lbl_Sub_Total_Qty_.Text));
                        detail.Commission = (decimal.Parse(Label_CP_Meiji_Price_.Text) - decimal.Parse(lbl_PricePerUnit_.Text)) * (detail.Previous_Balance_Qty + detail.Total_Qty);

                        Label Label_Vat_ = (Label)currentRow.FindControl("Label_Vat");
                        detail.Vat = byte.Parse(Label_Vat_.Text);

                        int? point = int.Parse(Label_Point_.Text) * (detail.Previous_Balance_Qty + detail.Total_Qty);

                        detail.Point = short.Parse(point.ToString());
                        detail.Selling_Price = decimal.Parse(Label_CP_Meiji_Price_.Text);

                        Label lbl_Old_Total_Qty = (Label)currentRow.FindControl("lbl_Old_Total_Qty");
                        detail.Old_Total_Qty = short.Parse(lbl_Old_Total_Qty.Text);
                        listofdetail.Add(detail);
                    }
                }
            }


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            foreach (GridViewRow currentRow in GridViewRequisition_5.Rows)
            {
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                TextBox lbl_Total_Qty_ = (TextBox)currentRow.FindControl("lbl_Total_Qty");
                if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text))
                {
                    lbl_Total_Qty_.Text = lbl_Total_Qty_.Text.Replace(",", "");

                    if (!string.IsNullOrEmpty(lbl_Total_Qty_.Text) && short.Parse(lbl_Total_Qty_.Text) > 0)
                    {
                        Label _lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        logger.Debug(_lbl_Product_ID.Text + " " + lbl_Total_Qty_.Text);
                        Label _lbl_Product_Name = (Label)currentRow.FindControl("lbl_Product_Name");

                        Label lbl_PricePerUnit_ = (Label)currentRow.FindControl("lbl_PricePerUnit");

                        Decimal SP_Price = Decimal.Parse(lbl_PricePerUnit_.Text);
                        Label lbl_Sub_Total_Qty_ = (Label)currentRow.FindControl("lbl_Sub_Total_Qty");

                        Label lbl_Previous_Balance_Qty_ = (Label)currentRow.FindControl("lbl_Previous_Balance_Qty");
                        Label lbl_Suggestion_Qty_ = (Label)currentRow.FindControl("lbl_Suggestion_Qty");

                        Label Label_CP_Meiji_Price_ = (Label)currentRow.FindControl("Label_CP_Meiji_Price");
                        Label Label_Point_ = (Label)currentRow.FindControl("Label_Point");


                        dbo_RequisitionDetailClass detail = new dbo_RequisitionDetailClass();
                        String timeno = txtTime_No.Text.PadLeft(2, '0');

                        detail.Requisition_Detail_ID = Requisition_No + index.ToString("00");
                        index++;


                        detail.Requisition_No = Requisition_No;
                        detail.Product_ID = _lbl_Product_ID.Text;
                        detail.Product_Name = _lbl_Product_Name.Text;
                        detail.Time_No = txtTime_No.Text;


                        detail.Requisition_Qty = short.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text);
                        detail.Total_Qty = short.Parse(lbl_Total_Qty_.Text);
                        detail.Previous_Balance_Qty = short.Parse(lbl_Previous_Balance_Qty_.Text);
                        detail.Suggestion_Qty = short.Parse(lbl_Suggestion_Qty_.Text);

                        detail.Total_Price = (detail.Previous_Balance_Qty + detail.Requisition_Qty) * SP_Price;
                        detail.Price = SP_Price;
                        detail.Sub_Total_Qty = (string.IsNullOrEmpty(lbl_Sub_Total_Qty_.Text) ? short.Parse("0") : short.Parse(lbl_Sub_Total_Qty_.Text));
                        detail.Commission = (decimal.Parse(Label_CP_Meiji_Price_.Text) - decimal.Parse(lbl_PricePerUnit_.Text)) * (detail.Previous_Balance_Qty + detail.Total_Qty);

                        Label Label_Vat_ = (Label)currentRow.FindControl("Label_Vat");
                        detail.Vat = byte.Parse(Label_Vat_.Text);

                        int? point = int.Parse(Label_Point_.Text) * (detail.Previous_Balance_Qty + detail.Total_Qty);

                        detail.Point = short.Parse(point.ToString());
                        detail.Selling_Price = decimal.Parse(Label_CP_Meiji_Price_.Text);
                        Label lbl_Old_Total_Qty = (Label)currentRow.FindControl("lbl_Old_Total_Qty");
                        detail.Old_Total_Qty = short.Parse(lbl_Old_Total_Qty.Text);

                        listofdetail.Add(detail);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        return listofdetail;
    }

    private void SearchSubmit()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            DateTime? StartDate = null;
            DateTime? EndDate = null;

            if (!string.IsNullOrEmpty(txtSearchBeginRequisition_Date.Text))
            {
                StartDate = DateTime.Parse(txtSearchBeginRequisition_Date.Text);
            }
            if (!string.IsNullOrEmpty(txtSearchEndTransaction_Date.Text))
            {
                EndDate = DateTime.Parse(txtSearchEndTransaction_Date.Text);
            }

            String SP_Name = (ddl_SPName.SelectedIndex == 0 ? string.Empty : ddl_SPName.SelectedValue);

            List<dbo_RequisitionClass> item = dbo_RequisitionDataClass.Search(StartDate, EndDate, SP_Name, user_class.CV_CODE);
            
            if (item.Count > 0)
            {
                GridViewSearchRequisition.DataSource = item;
                GridViewSearchRequisition.DataBind();
                GridViewSearchRequisition.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewSearchRequisition.Visible = false;
                pnlNoRec.Visible = true;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void GetDetailsDataToForm(string id, string time_no, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        pnlForm.Visible = true;
        pnlGrid.Visible = false;

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

        try
        {

            if (Mode == "View")
            {
                btnSaveMode.Value = "แก้ไข";
                btnSave.Text = "แก้ไข";
                btnCancel.Text = "กลับไปหน้าค้นหา";
            }
            else if (Mode == "Edit")
            {
                btnSave.Text = "บันทึก";
                btnSaveMode.Value = "แก้ไข";
                btnCancel.Text = "ยกเลิก";
            }
            else if (string.IsNullOrEmpty(Mode))
            {
                btnSave.Text = "บันทึก";
                btnSaveMode.Value = "บันทึก";
                btnCancel.Text = "ยกเลิก";
            }

            bool enable = Mode != "View";

            if (btnSaveMode.Value == "แก้ไข")
            {
                ddlUserSP.Enabled = false;
                txtRequisition_Date.Enabled = false;
                ddlUserRepresent.Enabled = false;
            }
            else
            {
                ddlUserSP.Enabled = true;
                txtRequisition_Date.Enabled = true;
                ddlUserRepresent.Enabled = true;
            }
            if (!string.IsNullOrEmpty(id))
            {

                dbo_RequisitionClass requis = dbo_RequisitionDataClass.Select_Record(id, time_no);
                ddlUserSP.ClearSelection();
                ddlUserSP.Items.FindByValue(requis.User_ID).Selected = true;
                txtRequisition_No.Text = requis.Requisition_No;
                txtTime_No.Text = requis.Time_No;
                txtRequisition_Date.Text = requis.Requisition_Date.Value.ToShortDateString();
                txtTransaction_Date.Text = requis.Transaction_Date.Value.ToShortDateString();
                txtGrand_Total_Qty.Text = requis.Grand_Total_Qty.Value.ToString("#,##0");
                txtGrand_Total_Amount.Text = requis.Grand_Total_Amount.Value.ToString("#,##0.#0");

                if (requis.Replace_Sales.Trim() != "")
                {
                    ddlUserRepresent.ClearSelection();
                    ddlUserRepresent.Items.FindByValue(requis.Replace_Sales).Selected = true;
                }

                hdfStatus.Value = requis.Status;

                if (Mode == "View")
                {
                    show_grid_view(requis.User_ID, txtRequisition_No.Text, txtTime_No.Text);
                }
                else
                {
                    DateTime? Requisition_Date = Convert.ToDateTime(txtRequisition_Date.Text);
                    show_grid(requis.User_ID, Requisition_Date);
                }
            }
            else
            {
                //txtRequisition_No.Text = GenerateID.Requisition_No(user_class.CV_CODE);
                txtRequisition_No.Text = string.Empty;
                txtRequisition_Date.Text = DateTime.Now.ToShortDateString();
                txtTransaction_Date.Text = DateTime.Now.ToShortDateString();
                hdfStatus.Value = "";
                txtTime_No.Text = "";
                txtGrand_Total_Amount.Text = "0";
                txtGrand_Total_Qty.Text = "0";

                DateTime? Requisition_Date = DateTime.Now;
                show_grid(string.Empty, Requisition_Date);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void show_grid_view(string User_ID, string Requisition_No, string Time_No)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            DateTime? date = DateTime.Parse(txtRequisition_Date.Text);

            List<dbo_ProductClass> item1 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_No, "นมสดพาสเจอร์ไรส์", Time_No);
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewRequisition_1.DataSource = item1;
            GridViewRequisition_1.DataBind();

            List<dbo_ProductClass> item2 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_No, "นมเปรี้ยว", Time_No);
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewRequisition_2.DataSource = item2;
            GridViewRequisition_2.DataBind();

            List<dbo_ProductClass> item3 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_No, "โยเกิร์ตเมจิ", Time_No);
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewRequisition_3.DataSource = item3;
            GridViewRequisition_3.DataBind();

            List<dbo_ProductClass> item4 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_No, "นมเปรี้ยวไพเกน", Time_No);
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewRequisition_4.DataSource = item4;
            GridViewRequisition_4.DataBind();

            List<dbo_ProductClass> item5 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, Requisition_No, "อื่นๆ", Time_No);
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewRequisition_5.DataSource = item5;
            GridViewRequisition_5.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void show_grid(string User_ID, DateTime? pricedate)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_ProductClass> item1 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, pricedate, "นมสดพาสเจอร์ไรส์");
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewRequisition_1.DataSource = item1;
            GridViewRequisition_1.DataBind();

            List<dbo_ProductClass> item2 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, pricedate, "นมเปรี้ยว");
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewRequisition_2.DataSource = item2;
            GridViewRequisition_2.DataBind();

            List<dbo_ProductClass> item3 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, pricedate, "โยเกิร์ตเมจิ");
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewRequisition_3.DataSource = item3;
            GridViewRequisition_3.DataBind();

            List<dbo_ProductClass> item4 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, pricedate, "นมเปรี้ยวไพเกน");
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewRequisition_4.DataSource = item4;
            GridViewRequisition_4.DataBind();

            List<dbo_ProductClass> item5 = dbo_RequisitionDataClass.GetRequisitionByProductGroupID(User_ID, pricedate, "อื่นๆ");
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewRequisition_5.DataSource = item5;
            GridViewRequisition_5.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);
            ViewState["CV_Code"] = clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code;


            List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
           , string.Empty, string.Empty, string.Empty, clsdbo_Agent == null ? string.Empty : clsdbo_Agent.CV_Code, null, string.Empty, string.Empty).Where(f => f.Status == "Active" && (f.Position == "ซุปเปอร์ไวซ์เซอร์" || f.Position == "สาวส่งนม")).ToList();


            users.Insert(0, new dbo_UserClass() { FullName_ddl = "==ระบุ==", User_ID = string.Empty });
            ddl_SPName.DataSource = users;
            ddl_SPName.DataBind();

            ddlUserSP.DataSource = users;
            ddlUserSP.DataBind();

            ddlUserRepresent.DataSource = users;
            ddlUserRepresent.DataBind();

        }
        catch (Exception ex)
        {
            // ddlUserSP.Items.Insert(0, new ListItem("--Select--", "0"));
            logger.Error(ex.Message);
        }
    }
    #endregion   
}