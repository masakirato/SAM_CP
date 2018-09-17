<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" uiculture="th" culture="th-TH" %>

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
                        <span class="title" style="font-size: 18px">ลืมรหัสผ่าน</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-cog" style="font-size: 18px"></span><!--<img src="Images/help-contents.png" />-->
                    </div>
                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                        <div class="col-md-12">
                            <span style="font-size: 16px">เพื่อรีเซ็ตรหัสผ่าน กรุณากรอกชื่อผู้ใช้งาน</span>
                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-md-2 control-label">ชื่อผู้ใช้งาน:</label>
                                <div class="col-md-4">
                                    <asp:textbox id="txtUserName" runat="server" CssClass="form-control"></asp:textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">อีเมล์:</label>
                                <div class="col-md-4">
                                    <asp:textbox id="txtEmail" runat="server" CssClass="form-control"></asp:textbox>
                                </div>



                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="ตกลง" OnClick="btnOK_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>

                            </div>
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

