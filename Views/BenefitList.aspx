<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="BenefitList.aspx.cs" Inherits="Views_BenefitList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">ผลตอบแทนและสวัสดิการ</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:Button ID="ButtonNew" class="btn btn-primary" runat="server" Text="สร้าง" />
                        </div>
                    </div>
                    <div class="col-md-12">

                        <div class="form-group">


                            <asp:GridView ID="GridViewBenefit"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Benefit_Date,Benefit_Name,Beneficiary,Relationship,Benefit_Amount"
                                ShowFooter="false"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                OnRowDataBound="GridView1_RowDataBound"
                                OnRowDeleting="GridView1_RowDeleting"
                                OnRowEditing="GridView1_RowEditing"
                                OnRowUpdating="GridView1_RowUpdating"
                                OnRowCommand="GridView1_RowCommand"
                                CellPadding="0" ForeColor="#333333"
                                CssClass="table table-striped table-bordered table-condensed">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                        <ItemTemplate>
                                            <span onclick="return confirm('ยืนยันการลบข้อมูล?')">
                                                <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-mini btn-primary" runat="server" CommandName="AddNew">บันทึก</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-info" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-mini btn-default" runat="server" CommandName="AddNew">ยกเลิก</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่ให้">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtInstallation_Detail" runat="server" width="100px" Text='<%# Eval("Benefit_Date") %>' style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelInstallation_Detail" runat="server" width="100px" Text='<%# Eval("Benefit_Date") %>' style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewInstallation_Detail" runat="server" width="100px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ชื่อผลตอบแทนและสวัสดิการ">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtInstallation_Type" runat="server" Text='<%# Eval("Benefit_Name") %>' style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelInstallation_Type" runat="server" Text='<%# Eval("Benefit_Name") %>' style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewInstallation_Type" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อผู้รับผลประโยชน์">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtDescription" runat="server" Text='<%# Eval("Beneficiary") %>' style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelDescription" runat="server" Text='<%# Eval("Beneficiary") %>' style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewDescription" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ความสัมพันธ์ผู้รับผลประโยชน์">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtInstallation_Amount" runat="server" Text='<%# Eval("Relationship") %>' Width="64px" style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelInstallation_Amount" runat="server" Text='<%# Eval("Relationship") %>' Width="64px" style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewInstallation_Amount" runat="server" Width="64px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="วงเงินที่ให้ (ต่อปี)" itemstyle-horizontalalign="Right">
                                        <EditItemTemplate>
                                            <asp:TextBox id="txtInstallation_Amount1" runat="server" Text='<%# Eval("Benefit_Amount") %>' Width="64px" style="color: blue;"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label id="LabelInstallation_Amount1" runat="server" Text='<%# Eval("Benefit_Amount") %>' Width="64px" style="color: blue;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewInstallation_Amount1" runat="server" Width="64px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>


                <div class="col-md-12">

                    <div class="panel panel-default" style="margin-right: 10px">

                        <div class="col-md-12">

                            <div class="container" style="padding-left: 30px; padding-right: 30px">
                                <div class="row">
                                    <section>
                                        <div class="wizard">
                                            <div class="wizard-inner">
                                                <%--<div class="connecting-line"></div>--%>
                                                <ul class="nav nav-tabs" role="tablist">

                                                    <li role="presentation" class="disabled">
                                                        <a href="#step1" data-toggle="tab" aria-controls="step1" role="tab" title="ข้อมูลผลตอบแทนและสวัสดิการ">
                                                            <span class="round-tab">
                                                                <i class="glyphicon glyphicon-menu-right"></i>
                                                            </span>
                                                        </a>
                                                    </li>
                                                    <li role="presentation" class="disabled">
                                                        <a href="#step2" data-toggle="tab" aria-controls="step2" role="tab" title="Step 2">
                                                            <span class="round-tab">
                                                                <i class="glyphicon glyphicon-menu-right"></i>
                                                            </span>
                                                        </a>
                                                    </li>

                                                    <li role="presentation" class="active">
                                                        <a href="#step3" data-toggle="tab" aria-controls="complete" role="tab" title="ข้อมูลผลตอบแทนและสวัสดิการ">
                                                            <span class="round-tab">
                                                                <i class="glyphicon glyphicon-menu-hamburger"></i>
                                                            </span>
                                                        </a>
                                                    </li>

                                                </ul>
                                            </div>

                                            <form role="form">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" role="tabpanel" id="step1">
                                                        <h3>ข้อมูลผลตอบแทนและสวัสดิการ</h3>
                                                        <p></p>
                                                    </div>
                                                    <div class="tab-pane" role="tabpanel" id="step2">
                                                        <h3>ข้อมูลการผ่อนสินค้า</h3>
                                                        <p></p>
                                                    </div>
                                                    <div class="tab-pane" role="tabpanel" id="complete">
                                                        <h3>ข้อมูลการผ่อนสินค้า</h3>
                                                        <p></p>
                                                    </div>

                                                </div>
                                            </form>
                                        </div>
                                    </section>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>

