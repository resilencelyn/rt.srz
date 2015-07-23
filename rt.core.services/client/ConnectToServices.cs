// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectToServices.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The connect to services.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.client
{
  #region references

  using System;
  using System.Security.Authentication;

  using rt.core.model.client;
  using rt.core.model.security;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The connect to services.
  /// </summary>
  public static class ConnectToServices
  {
    #region Properties

    /// <summary>
    ///   Логин
    /// </summary>
    internal static string Login { get; set; }

    /// <summary>
    ///   Пароль
    /// </summary>
    internal static string Password { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Создает и открывает подключение
    /// </summary>
    /// <param name="login">
    /// The login.
    /// </param>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <returns>
    /// Результат
    /// </returns>
    public static bool Connect(string login, string password)
    {
      if (string.IsNullOrEmpty(login))
      {
        login = Login;
      }

      if (string.IsNullOrEmpty(password))
      {
        password = Password;
      }

      var popitka = 0;
      while (popitka < 3)
      {
        try
        {
          var retVal = false;
          var authService = ObjectFactory.GetInstance<IAuthService>();
          var res = authService.Authenticate(login, password);
          TokenMessageInspector.TokenFunc = () => res.AuthToken;
          Login = login;
          Password = password;
          if (res.IsAuthenticated)
          {
            retVal = true;
          }

          return retVal;
        }
        catch (AuthenticationException)
        {
          NLog.LogManager.GetCurrentClassLogger().Error("Неудачная попытка аутентификации №" + popitka);
        }
        catch (Exception ex)
        {
          NLog.LogManager.GetCurrentClassLogger().Error("Неудачная попытка аутентификации №" + popitka, ex);
        }

        popitka++;
      }

      return false;
    }

    #endregion
  }
}