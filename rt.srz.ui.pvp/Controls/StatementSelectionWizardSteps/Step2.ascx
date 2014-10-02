<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step2.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps.Step2" %>
<%@ Register TagPrefix="uc" TagName="DocumentUDL" Src="~/Controls/DocumentUDLUserControl.ascx" %>

<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/ExtenderCustomizer.js") %>"></script>

<script type="text/javascript">

    //Подлключение обработчика ввода допустимых символов
    $(document).ready(function () {
        limitKeyPressForLFM();
    });

    //Обработчик выбора имени через Intellisense
    function firstNameItemSelected(sender, e) {
        //Присвоение id имени Intellisense для отчества для фильтрации
        $find('<%= aceMiddleNameIntellisense.ClientID %>').set_contextKey(e.get_value());

      //Перезагрузка содержимого поля "Пол"
      PageMethods.GetGenderByAutoCompleteId(e.get_value(), onSuccessGetGender);
  }

  function listShowingFirst(sender, e) {
      var searchList = sender.get_completionList().childNodes;
      if (searchList.length == 1) {
          sender._setText(searchList[0]);
          e.set_cancel(true);
          var nextEl = $get('<%= tbMiddleName.ClientID %>');
      nextEl.focus();
  }
}

//Обработчик выбора отчества через Intellisense
function middleNameItemSelected(sender, e) {
    //Перезагрузка содержимого поля "Пол"
    PageMethods.GetGenderByAutoCompleteId(e.get_value(), onSuccessGetGender);
}

function listShowingMiddle(sender, e) {
    var searchList = sender.get_completionList().childNodes;
    if (searchList.length == 1) {
        sender._setText(searchList[0]);
        e.set_cancel(true);
    }
}

//Идентификатор пола получен
function onSuccessGetGender(result) {
    $get('<%= ddlGender.ClientID %>').value = result;
  }

  //Обработчик события - потеря фокуса для поля "Отчество"
  function middleNameBlur(middleNameTextBox) {
      var value = middleNameTextBox.value;

      //Преобразование в верхний регистр
      value = value.toUpperCase();

      //Смена пола
      if (value.endsWith("ИЧ"))
          $get('<%= ddlGender.ClientID %>').value = "273";
      else if (value.endsWith("НА"))
          $get('<%= ddlGender.ClientID %>').value = "274";
  }

  // Обработчиков чекбоксов("Отсутствует фамилия", "Отсутствует имя", "Отсутствует отчество")
  function enabledisableLFMTextBox(checkbox) {
      if (checkbox.checked == true) {
          if (checkbox == $get('<%= chbIsLastNameAbsent.ClientID %>')) {
            $get('<%= tbLastName.ClientID %>').value = "";
          $get('<%= tbLastName.ClientID %>').disabled = true;
      } else if (checkbox == $get('<%= chbIsFirstNameAbsent.ClientID %>')) {
          $get('<%= tbFirstName.ClientID %>').value = "";
            $get('<%= tbFirstName.ClientID %>').disabled = true;
        } else if (checkbox == $get('<%= chbIsMiddleNameAbsent.ClientID %>')) {
            $get('<%= tbMiddleName.ClientID %>').value = "";
            $get('<%= tbMiddleName.ClientID %>').disabled = true;
        }
} else {
    if (checkbox == $get('<%= chbIsLastNameAbsent.ClientID %>')) {
            $get('<%= tbLastName.ClientID %>').disabled = false;
    } else if (checkbox == $get('<%= chbIsFirstNameAbsent.ClientID %>')) {
        $get('<%= tbFirstName.ClientID %>').disabled = false;
    } else if (checkbox == $get('<%= chbIsMiddleNameAbsent.ClientID %>')) {
        $get('<%= tbMiddleName.ClientID %>').disabled = false;
    }
}
}

function lastNameChanged() {
    //Скрытие валидатора
    var validator = $get('<%= cvLastName.ClientID %>');
    if (validator != null)
        validator.style.visibility = 'hidden';
}

function firstNameChanged() {
    //Скрытие валидатора
    var validator = $get('<%= cvFirstName.ClientID %>');
    if (validator != null)
        validator.style.visibility = 'hidden';

    //Исключение TAB
    if (event.which == null) { // IE
        if (event.keyCode == 9)
            return;
    }

    //Сброс id имени Intellisense для отчества для фильтрации
    $find('<%= aceMiddleNameIntellisense.ClientID %>').set_contextKey(null);
}

function middleNameChanged() {
    //Скрытие валидатора
    var validator = $get('<%= cvMiddleName.ClientID %>');
    if (validator != null)
        validator.style.visibility = 'hidden';
}

function genderChanged() {
    //Скрытие валидатора
    var validator = $get('<%= cvGender.ClientID %>');
    if (validator != null)
        validator.style.visibility = 'hidden';
}

//Скрытие валидатора
function birthPlaceChanged() {
    validator = $get('<%= cvBirthPlace.ClientID %>');
      if (validator != null)
          validator.style.visibility = 'hidden';
  }

  //Обработчик кнопки "Дата рождения полностью"
  function rbBirthDateChecked() {
      $get('<%= tbBirthDate.ClientID %>').disabled = false;
    $get('<%= tbBirthMonth.ClientID %>').disabled = true;
    $get('<%= tbBirthYear.ClientID %>').disabled = true;
    clearDateTextboxes();
}

//Обработчик кнопки "Дата рождения до месяца"
function rbBirthMonthChecked() {
    $get('<%= tbBirthDate.ClientID %>').disabled = true;
    $get('<%= tbBirthMonth.ClientID %>').disabled = false;
    $get('<%= tbBirthYear.ClientID %>').disabled = true;
    clearDateTextboxes();
}

//Обработчик кнопки "Дата рождения до года"
function rbBirthYearChecked() {
    $get('<%= tbBirthDate.ClientID %>').disabled = true;
        $get('<%= tbBirthMonth.ClientID %>').disabled = true;
        $get('<%= tbBirthYear.ClientID %>').disabled = false;
        clearDateTextboxes();
    }

    //Очищает даты
    function clearDateTextboxes() {
        $get('<%= tbBirthDate.ClientID %>').value = '';
        $get('<%= tbBirthMonth.ClientID %>').value = '';
        $get('<%= tbBirthYear.ClientID %>').value = '';
    }

    //Обработчик потери фокуса полем дата рождения до месяца
    function birthMonthBlur(tbBirthMonth) {
        VerifyMonth(tbBirthMonth);
        loadCategories();
        return true;
    }

    //Обработчик потери фокуса полем дата рождения до года
    function birthYearBlur(tbBirthYear) {
        VerifyYear(tbBirthYear);
        loadCategories();
        return true;
    }

    var SelectCategory = 0;
    var SelectDocumentType = 0;
    function loadCategories() {
        SelectCategory = $get('<%=ddlCategory.ClientID %>').value;
        $get('<%=ddlCategory.ClientID %>').options.length = 0;
        addCategory("-1", "Загружаются категории");
        var citizenshipId = $get('<%=ddlCitizenship.ClientID %>').value;
      var withoutCitizenship = $get('<%=chbWithoutCitizenship.ClientID %>').checked;
        var isRefugee = $get('<%=chbIsRefugee.ClientID %>').checked;

        PageMethods.GetCategory(citizenshipId, withoutCitizenship, isRefugee, getBirthDate(), onSuccessLoadCategories);
    }

    function onSuccessLoadCategories(response) {
        var j = 0;
        $get('<%=ddlCategory.ClientID %>').options.length = 0;
      addCategory("-1", "Выберите категорию");
      for (var i in response) {
          addCategory(response[i].Id, response[i].Name);
          if (response[i].Id == SelectCategory) {
              j = i;
          }
      }
      if (response.length > 0) {
          $get('<%=ddlCategory.ClientID %>').value = response[j].Id;

          //Сохранение значения в скрытом поле
          $get('<%=hfSelectedCategory.ClientID %>').value = response[j].Id;
      }

        //Загрузка типов документов
      loadDocumentTypes();
  }

    function addCategory(value, text) {
      var option = document.createElement("option");
      option.text = text;
      option.value = value;
      $get('<%=ddlCategory.ClientID %>').options.add(option);
    }

    //Обработчик изменения гражданства
    function citizenshipChanged() {
        var disabled = $get('<%=ddlCitizenship.ClientID %>').value == "190";
        if (disabled)
          $get('<%=chbIsRefugee.ClientID %>').checked = false;
        $get('<%=chbIsRefugee.ClientID %>').disabled = disabled;
      loadCategories();
    }

    function withoutCitizenshipChecked() {
      var checked = $get('<%=chbWithoutCitizenship.ClientID %>').checked
      $get('<%=ddlCitizenship.ClientID %>').value = "-1";
      $get('<%=ddlCitizenship.ClientID %>').disabled = checked;
      if (!checked) {
        $get('<%=ddlCitizenship.ClientID %>').value = "190";
        $get('<%=chbIsRefugee.ClientID %>').disabled = true;
      }
      loadCategories();
    }

    function isRefugeeChecked() {
        $get('<%=chbWithoutCitizenship.ClientID %>').disabled = $get('<%=chbIsRefugee.ClientID %>').checked;
        loadCategories();
    }

    function categoryChanged() {
        //Скрытие валидатора
        var validator = $get('<%= cvCategory.ClientID %>');
        if (validator != null)
            validator.style.visibility = 'hidden';
        //Загрузка типов документа
        loadDocumentTypes();

        //Сохранение значения в скрытом поле
        $get('<%=hfSelectedCategory.ClientID %>').value = $get('<%=ddlCategory.ClientID %>').value;
    }

    function getBirthDate() {
        if ($get('<%= rbBirthDate.ClientID %>').checked)
            return $get('<%= tbBirthDate.ClientID %>').value;
      if ($get('<%= rbBirthMonth.ClientID %>').checked)
            return $get('<%= tbBirthMonth.ClientID %>').value;
      if ($get('<%= rbBirthYear.ClientID %>').checked)
            return $get('<%= tbBirthYear.ClientID %>').value;
        return null;
    }

    function loadDocumentTypes() {
        SelectDocumentType = $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').value;
          // Загрузка докментов УДЛ
          $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').options.length = 0;
          addDocumentType("-1", "Загружаются типы документов");
          var categoryId = $get('<%=ddlCategory.ClientID %>').value;
        PageMethods.GetDocumentTypes(categoryId, getBirthDate(), onSuccessLoadDocumentTypes);

          // Загрузка Документов права проживания
        $get('<%= (documentResidency.FindControl("ddlDocumentType")).ClientID %>').options.length = 0;
        addDocumentResidencyType("-1", "Загружаются типы документов");
        PageMethods.GetDocumentResidencyTypes(categoryId, onSuccessLoadDocumentResidencyTypes);
    }

    function onSuccessLoadDocumentTypes(response) {
        $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').options.length = 0;
        addDocumentType("-1", "Выберите вид документа");

        var j = 0;
        for (var i in response) {
            addDocumentType(response[i].Id, response[i].Name);
            if (response[i].Id == SelectDocumentType) {
                j = i;
            }
        }
        if (response.length > 0) {
            $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').value = response[j].Id;

            //Сохранение значения в скрытом поле
            $get('<%= (documentUDL.FindControl("hfSelectedDocType")).ClientID %>').value = response[j].Id;
        }

        $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').onchange();
    }

    function onSuccessLoadDocumentResidencyTypes(response) {
        $get('<%= (documentResidency.FindControl("ddlDocumentType")).ClientID %>').options.length = 0;
          addDocumentResidencyType("-1", "Выберите вид документа");
          for (var i in response) {
              addDocumentResidencyType(response[i].Id, response[i].Name);
          }
          if (response.length > 0) {
              $get('<%=divDocumentResidency.ClientID%>').style.display = 'block';
        $get('<%= (documentResidency.FindControl("ddlDocumentType")).ClientID %>').value = response[0].Id;

        //Сохранение значения в скрытом поле
        $get('<%= (documentResidency.FindControl("hfSelectedDocType")).ClientID %>').value = response[0].Id;

    }
    else {
        $get('<%=divDocumentResidency.ClientID%>').style.display = 'none';
    }

    $get('<%= (documentResidency.FindControl("ddlDocumentType")).ClientID %>').onchange();
      }


      function addDocumentType(value, text) {
          var option = document.createElement("option");
          option.text = text;
          option.value = value;
          $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').options.add(option);
}

function addDocumentResidencyType(value, text) {
    var option = document.createElement("option");
    option.text = text;
    option.value = value;
    $get('<%= (documentResidency.FindControl("ddlDocumentType")).ClientID %>').options.add(option);
  }

</script>

<div class="wizardDiv">
    <br />
    <div class="headerTitles">
        <asp:Label ID="Label1" runat="server" Text="Введите персональные данные застрахованного лица"></asp:Label>
    </div>
    <table>
        <tr>
            <td class="regLeftColumn"></td>
            <td class="paddingLeft17">
                <asp:CheckBox ID="chbIsLastNameAbsent" runat="server" Text="Отсутствует в документе, удостоверяющем личность"
                    onclick="enabledisableLFMTextBox(this);" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="lblLastName" runat="Server" Text="Фамилия" />
            </td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbLastName" runat="Server" Width="100%" MaxLength="50" CssClass="lastName" onkeydown="lastNameChanged()" onkeypress="firstSymbolToUpper(this)" onpaste="return PasteFio(this, event);"/>
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvLastName" runat="server" Text=""
                    OnServerValidate="ValidateLastName" EnableClientScript="false" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn"></td>
            <td class="paddingLeft17">
                <asp:CheckBox ID="chbIsFirstNameAbsent" runat="server" Text="Отсутствует в документе, удостоверяющем личность"
                    onclick="enabledisableLFMTextBox(this);" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label2" runat="Server" Text="Имя" />
            </td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbFirstName" runat="Server" Width="100%" MaxLength="50" CssClass="firstName" onkeydown="firstNameChanged()" onkeypress="firstSymbolToUpper(this)" onpaste="return PasteFio(this, event);"/>
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
                    OnClientItemSelected="firstNameItemSelected " />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvFirstName" runat="server" Text=""
                    OnServerValidate="ValidateFirstName" EnableClientScript="false" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn"></td>
            <td class="paddingLeft17">
                <asp:CheckBox ID="chbIsMiddleNameAbsent" runat="server" Text="Отсутствует в документе, удостоверяющем личность"
                    onclick="enabledisableLFMTextBox(this);" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label3" runat="Server" Text="Отчество" />
            </td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbMiddleName" runat="Server" Width="100%" MaxLength="50" CssClass="middleName" onkeydown="middleNameChanged()" onkeypress="firstSymbolToUpper(this)" onblur="middleNameBlur(this)" onpaste="return PasteFio(this, event);"/>
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
                    OnClientItemSelected="middleNameItemSelected " />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvMiddleName" runat="server" Text=""
                    OnServerValidate="ValidateMiddleName" EnableClientScript="false" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label4" runat="Server" Text="Пол*" />
            </td>
            <td class="regRightColumn">
                <asp:DropDownList ID="ddlGender" runat="Server" Width="110px" OnChange="genderChanged()" CssClass="dropDowns">
                    <asp:ListItem Text="Выберите пол" Value="-1" />
                </asp:DropDownList>
                <asp:CustomValidator ID="cvGender" runat="server" EnableClientScript="false" Text="Укажите пол!" ControlToValidate="ddlGender" OnServerValidate="ValidateGender" CssClass="errorMessage" />
                <asp:CustomValidator ID="cvGenderConformity" runat="server" EnableClientScript="false" Text="Некорректный пол застрахованного!" ControlToValidate="ddlGender" OnServerValidate="ValidateGenderConformity" CssClass="errorMessage" />
            </td>
        </tr>
    </table>

    <table>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label5" runat="Server" Text="Дата рождения*" />
            </td>
            <td class="secondColumn">
                <asp:RadioButton ID="rbBirthDate" runat="server" Text="Известна полностью" GroupName="birthDateGroup"
                    Checked="True" onclick="rbBirthDateChecked()" />
            </td>
            <td class="thirdColumn">
                <uc:DateBox runat="server" ID="tbBirthDate" Width="100%" onblur="loadCategories();" CssClass="controlBoxes" />
            </td>
            <td style="padding-left: 5px">
                <asp:CheckBox ID="chBIsIncorrectDate" runat="server" Text="Несуществующая дата рождения в документе УДЛ"
                    CssClass="font12" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn"></td>
            <td class="secondColumn">
                <asp:RadioButton ID="rbBirthMonth" runat="server" Text="Известна с точностью до месяца"
                    GroupName="birthDateGroup" AutoPostBack="False" onclick="rbBirthMonthChecked()" />
            </td>
            <td class="thirdColumn">
                <asp:TextBox ID="tbBirthMonth" runat="Server" Width="100%" Enabled="False" AutoCompleteType="Disabled" CssClass="controlBoxes"
                    onPaste="javascript: return false;" onKeyDown="return CheckMonth(this);" onblur="return birthMonthBlur(this);" />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvBirthDate" runat="server" EnableClientScript="false" Text="Укажите дату рождения!" OnServerValidate="ValidateBirthDate" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn"></td>
            <td class="secondColumn">
                <asp:RadioButton ID="rbBirthYear" runat="server" Text="Известна с точностью до года"
                    GroupName="birthDateGroup" AutoPostBack="False" onclick="rbBirthYearChecked()" />
            </td>
            <td class="thirdColumn">
                <asp:TextBox ID="tbBirthYear" runat="Server" Width="100%" Enabled="False" AutoCompleteType="Disabled" CssClass="controlBoxes"
                    onPaste="javascript: return false;" onKeyDown="return CheckYear(this);" onblur="return birthYearBlur(this);" />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvBirthAndIssueDate" runat="server" EnableClientScript="false" Text="Дата рождения больше даты выдачи документа!" OnServerValidate="ValidateBirthAndIssueDate" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label7" runat="Server" Text="Место рождения*" />
            </td>
            <td class="regRightColumn">
                <asp:DropDownList ID="ddlBirthPlace" runat="Server" Width="230px" Height="22px" CssClass="dropDowns">
                    <asp:ListItem Text="Выберите страну рождения" Value="-1" />
                </asp:DropDownList>
                <asp:TextBox ID="tbBirthPlace" runat="Server" Width="315px" Style="margin-left: 10px;"
                    MaxLength="100" OnKeyDown="birthPlaceChanged()" CssClass="controlBoxes" />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvBirthPlace" runat="server" EnableClientScript="false" Text="" OnServerValidate="ValidateBirthPlace" />
            </td>

        </tr>
        <tr>
            <td class="regLeftColumn"></td>
            <td class="paddingLeft17">
                <asp:CheckBox ID="chBIsNotGuru" runat="server" Text="Не является высококвалифицированным специалистом или членом семьи специалиста в соответствии со 115-Ф3; военнослужащим или приравненным к ним лицом"
                    CssClass="font12" Checked="True" Visible="False" />
            </td>
        </tr>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label8" runat="Server" Text="СНИЛС" />
            </td>
            <td class="regRightColumn" colspan="2">
                <asp:TextBox ID="tbSnils" runat="Server" Width="200px" MaxLength="14" CssClass="controlBoxes" />
                <ajaxToolkit:MaskedEditExtender ID="mseSnils" runat="server" TargetControlID="tbSnils"
                    Mask="999-999-999 99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="LeftToRight"
                    AcceptNegative="None" ErrorTooltipEnabled="True" ClearMaskOnLostFocus="False" />
                <asp:CustomValidator ID="cvSnils" runat="server" EnableClientScript="false" Text="Контрольные цифры не соответствуют номеру или не полностью введен номер!"
                    ControlToValidate="tbSnils" OnServerValidate="ValidateSnils" CssClass="errorMessage" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:CheckBox ID="chbNotCheckDigitsSnils" runat="server" Text="Поле заполнено правильно" Visible="False" />
                <asp:CheckBox ID="chbNotCheckExistsSnils" runat="server" Text="СНИЛС действительно принадлежит данному лицу" Visible="False" />
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label14" runat="Server" Text="Гражданство*" />
            </td>
            <td class="regRightColumn">
                <asp:DropDownList ID="ddlCitizenship" runat="server" Width="300px" onchange="citizenshipChanged()" CssClass="dropDowns">
                    <asp:ListItem Text="Выберите гражданство" Value="-1" />
                </asp:DropDownList>
                <asp:CheckBox ID="chbWithoutCitizenship" runat="server" Text="Без гражданства" onclick="withoutCitizenshipChecked()" Style="margin-left: 5px;" />
                <asp:CheckBox ID="chbIsRefugee" runat="server" Text="Беженец" onclick="isRefugeeChecked()" Style="margin-left: 5px;" />
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvCitizenship" runat="server" EnableClientScript="false"
                    Text="Заполните гражданство или укажите его отсутствие!" ControlToValidate="ddlCitizenship"
                    OnServerValidate="ValidateCitizenship" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfSelectedCategory" runat="server" />
    <table>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label6" runat="Server" Text="Категория*" />
            </td>
            <td class="regRightColumn">
                <asp:DropDownList ID="ddlCategory" runat="Server" Width="100%" onchange="categoryChanged()" CssClass="dropDowns">
                    <asp:ListItem Text="Выберите категорию" Value="-1" />
                </asp:DropDownList>
            </td>
            <td class="errorMessage">
                <asp:CustomValidator ID="cvCategory" runat="server" EnableClientScript="false" Text="Категория заполнена неверно!"
                    ControlToValidate="ddlCategory" OnServerValidate="ValidateCategory" />
            </td>
        </tr>
    </table>
    <br />
    <div class="headerTitles">
        <asp:Label ID="Label10" runat="server" Text="Укажите сведения о документе, удостоверяющем личность"></asp:Label>
    </div>
    <div>
        <uc:DocumentUDL runat="server" ID="documentUDL" />
    </div>
    <div class="errorMessage">
        <asp:CustomValidator ID="cvDocument" runat="server" EnableClientScript="false" OnServerValidate="ValidateDocument" />
    </div>
    <br />
    <div id="divDocumentResidency" runat="server">
        <div class="headerTitles">
            <asp:Label ID="Label9" runat="server" Text="Укажите сведения о документе, подтверждающем право проживания в РФ"></asp:Label>
        </div>
        <div>
            <uc:DocumentUDL runat="server" ID="documentResidency" />
        </div>
        <div class="errorMessage">
            <asp:CustomValidator ID="cvDocumentResidecy" runat="server" EnableClientScript="false" OnServerValidate="ValidateDocumentResinedcy" />
        </div>
    </div>
    <%-- <table>
        <tr>
            <td class="regLeftColumn">
                <asp:Label ID="Label15" runat="Server" Text="Срок действия документа, подтверждающего право проживания в РФ*" />
            </td>
            <td class="regRightColumn">
                <asp:TextBox ID="tbValidityFrom" runat="Server" Width="143px" onKeyDown="return CheckDate(this);"
                    onblur="return VerifyDate(this);" onPaste="javascript: return false;" />&nbsp;
                <asp:TextBox ID="tbValidityTo" runat="Server" Width="143px" onKeyDown="return CheckDate(this);"
                    onblur="return VerifyDate(this);" onPaste="javascript: return false;" />
            </td>
        </tr>
    </table>--%>
</div>
