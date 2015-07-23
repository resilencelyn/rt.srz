// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITfomsService.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The TFService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service
{
  using System;
  using System.Collections.Generic;
  using System.ServiceModel;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using User = rt.core.model.core.User;

  /// <summary>
  ///   The TFService interface.
  /// </summary>
  [ServiceContract]
  public interface ITfomsService
  {
    #region Public Methods and Operators

    /// <summary>
    /// The annulate twin.
    /// </summary>
    /// <param name="twinId">
    /// The twin id.
    /// </param>
    [OperationContract]
    void AnnulateTwin(Guid twinId);

    /// <summary>
    /// Удаляет ключ поиска
    /// </summary>
    /// <param name="keyTypeId">
    /// The key Type id.
    /// </param>
    [OperationContract]
    void DeleteSearchKeyType(Guid keyTypeId);

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId">
    /// The key id.
    /// </param>
    [OperationContract]
    void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId);

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
    [OperationContract]
    List<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId);

    /// <summary>
    ///   Возвращает все глобальные УЭК сертификаты
    /// </summary>
    /// <returns>
    ///   The <see cref="List{SertificateUec}" />.
    /// </returns>
    [OperationContract]
    List<SertificateUec> GetGlobalSertificates();

    /// <summary>
    ///   Возвращает все батчи относящиеся к пфр
    /// </summary>
    /// <returns> The <see cref="List{Batch}" /> . </returns>
    [OperationContract]
    List<Batch> GetPfrBatchesByUser();

    /// <summary>
    ///   Возвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns> The <see cref="List{Period}" /> . </returns>
    [OperationContract]
    List<Period> GetPfrPeriods();

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="batchId">
    /// The batch id.
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> .
    /// </returns>
    [OperationContract]
    PfrStatisticInfo GetPfrStatisticInfoByBatch(Guid batchId);

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="periodId">
    /// The period id.
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> .
    /// </returns>
    [OperationContract]
    PfrStatisticInfo GetPfrStatisticInfoByPeriod(Guid periodId);

    /// <summary>
    /// Возвращает описатель ключа поиска
    /// </summary>
    /// <param name="keyTypeId">
    /// The key Type id.
    /// </param>
    /// <returns>
    /// The <see cref="SearchKeyType"/> .
    /// </returns>
    [OperationContract]
    SearchKeyType GetSearchKeyType(Guid keyTypeId);

    /// <summary>
    ///   Возвращает описатели всех ключей поиска для указанного ТФОМС
    /// </summary>
    /// <returns> The <see cref="List{SearchKeyType}" /> . </returns>
    [OperationContract]
    List<SearchKeyType> GetSearchKeyTypesByTFoms();

    /// <summary>
    /// Получает дубликат
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Twin"/> .
    /// </returns>
    [OperationContract]
    Twin GetTwin(Guid id);

    /// <summary>
    /// Дубликаты по критерию для разбивки постранично
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{Twin}"/> .
    /// </returns>
    [OperationContract]
    SearchResult<Twin> GetTwins(SearchTwinCriteria criteria);

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="List{User}" /> . </returns>
    [OperationContract]
    IList<User> GetUsersByCurrent();

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
    [OperationContract]
    void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId);

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId">
    /// The batch id.
    /// </param>
    [OperationContract]
    void MarkBatchAsUnexported(Guid batchId);

    /// <summary>
    /// Удаляет настройку из базы которую надо стало проверять
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    [OperationContract]
    void RemoveSetting(string className);

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    [OperationContract]
    void RemoveTwin(Guid id);

    /// <summary>
    /// Добавляет в базу настройку проверки о том что её не надо проверять с учётом территориального фонда
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    [OperationContract]
    void SaveCheckSetting(string className);

    /// <summary>
    /// Сохраняет ключ поиска
    /// </summary>
    /// <param name="keyType">
    /// The key Type.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SaveSearchKeyType(SearchKeyType keyType);

    /// <summary>
    /// Осуществляет поиск пакетных операций экспорта заявлений для СМО
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{SearchBatchResult}"/>.
    /// </returns>
    [OperationContract]
    SearchResult<SearchBatchResult> SearchExportSmoBatches(SearchExportSmoBatchCriteria criteria);

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
    [OperationContract]
    void Separate(
      Guid personId, 
      List<Statement> statementsToSeparate, 
      bool copyDeadInfo = true, 
      int status = StatusPerson.Active);

    #endregion
  }
}