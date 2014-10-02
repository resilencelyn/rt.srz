<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignRolesToGroupControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AssignRolesToGroupControl" %>

<div class="headerTitles">
  <asp:Label ID="lbTitle" runat="server" Text="Назначение ролей"></asp:Label>
</div>
<div class="partPadding">
  <asp:CheckBoxList ID="cblRoles" runat="server" DataTextField="Name" DataValueField="Id"></asp:CheckBoxList>
</div>
