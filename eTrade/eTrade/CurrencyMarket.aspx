<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CurrencyMarket.aspx.cs" Inherits="eTrade.CurrencyMarket" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table id="tblname1" runat="server" width="50%" horizontalalign="Center" align="center">
        <tr>
            <td width="45%">
                <asp:DropDownList ID="ddlcurrencyfrom" runat="server" Width="100%">
                    <asp:ListItem Text="Select" Selected="True">
                    </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="10%">
            </td>
            <td width="45%">
                <asp:DropDownList ID="ddlcurrencyto" runat="server" Width="100%">
                    <asp:ListItem Text="Select" Selected="True">
                    </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtcurrencyfrom" runat="server" Width="100%">1</asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnGetRate" runat="server" Text="Go" OnClick="btnGetRate_Click" Width="100%" />
            </td>
            <td>
                <asp:TextBox ID="txtcurrencyto" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
