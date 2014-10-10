<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignPdpToUserControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AssignPdpToUserControl" %>

<div class="headerTitles">
  <asp:Label ID="lbTitle" runat="server" Text="Назначение пункта выдачи" ></asp:Label>
</div>

<div style="clear: both;">
  <div class="admLabelControl2Fix">
    <div class="admControlPadding">
      <asp:Label ID="Label3" runat="server" Text="Территориальный фонд" />
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:DropDownList ID="dlTFoms" runat="server" DataTextField="ShortName" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="DlTFomsSelectedIndexChanged" CssClass="dropDowns" Width="100%"/>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl2Fix">
    <div class="admControlPadding">
      <asp:Label ID="Label1" runat="server" Text="СМО" />
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:DropDownList ID="dlSmo" runat="server" DataTextField="ShortName" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="DlSmoSelectedIndexChanged" CssClass="dropDowns" Width="100%"/>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl2Fix">
    <div class="admControlPadding">
      <asp:Label ID="Label2" runat="server" Text="Пункт выдачи" />
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:DropDownList ID="dlPdp" runat="server" DataTextField="ShortName" DataValueField="id" CssClass="dropDowns" Width="100%"/>
    </div>
  </div>
  <div class="errorMessage">
    <div class="admControlPadding">
      <asp:CustomValidator ID="rfPoint" runat="server" ErrorMessage="Не заполнен пункт выдачи!" OnServerValidate="RfPointServerValidate"></asp:CustomValidator>
    </div>
  </div>
</div>

<div style="clear: both">
</div>


