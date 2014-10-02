<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwinPersonControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.Twins.TwinPersonControl" %>

<div class="entireTwinControl">

  <div style="clear: both">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="lbTitle" runat="Server" Text="Человек {0} ID" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbId" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both">
    <div class="separator">
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="lblLastName" runat="Server" Text="Фамилия" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbLastName" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label2" runat="Server" Text="Имя" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbFirstName" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label3" runat="Server" Text="Отчество" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbMiddleName" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label5" runat="Server" Text="Дата рождения" />
      </div>
    </div>
    <div class="twinControl">
      <div class="valueControl1_4">
        <div class="twinControl">
          <asp:TextBox ID="tbBirthDate" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes"></asp:TextBox>
        </div>
      </div>
      <div class="labelControl1_4">
        <div class="twinControl">
          <asp:Label ID="Label4" runat="Server" Text="Пол" />
        </div>
      </div>
      <div class="valueControl1_4_last">
        <div class="twinControlNoRightPadding">
          <asp:TextBox ID="tbGender" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label8" runat="Server" Text="СНИЛС" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbSnils" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label9" runat="Server" Text="Документ удл" />
      </div>
    </div>
    <div class="twinControl">
      <div class="valueControl1_4">
        <div class="twinControl">
          <asp:TextBox ID="tbUdlSeries" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
      <div class="valueControl1_4">
        <div class="twinControl">
          <asp:TextBox ID="tbUdlAdditionalSeries" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
      <div class="valueControl1_4_last">
        <div class="twinControlNoRightPadding">
          <asp:TextBox ID="tbUdlNumber" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label14" runat="Server" Text="Гражданство" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbCitizenship" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
      </div>
    </div>
    <div class="checkControl">
      <div class="twinControl">
        <asp:CheckBox ID="cbWithoutCitizenship" runat="server" Text="Без гражданства" Enabled="False" />
      </div>
    </div>
    <div class="checkControl">
      <div class="twinControl">
        <asp:CheckBox ID="cbRefugee" runat="server" Text="Беженец" Enabled="False" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label7" runat="Server" Text="Место рождения" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbBirthPlace" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label6" runat="Server" Text="Категория" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbCategory" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label10" runat="Server" Text="Адрес регистрации" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbRegistrationAddress" runat="Server" Width="100%" ReadOnly="True" TextMode="MultiLine" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label11" runat="Server" Text="Адрес проживания" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbLiveAddress" runat="Server" Width="100%" ReadOnly="True" TextMode="MultiLine" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label13" runat="Server" Text="Документ подтверждающий регистрацию" />
      </div>
    </div>
    <div class="twinControl">
      <div class="valueControl1_4">
        <div class="twinControl">
          <asp:TextBox ID="tbRegSeries" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
      <div class="labelControl1_4">
        <div class="twinControl">
          <asp:TextBox ID="tbRegAdditionalSeries" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
      <div class="valueControl1_4_last">
        <div class="twinControlNoRightPadding">
          <asp:TextBox ID="tbRegNumber" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label1" runat="Server" Text="ЕНП" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbEnp" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label17" runat="Server" Text="ДПФС" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbMedicalInsurance" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label18" runat="Server" Text="с" />
      </div>
    </div>
    <div class="twinControl">
      <div class="valueControl1_4">
        <div class="twinControl">
          <asp:TextBox ID="tbMedicalInsuranceFrom" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
      <div class="labelControl1_4">
        <div class="twinControl">
          <asp:Label ID="Label19" runat="Server" Text="по" />
        </div>
      </div>
      <div class="valueControl1_4_last">
        <div class="twinControlNoRightPadding">
          <asp:TextBox ID="tbMedicalInsuranceTo" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label12" runat="Server" Text="Телефон домашний" />
      </div>
    </div>
    <div class="twinControl">
      <div class="valueControl1_4">
        <div class="twinControl">
          <asp:TextBox ID="tbHomePhone" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
      <div class="labelControl1_4">
        <div class="twinControl">
          <asp:Label ID="Label15" runat="Server" Text="Телефон рабочий" />
        </div>
      </div>
      <div class="valueControl1_4_last">
        <div class="twinControlNoRightPadding">
          <asp:TextBox ID="tbWorkPhone" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
        </div>
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label16" runat="Server" Text="Электронная почта" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbEmail" runat="Server" Width="100%" ReadOnly="True" CssClass="controlBoxes" />
      </div>
    </div>
  </div>

  <%--  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label17" runat="Server" Text="Временное свидетельство" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbTempCertificate" runat="Server" Width="100%" ReadOnly="True" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="labelControl">
      <div class="twinControl">
        <asp:Label ID="Label18" runat="Server" Text="Полис" />
      </div>
    </div>
    <div class="valueControl">
      <div class="twinControl">
        <asp:TextBox ID="tbPolis" runat="Server" Width="100%" ReadOnly="True" />
      </div>
    </div>
  </div>--%>

  <div runat="server" id="joinDiv" style="clear: both; padding-top: 2px">
    <div class="separator">
    </div>
    <asp:Button ID="btnJoin" runat="server" Text="Объединить" OnClick="btnJoin_Click" CssClass="buttons" />
  </div>

  <div runat="server" style="clear: both;" />
</div>
