<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="My_Zoho.Account.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Change Password
    </h2>
    <p>
        Use the form below to change your password.
    </p>
    <p>
        <fieldset id="pnlLogin">
            <legend>Please, enter the required fields</legend>
            <asp:Label runat="server" ID="lblCurrentPasword" Text="Current Password: " CssClass="column"></asp:Label>
            <asp:TextBox runat="server" ID="txtCurrentPassword" CssClass="column" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ErrorMessage="The current password is mandatory" Text="*" ControlToValidate="txtCurrentPassword"></asp:RequiredFieldValidator>
            <br class="clear" />
            <asp:Label runat="server" ID="lblNewPassword" Text="New Password: " CssClass="column"></asp:Label>
            <asp:TextBox runat="server" ID="txtNewPassword" CssClass="column" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="The new password is mandatory" Text="*" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
            <br class="clear" />
            <asp:Label runat="server" ID="lblConfirmPassword" Text="Confirm Password: " CssClass="column"></asp:Label>
            <asp:TextBox runat="server" ID="txtConfirmPassword" CssClass="column" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="The confirmation password is mandatory" Text="*" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPasswords" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="The new and confirm password do not match" Text="*"></asp:CompareValidator>
            <br class="clear" />
            <br />
            <asp:Button runat="server" ID="btnOk" Text="OK" CssClass="column" OnClick="btnOk_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="column" OnClick="btnCancel_Click" />
            <br class="clear" />
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </fieldset>
    </p>
</asp:Content>
