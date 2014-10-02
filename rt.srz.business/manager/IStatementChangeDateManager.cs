//-------------------------------------------------------------------------------------
// <copyright file="IStatementChangeDateManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using rt.srz.model.srz;
namespace rt.srz.business.manager
{
  /// <summary>
  /// The interface StatementChangeDateManager.
  /// </summary>
  public partial interface IStatementChangeDateManager
  {
    /// <summary>
    /// ��������� ������� ��������� ��������� ������ � ������ ���������
    /// </summary>
    /// <param name="newStatement"></param>
    /// <param name="oldStatement"></param>
    bool SaveStatementChangeHistory(Statement newStatement);

    /// <summary>
    /// ����������� ������� ��������� ��������� ������ � ������ ���������
    /// </summary>
    /// <param name="newStatement"></param>
    /// <param name="oldStatement"></param>
    void ReplicateStatementChangeHistory(Statement statement);
  }
}