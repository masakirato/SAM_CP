#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
#endregion

public partial class Home : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);

            if (user_class.ShowDashboard == "ไม่แสดง")
            {
                dashboardDiv.Visible = false;
            }
            else
            {
                dashboardDiv.Visible = true;
            }

            GenerateNews("เอเยนต์", user_class.AgentName);

            #region Comment
            //AgentInfo(user_class.CV_CODE);

            /*
            string User_ID1 = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class1 = dbo_UserDataClass.Select_Record(User_ID);                     

            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            logger.Debug("agent.Count " + agent.Count);


            List<dbo_UserClass> users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
           , string.Empty, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty);

            logger.Debug("users.Count " + users.Count);


            List<dbo_CustomerClass> customer = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            logger.Debug("customer.Count " + customer.Count);


            //หาจำนวนพนักงาน

            if (user_class1.User_Group_ID == "CP Meiji")
            {
                List<string> item = new List<string>(agent.Where(f => f.DM_ID == User_ID1 || f.GM_ID == User_ID1.Trim() || f.SD_ID == User_ID1.Trim()
                    || f.SM_ID == User_ID1.Trim() || f.APV_ID == User_ID1.Trim()).Select(f => f.CV_Code.Trim()));


                logger.Debug("item.Count " + item.Count);

                if (item.Count != 0)
                {
                    List<dbo_UserClass> user_tmp = new List<dbo_UserClass>();
                    int user_tmp1 = 0;
                    int user_SP = 0;
                    int user_SPOut = 0;
                    int Cust_tmp = 0;


                    foreach (string agent_tmp in item)
                    {
                        logger.Debug("agent_tmp " + agent_tmp);

                        users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                   , string.Empty, string.Empty, string.Empty, agent_tmp.Trim(), null, string.Empty, string.Empty);

                        user_tmp1 += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม").Count();

                        user_SP += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" && (f.Join_Date.HasValue ? f.Join_Date.Value.Month.ToString() : null) == DateTime.Now.Month.ToString() && (f.Join_Date.HasValue ? f.Join_Date.Value.Year.ToString() : null) == DateTime.Now.Year.ToString()).Count();

                        user_SPOut += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "In active" && f.Position == "สาวส่งนม" && (f.Resign_Date.HasValue ? f.Resign_Date.Value.Month.ToString() : null) == DateTime.Now.Month.ToString() && (f.Resign_Date.HasValue ? f.Resign_Date.Value.Year.ToString() : null) == DateTime.Now.Year.ToString()).Count();

                        customer = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, agent_tmp);

                        Cust_tmp += customer.Where(f => f.Active_status == "True" && f.CV_Code == agent_tmp.Trim()).Count();


                    }

                    lbl_Employee.Text = user_tmp1.ToString();
                    lbl_Customer.Text = Cust_tmp.ToString();
                    lbl_SPNew.Text = user_SP.ToString();
                    lbl_SPOut.Text = user_SPOut.ToString();
                }
                else
                {
                    int user_tmp1 = 0;
                    int user_SP = 0;
                    int user_SPOut = 0;
                    int Cust_tmp = 0;

                    string region = user_class1.Region;

                    string[] regions = region.Split(',');

                    foreach (string in_region in regions)
                    {
                        List<string> item3 = new List<string>(agent.Where(f => f.Location_Region == in_region).Select(f => f.CV_Code.Trim()));

                        logger.Debug("in_region " + in_region + " item3.Count " + item3.Count);
                        foreach (string agent_tmp in item3)
                        {
                            users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
                       , string.Empty, string.Empty, string.Empty, agent_tmp.Trim(), null, string.Empty, string.Empty);

                            user_tmp1 += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม").Count();

                            user_SP += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" && (f.Join_Date.HasValue ? f.Join_Date.Value.Month.ToString() : null) == DateTime.Now.Month.ToString() && (f.Join_Date.HasValue ? f.Join_Date.Value.Year.ToString() : null) == DateTime.Now.Year.ToString()).Count();

                            user_SPOut += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "In active" && f.Position == "สาวส่งนม" && (f.Resign_Date.HasValue ? f.Resign_Date.Value.Month.ToString() : null) == DateTime.Now.Month.ToString() && (f.Resign_Date.HasValue ? f.Resign_Date.Value.Year.ToString() : null) == DateTime.Now.Year.ToString()).Count();


                            customer = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                            Cust_tmp += customer.Where(f => f.Active_status == "True" && f.CV_Code == agent_tmp.Trim()).Count();
                        }
                    }

                    lbl_Employee.Text = user_tmp1.ToString();
                    lbl_Customer.Text = Cust_tmp.ToString();
                    lbl_SPNew.Text = user_SP.ToString();
                    lbl_SPOut.Text = user_SPOut.ToString();
                }
            }
            else
            {
                //List<string> item = new List<string>(agent.Where(f => f.DM_ID == User_ID1 || f.GM_ID == User_ID1.Trim() || f.SD_ID == User_ID1.Trim() || f.SM_ID == User_ID1.Trim() || f.APV_ID == User_ID1.Trim()).Select(f => f.CV_Code.Trim() == user_class1.CV_CODE.Trim()));
                List<string> item = new List<string>(agent.Where(f => f.DM_ID == User_ID1 || f.GM_ID == User_ID1.Trim() || f.SD_ID == User_ID1.Trim() || f.SM_ID == User_ID1.Trim() || f.APV_ID == User_ID1.Trim() || f.CV_Code == user_class1.CV_CODE.Trim()).Select(f => f.CV_Code.Trim()));


                List<dbo_UserClass> user_tmp = new List<dbo_UserClass>();
                int user_tmp1 = 0;
                int user_SP = 0;
                int user_SPOut = 0;
                int Cust_tmp = 0;
                foreach (string agent_tmp in item)
                {
                    users = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty
               , string.Empty, string.Empty, string.Empty, agent_tmp.Trim(), null, string.Empty, string.Empty);

                    user_tmp1 += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม").Count();

                    user_SP += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "Active" && f.Position == "สาวส่งนม" && (f.Join_Date.HasValue ? f.Join_Date.Value.Month.ToString() : null) == DateTime.Now.Month.ToString() && (f.Join_Date.HasValue ? f.Join_Date.Value.Year.ToString() : null) == DateTime.Now.Year.ToString()).Count();

                    user_SPOut += users.Where(f => f.User_Group_ID == "Agent" && f.Status == "In active" && f.Position == "สาวส่งนม" && (f.Resign_Date.HasValue ? f.Resign_Date.Value.Month.ToString() : null) == DateTime.Now.Month.ToString() && (f.Resign_Date.HasValue ? f.Resign_Date.Value.Year.ToString() : null) == DateTime.Now.Year.ToString()).Count();


                    //List<string> item1 = new List<string>(users.Select(f => f.User_ID));
                    //foreach (string Cust_tmp1 in item1)
                    //{
                    customer = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    Cust_tmp += customer.Where(f => f.Active_status == "True" && f.CV_Code == agent_tmp.Trim()).Count();
                    //}

                }

                lbl_Employee.Text = user_tmp1.ToString();
                lbl_Customer.Text = Cust_tmp.ToString();
                lbl_SPNew.Text = user_SP.ToString();
                lbl_SPOut.Text = user_SPOut.ToString();
            }
             * */
            #endregion

            Count_SP count_sp = dbo_UserDataClass.Get_Count_SP(User_ID);
            lbl_Employee.Text = count_sp.sp_total.ToString("#,##0.##");
            lbl_Customer.Text = count_sp.sp_customer_total.ToString("#,##0.##");
            lbl_SPNew.Text = count_sp.sp_this_month.ToString("#,##0.##");
            lbl_SPOut.Text = count_sp.sp_resign.ToString("#,##0.##");


            //Dashboard
            JSONObject json = new JSONObject();
            json.type = "bar";
            json.data = new RootObject();
            json.options = new options();

            //json.axisY = new axisY();
            //json.axisY.labelFontFamily = "Prompt, sans-serif";
            //json.axisY.labelFontSize = 20;
            //json.axisX = new axisX();
            //json.axisX.labelFontFamily = "Prompt, sans-serif";
            //json.axisX.labelFontSize = 20;

            json.defaults = new defaults();
            json.defaults.defaultOptions.global.defaultFontFamily = "Prompt";//"Tahoma";

            json.options.title = new title();
            json.options.title.display = true;
            json.options.title.text = "ยอดสั่งซื้อประจำปี " + (DateTime.Now.Year + 543).ToString();
            json.options.title.fontFamily = "Prompt, sans-serif";
            json.options.title.fontSize = 20;
           
            json.options.legend = new legend();
            json.options.legend.display = true ;
            json.options.legend.labels.fontFamily = "Prompt, sans-serif";
            json.options.legend.labels.fontSize = 14;

            json.options.scales = new scales();
            xAxes xitem = new xAxes();
            xitem.ticks.fontFamily = "Prompt, sans-serif";
            json.options.scales.xAxes.Add(xitem);

            yAxes yitem = new yAxes();
            yitem.ticks.fontFamily = "Prompt, sans-serif";
            json.options.scales.yAxes.Add(yitem);
            //json.options.scales.yAxes.ticks.fontFamily = "Prompt, sans-serif";

            //json.options.legend.fontFamily = "Prompt, sans-serif";
            //json.options.legend.fontSize = 20;

            //json.xAxes = new xAxes();
            //json.xAxes.ticks.fontFamily = "Prompt, sans-serif";

            //json.legend = new legend();
            //json.legend.fontSize = 30;

            //json.globals = new globals();
            //json.globals.fontFamily = "Prompt, sans-serif";

            #region DB Code
            int i = 0;
            decimal targetm1 = 0;
            decimal targetm2 = 0;
            decimal targetm3 = 0;
            decimal targetm4 = 0;
            decimal targetm5 = 0;
            decimal targetm6 = 0;
            decimal targetm7 = 0;
            decimal targetm8 = 0;
            decimal targetm9 = 0;
            decimal targetm10 = 0;
            decimal targetm11 = 0;
            decimal targetm12 = 0;
            decimal actualm1 = 0;
            decimal actualm2 = 0;
            decimal actualm3 = 0;
            decimal actualm4 = 0;
            decimal actualm5 = 0;
            decimal actualm6 = 0;
            decimal actualm7 = 0;
            decimal actualm8 = 0;
            decimal actualm9 = 0;
            decimal actualm10 = 0;
            decimal actualm11 = 0;
            decimal actualm12 = 0;

            dbo_UserClass user_class1 = dbo_UserDataClass.Select_Record(User_ID);

            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        
            if (user_class1.User_Group_ID == "CP Meiji")
            {
                #region Old
                //List<string> item_01 = new List<string>(
                //    agent.Where(f => f.Invoice_Region != null).Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim()
                //    || f.APV_ID == User_ID.Trim() ||
                //    user_class1.Region.Contains(f.Invoice_Region))

                //    .Where(f => f.Status == true)

                //    .Select(f => f.CV_Code.Trim()));

                //Edit by keng --Invoice_Region --> Location_Region
                //List<string> item = new List<string>(
                //   agent.Where(f => f.Location_Region != null).Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim()
                //   || f.APV_ID == User_ID.Trim() || user_class1.Region.Contains(f.Location_Region))

                //   .Where(f => f.Status == true)

                //   .Select(f => f.CV_Code.Trim()));
                #endregion
                //Edit by keng --Invoice_Region --> Location_Region and Remove (|| user_class1.Region.Contains(f.Location_Region))
                List<string> item = new List<string>(
                 agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim()
                 || f.APV_ID == User_ID.Trim())

                 .Where(f => f.Status == true)

                 .Select(f => f.CV_Code.Trim()));
               

                if (item.Count != 0)
                {
                    List<dbo_SalesTargetClass> saletarget1 = dbo_SalesTargetDataClass.Search(item.First());
                    foreach (dbo_SalesTargetClass sale_target in saletarget1.Where(f => f.Year == (DateTime.Now.Year + 543).ToString()))
                    {

                        json.data.labels.Add(sale_target.Month);
                    }
                    //Dataset ds = new Dataset();

                    //212689 201555 201557 201545 212789

                    foreach (string agent_tmp in item)
                    {
                        List<dbo_SalesTargetClass> saletarget = dbo_SalesTargetDataClass.Search(agent_tmp.Trim());


                        saletarget1 = new List<dbo_SalesTargetClass>(saletarget.Where(f => f.Year == (DateTime.Now.Year + 543).ToString()));

                        foreach (dbo_SalesTargetClass sale_target in saletarget1)
                        {
                            if (sale_target.Month == "มกราคม")
                            {
                                targetm1 += sale_target.Sales_Target.Value;
                                actualm1 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "กุมภาพันธ์")
                            {
                                targetm2 += sale_target.Sales_Target.Value;
                                actualm2 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "มีนาคม")
                            {
                                targetm3 += sale_target.Sales_Target.Value;
                                actualm3 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "เมษายน")
                            {
                                targetm4 += sale_target.Sales_Target.Value;
                                actualm4 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "พฤษภาคม")
                            {
                                targetm5 += sale_target.Sales_Target.Value;
                                actualm5 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "มิถุนายน")
                            {
                                targetm6 += sale_target.Sales_Target.Value;
                                actualm6 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "กรกฎาคม")
                            {
                                targetm7 += sale_target.Sales_Target.Value;
                                actualm7 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "สิงหาคม")
                            {
                                targetm8 += sale_target.Sales_Target.Value;
                                actualm8 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "กันยายน")
                            {
                                targetm9 += sale_target.Sales_Target.Value;
                                actualm9 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "ตุลาคม")
                            {
                                targetm10 += sale_target.Sales_Target.Value;
                                actualm10 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "พฤศจิกายน")
                            {
                                targetm11 += sale_target.Sales_Target.Value;
                                actualm11 += sale_target.Actual_Sales.Value;
                            }
                            else if (sale_target.Month == "ธันวาคม")
                            {
                                targetm12 += sale_target.Sales_Target.Value;
                                actualm12 += sale_target.Actual_Sales.Value;
                            }
                        }

                    }

                }
                else //dashboard ของ CP Meiji ทุกAgent ใน Region
                {
                    bool addLabel = false;
                    string region = user_class1.Region;

                    string[] regions = region.Split(',');

                    foreach (string in_region in regions)
                    {
                        List<string> item3 = new List<string>(agent.Where(f => f.Location_Region == in_region ).Select(f => f.CV_Code.Trim()));
                        if (item3.Count != 0 && addLabel == false)
                        {
                            List<dbo_SalesTargetClass> saletarget1 = dbo_SalesTargetDataClass.Search(item3.FirstOrDefault());
                            foreach (dbo_SalesTargetClass sale_target in saletarget1)
                            {
                                json.data.labels.Add(sale_target.Month);
                            }
                            addLabel = true;
                        }
                        foreach (string agent_tmp in item3)
                        {
                            List<dbo_SalesTargetClass> saletarget = dbo_SalesTargetDataClass.Search(agent_tmp.Trim());
                            List<dbo_SalesTargetClass> saletarget3 = new List<dbo_SalesTargetClass>(saletarget.Where(f => f.Year == (DateTime.Now.Year + 543).ToString()));

                            foreach (dbo_SalesTargetClass sale_target in saletarget3)
                            {
                                if (sale_target.Month == "มกราคม")
                                {
                                    targetm1 += sale_target.Sales_Target.Value;
                                    actualm1 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "กุมภาพันธ์")
                                {
                                    targetm2 += sale_target.Sales_Target.Value;
                                    actualm2 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "มีนาคม")
                                {
                                    targetm3 += sale_target.Sales_Target.Value;
                                    actualm3 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "เมษายน")
                                {
                                    targetm4 += sale_target.Sales_Target.Value;
                                    actualm4 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "พฤษภาคม")
                                {
                                    targetm5 += sale_target.Sales_Target.Value;
                                    actualm5 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "มิถุนายน")
                                {
                                    targetm6 += sale_target.Sales_Target.Value;
                                    actualm6 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "กรกฎาคม")
                                {
                                    targetm7 += sale_target.Sales_Target.Value;
                                    actualm7 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "สิงหาคม")
                                {
                                    targetm8 += sale_target.Sales_Target.Value;
                                    actualm8 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "กันยายน")
                                {
                                    targetm9 += sale_target.Sales_Target.Value;
                                    actualm9 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "ตุลาคม")
                                {
                                    targetm10 += sale_target.Sales_Target.Value;
                                    actualm10 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "พฤศจิกายน")
                                {
                                    targetm11 += sale_target.Sales_Target.Value;
                                    actualm11 += sale_target.Actual_Sales.Value;
                                }
                                else if (sale_target.Month == "ธันวาคม")
                                {
                                    targetm12 += sale_target.Sales_Target.Value;
                                    actualm12 += sale_target.Actual_Sales.Value;
                                }
                            }
                        }
                    }
                }
            }
            else  //Agent
            {
                List<dbo_SalesTargetClass> saletarget = dbo_SalesTargetDataClass.Search(user_class.CV_CODE);
                List<dbo_SalesTargetClass> saletarget2 = new List<dbo_SalesTargetClass>(saletarget.Where(f => f.Year == (DateTime.Now.Year + 543).ToString()));
              
                foreach (dbo_SalesTargetClass sale_target in saletarget2)
                {
                    json.data.labels.Add(sale_target.Month);
                }

                //Dataset ds = new Dataset();

                foreach (dbo_SalesTargetClass sale_target in saletarget2)
                {

                    if (sale_target.Month == "มกราคม")
                    {
                        targetm1 += sale_target.Sales_Target.Value;
                        actualm1 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "กุมภาพันธ์")
                    {
                        targetm2 += sale_target.Sales_Target.Value;
                        actualm2 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "มีนาคม")
                    {
                        targetm3 += sale_target.Sales_Target.Value;
                        actualm3 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "เมษายน")
                    {
                        targetm4 += sale_target.Sales_Target.Value;
                        actualm4 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "พฤษภาคม")
                    {
                        targetm5 += sale_target.Sales_Target.Value;
                        actualm5 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "มิถุนายน")
                    {
                        targetm6 += sale_target.Sales_Target.Value;
                        actualm6 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "กรกฎาคม")
                    {
                        targetm7 += sale_target.Sales_Target.Value;
                        actualm7 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "สิงหาคม")
                    {
                        targetm8 += sale_target.Sales_Target.Value;
                        actualm8 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "กันยายน")
                    {
                        targetm9 += sale_target.Sales_Target.Value;
                        actualm9 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "ตุลาคม")
                    {
                        targetm10 += sale_target.Sales_Target.Value;
                        actualm10 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "พฤศจิกายน")
                    {
                        targetm11 += sale_target.Sales_Target.Value;
                        actualm11 += sale_target.Actual_Sales.Value;
                    }
                    else if (sale_target.Month == "ธันวาคม")
                    {
                        targetm12 += sale_target.Sales_Target.Value;
                        actualm12 += sale_target.Actual_Sales.Value;
                    }
                }

                /*ds.label = "เป้ายอดขาย";
                ds.type = "line";
                ds.borderColor = "#0EB38D";
                ds.fill = false;

                json.data.datasets.Add(ds);
                ds = new Dataset();
                foreach (dbo_SalesTargetClass sale_target in saletarget2)
                {
                    ds.data.Add(sale_target.Actual_Sales.Value);
                }

                ds.label = "ยอดขายจริง";
                ds.type = "bar";
                ds.borderColor = "#0E1FB3";
                ds.backgroundColor = "#0E1FB3";
                ds.fill = false;
                json.data.datasets.Add(ds);*/
            }

            Dataset ds = new Dataset();


            for (i = 0; i < 12; i++)
            {
                if (i == 0) { ds.data.Add(targetm1); }
                if (i == 1) { ds.data.Add(targetm2); }
                if (i == 2) { ds.data.Add(targetm3); }
                if (i == 3) { ds.data.Add(targetm4); }
                if (i == 4) { ds.data.Add(targetm5); }
                if (i == 5) { ds.data.Add(targetm6); }
                if (i == 6) { ds.data.Add(targetm7); }
                if (i == 7) { ds.data.Add(targetm8); }
                if (i == 8) { ds.data.Add(targetm9); }
                if (i == 9) { ds.data.Add(targetm10); }
                if (i == 10) { ds.data.Add(targetm11); }
                if (i == 11) { ds.data.Add(targetm12); }
            }
            ds.label = "เป้ายอดขาย";
            ds.type = "line";         
            ds.borderColor = "blue";
            ds.backgroundColor.Add("blue");
            ds.fill = false;
            json.data.datasets.Add(ds);

            ds = new Dataset();

            for (i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    ds.data.Add(actualm1);
                    if (actualm1 >= targetm1)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 1)
                {
                    ds.data.Add(actualm2);

                    if (actualm2 >= targetm2)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 2)
                {
                    ds.data.Add(actualm3);

                    if (actualm3 >= targetm3)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 3)
                {
                    ds.data.Add(actualm4);

                    if (actualm4 >= targetm4)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 4)
                {
                    ds.data.Add(actualm5);

                    if (actualm5 >= targetm5)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 5)
                {
                    ds.data.Add(actualm6);

                    if (actualm6 >= targetm6)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 6)
                {
                    ds.data.Add(actualm7);

                    if (actualm7 >= targetm7)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 7)
                {
                    ds.data.Add(actualm8);

                    if (actualm8 >= targetm8)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 8)
                {
                    ds.data.Add(actualm9);
                    if (actualm9 >= targetm9)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 9)
                {
                    ds.data.Add(actualm10);
                    if (actualm10 >= targetm10)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 10)
                {
                    ds.data.Add(actualm11);

                    if (actualm11 >= targetm11)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }
                if (i == 11)
                {
                    ds.data.Add(actualm12);
                    if (actualm12 >= targetm12)
                    {
                        ds.backgroundColor.Add("green");
                    }
                    else
                    {
                        ds.backgroundColor.Add("red");
                    }
                }

            }
            //font-family: 'Nunito', sans-serif;
            ds.label = "ยอดขายจริง";
            ds.type = "bar";
            //ds.fontFamily = "Prompt, sans-serif";
            //ds.borderColor = "green";
            //ds.backgroundColor.Add("green");
            //ds.fill = false;
            //ds            
            json.data.datasets.Add(ds);
            #endregion

            string json_string = new JavaScriptSerializer().Serialize(json);
            string final_json = @"var mixed_chart = new Chart(document.getElementById(""mixed-chart"")," + json_string + ");";

            ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", final_json, true);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

            //หาจำนวยลูกค้า
            //List<dbo_CustomerClass> customer = dbo_CustomerDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            // lbl_Customer.Text = customer.Count().ToString();
            //  user_class1.CV_CODE;

            try
            {
                //List<dbo_BillingClass> bill_ZDOM = dbo_BillingDataClass.Search(user_class1.CV_CODE, "ZDOM");
                //List<dbo_BillingClass> bill_YDOM = dbo_BillingDataClass.Search(user_class1.CV_CODE, "YDOM");


                //grdTab_1.DataSource = bill_ZDOM;
                //grdTab_1.DataBind();
                //grdTab_2.DataSource = bill_YDOM;
                //grdTab_2.DataBind();
                //if(user_class1.CV_CODE == "" || user_class1.CV_CODE == null)
                //{
                //    user_class1.CV_CODE = string.Empty;
                //}
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                //List<dbo_BillingClass> bill_All = dbo_BillingDataClass.Search(user_class1.CV_CODE.Trim(), string.Empty);

                /*
                List<dbo_BillingClass> bill_All = new List<dbo_BillingClass>();
                foreach (dbo_AgentClass agent_Name in agent)
                {
                    List<dbo_BillingClass> bill_agent = dbo_BillingDataClass.Search(agent_Name.CV_Code, string.Empty);
                    bill_All = bill_All.Union(bill_agent).ToList();
                }
                */

                List<dbo_BillingClass> bill_All = dbo_BillingDataClass.Get_Billing(User_ID);


                bill_All = bill_All.Where(f => f.Billing_Type != "YDOM")
                 .Where(f => (!(f.Billing_Type == "ZDOM" && !string.IsNullOrEmpty(f.Ref_Invoice_No))))
                 .Where(f => f.Invoice_Status != "ยกเลิกแล้ว")
                 .Where(f => f.Invoice_Status != "ยันยันแล้ว")
                 .ToList();

                List<dbo_BillingClass> bill_Tab1 = new List<dbo_BillingClass>();
                List<dbo_BillingClass> bill_Tab2= new List<dbo_BillingClass>();
                List<dbo_BillingClass> bill_Tab3 = new List<dbo_BillingClass>();

                foreach (dbo_BillingClass b in bill_All) {
                    if ((b.Billing_Type == "ZDOM" || b.Billing_Type == "YDOM") && (b.Invoice_Status == "ยังไม่ยืนยัน"))
                    {
                        bill_Tab1.Add(b);
                    }
                }

                grdTab_1.DataSource = bill_Tab1;
                grdTab_1.DataBind();


                foreach (dbo_BillingClass b in bill_All)
                {
                    if ((b.Billing_Type == "ZDDN") && (b.Invoice_Status == "ยังไม่ยืนยัน"))
                    {
                        bill_Tab2.Add(b);
                    }
                }

                grdTab_2.DataSource = bill_Tab2;
                grdTab_2.DataBind();

                foreach (dbo_BillingClass b in bill_All)
                {
                    if ((b.Billing_Type == "ZDCN") && (b.Invoice_Status == "ยังไม่ยืนยัน"))
                    {
                        bill_Tab3.Add(b);
                    }
                }

                grdTab_3.DataSource = bill_Tab3;
                grdTab_3.DataBind();

                //grdTab_1.DataSource = bill_All.Where(f => f.Billing_Type == "ZDOM" || f.Billing_Type == "YDOM")
                //    .Where(f => f.Invoice_Status == "ยังไม่ยืนยัน");

                //grdTab_1.DataBind();
                //grdTab_2.DataSource = bill_All.Where(f => f.Billing_Type == "ZDDN").Where(f => f.Invoice_Status == "ยังไม่ยืนยัน"); ;
                //grdTab_2.DataBind();
                //grdTab_3.DataSource = bill_All.Where(f => f.Billing_Type == "ZDCN").Where(f => f.Invoice_Status == "ยังไม่ยืนยัน"); ;
                //grdTab_3.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }


        }
    }
    #endregion

    #region Methods
    private void GenerateNews(string News_type, string agentname)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        try
        {
            List<dbo_NewsClass2> news_list = dbo_NewsDataClass2.Search("ทั่วไป", agentname);

            foreach (dbo_NewsClass2 news in news_list)
            {
                string image = string.Empty;
                if (news.Photo_MemoryStream != null)
                {
                    image = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(news.Photo_MemoryStream, 0, news.Photo_MemoryStream.Length));
                }
                else
                {
                    image = string.Format("data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBvbHlnb24gc3R5bGU9ImZpbGw6I0VDRTdFQTsiIHBvaW50cz0iNTEyLDczLjk1NiA1MTIsNDk0LjkzMyAwLDQ5NC45MzMgMCw3My45NTYgMjU2LDUxLjIgIi8+Cjxwb2x5Z29uIHN0eWxlPSJmaWxsOiNGMTQ3NDI7IiBwb2ludHM9IjUxMiwxNy4wNjcgNTEyLDczLjk1NiA0NTUuMTExLDczLjk1NiA0MjAuOTc4LDQ1LjUxMSA0NTUuMTExLDE3LjA2NyAiLz4KPHJlY3QgeT0iMTcuMDY3IiBzdHlsZT0iZmlsbDojMUI0MTQ1OyIgd2lkdGg9IjQ1NS4xMTEiIGhlaWdodD0iNTYuODg5Ii8+CjxwYXRoIHN0eWxlPSJmaWxsOiNENkNFRDE7IiBkPSJNNDQzLjczMywyNTguODQ0SDY4LjI2N2MtNC43MTQsMC04LjUzMy0zLjgyMS04LjUzMy04LjUzM2MwLTQuNzEzLDMuODItOC41MzMsOC41MzMtOC41MzNoMzc1LjQ2NyAgYzQuNzE0LDAsOC41MzMsMy44MjEsOC41MzMsOC41MzNDNDUyLjI2NywyNTUuMDI0LDQ0OC40NDcsMjU4Ljg0NCw0NDMuNzMzLDI1OC44NDR6Ii8+CjxnPgoJPHBhdGggc3R5bGU9ImZpbGw6IzhCN0U3RjsiIGQ9Ik0yMzMuMjQ0LDMwNC4zNTZINjguMjY3Yy00LjcxNCwwLTguNTMzLTMuODIxLTguNTMzLTguNTMzczMuODItOC41MzMsOC41MzMtOC41MzNoMTY0Ljk3OCAgIGM0LjcxNCwwLDguNTMzLDMuODIxLDguNTMzLDguNTMzUzIzNy45NTgsMzA0LjM1NiwyMzMuMjQ0LDMwNC4zNTZ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojOEI3RTdGOyIgZD0iTTIzMy4yNDQsMzQ5Ljg2N0g2OC4yNjdjLTQuNzE0LDAtOC41MzMtMy44MjEtOC41MzMtOC41MzNzMy44Mi04LjUzMyw4LjUzMy04LjUzM2gxNjQuOTc4ICAgYzQuNzE0LDAsOC41MzMsMy44MjEsOC41MzMsOC41MzNTMjM3Ljk1OCwzNDkuODY3LDIzMy4yNDQsMzQ5Ljg2N3oiLz4KCTxwYXRoIHN0eWxlPSJmaWxsOiM4QjdFN0Y7IiBkPSJNMjMzLjI0NCwzOTUuMzc4SDY4LjI2N2MtNC43MTQsMC04LjUzMy0zLjgyMS04LjUzMy04LjUzM3MzLjgyLTguNTMzLDguNTMzLTguNTMzaDE2NC45NzggICBjNC43MTQsMCw4LjUzMywzLjgyMSw4LjUzMyw4LjUzM1MyMzcuOTU4LDM5NS4zNzgsMjMzLjI0NCwzOTUuMzc4eiIvPgo8L2c+CjxyZWN0IHg9IjI3OC43NTYiIHk9IjI5NS44MjIiIHN0eWxlPSJmaWxsOiNGMTQ3NDI7IiB3aWR0aD0iMTY0Ljk3OCIgaGVpZ2h0PSIxMzYuNTMzIi8+CjxnPgoJPHBhdGggc3R5bGU9ImZpbGw6IzhCN0U3RjsiIGQ9Ik0yMzMuMjQ0LDQ0MC44ODlINjguMjY3Yy00LjcxNCwwLTguNTMzLTMuODIxLTguNTMzLTguNTMzczMuODItOC41MzMsOC41MzMtOC41MzNoMTY0Ljk3OCAgIGM0LjcxNCwwLDguNTMzLDMuODIxLDguNTMzLDguNTMzUzIzNy45NTgsNDQwLjg4OSwyMzMuMjQ0LDQ0MC44ODl6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojOEI3RTdGOyIgZD0iTTM5NS4wNCwyMTMuMzMzYy0xNS4zNjYsMC0yNy4zNDQtNi43MjUtMzEuMjYzLTE3LjU1Yy0xLjYwMy00LjQzMiwwLjY4OC05LjMyNCw1LjEyMS0xMC45MjcgICBjNC40MjYtMS42MDQsOS4zMjMsMC42ODcsMTAuOTI3LDUuMTJjMS4zNjEsMy43NjQsNy40NzUsNi4yOTIsMTUuMjEzLDYuMjkyYzUuNzg2LDAsMTUuNTc2LTEuNzk5LDE1LjU3Ni04LjU0ICAgYzAtNC42MTktMi4xMTEtNi4yMTYtMTYuOTU5LTguNjQ1Yy02LjU5OS0xLjA3OS0xMy40MjEtMi4xOTUtMTkuMTY5LTUuMjE5Yy03LjkxMS00LjE2My0xMi4wOTItMTEuMTY3LTEyLjA5Mi0yMC4yNTcgICBjMC0xNS4wNzcsMTMuNDIzLTI1LjYwNywzMi42NDMtMjUuNjA3YzE1LjM1OSwwLDI3LjMzNiw2LjcyMSwzMS4yNTksMTcuNTRjMS42MDcsNC40MzEtMC42ODQsOS4zMjQtNS4xMTQsMTAuOTMxICAgYy00LjQzNCwxLjYxMS05LjMyNC0wLjY4NS0xMC45MjktNS4xMTRjLTEuMzY0LTMuNzYxLTcuNDc3LTYuMjktMTUuMjE0LTYuMjljLTUuNzg2LDAtMTUuNTc2LDEuNzk5LTE1LjU3Niw4LjU0ICAgYzAsNC42MTEsMi4xMDksNi4yMDUsMTYuOTQ5LDguNjMyYzYuNiwxLjA4LDEzLjQyNiwyLjE5NiwxOS4xNzUsNS4yMjFjNy45MTMsNC4xNjQsMTIuMDk2LDExLjE3MiwxMi4wOTYsMjAuMjY2ICAgQzQyNy42ODQsMjAyLjgwMyw0MTQuMjYsMjEzLjMzMywzOTUuMDQsMjEzLjMzM3oiLz4KCTxwYXRoIHN0eWxlPSJmaWxsOiM4QjdFN0Y7IiBkPSJNMzE5LjQ5NiwyMTMuMzMzYy0zLjE4NywwLTYuMTQzLTEuNzg2LTcuNjA5LTQuNjc2bC05LjY4Ni0xOS4xMTRsLTkuNjg2LDE5LjExNCAgIGMtMS42MDgsMy4xNzQtNS4wMjksNS4wMjItOC41NTMsNC42MjRjLTMuNTM2LTAuMzkxLTYuNDU4LTIuOTM3LTcuMzMyLTYuMzg1bC0xNy4yOTgtNjguMjY3Yy0xLjE1OC00LjU2OCwxLjYwOC05LjIxLDYuMTc3LTEwLjM2NyAgIGM0LjU2Ny0xLjE1NSw5LjIxLDEuNjA3LDEwLjM2Nyw2LjE3NmwxMS43MDUsNDYuMTk3bDcuMDA1LTEzLjgyNWMxLjQ1NC0yLjg2OCw0LjM5Ni00LjY3Niw3LjYxMi00LjY3NiAgIGMzLjIxNSwwLDYuMTU4LDEuODA4LDcuNjEyLDQuNjc2bDcuMDA1LDEzLjgyNWwxMS43MDUtNDYuMTk3YzEuMTU5LTQuNTY5LDUuODAyLTcuMzMzLDEwLjM2Ny02LjE3NiAgIGM0LjU2OSwxLjE1Nyw3LjMzNCw1Ljc5OSw2LjE3NywxMC4zNjdsLTE3LjI5OCw2OC4yNjdjLTAuODc1LDMuNDQ5LTMuNzk2LDUuOTk0LTcuMzMyLDYuMzg1ICAgQzMyMC4xMjMsMjEzLjMxNiwzMTkuODA5LDIxMy4zMzMsMzE5LjQ5NiwyMTMuMzMzeiIvPgoJPHBhdGggc3R5bGU9ImZpbGw6IzhCN0U3RjsiIGQ9Ik0yMzMuNDcyLDIxMy4zMzNoLTQ4LjIyYy00LjcxNCwwLTguNTMzLTMuODIxLTguNTMzLTguNTMzdi02OC4yNjdjMC00LjcxMywzLjgyLTguNTMzLDguNTMzLTguNTMzICAgaDQ4LjIyYzQuNzE0LDAsOC41MzMsMy44MjEsOC41MzMsOC41MzNzLTMuODIsOC41MzMtOC41MzMsOC41MzNoLTM5LjY4N3Y1MS4yaDM5LjY4N2M0LjcxNCwwLDguNTMzLDMuODIxLDguNTMzLDguNTMzICAgUzIzOC4xODYsMjEzLjMzMywyMzMuNDcyLDIxMy4zMzN6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojOEI3RTdGOyIgZD0iTTIxNy4zOTksMTc5LjJoLTMyLjE0N2MtNC43MTQsMC04LjUzMy0zLjgyMS04LjUzMy04LjUzM3MzLjgyLTguNTMzLDguNTMzLTguNTMzaDMyLjE0NyAgIGM0LjcxNCwwLDguNTMzLDMuODIxLDguNTMzLDguNTMzUzIyMi4xMTIsMTc5LjIsMjE3LjM5OSwxNzkuMnoiLz4KCTxwYXRoIHN0eWxlPSJmaWxsOiM4QjdFN0Y7IiBkPSJNMTUxLjEyMSwyMTMuMzMzYy0yLjQ1LDAtNC44MzktMS4wNTctNi40OTItMi45OTNsLTQzLjI0Ni01MC42NjVWMjA0LjggICBjMCw0LjcxMy0zLjgyLDguNTMzLTguNTMzLDguNTMzYy00LjcxNCwwLTguNTMzLTMuODIxLTguNTMzLTguNTMzdi02OC4yNjdjMC0zLjU3NSwyLjIyOC02Ljc3LDUuNTgxLTguMDA3ICAgYzMuMzUyLTEuMjM4LDcuMTIyLTAuMjUzLDkuNDQyLDIuNDY2bDQzLjI0Niw1MC42NjV2LTQ1LjEyNGMwLTQuNzEzLDMuODItOC41MzMsOC41MzMtOC41MzNzOC41MzMsMy44MjEsOC41MzMsOC41MzNWMjA0LjggICBjMCwzLjU3NS0yLjIyOCw2Ljc3LTUuNTgxLDguMDA3QzE1My4xMDcsMjEzLjE2MiwxNTIuMTA4LDIxMy4zMzMsMTUxLjEyMSwyMTMuMzMzeiIvPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+Cjwvc3ZnPgo=");
                }

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "well");

                container_new_tab1.Controls.Add(div);

                HtmlGenericControl div_media = new HtmlGenericControl("div");
                div_media.Attributes.Add("class", "media");
                div.Controls.Add(div_media);
                HtmlGenericControl div_pull_left = new HtmlGenericControl("a");
                div_pull_left.Attributes.Add("class", "pull-left");
                div_pull_left.Attributes.Add("href", "#");
                div_media.Controls.Add(div_pull_left);

                Image img = new Image();
                img.Width = 150;
                img.Height = 100;

                img.Style.Add("border", "1px solid #d4d1d1");
                img.Style.Add("padding", "1px 1px 1px 1px");
                img.ImageUrl = image;

                div_pull_left.Controls.Add(img);
                HtmlGenericControl div_media_body = new HtmlGenericControl("div");
                div_media_body.Attributes.Add("class", "media-body");
                div_media.Controls.Add(div_media_body);

                HtmlGenericControl h4 = new HtmlGenericControl("h4");
                h4.Attributes.Add("class", "media-heading");

                HtmlGenericControl a = new HtmlGenericControl("a");
                a.Attributes.Add("href", "NewsPage.aspx?News_ID=" + news.News_ID);
                a.Attributes.Add("target", "_blank");
                a.InnerHtml = news.Subject;


                HtmlGenericControl img_icon_mail = new HtmlGenericControl("img");

                h4.Controls.Add(a);

                if ((DateTime.Now.Date - news.Start_Date.Value.Date).TotalDays < 30)
                {
                    HtmlGenericControl img_icon_new = new HtmlGenericControl("img");
                    img_icon_new.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTYuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgd2lkdGg9IjMycHgiIGhlaWdodD0iMzJweCIgdmlld0JveD0iMCAwIDQ4MS4wNDUgNDgxLjA0NSIgc3R5bGU9ImVuYWJsZS1iYWNrZ3JvdW5kOm5ldyAwIDAgNDgxLjA0NSA0ODEuMDQ1OyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSI+CjxnPgoJPHBhdGggZD0iTTQzNC44NzQsMjQxLjAzMWwzNS40MDUtMzYuMjkyYzQuNDAzLTQuNTEzLDYuMjI1LTEwLjk0Nyw0LjgzOC0xNy4wOTdjLTEuMzg3LTYuMTUtNS43OS0xMS4xODItMTEuNzA0LTEzLjM3ICAgbC00Ny41NDYtMTcuNTk1bDE2LjE2My00OC4wNTRjMi4wMTEtNS45NzYsMC44NTktMTIuNTYzLTMuMDU2LTE3LjUwNGMtMy45MTctNC45NC0xMC4wNy03LjU3Mi0xNi4zNDQtNi45NzFsLTUwLjQ3OSw0Ljc2NiAgIGwtNi4yNy01MC4zMDljLTAuNzc5LTYuMjU3LTQuNjcyLTExLjY5NC0xMC4zNDYtMTQuNDQ4Yy01LjY3MS0yLjc1My0xMi4zNTMtMi40NS0xNy43NSwwLjgwNkwyODMuOTcsNTEuMzkxTDI1Ni4wOTQsOC40NzcgICBDMjUyLjY1OSwzLjE5LDI0Ni43ODIsMCwyNDAuNDc4LDBjLTYuMzA0LDAtMTIuMTgyLDMuMTktMTUuNjE3LDguNDc4bC0yNy42MTYsNDIuNTE3bC00My4zMjItMjYuMzMzICAgYy01LjM4Ni0zLjI3NS0xMi4wNjUtMy42MDMtMTcuNzQ4LTAuODY4Yy01LjY4MSwyLjczNS05LjU5Miw4LjE1OC0xMC4zOTQsMTQuNDEybC02LjQ0OCw1MC4yODhsLTUwLjQ1Ny00Ljk0MiAgIGMtNi4yNzMtMC42MTktMTIuNDM1LDEuOTg4LTE2LjM2OCw2LjkxNWMtMy45MzMsNC45MjctNS4xMDYsMTEuNTEtMy4xMTcsMTcuNDkybDE1Ljk5NCw0OC4xMDlsLTQ3LjYwOSwxNy40MjkgICBjLTUuOTIsMi4xNjctMTAuMzQxLDcuMTg0LTExLjc0OCwxMy4zMjljLTEuNDA4LDYuMTQ2LDAuMzksMTIuNTg2LDQuNzc1LDE3LjExNGwzNS4yNzksMzYuNDE0bC0zNS4zNCwzNi4zNTEgICBjLTQuMzk0LDQuNTIxLTYuMjAzLDEwLjk1OC00LjgwOCwxNy4xMDRjMS4zOTgsNi4xNDgsNS44MTIsMTEuMTcyLDExLjcyOCwxMy4zNUw2NS4yNCwzMjQuNjdsLTE2LjA4Myw0OC4wODIgICBjLTIsNS45NzktMC44NCwxMi41NjQsMy4wODYsMTcuNDk4YzMuOTI1LDQuOTM2LDEwLjA4Miw3LjU0OSwxNi4zNTUsNi45NDNsNTAuNDY1LTQuODU0bDYuMzYyLDUwLjMwMSAgIGMwLjc5Miw2LjI1NCw0LjY5MiwxMS42ODQsMTAuMzY5LDE0LjQyOGM1LjY3NywyLjc0NiwxMi4zNTYsMi40MywxNy43NDgtMC44MzZsNDMuMzctMjYuMjU4bDI3LjU0NSw0Mi41NjYgICBjMy40MjUsNS4yOTMsOS4yOTcsOC40OTQsMTUuNjAxLDguNTA0YzAuMDEsMCwwLjAyMSwwLDAuMDMyLDBjNi4yOTMsMCwxMi4xNjItMy4xNzgsMTUuNTk4LTguNDQ5bDI3LjY5Mi00Mi40NjlsNDMuMjc5LDI2LjQxICAgYzUuMzgxLDMuMjg1LDEyLjA2LDMuNjIzLDE3Ljc0NSwwLjg5OGM1LjY4Ny0yLjcyNyw5LjYwNi04LjE0MywxMC40Mi0xNC4zOTVsNi41MzUtNTAuMjc3bDUwLjQ1MSw1LjAyOSAgIGM2LjI4MSwwLjYyNSwxMi40MzgtMS45NjcsMTYuMzgtNi44ODdjMy45NDEtNC45Miw1LjEyNi0xMS41LDMuMTQ2LTE3LjQ4NmwtMTUuOTEyLTQ4LjEzNWw0Ny42MzktMTcuMzQ4ICAgYzUuOTI0LTIuMTU2LDEwLjM1NC03LjE2NiwxMS43NzItMTMuMzA5cy0wLjM2OC0xMi41ODYtNC43NDctMTcuMTIyTDQzNC44NzQsMjQxLjAzMXogTTE4MC43ODUsMjgxLjkxNSAgIGMwLDMuMDI0LTIuNDUxLDUuNDc2LTUuNDc2LDUuNDc2aC0xNi4zNjZjLTEuOTUzLDAtMy43NTctMS4wMzktNC43MzctMi43MjlsLTE5LjE0OC0zMy4wMDggICBjLTMuMTMyLTUuMzgzLTguNjQ0LTE1LjE5LTEzLjY5MS0yNS44NjRjMC4zNzUsMTAuMTIyLDAuNTU4LDIwLjkwNSwwLjU1OCwzMi43NDh2MjMuMzc3YzAsMy4wMjQtMi40NTIsNS40NzYtNS40NzYsNS40NzZoLTExLjg5MSAgIGMtMy4wMjUsMC01LjQ3Ny0yLjQ1MS01LjQ3Ny01LjQ3NnYtODIuNzgxYzAtMy4wMjQsMi40NTItNS40NzcsNS40NzctNS40NzdoMTguOTI2YzEuOTMzLDAsMy43MjIsMS4wMiw0LjcwOCwyLjY4MWwxOC41NzIsMzEuMjggICBjNC4yNTEsNy4yMDQsOC41MTEsMTUuNDE0LDEyLjE2NywyMy40MDRjLTAuNjc5LTguNS0wLjk5LTE3Ljg4OC0wLjk5LTI5LjMxNXYtMjIuNTcxYzAtMy4wMjQsMi40NTEtNS40NzcsNS40NzYtNS40NzdoMTEuODkxICAgYzMuMDI1LDAsNS40NzYsMi40NTMsNS40NzYsNS40NzdMMTgwLjc4NSwyODEuOTE1TDE4MC43ODUsMjgxLjkxNXogTTI1NC45OTYsMjgxLjkxNWMwLDMuMDI0LTIuNDUyLDUuNDc2LTUuNDc3LDUuNDc2aC01Mi43MjcgICBjLTMuMDI0LDAtNS40NzctMi40NTEtNS40NzctNS40NzZ2LTgyLjc4MWMwLTMuMDI0LDIuNDUyLTUuNDc3LDUuNDc3LTUuNDc3aDUwLjc1NWMzLjAyNCwwLDUuNDc2LDIuNDUzLDUuNDc2LDUuNDc3djguOTggICBjMCwzLjAyNC0yLjQ1MSw1LjQ3Ny01LjQ3Niw1LjQ3N2gtMzEuNTZ2MTUuNjg5aDI5LjQ0NWMzLjAyNSwwLDUuNDc3LDIuNDUyLDUuNDc3LDUuNDc3djguODQ3YzAsMy4wMjQtMi40NTEsNS40NzYtNS40NzcsNS40NzYgICBoLTI5LjQ0NXYxOC4zNzloMzMuNTNjMy4wMjQsMCw1LjQ3NywyLjQ1Miw1LjQ3Nyw1LjQ3NkwyNTQuOTk2LDI4MS45MTVMMjU0Ljk5NiwyODEuOTE1eiBNMzgxLjc3MywyMDAuNTYzbC0yMi4zODYsODIuNzggICBjLTAuNjQ2LDIuMzg4LTIuODEyLDQuMDQ4LTUuMjg2LDQuMDQ4aC0xNy4wNTNjLTIuNiwwLTQuODQxLTEuODI4LTUuMzY0LTQuMzczbC03LjM4LTM1Ljg0MSAgIGMtMS4zMzItNi42NTUtMi40MjItMTIuNTEtMy4zMjMtMTguNjQ5Yy0xLjE4MSw2LjU4Ni0yLjM4NywxMi41MTUtMy45MTEsMTguNzAzbC04LjMwMiwzNS45MTYgICBjLTAuNTc0LDIuNDg0LTIuNzg3LDQuMjQ0LTUuMzM2LDQuMjQ0aC0xNy4zNDdjLTIuNTE3LDAtNC43MDgtMS43MTQtNS4zMTQtNC4xNTZsLTIwLjU4NS04Mi43NzljLTAuNDA3LTEuNjM1LTAuMDM5LTMuMzY3LDEtNC42OTUgICBjMS4wMzgtMS4zMjgsMi42My0yLjEwNCw0LjMxNC0yLjEwNGgxNC43MThjMi42MjUsMCw0Ljg4MiwxLjg2NCw1LjM3Nyw0LjQ0M2w2LjU3NSwzNC4yMTJjMS41MjEsNy43NTEsMi45NjcsMTUuOTU4LDQuMjM3LDIzLjM4NiAgIGMxLjI5OS03LjExMSwyLjktMTQuODM2LDQuOC0yMy42OTZsNy4zNjItMzQuMDI3YzAuNTQ1LTIuNTIsMi43NzQtNC4zMTksNS4zNTMtNC4zMTloMTYuNDQ3YzIuNjExLDAsNC44NTksMS44NDYsNS4zNzEsNC40MDcgICBsNi45NzcsMzUuMDI5YzEuMzc1LDYuOTE2LDIuNTY3LDEzLjY4OCwzLjcxOSwyMS4xNTNjMC44NC00Ljk2MywxLjc4Ni0xMC4wMjYsMi43NjUtMTUuMjY4bDAuMDYzLTAuMzM1ICAgYzAuNDA2LTIuMTc3LDAuODEyLTQuMzYyLDEuMjE2LTYuNTU0YzAuMDA3LTAuMDQ0LDAuMDE2LTAuMDg3LDAuMDI1LTAuMTNsNy4wODYtMzMuOTQ0YzAuNTMtMi41MzgsMi43NjgtNC4zNTgsNS4zNi00LjM1OGgxMy41MzYgICBjMS43MDUsMCwzLjMxMywwLjc5NCw0LjM1LDIuMTQ4QzM4MS44NzIsMTk3LjE1OSwzODIuMjE5LDE5OC45MTgsMzgxLjc3MywyMDAuNTYzeiIgZmlsbD0iIzAwMDAwMCIvPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+Cjwvc3ZnPgo=");
                    h4.Controls.Add(img_icon_new);
                }


                div_media_body.Controls.Add(h4);

                HtmlGenericControl p = new HtmlGenericControl("p");
                p.InnerText = news.Content.Length > 120 ? news.Content.Substring(0, 118) : news.Content;

                HtmlGenericControl a1 = new HtmlGenericControl("a");
                a1.Attributes.Add("href", "NewsPage.aspx?News_ID=" + news.News_ID);
                a1.Attributes.Add("target", "_blank");
                a1.InnerHtml = "รายละเอียดเพิ่มเติม...";


                div_media_body.Controls.Add(p);
                div_media_body.Controls.Add(a1);


                List<dbo_ReadNewsClass> readnew = dbo_ReadNewsDataClass.Search(news.News_ID, HttpContext.Current.Request.Cookies["User_ID"].Value);
                if (readnew.Count == 0)
                {
                    //img_icon_mail.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDM3LjgwMSAzNy44MDEiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDM3LjgwMSAzNy44MDE7IiB4bWw6c3BhY2U9InByZXNlcnZlIiB3aWR0aD0iMzJweCIgaGVpZ2h0PSIzMnB4Ij4KPGc+Cgk8Zz4KCQk8Zz4KCQkJPHBhdGggZD0iTTI1LjEwOSwyOC4yOThjLTAuMTIzLDAtMC4yNDYtMC4wNDUtMC4zNDItMC4xMzZsLTUuNzU0LTUuMzk4Yy0wLjIwMi0wLjE4OC0wLjIxMi0wLjUwNS0wLjAyMi0wLjcwNiAgICAgYzAuMTg4LTAuMjAzLDAuNTA3LTAuMjExLDAuNzA2LTAuMDIybDUuNzU0LDUuMzk4YzAuMjAyLDAuMTg4LDAuMjEyLDAuNTA1LDAuMDIyLDAuNzA2ICAgICBDMjUuMzc2LDI4LjI0NSwyNS4yNDIsMjguMjk4LDI1LjEwOSwyOC4yOTh6IiBmaWxsPSIjMDAwMDAwIi8+CgkJCTxwYXRoIGQ9Ik01LjkwNCwyOC4yOThjLTAuMTMzLDAtMC4yNjctMC4wNTMtMC4zNjQtMC4xNThjLTAuMTg5LTAuMjAxLTAuMTgtMC41MTgsMC4wMjItMC43MDZsNS43NTQtNS40ICAgICBjMC4xOTktMC4xODgsMC41MTktMC4xODEsMC43MDYsMC4wMjJjMC4xODksMC4yMDEsMC4xOCwwLjUxOC0wLjAyMiwwLjcwNmwtNS43NTQsNS40QzYuMTUsMjguMjUzLDYuMDI3LDI4LjI5OCw1LjkwNCwyOC4yOTh6IiBmaWxsPSIjMDAwMDAwIi8+CgkJPC9nPgoJCTxwYXRoIGQ9Ik0yOC41MTIsMzMuMzE3SDIuNWMtMS4zNzksMC0yLjUtMS4xMjEtMi41LTIuNVYxMy43NjljMC0xLjM3OSwxLjEyMS0yLjUsMi41LTIuNWgyMS4yMjVjMC4yNzYsMCwwLjUsMC4yMjQsMC41LDAuNSAgICBzLTAuMjI0LDAuNS0wLjUsMC41SDIuNWMtMC44MjcsMC0xLjUsMC42NzMtMS41LDEuNXYxNy4wNDljMCwwLjgyNywwLjY3MywxLjUsMS41LDEuNWgyNi4wMTJjMC44MjcsMCwxLjUtMC42NzMsMS41LTEuNVYxOC41NTYgICAgYzAtMC4yNzYsMC4yMjQtMC41LDAuNS0wLjVzMC41LDAuMjI0LDAuNSwwLjV2MTIuMjYyQzMxLjAxMiwzMi4xOTYsMjkuODkxLDMzLjMxNywyOC41MTIsMzMuMzE3eiIgZmlsbD0iIzAwMDAwMCIvPgoJCTxwYXRoIGQ9Ik0xNS41MTIsMjQuODA2Yy0wLjY2NywwLTEuMzM1LTAuMjIyLTEuODQyLTAuNjY0TDAuODc3LDEyLjk4OGMtMC4yMDktMC4xODItMC4yMy0wLjQ5Ny0wLjA0OS0wLjcwNSAgICBjMC4xODItMC4yMTEsMC40OTctMC4yMzEsMC43MDUtMC4wNDlsMTIuNzkzLDExLjE1M2MwLjY0MiwwLjU1OCwxLjcyMywwLjU2LDIuMzY0LDAuMDA0bDguNTI5LTcuMzg2ICAgIGMwLjIwOS0wLjE4MSwwLjUyNC0wLjE1OCwwLjcwNSwwLjA1MXMwLjE1OCwwLjUyNC0wLjA1MSwwLjcwNWwtOC41MjksNy4zODZDMTYuODM5LDI0LjU4NiwxNi4xNzYsMjQuODA2LDE1LjUxMiwyNC44MDZ6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8L2c+Cgk8Zz4KCQk8cGF0aCBkPSJNMzAuNTE0LDE5LjA1NmMtNC4wMiwwLTcuMjg5LTMuMjY5LTcuMjg5LTcuMjg1YzAtNC4wMTksMy4yNy03LjI4Nyw3LjI4OS03LjI4N2M0LjAxOSwwLDcuMjg3LDMuMjY5LDcuMjg3LDcuMjg3ICAgIEMzNy44MDEsMTUuNzg3LDM0LjUzMiwxOS4wNTYsMzAuNTE0LDE5LjA1NnogTTMwLjUxNCw1LjQ4M2MtMy40NjgsMC02LjI4OSwyLjgyLTYuMjg5LDYuMjg3YzAsMy40NjYsMi44MjEsNi4yODUsNi4yODksNi4yODUgICAgYzMuNDY3LDAsNi4yODctMi44MTksNi4yODctNi4yODVDMzYuODAxLDguMzA0LDMzLjk4LDUuNDgzLDMwLjUxNCw1LjQ4M3oiIGZpbGw9IiMwMDAwMDAiLz4KCQk8cGF0aCBkPSJNMzMuNTg0LDEyLjI3MWgtNi4wMTZjLTAuMjc2LDAtMC41LTAuMjI0LTAuNS0wLjVzMC4yMjQtMC41LDAuNS0wLjVoNi4wMTZjMC4yNzYsMCwwLjUsMC4yMjQsMC41LDAuNSAgICBTMzMuODYsMTIuMjcxLDMzLjU4NCwxMi4yNzF6IiBmaWxsPSIjMDAwMDAwIi8+CgkJPHBhdGggZD0iTTMwLjU3NiwxNS4yNzhjLTAuMjc2LDAtMC41LTAuMjI0LTAuNS0wLjVWOC43NjVjMC0wLjI3NiwwLjIyNC0wLjUsMC41LTAuNXMwLjUsMC4yMjQsMC41LDAuNXY2LjAxNCAgICBDMzEuMDc2LDE1LjA1NSwzMC44NTMsMTUuMjc4LDMwLjU3NiwxNS4yNzh6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8L2c+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPC9zdmc+Cg==");

                    //h4.Controls.Add(img_icon_mail);
                }
                else
                {
                    img_icon_mail.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTguMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDYwIDYwIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA2MCA2MDsiIHhtbDpzcGFjZT0icHJlc2VydmUiIHdpZHRoPSIzMnB4IiBoZWlnaHQ9IjMycHgiPgo8Zz4KCTxwYXRoIGQ9Ik01OS45NzMsMjYuNTM1bC0wLjAwMy0wLjA1N2wtMC4xNDctMC4wODJMNTQsMjAuOTc1VjguOTk3SDQxLjEzNWwtOC41OTMtOGMtMS40MjYtMS4zMjYtMy42NTktMS4zMjctNS4wODUsMC4wMDEgICBsLTguNTkyLDcuOTk5SDZ2MTEuOTc4bC01Ljg4OSw1LjQ4MkwwLDI2LjUzMXYwLjAzdjMyLjMwOHYxLjEyOWgxLjEyOWg1Ni4xMjdoMS42MTVoMC40NDlINjB2LTEuMTI5VjI3LjEwMXYtMC41NEw1OS45NzMsMjYuNTM1eiAgICBNMzIuMDcyLDQyLjk4N2w5Ljk1Mi01LjQ4N0w1NCwzMC44OTd2MGw0LTIuMjA2djI4LjU5M2wtNC43MDYtMi41OTRMMzIuMDcyLDQyLjk4N3ogTTU3LjMxLDI2Ljc4OUw1NCwyOC42MTR2LTQuOTA3TDU3LjMxLDI2Ljc4OSAgIHogTTI4LjgxOSwyLjQ2MmMwLjY2Mi0wLjYxNSwxLjctMC42MTQsMi4zNi0wLjAwMWw3LjAyLDYuNTM2SDIxLjgwMUwyOC44MTksMi40NjJ6IE04LDEwLjk5N2g4LjcxN2gyNi41NjZINTJ2OC4xMTZ2MTAuNjAyICAgbC0xOC4xMDIsOS45ODJsLTMuODk3LDIuMTQ5TDgsMjkuNzE1VjE5LjExNFYxMC45OTd6IE02LDI4LjYxMmwtMy4zMDgtMS44MjRMNiwyMy43MDdWMjguNjEyeiBNMiw1Ny45OTdWMjguNjkxbDQsMi4yMDZ2MCAgIGwxNy44Miw5LjgyNWw0LjEwOSwyLjI2NmwwLDBsMTAuMzE2LDUuNjg4bDE2LjkwNyw5LjMyMkgyeiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTI3LDE3Ljk5N2g2YzAuNTUzLDAsMS0wLjQ0NywxLTFzLTAuNDQ3LTEtMS0xaC02Yy0wLjU1MywwLTEsMC40NDctMSwxUzI2LjQ0NywxNy45OTcsMjcsMTcuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTQyLDMwLjk5N2gtN2MtMC41NTMsMC0xLDAuNDQ3LTEsMXMwLjQ0NywxLDEsMWg3YzAuNTUzLDAsMS0wLjQ0NywxLTFTNDIuNTUzLDMwLjk5Nyw0MiwzMC45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMTcsMjQuOTk3aDVjMC41NTMsMCwxLTAuNDQ3LDEtMXMtMC40NDctMS0xLTFoLTVjLTAuNTUzLDAtMSwwLjQ0Ny0xLDFTMTYuNDQ3LDI0Ljk5NywxNywyNC45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMjksMjMuOTk3YzAsMC41NTMsMC40NDcsMSwxLDFoOGMwLjU1MywwLDEtMC40NDcsMS0xcy0wLjQ0Ny0xLTEtMWgtOEMyOS40NDcsMjIuOTk3LDI5LDIzLjQ0NSwyOSwyMy45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMjYsMjQuOTk3YzAuMjYsMCwwLjUyLTAuMTEsMC43MS0wLjI5YzAuMTgtMC4xOSwwLjI5LTAuNDUsMC4yOS0wLjcxYzAtMC4yNzEtMC4xMS0wLjUyMS0wLjI5LTAuNzEgICBjLTAuMzctMC4zNy0xLjA1LTAuMzctMS40MiwwYy0wLjE4MSwwLjE4OS0wLjI5LDAuNDM5LTAuMjksMC43MWMwLDAuMjYsMC4xMDksMC41MiwwLjI5LDAuNzEgICBDMjUuNDc5LDI0Ljg4NywyNS43NCwyNC45OTcsMjYsMjQuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTMxLDMwLjk5N0gyMWMtMC41NTMsMC0xLDAuNDQ3LTEsMXMwLjQ0NywxLDEsMWgxMGMwLjU1MywwLDEtMC40NDcsMS0xUzMxLjU1MywzMC45OTcsMzEsMzAuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTE3LDMyLjk5N2MwLjI2LDAsMC41Mi0wLjExLDAuNzEtMC4yOWMwLjE4LTAuMTksMC4yOS0wLjQ1LDAuMjktMC43MXMtMC4xMDEtMC41MS0wLjI5LTAuNzFjLTAuMzctMC4zNy0xLjA0LTAuMzctMS40MiwwICAgQzE2LjEsMzEuNDc3LDE2LDMxLjcyNywxNiwzMS45OTdjMCwwLjI2LDAuMTA5LDAuNTIsMC4yOSwwLjcxQzE2LjQ3OSwzMi44OTcsMTYuNzQsMzIuOTk3LDE3LDMyLjk5N3oiIGZpbGw9IiMwMDAwMDAiLz4KCTxwYXRoIGQ9Ik00MiwyNC45OTdjMC4yNiwwLDAuNTItMC4xMSwwLjcxLTAuMjljMC4xOC0wLjE5LDAuMjktMC40NSwwLjI5LTAuNzFzLTAuMTEtMC41MjEtMC4yOS0wLjcxYy0wLjM4LTAuMzctMS4wNS0wLjM3LTEuNDIsMCAgIGMtMC4xODEsMC4xODktMC4yOSwwLjQzOS0wLjI5LDAuNzFjMCwwLjI2LDAuMTA5LDAuNTIsMC4yOSwwLjcxQzQxLjQ3OSwyNC44ODcsNDEuNzQsMjQuOTk3LDQyLDI0Ljk5N3oiIGZpbGw9IiMwMDAwMDAiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K");
                    img_icon_mail.Style.Add("width", "24px");
                    h4.Controls.Add(new Literal { Text = "&nbsp;" });
                    h4.Controls.Add(img_icon_mail);
                    //  <img src= />
                    HtmlGenericControl h_read = new HtmlGenericControl("h6");
                    h_read.InnerText = "อ่านแล้ว " + readnew.OrderBy(f => f.Read_Date).ToList()[0]
                        .Read_Date.Value.ToString(@"dd MMM yyyy HH:mm", new CultureInfo("th-TH"));
                    h4.Controls.Add(h_read);

                    div_media_body.Controls.Add(h_read);
                }

                //HtmlGenericControl span = new HtmlGenericControl("h6");
                //span.InnerHtml = "";

                //div_media_body.Controls.Add(span);
            }

            if (agentname == null)
            {
                agentname = "--";
            }

            string User_ID = Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class1 = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_AgentClass> agent = dbo_AgentDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            if (user_class1.User_Group_ID == "CP Meiji")
            {
                List<string> item = new List<string>(agent.Where(f => f.DM_ID == User_ID || f.GM_ID == User_ID.Trim() || f.SD_ID == User_ID.Trim() || f.SM_ID == User_ID.Trim()
                    || f.APV_ID == User_ID.Trim()).Where(f => f.Status == true).Select(f => f.AgentName));

                news_list = new List<dbo_NewsClass2>();

                foreach (string a_name in item)
                {
                    List<dbo_NewsClass2> itm = dbo_NewsDataClass2.Search("เอเยนต์", a_name);


                    foreach (dbo_NewsClass2 tmp_new in itm)
                    {
                        news_list.Add(tmp_new);
                    }
                }

                news_list = news_list.GroupBy(f => f.News_ID)
                  .Select(grp => grp.First())
                  .ToList();
            }
            else
            {
                news_list = dbo_NewsDataClass2.Search("เอเยนต์", agentname);
            }

            foreach (dbo_NewsClass2 news in news_list)
            {
                string image = string.Empty;
                if (news.Photo_MemoryStream != null)
                {
                    image = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(news.Photo_MemoryStream, 0, news.Photo_MemoryStream.Length));
                }
                else
                {
                    image = string.Format("data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBvbHlnb24gc3R5bGU9ImZpbGw6I0VDRTdFQTsiIHBvaW50cz0iNTEyLDczLjk1NiA1MTIsNDk0LjkzMyAwLDQ5NC45MzMgMCw3My45NTYgMjU2LDUxLjIgIi8+Cjxwb2x5Z29uIHN0eWxlPSJmaWxsOiNGMTQ3NDI7IiBwb2ludHM9IjUxMiwxNy4wNjcgNTEyLDczLjk1NiA0NTUuMTExLDczLjk1NiA0MjAuOTc4LDQ1LjUxMSA0NTUuMTExLDE3LjA2NyAiLz4KPHJlY3QgeT0iMTcuMDY3IiBzdHlsZT0iZmlsbDojMUI0MTQ1OyIgd2lkdGg9IjQ1NS4xMTEiIGhlaWdodD0iNTYuODg5Ii8+CjxwYXRoIHN0eWxlPSJmaWxsOiNENkNFRDE7IiBkPSJNNDQzLjczMywyNTguODQ0SDY4LjI2N2MtNC43MTQsMC04LjUzMy0zLjgyMS04LjUzMy04LjUzM2MwLTQuNzEzLDMuODItOC41MzMsOC41MzMtOC41MzNoMzc1LjQ2NyAgYzQuNzE0LDAsOC41MzMsMy44MjEsOC41MzMsOC41MzNDNDUyLjI2NywyNTUuMDI0LDQ0OC40NDcsMjU4Ljg0NCw0NDMuNzMzLDI1OC44NDR6Ii8+CjxnPgoJPHBhdGggc3R5bGU9ImZpbGw6IzhCN0U3RjsiIGQ9Ik0yMzMuMjQ0LDMwNC4zNTZINjguMjY3Yy00LjcxNCwwLTguNTMzLTMuODIxLTguNTMzLTguNTMzczMuODItOC41MzMsOC41MzMtOC41MzNoMTY0Ljk3OCAgIGM0LjcxNCwwLDguNTMzLDMuODIxLDguNTMzLDguNTMzUzIzNy45NTgsMzA0LjM1NiwyMzMuMjQ0LDMwNC4zNTZ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojOEI3RTdGOyIgZD0iTTIzMy4yNDQsMzQ5Ljg2N0g2OC4yNjdjLTQuNzE0LDAtOC41MzMtMy44MjEtOC41MzMtOC41MzNzMy44Mi04LjUzMyw4LjUzMy04LjUzM2gxNjQuOTc4ICAgYzQuNzE0LDAsOC41MzMsMy44MjEsOC41MzMsOC41MzNTMjM3Ljk1OCwzNDkuODY3LDIzMy4yNDQsMzQ5Ljg2N3oiLz4KCTxwYXRoIHN0eWxlPSJmaWxsOiM4QjdFN0Y7IiBkPSJNMjMzLjI0NCwzOTUuMzc4SDY4LjI2N2MtNC43MTQsMC04LjUzMy0zLjgyMS04LjUzMy04LjUzM3MzLjgyLTguNTMzLDguNTMzLTguNTMzaDE2NC45NzggICBjNC43MTQsMCw4LjUzMywzLjgyMSw4LjUzMyw4LjUzM1MyMzcuOTU4LDM5NS4zNzgsMjMzLjI0NCwzOTUuMzc4eiIvPgo8L2c+CjxyZWN0IHg9IjI3OC43NTYiIHk9IjI5NS44MjIiIHN0eWxlPSJmaWxsOiNGMTQ3NDI7IiB3aWR0aD0iMTY0Ljk3OCIgaGVpZ2h0PSIxMzYuNTMzIi8+CjxnPgoJPHBhdGggc3R5bGU9ImZpbGw6IzhCN0U3RjsiIGQ9Ik0yMzMuMjQ0LDQ0MC44ODlINjguMjY3Yy00LjcxNCwwLTguNTMzLTMuODIxLTguNTMzLTguNTMzczMuODItOC41MzMsOC41MzMtOC41MzNoMTY0Ljk3OCAgIGM0LjcxNCwwLDguNTMzLDMuODIxLDguNTMzLDguNTMzUzIzNy45NTgsNDQwLjg4OSwyMzMuMjQ0LDQ0MC44ODl6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojOEI3RTdGOyIgZD0iTTM5NS4wNCwyMTMuMzMzYy0xNS4zNjYsMC0yNy4zNDQtNi43MjUtMzEuMjYzLTE3LjU1Yy0xLjYwMy00LjQzMiwwLjY4OC05LjMyNCw1LjEyMS0xMC45MjcgICBjNC40MjYtMS42MDQsOS4zMjMsMC42ODcsMTAuOTI3LDUuMTJjMS4zNjEsMy43NjQsNy40NzUsNi4yOTIsMTUuMjEzLDYuMjkyYzUuNzg2LDAsMTUuNTc2LTEuNzk5LDE1LjU3Ni04LjU0ICAgYzAtNC42MTktMi4xMTEtNi4yMTYtMTYuOTU5LTguNjQ1Yy02LjU5OS0xLjA3OS0xMy40MjEtMi4xOTUtMTkuMTY5LTUuMjE5Yy03LjkxMS00LjE2My0xMi4wOTItMTEuMTY3LTEyLjA5Mi0yMC4yNTcgICBjMC0xNS4wNzcsMTMuNDIzLTI1LjYwNywzMi42NDMtMjUuNjA3YzE1LjM1OSwwLDI3LjMzNiw2LjcyMSwzMS4yNTksMTcuNTRjMS42MDcsNC40MzEtMC42ODQsOS4zMjQtNS4xMTQsMTAuOTMxICAgYy00LjQzNCwxLjYxMS05LjMyNC0wLjY4NS0xMC45MjktNS4xMTRjLTEuMzY0LTMuNzYxLTcuNDc3LTYuMjktMTUuMjE0LTYuMjljLTUuNzg2LDAtMTUuNTc2LDEuNzk5LTE1LjU3Niw4LjU0ICAgYzAsNC42MTEsMi4xMDksNi4yMDUsMTYuOTQ5LDguNjMyYzYuNiwxLjA4LDEzLjQyNiwyLjE5NiwxOS4xNzUsNS4yMjFjNy45MTMsNC4xNjQsMTIuMDk2LDExLjE3MiwxMi4wOTYsMjAuMjY2ICAgQzQyNy42ODQsMjAyLjgwMyw0MTQuMjYsMjEzLjMzMywzOTUuMDQsMjEzLjMzM3oiLz4KCTxwYXRoIHN0eWxlPSJmaWxsOiM4QjdFN0Y7IiBkPSJNMzE5LjQ5NiwyMTMuMzMzYy0zLjE4NywwLTYuMTQzLTEuNzg2LTcuNjA5LTQuNjc2bC05LjY4Ni0xOS4xMTRsLTkuNjg2LDE5LjExNCAgIGMtMS42MDgsMy4xNzQtNS4wMjksNS4wMjItOC41NTMsNC42MjRjLTMuNTM2LTAuMzkxLTYuNDU4LTIuOTM3LTcuMzMyLTYuMzg1bC0xNy4yOTgtNjguMjY3Yy0xLjE1OC00LjU2OCwxLjYwOC05LjIxLDYuMTc3LTEwLjM2NyAgIGM0LjU2Ny0xLjE1NSw5LjIxLDEuNjA3LDEwLjM2Nyw2LjE3NmwxMS43MDUsNDYuMTk3bDcuMDA1LTEzLjgyNWMxLjQ1NC0yLjg2OCw0LjM5Ni00LjY3Niw3LjYxMi00LjY3NiAgIGMzLjIxNSwwLDYuMTU4LDEuODA4LDcuNjEyLDQuNjc2bDcuMDA1LDEzLjgyNWwxMS43MDUtNDYuMTk3YzEuMTU5LTQuNTY5LDUuODAyLTcuMzMzLDEwLjM2Ny02LjE3NiAgIGM0LjU2OSwxLjE1Nyw3LjMzNCw1Ljc5OSw2LjE3NywxMC4zNjdsLTE3LjI5OCw2OC4yNjdjLTAuODc1LDMuNDQ5LTMuNzk2LDUuOTk0LTcuMzMyLDYuMzg1ICAgQzMyMC4xMjMsMjEzLjMxNiwzMTkuODA5LDIxMy4zMzMsMzE5LjQ5NiwyMTMuMzMzeiIvPgoJPHBhdGggc3R5bGU9ImZpbGw6IzhCN0U3RjsiIGQ9Ik0yMzMuNDcyLDIxMy4zMzNoLTQ4LjIyYy00LjcxNCwwLTguNTMzLTMuODIxLTguNTMzLTguNTMzdi02OC4yNjdjMC00LjcxMywzLjgyLTguNTMzLDguNTMzLTguNTMzICAgaDQ4LjIyYzQuNzE0LDAsOC41MzMsMy44MjEsOC41MzMsOC41MzNzLTMuODIsOC41MzMtOC41MzMsOC41MzNoLTM5LjY4N3Y1MS4yaDM5LjY4N2M0LjcxNCwwLDguNTMzLDMuODIxLDguNTMzLDguNTMzICAgUzIzOC4xODYsMjEzLjMzMywyMzMuNDcyLDIxMy4zMzN6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojOEI3RTdGOyIgZD0iTTIxNy4zOTksMTc5LjJoLTMyLjE0N2MtNC43MTQsMC04LjUzMy0zLjgyMS04LjUzMy04LjUzM3MzLjgyLTguNTMzLDguNTMzLTguNTMzaDMyLjE0NyAgIGM0LjcxNCwwLDguNTMzLDMuODIxLDguNTMzLDguNTMzUzIyMi4xMTIsMTc5LjIsMjE3LjM5OSwxNzkuMnoiLz4KCTxwYXRoIHN0eWxlPSJmaWxsOiM4QjdFN0Y7IiBkPSJNMTUxLjEyMSwyMTMuMzMzYy0yLjQ1LDAtNC44MzktMS4wNTctNi40OTItMi45OTNsLTQzLjI0Ni01MC42NjVWMjA0LjggICBjMCw0LjcxMy0zLjgyLDguNTMzLTguNTMzLDguNTMzYy00LjcxNCwwLTguNTMzLTMuODIxLTguNTMzLTguNTMzdi02OC4yNjdjMC0zLjU3NSwyLjIyOC02Ljc3LDUuNTgxLTguMDA3ICAgYzMuMzUyLTEuMjM4LDcuMTIyLTAuMjUzLDkuNDQyLDIuNDY2bDQzLjI0Niw1MC42NjV2LTQ1LjEyNGMwLTQuNzEzLDMuODItOC41MzMsOC41MzMtOC41MzNzOC41MzMsMy44MjEsOC41MzMsOC41MzNWMjA0LjggICBjMCwzLjU3NS0yLjIyOCw2Ljc3LTUuNTgxLDguMDA3QzE1My4xMDcsMjEzLjE2MiwxNTIuMTA4LDIxMy4zMzMsMTUxLjEyMSwyMTMuMzMzeiIvPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+Cjwvc3ZnPgo=");
                }

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "well");
                container_new_tab2.Controls.Add(div);

                HtmlGenericControl div_media = new HtmlGenericControl("div");
                div_media.Attributes.Add("class", "media");
                div.Controls.Add(div_media);
                HtmlGenericControl div_pull_left = new HtmlGenericControl("a");
                div_pull_left.Attributes.Add("class", "pull-left");
                div_pull_left.Attributes.Add("href", "#");
                div_media.Controls.Add(div_pull_left);

                Image img = new Image();
                img.Width = 150;
                img.Height = 100;

                img.Style.Add("border", "1px solid #d4d1d1");
                img.Style.Add("padding", "1px 1px 1px 1px");
                img.ImageUrl = image;

                div_pull_left.Controls.Add(img);
                HtmlGenericControl div_media_body = new HtmlGenericControl("div");
                div_media_body.Attributes.Add("class", "media-body");
                div_media.Controls.Add(div_media_body);

                HtmlGenericControl h4 = new HtmlGenericControl("h4");
                h4.Attributes.Add("class", "media-heading");

                HtmlGenericControl a = new HtmlGenericControl("a");
                a.Attributes.Add("href", "NewsPage.aspx?News_ID=" + news.News_ID);
                a.Attributes.Add("target", "_blank");
                a.InnerHtml = news.Subject;

                HtmlGenericControl img_icon_mail = new HtmlGenericControl("img");
                h4.Controls.Add(a);

                if ((DateTime.Now.Date - news.Start_Date.Value.Date).TotalDays < 30)
                {
                    HtmlGenericControl img_icon_new = new HtmlGenericControl("img");
                    img_icon_new.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTYuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgd2lkdGg9IjMycHgiIGhlaWdodD0iMzJweCIgdmlld0JveD0iMCAwIDQ4MS4wNDUgNDgxLjA0NSIgc3R5bGU9ImVuYWJsZS1iYWNrZ3JvdW5kOm5ldyAwIDAgNDgxLjA0NSA0ODEuMDQ1OyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSI+CjxnPgoJPHBhdGggZD0iTTQzNC44NzQsMjQxLjAzMWwzNS40MDUtMzYuMjkyYzQuNDAzLTQuNTEzLDYuMjI1LTEwLjk0Nyw0LjgzOC0xNy4wOTdjLTEuMzg3LTYuMTUtNS43OS0xMS4xODItMTEuNzA0LTEzLjM3ICAgbC00Ny41NDYtMTcuNTk1bDE2LjE2My00OC4wNTRjMi4wMTEtNS45NzYsMC44NTktMTIuNTYzLTMuMDU2LTE3LjUwNGMtMy45MTctNC45NC0xMC4wNy03LjU3Mi0xNi4zNDQtNi45NzFsLTUwLjQ3OSw0Ljc2NiAgIGwtNi4yNy01MC4zMDljLTAuNzc5LTYuMjU3LTQuNjcyLTExLjY5NC0xMC4zNDYtMTQuNDQ4Yy01LjY3MS0yLjc1My0xMi4zNTMtMi40NS0xNy43NSwwLjgwNkwyODMuOTcsNTEuMzkxTDI1Ni4wOTQsOC40NzcgICBDMjUyLjY1OSwzLjE5LDI0Ni43ODIsMCwyNDAuNDc4LDBjLTYuMzA0LDAtMTIuMTgyLDMuMTktMTUuNjE3LDguNDc4bC0yNy42MTYsNDIuNTE3bC00My4zMjItMjYuMzMzICAgYy01LjM4Ni0zLjI3NS0xMi4wNjUtMy42MDMtMTcuNzQ4LTAuODY4Yy01LjY4MSwyLjczNS05LjU5Miw4LjE1OC0xMC4zOTQsMTQuNDEybC02LjQ0OCw1MC4yODhsLTUwLjQ1Ny00Ljk0MiAgIGMtNi4yNzMtMC42MTktMTIuNDM1LDEuOTg4LTE2LjM2OCw2LjkxNWMtMy45MzMsNC45MjctNS4xMDYsMTEuNTEtMy4xMTcsMTcuNDkybDE1Ljk5NCw0OC4xMDlsLTQ3LjYwOSwxNy40MjkgICBjLTUuOTIsMi4xNjctMTAuMzQxLDcuMTg0LTExLjc0OCwxMy4zMjljLTEuNDA4LDYuMTQ2LDAuMzksMTIuNTg2LDQuNzc1LDE3LjExNGwzNS4yNzksMzYuNDE0bC0zNS4zNCwzNi4zNTEgICBjLTQuMzk0LDQuNTIxLTYuMjAzLDEwLjk1OC00LjgwOCwxNy4xMDRjMS4zOTgsNi4xNDgsNS44MTIsMTEuMTcyLDExLjcyOCwxMy4zNUw2NS4yNCwzMjQuNjdsLTE2LjA4Myw0OC4wODIgICBjLTIsNS45NzktMC44NCwxMi41NjQsMy4wODYsMTcuNDk4YzMuOTI1LDQuOTM2LDEwLjA4Miw3LjU0OSwxNi4zNTUsNi45NDNsNTAuNDY1LTQuODU0bDYuMzYyLDUwLjMwMSAgIGMwLjc5Miw2LjI1NCw0LjY5MiwxMS42ODQsMTAuMzY5LDE0LjQyOGM1LjY3NywyLjc0NiwxMi4zNTYsMi40MywxNy43NDgtMC44MzZsNDMuMzctMjYuMjU4bDI3LjU0NSw0Mi41NjYgICBjMy40MjUsNS4yOTMsOS4yOTcsOC40OTQsMTUuNjAxLDguNTA0YzAuMDEsMCwwLjAyMSwwLDAuMDMyLDBjNi4yOTMsMCwxMi4xNjItMy4xNzgsMTUuNTk4LTguNDQ5bDI3LjY5Mi00Mi40NjlsNDMuMjc5LDI2LjQxICAgYzUuMzgxLDMuMjg1LDEyLjA2LDMuNjIzLDE3Ljc0NSwwLjg5OGM1LjY4Ny0yLjcyNyw5LjYwNi04LjE0MywxMC40Mi0xNC4zOTVsNi41MzUtNTAuMjc3bDUwLjQ1MSw1LjAyOSAgIGM2LjI4MSwwLjYyNSwxMi40MzgtMS45NjcsMTYuMzgtNi44ODdjMy45NDEtNC45Miw1LjEyNi0xMS41LDMuMTQ2LTE3LjQ4NmwtMTUuOTEyLTQ4LjEzNWw0Ny42MzktMTcuMzQ4ICAgYzUuOTI0LTIuMTU2LDEwLjM1NC03LjE2NiwxMS43NzItMTMuMzA5cy0wLjM2OC0xMi41ODYtNC43NDctMTcuMTIyTDQzNC44NzQsMjQxLjAzMXogTTE4MC43ODUsMjgxLjkxNSAgIGMwLDMuMDI0LTIuNDUxLDUuNDc2LTUuNDc2LDUuNDc2aC0xNi4zNjZjLTEuOTUzLDAtMy43NTctMS4wMzktNC43MzctMi43MjlsLTE5LjE0OC0zMy4wMDggICBjLTMuMTMyLTUuMzgzLTguNjQ0LTE1LjE5LTEzLjY5MS0yNS44NjRjMC4zNzUsMTAuMTIyLDAuNTU4LDIwLjkwNSwwLjU1OCwzMi43NDh2MjMuMzc3YzAsMy4wMjQtMi40NTIsNS40NzYtNS40NzYsNS40NzZoLTExLjg5MSAgIGMtMy4wMjUsMC01LjQ3Ny0yLjQ1MS01LjQ3Ny01LjQ3NnYtODIuNzgxYzAtMy4wMjQsMi40NTItNS40NzcsNS40NzctNS40NzdoMTguOTI2YzEuOTMzLDAsMy43MjIsMS4wMiw0LjcwOCwyLjY4MWwxOC41NzIsMzEuMjggICBjNC4yNTEsNy4yMDQsOC41MTEsMTUuNDE0LDEyLjE2NywyMy40MDRjLTAuNjc5LTguNS0wLjk5LTE3Ljg4OC0wLjk5LTI5LjMxNXYtMjIuNTcxYzAtMy4wMjQsMi40NTEtNS40NzcsNS40NzYtNS40NzdoMTEuODkxICAgYzMuMDI1LDAsNS40NzYsMi40NTMsNS40NzYsNS40NzdMMTgwLjc4NSwyODEuOTE1TDE4MC43ODUsMjgxLjkxNXogTTI1NC45OTYsMjgxLjkxNWMwLDMuMDI0LTIuNDUyLDUuNDc2LTUuNDc3LDUuNDc2aC01Mi43MjcgICBjLTMuMDI0LDAtNS40NzctMi40NTEtNS40NzctNS40NzZ2LTgyLjc4MWMwLTMuMDI0LDIuNDUyLTUuNDc3LDUuNDc3LTUuNDc3aDUwLjc1NWMzLjAyNCwwLDUuNDc2LDIuNDUzLDUuNDc2LDUuNDc3djguOTggICBjMCwzLjAyNC0yLjQ1MSw1LjQ3Ny01LjQ3Niw1LjQ3N2gtMzEuNTZ2MTUuNjg5aDI5LjQ0NWMzLjAyNSwwLDUuNDc3LDIuNDUyLDUuNDc3LDUuNDc3djguODQ3YzAsMy4wMjQtMi40NTEsNS40NzYtNS40NzcsNS40NzYgICBoLTI5LjQ0NXYxOC4zNzloMzMuNTNjMy4wMjQsMCw1LjQ3NywyLjQ1Miw1LjQ3Nyw1LjQ3NkwyNTQuOTk2LDI4MS45MTVMMjU0Ljk5NiwyODEuOTE1eiBNMzgxLjc3MywyMDAuNTYzbC0yMi4zODYsODIuNzggICBjLTAuNjQ2LDIuMzg4LTIuODEyLDQuMDQ4LTUuMjg2LDQuMDQ4aC0xNy4wNTNjLTIuNiwwLTQuODQxLTEuODI4LTUuMzY0LTQuMzczbC03LjM4LTM1Ljg0MSAgIGMtMS4zMzItNi42NTUtMi40MjItMTIuNTEtMy4zMjMtMTguNjQ5Yy0xLjE4MSw2LjU4Ni0yLjM4NywxMi41MTUtMy45MTEsMTguNzAzbC04LjMwMiwzNS45MTYgICBjLTAuNTc0LDIuNDg0LTIuNzg3LDQuMjQ0LTUuMzM2LDQuMjQ0aC0xNy4zNDdjLTIuNTE3LDAtNC43MDgtMS43MTQtNS4zMTQtNC4xNTZsLTIwLjU4NS04Mi43NzljLTAuNDA3LTEuNjM1LTAuMDM5LTMuMzY3LDEtNC42OTUgICBjMS4wMzgtMS4zMjgsMi42My0yLjEwNCw0LjMxNC0yLjEwNGgxNC43MThjMi42MjUsMCw0Ljg4MiwxLjg2NCw1LjM3Nyw0LjQ0M2w2LjU3NSwzNC4yMTJjMS41MjEsNy43NTEsMi45NjcsMTUuOTU4LDQuMjM3LDIzLjM4NiAgIGMxLjI5OS03LjExMSwyLjktMTQuODM2LDQuOC0yMy42OTZsNy4zNjItMzQuMDI3YzAuNTQ1LTIuNTIsMi43NzQtNC4zMTksNS4zNTMtNC4zMTloMTYuNDQ3YzIuNjExLDAsNC44NTksMS44NDYsNS4zNzEsNC40MDcgICBsNi45NzcsMzUuMDI5YzEuMzc1LDYuOTE2LDIuNTY3LDEzLjY4OCwzLjcxOSwyMS4xNTNjMC44NC00Ljk2MywxLjc4Ni0xMC4wMjYsMi43NjUtMTUuMjY4bDAuMDYzLTAuMzM1ICAgYzAuNDA2LTIuMTc3LDAuODEyLTQuMzYyLDEuMjE2LTYuNTU0YzAuMDA3LTAuMDQ0LDAuMDE2LTAuMDg3LDAuMDI1LTAuMTNsNy4wODYtMzMuOTQ0YzAuNTMtMi41MzgsMi43NjgtNC4zNTgsNS4zNi00LjM1OGgxMy41MzYgICBjMS43MDUsMCwzLjMxMywwLjc5NCw0LjM1LDIuMTQ4QzM4MS44NzIsMTk3LjE1OSwzODIuMjE5LDE5OC45MTgsMzgxLjc3MywyMDAuNTYzeiIgZmlsbD0iIzAwMDAwMCIvPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+Cjwvc3ZnPgo=");
                    //h4.Controls.Add(new Literal { Text = "&nbsp;" });
                    h4.Controls.Add(img_icon_new);
                }


                div_media_body.Controls.Add(h4);

                HtmlGenericControl p = new HtmlGenericControl("p");
                p.InnerText = news.Content.Length > 120 ? news.Content.Substring(0, 118) : news.Content;

                HtmlGenericControl a1 = new HtmlGenericControl("a");
                a1.Attributes.Add("href", "NewsPage.aspx?News_ID=" + news.News_ID);
                a1.Attributes.Add("target", "_blank");
                a1.InnerHtml = "รายละเอียดเพิ่มเติม...";


                div_media_body.Controls.Add(p);
                div_media_body.Controls.Add(a1);


                List<dbo_ReadNewsClass> readnew = dbo_ReadNewsDataClass.Search(news.News_ID, HttpContext.Current.Request.Cookies["User_ID"].Value);
                if (readnew.Count == 0)
                {
                    //img_icon_mail.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDM3LjgwMSAzNy44MDEiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDM3LjgwMSAzNy44MDE7IiB4bWw6c3BhY2U9InByZXNlcnZlIiB3aWR0aD0iMzJweCIgaGVpZ2h0PSIzMnB4Ij4KPGc+Cgk8Zz4KCQk8Zz4KCQkJPHBhdGggZD0iTTI1LjEwOSwyOC4yOThjLTAuMTIzLDAtMC4yNDYtMC4wNDUtMC4zNDItMC4xMzZsLTUuNzU0LTUuMzk4Yy0wLjIwMi0wLjE4OC0wLjIxMi0wLjUwNS0wLjAyMi0wLjcwNiAgICAgYzAuMTg4LTAuMjAzLDAuNTA3LTAuMjExLDAuNzA2LTAuMDIybDUuNzU0LDUuMzk4YzAuMjAyLDAuMTg4LDAuMjEyLDAuNTA1LDAuMDIyLDAuNzA2ICAgICBDMjUuMzc2LDI4LjI0NSwyNS4yNDIsMjguMjk4LDI1LjEwOSwyOC4yOTh6IiBmaWxsPSIjMDAwMDAwIi8+CgkJCTxwYXRoIGQ9Ik01LjkwNCwyOC4yOThjLTAuMTMzLDAtMC4yNjctMC4wNTMtMC4zNjQtMC4xNThjLTAuMTg5LTAuMjAxLTAuMTgtMC41MTgsMC4wMjItMC43MDZsNS43NTQtNS40ICAgICBjMC4xOTktMC4xODgsMC41MTktMC4xODEsMC43MDYsMC4wMjJjMC4xODksMC4yMDEsMC4xOCwwLjUxOC0wLjAyMiwwLjcwNmwtNS43NTQsNS40QzYuMTUsMjguMjUzLDYuMDI3LDI4LjI5OCw1LjkwNCwyOC4yOTh6IiBmaWxsPSIjMDAwMDAwIi8+CgkJPC9nPgoJCTxwYXRoIGQ9Ik0yOC41MTIsMzMuMzE3SDIuNWMtMS4zNzksMC0yLjUtMS4xMjEtMi41LTIuNVYxMy43NjljMC0xLjM3OSwxLjEyMS0yLjUsMi41LTIuNWgyMS4yMjVjMC4yNzYsMCwwLjUsMC4yMjQsMC41LDAuNSAgICBzLTAuMjI0LDAuNS0wLjUsMC41SDIuNWMtMC44MjcsMC0xLjUsMC42NzMtMS41LDEuNXYxNy4wNDljMCwwLjgyNywwLjY3MywxLjUsMS41LDEuNWgyNi4wMTJjMC44MjcsMCwxLjUtMC42NzMsMS41LTEuNVYxOC41NTYgICAgYzAtMC4yNzYsMC4yMjQtMC41LDAuNS0wLjVzMC41LDAuMjI0LDAuNSwwLjV2MTIuMjYyQzMxLjAxMiwzMi4xOTYsMjkuODkxLDMzLjMxNywyOC41MTIsMzMuMzE3eiIgZmlsbD0iIzAwMDAwMCIvPgoJCTxwYXRoIGQ9Ik0xNS41MTIsMjQuODA2Yy0wLjY2NywwLTEuMzM1LTAuMjIyLTEuODQyLTAuNjY0TDAuODc3LDEyLjk4OGMtMC4yMDktMC4xODItMC4yMy0wLjQ5Ny0wLjA0OS0wLjcwNSAgICBjMC4xODItMC4yMTEsMC40OTctMC4yMzEsMC43MDUtMC4wNDlsMTIuNzkzLDExLjE1M2MwLjY0MiwwLjU1OCwxLjcyMywwLjU2LDIuMzY0LDAuMDA0bDguNTI5LTcuMzg2ICAgIGMwLjIwOS0wLjE4MSwwLjUyNC0wLjE1OCwwLjcwNSwwLjA1MXMwLjE1OCwwLjUyNC0wLjA1MSwwLjcwNWwtOC41MjksNy4zODZDMTYuODM5LDI0LjU4NiwxNi4xNzYsMjQuODA2LDE1LjUxMiwyNC44MDZ6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8L2c+Cgk8Zz4KCQk8cGF0aCBkPSJNMzAuNTE0LDE5LjA1NmMtNC4wMiwwLTcuMjg5LTMuMjY5LTcuMjg5LTcuMjg1YzAtNC4wMTksMy4yNy03LjI4Nyw3LjI4OS03LjI4N2M0LjAxOSwwLDcuMjg3LDMuMjY5LDcuMjg3LDcuMjg3ICAgIEMzNy44MDEsMTUuNzg3LDM0LjUzMiwxOS4wNTYsMzAuNTE0LDE5LjA1NnogTTMwLjUxNCw1LjQ4M2MtMy40NjgsMC02LjI4OSwyLjgyLTYuMjg5LDYuMjg3YzAsMy40NjYsMi44MjEsNi4yODUsNi4yODksNi4yODUgICAgYzMuNDY3LDAsNi4yODctMi44MTksNi4yODctNi4yODVDMzYuODAxLDguMzA0LDMzLjk4LDUuNDgzLDMwLjUxNCw1LjQ4M3oiIGZpbGw9IiMwMDAwMDAiLz4KCQk8cGF0aCBkPSJNMzMuNTg0LDEyLjI3MWgtNi4wMTZjLTAuMjc2LDAtMC41LTAuMjI0LTAuNS0wLjVzMC4yMjQtMC41LDAuNS0wLjVoNi4wMTZjMC4yNzYsMCwwLjUsMC4yMjQsMC41LDAuNSAgICBTMzMuODYsMTIuMjcxLDMzLjU4NCwxMi4yNzF6IiBmaWxsPSIjMDAwMDAwIi8+CgkJPHBhdGggZD0iTTMwLjU3NiwxNS4yNzhjLTAuMjc2LDAtMC41LTAuMjI0LTAuNS0wLjVWOC43NjVjMC0wLjI3NiwwLjIyNC0wLjUsMC41LTAuNXMwLjUsMC4yMjQsMC41LDAuNXY2LjAxNCAgICBDMzEuMDc2LDE1LjA1NSwzMC44NTMsMTUuMjc4LDMwLjU3NiwxNS4yNzh6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8L2c+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPC9zdmc+Cg==");

                    //h4.Controls.Add(img_icon_mail);
                }
                else
                {
                    img_icon_mail.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTguMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDYwIDYwIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA2MCA2MDsiIHhtbDpzcGFjZT0icHJlc2VydmUiIHdpZHRoPSIzMnB4IiBoZWlnaHQ9IjMycHgiPgo8Zz4KCTxwYXRoIGQ9Ik01OS45NzMsMjYuNTM1bC0wLjAwMy0wLjA1N2wtMC4xNDctMC4wODJMNTQsMjAuOTc1VjguOTk3SDQxLjEzNWwtOC41OTMtOGMtMS40MjYtMS4zMjYtMy42NTktMS4zMjctNS4wODUsMC4wMDEgICBsLTguNTkyLDcuOTk5SDZ2MTEuOTc4bC01Ljg4OSw1LjQ4MkwwLDI2LjUzMXYwLjAzdjMyLjMwOHYxLjEyOWgxLjEyOWg1Ni4xMjdoMS42MTVoMC40NDlINjB2LTEuMTI5VjI3LjEwMXYtMC41NEw1OS45NzMsMjYuNTM1eiAgICBNMzIuMDcyLDQyLjk4N2w5Ljk1Mi01LjQ4N0w1NCwzMC44OTd2MGw0LTIuMjA2djI4LjU5M2wtNC43MDYtMi41OTRMMzIuMDcyLDQyLjk4N3ogTTU3LjMxLDI2Ljc4OUw1NCwyOC42MTR2LTQuOTA3TDU3LjMxLDI2Ljc4OSAgIHogTTI4LjgxOSwyLjQ2MmMwLjY2Mi0wLjYxNSwxLjctMC42MTQsMi4zNi0wLjAwMWw3LjAyLDYuNTM2SDIxLjgwMUwyOC44MTksMi40NjJ6IE04LDEwLjk5N2g4LjcxN2gyNi41NjZINTJ2OC4xMTZ2MTAuNjAyICAgbC0xOC4xMDIsOS45ODJsLTMuODk3LDIuMTQ5TDgsMjkuNzE1VjE5LjExNFYxMC45OTd6IE02LDI4LjYxMmwtMy4zMDgtMS44MjRMNiwyMy43MDdWMjguNjEyeiBNMiw1Ny45OTdWMjguNjkxbDQsMi4yMDZ2MCAgIGwxNy44Miw5LjgyNWw0LjEwOSwyLjI2NmwwLDBsMTAuMzE2LDUuNjg4bDE2LjkwNyw5LjMyMkgyeiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTI3LDE3Ljk5N2g2YzAuNTUzLDAsMS0wLjQ0NywxLTFzLTAuNDQ3LTEtMS0xaC02Yy0wLjU1MywwLTEsMC40NDctMSwxUzI2LjQ0NywxNy45OTcsMjcsMTcuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTQyLDMwLjk5N2gtN2MtMC41NTMsMC0xLDAuNDQ3LTEsMXMwLjQ0NywxLDEsMWg3YzAuNTUzLDAsMS0wLjQ0NywxLTFTNDIuNTUzLDMwLjk5Nyw0MiwzMC45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMTcsMjQuOTk3aDVjMC41NTMsMCwxLTAuNDQ3LDEtMXMtMC40NDctMS0xLTFoLTVjLTAuNTUzLDAtMSwwLjQ0Ny0xLDFTMTYuNDQ3LDI0Ljk5NywxNywyNC45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMjksMjMuOTk3YzAsMC41NTMsMC40NDcsMSwxLDFoOGMwLjU1MywwLDEtMC40NDcsMS0xcy0wLjQ0Ny0xLTEtMWgtOEMyOS40NDcsMjIuOTk3LDI5LDIzLjQ0NSwyOSwyMy45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMjYsMjQuOTk3YzAuMjYsMCwwLjUyLTAuMTEsMC43MS0wLjI5YzAuMTgtMC4xOSwwLjI5LTAuNDUsMC4yOS0wLjcxYzAtMC4yNzEtMC4xMS0wLjUyMS0wLjI5LTAuNzEgICBjLTAuMzctMC4zNy0xLjA1LTAuMzctMS40MiwwYy0wLjE4MSwwLjE4OS0wLjI5LDAuNDM5LTAuMjksMC43MWMwLDAuMjYsMC4xMDksMC41MiwwLjI5LDAuNzEgICBDMjUuNDc5LDI0Ljg4NywyNS43NCwyNC45OTcsMjYsMjQuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTMxLDMwLjk5N0gyMWMtMC41NTMsMC0xLDAuNDQ3LTEsMXMwLjQ0NywxLDEsMWgxMGMwLjU1MywwLDEtMC40NDcsMS0xUzMxLjU1MywzMC45OTcsMzEsMzAuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTE3LDMyLjk5N2MwLjI2LDAsMC41Mi0wLjExLDAuNzEtMC4yOWMwLjE4LTAuMTksMC4yOS0wLjQ1LDAuMjktMC43MXMtMC4xMDEtMC41MS0wLjI5LTAuNzFjLTAuMzctMC4zNy0xLjA0LTAuMzctMS40MiwwICAgQzE2LjEsMzEuNDc3LDE2LDMxLjcyNywxNiwzMS45OTdjMCwwLjI2LDAuMTA5LDAuNTIsMC4yOSwwLjcxQzE2LjQ3OSwzMi44OTcsMTYuNzQsMzIuOTk3LDE3LDMyLjk5N3oiIGZpbGw9IiMwMDAwMDAiLz4KCTxwYXRoIGQ9Ik00MiwyNC45OTdjMC4yNiwwLDAuNTItMC4xMSwwLjcxLTAuMjljMC4xOC0wLjE5LDAuMjktMC40NSwwLjI5LTAuNzFzLTAuMTEtMC41MjEtMC4yOS0wLjcxYy0wLjM4LTAuMzctMS4wNS0wLjM3LTEuNDIsMCAgIGMtMC4xODEsMC4xODktMC4yOSwwLjQzOS0wLjI5LDAuNzFjMCwwLjI2LDAuMTA5LDAuNTIsMC4yOSwwLjcxQzQxLjQ3OSwyNC44ODcsNDEuNzQsMjQuOTk3LDQyLDI0Ljk5N3oiIGZpbGw9IiMwMDAwMDAiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K");
                    img_icon_mail.Style.Add("width", "24px");
                    h4.Controls.Add(new Literal { Text = "&nbsp;" });
                    h4.Controls.Add(img_icon_mail);
                    //  <img src= />
                    HtmlGenericControl h_read = new HtmlGenericControl("h6");
                    h_read.InnerText = "อ่านแล้ว " + readnew.OrderBy(f => f.Read_Date).ToList()[0]
                        .Read_Date.Value.ToString(@"dd MMM yyyy HH:mm", new CultureInfo("th-TH"));
                    h4.Controls.Add(h_read);

                    div_media_body.Controls.Add(h_read);
                }


                /*

                HtmlGenericControl div_media = new HtmlGenericControl("div");
                div_media.Attributes.Add("class", "media");
                div.Controls.Add(div_media);
                HtmlGenericControl div_pull_left = new HtmlGenericControl("a");
                div_pull_left.Attributes.Add("class", "pull-left");
                div_pull_left.Attributes.Add("href", "#");
                div_media.Controls.Add(div_pull_left);

                Image img = new Image();
                img.Width = 150;
                img.Height = 100;

                img.Style.Add("border", "1px solid #d4d1d1");
                img.Style.Add("padding", "1px 1px 1px 1px");
                img.ImageUrl = image;

                div_pull_left.Controls.Add(img);
                HtmlGenericControl div_media_body = new HtmlGenericControl("div");
                div_media_body.Attributes.Add("class", "media-body");
                div_media.Controls.Add(div_media_body);

                HtmlGenericControl h4 = new HtmlGenericControl("h4");
                h4.Attributes.Add("class", "media-heading");

                HtmlGenericControl a = new HtmlGenericControl("a");
                a.Attributes.Add("href", "NewsPage.aspx?News_ID=" + news.News_ID);
                a.Attributes.Add("target", "_blank");
                a.InnerHtml = news.Subject;
                HtmlGenericControl img_icon_new = new HtmlGenericControl("img");
                img_icon_new.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTYuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgd2lkdGg9IjMycHgiIGhlaWdodD0iMzJweCIgdmlld0JveD0iMCAwIDQ4MS4wNDUgNDgxLjA0NSIgc3R5bGU9ImVuYWJsZS1iYWNrZ3JvdW5kOm5ldyAwIDAgNDgxLjA0NSA0ODEuMDQ1OyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSI+CjxnPgoJPHBhdGggZD0iTTQzNC44NzQsMjQxLjAzMWwzNS40MDUtMzYuMjkyYzQuNDAzLTQuNTEzLDYuMjI1LTEwLjk0Nyw0LjgzOC0xNy4wOTdjLTEuMzg3LTYuMTUtNS43OS0xMS4xODItMTEuNzA0LTEzLjM3ICAgbC00Ny41NDYtMTcuNTk1bDE2LjE2My00OC4wNTRjMi4wMTEtNS45NzYsMC44NTktMTIuNTYzLTMuMDU2LTE3LjUwNGMtMy45MTctNC45NC0xMC4wNy03LjU3Mi0xNi4zNDQtNi45NzFsLTUwLjQ3OSw0Ljc2NiAgIGwtNi4yNy01MC4zMDljLTAuNzc5LTYuMjU3LTQuNjcyLTExLjY5NC0xMC4zNDYtMTQuNDQ4Yy01LjY3MS0yLjc1My0xMi4zNTMtMi40NS0xNy43NSwwLjgwNkwyODMuOTcsNTEuMzkxTDI1Ni4wOTQsOC40NzcgICBDMjUyLjY1OSwzLjE5LDI0Ni43ODIsMCwyNDAuNDc4LDBjLTYuMzA0LDAtMTIuMTgyLDMuMTktMTUuNjE3LDguNDc4bC0yNy42MTYsNDIuNTE3bC00My4zMjItMjYuMzMzICAgYy01LjM4Ni0zLjI3NS0xMi4wNjUtMy42MDMtMTcuNzQ4LTAuODY4Yy01LjY4MSwyLjczNS05LjU5Miw4LjE1OC0xMC4zOTQsMTQuNDEybC02LjQ0OCw1MC4yODhsLTUwLjQ1Ny00Ljk0MiAgIGMtNi4yNzMtMC42MTktMTIuNDM1LDEuOTg4LTE2LjM2OCw2LjkxNWMtMy45MzMsNC45MjctNS4xMDYsMTEuNTEtMy4xMTcsMTcuNDkybDE1Ljk5NCw0OC4xMDlsLTQ3LjYwOSwxNy40MjkgICBjLTUuOTIsMi4xNjctMTAuMzQxLDcuMTg0LTExLjc0OCwxMy4zMjljLTEuNDA4LDYuMTQ2LDAuMzksMTIuNTg2LDQuNzc1LDE3LjExNGwzNS4yNzksMzYuNDE0bC0zNS4zNCwzNi4zNTEgICBjLTQuMzk0LDQuNTIxLTYuMjAzLDEwLjk1OC00LjgwOCwxNy4xMDRjMS4zOTgsNi4xNDgsNS44MTIsMTEuMTcyLDExLjcyOCwxMy4zNUw2NS4yNCwzMjQuNjdsLTE2LjA4Myw0OC4wODIgICBjLTIsNS45NzktMC44NCwxMi41NjQsMy4wODYsMTcuNDk4YzMuOTI1LDQuOTM2LDEwLjA4Miw3LjU0OSwxNi4zNTUsNi45NDNsNTAuNDY1LTQuODU0bDYuMzYyLDUwLjMwMSAgIGMwLjc5Miw2LjI1NCw0LjY5MiwxMS42ODQsMTAuMzY5LDE0LjQyOGM1LjY3NywyLjc0NiwxMi4zNTYsMi40MywxNy43NDgtMC44MzZsNDMuMzctMjYuMjU4bDI3LjU0NSw0Mi41NjYgICBjMy40MjUsNS4yOTMsOS4yOTcsOC40OTQsMTUuNjAxLDguNTA0YzAuMDEsMCwwLjAyMSwwLDAuMDMyLDBjNi4yOTMsMCwxMi4xNjItMy4xNzgsMTUuNTk4LTguNDQ5bDI3LjY5Mi00Mi40NjlsNDMuMjc5LDI2LjQxICAgYzUuMzgxLDMuMjg1LDEyLjA2LDMuNjIzLDE3Ljc0NSwwLjg5OGM1LjY4Ny0yLjcyNyw5LjYwNi04LjE0MywxMC40Mi0xNC4zOTVsNi41MzUtNTAuMjc3bDUwLjQ1MSw1LjAyOSAgIGM2LjI4MSwwLjYyNSwxMi40MzgtMS45NjcsMTYuMzgtNi44ODdjMy45NDEtNC45Miw1LjEyNi0xMS41LDMuMTQ2LTE3LjQ4NmwtMTUuOTEyLTQ4LjEzNWw0Ny42MzktMTcuMzQ4ICAgYzUuOTI0LTIuMTU2LDEwLjM1NC03LjE2NiwxMS43NzItMTMuMzA5cy0wLjM2OC0xMi41ODYtNC43NDctMTcuMTIyTDQzNC44NzQsMjQxLjAzMXogTTE4MC43ODUsMjgxLjkxNSAgIGMwLDMuMDI0LTIuNDUxLDUuNDc2LTUuNDc2LDUuNDc2aC0xNi4zNjZjLTEuOTUzLDAtMy43NTctMS4wMzktNC43MzctMi43MjlsLTE5LjE0OC0zMy4wMDggICBjLTMuMTMyLTUuMzgzLTguNjQ0LTE1LjE5LTEzLjY5MS0yNS44NjRjMC4zNzUsMTAuMTIyLDAuNTU4LDIwLjkwNSwwLjU1OCwzMi43NDh2MjMuMzc3YzAsMy4wMjQtMi40NTIsNS40NzYtNS40NzYsNS40NzZoLTExLjg5MSAgIGMtMy4wMjUsMC01LjQ3Ny0yLjQ1MS01LjQ3Ny01LjQ3NnYtODIuNzgxYzAtMy4wMjQsMi40NTItNS40NzcsNS40NzctNS40NzdoMTguOTI2YzEuOTMzLDAsMy43MjIsMS4wMiw0LjcwOCwyLjY4MWwxOC41NzIsMzEuMjggICBjNC4yNTEsNy4yMDQsOC41MTEsMTUuNDE0LDEyLjE2NywyMy40MDRjLTAuNjc5LTguNS0wLjk5LTE3Ljg4OC0wLjk5LTI5LjMxNXYtMjIuNTcxYzAtMy4wMjQsMi40NTEtNS40NzcsNS40NzYtNS40NzdoMTEuODkxICAgYzMuMDI1LDAsNS40NzYsMi40NTMsNS40NzYsNS40NzdMMTgwLjc4NSwyODEuOTE1TDE4MC43ODUsMjgxLjkxNXogTTI1NC45OTYsMjgxLjkxNWMwLDMuMDI0LTIuNDUyLDUuNDc2LTUuNDc3LDUuNDc2aC01Mi43MjcgICBjLTMuMDI0LDAtNS40NzctMi40NTEtNS40NzctNS40NzZ2LTgyLjc4MWMwLTMuMDI0LDIuNDUyLTUuNDc3LDUuNDc3LTUuNDc3aDUwLjc1NWMzLjAyNCwwLDUuNDc2LDIuNDUzLDUuNDc2LDUuNDc3djguOTggICBjMCwzLjAyNC0yLjQ1MSw1LjQ3Ny01LjQ3Niw1LjQ3N2gtMzEuNTZ2MTUuNjg5aDI5LjQ0NWMzLjAyNSwwLDUuNDc3LDIuNDUyLDUuNDc3LDUuNDc3djguODQ3YzAsMy4wMjQtMi40NTEsNS40NzYtNS40NzcsNS40NzYgICBoLTI5LjQ0NXYxOC4zNzloMzMuNTNjMy4wMjQsMCw1LjQ3NywyLjQ1Miw1LjQ3Nyw1LjQ3NkwyNTQuOTk2LDI4MS45MTVMMjU0Ljk5NiwyODEuOTE1eiBNMzgxLjc3MywyMDAuNTYzbC0yMi4zODYsODIuNzggICBjLTAuNjQ2LDIuMzg4LTIuODEyLDQuMDQ4LTUuMjg2LDQuMDQ4aC0xNy4wNTNjLTIuNiwwLTQuODQxLTEuODI4LTUuMzY0LTQuMzczbC03LjM4LTM1Ljg0MSAgIGMtMS4zMzItNi42NTUtMi40MjItMTIuNTEtMy4zMjMtMTguNjQ5Yy0xLjE4MSw2LjU4Ni0yLjM4NywxMi41MTUtMy45MTEsMTguNzAzbC04LjMwMiwzNS45MTYgICBjLTAuNTc0LDIuNDg0LTIuNzg3LDQuMjQ0LTUuMzM2LDQuMjQ0aC0xNy4zNDdjLTIuNTE3LDAtNC43MDgtMS43MTQtNS4zMTQtNC4xNTZsLTIwLjU4NS04Mi43NzljLTAuNDA3LTEuNjM1LTAuMDM5LTMuMzY3LDEtNC42OTUgICBjMS4wMzgtMS4zMjgsMi42My0yLjEwNCw0LjMxNC0yLjEwNGgxNC43MThjMi42MjUsMCw0Ljg4MiwxLjg2NCw1LjM3Nyw0LjQ0M2w2LjU3NSwzNC4yMTJjMS41MjEsNy43NTEsMi45NjcsMTUuOTU4LDQuMjM3LDIzLjM4NiAgIGMxLjI5OS03LjExMSwyLjktMTQuODM2LDQuOC0yMy42OTZsNy4zNjItMzQuMDI3YzAuNTQ1LTIuNTIsMi43NzQtNC4zMTksNS4zNTMtNC4zMTloMTYuNDQ3YzIuNjExLDAsNC44NTksMS44NDYsNS4zNzEsNC40MDcgICBsNi45NzcsMzUuMDI5YzEuMzc1LDYuOTE2LDIuNTY3LDEzLjY4OCwzLjcxOSwyMS4xNTNjMC44NC00Ljk2MywxLjc4Ni0xMC4wMjYsMi43NjUtMTUuMjY4bDAuMDYzLTAuMzM1ICAgYzAuNDA2LTIuMTc3LDAuODEyLTQuMzYyLDEuMjE2LTYuNTU0YzAuMDA3LTAuMDQ0LDAuMDE2LTAuMDg3LDAuMDI1LTAuMTNsNy4wODYtMzMuOTQ0YzAuNTMtMi41MzgsMi43NjgtNC4zNTgsNS4zNi00LjM1OGgxMy41MzYgICBjMS43MDUsMCwzLjMxMywwLjc5NCw0LjM1LDIuMTQ4QzM4MS44NzIsMTk3LjE1OSwzODIuMjE5LDE5OC45MTgsMzgxLjc3MywyMDAuNTYzeiIgZmlsbD0iIzAwMDAwMCIvPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+CjxnPgo8L2c+Cjwvc3ZnPgo=");

                HtmlGenericControl img_icon_mail = new HtmlGenericControl("img");
                //img_icon_mail.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDM3LjgwMSAzNy44MDEiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDM3LjgwMSAzNy44MDE7IiB4bWw6c3BhY2U9InByZXNlcnZlIiB3aWR0aD0iMzJweCIgaGVpZ2h0PSIzMnB4Ij4KPGc+Cgk8Zz4KCQk8Zz4KCQkJPHBhdGggZD0iTTI1LjEwOSwyOC4yOThjLTAuMTIzLDAtMC4yNDYtMC4wNDUtMC4zNDItMC4xMzZsLTUuNzU0LTUuMzk4Yy0wLjIwMi0wLjE4OC0wLjIxMi0wLjUwNS0wLjAyMi0wLjcwNiAgICAgYzAuMTg4LTAuMjAzLDAuNTA3LTAuMjExLDAuNzA2LTAuMDIybDUuNzU0LDUuMzk4YzAuMjAyLDAuMTg4LDAuMjEyLDAuNTA1LDAuMDIyLDAuNzA2ICAgICBDMjUuMzc2LDI4LjI0NSwyNS4yNDIsMjguMjk4LDI1LjEwOSwyOC4yOTh6IiBmaWxsPSIjMDAwMDAwIi8+CgkJCTxwYXRoIGQ9Ik01LjkwNCwyOC4yOThjLTAuMTMzLDAtMC4yNjctMC4wNTMtMC4zNjQtMC4xNThjLTAuMTg5LTAuMjAxLTAuMTgtMC41MTgsMC4wMjItMC43MDZsNS43NTQtNS40ICAgICBjMC4xOTktMC4xODgsMC41MTktMC4xODEsMC43MDYsMC4wMjJjMC4xODksMC4yMDEsMC4xOCwwLjUxOC0wLjAyMiwwLjcwNmwtNS43NTQsNS40QzYuMTUsMjguMjUzLDYuMDI3LDI4LjI5OCw1LjkwNCwyOC4yOTh6IiBmaWxsPSIjMDAwMDAwIi8+CgkJPC9nPgoJCTxwYXRoIGQ9Ik0yOC41MTIsMzMuMzE3SDIuNWMtMS4zNzksMC0yLjUtMS4xMjEtMi41LTIuNVYxMy43NjljMC0xLjM3OSwxLjEyMS0yLjUsMi41LTIuNWgyMS4yMjVjMC4yNzYsMCwwLjUsMC4yMjQsMC41LDAuNSAgICBzLTAuMjI0LDAuNS0wLjUsMC41SDIuNWMtMC44MjcsMC0xLjUsMC42NzMtMS41LDEuNXYxNy4wNDljMCwwLjgyNywwLjY3MywxLjUsMS41LDEuNWgyNi4wMTJjMC44MjcsMCwxLjUtMC42NzMsMS41LTEuNVYxOC41NTYgICAgYzAtMC4yNzYsMC4yMjQtMC41LDAuNS0wLjVzMC41LDAuMjI0LDAuNSwwLjV2MTIuMjYyQzMxLjAxMiwzMi4xOTYsMjkuODkxLDMzLjMxNywyOC41MTIsMzMuMzE3eiIgZmlsbD0iIzAwMDAwMCIvPgoJCTxwYXRoIGQ9Ik0xNS41MTIsMjQuODA2Yy0wLjY2NywwLTEuMzM1LTAuMjIyLTEuODQyLTAuNjY0TDAuODc3LDEyLjk4OGMtMC4yMDktMC4xODItMC4yMy0wLjQ5Ny0wLjA0OS0wLjcwNSAgICBjMC4xODItMC4yMTEsMC40OTctMC4yMzEsMC43MDUtMC4wNDlsMTIuNzkzLDExLjE1M2MwLjY0MiwwLjU1OCwxLjcyMywwLjU2LDIuMzY0LDAuMDA0bDguNTI5LTcuMzg2ICAgIGMwLjIwOS0wLjE4MSwwLjUyNC0wLjE1OCwwLjcwNSwwLjA1MXMwLjE1OCwwLjUyNC0wLjA1MSwwLjcwNWwtOC41MjksNy4zODZDMTYuODM5LDI0LjU4NiwxNi4xNzYsMjQuODA2LDE1LjUxMiwyNC44MDZ6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8L2c+Cgk8Zz4KCQk8cGF0aCBkPSJNMzAuNTE0LDE5LjA1NmMtNC4wMiwwLTcuMjg5LTMuMjY5LTcuMjg5LTcuMjg1YzAtNC4wMTksMy4yNy03LjI4Nyw3LjI4OS03LjI4N2M0LjAxOSwwLDcuMjg3LDMuMjY5LDcuMjg3LDcuMjg3ICAgIEMzNy44MDEsMTUuNzg3LDM0LjUzMiwxOS4wNTYsMzAuNTE0LDE5LjA1NnogTTMwLjUxNCw1LjQ4M2MtMy40NjgsMC02LjI4OSwyLjgyLTYuMjg5LDYuMjg3YzAsMy40NjYsMi44MjEsNi4yODUsNi4yODksNi4yODUgICAgYzMuNDY3LDAsNi4yODctMi44MTksNi4yODctNi4yODVDMzYuODAxLDguMzA0LDMzLjk4LDUuNDgzLDMwLjUxNCw1LjQ4M3oiIGZpbGw9IiMwMDAwMDAiLz4KCQk8cGF0aCBkPSJNMzMuNTg0LDEyLjI3MWgtNi4wMTZjLTAuMjc2LDAtMC41LTAuMjI0LTAuNS0wLjVzMC4yMjQtMC41LDAuNS0wLjVoNi4wMTZjMC4yNzYsMCwwLjUsMC4yMjQsMC41LDAuNSAgICBTMzMuODYsMTIuMjcxLDMzLjU4NCwxMi4yNzF6IiBmaWxsPSIjMDAwMDAwIi8+CgkJPHBhdGggZD0iTTMwLjU3NiwxNS4yNzhjLTAuMjc2LDAtMC41LTAuMjI0LTAuNS0wLjVWOC43NjVjMC0wLjI3NiwwLjIyNC0wLjUsMC41LTAuNXMwLjUsMC4yMjQsMC41LDAuNXY2LjAxNCAgICBDMzEuMDc2LDE1LjA1NSwzMC44NTMsMTUuMjc4LDMwLjU3NiwxNS4yNzh6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8L2c+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPC9zdmc+Cg==");
                //h4.Controls.Add(a);
                //h4.Controls.Add(img_icon_new);

                //List<dbo_ReadNewsClass> readnew = dbo_ReadNewsDataClass.Search(news.News_ID, HttpContext.Current.Request.Cookies["User_ID"].Value);

                div_media_body.Controls.Add(h4);

                HtmlGenericControl p = new HtmlGenericControl("p");
                p.InnerText = news.Content.Length > 120 ? news.Content.Substring(0, 118) : news.Content;
                div_media_body.Controls.Add(p);

                List<dbo_ReadNewsClass> readnew = dbo_ReadNewsDataClass.Search(news.News_ID, HttpContext.Current.Request.Cookies["User_ID"].Value);

                if (readnew.Count == 0)
                {
                    //img_icon_mail.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDM3LjgwMSAzNy44MDEiIHN0eWxlPSJlbmFibGUtYmFja2dyb3VuZDpuZXcgMCAwIDM3LjgwMSAzNy44MDE7IiB4bWw6c3BhY2U9InByZXNlcnZlIiB3aWR0aD0iMzJweCIgaGVpZ2h0PSIzMnB4Ij4KPGc+Cgk8Zz4KCQk8Zz4KCQkJPHBhdGggZD0iTTI1LjEwOSwyOC4yOThjLTAuMTIzLDAtMC4yNDYtMC4wNDUtMC4zNDItMC4xMzZsLTUuNzU0LTUuMzk4Yy0wLjIwMi0wLjE4OC0wLjIxMi0wLjUwNS0wLjAyMi0wLjcwNiAgICAgYzAuMTg4LTAuMjAzLDAuNTA3LTAuMjExLDAuNzA2LTAuMDIybDUuNzU0LDUuMzk4YzAuMjAyLDAuMTg4LDAuMjEyLDAuNTA1LDAuMDIyLDAuNzA2ICAgICBDMjUuMzc2LDI4LjI0NSwyNS4yNDIsMjguMjk4LDI1LjEwOSwyOC4yOTh6IiBmaWxsPSIjMDAwMDAwIi8+CgkJCTxwYXRoIGQ9Ik01LjkwNCwyOC4yOThjLTAuMTMzLDAtMC4yNjctMC4wNTMtMC4zNjQtMC4xNThjLTAuMTg5LTAuMjAxLTAuMTgtMC41MTgsMC4wMjItMC43MDZsNS43NTQtNS40ICAgICBjMC4xOTktMC4xODgsMC41MTktMC4xODEsMC43MDYsMC4wMjJjMC4xODksMC4yMDEsMC4xOCwwLjUxOC0wLjAyMiwwLjcwNmwtNS43NTQsNS40QzYuMTUsMjguMjUzLDYuMDI3LDI4LjI5OCw1LjkwNCwyOC4yOTh6IiBmaWxsPSIjMDAwMDAwIi8+CgkJPC9nPgoJCTxwYXRoIGQ9Ik0yOC41MTIsMzMuMzE3SDIuNWMtMS4zNzksMC0yLjUtMS4xMjEtMi41LTIuNVYxMy43NjljMC0xLjM3OSwxLjEyMS0yLjUsMi41LTIuNWgyMS4yMjVjMC4yNzYsMCwwLjUsMC4yMjQsMC41LDAuNSAgICBzLTAuMjI0LDAuNS0wLjUsMC41SDIuNWMtMC44MjcsMC0xLjUsMC42NzMtMS41LDEuNXYxNy4wNDljMCwwLjgyNywwLjY3MywxLjUsMS41LDEuNWgyNi4wMTJjMC44MjcsMCwxLjUtMC42NzMsMS41LTEuNVYxOC41NTYgICAgYzAtMC4yNzYsMC4yMjQtMC41LDAuNS0wLjVzMC41LDAuMjI0LDAuNSwwLjV2MTIuMjYyQzMxLjAxMiwzMi4xOTYsMjkuODkxLDMzLjMxNywyOC41MTIsMzMuMzE3eiIgZmlsbD0iIzAwMDAwMCIvPgoJCTxwYXRoIGQ9Ik0xNS41MTIsMjQuODA2Yy0wLjY2NywwLTEuMzM1LTAuMjIyLTEuODQyLTAuNjY0TDAuODc3LDEyLjk4OGMtMC4yMDktMC4xODItMC4yMy0wLjQ5Ny0wLjA0OS0wLjcwNSAgICBjMC4xODItMC4yMTEsMC40OTctMC4yMzEsMC43MDUtMC4wNDlsMTIuNzkzLDExLjE1M2MwLjY0MiwwLjU1OCwxLjcyMywwLjU2LDIuMzY0LDAuMDA0bDguNTI5LTcuMzg2ICAgIGMwLjIwOS0wLjE4MSwwLjUyNC0wLjE1OCwwLjcwNSwwLjA1MXMwLjE1OCwwLjUyNC0wLjA1MSwwLjcwNWwtOC41MjksNy4zODZDMTYuODM5LDI0LjU4NiwxNi4xNzYsMjQuODA2LDE1LjUxMiwyNC44MDZ6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8L2c+Cgk8Zz4KCQk8cGF0aCBkPSJNMzAuNTE0LDE5LjA1NmMtNC4wMiwwLTcuMjg5LTMuMjY5LTcuMjg5LTcuMjg1YzAtNC4wMTksMy4yNy03LjI4Nyw3LjI4OS03LjI4N2M0LjAxOSwwLDcuMjg3LDMuMjY5LDcuMjg3LDcuMjg3ICAgIEMzNy44MDEsMTUuNzg3LDM0LjUzMiwxOS4wNTYsMzAuNTE0LDE5LjA1NnogTTMwLjUxNCw1LjQ4M2MtMy40NjgsMC02LjI4OSwyLjgyLTYuMjg5LDYuMjg3YzAsMy40NjYsMi44MjEsNi4yODUsNi4yODksNi4yODUgICAgYzMuNDY3LDAsNi4yODctMi44MTksNi4yODctNi4yODVDMzYuODAxLDguMzA0LDMzLjk4LDUuNDgzLDMwLjUxNCw1LjQ4M3oiIGZpbGw9IiMwMDAwMDAiLz4KCQk8cGF0aCBkPSJNMzMuNTg0LDEyLjI3MWgtNi4wMTZjLTAuMjc2LDAtMC41LTAuMjI0LTAuNS0wLjVzMC4yMjQtMC41LDAuNS0wLjVoNi4wMTZjMC4yNzYsMCwwLjUsMC4yMjQsMC41LDAuNSAgICBTMzMuODYsMTIuMjcxLDMzLjU4NCwxMi4yNzF6IiBmaWxsPSIjMDAwMDAwIi8+CgkJPHBhdGggZD0iTTMwLjU3NiwxNS4yNzhjLTAuMjc2LDAtMC41LTAuMjI0LTAuNS0wLjVWOC43NjVjMC0wLjI3NiwwLjIyNC0wLjUsMC41LTAuNXMwLjUsMC4yMjQsMC41LDAuNXY2LjAxNCAgICBDMzEuMDc2LDE1LjA1NSwzMC44NTMsMTUuMjc4LDMwLjU3NiwxNS4yNzh6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8L2c+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPC9zdmc+Cg==");

                    //h4.Controls.Add(img_icon_mail);
                }
                else
                {
                    img_icon_mail.Attributes.Add("src", "data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTguMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDYwIDYwIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA2MCA2MDsiIHhtbDpzcGFjZT0icHJlc2VydmUiIHdpZHRoPSIzMnB4IiBoZWlnaHQ9IjMycHgiPgo8Zz4KCTxwYXRoIGQ9Ik01OS45NzMsMjYuNTM1bC0wLjAwMy0wLjA1N2wtMC4xNDctMC4wODJMNTQsMjAuOTc1VjguOTk3SDQxLjEzNWwtOC41OTMtOGMtMS40MjYtMS4zMjYtMy42NTktMS4zMjctNS4wODUsMC4wMDEgICBsLTguNTkyLDcuOTk5SDZ2MTEuOTc4bC01Ljg4OSw1LjQ4MkwwLDI2LjUzMXYwLjAzdjMyLjMwOHYxLjEyOWgxLjEyOWg1Ni4xMjdoMS42MTVoMC40NDlINjB2LTEuMTI5VjI3LjEwMXYtMC41NEw1OS45NzMsMjYuNTM1eiAgICBNMzIuMDcyLDQyLjk4N2w5Ljk1Mi01LjQ4N0w1NCwzMC44OTd2MGw0LTIuMjA2djI4LjU5M2wtNC43MDYtMi41OTRMMzIuMDcyLDQyLjk4N3ogTTU3LjMxLDI2Ljc4OUw1NCwyOC42MTR2LTQuOTA3TDU3LjMxLDI2Ljc4OSAgIHogTTI4LjgxOSwyLjQ2MmMwLjY2Mi0wLjYxNSwxLjctMC42MTQsMi4zNi0wLjAwMWw3LjAyLDYuNTM2SDIxLjgwMUwyOC44MTksMi40NjJ6IE04LDEwLjk5N2g4LjcxN2gyNi41NjZINTJ2OC4xMTZ2MTAuNjAyICAgbC0xOC4xMDIsOS45ODJsLTMuODk3LDIuMTQ5TDgsMjkuNzE1VjE5LjExNFYxMC45OTd6IE02LDI4LjYxMmwtMy4zMDgtMS44MjRMNiwyMy43MDdWMjguNjEyeiBNMiw1Ny45OTdWMjguNjkxbDQsMi4yMDZ2MCAgIGwxNy44Miw5LjgyNWw0LjEwOSwyLjI2NmwwLDBsMTAuMzE2LDUuNjg4bDE2LjkwNyw5LjMyMkgyeiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTI3LDE3Ljk5N2g2YzAuNTUzLDAsMS0wLjQ0NywxLTFzLTAuNDQ3LTEtMS0xaC02Yy0wLjU1MywwLTEsMC40NDctMSwxUzI2LjQ0NywxNy45OTcsMjcsMTcuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTQyLDMwLjk5N2gtN2MtMC41NTMsMC0xLDAuNDQ3LTEsMXMwLjQ0NywxLDEsMWg3YzAuNTUzLDAsMS0wLjQ0NywxLTFTNDIuNTUzLDMwLjk5Nyw0MiwzMC45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMTcsMjQuOTk3aDVjMC41NTMsMCwxLTAuNDQ3LDEtMXMtMC40NDctMS0xLTFoLTVjLTAuNTUzLDAtMSwwLjQ0Ny0xLDFTMTYuNDQ3LDI0Ljk5NywxNywyNC45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMjksMjMuOTk3YzAsMC41NTMsMC40NDcsMSwxLDFoOGMwLjU1MywwLDEtMC40NDcsMS0xcy0wLjQ0Ny0xLTEtMWgtOEMyOS40NDcsMjIuOTk3LDI5LDIzLjQ0NSwyOSwyMy45OTd6IiBmaWxsPSIjMDAwMDAwIi8+Cgk8cGF0aCBkPSJNMjYsMjQuOTk3YzAuMjYsMCwwLjUyLTAuMTEsMC43MS0wLjI5YzAuMTgtMC4xOSwwLjI5LTAuNDUsMC4yOS0wLjcxYzAtMC4yNzEtMC4xMS0wLjUyMS0wLjI5LTAuNzEgICBjLTAuMzctMC4zNy0xLjA1LTAuMzctMS40MiwwYy0wLjE4MSwwLjE4OS0wLjI5LDAuNDM5LTAuMjksMC43MWMwLDAuMjYsMC4xMDksMC41MiwwLjI5LDAuNzEgICBDMjUuNDc5LDI0Ljg4NywyNS43NCwyNC45OTcsMjYsMjQuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTMxLDMwLjk5N0gyMWMtMC41NTMsMC0xLDAuNDQ3LTEsMXMwLjQ0NywxLDEsMWgxMGMwLjU1MywwLDEtMC40NDcsMS0xUzMxLjU1MywzMC45OTcsMzEsMzAuOTk3eiIgZmlsbD0iIzAwMDAwMCIvPgoJPHBhdGggZD0iTTE3LDMyLjk5N2MwLjI2LDAsMC41Mi0wLjExLDAuNzEtMC4yOWMwLjE4LTAuMTksMC4yOS0wLjQ1LDAuMjktMC43MXMtMC4xMDEtMC41MS0wLjI5LTAuNzFjLTAuMzctMC4zNy0xLjA0LTAuMzctMS40MiwwICAgQzE2LjEsMzEuNDc3LDE2LDMxLjcyNywxNiwzMS45OTdjMCwwLjI2LDAuMTA5LDAuNTIsMC4yOSwwLjcxQzE2LjQ3OSwzMi44OTcsMTYuNzQsMzIuOTk3LDE3LDMyLjk5N3oiIGZpbGw9IiMwMDAwMDAiLz4KCTxwYXRoIGQ9Ik00MiwyNC45OTdjMC4yNiwwLDAuNTItMC4xMSwwLjcxLTAuMjljMC4xOC0wLjE5LDAuMjktMC40NSwwLjI5LTAuNzFzLTAuMTEtMC41MjEtMC4yOS0wLjcxYy0wLjM4LTAuMzctMS4wNS0wLjM3LTEuNDIsMCAgIGMtMC4xODEsMC4xODktMC4yOSwwLjQzOS0wLjI5LDAuNzFjMCwwLjI2LDAuMTA5LDAuNTIsMC4yOSwwLjcxQzQxLjQ3OSwyNC44ODcsNDEuNzQsMjQuOTk3LDQyLDI0Ljk5N3oiIGZpbGw9IiMwMDAwMDAiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K");

                    h4.Controls.Add(img_icon_mail);
                    //  <img src= /> dateTime.ToString(@"yyyy/MM/dd hh:mm:ss tt", new CultureInfo("en-US"));
                    HtmlGenericControl h_read = new HtmlGenericControl("h6");
                    h_read.InnerText = "อ่าน " + readnew.OrderByDescending(f => f.Read_Date).FirstOrDefault(g => g.News_ID == news.News_ID && g.User_ID == HttpContext.Current.Request.Cookies["User_ID"].Value)
                        .Read_Date.Value.ToString(@"yyyy/MM/dd hh:mm:ss tt", new CultureInfo("th-TH"));
                    h4.Controls.Add(h_read);

                    div_media_body.Controls.Add(h_read);
                }
                */

            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

    }
    #endregion

    #region GridView Row command
    protected void grdTab_1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        logger.Info(HttpContext.Current.Request.Cookies["User_ID"].Value + " " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            //  string Billing_ID = lnkView.CommandArgument;

            int index = int.Parse(lnkView.CommandArgument.ToString());
            GridViewRow currentRow = grdTab_1.Rows[index];
            Label lbl_Billing_ID = (Label)currentRow.FindControl("lbl_Billing_ID");
            string Billing_ID = lbl_Billing_ID.Text;
            Session["Billing_ID"] = Billing_ID;
            Response.Redirect("~/Views/ReceivingList.aspx");

        }
    }

    protected void grdTab_2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            int index = int.Parse(lnkView.CommandArgument.ToString());
            GridViewRow currentRow = grdTab_2.Rows[index];
            Label lbl_Billing_ID = (Label)currentRow.FindControl("lbl_Billing_ID");
            string Billing_ID = lbl_Billing_ID.Text;
            Session["Billing_ID"] = Billing_ID;
            Response.Redirect("~/Views/ReceivingList.aspx");

        }
    }

    protected void grdTab_3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            int index = int.Parse(lnkView.CommandArgument.ToString());
            GridViewRow currentRow = grdTab_3.Rows[index];
            Label lbl_Billing_ID = (Label)currentRow.FindControl("lbl_Billing_ID");
            string Billing_ID = lbl_Billing_ID.Text;
            Session["Billing_ID"] = Billing_ID;
            Response.Redirect("~/Views/ReceivingList.aspx");

        }
    }

    protected void PageDropDownListTab1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdTab_1.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownListTab1");

        // Set the PageIndex property to display that page selected by the user.
        grdTab_1.PageIndex = pageList.SelectedIndex;
        //btnSearch_Click(sender, e);

        List<dbo_BillingClass> bill_All = dbo_BillingDataClass.Get_Billing(Request.Cookies["User_ID"].Value);


        bill_All = bill_All.Where(f => f.Billing_Type != "YDOM")
         .Where(f => (!(f.Billing_Type == "ZDOM" && !string.IsNullOrEmpty(f.Ref_Invoice_No))))
         .Where(f => f.Invoice_Status != "ยกเลิกแล้ว")
         .Where(f => f.Invoice_Status != "ยันยันแล้ว")
         .ToList();

        List<dbo_BillingClass> bill_Tab1 = new List<dbo_BillingClass>();

        foreach (dbo_BillingClass b in bill_All)
        {
            if ((b.Billing_Type == "ZDOM" || b.Billing_Type == "YDOM") && (b.Invoice_Status == "ยังไม่ยืนยัน"))
            {
                bill_Tab1.Add(b);
            }
        }

        grdTab_1.DataSource = bill_Tab1;
        grdTab_1.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void PageDropDownListTab2_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdTab_2.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownListTab2");

        // Set the PageIndex property to display that page selected by the user.
        grdTab_2.PageIndex = pageList.SelectedIndex;
        List<dbo_BillingClass> bill_All = dbo_BillingDataClass.Get_Billing(Request.Cookies["User_ID"].Value);


        bill_All = bill_All.Where(f => f.Billing_Type != "YDOM")
         .Where(f => (!(f.Billing_Type == "ZDOM" && !string.IsNullOrEmpty(f.Ref_Invoice_No))))
         .Where(f => f.Invoice_Status != "ยกเลิกแล้ว")
         .Where(f => f.Invoice_Status != "ยันยันแล้ว")
         .ToList();

        List<dbo_BillingClass> bill_Tab2 = new List<dbo_BillingClass>();

        foreach (dbo_BillingClass b in bill_All)
        {
            if ((b.Billing_Type == "ZDDN") && (b.Invoice_Status == "ยังไม่ยืนยัน"))
            {
                bill_Tab2.Add(b);
            }
        }

        grdTab_2.DataSource = bill_Tab2;
        grdTab_2.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void PageDropDownListTab3_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdTab_3.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownListTab3");

        // Set the PageIndex property to display that page selected by the user.
        grdTab_3.PageIndex = pageList.SelectedIndex;

        List<dbo_BillingClass> bill_All = dbo_BillingDataClass.Get_Billing(Request.Cookies["User_ID"].Value);


        bill_All = bill_All.Where(f => f.Billing_Type != "YDOM")
         .Where(f => (!(f.Billing_Type == "ZDOM" && !string.IsNullOrEmpty(f.Ref_Invoice_No))))
         .Where(f => f.Invoice_Status != "ยกเลิกแล้ว")
         .Where(f => f.Invoice_Status != "ยันยันแล้ว")
         .ToList();

        List<dbo_BillingClass> bill_Tab3 = new List<dbo_BillingClass>();

        foreach (dbo_BillingClass b in bill_All)
        {
            if ((b.Billing_Type == "ZDCN") && (b.Invoice_Status == "ยังไม่ยืนยัน"))
            {
                bill_Tab3.Add(b);
            }
        }

        grdTab_3.DataSource = bill_Tab3;
        grdTab_3.DataBind();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void grdTab_1_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdTab_1.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownListTab1");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabelTab1");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < grdTab_1.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == grdTab_1.PageIndex)
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
            int currentPage = grdTab_1.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + grdTab_1.PageCount.ToString();

        }
    }

    protected void grdTab_2_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdTab_2.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownListTab2");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabelTab2");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < grdTab_2.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == grdTab_2.PageIndex)
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
            int currentPage = grdTab_2.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + grdTab_2.PageCount.ToString();

        }
    }

    protected void grdTab_3_DataBound(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = grdTab_3.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownListTab3");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabelTab3");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < grdTab_3.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == grdTab_3.PageIndex)
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
            int currentPage = grdTab_3.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "หน้า " + currentPage.ToString() +
              " จาก " + grdTab_3.PageCount.ToString();

        }
    }
    #endregion    
}

public class JSONObject
{
    public string type { get; set; }
    public RootObject data { get; set; }
    public options options { get; set; }
    public defaults defaults { get; set; }
    //public globals globals { get; set; }
    //public axisY axisY { get; set; }
    //public xAxes xAxes { get; set; }
    //public legend legend { get; set; }
}

public class Dataset
{
    public Dataset()
    {
        data = new List<decimal>();
        backgroundColor = new List<string>();
    }
    public string label { get; set; }
    public List<decimal> data { get; set; }

    public string type { get; set; }
    public string borderColor { get; set; }
    public bool fill { get; set; }
    //public string backgroundColor { get; set; }
    public List<string> backgroundColor { get; set; }
}

public class RootObject
{
    public RootObject()
    {
        labels = new List<string>();
        datasets = new List<Dataset>();
    }
    public List<string> labels { get; set; }
    public List<Dataset> datasets { get; set; }
}

public class options
{
    public title title { get; set; }
    public legend legend { get; set; }
    public scales scales { get; set; }
    //public axisY axisY { get; set; }
    //public axisX axisX { get; set; }
}

public class globals
{
    public globals()
    {
     
    }
    //public string font_size { get; set; }
    public string fontFamily { get; set; }
}

public class defaults
{
    //public defaults()
    //{
    //    global = new global();
    //}
    //public global global { get; set; }

    public defaults()
    {
        defaultOptions = new defaultOptions();
    }
    public defaultOptions defaultOptions { get; set; }
}

public class defaultOptions 
{
    //public string defaultFontFamily { get; set; }
    //public string defaultFont { get; set; }
    public defaultOptions()
    {
        global = new global();
    }
    public global global { get; set; }
}

public class global
{
    //public string defaultFontFamily { get; set; }
    public string defaultFontFamily { get; set; }
}

public class title
{
    public bool display { get; set; }
    public string text { get; set; }
    public string fontFamily { get; set; }
    public Int32 fontSize { get; set; }
}

public class legend
{
    public bool display { get; set; }
    public legend()
    {
        labels = new labels();
    }
    public labels labels { get; set; }
    //public string fontFamily { get; set; }
    //public Int32 fontSize { get; set; }
}

public class labels
{
    public string fontFamily { get; set; }
    public Int32 fontSize { get; set; }
}

public class scales
{
    public scales()
    {
        xAxes = new List<xAxes>();
        yAxes = new List<yAxes>();
    }
    public List<xAxes> xAxes { get; set; }
    public List<yAxes> yAxes { get; set; }
}

public class xAxes
{
    public xAxes()
    {
        ticks = new ticks();
    }
    public ticks ticks { get; set; }
}

public class yAxes
{
    public yAxes()
    {
        ticks = new ticks();
    }
    public ticks ticks { get; set; }
}

public class ticks
{
    public string fontFamily { get; set; }
}



