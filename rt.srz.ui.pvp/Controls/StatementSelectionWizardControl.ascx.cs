// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementSelectionWizardControl.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.logicalcontrol;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using rt.srz.services.Statement;
using StructureMap;
using rt.srz.ui.pvp.Enumerations;

#endregion

namespace rt.srz.ui.pvp.Controls
{
  using System.Collections.Generic;
  using System.Web;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps;

  using WizardStep = System.Web.UI.WebControls.WizardStep;
  using rt.srz.ui.pvp.Templates;
  using NHibernate;

  /// <summary>
  ///   The statement selection wizard control.
  /// </summary>
  public partial class StatementSelectionWizardControl : UserControl
  {
    /// <summary>
    ///   The _statement service.
    /// </summary>
    private IStatementService _statementService;

    private IRegulatoryService regulatoryService;

    /// <summary>
    ///   Gets a value indicating whether is step 4 blocked.
    /// </summary>
    private bool IsStep4Blocked
    {
      get { return CurrentStatement.ModeFiling != null && CurrentStatement.ModeFiling.Id == ModeFiling.ModeFiling1; }
    }

    /// <summary>
    ///   Gets a value indicating whether is step 5 blocked.
    /// </summary>
    private bool IsStep5Blocked
    {
      //поскольку для обычного полиса закоменчен ввод документов, то 5 шан пустой и его не надо отображать
      //если полис электронный и не требуется выдача нового полиса, то шаг пропускаем
      get { return CurrentStatement.FormManufacturing != null && (CurrentStatement.FormManufacturing.Id != PolisType.Э ||
        (CurrentStatement.FormManufacturing.Id == PolisType.Э && CurrentStatement.AbsentPrevPolicy.HasValue && !CurrentStatement.AbsentPrevPolicy.Value));
    }
    }

    protected Statement CurrentStatement
    {
      get
      {
        if (Session[SessionConsts.CCurrentStatement] != null)
        {
          return (Statement)Session[SessionConsts.CCurrentStatement];
        }

        if (Session[SessionConsts.CGuidStatementId] != null)
        {
          var statementId = (Guid)Session[SessionConsts.CGuidStatementId];
          var statementService = ObjectFactory.GetInstance<IStatementService>();
          var statement = statementService.GetStatement(statementId);
          if (statement != null)
          {
            return statement;
          }
        }
        
        return new Statement { Status = new Concept() };
      }

      set
      {
        Session[SessionConsts.CCurrentStatement] = value;
      }
    }

    protected Statement ExampleStatement
    {
      get
      {
        return Session[SessionConsts.CExampleStatement] != null ? (Statement)Session[SessionConsts.CExampleStatement] : new Statement { Status = new Concept() };
      }
      set
      {
        Session[SessionConsts.CExampleStatement] = value;
      }
    }

    protected Guid? PreviosStatementId
    {
      get
      {
        return Session[SessionConsts.CPreviosStatementId] != null ? (Guid?)Session[SessionConsts.CPreviosStatementId] : null;
      }
    }

    private bool IsNewStatement
    {
      get
      {
        if (CurrentStatement == null)
        {
          return true;
        }

        return CurrentStatement.Id == Guid.Empty;
      }
    }

    /// <summary>
    /// The page_ init.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Page_Init(object sender, EventArgs e)
    {
      _statementService = ObjectFactory.GetInstance<IStatementService>();
      regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
    }

    /// <summary>
    /// Проставление скрипта для кнопки закрыть
    /// </summary>
    private void SetOnClientClickCloseButton(string ItemTemplateCaptionId)
    {
      Button buttonClose = (Button)Wizard1.ActiveStep.FindControl(ItemTemplateCaptionId).FindControl("btnCancel");
      buttonClose.OnClientClick = confirm.ViewConfirmScriptForButton;
    }

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        if (Page.Master is AuthentificatedPage)
        {
          ((AuthentificatedPage)Page.Master).GotoMainPage += CloseStatement;
        }

        Session[SessionConsts.CInStatementEditing] = true;
        if (IsPostBack)
        {
          string eventArgs = Request["__EVENTARGUMENT"];
          if (!string.IsNullOrEmpty(eventArgs))
          {
            switch (eventArgs)
            {
              case "forward":
                GotoNextStep(new WizardNavigationEventArgs(Wizard1.ActiveStepIndex, Wizard1.ActiveStepIndex + 1));
                break;
              case "back":
                GotoPreviousStep(new WizardNavigationEventArgs(Wizard1.ActiveStepIndex, Wizard1.ActiveStepIndex - 1));
                break;
            }
          }

          // закрытие заявления подтверждённое
          UtilsHelper.PerformConfirmedAction(confirm, CloseStatement, Request);

          // если заявление отменённое то спрашиваем про смену статуса перед печатью //новое то надо задавать вопросы про подтверждение сохранения
          if (CurrentStatement.Status != null && CurrentStatement.Status.Id == StatusStatement.Cancelled)
          {
            // подтверждённое сохранение заявления перед печатью
            UtilsHelper.PerformConfirmedAction(confirmPrint, PrintStatement, Request);
            UtilsHelper.PerformConfirmedAction(confirmPrintVs, PrintVs, Request);

            ////// если заявление отменено то при попытке его сохранить предупреждение что статус сменится на новый.
            ////UtilsHelper.PerformConfirmedAction(confirmSave, SaveStatement, Request);
          }

          // подтверждённая печать без шаблона печати вс (в том числе по умолчанию)
          UtilsHelper.PerformConfirmedAction(сonfirmPrintVsWithoutTemplate, PrintVsOnly, Request);
        }

        if (!IsPostBack)
        {
          SetOnClientClickCloseButton("StartNavigationTemplateContainerID");
          SetOnClientClickCloseButton("StepNavigationTemplateContainerID");
          SetOnClientClickCloseButton("FinishNavigationTemplateContainerID");

          Statement st = null;

          var op = Session[SessionConsts.COperation];
          var operation = (StatementSearchMenuItem)(op ?? StatementSearchMenuItem.Reinsuranse);
          switch (operation)
          {
            case StatementSearchMenuItem.Reinsuranse:
              {
                step1.FillCauseFiling(x => CauseReinsurance.IsReinsurance(x.Id));

                st = ExampleStatement;


                st.Status = _statementService.GetConcept(StatusStatement.New);
                st.AbsentPrevPolicy = false;
              }

              break;
            case StatementSearchMenuItem.Reneval:
              {
                step1.FillCauseFiling(x => !CauseReinsurance.IsReinsurance(x.Id));
                st = ExampleStatement;
              }

              break;
            case StatementSearchMenuItem.Edit:
              {
                st = CurrentStatement;
                
                // поскольку текущее заявление было прибиндено к сессии на странице поиска, надо заново прибиндить уже к сессии хибернейта текушей страницы, 
                // иначе не инициализируются лэзи свойства
                ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Update(st);
                if (CauseReinsurance.IsReinsurance(st.CauseFiling.Id))
                {
                  step1.FillCauseFiling(x => CauseReinsurance.IsReinsurance(x.Id));
                }
                else
                {
                  step1.FillCauseFiling(x => !CauseReinsurance.IsReinsurance(x.Id));
                }
              }

              break;
          }

          CurrentStatement = st;

          //// Подтверждение сохранения заявления перед печатью
          //// если заявление новое или имеет статус отменено то надо задавать вопросы про подтверждение сохранения
          // вопросы про сохранение заявления теперь только для отменённых заявлений

          if (CurrentStatement != null && Wizard1.ActiveStep != null && CurrentStatement.Status != null
              && CurrentStatement.Status.Id == StatusStatement.Cancelled)
          {
            var control = Wizard1.ActiveStep.FindControl("FinishNavigationTemplateContainerID");
            if (control != null)
            {
              var buttonPrint = (Button)control.FindControl("btnFinish");
              if (buttonPrint != null)
              {
                buttonPrint.OnClientClick = confirmPrint.ViewConfirmScriptForButton;
              }
              
              var buttonPrintTemp = (Button)control.FindControl("btnPrintTemporaryCertificate");
              if (buttonPrintTemp != null)
              {
                buttonPrintTemp.OnClientClick = confirmPrintVs.ViewConfirmScriptForButton;
              }
            }

            confirmPrint.Message = "Заявление будет переведено в статус 'Новое' и пойдет в обработку";
            confirmPrintVs.Message = "Заявление будет переведено в статус 'Новое' и пойдет в обработку";
          }

          ////// если заявление отменено то при попытке его сохранить предупреждение что статус сменится на новый. и для печати тоже изменится мессага про то что сменится статус у заявления
          ////if (CurrentStatement != null && (CurrentStatement.Status != null
          ////                                 && Wizard1.ActiveStep != null
          ////                                 && CurrentStatement.Status.Id == StatusStatement.Cancelled))
          ////{
          ////  var control = Wizard1.ActiveStep.FindControl("FinishNavigationTemplateContainerID");
          ////  var findControl = control;
          ////  if (findControl != null)
          ////  {
          ////    var btnSave = (Button)findControl.FindControl("btnSaveStatement");
          ////    btnSave.OnClientClick = confirmSave.ViewConfirmScriptForButton;
          ////  }
          ////}

          // загрузка данных из объекта
          if (st != null)
          {
            step1.MoveDataFromObject2GUI(st);
            step2.MoveDataFromObject2GUI(st);
            step3.MoveDataFromObject2GUI(st);
            step4.MoveDataFromObject2GUI(st);
            step5.MoveDataFromObject2GUI(st);
            step6.MoveDataFromObject2GUI(st);

            // Внешние ошибки
            if (st.Errors != null && st.Errors.Any())
            {
              cvErrors.Text = st.Errors
                .Select(x => string.IsNullOrEmpty(x.Repl) ? x.Message1 : string.Format("{0} ({1})", x.Message1, x.Repl))
                .Aggregate((workingSentence, next) => string.Format("{0}; \r\n {1}", next, workingSentence));
            }
          }

          if (Session[SessionConsts.CStep] != null)
          {
            var step = (int)Session[SessionConsts.CStep];
            GoToStep(step);
          }

          if (st != null && st.Status != null && st.Status.Id == StatusStatement.Exercised)
          {
            var control = Wizard1.ActiveStep.FindControl("FinishNavigationTemplateContainerID");
            ((Button)control.FindControl("btnSaveStatement")).Enabled = false;
            ((Button)control.FindControl("btnFinish")).Enabled = false;
            ((Button)control.FindControl("btnPrintTemporaryCertificate")).Enabled = false;
          }
        }

        Wizard1.PreRender += Wizard1PreRender;
      }
      catch (Exception ex)
      {
        NLog.LogManager.GetCurrentClassLogger().Error(ex.Message, ex);
        throw;
      }
    }

    private void CloseStatement()
    {
      Wizard1CancelButtonClick(null, null);
    }

    private void PrintStatement()
    {
      Wizard1PrintSatatementClick(null, new WizardNavigationEventArgs(Wizard1.ActiveStepIndex, -1));
    }

    private void PrintVs()
    {
      Wizard1PrintTemporaryCertificateClick(null, null);
    }

    private void SaveStatement()
    {
      Wizard1SaveStatement(null, null);
    }

    /// <summary>
    /// The wizard 1_ save statement.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Wizard1SaveStatement(object sender, EventArgs e)
    {
      // Попытка сохранения заявления
      if (!SaveStatementInternal())
        return;

      RedirectUtils.ClearInStatementEditing();

      // Очистка веб сессии
      var searchCriteria = (Session[StatementsSearch.SearchCriteriaViewStateKey] as SearchStatementCriteria) ?? new SearchStatementCriteria
                                                                                                                {
                                                                                                                  Take = 5,
                                                                                                                  Skip = 0,
                                                                                                                };
      if (searchCriteria.SearchResult == null)
      {
        searchCriteria.SearchResult = new SearchResult<SearchStatementResult>();

      }

      if (searchCriteria.SearchResult.Rows == null)
      {
        searchCriteria.SearchResult.Rows = new List<SearchStatementResult>();
      }

      var item = searchCriteria.SearchResult.Rows.FirstOrDefault(x => x.Id == CurrentStatement.Id);
      if (item != null)
      {
        searchCriteria.SearchResult.Rows.Remove(item);
      }

      searchCriteria.SearchResult.Rows.Insert(0, _statementService.GetSearchStatementResult(CurrentStatement.Id));

      //CurrentStatement хранится в сессии поэтому здесь отдельно до очистки сессии
      string redirectLink = "~/Pages/Main.aspx";
      if (CurrentStatement.PolisMedicalInsurance != null && CurrentStatement.PolisMedicalInsurance.PolisType != null)
      {
        switch (CurrentStatement.PolisMedicalInsurance.PolisType.Id)
        {
          case PolisType.К:
            redirectLink = string.Format("~/Pages/UECWrite.aspx?StatementId={0}", CurrentStatement.Id);
            break;
          case PolisType.Э:
            redirectLink = string.Format("~/Pages/SMCWrite.aspx?StatementId={0}", CurrentStatement.Id);
            break;
          default:
            redirectLink = "~/Pages/Main.aspx";
            break;
        }
      }
        
      Session.Clear();
      Session[StatementsSearch.SearchCriteriaViewStateKey] = searchCriteria;

      // Переход на главную страницу, либо страницу записи на УЭК или Smart Card
      Response.Redirect(redirectLink);
    }

    /// <summary>
    /// The wizard 1_ finish button click.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Wizard1PrintSatatementClick(object sender, WizardNavigationEventArgs e)
    {
      // Попытка сохранения заявления
      if (!SaveStatementInternal())
      {
        e.Cancel = true;
        return;
      }

      // Печать заявления
      Session[SessionConsts.COperation] = StatementSearchMenuItem.Edit;
      Session[SessionConsts.CGuidStatementId] = CurrentStatement.Id;
      RedirectUtils.RedirectToPrintStatement(Response);
    }

    /// <summary>
    /// The wizard 1_ cancel button click.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Wizard1CancelButtonClick(object sender, EventArgs e)
    {
      ////// Очистка веб сессии
      ////Session.Clear();
      Session[SessionConsts.CExampleStatement] = null;

      RedirectUtils.ClearInStatementEditing();
      Session[SessionConsts.CStep] = null;
      step5.ClearSessionData();

      // Переход на главную страницу
      Response.Redirect("~/Pages/Main.aspx");
    }

    /// <summary>
    /// The wizard 1_ active step changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Wizard1ActiveStepChanged(object sender, EventArgs e)
    {
      switch ((StatementSelectionWizardStep)Wizard1.ActiveStepIndex)
      {
        case StatementSelectionWizardStep.Step1:
          step1.SetDefaultFocus();
          break;
        case StatementSelectionWizardStep.Step2:
          step2.CheckIsRightToEdit();
          step2.SetDefaultFocus();
          break;
        case StatementSelectionWizardStep.Step3:
          step3.CopyUdlDataFromStep2();
          step3.CheckIsRightToEdit();
          step3.SetDefaultFocus();
          break;
        case StatementSelectionWizardStep.Step4:
          step4.CheckIsRightToEdit();
          step4.SetDefaultFocus();
          break;
        case StatementSelectionWizardStep.Step5:
          step5.ShowOrHideElectronicPolicyPart();
          step5.CheckIsRightToEdit();
          step5.SetDefaultFocus();
          break;
        case StatementSelectionWizardStep.Step6:
          step6.CopyDateFillingFromStep1();
          step6.CheckIsRightToEdit();
          step6.SetDefaultFocus();
          break;
      }
    }

    /// <summary>
    /// The hidden btn_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void HiddenBtnClick(object sender, EventArgs e)
    {
      var targetStep = string.IsNullOrEmpty(hiddenTb.Text) ? 0 : int.Parse(hiddenTb.Text.Substring(4));
      if ((targetStep == 0) || (IsStep4Blocked && targetStep == 4) || (IsStep5Blocked && targetStep == 5))
      {
        return;
      }

      if (targetStep > Wizard1.ActiveStepIndex + 1)
      {
        //Page.Validate();
        if (!Page.IsValid)
        {
          return;
        }
      }

      GoToStep(targetStep);
    }

    /// <summary>
    /// The wizard 1_ next button click.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Wizard1NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
      GotoNextStep(e);
    }

    /// <summary>
    /// The wizard 1 print temporary certificate.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Wizard1PrintTemporaryCertificateClick(object sender, EventArgs e)
    {
      if (!SaveStatementInternal())
      {
        return;
      }

      Template template = regulatoryService.GetTemplateByStatement(CurrentStatement);
      Session[SessionConsts.CTemplateVsForPrint] = template;
      Session[SessionConsts.COperation] = StatementSearchMenuItem.Edit;
      Session[SessionConsts.CGuidStatementId] = CurrentStatement.Id;

      // если не нашли шаблон печати то выдаём сообщение
      if (template == null)
      {
        сonfirmPrintVsWithoutTemplate.Show();
      }
      else
      {
        RedirectUtils.RedirectToPrintTemporaryCertificate(Response);
      }
    }

    private void PrintVsOnly()
    {
      RedirectUtils.RedirectToPrintTemporaryCertificate(Response);
    }

    private void GotoNextStep(WizardNavigationEventArgs e)
    {
      //Page.Validate();

      //валидация по нажатию на кнопки и так вызывается поэтому просто используем извалид, но в случае когда переход на шаг клавиатурой, 
      //то метод валидации не вызывается и его надо вызвать руками. не вызовется и метод MoveDataFromGuiToObject вкладки
      var validated = false;
      try
      {
        validated = Page.IsValid;
      }
      catch
      {
        MoveDataFromGuiToObject(e.CurrentStepIndex);
        Page.Validate();
        validated = Page.IsValid;
      }

      if (!validated)
      {
        e.Cancel = true;
      }
      else
      {
        GoToStep(e.CurrentStepIndex + 2);
      }
    }

    private void MoveDataFromGuiToObject(int currentStepIndex)
    {
      var st = CurrentStatement;
      switch (currentStepIndex)
      {
        case 0:
          step1.MoveDataFromGui2Object(ref st);
          break;
        case 1:
          step2.MoveDataFromGui2Object(ref st);
          break;
        case 2:
          step3.MoveDataFromGui2Object(ref st);
          break;
        case 3:
          step4.MoveDataFromGui2Object(ref st);
          break;
        case 4:
          step5.MoveDataFromGui2Object(ref st);
          break;
        case 5:
          step6.MoveDataFromGui2Object(ref st);
          break;
      }
    }

    /// <summary>
    /// The wizard 1_ previous button click.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Wizard1PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {
      GotoPreviousStep(e);
    }

    private void GotoPreviousStep(WizardNavigationEventArgs e)
    {
      GoToStep(e.CurrentStepIndex);
    }

    /// <summary>
    /// The get class for wizard step.
    /// </summary>
    /// <param name="wizardStep">
    /// The wizard step. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    protected string GetClassForWizardStep(object wizardStep)
    {
      var step = wizardStep as WizardStep;

      if (step == null)
      {
        return string.Empty;
      }

      var stepIndex = Wizard1.WizardSteps.IndexOf(step);

      if (stepIndex < Wizard1.ActiveStepIndex)
      {
        return "prevStep";
      }

      return stepIndex > Wizard1.ActiveStepIndex ? "nextStep" : "currentStep";
    }

    /// <summary>
    ///   The save statement.
    /// </summary>
    /// <returns> The <see cref="bool" /> . </returns>
    private bool SaveStatementInternal()
    {
      try
      {
        if (!ValidateSteps())
        {
          var draftStatement = MakeStatement();
          CurrentStatement = _statementService.SaveStatement(draftStatement);
          return true;
        }
      }
      catch (FaultPersonAlreadyBelongsToSmoException)
      {
        // Меняем операцию на переоформление
        Session[SessionConsts.COperation] = StatementSearchMenuItem.Reneval;
        step1.FillCauseFiling(x => !CauseReinsurance.IsReinsurance(x.Id));
        CurrentStatement.CauseFiling = null;
        cvErrors.Text = "Лицо принадлежит текущей СМО, замена СМО невозможна, поменяйте причину подачи заявления.";
        GoToStep(1);
        return false;
      }
      catch (LogicalControlException exception)
      {
        cvErrors.Text = exception.GetAllMessages();

        // Проверяем есть ли ошибка дубль по документу
        if (exception.Contains<FaultDocumentUdlExistsException>())
        {
          step2.documentUDL.ShowCheckBoxValidDocument();
        }

        // Проверяем есть ли ошибка дубль по СНИЛСУ
        if (exception.Contains<FaultSnilsExistsException>())
        {
          step2.SetSnilsExistError();
        }

        GoToStep(exception.ToStep());
      }
      catch (Exception)
      {
        cvErrors.Text =
          "При сохранении заявления произошла ошибка. Обратитесь к администратору. В письме укажите время возникновения ошибки.";
      }

      // todo строка закоменчена т.к. при наличии ошибок до заполнения statement не дойдём - MakeStatement()
      ////_statementService.UnBindStatement(statement);

      return false;
    }

    /// <summary>
    /// The go to step.
    /// </summary>
    /// <param name="step">
    /// The step. 
    /// </param>
    private void GoToStep(int step)
    {
      if (Wizard1.ActiveStepIndex == 4 && step == 4 && IsStep4Blocked)
      {
        step = 3;
      }

      if (Wizard1.ActiveStepIndex == 5 && step == 5 && IsStep5Blocked)
      {
        step = IsStep4Blocked ? 3 : 4;
      }

      if (Wizard1.ActiveStepIndex == 2 && step == 4 && IsStep4Blocked)
      {
        step = IsStep5Blocked ? 6 : 5;
      }

      if (Wizard1.ActiveStepIndex == 3 && step == 5 && IsStep5Blocked)
      {
        step = 6;
      }

      var index = 2;

      if (Wizard1.ActiveStepIndex == 2 && IsStep4Blocked)
      {
        index = IsStep5Blocked ? 4 : 3;
      }

      if (Wizard1.ActiveStepIndex == 3 && IsStep5Blocked)
      {
        index = 3;
      }

      var isAvailableStep = FindControl("IsAvailableStep" + (Wizard1.ActiveStepIndex + index)) as HiddenField;

      if (!IsStep4Blocked && step == 4 && bool.Parse(IsAvailableStep2.Value) && bool.Parse(IsAvailableStep3.Value))
      {
        isAvailableStep = FindControl("IsAvailableStep4") as HiddenField;
      }

      if (!IsStep5Blocked && step == 5 && bool.Parse(IsAvailableStep2.Value) && bool.Parse(IsAvailableStep3.Value))
      {
        isAvailableStep = FindControl("IsAvailableStep5") as HiddenField;

        if (isAvailableStep != null)
        {
          isAvailableStep.Value = "true";
        }
      }

      if (isAvailableStep != null)
      {
        isAvailableStep.Value = "true";
      }

      Session[SessionConsts.CStep] = step;
      if (!IsNewStatement)
      {
        switch (step)
        {
          case 1:
            Wizard1.MoveTo(WizardStep1);
            break;
          case 2:
            Wizard1.MoveTo(WizardStep2);
            break;
          case 3:
            Wizard1.MoveTo(WizardStep3);
            break;
          case 4:
            Wizard1.MoveTo(WizardStep4);
            break;
          case 5:
            Wizard1.MoveTo(WizardStep5);
            break;
          case 6:
            Wizard1.MoveTo(WizardStep6);
            break;
        }
      }
      else
      {
        switch (step)
        {
          case 1:
            Wizard1.MoveTo(WizardStep1);
            break;
          case 2:
            if (bool.Parse(IsAvailableStep2.Value))
            {
              Wizard1.MoveTo(WizardStep2);
            }

            break;
          case 3:
            if (bool.Parse(IsAvailableStep3.Value))
            {
              Wizard1.MoveTo(WizardStep3);
            }

            break;
          case 4:
            if (bool.Parse(IsAvailableStep4.Value))
            {
              Wizard1.MoveTo(WizardStep4);
            }

            break;
          case 5:
            if (bool.Parse(IsAvailableStep5.Value))
            {
              Wizard1.MoveTo(WizardStep5);
            }

            break;
          case 6:
            if (bool.Parse(IsAvailableStep6.Value))
            {
              Wizard1.MoveTo(WizardStep6);
            }

            break;
        }
      }
    }

    /// <summary>
    /// The wizard 1 pre render.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    private void Wizard1PreRender(object sender, EventArgs e)
    {
      var sideBarList = Wizard1.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
      if (sideBarList == null)
        return;

      sideBarList.DataSource = Wizard1.WizardSteps;
      sideBarList.DataBind();

      //При вводе заявления по клавише ентер должен вызываться "продолжить",а на последнем шаге на "Сохранить заявление"
      if (Wizard1.ActiveStep.ID == "WizardStep1")
      {
        //Page.Master.Page.Form.DefaultButton = Wizard1.ActiveStep.FindControl("StartNavigationTemplateContainerID").FindControl("btnNext").UniqueID;
        //Page.Master.Page.Form.DefaultFocus = Wizard1.ActiveStep.FindControl("StartNavigationTemplateContainerID").FindControl("btnNext").ClientID;
      }
      else if (Wizard1.ActiveStep.ID == "WizardStep6")
      {
        //Page.Master.Page.Form.DefaultButton = Wizard1.ActiveStep.FindControl("FinishNavigationTemplateContainerID").FindControl("btnSaveStatement").UniqueID;
        //Page.Master.Page.Form.DefaultFocus = Wizard1.ActiveStep.FindControl("FinishNavigationTemplateContainerID").FindControl("btnSaveStatement").ClientID;
      }
      else
      {
        //Page.Master.Page.Form.DefaultButton = Wizard1.ActiveStep.FindControl("StepNavigationTemplateContainerID").FindControl("btnNext").UniqueID;
        //Page.Master.Page.Form.DefaultFocus = Wizard1.ActiveStep.FindControl("StepNavigationTemplateContainerID").FindControl("btnNext").ClientID;
      }
    }

    /// <summary>
    ///   The validate steps.
    /// </summary>
    /// <returns> The <see cref="bool" /> . </returns>
    private bool ValidateSteps()
    {
      ////const string Msg = "Не заполнены обязательные поля на шаге ";
      ////step6.cvSteps.Text = string.Empty;
      ////if (!step2.Validate())
      ////{
      ////  step6.cvSteps.Text = string.Format("{0}2", Msg);
      ////}

      ////if (!step3.Validate())
      ////{
      ////  if (string.IsNullOrEmpty(step6.cvSteps.Text))
      ////  {
      ////    step6.cvSteps.Text = string.Format("{0}3", Msg);
      ////  }
      ////  else
      ////  {
      ////    step6.cvSteps.Text += ", 3";
      ////  }
      ////}

      ////if (!IsStep4Blocked && !step4.Validate())
      ////{
      ////  if (string.IsNullOrEmpty(step6.cvSteps.Text))
      ////  {
      ////    step6.cvSteps.Text = string.Format("{0}4", Msg);
      ////  }
      ////  else
      ////  {
      ////    step6.cvSteps.Text += ", 4";
      ////  }
      ////}

      ////if (!string.IsNullOrEmpty(step6.cvSteps.Text))
      ////{
      ////  step6.cvSteps.Text += "!";
      ////  step6.cvSteps.IsValid = false;
      ////}

      // специально добавлен этот блок. Т.к. при печати следует вопрос - заявление будет сохранено, продолжить? 
      // При положительном ответе идёт постбэк с соответсвующими параметрами, ловится на лоаде и соответственно вызываются действия. 
      // В лоаде еще не отработает валидация(которая вызывается при сохранении заявления) и будет падать всё на обращении к свойству Page.IsValid
      if (Page.IsPostBack)
      {
        Page.Validate();
      }

      // Валидация последней страницы
      return !Page.IsValid;
    }

    /// <summary>
    ///   The make statement.
    /// </summary>
    private Statement MakeStatement()
    {

      // Загрузка заявления из базы
      var currentStatement = _statementService.GetStatement(CurrentStatement.Id);

      // Получение данных из элементов GUI
      var result = currentStatement ?? new Statement();
      step1.MoveDataFromGui2Object(ref result, false);
      step2.MoveDataFromGui2Object(ref result, false);
      step3.MoveDataFromGui2Object(ref result, false);
      step4.MoveDataFromGui2Object(ref result, false);
      step5.MoveDataFromGui2Object(ref result, false);
      step6.MoveDataFromGui2Object(ref result, false);

      if (PreviosStatementId.HasValue && PreviosStatementId.Value != Guid.Empty)
      {
        // Загрузка заявления из базы
        result.PreviousStatement = _statementService.GetStatement(PreviosStatementId.Value);
      }

      return result;
    }
  }
}