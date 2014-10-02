using System.Linq;
using StructureMap;

namespace CrystalQuartz.Web.Configuration
{
  using System;
  using System.Collections;
  using System.Web.Configuration;

  using Core.SchedulerProviders;

  public static class ConfigUtils
  {
    public static ISchedulerProvider SchedulerProvider
    {
      get
      {
        var section = (Hashtable)WebConfigurationManager.GetSection("crystalQuartz/provider");
        var provider = ObjectFactory.TryGetInstance<ISchedulerProvider>();
        if (provider == null)
        {
          var sectionType = section["Type"];
          if (sectionType != null)
          {
            var type = Type.GetType(sectionType.ToString());
            if (type != null)
            {
              provider = Activator.CreateInstance(type) as ISchedulerProvider;
            }
          }
        }

        foreach (var property in section.Keys.Cast<string>().Where(property => property != "Type"))
        {
          if (provider != null)
          {
            provider.GetType().GetProperty(property).SetValue(provider, section[property], new object[] { });
          }
        }

        return provider;
      }
    }
  }
}