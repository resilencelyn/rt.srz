<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="SmoDetail.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.SmoDetail" %>

<%@ Register Src="~/Controls/Administration/SmoDetailControl.ascx" TagName="SmoDetailControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:SmoDetailControl ID="smoDetailControl" runat="server" />
</asp:Content>
