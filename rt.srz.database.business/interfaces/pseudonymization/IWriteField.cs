//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace rt.srz.database.business.interfaces.pseudonymization
{
  using System;
  using System.IO;
  using System.Linq.Expressions;

  using rt.srz.database.business.model;

  public interface IWriteField
  {
    #region Public Properties

    /// <summary>
    /// Gets the expression.
    /// </summary>
    Expression<Func<ModelAdapter, object>> Expression { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ���������� �������� ���� � �����
    /// </summary>
    /// <param name="fieldValue">�������� ����</fieldValue>
    /// <param name="binaryWriter">�������� ������������</param>
    void WriteField(ModelAdapter model, BinaryWriter binaryWriter);

    #endregion
  }
}
