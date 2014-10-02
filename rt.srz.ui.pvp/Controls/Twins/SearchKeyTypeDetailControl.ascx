<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchKeyTypeDetailControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Twins.SearchKeyTypeDetailControl" %>

<script type="text/javascript">
  //Добавление масок к полям ввода серии и номера документа УДЛ в момент инициализации страницы
  $(document).ready(function () {
    addMasks();
  });

  //Добавление масок к полям ввода серии и номера документа УДЛ
  function addMasks() {
    $.mask.rules = { 'Я': /[А-ЯЁа-яё]/ }; //кирилические символы
    $('.maskTwin').setMask('Я-Я, Я-Я, Я-Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я, Я - Я');
  }

  function changeStateOfLengthTexbBox(checkbox) {
    if (checkbox.checked == true) {
      if (checkbox == $get('<%= chbUseLastName.ClientID %>')) {
        $get('<%= tbLastNameLength.ClientID %>').disabled = false;
      } else if (checkbox == $get('<%= chbUseFirstName.ClientID %>')) {
        $get('<%= tbFirstNameLength.ClientID %>').disabled = false;
      } else if (checkbox == $get('<%= chbUseMiddleName.ClientID %>')) {
        $get('<%= tbMiddleNameLength.ClientID %>').disabled = false;
      } else if (checkbox == $get('<%= chbUseBirthDate.ClientID %>')) {
        $get('<%= ddlBirtdateLength.ClientID %>').disabled = false;
      } else if (checkbox == $get('<%= chbUseRegistrationStreet.ClientID %>')) {
        $get('<%= tbRegistrationStreetLength.ClientID %>').disabled = false;
      } else if (checkbox == $get('<%= chbUseResidenceStreet.ClientID %>')) {
        $get('<%= tbResidenceStreetLength.ClientID %>').disabled = false;
      } else if (checkbox == $get('<%= chbDeleteTwinChar.ClientID %>')) {
        $get('<%= tbTwinChars.ClientID %>').disabled = false;
            }
} else {
  if (checkbox == $get('<%= chbUseLastName.ClientID %>')) {
        $get('<%= tbLastNameLength.ClientID %>').disabled = true;
    $get('<%= tbLastNameLength.ClientID %>').value = '';
  } else if (checkbox == $get('<%= chbUseFirstName.ClientID %>')) {
    $get('<%= tbFirstNameLength.ClientID %>').disabled = true;
    $get('<%= tbFirstNameLength.ClientID %>').value = '';
  } else if (checkbox == $get('<%= chbUseMiddleName.ClientID %>')) {
    $get('<%= tbMiddleNameLength.ClientID %>').disabled = true;
    $get('<%= tbMiddleNameLength.ClientID %>').value = '';
  } else if (checkbox == $get('<%= chbUseBirthDate.ClientID %>')) {
    $get('<%= ddlBirtdateLength.ClientID %>').disabled = true;
    $get('<%= ddlBirtdateLength.ClientID %>').value = '8';
  } else if (checkbox == $get('<%= chbUseRegistrationStreet.ClientID %>')) {
    $get('<%= tbRegistrationStreetLength.ClientID %>').disabled = true;
    $get('<%= tbRegistrationStreetLength.ClientID %>').value = '';
  } else if (checkbox == $get('<%= chbUseResidenceStreet.ClientID %>')) {
    $get('<%= tbResidenceStreetLength.ClientID %>').disabled = true;
    $get('<%= tbResidenceStreetLength.ClientID %>').value = '';
  } else if (checkbox == $get('<%= chbDeleteTwinChar.ClientID %>')) {
    $get('<%= tbTwinChars.ClientID %>').disabled = true;
      $get('<%= tbTwinChars.ClientID %>').value = '';
    }
}
}
</script>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление ключа поиска" CssClass="formTitle"></asp:Label>
</div>

<div style="display: table; margin: 0 auto;">
  <table>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbKeyCode" runat="server" Text="Код ключа:"></asp:Label>
      </td>
      <td style="width: 650px">
        <asp:TextBox ID="tbKeyCode" runat="server" MaxLength="50" Width="100%" CssClass="controlBoxes"></asp:TextBox>
      </td>
      <td class="errorMessage">
        <asp:RequiredFieldValidator ID="rfKeyCode" runat="server" Text="Укажите код ключа!" ControlToValidate="tbKeyCode" />
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbKeyName" runat="server" Text="Имя ключа:"></asp:Label>
      </td>
      <td style="width: 650px">
        <asp:TextBox ID="tbKeyName" runat="server" MaxLength="50" Width="100%" CssClass="controlBoxes"></asp:TextBox>
      </td>
      <td class="errorMessage">
        <asp:RequiredFieldValidator ID="rfKeyName" runat="server" Text="Укажите имя ключа!" ControlToValidate="tbKeyName" />
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbOperationKey" runat="server" Text="Тип операции:"></asp:Label>
      </td>
      <td style="width: 650px">
        <asp:DropDownList ID="ddlOperationKey" runat="server" Width="100%" CssClass="dropDowns">
        </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="Label1" runat="server" Text="Прикреплять по ключу:"></asp:Label>
      </td>
      <td style="width: 650px">
        <asp:CheckBox ID="chbInsertion" runat="server"></asp:CheckBox>
      </td>
    </tr>
  </table>
  <table style="margin-top: 5px; margin-left: -30px">
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseLastName" runat="server" Text="Использовать фамилию"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseLastName" runat="server" onclick="changeStateOfLengthTexbBox(this);"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbLastNameLength" runat="server" Text="Длина фамилии"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:TextBox ID="tbLastNameLength" runat="server" Width="100%" CssClass="controlBoxes"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseFirstName" runat="server" Text="Использовать имя"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseFirstName" runat="server" onclick="changeStateOfLengthTexbBox(this);"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbFirstNameLength" runat="server" Text="Длина имени"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:TextBox ID="tbFirstNameLength" runat="server" Width="100%" CssClass="controlBoxes"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseMiddleName" runat="server" Text="Использовать отчество"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseMiddleName" runat="server" onclick="changeStateOfLengthTexbBox(this);"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbMiddleNameLength" runat="server" Text="Длина отчества"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:TextBox ID="tbMiddleNameLength" runat="server" Width="100%" CssClass="controlBoxes"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseBirthDate" runat="server" Text="Использовать дату рождения"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseBirthDate" runat="server" onclick="changeStateOfLengthTexbBox(this);"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbBirtdateLength" runat="server" Text="Длина даты рождения"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:DropDownList ID="ddlBirtdateLength" runat="server" Width="100%" CssClass="dropDowns">
          <asp:ListItem Value="8" Text="Полностью" />
          <asp:ListItem Value="6" Text="Только год и месяц" />
          <asp:ListItem Value="4" Text="Только год" />
        </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseBirthPlace" runat="server" Text="Использовать место рождения"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseBirthPlace" runat="server" Width="100%"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseSnils" runat="server" Text="Использовать СНИЛС"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:CheckBox ID="chbUseSnils" runat="server" Width="100%"></asp:CheckBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseUDLType" runat="server" Text="Использовать тип УДЛ"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseUDLType" runat="server" Width="100%"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseUDLSeries" runat="server" Text="Использовать серию УДЛ"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:CheckBox ID="chbUseUDLSeries" runat="server" Width="100%"></asp:CheckBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseUDLNumber" runat="server" Text="Использовать номер УДЛ"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseUDLNumber" runat="server" Width="100%"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseOkato" runat="server" Text="Использовать OKATO"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:CheckBox ID="chbUseOkato" runat="server" Width="100%"></asp:CheckBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseRegistrationStreet" runat="server" Text="Использовать улицу адреса регистрации"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseRegistrationStreet" runat="server" onclick="changeStateOfLengthTexbBox(this);"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbRegistrationStreetLength" runat="server" Text="Длина улицы адреса регистрации"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:TextBox ID="tbRegistrationStreetLength" runat="server" Width="100%" CssClass="controlBoxes"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseRegistrationHouse" runat="server" Text="Использовать номер дома адреса регистрации"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseRegistrationHouse" runat="server" Width="100%"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseRegistrationRoom" runat="server" Text="Использовать номер квартиры адреса регистрации"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:CheckBox ID="chbUseRegistrationRoom" runat="server" Width="100%"></asp:CheckBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseResidenceStreet" runat="server" Text="Использовать улицу адреса проживания"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseResidenceStreet" runat="server" onclick="changeStateOfLengthTexbBox(this);"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbResidenceStreetLength" runat="server" Text="Длина улицы адреса проживания"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:TextBox ID="tbResidenceStreetLength" runat="server" Width="100%" CssClass="controlBoxes"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseResidenceHouse" runat="server" Text="Использовать номер дома адреса проживания"></asp:Label>
      </td>
      <td class="addKeyControlValuesFirstColumn">
        <asp:CheckBox ID="chbUseResidenceHouse" runat="server" Width="100%"></asp:CheckBox>
      </td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbUseResidenceRoom" runat="server" Text="Использовать номер квартиры адреса проживания"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:CheckBox ID="chbUseResidenceRoom" runat="server" Width="100%"></asp:CheckBox>
      </td>
    </tr>
    <tr>
      <td class="addKeyControlLabelsColumns"></td>
      <td class="addKeyControlValuesFirstColumn"></td>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbDeleteTwinChar" runat="server" Text="Устранять парные буквы"></asp:Label>
      </td>
      <td class="addKeyControlValuesSecondColumn">
        <asp:CheckBox ID="chbDeleteTwinChar" runat="server" Width="100%" onclick="changeStateOfLengthTexbBox(this);"></asp:CheckBox>
      </td>
    </tr>
  </table>
  <table>
    <tr>
      <td class="addKeyControlLabelsColumns">
        <asp:Label ID="lbTwinChars" runat="server" Text="Парные буквы"></asp:Label>
      </td>
      <td style="width: 650px">
        <asp:TextBox ID="tbTwinChars" runat="server" Width="100%" CssClass="maskTwin"></asp:TextBox>
      </td>
    </tr>
  </table>
</div>

<div class="separator">
</div>

<asp:Button ID="btnSave" runat="server" Text="Сохранить" OnClick="btnSave_Click" CssClass="buttons" />
<asp:Button ID="btnCancel" runat="server" Text="Отменить" OnClick="btnCancel_Click" CausesValidation="False" CssClass="buttons" />

