// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using rt.core.model.core;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  /// The user manager.
  /// </summary>
  public static class UserManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get pvp.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    public static Organisation GetTf(this User user)
    {
      var organisationManager = ObjectFactory.GetInstance<IOrganisationManager>();
      if (user.PointDistributionPolicyId != null)
      {
        var pvp = organisationManager.GetById(user.PointDistributionPolicyId.Value);
        if (pvp != null && pvp.Parent != null)
        {
          return pvp.Parent.Parent;
        }

        return pvp;
      }

      return null;
    }

    /// <summary>
    /// The has pvp.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool HasSmo(this User user)
    {
      var organisationManager = ObjectFactory.GetInstance<IOrganisationManager>();
      return user.PointDistributionPolicyId.HasValue && organisationManager.GetById(user.PointDistributionPolicyId.Value).Parent != null;
    }

    /// <summary>
    /// The get pvp.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    public static Organisation GetSmo(this User user)
    {
      if (user.PointDistributionPolicyId != null)
      {
        var organisationManager = ObjectFactory.GetInstance<IOrganisationManager>();
        var pvp = organisationManager.GetById(user.PointDistributionPolicyId.Value);
        return pvp.Parent;
      }

      return null;
    }

    /// <summary>
    /// The has tf.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool HasTf(this User user)
    {
      if (user.PointDistributionPolicyId == null)
      {
        return false;
      }

      var organisationManager = ObjectFactory.GetInstance<IOrganisationManager>();
      var org = organisationManager.GetById(user.PointDistributionPolicyId.Value);
      return org != null && org.Parent != null && org.Parent.Parent != null;
    }

    /// <summary>
    /// The point distribution policy.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    public static Organisation PointDistributionPolicy(this User user)
    {
      var organisationManager = ObjectFactory.GetInstance<IOrganisationManager>();
      return user.PointDistributionPolicyId != null ? organisationManager.GetById(user.PointDistributionPolicyId.Value) : null;
    }

    #endregion
  }
}