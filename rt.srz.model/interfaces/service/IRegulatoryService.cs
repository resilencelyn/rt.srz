// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegulatoryService.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The NsiService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service
{
  #region

  using System;
  using System.Collections.Generic;
  using System.ServiceModel;

  using rt.core.model.dto;
  using rt.core.model.interfaces;
  using rt.srz.model.dto;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The NsiService interface.
  /// </summary>
  [ServiceContract]
  public interface IRegulatoryService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Создание копии шаблона печати
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    [OperationContract]
    Template CreateCopyOfTemplate(Guid id);

    /// <summary>
    /// Удаление имени или отчества
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    [OperationContract]
    void DeleteAutoComplete(Guid id);

    /// <summary>
    /// Удаление pdp (set пометка IsActive=false)
    /// </summary>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    [OperationContract]
    void DeleteOrganisation(Guid pdpId);

    /// <summary>
    /// Удаление диапозона
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    [OperationContract]
    void DeleteRangeNumber(Guid id);

    /// <summary>
    /// Удаление шаблона печати вс
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    [OperationContract]
    void DeleteTemplateVs(Guid id);

    /// <summary>
    /// Проверяет существует ли уже запись в базе с таким же именем, типом, полом
    /// </summary>
    /// <param name="firstMiddleName">
    /// The first Middle Name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    [OperationContract]
    bool FirstMiddleNameExists(AutoComplete firstMiddleName);

    /// <summary>
    /// Получает запись по ид
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="AutoComplete"/> .
    /// </returns>
    [OperationContract]
    AutoComplete GetAutoComplete(Guid id);

    /// <summary>
    /// Получает результат по критерию для имён и отчеств
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{AutoComplete}"/> .
    /// </returns>
    [OperationContract]
    SearchResult<AutoComplete> GetAutoCompleteByCriteria(SearchAutoCompleteCriteria criteria);

    /// <summary>
    /// The get childres.
    /// </summary>
    /// <param name="parentId">
    /// The parent id.
    /// </param>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <returns>
    /// The <see cref="List{Organisation}"/> .
    /// </returns>
    [OperationContract]
    IList<Organisation> GetChildres(Guid parentId, string oid = "");

    /// <summary>
    /// The get concept.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Concept"/> .
    /// </returns>
    [OperationContract]
    Concept GetConcept(int id);

    /// <summary>
    /// Получает список concepts по оид
    /// </summary>
    /// <param name="oidId">
    /// The oid Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/> .
    /// </returns>
    [OperationContract]
    List<Concept> GetConceptsByOid(string oidId);

    /// <summary>
    /// Возвращает список варианатов для имени
    /// </summary>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <returns>
    /// The <see cref="List{AutoComplete}"/> .
    /// </returns>
    [OperationContract]
    List<AutoComplete> GetFirstNameAutoComplete(string prefix);

    /// <summary>
    /// Возвращает список вариантов для отчества
    /// </summary>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="nameId">
    /// The name Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{AutoComplete}"/> .
    /// </returns>
    [OperationContract]
    List<AutoComplete> GetMiddleNameAutoComplete(string prefix, Guid nameId);

    /// <summary>
    /// Возвращает список нормативно справочных данных
    /// </summary>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/> .
    /// </returns>
    [OperationContract]
    List<Concept> GetNsiRecords(string oid);

    /// <summary>
    /// Возвращает список нормативно справочных данных
    /// </summary>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/> .
    /// </returns>
    [OperationContract]
    List<Concept> GetNsiRecords(IEnumerable<string> oid);

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns> The <see cref="List{Oid}" /> . </returns>
    [OperationContract]
    List<Oid> GetOids();

    /// <summary>
    /// Возвращает пункт выдачи полисов
    /// </summary>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> .
    /// </returns>
    [OperationContract]
    Organisation GetOrganisation(Guid pdpId);

    /// <summary>
    /// Возвращет объект по ид
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="RangeNumber"/>.
    /// </returns>
    [OperationContract]
    RangeNumber GetRangeNumber(Guid id);

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns>
    ///   The <see cref="List{RangeNumber}" />.
    /// </returns>
    [OperationContract]
    List<RangeNumber> GetRangeNumbers();

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
    [OperationContract]
    Organisation GetSmoByOkatoAndOgrn(string okato, string ogrn);

    /// <summary>
    /// Получает список всех организаций
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{Organisation}"/> .
    /// </returns>
    [OperationContract]
    SearchResult<Organisation> GetSmos(SearchSmoCriteria criteria);

    /// <summary>
    /// Шаблон по ид
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    [OperationContract]
    Template GetTemplate(Guid id);

    /// <summary>
    /// Получает шаблон для печати вс по по номеру временного свидетельства заявления
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    [OperationContract]
    Template GetTemplateByStatement(Statement statement);

    /// <summary>
    ///   Все шаблоны печати вс
    /// </summary>
    /// <returns>
    ///   The <see cref="List{Template}" />.
    /// </returns>
    [OperationContract]
    List<Template> GetTemplates();

    /// <summary>
    ///   Возвращает список всех зарегестрированных ТФОМС
    /// </summary>
    /// <returns> The <see cref="List{Organisation}" /> . </returns>
    [OperationContract]
    List<Organisation> GetTfoms();

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
    /// The workstation Id.
    /// </param>
    /// <returns>
    /// The <see cref="Workstation"/> .
    /// </returns>
    [OperationContract]
    Workstation GetWorkstation(Guid workstationId);

    /// <summary>
    /// Получает список всех рабочих станций для пункта выдачи
    /// </summary>
    /// <param name="pvpId">
    /// The pvp Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Workstation}"/> .
    /// </returns>
    [OperationContract]
    List<Workstation> GetWorkstationsByPvp(Guid pvpId);

    /// <summary>
    /// Пересекается ли указанная запись с другими по диапозону. Только для диапазонов с парент ид = null,
    ///   т.е. это проверка пересечений главных диапазонов из шапки страницы
    /// </summary>
    /// <param name="range">
    /// The range.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [OperationContract]
    bool IntersectWithOther(RangeNumber range);

    /// <summary>
    /// Добавляет или обновляет запись в базе
    /// </summary>
    /// <param name="autoComplete">
    /// The AutoComplete.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SaveAutoComplete(AutoComplete autoComplete);

    /// <summary>
    /// Сохраняет указанный список пдп в базу. Все элементы которые присутствуют в базе для данной смо но отсутсвуют в
    ///   списке, будут удалены
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <param name="pvpList">
    /// The pvp List.
    /// </param>
    [OperationContract]
    void SavePdps(Guid smoId, List<Organisation> pvpList);

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="range">
    /// The range.
    /// </param>
    [OperationContract]
    void SaveRangeNumber(RangeNumber range);

    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <param name="smo">
    /// The smo.
    /// </param>
    /// <returns>
    /// The <see cref="int"/> .
    /// </returns>
    [OperationContract]
    Guid SaveSmo(Organisation smo);

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    [OperationContract]
    void SaveTemplate(Template template);

    /// <summary>
    /// Устанавливает признак IsOnline
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="isonline">
    /// The is Online.
    /// </param>
    [OperationContract]
    void SetTfomIsOnline(Guid id, bool isonline);

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
    [OperationContract]
    bool SmoCodeExists(Guid smoId, string code);

    #endregion
  }
}