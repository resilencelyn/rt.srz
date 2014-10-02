<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/AuthentificatedPage.Master" CodeBehind="SearchKeyTypeDetail.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Twins.SearchKeyTypeDetail" %>

<%@ Register Src="~/Controls/Twins/SearchKeyTypeDetailControl.ascx" TagName="SearchKeyTypeDetailControl" TagPrefix="uc" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="False" ScriptMode="Release"/>

  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-2.1.0.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-migrate-1.2.1.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.meio.mask.js") %>"></script>

  <div style="height: 100%; width: 100%; position: relative;">
    <uc:SearchKeyTypeDetailControl ID="SearchKeyTypeDetailCtrl" runat="server" />
  </div>
</asp:Content>
