<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="eTrade.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
        <ajaxToolkit:ToolkitScriptManager runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />
        <!-- ModalPopupExtender -->
        <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
            <div style="height: 100px">
                Do you like this product?&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                    <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnClose" runat="server" Text="Close" />
        </asp:Panel>
    </p>
</asp:Content>
