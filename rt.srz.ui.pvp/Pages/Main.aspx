<%@ Page Title="ПВП" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Main"
  EnableEventValidation="false"%>

<%@ Register Src="~/Controls/StatementsSearch.ascx" TagName="StatementsSearch" TagPrefix="uc" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="False" ScriptMode="Release"/>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-2.1.0.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-migrate-1.2.1.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.meio.mask.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.limitkeypress.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/DateTextBox.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/LFM.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/ScrollMethods.js") %>"></script>

    <div style="height:100%;width:100%;position:relative;">
        <uc:StatementsSearch ID="StatementsSearch" runat="server" />
    </div>
</asp:Content>
