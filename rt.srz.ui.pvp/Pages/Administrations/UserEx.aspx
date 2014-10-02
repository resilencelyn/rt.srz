<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="UserEx.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AddUserEx" %>

<%@ Register Src="~/Controls/Administration/AddUserControl.ascx" TagName="AddUserControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/AssignGroupsToUserControl.ascx" TagName="AssignGroupsToUserControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/AssignRolesToUserControl.ascx" TagName="AssignRolesToUserControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/AssignPdpToUserControl.ascx" TagName="AssignPdpToUserControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<div class="formTitle">
        <asp:Label ID="lbTitle" runat="server" Text="Добавление пользователя"></asp:Label>
    </div>
    <uc:AssignPdpToUserControl ID="assignPdpToUserControl" runat="server" />
    <uc:AddUserControl ID="addUserControl" runat="server" />
	<uc:AssignGroupsToUserControl ID="assignGroupsToControl" runat="server" />
	<uc:AssignRolesToUserControl ID="assignRolesToUserControl" runat="server" />
</asp:Content>
