<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="FirstMiddleNames.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.NSI.FirstMiddleNames" %>

<%@ Import Namespace="rt.srz.model.srz" %>
<%@ Register Src="~/Controls/CustomPager/Pager.ascx" TagPrefix="uc" TagName="Pager" %>
<%@ Register Src="~/Controls/Administration/SearchByNameControl.ascx" TagName="SearchByNameControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />
  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Имена и отчества" />
  </div>

  <uc:SearchByNameControl ID="searchByNameControl" runat="server" />

  <asp:UpdatePanel ID="MenuUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div class="paddingGridMenu">
        <asp:Menu ID="menu1" runat="server" OnMenuItemClick="menu_MenuItemClick" Orientation="Horizontal" CssClass="ItemMenu" OnPreRender="menu1_PreRender">
          <Items>
            <asp:MenuItem Text="Добавить" Value="Add" ImageUrl="~/Resources/create.png"></asp:MenuItem>
            <asp:MenuItem Text="Открыть" Value="Open" ImageUrl="~/Resources/open.png"></asp:MenuItem>
            <asp:MenuItem Text="Удалить" Value="Delete" ImageUrl="~/Resources/delete.png"></asp:MenuItem>
          </Items>
        </asp:Menu>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="contentUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <asp:GridView Style="width: 100%" ID="grid" runat="server" EnableModelValidation="True"
        AllowSorting="True" AutoGenerateColumns="False" OnSorting="grid_Sorting" OnSelectedIndexChanged="grid_SelectedIndexChanged"
        DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both">
        <HeaderStyle CssClass="GridHeader" />
        <RowStyle CssClass="GridRowStyle" />
        <SelectedRowStyle CssClass="GridSelectedRowStyle" />
        <Columns>
          <asp:CommandField SelectText="Выбор" ShowSelectButton="true" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
            <HeaderStyle CssClass="HideButton" />
            <ItemStyle CssClass="HideButton" />
          </asp:CommandField>
          <asp:TemplateField HeaderText="Тип" SortExpression="Type">
            <ItemTemplate>
              <asp:Label ID="Label1" runat="server" Text='<%# ((AutoComplete)Container.DataItem).Type.Name %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name" />
          <asp:TemplateField HeaderText="Пол" SortExpression="Gender">
            <ItemTemplate>
              <asp:Label ID="Label1" runat="server" Text='<%# ((AutoComplete)Container.DataItem).Gender.Name %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>

      <div class="pagerPadding">
        <uc:Pager ID="custPager" runat="server" OnPageIndexChanged="custPager_PageIndexChanged"
          OnPageSizeChanged="custPager_PageSizeChanged" />
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
