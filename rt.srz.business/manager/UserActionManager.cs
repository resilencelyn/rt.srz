//-------------------------------------------------------------------------------------
// <copyright file="UserActionManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using System;
using System.Linq;
using NHibernate;
using rt.srz.model.interfaces.service;
using StructureMap;
using rt.srz.model.srz;

namespace rt.srz.business.manager
{
  /// <summary>
  /// The UserActionManager.
  /// </summary>
  public partial class UserActionManager
  {
    public void LogAccessToPersonalData(Statement statement, string Event)
    {
      ISession session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      User user = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();

      try
      {
        var userAction = new UserAction()
        {
          User = user,
          Statement = statement,
          Event = session.QueryOver<Concept>()
            .Where(f => f.Name == Event || f.ShortName == Event)
            .List().Single()
        };
        session.SaveOrUpdate(userAction);
        session.Flush();
        session.Clear();
      }
      catch (Exception){}
    }
  }
}