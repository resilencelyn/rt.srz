// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthInfoLogMessage.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Информация об аутентфикации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.security.loggers
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   Информация об аутентфикации
  /// </summary>
  public class AuthInfoLogMessage
  {
    #region Public Properties

    /// <summary>
    ///   Адрес клиента
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///   Метод аутентификации
    /// </summary>
    public string AuthMethod { get; set; }

    /// <summary>
    ///   Дата эвента
    /// </summary>
    public DateTime EventDate { get; set; }

    /// <summary>
    ///   Успешна ли авторизация
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    ///   Порт клиента
    /// </summary>
    public string Port { get; set; }

    /// <summary>
    ///   Имя пользователя
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///   ВИА
    /// </summary>
    public Uri Via { get; set; }

    #endregion
  }
}