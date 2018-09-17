<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Main.master" CodeFile="ReportSummaryPickingOtherProduct.aspx.cs" Inherits="Views_ReportSummaryPickingOtherProduct" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function validateFloatKeyPress(el, evt) {
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
                            <span class="title" style="font-size: 18px">รายงานสรุปการเบิกสินค้าอื่นๆ</span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">วันที่:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="CountStockDate_From" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldCountStockDate_From" runat="server"
                                                TargetControlID="CountStockDate_From" PopupButtonID="cldCountStockDate_From" />
                                        </div>
                                    </div>
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">ถึง:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="CountStockDate_To" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldCountStockDate_To" runat="server"
                                                TargetControlID="CountStockDate_To" PopupButtonID="cldCountStockDate_To" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <%-- <label class="col-md-2 control-label">รหัสสินค้า:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txbProductID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>--%>
                                    <label class="col-md-2 control-label">ชื่อสินค้า:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_productname" runat="server" CssClass="form-control" datatextfield="Product_Name" datavaluefield="Product_ID"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">คนที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_RequisitionName" runat="server" CssClass="form-control" DataTextField="SP_Name" DataValueField="User_ID"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เหตุผล:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_Reason" runat="server" CssClass="form-control">
                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                            <asp:ListItem>แถม</asp:ListItem>
                                            <asp:ListItem>ชิม</asp:ListItem>
                                            <asp:ListItem>หมดอายุ</asp:ListItem>
                                            <asp:ListItem>อื่นๆ</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์รายงาน" OnClick="btnPrint_Click" OnClientClick="return PrintConfirm()?myApp.showPleaseWait(): false;;" />
                                <asp:Button ID="btnClear" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnClear_Click" OnClientClick="return ClearConfirm()?myApp.showPleaseWait(): false;;" />
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>

                            <div class="col-md-12">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function ClearConfirm() {
            if (confirm("ต้องการยกเลิกเงื่อนไขการพิมพ์รายงาน ใช่หรือไม่?")) {
                return true;
            } else {
                return false;
            }
        }

        function PrintConfirm() {
            if (confirm("ต้องการพิมพ์รายงาน ใช่หรือไม่?")) {
                return true;
            } else {
                return false;
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

</asp:Content>
