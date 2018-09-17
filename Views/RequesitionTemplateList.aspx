<%@ Page Title="Requesition Template" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="RequesitionTemplateList.aspx.cs" Inherits="Views_RequesitionTemplateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height:45px">
                <span class="title" style="font-size:18px">รูปแบบใบเบิกประจำตัว</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-shopping-cart" style="font-size:18px"></span>
            </div>
            <div class="col-md-12 bar-content" style="height: 660px;padding-top:20px">
                <div class="col-md-12">
                   <span style="font-size:16px">ค้นหาข้อมูลใบเบิกประจำตัว</span> 
                </div>
                <div class="col-md-12" style="margin-top: 20px">
                    <div class="col-md-1" style="width:150px">
                        รูปแบบใบเบิก
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                          <span class="input-group-addon" id="basic-addon1"><span class="glyphicon glyphicon-search"></span></span>
                          <input type="text" class="form-control" placeholder="กรอกรูปแบบใบเบิก" aria-describedby="basic-addon1">
                        </div>
                    </div>
                    <div class="col-md-1" style="width:100px">
                        สถานะ
                    </div>
                    <div class="col-md-2">
                       <asp:DropDownList class="form-control" runat="server" Width="150">
                           <asp:ListItem Text="">ใช้งาน</asp:ListItem>
                           <asp:ListItem Text="">ยกเลิก</asp:ListItem>
                       </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                       <asp:Button runat="server" Text="ค้นหา" CssClass="btn btn-danger"/>&nbsp;<asp:Button runat="server" Text="ยกเลิก" CssClass="btn btn-danger"/>
                    </div>
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12" style="padding-top: 10px">
                    <asp:Button runat="server" Text="สร้างรูปแบบใบเบิก" CssClass="btn btn-danger" OnClick="Unnamed4_Click"/>
                   
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
                                        <th style="font-weight:100;border-bottom:2px solid #808080">รูปแบบใบเบิก</th>
                                        <th style="width:150px;font-weight:100;border-bottom:2px solid #808080">วันที่สร้าง</th>
                                        <th style="width:150px;font-weight:100;border-bottom:2px solid #808080">สถานะ</th>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;border:0px">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;border:0px">1</td>
                                            <td style="padding:10px 10px;margin:0px;border:0px"><a href="#" style="color:#e6400a">30010 : นางสุชาดา ใจดี - วันจันทร์</a></td>
                                            <td style="padding:10px 10px;margin:0px;border:0px;color:blue">13-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;">ใช้งาน</td>
                                        </tr>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;">2</td>
                                            <td style="padding:10px 10px;margin:0px;"><a href="#" style="color:#e6400a">30010 : นางสุชาดา ใจดี - วันอังคาร</a></td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">13-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;">ใช้งาน</td>
                                        </tr>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;">3</td>
                                            <td style="padding:10px 10px;margin:0px;"><a href="#" style="color:#e6400a">30010 : นางสุชาดา ใจดี - วันพุธ</a></td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">13-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;">ใช้งาน</td>
                                        </tr>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;">4</td>
                                            <td style="padding:10px 10px;margin:0px;"><a href="#" style="color:#e6400a">30010 : นางสุชาดา ใจดี - วันพฤหัสบดี</a></td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">13-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;">ใช้งาน</td>
                                        </tr>
                                        <tr>
                                            <td style="padding:10px 10px;margin:0px;">
                                                <input type="checkbox" class="checkthis" />
                                            </td>
                                            <td style="padding:10px 10px;margin:0px;">5</td>
                                            <td style="padding:10px 10px;margin:0px;"><a href="#" style="color:#e6400a">30010 : นางสุชาดา ใจดี - วันศุกร์</a></td>
                                            <td style="padding:10px 10px;margin:0px;color:blue">13-ต.ค.-2016</td>                                            
                                            <td style="padding:10px 10px;margin:0px;color:blue;">ใช้งาน</td>
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

