<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="PermissionEx.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AddPermissionEx" %>

<%@ Register Src="~/Controls/Administration/AddPermissionControl.ascx" TagName="AddPermissionControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/AssignRolesToPermissionControl.ascx" TagName="AssignRolesToPermissionControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AddPermissionControl ID="addPermissionControl" runat="server"/>
	<uc:AssignRolesToPermissionControl ID="assignRolesToPermissionControl" runat="server" />
</asp:Content>
