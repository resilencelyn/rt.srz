// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizeResult.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The authorize result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.service.dto
{
  #region references

  using System.Runtime.InteropServices;

  #endregion

  /// <summary>
  /// The authorize result.
  /// </summary>
  [ComVisible(true)]
  public class AuthorizeResult : OperationResult
  {
    #region Public Properties

    /// <summary>
    ///   Количество оставшихся попыток ввода ПИН- кода
    /// </summary>
    public virtual byte PinRestTriesOut { get; set; }

    #endregion
  }
}