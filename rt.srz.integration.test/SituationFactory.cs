using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using rt.srz.model.interfaces.service;
using StructureMap;

namespace rt.srz.integration.test
{
  public class SituationFactory
  {
    //Применяем тестовую ситуацию
    public static void ApplyTestSituation(TestSituation situation, ISession pvpSession, ISession atlSession)
    { 
      //Сохраняем заявление в ПВП
      var statementService = ObjectFactory.GetInstance<IStatementService>();
      statementService.SaveStatement(situation.Statement);
      
      //Сохраняем заявления в Атлантике
    }
  }
}
