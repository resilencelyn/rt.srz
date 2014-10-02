using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rt.srz.business.manager;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using StructureMap;

namespace rt.srz.integration.test
{
  public class TestSituation
  {
    //Заявление
    public Statement Statement { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public TestSituation()
    {
      Statement = null;
    }
    
    /// <summary>
    /// Ситуация - выдача ВС(Выбор СМО)
    /// </summary>
    /// <returns></returns>
    public TestSituation OperationChoiceTemporaryCertificateIssue()
    {
      if (Statement == null)
        Statement = GoodStatement.CreateGoodStatement();
      Statement.AbsentPrevPolicy = true;
      Statement.FormManufacturing = ObjectFactory.GetInstance<IConceptManager>().GetById(PolisType.П);       //Бумажный полис
      Statement.CauseFiling = ObjectFactory.GetInstance<IConceptManager>().GetById(CauseReinsurance.Choice); //ВЫбор СМО
      Statement.ModeFiling = ObjectFactory.GetInstance<IConceptManager>().GetById(ModeFiling.ModeFiling1);   //Лично
      ////Statement.NumberTemporaryCertificate = "344334334";
      ////Statement.DateIssueTemporaryCertificate = DateTime.Today;
      return this;
    }

    /// <summary>
    /// Ситуация - выдача полиса после выдачи временного свидетельства(Выбор СМО)
    /// </summary>
    /// <returns></returns>
    public TestSituation OperationChoicePoliceIssueAfterTemporaryCertificate()
    {
      if (Statement == null)
        return this;
      Statement.CauseFiling = ObjectFactory.GetInstance<IConceptManager>().GetById(CauseReinsurance.Choice); //ВЫбор СМО
      Statement.PolicyIsIssued = true;
      Statement.NumberPolicy = "7851120818000011"; //ЕНП
      ////Statement.NumberPolisCertificate = "34343434345";
      ////Statement.DateIssuePolisCertificate = DateTime.Today;
      return this;
    }

    /// <summary>
    /// Ситуация - Выдача полиса(Замена СМО по желанию)
    /// </summary>
    /// <returns></returns>
    public TestSituation OperationReinsuranceAtWillPoliceIssueAfterTemporaryCertificate()
    {
      if (Statement == null)
        return this;
      Statement.CauseFiling = ObjectFactory.GetInstance<IConceptManager>().GetById(CauseReinsurance.ReinsuranceAtWill); //Замена СМО по желанию
      Statement.PolicyIsIssued = true;
      Statement.NumberPolicy = "7851120818000011"; //ЕНП
      ////Statement.NumberPolisCertificate = "34343434345";
      ////Statement.DateIssuePolisCertificate = DateTime.Today;
      return this;
    }

    /// <summary>
    /// Изменяет тип полиса
    /// </summary>
    /// <param name="polisType"></param>
    /// <returns></returns>
    public TestSituation ChangePoliceType(int polisType)
    {
      if (Statement == null)
        return this;

      Statement.FormManufacturing = ObjectFactory.GetInstance<IConceptManager>().GetById(polisType);       //Бумажный полис
      return this;
    }
  }
}
