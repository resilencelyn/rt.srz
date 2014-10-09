// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TfomsGateInternal.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Tfoms
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.core.services.aspects;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using User = rt.core.model.core.User;

  #endregion

  /// <summary>
  ///   The statement gate.
  /// </summary>
  public class TfomsGateInternal : InterceptedBase, ITfomsService
  {
    #region Fields

    /// <summary>
    ///   The service.
    /// </summary>
    private readonly ITfomsService Service = new TfomsService();

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The annulate twin.
    /// </summary>
    /// <param name="twinId">
    /// The twin id.
    /// </param>
    public void AnnulateTwin(Guid twinId)
    {
      InvokeInterceptors(() => Service.AnnulateTwin(twinId));
    }

    /// <summary>
    /// Удаляет ключ поиска
    /// </summary>
    /// <param name="keyTypeId">
    /// </param>
    public void DeleteSearchKeyType(Guid keyTypeId)
    {
      InvokeInterceptors(() => Service.DeleteSearchKeyType(keyTypeId));
    }

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId">
    /// </param>
    public void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId)
    {
      InvokeInterceptors(() => Service.DeleteTwinsCalculatedOnlyByGivenKey(keyId));
    }

    /// <summary>
    /// Возвращает сортированный список периодов в которых запускались пакетные операции экспорта в СМО для указаннго
    ///   отправителя либо получателя
    /// </summary>
    /// <param name="senderId">
    /// The sender Id.
    /// </param>
    /// <param name="receiverId">
    /// The receiver Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId)
    {
      return InvokeInterceptors(() => Service.GetExportSmoBatchPeriodList(senderId, receiverId));
    }

    /// <summary>
    /// Возвращает все глобальные УЭК сертификаты
    /// </summary>
    /// <param name="batchId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<SertificateUec> GetGlobalSertificates()
    {
      return InvokeInterceptors(() => Service.GetGlobalSertificates());
    }

    /// <summary>
    ///   Возвращает все батчи относящиеся к пфр
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Batch> GetPfrBatchesByUser()
    {
      return InvokeInterceptors(() => Service.GetPfrBatchesByUser());
    }

    /// <summary>
    ///   ВОзвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Period> GetPfrPeriods()
    {
      return InvokeInterceptors(() => Service.GetPfrPeriods());
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
      return InvokeInterceptors(() => Service.GetPfrStatisticInfoByBatch(batchId));
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
      return InvokeInterceptors(() => Service.GetPfrStatisticInfoByPeriod(periodId));
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
      return InvokeInterceptors(() => Service.GetSearchKeyType(keyTypeId));
    }

    /// <summary>
    ///   Возвращает описатели всех ключей поиска для указанного ТФОМС
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<SearchKeyType> GetSearchKeyTypesByTFoms()
    {
      return InvokeInterceptors(() => Service.GetSearchKeyTypesByTFoms());
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
      return InvokeInterceptors(() => Service.GetTwin(id));
    }

    /// <summary>
    ///   Получает все дубликаты
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Twin> GetTwins()
    {
      return InvokeInterceptors(() => Service.GetTwins());
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
      return InvokeInterceptors(() => Service.GetTwins(criteria));
    }

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns>
    ///   The <see cref="IList" />.
    /// </returns>
    public IList<User> GetUsersByCurrent()
    {
      return InvokeInterceptors(() => Service.GetUsersByCurrent());
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
      InvokeInterceptors(() => Service.JoinTwins(twinId, mainInsuredPersonId, secondInsuredPersonId));
    }

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId">
    /// </param>
    public void MarkBatchAsUnexported(Guid batchId)
    {
      InvokeInterceptors(() => Service.MarkBatchAsUnexported(batchId));
    }

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="Id">
    /// </param>
    public void RemoveTwin(Guid Id)
    {
      InvokeInterceptors(() => Service.RemoveTwin(Id));
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
      return InvokeInterceptors(() => Service.SaveSearchKeyType(keyType));
    }

    /// <summary>
    /// Осуществляет поиск пакетных операций экспорта заявлений для СМО
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
    /// </returns>
    public SearchResult<SearchBatchResult> SearchExportSmoBatches(SearchExportSmoBatchCriteria criteria)
    {
      return InvokeInterceptors(() => Service.SearchExportSmoBatches(criteria));
    }

    /// <summary>
    /// Разделение
    /// </summary>
    /// <param name="personId">
    /// </param>
    /// <param name="statementsToSeparate">
    /// </param>
    /// <param name="copyDeadInfo">
    /// The copy Dead Info.
    /// </param>
    /// <param name="status">
    /// The status.
    /// </param>
    public void Separate(Guid personId, IList<Statement> statementsToSeparate, bool copyDeadInfo, int status)
    {
      InvokeInterceptors(() => Service.Separate(personId, statementsToSeparate, copyDeadInfo, status));
    }

    #endregion
  }
}