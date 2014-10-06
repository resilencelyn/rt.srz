// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The BatchManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;
  using NHibernate.Transform;

  using rt.core.business.security.interfaces;
  using rt.core.model.dto;
  using rt.srz.business.manager.cache;
  using rt.srz.model.dto;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The BatchManager.
  /// </summary>
  public partial class BatchManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Получает список пфр батчей по периоду
    /// </summary>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<Batch> GetPfrBatchesByPeriod(Guid periodId)
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (currentUser.HasTf())
      {
        return
          ObjectFactory.GetInstance<IBatchManager>()
                       .GetBy(
                              x =>
                              x.Period.Id == periodId && x.Subject.Id == TypeSubject.Pfr
                              && x.Receiver.Id == currentUser.GetTf().Id);
      }

      return new List<Batch>();
    }

    /// <summary>
    ///   The get pfr batches by user.
    /// </summary>
    /// <returns>
    ///   The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<Batch> GetPfrBatchesByUser()
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (currentUser.HasTf())
      {
        return
          ObjectFactory.GetInstance<IBatchManager>()
                       .GetBy(x => x.Subject.Id == TypeSubject.Pfr && x.Receiver.Id == currentUser.GetTf().Id);
      }

      return new List<Batch>();
    }

    /// <summary>
    ///   Возвращает все периоды на которые есть ссылки из батчей с типом субъекта пфр
    /// </summary>
    /// <returns>
    ///   The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<Period> GetPfrPeriods()
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (currentUser.HasTf())
      {
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var query =
          session.QueryOver<Period>()
                 .JoinQueryOver<Batch>(p => p.Batches)
                 .Where(b => b.Subject.Id == TypeSubject.Pfr && b.Receiver.Id == currentUser.GetTf().Id);
        return query.List().Distinct().ToList();
      }

      return new List<Period>();
    }

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> .
    /// </returns>
    public PfrStatisticInfo GetPfrStatisticInfoByBatch(Guid batchId)
    {
      var batch = GetById(batchId);
      return GetPfrStatistic(new[] { batch });
    }

    /// <summary>
    /// Возвращает информацию по статистике пфр
    /// </summary>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> .
    /// </returns>
    public PfrStatisticInfo GetPfrStatisticInfoByPeriod(Guid periodId)
    {
      var batches = GetPfrBatchesByPeriod(periodId);
      return GetPfrStatistic(batches.ToArray());
    }

    /// <summary>
    /// Помечает батч как не выгруженный
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    public void MarkBatchAsUnexported(Guid batchId)
    {
      var batch = GetById(batchId);
      if (batch != null)
      {
        batch.CodeConfirm = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(CodeConfirm.CA);
      }

      SaveOrUpdate(batch);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Осуществляет поиск пакетных операций экспорта заявлений для СМО
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>SearchResult</cref>
    ///   </see>
    ///   .
    /// </returns>
    public SearchResult<SearchBatchResult> SearchExportSmoBatches(SearchExportSmoBatchCriteria criteria)
    {
      SearchBatchResult result = null;
      Message message = null;
      Period period = null;
      Organisation sender = null;
      Organisation receiver = null;
      Concept periodConcept = null;
      Concept codeConfirmConcept = null;

      // Получаем количество записей
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var queryCount = session.QueryOver<Batch>().JoinAlias(x => x.Period, () => period);
      queryCount = AddWhereCondition(criteria, queryCount);
      var count = queryCount.RowCount();

      // Выполняем запрос по батчам
      var searchResult = new SearchResult<SearchBatchResult> { Skip = criteria.Skip, Total = count };
      var query =
        session.QueryOver<Batch>()
               .JoinAlias(x => x.Messages, () => message)
               .JoinAlias(x => x.Period, () => period)
               .JoinAlias(x => x.Sender, () => sender)
               .JoinAlias(x => x.Receiver, () => receiver)
               .JoinAlias(() => period.Code, () => periodConcept)
               .JoinAlias(x => x.CodeConfirm, () => codeConfirmConcept);
      query = AddWhereCondition(criteria, query);
      query.OrderBy(() => sender.ShortName)
           .Asc.ThenBy(() => receiver.ShortName)
           .Asc.ThenBy(() => period.Year)
           .Asc.ThenBy(() => periodConcept.Code)
           .Asc.ThenBy(x => x.Number)
           .Asc.SelectList(
                           y =>
                           y.SelectGroup(x => x.Id)
                            .WithAlias(() => result.Id)
                            .SelectGroup(x => sender.ShortName)
                            .WithAlias(() => result.SenderName)
                            .SelectGroup(x => receiver.ShortName)
                            .WithAlias(() => result.ReceiverName)
                            .SelectGroup(x => period.Year)
                            .WithAlias(() => result.PeriodYear)
                            .SelectGroup(() => periodConcept.Code)
                            .WithAlias(() => result.PeriodMonth)
                            .SelectGroup(x => x.FileName)
                            .WithAlias(() => result.FileName)
                            .SelectGroup(x => x.Number)
                            .WithAlias(() => result.Number)
                            .SelectGroup(() => codeConfirmConcept.Code)
                            .WithAlias(() => result.CodeConfirm)
                            .SelectCount(x => message.Id)
                            .WithAlias(() => result.RecordCount))
           .TransformUsing(Transformers.AliasToBean<SearchBatchResult>())
           .Skip(criteria.Skip)
           .Take(criteria.Take);

      // выполняем запрос
      searchResult.Rows = query.List<SearchBatchResult>();
      return searchResult;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Добавляет в запрос поиска по батчам условие
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <returns>
    /// The <see cref="IQueryOver"/>.
    /// </returns>
    private IQueryOver<Batch, Batch> AddWhereCondition(
      SearchExportSmoBatchCriteria criteria, 
      IQueryOver<Batch, Batch> query)
    {
      // Выбираем по периоду 
      query.Where(x => x.Period.Id == criteria.PeriodId);

      // Выбираем по отправителю
      if (criteria.SenderId != Guid.Empty)
      {
        query.Where(x => x.Sender.Id == criteria.SenderId);
      }

      // Выбираем по получателю
      if (criteria.ReceiverId != Guid.Empty)
      {
        query.Where(x => x.Receiver.Id == criteria.ReceiverId);
      }

      // Выбираем по номеру батча
      if (criteria.BatchNumber != -1)
      {
        query.Where(x => x.Number == criteria.BatchNumber);
      }

      return query;
    }

    /// <summary>
    /// The get pfr statistic.
    /// </summary>
    /// <param name="batches">
    /// The batches.
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> .
    /// </returns>
    private PfrStatisticInfo GetPfrStatistic(IEnumerable<Batch> batches)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var result = new PfrStatisticInfo();
      foreach (var batch in batches)
      {
        var message = batch.Messages.FirstOrDefault();
        if (message != null)
        {
          var countQr = session.QueryOver<QueryResponse>().Where(x => x.Message.Id == message.Id).RowCount();

          result.TotalRecordCount += countQr;

          EmploymentHistory employmentHistory = null;
          InsuredPerson person = null;
          var foundRecordCount =
            session.QueryOver<QueryResponse>()
                   .JoinAlias(x => x.EmploymentHistories, () => employmentHistory)
                   .Where(x => x.Message.Id == message.Id)
                   .RowCount();

          var insuredRecordCount =
            session.QueryOver<QueryResponse>()
                   .JoinAlias(x => x.EmploymentHistories, () => employmentHistory)
                   .JoinAlias(x => employmentHistory.InsuredPerson, () => person)
                   .Where(x => x.Message.Id == message.Id)
                   .And(x => person.Status.Id == StatusPerson.Active)
                   .RowCount();

          var employedRecordCount =
            session.QueryOver<QueryResponse>()
                   .JoinAlias(x => x.EmploymentHistories, () => employmentHistory)
                   .JoinAlias(x => employmentHistory.InsuredPerson, () => person)
                   .Where(x => x.Message.Id == message.Id)
                   .And(x => person.Status.Id == StatusPerson.Active)
                   .And(x => employmentHistory.Employment)
                   .RowCount();

          result.NotFoundRecordCount += countQr - foundRecordCount;
          result.InsuredRecordCount += insuredRecordCount;
          result.EmployedRecordCount += employedRecordCount;

          switch (batch.Type.Id)
          {
            case TypeFile.PfrData:
              result.FoundByDataRecordCount += foundRecordCount;
              break;
            case TypeFile.PfrSnils:
              result.FoundBySnilsRecordCount += foundRecordCount;
              break;
          }
        }
      }

      return result;
    }

    #endregion
  }
}