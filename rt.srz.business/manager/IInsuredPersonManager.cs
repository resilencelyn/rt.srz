// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInsuredPersonManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface InsuredPersonManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.srz.model.srz;

  /// <summary>
  ///   The interface InsuredPersonManager.
  /// </summary>
  public partial interface IInsuredPersonManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The add twins.
    /// </summary>
    /// <param name="insuredPersons">
    /// The insured persons.
    /// </param>
    void AddTwinsFirstAndOther(IList<InsuredPerson> insuredPersons);

    /// <summary>
    /// Удаление инфы о смерти
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    void DeleteDeathInfo(Guid statementId);

    /// <summary>
    /// Входит ли указанная персона в объединение как главное или как второе лицо
    /// </summary>
    /// <param name="personId">
    /// The person Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool InsuredInJoined(Guid personId);

    /// <summary>
    /// The on canceled or remove statement.
    /// </summary>
    /// <param name="insuredPerson">
    /// The insured person.
    /// </param>
    void OnCanceledOrRemoveStatement(InsuredPerson insuredPerson);

    #endregion
  }
}