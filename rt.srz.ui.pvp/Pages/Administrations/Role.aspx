<%@ Page Title="" Language="C#" MasterPageFile="BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AddRole" %>

<%@ Register Src="~/Controls/Administration/AddRoleControl.ascx" TagName="AddRoleControl" TagPrefix="uc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentContent" runat="server">
		<uc:AddRoleControl ID="addRoleControl" runat="server" />
</asp:Content>
