<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplatesVsControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.NSI.TemplatesVsControl" %>

<%@ Register Src="~/Controls/Common/ConfirmControl.ascx" TagName="ConfirmControl" TagPrefix="uc" %>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Настройка шаблонов печати временных свидетельств"></asp:Label>
</div>

<uc:ConfirmControl ID="messageCantDeleteTemplate" runat="server" Message="На шаблон ссылаются интервалы. Удаление невозможно." ConfirmMode="Close" />

<asp:UpdatePanel ID="MenuUpdatePanel" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <div class="paddingGridMenu">
      <asp:Menu ID="menu1" runat="server" OnMenuItemClick="menu1_MenuItemClick" Orientation="Horizontal" CssClass="ItemMenu" OnPreRender="menu1_PreRender">
        <Items>
          <asp:MenuItem Text="Создать" Value="Add" ImageUrl="~/Resources/create.png"></asp:MenuItem>
          <asp:MenuItem Text="Скопировать" Value="Copy" ImageUrl="~/Resources/create.png"></asp:MenuItem>
          <asp:MenuItem Text="Открыть" Value="Open" ImageUrl="~/Resources/open.png"></asp:MenuItem>
          <asp:MenuItem Text="Удалить" Value="Delete" ImageUrl="~/Resources/delete.png"></asp:MenuItem>
        </Items>
      </asp:Menu>
    </div>
  </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="contentUpdatePanel" runat="server">
  <ContentTemplate>
    <asp:GridView Style="width: 100%" ID="grid" runat="server" EnableModelValidation="True"
      AllowSorting="True" AutoGenerateColumns="False" OnRowDeleting="grid_RowDeleting" OnSelectedIndexChanged="grid_SelectedIndexChanged"
      DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both" OnRowCommand="grid_RowCommand">
      <HeaderStyle CssClass="GridHeader" />
      <RowStyle CssClass="GridRowStyle" />
      <SelectedRowStyle CssClass="GridSelectedRowStyle" />
      <Columns>
        <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton"></asp:ButtonField>
        <asp:CommandField SelectText="Выбор" ShowSelectButton="true" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
          <HeaderStyle CssClass="HideButton" />
          <ItemStyle CssClass="HideButton" />
        </asp:CommandField>
        <asp:BoundField DataField="Name" HeaderText="Наименование шаблона" />
        <asp:TemplateField HeaderText="По умолчанию">
          <%--              <HeaderStyle CssClass="CheckHeader" />
              <ItemStyle CssClass="CheckItem" />--%>
          <ItemTemplate>
            <asp:CheckBox ID="cbByDefault" runat="server" Checked='<%# ((rt.srz.model.srz.Template)Container.DataItem).Default %>' Enabled="false"></asp:CheckBox>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>

  </ContentTemplate>
</asp:UpdatePanel>


