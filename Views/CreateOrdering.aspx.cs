using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreateOrdering : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("GetProduct_Quantity_tab_1");
            //List<dbo_ProductClass> item1 = dbo_ProductDataClass.GetProductByProductGroupID("นมสดพาสเจอร์ไรส์");
            //Session["GetProduct_Quantity_tab_1"] = item1;
            //GridViewOrdering_Tab1.DataSource = item1;
            //GridViewOrdering_Tab1.DataBind();

            //Session.Remove("GetProduct_Quantity_tab_2");
            //List<dbo_ProductClass> item2 = dbo_ProductDataClass.GetProductByProductGroupID("นมเปรี้ยว");
            //Session["GetProduct_Quantity_tab_2"] = item2;
            //GridViewOrdering_Tab2.DataSource = item2;
            //GridViewOrdering_Tab2.DataBind();


        }

    }

    protected void GridViewOrdering_1_OnDataBound(object sender, EventArgs e)
    {
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_1"];

        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewOrdering_Tab1.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 9;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;


                row.Cells[0].Text = listProduct_Quantity[i].Product_Name;
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }

        }
    }

    protected void GridViewOrdering_2_OnDataBound(object sender, EventArgs e)
    {
        List<dbo_ProductClass> listProduct_Quantity = (List<dbo_ProductClass>)Session["GetProduct_Quantity_tab_2"];

        for (int i = 0; i < listProduct_Quantity.Count; i++)
        {
            GridViewRow row = GridViewOrdering_Tab2.Rows[i];

            if (listProduct_Quantity[i].Product_ID.ToString() == "Merge")
            {
                string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 9;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;


                row.Cells[0].Text = listProduct_Quantity[i].Product_Name;
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                row.BackColor = System.Drawing.Color.Beige;
            }

        }
    }

    protected void ButtonOK_Click(object sender, EventArgs e)
    {

    }

    private void SetData(dbo_OrderingClass clsdbo_Ordering)
    {

    }

}
