// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBatchManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface BatchManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.srz;

  /// <summary>
  ///   The interface BatchManager.
  /// </summary>
  public partial interface IBatchManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Получает список пфр батчей по периоду
    /// </summary>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Batch}"/>.
    /// </returns>
    IList<Batch> GetPfrBatchesByPeriod(Guid periodId);

    /// <summary>
    ///   The get pfr batches by user.
    /// </summary>
    /// <returns>
    ///   The <see cref="IList{Batch}" />.
    /// </returns>
    IList<Batch> GetPfrBatchesByUser();

    /// <summary>
    ///   Возвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns>
    ///   The <see cref="IList{Batch}" />.
    /// </returns>
    IList<Period> GetPfrPeriods();

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> .
    /// </returns>
    PfrStatisticInfo GetPfrStatisticInfoByBatch(Guid batchId);

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> .
    /// </returns>
    PfrStatisticInfo GetPfrStatisticInfoByPeriod(Guid periodId);

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    void MarkBatchAsUnexported(Guid batchId);

    /// <summary>
    /// Осуществляет поиск пакетных операций экспорта заявлений для СМО
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{SearchBatchResult}"/>.
    /// </returns>
    SearchResult<SearchBatchResult> SearchExportSmoBatches(SearchExportSmoBatchCriteria criteria);

    #endregion
  }
}