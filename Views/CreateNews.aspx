<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master"
    AutoEventWireup="true" CodeFile="CreateNews.aspx.cs" Inherits="Views_CreateNews" Culture="th-TH" UICulture="th" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
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
            var file = document.getElementById('<%=fulImgUpload.ClientID%>').files[0];
            //alert(file);
            
            formData.append("Filedata", file);
            var t = file.type.split('/').pop().toLowerCase();
            if (t != "jpeg" && t != "jpg" && t != "png" && t != "bmp" && t != "gif") {
                alert('กรุณาเลือกไฟล์ที่เป็นรูปภาพ');
                document.getElementById('<%=fulImgUpload.ClientID%>').value = '';
                return false;
            }
            
            if (file.size > 10240000) {
                alert('ไฟล์ภาพ มีขนาดใหญ่กว่า 10 MB');
                document.getElementById('<%=fulImgUpload.ClientID%>').value = '';
                return false;
            }
            return true;
        }
      
    </script>

    <script type="text/javascript">
        function SearchEmployees(txtSearch, cblEmployees) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html('ค้นพบ ' + (count) + ' รายชื่อ');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }
    </script>

    <style>
        .cb label {
            margin-left: 20px;
        }
    </style>

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
                                <asp:ValidationSummary ID="NewsValidationSummary" runat="server" Font-Bold="true" ForeColor="Red" ValidationGroup="NewsValidation" DisplayMode="BulletList" ShowMessageBox="true" />
                                <label class="col-md-2 control-label">ประเภทข่าว:</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlNewsType" runat="server" CssClass="form-control"
                                        Style="border-left: 4px solid #ed1c24; padding-left: 10px; max-width: 100%"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlNewsType_SelectedIndexChanged">
                                        <asp:ListItem Text="ทั่วไป" Value="ทั่วไป"></asp:ListItem>
                                        <asp:ListItem Text="เอเยนต์" Value="เอเยนต์"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Panel ID="pnlAgent" runat="server">                                
                            <div class="form-group">
                                <label class="col-md-2 control-label">CV Code:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCV_Code" runat="server" CssClass="form-control" Style="max-width: 80%"
                                     onkeyup="SearchEmployees(this,'#ddlAgentName');" placeholder="ค้นหา Agent โดย CV Code"></asp:TextBox> <span id="spnCount" style="color:blue;margin-top:10px"></span>
                                </div>
                            </div>
                            
                                <div class="form-group">

                                    <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                    <div class="col-md-6">

                                        <%--                            <asp:DropDownList ID="ddlAgentName" runat="server" 
                                CssClass="form-control" DataTextField="AgentName" 
                                DataValueField="CV_Code"></asp:DropDownList>--%>

                                        <div style="max-height: 280px; overflow: auto">
                                            <asp:CheckBoxList ID="ddlAgentName" runat="server" Style="max-width: 100%"
                                                CssClass="form-control cb" DataTextField="CV_AgentName"
                                                DataValueField="CV_Code" ClientIDMode="Static">
                                            </asp:CheckBoxList>
                                        </div>

                                    </div>

                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="form-group">
                        <label class="col-md-2 control-label">ข่าวสาร:</label>
                        <div class="col-md-6">
                            <asp:RequiredFieldValidator ID="rfvtxtSubject" runat="server" Display="None" ErrorMessage="กรุณาระบุข่าวสาร" ControlToValidate="txtSubject" ValidationGroup="NewsValidation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"
                                Style="border-left: 4px solid #ed1c24; padding-left: 10px; max-width: 100%"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">เนื้อหาข่าว:</label>
                        <div class="col-md-6">
                            <asp:RequiredFieldValidator ID="rfvtxtContent" runat="server" Display="None" ErrorMessage="กรุณาระบุเนื้อหาข่าว" ControlToValidate="txtContent" ValidationGroup="NewsValidation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtContent" runat="server" CssClass="form-control"
                                Style="border-left: 4px solid #ed1c24; padding-left: 10px; max-width: 100%"
                                TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">เปิด/ดาวน์โหลด:</label>
                        <div class="col-md-6">
                            <%-- Can used browse file but cannot get element or value from this in code behind --%>
                            <%--<input type="text" id="txtFilename" Class="form-control"/>
                                <input type="file" id="fileLoader" name="files" title="Load File" style="display: none;" onchange="fileChoose(fileLoader)"/>
                                <input type="button" id="btnOpenFileDialog" value="..." class="btn btn-primary" onclick="openfileDialog();"/>
                                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
                                <script type="text/javascript">
                                    function openfileDialog() {
                                        $("#fileLoader").click();
                                    }
                                    function fileChoose(FLID) {
                                        document.getElementById("txtFilename").value = $(FLID).val().replace("C:\\fakepath\\", "");
                                    }
                                </script>--%>

                            <asp:CustomValidator ID="ctvfulFileUpload" runat="server" Display="None" ErrorMessage="ไฟล์มีขนาดใหญ่เกินไป" ControlToValidate="fulFileUpload" OnServerValidate="ctvfulFileUpload_ServerValidate" ValidationGroup="NewsValidation" ></asp:CustomValidator>
                            <asp:FileUpload ID="fulFileUpload" runat="server" CssClass="form-control"  onchange="ValidateSize(this)"   />
                            <asp:Label ID="lblfulFileUpload" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">วีดีโอ:</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtVideo" runat="server" CssClass="form-control" Style="max-width: 100%"></asp:TextBox>
                            <%--<asp:Button ID="btnShowVideo" runat="server" CssClass="browse btn btn-primary input-lg" Text="View" OnClick="btnShowVideo_Click" Enabled="false" />--%>
                        </div>
                        <%--<div class="col-md-4">
                                <asp:Label ID="LabelShowYouTubeVideo" runat="server"></asp:Label>
                        </div>--%>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">วันที่เริ่ม:</label>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator ID="rfvtxtStart_Date" runat="server" Display="None" ErrorMessage="กรุณาระบุวันที่เริ่ม" ControlToValidate="txtStart_Date" ValidationGroup="NewsValidation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtStart_Date" runat="server" CssClass="form-control"
                                Style="border-left: 4px solid #ed1c24; padding-left: 10px; max-width: 100%"></asp:TextBox>
                            <%--<asp:ImageButton ID="imgStart_Date" runat="Server" ImageUrl="/images/Calendar.png" Width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                            <ajaxToolkit:CalendarExtender ID="aceStart_Date" runat="server" TargetControlID="txtStart_Date" PopupButtonID="aceStart_Date" Format="dd/MM/yyyy" />
                        </div>
                        <label class="col-md-2 control-label">วันที่สิ้นสุด:</label>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator ID="rfvtxtEnd_Date" runat="server" Display="None" ErrorMessage="กรุณาระบุวันที่สิ้นสุด" ControlToValidate="txtEnd_Date" ValidationGroup="NewsValidation"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEnd_Date" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24; padding-left: 10px; max-width: 100%"></asp:TextBox>
                            <%--<asp:ImageButton runat="Server" ID="imgEnd_Date" ImageUrl="/images/Calendar.png" Width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                            <ajaxToolkit:CalendarExtender ID="aceEnd_Date" runat="server" TargetControlID="txtEnd_Date" PopupButtonID="aceEnd_Date" Format="dd/MM/yyyy" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-2 control-label">รูปภาพ:</label>
                        <div class="col-md-6">
                            <%--<asp:TextBox ID="txtUploadImage" runat="server" CssClass="form-control" placeholder="Upload Image"></asp:TextBox>
                                <asp:Button ID="btnUploadImage" runat="server" CssClass="browse btn btn-primary input-lg" Text="..." />--%>

                            <asp:CustomValidator ID="ctvfulImgUpload" runat="server" Display="None" ErrorMessage="รูปภาพมีขนาดใหญ่เกินไป" ControlToValidate="fulImgUpload" OnServerValidate="ctvfulImgUpload_ServerValidate" ValidationGroup="NewsValidation"></asp:CustomValidator>
                            <asp:FileUpload ID="fulImgUpload" runat="server" CssClass="form-control"   accept="image/*" onchange="validateImage()" />

                            <asp:Label ID="lblfulImgUpload" runat="server" ForeColor="Red"></asp:Label>
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
                <div class="col-md-12 text-center">
                    <br />
                </div>
                <%--<div class="col-md-12 text-center">
                    <asp:Label ID="resultUpload" runat="server"></asp:Label>
                </div>

                <div class="col-md-12 text-center">
                    <asp:Button ID="btnDownload" runat="server" OnClick="btnDownload_Click" Text="Download File" class="btn btn-primary" />
                    <asp:Button ID="btnDownloadImd" runat="server" OnClick="btnDownloadImd_Click" Text="Download Image" class="btn btn-primary" />
                </div>--%>
            </div>
        </div>
    </div>
    <%--  </ContentTemplate>
         </asp:UpdatePanel>--%>
</asp:Content>

