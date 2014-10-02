<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignRolesToUserControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AssignRolesToUserControl" %>

<div class="headerTitles">
  <asp:Label ID="lbTitle" runat="server" Text="Назначение ролей" ></asp:Label>
</div>
<div class="partPadding">
  <asp:CheckBoxList ID="cblRoles" runat="server" DataTextField="Name" DataValueField="Id"></asp:CheckBoxList>
</div>
