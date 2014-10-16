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
                    </td>
                    <td>
                        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </ajaxToolkit:ToolkitScriptManager>
                        <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />
                        <!-- ModalPopupExtender -->
                        <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
                            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                            <div>
                                <asp:DetailsView ID="dvWatchList" runat="server" AutoGenerateRows="true" Height="50px"
                                    Width="125px">
                                </asp:DetailsView>
                            </div>
                            <asp:Button ID="btnClose" runat="server" Text="Close" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
