// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatchSmo.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.export.smo
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;

  using NHibernate;

  using rt.core.business.server.exchange.export;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.enumerations;
  using rt.srz.model.HL7.enumerations;
  using rt.srz.model.HL7.smo;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  using PolisType = rt.srz.model.HL7.smo.PolisType;

  #endregion

  /// <summary>
  /// The export batch smo.
  /// </summary>
  public abstract class ExportBatchSmo<TSerializeObject, TNode> : ExportBatchSrz<TSerializeObject, TNode>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportBatchSmo{TSerializeObject,TNode}"/> class.
    /// </summary>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <param name="typeSubjectId">
    /// The type subject id. 
    /// </param>
    /// <param name="typeFileId">
    /// The type file id. 
    /// </param>
    public ExportBatchSmo(ExportBatchType type, int typeSubjectId, int typeFileId)
      : base(type, typeSubjectId, typeFileId)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get address.
    /// </summary>
    /// <param name="address">
    /// The address. 
    /// </param>
    /// <param name="kladr">
    /// The kladr. 
    /// </param>
    /// <returns>
    /// The <see cref="AddressType"/> . 
    /// </returns>
    protected virtual AddressType GetAddress(address address, string kladr)
    {
      var adr = new AddressType();
      if (address.IsHomeless.HasValue && address.IsHomeless.Value)
      {
        adr.BOMG = "1";
        return adr;
      }

      adr.SUBJ = !string.IsNullOrEmpty(address.Okato)
                   ? address.Okato.Substring(0, Math.Min(2, address.Okato.Length)) + "000"
                   : null;
      adr.INDX = address.Postcode;
      adr.OKATO = address.Okato;
      adr.RNNAME = address.Area;
      adr.NPNAME = address.City;
      adr.UL = address.Street;
      adr.DOM = address.House;
      adr.KORP = address.Housing;
      adr.KV = address.Room.HasValue ? address.Room.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
      if (address.DateRegistration.HasValue)
      {
        adr.DREG = address.DateRegistration.Value.ToShortDateString();
      }

      adr.CODEKLADR = kladr;

      return adr;
    }

    /// <summary>
    /// The get document.
    /// </summary>
    /// <param name="document">
    /// The document. 
    /// </param>
    /// <returns>
    /// The <see cref="DocType"/> . 
    /// </returns>
    protected virtual DocType GetDocument(Document document, DocumentCategory category)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var doc = new DocType();

      doc.DOCTYPE = conceptManager.Unproxy(document.DocumentType).Code;
      doc.DOCSER = document.Series;
      doc.DOCNUM = document.Number;
      doc.NAME_VP = document.IssuingAuthority;
      if (document.DateIssue.HasValue)
      {
        doc.DOCDATE = document.DateIssue.Value.ToShortDateString();
      }

      doc.DOCEXP = document.DateExp.HasValue ? document.DateExp.Value.ToShortDateString() : null;
      doc.DOCCAT = ((int)category).ToString();

      return doc;
    }

    /// <summary>
    /// The get dost.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="List"/> . 
    /// </returns>
    protected virtual List<string> GetDost(Statement statement)
    {
      var dost = new List<string>();
      var insuredPersonData = statement.InsuredPersonData;
      if (string.IsNullOrEmpty(insuredPersonData.MiddleName))
      {
        dost.Add("1");
      }

      if (string.IsNullOrEmpty(insuredPersonData.LastName))
      {
        dost.Add("2");
      }

      if (string.IsNullOrEmpty(insuredPersonData.FirstName))
      {
        dost.Add("3");
      }

      if (insuredPersonData.BirthdayType.HasValue)
      {
        switch ((BirthdayType)insuredPersonData.BirthdayType.Value)
        {
          case BirthdayType.MonthAndYear:
            dost.Add("4");
            break;
          case BirthdayType.Year:
            dost.Add("5");
            break;
        }
      }

      if (insuredPersonData.IsIncorrectDate.HasValue && insuredPersonData.IsIncorrectDate.Value)
      {
        dost.Add("6");
      }

      return dost;
    }

    /// <summary>
    /// The get insurance.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="InsuranceType"/>.
    /// </returns>
    protected virtual InsuranceType GetInsurance(Statement statement)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var insurance = new InsuranceType();
      insurance.TER_ST = Batch.Sender.Okato;
      insurance.ENP = statement.NumberPolicy;
      insurance.OGRNSMO = Batch.Receiver.Ogrn;

      // Зарегистрирован в РС ЕРП
      insurance.ERP = 1;

      // recType.Insurance.ORDERZ = TODO : Расширить функционал

      // Информация о документе, подтверждающем факт страхования по ОМС
      if (statement.MedicalInsurances != null)
      {
        insurance.POLIS = new List<PolisType>(2);
        foreach (var medicalInsurance in statement.MedicalInsurances)
        {
          var polis = new PolisType();
          polis.VPOLIS = conceptManager.Unproxy(medicalInsurance.PolisType).Code;

          polis.SPOLIS = medicalInsurance.PolisSeria;
          polis.NPOLIS = medicalInsurance.PolisNumber;
          polis.DBEG = medicalInsurance.DateFrom.ToShortDateString();
          polis.DEND = medicalInsurance.DateTo.ToShortDateString();
          polis.DSTOP = medicalInsurance.DateStop.HasValue ? medicalInsurance.DateTo.ToShortDateString() : string.Empty;

          insurance.POLIS.Add(polis);
        }
      }

      return insurance;
    }

    /// <summary>
    /// The get person.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="PersonType"/> . 
    /// </returns>
    protected virtual PersonType GetPerson(Statement statement)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var deadManager = ObjectFactory.GetInstance<IDeadInfoManager>();
      var insuredPersonData = statement.InsuredPersonData;
      var person = new PersonType();

      // ФИО
      person.FAM = insuredPersonData.LastName;
      person.IM = insuredPersonData.FirstName;
      person.OT = insuredPersonData.MiddleName;

      // Пол
      person.W = conceptManager.Unproxy(insuredPersonData.Gender).Code;

      // ДР
      if (insuredPersonData.Birthday.HasValue)
      {
        person.DR = insuredPersonData.Birthday.Value.ToShortDateString();
      }

      // Место рождения
      person.MR = insuredPersonData.Birthplace;

      // Гражданство, без гражданства, беженец
      person.C_OKSM = !insuredPersonData.IsNotCitizenship ? conceptManager.Unproxy(insuredPersonData.Citizenship).Code : "Б/Г";

      // Категория
      person.KATEG = insuredPersonData.Category != null ? conceptManager.Unproxy(insuredPersonData.Category).Code : null;

      person.SS = !string.IsNullOrEmpty(insuredPersonData.Snils)
                       ? SnilsChecker.SsToLong(statement.InsuredPersonData.Snils)
                       : null;

      // Контактная информация
      if (statement.ContactInfo != null)
      {
        person.PHONE = statement.ContactInfo.WorkPhone;
        person.PHONE_WORK = statement.ContactInfo.WorkPhone;
        person.EMAIL = statement.ContactInfo.Email;
      }

      // Информация о представителе
      if (statement.Representative != null)
      {
        var repr = statement.Representative;
        person.FIOPR = string.Format("{0} {1} {2}", repr.LastName, repr.FirstName, repr.MiddleName);
        person.CONTACT = string.Format("{0};{1}", repr.HomePhone, repr.WorkPhone);
      }

      // Дата смерти
      if (statement.InsuredPerson.DeadInfo != null)
      {
        person.DDEATH = deadManager.GetById(statement.InsuredPerson.DeadInfo.Id).DateDead.ToShortDateString();
      }

      // Код надёжности идентификации
      person.DOST = GetDost(statement);

      // Представитель
      person.PR = GetAssignee(statement.Representative);

      return person;
    }

    /// <summary>
    /// The get assignee.
    /// </summary>
    /// <param name="representative">
    /// The representative.
    /// </param>
    /// <returns>
    /// The <see cref="AssigneeType"/>.
    /// </returns>
    protected virtual AssigneeType GetAssignee(Representative representative)
    {
      if (representative == null)
      {
        return null;
      }

      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();
      var pr = new AssigneeType();
      pr.FAM = representative.LastName;
      pr.IM = representative.FirstName;
      pr.OT = representative.MiddleName;
      pr.PHONE = representative.HomePhone;
      pr.PHONE_WORK = representative.WorkPhone;
      if (representative.RelationType != null)
      {
        pr.RELATION = conceptManager.Unproxy(representative.RelationType).Code;
      }

      if (representative.Document != null)
      {
        pr.DOC = GetDocument(documentManager.GetById(representative.Document.Id), 
          DocumentCategory.Udl);
      }

      return pr;
    }

    /// <summary>
    /// The get vizit.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="VizitType"/> . 
    /// </returns>
    protected virtual VizitType GetVizit(Statement statement)
    {
      var vizit = new VizitType();

      vizit.DVIZIT = statement.DateFiling.HasValue ? statement.DateFiling.Value.ToShortDateString() : string.Empty;
      vizit.PETITION = statement.HasPetition.HasValue ? Convert.ToInt32(statement.HasPetition.Value) : 0;

      if (statement.ModeFiling != null)
      {
        vizit.METHOD = statement.ModeFiling.Code;
      }

      if (statement.FormManufacturing != null)
      {
        if (statement.AbsentPrevPolicy.HasValue && statement.AbsentPrevPolicy.Value)
        {
          vizit.FPOLIS = statement.FormManufacturing.Code;
        }
      }

      if (statement.CauseFiling != null)
      {
        if (CauseReinsurance.IsReinsurance(statement.CauseFiling.Id))
        {
          vizit.RSMO = statement.CauseFiling.Code;
        }
        else
        {
          vizit.RPOLIS = statement.CauseFiling.Code;
        }
      }

      return vizit;
    }

    protected virtual List<PersonBType> GetPersonBList(Statement statement)
    {
      var contentManager = ObjectFactory.GetInstance<IContentManager>();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      var contents = contentManager.GetBy(x => x.InsuredPersonData.Id == statement.InsuredPersonData.Id);
      var personBList = new List<PersonBType>();

      foreach (var content in contents)
      {
        var b = new PersonBType();
        b.PHOTO = contentManager.ByteToBase64(content.DocumentContent);
        b.TYPE = content.ContentType.Code;
        personBList.Add(b);
      }

      return personBList.Count > 0 ? personBList : null;
    }

    /// <summary>
    /// The map rec list type.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="RECType"/> . 
    /// </returns>
    protected virtual RECType MapRecListType(StatementBatch statement)
    {
      // Загрузка старых данных если требуется
      var statementChangeDates = LoadOldStatementData(statement);

      var recType = new RECType();

      recType.Id = statement.Id;
      recType.IsActive = (statement.Version == statement.VersionExport && statement.IsActive) ? "1" : "0";
      recType.Version = statement.VersionExport.ToString(CultureInfo.InvariantCulture);
      recType.NeedNewPolicy = statement.AbsentPrevPolicy.HasValue ? statement.AbsentPrevPolicy.Value : false;

      // Данные об обращении в СМО
      recType.Vizit = GetVizit(statement);

      // Данные о застрахованном лице
      recType.Person = GetPerson(statement);

      // Документы
      recType.Doc = new List<DocType>();
      recType.Doc.Add(GetDocument(statement.DocumentUdl, DocumentCategory.Udl));
      if (statement.DocumentRegistration != null && statement.DocumentUdl.Id != statement.DocumentRegistration.Id)
      {
        recType.Doc.Add(GetDocument(statement.DocumentRegistration, DocumentCategory.Registration));
      }

      if (statement.ResidencyDocument != null)
      {
        recType.Doc.Add(GetDocument(statement.ResidencyDocument, DocumentCategory.Residency));
      }

      // Адрес регистрации
      recType.AddresG = GetAddress(statement.Address, statement.Kladr);

      // Адрес проживания
      if (statement.Address2 != null && statement.Address.Id != statement.Address2.Id)
      {
        recType.AddresP = GetAddress(statement.Address2, statement.Kladr2);
      }

      // Событие страхования
      recType.Insurance = GetInsurance(statement);

      // Медиа
      recType.PersonB = GetPersonBList(statement);

      // Изменения по версиям
      recType.StatementChange = statementChangeDates != null && statementChangeDates.Count > 0
                                  ? statementChangeDates.Select(
                                    x =>
                                    new StatementChange
                                      {
                                        VERSION = x.Version.ToString(CultureInfo.InvariantCulture),
                                        FIELD = x.Field.Code,
                                        DATA = x.Datum
                                      }).ToList()
                                  : null;

      return recType;
    }

    /// <summary>
    /// The map op list type.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <returns>
    /// The <see cref="RECType"/> . 
    /// </returns>
    protected virtual OPType MapOpListType(StatementBatch statement, int recordNumber)
    {
      // Загрузка старых данных если требуется
      var statementChangeDates = LoadOldStatementData(statement);
      
      var opType = new OPType();

      opType.N_REC = recordNumber.ToString();
      opType.ID = statement.Id;
      opType.ISACTIVE = (statement.Version == statement.VersionExport && statement.IsActive) ? "1" : "0";
      opType.VERSION = statement.VersionExport.ToString(CultureInfo.InvariantCulture);
      opType.NEED_NEW_POLICY = statement.AbsentPrevPolicy.HasValue ? statement.AbsentPrevPolicy.Value : false;

      // Данные об обращении в СМО
      opType.VIZIT = GetVizit(statement);

      // Данные о застрахованном лице
      opType.PERSON = GetPerson(statement);

      // Документы
      opType.DOC_LIST = new List<DocType>();
      opType.DOC_LIST.Add(GetDocument(statement.DocumentUdl, DocumentCategory.Udl));
      if (statement.DocumentRegistration != null && statement.DocumentUdl.Id != statement.DocumentRegistration.Id)
      {
        opType.DOC_LIST.Add(GetDocument(statement.DocumentRegistration, DocumentCategory.Registration));
      }

      if (statement.ResidencyDocument != null)
      {
        opType.DOC_LIST.Add(GetDocument(statement.ResidencyDocument, DocumentCategory.Residency));
      }

      // Адрес регистрации
      opType.ADDRES_G = GetAddress(statement.Address, statement.Kladr);

      // Адрес проживания
      if (statement.Address2 != null && statement.Address.Id != statement.Address2.Id)
      {
        opType.ADDRES_P = GetAddress(statement.Address2, statement.Kladr2);
      }

      // Событие страхования
      opType.INSURANCE = GetInsurance(statement);

      // Медиа
      opType.PERSONB = GetPersonBList(statement);

      // Изменения по версиям
      opType.STATEMENT_CHANGE = statementChangeDates != null && statementChangeDates.Count > 0
                                  ? statementChangeDates.Select(
                                    x =>
                                    new StatementChange
                                    {
                                      VERSION = x.Version.ToString(CultureInfo.InvariantCulture),
                                      FIELD = x.Field.Code,
                                      DATA = x.Datum
                                    }).ToList()
                                  : null;

      return opType;
    }

    #endregion

    /// <summary>
    /// Загружает старые данные в завяление, если версия в батче и текущая версия не совпадают
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    protected IList<StatementChangeDate> LoadOldStatementData(StatementBatch statement)
    {
      if (statement.Version == statement.VersionExport)
      {
        return null;
      }

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var oldDataList = session.QueryOver<StatementChangeDate>()
        .Where(x => x.Statement.Id == statement.Id && x.Version == statement.VersionExport)
        .List();

      foreach (var oldData in oldDataList)
      {
        switch (oldData.Field.Id)
        {
          case TypeFields.Enp:
            statement.NumberPolicy = oldData.Datum;
            break;
          case TypeFields.FirstName:
            statement.InsuredPersonData.FirstName = oldData.Datum;
            break;
          case TypeFields.LastName:
            statement.InsuredPersonData.LastName = oldData.Datum;
            break;
          case TypeFields.MiddleName:
            statement.InsuredPersonData.MiddleName = oldData.Datum;
            break;
          case TypeFields.Birthday:
            DateTime birthday = DateTime.Now;
            if (DateTime.TryParse(oldData.Datum, out birthday))
              statement.InsuredPersonData.Birthday = birthday;
            break;
          case TypeFields.Birthplace:
            statement.InsuredPersonData.Birthplace = oldData.Datum;
            break;
          case TypeFields.GenderId:
            int genderId = -1;
            int.TryParse(oldData.Datum, out genderId);
            statement.InsuredPersonData.Gender = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(genderId);
            break;
          case TypeFields.Snils:
            statement.InsuredPersonData.Snils = oldData.Datum;
            break;
          case TypeFields.DocumentTypeId:
            int docTypeId = -1;
            int.TryParse(oldData.Datum, out docTypeId);
            statement.DocumentUdl.DocumentType = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(docTypeId);
            break;
          case TypeFields.DocumentSeries:
            statement.DocumentUdl.Series = oldData.Datum;
            break;
          case TypeFields.DocumentNumber:
            statement.DocumentUdl.Number = oldData.Datum;
            break;
        }
      }

      return oldDataList;
    }
  }
}