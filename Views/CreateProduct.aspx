<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateProduct.aspx.cs" Inherits="Views_CreateProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">Product</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>


            <div class="col-md-12 bar-content" style="height: 1150px; padding-top: 20px">
                <div class="form-horizontal">


                    <div class="form-group">
                        <label class="col-md-2 control-label">รหัสสินค้า:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtProduct_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ชื่อสินค้า:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtProduct_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ขนาด (CC):</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtSize" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">หน่วย:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtUnit_of_item_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">กลุ่มสินค้า:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtProduct_group_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">EAN:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtEAN" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ราคาตู้แช่:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtCP_Meiji_Price" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">แต้ม:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtPoint" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Exclude Vat:</label>
                        <div class="col-md-4">
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </div>
                        <label class="col-md-2 control-label">Vat:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtVat" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">รูปภาพ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox1" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ลำดับ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtOrder_No" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">จำนวนหน่วยต่อลัง:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtQuantity_in_carte" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">จำนวนขวดต่อแพค:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtPacking_Size" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">สถานะ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtStatus" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="ตกลง" />
                        <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" />
                    </div>
                </div>
            </div>

        </div>
    </div>


</asp:Content>

