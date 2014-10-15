<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="WatchList.aspx.cs" Inherits="eTrade.WatchList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upWatchListouter" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSymbol" runat="server"></asp:TextBox>
                        <asp:Button ID="btnGetSymbol" runat="server" Text="Go" OnClick="btnGetSymbol_Click"
                            ValidationGroup="Go" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rftxtSymbol" runat="server" ErrorMessage="Symbol Required!"
                            ForeColor="Red" ControlToValidate="txtSymbol" ValidationGroup="Go"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DetailsView ID="dvWatchList" runat="server" AutoGenerateRows="true" Height="50px"
                            Width="125px">
                        </asp:DetailsView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
