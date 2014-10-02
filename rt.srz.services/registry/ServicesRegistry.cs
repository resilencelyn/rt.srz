// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The services registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.atl.model.interfaces.Service;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.Atlantiko;
  using rt.srz.services.Kladr;
  using rt.srz.services.NSI;
  using rt.srz.services.Security;
  using rt.srz.services.Smo;
  using rt.srz.services.Statement;
  using rt.srz.services.TF;
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
      ForSingletonOf<IKladrService>().Use<KladrGateInternal>();
      ForSingletonOf<IStatementService>().Use<StatementGateInternal>();
      ForSingletonOf<INsiService>().Use<NsiGateInternal>();
      ForSingletonOf<ISecurityService>().Use<SecurityGateInternal>();
      ForSingletonOf<ISmoService>().Use<SmoGateInternal>();
      ForSingletonOf<ITFService>().Use<TFGateInternal>();
      ForSingletonOf<IUecService>().Use<UecGateInternal>();
      ForSingletonOf<IUirService>().Use<UirGateInternal>();
      ForSingletonOf<IAtlService>().Use<AtlGate>();
    }

    #endregion
  }
}