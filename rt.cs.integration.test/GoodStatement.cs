using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using rt.srz.business.manager;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;

namespace rt.srz.integration.test
{
  using rt.core.model.core;
  using rt.srz.business.extensions;

  /// <summary>
  /// The good statement.
  /// </summary>
  public class GoodStatement
  {
    /// <summary>
    /// The create good statement.
    /// </summary>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    public static Statement CreateGoodStatement()
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptManager>();

      var statement = new Statement();
      statement.DateFiling = DateTime.Now;

      statement.PointDistributionPolicy = new Organisation
                                          {
                                            Id = Guid.NewGuid(),
                                            FullName = "Аннинское агентство 001",
                                            ShortName = "Аннинское агентство 001",
                                            Code = "001",
                                          };

      statement.PointDistributionPolicy.Parent = new Organisation
                                          {
                                            Id = Guid.NewGuid(),
                                            FullName = "ОАО \"РОСНО-МС\"",
                                            ShortName = "ОАО \"РОСНО-МС\"",
                                            Code = "36079",
                                            Ogrn = "1027739051460"
                                          };

      statement.PointDistributionPolicy.Parent.Parent = new Organisation
               {
                 Id = Guid.NewGuid(),
                 FullName = "ТФОМС ВОРОНЕЖСКОЙ ОБЛАСТИ",
                 ShortName = "ТФОМС ВОРОНЕЖСКОЙ ОБЛАСТИ",
                 Code = "36",
                 Ogrn = "1033600019860",
                 Okato = "20000"
               };

      // InsuredPersonData
      statement.InsuredPersonData = new InsuredPersonDatum();
      statement.InsuredPersonData.FirstName = "Николай";
      statement.InsuredPersonData.LastName = "Хороший";
      statement.InsuredPersonData.MiddleName = "Иванович";
      statement.InsuredPersonData.Birthday = new DateTime(1978, 8, 31);
      statement.InsuredPersonData.BirthdayType = 1;
      statement.InsuredPersonData.Birthplace = "Хутор близ Диканьки";
      statement.InsuredPersonData.Category = conceptManager.GetById(CategoryPerson.WorkerRf);
      statement.InsuredPersonData.Citizenship = conceptManager.GetById(Country.RUS);
      statement.InsuredPersonData.Gender = conceptManager.GetById(Sex.Sex1);
      statement.InsuredPersonData.OldCountry = conceptManager.GetById(Country.RUS);

      // Адрес регистрации
      statement.Address = new address();
      statement.Address.DateRegistration = new DateTime(2010, 2, 3);
      statement.Address.House = "3";
      statement.Address.Housing = "2";
      statement.Address.Okato = "45286585000";
      statement.Address.Postcode = "468320";
      statement.Address.Room = 3;
      statement.Address.Street = "Тверская пл";
      statement.Address.Subject = "Москва г";

      // Адрес проживания
      statement.Address2 = new address();
      statement.Address2.House = "3";
      statement.Address2.Housing = "1";
      statement.Address2.Okato = "45286552000";
      statement.Address2.Postcode = "468320";
      statement.Address2.Room = 33;
      statement.Address2.Street = "Вахтангова ул";
      statement.Address2.Subject = "Москва г";

      // Контактная информация
      statement.ContactInfo = new ContactInfo();
      statement.ContactInfo.Email = "aa@bb.com";
      statement.ContactInfo.WorkPhone = "12345";
      statement.ContactInfo.HomePhone = "12345";

      // Документ УДЛ
      statement.DocumentUdl = new Document();
      statement.DocumentUdl.DateIssue = new DateTime(2010, 2, 3);
      statement.DocumentUdl.DocumentType = conceptManager.GetById(DocumentType.PassportRf);
      statement.DocumentUdl.IssuingAuthority = "Какой-то РОВД";
      statement.DocumentUdl.Number = "945093";
      statement.DocumentUdl.Series = "30 15";

      // Документ о регистрации
      statement.DocumentRegistration = new Document();
      statement.DocumentRegistration.DateIssue = new DateTime(2010, 2, 3);
      statement.DocumentRegistration.DocumentType = conceptManager.GetById(DocumentType.PassportRf);
      statement.DocumentRegistration.IssuingAuthority = "Какой-то РОВД";
      statement.DocumentRegistration.Number = "172046";
      statement.DocumentRegistration.Series = "15 30";

      // Персона
      statement.InsuredPerson = new InsuredPerson
                                {
                                  MainPolisNumber = "7851120818000011",
                                  Status = new Concept { Id = StatusPerson.Active }
                                };

      // Страховка
      statement.MedicalInsurances = new List<MedicalInsurance>();
      statement.MedicalInsurances.Add(new MedicalInsurance
                                      {
                                        InsuredPerson = statement.InsuredPerson,
                                        PolisNumber = "123456789",
                                        PolisSeria = null,
                                        DateFrom = DateTime.Today,
                                        DateTo = new DateTime(2200, 1, 1),
                                        StateDateFrom = DateTime.Today,
                                        StateDateTo = new DateTime(2200, 1, 1),
                                        PolisType = new Concept { Id = PolisType.В, Code = "В" },
                                        Smo = statement.PointDistributionPolicy.Parent
                                      });


      return statement;
    }

    /// <summary>
    /// The create good statement.
    /// </summary>
    /// <param name="currentUser">
    /// The current user.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    public static Statement CreateGoodStatement(User currentUser)
    {
      var statement = CreateGoodStatement();
      statement.PointDistributionPolicy = currentUser.PointDistributionPolicy();
      return statement;
    }
  }
}
