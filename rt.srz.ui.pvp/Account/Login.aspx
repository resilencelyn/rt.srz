<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Templates/AuthentificationPage.master" AutoEventWireup="true"
  CodeBehind="Login.aspx.cs" Inherits="rt.srz.ui.pvp.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

  <div class="loginContent">

    <div class="dialogContentPadding">


      <div class="loginHeader">
        <asp:Label ID="Label1" runat="server" Text="Пожалуйста, введите ваше имя и пароль:"></asp:Label>
      </div>


      <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" OnLoggedIn="LoginUser_OnLoggedIn" OnLoginError="LoginUser_OnLoginError">
        <LayoutTemplate>


          <div style="clear: both;">
            <div style="float: left; width: 25%">
              <div style="padding: 5px;">
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Имя пользователя:</asp:Label>
              </div>
            </div>
            <div style="float: left; width: 75%">
              <div style="padding: 5px;">
                <asp:TextBox ID="UserName" runat="server" CssClass="controlBoxes" Width="100%"></asp:TextBox>
              </div>
            </div>
          </div>

          <div style="clear: both">
            <div style="float: left; width: 25%">
              <div style="padding: 5px;">
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Пароль:</asp:Label>
              </div>
            </div>
            <div style="float: left; width: 75%;">
              <div style="padding: 5px;">
                <asp:TextBox ID="Password" runat="server" CssClass="controlBoxes" TextMode="Password" Width="100%"></asp:TextBox>
              </div>
            </div>
          </div>

          <div style="clear: both">
            <div class="separator">
            </div>
          </div>

          <div style="clear: both">
            <div style="float: left;">
              <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Войти" ValidationGroup="LoginUserValidationGroup" CssClass="buttons" />
            </div>
          </div>

          <div style="clear: both">
            <div style="float: left; width: 100%">
              <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                CssClass="error" ErrorMessage="Введите имя пользователя." ToolTip="User Name is required."
                ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div style="clear: both">
            <div style="float: left; width: 100%">
              <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                CssClass="error" ErrorMessage="Введите пароль." ToolTip="Password is required."
                ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
            </div>
          </div>

          <div style="clear: both">
          </div>

        </LayoutTemplate>
      </asp:Login>

      <asp:Label ID="lblFailureText" runat="server" Visible="False" Text="Неверный пароль или имя пользователя!" CssClass="error"></asp:Label>
    </div>
  </div>
</asp:Content>
