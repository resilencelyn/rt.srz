// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
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
  using rt.core.model;
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
    /// �������� ������ ��� ������ �� �������
    /// </summary>
    /// <param name="periodId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Batch> GetPfrBatchesByPeriod(Guid periodId)
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (currentUser.PointDistributionPolicy != null && (currentUser.PointDistributionPolicy.Parent != null && currentUser.PointDistributionPolicy.Parent.Parent != null))
      {
        return ObjectFactory.GetInstance<IBatchManager>()
          .GetBy(x => x.Period.Id == periodId && x.Subject.Id == TypeSubject.Pfr && x.Receiver.Id == currentUser.PointDistributionPolicy.Parent.Parent.Id);
      }

      return new List<Batch>();
    }

    /// <summary>
    ///   The get pfr batches by user.
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Batch> GetPfrBatchesByUser()
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (currentUser.PointDistributionPolicy != null && (currentUser.PointDistributionPolicy.Parent != null && currentUser.PointDistributionPolicy.Parent.Parent != null))
      {
        return ObjectFactory.GetInstance<IBatchManager>()
          .GetBy(x => x.Subject.Id == TypeSubject.Pfr && x.Receiver.Id == currentUser.PointDistributionPolicy.Parent.Parent.Id);
      }

      return new List<Batch>();
    }

    /// <summary>
    /// �������� ���� ��� �� �����������
    /// </summary>
    /// <param name="batchId">
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
    /// ������������ ����� �������� �������� �������� ��������� ��� ���
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
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

      // �������� ���������� �������
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var queryCount = session.QueryOver<Batch>().JoinAlias(x => x.Period, () => period);
      queryCount = AddWhereCondition(criteria, queryCount);
      var count = queryCount.RowCount();

      // ��������� ������ �� ������
      var searchResult = new SearchResult<SearchBatchResult> { Skip = criteria.Skip, Total = count };
      var query =
        session.QueryOver<Batch>().JoinAlias(x => x.Messages, () => message).JoinAlias(x => x.Period, () => period).
          JoinAlias(x => x.Sender, () => sender).JoinAlias(x => x.Receiver, () => receiver).JoinAlias(
            () => period.Code, () => periodConcept).JoinAlias(x => x.CodeConfirm, () => codeConfirmConcept);
      query = AddWhereCondition(criteria, query);
      query.OrderBy(() => sender.ShortName).Asc.ThenBy(() => receiver.ShortName).Asc.ThenBy(() => period.Year).Asc.
        ThenBy(() => periodConcept.Code).Asc.ThenBy(x => x.Number).Asc.SelectList(
          y =>
          y.SelectGroup(x => x.Id).WithAlias(() => result.Id).SelectGroup(x => sender.ShortName).WithAlias(
            () => result.SenderName).SelectGroup(x => receiver.ShortName).WithAlias(() => result.ReceiverName).
            SelectGroup(x => period.Year).WithAlias(() => result.PeriodYear).SelectGroup(() => periodConcept.Code).
            WithAlias(() => result.PeriodMonth).SelectGroup(x => x.FileName).WithAlias(() => result.FileName).
            SelectGroup(x => x.Number).WithAlias(() => result.Number).SelectGroup(() => codeConfirmConcept.Code).
            WithAlias(() => result.CodeConfirm).SelectCount(x => message.Id).WithAlias(() => result.RecordCount)).
        TransformUsing(Transformers.AliasToBean<SearchBatchResult>()).Skip(criteria.Skip).Take(criteria.Take);

      // ��������� ������
      searchResult.Rows = query.List<SearchBatchResult>();
      return searchResult;
    }

    /// <summary>
    /// ���������� ���������� �� ���������� ���
    /// </summary>
    /// <param name="batchId">
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
    /// ���������� ���������� �� ���������� ���
    /// </summary>
    /// <param name="periodId">
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
    /// The get pfr statistic.
    /// </summary>
    /// <param name="batches">
    /// The batches. 
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> . 
    /// </returns>
    private PfrStatisticInfo GetPfrStatistic(Batch[] batches)
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
          var foundRecordCount = session.QueryOver<QueryResponse>()
            .JoinAlias(x => x.EmploymentHistories, () => employmentHistory)
            .Where(x => x.Message.Id == message.Id).RowCount();

          var insuredRecordCount = session.QueryOver<QueryResponse>()
            .JoinAlias(x => x.EmploymentHistories, () => employmentHistory)
            .JoinAlias(x => employmentHistory.InsuredPerson, () => person)
            .Where(x => x.Message.Id == message.Id)
            .And(x => person.Status.Id == StatusPerson.Active)
            .RowCount();

          var employedRecordCount = session.QueryOver<QueryResponse>()
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

    #region Methods

    /// <summary>
    /// ��������� � ������ ������ �� ������ �������
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
      SearchExportSmoBatchCriteria criteria, IQueryOver<Batch, Batch> query)
    {
      // �������� �� ������� 
      query.Where(x => x.Period.Id == criteria.PeriodId);

      // �������� �� �����������
      if (criteria.SenderId != null && criteria.SenderId != Guid.Empty)
      {
        query.Where(x => x.Sender.Id == criteria.SenderId);
      }

      // �������� �� ����������
      if (criteria.ReceiverId != null && criteria.ReceiverId != Guid.Empty)
      {
        query.Where(x => x.Receiver.Id == criteria.ReceiverId);
      }

      // �������� �� ������ �����
      if (criteria.BatchNumber != -1)
      {
        query.Where(x => x.Number == criteria.BatchNumber);
      }

      return query;
    }

    /// <summary>
    /// ���������� ��� ������� �� ������� ���� ������ �� ������ � ����� �������� ���
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Period> GetPfrPeriods()
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (currentUser.PointDistributionPolicy != null && (currentUser.PointDistributionPolicy.Parent != null && currentUser.PointDistributionPolicy.Parent.Parent != null))
      {
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var query = session.QueryOver<Period>()
          .JoinQueryOver<Batch>(p => p.Batches)
          .Where(b => b.Subject.Id == TypeSubject.Pfr && b.Receiver.Id == currentUser.PointDistributionPolicy.Parent.Parent.Id);
        return query.List().Distinct().ToList();
      }

      return new List<Period>();
    }

    #endregion
  }
}