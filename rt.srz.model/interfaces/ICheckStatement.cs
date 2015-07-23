// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICheckStatement.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The CheckObject interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces
{
  #region references

  using System;
  using System.Linq.Expressions;

  using rt.srz.model.enumerations;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The CheckObject interface.
  /// </summary>
  public interface ICheckStatement
  {
    #region Public Properties

    /// <summary>
    ///   ����� �� ��������\��������� ��������
    /// </summary>
    bool AllowChange { get; }

    /// <summary>
    ///   �������� ��� ����������� ��������
    /// </summary>
    string Caption { get; }

    /// <summary>
    ///   ��������� ��� ��� ��������
    /// </summary>
    bool CheckRequired { get; }

    /// <summary>
    ///   ��� ����� ����� ����������� � ������ �����
    /// </summary>
    string ClassName { get; }

    /// <summary>
    ///   Gets the expression.
    /// </summary>
    Expression<Func<Statement, object>> Expression { get; }

    /// <summary>
    ///   The level.
    /// </summary>
    CheckLevelEnum Level { get; }

    /// <summary>
    ///   The level description.
    /// </summary>
    string LevelDescription { get; }

    /// <summary>
    ///   ����� ������
    /// </summary>
    int RecordNumber { get; set; }

    /// <summary>
    ///   ��������� �������� � ������ �� �������� ( �� ����������� ���� ���� ����� �� ����������� ������ ���� ��������� - �
    ///   ���� ������ ������������ ���)
    /// </summary>
    bool Visible { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    void CheckObject(Statement statement);

    #endregion
  }
}