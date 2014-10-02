<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="AdministrationSmos.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AdministrationSmos" %>

<%@ Register Src="~/Controls/Administration/SmosControl.ascx" TagName="SmosControl" TagPrefix="uc" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release" />

  <uc:SmosControl ID="SmosControl" runat="server" />

</asp:Content>
