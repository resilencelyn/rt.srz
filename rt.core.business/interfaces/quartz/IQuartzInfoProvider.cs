using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace rt.core.business.interfaces.quartz
{
    public interface IQuartzInfoProvider
    {
        /// <summary>
        /// Возаращает ключт триггера
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="triggerName"></param>
        /// <returns></returns>
        TriggerKey GetTriggerKey(string groupName, string triggerName);
    }
}
