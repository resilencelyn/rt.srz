<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="GroupEx.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AddGroupEx" %>

<%@ Register Src="~/Controls/Administration/AddGroupControl.ascx" TagName="AddGroupControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/AssignUsersToGroupControl.ascx" TagName="AssignUsersToGroupControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/AssignRolesToGroupControl.ascx" TagName="AssignRolesToGroupControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AddGroupControl ID="addGroupControl" runat="server" />
	<uc:AssignUsersToGroupControl ID="assignUsersToGroupToControl" runat="server" />
	<uc:AssignRolesToGroupControl ID="assignRolesToGroupControl" runat="server" />
</asp:Content>
