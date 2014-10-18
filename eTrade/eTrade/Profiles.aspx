<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Profiles.aspx.cs" Inherits="eTrade.Profiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:GridView ID="gvGetProfiles" runat="server" AutoGenerateColumns="True" DataKeyNames="ProfileID"
                    ViewStateMode="Enabled" CellPadding="4" Width="100%" ForeColor="#333333" AllowPaging="True"
                    DataSourceID="EDSProfiles">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ShowEditButton="True" />
                        <%-- <asp:BoundField DataField="ProfileID" HeaderText="ProfileID" ReadOnly="True" SortExpression="ProfileID" />
                        <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                        <asp:BoundField DataField="ProfileName" HeaderText="ProfileName" SortExpression="ProfileName" />
                        <asp:CheckBoxField DataField="isDefault" HeaderText="isDefault" SortExpression="isDefault" />--%>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Center" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:EntityDataSource ID="EDSProfiles" runat="server" ConnectionString="name=eTradeDbEntities"
                    DefaultContainerName="eTradeDbEntities" EnableFlattening="False" EnableUpdate="True"
                    EntitySetName="Profiles" EntityTypeFilter="Profile">
                </asp:EntityDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
