<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="My_Zoho.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
        This website is your automated interface between your Zoho Invoice and Contatax. 
        It enables you to import your data from Zoho Invoice to the My-Zoho.com data repository 
        and your Contatax administratives to download your data to do your accounting.
    </p>
    <p>
        Please, note that registration is not enough to start importing your data. Contatax 
        administratives will need to verify the data you entered and enable your account to
        import the data to our systems.
    </p>
    <p>
        Also note that this is NOT a backup system. You should back up your Zoho Invoice data,
        just as any other data, because this system is not designed to secure your data. Contatax
        will download the data and the data will be deleted once Contatax's done with it. No backup,
        no supervision, no performance optimization, nothing. Only accountable data to be accounted
        and then no data at all.
    </p>
    <p>
        This service is completely and fully tied to the conditions of service of Contatax.com. If 
        you have not been informed of them, please contact with Contatax.com to be informed.
    </p>
</asp:Content>
