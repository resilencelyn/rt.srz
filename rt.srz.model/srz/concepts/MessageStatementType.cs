// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageStatementType.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message statement type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary>
  /// The message statement type.
  /// </summary>
  public class MessageStatementType
  {
    #region Constants

    /// <summary> Основное заявление </summary>
    public const int MainStatement = 635;

    /// <summary> Предыдущее заявление </summary>
    public const int PreviousStatement = 636;

    /// <summary> Вторичное заявление</summary>
    public const int SecondaryStatement = 637;

    #endregion
  }
}