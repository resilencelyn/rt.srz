using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Context;
using NUnit.Framework;
using StructureMap;

using rt.core.business.nhibernate;
using rt.atl.model.atl;

namespace rt.srz.integration.test.atl
{
  using rt.core.model;

  public abstract class AtlTest
  {
    /// <summary>
    /// Сессия ПВП
    /// </summary>
    protected ISession pvpSession { get; set; }

    /// <summary>
    /// Сессия Атлантика
    /// </summary>
    protected ISession atlSession { get; set; }

    /// <summary>
    /// Выполнять тесты в транзакции
    /// </summary>
    protected virtual bool BeginTransaction
    {
      get
      {
        return false;
      }
    }

    [SetUp]
    public void SetUp()
    {
      Bootstrapper.Bootstrap();
      
      //Сессия ПВП
      pvpSession = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
      CurrentSessionContext.Bind(pvpSession);
      
      //Сессия Атлантика
      atlSession = ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml").OpenSession();

      //Отключаем проверку на номер бланка полиса в Атлантике
      Testproc proc73 = atlSession.QueryOver<Testproc>().Where(x => x.Id == 73).List().FirstOrDefault();
      if (proc73 != null)
      {
        proc73.Act = false;
        atlSession.SaveOrUpdate(proc73);
        atlSession.Flush();
      }
      
      if (BeginTransaction)
      {
        pvpSession.BeginTransaction();
        atlSession.BeginTransaction();
      }
    }

    [TearDown]
    public void TearDown()
    {
      //Включаем проверку на номер бланка полиса в Атлантике
      Testproc proc73 = atlSession.QueryOver<Testproc>().Where(x => x.Id == 73).List().FirstOrDefault();
      if (proc73 != null)
      {
        proc73.Act = true;
        atlSession.SaveOrUpdate(proc73);
        atlSession.Flush();
      }
      
      if (BeginTransaction)
      {
        pvpSession.Transaction.Rollback();
        atlSession.Transaction.Rollback();
      }

      //Чистим ПВП
      var pvpCleanSql = @"
      delete from Errors
      delete from PeriodInsurance
      delete from MedicalInsurance
      delete from Statement
      delete from SearchKey
      delete from Address
      delete from Contents
      delete from InsuredPersonData
      delete from TwinsKey
      delete from Twins
      delete from InsuredPerson
      delete from ContactInfo
      delete from Document
      delete from ResidencyDocument
      delete from Representative";
      pvpSession.CreateSQLQuery(pvpCleanSql).ExecuteUpdate();

      //Чистим Атлантику
      var atlCleanSql = @"
        DELETE FROM ZENP
        WHERE PID IN (SELECT PID FROM PRZBUF)

        DELETE FROM HISTUDL
        WHERE PID IN (SELECT PID FROM PRZBUF)

        DELETE FROM HISTFDR
        WHERE PID IN (SELECT PID FROM PRZBUF)

        DELETE FROM HISTFDR
        WHERE PID IN (SELECT PID FROM PRZBUF)

        DELETE FROM POLIS
        WHERE PID IN (SELECT PID FROM PRZBUF)

        DELETE FROM ERPMSG
        WHERE PID IN (SELECT PID FROM PRZBUF)

        DELETE FROM PEOPLE 
        WHERE ID IN (SELECT PID FROM PRZBUF)

        DELETE FROM ExchangePvp
        DELETE FROM PRZBUF";
      atlSession.CreateSQLQuery(atlCleanSql).ExecuteUpdate();

      pvpSession.Close();
      pvpSession.Dispose();
      atlSession.Close();
      atlSession.Dispose();
    }
  }
}
