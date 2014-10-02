// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChallengePrincipal.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Принципал
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.challenge
{
  #region references

  using System.Linq;
  using System.Security.Principal;
  using System.Threading;

  using rt.core.business.security.interfaces;

  using StructureMap;

  #endregion

  /// <summary>
  ///   Принципал
  /// </summary>
  public class ChallengePrincipal : IPrincipal
  {
    #region Fields

    /// <summary>
    ///   Удостоверение
    /// </summary>
    private readonly IIdentity identity;

    /// <summary>
    ///   Роли
    /// </summary>
    private string[] roles;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ChallengePrincipal"/> class. 
    /// Конструктор
    /// </summary>
    /// <param name="identity">
    /// Удостоверение
    /// </param>
    public ChallengePrincipal(IIdentity identity)
    {
      this.identity = identity;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Текущий
    /// </summary>
    public static ChallengePrincipal Current
    {
      get
      {
        return Thread.CurrentPrincipal as ChallengePrincipal;
      }
    }

    /// <summary>
    ///   Удостоверение
    /// </summary>
    public IIdentity Identity
    {
      get
      {
        return identity;
      }
    }

    /// <summary>
    ///   Роли
    /// </summary>
    public string[] Roles
    {
      get
      {
        EnsureRoles();
        return roles;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Находится ли в роли
    /// </summary>
    /// <param name="role">
    /// Роль
    /// </param>
    /// <returns>
    /// Находится ли там
    /// </returns>
    public bool IsInRole(string role)
    {
      EnsureRoles();
      return roles != null && roles.Contains(role);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Проверка ролей
    /// </summary>
    protected virtual void EnsureRoles()
    {
      var userRepository = ObjectFactory.GetInstance<ISecurityProvider>();
      roles = userRepository.GetUserRoles(identity.Name);
    }

    #endregion
  }
}