// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementGateInternal.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Statement
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using rt.core.model.dto;
  using rt.core.services.aspects;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using Serialize.Linq.Nodes;

  #endregion

  /// <summary>
  ///   The statement gate.
  /// </summary>
  public class StatementGateInternal : InterceptedBase, IStatementService
  {
    #region Fields

    /// <summary>
    ///   The service.
    /// </summary>
    protected readonly IStatementService Service = new StatementService();

    #endregion

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
      return InvokeInterceptors(() => Service.CalculateEndPeriodWorkingDay(dateFrom, count));
    }

    /// <summary>
    /// The remove statement.
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    public void CanceledStatement(Guid statementId)
    {
      InvokeInterceptors(() => Service.CanceledStatement(statementId));
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
      InvokeInterceptors(() => Service.CheckPropertyStatement(statement, expression));
    }

    /// <summary>
    /// Проверка заявления, используя
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void CheckStatementSimple(Statement statement)
    {
      InvokeInterceptors(() => Service.CheckStatementSimple(statement));
    }

    /// <summary>
    /// The content remove.
    /// </summary>
    /// <param name="content">
    /// The content.
    /// </param>
    public void ContentRemove(Content content)
    {
      InvokeInterceptors(() => Service.ContentRemove(content));
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
      return InvokeInterceptors(() => Service.ConvertToGrayScale(image));
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
      return InvokeInterceptors(() => Service.CreateEmptyPhoto());
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
      return InvokeInterceptors(() => Service.CreateEmptySign());
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
      return InvokeInterceptors(() => Service.CreateFromExample(statement));
    }

    /// <summary>
    /// Удаление инфы о смерти
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    public void DeleteDeathInfo(Guid statementId)
    {
      InvokeInterceptors(() => Service.DeleteDeathInfo(statementId));
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
      return InvokeInterceptors(() => Service.GetAllByInsuredId(insuredId));
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
      return InvokeInterceptors(() => Service.GetCategoryByCitizenship(citizenshipId, isnotCitizenship, isrefugee, age));
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
      return InvokeInterceptors(() => Service.GetContentRecord(id));
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
      return InvokeInterceptors(() => Service.GetDocumentResidencyTypeByCategory(categoryId));
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
      return InvokeInterceptors(() => Service.GetDocumentTypeByCategory(categoryId, age));
    }

    /// <summary>
    ///   Ворзвращает типы документов регистрации предназначениые для заполнения в зависимости от категории гражданина и пола
    /// </summary>
    /// <returns>
    ///   The <see cref="List{Concept}" />.
    /// </returns>
    public List<Concept> GetDocumentTypeForRegistrationDocument()
    {
      return InvokeInterceptors(() => Service.GetDocumentTypeForRegistrationDocument());
    }

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <returns> The <see cref="List{Concept}" /> . </returns>
    public List<Concept> GetDocumentTypeForRepresentative()
    {
      return InvokeInterceptors(() => Service.GetDocumentTypeForRepresentative());
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
      return InvokeInterceptors(() => Service.GetErrorsByPeriod(startDate, endDate));
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
      return InvokeInterceptors(() => Service.GetFormManufacturingByCauseFilling(causeFilling)).ToList();
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
      return InvokeInterceptors(() => Service.GetRepresentativeContactInfoByUdl(number, series));
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
      return InvokeInterceptors(() => Service.GetSearchStatementResult(id));
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
      return InvokeInterceptors(() => Service.GetSetting(name));
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
      return InvokeInterceptors(() => Service.GetSettingCurrentUser(nameSetting));
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
      return InvokeInterceptors(() => Service.GetStatement(statementId));
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
      return InvokeInterceptors(() => Service.GetStatementByInsuredPersonId(insuredPersonId));
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
      return InvokeInterceptors(() => Service.GetTypePolisByFormManufacturing(formManufacturing));
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
      return InvokeInterceptors(() => Service.InsuredInJoined(personId));
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
      return InvokeInterceptors(() => Service.IsRightToEdit(propertys, expression));
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
      return InvokeInterceptors(() => Service.SaveContentRecord(typeContent, content, fileName));
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
      // сохранение заявления
      return InvokeInterceptors(() => Service.SaveStatement(statement));
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
      return InvokeInterceptors(() => Service.Search(criteria));
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
      InvokeInterceptors(() => Service.SetSettingCurrentUser(nameSetting, value));
    }

    /// <summary>
    /// Трим полей заявления
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void TrimStatementData(Statement statement)
    {
      InvokeInterceptors(() => Service.TrimStatementData(statement));
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
      return InvokeInterceptors(() => Service.TryCheckProperty(statement, expression));
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
      return InvokeInterceptors(() => Service.TryCheckProperty1(statement, expression));
    }

    #endregion
  }
}