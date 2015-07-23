// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserSearchKeyCalculationException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The user search key calculation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol
{
  using System.Runtime.Serialization;

  /// <summary>
  /// The user search key calculation exception.
  /// </summary>
  public class UserSearchKeyCalculationException : LogicalControlException
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSearchKeyCalculationException"/> class. 
    ///   Initializes a new instance of the <see cref="FaultDubleException"/> class.
    /// </summary>
    public UserSearchKeyCalculationException()
      : base(new ExceptionInfo("99"), "Ошибка расчета пользовательских ключей")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSearchKeyCalculationException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected UserSearchKeyCalculationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The step.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    protected override int Step()
    {
      return 6;
    }

    #endregion
  }
}