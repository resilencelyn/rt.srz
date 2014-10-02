<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchByDatesControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Common.SearchByDatesControl" %>

<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
    <%-- для того чтобы работал Submit Button нормально нужна панель, 
				иначе второй контрол на странице(например группы) при нажатии в нем на контроле с критерием поиска ввода - срабатывает кнопка из первого контрола--%>
    <asp:Panel ID="SearchPanel" runat="server" DefaultButton="btnSearch">

      <div class="admSearchByNameLabel">
        <div class="admSearchByNamePaddingControl">      
          <asp:Label ID="lbName" runat="server" Text="Дата"></asp:Label>
        </div>
      </div>
      <div style="float: left; width: 120px">
        <div class="admSearchByNamePaddingControl">
          <uc:DateBox runat="server" ID="tbDateFrom" Width="100%" CssClass="controlBoxes" />
        </div>
      </div>
      <div style="float: left; width: 10px; text-align:center">
        <div class="admSearchByNamePaddingControl">      
          <asp:Label ID="Label1" runat="server" Text="-"></asp:Label>
        </div>
      </div>
      <div style="float: left; width: 120px">
        <div class="admSearchByNamePaddingControl">
          <uc:DateBox runat="server" ID="tbDateTo" Width="100%" CssClass="controlBoxes" />
        </div>
      </div>
      <div class="admfloatLeft">
        <div class="admSearchByNamePaddingControl">
          <asp:Button ID="btnClear" runat="server" Text="Очистить" Width="80px" OnClick="btnClear_Click" UseSubmitBehavior="False" CssClass="buttons" />
          <asp:Button ID="btnSearch" runat="server" Text="Поиск" Width="80px" OnClick="btnSearch_Click" CssClass="buttons" />
        </div>
      </div>

    </asp:Panel>
  </ContentTemplate>
</asp:UpdatePanel>

