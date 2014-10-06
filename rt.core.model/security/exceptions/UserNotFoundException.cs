// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserNotFoundException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Пользователь не найден в системе
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.security.exceptions
{
  #region references

  using System.ServiceModel;

  using rt.core.model.Properties;

  #endregion

  /// <summary>
  ///   Пользователь не найден в системе
  /// </summary>
  public class UserNotFoundException : FaultException<ExceptionInfo>
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="UserNotFoundException" /> class.
    ///   Конструктор <see cref="UserNotFoundException" /> класса
    /// </summary>
    public UserNotFoundException()
      : base(new ExceptionInfo(Resources.UserNotFoundExceptionCode), Resources.UserNotFoundExceptionMessage)
    {
    }

    #endregion
  }
}