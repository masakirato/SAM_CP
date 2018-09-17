<%@ Page Title="Ordering" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="OrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- General Info-->
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">สั่งซื้อสินค้า</span>&nbsp;&nbsp;
                <span class="glyphicon glyphicon-transfer" style="font-size: 18px"></span>
                <!--<img src="Images/help-contents.png" />-->
            </div>
            <div class="col-md-12 bar-content" style="height: 660px; padding-top: 20px">
                <div class="col-md-12">
                    <span style="font-size: 16px">ค้นหาสั่งซื้อสินค้า</span>
                </div>
                <div class="col-md-12" style="margin-top: 20px">
                    <div class="col-md-1">
                        เลขที่ PO
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                            <span class="input-group-addon" id="basic-addon1"><span class="glyphicon glyphicon-search"></span></span>
                            <input type="text" class="form-control" placeholder="กรอกเลขที่ PO" aria-describedby="basic-addon1">
                        </div>
                    </div>
                    <div class="col-md-2">
                        สถาณะการสั่งซื้อ
                    </div>
                    <div class="col-md-2">
                        <div class="dropdown">
                            <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span style="color: #969494">&nbsp;เลือกสถาณะใบ PO&nbsp;</span>
                                <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
                                <li><a href="#">ส่งแล้ว</a></li>
                                <li><a href="#">ยังไม่ส่ง</a></li>
                                <li role="separator" class="divider"></li>
                                <li><a href="#">อื่นๆ</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <asp:Button runat="server" Text="ค้นหา" CssClass="btn btn-danger" />&nbsp;<asp:Button runat="server" Text="ยกเลิก" CssClass="btn btn-danger" />
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 20px">
                    <div class="col-md-1">
                        วันที่สั่ง
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                            <input type="text" class="form-control" placeholder="01-10-2016" style="width: 110px; padding-left: 10px; margin-right: 10px">
                            <input type="text" class="form-control" placeholder="31-10-2016" style="width: 110px; padding-left: 10px">
                        </div>
                    </div>
                    <div class="col-md-1">
                        วันที่ส่ง
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                            <input type="text" class="form-control" placeholder="01-10-2016" style="width: 110px; padding-left: 10px; margin-right: 10px">
                            <input type="text" class="form-control" placeholder="31-10-2016" style="width: 110px; padding-left: 10px">
                        </div>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12" style="padding-top: 20px">
                    <asp:Button runat="server" Text="สั่งซื้อสินค้า" CssClass="btn btn-danger" OnClick="Unnamed3_Click" />&nbsp;|&nbsp;
                    <span class="pull-right">
                        <asp:Button runat="server" Text="พิมพ์รายการสั่งซื้อสินค้า" CssClass="btn btn-primary" />&nbsp;<asp:Button runat="server" Text="Export ข้อมูลรายการสั่งซื้อสินค้า" CssClass="btn btn-primary" />
                    </span>
                </div>
                <div class="col-md-12" style="padding-top: 20px">
                    <div class="panel panel-default">
                        <div class="panel-body" style="max-height: 320px; overflow-y: scroll;">
                            <div class="table-container">
                                <table id="mytableee" class="table table-bordred table-striped">
                                    <thead>
                                        <th style="width: 30px; border-bottom: 2px solid #808080">
                                            <input type="checkbox" id="checkall" /></th>
                                        <th style="width: 60px; font-weight: 100; border-bottom: 2px solid #808080">ลำดับ</th>
                                        <th style="font-weight: 100; border-bottom: 2px solid #808080">เลขที่ PO</th>
                                        <th style="width: 150px; font-weight: 100; border-bottom: 2px solid #808080">วันที่ส่ง</th>
                                        <th style="width: 80px; font-weight: 100; border-bottom: 2px solid #808080">เวลา</th>
                                        <th style="width: 150px; font-weight: 100; border-bottom: 2px solid #808080">ยอดเงิน</th>
                                        <th style="width: 150px; font-weight: 100; border-bottom: 2px solid #808080">สถานะใบ PO</th>
                                        <th style="width: 60px; font-weight: 100; border-bottom: 2px solid #808080">Sheet</th>
                                        <!--<th>Edit</th>

                                        <th>Delete</th>-->
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td style="padding: 10px 10px; margin: 0px; border: 0px">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding: 10px 10px; margin: 0px; border: 0px">1</td>
                                            <td style="padding: 10px 10px; margin: 0px; border: 0px"><a href="#" style="color: #e6400a">PO-30010-20161013001</a></td>
                                            <td style="padding: 10px 10px; margin: 0px; border: 0px; color: blue">13-ต.ค.-2016</td>
                                            <td style="padding: 10px 10px; margin: 0px; border: 0px; color: blue; text-align: right">10.15</td>
                                            <td style="padding: 10px 10px; margin: 0px; border: 0px; color: blue; text-align: right">85,000</td>
                                            <td style="padding: 10px 10px; margin: 0px; border: 0px">ส่งแล้ว</td>
                                            <td style="padding: 10px 10px; margin: 0px; border: 0px; text-align: center"><a href="#">
                                                <img src="../Images/application-pdf.png" />
                                            </a></td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 10px 10px; margin: 0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding: 10px 10px; margin: 0px;">2</td>
                                            <td style="padding: 10px 10px; margin: 0px;"><a href="#" style="color: #e6400a">PO-30010-20161014001</a></td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue">14-ต.ค.-2016</td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue; text-align: right">10.00</td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue; text-align: right">13,000</td>
                                            <td style="padding: 10px 10px; margin: 0px;">ส่งแล้ว</td>
                                            <td style="padding: 10px 10px; margin: 0px; text-align: center"><a href="#">
                                                <img src="../Images/application-pdf.png" />
                                            </a></td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 10px 10px; margin: 0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding: 10px 10px; margin: 0px;">3</td>
                                            <td style="padding: 10px 10px; margin: 0px;"><a href="#" style="color: #e6400a">PO-30010-20161015001</a></td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue">15-ต.ค.-2016</td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue; text-align: right">10.15</td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue; text-align: right">75,000</td>
                                            <td style="padding: 10px 10px; margin: 0px;">ส่งแล้ว</td>
                                            <td style="padding: 10px 10px; margin: 0px; text-align: center"><a href="#">
                                                <img src="../Images/application-pdf.png" />
                                            </a></td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 10px 10px; margin: 0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding: 10px 10px; margin: 0px;">4</td>
                                            <td style="padding: 10px 10px; margin: 0px;"><a href="#" style="color: #e6400a">PO-30010-20161016001</a></td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue">16-ต.ค.-2016</td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue; text-align: right">11.30</td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue; text-align: right">5,000</td>
                                            <td style="padding: 10px 10px; margin: 0px;">ส่งแล้ว</td>
                                            <td style="padding: 10px 10px; margin: 0px; text-align: center"><a href="#">
                                                <img src="../Images/application-pdf.png" />
                                            </a></td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 10px 10px; margin: 0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding: 10px 10px; margin: 0px;">5</td>
                                            <td style="padding: 10px 10px; margin: 0px;"><a href="#" style="color: #e6400a">PO-30010-20161017001</a></td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue">17-ต.ค.-2016</td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue; text-align: right">09.00</td>
                                            <td style="padding: 10px 10px; margin: 0px; color: blue; text-align: right">65,500</td>
                                            <td style="padding: 10px 10px; margin: 0px;">ส่งแล้ว</td>
                                            <td style="padding: 10px 10px; margin: 0px; text-align: center"><a href="#">
                                                <img src="../Images/application-pdf.png" />
                                            </a></td>
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
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>







</asp:Content>

