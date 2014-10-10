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
    /// <param name="organisation">
    /// The organisation.
    /// </param>
    void DeleteOrganisation(Organisation organisation);

    /// <summary>
    /// Удаление organisation (set пометка IsActive=false)
    /// </summary>
    /// <param name="organisationId">
    /// The organisation Id.
    /// </param>
    void DeleteOrganisation(Guid organisationId);

    /// <summary>
    /// The get childres.
    /// </summary>
    /// <param name="parentId">
    /// The parent Id.
    /// </param>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Organisation}"/>.
    /// </returns>
    IList<Organisation> GetChildrens(Guid parentId, string oid = "");

    /// <summary>
    /// Возвращает все МО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomsCode">
    /// The tfoms Code.
    /// </param>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <returns>
    /// The <see cref="List{MO}"/>.
    /// </returns>
    List<MO> GetMo(string tfomsCode, string workstationName);

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
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{Organisation}"/> .
    /// </returns>
    SearchResult<Organisation> GetSmosByCriteria(SearchSmoCriteria criteria);

    /// <summary>
    /// Возвращает все ТФОМС
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>MO[]</cref>
    ///   </see>
    ///   .
    /// </returns>
    List<MO> GetTFoms(string workstationName);

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
    ///   Возвращает список всех зарегестрированных ТФОМС
    /// </summary>
    /// <returns> The <see cref="IList{T}" /> . </returns>
    IList<Organisation> GetTfoms();

    /// <summary>
    /// Получает список всех организаций для мипа
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{Organisation}"/> .
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
    /// <returns> The <see cref="IList{User}" /> . </returns>
    IList<User> GetUsersByCurrent();

    /// <summary>
    ///   The off hours.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    bool OffHours();

    /// <summary>
    /// Сохраняет указанный список пдп в базу. Все элементы которые присутствуют в базе для данной смо но отсутсвуют в
    ///   списке, будут удалены
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <param name="pdps">
    /// The pdps.
    /// </param>
    void SavePdps(Guid smoId, List<Organisation> pdps);

    /// <summary>
    /// Сохранение смо
    /// </summary>
    /// <param name="smo">
    /// The smo.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    Guid SaveSmo(Organisation smo);

    /// <summary>
    /// Устанавливает признак IsOnline
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="isOnline">
    /// The is Online.
    /// </param>
    void SetTfomIsOnline(Guid id, bool isOnline);

    /// <summary>
    /// Существует ли смо с указанным кодом отличная от указанной
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    bool SmoCodeExists(Guid smoId, string code);

    #endregion
  }
}