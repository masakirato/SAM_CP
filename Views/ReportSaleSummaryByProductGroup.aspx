<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Main.master" CodeFile="ReportSaleSummaryByProductGroup.aspx.cs" Inherits="Views_ReportSaleSummaryByProductGroup" UICulture="th" Culture="th-TH" %>

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
                            <span class="title" style="font-size: 18px">รายงานสรุปยอดขายแยกตามกลุ่มสินค้า</span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ภาค :</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList runat="server" ID="ddlSearchRegion" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY" OnSelectedIndexChanged="ddlSearchRegion_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อเอเยนต์ :</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlAgentName" runat="server" CssClass="form-control" DataTextField="CV_AgentName" DataValueField="CV_Code" AutoPostBack="true" OnSelectedIndexChanged="ddlAgentName_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_SPName" runat="server" CssClass="form-control" DataTextField="FullName_ddl" DataValueField="User_ID"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">วันที่เบิก :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="RequisitionDate_From" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldRequisitionDate_From" runat="server"
                                                TargetControlID="RequisitionDate_From" PopupButtonID="cldRequisitionDate_From" />
                                        </div>
                                    </div>
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">ถึง :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="RequisitionDate_To" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldRequisitionDate_To" runat="server"
                                                TargetControlID="RequisitionDate_To" PopupButtonID="cldRequisitionDate_To" />
                                        </div>
                                    </div>
                                </div>
                               <div class="form-group">
                                    <label class="col-md-2 control-label">กลุ่มสินค้า :</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_ProductGroup" runat="server" CssClass="form-control" datatextfield="Product_group_ID" datavaluefield="Product_group_ID" OnSelectedIndexChanged="ddl_ProductGroup_SelectedIndexChanged" AutoPostBack ="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ขนาด :</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddl_Size" runat="server" CssClass="form-control" datatextfield="Size" datavaluefield="Size" >
                                             
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์รายงาน" OnClick="btnPrint_Click" OnClientClick="return PrintConfirm()?myApp.showPleaseWait(): false;;"/>
                                <asp:Button ID="btnClear" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnClear_Click" OnClientClick="return ClearConfirm()?myApp.showPleaseWait(): false;;"/>
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
