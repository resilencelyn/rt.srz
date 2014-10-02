<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="AssignRolesForPermission.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AssignRolesForPermission" %>

<%@ Register Src="~/Controls/Administration/AssignRolesToPermissionControl.ascx" TagName="AssignRolesToPermissionControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AssignRolesToPermissionControl ID="assignRolesToPermissionControl" runat="server" />
</asp:Content>
