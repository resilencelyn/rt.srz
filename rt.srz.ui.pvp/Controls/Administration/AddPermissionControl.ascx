<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPermissionControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.AddPermissionControl" %>


<script type="text/javascript">

  function CheckKeys(keypressed) {

    switch (keypressed) {
      case 8:
        return true;
      case 9:
        return true;
      case 13:
        return true;
      case 17:
        return true;
      case 18:
        return true;
      case 35:
        return true;
      case 36:
        return true;
      case 37:
        return true;
      case 39:
        return true;
      case 46:
        return true;

      case 96:
        return true;
      case 97:
        return true;
      case 98:
        return true;
      case 99:
        return true;
      case 100:
        return true;
      case 101:
        return true;
      case 102:
        return true;
      case 103:
        return true;
      case 104:
        return true;
      case 105:
        return true;
    }
  }

  //скрытиие валидатора
  function codeChanged(textBox) {

    //проверка чтобы код содержал только цифры и нельзя было ввести символы
    if (CheckKeys(event.keyCode))
      return true;

    if (!String.fromCharCode(event.keyCode).match(/^\d+$/)) {
      return false;
    }
    return true;
  }

</script>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление разрешения"></asp:Label>
</div>

<div style="clear: both;">
  <div class="admLabelControl1Fix">
    <div class="admControlPadding">
      <asp:Label ID="lbName" runat="server" Text="Название"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbName" runat="server" MaxLength="250" Width="100%" CssClass="controlBoxes"></asp:TextBox>
    </div>
  </div>
  <div class="errorMessage">
    <div class="admfloatLeft">
      <asp:RequiredFieldValidator ID="rfName" runat="server" Text="Укажите название!" ControlToValidate="tbName" />
    </div>
  </div>
</div>

<div style="clear: both;">
  <div class="admLabelControl1Fix">
    <div class="admControlPadding">
      <asp:Label ID="lbCode" runat="server" Text="Код"></asp:Label>
    </div>
  </div>
  <div class="admValueControl">
    <div class="admControlPadding">
      <asp:TextBox ID="tbCode" runat="server" MaxLength="50" Width="100%" onkeydown="return codeChanged(this)" CssClass="controlBoxes" />
    </div>
  </div>
  <div class="errorMessage">
    <div class="admfloatLeft">
      <asp:RequiredFieldValidator ID="rfCode" runat="server" Text="Укажите код!" ControlToValidate="tbCode" />
      <asp:CustomValidator ID="cvCode" runat="server" Text="Разрешение с таким кодом уже существует!" ControlToValidate="tbCode" OnServerValidate="ValidateCode" />
    </div>
  </div>
</div>
<div style="clear: both">
</div>


