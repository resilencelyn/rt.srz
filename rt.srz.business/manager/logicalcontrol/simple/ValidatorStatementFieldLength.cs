// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorStatementFieldLength.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator statement field length.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.srz;

  /// <summary>
  /// The validator statement field length.
  /// </summary>
  public class ValidatorStatementFieldLength : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorStatementFieldLength"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorStatementFieldLength(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorStatementFieldLength;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public override void CheckObject(Statement statement)
    {
      Validate(statement.NumberPolicy, 35, "Номер ранее выданного полиса");

      Validate(statement.NumberTemporaryCertificate, 50, "Номер временного свидетельства");
      Validate(statement.NumberPolisCertificate, 50, "Номер бланка выданного полиса");

      if (statement.InsuredPersonData != null)
      {
        Validate(statement.InsuredPersonData.FirstName, 50, "Имя");
        Validate(statement.InsuredPersonData.MiddleName, 50, "Отчество");
        Validate(statement.InsuredPersonData.LastName, 50, "Фамилия");
        Validate(statement.InsuredPersonData.Birthday2, 10, "Дата рождения строкой");
        Validate(statement.InsuredPersonData.Birthplace, 500, "Место рождения");
      }

      if (statement.DocumentUdl != null)
      {
        Validate(statement.DocumentUdl.IssuingAuthority, 500, "Орган выдавший документ удл");
        Validate(statement.DocumentUdl.Number, 20, "Номер документа удл");
        Validate(statement.DocumentUdl.Series, 20, "Серия документа удл");
      }

      if (statement.DocumentRegistration != null)
      {
        Validate(statement.DocumentRegistration.IssuingAuthority, 500, "Орган выдавший документ регистрации");
        Validate(statement.DocumentRegistration.Number, 20, "Номер документа регистрации");
        Validate(statement.DocumentRegistration.Series, 20, "Серия документа регистрации");
      }

      if (statement.ResidencyDocument != null)
      {
        Validate(statement.ResidencyDocument.IssuingAuthority, 500, "Орган выдавший документ представителя");
        Validate(statement.ResidencyDocument.Number, 20, "Номер документа представителя");
        Validate(statement.ResidencyDocument.Series, 20, "Серия документа представителя");
      }

      if (statement.ContactInfo != null)
      {
        Validate(statement.ContactInfo.Email, 50, "Электронная почта");
        Validate(statement.ContactInfo.HomePhone, 50, "Домашний телефон");
        Validate(statement.ContactInfo.WorkPhone, 50, "Рабочий телефон");
      }

      if (statement.Representative != null)
      {
        Validate(statement.Representative.HomePhone, 40, "Домашний телефон представителя");
        Validate(statement.Representative.WorkPhone, 40, "Рабочий телефон представителя");
        Validate(statement.Representative.FirstName, 50, "Имя представителя");
        Validate(statement.Representative.MiddleName, 50, "Отчество представителя");
        Validate(statement.Representative.LastName, 50, "Фамилия представителя");
      }

      Validate(statement.Address, "адреса регистрации");
      Validate(statement.Address2, "адреса проживания");
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get display field name.
    /// </summary>
    /// <param name="displayFieldName">
    /// The display field name.
    /// </param>
    /// <param name="suffix">
    /// The suffix.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetDisplayFieldName(string displayFieldName, string suffix)
    {
      return string.Format("{0} {1}", displayFieldName, suffix);
    }

    /// <summary>
    /// The validate.
    /// </summary>
    /// <param name="address">
    /// The address.
    /// </param>
    /// <param name="suffix">
    /// The suffix.
    /// </param>
    private void Validate(address address, string suffix)
    {
      if (address == null)
      {
        return;
      }

      Validate(address.Postcode, 10, GetDisplayFieldName("Индекс", suffix));
      Validate(address.House, 20, GetDisplayFieldName("Дом", suffix));
      Validate(address.Housing, 20, GetDisplayFieldName("Корпус", suffix));
      Validate(address.Unstructured, 500, GetDisplayFieldName("Строка", suffix));
      Validate(address.Okato, 20, GetDisplayFieldName("ОКАТО", suffix));
      Validate(address.Subject, 50, GetDisplayFieldName("Регион", suffix));
      Validate(address.Area, 50, GetDisplayFieldName("Район", suffix));
      Validate(address.City, 50, GetDisplayFieldName("Город", suffix));
      Validate(address.Town, 50, GetDisplayFieldName("Населённый пункт", suffix));
      Validate(address.Street, 50, GetDisplayFieldName("Улица", suffix));
    }

    /// <summary>
    /// The validate.
    /// </summary>
    /// <param name="fieldValue">
    /// The field value.
    /// </param>
    /// <param name="fieldLengthInDatabase">
    /// The field length in database.
    /// </param>
    /// <param name="displayFieldName">
    /// The display field name.
    /// </param>
    /// <exception cref="FieldLengthException">
    /// </exception>
    private void Validate(string fieldValue, int fieldLengthInDatabase, string displayFieldName)
    {
      if (string.IsNullOrEmpty(fieldValue))
      {
        return;
      }

      if (fieldValue.Length > fieldLengthInDatabase)
      {
        throw new FieldLengthException(displayFieldName);
      }
    }

    #endregion
  }
}