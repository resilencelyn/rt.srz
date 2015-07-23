// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOidManager.cs" company="јль€нс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface OidManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The interface OidManager.
  /// </summary>
  public partial interface IOidManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get category by citizenship.
    /// </summary>
    /// <param name="citizenshipId">
    /// The citizenship id.
    /// </param>
    /// <param name="isnotCitizenship">
    /// The isnot citizenship.
    /// </param>
    /// <param name="isrefugee">
    /// The isrefugee.
    /// </param>
    /// <param name="age">
    /// The age.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    IList<Concept> GetCategoryByCitizenship(int citizenshipId, bool isnotCitizenship, bool isrefugee, TimeSpan age);

    /// <summary>
    /// The get document residency type by category.
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/>.
    /// </returns>
    IList<Concept> GetDocumentResidencyTypeByCategory(int categoryId);

    /// <summary>
    ///   The get document type for registration document
    /// </summary>
    /// <returns>
    ///   The <see cref="IList{Concept}" />.
    /// </returns>
    IList<Concept> GetDocumentTypeForRegistrationDocument();

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <returns>
    ///   The <see cref="IList{Concept}" />.
    /// </returns>
    IList<Concept> GetDocumentTypeForRepresentative();

    /// <summary>
    /// The get document type by category.
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <param name="age">
    /// The age.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/>.
    /// </returns>
    IList<Concept> GetDocumentUdlTypeByCategory(int categoryId, TimeSpan age);

    /// <summary>
    /// ¬озвращает список типов полиса в зависимости от причины обращени€
    /// </summary>
    /// <param name="causeFilling">
    /// The cause Filling.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/>.
    /// </returns>
    IList<Concept> GetFormManufacturingByCauseFilling(int causeFilling);

    /// <summary>
    /// ¬озвращает список нормативно справочных данных
    /// </summary>
    /// <param name="nsiId">
    /// The nsi Id.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    IList<Concept> GetNsiRecords(string nsiId);

    /// <summary>
    /// ¬озвращает список нормативно справочных данных
    /// </summary>
    /// <param name="nsiId">
    /// The nsi Id.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    IList<Concept> GetNsiRecords(IEnumerable<string> nsiId);

    /// <summary>
    /// The get type polis by form manufacturing.
    /// </summary>
    /// <param name="formManufacturing">
    /// The form manufacturing.
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/>.
    /// </returns>
    IList<Concept> GetTypePolisByFormManufacturing(int formManufacturing);

    #endregion
  }
}