<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Absorbed.ascx.vb" Inherits="Absorbed" %>

<atlas:ScriptManagerProxy ID="sl6" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :" Width="76px"></asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="71px">000</asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :" Width="60px"></asp:Label></td>
        <td style="width: 100px; height: 16px;">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" CellPadding="4" Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectImageUrl="~/soimages/select.png" ButtonType="Image" />
                    <asp:BoundField DataField="period" HeaderText="Period" ReadOnly="True" SortExpression="period" />
                    <asp:BoundField DataField="nohours" HeaderText="Hours" ReadOnly="True" SortExpression="nohours" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cost" HeaderText="Cost" ReadOnly="True" SortExpression="cost" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataBurden" TypeName="BurdenTableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo2" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataBurdenDam" TypeName="BurdenDamTableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo2" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataBurdenJed" TypeName="BurdernJedTableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo2" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" ForeColor="White" Text="000" Visible="False"
                Width="98px"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="height: 16px; text-align: left;" colspan="2">
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small"
                ForeColor="Navy" Width="65px">Period :</asp:Label><asp:Label ID="Label7" runat="server"
                    BackColor="RoyalBlue" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small"
                    ForeColor="White" Text="0" Width="74px"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataSourceID="ObjectDataSource2" Font-Names="Verdana" Font-Size="X-Small" ForeColor="#333333"
                GridLines="None" Width="100%" ShowFooter="True">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="empno" HeaderText="Emp No." SortExpression="empno" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="alpha" HeaderText="Name" SortExpression="alpha" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SumOfHours" HeaderText="Hours" ReadOnly="True" SortExpression="SumOfHours" />
                    <asp:BoundField DataField="SumOfCost" HeaderText="Cost" ReadOnly="True" SortExpression="SumOfCost" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#E3EAEB" />
                <EditRowStyle BackColor="#7C6F57" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataAbsorbedDetails" TypeName="BurdenDetailsTableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo2" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label7" Name="OrderPeriod" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataAbsorbedDetailsDam" TypeName="BurdernDetailsDamTableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo2" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label7" Name="OrderPeriod" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataAbsorbedDetailsJed" TypeName="BurdernDetailsJedTableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderNo2" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label7" Name="OrderPeriod" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
</table>
</asp:Panel>