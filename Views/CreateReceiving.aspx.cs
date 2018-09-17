using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_CreateReceiving : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          

            Session.Remove("GetProduct_Quantity_tab_1");
            List<Product_Quantity> listProduct_Quantitytab_1 = dbo_OrderingDataClass.GetProduct_Quantity_Receiving(string.Empty, "91", "PO-0001");
            Session["GetProduct_Quantity_tab_1"] = listProduct_Quantitytab_1;
            GridViewReceiving_1.DataSource = listProduct_Quantitytab_1;
            GridViewReceiving_1.DataBind();




        }

    }

    protected void TxtId_TextChanged(object sender, EventArgs e)
    {

    }

    protected void GridViewReceiving_1_OnDataBound(object sender, EventArgs e)
    {
        List<Product_Quantity> listProduct_Quantity = (List<Product_Quantity>)Session["GetProduct_Quantity_tab_1"];

        if (listProduct_Quantity != null)
        {
            for (int i = 0; i < listProduct_Quantity.Count; i++)
            {
                GridViewRow row = GridViewReceiving_1.Rows[i];

                if (listProduct_Quantity[i].ItemNo.ToString() == "Merge")
                {
                    Label txt = (Label)row.FindControl("Label_Item");
                    txt.Text = listProduct_Quantity[i].Item_Value;

                    row.Cells[0].ForeColor = System.Drawing.Color.Olive;
                    row.BackColor = System.Drawing.Color.Beige;

                    row.Cells[0].ColumnSpan = 7;
                    row.Cells[1].Visible = false;
                    row.Cells[2].Visible = false;
                    row.Cells[3].Visible = false;
                    row.Cells[4].Visible = false;
                    row.Cells[5].Visible = false;
                    row.Cells[6].Visible = false;

                }
            }
        }
    }


}