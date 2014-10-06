// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangePvpManager.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ExchangePvpManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.manager
{
  using rt.core.business.nhibernate;

  using StructureMap;

  /// <summary>
  ///   The ExchangePvpManager.
  /// </summary>
  public partial class ExchangePvpManager
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The flag exported prz buff.
    /// </summary>
    public void FlagExportedPrzBuff()
    {
      var sessionFactorySrz =
        ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml");
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

    #endregion
  }
}