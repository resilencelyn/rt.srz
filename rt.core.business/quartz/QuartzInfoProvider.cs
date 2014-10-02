using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl.Matchers;
using StructureMap;
using rt.core.business.interfaces.quartz;

namespace rt.core.business.quartz
{
    public class QuartzInfoProvider : IQuartzInfoProvider
    {
        /// <summary>
        /// Возаращает ключт триггера
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="triggerName"></param>
        /// <returns></returns>
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
    }
}
