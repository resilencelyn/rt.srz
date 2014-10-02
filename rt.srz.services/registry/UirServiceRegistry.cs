using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using rt.core.services.registry;
using rt.srz.model.interfaces.service;
using rt.srz.services.Uir;

namespace rt.srz.services.registry
{

    public class UirServiceRegistry : ServiceRegistryBase<IUirService, UirService, UirGate>
    {
    }
}
 