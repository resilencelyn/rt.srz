// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
  using rt.srz.model.HL7.person.messages;
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
    /// ��������� � ���� ��������� � ��� ��� ����� �������� ��������� �������� ������� ����������
    /// </summary>
    /// <param name="className">
    /// ��� ���������� 
    /// </param>
    [OperationContract]
    void AddAllowChangeSetting(string className);

    /// <summary>
    /// ��������� � ���� ��������� �������� � ��� ��� � �� ���� ��������� � ������ ���������������� �����
    /// </summary>
    /// <param name="className">
    /// </param>
    [OperationContract]
    void AddSetting(string className);

    /// <summary>
    /// </summary>
    /// <param name="statement">
    /// </param>
    /// <param name="expression">
    /// </param>
    [OperationContract]
    void CheckPropertyStatement(Statement statement, ExpressionNode expression);

    /// <summary>
    /// </summary>
    /// <param name="statement">
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
    /// The <see cref="byte[]"/> . 
    /// </returns>
    [OperationContract]
    byte[] ConvertToGrayScale(byte[] image);

    /// <summary>
    ///   ��������� ������� ����
    /// </summary>
    /// <returns> The <see cref="byte[]" /> . </returns>
    [OperationContract]
    byte[] CreateEmptyPhoto();

    /// <summary>
    ///   ��������� ������ �������
    /// </summary>
    /// <returns> The <see cref="byte[]" /> . </returns>
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
    /// �������� ��� ��������� ��� ��������� �������
    /// </summary>
    /// <param name="insuredId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Statement> GetAllByInsuredId(Guid insuredId);

    /// <summary>
    /// ���������� ������ AutoComplete
    /// </summary>
    /// <param name="Id">
    /// </param>
    /// <returns>
    /// The <see cref="AutoComplete"/> . 
    /// </returns>
    [OperationContract]
    AutoComplete GetAutoComplete(Guid Id);

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
    [OperationContract]
    IList<Concept> GetCategoryByCitizenship(int citizenshipId, bool isnotCitizenship, bool isrefugee, TimeSpan age);

    /// <summary>
    /// The get concept.
    /// </summary>
    /// <param name="id">
    /// The id. 
    /// </param>
    /// <returns>
    /// The <see cref="Concept"/> . 
    /// </returns>
    [OperationContract]
    Concept GetConcept(int id);

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
    /// The get document residency type by category.
    /// </summary>
    /// <param name="categoryId">
    /// The category id. 
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/> . 
    /// </returns>
    IList<Concept> GetDocumentResidencyTypeByCategory(int categoryId);

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
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Concept> GetDocumentTypeByCategory(int categoryId, TimeSpan age);

    /// <summary>
    ///   The get document type for registration document
    /// </summary>
    /// <param name="categoryId"> The category id. </param>
    /// <param name="age"> The age. </param>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Concept> GetDocumentTypeForRegistrationDocument();

    /// <summary>
    ///   The get document type for representative
    /// </summary>
    /// <param name="categoryId"> The category id. </param>
    /// <param name="age"> The age. </param>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Concept> GetDocumentTypeForRepresentative();

    /// <summary>
    /// ���������� ������ ��������� ��� �����
    /// </summary>
    /// <param name="prefix">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<AutoComplete> GetFirstNameAutoComplete(string prefix);

    /// <summary>
    /// ���������� ������ ����� ������ � ����������� �� ������� ���������
    /// </summary>
    /// <param name="causeFilling">
    /// The cause Filling. 
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Concept> GetFormManufacturingByCauseFilling(int causeFilling);

    /// <summary>
    /// ���������� ������ ��������� ��� ��������
    /// </summary>
    /// <param name="prefix">
    /// </param>
    /// <param name="nameId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<AutoComplete> GetMiddleNameAutoComplete(string prefix, Guid nameId);

    /// <summary>
    /// ���������� ������ ���������� ���������� ������
    /// </summary>
    /// <param name="oid">
    /// The oid. 
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract(Name = "GetNsiRecordsByoid")]
    IList<Concept> GetNsiRecords(string oid);

    /// <summary>
    /// ���������� ������ ���������� ���������� ������
    /// </summary>
    /// <param name="oid">
    /// The oid. 
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract(Name = "GetNsiRecords")]
    IList<Concept> GetNsiRecords(IEnumerable<string> oid);

    /// <summary>
    /// �������� ������ �� ������ � ����� ���������
    /// </summary>
    /// <param name="number">
    /// </param>
    /// <param name="series">
    /// </param>
    /// <returns>
    /// The <see cref="Representative"/> . 
    /// </returns>
    [OperationContract]
    Representative GetRepresentativeContactInfoByUdl(string number, string series);

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
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> . 
    /// </returns>
    [OperationContract]
    Statement GetStatementByInsuredPersonId(Guid insuredPersonId);

    /// <summary>
    /// The get za 7.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="ZPI_ZA7"/> . 
    /// </returns>
    [OperationContract]
    ZPI_ZA7 GetZa7(Statement statement);

    /// <summary>
    /// ������ �� ��������� ������� � ����������� ��� ������� ��� ��� ������ ����
    /// </summary>
    /// <param name="personId">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    [OperationContract]
    bool InsuredInJoined(Guid personId);

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
    /// ������� �� ���� ��������� � ��� ��� ����� �������� ��������� �������� ������� ����������
    /// </summary>
    /// <param name="className">
    /// ��� ���������� 
    /// </param>
    [OperationContract]
    void RemoveAllowChangeSetting(string className);

    /// <summary>
    /// ������� ��������� �� ���� ������� ���� ����� ���������
    /// </summary>
    /// <param name="className">
    /// </param>
    [OperationContract]
    void RemoveSetting(string className);

    /// <summary>
    /// The remove statement.
    /// </summary>
    /// <param name="statementId"> </param>
    [OperationContract]
    void CanceledStatement(Guid statementId);

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
    /// The <see>
    ///                 <cref>IList</cref>
    ///               </see> . 
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
    /// </summary>
    /// <param name="statement">
    /// </param>
    /// <param name="expression">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    [OperationContract]
    bool TryCheckProperty(Statement statement, ExpressionNode expression);

    /// <summary>
    /// ��������� �������� � �������� ���������� �� ������ (����� ������ �� ����������)
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    [OperationContract]
    string TryCheckProperty1(Statement statement, ExpressionNode expression);


    /// <summary>
    /// The un bind statement.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    [OperationContract]
    void UnBindStatement(Statement statement);

    /// <summary>
    /// �������� ������ ������������ � ���������� �� ��������� ������
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    [OperationContract]
    IList<string> GetErrorsByPeriod(DateTime startDate, DateTime endDate);

    /// <summary>
    /// �������� ���� � ������
    /// </summary>
    /// <param name="statementId"></param>
    [OperationContract]
    void DeleteDeathInfo(Guid statementId);

    /// <summary>
    /// The get type polis by form manufacturing.
    /// </summary>
    /// <param name="formManufacturing">
    /// The form manufacturing.
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/>.
    /// </returns>
    [OperationContract]
    IList<Concept> GetTypePolisByFormManufacturing(int formManufacturing);

    /// <summary>
    /// ���� ����� ���������
    /// </summary>
    /// <param name="statement"></param>
    [OperationContract]
    void TrimStatementData(Statement statement);

    #endregion
  }
}