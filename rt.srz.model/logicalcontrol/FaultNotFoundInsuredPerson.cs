// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultNotFoundInsuredPerson.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault not found insured person.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  /// The fault not found insured person.
  /// </summary>
  [Serializable]
  public class FaultNotFoundInsuredPerson : LogicalControlException
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultNotFoundInsuredPerson"/> class. 
    ///   Initializes a new instance of the <see cref="FaultDubleException"/> class.
    /// </summary>
    public FaultNotFoundInsuredPerson()
      : base(new ExceptionInfo("99"), "Не найдена застрахованная персона")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultNotFoundInsuredPerson"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultNotFoundInsuredPerson(SerializationInfo info, StreamingContext context)
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