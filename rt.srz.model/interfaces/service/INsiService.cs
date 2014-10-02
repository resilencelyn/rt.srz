// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INsiService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
  using rt.srz.model.dto;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The NsiService interface.
  /// </summary>
  [ServiceContract]
  public interface INsiService
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
    [OperationContract]
    Guid AddOrUpdateFirstMiddleName(AutoComplete autoComplete);

    /// <summary>
    /// Удаление имени или отчества
    /// </summary>
    /// <param name="id">
    /// </param>
    [OperationContract]
    void DeleteFirstMiddleName(Guid id);

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
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Concept> GetConceptsByOid(string oidId);

    /// <summary>
    /// Получает запись по ид
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <returns>
    /// The <see cref="AutoComplete"/> . 
    /// </returns>
    [OperationContract]
    AutoComplete GetFirstMiddleName(Guid id);

    /// <summary>
    /// Получает результат по критерию для имён и отчеств
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> . 
    /// </returns>
    [OperationContract]
    SearchResult<AutoComplete> GetFirstMiddleNames(SearchAutoCompleteCriteria criteria);

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Oid> GetOids();

    #region Range Number

    /// <summary>
    /// Зачитывает все записи
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    IList<RangeNumber> GetRangeNumbers();

    /// <summary>
    /// Удаление диапозона
    /// </summary>
    /// <param name="id"></param>
    [OperationContract]
    void DeleteRangeNumber(Guid id);

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="range"></param>
    [OperationContract]
    void AddOrUpdateRangeNumber(RangeNumber range);

    /// <summary>
    /// Возвращет объект по ид
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [OperationContract]
    RangeNumber GetRangeNumber(Guid id);

    /// <summary>
    /// Пересекается ли указанная запись с другими по диапозону
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    [OperationContract]
    bool IntersectWithOther(RangeNumber range);

    /// <summary>
    /// Получает шаблон для печати вс по по номеру временного свидетельства заявления
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    [OperationContract]
    Template GetTemplateVsByStatement(Statement statement);

    /// <summary>
    /// Шаблон по ид
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [OperationContract]
    Template GetTemplate(Guid id);

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="template"></param>
    [OperationContract]
    void AddOrUpdateTemplate(Template template);

    /// <summary>
    /// Все шаблоны печати вс
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    IList<Template> GetTemplates();


    /// <summary>
    /// Удаление шаблона печати вс
    /// </summary>
    /// <param name="id"></param>
    [OperationContract]
    void DeleteTemplateVs(Guid id);

    /// <summary>
    /// Создание копии шаблона печати
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [OperationContract]
    Template CreateCopyOfTemplateVs(Guid id);

    #endregion

    #endregion
  }
}