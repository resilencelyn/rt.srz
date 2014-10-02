<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="AdministrationRoles.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AdministrationRoles" %>

<%@ Register Src="~/Controls/Administration/RolesControl.ascx" TagName="RolesControl" TagPrefix="uc" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release" />

  <uc:RolesControl ID="RolesControl" runat="server" />

</asp:Content>
