<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="AssignUecCertificates.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.Administrations.AssignUecCertificates" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" CombineScripts="False" ScriptMode="Release" />

  <div class="formTitle">
    <asp:Label ID="lbTitle" runat="server" Text="Сертификаты УЦ"></asp:Label>
  </div>

  <div>
    <%--УЦ ГОСТ--%>
    <div style="clear: both">
      <div class="admLabelControl1_4">
        <div class="admControlPadding">
          <asp:Label ID="lbUC1GOST" runat="server" Text="Сертификат  УЦ №1 ГОСТ"></asp:Label>
        </div>
      </div>
      <div class="admValueControl1_4">
        <div class="admControlPadding">
          <ajaxToolkit:AsyncFileUpload runat="server" ID="uUC1GOST" Width="100%" UploaderStyle="Modern"
            ThrobberID="UC1GOST" CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="uUC1GOST_UploadComplete" />
          <asp:Label runat="server" ID="UC1GOST" Text="загрузка..."></asp:Label>
        </div>
      </div>


      <%--УЦ RSA--%>
      <div class="admLabelControl1_4">
        <div class="admControlPadding">
          <asp:Label ID="lbUC1RSA" runat="server" Text="Сертификат  УЦ №1 RSA"></asp:Label>
        </div>
      </div>
      <div class="admValueControl1_4">
        <div class="admControlPadding">
          <ajaxToolkit:AsyncFileUpload runat="server" ID="uUC1RSA" Width="100%" UploaderStyle="Modern"
            ThrobberID="UC1RSA" CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="uUC1RSA_UploadComplete" />
          <asp:Label runat="server" ID="UC1RSA" Text="загрузка..."></asp:Label>
        </div>
      </div>

    </div>

    <div style="clear: both">

      <%--ОКО ГОСТ--%>
      <div class="admLabelControl1_4">
        <div class="admControlPadding">
          <asp:Label ID="lbOKO1GOST" runat="server" Text="Сертификат  ОКО №1 ГОСТ"></asp:Label>
        </div>
      </div>
      <div class="admValueControl1_4">
        <div class="admControlPadding">
          <ajaxToolkit:AsyncFileUpload runat="server" ID="uOKO1GOST" Width="100%" UploaderStyle="Modern"
            ThrobberID="OKO1GOST" CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="uOKO1GOST_UploadComplete" />
          <asp:Label runat="server" ID="OKO1GOST" Text="загрузка..."></asp:Label>
        </div>
      </div>


      <%--ОКО RSA--%>
      <div class="admLabelControl1_4">
        <div class="admControlPadding">
          <asp:Label ID="lbOKO1RSA" runat="server" Text="Сертификат  ОКО №1 RSA"></asp:Label>
        </div>
      </div>
      <div class="admValueControl1_4">
        <div class="admControlPadding">
          <ajaxToolkit:AsyncFileUpload runat="server" ID="uOKO1RSA" Width="100%" UploaderStyle="Modern"
            ThrobberID="OKO1RSA" CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="uOKO1RSA_UploadComplete" />
          <asp:Label runat="server" ID="OKO1RSA" Text="загрузка..."></asp:Label>
        </div>
      </div>

    </div>
  </div>

  <div style="clear: both">
    <div class="separator">
    </div>
  </div>

  <div style="clear: both; ">
    <asp:Button ID="btnSave" runat="server" Text="Сохранить" OnClick="btnSave_Click" CssClass="buttons" />
    <asp:Button ID="btnCancel" runat="server" Text="Отменить" OnClick="btnCancel_Click" CausesValidation="False" CssClass="buttons" />
  </div>

</asp:Content>

