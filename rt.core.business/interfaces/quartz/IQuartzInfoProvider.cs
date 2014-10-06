// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQuartzInfoProvider.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The QuartzInfoProvider interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.quartz
{
  using Quartz;

  /// <summary>
  /// The QuartzInfoProvider interface.
  /// </summary>
  public interface IQuartzInfoProvider
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возаращает ключт триггера
    /// </summary>
    /// <param name="groupName">
    /// </param>
    /// <param name="triggerName">
    /// </param>
    /// <returns>
    /// The <see cref="TriggerKey"/>.
    /// </returns>
    TriggerKey GetTriggerKey(string groupName, string triggerName);

    #endregion
  }
}