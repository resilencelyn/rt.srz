using NUnit.Framework;
using rt.core.business.test;
using rt.core.business.tests;
using rt.srz.business.manager;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rt.srz.model.srz.concepts;

namespace rt.srz.test
{
  /// <summary>
  /// Тесты по разделению - запускать на базе без заявлений, дубликатов, ключей
  /// </summary>
  [TestFixture]
  public class SeparateTest : BusinessTestsBase
  {
    private Guid _insuredPersonId;
    private IList<Statement> _separateItems;

    protected override bool IsTransaction 
    {
      get { return false; }
    }

    private void CreateSeparateData()
    {
      var service = ObjectFactory.GetInstance<IStatementService>();
      var sec = ObjectFactory.GetInstance<ISecurityService>();
      var curUser = sec.GetCurrentUser();

      _separateItems = new List<Statement>();
      var savedStatements = new List<Guid>();
      for (int i = 0; i < 2; i++)
      {
        //создание заявлений, страховки, расчёт ключей
        var statement = GoodStatement.CreateGoodStatement(curUser);
        statement.DateFiling = statement.DateFiling.Value.AddDays(i);

        if (i == 1)
        {
          statement.CauseFiling = ObjectFactory.GetInstance<IConceptManager>().GetById(CauseReinsurance.ReinsuranceWithTheMove);
          statement.Status = ObjectFactory.GetInstance<IConceptManager>().GetById(StatusStatement.Performed);
        }

        statement = service.SaveStatement(statement);
        //у всех заявлений будет один personid
        _insuredPersonId = statement.InsuredPerson.Id;
        savedStatements.Add(statement.Id);
      }
      //формирование дубликатов
      var manager = ObjectFactory.GetInstance<IExecuteStoredManager>();
      manager.FindTwins();

      //объединяем дубликаты
      var twinManager = ObjectFactory.GetInstance<ITwinManager>();
      var twins = twinManager.GetTwins();
      foreach (var twin in twins)
      {
        twinManager.JoinTwins(twin.Id, twin.FirstInsuredPerson.Id, twin.SecondInsuredPerson.Id);
      }

      int j = 0;
      //зачитываем заявления из базы повторно т.к. обновились заявления и данные после объединения
      foreach(var statid in savedStatements)
      {
        var stat = service.GetStatement(statid);
        //последнее заявление отправляем на разделение
        if (j == 1)
        {
          _separateItems.Add(stat);
        }
        j++;
      }
    }

    [Test]
    public void TestSeparate()
    {
      CreateSeparateData();

      return;
      var service = ObjectFactory.GetInstance<ITFService>();
      //service.Separate(_insuredPersonId, _separateItems);
      var insuredManager = ObjectFactory.GetInstance<IInsuredPersonManager>();
      var insureds = insuredManager.GetAll();

      //должен скопироваться InsuredPerson и к нему соответственно - DeadInfo, InsuredPeriod, EmploymentHistory, MedicalInsurance
      //перестроится заявления - проставеление новой персоны для разделяющихся заявлений и смена активного для каждой группы заявлений - той что разделяется и той что осталась
      //скопироваться дубликаты и ключи по которым они были найдены
      //TODO: дописать
    }
  }
}
