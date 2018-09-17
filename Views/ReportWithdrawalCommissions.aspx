<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Main.master" CodeFile="ReportWithdrawalCommissions.aspx.cs" Inherits="Views_ReportWithdrawalCommissions" UICulture="th" Culture="th-TH" %>

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
        function validateFloatKeyPress(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;


            return false;

        }


    </script>

    <script type = "text/javascript">
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
    
    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายงานการเบิกค่าคอมมิชชั่น</span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_SPName" runat="server" CssClass="form-control" DataTextField="FullName_ddl" DataValueField="User_ID"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">วันที่เคลียร์เงิน :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txt_Clearing_Date_From" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldClearing_Date_From" runat="server"
                                                TargetControlID="txt_Clearing_Date_From" PopupButtonID="cldClearing_Date_From" />
                                        </div>
                                    </div>
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">ถึง :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txt_Clearing_Date_TO" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldClearing_Date_TO" runat="server"
                                                TargetControlID="txt_Clearing_Date_TO" PopupButtonID="cldClearing_Date_TO" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">วันที่ขอเบิกเงิน :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txt_Requisition_Date_From" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldRequisition_Date_From" runat="server"
                                                TargetControlID="txt_Requisition_Date_From" PopupButtonID="cldRequisition_Date_From" />
                                        </div>
                                    </div>
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">ถึง :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txt_Requisition_Date_To" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldRequisition_Date_To" runat="server"
                                                TargetControlID="txt_Requisition_Date_To" PopupButtonID="cldRequisition_Date_To" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                               <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์รายงาน" OnClick="btnPrint_Click" OnClientClick="return PrintConfirm()?myApp.showPleaseWait(): false;"/>
                                <asp:Button ID="btnClear" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnClear_Click" OnClientClick = " return ClearConfirm()?myApp.showPleaseWait(): false;"/>
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
</asp:Content>
