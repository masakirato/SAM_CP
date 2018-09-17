<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="StockOnHand.aspx.cs" Inherits="Views_StockOnHand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function tab1click() {
            $("#imgTab1").show();

            $("#imgTab2").hide();
            $("#imgTab3").hide();
            $("#imgTab4").hide();
            // $("#imgTab5").hide();
        }
        function tab2click() {
            $("#imgTab2").show();

            $("#imgTab1").hide();
            $("#imgTab3").hide();
            $("#imgTab4").hide();
            // $("#imgTab5").hide();
        }
        function tab3click() {
            $("#imgTab3").show();

            $("#imgTab1").hide();
            $("#imgTab2").hide();
            $("#imgTab4").hide();
            // $("#imgTab5").hide();
        }
        function tab4click() {
            $("#imgTab4").show();

            $("#imgTab1").hide();
            $("#imgTab2").hide();
            $("#imgTab3").hide();
            //$("#imgTab5").hide();
        }
        function tab5click() {
            $("#imgTab1").hide();
            $("#imgTab2").hide();
            $("#imgTab3").hide();
            $("#imgTab4").hide();
            // $("#imgTab5").hide();
        }



    </script>
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">สต๊อก</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>

            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">

                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                        <div class="col-md-4">
                            <asp:label id="label_agent_name" runat="server" CssClass="form-control"></asp:label>
                        </div>
                        <label class="col-md-2 control-label">มูลค่าของสต๊อก:</label>
                        <div class="col-md-4">
                            <asp:label id="lblValue" runat="server" CssClass="form-control"></asp:label>
                        </div>
                    </div>
                </div>


                <div class="tabbable-panel" style="margin-top: 20px">
                    <div class="tabbable-line">
                        <ul class="nav nav-tabs">
                            <li class="active" id="li_01" style="width: 180px">
                                <img src="../Images/tab1.png" id="imgTab1" style="position: absolute; top: -50px; z-index: 999" />
                                <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab1click()">นมพาสเจอร์ไรส์</a>

                            </li>
                            <li id="li_02" style="width: 130px">
                                <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab2click()">นมเปรี้ยว </a>

                            </li>
                            <li id="li_03" style="width: 170px">
                                <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab3click()">&nbsp;&nbsp;&nbsp;&nbsp;โยเกิร์ตเมจิ </a>

                            </li>
                            <li id="li_04" style="width: 170px">
                                <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                <a href="#tab_default_4" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab4click()">&nbsp;&nbsp;&nbsp;&nbsp;นมเปรี้ยวไพเก้น </a>

                            </li>
                            <li id="li_05" style="width: auto">
                                <a href="#tab_default_5" id="tab5" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab5click()">อื่นๆ </a>

                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_default_1">
                                <asp:GridView ID="GridViewStock_Tab1"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    ondatabound="GridViewStock_Tab1_OnDataBound"
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
                                        <asp:TemplateField HeaderText="จำนวนสต๊อก" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Stock_on_hand" runat="server" style="color: blue" Text='<%# Eval("Stock","{0:N0}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane" id="tab_default_2">
                                <asp:GridView ID="GridViewStock_Tab2"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    ondatabound="GridViewStock_Tab2_OnDataBound"
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
                                        <asp:TemplateField HeaderText="จำนวนสต๊อก" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Stock_on_hand" runat="server" style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane" id="tab_default_3">
                                <asp:GridView ID="GridViewStock_Tab3"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    ondatabound="GridViewStock_Tab3_OnDataBound"
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
                                        <asp:TemplateField HeaderText="จำนวนสต๊อก" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Stock_on_hand" runat="server" style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane" id="tab_default_4">
                                <asp:GridView ID="GridViewStock_Tab4"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    ondatabound="GridViewStock_Tab4_OnDataBound"
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
                                        <asp:TemplateField HeaderText="จำนวนสต๊อก" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Stock_on_hand" runat="server" style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane" id="tab_default_5">
                                <asp:GridView ID="GridViewStock_Tab5"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    ondatabound="GridViewStock_Tab5_OnDataBound"
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
                                        <asp:TemplateField HeaderText="จำนวนสต๊อก" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Stock_on_hand" runat="server" style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>





                    </div>
                </div>
                <div class="col-md-12 text-center">
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

