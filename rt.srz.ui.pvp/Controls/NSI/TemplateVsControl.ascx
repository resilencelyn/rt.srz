<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateVsControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.NSI.TemplateVsControl" %>

<%@ Register TagPrefix="uc" TagName="PositionsControl" Src="~/Controls/NSI/PositionsControl.ascx" %>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление шаблона печати"></asp:Label>
</div>

<div style="clear: both;">
  <div style="float: left; width: 30%; text-align: right">
    <div class="admControlPadding">
      <asp:Label ID="lbName" runat="server" Text="Наименование шаблона печати"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbName" runat="server" Width="100%" CssClass="controlBoxes" MaxLength="500"></asp:TextBox>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admControlPadding">
    <uc:PositionsControl runat="server" ID="positionsControl" />
  </div>
</div>

<div style="clear: both;">
  <div class="admControlPadding">
    <asp:CheckBox ID="cbByDefault" runat="server" Text="Использовать по умолчанию при печати всех ВС" />
  </div>
</div>

