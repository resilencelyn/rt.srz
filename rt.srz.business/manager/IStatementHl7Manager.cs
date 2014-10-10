// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementHl7Manager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The StatementHl7Manager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using rt.srz.model.Hl7.person.messages;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The StatementHl7Manager interface.
  /// </summary>
  public interface IStatementHl7Manager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ������� ���� � ��������� � ��
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Batch"/>.
    /// </returns>
    Batch CreateBatchForExportAdtA01(Statement statement);

    /// <summary>
    /// ��������� ADT_A01 ��� ���������� ��� � ������� ����� ��
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    void ExportAdtA01ForFlk(Batch batch, Statement statement);

    /// <summary>
    /// The get za 7.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="ZPI_ZA7"/>.
    /// </returns>
    ZPI_ZA7 GetZa7(Statement statement);

    #endregion
  }
}