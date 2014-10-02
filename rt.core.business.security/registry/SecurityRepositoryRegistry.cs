// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityRepositoryRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Регистрация репозиториев
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.security.registry
{
  #region references

  using rt.core.business.security.interfaces;
  using rt.core.business.security.repository;

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   Регистрация репозиториев
  /// </summary>
  public class SecurityRepositoryRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SecurityRepositoryRegistry" /> class.
    ///   Конструктор
    /// </summary>
    public SecurityRepositoryRegistry()
    {
      ForSingletonOf<ISecurityProvider>().Use<SecurityProvider>();
    }

    #endregion
  }
}