#region Using
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
#endregion

public partial class Views_ReceivingList : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            //ddlSearchBillingType.SelectedIndex = 1;
            ddlSearchInvoice_Status.SelectedIndex = 1;
            if (user_class.User_Group_ID != "Agent")
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);
            }
            else
            {
                string Billing_ID = string.Empty;

                if (Session["Billing_ID"] != null)
                {
                    Billing_ID = (string)Session["Billing_ID"];
                }
                else if (Session["Invoice_No"] != null)
                {
                    List<dbo_BillingClass> billing = dbo_BillingDataClass.Search("", "", "", (string)Session["Invoice_No"], null, null, "", "");
                    if (billing.Count > 0)
                    {
                        Billing_ID = billing[0].Billing_ID;
                    }
                }

                Session.Remove("Billing_ID");
                Session.Remove("Invoice_No");


                logger.Debug("Billing_ID " + Billing_ID);

                btnSearchSubmit_Click(sender, e);


                if (!string.IsNullOrEmpty(Billing_ID))
                {
                    pnlForm.Visible = true;
                    pnlGrid.Visible = false;

                    dbo_BillingClass bill = dbo_BillingDataClass.Select_Record(Billing_ID);
                    DateTime? OrderDate = bill.Invoice_Date;// bill.Invoice_Date.Value.ToShortDateString();

                    Session.Remove("GetProduct_Quantity_tab_1");
                    List<dbo_ProductClass> item1 = dbo_ProductDataClass.SelectBillingByProductGroupID("นมสดพาสเจอร์ไรส์", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                    Session["GetProduct_Quantity_tab_1"] = item1;
                    if (item1.Count > 0)
                    {
                        GridViewReceiving_1.DataSource = item1;
                        GridViewReceiving_1.DataBind();
                        GridViewReceiving_1.Visible = true;
                        li_01.Visible = true;
                    }
                    else
                    {
                        GridViewReceiving_1.Visible = false;
                        li_01.Visible = false;
                    }



                    Session.Remove("GetProduct_Quantity_tab_2");
                    List<dbo_ProductClass> item2 = dbo_ProductDataClass.SelectBillingByProductGroupID("นมเปรี้ยว", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                    Session["GetProduct_Quantity_tab_2"] = item2;
                    if (item2.Count > 0)
                    {
                        GridViewReceiving_2.DataSource = item2;
                        GridViewReceiving_2.DataBind();
                        GridViewReceiving_2.Visible = true;
                        li_02.Visible = true;
                    }
                    else
                    {
                        GridViewReceiving_2.Visible = false;
                        li_02.Visible = false;
                    }

                    Session.Remove("GetProduct_Quantity_tab_3");
                    List<dbo_ProductClass> item3 = dbo_ProductDataClass.SelectBillingByProductGroupID("โยเกิร์ตเมจิ", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                    Session["GetProduct_Quantity_tab_3"] = item3;
                    if (item3.Count > 0)
                    {
                        GridViewReceiving_3.DataSource = item3;
                        GridViewReceiving_3.DataBind();
                        GridViewReceiving_3.Visible = true;
                        li_03.Visible = true;
                    }
                    else
                    {
                        GridViewReceiving_3.Visible = false;
                        li_03.Visible = false;
                    }

                    Session.Remove("GetProduct_Quantity_tab_4");
                    List<dbo_ProductClass> item4 = dbo_ProductDataClass.SelectBillingByProductGroupID("นมเปรี้ยวไพเกน", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                    Session["GetProduct_Quantity_tab_4"] = item4;
                    if (item4.Count > 0)
                    {
                        GridViewReceiving_4.DataSource = item4;
                        GridViewReceiving_4.DataBind();
                        GridViewReceiving_4.Visible = true;
                        li_04.Visible = true;
                    }
                    else
                    {
                        GridViewReceiving_4.Visible = false;
                        li_04.Visible = false;
                    }

                    Session.Remove("GetProduct_Quantity_tab_5");
                    List<dbo_ProductClass> item5 = dbo_ProductDataClass.SelectBillingByProductGroupID("อื่นๆ", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                    Session["GetProduct_Quantity_tab_5"] = item5;
                    if (item5.Count > 0)
                    {
                        GridViewReceiving_5.DataSource = item5;
                        GridViewReceiving_5.DataBind();
                        GridViewReceiving_5.Visible = true;
                        li_05.Visible = true;
                    }
                    else
                    {
                        GridViewReceiving_5.Visible = false;
                        li_05.Visible = false;
                    }

                    switch (bill.Billing_Type)
                    {
                        case "ZDOM":
                        case "YDOM":
                            LabelPageHeader.Text = "รับสินค้า";
                            break;
                        case "ZDCN":
                            LabelPageHeader.Text = "ลดหนี้";
                            break;
                        case "ZDDN":
                            LabelPageHeader.Text = "เพิ่มหนี้";
                            break;
                    }

                    txtInvoice_No.Text = bill.Invoice_No;
                    txtBilling_ID.Text = bill.Billing_ID;
                    txtReceiving_Date.Text = bill.Invoice_Date.HasValue ? bill.Invoice_Date.Value.ToShortDateString() :
                        DateTime.Now.ToShortDateString();

                    txtPO_No.Text = bill.PO_No;
                    txtInvoice_Date.Text = bill.PO_Date.HasValue ? bill.PO_Date.Value.ToShortDateString() : string.Empty;


                    txtInvoice_VAT.Text = bill.Vat.Value.ToString("#,##0.#0");
                    txtInvoice_Total.Text = bill.Total.Value.ToString("#,##0.#0");
                    txtInvoice_Net_Value.Text = bill.Net_Value.Value.ToString("#,##0.#0");

                    txtOrder_Status.Text = bill.Order_Status;
                    txtStatus.Text = bill.Invoice_Status;

                    CheckOwnerPosition();
                }
                else
                {
                    LabelPageHeader.Text = "รับสินค้า";
                }
            }
        }
    }

    protected void btnBacktogrid_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlForm.Visible = false;
        pnlGrid.Visible = true;

        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

        LabelPageHeader.Text = "รับสินค้า";
        //List<dbo_BillingClass> item = dbo_BillingDataClass.Search(user_class.CV_CODE);
        //GridViewReceiving.DataSource = item;
        //GridViewReceiving.DataBind();
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SearchSubmit();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        txtSearchInvoice_No.Text = string.Empty;
        ddlSearchBillingType.ClearSelection();
        txtSearchPO_DateStart.Text = string.Empty;
        txtSearchPO_DateEnd.Text = string.Empty;
        //ddlSearchInvoice_Status.ClearSelection();
        ddlSearchInvoice_Status.SelectedIndex = 1;
        txtSearchPO_No.Text = string.Empty;
        //ddlSearchBillingType.SelectedIndex = 1;
        if (GridViewReceiving.Rows.Count > 0)
        {
            List<dbo_BillingClass> itm = new List<dbo_BillingClass>();
            GridViewReceiving.DataSource = itm;
            GridViewReceiving.DataBind();
            GridViewReceiving.Visible = false;
        }


        pnlNoRec.Visible = false;
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    public void btnSave_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            dbo_CountStockClass stock1 = dbo_CountStockDataClass.Search(null, string.Empty, string.Empty, user_class.CV_CODE).FirstOrDefault(f => f.Status == "รอการคอนเฟิร์ม");
            dbo_OrderingClass ordering_ = new dbo_OrderingClass();
            ordering_ = dbo_OrderingDataClass.Select_Record(txtPO_No.Text);

            if (stock1 != null)
            {
                logger.Debug(stock1.Status + " " + stock1.Count_No);
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ระหว่างการนับสต๊อก ไม่สามารถทำการรับสินค้าได้");
            }
            else
            {
                string Billing_ID = txtBilling_ID.Text;
                dbo_BillingClass bill = dbo_BillingDataClass.Select_Record(Billing_ID);

                if (bill.Invoice_Status == "ยกเลิก" || bill.Invoice_Status == "ยกเลิกแล้ว")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ใบแจ้งหนี้ถูกยกเลิกแล้ว ไม่สามารถบันทึกรับสินค้าได้");
                }
                else if (bill.Invoice_Status == "ยืนยันแล้ว")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ใบแจ้งหนี้ได้ทำการบันทึกรับสินค้าแล้ว");
                }
                else if (bill.Order_Status == "รอ ซีพี-เมจิ รับข้อมูล")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("สถานะใบสั่งซื้อ ‘รอ ซีพี-เมจิ รับข้อมูล’ ไม่สามารถบันทึกรับสินค้าได้");
                }
                else if (bill.Order_Status == "ยกเลิกโดย ซีพี-เมจิ")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("สถานะใบสั่งซื้อ ‘ยกเลิกโดย ซีพี-เมจิ’ ไม่สามารถบันทึกรับสินค้าได้");
                }
                else if (bill.Order_Status == "ยกเลิกโดยเอเย่นต์")
                {
                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("สถานะใบสั่งซื้อ ‘ยกเลิกโดยเอเย่นต์’ ไม่สามารถบันทึกรับสินค้าได้");
                }
                //else if (bill.Billing_Type == "ZDOM")
                //{
                //    if (ordering_ == null)
                //    {
                //        System.Threading.Thread.Sleep(500);
                //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //        Show("ไม่พบ Order ที่มีใบสั่งซื้อ :" + txtPO_No.Text + " ไม่สามารถบันทึกรับสินค้าได้");
                //    }
                //}
               
                else
                {
                    dbo_ReceiveClass receive = GetReceive();

                    receive.Invoice_No = bill.Invoice_No;
                    receive.Invoice_Date = bill.Invoice_Date;
                    receive.Ref_Invoice_No = bill.Ref_Invoice_No;


                    List<dbo_ReceiveDetailClass> listreceive = SetReceiveDetail(receive.Receive_ID);
                    List<dbo_ProductClass> product = dbo_ProductDataClass.Search(string.Empty, string.Empty, null, null, string.Empty);
                    foreach (dbo_ReceiveDetailClass receivedetail in listreceive)
                    {
                        dbo_ReceiveDetailDataClass.Add(receivedetail, HttpContext.Current.Request.Cookies["User_ID"].Value);

                        string Product_ID = string.Empty;

                        logger.Debug(receivedetail.Material_No.Trim());

                        foreach (dbo_ProductClass prod in product)
                        {

                            if (receivedetail.Material_No.Trim() == prod.SAP_Product_Code.Trim())
                            {
                                Product_ID = prod.Product_ID;
                            }
                        }


                        if (bill.Billing_Type == "ZDOM" && !string.IsNullOrEmpty(bill.Ref_Invoice_No))
                        {
                            dbo_StockMovementClass movement = new dbo_StockMovementClass();

                            movement.CV_CODE = bill.CV_Number;
                            movement.Date = DateTime.Now;
                            movement.Movement_Type = "รับสินค้า";
                            movement.Product_List_ID = Product_ID;
                            movement.Qty = short.Parse(receivedetail.Qty.ToString());

                            dbo_StockMovementDataClass.Add(movement);
                            logger.Debug("movement.Product_List_ID  " + movement.Product_List_ID);
                        }


                        List<dbo_StockClass> prev_stock = dbo_StockDataClass.Search(user_class.CV_CODE, string.Empty, Product_ID);

                        logger.Debug("prev_stock.Count " + prev_stock.Count);

                        if (prev_stock.Count > 0)
                        {

                            logger.Debug("bill.Order_Type " + bill.Order_Type);


                            if (bill.Order_Type == "ZDOM")
                            {
                                dbo_StockClass stock = prev_stock[prev_stock.Count - 1];

                                stock.Stock_In += receivedetail.Qty;
                                stock.Stock_End = short.Parse((stock.Stock_Begin + stock.Stock_In - stock.Stock_Out).ToString());

                                stock.Product_ID = Product_ID;

                                dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                            }
                            else if (bill.Order_Type == "ZDRA" || bill.Order_Type == "ZDRT") //CN QTY
                            {
                                dbo_StockClass stock = prev_stock[prev_stock.Count - 1];


                                stock.Stock_Out += receivedetail.Qty;
                                stock.Stock_End = short.Parse((stock.Stock_Begin + stock.Stock_In - stock.Stock_Out).ToString());

                                stock.Product_ID = Product_ID;


                                dbo_StockDataClass.Update(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                            }

                        }
                        else
                        {
                            dbo_StockClass stock = new dbo_StockClass();
                            stock.Stock_on_Hand_ID = GenerateID.Stock_on_Hand_ID(user_class.CV_CODE);
                            stock.CV_Code = user_class.CV_CODE;
                            stock.Stock_Begin = 0;
                            stock.Stock_In = receivedetail.Qty;
                            stock.Stock_Out = 0;
                            stock.Stock_End = receivedetail.Qty;
                            stock.Product_ID = Product_ID;// receivedetail.Material_No;
                            stock.Date = DateTime.Now;

                            dbo_StockDataClass.Add(stock, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }
                    }
                    dbo_ReceiveDataClass.Add(receive, HttpContext.Current.Request.Cookies["User_ID"].Value);


                    if (bill.Billing_Type == "ZDOM" && !string.IsNullOrEmpty(bill.Ref_Invoice_No))
                    {
                        dbo_RevenueExpenseClass rev = new dbo_RevenueExpenseClass();
                        rev.Post_No = GenerateID.Post_No(user_class.CV_CODE);
                        rev.CV_Code = user_class.CV_CODE;
                        rev.Amount = receive.Invoice_Total;
                        rev.Account_Code = "5010";
                        rev.Account_No = GenerateID.EP(user_class.CV_CODE);
                        rev.Remark = string.Empty;
                        rev.Post_Date = DateTime.Now;
                        bool succes = false;
                        succes = dbo_RevenueExpenseDataClass.Add(rev);
                    }

                    string s = DateTime.Now.ToString();
                    string now = DateTime.Parse(s).ToString("yyyyMMdd");
                    string year = now.Substring(0, 4);
                    string month = now.Substring(4, 2);

                    dbo_SalesTargetClass sale_target = dbo_SalesTargetDataClass.Select_Record(user_class.CV_CODE, year, month);

                    if (bill.Order_Type == "ZDOM" || bill.Order_Type == "ZDDN")
                    {
                        sale_target.Actual_Sales += receive.Invoice_Net_Value;
                    }
                    else if (bill.Order_Type == "ZDRA" || bill.Order_Type == "ZDRT" || bill.Order_Type == "ZDCN")
                    {
                        sale_target.Actual_Sales -= receive.Invoice_Net_Value;
                    }
                    dbo_SalesTargetDataClass.Update(sale_target);


                    if (bill.Billing_Type == "ZDOM" && string.IsNullOrEmpty(bill.Ref_Invoice_No))
                    {

                        // dbo_OrderingClass ordering_ = new dbo_OrderingClass();
                        ordering_ = new dbo_OrderingClass();
                        ordering_ = dbo_OrderingDataClass.Select_Record(txtPO_No.Text);
                       
                        if(ordering_ != null )
                        {
                            ordering_.Order_Status = "รับสินค้าแล้ว";
                            dbo_OrderingDataClass.Update(ordering_, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        }
                      
                    }
                    // dbo_BillingClass billing = dbo_BillingDataClass.Select_Record(txtBilling_ID.Text);


                    bill.Invoice_Status = "ยืนยันแล้ว";
                    dbo_BillingDataClass.Update(bill);

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("บันทึกสำเร็จ");

                    pnlForm.Visible = false;
                    pnlGrid.Visible = true;

                    SearchSubmit();

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void TxtId_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            GridView currentRow = (GridView)((TextBox)sender).Parent.Parent.Parent.Parent;


            switch (currentRow.ID)
            {
                case "GridViewReceiving_1":

                    hfTab.Value = "tab_default_1";

                    break;
                case "GridViewReceiving_2":

                    hfTab.Value = "tab_default_2";
                    break;
                case "GridViewReceiving_3":

                    hfTab.Value = "tab_default_3";
                    break;
                case "GridViewReceiving_4":

                    hfTab.Value = "tab_default_4";
                    break;
                case "GridViewReceiving_5":

                    hfTab.Value = "tab_default_5";
                    break;
            }

        }
        catch (Exception)
        {

        }

    }
    #endregion

    #region Methods
    private string Product_ID_LookUp(string Material)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ProductClass> product = dbo_ProductDataClass.Search(string.Empty, string.Empty, null, null, string.Empty);

        foreach (dbo_ProductClass prod in product)
        {
            if (Material == prod.SAP_Product_Code)
            {
                return prod.Product_ID;
            }
        }

        return "";
    }

    private void CheckOwnerPosition()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
        if (user_class.Position != "เจ้าของ")
        {

            try
            {
                foreach (GridViewRow currentRow in GridViewReceiving_1.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("lbl_Price");
                    lbl_Price_.Text = "0.00";
                    Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    lbl_Amount_.Text = "0.00";
                }

            }
            catch (Exception) { }

            try
            {
                foreach (GridViewRow currentRow in GridViewReceiving_2.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("lbl_Price");
                    lbl_Price_.Text = "0.00";
                    Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    lbl_Amount_.Text = "0.00";
                }

            }
            catch (Exception) { }

            try
            {
                foreach (GridViewRow currentRow in GridViewReceiving_3.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("lbl_Price");
                    lbl_Price_.Text = "0.00";
                    Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    lbl_Amount_.Text = "0.00";
                }

            }
            catch (Exception) { }

            try
            {
                foreach (GridViewRow currentRow in GridViewReceiving_4.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("lbl_Price");
                    lbl_Price_.Text = "0.00";
                    Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    lbl_Amount_.Text = "0.00";
                }
            }
            catch (Exception) { }

            try
            {
                foreach (GridViewRow currentRow in GridViewReceiving_5.Rows)
                {
                    Label lbl_Price_ = (Label)currentRow.FindControl("lbl_Price");
                    lbl_Price_.Text = "0.00";
                    Label lbl_Amount_ = (Label)currentRow.FindControl("lbl_Amount");
                    lbl_Amount_.Text = "0.00";
                }
            }
            catch (Exception) { }
        }
    }

    private dbo_ReceiveClass GetReceive()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_ReceiveClass receive = new dbo_ReceiveClass();
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            receive.Billing_ID = txtBilling_ID.Text;
            receive.Receive_ID = GenerateID.Receive_ID(user_class.CV_CODE);
            logger.Debug("receive.Receive_ID " + receive.Receive_ID);
            receive.Invoice_Net_Value = Decimal.Parse(txtInvoice_Net_Value.Text.Replace(",", string.Empty));
            receive.Invoice_VAT = Decimal.Parse(txtInvoice_VAT.Text.Replace(",", string.Empty));
            receive.Invoice_Total = Decimal.Parse(txtInvoice_Total.Text.Replace(",", string.Empty));
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        return receive;
    }

    private List<dbo_ReceiveDetailClass> SetReceiveDetail(string Receive_ID)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ReceiveDetailClass> listofreceive = new List<dbo_ReceiveDetailClass>();
        int index = 1;
        try
        {
            foreach (GridViewRow currentRow in GridViewReceiving_1.Rows)
            {
                dbo_ReceiveDetailClass detail = new dbo_ReceiveDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lblBilling_Detail_ID_ = (Label)currentRow.FindControl("lblBilling_Detail_ID");

                // Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0" && !string.IsNullOrEmpty(lblBilling_Detail_ID_.Text))
                {
                    detail.Billing_Detail_ID = lblBilling_Detail_ID_.Text;
                    detail.Receive_Detail_ID = Receive_ID + index.ToString("00");
                    index++;

                    dbo_BillingDetailClass bill_detail = dbo_BillingDetailDataClass.Select_Record(detail.Billing_Detail_ID);

                    detail.Qty = bill_detail.Qty;
                    detail.Material_No = bill_detail.Material_No;
                    detail.Net_Value = bill_detail.Net_Value;
                    detail.Total = bill_detail.Total;
                    detail.UOM = bill_detail.UOM;
                    detail.Vat = bill_detail.Vat;

                    listofreceive.Add(detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            foreach (GridViewRow currentRow in GridViewReceiving_2.Rows)
            {
                dbo_ReceiveDetailClass detail = new dbo_ReceiveDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lblBilling_Detail_ID_ = (Label)currentRow.FindControl("lblBilling_Detail_ID");

                // Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0" && !string.IsNullOrEmpty(lblBilling_Detail_ID_.Text))
                {
                    detail.Billing_Detail_ID = lblBilling_Detail_ID_.Text;
                    detail.Receive_Detail_ID = Receive_ID + index.ToString("00");
                    index++;

                    dbo_BillingDetailClass bill_detail = dbo_BillingDetailDataClass.Select_Record(detail.Billing_Detail_ID);

                    detail.Qty = bill_detail.Qty;
                    detail.Material_No = bill_detail.Material_No;
                    detail.Net_Value = bill_detail.Net_Value;
                    detail.Total = bill_detail.Total;
                    detail.UOM = bill_detail.UOM;
                    detail.Vat = bill_detail.Vat;

                    listofreceive.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            foreach (GridViewRow currentRow in GridViewReceiving_3.Rows)
            {
                dbo_ReceiveDetailClass detail = new dbo_ReceiveDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lblBilling_Detail_ID_ = (Label)currentRow.FindControl("lblBilling_Detail_ID");

                // Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0" && !string.IsNullOrEmpty(lblBilling_Detail_ID_.Text))
                {
                    detail.Billing_Detail_ID = lblBilling_Detail_ID_.Text;
                    detail.Receive_Detail_ID = Receive_ID + index.ToString("00");
                    index++;

                    dbo_BillingDetailClass bill_detail = dbo_BillingDetailDataClass.Select_Record(detail.Billing_Detail_ID);

                    detail.Qty = bill_detail.Qty;
                    detail.Material_No = bill_detail.Material_No;
                    detail.Net_Value = bill_detail.Net_Value;
                    detail.Total = bill_detail.Total;
                    detail.UOM = bill_detail.UOM;
                    detail.Vat = bill_detail.Vat;

                    listofreceive.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            foreach (GridViewRow currentRow in GridViewReceiving_4.Rows)
            {
                dbo_ReceiveDetailClass detail = new dbo_ReceiveDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lblBilling_Detail_ID_ = (Label)currentRow.FindControl("lblBilling_Detail_ID");

                // Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0" && !string.IsNullOrEmpty(lblBilling_Detail_ID_.Text))
                {
                    detail.Billing_Detail_ID = lblBilling_Detail_ID_.Text;
                    detail.Receive_Detail_ID = Receive_ID + index.ToString("00");
                    index++;

                    dbo_BillingDetailClass bill_detail = dbo_BillingDetailDataClass.Select_Record(detail.Billing_Detail_ID);

                    detail.Qty = bill_detail.Qty;
                    detail.Material_No = bill_detail.Material_No;
                    detail.Net_Value = bill_detail.Net_Value;
                    detail.Total = bill_detail.Total;
                    detail.UOM = bill_detail.UOM;
                    detail.Vat = bill_detail.Vat;

                    listofreceive.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {
            foreach (GridViewRow currentRow in GridViewReceiving_5.Rows)
            {
                dbo_ReceiveDetailClass detail = new dbo_ReceiveDetailClass();

                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lblBilling_Detail_ID_ = (Label)currentRow.FindControl("lblBilling_Detail_ID");

                // Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0" && !string.IsNullOrEmpty(lblBilling_Detail_ID_.Text))
                {
                    detail.Billing_Detail_ID = lblBilling_Detail_ID_.Text;
                    detail.Receive_Detail_ID = Receive_ID + index.ToString("00");
                    index++;

                    dbo_BillingDetailClass bill_detail = dbo_BillingDetailDataClass.Select_Record(detail.Billing_Detail_ID);

                    detail.Qty = bill_detail.Qty;
                    detail.Material_No = bill_detail.Material_No;
                    detail.Net_Value = bill_detail.Net_Value;
                    detail.Total = bill_detail.Total;
                    detail.UOM = bill_detail.UOM;
                    detail.Vat = bill_detail.Vat;

                    listofreceive.Add(detail);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


        return listofreceive;
    }

    public void Show(string message)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        logger.Info(message);
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
        catch (Exception)
        {

        }
        //}
    }

    private void SearchSubmit()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            string bType = (ddlSearchBillingType.SelectedIndex == 0 ? string.Empty : ddlSearchBillingType.SelectedValue);
            string iStatus = (ddlSearchInvoice_Status.SelectedIndex == 0 ? string.Empty : ddlSearchInvoice_Status.SelectedValue);
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (!string.IsNullOrEmpty(txtSearchPO_DateStart.Text))
            {
                startDate = DateTime.Parse(txtSearchPO_DateStart.Text);
            }

            if (!string.IsNullOrEmpty(txtSearchPO_DateEnd.Text))
            {
                endDate = DateTime.Parse(txtSearchPO_DateEnd.Text);
            }

            List<dbo_BillingClass> bill_All = dbo_BillingDataClass.Search(user_class.CV_CODE, string.Empty, bType, txtSearchInvoice_No.Text, startDate, endDate, txtSearchPO_No.Text, iStatus);
            List<dbo_BillingClass> bill_ZDOM = new List<dbo_BillingClass>();
            foreach (dbo_BillingClass f in bill_All)
            {
                if (f.Billing_Type != "YDOM" && !(f.Billing_Type == "ZDOM" && !string.IsNullOrEmpty(f.Ref_Invoice_No)))
                {
                    bill_ZDOM.Add(f);
                }
            }


            //List<dbo_BillingClass> bill_ZDOM = bill_All.Where(f => f.Billing_Type != "YDOM")
            //   .Where(f => (

            //       !(f.Billing_Type == "ZDOM" && !string.IsNullOrEmpty(f.Ref_Invoice_No))

            //       )).ToList();

            if (bill_ZDOM.Count > 0)
            {

                GridViewReceiving.DataSource = bill_ZDOM;
                GridViewReceiving.DataBind();

                GridViewReceiving.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewReceiving.Visible = false;
                pnlNoRec.Visible = true;
            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    private void CancelSearch()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    #endregion

    #region GridView Events
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewReceiving.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewReceiving.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewReceiving_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewReceiving.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewReceiving.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewReceiving.PageIndex)
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
            int currentPage = GridViewReceiving.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewReceiving.PageCount.ToString();

        }
    }

    protected void GridViewReceiving_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            if (e.CommandName == "View")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string Billing_ID = lnkView.CommandArgument;

                pnlForm.Visible = true;
                pnlGrid.Visible = false;

                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

                dbo_BillingClass bill = dbo_BillingDataClass.Select_Record(Billing_ID);
                DateTime? OrderDate = bill.Invoice_Date;// bill.Invoice_Date.Value.ToShortDateString();

                Session.Remove("GetProduct_Quantity_tab_1");
                List<dbo_ProductClass> item1 = dbo_ProductDataClass.SelectBillingByProductGroupID("นมสดพาสเจอร์ไรส์", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                Session["GetProduct_Quantity_tab_1"] = item1;
                if (item1.Count > 0)
                {
                    GridViewReceiving_1.DataSource = item1;
                    GridViewReceiving_1.DataBind();
                    GridViewReceiving_1.Visible = true;
                    li_01.Visible = true;
                }
                else
                {
                    GridViewReceiving_1.Visible = false;
                    li_01.Visible = false;
                }

                Session.Remove("GetProduct_Quantity_tab_2");
                List<dbo_ProductClass> item2 = dbo_ProductDataClass.SelectBillingByProductGroupID("นมเปรี้ยว", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                Session["GetProduct_Quantity_tab_2"] = item2;
                if (item2.Count > 0)
                {
                    GridViewReceiving_2.DataSource = item2;
                    GridViewReceiving_2.DataBind();
                    GridViewReceiving_2.Visible = true;

                    li_02.Visible = true;
                }
                else
                {
                    GridViewReceiving_2.Visible = false;
                    li_02.Visible = false;
                }

                Session.Remove("GetProduct_Quantity_tab_3");
                List<dbo_ProductClass> item3 = dbo_ProductDataClass.SelectBillingByProductGroupID("โยเกิร์ตเมจิ", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                Session["GetProduct_Quantity_tab_3"] = item3;
                if (item3.Count > 0)
                {
                    GridViewReceiving_3.DataSource = item3;
                    GridViewReceiving_3.DataBind();
                    GridViewReceiving_3.Visible = true;

                    li_03.Visible = true;
                }
                else
                {
                    GridViewReceiving_3.Visible = false;
                    li_03.Visible = false;
                }

                Session.Remove("GetProduct_Quantity_tab_4");
                List<dbo_ProductClass> item4 = dbo_ProductDataClass.SelectBillingByProductGroupID("นมเปรี้ยวไพเกน", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                Session["GetProduct_Quantity_tab_4"] = item4;
                if (item4.Count > 0)
                {
                    GridViewReceiving_4.DataSource = item4;
                    GridViewReceiving_4.DataBind();
                    GridViewReceiving_4.Visible = true;
                    li_04.Visible = true;
                }
                else
                {
                    GridViewReceiving_4.Visible = false;
                    li_04.Visible = false;
                }

                Session.Remove("GetProduct_Quantity_tab_5");
                List<dbo_ProductClass> item5 = dbo_ProductDataClass.SelectBillingByProductGroupID("อื่นๆ", Billing_ID, user_class.CV_CODE, bill.Invoice_Date);
                Session["GetProduct_Quantity_tab_5"] = item5;
                if (item5.Count > 0)
                {
                    GridViewReceiving_5.DataSource = item5;
                    GridViewReceiving_5.DataBind();
                    GridViewReceiving_5.Visible = true;
                    li_05.Visible = true;
                }
                else
                {
                    GridViewReceiving_5.Visible = false;
                    li_05.Visible = false;
                }

                switch (bill.Billing_Type)
                {
                    case "ZDOM":
                    case "YDOM":
                        LabelPageHeader.Text = "รับสินค้า";
                        break;
                    case "ZDCN":
                        LabelPageHeader.Text = "ลดหนี้";
                        break;
                    case "ZDDN":
                        LabelPageHeader.Text = "เพิ่มหนี้";
                        break;
                }


                txtInvoice_No.Text = bill.Invoice_No;



                txtBilling_ID.Text = bill.Billing_ID;
                txtReceiving_Date.Text = bill.Invoice_Date.HasValue ? bill.Invoice_Date.Value.ToShortDateString() :
                    DateTime.Now.ToShortDateString();

                txtPO_No.Text = bill.PO_No;

                txtInvoice_Date.Text = bill.PO_Date.HasValue ? bill.PO_Date.Value.ToShortDateString() : string.Empty;


                //bill.PO_Date.Value.ToShortDateString();




                txtInvoice_VAT.Text = bill.Vat.Value.ToString("#,##0.#0");
                txtInvoice_Total.Text = bill.Total.Value.ToString("#,##0.#0");
                txtInvoice_Net_Value.Text = bill.Net_Value.Value.ToString("#,##0.#0");

                txtOrder_Status.Text = bill.Order_Status;
                txtStatus.Text = bill.Invoice_Status;


                CheckOwnerPosition();

                if(txtStatus.Text == "ยืนยันแล้ว")
                {
                    btnSave.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                }

            }

        }

        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewReceiving_1_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_1"];
        Session.Remove("GetProduct_Quantity_tab_1");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewReceiving_1.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {

                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;


                // string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 7;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                //row.Cells[7].Visible = false;
                //row.Cells[8].Visible = false;


                //row.Cells[0].Text = listProduct_Quantity[i].Product_Name;
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }

        }
    }

    protected void GridViewReceiving_2_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_2"];
        Session.Remove("GetProduct_Quantity_tab_2");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewReceiving_2.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {

                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;


                // string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 7;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                //row.Cells[7].Visible = false;
                //row.Cells[8].Visible = false;


                //row.Cells[0].Text = listProduct_Quantity[i].Product_Name;
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }

        }
    }

    protected void GridViewReceiving_3_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_3"];
        Session.Remove("GetProduct_Quantity_tab_3");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewReceiving_3.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {

                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;


                // string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 7;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                //row.Cells[7].Visible = false;
                //row.Cells[8].Visible = false;


                //row.Cells[0].Text = listProduct_Quantity[i].Product_Name;
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }

        }
    }

    protected void GridViewReceiving_4_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_4"];
        Session.Remove("GetProduct_Quantity_tab_4");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewReceiving_4.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {

                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;


                // string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 7;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                //row.Cells[7].Visible = false;
                //row.Cells[8].Visible = false;


                //row.Cells[0].Text = listProduct_Quantity[i].Product_Name;
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }

        }
    }

    protected void GridViewReceiving_5_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_5"];
        Session.Remove("GetProduct_Quantity_tab_5");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewReceiving_5.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {

                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;


                // string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 7;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                //row.Cells[7].Visible = false;
                //row.Cells[8].Visible = false;


                //row.Cells[0].Text = listProduct_Quantity[i].Product_Name;
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }

        }
    }
    #endregion    
}