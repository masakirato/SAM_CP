<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="CreatePriceGroup.aspx.cs" Inherits="Views_CreatePriceGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div class="container-full-content">
        <div class="row">
            <div class="col-md-12 top-bar-content-none" style="height: 45px">
                <span class="title" style="font-size: 18px">Agent</span>&nbsp;&nbsp;<span class="glyphicon glyphicon-text-color" style="font-size: 18px"></span><!--<img src="Images/help-contents.png" />-->
            </div>


            <div class="col-md-12 bar-content" style="height: 1150px; padding-top: 20px">
                <div class="form-horizontal">


                    <div class="form-group">
                        <label class="col-md-2 control-label">รหัสกลุ่มราคา:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtPrice_Group_ID" runat="server" CssClass="form-control"></asp:textbox>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">ชื่อกลุ่มราคา:</label>
                        <div class="col-md-4">
                            <asp:textbox id="txtPrice_Group_Name" runat="server" CssClass="form-control"></asp:textbox>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

