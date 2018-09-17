<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="CreateAgentUser.aspx.cs" Inherits="Views_CreateAgentUser" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="/scripts/AjaxRequestXML.js"></script>

    <script type="text/javascript">

        function callAjax(value, target) {
            if (encodeURIComponent) {
                var params = {
                    "value": value,
                    "target": target
                };
                return (new AjaxRequestXML()).post("/scripts/valid-date.xml", params);
            }
            return false;
        }

    </script>

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
    </script>

    <script>
        $(document).ready(function () {

            var navListItems = $('div.setup-panel div a'),
                    allWells = $('.setup-content'),
                    allNextBtn = $('.nextBtn');

            allWells.hide();

            navListItems.click(function (e) {
                e.preventDefault();
                var $target = $($(this).attr('href')),
                    $item = $(this);
                if (!$item.hasClass('disabled')) {
                    //navListItems.removeCflass('btn-primary').addClass('btn-default');
                    if ($item.attr('id') != $(navListItems[1]).attr('id')) {
                        $(navListItems[1]).removeClass('btn-primary').addClass('btn-success');
                    }
                    //$('#item3').addClass('btn-success');
                    $item.addClass('btn-primary');
                    allWells.hide();
                    $target.show();
                    $target.find('input:eq(0)').focus();
                }
            });

            allNextBtn.click(function () {
                var curStep = $(this).closest(".setup-content"),
                    curStepBtn = curStep.attr("id"),
                    nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
                    curInputs = curStep.find("input[type='text'],input[type='url'], input[type='password'], input[type='email']"),
                    isValid = true;

                $(".form-group").removeClass("has-error");
                for (var i = 0; i < curInputs.length; i++) {
                    if (!curInputs[i].validity.valid) {
                        isValid = false;
                        $(curInputs[i]).closest(".form-group").addClass("has-error");
                    }
                }

                if (isValid)
                    nextStepWizard.removeAttr('disabled').trigger('click');
            });

            $('div.setup-panel div a.btn-primary').trigger('click');
        });

        function postBackByObject() {
            __doPostBack("", "");

        }

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

        function validateFloatKeyPress2(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            //if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
            //}

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


        function isValidDate(sText) {
            var reDate = /(?:0[1-9]|[12][0-9]|3[01])\/(?:0[1-9]|1[0-2])\/(?:19|20\d{2})/;
            return reDate.test(sText);
        }
        function validate() {
            var oInput1 = document.getElementById("txtJoin_Date");
            if (isValidDate(oInput1.value)) {
                alert("Valid");
            } else {
                alert("Invalid!");
            }

        }


    </script>

    <style>
        .stepwizard-step p {
            margin-top: 10px;
        }

        .stepwizard-row {
            display: table-row;
        }

        .stepwizard {
            display: table;
            width: 100%;
            position: relative;
        }

        .stepwizard-step button[disabled] {
            opacity: 1 !important;
            filter: alpha(opacity=100) !important;
        }

        .stepwizard-row:before {
            top: 14px;
            bottom: 0;
            position: absolute;
            content: " ";
            width: 100%;
            height: 1px;
            background-color: #ccc;
            z-order: 0;
        }

        .stepwizard-step {
            display: table-cell;
            text-align: center;
            position: relative;
        }

        .btn-circle {
            width: 56px;
            height: 56px;
            text-align: center;
            padding: 12px 0;
            font-size: 20px;
            line-height: 1.428571429;
            border-radius: 35px;
            margin-top: -14px;
            border: solid 3px #ccc !important;
            opacity: 1 !important;
            -webkit-box-shadow: inset 0px 0px 0px 3px #fff !important;
            -moz-box-shadow: inset 0px 0px 0px 3px #fff !important;
            -o-box-shadow: inset 0px 0px 0px 3px #fff !important;
            -ms-box-shadow: inset 0px 0px 0px 3px #fff !important;
            box-shadow: inset 0px 0px 0px 3px #fff !important;
            backgournd-color: #428bca;
        }
    </style>

    <style>
        .cb label {
            margin-left: 20px;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>


            <asp:Panel ID="pnlGrid" Visible="true" runat="server">
                <div class="container-full-content">
                    <div class="row"  >

                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ค้นหาพนักงานเอเยนต์</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-user" style="font-size: 18px"></span>
                        </div>

                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <label class="col-md-2 control-label">CV Code:</label>

                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchCVCode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSearchCVCode_TextChanged"></asp:TextBox>

                                    </div>

                                    <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>


                                    <div class="col-md-4">
                                        <%--<asp:textbox id="txtSearchAgentName" runat="server" CssClass="form-control"></asp:textbox>--%>

                                        <asp:DropDownList ID="ddlSearchAgentName" runat="server" CssClass="form-control" DataTextField="AgentName" DataValueField="CV_Code" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCVCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <%--<asp:DropDownList ID="ddlSearchFirstName" runat="server" CssClass="form-control" datatextfield="FullName" datavaluefield="User_ID">
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtSearchFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ตำแหน่ง:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchPosition" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เริ่มงาน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="imgBSearchJoinDate" ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldESearchJoinDate" runat="server"
                                            TargetControlID="txtSearchStartDate" PopupButtonID="cldESearchJoinDate" />
                                    </div>
                                    <label class="col-md-2 control-label">สิทธิผู้ใช้งาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchRole" runat="server" CssClass="form-control" DataTextField="ROLE_NAME" DataValueField="ROLE_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <label class="col-md-2 control-label">รหัสพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchUser_ID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">สถานะ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem>==ระบุ==</asp:ListItem>
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>In active</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <br />
                                    <br />
                                </div>

                            </div>
                        </div>

                        <div class="col-md-12">
                            <br />
                            <br />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
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


                        </div>
                        <%--</div>--%>
                    </div>
                    <div class="row">

                        <div class="row">
                            <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                    <center>ไม่พบข้อมูล</center>
                                </div>
                            </asp:Panel>
                            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px;  background: #fbfafa ">
                                <div class="container-fluid panel-container" style="overflow: auto">
                                    <%--<div style="overflow: auto">--%>


                                    <asp:GridView ID="GridViewUser"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataKeyNames=""
                                        ShowFooter="false"
                                        CellPadding="0"
                                        ForeColor="#333333"
                                        OnRowCommand="GridViewUser_RowCommand"
                                        CssClass="table table-striped table-bordered table-condensed"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ShowHeaderWhenEmpty="false" AllowPaging="true" PageSize="10" OnDataBound="GridViewUser_DataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('คุณต้องการลบข้อมูล?')">
                                                        <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ" CommandArgument='<%# Eval("User_ID") %>' CommandName="_Delete"></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbEdit" runat="Server" CssClass="btn btn-mini btn-warning" Text="แก้ไข" CommandArgument='<%# Eval("User_ID") %>' CommandName="_Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CV" ShowHeader="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lnkBCVCode" runat="server" CssClass="btn btn-link" Text='<%# Eval("CV_Code") %>'
                                                    CommandArgument='<%# Eval("CV_Code") %>' CommandName="View_CV_Code" style="font-weight: bold; text-decoration: underline">
                                                </asp:Label>--%>

                                                    <asp:Label ID="lnkBCVCode" runat="server" Text='<%# Eval("CV_Code") %>' Style="color: blue"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อเอเยนต์" ShowHeader="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%--     <asp:LinkButton ID="lnkBFirst_Name" runat="server" CssClass="btn btn-link" Text='<%# Eval("AgentName") %>'
                                                    CommandArgument='<%# Eval("AgentName") %>' CommandName="View_CV_Code" style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                                    --%>
                                                    <asp:Label ID="lnkBFirst_Name" runat="server" Text='<%# Eval("AgentName") %>' Style="color: blue" Width="150px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อพนักงาน" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbl_First_Name" runat="server" CssClass="btn btn-link" Text='<%# Eval("FullName") %>' CommandArgument='<%# Eval("User_ID") %>' CommandName="View_User_ID" Style="font-weight: bold; text-decoration: underline" Width="170px"></asp:LinkButton>

                                                    <%--<asp:Label id="lbl_First_Name" runat="server" Text='<%# Eval("First_Name") %>' style="color: blue"></asp:Label>--%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ตำแหน่ง">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Position" runat="server" Text='<%# Eval("Position") %>' Style="color: blue" Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่เริ่มงาน" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Join_Date" runat="server" Text='<%# Eval("Join_Date","{0:dd/MM/yyyy}") %>' Style="color: blue" Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สิทธิผู้ใช้งาน">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Role" runat="server" Text='<%# Eval("Role_ID") %>' Style="color: blue" Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถานะ" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status") %>' Style="color: blue" Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เบอร์โทรศัพท์">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Mobile" runat="server" Text='<%# Eval("Home_Phone_No") %>' Style="color: blue;" Width="150px"></asp:Label>
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
                                    <%--</div>--%>
                                    <asp:HiddenField ID="hdfUser_ID" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <%--  Agent--%>

            <asp:Panel ID="pnlForm" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">

                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">พนักงานเอเยนต์</span>&nbsp;&nbsp;
                                     <span class="glyphicon glyphicon-user" style="font-size: 18px"></span>
                            <asp:Label ID="LabelPageHeader" runat="server" Text="แก้ไขข้อมูล User"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px;  background: #fbfafa " >
                            <div class="container-fluid panel-container" style="overflow: auto">
                                <div class="form-horizontal">
                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                            DisplayMode="BulletList"
                                            ValidationGroup="AgentValidation"
                                            ForeColor="red"
                                            Font-Bold="true" />
                                    </div>
                                    <div class="col-md-12">
                                        <h4>ข้อมูลทั่วไป</h4>
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">CV:</label>
                                            <div class="col-md-4">

                                                <%-- <asp:DropDownList ID="ddlCV_Code" runat="server" CssClass="form-control" style="border-left: 4px solid #ed1c24;"
                                                autopostback="true" onselectedindexchanged="ddlCV_Code_SelectedIndexChanged" datatextfield="CV_Code">
                                            </asp:DropDownList>--%>

                                                <asp:RequiredFieldValidator ID="rfvtxtCV_Code"
                                                    runat="server"
                                                    ControlToValidate="txtCV_Code"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ CV CODE"
                                                    ValidationGroup="AgentValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCV_Code" runat="server" CssClass="form-control" OnTextChanged="txtCV_Code_TextChanged" AutoPostBack="true" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtAgent_Name" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">รหัสพนักงาน:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtUser_ID" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">คำนำหน้าชื่อ:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlTitle_ID"
                                                    runat="server"
                                                    ControlToValidate="ddlTitle_ID"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ คำนำหน้านาม"
                                                    ValidationGroup="AgentValidation" InitialValue="">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlTitle_ID" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="VALUE" DataValueField="KEY">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtFirst_Name"
                                                    runat="server"
                                                    ControlToValidate="txtFirst_Name"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ชื่อพนักงาน"
                                                    ValidationGroup="AgentValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtFirst_Name" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">นามสกุล:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtLast_Name"
                                                    runat="server"
                                                    ControlToValidate="txtLast_Name"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ นามสกุล"
                                                    ValidationGroup="AgentValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtLast_Name" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtHome_Phone_No" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">เบอร์มือถือ:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">ตำแหน่ง:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rqvddlPosition"
                                                    runat="server"
                                                    ControlToValidate="ddlPosition"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ตำแหน่ง"
                                                    ValidationGroup="AgentValidation" InitialValue="">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged" AutoPostBack="true" Style="border-left: 4px solid #ed1c24;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">วัน/เดือน/ปี เกิด (พ.ศ.):</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtBirthdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <%--<asp:ImageButton runat="Server" ID="imgBBirthdate" ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText="" CausesValidation="False" />--%>

                                                <ajaxToolkit:CalendarExtender ID="cldEBirthdate" runat="server" TargetControlID="txtBirthdate" PopupButtonID="cldEBirthdate" />

                                            </div>
                                            <label class="col-md-2 control-label">หมายเลขบัตรประชาชน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtID_Card_No"
                                                    runat="server"
                                                    ControlToValidate="txtID_Card_No"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ หมายเลขบัตรประชาชน"
                                                    ValidationGroup="AgentValidation"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtID_Card_No" runat="server" OnTextChanged="txtID_Card_No_TextChanged" AutoPostBack="true" CssClass="form-control" Style="border-left: 4px solid #ed1c24;" MaxLength="13" onkeypress="return  validateFloatKeyPress(this,event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">สิทธิผู้ใช้งาน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlRole"
                                                    runat="server"
                                                    ControlToValidate="ddlRole"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ สิทธิผู้ใช้งาน"
                                                    ValidationGroup="AgentValidation" InitialValue="">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"
                                                    DataTextField="ROLE_NAME" DataValueField="ROLE_ID">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">ชื่อผู้ใช้งาน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtUser_Name"
                                                    runat="server"
                                                    ControlToValidate="txtUser_Name"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ชื่อผู้ใช้งาน"
                                                    ValidationGroup="AgentValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtUser_Name" runat="server" CssClass="form-control"
                                                    Style="border-left: 4px solid #ed1c24;" AutoCompleteType="disabled"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">รหัสผ่าน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtPassword"
                                                    runat="server"
                                                    ControlToValidate="txtPassword"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ รหัสผ่าน"
                                                    ValidationGroup="AgentValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtPassword" runat="server"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;"
                                                    AutoCompleteType="disabled"></asp:TextBox>

                                                <asp:HiddenField ID="hdfPasswrod" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">อีเมล์:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">สถานะการขออนุมัติ:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlApprove"
                                                    runat="server"
                                                    ControlToValidate="ddlApprove"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ สถานะการขออนุมัติ"
                                                    ValidationGroup="AgentValidation" InitialValue="">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlApprove" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                                    <%--<asp:ListItem>==ระบุ==</asp:ListItem>--%>
                                                    <asp:ListItem>รอการอนุมัติ</asp:ListItem>
                                                    <asp:ListItem>อนุมัติ</asp:ListItem>
                                                    <asp:ListItem>ไม่อนุมัติ</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">สถานะ:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlStatus"
                                                    runat="server"
                                                    ControlToValidate="ddlStatus"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ สถานะ"
                                                    ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                                    <asp:ListItem>==ระบุ==</asp:ListItem>
                                                    <asp:ListItem>Active</asp:ListItem>
                                                    <asp:ListItem>In active</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-2 control-label">แสดงกราฟ:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlShowDashborad" runat="server" CssClass="form-control">
                                                    <asp:ListItem>==ระบุ==</asp:ListItem>
                                                    <asp:ListItem>แสดง</asp:ListItem>
                                                    <asp:ListItem>ไม่แสดง</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">ข้อมูลรูท:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtRoute" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                    </div>
                                    <div class="col-md-12">
                                        <h4>ที่อยู่ตามบัตรประชาชน</h4>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">เลขที่:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtHome_House_No"
                                                    runat="server"
                                                    ControlToValidate="txtHome_House_No"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ที่อยู่ตามบัตรประชาชน เลขที่"
                                                    ValidationGroup="AgentValidation">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtHome_House_No" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">หมู่บ้าน:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtHome_Village" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">หมู่ที่:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtHome_Village_No" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">ตรอก/ซอย:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtHome_Alley" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">ถนน:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtHome_Road" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">จังหวัด:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlHome_Province"
                                                    runat="server"
                                                    ControlToValidate="ddlHome_Province"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ที่อยู่ตามบัตรประชาชน จังหวัด"
                                                    ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlHome_Province" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHome_Province_SelectedIndexChanged"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="Province">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-4">
                                            </div>
                                            <label class="col-md-2 control-label">อำเภอ/เขต:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlHome_District"
                                                    runat="server"
                                                    ControlToValidate="ddlHome_District"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ที่อยู่ตามบัตรประชาชน อำเภอ/เขต"
                                                    ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlHome_District" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHome_District_SelectedIndexChanged"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="District">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-4">
                                            </div>
                                            <label class="col-md-2 control-label">ตำบล/แขวง:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlHome_Sub_district"
                                                    runat="server"
                                                    ControlToValidate="ddlHome_Sub_district"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ที่อยู่ตามบัตรประชาชน ตำบล/แขวง"
                                                    ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlHome_Sub_district" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHome_Sub_district_SelectedIndexChanged"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="Sub_district">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-4">
                                            </div>
                                            <label class="col-md-2 control-label">รหัสไปรษณีย์:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtHome_Postal_ID" runat="server" CssClass="form-control" TextMode="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                    </div>
                                    <div class="col-md-12">
                                        <h4>ที่อยู่ปัจจุบัน
                                            <asp:Button ID="btnCopyAddress" class="btn btn-primary" runat="server" Text="ใช้ที่อยู่ตามบัตรประชาชน" OnClick="btnCopyAddress_Click" /></h4>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:RequiredFieldValidator ID="rfvtxtPresent_House_No"
                                                runat="server"
                                                ControlToValidate="txtPresent_House_No"
                                                Display="None"
                                                ErrorMessage="ต้องระบุ ที่อยู่ปัจจุบัน เลขที่"
                                                ValidationGroup="AgentValidation">
                                            </asp:RequiredFieldValidator>
                                            <label class="col-md-2 control-label">เลขที่:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPresent_House_No" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">หมู่บ้าน:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPresent_Village" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">หมู่ที่:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPresent_Village_No" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">ตรอก/ซอย:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPresent_Alley" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">ถนน:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPresent_Road" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-md-2 control-label">จังหวัด:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlShipment_Province"
                                                    runat="server"
                                                    ControlToValidate="ddlShipment_Province"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ที่อยู่ปัจจุบัน จังหวัด"
                                                    ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlShipment_Province" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShipment_Province_SelectedIndexChanged"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="Province">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-4">
                                            </div>
                                            <label class="col-md-2 control-label">อำเภอ/เขต:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlShipment_District"
                                                    runat="server"
                                                    ControlToValidate="ddlShipment_District"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ที่อยู่ปัจจุบัน อำเภอ/เขต"
                                                    ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlShipment_District" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShipment_District_SelectedIndexChanged"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="District">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-4">
                                            </div>
                                            <label class="col-md-2 control-label">ตำบล/แขวง:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlShipment_Sub_district"
                                                    runat="server"
                                                    ControlToValidate="ddlShipment_District"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ที่อยู่ปัจจุบัน ตำบล/แขวง"
                                                    ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>

                                                <asp:DropDownList ID="ddlShipment_Sub_district" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShipment_Sub_district_SelectedIndexChanged"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;" DataTextField="Sub_district">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-4">
                                            </div>
                                            <label class="col-md-2 control-label">รหัสไปรษณีย์:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPresent_Postal_ID" runat="server" CssClass="form-control" TextMode="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">วันที่เริ่มงาน (พ.ศ):</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvtxtJoin_Date"
                                                    runat="server"
                                                    ControlToValidate="txtJoin_Date"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ วันที่เริ่มงาน (พ.ศ)"
                                                    ValidationGroup="AgentValidation">
                                                </asp:RequiredFieldValidator>
                                                <%--<asp:TextBox ID="txtJoin_Date" runat="server" CssClass="form-control"></asp:TextBox>  // CssClass="form-control" Style="border-left: 4px solid #ed1c24;"--%>
                                                <asp:TextBox ID="txtJoin_Date" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;" onkeypress="return  validateFloatKeyPress2(this,event);"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cldtxtJoin_Date" runat="server"
                                                    TargetControlID="txtJoin_Date" PopupButtonID="cldtxtJoin_Date" />
                                            </div>

                                            <label class="col-md-2 control-label">วันที่ลาออก (พ.ศ):</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtResign_Date" runat="server" CssClass="form-control" onkeypress="return  validateFloatKeyPress2(this,event);"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cldtxtResign_Date" runat="server"
                                                    TargetControlID="txtResign_Date" PopupButtonID="cldtxtResign_Date" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label">ประเภทการชำระเงิน:</label>
                                            <div class="col-md-4">
                                                <asp:RequiredFieldValidator ID="rfvddlPayment_Type"
                                                    runat="server"
                                                    ControlToValidate="ddlPayment_Type"
                                                    Display="None"
                                                    ErrorMessage="ต้องระบุ ประเภทการชำระเงิน"
                                                    ValidationGroup="AgentValidation" InitialValue="==ระบุ==">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlPayment_Type" runat="server" CssClass="form-control" DataTextField="VALUE" DataValueField="KEY" Style="border-left: 4px solid #ed1c24;">
                                                    <asp:ListItem>==ระบุ==</asp:ListItem>
                                                    <asp:ListItem>เงินสด</asp:ListItem>
                                                    <asp:ListItem>เช็ค</asp:ListItem>
                                                    <asp:ListItem>โอน</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-2 control-label">เครดิต:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCredit_Term" runat="server" CssClass="form-control" TextMode="Number" MaxLength="4"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                    </div>
                                    <div class="col-md-12">
                                        <h4>เอกสารประกอบการเป็นพนักงาน</h4>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-8">
                                                <asp:CheckBox ID="chkApplied_1" runat="server" Text="ใบสมัคร" CssClass="cb" />
                                                <asp:CheckBox ID="chkApplied_2" runat="server" Text="รูปถ่าย" CssClass="cb" />
                                                <asp:CheckBox ID="chkApplied_3" runat="server" Text="สำเนาทะเบียนบ้าน" CssClass="cb" />
                                                <asp:CheckBox ID="chkApplied_4" runat="server" Text="สำเนาบัตรประชาชน" CssClass="cb" />
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-12">
                                        <br />
                                    </div>--%>
                                    <div class="col-md-12">
                                        <h4 id="hinstallation" runat="server">ข้อมูลการผ่อนสินค้า</h4>
                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-12">
                                            <asp:GridView ID="GridViewDetailInstallation"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                ShowHeaderWhenEmpty="true"
                                                CellPadding="0"
                                                ForeColor="#333333"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="รายละเอียดสินค้าผ่อนเบื้องต้น">
                                                        <%--<EditItemTemplate>
                                                            <asp:TextBox id="txtEditInstallation_Detail" runat="server" Text='<%# Eval("Installation_Detail") %>' style="color: blue;"></asp:TextBox>
                                                        </EditItemTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemInstallation_Detail" runat="server" Text='<%# Eval("Installation_Detail") %>' Style="color: blue;" Width="170"></asp:Label>
                                                        </ItemTemplate>
                                                        <%--  <FooterTemplate>
                                                            <asp:TextBox ID="txtNewInstallation_Detail" runat="server"></asp:TextBox>
                                                        </FooterTemplate>--%>
                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ประเภทการผ่อน">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemInstallation_Type" runat="server" Text='<%# Eval("Installation_Type") %>' Style="color: blue;" Width="150"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่ทำรายการ">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemTransaction_Date" runat="server" Text='<%# Eval("Transaction_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" Width="90"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle Wrap="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่ครบกำหนด" ItemStyle-Wrap="true">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDue_Date" runat="server" Text='<%# Eval("Due_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" Width="90"></asp:Label>
                                                        </ItemTemplate>
                                                        <%-- <FooterTemplate>
                                                            <asp:TextBox ID="txtNewDue_Date" runat="server"></asp:TextBox>
                                                        </FooterTemplate>--%>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายละเอียดเพิ่มเติม">
                                                        <%--  <EditItemTemplate>
                                                            <asp:TextBox id="txtEditDescription" runat="server" Text='<%# Eval("Description") %>' style="color: blue;"></asp:TextBox>
                                                        </EditItemTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Description") %>' Style="color: blue;" Width="120"></asp:Label>
                                                        </ItemTemplate>
                                                        <%--  <FooterTemplate>
                                                            <asp:TextBox ID="txtNewDescription" runat="server"></asp:TextBox>
                                                        </FooterTemplate>--%>
                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด" ItemStyle-HorizontalAlign="Right">
                                                        <%--   <EditItemTemplate>
                                                            <asp:TextBox id="txtEditInstallation_Amount" runat="server" Text='<%# Eval("Installation_Amount") %>' style="color: blue;"></asp:TextBox>
                                                        </EditItemTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemInstallation_Amount" runat="server" Text='<%# Eval("Installation_Amount","{0:N2}") %>' Style="color: blue;" Width="120"></asp:Label>
                                                        </ItemTemplate>
                                                        <%-- <FooterTemplate>
                                                            <asp:TextBox ID="txtNewInstallation_Amount" runat="server"></asp:TextBox>
                                                        </FooterTemplate>--%>
                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชำระแล้ว" ItemStyle-HorizontalAlign="Right">
                                                        <%--   <EditItemTemplate>
                                                            <asp:TextBox id="txtEditPaid" runat="server" style="color: blue;"></asp:TextBox>
                                                        </EditItemTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemPaid" runat="server" Text='<%# Eval("Payment_Amount","{0:N2}") %>' Style="color: blue;" Width="120"></asp:Label>
                                                        </ItemTemplate>
                                                        <%--  <FooterTemplate>
                                                            <asp:TextBox ID="txtNewPaid" runat="server"></asp:TextBox>
                                                        </FooterTemplate>--%>
                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดคงเหลือ" ItemStyle-HorizontalAlign="Right">
                                                        <%-- <EditItemTemplate>
                                                            <asp:TextBox id="txtEditBalance_Amount" runat="server" Text='<%# Eval("Balance_Amount") %>' style="color: blue;" Enabled="false"></asp:TextBox>
                                                        </EditItemTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemBalance_Amount" runat="server" Text='<%# Eval("Balance_Amount","{0:N2}") %>' Style="color: blue;" Width="120"></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<FooterTemplate>
                                                            <asp:TextBox ID="txtNewBalance_Amount" runat="server" Text="0"></asp:TextBox>
                                                        </FooterTemplate>--%>
                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Installation_ID" ItemStyle-HorizontalAlign="Right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInstallation_ID" runat="server" Text='<%# Eval("Installation_ID") %>' Style="color: blue;" Width="120"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <%--    </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                    </div>
                                    <div class="col-md-12">
                                        <h4 id="hbenefit" runat="server">ข้อมูลผลตอบแทนและสวัสดิการ</h4>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">

                                            <asp:GridView ID="GridViewDetailBenefit"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                ShowHeaderWhenEmpty="true"
                                                CellPadding="0" ForeColor="#333333"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="วันที่ให้">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemBenefit_Date" runat="server" Text='<%# Eval("Benefit_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่สิ้นสุด">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemEnd_Date" runat="server" Text='<%# Eval("End_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อผลตอบแทนและสวัสดิการ">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemBenefit_Name" runat="server" Text='<%# Eval("Benefit_Name") %>' Style="color: blue;"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อผู้รับผลประโยชน์">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemBeneficiary" runat="server" Text='<%# Eval("Beneficiary") %>' Style="color: blue;"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ความสัมพันธ์ผู้รับผลประโยชน์">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemRelationship" runat="server" Text='<%# Eval("Relationship") %>' Style="color: blue;"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="วงเงินที่ให้ (ต่อปี)" ItemStyle-HorizontalAlign="Right">--%>
                                                    <asp:TemplateField HeaderText="วงเงินที่ให้" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemBenefit_Amount" runat="server" Text='<%# Eval("Benefit_Amount","{0:N2}") %>' Style="color: blue;"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Benefit_ID" ItemStyle-HorizontalAlign="Right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBenefit_ID" runat="server" Text='<%# Eval("Benefit_ID") %>' Style="color: blue;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:HiddenField ID="btnSaveMode" runat="server" />

                                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        <asp:Button ID="btnSaveAndNext" class="btn btn-default" runat="server" Text="บันทึกและไปหน้าถัดไป" OnClick="btnSaveAndNext_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                    </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </asp:Panel>

            <asp:Panel ID="pnlInstallation" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ข้อมูลการผ่อนสินค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="col-md-12">
                                <asp:Button ID="btnCreateNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnCreateNew_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnNewBenefit" class="btn btn-primary" runat="server" Text="ถัดไป" OnClick="btnNewBenefit_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="container-fluid panel-container" style="overflow: auto">

                                <asp:GridView ID="GridViewInstallation"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false" ShowHeaderWhenEmpty="true"
                                    OnRowCancelingEdit="GridViewInstallation_RowCancelingEdit"
                                    OnRowDeleting="GridViewInstallation_RowDeleting"
                                    OnRowEditing="GridViewInstallation_RowEditing"
                                    OnRowUpdating="GridViewInstallation_RowUpdating"
                                    OnRowCommand="GridViewInstallation_RowCommand"
                                    CellPadding="0"
                                    ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                </div>
                                            </FooterTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" FooterStyle-Wrap="false">
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
                                        <asp:TemplateField HeaderText="รายละเอียดสินค้าผ่อนเบื้องต้น">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditInstallation_Detail" runat="server" CssClass="form-control" Text='<%# Eval("Installation_Detail") %>' Style="color: blue;" Width="190"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemInstallation_Detail" runat="server" Text='<%# Eval("Installation_Detail") %>' Style="color: blue;" Width="190"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewInstallation_Detail" runat="server" CssClass="form-control" Width="170"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ประเภทการผ่อน" FooterStyle-Wrap="true">
                                            <EditItemTemplate>

                                                <asp:HiddenField ID="hdfEditInstallation_Type" runat="server" Value='<%# Eval("Installation_Type")%>' />
                                                <asp:DropDownList ID="ddlEditInstallation_Type" runat="server" CssClass="form-control"
                                                    DataTextField="VALUE" DataValueField="KEY" Width="128px">


                                                    <%-- <asp:ListItem>==ระบุ==</asp:ListItem>
                                                        <asp:ListItem>ยานพาหนะ</asp:ListItem>
                                                        <asp:ListItem>อุปกรณ์</asp:ListItem>
                                                        <asp:ListItem>อื่นๆ</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemInstallation_Type" runat="server" Text='<%# Eval("Installation_Type") %>' Style="color: blue;" Width="128px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>


                                                <asp:DropDownList ID="ddlNewInstallation_Type" runat="server" DataTextField="VALUE" DataValueField="KEY" Width="128px" CssClass="form-control">

                                                    <%--<asp:ListItem>==ระบุ==</asp:ListItem>
                                                        <asp:ListItem>ยานพาหนะ</asp:ListItem>
                                                        <asp:ListItem>อุปกรณ์</asp:ListItem>
                                                        <asp:ListItem>อื่นๆ</asp:ListItem>--%>
                                                </asp:DropDownList>



                                            </FooterTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่ทำรายการ" ItemStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditTransaction_Date" runat="server" CssClass="form-control" Text='<%# Eval("Transaction_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" Width="110px" onkeypress="return validateFloatKeyPress2(this,event);"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cldEditTransaction_Date" runat="server"
                                                    TargetControlID="txtEditTransaction_Date" PopupButtonID="cldEditTransaction_Date" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemTransaction_Date" runat="server" Text='<%# Eval("Transaction_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewTransaction_Date" runat="server" Width="115px" CssClass="form-control" onkeypress="return validateFloatKeyPress2(this,event);"></asp:TextBox>

                                                <ajaxToolkit:CalendarExtender ID="cldNewTransaction_Date" runat="server"
                                                    TargetControlID="txtNewTransaction_Date" PopupButtonID="cldNewTransaction_Date" />


                                            </FooterTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่ครบกำหนด" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditDue_Date" runat="server" CssClass="form-control" Text='<%# Eval("Due_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" Width="110px" onkeypress="return validateFloatKeyPress2(this,event);"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cldEditDue_Date" runat="server"
                                                    TargetControlID="txtEditDue_Date" PopupButtonID="cldEditDue_Date" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemDue_Date" runat="server" Text='<%# Eval("Due_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewDue_Date" runat="server" CssClass="form-control" Width="115px" onkeypress="return validateFloatKeyPress2(this,event);"></asp:TextBox>

                                                <ajaxToolkit:CalendarExtender ID="cldNewDue_Date" runat="server"
                                                    TargetControlID="txtNewDue_Date" PopupButtonID="cldNewDue_Date" />

                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รายละเอียดเพิ่มเติม">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditDescription" runat="server" Width="170" CssClass="form-control" Text='<%# Eval("Description") %>' Style="color: blue;"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Description") %>' Style="color: blue;" Width="170"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewDescription" runat="server" Width="170" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditInstallation_Amount" runat="server" CssClass="form-control" Text='<%# Eval("Installation_Amount","{0:N2}") %>' Style="color: blue; text-align: right" AutoPostBack="true" OnTextChanged="txtEditPaid_TextChanged" Width="120"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemInstallation_Amount" runat="server" Text='<%# Eval("Installation_Amount","{0:N2}") %>' Style="color: blue; text-align: right" Width="120"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewInstallation_Amount" runat="server" Width="115px" CssClass="form-control" TextMode="number" step="0.01" Text="0" Style="color: blue; text-align: right" AutoPostBack="false" OnTextChanged="txtNewPaid_TextChanged"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชำระแล้ว" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditPaid" runat="server" CssClass="form-control" Style="color: blue; text-align: right" Text='<%# Eval("Payment_Amount","{0:N2}") %>' AutoPostBack="true" OnTextChanged="txtEditPaid_TextChanged" Width="120"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemPaid" runat="server" Style="color: blue; text-align: right" Text='<%# Eval("Payment_Amount","{0:N2}") %>' Width="120"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewPaid" runat="server" Width="115px" CssClass="form-control" Text="0" TextMode="number" step="0.01" AutoPostBack="false" OnTextChanged="txtNewPaid_TextChanged" Style="color: blue; text-align: right"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ยอดคงเหลือ" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBalance_Amount" CssClass="form-control" runat="server" Text='<%# Eval("Balance_Amount","{0:N2}") %>' Style="color: blue; text-align: right" Enabled="false" Width="120"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemBalance_Amount" runat="server" Text='<%# Eval("Balance_Amount","{0:N2}") %>' Style="color: blue; text-align: right" Width="120"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewBalance_Amount" CssClass="form-control" runat="server" Text="0" TextMode="number" step="0.01" Style="color: blue; text-align: right" Enabled="false" Width="120"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Installation_ID" ItemStyle-HorizontalAlign="Right" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInstallation_ID" runat="server" Text='<%# Eval("Installation_ID") %>' Style="color: blue;" Width="120"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlBenefit" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ข้อมูลผลตอบแทนและสวัสดิการ</span>&nbsp;&nbsp;
                             <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                       
                    </div>
                    <div class="row">
                         <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">

                            <asp:Button ID="btnAddNewBenefit" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnAddNewBenefit_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            <asp:Button ID="btnBackAll" class="btn btn-primary" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />

                            <div style="overflow: auto; margin-top: 20px">
                                <asp:GridView ID="GridViewBenefit"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false" ShowHeaderWhenEmpty="true"
                                    OnRowCancelingEdit="GridViewBenefit_RowCancelingEdit"
                                    OnRowDeleting="GridViewBenefit_RowDeleting"
                                    OnRowEditing="GridViewBenefit_RowEditing"
                                    OnRowUpdating="GridViewBenefit_RowUpdating"
                                    OnRowCommand="GridViewBenefit_RowCommand"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                            <ItemTemplate>
                                                <span onclick="return confirm('ยืนยันการลบข้อมูล?')">
                                                    <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                </div>
                                            </FooterTemplate>
                                            <%--  <FooterTemplate>
                                                    <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-mini btn-primary" runat="server" CommandName="AddNew">บันทึก</asp:LinkButton>
                                                </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" FooterStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%--asp:LinkButton ID="lnkBEditUpdate" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></--%>
                                                <asp:LinkButton ID="lnkBEditCancel" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <%--asp:LinkButton ID="lnkAddNew" CssClass="btn btn-mini btn-primary" runat="server" CommandName="AddNew">บันทึก</--%>
                                                <asp:LinkButton ID="lnkFooterCancel" CssClass="btn btn-mini btn-default" runat="server" CausesValidation="True" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="วันที่ให้" ItemStyle-Width="75">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBenefit_Date" CssClass="form-control" runat="server" Text='<%# Eval("Benefit_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" onkeypress="return validateFloatKeyPress2(this,event);"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cldEditBenefit_Date" runat="server"
                                                    TargetControlID="txtEditBenefit_Date" PopupButtonID="cldEditBenefit_Date" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemBenefit_Date" runat="server" Text='<%# Eval("Benefit_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewBenefit_Date" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress2(this,event);"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cldNewBenefit_Date" runat="server"
                                                    TargetControlID="txtNewBenefit_Date" PopupButtonID="cldNewBenefit_Date" />
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สิ้นสุด"  ItemStyle-Width="75">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditEnd_Date" runat="server" CssClass="form-control" Text='<%# Eval("End_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;" onkeypress="return validateFloatKeyPress2(this,event);" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cldEditEnd_Date" runat="server"
                                                    TargetControlID="txtEditEnd_Date" PopupButtonID="cldEditEnd_Date" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemEnd_Date" runat="server" Text='<%# Eval("End_Date","{0:dd/MM/yyyy}") %>' Style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewEnd_Date" runat="server" CssClass="form-control" onkeypress="return validateFloatKeyPress2(this,event);" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cldNewEnd_Date" runat="server"
                                                    TargetControlID="txtNewEnd_Date" PopupButtonID="cldNewEnd_Date" />
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อผลตอบแทนและสวัสดิการ"  ItemStyle-Width="175">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBenefit_Name" CssClass="form-control" runat="server" Text='<%# Eval("Benefit_Name") %>' Style="color: blue;" ></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemBenefit_Name" runat="server" Text='<%# Eval("Benefit_Name") %>' Style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewBenefit_Name" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อผู้รับผลประโยชน์"  ItemStyle-Width="150">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBeneficiary" CssClass="form-control" runat="server" Text='<%# Eval("Beneficiary") %>' Style="color: blue;" ></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemBeneficiary" runat="server" Text='<%# Eval("Beneficiary") %>' Style="color: blue;" ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewBeneficiary" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ความสัมพันธ์ผู้รับผลประโยชน์"  ItemStyle-Width="170">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditRelationship" CssClass="form-control" runat="server" Text='<%# Eval("Relationship") %>' Style="color: blue;"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRelationship" runat="server" Text='<%# Eval("Relationship") %>' Style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewRelationship" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="วงเงินที่ให้ (ต่อปี)" ItemStyle-HorizontalAlign="Right">--%>
                                        <asp:TemplateField HeaderText="วงเงินที่ให้" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="100">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditBenefit_Amount" CssClass="form-control" runat="server" Text='<%# Eval("Benefit_Amount","{0:N2}") %>' Style="color: blue; text-align: right" onkeypress="return validateFloatKeyPress1(this,event);" ></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemBenefit_Amount" runat="server" Text='<%# Eval("Benefit_Amount","{0:N2}") %>' Style="color: blue; text-align: right" onkeypress="return validateFloatKeyPress1(this,event);"  ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewBenefit_Amount" CssClass="form-control" runat="server" Style="color: blue; text-align: right" Text="0" onkeypress="return validateFloatKeyPress1(this,event);"  ></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Benefit_ID" ItemStyle-HorizontalAlign="Right" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenefit_ID" runat="server" Text='<%# Eval("Benefit_ID") %>' Style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>

            <asp:Panel ID="pnlStep" Visible="true" runat="server">
                <div class="container">
                    <div class="row">
                        <br />
                        <br />
                        <br />
                        <br />

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="stepwizard">
                                <div class="stepwizard-row setup-panel">
                                    <div class="stepwizard-step">
                                        <asp:Button ID="lblstep1" runat="server" class="btn btn-default btn-circle" Text="1" OnClick="lblstep1_Click" />
                                        <p>ข้อมูลผู้ใช้งาน</p>
                                    </div>
                                    <div class="stepwizard-step">
                                        <asp:Button ID="lblstep2" runat="server" class="btn btn-primary btn-circle" Text="2" OnClick="lblstep2_Click" />
                                        <p>ข้อมูลการผ่อนสินค้า</p>
                                    </div>
                                    <div class="stepwizard-step">
                                        <asp:Button ID="lblstep3" runat="server" class="btn btn-default btn-circle" Text="3" />
                                        <p>ข้อมูลผลตอบแทนและสวัสดิการ</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </asp:Panel>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddNew" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

