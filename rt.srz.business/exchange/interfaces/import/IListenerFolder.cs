// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListenerFolder.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Универсальный загрузчик файлов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.interfaces.import
{
  using Quartz;

  /// <summary>
  ///   Универсальный загрузчик файлов
  /// </summary>
  public interface IListenerFolder : IJob
  {
    #region Public Properties

    /// <summary>
    ///   путь загрузки
    /// </summary>
    string InputPath { get; }

    #endregion
  }
}