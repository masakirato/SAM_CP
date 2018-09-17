#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.Globalization;
using System.Globalization;
#endregion

public partial class Views_ExportPO : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string ChkOrderingID = string.Empty;
    CultureInfo ThaiCulture = new CultureInfo("th-TH");
    CultureInfo UsaCulture = new CultureInfo("en-US");
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Save_SS"] != null)
            {
                Show("บันทึกสำเร็จ");
                Session.Remove("Save_SS");
            }
            //LoadDDL();
            //LoadPO();
            // ddlAgentName.Attributes.Add("onchange", "myApp.showPleaseWait();");

            SetUpDrowDownList();
            btnExportPO.Visible = false;

            btnSearch_Click(sender, e);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        Int32 i = 0;
        try
        {
            string CV_Code = (ddlAgentName.SelectedIndex == 0 ? string.Empty : ddlAgentName.SelectedValue);
            DateTime? begin_date = string.IsNullOrEmpty(txtStart_Date.Text) ? (DateTime?)null : DateTime.Parse(txtStart_Date.Text);
            DateTime? End_date = string.IsNullOrEmpty(txtEnd_Date.Text) ? (DateTime?)null : DateTime.Parse(txtEnd_Date.Text);
            //string WindowsTime = ddlWindowTime.SelectedIndex == 0 ? string.Empty : ddlWindowTime.Text;
            string WindowsTime = ddlWindowTime.SelectedIndex == 0 ? string.Empty : ddlWindowTime.SelectedItem.Text;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            if (CV_Code == "")
            {
                List<dbo_OrderingClass> orderList = new List<dbo_OrderingClass>();

                for (i = 1; i < ddlAgentName.Items.Count; i++)
                {
                    List<dbo_OrderingClass> Ordering = dbo_OrderingDataClass.ExportPOSearch(ddlAgentName.Items[i].Value, begin_date, End_date, string.Empty, WindowsTime, ddlOrderingStatus.Text, string.Empty);
                    foreach (dbo_OrderingClass _order in Ordering)
                    {
                        orderList.Add(_order);
                    }
                }

                if (orderList.Count > 0)
                {
                    gdvExportPO.DataSource = orderList;
                    gdvExportPO.DataBind();

                    gdvExportPO.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    gdvExportPO.Visible = false;
                    pnlNoRec.Visible = true;
                }

            }
            else
            {
                List<dbo_OrderingClass> Ordering = dbo_OrderingDataClass.ExportPOSearch(CV_Code, begin_date, End_date, string.Empty, WindowsTime, ddlOrderingStatus.Text, string.Empty);

                if (Ordering.Count > 0)
                {
                    gdvExportPO.DataSource = Ordering;
                    gdvExportPO.DataBind();

                    gdvExportPO.Visible = true;
                    pnlNoRec.Visible = false;
                }
                else
                {
                    gdvExportPO.Visible = false;
                    pnlNoRec.Visible = true;
                }
            }

            int count = 0;
            foreach (GridViewRow row in gdvExportPO.Rows)
            {
                Label lnkBInvoice_No = (Label)row.FindControl("lnkBInvoice_No");

                if (lnkBInvoice_No.Text != string.Empty)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                btnExportPO.Visible = true;
            }
            else
            {
                btnExportPO.Visible = false;
            }




            /*
            if (gdvExportPO.Rows.Count > 1)
            {
                btnExportPO.Visible = true;
            }
            else
            {
                btnExportPO.Visible = false;
            }
            */

            // List<dbo_OrderingClass> Ordering = dbo_OrderingDataClass.ExportPOSearch(CV_Code, begin_date, End_date, string.Empty, WindowsTime, ddlOrderingStatus.Text, user_class.Region);

            //gdvExportPO.DataSource = Ordering;
            //gdvExportPO.DataBind();

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        }

    }

    protected void btnExportPO_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            //Encoding isoLatin1Encoding = Encoding.GetEncoding("ISO-8859-1");

            StringBuilder str1 = new StringBuilder();
            string W = string.Format("W");
            str1.Append(W + "\r\n");

            foreach (GridViewRow row in gdvExportPO.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("CheckBoxExportPO");


                if (chk.Checked)
                {
                    // Gen Header (H)
                    Label lnkBInvoice_No_ = (Label)row.FindControl("lnkBInvoice_No");

                    logger.Debug("lnkBInvoice_No_.Text " + lnkBInvoice_No_.Text);

                    //DateTime dateEng = Convert.ToDateTime(myDateTime, _cultureEnInfo);

                    dbo_OrderingClass order = dbo_OrderingDataClass.Select_Record(lnkBInvoice_No_.Text);
                    string RFF1_3_3 = string.Empty.PadRight(2);
                    string NAD10_5_14 = string.Empty.PadRight(12);
                    string BGM15_16_30 = order.PO_Number.PadRight(16);
                    string DTM8_32_39 = order.Date_of_create_order_or_PO_Date.Value.ToString("yyyyMMdd", UsaCulture).PadRight(9);
                    string DTM8_41_48 = order.Date_of_delivery_goods.Value.ToString("yyyyMMdd", UsaCulture).PadRight(9); //string.Empty.PadRight(8);
                    string NAD13_50_62 = string.Empty.PadRight(14);
                    string NAD13_64_76 = order.CV_Code_from_SAP.PadRight(13); //string.Empty.PadRight(13);
                    string other = string.Empty.PadRight(416);


                    string H = string.Format("H{0}{1}{2}{3}{4}{5}{6}{7}", RFF1_3_3, NAD10_5_14, BGM15_16_30, DTM8_32_39, DTM8_41_48, NAD13_50_62, NAD13_64_76, other);
                    str1.Append(H + "\r\n");

                    List<dbo_OrderingDetailClass> detail = dbo_OrderingDetailDataClass.Search(lnkBInvoice_No_.Text);
                    int lin_no = 1;
                    foreach (dbo_OrderingDetailClass d in detail)
                    {
                        // Gen Detail (D)
                        string LIN3_3_5 = lin_no.ToString().PadLeft(3, '0');
                        string LIN6 = string.Empty.PadRight(1);
                        string LIN_EN14_7_20 = d.Product_ID.PadRight(15);
                        string PIA = string.Empty.PadRight(31);
                        string IMD30_53_82 = String.Empty;
                        if (d.Product_Name.Length > 30)
                            IMD30_53_82 = d.Product_Name.Substring(0, 30);//string.Empty.PadRight(35);
                        else
                            IMD30_53_82 = d.Product_Name.Substring(0,d.Product_Name.Length).PadRight(30);//string.Empty.PadRight(35);

                        //if (d.Product_Name.Length > 30)
                        //    IMD30_53_82 = d.Product_Name.Trim().Substring(0, 30);//string.Empty.PadRight(35);
                        //else
                        //    IMD30_53_82 = d.Product_Name.Trim().PadRight(30);

                        string _QTY = string.Empty.PadRight(1);

                        string QTY12_84_95 = d.Quantity.ToString().PadLeft(12, '0');

                        string _PAC = string.Empty.PadRight(1);
                        string PAC = d.Packing_Size.ToString().PadLeft(10, '0');
                        string PAC_ = string.Empty.PadRight(1);
                        string UNIT = d.Unit_of_Item.PadRight(16);
                        string PRI15_124_138 = d.Price.ToString().PadLeft(15, '0');
                        string EN = string.Empty.PadRight(45);

                        string D = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}", "D ", LIN3_3_5, LIN6, LIN_EN14_7_20, PIA, IMD30_53_82, _QTY, QTY12_84_95, _PAC, PAC, PAC_, UNIT, PRI15_124_138, EN);
                        str1.Append(D + "\r\n");
                        lin_no++;


                        string LOC = order.CV_Code_from_SAP.PadRight(13);
                        string QTY = d.Quantity.ToString().PadLeft(12, '0');
                        string L = string.Format("L {0}{1}{2}", LOC, " ", QTY);
                        str1.Append(L + "\r\n");
                    }

                    order.Order_Status = "ซีพี-เมจิ รับข้อมูลแล้ว";
                    dbo_OrderingDataClass.Update(order, HttpContext.Current.Request.Cookies["User_ID"].Value);


                    // Gen Line(L)

                }

            }
            if (str1.Length > 0)
            {
                int last = str1.Length - 1;

                if (last >= 0) { str1.Remove(last, 1); }
            }

            Encoding encoding = new UTF8Encoding(true);
            string dt = string.Format("{0:yyyy-MM-dd_HHmmss}", DateTime.Now);
            string filename = "ExportPO_" + dt + ".txt";
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=zExportFile.txt");
            Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            Response.Charset = encoding.EncodingName;
            Response.ContentType = "application/text";
            //Response.ContentEncoding = new UTF8Encoding(true);
            Response.BinaryWrite(encoding.GetPreamble());

            Response.Output.Write(str1.ToString());
            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        SetUpDrowDownList();
        SetEmptyUserGrid();

        btnExportPO.Visible = false;

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void ddlAgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAgentName.SelectedIndex > 0)
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<dbo_OrderAndDeliveryCycleValueClass> WindowsTime = dbo_OrderAndDeliveryCycleValueDataClass.GetWindowTime(ddlAgentName.SelectedValue.ToString());
            //WindowsTime.Select(f => f.WindowTime_Full).ToList();
            WindowsTime = WindowsTime.GroupBy(t => t.WindowTime)
              .Select(grp => grp.First())
              .ToList();

            //WindowsTime.Insert(0, new dbo_OrderAndDeliveryCycleValueClass { WindowTime_Full = "==ระบุ==", OrderAndDeliveryCycleValue_ID = 0 });
            WindowsTime.Insert(0, new dbo_OrderAndDeliveryCycleValueClass { WindowTime = "==ระบุ==", Order_Cycle_ID = string.Empty });
            ddlWindowTime.DataSource = WindowsTime;
            ddlWindowTime.DataBind();
        }
        else
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(
               string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
               string.Empty, "ดำเนินธุรกิจอยู่", string.Empty);

            string region = user_class.Region;

            string[] regions = region.Split(',');

            List<dbo_AgentClass> cv_code_ = new List<dbo_AgentClass>();
            //cv_code_.Insert(0, new dbo_AgentClass { AgentName = "==ระบุ==", CV_Code = string.Empty });

            foreach (string in_region in regions)
            {
                List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => f.Location_Region == in_region).Select(f => f));
                foreach (dbo_AgentClass _cv in cv_code2)
                {
                    if (_cv.Status == true)
                    {
                        cv_code_.Add(_cv);
                    }
                }
            }
            List<dbo_OrderAndDeliveryCycleValueClass> WindowsTime2 = new List<dbo_OrderAndDeliveryCycleValueClass>();
            foreach (dbo_AgentClass _cv in cv_code_)
            {
                List<dbo_OrderAndDeliveryCycleValueClass> WindowsTime_ = dbo_OrderAndDeliveryCycleValueDataClass.GetWindowTime(_cv.CV_Code);
                foreach (dbo_OrderAndDeliveryCycleValueClass wt in WindowsTime_)
                    WindowsTime2.Add(wt);

            }
            WindowsTime2 = WindowsTime2.GroupBy(t => t.WindowTime)
              .Select(grp => grp.First())
              .ToList();

            WindowsTime2.Insert(0, new dbo_OrderAndDeliveryCycleValueClass { WindowTime = "==ระบุ==", Order_Cycle_ID = string.Empty });
            ddlWindowTime.DataSource = WindowsTime2;
            ddlWindowTime.DataBind();
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);

    }
    #endregion

    #region Medthods
    private void SetUpDrowDownList()
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);


            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, "ดำเนินธุรกิจอยู่", string.Empty);

            if (user_class.User_Group_ID == "CP Meiji")
            {

                #region

                //List<dbo_AgentClass> agent_ = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));
                //if (agent_.Count != 0)
                //{
                //    logger.Info("user_class.Region " + user_class.Region);
                //    agent_.Insert(0, new dbo_AgentClass { AgentName = "==ระบุ==", CV_AgentName = "==ระบุ==", CV_Code = string.Empty });
                //    ddlAgentName.DataSource = agent_;
                //    ddlAgentName.DataBind();
                //}
                //else
                //{
                #endregion
                List<dbo_AgentClass> cv_code1 = new List<dbo_AgentClass>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).Select(f => f));


                if (cv_code1.Count != 0)
                {
                    ddlAgentName.DataSource = cv_code1;
                    ddlAgentName.DataBind();
                    ddlAgentName.Items.Insert(0,"==ระบุ==");
                }
                else

                {
                    string region = user_class.Region;

                    string[] regions = region.Split(',');

                    List<dbo_AgentClass> cv_code_ = new List<dbo_AgentClass>();
                    cv_code_.Insert(0, new dbo_AgentClass { AgentName = "==ระบุ==", CV_AgentName = "==ระบุ==", CV_Code = string.Empty });

                    foreach (string in_region in regions)
                    {
                        List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => f.Location_Region == in_region).Select(f => f));
                        foreach (dbo_AgentClass _cv in cv_code2)
                        {
                            cv_code_.Add(_cv);
                        }

                    }
                    ddlAgentName.DataSource = cv_code_;
                    ddlAgentName.DataBind();
                }

               
                //}
            }
            #region
            // agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim() || f.APV_ID == User_ID.Trim()).ToList();

            /*agent = agent.Where(f => f.Invoice_Region == user_class.Region).ToList();

            logger.Info("user_class.Region " + user_class.Region);
            agent.Insert(0, new dbo_AgentClass { AgentName = "==ระบุ==", CV_Code = string.Empty });
            ddlAgentName.DataSource = agent;
            ddlAgentName.DataBind();*/
            #endregion
            if (ddlAgentName.SelectedIndex > 0)
            {
                List<dbo_OrderAndDeliveryCycleValueClass> WindowsTime = dbo_OrderAndDeliveryCycleValueDataClass.GetWindowTime(ddlAgentName.SelectedValue.ToString());
                WindowsTime = WindowsTime.GroupBy(t => t.WindowTime)
                  .Select(grp => grp.First())
                  .ToList();

                WindowsTime.Insert(0, new dbo_OrderAndDeliveryCycleValueClass { WindowTime = "==ระบุ==", Order_Cycle_ID = string.Empty });
                ddlWindowTime.DataSource = WindowsTime;
                ddlWindowTime.DataBind();
            }
            else
            {
                string region = user_class.Region;

                string[] regions = region.Split(',');

                List<dbo_AgentClass> cv_code_ = new List<dbo_AgentClass>();
                //cv_code_.Insert(0, new dbo_AgentClass { AgentName = "==ระบุ==", CV_Code = string.Empty });

                foreach (string in_region in regions)
                {
                    List<dbo_AgentClass> cv_code2 = new List<dbo_AgentClass>(agent.Where(f => f.Location_Region == in_region).Select(f => f));
                    foreach (dbo_AgentClass _cv in cv_code2)
                    {
                        if (_cv.Status == true)
                        {
                            cv_code_.Add(_cv);
                        }
                    }
                }
                List<dbo_OrderAndDeliveryCycleValueClass> WindowsTime2 = new List<dbo_OrderAndDeliveryCycleValueClass>();
                foreach (dbo_AgentClass _cv in cv_code_)
                {
                    List<dbo_OrderAndDeliveryCycleValueClass> WindowsTime_ = dbo_OrderAndDeliveryCycleValueDataClass.GetWindowTime(_cv.CV_Code);
                    foreach (dbo_OrderAndDeliveryCycleValueClass wt in WindowsTime_)
                        WindowsTime2.Add(wt);

                }
                WindowsTime2 = WindowsTime2.GroupBy(t => t.WindowTime)
                  .Select(grp => grp.First())
                  .ToList();

                WindowsTime2.Insert(0, new dbo_OrderAndDeliveryCycleValueClass { WindowTime = "==ระบุ==", Order_Cycle_ID = string.Empty });
                ddlWindowTime.DataSource = WindowsTime2;
                ddlWindowTime.DataBind();
            }


            //txtStart_Date.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //txtEnd_Date.Text = DateTime.Today.ToString("dd/MM/yyyy");


            txtStart_Date.Text = DateTime.Now.ToShortDateString();
            txtEnd_Date.Text = DateTime.Now.ToShortDateString();


            //if (user_class.Role_ID == "System Administrator" || user_class.Role_ID == "CSC")
            if (user_class.Role_ID == "01" || user_class.Role_ID == "03")
            {
                btnExportPO.Enabled = true;
            }
            else
            {
                btnExportPO.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            // ddlUserSP.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }

    private static string manageFormatBracket(string strText)
    {
        string format = "(";
        string[] strTextArr = strText.Split(',');
        for (int i = 0; i < strTextArr.Count(); i++)
        {
            format += "'" + strTextArr[i] + "',";
        }

        format = format.Remove(format.Length - 1, 1);
        format += ")";

        return format;
    }

    private void SetEmptyUserGrid()
    {
        List<dbo_OrderingClass> dt = new List<dbo_OrderingClass>();

        gdvExportPO.DataSource = dt;
        gdvExportPO.DataBind();

        ddlOrderingStatus.ClearSelection();
    }
    #endregion

    protected void gdvExportPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "_Cancel")
        {
            try
            {

                LinkButton lnkView = (LinkButton)e.CommandSource;
                string PO_Number = lnkView.CommandArgument;

                if (PO_Number != "")
                {

                }
                else
                {
                    Show("ไม่สามารถทำการยกเลิกได้เนื่องจากไม่มีเลขที่ใบสั่งซื้อ");
                }

                //string url = "../Report_From/ViewsReport.aspx?RPT=PO_Number&PRM=" + PO_Number;
                //string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }
        else if (e.CommandName == "View")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string PO_Number = lnkView.CommandArgument;

            //string url = "../Views/OrderingList.aspx?RPT=PO_Number&PRM=" + PO_Number;
            ////string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=768,left=100,top=100,resizable=yes');";
            //string s = "window.open('" + url + "', '_self', '');";

            ////ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", s, true);
            Response.Redirect("../Views/OrderingList.aspx?RPT=PO_Number&PRM=" + PO_Number);
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
        catch (Exception)
        {

        }
        //}
    }
}