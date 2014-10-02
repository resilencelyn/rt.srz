// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenAuthenticationManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Менеджер аутентификации по токену
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NHibernate;
using NHibernate.Context;

namespace rt.core.services.challenge
{
  #region references

  using System;
  using System.Collections.ObjectModel;
  using System.IdentityModel.Policy;
  using System.Security.Authentication;
  using System.Security.Principal;
  using System.ServiceModel;
  using System.ServiceModel.Channels;

  using rt.core.business.security.interfaces;
  using rt.core.model.client;

  using StructureMap;

  #endregion

  /// <summary>
  ///   Менеджер аутентификации по токену
  /// </summary>
  public class TokenAuthenticationManager : ServiceAuthenticationManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Проверка пароля
    /// </summary>
    /// <param name="authPolicy">
    /// полиция ёпта
    /// </param>
    /// <param name="listenUri">
    /// урл
    /// </param>
    /// <param name="message">
    /// сообщение
    /// </param>
    /// <returns>
    /// принципалы
    /// </returns>
    public override ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
    {
      var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      using (var session = sessionFactory.OpenSession())
      {
        try
        {
          CurrentSessionContext.Bind(session);

          var credentials = TokenCredentials.FromMessageHeader(message);

          var user = ObjectFactory.GetInstance<ISecurityProvider>().GetDateFromToken(credentials.Token);
          ////var user = ObjectFactory.GetInstance<ISecurityProvider>().GetUserByName("admin");
          if (user != null)
          {
            var identity = new ChallengeIdentity(user.Login, user.Password);
            IPrincipal principal = new ChallengePrincipal(identity);
            message.Properties["Principal"] = principal;
            return authPolicy;
          }
        }
        finally
        {
          CurrentSessionContext.Unbind(sessionFactory);
          session.Flush();
          session.Close();
        }
      }

      throw new AuthenticationException("Incorrect credentials!");
    }

    #endregion
  }
}