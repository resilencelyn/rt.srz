// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Step1.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps
{
  #region

  using System;
  using System.Globalization;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Web.UI.WebControls;

  using rt.srz.model.algorithms;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;
  using rt.srz.services;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The step 1.
  /// </summary>
  public partial class Step1 : WizardStep
  {
    #region Fields

    /// <summary>
    ///   The _statement service.
    /// </summary>
    private IStatementService statementService;

    private IRegulatoryService regulatoryService;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Проверка доступности элементов редактирования для ввода
    /// </summary>
    public override void CheckIsRightToEdit()
    {
      if (fStatus.Value != StatusStatement.New.ToString(CultureInfo.InvariantCulture)
        && fStatus.Value != StatusStatement.Cancelled.ToString(CultureInfo.InvariantCulture)
        && fStatus.Value != StatusStatement.Declined.ToString(CultureInfo.InvariantCulture))
      {
        chbNewPolicy.Enabled = false;
        chbHasPetition.Enabled = false;
        ddlPolicyType.Enabled = false;
        ddlCauseFiling.Enabled = false;
        tbDateFiling.Enabled = false;
        ddlModeFiling.Enabled = false;

        if (fStatus.Value == StatusStatement.Exercised.ToString(CultureInfo.InvariantCulture))
        {
          tbNumberPolicy.Enabled = false;
        }

        return;
      }

      if (ddlCauseFiling.SelectedValue == CauseReneval.Edit.ToString(CultureInfo.InvariantCulture))
      {
        chbNewPolicy.Enabled = false;

        // если переоформляем заявление и указываем в качестве причины последнюю (когда дизаблится тип полиса), то при переходе на 2 шаг и возврате обратно обнаруживаем, что
        // поле тип полиса задизаблено но пропало значение которое было - бумажный полис.
        if (int.Parse(hfPolicyType.Value) > 0)
        {
          ddlPolicyType.SelectedValue = hfPolicyType.Value;
        }

        ddlPolicyType.Enabled = false;
        chbHasPetition.Enabled = false;
      }
      else
      {
        chbHasPetition.Enabled = true;
      }
    }

    /// <summary>
    /// The fill cause filing.
    /// </summary>
    /// <param name="expression">
    /// The expression. 
    /// </param>
    public void FillCauseFiling(Expression<Func<Concept, bool>> expression)
    {
      ddlCauseFiling.Items.Clear();
      ddlCauseFiling.Items.Add(new ListItem("Выберите причину", "-1"));
      ddlCauseFiling.Items
        .AddRange(statementService.GetNsiRecords(new[] { Oid.ПричинаподачизаявлениянавыборилизаменуСмо, Oid.Причинаподачизаявлениянавыдачудубликата })
        .Where(x => x.Id != CauseReinsurance.Initialization)
        .Where(x => x.Id != CauseReneval.GettingTheFirst)
        .Where(expression.Compile())
        .Select(x => new ListItem(x.Description, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
    }

    /// <summary>
    /// Переносит данные из элементов на форме в объект
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <param name="setCurrentStatement">
    /// Обновлять ли свойство CurrentStatement после присвоения заявлению данных из дизайна 
    /// </param>
    public override void MoveDataFromGui2Object(ref Statement statement, bool setCurrentStatement = true)
    {
      if (statement == null)
      {
        return;
      }

      // Дата подачи заявления
      if (!string.IsNullOrEmpty(tbDateFiling.Text))
      {
        DateTime dt;
        statement.DateFiling = DateTime.TryParse(tbDateFiling.Text, out dt) ? (DateTime?)dt : null;
      }

      // Причина подачи заявления
      var causeFilling = int.Parse(ddlCauseFiling.SelectedValue);
      if (causeFilling >= 0)
      {
        statement.CauseFiling = regulatoryService.GetConcept(causeFilling);
      }

      // Способ подачи
      var modeFilling = int.Parse(ddlModeFiling.SelectedValue);
      if (modeFilling >= 0)
      {
        statement.ModeFiling = regulatoryService.GetConcept(modeFilling);
      }

      // Наличие ходатайства о регистрации в качестве застрахованного лица
      statement.HasPetition = chbHasPetition.Checked;

      // Форма изготовления полиса
      var formManufacturing = int.Parse(hfPolicyType.Value);
      if (formManufacturing >= 0)
      {
        statement.FormManufacturing = regulatoryService.GetConcept(formManufacturing);
      }

      // Отсутствует ранее выданный полис
      bool newPolis;
      statement.AbsentPrevPolicy = bool.TryParse(hfNewPolicy.Value, out newPolis) ? newPolis : chbNewPolicy.Checked;

      // Номер ранее выданного полиса
      statement.NumberPolicy = tbNumberPolicy.Text == string.Empty ? null : tbNumberPolicy.Text;
      statement.NotCheckPolisNumber = cbNotCheckEnp.Checked;

      // сохранение изменений в сессию
      if (setCurrentStatement)
      {
        CurrentStatement = statement;
      }
    }

    /// <summary>
    /// Переносит данные из объекта в элементы на форме
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    public override void MoveDataFromObject2GUI(Statement statement)
    {
      fStatus.Value = statement.Status != null
                        ? statement.Status.Id.ToString(CultureInfo.InvariantCulture)
                        : StatusStatement.New.ToString(CultureInfo.InvariantCulture);

      tbDateFiling.Text = statement.DateFiling.HasValue ? ((DateTime)statement.DateFiling).ToShortDateString() : DateTime.Today.ToShortDateString();

      // Причина подачи заявления
      if (statement.CauseFiling != null)
      {
        // если заявление импортировано, то причины подачи заявления не будет и надо его добавить в общий список комбика, но при этом комбик должен быть недоступен для смены значения
        if (statement.CauseFiling.Id == CauseReinsurance.Initialization)
        {
          AddInitializationCauseFiling();
          ddlCauseFiling.Enabled = false;
        }

        ddlCauseFiling.SelectedValue = statement.CauseFiling.Id.ToString(CultureInfo.InvariantCulture);
        if (ddlCauseFiling.SelectedValue == CauseReinsurance.Choice.ToString(CultureInfo.InvariantCulture))
        {
          chbNewPolicy.Enabled = false;
        }
      }

      // Способ подачи
      if (statement.ModeFiling != null)
      {
        ddlModeFiling.SelectedValue = statement.ModeFiling.Id.ToString(CultureInfo.InvariantCulture);
      }

      // Наличие ходатайства о регистрации в качестве застрахованного лица
      if (statement.HasPetition != null)
      {
        chbHasPetition.Checked = (bool)statement.HasPetition;
      }

      // Форма изготовления полиса
      if (statement.FormManufacturing != null)
      {
        ddlPolicyType.SelectedValue = statement.FormManufacturing.Id.ToString(CultureInfo.InvariantCulture);
        hfPolicyType.Value = ddlPolicyType.SelectedValue;
      }

      chbNewPolicy.Checked = statement.AbsentPrevPolicy.HasValue && statement.AbsentPrevPolicy.Value;
      hfNewPolicy.Value = chbNewPolicy.Checked ? "1" : "0";

      // Номер полиса
      tbNumberPolicy.Text = statement.NumberPolicy;

      cbNotCheckEnp.Checked = statement.NumberPolicy != null && statement.NumberPolicy.Length == 16 && statement.InsuredPersonData != null && statement.InsuredPersonData.Birthday.HasValue
        && statement.InsuredPersonData.Gender != null
        && (!EnpChecker.CheckIdentifier(statement.NumberPolicy) ||
         !EnpChecker.CheckBirthdayAndGender(
              statement.NumberPolicy,
              statement.InsuredPersonData.Birthday.Value,
              statement.InsuredPersonData.Gender.Code == "1"));
      if (cbNotCheckEnp.Checked)
      {
        cbNotCheckEnp.Visible = true;
      }

      CheckIsRightToEdit();
    }

    /// <summary>
    ///   Установка фокуса на контрол при смене шага
    /// </summary>
    public override void SetDefaultFocus()
    {
      try
      {
        ddlCauseFiling.Focus();
      }
      catch
      {
      }
    }

    /// <summary>
    /// The validate cause filing.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    public void ValidateCauseFiling(object source, ServerValidateEventArgs args)
    {
      args.IsValid = statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.CauseFiling.Id));
    }

    #endregion

    #region Methods

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
      statementService = ObjectFactory.GetInstance<IStatementService>();
      regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
      if (!IsPostBack)
      {
        FillModeFiling();
        FillFormManufacturing();
      }
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
      // вместо SetDefaultFocus используем здесь т.к. при первом запросе SetDefaultFocus на смене индекса шага еще не будет формы и вывалится ошибка
      if (!IsPostBack)
      {
        ddlCauseFiling.Focus();
      }

      if (IsPostBack)
      {
        int cause;
        int.TryParse(ddlCauseFiling.SelectedValue, out cause);

        // Восстановление состояния элементов, измененных JavaScript
        if ((cause == CauseReinsurance.Choice || CauseReneval.IsReneval(cause)) && cause != CauseReneval.Edit)
        {
          chbNewPolicy.Checked = true;
          chbNewPolicy.Enabled = false;
        }

        CheckIsRightToEdit();
      }

      // при открытии на просмотр полиса надо восстанавливать корректное название лабела
      lbPolicyType.Text = chbNewPolicy.Checked ? "Тип полиса*" : "Тип имеющегося полиса*";

      if (IsPostBack)
      {
        var st = CurrentStatement;
        MoveDataFromGui2Object(ref st);
      }
    }

    /// <summary>
    /// The validate number policy.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateNumberPolicy(object source, ServerValidateEventArgs args)
    {
      if (string.IsNullOrEmpty(tbNumberPolicy.Text))
      {
        return;
      }

      args.IsValid = statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.NumberPolicy));
      cbNotCheckEnp.Visible = true;
    }

    /// <summary>
    /// The validate policy type.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidatePolicyType(object source, ServerValidateEventArgs args)
    {
      args.IsValid = statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.FormManufacturing.Id));
    }

    /// <summary>
    /// The add initialization cause filing.
    /// </summary>
    private void AddInitializationCauseFiling()
    {
      ddlCauseFiling.Items.Add(statementService.GetNsiRecords(new[] { Oid.ПричинаподачизаявлениянавыборилизаменуСмо, Oid.Причинаподачизаявлениянавыдачудубликата })
                                               .Where(x => x.Id == CauseReinsurance.Initialization)
                                               .Select(x => new ListItem(x.Description, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray().First());
    }

    /// <summary>
    ///   The fill mode filing.
    /// </summary>
    private void FillModeFiling()
    {
      ddlModeFiling.Items.AddRange(
        statementService.GetNsiRecords(Oid.Способподачизаявления).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
    }

    /// <summary>
    /// The fill form manufacturing.
    /// </summary>
    private void FillFormManufacturing()
    {
      int causeFillingId;
      int.TryParse(ddlCauseFiling.SelectedValue, out causeFillingId);

      ddlPolicyType.Items.Clear();
      ddlPolicyType.Items.Add(new ListItem("Выберите тип полиса", "-1"));
      ddlPolicyType.Items.AddRange(
        statementService.GetFormManufacturingByCauseFilling(causeFillingId).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
    }

    #endregion
  }
}