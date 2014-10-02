// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The DocumentManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The DocumentManager.
  /// </summary>
  public partial class DocumentManager
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
    public string GetSerNumDocument(Document document)
    {
      return GetSerNumDocument(document.Series, document.Number);
    }

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
    public string GetSerNumDocument(string series, string number)
    {
      return string.IsNullOrWhiteSpace(series)
               ? string.Format("{0}", number)
               : string.Format("{0} № {1}", series, number);
    }

    #endregion
  }
}