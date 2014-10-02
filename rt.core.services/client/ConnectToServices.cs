// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectToServices.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The connect to services.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.client
{
  #region references

  using System;

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
    ///   �����
    /// </summary>
    internal static string Login { get; set; }

    /// <summary>
    ///   ������
    /// </summary>
    internal static string Password { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ������� � ��������� �����������
    /// </summary>
    /// <param name="login">
    /// The login.
    /// </param>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <returns>
    /// ���������
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
        catch (Exception ex)
        {
          ////todo: some thing
        }

        popitka++;
      }

      return false;
    }

    #endregion
  }
}