<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="ClearingInfo.aspx.cs" Inherits="Views_ClearingInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height:45px">
                <span class="title" style="font-size:18px">เคลียร์เงิน</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-piggy-bank" style="font-size:18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>
            <div class="col-md-12 bar-content" style="height: auto;padding-top:20px;background:#fbfafa">
                 <div class="col-md-12">
                    <div class="col-md-2" style="line-height: 40px">
                        รหัส CV
                    </div>
                    <div class="col-md-3" style="line-height: 40px;width:280px">
                        <span style="color: blue">301543</span>
                    </div>    
                     <div class="col-md-1" style="width: 170px; line-height: 40px">
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
                    <div class="col-md-1" style="width: 170px; line-height: 40px">
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
                        <span style="color: blue">เคลียร์วันที่ 04-04-2017</span>
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
                        เลขที่เอกสาร :
                    </div>
                    <div class="col-md-7" style="line-height: 40px;font-size:18px">
                        <span style="color: blue">CL-30010-20161013001</span>
                    </div>                    
                </div>
                <div class="col-md-12">
                    <br />        
                </div>
                <div class="col-md-12 text-center" style="font-size:14px">
                    <a href="#" style="color:#ed1c24">พิมพ์รายงานใบเคลียร์เงิน</a> &nbsp;|&nbsp;
                    <a href="ClearingList.aspx" style="color:#ed1c24">กลับหน้ารายการเคลียร์เงิน</a>
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

