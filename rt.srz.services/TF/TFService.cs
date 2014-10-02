// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TFService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The tf service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.TF
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;
  using NHibernate.Criterion;

  using rt.core.business.security.interfaces;
  using rt.core.model.dto;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.dto;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The tf service.
  /// </summary>
  public class TFService : ITFService
  {
    #region Public Methods and Operators

    /// <summary>
    /// The annulate twin.
    /// </summary>
    /// <param name="twinId">
    /// The twin id.
    /// </param>
    public void AnnulateTwin(Guid twinId)
    {
      ObjectFactory.GetInstance<ITwinManager>().AnnulateTwin(twinId);
    }

    /// <summary>
    /// Удаляет ключ поиска
    /// </summary>
    /// <param name="keyTypeId">
    /// </param>
    public void DeleteSearchKeyType(Guid keyTypeId)
    {
      ObjectFactory.GetInstance<ISearchKeyTypeManager>().DeleteSearchKeyType(keyTypeId);
    }

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId">
    /// </param>
    public void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId)
    {
      ObjectFactory.GetInstance<ITwinManager>().DeleteTwinsCalculatedOnlyByGivenKey(keyId);
    }

    /// <summary>
    ///   Возвращает все батчи относящиеся к пфр
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Batch> GetPfrBatchesByUser()
    {
      return ObjectFactory.GetInstance<IBatchManager>().GetPfrBatchesByUser();
    }

    /// <summary>
    ///   Возвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Period> GetPfrPeriods()
    {
      return ObjectFactory.GetInstance<IBatchManager>().GetPfrPeriods();
    }

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="batchId">
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> . 
    /// </returns>
    public PfrStatisticInfo GetPfrStatisticInfoByBatch(Guid batchId)
    {
      return ObjectFactory.GetInstance<IBatchManager>().GetPfrStatisticInfoByBatch(batchId);
    }

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="periodId">
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> . 
    /// </returns>
    public PfrStatisticInfo GetPfrStatisticInfoByPeriod(Guid periodId)
    {
      return ObjectFactory.GetInstance<IBatchManager>().GetPfrStatisticInfoByPeriod(periodId);
    }

    /// <summary>
    /// Возвращает описатель ключа поиска
    /// </summary>
    /// <param name="keyTypeId">
    /// </param>
    /// <returns>
    /// The <see cref="SearchKeyType"/> . 
    /// </returns>
    public SearchKeyType GetSearchKeyType(Guid keyTypeId)
    {
      return ObjectFactory.GetInstance<ISearchKeyTypeManager>().GetById(keyTypeId);
    }

    /// <summary>
    ///   Возвращает описатели всех ключей поиска для указанного ТФОМС
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<SearchKeyType> GetSearchKeyTypesByTFoms()
    {
      return ObjectFactory.GetInstance<ISearchKeyTypeManager>().GetSearchKeyTypesByTFoms();
    }

    /// <summary>
    /// Получает дубликат
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <returns>
    /// The <see cref="Twin"/> . 
    /// </returns>
    public Twin GetTwin(Guid id)
    {
      return ObjectFactory.GetInstance<ITwinManager>().GetById(id);
    }

    /// <summary>
    ///   Получает все дубликаты
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Twin> GetTwins()
    {
      return ObjectFactory.GetInstance<ITwinManager>().GetTwins();
    }

    /// <summary>
    /// Дубликаты по критерию для разбивки постранично
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> . 
    /// </returns>
    public SearchResult<Twin> GetTwins(SearchTwinCriteria criteria)
    {
      return ObjectFactory.GetInstance<ITwinManager>().GetTwins(criteria);
    }

    /// <summary>
    /// Объединяет дубликаты
    /// </summary>
    /// <param name="twinId">
    /// </param>
    /// <param name="mainInsuredPersonId">
    /// </param>
    /// <param name="secondInsuredPersonId">
    /// </param>
    public void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId)
    {
      ObjectFactory.GetInstance<ITwinManager>().JoinTwins(twinId, mainInsuredPersonId, secondInsuredPersonId);
    }

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="Id">
    /// </param>
    public void RemoveTwin(Guid Id)
    {
      ObjectFactory.GetInstance<ITwinManager>().RemoveTwin(Id);
    }

    /// <summary>
    /// Сохраняет ключ поиска
    /// </summary>
    /// <param name="keyType">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    public Guid SaveSearchKeyType(SearchKeyType keyType)
    {
      return ObjectFactory.GetInstance<ISearchKeyTypeManager>().SaveSearchKeyType(keyType);
    }

    /// <summary>
    /// Разделение
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="statementsToSeparate"></param>
    public void Separate(Guid personId, IList<Statement> statementsToSeparate, bool copyDeadInfo, int status)
    {
      ObjectFactory.GetInstance<ITwinManager>().Separate(personId, statementsToSeparate, copyDeadInfo, status);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Осуществляет поиск пакетных операций экспорта заявлений для СМО
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public SearchResult<SearchBatchResult> SearchExportSmoBatches(SearchExportSmoBatchCriteria criteria)
    {
      return ObjectFactory.GetInstance<IBatchManager>().SearchExportSmoBatches(criteria);
    }

    /// <summary>
    /// Возвращает сортированный список периодов в которых запускались пакетные операции экспорта в СМО для указаннго отправителя либо получателя
    /// </summary>
    /// <returns></returns>
    public IList<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId)
    {
      return ObjectFactory.GetInstance<IPeriodManager>().GetExportSmoBatchPeriodList(senderId, receiverId);
    }

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId"></param>
    public void MarkBatchAsUnexported(Guid batchId)
    {
      ObjectFactory.GetInstance<IBatchManager>().MarkBatchAsUnexported(batchId);
    }

    /// <summary>
    ///  Возвращает все глобальные УЭК сертификаты
    /// </summary>
    /// <param name="batchId"></param>
    /// <returns></returns>
    public IList<SertificateUec> GetGlobalSertificates()
    { 
      return ObjectFactory.GetInstance<ISertificateUecManager>().GetBy(x => x.Smo.Id == null && x.IsActive && x.Workstation.Id == null);
    }

    #endregion
  }
}