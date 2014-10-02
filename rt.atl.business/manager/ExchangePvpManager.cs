// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangePvpManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The ExchangePvpManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.manager
{
  using StructureMap;

  using rt.core.business.nhibernate;

  /// <summary>
  ///   The ExchangePvpManager.
  /// </summary>
  public partial class ExchangePvpManager
  {
    /// <summary>
    /// The flag exported prz buff.
    /// </summary>
    public void FlagExportedPrzBuff()
    {
      var sessionFactorySrz = ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml");
      using (var session = sessionFactorySrz.OpenSession())
      {
        var queryString = @"
INSERT INTO [ExchangePvp]
           ([PrzBuffId]
           ,[StatementId]
           ,[IsExport]
           ,[Error])
 (
 select ID,
        '00000000-0000-0000-0000-000000000000',
        1,
        null
 from PRZBUF b
  left join [ExchangePvp] e on e.[PrzBuffId] = b.ID
 where e.RowID is null and b.ST = 1
 )";

        session.CreateSQLQuery(queryString).SetTimeout(int.MaxValue).UniqueResult();
        session.Close();
      }
    }
  }
}