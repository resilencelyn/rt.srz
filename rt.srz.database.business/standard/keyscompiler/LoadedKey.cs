// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadedKey.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The loaded key.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler
{
  using rt.srz.database.business.standard.keyscompiler.Fields;

  /// <summary>
  /// The loaded key.
  /// </summary>
  public class LoadedKey
  {
    #region Fields

    /// <summary>
    /// The formula.
    /// </summary>
    public FieldTypes[] formula;

    /// <summary>
    /// The id card date.
    /// </summary>
    public string idCardDate;

    /// <summary>
    /// The id card date exp.
    /// </summary>
    public string idCardDateExp;

    /// <summary>
    /// The id card number.
    /// </summary>
    public string idCardNumber;

    /// <summary>
    /// The id card org.
    /// </summary>
    public string idCardOrg;

    /// <summary>
    /// The name.
    /// </summary>
    public string name;

    /// <summary>
    /// The subtype.
    /// </summary>
    public int subtype;

    /// <summary>
    /// The type.
    /// </summary>
    public int type;

    #endregion
  }
}