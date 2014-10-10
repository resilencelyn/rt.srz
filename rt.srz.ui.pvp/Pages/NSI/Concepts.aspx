<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="Concepts.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.NSI.Concepts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/ScrollMethods.js") %>"></script>

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
    <asp:Label ID="Label9" runat="server" Text="Справочник кодификаторов" />
  </div>

  <div style="clear: both">

    <div style="width: 50%; float: left;">
      <div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <asp:HiddenField ID="hfScrollPosition" runat="server" Value="0" />
            <div runat="server" id="scrollArea" onscroll="SetDivPosition()" style="height: 700px; overflow: auto;">
              <asp:GridView Style="" ID="grid" runat="server" EnableModelValidation="True"
                AllowSorting="True" AutoGenerateColumns="False" OnSelectedIndexChanged="grid_SelectedIndexChanged"
                CellPadding="4" ForeColor="#333333" GridLines="Both" Caption="Коды">
                <HeaderStyle CssClass="GridHeader" />
                <RowStyle CssClass="GridRowStyle" />
                <SelectedRowStyle CssClass="GridSelectedRowStyle" />
                <Columns>
                  <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                    <HeaderStyle CssClass="HideButton" />
                    <ItemStyle CssClass="HideButton" />
                  </asp:CommandField>
                  <asp:BoundField DataField="id" HeaderText="Идентификатор" />
                  <asp:BoundField DataField="ShortName" HeaderText="Краткое название" />
                  <asp:BoundField DataField="FullName" HeaderText="Полное название" />
                  <asp:BoundField DataField="LatinName" HeaderText="Латинское название" />
                </Columns>
              </asp:GridView>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
    <div style="float: left; width: 50%;">
      <div style="padding-left: 5px">
        <asp:UpdatePanel ID="gridPanel" runat="server">
          <ContentTemplate>
            <div runat="server" id="scrollArea1" style="height: 700px; overflow: auto;">
              <asp:GridView Style="" ID="gridConcept" runat="server" EnableModelValidation="True"
                AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" Caption="Данные">
                <HeaderStyle CssClass="GridHeader" />
                <RowStyle CssClass="GridRowStyle" />
                <SelectedRowStyle CssClass="GridSelectedRowStyle" />
                <Columns>
                  <asp:CommandField SelectText="Выбор" ShowSelectButton="false" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                    <HeaderStyle CssClass="HideButton" />
                    <ItemStyle CssClass="HideButton" />
                  </asp:CommandField>
                  <asp:BoundField DataField="Code" HeaderText="Код" />
                  <asp:BoundField DataField="Name" HeaderText="Наименование" />
                  <asp:BoundField DataField="ShortName" HeaderText="Краткое наименование" />
                  <asp:BoundField DataField="Description" HeaderText="Описание" />
                  <asp:BoundField DataField="Relevance" HeaderText="Порядок вывода" />
                </Columns>
              </asp:GridView>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>

  <div style="clear: both">
  </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
