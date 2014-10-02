// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCardInfoResult.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The get card info result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.service.dto
{
  #region references

  using System.Runtime.InteropServices;

  #endregion

  /// <summary>
  /// The get card info result.
  /// </summary>
  [ComVisible(true)]
  public class GetCardInfoResult : OperationResult
  {
    #region Public Properties

    /// <summary>
    ///   Дата рождения
    /// </summary>
    public virtual string Birthday { get; set; }

    /// <summary>
    ///   Место рождения
    /// </summary>
    public virtual string Birthplace { get; set; }

    /// <summary>
    ///   Имя
    /// </summary>
    public virtual string FirstName { get; set; }

    /// <summary>
    ///   Пол
    /// </summary>
    public virtual string Gender { get; set; }

    /// <summary>
    ///   Фамилия
    /// </summary>
    public virtual string LastName { get; set; }

    /// <summary>
    ///   Отчество
    /// </summary>
    public virtual string MiddleName { get; set; }

    /// <summary>
    ///   Номер полиса (ЕНП)
    /// </summary>
    public virtual string PolisNumber { get; set; }

    /// <summary>
    ///   СНИЛС
    /// </summary>
    public virtual string Snils { get; set; }

    /// <summary>
    /// Тип документа УДЛ
    /// </summary>
    public virtual string DocumentType { get; set; }

    /// <summary>
    /// Серия документа
    /// </summary>
    public virtual string DocumentSeries { get; set; }

    /// <summary>
    /// Номер документа
    /// </summary>
    public virtual string DocumentNumber { get; set; }

    /// <summary>
    /// Дата выдачи документа
    /// </summary>
    public virtual string DocumentIssueDate { get; set; }

    /// <summary>
    /// Кем выдан
    /// </summary>
    public virtual string DocumentIssueAuthority { get; set; }  

    #endregion
  }
}