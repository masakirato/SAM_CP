<%@ Page Title="Clearing" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="ClearingList.bak.aspx.cs" Inherits="Views_ClearingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height:45px">
                <span class="title" style="font-size:18px">เคลียร์เงิน</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-piggy-bank" style="font-size:18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: 660px;padding-top:20px">
                <div class="col-md-12">
                   <span style="font-size:16px">ค้นหาข้อมูลเคลียร์เงิน</span> 
                </div>
                <div class="col-md-12" style="margin-top: 20px">
                    <div class="col-md-1" style="width:110px">
                        เลขที่เอกสาร
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                          <span class="input-group-addon" id="basic-addon1"><span class="glyphicon glyphicon-search"></span></span>
                          <input type="text" class="form-control" placeholder="กรอกเลขที่เอกสาร" aria-describedby="basic-addon1">
                        </div>
                    </div>
                    <div class="col-md-1" style="width:150px">
                        รายชื่อพนักงานขาย
                    </div>
                    <div class="col-md-3">
                       <div class="dropdown">
                          <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" style="width:100%">
                            <span style="color:#969494">&nbsp;โปรดระบุรายชื่อพนักงานขาย&nbsp;</span>
                            <span class="caret"></span>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
                            <li><a href="#">30010 : นางสุชาดา ใจดี</a></li>
                            <li><a href="#">30011 : นางจันทิมา สุขสวัสดิ</a></li>
                          </ul>
                        </div>
                    </div>
                    <div class="col-md-3">
                       <asp:Button runat="server" Text="ค้นหา" CssClass="btn btn-danger"/>&nbsp;<asp:Button runat="server" Text="ยกเลิก" CssClass="btn btn-danger"/>
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 20px">
                    <div class="col-md-1" style="width:110px">
                        วันที่
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                            <input type="text" class="form-control" placeholder="01-10-2016" style="width:110px;padding-left:10px;margin-right:10px">
                            <input type="text" class="form-control" placeholder="31-10-2016" style="width:110px;padding-left:10px">
                        </div>
                    </div>
                    <!--<div class="col-md-1" style="width:150px">
                        สถานะใบเบิกสินค้า
                    </div>
                    <div class="col-md-3">
                       <div class="dropdown">
                          <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu11" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" style="width:70%">
                            <span style="color:#969494">&nbsp;โปรดระบุสถานะ&nbsp;</span>
                            <span class="caret"></span>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
                            <li><a href="#">ยังไม่เคลียร์เงิน</a></li>
                            <li><a href="#">เคลียร์เงินแล้ว</a></li>
                          </ul>
                        </div>
                    </div>-->
                    <!--
                    <div class="col-md-1">
                        วันที่ส่ง
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                            <input type="text" class="form-control" placeholder="01-10-2016" style="width:110px;padding-left:10px;margin-right:10px">
                            <input type="text" class="form-control" placeholder="31-10-2016" style="width:110px;padding-left:10px">
                        </div>
                    </div>
                    <div class="col-md-3">
                       
                    </div>-->
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12" style="padding-top: 20px">
                    <asp:Button runat="server" Text="เคลียร์เงินประจำวัน" CssClass="btn btn-danger" OnClick="Unnamed3_Click"/>&nbsp;|&nbsp;<asp:Button runat="server" Text="พิมพ์ใบเคลียร์เงิน" CssClass="btn btn-primary"/>
                </div>
                <div class="col-md-12" style="padding-top: 20px">
                    <div class="panel panel-default">
                        <div class="panel-body" style="max-height: 320px; overflow-y: scroll;">
                            <div class="table-container">                                
                                <table id="mytableee" class="table table-bordred table-striped">
                                    <thead>
                                        <th style="width:30px;border-bottom:2px solid #808080">
                                            <input type="checkbox" id="checkall" /></th>
                                        <th style="width:60px;font-weight:100;border-bottom:2px solid #808080">ลำดับ</th>
                                        <th style="font-weight:100;border-bottom:2px solid #808080">เลขที่เอกสาร</th>
                                        <th style="width:250px;font-weight:100;border-bottom:2px solid #808080">พนักงานขาย</th>
                                        <th style="width:100px;font-weight:100;border-bottom:2px solid #808080">วันที่</th>
                                        <th style="width:150px;font-weight:100;border-bottom:2px solid #808080">ยอดเงิน</th>
                                        <th style="width:60px;font-weight:100;border-bottom:2px solid #808080">Sheet</th>
                                        <!--<th>Edit</th>

                                        <th>Delete</th>-->
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;border:0px">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;border:0px">1</td>
                                            <td style="padding:10px 10px;margin:0px;border:0px"><a href="#" style="color:#e6400a">CL-30010-20161013001</a></td>
                                            <td style="padding:10px 10px;margin:0px;border:0px;color:blue">30010 / นางสุชาดา ใจดี</td>
                                            <td style="padding:10px 10px;margin:0px;border:0px;color:blue">13-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;border:0px;color:blue;text-align:right">10,000.00</td>                                            
                                            <td style="padding:10px 10px;margin:0px;border:0px;text-align:center"><a href="#"><img src="../Images/application-pdf.png" /> </a></td>
                                        </tr>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;">2</td>
                                            <td style="padding:10px 10px;margin:0px;"><a href="#" style="color:#e6400a">CL-30010-20161014001</a></td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">30010 / นางสุชาดา ใจดี</td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">14-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;text-align:right">8,500.00</td>
                                            <td style="padding:10px 10px;margin:0px;text-align:center"><a href="#"><img src="../Images/application-pdf.png" /> </a></td>
                                        </tr>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;">3</td>
                                            <td style="padding:10px 10px;margin:0px;"><a href="#" style="color:#e6400a">CL-30010-20161015001</a></td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">30010 / นางสุชาดา ใจดี</td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">15-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;text-align:right">1,200.00</td>
                                            <td style="padding:10px 10px;margin:0px;text-align:center"><a href="#"><img src="../Images/application-pdf.png" /> </a></td>
                                        </tr>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;">4</td>
                                            <td style="padding:10px 10px;margin:0px;"><a href="#" style="color:#e6400a">CL-30010-20161016001</a></td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">30010 / นางสุชาดา ใจดี</td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">16-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;text-align:right">9,800.00</td>
                                            <td style="padding:10px 10px;margin:0px;text-align:center"><a href="#"><img src="../Images/application-pdf.png" /> </a></td>
                                        </tr>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;">5</td>
                                            <td style="padding:10px 10px;margin:0px;"><a href="#" style="color:#e6400a">CL-30010-20161017001</a></td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">30010 / นางสุชาดา ใจดี</td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">17-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;text-align:right">3,500.00</td>
                                            <td style="padding:10px 10px;margin:0px;text-align:center"><a href="#"><img src="../Images/application-pdf.png" /> </a></td>
                                        </tr>
                                    </tbody>

                                </table>
                            </div>
                        </div>

                        <div class="panel-footer" style="height:55px">
                            <div class="clearfix"></div>
                                <span class="pull-left" style="margin-top:10px">
                                    
                                </span>
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
    </div>
</asp:Content>

