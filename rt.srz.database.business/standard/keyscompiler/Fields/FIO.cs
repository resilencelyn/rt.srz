// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FIO.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fio.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Fields
{
  /// <summary>
  /// The fio.
  /// </summary>
  public class FIO
  {
    #region Fields

    /// <summary>
    /// The family name.
    /// </summary>
    public SearchField familyName = new SearchField();

    /// <summary>
    /// The first name.
    /// </summary>
    public SearchField firstName = new SearchField();

    /// <summary>
    /// The middle name.
    /// </summary>
    public SearchField middleName = new SearchField();

    #endregion
  }
}