// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementHl7Manager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement hl 7 manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;

  using NHibernate;

  using rt.core.business.security.interfaces;
  using rt.core.model.configuration;
  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.Hl7;
  using rt.srz.model.Hl7.person;
  using rt.srz.model.Hl7.person.messages;
  using rt.srz.model.Hl7.person.target;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  using Document = rt.srz.model.Hl7.person.target.Document;

  #endregion

  /// <summary>
  ///   The statement hl 7 manager.
  /// </summary>
  public class StatementHl7Manager : IStatementHl7Manager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Создает бачт и сообщение в БД
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Batch"/>.
    /// </returns>
    public Batch CreateBatchForExportAdtA01(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var tfoms = statement.PointDistributionPolicy.Parent.Parent;

      // Создаем батч для выгрузки файла, для проверки через ФЛК шлюза ЦС ЕРЗ
      var batch = new Batch();
      batch.FileName = string.Empty;
      batch.Subject = conceptManager.GetById(ExchangeSubjectType.Erz);
      batch.Type = conceptManager.GetById(ExchangeFileType.PersonErp);
      batch.CodeConfirm = conceptManager.GetById(CodeConfirm.CA);
      batch.Sender = statement.PointDistributionPolicy.Parent.Parent;
      batch.Receiver = null;
      batch.Number = 1;
      session.Save(batch);
      batch.FileName = string.Format("{0}-{1}.{2}", tfoms.Okato, batch.Id, "uprmes");
      session.Save(batch);

      return batch;
    }

    /// <summary>
    /// Выгружает ADT_A01 для выполнения ФЛК с помощью шлюза РС
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void ExportAdtA01ForFlk(Batch batch, Statement statement)
    {
      var personErp = GetPersonErp(batch, statement);
      var path = Path.Combine(ConfigManager.ExchangeSettings.WorkingFolderExchange, "Out", "Gateway", "Input");
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      var file = Path.Combine(path, batch.FileName);
      XmlSerializationHelper.SerializeToFile(personErp, file, "person_list");
    }

    /// <summary>
    /// Возвращает маппинг Statment на ADT_A01
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="insurance">
    /// The insurance.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <returns>
    /// The <see cref="ADT_A01"/>.
    /// </returns>
    public ADT_A01 GetAdtA01(Statement statement, MedicalInsurance insurance, Message message)
    {
      var adt01 = new ADT_A01
                   {
                     Msh = GetMsh(statement, message),
                     Evn = GetEvn(statement),
                     InsuranceList = new List<ADT_A01_INSURANCE> { GetInsurance(statement, insurance) },
                     Pid = GetPid(statement),

                     // Pv1 = GetPv1(statement),
                     // Zvn = GetZvn(statement)
                   };

      return adt01;
    }

    /// <summary>
    /// Возвращает маппинг Statement на PersonErp
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="PersonErp"/>.
    /// </returns>
    public PersonErp GetPersonErp(Batch batch, Statement statement)
    {
      var personErp = new PersonErp
                      {
                        BeginPacket = GetBhs(batch.Id, statement),
                        Adt_A01 = new List<ADT_A01>(),
                        EndPacket = GetBts(statement.MedicalInsurances.Count)
                      };

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      foreach (var insurance in statement.MedicalInsurances)
      {
        // Создаем сообщение
        var message = new Message();
        message.Batch = batch;
        message.Type = conceptManager.GetById(model.srz.concepts.TransactionCode.A08);
        message.Reason = GetReason(statement);
        message.IsCommit = false;
        message.IsError = false;
        session.Save(message);

        // Связка
        var messageStatement = new MessageStatement();
        messageStatement.Message = message;
        messageStatement.Statement = statement;
        messageStatement.Type = conceptManager.GetById(MessageStatementType.MainStatement);
        messageStatement.Version = statement.Version;
        session.Save(messageStatement);

        personErp.Adt_A01.Add(GetAdtA01(statement, insurance, message));
      }

      return personErp;
    }

    /// <summary>
    /// The get za 7.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="ZPI_ZA7"/>.
    /// </returns>
    public ZPI_ZA7 GetZa7(Statement statement)
    {
      var za7 = new ZPI_ZA7
                {
                  Zah = GetZah(statement),
                  In1 = GetIn(statement),
                  Nk1 = GetNk(statement),
                  Znd = GetZnd(statement),
                  Msh = GetMsh()
                };

      return za7;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get bhs.
    /// </summary>
    /// <param name="batchId">
    /// The batch id.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="BHS"/>.
    /// </returns>
    private BHS GetBhs(Guid batchId, Statement statement)
    {
      var bhs = new BHS();

      // BHS.1
      bhs.FieldSeparator = Hl7Helper.BHS_Delimiter;

      // BHS.2
      bhs.SpecialSymbol = Hl7Helper.BHS_CodeSymbols;

      var tfoms = statement.PointDistributionPolicy.Parent.Parent;

      // BHS.3
      bhs.OriginApplicationName = new BHS3 { Application = "СРЗ " + tfoms.Code };

      // BHS.4
      bhs.OriginOrganizationName = new BHS4 { CodeOfRegion = tfoms.Code, TableCode = Oid.Pvp, Iso = "ISO" };

      // BHS.5
      bhs.ApplicationName = new BHS5 { Application = "ЦК ЕРП" };

      // BHS.6
      bhs.OrganizationName = new BHS6 { FomsCode = "00", TableCode = Oid.Pvp, Iso = "ISO" };

      // BHS.7
      bhs.DateTimeNow = Hl7Helper.FormatCurrentDateTime();

      // BHS.9
      bhs.TypeWork = "P";

      // BHS.11
      bhs.Identificator = batchId.ToString();

      return bhs;
    }

    /// <summary>
    /// The get bts.
    /// </summary>
    /// <param name="messageCount">
    /// The message count.
    /// </param>
    /// <returns>
    /// The <see cref="BTS"/>.
    /// </returns>
    private BTS GetBts(int messageCount)
    {
      return new BTS { CountMessages = messageCount.ToString(CultureInfo.InvariantCulture) };
    }

    /// <summary>
    /// The get evn.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Evn"/>.
    /// </returns>
    private Evn GetEvn(Statement statement)
    {
      var reason = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(ReasonType.П01);
      return new Evn
             {
               CodeReasonEvent = reason != null ? reason.Code : string.Empty,
               DateRegistrationEvent =
                 statement.DateFiling.HasValue
                   ? Hl7Helper.FormatDateTime(statement.DateFiling.Value)
                   : string.Empty
             };
    }

    /// <summary>
    /// The get in.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="In1Card"/>.
    /// </returns>
    private In1Card GetIn(Statement statement)
    {
      var pvp = statement.PointDistributionPolicy;
      var smo = pvp.Parent;
      var personData = statement.InsuredPersonData;
      var residencyDocument = statement.ResidencyDocument;
      var polisEndDate = statement.ResidencyDocument != null && statement.ResidencyDocument.DateExp.HasValue
                           ? ConversionHelper.DateTimeToStringShort(statement.ResidencyDocument.DateExp.Value)
                           : string.Empty;

      var contact = statement.ContactInfo;

      var in1 = new In1Card();

      // IN1.1	SI	Да	Порядковый номер сегмента
      in1.Id = "1";

      // IN1.2	CWE	Да	Идентификатор плана страхования
      in1.PlanId = new PlanId { Id = "ОМС", Oid = "1.2.643.2.40.5.100.72" };

      // IN1.3	CX	Да	Идентификатор организации
      in1.CompanyId = new CompanyId { Id = smo.Ogrn, CompanyIdType = "NII" };

      // IN1.4	XON	Нет	Наименование организации
      in1.CompanyName = new CompanyName { Name = smo.FullName };

      // IN1.5	XAD	Усл	Адрес СМО
      // in1.AddressSmo.Postcode = smo.Postcode;
      in1.AddressSmoInStr = smo.Address;

      // IN1.6	XPN	Усл	Контактное лицо в СМО
      ////in1.FioInSmo.Name = smo.FirstName;
      ////in1.FioInSmo.Otchestvo = smo.MiddleName;
      in1.FioInSmo.Surname.surname = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser().Fio;

      // IN1.7	XTN	Усл	Контактные телефоны СМО
      in1.Phone.Phone = smo.Phone;

      // IN1.12	DT	Да	Дата начала действия страховки
      in1.DateBeginInsurence = statement.DateFiling.HasValue
                                 ? ConversionHelper.DateTimeToStringGoznak(statement.DateFiling.Value)
                                 : string.Empty;

      // IN1.13	DT	Да	Дата окончания действия страховки
      in1.DateEndInsurence = polisEndDate;

      // IN1.15	IS	Да	Код территории страхования
      in1.CodeOfRegion = smo.Parent.Okato;

      // IN1.16	XPN	Да	Фамилия, имя, отчество 
      in1.FioList = new List<Fio>
                    {
                      new Fio(
                        new Surname(personData.FirstName), 
                        personData.LastName, 
                        personData.MiddleName, 
                        "L")
                    };

      // IN1.18	DTM	Да	Дата рождения
      in1.BirthDay = personData.Birthday.HasValue
                       ? ConversionHelper.DateTimeToStringGoznak(personData.Birthday.Value)
                       : string.Empty;

      var adr = statement.Address;
      var adr2 = statement.Address2 ?? adr;
      in1.AddressList = new List<AddressCard>
                        {
                          adr.IsHomeless.HasValue && adr.IsHomeless.Value
                            ? new AddressCard { IsHomeless = "1", AddressType = "L" }
                            : new AddressCard
                              {
                                StructureAddress =
                                  new StructureAddress
                                  {
                                    Building = adr.Housing, 
                                    Room =
                                      adr.Room.ToString(), 
                                    Street = adr.Street
                                  }, 
                                RegionName = adr.Subject, 
                                Region = adr.Okato, 
                                City = adr.City, 
                                Town = adr.Town, 
                                District = adr.Area, 
                                Building = adr.House, 
                                RegistrationDate =
                                  adr.DateRegistration.HasValue
                                    ? ConversionHelper.DateTimeToStringGoznak(
                                                                              adr
                                                                                .DateRegistration
                                                                                .Value)
                                    : string.Empty, 
                                CountryCode = "RUS", 
                                AddressType = "L", 
                                Postcode = adr.Postcode
                              }
                        };

      if (!(adr.IsHomeless.HasValue && adr.IsHomeless.Value))
      {
        in1.AddressList.Add(
                            new AddressCard
                            {
                              StructureAddress =
                                new StructureAddress
                                {
                                  Building = adr2.Housing,
                                  Room = adr2.Room.ToString(),
                                  Street = adr2.Street
                                },
                              RegionName = adr2.Subject,
                              Region = adr2.Okato,
                              City = adr2.City,
                              Town = adr2.Town,
                              RegistrationDate =
                                adr2.DateRegistration.HasValue
                                  ? ConversionHelper.DateTimeToStringGoznak(adr2.DateRegistration.Value)
                                  : string.Empty,
                              District = adr2.Area,
                              Building = adr2.House,
                              CountryCode = "RUS",
                              AddressType = "H",
                              Postcode = adr2.Postcode
                            });
      }

      // IN1.35	IS	Нет	Тип страховки
      in1.InsuranceType = statement.FormManufacturing != null ? statement.FormManufacturing.Code : string.Empty;

      // IN1.36	ST	Да	Номер временного свидетельства
      in1.InsuranceSerNum = statement.NumberTemporaryCertificate;

      // IN1.151 Дата выдачи временного свидетельства
      if (statement.DateIssueTemporaryCertificate.HasValue)
      {
        in1.TemporaryCertificateDateIssue =
          ConversionHelper.DateTimeToStringGoznak(statement.DateIssueTemporaryCertificate.Value);
      }

      // IN1.43	IS	Усл	Пол
      in1.Sex = personData.Gender.Code;

      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();

      // IN1.49	CX	Да	Список идентификаторов
      in1.IdentificatorsList = new List<IdentificatorsCard>
                               {
                                 new IdentificatorsCard
                                 {
                                   identificator = statement.DocumentUdl.SeriesNumber,
                                   identificatorType =
                                     statement.DocumentUdl
                                              .DocumentType.Code, 
                                   identificatorTypeName =
                                     statement.DocumentUdl
                                              .DocumentType.Name, 
                                   ActualFrom =
                                     statement.DocumentUdl.DateIssue
                                              .HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   statement
                                                                     .DocumentUdl
                                                                     .DateIssue
                                                                     .Value)
                                       : string.Empty, 
                                   ActualTo =
                                     statement.DocumentUdl.DateExp
                                              .HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   statement
                                                                     .DocumentUdl
                                                                     .DateExp
                                                                     .Value)
                                       : string.Empty, 
                                   Organization =
                                     new OrganizationName
                                     {
                                       Name =
                                         statement
                                         .DocumentUdl
                                         .IssuingAuthority
                                     }
                                 }, 
                                 new IdentificatorsCard
                                 {
                                   identificator = statement.DocumentRegistration.SeriesNumber,
                                   identificatorType =
                                     statement.DocumentRegistration
                                              .DocumentType.Code, 
                                   identificatorTypeName =
                                     statement.DocumentRegistration
                                              .DocumentType.Name, 
                                   ActualFrom =
                                     statement.DocumentRegistration
                                              .DateIssue.HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   statement
                                                                     .DocumentRegistration
                                                                     .DateIssue
                                                                     .Value)
                                       : string.Empty, 
                                   ActualTo =
                                     statement.DocumentRegistration
                                              .DateExp.HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   statement
                                                                     .DocumentRegistration
                                                                     .DateExp
                                                                     .Value)
                                       : string.Empty, 
                                   Organization =
                                     new OrganizationName
                                     {
                                       Name =
                                         statement
                                         .DocumentRegistration
                                         .IssuingAuthority
                                     }
                                 }, 
                                 new IdentificatorsCard
                                 {
                                   identificatorType =
                                     "ResidencyDocument", 
                                   ActualFrom =
                                     residencyDocument != null
                                     && residencyDocument.DateIssue
                                                         .HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   residencyDocument
                                                                     .DateIssue
                                                                     .Value)
                                       : string.Empty, 
                                   ActualTo =
                                     residencyDocument != null
                                     && residencyDocument.DateExp
                                                         .HasValue
                                       ? ConversionHelper
                                           .DateTimeToStringGoznak(
                                                                   residencyDocument
                                                                     .DateExp
                                                                     .Value)
                                       : string.Empty, 
                                 }, 
                                 new IdentificatorsCard
                                 {
                                   identificatorType = "NI", 
                                   identificator =
                                     statement.NumberPolicy, 
                                   Country = null, 
                                   Organization = null, 
                                 }, 
                                 new IdentificatorsCard
                                 {
                                   identificatorType = "PEN", 
                                   identificator = personData.Snils, 
                                   Country = null, 
                                   Organization = null
                                 }
                               };

      // IN1.52	ST	Да	Место рождения
      in1.PlaceOfBirth = personData.Birthplace;

      // IN1.100
      in1.Category = new CneStructure
                     {
                       FiveDigitCode = personData.Category.Code,
                       Oid = Oid.Категориязастрахованноголица,
                       Name = personData.Category.Name
                     };

      // IN.101 гражданство
      in1.National = new National
                     {
                       Country = personData.IsNotCitizenship ? "Б/Г" : personData.Citizenship.Name,
                       TableCode = personData.IsNotCitizenship ? "Б/Г" : personData.Citizenship.Code,
                     };

      // IN.150 страна рождения
      if (personData.OldCountry != null)
      {
        in1.BirthCountry = personData.OldCountry.Name;
      }

      // IN.102 беженец
      in1.IsRefugee = personData.IsRefugee ? "1" : "0";

      // IN1.103 контактные данные
      in1.TelecommunicationAddresseList = new List<TelecommunicationAddress>
                                          {
                                            new TelecommunicationAddress
                                            {
                                              Email =
                                                contact
                                                .Email, 
                                              Phone =
                                                contact
                                                .HomePhone
                                            }, 
                                            new TelecommunicationAddress
                                            {
                                              Phone =
                                                contact
                                                .WorkPhone
                                            }, 
                                          };

      return in1;
    }

    /// <summary>
    /// The get in 1.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="insurance">
    /// The insurance.
    /// </param>
    /// <returns>
    /// The <see cref="IN1"/>.
    /// </returns>
    private IN1 GetIn1(Statement statement, MedicalInsurance insurance)
    {
      var pvp = statement.PointDistributionPolicy;
      var smo = pvp.Parent;
      var personData = statement.InsuredPersonData;
      var insuredPerson = statement.InsuredPerson;
      var residencyDocument = statement.ResidencyDocument;
      var polisEndDate = statement.ResidencyDocument != null && statement.ResidencyDocument.DateExp.HasValue
                           ? Hl7Helper.FormatDate(statement.ResidencyDocument.DateExp.Value)
                           : Hl7Helper.FormatDate(new DateTime(2200, 1, 1));
      var contact = statement.ContactInfo;
      var tfoms = statement.PointDistributionPolicy.Parent.Parent;
      var documentUdl = statement.DocumentUdl;

      var in1 = new IN1();

      // IN1.1	SI	Да	Порядковый номер сегмента
      in1.Id = "1";

      // IN1.2	CWE	Да	Идентификатор плана страхования
      in1.PlanId = new PlanId { Id = "ОМС", Oid = "1.2.643.2.40.5.100.72" };

      // IN1.3	CX	Да	Идентификатор организации
      in1.CompanyId = new CompanyId { Id = smo.Ogrn, CompanyIdType = "NII" };

      // IN1.4	XON	Нет	Наименование организации
      in1.CompanyName = new CompanyName { Name = smo.FullName };

      // IN1.5	XAD	Усл	Адрес СМО
      // IN1.6	XPN	Усл	Контактное лицо в СМО
      // IN1.7	XTN	Усл	Контактные телефоны СМО

      // IN1.12	DT	Да	Дата начала действия страховки
      in1.DateBeginInsurence = statement.DateFiling.HasValue
                                 ? Hl7Helper.FormatDate(statement.DateFiling.Value)
                                 : string.Empty;

      // IN1.13	DT	Да	Дата окончания действия страховки
      in1.DateEndInsurence = polisEndDate;

      // IN1.15	IS	Да	Код территории страхования
      in1.CodeOfRegion = smo.Parent.Okato;

      // IN1.16	XPN	Да	Фамилия, имя, отчество 
      in1.FioList = new List<Fio>
                    {
                      new Fio(
                        new Surname(personData.FirstName), 
                        personData.LastName, 
                        personData.MiddleName, 
                        "L")
                    };

      // IN1.18	DTM	Да	Дата рождения
      in1.BirthDay = personData.Birthday.HasValue ? Hl7Helper.FormatDate(personData.Birthday.Value) : null;

      var adr = statement.Address;
      var adr2 = statement.Address2 ?? adr;
      in1.AddressList = new List<Address>
                        {
                          adr.IsHomeless.HasValue && adr.IsHomeless.Value
                            ? new Address { AddressType = "L" }
                            : new Address
                              {
                                Region = tfoms.Okato, 
                                Country = "RUS", 
                                AddressType = "L", 
                              }
                        };

      if (!(adr.IsHomeless.HasValue && adr.IsHomeless.Value))
      {
        in1.AddressList.Add(new Address { Region = tfoms.Okato, Country = "RUS", AddressType = "H", });
      }

      // IN1.35	IS	Нет	Тип страховки
      in1.InsuranceType = insurance.PolisType != null ? insurance.PolisType.Code : string.Empty;

      // IN1.36	ST	Да	Номер страховки
      in1.InsuranceSerNum = insurance.SeriesNumber;

      // IN1.42 
      in1.Employment = new Employment { employment = CategoryPerson.IsWorking(personData.Category.Id) ? "1" : "0" };

      // IN1.43	IS	Усл	Пол
      in1.Sex = personData.Gender.Code;

      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();

      // IN1.49	CX	Да	Список идентификаторов
      in1.IdentificatorsList = new List<Identificators>();

      in1.IdentificatorsList.Add(
                                 new Identificators
                                 {
                                   identificator = documentUdl.SeriesNumber,
                                   identificatorType = documentUdl.DocumentType.Code,
                                   identificatorFrom = Hl7Helper.FormatDate(documentUdl.DateIssue),
                                   identificatorTo =
                                     documentUdl.DateExp.HasValue
                                       ? Hl7Helper.FormatDate(documentUdl.DateExp.Value)
                                       : null
                                 });

      // Номер полиса
      if (!string.IsNullOrEmpty(insuredPerson.MainPolisNumber))
      {
        in1.IdentificatorsList.Add(
                                   new Identificators
                                   {
                                     identificatorType = "NI",
                                     identificator = insuredPerson.MainPolisNumber
                                   });
      }

      // СНИЛС
      if (!string.IsNullOrEmpty(personData.Snils))
      {
        in1.IdentificatorsList.Add(new Identificators { identificatorType = "PEN", identificator = personData.Snils });
      }

      // IN1.52	ST	Да	Место рождения
      in1.PlaceOfBirth = personData.Birthplace;

      return in1;
    }

    /// <summary>
    /// The get insurance.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="insurance">
    /// The insurance.
    /// </param>
    /// <returns>
    /// The <see cref="ADT_A01_INSURANCE"/>.
    /// </returns>
    private ADT_A01_INSURANCE GetInsurance(Statement statement, MedicalInsurance insurance)
    {
      var ins = new ADT_A01_INSURANCE { In1 = GetIn1(statement, insurance) };
      return ins;
    }

    /// <summary>
    ///   The get msh.
    /// </summary>
    /// <returns>
    ///   The <see cref="MSH" />.
    /// </returns>
    private MSH GetMsh()
    {
      return null;
    }

    /// <summary>
    /// The get msh.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <returns>
    /// The <see cref="MSH"/>.
    /// </returns>
    private MSH GetMsh(Statement statement, Message message)
    {
      var msh = new MSH();

      // MSH.1
      msh.FieldDivider = Hl7Helper.BHS_Delimiter;

      // MSH.2
      msh.SpecialSymbol = Hl7Helper.BHS_CodeSymbols;

      var tfoms = statement.PointDistributionPolicy.Parent.Parent;

      // MSH.3
      msh.OriginApplicationName = new BHS3 { Application = "СРЗ " + tfoms.Code };

      // MSH.4
      msh.OriginOrganizationName = new BHS4 { CodeOfRegion = tfoms.Code, TableCode = Oid.Pvp, Iso = "ISO" };

      // MSH.5
      msh.ApplicationName = new BHS5 { Application = "ЦК ЕРП" };

      // MSH.6
      msh.OrganizationName = new BHS6 { FomsCode = "00", TableCode = Oid.Pvp, Iso = "ISO" };

      // MSH.7
      msh.DateTimeCreation = Hl7Helper.FormatCurrentDateTime();

      // MSH.9
      msh.MessageType = new MessageType { MessType = "ADT", TransactionCode = "A08", StructureType = "ADT_A01", };

      // MSH.10
      msh.Identificator = message.Id.ToString();

      // MSH.11
      msh.TypeWork = new TypeWork { Type = "P" };

      // MSH.12
      msh.VersionStandartId = new VersionStandartId();

      // MSH.15
      msh.ConfirmationTypeGateWay = "AL";

      // MSH.16
      msh.ConfirmationTypeFoms = "AL";

      return msh;
    }

    /// <summary>
    /// The get nk.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Nk1"/>.
    /// </returns>
    private Nk1 GetNk(Statement statement)
    {
      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();
      if (!statement.HasPetition.HasValue)
      {
        return null;
      }

      if (statement.Representative == null)
      {
        return null;
      }

      var representative = statement.Representative;
      if (statement.ModeFiling.Id == ModeFiling.ModeFiling1)
      {
        return null;
      }

      var nk1 = new Nk1
                {
                  // Фамилия, Имя, Отчество (@Representative.LastName, @Representative.FirstName, @Representative.MiddleName)
                  Fio =
                    new Fio(
                    new Surname(representative.LastName),
                    representative.FirstName,
                    representative.MiddleName,
                    "L"),

                  // Отношение к застрахованному лицу, сведения о котором указаны в заявлении (@ASMOT, @ASFAT, @ASOT)
                  DegreeOfRelationship =
                    new Document
                    {
                      Code = representative.RelationType.Code,
                      Name = representative.RelationType.Name,
                      Oid = Oid.Отношениекзастрахованномулицу,
                      Assignment = null
                    },
                  IdentificatorList =
                    new List<IdentificatorsCard>
                    {
                      new IdentificatorsCard
                      {
                        identificator = representative.Document.SeriesNumber, 
                        enp = null, 
                        identificatorType =
                          representative.Document
                                        .DocumentType.Code, 
                        identificatorTypeName =
                          representative.Document
                                        .DocumentType.Name, 
                        ActualFrom =
                          representative.Document.DateIssue
                                        .HasValue
                            ? ConversionHelper
                                .DateTimeToStringGoznak(
                                                        representative
                                                          .Document
                                                          .DateIssue
                                                          .Value)
                            : string.Empty, 
                      }
                    },
                  TelecommunicationAddresseList =
                    new List<TelecommunicationAddress>
                    {
                      new TelecommunicationAddress
                      {
                        Phone =
                          representative
                          .HomePhone
                      }, 
                      new TelecommunicationAddress
                      {
                        Phone =
                          representative
                          .WorkPhone
                      }
                    }
                };

      return nk1;
    }

    /// <summary>
    /// The get pid.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="MessagePid"/>.
    /// </returns>
    private MessagePid GetPid(Statement statement)
    {
      var personData = statement.InsuredPersonData;
      var deadInfo = statement.InsuredPerson.DeadInfo;
      var insuredPerson = statement.InsuredPerson;
      var tfoms = statement.PointDistributionPolicy.Parent.Parent;
      var documentUdl = statement.DocumentUdl;

      var pid = new MessagePid();

      // PID.11
      var adr = statement.Address;
      var adr2 = statement.Address2 ?? adr;
      pid.AddressList = new List<Address>
                        {
                          adr.IsHomeless.HasValue && adr.IsHomeless.Value
                            ? new Address { AddressType = "L" }
                            : new Address
                              {
                                Region = tfoms.Okato, 
                                Country = "RUS", 
                                AddressType = "L", 
                              }
                        };

      if (!(adr.IsHomeless.HasValue && adr.IsHomeless.Value))
      {
        pid.AddressList.Add(new Address { Region = tfoms.Okato, Country = "RUS", AddressType = "H", });
      }

      // PID.7
      pid.BirthDay = personData.Birthday.HasValue ? Hl7Helper.FormatDate(personData.Birthday.Value) : string.Empty;

      // PID.29
      // if (deadInfo != null && deadInfo.ActRecordDate.HasValue)
      // {
      // pid.DeadDay = deadInfo.ActRecordDate.Value.ToShortDateString();
      // }

      // PID.5
      pid.FioList = new List<Fio>
                    {
                      new Fio(
                        new Surname(personData.FirstName), 
                        personData.LastName, 
                        personData.MiddleName, 
                        "L")
                    };

      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();

      // PID.3
      pid.IdentificatorsList = new List<Identificators>();

      // Документ УДЛ
      pid.IdentificatorsList.Add(
                                 new Identificators
                                 {
                                   identificator = documentUdl.SeriesNumber,
                                   identificatorType = documentUdl.DocumentType.Code,
                                   identificatorFrom = Hl7Helper.FormatDate(documentUdl.DateIssue),
                                   identificatorTo =
                                     documentUdl.DateExp.HasValue
                                       ? Hl7Helper.FormatDate(documentUdl.DateExp.Value)
                                       : null
                                 });

      // Номер ЕНП
      if (!string.IsNullOrEmpty(insuredPerson.MainPolisNumber))
      {
        pid.IdentificatorsList.Add(
                                   new Identificators
                                   {
                                     identificatorType = "NI",
                                     identificator = insuredPerson.MainPolisNumber
                                   });
      }

      // СНИЛС
      if (!string.IsNullOrEmpty(personData.Snils))
      {
        pid.IdentificatorsList.Add(new Identificators { identificatorType = "PEN", identificator = personData.Snils });
      }

      // PID.30
      // pid.IsDead = deadInfo != null ? "1" : "0";

      // PID.26"
      if (!personData.IsRefugee)
      {
        pid.Nationality = new National
                          {
                            Nationality = personData.IsNotCitizenship ? "Б/Г" : personData.Citizenship.Code,
                            TableCode = Oid.Страна
                          };
      }
      else
      {
        pid.Nationality = new National
                          {
                            Nationality = "1", // Является беженцем
                            TableCode = "1.2.643.2.40.3.3.0.6.19"
                          };
      }

      // PID.23
      pid.PlaceOfBirth = personData.Birthplace;

      // PID.32
      pid.ReliabilityIdList = new List<string>();

      // PID.8
      pid.Sex = personData.Gender.Code;

      return pid;
    }

    /// <summary>
    /// The get pv 1.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Pv1"/>.
    /// </returns>
    private Pv1 GetPv1(Statement statement)
    {
      return new Pv1();
    }

    /// <summary>
    /// The get type operation.
    /// </summary>
    /// <param name="st">
    /// The st.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private Concept GetReason(Statement st)
    {
      var conceptCacheManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      if (st.AbsentPrevPolicy.HasValue && st.AbsentPrevPolicy.Value)
      {
        switch (st.CauseFiling.Id)
        {
          case CauseReinsurance.Choice:
            return conceptCacheManager.GetById(ReasonType.П01);
          case CauseReinsurance.ReinsuranceAtWill:
          case CauseReinsurance.ReinsuranceWithTheMove:
          case CauseReinsurance.ReinsuranceStopFinance:
            return conceptCacheManager.GetById(ReasonType.П03);

          case CauseReneval.RenevalChangePersonDetails:
          case CauseReneval.RenevalInaccuracy:
          case CauseReneval.RenevalUnusable:
          case CauseReneval.RenevalLoss:
          case CauseReneval.RenevalExpiration:
            return conceptCacheManager.GetById(ReasonType.П06);
        }
      }
      else
      {
        if (st.CauseFiling != null)
        {
          switch (st.CauseFiling.Id)
          {
            case CauseReinsurance.ReinsuranceAtWill:
            case CauseReinsurance.ReinsuranceWithTheMove:
            case CauseReinsurance.ReinsuranceStopFinance:
              return conceptCacheManager.GetById(ReasonType.П03);
            case CauseReneval.Edit:
              return conceptCacheManager.GetById(ReasonType.П04);
          }
        }
      }

      return null;
    }

    /// <summary>
    /// The get zah.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Zah"/>.
    /// </returns>
    private Zah GetZah(Statement statement)
    {
      var zah = new Zah();

      // ZAH.1	CNE	Да	Тип заявления о выборе или замене СМО
      zah.PreferenceOrChangeSmoType = statement.CauseFiling != null
                                      && CauseReinsurance.IsReinsurance(statement.CauseFiling.Id)
                                        ? new CneStructure
                                          {
                                            FiveDigitCode = statement.CauseFiling.Code,
                                            Name = statement.CauseFiling.Name,
                                            Oid = Oid.ПричинаподачизаявлениянавыборилизаменуСмо
                                          }
                                        : null;

      // ZAH.2	CNE	Да	Тип заявления на выдачу полиса
      var type =
        ObjectFactory.GetInstance<IConceptCacheManager>().SingleOrDefault(x => x.Id == statement.TypeStatementId);
      zah.PolicyIssueApplicationType = type != null
                                         ? new CneStructure
                                           {
                                             FiveDigitCode = type.Code,
                                             Name = type.Name,
                                             Oid = Oid.Кодтипазаявления
                                           }
                                         : null;

      // ZAH.3	CNE	Да	Причина выдачи или замены полиса
      zah.PolicyIssueOrChangeReason = statement.CauseFiling != null && CauseReneval.IsReneval(statement.CauseFiling.Id)
                                        ? new CneStructure
                                          {
                                            FiveDigitCode = statement.CauseFiling.Code,
                                            Name =
                                              statement.CauseFiling.Description.Replace(
                                                                                        "Переоформление полиса ОМС в связи с",
                                                                                        string.Empty)
                                                       .Replace(
                                                                "Выдача дубликата полиса ОМС в связи с",
                                                                string.Empty),
                                            Oid = Oid.Причинаподачизаявлениянавыдачудубликата
                                          }
                                        : null;

      // ZAH.4	CNE	Да	Форма изготовления полиса
      zah.PolicyForm = statement.FormManufacturing != null
                         ? new CneStructure
                           {
                             FiveDigitCode = statement.FormManufacturing.Code,
                             Name = statement.FormManufacturing.Name,
                             Oid = Oid.Формаизготовленияполиса
                           }
                         : null;

      // ZAH.5	ID	Нет	Наличие представителя
      zah.IsRepresentative = statement.HasPetition.HasValue && statement.HasPetition.Value ? "1" : "0";

      // ZAH.6	CNE	Нет	Код типа ходатайствующей организации
      zah.IntercessorialOrganizationTypeCode = null;

      // ZAH.7	CNE	Нет	Способ подачи заявления
      zah.MethodOfApplicationSubmission = statement.ModeFiling != null
                                            ? new CneStructure
                                              {
                                                FiveDigitCode = statement.ModeFiling.Code,
                                                Name = statement.ModeFiling.Name,
                                                Oid = Oid.Способподачизаявления
                                              }
                                            : null;

      // ZAH.8	EI	Да	Идентификатор заявления у принявшей организации
      zah.ApplicationIDAtTheOrganizationReceivedIt = new EiStructure
                                                     {
                                                       Identificator = statement.Id.ToString(),
                                                       OrganizationCode =
                                                         statement.PointDistributionPolicy.Parent.Code,
                                                       Oid = "1.2.643.2.40.3.1.4.0",
                                                       Iso = null
                                                     };

      // ZAH.9	EI	Нет	Идентификатор пункта выдачи полисов
      zah.PolicyIssuingPointID = new EiStructure
                                 {
                                   Identificator = statement.PointDistributionPolicy.Code,
                                   OrganizationCode = statement.PointDistributionPolicy.Parent.Code,
                                   Oid = "1.2.643.2.40.3.1.4.0",
                                   Iso = null
                                 };

      // ZAH.10	CNE	Усл	Код территории страхования
      zah.CodeOfTeritory = new CneStructure { CodeTfoms = statement.PointDistributionPolicy.Parent.Parent.Code };

      // ZAH.11	DT	Нет	Дата составления заявления застрахованным лицом
      if (statement.DateFiling != null)
      {
        zah.CompletionDate = ConversionHelper.DateTimeToStringGoznak(statement.DateFiling.Value);
      }

      // ZAH.12	ID	Нет	Признак ознакомления с правилами ОМС
      zah.FamiliarizationAttribute = null;

      // ZAH.13	DTM	Нет	Дата и время приёма заявления
      if (statement.DateFiling != null)
      {
        zah.RecipiencyDateTime =
          zah.CompletionDate = ConversionHelper.DateTimeToStringGoznak(statement.DateFiling.Value);
      }

      // ZAH.14	ST	Нет	ФИО лица, принявшего заявление
      zah.ReceivingEmployeeFullName = string.Empty;

      // ZAH.15	DTM	Нет	Дата и время завершения обработки заявления
      zah.ProcessingEndingDateTime = null;

      // ZAH.16	CNE	Нет	Причина отказа
      zah.DeclineReason = null;

      return zah;
    }

    /// <summary>
    /// The get znd.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>List</cref>
    ///   </see>
    ///   .
    /// </returns>
    private List<Znd> GetZnd(Statement statement)
    {
      var contentManager = ObjectFactory.GetInstance<IContentManager>();

      var listZnd = new List<Znd>();

      // Foto
      var fileName = string.Format("{0}_foto.jpg", statement.Id);
      var z = new Znd
              {
                Id = "1",
                DispositionAndTitle =
                  new Document
                  {
                    Code = "2",
                    Assignment = "Фотография",
                    Name = fileName,
                    Oid = "1.2.643.2.40.3.3.0.7.2"
                  },
                MimeType = new CneStructure { FiveDigitCode = "image/jpeg", Oid = "1.2.643.2.40.1.8.1" },
                ApplicationType = new ApplicationType { MainName = "Microsoft Paint" },
                DocumentСontent = contentManager.GetFoto(statement.InsuredPersonData.Id),
                DocumentName = fileName
              };

      listZnd.Add(z);

      // Signature
      fileName = string.Format("{0}_signature.jpg", statement.Id);
      z = new Znd
          {
            Id = "2",
            DispositionAndTitle =
              new Document
              {
                Code = "3",
                Assignment = "Собственноручная подпись",
                Name = fileName,
                Oid = "1.2.643.2.40.3.3.0.7.2"
              },
            MimeType = new CneStructure { FiveDigitCode = "image/jpeg" },
            ApplicationType = new ApplicationType { MainName = "Microsoft Paint" },
            DocumentСontent = contentManager.GetSignature(statement.InsuredPersonData.Id),
            DocumentName = fileName
          };

      listZnd.Add(z);
      return listZnd;
    }

    /// <summary>
    /// The get zvn.
    /// </summary>
    /// <param name="statment">
    /// The statment.
    /// </param>
    /// <returns>
    /// The <see cref="Zvn"/>.
    /// </returns>
    private Zvn GetZvn(Statement statment)
    {
      return new Zvn();
    }

    #endregion
  }
}