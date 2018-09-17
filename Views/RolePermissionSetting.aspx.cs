using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_RolePermissionSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Menu");
            dt.Columns.Add("Lev1");
            dt.Columns.Add("Lev2");

            dt.Rows.Add("การกำหนดสิทธิการใช้งาน", string.Empty, string.Empty);
            dt.Rows.Add("ข้อมูลMaster", string.Empty, string.Empty);
            dt.Rows.Add("ข้อมูลCP user", string.Empty, string.Empty);

            GridViewPermission.DataSource = dt;
            GridViewPermission.DataBind();
        }



    }
    protected void GridViewPermission_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header,
                                                        DataControlRowState.Insert);  //creating new Header Type 

            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = string.Empty;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);
            GridViewPermission.Controls[0].Controls.AddAt(0, HeaderGridRow);

            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "เมนู/ฟังก์ชัน";
            HeaderCell1.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell1);//Adding HeaderCell to header.

            GridViewPermission.Controls[0].Controls.AddAt(0, HeaderGridRow);
            // GridViewPermission.Controls[0].Controls.RemoveAt(0);
        }
    }
    protected void GridViewPermission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Width = new Unit("80px");
            e.Row.Cells[1].Width = new Unit("240px");
        }
    }
}