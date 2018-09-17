<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="Product_List.aspx.cs" Inherits="Views_Product_List" %>

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

        function validateFloatKeyPress(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            //if (charCode == 46 && el.value.indexOf(".") !== -1) {
            //    return false;
            //}

            //if (el.value.indexOf(".") !== -1) {
            //    var range = document.selection.createRange();

            //    if (range.text != "") {
            //    }
            //    else {
            //        var number = el.value.split('.');
            //        if (number.length == 2 && number[1].length > 1)
            //            return false;
            //    }
            //}

            return true;
        }
        function validateFloatKeyPress1(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            if (charCode == 46 && el.value.indexOf(".") !== -1) {
                return false;
            }

            if (el.value.indexOf(".") !== -1) {
                var range = document.selection.createRange();

                if (range.text != "") {
                }
                else {
                    var number = el.value.split('.');
                    if (number.length == 2 && number[1].length > 1)
                        return false;
                }
            }

            return true;
        }
    </script>

    <asp:Panel ID="pnlGrid" Visible="True" runat="server">
        <div class="container-full-content">
            <div class="row">
                <div class="col-md-12 top-bar-content-none" style="height: 45px">
                    <span class="title" style="font-size: 18px">รายการสินค้า</span>&nbsp;&nbsp;
                    <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                </div>
                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-md-2 control-label">รหัสสินค้า:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSearchProduct_ID" runat="server" CssClass="form-control" onkeypress="return  validateFloatKeyPress(this,event);" MaxLength ="13"></asp:TextBox>
                                </div>
                                <label class="col-md-2 control-label">ชื่อสินค้า:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSearchProduct_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">กลุ่มสินค้า:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlSearchProduct_Group" runat="server" CssClass="form-control">
                                        <asp:ListItem>==ระบุ==</asp:ListItem>
                                        <asp:ListItem>นมสดพาสเจอร์ไรส์</asp:ListItem>
                                        <asp:ListItem>นมเปรี้ยว</asp:ListItem>
                                        <asp:ListItem>โยเกิร์ตเมจิ</asp:ListItem>
                                        <asp:ListItem>นมเปรี้ยวไพเกน</asp:ListItem>
                                        <asp:ListItem>อื่นๆ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2 control-label">ขนาด:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSearchSize" runat="server" CssClass="form-control" TextMode="number">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">หน่วย:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlSearchUnit_of_item_ID" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:HiddenField ID="btnSaveMode" runat="server" />

                            <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            <asp:Button ID="btnCancelSearch" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancelSearch_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
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
                        <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                <center>ไม่พบข้อมูล</center>
                            </div>
                        </asp:Panel>
                        <asp:GridView ID="grdProduct"
                            runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames=""
                            ShowFooter="True"
                            CellPadding="0" 
                            ForeColor="#333333" OnRowCommand="grdProduct_RowCommand"
                            CssClass="table table-striped table-bordered table-condensed" 
                            ShowHeaderWhenEmpty="false"
                            HeaderStyle-HorizontalAlign="Center" 
                            AllowPaging="true" PageSize="10" OnDataBound="grdProduct_DataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false ;">
                                            <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" Width="48px"
                                                CommandArgument='<%# Eval("Product_ID") %>'
                                                CommandName="_Delete"></asp:LinkButton>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลำดับ">
                                    <ItemTemplate>
                                        <%--<asp:Label id="lbl_Order_No" runat="server" Text='<%# Eval("Order_No") %>' style="color: blue"></asp:Label>--%>
                                        <asp:Label ID="lbl_Order_No" runat="server" Text='<%# Container.DataItemIndex + 1 %>' Style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสินค้า" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkB_Product_ID" runat="server" CssClass="btn btn-link"
                                            Text='<%# Eval("Product_ID") %>'
                                            CommandArgument='<%# Eval("Product_ID") %>'
                                            CommandName="View"
                                            Style="text-decoration: underline"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสินค้า">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="กลุ่มสินค้า">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Product_group_ID" runat="server" Text='<%# Eval("Product_group_ID") %>' Style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ขนาด">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Price" runat="server" Text='<%# Eval("Size") %>' Style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="หน่วย">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Vat" runat="server" Text='<%# Eval("Unit_of_item_ID") %>' Style="color: blue"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สถานะ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status") %>' Style="color: blue">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkB_Edit_Product" runat="Server" Text="แก้ไข Product"
                                            CommandArgument='<%# Eval("Product_ID") %>'
                                            CommandName="_Edit"
                                            CssClass="btn btn-mini btn-primary"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="True" />
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

    <asp:Panel ID="pnlForm" Visible="False" runat="server">
        <div class="container-full-content">
            <div class="row">
                <div class="col-md-12 top-bar-content-none" style="height: 45px">
                    <span class="title" style="font-size: 18px">รายการสินค้า</span>&nbsp;&nbsp;
                    <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                </div>
                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:ValidationSummary ID="ProductValidationSummary"
                                    runat="server"
                                    Font-Bold="true"
                                    ForeColor="Red"
                                    ValidationGroup="ProductValidation"
                                    DisplayMode="BulletList" />

                                <label class="col-md-2 control-label">รหัสสินค้า:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtProduct_ID"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุรหัสสินค้า"
                                        ControlToValidate="txtProduct_ID"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtProduct_ID" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;" AutoPostBack="true"
                                        OnTextChanged="txtProduct_ID_TextChanged"  MaxLength="13" onkeypress="return  validateFloatKeyPress(this,event);" ></asp:TextBox>
                                </div>
                                <label class="col-md-2 control-label">ชื่อสินค้า:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtProduct_Name"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุชื่อสินค้า"
                                        ControlToValidate="txtProduct_Name"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtProduct_Name" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">ขนาด:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtSize"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุขนาด"
                                        ControlToValidate="txtSize"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtSize" runat="server" CssClass="form-control" TextMode="Number" Style="border-left: 4px solid #ed1c24; text-align: right;" MaxLength="10" onkeypress="return  validateFloatKeyPress(this,event);" ></asp:TextBox>
                                </div>
                                <label class="col-md-2 control-label">หน่วย:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvddlUnit_of_item_ID"
                                        runat="server"
                                        ControlToValidate="ddlUnit_of_item_ID"
                                        Display="None"
                                        ErrorMessage="ต้องระบุหน่วย"
                                        ValidationGroup="ProductValidation" InitialValue="">
                                    </asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlUnit_of_item_ID" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY" Style="border-left: 4px solid #ed1c24;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">กลุ่มสินค้า:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvddlProduct_group_ID"
                                        runat="server"
                                        ControlToValidate="ddlProduct_group_ID"
                                        Display="None"
                                        ErrorMessage="ต้องระบุกลุ่มสินค้า"
                                        ValidationGroup="ProductValidation" InitialValue="==ระบุ==">
                                    </asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlProduct_group_ID" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                        <asp:ListItem>==ระบุ==</asp:ListItem>
                                        <asp:ListItem>นมสดพาสเจอร์ไรส์</asp:ListItem>
                                        <asp:ListItem>นมเปรี้ยว</asp:ListItem>
                                        <asp:ListItem>โยเกิร์ตเมจิ</asp:ListItem>
                                        <asp:ListItem>นมเปรี้ยวไพเกน</asp:ListItem>
                                        <asp:ListItem>อื่นๆ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2 control-label">SAP Product Code:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtSAPProductCode"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุSAP Product Code"
                                        ControlToValidate="txtSAPProductCode"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtSAPProductCode" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;" AutoCompleteType="disabled" onkeypress="return  validateFloatKeyPress(this,event);" MaxLength ="8"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">ราคาตู้แช่:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtCP_Meiji_Price"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุราคาตู้แช่"
                                        ControlToValidate="txtCP_Meiji_Price"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rgxtxtCP_Meiji_Price" runat="server"
                                        ControlToValidate="txtCP_Meiji_Price"
                                        ErrorMessage="กรุณาระบุราคาตู้แช่ให้ถูกต้อง" Display="Dynamic"
                                        ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$" ValidationGroup="ProductValidation">
                                    </asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtCP_Meiji_Price" runat="server" TextMode="number" step="0.01" CssClass="form-control" Style="border-left: 4px solid #ed1c24; text-align: right;" MaxLength="20" onkeypress="return  validateFloatKeyPress1(this,event);" ></asp:TextBox>
                                </div>
                                <label class="col-md-2 control-label">ราคาทุนสาว:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtSPPrice"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุราคาทุนสาว"
                                        ControlToValidate="txtSPPrice"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rgxtxtSPPrice" runat="server"
                                        ControlToValidate="txtSPPrice"
                                        ErrorMessage="กรุณาระบุรราคาทุนสาวให้ถูกต้อง" Display="Dynamic"
                                        ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$" ValidationGroup="ProductValidation">
                                    </asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtSPPrice" runat="server" TextMode="number" step="0.01" CssClass="form-control" Style="border-left: 4px solid #ed1c24; text-align: right;" MaxLength="20" onkeypress="return  validateFloatKeyPress1(this,event);" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">ราคาขายเอเยนต์:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtAgentPrice"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุราคาขายเอเยนต์"
                                        ControlToValidate="txtAgentPrice"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rgxtxtAgentPrice" runat="server"
                                        ControlToValidate="txtAgentPrice"
                                        ErrorMessage="กรุณาระบุรราคาเอเยนต์ให้ถูกต้อง" Display="Dynamic"
                                        ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$" ValidationGroup="ProductValidation">
                                    </asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtAgentPrice" runat="server" TextMode="number" step="0.01" CssClass="form-control" Style="border-left: 4px solid #ed1c24; text-align: right;" MaxLength="20" onkeypress="return  validateFloatKeyPress1(this,event);" ></asp:TextBox>
                                </div>
                                <label class="col-md-2 control-label">Vat(%):</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtVat"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุVat(%)"
                                        ControlToValidate="txtVat"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtVat" runat="server" TextMode="number" step="0.01" CssClass="form-control" Style="border-left: 4px solid #ed1c24; text-align: right;" MaxLength="20" onkeypress="return  validateFloatKeyPress1(this,event);" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">รูปภาพ:</label>
                                <div class="col-md-4">
                                    <span id="spanImage" runat="server">
                                        <asp:TextBox ID="txtImage" runat="server" AutoPostBack="true" CssClass="form-control" Visible="false"></asp:TextBox>
                                        <asp:HiddenField ID="hdfImage" runat="server" />
                                        <asp:HiddenField ID="hdffilename" runat="server" />
                                        <%--<asp:FileUpload ID="fulProductPhoto" runat="server" CssClass="form-control" />--%>
                                        <script>
                                            function uploadComplete(sender, args) {
                                                var control = document.getElementById("<%=AsyncFileUpload1.ClientID%>");
                                                control.style.display = "block";
                                                 // alert('upload รูปภาพสำเร็จ');
                                                //   __doPostBack('UpdatePanelGrid', '');

                                            }
                                        </script>
                                        <ajaxToolkit:AsyncFileUpload OnClientUploadError="uploadError"
                                            OnClientUploadComplete="uploadComplete" runat="server"
                                            ID="AsyncFileUpload1" UploaderStyle="Traditional"
                                            CompleteBackColor="White"
                                            UploadingBackColor="#CCFFFF"
                                            OnUploadedComplete="FileUploadComplete" />
                                    </span>
                                </div>
                                <label class="col-md-2 control-label">แต้ม:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtPoint"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุแต้ม"
                                        ControlToValidate="txtPoint"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtPoint" runat="server" TextMode="number"
                                        CssClass="form-control" Style="border-left: 4px solid #ed1c24; text-align: right;" MaxLength="20" onkeypress="return  validateFloatKeyPress1(this,event);" ></asp:TextBox>
                                </div>

                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">จำนวนขวด/แพ็คต่อลัง:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtPacking_Size"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุจำนวนขวด/แพ็คต่อลัง"
                                        ControlToValidate="txtPacking_Size"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtPacking_Size" runat="server"
                                        CssClass="form-control" Style="border-left: 4px solid #ed1c24; text-align: right;" TextMode="number" MaxLength="20" onkeypress="return  validateFloatKeyPress1(this,event);" ></asp:TextBox>
                                </div>
                                <label class="col-md-2 control-label">ลำดับ:</label>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvtxtOrder_No"
                                        runat="server"
                                        Display="None"
                                        ErrorMessage="ต้องระบุลำดับ"
                                        ControlToValidate="txtOrder_No"
                                        ValidationGroup="ProductValidation">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtOrder_No" runat="server" TextMode="number" CssClass="form-control" Style="border-left: 4px solid #ed1c24; text-align: right;" MaxLength="20" onkeypress="return  validateFloatKeyPress1(this,event);" ></asp:TextBox>
                                </div>
                                <%-- <label class="col-md-2 control-label">จำนวนขวดต่อแพ็ค:</label>
                                        <div class="col-md-4">
                                            <asp:textbox id="txtPacking_Size" runat="server" CssClass="form-control"></asp:textbox>
                                        </div>--%>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">สถานะ:</label>
                                <div class="col-md-4">
                                    <%--<asp:textbox id="txtStatus" runat="server" CssClass="form-control" enabled="false"></asp:textbox>--%>
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; text-align: right;">
                                        <%--<asp:ListItem Text="==ระบุ=="></asp:ListItem>--%>
                                        <asp:ListItem Text="Active"></asp:ListItem>
                                        <asp:ListItem Text="In active"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12 text-center">
                                    <asp:Image ID="imgProductPhoto" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>

                            <%--</form>--%>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">

        function uploadError(sender) {

        }
    </script>

</asp:Content>

