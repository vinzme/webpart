<%@ Page Language="VB" masterpagefile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SalesOrder.aspx.vb" Inherits="SalesOrder" %>

<%@ Register Src="SOCharges.ascx" TagName="SOCharges" TagPrefix="uc13" %>

<%@ Register Src="WipSumm.ascx" TagName="WipSumm" TagPrefix="uc12" %>

<%@ Register Src="Material.ascx" TagName="Material" TagPrefix="uc11" %>

<%@ Register Src="AdvancePayment.ascx" TagName="AdvancePayment" TagPrefix="uc10" %>

<%@ Register Src="Margin.ascx" TagName="Margin" TagPrefix="uc9" %>

<%@ Register Src="Backlog.ascx" TagName="Backlog" TagPrefix="uc8" %>

<%@ Register Src="CostofSales.ascx" TagName="CostofSales" TagPrefix="uc7" %>

<%@ Register Src="Workinprogress.ascx" TagName="Workinprogress" TagPrefix="uc6" %>

<%@ Register Src="Absorbed.ascx" TagName="Absorbed" TagPrefix="uc5" %>

<%@ Register Src="AccountsRec.ascx" TagName="AccountsRec" TagPrefix="uc4" %>

<%@ Register Src="PaymentSched.ascx" TagName="PaymentSched" TagPrefix="uc3" %>

<%@ Register Src="OrderDetail.ascx" TagName="OrderDetail" TagPrefix="uc1" %>

<%@ Register Src="OrderHeader.ascx" TagName="OrderHeader" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
    <asp:ScriptManager ID="sl" runat="server" />
    <asp:UpdatePanel ID="pl1" runat="server">
    <ContentTemplate>


<div class="menuheader">
    <asp:WebPartManager ID="WebPartManager1" runat="server">
    </asp:WebPartManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: lavender; border-top: lightslategray 2px solid; height: 33px;">
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="RoyalBlue"
                    Text="Enter Order No. :" Width="132px" Font-Size="Small"></asp:Label></td>
            <td colspan="2">
                <asp:TextBox ID="TextBox1" runat="server" BackColor="AliceBlue" Font-Bold="True"
                    Font-Names="Verdana" Font-Size="Small" ForeColor="Red" Width="80px"></asp:TextBox><asp:Button ID="ButtonView" runat="server" Text="View" TabIndex="1" OnClick="ButtonView_Click" /></td>
            <td style="width: 100px">
                <asp:Label ID="Label3" runat="server" Text="Label" Width="61px" ForeColor="Lavender"></asp:Label>
            </td>
            <td style="width: 100px">
                <asp:Label ID="Label7" runat="server" Text="Label" ForeColor="Lavender" Width="31px"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="TextBox2" runat="server" Width="259px" Font-Names="Verdana"></asp:TextBox><asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" /></td>
            <td style="width: 100px">
                <asp:Label ID="Label4" runat="server" Text="Label" ForeColor="Lavender" Width="78px"></asp:Label>
            </td>
            <td style="width: 100px">
                <asp:Label ID="Label5" runat="server" Text="Label" ForeColor="Lavender" Width="47px"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="Label6" runat="server" Text="Mode:" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="Red" Width="35px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Font-Names="Verdana" Font-Size="X-Small">
                    <asp:ListItem Value="0">Browse</asp:ListItem>
                    <asp:ListItem Value="1">Edit</asp:ListItem>
                    <asp:ListItem Value="2">Reset</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: #ccffcc;">
            <tr>
                <td style="width: 100px; text-align: center">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="MidnightBlue"
                        Text="Select Type :" Width="106px"></asp:Label></td>
                <td style="width: 100px">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" ForeColor="MidnightBlue" RepeatDirection="Horizontal" Width="899px">
                        <asp:ListItem Value="0">All</asp:ListItem>
                        <asp:ListItem Value="1">Supply and Installation</asp:ListItem>
                        <asp:ListItem Value="2">Supply Only</asp:ListItem>
                        <asp:ListItem Value="3">Maintenance</asp:ListItem>
                        <asp:ListItem Value="4">Current Account</asp:ListItem>
                        <asp:ListItem Value="5">Expense</asp:ListItem>
                        <asp:ListItem Value="6">Installation</asp:ListItem>
                        <asp:ListItem Value="7">Repair WorkShop</asp:ListItem>
                        <asp:ListItem Value="8">Warranty</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
        </table>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListSearch"
                    TypeName="SOListSearchTableAdapters.projectsTableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label4" Name="SearchKey" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>        
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListJeddah" TypeName="SOListSearchJedTableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label4" Name="SearchKey" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListI" TypeName="SOListSearchITableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label4" Name="SearchKey" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListJedI" TypeName="SOListSearchJedITableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label4" Name="SearchKey" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOByType" TypeName="SOListByTypeTableAdapters.projectsTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_project_code" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="project_code" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="customer" Type="String" />
                <asp:Parameter Name="custname" Type="String" />
                <asp:Parameter Name="project_type" Type="String" />
                <asp:Parameter Name="sitename" Type="String" />
                <asp:Parameter Name="Original_project_code" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="Label5" Name="SOType" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label3" Name="SOUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListAll" TypeName="SOListAllTableAdapters.projectsTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_project_code" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="project_code" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="customer" Type="String" />
                <asp:Parameter Name="custname" Type="String" />
                <asp:Parameter Name="project_type" Type="String" />
                <asp:Parameter Name="sitename" Type="String" />
                <asp:Parameter Name="Original_project_code" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource7" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListAllI" TypeName="SOListAllITableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListSearchDam" TypeName="SOListSearchDamTableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label4" Name="SearchKey" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource9" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListSearchDamI" TypeName="SOListSearchDamITableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label4" Name="SearchKey" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource10" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListByTypeJed" TypeName="SOListByTypeJedTableAdapters.projectsTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_project_code" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="project_code" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="customer" Type="String" />
                <asp:Parameter Name="custname" Type="String" />
                <asp:Parameter Name="project_type" Type="String" />
                <asp:Parameter Name="sitename" Type="String" />
                <asp:Parameter Name="Original_project_code" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="Label5" Name="SOType" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label3" Name="SOUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource11" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListAllJed" TypeName="SOListAllJedTableAdapters.projectsTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_project_code" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="project_code" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="customer" Type="String" />
                <asp:Parameter Name="custname" Type="String" />
                <asp:Parameter Name="project_type" Type="String" />
                <asp:Parameter Name="sitename" Type="String" />
                <asp:Parameter Name="Original_project_code" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource12" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListAllIJed" TypeName="SOListAllIJedTableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource13" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListByTypeDam" TypeName="SOListByTypeDamTableAdapters.projectsTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_project_code" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="project_code" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="customer" Type="String" />
                <asp:Parameter Name="custname" Type="String" />
                <asp:Parameter Name="project_type" Type="String" />
                <asp:Parameter Name="sitename" Type="String" />
                <asp:Parameter Name="Original_project_code" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="Label5" Name="SOType" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label3" Name="SOUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource14" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListAllDam" TypeName="SOListAllDamTableAdapters.projectsTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_project_code" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="project_code" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="customer" Type="String" />
                <asp:Parameter Name="custname" Type="String" />
                <asp:Parameter Name="project_type" Type="String" />
                <asp:Parameter Name="sitename" Type="String" />
                <asp:Parameter Name="Original_project_code" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource15" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListAllIDam" TypeName="SOListAllIDamTableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="customer" Type="String" />
                <asp:Parameter Name="custname" Type="String" />
                <asp:Parameter Name="project_type" Type="String" />
                <asp:Parameter Name="sitename" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource16" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListByTypeI" TypeName="SOListByTypeITableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label5" Name="SOType" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource17" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListByTypeIJed" TypeName="SOListByTypeIJedTableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label5" Name="SOType" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource18" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataSOListByTypeIDam" TypeName="SOListByTypeIDamTableAdapters.projectsTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label5" Name="SOType" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Label3" Name="SoUser" PropertyName="Text" Type="String" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="customer" Type="String" />
                <asp:Parameter Name="custname" Type="String" />
                <asp:Parameter Name="project_type" Type="String" />
                <asp:Parameter Name="sitename" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>

            <ProgressTemplate>
                <div class="progress" style="font-weight: bold; font-size: 8pt; left: 820px; color: #0033cc; font-family: Verdana; position: absolute; top: 24px;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="soimages/Computer004.gif" />In Progress......
                </div>
            
            </ProgressTemplate>        
        

        <div>
        <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" BorderColor="#CCCCCC" Font-Names="Verdana" Padding="6" Font-Size="Small">
            <ZoneTemplate>
                  
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="project_code"
                    DataSourceID="ObjectDataSource1" AllowPaging="True" AllowSorting="True" Width="100%" Font-Names="Verdana" Font-Size="X-Small" Height="22px" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/soimages/select.png" />
                        <asp:BoundField DataField="project_code" HeaderText="Sales Order" ReadOnly="True"
                            SortExpression="project_code" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cc" HeaderText="Cost Centre" ReadOnly="True" SortExpression="cc" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="div" HeaderText="Division" ReadOnly="True" SortExpression="div" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="customer" HeaderText="Customer" SortExpression="customer" />
                        <asp:BoundField DataField="custname" HeaderText="Name" SortExpression="custname" />
                        <asp:BoundField DataField="project_type" HeaderText="Type" SortExpression="project_type" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sitename" HeaderText="Site" SortExpression="sitename" />
                        <asp:BoundField DataField="start_date" HeaderText="Start Date" SortExpression="start_date" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="end_date" HeaderText="Expiry Date" SortExpression="end_date" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <RowStyle BackColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                </asp:GridView>
            </ZoneTemplate>
            <FooterStyle Wrap="True" />
            <PartChromeStyle BackColor="White" BorderColor="CornflowerBlue" Font-Names="Verdana" ForeColor="#333333" />
            <MenuLabelHoverStyle ForeColor="#FFCC66" />
            <EmptyZoneTextStyle Font-Size="0.8em" />
            <MenuLabelStyle ForeColor="MidnightBlue" />
            <MenuVerbHoverStyle BackColor="#FFFBD6" BorderColor="#CCCCCC" BorderStyle="Solid"
                BorderWidth="1px" ForeColor="#333333" />
            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
            <MenuVerbStyle BorderColor="#990000" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
            <MenuPopupStyle BackColor="#990000" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                Font-Size="0.6em" />
            <PartTitleStyle BackColor="AliceBlue" Font-Bold="True" Font-Size="0.8em" ForeColor="MidnightBlue" />
        </asp:WebPartZone>
      </div>
            <div id="wz1">
                <asp:WebPartZone ID="WebPartZone2" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana" Font-Size="Small" Padding="6" Width="100%">
                    <ZoneTemplate>
                        <uc2:OrderHeader ID="OrderHeader1" runat="server" />
                        <uc3:PaymentSched ID="PaymentSched1" runat="server" />
                        <uc7:CostofSales ID="CostofSales1" runat="server" />
                        <uc9:Margin ID="Margin1" runat="server" />
                        <uc11:Material ID="Material1" runat="server" />
                        <uc12:WipSumm ID="WipSumm1" runat="server" />
                    </ZoneTemplate>
                    <PartChromeStyle BackColor="#FFFBD6" BorderColor="#D1DDF1" Font-Names="Verdana" ForeColor="MidnightBlue" />
                    <MenuLabelHoverStyle ForeColor="#D1DDF1" />
                    <EmptyZoneTextStyle Font-Size="0.8em" />
                    <MenuLabelStyle ForeColor="MidnightBlue" />
                    <MenuVerbHoverStyle BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" ForeColor="#333333" />
                    <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                    <MenuVerbStyle BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                    <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                    <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="MidnightBlue" />
                    <MenuPopupStyle BackColor="#507CD1" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                        Font-Size="0.6em" />
                    <PartTitleStyle BackColor="#D8E9EC" Font-Bold="True" Font-Size="0.8em" ForeColor="MidnightBlue" />
                    <SelectedPartChromeStyle ForeColor="MidnightBlue" />
                </asp:WebPartZone>
            </div>
            <div id="wz2">
                <asp:WebPartZone ID="WebPartZone3" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana" Padding="6" Font-Bold="False" Font-Size="Small" Width="100%">
                    <ZoneTemplate>
                        <uc1:OrderDetail ID="OrderDetail1" runat="server" />
                        <uc4:AccountsRec ID="AccountsRec1" runat="server" />
                        <uc5:Absorbed ID="Absorbed1" runat="server" />
                        <uc8:Backlog ID="Backlog1" runat="server" />
                        <uc10:AdvancePayment ID="AdvancePayment1" runat="server" />
                    </ZoneTemplate>
                    <PartChromeStyle BackColor="#EFF3FB" BorderColor="#D1DDF1" Font-Names="Verdana" ForeColor="#333333" />
                    <MenuLabelHoverStyle ForeColor="#D1DDF1" />
                    <EmptyZoneTextStyle Font-Size="0.8em" />
                    <MenuLabelStyle ForeColor="MidnightBlue" />
                    <MenuVerbHoverStyle BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" ForeColor="#333333" />
                    <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                    <MenuVerbStyle BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                    <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                    <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                    <MenuPopupStyle BackColor="#507CD1" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                        Font-Size="0.6em" />
                    <PartTitleStyle BackColor="#D8E9EC" Font-Bold="True" Font-Size="0.8em" ForeColor="MidnightBlue" />
                </asp:WebPartZone>
            </div>
            <div id="wz4">
                <asp:WebPartZone ID="WebPartZone4" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana" Font-Size="Small" Padding="6" Width="100%">
                    <ZoneTemplate>
                        <uc6:Workinprogress ID="Workinprogress1" runat="server" />
                        <uc13:SOCharges ID="SOCharges1" runat="server" />
                    </ZoneTemplate>
                    <PartChromeStyle BackColor="#FFFBD6" BorderColor="#D1DDF1" Font-Names="Verdana" ForeColor="MidnightBlue" />
                    <MenuLabelHoverStyle ForeColor="#D1DDF1" />
                    <EmptyZoneTextStyle Font-Size="0.8em" />
                    <MenuLabelStyle ForeColor="MidnightBlue" />
                    <MenuVerbHoverStyle BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" ForeColor="#333333" />
                    <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                    <MenuVerbStyle BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                    <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                    <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="MidnightBlue" />
                    <MenuPopupStyle BackColor="#507CD1" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                        Font-Size="0.6em" />
                    <PartTitleStyle BackColor="#D8E9EC" Font-Bold="True" Font-Size="0.8em" ForeColor="MidnightBlue" />
                    <SelectedPartChromeStyle ForeColor="MidnightBlue" />                    
                </asp:WebPartZone>
            </div>            
</ContentTemplate>   
</asp:UpdatePanel>    
</asp:Content>