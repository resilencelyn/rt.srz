<%@ Page Title="Установка" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="Installation.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Installation" %>

<%@ Register Src="~/Controls/Installation.ascx" TagName="Installation" TagPrefix="uc" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="False" ScriptMode="Release" />
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-2.1.0.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-migrate-1.2.1.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.meio.mask.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.limitkeypress.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/DateTextBox.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/LFM.js") %>"></script>

  <uc:Installation ID="InstallationControll" runat="server" />
</asp:Content>
