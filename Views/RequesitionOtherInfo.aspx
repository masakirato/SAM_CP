<%@ Page Title="Requesition" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="RequesitionOtherInfo.aspx.cs" Inherits="Views_RequesitionOtherInfo" %>

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
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12">
                    <div class="col-md-5 text-right" style="line-height: 40px;font-size:18px;">
                        เลขที่ใบเบิก :
                    </div>
                    <div class="col-md-7" style="line-height: 40px;font-size:18px">
                        <span style="color: blue">RO-30010-20161013001</span>
                    </div>                    
                </div>
                <div class="col-md-12">
                    <br />        
                </div>
                <div class="col-md-12 text-center" style="font-size:14px">
                    <a href="#" style="color:#ed1c24">พิมพ์รายงานใบเบิกอื่นๆ</a> &nbsp;|&nbsp;
                    <a href="RequesitionOtherList.aspx" style="color:#ed1c24">กลับหน้ารายการเบิกอื่นๆ</a>
                </div>
                <div class="col-md-12">
                    <br />        
                </div>

            </div>
        </div>
    </div>
    <span id="date" style="color: blue;visibility:hidden"></span>
    <div class="col-md-12" style="height:400px">
    </div>
</asp:Content>

