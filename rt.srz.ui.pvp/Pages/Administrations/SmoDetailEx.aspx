<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/SmoAddCancelPage.master" AutoEventWireup="true" CodeBehind="SmoDetailEx.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.SmoDetailEx" 
	EnableEventValidation="false"	%>

<%@ Register Src="~/Controls/Administration/SmoDetailControl.ascx" TagName="SmoDetailControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Administration/PdpsListDetailControl.ascx" TagName="PdpsListDetailControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">
	<uc:SmoDetailControl ID="smoDetailControl" runat="server" />
	<uc:PdpsListDetailControl ID="pdpsListDetailControl" runat="server" />
</asp:Content>
