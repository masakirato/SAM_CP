using System;

public class Product_Quantity
{
    public String PO_Number { get; set; }

    public string ItemNo { get; set; }

    public string Product_ID { get; set; }

    public string Product_Name { get; set; }

    public Nullable<Decimal> Price { get; set; }

    public Nullable<Byte> Vat { get; set; }

    public Nullable<Int16> Stock_on_hand { get; set; }

    public Nullable<Int16> Suggest_Quantity { get; set; }

    public Nullable<Int16> Quantity { get; set; }

    public Nullable<Decimal> Sub_Total { get; set; }

    public Nullable<Decimal> Vat_Amount { get; set; }

    public Nullable<Decimal> Total { get; set; }

    public Nullable<Byte> Point { get; set; }

    public string Item_Value { get; set; }

    public int Stock_End { get; set; }

    public int Stock_Out { get; set; }

    public int Stock_SP { get; set; }

}
