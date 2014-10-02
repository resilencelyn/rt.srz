//-----------------------------------------------------------------------
// <copyright file="EDS.cs" company="Rintech" author="Syurov">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.smc.model
{
  /// <summary>The eds.</summary>
  [ComVisible(true)]
  public class Eds
  {
    /// <summary>
    /// Сертификат СМО
    /// </summary>
    public string Certificate { get; set; }

    /// <summary>
    /// СМО
    /// </summary>
    public string Ecp { get; set; }

    /// <summary>
    /// Состояние ЭЦП
    /// </summary>
    public string StateEcp { get; set; }
  }
}
