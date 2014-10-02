<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmoDetailControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.SmoDetailControl" %>

<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<script type="text/javascript">

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
  
  function KeyStarted(sender) {
    ////$get('<%=btnTurn.ClientID%>').disabled = true;
   }

   function KeyComplete(sender) {
     ////$get('<%=btnTurn.ClientID%>').disabled = false;
    }

</script>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление СМО"></asp:Label>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="lbName" runat="server" Text="Краткое название"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbShortName" runat="server" MaxLength="250" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label6" runat="server" Text="Фамилия"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbLastName" runat="server" MaxLength="250" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label1" runat="server" Text="Полное название"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbFullName" runat="server" MaxLength="250" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label7" runat="server" Text="Имя"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbFirstName" runat="server" MaxLength="250" Width="100%" CssClass="firstName" onkeydown="firstNameChanged()" onkeypress="firstSymbolToUpper(this)"></asp:TextBox>
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
        OnClientItemSelected="firstNameItemSelected" Enabled="True" />
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label2" runat="server" Text="Территориальный фонд"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:DropDownList ID="ddlFoms" runat="server" Width="100%" DataTextField="ShortName" DataValueField="ID" CssClass="dropDowns"></asp:DropDownList>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label8" runat="server" Text="Отчество"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbMiddleName" runat="server" MaxLength="250" Width="100%" onkeypress="firstSymbolToUpper(this)" CssClass="controlBoxes"></asp:TextBox>
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
        UseContextKey="true" Enabled="True" />
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label3" runat="server" Text="Код"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbCode" runat="server" MaxLength="20" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label9" runat="server" Text="Телефон"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbPhone" runat="server" MaxLength="100" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label4" runat="server" Text="ИНН"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbInn" runat="server" MaxLength="10" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label10" runat="server" Text="Факс"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbFax" runat="server" MaxLength="100" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label5" runat="server" Text="ОГРН"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbOgrn" runat="server" MaxLength="13" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label11" runat="server" Text="Email"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbEmail" runat="server" MaxLength="100" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label12" runat="server" Text="Вебсайт"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbWebsite" runat="server" MaxLength="100" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label16" runat="server" Text="Адрес"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbAddress" runat="server" MaxLength="500" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label13" runat="server" Text="Дата включения"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <uc:DateBox runat="server" ID="tbDateInclude" Width="100%"  CssClass="controlBoxes"/>
      <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="tbDateInclude" runat="server" />
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label14" runat="server" Text="Дата исключения"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <uc:DateBox runat="server" ID="tbDateExclude" Width="100%"  CssClass="controlBoxes"/>
      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="tbDateExclude" runat="server" />
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label15" runat="server" Text="Дата лицензирования"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <uc:DateBox runat="server" ID="tbDateLicensing" Width="100%"  CssClass="controlBoxes"/>
      <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="tbDateLicensing" runat="server" />
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label19" runat="server" Text="Дата окончания срока действия лицензии"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <uc:DateBox runat="server" ID="tbDateExpiryLicense" Width="100%"  CssClass="controlBoxes"/>
      <ajaxToolkit:CalendarExtender ID="CalendarExtender5" TargetControlID="tbDateExpiryLicense" runat="server" />
    </div>
  </div>
</div>

<div style="clear: both">
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label17" runat="server" Text="Номер лицензии"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbLicenseNumber" runat="server" MaxLength="50" Width="100%"  CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="admLabelControl1_4">
    <div class="admControlPadding">
      <asp:Label ID="Label18" runat="server" Text="Лицензия"></asp:Label>
    </div>
  </div>
  <div class="admValueControl1_4">
    <div class="admControlPadding">
      <asp:TextBox ID="tbLicense" runat="server" MaxLength="100" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
</div>

<%--<div runat="server" id="sertificateDiv">

  <div style="clear: both">
    <div class="admLabelControl1_4">
      <div class="admControlPadding">
        <asp:Label ID="lbUC1GOST" runat="server" Text="Сертификат  УЦ №1 ГОСТ"></asp:Label>
      </div>
    </div>
    <div class="admValueControl1_4">
      <div class="admControlPadding">
        <ajaxToolkit:AsyncFileUpload runat="server" ID="uUC1GOST" Width="100%" UploaderStyle="Modern"
          ThrobberID="UC1GOST" CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="uUC1GOST_UploadComplete"
          OnClientUploadComplete="KeyComplete" OnClientUploadStarted="KeyStarted" />
        <asp:Label runat="server" ID="UC1GOST" Text="загрузка..."></asp:Label>
      </div>
    </div>
    <div class="admLabelControl1_4">
      <div class="admControlPadding">
        <asp:Label ID="lbUC1RSA" runat="server" Text="Сертификат  УЦ №1 RSA"></asp:Label>
      </div>
    </div>
    <div class="admValueControl1_4">
      <div class="admControlPadding">
        <ajaxToolkit:AsyncFileUpload runat="server" ID="uUC1RSA" Width="100%" UploaderStyle="Modern"
          ThrobberID="UC1RSA" CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="uUC1RSA_UploadComplete"
          OnClientUploadComplete="KeyComplete" OnClientUploadStarted="KeyStarted" />
        <asp:Label runat="server" ID="UC1RSA" Text="загрузка..."></asp:Label>
      </div>
    </div>
  </div>

  <div style="clear: both">
    <div class="admLabelControl1_4">
      <div class="admControlPadding">
        <asp:Label ID="lbOKO1GOST" runat="server" Text="Сертификат ОКО №1 ГОСТ"></asp:Label>
      </div>
    </div>
    <div class="admValueControl1_4">
      <div class="admControlPadding">
        <ajaxToolkit:AsyncFileUpload runat="server" ID="uOKO1GOST" Width="100%" UploaderStyle="Modern"
          ThrobberID="OKO1GOST" CompleteBackColor="White" UploadingBackColor="#CCFFFF"
          OnUploadedComplete="uOKO1GOST_UploadComplete" OnClientUploadComplete="KeyComplete"
          OnClientUploadStarted="KeyStarted" />
        <asp:Label runat="server" ID="OKO1GOST" Text="загрузка..."></asp:Label>
      </div>
    </div>
    <div class="admLabelControl1_4">
      <div class="admControlPadding">
        <asp:Label ID="lbOKO1RSA" runat="server" Text="Сертификат ОКО №1 RSA"></asp:Label>
      </div>
    </div>
    <div class="admValueControl1_4">
      <div class="admControlPadding">
        <ajaxToolkit:AsyncFileUpload runat="server" ID="uOKO1RSA" Width="100%" UploaderStyle="Modern"
          ThrobberID="OKO1RSA" CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="uOKO1RSA_UploadComplete"
          OnClientUploadComplete="KeyComplete" OnClientUploadStarted="KeyStarted" />
        <asp:Label runat="server" ID="OKO1RSA" Text="загрузка..."></asp:Label>
      </div>
    </div>
  </div>

</div>--%>

<asp:UpdatePanel ID="turnUpdatePanel" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <div style="clear: both">
      <div class="admLabelControl1_4">
        <div class="admControlPadding">
        </div>
      </div>
      <div class="admValueControl1_4">
        <div class="admControlPadding">
          <asp:Button ID="btnTurn" runat="server" Text="Включить" OnClick="btnTurn_Click" CssClass="buttons"/>
        </div>
      </div>
    </div>
  </ContentTemplate>
</asp:UpdatePanel>

<div style="clear: both">
  <div class="errorMessage">
    <asp:RequiredFieldValidator ID="rfName" runat="server" Text="Укажите полное название!" ControlToValidate="tbFullName" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="Укажите краткое название!" ControlToValidate="tbShortName" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="Укажите территориальный фонд!" ControlToValidate="ddlFoms" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="Укажите код!" ControlToValidate="tbCode" />
    <asp:CustomValidator ID="vCode" runat="server" ErrorMessage="СМО с таким кодом уже существует!" OnServerValidate="vCode_ServerValidate" ControlToValidate="tbCode"></asp:CustomValidator>
    <asp:RequiredFieldValidator ID="rInn" runat="server" Text="Укажите ИНН!" ControlToValidate="tbInn" />
    <asp:RegularExpressionValidator ID="vEmail" runat="server"
      ControlToValidate="tbEmail" ValidationExpression="^([A-Za-z0-9_\.-]+)@([A-Za-z0-9_\.-]+)\.([A-Za-z\.]{2,6})$"
      ErrorMessage="Неверный формат E-mail" />
    <asp:RequiredFieldValidator ID="rOgrn" runat="server" Text="Укажите ОГРН!" ControlToValidate="tbOgrn" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
      ControlToValidate="tbWebsite" ValidationExpression="^(https?:\/\/)?(HTTPS?:\/\/)?([\dA-Za-z\.-]+)\.([A-Za-z\.]{2,6})([\/\w \.-]*)*\/?$"
      ErrorMessage="Неверный формат вебсайта" />
  </div>
</div>
