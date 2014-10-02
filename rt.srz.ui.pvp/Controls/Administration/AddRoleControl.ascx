<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddRoleControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AddRoleControl" %>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление роли" CssClass="formTitle"></asp:Label>
</div>

<div style="clear: both;">
  <div class="admLabelControl1Fix">
    <div class="admControlPadding">
      <asp:Label ID="lbName" runat="server" Text="Название"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbName" runat="server" MaxLength="50" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="errorMessage">
    <div class="admfloatLeft">
      <asp:RequiredFieldValidator ID="rfName" runat="server" Text="Укажите название!" ControlToValidate="tbName" />
    </div>
  </div>
</div>
<div style="clear: both">
</div>
