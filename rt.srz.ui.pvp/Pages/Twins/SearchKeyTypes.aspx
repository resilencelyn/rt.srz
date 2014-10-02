<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/AuthentificatedPage.Master" CodeBehind="SearchKeyTypes.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Twins.SearchKeyTypes" %>

<%@ Register Src="~/Controls/Twins/SearchKeyTypesControl.ascx" TagName="SearchKeyTypesControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="True" CombineScripts="False" ScriptMode="Release" />
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/ScrollMethods.js") %>"></script>

  <div>
    <uc:SearchKeyTypesControl ID="SearchKeyTypesCtrl" runat="server" />
  </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
