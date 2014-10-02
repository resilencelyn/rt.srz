<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatementSelectionWizardControl.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.StatementSelectionWizardControl" %>
<%@ Register TagPrefix="uc" TagName="Step1" Src="StatementSelectionWizardSteps/Step1.ascx" %>
<%@ Register TagPrefix="uc" TagName="Step2" Src="StatementSelectionWizardSteps/Step2.ascx" %>
<%@ Register TagPrefix="uc" TagName="Step3" Src="StatementSelectionWizardSteps/Step3.ascx" %>
<%@ Register TagPrefix="uc" TagName="Step4" Src="StatementSelectionWizardSteps/Step4.ascx" %>
<%@ Register TagPrefix="uc" TagName="Step5" Src="StatementSelectionWizardSteps/Step5.ascx" %>
<%@ Register TagPrefix="uc" TagName="Step6" Src="StatementSelectionWizardSteps/Step6.ascx" %>
<%@ Register Src="~/Controls/Common/ConfirmControl.ascx" TagName="ConfirmControl" TagPrefix="uc" %>

<script type="text/javascript">
  function SideBarClick(li) {
    var textBox = document.getElementById('<%= hiddenTb.ClientID %>');
      var button = document.getElementById('<%= hiddenBtn.ClientID %>');
      textBox.value = li.title;
      button.click();
    }

    function disableButtonsOnFinishNavigationTemplate() {
      $get('<%=Wizard1.FindControl("FinishNavigationTemplateContainerID").FindControl("btnCancel").ClientID%>').style.display = 'none';
    $get('<%=Wizard1.FindControl("FinishNavigationTemplateContainerID").FindControl("btnSaveStatement").ClientID%>').style.display = 'none';
    $get('<%=Wizard1.FindControl("FinishNavigationTemplateContainerID").FindControl("btnPrevious").ClientID%>').style.display = 'none';
    $get('<%=Wizard1.FindControl("FinishNavigationTemplateContainerID").FindControl("btnFinish").ClientID%>').style.display = 'none';
    var button = $get('<%=Wizard1.FindControl("FinishNavigationTemplateContainerID").FindControl("btnPrintTemporaryCertificate").ClientID%>');
    if (button != null) {
      button.style.display = 'none';
    }
    return true;
    }


  function Disable5StepButtonsDuringLoadingFile(value) {
    $get('<%=Wizard1.FindControl("StepNavigationTemplateContainerID").FindControl("btnCancel").ClientID%>').disabled = value;
    $get('<%=Wizard1.FindControl("StepNavigationTemplateContainerID").FindControl("btnNext").ClientID%>').disabled = value;
    $get('<%=Wizard1.FindControl("StepNavigationTemplateContainerID").FindControl("btnPrevious").ClientID%>').disabled = value;
  }

  document.onkeyup = KeyCheck;

  function KeyCheck(e) {
    if (e.ctrlKey) {
      switch (e.keyCode) {
        case 37:
          __doPostBack('__Page', 'back');
          break;
        case 13:
        case 39:

          __doPostBack('__Page', 'forward');
          break;
      }
    }
  }
</script>

<div style="display: none;">
  <asp:TextBox ID="hiddenTb" runat="server"></asp:TextBox><asp:Button ID="hiddenBtn" runat="server" OnClick="HiddenBtnClick" />
</div>
<asp:HiddenField ID="IsAvailableStep2" runat="server" Value="false" />
<asp:HiddenField ID="IsAvailableStep3" runat="server" Value="false" />
<asp:HiddenField ID="IsAvailableStep4" runat="server" Value="false" />
<asp:HiddenField ID="IsAvailableStep5" runat="server" Value="false" />
<asp:HiddenField ID="IsAvailableStep6" runat="server" Value="false" />

<asp:Label ID="cvErrors" runat="server" CssClass="errorMessage" />

<uc:ConfirmControl ID="confirm" runat="server" Message="Вы уверены, что хотите закрыть заявление?" />
<uc:ConfirmControl ID="confirmPrint" runat="server" Message="Заявление будет сохранено. Продолжить?" />
<uc:ConfirmControl ID="confirmPrintVs" runat="server" Message="Заявление будет сохранено. Продолжить?" />
<uc:ConfirmControl ID="сonfirmPrintVsWithoutTemplate" runat="server" Message="Не задан шаблон печати временного свидетельства. Продолжить печать в любом случае?" />
<uc:ConfirmControl ID="confirmSave" runat="server" Message="Заявление будет переведено в статус 'Новое' и пойдет в обработку" />

<asp:Wizard ID="Wizard1" runat="server"
  HeaderText="Заявление на выбор (замену) СМО"
  OnFinishButtonClick="Wizard1PrintSatatementClick" ActiveStepIndex="0"
  OnActiveStepChanged="Wizard1ActiveStepChanged" DisplaySideBar="False"
  OnNextButtonClick="Wizard1NextButtonClick"
  OnPreviousButtonClick="Wizard1PreviousButtonClick">
  <StepStyle BorderStyle="None" Font-Bold="false" />
  <WizardSteps>
    <asp:WizardStep ID="WizardStep1" runat="server" Title="Шаг 1">
      <uc:Step1 runat="server" ID="step1"></uc:Step1>
    </asp:WizardStep>
    <asp:WizardStep ID="WizardStep2" runat="server" Title="Шаг 2">
      <uc:Step2 runat="server" ID="step2"></uc:Step2>
    </asp:WizardStep>
    <asp:WizardStep ID="WizardStep3" runat="server" Title="Шаг 3">
      <uc:Step3 runat="server" ID="step3"></uc:Step3>
    </asp:WizardStep>
    <asp:WizardStep ID="WizardStep4" runat="server" Title="Шаг 4">
      <uc:Step4 runat="server" ID="step4"></uc:Step4>
    </asp:WizardStep>
    <asp:WizardStep ID="WizardStep5" runat="server" Title="Шаг 5">
      <uc:Step5 runat="server" ID="step5"></uc:Step5>
    </asp:WizardStep>
    <asp:WizardStep ID="WizardStep6" runat="server" Title="Шаг 6">
      <uc:Step6 runat="server" ID="step6"></uc:Step6>
    </asp:WizardStep>
  </WizardSteps>

  <StartNavigationTemplate>
    <table cellpadding="3" cellspacing="3">
      <tr>
        <td>
          <asp:Button ID="btnNext" runat="server" Text="Далее >" CssClass="buttons"
            CausesValidation="true" UseSubmitBehavior="True"
            CommandName="MoveNext" />
        </td>
        <td>
          <asp:Button ID="btnCancel" runat="server" Text="Закрыть" CssClass="buttons"
            CausesValidation="false"
            OnClick="Wizard1CancelButtonClick" />
        </td>
      </tr>
    </table>
  </StartNavigationTemplate>

  <StepNavigationTemplate>
    <table cellpadding="3" cellspacing="3">
      <tr>
        <td>
          <asp:Button ID="btnPrevious" runat="server" Text="< Назад" CssClass="buttons"
            CausesValidation="false" UseSubmitBehavior="False"
            CommandName="MovePrevious" />
          <asp:Button ID="btnNext" runat="server" Text="Далее >" CssClass="buttons"
            CausesValidation="true" UseSubmitBehavior="True"
            CommandName="MoveNext" />
        </td>
        <td>
          <asp:Button ID="btnCancel" runat="server" Text="Закрыть" CssClass="buttons"
            CausesValidation="false"
            OnClick="Wizard1CancelButtonClick" />
        </td>
      </tr>
    </table>
  </StepNavigationTemplate>

  <FinishNavigationTemplate>
    <table cellpadding="3" cellspacing="3">
      <tr>
        <td>
          <asp:Button ID="btnSaveStatement" runat="server" Text="Сохранить заявление" UseSubmitBehavior="True" CssClass="buttons"
            OnClientClick=" return disableButtonsOnFinishNavigationTemplate(); " OnClick="Wizard1SaveStatement" />
          <asp:Button ID="btnFinish" runat="server" Text="Печать" CssClass="buttons"
            OnClientClick=" return disableButtonsOnFinishNavigationTemplate();" CausesValidation="true" CommandName="MoveComplete" />

          <asp:Button ID="btnPrintTemporaryCertificate" runat="server" Text="Печать ВС" Enabled="true" CssClass="buttons"
            OnClientClick=" return disableButtonsOnFinishNavigationTemplate();" CausesValidation="true" OnClick="Wizard1PrintTemporaryCertificateClick" />

        </td>
        <td>
          <asp:Button ID="btnPrevious" runat="server" Text="< Назад" CssClass="buttons"
            CausesValidation="false" CommandName="MovePrevious" />
          <asp:Button ID="btnCancel" runat="server" Text="Закрыть" CausesValidation="false" CssClass="buttons"
             OnClick="Wizard1CancelButtonClick" />
        </td>
      </tr>
    </table>
  </FinishNavigationTemplate>

  <HeaderTemplate>
    <table style="margin: auto">
      <tr>
        <td>
          <ul id="wizHeader">
            <asp:Repeater ID="SideBarList" runat="server">
              <ItemTemplate>
                <li>
                  <a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%# Eval("Name") %>" onclick=" SideBarClick(this) "><%# Eval("Name") %></a>
                </li>
              </ItemTemplate>
            </asp:Repeater>
          </ul>
        </td>
      </tr>
    </table>
  </HeaderTemplate>
</asp:Wizard>
