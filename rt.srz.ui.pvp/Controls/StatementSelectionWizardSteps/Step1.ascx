<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step1.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps.Step1" %>
<%@ Import Namespace="rt.srz.model.srz.concepts" %>

<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<script type="text/javascript">

  //Подлключение обработчика ввода допустимых символов
  $(document).ready(function () {
    limitKeyPressForRoomNumber();
  });

  //Обработчик для комбо - "Причина подачи завления"
  function causeFillingChanged() {
    //скрытие валидатора
    var validator = window.$get('<%=cvCauseFiling.ClientID%>');
    if (validator != null)
      validator.style.visibility = 'hidden';
    switch (window.$get('<%=ddlCauseFiling.ClientID%>').value) {
        case '303':
            window.$get('<%=chbNewPolicy.ClientID%>').disabled = true;
            window.$get('<%=chbNewPolicy.ClientID%>').checked = true;
            window.$get('<%=ddlPolicyType.ClientID%>').disabled = false;
            window.$get('<%=chbHasPetition.ClientID%>').disabled = false;
            break;
            //Переоформление
        case '<%=CauseReneval.RenevalChangePersonDetails%>':
        case '<%=CauseReneval.RenevalInaccuracy%>':
        case '<%=CauseReneval.RenevalUnusable%>':
        case '<%=CauseReneval.RenevalLoss%>':
        case '<%=CauseReneval.RenevalExpiration%>':
            window.$get('<%=chbNewPolicy.ClientID%>').disabled = true;
            window.$get('<%=chbNewPolicy.ClientID%>').checked = true;
            window.$get('<%=chbHasPetition.ClientID%>').disabled = true;
            window.$get('<%=chbHasPetition.ClientID%>').checked = false;
            window.$get('<%=ddlPolicyType.ClientID%>').disabled = false;
        break;

        case '<%=CauseReneval.Edit%>': //Изменение данных о ЗЛ, не требующих выдачи нового полиса ОМС - запрещаем ввод данных
            window.$get('<%=chbNewPolicy.ClientID%>').disabled = true;
            window.$get('<%=chbNewPolicy.ClientID%>').checked = false;
            window.$get('<%=ddlPolicyType.ClientID%>').disabled = true;
            window.$get('<%=chbHasPetition.ClientID%>').disabled = true;
            window.$get('<%=chbHasPetition.ClientID%>').checked = false;
        break;

        default:
            window.$get('<%=chbNewPolicy.ClientID%>').disabled = false;
            window.$get('<%=ddlPolicyType.ClientID%>').disabled = false;
            window.$get('<%=chbHasPetition.ClientID%>').disabled = false;
        }

        newPolicyChecked();
    }

        //Обработчик для чекбокса - "Требуется выдача нового полиса"
    function newPolicyChecked() {
        window.$get('<%=hfNewPolicy.ClientID%>').value = window.$get('<%=chbNewPolicy.ClientID%>').checked;
        var dropDownListRef = window.$get('<%= ddlPolicyType.ClientID %>');
        var selectValue;
        if (window.$get('<%=chbNewPolicy.ClientID%>').checked == true) {
            selectValue = dropDownListRef.value;
            window.$get('<%=lbPolicyType.ClientID%>').innerHTML = "Тип полиса*";
            ////dropDownListRef.remove(3);
            dropDownListRef.value = selectValue;
            window.$get('<%=hfPolicyType.ClientID%>').value = selectValue;
        }
        else {
            window.$get('<%=lbPolicyType.ClientID%>').innerHTML = "Тип имеющегося полиса*";
            if (dropDownListRef.options.length == 3) {
              selectValue = dropDownListRef.value;
              dropDownListRef.value = selectValue;
              window.$get('<%=hfPolicyType.ClientID%>').value = selectValue;
        }
      }
      policyTypeChanged();
    }

    //скрытие валидатора
    function policyTypeChanged() {
    var validator = window.$get('<%=cvPolicyType.ClientID%>');
    if (validator != null)
        validator.style.visibility = 'hidden';

    //запись в скрытое поле
    window.$get('<%=hfPolicyType.ClientID%>').value = window.$get('<%=ddlPolicyType.ClientID%>').value;
    }

    //скрытие валидатора
    function dateFillingChanged() {
        var validator = window.$get('<%=rfvDateFilling.ClientID%>');
        if (validator != null)
            validator.style.visibility = 'hidden';
    }

    //скрытие валидатора
    function numberPolicyChanged() {
        var validator = window.$get('<%=cvNumberPolicy1.ClientID%>');
        if (validator != null)
            validator.style.visibility = 'hidden';
    }
</script>

<div class="wizardDiv">
  <asp:HiddenField ID="fStatus" runat="server" Value="285" />
  <br />
  <div class="headerTitles">
    <asp:Label ID="Label1" runat="server" Text="Укажите основные сведения о причине подачи заявления и выдаче нового полиса"></asp:Label>
  </div>
  <br />
  <br />
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="lblRegion" runat="Server" Text="Дата подачи заявления*" />
      </td>
      <td class="regRightColumn">
        <uc:DateBox runat="server" ID="tbDateFiling" Width="170px" onChange="dateFillingChanged()" CssClass="controlBoxes" />
      </td>
      <td class="errorMessage">
        <asp:RequiredFieldValidator ID="rfvDateFilling" runat="server" EnableClientScript="false" Text="Укажите дату подачи заявления!" ControlToValidate="tbDateFiling" />
      </td>
    </tr>
  </table>
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label3" runat="Server" Text="Причина подачи заявления*" />
      </td>
      <td class="regRightColumn">
        <asp:DropDownList ID="ddlCauseFiling" runat="Server" Width="100%" onChange="causeFillingChanged()" CssClass="dropDowns">
          <asp:ListItem Text="Выберите причину" Value="-1" />
        </asp:DropDownList>
      </td>
      <td class="errorMessage">
        <asp:CustomValidator ID="cvCauseFiling" runat="server" EnableClientScript="false" Text="Укажите причину подачи заявления!"
          ControlToValidate="ddlCauseFiling" OnServerValidate="ValidateCauseFiling" />
      </td>
    </tr>
  </table>
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label4" runat="Server" Text="Способ подачи*" />
      </td>
      <td class="regRightColumn">
        <asp:DropDownList ID="ddlModeFiling" runat="Server" Width="220px" CssClass="dropDowns" />
      </td>
    </tr>
  </table>
  <table>
    <tr>
      <td class="regLeftColumn"></td>
      <td class="regRightColumn">
        <asp:CheckBox ID="chbNewPolicy" runat="server" Text="Требуется выдача нового полиса" OnClick="newPolicyChecked()" Checked="True" />
        <asp:HiddenField ID="hfNewPolicy" runat="server" />
      </td>
    </tr>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="lbPolicyType" runat="Server" Text="Тип полиса*" />
      </td>
      <td class="regRightColumn">
        <asp:DropDownList ID="ddlPolicyType" runat="server" Width="100%" onChange="policyTypeChanged()" CssClass="dropDowns">
          <asp:ListItem Text="Выберите тип полиса" Value="-1" />
        </asp:DropDownList>
        <asp:HiddenField ID="hfPolicyType" runat="server" Value="-1" />
      </td>
      <td class="errorMessage">
        <asp:CustomValidator ID="cvPolicyType" runat="server" EnableClientScript="false" Text="Укажите тип полиса!"
          ControlToValidate="ddlPolicyType" OnServerValidate="ValidatePolicyType" />
      </td>
    </tr>
  </table>
  <table>
    <tr>
      <td class="regLeftColumn">
        <asp:Label ID="Label6" runat="Server" Text="Ранее выданный полис единого образца (ЕНП)" />
      </td>
      <td class="regRightColumn">
        <asp:TextBox ID="tbNumberPolicy" runat="Server" Width="200px" MaxLength="16" OnKeyDown="numberPolicyChanged()" onpaste="return PasteEnp(this, event);" CssClass="room" />
        <asp:CheckBox ID="cbNotCheckEnp" runat="server" Text="Значение введено правильно" Visible="False" />
      </td>
      <td class="errorMessage">
        <asp:CustomValidator ID="cvNumberPolicy1" runat="server" EnableClientScript="false" Text="Неправильная длина или контрольная сумма номера полиса!"
          ControlToValidate="tbNumberPolicy" OnServerValidate="ValidateNumberPolicy" />
      </td>
    </tr>
  </table>
  <table>
    <tr>
      <td class="regLeftColumn"></td>
      <td class="regRightColumn">
        <asp:CheckBox ID="chbHasPetition" runat="server" Text="Имеется ходатайство о регистрации в качестве застрахованного лица"
          Style="font-size: 14px;" />
      </td>
    </tr>
  </table>
</div>
