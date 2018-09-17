using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for RT_PO_DOC
/// </summary>
public class RT_PO_DOC
{
    
    
    private dbo_ProductClass m_Product;
    private dbo_AgentClass m_Agent;
    private dbo_OrderingClass m_Ordering;
    private dbo_OrderingDetailClass m_OrderingDetail;

    #region AG
    //AG
    private String AG_CV_Code;
    private String AG_Prefix_ID;
    private String AG_First_Name;
    private String AG_Last_Name;
    private String AG_Agent_Type_ID;
    private String AG_Home_Phone_No;
    private String AG_Mobile;
    private String AG_Tax_ID;
    private String AG_Email;
    private String AG_Fax;
    private String AG_Concession_Area;
    private String AG_Owner_First_Name;
    private String AG_Owner_Last_Name;
    private String AG_Owner_Phone_No1;
    private String AG_Owner_Phone_No2;
    private String AG_Contact_First_Name;
    private String AG_Contact_Last_Name;
    private String AG_Contact_Phone_No1;
    private String AG_Contact_Phone_No2;

    private String AG_SD_ID;
    private String AG_SM_ID;
    private String AG_DM_ID;
    private String AG_APV_ID;
    private String AG_GM_ID;


    private String AG_SD_ID_FullName;
    private String AG_SM_ID_FullName;
    private String AG_DM_ID_FullName;
    private String AG_APV_ID_FullName;
    private String AG_GM_ID_FullName;


    private String AG_Location_House_No;
    private String AG_Location_Village;
    private String AG_Location_Village_No;
    private String AG_Location_Alley;
    private String AG_Location_Road;
    private String AG_Location_Sub_district;
    private String AG_Location_District;
    private String AG_Location_Province;
    private String AG_Location_Postal_ID;
    private String AG_Invoice_House_No;
    private String AG_Invoice_Village;
    private String AG_Invoice_Village_No;
    private String AG_Invoice_Alley;
    private String AG_Invoice_Road;
    private String AG_Invoice_Sub_district;
    private String AG_Invoice_District;
    private String AG_Invoice_Province;
    private String AG_Invoice_Postal_ID;
    private DateTime AG_Start_Effective_Date;
    private DateTime AG_First_Order_Date;
    private Boolean AG_Status;
    private DateTime AG_Go_out_of_business_Date;
    private String AG_Applied_Document;
    private String AG_Other_Document;
    private Int16 AG_Small_Case;
    private Int16 AG_Large_Case;
    private Decimal AG_Pledge_Amount;
    private Int16 AG_Room_Size;
    private Decimal AG_Cash_Deposit;
    private Decimal AG_Bank_Guarantee;
    private String AG_Bank_ID;
    private String AG_Term_of_payment;
    private String AG_Remarks;
    private String AG_Product_Group_ID;
    private String AG_Price_Group_ID;
    private String AG_Grade;
    private DateTime AG_Grade_Effective_Date;
    private String AG_ItemValue;

    private String AG_Location_Region;
    private String AG_Invoice_Region;

    private String AG_AgentName;




    public String SD_ID_FullName_AG
    {
        get
        {
            AG_SD_ID_FullName = dbo_UserDataClass.Select_Record(SD_ID_AG).FullName;
            return AG_SD_ID_FullName;
        }
        set
        {
            AG_SD_ID_FullName = value;
        }
    }
    public String SAG_ID_FullName_AG
    {
        get
        {
            AG_SM_ID_FullName = dbo_UserDataClass.Select_Record(SM_ID_AG).FullName;
            return AG_SM_ID_FullName;
        }
        set
        {
            AG_SM_ID_FullName = value;
        }
    }
    public String DM_ID_FullName_AG
    {
        get
        {
            AG_DM_ID_FullName = dbo_UserDataClass.Select_Record(DM_ID_AG).FullName;
            return AG_DM_ID_FullName;
        }
        set
        {
            AG_DM_ID_FullName = value;
        }
    }
    public String APV_ID_FullName_AG
    {
        get
        {
            AG_APV_ID_FullName = dbo_UserDataClass.Select_Record(APV_ID_AG).FullName;
            return AG_APV_ID_FullName;
        }
        set
        {
            AG_APV_ID_FullName = value;
        }
    }
    public String GM_ID_FullName_AG
    {
        get
        {
            AG_GM_ID_FullName = dbo_UserDataClass.Select_Record(GM_ID_AG).FullName;
            return AG_GM_ID_FullName;
        }
        set
        {
            AG_GM_ID_FullName = value;
        }
    }



    public String AgentName_AG
    {
        get
        {
            //if (string.IsNullOrEmpty(AG_AgentName))
            //{
            //    AG_AgentName = AG_Prefix_ID + " " + AG_First_Name + " " + AG_Last_Name;
            //}



            AG_AgentName = dbo_AgentDataClass.Select_Record(this.AG_CV_Code).AgentName;
            return AG_AgentName;
        }
        set
        {
            AG_AgentName = value;
        }
    }


    public String Location_Region_AG
    {
        get
        {
            return AG_Location_Region;
        }
        set
        {
            AG_Location_Region = value;
        }
    }

    public String Invoice_Region_AG
    {
        get
        {
            return AG_Invoice_Region;
        }
        set
        {
            AG_Invoice_Region = value;
        }
    }


    public String CV_Code_AG
    {
        get
        {
            return AG_CV_Code == null ? string.Empty : AG_CV_Code;
            //return AG_CV_Code;
        }
        set
        {
            AG_CV_Code = value;
            m_Agent = dbo_AgentDataClass.Select_Record(value);
        }
    }

    public String Prefix_ID_AG
    {
        get
        {
            return AG_Prefix_ID;
        }
        set
        {
            AG_Prefix_ID = value;
        }
    }

    public String First_Name_AG
    {
        get
        {
            return AG_First_Name;
        }
        set
        {
            AG_First_Name = value;
        }
    }

    public String Last_Name_AG
    {
        get
        {
            return AG_Last_Name;
        }
        set
        {
            AG_Last_Name = value;
        }
    }

    public String Agent_Type_ID_AG
    {
        get
        {
            return AG_Agent_Type_ID;
        }
        set
        {
            AG_Agent_Type_ID = value;
        }
    }

    public String Home_Phone_No_AG
    {
        get
        {
            return AG_Home_Phone_No;
        }
        set
        {
            AG_Home_Phone_No = value;
        }
    }

    public String Mobile_AG
    {
        get
        {
            AG_Mobile = (string.IsNullOrEmpty(m_Agent.Mobile) ? string.Empty : m_Agent.Mobile);
            return AG_Mobile;
        }
        set
        {
            AG_Mobile = value;
        }
    }

    public String Tax_ID_AG
    {
        get
        {
            return AG_Tax_ID;
        }
        set
        {
            AG_Tax_ID = value;
        }
    }

    public String Email_AG
    {
        get
        {
            return AG_Email;
        }
        set
        {
            AG_Email = value;
        }
    }

    public String Fax_AG
    {
        get
        {
            AG_Fax = (string.IsNullOrEmpty(m_Agent.Fax) ? string.Empty : m_Agent.Fax);
            return AG_Fax;
        }
        set
        {
            AG_Fax = value;
        }
    }

    public String Concession_Area_AG
    {
        get
        {
            return AG_Concession_Area;
        }
        set
        {
            AG_Concession_Area = value;
        }
    }

    public String Owner_First_Name_AG
    {
        get
        {
            return AG_Owner_First_Name;
        }
        set
        {
            AG_Owner_First_Name = value;
        }
    }

    public String Owner_Last_Name_AG
    {
        get
        {
            return AG_Owner_Last_Name;
        }
        set
        {
            AG_Owner_Last_Name = value;
        }
    }

    public String Owner_Phone_No1_AG
    {
        get
        {
            return AG_Owner_Phone_No1;
        }
        set
        {
            AG_Owner_Phone_No1 = value;
        }
    }

    public String Owner_Phone_No2_AG
    {
        get
        {
            return AG_Owner_Phone_No2;
        }
        set
        {
            AG_Owner_Phone_No2 = value;
        }
    }

    public String Contact_First_Name_AG
    {
        get
        {
            return AG_Contact_First_Name;
        }
        set
        {
            AG_Contact_First_Name = value;
        }
    }

    public String Contact_Last_Name_AG
    {
        get
        {
            return AG_Contact_Last_Name;
        }
        set
        {
            AG_Contact_Last_Name = value;
        }
    }

    public String Contact_Phone_No1_AG
    {
        get
        {
            return AG_Contact_Phone_No1;
        }
        set
        {
            AG_Contact_Phone_No1 = value;
        }
    }

    public String Contact_Phone_No2_AG
    {
        get
        {
            return AG_Contact_Phone_No2;
        }
        set
        {
            AG_Contact_Phone_No2 = value;
        }
    }

    public String SD_ID_AG
    {
        get
        {
            return AG_SD_ID;
        }
        set
        {
            AG_SD_ID = value;
        }
    }

    public String SM_ID_AG
    {
        get
        {
            return AG_SM_ID;
        }
        set
        {
            AG_SM_ID = value;
        }
    }

    public String DM_ID_AG
    {
        get
        {
            return AG_DM_ID;
        }
        set
        {
            AG_DM_ID = value;
        }
    }

    public String GM_ID_AG
    {
        get
        {
            return AG_GM_ID;
        }
        set
        {
            AG_GM_ID = value;
        }
    }

    public String APV_ID_AG
    {
        get
        {
            return AG_APV_ID;
        }
        set
        {
            AG_APV_ID = value;
        }
    }

    public String Location_House_No_AG
    {
        get
        {
            return AG_Location_House_No;
        }
        set
        {
            AG_Location_House_No = value;
        }
    }

    public String Location_Village_AG
    {
        get
        {
            return AG_Location_Village;
        }
        set
        {
            AG_Location_Village = value;
        }
    }

    public String Location_Village_No_AG
    {
        get
        {
            return AG_Location_Village_No;
        }
        set
        {
            AG_Location_Village_No = value;
        }
    }

    public String Location_Alley_AG
    {
        get
        {
            return AG_Location_Alley;
        }
        set
        {
            AG_Location_Alley = value;
        }
    }

    public String Location_Road_AG
    {
        get
        {
            return AG_Location_Road;
        }
        set
        {
            AG_Location_Road = value;
        }
    }

    public String Location_Sub_district_AG
    {
        get
        {
            return AG_Location_Sub_district;
        }
        set
        {
            AG_Location_Sub_district = value;
        }
    }

    public String Location_District_AG
    {
        get
        {
            return AG_Location_District;
        }
        set
        {
            AG_Location_District = value;
        }
    }

    public String Location_Province_AG
    {
        get
        {
            return AG_Location_Province;
        }
        set
        {
            AG_Location_Province = value;
        }
    }

    public String Location_Postal_ID_AG
    {
        get
        {
            return AG_Location_Postal_ID;
        }
        set
        {
            AG_Location_Postal_ID = value;
        }
    }

    public String Invoice_House_No_AG
    {
        get
        {
            AG_Invoice_House_No = (string.IsNullOrEmpty(m_Agent.Invoice_House_No) ? string.Empty : m_Agent.Invoice_House_No);
            return AG_Invoice_House_No;
        }
        set
        {
            AG_Invoice_House_No = value;
        }
    }

    public String Invoice_Village_AG
    {
        get
        {
            AG_Invoice_Village = (string.IsNullOrEmpty(m_Agent.Invoice_Village) ? string.Empty : m_Agent.Invoice_Village);
            return AG_Invoice_Village;
        }
        set
        {
            AG_Invoice_Village = value;
        }
    }

    public String Invoice_Village_No_AG
    {
        get
        {
            AG_Invoice_Village_No = (string.IsNullOrEmpty(m_Agent.Invoice_Village_No) ? string.Empty : m_Agent.Invoice_Village_No);
            return AG_Invoice_Village_No;
        }
        set
        {
            AG_Invoice_Village_No = value;
        }
    }

    public String Invoice_Alley_AG
    {
        get
        {
            AG_Invoice_Alley = (string.IsNullOrEmpty(m_Agent.Invoice_Alley) ? string.Empty : m_Agent.Invoice_Alley);
            return AG_Invoice_Alley;
        }
        set
        {
            AG_Invoice_Alley = value;
        }
    }

    public String Invoice_Road_AG
    {
        get
        {
            AG_Invoice_Road = (string.IsNullOrEmpty(m_Agent.Invoice_Road) ? string.Empty : m_Agent.Invoice_Road);
            return AG_Invoice_Road;
        }
        set
        {
            AG_Invoice_Road = value;
        }
    }

    public String Invoice_Sub_district_AG
    {
        get
        {
            AG_Invoice_Sub_district = (string.IsNullOrEmpty(m_Agent.Invoice_Sub_district)?string.Empty:m_Agent.Invoice_Sub_district);
            return AG_Invoice_Sub_district;
        }
        set
        {
            AG_Invoice_Sub_district = value;
        }
    }

    public String Invoice_District_AG
    {
        get
        {
            AG_Invoice_District = (string.IsNullOrEmpty(m_Agent.Invoice_District) ? string.Empty : m_Agent.Invoice_District);
            return AG_Invoice_District;
        }
        set
        {
            AG_Invoice_District = value;
        }
    }

    public String Invoice_Province_AG
    {
        get
        {
            AG_Invoice_Province = (string.IsNullOrEmpty(m_Agent.Invoice_Province) ? string.Empty : m_Agent.Invoice_Province);
            return AG_Invoice_Province;
        }
        set
        {
            AG_Invoice_Province = value;
        }
    }

    public String Invoice_Postal_ID_AG
    {
        get
        {
            AG_Invoice_Postal_ID = (string.IsNullOrEmpty(m_Agent.Invoice_Postal_ID) ? string.Empty : m_Agent.Invoice_Postal_ID);
            return AG_Invoice_Postal_ID;
        }
        set
        {
            AG_Invoice_Postal_ID = value;
        }
    }

    public DateTime Start_Effective_Date_AG
    {
        get
        {
            return AG_Start_Effective_Date;
        }
        set
        {
            AG_Start_Effective_Date = value;
        }
    }

    public DateTime First_Order_Date_AG
    {
        get
        {
            return AG_First_Order_Date;
        }
        set
        {
            AG_First_Order_Date = value;
        }
    }

    public Boolean Status_AG
    {
        get
        {
            return AG_Status;
        }
        set
        {
            AG_Status = value;
        }
    }

    public DateTime Go_out_of_business_Date_AG
    {
        get
        {
            return AG_Go_out_of_business_Date;
        }
        set
        {
            AG_Go_out_of_business_Date = value;
        }
    }

    public String Applied_Document_AG
    {
        get
        {
            return AG_Applied_Document;
        }
        set
        {
            AG_Applied_Document = value;
        }
    }

    public String Other_Document_AG
    {
        get
        {
            return AG_Other_Document;
        }
        set
        {
            AG_Other_Document = value;
        }
    }

    public Int16 Small_Case_AG
    {
        get
        {
            return AG_Small_Case;
        }
        set
        {
            AG_Small_Case = value;
        }
    }

    public Int16 Large_Case_AG
    {
        get
        {
            return AG_Large_Case;
        }
        set
        {
            AG_Large_Case = value;
        }
    }

    public Decimal Pledge_Amount_AG
    {
        get
        {
            return AG_Pledge_Amount;
        }
        set
        {
            AG_Pledge_Amount = value;
        }
    }

    public Int16 Room_Size_AG
    {
        get
        {
            return AG_Room_Size;
        }
        set
        {
            AG_Room_Size = value;
        }
    }

    public Decimal Cash_Deposit_AG
    {
        get
        {
            return AG_Cash_Deposit;
        }
        set
        {
            AG_Cash_Deposit = value;
        }
    }

    public Decimal Bank_Guarantee_AG
    {
        get
        {
            return AG_Bank_Guarantee;
        }
        set
        {
            AG_Bank_Guarantee = value;
        }
    }

    public String Bank_ID_AG
    {
        get
        {
            return AG_Bank_ID;
        }
        set
        {
            AG_Bank_ID = value;
        }
    }

    public String Term_of_payment_AG
    {
        get
        {
            return AG_Term_of_payment;
        }
        set
        {
            AG_Term_of_payment = value;
        }
    }

    public String Remarks_AG
    {
        get
        {
            return AG_Remarks;
        }
        set
        {
            AG_Remarks = value;
        }
    }

    public String Product_Group_ID_AG
    {
        get
        {
            return AG_Product_Group_ID;
        }
        set
        {
            AG_Product_Group_ID = value;
        }
    }

    public String Price_Group_ID_AG
    {
        get
        {
            return AG_Price_Group_ID;
        }
        set
        {
            AG_Price_Group_ID = value;
        }
    }

    public String Grade_AG
    {
        get
        {
            return AG_Grade;
        }
        set
        {
            AG_Grade = value;
        }
    }

    public DateTime Grade_Effective_Date_AG
    {
        get
        {
            return AG_Grade_Effective_Date;
        }
        set
        {
            AG_Grade_Effective_Date = value;
        }
    }

    public String ItemValue_AG
    {
        get
        {
            return AG_ItemValue;
        }
        set
        {
            AG_ItemValue = value;
        }
    }
    #endregion

    #region Ordering
    private String OR_PO_Number;
    private String OR_CV_Code_froOR_SAP;
    private Decimal OR_Total_Amount_before_vat_included;
    private Decimal OR_Vat_amount;
    private Decimal OR_Total_amount_after_vat_included;
    private DateTime OR_Date_of_create_order_or_PO_Date;
    private DateTime OR_Date_of_CP_receive_transaction;
    private DateTime OR_Date_of_delivery_goods;
    private String OR_Order_Status;
    private String OR_Created_By;
    private String OR_OrderBy;

    private String OR_User_ID;

    public String User_ID
    {
        get
        {
            return OR_User_ID;
        }
        set
        {
            OR_User_ID = value;
        }
    }


    public String OrderBy_OR
    {
        get
        {
            OR_OrderBy = dbo_UserDataClass.Select_Record(Created_By_OR).FullName;
            return OR_OrderBy;
        }
        set
        {
            OR_OrderBy = value;
        }
    }

    public String Created_By_OR
    {
        get
        {
            return OR_Created_By;
        }
        set
        {
            OR_Created_By = value;
        }
    }

    public String PO_Number_OR
    {
        get
        {
            return OR_PO_Number;
        }
        set
        {

            //dbo_AgentDataClass.Select_Record(value);
            OR_PO_Number = value;
            m_Ordering = dbo_OrderingDataClass.Select_Record(value);

           // m_OrderingDetail = dbo_OrderingDetailDataClass.Select_Record(value);
        }
    }

    public String CV_Code_froOR_SAP_OR
    {
        get
        {
            return OR_CV_Code_froOR_SAP;
        }
        set
        {
            OR_CV_Code_froOR_SAP = value;
        }
    }

    public Decimal Total_Amount_before_vat_included_OR
    {
        get
        {
            return OR_Total_Amount_before_vat_included;
        }
        set
        {
            OR_Total_Amount_before_vat_included = value;
        }
    }

    public Decimal Vat_amount_OR
    {
        get
        {
            return OR_Vat_amount;
        }
        set
        {
            OR_Vat_amount = value;
        }
    }

    public Decimal Total_amount_after_vat_included_OR
    {
        get
        {
            return OR_Total_amount_after_vat_included;
        }
        set
        {
            OR_Total_amount_after_vat_included = value;
        }
    }

    public DateTime Date_of_create_order_or_PO_Date_OR
    {
        get
        {
            OR_Date_of_create_order_or_PO_Date = m_Ordering.Date_of_create_order_or_PO_Date.HasValue ? m_Ordering.Date_of_create_order_or_PO_Date.Value : DateTime.MinValue;
            return OR_Date_of_create_order_or_PO_Date;
        }
        set
        {
            OR_Date_of_create_order_or_PO_Date = value;
        }
    }

    public DateTime Date_of_CP_receive_transaction_OR
    {
        get
        {
            OR_Date_of_CP_receive_transaction = m_Ordering.Date_of_CP_receive_transaction.HasValue ? m_Ordering.Date_of_CP_receive_transaction.Value : DateTime.MinValue;
            return OR_Date_of_CP_receive_transaction;
        }
        set
        {
            OR_Date_of_CP_receive_transaction = value;
        }
    }

    public DateTime Date_of_delivery_goods_OR
    {
        get
        {
            OR_Date_of_delivery_goods = m_Ordering.Date_of_delivery_goods.HasValue ? m_Ordering.Date_of_delivery_goods.Value : DateTime.MinValue;
            return OR_Date_of_delivery_goods;
        }
        set
        {
            OR_Date_of_delivery_goods = value;
        }
    }

    public String Order_Status_OR
    {
        get
        {
            return OR_Order_Status;
        }
        set
        {
            OR_Order_Status = value;
        }
    }
    #endregion

    #region Orderingdetail
    private String ORD_Ordering_Detail_ID;
    private String ORD_PO_Number;
    private String ORD_Product_ID;
    private Decimal ORD_Price;
    private Decimal ORD_Vat;
    private Int16 ORD_Stock_on_hand;
    private Int16 ORD_Suggest_Quantity;
    private Int16 ORD_Quantity;
    private Decimal ORD_Sub_Total;
    private Decimal ORD_Vat_Amount;
    private Decimal ORD_Total;
    private Byte ORD_Point;

    public String Ordering_Detail_ID_ORD
    {
        get
        {
            return ORD_Ordering_Detail_ID;
        }
        set
        {
            ORD_Ordering_Detail_ID = value;
        }
    }

    public String PO_Number_ORD
    {
        get
        {
            return ORD_PO_Number;
        }
        set
        {
            ORD_PO_Number = value;
            
        }
    }

    public String Product_ID_ORD
    {
        get
        {
            ORD_Product_ID = m_Product.Product_ID;
            return ORD_Product_ID;
        }
        set
        {
            ORD_Product_ID = value;
        }
    }

    public Decimal Price_ORD
    {
        get
        {
           // ORD_Price = m_OrderingDetail.Price.HasValue ? m_OrderingDetail.Price.Value : 0;
            return ORD_Price;
        }
        set
        {
            ORD_Price = value;
        }
    }

    public Decimal Vat_ORD
    {
        get
        {
            //ORD_Vat = m_OrderingDetail.Vat.HasValue ? m_OrderingDetail.Vat.Value : 0;
            return ORD_Vat;
        }
        set
        {
            ORD_Vat = value;
        }
    }

    public Int16 Stock_on_hand_ORD
    {
        get
        {
            return ORD_Stock_on_hand;
        }
        set
        {
            ORD_Stock_on_hand = value;
        }
    }

    public Int16 Suggest_Quantity_ORD
    {
        get
        {
            return ORD_Suggest_Quantity;
        }
        set
        {
            ORD_Suggest_Quantity = value;
        }
    }

    public Int16 Quantity_ORD
    {
        get
        {
            //ORD_Quantity = m_OrderingDetail.Quantity.HasValue ? m_OrderingDetail.Quantity.Value : Int16.Parse("0");
            return ORD_Quantity;
        }
        set
        {
            ORD_Quantity = value;
        }
    }

    public Decimal Sub_Total_ORD
    {
        get
        {
            return ORD_Sub_Total;
        }
        set
        {
            ORD_Sub_Total = value;
        }
    }

    public Decimal Vat_Amount_ORD
    {
        get
        {
            //ORD_Vat_Amount = m_OrderingDetail.Vat_Amount.HasValue ? m_OrderingDetail.Vat_Amount.Value : 0;
            return ORD_Vat_Amount;
        }
        set
        {
            ORD_Vat_Amount = value;
        }
    }

    public Decimal Total_ORD
    {
        get
        {
            //ORD_Total = m_OrderingDetail.Total.HasValue ? m_OrderingDetail.Total.Value : 0;
            return ORD_Total;
        }
        set
        {
            ORD_Total = value;
        }
    }

    public Byte Point_ORD
    {
        get
        {
            return ORD_Point;
        }
        set
        {
            ORD_Point = value;
        }
    }

    #endregion

    #region Product
    private String PD_EAN;
    private String PD_Product_ID;
    private String PD_Product_Name;
    private Byte PD_Point;
    private Int16 PD_Size;
    private String PD_Unit_of_item_ID;
    private String PD_Product_group_ID;


   
    // private Nullable<Boolean> PD_Exclude_Vat;



    private Decimal PD_CP_Meiji_Price;
    private Int16 PD_Order_No;
    private Byte PD_Quantity_in__carte;
    private Int16 PD_Packing_Size;
    private String PD_Status;
    private Int32 PD_Quantity;
    private String PD_SAP_Product_Code;
    private Decimal PD_SP_Price;
    private Decimal PD_Agent_Price;
    private Int16 PD_Qty;
    private String PD_Billing_Detail_ID;
    private String PD_Billing_ID;
    private Decimal PD_Net_Value;
    private Byte PD_Vat;
    private Decimal PD_Total;
    private Int16 PD_Stock;
    private Int16 PD_Total_Qty;

    private byte[] PD_Photo;


    public byte[] Photo_PD
    {
        get
        {
            return PD_Photo;
        }
        set
        {
            PD_Photo = value;
        }
    }


    private Int16 PD_Requisition_Qty;


    private int PD_index;





    public Int16 Stock_PD
    {
        get
        {
            return PD_Stock;
        }
        set
        {
            PD_Stock = value;
        }
    }


    public Decimal SP_Price_PD
    {
        get
        {
            return PD_SP_Price;
        }
        set
        {
            PD_SP_Price = value;
        }
    }

    public Int16 Qty_PD
    {
        get
        {
            
            return PD_Qty;
        }
        set
        {
            PD_Qty = value;
        }
    }

    public String Billing_Detail_ID_PD
    {
        get
        {
            return PD_Billing_Detail_ID;
        }
        set
        {
            PD_Billing_Detail_ID = value;
        }
    }

    public String Billing_ID_PD
    {
        get
        {
            return PD_Billing_ID;
        }
        set
        {
            PD_Billing_ID = value;
        }
    }

    public Decimal Agent_Price_PD
    {
        get
        {
            return PD_Agent_Price;
        }
        set
        {
            PD_Agent_Price = value;
        }
    }

    public int index_PD
    {
        get
        {
            return PD_index;
        }
        set
        {
            PD_index = value;
        }
    }

    public Int32 Quantity_PD
    {
        get
        {
            // PD_Unit_of_item_ID = (string.IsNullOrEmpty(m_Product.Unit_of_item_ID) ? string.Empty : m_Product.Unit_of_item_ID); 
            
            return PD_Quantity;
        }
        set
        {
            PD_Quantity = value;
        }
    }


    public String Product_ID_PD
    {
        get
        {
            return PD_Product_ID;
        }
        set
        {
            PD_Product_ID = value;


            m_Product = dbo_ProductDataClass.Select_Record(value);
        }
    }


    public String SAP_Product_Code_PD
    {
        get
        {
            return PD_SAP_Product_Code;
        }
        set
        {
            PD_SAP_Product_Code = value;
        }
    }

    public String Product_Name_PD
    {
        get
        {
            //return PD_Product_Name;
            PD_Product_Name = dbo_ProductDataClass.Select_Record(ORD_Product_ID).Product_Name; //dbo_AgentDataClass.Select_Record(this.AG_CV_Code).AgentName;
            return PD_Product_Name;
        }
        set
        {
            PD_Product_Name = value;
        }
    }


    public Int16 Size_PD
    {
        get
        {

            PD_Size = (m_Product.Size.HasValue ? m_Product.Size.Value : Int16.Parse("0")); 

            return PD_Size;
        }
        set
        {
            PD_Size = value;
        }
    }

    public String Unit_of_item_ID_PD
    {
        get
        {
            //PD_Size = (m_Product.Size.HasValue? m_Product.Size.Value: Int16.Parse("0")); ;
            PD_Unit_of_item_ID = (string.IsNullOrEmpty(m_Product.Unit_of_item_ID) ? string.Empty : m_Product.Unit_of_item_ID);          
            return PD_Unit_of_item_ID;
        }
        set
        {
            PD_Unit_of_item_ID = value;
        }
    }

    public String Product_group_ID_PD
    {
        get
        {
            PD_Product_group_ID = (string.IsNullOrEmpty(m_Product.Product_group_ID) ? string.Empty : m_Product.Product_group_ID);
            return PD_Product_group_ID;
        }
        set
        {
            PD_Product_group_ID = value;
        }
    }

    public String EAN_PD
    {
        get
        {
            return PD_EAN;
        }
        set
        {
            PD_EAN = value;
        }
    }

    public Decimal CP_Meiji_Price_PD
    {
        get
        {
            return PD_CP_Meiji_Price;
        }
        set
        {
            PD_CP_Meiji_Price = value;
        }
    }

    public Byte Point_PD
    {
        get
        {
            return PD_Point;
        }
        set
        {
            PD_Point = value;
        }
    }

    //public Nullable<Boolean> Exclude_Vat
    //{
    //    get
    //    {
    //        return PD_Exclude_Vat;
    //    }
    //    set
    //    {
    //        PD_Exclude_Vat = value;
    //    }
    //}

    public Byte Vat_PD
    {
        get
        {
            return PD_Vat;
        }
        set
        {
            PD_Vat = value;
        }
    }

    public Int16 Order_No_PD
    {
        get
        {
            return PD_Order_No;
        }
        set
        {
            PD_Order_No = value;
        }
    }

    public Byte Quantity_in__carte_PD
    {
        get
        {
            return PD_Quantity_in__carte;
        }
        set
        {
            PD_Quantity_in__carte = value;
        }
    }

    public Int16 Packing_Size_PD
    {
        get
        {
            return PD_Packing_Size;
        }
        set
        {
            PD_Packing_Size = value;
        }
    }

    public String Status_PD
    {
        get
        {
            return PD_Status;
        }
        set
        {
            PD_Status = value;
        }
    }


    public Decimal Net_Value_PD
    {
        get
        {
            return PD_Net_Value;
        }
        set
        {
            PD_Net_Value = value;
        }
    }

    //public Decimal Vat
    //{
    //    get
    //    {
    //        return PD_Vat;
    //    }
    //    set
    //    {
    //        PD_Vat = value;
    //    }
    //}

    public Decimal Total_PD
    {
        get
        {
            return PD_Total;
        }
        set
        {
            PD_Total = value;
        }
    }

    public Int16 Total_Qty_PD
    {
        get
        {
            return PD_Total_Qty;
        }
        set
        {
            PD_Total_Qty = value;
        }
    }

    public Int16 Requisition_Qty_PD
    {
        get
        {
            return PD_Requisition_Qty;
        }
        set
        {
            PD_Requisition_Qty = value;
        }
    }
    #endregion

    #region 
    private byte[] m_IMG_Logo;

    public byte[] IMG_Logo
    {
        get
        {   
            m_IMG_Logo = File.ReadAllBytes(@"D:/sam/ViewsMockup/Images/img140.png");

            return m_IMG_Logo;
        }
        set
        {
            m_IMG_Logo = value;
        }
    }

    #endregion




}