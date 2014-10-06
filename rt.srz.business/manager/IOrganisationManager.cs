// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrganisationManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface OrganisationManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.srz;
  using rt.uec.model.dto;

  using User = rt.core.model.core.User;

  /// <summary>
  ///   The interface OrganisationManager.
  /// </summary>
  public partial interface IOrganisationManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Удаление пдп (пометка неактивен)
    /// </summary>
    /// <param name="pdp">
    /// </param>
    void DeleteOrganisation(Organisation pdp);

    /// <summary>
    /// Удаление pdp (set пометка IsActive=false)
    /// </summary>
    /// <param name="organisationId">
    /// </param>
    void DeleteOrganisation(Guid organisationId);

    /// <summary>
    /// Удаление смо (set пометка IsActive=false)
    /// </summary>
    /// <param name="smoId">
    /// </param>
    void DeleteSmo(Guid smoId);

    /// <summary>
    ///   Возвращает список всех зарегестрированных ТФОМС
    /// </summary>
    /// <returns> The <see cref="IList{T}" /> . </returns>
    IList<Organisation> GetAllTfoms();

    /// <summary>
    /// The get childres.
    /// </summary>
    /// <param name="parentId">
    /// The parent id.
    /// </param>
    /// <returns>
    /// The <see cref="List"/> .
    /// </returns>
    IList<Organisation> GetChildrens(Guid parentId);

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
    MO[] GetMO(string tfomsCode, string workstationName);

    /// <summary>
    /// Возвращает список всех зарегестрированных пуктов выдачи полисов для указанной СМО
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Organisation> GetPDPsBySmo(Guid smoId);

    /// <summary>
    /// The get parent.
    /// </summary>
    /// <param name="org">
    /// The org.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> .
    /// </returns>
    Organisation GetParent(Organisation org);

    /// <summary>
    /// Возвращает список всех зарегестрированных пуктов выдачи полисов для указанной СМО
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/> .
    /// </returns>
    IList<Organisation> GetPdPsBySmo(Guid smoId);

    /// <summary>
    /// Получает список всех пунктов выдачи
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    SearchResult<Organisation> GetPdps(SearchPdpCriteria criteria);

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
    Organisation GetSmo(string okato, string ogrn);

    /// <summary>
    /// Получает список всех организаций
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    SearchResult<Organisation> GetSmos(SearchSmoCriteria criteria);

    /// <summary>
    /// Возвращает список всех зарегестрированных СМО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomId">
    /// The tfom Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/> .
    /// </returns>
    IList<Organisation> GetSmosByTfom(Guid tfomId);

    /// <summary>
    /// Получает список всех организаций
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    SearchResult<Organisation> GetSmosExcludeTfom(SearchSmoCriteria criteria);

    /// <summary>
    /// Возвращает все ТФОМС
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <returns>
    /// The <see cref="MO[]"/> .
    /// </returns>
    MO[] GetTFoms(string workstationName);

    /// <summary>
    /// The get tfom by opfr code.
    /// </summary>
    /// <param name="opfrCode">
    /// The opfr code.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/>.
    /// </returns>
    Organisation GetTfomByOpfrCode(string opfrCode);

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
    /// The <see cref="Organisation"/>.
    /// </returns>
    Organisation GetTfomsByOkato(string okato);

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    IList<User> GetUsersByCurrent();

    /// <summary>
    ///   The off hours.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    bool OffHours();

    /// <summary>
    /// Сохраняет указанный список мед организаций в базу. Все элементы которые присутствуют в базе для данной смо(мипа) но
    ///   отсутсвуют в списке, будут удалены
    /// </summary>
    /// <param name="mipId">
    /// </param>
    /// <param name="mos">
    /// </param>
    void SaveMos(Guid mipId, List<Organisation> mos);

    /// <summary>
    /// Сохраняет указанный список пдп в базу. Все элементы которые присутствуют в базе для данной смо но отсутсвуют в
    ///   списке, будут удалены
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <param name="pdps">
    /// </param>
    void SavePdps(Guid smoId, List<Organisation> pdps);

    /// <summary>
    /// Сохранение смо
    /// </summary>
    /// <param name="smo">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    Guid SaveSmo(Organisation smo);

    /// <summary>
    /// Устанавливает признак IsOnline
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <param name="isOnline">
    /// </param>
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
    bool SmoCodeExists(Guid smoId, string code);

    #endregion
  }
}