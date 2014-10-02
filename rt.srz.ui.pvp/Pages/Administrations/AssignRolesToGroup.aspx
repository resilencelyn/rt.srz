<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="AssignRolesToGroup.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AssignRolesToGroup" %>

<%@ Register Src="~/Controls/Administration/AssignRolesToGroupControl.ascx" TagName="AssignRolesToGroupControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AssignRolesToGroupControl ID="assignRolesToGroupControl" runat="server" />
</asp:Content>
