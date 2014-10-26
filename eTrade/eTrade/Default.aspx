<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="eTrade._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to your Portfolio!
    </h2>
    <p>
        <table width="100%">
            <tr>
                <td width="45%">
                    <h3>
                        My Stocks Portfolio
                    </h3>
                </td>
                <td>
                </td>
                <td width="45%">
                    <h3>
                        My Currency Converter
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="imgStockBTN" runat="server" ImageUrl="~/Images/Stocks.png" Width="75%" Height="75%" PostBackUrl=UserPortfolio.aspx />
                </td>
                <td>
                </td>
                <td>
                    <asp:ImageButton ID="imgCurrencyBTN" runat="server" ImageUrl="~/Images/Currency.png" Width="75%" Height="75%" PostBackUrl="~/CurrencyMarket.aspx" />
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
