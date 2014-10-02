// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Сервис авторизации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.security
{
  #region references

  using System.ServiceModel;

  using rt.core.model.client;

  #endregion

  /// <summary>
  ///   Сервис авторизации
  /// </summary>
  [ServiceContract]
  public interface IAuthService
  {
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
    [OperationContract]
    AuthResponse Authenticate(string userName, string password);

    #endregion

    /// <summary>
    /// The get auth response.
    /// </summary>
    /// <returns>
    /// The <see cref="AuthResponse"/>.
    /// </returns>
    Token GetAuthToken();
  }
}