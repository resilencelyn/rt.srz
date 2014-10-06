// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fileld.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search field.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Fields
{
  /// <summary>
  /// The search field.
  /// </summary>
  public class SearchField
  {
    #region Fields

    /// <summary>
    /// The field type.
    /// </summary>
    public FieldTypes fieldType = FieldTypes.Undefined;

    /// <summary>
    /// The value.
    /// </summary>
    public string value = string.Empty;

    #endregion
  }
}