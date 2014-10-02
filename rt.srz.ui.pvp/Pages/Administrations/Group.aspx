<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AddGroup" %>

<%@ Register Src="~/Controls/Administration/AddGroupControl.ascx" TagName="AddGroupControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AddGroupControl ID="addGroupControl" runat="server" />
</asp:Content>
