<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="changepwd.aspx.cs" Inherits="changepwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>

            <div class="container-full-content">
                <div class="row">
                    <div class="col-md-12 top-bar-content-none" style="height: 45px">
                        <span class="title" style="font-size: 18px">เปลี่ยนรหัสผ่าน</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        <!--<img src="Images/help-contents.png" />-->
                    </div>
                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                        <asp:HiddenField id="txtUserName" runat="server" />



                        <div class="form-horizontal">
                            <div class="col-md-12">
                                <%--  <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    DisplayMode="BulletList"
                                    ValidationGroup="ValidatePassword"
                                    forecolor="red"
                                    font-bold="true" />--%>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <h4>กรุณากรอกรหัสผ่านใหม่และยืนยันรหัสผ่าน</h4>
                                        <h5>1. รหัสผ่านควรมีอย่างน้อย 8 ตัวอักษร</h5>
                                        <h5>2. รหัสผ่านควรประกอบด้วยตัวเลขและตัวอักษร</h5>
                                    </div>
                                </div>
                                <%--<div class="form-group">
                                <label class="col-md-2 control-label">ชื่อผู้ใช้:</label>
                                <div class="col-md-4">
                                    <asp:textbox id="txtUserName" runat="server" CssClass="form-control"></asp:textbox>
                                   
                                </div>
                            </div>--%>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">รหัสผ่านใหม่:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtNewPassword" runat="server" CssClass="form-control" textmode="Password" placeholder="รหัสผ่าน 8 หลัก"></asp:textbox>
                                        <%-- <asp:RegularExpressionValidator ID="rfvtxtNewPassword"
                                            runat="server" ErrorMessage="รหัสผ่านควรมีอย่างน้อย 8 ตัวอักษร และ ประกอบด้วยตัวเลขและตัวอักษร"
                                            ValidationGroup="ValidatePassword" ControlToValidate=" txtNewPassword"
                                            ForeColor="Red"
                                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$">
                                        </asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ยืนยันรหัสผ่าน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtConfirmPassword" runat="server" CssClass="form-control" textmode="Password" placeholder="รหัสผ่าน 8 หลัก"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="เปลี่ยนรหัสผ่าน" OnClick="btnOK_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        <%--<asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnCancel_Click" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

