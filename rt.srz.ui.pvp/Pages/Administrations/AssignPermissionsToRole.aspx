<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="AssignPermissionsToRole.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AssignPermissionsToRole" %>

<%@ Register Src="~/Controls/Administration/AssignPermissionsToRoleControl.ascx" TagName="AssignPermissionsToRoleControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AssignPermissionsToRoleControl ID="assignPermissionsToRoleControl" runat="server" />
</asp:Content>
