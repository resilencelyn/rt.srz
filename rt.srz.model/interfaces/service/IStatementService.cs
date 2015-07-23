// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementService.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The StatementService interface.
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

  using Serialize.Linq.Nodes;

  #endregion

  /// <summary>
  ///   The StatementService interface.
  /// </summary>
  [ServiceContract]
  public interface IStatementService
  {
    #region Public Methods and Operators

    /// <summary>
    /// ��������� ���� ��������� 30 ������� ����
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
    [OperationContract]
    DateTime CalculateEndPeriodWorkingDay(DateTime dateFrom, int count);

    /// <summary>
    /// The remove statement.
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    [OperationContract]
    void CanceledStatement(Guid statementId);

    /// <summary>
    /// �������� ����������� �������� ���������
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    [OperationContract]
    void CheckPropertyStatement(Statement statement, ExpressionNode expression);

    /// <summary>
    /// �������� ���������, ���������
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    [OperationContract]
    void CheckStatementSimple(Statement statement);

    /// <summary>
    /// The content remove.
    /// </summary>
    /// <param name="content">
    /// The content.
    /// </param>
    [OperationContract]
    void ContentRemove(Content content);

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
    [OperationContract]
    byte[] ConvertToGrayScale(byte[] image);

    /// <summary>
    ///   ��������� ������� ����
    /// </summary>
    /// <returns>
    ///   The
    ///   <see>
    ///     <cref>byte[]</cref>
    ///   </see>
    ///   .
    /// </returns>
    [OperationContract]
    byte[] CreateEmptyPhoto();

    /// <summary>
    ///   ��������� ������ �������
    /// </summary>
    /// <returns>
    ///   The
    ///   <see>
    ///     <cref>byte[]</cref>
    ///   </see>
    ///   .
    /// </returns>
    [OperationContract]
    byte[] CreateEmptySign();

    /// <summary>
    /// The create from example.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    [OperationContract]
    Statement CreateFromExample(Statement statement);

    /// <summary>
    /// �������� ���� � ������
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    [OperationContract]
    void DeleteDeathInfo(Guid statementId);

    /// <summary>
    /// �������� ��� ��������� ��� ��������� �������
    /// </summary>
    /// <param name="insuredId">
    /// The insured Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Statement}"/> .
    /// </returns>
    [OperationContract]
    List<Statement> GetAllByInsuredId(Guid insuredId);

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
    [OperationContract]
    List<Concept> GetCategoryByCitizenship(int citizenshipId, bool isnotCitizenship, bool isrefugee, TimeSpan age);

    /// <summary>
    /// The get content record.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Content"/> .
    /// </returns>
    [OperationContract]
    Content GetContentRecord(Guid id);

    /// <summary>
    /// ����������� ���� ���������� ����������� ���������� � �� ��������������� ��� ���������� � ����������� �� ���������
    ///   ����������
    /// </summary>
    /// <param name="categoryId">
    /// The category id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/>.
    /// </returns>
    [OperationContract]
    List<Concept> GetDocumentResidencyTypeByCategory(int categoryId);

    /// <summary>
    /// ����������� ���� ���������� ��� ��������������� ��� ���������� � ����������� �� ��������� ���������� � ����
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
    [OperationContract]
    List<Concept> GetDocumentTypeByCategory(int categoryId, TimeSpan age);

    /// <summary>
    ///   ����������� ���� ���������� ����������� ��������������� ��� ���������� � ����������� �� ��������� ���������� � ����
    /// </summary>
    /// <returns>
    ///   The <see cref="List{Concept}" />.
    /// </returns>
    [OperationContract]
    List<Concept> GetDocumentTypeForRegistrationDocument();

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <returns> The <see cref="List{Concept}" /> . </returns>
    [OperationContract]
    List<Concept> GetDocumentTypeForRepresentative();

    /// <summary>
    /// �������� ������ ������������ � ���������� �� ��������� ������
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
    [OperationContract]
    List<string> GetErrorsByPeriod(DateTime startDate, DateTime endDate);

    /// <summary>
    /// ���������� ������ ����� ������ � ����������� �� ������� ���������
    /// </summary>
    /// <param name="causeFilling">
    /// The cause Filling.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/> .
    /// </returns>
    [OperationContract]
    List<Concept> GetFormManufacturingByCauseFilling(int causeFilling);

    /// <summary>
    /// �������� ������ �� ������ � ����� ���������
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
    [OperationContract]
    Representative GetRepresentativeContactInfoByUdl(string number, string series);

    /// <summary>
    /// The get search statement result.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="SearchStatementResult"/>.
    /// </returns>
    [OperationContract]
    SearchStatementResult GetSearchStatementResult(Guid id);

    /// <summary>
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/>.
    /// </returns>
    [OperationContract]
    Setting GetSetting(string name);

    /// <summary>
    /// The get setting current user.
    /// </summary>
    /// <param name="nameSetting">
    /// The name setting.
    /// </param>
    /// <returns>
    /// The <see cref="string"/> .
    /// </returns>
    [OperationContract]
    string GetSettingCurrentUser(string nameSetting);

    /// <summary>
    /// ���������� ����� ����������� ���������
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    [OperationContract]
    Statement GetStatement(Guid statementId);

    /// <summary>
    /// �������� ��������� �� InsuredPersonId � IsActive = 1
    /// </summary>
    /// <param name="insuredPersonId">
    /// The insured Person Id.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    [OperationContract]
    Statement GetStatementByInsuredPersonId(Guid insuredPersonId);

    /// <summary>
    /// The get type polis by form manufacturing.
    /// </summary>
    /// <param name="formManufacturing">
    /// The form manufacturing.
    /// </param>
    /// <returns>
    /// The <see cref="List{Concept}"/>.
    /// </returns>
    [OperationContract]
    List<Concept> GetTypePolisByFormManufacturing(int formManufacturing);

    /// <summary>
    /// ������ �� ��������� ������� � ����������� ��� ������� ��� ��� ������ ����
    /// </summary>
    /// <param name="personId">
    /// The person Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    [OperationContract]
    bool InsuredInJoined(Guid personId);

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
    [OperationContract]
    bool IsRightToEdit(IEnumerable<Concept> propertys, ExpressionNode expression);

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
    [OperationContract]
    Content SaveContentRecord(int typeContent, byte[] content, string fileName = null);

    /// <summary>
    /// ��������� ���������
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    [OperationContract]
    Statement SaveStatement(Statement statement);

    /// <summary>
    /// ������������ ����� ��������� �� ��������� ��������
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{SearchStatementResult}"/>.
    /// </returns>
    [OperationContract]
    SearchResult<SearchStatementResult> Search(SearchStatementCriteria criteria);

    /// <summary>
    /// The set setting current user.
    /// </summary>
    /// <param name="nameSetting">
    /// The name setting.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    [OperationContract]
    void SetSettingCurrentUser(string nameSetting, string value);

    /// <summary>
    /// ���� ����� ���������
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    [OperationContract]
    void TrimStatementData(Statement statement);

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
    [OperationContract]
    bool TryCheckProperty(Statement statement, ExpressionNode expression);

    /// <summary>
    /// ��������� �������� � ���������� ���������� �� ������ (����� ������ �� ����������)
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
    [OperationContract]
    string TryCheckProperty1(Statement statement, ExpressionNode expression);

    #endregion
  }
}