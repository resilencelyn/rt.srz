//-------------------------------------------------------------------------------------
// <copyright file="PeriodManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using NHibernate;
using NHibernate.Criterion;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace rt.srz.business.manager
{
  using System;
  using System.Linq;

  using rt.core.business.security.interfaces;
  using rt.srz.business.manager.cache;
  using rt.srz.model.srz;

  /// <summary>
  /// The PeriodManager.
  /// </summary>
  public partial class PeriodManager
  {
    /// <summary>
    /// The get period by month.
    /// </summary>
    /// <param name="date">
    /// The date.
    /// </param>
    /// <returns>
    /// The <see cref="Period"/>.
    /// </returns>
    public Period GetPeriodByMonth(DateTime date)
    {
      var periodCodeId = 573 + date.Month;
      var year = new DateTime(date.Year, 1, 1);

      var period = GetBy(x => x.Year == year && x.Code.Id == periodCodeId).FirstOrDefault();
      if (period == null)
      {
        var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
        period = new Period
          {
            Code = conceptManager.GetById(periodCodeId),
            Year = year
          };
        ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(period);
      }

      return period;
    }

   /// <summary>
    /// Возвращает сортированный список периодов в которых запускались пакетные операции экспорта в СМО для указаннго отправителя либо получателя
    /// </summary>
    /// <returns></returns>
    public IList<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId)
    {
      // Выбираем батчи
      var subQuery = QueryOver.Of<Batch>().
        Where(x => x.Subject.Id == TypeSubject.Smo && x.Type.Id == TypeFile.Rec)
        .Select(x => x.Period.Id);

      // Выбираем по отправителю
      if (senderId != null && senderId != Guid.Empty)
        subQuery.Where(x => x.Sender.Id == senderId);

      // Выбираем по получателю
      if (receiverId != null && receiverId != Guid.Empty)
        subQuery.Where(x => x.Receiver.Id == receiverId);

      // Выбираем периоды
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var result = session.QueryOver<Period>()
        .WithSubquery.WhereProperty(x => x.Id).In(subQuery)
        .OrderBy(x => x.Year).Desc
        .ThenBy(x => x.Code).Desc
        .List();

      return result;
    }
  }
}