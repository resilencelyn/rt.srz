<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="AssignRolesToUser.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AssignRolesToUser" %>

<%@ Register Src="~/Controls/Administration/AssignRolesToUserControl.ascx" TagName="AssignRolesToUserControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AssignRolesToUserControl ID="assignRolesToUserControl" runat="server" />
</asp:Content>
