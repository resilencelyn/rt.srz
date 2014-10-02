// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationResult.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The operation result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.service.dto
{
  #region references

  using System.Runtime.InteropServices;

  #endregion

  /// <summary>
  /// The operation result.
  /// </summary>
  [ComVisible(true)]
  public class OperationResult
  {
    #region Public Properties

    /// <summary>
    ///   Описание ошибки прикладного уровня
    /// </summary>
    public virtual string ErrorString { get; set; }

    /// <summary>
    ///   Код результата
    /// </summary>
    public virtual uint Result { get; set; }

    #endregion
  }
}