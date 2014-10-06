// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListenerFolder.cs" company="–усЅ»“ех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ”ниверсальный загрузчик файлов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.interfaces.import
{
  using Quartz;

  /// <summary>
  ///   ”ниверсальный загрузчик файлов
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