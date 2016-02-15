<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OrderHeader.ascx.vb" Inherits="OrderHeader" %>


<atlas:ScriptManagerProxy ID="sl2" EnableScriptComponents="true" runat="server"/>
<table border="0" cellpadding="0" cellspacing="0" style="width: 485px;">
    <tr>
        <td style="width: 100px; border-top-width: 1px; border-left-width: 1px; border-left-color: darkgray; border-bottom-width: 1px; border-bottom-color: darkgray; border-top-color: darkgray; height: 16px; text-align: center; border-right-width: 1px; border-right-color: darkgray;">
            <asp:Label ID="Label1" runat="server" Text="Order No." Width="71px" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"></asp:Label></td>
        <td style="width: 100px; border-top-width: 1px; border-left-width: 1px; border-left-color: darkgray; border-bottom-width: 1px; border-bottom-color: darkgray; border-top-color: darkgray; height: 16px; text-align: center; border-right-width: 1px; border-right-color: darkgray;">
            <asp:Label ID="Label3" runat="server" Text="Type" Width="89px" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"></asp:Label></td>
        <td style="width: 100px; border-top-width: 1px; border-left-width: 1px; border-left-color: darkgray; border-bottom-width: 1px; border-bottom-color: darkgray; border-top-color: darkgray; height: 16px; text-align: center; border-right-width: 1px; border-right-color: darkgray;">
            <asp:Label ID="Label5" runat="server" Text="Cost Centre" Width="152px" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; height: 16px; border-top-width: 1px; border-left-width: 1px; border-left-color: darkgray; border-top-color: darkgray; border-bottom: darkgray 1px solid; text-align: center; border-right-width: 1px; border-right-color: darkgray;">
            <asp:Label ID="Label2" runat="server" Width="85px" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="White"></asp:Label></td>
        <td style="width: 100px; height: 16px; border-top-width: 1px; border-left-width: 1px; border-left-color: darkgray; border-top-color: darkgray; border-bottom: darkgray 1px solid; text-align: center; border-right-width: 1px; border-right-color: darkgray;">
            <asp:Label ID="Label4" runat="server" Width="148px" BackColor="PaleGreen" Font-Names="Verdana" Font-Size="X-Small" Font-Bold="True"></asp:Label></td>
        <td style="width: 100px; height: 16px; border-top-width: 1px; border-left-width: 1px; border-left-color: darkgray; border-top-color: darkgray; border-bottom: darkgray 1px solid; text-align: center; border-right-width: 1px; border-right-color: darkgray;">
            <asp:Label ID="Label6" runat="server" Width="253px" Font-Names="Verdana" Font-Size="X-Small" BackColor="SteelBlue" ForeColor="White"></asp:Label></td>
    </tr>
</table>
<table style="width: 485px; border-bottom: darkgray 1px solid;">
    <tr>
        <td colspan="2" style="text-align: left">
            <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue" Width="109px"></asp:Label><asp:Label ID="Label8" runat="server" BackColor="DarkGray"
                    Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                    Width="87px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue" Width="111px">Warranty Order:</asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label10" runat="server" BackColor="PapayaWhip" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="85px"></asp:Label></td>
    </tr>
</table>
<table style="width: 486px; background-color: mintcream;">
    <tr>
        <td colspan="3">
            <asp:Label ID="Label11" runat="server" Text="Project Name" Width="101px" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label12" runat="server" BackColor="Lavender" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue"
                Width="360px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 86px">
            <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"
                Text="Implementation" Width="118px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"
                Text="Site" Width="72px"></asp:Label></td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td style="border-bottom: darkgray 1px solid;">
            <asp:Label ID="Label14" runat="server" BackColor="Lavender" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="78px"></asp:Label></td>
        <td style="border-bottom: darkgray 1px solid;" colspan="2">
            <asp:Label ID="Label16" runat="server" BackColor="Lavender" Font-Names="Verdana"
                Font-Size="X-Small" Width="295px" ForeColor="MidnightBlue"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 86px">
            <asp:Label ID="Label17" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Red"
                Text="Customer : " Width="172px"></asp:Label></td>
        <td colspan="2">
            <asp:Label ID="Label18" runat="server" BackColor="Honeydew" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="Red" Width="296px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 86px">
            <asp:Label ID="Label19" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"
                Text="Address" Width="82px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label23" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"
                Text="Telephone No." Width="112px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label27" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Contact" Width="117px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 86px">
            <asp:Label ID="Label20" runat="server" BackColor="AliceBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="170px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label24" runat="server" BackColor="AliceBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="114px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label28" runat="server" BackColor="AliceBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="153px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 86px">
            <asp:Label ID="Label21" runat="server" BackColor="AliceBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="170px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label25" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"
                Text="Fax" Width="74px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label29" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Contact Tel#" Width="118px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 86px">
            <asp:Label ID="Label22" runat="server" BackColor="AliceBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="170px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label26" runat="server" BackColor="AliceBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="130px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label30" runat="server" BackColor="AliceBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="135px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 86px; border-bottom: lightgrey 1px solid;">
            <asp:Label ID="Label31" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"
                Text="Customer Reference :" Width="151px"></asp:Label></td>
        <td style="border-bottom: lightgrey 1px solid;" colspan="2">
            <asp:Label ID="Label32" runat="server" BackColor="LightGray" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="Blue" Width="291px"></asp:Label></td>
    </tr>
</table>
<table style="width: 483px; background-color: lightcyan;">
    <tr>
        <td style="width: 100px; text-align: left;">
            <asp:Label ID="Label33" runat="server" Font-Names="Verdana" Font-Size="X-Small" Text="Date Entered :" ForeColor="MidnightBlue" Width="130px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label34" runat="server" Font-Names="Verdana" Font-Size="X-Small"
                Width="87px" BackColor="#C0FFFF" ForeColor="MidnightBlue"></asp:Label></td>
        <td style="width: 100px; text-align: left;">
            <asp:Label ID="Label39" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Value :" Width="72px"></asp:Label></td>
        <td style="width: 100px; text-align: right;">
            <asp:Label ID="Label40" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Fuchsia" Width="99px" BackColor="#C0FFC0"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; text-align: left;">
            <asp:Label ID="Label35" runat="server" Font-Names="Verdana" Font-Size="X-Small" Text="Expected Delivery :"
                Width="132px" ForeColor="MidnightBlue"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label36" runat="server" Font-Names="Verdana" Font-Size="X-Small"
                Width="87px" BackColor="#C0FFFF" ForeColor="MidnightBlue"></asp:Label></td>
        <td style="width: 100px; text-align: left;">
            <asp:Label ID="Label41" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Discount :" Width="61px"></asp:Label></td>
        <td style="width: 100px; text-align: right;">
            <asp:Label ID="Label42" runat="server" Font-Names="Verdana" Font-Size="X-Small"
                Width="99px" BackColor="WhiteSmoke" ForeColor="Fuchsia"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; text-align: left; border-bottom-width: 1px; border-bottom-color: lightgrey;">
            <asp:Label ID="Label37" runat="server" Font-Names="Verdana" Font-Size="X-Small" Text="Completion Date :"
                Width="125px" ForeColor="MidnightBlue"></asp:Label></td>
        <td style="width: 100px; border-bottom-width: 1px; border-bottom-color: lightgrey;">
            <asp:Label ID="Label38" runat="server" Font-Names="Verdana" Font-Size="X-Small"
                Width="87px" BackColor="#C0FFFF" ForeColor="MidnightBlue"></asp:Label></td>
        <td style="width: 100px; border-bottom-width: 1px; border-bottom-color: lightgrey; text-align: left;">
            <asp:Label ID="Label43" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Expected Margin :" Width="121px"></asp:Label></td>
        <td style="width: 100px; border-bottom-width: 1px; border-bottom-color: lightgrey; text-align: right;">
            <asp:Label ID="Label44" runat="server" BackColor="PaleGreen" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="MidnightBlue" Width="34px"></asp:Label></td>
    </tr>
    <tr>
        <td style="border-bottom-width: 1px; border-bottom-color: lightgrey; width: 100px;
            text-align: left">
        </td>
        <td style="border-bottom-width: 1px; border-bottom-color: lightgrey; width: 100px">
        </td>
        <td style="border-bottom-width: 1px; border-bottom-color: lightgrey; width: 100px;
            text-align: right">
            <asp:Label ID="Label49" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Blue"
                Text="Rate :" Width="46px"></asp:Label></td>
        <td style="border-bottom-width: 1px; border-bottom-color: lightgrey; width: 100px;
            text-align: left">
            <asp:Label ID="Label50" runat="server" BackColor="DarkGreen" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="99px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; border-bottom: lightgrey 1px solid; height: 18px; text-align: right;">
            <asp:Label ID="Label45" runat="server" Font-Names="Verdana" Font-Size="X-Small" Text="Close Date :"
                Width="98px"></asp:Label></td>
        <td style="width: 100px; border-bottom: lightgrey 1px solid; height: 18px;">
            <asp:Label ID="Label46" runat="server" BackColor="Pink" Font-Names="Verdana" Font-Size="X-Small"
                ForeColor="MidnightBlue" Width="87px"></asp:Label></td>
        <td style="width: 100px; border-bottom: lightgrey 1px solid; height: 18px; text-align: right;">
            <asp:Label ID="Label47" runat="server" Font-Names="Verdana" Font-Size="X-Small" Text="Status :"
                Width="73px"></asp:Label></td>
        <td style="width: 100px; border-bottom: lightgrey 1px solid; height: 18px;">
            <asp:Label ID="Label48" runat="server" BackColor="Red" Font-Names="Verdana" Font-Size="X-Small"
                ForeColor="White" Width="77px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; text-align: right;">
            <asp:Label ID="Label51" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Navy"
                Text="Salesman 1 :" Width="93px"></asp:Label></td>
        <td style="text-align: left;" colspan="3">
            <asp:Label ID="Label53" runat="server" BackColor="CornflowerBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="Navy" Width="282px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; text-align: right;">
            <asp:Label ID="Label52" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Navy"
                Text="2 :" Width="91px"></asp:Label></td>
        <td style="text-align: left;" colspan="3">
            <asp:Label ID="Label54" runat="server" BackColor="CornflowerBlue" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="Navy" Width="282px"></asp:Label></td>
    </tr>
</table>
<table style="width: 480px; background-color: peachpuff">
    <tr>
        <td style="width: 100px; text-align: left">
            <asp:Label ID="Label55" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Internal Memo" Width="176px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; text-align: left">
            <asp:Label ID="Label56" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Red" Width="464px"></asp:Label></td>
    </tr>
</table>

