// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChallengeIdentity.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Удостоверение
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.challenge
{
  #region references

  using System.Security.Principal;

  #endregion

  /// <summary>
  ///   Удостоверение
  /// </summary>
  public class ChallengeIdentity : IIdentity
  {
    #region Fields

    /// <summary>
    ///   Имя
    /// </summary>
    private readonly string name;

    /// <summary>
    ///   Имя
    /// </summary>
    private readonly string password;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ChallengeIdentity"/> class.
    ///   Конструктор
    /// </summary>
    /// <param name="name">
    /// Имя
    /// </param>
    /// <param name="password">
    /// Пароль
    /// </param>
    public ChallengeIdentity(string name, string password)
    {
      this.name = name;
      this.password = password;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Тип
    /// </summary>
    public string AuthenticationType
    {
      get
      {
        return "Challenge";
      }
    }

    /// <summary>
    ///   Прошел ли проверку
    /// </summary>
    public bool IsAuthenticated
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Имя
    /// </summary>
    public string Name
    {
      get
      {
        return name;
      }
    }

    /// <summary>
    ///   Пароль
    /// </summary>
    public string Password
    {
      get
      {
        return password;
      }
    }

    #endregion
  }
}