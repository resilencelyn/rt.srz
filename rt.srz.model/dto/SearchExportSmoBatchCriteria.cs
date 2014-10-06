// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchExportSmoBatchCriteria.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search export smo batch criteria.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  using System;
  using System.Runtime.Serialization;

  using rt.core.model.dto;

  /// <summary>
  /// The search export smo batch criteria.
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchExportSmoBatchCriteria : BaseSearchCriteria
  {
    #region Public Properties

    /// <summary>
    ///   Номер батча
    /// </summary>
    public int BatchNumber { get; set; }

    /// <summary>
    ///   Идентификатор периода
    /// </summary>
    public Guid PeriodId { get; set; }

    /// <summary>
    ///   Получатель батча
    /// </summary>
    public Guid ReceiverId { get; set; }

    /// <summary>
    ///   Отправитель батча
    /// </summary>
    public Guid SenderId { get; set; }

    #endregion
  }
}