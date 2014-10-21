<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Common.ConfirmControl" %>

<asp:Button ID="btnFake" runat="server" CssClass="HideControl" />
<ajaxToolkit:ModalPopupExtender ID="extender" runat="server"
  TargetControlID="btnFake"
  PopupControlID="deleteConfirmDiv"
  OkControlID="btnConfirm"
  CancelControlID="btnCancel"
  BackgroundCssClass="confirmDeleteBackground">
</ajaxToolkit:ModalPopupExtender>

<div runat="server" ID="deleteConfirmDiv"  class="confirmBorder">

  <div class="confirmHeader">
    <div style="vertical-align: central; padding: 3px">
      <asp:Label ID="Label1" runat="server" Text="Сообщение" />
    </div>
  </div>

  <div class="confirmDelete">
    <div style="padding: 7px;">
      <asp:Label ID="lbMessage" runat="server" Text="Вы уверены, что хотите удалить выбранную запись?" />
    </div>
    <hr />
    <div style="clear: both; text-align: right">
      <asp:Button ID="btnConfirm" Text="Подтвердить" runat="server" CssClass="buttons" />
      <asp:Button ID="btnCancel" Text="Отменить" runat="server" CssClass="buttons" />
    </div>
  </div>
</div>



