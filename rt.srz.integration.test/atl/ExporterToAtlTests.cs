using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using NHibernate;
using NHibernate.Context;
using NUnit.Framework;
using rt.atl.model.interfaces.Service;
using rt.core.business.nhibernate;
using rt.atl.model.atl;
using rt.srz.model.srz;

namespace rt.srz.integration.test.atl
{
  [TestFixture]
  public class ExporterToAtlTests : AtlTest
  {
    [Test]
    public void ExportChoiceTemporaryCertificateIssue()
    {
      //создание тестовой ситуации
      var testSituation = new TestSituation().OperationChoiceTemporaryCertificateIssue();
      SituationFactory.ApplyTestSituation(testSituation, pvpSession, atlSession);
      int statementCount = pvpSession.QueryOver<Statement>().List().Count;
      Assert.AreEqual(statementCount, 1);

      //запуск экспорта
      var atlServices = ObjectFactory.GetInstance<IAtlService>();
      atlServices.RunExportToSrz(null);

      int przBuffCount = atlSession.QueryOver<Przbuf>().List().Count;
      Assert.AreEqual(przBuffCount, 1);
    }

    [Test]
    public void ExportChoicePoliceIssueAfterTemporaryCertificate()
    {
      //создание тестовой ситуации
      var testSituation = new TestSituation().OperationChoiceTemporaryCertificateIssue(). 
        OperationChoicePoliceIssueAfterTemporaryCertificate();
      SituationFactory.ApplyTestSituation(testSituation, pvpSession, atlSession);
      int statementCount = pvpSession.QueryOver<Statement>().List().Count;
      Assert.AreEqual(statementCount, 1);

      //запуск экспорта
      var atlServices = ObjectFactory.GetInstance<IAtlService>();
      atlServices.RunExportToSrz(null);

      int przBuffCount = atlSession.QueryOver<Przbuf>().List().Count;
      Assert.AreEqual(przBuffCount, 2);
    }

    [Test]
    public void ExportOperationReinsuranceAtWillPoliceIssueAfterTemporaryCertificate()
    {
      //создание тестовой ситуации
      var testSituation = new TestSituation().OperationChoiceTemporaryCertificateIssue(). 
        OperationReinsuranceAtWillPoliceIssueAfterTemporaryCertificate();
      SituationFactory.ApplyTestSituation(testSituation, pvpSession, atlSession);
      int statementCount = pvpSession.QueryOver<Statement>().List().Count;
      Assert.AreEqual(statementCount, 1);

      //запуск экспорта
      var atlServices = ObjectFactory.GetInstance<IAtlService>();
      atlServices.RunExportToSrz(null);

      int przBuffCount = atlSession.QueryOver<Przbuf>().List().Count;
      Assert.AreEqual(przBuffCount, 2);
    }
  }
}
