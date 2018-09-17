<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="NewsList.aspx.cs"
    Inherits="Views_NewsList" Culture="th-TH" UICulture="th" %>

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

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="true" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ข่าวสาร</span>&nbsp;&nbsp;
                        <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa;">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">หัวข้อข่าวสาร:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtNewsSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">CV Code:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCV_Code" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnOK_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>

                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnCreatNews" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnCreatNews_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <div style="overflow: auto">
                                    <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                            <center>ไม่พบข้อมูล</center>
                                        </div>
                                    </asp:Panel>
                                    <asp:GridView ID="gdvNews"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataKeyNames="News_ID,Content,VDO_Hyperlink,Start_Date,End_Date"
                                        ShowFooter="false"
                                        ShowHeaderWhenEmpty="true"
                                        OnRowDataBound="gdvNews_RowDataBound"
                                        OnRowDeleting="gdvNews_RowDeleting"
                                        OnRowCommand="gdvNews_RowCommand"
                                        CellPadding="0" ForeColor="#333333"
                                        CssClass="table table-striped table-bordered table-condensed" AllowPaging="true" PageSize="10" OnDataBound="gdvNews_DataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ShowHeader="False" Visible ="true">
                                                <ItemTemplate>
                                                    <span>
                                                        <asp:LinkButton ID="lnkBDeleteNews" runat="Server" CssClass="btn btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete" OnClientClick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;"></asp:LinkButton>
                                                    </span>
                                                    <asp:HiddenField ID="hdfNews_ID" runat="server" Value='<%# Eval("News_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภทข่าว" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lnkBNews_Type" runat="server" Text='<%# Eval("News_Type") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อเอเยนต์" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lnkBAgent_Name" runat="server" Text='<%# Eval("Agent_Name") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ข่าวสาร" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBSubject" runat="server" CssClass="btn btn-link" Text='<%# Eval("Subject") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="OpenDetailsNews" Style="text-decoration: underline"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เนื้อหาข่าว" HeaderStyle-Wrap="false" HeaderStyle-Width="300px" ItemStyle-Width="300px">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblContent" runat="server" Text='<%# Eval("Content") %>' Style="color: blue"></asp:Label>--%>
                                                    <asp:Label ID="lblContent" runat="server" Text='<%# Eval("Content").ToString().Length>150 ? (Eval("Content") as string).Substring(0,150) + ".........." : Eval("Content")  %>' Style="color: blue" Width="300"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เปิด/ดาวน์โหลด" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBContentFile" runat="server" CssClass="btn btn-link" Text='<%# Eval("Content_FileName") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="DownloadContentFile" Style="text-decoration: underline"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วีดีโอ" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hplVideo" runat="server" Text='<%# Eval("VDO_Hyperlink") %>' NavigateUrl='<%# Eval("VDO_Hyperlink") %>' Target="_new"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่เริ่ม" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("Start_Date", "{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่สิ้นสุด" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("End_Date", "{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                </ItemTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

