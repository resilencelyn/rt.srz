// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITFService.cs" company="РусБИТех">
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
  public interface ITFService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Аннулирование дубликата
    /// </summary>
    /// <param name="twinId">
    /// </param>
    [OperationContract]
    void AnnulateTwin(Guid twinId);

    /// <summary>
    /// Удаляет ключ поиска
    /// </summary>
    /// <param name="keyTypeId">
    /// </param>
    [OperationContract]
    void DeleteSearchKeyType(Guid keyTypeId);

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId">
    /// </param>
    [OperationContract]
    void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId);

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
    [OperationContract]
    IList<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId);

    /// <summary>
    /// Возвращает все глобальные УЭК сертификаты
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    [OperationContract]
    IList<SertificateUec> GetGlobalSertificates();

    /// <summary>
    /// Возвращает все батчи относящиеся к пфр
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    [OperationContract]
    IList<Batch> GetPfrBatchesByUser();

    /// <summary>
    /// ВОзвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    [OperationContract]
    IList<Period> GetPfrPeriods();

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="batchId">
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/>.
    /// </returns>
    [OperationContract]
    PfrStatisticInfo GetPfrStatisticInfoByBatch(Guid batchId);

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="periodId">
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/>.
    /// </returns>
    [OperationContract]
    PfrStatisticInfo GetPfrStatisticInfoByPeriod(Guid periodId);

    /// <summary>
    /// Возвращает описатель ключа поиска
    /// </summary>
    /// <param name="keyTypeId">
    /// </param>
    /// <returns>
    /// The <see cref="SearchKeyType"/>.
    /// </returns>
    [OperationContract]
    SearchKeyType GetSearchKeyType(Guid keyTypeId);

    /// <summary>
    /// Возвращает описатели всех ключей поиска для указанного ТФОМС
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    [OperationContract]
    IList<SearchKeyType> GetSearchKeyTypesByTFoms();

    /// <summary>
    /// Получает дубликат
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <returns>
    /// The <see cref="Twin"/>.
    /// </returns>
    [OperationContract]
    Twin GetTwin(Guid id);

    /// <summary>
    /// Получает все дубликаты
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    [OperationContract]
    IList<Twin> GetTwins();

    /// <summary>
    /// Дубликаты по критерию для разбивки постранично
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
    /// </returns>
    [OperationContract(Name = "GetTwinsBy")]
    SearchResult<Twin> GetTwins(SearchTwinCriteria criteria);

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="IList{User}" /> . </returns>
    [OperationContract]
    IList<User> GetUsersByCurrent();

    /// <summary>
    /// Объединяет дубликаты
    /// </summary>
    /// <param name="twinId">
    /// </param>
    /// <param name="mainInsuredPersonId">
    /// </param>
    /// <param name="secondInsuredPersonId">
    /// </param>
    [OperationContract]
    void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId);

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId">
    /// </param>
    [OperationContract]
    void MarkBatchAsUnexported(Guid batchId);

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="Id">
    /// </param>
    [OperationContract]
    void RemoveTwin(Guid Id);

    /// <summary>
    /// Сохраняет ключ поиска
    /// </summary>
    /// <param name="keyType">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    [OperationContract]
    Guid SaveSearchKeyType(SearchKeyType keyType);

    /// <summary>
    /// Осуществляет поиск пакетных операций экспорта заявлений для СМО
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
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
      IList<Statement> statementsToSeparate, 
      bool copyDeadInfo = true, 
      int status = StatusPerson.Active);

    #endregion
  }
}