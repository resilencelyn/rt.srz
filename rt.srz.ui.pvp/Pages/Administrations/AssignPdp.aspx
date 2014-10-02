<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="AssignPdp.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AssignPdp" %>

<%@ Register Src="~/Controls/Administration/AssignPdpToUserControl.ascx" TagName="AssignPdpToUserControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:AssignPdpToUserControl ID="assignPdpToUserControl" runat="server" />
</asp:Content>
