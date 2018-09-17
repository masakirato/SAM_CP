<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="Revenue_Expense.aspx.cs"
    Inherits="Views_Revenue_Expense" uiculture="th" culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
            //alert(el.value);
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

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายรับ/รายจ่าย</span>&nbsp;&nbsp;
                            <%--<span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>--%>
                            <span class="glyphicon glyphicon-retweet" style="font-size: 18px"></span>
                           
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่บันทึก:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtStartDate" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtStartDate" runat="server"
                                            TargetControlID="txtStartDate" PopupButtonID="cldtxtStartDate" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtEndDate" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtEndDate" runat="server"
                                            TargetControlID="txtEndDate" PopupButtonID="cldtxtEndDate" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" onclick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>

                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="ButtonAddNew" class="btn btn-primary" runat="server" Text="สร้าง" onclick="ButtonAddNew_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่พบข้อมูล</center>
                                    </div>
                                </asp:Panel>
                                <asp:GridView ID="GridViewRevenue_Expense"
                                    runat="server" showheaderwhenempty="true"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    onrowcommand="GridViewRevenue_Expense_RowCommand"
                                    AllowPaging="true" PageSize="10" OnDataBound="GridViewRevenue_Expense_DataBound" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center">
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ"
                                                        CommandArgument='<%# Eval("Post_No") %>'
                                                        CommandName="_Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="วันที่">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PostDate" runat="server" Text='<%# Eval("Post_Date","{0:dd/MM/yyyy}") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="เลขที่">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_Post_No" runat="server" CssClass="btn btn-link" Text='<%# Eval("Post_No") %>'
                                                    CommandArgument='<%# Eval("Post_No") %>' CommandName="View" style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="เลขที่รายรับ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_RV_Account_No" runat="server" Text='<%# Eval("RV_Account_No") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="จำนวนเงินรายรับ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_RV_Amount" runat="server" Text='<%# Eval("RV_Amount","{0:N2}") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="เลขที่รายจ่าย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_EP_Account_No" runat="server" Text='<%# Eval("EP_Account_No") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงินรายจ่าย">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_EP_Amount" runat="server" Text='<%# Eval("EP_Amount","{0:N2}") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <pagertemplate>
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

            <asp:Panel ID="pnlForm" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายรับ/รายจ่าย</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                            <%--<asp:Label ID="LabelPageHeader" runat="server" Text="รายรับ รายจ่าย"></asp:Label>--%>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <label class="col-md-2 control-label">วันที่บันทึก:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtPost_Date" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>

                                    </div>
                                    <label class="col-md-2 control-label">เลขที่:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtPost_No" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="col-md-1 control-label" style="text-align: left">รายรับ:</label>
                                <div class="col-md-2">
                                    <asp:textbox id="Textbox1" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                </div>
                                <label class="col-md-2 control-label">บาท</label>
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnCreateNew" class="btn btn-primary" runat="server" Text="สร้าง" onclick="btnCreateNew_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">                                

                                <asp:GridView ID="GridViewRevenue"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    OnRowCancelingEdit="GridViewRevenue_RowCancelingEdit"
                                    OnRowDataBound="GridViewRevenue_RowDataBound"
                                    OnRowEditing="GridViewRevenue_RowEditing"
                                    OnRowUpdating="GridViewRevenue_RowUpdating"
                                    OnRowCommand="GridViewRevenue_RowCommand"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>                                        
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center" HeaderStyle-Width="100px">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Eval("Account_No") %>' CommandName="_Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Eval("Account_No") %>' CommandName="AddNew" text="บันทึก"></asp:LinkButton>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center" footerstyle-wrap="false" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditCancel" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkFooterCancel" CssClass="btn btn-mini btn-default" runat="server" CausesValidation="True" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รายการ" footerstyle-horizontalalign="Left" itemstyle-horizontalalign="Left" HeaderStyle-Width="350px">                                          
                                        <EditItemTemplate>
                                            <asp:Label ID="lblItemAccount_Code" runat="server" Text='<%# Eval("Account_Name")%>' Visible = "false"></asp:Label>
                                            <asp:DropDownList ID="ddlEditDetail" runat="server" CssClass="form-control"  style="border-left: 4px solid #ed1c24;" Enabled="false">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="lblItemAccount_Code" runat="server" Text='<%# Eval("Account_Name") %>' style="color: blue;"></asp:Label>
                                        </ItemTemplate>                                            
                                        <FooterTemplate>
                                            <asp:Label ID="lblItemAccount_Code" runat="server" Text='<%# Eval("Account_Name")%>' Visible = "false" ></asp:Label>
                                            <asp:DropDownList ID="ddlFooterDetail" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงิน" HeaderStyle-Width="150px">
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtEditRevenue_Amount" runat="server" CssClass="form-control" Text='<%# Eval("Amount") %>' style="color: blue; width:150px " TextMode="Number"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label id="lblItemRevenue_Amount" runat="server" Text='<%# Eval("Amount","{0:N2}") %>' style="color: blue; text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterRevenue_Amount" runat="server" CssClass="form-control" style="color: blue; text-align: right; width:150px " TextMode="Number"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หมายเหตุ">
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtEditRemark" runat="server" CssClass="form-control" Text='<%# Eval("Remark") %>' style="color: blue; "></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label id="lblItemRemark" runat="server" Text='<%# Eval("Remark") %>' style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterRemark" runat="server" CssClass="form-control" style="color: blue; text-align: right;"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" visible="false">
                                            <ItemTemplate>
                                                <asp:Label id="lblPost_No" runat="server" Text='<%# Eval("Post_No") %>' style="color: blue; text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account_No" itemstyle-horizontalalign="Right" visible="false">
                                            <ItemTemplate>
                                                <asp:Label id="lblAccount_No" runat="server" Text='<%# Eval("Account_No") %>' style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>                                

                            </div>
                            <div class="col-md-12">
                                <label class="col-md-1 control-label" style="text-align: left">รายจ่าย:</label>
                                <div class="col-md-2">
                                    <asp:textbox id="Textbox2" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                </div>
                                <label class="col-md-2 control-label">บาท</label>
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnAddNewExpense" class="btn btn-primary" runat="server" Text="สร้าง" onclick="btnAddNewExpense_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">

                                <asp:GridView ID="GridViewExpense"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    OnRowCancelingEdit="GridViewExpense_RowCancelingEdit"
                                    OnRowDataBound="GridViewExpense_RowDataBound"
                                    OnRowEditing="GridViewExpense_RowEditing"
                                    OnRowUpdating="GridViewExpense_RowUpdating"
                                    OnRowCommand="GridViewExpense_RowCommand"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center" HeaderStyle-Width="100px">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Eval("Account_No") %>' CommandName="_Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Eval("Account_No") %>' CommandName="AddNew" text="บันทึก"></asp:LinkButton>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center" footerstyle-wrap="false" HeaderStyle-Width="100px"> 
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditCancel" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkFooterCancel" CssClass="btn btn-mini btn-default" runat="server" CausesValidation="True" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รายการ" footerstyle-horizontalalign="Left" itemstyle-horizontalalign="Left" HeaderStyle-Width="350px">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblItemAccount_Code" runat="server" Text='<%# Eval("Account_Name")%>' Visible = "false"></asp:Label>
                                            <asp:DropDownList ID="ddlEditDetail" runat="server" CssClass="form-control"  style="border-left: 4px solid #ed1c24;" Enabled="false">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label id="lblItemAccount_Code" runat="server" Text='<%# Eval("Account_Name") %>' style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblItemAccount_Code" runat="server" Text='<%# Eval("Account_Name")%>' Visible = "false" ></asp:Label>
                                            <asp:DropDownList ID="ddlFooterDetail" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงิน" HeaderStyle-Width="150px">
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtEdit_Amount" runat="server" CssClass="form-control" Text='<%# Eval("Amount") %>' style="color: blue; width:150px " TextMode="Number"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label id="lblItem_Amount" runat="server" Text='<%# Eval("Amount","{0:N2}") %>' style="color: blue; text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooter_Amount" runat="server" CssClass="form-control" style="color: blue; text-align: right; width:150px " TextMode="Number"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หมายเหตุ">
                                            <EditItemTemplate>
                                                <asp:TextBox id="txtEditRemark" runat="server" CssClass="form-control" Text='<%# Eval("Remark") %>' style="color: blue; "></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label id="lblItemRemark" runat="server" Text='<%# Eval("Remark") %>' style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterRemark" runat="server" CssClass="form-control" style="color: blue; text-align: right;"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" visible="false">
                                            <ItemTemplate>
                                                <asp:Label id="lblPost_No" runat="server" Text='<%# Eval("Post_No") %>' style="color: blue; text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account_No" itemstyle-horizontalalign="Right" visible="false">
                                            <ItemTemplate>
                                                <asp:Label id="lblAccount_No" runat="server" Text='<%# Eval("Account_No") %>' style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnBackToGrid" class="btn btn-primary" runat="server" Text="กลับไปหน้าค้นหา" onclick="btnBackToGrid_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

