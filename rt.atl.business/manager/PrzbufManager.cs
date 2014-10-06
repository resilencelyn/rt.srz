// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrzbufManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The PrzbufManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.manager
{
  using NHibernate.Transform;

  using rt.atl.model.atl;
  using rt.atl.model.dto;
  using rt.core.business.nhibernate;
  using rt.core.model.dto;

  using StructureMap;

  /// <summary>
  ///   The PrzbufManager.
  /// </summary>
  public partial class PrzbufManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
    /// </returns>
    public SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(
      SearchErrorSinchronizationCriteria criteria)
    {
      var factory = ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml");
      var session = factory.OpenSession();

      ExchangePvp exch = null;
      Przlog log = null;
      ErrorSinchronizationInfoResult resultData = null;

      var query =
        session.QueryOver<Przbuf>()
               .JoinAlias(p => p.ExchangePvps, () => exch)
               .JoinAlias(p => p.PRZLOG, () => log)
               .Where(p => exch.IsExport == false)
               .WhereRestrictionOn(x => exch.Error)
               .IsNotNull;

      if (criteria.DateFrom.HasValue && criteria.DateTo.HasValue)
      {
        query = query.WhereRestrictionOn(x => log.Dtin).IsBetween(criteria.DateFrom).And(criteria.DateTo);
      }

      query = query.OrderBy(p => p.Fam).Asc;

      var count = query.RowCount();
      var result = new SearchResult<ErrorSinchronizationInfoResult> { Skip = criteria.Skip, Total = count };
      query.Skip(criteria.Skip).Take(criteria.Take);

      var rows =
        query.SelectList(
                         p =>
                         p.Select(x => exch.Error)
                          .WithAlias(() => resultData.Error)
                          .Select(x => x.Fam)
                          .WithAlias(() => resultData.Fam)
                          .Select(x => x.Ot)
                          .WithAlias(() => resultData.Ot)
                          .Select(x => x.Im)
                          .WithAlias(() => resultData.Im)
                          .Select(x => x.Docn)
                          .WithAlias(() => resultData.Docn)
                          .Select(x => x.Docs)
                          .WithAlias(() => resultData.Docs)
                          .Select(x => x.Enp)
                          .WithAlias(() => resultData.Enp)
                          .Select(x => x.Mr)
                          .WithAlias(() => resultData.Mr)
                          .Select(x => x.W)
                          .WithAlias(() => resultData.W)
                          .Select(x => x.Dr)
                          .WithAlias(() => resultData.Dr))
             .TransformUsing(Transformers.AliasToBean<ErrorSinchronizationInfoResult>())
             .List<ErrorSinchronizationInfoResult>();

      result.Rows = rows;
      session.Close();
      return result;
    }

    #endregion
  }
}