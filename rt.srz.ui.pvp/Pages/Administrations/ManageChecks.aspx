<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master"
  AutoEventWireup="true" CodeBehind="ManageChecks.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.ManageChecks" %>

<%@ Import Namespace="rt.srz.business.interfaces.logicalcontrol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />

  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/ScrollMethods.js") %>"></script>

  <script type="text/javascript">

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function (s, e) {
      EndRequest(s, e, "<%=scrollArea.ClientID%>", "<%=hfScrollPosition.ClientID%>");
      var grd = document.getElementById("<%=grid.ClientID%>");
      if (grd != null) {
        grd.disabled = false;
      }
    });

    prm.add_beginRequest(function (s, e) {
      var grd = document.getElementById("<%=grid.ClientID%>");
      if (grd != null) {
        grd.disabled = true;
      }
    });

    window.onload = function () {
      OnLoad("<%=scrollArea.ClientID%>", "<%=hfScrollPosition.ClientID%>");
    }

    function SetDivPosition() {
      SetDivPos("<%=scrollArea.ClientID%>", "<%=hfScrollPosition.ClientID%>");
    }

  </script>

  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Настройка проверок заявления" />
  </div>
  <asp:UpdatePanel ID="gridPanel" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hfScrollPosition" runat="server" Value="0" />
      <div runat="server" id="scrollArea" onscroll="SetDivPosition()" style="overflow: auto;">
        <asp:GridView Style="width: 100%;" ID="grid" runat="server" EnableModelValidation="True"
          AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ClassName"
          CellPadding="4" ForeColor="#333333" GridLines="Both"
          OnSelectedIndexChanged="grid_SelectedIndexChanged"
          EnableViewState="true"
          OnDataBound="OnDataBound">
          <HeaderStyle CssClass="GridHeader" />
          <RowStyle CssClass="GridRowStyle" />
          <SelectedRowStyle CssClass="GridSelectedRowStyle" />
          <Columns>
            <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
              <HeaderStyle CssClass="HideButton" />
              <ItemStyle CssClass="HideButton" />
            </asp:CommandField>

            <asp:BoundField HeaderText="Тип проверки" DataField="LevelDescription">
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField HeaderText="№" DataField="RecordNumber" />

            <asp:TemplateField HeaderText="Можно ли включать/выключать проверку" SortExpression="" Visible=" false">
              <HeaderStyle CssClass="CheckHeader" />
              <ItemStyle CssClass="CheckItem" />
              <ItemTemplate>
                <asp:CheckBox ID="cbAllowChange" runat="server" Checked='<%# ((ICheckStatement)Container.DataItem).AllowChange %>' Enabled="false" OnCheckedChanged="cbAllowChange_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
              </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Включена ли проверка" SortExpression="">
              <HeaderStyle CssClass="CheckHeader" />
              <ItemStyle CssClass="CheckItem" />
              <ItemTemplate>
                <asp:CheckBox ID="cbCheck" runat="server" Checked='<%# ((ICheckStatement)Container.DataItem).CheckRequired %>' OnCheckedChanged="cbCheck_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                <asp:HiddenField ID="hfAllowChange" runat="server" Value='<%# ((ICheckStatement)Container.DataItem).AllowChange %>'></asp:HiddenField>
                <asp:HiddenField ID="hfClassName" runat="server" Value='<%# ((ICheckStatement)Container.DataItem).ClassName %>'></asp:HiddenField>
                <asp:HiddenField ID="hfVisible" runat="server" Value='<%# ((ICheckStatement)Container.DataItem).Visible %>'></asp:HiddenField>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Название" SortExpression="">
              <ItemTemplate>
                <asp:Label ID="lb1" runat="server" Text='<%# ((ICheckStatement)Container.DataItem).Caption %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
          </Columns>
        </asp:GridView>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
