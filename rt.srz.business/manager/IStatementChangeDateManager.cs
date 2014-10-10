// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementChangeDateManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface StatementChangeDateManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using rt.srz.model.srz;

  /// <summary>
  ///   The interface StatementChangeDateManager.
  /// </summary>
  public partial interface IStatementChangeDateManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ����������� ������� ��������� ��������� ������ � ������ ���������
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    void ReplicateStatementChangeHistory(Statement statement);

    /// <summary>
    /// ��������� ������� ��������� ��������� ������ � ������ ���������
    /// </summary>
    /// <param name="newStatement">
    /// The new Statement.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool SaveStatementChangeHistory(Statement newStatement);

    #endregion
  }
}