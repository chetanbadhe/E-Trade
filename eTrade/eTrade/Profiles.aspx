<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Profiles.aspx.cs" Inherits="eTrade.Profiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="75%">
        <tr>
            <td>
                <asp:Label ID="lblProfiledefault" runat="server" Text="Set default profile"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlprofiles" runat="server" DataValueField="ProfileID" DataTextField="ProfileName">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSetDefault" runat="server" Text="Set" OnClick="btnSetDefault_Click" />
            </td>
        </tr>
        <tr>
            <h3>
                User Profiles
            </h3>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvGetProfiles" runat="server" AutoGenerateColumns="False" DataKeyNames="ProfileID"
                    ViewStateMode="Enabled" CellPadding="4" Width="100%" ForeColor="#333333" AllowPaging="True"
                    DataSourceID="EDSProfiles">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="ProfileName" HeaderText="ProfileName" SortExpression="ProfileName" />
                        <asp:BoundField DataField="isDefault" HeaderText="Default" SortExpression="ProfileName"
                            ReadOnly="true" />
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
                    AutoGenerateWhereClause="true" EntitySetName="Profiles" EntityTypeFilter="Profile">
                </asp:EntityDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
