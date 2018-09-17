<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateOtherRequisition.aspx.cs" Inherits="Views_CreateOtherRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
        $(document).ready(function () {
            //Initialize tooltips
            $('.nav-tabs > li a[title]').tooltip();

            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {

                var $target = $(e.target);

                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            });

            $(".next-step").click(function (e) {

                var $active = $('.wizard .nav-tabs li.active');
                $active.next().removeClass('disabled');
                nextTab($active);

            });
            $(".prev-step").click(function (e) {

                var $active = $('.wizard .nav-tabs li.active');
                prevTab($active);

            });
        });

        function nextTab(elem) {
            $(elem).next().find('a[data-toggle="tab"]').click();
        }
        function prevTab(elem) {
            $(elem).prev().find('a[data-toggle="tab"]').click();
        }

        $("[data-toggle='myCollapse']").click(function (ev) {
            ev.preventDefault();
            var target;
            if (this.hasAttribute('data-target')) {
                target = $(this.getAttribute('data-target'));
            } else {
                target = $(this.getAttribute('href'));
            };
            target.toggleClass("in");
            console.log(target.hasClass('in'));
        });

        $("[data-toggle='group2']").click(function (ev) {
            ev.preventDefault();
            var target;
            if (this.hasAttribute('data-target')) {
                target = $(this.getAttribute('data-target'));
            } else {
                target = $(this.getAttribute('href'));
            };
            target.toggleClass("in");
            console.log(target.hasClass('in'));
        });
    </script>

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">เบิกสินค้าอื่นๆ</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>

            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">CV CODE:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtFirst_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">เลขที่ใบสั่งซื้อ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtLast_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox1" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">วันที่เบิก:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox2" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">เหตุผล:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox3" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">คนที่เบิก:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox5" runat="server" CssClass="form-control"></asp:textbox>
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
                                                <asp:Label id="Label_Item" runat="server" Text='<%# Eval("Item") %>' style="color: blue"></asp:Label>
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
                                                <asp:Label id="Label_PricePerUnit" runat="server" Text='<%# Eval("PricePerUnit") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สต๊อกที่มีอยู่ในระบบ">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Stock_on_hand" runat="server" Text='<%# Eval("Stock_on_hand") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนที่เบิก">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Quantity" runat="server" Text='<%# Eval("Quantity") %>' style="color: blue"></asp:Label>
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
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="ตกลง" />
                    <asp:Button ID="Button2" class="btn btn-default" runat="server" Text="ยกเลิก" />
                </div>
                <div class="col-md-12 text-center">
                    <br />
                </div>




            </div>
        </div>
    </div>
</asp:Content>

