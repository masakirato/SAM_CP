<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="EditNews.aspx.cs" Inherits="Views_EditNews" Culture="th-TH" UICulture="th" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>--%>

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

        function ValidateSize(file) {
            var FileSize = file.files[0].size / 1024 / 1024; // in MB
            if (FileSize > 10) {
                alert('ไฟล์ มีขนาดใหญ่กว่า 10 MB');
                $(file).val(''); //for clearing with Jquery

            }
        }
        function validateImage() {
            var formData = new FormData();
             <%--document.getElementById('<%=fulImgUpload.ClientID%>').value--%>

            //var file = document.getElementById('fulImgUpload').files[0];
            var file = document.getElementById('<%=fulImgUploadEdit.ClientID%>').files[0];
            //alert(file);

            formData.append("Filedata", file);
            var t = file.type.split('/').pop().toLowerCase();
            if (t != "jpeg" && t != "jpg" && t != "png" && t != "bmp" && t != "gif") {
                alert('กรุณาเลือกไฟล์ที่เป็นรูปภาพ');
                document.getElementById('<%=fulImgUploadEdit.ClientID%>').value = '';
                return false;
            }

            if (file.size > 10240000) {
                alert('ไฟล์ภาพ มีขนาดใหญ่กว่า 10 MB');
                document.getElementById('<%=fulImgUploadEdit.ClientID%>').value = '';
                return false;
            }
            return true;
        }



    </script>

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">ข่าวสาร</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
            </div>

            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="form-horizontal">

                    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>

                            <div class="form-group">
                                <asp:ValidationSummary ID="EditNewsValidationSummary" runat="server" Font-Bold="true" ForeColor="Red" ValidationGroup="EditNewsValidation" DisplayMode="BulletList" ShowMessageBox="true" />
                                <label class="col-md-2 control-label">ประเภทข่าว:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlNewsTypeEdit" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; padding-left: 10px;">
                                        <asp:ListItem Text="ทั่วไป" Value="ทั่วไป"></asp:ListItem>
                                        <asp:ListItem Text="เอเยนต์" Value="เอเยนต์"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlAgentNameEdit" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ข่าวสาร:</label>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator ID="rfvtxtSubjectEdit" runat="server" Display="None" ErrorMessage="กรุณาระบุข่าวสาร" ControlToValidate="txtSubjectEdit" ValidationGroup="EditNewsValidation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtSubjectEdit" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; padding-left: 10px;"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">เนื้อหาข่าว:</label>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator ID="rfvtxtContentEdit" runat="server" Display="None" ErrorMessage="กรุณาระบุเนื้อหาข่าว" ControlToValidate="txtContentEdit" ValidationGroup="EditNewsValidation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtContentEdit" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; padding-left: 10px;" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">เปิด/ดาวน์โหลด:</label>
                        <div class="col-md-4">
                            <%--<asp:FileUpload ID="fulFileUploadEdit" runat="server" CssClass="btn" ClientIDMode="Static"  onchange="ValidateSize(this)"  />--%>
                            <%--<asp:CustomValidator ID="ctvfulFileUpload" runat="server" Display="None" ErrorMessage="ไฟล์มีขนาดใหญ่เกินไป" ControlToValidate="fulFileUploadEdit" OnServerValidate="ctvfulFileUpload_ServerValidate" ValidationGroup="NewsValidation" ></asp:CustomValidator>--%>
                            <%--<asp:FileUpload ID="fulFileUploadEdit" runat="server" CssClass="form-control"  onchange="ValidateSize(this)"  />--%>
                            <asp:FileUpload ID="fulFileUploadEdit" runat="server" CssClass="form-control" onchange="ValidateSize(this)" />
                            <asp:Label ID="lblOldFileUploadEdit" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <triggers>
                                    
                                </triggers>

                    <div class="form-group">
                        <label class="col-md-2 control-label">วีดีโอ:</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtVideoEdit" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">วันที่เริ่ม:</label>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator ID="rfvtxtStart_DateEdit" runat="server" Display="None" ErrorMessage="กรุณาระบุวันที่เริ่ม" ControlToValidate="txtStart_DateEdit" ValidationGroup="EditNewsValidation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtStart_DateEdit" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; padding-left: 10px;" OnTextChanged="txtStart_DateEdit_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <%--<asp:ImageButton ID="imgStart_DateEdit" runat="Server" ImageUrl="/images/Calendar.png" Width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                            <ajaxToolkit:CalendarExtender ID="aceStart_DateEdit" runat="server" TargetControlID="txtStart_DateEdit" PopupButtonID="aceStart_DateEdit" Format="dd/MM/yyyy" />
                        </div>
                        <label class="col-md-2 control-label">วันที่สิ้นสุด:</label>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator ID="rfvtxtEnd_DateEdit" runat="server" Display="None" ErrorMessage="กรุณาระบุวันที่สิ้นสุด" ControlToValidate="txtEnd_DateEdit" ValidationGroup="EditNewsValidation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEnd_DateEdit" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; padding-left: 10px;" OnTextChanged="txtEnd_DateEdit_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <%--<asp:ImageButton ID="imgEnd_DateEdit" runat="Server" ImageUrl="/images/Calendar.png" Width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                            <ajaxToolkit:CalendarExtender ID="aceEnd_DateEdit" runat="server" TargetControlID="txtEnd_DateEdit" PopupButtonID="aceEnd_DateEdit" Format="dd/MM/yyyy" />
                        </div>
                        <%--<asp:CompareValidator ID="cpvDatetime" runat="server" ValidationGroup="EditNewsValidation" ControlToValidate="txtStart_DateEdit" ControlToCompare="txtEnd_DateEdit" Operator="LessThanEqual" Type="Date" ErrorMessage="กรุณาระบุวันที่เริ่มต้นให้ถูกต้อง" Display="None"></asp:CompareValidator>--%>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">รูปภาพ:</label>
                        <div class="col-md-4">
                            <%--<asp:FileUpload ID="fulImgUploadEdit" runat="server" CssClass="btn"   accept="image/*" onchange="validateImage()" />--%>
                            <asp:FileUpload ID="fulImgUploadEdit" runat="server" CssClass="form-control" accept="image/*" onchange="validateImage()" />
                            <asp:Label ID="lblOldImgUploadEdit" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                    <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                </div>

                <div class="col-md-12 text-center">
                    <br />
                </div>
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
