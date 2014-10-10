<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="SeparateNew.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.SeparateNew" %>

<%@ Register Src="~/Controls/Twins/SeparateGridControl.ascx" TagName="gridControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />

  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Разделение" />
  </div>

  <div style="min-height: 50px">
    <uc:gridControl ID="gridTop" runat="server" />
  </div>

  <div>
    <asp:UpdatePanel ID="movePanel" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
        <div style="float: left;">
          <div style="padding-top: 5px; padding-bottom: 5px; padding-right: 5px">
            <asp:Button ID="btnToBottom" runat="server" Text="Вниз" OnClick="Bottom_Click" CssClass="buttons" />
          </div>
        </div>
        <div>
          <div style="float: left;">
            <div style="padding-top: 5px; padding-bottom: 5px">
              <asp:Button ID="btnToTop" runat="server" Text="Вверх" OnClick="Top_Click" CssClass="buttons" />
            </div>
          </div>
        </div>
      </ContentTemplate>
    </asp:UpdatePanel>
  </div>

  <div style="clear: both" />

  <div style="min-height: 50px">
    <uc:gridControl ID="gridBottom" runat="server" />
  </div>

  <asp:UpdateProgress ID="GridUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
      <div class="updateProgressPositionDiv">
        <div class="updateProgress">
          <asp:Image ID="Image2" runat="server" ImageUrl="~/Resources/ajax-loader.gif" />
        </div>
      </div>
    </ProgressTemplate>
  </asp:UpdateProgress>

  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

      <div style="float: left">
        <div class="controlPart">
          <div>
            <asp:Button ID="btnSeparate" runat="server" Text="Разделить" OnClick="SeparateClick" Enabled="false" CssClass="buttons" />
          </div>
        </div>
      </div>

      <div style="clear: both">
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
