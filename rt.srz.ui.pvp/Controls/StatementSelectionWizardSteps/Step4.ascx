<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step4.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps.Step4" %>
<%@ Register TagPrefix="uc" TagName="DocumentUDL" Src="~/Controls/DocumentUDLUserControl.ascx" %>

<%@ Import Namespace="rt.srz.model.srz.concepts" %>

<script type="text/javascript">

  //Подлключение обработчика ввода допустимых символов
  $(document).ready(function () {
    limitKeyPressForLFM();
  });

  //Обработчик выбора имени через Intellisense
  function firstNameItemSelected(sender, e) {
    //Присвоение id имени Intellisense для отчества для фильтрации
    $find('<%=aceMiddleNameIntellisense.ClientID%>').set_contextKey(e.get_value());

	  //Перезагрузка содержимого поля "Отношение к застрахованному лицу"
	  PageMethods.GetRepresentativeGenderByAutoCompleteId(e.get_value(), onSuccessGetRepresentativeGender, onErrorGet);
	}

	//Обработчик выбора отчества через Intellisense
	function middleNameItemSelected(sender, e) {
	  //Перезагрузка содержимого поля "Отношение к застрахованному лицу"
	  PageMethods.GetRepresentativeGenderByAutoCompleteId(e.get_value(), onSuccessGetRepresentativeGender, onErrorGet);
	}

	function onSuccessGetRepresentativeGender(result) {
	  $get('<%=ddlRelationType.ClientID%>').value = result;
	}

	function lastNameChanged() {
	  //Скрытие валидатора
	  var validator = $get('<%=cvLastName.ClientID%>');
	  if (validator != null)
	    validator.style.visibility = 'hidden';
	}

	function firstNameChanged() {
	  //Скрытие валидатора
	  var validator = $get('<%=cvFirstName.ClientID%>');
	  if (validator != null)
	    validator.style.visibility = 'hidden';

	  //Исключение TAB
	  if (event.which == null) {  // IE
	    if (event.keyCode == 9)
	      return;
	  }

	  //Сброс id имени Intellisense для отчества для фильтрации
	  $find('<%=aceMiddleNameIntellisense.ClientID%>').set_contextKey(null);
  }

  function middleNameChanged() {
    //Скрытие валидатора
    var validator = $get('<%=cvMiddleName.ClientID%>');
	  if (validator != null)
	    validator.style.visibility = 'hidden';
	}

	//Скрытие валидатора
	function RelationTypeChanged() {
	  var validator = $get('<%=cvRelationType.ClientID%>');
	  if (validator != null)
	    validator.style.visibility = 'hidden';
	}

  function SetValuesByUdl() {
    
	  var docNumber = $get('<%= documentUDL.NumberClientId %>').value;
      var baseSeries = $get('<%= documentUDL.SeriesClientId %>').value;
      var additionalSeries = $get('<%= documentUDL.AdditionalSeriesClientId %>').value;
      var docType = $get('<%= documentUDL.DocumentTypeClientId %>').value;
      if (docType == '<%= DocumentType.BirthCertificateRf %>') {
        baseSeries = baseSeries + "-" + additionalSeries;
      }
      PageMethods.GetRepresentativeContactInfoByUdl(docNumber, baseSeries, onSuccessGetUdl, onErrorGet);
    }

  function onSuccessGetUdl(result) {
    
      if (result == null) {
        return;
      }
      SetTextBoxValue($get('<%= tbFirstName.ClientID%>'), result.FirstName);
		SetTextBoxValue($get('<%= tbLastName.ClientID%>'), result.LastName);
      SetTextBoxValue($get('<%= tbMiddleName.ClientID%>'), result.MiddleName);
      SetTextBoxValue($get('<%= tbHomePhone.ClientID%>'), result.HomePhone);
      SetTextBoxValue($get('<%= tbWorkPhone.ClientID%>'), result.WorkPhone);
      SetDropDownValue($get('<%= ddlRelationType.ClientID%>'), result.RelationTypeId);
      SetTextBoxValue($get('<%= (documentUDL.FindControl("tbIssuingAuthority")).ClientID %>'), result.IssuingAuthority);
      SetTextBoxValue($get('<%= (documentUDL.FindControl("tbIssueDate")).ClientID %>'), result.DateIssue);
      SetTextBoxValue($get('<%= (documentUDL.FindControl("tbDateExp")).ClientID %>'), result.DateExp);

    }

    function SetTextBoxValue(control, value) {
        control.value = value;
    }

    function SetDropDownValue(dropDownControl, value) {
    
      for (i = 0; i < dropDownControl.options.length; i++) {
        if (dropDownControl.options[i].value == value) {
          dropDownControl.selectedIndex = i;
        }
      }
    }

    function onErrorGet(result) {

    }

</script>

<div class="wizardDiv">
  <br />
  <div class="headerTitles">
    <asp:Label ID="Label10" runat="server" Text="Введите информацию о представителе застрахованного лица"></asp:Label>
  </div>
  <asp:Label ID="Label1" runat="server" Text="Сведения о документе, удостоверяющем личность" Font-Bold="True" />
  <div>
    <uc:documentudl runat="server" id="documentUDL" />
  </div>
  <br />
  <asp:Label ID="Label16" runat="server" Text="Персональные данные" Font-Bold="True" />
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="lblLastName" runat="Server" Text="Фамилия" /></td>
      <td class="regRightColumn">
        <asp:TextBox ID="tbLastName" runat="Server" Width="100%" MaxLength="50" CssClass="lastName" onkeydown="lastNameChanged()" onkeypress="firstSymbolToUpper(this)" onpaste="return PasteFio(this, event);"/></td>
      <td class="errorMessage">
        <asp:CustomValidator ID="cvLastName" runat="server" Text="" OnServerValidate="ValidateLastName" EnableClientScript="false" />
      </td>
    </tr>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label2" runat="Server" Text="Имя" /></td>
      <td class="regRightColumn">
        <asp:TextBox ID="tbFirstName" runat="Server" Width="100%" MaxLength="50" CssClass="firstName" onkeydown="firstNameChanged()" onkeypress="firstSymbolToUpper(this)" onpaste="return PasteFio(this, event);"/></td>
      <asp:HiddenField ID="hfFirstNameAutoCompleteID" runat="server" />
      <ajaxToolkit:AutoCompleteExtender
        runat="server"
        ID="aceFirstNameIntellisense"
        TargetControlID="tbFirstName"
        ServiceMethod="GetFirstNameAutoComplete"
        FirstRowSelected="true"
        MinimumPrefixLength="1"
        CompletionInterval="100"
        EnableCaching="false"
        CompletionSetCount="1000"
        DelimiterCharacters=""
        UseContextKey="false"
        OnClientItemSelected="firstNameItemSelected" />
      <td class="errorMessage">
        <asp:CustomValidator ID="cvFirstName" runat="server" Text="" OnServerValidate="ValidateFirstName" EnableClientScript="false" />
      </td>
    </tr>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label3" runat="Server" Text="Отчество" /></td>
      <td class="regRightColumn">
        <asp:TextBox ID="tbMiddleName" runat="Server" Width="100%" MaxLength="50" CssClass="middleName" onkeydown="middleNameChanged()" onkeypress="firstSymbolToUpper(this)" onpaste="return PasteFio(this, event);"/></td>
      <asp:HiddenField ID="hfMiddleNameAutoCompleteID" runat="server" />
      <ajaxToolkit:AutoCompleteExtender
        runat="server"
        ID="aceMiddleNameIntellisense"
        TargetControlID="tbMiddleName"
        ServiceMethod="GetMiddleNameAutoComplete"
        FirstRowSelected="true"
        MinimumPrefixLength="1"
        CompletionInterval="100"
        EnableCaching="false"
        CompletionSetCount="1000"
        DelimiterCharacters=""
        UseContextKey="true"
        OnClientItemSelected="middleNameItemSelected" />
      <td class="errorMessage">
        <asp:CustomValidator ID="cvMiddleName" runat="server" Text="" OnServerValidate="ValidateMiddleName" EnableClientScript="false" />
      </td>
    </tr>
  </table>
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label6" runat="Server" Text="Отношение к застрахованному лицу*" Font-Size="13px" /></td>
      <td class="regRightColumn">
        <asp:DropDownList ID="ddlRelationType" runat="Server" Width="100%" OnChange="RelationTypeChanged()">
          <asp:ListItem Text="Выберите отношение к застрахованному лицу" Value="-1" />
        </asp:DropDownList>
      </td>
      <td class="errorMessage">
        <asp:CustomValidator ID="cvRelationType" runat="server" EnableClientScript="false" Text="Укажите отношение к застрахованному лицу!"
          ControlToValidate="ddlRelationType" OnServerValidate="ValidateRelationType" />
      </td>
    </tr>
  </table>
  <br />
  <asp:Label ID="Label4" runat="server" Text="Контактная информация" Font-Bold="True" />
  <br />
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label11" runat="Server" Text="Телефон домашний:" /></td>
      <td class="regRightColumn">
        <asp:TextBox ID="tbHomePhone" runat="Server" Width="300px" MaxLength="40" /></td>
    </tr>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label14" runat="Server" Text="Телефон рабочий:" /></td>
      <td class="regRightColumn">
        <asp:TextBox ID="tbWorkPhone" runat="Server" Width="300px" MaxLength="40" /></td>
    </tr>
  </table>

  <div style="display: none;">
    <asp:CustomValidator ID="cvDocumentType" runat="server" EnableClientScript="false" OnServerValidate="ValidateDocument" /><br />
  </div>
</div>
