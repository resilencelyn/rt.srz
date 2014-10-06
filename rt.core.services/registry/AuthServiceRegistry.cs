// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthServiceRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   регистр
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.registry
{
  #region references

  using rt.core.model.security;

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   регистр
  /// </summary>
  public class AuthServiceRegistry : Registry
  {
    ////ServiceRegistryBase<IAuthService, AuthService, AuthGate>
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="AuthServiceRegistry" /> class.
    ///   Конструктор
    /// </summary>
    public AuthServiceRegistry()
    {
      ForSingletonOf<IAuthService>().Use<AuthGate>();
    }

    #endregion
  }
}