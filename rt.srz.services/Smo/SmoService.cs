// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmoService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The smo service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Smo
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  using NHibernate;
  using NHibernate.Criterion;

  using rt.core.model.dto;
  using rt.core.model.dto.enumerations;
  using rt.srz.business.manager;
  using rt.srz.model.dto;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The smo service.
  /// </summary>
  public class SmoService : ISmoService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Удаление pdp (set пометка IsActive=false)
    /// </summary>
    /// <param name="pdpId">
    /// </param>
    public void DeletePdp(Guid pdpId)
    {
      ObjectFactory.GetInstance<IOrganisationManager>().DeleteOrganisation(pdpId);
    }

    /// <summary>
    /// Удаление смо (set пометка IsActive=false)
    /// </summary>
    /// <param name="smoId">
    /// </param>
    public void DeleteSmo(Guid smoId)
    {
      ObjectFactory.GetInstance<IOrganisationManager>().DeleteSmo(smoId);
    }

    /// <summary>
    ///   Список всех смо
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Organisation> GetAllSmo()
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetAll(1000).OrderBy(s => s.ShortName).ToList();
    }

    /// <summary>
    ///   Возвращает список всех зарегестрированных ТФОМС
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Organisation> GetAllTfoms()
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetAllTfoms().OrderBy(f => f.ShortName).ToList();
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
    public IList<Organisation> GetChildres(Guid parentId)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetChildrens(parentId);
    }

    /// <summary>
    /// Возвращает пункт выдачи полисов
    /// </summary>
    /// <param name="pdpId">
    /// The pdp Id. 
    /// </param>
    /// <returns>
    /// The <see cref="PointDistributionPolicy"/> . 
    /// </returns>
    public Organisation GetPDP(Guid pdpId)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetById(pdpId);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().GetPDPsBySmo(smoId);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().GetPdps(criteria);
    }

    /// <summary>
    /// The get smo.
    /// </summary>
    /// <param name="smoId">
    /// The smo id. 
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> . 
    /// </returns>
    public Organisation GetSmo(Guid smoId)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetById(smoId);
    }

    /// <summary>
    /// The get smo by okato and ogrn.
    /// </summary>
    /// <param name="okato">
    /// The okato. 
    /// </param>
    /// <param name="ogrn">
    /// The ogrn. 
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> . 
    /// </returns>
    public Organisation GetSmoByOkatoAndOgrn(string okato, string ogrn)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetSmo(okato, ogrn);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().GetSmos(criteria);
    }

    /// <summary>
    /// Возвращает список всех зарегестрированных СМО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomId">
    /// The tfom Id. 
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Organisation> GetSmosByTfom(Guid tfomId)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetSmosByTfom(tfomId);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().GetSmosExcludeTfom(criteria);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().GetTfoms(criteria);
    }

    /// <summary>
    /// The get tfoms.
    /// </summary>
    /// <param name="id">
    /// The id. 
    /// </param>
    /// <returns>
    /// The <see cref="Tfom"/> . 
    /// </returns>
    public Organisation GetTfoms(Guid id)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetById(id);
    }

    /// <summary>
    /// Возвращает ТФОМС
    /// </summary>
    /// <param name="okato">
    /// The okato. 
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> . 
    /// </returns>
    public Organisation GetTfomsByOkato(string okato)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetTfomsByOkato(okato);
    }

    /// <summary>
    /// Получает рабочее место по ид
    /// </summary>
    /// <param name="workstationId">
    /// </param>
    /// <returns>
    /// The <see cref="Workstation"/> . 
    /// </returns>
    public Workstation GetWorkstation(Guid workstationId)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetById(workstationId);
    }

    /// <summary>
    /// Получает список всех рабочих станций для пункта выдачи
    /// </summary>
    /// <param name="pdpId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Workstation> GetWorkstationsByPdp(Guid pdpId)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetBy(w => w.PointDistributionPolicy.Id == pdpId);
    }

    /// <summary>
    /// Сохраняет указанный список мед организаций в базу. Все элементы которые присутствуют в базе для данного мипа но отсутсвуют в списке, будут удалены
    /// </summary>
    /// <param name="mipId">
    /// </param>
    /// <param name="mos">
    /// </param>
    public void SaveMos(Guid mipId, List<Organisation> mos)
    {
      ObjectFactory.GetInstance<IOrganisationManager>().SaveMos(mipId, mos);
    }

    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <param name="pdp">
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public Guid SavePdp(Organisation pdp)
    {
      ObjectFactory.GetInstance<IOrganisationManager>().SaveOrUpdate(pdp);
      return pdp.Id;
    }

    /// <summary>
    /// Сохраняет указанный список пдп в базу. Все элементы которые присутствуют в базе для данной смо но отсутсвуют в списке, будут удалены
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <param name="pdps">
    /// </param>
    public void SavePdps(Guid smoId, List<Organisation> pdps)
    {
      ObjectFactory.GetInstance<IOrganisationManager>().SavePdps(smoId, pdps);
    }

    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <param name="smo">
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public Guid SaveSmo(Organisation smo)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().SaveSmo(smo);
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
      ObjectFactory.GetInstance<IOrganisationManager>().SetTfomIsOnline(id, isOnline);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().SmoCodeExists(smoId, code);
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
      SearchSmoCriteria criteria, Organisation smo, Organisation tfom, IQueryOver<Organisation, Organisation> query)
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
      SearchPdpCriteria criteria, Organisation pdp, Organisation smo, IQueryOver<Organisation, Organisation> query)
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