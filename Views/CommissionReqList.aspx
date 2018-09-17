<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="CommissionReqList.aspx.cs" Inherits="Views_CommissionReqList" uiculture="th" culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

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

        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }

        function UpdateField() {

            var AvailableAmount = parseFloat(document.getElementById('<%=txtTotalAmount.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtTotalAmount.ClientID%>').value.split(',').join(''));
            var BalanceOutstanding = parseFloat(document.getElementById('<%=txtTotalBalanceOutstanding.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtTotalBalanceOutstanding.ClientID%>').value.split(',').join(''));
            var Amount = parseFloat(document.getElementById('<%=txtRequisition_Amount.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtRequisition_Amount.ClientID%>').value.split(',').join(''));
            var BalanceAmount = AvailableAmount - Amount;

            if (Amount == 0)
            {
                alert('กรุณาระบุ จำนวนเงินขอเบิก');
                document.getElementById('<%=txtRequisition_Amount.ClientID%>').value = '';
            }
            else if (Amount > AvailableAmount && AvailableAmount == 0) {
                alert('กรุณาเลือกเลขที่ใบเคลียร์เงินก่อนใส่จำนวนขอเบิก');
                document.getElementById('<%=txtRequisition_Amount.ClientID%>').value = '';
            }
            else if (Amount > AvailableAmount && AvailableAmount > 0) {
                alert('ไม่สามารถเบิกได้เกินจำนวน ยอดเงินเบิก');
                document.getElementById('<%=txtRequisition_Amount.ClientID%>').value = '';
            }
            else if (BalanceAmount < BalanceOutstanding && Amount > 0) {
                alert('ยอดเงินคงเหลือหลังเบิกค่าคอมมิสชั่นน้อยกว่ายอดหนี้คงค้าง');
                //document.getElementById('<%=txtRequisition_Amount.ClientID%>').value = '';
            }

        }

    </script>

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

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">เบิกค่าคอมมิชชั่น</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchSP" runat="server" CssClass="form-control" datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบเคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtClearing_No" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchClearing_Date_Begin" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtSearchClearing_Date_Begin" runat="server"
                                            TargetControlID="txtSearchClearing_Date_Begin" PopupButtonID="cldtxtSearchClearing_Date_Begin" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchClearing_Date_End" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtSearchClearing_Date_End" runat="server"
                                            TargetControlID="txtSearchClearing_Date_End" PopupButtonID="cldtxtSearchClearing_Date_End" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบขอเบิกเงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtRequisition_No" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่ขอเบิกเงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtRequistionStart" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtRequistionStart" runat="server"
                                            TargetControlID="txtRequistionStart" PopupButtonID="cldtxtRequistionStart" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtRequistionEnd" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtRequistionEnd" runat="server"
                                            TargetControlID="txtRequistionEnd" PopupButtonID="cldtxtRequistionEnd" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สถานะ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="ยังไม่ได้เบิก"></asp:ListItem>
                                            <asp:ListItem Text="เบิกบางส่วน"></asp:ListItem>
                                            <asp:ListItem Text="เบิกแล้ว"></asp:ListItem>
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
                            <div class="col-md-12">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Button ID="btnAddNew" class="btn btn-primary" runat="server" Text="บันทึกเบิก" onclick="btnAddNew_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <label class="col-md-2 control-label">ยอดเงินเบิก:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSum" runat="server" CssClass="form-control" enabled="false" style="text-align: right" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                    <center>ไม่มีรายการค่าคอมมิชชั่น</center>
                                </div>
                            </asp:Panel>
                            <div class="col-md-12">
                                <asp:GridView ID="grdCommission"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    OnRowCommand="grdCommission_RowCommand"
                                    onrowdatabound="grdCommission_RowDataBound"
                                    ondatabound="grdCommission_DataBound"
                                    AllowPaging="false" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" oncheckedchanged="chkAll_CheckedChanged" />
                                            </ItemTemplate>
                                            <itemstyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อพนักงาน">
                                            <ItemTemplate>
                                                <asp:Label id="lblSPName" runat="server" style="color: blue" Text='<%# Eval("SP_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="เลขที่ใบขอเบิก" ShowHeader="False">
                                            <ItemTemplate>



                                                <asp:LinkButton ID="lnkRequisition_No" runat="server" CssClass="btn btn-link" Text='<%# Eval("Requisition_No") %>'
                                                    CommandName="View" CommandArgument='<%# Eval("Requisition_No") %>' style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                            </ItemTemplate>
                                            <itemstyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่ขอเบิกเงิน">
                                            <ItemTemplate>
                                                <asp:Label id="lblDate" runat="server" style="color: blue" Text='<%# Eval("Commission_requisition_date","{0:d/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเคลียร์เงิน">
                                            <ItemTemplate>
                                                <asp:Label id="lblClearing_No" runat="server" Text='<%# Eval("Clearing_No") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เคลียร์เงิน">
                                            <ItemTemplate>
                                                <asp:Label id="lblClearingDate" runat="server" style="color: blue" Text='<%# Eval("Clearing_Date","{0:d/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ค่าคอมมิชชั่น" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lblCommissionAmount" runat="server" style="color: blue" Text='<%# Eval("Commission") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะ">
                                            <ItemTemplate>
                                                <asp:Label id="lblStatus" runat="server" style="color: blue" Text='<%# Eval("Commission_Requisition_Status") %>'></asp:Label>
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
            <asp:Panel ID="pnlForm" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">เบิกค่าคอมมิชชั่น</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_SPName" runat="server" CssClass="form-control" datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtClearing_Begin" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtClearing_Begin" runat="server"
                                            TargetControlID="txtClearing_Begin" PopupButtonID="cldtxtClearing_Begin" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtClearing_End" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtClearing_End" runat="server"
                                            TargetControlID="txtClearing_End" PopupButtonID="txtClearing_End" />
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12 text-center">
                                <asp:hiddenfield id="hdfTotal_Balance_Outstanding" runat="server" />
                                <asp:hiddenfield id="hdfTotal_Credit_Amount" runat="server" />
                                <asp:Button ID="btnSearch_2_Submit" class="btn btn-primary" runat="server" Text="ค้นหา" onclick="btnSearch_2_Submit_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnSearch_2_Cancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnSearch_2_Cancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>

                            <div class="col-md-12 text-center">
                                <hr />
                            </div>
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <label class="col-md-2 control-label">ยอดหนี้คงค้าง:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtTotalBalanceOutstanding" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">ยอดเงินเบิก:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtTotalAmount" runat="server" CssClass="form-control" Enabled=" false"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">จำนวนเงินขอเบิก:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtRequisition_Amount" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress()"
                                            onblur="UpdateField()"></asp:textbox>
                                    </div>

                                </div>

                            </div>

                            <div class="col-md-12">
                                <asp:HiddenField id="hdfChkUser_ID" runat="server" />
                                <asp:GridView ID="grdCommissionRequisition"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333" ondatabound="grdCommissionRequisition_DataBound"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" autopostback="true" oncheckedchanged="chkAll_CheckedChanged1" />
                                                <%--<asp:Label id="lblBalanceOut" runat="server" style="color: blue"></asp:Label>--%>
                                                <asp:Label id="lblUser_ID" runat="server" style="color: blue" Text='<%# Eval("User_ID") %>' visible="false"></asp:Label>

                                            </ItemTemplate>
                                            <itemstyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อพนักงาน">
                                            <ItemTemplate>
                                                <asp:HiddenField id="hdfUser_ID" runat="server" value='<%# Eval("User_ID") %>' />
                                                <asp:Label id="lblSPName" runat="server" style="color: blue" Text='<%# Eval("SP_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเคลียร์เงิน">
                                            <ItemTemplate>
                                                <asp:Label id="lblClearing_No" runat="server" Text='<%# Eval("Clearing_No") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เคลียร์เงิน">
                                            <ItemTemplate>
                                                <asp:Label id="lblClearingDate" runat="server" style="color: blue" Text='<%# Eval("Clearing_Date","{0:d/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ค่าคอมมิชชั่น" itemstyle-horizontalalign="right">
                                            <ItemTemplate>
                                                <asp:Label id="lblCommissionAmount" runat="server" style="color: blue" Text='<%# Eval("Commission_Balance_Outstanding","{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะ">
                                            <ItemTemplate>
                                                <asp:Label id="lblStatus" runat="server" style="color: blue" Text='<%# Eval("Commission_Requisition_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requisition_No" visible="false">
                                            <ItemTemplate>
                                                <asp:Label id="lblRequisition_No" runat="server" style="color: blue" Text='<%# Eval("Requisition_No") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSaveSubmit" class="btn btn-primary" runat="server" Text="บันทึก" onclick="btnSaveSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnSaveCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnSaveCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
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
