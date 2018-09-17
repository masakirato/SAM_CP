﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Site.Main.master" CodeFile="ReportPoint_Agent.aspx.cs" Inherits="Views_ReportPoint_Agent" UICulture="th" Culture="th-TH" %>

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

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }


            return true;
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
                            <span class="title" style="font-size: 18px">รายงานแต้มสำหรับเอเยนต์</span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">

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
                                    <label class="col-md-2 control-label">เดือน :</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlMonthGroup_From" runat="server" CssClass="form-control">
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

                                    <label class="col-md-2 control-label">ปี :</label>
                                    <div class="col-md-4">

                                        <asp:TextBox ID="txt_YearGroup_From" runat="server" CssClass="form-control"  MaxLength="4" OnClientClick ="myApp.showPleaseWait(); return true;"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">ถึงเดือน :</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlMonthGroup_To" runat="server" CssClass="form-control">
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

                                    <label class="col-md-2 control-label">ปี :</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_YearGroup_To" runat="server" CssClass="form-control"  MaxLength="4" OnClientClick ="myApp.showPleaseWait(); return true;"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์รายงาน" OnClick="btnPrint_Click" OnClientClick="return PrintConfirm()?myApp.showPleaseWait(): false;;"/>
                                <asp:Button ID="btnClear" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnClear_Click" OnClientClick = " return ClearConfirm()?myApp.showPleaseWait(): false;;"/>
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
