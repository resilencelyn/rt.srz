// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OmsData.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The oms data result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.service.dto
{
  #region references

  using System.Runtime.InteropServices;

  #endregion

  /// <summary>
  /// The oms data result.
  /// </summary>
  [ComVisible(true)]
  public class OMSDataResult : OperationResult
  {
    #region Public Properties

    /// <summary>
    ///   Дата начала действия страховки
    /// </summary>
    public virtual string DateFrom { get; set; }

    /// <summary>
    ///   Дата окончания действия страховки
    /// </summary>
    public virtual string DateTo { get; set; }

    /// <summary>
    ///   ОГРН СМО
    /// </summary>
    public virtual string Ogrn { get; set; }

    /// <summary>
    ///   ОКАТО территории страхования
    /// </summary>
    public virtual string Okato { get; set; }

    #endregion
  }
}