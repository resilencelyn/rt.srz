// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobInfo.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Базовый класс для описателя работ
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.jobpool
{
  using System;

  using rt.core.business.interfaces.jobpool;

  /// <summary>
  ///   Базовый класс для описателя работ
  /// </summary>
  [Serializable]
  public abstract class JobInfo : IJobInfo
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="JobInfo"/> class. 
    ///   Конструктор
    /// </summary>
    protected JobInfo()
    {
      Id = Guid.NewGuid();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Идентификатор
    /// </summary>
    public Guid Id { get; private set; }

    #endregion
  }
}