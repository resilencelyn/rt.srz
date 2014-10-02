<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="RoleEx.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AddRoleEx" %>

<%@ Register Src="~/Controls/Administration/AddRoleControl.ascx" TagName="AddRoleControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/AssignPermissionsToRoleControl.ascx" TagName="AssignPermissionsToRoleControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AddRoleControl ID="addRoleControl" runat="server" />
	<uc:AssignPermissionsToRoleControl ID="assignPermissionsToRoleControl" runat="server" />
</asp:Content>
