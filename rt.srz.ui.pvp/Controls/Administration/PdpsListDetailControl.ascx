<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PdpsListDetailControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.PdpsListDetailControl" %>

<%@ Register Src="PdpDetailWorkstationListControl.ascx" TagName="pdpDetailControl" TagPrefix="uc" %>

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

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <div class="headerTitles">
      <asp:Label ID="Label9" runat="server" Text="Пункты выдачи полисов" />
    </div>

    <asp:UpdatePanel ID="menuPanel" runat="server" UpdateMode="Conditional">
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
        <div runat="server" id="scrollArea" onscroll="SetDivPosition()" style="height: 180px; overflow: auto;">
          <asp:GridView Style="width: 100%; table-layout: fixed; word-wrap: break-word;" ID="pdpsGridView" runat="server" EnableModelValidation="True" OnRowDataBound="pdpsGridView_RowDataBound"
            AllowSorting="True" AutoGenerateColumns="False" OnRowDeleting="pdpsGridView_Deleting" OnRowCommand="pdpsGridView_RowCommand"
            DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both" OnSelectedIndexChanged="pdpsGridView_SelectedIndexChanged">
            <HeaderStyle CssClass="GridHeader" />
            <RowStyle CssClass="GridRowStyle" />
            <SelectedRowStyle CssClass="GridSelectedRowStyle" />
            <Columns>
              <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton"></asp:ButtonField>
              <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                <HeaderStyle CssClass="HideButton" />
                <ItemStyle CssClass="HideButton" />
              </asp:CommandField>
              <asp:BoundField DataField="ShortName" HeaderText="Краткое название" SortExpression="ShortName" />
              <asp:BoundField DataField="FullName" HeaderText="Полное название" SortExpression="FullName" />
            </Columns>
          </asp:GridView>
        </div>
        <asp:Panel ID="Panel1" runat="server" Width="100%">
          <div style="margin: 5px;">
            <asp:Image ID="Image1" runat="server" />
            <asp:Label ID="lbText" runat="server" Text="Label" Font-Underline="True"></asp:Label>
          </div>
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" Width="100%">
          <div class="separator">
          </div>
          <div class="contentPadding">
            <uc:pdpDetailControl ID="pdpDetailControl" runat="server" />
          </div>
        </asp:Panel>

        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapseControlID="Panel1" ExpandControlID="Panel1" TargetControlID="Panel2"
          CollapsedText="Отобразить детальную информацию" ExpandedText="Скрыть детальную информацию" TextLabelID="lbText"
          ExpandedImage="~/Resources/collapse.png" CollapsedImage="~/Resources/expand.png" ImageControlID="Image1" Collapsed="True">
        </ajaxToolkit:CollapsiblePanelExtender>


      </ContentTemplate>
    </asp:UpdatePanel>

  </ContentTemplate>
</asp:UpdatePanel>

