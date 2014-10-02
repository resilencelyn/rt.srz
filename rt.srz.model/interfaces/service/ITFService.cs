using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using rt.srz.model.srz;
using rt.srz.model.dto;

namespace rt.srz.model.interfaces.service
{
  using rt.core.model.dto;
  using rt.srz.model.srz.concepts;

  /// <summary>
  ///   The TFService interface.
  /// </summary>
  [ServiceContract]
  public interface ITFService
  {
    /// <summary>
    /// Возвращает описатели всех ключей поиска для указанного ТФОМС 
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    IList<SearchKeyType> GetSearchKeyTypesByTFoms();

    /// <summary>
    /// Возвращает описатель ключа поиска
    /// </summary>
    /// <param name="keyTypeId"></param>
    /// <returns></returns>
    [OperationContract]
    SearchKeyType GetSearchKeyType(Guid keyTypeId);

    /// <summary>
    /// Сохраняет ключ поиска
    /// </summary>
    /// <param name="keyType"></param>
    /// <returns></returns>
    [OperationContract]
    Guid SaveSearchKeyType(SearchKeyType keyType);

    /// <summary>
    /// Удаляет ключ поиска
    /// </summary>
    /// <param name="keyTypeId"></param>
    [OperationContract]
    void DeleteSearchKeyType(Guid keyTypeId);

    /// <summary>
    /// Получает все дубликаты
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    IList<Twin> GetTwins();

    /// <summary>
    /// Получает дубликат
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [OperationContract]
    Twin GetTwin(Guid id);

    /// <summary>
    /// Дубликаты по критерию для разбивки постранично
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    [OperationContract(Name = "GetTwinsBy")]
    SearchResult<Twin> GetTwins(SearchTwinCriteria criteria);

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="Id"></param>
    [OperationContract]
    void RemoveTwin(Guid Id);

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId"></param>
    [OperationContract]
    void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId);

    /// <summary>
    /// Объединяет дубликаты
    /// </summary>
    /// <param name="twinId"></param>
    /// <param name="mainInsuredPersonId"></param>
    /// <param name="secondInsuredPersonId"></param>
    [OperationContract]
    void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId);

    /// <summary>
    /// Аннулирование дубликата
    /// </summary>
    /// <param name="twinId"></param>
    [OperationContract]
    void AnnulateTwin(Guid twinId);

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
    void Separate(Guid personId, IList<Statement> statementsToSeparate, bool copyDeadInfo = true, int status = StatusPerson.Active);

    /// <summary>
    /// Возвращает все батчи относящиеся к пфр
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    IList<Batch> GetPfrBatchesByUser();

    /// <summary>
    /// ВОзвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    IList<Period> GetPfrPeriods();

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="batchId"></param>
    /// <returns></returns>
    [OperationContract]
    PfrStatisticInfo GetPfrStatisticInfoByBatch(Guid batchId);

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="periodId"></param>
    /// <returns></returns>
    [OperationContract]
    PfrStatisticInfo GetPfrStatisticInfoByPeriod(Guid periodId);

    /// <summary>
    /// Осуществляет поиск пакетных операций экспорта заявлений для СМО
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    [OperationContract]
    SearchResult<SearchBatchResult> SearchExportSmoBatches(SearchExportSmoBatchCriteria criteria);

    /// <summary>
    /// Возвращает сортированный список периодов в которых запускались пакетные операции экспорта в СМО для указаннго отправителя либо получателя
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    IList<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId);

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId"></param>
    [OperationContract]
    void MarkBatchAsUnexported(Guid batchId);

    /// <summary>
    ///  Возвращает все глобальные УЭК сертификаты
    /// </summary>
    /// <param name="batchId"></param>
    /// <returns></returns>
    [OperationContract]
    IList<SertificateUec> GetGlobalSertificates();
  }
}
