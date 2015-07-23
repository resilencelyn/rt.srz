// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchedulerProvider.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The scheduler provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.quartz
{
  using CrystalQuartz.Core.SchedulerProviders;

  /// <summary>
  ///   The scheduler provider.
  /// </summary>
  public class SchedulerProvider : StdSchedulerProvider
  {
    #region Properties

    /// <summary>
    ///   Gets a value indicating whether is lazy.
    /// </summary>
    protected override bool IsLazy
    {
      get
      {
        return true;
      }
    }

    #endregion
  }
}