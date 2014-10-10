// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegulatoryClient.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The nsi gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.services
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.core.services.registry;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The nsi gate.
  /// </summary>
  public class RegulatoryClient : ServiceClient<IRegulatoryService>, IRegulatoryService
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
    public Template CreateCopyOfTemplate(Guid id)
    {
      return InvokeInterceptors(() => Service.CreateCopyOfTemplate(id));
    }

    /// <summary>
    /// Удаление имени или отчества
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeleteAutoComplete(Guid id)
    {
      InvokeInterceptors(() => Service.DeleteAutoComplete(id));
    }

    /// <summary>
    /// Удаление pdp (set пометка IsActive=false)
    /// </summary>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    public void DeleteOrganisation(Guid pdpId)
    {
      InvokeInterceptors(() => Service.DeleteOrganisation(pdpId));
    }

    /// <summary>
    /// Удаление диапозона
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeleteRangeNumber(Guid id)
    {
      InvokeInterceptors(() => Service.DeleteRangeNumber(id));
    }

    /// <summary>
    /// Удаление шаблона печати вс
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeleteTemplateVs(Guid id)
    {
      InvokeInterceptors(() => Service.DeleteTemplateVs(id));
    }

    /// <summary>
    /// Проверяет существует ли уже запись в базе с таким же именем, типом, полом
    /// </summary>
    /// <param name="firstMiddleName">
    /// The first Middle Name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    public bool FirstMiddleNameExists(AutoComplete firstMiddleName)
    {
      return InvokeInterceptors(() => Service.FirstMiddleNameExists(firstMiddleName));
    }

    /// <summary>
    /// Получает запись по ид
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="AutoComplete"/> .
    /// </returns>
    public AutoComplete GetAutoComplete(Guid id)
    {
      return InvokeInterceptors(() => Service.GetAutoComplete(id));
    }

    /// <summary>
    /// Получает результат по критерию для имён и отчеств
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{AutoComplete}"/> .
    /// </returns>
    public SearchResult<AutoComplete> GetAutoCompleteByCriteria(SearchAutoCompleteCriteria criteria)
    {
      return InvokeInterceptors(() => Service.GetAutoCompleteByCriteria(criteria));
    }

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
    public IList<Organisation> GetChildres(Guid parentId, string oid = "")
    {
      return InvokeInterceptors(() => Service.GetChildres(parentId, oid));
    }

    /// <summary>
    /// The get concept.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Concept"/> .
    /// </returns>
    public Concept GetConcept(int id)
    {
      return InvokeInterceptors(() => Service.GetConcept(id));
    }

    /// <summary>
    /// Получает список concepts по оид
    /// </summary>
    /// <param name="oidId">
    /// The oid Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/> .
    /// </returns>
    public List<Concept> GetConceptsByOid(string oidId)
    {
      return InvokeInterceptors(() => Service.GetConceptsByOid(oidId));
    }

    /// <summary>
    /// The get first level by tfoms.
    /// </summary>
    /// <param name="tfoms">
    /// The tfoms.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/> .
    /// </returns>
    public Kladr GetFirstLevelByTfoms(Organisation tfoms)
    {
      return InvokeInterceptors(() => Service.GetFirstLevelByTfoms(tfoms));
    }

    /// <summary>
    /// Возвращает список варианатов для имени
    /// </summary>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <returns>
    /// The <see cref="List{AutoComplete}"/> .
    /// </returns>
    public List<AutoComplete> GetFirstNameAutoComplete(string prefix)
    {
      return InvokeInterceptors(() => Service.GetFirstNameAutoComplete(prefix));
    }

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
    public List<AutoComplete> GetMiddleNameAutoComplete(string prefix, Guid nameId)
    {
      return InvokeInterceptors(() => Service.GetMiddleNameAutoComplete(prefix, nameId));
    }

    /// <summary>
    /// Возвращает список нормативно справочных данных
    /// </summary>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/> .
    /// </returns>
    public List<Concept> GetNsiRecords(string oid)
    {
      return InvokeInterceptors(() => Service.GetNsiRecords(oid));
    }

    /// <summary>
    /// Возвращает список нормативно справочных данных
    /// </summary>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/> .
    /// </returns>
    public List<Concept> GetNsiRecords(IEnumerable<string> oid)
    {
      return InvokeInterceptors(() => Service.GetNsiRecords(oid));
    }

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns> The <see cref="List{Oid}" /> . </returns>
    public List<Oid> GetOids()
    {
      return InvokeInterceptors(() => Service.GetOids());
    }

    /// <summary>
    /// Возвращает пункт выдачи полисов
    /// </summary>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> .
    /// </returns>
    public Organisation GetOrganisation(Guid pdpId)
    {
      return InvokeInterceptors(() => Service.GetOrganisation(pdpId));
    }

    /// <summary>
    /// Возвращет объект по ид
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="RangeNumber"/>.
    /// </returns>
    public RangeNumber GetRangeNumber(Guid id)
    {
      return InvokeInterceptors(() => Service.GetRangeNumber(id));
    }

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns>
    ///   The <see cref="List{RangeNumber}" />.
    /// </returns>
    public List<RangeNumber> GetRangeNumbers()
    {
      return InvokeInterceptors(() => Service.GetRangeNumbers());
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
      return InvokeInterceptors(() => Service.GetSmoByOkatoAndOgrn(okato, ogrn));
    }

    /// <summary>
    /// Получает список всех организаций
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{Organisation}"/> .
    /// </returns>
    public SearchResult<Organisation> GetSmos(SearchSmoCriteria criteria)
    {
      return InvokeInterceptors(() => Service.GetSmos(criteria));
    }

    /// <summary>
    /// Шаблон по ид
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    public Template GetTemplate(Guid id)
    {
      return InvokeInterceptors(() => Service.GetTemplate(id));
    }

    /// <summary>
    /// Получает шаблон для печати вс по по номеру временного свидетельства заявления
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    public Template GetTemplateByStatement(Statement statement)
    {
      return InvokeInterceptors(() => Service.GetTemplateByStatement(statement));
    }

    /// <summary>
    ///   Все шаблоны печати вс
    /// </summary>
    /// <returns>
    ///   The <see cref="List{Template}" />.
    /// </returns>
    public List<Template> GetTemplates()
    {
      return InvokeInterceptors(() => Service.GetTemplates());
    }

    /// <summary>
    ///   Возвращает список всех зарегестрированных ТФОМС
    /// </summary>
    /// <returns> The <see cref="List{Organisation}" /> . </returns>
    public List<Organisation> GetTfoms()
    {
      return InvokeInterceptors(() => Service.GetTfoms());
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
      return InvokeInterceptors(() => Service.GetTfomsByOkato(okato));
    }

    /// <summary>
    /// Получает рабочее место по ид
    /// </summary>
    /// <param name="workstationId">
    /// The workstation Id.
    /// </param>
    /// <returns>
    /// The <see cref="Workstation"/> .
    /// </returns>
    public Workstation GetWorkstation(Guid workstationId)
    {
      return InvokeInterceptors(() => Service.GetWorkstation(workstationId));
    }

    /// <summary>
    /// Получает список всех рабочих станций для пункта выдачи
    /// </summary>
    /// <param name="pvpId">
    /// The pvp Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Workstation}"/> .
    /// </returns>
    public List<Workstation> GetWorkstationsByPvp(Guid pvpId)
    {
      return InvokeInterceptors(() => Service.GetWorkstationsByPvp(pvpId));
    }

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
    public bool IntersectWithOther(RangeNumber range)
    {
      return InvokeInterceptors(() => Service.IntersectWithOther(range));
    }

    /// <summary>
    /// Добавляет или обновляет запись в базе
    /// </summary>
    /// <param name="autoComplete">
    /// The AutoComplete.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    public Guid SaveAutoComplete(AutoComplete autoComplete)
    {
      return InvokeInterceptors(() => Service.SaveAutoComplete(autoComplete));
    }

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
    public void SavePdps(Guid smoId, List<Organisation> pvpList)
    {
      InvokeInterceptors(() => Service.SavePdps(smoId, pvpList));
    }

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="range">
    /// The range.
    /// </param>
    public void SaveRangeNumber(RangeNumber range)
    {
      InvokeInterceptors(() => Service.SaveRangeNumber(range));
    }

    /// <summary>
    /// Сохраняет изменения
    /// </summary>
    /// <param name="smo">
    /// The smo.
    /// </param>
    /// <returns>
    /// The <see cref="int"/> .
    /// </returns>
    public Guid SaveSmo(Organisation smo)
    {
      return InvokeInterceptors(() => Service.SaveSmo(smo));
    }

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    public void SaveTemplate(Template template)
    {
      InvokeInterceptors(() => Service.SaveTemplate(template));
    }

    /// <summary>
    /// Устанавливает признак IsOnline
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="isonline">
    /// The is Online.
    /// </param>
    public void SetTfomIsOnline(Guid id, bool isonline)
    {
      InvokeInterceptors(() => Service.SetTfomIsOnline(id, isonline));
    }

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
    public bool SmoCodeExists(Guid smoId, string code)
    {
      return InvokeInterceptors(() => Service.SmoCodeExists(smoId, code));
    }

    #endregion
  }
}