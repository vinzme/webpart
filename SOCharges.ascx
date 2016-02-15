<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SOCharges.ascx.vb" Inherits="SOCharges" %>

<atlas:ScriptManagerProxy ID="sl14" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :" Width="77px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Text="0" Width="71px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :" Width="50px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
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
        <td style="height: 19px;" colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataSourceID="ObjectDataSource1" Font-Names="Verdana" Font-Size="X-Small" ForeColor="#333333"
                GridLines="None" Width="100%" ShowFooter="True">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="analysis3" HeaderText="Group" SortExpression="analysis3" />
                    <asp:BoundField DataField="expense_code" HeaderText="Expense Code" SortExpression="expense_code" />
                    <asp:BoundField DataField="period" HeaderText="Period" SortExpression="period" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="amount" HeaderText="Debit" SortExpression="amount" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="amount1" HeaderText="Credit" SortExpression="amount1" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataChargesAfter" TypeName="ChargesTableAdapters.prtrm_aTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataChargesAfterDam" TypeName="ChargesDamTableAdapters.prtrm_aTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataChargesAfterJed" TypeName="ChargesJedTableAdapters.prtrm_aTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Text="Material" Width="79px"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow"
                BorderColor="Tan" BorderWidth="1px" CellPadding="2" DataSourceID="ObjectDataSource2"
                Font-Names="Verdana" Font-Size="X-Small" ForeColor="Black" GridLines="None" Width="100%" ShowFooter="True">
                <FooterStyle BackColor="Tan" />
                <Columns>
                    <asp:BoundField DataField="trans_date" DataFormatString="{0:d}" HeaderText="Date"
                        HtmlEncode="False" SortExpression="trans_date" />
                    <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True"
                        SortExpression="description" />
                    <asp:BoundField DataField="sec_reference" HeaderText="T" ReadOnly="True" SortExpression="sec_reference" />
                    <asp:BoundField DataField="ref_code" HeaderText="Document" SortExpression="ref_code" />
                    <asp:BoundField DataField="num_units" HeaderText="Qty" SortExpression="num_units" />
                    <asp:BoundField DataField="amount" HeaderText="Debit" SortExpression="amount" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="amount1" HeaderText="Credit" SortExpression="amount1" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataChargesMaterial" TypeName="SOChargesMatTableAdapters.prtrm_aTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataChargesMaterialDam" TypeName="SOChargesMatDamTableAdapters.prtrm_aTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataChargesMaterialJed" TypeName="SOChargesMatJedTableAdapters.prtrm_aTableAdapter">
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