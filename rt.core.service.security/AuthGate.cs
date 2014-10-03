// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthGate.svc.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Сервис авторизации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ServiceModel.Activation;
using rt.core.services.wcf;

namespace rt.core.service.security
{
  #region references

  using System.ServiceModel;

  using rt.core.model.client;
  using rt.core.model.security;
  using rt.core.services.aspects;
  using rt.core.services.nhibernate;

  #endregion

  /// <summary>
  ///   Сервис авторизации
  /// </summary>
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class AuthGate : InterceptedBase, IAuthService
  {
    /// <summary>
    /// The service.
    /// </summary>
    private readonly IAuthService service = new AuthService();

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
      return InvokeInterceptors(() => service.Authenticate(userName, password));
    }

    /// <summary>
    /// The get auth response.
    /// </summary>
    /// <returns>
    /// The <see cref="AuthResponse"/>.
    /// </returns>
    public Token GetAuthToken()
    {
      return InvokeInterceptors(() => service.GetAuthToken());
    }

    #endregion
  }
}