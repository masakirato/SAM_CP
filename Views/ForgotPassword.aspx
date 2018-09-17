<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Views_ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <script>
        $(document).ready(function () {
            $("#btnLogin").click(function (event) {
                $(".modal-dialog").css({ position: "absolute", top: event.pageY });
                $('#forgotpassword_modal').modal('show');
            })
        });

    </script>--%>

    <script type="text/javascript">
        function openModal() {
            $('#forgotpassword_modal').modal('show');
        }
    </script>


    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">ลืมรหัสผ่าน</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-cog" style="font-size: 18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>

            <div class="col-md-12 bar-content" style="height: 240px; padding-top: 20px">
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
                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-4">
                            <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="ตกลง" OnClick="btnOK_Click" />
                            <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" />
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="forgotpassword_modal" tabindex="-1" role="dialog" aria-labelledby="edit" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                    </button>

                    <%--<h4 class="modal-title custom_align" id="Heading"></h4>--%>

                    <div class="col-md-12 top-bar-content-none" style="height: 45px">
                        <asp:Label ID="title_user" runat="server" Text="ข้อมูลลูกค้า" style="font-size: 18px"></asp:Label>
                        &nbsp;&nbsp;
                        <span class="glyphicon glyphicon-user" style="font-size: 18px"></span>
                    </div>
                </div>
                <div class="modal-body">
                    <h4>กรุณาตรวจสอบอีเมล์ของคุณ ระบบได้ส่งอีเมล์พร้อมลิงค์ในการรีเซ็ตรหัสผ่านแล้ว</h4>

                    <asp:Button ID="btnLogin" class="btn btn-primary" runat="server" Text="กลับไปหน้า Login" OnClick="btnLogin_Click" />
                </div>
                <div class="modal-footer ">
                </div>

            </div>
        </div>
    </div>
</asp:Content>

