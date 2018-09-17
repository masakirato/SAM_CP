<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MockupCrystalReportPage.aspx.cs" 
    Inherits="ViewsMockup_MockupCrystalReportPage" UICulture="th" Culture="th-TH" %>

<%@ Register Assembly="CrystalDecisions.web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px; overflow: auto;">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="Img1" Height="50px" ImageUrl="~/ViewsMockup/Images/PdfImage.jpg"
                            runat="server" OnClick="Img1_Click" />
                        <asp:ImageButton ID="img2" Height="50px" ImageUrl="~/ViewsMockup/Images/index.jpg"
                            runat="server" OnClick="img2_Click" />
                        <asp:ImageButton ID="img3" Height="50px" ImageUrl="~/ViewsMockup/Images/docx.png"
                            runat="server" OnClick="img3_Click" />
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" ToolPanelView="None" runat="server" AutoDataBind="true" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
