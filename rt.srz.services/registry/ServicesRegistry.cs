// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesRegistry.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The services registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.atl.model.interfaces.Service;
  using rt.core.model.interfaces;
  using rt.core.services;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.Atlantiko;
  using rt.srz.services.Regulatory;
  using rt.srz.services.Statement;
  using rt.srz.services.Tfoms;
  using rt.srz.services.Uec;
  using rt.srz.services.Uir;
  using rt.uec.model.Interfaces;

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   The services registry.
  /// </summary>
  public class ServicesRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServicesRegistry" /> class.
    /// </summary>
    public ServicesRegistry()
    {
      ForSingletonOf<IStatementService>().Use<StatementGateInternal>();
      ForSingletonOf<IRegulatoryService>().Use<RegulatoryGateInternal>();
      ForSingletonOf<ISecurityService>().Use<SecurityGateInternal>();
      ForSingletonOf<ITfomsService>().Use<TfomsGateInternal>();
      ForSingletonOf<IUecService>().Use<UecGateInternal>();
      ForSingletonOf<IUirService>().Use<UirGateInternal>();
      ForSingletonOf<IAtlService>().Use<AtlGateInternal>();
    }

    #endregion
  }
}