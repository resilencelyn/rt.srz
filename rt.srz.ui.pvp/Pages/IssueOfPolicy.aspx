<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="IssueOfPolicy.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.IssueOfPolicy" %>

<%@ Register Src="~/Controls/IssueOfPolicy.ascx" TagName="IssueOfPolicy" TagPrefix="uc" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-2.1.0.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-migrate-1.2.1.js") %>"></script>
  <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/DateTextBox.js") %>"></script>

  <div class="formTitle">
    <asp:Label ID="lbTitle" runat="server" Text="Сведения о выданном полисе" />
  </div>
  <div>
    <asp:Label ID="cvErrors" runat="server" CssClass="errorMessage" />
  </div>
  <div>
    <uc:IssueOfPolicy ID="ctrlIssueOfPolicy" runat="server" />
  </div>

  <div style="clear: both">
    <div class="separator">
    </div>
  </div>

  <div style="clear: both; float: right">
    <asp:Button ID="btnSaveStatement" runat="server" Text="Сохранить" OnClick="btnSaveStatement_Click" CssClass="buttons" />
    <asp:Button ID="btnOpenStatement" runat="server" Text="Открыть заявление" OnClick="btnOpenStatement_OnClick" CssClass="buttons" />
    <asp:Button ID="btnCancel" runat="server" Text="Отменить" OnClick="btnCancel_Click" CausesValidation="False" CssClass="buttons" />
  </div>

  <div style="display: none;">
    <asp:CustomValidator ID="cvPolicyType" runat="server" EnableClientScript="false" OnServerValidate="ValidatePolicyType" />
    <asp:CustomValidator ID="cvEnpNumber" runat="server" EnableClientScript="false" OnServerValidate="ValidateEnpNumber" />
    <asp:CustomValidator ID="cvPolicyCertificateNumber" runat="server" EnableClientScript="false" OnServerValidate="ValidatePolicyCertificateNumber" />
    <asp:CustomValidator ID="cvPolicyDateIssue" runat="server" EnableClientScript="false" OnServerValidate="ValidatePolicyDateIssue" />
  </div>
</asp:Content>

