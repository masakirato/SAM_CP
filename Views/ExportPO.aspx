<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="ExportPO.aspx.cs" Inherits="Views_ExportPO" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=gdvExportPO.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportPO" />
        </Triggers>
        <ContentTemplate>
            <div class="container-full-content">
                <div class="row">
                    <div class="col-md-12 top-bar-content-none" style="height: 45px">
                        <span class="title" style="font-size: 18px">Export PO</span>&nbsp;&nbsp;
                        <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                    </div>
                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <%--<div class="form-inline">--%>
                                <label class="col-md-2 control-label">รอบวันที่สั่ง:</label>
                                <div class="col-md-4">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="กรุณาระบุวันที่เริ่ม" ControlToValidate="txtStart_Date" ValidationGroup="NewsValidation"></asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="txtStart_Date" runat="server" CssClass="form-control"></asp:TextBox>

                                    <ajaxToolkit:CalendarExtender ID="aceStart_Date" runat="server" TargetControlID="txtStart_Date" PopupButtonID="imgStart_Date" />
                                </div>
                                <label class="col-md-2 control-label">ถึง:</label>
                                <div class="col-md-4">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="กรุณาระบุวันที่สิ้นสุด" ControlToValidate="txtEnd_Date" ValidationGroup="NewsValidation"></asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="txtEnd_Date" runat="server" CssClass="form-control"></asp:TextBox>

                                    <ajaxToolkit:CalendarExtender ID="acrEnd_Date" runat="server" TargetControlID="txtEnd_Date" PopupButtonID="imgEnd_Date" />
                                </div>
                                <%-- </div>--%>
                            </div>

                            <div class="form-group">
                                <%--SelectedIndexChanged="ddlAgentName_SelectedIndexChanged"--%>
                                <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlAgentName" runat="server" CssClass="form-control"
                                        DataTextField="CV_AgentName" 
                                        DataValueField="CV_Code"
                                        OnSelectedIndexChanged="ddlAgentName_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2 control-label">Window Time:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlWindowTime" runat="server" CssClass="form-control" DataTextField="WindowTime" DataValueField="Order_Cycle_ID">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-2 control-label">สถานะ:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlOrderingStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="==ระบุ==" Value=""></asp:ListItem>
                                        <asp:ListItem Text="สั่งของแล้ว"></asp:ListItem>
                                        <asp:ListItem Text="ยังไม่สั่งของ"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                        </div>
                        <div class="col-md-12">
                            <hr />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnExportPO" class="btn btn-primary" runat="server" Text="Export PO" OnClick="btnExportPO_Click" />
                        </div>
                        <div class="col-md-12">
                            <br />
                        </div>

                        <div class="col-md-12">
                            <div style="overflow: auto">
                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่พบข้อมูล</center>
                                    </div>
                                </asp:Panel>
                                <asp:GridView ID="gdvExportPO"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed" OnRowCommand="gdvExportPO_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Export PO" ShowHeader="False" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="30px">
                                            <HeaderTemplate>
                                                <input id="CheckboxAll" type="checkbox" onclick="CheckAll(this)" runat="server" title="Click for select / remove all." />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxExportPO" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" itemstyle-horizontalalign="Center" Visible ="false">
                                            <ItemTemplate>
                                               <%-- <span onclick="return confirm('คุณต้องการยกเลิก เลขที่ใบสั่งซื้อ:) %>')?myApp.showPleaseWait(): true;">
                                                    <asp:LinkButton ID="lnkBCancel" runat="Server" CssClass="btn btn-mini btn-danger" Width="60px" Text="ยกเลิก" CommandArgument='<%# Eval("PO_Number") %>' CommandName="Cancel"></asp:LinkButton>
                                                </span>--%>
                                                <span onclick="return confirm('คุณต้องการยกเลิก เลขที่ใบสั่งซื้อ: '+'<%# Eval("PO_Number") %>' +' หรือไม่ ?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="60px" Text="ยกเลิก"
                                                        CommandArgument='<%# Eval("PO_Number") %>'
                                                        CommandName="_Cancel"></asp:LinkButton>
                                                </span>
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ" ShowHeader="False" itemstyle-wrap="false" headerstyle-wrap="false"  >
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="lnkBPO_No" runat="server" CssClass="btn btn-link" OnClientClick="myApp.showPleaseWait(); return true;" 
                                                    Text='<%# Eval("PO_Number") %>'
                                                    CommandArgument='<%# Eval("PO_Number") %>'
                                                    CommandName="View" Style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                               <%-- <asp:LinkButton ID="lnkBPO_No" runat="server" CssClass="btn btn-link" Text='<%# Eval("PO_Number") %>' 
                                                    CommandArgument='<%# Eval("PO_Number") %>' CommandName="View" Style="font-weight: bolder; text-decoration: underline"></asp:LinkButton>--%>
                                                 <!--<asp:Label ID="lnkBInvoice_No" runat="server" Text='<%# Eval("PO_Number") %>' Style="color: blue"></asp:Label>-->
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สร้างใบสั่งซื้อ" itemstyle-wrap="false" headerstyle-wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Date" runat="server" Text='<%# Eval("Date_of_create_order_or_PO_Date","{0:dd/MM/yyyy}") == null ? string.Empty :Eval("Date_of_create_order_or_PO_Date","{0:dd/MM/yyyy}")  %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่รับสินค้า" itemstyle-wrap="false" headerstyle-wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReceiving_Date" runat="server" Text='<%# Eval("Date_of_delivery_goods","{0:dd/MM/yyyy}")== null ? string.Empty :Eval("Date_of_delivery_goods","{0:dd/MM/yyyy}")  %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รอบวันที่สั่ง" itemstyle-wrap="false" headerstyle-wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrder_Date" runat="server" Text='<%# Eval("date_id","{0:dd/MM/yyyy}")== null ? string.Empty :Eval("date_id","{0:dd/MM/yyyy}")  %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อเอเยนต์" itemstyle-wrap="false" headerstyle-wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAgentName" runat="server" Text='<%# Eval("AgentName") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ราคารวมสั่งซื้อทั้งหมด" ItemStyle-HorizontalAlign="right" itemstyle-wrap="false" headerstyle-wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalOrderAmount" runat="server" Text='<%# Eval("Total_Amount_before_vat_included","{0:N2}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ยอดรวมทั้งสิ้น" ItemStyle-HorizontalAlign="right" itemstyle-wrap="false" headerstyle-wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("Total_amount_after_vat_included","{0:N2}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ทำการสั่งซื้อหรือยัง" itemstyle-wrap="false" headerstyle-wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsOrdered" runat="server" Text='<%# Eval("Order_Status") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="เบอร์โทรศัพท์ผู้รับผิดชอบ" itemstyle-wrap="false" headerstyle-wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Home_Phone_No") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

