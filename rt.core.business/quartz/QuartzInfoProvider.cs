// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuartzInfoProvider.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The quartz info provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.quartz
{
  using System.Linq;

  using Quartz;
  using Quartz.Impl.Matchers;

  using rt.core.business.interfaces.quartz;

  using StructureMap;

  /// <summary>
  /// The quartz info provider.
  /// </summary>
  public class QuartzInfoProvider : IQuartzInfoProvider
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
    public TriggerKey GetTriggerKey(string groupName, string triggerName)
    {
      var schedulerFactory = ObjectFactory.TryGetInstance<ISchedulerFactory>();
      if (schedulerFactory != null)
      {
        // Получаем шедулер
        var scheduler = schedulerFactory.GetScheduler();
        var serviceKeys = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(groupName));
        return serviceKeys.FirstOrDefault(x => x.Name == triggerName);
      }

      return null;
    }

    #endregion
  }
}