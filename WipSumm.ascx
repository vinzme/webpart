<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WipSumm.ascx.vb" Inherits="WipSumm" %>

<atlas:ScriptManagerProxy ID="sl13" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :" Width="80px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Text="0" Width="71px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :" Width="47px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" Text="0" Width="96px" Visible="False"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" CellPadding="4" Font-Names="Verdana" Font-Size="X-Small" ForeColor="#333333" GridLines="None" Width="100%" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="expense_code" HeaderText="Source" SortExpression="expense_code" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="debit" HeaderText="Debit" SortExpression="debit" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="credit" HeaderText="Credit" SortExpression="credit" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <EditRowStyle BackColor="#7C6F57" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataWipSumm" TypeName="SOWipSummTableAdapters.and_wipsummTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                    <asp:Parameter DefaultValue="N" Name="CostAfter" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="expense_code" Type="String" />
                    <asp:Parameter Name="description" Type="String" />
                    <asp:Parameter Name="debit" Type="Double" />
                    <asp:Parameter Name="credit" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataWipSummDam" TypeName="SOWipSummDamTableAdapters.and_wipsummTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                    <asp:Parameter DefaultValue="N" Name="CostAfter" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataWipSummJed" TypeName="SOWipSummJedTableAdapters.and_wipsummTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                    <asp:Parameter DefaultValue="N" Name="CostAfter" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small"
                ForeColor="MidnightBlue" Text="Cost After Closing :" Width="155px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
        </td>
        <td style="width: 100px; height: 19px;">
        </td>
        <td style="width: 100px; height: 19px;">
        </td>
    </tr>
    <tr>
        <td style="height: 19px;" colspan="4">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="expense_code" HeaderText="Source" SortExpression="expense_code" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="debit" HeaderText="Debit" SortExpression="debit" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="credit" HeaderText="Credit" SortExpression="credit" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataWipSumm" TypeName="SOWipSummTableAdapters.and_wipsummTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                    <asp:Parameter DefaultValue="Y" Name="CostAfter" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="expense_code" Type="String" />
                    <asp:Parameter Name="description" Type="String" />
                    <asp:Parameter Name="debit" Type="Double" />
                    <asp:Parameter Name="credit" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataWipSummDam" TypeName="SOWipSummDamTableAdapters.and_wipsummTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                    <asp:Parameter DefaultValue="Y" Name="CostAfter" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataWipSummJed" TypeName="SOWipSummJedTableAdapters.and_wipsummTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                    <asp:Parameter DefaultValue="Y" Name="CostAfter" Type="String" />
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