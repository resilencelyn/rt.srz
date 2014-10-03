// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Step3.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps
{
  #region

  using System;
  using System.Globalization;
  using System.Linq;
  using System.Web.UI.WebControls;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;
  using rt.srz.services;
  using rt.srz.ui.pvp.Enumerations;
  using StructureMap;

  #endregion

  /// <summary>
  ///   The step 3.
  /// </summary>
  public partial class Step3 : WizardStep
  {
    #region Fields

    /// <summary>
    ///   The _kladr service.
    /// </summary>
    private IKladrService kladrService;

    /// <summary>
    ///   The _statement service.
    /// </summary>
    private IStatementService _statementService;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Проверка доступности элементов редактирования для ввода
    /// </summary>
    public override void CheckIsRightToEdit()
    {
      var propertyList = GetPropertyListForCheckIsRightToEdit();

      // Без определенного места жительства, Ввод в свободной форме, адрес региcтрации
      var enable = _statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.Address));
      chBIsHomeless.Enabled = chbIsFreeMainAddress.Enabled = tbDateRegistration.Enabled = enable;
      mainAddressKladr.Enable(enable);
      mainAddressKladrIntellisense.Enable(enable);

      // Документ подтверждающий регистрацию
      //var isRightToEdit = _statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.DocumentRegistration));
      //documentRegistration.Enable(!chBCopyFromUDL.Checked && isRightToEdit);
      //chBCopyFromUDL.Enabled = isRightToEdit;

      // Адрес проживания
      enable = _statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.Address2));
      rbYesResAddress.Enabled = rbNoResAddress.Enabled = chbIsFreeResidencyAddress.Enabled = enable;
      residencyAddressKladr.Enable(enable);
      residencyAddressKladrIntellisense.Enable(enable);

      // Контактная информация
      tbHomePhone.Enabled =
        //tbWorkPhone.Enabled =
        tbEmail.Enabled = _statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.ContactInfo));
    }

    /// <summary>
    ///   Переносит данные для документа регистрации со второго шага
    /// </summary>
    public void CopyUdlDataFromStep2()
    {
      //chBCopyFromUDL.Checked = true;
      CopyFromUdlCheckedChanged(null, null);
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
      var mainAdress = statement.Address ?? new address();

      // Адрес в свободной форме
      mainAdress.IsHomeless = chBIsHomeless.Checked;
      mainAdress.IsNotStructureAddress = chbIsFreeMainAddress.Checked;

      if (GetKLADRControlType() == KLADRControlType.Structured || chbIsFreeMainAddress.Checked)
      {
        MoveDataFromGui2Object(mainAddressKladr, mainAdress);
      }
      else
      {
        MoveDataFromGui2Object(mainAddressKladrIntellisense, mainAdress);
      }

      // Дата регистрации
      if (!string.IsNullOrEmpty(tbDateRegistration.Text))
      {
        DateTime dt;
        mainAdress.DateRegistration = DateTime.TryParse(tbDateRegistration.Text, out dt) ? (DateTime?)dt : null;
      }

      statement.Address = mainAdress;


      //if (chBCopyFromUDL.Checked)
      //{
      //  if (statement.DocumentUdl != null)
      //  {
      //    statement.DocumentRegistration = statement.DocumentUdl;
      //  }
      //}
      //else
      //{
      //  Document document;
      //  if (statement.DocumentRegistration != null && statement.DocumentUdl != null
      //      && statement.DocumentRegistration.Id != statement.DocumentUdl.Id)
      //  {
      //    document = statement.DocumentRegistration;
      //  }
      //  else
      //  {
      //    document = new Document();
      //  }

      //  // Вид документа
      //  if (documentRegistration.DocumentType >= 0)
      //  {
      //    document.DocumentType = _statementService.GetConcept(documentRegistration.DocumentType);
      //  }

      //  // Серия
      //  document.Series = documentRegistration.DocumentSeries;

      //  // Номер
      //  document.Number = documentRegistration.DocumentNumber;

      //  // Выдавший орган
      //  document.IssuingAuthority = documentRegistration.DocumentIssuingAuthority;

      //  // Дата выдачи
      //  document.DateIssue = documentRegistration.DocumentIssueDate;
      //  document.DateExp = documentRegistration.DocumentExpDate;

      //  // Признак валидности документа по дублю
      //  document.ExistDocument = documentRegistration.ValidDocument;

      //  // Присвоени ссылки
      //  statement.DocumentRegistration = document;
      //}

      var contactInfo = statement.ContactInfo ?? new ContactInfo();

      // Телефон домашний
      contactInfo.HomePhone = tbHomePhone.Text;

      // Телефон рабочий
      contactInfo.WorkPhone = null;//tbWorkPhone.Text;

      // Электронная почта
      contactInfo.Email = tbEmail.Text;

      statement.ContactInfo = contactInfo;

      if (rbYesResAddress.Checked)
      {
        var resAddress = statement.Address2 ?? new address();

        // Адрес в свободной форме
        resAddress.IsNotStructureAddress = chbIsFreeResidencyAddress.Checked;

        if (GetKLADRControlType() == KLADRControlType.Structured || chbIsFreeResidencyAddress.Checked)
        {
          MoveDataFromGui2Object(residencyAddressKladr, resAddress);
        }
        else
        {
          MoveDataFromGui2Object(residencyAddressKladrIntellisense, resAddress);
        }

        statement.Address2 = resAddress;
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
      if (statement.Address != null)
      {
        // Лицо без определенного места жительства
        if (statement.Address.IsHomeless != null)
        {
          chBIsHomeless.Checked = (bool)statement.Address.IsHomeless;
        }

        // адрес в свободной форме
        if (statement.Address.IsNotStructureAddress != null)
        {
          chbIsFreeMainAddress.Checked = (bool)statement.Address.IsNotStructureAddress;
          mainAddressKladr.Mode = (bool)statement.Address.IsNotStructureAddress
                                    ? KladrUserControlMode.Free
                                    : KladrUserControlMode.Database;
        }

        // switch (GetKLADRControlType())
        // {
        // case KLADRControlType.Structured:
        // {
        MoveDataFromObject2GUI(statement.Address, mainAddressKladr);

        // }
        // break;
        // case KLADRControlType.Intellisense:
        // {
        MoveDataFromObject2GUI(statement.Address, mainAddressKladrIntellisense);

        // }
        // break;
        // }

        // Дата регистрации
        tbDateRegistration.Text = statement.Address.DateRegistration == null
                                    ? string.Empty
                                    : statement.Address.DateRegistration.Value.ToShortDateString();
      }

      //// Проверка на совпадение документов
      //chBCopyFromUDL.Checked = statement.DocumentUdl == null || statement.DocumentRegistration == null
      //                         || statement.DocumentUdl.Id == statement.DocumentRegistration.Id;

      //if (statement.DocumentRegistration != null)
      //{
      //  // Вид документа
      //  if (statement.DocumentRegistration.DocumentType != null)
      //  {
      //    var documentType = statement.DocumentRegistration.DocumentType;
      //    documentRegistration.FillDocumentTypeDdl(
      //      new[]
      //        {
      //          new ListItem
      //            {
      //               Text = documentType.Name, Value = documentType.Id.ToString(CultureInfo.InvariantCulture) 
      //            } 
      //        },
      //      documentType.Id.ToString(CultureInfo.InvariantCulture));
      //    documentRegistration.DocumentType = statement.DocumentRegistration.DocumentType.Id;
      //  }

      //  // Серия
      //  documentRegistration.DocumentSeries = statement.DocumentRegistration.Series;

      //  // Номер
      //  documentRegistration.DocumentNumber = statement.DocumentRegistration.Number;

      //  // Выдавший орган
      //  documentRegistration.DocumentIssuingAuthority = statement.DocumentRegistration.IssuingAuthority;

      //  // Дата выдачи
      //  documentRegistration.DocumentIssueDate = statement.DocumentRegistration.DateIssue;
      //  documentRegistration.DocumentExpDate = statement.DocumentRegistration.DateExp;


      //}

      if (statement.ContactInfo != null)
      {
        // Телефон домашний
        tbHomePhone.Text = statement.ContactInfo.HomePhone;

        // Телефон рабочий
        //tbWorkPhone.Text = statement.ContactInfo.WorkPhone;

        // Электронная почта
        tbEmail.Text = statement.ContactInfo.Email;
      }

      if (statement.Address2 != null)
      {
        // адрес в свободной форме
        if (statement.Address2.IsNotStructureAddress != null)
        {
          chbIsFreeResidencyAddress.Checked = (bool)statement.Address2.IsNotStructureAddress;

          // mainAddressKladr.Mode = (bool)statement.Address.IsNotStructureAddress ?
          // KLADRUserControlMode.Free : KLADRUserControlMode.Database;
        }

        //switch (GetKLADRControlType())
        //{
        //  case KLADRControlType.Structured:
        //    {
        MoveDataFromObject2GUI(statement.Address2, residencyAddressKladr);
        //    }

        //    break;
        //  case KLADRControlType.Intellisense:
        //    {
        MoveDataFromObject2GUI(statement.Address2, residencyAddressKladrIntellisense);
        //    }

        //    break;
        //}

        rbYesResAddress.Checked = true;
        rbNoResAddress.Checked = false;
      }

      RestoreAfterPostBack();
    }

    /// <summary>
    ///   Установка фокуса на контрол при смене шага
    /// </summary>
    public override void SetDefaultFocus()
    {
      switch (GetKLADRControlType())
      {
        case KLADRControlType.Structured:
          mainAddressKladr.SetFocusFirstControl();
          break;
        case KLADRControlType.Intellisense:
          mainAddressKladrIntellisense.SetFocusFirstControl();
          break;
      }
    }

    /// <summary>
    ///   The validate.
    /// </summary>
    /// <returns> The <see cref="bool" /> . </returns>
    public bool Validate()
    {
      var args = new ServerValidateEventArgs(string.Empty, true);

      ValidateRegistrationSubject(cvRegistrationSubject, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateRegistrationPostcode(cvEmail, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateDocument(cvDocument, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateResidencySubject(cvResidencySubject, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateResidencyPostcode(cvEmail, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateEmail(cvEmail, args);
      if (!args.IsValid)
      {
        return false;
      }

      return true;
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
      _statementService = ObjectFactory.GetInstance<IStatementService>();
      kladrService = ObjectFactory.GetInstance<IKladrService>();

      if (!IsPostBack)
      {
        // Установка типа адресного компонента
        hfKLADRControlType.Value = GetKLADRControlType().ToString();
        switch (GetKLADRControlType())
        {
          case KLADRControlType.Structured:
            {
              chbIsFreeMainAddress.Visible = false;
              chbIsFreeResidencyAddress.Visible = false;
              mainAddressKladrIntellisenseDiv.Style.Add("display", "none");
              mainAddressKladrDiv.Style.Remove("display");
            }

            break;
          case KLADRControlType.Intellisense:
            {
              // Адрес регистрации
              mainAddressKladrIntellisenseDiv.Style.Remove("display");
              mainAddressKladrDiv.Style.Add("display", "none");

              mainAddressKladr.Mode = KladrUserControlMode.Free;
              residencyAddressKladr.Mode = KladrUserControlMode.Free;
            }

            break;
        }

        //documentRegistration.FillDocumentTypeDdl(
        //  _statementService.GetDocumentTypeForRegistrationDocument().Select(
        //    x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray(),
        //  null);

        // Сокрытие адреса проживания
        residencyAddressLabelDiv.Style.Add("display", "none");
        isFreeResidencyAdressDiv.Style.Add("display", "none");
        residencyAddressKladrIntellisenseDiv.Style.Add("display", "none");
        residencyAddressKladrDiv.Style.Add("display", "none");
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
      if (IsPostBack)
      {
        RestoreAfterPostBack();
        var st = CurrentStatement;
        MoveDataFromGui2Object(ref st);
      }
    }

    /// <summary>
    /// The validate document.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateDocument(object sender, ServerValidateEventArgs args)
    {
      // Если указан признак бомж, валидацию не проводим
      if (chBIsHomeless.Checked)
      {
        args.IsValid = true;
        return;
      }


      args.IsValid = true;
      //// Валидация
      //try
      //{
      //  _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.DocumentRegistration));
      //}
      //catch (LogicalControlException exception)
      //{
      //  args.IsValid = documentRegistration.cvDocument.IsValid = false;
      //  documentRegistration.cvDocument.Text = exception.GetAllMessages();
      //}
    }

    /// <summary>
    /// The validate email.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateEmail(object source, ServerValidateEventArgs args)
    {
      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.ContactInfo.Email));
    }

    /// <summary>
    /// The validate registration postcode.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateRegistrationPostcode(object source, ServerValidateEventArgs args)
    {
      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.Address.Postcode));

      switch (GetKLADRControlType())
      {
        case KLADRControlType.Intellisense:
          {
            if (chbIsFreeMainAddress.Checked)
            {
              mainAddressKladr.cvPostcode.IsValid = args.IsValid;
            }
            else
            {
              mainAddressKladrIntellisense.cvPostcode.IsValid = args.IsValid;
            }
          }

          break;
        case KLADRControlType.Structured:
          mainAddressKladr.cvPostcode.IsValid = args.IsValid;
          break;
      }
    }

    /// <summary>
    /// The validate registration subject.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateRegistrationSubject(object source, ServerValidateEventArgs args)
    {
      // Если указан признак бомж, валидацию не проводим
      if (chBIsHomeless.Checked)
      {
        args.IsValid = true;
        return;
      }

      // Валидация
      var messages = string.Empty;
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.Address));
      }
      catch (LogicalControlException exception)
      {
        messages = exception.GetAllMessages();
      }

      // Возврат, если все в порядке
      if (string.IsNullOrEmpty(messages))
      {
        return;
      }

      // Вывод сообщения
      switch (GetKLADRControlType())
      {
        case KLADRControlType.Intellisense:
          if (chbIsFreeMainAddress.Checked)
          {
            args.IsValid = mainAddressKladr.cvSubject.IsValid = false;
            mainAddressKladr.cvSubject.Text = messages;
          }
          else
          {
            args.IsValid = mainAddressKladrIntellisense.cvSubject.IsValid = false;
            mainAddressKladrIntellisense.cvSubject.Text = messages;
          }

          break;
        case KLADRControlType.Structured:
          args.IsValid = mainAddressKladr.cvSubject.IsValid = false;
          mainAddressKladr.cvSubject.Text = messages;
          break;
      }
    }

    /// <summary>
    /// The validate residency postcode.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateResidencyPostcode(object source, ServerValidateEventArgs args)
    {
      // Если указан признак отсутствия адреса проживания, проверку не производим
      if (rbNoResAddress.Checked)
      {
        args.IsValid = true;
        return;
      }

      args.IsValid = _statementService.TryCheckProperty(CurrentStatement, Utils.GetExpressionNode(x => x.Address2.Postcode));

      switch (GetKLADRControlType())
      {
        case KLADRControlType.Intellisense:
          {
            if (chbIsFreeResidencyAddress.Checked)
            {
              residencyAddressKladr.cvPostcode.IsValid = args.IsValid;
            }
            else
            {
              residencyAddressKladrIntellisense.cvPostcode.IsValid = args.IsValid;
            }
          }

          break;
        case KLADRControlType.Structured:
          residencyAddressKladr.cvPostcode.IsValid = args.IsValid;
          break;
      }
    }

    /// <summary>
    /// The validate residency subject.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateResidencySubject(object source, ServerValidateEventArgs args)
    {
      // Если указан признак отсутствия адреса проживания, проверку не производим
      if (rbNoResAddress.Checked)
      {
        args.IsValid = true;
        return;
      }

      // Валидация
      var messages = string.Empty;
      try
      {
        _statementService.CheckPropertyStatement(CurrentStatement, Utils.GetExpressionNode(x => x.Address2));
      }
      catch (LogicalControlException exception)
      {
        messages = exception.GetAllMessages();
      }

      // Возврат, если все в порядке
      if (string.IsNullOrEmpty(messages))
      {
        return;
      }

      // Вывод сообщения
      switch (GetKLADRControlType())
      {
        case KLADRControlType.Structured:
          args.IsValid = residencyAddressKladr.cvSubject.IsValid = false;
          residencyAddressKladr.cvSubject.Text = messages;
          break;
        case KLADRControlType.Intellisense:
          if (chbIsFreeResidencyAddress.Checked)
          {
            args.IsValid = residencyAddressKladr.cvSubject.IsValid = false;
            residencyAddressKladr.cvSubject.Text = messages;
          }
          else
          {
            args.IsValid = residencyAddressKladrIntellisense.cvSubject.IsValid = false;
            residencyAddressKladrIntellisense.cvSubject.Text = messages;
          }

          break;
      }
    }

    /// <summary>
    /// The ch b copy from ud l_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void CopyFromUdlCheckedChanged(object sender, EventArgs e)
    {
      //documentRegistration.FillDocumentTypeDdl(
      //    _statementService.GetNsiRecords(Oid.ДокументУдл).Select(
      //      x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray(), null);

      //if (chBCopyFromUDL.Checked)
      //{
      //  // Перенос данных со второго шага
      //  documentRegistration.DocumentType = CurrentStatement.DocumentUdl.DocumentType.Id; //DocumentUdlType;
      //  documentRegistration.DocumentSeries = CurrentStatement.DocumentUdl.Series;//DocumentUdlSeries;
      //  documentRegistration.DocumentNumber = CurrentStatement.DocumentUdl.Number;//DocumentUdlNumber;
      //  documentRegistration.DocumentIssuingAuthority = CurrentStatement.DocumentUdl.IssuingAuthority;//DocumentUdlIssuingAuthority;
      //  documentRegistration.DocumentIssueDate = CurrentStatement.DocumentUdl.DateIssue;//DocumentUdlIssueDate;
      //  documentRegistration.DocumentExpDate = CurrentStatement.DocumentUdl.DateExp;//DocumentUdlExpDate;
      //  documentRegistration.Enable(false);
      //}
      //else
      //{
      //  documentRegistration.Enable(true);
      //  documentRegistration.EnableDocumentType(false);
      //  documentRegistration.DocumentType = DocumentType.CertificationRegistration;
      //}
      //if (chBCopyFromUDL.Enabled && chBCopyFromUDL.Visible)
      //{
      //  chBCopyFromUDL.Focus();
      //}
    }

    /// <summary>
    ///   The get kladr control type.
    /// </summary>
    /// <returns> The <see cref="KLADRControlType" /> . </returns>
    private KLADRControlType GetKLADRControlType()
    {
      // Получение типа адресного компонента
      var configValue = _statementService.GetSettingCurrentUser("KLADRControlType");
      var value = (KLADRControlType)Enum.Parse(typeof(KLADRControlType), configValue);
      return value;
    }

    /// <summary>
    /// The move data from gui 2 object.
    /// </summary>
    /// <param name="control">
    /// The control. 
    /// </param>
    /// <param name="address">
    /// The address. 
    /// </param>
    private void MoveDataFromGui2Object(KladrUserControl control, address address)
    {
      var selectedKladrid = control.SelectedKLADRId;
      if (selectedKladrid != Guid.Empty)
      {
        var kladr = kladrService.GetKLADR(selectedKladrid);

        // ОКАТО выбранного уровня
        address.Okato = kladr.Ocatd;

        // Ссылка на КЛАДР
        address.Kladr = kladr;
      }

      // Индекс
      address.Postcode = control.tbPostcode.Text;

      // Регион
      address.Subject = control.Subject;

      // Район
      address.Area = control.Area;

      // Город
      address.City = control.City;

      // Населенный пункт
      address.Town = control.Town;

      // Улица
      address.Street = control.Street;

      // Номер дома
      address.House = control.tbHouse.Text;

      // Корпус
      address.Housing = control.tbHousing.Text;

      // Квартира
      if (!string.IsNullOrEmpty(control.tbRoom.Text))
      {
        address.Room = short.Parse(control.tbRoom.Text);
      }
    }

    /// <summary>
    /// The move data from gu i 2 object.
    /// </summary>
    /// <param name="control">
    /// The control. 
    /// </param>
    /// <param name="address">
    /// The address. 
    /// </param>
    private void MoveDataFromGui2Object(KladrIntellisenseUserControl control, address address)
    {
      // Лицо без определенного места жительства
      address.IsHomeless = chBIsHomeless.Checked;

      // Индекс
      address.Postcode = control.tbPostcode.Text;

      // Регион
      var selectedKladrid = control.SelectedKLADRId;
      if (selectedKladrid != Guid.Empty)
      {
        var kladr = kladrService.GetKLADR(selectedKladrid);

        // ОКАТО выбранного уровня
        address.Okato = kladr.Ocatd;

        // Ссылка на КЛАДР
        address.Kladr = kladr;

        do
        {
          selectedKladrid = kladr.KLADRPARENT != null ? kladr.KLADRPARENT.Id : Guid.Empty;

          var strTemp = kladr.Name + " " + kladr.Socr;
          switch (kladr.Level)
          {
            case (int)KLADRLevel.Subject:
              {
                address.Subject = strTemp;
              }

              break;
            case (int)KLADRLevel.Area:
              {
                address.Area = strTemp;
              }

              break;
            case (int)KLADRLevel.City:
              {
                address.City = strTemp;
              }

              break;
            case (int)KLADRLevel.Town:
              {
                address.Town = strTemp;
              }

              break;
            case (int)KLADRLevel.Street:
              {
                address.Street = strTemp;
              }

              break;
          }

          kladr = kladrService.GetKLADR(selectedKladrid);
        }
        while (selectedKladrid != Guid.Empty);
      }

      // Номер дома
      address.House = control.tbHouse.Text;

      // Корпус
      address.Housing = control.tbHousing.Text;

      // Квартира
      if (!string.IsNullOrEmpty(control.tbRoom.Text))
      {
        address.Room = short.Parse(control.tbRoom.Text);
      }
    }

    /// <summary>
    /// The move data from object 2 gui.
    /// </summary>
    /// <param name="address">
    /// The address. 
    /// </param>
    /// <param name="control">
    /// The control. 
    /// </param>
    private void MoveDataFromObject2GUI(address address, KladrUserControl control)
    {
      // Регион
      control.Subject = address.Subject;

      // Район
      control.Area = address.Area;

      // Город
      control.City = address.City;

      // Населенный пункт
      control.Town = address.Town;

      // Улица
      control.Street = address.Street;

      // при установке полей выше затирается значение индекса, поэтому оно здесь
      // Индекс
      control.tbPostcode.Text = address.Postcode;

      // Номер дома
      control.tbHouse.Text = address.House;

      // Корпус
      control.tbHousing.Text = address.Housing;

      // Квартира
      control.tbRoom.Text = address.Room == null ? string.Empty : address.Room.Value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// The move data from object 2 gui.
    /// </summary>
    /// <param name="address">
    /// The address. 
    /// </param>
    /// <param name="control">
    /// The control. 
    /// </param>
    private void MoveDataFromObject2GUI(address address, KladrIntellisenseUserControl control)
    {
      // Адрес
      if (address.Kladr != null && address.Kladr.Id != Guid.Empty)
      {
        control.SelectedKLADRId = address.Kladr.Id;
      }

      // Индекс
      control.tbPostcode.Text = address.Postcode;

      // Номер дома
      control.tbHouse.Text = address.House;

      // Корпус
      control.tbHousing.Text = address.Housing;

      // Квартира
      control.tbRoom.Text = address.Room == null ? string.Empty : address.Room.Value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   The restore after post back.
    /// </summary>
    private void RestoreAfterPostBack()
    {
      if (chBIsHomeless.Checked)
      {
        isFreeMainAddressDiv.Style.Add("display", "none");
        mainAddressKladrIntellisenseDiv.Style.Add("display", "none");
        mainAddressKladrDiv.Style.Add("display", "none");
        dateRegistrationDiv.Style.Add("display", "none");
        //registrationDocumentDiv.Style.Add("display", "none");
      }
      else
      {
        isFreeMainAddressDiv.Style.Add("display", "block");
        if (GetKLADRControlType() == KLADRControlType.Structured || chbIsFreeMainAddress.Checked)
        {
          mainAddressKladrDiv.Style.Add("display", "block");
          mainAddressKladrIntellisenseDiv.Style.Add("display", "none");
        }
        else
        {
          mainAddressKladrDiv.Style.Add("display", "none");
          mainAddressKladrIntellisenseDiv.Style.Add("display", "block");
        }

        dateRegistrationDiv.Style.Add("display", "block");
        //registrationDocumentDiv.Style.Add("display", "block");
      }

      if (rbYesResAddress.Checked)
      {
        residencyAddressLabelDiv.Style.Add("display", "block");
        isFreeResidencyAdressDiv.Style.Add("display", "block");
        if (GetKLADRControlType() == KLADRControlType.Structured || chbIsFreeResidencyAddress.Checked)
        {
          residencyAddressKladrDiv.Style.Add("display", "block");
          residencyAddressKladrIntellisenseDiv.Style.Add("display", "none");
        }
        else
        {
          residencyAddressKladrDiv.Style.Add("display", "none");
          residencyAddressKladrIntellisenseDiv.Style.Add("display", "block");
        }
      }
      else
      {
        residencyAddressLabelDiv.Style.Add("display", "none");
        isFreeResidencyAdressDiv.Style.Add("display", "none");
        residencyAddressKladrDiv.Style.Add("display", "none");
        residencyAddressKladrIntellisenseDiv.Style.Add("display", "none");
      }

      //if (documentRegistration.DocumentTypeInHf != -1)
      //{
      //  documentRegistration.DocumentType = documentRegistration.DocumentTypeInHf;
      //}
    }

    #endregion
  }
}