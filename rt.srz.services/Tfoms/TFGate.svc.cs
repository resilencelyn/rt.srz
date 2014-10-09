using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using rt.core.services.aspects;
using rt.core.services.nhibernate;
using rt.core.services.wcf;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.model.dto;
using rt.core.dto;

namespace rt.srz.services.TF
{
  /// <summary>
  /// The statement gate.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  public class TFGate : InterceptedBase, ITFService
  {
    /// <summary>
    ///   The service.
    /// </summary>
    private readonly ITFService Service = new TFService();

    /// <summary>
    /// Возвращает описатели всех ключей поиска для указанного ТФОМС 
    /// </summary>
    /// <returns></returns>
    public IList<SearchKeyType> GetSearchKeyTypesByTFoms()
    {
      return InvokeInterceptors(() => Service.GetSearchKeyTypesByTFoms());
    }

    /// <summary>
    /// Возвращает описатель ключа поиска
    /// </summary>
    /// <param name="keyTypeId"></param>
    /// <returns></returns>
    public SearchKeyType GetSearchKeyType(Guid keyTypeId)
    {
      return InvokeInterceptors(() => Service.GetSearchKeyType(keyTypeId));
    }

    /// <summary>
    /// Сохраняет ключ поиска
    /// </summary>
    /// <param name="keyType"></param>
    /// <returns></returns>
    public Guid SaveSearchKeyType(SearchKeyType keyType)
    {
      return InvokeInterceptors(() => Service.SaveSearchKeyType(keyType));
    }

    /// <summary>
    /// Удаляет ключ поиска
    /// </summary>
    /// <param name="keyTypeId"></param>
    public void DeleteSearchKeyType(Guid keyTypeId)
    {
      InvokeInterceptors(() => Service.DeleteSearchKeyType(keyTypeId));
    }

    /// <summary>
    /// Получает все дубликаты
    /// </summary>
    /// <returns></returns>
    public IList<Twin> GetTwins()
    {
      return InvokeInterceptors(() => Service.GetTwins());
    }

    /// <summary>
    /// Получает дубликат
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Twin GetTwin(Guid id)
    {
      return InvokeInterceptors(() => Service.GetTwin(id));
    }

    /// <summary>
    /// Дубликаты по критерию для разбивки постранично
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public SearchResult<Twin> GetTwins(SearchTwinCriteria criteria)
    {
      return InvokeInterceptors(() => Service.GetTwins(criteria));
    }

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="Id"></param>
    public void RemoveTwin(Guid Id)
    {
      InvokeInterceptors(() => Service.RemoveTwin(Id));
    }

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId"></param>
    public void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId)
    {
      InvokeInterceptors(() => Service.DeleteTwinsCalculatedOnlyByGivenKey(keyId));
    }

    /// <summary>
    /// Объединяет дубликаты
    /// </summary>
    /// <param name="twinId"></param>
    /// <param name="mainInsuredPersonId"></param>
    /// <param name="secondInsuredPersonId"></param>
    public void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId)
    {
      InvokeInterceptors(() => Service.JoinTwins(twinId, mainInsuredPersonId, secondInsuredPersonId));
    }

    /// <summary>
    /// Аннулирование дубликата
    /// </summary>
    /// <param name="twinId"></param>
    public void AnnulateTwin(Guid twinId)
    {
      InvokeInterceptors(() => Service.AnnulateTwin(twinId));
    }

    /// <summary>
    /// Разделение ранее объединённых людей
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="mainPersonId"></param>
    public void SeparatePersons(Guid personId, Guid mainPersonId)
    {
      InvokeInterceptors(() => Service.SeparatePersons(personId, mainPersonId));
    }

    /// <summary>
    /// Возвращает следующий номер ЕНП
    /// </summary>
    /// <param name="tfomsId"></param>
    /// <param name="genderId"></param>
    /// <param name="birthday"></param>
    /// <returns></returns>
    public string GetNextENPNumber(Guid tfomsId, int genderId, DateTime birthday)
    {
      return InvokeInterceptors(() => Service.GetNextENPNumber(tfomsId, genderId, birthday));
    }

    /// <summary>
    /// Возвращает следующий номер ЕНП
    /// </summary>
    /// <param name="tfomsId"></param>
    /// <param name="genderId"></param>
    /// <param name="birthday"></param>
    /// <returns></returns>
    public string GetNextENPNumber(Guid tfomsId, int genderId, int year, int month, int day)
    {
      return InvokeInterceptors(() => Service.GetNextENPNumber(tfomsId, genderId, year, month, day));
    }

    /// <summary>
    /// Возвращает все батчи относящиеся к пфр
    /// </summary>
    /// <returns></returns>
    public IList<Batch> GetPfrBatches()
    {
      return InvokeInterceptors(() => Service.GetPfrBatches());
    }

    /// <summary>
    /// ВОзвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns></returns>
    public IList<Period> GetPfrPeriods()
    {
      return InvokeInterceptors(() => Service.GetPfrPeriods());
    }

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="batchId"></param>
    /// <returns></returns>
    public PfrStatisticInfo GetPfrStatisticInfoByBatch(Guid batchId)
    {
      return InvokeInterceptors(() => Service.GetPfrStatisticInfoByBatch(batchId));
    }

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="periodId"></param>
    /// <returns></returns>
    public PfrStatisticInfo GetPfrStatisticInfoByPeriod(Guid periodId)
    {
      return InvokeInterceptors(() => Service.GetPfrStatisticInfoByPeriod(periodId));
    }

  }
}
