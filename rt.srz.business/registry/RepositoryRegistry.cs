// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   регистр SM - инициализация всех репозиториев
//   Scope - синглтон
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.registry
{
  #region references

  using rt.core.business.interfaces.directorywatcher;
  using rt.core.business.interfaces.exchange;
  using rt.core.business.server.exchange.export;
  using rt.srz.business.exchange.export.pfr;
  using rt.srz.business.exchange.export.smo;
  using rt.srz.business.exchange.import.zags;
  using rt.srz.business.interfaces.logicalcontrol;
  using rt.srz.business.manager.logicalcontrol;
  using rt.srz.business.manager.rightedit;
  using rt.srz.model.HL7.pfr;
  using rt.srz.model.HL7.smo;
  using rt.srz.model.interfaces;

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
      Scan(
           s =>
           {
             s.TheCallingAssembly();
             s.IgnoreStructureMapAttributes();

             ////s.ExcludeNamespace("");
             s.IncludeNamespace("rt.srz.business.manager");
             s.WithDefaultConventions().OnAddedPluginTypes(t => t.Singleton());
           });

      Scan(
           s =>
           {
             s.TheCallingAssembly();
             s.IgnoreStructureMapAttributes();
             s.IncludeNamespace("rt.srz.business.manager.cache");
             s.WithDefaultConventions().OnAddedPluginTypes(t => t.Singleton());
           });

      // Синглтоны
      ForSingletonOf<ICheckManager>().Use<CheckStatementManager>();
      ForSingletonOf<IStatementRightToEditManager>().Use<StatementRightToEditManager>();

      Scan(
           s =>
           {
             s.TheCallingAssembly();
             s.AddAllTypesOf<ICheckStatement>();
             s.AddAllTypesOf<IStatementRightToEdit>();
             s.AddAllTypesOf<IImporterFile>();
           });

      // альтернатива для ForSingletonOf<IZagsImportFactory>().Use<ZagsImportFactory>();
      ForConcreteType<ZagsImportFactory>();

      // ForSingletonOf<IImporterFile>().Use<ImporterFileSmoOrganisation>();
      Scan(
           s =>
           {
             s.TheCallingAssembly();
             s.AddAllTypesOf<IImporterFile>();
             s.AddAllTypesOf<IZagsImporter>();
           });

      ForSingletonOf<IExportBatchFactory<SnilsZlListAtr, string>>().Use<ExportBatchFactory<SnilsZlListAtr, string>>();
      ForSingletonOf<IExportBatchFactory<RECListType, RECType>>().Use<ExportBatchFactory<RECListType, RECType>>();

      // ForSingletonOf<IExportBatchFactory<OPListType, OPType>>().Use<ExportBatchFactory<OPListType, OPType>>();
      // ForSingletonOf<IExportBatchFactory<REPListType, REPType>>().Use<ExportBatchFactory<REPListType, REPType>>();
      // ForSingletonOf<IExportBatchFactory<PFLKType, PRType>>().Use<ExportBatchFactory<PFLKType, PRType>>();
      For<IExportBatchTyped<SnilsZlListAtr, string>>().Add<ExportBatchPfr>();
      For<IExportBatchTyped<RECListType, RECType>>().Add<ExportBatchSmoRec>();

      // For<IExportBatchTyped<OPListType, OPType>>().Add<ExportBatchSmoOp>();
      // For<IExportBatchTyped<REPListType, REPType>>().Add<ExportBatchSmoRep>();
      // For<IExportBatchTyped<PFLKType, PRType>>().Add<ExportBatchSmoFlk>();
    }

    #endregion
  }
}