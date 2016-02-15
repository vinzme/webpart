<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CostofSales.ascx.vb" Inherits="CostofSales" %>

<atlas:ScriptManagerProxy ID="sl8" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :" Width="78px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Text="0" Width="71px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :" Width="51px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" Text="Label" BackColor="White" ForeColor="White" Visible="False"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label6" runat="server" Text="Label" BackColor="White" ForeColor="White" Visible="False"></asp:Label></td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataSourceID="ObjectDataSource1" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Black"
                GridLines="Vertical" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" ShowFooter="True">
                <FooterStyle BackColor="#CCCC99" />
                <Columns>
                    <asp:BoundField DataField="analysis3" HeaderText="Group" SortExpression="analysis3" />
                    <asp:BoundField DataField="expense_code" HeaderText="Expense Code" SortExpression="expense_code" />
                    <asp:BoundField DataField="period" HeaderText="Period" SortExpression="period" />
                    <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True"
                        SortExpression="description" />
                    <asp:BoundField DataField="amt1" HeaderText="Debit" SortExpression="amt1" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="amt2" HeaderText="Credit" SortExpression="amt2" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="Honeydew" Height="20px" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataCostofSales" TypeName="SOCostofSalesTableAdapters.prtrm_aTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="Order1" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="Order2" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label6" Name="Order3" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataCostofSalesDam" TypeName="SOCostofSalesDamTableAdapters.prtrm_aTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="Order1" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="Order2" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label6" Name="Order3" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataCostofSalesJed" TypeName="SOCostofSalesJedTableAdapters.prtrm_aTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="Order1" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="Order2" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label6" Name="Order3" PropertyName="Text" Type="String" />
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
