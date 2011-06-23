<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="My_Zoho.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.
    </p>
    <p>
        <fieldset id="pnlLogin">
            <legend>Please, enter your login information</legend>
            <asp:Label runat="server" ID="lblUserName" Text="User name" CssClass="column"></asp:Label>
            <asp:Textbox runat="server" ID="txtUserName" CssClass="column"></asp:Textbox><br class="clear" />
            <asp:Label runat="server" ID="lblPassword" Text="Password" CssClass="column"></asp:Label>
            <asp:TextBox runat="server" ID="txtPassword" CssClass="column" TextMode="Password"></asp:TextBox> <br class="clear" />
            <br />
            <asp:Button runat="server" ID="btnOk" Text="OK" CssClass="column" 
                onclick="btnOk_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="column" onclick="btnCancel_Click" />
        </fieldset>
    </p>    
</asp:Content>
