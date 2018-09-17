using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_DeptPayToSP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Debt_ID");
            dt.Columns.Add("SP_ID");
            dt.Columns.Add("Debt_Date");
            dt.Columns.Add("Debt_Amount");
            dt.Columns.Add("Total_Payment_Amount");
            dt.Columns.Add("Balance_Outstanding_Amount");

            dt.Rows.Add("C000001", "sale 1", "20/02/2560", "10,000.00", "9,000.00", "1,000.00");
            dt.Rows.Add("C000001", "sale 1", "20/03/2560", "10,000.00", "9,000.00", "1,000.00");
            dt.Rows.Add("T1", "2,000.00", "", "", "", "");

            dt.Rows.Add("C000002", "sale 2", "20/02/2560", "10,000.00", "9,000.00", "1,000.00");
            dt.Rows.Add("C000002", "sale 2", "20/03/2560", "10,000.00", "9,000.00", "1,000.00");
            dt.Rows.Add("T1", "2,000.00", "", "", "", "");
            dt.Rows.Add("T2", "4,000.00", "", "", "", "");

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

            if (dt.Rows[i][0].ToString() == "T1")
            {
                row.Cells[0].ColumnSpan = 5;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;

                row.Cells[0].Text = "ยอดค้างชำระรวม";
                row.Cells[5].Text = dt.Rows[i][1].ToString();
                row.HorizontalAlign = HorizontalAlign.Right;
                row.Cells[0].ForeColor = System.Drawing.Color.DarkBlue;
            }
            else if (dt.Rows[i][0].ToString() == "T2")
            {
                row.Cells[0].ColumnSpan = 5;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;

                row.Cells[0].Text = "ยอดค้างชำระรวมทั้งหมด";
                row.Cells[5].Text = dt.Rows[i][1].ToString();
                row.HorizontalAlign = HorizontalAlign.Right;
                row.Cells[0].ForeColor = System.Drawing.Color.Black;
            }

        }
    }
}