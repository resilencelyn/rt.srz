using NUnit.Framework;
using rt.core.business.tests;
using rt.srz.business.manager;
using System;
using rt.srz.model.interfaces.service.uir;

namespace rt.srz.integration.test
{
  public class MedInsuranceTest : BusinessTestsBase
  {
    private UIRRequest CreateUIRRequest()
    {
      return new UIRRequest
      {
        FullName = new FullName
        {
          FamilyName = "Климентьева",
          FirstName = "Антонина",
          MiddleName = "Егоровна"
        },
        Birth = new Birth
        {
          BirthDate = DateTime.Parse("11.02.1964").Date,
          BirthPlace = "г. Самара"
        },
        Document = new Document
        {
          DocType = 14,
          DocIdent = "4100 № 380082"
        },
        InsDate = DateTime.Parse("13.03.2014").Date
      };
    }

    private UIRRequest2 CreateUIRRequest2()
    {
      return new UIRRequest2
      {
        FullName = new FullName
        {
          FamilyName = "Климентьева",
          FirstName = "Антонина",
          MiddleName = "Егоровна"
        },
        Birth = new Birth
        {
          BirthDate = DateTime.Parse("11.02.1964").Date,
          BirthPlace = "г. Самара"
        },
        InsDate = DateTime.Parse("13.03.2014").Date,
        InsRegion = "77",
        PolicyNumber = "7856340893000075",
        PolicyType = "Бумажный полис ОМС единого образца"
      };
    }

    [Test]
    public void TestFirstUirRequest()
    {
      var request = new Request(CreateUIRRequest());
      var uirManager = new UirManager();

      CheckingMethod(uirManager.GetMedInsState(request).UIRResponse);
    }

    [Test]
    public void TestSecondUirRequest()
    {
      var request = new Request2(CreateUIRRequest2());
      var uirManager = new UirManager();

      CheckingMethod(uirManager.GetMedInsState2(request).UIRResponse);
    }

    private void CheckingMethod(UIRResponse response)
    {
      Assert.AreEqual(response.Ack, "AA");
      var uirResponses = response.UIRQueryResponse;

      foreach (var uirResponse in uirResponses)
      {
        try
        {
          //часть данных согласно базе

          // Person
          Assert.AreEqual(uirResponse.Person.MainENP, "775753088811");
          Assert.AreEqual(uirResponse.Person.RegionalENP, "7856340893000075");

          // Insurance
          Assert.AreEqual(uirResponse.Insurance.InsId, " № 09966625538");
          Assert.AreEqual(uirResponse.Insurance.InsType, "П");
          Assert.AreEqual(uirResponse.Insurance.InsRegion, "77");
          Assert.AreEqual(uirResponse.Insurance.MedInsCompanyId, "77004");
          Assert.AreEqual(uirResponse.Insurance.StartDate, DateTime.Parse("13.03.2014"));
          Assert.AreEqual(uirResponse.Insurance.EndDate, DateTime.Parse("01.01.2200"));
          return;
        }
        catch (Exception)
        {
          continue;
        }
        Assert.Fail("Some fail. Go to debug!");
      }
    }
  }
}
