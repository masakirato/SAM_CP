using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreateCNDN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GridViewOrdering_1
            DataTable dt = new DataTable();
            dt.Columns.Add("Item");
            dt.Columns.Add("Product_ID");
            dt.Columns.Add("Product_Name");
            dt.Columns.Add("Unit_of_item");
            dt.Columns.Add("Amount");




            dt.Rows.Add("Merge", "180 CC.", "", "", "");
            dt.Rows.Add("1", "10001", "PM 180 โกลด์ แอดวานซ์จืด", "ขวด", "100");
            dt.Rows.Add("2", "10001", "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง", "ขวด", "100");

            dt.Rows.Add("Merge", "200 CC.", "", "", "");

            dt.Rows.Add("1", "10001", "PM 200 เมล่อนญี่ปุ่น", "ขวด", "100");
            dt.Rows.Add("2", "10001", "PM 200 เมล่อนญี่ปุ่น", "ขวด", "100");
            dt.Rows.Add("3", "10001", "PM 200 เมล่อนญี่ปุ่น", "ขวด", "100");
            dt.Rows.Add("4", "10001", "PM 200 เมล่อนญี่ปุ่น", "ขวด", "100");
            Session["DataTable"] = dt;

            GridViewCNDN_1.DataSource = dt;
            GridViewCNDN_1.DataBind();
        }

    }

    protected void GridViewOrdering_1_OnDataBound(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["DataTable"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GridViewRow row = GridViewCNDN_1.Rows[i];

            if (dt.Rows[i][0].ToString() == "Merge")
            {
                string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 4;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;

                row.Cells[0].Text = dt.Rows[i][1].ToString();
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
            }

        }

    }
}