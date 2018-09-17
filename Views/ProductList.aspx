<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="Views_ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
        <div class="panel panel-default">
            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>


                    <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                        <div class="container-full-content">
                            <div class="row">
                                <div class="col-md-12 top-bar-content-none" style="height: 45px">
                                    <span class="title" style="font-size: 18px">Product List</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                                </div>
                            </div>
                            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">รหัสสินค้า:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSearchProduct_ID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 control-label">ชื่อสินค้า:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSearchProduct_Name" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="ค้นหา" />
                                    <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" />
                                </div>
                                <div class="col-md-12">
                                    <hr />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="ButtonNew" class="btn btn-primary" runat="server" Text="เพิ่ม Product" />
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <asp:GridView ID="grdProduct"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataKeyNames=""
                                        ShowFooter="false"
                                        CellPadding="0" ForeColor="#333333"
                                        CssClass="table table-striped table-bordered table-condensed">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('ยืนยันการลบข้อมูล?')">
                                                        <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" Width="48px" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสสินค้า" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Product_ID" runat="server" CssClass="btn btn-link" Text='<%# Eval("Product_ID") %>' CommandArgument='<%# Eval("Product_ID") %>' CommandName="View" Style="text-decoration: underline"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ราคา">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Price" runat="server" Text='<%# Eval("Price") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vat">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Vat" runat="server" Text='<%# Eval("Vat") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start_Effective_Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Start_Effective_Date" runat="server" Text='<%# Eval("Start_Effective_Date") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End_Effective_Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_End_Effective_Date" runat="server" Text='<%# Eval("End_Effective_Date") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Edit_Product" runat="Server" Text="แก้ไข Product" CssClass="btn btn-mini btn-primary"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pnlForm" Visible="False" runat="server">
                        <div class="container-full-content">
                            <div class="row">
                                <div class="col-md-12 top-bar-content-none" style="height: 45px">
                                    <span class="title" style="font-size: 18px">Product</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                                </div>
                                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">รหัสกลุ่มราคา:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                    <asp:ListItem>1001</asp:ListItem>
                                                    <asp:ListItem>1002</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-2 control-label">ชื่อกลุ่มราคา:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPriceGroupID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">รหัสสินค้า:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                    <asp:ListItem>ราคา 10.00 บาท</asp:ListItem>
                                                    <asp:ListItem>ราคา 20.00 บาท</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-2 control-label">ชื่อสินค้า:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtProduct_Name" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">ราคาตู้แช่:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCP_Meiji_Price" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">ราคา:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">แต้ม:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPoint" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">Exclude Vat:</label>
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="chkExcludeVat" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Vat:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtVat" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">วันที่เริ่มใช้ราคา:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="Textbox1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">วันที่เริ่มใช้ราคา:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="Textbox2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="ButtonOK" class="btn btn-primary" runat="server" Text="ตกลง" />
                                            <asp:Button ID="ButtonCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="ButtonCancel_Click" />
                                        </div>
                                        <div class="col-md-12 text-center">
                                            <br />
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

