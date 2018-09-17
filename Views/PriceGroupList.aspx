<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="PriceGroupList.aspx.cs"
    Inherits="Views_PriceGroupList" UICulture="th" Culture="th-TH" %>

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
            <asp:Panel ID="pnlPriceGroup" Visible="True" runat="server">

                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">กำหนดกลุ่มราคา</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>

                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อกลุ่มราคา:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPrice_Group_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ประเภทกลุ่มราคา:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlPrice_Group_Type" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPrice_Group_Type_SelectedIndexChanged">
                                            <asp:ListItem Text="เอเยนต์" Value="0">  </asp:ListItem>
                                            <asp:ListItem Text="สาว" Value="1">  </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnOK_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnAddNewPriceGroup" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="ShowPriceGroupFooter_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        <asp:Button ID="btnAddAssign" class="btn btn-primary" runat="server" Text="กำหนดกลุ่มราคา" OnClick="btnAddAssign_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                                <center>ไม่พบข้อมูล</center>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="GridViewPrice_Group"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames=""
                                            ShowFooter="false"
                                            OnRowCancelingEdit="GridViewPrice_Group_RowCancelingEdit"
                                            OnRowDataBound="GridViewPrice_Group_RowDataBound"
                                            OnRowDeleting="GridViewPrice_Group_RowDeleting"
                                            OnRowEditing="GridViewPrice_Group_RowEditing"
                                            OnRowUpdating="GridViewPrice_Group_RowUpdating"
                                            OnRowCommand="GridViewPrice_Group_RowCommand"
                                            CellPadding="0" ForeColor="#333333"
                                            CssClass="table table-striped table-bordered table-condensed"
                                            AllowPaging="true" PageSize="10" OnDataBound="GridViewPrice_DataBound">
                                            <Columns>                                                
                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                            <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Eval("Price_Group_ID") %>' CommandName="Delete"></asp:LinkButton>
                                                        </span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Eval("Price_Group_ID") %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                                <%--<asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">--%>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBitemEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkBEditCancel" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;">
                                                            <%--<asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Eval("Price_Group_ID") %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkBFooterAddNewCancel" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ลำดับ" HeaderStyle-Width="80px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPrice_Group_ID" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice_Group_ID" runat="server" Text='<%# Eval("Price_Group_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อกลุ่มราคา">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditPrice_Group_Name" runat="server" Text='<%# Eval("Price_Group_Name")%>' Style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPrice_Group_Name" runat="server" Text='<%# Eval("Price_Group_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewPrice_Group_Name" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ประเภทกลุ่มราคา">
                                                    <%--<EditItemTemplate>
                                                        <asp:TextBox id="txtEditGroupPriceType" runat="server" Text='<%# Eval("Price_Group_Type")%>' style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupPriceType" runat="server" Text='<%# Eval("Price_Group_Type")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="txtNewGroupPriceType" runat="server" Text='<%# ((bool)ddlPrice_Group_Type.Enabled == true) ? "เอเยนต์" : "สาว"  %>'></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ราคามาตรฐาน" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkEditStandard" runat="server" Checked='<%# Eval("StandardPrice")%>' AutoPostBack="true" OnCheckedChanged="chkItemStandard_CheckedChanged" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkItemStandard" runat="server" Checked='<%# Eval("StandardPrice")%>' Enabled="false" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:CheckBox ID="chkFooterStandard" runat="server" AutoPostBack="true" OnCheckedChanged="chkFooterStandard_CheckedChanged" />
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkB_Product_List" runat="server" CssClass="btn btn-mini btn-primary" Text="แก้ไข Product List" CommandArgument='<%# Eval("Price_Group_ID") %>' CommandName="EditProductList"></asp:LinkButton>
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
            </asp:Panel>

            <asp:Panel ID="pnlGrid" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายการสินค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>

                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">รหัสกลุ่มราคา:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchPriceGroupID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ชื่อกลุ่มราคา:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchPriceGroupName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <hr />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="ButtonNew" class="btn btn-primary" runat="server" Text="เพิ่มรายการสินค้า" OnClick="ButtonNew_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">

                                    <asp:GridView ID="grdProduct_Agent"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataKeyNames=""
                                        ShowFooter="false"
                                        CellPadding="0" ForeColor="#333333"
                                        OnRowCommand="grdProduct_RowCommand"
                                        CssClass="table table-striped table-bordered table-condensed">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible ="true">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('คุณต้องการลบข้อมูล?')">
                                                        <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" Width="48px" CommandArgument='<%# Eval("Product_List_ID") %>' CommandName="_Delete"></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสสินค้า" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkB_Product_ID" runat="server" CssClass="btn btn-link" Text='<%# Eval("Product_ID") %>' CommandArgument='<%# Eval("Product_List_ID") %>' CommandName="View" Style="text-decoration: underline"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ขนาด">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Size" runat="server" Text='<%# Eval("Size") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="ราคาขายเอเยนต์" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Price_Agent" runat="server" Text='<%# Eval("Agent_Price") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Vat(%)" itemstyle-horizontalalign="right">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_Vat" runat="server" Text='<%# Eval("Vat") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="วันที่เริ่มใช้ราคา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Start_Effective_Date" runat="server" Text='<%# Eval("Start_Effective_Date", "{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>

                                                </ItemTemplate>

                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="วันที่สิ้นสุดใช้ราคา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_End_Effective_Date" runat="server" Text='<%# Eval("End_Effective_Date", "{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkB_Edit_Product" runat="Server" Text="แก้ไข Product" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Eval("Product_List_ID") %>' CommandName="EditProductList"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                    <asp:GridView ID="grdProduct_SP"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataKeyNames=""
                                        ShowFooter="false"
                                        CellPadding="0" ForeColor="#333333"
                                        OnRowCommand="grdProduct_RowCommand"
                                        CssClass="table table-striped table-bordered table-condensed">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('คุณต้องการลบข้อมูล?')">
                                                        <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" Width="48px" CommandArgument='<%# Eval("Product_List_ID") %>' CommandName="_Delete"></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสสินค้า" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkB_Product_ID" runat="server" CssClass="btn btn-link" Text='<%# Eval("Product_ID") %>'
                                                        CommandArgument='<%# Eval("Product_List_ID") %>' CommandName="View" Style="text-decoration: underline; font-weight: bold"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ขนาด">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Size" runat="server" Text='<%# Eval("Size") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ราคาตู้แช่" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Price_CP_Meiji" runat="server" Text='<%# Eval("CP_Meiji_Price") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ราคาทุนสาว" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Price_SP" runat="server" Text='<%# Eval("SP_Price") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="แต้ม">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Point" runat="server" Text='<%# Eval("Point") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่เริ่มใช้ราคา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Start_Effective_Date" runat="server" Text='<%# Eval("Start_Effective_Date", "{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่สิ้นสุดใช้ราคา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_End_Effective_Date" runat="server" Text='<%# Eval("End_Effective_Date", "{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkB_Edit_Product" runat="Server" Text="แก้ไข Product" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Eval("Product_List_ID") %>' CommandName="EditProductList"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="col-md-12 text-center">
                                <asp:Button ID="ButtonBackToPriceGroup" class="btn btn-primary" runat="server" Text="กลับหน้ากลุ่มราคา" OnClick="ButtonBackToPriceGroup_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>
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

                            <asp:Panel ID="pnlAgent" runat="server" Visible="false">


                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <asp:ValidationSummary ID="ProductValidationSummary"
                                            runat="server"
                                            Font-Bold="true"
                                            ForeColor="Red"
                                            ValidationGroup="AgentValidation"
                                            DisplayMode="BulletList" />
                                        <label class="col-md-2 control-label">รหัสกลุ่มราคา:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtPrice_Group_ID" runat="server" CssClass="form-control" Enabled="false" style="max-width: 80%"></asp:TextBox>
                                            <asp:TextBox ID="txtProduct_List_ID" runat="server" CssClass="form-control" Enabled="false" Visible="false" style="max-width: 80%"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 control-label">ชื่อกลุ่มราคา:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtPriceGroupName" runat="server" CssClass="form-control" Enabled="false" style="max-width: 80%"></asp:TextBox>
                                            <asp:TextBox ID="txtPriceGroupType" runat="server" CssClass="form-control" Enabled="false" Visible="false" style="max-width: 80%"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <label class="control-label col-md-2">รหัสสินค้า:</label>
                                        <div class="col-md-6">
                                            <%--<asp:RequiredFieldValidator ID="rfvddlProductID"
                                                runat="server"
                                                ControlToValidate="ddlProductID"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ รหัสสินค้า"
                                                ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                            </asp:RequiredFieldValidator>--%>
                                            <asp:DropDownList ID="ddlProductID" runat="server" Style="border-left: 4px solid #ed1c24; max-width: 80%"
                                                CssClass="form-control"
                                                AutoPostBack="true" DataTextField="KEY"  DataValueField="VALUE" 
                                                OnSelectedIndexChanged="ddlProductID_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtProduct_ID" runat="server" CssClass="form-control" Enabled="false" Style="max-width: 80%" Visible="false"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">ราคาขายเอเยนต์:</label>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="rfvtxtCP_Meiji_Price"
                                                runat="server"
                                                ControlToValidate="txtCP_Meiji_Price"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ ราคาขายเอเยนต์"
                                                ValidationGroup="AgentValidation">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="rgxtxtCP_Meiji_Price" runat="server"
                                                ControlToValidate="txtCP_Meiji_Price"
                                                ErrorMessage="กรุณาระบุราคาขายเอเยนต์" Display="Dynamic"
                                                ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$" ValidationGroup="AgentValidation">
                                            </asp:RegularExpressionValidator>
                                            <asp:TextBox ID="txtCP_Meiji_Price" runat="server" CssClass="form-control" Style="text-align: right; border-left: 4px solid #ed1c24; max-width: 80%" TextMode="number" step="0.01"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 control-label">Vat(%):</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtVat" runat="server" CssClass="form-control" Style="text-align: right; max-width: 80%" TextMode="number"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <label class="col-md-2 control-label">วันที่เริ่มขายสินค้า:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtStartSaleDate" runat="server" CssClass="form-control" style="max-width: 80%" AutoCompleteType="disabled"></asp:TextBox>

                                            <ajaxToolkit:CalendarExtender ID="cldEtxtStartSaleDate"
                                                runat="server"
                                                TargetControlID="txtStartSaleDate"
                                                PopupButtonID="cldEtxtStartSaleDate" />
                                        </div>
                                        <label class="col-md-2 control-label"></label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtProduct_Name" runat="server" visible="false" CssClass="form-control" style="max-width: 80%" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <label class="col-md-2 control-label">วันที่เริ่มใช้ราคา:</label>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="rfvtxtStart_Effective_Date"
                                                runat="server"
                                                ControlToValidate="txtStart_Effective_Date"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ วันที่เริ่มใช้ราคา"
                                                ValidationGroup="AgentValidation">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtStart_Effective_Date" runat="server" CssClass="form-control" AutoCompleteType="disabled" Style="border-left: 4px solid #ed1c24; max-width: 80%"></asp:TextBox>

                                            <ajaxToolkit:CalendarExtender ID="cldEtxtStart_Effective_Date"
                                                runat="server"
                                                TargetControlID="txtStart_Effective_Date"
                                                PopupButtonID="cldEtxtStart_Effective_Date" />
                                        </div>
                                        <label class="col-md-2 control-label">วันที่สิ้นสุดใช้ราคา:</label>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="rfvtxtEnd_Effective_Date"
                                                runat="server"
                                                ControlToValidate="txtEnd_Effective_Date"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ วันที่สิ้นสุดใช้ราคา"
                                                ValidationGroup="AgentValidation">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtEnd_Effective_Date" runat="server" CssClass="form-control" AutoCompleteType="disabled"
                                                Style="border-left: 4px solid #ed1c24; max-width: 80%"></asp:TextBox>

                                            <ajaxToolkit:CalendarExtender ID="cldEtxtEnd_Effective_Date"
                                                runat="server"
                                                TargetControlID="txtEnd_Effective_Date"
                                                PopupButtonID="cldEtxtEnd_Effective_Date" />
                                        </div>

                                    </div>
                                    <div class="form-group">
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlSP" runat="server" Visible="false">


                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:ValidationSummary ID="ValidationSummary1"
                                            runat="server"
                                            Font-Bold="true"
                                            ForeColor="Red"
                                            ValidationGroup="SPValidation"
                                            DisplayMode="BulletList" />
                                        <label class="col-md-2 control-label">รหัสกลุ่มราคา:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSPPrice_Group_ID" runat="server" CssClass="form-control" Enabled="false" style="max-width: 80%"></asp:TextBox>
                                            <asp:TextBox ID="txtSPProduct_List_ID" runat="server" CssClass="form-control" Enabled="false" Visible="false" style="max-width: 80%"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 control-label">ชื่อกลุ่มราคา:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSPPriceGroupName" runat="server" CssClass="form-control" Enabled="false" style="max-width: 80%"></asp:TextBox>
                                            <asp:TextBox ID="txtSPPriceGroupType" runat="server" CssClass="form-control" Enabled="false" Visible="false" style="max-width: 80%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">รหัสสินค้า:</label>
                                        <div class="col-md-8">
                                            <asp:RequiredFieldValidator ID="rfvddlSPProductID"
                                                runat="server"
                                                ControlToValidate="ddlSPProductID"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ รหัสสินค้า"
                                                ValidationGroup="SPValidation" InitialValue="==ระบุ==">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlSPProductID" runat="server" Style="border-left: 4px solid #ed1c24; max-width: 80%"
                                                CssClass="form-control" AutoPostBack="true"
                                                DataTextField="KEY" DataValueField="VALUE" OnSelectedIndexChanged="ddlProductID_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSPProduct_ID" runat="server" CssClass="form-control" Enabled="false" Visible="false" style="max-width: 80%"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">ราคาตู้แช่:</label>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="rfvtxtSPCPMeijiPrice"
                                                runat="server"
                                                ControlToValidate="txtSPCPMeijiPrice"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ ราคาตู้แช่"
                                                ValidationGroup="SPValidation">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rgxtxtSPCPMeijiPrice" runat="server"
                                                ControlToValidate="txtSPCPMeijiPrice"
                                                ErrorMessage="กรุณาระบุราคาตู้แช่" Display="Dynamic"
                                                ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$" ValidationGroup="SPValidation">
                                            </asp:RegularExpressionValidator>
                                            <asp:TextBox ID="txtSPCPMeijiPrice" runat="server" CssClass="form-control" TextMode="number" step="0.01"
                                                Style="text-align: right; border-left: 4px solid #ed1c24; max-width: 80%"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 control-label">ราคาทุนสาว:</label>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="rfvtxtSPPrice"
                                                runat="server"
                                                ControlToValidate="txtSPPrice"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ ราคาทุนสาว"
                                                ValidationGroup="SPValidation">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rgetxtSPPrice" runat="server"
                                                ControlToValidate="txtSPPrice"
                                                ErrorMessage="กรุณาระบุราคาทุนสาวให้ถูกต้อง" Display="Dynamic"
                                                ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$"
                                                ValidationGroup="SPValidation">
                                            </asp:RegularExpressionValidator>
                                            <asp:TextBox ID="txtSPPrice" runat="server" CssClass="form-control" step="0.01" TextMode="number"
                                                Style="text-align: right; border-left: 4px solid #ed1c24; max-width: 80%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Vat(%):</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSPVat" runat="server" CssClass="form-control" TextMode="Number"
                                                Style="text-align: right; max-width: 80%"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 control-label">แต้ม:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSPPoint" runat="server" CssClass="form-control" TextMode="Number"
                                                Style="text-align: right; max-width: 80%" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">วันที่เริ่มขายสินค้า:</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSPStartSaleDate" runat="server" CssClass="form-control" style="max-width: 80%"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldEtxtSPStartSaleDate"
                                                runat="server"
                                                TargetControlID="txtSPStartSaleDate"
                                                PopupButtonID="cldEtxtSPStartSaleDate" />
                                        </div>
                                        <label class="col-md-2 control-label"></label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtSPProduct_Name" runat="server" CssClass="form-control" Enabled="false" visible="false" style="max-width: 80%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">วันที่เริ่มใช้ราคา:</label>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="rfvtxtSPStart_Effective_Date"
                                                runat="server"
                                                ControlToValidate="txtSPStart_Effective_Date"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ วันที่เริ่มใช้ราคา"
                                                ValidationGroup="SPValidation">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtSPStart_Effective_Date" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; max-width: 80%"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldEtxtSPStart_Effective_Date"
                                                runat="server"
                                                TargetControlID="txtSPStart_Effective_Date"
                                                PopupButtonID="cldEtxtSPStart_Effective_Date" />
                                        </div>
                                        <label class="col-md-2 control-label">วันที่สิ้นสุดใช้ราคา:</label>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="rfvtxtSPEnd_Effective_Date"
                                                runat="server"
                                                ControlToValidate="txtSPEnd_Effective_Date"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ วันที่สิ้นสุดใช้ราคา"
                                                ValidationGroup="SPValidation">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtSPEnd_Effective_Date" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; max-width: 80%"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="cldEtxtSPEnd_Effective_Date"
                                                runat="server"
                                                TargetControlID="txtSPEnd_Effective_Date"
                                                PopupButtonID="cldEtxtSPEnd_Effective_Date" />
                                        </div>
                                    </div>
                                    <div class="form-group"></div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>


                            </asp:Panel>


                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="ตกลง" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="ButtonCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="ButtonCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />

                                <asp:HiddenField ID="btnSaveMode" runat="server" />


                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlAssign" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">กำหนดกลุ่มราคา</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ประเภทกลุ่มราคา:</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblCPriceGroupType" runat="server" Text='<%# Eval("lblCPriceGroupType") %>'></asp:Label>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="col-md-2 control-label">ประเภทกลุ่มราคา:</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblNPriceGroupType" runat="server" Text='<%# Eval("lblNPriceGroupType") %>'></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">กลุ่มราคาปัจจุบัน:</label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlCurrent" runat="server" CssClass="form-control"
                                            DataTextField="Price_Group_Name" DataValueField="Price_Group_ID" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCurrent_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="col-md-2 control-label">กลุ่มราคาใหม่:</label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlNew" runat="server" CssClass="form-control"
                                            DataTextField="Price_Group_Name" DataValueField="Price_Group_ID" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlNew_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">


                                    <div class="col-md-5">
                                        <asp:ListBox ID="lstBPrimary" runat="server" CssClass="table table-striped table-bordered table-condensed"
                                            OnSelectedIndexChanged="lstBPrimary_SelectedIndexChanged" SelectionMode="Single"
                                            Height="200px"
                                            DataTextField="VALUE"
                                            DataValueField="KEY"></asp:ListBox>
                                    </div>
                                    <div class="col-md-2">
                                        <div style="text-align: center">
                                            <asp:Button ID="btnAddAll" runat="server" Font-Bold="true" Text=">>" CssClass="btn btn-default" Width="54px" OnClick="btnAddAll_Click" />
                                            <br />
                                            <asp:Button ID="btnAddOne" runat="server" Font-Bold="true" Text=">" CssClass="btn btn-default" Width="54px" OnClick="btnAddOne_Click" />
                                            <br />
                                            <asp:Button ID="btnRemoveOne" runat="server" Font-Bold="true" Text="<" CssClass="btn btn-default" Width="54px" OnClick="btnRemoveOne_Click" />
                                            <br />
                                            <asp:Button ID="btnRemoveAll" runat="server" Font-Bold="true" Text="<<" CssClass="btn btn-default" Width="54px" OnClick="btnRemoveAll_Click" />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:ListBox ID="lsbBSecondary" runat="server" CssClass="table table-striped table-bordered table-condensed"
                                            Height="200px" SelectionMode="Single"
                                            DataTextField="VALUE"
                                            DataValueField="KEY"></asp:ListBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnAssignSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnAssignSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        <asp:Button ID="btnAssignCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnAssignCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        <asp:Button ID="btnShowGrid" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnShowGrid_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

