<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Main.master" AutoEventWireup="true" CodeFile="OrderAndDeliveryCycleList.aspx.cs"
    Inherits="Views_OrderAndDeliveryCycleList" uiculture="th" culture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        var myApp;
        myApp = myApp || (function () {
            //var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 80%;"></div></div></div></div>');
            var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content" style="width: 100%;"><div class="modal-header"><h5>กรุณารอสักครู่...</h5></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar progress-red" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
            return {
                showPleaseWait: function () {
                    pleaseWaitDiv.modal();
                },
                hidePleaseWait: function () {
                    pleaseWaitDiv.modal('hide');
                },

            };
        })();
    </script>

    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" Visible="true" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">กำหนดรอบสั่งรอบส่ง</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">ชื่อรอบสั่งรอบส่ง:</label>
                                    <div class="col-md-4">
                                        <asp:dropdownlist id="ddlOrderCycle" runat="server" CssClass="form-control"
                                            datatextfield="Order_Cycle_Name" datavaluefield="Order_Cycle_Name">
                                        </asp:dropdownlist>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSearchOK" class="btn btn-primary" runat="server" Text="ค้นหา" onclick="btnSearchOK_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                        <asp:Button ID="btnSearchCancel" class="btn btn-default" runat="server" Text="ยกเลิก" onclick="btnSearchCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Button ID="ButtonCreateNew" class="btn btn-primary" runat="server" Text="สร้าง" OnClick="ButtonCreateNew_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                        <asp:Button ID="btnAssignment" class="btn btn-primary" runat="server" Text="กำหนดรอบสั่งรอบส่ง" OnClick="btnAssignment_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlNoRec" runat="server" Visible="false">
                                            <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; padding-bottom: 20px; background: #fbfafa">
                                                <center>ไม่พบข้อมูล</center>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="GridViewCycle"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames=""
                                            ShowFooter="false"
                                            showheaderwhenempty="true"
                                            OnRowCancelingEdit="GridViewCycle_RowCancelingEdit"
                                            OnRowDataBound="GridViewCycle_RowDataBound"
                                            OnRowDeleting="GridViewCycle_RowDeleting"
                                            OnRowEditing="GridViewCycle_RowEditing"
                                            OnRowUpdating="GridViewCycle_RowUpdating"
                                            OnRowCommand="GridViewCycle_RowCommand"
                                            CellPadding="0"
                                            ForeColor="#333333"
                                            CssClass="table table-striped table-bordered table-condensed"
                                            AllowPaging="false" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center" HeaderStyle-Width="80px" Visible ="true">
                                                    <ItemTemplate>
                                                        <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                            <asp:LinkButton ID="lnkBDelete" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                        </span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" text="บันทึก"></asp:LinkButton>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center" HeaderStyle-Width="80px">
                                                 <%--<asp:TemplateField HeaderText=""  itemstyle-horizontalalign="Center">--%>   
                                                 <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBitemEdit" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                         <%--<asp:LinkButton ID="lnkBEditUpdate" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkBEditCancel" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;">
                                                            <%--<asp:LinkButton ID="lnkBFooterAddNew" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew" text="บันทึก"></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkBFooterAddNewCancel" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-default" CommandName="Cancel" text="ยกเลิก"></asp:LinkButton>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="ลำดับ" HeaderStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblOrder" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <div style="margin-bottom:20px">
                                                            <asp:button id="btnNewValue" runat="server" text="สร้าง" CssClass="btn btn-mini btn-primary" visible="false" onclick="btnNewValue_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                                        </div>
                                                       <asp:GridView ID="grdNewValue"
                                                            runat="server"
                                                            AutoGenerateColumns="False"
                                                            ShowFooter="false" showheaderwhenempty="false"
                                                            OnRowCancelingEdit="grdNewValue_RowCancelingEdit"
                                                            OnRowDataBound="grdNewValue_RowDataBound"
                                                            OnRowDeleting="grdNewValue_RowDeleting"
                                                            OnRowEditing="grdNewValue_RowEditing"
                                                            OnRowUpdating="grdNewValue_RowUpdating"
                                                            OnRowCommand="grdNewValue_RowCommand"
                                                            CellPadding="0"
                                                            visible="false"
                                                            ForeColor="#333333"
                                                           CssClass="table-bordered table-condensed">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center" Visible="true">
                                                                    <ItemTemplate>
                                                                        <span onclick="return confirm('คุณต้องการลบข้อมูล?')?myApp.showPleaseWait(): false;">
                                                                            <asp:LinkButton ID="lnkBDeleteValue" runat="Server" CssClass="btn btn-mini btn-danger" Text="ลบ" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="lnkBEditUpdateValue" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div style="text-align: center;">
                                                                            <asp:LinkButton ID="lnkBFooterAddNewValue" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNewValue" text="บันทึก"></asp:LinkButton>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" ShowHeader="False" itemstyle-horizontalalign="Center">
                                                                <%--<asp:TemplateField HeaderText=""  itemstyle-horizontalalign="Center">--%>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBitemEditValue" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-warning" Text="แก้ไข"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <%--<asp:LinkButton ID="lnkBEditUpdateValue" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandName="Update" Text="บันทึก"></asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="lnkBEditCancelValue" runat="server" CausesValidation="False" CssClass="btn btn-mini btn-default" CommandName="Cancel" Text="ยกเลิก"></asp:LinkButton>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div style="text-align: center;">
                                                                             <%--<asp:LinkButton ID="lnkBFooterAddNewValue" OnClientClick="myApp.showPleaseWait(); return true;" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-primary" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNewValue" text="บันทึก"></asp:LinkButton>--%>
                                                                            <asp:LinkButton ID="lnkBFooterAddNewCancelValue" runat="server" CausesValidation="True" CssClass="btn btn-mini btn-default" CommandName="Cancel" text="ยกเลิก"></asp:LinkButton>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="รอบการสั่ง(วัน)">
                                                                    <EditItemTemplate>
                                                                        <asp:dropdownlist id="ddlEditOrder_Cycle_Date" Text='<%# Eval("Order_Cycle_Date")%>' runat="server" style="border-left: 4px solid #ed1c24;">
                                                                            <asp:ListItem Text="อาทิตย์" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="จันทร์" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="อังคาร" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="พุธ" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="พฤหัสบดี" Value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="ศุกร์" Value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="เสาร์" Value="7"></asp:ListItem>
                                                                        </asp:dropdownlist>
                                                                    </EditItemTemplate>
                                                                    <footertemplate>
                                                                        <asp:dropdownlist id="ddlFooterOrder_Cycle_Date" runat="server" style="border-left: 4px solid #ed1c24;">
                                                                            <asp:ListItem Text="อาทิตย์" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="จันทร์" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="อังคาร" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="พุธ" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="พฤหัสบดี" Value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="ศุกร์" Value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="เสาร์" Value="7"></asp:ListItem>
                                                                        </asp:dropdownlist>
                                                                    </footertemplate>
                                                                    <ItemTemplate>
                                                                        <asp:label id="txtItemOrder_Cycle_Date" runat="server" Text='<%# Eval("Order_Cycle_Date_Name")%>'></asp:label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="รอบการสั่ง(เวลา)">
                                                                    <EditItemTemplate>
                                                                        <asp:dropdownlist id="ddlEditOrder_Cycle_Hour" runat="server"  Text='<%# Eval("Order_Cycle_Hour")%>' style="border-left: 4px solid #ed1c24;">
                                                                            <asp:ListItem>00</asp:ListItem>
                                                                            <asp:ListItem>01</asp:ListItem>
                                                                            <asp:ListItem>02</asp:ListItem>
                                                                            <asp:ListItem>03</asp:ListItem>
                                                                            <asp:ListItem>04</asp:ListItem>
                                                                            <asp:ListItem>05</asp:ListItem>
                                                                            <asp:ListItem>06</asp:ListItem>
                                                                            <asp:ListItem>07</asp:ListItem>
                                                                            <asp:ListItem>08</asp:ListItem>
                                                                            <asp:ListItem>09</asp:ListItem>
                                                                            <asp:ListItem>10</asp:ListItem>
                                                                            <asp:ListItem>11</asp:ListItem>
                                                                            <asp:ListItem>12</asp:ListItem>
                                                                            <asp:ListItem>13</asp:ListItem>
                                                                            <asp:ListItem>14</asp:ListItem>
                                                                            <asp:ListItem>15</asp:ListItem>
                                                                            <asp:ListItem>16</asp:ListItem>
                                                                            <asp:ListItem>17</asp:ListItem>
                                                                            <asp:ListItem>18</asp:ListItem>
                                                                            <asp:ListItem>19</asp:ListItem>
                                                                            <asp:ListItem>20</asp:ListItem>
                                                                            <asp:ListItem>21</asp:ListItem>
                                                                            <asp:ListItem>22</asp:ListItem>
                                                                            <asp:ListItem>23</asp:ListItem>
                                                                        </asp:dropdownlist>:
                                                                 <asp:dropdownlist id="ddlEditOrder_Cycle_Minute" runat="server"  Text='<%# Eval("Order_Cycle_Minute")%>' style="border-left: 4px solid #ed1c24;">
                                                                     <asp:ListItem>00</asp:ListItem>
                                                                     <asp:ListItem>01</asp:ListItem>
                                                                     <asp:ListItem>02</asp:ListItem>
                                                                     <asp:ListItem>03</asp:ListItem>
                                                                     <asp:ListItem>04</asp:ListItem>
                                                                     <asp:ListItem>05</asp:ListItem>
                                                                     <asp:ListItem>06</asp:ListItem>
                                                                     <asp:ListItem>07</asp:ListItem>
                                                                     <asp:ListItem>08</asp:ListItem>
                                                                     <asp:ListItem>09</asp:ListItem>
                                                                     <asp:ListItem>10</asp:ListItem>
                                                                     <asp:ListItem>11</asp:ListItem>
                                                                     <asp:ListItem>12</asp:ListItem>
                                                                     <asp:ListItem>13</asp:ListItem>
                                                                     <asp:ListItem>14</asp:ListItem>
                                                                     <asp:ListItem>15</asp:ListItem>
                                                                     <asp:ListItem>16</asp:ListItem>
                                                                     <asp:ListItem>17</asp:ListItem>
                                                                     <asp:ListItem>18</asp:ListItem>
                                                                     <asp:ListItem>19</asp:ListItem>
                                                                     <asp:ListItem>20</asp:ListItem>
                                                                     <asp:ListItem>21</asp:ListItem>
                                                                     <asp:ListItem>22</asp:ListItem>
                                                                     <asp:ListItem>23</asp:ListItem>
                                                                     <asp:ListItem>24</asp:ListItem>
                                                                     <asp:ListItem>25</asp:ListItem>
                                                                     <asp:ListItem>26</asp:ListItem>
                                                                     <asp:ListItem>27</asp:ListItem>
                                                                     <asp:ListItem>28</asp:ListItem>
                                                                     <asp:ListItem>29</asp:ListItem>
                                                                     <asp:ListItem>30</asp:ListItem>
                                                                     <asp:ListItem>31</asp:ListItem>
                                                                     <asp:ListItem>32</asp:ListItem>
                                                                     <asp:ListItem>33</asp:ListItem>
                                                                     <asp:ListItem>34</asp:ListItem>
                                                                     <asp:ListItem>35</asp:ListItem>
                                                                     <asp:ListItem>36</asp:ListItem>
                                                                     <asp:ListItem>37</asp:ListItem>
                                                                     <asp:ListItem>38</asp:ListItem>
                                                                     <asp:ListItem>39</asp:ListItem>
                                                                     <asp:ListItem>40</asp:ListItem>
                                                                     <asp:ListItem>41</asp:ListItem>
                                                                     <asp:ListItem>42</asp:ListItem>
                                                                     <asp:ListItem>43</asp:ListItem>
                                                                     <asp:ListItem>44</asp:ListItem>
                                                                     <asp:ListItem>45</asp:ListItem>
                                                                     <asp:ListItem>46</asp:ListItem>
                                                                     <asp:ListItem>47</asp:ListItem>
                                                                     <asp:ListItem>48</asp:ListItem>
                                                                     <asp:ListItem>49</asp:ListItem>
                                                                     <asp:ListItem>50</asp:ListItem>
                                                                     <asp:ListItem>51</asp:ListItem>
                                                                     <asp:ListItem>52</asp:ListItem>
                                                                     <asp:ListItem>53</asp:ListItem>
                                                                     <asp:ListItem>54</asp:ListItem>
                                                                     <asp:ListItem>55</asp:ListItem>
                                                                     <asp:ListItem>56</asp:ListItem>
                                                                     <asp:ListItem>57</asp:ListItem>
                                                                     <asp:ListItem>58</asp:ListItem>
                                                                     <asp:ListItem>59</asp:ListItem>
                                                                 </asp:dropdownlist>
                                                                    </EditItemTemplate>
                                                                    <footertemplate>
                                                                        <asp:dropdownlist id="ddlFooterOrder_Cycle_Hour" runat="server" style="border-left: 4px solid #ed1c24;">
                                                                            <asp:ListItem>00</asp:ListItem>
                                                                            <asp:ListItem>01</asp:ListItem>
                                                                            <asp:ListItem>02</asp:ListItem>
                                                                            <asp:ListItem>03</asp:ListItem>
                                                                            <asp:ListItem>04</asp:ListItem>
                                                                            <asp:ListItem>05</asp:ListItem>
                                                                            <asp:ListItem>06</asp:ListItem>
                                                                            <asp:ListItem>07</asp:ListItem>
                                                                            <asp:ListItem>08</asp:ListItem>
                                                                            <asp:ListItem>09</asp:ListItem>
                                                                            <asp:ListItem>10</asp:ListItem>
                                                                            <asp:ListItem>11</asp:ListItem>
                                                                            <asp:ListItem>12</asp:ListItem>
                                                                            <asp:ListItem>13</asp:ListItem>
                                                                            <asp:ListItem>14</asp:ListItem>
                                                                            <asp:ListItem>15</asp:ListItem>
                                                                            <asp:ListItem>16</asp:ListItem>
                                                                            <asp:ListItem>17</asp:ListItem>
                                                                            <asp:ListItem>18</asp:ListItem>
                                                                            <asp:ListItem>19</asp:ListItem>
                                                                            <asp:ListItem>20</asp:ListItem>
                                                                            <asp:ListItem>21</asp:ListItem>
                                                                            <asp:ListItem>22</asp:ListItem>
                                                                            <asp:ListItem>23</asp:ListItem>
                                                                        </asp:dropdownlist>:
                                                                 <asp:dropdownlist id="ddlFooterOrder_Cycle_Minute" runat="server" style="border-left: 4px solid #ed1c24;">
                                                                     <asp:ListItem>00</asp:ListItem>
                                                                     <asp:ListItem>01</asp:ListItem>
                                                                     <asp:ListItem>02</asp:ListItem>
                                                                     <asp:ListItem>03</asp:ListItem>
                                                                     <asp:ListItem>04</asp:ListItem>
                                                                     <asp:ListItem>05</asp:ListItem>
                                                                     <asp:ListItem>06</asp:ListItem>
                                                                     <asp:ListItem>07</asp:ListItem>
                                                                     <asp:ListItem>08</asp:ListItem>
                                                                     <asp:ListItem>09</asp:ListItem>
                                                                     <asp:ListItem>10</asp:ListItem>
                                                                     <asp:ListItem>11</asp:ListItem>
                                                                     <asp:ListItem>12</asp:ListItem>
                                                                     <asp:ListItem>13</asp:ListItem>
                                                                     <asp:ListItem>14</asp:ListItem>
                                                                     <asp:ListItem>15</asp:ListItem>
                                                                     <asp:ListItem>16</asp:ListItem>
                                                                     <asp:ListItem>17</asp:ListItem>
                                                                     <asp:ListItem>18</asp:ListItem>
                                                                     <asp:ListItem>19</asp:ListItem>
                                                                     <asp:ListItem>20</asp:ListItem>
                                                                     <asp:ListItem>21</asp:ListItem>
                                                                     <asp:ListItem>22</asp:ListItem>
                                                                     <asp:ListItem>23</asp:ListItem>
                                                                     <asp:ListItem>24</asp:ListItem>
                                                                     <asp:ListItem>25</asp:ListItem>
                                                                     <asp:ListItem>26</asp:ListItem>
                                                                     <asp:ListItem>27</asp:ListItem>
                                                                     <asp:ListItem>28</asp:ListItem>
                                                                     <asp:ListItem>29</asp:ListItem>
                                                                     <asp:ListItem>30</asp:ListItem>
                                                                     <asp:ListItem>31</asp:ListItem>
                                                                     <asp:ListItem>32</asp:ListItem>
                                                                     <asp:ListItem>33</asp:ListItem>
                                                                     <asp:ListItem>34</asp:ListItem>
                                                                     <asp:ListItem>35</asp:ListItem>
                                                                     <asp:ListItem>36</asp:ListItem>
                                                                     <asp:ListItem>37</asp:ListItem>
                                                                     <asp:ListItem>38</asp:ListItem>
                                                                     <asp:ListItem>39</asp:ListItem>
                                                                     <asp:ListItem>40</asp:ListItem>
                                                                     <asp:ListItem>41</asp:ListItem>
                                                                     <asp:ListItem>42</asp:ListItem>
                                                                     <asp:ListItem>43</asp:ListItem>
                                                                     <asp:ListItem>44</asp:ListItem>
                                                                     <asp:ListItem>45</asp:ListItem>
                                                                     <asp:ListItem>46</asp:ListItem>
                                                                     <asp:ListItem>47</asp:ListItem>
                                                                     <asp:ListItem>48</asp:ListItem>
                                                                     <asp:ListItem>49</asp:ListItem>
                                                                     <asp:ListItem>50</asp:ListItem>
                                                                     <asp:ListItem>51</asp:ListItem>
                                                                     <asp:ListItem>52</asp:ListItem>
                                                                     <asp:ListItem>53</asp:ListItem>
                                                                     <asp:ListItem>54</asp:ListItem>
                                                                     <asp:ListItem>55</asp:ListItem>
                                                                     <asp:ListItem>56</asp:ListItem>
                                                                     <asp:ListItem>57</asp:ListItem>
                                                                     <asp:ListItem>58</asp:ListItem>
                                                                     <asp:ListItem>59</asp:ListItem>
                                                                 </asp:dropdownlist>
                                                                    </footertemplate>
                                                                    <ItemTemplate>
                                                                        <asp:label id="txtItemOrder_Cycle_Hour" runat="server" Text='<%# Eval("Order_Cycle_Hour")%>'></asp:label>:<asp:label id="txtItemOrder_Cycle_Minute" runat="server" Text='<%# Eval("Order_Cycle_Minute")%>'></asp:label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="รอบการส่ง">
                                                                    <EditItemTemplate>
                                                                        <asp:dropdownlist id="ddlEditDelivery_Cycle_Date" runat="server" Text='<%# Eval("Delivery_Cycle_Date")%>' style="border-left: 4px solid #ed1c24;">
                                                                            <asp:ListItem Text="อาทิตย์" value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="จันทร์" value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="อังคาร" value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="พุธ" value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="พฤหัสบดี" value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="ศุกร์" value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="เสาร์" value="7"></asp:ListItem>
                                                                        </asp:dropdownlist>
                                                                    </EditItemTemplate>
                                                                    <footertemplate>
                                                                        <asp:dropdownlist id="ddlFooterDelivery_Cycle_Date" runat="server" style="border-left: 4px solid #ed1c24;">
                                                                            <asp:ListItem Text="อาทิตย์" value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="จันทร์" value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="อังคาร" value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="พุธ" value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="พฤหัสบดี" value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="ศุกร์" value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="เสาร์" value="7"></asp:ListItem>
                                                                        </asp:dropdownlist>
                                                                    </footertemplate>
                                                                    <ItemTemplate>
                                                                        <asp:label id="txtItemDelivery_Cycle_Date" runat="server" Text='<%# Eval("Delivery_Cycle_Date_Name")%>'></asp:label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Route การส่ง">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox id="txtEditRoute" runat="server" CssClass="form-control" Text='<%# Eval("Route")%>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label id="lblItemRoute" runat="server" Text='<%# Eval("Route")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtFooterRoute" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ID" visible="false">

                                                                    <ItemTemplate>
                                                                        <asp:Label id="lblOrderAndDeliveryCycleValue_ID" runat="server" Text='<%# Eval("OrderAndDeliveryCycleValue_ID")%>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblOrder_Cycle_ID" runat="server" Text='<%# Eval("Order_Cycle_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อรอบสั่งรอบส่ง">
                                                    <EditItemTemplate>
                                                        <asp:TextBox id="txtEditOrder_Cycle_Name" runat="server" Text='<%# Eval("Order_Cycle_Name")%>' style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblOrder_Cycle_Name" runat="server" Text='<%# Eval("Order_Cycle_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewOrder_Cycle_Name" runat="server" style="border-left: 4px solid #ed1c24;" CssClass="form-control"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#" ShowHeader="False" itemstyle-horizontalalign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkB_SetOrder_Cycle_Name" runat="server" CssClass="btn btn-mini btn-primary" Text="แก้ไขรอบสั่งรอบส่ง"
                                                            CommandArgument='<%# Container.DataItemIndex %>'
                                                            CommandName="EditCycle"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <itemstyle Wrap="true" />
                                                </asp:TemplateField>
                                            </Columns>

                                            <PagerTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 70%">
                                                        <asp:Label ID="MessageLabel"
                                                            ForeColor="Blue"
                                                            Text="เลือกหน้า :"
                                                            runat="server" />
                                                        <asp:DropDownList ID="PageDropDownList"
                                                            AutoPostBack="true"
                                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                                            onchange="myApp.showPleaseWait();"
                                                            runat="server" />
                                                    </td>

                                                    <td style="width: 70%; text-align: right">
                                                        <asp:Label ID="CurrentPageLabel"
                                                            ForeColor="Black"
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </PagerTemplate>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlForm" Visible="false" runat="server">
                <div class="container-full-content">
                    <div class="row">
                        <div class="col-md-12 top-bar-content-none" style="height: 45px">
                            <span class="title" style="font-size: 18px">กำหนดรอบสั่งรอบส่ง</span>&nbsp;&nbsp;
                            <span class="glyphicon glyphicon-cog" style="font-size: 18px"></span>
                        </div>
                        <div class="col-md-12 bar-content" style="height: auto; padding-top: 20px; background: #fbfafa">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">รอบสั่งรอบส่งปัจจุบัน:</label>
                                    <div class="col-md-3">
                                        <asp:dropdownlist id="ddlCurrent" runat="server" CssClass="form-control"
                                            datatextfield="Order_Cycle_Name" datavaluefield="Order_Cycle_ID" autopostback="true"
                                            onselectedindexchanged="ddlCurrent_SelectedIndexChanged">
                                        </asp:dropdownlist>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="col-md-2 control-label">รอบสั่งรอบส่งใหม่:</label>
                                    <div class="col-md-3">
                                        <asp:dropdownlist id="ddlNew" runat="server" CssClass="form-control"
                                            datatextfield="Order_Cycle_Name" datavaluefield="Order_Cycle_ID" autopostback="true"
                                            onselectedindexchanged="ddlNew_SelectedIndexChanged">
                                        </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="form-group">


                                    <div class="col-md-5">
                                        <asp:ListBox ID="lstBPrimary" runat="server" CssClass="table table-striped table-bordered table-condensed"
                                            selectionmode="Single"
                                            Height="200px"
                                            datatextfield="VALUE"
                                            datavaluefield="KEY"></asp:ListBox>
                                        <%--<asp:CheckBoxList ID="lstBPrimary" runat="server"
                                            CssClass="table table-striped table-bordered table-condensed"
                                            datatextfield="AgentName"
                                            datavaluefield="Order_Cycle_ID" >
                                        </asp:CheckBoxList>--%>
                                    </div>
                                    <div class="col-md-2">
                                        <div style="text-align: center">
                                            <asp:button id="btnAddAll" runat="server" font-bold="true" text=">>" CssClass="btn btn-default" Width="54px" onclick="btnAddAll_Click" />
                                            <br />
                                            <asp:button id="btnAddOne" runat="server" font-bold="true" text=">" CssClass="btn btn-default" Width="54px" onclick="btnAddOne_Click" />
                                            <br />
                                            <asp:button id="btnRemoveOne" runat="server" font-bold="true" text="<" CssClass="btn btn-default" Width="54px" onclick="btnRemoveOne_Click" />
                                            <br />
                                            <asp:button id="btnRemoveAll" runat="server" font-bold="true" text="<<" CssClass="btn btn-default" Width="54px" onclick="btnRemoveAll_Click" />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:ListBox ID="lsbBSecondary" runat="server" CssClass="table table-striped table-bordered table-condensed"
                                            Height="200px" SelectionMode="Single"
                                            datatextfield="VALUE"
                                            datavaluefield="KEY"></asp:ListBox>

                                        <%-- <asp:CheckBoxList ID="lsbBSecondary" runat="server"
                                            CssClass="table table-striped table-bordered table-condensed"
                                            datatextfield="AgentName"
                                            datavaluefield="Order_Cycle_ID" >
                                        </asp:CheckBoxList>--%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                        <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                        <asp:Button ID="btnShowGrid" class="btn btn-default" runat="server" Text="กลับไปหน้าค้นหา" OnClick="btnShowGrid_Click" OnClientClick="myApp.showPleaseWait(); return true;"/>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

