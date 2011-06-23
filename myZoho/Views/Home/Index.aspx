<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: myZoho.Properties.Resources.WelcomeTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: myZoho.Properties.Resources.WelcomeMessage %></h2>
    <p>
    <%: myZoho.Properties.Resources.WelcomeContent %>
    </p>
</asp:Content>
