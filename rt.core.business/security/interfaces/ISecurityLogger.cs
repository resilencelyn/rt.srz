// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISecurityLogger.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Интерфейс логирования сообщений по безопасности
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.security.interfaces
{
  #region references

  using System;
  using System.ServiceModel.Channels;

  #endregion

  /// <summary>
  ///   Интерфейс логирования сообщений по безопасности
  /// </summary>
  public interface ISecurityLogger
  {
    #region Public Methods and Operators

    /// <summary>
    /// Ошибка аутентификации пользователя
    /// </summary>
    /// <param name="isSuccess">
    /// Удачна ли попытка входа
    /// </param>
    /// <param name="authMethod">
    /// Метод аутентификации
    /// </param>
    /// <param name="userName">
    /// Имя пользователя
    /// </param>
    /// <param name="endpointProperty">
    /// Ендпоинт клиента
    /// </param>
    /// <param name="via">
    /// Адрес метода
    /// </param>
    void LogAuth(
      bool isSuccess, 
      string authMethod, 
      string userName, 
      RemoteEndpointMessageProperty endpointProperty, 
      Uri via);

    #endregion
  }
}