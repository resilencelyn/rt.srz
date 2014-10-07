// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrganisationManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The OrganisationManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  using NHibernate;
  using NHibernate.Criterion;

  using NLog;

  using rt.core.business.manager;
  using rt.core.business.security.interfaces;
  using rt.core.model.dto;
  using rt.core.model.dto.enumerations;
  using rt.srz.model.dto;
  using rt.srz.model.srz;
  using rt.uec.model.dto;

  using StructureMap;

  using User = rt.core.model.core.User;

  #endregion

  /// <summary>
  ///   The OrganisationManager.
  /// </summary>
  public partial class OrganisationManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Удаление pdp (set пометка IsActive=false)
    /// </summary>
    /// <param name="organisationId">
    /// </param>
    public void DeleteOrganisation(Guid organisationId)
    {
      var organisation = GetById(organisationId);
      if (organisation != null)
      {
        DeleteOrganisation(organisation);
      }
    }

    /// <summary>
    /// Удаление пдп (пометка неактивен)
    /// </summary>
    /// <param name="pdp">
    /// The pdp.
    /// </param>
    public void DeleteOrganisation(Organisation pdp)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      pdp.IsActive = false;
      session.SaveOrUpdate(pdp);
    }

    /// <summary>
    /// Удаление смо (set пометка IsActive=false)
    /// </summary>
    /// <param name="smoId">
    /// </param>
    public void DeleteSmo(Guid smoId)
    {
      var smo = GetById(smoId);
      if (smo != null)
      {
        smo.IsActive = false;
        SaveOrUpdate(smo);

        // удаляем пометками нактивен и все пункты выдачи данной организации
        var pdps = GetPdPsBySmo(smo.Id);
        foreach (var pdp in pdps)
        {
          pdp.IsActive = false;
          SaveOrUpdate(pdp);
        }

        ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
      }
    }

    /// <summary>
    ///   Возвращает список всех зарегестрированных ТФОМС
    /// </summary>
    /// <returns> The <see cref="IList{T}" /> . </returns>
    public IList<Organisation> GetAllTfoms()
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return session.QueryOver<Organisation>().Where(x => x.Oid.Id == Oid.Tfoms).List();
    }

    /// <summary>
    /// The get childres.
    /// </summary>
    /// <param name="parentId">
    /// The parent id.
    /// </param>
    /// <returns>
    /// The <see cref="List"/> .
    /// </returns>
    public IList<Organisation> GetChildrens(Guid parentId)
    {
      return GetBy(x => x.Parent.Id == parentId && x.IsActive).ToList();
    }

    /// <summary>
    /// Возвращает все МО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomsCode">
    /// </param>
    /// <param name="workstationName">
    /// </param>
    /// <returns>
    /// The <see cref="MO[]"/> .
    /// </returns>
    public MO[] GetMO(string tfomsCode, string workstationName)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Workstation workstation = null;
      Organisation organisation = null;
      var lpuList =
        session.QueryOver<Organisation>()
               .JoinAlias(x => x.Parent, () => organisation)
               .JoinAlias(x => x.Workstations, () => workstation)
               .Where(
                      o =>
                      o.Oid.Id == Oid.Mo && o.IsActive && organisation.Code == tfomsCode
                      && workstation.Name == workstationName)
               .List();
      return lpuList.Select(o => new MO { Id = o.Id, Code = o.Code, Name = o.FullName }).ToArray();
    }

    /// <summary>
    /// Возвращает список всех зарегестрированных пуктов выдачи полисов для указанной СМО
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<Organisation> GetPDPsBySmo(Guid smoId)
    {
      return GetPdPsBySmo(smoId).Where(p => p.IsActive).OrderBy(d => d.ShortName).ToList();
    }

    /// <summary>
    /// The get parent.
    /// </summary>
    /// <param name="org">
    /// The org.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> .
    /// </returns>
    public Organisation GetParent(Organisation org)
    {
      return GetBy(x => x.Id == org.Parent.Id).FirstOrDefault();
    }

    /// <summary>
    /// Возвращает список всех зарегестрированных пуктов выдачи полисов для указанной СМО
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/> .
    /// </returns>
    public IList<Organisation> GetPdPsBySmo(Guid smoId)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return
        session.QueryOver<Organisation>()
               .Where(x => x.Oid.Id == Oid.Pvp)
               .And(x => x.Parent.Id == smoId)
               .OrderBy(x => x.ShortName)
               .Asc.List();
    }

    /// <summary>
    /// Получает список всех пунктов выдачи
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    public SearchResult<Organisation> GetPdps(SearchPdpCriteria criteria)
    {
      var umanager = ObjectFactory.GetInstance<IUserManager>();
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation pdp = null;
      Organisation smo = null;
      Organisation foms = null;
      var query = session.QueryOver(() => pdp)
                         .JoinAlias(x => x.Parent, () => smo)
                         .JoinAlias(() => smo.Parent, () => foms);

      if (currentUser.PointDistributionPolicyId != null)
      {
        if (umanager.IsUserHasAdminPermissions(currentUser))
        {
        }
        else
        {
          // свой регион
          if (umanager.IsUserAdminTf(currentUser.Id))
          {
            query.Where(x => foms.Id == currentUser.GetTf().Id);
          }
          else
          {
            // своя смо
            if (umanager.IsUserAdminSmo(currentUser.Id))
            {
              query.Where(p => smo.Id == currentUser.GetSmo().Id);
            }
          }
        }
      }

      query.Where(p => p.IsActive);

      if (!string.IsNullOrEmpty(criteria.ShortName))
      {
        query.WhereRestrictionOn(s => s.ShortName).IsInsensitiveLike(criteria.ShortName, MatchMode.Anywhere);
      }

      var count = query.RowCount();

      var searchResult = new SearchResult<Organisation> { Skip = criteria.Skip, Total = count };

      query = AddOrder(criteria, pdp, smo, query);
      query.Skip(criteria.Skip).Take(criteria.Take);

      searchResult.Rows = query.List();
      return searchResult;
    }

    /// <summary>
    /// Возвращает смо
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <param name="ogrn">
    /// The ogrn.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    public Organisation GetSmo(string okato, string ogrn)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var tfoms = GetBy(x => x.Oid.Id == Oid.Tfoms && x.Okato == okato).FirstOrDefault();

      return tfoms != null
               ? session.QueryOver<Organisation>()
                        .Where(x => x.Parent.Id == tfoms.Id)
                        .And(x => x.Ogrn == ogrn)
                        .Take(1)
                        .List()
                        .FirstOrDefault()
               : null;
    }

    /// <summary>
    /// Получает список всех организаций
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    public SearchResult<Organisation> GetSmos(SearchSmoCriteria criteria)
    {
      var umanager = ObjectFactory.GetInstance<IUserManager>();
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation smo = null;
      Organisation tfom = null;
      var query = session.QueryOver(() => smo).JoinAlias(x => x.Parent, () => tfom);
      if (currentUser.PointDistributionPolicyId != null)
      {
        if (umanager.IsUserHasAdminPermissions(currentUser))
        {
        }
        else
        {
          // свой регион
          if (umanager.IsUserAdminTf(currentUser.Id))
          {
            query.Where(x => tfom.Id == currentUser.GetTf().Id);
          }
          else
          {
            // своя смо
            if (umanager.IsUserAdminSmo(currentUser.Id))
            {
              query.Where(s => s.Id == currentUser.GetSmo().Id);
            }
          }
        }
      }

      query.Where(s => s.IsActive);

      if (!string.IsNullOrEmpty(criteria.ShortName))
      {
        query.WhereRestrictionOn(s => s.ShortName).IsInsensitiveLike(criteria.ShortName, MatchMode.Anywhere);
      }

      if (!string.IsNullOrEmpty(criteria.Oid))
      {
        query.WhereRestrictionOn(s => s.Oid.Id).IsLike(criteria.Oid);
      }

      var count = query.RowCount();

      var searchResult = new SearchResult<Organisation> { Skip = criteria.Skip, Total = count };

      query = AddOrder(criteria, smo, tfom, query);
      query.Skip(criteria.Skip).Take(criteria.Take);

      searchResult.Rows = query.List();
      return searchResult;
    }

    /// <summary>
    /// Возвращает список всех зарегестрированных СМО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomId">
    /// The tfom Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/> .
    /// </returns>
    public IList<Organisation> GetSmosByTfom(Guid tfomId)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return
        session.QueryOver<Organisation>()
               .Where(x => x.Oid.Id == Oid.Smo && x.Parent.Id == tfomId && (x.IsActive || x.IsActive == null))
               .OrderBy(x => x.ShortName)
               .Asc.List();
    }

    /// <summary>
    /// Получает список всех организаций
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    public SearchResult<Organisation> GetSmosExcludeTfom(SearchSmoCriteria criteria)
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation smo = null;
      var query = session.QueryOver(() => smo)
                         .Where(s => s.IsActive)
                         .And(x => x.Oid.Id == Oid.Smo || x.Oid.Id == Oid.Mo);

      if (!string.IsNullOrEmpty(criteria.ShortName))
      {
        query.WhereRestrictionOn(s => s.ShortName).IsInsensitiveLike(criteria.ShortName, MatchMode.Anywhere);
      }

      var count = query.RowCount();

      var searchResult = new SearchResult<Organisation> { Skip = criteria.Skip, Total = count };

      query = AddOrder(criteria, smo, null, query);
      query.Skip(criteria.Skip).Take(criteria.Take);

      searchResult.Rows = query.List();
      return searchResult;
    }

    /// <summary>
    /// Возвращает все ТФОМС
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <returns>
    /// The <see cref="MO[]"/> .
    /// </returns>
    public MO[] GetTFoms(string workstationName)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation mo = null;
      Workstation workstation = null;
      var tfomsList =
        session.QueryOver<Organisation>()
               .JoinAlias(x => x.Organisations1, () => mo)
               .JoinAlias(() => mo.Workstations, () => workstation)
               .Where(t => t.Oid.Id == Oid.Tfoms && t.IsActive && workstation.Name == workstationName)
               .List()
               .Distinct();
      return tfomsList.Select(o => new MO { Id = o.Id, Code = o.Code, Name = o.FullName }).ToArray();
    }

    /// <summary>
    /// The get tfom by opfr code.
    /// </summary>
    /// <param name="opfrCode">
    /// The opfr code.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    public Organisation GetTfomByOpfrCode(string opfrCode)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation foms = null;
      var query =
        session.QueryOver(() => foms)
               .Where(x => x.Oid.Id == Oid.Opfr)
               .WhereRestrictionOn(f => f.Code)
               .IsInsensitiveLike(opfrCode, MatchMode.Anywhere);
      return query.List().FirstOrDefault();
    }

    /// <summary>
    /// Получает список всех организаций для мипа
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    public SearchResult<Organisation> GetTfoms(SearchSmoCriteria criteria)
    {
      var secService = ObjectFactory.GetInstance<ISecurityProvider>();
      var currentUser = secService.GetCurrentUser();
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation smo = null;
      var query = session.QueryOver(() => smo).Where(s => s.IsActive).And(x => x.Oid.Id == Oid.Tfoms);

      if (!string.IsNullOrEmpty(criteria.ShortName))
      {
        query.WhereRestrictionOn(s => s.FullName).IsInsensitiveLike(criteria.ShortName, MatchMode.Anywhere);
      }

      var count = query.RowCount();

      var searchResult = new SearchResult<Organisation> { Skip = criteria.Skip, Total = count };

      query = AddOrder(criteria, smo, null, query);
      query.Skip(criteria.Skip).Take(criteria.Take);

      searchResult.Rows = query.List();
      return searchResult;
    }

    /// <summary>
    /// Возвращает ТФОМС
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    public Organisation GetTfomsByOkato(string okato)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return
        session.QueryOver<Organisation>()
               .Where(x => x.Oid.Id == Oid.Tfoms)
               .And(x => x.Okato == okato)
               .Take(1)
               .List()
               .FirstOrDefault();
    }

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<User> GetUsersByCurrent()
    {
      var sec = ObjectFactory.GetInstance<ISecurityProvider>();
      var currentUser = sec.GetCurrentUser();
      var userManager = ObjectFactory.GetInstance<IUserManager>();
      if (userManager.IsUserHasAdminPermissions(currentUser))
      {
        return userManager.GetUsers();
      }

      if (!currentUser.PointDistributionPolicyId.HasValue)
      {
        return null;
      }

      // свой регион
      if (userManager.IsUserAdminTf(currentUser.Id))
      {
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        User user = null;
        Organisation foms = null;
        Organisation smo = null;
        var pvpUser = GetById(currentUser.PointDistributionPolicyId.Value);
        var curFomsId = pvpUser.Parent.Parent.Id;

        var pvps =
          QueryOver.Of<Organisation>()
                   .JoinAlias(x => x.Parent, () => smo)
                   .JoinAlias(() => smo.Parent, () => foms)
                   .Where(x => foms.Id == curFomsId)
                   .Where(x => x.Id == user.PointDistributionPolicyId)
                   .Select(x => x.Id);

        var query = session.QueryOver(() => user).Where(x => x.IsApproved).WithSubquery.WhereExists(pvps);
        return query.List();
      }

      // своя смо
      if (userManager.IsUserAdminSmo(currentUser.Id))
      {
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        User user = null;
        Organisation foms = null;
        Organisation smo = null;
        Organisation pdp = null;
        var curSmoId = currentUser.GetSmo().Id;

        var over =
          QueryOver.Of(() => pdp)
                   .JoinAlias(() => pdp.Parent, () => smo)
                   .JoinAlias(() => smo.Parent, () => foms)
                   .Where(x => smo.Id == curSmoId && user.PointDistributionPolicyId == pdp.Id)
                   .Select(x => x.Id);

        var query = session.QueryOver(() => user).WithSubquery.WhereExists(over).Where(x => user.IsApproved);
        return query.List();
      }

      return null;
    }

    /// <summary>
    ///   The off hours.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    public bool OffHours()
    {
      return false;

      ////var time = DateTime.UtcNow.TimeOfDay;

      ////return GetAllTfoms().Any(
      ////  x =>
      ////  x.TimeRunFrom.HasValue && x.TimeRunTo.HasValue 
      ////  && (x.TimeRunFrom.Value.TimeOfDay <= time) 
      ////  && (x.TimeRunTo.Value.TimeOfDay >= time));
    }

    /// <summary>
    /// Сохраняет указанный список мед организаций в базу. Все элементы которые присутствуют в базе для данного мипа но
    ///   отсутсвуют в списке, будут удалены
    /// </summary>
    /// <param name="mipId">
    /// </param>
    /// <param name="mos">
    /// </param>
    public void SaveMos(Guid mipId, List<Organisation> mos)
    {
      SavePdpsInternal(mipId, mos, true);
    }

    /// <summary>
    /// Сохраняет указанный список пдп в базу. Все элементы которые присутствуют в базе для данной смо но отсутсвуют в
    ///   списке, будут удалены
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <param name="pdps">
    /// </param>
    public void SavePdps(Guid smoId, List<Organisation> pdps)
    {
      SavePdpsInternal(smoId, pdps, false);
    }

    /// <summary>
    /// Сохраняет указанный список пдп в базу. Все элементы которые присутствуют в базе для данной смо но отсутсвуют в
    ///   списке, будут удалены
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <param name="pdps">
    /// </param>
    /// <param name="useMoOidForPdp">
    /// The use Mo Oid For Pdp.
    /// </param>
    public void SavePdpsInternal(Guid smoId, List<Organisation> pdps, bool useMoOidForPdp)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var transaction = session.BeginTransaction();
      var workstationManager = ObjectFactory.GetInstance<IWorkstationManager>();
      var sertificateUecManager = ObjectFactory.GetInstance<ISertificateUecManager>();
      try
      {
        // Удаляем ПВП, которых нет в списке
        IList<Organisation> pointDistributionPolicies = null;
        if (useMoOidForPdp)
        {
          pointDistributionPolicies = GetBy(x => x.Oid.Id == Oid.Mo && x.Parent.Id == smoId && x.IsActive);
        }
        else
        {
          pointDistributionPolicies = GetBy(x => x.Oid.Id == Oid.Pvp && x.Parent.Id == smoId && x.IsActive);
        }

        var pvpToDelete = pointDistributionPolicies.Where(x => pdps.All(y => y.Id != x.Id));
        foreach (var p in pvpToDelete)
        {
          foreach (var workstation in workstationManager.GetBy(x => x.PointDistributionPolicy.Id == p.Id))
          {
            sertificateUecManager.Delete(x => x.Workstation.Id == workstation.Id);
            workstationManager.Delete(workstation);
          }

          DeleteOrganisation(p);
        }

        foreach (var actual in pdps)
        {
          actual.Parent = GetById(smoId);
          var p = GetById(actual.Id) ?? actual;
          p.Id = actual.Id;
          p.Code = actual.Code;
          p.EMail = actual.EMail;
          p.Fax = actual.Fax;
          p.FirstName = actual.FirstName;
          p.FullName = actual.FullName;
          p.IsActive = actual.IsActive;
          p.LastName = actual.LastName;
          p.MiddleName = actual.MiddleName;
          p.Phone = actual.Phone;
          p.ShortName = actual.ShortName;
          p.Parent = actual.Parent;
          p.Workstations = actual.Workstations;
          session.SaveOrUpdate(p);

          if (p.Workstations != null)
          {
            // Удаляем АРМы, которых нет в списке
            var workstationToDelete =
              workstationManager.GetBy(x => x.PointDistributionPolicy.Id == p.Id)
                                .Where(x => p.Workstations.All(y => y.Id != x.Id));
            foreach (var old in workstationToDelete)
            {
              // удалим все сертификаты
              sertificateUecManager.Delete(s => s.Workstation.Id == old.Id);

              // удалим сам элемент
              workstationManager.Delete(old);
            }

            // синхронизируем список Workstations из базы для пдп и пришедший в объекте
            foreach (var workstation in p.Workstations)
            {
              var w = workstationManager.GetById(workstation.Id) ?? workstation;
              w.Name = workstation.Name;
              w.UecCerticateType = workstation.UecCerticateType;
              w.UecReaderName = workstation.UecReaderName;
              w.SmardCardReaderName = workstation.SmardCardReaderName;
              w.SertificateUecs = workstation.SertificateUecs;

              w.PointDistributionPolicy = p;

              workstationManager.SaveOrUpdate(w);

              // сохраняем сертификаты (в списке только добавленные будут - остальные очищаю)
              if (w.SertificateUecs != null)
              {
                foreach (var sert in w.SertificateUecs)
                {
                  sert.Workstation = w;
                  sertificateUecManager.SaveWorkstationSertificateKey(w.Id, sert.Version, sert.Type.Id, sert.Key);
                }
              }
            }
          }
        }

        session.Flush();
        transaction.Commit();
      }
      catch (Exception ex)
      {
        LogManager.GetCurrentClassLogger().Error(ex.Message, ex);
        transaction.Dispose();
        throw;
      }
    }

    /// <summary>
    /// Сохранение смо
    /// </summary>
    /// <param name="smo">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    public Guid SaveSmo(Organisation smo)
    {
      ObjectFactory.GetInstance<IOrganisationManager>().SaveOrUpdate(smo);
      if (smo.SertificateUecs != null)
      {
        foreach (var sert in smo.SertificateUecs)
        {
          sert.Smo = smo;
          ObjectFactory.GetInstance<ISertificateUecManager>()
                       .SaveSmoSertificateKey(smo.Id, sert.Version, sert.Type.Id, sert.Key);
        }
      }

      return smo.Id;
    }

    /// <summary>
    /// Устанавливает признак IsOnline
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <param name="isOnline">
    /// </param>
    public void SetTfomIsOnline(Guid id, bool isOnline)
    {
      var fom = GetById(id);
      fom.IsOnLine = isOnline;
      SaveOrUpdate(fom);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Существует ли смо с указанным кодом отличная от указанной
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <param name="code">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    public bool SmoCodeExists(Guid smoId, string code)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var rowCount =
        session.QueryOver<Organisation>()
               .Where(x => x.Oid.Id == Oid.Smo && x.IsActive)
               .And(s => s.Id != smoId && s.Code == code)
               .RowCount();
      return rowCount != 0;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The add order.
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <param name="smo">
    /// The smo.
    /// </param>
    /// <param name="tfom">
    /// The tfom.
    /// </param>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <returns>
    /// The <see cref="IQueryOver"/> .
    /// </returns>
    private IQueryOver<Organisation, Organisation> AddOrder(
      SearchSmoCriteria criteria, 
      Organisation smo, 
      Organisation tfom, 
      IQueryOver<Organisation, Organisation> query)
    {
      // Сортировка
      if (!string.IsNullOrEmpty(criteria.SortExpression))
      {
        Expression<Func<object>> expression = () => smo.ShortName;
        switch (criteria.SortExpression)
        {
          case "TFom":
            expression = () => tfom.ShortName;
            break;
          case "ShortName":
            expression = () => smo.ShortName;
            break;
          case "FullName":
            expression = () => smo.FullName;
            break;
        }

        query = criteria.SortDirection == SortDirection.Ascending
                  ? query.OrderBy(expression).Asc
                  : query.OrderBy(expression).Desc;
      }

      return query;
    }

    /// <summary>
    /// The add order.
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <param name="pdp">
    /// The pdp.
    /// </param>
    /// <param name="smo">
    /// The smo.
    /// </param>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <returns>
    /// The <see cref="IQueryOver"/> .
    /// </returns>
    private IQueryOver<Organisation, Organisation> AddOrder(
      SearchPdpCriteria criteria, 
      Organisation pdp, 
      Organisation smo, 
      IQueryOver<Organisation, Organisation> query)
    {
      // Сортировка
      if (!string.IsNullOrEmpty(criteria.SortExpression))
      {
        Expression<Func<object>> expression = () => smo.ShortName;
        switch (criteria.SortExpression)
        {
          case "Smo":
            expression = () => smo.ShortName;
            break;
          case "ShortName":
            expression = () => pdp.ShortName;
            break;
          case "FullName":
            expression = () => pdp.FullName;
            break;
        }

        query = criteria.SortDirection == SortDirection.Ascending
                  ? query.OrderBy(expression).Asc
                  : query.OrderBy(expression).Desc;
      }

      return query;
    }

    #endregion
  }
}