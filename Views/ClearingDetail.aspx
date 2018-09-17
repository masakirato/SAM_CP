<%@ Page Title="Clearing" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="ClearingDetail.aspx.cs" Inherits="Views_ClearingDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height:45px">
                <span class="title" style="font-size:18px">เคลียร์เงิน</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-piggy-bank" style="font-size:18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>
         </div>
    </div>

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
    </script>

    <div class="panel panel-default" style="margin-right:10px;border-radius:0px;border: 1px solid #d4d1d1">
        <div class="container" style="padding-left:30px;padding-right:30px">
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

                            <li role="presentation" class="active">
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

                    <form role="form">
                        <div class="tab-content">
                            <div class="tab-pane" role="tabpanel" id="step1">
                                <h3>ขั้นตอนที่ 1</h3>
                                <p>เบิกสินค้า : กรอกข้อมูลการเบิกสินค้า และ เลือกรูปแบบการเบิกสินค้า</p>
                                <ul class="list-inline pull-right">
                                    <li>
                                        <button type="button" class="btn btn-default">ยกเลิก</button>
                                    </li>
                                    <li>
                                        <button type="button" class="btn btn-danger" style="width:120px">บันทึก และ ถัดไป</button>
                                        
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-pane active" role="tabpanel" id="step2">
                                <h3>ขั้นตอนที่ 2</h3>
                                <p>สรุปรายการประจำวัน : กรอกรายการต่างๆประจำวัน</p>
                                <ul class="list-inline pull-right">
                                    <li>
                                        <button type="button" class="btn btn-default prev-step" onclick="window.location.replace('ClearingNew');">กลับ</button></li>
                                    <li>
                                        <button type="button" class="btn btn-danger next-step" style="width:120px" onclick="window.location.replace('ClearingConfirm');">ต่อไป</button></li>
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

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height:45px">
                <span style="font-size:18px">ขั้นตอนที่ 2 : สรุปรายการประจำวัน - กรอกรายการต่างๆประจำวัน</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-info-sign" style="font-size:18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>
            <div class="col-md-12 bar-content" style="height: auto;padding-top:20px;background:#fbfafa">
                <div class="col-md-12">
                   <span class="title" style="float:right">บังคับกรอกข้อมูล</span> 
                </div><br /><br />
                 <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        รหัส CV
                    </div>
                    <div class="col-md-3" style="line-height: 40px;width:280px">
                        <span style="color: blue">301543</span>
                    </div>  
                     <div class="col-md-1" style="width: 200px; line-height: 40px">
                        วันที่
                    </div>
                     <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">04 April 2017</span>
                    </div>                  
                </div>

                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        ตัวแทนจำหน่าย
                    </div>
                    <div class="col-md-3" style="line-height: 40px;width:280px">
                        <span style="color: blue">หจก. ค้าขายใจดี</span>
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        พนักงานขาย
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">30010 : นางสุชาดา ใจดี</span>
                    </div>
                </div>
                
                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        หมายเหตุ
                    </div>
                    <div class="col-md-3" style="line-height: 40px;width:280px">
                        <textarea class="form-control" placeholder="หมายเหตุ..." rows="3" style="width: 250px;padding-left:10px">เคลียร์วันที่ 04-04-2017</textarea>
                    </div>
                    <div class="col-md-1" style="width: 200px; line-height: 40px">
                        ยอดตั้งหนี้คงค้างทั้งหมด
                    </div>
                    <div class="col-md-3" style="line-height: 40px">
                        <span style="color: blue">5,000.00</span>
                    </div>
                    
                </div>

                <div class="col-md-12" style="padding-top: 20px">
                    <div class="panel panel-default">
                        <div class="panel-body">

                            <div class="col-md-12">
                                <span style="color: blue">รายการประจำวัน</span>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    1.) ยอดขายรวมจำนวนสินค้า
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="700" disabled="disabled"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    ชิ้น
                                </div>
                            </div>

                              <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    2.) ยอดขายรวมทั้งหมด
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="25,000.00" disabled="disabled"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            <div class="col-md-12">
                            <hr />
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    3.) ยอดเงินสดสมาชิก
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="border-left: 4px solid #ed1c24;padding-left:10px;text-align:right;color:blue" value="2,400.00"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    4.) ยอดเงินสด SP
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="border-left: 4px solid #ed1c24;padding-left:10px;text-align:right;color:blue" value="13,250.00"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    5.) เก็บเงินเครดิต ลูกค้า
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="border-left: 4px solid #ed1c24;padding-left:10px;text-align:right;color:blue" value="5,302.00"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    6.) ยอดเครดิต ลูกค้า
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="border-left: 4px solid #ed1c24;padding-left:10px;text-align:right;color:blue" value="4,048.00"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    7.) ยอดวางบิล
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="border-left: 4px solid #ed1c24;padding-left:10px;text-align:right;color:blue" value="12,450.00"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            <div class="col-md-12">
                            <hr />
                            </div>

                             <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    รวมเงินสดจ่าย
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="20,952.00" disabled="disabled"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    เงินช่วยเหลือ
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="border-left: 4px solid #ed1c24;padding-left:10px;text-align:right;color:blue" value="150.00"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                             <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    ยอดสรุปเงินสดจ่าย
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="20,782.00" disabled="disabled"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            <div class="col-md-12">
                            <hr />
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3" style="line-height: 40px">
                                    จ่ายจริง
                                </div>
                                <div class="col-md-3" style="line-height: 40px; width: 200px">
                                    <input type="text" class="form-control" style="border-left: 4px solid #ed1c24;padding-left:10px;text-align:right;color:blue" value="15,000.00"/>
                                </div>
                                <div class="col-md-1" style="line-height: 40px">
                                    บาท
                                </div>
                            </div>

                            

                        </div>
                    </div>
                </div>



            </div>
        </div>
    </div>

</asp:Content>

