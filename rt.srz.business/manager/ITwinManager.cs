// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITwinManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface TwinManager.
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
  ///   The interface TwinManager.
  /// </summary>
  public partial interface ITwinManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Аннулирование дубликата
    /// </summary>
    /// <param name="twinId">
    /// The twin id.
    /// </param>
    void AnnulateTwin(Guid twinId);

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId">
    /// The key id.
    /// </param>
    void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId);

    /// <summary>
    /// Дубликаты по критерию для разбивки постранично
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{Twin}"/> .
    /// </returns>
    SearchResult<Twin> GetTwins(SearchTwinCriteria criteria);

    /// <summary>
    /// Объединяет дубликаты
    /// </summary>
    /// <param name="twinId">
    /// The twin id.
    /// </param>
    /// <param name="mainInsuredPersonId">
    /// The main Insured Person id.
    /// </param>
    /// <param name="secondInsuredPersonId">
    /// The second Insured Person id.
    /// </param>
    void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId);

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    void RemoveTwin(Guid id);

    /// <summary>
    /// Разделение
    /// </summary>
    /// <param name="personId">
    /// The person id.
    /// </param>
    /// <param name="statementsToSeparate">
    /// The statements To Separate.
    /// </param>
    /// <param name="copyDeadInfo">
    /// The copy Dead Info.
    /// </param>
    /// <param name="status">
    /// The status.
    /// </param>
    void Separate(Guid personId, IList<Statement> statementsToSeparate, bool copyDeadInfo, int status);

    #endregion
  }
}