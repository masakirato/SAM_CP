<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="ClearingList.aspx.cs" Inherits="Views_ClearingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


       <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">Clearing List</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtOrderingStartDate" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบเบิก:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="Textbox22" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="Textbox23" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="Textbox24" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบเคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="Textbox25" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เคลียร์เงิน:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="Textbox26" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="Textbox27" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 text-center">
                                <asp:Button ID="Button6" class="btn btn-primary" runat="server" Text="ค้นหา"  />
                                <asp:Button ID="Button7" class="btn btn-default" runat="server" Text="ยกเลิก" />
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="ButtonAddNew" class="btn btn-primary" runat="server" Text="สร้างใบเคลียร์เงิน" OnClick="ButtonAddNew_Click" />
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <asp:GridView ID="GridViewClearing"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed" OnRowCommand="GridViewClearing_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center">
                                            <ItemTemplate>
                                                <span onclick="return confirm('ยืนยันการลบข้อมูล?')">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เคลียร์เงิน" ShowHeader="False" itemstyle-horizontalalign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <itemstyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเคลียร์เงิน" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonClearing_No" runat="server" CssClass="btn btn-link" Text='<%# Eval("Clearing_No") %>' CommandArgument='<%# Eval("Clearing_No") %>' CommandName="View" style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                            </ItemTemplate>
                                            <itemstyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เคลียร์เงิน">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Clearing_Date" runat="server" Text='<%# Eval("Clearing_Date") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อพนักงาน">
                                            <ItemTemplate>
                                                <asp:Label id="Label_SP_Name" runat="server" Text='<%# Eval("SP_Name") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเบิก">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Requisition_No" runat="server" Text='<%# Eval("Requisition_No") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เบิก" itemstyle-horizontalalign="Center">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Requisition_Date" runat="server" Text='<%# Eval("Requisition_Date") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะ">
                                            <ItemTemplate>
                                                <asp:Label id="Label_Status" runat="server" Text='<%# Eval("Status") %>' style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_CV_CODE" runat="server" CssClass="btn btn-mini btn-primary" Text="ยืนยันการเคลียร์เงิน" CommandName="Print"></asp:LinkButton>
                                            </ItemTemplate>
                                            <itemstyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_CV_CODE1" runat="server" CssClass="btn btn-mini btn-primary" Text="พิมพ์ใบเคลียร์เงิน" CommandName="Print"></asp:LinkButton>
                                            </ItemTemplate>
                                            <itemstyle Wrap="true" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>

</asp:Content>

