using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using NHibernate;
using rt.srz.business.configuration.algorithms;
using rt.srz.model.HL7.person.messages;
using rt.srz.model.HL7.person.target;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using rt.srz.business.manager.cache;
using rt.srz.model.HL7;
using rt.srz.model.HL7.person;
using rt.srz.business.configuration.algorithms.serialization;
using rt.core.business.configuration;
using System.IO;

namespace rt.srz.business.manager
{
  public class StatementADT_A01Manager : IStatementADT_A01Manager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Создает бачт и сообщение в БД
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public Batch CreateBatchForExportADT_A01(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var organisationManager = ObjectFactory.GetInstance<IOrganisationCacheManager>();
      var tfoms = statement.PointDistributionPolicy.Parent.Parent;
      
      // Создаем батч для выгрузки файла, для проверки через ФЛК шлюза ЦС ЕРЗ
      var batch = new Batch();
      batch.FileName = string.Empty;
      batch.Subject = conceptManager.GetById(TypeSubject.Erz);
      batch.Type = conceptManager.GetById(TypeFile.PersonErp);
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
    /// <param name="statement"></param>
    public void Export_ADT_A01_ForFLK(Batch batch, Statement statement)
    {
      var personErp = GetPersonErp(batch, statement);
      string path = Path.Combine(ConfigManager.ExchangeSettings.WorkingFolderExchange, "Out", "Gateway", "Input");
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      string file = Path.Combine(path, batch.FileName);
      XmlSerializationHelper.SerializeToFile(personErp, file, "person_list");
    }
    
    /// <summary>
    /// Возвращает маппинг Statement на PersonErp
    /// </summary>
    /// <param name="statment"></param>
    /// <returns></returns>
    public PersonErp GetPersonErp(Batch batch, Statement statement)
    {
      var personErp = new PersonErp()
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
        message.Type = conceptManager.GetById(HL7EventType.A08);
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

        personErp.Adt_A01.Add(GetADT_A01(statement, insurance, message));
      }

      return personErp;
    }
    
    /// <summary>
    /// Возвращает маппинг Statment на ADT_A01
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="ADT_A01"/>.
    /// </returns>
    public ADT_A01 GetADT_A01(Statement statement, MedicalInsurance insurance, Message message)
    {
      var adt_01 = new ADT_A01()
      {
        Msh = GetMsh(statement, message),
        Evn = GetEvn(statement),
        InsuranceList = new List<ADT_A01_INSURANCE>() { GetInsurance(statement, insurance) },
        Pid = GetPid(statement),
        //Pv1 = GetPv1(statement),
        //Zvn = GetZvn(statement)
      };

      return adt_01;
    }

    #endregion

    #region Methods
    private BHS GetBhs(Guid batchId, Statement statement)
    {
      BHS bhs = new BHS();

      //BHS.1
      bhs.FieldSeparator = HL7Helper.BHS_Delimiter;

      //BHS.2
      bhs.SpecialSymbol = HL7Helper.BHS_CodeSymbols;

      var tfoms = statement.PointDistributionPolicy.Parent.Parent;
      
      //BHS.3
      bhs.OriginApplicationName = new BHS3 { Application = "СРЗ " + tfoms.Code };

      //BHS.4
      bhs.OriginOrganizationName = new BHS4 { CodeOfRegion = tfoms.Code, TableCode = Oid.Pvp, Iso = "ISO" };
      
      //BHS.5
      bhs.ApplicationName = new BHS5() { Application = "ЦК ЕРП" };

      //BHS.6
      bhs.OrganizationName = new BHS6() { FomsCode = "00", TableCode = Oid.Pvp, Iso = "ISO" };

      //BHS.7
      bhs.DateTimeNow = HL7Helper.FormatCurrentDateTime();

      //BHS.9
      bhs.TypeWork = "P";

      //BHS.11
      bhs.Identificator = batchId.ToString();

      return bhs;
    }

    private BTS GetBts(int messageCount)
    {
      return new BTS { CountMessages = messageCount.ToString() };
    }
    
    private MSH GetMsh(Statement statement, Message message)
    {
      MSH msh = new MSH();

      //MSH.1
      msh.FieldDivider = HL7Helper.BHS_Delimiter;

      //MSH.2
      msh.SpecialSymbol = HL7Helper.BHS_CodeSymbols;

      var tfoms = statement.PointDistributionPolicy.Parent.Parent;
      
      //MSH.3
      msh.OriginApplicationName = new BHS3 { Application = "СРЗ " + tfoms.Code };

      //MSH.4
      msh.OriginOrganizationName = new BHS4 { CodeOfRegion = tfoms.Code, TableCode = Oid.Pvp, Iso = "ISO" };

      //MSH.5
      msh.ApplicationName = new BHS5() {Application = "ЦК ЕРП"};

      //MSH.6
      msh.OrganizationName = new BHS6(){ FomsCode="00", TableCode=Oid.Pvp, Iso="ISO" };

      //MSH.7
      msh.DateTimeCreation = HL7Helper.FormatCurrentDateTime();

      //MSH.9
      msh.MessageType = new model.HL7.person.target.MessageType { MessType = "ADT", TransactionCode = "A08", StructureType = "ADT_A01", };

      //MSH.10
      msh.Identificator = message.Id.ToString();

      //MSH.11
      msh.TypeWork = new TypeWork(){ Type="P"};

      //MSH.12
      msh.VersionStandartId = new VersionStandartId();

      //MSH.15
      msh.ConfirmationTypeGateWay = "AL";

      //MSH.16
      msh.ConfirmationTypeFoms = "AL";

      return msh;
    }

    private Evn GetEvn(Statement statement)
    {
      var reason = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(ReasonType.П01);
      return new Evn
      {
        CodeReasonEvent = reason != null ? reason.Code : string.Empty,
        DateRegistrationEvent = statement.DateFiling.HasValue ? HL7Helper.FormatDateTime(statement.DateFiling.Value) : string.Empty
      };
    }
    
    private ADT_A01_INSURANCE GetInsurance(Statement statement, MedicalInsurance insurance)
    {
      var ins = new ADT_A01_INSURANCE
      {
        In1 = GetIn1(statement, insurance)
      };
      return ins;
    }

    private IN1 GetIn1(Statement statement, MedicalInsurance insurance)
    {
      var pvp = statement.PointDistributionPolicy;
      var smo = pvp.Parent;
      var personData = statement.InsuredPersonData;
      var insuredPerson = statement.InsuredPerson;
      var residencyDocument = statement.ResidencyDocument;
      var polisEndDate = statement.ResidencyDocument != null && statement.ResidencyDocument.DateExp.HasValue
                           ? HL7Helper.FormatDate(statement.ResidencyDocument.DateExp.Value)
                           : HL7Helper.FormatDate(new DateTime(2200, 1, 1));
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
                                 ? HL7Helper.FormatDate(statement.DateFiling.Value)
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
                       ? HL7Helper.FormatDate(personData.Birthday.Value)
                       : null;

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
        in1.AddressList.Add(
          new Address
          {
            Region = tfoms.Okato,
            Country = "RUS",
            AddressType = "H",
          });
      }

      // IN1.35	IS	Нет	Тип страховки
      in1.InsuranceType = insurance.PolisType != null ? insurance.PolisType.Code : string.Empty;

      // IN1.36	ST	Да	Номер страховки
      in1.InsuranceSerNum = insurance.SeriesNumber;

      // IN1.42 
      in1.Employment = new Employment
      {
        employment = CategoryPerson.IsWorking(personData.Category.Id) ? "1" : "0"
      };

      // IN1.43	IS	Усл	Пол
      in1.Sex = personData.Gender.Code;

      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();

      // IN1.49	CX	Да	Список идентификаторов
      in1.IdentificatorsList = new List<Identificators>();

      in1.IdentificatorsList.Add(
        new Identificators
        { 
          identificator = documentManager.GetSerNumDocument(documentUdl), 
          identificatorType = documentUdl.DocumentType.Code,
          identificatorFrom = HL7Helper.FormatDate(documentUdl.DateIssue),
          identificatorTo = documentUdl.DateExp.HasValue ? HL7Helper.FormatDate(documentUdl.DateExp.Value) : null
        });
      
      // Номер полиса
      if (!string.IsNullOrEmpty(insuredPerson.MainPolisNumber))
      {
        in1.IdentificatorsList.Add(new Identificators { identificatorType = "NI", identificator = insuredPerson.MainPolisNumber });
      }
      
      // СНИЛС
      if (!string.IsNullOrEmpty(personData.Snils))
      {
        in1.IdentificatorsList.Add(new Identificators{identificatorType = "PEN", identificator = personData.Snils});
      }

      // IN1.52	ST	Да	Место рождения
      in1.PlaceOfBirth = personData.Birthplace;

      return in1;
    }

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
        pid.AddressList.Add(
          new Address
          {
            Region = tfoms.Okato,
            Country = "RUS",
            AddressType = "H",
          });
      }

      //PID.7
      pid.BirthDay = personData.Birthday.HasValue ? HL7Helper.FormatDate(personData.Birthday.Value) : string.Empty;

      //PID.29
      //if (deadInfo != null && deadInfo.ActRecordDate.HasValue)
      //{
      //  pid.DeadDay = deadInfo.ActRecordDate.Value.ToShortDateString();
      //}

      //PID.5
      pid.FioList = new List<Fio>
                {
                  new Fio(
                    new Surname(personData.FirstName), 
                    personData.LastName, 
                    personData.MiddleName, 
                    "L")
                };

      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();
      
      //PID.3
      pid.IdentificatorsList = new List<Identificators>();
      
      // Документ УДЛ
      pid.IdentificatorsList.Add(
        new Identificators 
        { 
          identificator = documentManager.GetSerNumDocument(documentUdl), 
          identificatorType = documentUdl.DocumentType.Code,
          identificatorFrom = HL7Helper.FormatDate(documentUdl.DateIssue),
          identificatorTo = documentUdl.DateExp.HasValue ? HL7Helper.FormatDate(documentUdl.DateExp.Value) : null
        });

      // Номер ЕНП
      if (!string.IsNullOrEmpty(insuredPerson.MainPolisNumber))
      {
        pid.IdentificatorsList.Add(new Identificators { identificatorType = "NI", identificator = insuredPerson.MainPolisNumber});
      }
      
      // СНИЛС
      if (!string.IsNullOrEmpty(personData.Snils))
      {
        pid.IdentificatorsList.Add(new Identificators{identificatorType = "PEN", identificator = personData.Snils});
      }
      
      //PID.30
      //pid.IsDead = deadInfo != null ? "1" : "0";

      //PID.26"
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
          Nationality = "1", //Является беженцем
          TableCode = "1.2.643.2.40.3.3.0.6.19"
        };
      }
      
      //PID.23
      pid.PlaceOfBirth = personData.Birthplace;

      //PID.32
      pid.ReliabilityIdList = new List<string>();

      //PID.8
      pid.Sex = personData.Gender.Code;

      return pid;
    }

    private Pv1 GetPv1(Statement statement)
    {
      return new Pv1();
    }

    private Zvn GetZvn(Statement statment)
    {
      return new Zvn();
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
    #endregion
  }
}
