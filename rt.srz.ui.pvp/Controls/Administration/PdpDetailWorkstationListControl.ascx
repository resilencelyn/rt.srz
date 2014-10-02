<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PdpDetailWorkstationListControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Administration.PdpDetailWorkstationListControl" %>


<%@ Register Src="WorkstationDetailControl.ascx" TagName="WorkstationDetailControl" TagPrefix="uc" %>

<script type="text/javascript">

  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_endRequest(function (s, e) {
    EndRequest(s, e, "<%=scrollArea1.ClientID%>", "<%=hfScrollPosition1.ClientID%>");
  });

  window.onload = function () {
    OnLoad("<%=scrollArea1.ClientID%>", "<%=hfScrollPosition1.ClientID%>");
  }

  function SetDivPosition1() {
    SetDivPos("<%=scrollArea1.ClientID%>", "<%=hfScrollPosition1.ClientID%>");
  }

</script>

<div style="clear: both">
  <div class="admSection">
    <div class="admDivSection">

      <div class="partPadding">

        <div class="headerTitles">
          <asp:Label ID="lbDetailCaption" runat="server" Text="Пункт выдачи"></asp:Label>
        </div>

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="lbName" runat="server" Text="Краткое название"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbShortName" runat="server" MaxLength="250" Width="100%" CssClass="controlBoxes"></asp:TextBox>
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="Label1" runat="server" Text="Полное название"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbFullName" runat="server" MaxLength="250" Width="100%" CssClass="controlBoxes"></asp:TextBox>
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="Label3" runat="server" Text="Код"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbCode" runat="server" MaxLength="5" Width="100%" CssClass="controlBoxes"></asp:TextBox>
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="Label6" runat="server" Text="Фамилия"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbLastName" runat="server" MaxLength="50" Width="100%" CssClass="controlBoxes"></asp:TextBox>
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="Label7" runat="server" Text="Имя"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbFirstName" runat="server" MaxLength="50" Width="100%" onkeydown="firstNameChanged()" onkeypress="firstSymbolToUpper(this)" CssClass="controlBoxes"></asp:TextBox>
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

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="Label8" runat="server" Text="Отчество"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbMiddleName" runat="server" MaxLength="50" Width="100%" onkeypress="firstSymbolToUpper(this)" CssClass="controlBoxes"></asp:TextBox>
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

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="Label4" runat="server" Text="Телефон"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbPhone" runat="server" MaxLength="100" Width="100%" CssClass="controlBoxes"></asp:TextBox>
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="Label10" runat="server" Text="Факс"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbFax" runat="server" MaxLength="100" Width="100%" CssClass="controlBoxes"></asp:TextBox>
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="admLabelControl">
            <div class="admControlPadding">
              <asp:Label ID="Label11" runat="server" Text="Email"></asp:Label>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
              <asp:TextBox ID="tbEmail" runat="server" MaxLength="100" Width="100%" CssClass="controlBoxes"></asp:TextBox>
            </div>
          </div>
        </div>

        <div style="clear: both;">
          <div class="admErrorControl">
            <div class="admControlPadding">
              <div class="errorMessage">
                <asp:RequiredFieldValidator ID="rfShortName" runat="server" Text="Укажите краткое название!" ControlToValidate="tbShortName" Enabled="False" />
                <asp:RequiredFieldValidator ID="rfFullName" runat="server" Text="Укажите полное название!" ControlToValidate="tbFullName" Enabled="False" />
                <asp:RequiredFieldValidator ID="rfCode" runat="server" Text="Укажите код!" ControlToValidate="tbCode" />
                <asp:RegularExpressionValidator ID="vEmail" runat="server"
                  ControlToValidate="tbEmail" ValidationExpression="^([A-Za-z0-9_\.-]+)@([A-Za-z0-9_\.-]+)\.([A-Za-z\.]{2,6})$"
                  ErrorMessage="Неверный формат E-mail" Enabled="False" />
                <asp:CustomValidator ID="vCode" runat="server" ErrorMessage="Пункт выдачи с таким кодом уже существует у данной СМО!" OnServerValidate="vCode_ServerValidate" ControlToValidate="tbCode"></asp:CustomValidator>
              </div>
            </div>
          </div>
          <div class="admValueControl">
            <div class="admControlPadding">
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="admDivSection">
      <div class="partPadding">

        <div class="headerTitles">
          <asp:Label ID="lbListDetail" runat="server" Text="Рабочие станции"></asp:Label>
        </div>

        <asp:Menu ID="menu1" runat="server" OnMenuItemClick="menu_MenuItemClick" Orientation="Horizontal" CssClass="ItemMenu" OnPreRender="menu1_PreRender">
          <Items>
            <asp:MenuItem Text="Добавить" Value="Add" ImageUrl="~/Resources/create.png"></asp:MenuItem>
            <asp:MenuItem Text="Открыть" Value="Open" ImageUrl="~/Resources/open.png"></asp:MenuItem>
            <asp:MenuItem Text="Удалить" Value="Delete" ImageUrl="~/Resources/delete.png"></asp:MenuItem>
          </Items>
        </asp:Menu>

        <asp:HiddenField ID="hfScrollPosition1" runat="server" Value="0" />
        <div class="workstationGridPart" runat="server" id="scrollArea1" onscroll="SetDivPosition1()">
          <asp:GridView Style="width: 100%; table-layout: fixed; word-wrap: break-word;" ID="gridView" runat="server" EnableModelValidation="True"
            AllowSorting="True" AutoGenerateColumns="False" OnRowDeleting="gridView_Deleting" OnRowCommand="gridView_RowCommand"
            DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="Both" OnSelectedIndexChanged="gridView_SelectedIndexChanged">
            <HeaderStyle CssClass="GridHeader" />
            <RowStyle CssClass="GridRowStyle" />
            <SelectedRowStyle CssClass="GridSelectedRowStyle" />
            <Columns>
              <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton"></asp:ButtonField>
              <asp:CommandField SelectText="Выбор" ShowSelectButton="true" ItemStyle-CssClass="HideButton" HeaderStyle-CssClass="HideButton">
                <HeaderStyle CssClass="HideButton" />
                <ItemStyle CssClass="HideButton" />
              </asp:CommandField>
              <asp:BoundField DataField="Name" HeaderText="Имя компьютера" SortExpression="Name" />
              <asp:BoundField DataField="UecReaderName" HeaderText="Наименование ридера" SortExpression="UecReaderName" />
            </Columns>
          </asp:GridView>
        </div>
      </div>
    </div>
    <div class="admWorkstationSection">
      <div class="partPadding">
        <uc:WorkstationDetailControl ID="workstationDetailControl" runat="server" />
      </div>
    </div>

    <div style="clear: both">
      <div class="separator">
      </div>
    </div>

    <div style="clear: both">
      <asp:Button ID="btnSave" runat="server" Text="Сохранить" OnClick="btnSave_Click" CssClass="buttons" />
      <asp:Button ID="btnCancel" runat="server" Text="Отменить" OnClick="btnCancel_Click" CausesValidation="False" CssClass="buttons" />
    </div>

  </div>
</div>

<div style="clear: both">
</div>

