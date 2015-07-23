// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWriteField.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The WriteField interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.interfaces.pseudonymization
{
  using System;
  using System.IO;
  using System.Linq.Expressions;

  using rt.srz.database.business.model;

  /// <summary>
  /// The WriteField interface.
  /// </summary>
  public interface IWriteField
  {
    #region Public Properties

    /// <summary>
    ///   Gets the expression.
    /// </summary>
    Expression<Func<ModelAdapter, object>> Expression { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The write field.
    /// </summary>
    /// <param name="model">
    /// The model.
    /// </param>
    /// <param name="binaryWriter">
    /// The binary writer.
    /// </param>
    void WriteField(ModelAdapter model, BinaryWriter binaryWriter);

    #endregion
  }
}