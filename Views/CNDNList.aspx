<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CNDNList.aspx.cs" Inherits="Views_CNDNList" uiculture="th" culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

        function validateFloatKeyPress(el, evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            if (charCode == 46 && el.value.indexOf(".") !== -1) {
                return false;
            }

            if (el.value.indexOf(".") !== -1) {
                var range = document.selection.createRange();

                if (range.text != "") {
                }
                else {
                    var number = el.value.split('.');
                    if (number.length == 2 && number[1].length > 1)
                        return false;
                }
            }

            return true;
        }

        function ClearValue(Quantity) {
            global_Quantity = Quantity.value == '' ? '0' : Quantity.value;
        }
        var global_quantity = 0;

        function UpdateField(Quantity, hf_Qty_inv_) {
            var tmp_Quantity = Quantity.value == '' ? '0' : Quantity.value;
            //alert(tmp_Quantity);
            var calGrandTotalAmount = parseFloat(document.getElementById('<%=txtSAM_CN_DN_Quantity.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtSAM_CN_DN_Quantity.ClientID%>').value.split(',').join(''));
           
            //alert(calGrandTotalAmount);
    
            if (parseInt(tmp_Quantity) > parseInt(global_Quantity)) {

                calGrandTotalAmount = parseInt(calGrandTotalAmount) + (parseInt(tmp_Quantity) - parseInt(global_Quantity));
                //alert(calGrandTotalAmount+'gg1');

            } else {

                calGrandTotalAmount = parseInt(calGrandTotalAmount) - (parseInt(global_Quantity) - parseInt(tmp_Quantity));
                //alert(calGrandTotalAmount + 'gg2');
            }

            var caldiff = (parseInt(hf_Qty_inv_.value) - parseInt(tmp_Quantity));

           // alert(caldiff);

          if (caldiff < 0)
            {
             
              alert('ไม่สามารถระบุจำนวนมากกว่าจำนวนใน Invoice ' + parseInt(hf_Qty_inv_.value) + ' ได้');
              if (parseInt(tmp_Quantity) < parseInt(calGrandTotalAmount))
              {
                  document.getElementById('<%=txtSAM_CN_DN_Quantity.ClientID%>').value = parseInt(calGrandTotalAmount) - parseInt(tmp_Quantity); 
                  Quantity.value = '';
              }
              else
              {
                  document.getElementById('<%=txtSAM_CN_DN_Quantity.ClientID%>').value = parseInt(tmp_Quantity)- parseInt(calGrandTotalAmount) ; 
                  Quantity.value = '';
              }
     
            }
            else
            {
              document.getElementById('<%=txtSAM_CN_DN_Quantity.ClientID%>').value = parseInt(calGrandTotalAmount); 
              if(Quantity.value =='0')
              {
                  Quantity.value = '';
              }
            }
                        
             
           

        }

        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num + '.' + cents);
        }
    </script>

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

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Panel ID="pnlGrid" Visible="true" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ลดหนี้/เพิ่มหนี้</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบแจ้งหนี้:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchBilling_ID" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่ใบแจ้งหนี้:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchBillingDate" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtSearchBillingDate" runat="server"
                                            TargetControlID="txtSearchBillingDate" PopupButtonID="cldtxtSearchBillingDate" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบลดหนี้/เพิ่มหนี้:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchInvoice_No" runat="server" CssClass="form-control"></asp:textbox>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่ใบลดหนี้/เพิ่มหนี้:</label>
                                    <div class="col-md-4">
                                        <asp:textbox id="txtSearchInvoice_Date" runat="server" CssClass="form-control"></asp:textbox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtSearchInvoice_Date" runat="server"
                                            TargetControlID="txtSearchInvoice_Date" PopupButtonID="cldtxtSearchInvoice_Date" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" onclick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <%--<asp:Button ID="ButtonAddNew" class="btn btn-primary" runat="server" Text="สร้าง" onclick="ButtonAddNew_Click" />--%>
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <div style="overflow: auto">
                                    <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
	                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
		                                    <center>ไม่พบข้อมูล</center>
	                                    </div>
                                    </asp:Panel>
                                    <asp:GridView ID="GridViewCNDN"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        ShowFooter="false"
                                        CellPadding="0" ForeColor="#333333"
                                        onrowcommand="GridViewCNDN_RowCommand"
                                        CssClass="table table-striped table-bordered table-condensed"
                                        AllowPaging="true" PageSize="10" OnDataBound="GridViewCNDN_DataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                        <asp:LinkButton ID="lnkB" runat="server" CssClass="btn btn-mini btn-danger" Text="ลบ" Width="48px" CommandArgument='<%# Container.DataItemIndex %>' CommandName="_Delete"></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" itemstyle-horizontalalign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBCreateNew" runat="server" CssClass="btn btn-mini btn-primary" Text="สร้าง"
                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="AddNewCNDN"></asp:LinkButton>
                                                </ItemTemplate>
                                                <itemstyle Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เลขที่ใบแจ้งหนี้" ShowHeader="False" headerstyle-wrap="false">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkBBillingID" runat="server" CssClass="btn btn-link"
                                                        Text='<%# Eval("Billing_ID") %>'
                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="View" style="font-weight: bold; text-decoration: underline"></asp:LinkButton>--%>
                                                    <%--<asp:LinkButton ID="lnkBBillingID" runat="server" CssClass="btn btn-link"
                                                        Text='<%# Eval("Invoice_No") %>'
                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="View" style="font-weight: bold; text-decoration: underline"></asp:LinkButton>--%>
                                                    <asp:Label id="lnkBBillingID" runat="server" style="color: blue" Text='<%# Eval("Invoice_No") %>'></asp:Label>
                                                </ItemTemplate>
                                                <itemstyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่ใบแจ้งหนี้" headerstyle-wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_Billing_Date" runat="server" style="color: blue" Text='<%# Eval("Invoice_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เลขที่ใบลดหนี้/เพิ่มหนี้" headerstyle-wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_CNDN_ID" runat="server" style="color: blue" Text='<%# Eval("CNDN_No") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่ใบลดหนี้/เพิ่มหนี้" headerstyle-wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_CNDN_Date" runat="server" style="color: blue" Text='<%# Eval("CNDN_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภทเอกสาร" headerstyle-wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_BillingType" runat="server" Text='<%# Eval("SAM_CN_DN_Type") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภทOrder" headerstyle-wrap="false" visible="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_OrderType" runat="server" Text='<%# Eval("Order_Type") %>' style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภทSAM CN/DN" headerstyle-wrap="false" visible="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_SAM_CN_DN_Type" runat="server" style="color: blue"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SAM CN/DN No." headerstyle-wrap="false">
                                                <ItemTemplate>
                                                    <%--<asp:Label id="lbl_SAM_CN_DN_No" runat="server" style="color: blue" Text='<%# Eval("SAM_CN_DN_No") %>'></asp:Label>--%>
                                                    <asp:LinkButton ID="lbl_SAM_CN_DN_No" runat="server" CssClass="btn btn-link"
                                                        Text='<%# Eval("SAM_CN_DN_No") %>'
                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="View" style="font-weight: bold; text-decoration: underline"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่ SAM CN/DN" headerstyle-wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_SAM_CN_DN_Date" runat="server" style="color: blue" Text='<%# Eval("SAM_CN_DN_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="จำนวน SAM CN/DN" headerstyle-wrap="false" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_SAM_CN_DN_Quantity" runat="server" style="color: blue" Text='<%# Eval("SAM_CN_DN_Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถานะ SAM CN/DN" headerstyle-wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_SAM_CN_DN_Status" runat="server" style="color: blue" Text='<%# Eval("SAM_CN_DN_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" itemstyle-horizontalalign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBConfirmCNDN" runat="server" CssClass="btn btn-mini btn-primary" Text="ยืนยัน SAP CN/DN" CommandArgument='<%# Container.DataItemIndex %>' CommandName="ConfirmCNDN"></asp:LinkButton>
                                                    <%--<asp:LinkButton ID="lnkBConfirmCNDN" runat="server" CssClass="btn btn-link" CommandArgument='<%# Eval("Billing_ID") %>'
                                                    CommandName="View" style="font-weight: bold; text-decoration: underline" Text='<%# Eval("Invoice_No") %>'></asp:LinkButton>--%>
                                                </ItemTemplate>
                                                <itemstyle Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" visible="false">
                                                <ItemTemplate>
                                                    <asp:Label id="lbl_PO_Number" runat="server" style="color: blue" Text='<%# Eval("Billing_ID") %>'></asp:Label>
                                                    <asp:Label id="lbl_CN_Number" runat="server" style="color: blue" Text='<%# Eval("CNDN_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <pagertemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 70%">
                                                    <asp:Label ID="MessageLabel"
                                                        ForeColor="Blue"
                                                        Text="เลือกหน้า :"
                                                        runat="server" />
                                                    <asp:DropDownList ID="PageDropDownList"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                                        onchange="myApp.showPleaseWait();"
                                                        runat="server" />
                                                </td>

                                                <td style="width: 70%; text-align: right">
                                                    <asp:Label ID="CurrentPageLabel"
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
            </asp:Panel>

            <asp:Panel ID="pnlForm" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">ลดหนี้/เพิ่มหนี้</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <asp:panel id="pnlnewCNDN" runat="server">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">เลขที่ใบแจ้งหนี้:</label>
                                        <div class="col-md-4">
                                            <asp:textbox id="txtInvoice_No" runat="server" CssClass="form-control" enabled="false"></asp:textbox>
                                        </div>
                                        <label class="col-md-2 control-label">วันที่ใบแจ้งหนี้:</label>
                                        <div class="col-md-4">
                                            <asp:textbox id="txtInvoice_Date" runat="server" CssClass="form-control" enabled="false"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">ประเภทเอกสาร:</label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlSAM_CN_DN_Type" runat="server" style="border-left: 4px solid #ed1c24;" CssClass="form-control" 
                                                autopostback="true" onselectedindexchanged="ddlSAM_CN_DN_Type_SelectedIndexChanged">
                                                <asp:ListItem>CN</asp:ListItem>
                                                <asp:ListItem>DN</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-md-2 control-label">SAM CN/DN No.:</label>
                                        <div class="col-md-4">
                                            <asp:textbox id="txtSAM_CN_DN_No" runat="server" CssClass="form-control" Enabled="false"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">วันที่SAM CN/DN:</label>
                                        <div class="col-md-4">
                                            <asp:textbox id="txtSAM_CN_DN_Date" runat="server" CssClass="form-control" enabled="false"></asp:textbox>
                                        </div>
                                        <label class="col-md-2 control-label">จำนวนSAM CN/DN:</label>
                                        <div class="col-md-4">
                                            <asp:textbox id="txtSAM_CN_DN_Quantity" runat="server" enabled="false" CssClass="form-control" TextMode="Number"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">สถานะSAM CN/DN:</label>
                                        <div class="col-md-4">
                                            <asp:textbox id="txtSAM_CN_DN_Status" runat="server" CssClass="form-control" enabled="false"></asp:textbox>
                                        </div>
                                        <label class="col-md-2 control-label">CN/DN ทั้งใบ:</label>
                                        <div class="col-md-4">
                                            <asp:CheckBox id="chkCNDN" runat="server" autopostback="true" oncheckedchanged="chkCNDN_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div class="form-group"></div>
                                </div>
                            </asp:panel>


                            <div class="tabbable-panel" style="margin-top: 20px">
                                <div class="tabbable-line">
                                    <ul class="nav nav-tabs ">
                                        <li class="active " style="width: 180px">
                                            <img src="../Images/tab1.png" id="imgTab1" style="position: absolute; top: -50px; z-index: 999" />
                                            <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center">นมพาสเจอร์ไรส์</a>

                                        </li>
                                        <li style="width: 160px">
                                            <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center">นมเปรี้ยว </a>

                                        </li>
                                        <li style="width: 160px">
                                            <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;โยเกิร์ตเมจิ </a>

                                        </li>
                                        <li style="width: 160px">
                                            <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_4" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;นมเปรี้ยวไพเก้น </a>

                                        </li>
                                        <li style="width: 150px">
                                            <a href="#tab_default_5" id="tab6" data-toggle="tab" style="padding-left: 30px" class="text-center">อื่นๆ </a>

                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_default_1">
                                            <asp:GridView ID="GridViewCNDN_1"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                showheaderwhenempty="true"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewCNDN_1_RowDataBound"
                                                ondatabound="GridViewCNDN_1_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Item" runat="server" Text='<%# Eval("index") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_PricePerUnit" runat="server" Text='<%# Eval("Unit_of_item_ID") %>' style="color: blue"></asp:Label>
                                                            <span> <asp:HiddenField ID="hf_Qty_inv" runat="server"  Value='<%#Eval("Qty")%>'/></span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:Textbox ID="txtOrderingAmount" runat="server" Text='<%# Eval("Quantity") %>'
                                                                width="84px" style="color: blue; text-align: right"></asp:Textbox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Vat" runat="server" Text='<%# Eval("Vat") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Agent_Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Agent_Price" runat="server" Text='<%# Eval("Agent_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                  <%--  <asp:TemplateField HeaderText="QTY" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_QTY" runat="server" Text='<%# Eval("Qty") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>--%>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_2">
                                            <asp:GridView ID="GridViewCNDN_2"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                showheaderwhenempty="true"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewCNDN_2_RowDataBound"
                                                ondatabound="GridViewCNDN_2_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Item" runat="server" Text='<%# Eval("index") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_PricePerUnit" runat="server" Text='<%# Eval("Unit_of_item_ID") %>' style="color: blue"></asp:Label>
                                                           <span> <asp:HiddenField ID="hf_Qty_inv" runat="server"  Value='<%#Eval("Qty")%>'/></span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:Textbox ID="txtOrderingAmount" runat="server" Text='<%# Eval("Quantity") %>'
                                                                width="84px" style="color: blue; text-align: right"></asp:Textbox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Vat" runat="server" Text='<%# Eval("Vat") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Agent_Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Agent_Price" runat="server" Text='<%# Eval("Agent_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_3">
                                            <asp:GridView ID="GridViewCNDN_3"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                showheaderwhenempty="true"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewCNDN_3_RowDataBound"
                                                ondatabound="GridViewCNDN_3_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Item" runat="server" Text='<%# Eval("index") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_PricePerUnit" runat="server" Text='<%# Eval("Unit_of_item_ID") %>' style="color: blue"></asp:Label>
                                                           <span> <asp:HiddenField ID="hf_Qty_inv" runat="server"  Value='<%#Eval("Qty")%>'/></span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:Textbox ID="txtOrderingAmount" runat="server" Text='<%# Eval("Quantity") %>'
                                                                width="84px" style="color: blue; text-align: right"></asp:Textbox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Vat" runat="server" Text='<%# Eval("Vat") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Agent_Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Agent_Price" runat="server" Text='<%# Eval("Agent_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_4">
                                            <asp:GridView ID="GridViewCNDN_4"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                showheaderwhenempty="true"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewCNDN_4_RowDataBound"
                                                ondatabound="GridViewCNDN_4_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Item" runat="server" Text='<%# Eval("index") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_PricePerUnit" runat="server" Text='<%# Eval("Unit_of_item_ID") %>' style="color: blue"></asp:Label>
                                                           <span> <asp:HiddenField ID="hf_Qty_inv" runat="server"  Value='<%#Eval("Qty")%>'/></span>
                                                             </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:Textbox ID="txtOrderingAmount" runat="server" Text='<%# Eval("Quantity") %>'
                                                                width="84px" style="color: blue; text-align: right"></asp:Textbox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Vat" runat="server" Text='<%# Eval("Vat") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Agent_Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Agent_Price" runat="server" Text='<%# Eval("Agent_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_5">
                                            <asp:GridView ID="GridViewCNDN_5"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                showheaderwhenempty="true"
                                                CellPadding="0"
                                                ForeColor="#333333" onrowdatabound="GridViewCNDN_5_RowDataBound"
                                                ondatabound="GridViewCNDN_5_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Item" runat="server" Text='<%# Eval("index") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_PricePerUnit" runat="server" Text='<%# Eval("Unit_of_item_ID") %>' style="color: blue"></asp:Label>
                                                           <span> <asp:HiddenField ID="hf_Qty_inv" runat="server"  Value='<%#Eval("Qty")%>'/></span>
                                                             </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวน" itemstyle-horizontalalign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:Textbox ID="txtOrderingAmount" runat="server" Text='<%# Eval("Quantity") %>'
                                                                width="84px" style="color: blue; text-align: right"></asp:Textbox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Vat" runat="server" Text='<%# Eval("Vat") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Agent_Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Agent_Price" runat="server" Text='<%# Eval("Agent_Price") %>' style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:hiddenfield id="hdnBilling_ID" runat="server" />
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" onclick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>

                                <asp:hiddenfield id="btnSaveMode" runat="server" />
                            </div>

                            <div class="col-md-12 text-center">
                                <br />
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

