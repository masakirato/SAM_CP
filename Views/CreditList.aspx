<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreditList.aspx.cs" Inherits="Views_CreditList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">Cretdit List</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:Button ID="ButtonNew" class="btn btn-primary" runat="server" Text="สร้าง" />
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="GridViewCredit"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames=""
                                ShowFooter="false"
                                OnRowCancelingEdit="GridViewCredit_RowCancelingEdit"
                                OnRowDataBound="GridViewCredit_RowDataBound"
                                OnRowDeleting="GridViewCredit_RowDeleting"
                                OnRowEditing="GridViewCredit_RowEditing"
                                OnRowUpdating="GridViewCredit_RowUpdating"
                                OnRowCommand="GridViewCredit_RowCommand"
                                CellPadding="0" ForeColor="#333333"
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
                                    <asp:TemplateField HeaderText="ชื่อลูกค้า">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtCustomerName" runat="server" width="100px" Text='<%# Eval("CustomerName") %>' style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelCustomerName" runat="server" width="100px" Text='<%# Eval("CustomerName") %>' style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCustomerName" runat="server" width="100px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่ค้างชำระ">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtCredit_Date" runat="server" Text='<%# Eval("Credit_Date") %>' style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelCredit_Date" runat="server" Text='<%# Eval("Credit_Date") %>' style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewInstallation_Type" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงินค้างชำระ">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtCredit_Amount" runat="server" Text='<%# Eval("Credit_Amount") %>' style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelCredit_Amount" runat="server" Text='<%# Eval("Credit_Amount") %>' style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewDescription" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สถานะ">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtStatus" runat="server" Text='<%# Eval("Status") %>' Width="64px" style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelStatus" runat="server" Text='<%# Eval("Status") %>' Width="64px" style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewInstallation_Amount" runat="server" Width="64px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">
                        <br />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="จ่ายหนี้เครดิต" onclick="btnCreditPayment_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>




</asp:Content>

