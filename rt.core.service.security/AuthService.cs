// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   сервис администрирования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Authentication;
using NHibernate;
using rt.core.business.security.repository;

namespace rt.core.service.security
{
  #region references

  using System;
  using System.IO;
  using System.Security.Cryptography;

  using ProtoBuf;

  using rt.core.business.security.interfaces;
  using rt.core.model.client;
  using rt.core.model.security;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   сервис администрирования
  /// </summary>
  public class AuthService : IAuthService
  {
    #region Static Fields

    /// <summary>
    ///   Алгоритм
    /// </summary>
    private static readonly RSACryptoServiceProvider alg;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes static members of the <see cref="AuthService"/> class. 
    ///   Статический конструктор
    /// </summary>
    static AuthService()
    {
      alg = new RSACryptoServiceProvider();
      alg.FromXmlString(
        "<RSAKeyValue><Modulus>mjjXXni9qZbQBI8BxDuSVyF8xfWui7EE0az5yZ5OLJH+ZOCaqODhwxmACT6GihIq4Z0hrm7j8qH5wW9PK+aMEelFCQ1Sdxuh9Gwj2xraWTCTxj9RV4e1zXwR7t1ijBRCGN7HmRGijnFwOijiP0+RxVzXZE68JREhKrxT7JfTWEk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Аутентификация по пользователю и паролю
    /// </summary>
    /// <param name="userName">
    /// Имя пользователя
    /// </param>
    /// <param name="password">
    /// Пароль
    /// </param>
    /// <returns>
    /// Результат аутентификации
    /// </returns>
    public AuthResponse Authenticate(string userName, string password)
    {
      // используем сессию, так как вызовы идут из ядра и до привязки контекста к сессии.
      using (var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession())
      {
        var user = session.QueryOver<User>().Where(x => x.Login == userName && x.IsApproved).Take(1).SingleOrDefault();
        if (user == null)
        {
          throw new AuthenticationException("Incorrect credentials!");
        }

        var hash = new PasswordHash(Convert.FromBase64String(user.Salt), Convert.FromBase64String(user.Password));

        if (!hash.Verify(password))
        {
          throw new AuthenticationException("Incorrect credentials!");
        }

        user.LastLoginDate = DateTime.Now;
        session.Update(user);
        session.Flush();
        return ObjectFactory.GetInstance<ISecurityProvider>().GetAuthToken(user);
      }
    }

    /// <summary>
    /// The get auth response.
    /// </summary>
    /// <returns>
    /// The <see cref="AuthResponse"/>.
    /// </returns>
    public Token GetAuthToken()
    {
      var user = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      return ObjectFactory.GetInstance<ISecurityProvider>().GetAuthToken(user).AuthToken;
    }

    #endregion
  }
}