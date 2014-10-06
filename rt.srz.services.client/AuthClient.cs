// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthClient.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The auth client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client
{
  #region references

  using rt.core.model.client;
  using rt.core.model.security;
  using rt.core.services.registry;

  #endregion

  /// <summary>
  ///   The auth client.
  /// </summary>
  public class AuthClient : ServiceClient<IAuthService>, IAuthService
  {
    #region Public Methods and Operators

    /// <summary>
    /// The authenticate.
    /// </summary>
    /// <param name="userName">
    /// The user name.
    /// </param>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <returns>
    /// The <see cref="AuthResponse"/>.
    /// </returns>
    public AuthResponse Authenticate(string userName, string password)
    {
      return InvokeInterceptors(() => Service.Authenticate(userName, password));
    }

    /// <summary>
    ///   The get auth response.
    /// </summary>
    /// <returns>
    ///   The <see cref="AuthResponse" />.
    /// </returns>
    public Token GetAuthToken()
    {
      return InvokeInterceptors(() => Service.GetAuthToken());
    }

    #endregion
  }
}