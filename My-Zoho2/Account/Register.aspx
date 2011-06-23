<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="My_Zoho.Account.Register" ClientIDMode="Static" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script src="/Scripts/Register.js" type="text/javascript"></script>
    <fieldset id="pnlRegister">
        <legend>Please, fill in the required information.</legend>
        <asp:Label runat="server" ID="lblUserName" Text="User name:" CssClass="column"></asp:Label>
        <asp:TextBox runat="server" ID="txtUserName" CssClass="column" MaxLength="20"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvUserName" ControlToValidate="txtUserName"
            ErrorMessage="The user name is mandatory" Text="*" Style="float:left"></asp:RequiredFieldValidator>
        <span id="spnUserNameAvaliable" CssClass="column"></span><br class="nofloat" />
        <asp:Label runat="server" ID="lblPassword" Text="Password: " CssClass="column"></asp:Label>
        <asp:TextBox runat="server" ID="txtPassword" CssClass="column" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfvPassword" ControlToValidate="txtPassword" Text="*" Style="float:left" ErrorMessage="The password is mandatory"></asp:RequiredFieldValidator>
        <br class="nofloat" />
        <br />
        <asp:Button runat="server" ID="btnOK" Text="OK" Enabled="false" CssClass="column"
            onclick="btnOK_Click" /><asp:Button 
            runat="server" ID="btnCancel" Text="Cancel" CausesValidation="False" CssClass="column" OnClick="btnCancel_Click" /><br class="nofloat" />
        <asp:Label id="lblError" runat="server" ForeColor="Red"></asp:Label>
    </fieldset>
</asp:Content>
