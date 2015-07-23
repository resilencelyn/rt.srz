// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegulatoryService.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The nsi service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Regulatory
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;

  using rt.core.model.dto;
  using rt.core.model.interfaces;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The nsi service.
  /// </summary>
  public class RegulatoryService : IRegulatoryService
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
      return ObjectFactory.GetInstance<ITemplateManager>().CreateCopyOfTemplate(id);
    }

    /// <summary>
    /// Удаление имени или отчества
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeleteAutoComplete(Guid id)
    {
      ObjectFactory.GetInstance<IAutoCompleteManager>().Delete(x => x.Id == id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Удаление pdp (set пометка IsActive=false)
    /// </summary>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    public void DeleteOrganisation(Guid pdpId)
    {
      ObjectFactory.GetInstance<IOrganisationManager>().DeleteOrganisation(pdpId);
    }

    /// <summary>
    /// Удаление диапозона
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeleteRangeNumber(Guid id)
    {
      // надо удалять ещё и все дочерние интервалы (та же таблица RangeNumber)
      ObjectFactory.GetInstance<IRangeNumberManager>().Delete(x => x.Parent.Id == id);
      ObjectFactory.GetInstance<IRangeNumberManager>().Delete(x => x.Id == id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Удаление шаблона печати вс
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeleteTemplateVs(Guid id)
    {
      ObjectFactory.GetInstance<ITemplateManager>().Delete(x => x.Id == id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
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
      return ObjectFactory.GetInstance<IAutoCompleteManager>().AutoCompleteExists(firstMiddleName);
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
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetById(id);
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
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetAutoCompleteByCriteria(criteria);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().GetChildrens(parentId, oid);
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
      return ObjectFactory.GetInstance<IConceptCacheManager>().GetById(id);
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
      return ObjectFactory.GetInstance<IConceptCacheManager>().GetConceptsByOid(oidId).ToList();
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
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetFirstNameAutoComplete(prefix).ToList();
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
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetMiddleNameAutoComplete(prefix, nameId).ToList();
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
      return ObjectFactory.GetInstance<IOidManager>().GetNsiRecords(oid).ToList();
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
      return ObjectFactory.GetInstance<IOidManager>().GetNsiRecords(oid).ToList();
    }

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns> The <see cref="List{Oid}" /> . </returns>
    public List<Oid> GetOids()
    {
      return ObjectFactory.GetInstance<IOidManager>().GetAll(int.MaxValue).ToList();
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
      return ObjectFactory.GetInstance<IOrganisationManager>().GetById(pdpId);
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
      return ObjectFactory.GetInstance<IRangeNumberManager>().GetById(id);
    }

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns>
    ///   The <see cref="List{RangeNumber}" />.
    /// </returns>
    public List<RangeNumber> GetRangeNumbers()
    {
      return ObjectFactory.GetInstance<IRangeNumberManager>().GetRangeNumbers().ToList();
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
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{Organisation}"/> .
    /// </returns>
    public SearchResult<Organisation> GetSmos(SearchSmoCriteria criteria)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetSmosByCriteria(criteria);
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
      return ObjectFactory.GetInstance<ITemplateManager>().GetById(id);
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
      return ObjectFactory.GetInstance<IRangeNumberManager>().GetTemplateByStatement(statement);
    }

    /// <summary>
    ///   Все шаблоны печати вс
    /// </summary>
    /// <returns>
    ///   The <see cref="List{Template}" />.
    /// </returns>
    public List<Template> GetTemplates()
    {
      return ObjectFactory.GetInstance<ITemplateManager>().GetAll(int.MaxValue).ToList();
    }

    /// <summary>
    ///   Возвращает список всех зарегестрированных ТФОМС
    /// </summary>
    /// <returns> The <see cref="List{Organisation}" /> . </returns>
    public List<Organisation> GetTfoms()
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetTfoms().OrderBy(f => f.ShortName).ToList();
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
    /// The workstation Id.
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
    /// <param name="pvpId">
    /// The pvp Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Workstation}"/> .
    /// </returns>
    public List<Workstation> GetWorkstationsByPvp(Guid pvpId)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetBy(w => w.PointDistributionPolicy.Id == pvpId).ToList();
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
      return ObjectFactory.GetInstance<IRangeNumberManager>().IntersectWithOther(range);
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
      ObjectFactory.GetInstance<IAutoCompleteManager>().SaveOrUpdate(autoComplete);
      return autoComplete.Id;
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
      ObjectFactory.GetInstance<IOrganisationManager>().SavePdps(smoId, pvpList);
    }

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="range">
    /// The range.
    /// </param>
    public void SaveRangeNumber(RangeNumber range)
    {
      ObjectFactory.GetInstance<IRangeNumberManager>().AddOrUpdateRangeNumber(range);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().SaveSmo(smo);
    }

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    public void SaveTemplate(Template template)
    {
      ObjectFactory.GetInstance<ITemplateManager>().AddOrUpdateTemplate(template);
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
      ObjectFactory.GetInstance<IOrganisationManager>().SetTfomIsOnline(id, isonline);
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
      return ObjectFactory.GetInstance<IOrganisationManager>().SmoCodeExists(smoId, code);
    }

    #endregion
  }
}