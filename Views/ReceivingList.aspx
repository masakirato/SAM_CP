<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master"
    AutoEventWireup="true" uiculture="th" culture="th-TH"
    CodeFile="ReceivingList.aspx.cs"
    Inherits="Views_ReceivingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        var myApp;
        myApp = myApp || (function () {
            //var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
            var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content" style="width: 100%;"><div class="modal-header"><h5>กรุณารอสักครู่...</h5></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar progress-red" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
            return {
                showPleaseWait: function () {
                    pleaseWaitDiv.modal();
                },
                hidePleaseWait: function () {
                    pleaseWaitDiv.modal('hide');
                },

            };
        })();
    </script>

    <script type="text/javascript">
        function pageLoad() {

            maintainSelectedTab();
        }
        function maintainSelectedTab() {
            var selectedTab = $("#<%=hfTab.ClientID%>");
            var tabId = selectedTab.val() != "" ? selectedTab.val() : "tab_default_1";
            $('#dvTab a[href="#' + tabId + '"]').tab('show');
            $("#dvTab a").click(function () {
                selectedTab.val($(this).attr("href").substring(1));
            });
        }
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

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" EnableViewState="true">
        <ContentTemplate>
            <asp:HiddenField ID="hfTab" runat="server" />

            <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <%--<span class="title" style="font-size: 18px">Receiving List</span>&nbsp;&nbsp;--%>
                            <span class="title" style="font-size: 18px"> รับสินค้าตามรายการสั่งซื้อ</span>&nbsp;&nbsp;
                            <%--<span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>--%>
                            <span class="glyphicon glyphicon-log-in" style="font-size: 18px"></span>   
                            <asp:Label ID="LabelPageHeader" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่เอกสาร:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchInvoice_No" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">ประเภทเอกสาร:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchBillingType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="">==ระบุ==</asp:ListItem>
                                            <asp:ListItem Value="ZDOM">ใบแจ้งหนี้</asp:ListItem>
                                            <asp:ListItem Value="ZDCN">ใบลดหนี้</asp:ListItem>
                                            <asp:ListItem Value="ZDDN">ใบเพิ่มหนี้</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่สั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchPO_DateStart" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldSearchPO_DateStart" runat="server"
                                            TargetControlID="txtSearchPO_DateStart" PopupButtonID="cldSearchPO_DateStart" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchPO_DateEnd" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldSearchPO_DateEnd" runat="server"
                                            TargetControlID="txtSearchPO_DateEnd" PopupButtonID="cldSearchPO_DateEnd" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สถานะเอกสาร:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchInvoice_Status" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="ยังไม่ยืนยัน"></asp:ListItem>
                                            <asp:ListItem Text="ยกเลิก"></asp:ListItem>
                                            <asp:ListItem Text="ยืนยันแล้ว"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบสั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchPO_No" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" onclick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>

                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่พบข้อมูล</center>
                                    </div>
                                </asp:Panel>
                                <asp:GridView ID="GridViewReceiving"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed" onrowcommand="GridViewReceiving_RowCommand"
                                    AllowPaging="true" PageSize="10" OnDataBound="GridViewReceiving_DataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ประเภทเอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Billing_Type_Name" runat="server" style="color: blue; margin-left: 10px" Text='<%# Eval("Billing_Type_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่เอกสาร" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkB_ID" runat="server" CssClass="btn btn-link" CommandArgument='<%# Eval("Billing_ID") %>'
                                                    CommandName="View" style="font-weight: bold; text-decoration: underline" Text='<%# Eval("Invoice_No") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <itemstyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Billing_Date" runat="server" style="color: blue" Text='<%# Eval("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="LinkInvoice_No" runat="server" CssClass="btn btn-link" Text='<%# Eval("PO_No") %>' CommandArgument='<%# Eval("Billing_ID") %>' CommandName="View" style="font-weight: bold; text-decoration: underline"></asp:LinkButton>--%>
                                                <asp:Label id="LinkInvoice_No" runat="server" style="color: blue" Text='<%# Eval("PO_No") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สั่งสินค้า">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Invoice_Date" runat="server" style="color: blue" Text='<%# Eval("PO_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Invoice_Total" runat="server" style="color: blue" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ภาษีมูลค่าเพิ่ม" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Invoice_VAT" runat="server" style="color: blue" Text='<%# Eval("Vat","{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_POStatus" runat="server" style="color: blue" Text='<%# Eval("Order_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะเอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Invoice_Status" runat="server" style="color: blue" Text='<%# Eval("Invoice_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" visible="false">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PO_Number" runat="server" style="color: blue" Text='<%# Eval("PO_No") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <pagertemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 70%">
                                                    <asp:Label ID="MessageLabel"
                                                        ForeColor="Blue"
                                                        Text="เลือกหน้า :"
                                                        runat="server" />
                                                    <asp:DropDownList ID="PageDropDownList"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                                        onchange="myApp.showPleaseWait();"
                                                        runat="server" />
                                                </td>

                                                <td style="width: 70%; text-align: right">
                                                    <asp:Label ID="CurrentPageLabel"
                                                        ForeColor="Black"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </PagerTemplate>

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>

            <asp:Panel ID="pnlForm" Visible="False" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รับสินค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่เอกสาร:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_No" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                        <asp:textbox id="txtBilling_ID" runat="server" CssClass="form-control" Enabled="false" Visible="false"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่เอกสาร:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtReceiving_Date" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบสั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtPO_No" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่สั่งสินค้า:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_Date" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">จำนวนเงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_Net_Value" runat="server" CssClass="form-control" Enabled="false" style="text-align: right"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">ภาษีมูลค่าเพิ่ม:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_VAT" runat="server" CssClass="form-control" Enabled="false" style="text-align: right"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">จำนวนเงินทั้งหมด:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_Total" runat="server" CssClass="form-control" Enabled="false" style="text-align: right"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">สถานะใบสั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtOrder_Status" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สถานะเอกสาร:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtStatus" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                    </div>
                                </div>
                            </div>

                            <div class="tabbable-panel" style="margin-top: 20px">
                                <div class="tabbable-line" id="dvTab">
                                    <ul class="nav nav-tabs">
                                        <li style="width: 180px" id="li_01" class="active" runat="server">

                                            <img src="../Images/tab1.png" id="imgTab1" style="position: absolute; top: -50px; z-index: 999" />
                                            <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab1click()">นมพาสเจอร์ไรส์</a>
                                            <%--  <script>
                                                $("#tab1").on('click', function (e) {
                                                    $("#imgTab1").show();

                                                    $("#imgTab2").hide();
                                                    $("#imgTab3").hide();
                                                    $("#imgTab4").hide();
                                                    $("#imgTab5").hide();
                                                    return true;
                                                });
                                            </script>--%>
                                        </li>
                                        <li style="width: 160px" id="li_02" runat="server">
                                            <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab2click()">นมเปรี้ยว </a>
                                            <%--<script>
                                                $("#tab2").on('click', function (e) {

                                                    $("#imgTab2").show();

                                                    $("#imgTab1").hide();
                                                    $("#imgTab3").hide();
                                                    $("#imgTab4").hide();
                                                    $("#imgTab5").hide();
                                                    return true;
                                                });
                                            </script>--%>
                                        </li>
                                        <li style="width: 160px" id="li_03" runat="server">
                                            <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab3click()">&nbsp;&nbsp;&nbsp;&nbsp;โยเกิร์ตเมจิ </a>
                                            <%-- <script>
                                                $("#tab3").on('click', function (e) {
                                                    $("#imgTab3").show();

                                                    $("#imgTab1").hide();
                                                    $("#imgTab2").hide();
                                                    $("#imgTab4").hide();
                                                    $("#imgTab5").hide();
                                                    return true;
                                                });
                                            </script>--%>
                                        </li>
                                        <li style="width: 160px" id="li_04" runat="server">
                                            <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_4" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab4click()">&nbsp;&nbsp;&nbsp;&nbsp;นมเปรี้ยวไพเก้น </a>
                                            <%--<script>
                                                $("#tab4").on('click', function (e) {
                                                    $("#imgTab4").show();

                                                    $("#imgTab1").hide();
                                                    $("#imgTab2").hide();
                                                    $("#imgTab3").hide();
                                                    $("#imgTab5").hide();
                                                    return true;
                                                });
                                            </script>--%>
                                        </li>
                                        <li style="width: 150px" id="li_05" runat="server">
                                            <a href="#tab_default_5" id="tab5" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab5click()">อื่นๆ </a>
                                            <%--<script>
                                                $("#tab5").on('click', function (e) {
                                                    $("#imgTab1").hide();
                                                    $("#imgTab2").hide();
                                                    $("#imgTab3").hide();
                                                    $("#imgTab4").hide();
                                                    // $("#imgTab5").hide();
                                                    return true;
                                                });
                                            </script>--%>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_default_1">
                                            <asp:GridView ID="GridViewReceiving_1"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333"
                                                ondatabound="GridViewReceiving_1_OnDataBound"
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
                                                            <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Price" runat="server" Text='<%# Eval("CP_Meiji_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" OnTextChanged="TxtId_TextChanged" Width="84px"
                                                                AutoPostBack="true" Text='<%# Eval("Qty") %>' style="text-align: right" Enabled="false">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงิน" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Amount" runat="server" style="color: blue" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lblBilling_Detail_ID" runat="server" style="color: blue" Text='<%# Eval("Billing_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_2">

                                            <asp:GridView ID="GridViewReceiving_2"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333"
                                                ondatabound="GridViewReceiving_2_OnDataBound"
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
                                                            <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Price" runat="server" Text='<%# Eval("CP_Meiji_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" OnTextChanged="TxtId_TextChanged" Width="84px"
                                                                AutoPostBack="true" Text='<%# Eval("Qty") %>' style="text-align: right" Enabled="false">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงิน" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Amount" runat="server" style="color: blue" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lblBilling_Detail_ID" runat="server" style="color: blue" Text='<%# Eval("Billing_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_3">

                                            <asp:GridView ID="GridViewReceiving_3"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333"
                                                ondatabound="GridViewReceiving_3_OnDataBound"
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
                                                            <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Price" runat="server" Text='<%# Eval("CP_Meiji_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" OnTextChanged="TxtId_TextChanged" Width="84px"
                                                                AutoPostBack="true" Text='<%# Eval("Qty") %>' style="text-align: right" Enabled="false">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงิน" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Amount" runat="server" style="color: blue" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lblBilling_Detail_ID" runat="server" style="color: blue" Text='<%# Eval("Billing_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_4">

                                            <asp:GridView ID="GridViewReceiving_4"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333"
                                                ondatabound="GridViewReceiving_4_OnDataBound"
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
                                                            <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Price" runat="server" Text='<%# Eval("CP_Meiji_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" OnTextChanged="TxtId_TextChanged" Width="84px"
                                                                AutoPostBack="true" Text='<%# Eval("Qty") %>' style="text-align: right" Enabled="false">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงิน" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Amount" runat="server" style="color: blue" Text='<%# Eval("Total","{0:N2}") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lblBilling_Detail_ID" runat="server" style="color: blue" Text='<%# Eval("Billing_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_5">

                                            <asp:GridView ID="GridViewReceiving_5"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333"
                                                ondatabound="GridViewReceiving_5_OnDataBound"
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
                                                            <asp:Label id="lbl_Unit_of_item" runat="server" style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Price" runat="server" Text='<%# Eval("CP_Meiji_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" OnTextChanged="TxtId_TextChanged" Width="84px"
                                                                AutoPostBack="true" Text='<%# Eval("Qty") %>' style="text-align: right" Enabled="false">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงิน" itemstyle-horizontalalign="right">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Amount" runat="server" style="color: blue" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lblBilling_Detail_ID" runat="server" style="color: blue" Text='<%# Eval("Billing_Detail_ID") %>'></asp:Label>
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
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="ยืนยัน" onclick="btnSave_Click" onclientclick="return confirm('ยืนยันการรับสินค้าใช่หรือไม่?')?myApp.showPleaseWait(): false;" />
                                <asp:Button ID="btnBacktogrid" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" onclick="btnBacktogrid_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>

                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

