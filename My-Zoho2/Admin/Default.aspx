<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="My_Zoho.Admin.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        From here you can administrate the requests and download the submitted data.
    </p>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel ID="tpRequestedConnections" runat="server">
            <HeaderTemplate>
                Requested connections
            </HeaderTemplate>
            <ContentTemplate>
                <asp:Label ID="lblPendingConnections" runat="server"></asp:Label>
                <asp:GridView ID="dgvRequestedConections" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDeleting="dgvRequestedConections_RowDeleting" OnSelectedIndexChanged="dgvRequestedConections_SelectedIndexChanged"
                    DataKeyNames="iduser_reqestedConnection">
                    <Columns>
                        <asp:CommandField SelectText="Confirm" ShowSelectButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="iduser_reqestedConnection" HeaderText="iduser_reqestedConnection"
                            SortExpression="iduser_reqestedConnection" Visible="False" ReadOnly="True" />
                        <asp:TemplateField HeaderText="userid" SortExpression="user">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlUserId" runat="server" SelectedValue='<%# Bind("userid") %>'
                                    DataSourceID="sdsUsuarios" DataTextField="userName" DataValueField="userid">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlUserId" runat="server" SelectedValue='<%# Bind("userid") %>'
                                    DataSourceID="sdsUsuarios" DataTextField="userName" DataValueField="userid" Enabled="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="ApiKey" HeaderText="ApiKey" SortExpression="ApiKey" />
                        <asp:BoundField DataField="AuthToken" HeaderText="AuthToken" SortExpression="AuthToken" />
                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsUsuarios" runat="server" SelectCommand="SELECT userid, userName, password, signedUp, lastLogIn, failedTries FROM `user`"
                    ConnectionString="<%$ ConnectionStrings:My_Zoho.Properties.Settings.ConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:My_Zoho.Properties.Settings.ConnectionString.ProviderName %>">
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="tpUsuarios">
            <HeaderTemplate>
                Users
            </HeaderTemplate>
            <ContentTemplate>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
