<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="OtherRequisitionList.aspx.cs" Inherits="Views_OtherRequisitionList" uiculture="th" culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <style>
         #link_pop {
             z-index: 1;
             position: fixed;
             top: 47%;
             left: 0;
         }

         .popover {
             position: fixed !important; /* I use !important because in this snippet bootstrap's styles overwrite mines but in general it is not nedded*/
         }
     </style>

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

        $(document).ready(function () {

            document.getElementById('lblQtyTotal').value = document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value;

            var position = document.getElementById('<%=hdfPosition.ClientID%>');

            //alert(position.value);


            // if (position.value == 'เจ้าของ') {
            document.getElementById('lblPriceTotal').value = document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value;
            // }



        });

        $(function () {
            $("#link_pop").popover('show');
        });

        function pageLoad() {
            $("#link_pop").popover('show');

            //document.getElementById('lblQtyTotal').value = document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value;
            //document.getElementById('lblPriceTotal').value = document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value;
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

        function validateFloatKeyPress(el, evt) {
            //  alert(el.value);
            if ((event.keyCode < 48 || event.keyCode > 57)) {
                event.returnValue = false;
            }

            //var charCode = (evt.which) ? evt.which : event.keyCode;

            //if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            //    return false;
            //}

            //if (charCode == 46 && el.value.indexOf(".") !== -1) {
            //    return false;
            //}

            //if (el.value.indexOf(".") !== -1) {
            //    var range = document.selection.createRange();

            //    if (range.text != "") {
            //    }
            //    else {
            //        var number = el.value.split('.');
            //        if (number.length == 2 && number[1].length > 1)
            //            return false;
            //    }
            //}

            return true;
        }

        function ClearValue(txt, price, stock) {
           <%-- var calAmount = parseFloat(txt.value == '' ? '0' : txt.value) * parseFloat(price.innerHTML == '' ? '0' : price.innerHTML);

            var calGrand_Total_Qty = parseInt(document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value.split(',').join(''));
            calGrand_Total_Qty -= parseInt(txt.value == '' ? '0' : txt.value);
            document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value = calGrand_Total_Qty;


            var calGrand_Total_Amount = parseInt(document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value.split(',').join(''));
            calGrand_Total_Amount -= calAmount;
            document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value = calGrand_Total_Amount;--%>


            global_Quantity = txt.value == '' ? '0' : txt.value;
        }


        var global_Quantity = 0;

        function UpdateField(txt, price, stock) {

            if (isNaN(txt.value)) {
                txt.value = global_Quantity;
            } else {




                var tmp_Quantity = txt.value == '' ? '0' : txt.value;
                var tmp_Stock = stock.innerHTML == '' ? '0' : stock.innerHTML;



                if (parseInt(tmp_Quantity) > parseInt(tmp_Stock)) {
                    alert('ไม่สามารถเบิกเกินจำนวนสินค้าที่มีในสต๊อก');
                    txt.value = global_Quantity;
                } else {


                    var calGrand_Total_Qty = parseInt(document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value.split(',').join(''));
                var calGrand_Total_Amount = parseFloat(document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value.split(',').join(''));

                if (parseInt(tmp_Quantity) > parseInt(global_Quantity)) {
                    calGrand_Total_Qty = parseInt(calGrand_Total_Qty) + (parseInt(tmp_Quantity) - parseInt(global_Quantity));
                    calGrand_Total_Amount = parseFloat(calGrand_Total_Amount) + parseFloat(((parseInt(tmp_Quantity) - parseInt(global_Quantity)) * parseFloat(price.innerHTML)));
                } else {
                    calGrand_Total_Qty = parseInt(calGrand_Total_Qty) - (parseInt(global_Quantity) - parseInt(tmp_Quantity));
                    calGrand_Total_Amount = parseFloat(calGrand_Total_Amount) - parseFloat(((parseInt(global_Quantity) - parseInt(tmp_Quantity)) * parseFloat(price.innerHTML)));
                }
                document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value = formatCurrency(parseInt(calGrand_Total_Qty));
                document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value = formatCurrency(calGrand_Total_Amount);

                document.getElementById('lblQtyTotal').value = document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value;

                var position = document.getElementById('<%=hdfPosition.ClientID%>');


                //  alert(position.value);

                // if (position.value == 'เจ้าของ') {
                document.getElementById('lblPriceTotal').value = document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value;
                // }

            }



<%--            var calAmount = parseFloat(txt.value == '' ? '0' : txt.value) * parseFloat(price.innerHTML == '' ? '0' : price.innerHTML);

            var calGrand_Total_Qty = parseInt(document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value.split(',').join(''));
            calGrand_Total_Qty += parseInt(txt.value == '' ? '0' : txt.value);
            document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value = calGrand_Total_Qty;
            var calGrand_Total_Amount = parseInt(document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value.split(',').join(''));

            calGrand_Total_Amount += calAmount;

            document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value = calGrand_Total_Amount;--%>

            }
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


        function validateFloatKeyPress2(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

          
            return false;
           
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">เบิกอื่นๆ</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                           
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_FromDate" runat="server" CssClass="form-control" onkeypress="return  validateFloatKeyPress2(this,event);"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxt_FromDate" runat="server"
                                            TargetControlID="txt_FromDate" PopupButtonID="cldtxt_FromDate" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_ToDate" runat="server" CssClass="form-control" onkeypress="return  validateFloatKeyPress2(this,event);"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxt_ToDate" runat="server"
                                            TargetControlID="txt_ToDate" PopupButtonID="cldtxt_ToDate" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ผู้เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchUserSP" runat="server" CssClass="form-control" autopostback="true"
                                            DataTextField="FullName_ddl" DataValueField="User_ID" onselectedindexchanged="ddlUserSP_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">เหตุผล:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchReason" runat="server" CssClass="form-control"
                                            DataTextField="VALUE" DataValueField="KEY">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>แถม</asp:ListItem>
                                            <asp:ListItem>ชิม</asp:ListItem>
                                            <asp:ListItem>หมดอายุ</asp:ListItem>
                                            <asp:ListItem>อื่นๆ</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" onclick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                 
                                <asp:Button ID="btnAddNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnAddNew_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px;padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่มีรายการเบิกสินค้า</center>
                                    </div>
                                </asp:Panel>
                                <asp:GridView ID="GridViewOtherRequistion"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false" onrowcommand="GridViewOtherRequistion_RowCommand"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    AllowPaging="true" PageSize="10" OnDataBound="GridViewOtherRequistion_DataBound" OnRowDataBound="GridViewOtherRequistion_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ" CommandArgument='<%# Eval("Requisition_No") %>' CommandName="_Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="เลขที่ใบเบิก">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_Requisition_No" runat="server" Text='<%# Eval("Requisition_No") %>'
                                                    CommandArgument='<%# Eval("Requisition_No") %>'
                                                    CommandName="View"
                                                    style="font-weight: bold; text-decoration: underline; text-align: left">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เบิก">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Requisition_Date" runat="server" Text='<%# Eval("Requisition_Date", "{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ผู้เบิก">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Requisition_Name" runat="server" Text='<%# Eval("Requisition_FullName") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ยอดเงิน" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Grand_Total_Amount" runat="server" Text='<%# Eval("Grand_Total_Amount", "{0:N2}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เหตุผล">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Reason" runat="server" Text='<%# Eval("Reason") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เหตุผลอื่นๆ">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Other_reason" runat="server" Text='<%# Eval("Other_reason") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_Print" runat="server" OnClientClick="myApp.showPleaseWait();" CommandArgument='<%# Eval("Requisition_No")%>' 
                                                    CssClass="btn btn-mini btn-primary" Text="พิมพ์ใบเบิกสินค้า" CommandName="Print"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
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
                            <span class="title" style="font-size: 18px">เบิกสินค้าอื่นๆ</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>

                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">CV CODE:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCV_Code" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">เลขที่ใบเบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRequisition_No" runat="server" CssClass="form-control" enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control" enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRequisition_Date" runat="server" style="border-left: 4px solid #ed1c24;" CssClass="form-control" onkeypress="return  validateFloatKeyPress2(this,event);" OnTextChanged="txtRequisition_Date_TextChanged" AutoPostBack ="true"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtRequisition_Date" runat="server"
                                            TargetControlID="txtRequisition_Date" PopupButtonID="cldtxtRequisition_Date" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เหตุผล:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlReason" runat="server" style="border-left: 4px solid #ed1c24;" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>แถม</asp:ListItem>
                                            <asp:ListItem>ชิม</asp:ListItem>
                                            <asp:ListItem>หมดอายุ</asp:ListItem>
                                            <asp:ListItem>อื่นๆ</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">คนที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlUserSP" runat="server" CssClass="form-control" autopostback="true"
                                            DataTextField="FullName_ddl" DataValueField="User_ID" onselectedindexchanged="ddlUserSP_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtOtherReason" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtOtherReason2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">จำนวนรวม:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtGrand_Total_Qty" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">มูลค่าการเบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtGrand_Total_Amount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:HiddenField ID="hdfPosition" runat="server"  /> 
                                   
                                </div>
                            </div>
                            <div class="tabbable-panel" style="margin-top: 20px">
                                <div class="tabbable-line">
                                    <ul class="nav nav-tabs ">
                                        <li class="active " style="width: 180px">
                                            <img src="../Images/tab1.png" id="imgTab1" style="position: absolute; top: -50px; z-index: 999" />
                                            <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab1click()">นมพาสเจอร์ไรส์</a>

                                        </li>
                                        <li style="width: 130px">
                                            <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab2click()">นมเปรี้ยว </a>

                                        </li>
                                        <li style="width: 170px">
                                            <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab3click()">โยเกิร์ตเมจิ </a>

                                        </li>
                                        <li style="width: 170px">
                                            <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_4" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab4click()">นมเปรี้ยวไพเก้น </a>

                                        </li>
                                        <li style="width: auto">
                                            <a href="#tab_default_5" id="tab5" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab5click()">อื่นๆ </a>

                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_default_1">
                                            <asp:GridView ID="GridViewOtherRequistion_1"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewOtherRequistion_1_RowDataBound"
                                                OnDataBound="GridViewOtherRequistion_1_OnDataBound"
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
                                                            <asp:Label ID="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="สต๊อกที่มีอยู่ในระบบ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>' itemstyle-horizontalalign="right"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" Width="84px" runat="server" Style="text-align: right"
                                                                maxlength="5" Text='<%# Eval("Total_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" Width="84px" runat="server" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblStock_on_Hand_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStock_on_Hand_ID" Width="84px" runat="server" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOther_Requisition_Detail_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOther_Requisition_Detail_ID" Width="84px" runat="server" Text='<%# Eval("Other_Requisition_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOldQty" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOldQty" Width="84px" runat="server" Text='<%# Eval("Total_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_2">
                                            <asp:GridView ID="GridViewOtherRequistion_2"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewOtherRequistion_2_RowDataBound"
                                                OnDataBound="GridViewOtherRequistion_2_OnDataBound"
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
                                                            <asp:Label ID="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="สต๊อกที่มีอยู่ในระบบ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>' itemstyle-horizontalalign="right"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" Width="84px" runat="server" Style="text-align: right"
                                                                maxlength="5" Text='<%# Eval("Total_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" Width="84px" runat="server" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblStock_on_Hand_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStock_on_Hand_ID" Width="84px" runat="server" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOther_Requisition_Detail_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOther_Requisition_Detail_ID" Width="84px" runat="server" Text='<%# Eval("Other_Requisition_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOldQty" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOldQty" Width="84px" runat="server" Text='<%# Eval("Total_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_3">
                                            <asp:GridView ID="GridViewOtherRequistion_3"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewOtherRequistion_3_RowDataBound"
                                                OnDataBound="GridViewOtherRequistion_3_OnDataBound"
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
                                                            <asp:Label ID="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="สต๊อกที่มีอยู่ในระบบ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>' itemstyle-horizontalalign="right"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" Width="84px" runat="server" Style="text-align: right"
                                                                maxlength="5" Text='<%# Eval("Total_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" Width="84px" runat="server" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblStock_on_Hand_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStock_on_Hand_ID" Width="84px" runat="server" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOther_Requisition_Detail_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOther_Requisition_Detail_ID" Width="84px" runat="server" Text='<%# Eval("Other_Requisition_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOldQty" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOldQty" Width="84px" runat="server" Text='<%# Eval("Total_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_4">
                                            <asp:GridView ID="GridViewOtherRequistion_4"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewOtherRequistion_4_RowDataBound"
                                                OnDataBound="GridViewOtherRequistion_4_OnDataBound"
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
                                                            <asp:Label ID="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="สต๊อกที่มีอยู่ในระบบ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>' itemstyle-horizontalalign="right"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" Width="84px" runat="server" Style="text-align: right"
                                                                maxlength="5" Text='<%# Eval("Total_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" Width="84px" runat="server" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblStock_on_Hand_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStock_on_Hand_ID" Width="84px" runat="server" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOther_Requisition_Detail_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOther_Requisition_Detail_ID" Width="84px" runat="server" Text='<%# Eval("Other_Requisition_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOldQty" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOldQty" Width="84px" runat="server" Text='<%# Eval("Total_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_5">
                                            <asp:GridView ID="GridViewOtherRequistion_5"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewOtherRequistion_5_RowDataBound"
                                                OnDataBound="GridViewOtherRequistion_5_OnDataBound"
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
                                                            <asp:Label ID="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="สต๊อกที่มีอยู่ในระบบ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>' itemstyle-horizontalalign="right"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" Width="84px" runat="server" Style="text-align: right"
                                                                maxlength="5" Text='<%# Eval("Total_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" Width="84px" runat="server" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblStock_on_Hand_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStock_on_Hand_ID" Width="84px" runat="server" Text='<%# Eval("Stock_on_Hand_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOther_Requisition_Detail_ID" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOther_Requisition_Detail_ID" Width="84px" runat="server" Text='<%# Eval("Other_Requisition_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lblOldQty" visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOldQty" Width="84px" runat="server" Text='<%# Eval("Total_Qty") %>'></asp:Label>
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
                                <asp:hiddenfield id="btnSaveMode" runat="server" />
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="ตกลง" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>

                        </div>
                    </div>
                </div>

                            <a tabindex="0"
                               id="link_pop"
                               class="btn btn-primary" 
                               role="button" 
                               data-html="true" 
                               data-toggle="popover"                                
                               title="<div class=text-label-bold><b>ข้อมูลการเบิกสินค้า</b></div>" 
                               style="margin-left:2px"
                               data-content="<div class=text-label-bold>จำนวนรวม: <br>
                                <input id=lblQtyTotal style=color:blue;border:0px;width:120px></div>
                                <div class=text-label-bold>ราคารวม:<br>
                                <input id=lblPriceTotal style=color:blue;border:0px;width:120px><br /></div>">
                                <span class="glyphicon glyphicon-info-sign"></span>
                            </a>

            </asp:Panel>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

