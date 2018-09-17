<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="RolePermissionSetting.aspx.cs" Inherits="Views_RolePermissionSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">Role Permission Details</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-cog"></span>
            </div>


            <div class="col-md-12 bar-content" style="height: 1820px; padding-top: 20px">
                <div class="row">

                    <asp:GridView ID="GridViewPermission"
                        runat="server"
                        AutoGenerateColumns="False"
                        ShowFooter="false"
                        CellPadding="0"
                        ForeColor="#333333"
                        CssClass="table table-striped table-bordered table-condensed"
                        OnRowCreated="GridViewPermission_RowCreated"
                        showheader="false" OnRowDataBound="GridViewPermission_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSelectAll_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Menu"
                                HeaderText="เมนู/ฟังก์ชัน" ControlStyle-Width="80px" />
                            <asp:BoundField DataField="Lev1"
                                HeaderText="" ControlStyle-Width="80px" />
                            <asp:BoundField DataField="Lev2"
                                HeaderText="" ControlStyle-Width="80px" />

                        </Columns>
                    </asp:GridView>


                </div>
            </div>



            <div class="col-md-12 bar-content" style="height: 1820px; padding-top: 20px">
                <div class="row">
                    <div class="table-container">
                        <table id="mytableee" class="table table-bordred table-striped">
                            <thead>
                                <th style="width: 30px; border-bottom: 2px solid #808080">
                                    <input type="checkbox" id="checkall" /></th>
                                <th style="width: 240px; font-weight: 100; border-bottom: 2px solid #808080">เมนู/ฟังก์ชัน</th>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>

                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                การกำหนดสิทธิการใช้งาน
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                ข้อมูล Master
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลCP user
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลพนักงาน
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลagent
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลลูกค้า
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลสินค้า
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลราคา
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลข่าวสาร
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลประเภทบัญชี
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                ระบบการสั่งซื้อ
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                ข้อมูลการสั่งซื้อ
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รับสินค้าตามการสั่งซื้อ
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                สร้างCN/DN
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                export PO No.
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                ระบบเบิก
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                เบิกสินค้า
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                เบิกอื่นๆ
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                ระบบเคลียร์เงิน
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                เคลียร์เงิน
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                เบิกค่าคอมมิชชั่น
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                ระบบสต๊อก
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                สินค้าคงคลัง
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                นับสต๊อก
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                ระบบหนี้
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                หนี้ลูกค้าผูกกับSP
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                หนีSPผูกับAgent
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                หนี้ลูกค้าผูกกับAgent
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                รายรับ-รายจ่าย
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Sales Target
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                รายงาน
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รายงานระบบสั่งซื้อ
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                รายงานยอดสั่งซื้อรายกลุ่มสินค้า
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                รายงานยอดสั่งซื้อเทียบเป้ารายเดือน
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                รายงานยอดสั่งซื้อเทียบเป้ารายไตรมาส
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                รายงานยอดสั่งซื้อเทียบเป้ารายปี
                                            </div>
                                        </div>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รายงานระบบสต๊อก
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>





                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                รายงานสรุปเบิกสินค้าราย SP
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                รายงานการเคลื่อนไหวสินค้า
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                รายงานการปรับสต๊อกสินค้า
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                รายงานสรุปการเบิกสินค้าอื่นๆ
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รายงานข้อมูลลูกค้า
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รายงานการขาย
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รายงานพนักงาน
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>



                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รายงานรายรับ-รายจ่าย
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รายงานระบบหนี้
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                        <input type="checkbox" class="checkthis" />
                                    </td>
                                    <td>
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                รายงานแต้ม
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </td>
                                </tr>




                            </tbody>
                        </table>
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>

