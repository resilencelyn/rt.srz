// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TfomsService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The tf service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Tfoms
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using rt.core.model.dto;
  using rt.srz.business.manager;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  using User = rt.core.model.core.User;

  #endregion

  /// <summary>
  ///   The tf service.
  /// </summary>
  public class TfomsService : ITfomsService
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
    /// The key Type id.
    /// </param>
    public void DeleteSearchKeyType(Guid keyTypeId)
    {
      ObjectFactory.GetInstance<ISearchKeyTypeManager>().DeleteSearchKeyType(keyTypeId);
    }

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId">
    /// The key id.
    /// </param>
    public void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId)
    {
      ObjectFactory.GetInstance<ITwinManager>().DeleteTwinsCalculatedOnlyByGivenKey(keyId);
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
      return ObjectFactory.GetInstance<IPeriodManager>().GetExportSmoBatchPeriodList(senderId, receiverId).ToList();
    }

    /// <summary>
    ///   Возвращает все глобальные УЭК сертификаты
    /// </summary>
    /// <returns>
    ///   The <see cref="List{SertificateUec}" />.
    /// </returns>
    public List<SertificateUec> GetGlobalSertificates()
    {
      return
        ObjectFactory.GetInstance<ISertificateUecManager>()
                     .GetBy(x => x.Smo.Id == null && x.IsActive && x.Workstation.Id == null)
                     .ToList();
    }

    /// <summary>
    ///   Возвращает все батчи относящиеся к пфр
    /// </summary>
    /// <returns> The <see cref="List{Batch}" /> . </returns>
    public List<Batch> GetPfrBatchesByUser()
    {
      return ObjectFactory.GetInstance<IBatchManager>().GetPfrBatchesByUser().ToList();
    }

    /// <summary>
    ///   Возвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns> The <see cref="List{Period}" /> . </returns>
    public List<Period> GetPfrPeriods()
    {
      return ObjectFactory.GetInstance<IBatchManager>().GetPfrPeriods().ToList();
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
      return ObjectFactory.GetInstance<IBatchManager>().GetPfrStatisticInfoByBatch(batchId);
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
      return ObjectFactory.GetInstance<IBatchManager>().GetPfrStatisticInfoByPeriod(periodId);
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
      return ObjectFactory.GetInstance<ISearchKeyTypeManager>().GetById(keyTypeId);
    }

    /// <summary>
    ///   Возвращает описатели всех ключей поиска для указанного ТФОМС
    /// </summary>
    /// <returns> The <see cref="List{SearchKeyType}" /> . </returns>
    public List<SearchKeyType> GetSearchKeyTypesByTFoms()
    {
      return ObjectFactory.GetInstance<ISearchKeyTypeManager>().GetSearchKeyTypesByTFoms().ToList();
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
      return ObjectFactory.GetInstance<ITwinManager>().GetById(id);
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
      return ObjectFactory.GetInstance<ITwinManager>().GetTwins(criteria);
    }

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="List{User}" /> . </returns>
    public IList<User> GetUsersByCurrent()
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetUsersByCurrent();
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
      ObjectFactory.GetInstance<ITwinManager>().JoinTwins(twinId, mainInsuredPersonId, secondInsuredPersonId);
    }

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId">
    /// The batch id.
    /// </param>
    public void MarkBatchAsUnexported(Guid batchId)
    {
      ObjectFactory.GetInstance<IBatchManager>().MarkBatchAsUnexported(batchId);
    }

    /// <summary>
    /// Удаляет настройку из базы которую надо стало проверять
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    public void RemoveSetting(string className)
    {
      ObjectFactory.GetInstance<ISettingManager>().RemoveSetting(className);
    }

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void RemoveTwin(Guid id)
    {
      ObjectFactory.GetInstance<ITwinManager>().RemoveTwin(id);
    }

    /// <summary>
    /// Добавляет в базу настройку проверки о том что её не надо проверять с учётом территориального фонда
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    public void SaveCheckSetting(string className)
    {
      ObjectFactory.GetInstance<ISettingManager>().SaveCheckSetting(className);
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
      return ObjectFactory.GetInstance<ISearchKeyTypeManager>().SaveSearchKeyType(keyType);
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
      return ObjectFactory.GetInstance<IBatchManager>().SearchExportSmoBatches(criteria);
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
      ObjectFactory.GetInstance<ITwinManager>().Separate(personId, statementsToSeparate, copyDeadInfo, status);
    }

    #endregion
  }
}