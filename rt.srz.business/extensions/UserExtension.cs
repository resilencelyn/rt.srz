// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserExtension.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.extensions
{
  using System.Linq;

  using NHibernate;

  using rt.core.model.core;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The user extension.
  /// </summary>
  public static class UserExtension
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
    public static Organisation GetSmo(this User user)
    {
      if (user.PointDistributionPolicyId != null)
      {
        return
          ObjectFactory.GetInstance<ISessionFactory>()
                       .GetCurrentSession()
                       .QueryOver<Organisation>()
                       .Where(x => x.Id == user.PointDistributionPolicyId.Value)
                       .Take(1)
                       .List()
                       .Single()
                       .Parent;
      }

      return null;
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
    public static Organisation GetTf(this User user)
    {
      if (user.PointDistributionPolicyId != null)
      {
        var pvp =
          ObjectFactory.GetInstance<ISessionFactory>()
                       .GetCurrentSession()
                       .QueryOver<Organisation>()
                       .Where(x => x.Id == user.PointDistributionPolicyId.Value)
                       .Take(1)
                       .List()
                       .Single();
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
      return user.PointDistributionPolicyId.HasValue
             && ObjectFactory.GetInstance<ISessionFactory>()
                             .GetCurrentSession()
                             .QueryOver<Organisation>()
                             .Where(x => x.Id == user.PointDistributionPolicyId.Value)
                             .Take(1)
                             .List()
                             .Single()
                             .Parent != null;
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

      var org =
        ObjectFactory.GetInstance<ISessionFactory>()
                     .GetCurrentSession()
                     .QueryOver<Organisation>()
                     .Where(x => x.Id == user.PointDistributionPolicyId.Value)
                     .Take(1)
                     .List()
                     .Single();
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
      return user.PointDistributionPolicyId != null
               ? ObjectFactory.GetInstance<ISessionFactory>()
                              .GetCurrentSession()
                              .QueryOver<Organisation>()
                              .Where(x => x.Id == user.PointDistributionPolicyId.Value)
                              .Take(1)
                              .List()
                              .Single()
               : null;
    }

    #endregion
  }
}