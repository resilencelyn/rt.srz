<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchKeyTypesControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Twins.SearchKeyTypesControl" %>

<%@ Import Namespace="rt.srz.model.srz" %>

<script type="text/javascript">

  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_endRequest(function (s, e) {
    EndRequest(s, e, "<%=scrollArea.ClientID%>", "<%=hfScrollPosition.ClientID%>");
  });

  window.onload = function () {
    OnLoad("<%=scrollArea.ClientID%>", "<%=hfScrollPosition.ClientID%>");
  }

  function SetDivPosition() {
    SetDivPos("<%=scrollArea.ClientID%>", "<%=hfScrollPosition.ClientID%>");
  }

</script>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Ключи поиска" />
</div>

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

    <asp:HiddenField ID="hfScrollPosition" runat="server" Value="0" />
    <div runat="server" id="scrollArea" onscroll="SetDivPosition()" style="height: 700px; overflow: auto;">

      <asp:GridView Style="width: 100%" ID="grid" runat="server" EnableModelValidation="True"
        AllowSorting="True" AutoGenerateColumns="False" OnRowDeleting="grid_RowDeleting" OnSelectedIndexChanged="grid_SelectedIndexChanged" OnRowDataBound="grid_RowDataBound"
        DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both">
        <HeaderStyle CssClass="GridHeader" />
        <RowStyle CssClass="GridRowStyle" />
        <SelectedRowStyle CssClass="GridSelectedRowStyle" />
        <Columns>
          <asp:CommandField SelectText="Выбор" ShowSelectButton="true" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
            <HeaderStyle CssClass="HideButton" />
            <ItemStyle CssClass="HideButton" />
          </asp:CommandField>
          <asp:TemplateField HeaderText="Пересчитан">
            <ItemTemplate>
              <asp:CheckBox ID="cbCheck" runat="server" Enabled="false" Checked='<%# ((SearchKeyType)Container.DataItem).Recalculated %>'></asp:CheckBox>
              <asp:HiddenField ID="hfom" runat="server" Value='<%# ((SearchKeyType)Container.DataItem).Tfoms != null ?  ((SearchKeyType)Container.DataItem).Tfoms.Id.ToString() : null %>' />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="Name" HeaderText="Наименование" />
        </Columns>
      </asp:GridView>
    </div>
  </ContentTemplate>
</asp:UpdatePanel>


