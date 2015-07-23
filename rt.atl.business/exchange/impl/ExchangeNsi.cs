// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangeNsi.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The exchange nsi.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.exchange.impl
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;

  using Quartz;

  using rt.atl.model.atl;
  using rt.core.business.nhibernate;
  using rt.core.model.interfaces;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  using AutoComplete = rt.srz.model.srz.AutoComplete;

  #endregion

  /// <summary>
  ///   The exchange nsi.
  /// </summary>
  public class ExchangeNsi : ExchangeBase
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ExchangeNsi" /> class.
    /// </summary>
    public ExchangeNsi()
      : base(ExchangeTypeEnum.SinhronizeNsi)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The run.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public override void Run(IJobExecutionContext context)
    {
      SinhronizeSmo();
      SinhronizePvp();
      SinhronizeNames();
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The sinhronize names.
    /// </summary>
    private void SinhronizeNames()
    {
      using (
        var sessionSrz =
          ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml").OpenSession())
      {
        var sessionPvp = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
        var srzImList = sessionSrz.QueryOver<Im>().List();
        var srzOtList = sessionSrz.QueryOver<Ot>().List();
        var pvpImOtList = sessionPvp.QueryOver<AutoComplete>().List();
        var pvpImList =
          pvpImOtList.Where(x => x.Type.Id == srz.model.srz.concepts.AutoComplete.FirstName && x.Gender != null)
                     .ToList();
        var pvpOtList =
          pvpImOtList.Where(x => x.Type.Id == srz.model.srz.concepts.AutoComplete.MiddleName && x.Gender != null)
                     .ToList();

        // Перенос имен из СРЗ в ПВП
        var srz2PvpImList =
          srzImList.Where(
                          x =>
                          !pvpImList.Any(
                                         y =>
                                         x.Caption.ToLower() == y.Name.ToLower() && (x.W == y.Gender.Id - Sex.Sex1 + 1)))
                   .ToList();
        foreach (var im in srz2PvpImList)
        {
          var ac = new AutoComplete();
          ac.Name = im.Caption.ToUpperFirstChar();
          ac.Gender = conceptManager.GetBy(x => x.Code == im.W.ToString() && x.Oid.Id == Oid.Пол).FirstOrDefault()
                      ?? conceptManager.GetById(Sex.Sex1);
          ac.Type = conceptManager.GetById(srz.model.srz.concepts.AutoComplete.FirstName);
          pvpImList.Add(ac);
          sessionPvp.Save(ac);
        }

        // Перенос отчеств из СРЗ в ПВП
        var srz2PvpOtList =
          srzOtList.Where(
                          x =>
                          !pvpOtList.Any(
                                         y =>
                                         x.Caption.ToLower() == y.Name.ToLower() && (x.W == y.Gender.Id - Sex.Sex1 + 1)))
                   .ToList();
        foreach (var ot in srz2PvpOtList)
        {
          var ac = new AutoComplete();
          ac.Name = ot.Caption.ToUpperFirstChar();
          ac.Gender = conceptManager.GetBy(x => x.Code == ot.W.ToString() && x.Oid.Id == Oid.Пол).FirstOrDefault()
                      ?? conceptManager.GetById(Sex.Sex1);
          ac.Type = conceptManager.GetById(srz.model.srz.concepts.AutoComplete.MiddleName);
          pvpOtList.Add(ac);
          sessionPvp.Save(ac);
        }

        sessionPvp.Flush();

        // Перенос имен из ПВП в СРЗ
        var pvp2SrzImList =
          pvpImList.Where(
                          y =>
                          !srzImList.Any(
                                         x =>
                                         x.Caption.ToLower() == y.Name.ToLower() && (x.W == y.Gender.Id - Sex.Sex1 + 1)))
                   .ToList();
        foreach (var im in pvp2SrzImList)
        {
          var ac = new Im();
          ac.Caption = im.Name.ToUpperFirstChar();
          int gender;
          if (im.Gender != null && int.TryParse(im.Gender.Code, out gender))
          {
            ac.W = gender;
          }

          sessionSrz.Save(ac);
        }

        // Перенос отчеств из ПВП в СРЗ
        var pvp2SrzOtList =
          pvpOtList.Where(
                          y =>
                          !srzOtList.Any(
                                         x =>
                                         x.Caption.ToLower() == y.Name.ToLower() && (x.W == y.Gender.Id - Sex.Sex1 + 1)))
                   .ToList();
        foreach (var ot in pvp2SrzOtList)
        {
          var ac = new Ot();
          ac.Caption = ot.Name.ToUpperFirstChar();
          int gender;
          if (int.TryParse(ot.Gender.Code, out gender))
          {
            ac.W = gender;
          }

          sessionSrz.Save(ac);
        }

        sessionSrz.Flush();

        // закрываем сессию СРЗ
        sessionSrz.Close();
      }
    }

    /// <summary>
    ///   The sinhronize pvp.
    /// </summary>
    private void SinhronizePvp()
    {
      using (
        var sessionSrz =
          ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml").OpenSession())
      {
        var przs = sessionSrz.QueryOver<Prz>().List();
        var sessionPvp = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var pointDistributionPolicies = sessionPvp.QueryOver<Organisation>().Where(x => x.Oid.Id == Oid.Pvp).List();
        var list = pointDistributionPolicies;
        var przToPvpList = przs.Where(x => !list.Any(y => y.Code == x.Code && y.Parent.Code == x.SMO.Code)).ToList();
        var oid = ObjectFactory.GetInstance<IOidManager>().GetById(Oid.Pvp);
        foreach (var prz in przToPvpList)
        {
          Organisation smo = null;
          if (prz.SMO != null)
          {
            var prz1 = prz;
            smo =
              sessionPvp.QueryOver<Organisation>()
                        .Where(x => x.Code == prz1.SMO.Code && x.Oid.Id == Oid.Smo)
                        .List()
                        .SingleOrDefault();
          }

          var bossfio = string.IsNullOrEmpty(prz.Bossname)
                          ? new[] { string.Empty, string.Empty, string.Empty }
                          : prz.Bossname.Split(' ');
          if (smo != null)
          {
            var pvp = new Organisation
                      {
                        Parent = smo, 
                        Code = prz.Code, 
                        FirstName = bossfio.Length >= 1 ? bossfio[0] : string.Empty, 
                        LastName = bossfio.Length >= 2 ? bossfio[1] : string.Empty, 
                        MiddleName = bossfio.Length >= 3 ? bossfio[2] : string.Empty, 
                        EMail = prz.Email, 
                        Fax = prz.Tel2, 
                        Phone = prz.Tel1, 
                        FullName = prz.Fullname, 
                        ShortName = prz.Caption, 
                        Oid = oid, 
                        IsActive = true, 
                        IsOnLine = true
                      };

            sessionPvp.Save(pvp);
            pointDistributionPolicies.Add(pvp);
          }
        }

        sessionPvp.Flush();
        var pvpToPrzList =
          pointDistributionPolicies.Where(x => !przs.Any(y => y.Code == x.Code && y.SMO.Code == x.Parent.Code)).ToList();
        foreach (var pvp in pvpToPrzList)
        {
          var pvp1 = pvp;
          var smo = sessionSrz.QueryOver<Smo>().Where(x => x.Code == pvp1.Parent.Code).List().SingleOrDefault();
          if (smo == null)
          {
            continue;
          }

          var prz = new Prz
                    {
                      SMO = smo, 
                      Code = pvp.Code, 
                      Caption = pvp.ShortName, 
                      Fullname = pvp.FullName, 
                      Dedit = DateTime.Now, 
                      Email = pvp.EMail, 
                      Ogrn = smo.Ogrn, 
                      Bossname = string.Format("{0} {1} {2}", pvp.FirstName, pvp.LastName, pvp.MiddleName), 
                      Tel1 = pvp.Phone, 
                      Tel2 = pvp.Fax
                    };

          sessionSrz.Save(prz);
        }

        sessionSrz.Flush();
        sessionSrz.Close();
      }
    }

    /// <summary>
    ///   The sinhronize smo.
    /// </summary>
    private void SinhronizeSmo()
    {
      using (
        var sessionSrz =
          ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml").OpenSession())
      {
        var sessionPvp = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var smoSrzList = sessionSrz.QueryOver<Smo>().List();
        var smoPvpList = sessionPvp.QueryOver<Organisation>().Where(x => x.Oid.Id == Oid.Smo).List();
        var list = smoPvpList;

        // Перенос СМО из базы srz в базу pvp
        var smoToPvpList = smoSrzList.Where(x => list.All(y => y.Code != x.Code)).ToList();
        var oid = ObjectFactory.GetInstance<IOidManager>().GetById(Oid.Smo);
        foreach (var smo in smoToPvpList)
        {
          var bossfio = string.IsNullOrEmpty(smo.Bossname)
                          ? new[] { string.Empty, string.Empty, string.Empty }
                          : smo.Bossname.Split(' ');

          var tfomsCode = smo.Code.Length >= 2 ? smo.Code.Substring(0, 2) : string.Empty;
          var smoPvp = new Organisation
                       {
                         Parent =
                           ObjectFactory.GetInstance<IOrganisationManager>()
                                        .GetBy(x => x.Code == tfomsCode && x.Oid.Id == Oid.Tfoms)
                                        .FirstOrDefault(), 
                         Code = smo.Code, 
                         ShortName = smo.Caption, 
                         FullName = smo.Fullname, 
                         Ogrn = smo.Ogrn, 
                         Phone = smo.Tel1, 
                         DateLastEdit = smo.Dedit, 
                         FirstName = bossfio.Length >= 1 ? bossfio[0] : string.Empty, 
                         LastName = bossfio.Length >= 2 ? bossfio[1] : string.Empty, 
                         MiddleName = bossfio.Length >= 3 ? bossfio[2] : string.Empty, 
                         DateIncludeRegister = smo.Db, 
                         DateExcludeRegister = smo.De, 
                         Oid = oid
                       };

          sessionPvp.Save(smoPvp);
          smoPvpList.Add(smoPvp);
        }

        sessionPvp.Flush();

        // Перенос диапазонов номеров бланков ВС из базы СРЗ в базу ПВП
        var rangeNumbersSrzList = sessionSrz.QueryOver<Vsdiap>().List();
        var addedPvpRangeNumber = new List<RangeNumber>();
        foreach (var rangeNumberSrz in rangeNumbersSrzList)
        {
          Organisation smo = null;
          var rangeNumberPvp =
            sessionPvp.QueryOver<RangeNumber>()
                      .JoinAlias(x => x.Smo, () => smo)
                      .Where(
                             x =>
                             x.RangelFrom == rangeNumberSrz.Lo && x.RangelTo == rangeNumberSrz.Hi
                             && smo.Code == rangeNumberSrz.SMO.Code)
                      .List()
                      .FirstOrDefault();

          // создаем новую запись
          if (rangeNumberPvp == null)
          {
            rangeNumberPvp = new RangeNumber();
            rangeNumberPvp.Smo =
              sessionPvp.QueryOver<Organisation>().Where(x => x.Code == rangeNumberSrz.SMO.Code).List().FirstOrDefault();
            rangeNumberPvp.RangelFrom = rangeNumberSrz.Lo.HasValue ? rangeNumberSrz.Lo.Value : 0;
            rangeNumberPvp.RangelTo = rangeNumberSrz.Hi.HasValue ? rangeNumberSrz.Hi.Value : 0;
            addedPvpRangeNumber.Add(rangeNumberPvp);
            sessionPvp.Save(rangeNumberPvp);
          }
        }

        sessionPvp.Flush();

        // Перенос СМО из базы ПВП в базу СРЗ
        var smoToSrzList = smoPvpList.Where(x => smoSrzList.All(y => y.Code != x.Code)).ToList();
        foreach (var smo in smoToSrzList)
        {
          var smoSrz = new Smo
                       {
                         Code = smo.Code, 
                         Caption = smo.ShortName, 
                         Fullname = smo.FullName, 
                         Ogrn = smo.Ogrn, 
                         Db = smo.DateIncludeRegister, 
                         De = smo.DateExcludeRegister, 
                         Bossname = string.Format("{0} {1} {2}", smo.FirstName, smo.LastName, smo.MiddleName), 
                         Tel1 = smo.Phone, 
                       };

          sessionSrz.Save(smoSrz);
        }

        sessionSrz.Flush();

        // Перенос диапазонов номеров бланков ВС из базы ПВП в базу СРЗ
        var rangeNumbersPvpList = sessionPvp.QueryOver<RangeNumber>().List();
        rangeNumbersPvpList = rangeNumbersPvpList.Where(x => addedPvpRangeNumber.All(y => x.Id != y.Id)).ToList();
        foreach (var rangeNumberPvp in rangeNumbersPvpList)
        {
          Smo smo = null;
          var vsdiapSrz =
            sessionSrz.QueryOver<Vsdiap>()
                      .JoinAlias(x => x.SMO, () => smo)
                      .Where(
                             x =>
                             x.Lo == rangeNumberPvp.RangelFrom && x.Hi == rangeNumberPvp.RangelTo
                             && smo.Code == rangeNumberPvp.Smo.Code)
                      .List()
                      .FirstOrDefault();

          // создаем новую запись
          if (vsdiapSrz == null)
          {
            vsdiapSrz = new Vsdiap();
            vsdiapSrz.Dedit = DateTime.Now;
            vsdiapSrz.SMO =
              sessionSrz.QueryOver<Smo>().Where(x => x.Code == rangeNumberPvp.Smo.Code).List().FirstOrDefault();
            vsdiapSrz.Lo = rangeNumberPvp.RangelFrom;
            vsdiapSrz.Hi = rangeNumberPvp.RangelTo;
            sessionSrz.Save(vsdiapSrz);
          }
        }

        sessionSrz.Flush();

        // закрываем сессию СРЗ
        sessionSrz.Close();
      }
    }

    #endregion
  }
}