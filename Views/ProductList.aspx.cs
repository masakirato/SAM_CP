using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ProductList : System.Web.UI.Page
{
    private dbo_ProductDataClass clsdbo_ProductData = new dbo_ProductDataClass();
    private SAMDataClass clsSAMData = new SAMDataClass();
    private DataView dvProduct;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetUpDrowDownList();
            ShowGridPanel();
        }
    }

    private void SetUpDrowDownList()
    {
        Dictionary<string, string> title = dbo_ItemDataClass.GetDropDown("2401");

    }

    private void ShowGridPanel()
    {

    }

    private void ShowFormPanel(string User_ID, string Mode)
    {
        pnlForm.Visible = false;
        pnlGrid.Visible = true;
    }

    private void InsertRecord()
    {
        dbo_ProductClass clsdbo_Product = new dbo_ProductClass();

    }

    private void SetData(dbo_ProductClass clsdbo_Product)
    {
        //if (string.IsNullOrEmpty(txtProduct_ID.Text))
        //{
        //    clsdbo_Product.Product_ID = null;
        //}
        //else
        //{
        //    clsdbo_Product.Product_ID = txtProduct_ID.Text;
        //}
        if (string.IsNullOrEmpty(txtProduct_Name.Text))
        {
            clsdbo_Product.Product_Name = null;
        }
        else
        {
            clsdbo_Product.Product_Name = txtProduct_Name.Text;
        }
        //if (string.IsNullOrEmpty(txtSize.Text))
        //{
        //    clsdbo_Product.Size = null;
        //}
        //else
        //{
        //    clsdbo_Product.Size = System.Convert.ToInt16(txtSize.Text);
        //}
        //if (string.IsNullOrEmpty(txtUnit_of_item_ID.Text))
        //{
        //    clsdbo_Product.Unit_of_item_ID = null;
        //}
        //else
        //{
        //    clsdbo_Product.Unit_of_item_ID = txtUnit_of_item_ID.Text;
        //}
        //if (string.IsNullOrEmpty(txtProduct_group_ID.Text))
        //{
        //    clsdbo_Product.Product_group_ID = null;
        //}
        //else
        //{
        //    clsdbo_Product.Product_group_ID = txtProduct_group_ID.Text;
        //}
        //if (string.IsNullOrEmpty(txtEAN.Text))
        //{
        //    clsdbo_Product.EAN = null;
        //}
        //else
        //{
        //    clsdbo_Product.EAN = txtEAN.Text;
        //}
        if (string.IsNullOrEmpty(txtCP_Meiji_Price.Text))
        {
            clsdbo_Product.CP_Meiji_Price = null;
        }
        else
        {
            clsdbo_Product.CP_Meiji_Price = System.Convert.ToDecimal(txtCP_Meiji_Price.Text);
        }
        if (string.IsNullOrEmpty(txtPoint.Text))
        {
            clsdbo_Product.Point = null;
        }
        else
        {
            clsdbo_Product.Point = System.Convert.ToByte(txtPoint.Text);
        }
        // clsdbo_Product.Exclude_Vat = txtExclude_Vat.Checked ? true : false;
        if (string.IsNullOrEmpty(txtVat.Text))
        {
            clsdbo_Product.Vat = null;
        }
        else
        {
            clsdbo_Product.Vat = System.Convert.ToByte(txtVat.Text);
        }
        //if (string.IsNullOrEmpty(txtOrder_No.Text))
        //{
        //    clsdbo_Product.Order_No = null;
        //}
        //else
        //{
        //    clsdbo_Product.Order_No = System.Convert.ToInt16(txtOrder_No.Text);
        //}
        //if (string.IsNullOrEmpty(txtQuantity_in__carte.Text))
        //{
        //    clsdbo_Product.Quantity_in__carte = null;
        //}
        //else
        //{
        //    clsdbo_Product.Quantity_in__carte = System.Convert.ToByte(txtQuantity_in__carte.Text);
        //}
        //if (string.IsNullOrEmpty(txtPacking_Size.Text))
        //{
        //    clsdbo_Product.Packing_Size = null;
        //}
        //else
        //{
        //    clsdbo_Product.Packing_Size = System.Convert.ToByte(txtPacking_Size.Text);
        //}
        // clsdbo_Product.Status = txtStatus.Checked ? true : false;
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {


    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}