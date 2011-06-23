﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RequestConnection.aspx.cs" Inherits="My_Zoho.User.RequestConnection"
    ValidateRequest="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        This is the place to request some Zoho Invoice accounts to be included in your import
        sessions.
    </p>
    <p>
        Please review your current connections, your pending connections and add new pending
        connections if you need to.
    </p>
    <asp:UpdatePanel ID="updMain" runat="server" RenderMode="Block" UpdateMode="Always">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <asp:TabPanel runat="server" ID="tpCurrentConnections">
                    <HeaderTemplate>
                        Current connections
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Label ID="lblCurrentConnections" runat="server"></asp:Label>
                        <asp:GridView ID="dgvCurrentConnections" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            Width="99%">
                            <Columns>
                                <asp:BoundField DataField="idConnection" HeaderText="idConnection" SortExpression="idConnection"
                                    Visible="False" />
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                <asp:BoundField DataField="ApiKey" HeaderText="ApiKey" SortExpression="ApiKey" />
                                <asp:BoundField DataField="AuthToken" HeaderText="AuthToken" SortExpression="AuthToken" />
                                <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tpRequestedConnections">
                    <HeaderTemplate>
                        Requested connections
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Label ID="lblPendingConnections" runat="server"></asp:Label>
                        <asp:GridView ID="dgvRequestedConections" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnRowDeleting="dgvRequestedConections_RowDeleting" OnRowEditing="dgvRequestedConections_RowEditing"
                            OnRowUpdating="dgvRequestedConections_RowUpdating" OnRowCancelingEdit="dgvRequestedConections_RowCancelingEdit"
                            DataKeyNames="iduser_reqestedConnection" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True">
                            <Columns>
                                <asp:BoundField DataField="iduser_reqestedConnection" HeaderText="iduser_reqestedConnection"
                                    SortExpression="iduser_reqestedConnection" Visible="False" ReadOnly="True" />
                                <asp:BoundField DataField="userid" HeaderText="userid" SortExpression="user" Visible="False" />
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                <asp:BoundField DataField="ApiKey" HeaderText="ApiKey" SortExpression="ApiKey" />
                                <asp:BoundField DataField="AuthToken" HeaderText="AuthToken" SortExpression="AuthToken" />
                                <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnAddRequestedConnection" runat="server" Text="New Request" OnClick="btnAddRequestedConnection_Click" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>