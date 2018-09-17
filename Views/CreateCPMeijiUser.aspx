<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateCPMeijiUser.aspx.cs" Inherits="Views_CreateAgentUser" %>




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
    </script>

    
    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
        <div class="panel panel-default">
             <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ค้นหาพนักงาน ซีพี-เมจิ</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-user" style="font-size: 18px"></span>
                        </div>

                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <%--<asp:textbox id="txtSearchEmpName" runat="server" CssClass="form-control"></asp:textbox>--%>
                                        <asp:DropDownList ID="ddlSearchFirst_Name" runat="server" CssClass="form-control" DataTextField="FullName" DataValueField="User_ID">
                                        </asp:DropDownList>
                                    </div>

                                    <label class="col-md-2 control-label">ตำแหน่ง:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchPosition" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สิทธิผู้ใช้งาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchRole" runat="server" CssClass="form-control" DataTextField="Role_Name" DataValueField="Role_ID">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ภาค:</label>
                                    <div class="col-md-4">

                                        <asp:DropDownList ID="ddlSearchState" runat="server"
                                            CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สถานะ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>In active</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnAddNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnAddNew_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>

                        </div>
                        <asp:HiddenField ID="hdfUser_ID" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                            <div class="container-fluid panel-container" style="overflow: auto">
                                <%-- <div class="col-md-12">--%>
                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่พบข้อมูล</center>
                                    </div>
                                </asp:Panel>


                                <asp:GridView ID="GridViewUser"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    OnRowCommand="GridViewUser_RowCommand" OnRowDeleting="GridViewUser_RowDeleting"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    HeaderStyle-HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="false" AllowPaging="true" PageSize="10" OnDataBound="GridViewUser_DataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ" CommandArgument='<%# Eval("User_ID") %>' CommandName="Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ชื่อพนักงาน" HeaderStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBFirst_Name" runat="server" CssClass="btn btn-link" Text='<%# Eval("FullName") %>'
                                                    CommandArgument='<%# Eval("User_ID") %>' CommandName="View" Style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ตำแหน่ง">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Position" runat="server" Text='<%# Eval("Position") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สิทธิผู้ใช้งาน">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Role" runat="server" Text='<%# Eval("Role_ID") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ภาค">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_State" runat="server" Style="color: blue" Text='<%# Eval("Region") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="สถานะ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เบอร์โทรศัพท์">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Phone" runat="server" Text='<%# Eval("Home_Phone_No") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <PagerTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 70%">
                                                    <asp:Label ID="MessageLabel"
                                                        ForeColor="Blue"
                                                        Text="เลือกหน้า :"
                                                        runat="server" />
                                                    <asp:DropDownList ID="PageDropDownList"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                                        onchange="myApp.showPleaseWait();"
                                                        runat="server" />
                                                </td>

                                                <td style="width: 70%; text-align: right">
                                                    <asp:Label ID="CurrentPageLabel"
                                                        ForeColor="Black"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </PagerTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="pnlCPMeiji" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">CP Meiji User</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-user" style="font-size: 18px"></span>
                            <asp:Label ID="LabelPageHeader" runat="server" Text="สร้างข้อมูล User CP Meiji"></asp:Label>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="container-fluid panel-container" style="overflow: auto">

                                <div class="col-md-12">
                                    <div class="form-horizontal">
                                        <div class="col-md-12">
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                                DisplayMode="BulletList"
                                                ValidationGroup="CPValidation"
                                                ForeColor="red"
                                                Font-Bold="true" />
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">รหัสพนักงาน:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCP_User_ID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">คำนำหน้าชื่อ:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlCP_TitleID"
                                                    runat="server"
                                                    ControlToValidate="ddlCP_TitleID"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุคำนำหน้านาม"
                                                    ValidationGroup="CPValidation" InitialValue="">
                                                </asp:RequiredFieldValidator>

                                                <asp:DropDownList ID="ddlCP_TitleID" runat="server" CssClass="form-control"
                                                    DataTextField="VALUE" DataValueField="KEY"
                                                    Style="border-left: 4px solid #ed1c24;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtCP_FirstName"
                                                    runat="server"
                                                    ControlToValidate="txtCP_FirstName"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุชื่อ"
                                                    ValidationGroup="CPValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCP_FirstName" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">นามสกุล:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtCP_LastName"
                                                    runat="server"
                                                    ControlToValidate="txtCP_LastName"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุนามสกุล"
                                                    ValidationGroup="CPValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCP_LastName" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">
                                                ชื่อพนักงาน<br />
                                                (ภาษาอังกฤษ):</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtCP_First_Name_Eng"
                                                    runat="server"
                                                    ControlToValidate="txtCP_First_Name_Eng"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุชื่อ (ภาษาอังกฤษ)"
                                                    ValidationGroup="CPValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCP_First_Name_Eng" runat="server" CssClass="form-control"
                                                    Style="border-left: 4px solid #ed1c24;" AutoPostBack="true" OnTextChanged="txtCP_Last_Name_Eng_TextChanged"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">
                                                นามสกุล<br />
                                                (ภาษาอังกฤษ):</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtCP_Last_Name_Eng"
                                                    runat="server"
                                                    ControlToValidate="txtCP_Last_Name_Eng"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุนามสกุล (ภาษาอังกฤษ)"
                                                    ValidationGroup="CPValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCP_Last_Name_Eng" runat="server" CssClass="form-control"
                                                    Style="border-left: 4px solid #ed1c24;" AutoPostBack="true" OnTextChanged="txtCP_Last_Name_Eng_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCP_Phone" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">เบอร์มือถือ:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCP_Mobile" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">ตำแหน่ง:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlCP_Position" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-2 control-label">ภูมิภาค:</label>
                                            <div class="col-md-4">
                                                <asp:CheckBoxList runat="server" ID="ddlLocation_Region" CssClass="cb" RepeatLayout="Flow" RepeatDirection="Vertical" DataTextField="VALUE" DataValueField="KEY">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">สิทธิผู้ใช้งาน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlCP_UserRole"
                                                    runat="server"
                                                    ControlToValidate="ddlCP_UserRole"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุสิทธิผู้ใช้งาน"
                                                    ValidationGroup="CPValidation" InitialValue="">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCP_UserRole" runat="server" CssClass="form-control"
                                                    DataTextField="Role_Name" DataValueField="Role_ID" Style="border-left: 4px solid #ed1c24;">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">ชื่อผู้ใช้งาน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtCP_UserName"
                                                    runat="server"
                                                    ControlToValidate="txtCP_UserName"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุชื่อผู้ใช้งาน"
                                                    ValidationGroup="CPValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCP_UserName" runat="server" CssClass="form-control" AutoCompleteType="disabled"
                                                    Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">รหัสผ่าน:</label>
                                            <div class="col-md-4">
                                                <%--  <asp:RequiredFieldValidator ID="rfvtxtCP_Password"
                                                runat="server"
                                                ControlToValidate="txtCP_Password"
                                                Display="None"
                                                ErrorMessage="ต้องระบุรหัสผ่าน"
                                                ValidationGroup="CPValidation">
                                            </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtCP_Password" runat="server" CssClass="form-control" AutoCompleteType="disabled"
                                                    Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                                <asp:HiddenField ID="hdfPasswrod" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">อีเมล์:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCP_Email" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <%-- <label class="col-md-2 control-label">สถานะการขออนุมัติ:</label>
                                        <div class="col-md-4">
                                           
                                            <asp:RequiredFieldValidator ID="rfvddlCP_Approval"
                                                runat="server"
                                                ControlToValidate="ddlCP_Approval"
                                                Display="None"
                                                ErrorMessage="ต้องระบุสถานะการขออนุมัติ"
                                                ValidationGroup="CPValidation" InitialValue="==ระบุ==">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCP_Approval" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;">
                                                <asp:ListItem>==ระบุ==</asp:ListItem>
                                                <asp:ListItem>อนุมัติ</asp:ListItem>
                                                <asp:ListItem>รอการอนุมัติ</asp:ListItem>
                                                <asp:ListItem>ไม่อนุมัติ</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">สถานะ:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlCP_Status"
                                                    runat="server"
                                                    ControlToValidate="ddlCP_Status"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุสถานะผู้ใช้งาน"
                                                    ValidationGroup="CPValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCP_Status" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                                    <asp:ListItem>==ระบุ==</asp:ListItem>
                                                    <asp:ListItem>Active</asp:ListItem>
                                                    <asp:ListItem>In active</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-2 control-label">แสดงกราฟ:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlCP_ShowDashboard" runat="server" CssClass="form-control">
                                                    <asp:ListItem>==ระบุ==</asp:ListItem>
                                                    <asp:ListItem>แสดง</asp:ListItem>
                                                    <asp:ListItem>ไม่แสดง</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                        <label class="col-md-2 control-label">ข้อมูลรูท:</label>
                                        <div class="col-md-4">
                                            <asp:textbox id="txtRoute" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                        </div>
                                    </div>--%>
                                        <div class="col-md-12 text-center">
                                            <asp:HiddenField ID="btnSaveMode" runat="server" />
                                            <asp:Button ID="btnCP_Save" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                            <asp:Button ID="btnCP_Cancel" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</div>
        </div>
</asp:Content>
           