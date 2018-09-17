<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateUserCPMeiji.aspx.cs" Inherits="Views_CreateUserCPMeiji" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">CP Meiji User</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-user" style="font-size: 18px"></span>
                <asp:Label ID="LabelPageHeader" runat="server" Text="สร้างข้อมูล User CP Meiji"></asp:Label>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="col-md-12">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-2 control-label">รหัสพนักงาน:</label>
                            <div class="col-md-4">
                                <asp:textbox id="TextboxUserID" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">คำนำหน้าชื่อ:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="DropDownListTitle_ID" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="นาย"></asp:ListItem>
                                    <asp:ListItem Text="นางสาว"></asp:ListItem>
                                    <asp:ListItem Text="นาง"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtFirst_Name" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                            <label class="col-md-2 control-label">นามสกุล:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtLast_Name" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                ชื่อพนักงาน<br />
                                (ภาษาอังกฤษ):</label>
                            <div class="col-md-4">
                                <asp:textbox id="TextboxFirst_Name_Eng" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                            <label class="col-md-2 control-label">
                                นามสกุล<br />
                                (ภาษาอังกฤษ):</label>
                            <div class="col-md-4">
                                <asp:textbox id="TextboxLast_Name_Eng" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtHome_Phone_No" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                            <label class="col-md-2 control-label">เบอร์มือถือ:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtMobile" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">ตำแหน่ง:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtPosition" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                            <label class="col-md-2 control-label">ฝ่าย:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtSection" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">แผนก:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtDivision" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                            <label class="col-md-2 control-label">ขึ้นตรงกับ:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtManager" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">User group:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="DropDownUserGroup" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Admin"></asp:ListItem>
                                    <asp:ListItem Text="Standard"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label class="col-md-2 control-label">User Role:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="DropDownListRole" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Admin"></asp:ListItem>
                                    <asp:ListItem Text="Standard"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">User Name:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtUser_Name" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                            <label class="col-md-2 control-label">Password:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtPassword" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">อีเมล์:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtEmail" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                            <label class="col-md-2 control-label">สถานะการขออนุมัติ:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtApproval" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">สถานะ:</label>
                            <div class="col-md-4">
                                <asp:textbox id="txtStatus" runat="server" CssClass="form-control"></asp:textbox>
                            </div>
                            <label class="col-md-2 control-label">Show Dashboard:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="DropDownListShowDashboard" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="แสดง Dashboard"></asp:ListItem>
                                    <asp:ListItem Text="ไม่แสดง Dashboard"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>




                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="แก้ไข" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnCancel_Click" />
                        </div>

                        <div class="col-md-12">
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

