// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionInfo.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Информация о исключении
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.security.exceptions
{
  /// <summary>
  ///   Информация о исключении
  /// </summary>
  public class ExceptionInfo
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ExceptionInfo" /> class.
    ///   Конструктор по умолчанию
    /// </summary>
    public ExceptionInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionInfo"/> class.
    ///   Конструктор
    /// </summary>
    /// <param name="code">
    /// Код исключения
    /// </param>
    public ExceptionInfo(string code)
    {
      Code = code;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Код исключения
    /// </summary>
    public string Code { get; set; }

    #endregion
  }
}