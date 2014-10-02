<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmosControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.SmosControl" %>
<%@ Import Namespace="rt.srz.model.srz" %>

<%@ Register Src="~/Controls/CustomPager/Pager.ascx" TagPrefix="uc" TagName="Pager" %>
<%@ Register Src="SearchByNameControl.ascx" TagName="SearchByNameControl" TagPrefix="uc" %>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Реестр страховых медицинских организаций" />
</div>

<uc:SearchByNameControl ID="searchByNameControl" runat="server" />

<asp:UpdatePanel ID="MenuUpdatePanel" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <div class="paddingGridMenu">
      <asp:Menu ID="menu1" runat="server" OnMenuItemClick="menu_MenuItemClick" Orientation="Horizontal" CssClass="ItemMenu" OnPreRender="menu1_PreRender">
        <Items>
          <%--<asp:MenuItem Text="Добавить" Value="Add" ImageUrl="~/Resources/create.png"></asp:MenuItem>--%>
          <asp:MenuItem Text="Открыть" Value="Open" ImageUrl="~/Resources/open.png"></asp:MenuItem>
          <%--              <asp:MenuItem Text="Удалить" Value="Delete" ImageUrl="~/Resources/delete.png"></asp:MenuItem>--%>
        </Items>
      </asp:Menu>
    </div>
  </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="contentUpdatePanel" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <asp:GridView Style="width: 100%" ID="smosGridView" runat="server" EnableModelValidation="True" OnRowCommand="smosGridView_RowCommand"
      AllowSorting="True" AutoGenerateColumns="False" OnSorting="smosGridView_Sorting" OnRowDeleting="smoGridView_Deleting" OnSelectedIndexChanged="smosGridView_SelectedIndexChanged"
      DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both">
      <HeaderStyle CssClass="GridHeader" />
      <RowStyle CssClass="GridRowStyle" />
      <SelectedRowStyle CssClass="GridSelectedRowStyle" />
      <Columns>
        <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton"></asp:ButtonField>
        <asp:CommandField SelectText="Выбор" ShowSelectButton="true" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
          <HeaderStyle CssClass="HideButton" />
          <ItemStyle CssClass="HideButton" />
        </asp:CommandField>
        <asp:TemplateField HeaderText="Территориальный фонд" SortExpression="TFom">
          <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# ((Organisation)Container.DataItem).Parent.ShortName %>'></asp:Label>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ShortName" HeaderText="Краткое название" SortExpression="ShortName" />
        <asp:BoundField DataField="FullName" HeaderText="Полное название" SortExpression="FullName" />
        <asp:BoundField DataField="LastName" HeaderText="Фамилия" />
        <asp:BoundField DataField="FirstName" HeaderText="Имя" />
        <asp:BoundField DataField="MiddleName" HeaderText="Отчество" />
        <asp:BoundField DataField="Phone" HeaderText="Телефон" />
        <asp:BoundField DataField="Fax" HeaderText="Факс" />
      </Columns>
    </asp:GridView>

    <div class="pagerPadding">
      <uc:Pager ID="custPager" runat="server" OnPageIndexChanged="custPager_PageIndexChanged"
        OnPageSizeChanged="custPager_PageSizeChanged" />
    </div>
  </ContentTemplate>
</asp:UpdatePanel>


