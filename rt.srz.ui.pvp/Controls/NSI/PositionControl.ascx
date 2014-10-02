<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PositionControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.NSI.PositionControl" %>

<div style="clear: both;">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="lbTitle" runat="server" Text="title"></asp:Label>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbLeft" runat="server" Width="100%" CssClass="rangeNum"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbBottom" runat="server" Width="100%" CssClass="rangeNum"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbWidth" runat="server" Width="100%" CssClass="rangeNum"></asp:TextBox>
    </div>
  </div>
  <div style="float: left; width: 40%">
    <div class="admControlPadding">
        <asp:CustomValidator ID="cv" runat="server" Text="Лево в сумме с шириной не должны превышать 210 мм!"
          OnServerValidate="cv_ServerValidate" CssClass="errorMessage" />
    </div>
  </div>
</div>


