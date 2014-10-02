﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupNotFoundException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
  public class GroupNotFoundException : FaultException<ExceptionInfo>
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="GroupNotFoundException" /> class.
    ///   Конструктор <see cref="UserNotFoundException" /> класса
    /// </summary>
    public GroupNotFoundException()
      : base(new ExceptionInfo(Resources.GroupNotFoundExceptionCode), (string)Resources.GroupNotFoundExceptionMessage)
    {
    }

    #endregion
  }
}