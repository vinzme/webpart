<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AccountsRec.ascx.vb" Inherits="AccountsRec" %>

<atlas:ScriptManagerProxy ID="sl5" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Text="Order No. :" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue" Width="74px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="White" Width="71px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label3" runat="server" Text="Type :" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue" Width="51px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="White" Width="173px"></asp:Label></td>
        <td style="width: 100px">
            </td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label5" runat="server" Font-Size="XX-Small" ForeColor="White" Height="9px"
                Text="Label" Width="72px" Visible="False"></asp:Label></td>
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
        <td colspan="5">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource1"
                GridLines="Horizontal" Width="100%" Font-Names="Verdana" Font-Size="X-Small" ShowFooter="True">
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/soimages/select.png">
                        <ItemStyle Font-Size="XX-Small" />
                    </asp:CommandField>
                    <asp:BoundField DataField="invdate" DataFormatString="{0:d}" HeaderText="Date" HtmlEncode="False"
                        SortExpression="invdate" />
                    <asp:BoundField DataField="invoice_no" HeaderText="Invoice No" SortExpression="invoice_no" />
                    <asp:BoundField DataField="aramount" HeaderText="Amount" SortExpression="aramount">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="paid" HeaderText="Paid" SortExpression="paid">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="adjustment" HeaderText="Adjustment" SortExpression="adjustment">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="outstanding" HeaderText="Outstanding" SortExpression="outstanding">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Height="24px" />
                <SelectedRowStyle BackColor="SlateBlue" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <AlternatingRowStyle BackColor="#F7F7F7" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOAr" TypeName="SOArTableAdapters.and_arTableAdapter" InsertMethod="Insert">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" DefaultValue="" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" DefaultValue="" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="invdate" Type="DateTime" />
                    <asp:Parameter Name="invoice_no" Type="String" />
                    <asp:Parameter Name="aramount" Type="String" />
                    <asp:Parameter Name="paid" Type="Double" />
                    <asp:Parameter Name="adjustment" Type="Double" />
                    <asp:Parameter Name="outstanding" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOArDam" TypeName="SOArDamTableAdapters.and_arTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="invdate" Type="DateTime" />
                    <asp:Parameter Name="invoice_no" Type="String" />
                    <asp:Parameter Name="aramount" Type="String" />
                    <asp:Parameter Name="paid" Type="Double" />
                    <asp:Parameter Name="adjustment" Type="Double" />
                    <asp:Parameter Name="outstanding" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOArJed" TypeName="SOArJedTableAdapters.and_arTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td style="text-align: left;" colspan="2">
            <asp:Label ID="Label6" runat="server" Text="Invoice No. :" Width="89px" Font-Bold="False" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"></asp:Label><asp:Label
                ID="Label7" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small"
                ForeColor="MediumBlue" Width="98px" BackColor="PapayaWhip"></asp:Label></td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
        <td style="width: 100px">
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource2"
                Font-Names="Verdana" Font-Size="X-Small" ShowFooter="True" Width="100%" GridLines="None">
                <FooterStyle BackColor="LightSkyBlue" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="invdate" HeaderText="Date" SortExpression="invdate" DataFormatString="{0:d}" HtmlEncode="False" />
                    <asp:BoundField DataField="kind" HeaderText="Kind" SortExpression="kind" />
                    <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="payment" HeaderText="Payment/Adj" SortExpression="payment" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOArDetails"
                TypeName="SOArDetailsTableAdapters.and_soarinvTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label7" Name="arinvoiceno" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="invdate" Type="DateTime" />
                    <asp:Parameter Name="kind" Type="String" />
                    <asp:Parameter Name="des" Type="String" />
                    <asp:Parameter Name="amount" Type="Double" />
                    <asp:Parameter Name="payment" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOArDetailsDam"
                TypeName="SOArDetailsDamTableAdapters.and_soarinvTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label7" Name="arinvoiceno" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="invdate" Type="DateTime" />
                    <asp:Parameter Name="kind" Type="String" />
                    <asp:Parameter Name="des" Type="String" />
                    <asp:Parameter Name="amount" Type="Double" />
                    <asp:Parameter Name="payment" Type="Double" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOArDetailsJed"
                TypeName="SOArDetailsJedTableAdapters.and_soarinvTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label7" Name="arinvoiceno" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="Label5" Name="OrderUser" PropertyName="Text" Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="invdate" Type="DateTime" />
                    <asp:Parameter Name="kind" Type="String" />
                    <asp:Parameter Name="des" Type="String" />
                    <asp:Parameter Name="amount" Type="Double" />
                    <asp:Parameter Name="payment" Type="Double" />
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
        <td style="width: 100px">
        </td>
    </tr>
</table>
</asp:Panel>