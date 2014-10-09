// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Step6.ascx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Web.UI.WebControls;

  using rt.srz.model.interfaces.service;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;
  using rt.srz.services;

  using StructureMap;

  /// <summary>
  ///   The step 6.
  /// </summary>
  public partial class Step6 : WizardStep
  {
    #region Fields

    /// <summary>
    ///   The statement service.
    /// </summary>
    private IStatementService statementService;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the previos statement id.
    /// </summary>
    protected Guid? PreviosStatementId
    {
      get
      {
        return Session[SessionConsts.CPreviosStatementId] != null
                 ? (Guid?)Session[SessionConsts.CPreviosStatementId]
                 : null;
      }
    }

    /// <summary>
    /// Gets the polis end date.
    /// </summary>
    private DateTime PolisEndDate
    {
      get
      {
        if (CurrentStatement.InsuredPersonData != null
            && ((CurrentStatement.InsuredPersonData.Citizenship != null
                 && CurrentStatement.InsuredPersonData.Citizenship.Id != Country.RUS)
                || CurrentStatement.InsuredPersonData.Citizenship == null))
        {
          var dateEnd = new DateTime(1900, 1, 1);
          if (CurrentStatement.DocumentUdl.DateExp.HasValue)
          {
            dateEnd = CurrentStatement.DocumentUdl.DateExp.Value;
          }

          if (CurrentStatement.ResidencyDocument != null && CurrentStatement.ResidencyDocument.DateExp.HasValue)
          {
            dateEnd = CurrentStatement.ResidencyDocument.DateExp.Value;
          }

          if (CurrentStatement.CauseFiling != null && CurrentStatement.CauseFiling.Id == CauseReneval.Edit)
          {
            if (PreviosStatementId.HasValue && PreviosStatementId.Value != Guid.Empty)
            {
              var previousStatement =
                ObjectFactory.GetInstance<IStatementService>().GetStatement(PreviosStatementId.Value);
              var polis = previousStatement.PolisMedicalInsurance;
              if (polis != null)
              {
                dateEnd = polis.DateTo;
              }
            }
          }

          return dateEnd;
        }

        return new DateTime(2200, 1, 1);
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Проверка доступности элементов редактирования для ввода
    /// </summary>
    public override void CheckIsRightToEdit()
    {
      var propertyList = GetPropertyListForCheckIsRightToEdit();

      // ВС
      tbTemporaryCertificateNumber.Enabled = statementService.IsRightToEdit(
                                                                            propertyList,
                                                                            Utils.GetExpressionNode(x => x.MedicalInsurances[0].PolisNumber));
      tbTemporaryCertificateDateIssue.Enabled = statementService.IsRightToEdit(
                                                                               propertyList,
                                                                               Utils.GetExpressionNode(x => x.MedicalInsurances[0].DateFrom));

      tbPolicyNumber.MaxLength = CurrentStatement.FormManufacturing != null
                                 && CurrentStatement.FormManufacturing.Id == PolisType.К ? 14 : 11;

      if (CurrentStatement.AbsentPrevPolicy != null)
      {
        lbTemporaryCertificate.Visible =
          tbTemporaryCertificateNumber.Visible =
          lblTemporaryCertificateNumber.Visible =
          tbTemporaryCertificateDateIssue.Visible =
          lblTemporaryCertificateDateIssue.Visible = CurrentStatement.AbsentPrevPolicy.Value; /*NeedNewPolicy*/
      }

      if (CurrentStatement.CauseFiling != null)
      {
        // Полис
        if (CauseReinsurance.IsReinsurance(CurrentStatement.CauseFiling.Id))
        {
          if (CurrentStatement.AbsentPrevPolicy.Value /*NeedNewPolicy*/)
          {
            chbPolicyIsIssued.Enabled = statementService.IsRightToEdit(
                                                                       propertyList,
                                                                       Utils.GetExpressionNode(x => x.PolicyIsIssued));
          }
          else
          {
            chbPolicyIsIssued.Enabled = false;
            chbPolicyIsIssued.Checked = true;
          }
        }
        else
        {
          if (CurrentStatement.CauseFiling.Id == CauseReneval.Edit)
          {
            chbPolicyIsIssued.Enabled = false;
            chbPolicyIsIssued.Checked = true;
          }
          else
          {
            chbPolicyIsIssued.Enabled = statementService.IsRightToEdit(
                                                                       propertyList,
                                                                       Utils.GetExpressionNode(x => x.PolicyIsIssued));
          }
        }
      }

      tbEnpNumber.Enabled = chbPolicyIsIssued.Checked
                            && statementService.IsRightToEdit(
                                                              propertyList,
                                                              Utils.GetExpressionNode(x => x.MedicalInsurances[1].Enp));
      tbPolicyNumber.Enabled = chbPolicyIsIssued.Checked
                               && statementService.IsRightToEdit(
                                                                 propertyList,
                                                                 Utils.GetExpressionNode(
                                                                                         x =>
                                                                                         x.MedicalInsurances[1]
                                                                                           .PolisNumber));
      tbPolicyDateIssue.Enabled = chbPolicyIsIssued.Checked
                                  && statementService.IsRightToEdit(
                                                                    propertyList,
                                                                    Utils.GetExpressionNode(
                                                                                            x =>
                                                                                            x.MedicalInsurances[1]
                                                                                              .DateFrom));

      if (CurrentStatement.AbsentPrevPolicy != null)
      {
        if (!CurrentStatement.AbsentPrevPolicy.Value /*!NeedNewPolicy*/)
        {
          if (string.IsNullOrEmpty(tbPolicyDateIssue.Text))
          {
            tbPolicyDateIssue.Text = DateTime.Now.ToString("dd.MM.yyyy");
          }

          Parent.FindControl("FinishNavigationTemplateContainerID").FindControl("btnPrintTemporaryCertificate").Visible
            = false;
        }
        else
        {
          Parent.FindControl("FinishNavigationTemplateContainerID").FindControl("btnPrintTemporaryCertificate").Visible
            = true;
        }
      }
    }

    /// <summary>
    ///   The copy date filling from step 1.
    /// </summary>
    public void CopyDateFillingFromStep1()
    {
      if (CurrentStatement.DateFiling.HasValue && string.IsNullOrEmpty(tbTemporaryCertificateDateIssue.Text))
      {
        tbTemporaryCertificateDateIssue.Text = CurrentStatement.DateFiling.Value.ToShortDateString();
      }

      if (string.IsNullOrEmpty(tbPolicyNumber.Text) && !string.IsNullOrEmpty(CurrentStatement.NumberPolicy))
      {
        tbEnpNumber.Text = CurrentStatement.NumberPolicy; // PoliceNumber;
      }
    }

    /// <summary>
    /// The move data from gui 2 object.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="setCurrentStatement">
    /// The set current statement.
    /// </param>
    public override void MoveDataFromGui2Object(ref Statement statement, bool setCurrentStatement = true)
    {
      if (statement.MedicalInsurances == null)
      {
        statement.MedicalInsurances = new List<MedicalInsurance>(2);
      }

      foreach (var medicalInsurance in statement.MedicalInsurances)
      {
        medicalInsurance.IsActive = false;
      }

      // Выдача ВС
      if (statement.AbsentPrevPolicy.HasValue && statement.AbsentPrevPolicy.Value)
      {
        var temp = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.В);
        if (temp == null)
        {
          temp = new MedicalInsurance();
          statement.MedicalInsurances.Add(temp);
        }

        temp.IsActive = true;
        temp.PolisType = ObjectFactory.GetInstance<IStatementService>().GetConcept(PolisType.В);
        temp.PolisSeria = string.Empty;
        temp.PolisNumber = tbTemporaryCertificateNumber.Text;
        temp.Statement = statement;
        temp.StateDateTo = new DateTime(2200, 1, 1);
        temp.StateDateFrom = DateTime.Now;

        // Дата выдачи ВС
        DateTime dateTime;
        temp.DateFrom = !string.IsNullOrEmpty(tbTemporaryCertificateDateIssue.Text)
                        && DateTime.TryParse(tbTemporaryCertificateDateIssue.Text, out dateTime)
                          ? dateTime
                          : new DateTime(1900, 1, 1);

        temp.DateTo = statementService.CalculateEndPeriodWorkingDay(temp.DateFrom, 30);
        temp.DateStop = PolisEndDate != new DateTime(2200, 1, 1) ? (DateTime?)PolisEndDate : null;
      }

      // Выдача полиса
      statement.PolicyIsIssued = chbPolicyIsIssued.Checked;
      if (statement.PolicyIsIssued.Value)
      {
        var polis = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.В);
        if (polis == null)
        {
          polis = new MedicalInsurance();
          statement.MedicalInsurances.Add(polis);
        }

        polis.IsActive = true;
        var policyType = int.Parse(ddlPolicyType.SelectedValue);
        polis.PolisType = ObjectFactory.GetInstance<IStatementService>().GetConcept(policyType);
        polis.PolisSeria = string.Empty;
        polis.PolisNumber = tbPolicyNumber.Text;
        polis.Enp = tbEnpNumber.Text;
        polis.Statement = statement;
        polis.StateDateTo = new DateTime(2200, 1, 1);
        polis.StateDateFrom = DateTime.Now;

        // Дата выдачи ВС
        DateTime dateTime;
        polis.DateFrom = !string.IsNullOrEmpty(tbPolicyDateIssue.Text)
                         && DateTime.TryParse(tbPolicyDateIssue.Text, out dateTime)
                           ? dateTime
                           : new DateTime(2011, 1, 1);

        // Дата окончания действия
        polis.DateTo = !string.IsNullOrEmpty(tbPolicyDateEnd.Text)
                       && DateTime.TryParse(tbPolicyDateEnd.Text, out dateTime)
                         ? dateTime
                         : new DateTime(2200, 1, 1);
        polis.DateStop = null;
      }

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
      if (statement == null)
      {
        return;
      }

      if (statement.MedicalInsurances == null)
      {
        statement.MedicalInsurances = new List<MedicalInsurance>();
      }

      var temp = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.В);
      if (temp != null)
      {
        // Номер ВС
        tbTemporaryCertificateNumber.Text = temp.PolisNumber;

        // Дата выдачи ВС
        tbTemporaryCertificateDateIssue.Text = temp.DateFrom.ToShortDateString();
      }

      // Выдан полис
      if (statement.PolicyIsIssued != null)
      {
        chbPolicyIsIssued.Checked = (bool)statement.PolicyIsIssued;
      }

      if (statement.MedicalInsurances == null)
      {
        return;
      }

      // Получаем полис
      var policy = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.В);

      // Текущим указываем запрошенный тип полиса
      var polisType = PolisType.П.ToString(CultureInfo.InvariantCulture);
      if (policy != null)
      {
        // Тип полиса
        ddlPolicyType.SelectedValue = polisType = policy.PolisType.Id.ToString(CultureInfo.InvariantCulture);

        // Номер бланка 
        tbPolicyNumber.Text = policy.PolisNumber;

        // Номер ЕНП
        tbEnpNumber.Text = policy.Enp;

        // Дата выдачи полиса на руки
        tbPolicyDateIssue.Text = policy.DateFrom.ToShortDateString();

        // Дата окончания действия полиса
        tbPolicyDateEnd.Text = policy.DateTo.ToShortDateString();
      }
      else
      {
        if (statement.FormManufacturing != null)
        {
          ddlPolicyType.SelectedValue =
            polisType = statement.FormManufacturing.Id.ToString(CultureInfo.InvariantCulture);
        }
      }

      hfRequestedPolicyType.Value = polisType;
    }

    /// <summary>
    ///   Установка фокуса на контрол при смене шага
    /// </summary>
    public override void SetDefaultFocus()
    {
      if (tbTemporaryCertificateNumber.Visible)
      {
        tbTemporaryCertificateNumber.Focus();
      }
      else
      {
        if (tbPolicyNumber.Visible)
        {
          tbPolicyNumber.Focus();
        }
      }
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
      ddlPolicyType.Items.AddRange(
                                   statementService.GetFormManufacturingByCauseFilling(-1)
                                                   .Select(
                                                           x =>
                                                           new ListItem(
                                                             x.Name,
                                                             x.Id.ToString(CultureInfo.InvariantCulture)))
                                                   .ToArray());
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
      statementService = ObjectFactory.GetInstance<IStatementService>();
      CheckIsRightToEdit();

      if (!IsPostBack)
      {
        return;
      }

      if (CurrentStatement.FormManufacturing == null)
      {
        return;
      }

      // Пытаемся получить редактируемое заявление, и проверить наличие уже имеющегося полиса
      ////var _statementService = ObjectFactory.GetInstance<IStatementService>();
      ////var tempStatementId = Guid.Empty;

      // Guid.TryParse(StatementId, out tempStatementId);
      MedicalInsurance polis = null;
      if (CurrentStatement.Id != Guid.Empty /* tempStatementId != null*/)
      {
        // var statement = _statementService.GetStatement(tempStatementId);
        if (CurrentStatement != null && CurrentStatement.MedicalInsurances != null)
        {
          polis = CurrentStatement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.В);
        }
      }

      // В случае если полис заказан в составе УЭК, то пользователь может осуществить только операцию
      // выдачи бумажного полиса
      string polisType;
      if (CurrentStatement.FormManufacturing.Id == PolisType.К && chbPolicyIsIssued.Checked && polis == null)
      {
        ddlPolicyType.SelectedValue = polisType = PolisType.П.ToString(CultureInfo.InvariantCulture);
      }
      else
      {
        if (polis != null)
        {
          ddlPolicyType.SelectedValue = polisType = polis.PolisType.Id.ToString(CultureInfo.InvariantCulture);
        }
        else
        {
          ddlPolicyType.SelectedValue =
            polisType = CurrentStatement.FormManufacturing.Id.ToString(CultureInfo.InvariantCulture);
        }
      }

      hfRequestedPolicyType.Value = polisType;

      // Дата окончания действия полиса
      tbPolicyDateEnd.Text = PolisEndDate.ToShortDateString();

      if (IsPostBack)
      {
        var st = CurrentStatement;
        MoveDataFromGui2Object(ref st);
      }
    }

    /// <summary>
    /// The validate enp number.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidateEnpNumber(object source, ServerValidateEventArgs args)
    {
      try
      {
        statementService.CheckPropertyStatement(
                                                CurrentStatement,
                                                Utils.GetExpressionNode(x => x.MedicalInsurances[1].Enp));
      }
      catch (LogicalControlException ex)
      {
        args.IsValid = false;
        cvEnpNumber.Text = ex.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate policy date issue.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidatePolicyDateIssue(object source, ServerValidateEventArgs args)
    {
      try
      {
        statementService.CheckPropertyStatement(
                                                CurrentStatement,
                                                Utils.GetExpressionNode(x => x.MedicalInsurances[1].DateFrom));
      }
      catch (LogicalControlException ex)
      {
        args.IsValid = false;
        cvPolicyDateIssue.Text = ex.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate policy number.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidatePolicyNumber(object source, ServerValidateEventArgs args)
    {
      try
      {
        statementService.CheckPropertyStatement(
                                                CurrentStatement,
                                                Utils.GetExpressionNode(x => x.MedicalInsurances[1].PolisNumber));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvPolicyNumber.Text = e.GetAllMessages();
      }
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
      try
      {
        statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.FormManufacturing.Id));
      }
      catch (LogicalControlException ex)
      {
        args.IsValid = false;
        cvPolicyType.Text = ex.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate temporary certificate date issue.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidateTemporaryCertificateDateIssue(object source, ServerValidateEventArgs args)
    {
      try
      {
        statementService.CheckPropertyStatement(
                                                CurrentStatement,
                                                Utils.GetExpressionNode(x => x.MedicalInsurances[0].DateFrom));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvTemporaryCertificateDateIssue.Text = e.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate temporary certificate number.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    protected void ValidateTemporaryCertificateNumber(object source, ServerValidateEventArgs args)
    {
      try
      {
        statementService.CheckPropertyStatement(
                                                CurrentStatement,
                                                Utils.GetExpressionNode(x => x.MedicalInsurances[0].PolisNumber));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvTemporaryCertificateNumber.Text = e.GetAllMessages();
      }
    }

    #endregion
  }
}