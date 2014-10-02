// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The User.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  /// <summary>
  ///   The User.
  /// </summary>
  public partial class User
  {
    public virtual bool IsAdmin
    {
      get { return Login.ToLower() == "admin"; }
    }

    /// <summary>
    /// The has pvp.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public virtual bool HasTf()
    {
      return PointDistributionPolicy != null
        && PointDistributionPolicy.Parent != null
        && PointDistributionPolicy.Parent.Parent != null;
    }

    /// <summary>
    /// The has pvp.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public virtual bool HasSmo()
    {
      return PointDistributionPolicy != null && PointDistributionPolicy.Parent != null;
    }

    /// <summary>
    /// The get pvp.
    /// </summary>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    public virtual Organisation GetSmo()
    {
      return HasSmo() ? PointDistributionPolicy.Parent : null;
    }
    
    /// <summary>
    /// The get pvp.
    /// </summary>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    public virtual Organisation GetTf()
    {
      return HasTf() ? PointDistributionPolicy.Parent.Parent : null;
    }
  }
}