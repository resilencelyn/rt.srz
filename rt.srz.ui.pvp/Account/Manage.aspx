<%@ Page Language="C#" Title="Редактирование профиля" MasterPageFile="~/Templates/AuthentificatedPage.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="rt.srz.ui.pvp.Account.Account_Manage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
  <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptLocalization="True" EnableScriptGlobalization="True" EnablePageMethods="true" CombineScripts="false" ScriptMode="Release" />

  <div style="padding: 25px">

    <section id="passwordForm">
      <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
        <p class="message-success"><%: SuccessMessage %></p>
      </asp:PlaceHolder>

      <div class="dialogContentPadding">


        <asp:PlaceHolder runat="server" ID="changePassword" Visible="True">

          <div class="loginHeader">
            <asp:Label ID="Label1" runat="server" Text="Изменение пароля"></asp:Label>
          </div>


          <asp:ChangePassword ID="ChangePassword1" runat="server" CancelDestinationPageUrl="~/" ViewStateMode="Disabled" RenderOuterTable="false" SuccessPageUrl="Manage.aspx?m=ChangePwdSuccess" OnChangePasswordError="OnChangePasswordError">
            <ChangePasswordTemplate>
              <p class="error">
                <asp:Literal runat="server" ID="FailureText" />
              </p>


              <div style="clear: both;">
                <div style="float: left; width: 25%">
                  <div style="padding: 5px;">
                    <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword">Текущий пароль</asp:Label>
                  </div>
                </div>
                <div style="float: left; width: 35%">
                  <div style="padding: 5px;">
                    <asp:TextBox runat="server" ID="CurrentPassword" CssClass="controlBoxes" TextMode="Password" Width="100%" />
                  </div>
                </div>
                <div style="float: left; width: 40%">
                  <div style="padding: 5px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CurrentPassword" CssClass="error" ErrorMessage="Поле текущий пароль - обязательное." />
                  </div>
                </div>
              </div>

              <div style="clear: both;">
                <div style="float: left; width: 25%">
                  <div style="padding: 5px;">
                    <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword">Новый пароль</asp:Label>
                  </div>
                </div>
                <div style="float: left; width: 35%">
                  <div style="padding: 5px;">
                    <asp:TextBox runat="server" ID="NewPassword" CssClass="controlBoxes" TextMode="Password" Width="100%" />
                  </div>
                </div>
                <div style="float: left; width: 40%">
                  <div style="padding: 5px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewPassword" CssClass="error" ErrorMessage="Поле новый пароль - обязательное." />
                  </div>
                </div>
              </div>

              <div style="clear: both;">
                <div style="float: left; width: 25%">
                  <div style="padding: 5px;">
                    <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword">Подтвердите новый пароль</asp:Label>
                  </div>
                </div>
                <div style="float: left; width: 35%">
                  <div style="padding: 5px;">
                    <asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="controlBoxes" TextMode="Password" Width="100%" />
                  </div>
                </div>
                <div style="float: left; width: 40%">
                  <div style="padding: 5px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmNewPassword" CssClass="error" Display="Dynamic" ErrorMessage="Подтвердите новый пароль." />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" CssClass="error" Display="Dynamic" ErrorMessage="Новый пароль и подтверждение не совпадают." />
                  </div>
                </div>
              </div>

              <div style="clear: both">
                <div class="separator">
                </div>
              </div>
              <asp:Button runat="server" CommandName="ChangePassword" Text="Изменить пароль" CssClass="buttons" />

            </ChangePasswordTemplate>
          </asp:ChangePassword>
        </asp:PlaceHolder>
      </div>
    </section>

  </div>
</asp:Content>
