<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Margin.ascx.vb" Inherits="Margin" %>

<atlas:ScriptManagerProxy ID="sl10" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :" Width="77px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Text="0" Width="71px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :" Width="49px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" Text="0" Visible="False" Width="61px"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataSourceID="ObjectDataSource1" Font-Names="Verdana" Font-Size="X-Small" ForeColor="#333333"
                GridLines="None" Width="100%" ShowFooter="True">
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="analysis3" HeaderText="Product Group" SortExpression="analysis3" />
                    <asp:BoundField DataField="sales" HeaderText="Sales" ReadOnly="True" SortExpression="sales" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wipdebit" HeaderText="Wip-Debit" ReadOnly="True" SortExpression="wipdebit" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wipcredit" HeaderText="Wip-Credit" ReadOnly="True" SortExpression="wipcredit" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="costofsales" HeaderText="Cost of Sales" ReadOnly="True"
                        SortExpression="costofsales" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="margin" HeaderText="Margin" ReadOnly="True" SortExpression="margin" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataOrderMargin" TypeName="SOMarginTableAdapters.and_marginTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataOrderMarginDam" TypeName="SOMarginDamTableAdapters.and_marginTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataOrderMarginJed" TypeName="SOMarginJedTableAdapters.and_marginTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
        </td>
        <td style="width: 100px; text-align: center">
        </td>
        <td style="width: 100px; text-align: center">
        </td>
        <td style="width: 100px; text-align: center">
        </td>
    </tr>
    <tr>
        <td style="width: 100px; height: 27px;">
        </td>
        <td style="width: 100px; text-align: center; height: 27px;">
            <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="RoyalBlue"
                Text="Sales" Width="92px"></asp:Label></td>
        <td style="width: 100px; text-align: center; height: 27px;">
            <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="SaddleBrown"
                Text="Cost of Sales" Width="97px"></asp:Label></td>
        <td style="width: 100px; text-align: center; height: 27px;">
            <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="DarkGreen"
                Text="Margin" Width="63px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; text-align: right; height: 26px;">
            <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Gross :" Width="55px"></asp:Label></td>
        <td style="width: 100px; text-align: right; height: 26px;">
            <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Width="99px"></asp:Label></td>
        <td style="width: 100px; height: 26px;">
        </td>
        <td style="width: 100px; height: 26px;">
        </td>
    </tr>
    <tr>
        <td style="width: 100px; text-align: right; height: 24px;">
            <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Red"
                Text="Discount :" Width="48px"></asp:Label></td>
        <td style="width: 100px; text-align: right; height: 24px;">
            <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Width="99px"></asp:Label></td>
        <td style="width: 100px; height: 24px;">
        </td>
        <td style="width: 100px; height: 24px;">
        </td>
    </tr>
    <tr>
        <td style="width: 100px; height: 23px; background-color: palegreen; text-align: right">
            <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="DarkGreen"
                Text="Net :" Width="43px"></asp:Label></td>
        <td style="width: 100px; height: 23px; background-color: palegreen; text-align: right">
            <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Width="99px" Font-Bold="True"></asp:Label></td>
        <td style="width: 100px; height: 23px; background-color: palegreen; text-align: right">
            <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Width="99px" Font-Bold="True"></asp:Label></td>
        <td style="width: 100px; height: 23px; background-color: palegreen; text-align: right">
            <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Width="99px" Font-Bold="True"></asp:Label></td>
    </tr>
</table>
</asp:Panel>