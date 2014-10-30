<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserPortfolio.aspx.cs" Inherits="eTrade.UserPortfolio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="JavaScript">
        /// <summary>
        /// The function will be called when user
        // changes the chart type to another type.
        /// </summary>
        /// <param name="type">Chart type.</param>
        /// <param name="num">Stock number.</param>
        /// <param name="symbol">Stock symobl.</param>     
        function changeChart(type, num, symbol) {
            // All the DIVs are inside the main DIV
            // and defined in the code-behind class.
            var div1d = document.getElementById("div1d_" + num);
            var div5d = document.getElementById("div5d_" + num);
            var div3m = document.getElementById("div3m_" + num);
            var div6m = document.getElementById("div6m_" + num);
            var div1y = document.getElementById("div1y_" + num);
            var div2y = document.getElementById("div2y_" + num);
            var div5y = document.getElementById("div5y_" + num);
            var divMax = document.getElementById("divMax_" + num);
            var divChart = document.getElementById("imgChart_" + num);
            // Set innerHTML property.
            div1d.innerHTML = "1d";
            div5d.innerHTML = "5d";
            div3m.innerHTML = "3m";
            div6m.innerHTML = "6m";
            div1y.innerHTML = "1y";
            div2y.innerHTML = "2y";
            div5y.innerHTML = "5y";
            divMax.innerHTML = "Max";
            // Use a random number to defeat cache.
            var rand_no = Math.random();
            rand_no = rand_no * 100000000;
            //  Display the stock chart.
            switch (type) {
                case 1: // 5 days
                    div5d.innerHTML = "<b>5d</b>";
                    divChart.src = "http://ichart.finance.yahoo.com/w?s=" +
                           symbol + "&" + rand_no;
                    break;
                case 2: // 3 months
                    div3m.innerHTML = "<b>3m</b>";
                    divChart.src = "http://chart.finance.yahoo.com/c/3m/" +
                           symbol + "?" + rand_no;
                    break;
                case 3: // 6 months 
                    div6m.innerHTML = "<b>6m</b>";
                    divChart.src = "http://chart.finance.yahoo.com/c/6m/" +
                           symbol + "?" + rand_no;
                    break;
                case 4: // 1 year
                    div1y.innerHTML = "<b>1y</b>";
                    divChart.src = "http://chart.finance.yahoo.com/c/1y/" +
                           symbol + "?" + rand_no;
                    break;
                case 5: // 2 years
                    div2y.innerHTML = "<b>2y</b>";
                    divChart.src = "http://chart.finance.yahoo.com/c/2y/" +
                           symbol + "?" + rand_no;
                    break;
                case 6: // 5 years
                    div5y.innerHTML = "<b>5y</b>";
                    divChart.src = "http://chart.finance.yahoo.com/c/5y/" +
                           symbol + "?" + rand_no;
                    break;
                case 7: // Max
                    divMax.innerHTML = "<b>msx</b>";
                    divChart.src = "http://chart.finance.yahoo.com/c/my/" +
                           symbol + "?" + rand_no;
                    break;
                case 0: // 1 day
                default:
                    div1d.innerHTML = "<b>1d</b>";
                    divChart.src = "http://ichart.finance.yahoo.com/b?s=" +
                           symbol + "&" + rand_no;
                    break;
            }
        }



        function ddlAction_SelectedIndexChanged() {
            var DropdownList = document.getElementById('<%=ddlAction.ClientID %>');
            var SelectedValue = DropdownList.value;
            if (SelectedValue == "Purchase" || SelectedValue == "Sell") {
                document.getElementById('<%= txtBuyVolume.ClientID %>').disabled = false;
                document.getElementById('<%= txtBuyPrice.ClientID %>').disabled = false;
                document.getElementById('<%= txtBuyDateofPurchase.ClientID %>').disabled = false;
                document.getElementById('<%= btnBuyStock.ClientID %>').disabled = false;
                var btnBuyStock = document.getElementById('<%=btnBuyStock.ClientID %>');
                btnBuyStock.setAttribute("Text", SelectedValue);
            }
            else {
                document.getElementById('<%= txtBuyVolume.ClientID %>').disabled = true;
                document.getElementById('<%= txtBuyPrice.ClientID %>').disabled = true;
                document.getElementById('<%= txtBuyDateofPurchase.ClientID %>').disabled = true;
                document.getElementById('<%= btnBuyStock.ClientID %>').disabled = true;
                document.getElementById('<%= btnBuyStock.ClientID %>').value = "Action";
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="upWatchListouter" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <asp:TextBox ID="txtSymbol" runat="server"></asp:TextBox>
                        <asp:Button ID="btnGetSymbol" runat="server" Text="Go" OnClick="btnGetSymbol_Click"
                            ValidationGroup="Go" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rftxtSymbol" runat="server" ErrorMessage="Symbol Required!"
                            ForeColor="Red" ControlToValidate="txtSymbol" ValidationGroup="Go"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvGetSymbol" runat="server" AutoGenerateColumns="False" DataKeyNames="Symbol"
                            EmptyDataText="" ViewStateMode="Enabled" CellPadding="4" GridLines="Both" Width="100%"
                            ForeColor="#333333" AllowPaging="true">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="Symbol" HeaderText="Symbol" ReadOnly="True" />
                                <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                <asp:BoundField DataField="LastTradeDate" HeaderText="Date" ReadOnly="True" />
                                <asp:BoundField DataField="Change" HeaderText="% Change" ReadOnly="True" />
                                <asp:BoundField DataField="Bid" HeaderText="Bid" ReadOnly="True" />
                                <asp:BoundField DataField="Ask" HeaderText="Ask" ReadOnly="True" />
                                <asp:BoundField DataField="Volume" HeaderText="Volume" ReadOnly="True" />
                                <asp:BoundField DataField="DaysHigh" HeaderText="DaysHigh" ReadOnly="True" />
                                <asp:BoundField DataField="DaysLow" HeaderText="DaysLow" ReadOnly="True" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnWatchListChart" runat="server" CommandName="CheckChart" CommandArgument='<%#Eval("Symbol")%>'
                                            Text="Chart" />
                                        <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnWatchListChart"
                                            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnWatchListDetails" runat="server" CommandName="StockDetails" CommandArgument='<%#Eval("Symbol")%>'
                                            Text="Details" />
                                        <ajaxToolkit:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="btnWatchListDetails"
                                            CancelControlID="btnDetailsClose" BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnBuy" runat="server" Text="Buy" />
                                        <ajaxToolkit:ModalPopupExtender ID="mpBuy" runat="server" PopupControlID="BuyPanel"
                                            TargetControlID="btnBuy" CancelControlID="btnBuyClose" BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="width: 600px;
                            height: 370px;">
                            <div id="divService" runat="server">
                            </div>
                            <asp:Button ID="btnClose" runat="server" Text="Close" />
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="width: 600px;
                            height: 370px; overflow-y: scroll;">
                            <div id="divDetails" runat="server" style="overflow-y: scroll;">
                                <asp:DetailsView ID="dvStock" runat="server" Height="100%" Width="100%">
                                </asp:DetailsView>
                            </div>
                            <asp:Button ID="btnDetailsClose" runat="server" Text="Close" />
                        </asp:Panel>
                    </td>
                    <tr>
                        <asp:HiddenField ID="hdnFieldSymbol" runat="server" Value="" />
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="BuyPanel" runat="server" CssClass="modalPopup" align="center" Style="width: 600px;
                                height: 370px;">
                                <h2>
                                    Purchase/ Sell Order
                                </h2>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            Make a Selection
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAction" runat="server" ValidationGroup="Buy" onchange="ddlAction_SelectedIndexChanged();">
                                                <asp:ListItem>No Selection</asp:ListItem>
                                                <asp:ListItem>Purchase</asp:ListItem>
                                                <asp:ListItem>Sell</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBuyStock" runat="server" Text="Stock"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBuyStock" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="49%">
                                            <asp:Label ID="lblBuyVolumes" runat="server" Text="Volumes"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBuyVolume" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rftxtBuyVolume" runat="server" ErrorMessage="*" ForeColor="Red"
                                                ControlToValidate="txtBuyVolume" ValidationGroup="Buy"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBuyPrice" runat="server" Text="Price"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBuyPrice" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rftxtBuyPrice" runat="server" ErrorMessage="*" ForeColor="Red"
                                                ControlToValidate="txtBuyPrice" ValidationGroup="Buy"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBuyDate" runat="server" Text="Purchase Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBuyDateofPurchase" runat="server" Enabled="false" />
                                            <asp:Image ID="imgbuycalendar" runat="server" ImageUrl="~/Images/calendar_year.ico"
                                                Height="100%" Width="23px" />
                                            <ajaxToolkit:CalendarExtender ID="calextbuy" runat="server" TargetControlID="txtBuyDateofPurchase"
                                                PopupButtonID="imgbuycalendar">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rftxtBuyDateofPurchase" runat="server" ErrorMessage="*"
                                                ForeColor="Red" ControlToValidate="txtBuyDateofPurchase" ValidationGroup="Buy"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <caption>
                                        <br />
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnBuyStock" runat="server" OnClick="btnBuyStock_Click" Text="Action"
                                                    ValidationGroup="Buy" Enabled="false" />
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnBuyClose" runat="server" Text="Close" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="Buy" runat="server" DisplayMode="BulletList" EnableClientScript="true"
                                                    HeaderText="Required fields:" ShowSummary="true" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </caption>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            <%--<asp:Panel ID="SellPanel" runat="server" CssClass="modalPopup" align="center" Style="width: 600px;
                                height: 370px;">
                                <h2>
                                    Sell Order
                                </h2>
                                <table width="100%">
                                    <tr>
                                        <td width="49%">
                                            <asp:Label ID="lblSellStock" runat="server" Text="Stock"></asp:Label>
                                        </td>
                                        <td width="49%">
                                            <asp:TextBox ID="txtSellStock" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSellVolume" runat="server" Text="Volumes"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSellVolume" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rftxtSellVolume" runat="server" ErrorMessage="Symbol Required!"
                                                ForeColor="Red" ControlToValidate="txtSellVolume" ValidationGroup="Sell"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSellPrice" runat="server" Text="Price"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSellPrice" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rftxtSellPrice" runat="server" ErrorMessage="Symbol Required!"
                                                ForeColor="Red" ControlToValidate="txtSellPrice" ValidationGroup="Sell"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSellDate" runat="server" Text="Purchase Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDateSell" runat="server" />
                                            <asp:Image ID="imgsellcalender" runat="server" ImageUrl="~/Images/calendar_year.ico"
                                                Height="17px" Width="100%" />
                                            <ajaxToolkit:CalendarExtender ID="calextsell" runat="server" TargetControlID="txtDateSell"
                                                PopupButtonID="imgsellcalender">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rftxtDateSell" runat="server" ErrorMessage="Symbol Required!"
                                                ForeColor="Red" ControlToValidate="txtDateSell" ValidationGroup="Sell"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <caption>
                                        <br />
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnSellStock" runat="server" OnClick="btnSellStock_Click" Text="Sell" />
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnSellClose" runat="server" Text="Close" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="Sell" runat="server" DisplayMode="BulletList" EnableClientScript="true"
                                                    HeaderText="Required fields:" ShowSummary="true" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </caption>
                            </asp:Panel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="100%">
                            <asp:GridView ID="gvPortfolio" runat="server" AutoGenerateColumns="False" DataKeyNames="Symbol"
                                EmptyDataText="" ViewStateMode="Enabled" CellPadding="4" GridLines="Both" Width="100%"
                                ForeColor="#333333" AllowPaging="true" OnRowCommand="gvPortfolio_RowCommand">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Symbol" HeaderText="Symbol" ReadOnly="True" />
                                    <asp:BoundField DataField="avgprice" HeaderText="Unit Price" ReadOnly="True" />
                                    <asp:BoundField DataField="remainingvolume" HeaderText="Volume" ReadOnly="True" />
                                    <asp:BoundField DataField="livePrice" HeaderText="Live Price" ReadOnly="True" />
                                    <asp:BoundField DataField="change" HeaderText="Change" ReadOnly="True" />
                                    <asp:BoundField DataField="Profit" HeaderText="Profit" ReadOnly="True" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelect" runat="server" Text="Select" CommandName="Select"
                                                CommandArgument='<%#Eval("Symbol")+","+Eval("profileID")+","+Eval("portfolioID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <ajaxToolkit:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%" Height="100%">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                Symbol
                                            </td>
                                            <td>
                                                Volume
                                            </td>
                                            <td>
                                                Live Price
                                            </td>
                                            <td>
                                                Avg Price
                                            </td>
                                            <td>
                                                Profit
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "symbol")%>
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "remainingvolume")%>
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "livePrice")%>
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "avgprice")%>
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "profit")%>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    Purchase Details
                                    <asp:GridView ID="gvBuyPortfolio" runat="server" AutoGenerateColumns="False" DataSource='<%# Eval("buyorder") %>'
                                        DataKeyNames="PortfolioID" EmptyDataText="" ViewStateMode="Enabled" CellPadding="4"
                                        ShowHeaderWhenEmpty="true" GridLines="Both" Width="100%" ForeColor="#333333"
                                        AllowPaging="true" ShowFooter="true">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="Volume" HeaderText="Volume" ReadOnly="True" />
                                            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" ReadOnly="True" />
                                            <asp:BoundField DataField="DateofPurchase" HeaderText="DateofPurchase" ReadOnly="True" />
                                            <asp:TemplateField>
                                                <FooterTemplate>
                                                    <asp:Button ID="btnBuyStock" runat="server" Text="Purchase/Sell" />
                                                    <ajaxToolkit:ModalPopupExtender ID="mpbuy" runat="server" PopupControlID="BuyPanel"
                                                        TargetControlID="btnBuyStock" CancelControlID="btnBuyClose" BackgroundCssClass="modalBackground">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                </FooterTemplate>
                                            </asp:TemplateField>
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
                                    Sell Details
                                    <asp:GridView ID="gvSellPortfolio" runat="server" AutoGenerateColumns="False" DataKeyNames="PortfolioID"
                                        DataSource='<%# Eval("sellorder") %>' EmptyDataText="" ViewStateMode="Enabled"
                                        CellPadding="4" GridLines="Both" Width="100%" ShowHeaderWhenEmpty="true" ForeColor="#333333"
                                        AllowPaging="true" ShowFooter="true">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EmptyDataTemplate>
                                            No sell Activity yet
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="Volume" HeaderText="Volume" ReadOnly="True" FooterText="Volume" />
                                            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" ReadOnly="True" />
                                            <asp:BoundField DataField="DateofSell" HeaderText="DateofSell" ReadOnly="True" />
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
                                </ContentTemplate>
                            </ajaxToolkit:Accordion>
                        </td>
                    </tr>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
