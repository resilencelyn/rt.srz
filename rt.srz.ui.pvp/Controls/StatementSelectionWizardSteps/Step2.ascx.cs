// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Step2.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Web.UI.WebControls;

  using rt.srz.model.algorithms;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;
  using rt.srz.services;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The step 2.
  /// </summary>
  public partial class Step2 : WizardStep
  {
    #region Fields

    /// <summary>
    ///   The _statement service.
    /// </summary>
    private IStatementService _statementService;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Дата рождения
    /// </summary>
    public DateTime? BirthDate
    {
      get
      {
        DateTime dateTime;
        if (!string.IsNullOrEmpty(tbBirthDate.Text) && DateTime.TryParse(tbBirthDate.Text, out dateTime))
        {
          return dateTime;
        }

        if (!string.IsNullOrEmpty(tbBirthMonth.Text) && DateTime.TryParse(tbBirthMonth.Text, out dateTime))
        {
          return dateTime;
        }

        if (!string.IsNullOrEmpty(tbBirthYear.Text) && DateTime.TryParse("01.01." + tbBirthYear.Text, out dateTime))
        {
          return dateTime;
        }

        return null;
      }
    }

    /// <summary>
    ///   Тип даты рождения
    /// </summary>
    public BirthdayType BirthdayType
    {
      get
      {
        try
        {
          if (!string.IsNullOrEmpty(tbBirthDate.Text))
          {
            return BirthdayType.Full;
          }

          if (!string.IsNullOrEmpty(tbBirthMonth.Text))
          {
            return BirthdayType.MonthAndYear;
          }

          if (!string.IsNullOrEmpty(tbBirthYear.Text))
          {
            return BirthdayType.Year;
          }
        }
        catch
        {
          return BirthdayType.Unknown;
        }

        return BirthdayType.Unknown;
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

      // ФИО
      if (!_statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.LastName)))
      {
        tbLastName.Enabled = chbIsLastNameAbsent.Enabled = false;
      }

      if (!_statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.FirstName)))
      {
        tbFirstName.Enabled = chbIsFirstNameAbsent.Enabled = false;
      }

      if (!_statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.MiddleName)))
      {
        tbMiddleName.Enabled = chbIsMiddleNameAbsent.Enabled = false;
      }

      // Пол
      ddlGender.Enabled = _statementService.IsRightToEdit(
        propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.Gender));

      // Дата рождения
      chBIsIncorrectDate.Enabled = _statementService.IsRightToEdit(
        propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.IsIncorrectDate));
      if (!_statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.BirthdayType)))
      {
        rbBirthDate.Enabled = rbBirthMonth.Enabled = rbBirthYear.Enabled = false;
      }

      if (!_statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.Birthday)))
      {
        tbBirthDate.Enabled = tbBirthMonth.Enabled = tbBirthYear.Enabled = false;
      }

      // Место рождения
      ddlBirthPlace.Enabled = _statementService.IsRightToEdit(
        propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.OldCountry));
      tbBirthPlace.Enabled = _statementService.IsRightToEdit(
        propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.Birthplace));

      // Категория
      ddlCategory.Enabled = _statementService.IsRightToEdit(
        propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.Category));
      chBIsNotGuru.Enabled = _statementService.IsRightToEdit(
        propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.IsNotGuru));

      // СНИЛС
      tbSnils.Enabled = _statementService.IsRightToEdit(
        propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.Snils));

      // Гражданство
      ddlCitizenship.Enabled = _statementService.IsRightToEdit(
        propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.Citizenship));
      if (
        !_statementService.IsRightToEdit(
          propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.IsNotCitizenship)))
      {
        chbWithoutCitizenship.Enabled = false;
      }

      if (!_statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.IsRefugee)))
      {
        chbIsRefugee.Enabled = false;
      }

      // Документ УДЛ
      documentUDL.Enable(_statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.DocumentUdl)));

      // Документ подтвержающий право проживания
      documentResidency.Enable(
        _statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.DocumentRegistration)));
    }

    /// <summary>
    /// Переносит данные из элементов на форме в объект
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// </summary>
    /// <param name="setCurrentStatement">
    /// Обновлять ли свойство CurrentStatement после присвоения заявлению данных из дизайна 
    /// </param>
    public override void MoveDataFromGui2Object(ref Statement statement, bool setCurrentStatement = true)
    {
      var insuredPersonData = statement.InsuredPersonData ?? new InsuredPersonDatum();

      // Фамилия
      insuredPersonData.LastName = tbLastName.Text;

      // Имя
      insuredPersonData.FirstName = tbFirstName.Text;

      // Отчество
      insuredPersonData.MiddleName = tbMiddleName.Text;

      // Пол
      var gender = int.Parse(ddlGender.SelectedValue);
      if (gender >= 0)
      {
        insuredPersonData.Gender = _statementService.GetConcept(gender);
      }

      // Несуществующая дата рождения в документе УДЛ
      insuredPersonData.IsIncorrectDate = chBIsIncorrectDate.Checked;

      // Тип даты рождения
      insuredPersonData.BirthdayType = (int?)BirthdayType;

      // Дата рождения
      if (BirthDate.HasValue)
      {
        insuredPersonData.Birthday = BirthDate.Value;
      }

      // Место рождения
      var birthPlaceCountry = int.Parse(ddlBirthPlace.SelectedValue);
      if (birthPlaceCountry >= 0)
      {
        insuredPersonData.OldCountry = _statementService.GetConcept(birthPlaceCountry);
      }

      insuredPersonData.Birthplace = tbBirthPlace.Text;

      // Категория
      var category = int.Parse(ddlCategory.SelectedValue);
      if (category >= 0)
      {
        insuredPersonData.Category = _statementService.GetConcept(category);
      }

      // Не специалист...
      insuredPersonData.IsNotGuru = chBIsNotGuru.Checked;

      // СНИЛС
      var ss = SnilsChecker.SsToShort(tbSnils.Text.Replace("_", string.Empty)).Trim();
      insuredPersonData.Snils = !string.IsNullOrEmpty(ss) ? ss : null;
      insuredPersonData.NotCheckSnils = chbNotCheckDigitsSnils.Checked;
      insuredPersonData.NotCheckExistsSnils = chbNotCheckExistsSnils.Checked;

      // Гражданство
      var citizenship = int.Parse(ddlCitizenship.SelectedValue);
      if (citizenship >= 0)
      {
        insuredPersonData.Citizenship = _statementService.GetConcept(citizenship);
      }
      else
      {
        insuredPersonData.Citizenship = null;
      }

      // Без гражданства
      insuredPersonData.IsNotCitizenship = chbWithoutCitizenship.Checked;

      // Беженец
      insuredPersonData.IsRefugee = chbIsRefugee.Checked;

      statement.InsuredPersonData = insuredPersonData;

      var document = statement.DocumentUdl ?? new Document();

      // Вид документа УДЛ
      if (documentUDL.DocumentType >= 0)
      {
        document.DocumentType = _statementService.GetConcept(documentUDL.DocumentType);
      }

      // Серия
      document.Series = documentUDL.DocumentSeries;

      // Номер
      document.Number = documentUDL.DocumentNumber;

      // Номер
      document.IssuingAuthority = documentUDL.DocumentIssuingAuthority;

      // Дата выдачи
      document.DateIssue = documentUDL.DocumentIssueDate;
      document.DateExp = documentUDL.DocumentExpDate;

      // Признак валидности документа по дублю
      document.ExistDocument = documentUDL.ValidDocument;

      statement.DocumentUdl = document;

      // Документ подтвержающий право проживания
      if (statement.InsuredPersonData.Category != null && CategoryPerson.IsDocumentResidency(statement.InsuredPersonData.Category.Id) && documentResidency.DocumentTypeInHf >= 0)
      {
        var residencyDocument = statement.ResidencyDocument ?? new Document();

        residencyDocument.DocumentType = _statementService.GetConcept(documentResidency.DocumentTypeInHf);


        // Серия
        residencyDocument.Series = documentResidency.DocumentSeries;

        // Номер
        residencyDocument.Number = documentResidency.DocumentNumber;

        // Номер
        residencyDocument.IssuingAuthority = documentResidency.DocumentIssuingAuthority;

        // Дата выдачи
        residencyDocument.DateIssue = documentResidency.DocumentIssueDate;
        residencyDocument.DateExp = documentResidency.DocumentExpDate;

        // Признак валидности документа по дублю
        residencyDocument.ExistDocument = documentResidency.ValidDocument;

        statement.ResidencyDocument = residencyDocument;
      }
      else
      {
        statement.ResidencyDocument = null;
      }

      _statementService.TrimStatementData(statement);

      //сохранение изменений в сессию
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
      if (statement.InsuredPersonData != null)
      {
        var insuredPersonData = statement.InsuredPersonData;

        // Фамилия
        tbLastName.Text = insuredPersonData.LastName;
        if (string.IsNullOrEmpty(tbLastName.Text) && statement.Id != Guid.Empty)
        {
          chbIsLastNameAbsent.Checked = true;
          ChbIsLastNameAbsentCheckedChanged(null, null);
        }

        // Имя
        tbFirstName.Text = insuredPersonData.FirstName;
        if (string.IsNullOrEmpty(tbFirstName.Text) && statement.Id != Guid.Empty)
        {
          chbIsFirstNameAbsent.Checked = true;
          ChbIsFirstNameAbsentCheckedChanged(null, null);
        }

        // Отчество
        tbMiddleName.Text = insuredPersonData.MiddleName;
        if (string.IsNullOrEmpty(tbMiddleName.Text) && statement.Id != Guid.Empty)
        {
          chbIsMiddleNameAbsent.Checked = true;
          ChbIsMiddleNameAbsentCheckedChanged(null, null);
        }

        // Пол
        if (insuredPersonData.Gender != null)
        {
          ddlGender.SelectedValue = insuredPersonData.Gender.Id.ToString(CultureInfo.InvariantCulture);
        }

        // Несуществующая дата рождения в документе УДЛ
        if (insuredPersonData.IsIncorrectDate != null)
        {
          chBIsIncorrectDate.Checked = (bool)insuredPersonData.IsIncorrectDate;
        }

        // Дата рождения
        switch (insuredPersonData.BirthdayType)
        {
          case (int)BirthdayType.Full:
            {
              if (insuredPersonData.Birthday != null)
              {
                tbBirthDate.Text = insuredPersonData.Birthday.Value.ToShortDateString();
              }

              rbBirthDate.Checked = true;
              rbBirthMonth.Checked = false;
              rbBirthYear.Checked = false;
              RbBirthDateCheckedChanged(null, null);
            }

            break;
          case (int)BirthdayType.MonthAndYear:
            {
              if (insuredPersonData.Birthday != null)
              {
                tbBirthMonth.Text = insuredPersonData.Birthday.Value.ToString("MMMM yyyy");
              }

              rbBirthDate.Checked = false;
              rbBirthMonth.Checked = true;
              rbBirthYear.Checked = false;
              RbBirthMonthCheckedChanged(null, null);
            }

            break;
          case (int)BirthdayType.Year:
            {
              if (insuredPersonData.Birthday != null)
              {
                tbBirthYear.Text = insuredPersonData.Birthday.Value.ToString("yyyy");
              }

              rbBirthDate.Checked = false;
              rbBirthMonth.Checked = false;
              rbBirthYear.Checked = true;
              RbBirthYearCheckedChanged(null, null);
            }

            break;
        }

        // Место рождения
        if (insuredPersonData.OldCountry != null)
        {
          ddlBirthPlace.SelectedValue = insuredPersonData.OldCountry.Id.ToString(CultureInfo.InvariantCulture);
        }

        tbBirthPlace.Text = insuredPersonData.Birthplace;

        // Не специалист...
        if (!insuredPersonData.IsNotGuru != null)
        {
          chBIsNotGuru.Checked = (bool)insuredPersonData.IsNotGuru;
        }

        // СНИЛС
        tbSnils.Text = insuredPersonData.Snils;

        // Проверяем корректность снилса
        chbNotCheckDigitsSnils.Checked = chbNotCheckDigitsSnils.Visible = !string.IsNullOrEmpty(insuredPersonData.Snils) && !SnilsChecker.CheckIdentifier(insuredPersonData.Snils);


        // Без гражданства
        chbWithoutCitizenship.Checked = insuredPersonData.IsNotCitizenship;
        ChbWithoutCitizenshipCheckedChanged(null, null);

        // Беженец
        chbIsRefugee.Checked = insuredPersonData.IsRefugee;
        ChbIsRefugeeCheckedChanged(null, null);

        // Гражданство
        if (insuredPersonData.Citizenship != null)
        {
          ddlCitizenship.SelectedValue = insuredPersonData.Citizenship.Id.ToString(CultureInfo.InvariantCulture);
        }

        // Категория
        UpdateCategory();
        if (insuredPersonData.Category != null)
        {
          ddlCategory.SelectedValue = insuredPersonData.Category.Id.ToString(CultureInfo.InvariantCulture);
          hfSelectedCategory.Value = ddlCategory.SelectedValue;

          SetVisibleDocumentResidency(CategoryPerson.IsDocumentResidency(insuredPersonData.Category.Id));
        }
        else
        {
          SetVisibleDocumentResidency(false);
        }
      }

      UpdateDocTypeUdl();
      if (statement.DocumentUdl != null)
      {
        // Вид документа
        if (statement.DocumentUdl.DocumentType != null)
        {
          documentUDL.DocumentType = statement.DocumentUdl.DocumentType.Id;
        }

        // Серия
        documentUDL.DocumentSeries = statement.DocumentUdl.Series;

        // Номер
        documentUDL.DocumentNumber = statement.DocumentUdl.Number;

        // Орган
        documentUDL.DocumentIssuingAuthority = statement.DocumentUdl.IssuingAuthority;

        // Дата выдачи
        documentUDL.DocumentIssueDate = statement.DocumentUdl.DateIssue;
        documentUDL.DocumentExpDate = statement.DocumentUdl.DateExp;
      }

      // Документ подтвержающий право проживания
      UpdateDocTypeResidency();
      if (statement.ResidencyDocument != null)
      {
        // Вид документа
        if (statement.ResidencyDocument.DocumentType != null)
        {
          documentResidency.DocumentType = statement.ResidencyDocument.DocumentType.Id;
        }

        // Серия
        documentResidency.DocumentSeries = statement.ResidencyDocument.Series;

        // Номер
        documentResidency.DocumentNumber = statement.ResidencyDocument.Number;

        // Орган
        documentResidency.DocumentIssuingAuthority = statement.ResidencyDocument.IssuingAuthority;

        // Дата выдачи
        documentResidency.DocumentIssueDate = statement.ResidencyDocument.DateIssue;
        documentResidency.DocumentExpDate = statement.ResidencyDocument.DateExp;
      }

    #endregion
    }

    /// <summary>
    ///   Установка фокуса на контрол при смене шага
    /// </summary>
    public override void SetDefaultFocus()
    {
      tbLastName.Focus();
    }

    /// <summary>
    ///   The validate.
    /// </summary>
    /// <returns> The <see cref="bool" /> . </returns>
    public bool Validate()
    {
      var args = new ServerValidateEventArgs(string.Empty, true);

      ValidateLastName(cvLastName, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateFirstName(cvFirstName, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateMiddleName(cvMiddleName, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateBirthDate(cvBirthDate, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateBirthPlace(cvBirthPlace, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateGender(cvGender, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateCategory(cvCategory, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateCitizenship(cvCitizenship, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateSnils(tbSnils, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateDocument(cvDocument, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateDocumentResinedcy(cvDocumentResidecy, args);
      if (!args.IsValid)
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// The set snils error.
    /// </summary>
    public void SetSnilsExistError()
    {
      chbNotCheckExistsSnils.Visible = true;
    }

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
      _statementService = ObjectFactory.GetInstance<IStatementService>();
      if (!IsPostBack)
      {
        FillGender();
        FillCategory();
        FillDocType();
        FillCitizenshipAndBirthPlace();
        UpdateCategory();
        UpdateDocTypeUdl();
        UpdateDocTypeResidency();
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
      if (!IsPostBack)
      {
        return;
      }

      // ФИО
      ChbIsLastNameAbsentCheckedChanged(null, null);
      ChbIsFirstNameAbsentCheckedChanged(null, null);
      ChbIsMiddleNameAbsentCheckedChanged(null, null);

      // Дата рождения
      if (rbBirthDate.Checked)
      {
        RbBirthDateCheckedChanged(null, null);
      }

      // Дата рождения c точностью до месяца
      if (rbBirthMonth.Checked)
      {
        RbBirthMonthCheckedChanged(null, null);
      }

      // Дата рождения c точностью до года
      if (rbBirthYear.Checked)
      {
        RbBirthYearCheckedChanged(null, null);
      }

      // Без гражданства
      chbWithoutCitizenship.Enabled = !chbIsRefugee.Checked;
      ddlCitizenship.Enabled = !chbWithoutCitizenship.Checked;

      // Беженец
      chbIsRefugee.Enabled = !chbWithoutCitizenship.Checked && ddlCitizenship.SelectedValue != Country.RUS.ToString(CultureInfo.InvariantCulture); ;

      // Гражданство

      // Категория
      UpdateCategory();
      if (!string.IsNullOrEmpty(hfSelectedCategory.Value) && ddlCategory.Items.FindByValue(hfSelectedCategory.Value) != null)
      {
        ddlCategory.SelectedValue = hfSelectedCategory.Value;
        SetVisibleDocumentResidency(CategoryPerson.IsDocumentResidency(int.Parse(ddlCategory.SelectedValue)));
      }
      else
      {
        SetVisibleDocumentResidency(false);
      }

      // Документ
      UpdateDocTypeUdl();
      if (documentUDL.DocumentTypeInHf != -1)
      {
        documentUDL.DocumentType = documentUDL.DocumentTypeInHf;
      }

      // Документ
      UpdateDocTypeResidency();
      //if (documentResidency.DocumentTypeInHf != -1)
      //{
      documentResidency.DocumentType = documentResidency.DocumentTypeInHf;
      //}

      if (IsPostBack)
      {
        var st = CurrentStatement;
        MoveDataFromGui2Object(ref st);
      }
    }

    /// <summary>
    /// The validate birth and issue date.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateBirthAndIssueDate(object source, ServerValidateEventArgs args)
    {
      var errorsText = _statementService.TryCheckProperty1(CurrentStatement, Utils.GetExpressionNode(x => x.DocumentUdl.DateIssue));
      cvBirthAndIssueDate.Text = errorsText;
      args.IsValid = string.IsNullOrEmpty(errorsText);
    }

    /// <summary>
    /// The validate birth date.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateBirthDate(object source, ServerValidateEventArgs args)
    {
      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.Birthday));
    }

    /// <summary>
    /// The validate birth place.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateBirthPlace(object source, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.Birthplace));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvBirthPlace.Text = e.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate category.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateCategory(object source, ServerValidateEventArgs args)
    {
      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.Category.Id));
    }

    /// <summary>
    /// The validate citizenship.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateCitizenship(object source, ServerValidateEventArgs args)
    {
      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.Citizenship.Id));
    }

    /// <summary>
    /// The validate document.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void ValidateDocument(object sender, ServerValidateEventArgs e)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.DocumentUdl));
      }
      catch (LogicalControlException exception)
      {
        e.IsValid = false;
        cvDocument.Text = exception.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate document resinedcy.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void ValidateDocumentResinedcy(object sender, ServerValidateEventArgs e)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.ResidencyDocument));
      }
      catch (LogicalControlException exception)
      {
        e.IsValid = false;
        cvDocumentResidecy.Text = exception.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate first name.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateFirstName(object source, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.FirstName));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvFirstName.Text = e.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate gender.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateGender(object source, ServerValidateEventArgs args)
    {
      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.Gender.Id));
    }

    /// <summary>
    /// The validate gender conformity.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateGenderConformity(object source, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.Gender.Id));
        args.IsValid = true;
      }
      catch (LogicalControlException ex)
      {
        cvGenderConformity.Text = ex.GetAllMessages();
        args.IsValid = false;
      }
    }

    /// <summary>
    /// The validate last name.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateLastName(object sender, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.LastName));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvLastName.Text = e.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate middle name.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateMiddleName(object sender, ServerValidateEventArgs args)
    {
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.MiddleName));
      }
      catch (LogicalControlException e)
      {
        args.IsValid = false;
        cvMiddleName.Text = e.GetAllMessages();
      }
    }

    /// <summary>
    /// The validate snils.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateSnils(object source, ServerValidateEventArgs args)
    {
      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.Snils));
      chbNotCheckDigitsSnils.Visible = !args.IsValid;
    }

    /// <summary>
    /// The chb is first name absent_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void ChbIsFirstNameAbsentCheckedChanged(object sender, EventArgs e)
    {
      tbFirstName.Enabled = !chbIsFirstNameAbsent.Checked;
      if (chbIsFirstNameAbsent.Checked)
      {
        tbFirstName.Text = string.Empty;
      }
    }

    /// <summary>
    /// The chb is last name absent_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void ChbIsLastNameAbsentCheckedChanged(object sender, EventArgs e)
    {
      tbLastName.Enabled = !chbIsLastNameAbsent.Checked;
      if (chbIsLastNameAbsent.Checked)
      {
        tbLastName.Text = string.Empty;
      }
    }

    /// <summary>
    /// The chb is middle name absent_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void ChbIsMiddleNameAbsentCheckedChanged(object sender, EventArgs e)
    {
      tbMiddleName.Enabled = !chbIsMiddleNameAbsent.Checked;
      if (chbIsMiddleNameAbsent.Checked)
      {
        tbMiddleName.Text = string.Empty;
      }
    }

    /// <summary>
    /// The chb is refugee_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void ChbIsRefugeeCheckedChanged(object sender, EventArgs e)
    {
      chbWithoutCitizenship.Enabled = !chbIsRefugee.Checked;
    }

    /// <summary>
    /// The chb without citizenship_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void ChbWithoutCitizenshipCheckedChanged(object sender, EventArgs e)
    {
      ddlCitizenship.SelectedIndex = -1;
      ddlCitizenship.Enabled = !chbWithoutCitizenship.Checked;
      //chbIsRefugee.Enabled = !chbWithoutCitizenship.Checked;

      if (!chbWithoutCitizenship.Checked)
      {
        ddlCitizenship.SelectedValue = Country.RUS.ToString(CultureInfo.InvariantCulture);
        chbIsRefugee.Enabled = false;
      }
    }

    /// <summary>
    /// The rb birth date_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void RbBirthDateCheckedChanged(object sender, EventArgs e)
    {
      tbBirthDate.Enabled = rbBirthDate.Checked;
      if (rbBirthDate.Checked)
      {
        tbBirthMonth.Enabled = false;
        tbBirthYear.Enabled = false;
        tbBirthMonth.Text = tbBirthYear.Text = string.Empty;
      }
    }

    /// <summary>
    /// The rb birth month_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void RbBirthMonthCheckedChanged(object sender, EventArgs e)
    {
      tbBirthMonth.Enabled = rbBirthMonth.Checked;
      if (rbBirthMonth.Checked)
      {
        tbBirthDate.Enabled = false;
        tbBirthYear.Enabled = false;
        tbBirthDate.Text = tbBirthYear.Text = string.Empty;
      }
    }

    /// <summary>
    /// The rb birth year_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void RbBirthYearCheckedChanged(object sender, EventArgs e)
    {
      tbBirthYear.Enabled = rbBirthYear.Checked;
      if (rbBirthYear.Checked)
      {
        tbBirthDate.Enabled = false;
        tbBirthMonth.Enabled = false;
        tbBirthDate.Text = tbBirthMonth.Text = string.Empty;
      }
    }

    /// <summary>
    ///   The fill category.
    /// </summary>
    private void FillCategory()
    {
      ddlCategory.Items.AddRange(
        _statementService.GetNsiRecords(Oid.Категориязастрахованноголица).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
    }

    /// <summary>
    ///   The fill citizenship and birth place.
    /// </summary>
    private void FillCitizenshipAndBirthPlace()
    {
      ddlCitizenship.Items
        .AddRange(_statementService
          .GetNsiRecords(Oid.Страна)
          .Select(x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture)))
          .ToArray());

      ddlBirthPlace.Items
        .AddRange(_statementService
        .GetNsiRecords(new[] { Oid.Страна, Oid.Странадляместарождения })
        .OrderBy(x => x.Relevance).ThenBy(x => x.Name)
        .Select(x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture)))
        .ToArray());

      ddlCitizenship.SelectedValue = Country.RUS.ToString(CultureInfo.InvariantCulture);
      ddlBirthPlace.SelectedValue = Country.RUS.ToString(CultureInfo.InvariantCulture);
      chbIsRefugee.Enabled = false;
    }

    /// <summary>
    ///   The fill doc type.
    /// </summary>
    private void FillDocType()
    {
      documentUDL.FillDocumentTypeDdl(
        _statementService.GetNsiRecords(Oid.ДокументУдл).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray(),
        DocumentType.PassportRf.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    ///   The fill gender.
    /// </summary>
    private void FillGender()
    {
      ddlGender.Items.AddRange(
        _statementService.GetNsiRecords(Oid.Пол).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
    }

    /// <summary>
    /// The set visible document residency.
    /// </summary>
    /// <param name="visible">
    /// The visible.
    /// </param>
    private void SetVisibleDocumentResidency(bool visible)
    {
      divDocumentResidency.Style.Value = visible ? "display: block" : "display: none";
    }

    /// <summary>
    ///   The update category.
    /// </summary>
    private void UpdateCategory()
    {
      // Заполнение списка категорий
      ddlCategory.Items.Clear();
      ddlCategory.Items.Add(new ListItem("Выберите категорию", "-1"));
      var citizenshipId = -1;
      if (ddlCitizenship.SelectedValue != "-1")
      {
        citizenshipId = int.Parse(ddlCitizenship.SelectedValue);
      }

      var itemList = _statementService
        .GetCategoryByCitizenship(citizenshipId, chbWithoutCitizenship.Checked, chbIsRefugee.Checked, Age)
        .Select(x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture)))
        .ToArray();
      ddlCategory.Items.AddRange(itemList);
      if (itemList.Length >= 1)
      {
        ddlCategory.SelectedValue = itemList[0].Value;
      }
    }

    /// <summary>
    ///   The update doc type.
    /// </summary>
    private void UpdateDocTypeResidency()
    {
      // Заполнение списка документов
      var documentList = new List<ListItem>();
      if (ddlCategory.SelectedValue != "-1")
      {
        var categoryId = int.Parse(ddlCategory.SelectedValue);
        documentList.AddRange(
          _statementService.GetDocumentResidencyTypeByCategory(categoryId).Select(
            x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
      }

      // Значение по умолчанию
      string defValue = null;
      if (documentList.Count > 0)
      {
        defValue = documentList[0].Value;
      }

      documentList.Insert(0, new ListItem("Выберите вид документа", "-1"));
      documentResidency.FillDocumentTypeDdl(documentList.ToArray(), defValue);
      documentResidency.UpdateDocumentType();
    }

    /// <summary>
    ///   The update doc type.
    /// </summary>
    private void UpdateDocTypeUdl()
    {
      // Заполнение списка документов
      var documentList = new List<ListItem>();
      if (ddlCategory.SelectedValue != "-1")
      {
        var categoryId = int.Parse(ddlCategory.SelectedValue);

        documentList.AddRange(
          _statementService.GetDocumentTypeByCategory(categoryId, Age).Select(
            x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
      }

      // Значение по умолчанию
      string defValue = null;
      if (documentList.Count > 0)
      {
        defValue = documentList[0].Value;
      }

      documentList.Insert(0, new ListItem("Выберите вид документа", "-1"));
      documentUDL.FillDocumentTypeDdl(documentList.ToArray(), defValue);
      documentUDL.UpdateDocumentType();
    }

    /// <summary>
    /// Gets the age.
    /// </summary>
    private TimeSpan Age
    {
      get
      {
        // 1 год = 365.242199 суток * 15 лет = 5113,390786 суток
        var age = new TimeSpan(5114, 0, 0, 0);
        if (BirthDate.HasValue)
        {
          age = DateTime.Now - BirthDate.Value;
        }
        return age;
      }
    }

    #endregion
  }
}