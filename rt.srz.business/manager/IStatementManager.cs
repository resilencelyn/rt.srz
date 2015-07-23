// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface StatementManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.srz.model.dto;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The interface StatementManager.
  /// </summary>
  public partial interface IStatementManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The apply active.
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    void ApplyActive(InsuredPerson person);

    /// <summary>
    /// Редактирование заявления
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    void CanceledStatement(Guid statementId);

    /// <summary>
    /// The create from example.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    Statement CreateFromExample(Statement statement);

    /// <summary>
    /// Получает заявление по InsuredPersonId с IsActive = 1
    /// </summary>
    /// <param name="insuredPersonId">
    /// The insured Person Id.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    Statement GetActiveByInsuredPersonId(Guid insuredPersonId);

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
    /// The <see cref="IList{String}"/>.
    /// </returns>
    IList<string> GetErrorsByPeriod(DateTime startDate, DateTime endDate);

    /// <summary>
    /// The get search statement result.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="SearchStatementResult"/>.
    /// </returns>
    SearchStatementResult GetSearchStatementResult(Guid id);

    /// <summary>
    /// Импорт заявления из внешнего источника(Атлантика, XML)
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="srzOperationName">
    /// The srz Operation Name.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    Statement ImportStatementFromExternalSource(Statement statement, string srzOperationName);

    /// <summary>
    /// Сохраняет заявление
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    Statement SaveStatement(Statement statement);

    /// <summary>
    /// Трим полей заявления
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    void TrimStatementData(Statement statement);

    /// <summary>
    /// The unbind statement.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    void UnBindStatement(Statement statement);

    #endregion
  }
}