<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OKMessageBox.ascx.cs" Inherits="Library_OKMessageBox" %>

<ajaxToolkit:ModalPopupExtender ID="MPE" runat="server" BackgroundCssClass="modalBackground"  
    TargetControlID="pnlPopup" PopupControlID="pnlPopup">
</ajaxToolkit:ModalPopupExtender>

<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;"
    DefaultButton="btnOk">
    <table width="100%">
        <tr class="topHandle">
            <td colspan="2" align="left" runat="server" id="tdCaption">
                &nbsp; <asp:Label ID="lblCaption" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 60px" valign="middle" align="center">
                <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Images/Info-48x48.png" />
            </td>
            <td valign="middle" align="left">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="middle" align="center">
                <asp:Button ID="btnOk" runat="server" Text="   Ok   " OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>

<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
</script>

 
