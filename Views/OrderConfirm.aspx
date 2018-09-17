<%@ Page Title="Ordering" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="OrderConfirm.aspx.cs" Inherits="OrderConfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">ยืนยันรายการสั่งซื้อสินค้า</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>
            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        รหัส CV
                    </div>
                    <div class="col-md-3" style="line-height: 40px; width: 280px">
                        <span style="color: blue">301543</span>
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        เลขที่ใบสั่งซื้อ
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">30154PO01012016</span>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        ตัวแทนจำหน่าย
                    </div>
                    <div class="col-md-3" style="line-height: 40px; width: 280px">
                        <span style="color: blue">หจก. ค้าขายใจดี</span>
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        วันที่รับสินค้า
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">25-10-2016</span>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        หมายเหตุ
                    </div>
                    <div class="col-md-3" style="line-height: 40px; width: 280px">
                        <span style="color: blue">Urgent order</span>
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        ราคารวมสั่งสินค้าทั้งหมด
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">10,000.00</span>
                    </div>
                    <div class="col-md-2" style="line-height: 40px">
                        &nbsp;
                    </div>
                    <div class="col-md-3" style="line-height: 40px; width: 280px">
                        &nbsp;
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        ภาษีทั้งหมด
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">700.00</span>
                    </div>
                    <div class="col-md-2" style="line-height: 40px">
                        &nbsp;
                    </div>
                    <div class="col-md-3" style="line-height: 40px; width: 280px">
                        <span style="color: blue"></span>
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        ราคารวมทั้งหมด
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">10,700.00</span>
                    </div>
                </div>

                <!-- PRODUCT LIST -->
                <div class="panel panel-default" style="margin-top: 220px">
                    <div class="panel-heading">รายการสินค้าที่สั่ง</div>
                    <div class="panel-body" style="height: auto;">
                        <div class="tabbable-panel" style="margin-top: 20px">
                            <div class="tabbable-line">
                                <ul class="nav nav-tabs ">
                                    <li class="active " style="width: 250px">
                                        <img src="../Images/tab1.png" id="imgTab1" style="position: absolute; top: -50px; z-index: 999" />
                                        <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center">รายการทั้งหมด <span style="color: red">(5 รายการ)</span></a>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_default_1">
                                        <table id="productListTable" class="table table-bordred table-striped" style="margin-top: 10px">
                                            <thead>
                                                <th style="font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">รหัสสินค้า</th>
                                                <th style="width: 250px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">ชื่อสินค้า</th>
                                                <th style="width: 80px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">หน่วย</th>
                                                <th style="width: 150px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">ราคาต่อหน่วย</th>
                                                <th style="width: 80px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">VAT (%)</th>
                                                <th style="width: 150px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">จำนวนที่สั่ง</th>
                                                <th style="width: 150px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">มูลค่าสินค้า</th>
                                            </thead>

                                            <tr>
                                                <td colspan="7" style="padding-left: 0px">
                                                    <button type="button" class="btn btn-success" data-toggle="myCollapse" data-target="#product-group1" style="font-size: 14px">นมพาสเจอร์ไรส์</button>
                                                    - 
                                                    <span style="color: #000">(4 รายการ : 6,834.63 บาท)</span>
                                                </td>
                                            </tr>

                                            <tbody id="product-group1" class="myCollapse">

                                                <tr>
                                                    <td colspan="7" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">200 CC.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="line-height: 35px">72000098
                                                    </td>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมสด 200 CC. เมจิโอ
                                                    </td>
                                                    <td style="line-height: 35px">ขวด
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7.80
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        200
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">1,669.20
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="line-height: 35px">72000108
                                                    </td>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมสด 200 CC. กาแฟ
                                                    </td>
                                                    <td style="line-height: 35px">ขวด
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7.80
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        200
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">1,669.20
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="line-height: 35px">72000111
                                                    </td>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมสด 200 CC. ขาดมันเนย
                                                    </td>
                                                    <td style="line-height: 35px">ขวด
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">8.05
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        200
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">1,819.00
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">946 CC.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="line-height: 35px">72000513
                                                    </td>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมสด 946 CC. ไฮโลว์ เฟรชแพค
                                                    </td>
                                                    <td style="line-height: 35px">กล่อง
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">31.35
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        50
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">1,677.23
                                                    </td>
                                                </tr>


                                            </tbody>

                                            <tr>
                                                <td colspan="7" style="padding-left: 0px">
                                                    <button type="button" class="btn btn-success" data-toggle="group2" data-target="#product-group2" style="font-size: 14px">นมเปรี้ยวเมจิ</button>
                                                    - 
                                                    <span style="color: #000">(1 รายการ : 3,165.37 บาท)</span>
                                                </td>
                                            </tr>

                                            <tbody id="product-group2" class="myCollapse">

                                                <tr>
                                                    <td colspan="7" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">200 CC.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="line-height: 35px">72000299
                                                    </td>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมเปรี้ยว 200 CC. เมจิโอ
                                                    </td>
                                                    <td style="line-height: 35px">ขวด
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7.80
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        400
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">3,165.37
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
        </div>
    </div>
    <span id="date" style="color: blue; visibility: hidden"></span>

    <script>
        $(document).ready(function () {
            //Initialize tooltips
            $('.nav-tabs > li a[title]').tooltip();

            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {

                var $target = $(e.target);

                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            });

            $(".next-step").click(function (e) {

                var $active = $('.wizard .nav-tabs li.active');
                $active.next().removeClass('disabled');
                nextTab($active);

            });
            $(".prev-step").click(function (e) {

                var $active = $('.wizard .nav-tabs li.active');
                prevTab($active);

            });
        });

        function nextTab(elem) {
            $(elem).next().find('a[data-toggle="tab"]').click();
        }
        function prevTab(elem) {
            $(elem).prev().find('a[data-toggle="tab"]').click();
        }

        $("[data-toggle='myCollapse']").click(function (ev) {
            ev.preventDefault();
            var target;
            if (this.hasAttribute('data-target')) {
                target = $(this.getAttribute('data-target'));
            } else {
                target = $(this.getAttribute('href'));
            };
            target.toggleClass("in");
            console.log(target.hasClass('in'));
        });

        $("[data-toggle='group2']").click(function (ev) {
            ev.preventDefault();
            var target;
            if (this.hasAttribute('data-target')) {
                target = $(this.getAttribute('data-target'));
            } else {
                target = $(this.getAttribute('href'));
            };
            target.toggleClass("in");
            console.log(target.hasClass('in'));
        });
    </script>

    <div class="panel panel-default" style="margin-right: 10px">
        <div class="container" style="padding-left: 30px; padding-right: 30px">
            <div class="row">
                <section>
                    <div class="wizard">
                        <div class="wizard-inner">
                            <div class="connecting-line"></div>
                            <ul class="nav nav-tabs" role="tablist">

                                <li role="presentation" class="disabled">
                                    <a href="#step1" data-toggle="tab" aria-controls="step1" role="tab" title="Step 1">
                                        <span class="round-tab">
                                            <i class="glyphicon glyphicon-shopping-cart"></i>
                                        </span>
                                    </a>
                                </li>

                                <li role="presentation" class="disabled">
                                    <a href="#step2" data-toggle="tab" aria-controls="step2" role="tab" title="Step 2">
                                        <span class="round-tab">
                                            <i class="glyphicon glyphicon-pencil"></i>
                                        </span>
                                    </a>
                                </li>
                                <li role="presentation" class="active">
                                    <a href="#step3" data-toggle="tab" aria-controls="step3" role="tab" title="Step 3">
                                        <span class="round-tab">
                                            <i class="glyphicon glyphicon-list-alt"></i>
                                        </span>
                                    </a>
                                </li>

                                <li role="presentation" class="disabled">
                                    <a href="#complete" data-toggle="tab" aria-controls="complete" role="tab" title="Complete">
                                        <span class="round-tab">
                                            <i class="glyphicon glyphicon-ok"></i>
                                        </span>
                                    </a>
                                </li>
                            </ul>
                        </div>

                        <form role="form">
                            <div class="tab-content">
                                <div class="tab-pane" role="tabpanel" id="step1">
                                    <h3>ขั้นตอนที่ 1</h3>
                                    <p>สั้งซื้อสินค้า : เลือกสินค้า และ จำนวนที่สั่ง ตามต้องการ</p>
                                    <ul class="list-inline pull-right">
                                        <li>
                                            <button type="button" class="btn btn-default">ยกเลิก</button>
                                        </li>
                                        <li>
                                            <button type="button" class="btn btn-danger" style="width: 120px" onclick="window.location.replace('OrderDetail'); ">บันทึก และ ถัดไป</button>

                                        </li>
                                    </ul>
                                </div>
                                <div class="tab-pane" role="tabpanel" id="step2">
                                    <h3>ขั้นตอนที่ 2</h3>
                                    <p>ตรวจสอบความถูกจ้อง : ตรวจสอบสินค้า และ จำนวน</p>
                                    <ul class="list-inline pull-right">
                                        <li>
                                            <button type="button" class="btn btn-default prev-step">กลับ</button></li>
                                        <li>
                                            <button type="button" class="btn btn-danger next-step" style="width: 120px">ต่อไป</button></li>
                                    </ul>
                                </div>
                                <div class="tab-pane active" role="tabpanel" id="step3">
                                    <h3>ขั้นตอนที่ 3</h3>
                                    <p>ยืนยันการสั่งซื้อ : ยืนยันการสั่งซื้อ และ พิมพ์รายงาน</p>
                                    <ul class="list-inline pull-right">
                                        <li>
                                            <button type="button" class="btn btn-default prev-step" onclick="window.location.replace('OrderDetail');">กลับ</button></li>
                                        <li>
                                            <button type="button" class="btn btn-danger next-step">ข้าม และ ยืนยันการสั่งซื้อ</button></li>
                                        <li>
                                            <button type="button" class="btn btn-danger btn-info-full next-step" onclick="window.open('/PDF/ORDER_FORM.pdf')">พิมพ์รายงาน และ ยืนยันการสั่งซื้อ</button></li>
                                    </ul>
                                </div>
                                <div class="tab-pane" role="tabpanel" id="complete">
                                    <h3 style="color: green">สำเร็จ</h3>
                                    <p>การสั่งซื้อสินค้าเสร็จสิ้น</p>
                                    <a href="OrderList.aspx" style="color:blue"><p>กลับหน้ารายการสั่งสินค้า</p></a>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </form>
                    </div>
                </section>
            </div>
        </div>
    </div>
</asp:Content>

