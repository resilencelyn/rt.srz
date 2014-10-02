// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SMOGate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The smo gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Collections.Generic;
using System.ServiceModel;

using rt.core.services.aspects;
using rt.core.services.nhibernate;
using rt.core.services.registry;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.model.dto;

#endregion

namespace rt.srz.services.SMO
{
  using System;

  using rt.atl.model.atl;
  using rt.core.services.wcf;
  using rt.core.dto;

  /// <summary>
  /// The smo gate.
  /// </summary>
	[NHibernateWcfContext]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	[ErrorHandlingBehavior]
	public class SMOGate : InterceptedBase, ISMOService
	{
		private readonly ISMOService Service = new SMOService();

		/// <summary>
		/// Возвращает список всех зарегестрированных ТФОМС
		/// </summary>
		/// <returns>
		/// The <see cref="IList"/>.
		/// </returns>
    public IList<Organisation> GetAllTfoms()
		{
			return InvokeInterceptors(() => Service.GetAllTfoms());
		}

    /// <summary>
    /// Возвращает СМО
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <returns>
    /// The <see cref="Smo"/>.
    /// </returns>
    public Organisation GetSmo(Guid smoId)
		{
			return InvokeInterceptors(() => Service.GetSmo(smoId));
		}

    /// <summary>
    /// Возвращает СМО
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <returns>
    /// The <see cref="Smo"/>.
    /// </returns>
    public Organisation GetSmoByOkatoAndOgrn(string okato, string ogrn)
    {
      return InvokeInterceptors(() => Service.GetSmoByOkatoAndOgrn(okato, ogrn));
    }

    /// <summary>
    /// The get tfoms.
    /// </summary>
    /// <param name="id">
    ///   The id.
    /// </param>
    /// <returns>
    /// The <see cref="Tfom"/>.
    /// </returns>
    public Organisation GetTfoms(Guid id)
    {
      return InvokeInterceptors(() => Service.GetTfoms(id));
    }

    /// <summary>
    /// Устанавливает признак IsOnline
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isOnline"></param>
    public void SetTfomIsOnline(Guid id, bool isOnline)
    {
      InvokeInterceptors(() => Service.SetTfomIsOnline(id, isOnline));
    }

    /// <summary>
    /// Возвращает список всех зарегестрированных СМО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomId">
    ///   The tfom Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Organisation> GetSmosByTfom(Guid tfomId)
		{
			return InvokeInterceptors(() => Service.GetSmosByTfom(tfomId));
		}

    /// <summary>
    /// Возвращает список всех зарегестрированных пуктов выдачи полисов для указанной СМО
    /// </summary>
    /// <param name="smoId">
    ///   The smo Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Organisation> GetPDPsBySmo(Guid smoId)
		{
			return InvokeInterceptors(() => Service.GetPDPsBySmo(smoId));
		}

    /// <summary>
    /// Возвращает пункт выдачи полисов
    /// </summary>
    /// <param name="pdpId">
    ///   The pdp Id.
    /// </param>
    /// <returns>
    /// The <see cref="PointDistributionPolicy"/>.
    /// </returns>
    public Organisation GetPDP(Guid pdpId)
		{
			return InvokeInterceptors(() => Service.GetPDP(pdpId));
		}

    /// <summary>
    ///  Получает список всех организаций
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public SearchResult<Organisation> GetSmosExcludeTfom(SearchSmoCriteria criteria)
    {
      return InvokeInterceptors(() => Service.GetSmosExcludeTfom(criteria));
    }

		/// <summary>
		///  Получает список всех организаций
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
    public SearchResult<Organisation> GetSmos(SearchSmoCriteria criteria)
		{
			return InvokeInterceptors(() => Service.GetSmos(criteria));
		}

    /// <summary>
    /// Возвращает ТФОМС
    /// </summary>
    /// <param name="?"></param>
    /// <returns></returns>
    public Organisation GetTfomsByOkato(string okato)
    {
      return InvokeInterceptors(() => Service.GetTfomsByOkato(okato));
    }

		/// <summary>
		/// Получает список всех пунктов выдачи
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
    public SearchResult<Organisation> GetPdps(SearchPdpCriteria criteria)
		{
			return InvokeInterceptors(() => Service.GetPdps(criteria));
		}

    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <param name="smo"></param>
    /// <returns></returns>
    public Guid SaveSmo(Organisation smo)
		{
			return InvokeInterceptors(() => Service.SaveSmo(smo));
		}

    /// <summary>
    /// Удаление смо (set пометка IsActive=false)
    /// </summary>
    /// <param name="smoId"></param>
    public void DeleteSmo(Guid smoId)
		{
			InvokeInterceptors(() => Service.DeleteSmo(smoId));
		}

    /// <summary>
    ///  Удаление pdp (set пометка IsActive=false)
    /// </summary>
    /// <param name="pdpId"></param>
    public void DeletePdp(Guid pdpId)
		{
			InvokeInterceptors(() => Service.DeletePdp(pdpId));
		}

    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <param name="pdp"></param>
    /// <returns></returns>
    public Guid SavePdp(Organisation pdp)
		{
			return InvokeInterceptors(() => Service.SavePdp(pdp));
		}

    /// <summary>
    /// Сохраняет указанный список пдп в базу. Все элементы которые присутствуют в базе для данной смо но отсутсвуют в списке, будут удалены
    /// </summary>
    /// <param name="smoId"></param>
    /// <param name="pdps"></param>
    public void SavePdps(Guid smoId, List<Organisation> pdps)
		{
			InvokeInterceptors(() => Service.SavePdps(smoId, pdps));
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
      InvokeInterceptors(() => Service.SaveMos(mipId, mos));
    }

		/// <summary>
		/// Список всех смо
		/// </summary>
		/// <returns></returns>
    public IList<Organisation> GetAllSmo()
		{
			return InvokeInterceptors(() => Service.GetAllSmo());
		}

    /// <summary>
    /// Получает список всех рабочих станций для пункта выдачи
    /// </summary>
    /// <param name="pdpId"></param>
    /// <returns></returns>
    public IList<Workstation> GetWorkstationsByPdp(Guid pdpId)
		{
			return InvokeInterceptors(() => Service.GetWorkstationsByPdp(pdpId));
		}

    /// <summary>
    /// Получает рабочее место по ид
    /// </summary>
    /// <param name="workstationId"></param>
    /// <returns></returns>
    public Workstation GetWorkstation(Guid workstationId)
		{
			return InvokeInterceptors(() => Service.GetWorkstation(workstationId));
		}

    /// <summary>
    /// Существует ли смо с указанным кодом отличная от указанной
    /// </summary>
    /// <param name="smoId"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public bool SmoCodeExists(Guid smoId, string code)
		{
			return InvokeInterceptors(() => Service.SmoCodeExists(smoId, code));
		}

    /// <summary>
    /// The get childres.
    /// </summary>
    /// <param name="parentId">
    /// The parent id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Organisation> GetChildres(Guid parentId)
    {
      return InvokeInterceptors(() => Service.GetChildres(parentId));
    }

    /// <summary>
    /// Получает список всех организаций для мипа
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
    /// </returns>
    public SearchResult<Organisation> GetTfoms(SearchSmoCriteria criteria)
    {
      return InvokeInterceptors(() => Service.GetTfoms(criteria));
    }

	}
}