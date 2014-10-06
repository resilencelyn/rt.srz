// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NsiService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The nsi service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.NSI
{
  #region

  using System;
  using System.Collections.Generic;

  using NHibernate;

  using rt.core.model.dto;
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
  public class NsiService : INsiService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавляет или обновляет запись в базе
    /// </summary>
    /// <param name="autoComplete">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    public Guid AddOrUpdateFirstMiddleName(AutoComplete autoComplete)
    {
      ObjectFactory.GetInstance<IAutoCompleteManager>().SaveOrUpdate(autoComplete);
      return autoComplete.Id;
    }

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="range">
    /// </param>
    public void AddOrUpdateRangeNumber(RangeNumber range)
    {
      ObjectFactory.GetInstance<IRangeNumberManager>().AddOrUpdateRangeNumber(range);
    }

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="template">
    /// </param>
    public void AddOrUpdateTemplate(Template template)
    {
      ObjectFactory.GetInstance<ITemplateManager>().AddOrUpdateTemplate(template);
    }

    /// <summary>
    /// Создание копии шаблона печати
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    public Template CreateCopyOfTemplateVs(Guid id)
    {
      return ObjectFactory.GetInstance<ITemplateManager>().CreateCopyOfTemplateVs(id);
    }

    /// <summary>
    /// Удаление имени или отчества
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeleteFirstMiddleName(Guid id)
    {
      ObjectFactory.GetInstance<IAutoCompleteManager>().Delete(x => x.Id == id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Удаление диапозона
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeleteRangeNumber(Guid id)
    {
      // сейчас функционал не используется, но если понадобится то надо удалять ещё и все дочерние интервалы (та же таблица RangeNumber)
      ObjectFactory.GetInstance<IRangeNumberManager>().Delete(x => x.Id == id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Удаление шаблона печати вс
    /// </summary>
    /// <param name="id">
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
      return ObjectFactory.GetInstance<IAutoCompleteManager>().FirstMiddleNameExists(firstMiddleName);
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
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<Concept> GetConceptsByOid(string oidId)
    {
      return ObjectFactory.GetInstance<IConceptCacheManager>().GetConceptsByOid(oidId);
    }

    /// <summary>
    /// Получает запись по ид
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <returns>
    /// The <see cref="AutoComplete"/> .
    /// </returns>
    public AutoComplete GetFirstMiddleName(Guid id)
    {
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetById(id);
    }

    /// <summary>
    /// Получает результат по критерию для имён и отчеств
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    public SearchResult<AutoComplete> GetFirstMiddleNames(SearchAutoCompleteCriteria criteria)
    {
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetFirstMiddleNames(criteria);
    }

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Oid> GetOids()
    {
      return ObjectFactory.GetInstance<IOidManager>().GetAll(int.MaxValue);
    }

    /// <summary>
    /// Возвращет объект по ид
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <returns>
    /// The <see cref="RangeNumber"/>.
    /// </returns>
    public RangeNumber GetRangeNumber(Guid id)
    {
      return ObjectFactory.GetInstance<IRangeNumberManager>().GetById(id);
    }

    /// <summary>
    /// Зачитывает все записи
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<RangeNumber> GetRangeNumbers()
    {
      return ObjectFactory.GetInstance<IRangeNumberManager>().GetRangeNumbers();
    }

    /// <summary>
    /// Шаблон по ид
    /// </summary>
    /// <param name="id">
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
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    public Template GetTemplateVsByStatement(Statement statement)
    {
      return ObjectFactory.GetInstance<IRangeNumberManager>().GetTemplateVsByStatement(statement);
    }

    /// <summary>
    /// Все шаблоны печати вс
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Template> GetTemplates()
    {
      return ObjectFactory.GetInstance<ITemplateManager>().GetAll(int.MaxValue);
    }

    /// <summary>
    /// Пересекается ли указанная запись с другими по диапозону. Только для диапазонов с парент ид = null,
    ///   т.е. это проверка пересечений главных диапазонов из шапки страницы
    /// </summary>
    /// <param name="range">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool IntersectWithOther(RangeNumber range)
    {
      return ObjectFactory.GetInstance<IRangeNumberManager>().IntersectWithOther(range);
    }

    #endregion
  }
}