<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master"
    AutoEventWireup="true" uiculture="th" culture="th-TH"
    CodeFile="AgentList.aspx.cs" Inherits="Views_AgentList" %>

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

        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }

        function validateFloatKeyPress(el, evt) {
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
                            <span class="title" style="font-size: 18px">Agent List</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">CV CODE:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchCV_CODE" runat="server" CssClass="form-control" OnTextChanged="txtSearchCV_CODE_TextChanged"  AutoPostBack ="true"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">ชื่อที่ใช้ทำสัญญากับบริษัท:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchPrefix_ID" runat="server" CssClass="form-control" datatextfield="AgentName" datavaluefield="CV_Code" onselectedindexchanged="ddlSearchPrefix_ID_SelectedIndexChanged" autopostback="true">
                                        </asp:DropDownList>
                                        <%--<asp:textbox id="txtSearchPrefix_ID" runat="server" CssClass="form-control" datatextfield="Prefix_ID" datavaluefield="CV_Code"></asp:textbox>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ประเภทเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchAgentType" runat="server" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                            <%-- <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="นมสด"></asp:ListItem>
                                            <asp:ListItem Text="ไพเกน"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">พื้นที่สัมปทาน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchConcessionArea" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">พนักงานขายผู้ดูแล:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchSP" runat="server" CssClass="form-control" datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ผู้จัดการแผนก:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchSM" runat="server" CssClass="form-control" datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ผู้จัดการฝ่าย:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchDM" runat="server" CssClass="form-control" datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ผู้จัดการทั่วไป:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchGM" runat="server" CssClass="form-control" datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ผู้ช่วยกรรมการผู้จัดการ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchAPV" runat="server" CssClass="form-control" datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ภูมิภาค:</label>
                                    <div class="col-md-4">
                                        <asp:DropdownList runat="server" id="ddlSearchRegion" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                        </asp:DropdownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">สถานะ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="ดำเนินธุรกิจอยู่"></asp:ListItem>
                                            <asp:ListItem Text="ยกเลิกกิจการ"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ระดับเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchGrade" runat="server" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                            <%-- <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="Platinum"></asp:ListItem>
                                            <asp:ListItem Text="A+"></asp:ListItem>
                                            <asp:ListItem Text="Platinum"></asp:ListItem>
                                            <asp:ListItem Text="A+"></asp:ListItem>
                                            <asp:ListItem Text="A"></asp:ListItem>
                                            <asp:ListItem Text="B"></asp:ListItem>
                                            <asp:ListItem Text="C"></asp:ListItem>
                                            <asp:ListItem Text="D"></asp:ListItem>
                                            <asp:ListItem Text="E"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="ButtonCreateNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="ButtonCreateNew_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
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
                                    <asp:GridView ID="grdAgent"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataKeyNames=""
                                        ShowFooter="false" showheaderwhenempty="false"
                                        CellPadding="0" ForeColor="#333333"
                                        CssClass="table table-striped table-bordered table-condensed"  AllowPaging="true" PageSize="10"
                                        OnRowCommand="grdAgent_RowCommand"  OnDataBound="grdAgent_DataBound" OnRowDataBound="grdAgent_RowDataBound">
                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                        <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ"
                                                            CommandArgument='<%# Eval("CV_CODE") %>' CommandName="_Delete" ></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="CV Code" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_CV_CODE" runat="server" CssClass="btn btn-link" Text='<%# Eval("CV_CODE") %>'
                                                        CommandArgument='<%# Eval("CV_CODE") %>'
                                                        CommandName="View"
                                                        style="font-weight: bold; text-decoration: underline; text-align: left"></asp:LinkButton>
                                                </ItemTemplate>
                                                <itemstyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อที่ใช้ทำสัญญากับบริษัท">
                                                <ItemTemplate>
                                                    <asp:Label id="Label_First_Name" runat="server" Text='<%# Eval("AgentName") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภทเอเยนต์" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="Label_Agent_Type_ID" runat="server" Text='<%# Eval("Agent_Type_ID") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="พื้นที่สัมปทาน" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_Concession_Area" runat="server" Text='<%# Eval("Concession_Area") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="พนักงานขายผู้ดูแล" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_SP" runat="server" Text='<%# Eval("SD_ID_FullName") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ผู้จัดการแผนก" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_SM" runat="server" Text='<%# Eval("SM_ID_FullName") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ผู้จัดการฝ่าย" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_DM" runat="server" Text='<%# Eval("DM_ID_FullName") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ผู้จัดการทั่วไป" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_GM" runat="server" style="color: blue" Text='<%# Eval("GM_ID_FullName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ผู้ช่วยกรรมการผู้จัดการ" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_AVP" runat="server" style="color: blue" Text='<%# Eval("APV_ID_FullName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถานะ">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_Status" runat="server" Text='<%#Eval("Status")  %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ภูมิภาค">
                                                <ItemTemplate>
                                                    <asp:Label id="lblLocation_Region" runat="server" style="color: blue" Text='<%# Eval("Location_Region") %>' Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ระดับเอเยนต์" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lblGrade" runat="server" Text='<%# Eval("Grade") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เบอร์โทรศัพท์" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lblHome_Phone_No" runat="server" Text='<%# Eval("Home_Phone_No") %>' style="color: blue" Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
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

            <asp:Panel ID="pnlForm" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">เอเยนต์</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                            <asp:Label ID="LabelPageHeader" runat="server" Text="สร้างข้อมูลเอเยนต์"></asp:Label>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="col-md-12">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        DisplayMode="BulletList"
                                        ValidationGroup="Validation"
                                        forecolor="red"
                                        font-bold="true" />
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">CV Code:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvtxt_CV_Code"
                                            runat="server"
                                            ControlToValidate="txt_CV_Code"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ CV Code"
                                            ValidationGroup="Validation">
                                        </asp:RequiredFieldValidator>
                                        <asp:textbox id="txt_CV_Code" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;" maxlength="6" textmode="number" ontextchanged="txt_CV_Code_TextChanged" autopostback="true"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อที่ใช้ทำสัญญากับบริษัท:</label>
                                    <div class="col-md-4">

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator99"
                                            runat="server"
                                            ControlToValidate="ddlPrefix_ID"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ ชื่อที่ใช้ทำสัญญากับบริษัท"
                                            ValidationGroup="Validation" initialvalue="">
                                        </asp:RequiredFieldValidator>

                                        <%--<asp:textbox id="txtPrefix_ID" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;"></asp:textbox>--%>

                                        <asp:DropDownList ID="ddlPrefix_ID" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;" datatextfield="VALUE" datavaluefield="KEY">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="rfvtxt_First_Name"
                                        runat="server"
                                        ControlToValidate="txt_First_Name"
                                        Display="None"
                                        ErrorMessage="ต้องระบุ ชื่อเอเยนต์"
                                        ValidationGroup="Validation">
                                    </asp:RequiredFieldValidator>
                                    <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txt_First_Name" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">นามสกุล:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtLast_Name" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ประเภทเอเยนต์:</label>

                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvddlAgent_Type_ID"
                                            runat="server"
                                            ControlToValidate="ddlAgent_Type_ID"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ ประเภทเอเยนต์"
                                            ValidationGroup="Validation" initialvalue="">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlAgent_Type_ID" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;" datatextfield="VALUE" datavaluefield="KEY">
                                            <%-- <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>นมสด</asp:ListItem>
                                            <asp:ListItem>ไพเกน</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtHome_Phone_No" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขประจำตัวผู้เสียภาษี:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtTax_ID" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">เบอร์มือถือ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtMobile" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="rfvtxtEmail"
                                        runat="server"
                                        ControlToValidate="txtEmail"
                                        Display="None"
                                        ErrorMessage="ต้องระบุ Email"
                                        ValidationGroup="Validation">
                                    </asp:RequiredFieldValidator>
                                    <label class="col-md-2 control-label">Email:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtEmail" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">Fax:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtFax" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">พื่นที่สัมปทาน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtConcession_Area" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>บุคคลที่มีสิทธิในกิจการ</h4>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtOwner_First_Name" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">นามสกุล:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtOwner_Last_Name" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtOwner_Phone_No1" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">เบอร์โทรศัพท์ 2:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtOwner_Phone_No2" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>ผู้ติดต่อ</h4>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtContact_First_Name" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">นามสกุล:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtContact_Last_Name" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtContact_Phone_No1" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">เบอร์โทรศัพท์ 2:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtContact_Phone_No2" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>พนักงานผู้ดูแล</h4>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">พนักงานขาย:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSD_ID" runat="server" CssClass="form-control"
                                            datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ผู้จัดการแผนก:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSM_ID" runat="server" CssClass="form-control"
                                            datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ผู้จัดการฝ่าย:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlDM_ID" runat="server" CssClass="form-control"
                                            datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ผู้จัดการทั่วไป:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-control"
                                            datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ผู้ช่วยกรรมการผู้จัดการ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlAVP" runat="server" CssClass="form-control"
                                            datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>ที่อยู่ศูนย์</h4>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvtxtLocation_House_No"
                                            runat="server"
                                            ControlToValidate="txtLocation_House_No"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ ที่อยู่ศูนย์ เลขที่"
                                            ValidationGroup="Validation"> </asp:RequiredFieldValidator>
                                        <asp:textbox id="txtLocation_House_No" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">หมู่บ้าน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtLocation_Village" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">หมู่ที่:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtLocation_Village_No" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">ตรอก/ซอย:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtLocation_Alley" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ถนน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtLocation_Road" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">จังหวัด:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvtxtLocation_Province"
                                            runat="server"
                                            ControlToValidate="ddlLocation_Province"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ ที่อยู่ศูนย์ จังหวัด" initialvalue="==ระบุ=="
                                            ValidationGroup="Validation"> </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlLocation_Province" runat="server" autopostback="true" onselectedindexchanged="ddlLocation_Province_SelectedIndexChanged"
                                            CssClass="form-control" style="border-left: 4px solid #ed1c24;" DataTextField="Province">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">อำเภอ/เขต:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rftxtLocation_District"
                                            runat="server"
                                            ControlToValidate="ddlLocation_District"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ ที่อยู่ศูนย์ อำเภอ/เขต" initialvalue="==ระบุ=="
                                            ValidationGroup="Validation"> </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlLocation_District" runat="server" autopostback="true" onselectedindexchanged="ddlLocation_District_SelectedIndexChanged"
                                            CssClass="form-control" style="border-left: 4px solid #ed1c24;" DataTextField="District">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">ตำบล/แขวง:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvtxtLocation_Sub_district"
                                            runat="server"
                                            ControlToValidate="ddlLocation_Sub_district"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ ที่อยู่ศูนย์ ตำบล/แขวง" initialvalue="==ระบุ=="
                                            ValidationGroup="Validation"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlLocation_Sub_district" runat="server" autopostback="true"
                                            CssClass="form-control" style="border-left: 4px solid #ed1c24;" DataTextField="Sub_district" onselectedindexchanged="ddlLocation_Sub_district_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ภูมิภาค:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvddlLocation_Region"
                                            runat="server"
                                            ControlToValidate="ddlLocation_Region"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ ที่อยู่ศูนย์ ภูมิภาค"
                                            ValidationGroup="Validation"> </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlLocation_Region" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;" datatextfield="VALUE" datavaluefield="KEY">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">รหัสไปรษณีย์:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtLocation_Postal_ID" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>ที่อยู่ในการออกใบแจ้งหนี้
                                        <asp:Button ID="btnCopyAddress" runat="server" class="btn btn-primary" onclick="btnCopyAddress_Click" Text="ใช้ที่อยู่ตามที่อยู่ศูนย์" />
                                    </h4>
                                </div>
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="rfvtxtInvoice_Village_No"
                                        runat="server"
                                        ControlToValidate="txtInvoice_House_No"
                                        Display="None"
                                        ErrorMessage="ต้องระบุ ที่อยู่ในการออกใบแจ้งหนี้ เลขที่"
                                        ValidationGroup="Validation"> </asp:RequiredFieldValidator>
                                    <label class="col-md-2 control-label">เลขที่:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_House_No" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">หมู่บ้าน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_Village" runat="server"
                                            CssClass="form-control">
                                        </asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">หมู่ที่:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_Village_No" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">ตรอก/ซอย:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_Alley" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">ถนน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_Road" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">จังหวัด:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvtxtInvoice_Province"
                                            runat="server"
                                            ControlToValidate="ddlInvoice_Province"
                                            Display="None" initialvalue="==ระบุ=="
                                            ErrorMessage="ต้องระบุ ที่อยู่ในการออกใบแจ้งหนี้ จังหวัด"
                                            ValidationGroup="Validation"> </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlInvoice_Province" runat="server" autopostback="true" onselectedindexchanged="ddlInvoice_Province_SelectedIndexChanged"
                                            CssClass="form-control" style="border-left: 4px solid #ed1c24;" DataTextField="Province">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">อำเภอ/เขต:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvtxtInvoice_District"
                                            runat="server"
                                            ControlToValidate="ddlInvoice_District"
                                            Display="None" initialvalue="==ระบุ=="
                                            ErrorMessage="ต้องระบุ ที่อยู่ในการออกใบแจ้งหนี้ อำเภอ/เขต"
                                            ValidationGroup="Validation"> </asp:RequiredFieldValidator>

                                        <asp:DropDownList ID="ddlInvoice_District" runat="server" autopostback="true" onselectedindexchanged="ddlInvoice_District_SelectedIndexChanged"
                                            CssClass="form-control" style="border-left: 4px solid #ed1c24;" DataTextField="District">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-4">
                                    </div>
                                    <label class="col-md-2 control-label">ตำบล/แขวง:</label>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="rfvtxtInvoice_Sub_district"
                                            runat="server" initialvalue="==ระบุ=="
                                            ControlToValidate="ddlInvoice_Sub_district"
                                            Display="None"
                                            ErrorMessage="ต้องระบุ ที่อยู่ในการออกใบแจ้งหนี้ ตำบล/แขวง"
                                            ValidationGroup="Validation"> </asp:RequiredFieldValidator>

                                        <asp:DropDownList ID="ddlInvoice_Sub_district" runat="server" autopostback="true"
                                            CssClass="form-control" style="border-left: 4px solid #ed1c24;" DataTextField="Sub_district" onselectedindexchanged="ddlInvoice_Sub_district_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ภูมิภาค:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlInvoice_Region" runat="server" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">รหัสไปรษณีย์:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtInvoice_Postal_ID" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>สถานะภาพเอเยนต์</h4>
                                </div>
                                <div class="form-group">
                                    <%--  <div class="form-inline">--%>
                                    <label class="col-md-2 control-label">วันที่เริ่มเป็น agent:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtStart_Effective_Date" runat="server" CssClass="form-control"></asp:textbox>
                                        <%-- <asp:ImageButton runat="Server" ID="imgBtxtStart_Effective_Date"
                                                ImageUrl="/images/Calendar.png" width="26" Height="22"
                                                AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldEtxtStart_Effective_Date"
                                            runat="server"
                                            TargetControlID="txtStart_Effective_Date"
                                            PopupButtonID="cldEtxtStart_Effective_Date" />

                                    </div>
                                    <label class="col-md-2 control-label">วันที่ได้สั่งสินค้าครั้งแรก:</label>
                                    <div class="col-md-4">


                                        <asp:textbox id="txtFirst_Order_Date" runat="server" CssClass="form-control"></asp:textbox>
                                        <%--<asp:ImageButton runat="Server" ID="imgBtxtFirst_Order_Date"
                                                ImageUrl="/images/Calendar.png" width="26" Height="22"
                                                AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldEtxtFirst_Order_Date"
                                            runat="server"
                                            TargetControlID="txtFirst_Order_Date"
                                            PopupButtonID="cldEtxtFirst_Order_Date" />

                                    </div>
                                    <%--  </div>--%>
                                </div>
                                <div class="form-group">
                                    <%--   <div class="form-inline">--%>
                                    <label class="col-md-2 control-label">สถานะเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <%--<asp:textbox id="txtStatus" runat="server" CssClass="form-control"></asp:textbox>--%>

                                        <asp:RequiredFieldValidator ID="rfvddlStatus"
                                            runat="server"
                                            ControlToValidate="ddlStatus"
                                            Display="None" initialvalue="==ระบุ=="
                                            ErrorMessage="ต้องระบุ สถานะเอเยนต์"
                                            ValidationGroup="Validation"> </asp:RequiredFieldValidator>

                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>ดำเนินธุรกิจอยู่</asp:ListItem>
                                            <asp:ListItem>ยกเลิกกิจการ</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่เลิกดำเนินการ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtGo_out_of_business_Date" runat="server" CssClass="form-control"></asp:textbox>
                                        <%-- <asp:ImageButton runat="Server" ID="imgBtxtGo_out_of_business_Date"
                                                ImageUrl="/images/Calendar.png" width="26" Height="22"
                                                AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldEtxtGo_out_of_business_Date"
                                            runat="server"
                                            TargetControlID="txtGo_out_of_business_Date"
                                            PopupButtonID="cldEtxtGo_out_of_business_Date" />
                                    </div>
                                    <%--  </div>--%>
                                </div>

                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>เอกสารประกอบการเป็นเอเยนต์</h4>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>

                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk01" runat="server" text="ใบสมัคร" CssClass="cb" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk02" runat="server" text="รูปถ่าย" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk03" runat="server" text="ทะเบียนสมรส" CssClass="cb"/>
                                    </div>

                                    <label class="col-md-2 control-label"></label>

                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk04" runat="server" text="สำเนาทะเบียนบ้าน" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk05" runat="server" text="ใบเปลี่ยนชื่อ (ถ้ามี)" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk06" runat="server" text="สำเนาบัตรประชาชน" CssClass="cb"/>
                                    </div>

                                    <label class="col-md-2 control-label"></label>

                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk07" runat="server" text="หนังสือรับรองบริษัท" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk08" runat="server" text="สำเนาผู้เสียภาษี" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk09" runat="server" text="ใบทะเบียนพาณิชย์" CssClass="cb"/>
                                    </div>

                                    <label class="col-md-2 control-label"></label>

                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk10" runat="server" text="ภพ.20" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chk11" runat="server" text="อื่นๆ (ระบุ)" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:textbox id="txtOther_Document" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>


                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>เงื่อนไขตามมาตรฐาน</h4>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">จำนวนลังที่มัดจำใบเล็ก:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSmall_Case" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">จำนวนลังที่มัดจำใบใหญ่:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtLarge_Case" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">จำนวนเงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtPledge_Amount" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ขนาดห้องเย็น:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtRoom_Size" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <label class="col-md-2 control-label">เงินสดประกันสินค้า:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtCash_Deposit" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">BG:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtBank_Guarantee" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">ธนาคาร:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                            <%--<asp:ListItem Text="ธนาคารกรุงเทพ จำกัด (มหาขน)"></asp:ListItem>
                                    <asp:ListItem Text="ธนาคารกสิกรไทย จำกัด (มหาชน)"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เงื่อนไขการชำระเงิน:</label>
                                    <div class="col-md-4">

                                        <asp:DropDownList ID="ddlTerm_of_payment" runat="server" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                        </asp:DropDownList>


                                    </div>
                                    <label class="col-md-2 control-label">หมายเหตุ:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtRemarks" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>



                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>สินค้าที่ได้รับสิทธิให้ขาย</h4>
                                </div>


                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>

                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkTab1" runat="server" text="นมสดพาสเจอร์ไรส์" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkTab2" runat="server" text="นมเปรี้ยว" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkTab3" runat="server" text="โยเกิร์ตเมจิ" CssClass="cb"/>
                                    </div>

                                    <label class="col-md-2 control-label"></label>

                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkTab4" runat="server" text="นมเปรี้ยวไพเกน" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkTab5" runat="server" text="อื่นๆ" CssClass="cb"/>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>

                                <%-- <div class="form-group">

                                    <label class="col-md-2 control-label">ราคาขาย:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtPrice_Group_ID" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>

                                </div>--%>

                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <h4>ระดับเอเยนต์</h4>
                                </div>

                                <div class="form-group">

                                    <label class="col-md-2 control-label">ระดับเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control" datatextfield="VALUE" datavaluefield="KEY">
                                            <%-- <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>Platinum</asp:ListItem>
                                            <asp:ListItem>A+</asp:ListItem>
                                            <asp:ListItem>A</asp:ListItem>
                                            <asp:ListItem>B</asp:ListItem>
                                            <asp:ListItem>C</asp:ListItem>
                                            <asp:ListItem>D</asp:ListItem>
                                            <asp:ListItem>E</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label">ระดับเอเยนต์  เริ่มวันที่:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtGrade_Effective_Date" runat="server" CssClass="form-control"></asp:textbox>
                                        <%-- <asp:ImageButton runat="Server" ID="imgBEffective_Date"
                                            ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText=""
                                            CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldEEffective_Date" PopupPosition="TopLeft"
                                            runat="server"
                                            TargetControlID="txtGrade_Effective_Date"
                                            PopupButtonID="cldEEffective_Date" />
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>
                                <div class="col-md-12 text-center">

                                    <asp:hiddenfield id="btnSaveMode" runat="server" />

                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="ButtonSave_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                    <asp:Button ID="ButtonCancelSave" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="ButtonCancelSave_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
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
</asp:Content>

