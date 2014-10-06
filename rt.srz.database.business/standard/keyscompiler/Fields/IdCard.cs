// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdCard.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The id card.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Fields
{
  /// <summary>
  /// The id card.
  /// </summary>
  public class IDCard
  {
    #region Fields

    /// <summary>
    /// The id card date.
    /// </summary>
    public SearchField idCardDate = new SearchField();

    /// <summary>
    /// The id card date exp.
    /// </summary>
    public SearchField idCardDateExp = new SearchField();

    /// <summary>
    /// The id card number.
    /// </summary>
    public SearchField idCardNumber = new SearchField();

    /// <summary>
    /// The id card org.
    /// </summary>
    public SearchField idCardOrg = new SearchField();

    /// <summary>
    /// The id card type.
    /// </summary>
    public SearchField idCardType = new SearchField();

    #endregion
  }
}