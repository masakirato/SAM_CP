<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="ItemValueList.aspx.cs" Inherits="Views_ItemValueList" %>

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

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">Item Value List</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:Button ID="btnCreateItem" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnCreateItem_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                    <center>ไม่พบข้อมูล</center>
                                </div>
                            </asp:Panel>
                            <asp:GridView ID="gdvItem"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames=""
                                ShowFooter="false"
                                ShowHeaderWhenEmpty="true"
                                OnRowCancelingEdit="gdvItem_RowCancelingEdit"
                                OnRowDataBound="gdvItem_RowDataBound"
                                OnRowDeleting="gdvItem_RowDeleting"
                                OnRowEditing="gdvItem_RowEditing"
                                OnRowUpdating="gdvItem_RowUpdating"
                                OnRowCommand="gdvItem_RowCommand"
                                CellPadding="0"
                                ForeColor="#333333"
                                CssClass="table table-striped table-bordered table-condensed"
                                AllowPaging="false" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" Visible ="true">
                                        <ItemTemplate>
                                            <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <div style="text-align: center;">
                                                <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                       <%--<asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center" >--%>  
                                    <ItemTemplate>
                                            <asp:LinkButton ID="lnkBitemEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <%--<asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkBEditCancel" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <div style="text-align: center;">
                                                 <%--<asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkBFooterAddNewCancel" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order No." HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrder" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            <div style="margin-bottom:10px">
                                            <asp:Button ID="btnCreateItemValue" runat="server" Text="สร้าง" CssClass="btn btn-mini btn-primary" Visible="false" OnClick="btnCreateItemValue_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                            </div>
                                            <asp:GridView ID="gdvItemValue"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                ShowFooter="false" ShowHeaderWhenEmpty="false"
                                                OnRowCancelingEdit="gdvItemValue_RowCancelingEdit"
                                                OnRowDataBound="gdvItemValue_RowDataBound"
                                                OnRowDeleting="gdvItemValue_RowDeleting"
                                                OnRowEditing="gdvItemValue_RowEditing"
                                                OnRowUpdating="gdvItemValue_RowUpdating"
                                                OnRowCommand="gdvItemValue_RowCommand"
                                                CellPadding="0"
                                                Visible="false"
                                                ForeColor="#333333"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                                <asp:LinkButton ID="lnkBDeleteValue" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                            </span>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lnkBEditUpdateValue" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: center;">
                                                                <asp:LinkButton ID="lnkBFooterAddNewValue" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNewItemValue" Text="บันทึก"></asp:LinkButton>
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBitemEditValue" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <%--<asp:LinkButton ID="lnkBEditUpdateValue" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkBEditCancelValue" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: center;">
                                                                <%--<asp:LinkButton ID="lnkBFooterAddNewValue" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNewItemValue" Text="บันทึก"></asp:LinkButton>--%>
                                                                <asp:LinkButton ID="lnkBFooterAddNewCancelValue" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemValueOrder_No" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemValue_ID" runat="server" Text='<%# Eval("Item_Value_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemValue_Name" runat="server" Text='<%# Eval("Item_Value_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtEditItemValue_Name" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control" Text='<%# Eval("Item_Value_Name")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtFooterItemValue_Name" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </ItemTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem_ID" runat="server" Text='<%# Eval("Item_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem_Name" runat="server" Text='<%# Eval("Item_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditItem_Name" runat="server" Text='<%# Eval("Item_Name")%>' Style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewItem_Name" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="#" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkB_SetItem_Value" runat="server" CssClass="btn btn-mini btn-primary" Text="แก้ไข Value"
                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                CommandName="EditItemValue"></asp:LinkButton>
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


