// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementClient.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The atl client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.services
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.core.services.aspects;
  using rt.core.services.registry;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using Serialize.Linq.Nodes;

  using StructureMap.Query;

  #endregion

  /// <summary>
  ///   The atl client.
  /// </summary>
  public class StatementClient : ServiceClient<IStatementService>, IStatementService
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatementClient" /> class.
    /// </summary>
    public StatementClient()
    {
      Interceptors.Clear();
      Interceptors.Add(new LoggingInterceptor());
      Interceptors.Add(new NHibernateProxyInterceptorClient());
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Добавляет в базу настройку проверки о том что её не надо проверять с учётом территориального фонда
    /// </summary>
    /// <param name="className">
    /// </param>
    public void SaveCheckSetting(string className)
    {
      InvokeInterceptors(() => Service.SaveCheckSetting(className));
    }

    /// <summary>
    /// The calculate en period working day.
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
    /// </param>
    public void CanceledStatement(Guid statementId)
    {
      InvokeInterceptors(() => Service.CanceledStatement(statementId));
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
      InvokeInterceptors(() => Service.CheckPropertyStatement(statement, expression));
    }

    /// <summary>
    /// The check statement simple.
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
    /// The <see cref="byte[]"/> .
    /// </returns>
    public byte[] ConvertToGrayScale(byte[] image)
    {
      return InvokeInterceptors(() => Service.ConvertToGrayScale(image));
    }

    /// <summary>
    ///   Генерация пустого фото
    /// </summary>
    /// <returns>
    ///   The <see cref="byte[]" />.
    /// </returns>
    public byte[] CreateEmptyPhoto()
    {
      return InvokeInterceptors(() => Service.CreateEmptyPhoto());
    }

    /// <summary>
    ///   Генерация пустой подписи
    /// </summary>
    /// <returns>
    ///   The <see cref="byte[]" />.
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
    /// </param>
    public void DeleteDeathInfo(Guid statementId)
    {
      InvokeInterceptors(() => Service.DeleteDeathInfo(statementId));
    }

    /// <summary>
    /// Получает все заявления для указанной персоны
    /// </summary>
    /// <param name="insuredId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public List<Statement> GetAllByInsuredId(Guid insuredId)
    {
      return InvokeInterceptors(() => Service.GetAllByInsuredId(insuredId));
    }

    /// <summary>
    /// The get category by citizenship.
    /// </summary>
    /// <param name="citizenshipId">
    ///   The citizenship id.
    /// </param>
    /// <param name="isnotCitizenship">
    ///   The isnot citizenship.
    /// </param>
    /// <param name="isrefugee">
    ///   The isrefugee.
    /// </param>
    /// <param name="age">
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public List<Concept> GetCategoryByCitizenship(int citizenshipId, bool isnotCitizenship, bool isrefugee, TimeSpan age)
    {
      return InvokeInterceptors(() => Service.GetCategoryByCitizenship(citizenshipId, isnotCitizenship, isrefugee, age));
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
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/> .
    /// </returns>
    public Setting GetCurrentSetting(string name)
    {
      return InvokeInterceptors(() => Service.GetCurrentSetting(name));
    }

    /// <summary>
    /// The get document residency type by category.
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Concept> GetDocumentResidencyTypeByCategory(int categoryId)
    {
      return InvokeInterceptors(() => Service.GetDocumentResidencyTypeByCategory(categoryId));
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
    /// The <see cref="IList{T}"/> .
    /// </returns>
    public IList<Concept> GetDocumentTypeByCategory(int categoryId, TimeSpan age)
    {
      return InvokeInterceptors(() => Service.GetDocumentTypeByCategory(categoryId, age));
    }

    /// <summary>
    ///   The get document type for registration document
    /// </summary>
    /// <param name="categoryId"> The category id. </param>
    /// <param name="age"> The age. </param>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Concept> GetDocumentTypeForRegistrationDocument()
    {
      return InvokeInterceptors(() => Service.GetDocumentTypeForRegistrationDocument());
    }

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <param name="categoryId"> The category id. </param>
    /// <param name="age"> The age. </param>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Concept> GetDocumentTypeForRepresentative()
    {
      return InvokeInterceptors(() => Service.GetDocumentTypeForRepresentative());
    }

    /// <summary>
    /// Получает ошибки существующие в заявлениях за указанный период
    /// </summary>
    /// <param name="startDate">
    /// </param>
    /// <param name="endDate">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<string> GetErrorsByPeriod(DateTime startDate, DateTime endDate)
    {
      return InvokeInterceptors(() => Service.GetErrorsByPeriod(startDate, endDate));
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
      return InvokeInterceptors(() => Service.GetFirstNameAutoComplete(prefix));
    }

    /// <summary>
    /// The get form manufacturing by cause filling.
    /// </summary>
    /// <param name="causeFilling">
    /// The cause filling.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<Concept> GetFormManufacturingByCauseFilling(int causeFilling)
    {
      return InvokeInterceptors(() => Service.GetFormManufacturingByCauseFilling(causeFilling));
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
      return InvokeInterceptors(() => Service.GetMiddleNameAutoComplete(prefix, nameId));
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
      return InvokeInterceptors(() => Service.GetNsiRecords(oid));
    }

    /// <summary>
    /// The get nsi records.
    /// </summary>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<Concept> GetNsiRecords(IEnumerable<string> oid)
    {
      return InvokeInterceptors(() => Service.GetNsiRecords(oid));
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
    /// <exception cref="NotImplementedException">
    /// </exception>
    public Setting GetSetting(string name)
    {
      throw new NotImplementedException();
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
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Concept> GetTypePolisByFormManufacturing(int formManufacturing)
    {
      return InvokeInterceptors(() => Service.GetTypePolisByFormManufacturing(formManufacturing));
    }

    /// <summary>
    /// Входит ли указанная персона в объединение как главное или как второе лицо
    /// </summary>
    /// <param name="personId">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
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
    /// Удаляет из базы настройку о том что можно включать отключать проверку данного валидатора
    /// </summary>
    /// <param name="className">
    /// тип валидатора
    /// </param>
    public void RemoveAllowChangeSetting(string className)
    {
      InvokeInterceptors(() => Service.RemoveAllowChangeSetting(className));
    }

    /// <summary>
    /// Удаляет настройку из базы которую надо стало проверять
    /// </summary>
    /// <param name="className">
    /// </param>
    public void RemoveSetting(string className)
    {
      InvokeInterceptors(() => Service.RemoveSetting(className));
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
    /// The search.
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
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
    /// Проверяет проверку и получает информацию об ошибке (текст ошибки из исключения)
    /// </summary>
    /// <param name="statement">
    /// </param>
    /// <param name="expression">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string TryCheckProperty1(Statement statement, ExpressionNode expression)
    {
      return InvokeInterceptors(() => Service.TryCheckProperty1(statement, expression));
    }

    /// <summary>
    /// The un bind statement.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void UnBindStatement(Statement statement)
    {
      InvokeInterceptors(() => Service.UnBindStatement(statement));
    }

    #endregion
  }
}