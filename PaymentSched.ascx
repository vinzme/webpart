<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PaymentSched.ascx.vb" Inherits="PaymentSchedule" %>

<atlas:ScriptManagerProxy ID="sl4" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Text="Order No. :" Width="80px" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" Text="000" Width="77px" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="White"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label3" runat="server" Text="Type :" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue" Width="49px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label4" runat="server" Text="Supply and Installation" Width="180px" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="White"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label6" runat="server" Font-Size="XX-Small" ForeColor="White" Text="Label" Visible="False" Width="80px" BackColor="White"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" Font-Names="Verdana" Font-Size="X-Small">
                <Columns>
                    <asp:BoundField DataField="expected_date" HeaderText="Date" SortExpression="expected_date" DataFormatString="{0:d}" HtmlEncode="False" />
                    <asp:BoundField DataField="statusval" HeaderText="Amount" ReadOnly="True" SortExpression="statusval" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="RoyalBlue" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataPaymentSched" TypeName="PaymentSchedTableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataPaymentSchedDam" TypeName="PaymentSchedDamTableAdapters.prshmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataPaymentSchedJed" TypeName="PaymentSchedJedTableAdapters.prshmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 100px; height: 18px;">
            <asp:Label ID="Label5" runat="server" Text="Document:" Width="91px" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small"></asp:Label></td>
        <td style="width: 100px; height: 18px;">
        </td>
        <td style="width: 100px; height: 18px;">
        </td>
        <td style="width: 100px; height: 18px;">
        </td>
    </tr>
    <tr>
        <td style="height: 19px;" colspan="4">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataSourceID="ObjectDataSource2" ForeColor="#333333" GridLines="None" Width="100%" Font-Names="Verdana" Font-Size="X-Small">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="dated" HeaderText="Date" SortExpression="dated" DataFormatString="{0:d}" HtmlEncode="False" />
                    <asp:BoundField DataField="docno" HeaderText="Doc No" ReadOnly="True" SortExpression="docno" />
                    <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="HotPink" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataPaymentSchedDoc" TypeName="PaymentSchedDocTableAdapters.sldetailsTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataPaymentSchedDocDam" TypeName="PaymentSchedDocDamTableAdapters.sldetailsTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataPaymentSchedDocJed" TypeName="PaymentSchedDocJedTableAdapters.sldetailsTableAdapter">
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