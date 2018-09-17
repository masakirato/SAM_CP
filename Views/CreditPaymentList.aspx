<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreditPaymentList.aspx.cs" Inherits="Views_CreditPaymentList" %>

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
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-info" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField headerText="ชื่อลูกค้า">
                                        <ItemTemplate>
                                            <asp:GridView ID="GridViewCustomer"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                ShowFooter="false"
                                                OnRowCancelingEdit="GridViewCustomer_RowCancelingEdit"
                                                OnRowDataBound="GridViewCustomer_RowDataBound"
                                                OnRowDeleting="GridViewCustomer_RowDeleting"
                                                OnRowEditing="GridViewCustomer_RowEditing"
                                                OnRowUpdating="GridViewCustomer_RowUpdating"
                                                OnRowCommand="GridViewCustomer_RowCommand"
                                                CellPadding="0"
                                                ForeColor="#333333">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <span onclick="return confirm('ยืนยันการลบข้อมูล?')">
                                                                <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-link" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-link" Text="แก้ไข"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CssClass="btn btn-link" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-link" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-link" runat="server" CommandName="AddNew">สร้าง</asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่ชำระเงิน">
                                                        <EditItemTemplate>
                                                            <asp:TextBox id="txtPayment_Date" runat="server" Text='<%# Eval("Payment_Date") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label id="lblPayment_Date" runat="server" Text='<%# Eval("Payment_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงินชำระ">
                                                        <EditItemTemplate>
                                                            <asp:TextBox id="txtPayment_Amount" runat="server" Text='<%# Eval("Payment_Amount")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label id="lblPayment_Amount" runat="server" Text='<%# Eval("Payment_Amount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วิธีการชำระ">
                                                        <EditItemTemplate>
                                                            <asp:TextBox id="txtPayment_Method" runat="server" Text='<%# Eval("Payment_Method")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label id="lblPayment_Method" runat="server" Text='<%# Eval("Payment_Method")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ธนาคาร">
                                                        <EditItemTemplate>
                                                            <asp:TextBox id="txtBank" runat="server" Text='<%# Eval("Bank")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label id="lblBank" runat="server" Text='<%# Eval("Bank")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขที่">
                                                        <EditItemTemplate>
                                                            <asp:TextBox id="txtEditCheque_No" runat="server" Text='<%# Eval("Cheque_No")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label id="lblItemCheque_No" runat="server" Text='<%# Eval("Cheque_No")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtRole_Name" runat="server"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่">
                                                        <EditItemTemplate>
                                                            <asp:TextBox id="txtEditDate" runat="server" Text='<%# Eval("Date")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label id="txtItemDate" runat="server" Text='<%# Eval("Date")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รับเงินจากเช็ค">
                                                        <ItemTemplate>
                                                            <asp:checkbox ID="chkItemClearing_Cheque" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("Customer_Name") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="วันที่ค้างชำระ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit_Date" runat="server" Text='<%#Eval("Credit_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงินค้างชำระ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit_Amount" runat="server" Text='<%#Eval("Credit_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงินชำระรวม">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal_Payment_Amount" runat="server" Text='<%#Eval("Total_Payment_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงินค้างชำระคงเหลือ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance_Outstanding_Amount" runat="server" Text='<%#Eval("Balance_Outstanding_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สถานะ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="btnCustomerName" runat="server" Text="จ่ายหนี้เครดิต" CommandName="ShowCredit" CommandArgument='<%# Container.DataItemIndex%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

