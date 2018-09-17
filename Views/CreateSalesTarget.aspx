<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateSalesTarget.aspx.cs"
    Inherits="Views_CreateSalesTarget" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        function validateFloatKeyPress(el, evt) {
            //  alert(el.value);
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

        function validateFloatKeyPress1(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            return true;
        }



        <%--    var GridId = "<%=GridViewSaleTarget.ClientID %>";
    var ScrollHeight = 300;
    window.onload = function () {
        var grid = document.getElementById(GridId);
        var gridWidth = grid.offsetWidth;
        var gridHeight = grid.offsetHeight;
        var headerCellWidths = new Array();
        for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
            headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
        }
        grid.parentNode.appendChild(document.createElement("div"));
        var parentDiv = grid.parentNode;
 
        var table = document.createElement("table");
        for (i = 0; i < grid.attributes.length; i++) {
            if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
            }
        }
        table.style.cssText = grid.style.cssText;
        table.style.width = gridWidth + "px";
        table.appendChild(document.createElement("tbody"));
        table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
        var cells = table.getElementsByTagName("TH");
 
        var gridRow = grid.getElementsByTagName("TR")[0];
        for (var i = 0; i < cells.length; i++) {
            var width;
            if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                width = headerCellWidths[i];
            }
            else {
                width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
            }
            cells[i].style.width = parseInt(width - 3) + "px";
            gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
        }
        parentDiv.removeChild(grid);
 
        var dummyHeader = document.createElement("div");
        dummyHeader.appendChild(table);
        parentDiv.appendChild(dummyHeader);
        var scrollableDiv = document.createElement("div");
        if(parseInt(gridHeight) > ScrollHeight){
             gridWidth = parseInt(gridWidth) + 17;
        }
        scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
        scrollableDiv.appendChild(grid);
        parentDiv.appendChild(scrollableDiv);
    }--%>



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



    <asp:Panel ID="pnlGrid" Visible="True" runat="server">

        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
                <asp:PostBackTrigger ControlID="btnAddNew" />
            </Triggers>

            <ContentTemplate>

                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">Sales Target</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSearchAgentName" runat="server" CssClass="form-control"
                                            DataTextField="CV_AgentName"
                                            DataValueField="CV_Code">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เดือน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ==" Value=""></asp:ListItem>
                                            <asp:ListItem Text="มกราคม" Value="01"></asp:ListItem>
                                            <asp:ListItem Text="กุมภาพันธ์" Value="02"></asp:ListItem>
                                            <asp:ListItem Text="มีนาคม" Value="03"></asp:ListItem>
                                            <asp:ListItem Text="เมษายน" Value="04"></asp:ListItem>
                                            <asp:ListItem Text="พฤษภาคม" Value="05"></asp:ListItem>
                                            <asp:ListItem Text="มิถุนายน" Value="06"></asp:ListItem>
                                            <asp:ListItem Text="กรกฎาคม" Value="07"></asp:ListItem>
                                            <asp:ListItem Text="สิงหาคม" Value="08"></asp:ListItem>
                                            <asp:ListItem Text="กันยายน" Value="09"></asp:ListItem>
                                            <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ไตรมาส:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlQuater" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ==" Value=""></asp:ListItem>
                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ปี:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchYear" runat="server" CssClass="form-control" MaxLength="4" onkeypress="return  validateFloatKeyPress1(this,event);"></asp:TextBox>

                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12 text-center">

                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;" />


                                <%--<button type="button" ID="btnSearchSubmit1" class="btn btn-primary"   runat="server" OnClick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>ค้นหา
                                </button>--%>
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>

                            <div class="col-md-4">
                                <asp:Button ID="btnAddNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="ButtonNew_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="อัพโหลด" OnClick="btnUpload_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>



                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                            <div class="container-fluid panel-container" style="overflow: auto">
                                <%--<div class="col-md-12">--%>
                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่พบข้อมูล</center>
                                    </div>
                                </asp:Panel>

                                <asp:GridView ID="GridViewSaleTarget"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false" ShowHeaderWhenEmpty="true"
                                    OnRowCancelingEdit="GridViewSaleTarget_RowCancelingEdit"
                                    OnRowDeleting="GridViewSaleTarget_RowDeleting"
                                    OnRowEditing="GridViewSaleTarget_RowEditing"
                                    OnRowUpdating="GridViewSaleTarget_RowUpdating"
                                    OnRowCommand="GridViewSaleTarget_RowCommand"
                                    OnRowDataBound="GridViewSaleTarget_RowDataBound"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    AllowPaging="true" PageSize="10" OnDataBound="GridViewSaleTarget_DataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="60px">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Eval("Sales_Target_ID") %>' CommandName="Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Eval("Sales_Target_ID") %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="center" FooterStyle-Wrap="false" HeaderStyle-Width="60px">
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

                                        <asp:TemplateField HeaderText="ชื่อเอเยนต์">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblItemAgent" runat="server" Text='<%# Eval("AgentName")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlAgentName" runat="server" CssClass="form-control" Enabled="false"
                                                    Style="border-left: 4px solid #ed1c24;">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemAgent" runat="server" Style="color: blue;" Text='<%# Eval("AgentName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblItemAgent" runat="server" Text='<%# Eval("AgentName")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlFooterAgent" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ปี">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditYear" runat="server" Text='<%# Eval("Year") %>' Enabled="false"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;" MaxLength="4" onkeypress="return validateFloatKeyPress1(this,event);"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemYear" runat="server" Text='<%# Eval("Year") %>' Width="64px" Style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterYear" runat="server" CssClass="form-control"
                                                    Style="border-left: 4px solid #ed1c24;" MaxLength="4" onkeypress="return validateFloatKeyPress1(this,event);"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เดือน">
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="ddlEditMonth" runat="server" Text='<%# Eval("Month") %>' Enabled="false"
                                                    CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                                    <asp:ListItem Text="==ระบุ==" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="มกราคม" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="กุมภาพันธ์" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="มีนาคม" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="เมษายน" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="พฤษภาคม" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="มิถุนายน" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="กรกฎาคม" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="สิงหาคม" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="กันยายน" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                                </asp:DropDownList>



                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemMonth" runat="server" Text='<%# Eval("MonthName") %>' Width=" 64px" Style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlFooterMonth" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                                    <asp:ListItem Text="==ระบุ==" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="มกราคม" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="กุมภาพันธ์" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="มีนาคม" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="เมษายน" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="พฤษภาคม" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="มิถุนายน" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="กรกฎาคม" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="สิงหาคม" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="กันยายน" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="เป้ายอดขาย" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditSales_Target" CssClass="form-control" runat="server" Text='<%# Eval("Sales_Target") %>' Style="color: blue; border-left: 4px solid #ed1c24; text-align: right" MaxLength="20" onkeypress="return validateFloatKeyPress1(this,event);"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSales_Target" runat="server" Text='<%# Eval("Sales_Target","{0:N0}") %>' Style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterSales_Target" CssClass="form-control" runat="server" Style="border-left: 4px solid #ed1c24; text-align: right" MaxLength="20" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales_Target_ID" ItemStyle-HorizontalAlign="Right" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSales_Target_ID" runat="server" Text='<%# Eval("Sales_Target_ID") %>' Style="color: blue;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <PagerTemplate>
                                        <table width="100%" >
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:Panel ID="pnlForm" Visible="false" runat="server">

        <div class="container-full-content">
            <div class="row">
                <div class="col-md-12 top-bar-content-none" style="height: 45px">
                    <span class="title" style="font-size: 18px">Sales Target</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                </div>
                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-4">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>

                        </div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>

                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdCSV"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames=""
                                            ShowFooter="false"
                                            ShowHeaderWhenEmpty="false"
                                            CellPadding="0"
                                            ForeColor="#333333"
                                            CssClass="table table-striped table-bordered table-condensed">
                                            <Columns>
                                                <asp:TemplateField HeaderText="CV Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemAgent" runat="server" Width=" 64px" Style="color: blue;" Text='<%# Eval("CV_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ปี">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemYear" runat="server" Text='<%# Eval("Year") %>' Width="64px" Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เดือน">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemMonth" runat="server" Text='<%# Eval("Month") %>' Width=" 64px" Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เป้ายอดขาย" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSales_Target" runat="server" Text='<%# Eval("Sales_Target") %>' Width="64px" Style="color: blue;"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="false" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                </div>

                    <div class="col-md-12">
                        <hr />
                    </div>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnUploadCSV" class="btn btn-primary" runat="server" Text="ดูข้อมูล" OnClick="btnUploadCSV_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                    <asp:Button ID="btnSaveUpload" class="btn btn-primary" runat="server" Text="อัพโหลด" OnClick="btnSaveUpload_Click" OnClientClick="myApp.showPleaseWait(); return true;" Enabled="false" />
                                    <asp:Button ID="btnCancel" class="btn btn-primary" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                </div>
                                <div class="col-md-12">
                                    <br />
                                </div>

                                </div>
            </div>
        </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>

