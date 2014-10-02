// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRspZk.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Интерфейс ответов списков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System.Collections.Generic;

  #endregion

  /// <summary>
  ///   Интерфейс ответов списков
  /// </summary>
  public interface IRspZk
  {
    #region Public Properties

    /// <summary>
    ///   Список ответа
    /// </summary>
    List<QueryResponse> QueryResponseList { get; set; }

    #endregion
  }
}