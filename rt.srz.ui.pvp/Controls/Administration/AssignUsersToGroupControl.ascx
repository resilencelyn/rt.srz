<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignUsersToGroupControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AssignUsersToGroupControl" %>

<div class="headerTitles">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление пользователей в группу"></asp:Label>
</div>
<div class="partPadding">
  <asp:CheckBoxList ID="cblUsers" runat="server" DataTextField="Login" DataValueField="id"></asp:CheckBoxList>
</div>
