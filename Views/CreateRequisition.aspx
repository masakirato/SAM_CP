<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateRequisition.aspx.cs" Inherits="Views_CreateRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">เบิกสินค้า</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtFirst_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">เลขที่ใบเบิก:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtLast_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">วันที่เบิก:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox3" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">วันที่ทำรายการ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox4" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">จำนวนรวม:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox7" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ราคารวม:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox8" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                </div>



                <div class="tabbable-panel" style="margin-top: 20px">
                    <div class="tabbable-line">
                        <ul class="nav nav-tabs ">
                            <li class="active " style="width: 180px">
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
                            <li style="width: 160px">
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
                            <li style="width: 160px">
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
                            <li style="width: 160px">
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
                            <li style="width: 150px">
                                <a href="#tab_default_3" id="tab6" data-toggle="tab" style="padding-left: 30px" class="text-center">อื่นๆ </a>
                                <script>
                                    $("#tab6").on('click', function (e) {
                                        $("#imgTab1").hide();
                                        $("#imgTab2").hide();
                                        $("#imgTab3").hide();
                                        $("#imgTab4").hide();
                                        $("#imgTab5").hide();
                                        return true;
                                    });
                                </script>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_default_1">
                                <asp:GridView ID="GridViewOrdering_1"
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
                                                <asp:Label id="Label_Item" runat="server" Text='<%# Eval("Requisition_Detail_ID") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รหัสสินค้า">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อสินค้า">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วย">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Unit_of_item" runat="server" Text='<%# Eval("Unit_of_item") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ราคาต่อหน่วย" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="Label_PricePerUnit" runat="server" Text='<%# Eval("Price") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="คงเหลือ" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="Label_PricePerUnit" runat="server" Text='<%# Eval("Previous_Balance_Qty") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ยอดแนะนำ" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="Label_PricePerUnit" runat="server" Text='<%# Eval("Suggestion_Qty") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนที่เบิกแล้ว" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="Label_PricePerUnit" runat="server" Text='<%# Eval("Sub_Total_Qty") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนที่เบิก" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrderingAmount" runat="server" Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเบิกรวม" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Price" runat="server" Text='<%# Eval("Total_Qty") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="tab-pane" id="tab_default_2">
                            </div>
                            <div class="tab-pane" id="tab_default_3">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 text-center">
                    <br />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="แก้ไข" />
                    <asp:Button ID="Button2" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" />
                </div>
                <div class="col-md-12 text-center">
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

