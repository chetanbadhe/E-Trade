<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="eTrade._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome! Shortcuts to your Account!
    </h2>
    <p>
        <table width="100%">
            <tr>
                <td align="center" width="45%">
                    <h3>
                        My Stocks Portfolio
                    </h3>
                </td>
                <td>
                </td>
                <td align="center" width="45%">
                    <h3>
                        My Currency Converter
                    </h3>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:ImageButton ID="imgStockBTN" runat="server" ImageUrl="~/Images/Stocks.png" Width="75%"
                        Height="100%" PostBackUrl="UserPortfolio.aspx" />
                </td>
                <td>
                </td>
                <td align="center">
                    <asp:ImageButton ID="imgCurrencyBTN" runat="server" ImageUrl="~/Images/Currency.png"
                        Width="72%" Height="77%" PostBackUrl="~/CurrencyMarket.aspx" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <h3>
                        WatchList
                    </h3>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:ImageButton ID="imgWatchListBTN" runat="server" ImageUrl="~/Images/watchlist.png"
                        Width="30%" Height="70%" PostBackUrl="~/UsersWatchList.aspx" />
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
