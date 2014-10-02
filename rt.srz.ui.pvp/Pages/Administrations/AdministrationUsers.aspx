<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="AdministrationUsers.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AdministrationUsers" %>

<%@ Register Src="~/Controls/Administration/UsersControl.ascx" TagName="UsersControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/GroupsControl.ascx" TagName="GroupsControl" TagPrefix="uc" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release" />

  <uc:UsersControl ID="UsersControl" runat="server" />

  <div class="separator">
  </div>

  <uc:GroupsControl ID="GroupsControl" runat="server" />

</asp:Content>
