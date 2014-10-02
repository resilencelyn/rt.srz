// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOidManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
    /// <param name="age"> </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    IList<Concept> GetCategoryByCitizenship(int citizenshipId, bool isnotCitizenship, bool isrefugee, TimeSpan age);

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
    /// The <see cref="IList"/>.
    /// </returns>
    IList<Concept> GetDocumentUdlTypeByCategory(int categoryId, TimeSpan age);

    /// <summary>
    ///   The get document type for registration document
    /// </summary>
    /// <param name="categoryId">
    ///   The category id.
    /// </param>
    /// <param name="age">
    ///   The age.
    /// </param>
    /// <returns>
    ///   The <see cref="IList" />.
    /// </returns>
    IList<Concept> GetDocumentTypeForRegistrationDocument();

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <param name="categoryId">
    ///   The category id.
    /// </param>
    /// <param name="age">
    ///   The age.
    /// </param>
    /// <returns>
    ///   The <see cref="IList" />.
    /// </returns>
    IList<Concept> GetDocumentTypeForRepresentative();

    /// <summary>
    /// Возвращает список нормативно справочных данных
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
    /// Возвращает список нормативно справочных данных
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
    /// Возвращает список типов полиса в зависимости от причины обращения 
    /// </summary>
    /// <returns></returns>
    IList<Concept> GetFormManufacturingByCauseFilling(int causeFilling);
    
    #endregion

    IList<Concept> GetDocumentResidencyTypeByCategory(int categoryId);

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
  }
}