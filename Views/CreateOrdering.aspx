<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateOrdering.aspx.cs" Inherits="Views_CreateOrdering" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">สั่งซื้อสินค้า</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>

            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">CV CODE:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtCV_Code_from_SAP" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">เลขที่ใบสั่งซื้อ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtPO_Number" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox1" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ราคารวมสั่งซื้อทั้งหมด:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtTotal_Amount_before_vat_included" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">VAT:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtVat_amount" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดรวมทั้งสิ้น:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtTotal_amount_after_vat_included" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">วันที่ทำรายการ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtDate_of_create_order_or_PO_Date" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">วันที่ CP รับข้อมูล:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtDate_of_CP_receive_transaction" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">วันที่รับสินค้า:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtDate_of_delivery_goods" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">สถานะ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtOrder_Status" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">เป้ายอดขายรายเดือน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox11" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">เป้ายอดขายรายไตรมาส:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox12" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">เป้ายอดขายรายปี:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox2" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                </div>


                <div class="tabbable-panel" style="margin-top: 20px">
                    <div class="tabbable-line">
                        <ul class="nav nav-tabs">
                            <li class="active">
                                <img src="../Images/tab1.png" id="imgTab1" style="position: absolute; top: -50px; z-index: 999" />
                                <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center">นมพาสเจอร์ไรส์</a>
                                <script>
                                    $("#tab1").on('click', function (e) {
                                        $("#imgTab1").show();

                                        $("#imgTab2").hide();
                                        $("#imgTab3").hide();
                                        $("#imgTab4").hide();
                                        $("#imgTab5").hide();
                                        return true;
                                    });
                                </script>
                            </li>
                            <li>
                                <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center">นมเปรี้ยว </a>
                                <script>
                                    $("#tab2").on('click', function (e) {
                                        $("#imgTab2").show();

                                        $("#imgTab1").hide();
                                        $("#imgTab3").hide();
                                        $("#imgTab4").hide();
                                        $("#imgTab5").hide();
                                        return true;
                                    });
                                </script>
                            </li>
                            <li>
                                <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;โยเกิร์ตเมจิ </a>
                                <script>
                                    $("#tab3").on('click', function (e) {
                                        $("#imgTab3").show();

                                        $("#imgTab1").hide();
                                        $("#imgTab2").hide();
                                        $("#imgTab4").hide();
                                        $("#imgTab5").hide();
                                        return true;
                                    });
                                </script>
                            </li>
                            <li>
                                <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                <a href="#tab_default_3" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;นมเปรี้ยวไพเก้น </a>
                                <script>
                                    $("#tab4").on('click', function (e) {
                                        $("#imgTab4").show();

                                        $("#imgTab1").hide();
                                        $("#imgTab2").hide();
                                        $("#imgTab3").hide();
                                        $("#imgTab5").hide();
                                        return true;
                                    });
                                </script>
                            </li>
                            <li>
                                <a href="#tab_default_3" id="tab5" data-toggle="tab" style="padding-left: 30px" class="text-center">อื่นๆ </a>
                                <script>
                                    $("#tab5").on('click', function (e) {
                                        //$("#imgTab5").show();

                                        $("#imgTab1").hide();
                                        $("#imgTab2").hide();
                                        $("#imgTab3").hide();
                                        $("#imgTab4").hide();
                                        return true;
                                    });
                                </script>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_default_1">



                                <asp:GridView ID="GridViewOrdering_Tab1"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    ondatabound="GridViewOrdering_1_OnDataBound"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Item" runat="server" Text='<%# Eval("index") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รหัสสินค้า">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อสินค้า">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ราคาต่อหน่วย" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PricePerUnit" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนสต๊อก">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Stock_on_hand" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนแนะนำ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Suggest_Quantity" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนที่สั่ง" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrderingAmount" runat="server" Style="text-align: right" textmode="Number"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="มูลค่าสินค้า" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Price" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane" id="tab_default_2">
                                <asp:GridView ID="GridViewOrdering_Tab2"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    ondatabound="GridViewOrdering_2_OnDataBound"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Item" runat="server" Text='<%# Eval("index") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รหัสสินค้า">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อสินค้า">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ราคาต่อหน่วย" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PricePerUnit" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนสต๊อก">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Stock_on_hand" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนแนะนำ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Suggest_Quantity" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนที่สั่ง" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrderingAmount" runat="server" Style="text-align: right" textmode="Number"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="มูลค่าสินค้า" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Price" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane" id="tab_default_3">
                                tab 3
                            </div>
                            <div class="tab-pane" id="tab_default_4">
                                tab 4
                            </div>
                            <div class="tab-pane" id="tab_default_5">
                                tab 5
                            </div>
                        </div>
                    </div>
                </div>




                <div class="col-md-12 text-center">
                    <br />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="ButtonOK" class="btn btn-primary" runat="server" Text="ตกลง" OnClick="ButtonOK_Click" />
                    <asp:Button ID="Button2" class="btn btn-default" runat="server" Text="ยกเลิก" />
                </div>
                <div class="col-md-12 text-center">
                    <br />
                </div>

            </div>



        </div>

    </div>

</asp:Content>

