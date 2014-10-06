// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WardUnidimensional.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ward unidimensional.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.enums
{
  /// <summary>
  ///   The ward unidimensional.
  /// </summary>
  public enum WardUnidimensional
  {
    /// <summary>
    ///   The forward.
    /// </summary>
    Forward = 0x01, 

    /// <summary>
    ///   The back.
    /// </summary>
    Back = 0x02, 

    // оба направления
    /// <summary>
    ///   The both.
    /// </summary>
    Both = Forward | Back, 

    // ни одного направления
    /// <summary>
    ///   The none.
    /// </summary>
    None = 0x00, 
  }
}