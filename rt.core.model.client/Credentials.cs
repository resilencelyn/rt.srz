// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Credentials.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Кредентиалы
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.client
{
  #region references

  using System;
  using System.ServiceModel.Channels;

  #endregion

  /// <summary>
  ///   Кредентиалы
  /// </summary>
  public class Credentials
  {
    #region Constants

    /// <summary>
    ///   Заголовок
    /// </summary>
    public const string CredentialsHeader = "Credentials";

    /// <summary>
    ///   Неймспейс
    /// </summary>
    public const string CredentialsNamespace = "http://www.challenge-me.ws";

    /// <summary>
    ///   Срок годности
    /// </summary>
    public const int ExpiresTimeSpan = 10000;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Таймспан
    /// </summary>
    public static TimeSpan ClientServerTimeSpan { get; private set; }

    /// <summary>
    ///   Время по серверному
    /// </summary>
    public static DateTime ServerDateTime
    {
      get
      {
        return DateTime.Now - ClientServerTimeSpan;
      }

      set
      {
        ClientServerTimeSpan = DateTime.Now - value;
      }
    }

    /// <summary>
    ///   Когда кончится
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    ///   Пароль
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///   Пользователь
    /// </summary>
    public string UserName { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Читает из сообщения
    /// </summary>
    /// <param name="message">
    /// Сообщение
    /// </param>
    /// <returns>
    /// Кридентиалы
    /// </returns>
    public static Credentials FromMessageHeader(Message message)
    {
      var tokenPosition = message.Headers.FindHeader(CredentialsHeader, CredentialsNamespace);
      var credentials = message.Headers.GetHeader<Credentials>(tokenPosition);
      return credentials;
    }

    /// <summary>
    ///   Записывает в сообщение
    /// </summary>
    /// <returns>Сообщение</returns>
    public MessageHeader ToMessageHeader()
    {
      Expires = DateTime.Now.AddMilliseconds(ClientServerTimeSpan.TotalMilliseconds + ExpiresTimeSpan);
      var header = MessageHeader.CreateHeader(CredentialsHeader, CredentialsNamespace, this);
      return header;
    }

    #endregion
  }
}