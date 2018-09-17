<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreatePayment.aspx.cs" Inherits="Views_CreatePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">จ่ายเงินสด</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">CV CODE:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="txtLast_Name" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">ชิ้น</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดขายรวมทั้งหมด:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox1" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดเงินสดSP:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox2" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">เก็บเงินสดเครดิตลูกค้า:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox3" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดเครดิตลูกค้า:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox4" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">รวมเงินสดจ่าย:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox5" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">เงินข่วยเหลือ:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <%--<asp:textbox id="Textbox6" runat="server" CssClass="form-control" text="100.00"></asp:textbox>--%>
                        </div>
                        <label class="col-md-2 text-left"></label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-2">
                            <asp:label id="label_1" text="1. ช่วยเหลือค่าน้ำแข็ง" cssclass="from-control" runat="server"></asp:label>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox7" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left"></label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">เงินหักอื่นๆ:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <%--<asp:textbox id="Textbox8" runat="server" CssClass="form-control" text="100.00"></asp:textbox>--%>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-2">
                            <asp:label id="label1" text="1. หักเงินประกันสินค้า" cssclass="from-control" runat="server"></asp:label>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox9" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดสรุปเงินสดจ่าย:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox6" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">จ่ายจริง:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox8" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ค้างชำระ:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox10" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="col-md-12">
                        <br />
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">แต้มโบนัสวันนี้:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox11" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">แต้ม</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">คำนวนค่าคอมมิชชั่น(เต็ม):</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox12" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="col-md-12">
                        <br />
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดขายรวมเดือน ?:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox13" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดสินค้าฝาก:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox14" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">ชิ้น</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดบิลค่าชำระ:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox15" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดหนี้คงค้าง:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox16" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ค่าคอมมิชชั่นคงเหลือ:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox17" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">แต้มสะสม:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox18" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">แต้ม</label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ยอดค่าประกันสินค้า:</label>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:textbox id="Textbox19" runat="server" CssClass="form-control" text="100.00"></asp:textbox>
                        </div>
                        <label class="col-md-2 text-left">บาท</label>
                    </div>
                </div>


                <div class="col-md-12 text-center">
                    <br />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="บันทึก" />
                    <asp:Button ID="Button2" class="btn btn-default" runat="server" Text="ยกเลิก" />
                </div>
                <div class="col-md-12 text-center">
                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

