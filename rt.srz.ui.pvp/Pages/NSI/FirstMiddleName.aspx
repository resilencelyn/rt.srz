<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrations/BaseAddCancelPage.master" AutoEventWireup="true" CodeBehind="FirstMiddleName.aspx.cs" Inherits="rt.srz.ui.pvp.Pages.NSI.FirstMiddleName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentContent" runat="server">

  <div class="formTitle">
    <asp:Label ID="lbTitle" runat="server" Text="Добавление имени/отчества"></asp:Label>
  </div>

  <div style="clear: both;">
    <div class="admLabelControl1Fix">
      <div class="admControlPadding">
        <asp:Label ID="lbName" runat="server" Text="Название"></asp:Label>
      </div>
    </div>
    <div class="admValueControl">
      <div class="admControlPadding">
        <asp:TextBox ID="tbName" runat="server" MaxLength="150" Width="100%" CssClass="controlBoxes"></asp:TextBox>
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
        <asp:Label ID="lbType" runat="server" Text="Тип"></asp:Label>
      </div>
    </div>
    <div class="admValueControl">
      <div class="admControlPadding">
        <asp:DropDownList ID="cbType" runat="server" Width="100%" DataTextField="Name" DataValueField="Id" CssClass="dropDowns"></asp:DropDownList>
      </div>
    </div>
    <div class="errorMessage">
      <div class="admfloatLeft">
        <asp:RequiredFieldValidator ID="rfType" runat="server" Text="Укажите тип!" ControlToValidate="cbType" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="admLabelControl1Fix">
      <div class="admControlPadding">
        <asp:Label ID="lbGender" runat="server" Text="Пол"></asp:Label>
      </div>
    </div>
    <div class="admValueControl">
      <div class="admControlPadding">
        <asp:DropDownList ID="cbGender" runat="server" Width="100%" DataTextField="Name" DataValueField="Id" CssClass="dropDowns"></asp:DropDownList>
      </div>
    </div>
    <div class="errorMessage">
      <div class="admfloatLeft">
        <asp:RequiredFieldValidator ID="rfGender" runat="server" Text="Укажите пол!" ControlToValidate="cbGender" />
      </div>
    </div>
  </div>

  <div style="clear: both;">
    <div class="admLabelControl1Fix">
      <div class="admControlPadding">
      </div>
    </div>
    <div class="errorMessage">
      <div class="admControlPadding">
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Запись с таким именем, типом, полом уже существует!" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
      </div>
    </div>
    <div class="errorMessage">
      <div class="admfloatLeft">
      </div>
    </div>
  </div>

  <div style="clear: both">
  </div>

</asp:Content>
