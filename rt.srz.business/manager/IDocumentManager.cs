// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDocumentManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface DocumentManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The interface DocumentManager.
  /// </summary>
  public partial interface IDocumentManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает строку докумета для федералов
    /// </summary>
    /// <param name="document">
    /// The document.
    /// </param>
    /// <returns>
    /// строка документа
    /// </returns>
    string GetSerNumDocument(Document document);

    /// <summary>
    /// The get ser num document.
    /// </summary>
    /// <param name="series">
    /// The series.
    /// </param>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetSerNumDocument(string series, string number);

    #endregion
  }
}