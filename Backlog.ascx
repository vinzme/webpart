<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Backlog.ascx.vb" Inherits="BacklogToo" %>

<atlas:ScriptManagerProxy ID="sl9" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :" Width="76px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="71px">0</asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" Text="0" Visible="False" Width="95px"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84"
                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2"
                DataSourceID="ObjectDataSource1" Font-Names="Verdana" Font-Size="X-Small" Width="100%" GridLines="None" ShowFooter="True">
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <Columns>
                    <asp:BoundField DataField="product_group" HeaderText="Product Group" SortExpression="product_group" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="orders_booked" HeaderText="Orders Booked" SortExpression="orders_booked" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sales" HeaderText="Sales" SortExpression="sales" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="backlog" HeaderText="Backlog" SortExpression="backlog" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataBacklog" TypeName="BacklogTableAdapters.and_backlogTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="product_group" Type="String" />
                    <asp:Parameter Name="description" Type="String" />
                    <asp:Parameter Name="orders_booked" Type="Double" />
                    <asp:Parameter Name="sales" Type="Double" />
                    <asp:Parameter Name="backlog" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataBacklogDam"
                TypeName="BacklogDamTableAdapters.and_backlogTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="product_group" Type="String" />
                    <asp:Parameter Name="description" Type="String" />
                    <asp:Parameter Name="orders_booked" Type="Double" />
                    <asp:Parameter Name="sales" Type="Double" />
                    <asp:Parameter Name="backlog" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataBacklogJed"
                TypeName="BacklogJedTableAdapters.and_backlogTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="product_group" Type="String" />
                    <asp:Parameter Name="description" Type="String" />
                    <asp:Parameter Name="orders_booked" Type="Double" />
                    <asp:Parameter Name="sales" Type="Double" />
                    <asp:Parameter Name="backlog" Type="Double" />
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
</table>
</asp:Panel>