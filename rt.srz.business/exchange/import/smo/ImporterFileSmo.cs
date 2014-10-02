// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFileSmo.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.smo
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using rt.core.business.server.exchange.import;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.enumerations;
  using rt.srz.model.HL7.enumerations;
  using rt.srz.model.HL7.smo;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  /// The importer file smo.
  /// </summary>
  /// <typeparam name="TSerializeObject">
  /// </typeparam>
  /// <typeparam name="TNode">
  /// </typeparam>
  public abstract class ImporterFileSmo<TSerializeObject, TNode> : ImporterFile
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterFileSmo{TSerializeObject,TNode}"/> class.
    /// </summary>
    /// <param name="subject">
    /// The subject.
    /// </param>
    public ImporterFileSmo(int subject)
      : base(subject)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Отмена загрузки пакетов
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    public override void UndoBatches(string fileName)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// Создание батча
    /// </summary>
    /// <param name="serObject">
    /// </param>
    /// <returns>
    /// The <see cref="Batch"/>.
    /// </returns>
    protected abstract Batch CreateBatch(TSerializeObject serObject);

    /// <summary>
    /// Возвращает адрес регистрации
    /// </summary>
    /// <param name="address">
    /// The address.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    protected virtual void FillAddressG(AddressType address, Statement statement)
    {
      if (address == null)
      {
        statement.Address = null;
        return;
      }

      var addr = statement.Address = new address();
      if (!string.IsNullOrEmpty(address.BOMG) && address.BOMG == "1")
      {
        addr.IsHomeless = true;
      }

      var kladrManager = ObjectFactory.GetInstance<IKladrManager>();
      var subj = !string.IsNullOrEmpty(address.SUBJ)
        ? kladrManager.GetBy(x => x.Ocatd == (address.SUBJ + "000000") && x.Level == 1).FirstOrDefault()
        : null;
      addr.Subject = subj != null ? string.Format("{0} {1}", subj.Name, subj.Socr) : string.Empty;
      addr.Postcode = address.INDX;
      addr.Okato = address.OKATO;
      addr.Area = address.RNNAME;
      addr.City = address.NPNAME;
      addr.Street = address.UL;
      addr.House = address.DOM;
      addr.Housing = address.KORP;
      short room = 0;
      if (!string.IsNullOrEmpty(address.KV) && short.TryParse(address.KV, out room))
      {
        addr.Room = room;
      }

      DateTime dateReg;
      if (!string.IsNullOrEmpty(address.DREG) && DateTime.TryParse(address.DREG, out dateReg))
      {
        addr.DateRegistration = dateReg;
      }

      if (!string.IsNullOrEmpty(address.CODEKLADR))
      {
        addr.Kladr = ObjectFactory.GetInstance<IKladrManager>().GetByCODE(address.CODEKLADR).FirstOrDefault();
      }

      statement.Address = addr;
    }

    /// <summary>
    /// Возвращает адрес проживания
    /// </summary>
    /// <param name="address">
    /// The address.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    protected virtual void FillAddressP(AddressType address, Statement statement)
    {
      if (address == null)
      {
        statement.Address2 = statement.Address;
        return;
      }

      var addr = statement.Address2 ?? new address();
      var kladrManager = ObjectFactory.GetInstance<IKladrManager>();
      var subj = !string.IsNullOrEmpty(address.SUBJ)
        ? kladrManager.GetBy(x => x.Ocatd == (address.SUBJ + "000000") && x.Level == 1).FirstOrDefault()
        : null;
      addr.Postcode = address.INDX;
      addr.Okato = address.OKATO;
      addr.Area = address.RNNAME;
      addr.City = address.NPNAME;
      addr.Street = address.UL;
      addr.House = address.DOM;
      addr.Housing = address.KORP;
      short room = 0;
      if (!string.IsNullOrEmpty(address.KV) && short.TryParse(address.KV, out room))
      {
        addr.Room = room;
      }

      if (!string.IsNullOrEmpty(address.CODEKLADR))
      {
        addr.Kladr = ObjectFactory.GetInstance<IKladrManager>().GetByCODE(address.CODEKLADR).FirstOrDefault();
      }

      statement.Address2 = addr;
    }

    /// <summary>
    /// Возвращает контактную информацию
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    protected virtual void FillContactInfo(PersonType person, Statement statement)
    {
      if (person == null)
      {
        statement.ContactInfo = null;
        return;
      }

      var contactInfo = statement.ContactInfo ?? new ContactInfo();
      contactInfo.HomePhone = person.PHONE;
      contactInfo.WorkPhone = person.PHONE_WORK;
      contactInfo.Email = person.EMAIL;
      statement.ContactInfo = contactInfo;
    }

    /// <summary>
    /// Заполняет данные о смерти
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    /// <param name="statement">
    /// </param>
    protected virtual void FillDeadInfo(PersonType person, Statement statement)
    {
      DateTime deadDate;
      if (!string.IsNullOrEmpty(person.DDEATH) && DateTime.TryParse(person.DDEATH, out deadDate))
      {
        var insuredPerson = statement.InsuredPerson ?? new InsuredPerson();
        var deadInfo = insuredPerson.DeadInfo ?? new DeadInfo();
        deadInfo.DateDead = deadDate;
        insuredPerson.DeadInfo = deadInfo;
        statement.InsuredPerson = insuredPerson;
      }
    }

    /// <summary>
    /// Заполняет документы
    /// </summary>
    /// <param name="docList">
    /// The doc List.
    /// </param>
    /// <param name="statement">
    /// </param>
    protected virtual void FillDocuments(List<DocType> docList, Statement statement)
    {
      // Документы
      foreach (var doc in docList)
      {
        var category = DocumentCategory.Unknown;
        var tempDocument = GetDocument(doc, ref category);
        Document document = null;
        if (tempDocument != null)
        {
          switch (category)
          {
            case DocumentCategory.Udl:
              document = statement.DocumentUdl ?? new Document();
              statement.DocumentUdl = document;
              break;
            case DocumentCategory.Registration:
              document = statement.DocumentRegistration ?? new Document();
              statement.DocumentRegistration = document;
              break;
            case DocumentCategory.Residency:
              document = statement.ResidencyDocument ?? new Document();
              statement.ResidencyDocument = document;
              break;
          }
        }

        document.DocumentType = tempDocument.DocumentType;
        document.Series = tempDocument.Series;
        document.Number = tempDocument.Number;
        document.IssuingAuthority = tempDocument.IssuingAuthority;
        document.DateIssue = tempDocument.DateIssue;
        document.DateExp = tempDocument.DateExp;
      }

      if (statement.DocumentRegistration == null)
      {
        statement.DocumentRegistration = statement.DocumentUdl;
      }
    }

    /// <summary>
    /// Заполняет коды надежности идентификации
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    protected virtual void FillDost(PersonType person, Statement statement)
    {
      if (person == null && person.DOST == null)
      {
        statement.InsuredPersonData.BirthdayType = (int)BirthdayType.Full;
        statement.InsuredPersonData.IsIncorrectDate = false;
        return;
      }

      statement.InsuredPersonData.BirthdayType = (int)BirthdayType.Full;
      if (person.DOST.Contains("4"))
      {
        statement.InsuredPersonData.BirthdayType = (int)BirthdayType.MonthAndYear;
      }

      if (person.DOST.Contains("5"))
      {
        statement.InsuredPersonData.BirthdayType = (int)BirthdayType.Year;
      }

      statement.InsuredPersonData.IsIncorrectDate = person.DOST.Contains("6");
    }

    /// <summary>
    /// Заполняет информацию о факте страхования
    /// </summary>
    /// <param name="insurance">
    /// The insurance.
    /// </param>
    /// <param name="statement">
    /// </param>
    protected virtual void FillInsurance(InsuranceType insurance, Statement statement)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      // Номер полиса
      statement.NumberPolicy = insurance.ENP;

      // Информация о документе, подтверждающем факт страхования по ОМС
      statement.MedicalInsurances = new List<MedicalInsurance>();
      foreach (var polis in insurance.POLIS)
      {
        var medInsurance = new MedicalInsurance();

        medInsurance.Statement = statement;

        medInsurance.PolisType =
          conceptManager.GetBy(x => x.Code == polis.VPOLIS && x.Oid.Id == Oid.Формаизготовленияполиса).FirstOrDefault();

        medInsurance.PolisSeria = polis.SPOLIS;
        medInsurance.PolisNumber = polis.NPOLIS;

        DateTime dateFrom;
        if (!string.IsNullOrEmpty(polis.DBEG) && DateTime.TryParse(polis.DBEG, out dateFrom))
        {
          medInsurance.DateFrom = dateFrom;
        }

        DateTime dateTo;
        if (!string.IsNullOrEmpty(polis.DEND) && DateTime.TryParse(polis.DEND, out dateTo))
        {
          medInsurance.DateTo = dateTo;
        }

        DateTime dateStop;
        if (!string.IsNullOrEmpty(polis.DSTOP) && DateTime.TryParse(polis.DSTOP, out dateStop))
        {
          medInsurance.DateStop = dateStop;
        }

        statement.MedicalInsurances.Add(medInsurance);

        if (!string.IsNullOrEmpty(medInsurance.PolisNumber))
        {
          ////// Временное свидетельство
          ////if (medInsurance.PolisNumber.Length == 9)
          ////{
          ////  statement.NumberTemporaryCertificate = medInsurance.PolisNumber;
          ////  statement.DateIssueTemporaryCertificate = medInsurance.DateFrom;
          ////}

          ////// Номер бланка полиса
          ////if (medInsurance.PolisNumber.Length == 11)
          ////{
          ////  statement.NumberPolisCertificate = medInsurance.PolisNumber;
          ////  statement.DateIssuePolisCertificate = medInsurance.DateFrom;
          ////}
        }
      }
    }

    /// <summary>
    /// Возвращает данные застрахованного лица
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    protected virtual void FillInsuredPersonData(PersonType person, Statement statement)
    {
      if (person == null)
      {
        statement.InsuredPersonData = null;
        return;
      }

      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      // Данные о застрахованном лице
      var insuredPersonData = statement.InsuredPersonData ?? new InsuredPersonDatum();

      // ФИО
      insuredPersonData.LastName = person.FAM;
      insuredPersonData.FirstName = person.IM;
      insuredPersonData.MiddleName = person.OT;

      // Пол
      insuredPersonData.Gender = conceptManager.GetBy(x => x.Code == person.W && x.Oid.Id == Oid.Пол).FirstOrDefault();

      // ДР
      var birthday = new DateTime();
      if (DateTime.TryParse(person.DR, out birthday))
      {
        insuredPersonData.Birthday = birthday;
      }
      else
      {
        insuredPersonData.Birthday = null;
      }

      // Место рождения
      insuredPersonData.Birthplace = person.MR;

      // Гражданство, без гражданства, беженец
      if (!string.IsNullOrEmpty(person.C_OKSM))
      {
        if (person.C_OKSM == "Б/Г")
        {
          insuredPersonData.IsNotCitizenship = true;
        }
        else
        {
          insuredPersonData.Citizenship =
            conceptManager.GetBy(x => x.Code == person.C_OKSM && x.Oid.Id == Oid.Страна).FirstOrDefault();
        }
      }
      else
      {
        insuredPersonData.IsNotCitizenship = false;
        insuredPersonData.Citizenship = null;
      }

      // Категория
      if (!string.IsNullOrEmpty(person.KATEG))
      {
        insuredPersonData.Category =
          conceptManager.GetBy(x => x.Code == person.KATEG && x.Oid.Id == Oid.Категориязастрахованноголица)
            .FirstOrDefault();
      }

      // СНИЛС
      if (!string.IsNullOrEmpty(person.SS))
      {
        insuredPersonData.Snils = SnilsChecker.SsToShort(person.SS);
      }
      else
      {
        insuredPersonData.Snils = null;
      }

      statement.InsuredPersonData = insuredPersonData;
    }

    /// <summary>
    /// Заполняет медиа данные
    /// </summary>
    /// <param name="mediaList">
    /// The media List.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    protected virtual void FillMediaData(List<PersonBType> mediaList, Statement statement)
    {
      if (mediaList == null || mediaList.Count == 0)
      {
        statement.InsuredPersonData.Contents = null;
        return;
      }

      var contentManager = ObjectFactory.GetInstance<IContentManager>();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      var contentList = statement.InsuredPersonData.Contents ?? new List<Content>();
      foreach (var pb in mediaList)
      {
        var content = new Content();
        content.DocumentContent = contentManager.Base64ToByte(pb.PHOTO);
        content.ContentType =
          conceptManager.GetBy(x => x.Code == pb.TYPE && x.Oid.Id == Oid.ContentType).FirstOrDefault();
        content.ChangeDate = DateTime.Now;
        contentList.Add(content);
      }

      statement.InsuredPersonData.Contents = contentList;
    }

    /// <summary>
    /// Возвращает информацию о представителе
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    protected virtual void FillRepresentative(PersonType person, Statement statement)
    {
      if (person.PR == null)
      {
        statement.Representative = null;
        return;
      }

      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var representative = statement.Representative ?? new Representative();
      representative.LastName = person.PR.FAM;
      representative.FirstName = person.PR.IM;
      representative.MiddleName = person.PR.OT;
      representative.HomePhone = person.PR.PHONE;
      representative.WorkPhone = person.PR.PHONE_WORK;

      if (!string.IsNullOrEmpty(person.PR.RELATION))
      {
        representative.RelationType =
          conceptManager.GetBy(x => x.Code == person.PR.RELATION && x.Oid.Id == Oid.Отношениекзастрахованномулицу)
            .FirstOrDefault();
      }

      if (person.PR.DOC != null)
      {
        var category = DocumentCategory.Unknown;
        var document = representative.Document ?? new Document();
        var tempDocument = GetDocument(person.PR.DOC, ref category);
        document.DocumentType = tempDocument.DocumentType;
        document.Series = tempDocument.Series;
        document.Number = tempDocument.Number;
        document.IssuingAuthority = tempDocument.IssuingAuthority;
        document.DateIssue = tempDocument.DateIssue;
        document.DateExp = tempDocument.DateExp;
        representative.Document = document;
      }

      statement.Representative = representative;
    }

    /// <summary>
    /// Заполняет данные по истории изменения заявления
    /// </summary>
    /// <param name="statementChangeList">
    /// The statement Change List.
    /// </param>
    /// <param name="statement">
    /// The statement.
    /// </param>
    protected virtual void FillStatementChangeData(List<StatementChange> statementChangeList, Statement statement)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      statement.StatementChangeDates = statementChangeList != null && statementChangeList.Count > 0
        ? statementChangeList.Select(
          x =>
            new StatementChangeDate
            {
              Statement = statement, 
              Field =
                conceptManager.GetBy(y => y.Code == x.FIELD && y.Oid.Id == Oid.TypeFields)
                .FirstOrDefault(), 
              Datum = x.DATA, 
              Version = int.Parse(x.VERSION)
            }).ToList()
        : null;
    }

    /// <summary>
    /// Заполняет информацию о факте посещения СМО
    /// </summary>
    /// <param name="vizit">
    /// The vizit.
    /// </param>
    /// <param name="statement">
    /// </param>
    protected virtual void FillVizit(VizitType vizit, Statement statement)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      DateTime dateFilling;
      if (!string.IsNullOrEmpty(vizit.DVIZIT) && DateTime.TryParse(vizit.DVIZIT, out dateFilling))
      {
        statement.DateFiling = dateFilling;
      }

      statement.HasPetition = vizit.PETITION > 0 ? true : false;

      // Способ подачи
      if (!string.IsNullOrEmpty(vizit.METHOD))
      {
        statement.ModeFiling =
          conceptManager.GetBy(x => x.Code == vizit.METHOD && x.Oid.Id == Oid.Способподачизаявления).FirstOrDefault();
      }

      // Форма полиса
      if (!string.IsNullOrEmpty(vizit.FPOLIS))
      {
        statement.FormManufacturing =
          conceptManager.GetBy(x => x.Code == vizit.FPOLIS && x.Oid.Id == Oid.Формаизготовленияполиса).FirstOrDefault();
      }

      // Причина подачи
      if (!string.IsNullOrEmpty(vizit.RPOLIS))
      {
        statement.CauseFiling =
          conceptManager.GetBy(x => x.Code == vizit.RPOLIS && x.Oid.Id == Oid.ПричинаподачизаявлениянавыборилизаменуСмо)
            .FirstOrDefault();
      }
      else
      {
        if (!string.IsNullOrEmpty(vizit.RSMO))
        {
          statement.CauseFiling =
            conceptManager.GetBy(x => x.Code == vizit.RSMO && x.Oid.Id == Oid.Причинаподачизаявлениянавыдачудубликата)
              .FirstOrDefault();
        }
      }
    }

    /// <summary>
    /// Возвращает документ
    /// </summary>
    /// <param name="doc">
    /// </param>
    /// <param name="category">
    /// The category.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/>.
    /// </returns>
    protected virtual Document GetDocument(DocType doc, ref DocumentCategory category)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      var document = new Document();
      document.DocumentType =
        conceptManager.GetBy(x => x.Code == doc.DOCTYPE && x.Oid.Id == Oid.ДокументУдл).FirstOrDefault();
      document.Series = doc.DOCSER;
      document.Number = doc.DOCNUM;
      document.IssuingAuthority = doc.NAME_VP;
      DateTime dateIssue;
      if (!string.IsNullOrEmpty(doc.DOCDATE) && DateTime.TryParse(doc.DOCDATE, out dateIssue))
      {
        document.DateIssue = dateIssue;
      }

      DateTime dateExp;
      if (!string.IsNullOrEmpty(doc.DOCEXP) && DateTime.TryParse(doc.DOCEXP, out dateExp))
      {
        document.DateExp = dateExp;
      }

      if (!string.IsNullOrEmpty(doc.DOCCAT))
      {
        var cat = 0;
        if (int.TryParse(doc.DOCCAT, out cat))
        {
          category = (DocumentCategory)cat;
        }
      }

      return document;
    }

    /// <summary>
    /// Маппинг записи из загруженного файла в заявление
    /// </summary>
    /// <param name="rec">
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    protected abstract Statement MapStatement(TNode rec);

    /// <summary>
    /// Отмена загрузки пакета
    /// </summary>
    /// <param name="batch">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected override bool UndoBatch(Guid batch)
    {
      return true;
    }

    #endregion
  }
}