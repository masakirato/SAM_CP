<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Main.master" CodeFile="ReportRevenue.aspx.cs" Inherits="Views_ReportRevenue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายงานรายรับ</span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เดือน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="DDLMonthGroup" runat="server" CssClass="form-control">
                                             <asp:ListItem Value="1">มกราคม</asp:ListItem>
                                            <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                                            <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                                            <asp:ListItem Value="4">เมษายน</asp:ListItem>
                                            <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                                            <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                                            <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                                            <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                                            <asp:ListItem Value="9">กันยายน</asp:ListItem>
                                            <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                                            <asp:ListItem Value="11">พฤษจิกายน</asp:ListItem>
                                            <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="DDLMonthGroupTo" runat="server" CssClass="form-control">
                                             <asp:ListItem Value="1">มกราคม</asp:ListItem>
                                            <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                                            <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                                            <asp:ListItem Value="4">เมษายน</asp:ListItem>
                                            <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                                            <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                                            <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                                            <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                                            <asp:ListItem Value="9">กันยายน</asp:ListItem>
                                            <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                                            <asp:ListItem Value="11">พฤษจิกายน</asp:ListItem>
                                            <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ปี:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
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
