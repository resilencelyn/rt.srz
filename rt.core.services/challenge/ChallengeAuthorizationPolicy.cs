// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChallengeAuthorizationPolicy.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Проврка прав доступа
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.challenge
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.IdentityModel.Claims;
  using System.IdentityModel.Policy;
  using System.Security.Principal;
  using System.ServiceModel;

  #endregion

  /// <summary>
  ///   Проврка прав доступа
  /// </summary>
  public class ChallengeAuthorizationPolicy : IAuthorizationPolicy
  {
    #region Fields

    /// <summary>
    ///   ИД
    /// </summary>
    private Guid id = Guid.NewGuid();

    #endregion

    #region Public Properties

    /// <summary>
    ///   ИД
    /// </summary>
    public string Id
    {
      get
      {
        return id.ToString();
      }
    }

    /// <summary>
    ///   Кто крышует
    /// </summary>
    public ClaimSet Issuer
    {
      get
      {
        return ClaimSet.System;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Проверка
    /// </summary>
    /// <param name="evaluationContext">
    /// Контекст
    /// </param>
    /// <param name="state">
    /// Статус
    /// </param>
    /// <returns>
    /// фалс
    /// </returns>
    public bool Evaluate(EvaluationContext evaluationContext, ref object state)
    {
      var user = OperationContext.Current.IncomingMessageProperties["Principal"] as IPrincipal;
      evaluationContext.Properties["Principal"] = user;
      evaluationContext.Properties["Identities"] = new List<IIdentity> { user.Identity };

      return false;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Похоже не используется :(
    /// </summary>
    /// <param name="evaluationContext">
    /// Контекст
    /// </param>
    /// <returns>
    /// Идентити
    /// </returns>
    private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
    {
      object obj;
      if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
      {
        throw new Exception("No Identity found!");
      }

      var identities = obj as IList<IIdentity>;
      if (identities == null || identities.Count <= 0)
      {
        throw new Exception("No Identity found!");
      }

      return identities[0];
    }

    #endregion
  }
}