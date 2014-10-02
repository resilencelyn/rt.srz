<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="Tfoms.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.Tfoms" %>

<%@ Import Namespace="rt.srz.model.srz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="True" CombineScripts="False" ScriptMode="Release" />


  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Территориальные фонды" />
  </div>

  <asp:UpdatePanel ID="contentUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <asp:GridView Style="width: 100%" ID="grid" runat="server" EnableModelValidation="True"
        AllowSorting="True" AutoGenerateColumns="False"
        DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both">
        <HeaderStyle CssClass="GridHeader" />
        <RowStyle CssClass="GridRowStyle" />
        <SelectedRowStyle CssClass="GridSelectedRowStyle" />
        <Columns>
          <asp:BoundField DataField="Code" HeaderText="Код" />
          <asp:BoundField DataField="ShortName" HeaderText="Краткое наименование" />
          <asp:BoundField DataField="FullName" HeaderText="Полное наименование" />
          <asp:TemplateField HeaderText="Включить/Выключить">
            <ItemTemplate>
              <asp:Button ID="btnTurn" runat="server" Text='<%# ((Organisation)Container.DataItem).IsOnLine ? "Выключить" : "Включить" %>' OnClick="btnTurn_Click" CssClass="buttons"></asp:Button>
              <asp:HiddenField runat="server" ID="hId" Value='<%# ((Organisation)Container.DataItem).Id %>' />
              <asp:HiddenField runat="server" ID="hIsOnline" Value='<%# ((Organisation)Container.DataItem).IsOnLine %>' />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
