// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelAdapter.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Адаптер данных для расчета ключа
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  /// <summary>
  ///   Адаптер данных для расчета ключа
  /// </summary>
  public class ModelAdapter
  {
    #region Public Properties

    /// <summary>
    ///   Адрес регистрации
    /// </summary>
    public address Address1 { get; set; }

    /// <summary>
    ///   Адрес проживания
    /// </summary>
    public address Address2 { get; set; }

    /// <summary>
    ///   Документ УДЛ
    /// </summary>
    public Document Document { get; set; }

    /// <summary>
    ///   Медицинская страховка
    /// </summary>
    public MedicalInsurance MedicalInsurance { get; set; }

    /// <summary>
    ///   Окато территории страхования
    /// </summary>
    public string Okato { get; set; }

    /// <summary>
    ///   Персональные данные
    /// </summary>
    public InsuredPersonDatum PersonData { get; set; }

    /// <summary>
    ///   Описатель ключа
    /// </summary>
    public SearchKeyType SearchKeyType { get; set; }

    /// <summary>
    ///   Персональные данные
    /// </summary>
    public Statement Statement { get; set; }

    #endregion
  }
}