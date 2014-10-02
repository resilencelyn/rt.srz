<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release" />
  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Общие настройки" />
  </div>

  <asp:GridView ID="gvSettings" AutoGenerateColumns="false" runat="server" OnRowCreated="gvStates_RowCreated" Width="40%">
    <HeaderStyle CssClass="GridHeader" />
    <RowStyle CssClass="GridRowStyle" />
    <SelectedRowStyle CssClass="GridSelectedRowStyle" />
    <Columns>
      <asp:BoundField HeaderText="название" DataField="Name" />
      <asp:TemplateField HeaderText="значение">
        <ItemTemplate>
          <asp:DropDownList ID="ddlValues" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlValues_SelectedIndexChanged" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>
</asp:Content>
