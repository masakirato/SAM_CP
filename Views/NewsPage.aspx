<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="NewsPage.aspx.cs" Inherits="Views_NewsPage" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .zoom {
            padding: 50px;
            /*background-color: green;*/
            transition: transform .2s;
            width: 200px;
            height: 200px;
            margin: 0 auto;
        }

            .zoom:hover {
                -ms-transform: scale(1.5); /* IE 9 */
                -webkit-transform: scale(1.5); /* Safari 3-8 */
                transform: scale(1.5);
            }

        /*#myBtn {
            display: none;
            position: fixed;
            bottom: 20px;
            right: 30px;
            z-index: 99;
            font-size: 18px;
            border: none;
            outline: none;
            background-color: red;
            color: white;
            cursor: pointer;
            padding: 15px;
            border-radius: 4px;
        }

            #myBtn:hover {
                background-color: #555;
            }*/
    </style>

    <%-- <script>
        // When the user scrolls down 20px from the top of the document, show the button
        window.onscroll = function () { scrollFunction() };

        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                document.getElementById("myBtn").style.display = "block";
            } else {
                document.getElementById("myBtn").style.display = "none";
            }
        }

        // When the user clicks on the button, scroll to the top of the document
        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }
    </script>--%>


    <asp:Panel ID="pnlCPMeiji" Visible="true" runat="server">
        <asp:HiddenField ID="hdfNEWS_ID" runat="server" />
        <div class="container-full-content">
            <%--<button onclick="topFunction()" id="myBtn" title="Go to top">Top</button>--%>

            <div class="row">


                <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                    <div class="panel panel-default">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">รายละเอียดข่าวสาร</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-bullhorn" style="font-size: 18px"></span>
                            <asp:Label ID="LabelPageHeader" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">

                            <div class="form-horizontal">


                                <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Image ID="imgNewsPhoto" runat="server" class="img-thumbnail zoom" alt="Cinque Terre" Width="304" Height="236" />

                                        <%--<img  ID="imgNewsPhoto" runat="server" class="img-thumbnail" alt="Cinque Terre" width="304" height="236"> --%>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ข่าวสาร:</label>
                                    <div class="col-md-10">
                                        <div style="border: 1px solid black; background-color: lightgray; border-top-left-radius: 5px; border-top-right-radius: 5px; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px;" id="txtSubjectDiv" runat="server">
                                        </div>
                                        <%--<asp:Label id="txtSubjectH1" runat="server" CssClass="form-control" AutoCompleteType="disabled" enabled="false"></asp:Label>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">รายละเอียด:</label>
                                    <div class="col-md-10">
                                        <div style="border: 1px solid black; background-color: lightgray; border-top-left-radius: 5px; border-top-right-radius: 5px; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px; height: 240px; overflow: auto" id="txtContentDiv" runat="server">
                                        </div>
                                        <%--<asp:Label id="txtContentH4" runat="server" CssClass="form-control" AutoCompleteType="disabled" enabled="false"></asp:Label>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เริ่ม:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtStart_Effective_Date" runat="server"
                                            CssClass="form-control" AutoCompleteType="disabled" Enabled="false" BackColor="lightgray"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่สิ้นสุด:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtEnd_Effective_Date" runat="server"
                                            CssClass="form-control" AutoCompleteType="disabled" Enabled="false" BackColor="lightgray"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วีดีโอ:</label>
                                    <div class="col-md-10">
                                        <a id="linkVidoes" target="_parent" runat="server" cssclass="form-control"></a>

                                        <!-- 4:3 aspect ratio -->
                                        <div class="embed-responsive embed-responsive-16by9">
                                            <iframe id="gg" class="embed-responsive-item" runat="server" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                                        </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เอกสารแนบ:</label>
                                    <div class="col-md-10">
                                        <asp:LinkButton ID="lnkBContentFile" runat="server" Text='<%# Eval("Content_FileName") %>' CssClass="form-control" BackColor="lightgray"
                                            CommandName="DownloadContentFile" Style="text-align: left;" OnCommand="lnkBContentFile_Command">
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <button onclick="topFunction()" id="myBtn" title="Go to top">Top</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


</asp:Content>

