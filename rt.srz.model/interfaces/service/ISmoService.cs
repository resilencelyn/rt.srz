// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISmoService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The SMOService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service
{
  #region

  using System;
  using System.Collections.Generic;
  using System.ServiceModel;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The SMOService interface.
  /// </summary>
  [ServiceContract]
  public interface ISmoService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Удаление pdp (set пометка IsActive=false)
    /// </summary>
    /// <param name="pdpId">
    /// </param>
    [OperationContract]
    void DeletePdp(Guid pdpId);

    /// <summary>
    /// Удаление смо (set пометка IsActive=false)
    /// </summary>
    /// <param name="smoId">
    /// </param>
    [OperationContract]
    void DeleteSmo(Guid smoId);

    /// <summary>
    ///   Список всех смо
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Organisation> GetAllSmo();

    /// <summary>
    ///   Возвращает список всех зарегестрированных ТФОМС
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Organisation> GetAllTfoms();

    /// <summary>
    /// The get childres.
    /// </summary>
    /// <param name="parentId">
    /// The parent id.
    /// </param>
    /// <returns>
    /// The <see cref="List{T}"/> .
    /// </returns>
    IList<Organisation> GetChildres(Guid parentId);

    /// <summary>
    /// Возвращает пункт выдачи полисов
    /// </summary>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    /// <returns>
    /// The <see cref="PointDistributionPolicy"/> .
    /// </returns>
    [OperationContract]
    Organisation GetPDP(Guid pdpId);

    /// <summary>
    /// Возвращает список всех зарегестрированных пуктов выдачи полисов для указанной СМО
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    [OperationContract]
    IList<Organisation> GetPDPsBySmo(Guid smoId);

    /// <summary>
    /// Получает список всех пунктов выдачи
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    [OperationContract]
    SearchResult<Organisation> GetPdps(SearchPdpCriteria criteria);

    /// <summary>
    /// Возвращает СМО
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <returns>
    /// The <see cref="Smo"/> .
    /// </returns>
    [OperationContract]
    Organisation GetSmo(Guid smoId);

    /// <summary>
    /// Возвращает СМО
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <param name="ogrn">
    /// The ogrn.
    /// </param>
    /// <returns>
    /// The <see cref="Smo"/> .
    /// </returns>
    [OperationContract]
    Organisation GetSmoByOkatoAndOgrn(string okato, string ogrn);

    /// <summary>
    /// Получает список всех организаций
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    [OperationContract]
    SearchResult<Organisation> GetSmos(SearchSmoCriteria criteria);

    /// <summary>
    /// Возвращает список всех зарегестрированных СМО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomId">
    /// The tfom Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    [OperationContract]
    IList<Organisation> GetSmosByTfom(Guid tfomId);

    /// <summary>
    /// Получает список всех организаций
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    [OperationContract]
    SearchResult<Organisation> GetSmosExcludeTfom(SearchSmoCriteria criteria);

    /// <summary>
    /// The get tfoms.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Tfom"/> .
    /// </returns>
    [OperationContract]
    Organisation GetTfoms(Guid id);

    /// <summary>
    /// Получает список всех организаций для мипа
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    SearchResult<Organisation> GetTfoms(SearchSmoCriteria criteria);

    /// <summary>
    /// Возвращает ТФОМС
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> .
    /// </returns>
    [OperationContract]
    Organisation GetTfomsByOkato(string okato);

    /// <summary>
    /// Получает рабочее место по ид
    /// </summary>
    /// <param name="workstationId">
    /// </param>
    /// <returns>
    /// The <see cref="Workstation"/> .
    /// </returns>
    [OperationContract]
    Workstation GetWorkstation(Guid workstationId);

    /// <summary>
    /// Получает список всех рабочих станций для пункта выдачи
    /// </summary>
    /// <param name="pdpId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    [OperationContract]
    IList<Workstation> GetWorkstationsByPdp(Guid pdpId);

    /// <summary>
    /// Сохраняет указанный список мед организаций в базу. Все элементы которые присутствуют в базе для данного мипа но
    ///   отсутсвуют в списке, будут удалены
    /// </summary>
    /// <param name="mipId">
    /// </param>
    /// <param name="mos">
    /// </param>
    [OperationContract]
    void SaveMos(Guid mipId, List<Organisation> mos);

    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <param name="pdp">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SavePdp(Organisation pdp);

    /// <summary>
    /// Сохраняет указанный список пдп в базу. Все элементы которые присутствуют в базе для данной смо но отсутсвуют в
    ///   списке, будут удалены
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <param name="pdps">
    /// </param>
    [OperationContract]
    void SavePdps(Guid smoId, List<Organisation> pdps);

    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <param name="smo">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SaveSmo(Organisation smo);

    /// <summary>
    /// Устанавливает признак IsOnline
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <param name="isOnline">
    /// </param>
    [OperationContract]
    void SetTfomIsOnline(Guid id, bool isOnline);

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
    [OperationContract]
    bool SmoCodeExists(Guid smoId, string code);

    #endregion
  }
}