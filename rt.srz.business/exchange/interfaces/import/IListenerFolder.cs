// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListenerFolder.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ”ниверсальный загрузчик файлов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.business.exchange.interfaces.import
{
  using Quartz;

  /// <summary>
  ///   ”ниверсальный загрузчик файлов
  /// </summary>
  public interface IListenerFolder : IJob
  {
    /// <summary>
    ///   путь загрузки
    /// </summary>
    string InputPath { get; }
  }
}