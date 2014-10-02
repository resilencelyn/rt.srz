// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionInfo.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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

    /// <summary>
    ///   Gets or sets the code.
    /// </summary>
    public string Code { get; protected set; }
  }
}