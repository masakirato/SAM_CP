<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreateCustomer.aspx.cs" Inherits="Views_CreateCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">ข้อมูลลูกค้า</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-user" style="font-size: 18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>

            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">CV CODE:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox1" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox2" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">รหัสลูกค้า:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtCustomer_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ประเภทลูกค้า:</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="DropDownListPosition" runat="server" CssClass="form-control">
                                <asp:ListItem Text="ประเภท 1"></asp:ListItem>
                                <asp:ListItem Text="ประเภท 2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">สถานะการเป็นลูกค้า:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtStatus" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ประเภทที่พักอาศัย:</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                <asp:ListItem Text="บ้านพัก"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtFirst_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">นามสกุล:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtLast_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">เบอร์โทรศัพท์:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Phone_No" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">เบอร์มือถือ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtMobile" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อผู้ติดต่อ:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtContact_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">วันเกิด:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtBirthday" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">สถานะ:</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Active"></asp:ListItem>
                                <asp:ListItem Text="InActive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <br />
                    </div>
                    <div class="col-md-12">
                        <h3>ที่อยู่ลูกค้า</h3>
                    </div>


                    <div class="form-group">
                        <label class="col-md-2 control-label">เลขที่:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_House_No" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">อาคาร:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Tower" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">หมู่บ้าน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Village" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">หมู่ที่:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Village_No" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-4">
                        </div>
                        <label class="col-md-2 control-label">ตรอก/ซอย:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Alley" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ถนน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Road" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">จังหวัด:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Province" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-4">
                        </div>
                        <label class="col-md-2 control-label">อำเภอ/เขต:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_District" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-4">
                        </div>
                        <label class="col-md-2 control-label">ตำบล/แขวง:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Sub_district" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-4">
                        </div>
                        <label class="col-md-2 control-label">รหัสไปรษณีย์:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtHome_Postal_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <br />
                    </div>
                    <div class="col-md-12">
                        <h3>ที่อยู่จัดส่ง</h3>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">เลขที่:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_House_No" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">อาคาร:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_Tower" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">หมู่บ้าน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_Village" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">หมู่ที่:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_Village_No" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">ตรอก/ซอย:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_Alley" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ถนน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_Road" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">ตำบล/แขวง:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_Sub_district" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">อำเภอ/เขต:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_District" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">จังหวัด:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_Province" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">รหัสไปรษณีย์:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtShipment_Postal_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">รายละเอียดเพิ่มเติม:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox3" runat="server" CssClass="form-control"></asp:textbox>
                        </div>

                    </div>



                    <div class="col-md-12">
                        <br />
                    </div>
                    <div class="col-md-12">
                        <h3>พนักงานที่ดูแล</h3>
                    </div>


                    <div class="form-group">
                        <label class="col-md-2 control-label">รหัสพนักงาน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtSP_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtSP_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">พนักงานขายแทน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox4" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ประเภทการชำระเงิน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtPayment_Type" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ข้อมูลการวางบิล:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtBilling_Type" runat="server" CssClass="form-control"></asp:textbox>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">วันวางบิล:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtBilling_Day_of_Week" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">วันเก็บเงิน:</label>
                        <div class="col-md-4">
                            <asp:textbox id="Textbox5" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">เครดิต:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtCredit_Term" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                        <label class="col-md-2 control-label">วงเงินเครดิต(ต้องไม่มีทศนิยม):</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtCredit_Limit" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ราคาขาย:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtPrice_Group_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-lg-offset-2 col-lg-10">
                            <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" />
                            <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
