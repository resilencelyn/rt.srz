// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchedulerProvider.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

using CrystalQuartz.Core.SchedulerProviders;
using Quartz;
using StructureMap;

namespace rt.core.business.quartz
{
  /// <summary>
  /// The scheduler provider.
  /// </summary>
  public class SchedulerProvider : StdSchedulerProvider
  {
    /// <summary>
    /// Gets a value indicating whether is lazy.
    /// </summary>
    protected override bool IsLazy
    {
      get { return true; }
    }
  }
}