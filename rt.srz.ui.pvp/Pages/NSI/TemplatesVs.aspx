<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="TemplatesVs.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.NSI.TemplatesVs" %>

<%@ Register Src="~/Controls/NSI/TemplatesVsControl.ascx" TagName="TemplatesVsControl" TagPrefix="uc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release" />
  <uc:TemplatesVsControl ID="TemplatesVsControl" runat="server" />
</asp:Content>
