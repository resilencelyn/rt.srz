<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="JobsSettings.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administration.JobsSettings" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-2.1.0.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-migrate-1.2.1.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/DateTextBox.js") %>"></script>

    <div class="formTitle">
        <asp:Label ID="lbTitle" runat="server" Text="Настройки задач" />
    </div>

    <div class="headerTitles">
        <asp:Label ID="lbFirstLoadingTitle" runat="server" Text="Настройки задач первичной загрузки"></asp:Label>
    </div>

    <%--Максимальное количество исполняемых задач--%>
    <div style="clear: both;">
        <div class="admLabelControl1_4">
            <div class="admControlPadding">
                <asp:Label ID="lbMaxJobCount" runat="server" Text="Максимально к-во исполняемых задач первичной загрузки:"></asp:Label>
            </div>
        </div>
        <div class="admValueControl1_4">
            <div class="admControlPadding">
                <asp:TextBox ID="tbMaxJobCount" runat="server" MaxLength="16" Width="100%" CssClass="controlBoxes"></asp:TextBox>
            </div>
        </div>
    </div>

     <div style="clear: both">
        <div class="separator">
        </div>
    </div>
    
    <div style="clear: both; ">
        <asp:Button ID="btnSaveStatement" runat="server" Text="Сохранить" OnClick="btnSaveStatement_Click" CssClass="buttons" />
        <asp:Button ID="btnCancel" runat="server" Text="Отменить" OnClick="btnCancel_Click" CausesValidation="False" CssClass="buttons" />
    </div>
</asp:Content>