<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="Permission.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AddPermission" %>

<%@ Register Src="~/Controls/Administration/AddPermissionControl.ascx" TagName="AddPermissionControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AddPermissionControl ID="addPermissionControl" runat="server" />
</asp:Content>
