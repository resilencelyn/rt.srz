// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementHl7Manager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The StatementHl7Manager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using rt.srz.model.HL7.person.messages;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The StatementHl7Manager interface.
  /// </summary>
  public interface IStatementHl7Manager
  {
    #region Public Methods and Operators

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