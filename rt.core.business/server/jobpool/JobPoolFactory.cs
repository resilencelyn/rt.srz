using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using rt.core.business.interfaces.jobpool;


namespace rt.core.business.server.jobpool
{
  public class JobPoolFactory<TPoolObject> : IJobPoolFactory<TPoolObject>
  {
    /// <summary>
    /// Возвращает реализацию конкретного типа пула
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public IJobPoolTyped<TPoolObject> GetJobPool(JobPoolType type)
    {
      //var jobPools = ObjectFactory.GetAllInstances<IJobPoolTyped<TPoolObject, TJobInfo>>();
      //return jobPools.FirstOrDefault(x => x.TypePool == type);
      return null;
    }
  }
}
