<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatementsSearch.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.StatementsSearch" %>
<%@ Import Namespace="rt.srz.model.dto" %>
<%@ Import Namespace="rt.srz.model.srz.concepts" %>
<%@ Import Namespace="rt.core.services" %>

<%@ Register Src="CustomPager/Pager.ascx" TagPrefix="uc" TagName="Pager" %>
<%@ Register Src="DocumentUDLUserControl.ascx" TagPrefix="uc" TagName="DocumentUDL" %>
<%@ Register Src="~/Controls/Common/ConfirmControl.ascx" TagName="ConfirmControl" TagPrefix="uc" %>

<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<style type="text/css">
  .auto-style1 {
    width: 48%;
  }
</style>

<script type="text/javascript">

  function ExpandFilter(sender, args) {
    $get('<%= FilterDiv.ClientID%>').style.display = "none";
  }

  function CollapseFilter(sender, args) {
    $get('<%= FilterDiv.ClientID%>').style.display = "block";

  }

  var readMode = true;

  // Вывод окна для ввода пина для УЭК карты
  function showUEKPinPad() {
    $find('<%= mpeUEKPinCode.ClientID%>').show();
    $get('<%= tbUEKPinCode.ClientID%>').focus();
  }

  // Обработчик отмены ввода пин для УЭК
  function UEKPinCanceled() {
    $get('<%= tbUEKPinCode.ClientID%>').value = "";
    $get('<%= UECLabelDiv.ClientID%>').style.display = 'none';
  }

  // Чтение электронного полиса
  function smardCardRead() {
    try {
      window.smardcardReader.OpenConnection(<%= string.Format("'{0}{1}'", AuthService.GetAuthToken().Signature, GetUrlUecGate()) %>);
      window.smardcardReader.SetCardReader();
      var cardInfo = window.smardcardReader.GetOwnerInfo();
      if (cardInfo != null) {
        $get('<%= tbFirstName.ClientID%>').value = cardInfo.FirstName;
        $get('<%= tbLastName.ClientID%>').value = cardInfo.LastName;
        $get('<%= tbMiddleName.ClientID%>').value = cardInfo.MiddleName;
        $get('<%= tbBirthDate.ClientID%>').value = cardInfo.Birthday;
        $get('<%= tbBirthPlace.ClientID%>').value = cardInfo.BirthPlace;
        $get('<%= tbSnils.ClientID%>').value = cardInfo.Snils;
        $get('<%= ddlGender.ClientID%>').value = cardInfo.Sex;

        $get('<%= tbPolicyNumber.ClientID%>').value = cardInfo.PolisNumber;
        $get('<%= tbDatePolicyFrom.ClientID%>').value = cardInfo.PolisDateFrom;
        $get('<%= tbDatePolicyTo.ClientID%>').value = cardInfo.PolisDateTo;

        var omsData = window.smardcardReader.GetCurrentSmo();
        PageMethods.GetTfomsAndSmoNames(omsData.Okato, omsData.OgrnSmo, onSuccessGet, onErrorGet);

        $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').value = '';
        $get('<%= (documentUDL.FindControl("tbSeries")).ClientID %>').value = '';
        $get('<%= (documentUDL.FindControl("tbNumber")).ClientID %>').value = '';
        $get('<%= (documentUDL.FindControl("tbIssuingAuthority")).ClientID %>').value = '';
        $get('<%= (documentUDL.FindControl("tbIssueDate")).ClientID %>').value = '';
      }
    } catch (e) {
      alert(e);
    }
  }

  //Непосредственно чтение
  function readUEC() {
    //Чтение приватных данных
    var cardInfo = window.uecReader.GetCardInfo();
    if (cardInfo.Result == 0) {
      $get('<%= tbFirstName.ClientID%>').value = cardInfo.FirstName;
      $get('<%= tbLastName.ClientID%>').value = cardInfo.LastName;
      $get('<%= tbMiddleName.ClientID%>').value = cardInfo.MiddleName;
      $get('<%= tbBirthDate.ClientID%>').value = cardInfo.Birthday;
      $get('<%= tbBirthPlace.ClientID%>').value = cardInfo.BirthPlace;
      $get('<%= tbSnils.ClientID%>').value = cardInfo.Snils;
      $get('<%= tbPolicyNumber.ClientID%>').value = cardInfo.PolisNumber;
      $get('<%= ddlGender.ClientID%>').value = cardInfo.Gender;
      $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').value = cardInfo.DocumentType;
      $get('<%= (documentUDL.FindControl("tbSeries")).ClientID %>').value = cardInfo.DocumentSeries;
      $get('<%= (documentUDL.FindControl("tbNumber")).ClientID %>').value = cardInfo.DocumentNumber;
      $get('<%= (documentUDL.FindControl("tbIssuingAuthority")).ClientID %>').value = cardInfo.DocumentIssueAuthority;
      if (cardInfo.DocumentIssueDate != undefined)
        $get('<%= (documentUDL.FindControl("tbIssueDate")).ClientID %>').value = cardInfo.DocumentIssueDate;
    }
    else {
      alert(cardInfo.ErrorString);
    }

    //Чтение данных о страховании
    var omsData = window.uecReader.ReadMainOmsData();
    if (omsData.Result == 0) {
      PageMethods.GetTfomsAndSmoNames(omsData.Okato, omsData.Ogrn, onSuccessGet, onErrorGet);
      $get('<%= tbDatePolicyFrom.ClientID%>').value = omsData.DateFrom;
      $get('<%= tbDatePolicyTo.ClientID%>').value = omsData.DateTo;
    }
    else {
      alert(omsData.ErrorString);
    }
  }

  function onSuccessGet(result) {
    var values = result.split(';');
    $get('<%= tbTfoms.ClientID%>').value = values[0];
    $get('<%= tbSmo.ClientID%>').value = values[1];
  }

  function onErrorGet(result) {
  }

  //Непосредственно запись
  function writeUEC() {
    var gridView = $get('<%= SearchResultGridView.ClientID%>');
    if (gridView == undefined)
      return;

    var index = $get('<%= hfSearchResultGVSelectedRowIndex.ClientID%>').value;
    if (index == "-1")
      return;
    var selectedRow = gridView.rows[parseInt(index) + 1];
    if (selectedRow == undefined)
      return;

    var lastName = selectedRow.cells[1].innerText;
    var firstName = selectedRow.cells[2].innerText;
    var midleName = selectedRow.cells[3].innerText;
    var birthDate = selectedRow.cells[4].innerText;
    var ogrn = selectedRow.cells[5].innerText;
    var okato = selectedRow.cells[6].innerText;
    var dateFrom = selectedRow.cells[7].innerText;
    var dateTo = selectedRow.cells[8].innerText;

    //Вызов ActiveX
    var resdata = window.uecReader.WriteOmsData(lastName, firstName, midleName, birthDate, ogrn, okato, dateFrom, dateTo);
    if (resdata.Result != 0) {
      alert(resdata.ErrorString);
    }
  }

  //Обработчик ввода пин для УЭК карты
  function UEKPinEntered() {
    var tbUEKPinCode = $get('<%= tbUEKPinCode.ClientID%>');

    var resa = window.uecReader.Authorize(tbUEKPinCode.value); //попытка авторизации
    tbUEKPinCode.value = '';
    if (resa.Result == 0) {
      if (readMode) {
        readUEC();  //чтение
      }
      else {
        writeUEC(); //запись
      }

      window.uecReader.CloseCard();  //закрытие карты
    }
    else {
      alert(resa.ErrorString);
      if (resa.PinRestTriesOut > 0)
        showUEKPinPad(); //повторный вывод окна для ввода пина
      else
        window.uecReader.CloseCard();  //закрытие карты
    }
    $get('<%= UECLabelDiv.ClientID%>').style.display = 'none';
  }

  //чтение УЭК карты
  function readUEСHandler() {
    $get('<%= UECLabelDiv.ClientID%>').style.display = 'inline-block';
    $get('<%= tbFirstName.ClientID%>').value = '';
    $get('<%= tbLastName.ClientID%>').value = '';
    $get('<%= tbMiddleName.ClientID%>').value = '';
    $get('<%= tbBirthDate.ClientID%>').value = '';
    $get('<%= tbBirthPlace.ClientID%>').value = '';
    $get('<%= tbSnils.ClientID%>').value = '';
    $get('<%= tbPolicyNumber.ClientID%>').value = '';
    $get('<%= tbTfoms.ClientID%>').value = '';
    $get('<%= tbSmo.ClientID%>').value = '';
    $get('<%= tbDatePolicyFrom.ClientID%>').value = '';
    $get('<%= tbDatePolicyTo.ClientID%>').value = '';
    $get('<%= (documentUDL.FindControl("ddlDocumentType")).ClientID %>').value = "-1";
    $get('<%= (documentUDL.FindControl("tbSeries")).ClientID %>').value = '';
    $get('<%= (documentUDL.FindControl("tbNumber")).ClientID %>').value = '';

    try {
      //открытие карты
      var res = window.uecReader.OpenCard(<%= string.Format("'{0}'", AuthService.GetAuthToken().Signature) %>); //открытие карты
      if (res.Result == 0) {
        readMode = true;
        //Вывод окна для ввода пина для УЭК карты
        showUEKPinPad();
      } else {
        alert(res.ErrorString);
        $get('<%= UECLabelDiv.ClientID%>').style.display = 'none';
      }
    } catch (e) {
      alert(e);
      $get('<%= UECLabelDiv.ClientID%>').style.display = 'none';
    }
  }

  //запись на УЭК карту
  function writeUECHandler() {
    $get('<%= UECLabelDiv.ClientID%>').style.display = 'inline-block';
    try {
      //открытие карты
      var res = window.uecReader.OpenCard(<%= string.Format("'{0}'", AuthService.GetAuthToken().Signature) %>); //открытие карты
      if (res.Result == 0) {
        readMode = false;
        //Вывод окна для ввода пина для УЭК карты
        showUEKPinPad();
      } else {
        alert(res.ErrorString);
        $get('<%= UECLabelDiv.ClientID%>').style.display = 'none';
      }
    } catch (e) {
      alert(e);
      $get('<%= UECLabelDiv.ClientID%>').style.display = 'none';
    }
  }

  // Create the event handlers for PageRequestManager
  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_beginRequest(PageRequestManager_beginRequest);

  prm.add_endRequest(PageRequestManager_endRequest);

  function PageRequestManager_endRequest(sender, args) {
    FillErrors();
  }

  window.onload = OnLoad;

  function OnLoad() {
    FillErrors();
  }

  function pageLoad() {
    var cb = $find("collapsibleBehavior");
    cb.add_expandComplete(ExpandFilter);
    cb.add_collapseComplete(CollapseFilter);
  }

  function PageRequestManager_beginRequest(sender, args) {
    var grid = $get('<%= SearchResultGridView.ClientID %>');
    var btnSearch = $get('<%= btnSearch.ClientID %>');
    var postbackElem = args.get_postBackElement();
    var pager = $get('<%= custPager.ClientID %>');

    if (btnSearch == postbackElem) {
      if (grid != null)
        grid.disabled = true;
      if (pager != null)
        pager.disabled = true;
      btnSearch.disabled = true;
    }
  }

  //Подлключение обработчика ввода допустимых символов
  $(document).ready(function () {
    SetDesignerSettingsDatesAndErrors();
    limitKeyPressForLFM();
    window.barcodeReader.Start('<%= StatementService.GetSettingCurrentUser("COMPort") != null ? StatementService.GetSettingCurrentUser("COMPort") : string.Empty %>');
  });

  //Закрытия окна для ввода пина по ESC
  $(document).keyup(function (event) {
    if (event.keyCode == 27) {//ESC
      var mpe = $find('<%= mpeUEKPinCode.ClientID%>');
      if (mpe != null && mpe != undefined) {
        mpe.hide();
        $get('<%= UECLabelDiv.ClientID%>').style.display = 'none';
      }
    }
  });

  //Обработчик выбора имени через Intellisense
  function firstNameItemSelected(sender, e) {
    //Присвоение id имени Intellisense для отчества для фильтрации
    $find('<%=aceMiddleNameIntellisense.ClientID%>').set_contextKey(e.get_value());
  }

  //Обработчик нажатия клавиши в одном из полей - Фамилия, Имя, Отчество
  function firstNameChanged() {
    //Исключение TAB
    if (event.which == null) {  // IE
      if (event.keyCode == 9)
        return;
    }

    //Сброс id имени Intellisense для отчества для фильтрации
    $find('<%=aceMiddleNameIntellisense.ClientID%>').set_contextKey(null);
  }

  //скрытиие валидатора
  function temporaryCertificateNumberChanged() {
    var validator = $get('<%=cvTemporaryCertificateNumber.ClientID%>');
    if (validator != null)
      validator.style.visibility = 'hidden';
  }

  function useFillingDateChecked() {
    SetDesignerSettingsDatesAndErrors();
  }

  function SetDesignerSettingsDatesAndErrors() {
    //если статус заявления выбран и это не статус отклонено, то список с ошибками скрываем
    var statusControl = $get('<%=ddlCertificateStatus.ClientID%>');
    var errorControl = $get('<%=ddlErrors.ClientID%>');
    switch (statusControl.value) {
      case '<%=StatusStatement.Declined%>':
      case '-1':
        errorControl.disabled = false;
        errorControl.value = '-1';
        $get('<%=hSelectedError.ClientID %>').value = "-1";
        break;
      default:
        errorControl.disabled = true;
        break;
    }
    ChangeDatesDesignerSettings();
  }

  function ChangeDatesDesignerSettings() {
    var usePeriod = $get('<%=chbUseDateFilling.ClientID%>').checked;
    var dateFromControl = $get('<%=tbDateFillingFrom.ClientID%>');
    var dateToControl = $get('<%=tbDateFillingTo.ClientID%>');
    dateFromControl.disabled = !usePeriod;
    dateToControl.disabled = !usePeriod;
    var lberror = $get('<%=lbErrors.ClientID%>');
    var tbxerror = $get('<%=ddlErrors.ClientID%>');
    var lbDates = $get('<%=lblDateFilling.ClientID%>');
    var lbDatesSeparator = $get('<%=Label8.ClientID%>');

    //скрываем даты если не выбрана птичка использовать период дат
    if (!usePeriod) {
      dateFromControl.style.display = "none";
      dateToControl.style.display = "none";
      lbDates.style.display = "none";
      lbDatesSeparator.style.display = "none";
    }
    else {
      dateFromControl.style.display = "block";
      dateToControl.style.display = "block";
      lbDates.style.display = "block";
      lbDatesSeparator.style.display = "block";
    }

    //скрываем список с ошибками если не используется период или ошибки недоступны для выбора 
    //(в случае смены статуса заявления может быть скрыт список с ошибками но при может быть выбрано использовать период дат)
    if (!usePeriod || tbxerror.disabled) {
      lberror.style.display = "none";
      tbxerror.style.display = "none";
    }
    else {
      lberror.style.display = "block";
      tbxerror.style.display = "block";
    }
  }

  function FillErrors() {
    var ddlErr = $get('<%=ddlErrors.ClientID%>');

    ddlErr.options.length = 0;
    var usePeriod = $get('<%=chbUseDateFilling.ClientID%>').checked;
    if (usePeriod) {
      var startDate = $get('<%=tbDateFillingFrom.ClientID%>').value;
      var endDate = $get('<%=tbDateFillingTo.ClientID%>').value;
      AddErrorItem("-1", "Загрузка ошибок...");
      PageMethods.GetErrors(startDate, endDate, onSuccessGetErrors, onErrorGetErrors);
    } else {
      $get('<%=hSelectedError.ClientID %>').value = "-1";
    }
  }

  function onSuccessGetErrors(result) {
    var ddlErr = $get('<%=ddlErrors.ClientID%>');
    var hSel = $get('<%=hSelectedError.ClientID %>');
    var oldhSelValue = hSel.value;
    hSel.value = "";

    ddlErr.options.length = 0;
    AddErrorItem("-1", "Данные не выбраны");
    $get('<%=ddlErrors.ClientID %>').value = -1;

    if (result == null) {
      return;
    }

    for (var i = 0; i < result.length; i++) {
      AddErrorItem(result[i], result[i]);
      if (oldhSelValue == result[i]) {
        hSel.value = oldhSelValue;
        $get('<%=ddlErrors.ClientID %>').value = oldhSelValue;
      }
    }

  }

  function AddErrorItem(displayValue, displayText) {
    var ddlErr = $get('<%=ddlErrors.ClientID%>');
    var option1 = document.createElement("option");
    option1.text = displayText;
    option1.value = displayValue;
    ddlErr.options.add(option1);
  }

  function onErrorGetErrors(result) {
    var ddlErr = $get('<%=ddlErrors.ClientID%>');
    ddlErr.options.length = 0;
    ddlErr.style.width = "120px";
    //debugger;
    AddErrorItem("-1", "Ошибка загрузки данных: " + result._message);
  }

  function ErrorsChanged() {
    //Сохранение значения в скрытом поле
    $get('<%=hSelectedError.ClientID %>').value = $get('<%=ddlErrors.ClientID %>').value;
  }

</script>

<%--Событие сканирования ШК--%>
<script for="barcodeReader" event="DataRecieved(resdata)" language="javascript">
  function barcodeReader::DataRecieved(resdata) {
    $get('<%= tbFirstName.ClientID%>').value = resdata.FirstName;
    $get('<%= tbLastName.ClientID%>').value = resdata.LastName;
    $get('<%= tbMiddleName.ClientID%>').value = resdata.Patronymic;
    $get('<%= tbBirthDate.ClientID%>').value = resdata.BirthDate;
    $get('<%= tbPolicyNumber.ClientID%>').value = resdata.PolicyNumber;

  }
</script>

<asp:HiddenField ID="hSelectedError" runat="server" />

<div>
  <div class="formTitle">
    <asp:Label ID="Label9" runat="server" Text="Заявления застрахованных лиц" />
  </div>

  <asp:UpdatePanel ID="MenuUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <ajaxToolkit:ModalPopupExtender ID="mpeUEKPinCode" runat="server"
        TargetControlID="btnFake"
        PopupControlID="pnUEKPinCode"
        OkControlID="btnUEKConfirmPin"
        CancelControlID="btnUEKCancelPin"
        BackgroundCssClass="modalBackground"
        OnOkScript="UEKPinEntered()"
        OnCancelScript="UEKPinCanceled()">
      </ajaxToolkit:ModalPopupExtender>
      <asp:Button ID="btnFake" runat="server" CssClass="HideButton" />
      <asp:Panel ID="pnUEKPinCode" runat="server" CssClass="modalPopup" DefaultButton="btnUEKConfirmPin">
        <div>
          <table>
            <tr>
              <td>
                <asp:Label ID="lbUEKPinCode" runat="server" Text="Введите Pin:" /></td>
              <td>
                <asp:TextBox ID="tbUEKPinCode" runat="server" TextMode="Password" MaxLength="4" /></td>
              <tr>
                <td>
                  <asp:Button ID="btnUEKConfirmPin" Text="Подтвердить" runat="server" />
                </td>
                <td>
                  <asp:Button ID="btnUEKCancelPin" Text="Отменить" runat="server" />
                </td>
          </table>
        </div>
      </asp:Panel>
      <asp:Menu ID="Menu" runat="server" StaticDisplayLevels="3" Orientation="Horizontal" OnMenuItemClick="MenuMenuItemClick" CssClass="ItemMenu" OnPreRender="MenuPreRender">
        <Items>
          <asp:MenuItem Text="Создать" Value="Reinsuranse" ImageUrl="~/Resources/search/empty.png"></asp:MenuItem>
          <asp:MenuItem Text="Переоформить" Value="Reneval" ImageUrl="~/Resources/search/full (2).png"></asp:MenuItem>
          <asp:MenuItem Text="Открыть" Value="Edit" ImageUrl="~/Resources/search/full.png"></asp:MenuItem>
          <asp:MenuItem Text="Выдать полис" Value="Issue" ImageUrl="~/Resources/search/full.png"></asp:MenuItem>
          <asp:MenuItem Text="Отменить" Value="Delete" ImageUrl="~/Resources/search/QampatykB (31).png"></asp:MenuItem>
          <asp:MenuItem Text="Прочитать УЭК" Value="ReadUEC" NavigateUrl="javascript:readUEСHandler()" ImageUrl="~/Resources/search/QampatykB (33).png"></asp:MenuItem>
          <asp:MenuItem Text="Записать на УЭК" Value="WriteUEC" NavigateUrl="javascript:writeUECHandler()" ImageUrl="~/Resources/search/QampatykB (77).png"></asp:MenuItem>
          <asp:MenuItem Text="Прочитать ЭП" Value="ReadSmardCard" NavigateUrl="javascript:smardCardRead()" ImageUrl="~/Resources/search/QampatykB (33).png"></asp:MenuItem>
          <%--           <asp:MenuItem Text="Разделить" Value="Separate" ImageUrl="~/Resources/search/QampatykB (37).png"></asp:MenuItem>--%>
          <asp:MenuItem Text="История" Value="InsuranceHistory" ImageUrl="~/Resources/search/QampatykB (87).png"></asp:MenuItem>
          <%--<asp:MenuItem Text="Отмена смерти" Value="DeleteDeathInfo" ImageUrl="~/Resources/search/QampatykB (5).png"></asp:MenuItem>--%>
        </Items>
      </asp:Menu>
    </ContentTemplate>
  </asp:UpdatePanel>


  <div id="UECLabelDiv" style="display: none" runat="server">
    <asp:Label ID="lbUECProcess" runat="server" Text="Выполняется операция с картой УЭК" CssClass="errorMessage" />
  </div>

  <div class="separator">
  </div>


  <asp:Panel ID="Panel1" runat="server" Width="100%">
    <div style="margin: 5px;">

      <asp:Image ID="Imaged" runat="server" />
      <asp:Label ID="lbText" runat="server" Text="Label" Font-Underline="True"></asp:Label>

      <div id="FilterDiv" runat="server" style="float: right; display: none">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <asp:Label ID="lbDatesF" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbFioF" runat="server" Text="ФИО: "></asp:Label>
            <asp:Label ID="lbUdlF" runat="server" Text="УДЛ: "></asp:Label>
            <asp:Label ID="lbSnilsF" runat="server" Text="СНИЛС: "></asp:Label>
            <asp:Label ID="lbBirthPlaceF" runat="server" Text="Место рождения: "></asp:Label>
            <asp:Label ID="lbDatebirthF" runat="server" Text="Дата рождения: "></asp:Label>
            <asp:Label ID="lbLastStatementsF" runat="server" Text="Отображаются последние заявления; " Font-Bold="true"></asp:Label>
            <asp:Label ID="lbErrorF" runat="server" Text=""></asp:Label>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>

      <div style="clear: both">
      </div>

    </div>
  </asp:Panel>



  <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapseControlID="Panel1" ExpandControlID="Panel1" TargetControlID="Panel2"
    CollapsedText="Отобразить фильтр" ExpandedText="Скрыть фильтр" TextLabelID="lbText" BehaviorID="collapsibleBehavior"
    ExpandedImage="~/Resources/collapse.png" CollapsedImage="~/Resources/expand.png" ImageControlID="Imaged">
  </ajaxToolkit:CollapsiblePanelExtender>

  <asp:Panel ID="Panel2" runat="server" Width="100%" Style="height: 0px; overflow: hidden;">

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
      <ContentTemplate>


        <div style="clear: both;">
          <div class="searchFilter1">
            <div class="searchPartsPadding">

              <div style="clear: both">
                <div class="searchPart1_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="lblDateFilling" runat="server" Text="Дата подачи заявления:" />
                  </div>
                </div>
                <div style="float: left; width: 15%">
                  <div class="searchPartsControlPadding">
                    <uc:DateBox runat="server" ID="tbDateFillingFrom" Width="100%" onblur="FillErrors();" CssClass="controlBoxes" />
                  </div>
                </div>
                <div style="float: left; width: 5%; text-align: center">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label8" runat="server" Text="-" />
                  </div>
                </div>
                <div style="float: left; width: 15%">
                  <div class="searchPartsControlPadding">
                    <uc:DateBox runat="server" ID="tbDateFillingTo" Width="100%" Enabled="False" onblur="FillErrors();" CssClass="controlBoxes" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart1_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="lbErrors" runat="server" Text="Причина отклонения заявления" Style=""></asp:Label>
                  </div>
                </div>
                <div class="searchPart1_2">
                  <div class="searchPartsControlPadding">
                    <asp:DropDownList ID="ddlErrors" runat="server" onchange="ErrorsChanged()" Width="100%" CssClass="dropDowns">
                      <asp:ListItem>Данные не выбраны</asp:ListItem>
                    </asp:DropDownList>
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart1_1">
                  <div class="searchPartsControlPadding">
                  </div>
                </div>
                <div class="searchPart1_2">
                  <div class="searchPartsControlPadding">
                    <asp:CheckBox ID="chbUseDateFilling" runat="server" Text="Учитывать дату подачи при поиске:" Checked="False" onClick="FillErrors(); useFillingDateChecked()" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart1_1">
                  <div class="searchPartsControlPadding">
                  </div>
                </div>
                <div class="searchPart1_2">
                  <div class="searchPartsControlPadding">
                    <asp:CheckBox ID="cbReturnLastStatement" runat="server" Text="Показать последние заявления ЗЛ" Checked="True" />
                  </div>
                </div>
              </div>

              <div style="clear: both;">
                <div>
                  <div style="margin-left: 1px">
                    <uc:DocumentUDL runat="server" ID="documentUDL" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart1_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label12" runat="server" Text="Номер полиса:" />
                  </div>
                </div>
                <div style="float: left; width: 35%">
                  <div class="searchPartsControlPadding">
                    <asp:TextBox ID="tbPolicyNumber" runat="Server" Width="100%" MaxLength="16" CssClass="controlBoxes" />
                  </div>
                </div>
                <div style="float: left; width: 5%; text-align: right;">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label13" runat="server" Text="c:" Width="100%" />
                  </div>
                </div>
                <div style="float: left; width: 15%">
                  <div class="searchPartsControlPadding">
                    <uc:DateBox runat="server" ID="tbDatePolicyFrom" Width="100%" Enabled="false" CssClass="controlBoxes" />
                  </div>
                </div>
                <div style="float: left; width: 5%; text-align: right;">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label17" runat="server" Text="по:" Width="100%" />
                  </div>
                </div>
                <div style="float: left; width: 15%">
                  <div class="searchPartsControlPadding">
                    <uc:DateBox runat="server" ID="tbDatePolicyTo" Width="100%" Enabled="false" CssClass="controlBoxes" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart1_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label1" runat="server" Text="Номер ВС:" />
                  </div>
                </div>
                <div style="float: left; width: 35%">
                  <div class="searchPartsControlPadding">
                    <asp:TextBox ID="tbTemporaryCertificateNumber" runat="Server" Width="100%" MaxLength="9" OnKeyDown="temporaryCertificateNumberChanged()" CssClass="controlBoxes" />
                  </div>
                </div>
                <div style="float: left; width: 40%">
                  <div class="errorMessage">
                    <asp:CustomValidator ID="cvTemporaryCertificateNumber" runat="server" EnableClientScript="false" Text="" OnServerValidate="ValidateTemporaryCertificateNumber" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart1_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label14" runat="server" Text="Наименование СМО:" />
                  </div>
                </div>
                <div class="searchPart1_2">
                  <div class="searchPartsControlPadding">
                    <asp:TextBox ID="tbSmo" runat="Server" Width="100%" Enabled="False" CssClass="controlBoxes" />
                  </div>
                </div>
              </div>

            </div>
          </div>

          <div class="searchFilter2">
            <div class="searchPartsPadding">

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label2" runat="server" Text="Тип заявления:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:DropDownList ID="ddlCertificateType" runat="Server" Width="100%" CssClass="dropDowns">
                      <asp:ListItem Text="Выберите тип" Value="-1" />
                    </asp:DropDownList>
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label3" runat="server" Text="Статус заявления:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:DropDownList ID="ddlCertificateStatus" runat="Server" Width="100%" CssClass="dropDowns" onchange="SetDesignerSettingsDatesAndErrors();">
                      <asp:ListItem Text="Выберите статус" Value="-1" />
                    </asp:DropDownList>
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label4" runat="server" Text="Фамилия:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:TextBox ID="tbLastName" runat="Server" Width="100%" MaxLength="50" CssClass="lastName" onkeypress="firstSymbolToUpper(this)" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label5" runat="server" Text="Имя:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:TextBox ID="tbFirstName" runat="Server" Width="100%" MaxLength="50" CssClass="firstName" onkeydown="firstNameChanged()" onkeypress="firstSymbolToUpper(this)" />
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
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label6" runat="server" Text="Отчество:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:TextBox ID="tbMiddleName" runat="Server" Width="100%" MaxLength="50" CssClass="middleName" onkeypress="firstSymbolToUpper(this)" />
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
                      UseContextKey="true" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label10" runat="server" Text="Дата рождения:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <uc:DateBox runat="server" ID="tbBirthDate" Width="100%" CssClass="controlBoxes" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label11" runat="server" Text="Место рождения:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:TextBox ID="tbBirthPlace" runat="Server" MaxLength="100" Width="100%" CssClass="controlBoxes" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label7" runat="server" Text="СНИЛС:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:CheckBox ID="cbNotCheckSnils" runat="server" Text="Поле введено правильно" Visible="false" />
                    <asp:TextBox ID="tbSnils" runat="Server" Width="100%" CssClass="controlBoxes" />
                    <ajaxToolkit:MaskedEditExtender ID="mseSnils" runat="server" TargetControlID="tbSnils"
                      Mask="999-999-999 99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                      OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="LeftToRight"
                      AcceptNegative="None" ErrorTooltipEnabled="True" ClearMaskOnLostFocus="False" />
                    <asp:CustomValidator ID="cvSnils" runat="server" EnableClientScript="false" Text="Контрольные цифры СНИЛС не соответствуют номеру или не полностью введён номер!"
                      ControlToValidate="tbSnils" OnServerValidate="ValidateSnils" CssClass="errorMessage" />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label16" runat="Server" Text="Пол" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:DropDownList ID="ddlGender" runat="Server" Width="110px" Enabled="False" CssClass="dropDowns">
                      <asp:ListItem Text="" Value="-1" />
                    </asp:DropDownList>
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="searchPart2_1">
                  <div class="searchPartsControlPadding">
                    <asp:Label ID="Label15" runat="server" Text="Наименование ТФОМС:" />
                  </div>
                </div>
                <div class="searchPart2_2">
                  <div class="searchPartsControlPadding">
                    <asp:TextBox ID="tbTfoms" runat="Server" Width="100%" Enabled="False" CssClass="controlBoxes" />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

      </ContentTemplate>
    </asp:UpdatePanel>

  </asp:Panel>


  <asp:UpdatePanel ID="CommonUpdatePanel" runat="server">
    <ContentTemplate>

      <asp:UpdatePanel ID="SearchUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <div style="clear: both">
            <div style="float: right">
              <div class="searchPartsControlPadding">
                <asp:Button ID="btnClear" runat="server" Text="Очистить" OnClick="BtnClearClick" Height="30px" Width="75px" CssClass="buttons" />
              </div>
            </div>
            <div style="float: right">
              <div class="searchPartsControlPadding">
                <asp:Button ID="btnSearch" runat="server" Text="Искать" OnClick="BtnSearchClick" Height="30px" Width="75px" CssClass="buttons" UseSubmitBehavior="true" />
              </div>
            </div>
          </div>
          <div style="clear: both">
            <asp:Label ID="lbSeachError" runat="server" CssClass="errorMessage" />
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>

      <div class="updatePanelWithProgressDiv">
        <asp:UpdateProgress ID="SearchUpdateProgress" runat="server" AssociatedUpdatePanelID="SearchUpdatePanel">
          <ProgressTemplate>
            <div class="updateProgressPositionDiv">
              <div class="updateProgress">
                <div style="padding: 10px">
                  <span><b>Производится поиск...</b></span> &nbsp;&nbsp;&nbsp;
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/ajax-loader.gif" />
                </div>
              </div>
            </div>
          </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="GridUpdateProgress" runat="server" AssociatedUpdatePanelID="GridUpdatePanel">
          <ProgressTemplate>
            <div class="updateProgressPositionDiv">
              <div class="updateProgress">
                <div style="padding: 10px">
                  <span><b>Ожидайте...</b></span> &nbsp;&nbsp;&nbsp;
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Resources/ajax-loader.gif" />
                </div>
              </div>
            </div>
          </ProgressTemplate>
        </asp:UpdateProgress>
      </div>

      <uc:ConfirmControl ID="confirmDelete" runat="server" Message="Вы уверены, что хотите отменить заявление?" />
      <uc:ConfirmControl ID="confirmDeath" runat="server" Message="Вы уверены, что хотите отменить смерть?" />

      <asp:HiddenField ID="hfScrollPosition" runat="server" Value="0" />

      <asp:UpdatePanel ID="GridUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <div class="dataNotFound" id="notFoundData" runat="server" visible="false">
            <asp:Label ID="Label18" runat="server" Text="Данные не найдены" />
          </div>

          <div runat="server" id="scrollArea" style="overflow: auto;" visible="false">

            <div class="separator">
            </div>

            <asp:HiddenField ID="hfSearchResultGVSelectedRowIndex" runat="server" />
            <asp:GridView ID="SearchResultGridView" runat="server" EnableModelValidation="True" Style="width: 100%"
              AutoGenerateColumns="False" AllowSorting="True" OnRowDataBound="SearchResultGridViewRowDataBound"
              OnSorting="SearchResultGridViewSorting" OnSelectedIndexChanged="SearchResultGridViewSelectedIndexChanged"
              OnRowCommand="SearchResultGridViewRowCommand" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both">
              <HeaderStyle CssClass="GridHeaderS" />
              <RowStyle CssClass="GridRowStyleS" />
              <SelectedRowStyle CssClass="GridSelectedRowStyleS" />
              <Columns>
                <asp:BoundField DataField="FromCurrentSmo" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:BoundField>
                <asp:BoundField DataField="PersonStatus" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:BoundField>
                <asp:BoundField DataField="FirstName" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:BoundField>
                <asp:BoundField DataField="MiddleName" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:BoundField>
                <asp:TemplateField ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                  <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).Birthday.ToString("dd.MM.yyyy") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SmoOGRN" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:BoundField>
                <asp:BoundField DataField="TfomOKATO" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Дата подачи" SortExpression="DateFiling">
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                  <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).DateFiling.ToString("dd.MM.yyyy") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                  <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).DateInsuranceEnd.ToString("dd.MM.yyyy") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField SelectText="Выбор" ShowSelectButton="true" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                  <HeaderStyle CssClass="HideButton" />
                  <ItemStyle CssClass="HideButton" />
                </asp:CommandField>
                <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton"></asp:ButtonField>
                <asp:TemplateField HeaderText="Статус заявления">
                  <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# ((SearchStatementResult) Container.DataItem).StatusStatement %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="СМО" SortExpression="SMO">
                  <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).Smo ?? "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Фамилия" SortExpression="LastName">
                  <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).LastName ?? "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Имя" SortExpression="FirstName">
                  <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).FirstName ?? "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Отчество" SortExpression="MiddleName">
                  <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).MiddleName ?? "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Пол" SortExpression="Gender">
                  <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).Gender ?? "&nbsp"%>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Дата рождения" SortExpression="Birthday">
                  <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).Birthday.ToString("dd.MM.yyyy") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Гражданство" SortExpression="Citizenship">
                  <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).Citizenship ?? "&nbsp"%>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Тип док. УДЛ" SortExpression="DocumentType">
                  <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).DocumentType ?? "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Серия и № документа" SortExpression="Documentid">
                  <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).DocumentSeria == null ? ((SearchStatementResult)Container.DataItem).DocumentNumber :
                                        ((SearchStatementResult)Container.DataItem).DocumentSeria + " № " +((SearchStatementResult)Container.DataItem).DocumentNumber %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Birthplace" HeaderText="Место рождения" />
                <asp:TemplateField HeaderText="СНИЛС" SortExpression="SNILS">
                  <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" Text='<%# ((SearchStatementResult)Container.DataItem).Snils ?? "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PolicyNumber" HeaderText="Номер полиса" />
                <asp:TemplateField HeaderText="Номер ВС" SortExpression="NumberTemporaryCertificate">
                  <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" Text='<%#  ((SearchStatementResult)Container.DataItem).NumberTemporaryCertificate ?? "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AddressRegistrationStr" HeaderText="Адрес регистрации" />
                <asp:BoundField DataField="AddressLiveStr" HeaderText="Адрес проживания" />
                <asp:TemplateField HeaderText="Типовая СРЗ">
                  <ItemTemplate>
                    <asp:Label ID="Label16" runat="server" Text='<%# ((SearchStatementResult) Container.DataItem).Errors.Any()? "Есть ошибки" : 
                                             ((SearchStatementResult) Container.DataItem).IsSinhronized ? "Синхронизовано" :  "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Тип заявления">
                  <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%# ((SearchStatementResult) Container.DataItem).TypeStatement %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Причина подачи" SortExpression="CauseFilling">
                  <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" Text='<%#((SearchStatementResult)Container.DataItem).CauseFiling ?? "&nbsp" %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>

              </Columns>
            </asp:GridView>
          </div>
          <div class="pagerPadding">
            <uc:Pager ID="custPager" runat="server" OnPageIndexChanged="CustPagerPageIndexChanged"
              OnPageSizeChanged="CustPagerPageSizeChanged" />
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>

    </ContentTemplate>
  </asp:UpdatePanel>

</div>
