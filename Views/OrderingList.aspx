<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master"
    AutoEventWireup="true" UICulture="th" Culture="th-TH"
    CodeFile="OrderingList.aspx.cs" Inherits="Views_OrderingList" %>

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

        h1 {
            color: Silver;
        }

        #btn-pop {
            z-index: 1;
            position: fixed;
            top: 47%;
            left: 0;
        }

        #link_pop {
            z-index: 1;
            position: fixed;
            top: 47%;
            left: 0;
        }

        .popover {
            position: fixed !important; /* I use !important because in this snippet bootstrap's styles overwrite mines but in general it is not nedded*/
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
            // Enables popover
            //$("[data-toggle=popover]").popover();
            $("#link_pop").popover('show');

            ///document.getElementById('lblTotal').value = document.getElementById('<%=txtTotal_Amount_before_vat_included.ClientID%>').value;
            ///document.getElementById('lblVat').value = document.getElementById('<%=txtVat_amount.ClientID%>').value;
            ///document.getElementById('lblSumTotal').value = document.getElementById('<%=txtTotal_amount_after_vat_included.ClientID%>').value;
            ///document.getElementById('lblDiff').value = document.getElementById('<%=TextboxDiff.ClientID%>').value;
        });


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

        // starting the script on page load
        $(document).ready(function () {
            imagePreview();
            ///gridviewScroll();
        });


        function pageLoad() {
            maintainSelectedTab();
            imagePreview();
        }

        function maintainSelectedTab() {
            var selectedTab = $("#<%=hfTab.ClientID%>");
            var tabId = selectedTab.val() != "" ? selectedTab.val() : "tab_default_1";
            $('#dvTab a[href="#' + tabId + '"]').tab('show');
            $("#dvTab a").click(function () {
                selectedTab.val($(this).attr("href").substring(1));
            });

            $("#link_pop").popover('show');
            ///document.getElementById('lblTotal').value = document.getElementById('<%=txtTotal_Amount_before_vat_included.ClientID%>').value;
            ///document.getElementById('lblVat').value = document.getElementById('<%=txtVat_amount.ClientID%>').value;
            ///document.getElementById('lblSumTotal').value = document.getElementById('<%=txtTotal_amount_after_vat_included.ClientID%>').value;
            ///document.getElementById('lblDiff').value = document.getElementById('<%=TextboxDiff.ClientID%>').value;
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
        var temp_var = 0;

        function ClearValue(Quantity, Agent_Price, Total, Vat, suggest) {

            global_Quantity = Quantity.value == '' ? '0' : Quantity.value;

            //alert(global_Quantity + ' in function ClearValue');
        }

        var global_Quantity = 0;

        function UpdateField(Quantity, Agent_Price, Total, Vat, suggest) {

            var tmp_Quantity = Quantity.value == '' ? '0' : Quantity.value;

            //alert(Quantity.innerHTML + ',' + Agent_Price.innerHTML + ',' + Total.innerHTML + ',' + Vat.innerHTML + ',' + suggest.innerHTML)
            var boolCon = true;
            if (parseInt(suggest.innerHTML) > 0) {
                //alert(tmp_Quantity + ',' + suggest.innerHTML);
                if (parseInt(tmp_Quantity) > (parseInt(suggest.innerHTML) * 2)) {

                    if (confirm('จำนวนที่สั่งมากกว่าจำนวนแนะนำ2เท่า คุณยืนยันจำนวนที่สั่งนี้หรือไม่?')) {
                        boolCon = true;
                    } else {
                        boolCon = false;
                        Quantity.value = '';
                        Quantity.focus();
                    }
                }
            }


            if (boolCon) {



                var calMonthTarget = document.getElementById('<%=txtMonthTarget.ClientID%>').value.split(',').join('');
                var arr = calMonthTarget.split('/');
                var expectedTarget = arr[1];
                var actualTarget = arr[0];

                var calMonthTarget = document.getElementById('<%=txtMonthTarget.ClientID%>').value.split(',').join('');
                var arr = calMonthTarget.split('/');
                var expectedTarget = arr[1];
                var actualTarget = arr[0];

                var calQuarterTarget = document.getElementById('<%=txtQuarterTarget.ClientID%>').value.split(',').join('');
                var arr = calQuarterTarget.split('/');
                var expectedQuarterTarget = arr[1];
                var actualQuarterTarget = arr[0];

                var calYearTarget = document.getElementById('<%=txtYearTarget.ClientID%>').value.split(',').join('');
                var arr = calYearTarget.split('/');
                var expectedYearTarget = arr[1];
                var actualYearTarget = arr[0];


                var calTotal = 0;


<%--
                var calTotalAmount = parseFloat(document.getElementById('<%=txtTotal_Amount_before_vat_included.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtTotal_Amount_before_vat_included.ClientID%>').value.split(',').join(''));
                var calTotalVat = parseFloat(document.getElementById('<%=txtVat_amount.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtVat_amount.ClientID%>').value.split(',').join(''));
                var calGrandTotalAmount = parseFloat(document.getElementById('<%=txtTotal_amount_after_vat_included.ClientID%>').value == '' ? '0' : document.getElementById('<%=txtTotal_amount_after_vat_included.ClientID%>').value.split(',').join(''));
--%>
                var calTotalAmount = 0;
                var calTotalVat = 0;
                var calGrandTotalAmount = 0;


                //if (parseInt(tmp_Quantity) > parseInt(global_Quantity)) {
                //    calTotalAmount = parseFloat(calTotalAmount) + (parseFloat(parseInt(tmp_Quantity) - parseInt(global_Quantity)) * parseFloat(Agent_Price.value));

                //    actualTarget = parseFloat(actualTarget) + parseFloat((parseInt(tmp_Quantity) - parseInt(global_Quantity)) * parseFloat(Agent_Price.value));
                //    actualQuarterTarget = parseFloat(actualQuarterTarget) + parseFloat((parseInt(tmp_Quantity) - parseInt(global_Quantity)) * parseFloat(Agent_Price.value));
                //    actualYearTarget = parseFloat(actualYearTarget) + parseFloat((parseInt(tmp_Quantity) - parseInt(global_Quantity)) * parseFloat(Agent_Price.value));

                //    calTotal = parseFloat(parseInt(tmp_Quantity) - parseInt(global_Quantity)) * parseFloat(Agent_Price.value);
                //    calTotal = calTotal.toFixed(2);


                //    var calVat = parseFloat(Vat.value) * parseFloat(calTotal) / 100;

                //    calVat = calVat.toFixed(2);


                //    calTotalVat = parseFloat(calTotalVat) + parseFloat(calVat);

                //    calGrandTotalAmount = calGrandTotalAmount + (parseFloat(calVat) + parseFloat(calTotal));
                //} else if (parseInt(tmp_Quantity) <= parseInt(global_Quantity)) {

                //    calTotalAmount = parseFloat(calTotalAmount) - (parseFloat(parseInt(global_Quantity) - parseInt(tmp_Quantity)) * parseFloat(Agent_Price.value));
                //    actualTarget = parseFloat(actualTarget) - (parseFloat(parseInt(global_Quantity) - parseInt(tmp_Quantity)) * parseFloat(Agent_Price.value));
                //    actualQuarterTarget = parseFloat(actualQuarterTarget) - parseFloat((parseInt(global_Quantity) - parseInt(tmp_Quantity)) * parseFloat(Agent_Price.value));
                //    actualYearTarget = parseFloat(actualYearTarget) - parseFloat((parseInt(global_Quantity) - parseInt(tmp_Quantity)) * parseFloat(Agent_Price.value));

                //    calTotal = parseFloat(parseInt(global_Quantity) - parseInt(tmp_Quantity)) * parseFloat(Agent_Price.value);

                //    calTotal = calTotal.toFixed(2);

                //    var calVat = parseFloat(Vat.value) * parseFloat(calTotal) / 100;

                //    calVat = calVat.toFixed(2);


                //    var old_vat = parseFloat(Vat.value) * parseFloat(parseFloat(parseInt(global_Quantity)) * parseFloat(Agent_Price.value)) / 100;
                //    old_vat = old_vat.toFixed(2);



                //    calTotalVat = parseFloat(calTotalVat) - (old_vat)


                //        + parseFloat(calVat) - parseFloat(calTotalVat);



                //    calGrandTotalAmount = calGrandTotalAmount - (parseFloat(calVat) + parseFloat(calTotal));
                //}

                var Agent_Price = 0;
                var Vat = 0;
                var Quantity = 0;
                var tmp_calTotalAmount = 0;
                var tmp_vat = 0;
                var Grid_Table = "";

                if (document.getElementById('<%= GridViewOrdering_Tab1.ClientID %>') != null) {


                    Grid_Table = document.getElementById('<%= GridViewOrdering_Tab1.ClientID %>');

                    //alert(Grid_Table.rows.length+'tab1');
                    for (var row = 1; row < Grid_Table.rows.length; row++) {

                        if (Grid_Table.rows[row].cells.length == 1) {

                        } else {

                            //alert(Grid_Table.rows[row].cells[7].innerHTML);
                            if (Grid_Table.rows[row].cells[7] != undefined) {
                                //alert('gg');
                                if (Grid_Table.rows[row].cells[7].childNodes[1].value != '') {

                                    Agent_Price = Grid_Table.rows[row].cells[4].childNodes[1].value;
                                    Vat = Grid_Table.rows[row].cells[5].childNodes[1].value;
                                    Quantity = Grid_Table.rows[row].cells[7].childNodes[1].value;

                                    tmp_calTotalAmount = formatCurrency(parseFloat(Agent_Price) * parseFloat(Quantity)).replace(',', '');


                                    calTotalAmount = parseFloat(calTotalAmount) + parseFloat(tmp_calTotalAmount);


                                    tmp_vat = formatCurrency(parseFloat((tmp_calTotalAmount * Vat) / 100)).replace(',', '');

                                    calTotalVat = parseFloat(calTotalVat) + parseFloat(tmp_vat);


                                    calGrandTotalAmount = parseFloat(calTotalAmount) + parseFloat(calTotalVat);


                                    Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = tmp_calTotalAmount;

                                }
                                else {

                                    Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = '';

                                }
                            }
                        }
                    }
                }
                if (document.getElementById('<%= GridViewOrdering_Tab2.ClientID %>') != null) {
                    Grid_Table = document.getElementById('<%= GridViewOrdering_Tab2.ClientID %>');
                    //alert(Grid_Table.rows.length + 'tab2');
                    for (var row = 1; row < Grid_Table.rows.length; row++) {

                        if (Grid_Table.rows[row].cells.length == 1) {

                        } else {

                            if (Grid_Table.rows[row].cells[7] != undefined) {
                                if (Grid_Table.rows[row].cells[7].childNodes[1].value != '') {

                                    Agent_Price = Grid_Table.rows[row].cells[4].childNodes[1].value;
                                    Vat = Grid_Table.rows[row].cells[5].childNodes[1].value;
                                    Quantity = Grid_Table.rows[row].cells[7].childNodes[1].value;

                                    tmp_calTotalAmount = formatCurrency(parseFloat(Agent_Price) * parseFloat(Quantity)).replace(',', '');

                                    calTotalAmount = parseFloat(calTotalAmount) + parseFloat(tmp_calTotalAmount);

                                    tmp_vat = formatCurrency(parseFloat((tmp_calTotalAmount * Vat) / 100)).replace(',', '');

                                    calTotalVat = parseFloat(calTotalVat) + parseFloat(tmp_vat);

                                    calGrandTotalAmount = parseFloat(calTotalAmount) + parseFloat(calTotalVat);
                                    Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = tmp_calTotalAmount;
                                } else {
                                    Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = '';

                                }
                            }
                        }
                    }
                }



             <%--   var rowcount = document.getElementById('<%= GridViewOrdering_Tab3.ClientID %>').row.length;
                alert(rowcount);--%>
                if (document.getElementById('<%= GridViewOrdering_Tab3.ClientID %>') != null) {


                    Grid_Table = document.getElementById('<%= GridViewOrdering_Tab3.ClientID %>');

                    //  alert(Grid_Table.rows.length + 'tab3');

                    for (var row = 1; row < Grid_Table.rows.length; row++) {

                        if (Grid_Table.rows[row].cells.length == 1) {

                        } else {

                            if (Grid_Table.rows[row].cells[7] != undefined) {
                                if (Grid_Table.rows[row].cells[7].childNodes[1].value != '') {
                                    // Quantity.value == '' ? '0' : Quantity.value
                                    Agent_Price = Grid_Table.rows[row].cells[4].childNodes[1].value;
                                    Vat = Grid_Table.rows[row].cells[5].childNodes[1].value;
                                    Quantity = Grid_Table.rows[row].cells[7].childNodes[1].value;

                                    tmp_calTotalAmount = formatCurrency(parseFloat(Agent_Price) * parseFloat(Quantity)).replace(',', '');

                                    calTotalAmount = parseFloat(calTotalAmount) + parseFloat(tmp_calTotalAmount);

                                    tmp_vat = formatCurrency(parseFloat((tmp_calTotalAmount * Vat) / 100)).replace(',', '');

                                    calTotalVat = parseFloat(calTotalVat) + parseFloat(tmp_vat);

                                    calGrandTotalAmount = parseFloat(calTotalAmount) + parseFloat(calTotalVat);
                                    Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = tmp_calTotalAmount;
                                } else {
                                    Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = '';

                                }
                            }
                        }
                    }
                }

                if (document.getElementById('<%= GridViewOrdering_Tab4.ClientID %>') != null) {
                    Grid_Table = document.getElementById('<%= GridViewOrdering_Tab4.ClientID %>');
                    //alert(Grid_Table.rows.length + 'tab4');
                    if (Grid_Table.rows.length > 0) {
                        for (var row = 1; row < Grid_Table.rows.length; row++) {

                            if (Grid_Table.rows[row].cells.length == 1) {

                            } else {

                                if (Grid_Table.rows[row].cells[7] != undefined) {
                                    if (Grid_Table.rows[row].cells[7].childNodes[1].value != '') {

                                        Agent_Price = Grid_Table.rows[row].cells[4].childNodes[1].value;
                                        Vat = Grid_Table.rows[row].cells[5].childNodes[1].value;
                                        Quantity = Grid_Table.rows[row].cells[7].childNodes[1].value;

                                        tmp_calTotalAmount = formatCurrency(parseFloat(Agent_Price) * parseFloat(Quantity)).replace(',', '');

                                        calTotalAmount = parseFloat(calTotalAmount) + parseFloat(tmp_calTotalAmount);

                                        tmp_vat = formatCurrency(parseFloat((tmp_calTotalAmount * Vat) / 100)).replace(',', '');

                                        calTotalVat = parseFloat(calTotalVat) + parseFloat(tmp_vat);

                                        calGrandTotalAmount = parseFloat(calTotalAmount) + parseFloat(calTotalVat);


                                        Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = tmp_calTotalAmount;
                                    } else {
                                        Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = '';

                                    }
                                }
                            }
                        }
                    }
                }


                if (document.getElementById('<%= GridViewOrdering_Tab5.ClientID %>') != null) {


                    Grid_Table = document.getElementById('<%= GridViewOrdering_Tab5.ClientID %>');

                    //alert(Grid_Table.rows.length+'tab1');
                    for (var row = 1; row < Grid_Table.rows.length; row++) {

                        if (Grid_Table.rows[row].cells.length == 1) {

                        } else {

                            //alert(Grid_Table.rows[row].cells[7].innerHTML);
                            if (Grid_Table.rows[row].cells[7] != undefined) {
                                //alert('gg');
                                if (Grid_Table.rows[row].cells[7].childNodes[1].value != '') {

                                    Agent_Price = Grid_Table.rows[row].cells[4].childNodes[1].value;
                                    Vat = Grid_Table.rows[row].cells[5].childNodes[1].value;
                                    Quantity = Grid_Table.rows[row].cells[7].childNodes[1].value;

                                    tmp_calTotalAmount = formatCurrency(parseFloat(Agent_Price) * parseFloat(Quantity)).replace(',', '');


                                    calTotalAmount = parseFloat(calTotalAmount) + parseFloat(tmp_calTotalAmount);


                                    tmp_vat = formatCurrency(parseFloat((tmp_calTotalAmount * Vat) / 100)).replace(',', '');

                                    calTotalVat = parseFloat(calTotalVat) + parseFloat(tmp_vat);


                                    calGrandTotalAmount = parseFloat(calTotalAmount) + parseFloat(calTotalVat);


                                    Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = tmp_calTotalAmount;

                                }
                                else {

                                    Grid_Table.rows[row].cells[8].childNodes[1].innerHTML = '';

                                }
                            }
                        }
                    }
                }



                //var calTotal_ = parseFloat(parseInt(tmp_Quantity)) * parseFloat(Agent_Price.value);
                //Total.innerHTML = (calTotal_ > 0 ? formatCurrency(calTotal_) : '');

                if (calTotalVat < 0) {
                    calTotalVat = 0;
                }

                if (calGrandTotalAmount < 0) {
                    calGrandTotalAmount = 0;
                }



                // alert(rmatCurrency(calTotalAmount), formatCurrency(calTotalVat), formatCurrency(calGrandTotalAmount));

                document.getElementById('<%=txtTotal_Amount_before_vat_included.ClientID%>').value = formatCurrency(calTotalAmount);
                document.getElementById('<%=txtVat_amount.ClientID%>').value = formatCurrency(calTotalVat);
                document.getElementById('<%=txtTotal_amount_after_vat_included.ClientID%>').value = formatCurrency(calGrandTotalAmount);

                if (expectedTarget > 0) {
                    if ((expectedTarget - (actualTarget + calTotalAmount)) > 0) {

                        var calMonthTargetIn = document.getElementById('<%=txtMonthTarget.ClientID%>').value.split(',').join('');
                        //alert(calMonthTargetIn);
                        var arrIn = calMonthTargetIn.split('/');
                        var expectedTargetIn = arrIn[1];
                        var actualTargetIn = arrIn[0];
                        var mode = document.getElementById('<%=hfSaveMode.ClientID%>').value
                        //alert(document.getElementById('<%=hfSaveMode.ClientID%>').value);
                        if (mode == "NEW") {


                            if ((parseFloat(document.getElementById('<%=hdfTargetDiff.ClientID%>').value.replace(',', '')) - calTotalAmount) > 0) {
                                document.getElementById('<%=TextboxDiff.ClientID%>').value = formatCurrency(parseFloat(document.getElementById('<%=hdfTargetDiff.ClientID%>').value.replace(',', '')) - calTotalAmount);
                                //document.getElementById("lblDiff").style.color = red;
                            } else {
                                document.getElementById('<%=TextboxDiff.ClientID%>').value = "0.00";
                                //document.getElementById("lblDiff").style.color = green;
                            }


                        } else if (mode == "EDIT") {
                            document.getElementById('<%=TextboxDiff.ClientID%>').value = formatCurrency(expectedTarget - (parseFloat(actualTargetIn) + calTotalAmount));

                        }

                        //alert((expectedTarget - (actualTarget)));
                        // document.getElementById('<%=TextboxDiff.ClientID%>').value = formatCurrency(parseFloat(document.getElementById('<%=hdfTargetDiff.ClientID%>').value.replace(',', '')));
                    } else {

                        document.getElementById('<%=TextboxDiff.ClientID%>').value = "0.00";
                        //document.getElementById("lblDiff").style.color = green;
                    }
                } else {

                    document.getElementById('<%=TextboxDiff.ClientID%>').value = "0.00";
                    //document.getElementById("lblDiff").style.color = green;
                }

                if ((parseFloat(document.getElementById('<%=hdfTargetDiff.ClientID%>').value.replace(',', '')) - calTotalAmount) > 0) {
                    document.getElementById('<%=TextboxDiff.ClientID%>').value = formatCurrency(parseFloat(document.getElementById('<%=hdfTargetDiff.ClientID%>').value.replace(',', '')) - calTotalAmount);
                }
                else {
                    document.getElementById('<%=TextboxDiff.ClientID%>').value = "0.00";
                }

                document.getElementById('lblTotal').value = document.getElementById('<%=txtTotal_Amount_before_vat_included.ClientID%>').value;
                document.getElementById('lblVat').value = document.getElementById('<%=txtVat_amount.ClientID%>').value;
                document.getElementById('lblSumTotal').value = document.getElementById('<%=txtTotal_amount_after_vat_included.ClientID%>').value;
                document.getElementById('lblDiff').value = document.getElementById('<%=TextboxDiff.ClientID%>').value;
            }

            return true;
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

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <Triggers>
            <asp:PostBackTrigger ControlID="ButtonAddNew" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField ID="hfTab" runat="server" />
            <asp:HiddenField ID="hfSaveMode" runat="server" />
            <asp:Panel ID="pnlGrid" Visible="True" runat="server">


                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 " >
                            <span class="title" style="font-size: 18px">รายการใบสั่งซื้อ</span>&nbsp;&nbsp;
                              <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                       
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เลขที่ใบสั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtOderingNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2"></div>
                                </div>
                                <div class="form-group">
                                    <%--<div class="form-inline">--%>
                                    <label class="col-md-2 control-label">วันที่สั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtOrderingStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="imgBtxtOrderingStartDate" ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldEtxtOrderingStartDate" runat="server"
                                            TargetControlID="txtOrderingStartDate" PopupButtonID="cldEtxtOrderingStartDate" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtOrderingEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="imgBtxtOrderingEndDate" ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldEtxtOrderingEndDate" runat="server"
                                            TargetControlID="txtOrderingEndDate" PopupButtonID="cldEtxtOrderingEndDate" />
                                    </div>
                                    <%--</div>--%>
                                </div>
                                <div class="form-group">
                                    <%--<div class="form-inline">--%>
                                    <label class="col-md-2 control-label">วันที่รับสินค้า:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRecievingStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="imgBtxtRecievingStartDate" ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldEtxtRecievingStartDate" runat="server"
                                            TargetControlID="txtRecievingStartDate" PopupButtonID="cldEtxtRecievingStartDate" />
                                    </div>
                                    <label class="col-md-2 control-label">ถึง:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRecievingEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="imgBtxtRecievingEndDate" ImageUrl="/images/Calendar.png" width="26" Height="22" AlternateText="" CausesValidation="False" />--%>
                                        <ajaxToolkit:CalendarExtender ID="cldEtxtRecievingEndDate" runat="server"
                                            TargetControlID="txtRecievingEndDate" PopupButtonID="cldEtxtRecievingEndDate" />
                                    </div>
                                    <%--</div>--%>
                                </div>
                                <div class="form-group">
                                   
                                    <label class="col-md-2 control-label">สถานะใบสั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="DropDownListOrderingStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="==ระบุ=="></asp:ListItem>
                                            <asp:ListItem Text="รอ ซีพี-เมจิ รับข้อมูล"></asp:ListItem>
                                            <asp:ListItem Text="ซีพี-เมจิ รับข้อมูลแล้ว"></asp:ListItem>
                                            <asp:ListItem Text="รับสินค้าแล้ว"></asp:ListItem>
                                            <asp:ListItem Text="ยกเลิกโดย ซีพี-เมจิ"></asp:ListItem>
                                            <asp:ListItem Text="ยกเลิกโดยเอเย่นต์"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchSubmit" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearchSubmit_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>

                            <div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="ButtonAddNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="btnAddNew_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                            <div class="col-md-12">
                               <%-- <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px;padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่มีรายการสั่งสินค้า</center>
                                    </div>
                                </asp:Panel>--%>

 <%--<div class="row">--%>
                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                        <%--<div class="panel panel-default">--%>
                               <div class="container-fluid panel-container" style="overflow: auto">

                                <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px;padding-bottom: 20px; background: #fbfafa">
                                        <center>ไม่มีรายการสั่งสินค้า</center>
                                    </div>
                                </asp:Panel>


                                <asp:GridView ID="GridViewOrdering"
                                    runat="server" ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    OnRowCommand="GridViewOrdering_RowCommand"
                                    AllowPaging="true" PageSize="10" OnDataBound="GridViewOrdering_DataBound" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ"
                                                        CommandArgument='<%# Eval("PO_Number") %>'
                                                        CommandName="_Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBPONumber" runat="server" CssClass="btn btn-link"
                                                    Text='<%# Eval("PO_Number") %>'
                                                    CommandArgument='<%# Eval("PO_Number") %>'
                                                    CommandName="View" Style="font-weight: bold; text-decoration: underline" OnClientClick="myApp.showPleaseWait(); return true;"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_OrderingDate" runat="server" Text='<%# Eval("Date_of_create_order_or_PO_Date","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่รับสินค้า">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ReceivingDate" runat="server" Text='<%# Eval("Date_of_delivery_goods","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ผู้สั่ง">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_OrderBy" runat="server" Style="color: blue" Text='<%# Eval("OrderBy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ยอดสั่งซื้อ" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TotalAmount" runat="server" Text='<%# Eval("Total_amount_after_vat_included","{0:N2}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Order_Status" runat="server" Text='<%# Eval("Order_Status") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkB_CV_CODE" runat="server" OnClientClick="myApp.showPleaseWait(); return true;" CssClass="btn btn-mini btn-primary" Text="พิมพ์ใบสั่งซื้อ"
                                                    CommandName="Print" CommandArgument='<%# Eval("PO_Number") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkB_Create_Billing" runat="server" CssClass="btn btn-mini btn-primary" Text="Create billing" CommandName="Create_Billing" CommandArgument='<%# Eval("PO_Number") %>'></asp:LinkButton>
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

                                       <%-- </div>--%>
                                     </div>
                          </div>
       <%--</div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
               <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                        <div class="panel panel-default">

                            <asp:GridView ID="GridViewOrdering"
                                    runat="server" ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="False"
                                    ShowFooter="false"
                                    CellPadding="0" ForeColor="#333333"
                                    CssClass="table table-striped table-bordered table-condensed"
                                    OnRowCommand="GridViewOrdering_RowCommand"
                                    AllowPaging="true" PageSize="10" OnDataBound="GridViewOrdering_DataBound" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                    <asp:LinkButton ID="lnkB" runat="Server" CssClass="btn btn-mini btn-danger" Width="48px" Text="ลบ"
                                                        CommandArgument='<%# Eval("PO_Number") %>'
                                                        CommandName="_Delete"></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBPONumber" runat="server" CssClass="btn btn-link"
                                                    Text='<%# Eval("PO_Number") %>'
                                                    CommandArgument='<%# Eval("PO_Number") %>'
                                                    CommandName="View" Style="font-weight: bold; text-decoration: underline" OnClientClick="myApp.showPleaseWait(); return true;"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_OrderingDate" runat="server" Text='<%# Eval("Date_of_create_order_or_PO_Date","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่รับสินค้า">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ReceivingDate" runat="server" Text='<%# Eval("Date_of_delivery_goods","{0:d/MM/yyyy}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ผู้สั่ง">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_OrderBy" runat="server" Style="color: blue" Text='<%# Eval("OrderBy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ยอดสั่งซื้อ" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TotalAmount" runat="server" Text='<%# Eval("Total_amount_after_vat_included","{0:N2}") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะใบสั่งซื้อ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Order_Status" runat="server" Text='<%# Eval("Order_Status") %>' Style="color: blue"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkB_CV_CODE" runat="server" OnClientClick="myApp.showPleaseWait(); return true;" CssClass="btn btn-mini btn-primary" Text="พิมพ์ใบสั่งซื้อ"
                                                    CommandName="Print" CommandArgument='<%# Eval("PO_Number") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ShowHeader="False" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkB_Create_Billing" runat="server" CssClass="btn btn-mini btn-primary" Text="Create billing" CommandName="Create_Billing" CommandArgument='<%# Eval("PO_Number") %>'></asp:LinkButton>
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
            </ContentTemplate>
        </asp:UpdatePanel>--%>
















            <asp:Panel ID="pnlForm" Visible="False" runat="server">


                
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">สั่งซื้อสินค้า</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">CV CODE:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtCV_Code_from_SAP" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">เลขที่ใบสั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPO_Number" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อเอเยนต์:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่สั่งซื้อ:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtDate_of_create_order_or_PO_Date" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ราคารวมสั่งซื้อทั้งหมด:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtTotal_Amount_before_vat_included" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">วันที่ CP รับข้อมูล:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtDate_of_CP_receive_transaction" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">VAT:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtVat_amount" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <label class="col-md-2 control-label">วันที่รับสินค้า:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtDate_of_delivery_goods" runat="server" Style="border-left: 4px solid #ed1c24;" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDate_of_delivery_goods_TextChanged" OnClick="btnSearchCancel_Click" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderStart_Effective_Date" runat="server" TargetControlID="txtDate_of_delivery_goods" PopupButtonID="txtDate_of_delivery_goods" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ยอดรวมทั้งสิ้น:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtTotal_amount_after_vat_included" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">สถานะ:</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlOrder_Status" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="รอ ซีพี-เมจิ รับข้อมูล"></asp:ListItem>
                                            <asp:ListItem Text="ซีพี-เมจิ รับข้อมูลแล้ว"></asp:ListItem>
                                            <asp:ListItem Text="รับสินค้าแล้ว"></asp:ListItem>
                                            <asp:ListItem Text="ยกเลิกโดย ซีพี-เมจิ"></asp:ListItem>
                                            <asp:ListItem Text="ยกเลิกโดยเอเย่นต์"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">เป้ายอดขายรายเดือน:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtMonthTarget" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label">เป้ายอดขายรายไตรมาส:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtQuarterTarget" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <%-- GV Tap --%>
                               
                                <div class="form-group">
                                    <label id="lblTotalTarget" runat="server" class="col-md-2 control-label" style="color: red">ยอดห่างจากเป้า:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextboxDiff" runat="server" CssClass="form-control" Font-Bold="true" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="hdfTargetDiff" runat="server" />
                                    </div>
                                    <label class="col-md-2 control-label">เป้ายอดขายรายปี:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtYearTarget" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <!-- Tab -->
 
                             <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px">
                     
                                 <div class="tabbable-panel tabbable-line">
                                         <ul class="nav nav-tabs">
                                        <li class="active" id="li_01" style="width: 180px">
                                            
                                            <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab1click()"><span><img src="../Images/tab1.png" id="imgTab1" style="position:absolute; top: -3px; z-index: 999; left: 0px; height: 64px; width: 45px; " /></span>  นมพาสเจอร์ไรส์</a>

                                        </li>
                                        <li id="li_02" style="width: 130px">
                                            <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -29px;    z-index: 999; left: 4px; height: 85px; width: 40px;  display: none;" />
                                            <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab2click()">นมเปรี้ยว </a>

                                        </li>
                                        <li id="li_03" style="width: 170px">
                                            <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -40px;  z-index: 999; left: 2px; height: 94px; width: 56px;  display: none;" />
                                            <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab3click()">&nbsp;&nbsp;&nbsp;&nbsp;โยเกิร์ตเมจิ </a>

                                        </li>
                                        <li id="li_04" style="width: 170px">
                                            <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -7px;  z-index: 999; left: 0px; height: 64px; width: 36px;  display: none;"/>
                                            <a href="#tab_default_4" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center" onclick="tab4click()">&nbsp;&nbsp;&nbsp;&nbsp;นมเปรี้ยวไพเก้น </a>

                                        </li>
                                        <li id="li_05" style="display: none;">
                                            <a href="#tab_default_5" id="tab5" data-toggle="tab" style="padding-left: 30px; top: 0px; left: 0px; height: 36px;" class="text-center" onclick="tab5click()">อื่นๆ </a>

                                        </li>
                                    </ul>
                                   </div>


                           <div class="container-fluid panel-container" style="overflow: auto">
                           <%-- <div class="tabbable-panel" style="margin-top: 20px;""> <!--20px-->--%>



                                <div class="tabbable-line" id="dvTab">                 
                                    <div class="tab-content">                                
                                        <div class="tab-pane active" id="tab_default_1">                      
                                            <asp:GridView ID="GridViewOrdering_Tab1"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed"
                                                OnDataBound="GridViewOrdering_1_OnDataBound" OnRowDataBound="GridViewOrdering_Tab1_RowDataBound">
                                                 <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item" runat="server" Text='<%# Eval("index") %>' Style="color: blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                            <%--<a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>--%>
                                                            <%--<asp:Label runat="server" ID="lbl_Product_ID" Text='<%# Eval("Product_ID") %>'  />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                             <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue" visible ="false"></asp:Label>
                                                          <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdfPrice" runat="server" Value='<%# Eval("Agent_Price") %>' />
                                                            <asp:Label ID="lbl_Price" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนสต๊อก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="hdfVat" runat="server" Value='<%# Eval("Vat") %>' />
                                                            <asp:Label ID="lbl_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggest_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่สั่ง" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" Width="84px"
                                                                Text='<%# Eval("Quantity") %>' Style="text-align: right" MaxLength="5"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                            <asp:HiddenField ID="hdfOldQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="มูลค่าสินค้า" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Amount" runat="server" Width="84px" Style="color: blue; text-align: right" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfAmount" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Point" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="tab-pane" id="tab_default_2">
                                            <asp:GridView ID="GridViewOrdering_Tab2"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewOrdering_Tab2_RowDataBound"
                                                OnDataBound="GridViewOrdering_2_OnDataBound"
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
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                            <%--<a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>--%>
                                                            <%--<asp:Label runat="server" ID="lbl_Product_ID" Text='<%# Eval("Product_ID") %>'  />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                             <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue" visible ="false"></asp:Label>
                                                          <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdfPrice" runat="server" Value='<%# Eval("Agent_Price") %>' />
                                                            <asp:Label ID="lbl_Price" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนสต๊อก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="hdfVat" runat="server" Value='<%# Eval("Vat") %>' />
                                                            <asp:Label ID="lbl_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggest_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่สั่ง" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" Width="84px"
                                                                Text='<%# Eval("Quantity") %>' Style="text-align: right" MaxLength="5"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                            <asp:HiddenField ID="hdfOldQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="มูลค่าสินค้า" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Amount" runat="server" Width="84px" Style="color: blue; text-align: right" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfAmount" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Point" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_3">
                                            <asp:GridView ID="GridViewOrdering_Tab3"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewOrdering_Tab3_RowDataBound"
                                                OnDataBound="GridViewOrdering_3_OnDataBound"
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
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                            <%--<a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>--%>
                                                            <%--<asp:Label runat="server" ID="lbl_Product_ID" Text='<%# Eval("Product_ID") %>'  />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                             <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue" visible ="false"></asp:Label>
                                                          <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdfPrice" runat="server" Value='<%# Eval("Agent_Price") %>' />
                                                            <asp:Label ID="lbl_Price" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนสต๊อก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="hdfVat" runat="server" Value='<%# Eval("Vat") %>' />
                                                            <asp:Label ID="lbl_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggest_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่สั่ง" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" Width="84px"
                                                                Text='<%# Eval("Quantity") %>' Style="text-align: right" MaxLength="5"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                            <asp:HiddenField ID="hdfOldQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="มูลค่าสินค้า" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Amount" runat="server" Width="84px" Style="color: blue; text-align: right" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfAmount" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Point" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_4">
                                            <asp:GridView ID="GridViewOrdering_Tab4"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewOrdering_Tab4_RowDataBound"
                                                OnDataBound="GridViewOrdering_4_OnDataBound"
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
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                            <%--<a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>--%>
                                                            <%--<asp:Label runat="server" ID="lbl_Product_ID" Text='<%# Eval("Product_ID") %>'  />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                             <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue" visible ="false"></asp:Label>
                                                          <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdfPrice" runat="server" Value='<%# Eval("Agent_Price") %>' />
                                                            <asp:Label ID="lbl_Price" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนสต๊อก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="hdfVat" runat="server" Value='<%# Eval("Vat") %>' />
                                                            <asp:Label ID="lbl_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggest_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่สั่ง" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" Width="84px"
                                                                Text='<%# Eval("Quantity") %>' Style="text-align: right" MaxLength="5"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                            <asp:HiddenField ID="hdfOldQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="มูลค่าสินค้า" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Amount" runat="server" Width="84px" Style="color: blue; text-align: right" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfAmount" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Point" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="tab_default_5">
                                            <asp:GridView ID="GridViewOrdering_Tab5"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                DataKeyNames=""
                                                ShowFooter="false"
                                                CellPadding="0"
                                                ForeColor="#333333" OnRowDataBound="GridViewOrdering_Tab5_RowDataBound"
                                                OnDataBound="GridViewOrdering_5_OnDataBound"
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
                                                            <asp:Label id="lbl_Product_ID" runat="server" Text='<%# Eval("Product_ID") %>' style="color: blue"></asp:Label>
                                                            <%--<a href='<%# Eval("base64_Photo") %>' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_ID") %></a>--%>
                                                            <%--<asp:Label runat="server" ID="lbl_Product_ID" Text='<%# Eval("Product_ID") %>'  />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="350px">
                                                        <ItemTemplate>
                                                             <a href='#' class="preview" style="color: blue" rel='<%# Eval("base64_Photo") %>'><%# Eval("Product_Name") %></a>
                                                            <asp:Label ID="lbl_Product_Name" runat="server" Text='<%# Eval("Product_Name") %>' Style="color: blue" visible ="false"></asp:Label>
                                                          <img runat ="server" visible = '<%# Eval("IconVisible") %>' 
                                                                width="16" height ="16" src ="data:image/svg+xml;utf8;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0UwNEY1RjsiIGQ9Ik01MTIsMjU2LjhsLTY3LjItNTQuMjI0bDQzLjItNzQuOTZsLTkxLjItNy45NjhsLTgtODcuNzI4TDMxMiw2OC42MDhMMjU5LjIsMGwtNDkuNiw3MC4xNzYgIGwtODAtNDEuNDcybC00LjgsOTAuOTI4bC04OS42LDQuNzg0bDMzLjYsODYuMTI4TDAsMjUyLjAxNmw3MC40LDUyLjY0bC0zNS4yLDc0Ljk2bDg4LDkuNTY4bDQuOCw4OS4zMjhsODEuNi0zOC4yODhMMjY0LDUxMiAgbDQ4LTcxLjc3Nmw3NS4yLDM2LjY4OGw2LjQtODkuMzI4bDg4LTEuNmwtMzItNzMuMzc2TDUxMiwyNTYuOHoiLz4KPGc+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTE1Mi45OTIsMzI0LjUxMmwtMzEuNTg0LTkwLjI0bDIzLjgyNC04LjMzNmwzMC4zMiwyNi41MTJjOC42ODgsNy42MzIsMTcuOTY4LDE3LjAwOCwyNS41NjgsMjUuNzc2ICAgbDAuNC0wLjE0NGMtNS41Mi0xMS40NTYtMTAuMTkyLTIzLjUwNC0xNC45MTItMzcuMDA4bC05LjIzMi0yNi4zODRsMTguNzM2LTYuNTQ0bDMxLjU4NCw5MC4yNGwtMjEuNDI0LDcuNTA0bC0zMS40NTYtMjguMDY0ICAgYy04LjczNi03Ljc2LTE4LjcwNC0xNy4zNi0yNi44MzItMjYuMzY4bC0wLjMzNiwwLjI3MmM0LjcwNCwxMS43MjgsOS40MjQsMjQuMzUyLDE0LjU2LDM5LjA3Mmw5LjUyLDI3LjE4NEwxNTIuOTkyLDMyNC41MTJ6Ii8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTI1MC44MTYsMjYwLjgzMmMzLjYzMiw4LjIwOCwxMy4zMTIsOS4zMTIsMjIuODQ4LDUuOTg0YzYuOTYtMi40MzIsMTIuMjU2LTUuMzYsMTcuMTItOS4wMDggICBsNy41MDQsMTIuODQ4Yy01Ljc0NCw1LjAyNC0xMy40NTYsOS4yMzItMjIuMjg4LDEyLjMzNmMtMjIuMjI0LDcuNzc2LTM5LjQ0LTAuNjI0LTQ2LjYyNC0yMS4xMDQgICBjLTUuNzkyLTE2LjU5Mi0xLjkwNC0zOC41NDQsMjAuODQ4LTQ2LjUxMmMyMS4xNTItNy40MDgsMzQuOTYsNi4yNTYsNDAuNjI0LDIyLjQ0OGMxLjIxNiwzLjQ3MiwxLjkwNCw2LjcwNCwyLjE2LDguMjcyICAgTDI1MC44MTYsMjYwLjgzMnogTTI2OS45MDQsMjM4LjUyOGMtMS43MjgtNC45Ni02Ljc4NC0xMi40OTYtMTYuMTYtOS4yMTZjLTguNTYsMy4wMDgtOS4zMjgsMTEuOTY4LTcuOTM2LDE3LjY2NEwyNjkuOTA0LDIzOC41Mjh6ICAgIi8+Cgk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMwNS4zNzYsMTk3LjcxMmwxNC41NzYsMjQuOTQ0YzMuNzI4LDYuMzUyLDcuNjE2LDEzLjEyLDExLjI5NiwyMC4yNTZsMC4yNzItMC4wOTYgICBjLTEuMjgtNy45NjgtMi4wNDgtMTYuMTI4LTIuNjcyLTIzLjEwNGwtMi40NjQtMjkuMzQ0bDE2LjA4LTUuNjMybDE1LjY4LDIzLjgwOGM0LjMyLDYuNzUyLDguNjQsMTMuNTIsMTIuNjA4LDIwLjUyOGwwLjI1Ni0wLjA5NiAgIGMtMS42NjQtNy44MDgtMi45MTItMTUuNjY0LTQuMDY0LTIzLjY2NGwtMy40NzItMjcuOTUybDE5Ljk2OC02Ljk5MmwyLjgxNiw3Mi40OTZsLTE5LjEzNiw2LjcwNGwtMTQuMjI0LTIwLjg4ICAgYy0zLjg1Ni01Ljg1Ni03LjEzNi0xMS4zMjgtMTEuMjY0LTE4Ljg5NmwtMC4yODgsMC4wOTZjMS42NjQsOC41NzYsMi40MTYsMTUuMDcyLDIuODQ4LDIxLjg0bDEuNDg4LDI1LjMyOGwtMTkuMTM2LDYuNzA0ICAgbC00MS45MDQtNTguODMyTDMwNS4zNzYsMTk3LjcxMnoiLz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หน่วย">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_of_item" runat="server" Style="color: blue" Text='<%# Eval("Unit_of_item_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdfPrice" runat="server" Value='<%# Eval("Agent_Price") %>' />
                                                            <asp:Label ID="lbl_Price" runat="server" Style="color: blue" Text='<%# Eval("Agent_Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนสต๊อก" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                             <asp:HiddenField ID="hdfVat" runat="server" Value='<%# Eval("Vat") %>' />
                                                            <asp:Label ID="lbl_Stock_on_hand" runat="server" Style="color: blue" Text='<%# Eval("Stock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนแนะนำ" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Suggest_Quantity" runat="server" Style="color: blue" Text='<%# Eval("Suggestion_Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนที่สั่ง" ItemStyle-HorizontalAlign="right" ItemStyle-Width="84px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderingAmount" runat="server" Width="84px"
                                                                Text='<%# Eval("Quantity") %>' Style="text-align: right" MaxLength="5"
                                                                TabIndex="<%# Container.DataItemIndex %>"></asp:TextBox>
                                                            <asp:HiddenField ID="hdfOldQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="มูลค่าสินค้า" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Amount" runat="server" Width="84px" Style="color: blue; text-align: right" Text='<%# Eval("Total","{0:N2}") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfAmount" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vat" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Vat" runat="server" Style="color: blue" Text='<%# Eval("Vat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Point" ItemStyle-HorizontalAlign="right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Point" runat="server" Style="color: blue" Text='<%# Eval("Point") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            <%--</div>--%>
 </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:HiddenField ID="btnSaveMode" runat="server" />
                                <asp:HiddenField ID="hdfPosition" runat="server" />
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                <asp:Button ID="butNo" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="butNo_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                            </div>
                            <div class="col-md-12 text-center">
                                <br />
                            </div>

                             <button id="btn-pop" rel="popover" type="button" class="btn btn-primary " data-original-title="ข้อมูลการสั่งซื้อสินค้า" 
                                 data-content="<div>Check this amazing feature</div><div>Check this amazing feature</div>" style="margin-left:2px"> 
                                    <span class="glyphicon glyphicon-transfer"></span>                                    
                             </button>

                            <!--data-trigger="focus"-->

                            <a tabindex="0"
                               id="link_pop"
                               class="btn btn-primary" 
                               role="button" 
                               data-html="true" 
                               data-toggle="popover"                                
                               title="<div class=text-label-bold><b>ข้อมูลการสั่งซื้อสินค้า</b></div>" 
                               style="margin-left:2px"
                               data-content="<div class=text-label-bold>ราคารวมสั่งซื้อทั้งหมด: <br>
                                <input id=lblTotal style=color:blue;border:0px;width:120px></div>
                                <div class=text-label-bold>VAT:<br>
                                <input id=lblVat style=color:blue;border:0px;width:120px></div>
                                <div class=text-label-bold>ยอดรวมทั้งสิ้น: <br>
                                <input id=lblSumTotal style=color:blue;border:0px;width:120px></div>
                                <div class=text-label-bold>ยอดห่างจากเป้า: <br>
                                <input id=lblDiff style=color:red;border:0px;width:120px></div>">
                                <span class="glyphicon glyphicon-info-sign"></span>

                            </a>

                        </div>
                    </div>

                </div>
                                   
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
