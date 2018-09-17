<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="StockOnHandList.aspx.cs" Inherits="Views_StockOnHandList" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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


        function validateFloatKeyPress(el, evt) {
            //  alert(el.value);
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            if (charCode == 46 && el.value.indexOf(".") !== -1) {
                return false;
            }

            if (el.value.indexOf(".") !== -1) {
                var range = document.selection.createRange();

                if (range.text != "") {
                }
                else {
                    var number = el.value.split('.');
                    if (number.length == 2 && number[1].length > 1)
                        return false;
                }
            }

            return true;
        }

        function ClearValue(lbl_Quantity, txt, diff) {

        }



        function UpdateField(lbl_Quantity, txt, diff) {


            //diff.value = parseInt(lbl_Quantity.innerHTML == '' ? '0' : lbl_Quantity.innerHTML) -
            //    parseInt(txt.value == '' ? '0' : txt.value); 

            if (txt.value == '') {
                //alert("diff.value = 0");
                diff.value = 0;
                //style.color = black;
            }
            else {

                diff.value = parseInt(txt.value == '' ? '0' : txt.value) - parseInt(lbl_Quantity.innerHTML == '' ? '0' : lbl_Quantity.innerHTML);
                //alert(diff.value);

                if (diff.value < 0) {

                    //Change Color
                    diff.style.color = "Red";
                }
                else if (diff.value > 0) {
                   
                    //Change Color
                    diff.style.color = "Green";
                }
                else {

                    //Change Color
                    diff.style.color = "Black";
                }

            }
     
            return true;
        }

        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num + '.' + cents);
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>

            <asp:Panel ID="pnlGrid" Visible="True" runat="server">

                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">นับสต๊อก</span>&nbsp;&nbsp;
                             <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่นับสต๊อก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchCount_Date" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtSearchCount_Date" runat="server"
                                            TargetControlID="txtSearchCount_Date" PopupButtonID="cldtxtSearchCount_Date" />
                                    </div>
                                    <label class="col-md-2 control-label">เลขที่ใบนับสต๊อก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchCount_No" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สถานะ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="รอการคอนเฟิร์ม"></asp:ListItem>
                                            <asp:ListItem Text="คอนเฟิร์มแล้ว"></asp:ListItem>
                                            <asp:ListItem Text="ยกเลิกการนับ"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="พิมพ์ฟอร์มใบนับสต๊อก" OnClick="Button1_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnStartCountStock" class="btn btn-primary" runat="server" Text="เริ่มนับสต๊อก" OnClick="btnStartCountStock_Click" OnClientClick="return confirm('คุณต้องการ เริ่มนับสต๊อคหรือไม่?')?myApp.showPleaseWait(): false;" />
                                <asp:Button ID="btnCancelCountStock" class="btn btn-primary" runat="server" Text="ยกเลิกการนับสต๊อก" OnClick="btnCancelCountStock_Click" OnClientClick="return confirm('คุณต้องการ ยกเลิกนับสต๊อคหรือไม่?')?myApp.showPleaseWait(): false;" />
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
                                <asp:GridView ID="GridViewCountStock"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333" OnRowCommand="GridViewCountStock_RowCommand"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    AllowPaging="true" PageSize="10" OnDataBound="GridViewCountStock_DataBound" OnRowDataBound="GridViewCountStock_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="วันที่นับสต๊อก">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Date" runat="server" Text='<%# Eval("Count_Date","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบนับสต๊อก" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCount_No" runat="server" CssClass="btn btn-link" Text='<%# Eval("Count_No") %>'
                                                    CommandArgument='<%# Eval("Count_No") %>' CommandName="View" Style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะ">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Order_Status" runat="server" Text='<%# Eval("Status") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวน" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Stock_Begin" runat="server" Text='<%# Eval("Stock_on_Hand","{0:N0}") %>' Style="color: blue; text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนนับ" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Stock_In" runat="server" Text='<%# Eval("Count_Quantity","{0:N0}") %>' Style="color: blue; text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ผลต่าง" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Stock_Out" runat="server" Text='<%# Eval("Diff_Quantity","{0:N0}") %>' Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_CV_CODE" runat="server" CssClass="btn btn-mini btn-primary" Text="พิมพ์ใบนับสต๊อก"
                                                    CommandName="Print" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <PagerTemplate>
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
                            <span class="title" style="font-size: 18px">นับสต๊อก</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่นับสต๊อก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCount_Date" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">เลขที่ใบนับสต๊อก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCount_No" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สถานะ:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="form-group"></div>

                            </div>
                            <div class="tabbable-panel" style="margin-top: 20px">
                                <div class="tabbable-line" id="dvTab">
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
                                        <li id="li_05" style="auto">
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
                                                ForeColor="#333333" OnRowDataBound="GridViewStock_Tab1_RowDataBound"
                                                OnDataBound="GridViewStock_Tab1_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Quantity","{0:N0}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนนับ" ItemStyle-Width="84px" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCount_Quantity" runat="server" Style="text-align: right; color: blue"
                                                                Text='<%# Eval("Count_Quantity") %>' Width="84px"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ผลต่าง">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Diff_Quantity" Width="84px" runat="server" Style="text-align: right;" Enabled="false" Text='<%# Eval("Diff_Quantity","{0:N0}") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หมายเหตุ" ItemStyle-Width="184px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Eval("Remark") %>' Width="184px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock_on_Hand_ID" runat="server" Style="color: blue" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
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
                                                ForeColor="#333333" OnRowDataBound="GridViewStock_Tab2_RowDataBound"
                                                OnDataBound="GridViewStock_Tab2_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Quantity","{0:N0}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนนับ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCount_Quantity" runat="server" Style="text-align: right; color: blue"
                                                                Text='<%# Eval("Count_Quantity") %>' Width="84px"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ผลต่าง">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Diff_Quantity" Width="84px" runat="server" Style="text-align: right;" Enabled="false" Text='<%# Eval("Diff_Quantity","{0:N0}") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หมายเหตุ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Text='<%# Eval("Remark") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock_on_Hand_ID" runat="server" Style="color: blue" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
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
                                                ForeColor="#333333" OnRowDataBound="GridViewStock_Tab3_RowDataBound"
                                                OnDataBound="GridViewStock_Tab3_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Quantity","{0:N0}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนนับ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCount_Quantity" runat="server" Style="text-align: right; color: blue"
                                                                Text='<%# Eval("Count_Quantity") %>' Width="84px"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ผลต่าง">
                                                        <ItemTemplate>
                                                            <%-- <asp:TextBox ID="lbl_Diff_Quantity" Width="84px" runat="server" Style="text-align: right; color: blue" Enabled="false" Text='<%# Eval("Diff_Quantity") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="lbl_Diff_Quantity" Width="84px" runat="server" Style="text-align: right;" Enabled="false" Text='<%# Eval("Diff_Quantity","{0:N0}") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หมายเหตุ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Text='<%# Eval("Remark") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock_on_Hand_ID" runat="server" Style="color: blue" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
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
                                                ForeColor="#333333" OnRowDataBound="GridViewStock_Tab4_RowDataBound"
                                                OnDataBound="GridViewStock_Tab4_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Quantity","{0:N0}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนนับ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCount_Quantity" runat="server" Style="text-align: right; color: blue"
                                                                Text='<%# Eval("Count_Quantity") %>' Width="84px"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ผลต่าง">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Diff_Quantity" Width="84px" runat="server" Style="text-align: right;" Enabled="false" Text='<%# Eval("Diff_Quantity","{0:N0}") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หมายเหตุ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Text='<%# Eval("Remark") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock_on_Hand_ID" runat="server" Style="color: blue" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
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
                                                ForeColor="#333333" OnRowDataBound="GridViewStock_Tab5_RowDataBound"
                                                OnDataBound="GridViewStock_Tab5_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Quantity","{0:N0}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนนับ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCount_Quantity" runat="server" Style="text-align: right; color: blue"
                                                                Text='<%# Eval("Count_Quantity") %>' Width="84px"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ผลต่าง">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Diff_Quantity" Width="84px" runat="server" Style="text-align: right; color: blue" Enabled="false" Text='<%# Eval("Diff_Quantity","{0:N0}") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หมายเหตุ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Text='<%# Eval("Remark") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock_on_Hand_ID" runat="server" Style="color: blue" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
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
                                <asp:HiddenField ID="btnSaveMode" runat="server" />
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="butNo" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="butNo_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <%--<asp:Button ID="btnConfirm" class="btn btn-default" runat="server" Text="ยืนยันการนับสต๊อก" OnClick="btnConfirm_Click" OnClientClick="myApp.showPleaseWait(); return true;" BackColor="Red" BorderColor="#CCCCCC" />   OnClientClick="return confirm('ยืนยันการเคลียร์เงินใช่หรือไม่?')?myApp.showPleaseWait(): false;"--%>
                                <asp:Button ID="btnConfirm" class="btn btn-default" runat="server" Text="ยืนยันการนับสต๊อก" OnClick="btnConfirm_Click" OnClientClick="return confirm('ท่านต้องการยืนยันการนับสต๊อกใช่หรือไม่?')?myApp.showPleaseWait(): false;" BackColor="Red" BorderColor="#CCCCCC" />
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
