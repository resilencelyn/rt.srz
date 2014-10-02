<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RangeNumberControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.NSI.RangeNumberControl" %>

<%@ Register TagPrefix="uc" TagName="PositionsControl" Src="~/Controls/NSI/PositionsControl.ascx" %>


<script type="text/javascript">

  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_endRequest(function (s, e) {
    DisableMenuItemDelete(true);
  });

  window.onload = function () {
    DisableMenuItemDelete(true);
  }

  function SetEnableDeleteButtonDependSelectedRows(obj) {

    var row = obj.parentNode.parentNode;
    var GridView = row.parentNode;

    //Get all input elements in Gridview
    var inputList = GridView.getElementsByTagName("input");

    var anyChecked = false;

    for (var i = 0; i < inputList.length; i++) {
      if (inputList[i].type == "checkbox" && inputList[i].checked) {
        anyChecked = true;
        break;
      }
    }
    DisableMenuItemDelete(!anyChecked);
  }

  function DisableMenuItemDelete(disable) {
    //дизаблим пункт меню удалить если нет выбранных записей и наооборот
    var menu = $get("<%=menu1.ClientID %>");
    var count = menu.all.length;

    for (var i = 0; i < count; i++) {
      if (!menu.all[i]) {
        continue;
      }
      if (menu.all[i].nodeName != "LI" || menu.all[i].innerText != "Удалить") {
        continue;
      }
      //удаляем спан тэги с серым цветом дизабла
      if (!disable) {
        menu.all[i].innerHTML = menu.all[i].innerHTML.replace(/<\/?span[^>]*>/g, "");;
      }
        //добавляем спан тэги с серым цветом дизабла
      else {
        menu.all[i].innerHTML = menu.all[i].innerHTML.replace('Удалить', "<span style='color:LightGray'>Удалить</span>")
      }
      menu.all[i].disabled = disable;
    }
  }

  //плагие по форматированному вводу почему-то внутри грида не работает
  function onlyNumbers(event) {
    var e = event || evt;
    var charCode = e.which || e.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
      return false;
    return true;
  }
</script>

<div class="formTitle">
  <asp:Label ID="lbTitle" runat="server" Text="Добавление диапазона номеров вс"></asp:Label>
</div>

<asp:Panel ID="panel" runat="server" Enabled="false">

  <div style="clear: both;">
    <div class="admLabelControl2Fix">
      <div class="admControlPadding">
        <asp:Label ID="lbName" runat="server" Text="СМО"></asp:Label>
      </div>
    </div>
    <div class="admValueControl">
      <div class="admControlPadding">
        <asp:DropDownList ID="dlSmo" runat="server" DataTextField="ShortName" DataValueField="Id" CssClass="dropDowns" />
      </div>
    </div>
    <div class="errorMessage">
      <div class="admfloatLeft">
        <asp:RequiredFieldValidator ID="rfSmo" runat="server" Text="Укажите СМО!" ControlToValidate="dlSmo" />
      </div>
    </div>
  </div>
  <div style="clear: both;">
    <div class="admLabelControl2Fix">
      <div class="admControlPadding">
        <asp:Label ID="lbType" runat="server" Text="Диапазон с"></asp:Label>
      </div>
    </div>
    <div class="admValueControl">
      <div class="admControlPadding">
        <asp:TextBox ID="tbFrom" runat="server" Width="100%" CssClass="rangeNum" MaxLength="9"></asp:TextBox>
      </div>
    </div>
    <div class="errorMessage">
      <div class="admfloatLeft">
        <asp:RequiredFieldValidator ID="rfFrom" runat="server" Text="Укажите начало диапазона!" ControlToValidate="tbFrom" />
      </div>
    </div>
  </div>
  <div style="clear: both;">
    <div class="admLabelControl2Fix">
      <div class="admControlPadding">
        <asp:Label ID="lbGender" runat="server" Text="Диапазон по"></asp:Label>
      </div>
    </div>
    <div class="admValueControl">
      <div class="admControlPadding">
        <asp:TextBox ID="tbTo" runat="server" Width="100%" CssClass="rangeNum" MaxLength="9"></asp:TextBox>
      </div>
    </div>
    <div class="errorMessage">
      <div class="admfloatLeft">
        <asp:RequiredFieldValidator ID="rfTo" runat="server" Text="Укажите конец диапазона!" ControlToValidate="tbTo" />
      </div>
    </div>
  </div>

</asp:Panel>


<div style="clear: both">
  <div class="headerTitles">
    <asp:Label ID="Label1" runat="server" Text="Настройка печати временных свидетельств выделенного диапазона"></asp:Label>
  </div>

  <asp:UpdatePanel ID="MenuUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="contentUpdatePanel" runat="server">
    <ContentTemplate>

      <div class="paddingGridMenu">
        <asp:Menu ID="menu1" runat="server" OnMenuItemClick="menu_MenuItemClick" Orientation="Horizontal" CssClass="ItemMenu" OnPreRender="menu1_PreRender">
          <Items>
            <asp:MenuItem Text="Добавить" Value="Add" ImageUrl="~/Resources/create.png"></asp:MenuItem>
            <asp:MenuItem Text="Удалить" Value="Delete" ImageUrl="~/Resources/delete.png"></asp:MenuItem>
          </Items>
        </asp:Menu>
      </div>

      <asp:GridView Style="width: 100%" ID="grid" runat="server" EnableModelValidation="True"
        AllowSorting="True" AutoGenerateColumns="False"
        DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both">
        <HeaderStyle CssClass="GridHeader" />
        <RowStyle CssClass="GridRowStyle" />
        <SelectedRowStyle CssClass="GridSelectedRowStyle" />
        <FooterStyle CssClass="GridFooterStyle" />
        <Columns>
          <asp:TemplateField HeaderText="Выбор">
            <HeaderStyle CssClass="CheckHeader" />
            <ItemStyle CssClass="CheckItem" />
            <ItemTemplate>
              <asp:CheckBox ID="cbCheck" runat="server" onclick="javascript: SetEnableDeleteButtonDependSelectedRows(this);"></asp:CheckBox>
            </ItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="Номер ВС с">
            <ItemTemplate>
              <asp:TextBox ID="tbRangelFrom" runat="server" CssClass="rangeNumInGrid" Width="100%"></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Номер ВС по">
            <ItemTemplate>
              <asp:TextBox ID="tbRangelTo" runat="server" CssClass="rangeNumInGrid" Width="100%"></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Наименоваие шаблона печати">
            <ItemTemplate>
              <asp:DropDownList ID="ddlTemplate" runat="server" DataValueField="Id" DataTextField="Name" Width="100%" CssClass="dropDowns"></asp:DropDownList>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </ContentTemplate>
  </asp:UpdatePanel>

</div>

<div class="errorMessage">
  <div style="clear: both;">
    <div class="admControlPadding">
      <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Запись по диапазону пересекается с другими записями!" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
    </div>
  </div>

  <div style="clear: both;">
    <div class="admControlPadding">
      <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Начальное значение диапазона больше конечного!" OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
    </div>
  </div>

  <div style="clear: both;">
    <div class="admfloatLeft">
      <%--Проверка чтобы записи в гриде имели выбранный шаблон--%>
      <asp:CustomValidator ID="cvGridEmptyTemplate" runat="server" ErrorMessage="Не указан шаблон для печати временных свидетельств!" OnServerValidate="cvGridEmptyTemplate_ServerValidate"></asp:CustomValidator>
    </div>
  </div>

  <div style="clear: both;">
    <div class="admfloatLeft">
      <%--Проверка чтобы записи в гриде имели номера вс начальные меньше либо равные конечным--%>
      <asp:CustomValidator ID="cvGridStartLargerEnd" runat="server" ErrorMessage="Начальное значение шаблона больше конечного!" OnServerValidate="cvGridStartLargerEnd_ServerValidate"></asp:CustomValidator>
    </div>
  </div>

  <div style="clear: both;">
    <div class="admfloatLeft">
      <%--Проверка чтобы записи в гриде попадали в дипазон шапки--%>
      <asp:CustomValidator ID="cvGridOutOfRange" runat="server" ErrorMessage="Номера ВС не должны выходить за пределы текущего диапазона!" OnServerValidate="cvGridOutOfRange_ServerValidate"></asp:CustomValidator>
    </div>
  </div>

  <div style="clear: both;">
    <div class="admfloatLeft">
      <%--Проверка чтобы записи в гриде не пересекались между собой по интервалам--%>
      <asp:CustomValidator ID="cvGridIntersect" runat="server" ErrorMessage="Интервалы номеров ВС не должны пересекаться при выборе шаблона печати!" OnServerValidate="cvGridIntersect_ServerValidate"></asp:CustomValidator>
    </div>
  </div>

</div>

<div style="clear: both">
</div>

