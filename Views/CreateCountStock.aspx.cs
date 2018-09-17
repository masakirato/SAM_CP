using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreateCountStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item");
            dt.Columns.Add("Product_ID");
            dt.Columns.Add("Product_Name");
            dt.Columns.Add("Stock_on_hand");
            dt.Columns.Add("Unit_of_item");
            dt.Columns.Add("Suggest_Quantity");
            dt.Columns.Add("Dif");
            dt.Columns.Add("Remark");

            dt.Rows.Add("Merge", "180 CC.", "", "", "", "", "", "");
            dt.Rows.Add("1", "10001", "PM 180 โกลด์ แอดวานซ์จืด", "100", "ขวด", "90", "10", "-");
            dt.Rows.Add("2", "10001", "PM 180 โกลด์ แอดวานซ์น้ำผึ้ง", "100", "ขวด", "100", "0", "-");

            dt.Rows.Add("Merge", "200 CC.", "", "", "", "", "", "");

            dt.Rows.Add("1", "10001", "PM 200 เมล่อนญี่ปุ่น", "100", "ขวด", "100", "90", "-");
            dt.Rows.Add("2", "10001", "PM 200 เมล่อนญี่ปุ่น", "100", "ขวด", "100", "90", "-");
            dt.Rows.Add("3", "10001", "PM 200 เมล่อนญี่ปุ่น", "100", "ขวด", "100", "90", "-");
            dt.Rows.Add("4", "10001", "PM 200 เมล่อนญี่ปุ่น", "100", "ขวด", "100", "90", "-");
            Session["DataTable"] = dt;

            GridViewOrdering_1.DataSource = dt;
            GridViewOrdering_1.DataBind();
        }
    }


    protected void GridViewOrdering_1_OnDataBound(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["DataTable"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GridViewRow row = GridViewOrdering_1.Rows[i];

            if (dt.Rows[i][0].ToString() == "Merge")
            {
                string header = row.Cells[0].Text;

                row.Cells[0].ColumnSpan = 8;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                //row.Cells[8].Visible = false;

                row.Cells[0].Text = dt.Rows[i][1].ToString();
                row.Cells[0].ForeColor = System.Drawing.Color.Olive;
            }

        }
    }
}