#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_OrderingList : System.Web.UI.Page
{
    #region Private Var
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    decimal oldBeforeAmount;
    decimal oldVat;
    decimal oldAfterAmount;
    string _PO_Number;
    string _CV_code;
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtOrderingStartDate.Text = DateTime.Now.ToShortDateString();
            txtOrderingEndDate.Text = DateTime.Now.ToShortDateString();
            txtRecievingStartDate.Text = DateTime.Now.ToShortDateString();
            txtRecievingEndDate.Text = DateTime.Now.ToShortDateString();
            DropDownListOrderingStatus.SelectedIndex = 1;
            string RPT = Request.QueryString["RPT"];
            string PRM = Request.QueryString["PRM"];
            _PO_Number = PRM;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            txtDate_of_delivery_goods.Attributes.Add("onchange", "myApp.showPleaseWait();");
           
            if (user_class.User_Group_ID == "Agent" || (user_class.Role_ID == "03" && PRM == null))
            {
                hdfPosition.Value = user_class.Position;

                pnlGrid.Visible = true;
                pnlForm.Visible = false;
                btnSearchSubmit_Click(sender, e);
            }
            else if (PRM != null)
            {
                //pnlGrid.Visible = true;
                //pnlForm.Visible = false;
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                GetDetailsDataToForm(PRM, "View");
            }
            else if (user_class.Role_ID == "03" && PRM != null)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                GetDetailsDataToForm(PRM, "View");
            }

            else
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
        //txtOrderingStartDate.Text = DateTime.Now.ToShortDateString();
        //txtOrderingEndDate.Text = DateTime.Now.ToShortDateString();
        //txtRecievingStartDate.Text = DateTime.Now.ToShortDateString();
        //txtRecievingEndDate.Text = DateTime.Now.ToShortDateString();
        
    }

    public void btnSave_Click(object sender, System.EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (btnSave.Text == "แก้ไข")
        {
            dbo_OrderingClass item = dbo_OrderingDataClass.Select_Record(txtPO_Number.Text);
            if (item.Order_Status == "ซีพี-เมจิ รับข้อมูลแล้ว" || item.Order_Status == "รับสินค้าแล้ว" || item.Order_Status == "ยกเลิกโดยเอเย่นต์" || item.Order_Status == "ยกเลิกโดย ซีพี-เมจิ" || item.Date_of_CP_receive_transaction < DateTime.Now)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ไม่สามารถแก้ไขข้อมูลใบสั่งซื้อ");
            }
            else
            {
                #region
                //if(item.Date_of_CP_receive_transaction  < DateTime.Now)
                //{
                //    Show("ไม่สามารถทำการแก้ไขข้อมูลการสั่งซื้อได้หลัง วันที่ ซีพี-เมจิ รับข้อมูลแล้ว");
                //}
                //else
                //{
                //    GetDetailsDataToForm(txtPO_Number.Text, "Edit");
                //}
                #endregion
                GetDetailsDataToForm(txtPO_Number.Text, "Edit");
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

            }
        }
        else
        {

            if (!string.IsNullOrEmpty(txtDate_of_delivery_goods.Text))
            {

                if (btnSaveMode.Value == "บันทึก")
                {
                    logger.Debug("==start==");
                    dbo_OrderingClass ordering = SetOrderingData();
                    //  logger.Debug("==1==");
                    List<dbo_OrderingDetailClass> ordering_detail = SetOrderingDetailData();
                    // logger.Debug("==2==");
                    dbo_OrderingDataClass.Add(ordering, HttpContext.Current.Request.Cookies["User_ID"].Value);
                    //  logger.Debug("==3==");

                    foreach (dbo_OrderingDetailClass detail in ordering_detail)
                    {
                        // logger.Debug("==>");
                        dbo_OrderingDetailDataClass.Add(detail, HttpContext.Current.Request.Cookies["User_ID"].Value);
                        // logger.Debug("==<");
                    }
                    logger.Debug("==end==");

                    Show("บันทึกสำเร็จ");

                }
                else //gg
                {
                    dbo_OrderingClass item01 = dbo_OrderingDataClass.Select_Record(txtPO_Number.Text);

                    if (item01.Date_of_CP_receive_transaction > DateTime.Now)
                    {
                        dbo_OrderingClass ordering = SetOrderingData();

                        dbo_OrderingDataClass.Update(ordering, HttpContext.Current.Request.Cookies["User_ID"].Value);

                        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);




                        if (!string.IsNullOrEmpty(user_class.CV_CODE))
                        {

                            List<dbo_OrderingDetailClass> detail = dbo_OrderingDetailDataClass.Search(ordering.PO_Number);
                            foreach (dbo_OrderingDetailClass _detail in detail)
                            {
                                dbo_OrderingDetailDataClass.Delete(_detail.Ordering_Detail_ID);
                            }


                            List<dbo_OrderingDetailClass> ordering_detail = SetOrderingDetailData();
                            foreach (dbo_OrderingDetailClass _detail in ordering_detail)
                            {

                                dbo_OrderingDetailDataClass.Add(_detail, HttpContext.Current.Request.Cookies["User_ID"].Value);
                            }
                            #region
                            /*
                            List<dbo_OrderingDetailClass> ordering_detail = SetOrderingDetailData();
                            foreach (dbo_OrderingDetailClass detail in ordering_detail)
                            {
                                if (dbo_OrderingDetailDataClass.Select_Record(detail.Ordering_Detail_ID) == null)
                                {
                                    dbo_OrderingDetailDataClass.Add(detail, HttpContext.Current.Request.Cookies["User_ID"].Value);
                                }
                                else
                                {
                                    dbo_OrderingDetailDataClass.Update(detail, HttpContext.Current.Request.Cookies["User_ID"].Value);
                                }
                            }
                             */
                            #endregion
                        }

                        Show("บันทึกสำเร็จ");


                    }
                    else
                    {
                        Show("ไม่สามารถทำการบันทึกข้อมูลการสั่งซื้อได้หลัง วันที่ ซีพี-เมจิ รับข้อมูลแล้ว");
                    }


                }

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);


                _PO_Number = Request.QueryString["PRM"];

                //Show("บันทึกสำเร็จ");
                if (_PO_Number != null)
                {
                    //string url = "../Views/ExportPO.aspx?RPT=Msg";
                    //string s = "window.open('" + url + "', '_self', '');";
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
                    //System.Threading.Thread.Sleep(1000);
                    Session["Save_SS"] = "Save_SS";
                    Response.Redirect("../Views/ExportPO.aspx");
                }
                else
                {
                    SearchSubmit();
                    pnlGrid.Visible = true;
                    pnlForm.Visible = false;
                   // Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }

            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณาระบุวันที่รับสินค้า:");
            }
        }

    }

    public void btnAddNew_Click(object sender, System.EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);
            /*
            if (string.IsNullOrEmpty(user_class.CV_CODE))
            {   //unraechable
                System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ไม่สามารถทำรายการได้");
            }
            */

            if (!clsdbo_Agent.Status.Value)
            {
                Show("ไม่สามารถทำรายการได้ เนื่องจากเอเย่นต์ได้ยกเลิกกิจการแล้ว");
            }
            else
            {
                pnlForm.Visible = true;
                pnlGrid.Visible = false;

                List<dbo_CycleAssignmentClass> item = dbo_CycleAssignmentDataClass.SelectAll();
                dbo_CycleAssignmentClass cycle = item.FirstOrDefault(f => f.CV_Code == user_class.CV_CODE);

                if (cycle == null)
                {
                    //System.Threading.Thread.Sleep(500);
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("ยังไม่กำหนด รอบสั่งรอบส่ง");
                    return;
                }

                txtDate_of_delivery_goods.Enabled = true;
                GetDetailsDataToForm(string.Empty, string.Empty);

                List<dbo_OrderAndDeliveryCycleValueClass> deliverydate = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle.Order_Cycle_ID);

                bool found = false;
                int index = 0;
                DateTime tmp_date = DateTime.Now;

                //do
                //{
                tmp_date = tmp_date.AddDays(index).Date;

                DayOfWeek _dayofweek = tmp_date.DayOfWeek;
                int validday = 0;

                switch (_dayofweek.ToString())
                {
                    case "Sunday":
                        validday = 1;
                        break;
                    case "Monday":
                        validday = 2;
                        break;
                    case "Tuesday":
                        validday = 3;
                        break;
                    case "Wednesday":
                        validday = 4;
                        break;
                    case "Thursday":
                        validday = 5;
                        break;
                    case "Friday":
                        validday = 6;
                        break;
                    case "Saturday":
                        validday = 7;
                        break;
                }
                //DateTime Hour = DateTime.Now;
                //int _hour = Hour.Hour;
                // int _min = Hour.Minute;
                //dbo_OrderAndDeliveryCycleValueClass delivery_ = deliverydate.FirstOrDefault(f => Convert.ToInt16(f.Order_Cycle_Date) >= Convert.ToInt16(validday) &&  int.Parse(f.Order_Cycle_Hour) >= _hour && int.Parse(f.Order_Cycle_Minute) >= _min);
                dbo_OrderAndDeliveryCycleValueClass delivery_ = deliverydate.FirstOrDefault(f => Convert.ToInt16(f.Order_Cycle_Date) >= Convert.ToInt16(validday));

                #region
                //dbo_OrderAndDeliveryCycleValueClass delivery_ = deliverydate.FirstOrDefault(f => f.Order_Cycle_Date == validday);
                //List<dbo_OrderAndDeliveryCycleValueClass> delivery_ = new List<dbo_OrderAndDeliveryCycleValueClass>(deliverydate.Where(f => f.Order_Cycle_Date_Name = _dayofweek).FirstOrDefault());

                //dbo_OrderAndDeliveryCycleValueClass delivery_ = deliverydate.FirstOrDefault(f => f.Order_Cycle_Date == validday);
                #endregion

                var _Dalivery_Date = "";
                var _Dalivery_Hour = "";
                var _Dalivery_Minute = "";
                bool flag = false;

  
               
                if (delivery_ != null)
                {
                    int addOrderdate = Convert.ToInt16(delivery_.Order_Cycle_Date) - validday; //6
                    int addDeliverydate = Convert.ToInt16(delivery_.Delivery_Cycle_Date) - validday;
                    //int addDeliverydate = (Convert.ToInt16(delivery_.Order_Cycle_Date) -  Convert.ToInt16(delivery_.Delivery_Cycle_Date);
                    txtDate_of_CP_receive_transaction.Text = tmp_date.AddDays(addOrderdate).ToShortDateString() + " " + delivery_.Order_Cycle_Hour + ":" + delivery_.Order_Cycle_Minute;

                    DateTime tmp_date1 = DateTime.Parse(txtDate_of_CP_receive_transaction.Text);
                    string str_tmp_date1 = Convert.ToString(tmp_date1.DayOfWeek.GetHashCode()+1);
                    var _dayofweek1 = DayOfWeek.Sunday;
                    var _dayofweek2 = tmp_date1.DayOfWeek;
                    foreach (var d in deliverydate)
                    {

                        if (d.Order_Cycle_Date == str_tmp_date1)
                        {

                            _Dalivery_Date = d.Delivery_Cycle_Date;
                            _Dalivery_Hour = d.Delivery_Cycle_Date;
                            _Dalivery_Minute = d.Delivery_Cycle_Date;
                            flag = true;
                            break;
                        }
                    }

                    if (_Dalivery_Date == "1")
                    {
                        _dayofweek1 = DayOfWeek.Sunday;
                    }
                    else if (_Dalivery_Date == "2")
                    {
                        _dayofweek1 = DayOfWeek.Monday;
                    }
                    else if (_Dalivery_Date == "3")
                    {
                        _dayofweek1 = DayOfWeek.Tuesday;
                    }
                    else if (_Dalivery_Date == "4")
                    {
                        _dayofweek1 = DayOfWeek.Wednesday;
                    }
                    else if (_Dalivery_Date == "5")
                    {
                        _dayofweek1 = DayOfWeek.Thursday;
                    }
                    else if (_Dalivery_Date == "6")
                    {
                        _dayofweek1 = DayOfWeek.Friday;
                    }
                    else if (_Dalivery_Date == "7")
                    {
                        _dayofweek1 = DayOfWeek.Saturday;
                    }


                    var daysDiff = (7 + (_dayofweek1 - _dayofweek2)) % 7;
                    int day = daysDiff;

                    txtDate_of_delivery_goods.Text = tmp_date1.AddDays(day).ToShortDateString();

                    //txtDate_of_delivery_goods.Text = tmp_date.AddDays(addDeliverydate).ToShortDateString();
                }
                else
                {
                    dbo_OrderAndDeliveryCycleValueClass delivery1 = deliverydate.FirstOrDefault(f => Convert.ToInt16(f.Order_Cycle_Date) >= 1);
                    int addOrderdate = 7 - validday + Convert.ToInt16(delivery1.Order_Cycle_Date);
                    int addDeliverydate = 7 - validday + Convert.ToInt16(delivery1.Delivery_Cycle_Date);
                    txtDate_of_CP_receive_transaction.Text = tmp_date.AddDays(addOrderdate).ToShortDateString() + " " + delivery1.Order_Cycle_Hour + ":" + delivery1.Order_Cycle_Minute;
                    txtDate_of_delivery_goods.Text = tmp_date.AddDays(addDeliverydate).ToShortDateString();
                }

                txtDate_of_delivery_goods_TextChanged(null, null);
                hfSaveMode.Value = "NEW";

               // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    public void butNo_Click(object sender, System.EventArgs e)
    {
        _PO_Number = Request.QueryString["PRM"];
        if (_PO_Number != null)
        {
            Response.Redirect("../Views/ExportPO.aspx");
        }
        else
        {
            SearchSubmit();
            pnlGrid.Visible = true;
            pnlForm.Visible = false;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }


        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        SearchSubmit();

    }

    protected void btnSearchCancel_Click(object sender, EventArgs e)
    {
        //txtOrderingStartDate.Text = string.Empty;
        //txtOrderingEndDate.Text = string.Empty;
        //txtRecievingStartDate.Text = string.Empty;
        //txtRecievingEndDate.Text = string.Empty;
       
        txtOrderingStartDate.Text = DateTime.Now.ToShortDateString();
        txtOrderingEndDate.Text = DateTime.Now.ToShortDateString();
        txtRecievingStartDate.Text = DateTime.Now.ToShortDateString();
        txtRecievingEndDate.Text = DateTime.Now.ToShortDateString();
        txtOderingNumber.Text = string.Empty;
        // DropDownListOrderingStatus.ClearSelection();
        DropDownListOrderingStatus.SelectedIndex = 1;

        if (GridViewOrdering.Rows.Count > 0)
        {
            List<dbo_OrderingClass> OrderingList = new List<dbo_OrderingClass>();// dbo_OrderingDataClass.Search(string.Empty);
            GridViewOrdering.DataSource = OrderingList;
            GridViewOrdering.DataBind();
            GridViewOrdering.Visible = false;
        }


        pnlGrid.Visible = true;
        pnlForm.Visible = false;
        pnlNoRec.Visible = false;

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void TxtId_TextChanged(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            //  Decimal sum = getAllRow();

            //GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            //Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");
            //lbl_Amount.Text = "99";

            // GridView currentRow = (GridView)((TextBox)sender).Parent.Parent.Parent.Parent;


            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            Label lbl_Price = (Label)currentRow.FindControl("lbl_Price");
            HiddenField hdfOldQty = (HiddenField)currentRow.FindControl("hdfOldQty");
            HiddenField hdfVat = (HiddenField)currentRow.FindControl("hdfVat");
            Label lbl_Suggest_Quantity = (Label)currentRow.FindControl("lbl_Suggest_Quantity");

            decimal newVat;
            decimal newAmt;
            if (hdfOldQty.Value != "")
            {
                oldBeforeAmount = int.Parse(hdfOldQty.Value) * decimal.Parse(lbl_Price.Text);
                oldVat = int.Parse(hdfOldQty.Value) * decimal.Parse(lbl_Price.Text) * decimal.Parse(hdfVat.Value) / 100;
                oldAfterAmount = oldBeforeAmount + oldVat;
            }
            else
            {
                oldBeforeAmount = 0;
                oldVat = 0;
                oldAfterAmount = 0;
            }


            TextBox NewQty = (TextBox)sender;

            hdfOldQty.Value = NewQty.Text;
            int suggest_qty = lbl_Suggest_Quantity.Text == "" ? 0 : int.Parse(lbl_Suggest_Quantity.Text);
            if (int.Parse(NewQty.Text) > suggest_qty * 2)
            {
                Show("จำนวนที่สั่งมากกว่าจำนวนแนะนำ2เท่า คุณยืนยันจำนวนที่สั่งนี้หรือไม่");
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),"SAM", "if (!confirm('จำนวนที่สั่งมากกว่าจำนวนแนะนำ2เท่า คุณยืนยันจำนวนที่สั่งนี้หรือไม่')) return false;", true);

            }
            Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");
            if (NewQty.Text != "" && NewQty.Text != "0")
            {

                lbl_Amount.Text = string.Format("{0}", (int.Parse(NewQty.Text) * decimal.Parse(lbl_Price.Text)).ToString("#,##0.##"));
                newVat = int.Parse(NewQty.Text) * decimal.Parse(lbl_Price.Text) * decimal.Parse(hdfVat.Value) / 100;
                newAmt = decimal.Parse(lbl_Amount.Text);
            }
            else
            {

                lbl_Amount.Text = "";
                newVat = 0;
                newAmt = 0;
            }
            //Text = string.Format("{0}", (int.Parse(NewQty.Text) * decimal.Parse(lbl_Price.Text)).ToString("#,##0.##"));
            //decimal newVat = int.Parse(NewQty.Text) * decimal.Parse(lbl_Price.Text) * decimal.Parse(hdfVat.Value)/100;
            if (txtTotal_Amount_before_vat_included.Text == "")
            {
                txtTotal_Amount_before_vat_included.Text = "0.00";
            }
            if (txtVat_amount.Text == "")
            {
                txtVat_amount.Text = "0.00";
            }
            if (txtTotal_amount_after_vat_included.Text == "")
            {
                txtTotal_amount_after_vat_included.Text = "0.00";
            }
            dbo_SalesTargetClass sale_target = dbo_SalesTargetDataClass.GetCurrentTarget(txtCV_Code_from_SAP.Text, DateTime.Now);
            txtTotal_Amount_before_vat_included.Text = string.Format("{0}", (decimal.Parse(txtTotal_Amount_before_vat_included.Text) - oldBeforeAmount + newAmt).ToString("#,##0.#0"));
            txtVat_amount.Text = string.Format("{0}", (decimal.Parse(txtVat_amount.Text) - oldVat + newVat).ToString("#,##0.#0"));
            txtTotal_amount_after_vat_included.Text = string.Format("{0}", (decimal.Parse(txtTotal_Amount_before_vat_included.Text) + decimal.Parse(txtVat_amount.Text)).ToString("#,##0.#0"));
            //TextboxDiff.Text = string.Format("{0}", (sale_target.Sales_Target.Value - sale_target.Actual_Sales.Value - oldBeforeAmount + decimal.Parse(txtTotal_Amount_before_vat_included.Text)).ToString("#,##0.##"));
            TextboxDiff.Text = string.Format("{0}", (decimal.Parse(hdfTargetDiff.Value) - decimal.Parse(txtTotal_Amount_before_vat_included.Text)).ToString("#,##0.#0"));
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }

    protected void txtDate_of_delivery_goods_TextChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        List<dbo_CycleAssignmentClass> item = dbo_CycleAssignmentDataClass.SelectAll();
        dbo_CycleAssignmentClass cycle = item.FirstOrDefault(f => f.CV_Code == user_class.CV_CODE);
        List<dbo_OrderAndDeliveryCycleValueClass> deliverydate = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle.Order_Cycle_ID);

        DateTime tmp_date = DateTime.Parse(txtDate_of_delivery_goods.Text);
        string tmp_date1 = Convert.ToString(tmp_date.DayOfWeek.GetHashCode() + 1);
        string tmp_date2 = Convert.ToString(tmp_date.DayOfWeek.GetHashCode());
        DayOfWeek _dayofweek = tmp_date.DayOfWeek;
        var _dayofweek1 = DayOfWeek.Sunday;
        //deliverydate.Where(f => f.Delivery_Cycle_Date == tmp_date1);

        var _Cycle_Date = "";
        var _Cycle_Hour = "";
        var _Cycle_Minute = "";
        //dbo_OrderAndDeliveryCycleValueClass tem2 = deliverydate.FirstOrDefault(f => f.Delivery_Cycle_Date == tmp_date1);
        bool flag = false;

      
        foreach (var d in deliverydate)
        {
            if (d.Delivery_Cycle_Date == tmp_date1)
            {
                 
                _Cycle_Date = d.Order_Cycle_Date;
                _Cycle_Hour = d.Order_Cycle_Hour;
                _Cycle_Minute = d.Order_Cycle_Minute;
                flag = true;
                break;
            }
           
        }

        
            if (_Cycle_Date == "1")
            {
                _dayofweek1 = DayOfWeek.Sunday;  
            }
            else if (_Cycle_Date == "2")
            {
                _dayofweek1 = DayOfWeek.Monday;
            }
            else if (_Cycle_Date == "3")
            {
                _dayofweek1 = DayOfWeek.Tuesday;
            }
            else if (_Cycle_Date == "4")
            {
                _dayofweek1 = DayOfWeek.Wednesday;
            }
            else if (_Cycle_Date == "5")
            {
                _dayofweek1 = DayOfWeek.Thursday;
            }
            else if (_Cycle_Date == "6")
            {
                _dayofweek1 = DayOfWeek.Friday;
            }
            else if (_Cycle_Date == "7")
            {
                _dayofweek1 = DayOfWeek.Saturday;
            }


        var daysDiff = (7 + (_dayofweek-_dayofweek1)) % 7;


        #region
        //if (_Cycle_Date == "")
        //{

        //    if (tmp_date1 == "1")
        //    {
        //        foreach (var d in deliverydate)
        //        {

        //            if (int.Parse(d.Delivery_Cycle_Date) == int.Parse(tmp_date1) + 6)
        //            {
        //                _Cycle_Date = d.Order_Cycle_Date;
        //                _Cycle_Hour = d.Order_Cycle_Hour;
        //                _Cycle_Minute = d.Order_Cycle_Minute;

        //            }
        //        }

        //    }


        //}
        #endregion

        int day = daysDiff;
        DateTime tem_date_CP_receive = DateTime.Now;

       
        if (flag == true)
        {
            #region old
            //if (tmp_date1 != "1")
            //{
            //    if(int.Parse(tmp_date1) < int.Parse(_Cycle_Date))
            //    {
            //        day = int.Parse(tmp_date1) - int.Parse(_Cycle_Date);
            //    }
            //    else
            //    {
            //        day = int.Parse(tmp_date1) - int.Parse(_Cycle_Date);
            //    }

            //}
            //else
            //{
            //    if (_Cycle_Date == "1")
            //    {
            //        if (int.Parse(tmp_date1) < int.Parse(_Cycle_Date))
            //        {
            //            day = (int.Parse(tmp_date1)) - int.Parse(_Cycle_Date);
            //        }
            //        else
            //        {
            //            day = (int.Parse(tmp_date1)) - int.Parse(_Cycle_Date);
            //        }        
            //    }
            //    else
            //    {
            //        day = (int.Parse(tmp_date1) + 7) - int.Parse(_Cycle_Date);
            //    }

            //}
            //if (day < 0)
            //{
            //    day = day * -1;
            //}
            //if (int.Parse(tmp_date1) < int.Parse(_Cycle_Date))
            //{
            //    tem_date_CP_receive = tem_date_CP_receive.AddDays(-day).AddHours(Int32.Parse(_Cycle_Hour)).AddMinutes(Int32.Parse(_Cycle_Minute));
            //    txtDate_of_CP_receive_transaction.Text = Convert.ToString(tem_date_CP_receive);
            //}
            //else
            //{
            //    tem_date_CP_receive = tmp_date.AddDays(-day).AddHours(Int32.Parse(_Cycle_Hour)).AddMinutes(Int32.Parse(_Cycle_Minute));
            //    txtDate_of_CP_receive_transaction.Text = Convert.ToString(tem_date_CP_receive);
            //}
            #endregion

            tem_date_CP_receive = tmp_date.AddDays(-day).AddHours(Int32.Parse(_Cycle_Hour)).AddMinutes(Int32.Parse(_Cycle_Minute));

            if(tem_date_CP_receive < DateTime.Now)
            {
                Show("ไม่สามารถทำการสั่งสินค้าได้");
                txtDate_of_delivery_goods.Text = "";
            }
            else
            {
                txtDate_of_CP_receive_transaction.Text = Convert.ToString(tem_date_CP_receive);
            }
           
        }
        //else
        //{

        //    Show("ไม่พบรอบสั่งรอบส่งในวันนี้");
        //    //txtDate_of_CP_receive_transaction.Text = Convert.ToString(tem_date_CP_receive.AddDays(-1));
        //}



        logger.Debug("txtDate_of_delivery_goods.Text " + txtDate_of_delivery_goods.Text);
        DateTime order_date = DateTime.Parse(txtDate_of_create_order_or_PO_Date.Text);
        logger.Debug("txtDate_of_create_order_or_PO_Date.Text " + txtDate_of_create_order_or_PO_Date.Text);
        //วันที่ CP รับข้อมูล
        DateTime CP_Receive_date = DateTime.Parse(txtDate_of_CP_receive_transaction.Text);
        //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

        if (DateTime.Compare(DateTime.Now.Date, tmp_date.Date) <= 0 && CP_Receive_date.Date <= tmp_date.Date)
        {

            item = dbo_CycleAssignmentDataClass.SelectAll();
            cycle = item.FirstOrDefault(f => f.CV_Code == user_class.CV_CODE);

            if (cycle == null)
            {
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show("ยังไม่กำหนด รอบสั่งรอบส่ง");
                return;
                // txtDate_of_delivery_goods.Text = string.Empty;
            }

            //เช็คไปแล้วมีรอบสั่งรอบส่งหรือไม่
            deliverydate = dbo_OrderAndDeliveryCycleValueDataClass.Search(cycle.Order_Cycle_ID);


            //DayOfWeek _dayofweek = tmp_date.DayOfWeek;
            _dayofweek = tmp_date.DayOfWeek;


            bool validday = false;
            //var gg = _dayofweek.GetHashCode();
            string deli_day = string.Empty;
            //int day_ = -1;

            string hour_deli = string.Empty;
            foreach (dbo_OrderAndDeliveryCycleValueClass delivery in deliverydate)
            {

                deli_day += " " + delivery.Delivery_Cycle_Date_Name;
                hour_deli = delivery.Order_Cycle_Hour + ":" + delivery.Order_Cycle_Minute;
                switch (delivery.Delivery_Cycle_Date_Name)
                {
                    case "อาทิตย์":
                        validday = (_dayofweek == DayOfWeek.Sunday);
                        //Show (deliverydate.Select(f => f.Order_Cycle_Date == "1").ToString() + deliverydate.Select(f => f.Order_Cycle_Date_Name).ToString());
                        //   day_ = 7;
                        break;
                    case "จันทร์":
                        validday = (_dayofweek == DayOfWeek.Monday);
                        //  day_ = 0;
                        break;
                    case "อังคาร":
                        validday = (_dayofweek == DayOfWeek.Tuesday);
                        //Show(deliverydate.Select(f => f.Order_Cycle_Date == "3").ToString() + deliverydate.Select(f => f.Order_Cycle_Date_Name).ToString());
                        //  day_ = 1;
                        break;
                    case "พุธ":
                        validday = (_dayofweek == DayOfWeek.Wednesday);
                        // day_ = 2;
                        break;
                    case "พฤหัสบดี":
                        validday = (_dayofweek == DayOfWeek.Thursday);
                        //   day_ = 3;
                        break;
                    case "ศุกร์":
                        validday = (_dayofweek == DayOfWeek.Friday);
                        //    day_ = 4;
                        break;
                    case "เสาร์":
                        validday = (_dayofweek == DayOfWeek.Saturday);
                        //    day_ = 5;
                        break;
                }

                if (validday) break;
            }


            dbo_OrderingDataClass.Search("", "", null, null, tmp_date, tmp_date, user_class.CV_CODE);

            string error_message = "วันที่รับสินค้าไม่ตรงรอบการส่ง รอบส่งสินค้าของคุณคือ" + deli_day;


            List<dbo_OrderingClass> chk_deliv = dbo_OrderingDataClass.Search("", "", null, null, tmp_date, tmp_date, user_class.CV_CODE);
            logger.Debug("chk_deliv.Count " + chk_deliv.Count);


            if (chk_deliv.Count > 0)
            {
                string error_msg = string.Empty;
                foreach (dbo_OrderingClass _chk_deliv in chk_deliv)
                {
                    error_msg = string.Format("คุณได้สร้างใบสั่งซื้อ {0} ที่มีวันที่รับสินค้าวันที่ {1:dd/MM/yyyy} แล้ว กรุณาตรวจสอบใบสั่งซื้อ", _chk_deliv.PO_Number, _chk_deliv.Date_of_delivery_goods);
                }

               // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                Show(error_msg);

                txtTotal_Amount_before_vat_included.Text = "0";
                txtVat_amount.Text = "0";
                txtTotal_amount_after_vat_included.Text = "0";
                //txtMonthTarget.Text = "0/0";
                //txtQuarterTarget.Text = "0/0";
                //txtYearTarget.Text = "0/0";
                //TextboxDiff.Text = "0";

                txtDate_of_delivery_goods.Text = string.Empty;
                txtDate_of_delivery_goods.Enabled = true;
               
            }
            else
            {
                if (validday)
                {
                    //txtDate_of_CP_receive_transaction.Text = txtDate_of_delivery_goods.Text + " " + hour_deli;


                    //show_grid(user_class.CV_CODE, string.Empty, tmp_date, tmp_date);
                    _PO_Number = Request.QueryString["PRM"];

                    if (user_class.Role_ID != "03")
                    {
                        if (_PO_Number == null)
                        {
                            show_grid(user_class.CV_CODE, string.Empty, Convert.ToDateTime(txtDate_of_create_order_or_PO_Date.Text), tmp_date);

                        }
                    }
                    else
                    {
                        show_grid(txtCV_Code_from_SAP.Text, string.Empty, Convert.ToDateTime(txtDate_of_create_order_or_PO_Date.Text), tmp_date);
                    }

                    txtTotal_Amount_before_vat_included.Text = "0";
                    txtVat_amount.Text = "0";
                    txtTotal_amount_after_vat_included.Text = "0";
                    //txtMonthTarget.Text = "0/0";
                    //txtQuarterTarget.Text = "0/0";
                    //txtYearTarget.Text = "0/0";
                    //TextboxDiff.Text = "0";

                    bool found = false;

                    int index = 0;
                    do
                    {
                        DateTime d = DateTime.Now.AddDays(index);
                        DayOfWeek _dayofweek_ = d.Date.DayOfWeek;
                        int day_ = -1;
                        foreach (dbo_OrderAndDeliveryCycleValueClass delivery in deliverydate)
                        {
                            switch (d.Date.DayOfWeek)
                            {
                                case DayOfWeek.Sunday:
                                    //   found = (_dayofweek_ == DayOfWeek.Sunday);
                                    day_ = 7;
                                    break;
                                case DayOfWeek.Monday:
                                    //  found = (_dayofweek_ == DayOfWeek.Monday);
                                    day_ = 1;
                                    break;
                                case DayOfWeek.Tuesday:
                                    //   found = (_dayofweek_ == DayOfWeek.Monday);
                                    day_ = 2;
                                    break;
                                case DayOfWeek.Wednesday:
                                    //   found = (_dayofweek_ == DayOfWeek.Wednesday);
                                    day_ = 3;
                                    break;
                                case DayOfWeek.Thursday:
                                    //   found = (_dayofweek_ == DayOfWeek.Thursday);
                                    day_ = 4;
                                    break;
                                case DayOfWeek.Friday:
                                    //  found = (_dayofweek_ == DayOfWeek.Friday);
                                    day_ = 5;
                                    break;
                                case DayOfWeek.Saturday:
                                    // found = (_dayofweek_ == DayOfWeek.Saturday);
                                    day_ = 6;
                                    break;
                            }

                            if (day_.ToString() == delivery.Delivery_Cycle_Date)
                            {



                                //txtDate_of_CP_receive_transaction.Text = DateTime.Now.ToShortDateString() + " " + hour_deli;
                                found = true;
                            }
                        }

                        index++;
                        //Show(_dayofweek_.ToString());
                        //deliverydate
                    } while (!found && index < 7);


                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                }
                else
                {
                    
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

                    Show(error_message);
                    txtDate_of_delivery_goods.Text = string.Empty;
                    txtDate_of_CP_receive_transaction.Text = DateTime.Now.ToString();
                   _PO_Number = Request.QueryString["PRM"];


                    if (user_class.Role_ID != "03")
                    {
                        if (_PO_Number == null)
                        {
                            show_grid(user_class.CV_CODE, string.Empty, order_date, tmp_date);
                        }
                    }
                    else
                    {
                        show_grid(txtCV_Code_from_SAP.Text, string.Empty, order_date, tmp_date);
                    }

                    txtTotal_Amount_before_vat_included.Text = "0";
                    txtVat_amount.Text = "0";
                    txtTotal_amount_after_vat_included.Text = "0";
                    //txtMonthTarget.Text = "0/0";
                    //txtQuarterTarget.Text = "0/0";
                    //txtYearTarget.Text = "0/0";
                    //TextboxDiff.Text = "0";
                }
            }
        }
        else
        {
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("ข้อมูลไม่ถูกต้อง");
            txtDate_of_delivery_goods.Text = string.Empty;
            show_grid(user_class.CV_CODE, string.Empty, order_date, tmp_date);

            txtTotal_Amount_before_vat_included.Text = "0";
            txtVat_amount.Text = "0";
            txtTotal_amount_after_vat_included.Text = "0";
            //txtMonthTarget.Text = "0/0";
            //txtQuarterTarget.Text = "0/0";
            //txtYearTarget.Text = "0/0";
            TextboxDiff.Text = "0";

        }


    }
    #endregion

    #region Methods
    private bool CheckPermission()
    {
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
        dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

        if (clsdbo_Agent.CV_Code != null)
        {
            ViewState["CV_Code"] = clsdbo_Agent.CV_Code;
            return true;
        }
        else
        {
            return false;
        }

    }

    private void show_grid(string CV_Code, string PO_Number, DateTime? pricedate, DateTime? deliverydate)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {

            Session.Remove("GetProduct_Quantity_tab_1");
            List<dbo_ProductClass> item1 = dbo_ProductDataClass.GetProductByProductGroupID("นมสดพาสเจอร์ไรส์", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewOrdering_Tab1.DataSource = item1;
            GridViewOrdering_Tab1.DataBind();

            Session.Remove("GetProduct_Quantity_tab_2");
            List<dbo_ProductClass> item2 = dbo_ProductDataClass.GetProductByProductGroupID("นมเปรี้ยว", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewOrdering_Tab2.DataSource = item2;
            GridViewOrdering_Tab2.DataBind();

            Session.Remove("GetProduct_Quantity_tab_3");
            List<dbo_ProductClass> item3 = dbo_ProductDataClass.GetProductByProductGroupID("โยเกิร์ตเมจิ", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewOrdering_Tab3.DataSource = item3;
            GridViewOrdering_Tab3.DataBind();

            Session.Remove("GetProduct_Quantity_tab_4");
            List<dbo_ProductClass> item4 = dbo_ProductDataClass.GetProductByProductGroupID("นมเปรี้ยวไพเกน", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewOrdering_Tab4.DataSource = item4;
            GridViewOrdering_Tab4.DataBind();

            Session.Remove("GetProduct_Quantity_tab_5");
            List<dbo_ProductClass> item5 = dbo_ProductDataClass.GetProductByProductGroupID("อื่นๆ", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewOrdering_Tab5.DataSource = item5;
            GridViewOrdering_Tab5.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void show_grid_view(string CV_Code, string PO_Number, DateTime? pricedate, DateTime? deliverydate)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {

            Session.Remove("GetProduct_Quantity_tab_1");
            List<dbo_ProductClass> item1 = dbo_ProductDataClass.GetProductByProductGroupID_View("นมสดพาสเจอร์ไรส์", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewOrdering_Tab1.DataSource = item1;
            GridViewOrdering_Tab1.DataBind();

            Session.Remove("GetProduct_Quantity_tab_2");
            List<dbo_ProductClass> item2 = dbo_ProductDataClass.GetProductByProductGroupID_View("นมเปรี้ยว", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewOrdering_Tab2.DataSource = item2;
            GridViewOrdering_Tab2.DataBind();

            Session.Remove("GetProduct_Quantity_tab_3");
            List<dbo_ProductClass> item3 = dbo_ProductDataClass.GetProductByProductGroupID_View("โยเกิร์ตเมจิ", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewOrdering_Tab3.DataSource = item3;
            GridViewOrdering_Tab3.DataBind();

            Session.Remove("GetProduct_Quantity_tab_4");
            List<dbo_ProductClass> item4 = dbo_ProductDataClass.GetProductByProductGroupID_View("นมเปรี้ยวไพเกน", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewOrdering_Tab4.DataSource = item4;
            GridViewOrdering_Tab4.DataBind();

            Session.Remove("GetProduct_Quantity_tab_5");
            List<dbo_ProductClass> item5 = dbo_ProductDataClass.GetProductByProductGroupID_View("อื่นๆ", CV_Code, PO_Number, pricedate, deliverydate);
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewOrdering_Tab5.DataSource = item5;
            GridViewOrdering_Tab5.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        
    }

    private Decimal getAllRow()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        Decimal sum_cal_amount_price = 0;
        Decimal sum_vat = 0;

        try
        {

            foreach (GridViewRow currentRow in GridViewOrdering_Tab1.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");

                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"\d+"))
                    {
                        if (int.Parse(txt.Text) >= 0)
                        {
                            Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");

                            if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                            {
                                amount = int.Parse(txt.Text);
                                Label lblPrice = (Label)currentRow.FindControl("lbl_Price");
                                Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);
                                Decimal cal_amount_price = (amount * Price);

                                lbl_Amount.Text = Decimal.Round(cal_amount_price, 2).ToString("#,##0.##");


                                sum_cal_amount_price += cal_amount_price;

                                Label Vat = (Label)currentRow.FindControl("lbl_Vat");


                                Decimal cal_Vat = (Decimal.Parse(Vat.Text) * cal_amount_price) / 100;


                                sum_vat += cal_Vat;

                            }

                        }
                        else
                        {
                            Show("ไม่สามารถกรอกจำนวนที่สั่งน้อยกว่าศูนย์");
                        }
                    }
                    else
                    {
                        Show("จำนวนที่ระบุไม่ถูกต้อง");
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


            foreach (GridViewRow currentRow in GridViewOrdering_Tab2.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"\d+"))
                    {
                        if (int.Parse(txt.Text) >= 0)
                        {
                            Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");

                            if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                            {
                                amount = int.Parse(txt.Text);
                                Label lblPrice = (Label)currentRow.FindControl("lbl_Price");

                                Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                                Decimal cal_amount_price = (amount * Price);


                                lbl_Amount.Text = Decimal.Round(cal_amount_price, 2).ToString("#,##0.##");


                                sum_cal_amount_price += cal_amount_price;

                                Label Vat = (Label)currentRow.FindControl("lbl_Vat");


                                Decimal cal_Vat = (Decimal.Parse(Vat.Text) * cal_amount_price) / 100;


                                sum_vat += cal_Vat;

                            }

                        }
                        else
                        {
                            Show("ไม่สามารถกรอกจำนวนที่สั่งน้อยกว่าศูนย์");
                        }
                    }
                    else
                    {
                        Show("จำนวนที่ระบุไม่ถูกต้อง");
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


            foreach (GridViewRow currentRow in GridViewOrdering_Tab3.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"\d+"))
                    {
                        if (int.Parse(txt.Text) >= 0)
                        {
                            Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");

                            if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                            {
                                amount = int.Parse(txt.Text);
                                Label lblPrice = (Label)currentRow.FindControl("lbl_Price");

                                Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                                Decimal cal_amount_price = (amount * Price);


                                lbl_Amount.Text = Decimal.Round(cal_amount_price, 2).ToString("#,##0.##");


                                sum_cal_amount_price += cal_amount_price;

                                Label Vat = (Label)currentRow.FindControl("lbl_Vat");


                                Decimal cal_Vat = (Decimal.Parse(Vat.Text) * cal_amount_price) / 100;


                                sum_vat += cal_Vat;

                            }

                        }
                        else
                        {
                            Show("ไม่สามารถกรอกจำนวนที่สั่งน้อยกว่าศูนย์");
                        }
                    }
                    else
                    {
                        Show("จำนวนที่ระบุไม่ถูกต้อง");
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


            foreach (GridViewRow currentRow in GridViewOrdering_Tab4.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"\d+"))
                    {
                        if (int.Parse(txt.Text) >= 0)
                        {
                            Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");

                            if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                            {
                                amount = int.Parse(txt.Text);
                                Label lblPrice = (Label)currentRow.FindControl("lbl_Price");

                                Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                                Decimal cal_amount_price = (amount * Price);


                                lbl_Amount.Text = Decimal.Round(cal_amount_price, 2).ToString("#,##0.##");


                                sum_cal_amount_price += cal_amount_price;

                                Label Vat = (Label)currentRow.FindControl("lbl_Vat");


                                Decimal cal_Vat = (Decimal.Parse(Vat.Text) * cal_amount_price) / 100;


                                sum_vat += cal_Vat;

                            }

                        }
                        else
                        {
                            Show("ไม่สามารถกรอกจำนวนที่สั่งน้อยกว่าศูนย์");
                        }
                    }
                    else
                    {
                        Show("จำนวนที่ระบุไม่ถูกต้อง");
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


            foreach (GridViewRow currentRow in GridViewOrdering_Tab5.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"\d+"))
                    {
                        if (int.Parse(txt.Text) > 0)
                        {
                            Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");

                            if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                            {
                                amount = int.Parse(txt.Text);
                                Label lblPrice = (Label)currentRow.FindControl("lbl_Price");

                                Decimal Price = Decimal.Parse(string.IsNullOrEmpty(lblPrice.Text) ? "0" : lblPrice.Text);

                                Decimal cal_amount_price = (amount * Price);


                                lbl_Amount.Text = Decimal.Round(cal_amount_price, 2).ToString("#,##0.##");


                                sum_cal_amount_price += cal_amount_price;

                                Label Vat = (Label)currentRow.FindControl("lbl_Vat");


                                Decimal cal_Vat = (Decimal.Parse(Vat.Text) * cal_amount_price) / 100;


                                sum_vat += cal_Vat;

                            }

                        }
                        else
                        {
                            Show("ไม่สามารถกรอกจำนวนที่สั่งน้อยกว่าศูนย์");
                        }
                    }
                    else
                    {
                        Show("จำนวนที่ระบุไม่ถูกต้อง");
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
            txtVat_amount.Text = Decimal.Round(sum_vat, 2).ToString("#,##0.##");
            txtTotal_Amount_before_vat_included.Text = Decimal.Round(sum_cal_amount_price, 2).ToString("#,##0.##");
            txtTotal_amount_after_vat_included.Text = (Decimal.Round(sum_vat, 2) + Decimal.Round(sum_cal_amount_price, 2)).ToString("#,##0.##");
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        return sum_cal_amount_price;
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

    private dbo_OrderingClass SetOrderingData()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_OrderingClass ordering = new dbo_OrderingClass();

        try
        {
            ordering.PO_Number = txtPO_Number.Text;
            ordering.Total_Amount_before_vat_included = Decimal.Parse(txtTotal_Amount_before_vat_included.Text);
            ordering.Total_amount_after_vat_included = Decimal.Parse(txtTotal_amount_after_vat_included.Text);
            ordering.Vat_amount = Decimal.Parse(txtVat_amount.Text);
            ordering.User_ID = Request.Cookies["User_ID"].Value;

            ordering.Date_of_create_order_or_PO_Date = DateTime.Now;
            ordering.CV_Code_from_SAP = txtCV_Code_from_SAP.Text;
            ordering.Date_of_delivery_goods = DateTime.Parse(txtDate_of_delivery_goods.Text);
            ordering.Order_Status = ddlOrder_Status.Text;
            ordering.Date_of_CP_receive_transaction = DateTime.Parse(txtDate_of_CP_receive_transaction.Text);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        return ordering;
    }

    private List<dbo_OrderingDetailClass> SetOrderingDetailData()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        List<dbo_OrderingDetailClass> item_detail = new List<dbo_OrderingDetailClass>();
        int index = 1;

        Decimal sum_cal_amount_price = 0;
        Decimal sum_vat = 0;


        try
        {
            foreach (GridViewRow currentRow in GridViewOrdering_Tab1.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    amount = int.Parse(txt.Text);
                    HiddenField lblPrice = (HiddenField)currentRow.FindControl("hdfPrice");

                    Decimal Price = Decimal.Parse(lblPrice.Value);

                    Decimal cal_amount_price = (amount * Price);

                    sum_cal_amount_price += cal_amount_price;


                    Label Vat = (Label)currentRow.FindControl("lbl_Vat");

                    Label lbl_Stock_on_hand_ = (Label)currentRow.FindControl("lbl_Stock_on_hand");
                    Label lbl_Suggest_Quantity_ = (Label)currentRow.FindControl("lbl_Suggest_Quantity");
                    Label Point = (Label)currentRow.FindControl("lbl_Point");

                    Decimal cal_Vat = 0;

                    cal_Vat = Decimal.Parse(String.Format("{0:0.00}", ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)));

                    /*
                    cal_Vat = Math.Round(
                        ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)
                        , 2, MidpointRounding.ToEven);
                    */

                    sum_vat += cal_Vat;


                    Label lbl_ProductID = (Label)currentRow.FindControl("lbl_Product_ID");

                    dbo_OrderingDetailClass ordering_detail = new dbo_OrderingDetailClass();
                    ordering_detail.PO_Number = txtPO_Number.Text;
                    ordering_detail.Ordering_Detail_ID = ordering_detail.PO_Number + index.ToString("00");
                    ordering_detail.Price = Price;
                    ordering_detail.Quantity = short.Parse(amount.ToString());
                    ordering_detail.Product_ID = lbl_ProductID.Text;
                    ordering_detail.Vat = Decimal.Parse(Vat.Text);

                    ordering_detail.Sub_Total = (ordering_detail.Quantity * ordering_detail.Price);

                    ordering_detail.Vat_Amount = cal_Vat;

                    ordering_detail.Total = ordering_detail.Sub_Total + ordering_detail.Vat_Amount;


                    ordering_detail.Suggest_Quantity = short.Parse(string.IsNullOrEmpty(lbl_Suggest_Quantity_.Text) ? "0" : lbl_Suggest_Quantity_.Text);
                    ordering_detail.Stock_on_hand = short.Parse(string.IsNullOrEmpty(lbl_Stock_on_hand_.Text) ? "0" : lbl_Stock_on_hand_.Text);
                    ordering_detail.Point = short.Parse(string.IsNullOrEmpty(Point.Text) ? "0" : Point.Text);


                    index++;
                    item_detail.Add(ordering_detail);
                }

            }




        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        try
        {
            foreach (GridViewRow currentRow in GridViewOrdering_Tab2.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    amount = int.Parse(txt.Text);
                    HiddenField lblPrice = (HiddenField)currentRow.FindControl("hdfPrice");

                    Decimal Price = Decimal.Parse(lblPrice.Value);

                    Decimal cal_amount_price = (amount * Price);

                    sum_cal_amount_price += cal_amount_price;


                    Label Vat = (Label)currentRow.FindControl("lbl_Vat");

                    Label lbl_Stock_on_hand_ = (Label)currentRow.FindControl("lbl_Stock_on_hand");
                    Label lbl_Suggest_Quantity_ = (Label)currentRow.FindControl("lbl_Suggest_Quantity");
                    Label Point = (Label)currentRow.FindControl("lbl_Point");

                    Decimal cal_Vat = 0;
                    cal_Vat = Decimal.Parse(String.Format("{0:0.00}", ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)));

                    /*
                    cal_Vat = Math.Round(
                        ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)
                        , 2, MidpointRounding.ToEven);
                    */
                    sum_vat += cal_Vat;


                    Label lbl_ProductID = (Label)currentRow.FindControl("lbl_Product_ID");

                    dbo_OrderingDetailClass ordering_detail = new dbo_OrderingDetailClass();
                    ordering_detail.PO_Number = txtPO_Number.Text;
                    ordering_detail.Ordering_Detail_ID = ordering_detail.PO_Number + index.ToString("00");
                    ordering_detail.Price = Price;
                    ordering_detail.Quantity = short.Parse(amount.ToString());
                    ordering_detail.Product_ID = lbl_ProductID.Text;
                    ordering_detail.Vat = Decimal.Parse(Vat.Text);

                    ordering_detail.Sub_Total = (ordering_detail.Quantity * ordering_detail.Price);
                    ordering_detail.Vat_Amount = cal_Vat;
                    ordering_detail.Total = ordering_detail.Sub_Total + ordering_detail.Vat_Amount;


                    ordering_detail.Suggest_Quantity = short.Parse(string.IsNullOrEmpty(lbl_Suggest_Quantity_.Text) ? "0" : lbl_Suggest_Quantity_.Text);
                    ordering_detail.Stock_on_hand = short.Parse(string.IsNullOrEmpty(lbl_Stock_on_hand_.Text) ? "0" : lbl_Stock_on_hand_.Text);
                    ordering_detail.Point = short.Parse(string.IsNullOrEmpty(Point.Text) ? "0" : Point.Text);


                    index++;
                    item_detail.Add(ordering_detail);
                }

            }

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {


            foreach (GridViewRow currentRow in GridViewOrdering_Tab3.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    amount = int.Parse(txt.Text);
                    HiddenField lblPrice = (HiddenField)currentRow.FindControl("hdfPrice");

                    Decimal Price = Decimal.Parse(lblPrice.Value);

                    Decimal cal_amount_price = (amount * Price);

                    sum_cal_amount_price += cal_amount_price;


                    Label Vat = (Label)currentRow.FindControl("lbl_Vat");

                    Label lbl_Stock_on_hand_ = (Label)currentRow.FindControl("lbl_Stock_on_hand");
                    Label lbl_Suggest_Quantity_ = (Label)currentRow.FindControl("lbl_Suggest_Quantity");
                    Label Point = (Label)currentRow.FindControl("lbl_Point");

                    Decimal cal_Vat = 0;
                    cal_Vat = Decimal.Parse(String.Format("{0:0.00}", ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)));

                    /*
                    cal_Vat = Math.Round(
                        ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)
                        , 2, MidpointRounding.ToEven);
                    */
                    sum_vat += cal_Vat;


                    Label lbl_ProductID = (Label)currentRow.FindControl("lbl_Product_ID");

                    dbo_OrderingDetailClass ordering_detail = new dbo_OrderingDetailClass();
                    ordering_detail.PO_Number = txtPO_Number.Text;
                    ordering_detail.Ordering_Detail_ID = ordering_detail.PO_Number + index.ToString("00");
                    ordering_detail.Price = Price;
                    ordering_detail.Quantity = short.Parse(amount.ToString());
                    ordering_detail.Product_ID = lbl_ProductID.Text;
                    ordering_detail.Vat = Decimal.Parse(Vat.Text);

                    ordering_detail.Sub_Total = (ordering_detail.Quantity * ordering_detail.Price);
                    ordering_detail.Vat_Amount = cal_Vat;
                    ordering_detail.Total = ordering_detail.Sub_Total + ordering_detail.Vat_Amount;


                    ordering_detail.Suggest_Quantity = short.Parse(string.IsNullOrEmpty(lbl_Suggest_Quantity_.Text) ? "0" : lbl_Suggest_Quantity_.Text);
                    ordering_detail.Stock_on_hand = short.Parse(string.IsNullOrEmpty(lbl_Stock_on_hand_.Text) ? "0" : lbl_Stock_on_hand_.Text);
                    ordering_detail.Point = short.Parse(string.IsNullOrEmpty(Point.Text) ? "0" : Point.Text);


                    index++;
                    item_detail.Add(ordering_detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {

            foreach (GridViewRow currentRow in GridViewOrdering_Tab4.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    amount = int.Parse(txt.Text);
                    HiddenField lblPrice = (HiddenField)currentRow.FindControl("hdfPrice");

                    Decimal Price = Decimal.Parse(lblPrice.Value);

                    Decimal cal_amount_price = (amount * Price);

                    sum_cal_amount_price += cal_amount_price;


                    Label Vat = (Label)currentRow.FindControl("lbl_Vat");

                    Label lbl_Stock_on_hand_ = (Label)currentRow.FindControl("lbl_Stock_on_hand");
                    Label lbl_Suggest_Quantity_ = (Label)currentRow.FindControl("lbl_Suggest_Quantity");
                    Label Point = (Label)currentRow.FindControl("lbl_Point");

                    Decimal cal_Vat = 0;
                    cal_Vat = Decimal.Parse(String.Format("{0:0.00}", ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)));

                    /*
                    cal_Vat = Math.Round(
                        ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)
                        , 2, MidpointRounding.ToEven);
                    */
                    sum_vat += cal_Vat;


                    Label lbl_ProductID = (Label)currentRow.FindControl("lbl_Product_ID");

                    dbo_OrderingDetailClass ordering_detail = new dbo_OrderingDetailClass();
                    ordering_detail.PO_Number = txtPO_Number.Text;
                    ordering_detail.Ordering_Detail_ID = ordering_detail.PO_Number + index.ToString("00");
                    ordering_detail.Price = Price;
                    ordering_detail.Quantity = short.Parse(amount.ToString());
                    ordering_detail.Product_ID = lbl_ProductID.Text;
                    ordering_detail.Vat = Decimal.Parse(Vat.Text);

                    ordering_detail.Sub_Total = (ordering_detail.Quantity * ordering_detail.Price);
                    ordering_detail.Vat_Amount = cal_Vat;
                    ordering_detail.Total = ordering_detail.Sub_Total + ordering_detail.Vat_Amount;


                    ordering_detail.Suggest_Quantity = short.Parse(string.IsNullOrEmpty(lbl_Suggest_Quantity_.Text) ? "0" : lbl_Suggest_Quantity_.Text);
                    ordering_detail.Stock_on_hand = short.Parse(string.IsNullOrEmpty(lbl_Stock_on_hand_.Text) ? "0" : lbl_Stock_on_hand_.Text);
                    ordering_detail.Point = short.Parse(string.IsNullOrEmpty(Point.Text) ? "0" : Point.Text);


                    index++;
                    item_detail.Add(ordering_detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        try
        {


            foreach (GridViewRow currentRow in GridViewOrdering_Tab5.Rows)
            {
                int amount = 0;
                TextBox txt = (TextBox)currentRow.FindControl("txtOrderingAmount");
                Label lbl_Amount = (Label)currentRow.FindControl("lbl_Amount");
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Trim() != "0")
                {
                    amount = int.Parse(txt.Text);
                    HiddenField lblPrice = (HiddenField)currentRow.FindControl("hdfPrice");

                    Decimal Price = Decimal.Parse(lblPrice.Value);

                    Decimal cal_amount_price = (amount * Price);

                    sum_cal_amount_price += cal_amount_price;


                    Label Vat = (Label)currentRow.FindControl("lbl_Vat");

                    Label lbl_Stock_on_hand_ = (Label)currentRow.FindControl("lbl_Stock_on_hand");
                    Label lbl_Suggest_Quantity_ = (Label)currentRow.FindControl("lbl_Suggest_Quantity");
                    Label Point = (Label)currentRow.FindControl("lbl_Point");

                    Decimal cal_Vat = 0;
                    cal_Vat = Decimal.Parse(String.Format("{0:0.00}", ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)));

                    /*
                    cal_Vat = Math.Round(
                        ((Decimal.Parse(Vat.Text) * cal_amount_price) / 100)
                        , 2, MidpointRounding.ToEven);
                    */
                    sum_vat += cal_Vat;


                    Label lbl_ProductID = (Label)currentRow.FindControl("lbl_Product_ID");

                    dbo_OrderingDetailClass ordering_detail = new dbo_OrderingDetailClass();
                    ordering_detail.PO_Number = txtPO_Number.Text;
                    ordering_detail.Ordering_Detail_ID = ordering_detail.PO_Number + index.ToString("00");
                    ordering_detail.Price = Price;
                    ordering_detail.Quantity = short.Parse(amount.ToString());
                    ordering_detail.Product_ID = lbl_ProductID.Text;
                    ordering_detail.Vat = Decimal.Parse(Vat.Text);

                    ordering_detail.Sub_Total = (ordering_detail.Quantity * ordering_detail.Price);
                    ordering_detail.Vat_Amount = cal_Vat;
                    ordering_detail.Total = ordering_detail.Sub_Total + ordering_detail.Vat_Amount;


                    ordering_detail.Suggest_Quantity = short.Parse(string.IsNullOrEmpty(lbl_Suggest_Quantity_.Text) ? "0" : lbl_Suggest_Quantity_.Text);
                    ordering_detail.Stock_on_hand = short.Parse(string.IsNullOrEmpty(lbl_Stock_on_hand_.Text) ? "0" : lbl_Stock_on_hand_.Text);
                    ordering_detail.Point = short.Parse(string.IsNullOrEmpty(Point.Text) ? "0" : Point.Text);


                    index++;
                    item_detail.Add(ordering_detail);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        return item_detail;
    }

    private void GetDetailsDataToForm(string id, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        logger.Debug("id " + id + " ,Mode " + Mode);

        pnlForm.Visible = true;
        pnlGrid.Visible = false;

        txtAgentName.Text = "";
        txtVat_amount.Text = "0";
        txtCV_Code_from_SAP.Text = "";
        txtTotal_Amount_before_vat_included.Text = "0";
        txtTotal_amount_after_vat_included.Text = "0";

        _PO_Number = Request.QueryString["PRM"];


        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

        ddlOrder_Status.Enabled = false;

        if (Mode == "View")
        {
            btnSaveMode.Value = "แก้ไข";
            btnSave.Text = "แก้ไข";

            if (_PO_Number == null)
            {
                butNo.Text = "กลับไปหน้าค้นหา";
            }
            else
            {
                butNo.Text = "กลับไปหน้า ExportPO";
            }

        }
        else if (Mode == "Edit")
        {
            btnSave.Text = "บันทึก";
            btnSaveMode.Value = "แก้ไข";

            butNo.Text = "ยกเลิก";

            ddlOrder_Status.Enabled = true;

            ddlOrder_Status.Items.Remove("ซีพี-เมจิ รับข้อมูลแล้ว");
            ddlOrder_Status.Items.Remove("รับสินค้าแล้ว");

            hfSaveMode.Value = "EDIT";

            if (!string.IsNullOrEmpty(user_class.CV_CODE))
            {
                ddlOrder_Status.Items.Remove("ยกเลิกโดย ซีพี-เมจิ");
            }
            else
            {
                ddlOrder_Status.Items.Remove("ยกเลิกโดยเอเย่นต์");
            }
        }
        else if (string.IsNullOrEmpty(Mode))
        {
            btnSave.Text = "บันทึก";
            btnSaveMode.Value = "บันทึก";
            butNo.Text = "ยกเลิก";
        }


        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                ddlOrder_Status.ClearSelection();
                dbo_OrderingClass item = dbo_OrderingDataClass.Select_Record(id);

                logger.Debug("item.Order_Status " + item.Order_Status);

                if (ddlOrder_Status.Items.FindByText(item.Order_Status) == null)
                {
                    ddlOrder_Status.Items.Add(item.Order_Status);
                }

                if (!string.IsNullOrEmpty(item.Order_Status))
                    ddlOrder_Status.Items.FindByText(item.Order_Status).Selected = true;

                // ddlOrder_Status.Enabled = false;
                _PO_Number = Request.QueryString["PRM"];
                // dbo_OrderingClass item1 = dbo_OrderingDataClass.Select_Record(_PO_Number);


                if (Mode == "View")
                {
                    show_grid_view(user_class.CV_CODE, id, item.Date_of_create_order_or_PO_Date, item.Date_of_delivery_goods);

                }
                else
                {
                    //if(item.Date_of_CP_receive_transaction >= DateTime.Now)
                    //{
                    //    show_grid(user_class.CV_CODE, id, DateTime.Now, item.Date_of_delivery_goods);
                    //}
                    //else
                    //{
                    //    Show("ไม่สามารถทำการแก้ไขข้อมูลการสั่งซื้อได้หลังเวลา ซีพี-เมจิ รับข้อมูลแล้ว");
                    //}


                    if (user_class.Role_ID != "03")
                    {
                        if (_PO_Number == null || _PO_Number == "")
                        {
                            show_grid(user_class.CV_CODE, id, DateTime.Now, item.Date_of_delivery_goods);
                        }
                    }


                }

                txtPO_Number.Text = item.PO_Number;
                txtTotal_amount_after_vat_included.Text = item.Total_amount_after_vat_included.Value.ToString("#,##0.##");
                txtTotal_Amount_before_vat_included.Text = item.Total_Amount_before_vat_included.Value.ToString("#,##0.##");
                string dateTime = item.Date_of_create_order_or_PO_Date.ToString();
                string createddate = Convert.ToDateTime(dateTime).ToString("dd/MM/yyyy");
                txtDate_of_create_order_or_PO_Date.Text = createddate;
                //txtDate_of_create_order_or_PO_Date.Text = string.Format("{0:dd/MM/yyyy}", item.Date_of_create_order_or_PO_Date.Date().ToString());
                txtDate_of_CP_receive_transaction.Text = item.Date_of_CP_receive_transaction.ToString();
                txtDate_of_delivery_goods.Text = item.Date_of_delivery_goods.Value.ToShortDateString();

                txtVat_amount.Text = item.Vat_amount.Value.ToString("#,##0.##");

                txtTotal_amount_after_vat_included.Enabled = false;
                txtTotal_Amount_before_vat_included.Enabled = false;
                txtCV_Code_from_SAP.Enabled = false;
                txtAgentName.Enabled = false;
                txtPO_Number.Enabled = false;

                // ddlOrder_Status.Enabled = false;

                txtVat_amount.Enabled = false;
                txtTotal_amount_after_vat_included.Enabled = false;
                txtTotal_Amount_before_vat_included.Enabled = false;
                txtDate_of_create_order_or_PO_Date.Enabled = false;
                txtDate_of_CP_receive_transaction.Enabled = false;
                txtDate_of_delivery_goods.Enabled = false;


                txtMonthTarget.Enabled = false;
                txtQuarterTarget.Enabled = false;
                txtYearTarget.Enabled = false;

                //  dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                string _CVCode_;
                if (user_class.CV_CODE != "")
                {
                    _CVCode_ = user_class.CV_CODE;
                }
                else
                {
                    _CVCode_ = item.CV_Code_from_SAP;
                }
                //dbo_SalesTargetClass sale_target = dbo_SalesTargetDataClass.GetCurrentTarget(user_class.CV_CODE, item.Date_of_create_order_or_PO_Date);
                dbo_SalesTargetClass sale_target = dbo_SalesTargetDataClass.GetCurrentTarget(_CVCode_, item.Date_of_create_order_or_PO_Date);

                txtMonthTarget.Text = string.Format("{0} / {1}", sale_target.Actual_Sales.Value.ToString("#,##0.00"), sale_target.Sales_Target.Value.ToString("#,##0.00"));
                txtQuarterTarget.Text = string.Format("{0} / {1}", sale_target.Actual_Sales_Quarter.Value.ToString("#,##0.00"), sale_target.Sales_Target_Quarter.Value.ToString("#,##0.00"));
                txtYearTarget.Text = string.Format("{0} / {1}", sale_target.Actual_Sales_Year.Value.ToString("#,##0.00"), sale_target.Sales_Target_Year.Value.ToString("#,##0.00"));
                //TextboxDiff.Text = string.Format("{0}", (sale_target.Sales_Target - (sale_target.Actual_Sales + sale_target.Actual_PO)).Value.ToString("#,##0.00"));
                if (sale_target.Sales_Target > 0)
                {
                    if ((sale_target.Sales_Target - (sale_target.Actual_Sales + sale_target.Actual_PO)) > 0)
                    {
                        TextboxDiff.Text = string.Format("{0}", (sale_target.Sales_Target - (sale_target.Actual_Sales + sale_target.Actual_PO)).Value.ToString("#,##0.00"));
                    }
                    else
                    {
                        TextboxDiff.Text = "0.00";
                    }
                }
                else
                {
                    TextboxDiff.Text = "0.00";
                }
                hdfTargetDiff.Value = TextboxDiff.Text;

                if (user_class.CV_CODE != "")
                {
                    txtCV_Code_from_SAP.Text = user_class.CV_CODE;
                }
                else
                {
                    txtCV_Code_from_SAP.Text = item.CV_Code_from_SAP; //user_class.CV_CODE;
                }

                txtAgentName.Text = dbo_AgentDataClass.Select_Record(item.CV_Code_from_SAP).AgentName;

            }

            else
            {
                _PO_Number = Request.QueryString["PRM"];
                dbo_OrderingClass item = new dbo_OrderingClass();
                if (_PO_Number != null)
                {
                    item = dbo_OrderingDataClass.Select_Record(_PO_Number);
                }


                if (user_class.Role_ID != "03")
                {
                    if (_PO_Number == null)
                    {
                        show_grid(user_class.CV_CODE, string.Empty, DateTime.Now, DateTime.Now);
                    }
                }
                else
                {
                    show_grid(item.CV_Code_from_SAP, string.Empty, DateTime.Now, DateTime.Now);
                }



                if (_PO_Number != null)
                {
                    txtPO_Number.Text = GenerateID.PurchaseOrderNumber(user_class.CV_CODE);
                    txtCV_Code_from_SAP.Text = item.CV_Code_from_SAP;
                }
                else
                {
                    txtPO_Number.Text = GenerateID.PurchaseOrderNumber(user_class.CV_CODE);
                    txtCV_Code_from_SAP.Text = user_class.CV_CODE;
                }

                if (user_class.CV_CODE != "")
                {
                    txtAgentName.Text = dbo_AgentDataClass.Select_Record(user_class.CV_CODE).AgentName;
                }
                else
                {
                    txtAgentName.Text = dbo_AgentDataClass.Select_Record(item.CV_Code_from_SAP).AgentName;
                }
                ddlOrder_Status.Items.FindByText("รอ ซีพี-เมจิ รับข้อมูล").Selected = true;

                txtDate_of_create_order_or_PO_Date.Text = DateTime.Now.ToShortDateString();

                txtAgentName.Enabled = false;
                txtPO_Number.Enabled = false;
                txtVat_amount.Enabled = false;
                ddlOrder_Status.Enabled = false;
                txtCV_Code_from_SAP.Enabled = false;
                txtDate_of_CP_receive_transaction.Enabled = false;
                txtDate_of_create_order_or_PO_Date.Enabled = false;
                txtTotal_amount_after_vat_included.Enabled = false;
                txtTotal_Amount_before_vat_included.Enabled = false;

                txtVat_amount.Text = "0";
                txtTotal_amount_after_vat_included.Text = "0";
                txtTotal_Amount_before_vat_included.Text = "0";
                txtDate_of_CP_receive_transaction.Text = string.Empty;
                // txtDate_of_create_order_or_PO_Date.Text = string.Empty;
                txtDate_of_delivery_goods.Text = string.Empty;

                dbo_SalesTargetClass sale_target = new dbo_SalesTargetClass();

                if (user_class.CV_CODE != "")
                {
                    sale_target = dbo_SalesTargetDataClass.GetCurrentTarget(user_class.CV_CODE, DateTime.Now);
                }
                else
                {
                    sale_target = dbo_SalesTargetDataClass.GetCurrentTarget(item.CV_Code_from_SAP, DateTime.Now);
                }


                txtMonthTarget.Enabled = false;
                txtQuarterTarget.Enabled = false;
                txtYearTarget.Enabled = false;
                TextboxDiff.Enabled = false;


                txtMonthTarget.Text = string.Format("{0} / {1}", sale_target.Actual_Sales.Value.ToString("#,##0.00"), sale_target.Sales_Target.Value.ToString("#,##0.00"));
                txtQuarterTarget.Text = string.Format("{0} / {1}", sale_target.Actual_Sales_Quarter.Value.ToString("#,##0.00"), sale_target.Sales_Target_Quarter.Value.ToString("#,##0.00"));
                txtYearTarget.Text = string.Format("{0} / {1}", sale_target.Actual_Sales_Year.Value.ToString("#,##0.00"), sale_target.Sales_Target_Year.Value.ToString("#,##0.00"));
                if (sale_target.Sales_Target > 0)
                {
                    if ((sale_target.Sales_Target - (sale_target.Actual_Sales + sale_target.Actual_PO)) > 0)
                    {
                        TextboxDiff.Text = string.Format("{0}", (sale_target.Sales_Target - (sale_target.Actual_Sales + sale_target.Actual_PO)).Value.ToString("#,##0.00"));
                    }
                    else
                    {
                        TextboxDiff.Text = "0.00";
                    }
                }
                else
                {
                    TextboxDiff.Text = "0.00";
                }
                hdfTargetDiff.Value = TextboxDiff.Text;
            }


            bool enable = Mode != "View";


            // txtDate_of_delivery_goods.Enabled = false;

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void SearchSubmit()
    {
        try
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);

            if (user_class.Role_ID == "03")
            {
                ButtonAddNew.Visible = false;
            }
            String Order_Status = DropDownListOrderingStatus.SelectedIndex == 0 ? string.Empty : DropDownListOrderingStatus.SelectedValue;

            String PO_Number = txtOderingNumber.Text;
            DateTime? Date_of_create_order_or_PO_Date_start_Date = (!string.IsNullOrEmpty(txtOrderingStartDate.Text) ? DateTime.Parse(txtOrderingStartDate.Text) : (DateTime?)null);
            DateTime? Date_of_create_order_or_PO_Date_end_Date = (!string.IsNullOrEmpty(txtOrderingEndDate.Text) ? DateTime.Parse(txtOrderingEndDate.Text) : (DateTime?)null);


            DateTime? Date_of_delivery_goods_start_date = (!string.IsNullOrEmpty(txtRecievingStartDate.Text) ? DateTime.Parse(txtRecievingStartDate.Text) : (DateTime?)null);
            DateTime? Date_of_delivery_goods_end_date = (!string.IsNullOrEmpty(txtRecievingEndDate.Text) ? DateTime.Parse(txtRecievingEndDate.Text) : (DateTime?)null);

            List<dbo_OrderingClass> OrderingList = dbo_OrderingDataClass.Search(Order_Status, PO_Number, Date_of_create_order_or_PO_Date_start_Date,
                Date_of_create_order_or_PO_Date_end_Date,
                Date_of_delivery_goods_start_date,
                Date_of_delivery_goods_end_date, user_class.CV_CODE
                );

            if (OrderingList.Count > 0)
            {
                GridViewOrdering.DataSource = OrderingList;
                GridViewOrdering.DataBind();

                GridViewOrdering.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewOrdering.Visible = false;
                pnlNoRec.Visible = true;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion

    #region GridView Row DataBound
    protected void GridViewOrdering_Tab1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;

            Label lbl_Amount = e.Row.FindControl("lbl_Amount") as Label;
            HiddenField _lbl_Price = e.Row.FindControl("hdfPrice") as HiddenField;
            HiddenField _lbl_Vat = e.Row.FindControl("hdfVat") as HiddenField;
            Label suggest = e.Row.FindControl("lbl_Suggest_Quantity") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");

        }
    }

    protected void GridViewOrdering_Tab2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;

            Label lbl_Amount = e.Row.FindControl("lbl_Amount") as Label;
            HiddenField _lbl_Price = e.Row.FindControl("hdfPrice") as HiddenField;
            HiddenField _lbl_Vat = e.Row.FindControl("hdfVat") as HiddenField;
            Label suggest = e.Row.FindControl("lbl_Suggest_Quantity") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");

        }
    }

    protected void GridViewOrdering_Tab3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;

            Label lbl_Amount = e.Row.FindControl("lbl_Amount") as Label;
            HiddenField _lbl_Price = e.Row.FindControl("hdfPrice") as HiddenField;
            HiddenField _lbl_Vat = e.Row.FindControl("hdfVat") as HiddenField;
            Label suggest = e.Row.FindControl("lbl_Suggest_Quantity") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");

        }
    }

    protected void GridViewOrdering_Tab4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;

            Label lbl_Amount = e.Row.FindControl("lbl_Amount") as Label;
            HiddenField _lbl_Price = e.Row.FindControl("hdfPrice") as HiddenField;
            HiddenField _lbl_Vat = e.Row.FindControl("hdfVat") as HiddenField;
            Label suggest = e.Row.FindControl("lbl_Suggest_Quantity") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");

        }
    }

    protected void GridViewOrdering_Tab5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            TextBox textBox1 = e.Row.FindControl("txtOrderingAmount") as TextBox;

            Label lbl_Amount = e.Row.FindControl("lbl_Amount") as Label;
            HiddenField _lbl_Price = e.Row.FindControl("hdfPrice") as HiddenField;
            HiddenField _lbl_Vat = e.Row.FindControl("hdfVat") as HiddenField;
            Label suggest = e.Row.FindControl("lbl_Suggest_Quantity") as Label;

            textBox1.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            textBox1.Attributes.Add("onblur", "javascript:return UpdateField(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");
            textBox1.Attributes.Add("onFocus", "javascript:return ClearValue(" + textBox1.ClientID + "," + _lbl_Price.ClientID + "," + lbl_Amount.ClientID + "," + _lbl_Vat.ClientID + "," + suggest.ClientID + ");");

        }
    }
    #endregion

    #region GridView DataBound
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Retrieve the pager row.
            GridViewRow pagerRow = GridViewOrdering.BottomPagerRow;

            // Retrieve the PageDropDownList DropDownList from the bottom pager row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

            // Set the PageIndex property to display that page selected by the user.
            GridViewOrdering.PageIndex = pageList.SelectedIndex;
            btnSearchSubmit_Click(sender, e);

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {

        }
       

    }

    protected void GridViewOrdering_DataBound(object sender, EventArgs e)
    {

        try
        {


            // Retrieve the pager row.
            GridViewRow pagerRow = GridViewOrdering.BottomPagerRow;

            // Retrieve the DropDownList and Label controls from the row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

            if (pageList != null)
            {

                // Create the values for the DropDownList control based on 
                // the  total number of pages required to display the data
                // source.
                for (int i = 0; i < GridViewOrdering.PageCount; i++)
                {

                    // Create a ListItem object to represent a page.
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    // If the ListItem object matches the currently selected
                    // page, flag the ListItem object as being selected. Because
                    // the DropDownList control is recreated each time the pager
                    // row gets created, this will persist the selected item in
                    // the DropDownList control.   
                    if (i == GridViewOrdering.PageIndex)
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
                int currentPage = GridViewOrdering.PageIndex + 1;

                // Update the Label control with the current page information.
                pageLabel.Text = "หน้า " + currentPage.ToString() +
                  " จาก " + GridViewOrdering.PageCount.ToString();

            }

        }
        catch (Exception ex)
        {

        }
       
    }

    protected void GridViewOrdering_1_OnDataBound(object sender, EventArgs e)
    {
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_1"];
        Session.Remove("GetProduct_Quantity_tab_1");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewOrdering_Tab1.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");


                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 9;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtDate_of_delivery_goods.Text))
                    {


                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                        Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                        _lbl_Price.Visible = (hdfPosition.Value == "เจ้าของ" ? true : false);

                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {
                                _lbl_Amount.Style.Add("display", "none");
                            }
                        }

                        if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                        {
                            _Amount.Enabled = false;
                        }
                        else
                        {
                            _Amount.Enabled = true;
                            //Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                        }
                    }
                    else
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        _Amount.Enabled = false;

                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {

                                Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                                Label _lbl_Stock_on_hand = (Label)row.FindControl("lbl_Stock_on_hand");
                                Label _lbl_Suggest_Quantity = (Label)row.FindControl("lbl_Suggest_Quantity");
                                Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");


                                _lbl_Price.Visible = false;

                                _lbl_Amount.Style.Add("display", "none");
                                //_lbl_Stock_on_hand.Visible = false;
                                //_lbl_Suggest_Quantity.Visible = false;
                                //_lbl_Amount.Visible = false;
                            }
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

    protected void GridViewOrdering_2_OnDataBound(object sender, EventArgs e)
    {
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_2"];
        Session.Remove("GetProduct_Quantity_tab_2");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewOrdering_Tab2.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 9;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtDate_of_delivery_goods.Text))
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                        Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                        _lbl_Price.Visible = (hdfPosition.Value == "เจ้าของ" ? true : false);

                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {
                                _lbl_Amount.Style.Add("display", "none");
                            }

                        }

                        if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                        {
                            _Amount.Enabled = false;
                        }
                        else
                        {
                            _Amount.Enabled = true;
                            //Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                        }
                    }
                    else
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        _Amount.Enabled = false;

                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {

                                Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                                Label _lbl_Stock_on_hand = (Label)row.FindControl("lbl_Stock_on_hand");
                                Label _lbl_Suggest_Quantity = (Label)row.FindControl("lbl_Suggest_Quantity");
                                Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");


                                _lbl_Price.Visible = false;
                                _lbl_Amount.Style.Add("display", "none");

                                //_lbl_Stock_on_hand.Visible = false;
                                //_lbl_Suggest_Quantity.Visible = false;
                                // _lbl_Amount.Visible = false;
                            }
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

    protected void GridViewOrdering_3_OnDataBound(object sender, EventArgs e)
    {
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_3"];
        Session.Remove("GetProduct_Quantity_tab_3");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewOrdering_Tab3.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 9;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtDate_of_delivery_goods.Text))
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                        Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                        _lbl_Price.Visible = (hdfPosition.Value == "เจ้าของ" ? true : false);


                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {
                                _lbl_Amount.Style.Add("display", "none");
                            }
                        }


                        if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                        {
                            _Amount.Enabled = false;
                        }
                        else
                        {
                            _Amount.Enabled = true;
                            //Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                            //  _lbl_Amount.Visible = (hdfPosition.Value == "เจ้าของ" ? true : false);
                        }
                    }
                    else
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        _Amount.Enabled = false;
                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {

                                Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                                Label _lbl_Stock_on_hand = (Label)row.FindControl("lbl_Stock_on_hand");
                                Label _lbl_Suggest_Quantity = (Label)row.FindControl("lbl_Suggest_Quantity");
                                Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");


                                _lbl_Price.Visible = false;
                                _lbl_Amount.Style.Add("display", "none");
                                //_lbl_Stock_on_hand.Visible = false;
                                //_lbl_Suggest_Quantity.Visible = false;
                                // _lbl_Amount.Visible = false;
                            }
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

    protected void GridViewOrdering_4_OnDataBound(object sender, EventArgs e)
    {
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_4"];
        Session.Remove("GetProduct_Quantity_tab_4");
        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewOrdering_Tab4.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 9;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtDate_of_delivery_goods.Text))
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                        Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                        _lbl_Price.Visible = (hdfPosition.Value == "เจ้าของ" ? true : false);

                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {
                                _lbl_Amount.Style.Add("display", "none");
                            }
                        }

                        if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                        {
                            _Amount.Enabled = false;
                        }
                        else
                        {
                            _Amount.Enabled = true;
                            //Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                            //   _lbl_Amount.Visible = (hdfPosition.Value == "เจ้าของ" ? true : false);
                        }
                    }
                    else
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        _Amount.Enabled = false;

                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {

                                Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                                Label _lbl_Stock_on_hand = (Label)row.FindControl("lbl_Stock_on_hand");
                                Label _lbl_Suggest_Quantity = (Label)row.FindControl("lbl_Suggest_Quantity");
                                Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");


                                _lbl_Price.Visible = false;
                                _lbl_Amount.Style.Add("display", "none");
                                //_lbl_Stock_on_hand.Visible = false;
                                //_lbl_Suggest_Quantity.Visible = false;
                                //   _lbl_Amount.Visible = false;
                            }
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

    protected void GridViewOrdering_5_OnDataBound(object sender, EventArgs e)
    {
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_5"];
        Session.Remove("GetProduct_Quantity_tab_5");

        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewOrdering_Tab5.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                Label txt = (Label)row.FindControl("lbl_Item");
                txt.Text = listProduct_Quantity[i].Product_Name;

                row.Cells[0].ColumnSpan = 9;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;

                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtDate_of_delivery_goods.Text))
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                        Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                        _lbl_Price.Visible = (hdfPosition.Value == "เจ้าของ" ? true : false);

                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {
                                _lbl_Amount.Style.Add("display", "none");
                            }
                        }

                        if (btnSave.Text == "แก้ไข" || string.IsNullOrEmpty(_lbl_Price.Text))
                        {
                            _Amount.Enabled = false;
                        }
                        else
                        {
                            _Amount.Enabled = true;
                            //Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");
                            //   _lbl_Amount.Visible = (hdfPosition.Value == "เจ้าของ" ? true : false);
                        }
                    }
                    else
                    {
                        TextBox _Amount = (TextBox)row.FindControl("txtOrderingAmount");
                        _Amount.Enabled = false;

                        if (user_class.User_Group_ID != "CP Meiji")
                        {
                            if (hdfPosition.Value != "เจ้าของ")
                            {

                                Label _lbl_Price = (Label)row.FindControl("lbl_Price");
                                Label _lbl_Stock_on_hand = (Label)row.FindControl("lbl_Stock_on_hand");
                                Label _lbl_Suggest_Quantity = (Label)row.FindControl("lbl_Suggest_Quantity");
                                Label _lbl_Amount = (Label)row.FindControl("lbl_Amount");


                                _lbl_Price.Visible = false;
                                _lbl_Amount.Style.Add("display", "none");
                                //_lbl_Stock_on_hand.Visible = false;
                                //_lbl_Suggest_Quantity.Visible = false;
                                //  _lbl_Amount.Visible = false;
                            }
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
    #endregion

    #region GridView Row Command
    protected void GridViewOrdering_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string PO_Number = lnkView.CommandArgument;


           // System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            GetDetailsDataToForm(PO_Number, "View");
        }
        else if (e.CommandName == "_Delete")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string PO_Number = lnkView.CommandArgument;

            dbo_OrderingClass order = new dbo_OrderingClass();

            order = dbo_OrderingDataClass.Select_Record(PO_Number);

            if (order.Order_Status == "รอ ซีพี-เมจิ รับข้อมูล")
            {
                dbo_OrderingDataClass.Delete(PO_Number);

                List<dbo_OrderingDetailClass> detail = dbo_OrderingDetailDataClass.Search(PO_Number);


                foreach (dbo_OrderingDetailClass _detail in detail)
                {
                    dbo_OrderingDetailDataClass.Delete(_detail.Ordering_Detail_ID);
                }

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ลบข้อมูลสำเร็จ");

                SearchSubmit();

            }
            else
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบสถานะใบสั่งซื้ออีกครั้ง");
            }

        }
        else if (e.CommandName == "Print")
        {
            try
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string PO_Number = lnkView.CommandArgument;


                string url = "../Report_From/ViewsReport.aspx?RPT=PO_Number&PRM=" + PO_Number;
                string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }

    }
    #endregion


}