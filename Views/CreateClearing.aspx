<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateClearing.aspx.cs"
    Inherits="Views_CreateClearing" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:HiddenField ID="hfTab" runat="server" />
            

            <style>
                #preview {
                    position: absolute;
                    border: 1px solid #ccc;
                    background: #333;
                    padding: 5px;
                    display: none;
                    color: #fff;
                }
            </style>

            <style type="text/css">
                .calendetextender {
                    border: Inset 1px black;
                    background-color: White;
                    margin-top: 0px;
                    margin-left: 0px;
                    width: 185px;
                    height: 190px;
                    font-size: 7pt;
                    font-family: Arial;
                }

                /*div.calendercustomerdetail {
                    /*float: left;
                    margin-left: 0px;
                    margin-top:0px;*/
                /*width: 10px;
                    height: 10px;
                    width: 10px;*/
                }
                */
            </style>


            <script src="../Scripts/jquery-1.10.2.min.js"></script>

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

                var specialKeys = new Array();
                specialKeys.push(8); //Backspace
                function IsNumeric(e) {
                    var keyCode = e.which ? e.which : e.keyCode
                    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
                    return ret;
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

                this.imagePreview = function () {

                    /* CONFIG */

                    xOffset = 10;
                    yOffset = 30;

                    // these 2 variable determine popup's distance from the cursor
                    // you might want to adjust to get the right result

                    /* END CONFIG */
                    $("a.preview").hover(function (e) {

                        this.t = this.title;
                        this.title = "";
                        var c = (this.t != "") ? "<br/>" + this.t : "";

                        $("body").append("<p id='preview'><img src='" + this.rel + "' alt='' width='200px' height='200px' />" + c + "</p>");
                        $("#preview")
                            .css("top", (e.pageY - xOffset) + "px")
                            .css("left", (e.pageX + yOffset) + "px")

                            .fadeIn("fast");
                    },
                    function () {
                        this.title = this.t;
                        $("#preview").remove();
                    });
                    $("linkbutton.preview").mousemove(function (e) {
                        $("#preview")
                            .css("top", (e.pageY - xOffset) + "px")
                            .css("left", (e.pageX + yOffset) + "px")
                    });
                };

                // starting the script on page load
                $(document).ready(function () {
                    imagePreview();
                });

                function validateFloatKeyPress(el, evt) {
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

                function validateFloatKeyPress1(el, evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;

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

                    return false;
                }


                var global_txt_Deposit_Qty = 0;
                var global_txt_Deposit_QtyReturn = 0;

                function ClearValue_txt_Deposit_Qty(tx1) {
                    global_txt_Deposit_Qty = tx1.value == '' ? '0' : tx1.value;
                }

                function ClearValue_txt_Deposit_QtyReturn(QtyReturn) {
                    global_txt_Deposit_QtyReturn = QtyReturn.value == '' ? '0' : QtyReturn.value;
                }

                function UpdateField_txt_Deposit_Qty(tx1, sale_qty, total_sale_qty, price, net_sale_qty, QtyReturn) {

                    var tmp_tx1 = tx1.value == '' ? '0' : tx1.value;
                    var calsumtotal = document.getElementById('<%=TextboxTotalAmount.ClientID%>').value.split(',').join('');
                    var calTextboxTotal = document.getElementById('<%=TextboxTotal.ClientID%>').value.split(',').join('');

                    if (parseInt(net_sale_qty.innerHTML) - parseInt(tmp_tx1) - parseInt(QtyReturn.value == '' ? '0' : QtyReturn.value) < 0) {


                        //alert('กรุณาระบุข้อมูลให้ถูกต้อง');
                        alert('จำนวนฝาก + จำนวนคืน ต้องไม่เกินจำนวนเบิก');
                        tx1.value = global_txt_Deposit_Qty;
                        tx1.focus();

                    } else {

                        sale_qty.value = parseInt(net_sale_qty.innerHTML) - parseInt(tmp_tx1) - parseInt(QtyReturn.value == '' ? '0' : QtyReturn.value);

                        total_sale_qty.value = formatCurrency(parseFloat((sale_qty.value)) * parseFloat((price.innerHTML)));


                        if (parseInt(tmp_tx1) > parseInt(global_txt_Deposit_Qty)) {

                            calsumtotal = parseInt(calsumtotal) - (parseInt(tmp_tx1) - parseInt(global_txt_Deposit_Qty));
                            calTextboxTotal = parseFloat(calTextboxTotal) - parseFloat((parseInt(tmp_tx1) - parseInt(global_txt_Deposit_Qty)) * (price.innerHTML));


                        } else {
                            calsumtotal = parseInt(calsumtotal) + (parseInt(global_txt_Deposit_Qty) - parseInt(tmp_tx1))

                            calTextboxTotal = parseFloat(calTextboxTotal) + parseFloat((parseInt(global_txt_Deposit_Qty) - parseInt(tmp_tx1)) * (price.innerHTML));

                        }

                        document.getElementById('<%=TextboxTotal.ClientID%>').value = formatCurrency(calTextboxTotal);
                        document.getElementById('<%=TextboxTotalAmount.ClientID%>').value = parseInt(calsumtotal);

                    }
                }

                function UpdateField_txt_Deposit_QtyReturn(tx1, sale_qty, total_sale_qty, price, net_sale_qty, QtyReturn, depositCheck, returnCheck, resign) {

                    var tmp_QtyReturn = QtyReturn.value == '' ? '0' : QtyReturn.value;

                    var calsumtotal = document.getElementById('<%=TextboxTotalAmount.ClientID%>').value.split(',').join('');
                    var calTextboxTotal = document.getElementById('<%=TextboxTotal.ClientID%>').value.split(',').join('');

                    if (parseInt(net_sale_qty.innerHTML) - parseInt(tmp_QtyReturn) - parseInt(tx1.value == '' ? '0' : tx1.value) < 0) {

                        //alert('กรุณาระบุข้อมูลให้ถูกต้อง');
                        //QtyReturn.value = '';
                        alert('จำนวนฝาก + จำนวนคืน ต้องไม่เกินจำนวนเบิก');
                        QtyReturn.value = global_txt_Deposit_QtyReturn;
                        QtyReturn.focus();
                    } else {

                        if (resign == "1") {
                            var depoValue = parseInt(depositCheck.innerHTML == '' ? '0' : depositCheck.innerHTML);
                            var returnValue = parseInt(returnCheck.innerHTML == '' ? '0' : returnCheck.innerHTML);
                            if (depoValue != 0) {
                                if (parseInt(tmp_QtyReturn) < (returnValue)) {
                                    alert('จำนวนคืน ต้องไม่น้อยกว่าจำนวนคืนเดิม (' + (returnValue) + ')');
                                    QtyReturn.value = global_txt_Deposit_QtyReturn;
                                    QtyReturn.focus();

                                    return;
                                }

                                if (parseInt(tmp_QtyReturn) > (depoValue + returnValue)) {

                                    //alert('กรุณาระบุข้อมูลให้ถูกต้อง');
                                    //QtyReturn.value = '';
                                    alert('จำนวนคืน ต้องไม่เกินจำนวนฝาก (' + (depoValue + returnValue) + ')');
                                    QtyReturn.value = global_txt_Deposit_QtyReturn;
                                    QtyReturn.focus();

                                    return;
                                }
                            } else {
                                if (parseInt(tmp_QtyReturn) < (returnValue)) {
                                    alert('จำนวนคืน ต้องไม่น้อยกว่าจำนวนคืนเดิม (' + (returnValue) + ')');
                                    QtyReturn.value = global_txt_Deposit_QtyReturn;
                                    QtyReturn.focus();

                                    return;
                                }

                                if (parseInt(tmp_QtyReturn) > (returnValue)) {
                                    alert('จำนวนคืน ต้องไม่มากกว่าจำนวนคืนเดิม (' + (returnValue) + ')');
                                    QtyReturn.value = global_txt_Deposit_QtyReturn;
                                    QtyReturn.focus();

                                    return;
                                }
                            }
                        }


                        sale_qty.value = parseInt(net_sale_qty.innerHTML) - parseInt(tmp_QtyReturn) - parseInt(tx1.value == '' ? '0' : tx1.value);
                        total_sale_qty.value = formatCurrency(parseFloat((sale_qty.value)) * parseFloat((price.innerHTML)));

                        if (parseInt(tmp_QtyReturn) > parseInt(global_txt_Deposit_QtyReturn)) {
                            calsumtotal = parseInt(calsumtotal) - (parseInt(tmp_QtyReturn) - parseInt(global_txt_Deposit_QtyReturn))
                            calTextboxTotal = parseFloat(calTextboxTotal) - parseFloat((price.innerHTML) * (parseInt(tmp_QtyReturn) - parseInt(global_txt_Deposit_QtyReturn)));

                        } else {
                            calsumtotal = parseInt(calsumtotal) + (parseInt(global_txt_Deposit_QtyReturn) - parseInt(tmp_QtyReturn))

                            calTextboxTotal = parseFloat(calTextboxTotal) + parseFloat((price.innerHTML) * (parseInt(global_txt_Deposit_QtyReturn) - parseInt(tmp_QtyReturn)));

                        }

                        document.getElementById('<%=TextboxTotal.ClientID%>').value = formatCurrency(calTextboxTotal);
                        document.getElementById('<%=TextboxTotalAmount.ClientID%>').value = parseInt(calsumtotal);
                    }
                }

                function UpdateField() {
                    var _txtTotal = parseFloat(document.getElementById('<%=txtTotal.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtTotal.ClientID%>').value.split(',').join(''));
                    var _txtDiscount = parseFloat(document.getElementById('<%=txtDiscount.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtDiscount.ClientID%>').value.split(',').join(''));
                    var _txtActualPayment = parseFloat(document.getElementById('<%=txtActualPayment.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtActualPayment.ClientID%>').value.split(',').join(''));
                    var _CalNetTotal = _txtTotal - _txtDiscount;
                    var _CalBalanceOutstanding = _txtTotal - _txtDiscount - _txtActualPayment;
                    var _CalDebtPayment = (_txtActualPayment - _CalNetTotal) > '0' ? (_txtActualPayment - _CalNetTotal) : '0';
                    var _txtNetTotal = parseFloat(document.getElementById('<%=txtNetTotal.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtNetTotal.ClientID%>').value.split(',').join(''));
                    var _txtclearing_Status = parseFloat(document.getElementById('<%=txtclearing_Status.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtclearing_Status.ClientID%>').value.split(',').join(''));
                    var _hdfcalSumDebt = parseFloat(document.getElementById('<%=hdfcalSumDebt.ClientID%>').value == '' ? '0' : document.getElementById('<%=hdfcalSumDebt.ClientID%>').value.split(',').join(''));
                    var _hdfsearch_dep_count = parseInt(document.getElementById('<%=hdfcalSumDebt.ClientID%>').value == '' ? '0' : document.getElementById('<%=hdfcalSumDebt.ClientID%>').value.split(',').join(''));
                    var _hdfclearingDebt_Balance = parseFloat(document.getElementById('<%=hdfclearingDebt_Balance.ClientID%>').value == '' ? '0' : document.getElementById('<%=hdfclearingDebt_Balance.ClientID%>').value.split(',').join(''));

                    var _calDebtBalance = _hdfcalSumDebt - _CalDebtPayment;

                    document.getElementById('<%=txtNetTotal.ClientID%>').value = formatCurrency(_CalNetTotal);
                    document.getElementById('<%=txtBalanceOutstanding.ClientID%>').value = formatCurrency(_CalBalanceOutstanding > '0' ? _CalBalanceOutstanding : '0');
                    document.getElementById('<%=txtDebtPayment.ClientID%>').value = formatCurrency(_CalDebtPayment);

                    if (_txtclearing_Status != '2') {


                        if (search_dep.Count > 0) {
                            // txtDebtBalance.Text = (calSumDebt - DebtPayment).Value.ToString("#,##0.#0");
                            document.getElementById('<%=txtDebtBalance.ClientID%>').value = formatCurrency(_calDebtBalance);
                        }
                        else {
                            txtDebtBalance.Text = "0";
                        }


                    }
                    else {
                        document.getElementById('<%=txtDebtBalance.ClientID%>').value = formatCurrency(_hdfclearingDebt_Balance);
                    }

                    //alert(_hdfclearingDebt_Balance);
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

                function Confim() {
                    var result = window.confirm('ยืนยันการเคลียร์เงินใช่หรือไม่?');
                    if (result == true)

                        return true;
                    else
                        return false;
                }

                function CheckDate(txtdate) {
                    //document.getElementById('<%=txtDebtBalance.ClientID%>').value 
                    var tempdate = txtdate.value;

                    //var datetime = Date.now();
                    var d = new Date();
                    var tempdate_now = d.getDate() + "/" + d.getMonth() + "/" + d.getFullYear();
                    alert(tempdate_now);
                    var curr_date = d.getDate();

                    var curr_month = d.getMonth();

                    var curr_year = d.getFullYear();

                    //alert(curr_date + "/" + curr_month + "/" + curr_year);
                   //document.write(curr_date + "-" + curr_month + "-" + curr_year);

                    //if (datetime < tempdate)
                    //{
                    //    alert('ไม่สามารถเลือกวันที่ย้อนหลังได้');
                    //}
                    //else {
                    //    alert('ไม่เข้าเงื่อนไข');

                    //}

                }

                function Confirm() {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("คุณต้องการบันทึกเครดิตลูกค้า หรือไม่?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }

                function ConfirmMessage() {

                    if (document.getElementById('<%=HiddenSession.ClientID %>').value != "") {
                        var selectedvalue = confirm("คุณต้องการบันทึกเครดิตลูกค้า หรือไม่?");
                        if (selectedvalue) {
                            document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
                        } else {
                            document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
                        }
                    }
                   
                }
        

  
            </script>

            <asp:Panel ID="pnlGrid" Visible="true" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายการใบเคลียร์เงิน</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_SPName" runat="server" CssClass="form-control" DataTextField="FullName_ddl" DataValueField="User_ID" ></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบเบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchRequisition_No" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_RequisitionDate_From" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxt_RequisitionDate_From" runat="server"
                                            TargetControlID="txt_RequisitionDate_From" PopupButtonID="cldtxt_RequisitionDate_From" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_RequisitionDate_TO" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxt_RequisitionDate_TO" runat="server"
                                            TargetControlID="txt_RequisitionDate_TO" PopupButtonID="cldtxt_RequisitionDate_TO" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบเคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchClearing_No" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_ClearingDate_From" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="Calendartxt_ClearingDate_From" runat="server"
                                            TargetControlID="txt_ClearingDate_From" PopupButtonID="Calendartxt_ClearingDate_From" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_ClearingDate_To" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="Calendartxt_ClearingDate_To" runat="server"
                                            TargetControlID="txt_ClearingDate_To" PopupButtonID="Calendartxt_ClearingDate_To" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="ButtonAddNew" class="btn btn-primary" runat="server" Text="สร้างใบเคลียร์เงิน" OnClick="ButtonAddNew_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:HiddenField ID="hdfUser_ID" runat="server" />
                                <asp:HiddenField ID="hdfReplace_Sale" runat="server" />
                                <asp:HiddenField ID="hdfRequisition_No" runat="server" />
                                <asp:HiddenField ID="hdfClearing_Status" runat="server" />
                                <asp:HiddenField ID="hdfPriceExpire" runat="server" />
                                <asp:HiddenField ID="hdfResign" runat="server" />
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                    <center>ไม่มีรายการเคลียร์เงิน</center>
                                </div>
                            </asp:Panel>
                            <div class="col-md-12" style="overflow: scroll">
                                <asp:GridView ID="GridViewClearing"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    OnRowCommand="GridViewClearing_RowCommand" OnDataBound="GridViewClearing_DataBound" OnRowDeleting="GridViewClearing_RowDeleting"
                                    OnRowDataBound="GridViewClearing_RowDataBound"
                                    AllowPaging="true" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เคลียร์เงิน" ShowHeader="False" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkClearing" runat="server" AutoPostBack="true" OnCheckedChanged="chkClearing_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเคลียร์เงิน" ShowHeader="False" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBClearing_No" runat="server" CssClass="btn btn-link" CommandArgument='<%# Container.DataItemIndex %>'
                                                    CommandName="View" Style="font-weight: bold; text-decoration: underline" Text='<%# Eval("Clearing_No") %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เคลียร์เงิน" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblClearing_Date" runat="server" Style="color: blue" Text='<%# Eval("Clearing_Date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อพนักงาน" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SP_Name" runat="server" Text='<%# Eval("SP_Name") %>' Style="color: blue"></asp:Label>
                                           <span>
                                                <asp:HiddenField ID="hdfUser_ID_SP" Value='<%# Eval("User_ID") %>' runat="server" />
                                           </span>
                                                 </ItemTemplate>
                                            
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเบิก" HeaderStyle-Wrap="false">
                                            <ItemTemplate>

                                                <asp:Label ID="lnkB_Requisition_No" runat="server" Text='<%# Eval("Requisition_No") %>' Style="color: blue"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เบิก" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Requisition_Date" runat="server" Text='<%# Eval("Requisition_Date", "{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะ" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Status" runat="server" Text='<%# Eval("Status") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBConfirmClearing" runat="server" OnClientClick="myApp.showPleaseWait();" CssClass="btn btn-mini btn-primary" Text="พิมพ์ใบเคลียร์เงิน"
                                                    CommandName="Print" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <span onclick="return confirm('คุณต้องการเคลียร์เงินค้างชำระทั้งหมด ,เลขที่ใบเคลียร์เงิน: '+'<%# Eval("Clearing_No") %>'+',ชื่อพนักงาน: '+'<%# Eval("SP_Name") %>'+' หรือไม่?')">
                                                <asp:LinkButton ID="lnkBPrintClearing" runat="server" Visible="false"
                                                    CssClass="btn btn-mini btn-primary" Text="การเคลียร์เงินค้างชำระทั้งหมด" CommandArgument='<%# Container.DataItemIndex %>'
                                                    CommandName="ClearingResign"></asp:LinkButton>
                                                     </span>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBCreditPayment" runat="server" Visible="false"
                                                    CssClass="btn btn-mini btn-primary" Text="จ่ายหนี้ลูกค้า" CommandArgument='<%# Container.DataItemIndex %>'
                                                    CommandName="ClearingCreditPayment"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUser_ID" runat="server" Text='<%# Eval("User_ID") %>'></asp:Label>
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

            <asp:Panel ID="pnlStep1" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ฝากสินค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSP_Name" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRequisition_Date" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">เลขที่ใบเบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRequisition_No" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtClearing_Date" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">เลขที่ใบเคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtClearing_No" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">จำนวนยอดขายรวม(ชิ้น):</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextboxTotalAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ยอดขายรวม(บาท):</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextboxTotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                </div>
                            </div>
                            <div class="tabbable-panel" style="margin-top: 20px">
                                <div class="tabbable-line" id="dvTab">
                                    <ul class="nav nav-tabs ">
                                        <li class="active" style="width: 180px" id="clearingTab1" runat="server">
                                            <img src="../Images/tab1.png" id="imgTab1" style="position: absolute; top: -50px; z-index: 999" />
                                            <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab1click()">นมพาสเจอร์ไรส์</a>
                                        </li>
                                        <li style="width: 130px" id="clearingTab2" runat="server">
                                            <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab2click()">นมเปรี้ยว </a>

                                        </li>
                                        <li style="width: 170px" id="clearingTab3" runat="server">
                                            <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab3click()">โยเกิร์ตเมจิ </a>
                                        </li>
                                        <li style="width: 170px" id="clearingTab4" runat="server">
                                            <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_4" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab4click()">นมเปรี้ยวไพเก้น </a>
                                        </li>
                                        <li style="width: auto" id="clearingTab5" runat="server">
                                            <a href="#tab_default_5" id="tab6" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab5click()">อื่นๆ </a>
                                        </li>
                                    </ul>

                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_default_1">
                                            <asp:GridView ID="GridViewClearing_1"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewClearing_1_RowDataBound"
                                                OnDataBound="GridViewClearing_1_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed HeaderFreez">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <%--<asp:Label id="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>--%>

                                                            <a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>
                                                            <asp:Label runat="server" ID="Label_Product_ID" Text='<%# Eval("Product_ID") %>' Visible="false" />

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
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Net_Sales_Qty" runat="server" Text='<%# Eval("Total_Qty") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนฝาก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_Qty" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2)  %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Deposit_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldDeposit_Qty" runat="server" Value='<%# Eval("Deposit_Qty")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="คืนของ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_QtyReturn" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2) + 1 %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Return_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldReturn_Qty" runat="server" Value='<%# Eval("Return_Qty")%>' />
                                                            <span style="display: none">
                                                                <asp:Label ID="Label_CheckDeposit_Qty" runat="server" Text='<%# Eval("Deposit_Qty")%>'></asp:Label>
                                                                <asp:Label ID="Label_CheckReturn_Qty" runat="server" Text='<%# Eval("Return_Qty")%>'></asp:Label>
                                                            </span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนยอดขายรวม(ชิ้น)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_Sales_Qty" runat="server" Style="color: blue; text-align: right" Width="84px" Enabled="false" Text='<%# Eval("Sales_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดขายรวม(บาท)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_TotalSales_Qty" runat="server" Style="color: blue; text-align: right" Enabled="false" Width="84px" Text='<%# Eval("Sales_Amount") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_SP_Price" runat="server" Style="color: blue" Text='<%# Eval("SP_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Deposit_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Deposit_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_2">
                                            <asp:GridView ID="GridViewClearing_2"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewClearing_2_RowDataBound"
                                                OnDataBound="GridViewClearing_2_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed HeaderFreez">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <%--<asp:Label id="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>--%>

                                                            <a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>
                                                            <asp:Label runat="server" ID="Label_Product_ID" Text='<%# Eval("Product_ID") %>' Visible="false" />

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
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Net_Sales_Qty" runat="server" Text='<%# Eval("Total_Qty") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนฝาก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_Qty" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2)  %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Deposit_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldDeposit_Qty" runat="server" Value='<%# Eval("Deposit_Qty")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="คืนของ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_QtyReturn" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2) + 1 %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Return_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldReturn_Qty" runat="server" Value='<%# Eval("Return_Qty")%>' />
                                                            <span style="display: none">
                                                                <asp:Label ID="Label_CheckDeposit_Qty" runat="server" Text='<%# Eval("Deposit_Qty")%>'></asp:Label>
                                                                <asp:Label ID="Label_CheckReturn_Qty" runat="server" Text='<%# Eval("Return_Qty")%>'></asp:Label>
                                                            </span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนยอดขายรวม(ชิ้น)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_Sales_Qty" runat="server" Style="color: blue; text-align: right" Width="84px" Enabled="false" Text='<%# Eval("Sales_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดขายรวม(บาท)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_TotalSales_Qty" runat="server" Style="color: blue; text-align: right" Enabled="false" Width="84px" Text='<%# Eval("Sales_Amount") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_SP_Price" runat="server" Style="color: blue" Text='<%# Eval("SP_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Deposit_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Deposit_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_3">
                                            <asp:GridView ID="GridViewClearing_3"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewClearing_3_RowDataBound"
                                                OnDataBound="GridViewClearing_3_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed HeaderFreez">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <%--<asp:Label id="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>--%>

                                                            <a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>
                                                            <asp:Label runat="server" ID="Label_Product_ID" Text='<%# Eval("Product_ID") %>' Visible="false" />

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
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Net_Sales_Qty" runat="server" Text='<%# Eval("Total_Qty") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนฝาก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_Qty" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2)  %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Deposit_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldDeposit_Qty" runat="server" Value='<%# Eval("Deposit_Qty")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="คืนของ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_QtyReturn" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2) + 1 %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Return_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldReturn_Qty" runat="server" Value='<%# Eval("Return_Qty")%>' />
                                                            <span style="display: none">
                                                                <asp:Label ID="Label_CheckDeposit_Qty" runat="server" Text='<%# Eval("Deposit_Qty")%>'></asp:Label>
                                                                <asp:Label ID="Label_CheckReturn_Qty" runat="server" Text='<%# Eval("Return_Qty")%>'></asp:Label>
                                                            </span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนยอดขายรวม(ชิ้น)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_Sales_Qty" runat="server" Style="color: blue; text-align: right" Width="84px" Enabled="false" Text='<%# Eval("Sales_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดขายรวม(บาท)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_TotalSales_Qty" runat="server" Style="color: blue; text-align: right" Enabled="false" Width="84px" Text='<%# Eval("Sales_Amount") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_SP_Price" runat="server" Style="color: blue" Text='<%# Eval("SP_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Deposit_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Deposit_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_4">
                                            <asp:GridView ID="GridViewClearing_4"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewClearing_4_RowDataBound"
                                                OnDataBound="GridViewClearing_4_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed HeaderFreez">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <%--<asp:Label id="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>--%>

                                                            <a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>
                                                            <asp:Label runat="server" ID="Label_Product_ID" Text='<%# Eval("Product_ID") %>' Visible="false" />

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
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Net_Sales_Qty" runat="server" Text='<%# Eval("Total_Qty") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนฝาก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_Qty" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2)  %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Deposit_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldDeposit_Qty" runat="server" Value='<%# Eval("Deposit_Qty")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="คืนของ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_QtyReturn" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2) + 1 %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Return_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldReturn_Qty" runat="server" Value='<%# Eval("Return_Qty")%>' />
                                                            <span style="display: none">
                                                                <asp:Label ID="Label_CheckDeposit_Qty" runat="server" Text='<%# Eval("Deposit_Qty")%>'></asp:Label>
                                                                <asp:Label ID="Label_CheckReturn_Qty" runat="server" Text='<%# Eval("Return_Qty")%>'></asp:Label>
                                                            </span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนยอดขายรวม(ชิ้น)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_Sales_Qty" runat="server" Style="color: blue; text-align: right" Width="84px" Enabled="false" Text='<%# Eval("Sales_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดขายรวม(บาท)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_TotalSales_Qty" runat="server" Style="color: blue; text-align: right" Enabled="false" Width="84px" Text='<%# Eval("Sales_Amount") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_SP_Price" runat="server" Style="color: blue" Text='<%# Eval("SP_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Deposit_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Deposit_Detail_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_5">
                                            <asp:GridView ID="GridViewClearing_5"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewClearing_5_RowDataBound"
                                                OnDataBound="GridViewClearing_5_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed HeaderFreez">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <%--<asp:Label id="Label_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>--%>

                                                            <a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>
                                                            <asp:Label runat="server" ID="Label_Product_ID" Text='<%# Eval("Product_ID") %>' Visible="false" />

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
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Net_Sales_Qty" runat="server" Text='<%# Eval("Total_Qty") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนฝาก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_Qty" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2)  %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Deposit_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldDeposit_Qty" runat="server" Value='<%# Eval("Deposit_Qty")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="คืนของ" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Deposit_QtyReturn" runat="server" Style="color: blue; text-align: right" MaxLength="4"
                                                                OnTextChanged="txt_Deposit_Qty_TextChanged" AutoPostBack="false" TabIndex="<%# ( Container.DataItemIndex * 2) + 1 %>"
                                                                autocomplete="off" Width="84px" Text='<%# Eval("Return_Qty") %>' />
                                                            <asp:HiddenField ID="hdfOldReturn_Qty" runat="server" Value='<%# Eval("Return_Qty")%>' />
                                                            <span style="display: none">
                                                                <asp:Label ID="Label_CheckDeposit_Qty" runat="server" Text='<%# Eval("Deposit_Qty")%>'></asp:Label>
                                                                <asp:Label ID="Label_CheckReturn_Qty" runat="server" Text='<%# Eval("Return_Qty")%>'></asp:Label>
                                                            </span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนยอดขายรวม(ชิ้น)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_Sales_Qty" runat="server" Style="color: blue; text-align: right" Width="84px" Enabled="false" Text='<%# Eval("Sales_Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดขายรวม(บาท)" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Label_TotalSales_Qty" runat="server" Style="color: blue; text-align: right" Enabled="false" Width="84px" Text='<%# Eval("Sales_Amount") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_SP_Price" runat="server" Style="color: blue" Text='<%# Eval("SP_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Deposit_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Deposit_Detail_ID") %>'></asp:Label>
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
                                <asp:HiddenField ID="btnSaveMode" runat="server" />
                                <asp:Button ID="ButtonSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="ButtonSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="ButtonSaveAndNext" class="btn btn-default" runat="server" Text="บันทึกและหน้าถัดไป" OnClick="ButtonSaveAndNext_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="ButtonDipositCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="ButtonDipositCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <br />
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>

            <asp:Panel ID="pnlStep2" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">บันทึกเครดิตลูกค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="ButtonNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClientClick="myApp.showPleaseWait(); return true;" OnClick="ButtonNew_Click" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' />
                                    </div>
                                    <div class="form-group">
                                        <asp:GridView ID="GridViewCredit"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames=""
                                            ShowFooter="true"
                                            OnRowCancelingEdit="GridViewCredit_RowCancelingEdit"
                                            OnRowDataBound="GridViewCredit_RowDataBound"
                                            OnRowDeleting="GridViewCredit_RowDeleting"
                                            OnRowEditing="GridViewCredit_RowEditing"
                                            OnRowUpdating="GridViewCredit_RowUpdating"
                                            OnRowCommand="GridViewCredit_RowCommand"
                                            CellPadding="0" ForeColor="#333333"
                                            ShowHeaderWhenEmpty="true"
                                            CssClass="table table-striped table-bordered table-condensed">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="Center">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                            <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>'></asp:LinkButton>
                                                        </span>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="Center" FooterStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBEdit" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkBEditCancel" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkFooterCancel" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' CssClass="btn btn-mini btn-default" runat="server" CausesValidation="True" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อลูกค้า" HeaderStyle-Width="600px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCustomerName" runat="server" Style="color: blue;" Text='<%# Eval("Customer_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblCustomer_ID" runat="server" Style="color: blue;" Text='<%# Eval("Customer_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlFooterCustomerName" runat="server" CssClass="form-control"
                                                            DataTextField="Full_Name" DataValueField="Customer_ID" Width="600px" Style="border-left: 4px solid #ed1c24;">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่ค้างชำระ" HeaderStyle-Width="180px">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="txtEditCredit_Date" runat="server" Style="color: blue;" Text='<%# Eval("Credit_Date","{0:dd/MM/yyyy}") %>' Width="180px"></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCredit_Date" runat="server" Style="color: blue;" Text='<%# Eval("Credit_Date","{0:dd/MM/yyyy}") %>' Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtFooterCredit_Date" runat="server" Style="border-left: 4px solid #ed1c24;"
                                                            AutoPostBack="true" OnTextChanged="txtFooterCredit_Date_TextChanged" CssClass="form-control" Width="180px" onkeypress="return validateFloatKeyPress1(this,event);"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cldtxtFooterCredit_Date"
                                                            runat="server"
                                                            TargetControlID="txtFooterCredit_Date"
                                                            PopupButtonID="cldtxtFooterCredit_Date" />
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงินค้างชำระ" HeaderStyle-Width="180px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditCredit_Amount" runat="server" Style="color: blue; text-align: right; border-left: 4px solid #ed1c24;" Text='<%# Eval("Credit_Amount") %>' Width="180px" onkeypress="return validateFloatKeyPress(this,event);" CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCredit_Amount" runat="server" Text='<%# Eval("Credit_Amount","{0:N2}") %>' Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtFooterCredit_Amount" runat="server" CssClass="form-control"
                                                            onkeypress="return validateFloatKeyPress(this,event);" Style="color: blue; text-align: right; border-left: 4px solid #ed1c24;" Width="180px">
                                                        </asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit_ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCredit_ID" runat="server" Text='<%# Eval("Credit_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOldCredit_ID" runat="server" Text='<%# Eval("Credit_Amount")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>

                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                            <div class="col-md-12 text-center">
                                 <asp:HiddenField ID="txtconformmessageValue" runat="server" />
                                 <asp:HiddenField ID="HiddenSession" runat="server" />
                                <asp:Button ID="btnCreditPaymentBack" class="btn btn-primary" runat="server" Text="ย้อนกลับ" OnClick="btnCreditPaymentBack_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnCreditPaymentNext" class="btn btn-primary" runat="server" Text="ถัดไป" OnClick="btnCreditPayment_Click" OnClientClick="myApp.showPleaseWait(); return true;"  />
                                 <%--<asp:Button ID="btnCreditPaymentNext" class="btn btn-primary" runat="server" Text="ถัดไป" OnClick="btnCreditPayment_Click" OnClientClick="Confirm();  myApp.showPleaseWait(); return true; " />--%>
                                 <%--<asp:Button ID="btnCreditPaymentNext" class="btn btn-primary" runat="server" Text="ถัดไป" OnClick="btnCreditPayment_Click" OnClientClick ="return Confirm() ? myApp.showPleaseWait() : false;" />--%>
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>


            </asp:Panel>

            <asp:Panel ID="pnlStep3" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">จ่ายเงินเครดิตลูกค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">ชื่อลูกค้า:</label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="DropDownListCustomer" runat="server" CssClass="form-control" DataTextField="Full_Name" DataValueField="Customer_ID">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-md-2 control-label">วันที่ค้างชำระ:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSearchCreditPaymentDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldtxtSearchCreditPaymentDate" runat="server"
                                                TargetControlID="txtSearchCreditPaymentDate" PopupButtonID="txtSearchCreditPaymentDate" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">สถานะ:</label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlSearchCreditPaymentStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                                <asp:ListItem Text="ค้างชำระ" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="ชำระครบแล้ว" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnSearchCreditPayment" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearchCreditPayment_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                            <asp:Button ID="btnSearchCreditPaymentCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnSearchCreditPaymentCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Panel ID="pnlNoCreditPayment" runat="server" Visible="false">
                                            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                                <center>ไม่พบข้อมูล หนี้ลูกค้า</center>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="GridViewCreditPayment"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames=""
                                            ShowFooter="false"
                                            OnRowCancelingEdit="GridViewCreditPayment_RowCancelingEdit"
                                            OnRowDataBound="GridViewCreditPayment_RowDataBound"
                                            OnRowDeleting="GridViewCreditPayment_RowDeleting"
                                            OnRowEditing="GridViewCreditPayment_RowEditing"
                                            OnRowUpdating="GridViewCreditPayment_RowUpdating"
                                            OnRowCommand="GridViewCreditPayment_RowCommand"
                                            CellPadding="0"
                                            ForeColor="#333333"
                                            CssClass="table table-striped table-bordered table-condensed">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ชื่อลูกค้า" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAddNewCreditPayment" runat="server" Text="สร้าง" CssClass="btn btn-mini btn-primary" Visible="false" OnClick="btnAddNewCreditPayment_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                                        <asp:GridView ID="GridViewCustomer"
                                                            runat ="server"
                                                            AutoGenerateColumns="False"
                                                            ShowFooter="false"
                                                            OnRowCancelingEdit="GridViewCustomer_RowCancelingEdit"
                                                            OnRowDataBound="GridViewCustomer_RowDataBound"
                                                            OnRowDeleting="GridViewCustomer_RowDeleting"
                                                            OnRowEditing="GridViewCustomer_RowEditing"
                                                            OnRowUpdating="GridViewCustomer_RowUpdating"
                                                            OnRowCommand="GridViewCustomer_RowCommand"
                                        
                                                            CellPadding="0"
                                                            ForeColor="#333333"
                                                            CssClass="table table-striped table-bordered table-condensed"
                                                            Style="margin-top: 10px">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                                            <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Eval("Payment_No") %>' CommandName="Delete"></asp:LinkButton>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div style="text-align: center;">
                                                                            <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" FooterStyle-Wrap="false">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBEdit" runat="server" OnClientClick="myApp.showPleaseWait(); return true;" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="lnkBEditCancel" runat="server" OnClientClick="myApp.showPleaseWait(); return true;" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:LinkButton ID="lnkFooterCancel" OnClientClick="myApp.showPleaseWait(); return true;" CssClass="btn btn-mini btn-default" runat="server" CausesValidation="True" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                                    </FooterTemplate>
                                                                    <ItemStyle Wrap="false" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่ชำระเงิน" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-Wrap="false">
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtFooterPaymentDate" runat="server" Width="120px" Style="border-left: 4px solid #ed1c24;" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFooterPaymentDate_TextChanged" onkeypress="return validateFloatKeyPress1(this,event);" Enabled="false" ></asp:TextBox>
                                                                        
                                                                        <ajaxToolkit:CalendarExtender ID="cldtxtFooterPaymentDate"
                                                                            runat="server"
                                                                            TargetControlID="txtFooterPaymentDate"
                                                                            PopupButtonID="cldtxtFooterPaymentDate" PopupPosition="TopLeft" />
                                                                    </FooterTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtItemPayment_Date" runat="server" CssClass="form-control" Text='<%# Eval("Payment_Date","{0:dd/MM/yyyy}") %>' Width="120px" Style="border-left: 4px solid #ed1c24;"  AutoPostBack="true" OnTextChanged="txtFooterPaymentDate_TextChanged" onkeypress="return validateFloatKeyPress1(this,event);" Enabled ="false" ></asp:TextBox>
                                                                        <ajaxToolkit:CalendarExtender ID="cldtxtItemPayment_Date"
                                                                            runat="server"
                                                                            TargetControlID="txtItemPayment_Date"
                                                                            PopupButtonID="cldtxtItemPayment_Date" PopupPosition="TopLeft" />
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayment_Date" runat="server" Text='<%# Eval("Payment_Date","{0:dd/MM/yyyy}") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="จำนวนเงินชำระ" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtFooterPaymentAmount" runat="server" Width="100px" CssClass="form-control" Style="text-align: right; border-left: 4px solid #ed1c24;" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtPayment_Amount" runat="server" CssClass="form-control" Text='<%# Eval("Payment_Amount")%>' Width="100px" Style="text-align: right; border-left: 4px solid #ed1c24;" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdfOldPayment_Amount" runat="server" Value='<%# Eval("Payment_Amount")%>' />
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayment_Amount" runat="server" Text='<%# Eval("Payment_Amount","{0:N2}")%>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วิธีการชำระ" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList ID="ddlEditpaymentmethod" runat="server" Width="120px" CssClass="form-control"
                                                                            Style="border-left: 4px solid #ed1c24;">
                                                                            <asp:ListItem Text="เงินสด" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="เช็ค" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="โอน" Value="3"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="hdfEditPaymenyMethod" runat="server" Value='<%# Eval("Payment_Method")%>' />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList ID="ddlFooterpaymentmethod" runat="server" Width="120px" Style="border-left: 4px solid #ed1c24;" CssClass="form-control">
                                                                            <asp:ListItem Text="เงินสด" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="เช็ค" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="โอน" Value="3"></asp:ListItem>
                                                                        </asp:DropDownList>


                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txteditpaymentmethod" runat="server" Text='<%# Eval("Payment_Method")%>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- FooterStyle-Width="300px"--%>
                                                                <asp:TemplateField HeaderText="ธนาคาร" HeaderStyle-Wrap="false" ItemStyle-Wrap="true">
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList ID="ddlFooterbank" runat="server" Width="200px" DataTextField="VALUE" DataValueField="KEY" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtItemBank" runat="server" Text='<%# Eval("Bank")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList ID="ddlEditbank" runat="server" Width="200px" DataTextField="VALUE" DataValueField="KEY" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="hdfEditbank" runat="server" Value='<%# Eval("Bank")%>' />
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เลขที่" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtEditCheque_No" runat="server" Text='<%# Eval("Cheque_No")%>' Width="150px" CssClass="form-control"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCheque_No" runat="server" Text='<%# Eval("Cheque_No")%>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtFooterCheque_Name" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่">
                                                                    <EditItemTemplate>
                                                                        <ajaxToolkit:CalendarExtender ID="cldtxtEditDate"
                                                                            runat="server"
                                                                            TargetControlID="txtEditDate"
                                                                            PopupButtonID="cldtxtEditDate" CssClass="calendetextender" PopupPosition="TopLeft" />
                                                                        <asp:TextBox ID="txtEditDate" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}")%>' Width="120px" CssClass="form-control" onkeypress="return validateFloatKeyPress1(this,event);"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtItemDate" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}")%>' Width="120px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <ajaxToolkit:CalendarExtender ID="cldtxtFooterDate"
                                                                            runat="server"
                                                                            TargetControlID="txtFooterDate"
                                                                            PopupButtonID="cldtxtFooterDate" CssClass="calendetextender" PopupPosition="TopLeft" />
                                                                        <asp:TextBox ID="txtFooterDate" runat="server" Width="120px" CssClass="form-control" onkeypress="return validateFloatKeyPress1(this,event);"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="รับเงินจากเช็ค" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkItemClearing_Cheque" runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:CheckBox ID="chkFooterClearing_Cheque" runat="server" />
                                                                    </FooterTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:CheckBox ID="chkEditClearing_Cheque" runat="server" />
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayment_No" runat="server" Text='<%# Eval("Payment_No")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("Customer_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่ค้างชำระ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCredit_Date" runat="server" Text='<%#Eval("Credit_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงินค้างชำระ" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCredit_Amount" runat="server" Text='<%#Eval("Credit_Amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงินชำระรวม" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal_Payment_Amount" runat="server" Text='<%#Eval("Total_Payment_Amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงินค้างชำระคงเหลือ" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalance_Outstanding_Amount" runat="server" Text='<%#Eval("Balance_Outstanding_Amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="สถานะ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCustomerName" runat="server" Text="จ่ายหนี้เครดิต" OnClientClick="myApp.showPleaseWait(); return true;" CssClass="btn btn-mini btn-primary" CommandName="ShowCredit" CommandArgument='<%# Container.DataItemIndex%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit_ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCredit_ID" runat="server" Text='<%# Eval("Credit_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:HiddenField ID="hdfPaymentFlag" runat="server" />
                                <%-- <asp:Button ID="btnConfirmPayment" class="btn btn-primary" runat="server" Text="ยืนยันการจ่ายหนี้ลูกค้า"
                                    OnClick="btnConfirmPayment_Click" OnClientClick="return confirm('ยืนยันการจ่ายหนี้ลูกค้าใช่หรือไม่')?myApp.showPleaseWait():'true';" Visible="false" />--%>
                                <asp:Button ID="btnConfirmPayment" class="btn btn-primary" runat="server" Text="ยืนยันการจ่ายหนี้ลูกค้า" OnClick="btnConfirmPayment_Click" OnClientClick="return confirm('ยืนยันการจ่ายหนี้ลูกค้าใช่หรือไม่?')?myApp.showPleaseWait(): false;" Visible="false" />
                                <asp:Button ID="btnConfirmPaymentBackToGrid" class="btn btn-default" runat="server" Text="กลับหน้าค้นหา"
                                    OnClick="btnConfirmPaymentBackToGrid_Click" OnClientClick="myApp.showPleaseWait(); return true;" Visible="false" />
                                <br />
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnBackToCredit" class="btn btn-primary" runat="server" Text="ย้อนกลับ" OnClick="btnBackToCredit_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="ButtonSubsidy" class="btn btn-primary" runat="server" Text="ถัดไป" OnClick="ButtonSubsidy_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlStep4" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">เงินอื่นๆ</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <h4>เงินช่วยเหลือ</h4>
                            <div class="form-horizontal">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnAddNewGridViewSubsidy" OnClientClick="myApp.showPleaseWait(); return true;" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnAddNewGridViewSubsidy_Click" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="GridViewSubsidy"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames=""
                                            ShowFooter="false"
                                            OnRowCancelingEdit="GridViewSubsidy_RowCancelingEdit"
                                            OnRowDataBound="GridViewSubsidy_RowDataBound"
                                            OnRowDeleting="GridViewSubsidy_RowDeleting"
                                            OnRowEditing="GridViewSubsidy_RowEditing"
                                            OnRowUpdating="GridViewSubsidy_RowUpdating"
                                            OnRowCommand="GridViewSubsidy_RowCommand"
                                            CellPadding="0" ForeColor="#333333"
                                            CssClass="table table-striped table-bordered table-condensed">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                            <asp:LinkButton ID="lnkBDelete" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Eval("Subsidy_ID") %>' CommandName="Delete"></asp:LinkButton>
                                                        </span>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" FooterStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBEdit" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkBEditCancel" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkFooterCancel" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' CssClass="btn btn-mini btn-default" runat="server" CausesValidation="True" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ลำดับ">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEditIndex" runat="server" Width="100px" Text='<%# Container.DataItemIndex +1 %>' Style="color: blue;"></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemIndex" runat="server" Width="100px" Text='<%# Container.DataItemIndex + 1 %>' Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="รายละเอียด" FooterStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlEditDetail" runat="server" Width="600px" Style="border-left: 4px solid #ed1c24;" CssClass="form-control"
                                                            DataTextField="Account_Name" DataValueField="Account_Code">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdfEditDetail" runat="server" Value='<%# Eval("Subsidy_Detail")%>' />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCredit_Date" runat="server" Text='<%# Eval("Subsidy_Detail") %>' Width="600px" Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlFooterDetail" runat="server" Width="600px"
                                                            Style="border-left: 4px solid #ed1c24;" CssClass="form-control"
                                                            DataTextField="Account_Name" DataValueField="Account_Code">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงิน" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditSubsidy_Amount" runat="server" Text='<%# Eval("Subsidy_Amount") %>' CssClass="form-control"
                                                            Style="color: blue; border-left: 4px solid #ed1c24; text-align: right" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSubsidy_Amount" runat="server" Text='<%# Eval("Subsidy_Amount","{0:N2}") %>' Style="color: blue; text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtFooterSubsidy_Amount" runat="server" Style="color: blue; text-align: right; border-left: 4px solid #ed1c24;" CssClass="form-control"
                                                            onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubsidy_ID" runat="server" Text='<%# Eval("Subsidy_ID") %>' Style="color: blue; text-align: right"></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-md-12 text-center">
                                        <br />
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <br />
                                    </div>

                                </div>
                            </div>

                            <h4>เงินหักอื่นๆ</h4>
                            <div class="form-horizontal">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnAddNewGridViewDeduct" OnClientClick="myApp.showPleaseWait(); return true;" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnAddNewGridViewDeduct_Click" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="GridViewDeduct"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames=""
                                            ShowFooter="false"
                                            OnRowCancelingEdit="GridViewDeduct_RowCancelingEdit"
                                            OnRowDataBound="GridViewDeduct_RowDataBound"
                                            OnRowDeleting="GridViewDeduct_RowDeleting"
                                            OnRowEditing="GridViewDeduct_RowEditing"
                                            OnRowUpdating="GridViewDeduct_RowUpdating"
                                            OnRowCommand="GridViewDeduct_RowCommand"
                                            CellPadding="0" ForeColor="#333333"
                                            CssClass="table table-striped table-bordered table-condensed">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                            <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Eval("Deduct_ID") %>' CommandName="Delete"></asp:LinkButton>
                                                        </span>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" FooterStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBEdit" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkBEditCancel" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkFooterCancel" Visible='<%# hdfClearing_Status.Value == "1" ? true:false %>' CssClass="btn btn-mini btn-default" runat="server" CausesValidation="True" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="false" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ลำดับ">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEditIndex" runat="server" Width="100px" Text='<%# Container.DataItemIndex +1 %>' Style="color: blue;"></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemIndex" runat="server" Width="100px" Text='<%# Container.DataItemIndex + 1 %>' Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="รายละเอียด" FooterStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlEditDetail" runat="server" Width="600px" Style="border-left: 4px solid #ed1c24;" CssClass="form-control"
                                                            DataTextField="Account_Name" DataValueField="Account_Code">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdfEditDetail" runat="server" Value='<%# Eval("Deduct_Detail")%>' />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelCredit_Date" runat="server" Width="600px" Text='<%# Eval("Deduct_Detail") %>' Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlFooterDetail" runat="server" Width="600px"
                                                            Style="border-left: 4px solid #ed1c24;" CssClass="form-control"
                                                            DataTextField="Account_Name" DataValueField="Account_Code">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงิน" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCredit_Amount" runat="server" Text='<%# Eval("Deduct_Amount") %>' CssClass="form-control"
                                                            Style="color: blue; border-left: 4px solid #ed1c24; text-align: right" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelCredit_Amount" runat="server" Text='<%# Eval("Deduct_Amount","{0:N2}") %>' Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtFooterDeduct_Amount" runat="server" CssClass="form-control"
                                                            Style="color: blue; text-align: right; border-left: 4px solid #ed1c24;" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeduct_ID" runat="server" Text='<%# Eval("Deduct_ID") %>' Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-md-12 text-center">
                                        <br />
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <div class="col-md-12 text-center">
                                            
                                            <asp:Button ID="btnBackDeduct" class="btn btn-primary" runat="server" Text="ย้อนกลับ" OnClick="btnBackDeduct_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                            <asp:Button ID="ButtonDeduct" class="btn btn-primary" runat="server" Text="ถัดไป" OnClick="ButtonDeduct_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>

            <asp:Panel ID="pnlStep5" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">สรุปการเคลียร์เงิน</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal" id="Divpayment" runat="server">
                                <div class="form-group">
                                    <label class="col-md-4 control-label">ยอดขายรวมจำนวนสินค้า:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtNet_Sales_Qty" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">ชิ้น</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">ยอดขายรวมทั้งหมด:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtNet_Sales_Amount" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">ยอดเครดิตลูกค้า:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtCredit_Amount" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">ยอดเงินสดSP:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtSP_Cash" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">เก็บเงินเครดิตลูกค้า:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <label class="col-md-2 text-left"></label>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                    </div>
                                    <label class="col-md-2 control-label">เงินสด:</label>
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtCashPaymentAmount" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                    </div>
                                    <label class="col-md-2 control-label">เช็ค:</label>
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtChequePaymentAmount" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                    </div>
                                    <label class="col-md-2 control-label">โอน:</label>
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtTransferPaymentAmount" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">รวมเงินสดขายสินค้า:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">เงินช่วยเหลือ:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <label class="col-md-2 text-left"></label>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-6">
                                        <asp:GridView ID="grdSubsidy"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="" GridLines="None" ShowHeader="false"
                                            ShowFooter="false" BorderStyle="NotSet" BorderWidth="0px"
                                            CellPadding="0" ForeColor="#333333"
                                            CssClass="table table-borderless table-condensed">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemIndex" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubsidy_Detail" Style="text-align: left" runat="server" Text='<%# Eval("Subsidy_Detail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="128px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSubsidy_Amount" runat="server" Text='<%# Eval("Subsidy_Amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="left" ItemStyle-Width="84px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBath" runat="server" Text="บาท"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-2">
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">เงินหักอื่นๆ:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <label class="col-md-2 text-left"></label>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-6">
                                        <asp:GridView ID="grdDeduct"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="" GridLines="None" ShowHeader="false"
                                            ShowFooter="false" BorderStyle="NotSet" BorderWidth="0px"
                                            CellPadding="0" ForeColor="#333333"
                                            CssClass="table table-borderless">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemIndex" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeduct_Detail" Style="text-align: left" runat="server" Text='<%# Eval("Deduct_Detail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="128px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemDeduct_Amount" runat="server" Text='<%# Eval("Deduct_Amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="left" ItemStyle-Width="84px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBath" runat="server" Text="บาท"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">สรุปยอดเงินควรนำส่ง:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" Enabled="false" Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">ส่วนลด:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <%--<asp:TextBox ID="txtDiscount" runat="server" Text="0" CssClass="form-control" Style="text-align: right" onkeypress="return validateFloatKeyPress(this,event);"
                                            onblur="UpdateField()"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtDiscount" runat="server" Text="0" CssClass="form-control" Style="text-align: right" onkeypress="return validateFloatKeyPress(this,event);"
                                            AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">สรุปเงินสดที่ต้องจ่าย:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtNetTotal" runat="server" Style="text-align: right" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">จ่ายจริง:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <%-- <asp:TextBox ID="txtActualPayment" runat="server" Style="text-align: right"
                                            onkeypress="return validateFloatKeyPress(this,event);"
                                            CssClass="form-control" onblur="UpdateField()"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtActualPayment" runat="server" Style="text-align: right"
                                            onkeypress="return validateFloatKeyPress(this,event);"
                                            CssClass="form-control" AutoPostBack="true" OnTextChanged="txtActualPayment_TextChanged"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">ค้างชำระ:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtBalanceOutstanding" runat="server" Style="text-align: right" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label">ชำระหนี้เงินสด:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDebtPayment" runat="server" Style="text-align: right" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblDebtBalance" runat="server" class="col-md-4 control-label" Font-Bold="true"> </asp:Label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDebtBalance" runat="server" Style="text-align: right" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">แต้มโบนัสวันนี้:</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtTodayPoints" runat="server" Style="text-align: right" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 text-left">แต้ม</label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">คำนวนค่าคอมมิชชั่น(เต็ม):</label>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtTodayCommission" runat="server" Style="text-align: right" CssClass="form-control" Enabled="false"></asp:TextBox>

                                        <asp:HiddenField ID="txtclearing_Status" runat="server" />
                                        <asp:HiddenField ID="hdfcalSumDebt" runat="server" />
                                        <asp:HiddenField ID="hdfsearch_dep_count" runat="server" />
                                        <asp:HiddenField ID="hdfclearingDebt_Balance" runat="server" />

                                    </div>
                                    <label class="col-md-2 text-left">บาท</label>
                                </div>
                            </div>


                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonSaveClearing" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="ButtonSaveClearing_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <%--<asp:Button ID="ButtonConfirmClearing" class="btn btn-primary" runat="server" Text="ยืนยันการเคลียร์เงิน" OnClick="ButtonConfirmClearing_Click"  OnClientClick ="return confirm('ยืนยันการเคลียร์เงินใช่หรือไม่')?myApp.showPleaseWait():'true';" />--%>
                                <asp:Button ID="ButtonConfirmClearing" class="btn btn-primary" runat="server" Text="ยืนยันการเคลียร์เงิน" OnClick="ButtonConfirmClearing_Click" OnClientClick="return confirm('ยืนยันการเคลียร์เงินใช่หรือไม่?')?myApp.showPleaseWait(): false;" />

                                <asp:Button ID="ButtonCancel" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="ButtonCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>


            </asp:Panel>

            <asp:Panel ID="pnlStep" Visible="true" runat="server">
                <br />
                <br />
                <div class="container">
                    <div class="stepwizard">
                        <div class="stepwizard-row setup-panel">
                            <div class="stepwizard-step">
                                <asp:Button ID="ButtonStep1" runat="server" class="btn btn-default btn-circle active-step" Text="1" OnClick="ButtonStep1_Click" />
                                <p>ฝากของ</p>
                            </div>
                            <div class="stepwizard-step">
                                <asp:Button ID="ButtonStep2" runat="server" class="btn btn-default btn-circle" Text="2" OnClick="ButtonStep2_Click" />
                                <p>เครดิต</p>
                            </div>
                            <div class="stepwizard-step">
                                <asp:Button ID="ButtonStep3" runat="server" class="btn btn-default btn-circle" Text="3" OnClick="ButtonStep3_Click" />
                                <p>จ่ายหนี้เครดิต</p>
                            </div>
                            <div class="stepwizard-step">
                                <asp:Button ID="ButtonStep4" runat="server" class="btn btn-default btn-circle" Text="4" OnClick="ButtonStep4_Click" />
                                <p>เงินอื่นๆ</p>
                            </div>
                            <div class="stepwizard-step">
                                <asp:Button ID="ButtonStep5" runat="server" class="btn btn-default btn-circle" Text="5" OnClick="ButtonStep5_Click" />
                                <p>สรุปการเคลียร์เงิน</p>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </asp:Panel>

            <asp:Panel ID="pnlResign" Visible="false" runat="server">
                <br />
                <br />
                <div class="container">
                    <div class="stepwizard">
                        <div class="stepwizard-row setup-panel">
                            <div class="stepwizard-step">
                                <asp:Button ID="ButtonResignStep1" runat="server" class="btn btn-default btn-circle active-step" Text="1" />
                                <p>คืนของ</p>
                            </div>
                            <div class="stepwizard-step">
                                <asp:Button ID="ButtonResignStep2" runat="server" class="btn btn-default btn-circle" Text="2" />
                                <p>สรุปการเคลียร์เงิน</p>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
