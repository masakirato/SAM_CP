<%@ Page Title="Home" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" UICulture="th" Culture="th-TH" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/jquery.bootstrap.newsbox.min.js" type="text/javascript">
    </script>

    <script>
        $(document).on('click', '.panel-heading span.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-minus').addClass('glyphicon-plus');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-plus').addClass('glyphicon-minus');
            }
        });
        $(document).on('click', '.panel div.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-minus').addClass('glyphicon-plus');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-plus').addClass('glyphicon-minus');
            }
        });
        $(document).ready(function () {
            //$('.panel-heading span.clickable').click();
            //$('.panel div.clickable').click();
        });

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

    <div class="container-full-content" id="mainDivHome" runat="server">
        <div class="row">
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                <div class="panel panel-default">
                    <div class="panel-heading clickable">
                        <span><i class="glyphicon glyphicon-minus"></i></span>
                        <span style="font-size: 1.2em;">ข้อมูลทั่วไป</span>
                    </div>
                    <div class="panel-body">

                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-md-3 label-text">
                                        <p>จำนวนพนักงาน :</p>
                                    </div>
                                    <div class="col-md-2 label-value">
                                        <asp:Label ID="lbl_Employee" runat="server" Text="20"></asp:Label>
                                    </div>

                                    <div class="col-md-3 label-text">
                                        <p>จำนวนลูกค้า :</p>
                                    </div>
                                    <div class="col-md-2 label-value">
                                        <asp:Label ID="lbl_Customer" runat="server" Text="20"></asp:Label>
                                    </div>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-md-3 label-text">
                                        <p>จำนวนพนักงานมาใหม่เดือนนี้ :</p>
                                    </div>
                                    <div class="col-md-2 label-value">
                                        <asp:Label ID="lbl_SPNew" runat="server" Text="2"></asp:Label>
                                    </div>
                                    <div class="col-md-3 label-text">
                                        <p>จำนวนพนักงานลาออกเดือนนี้ :</p>
                                    </div>
                                    <div class="col-md-2 label-value">
                                        <asp:Label ID="lbl_SPOut" runat="server" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row" id="dashboardDiv" runat="server">
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                <div class="panel panel-default">
                    <div class="panel-heading clickable">
                        <span><i class="glyphicon glyphicon-minus"></i></span>
                        <span style="font-size: 1.2em;">กราฟยอดสั่งสินค้าเทียบเป้า</span>
                        <%-- <h3 class="panel-title">Dashboard</h3>
                        <span class="pull-right"><i class="glyphicon glyphicon-minus"></i></span>--%>
                    </div>
                    <div class="panel-body">
                        <canvas id="mixed-chart">
                            <script>
                       
                            </script>
                        </canvas>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                <div class="panel panel-default">
                    <div class="panel-heading clickable">
                        <span><i class="glyphicon glyphicon-minus"></i></span>
                        <span style="font-size: 1.2em;">ข่าวสาร</span>
                        <%--<h3 class="panel-title">ข่าวสาร</h3>
                        <span class="pull-right"><i class="glyphicon glyphicon-minus"></i></span>--%>
                    </div>
                    <div class="panel-body" style="height: 480px; overflow: auto">
                        <div class="tabbable-panel" style="margin-top: 20px">
                            <div class="tabbable-line">
                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center">ข่าวสารทั่วไป</a>
                                        <script>
                                            $("#tab1").on('click', function (e) {
                                                $("#imgTab1").show();
                                                $("#imgTab2").hide();

                                                return true;
                                            });
                                        </script>
                                    </li>
                                    <li>
                                        <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center">ข่าวสารสำหรับแต่ละเอเยนต์</a>
                                        <script>
                                            $("#tab2").on('click', function (e) {
                                                $("#imgTab2").show();
                                                $("#imgTab1").hide();
                                                return true;
                                            });
                                        </script>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_default_1">
                                        <div class="container-fluid panel-container" id="container_new_tab1" runat="server">
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_default_2">
                                        <div class="container-fluid panel-container" id="container_new_tab2" runat="server">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                        <div class="panel panel-default">
                            <div class="panel-heading clickable">
                                <span><i class="glyphicon glyphicon-minus"></i></span>
                                <span style="font-size: 1.2em;">ข้อมูลใบแจ้งหนี้,ใบลดหนี้,ใบเพิ่มหนี้</span>
                                <%--<h3 class="panel-title">ข้อมูลใบแจ้งหนี้,ใบลดหนี้,ใบเพิ่มหนี้</h3>
                                <span class="pull-right"><i class="glyphicon glyphicon-minus"></i></span>--%>
                            </div>
                            <div class="panel-body">
                                <div class="tabbable-panel" style="margin-top: 20px">
                                    <div class="tabbable-line">
                                        <ul class="nav nav-tabs">
                                            <li class="active">
                                                <a href="#tab_info_1" id="tab_info_li_1" data-toggle="tab" style="padding-left: 30px" class="text-center">ใบแจ้งหนี้</a>
                                            </li>
                                            <li>
                                                <a href="#tab_info_2" id="tab_info_li_2" data-toggle="tab" style="padding-left: 30px" class="text-center">ใบเพิ่มหนี้</a>

                                            </li>
                                            <li>
                                                <a href="#tab_info_3" id="tab_info_li_3" data-toggle="tab" style="padding-left: 30px" class="text-center">ใบลดหนี้</a>

                                            </li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane" id="tab_info_2">
                                                <div class="container-fluid panel-container" style="overflow: auto">
                                                    <asp:GridView ID="grdTab_2"
                                                        runat="server"
                                                        AutoGenerateColumns="False"
                                                        DataKeyNames=""
                                                        ShowFooter="false"
                                                        CellPadding="0"
                                                        ForeColor="#333333" ShowHeaderWhenEmpty="true"
                                                        CssClass="table table-striped table-bordered table-condensed"
                                                        OnRowCommand="grdTab_2_RowCommand"
                                                        AllowPaging="true" PageSize="10" OnDataBound="grdTab_2_DataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ประเภทเอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DocumentType" runat="server" Text='<%# Eval("Billing_Type_tmp") %>' Style="color: blue; text-align: center"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่เอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <%-- <ItemTemplate>
                                                        <asp:Label id="lbl_DocumentNo" runat="server" Text='<%# Eval("Billing_ID") %>' style="color: blue"></asp:Label>
                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_DocumentNo" runat="server" CssClass="btn btn-link"
                                                                        Text='<%# Eval("Invoice_No") %>'
                                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                                        CommandName="View" Style="font-weight: bold; text-decoration: underline; color: blue">

                                                                    </asp:LinkButton>
                                                                    <asp:Label ID="lbl_Billing_ID" Visible="false" runat="server" Text='<%# Eval("Billing_ID") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่เอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DocumentDate" runat="server" Text='<%# Eval("Created_Date","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PONumber" runat="server" Text='<%# Eval("PO_No") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่สร้างใบสั่งซื้อ" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PODate" runat="server" Text='<%# Eval("PO_Date","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนเงิน" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Amount" runat="server" Text='<%# Eval("Net_Value","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ภาษีมูลค่าเพิ่ม" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVat" runat="server" Text='<%# Eval("Vat","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_TotalAmount" runat="server" Text='<%# Eval("Total","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="อ้างอิงเลขที่เอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <%--<ItemTemplate>
                                                        <asp:Label id="lbl_RefPONumber" runat="server" Text='<%# Eval("Ref_Invoice_No") %>' style="color: blue"></asp:Label>
                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_RefPONumber" runat="server" Text='<%# Eval("Ref_Invoice_No") %>' Style="color: blue"></asp:Label>

                                                                    <%--  <asp:LinkButton ID="lbl_RefPONumber" runat="server" CssClass="btn btn-link"
                                                                Text='<%# Eval("Ref_Invoice_No") %>'
                                                                CommandArgument='<%# Eval("Ref_Invoice_No") %>'
                                                                CommandName="View" Style="font-weight: bold; text-decoration: underline; color: blue">
                                                            </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                        <PagerTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 70%">
                                                                        <asp:Label ID="MessageLabel"
                                                                            ForeColor="Blue"
                                                                            Text="เลือกหน้า :"
                                                                            runat="server" />
                                                                        <asp:DropDownList ID="PageDropDownListTab2"
                                                                            AutoPostBack="true"
                                                                            OnSelectedIndexChanged="PageDropDownListTab2_SelectedIndexChanged"
                                                                            onchange="myApp.showPleaseWait();"
                                                                            runat="server" />
                                                                    </td>

                                                                    <td style="width: 70%; text-align: right">
                                                                        <asp:Label ID="CurrentPageLabelTab2"
                                                                            ForeColor="Black"
                                                                            runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </PagerTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab_info_3">
                                                <div class="container-fluid panel-container" style="overflow: auto">
                                                    <asp:GridView ID="grdTab_3"
                                                        runat="server"
                                                        AutoGenerateColumns="False"
                                                        DataKeyNames=""
                                                        ShowFooter="false"
                                                        CellPadding="0"
                                                        ForeColor="#333333" ShowHeaderWhenEmpty="true"
                                                        CssClass="table table-striped table-bordered table-condensed"
                                                        OnRowCommand="grdTab_3_RowCommand"
                                                        AllowPaging="true" PageSize="10" OnDataBound="grdTab_3_DataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ประเภทเอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DocumentType" runat="server" Text='<%# Eval("Billing_Type_tmp") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่เอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <%-- <ItemTemplate>
                                                        <asp:Label id="lbl_DocumentNo" runat="server" Text='<%# Eval("Billing_ID") %>' style="color: blue"></asp:Label>
                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_DocumentNo" runat="server" CssClass="btn btn-link"
                                                                        Text='<%# Eval("Invoice_No") %>'
                                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                                        CommandName="View" Style="font-weight: bold; text-decoration: underline; color: blue">
                                                                    </asp:LinkButton>
                                                                    <asp:Label ID="lbl_Billing_ID" Visible="false" runat="server" Text='<%# Eval("Billing_ID") %>' Style="color: blue"></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่เอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DocumentDate" runat="server" Text='<%# Eval("Created_Date","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PONumber" runat="server" Text='<%# Eval("PO_No") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่สร้างใบสั่งซื้อ" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PODate" runat="server" Text='<%# Eval("PO_Date","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนเงิน" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Amount" runat="server" Text='<%# Eval("Net_Value","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ภาษีมูลค่าเพิ่ม" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVat" runat="server" Text='<%# Eval("Vat","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_TotalAmount" runat="server" Text='<%# Eval("Total","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="อ้างอิงเลขที่เอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <%--<ItemTemplate>
                                                        <asp:Label id="lbl_RefPONumber" runat="server" Text='<%# Eval("Ref_Invoice_No") %>' style="color: blue"></asp:Label>
                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_RefPONumber" runat="server" Text='<%# Eval("Ref_Invoice_No") %>' Style="color: blue"></asp:Label>
                                                                    <%--<asp:LinkButton ID="lbl_RefPONumber" runat="server" CssClass="btn btn-link"
                                                                Text='<%# Eval("Ref_Invoice_No") %>'
                                                                CommandArgument='<%# Eval("Ref_Invoice_No") %>'
                                                                CommandName="View" Style="font-weight: bold; text-decoration: underline; color: blue">

                                                            </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                        <PagerTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 70%">
                                                                        <asp:Label ID="MessageLabel"
                                                                            ForeColor="Blue"
                                                                            Text="เลือกหน้า :"
                                                                            runat="server" />
                                                                        <asp:DropDownList ID="PageDropDownListTab3"
                                                                            AutoPostBack="true"
                                                                            OnSelectedIndexChanged="PageDropDownListTab3_SelectedIndexChanged"
                                                                            onchange="myApp.showPleaseWait();"
                                                                            runat="server" />
                                                                    </td>

                                                                    <td style="width: 70%; text-align: right">
                                                                        <asp:Label ID="CurrentPageLabelTab3"
                                                                            ForeColor="Black"
                                                                            runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </PagerTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="tab-pane active" id="tab_info_1">
                                                <div class="container-fluid panel-container" style="overflow: auto">
                                                    <asp:GridView ID="grdTab_1"
                                                        runat="server"
                                                        AutoGenerateColumns="False"
                                                        CellPadding="0"
                                                        ForeColor="#333333" ShowHeaderWhenEmpty="True"
                                                        CssClass="table table-striped table-bordered table-condensed"
                                                        OnRowCommand="grdTab_1_RowCommand"
                                                        AllowPaging="true" PageSize="10" OnDataBound="grdTab_1_DataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ประเภทเอกสาร" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DocumentType" runat="server" Text='<%# Eval("Billing_Type_tmp") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่เอกสาร">
                                                                <%--<ItemTemplate>
                                                        <asp:Label id="lbl_DocumentNo" runat="server" Text='<%# Eval("Billing_ID") %>' style="color: blue"></asp:Label>
                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_DocumentNo" runat="server" CssClass="btn btn-link"
                                                                        Text='<%# Eval("Invoice_No") %>'
                                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                                        CommandName="View" Style="font-weight: bold; text-decoration: underline; color: blue">

                                                                    </asp:LinkButton>
                                                                    <asp:Label ID="lbl_Billing_ID" Visible="false" runat="server" Text='<%# Eval("Billing_ID") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่เอกสาร">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DocumentDate" runat="server" Text='<%# Eval("Created_Date","{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PONumber" runat="server" Text='<%# Eval("PO_No") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่สร้างใบสั่งซื้อ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PODate" runat="server" Text='<%# Eval("PO_Date","{0:dd/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนเงิน">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Amount" runat="server" Text='<%# Eval("Net_Value","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ภาษีมูลค่าเพิ่ม">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVat" runat="server" Text='<%# Eval("Vat","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_TotalAmount" runat="server" Text='<%# Eval("Total","{0:N2}") %>' Style="color: blue"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="อ้างอิงเลขที่เอกสาร">
                                                                <%--<ItemTemplate>
                                                        <asp:Label id="lbl_RefPONumber" runat="server" Text='<%# Eval("Ref_Invoice_No") %>' style="color: blue"></asp:Label>
                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_RefPONumber" runat="server" Text='<%# Eval("Ref_Invoice_No") %>' Style="color: blue"></asp:Label>

                                                                    <%-- <asp:LinkButton ID="lbl_RefPONumber" runat="server" CssClass="btn btn-link"
                                                                Text='<%# Eval("Ref_Invoice_No") %>'
                                                                CommandArgument='<%# Eval("Ref_Invoice_No") %>'
                                                                CommandName="View" Style="font-weight: bold; text-decoration: underline; color: blue">

                                                            </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="True" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                        <PagerTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 70%">
                                                                        <asp:Label ID="MessageLabel"
                                                                            ForeColor="Blue"
                                                                            Text="เลือกหน้า :"
                                                                            runat="server" />
                                                                        <asp:DropDownList ID="PageDropDownListTab1"
                                                                            AutoPostBack="true"
                                                                            OnSelectedIndexChanged="PageDropDownListTab1_SelectedIndexChanged"
                                                                            onchange="myApp.showPleaseWait();"
                                                                            runat="server" />
                                                                    </td>

                                                                    <td style="width: 70%; text-align: right">
                                                                        <asp:Label ID="CurrentPageLabelTab1"
                                                                            ForeColor="Black"
                                                                            runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </PagerTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>


    <%--    <div class="container">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">ข้อมูลทั่วไป</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-info-sign"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                <div class="row">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-3 label-text">
                                    <p>จำนวนพนักงาน :</p>
                                </div>
                                <div class="col-md-2 label-value">
                                    <asp:Label ID="LabelNumberOfEmployee" runat="server" Text="">20</asp:Label>
                                </div>

                                <div class="col-md-3 label-text">
                                    <p>จำนวนลูกค้า :</p>
                                </div>
                                <div class="col-md-2 label-value">
                                    <asp:Label ID="LabelNumberOfCustomer" runat="server" Text="">20</asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-3 label-text">
                                    <p>จำนวน SP มาใหม่เดือนนี้ :</p>
                                </div>
                                <div class="col-md-2 label-value">
                                    <asp:Label ID="LabelNumberOfNewSP" runat="server" Text="">2</asp:Label>
                                </div>
                                <div class="col-md-3 label-text">
                                    <p>จำนวน SP ลาออกเดือนนี้ :</p>
                                </div>
                                <div class="col-md-2 label-value">
                                    <asp:Label ID="LabelNumberOfLeftSP" runat="server" Text="">0</asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <!-- Dashboard -->
    <%-- <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">Dashboard </span>&nbsp;&nbsp;<span class=" glyphicon glyphicon-dashboard"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">

                <%-- <canvas id="mixed-chart">
                    <script>
                        //var mixed_chart = new Chart(document.getElementById("mixed-chart"), {
                        //    type: 'bar',
                        //    data: {
                        //        labels: ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน"],
                        //        datasets: [{
                        //            label: "เป้ายอดขาย",
                        //            type: "line",
                        //            borderColor: "#0EB38D",
                        //            data: [10000, 11000, 12000, 13000, 14000, 15000],
                        //            fill: false
                        //        }, {
                        //            label: "ยอดขายจริง",
                        //            type: "bar",
                        //            backgroundColor: "#0E1FB3",
                        //            data: [8000, 3400, 1200, 8000, 8500, 300],
                        //        }
                        //        ]
                        //    },
                        //    options: {
                        //        title: {
                        //            display: true,
                        //            text: 'ยอดสั่งซื้อประจำปี 2560'
                        //        },
                        //        legend: { display: false }
                        //    }
                        //});
                    </script>
                </canvas>--%>

    <%--                    <canvas id="myChart" width="400" height="280"></canvas>
                    <script>
                        var myChart = new Chart(ctx, {
                            type: 'bar',
                            data: data,
                            options: options
                        });

                        var mixedChart = new Chart(ctx, {
                            type: 'bar',
                            data: {
                                datasets: [{
                                    label: 'Bar Dataset',
                                    data: [10, 20, 30, 40]
                                }, {
                                    label: 'Line Dataset',
                                    data: [50, 50, 50, 50],

                                    // Changes this dataset to become a line
                                    type: 'line'
                                }],
                                labels: ['January', 'February', 'March', 'April']
                            },
                            options: options
                        });

                    </script>
            </div>
        </div>
    </div>--%>


    <%--    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">ข่าวสาร</span>
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                <div class="tabbable-panel" style="margin-top: 20px">
                    <div class="tabbable-line">
                        <ul class="nav nav-tabs">
                            <li class="active">
                                <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center">ข่าวสารทั่วไป</a>
                                <script>
                                    $("#tab1").on('click', function (e) {
                                        $("#imgTab1").show();
                                        $("#imgTab2").hide();

                                        return true;
                                    });
                                </script>
                            </li>
                            <li>
                                <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center">ข่าวสารสำหรับแต่ละเอเยนต์</a>
                                <script>
                                    $("#tab2").on('click', function (e) {
                                        $("#imgTab2").show();
                                        $("#imgTab1").hide();
                                        return true;
                                    });
                                </script>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_default_1">
                                <div class="container" id="container_new_tab1" runat="server">
                                </div>

                            </div>
                            <div class="tab-pane" id="tab_default_2">
                                <div id="ContentPlaceHolder1_container_new_tab1" class="container">
                                    <div class="well">
                                        <div class="media">
                                            <a class="pull-left" href="#">



                                                <img src="../images/new1.jpg" width="150" height="100" style="border: 1px solid #d4d1d1; padding: 1px 1px 1px 1px"></img></a><div class="media-body">
                                                    <h4 class="media-heading">เปิดตัวนมเปรี้ยว &quot;ไพเกน โปร5&quot; พร้อมเปิดตัว พรีเซ็นเตอร์หนุ่มฮอต &quot;ณเดชน์ คูกิมิยะ&quot;</h4>
                                                    <p>นายประสิทธิ์ บุญดวงประเสริฐ กรรมการผู้จัดการ บริษัท ซีพี-เมจิ จำกัด ผู้ผลิตและจำหน่ายนมสดพาสเจอร์ไรส์ ภายใต้แบรนด์ &quot;เมจิ&quot; จัดโครงการ &quot;เมจิ เทนไซ ปี2 ติวน้องสู่เส้นชัย เข้า ม.1&quot; สนับสนุนก้าวแรกที่สำคัญของนักเรียนชั้น ป.6 เพื่อเตรียมสอบเข้าชั้น ม.1 โดยเปิดรับสมัครนักเรียนจากโรงเรียนทั่วประเทศจำนวน 1,200 คน</p>

                                                    <span><i class="glyphicon glyphicon-calendar"></i>7 มิถุนายน 2560</span>

                                                </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>


    </div>--%>


    <%--    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">ข้อมูลใบแจ้งหนี้,ใบลดหนี้,ใบเพิ่มหนี้</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-bullhorn"></span>
            </div>

            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">

                <div class="tabbable-panel" style="margin-top: 20px">
                    <div class="tabbable-line">
                        <ul class="nav nav-tabs">
                            <li class="active">
                                <a href="#tab_info_1" id="tab_info_li_1" data-toggle="tab" style="padding-left: 30px" class="text-center">ใบแจ้งหนี้</a>

                            </li>
                            <li>
                                <a href="#tab_info_2" id="tab_info_li_2" data-toggle="tab" style="padding-left: 30px" class="text-center">ใบเพิ่มหนี้</a>

                            </li>
                            <li>
                                <a href="#tab_info_3" id="tab_info_li_3" data-toggle="tab" style="padding-left: 30px" class="text-center">ใบลดหนี้</a>

                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_info_1">
                                <asp:GridView ID="grdTab_1"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333" showheaderwhenempty="true"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ประเภทเอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentType" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่เอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentNo" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentDate" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PONumber" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สร้างใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PODate" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงิน">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Amount" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vat">
                                            <ItemTemplate>
                                                <asp:TextBox id="lblVat" runat="server" style="color: blue"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด">
                                            <ItemTemplate>
                                                <asp:checkbox id="lbl_TotalAmount" runat="server" style="color: blue"></asp:checkbox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="อ้างอิงเลขที่ใบแจ้งหนี้">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_RefPONumber" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane" id="tab_info_2">

                                <asp:GridView ID="grdTab_2"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333" showheaderwhenempty="true"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ประเภทเอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentType" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่เอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentNo" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentDate" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PONumber" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สร้างใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PODate" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงิน">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Amount" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vat">
                                            <ItemTemplate>
                                                <asp:TextBox id="lblVat" runat="server" style="color: blue"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด">
                                            <ItemTemplate>
                                                <asp:checkbox id="lbl_TotalAmount" runat="server" style="color: blue"></asp:checkbox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="อ้างอิงเลขที่ใบแจ้งหนี้">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_RefPONumber" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="tab-pane" id="tab_info_3">

                                <asp:GridView ID="grdTab_3"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    ShowFooter="false"
                                    CellPadding="0"
                                    ForeColor="#333333" showheaderwhenempty="true"
                                    CssClass="table table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ประเภทเอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentType" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่เอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentNo" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เอกสาร">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_DocumentDate" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PONumber" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สร้างใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_PODate" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงิน">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_Amount" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vat">
                                            <ItemTemplate>
                                                <asp:TextBox id="lblVat" runat="server" style="color: blue"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงินทั้งหมด">
                                            <ItemTemplate>
                                                <asp:checkbox id="lbl_TotalAmount" runat="server" style="color: blue"></asp:checkbox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="อ้างอิงเลขที่ใบแจ้งหนี้">
                                            <ItemTemplate>
                                                <asp:Label id="lbl_RefPONumber" runat="server" style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>--%>

    <!--# Promotions -->

    <%-- <!-- Smart Inventory -->
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">Smart สินค้าคงคลัง</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-barcode"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: 350px">
                <div class="col-md-4" style="padding-top: 10px">
                    <div class="panel panel-default">
                        <div class="panel-heading"><span class="glyphicon glyphicon-comment"></span>&nbsp;แจ้งเตือนสินค้าคงคลัง</div>
                        <div class="panel-body" style="height: 210px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <ul class="alert">
                                        <li class="news-item">
                                            <table>
                                                <tr>
                                                    <td><span style="font-size: 14px; font-weight: 600; color: red">สินค้าคงคลังเหลือน้อยกว่ากำหนด</span><br />
                                                        <br />
                                                        คุณมีสินค้าบางชนิด จำนวนน้อยกว่ามาตรฐาน
                                                        <br />
                                                        <br />
                                                        <span style="font-size: 12px;">&nbsp;&nbsp;- Bulgaria Yoghurt จำนวน 10 / <span style="color: blue">15</span> กล่อง </span>
                                                        <br />
                                                        <span style="font-size: 12px;">&nbsp;&nbsp;- Low Fat Yoghurt จำนวน 8 / <span style="color: blue">15</span> กล่อง </span>
                                                        <br />
                                                        <br />
                                                        <span style="color: blue">ควรสั่งซื้อสินค้าเพิ่มเติมสินค้าคงคลัง</span></td>
                                                </tr>
                                            </table>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-8" style="padding-top: 10px">
                    <div class="panel panel-default">
                        <div class="panel-heading"><span class="glyphicon glyphicon-list-alt"></span>&nbsp;สินค้าคงคลังแยกตามประเภท</div>
                        <div class="panel-body" style="height: 265px">
                            <div class="row">
                                <div class="col-xs-12">
                                    <ul class="inventory">
                                        <li class="news-item" style="border-bottom: 1px solid rgba(0,0,0,0.10);">
                                            <table>
                                                <tr style="background: #e7f3fa">
                                                    <td style="width: 160px; font-size: 16px">ประเภทสินค้า</td>
                                                    <td style="font-size: 16px; width: 80px">ปัจจุบัน</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td style="width: 60px; font-size: 16px">เบิก</td>
                                                    <td style="width: 60px; font-size: 16px">ขาย</td>
                                                    <td style="width: 60px; font-size: 16px">รับเข้า</td>
                                                    <td style="font-size: 16px; width: 80px;">ตั้งต้น</td>
                                                </tr>
                                            </table>
                                        </li>
                                        <li class="news-item" style="border-bottom: 0px solid rgba(0,0,0,0.10);">
                                            <table>
                                                <tr>
                                                    <td style="width: 180px"><span style="font-size: 13px;">Bulgaria Yoghurt</span></td>
                                                    <td style="width: 80px"><span style="font-size: 14px; font-weight: 100; color: blue">10</span>&nbsp;กล่อง</td>
                                                    <td style="width: 60px">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: red">3</span>&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: red">2</span>&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: Green">0</span>&nbsp;</td>
                                                    <td style="width: 80px"><span style="font-size: 13px; color: blue">15</span>&nbsp;กล่อง</td>
                                                    <td><span class="glyphicon glyphicon-chevron-down"></span></td>
                                                </tr>
                                            </table>
                                        </li>
                                        <li class="news-item" style="border-bottom: 0px solid rgba(0,0,0,0.10);">
                                            <table>
                                                <tr>
                                                    <td style="width: 180px"><span style="font-size: 13px;">Low Fat Yoghurt</span></td>
                                                    <td style="width: 80px"><span style="font-size: 14px; font-weight: 100; color: blue">8</span>&nbsp;กล่อง</td>
                                                    <td style="width: 60px">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: red">1</span>&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: red">1</span>&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: Green">0</span>&nbsp;</td>
                                                    <td style="width: 80px"><span style="font-size: 13px; color: blue">10</span>&nbsp;กล่อง</td>
                                                    <td><span class="glyphicon glyphicon-chevron-down"></span></td>
                                                </tr>
                                            </table>
                                        </li>
                                        <li class="news-item" style="border-bottom: 0px solid rgba(0,0,0,0.10);">
                                            <table>
                                                <tr>
                                                    <td style="width: 180px"><span style="font-size: 13px;">Paigen Drinking Yoghurt</span></td>
                                                    <td style="width: 80px"><span style="font-size: 14px; font-weight: 100; color: blue">20</span>&nbsp;กล่อง</td>
                                                    <td style="width: 60px">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: red">4</span>&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: red">4</span>&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: Green">8</span>&nbsp;</td>
                                                    <td style="width: 80px"><span style="font-size: 13px; color: blue">20</span>&nbsp;กล่อง</td>
                                                    <td><span class="glyphicon glyphicon-resize-horizontal"></span></td>
                                                </tr>
                                            </table>
                                        </li>
                                        <li class="news-item" style="border-bottom: 0px solid rgba(0,0,0,0.10);">
                                            <table>
                                                <tr>
                                                    <td style="width: 180px"><span style="font-size: 13px;">Pasteurized Fresh Milk</span></td>
                                                    <td style="width: 80px"><span style="font-size: 14px; font-weight: 100; color: blue">49</span>&nbsp;กล่อง</td>
                                                    <td style="width: 60px">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: red">10</span>&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: red">6</span>&nbsp;</td>
                                                    <td style="width: 60px"><span style="font-size: 13px; color: Green">2</span>&nbsp;</td>
                                                    <td style="width: 80px"><span style="font-size: 13px; color: blue">63</span>&nbsp;กล่อง</td>
                                                    <td><span class="glyphicon glyphicon-chevron-down"></span></td>
                                                </tr>
                                            </table>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--# Smart Inventory -->--%>

    <!-- Pre-order -->
    <%-- <div class="container-full-content" style="display: none">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">รายการออกบิลประจำวัน</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-duplicate"></span>&nbsp;&nbsp;<span style="color: blue">5 ใบเสร็จ - ยอดรวม 120,000 บาท</span>
            </div>
            <div class="col-md-12 bar-content" style="height: 355px">
                <div class="col-md-12" style="padding-top: 10px">
                    <asp:Button runat="server" Text="แสดงข้อมูลในรูปแบบ Excel" CssClass="btn btn-danger" />&nbsp;<asp:Button runat="server" Text="พิมพ์ใบเสร็จ" CssClass="btn btn-danger" />&nbsp;|&nbsp;<asp:Button runat="server" Text="พิมพ์ใบเสร็จทั้งหมด" CssClass="btn btn-danger" />
                </div>
                <div class="col-md-12" style="padding-top: 10px">
                    <div class="col-md-12" style="padding: 0px">
                        <div class="panel panel-default">
                            <div class="panel-body" style="max-height: 270px; overflow-y: scroll;">
                                <!--<div class="pull-right">
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-success btn-filter" data-target="pagado">Pagado</button>
                                        <button type="button" class="btn btn-warning btn-filter" data-target="pendiente">Pendiente</button>
                                        <button type="button" class="btn btn-danger btn-filter" data-target="cancelado">Cancelado</button>
                                        <button type="button" class="btn btn-default btn-filter" data-target="all">Todos</button>
                                    </div>
                                </div>-->
                                <div class="table-container">
                                    <table class="table table-filter">
                                        <tbody>
                                            <tr data-status="pagado">
                                                <td>
                                                    <div class="ckbox">
                                                        <input type="checkbox" id="checkbox1">
                                                        <label for="checkbox1"></label>
                                                    </div>
                                                </td>
                                                <!--<td>
                                                    <a href="javascript:;" class="star">
                                                        <i class="glyphicon glyphicon-star"></i>
                                                    </a>
                                                </td>-->
                                                <td>
                                                    <div>
                                                        <!--<a href="#" class="pull-left">
                                                            <img src="https://s3.amazonaws.com/uifaces/faces/twitter/fffabs/128.jpg" class="media-photo">
                                                        </a>-->
                                                        <div>
                                                            <span class="pull-right"><span class="glyphicon glyphicon-ok-sign"></span>&nbsp; มิถุนายน 01, 2016</span>
                                                            <span class="title-order"><a href="#" style="color: blue">PO-20160601-0001</a>
                                                                <span class="pull-right">ยอดรวม&nbsp;<span class="label-text-amount">30,000&nbsp;บาท</span></span>
                                                            </span>
                                                            <p class="label-text-order">ร้านสมใจนึก&nbsp;,&nbsp;พนักงานขาย : น.ส. อรวรรณ บุญส่ง (SP-300010) </p>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr data-status="pagado">
                                                <td>
                                                    <div class="ckbox">
                                                        <input type="checkbox">
                                                        <label for="checkbox1"></label>
                                                    </div>
                                                </td>
                                                <!--<td>
                                                    <a href="javascript:;" class="star">
                                                        <i class="glyphicon glyphicon-star"></i>
                                                    </a>
                                                </td>-->
                                                <td>
                                                    <div>
                                                        <!--<a href="#" class="pull-left">
                                                            <img src="https://s3.amazonaws.com/uifaces/faces/twitter/fffabs/128.jpg" class="media-photo">
                                                        </a>-->
                                                        <div>
                                                            <span class="pull-right"><span class="glyphicon glyphicon-ok-sign"></span>&nbsp; มิถุนายน 01, 2016</span>
                                                            <span class="title-order"><a href="#" style="color: blue">PO-20160601-0002</a>
                                                                <span class="pull-right">ยอดรวม&nbsp;<span class="label-text-amount">5,500&nbsp;บาท</span></span>
                                                            </span>
                                                            <p class="label-text-order">ร้านพาขวัญ&nbsp;,&nbsp;พนักงานขาย : น.ส. อรวรรณ บุญส่ง (SP-300010) </p>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr data-status="pagado">
                                                <td>
                                                    <div class="ckbox">
                                                        <input type="checkbox">
                                                        <label for="checkbox1"></label>
                                                    </div>
                                                </td>
                                                <!--<td>
                                                    <a href="javascript:;" class="star">
                                                        <i class="glyphicon glyphicon-star"></i>
                                                    </a>
                                                </td>-->
                                                <td>
                                                    <div>
                                                        <!--<a href="#" class="pull-left">
                                                            <img src="https://s3.amazonaws.com/uifaces/faces/twitter/fffabs/128.jpg" class="media-photo">
                                                        </a>-->
                                                        <div>
                                                            <span class="pull-right"><span class="glyphicon glyphicon-ok-sign"></span>&nbsp; มิถุนายน 01, 2016</span>
                                                            <span class="title-order"><a href="#" style="color: blue">PO-20160601-0003</a>
                                                                <span class="pull-right">ยอดรวม&nbsp;<span class="label-text-amount">72,000&nbsp;บาท</span></span>
                                                            </span>
                                                            <p class="label-text-order">นายสมชาย ใจแจ๋ม&nbsp;,&nbsp;พนักงานขาย : น.ส. อรวรรณ บุญส่ง (SP-300010) </p>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr data-status="pagado">
                                                <td>
                                                    <div class="ckbox">
                                                        <input type="checkbox">
                                                        <label for="checkbox1"></label>
                                                    </div>
                                                </td>
                                                <!--<td>
                                                    <a href="javascript:;" class="star">
                                                        <i class="glyphicon glyphicon-star"></i>
                                                    </a>
                                                </td>-->
                                                <td>
                                                    <div>
                                                        <!--<a href="#" class="pull-left">
                                                            <img src="https://s3.amazonaws.com/uifaces/faces/twitter/fffabs/128.jpg" class="media-photo">
                                                        </a>-->
                                                        <div>
                                                            <span class="pull-right"><span class="glyphicon glyphicon-ok-sign"></span>&nbsp; มิถุนายน 01, 2016</span>
                                                            <span class="title-order"><a href="#" style="color: blue">PO-20160601-0004</a>
                                                                <span class="pull-right">ยอดรวม&nbsp;<span class="label-text-amount">1,200&nbsp;บาท</span></span>
                                                            </span>
                                                            <p class="label-text-order">ร้าน S&D Seafood&nbsp;,&nbsp;พนักงานขาย : น.ส. อรวรรณ บุญส่ง (SP-300010) </p>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr data-status="pagado">
                                                <td>
                                                    <div class="ckbox">
                                                        <input type="checkbox">
                                                        <label for="checkbox1"></label>
                                                    </div>
                                                </td>
                                                <!--<td>
                                                    <a href="javascript:;" class="star">
                                                        <i class="glyphicon glyphicon-star"></i>
                                                    </a>
                                                </td>-->
                                                <td>
                                                    <div>
                                                        <!--<a href="#" class="pull-left">
                                                            <img src="https://s3.amazonaws.com/uifaces/faces/twitter/fffabs/128.jpg" class="media-photo">
                                                        </a>-->
                                                        <div>
                                                            <span class="pull-right"><span class="glyphicon glyphicon-ok-sign"></span>&nbsp; มิถุนายน 01, 2016</span>
                                                            <span class="title-order"><a href="#" style="color: blue">PO-20160601-0005</a>
                                                                <span class="pull-right">ยอดรวม&nbsp;<span class="label-text-amount">11,300&nbsp;บาท</span></span>
                                                            </span>
                                                            <p class="label-text-order">หจก. นานอก&nbsp;,&nbsp;พนักงานขาย : น.ส. อรวรรณ บุญส่ง (SP-300010) </p>
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
            </div>
        </div>
    </div>--%>
    <!--# Pre-order -->

    <!-- Invoice -->
    <%-- <div class="container-full-content" style="display: none">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">รายการแจ้งการเก็บบิลประจำวัน</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-tags"></span>&nbsp;&nbsp;<span style="color: blue">6 บิล - ยอดรวม 72,000 บาท</span>
            </div>
            <div class="col-md-12 bar-content" style="height: 460px">
                <div class="col-md-12" style="padding-top: 10px">
                    <asp:Button runat="server" Text="แสดงข้อมูลในรูปแบบ Excel" CssClass="btn btn-primary" />&nbsp;<asp:Button runat="server" Text="พิมพ์บิล" CssClass="btn btn-primary" />&nbsp;|&nbsp;<asp:Button runat="server" Text="พิมพ์บิลทั้งหมด" CssClass="btn btn-primary" />
                </div>
                <div class="col-md-12" style="padding-top: 10px">
                    <div class="panel panel-default">
                        <div class="panel-body" style="max-height: 320px; overflow-y: scroll;">
                            <div class="table-container">
                                <table id="mytableee" class="table table-bordred table-striped">
                                    <thead>
                                        <th>
                                            <input type="checkbox" id="checkall" /></th>
                                        <th style="width: 180px">หมายเลขบิล</th>
                                        <th style="width: 200px">ลูกค้า</th>
                                        <th>ที่อยู่</th>
                                        <th style="width: 100px">ยอดรวม</th>
                                        <th style="width: 250px">พนง.ขาย</th>
                                        <!--<th>Edit</th>

                                        <th>Delete</th>-->
                                    </thead>
                                    <tbody>

                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkthis" /></td>
                                            <td style="color: blue">INV-20160601-00001</td>
                                            <td>ร้านมุมอร่อย</td>
                                            <td>51/167 ม.3 ต.คลอง 1 อ.คลองหลวง จ.ปทุมธานี</td>
                                            <td style="text-align: right; color: Green; font-size: 16px">10,800</td>
                                            <td>น.ส. อรวรรณ บุญส่ง (SP-300010)</td>
                                            <!--<td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Edit">
                                                                <button class="btn btn-primary btn-xs" data-title="Edit" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Delete">
                                                                <button class="btn btn-danger btn-xs" data-title="Delete" data-toggle="modal" data-target="#delete"><span class="glyphicon glyphicon-trash"></span></button>
                                                            </p>
                                                        </td>-->
                                        </tr>

                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkthis" /></td>
                                            <td style="color: blue">INV-20160601-00002</td>
                                            <td>ร้านสุขสบายใจ</td>
                                            <td>13/1 ม.8 ต.คลอง 3 อ.คลองหลวง จ.ปทุมธานี</td>
                                            <td style="text-align: right; color: Green; font-size: 16px">4,200</td>
                                            <td>น.ส. อรวรรณ บุญส่ง (SP-300010)</td>
                                            <!--<td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Edit">
                                                                <button class="btn btn-primary btn-xs" data-title="Edit" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Delete">
                                                                <button class="btn btn-danger btn-xs" data-title="Delete" data-toggle="modal" data-target="#delete"><span class="glyphicon glyphicon-trash"></span></button>
                                                            </p>
                                                        </td>-->
                                        </tr>

                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkthis" /></td>
                                            <td style="color: blue">INV-20160601-00003</td>
                                            <td>ร้านแสนดี</td>
                                            <td>17 ม.9 ต.คลอง 3 อ.คลองหลวง จ.ปทุมธานี</td>
                                            <td style="text-align: right; color: Green; font-size: 16px">8,000</td>
                                            <td>น.ส. อรวรรณ บุญส่ง (SP-300010)</td>
                                            <!--<td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Edit">
                                                                <button class="btn btn-primary btn-xs" data-title="Edit" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Delete">
                                                                <button class="btn btn-danger btn-xs" data-title="Delete" data-toggle="modal" data-target="#delete"><span class="glyphicon glyphicon-trash"></span></button>
                                                            </p>
                                                        </td>-->
                                        </tr>

                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkthis" /></td>
                                            <td style="color: blue">INV-20160601-00004</td>
                                            <td>นายสุรเดช ดำรอด</td>
                                            <td>99/432 ม.10 ต.คลอง 10 อ.คลองหลวง จ.ปทุมธานี</td>
                                            <td style="text-align: right; color: Green; font-size: 16px">780</td>
                                            <td>น.ส. อรวรรณ บุญส่ง (SP-300010)</td>
                                            <!--<td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Edit">
                                                                <button class="btn btn-primary btn-xs" data-title="Edit" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Delete">
                                                                <button class="btn btn-danger btn-xs" data-title="Delete" data-toggle="modal" data-target="#delete"><span class="glyphicon glyphicon-trash"></span></button>
                                                            </p>
                                                        </td>-->
                                        </tr>

                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkthis" /></td>
                                            <td style="color: blue">INV-20160601-00005</td>
                                            <td>หจก. อินทร</td>
                                            <td>33/1 ม.1 ต.คลอง 12 อ.คลองหลวง จ.ปทุมธานี</td>
                                            <td style="text-align: right; color: Green">40,000</td>
                                            <td>น.ส. อรวรรณ บุญส่ง (SP-300010)</td>
                                            <!--<td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Edit">
                                                                <button class="btn btn-primary btn-xs" data-title="Edit" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Delete">
                                                                <button class="btn btn-danger btn-xs" data-title="Delete" data-toggle="modal" data-target="#delete"><span class="glyphicon glyphicon-trash"></span></button>
                                                            </p>
                                                        </td>-->
                                        </tr>

                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkthis" /></td>
                                            <td style="color: blue">INV-20160601-00006</td>
                                            <td>หจก. กิริติ</td>
                                            <td>19 ม.1 ต.คลอง 1 อ.คลองหลวง จ.ปทุมธานี</td>
                                            <td style="text-align: right; color: Green">8,220</td>
                                            <td>น.ส. อรวรรณ บุญส่ง (SP-300010)</td>
                                            <!--<td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Edit">
                                                                <button class="btn btn-primary btn-xs" data-title="Edit" data-toggle="modal" data-target="#edit"><span class="glyphicon glyphicon-pencil"></span></button>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p data-placement="top" data-toggle="tooltip" title="Delete">
                                                                <button class="btn btn-danger btn-xs" data-title="Delete" data-toggle="modal" data-target="#delete"><span class="glyphicon glyphicon-trash"></span></button>
                                                            </p>
                                                        </td>-->
                                        </tr>
                                    </tbody>

                                </table>
                            </div>

                            <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="edit" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button>
                                            <h4 class="modal-title custom_align" id="Heading">Edit Your Detail</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group">
                                                <input class="form-control " type="text" placeholder="Mohsin">
                                            </div>
                                            <div class="form-group">

                                                <input class="form-control " type="text" placeholder="Irshad">
                                            </div>
                                            <div class="form-group">
                                                <textarea rows="2" class="form-control" placeholder="CB 106/107 Street # 11 Wah Cantt Islamabad Pakistan"></textarea>


                                            </div>
                                        </div>
                                        <div class="modal-footer ">
                                            <button type="button" class="btn btn-warning btn-lg" style="width: 100%;"><span class="glyphicon glyphicon-ok-sign"></span>Update</button>
                                        </div>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>

                            <!--<div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-labelledby="edit" aria-hidden="true">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button>
                                            <h4 class="modal-title custom_align" id="Heading">Delete this entry</h4>
                                        </div>
                                        <div class="modal-body">

                                            <div class="alert alert-danger"><span class="glyphicon glyphicon-warning-sign"></span>Are you sure you want to delete this Record?</div>

                                        </div>
                                        <div class="modal-footer ">
                                            <button type="button" class="btn btn-success"><span class="glyphicon glyphicon-ok-sign"></span>Yes</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal"><span class="glyphicon glyphicon-remove"></span>No</button>
                                        </div>
                                    </div>
                                   
                                </div>
                               
                            </div>-->
                        </div>

                        <div class="panel-footer" style="height: 55px">
                            <div class="clearfix"></div>
                            <ul class="pagination pull-right">
                                <li class="disabled"><a href="#"><span class="glyphicon glyphicon-chevron-left"></span></a></li>
                                <li class="active"><a href="#">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">4</a></li>
                                <li><a href="#">5</a></li>
                                <li><a href="#"><span class="glyphicon glyphicon-chevron-right"></span></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <!--# Invoice -->

    <!-- General Info-->
    <%-- <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none">
                <span class="title">ข้อมูลทั่วไป</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-info-sign"></span><!--<img src="Images/help-contents.png" />-->
            </div>
            <div class="col-md-12 bar-content" style="height: 300px; padding-top: 20px">
                <div class="col-md-4">
                    <div class="col-md-12 label-text2">
                        <p>ประจำวันที่ : &nbsp;<span id="date" style="color: blue"></span> </p>
                    </div>
                </div>
                <div class="col-md-6">
                </div>
                <div class="col-md-12" style="height: 20px">
                </div>
                <!-- Content-->
                <div class="row">
                    <div class="col-md-4">
                        <div class="col-md-8 label-text">
                            <p>ยอดสั่งสินค้ากับ CP MEIJI :</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>150,000 </p>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 label-text">
                            <p>ยอดเบิกสินค้าของ SP :</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>180,000 </p>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="col-md-8 label-text">
                            <p>ยอดขายสินค้ารวม :</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>355,000 </p>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 label-text">
                            <p>ยอดตั้งหนี้รวม :</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>40,000 </p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-8 label-text">
                            <p>ยอดคงค้าง CP MEIJI :</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>125,000 </p>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 label-text">
                            <p>&nbsp;</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>&nbsp;</p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-8 label-text">
                            <p>สถาณะการสั่งสินค้า CP MEIJI :</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>รับแล้ว </p>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 label-text">
                            <p>&nbsp;</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>&nbsp;</p>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <hr />
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-8 label-text">
                            <p>จำนวนพนักงาน :</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>15 </p>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 label-text">
                            <p>&nbsp;</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p></p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-8 label-text">
                            <p>จำนวนลูกค้า :</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p>817 </p>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 label-text">
                            <p>&nbsp;</p>
                        </div>
                        <div class="col-md-3 label-value">
                            <p></p>
                        </div>
                    </div>

                </div>
                <!--#Content-->
            </div>
        </div>
    </div>--%>
    <!--# General Info-->

    <!--label: '# of Votes',-->
    <script>
        var ctx = document.getElementById("myChart");
        var ptx = document.getElementById("pieChart");
        var mix = document.getElementById("mixedChart").getContext('2d');

        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน"],
                datasets: [{

                    label: 'ยอดขาย',
                    data: [230000, 550000, 490000, 670000, 712000, 773450],
                    backgroundColor: [
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 99, 132, 0.2)',

                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',

                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1,
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 1,
                    pointRadius: 5,
                    pointHitRadius: 10,
                    spanGaps: false,
                },
                {
                    label: 'เป้าหมาย',
                    data: [450000, 550000, 650000, 750000, 855000, 950000],
                    backgroundColor: [
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 99, 132, 0.2)',

                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',

                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 26, 26, 1)'
                    ],
                    borderWidth: 1,
                    pointBorderColor: "rgba(255, 102, 102,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(255, 26, 26,1)",
                    pointHoverBorderWidth: 1,
                    pointRadius: 5,
                    pointHitRadius: 10,
                    spanGaps: false,
                }]
            },

            options: {
                responsive: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            labelOffset: 1
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            display: true
                        }
                    }]
                },
                title: {
                    display: true,
                    text: 'ยอดสั่งซื้อ ประจำปี 2016',
                    fontFamily: 'Prompt',
                    fontSize: 16,
                    fontColor: '#000',
                    fontStyle: 'normal',
                    padding: 20

                },
                legend: {
                    display: false
                }

            }
        });

        var myPieChart = new Chart(ptx, {
            type: 'doughnut',
            data: {
                labels: ["Pasteurized Fresh Milk", "Paigen Drinking Yoghurt", "Low Fat Yoghurt", "Bulgaria Yoghurt "],
                datasets: [{
                    label: 'Sum Amount',
                    data: [230000, 150000, 90000, 303450],
                    backgroundColor: [
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        //'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(255, 206, 86, 1)',
                        //'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },

            options: {
                responsive: false,
                /*scales: {
                    display:false,
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            display: false
                        },
                        gridLines:{
                            display: false
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            display: false
                        },
                        gridLines: {
                            display: false
                        }
                    }]
                },*/
                title: {
                    display: true,
                    text: 'ยอดขายสินค้า/แยกตามประเภท ประจำเดือน มิถุนายน',
                    fontFamily: 'Prompt',
                    fontSize: 16,
                    fontColor: '#000',
                    fontStyle: 'normal',
                    padding: 20

                },
                legend: {
                    display: true,
                    position: 'right',
                    fontColor: '#fff'
                }

            }
        });

        var mixedChart = new Chart(mix, {
            type: 'bar',
            data: {
                datasets: [{
                    label: 'Bar Dataset',
                    data: [10, 20, 30, 40]
                }, {
                    label: 'Line Dataset',
                    data: [50, 50, 50, 50],

                    // Changes this dataset to become a line
                    type: 'line'
                }],
                labels: ['January', 'February', 'March', 'April']
            },
            options: options
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $(".demo").bootstrapNews({
                newsPerPage: 4,
                navigation: true,
                autoplay: true,
                direction: 'up', // up or down
                animationSpeed: 'normal',
                newsTickerInterval: 3000, //4 secs
                pauseOnHover: true,
                onStop: null,
                onPause: null,
                onReset: null,
                onPrev: null,
                onNext: null,
                onToDo: null
            });
        });

        $(function () {
            $(".alert").bootstrapNews({
                newsPerPage: 4,
                navigation: true,
                autoplay: false,
                direction: 'up', // up or down
                animationSpeed: 'normal',
                newsTickerInterval: 3000, //4 secs
                pauseOnHover: true,
                onStop: null,
                onPause: null,
                onReset: null,
                onPrev: null,
                onNext: null,
                onToDo: null
            });
        });
    </script>
</asp:Content>

