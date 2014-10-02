<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="AssignGroupsToUser.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AssignGroupsToUser" %>

<%@ Register Src="~/Controls/Administration/AssignGroupsToUserControl.ascx" TagName="AssignGroupsToUserControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AssignGroupsToUserControl ID="assignGroupsToUserControl" runat="server" />
</asp:Content>
