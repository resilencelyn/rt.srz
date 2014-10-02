// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthResponse.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Ответ авторизации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.security
{
  #region references

  using System.Runtime.Serialization;

  using rt.core.model.client;

  #endregion

  /// <summary>
  ///   Ответ авторизации
  /// </summary>
  [DataContract]
  public class AuthResponse
  {
    #region Public Properties

    /// <summary>
    ///   Токен авторизации
    /// </summary>
    [DataMember(Order = 2)]
    public Token AuthToken { get; set; }

    /// <summary>
    ///   Дружественное имя пользователя
    /// </summary>
    [DataMember(Order = 3)]
    public string FriendlyName { get; set; }

    /// <summary>
    ///   Прошёл ли пользователь аутентификацию
    /// </summary>
    [DataMember(Order = 1)]
    public bool IsAuthenticated { get; set; }

    /// <summary>
    ///   Идентификатор пользователя
    /// </summary>
    [DataMember(Order = 4)]
    public string UserUid { get; set; }

    #endregion
  }
}