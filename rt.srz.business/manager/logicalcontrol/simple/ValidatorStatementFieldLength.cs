using NHibernate;
using rt.srz.business.Properties;
using rt.srz.model.logicalcontrol.exceptions;
using rt.srz.model.srz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.srz.business.manager.logicalcontrol.simple
{
  public class ValidatorStatementFieldLength : Check
  {

    #region Public Properties

    /// <summary>
    /// Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorStatementFieldLength;
      }
    }

    #endregion

    public ValidatorStatementFieldLength(ISessionFactory sessionFactory) : base (CheckLevelEnum.Simple, sessionFactory)
    {
    }

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

    private string GetDisplayFieldName(string displayFieldName, string suffix)
    {
      return string.Format("{0} {1}", displayFieldName, suffix);
    }

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

  }
}
