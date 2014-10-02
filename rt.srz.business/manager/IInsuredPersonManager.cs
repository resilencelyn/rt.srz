// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInsuredPersonManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface InsuredPersonManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface InsuredPersonManager.
  /// </summary>
  public partial interface IInsuredPersonManager
  {
    /// <summary>
    /// Входит ли указанная персона в объединение как главное или как второе лицо
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    bool InsuredInJoined(Guid personId);

    /// <summary>
    /// The add twins.
    /// </summary>
    /// <param name="insuredPersons">
    /// The insured persons.
    /// </param>
    void AddTwinsFirstAndOther(IList<InsuredPerson> insuredPersons);

    /// <summary>
    /// The on canceled or remove statement.
    /// </summary>
    /// <param name="insuredPerson">
    /// The insured person.
    /// </param>
    void OnCanceledOrRemoveStatement(InsuredPerson insuredPerson);

    /// <summary>
    /// Удаление инфы о смерти
    /// </summary>
    /// <param name="statementId"></param>
    void DeleteDeathInfo(Guid statementId);

  }
}