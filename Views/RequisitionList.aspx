<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true"
    CodeFile="RequisitionList.aspx.cs" Inherits="Views_RequisitionList" UICulture="th" Culture="th-TH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #preview {
            position: absolute;
            border: 0px solid #ccc;
            background: #333;
            padding: 0px;
            display: none;
            color: #fff;
        }

        .popover {
            position: fixed !important; /* I use !important because in this snippet bootstrap's styles overwrite mines but in general it is not nedded*/
        }

        #link_pop {
            z-index: 1;
            position: fixed;
            top: 47%;
            left: 0;
        }
    </style>

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

        $(function () {
            $("#link_pop").popover('show');
        });

        function showImage(ctl) {
            $('#' + ctl).css('display', 'block');
        }
        function hideImage(ctl) {
            $('#' + ctl).css('display', 'none');
        }

        this.imagePreview = function () {

            /* CONFIG */

            xOffset = 10;
            yOffset = 30;

            // these 2 variable determine popup's distance from the cursor
            // you might want to adjust to get the right result

            /* END CONFIG */
            $("a.preview").hover(function (e) {

                this.t = this.title;
                this.title = "";
                var c = (this.t != "") ? "<br/>" + this.t : "";

                $("body").append("<p id='preview'><img src='" + this.rel + "' alt='' width='200px' height='200px' />" + c + "</p>");
                $("#preview")
                    .css("top", (e.pageY - xOffset) + "px")
                    .css("left", (e.pageX + yOffset) + "px")

                    .fadeIn("fast");
            },
            function () {
                this.title = this.t;
                $("#preview").remove();
            });
            $("linkbutton.preview").mousemove(function (e) {
                $("#preview")
                    .css("top", (e.pageY - xOffset) + "px")
                    .css("left", (e.pageX + yOffset) + "px")

            });
        };

        $(document).ready(function () {
            imagePreview();

            document.getElementById('lblQtyTotal').value = document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value;
            document.getElementById('lblPriceTotal').value = document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value;
        });

        function pageLoad() {
            maintainSelectedTab();
            imagePreview();

            //document.getElementById('lblQtyTotal').value = document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value;
            //document.getElementById('lblPriceTotal').value = document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value;
        }

        function maintainSelectedTab() {
            var selectedTab = $("#<%=hfTab.ClientID%>");
            var tabId = selectedTab.val() != "" ? selectedTab.val() : "tab_default_1";
            $('#dvTab a[href="#' + tabId + '"]').tab('show');
            $("#dvTab a").click(function () {
                selectedTab.val($(this).attr("href").substring(1));
            });

            $("#link_pop").popover('show');
        }
        function tab1click() {
            $("#imgTab1").show();

            $("#imgTab2").hide();
            $("#imgTab3").hide();
            $("#imgTab4").hide();
            // $("#imgTab5").hide();
        }
        function tab2click() {
            $("#imgTab2").show();

            $("#imgTab1").hide();
            $("#imgTab3").hide();
            $("#imgTab4").hide();
            // $("#imgTab5").hide();
        }
        function tab3click() {
            $("#imgTab3").show();

            $("#imgTab1").hide();
            $("#imgTab2").hide();
            $("#imgTab4").hide();
            // $("#imgTab5").hide();
        }
        function tab4click() {
            $("#imgTab4").show();

            $("#imgTab1").hide();
            $("#imgTab2").hide();
            $("#imgTab3").hide();
            //$("#imgTab5").hide();
        }
        function tab5click() {
            $("#imgTab1").hide();
            $("#imgTab2").hide();
            $("#imgTab3").hide();
            $("#imgTab4").hide();
            // $("#imgTab5").hide();
        }

        function validateFloatKeyPress(el, evt) {
            //  alert(el.value);
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

        var global_Quantity = 0;


        function ClearValue(textBox1, lbl_Sub_Total_Qty, lbl_Previous_Balance_Qty_, lbl_Total_Qty_, lbl_PricePerUnit) {

            global_Quantity = textBox1.value == '' ? '0' : textBox1.value;
        }

        var global_Quantity = 0;

        function UpdateField(textBox1, lbl_Sub_Total_Qty, lbl_Previous_Balance_Qty_, lbl_Total_Qty_, lbl_PricePerUnit,hfEnd_Sock_) {


            //alert(parseInt(hfEnd_Sock_.value));
            var tmp_Quantity = textBox1.value == '' ? '0' : textBox1.value;

            var cal_Total_Qty = 0;
            var calGrand_Total_Qty = parseInt(document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value.split(',').join(''));
            var calTotalPrice = parseFloat(document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value.split(',').join(''));
            var cal_Total_Qty = parseInt(tmp_Quantity) +
                parseInt(lbl_Sub_Total_Qty.innerHTML) +
                parseInt(lbl_Previous_Balance_Qty_.innerHTML);

            //var cal_Total_Qty = parseInt(tmp_Quantity) +
            //   parseInt(lbl_Previous_Balance_Qty_.innerHTML);

            var calPrice = 0;

            if (parseInt(tmp_Quantity) > parseInt(global_Quantity)) {

                calGrand_Total_Qty = (parseInt(calGrand_Total_Qty)) + (parseInt(tmp_Quantity) - parseInt(global_Quantity));
                // + parseInt(lbl_Sub_Total_Qty.innerHTML) + parseInt(lbl_Previous_Balance_Qty_.innerHTML);

                calTotalPrice = parseFloat(calTotalPrice) + parseFloat(lbl_PricePerUnit.innerHTML) * parseFloat((parseInt(tmp_Quantity) - parseInt(global_Quantity)));

            } else {

                calGrand_Total_Qty = (parseInt(calGrand_Total_Qty)) - (parseInt(global_Quantity) - parseInt(tmp_Quantity));
                //  parseInt(lbl_Sub_Total_Qty.innerHTML) +
                //  parseInt(lbl_Previous_Balance_Qty_.innerHTML);

                calTotalPrice = parseFloat(calTotalPrice) - parseFloat(lbl_PricePerUnit.innerHTML) * parseFloat((parseInt(global_Quantity) - parseInt(tmp_Quantity)));
            }

            //var caldiff = parseInt(lbl_stock_end_.innerHTML - parseInt(cal_Total_Qty));
            var caldiff = (parseInt(hfEnd_Sock_.value) - parseInt(cal_Total_Qty));
            //alert(caldiff);
            //alert(parseInt(lbl_stock_end_.innerHTML));

            
           
                if (caldiff < 0) {
                    //alert('ไม่สมารถเบิกเกินจำนวนสต๊อกที่มีอยู่ได้');
                    alert('ไม่สามารถเบิกเกินจำนวนสต๊อก ' + parseInt(hfEnd_Sock_.value) + ' ได้');
                    //textBox1.value = tmp_Quantity - textBox1.value;
                    calPrice = parseFloat(lbl_PricePerUnit.innerHTML) * parseInt(tmp_Quantity);
                    //alert(calPrice);
                    document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value = formatCurrency1(parseInt(calGrand_Total_Qty) - parseInt(tmp_Quantity));
                    document.getElementById('lblQtyTotal').value = document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value;
                    document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value = formatCurrency(parseFloat(calTotalPrice) - parseFloat(calPrice));
                    document.getElementById('lblPriceTotal').value = document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value;
                    textBox1.value = '';
                    lbl_Total_Qty_.value = lbl_Sub_Total_Qty.innerHTML;
                }
                else {
                    //lbl_stock_end_.innerHTM = caldiff;
                    lbl_Total_Qty_.value = formatCurrency1(cal_Total_Qty);
                    document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value = formatCurrency1(calGrand_Total_Qty);
                    document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value = formatCurrency(calTotalPrice);
                    document.getElementById('lblQtyTotal').value = document.getElementById('<%=txtGrand_Total_Qty.ClientID%>').value;
                    document.getElementById('lblPriceTotal').value = document.getElementById('<%=txtGrand_Total_Amount.ClientID%>').value;

                    if (parseInt(textBox1.value) == 0)
                    {
                       // alert('ไม่สามารถทำการเบิกสินค้าในจำนวนที่เป็น 0 ได้');
                        textBox1.value = '';
                    }
                    else
                    {
                       
                    }


                }

                // }
                //else
                // {
                //     alert("ไม่สามารถเบิกสินค้าจำนวน 0 ได้");
                //     textBox1.value = '';
                //     lbl_Sub_Total_Qty = lbl_Sub_Total_Qty.innerHTML;

                // }
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
        function formatCurrency1(num) {
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
            return (((sign) ? '' : '-') + num);

        }

     
      <%--  var GridId = "<%=GridViewSearchRequisition.ClientID %>";
    
    var ScrollHeight = 300;
    window.onload = function () {
       
        var grid = document.getElementById(GridId);
        var gridWidth = grid.offsetWidth;
        var gridHeight = grid.offsetHeight;
        var headerCellWidths = new Array();
        for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
            headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
        }
        grid.parentNode.appendChild(document.createElement("div"));
        var parentDiv = grid.parentNode;
 
        var table = document.createElement("table");
        for (i = 0; i < grid.attributes.length; i++) {
            if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
            }
        }
        table.style.cssText = grid.style.cssText;
        table.style.width = gridWidth + "px";
        table.appendChild(document.createElement("tbody"));
        table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
        var cells = table.getElementsByTagName("TH");
 
        var gridRow = grid.getElementsByTagName("TR")[0];
        for (var i = 0; i < cells.length; i++) {
            var width;
            if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                width = headerCellWidths[i];
            }
            else {
                width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
            }
            cells[i].style.width = parseInt(width - 3) + "px";
            gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
        }
        parentDiv.removeChild(grid);
 
        var dummyHeader = document.createElement("div");
        dummyHeader.appendChild(table);
        parentDiv.appendChild(dummyHeader);
        var scrollableDiv = document.createElement("div");
        if(parseInt(gridHeight) > ScrollHeight){
             gridWidth = parseInt(gridWidth) + 17;
        }
        scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
        scrollableDiv.appendChild(grid);
        parentDiv.appendChild(scrollableDiv);
    }--%>


    </script>

    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     
    </style>   

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>       
            <asp:HiddenField ID="hfTab" runat="server" />
            <asp:Panel ID="pnlGrid" Visible="true" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">เบิกสินค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">

                                        <asp:DropDownList ID="ddl_SPName" runat="server" CssClass="form-control"
                                            DataTextField="FullName_ddl" DataValueField="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">

                                    <label class="col-md-2 control-label">วันที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchBeginRequisition_Date" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="imgBtxtSearchBeginRequisition_Date" ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtSearchBeginRequisition_Date" runat="server"
                                            TargetControlID="txtSearchBeginRequisition_Date" PopupButtonID="cldtxtSearchBeginRequisition_Date" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSearchEndTransaction_Date" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="imgBtxtSearchEndTransaction_Date" ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtSearchEndTransaction_Date" runat="server"
                                            TargetControlID="txtSearchEndTransaction_Date" PopupButtonID="cldtxtSearchEndTransaction_Date" />
                                    </div>
                                    <%--</div>--%>
                                </div>

                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnAddNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="ButtonAddNew_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์ใบเบิกสินค้าตอนเช้า" OnClick="btnPrint_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px;padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่มีรายการเบิกสินค้า</center>
                                    </div>
                                </asp:Panel>
                                <asp:GridView ID="GridViewSearchRequisition"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333" OnRowCommand="GridViewSearchRequisition_RowCommand"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    AllowPaging="true" PageSize="10" OnDataBound="GridViewSearchRequisition_DataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ" CommandArgument='<%# Container.DataItemIndex%>' CommandName="_Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkB_Edit" runat="server"
                                                    CssClass="btn btn-mini btn-warning"
                                                    Text="แก้ไข" CommandArgument='<%# Eval("Requisition_No")%>'
                                                    CommandName="_Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อพนักงาน">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_SP_Name" runat="server" Text='<%# Eval("SP_Name")%>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่เบิก">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Requisition_Date" runat="server" Text='<%# Eval("Requisition_Date", "{0:dd/MM/yyyy}")%>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเบิก">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="LinkButton_Requisition_No" runat="server" Text='<%# Eval("Requisition_No")%>'
                                                    CommandArgument='<%# Eval("Requisition_No")%>'
                                                    CommandName="View"
                                                    Style="font-weight: bold; text-decoration: underline; text-align: left">
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ครั้งที่" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Time_No" runat="server" Text='<%# Eval("Time_No")%>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Total_Req" runat="server" Text='<%# Eval("Grand_Total_Qty","{0:N0}")%>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="จำนวนเงิน" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_Total_Amount" runat="server" Text='<%# Eval("Grand_Total_Amount","{0:N0}")%>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_Print" runat="server" OnClientClick="myApp.showPleaseWait();" CommandArgument='<%# Eval("Requisition_No")%>' 
                                                    CssClass="btn btn-mini btn-primary" Text="พิมพ์ใบเบิกสินค้า" CommandName="Print"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
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
            </asp:Panel>

            <asp:Panel ID="pnlForm" Visible="False" runat="server">

                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">เบิกสินค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อพนักงาน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlUserSP" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUserSP_SelectedIndexChanged"
                                            DataTextField="FullName_ddl" DataValueField="User_ID" AutoPostBack="true" Style="border-left: 4px solid #ed1c24;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบเบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRequisition_No" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ครั้งที่:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtTime_No" runat="server" CssClass="form-control" Enabled="false" Text="1"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">วันที่เบิก:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRequisition_Date" runat="server" CssClass="form-control" Style="border-left: 4px solid #ed1c24;" AutoPostBack="true"
                                            OnTextChanged="txtRequisition_Date_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="cldtxtRequisition_Date" runat="server"
                                            TargetControlID="txtRequisition_Date" PopupButtonID="cldtxtRequisition_Date" />
                                    </div>
                                    <label class="col-md-2 control-label">วันที่ทำรายการ:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtTransaction_Date" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">จำนวนรวม:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtGrand_Total_Qty" runat="server" CssClass="form-control" Enabled="false" Text="0"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">ราคารวม:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtGrand_Total_Amount" runat="server" CssClass="form-control" Enabled="false" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ขายแทน:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlUserRepresent" runat="server" CssClass="form-control"
                                            DataTextField="FullName_ddl" DataValueField="User_ID">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="tabbable-panel" style="margin-top: 20px">
                                <div class="tabbable-line" id="dvTab">
                                    <ul class="nav nav-tabs ">
                                        <li class="active" style="width: 180px">
                                            <img src="../Images/tab1.png" id="imgTab1" style="position: absolute; top: -50px; z-index: 999" />
                                            <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab1click()">นมพาสเจอร์ไรส์</a>
                                        </li>
                                        <li style="width: 130px">
                                            <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab2click()">นมเปรี้ยว </a>
                                        </li>
                                        <li style="width: 170px">
                                            <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab3click()">โยเกิร์ตเมจิ </a>
                                        </li>
                                        <li style="width: 170px">
                                            <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -50px; display: none; z-index: 999" />
                                            <a href="#tab_default_4" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab4click()">นมเปรี้ยวไพเก้น </a>
                                        </li>
                                        <li style="width: auto">
                                            <a href="#tab_default_5" id="tab5" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab5click()">อื่นๆ </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_default_1">

                                            <asp:GridView ID="GridViewRequisition_1"
                                                
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewRequisition_1_RowDataBound"
                                                OnDataBound="GridViewRequisition_1_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>                                                 
                                                    <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID")%>' Style="color: blue"></asp:Label>                                                    
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                            <a href='#' class="preview" style="color: blue;" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name")%>' Style="color: blue" Visible="false"></asp:Label> 
                                                            <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />                                                           
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("SP_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดฝาก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Previous_Balance_Qty" runat="server" Style="color: blue" Text='<%# Eval("Previous_Balance_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggestion_Qty" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิกแล้ว" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Sub_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Sub_Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server"  TabIndex="<%# Container.DataItemIndex %>"
                                                                Style="text-align: right; color: blue" Width="84px" AutoCompleteType="disabled"> </asp:TextBox>  
                                                            
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Total_Qty" runat="server" Style="text-align: right; color: blue" Width="84px" Enabled="false"></asp:TextBox>                                            
                                                            <span> <asp:HiddenField ID="hfEnd_Sock1" runat="server"  Value='<%#Eval("Stock_END")%>'/></span>
                                                             </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_CP_Meiji_Price" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Requisition_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Requisition_Detail_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Old_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                     <%--<asp:TemplateField HeaderText="สต๊อคคงเหลือ" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Stock_end" runat="server" Style="color: blue" Text='<%#Eval("Stock_END")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>--%>

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_2">

                                            <asp:GridView ID="GridViewRequisition_2"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewRequisition_2_RowDataBound"
                                                OnDataBound="GridViewRequisition_2_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID")%>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                            <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name")%>' Style="color: blue" Visible="false"></asp:Label>
                                                            <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("SP_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดฝาก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Previous_Balance_Qty" runat="server" Style="color: blue" Text='<%# Eval("Previous_Balance_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggestion_Qty" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิกแล้ว" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Sub_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Sub_Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                         <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server"  TabIndex="<%# Container.DataItemIndex %>"
                                                                Style="text-align: right; color: blue" Width="84px" AutoCompleteType="disabled"> </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Total_Qty" runat="server" Style="text-align: right; color: blue" Width="84px" Enabled="false"></asp:TextBox>    
                                                            <span> <asp:HiddenField ID="hfEnd_Sock1" runat="server"  Value='<%#Eval("Stock_END")%>'/></span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_CP_Meiji_Price" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Requisition_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Requisition_Detail_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Old_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_3">

                                            <asp:GridView ID="GridViewRequisition_3"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewRequisition_3_RowDataBound"
                                                OnDataBound="GridViewRequisition_3_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID")%>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                            <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name")%>' Style="color: blue" Visible="false"></asp:Label>
                                                            <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("SP_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดฝาก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Previous_Balance_Qty" runat="server" Style="color: blue" Text='<%# Eval("Previous_Balance_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggestion_Qty" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิกแล้ว" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Sub_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Sub_Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server"  TabIndex="<%# Container.DataItemIndex %>"
                                                                Style="text-align: right; color: blue" Width="84px" AutoCompleteType="disabled"> </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Total_Qty" runat="server" Style="text-align: right; color: blue" Width="84px" Enabled="false"></asp:TextBox>
                                                        <span> <asp:HiddenField ID="hfEnd_Sock1" runat="server"  Value='<%#Eval("Stock_END")%>'/></span>
                                                             </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_CP_Meiji_Price" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Requisition_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Requisition_Detail_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Old_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_4">

                                            <asp:GridView ID="GridViewRequisition_4"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewRequisition_4_RowDataBound"
                                                OnDataBound="GridViewRequisition_4_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID")%>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                            <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name")%>' Style="color: blue" Visible="false"></asp:Label>
                                                            <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("SP_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดฝาก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Previous_Balance_Qty" runat="server" Style="color: blue" Text='<%# Eval("Previous_Balance_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggestion_Qty" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิกแล้ว" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Sub_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Sub_Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server"  TabIndex="<%# Container.DataItemIndex %>"
                                                                Style="text-align: right; color: blue" Width="84px" AutoCompleteType="disabled"> </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Total_Qty" runat="server" Style="text-align: right; color: blue" Width="84px" Enabled="false"></asp:TextBox>
                                                         <span> <asp:HiddenField ID="hfEnd_Sock1" runat="server"  Value='<%#Eval("Stock_END")%>'/></span>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_CP_Meiji_Price" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Requisition_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Requisition_Detail_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Old_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_5">

                                            <asp:GridView ID="GridViewRequisition_5"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewRequisition_5_RowDataBound"
                                                OnDataBound="GridViewRequisition_5_OnDataBound"
                                                CssClass="table table-striped table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID")%>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                            <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name")%>' Style="color: blue" Visible="false"></asp:Label>
                                                            <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PricePerUnit" runat="server" Style="color: blue" Text='<%# Eval("SP_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดฝาก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Previous_Balance_Qty" runat="server" Style="color: blue" Text='<%# Eval("Previous_Balance_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggestion_Qty" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิกแล้ว" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Sub_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Sub_Total_Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่เบิก" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                       <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server"  TabIndex="<%# Container.DataItemIndex %>"
                                                                Style="text-align: right; color: blue" Width="84px" AutoCompleteType="disabled"> </asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเบิกรวม" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_Total_Qty" runat="server" Style="text-align: right; color: blue" Width="84px" Enabled="false"></asp:TextBox>
                                                            <span> <asp:HiddenField ID="hfEnd_Sock1" runat="server"  Value='<%#Eval("Stock_END")%>'/></span>
                                                             </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_CP_Meiji_Price" runat="server" Style="color: blue" Text='<%# Eval("CP_Meiji_Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Point" runat="server" Style="color: blue" Text='<%# Eval("Point")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Requisition_Detail_ID" runat="server" Style="color: blue" Text='<%# Eval("Requisition_Detail_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Old_Total_Qty" runat="server" Style="color: blue" Text='<%# Eval("Total_Qty")%>'></asp:Label>
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
                                <asp:HiddenField ID="hdfStatus" runat="server" />
                                <asp:HiddenField ID="btnSaveMode" runat="server" />
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;" />
                                <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>                            
                        </div>
                    </div>
                               <a tabindex="0"
                               id="link_pop"
                               class="btn btn-primary" 
                               role="button" 
                               data-html="true" 
                               data-toggle="popover"                                
                               title="<div class=text-label-bold><b>ข้อมูลการเบิกสินค้า</b></div>" 
                               style="margin-left:2px"
                               data-content= "
                                 <div class=text-label-bold>จำนวนรวม: <br>
                                <input id=lblQtyTotal style=color:blue;border:0px;width:120px></div>
                                <div class=text-label-bold>ราคารวม:<br>
                                <input id=lblPriceTotal style=color:blue;border:0px;width:120px><br />
                                </div>">
                                <span class="glyphicon glyphicon-info-sign"></span>
                            </a>

                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

