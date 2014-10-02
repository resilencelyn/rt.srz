// --------------------------------------------------------------------------------------------------------------------
// <copyright file="personManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The personManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NHibernate;
using NHibernate.Transform;
using rt.atl.model.atl;
using rt.atl.model.dto;
using rt.core.business.nhibernate;
using StructureMap;
using System.Collections.Generic;
namespace rt.atl.business.manager
{
  /// <summary>
  ///   The personManager.
  /// </summary>
  public partial class personManager
  {
    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns></returns>
    public IList<StatisticInitialLoading> GetStatisticInitialLoading()
    {
      var factory = ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml");
      var session = factory.OpenSession();

      StatisticInitialLoading stl = null;

      var result = session.QueryOver<person>()
         .SelectList(list =>
           list.SelectGroup(x => x.IsExported).WithAlias(() => stl.IsExported).
           SelectGroup(x => x.ExportError).WithAlias(() => stl.ExportError).
           SelectCount(x => x.Id).WithAlias(() => stl.Count)
           )
        .TransformUsing(Transformers.AliasToBean<StatisticInitialLoading>())
        .List<StatisticInitialLoading>();

      session.Close();
      return result;
    }
  }
}