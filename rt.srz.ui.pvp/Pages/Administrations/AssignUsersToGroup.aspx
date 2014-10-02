<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="AssignUsersToGroup.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AssignUsersToGroup" %>

<%@ Register Src="~/Controls/Administration/AssignUsersToGroupControl.ascx" TagName="AssignUsersToGroupControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AssignUsersToGroupControl ID="assignUsersToGroupControl" runat="server" />
</asp:Content>
