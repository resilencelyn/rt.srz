<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AddUser" %>

<%@ Register Src="~/Controls/Administration/AddUserControl.ascx" TagName="AddUserControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AddUserControl ID="addUserControl" runat="server" />
</asp:Content>
