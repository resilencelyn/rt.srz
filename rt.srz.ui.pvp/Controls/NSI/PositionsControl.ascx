<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PositionsControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.NSI.PositionsControl" %>

<%@ Register TagPrefix="uc" TagName="PositionControl" Src="~/Controls/NSI/PositionControl.ascx" %>

<div style="text-align: center;">
  <div class="headerSubTitles">
    <asp:Label ID="Label1" runat="server" Text="Координаты полей временного свидетельства в миллиметрах"></asp:Label>
  </div>

  <div style="clear: both">

    <div class="admLabelControl1_4">
      <div class="admControlPadding">
      </div>
    </div>
    <div class="admLabelControl1_4">
      <div class="admControlPadding">
        <asp:Label ID="Label3" runat="server" Text="Отступ слева"></asp:Label>
      </div>
    </div>
    <div class="admLabelControl1_4">
      <div class="admControlPadding">
        <asp:Label ID="Label2" runat="server" Text="Отступ сверху"></asp:Label>
      </div>
    </div>
    <div class="admLabelControl1_4">
      <div class="admControlPadding">
        <asp:Label ID="Label4" runat="server" Text="Ширина поля"></asp:Label>
      </div>
    </div>
  </div>

  <uc:PositionControl runat="server" ID="controlSmo" />
  <uc:PositionControl runat="server" ID="controlAddress" />
  <uc:PositionControl runat="server" ID="controlDay1" />
  <uc:PositionControl runat="server" ID="controlMonth1" />
  <uc:PositionControl runat="server" ID="controlYear1" />
  <uc:PositionControl runat="server" ID="controlBirthPlace" />
  <uc:PositionControl runat="server" ID="controlMale" />
  <uc:PositionControl runat="server" ID="controlFemale" />
  <uc:PositionControl runat="server" ID="controlDay2" />
  <uc:PositionControl runat="server" ID="controlMonth2" />
  <uc:PositionControl runat="server" ID="controlYear2" />
  <uc:PositionControl runat="server" ID="controlFio" />
  <uc:PositionControl runat="server" ID="controlLine1" />
  <uc:PositionControl runat="server" ID="controlLine2" />
  <uc:PositionControl runat="server" ID="controlLine3" />

  <div style="clear: both;">
  </div>

</div>

