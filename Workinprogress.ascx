<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Workinprogress.ascx.vb" Inherits="Workinprogress" %>

<atlas:ScriptManagerProxy ID="sl7" EnableScriptComponents="true" runat="server"/>
<asp:Panel ID="Panel1" runat="server" Height="371px" Width="100%">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Order No. :" Width="77px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Text="000" Width="68px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="MidnightBlue"
                Text="Type :" Width="47px"></asp:Label></td>
        <td style="width: 100px">
            <asp:Label ID="Label4" runat="server" BackColor="DodgerBlue" Font-Bold="True" Font-Names="Verdana"
                Font-Size="X-Small" ForeColor="White" Width="165px"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Red"
                Text="Selection :" Width="72px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
        </td>
        <td style="width: 100px; height: 19px;">
            <asp:Label ID="Label5" runat="server" Text="Label" Visible="False" Width="29px"></asp:Label></td>
        <td style="width: 100px; height: 19px;">
        </td>
    </tr>
    <tr>
        <td style="text-align: left;" colspan="4">
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="233px" BackColor="Honeydew" Font-Names="Verdana" Font-Size="XX-Small">
            </asp:DropDownList></td>
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
        <td colspan="4">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource1"
                        Font-Names="Verdana" Font-Size="XX-Small" ShowFooter="True" Width="100%">
                        <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="analysis3" HeaderText="Group" SortExpression="analysis3" />
                            <asp:BoundField DataField="expense_code" HeaderText="Expense Code" SortExpression="expense_code" />
                            <asp:BoundField DataField="period" HeaderText="Period" SortExpression="period" />
                            <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                            <asp:BoundField DataField="amount" HeaderText="Debit" SortExpression="amount">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amount2" HeaderText="Credit" SortExpression="amount2">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipAllSource" TypeName="SOWipAllSourceTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource9" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipAllSourceDam" TypeName="SOWipAllSourceDamTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource10" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipAllSourceJed" TypeName="SOWipAllSourceJedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource2"
                        Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True">
                        <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="trans_date" DataFormatString="{0:d}" HeaderText="Date"
                                HtmlEncode="False" SortExpression="trans_date" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True"
                                SortExpression="description" />
                            <asp:BoundField DataField="trans_reference" HeaderText="Batch" SortExpression="trans_reference" />
                            <asp:BoundField DataField="amount" HeaderText="Debit" SortExpression="amount" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amount2" HeaderText="Credit" SortExpression="amount2" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataDirectCost" TypeName="SOWipDirectTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource11" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataDirectCostDam" TypeName="SOWipDirectDamTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource12" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataDirectCostJed" TypeName="SOWipDirectJedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource3"
                        Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True">
                        <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="trans_date" DataFormatString="{0:d}" HeaderText="Date"
                                HtmlEncode="False" SortExpression="trans_date" />
                            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True"
                                SortExpression="description" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sec_reference" HeaderText="T" ReadOnly="True" SortExpression="sec_reference" />
                            <asp:BoundField DataField="ref_code" HeaderText="Document" SortExpression="ref_code" />
                            <asp:BoundField DataField="num_units" HeaderText="Qty" SortExpression="num_units" />
                            <asp:BoundField DataField="amount" HeaderText="Debit" SortExpression="amount" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amount2" HeaderText="Credit" SortExpression="amount2" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="#000066" BackColor="AliceBlue" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipMaterials" TypeName="SOWipMaterialsTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource13" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipMaterialsDam" TypeName="SOWipMaterialsDamTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource14" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipMaterialsJed" TypeName="SOWipMaterialsJedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource25" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipMaterialsLive" TypeName="SOWipMaterialsLiveTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="View4" runat="server">
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="period"
                        DataSourceID="ObjectDataSource4" Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True">
                        <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="period" HeaderText="Period" ReadOnly="True" SortExpression="period" />
                            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True"
                                SortExpression="description" />
                            <asp:BoundField DataField="amt1" HeaderText="Debit" ReadOnly="True" SortExpression="amt1" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amt2" HeaderText="Credit" ReadOnly="True" SortExpression="amt2" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipAbsorbed" TypeName="SOWipAbsorbedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource15" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipAbsorbedDam" TypeName="SOWipAbsorbedDamTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource16" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipAbsorbedJed" TypeName="SOWipAbsorbedJedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="View5" runat="server">
                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource5"
                        Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True">
                        <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="analysis3" HeaderText="Group" SortExpression="analysis3" />
                            <asp:BoundField DataField="expense_code" HeaderText="Expense Code" SortExpression="expense_code" />
                            <asp:BoundField DataField="period" HeaderText="Period" SortExpression="period" />
                            <asp:BoundField DataField="trans_date" DataFormatString="{0:d}" HeaderText="Date"
                                HtmlEncode="False" SortExpression="trans_date" />
                            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True"
                                SortExpression="description" />
                            <asp:BoundField DataField="amount" HeaderText="Debit" SortExpression="amount">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amount1" HeaderText="Credit" SortExpression="amount1">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesSource" TypeName="SOWipSalesSourceTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource17" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesSourceDam" TypeName="SOWIPSalesSourceDamTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource18" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesSourceJed" TypeName="SOWIPSalesSourceJedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="View6" runat="server">
                    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource6"
                        Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True">
                        <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="trans_date" DataFormatString="{0:d}" HeaderText="Date"
                                HtmlEncode="False" SortExpression="trans_date" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True"
                                SortExpression="description" />
                            <asp:BoundField DataField="trans_reference" HeaderText="Batch" SortExpression="trans_reference" />
                            <asp:BoundField DataField="amount" HeaderText="Debit" SortExpression="amount" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amount1" HeaderText="Credit" SortExpression="amount1" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesDirect" TypeName="SOWipSalesDirectTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource19" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesDirectDam" TypeName="SOWipSalesDirectDamTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource20" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesDirectJed" TypeName="SOWipSalesDirectJedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="View7" runat="server">
                    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource7"
                        Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True">
                        <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
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
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource7" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesMaterials" TypeName="SOWipSalesMaterialsTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource21" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesMaterialsDam" TypeName="SOWipSalesMaterialsDamTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource22" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesMaterialsJed" TypeName="SOWipSalesMaterialsJedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="View8" runat="server">
                    <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="period"
                        DataSourceID="ObjectDataSource8" Font-Names="Verdana" Font-Size="X-Small" Width="100%" ShowFooter="True">
                        <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="period" HeaderText="Period" ReadOnly="True" SortExpression="period" />
                            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True"
                                SortExpression="description" />
                            <asp:BoundField DataField="amt1" HeaderText="Debit" ReadOnly="True" SortExpression="amt1" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="amt2" HeaderText="Credit" ReadOnly="True" SortExpression="amt2" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesAbsorbed" TypeName="SOWipSalesAbsorbedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource23" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesAbsorbedDam" TypeName="SOWipSalesAbsorbedDamTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource24" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataWipSalesAbsorbedJed" TypeName="SOWipSalesAbsorbedJedTableAdapters.prtrm_aTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Label2" Name="OrderNo" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="Label5" Name="ProjType" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
            </asp:MultiView></td>
    </tr>
</table>
</asp:Panel>
