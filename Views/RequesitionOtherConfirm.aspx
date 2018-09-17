﻿<%@ Page Title="Requesition" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="RequesitionOtherConfirm.aspx.cs" Inherits="Views_RequesitionOtherConfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height:45px">
                <span class="title" style="font-size:18px">เบิกอื่นๆ</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-transfer" style="font-size:18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>
            <div class="col-md-12 bar-content" style="height: auto;padding-top:20px;background:#fbfafa">
                 <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        รหัส CV
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">301543</span>
                    </div>                    
                </div>
                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        ตัวแทนจำหน่าย
                    </div>
                    <div class="col-md-3" style="line-height: 40px;width:280px">
                        <span style="color: blue">หจก. ค้าขายใจดี</span>
                    </div>
                    <div class="col-md-1" style="width: 170px; line-height: 40px">
                        จำนวนรายการที่เบิก
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">5</span>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        หมายเหตุ
                    </div>
                    <div class="col-md-3" style="line-height: 40px;width:280px">
                        <span style="color: blue">เบิกของแถมประจำเดือน 04-2017</span>
                    </div>
                    <!--<div class="col-md-1" style="width: 200px; line-height: 40px">
                        ราคารวมสั่งสินค้าทั้งหมด
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">0.00</span>
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        ภาษีทั้งหมด
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">0.00</span>
                    </div>
                    <div class="col-md-2" style="line-height: 40px">
                        &nbsp;
                    </div>
                    <div class="col-md-3" style="line-height: 40px;width:280px">
                        <span style="color: blue"></span>
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        ราคารวมทั้งหมด
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">0.00</span>
                    </div>-->
                </div>

                <!-- PRODUCT LIST -->
                <div class="panel panel-default" style="margin-top: 140px">
                    <div class="panel-heading">รายการสินค้าที่เบิก</div>
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
                                                <th style="font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">ชื่อสินค้า</th>
                                                <th style="width: 80px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">หน่วย</th>
                                                <th style="width: 120px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">ราคาต่อหน่วย</th>
                                                <th style="width: 80px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">คงเหลือ</th>
                                                <th style="width: 120px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">จำนวนที่เบิก</th>
                                                <th style="width: 150px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">มูลค่าสินค้า</th>
                                            </thead>

                                            <tr>
                                                <td colspan="6" style="padding-left: 0px">
                                                    <button type="button" class="btn btn-success" data-toggle="myCollapse" data-target="#product-group1" style="font-size: 14px">นมพาสเจอร์ไรส์</button>
                                                    - 
                                                    <span style="color: #000">(4 รายการ : 642.40 บาท)</span>
                                                </td>
                                            </tr>

                                            <tbody id="product-group1" class="myCollapse">

                                                <tr>
                                                    <td colspan="6" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">200 CC.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมสด 200 CC. เมจิโอ
                                                    </td>
                                                    <td style="line-height: 35px">ขวด
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7.80
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">23
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="10" />
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">78.00
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมสด 200 CC. กาแฟ
                                                    </td>
                                                    <td style="line-height: 35px">ขวด
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7.80
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">20
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="20" />
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">156.00
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมสด 200 CC. ขาดมันเนย
                                                    </td>
                                                    <td style="line-height: 35px">ขวด
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">8.05
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">16
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="4" />
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">32.20
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">946 CC.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมสด 946 CC. ไฮโลว์ เฟรชแพค
                                                    </td>
                                                    <td style="line-height: 35px">กล่อง
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">31.35
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">15
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="12" />
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">376.20
                                                    </td>
                                                </tr>


                                            </tbody>

                                            <tr>
                                                <td colspan="6" style="padding-left: 0px">
                                                    <button type="button" class="btn btn-success" data-toggle="group2" data-target="#product-group2" style="font-size: 14px">นมเปรี้ยวเมจิ</button>
                                                    - 
                                                    <span style="color: #000">(1 รายการ : 93.60 บาท)</span>
                                                </td>
                                            </tr>

                                            <tbody id="product-group2" class="myCollapse">

                                                <tr>
                                                    <td colspan="6" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">200 CC.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="color: blue; font-size: 15px; line-height: 35px">นมเปรี้ยว 200 CC. เมจิโอ
                                                    </td>
                                                    <td style="line-height: 35px">ขวด
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">7.80
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">12
                                                    </td>
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="12" />
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">93.60
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

                <!--<div class="col-md-12 text-center" style="padding-top: 10px;padding-bottom:20px">
                    <asp:Button runat="server" Text="บันทึก" CssClass="btn btn-danger"/>&nbsp;&nbsp;<asp:Button runat="server" Text="ยกเลิก" CssClass="btn btn-danger"/>
                </div>-->

            </div>
        </div>
    </div>
    <span id="date" style="color: blue;visibility:hidden"></span>

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
   
    <div class="panel panel-default" style="margin-right:10px">
    <div class="container" style="padding-left:30px;padding-right:30px">
        <div class="row">
            <section>
                <div class="wizard">    

                    <!--
                    <div class="wizard-inner">
                        <div class="connecting-line"></div>
                        <ul class="nav nav-tabs" role="tablist">

                            <li role="presentation" class="active">
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
                            <li role="presentation" class="disabled">
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
                    -->

                    <form role="form">
                        <div class="tab-content">
                            <div class="tab-pane " role="tabpanel" id="step1">
                                <h3>ขั้นตอนที่ 1</h3>
                                <p>เบิกอื่นๆ : เลือกสินค้า และ จำนวนที่เบิก ตามต้องการ</p>
                                <ul class="list-inline pull-right">
                                    <li>
                                        <button type="button" class="btn btn-default">ยกเลิก</button>
                                    </li>
                                    <li>
                                        <button type="button" class="btn btn-danger" style="width:120px" onclick="window.location.replace('RequesitionOtherConfirm'); ">บันทึก และ ถัดไป</button>
                                        
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-pane active" role="tabpanel" id="step2">
                                <h3>ขั้นตอนที่ 2</h3>
                                <p>ตรวจสอบความถูกต้อง : ตรวจสอบสินค้า และ จำนวน</p>
                                <ul class="list-inline pull-right">
                                    <li>
                                        <button type="button" class="btn btn-default prev-step">กลับ</button></li>
                                    <li>
                                        <button type="button" class="btn btn-danger next-step" style="width:120px" onclick="window.location.replace('RequesitionOtherInfo'); ">ยืนยัน</button></li>
                                </ul>
                            </div>
                            <div class="tab-pane" role="tabpanel" id="step3">
                                <h3>ขั้นตอนที่ 3</h3>
                                <p>ยืนยันการสั่งซื้อ : ยืนยันการสั่งซื้อ และ พิมพ์รายงาน</p>
                                <ul class="list-inline pull-right">
                                    <li>
                                        <button type="button" class="btn btn-default prev-step">กลับ</button></li>
                                    <li>
                                        <button type="button" class="btn btn-danger next-step">ข้าม และ ยืนยันการสั่งซื้อ</button></li>
                                    <li>
                                        <button type="button" class="btn btn-danger btn-info-full next-step">พิมพ์รายงาน และ ยืนยันการสั่งซื้อ</button></li>
                                </ul>
                            </div>
                            <div class="tab-pane" role="tabpanel" id="complete">
                                <h3 style="color:green">สำเร็จ</h3>
                                <p>การสั่งซื้อสินค้าเสร็จสิ้น</p>
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

