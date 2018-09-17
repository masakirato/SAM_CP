<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateRevenue_Expense.aspx.cs" Inherits="Views_CreateRevenue_Expense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">ข้อมูลการผ่อนสินค้า</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">วันที่บันทึก:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtOderingNumber" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ถึง:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox1" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <h4>รายรับ</h4>
                    </div>
                    <div class="col-md-12">
                        <hr />
                    </div>

                    <asp:GridView ID="GridViewInstallation"
                        runat="server"
                        AutoGenerateColumns="False"
                        DataKeyNames=""
                        ShowFooter="true"
                        OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowDataBound="GridView1_RowDataBound"
                        OnRowDeleting="GridView1_RowDeleting"
                        OnRowEditing="GridView1_RowEditing"
                        OnRowUpdating="GridView1_RowUpdating"
                        OnRowCommand="GridView1_RowCommand"
                        CellPadding="0"
                        ForeColor="#333333"
                        CssClass="table table-striped table-bordered table-condensed">
                        <Columns>
                            <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                <ItemTemplate>
                                    <span onclick="return confirm('ยืนยันการลบข้อมูล?')">
                                        <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                    </span>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-mini btn-primary" runat="server" CommandName="AddNew">บันทึก</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-info" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-mini btn-default" runat="server" CommandName="AddNew">ยกเลิก</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ประเภทบัญชี">
                                <EditItemTemplate>
                                    <asp:dropdownlist id="txtAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>บัญชีประเภท 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:dropdownlist id="LabelAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>บัญชีประเภท 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:dropdownlist ID="txtNewAgentName" runat="server">
                                        <asp:ListItem>บัญชีประเภท 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสบัญชี">
                                <EditItemTemplate>
                                    <asp:dropdownlist id="txtAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>A00001</asp:ListItem>
                                    </asp:dropdownlist>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:dropdownlist id="LabelAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>A00001</asp:ListItem>
                                    </asp:dropdownlist>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:dropdownlist ID="txtNewAgentName" runat="server">
                                        <asp:ListItem>A00001</asp:ListItem>
                                    </asp:dropdownlist>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชื่อบัญชี">
                                <EditItemTemplate>
                                    <asp:dropdownlist id="txtAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>บัญชี 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:dropdownlist id="LabelAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>บัญชี 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:dropdownlist ID="txtNewAgentName" runat="server">
                                        <asp:ListItem>บัญชี 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="จำนวนเงิน" itemstyle-horizontalalign="Right">
                                <EditItemTemplate>
                                    <asp:TextBox id="txtInstallation_Amount" runat="server" Text='<%# Eval("Amount") %>' Width=" 64px" style="color: blue;"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label id="LabelInstallation_Amount" runat="server" Text='<%# Eval("Amount") %>' Width=" 64px" style="color: blue;"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewInstallation_Amount" runat="server" Width="64px"></asp:TextBox>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมายเหตุ">
                                <EditItemTemplate>
                                    <asp:TextBox id="txtDescription" runat="server" Text='<%# Eval("Description") %>' style="color: blue;"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label id="LabelDescription" runat="server" Text='<%# Eval("Description") %>' style="color: blue;"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewDescription" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div class="col-md-12">
                        <h4>รายจ่าย</h4>
                    </div>
                    <div class="col-md-12">
                        <hr />
                    </div>

                    <asp:GridView ID="GridView1"
                        runat="server"
                        AutoGenerateColumns="False"
                        DataKeyNames=""
                        ShowFooter="true"
                        OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowDataBound="GridView1_RowDataBound"
                        OnRowDeleting="GridView1_RowDeleting"
                        OnRowEditing="GridView1_RowEditing"
                        OnRowUpdating="GridView1_RowUpdating"
                        OnRowCommand="GridView1_RowCommand"
                        CellPadding="0"
                        ForeColor="#333333"
                        CssClass="table table-striped table-bordered table-condensed">
                        <Columns>
                            <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                <ItemTemplate>
                                    <span onclick="return confirm('ยืนยันการลบข้อมูล?')">
                                        <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                    </span>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-mini btn-primary" runat="server" CommandName="AddNew">บันทึก</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-info" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-mini btn-default" runat="server" CommandName="AddNew">ยกเลิก</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ประเภทบัญชี">
                                <EditItemTemplate>
                                    <asp:dropdownlist id="txtAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>บัญชีประเภท 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:dropdownlist id="LabelAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>บัญชีประเภท 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:dropdownlist ID="txtNewAgentName" runat="server">
                                        <asp:ListItem>บัญชีประเภท 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสบัญชี">
                                <EditItemTemplate>
                                    <asp:dropdownlist id="txtAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>A00001</asp:ListItem>
                                    </asp:dropdownlist>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:dropdownlist id="LabelAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>A00001</asp:ListItem>
                                    </asp:dropdownlist>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:dropdownlist ID="txtNewAgentName" runat="server">
                                        <asp:ListItem>A00001</asp:ListItem>
                                    </asp:dropdownlist>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชื่อบัญชี">
                                <EditItemTemplate>
                                    <asp:dropdownlist id="txtAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>บัญชี 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:dropdownlist id="LabelAgentName" runat="server" style="color: blue;">
                                        <asp:ListItem>บัญชี 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:dropdownlist ID="txtNewAgentName" runat="server">
                                        <asp:ListItem>บัญชี 1</asp:ListItem>
                                    </asp:dropdownlist>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="จำนวนเงิน" itemstyle-horizontalalign="Right">
                                <EditItemTemplate>
                                    <asp:TextBox id="txtInstallation_Amount" runat="server" Text='<%# Eval("Amount") %>' Width=" 64px" style="color: blue;"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label id="LabelInstallation_Amount" runat="server" Text='<%# Eval("Amount") %>' Width=" 64px" style="color: blue;"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewInstallation_Amount" runat="server" Width="64px"></asp:TextBox>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมายเหตุ">
                                <EditItemTemplate>
                                    <asp:TextBox id="txtDescription" runat="server" Text='<%# Eval("Description") %>' style="color: blue;"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label id="LabelDescription" runat="server" Text='<%# Eval("Description") %>' style="color: blue;"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNewDescription" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

