<%@ Page Title="Requesition Template" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="RequesitionTemplateNew.aspx.cs" Inherits="Views_RequesitionTemplateNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height:45px">
                <span style="font-size:18px">สร้างรูปแบบใบเบิกประจำตัว</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-info-sign" style="font-size:18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>
            <div class="col-md-12 bar-content" style="height: auto;padding-top:20px;background:#fbfafa">
                <div class="col-md-12">
                   <span class="title" style="float:right">บังคับกรอกข้อมูล</span> 
                </div><br /><br />

                <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        รูปแบบการเบิกสินค้า
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox class="form-control"  runat="server" placeholder="กรอกรูปแบบการเบิกสินค้า" style="border-left: 4px solid #ed1c24" ></asp:TextBox>
                    </div>
                    <div class="col-md-1" style="line-height: 40px">
                        สถานะ
                    </div>
                    <div class="col-md-2">
                       <asp:DropDownList class="form-control" runat="server" Width="150">
                           <asp:ListItem Text="">ใช้งาน</asp:ListItem>
                           <asp:ListItem Text="">ยกเลิก</asp:ListItem>
                       </asp:DropDownList>
                    </div>
                </div>

                <!-- PRODUCT LIST -->
                <div class="panel panel-default" style="margin-top: 70px">
                    <div class="panel-heading">รายการสินค้าที่เบิก</div>
                    <div class="panel-body" style="height: auto;">
                        <div class="tabbable-panel" style="margin-top: 20px">
                            <div class="tabbable-line">
                                <span class="pull-right">
                                    <asp:Button runat="server" Text="เพิ่มรายการสินค้า" CssClass="btn btn-primary" OnClientClick="return PopupCenter('PopupProduct','Product List', 700 , 600);"/>
                                </span>
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
                                                <th style="width: 100px;font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">รหัสสินค้า</th>
                                                <th style="font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">ชื่อสินค้า</th>
                                                <th style="width: 80px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">หน่วย</th>
                                                <th style="width: 120px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">ราคาต่อหน่วย</th>                                                
                                                <th style="width: 100px; font-weight: 100; border-bottom: 2px solid #7486ce; background: #304d93; color: #fff; text-align: center">จำนวนที่เบิก</th>
                                            </thead>

                                            <tr>
                                                <td colspan="6" style="padding-left: 0px">
                                                    <button type="button" class="btn btn-success" data-toggle="myCollapse" data-target="#product-group1" style="font-size: 14px">นมพาสเจอร์ไรส์</button>
                                                  
                                                </td>
                                            </tr>

                                            <tbody id="product-group1" class="myCollapse">

                                                <tr>
                                                    <td colspan="6" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">200 CC.
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
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="10" />
                                                    </td>

                                                    <td style="color: blue; line-height: 35px; text-align: right">
                                                        <asp:Button runat="server" Text="ลบ" CssClass="btn btn-danger"/>
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
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="20" />
                                                    </td>

                                                    <td style="color: blue; line-height: 35px; text-align: right">
                                                        <asp:Button runat="server" Text="ลบ" CssClass="btn btn-danger"/>
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
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="4" />
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">
                                                        <asp:Button runat="server" Text="ลบ" CssClass="btn btn-danger"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">946 CC.
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
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="12" />
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">
                                                        <asp:Button runat="server" Text="ลบ" CssClass="btn btn-danger"/>
                                                    </td>
                                                </tr>


                                            </tbody>

                                            <tr>
                                                <td colspan="6" style="padding-left: 0px">
                                                    <button type="button" class="btn btn-success" data-toggle="group2" data-target="#product-group2" style="font-size: 14px">นมเปรี้ยวเมจิ</button>
                                                   
                                                </td>
                                            </tr>

                                            <tbody id="product-group2" class="myCollapse">

                                                <tr>
                                                    <td colspan="6" style="background: #c6c2c2; height: 30px; padding-top: 10px; border-left: 4px solid #4949ae; border-top: 0px; color: #000">200 CC.
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
                                                    <td style="color: red; line-height: 35px; text-align: right">
                                                        <input type="text" class="form-control" style="padding-left: 10px; text-align: right; color: red" value="12" />
                                                    </td>
                                                    <td style="color: blue; line-height: 35px; text-align: right">
                                                        <asp:Button runat="server" Text="ลบ" CssClass="btn btn-danger"/>
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

        function OpenProductPopup(url, name) {
            newwindow = window.open(url, name, 'width=560,height=340,toolbar=0,menubar=0,resizable=0,location=0;top=100px;left:200px');
            if (window.focus) { newwindow.focus() }
            return false;
        }

        function PopupCenter(url, title, w, h) {
            // Fixes dual-screen position                         Most browsers      Firefox
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;
            var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

            // Puts focus on the newWindow
            if (window.focus) {
                newWindow.focus();
            }

            return false;
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
                            <div class="tab-pane " role="tabpanel" id="step1">
                                <h3>ขั้นตอนที่ 1</h3>
                                <p>เบิกอื่นๆ : เลือกสินค้า และ จำนวนที่เบิก ตามต้องการ</p>
                                <ul class="list-inline pull-right">
                                    <li>
                                        <button type="button" class="btn btn-default">ยกเลิก</button>
                                    </li>
                                    <li>
                                        <button type="button" class="btn btn-danger" style="width:120px" onclick="window.location.replace('RequesitionTemplateList'); ">บันทึก</button>
                                        
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-pane active" role="tabpanel" id="step2">
                                <ul class="list-inline text-center">
                                    <li>
                                        <button type="button" class="btn btn-default prev-step">ยกเลิก</button></li>
                                    <li>
                                        <button type="button" class="btn btn-danger next-step" style="width:120px" onclick="window.location.replace('RequesitionTemplateList'); ">บันทึก</button></li>
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

