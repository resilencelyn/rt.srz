// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAtlService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The AtlGate interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// --------------------------------------------------------------------------------------------------------------------

// <copyright file="IAtlService.cs" company="Rintech">

// Copyright (c) 2013. All rights reserved.

// </copyright>

// <summary>

// The AtlGate interface.

// </summary>

// --------------------------------------------------------------------------------------------------------------------
namespace rt.atl.model.interfaces.Service
{
  using System.Collections.Generic;
  using System.ServiceModel;

  using Quartz;

  using rt.atl.model.dto;
  using rt.core.model.dto;

  /// <summary>
  ///   The AtlGate interface.
  /// </summary>
  [ServiceContract]
  public interface IAtlService
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The flag exported prz buff.
    /// </summary>
    [OperationContract]
    void FlagExportedPrzBuff();

    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
    /// </returns>
    [OperationContract]
    SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(
      SearchErrorSinchronizationCriteria criteria);

    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    [OperationContract]
    IList<StatisticInitialLoading> GetStatisticInitialLoading();

    /// <summary>
    /// The run export to pvp.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    [OperationContract]
    void RunExportToPvp(IJobExecutionContext context);

    /// <summary>
    /// The run export to srz.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    [OperationContract]
    void RunExportToSrz(IJobExecutionContext context);

    /// <summary>
    /// The run export to srz.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    [OperationContract]
    void RunSinhronizeNsi(IJobExecutionContext context);

    #endregion
  }
}