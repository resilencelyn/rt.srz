<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step3.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps.Step3" %>
<%@ Register Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" TagPrefix="asp" %>
<%@ Register TagPrefix="uc" TagName="Kladr" Src="~/Controls/KLADRUserControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="KladrIntellisense" Src="~/Controls/KLADRIntellisenseUserControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="DocumentUDL" Src="~/Controls/DocumentUDLUserControl.ascx" %>

<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<script type="text/javascript">

  function isHomelessChecked() {
    if ($get('<%=chBIsHomeless.ClientID%>').checked == true) {
      var v = $get('<%=chbIsFreeMainAddress.ClientID%>');
      $get('<%=isFreeMainAddressDiv.ClientID%>').style.display = 'none';
      $get('<%=mainAddressKladrIntellisenseDiv.ClientID%>').style.display = 'none';
      $get('<%=mainAddressKladrDiv.ClientID%>').style.display = 'none';
      $get('<%=dateRegistrationDiv.ClientID%>').style.display = 'none';
      //$get('registrationDocumentDiv.ClientID%>').style.display = 'none';
    }
    else {
      $get('<%=isFreeMainAddressDiv.ClientID%>').style.display = 'block';
      var type = $get('<%=hfKLADRControlType.ClientID%>').value;
      if (type == "Structured" || $get('<%=chbIsFreeMainAddress.ClientID%>').checked == true) {
        $get('<%=mainAddressKladrDiv.ClientID%>').style.display = 'block';
      }
      else if (type == "Intellisense") {
        $get('<%=mainAddressKladrIntellisenseDiv.ClientID%>').style.display = 'block';
          }
        $get('<%=dateRegistrationDiv.ClientID%>').style.display = 'block';
      //$get('=registrationDocumentDiv.ClientID%>').style.display = 'block';
    }
  }

  function isFreeMainAddress() {
    var type = $get('<%=hfKLADRControlType.ClientID%>').value;
    if (type == "Intellisense") {
      if ($get('<%=chbIsFreeMainAddress.ClientID%>').checked == true) {
            $get('<%=mainAddressKladrDiv.ClientID%>').style.display = 'block';
            $get('<%=mainAddressKladrIntellisenseDiv.ClientID%>').style.display = 'none';
          }
          else {
            $get('<%=mainAddressKladrDiv.ClientID%>').style.display = 'none';
            $get('<%=mainAddressKladrIntellisenseDiv.ClientID%>').style.display = 'block';
          }
        }
      }

      function isYesResidencyAddressChecked() {
        $get('<%=residencyAddressLabelDiv.ClientID%>').style.display = 'block';
      $get('<%=isFreeResidencyAdressDiv.ClientID%>').style.display = 'block';
      var type = $get('<%=hfKLADRControlType.ClientID%>').value;
      if (type == "Structured") {
        $get('<%=residencyAddressKladrDiv.ClientID%>').style.display = 'block';
          }
          else if (type == "Intellisense") {
            $get('<%=residencyAddressKladrIntellisenseDiv.ClientID%>').style.display = 'block';
      }
  }

  function isNoResidencyAddressChecked() {
    $get('<%=residencyAddressLabelDiv.ClientID%>').style.display = 'none';
      $get('<%=isFreeResidencyAdressDiv.ClientID%>').style.display = 'none';
      $get('<%=residencyAddressKladrDiv.ClientID%>').style.display = 'none';
      $get('<%=residencyAddressKladrIntellisenseDiv.ClientID%>').style.display = 'none';
    }

    function isFreeResidencyAddress() {
      var type = $get('<%=hfKLADRControlType.ClientID%>').value;
      if (type == "Intellisense") {
        if ($get('<%=chbIsFreeResidencyAddress.ClientID%>').checked == true) {
          $get('<%=residencyAddressKladrDiv.ClientID%>').style.display = 'block';
          $get('<%=residencyAddressKladrIntellisenseDiv.ClientID%>').style.display = 'none';
        }
        else {
          $get('<%=residencyAddressKladrDiv.ClientID%>').style.display = 'none';
          $get('<%=residencyAddressKladrIntellisenseDiv.ClientID%>').style.display = 'block';
        }
      }
    }

  function isCopyDataFromUdl() {

      //var docType = $get(' (documentRegistration.FindControl("ddlDocumentType")).ClientID %>');
      //var docTypeHf = $get(' (documentRegistration.FindControl("hfSelectedDocType")).ClientID %>');
      //var lbDocSeries = $get(' (documentRegistration.FindControl("lbSeries")).ClientID %>');
      //var docSeries = $get(' (documentRegistration.FindControl("tbSeries")).ClientID %>');
      //var docNumber = $get(' (documentRegistration.FindControl("tbNumber")).ClientID %>');
      //var docIssuer = $get(' (documentRegistration.FindControl("tbIssuingAuthority")).ClientID %>');
      //var docIssueDate = $get(' (documentRegistration.FindControl("tbIssueDate")).ClientID %>');

      //if ($get('chBCopyFromUDL.ClientID%>').checked == true) {
        //docType.disabled = true;
        //docSeries.disabled = true;
        //docSeries.value = '';
        //docSeries.style.display = 'block';
        //lbDocSeries.style.display = 'block';
    //docNumber.value = '';
    //docNumber.disabled = true;
    //  docIssuer.value = '';
    //  docIssuer.disabled = true;
    //  docIssueDate.value = '';
    //  docIssueDate.disabled = true;
    //  PageMethods.GetCurrentDocumentUdl(onSuccessLoadCurrentDocumentUdl);
    //}
    //else {
    //  docType.value = '429';   //Свидетельство о регистрации по месту жительства
    //  docTypeHf.value = '429'; //Свидетельство о регистрации по месту жительства
    //  docType.disabled = true;
    //  docType.onchange(); // Применяем маски
    //  docSeries.disabled = false;
    //  docSeries.value = '';
    //  docSeries.style.display = 'none';
    //  lbDocSeries.style.display = 'none';
    //  docNumber.value = '';
    //  docNumber.disabled = false;
    //  docIssuer.value = '';
    //  docIssuer.disabled = false;
    //  docIssueDate.value = $get('tbDateRegistration.ClientID%>').value;
    //      docIssueDate.disabled = false;
    //
    //    }
        }

        function onSuccessLoadCurrentDocumentUdl(response) {
          //$get(' (documentRegistration.FindControl("ddlDocumentType")).ClientID %>').value = response.DocumentType;
          //$get(' (documentRegistration.FindControl("hfSelectedDocType")).ClientID %>').value = response.DocumentType;
          //$get(' (documentRegistration.FindControl("tbSeries")).ClientID %>').value = response.DocumentSeries;
          //$get(' (documentRegistration.FindControl("tbNumber")).ClientID %>').value = response.DocumentNumber;
          //$get(' (documentRegistration.FindControl("tbIssuingAuthority")).ClientID %>').value = response.DocumentIssuer;
          //$get(' (documentRegistration.FindControl("tbIssueDate")).ClientID %>').value = response.DocumentIssueDate;
    }
</script>

<div class="wizardDiv">
  <asp:HiddenField ID="hfKLADRControlType" runat="server" />
  <br />
  <div class="headerTitles">
    <asp:Label ID="Label10" runat="server" Text="Введите контактно-адресную информацию застрахованного лица"></asp:Label>
  </div>
  <div class="headerSubTitles">
    <asp:Label ID="Label16" runat="server" Text="Адрес регистрации по месту жительства в Российской Федерации" />
  </div>
  <table>
    <tr>
      <td class="regLeftColumn"></td>
      <td class="regRightColumn">
        <asp:CheckBox ID="chBIsHomeless" runat="server" Text="Лицо без определенного места жительства" onclick="isHomelessChecked()" /></td>
    </tr>
  </table>
  <div id="isFreeMainAddressDiv" runat="server">
    <table>
      <tr>
        <td class="regLeftColumn"></td>
        <td class="regRightColumn">
          <asp:CheckBox ID="chbIsFreeMainAddress" runat="server" Text="Ввести адрес в свободной форме" onclick="isFreeMainAddress()" /></td>
      </tr>
    </table>
  </div>
  <div id="mainAddressKladrIntellisenseDiv" runat="server">
    <uc:KladrIntellisense runat="server" ID="mainAddressKladrIntellisense" />
  </div>
  <div id="mainAddressKladrDiv" runat="server">
    <uc:Kladr runat="server" ID="mainAddressKladr" />
  </div>
  <div id="dateRegistrationDiv" runat="server">
    <table>
      <tr>
        <td class="regLeftColumn">
          <asp:Label ID="lbDateRegistration" runat="Server" Text="Дата регистрации" /></td>
        <td class="regRightColumn">
          <uc:DateBox runat="server" ID="tbDateRegistration" Width="100px" CssClass="controlBoxes" />
        </td>
      </tr>
    </table>
  </div>
  <br />
  <br />
  <%--<asp:UpdatePanel ID="RegistrationDocumentUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>

<%--  <div id="registrationDocumentDiv" runat="server">
    <div class="headerSubTitles">
      <asp:Label ID="Label1" runat="server" Text="Документ, подтверждающий регистрацию" />
    </div>
    <table>
      <tr>
        <td class="regLeftColumn"></td>
        <td class="regRightColumn">
          <asp:CheckBox ID="chBCopyFromUDL" runat="server" Text="Совпадает с документом, удостоверяющим личность" Checked="false" onclick="isCopyDataFromUdl()" />
        </td>
      </tr>
    </table>
    <div>
      <uc:DocumentUDL runat="server" ID="documentRegistration" />
    </div>
  </div>--%>


  <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label8" runat="Server" Text="Адрес места пребывания отличается от адреса регистрации" /></td>
      <td class="regRightColumn">
        <asp:RadioButton ID="rbYesResAddress" runat="server" Text="Да" GroupName="rgResidencyAddress" onclick="isYesResidencyAddressChecked()" />
        <asp:RadioButton ID="rbNoResAddress" runat="server" Text="Нет" GroupName="rgResidencyAddress" Checked="true" onclick="isNoResidencyAddressChecked()" />
      </td>
    </tr>
  </table>
  <div id="residencyAddressLabelDiv" runat="server">
    <div class="headerSubTitles">
      <asp:Label ID="Label9" runat="server" Text="Адрес места пребывания" />
    </div>
  </div>
  <div id="isFreeResidencyAdressDiv" runat="server">
    <table>
      <tr>
        <td class="regLeftColumn"></td>
        <td class="regRightColumn">
          <asp:CheckBox ID="chbIsFreeResidencyAddress" runat="server" Text="Ввести адрес в свободной форме" onclick="isFreeResidencyAddress()" /></td>
      </tr>
    </table>
  </div>
  <div id="residencyAddressKladrIntellisenseDiv" runat="server">
    <uc:KladrIntellisense runat="server" ID="residencyAddressKladrIntellisense" />
  </div>
  <div id="residencyAddressKladrDiv" runat="server">
    <uc:Kladr runat="server" ID="residencyAddressKladr" />
  </div>
  <br />
  <div class="headerSubTitles">
    <asp:Label ID="Labe10" runat="server" Text="Контактная информация" />
  </div>
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label11" runat="Server" Text="Телефон домашний:" /></td>
      <td style="padding-left: 20px;">
        <asp:TextBox ID="tbHomePhone" runat="Server" Width="200px" MaxLength="40" CssClass="controlBoxes" /></td>
      <td></td>
    </tr>
<%--    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label12" runat="Server" Text="Телефон рабочий:" /></td>
      <td style="padding-left: 20px;">
        <asp:TextBox ID="tbWorkPhone" runat="Server" Width="200px" MaxLength="40" CssClass="controlBoxes" /></td>
      <td></td>
    </tr>--%>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label13" runat="Server" Text="Электронная почта:" /></td>
      <td style="padding-left: 20px;">
        <asp:TextBox ID="tbEmail" runat="Server" Width="200px" MaxLength="50" CssClass="controlBoxes" /></td>
      <td class="errorMessage">
        <asp:CustomValidator ID="cvEmail" runat="server" EnableClientScript="false" Text="Некорректный адрес электронной почты" OnServerValidate="ValidateEmail" />
      </td>
    </tr>
  </table>

  <div style="display: none;">
    <asp:CustomValidator ID="cvRegistrationSubject" runat="server" EnableClientScript="false" OnServerValidate="ValidateRegistrationSubject" /><br />
    <asp:CustomValidator ID="cvRegistrationPostcode" runat="server" EnableClientScript="false" OnServerValidate="ValidateRegistrationPostcode" /><br />
    <asp:CustomValidator ID="cvDocument" runat="server" EnableClientScript="false" OnServerValidate="ValidateDocument" />
    <asp:CustomValidator ID="cvResidencySubject" runat="server" EnableClientScript="false" OnServerValidate="ValidateResidencySubject" /><br />
    <asp:CustomValidator ID="cvResidencyPostcode" runat="server" EnableClientScript="false" OnServerValidate="ValidateResidencyPostcode" /><br />
  </div>
</div>
