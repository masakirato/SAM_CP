<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="AccountTypeList.aspx.cs" Inherits="Views_AccountTypeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        var myApp;
        myApp = myApp || (function () {
            //var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 80%;"></div></div></div></div>');
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

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-full-content">
                <div class="row">
                    <div class="col-md-12 top-bar-content-none" style="height: 45px">
                        <span class="title" style="font-size: 18px">Account Type List</span>&nbsp;&nbsp;
                        <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                    </div>
                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:Button ID="btnCreateAccountType" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnCreateAccountType_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                     <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                                <center>ไม่พบข้อมูล</center>
                                            </div>
                                    </asp:Panel>
                                    <asp:GridView ID="gdvAccountType"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataKeyNames=""
                                        ShowFooter="false"
                                        ShowHeaderWhenEmpty="true"
                                        OnRowCancelingEdit="gdvAccountType_RowCancelingEdit"
                                        OnRowDataBound="gdvAccountType_RowDataBound"
                                        OnRowDeleting="gdvAccountType_RowDeleting"
                                        OnRowEditing="gdvAccountType_RowEditing"
                                        OnRowUpdating="gdvAccountType_RowUpdating"
                                        OnRowCommand="gdvAccountType_RowCommand"
                                        CellPadding="0"
                                        ForeColor="#333333"
                                        CssClass="table table-striped table-bordered table-condensed"
                                        AllowPaging="false" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" Visible ="true">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                        <asp:LinkButton ID="lnkBDeleteAccountType" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="lnkBEditUpdateAccountType" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:LinkButton ID="lnkBFooterAddNewAccountType"  OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                               <%--<asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center" >--%>
                                             <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBEditAccountType" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkBEditUpdateAccountType" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>--%>
                                                    <asp:LinkButton ID="lnkBEditCancelAccountType" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <div style="text-align: center;">
                                                        <%--<asp:LinkButton ID="lnkBFooterAddNewAccountType"  OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkBFooterAddNewCancelAccountType" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Account Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccount_ID" runat="server" Text='<%#Eval("Account_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlAccount_ID" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control">
                                                        <asp:ListItem Text="รายรับ" Value="รายรับ"></asp:ListItem>
                                                        <asp:ListItem Text="รายจ่าย" Value="รายจ่าย"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlFooterAccount_ID" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control">
                                                        <asp:ListItem Text="รายรับ" Value="รายรับ"></asp:ListItem>
                                                        <asp:ListItem Text="รายจ่าย" Value="รายจ่าย"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrder" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>

                                                    <div style="margin-bottom:20px">
                                                    <asp:Button ID="btnCreateAccountCode" runat="server" Text="สร้าง" CssClass="btn btn-mini btn-primary" Visible="false" OnClick="btnCreateAccountCode_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                                    </div>
                                                    <div style="margin-bottom:10px">
                                                    <asp:GridView ID="gdvAccountCode"
                                                        runat="server"
                                                        AutoGenerateColumns="False"
                                                        ShowFooter="false" ShowHeaderWhenEmpty="false"
                                                        OnRowCancelingEdit="gdvAccountCode_RowCancelingEdit"
                                                        OnRowDataBound="gdvAccountCode_RowDataBound"
                                                        OnRowDeleting="gdvAccountCode_RowDeleting"
                                                        OnRowEditing="gdvAccountCode_RowEditing"
                                                        OnRowUpdating="gdvAccountCode_RowUpdating"
                                                        OnRowCommand="gdvAccountCode_RowCommand"
                                                        CellPadding="0"
                                                        Visible="false"
                                                        ForeColor="#333333"
                                                        CssClass="table-bordered table-condensed">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                                        <asp:LinkButton ID="lnkBDeleteAccountCode" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                                    </span>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="lnkBEditUpdateAccountCode" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: center;">
                                                                        <asp:LinkButton ID="lnkBFooterAddNewAccountCode" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNewAccountCode" Text="บันทึก"></asp:LinkButton>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBitemEditAccountCode" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%--<asp:LinkButton ID="lnkBEditUpdateAccountCode" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>--%>
                                                                    <asp:LinkButton ID="lnkBEditCancelAccountCode" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: center;">
                                                                        <%--<asp:LinkButton ID="lnkBFooterAddNewAccountCode" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNewAccountCode" Text="บันทึก"></asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="lnkBFooterAddNewCancelAccountCode" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="รหัสบัญชี">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAccountCode_ID" runat="server" Text='<%# Eval("Account_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ชื่อบัญชี">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAccountCode_Name" runat="server" Text='<%# Eval("Account_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEditAccountCode_Name" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control" Text='<%# Eval("Account_Name")%>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtFooterAccountCode_Name" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountType_ID" runat="server" Text='<%# Eval("Account_Type_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Accout Type Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountType_Name" runat="server" Text='<%# Eval("Account_Type")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEditAccountType_Name" runat="server" Text='<%# Eval("Account_Type")%>' Style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNewAccountType_Name" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="#" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkB_SetAccountCode" runat="server" CssClass="btn btn-mini btn-primary" Text="แก้ไขรหัสบัญชี"
                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="EditAccountCode"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


