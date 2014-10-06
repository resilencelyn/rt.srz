// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementADT_A01Manager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The StatementADT_A01Manager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using rt.srz.model.srz;

  /// <summary>
  /// The StatementADT_A01Manager interface.
  /// </summary>
  public interface IStatementADT_A01Manager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Создает бачт и сообщение в БД
    /// </summary>
    /// <param name="statement">
    /// </param>
    /// <returns>
    /// The <see cref="Batch"/>.
    /// </returns>
    Batch CreateBatchForExportADT_A01(Statement statement);

    /// <summary>
    /// Выгружает ADT_A01 для выполнения ФЛК с помощью шлюза РС
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <param name="statement">
    /// </param>
    void Export_ADT_A01_ForFLK(Batch batch, Statement statement);

    #endregion
  }
}