<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdvancePayment.ascx.vb" Inherits="AdvancePayment" %>

<atlas:ScriptManagerProxy ID="sl11" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :" Width="76px"></asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Text="0" Width="71px"></asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :" Width="46px"></asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" Text="0" Visible="False"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="ObjectDataSource1"
                Font-Names="Verdana" Font-Size="X-Small" ForeColor="Black" GridLines="Horizontal"
                Width="100%" ShowFooter="True">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="reference" HeaderText="Reference" SortExpression="reference" />
                    <asp:BoundField DataField="period" HeaderText="Period" SortExpression="period" />
                    <asp:BoundField DataField="analysis3" HeaderText="Prod Group" SortExpression="analysis3" />
                    <asp:BoundField DataField="sales" HeaderText="Sales" SortExpression="sales" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="advance" HeaderText="Advance Payment" SortExpression="advance" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="discount" HeaderText="Discount" SortExpression="discount" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="Honeydew" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataAdvancePayment"
                TypeName="SOAdvanceTableAdapters.and_advanceTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="reference" Type="String" />
                    <asp:Parameter Name="period" Type="String" />
                    <asp:Parameter Name="analysis3" Type="String" />
                    <asp:Parameter Name="sales" Type="Double" />
                    <asp:Parameter Name="advance" Type="Double" />
                    <asp:Parameter Name="discount" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataAdvancePaymentDam"
                TypeName="SOAdvanceDamTableAdapters.and_advanceTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="reference" Type="String" />
                    <asp:Parameter Name="period" Type="String" />
                    <asp:Parameter Name="analysis3" Type="String" />
                    <asp:Parameter Name="sales" Type="Double" />
                    <asp:Parameter Name="advance" Type="Double" />
                    <asp:Parameter Name="discount" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataAdvancePaymentJed"
                TypeName="SOAdvanceJedTableAdapters.and_advanceTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="reference" Type="String" />
                    <asp:Parameter Name="period" Type="String" />
                    <asp:Parameter Name="analysis3" Type="String" />
                    <asp:Parameter Name="sales" Type="Double" />
                    <asp:Parameter Name="advance" Type="Double" />
                    <asp:Parameter Name="discount" Type="Double" />
                </InsertParameters>
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


