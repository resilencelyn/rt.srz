// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The statement service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Statement
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  using rt.core.model.dto;
  using rt.srz.business.interfaces.logicalcontrol;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.business.manager.logicalcontrol;
  using rt.srz.business.manager.rightedit;
  using rt.srz.model.dto;
  using rt.srz.model.HL7.person.messages;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;

  using Serialize.Linq.Nodes;

  using StructureMap;
  using rt.srz.model.srz.concepts;

  using AutoComplete = rt.srz.model.srz.AutoComplete;
  using NHibernate;

  #endregion

  /// <summary>
  ///   The statement service.
  /// </summary>
  public class StatementService : IStatementService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавляет в базу настройку о том что можно включать отключать проверку данного валидатора
    /// </summary>
    /// <param name="className">
    /// тип валидатора 
    /// </param>
    public void AddAllowChangeSetting(string className)
    {
      ObjectFactory.GetInstance<ISettingManager>().AddAllowChangeSetting(className);
    }

    /// <summary>
    /// Добавляет в базу настройку проверки о том что её не надо проверять с учётом территориального фонда
    /// </summary>
    /// <param name="className">
    /// </param>
    public void AddSetting(string className)
    {
      ObjectFactory.GetInstance<ISettingManager>().AddSetting(className);
    }

    /// <summary>
    /// The check property statement.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <param name="expression">
    /// The expression. 
    /// </param>
    public void CheckPropertyStatement(Statement statement, ExpressionNode expression)
    {
      ObjectFactory.GetInstance<ICheckManager>().CheckProperty(
        statement, (Expression<Func<Statement, object>>)expression.ToExpression());
    }

    /// <summary>
    /// The check statement simple.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    public void CheckStatementSimple(Statement statement)
    {
      ObjectFactory.GetInstance<ICheckManager>().CheckStatement(statement, CheckLevelEnum.Simple);
    }

    /// <summary>
    /// The content remove.
    /// </summary>
    /// <param name="content">
    /// The content. 
    /// </param>
    public void ContentRemove(Content content)
    {
      ObjectFactory.GetInstance<IContentManager>().Delete(content);
    }

    /// <summary>
    /// The convert to gray scale.
    /// </summary>
    /// <param name="image">
    /// The image. 
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/> . 
    /// </returns>
    public byte[] ConvertToGrayScale(byte[] image)
    {
      return ObjectFactory.GetInstance<IContentManager>().ConvertToGrayScale(image);
    }

    /// <summary>
    ///   Генерация пустого фото
    /// </summary>
    /// <returns> The <see cref="byte[]" /> . </returns>
    public byte[] CreateEmptyPhoto()
    {
      return ObjectFactory.GetInstance<IContentManager>().CreateEmptyPhoto();
    }

    /// <summary>
    ///   Генерация пустой подписи
    /// </summary>
    /// <returns> The <see cref="byte[]" /> . </returns>
    public byte[] CreateEmptySign()
    {
      return ObjectFactory.GetInstance<IContentManager>().CreateEmptySign();
    }

    /// <summary>
    /// The create from example.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> . 
    /// </returns>
    public Statement CreateFromExample(Statement statement)
    {
      return ObjectFactory.GetInstance<IStatementManager>().CreateFromExample(statement);
    }

    /// <summary>
    /// Получает все заявления для указанной персоны
    /// </summary>
    /// <param name="insuredId">
    /// The insured Id. 
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Statement> GetAllByInsuredId(Guid insuredId)
    {
      return
        ObjectFactory.GetInstance<IStatementManager>().GetBy(x => x.InsuredPerson.Id == insuredId).OrderBy(
          s => s.IsActive).ThenBy(s => s.DateFiling).ToList();
    }

    /// <summary>
    /// Возвращает объект AutoComplete
    /// </summary>
    /// <param name="Id">
    /// </param>
    /// <returns>
    /// The <see cref="AutoComplete"/> . 
    /// </returns>
    public AutoComplete GetAutoComplete(Guid Id)
    {
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetById(Id);
    }

    /// <summary>
    /// The get category by citizenship.
    /// </summary>
    /// <param name="citizenshipId">
    /// The citizenship id. 
    /// </param>
    /// <param name="isnotCitizenship">
    /// The isnot citizenship. 
    /// </param>
    /// <param name="isrefugee">
    /// The isrefugee. 
    /// </param>
    /// <param name="age"> </param>
    /// <returns>
    /// The <see>
    ///                 <cref>IList</cref>
    ///               </see> . 
    /// </returns>
    public IList<Concept> GetCategoryByCitizenship(int citizenshipId, bool isnotCitizenship, bool isrefugee, TimeSpan age)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetCategoryByCitizenship(
        citizenshipId, isnotCitizenship, isrefugee, age);
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
      return id < 0 ? null : ObjectFactory.GetInstance<IConceptCacheManager>().Single(x => x.Id == id);
    }

    /// <summary>
    /// The get content record.
    /// </summary>
    /// <param name="id">
    /// The id. 
    /// </param>
    /// <returns>
    /// The <see cref="Content"/> . 
    /// </returns>
    public Content GetContentRecord(Guid id)
    {
      return ObjectFactory.GetInstance<IContentManager>().GetById(id);
    }

    /// <summary>
    /// The get document residency type by category.
    /// </summary>
    /// <param name="categoryId">
    /// The category id. 
    /// </param>
    /// <returns>
    /// The <see>
    ///                 <cref>IList</cref>
    ///               </see> . 
    /// </returns>
    public IList<Concept> GetDocumentResidencyTypeByCategory(int categoryId)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetDocumentResidencyTypeByCategory(categoryId);
    }

    /// <summary>
    /// The get document type by category.
    /// </summary>
    /// <param name="categoryId">
    /// The category id. 
    /// </param>
    /// <param name="age">
    /// The age. 
    /// </param>
    /// <returns>
    /// The <see>
    ///                 <cref>IList</cref>
    ///               </see> . 
    /// </returns>
    public IList<Concept> GetDocumentTypeByCategory(int categoryId, TimeSpan age)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetDocumentUdlTypeByCategory(categoryId, age);
    }

    /// <summary>
    ///   The get document type for registration document
    /// </summary>
    /// <returns> The <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   . </returns>
    public IList<Concept> GetDocumentTypeForRegistrationDocument()
    {
      return ObjectFactory.GetInstance<IOidManager>().GetDocumentTypeForRegistrationDocument();
    }

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <param name="categoryId"> The category id. </param>
    /// <param name="age"> The age. </param>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Concept> GetDocumentTypeForRepresentative()
    {
      return ObjectFactory.GetInstance<IOidManager>().GetDocumentTypeForRepresentative();
    }

    /// <summary>
    /// Возвращает список варианатов для имени
    /// </summary>
    /// <param name="prefix">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<AutoComplete> GetFirstNameAutoComplete(string prefix)
    {
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetFirstNameAutoComplete(prefix);
    }

    /// <summary>
    /// Возвращает список типов полиса в зависимости от причины обращения
    /// </summary>
    /// <param name="causeFilling">
    /// The cause Filling. 
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Concept> GetFormManufacturingByCauseFilling(int causeFilling)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetFormManufacturingByCauseFilling(causeFilling);
    }

    /// <summary>
    /// Возвращает список вариантов для отчества
    /// </summary>
    /// <param name="prefix">
    /// </param>
    /// <param name="nameId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<AutoComplete> GetMiddleNameAutoComplete(string prefix, Guid nameId)
    {
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetMiddleNameAutoComplete(prefix, nameId);
    }

    /// <summary>
    /// Возвращает список нормативно справочных данных
    /// </summary>
    /// <param name="oid">
    /// The oid. 
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Concept> GetNsiRecords(string oid)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetNsiRecords(oid);
    }

    /// <summary>
    /// Возвращает список нормативно справочных данных
    /// </summary>
    /// <param name="oid">
    /// The oid. 
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Concept> GetNsiRecords(IEnumerable<string> oid)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetNsiRecords(oid);
    }

    /// <summary>
    /// Получает данные по номеру и серии документа
    /// </summary>
    /// <param name="number">
    /// </param>
    /// <param name="series">
    /// </param>
    /// <returns>
    /// The <see cref="Representative"/> . 
    /// </returns>
    public Representative GetRepresentativeContactInfoByUdl(string number, string series)
    {
      return ObjectFactory.GetInstance<IRepresentativeManager>().GetRepresentativeContactInfoByUdl(number, series);
    }

    /// <summary>
    /// The get setting current user.
    /// </summary>
    /// <param name="nameSetting">
    /// The name setting. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public string GetSettingCurrentUser(string nameSetting)
    {
      return ObjectFactory.GetInstance<ISettingManager>().GetSettingCurrentUser(nameSetting);
    }

    /// <summary>
    /// Возвращает ранее сохраненное завяление
    /// </summary>
    /// <param name="statementId">
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> . 
    /// </returns>
    public Statement GetStatement(Guid statementId)
    {
      return ObjectFactory.GetInstance<IStatementManager>().GetById(statementId);
    }

    /// <summary>
    /// Получает заявление по InsuredPersonId с IsActive = 1
    /// </summary>
    /// <param name="insuredPersonId">
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> . 
    /// </returns>
    public Statement GetStatementByInsuredPersonId(Guid insuredPersonId)
    {
      return ObjectFactory.GetInstance<IStatementManager>().GetActiveByInsuredPersonId(insuredPersonId);
    }

    /// <summary>
    /// The get za 7.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="ZPI_ZA7"/> . 
    /// </returns>
    public ZPI_ZA7 GetZa7(Statement statement)
    {
      return ObjectFactory.GetInstance<IStatementHl7Manager>().GetZa7(statement);
    }

    /// <summary>
    /// Входит ли указанная персона в объединение как главное или как второе лицо
    /// </summary>
    /// <param name="personId">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public bool InsuredInJoined(Guid personId)
    {
      return ObjectFactory.GetInstance<IInsuredPersonManager>().InsuredInJoined(personId);
    }

    public SearchStatementResult GetSearchStatementResult(Guid id)
    {
      return ObjectFactory.GetInstance<IStatementManager>().GetSearchStatementResult(id);
    }

    /// <summary>
    /// The is right to edit.
    /// </summary>
    /// <param name="propertys">
    /// The propertys. 
    /// </param>
    /// <param name="expression">
    /// The expression. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public bool IsRightToEdit(IEnumerable<Concept> propertys, ExpressionNode expression)
    {
      return ObjectFactory.GetInstance<IStatementRightToEditManager>().IsRightToEdit(
        propertys, (Expression<Func<Statement, object>>)expression.ToExpression());
    }

    /// <summary>
    /// Удаляет из базы настройку о том что можно включать отключать проверку данного валидатора
    /// </summary>
    /// <param name="className">
    /// тип валидатора 
    /// </param>
    public void RemoveAllowChangeSetting(string className)
    {
      ObjectFactory.GetInstance<ISettingManager>().RemoveAllowChangeSetting(className);
    }

    /// <summary>
    /// Удаляет настройку из базы которую надо стало проверять
    /// </summary>
    /// <param name="className">
    /// </param>
    public void RemoveSetting(string className)
    {
      ObjectFactory.GetInstance<ISettingManager>().RemoveSetting(className);
    }

    /// <summary>
    /// The remove statement.
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    public void CanceledStatement(Guid statementId)
    {
      ObjectFactory.GetInstance<IStatementManager>().CanceledStatement(statementId);
    }

    /// <summary>
    /// The save content record.
    /// </summary>
    /// <param name="typeContent">
    /// The type content. 
    /// </param>
    /// <param name="content">
    /// The content. 
    /// </param>
    /// <param name="fileName">
    /// The file Name. 
    /// </param>
    /// <returns>
    /// The <see cref="Content"/> . 
    /// </returns>
    public Content SaveContentRecord(int typeContent, byte[] content, string fileName = null)
    {
      return ObjectFactory.GetInstance<IContentManager>().SaveContentRecord(typeContent, content, fileName);
    }

    /// <summary>
    /// Сохраняет заявление
    /// </summary>
    /// <param name="statement">
    ///   The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> . 
    /// </returns>
    public Statement SaveStatement(Statement statement)
    {
      return ObjectFactory.GetInstance<IStatementManager>().SaveStatement(statement);
    }

    /// <summary>
    /// Осуществляет поиск заявлений по заданному критерию
    /// </summary>
    /// <param name="criteria">
    /// The criteria. 
    /// </param>
    /// <returns>
    /// The <see>
    ///                 <cref>IList</cref>
    ///               </see> . 
    /// </returns>
    public SearchResult<SearchStatementResult> Search(SearchStatementCriteria criteria)
    {
      return ObjectFactory.GetInstance<IStatementSearchManager>().Search(criteria);
    }

    /// <summary>
    /// The set setting current user.
    /// </summary>
    /// <param name="nameSetting">
    /// The name setting. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    public void SetSettingCurrentUser(string nameSetting, string value)
    {
      ObjectFactory.GetInstance<ISettingManager>().SetSettingCurrentUser(nameSetting, value);
    }

    /// <summary>
    /// The try check property.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <param name="expression">
    /// The expression. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public bool TryCheckProperty(Statement statement, ExpressionNode expression)
    {
      try
      {
        ObjectFactory.GetInstance<ICheckManager>().CheckProperty(
          statement, (Expression<Func<Statement, object>>)expression.ToExpression());
        return true;
      }
      catch (LogicalControlException)
      {
        return false;
      }
    }

    /// <summary>
    /// Проверяет проверку и возвращает информацию об ошибке (текст ошибки из исключения)
    /// </summary>
    public string TryCheckProperty1(Statement statement, ExpressionNode expression)
    {
      try
      {
        ObjectFactory.GetInstance<ICheckManager>().CheckProperty(
          statement, (Expression<Func<Statement, object>>)expression.ToExpression());
        return string.Empty;
      }
      catch (LogicalControlException e)
      {
        return e.Message;
      }
    }

    /// <summary>
    /// The un bind statement.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    public void UnBindStatement(Statement statement)
    {
      ObjectFactory.GetInstance<IStatementManager>().UnBindStatement(statement);
    }

    /// <summary>
    /// Получает ошибки существующие в заявлениях за указанный период
    /// </summary>
    /// <param name="startDate">
    /// The start Date.
    /// </param>
    /// <param name="endDate">
    /// The end Date.
    /// </param>
    /// <returns>
    /// The <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<string> GetErrorsByPeriod(DateTime startDate, DateTime endDate)
    {
      return ObjectFactory.GetInstance<IStatementManager>().GetErrorsByPeriod(startDate, endDate);
    }

    /// <summary>
    /// Удаление инфы о смерти
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    public void DeleteDeathInfo(Guid statementId)
    {
      ObjectFactory.GetInstance<IInsuredPersonManager>().DeleteDeathInfo(statementId);
    }

    /// <summary>
    /// The get type polis by form manufacturing.
    /// </summary>
    /// <param name="formManufacturing">
    /// The form manufacturing.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Concept> GetTypePolisByFormManufacturing(int formManufacturing)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetTypePolisByFormManufacturing(formManufacturing);
    }

    /// <summary>
    /// Трим полей заявления
    /// </summary>
    /// <param name="statement"></param>
    public void TrimStatementData(Statement statement)
    {
      ObjectFactory.GetInstance<IStatementManager>().TrimStatementData(statement);
    }

    #endregion
  }
}