<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Installation.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Installation" %>

<div style="width: 100%;">
  <div style="border: none; padding-left: 25px;">
    <asp:Label ID="Label9" runat="server" Text="Первичная установка приложения" Font-Bold="False" Font-Size="XX-Large" />
  </div>
  <br />
  <div style="border: none; padding-left: 25px;">
    <asp:Label ID="Label1" runat="server" Text="1) Для загрузки данных из БД СРЗ Атлантика необходимо создать задания по 5 тыс записей" Font-Bold="False" Font-Size="Large" />

  </div>
  <div style="border: none; padding-left: 25px;">
    <asp:Label ID="Label2" runat="server" Text="(ход выполнения загрузки можно наблюдать в планировщике текущих задач)" Font-Bold="False" Font-Size="Medium" />
  </div>
  <div style="border: none; margin-top: 1px; padding-left: 25px;">
    <asp:Button ID="btnLoadAtlantika" runat="server" Text="Создать задание на первичную загрузку из СРЗ Атлантика" Width="350"
      CausesValidation="false"
      OnClientClick=" return confirm('Вы уверены, что хотите создать задание?'); "
      OnClick="BtnLoadAtlantikaClick" CssClass="buttons"/>
  </div>
  <br />
  <div style="border: none; padding-left: 25px;">
    <asp:Label ID="Label3" runat="server" Text="2) Необходимо просмотреть статистику ошибок первичнй загрузки" Font-Bold="False" Font-Size="Large" />
  </div>
  <div style="border: none; margin-top: 1px; padding-left: 25px;">
    <asp:Button ID="btnStatistic" runat="server" Text="Статистика первичной загрузки" Width="350"
      CausesValidation="false"
      OnClientClick=" return confirm('Вы уверены, что хотите просмотреть статистику первичной загрузки?'); "
      OnClick="BtnStatisticClick" CssClass="buttons"/> 
  </div>
  <br />
  <div style="border: none; padding-left: 25px;">
    <asp:Label ID="Label4" runat="server" Text="3) Если первичная загрузка закончена, то завершаем установку" Font-Bold="False" Font-Size="Large" />
    </div>
    <div style="border: none; padding-left: 25px;">
    <asp:Label ID="Label17" runat="server" Text="(пункт меню Администрирование\Установка больше виден не будет)" Font-Bold="False" Font-Size="Medium" />
  </div>
  <div style="border: none; margin-top: 1px; padding-left: 25px;">
    <asp:Button ID="btnCompleteTheInstallation" runat="server" Text="Установка завершена" Width="350"
      CausesValidation="false"
      OnClientClick=" return confirm('Вы уверены, что хотите завершить установку?'); "
      OnClick="BtnCompleteTheInstallationClick" CssClass="buttons"/>
  </div>

</div>
