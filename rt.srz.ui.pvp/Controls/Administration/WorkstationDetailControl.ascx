<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkstationDetailControl.ascx.cs"
  Inherits="rt.srz.ui.pvp.Controls.Administration.WorkstationDetailControl" %>

<style type="text/css">
  .WindowsStyle .ajax__combobox_textboxcontainer {
    width: 100%;
  }

    .WindowsStyle .ajax__combobox_textboxcontainer input {
      width: 100%;
    }

  .WindowsStyle .ajax__combobox_itemlist li {
    white-space: nowrap;
    width: 100%;
    padding: 0 3px 0 2px;
  }
</style>

<script type="text/javascript">

  function ResetComboBox(ComboBoxCtrl) {
    if (ComboBoxCtrl) {
      ComboBoxCtrl._optionListHeight = null;
      ComboBoxCtrl._optionListWidth = null;
      //ComboBoxCtrl._buttonControl.style.height = '';
      //ComboBoxCtrl._buttonControl.style.width = '';
      //ComboBoxCtrl._buttonControl.style.margin = '';
      //ComboBoxCtrl._buttonControl.style.padding = '';
      //ComboBoxCtrl._highlightedIndex = 0;
    }
  }

  function InitializeComboBox(ComboBoxCtrl) {
    if (ComboBoxCtrl) {
      Sys.Application.removeComponent(ComboBoxCtrl._popupBehavior);
      ComboBoxCtrl.initializeOptionList();
      ComboBoxCtrl.initializeButton();
      ComboBoxCtrl._popupBehavior._visible = false;
    }
  }

  //список обработчиков карт
  function GetReaderList() {
    try {
      var controlReader = $find('<%= comboReaderName.ClientID%>');
      var controlSmcReader = $find('<%= comboReaderSmcName.ClientID%>');
      // You need to reset the Control
      ResetComboBox(controlReader);
      ResetComboBox(controlSmcReader);
      var currentReader = $get('<%=readerName.ClientID%>');
      var currentSmcReader = $get('<%=readerSmcName.ClientID%>');

      var currentReaderName = currentReader.value;
      var currentSmcReaderName = currentSmcReader.value;

      var readersStr = "";
      try {
        readersStr = window.uecReader.GetReaderList();
      }
      catch (er) {
      }
      var readers = readersStr.split(";");

      //удаляем все элементы из комбика (там был один элемент для корректной отрисовки)
      while (controlReader._optionListControl.hasChildNodes()) {
        controlReader._optionListControl.removeChild(controlReader._optionListControl.lastChild);
      }
      //добавляем пустой элемент в список
      var newItem = document.createElement('li');
      newItem.innerHTML = "";
      controlReader._optionListControl.appendChild(newItem);

      //удаляем все элементы из комбика (там был один элемент для корректной отрисовки)
      while (controlSmcReader._optionListControl.hasChildNodes()) {
        controlSmcReader._optionListControl.removeChild(controlSmcReader._optionListControl.lastChild);
      }
      //добавляем пустой элемент в список
      var newItem = document.createElement('li');
      newItem.innerHTML = "";
      controlSmcReader._optionListControl.appendChild(newItem);

      for (var i = 0; i < readers.length; i++) {
        if (readers[i] == "") {
          continue;
        }
        var newItem = document.createElement('li');
        newItem.innerHTML = readers[i];
        controlReader._optionListControl.appendChild(newItem);
      }

      for (var i = 0; i < readers.length; i++) {
        if (readers[i] == "") {
          continue;
        }
        var newItem = document.createElement('li');
        newItem.innerHTML = readers[i];
        controlSmcReader._optionListControl.appendChild(newItem);
      }

      try {
        InitializeComboBox(controlReader);
        InitializeComboBox(controlSmcReader);
      }
      catch (sd) {
      }

      //если в невидимое поле readerName уже сохранено значение какое-то, то надо его сделать выбранным
      controlReader.get_textBoxControl().value = currentReaderName;
      controlSmcReader.get_textBoxControl().value = currentSmcReaderName;

      //controlReader._buttonControl.disabled = false;
      //controlReader._optionListControl.disabled = false;
      //controlReader._textBoxControl.disabled = false;
    }
    catch (err) {
      ////alert(err.message);
    }
  }

  window.onload = SetSertificateControlsEnable;

  function SetSertificateControlsEnable() {
    //сделаем видимыми контролы для загрузки сертификатов
    //приходится делать это здесь так как при установке серверного свойства Visible в нажатии меню эти контролы появляются но оказываются непривязанными события complete
    //установка enable серверного после лоад в событии из меню не приносит никакого эффекта
    var disabled = $get('<%= tbName.ClientID%>').disabled;

    var upl = $get('<%= uTerminalGOST.ClientID%>');
    var uplElements = upl.getElementsByTagName('input');
    for (var i = 0; i < uplElements.length; i++) {
      uplElements[i].disabled = disabled;
    }

    var upl = $get('<%= uTerminalRSA.ClientID%>');
    var uplElements = upl.getElementsByTagName('input');
    for (var i = 0; i < uplElements.length; i++) {
      uplElements[i].disabled = disabled;
    }

    var upl = $get('<%= uPrivateTerminalGOST.ClientID%>');
    var uplElements = upl.getElementsByTagName('input');
    for (var i = 0; i < uplElements.length; i++) {
      uplElements[i].disabled = disabled;
    }

    $get('<%= comboReaderName.ClientID%>').disabled = disabled;
    $get('<%= comboReaderSmcName.ClientID%>').disabled = disabled;
  }

  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_endRequest(function (s, e) {
    //если добавили станцию то по умолчанию имя компьютера выставляем как имя текущей машины
    var wasAdded = $get('<%=wasAdded.ClientID%>');
    try {
      if (wasAdded.value == "add") {
        $get('<%= tbName.ClientID%>').value = window.uecReader.GetWorkstationName();
        wasAdded.value = "";
      }
    }
    catch (err) {
    }
    GetReaderList();
    SetSertificateControlsEnable();
  });

  //При смене значения ридера в выпадающем списке или вводе руками сохраним это выбранное значение в невидимое поле readerName
  function ClientChangeReader(sender) {
    var currentReader = $get('<%=readerName.ClientID%>');
    currentReader.value = sender.value;
  }

  //При смене значения ридера в выпадающем списке или вводе руками сохраним это выбранное значение в невидимое поле readerName
  function ClientChangeSmcReader(sender) {
    var currentReader = $get('<%=readerSmcName.ClientID%>');
    currentReader.value = sender.value;
  }


  function KeyStarted(sender) {
    $get('<%=btnSave.ClientID%>').disabled = true;
  }

  function KeyComplete(sender) {
    $get('<%=btnSave.ClientID%>').disabled = false;
    }

</script>

<asp:HiddenField ID="readerName" runat="server" />
<asp:HiddenField ID="readerSmcName" runat="server" />
<asp:HiddenField ID="wasAdded" runat="server" />

<div class="headerTitles">
  <asp:Label ID="lbDetailCaption" runat="server" Text="Рабочее место"></asp:Label>
</div>


<div style="clear: both;">
  <div class="admLabelControl">
    <div class="admControlPadding">
      <asp:Label ID="lbName" runat="server" Text="Имя компьютера"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbName" runat="server" MaxLength="50" Width="100%" CssClass="controlBoxes" />
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl">
    <div class="admControlPadding">
      <asp:Label ID="Label1" runat="server" Text="Наименование ридера для УЭК"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <ajaxToolkit:ComboBox runat="server" ID="comboReaderName" CssClass="WindowsStyle" onclientblur="ClientChangeReader(this)" EnableViewState="False">
        <asp:ListItem>Не указан</asp:ListItem>
      </ajaxToolkit:ComboBox>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl">
    <div class="admControlPadding">
      <asp:Label ID="Label3" runat="server" Text="Наименование ридера для ЭП"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <ajaxToolkit:ComboBox runat="server" ID="comboReaderSmcName" CssClass="WindowsStyle" onclientblur="ClientChangeSmcReader(this)" EnableViewState="False" Width="100%">
        <asp:ListItem>Не указан</asp:ListItem>
      </ajaxToolkit:ComboBox>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl">
    <div class="admControlPadding">
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:RadioButtonList ID="rblCrypto" runat="server">
        <asp:ListItem Selected="True" Value="GOST">Тип криптографии ГОСТ</asp:ListItem>
        <asp:ListItem Value="RSA">Тип криптографии RSA</asp:ListItem>
      </asp:RadioButtonList>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl">
    <div class="admControlPadding">
      <asp:Label ID="lbTerminalGOST" runat="server" Text="Сертификат терминала ГОСТ"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <ajaxToolkit:AsyncFileUpload runat="server" ID="uTerminalGOST" Width="100%" UploaderStyle="Modern"
        ThrobberID="TerminalGOST" CompleteBackColor="White" UploadingBackColor="#CCFFFF"
        OnUploadedComplete="uTerminalGOST_UploadComplete" OnClientUploadComplete="KeyComplete"
        OnClientUploadStarted="KeyStarted" />
      <asp:Label runat="server" ID="TerminalGOST" Text="загрузка..."></asp:Label>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl">
    <div class="admControlPadding">
      <asp:Label ID="lbTerminalRSA" runat="server" Text="Сертификат терминала RSA"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <ajaxToolkit:AsyncFileUpload runat="server" ID="uTerminalRSA" Width="100%" UploaderStyle="Modern"
        ThrobberID="TerminalRSA" CompleteBackColor="White" UploadingBackColor="#CCFFFF"
        OnUploadedComplete="uTerminalRSA_UploadComplete" OnClientUploadComplete="KeyComplete"
        OnClientUploadStarted="KeyStarted" />
      <asp:Label runat="server" ID="TerminalRSA" Text="загрузка..."></asp:Label>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl">
    <div class="admControlPadding">
      <asp:Label ID="Label2" runat="server" Text="Закрытый сертификат терминала ГОСТ"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <ajaxToolkit:AsyncFileUpload runat="server" ID="uPrivateTerminalGOST" Width="100%" UploaderStyle="Modern"
        ThrobberID="PrivateTerminalGOST" CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="uPrivateTerminalGOST_UploadComplete"
        OnClientUploadComplete="KeyComplete" OnClientUploadStarted="KeyStarted" />
      <asp:Label runat="server" ID="PrivateTerminalGOST" Text="загрузка..."></asp:Label>
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admErrorControl">
    <div class="admControlPadding">
      <div class="errorMessage">
        <asp:RequiredFieldValidator ID="rfName" runat="server" Text="Укажите имя компьютера!"
          ControlToValidate="tbName" Enabled="False" />
      </div>
    </div>
  </div>
</div>

<div class="separator">
</div>

<div style="clear: both;">
  <div>
    <asp:Button ID="btnSave" runat="server" Text="Сохранить" OnClick="btnSave_Click" CssClass="buttons" />
    <asp:Button ID="btnCancel" runat="server" Text="Отменить" OnClick="btnCancel_Click" CausesValidation="False" CssClass="buttons" />
  </div>
</div>

<div style="clear: both">
</div>
