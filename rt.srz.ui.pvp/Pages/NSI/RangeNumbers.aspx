<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="RangeNumbers.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.NSI.RangeNumbers" %>

<%@ Import Namespace="rt.srz.model.srz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />
  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Диапазоны номеров вс" />
  </div>

  <asp:UpdatePanel ID="MenuUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div class="paddingGridMenu">
        <asp:Menu ID="menu1" runat="server" OnMenuItemClick="menu_MenuItemClick" Orientation="Horizontal" CssClass="ItemMenu" OnPreRender="menu1_PreRender">
          <Items>
            <%--<asp:MenuItem Text="Добавить" Value="Add" ImageUrl="~/Resources/create.png"></asp:MenuItem>--%>
            <asp:MenuItem Text="Открыть" Value="Open" ImageUrl="~/Resources/open.png"></asp:MenuItem>
            <%--            <asp:MenuItem Text="Удалить" Value="Delete" ImageUrl="~/Resources/delete.png"></asp:MenuItem>--%>
          </Items>
        </asp:Menu>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="contentUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <asp:GridView Style="width: 100%" ID="grid" runat="server" EnableModelValidation="True"
        AllowSorting="True" AutoGenerateColumns="False" OnSelectedIndexChanged="grid_SelectedIndexChanged"
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
          <asp:TemplateField HeaderText="СМО">
            <ItemTemplate>
              <asp:Label ID="Label1" runat="server" Text='<%# ((RangeNumber)Container.DataItem).Smo.ShortName %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="RangelFrom" HeaderText="c" />
          <asp:BoundField DataField="RangelTo" HeaderText="по" />
          <asp:TemplateField HeaderText="Дата">
            <ItemTemplate>
              <asp:Label ID="Label2" runat="server" Text='<%# ((RangeNumber)Container.DataItem).ChangeDate.HasValue ?  ((RangeNumber)Container.DataItem).ChangeDate.Value.ToShortDateString() : null %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
