<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Material.ascx.vb" Inherits="Material" %>

<atlas:ScriptManagerProxy ID="sl12" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Text="0" Width="71px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :" Width="50px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" Text="Label" Width="78px" ForeColor="White" Visible="False"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 50px">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                DataSourceID="ObjectDataSource1" Font-Names="Verdana" Font-Size="X-Small" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                <FooterStyle BackColor="Lavender" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="product" HeaderText="Item Part No." ReadOnly="True" SortExpression="product" />
                    <asp:BoundField DataField="order_qty" HeaderText="Ordered Qty" SortExpression="order_qty" />
                    <asp:BoundField DataField="packaging" HeaderText="Scheduled Date" SortExpression="packaging" />
                    <asp:BoundField DataField="transaction_anals1" HeaderText="Shipped Qty" SortExpression="transaction_anals1" />
                    <asp:BoundField DataField="transaction_anals2" HeaderText="Returned Qty" SortExpression="transaction_anals2" />
                    <asp:BoundField DataField="balance" HeaderText="Balance" ReadOnly="True" SortExpression="balance" />
                </Columns>
                <RowStyle ForeColor="#000066" BackColor="Honeydew" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="Purple" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </td>
    </tr>
</table>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataMaterialStat" TypeName="MaterialStatTableAdapters.opdetmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataMaterialStatDam" TypeName="MaterialStatDamTableAdapters.opdetmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataMaterialStatJed" TypeName="MaterialStatJedTableAdapters.opdetmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataMaterialStatLive" TypeName="MaterialStatusLiveTableAdapters.opdetmTableAdapter">
    <SelectParameters>
        <asp:ControlParameter ControlID="Label5" Name="OrderNo" PropertyName="Text" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Panel>