// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.registry
{
  #region references

  using System.Runtime.Remoting.Messaging;

  using rt.core.business.server.exchange.export;
  using rt.cs.business.request;
  using rt.srz.model.Hl7.person;

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   регистр SM - инициализация всех репозиториев
  ///   Scope - синглтон
  /// </summary>
  public class RepositoryRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="RepositoryRegistry" /> class.
    ///   Создает новый экземпляр <see cref="RepositoryRegistry" />.
    /// </summary>
    public RepositoryRegistry()
    {
      ForSingletonOf<IMessageFactory>().Use<MessageFactory>();

      Scan(
          s =>
          {
            s.TheCallingAssembly();
            s.AddAllTypesOf<IMessageExporter>();
          });
    }

    #endregion
  }
}