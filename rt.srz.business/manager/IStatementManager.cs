// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;

  using rt.srz.model.dto;
  using rt.srz.model.srz;
  using System.Collections.Generic;

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
    /// Редактирование заявления
    /// </summary>
    /// <param name="statementId"> </param>
    void CanceledStatement(Guid statementId);

    /// <summary>
    /// Сохраняет заявление
    /// </summary>
    /// <param name="statement">
    ///   The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> . 
    /// </returns>
    Statement SaveStatement(Statement statement);

    /// <summary>
    /// The unbind statement.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    void UnBindStatement(Statement statement);

    /// <summary>
    /// Получает ошибки существующие в заявлениях за указанный период
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    IList<string> GetErrorsByPeriod(DateTime startDate, DateTime endDate);

    #endregion

    SearchStatementResult GetSearchStatementResult(Guid id);

    /// <summary>
    /// Трим полей заявления
    /// </summary>
    /// <param name="statement"></param>
    void TrimStatementData(Statement statement);

  }
}