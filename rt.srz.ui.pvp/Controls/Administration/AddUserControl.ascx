<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUserControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AddUserControl" %>

<div class="headerTitles">
  <asp:Label ID="lbTitle" runat="server" Text="Реквизиты пользователя"></asp:Label>
</div>

<div style="clear: both;">
  <div class="admLabelControl1Fix">
    <div class="admControlPadding">
      <asp:Label ID="lbName" runat="server" Text="Логин"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbLogin" runat="server" MaxLength="50" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="errorMessage">
    <div class="admfloatLeft">
      <asp:RequiredFieldValidator ID="rfName" runat="server" Text="Укажите логин!" ControlToValidate="tbLogin" />
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl1Fix">
    <div class="admControlPadding">
      <asp:Label ID="lbPassowrd" runat="server" Text="Пароль"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="errorMessage">
    <div class="admfloatLeft">
      <asp:RequiredFieldValidator ID="rfPassword" runat="server" Text="Укажите пароль!" ControlToValidate="tbPassword" />
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl1Fix">
    <div class="admControlPadding">
      <asp:Label ID="lbEmail" runat="server" Text="Email"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbEmail" runat="server" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="errorMessage">
    <div class="admfloatLeft">
      <asp:RegularExpressionValidator ID="vEmail" runat="server"
        ControlToValidate="tbEmail" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
        ErrorMessage="Неверный формат E-mail" />
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl1Fix">
    <div class="admControlPadding">
      <asp:Label ID="Label1" runat="server" Text="ФИО"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbFio" runat="server" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
</div>
<div style="clear: both">
</div>

