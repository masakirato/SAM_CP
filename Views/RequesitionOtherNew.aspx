<%@ Page Title="Requesition" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="RequesitionOtherNew.aspx.cs" Inherits="Views_RequesitionOtherNew" %>

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
                        แสดงสินค้าที่มีในคลัง
                    </div>
                    <div class="col-md-3">
                        <input type="checkbox" style="margin-top:13px" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        หมายเหตุ
                    </div>
                    <div class="col-md-3" style="line-height: 40px;width:280px">
                        <textarea class="form-control" placeholder="หมายเหตุ..." rows="3" style="width: 250px;padding-left:10px"></textarea>
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
                <div class="panel panel-default" style="margin-top: 180px">
                    <div class="panel-heading">เลือกสินค้า</div>
                    <div class="panel-body" style="max-height: 750px;">
                        <div class="tabbable-panel" style="margin-top:20px">
                            <div class="tabbable-line">
                                <ul class="nav nav-tabs ">
                                    <li class="active " style="width: 180px">
                                        <img src="../Images/tab1.png" id="imgTab1" style="position: absolute;top: -50px; z-index: 999" />
                                        <a href="#tab_default_1" id="tab1" data-toggle="tab" style="padding-left: 30px" class="text-center">นมพาสเจอร์ไรส์</a>
                                        <script>
                                            $("#tab1").on('click', function (e) {
                                                $("#imgTab1").show();

                                                $("#imgTab2").hide();
                                                $("#imgTab3").hide();
                                                $("#imgTab4").hide();
                                                $("#imgTab5").hide();
                                                return true;
                                            });
                                        </script>
                                    </li>
                                    <li style="width: 160px">
                                        <img src="../Images/tab2.png" id="imgTab2" style="position: absolute; top: -50px;display:none;z-index:999" />
                                        <a href="#tab_default_2" id="tab2" data-toggle="tab" style="padding-left: 30px" class="text-center">นมเปรี้ยวเมจิ </a>
                                        <script>
                                            $("#tab2").on('click', function (e) {
                                                $("#imgTab2").show();
                                                
                                                $("#imgTab1").hide();
                                                $("#imgTab3").hide();
                                                $("#imgTab4").hide();
                                                $("#imgTab5").hide();
                                                return true;
                                            });
                                        </script>
                                    </li>
                                    <li style="width: 160px">
                                        <img src="../Images/tab3.png" id="imgTab3" style="position: absolute; top: -50px;display:none;z-index:999" />
                                        <a href="#tab_default_3" id="tab3" data-toggle="tab" style="padding-left: 30px" class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;โยเกิร์ตเมจิ </a>
                                        <script>
                                            $("#tab3").on('click', function (e) {
                                                $("#imgTab3").show();

                                                $("#imgTab1").hide();
                                                $("#imgTab2").hide();
                                                $("#imgTab4").hide();
                                                $("#imgTab5").hide();
                                                return true;
                                            });
                                        </script>
                                    </li>
                                    <li style="width: 160px">
                                        <img src="../Images/tab4.png" id="imgTab4" style="position: absolute; top: -50px;display:none;z-index:999" />
                                        <a href="#tab_default_3" id="tab4" data-toggle="tab" style="padding-left: 30px" class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;นมเปรี้ยวไพเก้น </a>
                                        <script>
                                            $("#tab4").on('click', function (e) {
                                                $("#imgTab4").show();

                                                $("#imgTab1").hide();
                                                $("#imgTab2").hide();
                                                $("#imgTab3").hide();
                                                $("#imgTab5").hide();
                                                return true;
                                            });
                                        </script>
                                    </li>
                                    <li style="width: 160px">
                                        <img src="../Images/tab5.png" id="imgTab5" style="position: absolute; top: -50px;display:none;z-index:999" />
                                        <a href="#tab_default_3" id="tab5" data-toggle="tab" style="padding-left: 30px" class="text-center">&nbsp;&nbsp;&nbsp;&nbsp;โยเกิร์ตไพเก้น </a>
                                        <script>
                                            $("#tab5").on('click', function (e) {
                                                $("#imgTab5").show();

                                                $("#imgTab1").hide();
                                                $("#imgTab2").hide();
                                                $("#imgTab3").hide();
                                                $("#imgTab4").hide();
                                                return true;
                                            });
                                        </script>
                                    </li>
                                    <li style="width: 150px">
                                        <a href="#tab_default_3" id="tab6" data-toggle="tab" style="padding-left: 30px" class="text-center">อื่นๆ </a>
                                        <script>
                                            $("#tab6").on('click', function (e) {                                               
                                                $("#imgTab1").hide();
                                                $("#imgTab2").hide();
                                                $("#imgTab3").hide();
                                                $("#imgTab4").hide();
                                                $("#imgTab5").hide();
                                                return true;
                                            });
                                        </script>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_default_1">
                                        <table id="productListTable" class="table table-bordred table-striped" style="margin-top:10px">
                                            <thead>
                                                <th style="font-weight:100;border-bottom:2px solid #7486ce;background:#304d93;color:#fff;text-align:center">รหัสสินค้า</th>
                                                <th style="width:250px;font-weight:100;border-bottom:2px solid #7486ce;background:#304d93;color:#fff;text-align:center">ชื่อสินค้า</th>
                                                <th style="width:80px;font-weight:100;border-bottom:2px solid #7486ce;background:#304d93;color:#fff;text-align:center">หน่วย</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #7486ce;background:#304d93;color:#fff;text-align:center">ราคาต่อหน่วย</th>
                                                <th style="width:80px;font-weight:100;border-bottom:2px solid #7486ce;background:#304d93;color:#fff;text-align:center">คงเหลือ</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #7486ce;background:#304d93;color:#fff;text-align:center">จำนวนที่เบิก</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #7486ce;background:#304d93;color:#fff;text-align:center">มูลค่าสินค้า</th>                                            
                                            </thead>
                                            <tbody style="overflow:scroll">
                                           
                                            <tr>
                                                <td colspan="7" style="background:#c6c2c2;height:30px;padding-top:10px;border-left: 4px solid #4949ae;border-top:0px;color:#000">
                                                    200 CC.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    72000098
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    นมสด 200 CC. เมจิโอ
                                                </td>
                                                <td style="line-height:35px">
                                                    ขวด
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="23" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    72000108
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    นมสด 200 CC. กาแฟ
                                                </td>
                                                <td style="line-height:35px">
                                                    ขวด
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="20" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    72000111
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    นมสด 200 CC. ขาดมันเนย
                                                </td>
                                                <td style="line-height:35px">
                                                    ขวด
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="8.05" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="16" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    72000112
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    นมสด 200 CC. จืด
                                                </td>
                                                <td style="line-height:35px">
                                                    ขวด
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="8.05" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="50" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    72000121
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    นมสด 200 CC. ช็อคโกแลต
                                                </td>
                                                <td style="line-height:35px">
                                                    ขวด
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:red" value="0" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" style="background:#c6c2c2;height:30px;padding-top:10px;border-left: 4px solid #4949ae;border-top:0px;color:#000">
                                                    946 CC.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    72000513
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    นมสด 946 CC. ไฮโลว์ เฟรชแพค
                                                </td>
                                                <td style="line-height:35px">
                                                    กล่อง
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="31.35" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="15" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    72000516
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    นมสด 946 CC. 4.3% เฟรชแพค
                                                </td>
                                                <td style="line-height:35px">
                                                    กล่อง
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="35.13" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="15" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            </tbody>
                                        </table>                                     
                                    </div>
                                    <div class="tab-pane" id="tab_default_2">
                                        <table id="productListTable2" class="table table-bordred table-striped" style="margin-top:10px">
                                            <thead>
                                                <th style="font-weight:100;border-bottom:2px solid #f67075;background:#ed1c24;color:#fff;text-align:center">รหัสสินค้า</th>
                                                <th style="width:250px;font-weight:100;border-bottom:2px solid #f67075;background:#ed1c24;color:#fff;text-align:center">ชื่อสินค้า</th>
                                                <th style="width:80px;font-weight:100;border-bottom:2px solid #f67075;background:#ed1c24;color:#fff;text-align:center">หน่วย</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #f67075;background:#ed1c24;color:#fff;text-align:center">ราคาต่อหน่วย</th>
                                                <th style="width:80px;font-weight:100;border-bottom:2px solid #f67075;background:#ed1c24;color:#fff;text-align:center">คงเหลือ</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #f67075;background:#ed1c24;color:#fff;text-align:center">จำนวนที่เบิก</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #f67075;background:#ed1c24;color:#fff;text-align:center">มูลค่าสินค้า</th>                                            
                                            </thead>
                                            <tbody style="overflow:scroll">
                                           
                                            <tr>
                                                <td colspan="7" style="background:#faaa90;height:30px;padding-top:10px;border-left: 4px solid #ed1c24;border-top:0px;color:#000">
                                                    XXX CC.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="15" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="35" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="7" style="background:#faaa90;height:30px;padding-top:10px;border-left: 4px solid #ed1c24;border-top:0px;color:#000">
                                                    XXX CC.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="31.35" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="35.13" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            </tbody>
                                        </table>  
                                    </div>
                                    <div class="tab-pane" id="tab_default_3">
                                       <table id="productListTable3" class="table table-bordred table-striped" style="margin-top:10px">
                                            <thead>
                                                <th style="font-weight:100;border-bottom:2px solid #6ac674;background:#186220;color:#fff;text-align:center">รหัสสินค้า</th>
                                                <th style="width:250px;font-weight:100;border-bottom:2px solid #6ac674;background:#186220;color:#fff;text-align:center">ชื่อสินค้า</th>
                                                <th style="width:80px;font-weight:100;border-bottom:2px solid #6ac674;background:#186220;color:#fff;text-align:center">หน่วย</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #6ac674;background:#186220;color:#fff;text-align:center">ราคาต่อหน่วย</th>
                                                <th style="width:80px;font-weight:100;border-bottom:2px solid #6ac674;background:#186220;color:#fff;text-align:center">คงเหลือ</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #6ac674;background:#186220;color:#fff;text-align:center">จำนวนที่เบิก</th>
                                                <th style="width:150px;font-weight:100;border-bottom:2px solid #6ac674;background:#186220;color:#fff;text-align:center">มูลค่าสินค้า</th>                                            
                                            </thead>
                                            <tbody style="overflow:scroll">
                                           
                                            <tr>
                                                <td colspan="7" style="background:#88ca90;height:30px;padding-top:10px;border-left: 4px solid #186220;border-top:0px;color:#000">
                                                    XXX CC.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:red" value="0" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:red" value="0" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="2" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="6" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="7.80" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="10" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" style="background:#88ca90;height:30px;padding-top:10px;border-left: 4px solid #186220;border-top:0px;color:#000">
                                                    XXX CC.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="31.35" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="30" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="line-height:35px">
                                                    XXXXXXXX
                                                </td>
                                                <td style="color:blue;font-size:15px;line-height:35px">
                                                    XXXXXXXXXXXXXXXX
                                                </td>
                                                <td style="line-height:35px">
                                                    XXXX
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="35.13" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:red" value="0" disabled="disabled"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" style="padding-left:10px;text-align:right;color:blue" value="0.00" disabled="disabled"/>
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
                            <div class="tab-pane active" role="tabpanel" id="step1">
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
                            <div class="tab-pane" role="tabpanel" id="step2">
                                <h3>ขั้นตอนที่ 2</h3>
                                <p>ตรวจสอบความถูกต้อง : ตรวจสอบสินค้า และ จำนวน</p>
                                <ul class="list-inline pull-right">
                                    <li>
                                        <button type="button" class="btn btn-default prev-step">กลับ</button></li>
                                    <li>
                                        <button type="button" class="btn btn-danger next-step" style="width:120px">ต่อไป</button></li>
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

