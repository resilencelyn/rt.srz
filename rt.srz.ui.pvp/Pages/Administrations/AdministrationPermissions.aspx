<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="AdministrationPermissions.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AdministrationPermissions" %>

<%@ Register Src="~/Controls/Administration/PermissionsControl.ascx" TagName="PermissionsControl" TagPrefix="uc" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release" />

  <uc:PermissionsControl ID="PermissionsControl" runat="server" />

</asp:Content>
