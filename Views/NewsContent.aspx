<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="NewsContent.aspx.cs" Inherits="Views_NewsContent" Culture="th-TH" UICulture="th" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>CP-Meiji | SAM - <%: Page.Title %></title>

    <webopt:BundleReference runat="server" Path="~/Content/css" />

    <link href="~/cpmeiji.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="https://fonts.googleapis.com/css?family=Prompt" rel="stylesheet">
    <!--<link href="https://fonts.googleapis.com/css?family=Mitr" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Kanit" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Abel" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Slabo+27px" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Hind" rel="stylesheet">-->
    <link href="https://fonts.googleapis.com/css?family=Nunito" rel="stylesheet">

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="../Scripts/sweetalert.min.js"></script>

    <link rel="stylesheet" type="text/css" href="../Styles/sweetalert.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-full-content">
            <div class="row">
                <div class="col-md-12 top-bar-content-none" style="height: 45px">
                    <span class="title" style="font-size: 18px">รายละเอียดข่าวสาร</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                </div>
                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                    <div class="form-horizontal" style="align-content: center;">
                        <div class="form-group">
                            <label class="col-md-2 control-label"></label>
                            <div class="col-md-4">
                                <asp:Image ID="imgFile" runat="server" Enabled="false" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">ข่าวสาร:</label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">รายละเอียด:</label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="10"></asp:TextBox>
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
                            <label class="col-md-2 control-label">วีดีโอ:</label>
                            <div class="col-md-4">
                                <asp:Label ID="lblShowYouTubeVideo" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">เอกสารแนบ:</label>
                            <div class="col-md-4">
                                <asp:LinkButton ID="lnkBAttachFile" runat="server" CssClass="btn btn-link" Style="text-decoration: underline" OnClick="lnkBAttachFile_Click"></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnClose" class="btn btn-primary" runat="server" Text="ปิด" OnClick="btnClose_Click"/>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
