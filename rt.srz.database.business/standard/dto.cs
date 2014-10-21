// --------------------------------------------------------------------------------------------------------------------
// <copyright file="dto.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The field variation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard
{
  /// <summary>
  ///   The field variation.
  /// </summary>
  public class FieldVariation
  {
    #region Fields

    /// <summary>
    ///   The field type field.
    /// </summary>
    private FieldTypes fieldTypeField;

    /// <summary>
    ///   The value field.
    /// </summary>
    private string valueField;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the field type.
    /// </summary>
    public FieldTypes fieldType
    {
      get
      {
        return fieldTypeField;
      }

      set
      {
        if (fieldTypeField.Equals(value) != true)
        {
          fieldTypeField = value;
        }
      }
    }

    /// <summary>
    ///   Gets or sets the value.
    /// </summary>
    public string value
    {
      get
      {
        return valueField;
      }

      set
      {
        if (ReferenceEquals(valueField, value) != true)
        {
          valueField = value;
        }
      }
    }

    #endregion
  }

  /// <summary>
  ///   The field types.
  /// </summary>
  public enum FieldTypes
  {
    /// <summary>
    ///   The event insurance.
    /// </summary>
    EventInsurance = 0,

    /// <summary>
    ///   The policy type.
    /// </summary>
    PolicyType = 1,

    /// <summary>
    ///   The enp.
    /// </summary>
    ENP = 2,

    /// <summary>
    ///   The event insurance date.
    /// </summary>
    EventInsuranceDate = 3,

    /// <summary>
    ///   The policy number.
    /// </summary>
    PolicyNumber = 4,

    /// <summary>
    ///   The family name.
    /// </summary>
    FamilyName = 5,

    /// <summary>
    ///   The first name.
    /// </summary>
    FirstName = 6,

    /// <summary>
    ///   The middle name.
    /// </summary>
    MiddleName = 7,

    /// <summary>
    ///   The gender.
    /// </summary>
    Gender = 8,

    /// <summary>
    ///   The birth date.
    /// </summary>
    BirthDate = 9,

    /// <summary>
    ///   The birth place.
    /// </summary>
    BirthPlace = 10,

    /// <summary>
    ///   The id card type.
    /// </summary>
    IdCardType = 11,

    /// <summary>
    ///   The id card number.
    /// </summary>
    IdCardNumber = 12,

    /// <summary>
    ///   The id card date.
    /// </summary>
    IdCardDate = 13,

    /// <summary>
    ///   The snils.
    /// </summary>
    SNILS = 14,

    /// <summary>
    ///   The insurance territory.
    /// </summary>
    InsuranceTerritory = 15,

    /// <summary>
    ///   The insurance company code.
    /// </summary>
    InsuranceCompanyCode = 16,

    /// <summary>
    ///   The policy actual from.
    /// </summary>
    PolicyActualFrom = 17,

    /// <summary>
    ///   The policy actual to.
    /// </summary>
    PolicyActualTo = 18,

    /// <summary>
    ///   The citizenry_ oks m_3.
    /// </summary>
    Citizenry_OKSM_3 = 19,

    /// <summary>
    ///   The death mark.
    /// </summary>
    DeathMark = 20,

    /// <summary>
    ///   The death date.
    /// </summary>
    DeathDate = 21,

    /// <summary>
    ///   The registration territory.
    /// </summary>
    RegistrationTerritory = 22,

    /// <summary>
    ///   The organization type.
    /// </summary>
    OrganizationType = 23,

    /// <summary>
    ///   The pvp id.
    /// </summary>
    PvpId = 24,

    /// <summary>
    ///   The insurance temp number.
    /// </summary>
    InsuranceTempNumber = 25,

    /// <summary>
    ///   The region.
    /// </summary>
    Region = 1000,

    /// <summary>
    ///   The insurance card id.
    /// </summary>
    InsuranceCardID = 1003,

    /// <summary>
    ///   The insurance company coding.
    /// </summary>
    InsuranceCompanyCoding = 1005,

    /// <summary>
    ///   The citizenry.
    /// </summary>
    Citizenry = 1010,

    /// <summary>
    ///   The citizenry_ oki n_2.
    /// </summary>
    Citizenry_OKIN_2 = 1011,

    /// <summary>
    ///   The id card type_ id card number.
    /// </summary>
    IdCardType_IdCardNumber = -101,

    /// <summary>
    ///   The policy type_ policy number.
    /// </summary>
    PolicyType_PolicyNumber = -102,

    /// <summary>
    ///   The insurance company code_ insurance company coding.
    /// </summary>
    InsuranceCompanyCode_InsuranceCompanyCoding = -103,

    /// <summary>
    ///   The undefined.
    /// </summary>
    Undefined = -10000,
  }

  /// <summary>
  ///   The fio.
  /// </summary>
  public class FIO
  {
    #region Fields

    /// <summary>
    ///   The family name field.
    /// </summary>
    private FieldVariation familyNameField;

    /// <summary>
    ///   The first name field.
    /// </summary>
    private FieldVariation firstNameField;

    /// <summary>
    ///   The middle name field.
    /// </summary>
    private FieldVariation middleNameField;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the family name.
    /// </summary>
    public FieldVariation familyName
    {
      get
      {
        return familyNameField;
      }

      set
      {
        if (ReferenceEquals(familyNameField, value) != true)
        {
          familyNameField = value;
        }
      }
    }

    /// <summary>
    ///   Gets or sets the first name.
    /// </summary>
    public FieldVariation firstName
    {
      get
      {
        return firstNameField;
      }

      set
      {
        if (ReferenceEquals(firstNameField, value) != true)
        {
          firstNameField = value;
        }
      }
    }

    /// <summary>
    ///   Gets or sets the middle name.
    /// </summary>
    public FieldVariation middleName
    {
      get
      {
        return middleNameField;
      }

      set
      {
        if (ReferenceEquals(middleNameField, value) != true)
        {
          middleNameField = value;
        }
      }
    }

    #endregion
  }

  /// <summary>
  ///   The document.
  /// </summary>
  public class Document
  {
    #region Fields

    /// <summary>
    ///   The id card number field.
    /// </summary>
    private FieldVariation idCardNumberField;

    /// <summary>
    ///   The id card type field.
    /// </summary>
    private FieldVariation idCardTypeField;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the id card number.
    /// </summary>
    public FieldVariation idCardNumber
    {
      get
      {
        return idCardNumberField;
      }

      set
      {
        if (ReferenceEquals(idCardNumberField, value) != true)
        {
          idCardNumberField = value;
        }
      }
    }

    /// <summary>
    ///   Gets or sets the id card type.
    /// </summary>
    public FieldVariation idCardType
    {
      get
      {
        return idCardTypeField;
      }

      set
      {
        if (ReferenceEquals(idCardTypeField, value) != true)
        {
          idCardTypeField = value;
        }
      }
    }

    #endregion
  }

  /// <summary>
  ///   The hash data.
  /// </summary>
  public class HashData
  {
    #region Fields

    /// <summary>
    ///   The hash field.
    /// </summary>
    private byte[] hashField;

    /// <summary>
    ///   The key field.
    /// </summary>
    private string keyField;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the hash.
    /// </summary>
    public byte[] hash
    {
      get
      {
        return hashField;
      }

      set
      {
        if (ReferenceEquals(hashField, value) != true)
        {
          hashField = value;
        }
      }
    }

    /// <summary>
    ///   Gets or sets the key.
    /// </summary>
    public string key
    {
      get
      {
        return keyField;
      }

      set
      {
        if (ReferenceEquals(keyField, value) != true)
        {
          keyField = value;
        }
      }
    }

    #endregion
  }

  /// <summary>
  ///   The hash data new.
  /// </summary>
  public class HashDataNew
  {
    #region Fields

    /// <summary>
    ///   The hash.
    /// </summary>
    public byte[] hash;

    /// <summary>
    ///   The key.
    /// </summary>
    public string key;

    /// <summary>
    ///   The subtype.
    /// </summary>
    public int subtype;

    /// <summary>
    ///   The type.
    /// </summary>
    public int type;

    #endregion

    /// <summary>
    ///   The hash.
    /// </summary>
    public byte[] Hash
    {
      get
      {
        return hash;
      }
      set
      {
        hash = value;
      }
    }
  }

  /// <summary>
  ///   The key.
  /// </summary>
  public class Key
  {
    #region Fields

    /// <summary>
    ///   The formula.
    /// </summary>
    public FieldTypes[] formula;

    /// <summary>
    ///   The number.
    /// </summary>
    public string number;

    #endregion
  }
}