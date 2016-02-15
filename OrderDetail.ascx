<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OrderDetail.ascx.vb" Inherits="OrderDetails" %>

<atlas:ScriptManagerProxy ID="sl3" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label1" runat="server" Text="Order No. :" BackColor="White" Font-Bold="False" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue" Width="80px"></asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label2" runat="server" Text="000" Width="71px" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="White"></asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label3" runat="server" Text="Type :" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue" Width="62px"></asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Names="Verdana" Font-Size="X-Small" ForeColor="White" Width="173px" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" DataSourceID="ObjectDataSource1" Font-Names="Verdana" Font-Size="X-Small"
                ForeColor="Black" GridLines="None" ShowFooter="True" Width="100%">
                <FooterStyle BackColor="LightCoral" BorderStyle="None" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="order_line_no" HeaderText="Item No" SortExpression="order_line_no">
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <ControlStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="long_description" HeaderText="Description" SortExpression="long_description" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="order_qty" HeaderText="Qty" SortExpression="order_qty">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="net_price" HeaderText="Unit Price" SortExpression="net_price">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="val" HeaderText="Total Value" SortExpression="val">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="transaction_anals3" HeaderText="Product Group" SortExpression="transaction_anals3">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Black"
                    HorizontalAlign="Right" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataSODetail" TypeName="OrderDetailTableAdapters.opdetmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataSODetailDam" TypeName="OrderDetailDamTableAdapters.opdetmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataSODetailJed" TypeName="OrderDetailJedTableAdapters.opdetmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
</table>
</asp:Panel>



