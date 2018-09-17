<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="DeptPayToSP.aspx.cs" Inherits="Views_DeptPayToSP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">ลูกหนี้</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="DropDownListOrderingStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="สมพงษ์"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">

                        <asp:GridView ID="GridViewOrdering_1"
                            runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames=""
                            ShowFooter="false"
                            CellPadding="0"
                            ForeColor="#333333"
                            ondatabound="GridViewOrdering_1_OnDataBound"
                            CssClass="table table-striped table-bordered table-condensed">
                            <Columns>
                                <asp:TemplateField HeaderText="รหัสลูกหนี้" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonDebt_ID" runat="server" CssClass="btn btn-link" Text='<%# Eval("Debt_ID") %>' CommandArgument='<%# Eval("Debt_ID") %>' CommandName="View" style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                    </ItemTemplate>
                                    <itemstyle Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อลูกหนี้้">
                                    <ItemTemplate>
                                        <asp:Label id="Label_SP_ID" runat="server" Text='<%# Eval("SP_ID") %>' style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่ค้างชำระ">
                                    <ItemTemplate>
                                        <asp:Label id="Label_Debt_Date" runat="server" Text='<%# Eval("Debt_Date") %>' style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ยอดเงิน" itemstyle-horizontalalign="right">
                                    <ItemTemplate>
                                        <asp:Label id="Label_Debt_Amount" runat="server" Text='<%# Eval("Debt_Amount") %>' style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ยอดเงินที่ชำระแล้ว" itemstyle-horizontalalign="right">
                                    <ItemTemplate>
                                        <asp:Label id="Label_Total_Payment_Amount" runat="server" Text='<%# Eval("Total_Payment_Amount") %>' style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ยอดค้างชำระ" itemstyle-horizontalalign="right">
                                    <ItemTemplate>
                                        <asp:Label id="Label_Balance_Outstanding_Amount" runat="server" Text='<%# Eval("Balance_Outstanding_Amount") %>' style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

