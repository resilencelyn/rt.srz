<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignGroupsToUserControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AssignGroupsToUserControl" %>

<div class="headerTitles">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление в группы"></asp:Label>
</div>
<div class="partPadding">
  <asp:CheckBoxList ID="cblGroups" runat="server" DataTextField="Name" DataValueField="Id"></asp:CheckBoxList>
</div>

