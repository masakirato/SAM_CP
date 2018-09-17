<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            /*$('.btn').on('click', function () {
                var $this = $(this);
                $this.button('loading');
                setTimeout(function () {
                    $this.button('reset');
                }, 3000);
            });*/

            $('#MainContent_txtUser').focus();
        });

    </script>

    <div id="login-block">
        <div class="col-md-3 col-md-offset-5">
            <div class="icon-user">
                <asp:textbox id="txtUser" runat="server" class="form-control textBlue" placeholder="ชื่อผู้ใช้งาน" validationgroup="login" AutoCompleteType="disabled" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server"
                    ErrorMessage="โปรดระบุชื่อผู้ใช้งาน"
                    ControlToValidate="txtUser"
                    Display="Dynamic"
                    CssClass="ErrorControl"
                    validationgroup="login" />
            </div>
        </div>
        <div class="col-md-3">
            <div class="icon-pwd">
                <asp:textbox id="txtPassword" type="password" class="form-control textBlue"  placeholder="รหัสผ่าน" runat="server" validationgroup="login" AutoCompleteType="disabled" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server"
                    ErrorMessage="โปรดระบุรหัสผ่าน"
                    ControlToValidate="txtPassword"
                    Display="Dynamic"
                    CssClass="ErrorControl"
                    validationgroup="login" />
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="col-md-1" style="right: 0px; padding-left: 10px;">
                    <asp:Button runat="server" ID="btnLogin" Text="ตกลง" CssClass="btn btn-danger" OnClick="btnLogin_Click" />
                    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                        <ProgressTemplate>
                            <div class="text-center">
                                <img alt="progress" src="Images/slide/ajax-loader-grey_round.gif" width="30" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <div class="col-md-8 col-md-offset-5 text-label-bold"
                    style="margin-top: 10px" id="divError" runat="server" visible="false">
                    <asp:Label ID="lblErrorMsg" runat="server" CssClass="ErrorControl" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </div>
    <div class="row">
        <div class="col-md-9">
        </div>
        <%-- <div class="col-md-3">
            <asp:CheckBox ID="CheckBoxRememberMe" runat="server" class="pull-right" Text="remember me" />
        </div>--%>
        <div class="col-md-3">
            <div style="margin-top:10px">
            <asp:LinkButton ID="LinkButtonForgotPassword" runat="server" class="pull-right text-label-bold" 
                OnClick="LinkButtonForgotPassword_Click">ลืมรหัสผ่าน?</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

