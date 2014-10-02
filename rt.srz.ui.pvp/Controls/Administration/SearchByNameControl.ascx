<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchByNameControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.SearchByNameControl" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <%-- для того чтобы работал Submit Button нормально нужна панель, 
				иначе второй контрол на странице(например группы) при нажатии в нем на контроле с критерием поиска ввода - срабатывает кнопка из первого контрола--%>
    <asp:Panel ID="SearchPanel" runat="server" DefaultButton="btnSearch">

        <div class="admSearchByNameLabel">
          <div class="admSearchByNamePaddingControl">
            <asp:Label ID="lbName" runat="server" Text="Название"></asp:Label>
          </div>
        </div>
        <div class="admSearchByNameValue">
          <div class="admSearchByNamePaddingControl">
            <asp:TextBox ID="tbName" runat="server" MaxLength="50" Width="100%" CssClass="controlBoxes"></asp:TextBox>
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

