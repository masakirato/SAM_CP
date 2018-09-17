#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
#endregion

public partial class Views_StockOnHandList : System.Web.UI.Page
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

            if (user_class.User_Group_ID != "Agent")
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "history.back();", true);
            }

            pnlForm.Visible = false;
            pnlGrid.Visible = true;

            btnSearchSubmit_Click(sender, e);

        }

        DisableButton();
    }

    protected void btnStartCountStock_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        List<dbo_CountStockClass> count1 = dbo_CountStockDataClass.Search(null, "", "รอการคอนเฟิร์ม", user_class.CV_CODE);
        
        if(count1.Count > 0)
        {
            string _countno = count1.Select(f => f.Count_No).First();
            Show("ไม่สามารถทำการกดนับสต๊อกได้ เนื่องจากมีการนับสต๊อกที่ยัง รอการคอนเฟิร์ม หมายเลขการนับ :"+ _countno +"!");
            btnSearchSubmit_Click(null, null);
        }
       else
        {
            dbo_CountStockClass count = new dbo_CountStockClass();
            count.Count_No = GenerateID.Count_No(user_class.CV_CODE);
            count.Count_Date = DateTime.Now;
            count.Status = "รอการคอนเฟิร์ม";
            count.CV_Code = user_class.CV_CODE;

            dbo_CountStockDataClass.Add(count, user_class.User_ID);

            btnSearchSubmit_Click(null, null);

            btnStartCountStock.Attributes.Add("disabled", "disable");
            btnCancelCountStock.Attributes.Remove("disabled");
        }     
        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        if (btnSave.Text == "แก้ไข")
        {
            GetDetailsDataToForm(txtCount_No.Text, "Edit");

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        else
        {
            if (btnSaveMode.Value == "บันทึก")
            {

                Show("กรุณาตรวจสอบจำนวนสินค้าที่นับก่อนการกดยืนยันทุกครั้ง"+"("+DateTime.Now.ToShortDateString()+")");

                int index = 0;
                int Stock_on_Hand = 0;
                int Count_Quantity = 0;
                Int16? Diff_Quantity = 0;

                try
                {
                    dbo_CountStockDetailDataClass.DeletebyCountNo(txtCount_No.Text);

                }
                catch
                { }

                try
                {
                    foreach (GridViewRow currentRow in GridViewStock_Tab1.Rows)
                    {
                        TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl = (Label)currentRow.FindControl("lbl_Quantity");

                        //if (!string.IsNullOrEmpty(txt.Text) && !string.IsNullOrEmpty(lbl.Text) )
                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                            Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                            Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                            lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? 0 : int.Parse(lbl_Quantity.Text)) - (txt.Text == "" ? 0 : int.Parse(txt.Text))).ToString();
                            TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                            TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                            Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                            index++;

                            dbo_CountStockDetailClass detail = new dbo_CountStockDetailClass();
                            detail.Count_Stock_Detail_ID = txtCount_No.Text + index.ToString("0000");
                            detail.Count_No = txtCount_No.Text;
                            detail.Product_ID = lbl_Product_ID.Text;
                            detail.Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                            detail.Quantity = lbl_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(lbl_Quantity.Text); //Int16.Parse(lbl_Quantity.Text);
                            detail.Remark = txtRemark.Text;
                            detail.Count_Quantity = txtCount_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(txtCount_Quantity.Text);

                            dbo_CountStockDetailDataClass.Add(detail, user_class.User_ID);


                            Stock_on_Hand += int.Parse(detail.Quantity.ToString());
                            Count_Quantity += int.Parse(detail.Count_Quantity.ToString());
                            Diff_Quantity += detail.Diff_Quantity;


                            dbo_StockClass update_stock = dbo_StockDataClass.Select_Record(lbl_Stock_on_Hand_ID_.Text);

                            logger.Debug("1 lbl_Stock_on_Hand_ID_.Text " + lbl_Stock_on_Hand_ID_.Text);
                        }
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }



                try
                {
                    foreach (GridViewRow currentRow in GridViewStock_Tab2.Rows)
                    {
                        TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl = (Label)currentRow.FindControl("lbl_Quantity");

                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                            Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                            Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                            lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? 0 : int.Parse(lbl_Quantity.Text)) - (txt.Text == "" ? 0 : int.Parse(txt.Text))).ToString();
                            TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                            TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                            Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                            index++;

                            dbo_CountStockDetailClass detail = new dbo_CountStockDetailClass();
                            detail.Count_Stock_Detail_ID = txtCount_No.Text + index.ToString("0000");
                            detail.Count_No = txtCount_No.Text;
                            detail.Product_ID = lbl_Product_ID.Text;
                            detail.Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                            detail.Quantity = lbl_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(lbl_Quantity.Text);
                            detail.Remark = txtRemark.Text;
                            detail.Count_Quantity = txtCount_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(txtCount_Quantity.Text);
                            dbo_CountStockDetailDataClass.Add(detail, user_class.User_ID);


                            Stock_on_Hand += int.Parse(detail.Quantity.ToString());
                            Count_Quantity += int.Parse(detail.Count_Quantity.ToString());
                            Diff_Quantity += detail.Diff_Quantity;
                        }
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }


                try
                {
                    foreach (GridViewRow currentRow in GridViewStock_Tab3.Rows)
                    {
                        TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl = (Label)currentRow.FindControl("lbl_Quantity");

                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                            Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                            Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                            lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? 0 : int.Parse(lbl_Quantity.Text)) - (txt.Text == "" ? 0 : int.Parse(txt.Text))).ToString();
                            TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                            TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                            Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                            index++;

                            dbo_CountStockDetailClass detail = new dbo_CountStockDetailClass();
                            detail.Count_Stock_Detail_ID = txtCount_No.Text + index.ToString("0000");
                            detail.Count_No = txtCount_No.Text;
                            detail.Product_ID = lbl_Product_ID.Text;
                            detail.Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                            detail.Quantity = lbl_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(lbl_Quantity.Text);
                            detail.Remark = txtRemark.Text;
                            detail.Count_Quantity = txtCount_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(txtCount_Quantity.Text);
                            dbo_CountStockDetailDataClass.Add(detail, user_class.User_ID);


                            Stock_on_Hand += int.Parse(detail.Quantity.ToString());
                            Count_Quantity += int.Parse(detail.Count_Quantity.ToString());
                            Diff_Quantity += detail.Diff_Quantity;
                        }
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }


                try
                {
                    foreach (GridViewRow currentRow in GridViewStock_Tab4.Rows)
                    {
                        TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl = (Label)currentRow.FindControl("lbl_Quantity");

                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                            Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                            Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                            lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? 0 : int.Parse(lbl_Quantity.Text)) - (txt.Text == "" ? 0 : int.Parse(txt.Text))).ToString();
                            TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                            TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                            Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                            index++;

                            dbo_CountStockDetailClass detail = new dbo_CountStockDetailClass();
                            detail.Count_Stock_Detail_ID = txtCount_No.Text + index.ToString("0000");
                            detail.Count_No = txtCount_No.Text;
                            detail.Product_ID = lbl_Product_ID.Text;
                            detail.Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                            detail.Quantity = lbl_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(lbl_Quantity.Text);
                            detail.Remark = txtRemark.Text;
                            detail.Count_Quantity = txtCount_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(txtCount_Quantity.Text); //Int16.Parse(txtCount_Quantity.Text);
                            dbo_CountStockDetailDataClass.Add(detail, user_class.User_ID);


                            Stock_on_Hand += int.Parse(detail.Quantity.ToString());
                            Count_Quantity += int.Parse(detail.Count_Quantity.ToString());
                            Diff_Quantity += detail.Diff_Quantity;
                        }
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }



                try
                {
                    foreach (GridViewRow currentRow in GridViewStock_Tab5.Rows)
                    {
                        TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl = (Label)currentRow.FindControl("lbl_Quantity");

                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                            Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                            Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");

                            lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? 0 : int.Parse(lbl_Quantity.Text)) - (txt.Text == "" ? 0 : int.Parse(txt.Text))).ToString();
                            TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                            TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                            Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                            index++;

                            dbo_CountStockDetailClass detail = new dbo_CountStockDetailClass();
                            detail.Count_Stock_Detail_ID = txtCount_No.Text + index.ToString("0000");
                            detail.Count_No = txtCount_No.Text;
                            detail.Product_ID = lbl_Product_ID.Text;
                            detail.Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                            detail.Quantity = lbl_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(lbl_Quantity.Text);
                            detail.Remark = txtRemark.Text;
                            detail.Count_Quantity = txtCount_Quantity.Text == "" ? Int16.Parse("0") : Int16.Parse(txtCount_Quantity.Text);
                            dbo_CountStockDetailDataClass.Add(detail, user_class.User_ID);


                            Stock_on_Hand += int.Parse(detail.Quantity.ToString());
                            Count_Quantity += int.Parse(detail.Count_Quantity.ToString());
                            Diff_Quantity += detail.Diff_Quantity;
                        }
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }

                try
                {
                    //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                    //dbo_CountStockClass count = new dbo_CountStockClass();
                    dbo_CountStockClass count = dbo_CountStockDataClass.Select_Record(txtCount_No.Text);
                    //count.Count_No = txtCount_No.Text;
                    //count.Count_Date = DateTime.Parse(txtCount_Date.Text);
                    //count.Status = "รอการคอนเฟิร์ม";
                    count.Stock_on_Hand = Stock_on_Hand;
                    count.Count_Quantity = Count_Quantity;
                    count.Diff_Quantity = Diff_Quantity;
                    //count.CV_Code = user_class.CV_CODE;
                    if (count.Count_Quantity != 0)
                    {
                        dbo_CountStockDataClass.Update(count, user_class.User_ID);
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("บันทึกสำเร็จ!");

                        GetDetailsDataToForm(txtCount_No.Text, "View");

                        // btnSearchSubmit_Click(null, null);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                        Show("กรุณาระบุจำนวนนับก่อนการกดบันทึก!");

                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
        }
    }

    protected void btnSearchSubmit_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        pnlForm.Visible = false;
        pnlGrid.Visible = true;

        try
        {


            DateTime? Count_Date = null;
            if (!string.IsNullOrEmpty(txtSearchCount_Date.Text))
            {
                Count_Date = DateTime.Parse(txtSearchCount_Date.Text);
            }

            string status = ddlSearchStatus.SelectedIndex == 0 ? string.Empty : ddlSearchStatus.SelectedValue;

            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            List<dbo_CountStockClass> count = dbo_CountStockDataClass.Search(Count_Date, txtSearchCount_No.Text, status, user_class.CV_CODE);

            foreach(var d in count)
            {
                if (d.Diff_Quantity < 0)
                {
                    int P_Diff_QTY = Convert.ToInt32(d.Diff_Quantity) * -1;
                    d.Diff_Quantity = Convert.ToInt16(P_Diff_QTY);
                }
                else if (d.Diff_Quantity > 0)
                {
                    int N_Diff_QTY = Convert.ToInt16(d.Diff_Quantity) * -1;
                    d.Diff_Quantity = Convert.ToInt16(N_Diff_QTY);
                }
                else
                {
                    d.Diff_Quantity = d.Diff_Quantity;
                }
               
            }

            if (count.Count > 0)
            {
                GridViewCountStock.DataSource = count;
                GridViewCountStock.DataBind();

                GridViewCountStock.Visible = true;
                pnlNoRec.Visible = false;
            }
            else
            {
                GridViewCountStock.Visible = false;
                pnlNoRec.Visible = true;
            }

            foreach (GridViewRow row in GridViewCountStock.Rows)
            {
                Label Label_Stock_In = (Label)row.FindControl("Label_Stock_In");
                LinkButton Print = (LinkButton)row.FindControl("LinkButton_CV_CODE");
                Label Label_Order_Status = (Label)row.FindControl("Label_Order_Status");
               

                if (Label_Stock_In.Text == "" || Label_Stock_In.Text == "0")
                {
                    Print.Visible = false;

                }
                if (Label_Order_Status.Text == "ยกเลิกการนับ")
                {
                    Print.Visible = false;
                }
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
        txtSearchCount_Date.Text = string.Empty;
        ddlSearchStatus.ClearSelection();
        txtSearchCount_No.Text = string.Empty;

        if (GridViewCountStock.Rows.Count > 0)
        {
            List<dbo_CountStockClass> count = new List<dbo_CountStockClass>();
            GridViewCountStock.DataSource = count;
            GridViewCountStock.DataBind();
        }


        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void butNo_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        btnSearchSubmit_Click(null, null);
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (txtStatus.Text == "รอการคอนเฟิร์ม")
        {
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
            try
            {
                foreach (GridViewRow currentRow in GridViewStock_Tab1.Rows)
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");

                    if (!string.IsNullOrEmpty(txt.Text))
                    {
                        TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                        Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                        Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? int.Parse("0") : int.Parse(lbl_Quantity.Text)) - int.Parse(txt.Text)).ToString();
                        TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                        TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                        Int16? Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                        dbo_StockClass update_stock = dbo_StockDataClass.Select_Record(lbl_Stock_on_Hand_ID_.Text);
                        if (update_stock != null)
                        {
                            if (Diff_Quantity > 0)
                            {
                                update_stock.Stock_Out += Diff_Quantity;
                                update_stock.Stock_End -= Diff_Quantity;
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                            else
                            {
                                update_stock.Stock_In += (Int16)(Diff_Quantity * -1);
                                update_stock.Stock_End += (Int16)(Diff_Quantity * -1);
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                        }
                        else
                        {

                            dbo_StockClass stock = new dbo_StockClass();
                            stock.Stock_on_Hand_ID = GenerateID.Stock_on_Hand_ID(user_class.CV_CODE);
                            stock.CV_Code = user_class.CV_CODE;
                            stock.Stock_Begin = 0;
                            stock.Stock_In = (Int16)(Diff_Quantity * -1);
                            stock.Stock_Out = 0;
                            stock.Stock_End = (Int16)(Diff_Quantity * -1);
                            stock.Product_ID = lbl_Product_ID.Text;
                            stock.Date = DateTime.Now;

                            dbo_StockDataClass.Add(stock, user_class.User_ID);
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
                foreach (GridViewRow currentRow in GridViewStock_Tab2.Rows)
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");

                    if (!string.IsNullOrEmpty(txt.Text))
                    {
                        TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                        Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                        Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? int.Parse("0") : int.Parse(lbl_Quantity.Text)) - int.Parse(txt.Text)).ToString();
                        TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                        TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                        Int16? Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                        dbo_StockClass update_stock = dbo_StockDataClass.Select_Record(lbl_Stock_on_Hand_ID_.Text);
                        if (update_stock != null)
                        {
                            if (Diff_Quantity > 0)
                            {
                                update_stock.Stock_Out += Diff_Quantity;
                                update_stock.Stock_End -= Diff_Quantity;
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                            else
                            {
                                update_stock.Stock_In += (Int16)(Diff_Quantity * -1);
                                update_stock.Stock_End += (Int16)(Diff_Quantity * -1);
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                        }
                        else
                        {

                            dbo_StockClass stock = new dbo_StockClass();
                            stock.Stock_on_Hand_ID = GenerateID.Stock_on_Hand_ID(user_class.CV_CODE);
                            stock.CV_Code = user_class.CV_CODE;
                            stock.Stock_Begin = 0;
                            stock.Stock_In = (Int16)(Diff_Quantity * -1);
                            stock.Stock_Out = 0;
                            stock.Stock_End = (Int16)(Diff_Quantity * -1);
                            stock.Product_ID = lbl_Product_ID.Text;
                            stock.Date = DateTime.Now;

                            dbo_StockDataClass.Add(stock, user_class.User_ID);
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
                foreach (GridViewRow currentRow in GridViewStock_Tab3.Rows)
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");

                    if (!string.IsNullOrEmpty(txt.Text))
                    {
                        TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                        Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                        Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? int.Parse("0") : int.Parse(lbl_Quantity.Text)) - int.Parse(txt.Text)).ToString();
                        TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                        TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                        Int16? Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                        dbo_StockClass update_stock = dbo_StockDataClass.Select_Record(lbl_Stock_on_Hand_ID_.Text);
                        if (update_stock != null)
                        {
                            if (Diff_Quantity > 0)
                            {
                                update_stock.Stock_Out += Diff_Quantity;
                                update_stock.Stock_End -= Diff_Quantity;
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                            else
                            {
                                update_stock.Stock_In += (Int16)(Diff_Quantity * -1);
                                update_stock.Stock_End += (Int16)(Diff_Quantity * -1);
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                        }
                        else
                        {

                            dbo_StockClass stock = new dbo_StockClass();
                            stock.Stock_on_Hand_ID = GenerateID.Stock_on_Hand_ID(user_class.CV_CODE);
                            stock.CV_Code = user_class.CV_CODE;
                            stock.Stock_Begin = 0;
                            stock.Stock_In = (Int16)(Diff_Quantity * -1);
                            stock.Stock_Out = 0;
                            stock.Stock_End = (Int16)(Diff_Quantity * -1);
                            stock.Product_ID = lbl_Product_ID.Text;
                            stock.Date = DateTime.Now;

                            dbo_StockDataClass.Add(stock, user_class.User_ID);
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
                foreach (GridViewRow currentRow in GridViewStock_Tab4.Rows)
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");

                    if (!string.IsNullOrEmpty(txt.Text))
                    {
                        TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                        Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                        Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? int.Parse("0") : int.Parse(lbl_Quantity.Text)) - int.Parse(txt.Text)).ToString();
                        TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                        TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                        Int16? Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                        dbo_StockClass update_stock = dbo_StockDataClass.Select_Record(lbl_Stock_on_Hand_ID_.Text);
                        if (update_stock != null)
                        {
                            if (Diff_Quantity > 0)
                            {
                                update_stock.Stock_Out += Diff_Quantity;
                                update_stock.Stock_End -= Diff_Quantity;
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                            else
                            {
                                update_stock.Stock_In += (Int16)(Diff_Quantity * -1);
                                update_stock.Stock_End += (Int16)(Diff_Quantity * -1);
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                        }
                        else
                        {

                            dbo_StockClass stock = new dbo_StockClass();
                            stock.Stock_on_Hand_ID = GenerateID.Stock_on_Hand_ID(user_class.CV_CODE);
                            stock.CV_Code = user_class.CV_CODE;
                            stock.Stock_Begin = 0;
                            stock.Stock_In = (Int16)(Diff_Quantity * -1);
                            stock.Stock_Out = 0;
                            stock.Stock_End = (Int16)(Diff_Quantity * -1);
                            stock.Product_ID = lbl_Product_ID.Text;
                            stock.Date = DateTime.Now;

                            dbo_StockDataClass.Add(stock, user_class.User_ID);
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
                foreach (GridViewRow currentRow in GridViewStock_Tab5.Rows)
                {
                    TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");

                    if (!string.IsNullOrEmpty(txt.Text))
                    {
                        TextBox lbl_Diff_Quantity = (TextBox)currentRow.FindControl("lbl_Diff_Quantity");
                        Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");
                        Label lbl_Product_ID = (Label)currentRow.FindControl("lbl_Product_ID");
                        lbl_Diff_Quantity.Text = ((lbl_Quantity.Text == "" ? int.Parse("0") : int.Parse(lbl_Quantity.Text)) - int.Parse(txt.Text)).ToString();
                        TextBox txtRemark = (TextBox)currentRow.FindControl("txtRemark");
                        TextBox txtCount_Quantity = (TextBox)currentRow.FindControl("txtCount_Quantity");
                        Label lbl_Stock_on_Hand_ID_ = (Label)currentRow.FindControl("lbl_Stock_on_Hand_ID");

                        Int16? Diff_Quantity = Int16.Parse(lbl_Diff_Quantity.Text);
                        dbo_StockClass update_stock = dbo_StockDataClass.Select_Record(lbl_Stock_on_Hand_ID_.Text);
                        if (update_stock != null)
                        {
                            if (Diff_Quantity > 0)
                            {
                                update_stock.Stock_Out += Diff_Quantity;
                                update_stock.Stock_End -= Diff_Quantity;
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                            else
                            {
                                update_stock.Stock_In += (Int16)(Diff_Quantity * -1);
                                update_stock.Stock_End += (Int16)(Diff_Quantity * -1);
                                dbo_StockDataClass.Update(update_stock, user_class.User_ID);
                            }
                        }
                        else
                        {

                            dbo_StockClass stock = new dbo_StockClass();
                            stock.Stock_on_Hand_ID = GenerateID.Stock_on_Hand_ID(user_class.CV_CODE);
                            stock.CV_Code = user_class.CV_CODE;
                            stock.Stock_Begin = 0;
                            stock.Stock_In = (Int16)(Diff_Quantity * -1);
                            stock.Stock_Out = 0;
                            stock.Stock_End = (Int16)(Diff_Quantity * -1);
                            stock.Product_ID = lbl_Product_ID.Text;
                            stock.Date = DateTime.Now;

                            dbo_StockDataClass.Add(stock, user_class.User_ID);
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }


            //dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);



            try
            {
                dbo_CountStockClass count = dbo_CountStockDataClass.Select_Record(txtCount_No.Text);
                logger.Debug("count.Stock_on_Hand  " + count.Stock_on_Hand + " count.Diff_Quantity " + count.Diff_Quantity);

                count.Status = "คอนเฟิร์มแล้ว";
                if(count.Count_Quantity > 0)
                {
                    Show("ยืนยันการนับสต๊อกสำเร็จ");
                    dbo_CountStockDataClass.Update(count, user_class.User_ID);
                    btnStartCountStock.Attributes.Remove("disabled");
                }
                else
                {
                    Show("เนื่องจากจำนวนนับเท่ากับ 0 ไม่สามารถทำการยืนยันการนับสต๊อกได้");
                }
               
                //btnStartCountStock.Attributes.Add("enabled", "enabled");
                //btnStartCountStock.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            btnSearchSubmit_Click(null, null);

        }
        else if (txtStatus.Text == "คอนเฟิร์มแล้ว")
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("สต๊อกถูกคอนเฟิร์ม ไม่สามารถทำการยืนยันการนับสต๊อกได้");
        }
        else if (txtStatus.Text == "ยกเลิกการนับ")
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("สต๊อกถูกยกเลิกแล้ว ไม่สามารถทำการยืนยันการนับสต๊อกได้");
        }
    }

    protected void btnCancelCountStock_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
        try
        {
            List<dbo_CountStockClass> stock = dbo_CountStockDataClass.Search(null, string.Empty, "รอการคอนเฟิร์ม", user_class.CV_CODE);

            logger.Debug("stock.Count " + stock.Count);

            if (stock.Count < 1)
            {
                Show("มีกการยกเลิกการนับสต๊อกไปแล้ว!");
                btnSearchSubmit_Click(null, null);
                btnStartCountStock.Attributes.Remove("disabled");
                btnCancelCountStock.Attributes.Add("disabled", "disable");

            }
            else
            {

                foreach (dbo_CountStockClass s in stock)
                {
                    s.Status = "ยกเลิกการนับ";
                    dbo_CountStockDataClass.Update(s, user_class.User_ID);
                    btnSearchSubmit_Click(null, null);
                    btnStartCountStock.Attributes.Remove("disabled");
                    btnCancelCountStock.Attributes.Add("disabled", "disable");

                }
            }
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            string url = "../Report_From/ViewsReport.aspx?RPT=StockOnHandList&CV_Code=" + user_class.CV_CODE.Trim() + "&Temp1=Temp1";
            string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void TxtId_TextChanged(object sender, EventArgs e)
    {

        try
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txt = (TextBox)currentRow.FindControl("txtCount_Quantity");
            Label lbl_Diff_Quantity = (Label)currentRow.FindControl("lbl_Diff_Quantity");
            Label lbl_Quantity = (Label)currentRow.FindControl("lbl_Quantity");

            lbl_Diff_Quantity.Text = (int.Parse(lbl_Quantity.Text) - int.Parse(txt.Text)).ToString();


        }
        catch (Exception)
        {

        }
    }
    #endregion

    #region Method
    public void DisableButton()
    {
        string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
        dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

        List<dbo_CountStockClass> count_ = dbo_CountStockDataClass.Search(null, String.Empty, "รอการคอนเฟิร์ม", user_class.CV_CODE);
        dbo_CountStockClass countstatus = count_.FirstOrDefault();
        if (countstatus != null)
        {
            btnStartCountStock.Attributes.Add("disabled", "disable");
            btnCancelCountStock.Attributes.Remove("disabled");
        }
        else
        {
            btnStartCountStock.Attributes.Remove("disabled");
            btnCancelCountStock.Attributes.Add("disabled", "disable");
        }
    }

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

    private void show_grid(string CV_Code, DateTime? pricedate)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            Session.Remove("GetProduct_Quantity_tab_1");
            List<dbo_ProductClass> item1 = dbo_StockDataClass.GetStockByProductGroupID("นมสดพาสเจอร์ไรส์", CV_Code, string.Empty, pricedate);
            Session["GetProduct_Quantity_tab_1"] = item1;
            GridViewStock_Tab1.DataSource = item1;
            GridViewStock_Tab1.DataBind();

            Session.Remove("GetProduct_Quantity_tab_2");
            List<dbo_ProductClass> item2 = dbo_StockDataClass.GetStockByProductGroupID("นมเปรี้ยว", CV_Code, string.Empty, pricedate);
            Session["GetProduct_Quantity_tab_2"] = item2;
            GridViewStock_Tab2.DataSource = item2;
            GridViewStock_Tab2.DataBind();

            Session.Remove("GetProduct_Quantity_tab_3");
            List<dbo_ProductClass> item3 = dbo_StockDataClass.GetStockByProductGroupID("โยเกิร์ตเมจิ", CV_Code, string.Empty, pricedate);
            Session["GetProduct_Quantity_tab_3"] = item3;
            GridViewStock_Tab3.DataSource = item3;
            GridViewStock_Tab3.DataBind();

            Session.Remove("GetProduct_Quantity_tab_4");
            List<dbo_ProductClass> item4 = dbo_StockDataClass.GetStockByProductGroupID("นมเปรี้ยวไพเกน", CV_Code, string.Empty, pricedate);
            Session["GetProduct_Quantity_tab_4"] = item4;
            GridViewStock_Tab4.DataSource = item4;
            GridViewStock_Tab4.DataBind();

            Session.Remove("GetProduct_Quantity_tab_5");
            List<dbo_ProductClass> item5 = dbo_StockDataClass.GetStockByProductGroupID("อื่นๆ", CV_Code, string.Empty, pricedate);
            Session["GetProduct_Quantity_tab_5"] = item5;
            GridViewStock_Tab5.DataSource = item5;
            GridViewStock_Tab5.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    private void GetDetailsDataToForm(string id, string Mode)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        pnlForm.Visible = true;
        pnlGrid.Visible = false;

        try
        {


            if (Mode == "View")
            {
                btnSaveMode.Value = "แก้ไข";
                btnSave.Text = "แก้ไข";
                butNo.Text = "กลับไปหน้าค้นหา";
                btnConfirm.Visible = true;
            }
            else if (Mode == "Edit")
            {
                btnSave.Text = "บันทึก";
                btnSaveMode.Value = "บันทึก";
                butNo.Text = "ยกเลิก";
                btnConfirm.Visible = false;
            }
            else if (string.IsNullOrEmpty(Mode))
            {
                btnSave.Text = "บันทึก";
                btnSaveMode.Value = "บันทึก";
                butNo.Text = "ยกเลิก";
                btnConfirm.Visible = false;
            }

            bool enable = Mode != "View";


            if (!string.IsNullOrEmpty(id))
            {

                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);
                dbo_CountStockClass count = dbo_CountStockDataClass.Select_Record(id);

                txtAgentName.Text = clsdbo_Agent.AgentName;
                txtCount_Date.Text = count.Count_Date.Value.ToShortDateString();
                txtCount_No.Text = count.Count_No;
                txtStatus.Text = count.Status;
                if (txtStatus.Text == "รอการคอนเฟิร์ม" || Mode == "View")
                {

                    try
                    {
                        Session.Remove("GetProduct_Quantity_tab_1");
                        List<dbo_ProductClass> item1 = dbo_StockDataClass.GetStockByProductGroupID("นมสดพาสเจอร์ไรส์", user_class.CV_CODE, id, DateTime.Now);
                        Session["GetProduct_Quantity_tab_1"] = item1;
                        GridViewStock_Tab1.DataSource = item1;
                        GridViewStock_Tab1.DataBind();

                        Session.Remove("GetProduct_Quantity_tab_2");
                        List<dbo_ProductClass> item2 = dbo_StockDataClass.GetStockByProductGroupID("นมเปรี้ยว", user_class.CV_CODE, id, DateTime.Now);
                        Session["GetProduct_Quantity_tab_2"] = item2;
                        GridViewStock_Tab2.DataSource = item2;
                        GridViewStock_Tab2.DataBind();

                        Session.Remove("GetProduct_Quantity_tab_3");
                        List<dbo_ProductClass> item3 = dbo_StockDataClass.GetStockByProductGroupID("โยเกิร์ตเมจิ", user_class.CV_CODE, id, DateTime.Now);
                        Session["GetProduct_Quantity_tab_3"] = item3;
                        GridViewStock_Tab3.DataSource = item3;
                        GridViewStock_Tab3.DataBind();

                        Session.Remove("GetProduct_Quantity_tab_4");
                        List<dbo_ProductClass> item4 = dbo_StockDataClass.GetStockByProductGroupID("นมเปรี้ยวไพเกน", user_class.CV_CODE, id, DateTime.Now);
                        Session["GetProduct_Quantity_tab_4"] = item4;
                        GridViewStock_Tab4.DataSource = item4;
                        GridViewStock_Tab4.DataBind();

                        Session.Remove("GetProduct_Quantity_tab_5");
                        List<dbo_ProductClass> item5 = dbo_StockDataClass.GetStockByProductGroupID("อื่นๆ", user_class.CV_CODE, id, DateTime.Now);
                        Session["GetProduct_Quantity_tab_5"] = item5;
                        GridViewStock_Tab5.DataSource = item5;
                        GridViewStock_Tab5.DataBind();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }

                }
                else if (txtStatus.Text == "คอนเฟิร์มแล้ว")
                {
                    Show("สต๊อกถูกคอนเฟิร์ม ไม่สามารถทำการแก้ไขข้อมูลการนับสต๊อกได้");
                    btnSaveMode.Value = "แก้ไข";
                    btnSave.Text = "แก้ไข";
                    butNo.Text = "กลับไปหน้าค้นหา";
                    btnConfirm.Visible = true;

                }
                else if (txtStatus.Text == "ยกเลิกการนับ")
                {
                    Show("สต๊อกถูกยกเลิกแล้ว ไม่สามารถทำการแก้ไขข้อมูลการนับสต๊อกได้");
                    btnSaveMode.Value = "แก้ไข";
                    btnSave.Text = "แก้ไข";
                    butNo.Text = "กลับไปหน้าค้นหา";
                    btnConfirm.Visible = true;

                }
            }
            else
            {
                dbo_UserClass user_class = dbo_UserDataClass.Select_Record(Request.Cookies["User_ID"].Value);
                dbo_AgentClass clsdbo_Agent = dbo_AgentDataClass.Select_Record(user_class.CV_CODE);

                txtAgentName.Text = clsdbo_Agent.AgentName;
                txtCount_No.Text = GenerateID.Count_No(user_class.CV_CODE);
                txtCount_Date.Text = DateTime.Now.ToShortDateString();
                txtStatus.Text = "รอการคอนเฟิร์ม";


                show_grid(user_class.CV_CODE, DateTime.Now);

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    #endregion   

    #region GridView Events
    protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = GridViewCountStock.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        GridViewCountStock.PageIndex = pageList.SelectedIndex;
        btnSearchSubmit_Click(sender, e);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void GridViewCountStock_DataBound(object sender, EventArgs e)
    {

        //foreach (GridViewRow row in GridViewCountStock.Rows)
        //{
        //    Label Label_Stock_In = (Label)row.FindControl("Label_Stock_In");
        //    LinkButton Print = (LinkButton)row.FindControl("LinkButton_CV_CODE");
        //    Label Label_Stock_Out = (Label)row.FindControl("Label_Stock_Out");
        //    //string Stock_Diff = Label_Stock_Out.ToString();



        //    if (Convert.ToDecimal(Label_Stock_Out.Text) < 0)
        //    {
        //        //Label_Stock_Out.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff6230");
        //        Label_Stock_Out.ForeColor = System.Drawing.Color.Red;
        //    }
        //    else if (Convert.ToDecimal(Label_Stock_Out.Text) > 0)
        //    {
        //        Label_Stock_Out.ForeColor = System.Drawing.Color.Green;
        //    }

        //}

        
            // Retrieve the pager row.
            GridViewRow pagerRow = GridViewCountStock.BottomPagerRow;
        // int Index = int.Parse(e.CommandArgument.ToString());
        
        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

       // Label Label_Stock_In = (Label)pagerRow.FindControl("Label_Stock_In");
       // LinkButton Print = (LinkButton)pagerRow.FindControl("LinkButton_CV_CODE");

        



        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < GridViewCountStock.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == GridViewCountStock.PageIndex)
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
            int currentPage = GridViewCountStock.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + GridViewCountStock.PageCount.ToString();

        }
    }

    protected void GridViewStock_Tab1_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_1"];
            Session.Remove("GetProduct_Quantity_tab_1");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab1.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 8;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {


                    if (btnSaveMode.Value == "แก้ไข")
                    {
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        txtCount_Quantity.Enabled = false;
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtRemark.Enabled = false;

                    }
                    else
                    {
                        //Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                        TextBox lbl_Diff_Quantity = (TextBox)row.FindControl("lbl_Diff_Quantity");
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtCount_Quantity.Text = txtCount_Quantity.Text;
                        //lbl_Diff_Quantity.Text = "0";
                        txtCount_Quantity.Enabled = true;
                        txtRemark.Enabled = true;
   
                    }

                    Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                    if (txtStatus.Text != "รอการคอนเฟิร์ม")
                    {

                        lbl_Quantity.Text = lbl_Quantity.Text;
                    }
                    else
                    {
                        Label lbl_Stock = (Label)row.FindControl("lbl_Stock");
                        lbl_Quantity.Text = lbl_Stock.Text;
                    }



                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewStock_Tab2_OnDataBound(object sender, EventArgs e)
    {
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_2"];
            Session.Remove("GetProduct_Quantity_tab_2");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab2.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 8;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    if (btnSaveMode.Value == "แก้ไข")
                    {
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        txtCount_Quantity.Enabled = false;
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtRemark.Enabled = false;

                    }
                    else
                    {
                        //Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                        TextBox lbl_Diff_Quantity = (TextBox)row.FindControl("lbl_Diff_Quantity");
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtCount_Quantity.Text = txtCount_Quantity.Text;
                        //lbl_Diff_Quantity.Text = "0";
                        txtCount_Quantity.Enabled = true;
                        txtRemark.Enabled = true;

                    }

                    Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                    if (txtStatus.Text != "รอการคอนเฟิร์ม")
                    {

                        lbl_Quantity.Text = lbl_Quantity.Text;
                    }
                    else
                    {
                        Label lbl_Stock = (Label)row.FindControl("lbl_Stock");
                        lbl_Quantity.Text = lbl_Stock.Text;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewStock_Tab3_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_3"];
            Session.Remove("GetProduct_Quantity_tab_3");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab3.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 8;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    if (btnSaveMode.Value == "แก้ไข")
                    {
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        txtCount_Quantity.Enabled = false;
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtRemark.Enabled = false;

                    }
                    else
                    {
                        //Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                        TextBox lbl_Diff_Quantity = (TextBox)row.FindControl("lbl_Diff_Quantity");
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtCount_Quantity.Text = txtCount_Quantity.Text;
                        //lbl_Diff_Quantity.Text = "0";
                        txtCount_Quantity.Enabled = true;
                        txtRemark.Enabled = true;

                    }

                    Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                    if (txtStatus.Text != "รอการคอนเฟิร์ม")
                    {

                        lbl_Quantity.Text = lbl_Quantity.Text;
                    }
                    else
                    {
                        Label lbl_Stock = (Label)row.FindControl("lbl_Stock");
                        lbl_Quantity.Text = lbl_Stock.Text;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewStock_Tab4_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_4"];
            Session.Remove("GetProduct_Quantity_tab_4");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab4.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 8;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    if (btnSaveMode.Value == "แก้ไข")
                    {
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        txtCount_Quantity.Enabled = false;
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtRemark.Enabled = false;

                    }
                    else
                    {
                        //Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                        TextBox lbl_Diff_Quantity = (TextBox)row.FindControl("lbl_Diff_Quantity");
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtCount_Quantity.Text = txtCount_Quantity.Text;
                        //lbl_Diff_Quantity.Text = "0";
                        txtCount_Quantity.Enabled = true;
                        txtRemark.Enabled = true;

                    }

                    Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                    if (txtStatus.Text != "รอการคอนเฟิร์ม")
                    {

                        lbl_Quantity.Text = lbl_Quantity.Text;
                    }
                    else
                    {
                        Label lbl_Stock = (Label)row.FindControl("lbl_Stock");
                        lbl_Quantity.Text = lbl_Stock.Text;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewStock_Tab5_OnDataBound(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        try
        {
            List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_5"];
            Session.Remove("GetProduct_Quantity_tab_5");
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewStock_Tab5.Rows[i];

                if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("lbl_Item");


                    txt.Text = listProduct_Quantity[i].Product_Name;

                    row.Cells[0].ColumnSpan = 8;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;
                    row.Cells[7].Visible = false;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    if (btnSaveMode.Value == "แก้ไข")
                    {
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        txtCount_Quantity.Enabled = false;
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtRemark.Enabled = false;

                    }
                    else
                    {
                        //Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                        TextBox lbl_Diff_Quantity = (TextBox)row.FindControl("lbl_Diff_Quantity");
                        TextBox txtCount_Quantity = (TextBox)row.FindControl("txtCount_Quantity");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        txtCount_Quantity.Text = txtCount_Quantity.Text;
                        //lbl_Diff_Quantity.Text = "0";
                        txtCount_Quantity.Enabled = true;
                        txtRemark.Enabled = true;

                    }

                    Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
                    if (txtStatus.Text != "รอการคอนเฟิร์ม")
                    {

                        lbl_Quantity.Text = lbl_Quantity.Text;
                    }
                    else
                    {
                        Label lbl_Stock = (Label)row.FindControl("lbl_Stock");
                        lbl_Quantity.Text = lbl_Stock.Text;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    protected void GridViewStock_Tab1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        Label lbl_Quantity = e.Row.FindControl("lbl_Quantity") as Label;
        TextBox lbl_Diff_Quantity = (TextBox)e.Row.FindControl("lbl_Diff_Quantity");

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lbl_Diff_Quantity.Text != "" || lbl_Diff_Quantity.Text != null)
                {
                    if (Convert.ToDecimal(lbl_Diff_Quantity.Text) < 0)
                    {
                        lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (Convert.ToDecimal(lbl_Diff_Quantity.Text) > 0)
                    {
                        lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Black;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            logger.Debug(ex);
        }


        if (e.Row.DataItem != null)
        {
           

          
           
            


            TextBox txt = e.Row.FindControl("txtCount_Quantity") as TextBox;
            TextBox diff = e.Row.FindControl("lbl_Diff_Quantity") as TextBox;

            txt.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt.Attributes.Add("onblur", "javascript:return UpdateField(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");
            txt.Attributes.Add("onFocus", "javascript:return ClearValue(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");


        }
    }

    protected void GridViewStock_Tab2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            Label lbl_Quantity = e.Row.FindControl("lbl_Quantity") as Label;

            TextBox lbl_Diff_Quantity = (TextBox)e.Row.FindControl("lbl_Diff_Quantity");

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (lbl_Diff_Quantity.Text != "" || lbl_Diff_Quantity.Text != null)
                    {
                        if (Convert.ToDecimal(lbl_Diff_Quantity.Text) < 0)
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (Convert.ToDecimal(lbl_Diff_Quantity.Text) > 0)
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Black;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
            }




            TextBox txt = e.Row.FindControl("txtCount_Quantity") as TextBox;
            TextBox diff = e.Row.FindControl("lbl_Diff_Quantity") as TextBox;

            txt.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt.Attributes.Add("onblur", "javascript:return UpdateField(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");
            txt.Attributes.Add("onFocus", "javascript:return ClearValue(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");

        }
    }

    protected void GridViewStock_Tab3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            Label lbl_Quantity = e.Row.FindControl("lbl_Quantity") as Label;

            TextBox lbl_Diff_Quantity = (TextBox)e.Row.FindControl("lbl_Diff_Quantity");

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (lbl_Diff_Quantity.Text != "" || lbl_Diff_Quantity.Text != null)
                    {
                        if (Convert.ToDecimal(lbl_Diff_Quantity.Text) < 0)
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (Convert.ToDecimal(lbl_Diff_Quantity.Text) > 0)
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Black;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
            }



            TextBox txt = e.Row.FindControl("txtCount_Quantity") as TextBox;
            TextBox diff = e.Row.FindControl("lbl_Diff_Quantity") as TextBox;

            txt.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt.Attributes.Add("onblur", "javascript:return UpdateField(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");
            txt.Attributes.Add("onFocus", "javascript:return ClearValue(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");

        }
    }

    protected void GridViewStock_Tab4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            Label lbl_Quantity = e.Row.FindControl("lbl_Quantity") as Label;

            TextBox lbl_Diff_Quantity = (TextBox)e.Row.FindControl("lbl_Diff_Quantity");

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (lbl_Diff_Quantity.Text != "" || lbl_Diff_Quantity.Text != null)
                    {
                        if (Convert.ToDecimal(lbl_Diff_Quantity.Text) < 0)
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (Convert.ToDecimal(lbl_Diff_Quantity.Text) > 0)
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lbl_Diff_Quantity.ForeColor = System.Drawing.Color.Black;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
            }

            TextBox txt = e.Row.FindControl("txtCount_Quantity") as TextBox;
            TextBox diff = e.Row.FindControl("lbl_Diff_Quantity") as TextBox;

            txt.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt.Attributes.Add("onblur", "javascript:return UpdateField(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");
            txt.Attributes.Add("onFocus", "javascript:return ClearValue(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");

        }

    }

    protected void GridViewStock_Tab5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            Label lbl_Quantity = e.Row.FindControl("lbl_Quantity") as Label;

            TextBox txt = e.Row.FindControl("txtCount_Quantity") as TextBox;
            TextBox diff = e.Row.FindControl("lbl_Diff_Quantity") as TextBox;

            txt.Attributes.Add("onkeypress", "javascript:return validateFloatKeyPress(this, event);");
            txt.Attributes.Add("onblur", "javascript:return UpdateField(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");
            txt.Attributes.Add("onFocus", "javascript:return ClearValue(" + lbl_Quantity.ClientID + "," + txt.ClientID + "," + diff.ClientID + ");");

        }
    }

    protected void GridViewCountStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

      

        if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string PO_Count_No = lnkView.CommandArgument;


            GetDetailsDataToForm(PO_Count_No, "View");

        }
        else if (e.CommandName == "Print")
        {
            int Index = int.Parse(e.CommandArgument.ToString());
            GridViewRow currentRow = GridViewCountStock.Rows[Index];
            Label Label_Stock_Begin = (Label)currentRow.FindControl("Label_Stock_Begin");
            LinkButton lnkView = (LinkButton)currentRow.FindControl("lnkCount_No");
            Label Label_Stock_In = (Label)currentRow.FindControl("Label_Stock_In");
            //LinkButton Print = (LinkButton)currentRow.FindControl("LinkButton_CV_CODE");
            

            //if (Label_Stock_Begin.Text == "" || Label_Stock_Begin.Text == "0")
            if (Label_Stock_In.Text == "" || Label_Stock_In.Text == "0")
            {
                string script = "alert(\"ไม่พบข้อมูลนับสินค้า\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                //Print.Visible = false;
            }
            else
            {

                string url = "../Report_From/ViewsReport.aspx?RPT=StockOnHandList&PRM=" + lnkView.Text;
                string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

                //string url1 = "../Report/RT_ShowReportStockPDF.aspx?RPT=Clearing_No&PRM=" + lnkView.Text;
                //string s1 = "window.open('" + url1 + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
            }

        }

    }
    #endregion


    protected void GridViewCountStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //Label Label_Stock_Out = (Label)e.Row.FindControl("Label_Stock_Out");

        //string Stock_Diff = Label_Stock_Out.ToString();

        //if (Convert.ToDecimal(Label_Stock_Out.Text) < 0)
        //{
        //    //Label_Stock_Out.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff6230");
        //    Label_Stock_Out.ForeColor = System.Drawing.Color.Red;
        //}
        //else if (Convert.ToDecimal(Label_Stock_Out.Text) > 0)
        //{
        //    Label_Stock_Out.ForeColor = System.Drawing.Color.Green;
        //}

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

               
                Label Label_Stock_Out = (Label)e.Row.FindControl("Label_Stock_Out");

                if(Convert.ToDecimal(Label_Stock_Out.Text) < 0)
                {
                    //Label_Stock_Out.ForeColor = new System.Drawing.Color();
                    Label_Stock_Out.ForeColor = System.Drawing.Color.Red;
                    //Label_Stock_Out.BackColor = System.Drawing.Color.Red;
                    // e.Row.Cells[2].ForeColor = System.Drawing.Color.Green;
                }
                else if(Convert.ToDecimal(Label_Stock_Out.Text) > 0)
                {
                    Label_Stock_Out.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Label_Stock_Out.ForeColor = System.Drawing.Color.Black;
                }

               

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }

    
}