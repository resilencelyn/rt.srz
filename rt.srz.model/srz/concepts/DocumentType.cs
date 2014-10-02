// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentType.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Документ УДЛ
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.srz.model.srz.concepts
{
  /// <summary> Документ УДЛ </summary>
  public class DocumentType : Concept
  {
    /// <summary> Паспорт гражданина СССР </summary>
    public const int DocumentType1 = 1;

    /// <summary> Свидетельство о регистрации ходатайства о признании иммигранта беженцем </summary>
    public const int DocumentType10 = 10;

    /// <summary> Вид на жительство </summary>
    public const int DocumentType11 = 11;

    /// <summary> Удостоверение беженца в Российской Федерации </summary>
    public const int DocumentType12 = 12;

    /// <summary> Временное удостоверение личности гражданина Российской Федерации </summary>
    public const int DocumentType13 = 13;

    /// <summary> Паспорт гражданина Российской Федерации </summary>
    public const int PassportRf = 14;

    /// <summary> Заграничный паспорт гражданина Российской Федерации </summary>
    public const int DocumentType15 = 15;

    /// <summary> Паспорт моряка </summary>
    public const int DocumentType16 = 16;

    /// <summary> Военный билет офицера запаса </summary>
    public const int DocumentType17 = 17;

    /// <summary> Иные документы </summary>
    public const int DocumentType18 = 18;

    /// <summary> Загранпаспорт гражданина СССР </summary>
    public const int DocumentType2 = 2;

    /// <summary> Свидетельство о рождении Российский Федерации </summary>
    public const int BirthCertificateRf = 3;

    /// <summary> Удостоверение личности офицера </summary>
    public const int DocumentType4 = 4;

    /// <summary> Справка об освобождении из места лишения свободы </summary>
    public const int DocumentType5 = 5;

    /// <summary> Паспорт Минморфлота </summary>
    public const int DocumentType6 = 6;

    /// <summary> Военный билет </summary>
    public const int DocumentType7 = 7;

    /// <summary> Дипломатический паспорт гражданина Российской Федерации </summary>
    public const int DocumentType8 = 8;

    /// <summary> Иностранный паспорт </summary>
    public const int DocumentType9 = 9;

    /// <summary> Копия жалобы на решение о лишении статуса беженца в Федеральную миграционную службу с отметкой о её приёме к рассмотрению </summary>
    public const int DocumentType20 = 391;

    /// <summary> Документ иностранного гражданина </summary>
    public const int DocumentType21 = 392;

    /// <summary> Документ лица без гражданства </summary>
    public const int DocumentType22 = 393;

    /// <summary> Разрешение на временное проживание  </summary>
    public const int DocumentType23 = 394;

    /// <summary> Свидетельство о регистрации по месту жительства  </summary>
    public const int CertificationRegistration = 429;

    /// <summary> Свидетельство о рождении, выданное не в Российской Федерации </summary>
    public const int DocumentType24 = 629;

    /// <summary> Свидетельство о предоставлении временного убежища на территории Российской Федерации </summary>
    public const int DocumentType25 = 630;

    public static bool IsDocExp(int id)
    {
      if (id == DocumentType10)
      {
        return true;
      }

      if (id == DocumentType11)
      {
        return true;
      }

      if (id == DocumentType12)
      {
        return true;
      }

      if (id == DocumentType13)
      {
        return true;
      }

      if (id == DocumentType23)
      {
        return true;
      }

      if (id == DocumentType25)
      {
        return true;
      }

      return false;
    }
  }
}