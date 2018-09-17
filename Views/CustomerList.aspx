<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="CustomerList.aspx.cs" Inherits="Views_CustomerList" UICulture="th" Culture="th-TH" %>

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

        /*Disable enter key */
        function DisableEnterKey(e) {
            var key;
            if (window.event)
                key = window.event.keyCode; //IE
            else
                key = e.which; //firefox     

            return (key != 13);
        }
    </script>

     <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
        <div class="panel panel-default">
    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>

            <asp:Panel ID="pnlGrid" Visible="True" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายชื่อลูกค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อลูกค้า:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchFirst_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ประเภทลูกค้า:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchCustomerType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="สมาชิก"></asp:ListItem>
                                            <asp:ListItem Text="ทั่วไป"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">รหัสลูกค้า:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchCustomerID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ประเภทที่พักอาศัย:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchResidence_Type_ID" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ที่อยู่ลูกค้า:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchHome_House_No" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchSP" runat="server" CssClass="form-control" DataTextField="FullName" DataValueField="User_ID"></asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">สถานะการเป็นลูกค้า:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchCustomerStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="ยังติดต่ออยู่"></asp:ListItem>
                                            <asp:ListItem Text="ขาดการติดต่อ"></asp:ListItem>
                                            <asp:ListItem Text="ระงับการส่งชั่วคราว"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnCancelSearch" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancelSearch_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>

                            <div class="col-md-4">
                                <asp:Button ID="btnAddNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnAddNew_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <div style="overflow: auto; height: auto; width: auto; max-height: 600px; max-width: 1200px;">
                                    <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                            <center>ไม่พบข้อมูล</center>
                                        </div>
                                    </asp:Panel>
                                    <asp:GridView ID="grdCustomer"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataKeyNames=""
                                        ShowFooter="false" showheaderwhenempty="false"
                                        OnRowCommand="grdCustomer_RowCommand"
                                        OnRowDeleting="grdCustomer_RowDeleting"
                                        CellPadding="0" ForeColor="#333333"
                                        CssClass="table table-striped table-bordered table-condensed" AllowPaging="true" PageSize="10" OnDataBound="grdCustomer_DataBound">
                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                        <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Eval("Customer_ID") %>' CommandName="Delete"></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="รหัสลูกค้า" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkB_Customer_ID" runat="server" CssClass="btn btn-link" Text='<%# Eval("Customer_ID") %>'
                                                        CommandArgument='<%# Eval("Customer_ID") %>' CommandName="View" Style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อลูกค้า">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_First_Name" runat="server" Text='<%# Eval("First_Name") + " " + Eval("Last_Name") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ที่อยู่ลูกค้า">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Home_House_No" runat="server" Text='<%# Eval("Home_House_No") + " " + Eval("Home_Tower") + " " + Eval("Home_Village") + " " + Eval("Home_Village_No") + " " + Eval("Home_Alley") + " " + Eval("Home_Road") + " " + Eval("Home_Sub_district") + " " + Eval("Home_District") + " " + Eval("Home_Province") + " " + Eval("Home_Postal_ID")  %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อพนักงาน">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SP_Name" runat="server" Text='<%# Eval("SP_Name") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เบอร์โทรศัพท์">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Mobile" runat="server" Text='<%# Eval("Home_Phone_No") %>' Style="color: blue" Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถานะการเป็นลูกค้า">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภทลูกค้า">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Customer_Type" runat="server" Text='<%# Eval("Customer_Type") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
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

                </div>
            </asp:Panel>

            <asp:Panel ID="pnlForm" Visible="False" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ข้อมูลลูกค้า</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-user" style="font-size: 18px"></span><!--<img src="Images/help-contents.png" />-->
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <asp:ValidationSummary ID="ValidationSummary1"
                                    runat="server"
                                    DisplayMode="BulletList"
                                    ValidationGroup="CustomerValidation"
                                    ForeColor="red"
                                    Font-Bold="true" />
                                <div class="form-group">
                                    <label class="col-md-2 control-label">CV CODE:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCV_Code" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;" Enabled="false" AutoPostBack="true" OnTextChanged="txtCV_Code_TextChanged"></asp:TextBox>


                                        <%--  
                                        <asp:DropDownList ID="ddlCV_Code" runat="server" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                        </asp:DropDownList>
                                        --%>
                                    </div>
                                    <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">รหัสลูกค้า:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvtxtCustomer_ID"
                                            runat="server"
                                            ControlToValidate="txtCustomer_ID"
                                            Display="None"
                                            ErrorMessage="ต้องระบุรหัสลูกค้า"
                                            ValidationGroup="CustomerValidation">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCustomer_ID" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ประเภทลูกค้า:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txtddlCustomerType"
                                            runat="server"
                                            ControlToValidate="ddlCustomerType"
                                            Display="None"
                                            ErrorMessage="ต้องระบุประเภทลูกค้า"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ==">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="สมาชิก"></asp:ListItem>
                                            <asp:ListItem Text="ทั่วไป"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สถานะการเป็นลูกค้า:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txtddlCustomerStatus"
                                            runat="server"
                                            ControlToValidate="ddlCustomerStatus"
                                            Display="None"
                                            ErrorMessage="ต้องระบุสถานะการเป็นลูกค้า"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCustomerStatus" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="ยังติดต่ออยู่"></asp:ListItem>
                                            <asp:ListItem Text="ขาดการติดต่อ"></asp:ListItem>
                                            <asp:ListItem Text="ระงับการส่งชั่วคราว"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ประเภทที่พักอาศัย:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlResidentType_ID" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อ:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txttxtFirst_Name"
                                            runat="server"
                                            ControlToValidate="txtFirst_Name"
                                            Display="None"
                                            ErrorMessage="ต้องระบุชื่อลูกค้า"
                                            ValidationGroup="CustomerValidation"> 
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtFirst_Name" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">นามสกุล:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtLast_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txttxtHome_Phone_No"
                                            runat="server"
                                            ControlToValidate="txtHome_Phone_No"
                                            Display="None"
                                            ErrorMessage="ต้องระบุเบอร์โทรศัพท์"
                                            ValidationGroup="CustomerValidation"> 
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtHome_Phone_No" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">เบอร์มือถือ:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <%-- <div class="form-inline">--%>
                                    <label class="col-md-2 control-label">ชื่อผู้ติดต่อ:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txttxtContact_Name"
                                            runat="server"
                                            ControlToValidate="txtContact_Name"
                                            Display="None"
                                            ErrorMessage="ต้องระบุชื่อผู้ติดต่อ"
                                            ValidationGroup="CustomerValidation"> 
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtContact_Name" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">วัน/เดือน/ปี เกิด:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtBirthday" runat="server" CssClass="form-control"></asp:TextBox>

                                        <ajaxToolkit:CalendarExtender ID="cldEtxtBirthday"
                                            runat="server"
                                            TargetControlID="txtBirthday"
                                            PopupButtonID="cldEtxtBirthday" />
                                    </div>
                                    <%-- </div>--%>
                                </div>
                                <div class="form-group">
                                    <%-- <div class="form-inline">--%>
                                    <label class="col-md-2 control-label">สถานะ:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvddlStatus"
                                            runat="server"
                                            ControlToValidate="ddlStatus"
                                            Display="None"
                                            ErrorMessage="ต้องระบุสถานะ"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ==">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>In active</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่สมัครสมาชิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtMemberDate" runat="server" CssClass="form-control"></asp:TextBox>


                                        <ajaxToolkit:CalendarExtender ID="cldEtxtMemberDate"
                                            runat="server"
                                            TargetControlID="txtMemberDate"
                                            PopupButtonID="cldEtxtMemberDate" />
                                    </div>
                                    <%-- </div>--%>
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>ที่อยู่ลูกค้า</h4>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txttxtHome_House_No"
                                            runat="server"
                                            ControlToValidate="txtHome_House_No"
                                            Display="None"
                                            ErrorMessage="ต้องระบุเลขที่ที่อยู่ลูกค้า"
                                            ValidationGroup="CustomerValidation"> 
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtHome_House_No" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">อาคาร:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtHome_Tower" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">หมู่บ้าน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtHome_Village" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">หมู่ที่:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtHome_Village_No" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ตรอก/ซอย:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtHome_Alley" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ถนน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtHome_Road" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">จังหวัด:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txtddlHome_Province"
                                            runat="server"
                                            ControlToValidate="ddlHome_Province"
                                            Display="None"
                                            ErrorMessage="ต้องระบุจังหวัดที่อยู่ลูกค้า"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlHome_Province" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHome_Province_SelectedIndexChanged"
                                            CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="Province">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">อำเภอ/เขต:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txtddlHome_District"
                                            runat="server"
                                            ControlToValidate="ddlHome_District"
                                            Display="None"
                                            ErrorMessage="ต้องระบุอำเภอ/เขตที่อยู่ลูกค้า"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlHome_District" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHome_District_SelectedIndexChanged"
                                            CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="District">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">ตำบล/แขวง:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txtddlHome_Sub_district"
                                            runat="server"
                                            ControlToValidate="ddlHome_Sub_district"
                                            Display="None"
                                            ErrorMessage="ต้องระบุตำบล/แขวงที่อยู่ลูกค้า"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlHome_Sub_district" runat="server" AutoPostBack="true"
                                            CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="Sub_district"
                                            onselectedindexchanged="ddlHome_Sub_district_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">รหัสไปรษณีย์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtHome_Postal_ID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>ที่อยู่จัดส่ง
                                        <asp:Button ID="btnCopyAddress" class="btn btn-primary" runat="server" Text="ใช้ที่อยู่ตามที่อยู่ลูกค้า" OnClick="btnCopyAddress_Click" /></h4>

                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txttxtShipment_House_No"
                                            runat="server"
                                            ControlToValidate="txtShipment_House_No"
                                            Display="None"
                                            ErrorMessage="ต้องระบุเลขที่ที่อยู่จัดส่ง"
                                            ValidationGroup="CustomerValidation"> 
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtShipment_House_No" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">อาคาร:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtShipment_Tower" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">หมู่บ้าน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtShipment_Village" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">หมู่ที่:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtShipment_Village_No" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ตรอก/ซอย:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtShipment_Alley" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ถนน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtShipment_Road" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">จังหวัด:</label>
                                    <div class="col-md-4">

                                        <asp:RequiredFieldValidator ID="txtddlShipment_Province"
                                            runat="server"
                                            ControlToValidate="ddlShipment_Province"
                                            Display="None"
                                            ErrorMessage="ต้องระบุจังหวัดที่อยู่จัดส่ง"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlShipment_Province" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShipment_Province_SelectedIndexChanged"
                                            CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="Province">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">อำเภอ/เขต:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txtddlShipment_District"
                                            runat="server"
                                            ControlToValidate="ddlShipment_District"
                                            Display="None"
                                            ErrorMessage="ต้องระบุอำเภอ/เขตที่อยู่จัดส่ง"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlShipment_District" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShipment_District_SelectedIndexChanged"
                                            CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="District">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">ตำบล/แขวง:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txtddlShipment_Sub_district"
                                            runat="server"
                                            ControlToValidate="ddlShipment_Sub_district"
                                            Display="None"
                                            ErrorMessage="ต้องระบุตำบล/แขวงที่อยู่จัดส่ง"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlShipment_Sub_district" runat="server" AutoPostBack="true"
                                            CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="Sub_district"
                                            onselectedindexchanged="ddlShipment_Sub_district_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">รหัสไปรษณีย์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtShipment_Postal_ID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">รายละเอียดเพิ่มเติม:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>พนักงานที่ดูแล</h4>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">รหัสพนักงาน:</label>
                                    <div class="col-md-4">
                                         <asp:RequiredFieldValidator ID="rqvtxtSP_ID"
                                            runat="server"
                                            ControlToValidate="txtSP_ID"
                                            Display="None"
                                            ErrorMessage="ต้องระบุชื่อพนักงาน"
                                            ValidationGroup="CustomerValidation"> 
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSP_ID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                     
                                         <asp:RequiredFieldValidator ID="rqvddlSP1"
                                            runat="server"
                                            ControlToValidate="ddlSP"
                                            Display="None"
                                            ErrorMessage="ต้องระบุชื่อพนักงาน"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>

                                        <asp:DropDownList ID="ddlSP" runat="server" AutoPostBack="true"  DataTextField="FullName" DataValueField="User_ID"  OnSelectedIndexChanged="ddlSP_SelectedIndexChanged" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"> 
                                        </asp:DropDownList>

                                       
                                    </div>
                                </div>

                                <%--<div class="form-group">
                                    <label class="col-md-2 control-label">พนักงานขายแทน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlReplace_Sales" runat="server" CssClass="form-control" datatextfield="First_Name" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">ประเภทการชำระเงิน:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="txtddlPaymentType"
                                            runat="server"
                                            ControlToValidate="ddlPaymentType"
                                            Display="None"
                                            ErrorMessage="ต้องระบุประเภทการชำระเงิน"
                                            ValidationGroup="CustomerValidation" initialvalue="==ระบุ=="> 
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>เงินสด</asp:ListItem>
                                            <asp:ListItem>เครดิต</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ข้อมูลการวางบิล:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlBillingType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBillingType_SelectedIndexChanged">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>บิลชนบิล</asp:ListItem>
                                            <asp:ListItem>รายสัปดาห์</asp:ListItem>
                                            <asp:ListItem>รายเดือน</asp:ListItem>
                                            <asp:ListItem>วางบิลอื่นๆ</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันวางบิล:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlBilling_Day_of_Week" runat="server" CssClass="form-control">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>อาทิตย์</asp:ListItem>
                                            <asp:ListItem>จันทร์</asp:ListItem>
                                            <asp:ListItem>อังคาร</asp:ListItem>
                                            <asp:ListItem>พุธ</asp:ListItem>
                                            <asp:ListItem>พฤหัสบดี</asp:ListItem>
                                            <asp:ListItem>ศุกร์</asp:ListItem>
                                            <asp:ListItem>เสาร์</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlBilling_Day_of_Month" runat="server" CssClass="form-control">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>ไม่มีการวางบิล</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>วันสุดท้ายของเดือน</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBilling_Day_Other" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:TextBox ID="txtBilling" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<ajaxToolkit:CalendarExtender ID="cldEtxtBilling_Day_Other"
                                            runat="server"
                                            TargetControlID="txtBilling_Day_Other"
                                            PopupButtonID="cldEtxtBilling_Day_Other" />--%>
                                    </div>
                                    <label class="col-md-2 control-label">วันเก็บเงิน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlDue_Billing_Day_of_Week" runat="server" CssClass="form-control">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>อาทิตย์</asp:ListItem>
                                            <asp:ListItem>จันทร์</asp:ListItem>
                                            <asp:ListItem>อังคาร</asp:ListItem>
                                            <asp:ListItem>พุธ</asp:ListItem>
                                            <asp:ListItem>พฤหัสบดี</asp:ListItem>
                                            <asp:ListItem>ศุกร์</asp:ListItem>
                                            <asp:ListItem>เสาร์</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlDue_Billing_Day_of_Month" runat="server" CssClass="form-control">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>วันสุดท้ายของเดือน</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtDue_Billing_Day_Other" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:TextBox ID="txtDue_Billing" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<ajaxToolkit:CalendarExtender ID="cldEtxtDue_Billing_Day_Other"
                                            runat="server"
                                            TargetControlID="txtDue_Billing_Day_Other"
                                            PopupButtonID="cldEtxtDue_Billing_Day_Other" />--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เครดิต:</label>

                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlCredit_Term" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                        </asp:DropDownList>
                                    </div>
                                    <%--<label class="col-md-2 control-label">วงเงินเครดิต(ต้องไม่มีทศนิยม):</label>--%>
                                    <%--<div class="col-md-4">
                                        <asp:textbox id="txtCredit_Limit" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>--%>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">ช่วงเวลารับสินค้า:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlReceiveProduct_Hour" runat="server">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                        </asp:DropDownList>
                                        <label>:</label>
                                        <asp:DropDownList ID="ddlReceiveProduct_Minute" runat="server">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList>
                                        <label>ถึง</label>
                                        <asp:DropDownList ID="ddlReceiveToProduct_Hour" runat="server">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                        </asp:DropDownList>
                                        <label>:</label>
                                        <asp:DropDownList ID="ddlReceiveToProduct_Minute" runat="server">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ชื่อร้านค้า:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtShopName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <%--<div class="form-group">
                                    <label class="col-md-2 control-label">ราคาขาย:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlPrice_Group_ID" runat="server" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                            <%--onkeydown="return (event.keyCode!=13)"--%>
                            <div class="col-md-12 text-center" >
                                <asp:HiddenField ID="hdnCV_CODE" runat="server" />
                                <asp:HiddenField ID="hdnAgent_Name" runat="server" />
                                <asp:HiddenField ID="btnSaveMode" runat="server" />
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;"  />
                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12 text-center">
                                <hr />
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

