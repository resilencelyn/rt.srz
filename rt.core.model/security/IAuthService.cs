// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
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

    /// <summary>
    ///   The get auth response.
    /// </summary>
    /// <returns>
    ///   The <see cref="AuthResponse" />.
    /// </returns>
    Token GetAuthToken();

    #endregion
  }
}