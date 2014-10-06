// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The User.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.core
{
  /// <summary>
  ///   The User.
  /// </summary>
  public partial class User
  {
    #region Public Properties

    /// <summary>
    /// Gets a value indicating whether is admin.
    /// </summary>
    public virtual bool IsAdmin
    {
      get
      {
        return Login.ToLower() == "admin";
      }
    }

    #endregion
  }
}