<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Main.master" CodeFile="ReportTotalSPStatus.aspx.cs" Inherits="Views_ReportTotalSPStatus" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายงานสถานะของ SP ทั้งหมด</span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">วันที่:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="InvoiceDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldInvoiceDate" runat="server"
                                                TargetControlID="InvoiceDate" PopupButtonID="cldInvoiceDate" />
                                        </div>
                                    </div>
                                    <div class="form-inline">
                                        <label class="col-md-2 control-label">ถึง:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="InvoiceDateTo" runat="server" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldInvoiceDateTo" runat="server"
                                                TargetControlID="InvoiceDateTo" PopupButtonID="cldInvoiceDateTo" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์รายงาน" OnClick="btnPrint_Click" />
                                <asp:Button ID="btnClear" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnClear_Click" />
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

