// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldTypes.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The field types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Fields
{
  /// <summary>
  /// The field types.
  /// </summary>
  public enum FieldTypes
  {
    // ------------------------------------------------------------------------------------------------

    /// <summary>
    /// The policy type.
    /// </summary>
    PolicyType = 0, // тип полиса

    /// <summary>
    /// The enp.
    /// </summary>
    ENP = 1, // единый номер полиса ОМС

    /// <summary>
    /// The sid.
    /// </summary>
    SID = 2, // системный идентификатор застрахованого лица        

    /// <summary>
    /// The policy number.
    /// </summary>
    PolicyNumber = 3, // серия и номер выданного полиса ОМС

    /// <summary>
    /// The family name.
    /// </summary>
    FamilyName = 4, // фамилия застрахованного лица

    /// <summary>
    /// The first name.
    /// </summary>
    FirstName = 5, // имя застрахованного лица

    /// <summary>
    /// The middle name.
    /// </summary>
    MiddleName = 6, // отчество застрахованного лица

    /// <summary>
    /// The gender.
    /// </summary>
    Gender = 7, // пол застрахованного лица

    /// <summary>
    /// The birth date.
    /// </summary>
    BirthDate = 8, // дата рождения застрахованного лица

    /// <summary>
    /// The birth place.
    /// </summary>
    BirthPlace = 9, // место рождения застрахованного лица

    /// <summary>
    /// The id card type.
    /// </summary>
    IdCardType = 10, // тип документа, удостоверяющего личность

    /// <summary>
    /// The id card number.
    /// </summary>
    IdCardNumber = 11, // номер или серия и номер документа, удостоверяющего личность

    /// <summary>
    /// The id card date.
    /// </summary>
    IdCardDate = 12, // дата выдачи документа, удостоверяющего личность

    /// <summary>
    /// The id card date exp.
    /// </summary>
    IdCardDateExp = 13, // дата окончания действия документа, удостоверяющего личность

    /// <summary>
    /// The id card org.
    /// </summary>
    IdCardOrg = 14, // наименование организации, выдавшей документ, удостоверяющий личность

    /// <summary>
    /// The snils.
    /// </summary>
    SNILS = 15, // СНИЛС застрахованного лица

    /// <summary>
    /// The insurance territory.
    /// </summary>
    InsuranceTerritory = 16, // код территории страхования !! 5 цифр [ОИД 1.2.643.2.40.3.3.1]

    /// <summary>
    /// The insurance company code.
    /// </summary>
    InsuranceCompanyCode = 17, 

    // код организации-страхователя !! для начальной выгрузки здесь лежит ОГРН страховой медицинской организации, выдавшей полис ОМС (13 цифр)

    /// <summary>
    /// The policy actual from.
    /// </summary>
    PolicyActualFrom = 18, // дата выдачи полиса ОМС

    /// <summary>
    /// The policy actual to.
    /// </summary>
    PolicyActualTo = 19, // дата окончания действия полиса ОМС

    /// <summary>
    /// The date deregistration.
    /// </summary>
    DateDeregistration = 20, // дата снятия с учёта застрахованного лица

    /// <summary>
    /// The citizenry_ oks m_3.
    /// </summary>
    Citizenry_OKSM_3 = 21, 

    // гражданство застрахованного лица (трёхбуквенный код страны по классификатору ОКСМ.3) [ОИД ОКСМ.3 = 1.2.643.2.40.5.0.25.3]

    /// <summary>
    /// The death mark.
    /// </summary>
    DeathMark = 22, // признак смерти застрахованного лица

    /// <summary>
    /// The death date.
    /// </summary>
    DeathDate = 23, // дата смерти застрахованного лица

    /// <summary>
    /// The registration territory.
    /// </summary>
    RegistrationTerritory = 24, // код территории регистрации !! 5 цифр [ОИД 1.2.643.2.40.3.3.1]

    // Advanced CSV in card (field 25-27)
    /// <summary>
    /// The organization type.
    /// </summary>
    OrganizationType = 25, // тип организации

    /// <summary>
    /// The pvp id.
    /// </summary>
    PvpId = 26, // Идентификатор пункта выдачи полисов

    /// <summary>
    /// The insurance temp number.
    /// </summary>
    InsuranceTempNumber = 27, // Номер временного свидетельства

    // ------------------------------------------------------------------------------------------------

    /// <summary>
    /// The real identifier.
    /// </summary>
    RealIdentifier = 100, // значение реального идентификатора

    /// <summary>
    /// The uec.
    /// </summary>
    UEC = 101, // номер УЭК

    // ------------------------------------------------------------------------------------------------

    /// <summary>
    /// The region.
    /// </summary>
    Region = 1000, // регион
    // PolicyType = 1001, // тип полиса
    /// <summary>
    /// The insurance card id.
    /// </summary>
    InsuranceCardID = 1003, // номер бланка страховки

    /// <summary>
    /// The insurance company coding.
    /// </summary>
    InsuranceCompanyCoding = 1005, // тип кода организации-страхователя

    /// <summary>
    /// The citizenry.
    /// </summary>
    Citizenry = 1010, // гражданство застрахованного лица (в неспецифицированном формате)

    /// <summary>
    /// The citizenry_ oki n_2.
    /// </summary>
    Citizenry_OKIN_2 = 1011, // код гражданства по ОКИН [ОИД ОКИН.2 = 1.2.643.2.40.5.0.18.2]

    // ------------------------------------------------------------------------------------------------

    // [тип документа, удостоверяющего личность] + [номер или серия и номер документа, удостоверяющего личность]
    /// <summary>
    /// The id card type_ id card number.
    /// </summary>
    IdCardType_IdCardNumber = -101, 

    // [тип полиса] + [серия и номер выданного полиса ОМС]
    /// <summary>
    /// The policy type_ policy number.
    /// </summary>
    PolicyType_PolicyNumber = -102, 

    // [код организации-страхователя] + [тип кода организации-страхователя]
    /// <summary>
    /// The insurance company code_ insurance company coding.
    /// </summary>
    InsuranceCompanyCode_InsuranceCompanyCoding = -103, 

    // ------------------------------------------------------------------------------------------------

    /// <summary>
    /// The undefined.
    /// </summary>
    Undefined = -10000, // поле неопределено
  }
}