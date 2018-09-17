using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class RPT_Get_OtherRequisition_No
{
    public string Requisition_No { get; set; }
    public string RQ_Date { get; set; }
    public string CV_Code { get; set; }
    public string AgentName { get; set; }
    public string Address { get; set; }
    public string Home_Phone_No { get; set; }
    public string Fax { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Reason { get; set; }
    public string Product_ID { get; set; }
    public string Product_Name { get; set; }
    public string Product_group_ID { get; set; }
    public short Size { get; set; }
    public string Unit { get; set; }
    public short Requisition_Qty { get; set; }
    public string Admin_Name { get; set; }
    public string Owner_Name { get; set; }
    public string Warehouse_Name { get; set; }
    public string Order_Product { get; set; }
}



public class RPT_Get_Requisition_No
{

    public string Product_ID { get; set; }
    public int clm_1 { get; set; }
    public int clm_2 { get; set; }
    public int clm_3 { get; set; }
    public int clm_4 { get; set; }
    public int clm_5 { get; set; }
    public int clm_6 { get; set; }
    public int clm_7 { get; set; }
    public int clm_8 { get; set; }
    public int clm_9 { get; set; }
    public int clm_10 { get; set; }
    public string Product_Name { get; set; }
    public string User_ID { get; set; }
    public string AgentName { get; set; }
    public short Size { get; set; }
    public string Requisition_Date { get; set; }
    public string Requisition_No { get; set; }
    public string Product_group_ID { get; set; }
    public short Deposit_Qty { get; set; }
    public string Invoice_Address { get; set; }
    public string User_SP { get; set; }
    public string User_Admin { get; set; }
    public string User_Owner { get; set; }
    public string User_Warehouse { get; set; }
    public string Tax_ID { get; set; }
    public string sp_name { get; set; }
    public short Return_Qty { get; set; }

}

public class RPT_Get_PO_Number
{
    public DateTime Date_of_CP_receive_transaction { get; set; }
    public DateTime Date_of_create_order_or_PO_Date { get; set; }
    public DateTime Date_of_delivery_goods { get; set; }
    public string AgentName { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string CV_Code { get; set; }
    public string Home_Phone_No { get; set; }
    public string Fax { get; set; }
    public string Product_ID { get; set; }
    public short Quantity { get; set; }
    public decimal Total { get; set; }
    public decimal Price { get; set; }
    public decimal Vat { get; set; }
    public short Size { get; set; }
    public string Product_Name { get; set; }
    public string Unit_of_item_ID { get; set; }
    public string PO_Number { get; set; }
    public string Product_group_ID { get; set; }
    public string Position { get; set; }


}

public class RPT_Get_Clearing_No
{
    public string Clearing_No { get; set; }
    public string Clearing_Date { get; set; }
    public string User_ID { get; set; }
    public decimal Cash_Payment { get; set; }
    public decimal Actual_Payment { get; set; }
    public decimal Balance_Outstanding { get; set; }
    public short Net_Sales_Qty { get; set; }
    public decimal Net_Sales_Amount { get; set; }
    public decimal Credit_Amount { get; set; }
    public decimal SP_Cash { get; set; }
    public decimal Cash_Payment_Amount { get; set; }
    public decimal Cheque_Payment_Amount { get; set; }
    public decimal Transfer_Payment_Amount { get; set; }
    public decimal Sub_Total { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public short Today_Points { get; set; }
    public decimal Today_Commission { get; set; }
    public decimal This_Month_Sales_Amount { get; set; }
    public decimal Today_Deposit_Amount { get; set; }
    public decimal Total_Credit_Amount { get; set; }
    public decimal Total_Balance_Outstanding { get; set; }
    public decimal Total_Commission { get; set; }
    public decimal This_Month_Points { get; set; }
    public decimal Total_Deposit { get; set; }
    public string SP { get; set; }
    public string ADMIN { get; set; }
    public string OWNER { get; set; }
    public string Tax_ID { get; set; }
    public string AgentName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public decimal Total_Deduct { get; set; }
    public decimal Total_Subsidy { get; set; }
    public decimal Total_Credit1 { get; set; }
    public decimal Total_Credit2 { get; set; }
    public decimal Total_Credit3 { get; set; }
    public decimal Today_Return_Amount { get; set; }


    //private String m_Type_Deduct;
    //private String m_Description_Deduct;
    //private Decimal m_Amount_Deduct;
    //private String m_Unit_Deduct;


    //public String Type_Deduct
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Type_Deduct)) return string.Empty;
    //        try
    //        {
    //            m_Type_Deduct = Reports.Select_Record_Deduct(Clearing_No).Type;
    //            return m_Type_Deduct;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Type_Deduct = value;
    //    }
    //}

    //public String Description_Deduct
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Description_Deduct)) return string.Empty;
    //        try
    //        {
    //            m_Description_Deduct = Reports.Select_Record_Deduct(Clearing_No).Description;
    //            return m_Description_Deduct;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Description_Deduct = value;
    //    }
    //}

    //public String Unit_Deduct
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Unit_Deduct)) return string.Empty;
    //        try
    //        {
    //            m_Unit_Deduct = Reports.Select_Record_Deduct(Clearing_No).Unit;
    //            return m_Unit_Deduct;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Unit_Deduct = value;
    //    }
    //}

    //public decimal Amount_Deduct
    //{
    //    get
    //    {
    //        try
    //        {
    //            m_Amount_Deduct = Reports.Select_Record_Deduct(Clearing_No).Amount;
    //            return m_Amount_Deduct;
    //        }
    //        catch (Exception ex)
    //        {
    //            return 0;
    //        }
    //    }
    //    set
    //    {
    //        m_Amount_Deduct = value;
    //    }
    //}


    //private String m_Type_Payment;
    //private String m_Description_Payment;
    //private Decimal m_Amount_Payment;
    //private String m_Unit_Payment;

    //public String Type_Payment
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Type_Payment)) return string.Empty;
    //        try
    //        {
    //            m_Type_Payment = Reports.Select_Record_Payment(Clearing_No).Type;
    //            return m_Type_Payment;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Type_Payment = value;
    //    }
    //}
    //public String Description_Payment
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Description_Payment)) return string.Empty;
    //        try
    //        {
    //            m_Description_Payment = Reports.Select_Record_Payment(Clearing_No).Description;
    //            return m_Description_Payment;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Description_Payment = value;
    //    }
    //}

    //public String Unit_Payment
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Unit_Payment)) return string.Empty;
    //        try
    //        {
    //            m_Unit_Payment = Reports.Select_Record_Payment(Clearing_No).Unit;
    //            return m_Unit_Payment;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Unit_Payment = value;
    //    }
    //}

    //public decimal Amount_Payment
    //{
    //    get
    //    {
    //        try
    //        {
    //            m_Amount_Payment = Reports.Select_Record_Payment(Clearing_No).Amount;
    //            return m_Amount_Payment;
    //        }
    //        catch (Exception ex)
    //        {
    //            return 0;
    //        }
    //    }
    //    set
    //    {
    //        m_Amount_Payment = value;
    //    }
    //}


    //private String m_Type_Subsidy;
    //private String m_Description_Subsidy;
    //private Decimal m_Amount_Subsidy;
    //private String m_Unit_Subsidy;

    //public String Type_Subsidy
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Type_Subsidy)) return string.Empty;
    //        try
    //        {
    //            m_Type_Subsidy = Reports.Select_Record_Subsidy(Clearing_No).Type;
    //            return m_Type_Subsidy;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Type_Subsidy = value;
    //    }
    //}
    //public String Description_Subsidy
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Description_Subsidy)) return string.Empty;
    //        try
    //        {
    //            m_Description_Subsidy = Reports.Select_Record_Subsidy(Clearing_No).Description;
    //            return m_Description_Subsidy;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Description_Subsidy = value;
    //    }
    //}
    //public String Unit_Subsidy
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(m_Unit_Subsidy)) return string.Empty;
    //        try
    //        {
    //            m_Unit_Subsidy = Reports.Select_Record_Subsidy(Clearing_No).Unit;
    //            return m_Unit_Subsidy;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    set
    //    {
    //        m_Unit_Subsidy = value;
    //    }
    //}

    //public decimal Amount_Subsidy
    //{
    //    get
    //    {
    //        try
    //        {
    //            m_Amount_Subsidy = Reports.Select_Record_Subsidy(Clearing_No).Amount;
    //            return m_Amount_Subsidy;
    //        }
    //        catch (Exception ex)
    //        {
    //            return 0;
    //        }
    //    }
    //    set
    //    {
    //        m_Amount_Subsidy = value;
    //    }
    //}


}

public class RPT_Get_Clearing_No_Deduct
{
    public string Type { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; }
    public string Clearing_No { get; set; }
}


public class RPT_Get_Clearing_No_Payment
{
    public string Type { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; }
    public string Clearing_No { get; set; }
}


public class RPT_Get_Clearing_No_Subsidy
{
    public string Type { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; }
    public string Clearing_No { get; set; }
}

public class RPT_Order_By_Product
{
    public string PO_Number { get; set; }
    public string CV_Code_from_SAP { get; set; }
    public decimal Total_Amount_before_vat_included { get; set; }
    public decimal Vat_amount { get; set; }
    public decimal Total_amount_after_vat_included { get; set; }
    public DateTime Date_of_create_order_or_PO_Date { get; set; }
    public DateTime Date_of_CP_receive_transaction { get; set; }
    public DateTime Date_of_delivery_goods { get; set; }
    public string Order_Status { get; set; }
    public string Product_ID { get; set; }
    public decimal Price { get; set; }
    public decimal Vat { get; set; }
    public short Stock_on_hand { get; set; }
    public short Suggest_Quantity { get; set; }
    public short Quantity { get; set; }
    public decimal Sub_Total { get; set; }
    public decimal Vat_Amount { get; set; }
    public decimal Total { get; set; }
    public byte Point { get; set; }
    public string SAP_Product_Code { get; set; }
    public string Product_Name { get; set; }
    public short Size { get; set; }
    public string Unit_of_item_ID { get; set; }
    public string Product_group_ID { get; set; }
    public short Order_No { get; set; }
    public byte Packing_Size { get; set; }
    public string CV_Code { get; set; }
    public string Prefix_ID { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Agent_Type_ID { get; set; }
    public string Home_Phone_No { get; set; }
    public string Mobile { get; set; }
    public string Tax_ID { get; set; }
    public string Email { get; set; }
    public string Fax { get; set; }
    public string Concession_Area { get; set; }
    public string Owner_First_Name { get; set; }
    public string Owner_Last_Name { get; set; }
    public string Owner_Phone_No1 { get; set; }
    public string Owner_Phone_No2 { get; set; }
    public string Contact_First_Name { get; set; }
    public string Contact_Last_Name { get; set; }
    public string Contact_Phone_No1 { get; set; }
    public string Contact_Phone_No2 { get; set; }
    public string SD_ID { get; set; }
    public string SM_ID { get; set; }
    public string DM_ID { get; set; }
    public string Location_House_No { get; set; }
    public string Location_Village { get; set; }
    public string Location_Village_No { get; set; }
    public string Location_Alley { get; set; }
    public string Location_Road { get; set; }
    public string Location_Sub_district { get; set; }
    public string Location_District { get; set; }
    public string Location_Province { get; set; }
    public string Location_Postal_ID { get; set; }
    public string Invoice_House_No { get; set; }
    public string Invoice_Village { get; set; }
    public string Invoice_Village_No { get; set; }
    public string Invoice_Alley { get; set; }
    public string Invoice_Road { get; set; }
    public string Invoice_Sub_district { get; set; }
    public string Invoice_District { get; set; }
    public string Invoice_Province { get; set; }
    public string Invoice_Postal_ID { get; set; }
    public DateTime Start_Effective_Date { get; set; }
    public DateTime First_Order_Date { get; set; }
    public bool Status { get; set; }
    public DateTime Go_out_of_business_Date { get; set; }
    public string Applied_Document { get; set; }
    public string Other_Document { get; set; }
    public short Small_Case { get; set; }
    public short Large_Case { get; set; }
    public decimal Pledge_Amount { get; set; }
    public short Room_Size { get; set; }
    public decimal Cash_Deposit { get; set; }
    public decimal Bank_Guarantee { get; set; }
    public string Bank_ID { get; set; }
    public string Term_of_payment { get; set; }
    public string Remarks { get; set; }
    public string Product_Group_ID { get; set; }
    public string Price_Group_ID { get; set; }
    public string Grade { get; set; }
    public DateTime Grade_Effective_Date { get; set; }
    public string Location_Region { get; set; }
    public string Invoice_Region { get; set; }
    public string GM_ID { get; set; }
    public string APV_ID { get; set; }
    public decimal Credit_Limit { get; set; }
}

public class RPT_CustomerType
{
    public string CV_Code { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Residence_Type_ID { get; set; }
    public int Type_1 { get; set; }
    public int Type_2 { get; set; }
    public int Type_3 { get; set; }
}
public class RPT_ADJ_STOCK__4128
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string ProductGroup { get; set; }
    public string Proudct_ID { get; set; }
    public string Product_Name { get; set; }
    public short Size { get; set; }
    public short QTY { get; set; }
    public string Unit { get; set; }
    public short Count_QTY { get; set; }
    public short Diff_QTY { get; set; }
    public string Remark { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion { get; set; }
    public string paramAgent { get; set; }
    public string paramSP { get; set; }
    public string paramDateFrom { get; set; }
    public string paramDateTo { get; set; }
    public string paramProductGroup { get; set; }
    public string paramSize { get; set; }
}

public class RPT_ADJUST_CountSotck_4128
{
    public string CV_Code { get; set; }
    public string Agent_Name { get; set; }
    public DateTime? Count_Date { get; set; }
    public string Product_group_id { get; set; }
    public short? Size { get; set; }
    public short? Quantity { get; set; }
    public string Unit { get; set; }
    public short? Count_Quantity { get; set; }
    public short? Diff_Quantity { get; set; }
}

public class RPT_AGENT_DEBT_41222
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Debt_StartDate { get; set; }
    public string Debt_EndDate { get; set; }
    public string Status { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Debt_Date { get; set; }
    public decimal Debt_Amt { get; set; }
    public decimal Pay_Amt { get; set; }
    public decimal Outstanding_Amt { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion_Name { get; set; }
    public string pramAgent_Name { get; set; }
    public string pramSP_Name { get; set; }
    public string pramDebt_ID { get; set; }
    public string pramDebt_Name { get; set; }
    public string pramDebt_StartDate { get; set; }
    public string pramDebt_EndDate { get; set; }
    public string pramStatus { get; set; }
}

public class RPT_AnnualReport_4121
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string Agent_Name { get; set; }
    public string Product_group_ID { get; set; }
    public short Size { get; set; }
    public decimal M1 { get; set; }
    public decimal M2 { get; set; }
    public decimal M3 { get; set; }
    public decimal Q1 { get; set; }
    public decimal M4 { get; set; }
    public decimal M5 { get; set; }
    public decimal M6 { get; set; }
    public decimal Q2 { get; set; }
    public decimal M7 { get; set; }
    public decimal M8 { get; set; }
    public decimal M9 { get; set; }
    public decimal Q3 { get; set; }
    public decimal M10 { get; set; }
    public decimal M11 { get; set; }
    public decimal M12 { get; set; }
    public decimal Q4 { get; set; }







    public string paramRegion_Name { get; set; }
    public string pramAgent_Name { get; set; }
    public string pramSP_Name { get; set; }
    public string pramMonth_From { get; set; }
    public string pramMonth_To { get; set; }
    public string pramYear { get; set; }
    public string pramStatus { get; set; }
    public string pramProduct_Group { get; set; }
    public string pramSize { get; set; }
    public string pramUnitType { get; set; }
}


public class RPT_CLR_SALE_SUM_FORM_1_3
{
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string TAXID { get; set; }
    public string CLR_NO { get; set; }
    public string CreateDate { get; set; }
    public string SPNO { get; set; }
    public string SP_Name { get; set; }
    public string SP_Tel { get; set; }
    public string Product_Name { get; set; }
    public int QTY { get; set; }
    public decimal Total_Amt { get; set; }
    public int Point { get; set; }
    public decimal Comm { get; set; }
    public string PG_ID { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
}


public class RPT_COUNT_STOCK_FORM_5
{
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string CTNO { get; set; }
    public DateTime CreateDate { get; set; }
    public string Product_ID { get; set; }
    public string Product_Name { get; set; }
    public short QTY { get; set; }
    public string Unit { get; set; }
    public short Count_QTY { get; set; }
    public short Diff_QTY { get; set; }
    public string Remark { get; set; }
    public string RQ_Name { get; set; }
    public string AG_Admin_Name { get; set; }
    public string AG_OWNER_Name { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

}

public class RPT_Customer_BIRTH_DATE_41213
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Cust_ID { get; set; }
    public string Cust_Name { get; set; }
    public string Birth_Date { get; set; }
    public string Cust_Type { get; set; }
    public string Adress { get; set; }
    public string SP_Name { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string paramRegion_Name { get; set; }
    public string paramCV_Name { get; set; }
    public string paramBirth_Month { get; set; }
}

public class RPT_CUSTOMER_DEBT_41223
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Debt_StartDate { get; set; }
    public string Debt_EndDate { get; set; }
    public string Status { get; set; }
    public string Debt_Type { get; set; }
    public string SP_ID { get; set; }
    public string Debt_ID { get; set; }
    public string Debt_Name { get; set; }
    public string SP_Peroid { get; set; }

    public decimal SP_Amt { get; set; }
    public decimal SP_Pay_Amt { get; set; }
    public decimal SP_OST_Amt { get; set; }
    public string CT_Peroid { get; set; }
    public decimal CT_Amt { get; set; }
    public decimal CT_Pay_Amt { get; set; }
    public decimal CT_OST_Amt { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }

    public string paramRegion_Name { get; set; }
    public string pramAgent_Name { get; set; }
    public string pramSP_Name { get; set; }
    public string pramDebt_ID { get; set; }
    public string pramDebt_Name { get; set; }
    public string pramDebt_StartDate { get; set; }
    public string pramDebt_EndDate { get; set; }
    public string pramStatus { get; set; }
}


public class RPT_Customer_INFO_41211
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string Agent_Name { get; set; }
    public string Customer_ID { get; set; }
    public string Customer_Name { get; set; }
    public string CustType { get; set; }
    public string Resident { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string SP_Name { get; set; }
    public string Status { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion { get; set; }
    public string paramAgent { get; set; }
    public string paramCustomerType { get; set; }
    public string paramCustomerName { get; set; }
    public string paramSPName { get; set; }
    public string paramResidenceType { get; set; }
    public string paramStatus { get; set; }
}

public class RPT_Customer_PAY_TYPE_41212
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Cust_ID { get; set; }
    public string Customer_Name { get; set; }
    public string Status { get; set; }
    public string PayType { get; set; }
    public string Bill_INFO { get; set; }
    public string Bill_Date { get; set; }
    public string Pay_Date { get; set; }
    public string SP_Name { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string paramRegion_Name { get; set; }
    public string paramCV_Name { get; set; }
    public string paramCustomer_ID { get; set; }
    public string paramCustomer_Name { get; set; }
    public string paramStatus { get; set; }
    public string paramPayType { get; set; }
    public string paramSP_Name { get; set; }

}

public class RPT_DN_CN_41224
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string DocNo { get; set; }
    public string DocDate { get; set; }
    public decimal Total { get; set; }
    public string INV_NO { get; set; }
    public string INV_Date { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion_Name { get; set; }
    public string pramAgent_Name { get; set; }
    public string pramSP_Name { get; set; }
    public string pramDebt_ID { get; set; }
    public string pramDebt_Name { get; set; }
    public string pramDebt_StartDate { get; set; }
    public string pramDebt_EndDate { get; set; }
    public string pramStatus { get; set; }
    public string pramType { get; set; }
    public string pramDocNo { get; set; }

}

public class RPT_EXPENSE_MONTHLY_41218
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string StartMonth { get; set; }
    public string EndMonth { get; set; }
    public string Year { get; set; }
    public string Exp_ID { get; set; }
    public string Exp_Name { get; set; }
    public string Exp_Date { get; set; }
    public decimal Jan { get; set; }
    public decimal Feb { get; set; }
    public decimal Mar { get; set; }
    public decimal Apl { get; set; }
    public decimal May { get; set; }
    public decimal Jun { get; set; }
    public decimal Jul { get; set; }
    public decimal Aug { get; set; }
    public decimal Sep { get; set; }
    public decimal Oct { get; set; }
    public decimal Nov { get; set; }
    public decimal Dec { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string paramCV_Code { get; set; }
    public string paramCV_Name { get; set; }
    public string paramStartMonth { get; set; }
    public string paramEndMonth { get; set; }
    public string paramYear { get; set; }
}


public class RPT_REQUES_COMMISSION_41217
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string CR_StartDate { get; set; }
    public string CR_EndDate { get; set; }
    public string RQ_StartDate { get; set; }
    public string RQ_EndDate { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Requisition_Date { get; set; }
    public string Clearing_Date { get; set; }
    public string Commission_Requisition_Date { get; set; }
    public decimal Comm_Amt { get; set; }
    public string Status { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string paramCV_Code { get; set; }
    public string paramCV_Name { get; set; }
    public string paramSP_ID { get; set; }
    public string paramSP_Name { get; set; }
    public string paramCR_StartDate { get; set; }
    public string paramCR_EndDate { get; set; }
    public string paramRQ_StartDate { get; set; }
    public string paramRQ_EndDate { get; set; }
}

public class RPT_REV_EXP_DAILY_41220
{
    public string Region { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Tax_ID { get; set; }
    public string ACC_Type { get; set; }
    public string ACC_Code { get; set; }
    public string ACC_Name { get; set; }
    public decimal RV_EX_Amount { get; set; }
    public decimal SP_Cash { get; set; }
    public decimal Credit_Amount { get; set; }
    public decimal Net_Sales_Amount { get; set; }
    public decimal Balance_Outstanding { get; set; }
    public string Post_Date { get; set; }
    public string paramCV_Name { get; set; }
    public string paramPostDate_From { get; set; }
    public string paramPostDate_To { get; set; }
    public string Remark { get; set; }
}


public class RPT_RQ_CASH_FORM_4
{
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string RENo { get; set; }
    public string CreateDate { get; set; }
    public string SPNO { get; set; }
    public string SP_Name { get; set; }
    public string SP_Tel { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public decimal Total_Amt { get; set; }
    public decimal Comm_Balance { get; set; }
    public decimal Outstanding_Amt { get; set; }
    public decimal Remain_Amt { get; set; }
    public string RQ_Name { get; set; }
    public string AG_Admin_Name { get; set; }
    public string AG_OWNER_Name { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
}


public class RPT_SALE_AMT_DAILY_41220A
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Create_Date { get; set; }
    public string Product_Group { get; set; }
    public string Size { get; set; }
    public string Product_ID { get; set; }
    public string Product_Name { get; set; }
    public decimal Sales_Amt { get; set; }
    public decimal Cash_Amt { get; set; }
    public decimal Pay_Amt { get; set; }
    public decimal Confirm_Pay_Amt { get; set; }
    public decimal Current_Balance { get; set; }
    public decimal Commission { get; set; }
    public decimal REV_Agent { get; set; }
    public decimal Cash_deposit { get; set; }
    public decimal Car_Rent { get; set; }
    public decimal Installment { get; set; }
    public decimal Other { get; set; }
    public string Remark { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public decimal SizeA1 { get; set; }
    public decimal SizeA2 { get; set; }
    public decimal SizeA3 { get; set; }
    public decimal SizeA4 { get; set; }
    public decimal SizeA5 { get; set; }
    public decimal SizeA6 { get; set; }
    public decimal SizeA7 { get; set; }
    public decimal SizeA8 { get; set; }
    public decimal SizeA9 { get; set; }
    public decimal SizeA10 { get; set; }
    public decimal SizeA11 { get; set; }
    public decimal SizeA12 { get; set; }
    public decimal SizeA13 { get; set; }
    public decimal SizeA14 { get; set; }
    public decimal SizeA15 { get; set; }
    public decimal SizeA16 { get; set; }
    public decimal SizeA17 { get; set; }
    public decimal SizeA18 { get; set; }
    public decimal SizeA19 { get; set; }
    public decimal SizeA20 { get; set; }
    public decimal SizeA21 { get; set; }
    public decimal SizeA22 { get; set; }
    public decimal SizeA23 { get; set; }
    public decimal SizeA24 { get; set; }
    public string paramPostDate_From { get; set; }
    public string paramPostDate_To { get; set; }
    public decimal Total_Other_Expense { get; set; }
}

public class RPT_SALE_AMT_DAILY_41220
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Create_Date { get; set; }
    public string Product_Group { get; set; }
    public string Size { get; set; }
    public string Product_ID { get; set; }
    public string Product_Name { get; set; }
    public decimal Sales_Amt { get; set; }
    public decimal Cash_Amt { get; set; }
    public decimal Pay_Amt { get; set; }
    public decimal Confirm_Pay_Amt { get; set; }
    public decimal Current_Balance { get; set; }
    public decimal Commission { get; set; }
    public decimal REV_Agent { get; set; }
    public decimal Cash_deposit { get; set; }
    public decimal Car_Rent { get; set; }
    public decimal Installment { get; set; }
    public decimal Other { get; set; }
    public string Remark { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public decimal SizeA1 { get; set; }
    public decimal SizeA2 { get; set; }
    public decimal SizeA3 { get; set; }
    public decimal SizeA4 { get; set; }
    public decimal SizeA5 { get; set; }
    public decimal SizeA6 { get; set; }
    public decimal SizeA7 { get; set; }
    public decimal SizeA8 { get; set; }
    public decimal SizeA9 { get; set; }
    public decimal SizeA10 { get; set; }
    public decimal SizeA11 { get; set; }
    public decimal SizeA12 { get; set; }
    public decimal SizeA13 { get; set; }
    public decimal SizeA14 { get; set; }
    public decimal SizeA15 { get; set; }
    public decimal SizeA16 { get; set; }
    public decimal SizeA17 { get; set; }
    public decimal SizeA18 { get; set; }
    public decimal SizeA19 { get; set; }
    public decimal SizeA20 { get; set; }
    public decimal SizeA21 { get; set; }
    public decimal SizeA22 { get; set; }
    public decimal SizeA23 { get; set; }
    public decimal SizeA24 { get; set; }
    public string paramPostDate_From { get; set; }
    public string paramPostDate_To { get; set; }
}


public class RPT_SO_BY_PRODUCT_4122
{
    public string Region_Name { get; set; }
    public string CV_CODE { get; set; }
    public string Agent_Name { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string Product_group_id { get; set; }
    public string PID { get; set; }
    public string PName { get; set; }
    public short Size { get; set; }
    public string Unit { get; set; }
    public int QTY { get; set; }
    public decimal Total_AMT { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string Temp6 { get; set; }
    public string Temp7 { get; set; }
    public string Temp8 { get; set; }

    public string paramRegion { get; set; }
    public string paramAgent { get; set; }
    public string paramDateFrom { get; set; }
    public string paramDateTo { get; set; }
    public string paramProductGroup { get; set; }
    public string paramSize { get; set; }
    public string paramProductName { get; set; }
}


public class RPT_SO_TARGET_MONTH_4123
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Year { get; set; }
    public decimal Target { get; set; }
    public decimal SO_Amount { get; set; }
    public decimal Diff_Amount { get; set; }
    public int Diff_Percent { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
}

public class RPT_SO_TARGET_QUARTER_4124
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
    public decimal Target { get; set; }
    public decimal SO_Amount { get; set; }
    public decimal Diff_Amount { get; set; }
    public int Diff_Percent { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
}


public class RPT_SO_TARGET_YEAR_4125_New
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
    public decimal Target { get; set; }
    public decimal SO_Amount { get; set; }
    public decimal Diff_Amount { get; set; }
    public int Diff_Percent { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion { get; set; }
    public string paramAgent { get; set; }
    public string paramYear { get; set; }

    public decimal M1_Target { get; set;}
    public decimal M2_Target { get; set; }
    public decimal M3_Target { get; set; }
    public decimal M4_Target { get; set; }
    public decimal M5_Target { get; set; }
    public decimal M6_Target { get; set; }
    public decimal M7_Target { get; set; }
    public decimal M8_Target { get; set; }
    public decimal M9_Target { get; set; }
    public decimal M10_Target { get; set; }
    public decimal M11_Target { get; set; }
    public decimal M12_Target { get; set; }
    public decimal P1_Target { get; set; }
    public decimal P2_Target { get; set; }
    public decimal P3_Target { get; set; }
    public decimal P4_Target { get; set; }


    public decimal M1_Act { get; set; }
    public decimal M2_Act { get; set; }
    public decimal M3_Act { get; set; }
    public decimal M4_Act { get; set; }
    public decimal M5_Act { get; set; }
    public decimal M6_Act { get; set; }
    public decimal M7_Act { get; set; }
    public decimal M8_Act { get; set; }
    public decimal M9_Act { get; set; }
    public decimal M10_Act { get; set; }
    public decimal M11_Act { get; set; }
    public decimal M12_Act { get; set; }
    public decimal P1_Act { get; set; }
    public decimal P2_Act { get; set; }
    public decimal P3_Act { get; set; }
    public decimal P4_Act { get; set; }


}
public class RPT_SO_TARGET_YEAR_4125
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
    public decimal Target { get; set; }
    public decimal SO_Amount { get; set; }
    public decimal Diff_Amount { get; set; }
    public int Diff_Percent { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion { get; set; }
    public string paramAgent { get; set; }
    public string paramYear { get; set; }

    public decimal M1_Target { get; set; }
    public decimal M2_Target { get; set; }
    public decimal M3_Target { get; set; }
    public decimal M4_Target { get; set; }
    public decimal M5_Target { get; set; }
    public decimal M6_Target { get; set; }
    public decimal M7_Target { get; set; }
    public decimal M8_Target { get; set; }
    public decimal M9_Target { get; set; }
    public decimal M10_Target { get; set; }
    public decimal M11_Target { get; set; }
    public decimal M12_Target { get; set; }
    public decimal P1_Target { get; set; }
    public decimal P2_Target { get; set; }
    public decimal P3_Target { get; set; }
    public decimal P4_Target { get; set; }


    public decimal M1_Act { get; set; }
    public decimal M2_Act { get; set; }
    public decimal M3_Act { get; set; }
    public decimal M4_Act { get; set; }
    public decimal M5_Act { get; set; }
    public decimal M6_Act { get; set; }
    public decimal M7_Act { get; set; }
    public decimal M8_Act { get; set; }
    public decimal M9_Act { get; set; }
    public decimal M10_Act { get; set; }
    public decimal M11_Act { get; set; }
    public decimal M12_Act { get; set; }
    public decimal P1_Act { get; set; }
    public decimal P2_Act { get; set; }
    public decimal P3_Act { get; set; }
    public decimal P4_Act { get; set; }

    public string DataType { get; set; }
}

public class RPT_SP_COMMISSION_41216
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Product_Group { get; set; }
    public short Size { get; set; }
    public string Prod_ID { get; set; }
    public string Prod_Name { get; set; }
    public short QTY { get; set; }
    public decimal Comm_Amt { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string paramCV_Code { get; set; }
    public string paramCV_Name { get; set; }
    public string paramStartDate { get; set; }
    public string paramEndDate { get; set; }
    public string paramSP_ID { get; set; }
    public string paramSP_Name { get; set; }
    public string paramProduct_Group { get; set; }
    public string paramSize { get; set; }
}

public class RPT_SP_DEBT_41221
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Debt_StartDate { get; set; }
    public string Debt_EndDate { get; set; }
    public string Status { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Debt_ID { get; set; }
    public string Debt_Name { get; set; }
    public string Debt_Date { get; set; }
    public decimal Debt_Amt { get; set; }
    public decimal Pay_Amt { get; set; }
    public decimal Outstanding_Amt { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion_Name { get; set; }
    public string pramAgent_Name { get; set; }
    public string pramSP_Name { get; set; }
    public string pramDebt_ID { get; set; }
    public string pramDebt_Name { get; set; }
    public string pramDebt_StartDate { get; set; }
    public string pramDebt_EndDate { get; set; }
    public string pramStatus { get; set;}
}


public class RPT_STOCK_MOV_4127
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string ProductGroup { get; set; }
    public string Proudct_ID { get; set; }
    public string Product_Name { get; set; }
    public short Size { get; set; }
    public short CF_QTY { get; set; }
    public int IN { get; set; }
    public int OUT_RQ { get; set; }
    public int OUT_OTHER { get; set; }
    public short BF_QTY { get; set; }
    public string Remark { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }


    public string pramCV_Code { get; set; }
    public string pramAgent_Name { get; set; }
    public string pramDate_From { get; set; }
    public string pramDate_To { get; set; }
    public string pramProduct_group { get; set; }
    public string pramSize { get; set; }
}

public class RPT_SUMM_EXPENSE_41219
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Exp_Date { get; set; }
    public string Post_No { get; set; }
    public string Exp_No { get; set; }
    public string ACC_ID { get; set; }
    public string ACC_Name { get; set; }
    public decimal Exp_Amt { get; set; }
    public decimal Rev_Amt { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string paramCV_Code { get; set; }
    public string paramCV_Name { get; set; }
}


public class RPT_SUMM_MEMBER__41210
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Type { get; set; }
    public int Connecting { get; set; }
    public int DisConnect_Temporary { get; set; }
    public int DisConnected { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
}

public class RPT_SUMM_RQ_OTHER__4129
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Request_Date { get; set; }
    public string User_Name { get; set; }
    public string Proudct_ID { get; set; }
    public string Product_Name { get; set; }
    public short Size { get; set; }
    public short QTY { get; set; }
    public string Unit { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
  
    public string paramDateFrom { get; set; }
    public string paramDateTo { get; set; }
    public string paramProductGroup { get; set; }
    public string paramSP { get; set; }
    public string paramReason { get; set; }
}

public class RPT_SUMM_RQ_SP_4126
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string ProductGroup { get; set; }
    public string Proudct_ID { get; set; }
    public string Product_Name { get; set; }
    public short Size { get; set; }
    public short RQ_QTY { get; set; }
    public decimal SP_Price { get; set; }
    public decimal Total_Amt { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion { get; set; }
    public string paramAgent { get; set; }
    public string paramSP { get; set; }
    public string paramDateFrom { get; set; }
    public string paramDateTo { get; set; }
    public string paramProductGroup { get; set; }
    public string paramSize { get; set; }
}

public class RPT_SUMM_SO_PG_41214
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Prod_Group { get; set; }
    public short Size { get; set; }
    public int QTY { get; set; }
    public decimal Total_Amt { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string paramRegion_Name { get; set; }
    public string paramCV_Name { get; set; }
    public string paramStartDate { get; set; }
    public string paramEndDate { get; set; }
    public string paramSP_ID { get; set; }
    public string paramSP_Name { get; set; }
    public string paramProd_Group { get; set; }
    public string paramSize { get; set; }
}

public class RPT_SUMM_SP_BY_AGENT_41215
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Position { get; set; }
    public string Status { get; set; }
    public string StartDate { get; set; }
    public string Join_Date { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }
    public string paramRegion_Name { get; set; }
    public string paramCV_Name { get; set; }
    public string paramSP_ID { get; set; }
    public string paramSP_Name { get; set; }
    public string paramStartDate_From { get; set; }
    public string paramStartDate_To { get; set; }
    public string paramJoinDate_From { get; set; }
    public string paramJoinDate_To { get; set; }
    public string paramStatus { get; set; }
    public string paramPosition { get; set; }
}

public class RPT_SP_POINT_41225
{
    public string Region_Name { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string Address { get; set; }
    public string CV_Phone { get; set; }
    public string TAXID { get; set; }
    public string Region { get; set; }  
    public string SP_ID { get; set; }
    public string PS_Name { get; set; }
    public string Date { get; set; }
    public string Product_group_ID { get; set; }
    public short Size { get; set; }
    public string Product_ID { get; set; }
    public string Product_Name { get; set; }
    public short Sales_Qty { get; set; }
    public short Point { get; set; }
    public short Tota_Point { get; set; }
    public string Temp0 { get; set; }
    public string Temp1 { get; set; }
    public string Temp2 { get; set; }
    public string Temp3 { get; set; }
    public string Temp4 { get; set; }
    public string Temp5 { get; set; }

    public string paramRegion_Name { get; set; }
    public string paramCV_Name { get; set; }
    public string paramMonth_From { get; set; }
    public string paramMonth_To { get; set; }
}

public class RPT_AGENT_POINT_41226
{
    public string Region { get; set; }
    public string CV_Code { get; set; }
    public string CV_Name { get; set; }
    public string SP_ID { get; set; }
    public string SP_Name { get; set; }
    public string Product_group_ID { get; set; }
    public short Size { get; set; }
    public string ClearingYear { get; set; }
    public string ClearingMonth { get; set; }
    public int D1 { get; set; }
    public int D2 { get; set; }
    public int D3 { get; set; }
    public int D4 { get; set; }
    public int D5 { get; set; }
    public int D6 { get; set; }
    public int D7 { get; set; }
    public int D8 { get; set; }
    public int D9 { get; set; }
    public int D10 { get; set; }
    public int D11 { get; set; }
    public int D12 { get; set; }
    public int D13 { get; set; }
    public int D14 { get; set; }
    public int D15 { get; set; }
    public int D16 { get; set; }
    public int D17 { get; set; }
    public int D18 { get; set; }
    public int D19 { get; set; }
    public int D20 { get; set; }
    public int D21 { get; set; }
    public int D22 { get; set; }
    public int D23 { get; set; }
    public int D24 { get; set; }
    public int D25 { get; set; }
    public int D26 { get; set; }
    public int D27 { get; set; }
    public int D28 { get; set; }
    public int D29 { get; set; }
    public int D30 { get; set; }
    public int D31 { get; set; }
    public int TOTAL_POINT { get; set; }

    public string paramMonth_From { get; set; }
    public string paramMonth_To { get; set; }
    public string paramYear_From { get; set; }
    public string paramYear_To{ get; set; }
    public string paramCV_Name { get; set; }
    public string paramCV_Code { get; set; }
    public string paramSP_Name { get; set; }
    public string paramSP_ID { get; set; }


    public int A200_D1 { get; set; }
    public int A200_D2 { get; set; }
    public int A200_D3 { get; set; }
    public int A200_D4 { get; set; }
    public int A200_D5 { get; set; }
    public int A200_D6 { get; set; }
    public int A200_D7 { get; set; }
    public int A200_D8 { get; set; }
    public int A200_D9 { get; set; }
    public int A200_D10 { get; set; }
    public int A200_D11 { get; set; }
    public int A200_D12 { get; set; }
    public int A200_D13 { get; set; }
    public int A200_D14 { get; set; }
    public int A200_D15 { get; set; }
    public int A200_D16 { get; set; }
    public int A200_D17 { get; set; }
    public int A200_D18 { get; set; }
    public int A200_D19 { get; set; }
    public int A200_D20 { get; set; }
    public int A200_D21 { get; set; }
    public int A200_D22 { get; set; }
    public int A200_D23 { get; set; }
    public int A200_D24 { get; set; }
    public int A200_D25 { get; set; }
    public int A200_D26 { get; set; }
    public int A200_D27 { get; set; }
    public int A200_D28 { get; set; }
    public int A200_D29 { get; set; }
    public int A200_D30 { get; set; }
    public int A200_D31 { get; set; }

    public int A450_D1 { get; set; }
    public int A450_D2 { get; set; }
    public int A450_D3 { get; set; }
    public int A450_D4 { get; set; }
    public int A450_D5 { get; set; }
    public int A450_D6 { get; set; }
    public int A450_D7 { get; set; }
    public int A450_D8 { get; set; }
    public int A450_D9 { get; set; }
    public int A450_D10 { get; set; }
    public int A450_D11 { get; set; }
    public int A450_D12 { get; set; }
    public int A450_D13 { get; set; }
    public int A450_D14 { get; set; }
    public int A450_D15 { get; set; }
    public int A450_D16 { get; set; }
    public int A450_D17 { get; set; }
    public int A450_D18 { get; set; }
    public int A450_D19 { get; set; }
    public int A450_D20 { get; set; }
    public int A450_D21 { get; set; }
    public int A450_D22 { get; set; }
    public int A450_D23 { get; set; }
    public int A450_D24 { get; set; }
    public int A450_D25 { get; set; }
    public int A450_D26 { get; set; }
    public int A450_D27 { get; set; }
    public int A450_D28 { get; set; }
    public int A450_D29 { get; set; }
    public int A450_D30 { get; set; }
    public int A450_D31 { get; set; }

    public int A830_D1 { get; set; }
    public int A830_D2 { get; set; }
    public int A830_D3 { get; set; }
    public int A830_D4 { get; set; }
    public int A830_D5 { get; set; }
    public int A830_D6 { get; set; }
    public int A830_D7 { get; set; }
    public int A830_D8 { get; set; }
    public int A830_D9 { get; set; }
    public int A830_D10 { get; set; }
    public int A830_D11 { get; set; }
    public int A830_D12 { get; set; }
    public int A830_D13 { get; set; }
    public int A830_D14 { get; set; }
    public int A830_D15 { get; set; }
    public int A830_D16 { get; set; }
    public int A830_D17 { get; set; }
    public int A830_D18 { get; set; }
    public int A830_D19 { get; set; }
    public int A830_D20 { get; set; }
    public int A830_D21 { get; set; }
    public int A830_D22 { get; set; }
    public int A830_D23 { get; set; }
    public int A830_D24 { get; set; }
    public int A830_D25 { get; set; }
    public int A830_D26 { get; set; }
    public int A830_D27 { get; set; }
    public int A830_D28 { get; set; }
    public int A830_D29 { get; set; }
    public int A830_D30 { get; set; }
    public int A830_D31 { get; set; }

    public int A2000_D1 { get; set; }
    public int A2000_D2 { get; set; }
    public int A2000_D3 { get; set; }
    public int A2000_D4 { get; set; }
    public int A2000_D5 { get; set; }
    public int A2000_D6 { get; set; }
    public int A2000_D7 { get; set; }
    public int A2000_D8 { get; set; }
    public int A2000_D9 { get; set; }
    public int A2000_D10 { get; set; }
    public int A2000_D11 { get; set; }
    public int A2000_D12 { get; set; }
    public int A2000_D13 { get; set; }
    public int A2000_D14 { get; set; }
    public int A2000_D15 { get; set; }
    public int A2000_D16 { get; set; }
    public int A2000_D17 { get; set; }
    public int A2000_D18 { get; set; }
    public int A2000_D19 { get; set; }
    public int A2000_D20 { get; set; }
    public int A2000_D21 { get; set; }
    public int A2000_D22 { get; set; }
    public int A2000_D23 { get; set; }
    public int A2000_D24 { get; set; }
    public int A2000_D25 { get; set; }
    public int A2000_D26 { get; set; }
    public int A2000_D27 { get; set; }
    public int A2000_D28 { get; set; }
    public int A2000_D29 { get; set; }
    public int A2000_D30 { get; set; }
    public int A2000_D31 { get; set; }

    public int A5000_D1 { get; set; }
    public int A5000_D2 { get; set; }
    public int A5000_D3 { get; set; }
    public int A5000_D4 { get; set; }
    public int A5000_D5 { get; set; }
    public int A5000_D6 { get; set; }
    public int A5000_D7 { get; set; }
    public int A5000_D8 { get; set; }
    public int A5000_D9 { get; set; }
    public int A5000_D10 { get; set; }
    public int A5000_D11 { get; set; }
    public int A5000_D12 { get; set; }
    public int A5000_D13 { get; set; }
    public int A5000_D14 { get; set; }
    public int A5000_D15 { get; set; }
    public int A5000_D16 { get; set; }
    public int A5000_D17 { get; set; }
    public int A5000_D18 { get; set; }
    public int A5000_D19 { get; set; }
    public int A5000_D20 { get; set; }
    public int A5000_D21 { get; set; }
    public int A5000_D22 { get; set; }
    public int A5000_D23 { get; set; }
    public int A5000_D24 { get; set; }
    public int A5000_D25 { get; set; }
    public int A5000_D26 { get; set; }
    public int A5000_D27 { get; set; }
    public int A5000_D28 { get; set; }
    public int A5000_D29 { get; set; }
    public int A5000_D30 { get; set; }
    public int A5000_D31 { get; set; }

    public int A350_D1 { get; set; }
    public int A350_D2 { get; set; }
    public int A350_D3 { get; set; }
    public int A350_D4 { get; set; }
    public int A350_D5 { get; set; }
    public int A350_D6 { get; set; }
    public int A350_D7 { get; set; }
    public int A350_D8 { get; set; }
    public int A350_D9 { get; set; }
    public int A350_D10 { get; set; }
    public int A350_D11 { get; set; }
    public int A350_D12 { get; set; }
    public int A350_D13 { get; set; }
    public int A350_D14 { get; set; }
    public int A350_D15 { get; set; }
    public int A350_D16 { get; set; }
    public int A350_D17 { get; set; }
    public int A350_D18 { get; set; }
    public int A350_D19 { get; set; }
    public int A350_D20 { get; set; }
    public int A350_D21 { get; set; }
    public int A350_D22 { get; set; }
    public int A350_D23 { get; set; }
    public int A350_D24 { get; set; }
    public int A350_D25 { get; set; }
    public int A350_D26 { get; set; }
    public int A350_D27 { get; set; }
    public int A350_D28 { get; set; }
    public int A350_D29 { get; set; }
    public int A350_D30 { get; set; }
    public int A350_D31 { get; set; }

    public int A180_D1 { get; set; }
    public int A180_D2 { get; set; }
    public int A180_D3 { get; set; }
    public int A180_D4 { get; set; }
    public int A180_D5 { get; set; }
    public int A180_D6 { get; set; }
    public int A180_D7 { get; set; }
    public int A180_D8 { get; set; }
    public int A180_D9 { get; set; }
    public int A180_D10 { get; set; }
    public int A180_D11 { get; set; }
    public int A180_D12 { get; set; }
    public int A180_D13 { get; set; }
    public int A180_D14 { get; set; }
    public int A180_D15 { get; set; }
    public int A180_D16 { get; set; }
    public int A180_D17 { get; set; }
    public int A180_D18 { get; set; }
    public int A180_D19 { get; set; }
    public int A180_D20 { get; set; }
    public int A180_D21 { get; set; }
    public int A180_D22 { get; set; }
    public int A180_D23 { get; set; }
    public int A180_D24 { get; set; }
    public int A180_D25 { get; set; }
    public int A180_D26 { get; set; }
    public int A180_D27 { get; set; }
    public int A180_D28 { get; set; }
    public int A180_D29 { get; set; }
    public int A180_D30 { get; set; }
    public int A180_D31 { get; set; }

    public int B140_D1 { get; set; }
    public int B140_D2 { get; set; }
    public int B140_D3 { get; set; }
    public int B140_D4 { get; set; }
    public int B140_D5 { get; set; }
    public int B140_D6 { get; set; }
    public int B140_D7 { get; set; }
    public int B140_D8 { get; set; }
    public int B140_D9 { get; set; }
    public int B140_D10 { get; set; }
    public int B140_D11 { get; set; }
    public int B140_D12 { get; set; }
    public int B140_D13 { get; set; }
    public int B140_D14 { get; set; }
    public int B140_D15 { get; set; }
    public int B140_D16 { get; set; }
    public int B140_D17 { get; set; }
    public int B140_D18 { get; set; }
    public int B140_D19 { get; set; }
    public int B140_D20 { get; set; }
    public int B140_D21 { get; set; }
    public int B140_D22 { get; set; }
    public int B140_D23 { get; set; }
    public int B140_D24 { get; set; }
    public int B140_D25 { get; set; }
    public int B140_D26 { get; set; }
    public int B140_D27 { get; set; }
    public int B140_D28 { get; set; }
    public int B140_D29 { get; set; }
    public int B140_D30 { get; set; }
    public int B140_D31 { get; set; }

    public int B145_D1 { get; set; }
    public int B145_D2 { get; set; }
    public int B145_D3 { get; set; }
    public int B145_D4 { get; set; }
    public int B145_D5 { get; set; }
    public int B145_D6 { get; set; }
    public int B145_D7 { get; set; }
    public int B145_D8 { get; set; }
    public int B145_D9 { get; set; }
    public int B145_D10 { get; set; }
    public int B145_D11 { get; set; }
    public int B145_D12 { get; set; }
    public int B145_D13 { get; set; }
    public int B145_D14 { get; set; }
    public int B145_D15 { get; set; }
    public int B145_D16 { get; set; }
    public int B145_D17 { get; set; }
    public int B145_D18 { get; set; }
    public int B145_D19 { get; set; }
    public int B145_D20 { get; set; }
    public int B145_D21 { get; set; }
    public int B145_D22 { get; set; }
    public int B145_D23 { get; set; }
    public int B145_D24 { get; set; }
    public int B145_D25 { get; set; }
    public int B145_D26 { get; set; }
    public int B145_D27 { get; set; }
    public int B145_D28 { get; set; }
    public int B145_D29 { get; set; }
    public int B145_D30 { get; set; }
    public int B145_D31 { get; set; }

    public int C100_D1 { get; set; }
    public int C100_D2 { get; set; }
    public int C100_D3 { get; set; }
    public int C100_D4 { get; set; }
    public int C100_D5 { get; set; }
    public int C100_D6 { get; set; }
    public int C100_D7 { get; set; }
    public int C100_D8 { get; set; }
    public int C100_D9 { get; set; }
    public int C100_D10 { get; set; }
    public int C100_D11 { get; set; }
    public int C100_D12 { get; set; }
    public int C100_D13 { get; set; }
    public int C100_D14 { get; set; }
    public int C100_D15 { get; set; }
    public int C100_D16 { get; set; }
    public int C100_D17 { get; set; }
    public int C100_D18 { get; set; }
    public int C100_D19 { get; set; }
    public int C100_D20 { get; set; }
    public int C100_D21 { get; set; }
    public int C100_D22 { get; set; }
    public int C100_D23 { get; set; }
    public int C100_D24 { get; set; }
    public int C100_D25 { get; set; }
    public int C100_D26 { get; set; }
    public int C100_D27 { get; set; }
    public int C100_D28 { get; set; }
    public int C100_D29 { get; set; }
    public int C100_D30 { get; set; }
    public int C100_D31 { get; set; }

    public int C155_D1 { get; set; }
    public int C155_D2 { get; set; }
    public int C155_D3 { get; set; }
    public int C155_D4 { get; set; }
    public int C155_D5 { get; set; }
    public int C155_D6 { get; set; }
    public int C155_D7 { get; set; }
    public int C155_D8 { get; set; }
    public int C155_D9 { get; set; }
    public int C155_D10 { get; set; }
    public int C155_D11 { get; set; }
    public int C155_D12 { get; set; }
    public int C155_D13 { get; set; }
    public int C155_D14 { get; set; }
    public int C155_D15 { get; set; }
    public int C155_D16 { get; set; }
    public int C155_D17 { get; set; }
    public int C155_D18 { get; set; }
    public int C155_D19 { get; set; }
    public int C155_D20 { get; set; }
    public int C155_D21 { get; set; }
    public int C155_D22 { get; set; }
    public int C155_D23 { get; set; }
    public int C155_D24 { get; set; }
    public int C155_D25 { get; set; }
    public int C155_D26 { get; set; }
    public int C155_D27 { get; set; }
    public int C155_D28 { get; set; }
    public int C155_D29 { get; set; }
    public int C155_D30 { get; set; }
    public int C155_D31 { get; set; }

    public int C330_D1 { get; set; }
    public int C330_D2 { get; set; }
    public int C330_D3 { get; set; }
    public int C330_D4 { get; set; }
    public int C330_D5 { get; set; }
    public int C330_D6 { get; set; }
    public int C330_D7 { get; set; }
    public int C330_D8 { get; set; }
    public int C330_D9 { get; set; }
    public int C330_D10 { get; set; }
    public int C330_D11 { get; set; }
    public int C330_D12 { get; set; }
    public int C330_D13 { get; set; }
    public int C330_D14 { get; set; }
    public int C330_D15 { get; set; }
    public int C330_D16 { get; set; }
    public int C330_D17 { get; set; }
    public int C330_D18 { get; set; }
    public int C330_D19 { get; set; }
    public int C330_D20 { get; set; }
    public int C330_D21 { get; set; }
    public int C330_D22 { get; set; }
    public int C330_D23 { get; set; }
    public int C330_D24 { get; set; }
    public int C330_D25 { get; set; }
    public int C330_D26 { get; set; }
    public int C330_D27 { get; set; }
    public int C330_D28 { get; set; }
    public int C330_D29 { get; set; }
    public int C330_D30 { get; set; }
    public int C330_D31 { get; set; }

    public int D90_D1 { get; set; }
    public int D90_D2 { get; set; }
    public int D90_D3 { get; set; }
    public int D90_D4 { get; set; }
    public int D90_D5 { get; set; }
    public int D90_D6 { get; set; }
    public int D90_D7 { get; set; }
    public int D90_D8 { get; set; }
    public int D90_D9 { get; set; }
    public int D90_D10 { get; set; }
    public int D90_D11 { get; set; }
    public int D90_D12 { get; set; }
    public int D90_D13 { get; set; }
    public int D90_D14 { get; set; }
    public int D90_D15 { get; set; }
    public int D90_D16 { get; set; }
    public int D90_D17 { get; set; }
    public int D90_D18 { get; set; }
    public int D90_D19 { get; set; }
    public int D90_D20 { get; set; }
    public int D90_D21 { get; set; }
    public int D90_D22 { get; set; }
    public int D90_D23 { get; set; }
    public int D90_D24 { get; set; }
    public int D90_D25 { get; set; }
    public int D90_D26 { get; set; }
    public int D90_D27 { get; set; }
    public int D90_D28 { get; set; }
    public int D90_D29 { get; set; }
    public int D90_D30 { get; set; }
    public int D90_D31 { get; set; }

    public int D110_D1 { get; set; }
    public int D110_D2 { get; set; }
    public int D110_D3 { get; set; }
    public int D110_D4 { get; set; }
    public int D110_D5 { get; set; }
    public int D110_D6 { get; set; }
    public int D110_D7 { get; set; }
    public int D110_D8 { get; set; }
    public int D110_D9 { get; set; }
    public int D110_D10 { get; set; }
    public int D110_D11 { get; set; }
    public int D110_D12 { get; set; }
    public int D110_D13 { get; set; }
    public int D110_D14 { get; set; }
    public int D110_D15 { get; set; }
    public int D110_D16 { get; set; }
    public int D110_D17 { get; set; }
    public int D110_D18 { get; set; }
    public int D110_D19 { get; set; }
    public int D110_D20 { get; set; }
    public int D110_D21 { get; set; }
    public int D110_D22 { get; set; }
    public int D110_D23 { get; set; }
    public int D110_D24 { get; set; }
    public int D110_D25 { get; set; }
    public int D110_D26 { get; set; }
    public int D110_D27 { get; set; }
    public int D110_D28 { get; set; }
    public int D110_D29 { get; set; }
    public int D110_D30 { get; set; }
    public int D110_D31 { get; set; }

    public int D135_D1 { get; set; }
    public int D135_D2 { get; set; }
    public int D135_D3 { get; set; }
    public int D135_D4 { get; set; }
    public int D135_D5 { get; set; }
    public int D135_D6 { get; set; }
    public int D135_D7 { get; set; }
    public int D135_D8 { get; set; }
    public int D135_D9 { get; set; }
    public int D135_D10 { get; set; }
    public int D135_D11 { get; set; }
    public int D135_D12 { get; set; }
    public int D135_D13 { get; set; }
    public int D135_D14 { get; set; }
    public int D135_D15 { get; set; }
    public int D135_D16 { get; set; }
    public int D135_D17 { get; set; }
    public int D135_D18 { get; set; }
    public int D135_D19 { get; set; }
    public int D135_D20 { get; set; }
    public int D135_D21 { get; set; }
    public int D135_D22 { get; set; }
    public int D135_D23 { get; set; }
    public int D135_D24 { get; set; }
    public int D135_D25 { get; set; }
    public int D135_D26 { get; set; }
    public int D135_D27 { get; set; }
    public int D135_D28 { get; set; }
    public int D135_D29 { get; set; }
    public int D135_D30 { get; set; }
    public int D135_D31 { get; set; }

    public int D450_D1 { get; set; }
    public int D450_D2 { get; set; }
    public int D450_D3 { get; set; }
    public int D450_D4 { get; set; }
    public int D450_D5 { get; set; }
    public int D450_D6 { get; set; }
    public int D450_D7 { get; set; }
    public int D450_D8 { get; set; }
    public int D450_D9 { get; set; }
    public int D450_D10 { get; set; }
    public int D450_D11 { get; set; }
    public int D450_D12 { get; set; }
    public int D450_D13 { get; set; }
    public int D450_D14 { get; set; }
    public int D450_D15 { get; set; }
    public int D450_D16 { get; set; }
    public int D450_D17 { get; set; }
    public int D450_D18 { get; set; }
    public int D450_D19 { get; set; }
    public int D450_D20 { get; set; }
    public int D450_D21 { get; set; }
    public int D450_D22 { get; set; }
    public int D450_D23 { get; set; }
    public int D450_D24 { get; set; }
    public int D450_D25 { get; set; }
    public int D450_D26 { get; set; }
    public int D450_D27 { get; set; }
    public int D450_D28 { get; set; }
    public int D450_D29 { get; set; }
    public int D450_D30 { get; set; }
    public int D450_D31 { get; set; }


    public int D500_D1 { get; set; }
    public int D500_D2 { get; set; }
    public int D500_D3 { get; set; }
    public int D500_D4 { get; set; }
    public int D500_D5 { get; set; }
    public int D500_D6 { get; set; }
    public int D500_D7 { get; set; }
    public int D500_D8 { get; set; }
    public int D500_D9 { get; set; }
    public int D500_D10 { get; set; }
    public int D500_D11 { get; set; }
    public int D500_D12 { get; set; }
    public int D500_D13 { get; set; }
    public int D500_D14 { get; set; }
    public int D500_D15 { get; set; }
    public int D500_D16 { get; set; }
    public int D500_D17 { get; set; }
    public int D500_D18 { get; set; }
    public int D500_D19 { get; set; }
    public int D500_D20 { get; set; }
    public int D500_D21 { get; set; }
    public int D500_D22 { get; set; }
    public int D500_D23 { get; set; }
    public int D500_D24 { get; set; }
    public int D500_D25 { get; set; }
    public int D500_D26 { get; set; }
    public int D500_D27 { get; set; }
    public int D500_D28 { get; set; }
    public int D500_D29 { get; set; }
    public int D500_D30 { get; set; }
    public int D500_D31 { get; set; }



}




