// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenCredentials.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Кридентиалы по токену
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.client
{
  #region references

  using System.ServiceModel.Channels;

  #endregion

  /// <summary>
  ///   Кридентиалы по токену
  /// </summary>
  public class TokenCredentials
  {
    #region Constants

    /// <summary>
    ///   Заголовок
    /// </summary>
    public const string CredentialsHeader = "TokenCredentials";

    /// <summary>
    ///   Неймспейс
    /// </summary>
    public const string CredentialsNamespace = "http://www.rintech.ru";

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="TokenCredentials" /> class.
    ///   Конструктор
    /// </summary>
    public TokenCredentials()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenCredentials"/> class.
    ///   Конструктор
    /// </summary>
    /// <param name="token">
    /// Токен
    /// </param>
    public TokenCredentials(Token token)
    {
      Token = token;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Токен
    /// </summary>
    public Token Token { get; set; }

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
    public static TokenCredentials FromMessageHeader(Message message)
    {
      var tokenPosition = message.Headers.FindHeader(CredentialsHeader, CredentialsNamespace);
      if (tokenPosition >= 0)
      {
        var credentials = message.Headers.GetHeader<TokenCredentials>(tokenPosition);
        return credentials;
      }

      ////tokenPosition = message.Headers.FindHeader(CredentialsHeader, CredentialsNamespace + "/");

      ////// пришли из JBoss
      ////if (tokenPosition >= 0)
      ////{
      ////  var credentials = new TokenCredentials();
      ////  var str = message.ToString();
      ////  var start = str.IndexOf("<ns1:Token>", StringComparison.OrdinalIgnoreCase) + "<ns1:Token>".Length;
      ////  var len = str.IndexOf("</ns1:Token>", StringComparison.OrdinalIgnoreCase) - start;
      ////  credentials.Token = str.Substring(start, len);
      ////  return credentials;
      ////}
      return null;
    }

    /// <summary>
    ///   Записывает в сообщение
    /// </summary>
    /// <returns>Сообщение</returns>
    public MessageHeader ToMessageHeader()
    {
      var header = MessageHeader.CreateHeader(CredentialsHeader, CredentialsNamespace, this);
      return header;
    }

    #endregion
  }
}