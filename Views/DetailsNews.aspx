<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="DetailsNews.aspx.cs" Inherits="Views_DetailsNews" Culture="th-TH" UICulture="th" %>

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

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">ข่าวสาร</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2 control-label">ประเภทข่าว:</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlNewsType" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem Text="ทั่วไป" Value="ทั่วไป"></asp:ListItem>
                                <asp:ListItem Text="เอเยนต์" Value="เอเยนต์"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlAgentName" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">ข่าวสาร:</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">เนื้อหาข่าว:</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">เปิด/ดาวน์โหลด:</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDownloadFile" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">วีดีโอ:</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtVideo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">วันที่เริ่ม:</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtStart_Date" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <label class="col-md-2 control-label">วันที่สิ้นสุด:</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtEnd_Date" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">รูปภาพ:</label>
                        <div class="col-md-4">
                            <asp:Image ID="imgFile" runat="server" Enabled="false" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12 text-center">
                    <asp:Button ID="btnEdit" class="btn btn-primary" runat="server" Text="แก้ไข" OnClick="btnEdit_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                    <asp:Button ID="btnBackTo" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnBackTo_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                </div>

                <div class="col-md-12 text-center">
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
