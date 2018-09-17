﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreateRequisition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //DataTable dt = new DataTable();
            //dt.Columns.Add("Item");
            //dt.Columns.Add("Product_ID");
            //dt.Columns.Add("Product_Name");
            //dt.Columns.Add("Unit_of_item");
            //dt.Columns.Add("PricePerUnit");

            //dt.Columns.Add("Grand_Total_Qty");
            //dt.Columns.Add("SuggestAmount");
            //dt.Columns.Add("Requisition");
            //dt.Columns.Add("Grand_Total_Amount");


            //dt.Rows.Add("Merge", "180 CC.", "", "", "", "", "", "", "");
            //dt.Rows.Add("1", "10001", "PM 180 โกลด์ แอดวานซ์จืด", "ขวด", "15.89", "100", "10", "100.00", "10");
            //dt.Rows.Add("2", "10001", "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง", "ขวด", "15.89", "100", "10", "100.00", "10");

            //dt.Rows.Add("Merge", "200 CC.", "", "", "", "", "", "", "");

            //dt.Rows.Add("1", "10001", "PM 200 เมล่อนญี่ปุ่น", "ขวด", "15.89", "100", "10", "100.00", "10");
            //dt.Rows.Add("2", "10001", "PM 200 เมล่อนญี่ปุ่น", "ขวด", "15.89", "100", "10", "100.00", "10");
            //dt.Rows.Add("3", "10001", "PM 200 เมล่อนญี่ปุ่น", "ขวด", "15.89", "100", "10", "100.00", "10");
            //dt.Rows.Add("4", "10001", "PM 200 เมล่อนญี่ปุ่น", "ขวด", "15.89", "100", "10", "100.00", "10");
            //Session["DataTable"] = dt;

            //GridViewOrdering_1.DataSource = dt;
            //GridViewOrdering_1.DataBind();
        }

    }

    protected void GridViewOrdering_1_OnDataBound(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["DataTable"];

        List<Product_Quantity> listProduct_Quantity = (List<Product_Quantity>)Session["GetProduct_Quantity_tab_1"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GridViewRow row = GridViewOrdering_1.Rows[i];

            if (dt.Rows[i][0].ToString() == "Merge")
            {
                string header = row.Cells[0].Text;

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

                row.Cells[0].Text = dt.Rows[i][1].ToString();
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
            }

        }
    }
}