<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentUDLUserControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.DocumentUdlUserControl" %>

<%@ Register Assembly="rt.srz.ui.pvp" Namespace="rt.srz.ui.pvp.Controls" TagPrefix="uc" %>

<script type="text/javascript">
  // Create the event handlers for PageRequestManager
  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_endRequest(pageRequestManager_endRequest);

  //Добавление масок к полям ввода серии и номера документа УДЛ в момент инициализации страницы
  $(document).ready(function () {
    addMasks_<%=UniqueKey %>();
  });

  //Добавление масок к полям ввода серии и номера документа УДЛ после вызова AJAX
  function pageRequestManager_endRequest(sender, args) {
    addMasks_<%=UniqueKey %>();
  }

  //Добавление масок к полям ввода серии и номера документа УДЛ
  function addMasks_<%=UniqueKey %>() {
      jQuery.mask.rules = {
          '9': /[0-9]/,                       //от нуля до 9
          '*': /[0-9A-Za-zА-ЯЁа-яё\x20\x2D]/, //цифры, буквы, пробел и тире
          'A': /[IVLXC]/,                     //все римские цифры
          'Я': /[А-Яа-я]/                        //все кирилические символы
      };

      var currentDocId = window.$get('<%=ddlDocumentType.ClientID%>').value;
        switch (currentDocId) {
          case "-1": //Значение по умолчанию
            {
              jQuery.mask.options.fixedChars = '[(),.:/ -]';
              window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "none";
              window.$get('<%=tbDateExp.ClientID%>').style.display = "none";
              window.$get('<%=tbDateExp.ClientID%>').value = "";
              window.$get('<%=lbDateExp.ClientID%>').style.display = "none";
            }
            break;

            case "3":  // 3 Свидетельство о рождении Российский Федерации
            {
                jQuery.mask.options.fixedChars = '[(),.:/ -]';
                $('#<%=tbSeries.ClientID%>').setMask('AAAAAAA');
                $('#<%=tbNumber.ClientID%>').setMask('999999');
                $('#<%=tbAdditionalSeries.ClientID%>').setMask('ЯЯ');
                window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "";
                window.$get('<%=tbDateExp.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').value = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "none";
            }
            break;

            case "9":  // 9 Иностранный паспорт
            {
                jQuery.mask.options.fixedChars = '[(),.:/]';
                $('#<%=tbSeries.ClientID%>').setMask('********');
                $('#<%=tbNumber.ClientID%>').setMask('9999999999');
                window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').value = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "none";
            }
            break;

            case "10": // 10 Свидетельство о регистрации ходатайства о признании иммигранта беженцем
            case "11": // 11 Вид на жительство
            case "12": // 12 Удостоверение беженца в Российской Федерации
            case "13": // 13 Временное удостоверение личности гражданина Российской Федерации
            {
                jQuery.mask.options.fixedChars = '[(),.:/]';
                $('#<%=tbSeries.ClientID%>').setMask('********');
                $('#<%=tbNumber.ClientID%>').setMask('99999999');
                window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').style.display = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "";
            }
            break;

            case "14": //14 Паспорт гражданина Российской Федерации
            {
                jQuery.mask.options.fixedChars = '[(),.:/ -]';
                $('#<%=tbSeries.ClientID%>').setMask('99 99');
                $('#<%=tbNumber.ClientID%>').setMask('999999');
                $('#<%=tbAdditionalSeries.ClientID%>').setMask('ЯЯ');
                window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').value = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "none";
            }   
            break;
        
            case "391": // 20 Копия жалобы на решение о лишении статуса беженца в Федеральную миграционную службу с отметкой о её приёме к рассмотрению
            case "392": // 21 Документ иностранного гражданина
            case "393": // 22 Документ лица без гражданства
            case "629": // 24 Свидетельство о рождении, выданное не в Российской Федерации
            {
                jQuery.mask.options.fixedChars = '[(),.:/]';
                $('#<%=tbSeries.ClientID%>').setMask('********');
                $('#<%=tbNumber.ClientID%>').setMask('9999999999');
                window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').value = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "none";
            }
            break;
        
            case "-393": // 22 Документ лица без гражданства
            {
                jQuery.mask.options.fixedChars = '[(),.:/]';
                $('#<%=tbSeries.ClientID%>').setMask('********');
                $('#<%=tbNumber.ClientID%>').setMask('9999999999');
                window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').style.display = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "";
            }
            break;

            case "394": // 23 Разрешение на временное проживание 
            {
                jQuery.mask.options.fixedChars = '[(),.:/]';
                $('#<%=tbSeries.ClientID%>').setMask('********');
                $('#<%=tbNumber.ClientID%>').setMask('9999999999');
                window.$get('<% =tbAdditionalSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').style.display = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "";
            }
            break;

            case "630": // 25 Свидетельство о предоставлении временного убежища на территории Российской Федерации
            {
                jQuery.mask.options.fixedChars = '[(),.:/ -]';
                $('#<%=tbSeries.ClientID%>').setMask('ЯЯ');
                $('#<%=tbNumber.ClientID%>').setMask('9999999');
                window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').style.display = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "";
            }
            break;

            case "429": // 30 Свидетельство о регистрации по месту жительства
            {
                jQuery.mask.options.fixedChars = '[(),.:/ -]';
                window.$get('<%=lbSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbSeries.ClientID%>').style.display = "none";
                window.$get('<%=tbAdditionalSeries.ClientID%>').style.display = "none";
                $('#<%=tbNumber.ClientID%>').setMask('999999999999');
                window.$get('<%=tbDateExp.ClientID%>').style.display = "none";
                window.$get('<%=tbDateExp.ClientID%>').value = "";
                window.$get('<%=lbDateExp.ClientID%>').style.display = "none";
            }
            break;
        }
    }

//Траслирует клавиши из русской раскладки в латинскую
function translateKey_<%=UniqueKey %>(e) {
    //Возврат, если документ иной от "Свидетельство о рождении Российский Федерации" 
    var currentDocId = window.$get('<%=ddlDocumentType.ClientID%>').value;
     if (currentDocId != "3") //Свидетельство о рождении Российский Федерации
       return;
     var evt = e || window.event;
     if (evt) {
       var keyCode = evt.charCode || evt.keyCode;
       var textBox = document.getElementById('<%=tbSeries.ClientID%>');
    switch (keyCode) {
      case 73: //I
        textBox.value += 'I';
        evt.returnValue = false;
        break;
      case 86: //V
        textBox.value += 'V';
        evt.returnValue = false;
        break;
      case 76: //L
        textBox.value += 'L';
        evt.returnValue = false;
        break;
      case 88: //X
        textBox.value += 'X';
        evt.returnValue = false;
        break;
      case 67: //C
        textBox.value += 'C';
        evt.returnValue = false;
        break;
    }
  }
}

function toUpper_<%=UniqueKey %>(e) {
    //Возврат, если документ иной от "Свидетельство о рождении Российский Федерации" 
    var currentDocId = window.$get('<%=ddlDocumentType.ClientID%>').value;
  if (currentDocId != "3") //Свидетельство о рождении Российский Федерации
    return;

  if (event.which == null) {  // IE
    if (event.keyCode < 32)
      return null; // спец. символ
    var symbol = String.fromCharCode(event.keyCode);
    symbol = symbol.toUpperCase(symbol);
    var textBox = document.getElementById('<%=tbAdditionalSeries.ClientID%>').value += symbol;
    event.returnValue = false;
  }
}


    function documentTypeChanged_<%=UniqueKey %>() {
        documentChanged_<%=UniqueKey %>();
        addMasks_<%=UniqueKey %>();

        //Запись значения в скрытое поле
        //Сохранение значения в скрытом поле
        window.$get('<%=hfSelectedDocType.ClientID %>').value = window.$get('<%=ddlDocumentType.ClientID %>').value;
    }

     function documentChanged_<%=UniqueKey %>() {
    var validator = window.$get('<%=cvDocument.ClientID%>');
            if (validator != null)
              validator.style.visibility = 'hidden';
          }

</script>

<%--стиль только для того чтобы переопределить text-align: center из стиля wizardDiv которым обрамляется шаг визарда--%>
<div style="text-align: left;">

  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>


  <asp:HiddenField ID="hfSelectedDocType" runat="server" />
  <asp:UpdatePanel ID="DocTypeUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div style="clear: both">
        <div class="labelControl">
          <div class="labelPadding">
            <asp:Label ID="lbDocumentType" runat="Server" Text="Вид документа*" />
          </div>
        </div>
        <div class="valueControl">
          <div class="controlPadding">
            <asp:DropDownList ID="ddlDocumentType" runat="Server" Width="100%" CssClass="dropDowns">
              <asp:ListItem Text="Выберите вид документа" Value="-1" />
            </asp:DropDownList>
          </div>
        </div>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

  <div style="clear: both">
    <div class="labelControl">
      <div class="labelPadding">
        <asp:Label ID="lbSeries" runat="Server" Text="Серия документа*" />
      </div>
    </div>
    <div class="valueControl">
      <div class="controlPadding">
        <asp:TextBox ID="tbSeries" runat="Server" Width="130px" MaxLength="20" CssClass="maskEditSeries" onkeyup="this.value=this.value.toUpperCase()" />
        <asp:TextBox ID="tbAdditionalSeries" runat="Server" Width="50px" MaxLength="20" CssClass="maskEditAdditionalSeries" onkeyup="this.value=this.value.toUpperCase()"/>
      </div>
    </div>
  </div>
  <div style="clear: both">
    <div class="labelControl">
      <div class="labelPadding">
        <asp:Label ID="lbNumber" runat="Server" Text="Номер документа*" />
      </div>
    </div>
    <div class="valueControl">
      <div class="controlPadding">
                <asp:TextBox ID="tbNumber" runat="Server" Width="130px" MaxLength="20" CssClass='maskEditNumber' />
        <asp:CustomValidator ID="cvDocument" runat="server" EnableClientScript="false" Text="T" CssClass="errorMessage" />
      </div>
    </div>
  </div>

  <div id="additionalPartDiv" runat="server">

    <div style="clear: both">
      <div class="labelControl">
        <div class="labelPadding">
          <asp:Label ID="lbIssuingAuthority" runat="Server" Text="Выдавший орган*" />
        </div>
      </div>
      <div class="valueControl">
        <div class="controlPadding">
          <asp:TextBox ID="tbIssuingAuthority" runat="Server" Width="100%" MaxLength="500" CssClass="controlBoxes" />
        </div>
      </div>
    </div>

    <div style="clear: both">
      <div class="labelControl">
        <div class="labelPadding">
          <asp:Label ID="lbIssueDate" runat="Server" Text="Дата выдачи*" />
        </div>
      </div>
      <div class="valueControl">
        <div class="controlPadding">
          <uc:DateBox runat="server" ID="tbIssueDate" Width="130px" CssClass="controlBoxes" />
        </div>
      </div>
    </div>

    <div style="clear: both">
      <div class="labelControl">
        <div class="labelPadding">
          <asp:Label ID="lbDateExp" runat="Server" Text="Действует до*" />
        </div>
      </div>
      <div class="valueControl">
        <div class="controlPadding">
          <uc:DateBox runat="server" ID="tbDateExp" Width="130px" CssClass="controlBoxes" />
        </div>
      </div>
    </div>

    <div style="clear: both">
      <div class="labelControl">
        <div class="labelPadding">
        </div>
      </div>
      <div class="valueControl">
        <div class="controlPadding">
          <asp:CheckBox ID="chbValidDocument" runat="server" Text="Документ правильный" Checked="false" Visible="False" />
        </div>
      </div>
    </div>

  </div>


    </ContentTemplate>
  </asp:UpdatePanel>

</div>
