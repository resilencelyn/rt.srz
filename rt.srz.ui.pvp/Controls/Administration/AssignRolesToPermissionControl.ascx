<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignRolesToPermissionControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AssignRolesToPermissionControl" %>

<div class="headerTitles">
  <asp:Label ID="lbTitle" runat="server" Text="Назначение ролей" ></asp:Label>
</div>
<div class="partPadding">
  <asp:CheckBoxList ID="cblRoles" runat="server" DataTextField="Name" DataValueField="id"></asp:CheckBoxList>
</div>
