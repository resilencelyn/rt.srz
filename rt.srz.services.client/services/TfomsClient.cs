// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TfomsClient.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.services
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.core.services.registry;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using User = rt.core.model.core.User;

  #endregion

  /// <summary>
  ///   The statement gate.
  /// </summary>
  public class TfomsClient : ServiceClient<ITfomsService>, ITfomsService
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
      InvokeInterceptors(() => Service.AnnulateTwin(twinId));
    }

    /// <summary>
    /// Удаляет ключ поиска
    /// </summary>
    /// <param name="keyTypeId">
    /// The key Type id.
    /// </param>
    public void DeleteSearchKeyType(Guid keyTypeId)
    {
      InvokeInterceptors(() => Service.DeleteSearchKeyType(keyTypeId));
    }

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId">
    /// The key id.
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
    /// The sender id.
    /// </param>
    /// <param name="receiverId">
    /// The receiver id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Period}"/>.
    /// </returns>
    public List<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId)
    {
      return InvokeInterceptors(() => Service.GetExportSmoBatchPeriodList(senderId, receiverId));
    }

    /// <summary>
    ///   Возвращает все глобальные УЭК сертификаты
    /// </summary>
    /// <returns>
    ///   The <see cref="List{SertificateUec}" />.
    /// </returns>
    public List<SertificateUec> GetGlobalSertificates()
    {
      return InvokeInterceptors(() => Service.GetGlobalSertificates());
    }

    /// <summary>
    ///   Возвращает все батчи относящиеся к пфр
    /// </summary>
    /// <returns> The <see cref="List{Batch}" /> . </returns>
    public List<Batch> GetPfrBatchesByUser()
    {
      return InvokeInterceptors(() => Service.GetPfrBatchesByUser());
    }

    /// <summary>
    ///   Возвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns> The <see cref="List{Period}" /> . </returns>
    public List<Period> GetPfrPeriods()
    {
      return InvokeInterceptors(() => Service.GetPfrPeriods());
    }

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="batchId">
    /// The batch id.
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
    /// The period id.
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
    /// The key Type id.
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
    /// <returns> The <see cref="List{SearchKeyType}" /> . </returns>
    public List<SearchKeyType> GetSearchKeyTypesByTFoms()
    {
      return InvokeInterceptors(() => Service.GetSearchKeyTypesByTFoms());
    }

    /// <summary>
    /// Получает дубликат
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Twin"/> .
    /// </returns>
    public Twin GetTwin(Guid id)
    {
      return InvokeInterceptors(() => Service.GetTwin(id));
    }

    /// <summary>
    /// Дубликаты по критерию для разбивки постранично
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{Twin}"/> .
    /// </returns>
    public SearchResult<Twin> GetTwins(SearchTwinCriteria criteria)
    {
      return InvokeInterceptors(() => Service.GetTwins(criteria));
    }

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="List{User}" /> . </returns>
    public IList<User> GetUsersByCurrent()
    {
      return InvokeInterceptors(() => Service.GetUsersByCurrent());
    }

    /// <summary>
    /// Объединяет дубликаты
    /// </summary>
    /// <param name="twinId">
    /// The twin id.
    /// </param>
    /// <param name="mainInsuredPersonId">
    /// The main Insured Person id.
    /// </param>
    /// <param name="secondInsuredPersonId">
    /// The second Insured Person id.
    /// </param>
    public void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId)
    {
      InvokeInterceptors(() => Service.JoinTwins(twinId, mainInsuredPersonId, secondInsuredPersonId));
    }

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId">
    /// The batch id.
    /// </param>
    public void MarkBatchAsUnexported(Guid batchId)
    {
      InvokeInterceptors(() => Service.MarkBatchAsUnexported(batchId));
    }

    /// <summary>
    /// Удаляет настройку из базы которую надо стало проверять
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    public void RemoveSetting(string className)
    {
      InvokeInterceptors(() => Service.RemoveSetting(className));
    }

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void RemoveTwin(Guid id)
    {
      InvokeInterceptors(() => Service.RemoveTwin(id));
    }

    /// <summary>
    /// Добавляет в базу настройку проверки о том что её не надо проверять с учётом территориального фонда
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    public void SaveCheckSetting(string className)
    {
      InvokeInterceptors(() => Service.SaveCheckSetting(className));
    }

    /// <summary>
    /// Сохраняет ключ поиска
    /// </summary>
    /// <param name="keyType">
    /// The key Type.
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
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{SearchBatchResult}"/>.
    /// </returns>
    public SearchResult<SearchBatchResult> SearchExportSmoBatches(SearchExportSmoBatchCriteria criteria)
    {
      return InvokeInterceptors(() => Service.SearchExportSmoBatches(criteria));
    }

    /// <summary>
    /// Разделение
    /// </summary>
    /// <param name="personId">
    /// The person Id.
    /// </param>
    /// <param name="statementsToSeparate">
    /// The statements To Separate.
    /// </param>
    /// <param name="copyDeadInfo">
    /// The copy Dead Info.
    /// </param>
    /// <param name="status">
    /// The status.
    /// </param>
    public void Separate(
      Guid personId, 
      List<Statement> statementsToSeparate, 
      bool copyDeadInfo = true, 
      int status = StatusPerson.Active)
    {
      InvokeInterceptors(() => Service.Separate(personId, statementsToSeparate, copyDeadInfo, status));
    }

    #endregion
  }
}