// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionInfo.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Информация о исключении
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol
{
  /// <summary>
  ///   Информация о исключении
  /// </summary>
  public class ExceptionInfo
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionInfo"/> class.
    /// </summary>
    /// <param name="code">
    /// The code.
    /// </param>
    public ExceptionInfo(string code)
    {
      Code = code;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the code.
    /// </summary>
    public string Code { get; protected set; }

    #endregion
  }
}