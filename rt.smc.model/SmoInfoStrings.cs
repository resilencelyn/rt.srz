//-----------------------------------------------------------------------
// <copyright file="SmoInfoStrings.cs" company="Rintech" author="Syurov">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.smc.model
{
  /// <summary>The smo info strings.</summary>
  [ComVisible(true)]
  public class SmoInfoStrings
  {
    /// <summary>
    /// Данные ЭЦП
    /// </summary>
    public Eds Eds { get; set; }

    /// <summary>
    /// ОГРН СМО
    /// </summary>
    public string OgrnSmo { get; set; }

    /// <summary>
    /// ОКАТО территории СМО
    /// </summary>
    public string Okato { get; set; }

    /// <summary>
    /// Дата начала действия страховки
    /// </summary>
    public string InsuranceStartDate { get; set; }

    /// <summary>
    /// Дата окончания действия страховки
    /// </summary>
    public string InsuranceEndDate { get; set; }
  }
}
