﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PermissionsControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.PermissionsControl" %>

<%@ Register Src="SearchByNameControl.ascx" TagName="SearchByNameControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Common/ConfirmControl.ascx" TagName="ConfirmControl" TagPrefix="uc" %>

<div class="formTitle">
  <asp:Label ID="Label9" runat="server" Text="Разрешения" />
</div>

<uc:SearchByNameControl ID="searchByNameControl" runat="server" />

<uc:ConfirmControl ID="confirmDelete" runat="server" />

<asp:UpdatePanel ID="MenuUpdatePanel" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <div class="paddingGridMenu">
      <asp:Menu ID="menu1" runat="server" OnMenuItemClick="menu1_MenuItemClick" Orientation="Horizontal" CssClass="ItemMenu" OnPreRender="menu1_PreRender">
        <Items>
          <asp:MenuItem Text="Добавить" Value="Add" ImageUrl="~/Resources/create.png"></asp:MenuItem>
          <asp:MenuItem Text="Добавить расширенно" Value="AddEx" ImageUrl="~/Resources/create.png"></asp:MenuItem>
          <asp:MenuItem Text="Открыть" Value="Open" ImageUrl="~/Resources/open.png"></asp:MenuItem>
          <asp:MenuItem Text="Назначить роли для разрешения" Value="AssignRoles" ImageUrl="~/Resources/open.png"></asp:MenuItem>
          <asp:MenuItem Text="Удалить" Value="Delete" ImageUrl="~/Resources/delete.png"></asp:MenuItem>
        </Items>
      </asp:Menu>
    </div>
  </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="contentUpdatePanel" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <div class="partHeight">
      <asp:ListBox ID="lstPermission" runat="server" AutoPostBack="True" DataTextField="Name" DataValueField="Id" Width="100%" Height="100%" CssClass="listbox"></asp:ListBox>
    </div>
  </ContentTemplate>
</asp:UpdatePanel>
