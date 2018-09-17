<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="RolePermission.aspx.cs"
    Inherits="Views_RolePermission" %>

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
    </script>

    <script type="text/javascript">
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {


                var tmp = document.getElementById(o.id);
                var node = document.getElementById(tmp.nextElementSibling.id);
                node.scrollIntoView(true);

                __doPostBack("", "");
            }
        }

        function MyFunction() {
            alert('1');
            $.ajax({
                type: "POST",
                url: "RolePermission.aspx/MyMethod11",
                data: '{ }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function () {
                    // On success       
                    alert('On success');

                },
                Error: function (x, e) {
                    // On Error
                    alert('error');
                }
            });

            // alert('2');
        }

        function onSucceed(results, currentContext, methodName) {
            alert('onSucceed');
            //Do here success event     
        }


        //CallBack method when the page call fails due to internal, server error 
        function onError(results, currentContext, methodName) {
            //Do here failure event
            alert('onError');
        }


        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any(one or more) child not checked
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }



    </script>

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="true" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">กำหนดสิทธิ์</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-user" style="font-size: 18px"></span>
                        </div>
                       
                    </div>
                    <div class="row">
                         
                          
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                            
                          <div class="col-md-12">
                              <asp:Button ID="ButtonCreateNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="ButtonCreateNew_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                          </div>
                            <div class="col-md-12">
                            <br />

                            </div>
                            <div class="container-fluid panel-container" style="overflow: auto">
                                <%--  <div class="col-md-12">--%>
                                <div class="col-md-12">
                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่พบข้อมูล</center>
                                    </div>
                                </asp:Panel>
                                 
                                <asp:GridView ID="GridViewRole"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false" ShowHeaderWhenEmpty="false"
                                    OnRowCancelingEdit="GridViewRole_RowCancelingEdit"
                                    OnRowDataBound="GridViewRole_RowDataBound"
                                    OnRowDeleting="GridViewRole_RowDeleting"
                                    OnRowEditing="GridViewRole_RowEditing"
                                    OnRowUpdating="GridViewRole_RowUpdating"
                                    OnRowCommand="GridViewRole_RowCommand"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">

                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="Delete">
                                                    </asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" Text="บันทึก"></asp:LinkButton>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBitemEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkBEditCancel" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: center;">
                                                    <asp:LinkButton ID="lnkBFooterAddNewCancel" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="รหัสสิทธิ์">
                                            <%--   <EditItemTemplate>
                                                <asp:TextBox id="txtEditRole_ID" runat="server" Text='<%# Eval("Role_ID") %>' maxlength="2" textmode="number"></asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRole_ID" runat="server" Text='<%# Eval("Role_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewRole_ID" runat="server" MaxLength="2" TextMode="number" BorderStyle="None" BackColor="#f9f9f9"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อสิทธิ์">
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" Text='<%# Eval("Role_Name")%>' ID="txtEditRole_Name" CssClass="form-control" Style="border-left: 4px solid #ed1c24;"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Role_Name")%>' ID="lblRole_Name"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewRole_Name" CssClass="form-control" runat="server" Style="border-left: 4px solid #ed1c24;"> </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ประเภทสิทธิ์" FooterStyle-Wrap="true">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEditRole_Type" runat="server" Text='<%#(Eval("Role_Type")==null) ? "==ระบุ==" : Eval("Role_Type")%>' DataTextField="VALUE" DataValueField="KEY" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                                    <asp:ListItem>==ระบุ==</asp:ListItem>
                                                    <asp:ListItem>CP Meiji</asp:ListItem>
                                                    <asp:ListItem>Agent</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRole_Type" runat="server" Text='<%# Eval("Role_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlNewRole_Type" runat="server" DataTextField="VALUE" DataValueField="KEY" CssClass="form-control" Style="border-left: 4px solid #ed1c24;">
                                                    <asp:ListItem>==ระบุ==</asp:ListItem>
                                                    <asp:ListItem>CP Meiji</asp:ListItem>
                                                    <asp:ListItem>Agent</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="#" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkB_SetPermission" runat="server" CssClass="btn btn-mini btn-primary" Text="แก้ไข Permission"
                                                    CommandArgument='<%# Eval("Role_ID") %>'
                                                    CommandName="EditPermission" OnClientClick="javascript:scroll(0,0);"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                    </Columns>
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
                            <span class="title" style="font-size: 18px">Role Permission</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-user" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">รหัสสิทธิ์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRole_ID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ชื่อสิทธิ์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRole_Name" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="ตกลง" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                        <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                    </div>
                                </div>
                                <%-- OnSelectedNodeChanged="OnSelectedNodeChanged"--%>
                                <div class="form-group" style="content">
                                    <asp:UpdatePanel ID="UP_HealthCon_Ddl_Conditions" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:TreeView ID="TreeView2"
                                                runat="server"
                                                ShowExpandCollapse="true"
                                                OnTreeNodeCheckChanged="OnTreeNodeCheckChanged"
                                                ShowCheckBoxes="All" ShowLines="false"
                                                ExpandDepth="0" AfterClientCheck="CheckChildNodes();" PopulateNodesFromClient="true"
                                                onclick="OnTreeClick(event)">
                                            </asp:TreeView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>


                                <%--<div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnShowGrid" class="btn btn-default" runat="server" Text="กลับหน้าค้นหา" OnClick="btnShowGrid_Click" />
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
