<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignPermissionsToRoleControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AssignPermissionsToRoleControl" %>

<div class="headerTitles">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление разрешений для роли" ></asp:Label>
</div>
<div class="partPadding">
  <asp:CheckBoxList ID="cblPermissions" runat="server" DataTextField="Name" DataValueField="id"></asp:CheckBoxList>
</div>


