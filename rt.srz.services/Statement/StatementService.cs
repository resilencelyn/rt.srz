// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementService.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
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
  using rt.srz.business.manager.rightedit;
  using rt.srz.model.dto;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;

  using Serialize.Linq.Nodes;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The statement service.
  /// </summary>
  public class StatementService : IStatementService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Вычисляет дату окончания 30 рабочих дней
    /// </summary>
    /// <param name="dateFrom">
    /// The date from.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public DateTime CalculateEndPeriodWorkingDay(DateTime dateFrom, int count)
    {
      return ObjectFactory.GetInstance<IMedicalInsuranceManager>().CalculateEndPeriodWorkingDay(dateFrom, count);
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
    /// Проверка конкретного свойства заявления
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    public void CheckPropertyStatement(Statement statement, ExpressionNode expression)
    {
      ObjectFactory.GetInstance<ICheckManager>()
                   .CheckProperty(statement, (Expression<Func<Statement, object>>)expression.ToExpression());
    }

    /// <summary>
    /// Проверка заявления, используя
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
    /// The
    ///   <see>
    ///     <cref>byte[]</cref>
    ///   </see>
    ///   .
    /// </returns>
    public byte[] ConvertToGrayScale(byte[] image)
    {
      return ObjectFactory.GetInstance<IContentManager>().ConvertToGrayScale(image);
    }

    /// <summary>
    ///   Генерация пустого фото
    /// </summary>
    /// <returns>
    ///   The
    ///   <see>
    ///     <cref>byte[]</cref>
    ///   </see>
    ///   .
    /// </returns>
    public byte[] CreateEmptyPhoto()
    {
      return ObjectFactory.GetInstance<IContentManager>().CreateEmptyPhoto();
    }

    /// <summary>
    ///   Генерация пустой подписи
    /// </summary>
    /// <returns>
    ///   The
    ///   <see>
    ///     <cref>byte[]</cref>
    ///   </see>
    ///   .
    /// </returns>
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
    /// Получает все заявления для указанной персоны
    /// </summary>
    /// <param name="insuredId">
    /// The insured Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Statement}"/> .
    /// </returns>
    public List<Statement> GetAllByInsuredId(Guid insuredId)
    {
      return
        ObjectFactory.GetInstance<IStatementManager>()
                     .GetBy(x => x.InsuredPerson.Id == insuredId)
                     .OrderBy(s => s.IsActive)
                     .ThenBy(s => s.DateFiling)
                     .ToList();
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
    /// <param name="age">
    /// The age.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/>.
    /// </returns>
    public List<Concept> GetCategoryByCitizenship(
      int citizenshipId, 
      bool isnotCitizenship, 
      bool isrefugee, 
      TimeSpan age)
    {
      var oidManager = ObjectFactory.GetInstance<IOidManager>();
      return oidManager.GetCategoryByCitizenship(citizenshipId, isnotCitizenship, isrefugee, age).ToList();
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
    /// Ворзвращает типы документов разрешающих проживание в РФ предназначениые для заполнения в зависимости от категории
    ///   гражданина
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/>.
    /// </returns>
    public List<Concept> GetDocumentResidencyTypeByCategory(int categoryId)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetDocumentResidencyTypeByCategory(categoryId).ToList();
    }

    /// <summary>
    /// Ворзвращает типы документов УДЛ предназначениые для заполнения в зависимости от категории гражданина и пола
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <param name="age">
    /// The age.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/>.
    /// </returns>
    public List<Concept> GetDocumentTypeByCategory(int categoryId, TimeSpan age)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetDocumentUdlTypeByCategory(categoryId, age).ToList();
    }

    /// <summary>
    ///   Ворзвращает типы документов регистрации предназначениые для заполнения в зависимости от категории гражданина и пола
    /// </summary>
    /// <returns>
    ///   The <see cref="List{Concept}" />.
    /// </returns>
    public List<Concept> GetDocumentTypeForRegistrationDocument()
    {
      return ObjectFactory.GetInstance<IOidManager>().GetDocumentTypeForRegistrationDocument().ToList();
    }

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <returns> The <see cref="List{Concept}" /> . </returns>
    public List<Concept> GetDocumentTypeForRepresentative()
    {
      return ObjectFactory.GetInstance<IOidManager>().GetDocumentTypeForRepresentative().ToList();
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
    /// The <see cref="List{String}"/>.
    /// </returns>
    public List<string> GetErrorsByPeriod(DateTime startDate, DateTime endDate)
    {
      return ObjectFactory.GetInstance<IStatementManager>().GetErrorsByPeriod(startDate, endDate).ToList();
    }

    /// <summary>
    /// Возвращает список типов полиса в зависимости от причины обращения
    /// </summary>
    /// <param name="causeFilling">
    /// The cause Filling.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/> .
    /// </returns>
    public List<Concept> GetFormManufacturingByCauseFilling(int causeFilling)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetFormManufacturingByCauseFilling(causeFilling).ToList();
    }

    /// <summary>
    /// Получает данные по номеру и серии документа
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <param name="series">
    /// The series.
    /// </param>
    /// <returns>
    /// The <see cref="Representative"/> .
    /// </returns>
    public Representative GetRepresentativeContactInfoByUdl(string number, string series)
    {
      return ObjectFactory.GetInstance<IRepresentativeManager>().GetRepresentativeContactInfoByUdl(number, series);
    }

    /// <summary>
    /// The get search statement result.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="SearchStatementResult"/>.
    /// </returns>
    public SearchStatementResult GetSearchStatementResult(Guid id)
    {
      return ObjectFactory.GetInstance<IStatementManager>().GetSearchStatementResult(id);
    }

    /// <summary>
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/>.
    /// </returns>
    public Setting GetSetting(string name)
    {
      return ObjectFactory.GetInstance<ISettingManager>().GetSetting(name);
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
    /// The statement Id.
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
    /// The insured Person Id.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    public Statement GetStatementByInsuredPersonId(Guid insuredPersonId)
    {
      return ObjectFactory.GetInstance<IStatementManager>().GetActiveByInsuredPersonId(insuredPersonId);
    }

    /// <summary>
    /// The get type polis by form manufacturing.
    /// </summary>
    /// <param name="formManufacturing">
    /// The form manufacturing.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/>.
    /// </returns>
    public List<Concept> GetTypePolisByFormManufacturing(int formManufacturing)
    {
      return ObjectFactory.GetInstance<IOidManager>().GetTypePolisByFormManufacturing(formManufacturing).ToList();
    }

    /// <summary>
    /// Входит ли указанная персона в объединение как главное или как второе лицо
    /// </summary>
    /// <param name="personId">
    /// The person Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    public bool InsuredInJoined(Guid personId)
    {
      return ObjectFactory.GetInstance<IInsuredPersonManager>().InsuredInJoined(personId);
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
      var statementRightToEditManager = ObjectFactory.GetInstance<IStatementRightToEditManager>();
      return statementRightToEditManager.IsRightToEdit(
                                                       propertys, 
                                                       (Expression<Func<Statement, object>>)expression.ToExpression());
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
    /// The statement.
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
    /// The <see cref="SearchResult{SearchStatementResult}"/>.
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
    /// Трим полей заявления
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void TrimStatementData(Statement statement)
    {
      ObjectFactory.GetInstance<IStatementManager>().TrimStatementData(statement);
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
        ObjectFactory.GetInstance<ICheckManager>()
                     .CheckProperty(statement, (Expression<Func<Statement, object>>)expression.ToExpression());
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
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string TryCheckProperty1(Statement statement, ExpressionNode expression)
    {
      try
      {
        ObjectFactory.GetInstance<ICheckManager>()
                     .CheckProperty(statement, (Expression<Func<Statement, object>>)expression.ToExpression());
        return string.Empty;
      }
      catch (LogicalControlException e)
      {
        return e.Message;
      }
    }

    #endregion
  }
}