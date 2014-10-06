// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NsiGateInternal.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The nsi gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.NSI
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.core.services.aspects;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The nsi gate.
  /// </summary>
  public class NsiGateInternal : InterceptedBase, INsiService
  {
    #region Fields

    /// <summary>
    ///   The _service.
    /// </summary>
    private readonly INsiService _service = new NsiService();

    #endregion

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
      return InvokeInterceptors(() => _service.AddOrUpdateFirstMiddleName(autoComplete));
    }

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="range">
    /// </param>
    public void AddOrUpdateRangeNumber(RangeNumber range)
    {
      InvokeInterceptors(() => _service.AddOrUpdateRangeNumber(range));
    }

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="template">
    /// </param>
    public void AddOrUpdateTemplate(Template template)
    {
      InvokeInterceptors(() => _service.AddOrUpdateTemplate(template));
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
      return InvokeInterceptors(() => _service.CreateCopyOfTemplateVs(id));
    }

    /// <summary>
    /// Удаление имени или отчества
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeleteFirstMiddleName(Guid id)
    {
      InvokeInterceptors(() => _service.DeleteFirstMiddleName(id));
    }

    /// <summary>
    /// Удаление диапозона
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeleteRangeNumber(Guid id)
    {
      InvokeInterceptors(() => _service.DeleteRangeNumber(id));
    }

    /// <summary>
    /// Удаление шаблона печати вс
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeleteTemplateVs(Guid id)
    {
      InvokeInterceptors(() => _service.DeleteTemplateVs(id));
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
      return InvokeInterceptors(() => _service.FirstMiddleNameExists(firstMiddleName));
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
      return InvokeInterceptors(() => _service.GetConcept(id));
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
      return InvokeInterceptors(() => _service.GetConceptsByOid(oidId));
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
      return InvokeInterceptors(() => _service.GetFirstMiddleName(id));
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
      return InvokeInterceptors(() => _service.GetFirstMiddleNames(criteria));
    }

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Oid> GetOids()
    {
      return InvokeInterceptors(() => _service.GetOids());
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
      return InvokeInterceptors(() => _service.GetRangeNumber(id));
    }

    /// <summary>
    /// Зачитывает все записи
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<RangeNumber> GetRangeNumbers()
    {
      return InvokeInterceptors(() => _service.GetRangeNumbers());
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
      return InvokeInterceptors(() => _service.GetTemplate(id));
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
      return InvokeInterceptors(() => _service.GetTemplateVsByStatement(statement));
    }

    /// <summary>
    /// Все шаблоны печати вс
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Template> GetTemplates()
    {
      return InvokeInterceptors(() => _service.GetTemplates());
    }

    /// <summary>
    /// Пересекается ли указанная запись с другими по диапозону
    /// </summary>
    /// <param name="range">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool IntersectWithOther(RangeNumber range)
    {
      return InvokeInterceptors(() => _service.IntersectWithOther(range));
    }

    #endregion
  }
}