// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExporterToSrz.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The exporter to srz.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.exchange.impl
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;

  using NHibernate;

  using NLog;

  using Quartz;

  using rt.atl.model.atl;
  using rt.core.business.nhibernate;
  using rt.core.model.interfaces;
  using rt.srz.business.extensions;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The exporter to srz.
  /// </summary>
  public class ExporterToSrz : ExchangeBase
  {
    #region Fields

    /// <summary>
    ///   The logger.
    /// </summary>
    private readonly Logger logger;

    /// <summary>
    ///   The session factory pvp.
    /// </summary>
    private readonly ISessionFactory sessionFactoryPvp;

    /// <summary>
    ///   The session factory srz.
    /// </summary>
    private readonly ISessionFactory sessionFactorySrz;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExporterToSrz"/> class.
    /// </summary>
    /// <param name="sessionFactoryPvp">
    /// The session factory pvp.
    /// </param>
    /// <param name="managerSessionFactorys">
    /// The manager Session Factorys.
    /// </param>
    public ExporterToSrz(ISessionFactory sessionFactoryPvp, IManagerSessionFactorys managerSessionFactorys)
      : base(ExchangeTypeEnum.ExportToSrz)
    {
      this.sessionFactoryPvp = sessionFactoryPvp;
      sessionFactorySrz = managerSessionFactorys.GetFactoryByName("NHibernateCfgAtl.xml");
      logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The run.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public override void Run(IJobExecutionContext context)
    {
      var session = sessionFactoryPvp.GetCurrentSession();
      {
        var transactionPvp = session.BeginTransaction();
        try
        {
          using (var sessionAtl = sessionFactorySrz.OpenSession())
          {
            var transactionAtl = sessionAtl.BeginTransaction();
            try
            {
              var log = new Przlog { Filename = Guid.NewGuid().ToString(), Dtin = DateTime.Now, Reccount = 0 };

              sessionAtl.Save(log);

              // Синхронизируем операции с выдачей временного свидетельства
              StatementMaximum statement = null;
              var queryOver =
                session.QueryOver(() => statement)
                       .Where(
                              x =>
                              x.Status.Id == StatusStatement.New || x.Status.Id == StatusStatement.Performed
                              || x.Status.Id == StatusStatement.Declined)
                       .Where(x => !x.IsExportTemp)
                       .And(x => x.AbsentPrevPolicy.Value)
                       .And(x => x.PolicyIsIssued.Value == false);

              ExportByQuery(queryOver, sessionAtl, log, context);

              queryOver =
                session.QueryOver(() => statement)
                       .Where(
                              x =>
                              x.Status.Id == StatusStatement.New || x.Status.Id == StatusStatement.Performed
                              || x.Status.Id == StatusStatement.Declined)
                       .Where(x => !x.IsExportTemp || !x.IsExportPolis)
                       .And(x => x.AbsentPrevPolicy.Value)
                       .And(x => x.PolicyIsIssued.Value);
              ExportByQuery(queryOver, sessionAtl, log, context);

              // Синхронизируем операции без выдачи временного свидетельства
              queryOver =
                session.QueryOver(() => statement)
                       .Where(x => x.Status.Id == StatusStatement.New || x.Status.Id == StatusStatement.Declined)
                       .Where(x => !x.IsExportPolis)
                       .AndNot(x => x.AbsentPrevPolicy.Value);

              ExportByQuery(queryOver, sessionAtl, log, context);

              transactionAtl.Commit();
            }
            catch (Exception ex)
            {
              transactionAtl.Dispose();
              logger.Error(ex.Message, ex);
              throw;
            }

            sessionAtl.Close();
          }

          session.Flush();
          transactionPvp.Commit();
        }
        catch (Exception ex)
        {
          transactionPvp.Dispose();
          logger.Error(ex.Message, ex);
          throw;
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The set errors statement.
    /// </summary>
    /// <param name="session">
    /// The session.
    /// </param>
    /// <param name="st">
    /// The st.
    /// </param>
    /// <param name="przBuff">
    /// The prz buff.
    /// </param>
    /// <param name="errors">
    /// The errors.
    /// </param>
    private static void SetErrorsStatement(
      ISession session,
      StatementMaximum st,
      Przbuf przBuff,
      IEnumerable<Testproc> errors)
    {
      var flag = true;
      var errorStringList = !string.IsNullOrEmpty(przBuff.Sflk)
                              ? przBuff.Sflk.Split(',').Select(x => x.PadLeft(3, '0'))
                              : new[] { "Not" };
      foreach (var error in
        errors.Where(x => errorStringList.Any(y => y == x.Code))
              .Select(
                      procedure =>
                      new Error
                      {
                        Code = procedure.Code,
                        Message1 =
                          RecodingErros.Recoding(
                                                 int.Parse(procedure.Code),
                                                 procedure.Caption.ToLower().ToUpperFirstChar()),
                        Statement = st,
                        Repl = przBuff.Repl
                      }))
      {
        if (flag)
        {
          error.Repl = przBuff.Repl;
        }

        flag = false;
        session.Save(error);
      }

      if (flag)
      {
        session.Save(
                     new Error
                     {
                       Code = "-1",
                       Message1 = przBuff.Repl ?? "СРЗ не вернула ни одной ошибки, но даные не приняла.",
                       Statement = st,
                       Repl = przBuff.Repl
                     });
      }

      st.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusStatement.Declined);
      session.Save(st);
    }

    /// <summary>
    /// The export by query.
    /// </summary>
    /// <param name="session">
    /// The session.
    /// </param>
    /// <param name="sessionAtl">
    /// The session atl.
    /// </param>
    /// <param name="log">
    /// The log.
    /// </param>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    private void ExportByList(
      ISession session,
      ISession sessionAtl,
      Przlog log,
      IList<StatementMaximum> query,
      IJobExecutionContext context)
    {
      if (query.Count == 0)
      {
        return;
      }

      context.JobDetail.JobDataMap["progress"] = 0;
      var i = 0;
      var count = query.Count;

      // Экспортируем все заявления
      foreach (var st in query)
      {
        ExportStatement(st, log, sessionAtl, session);
        context.JobDetail.JobDataMap["progress"] = (int)(++i / (double)count * 40);
      }

      log.Dtout = DateTime.Now;
      sessionAtl.Save(log);
      sessionAtl.Flush();

      // Запуск процедуры загрузки в СРЗ
      StartLoadBuff(sessionAtl, log.Id);
      context.JobDetail.JobDataMap["progress"] = 60;

      // Проходим еще раз с целью выявления не загрузившихся
      var errorsManager = ObjectFactory.GetInstance<IErrorManager>();
      var errors = sessionAtl.QueryOver<Testproc>().List();
      i = 0;
      foreach (var st in query)
      {
        var isError = false;
        errorsManager.Delete(x => x.Statement.Id == st.Id);

        Przbuf przBuff = null;
        Przbuf przBuffPolis = null;

        // Проверяем операцию с выдачей ВС
        if (st.PrzBuff != null)
        {
          przBuff = st.PrzBuff as Przbuf;
          sessionAtl.Refresh(przBuff);
          if (przBuff != null)
          {
            if (przBuff.St == 2 && !string.IsNullOrEmpty(przBuff.Sflk))
            {
              isError = true;
              SetErrorsStatement(session, st, przBuff, errors);
              st.IsExportTemp = false;
              sessionAtl.Delete(przBuff);
              continue;
            }

            // Создаем ссвязку в базе данных СРЗ
            sessionAtl.Save(new ExchangePvp { PrzBuff = przBuff, StatementId = st.Id.ToString("D"), });
          }
        }

        // Проверяем операцию с выдачей полиса
        if (st.PrzBuffPolis != null)
        {
          przBuffPolis = st.PrzBuffPolis as Przbuf;
          sessionAtl.Refresh(przBuffPolis);
          if (przBuffPolis != null)
          {
            if (przBuffPolis.St == 2 && !string.IsNullOrEmpty(przBuffPolis.Sflk))
            {
              isError = true;
              SetErrorsStatement(session, st, przBuffPolis, errors);
              st.IsExportPolis = false;
              sessionAtl.Delete(przBuffPolis);
            }
            else
            {
              sessionAtl.Save(new ExchangePvp { PrzBuff = przBuffPolis, StatementId = st.Id.ToString("D"), });
            }
          }
        }

        // Заполняем ссылки на People and Polis
        var buf = przBuff ?? przBuffPolis;
        if (buf != null)
        {
          st.PidId = buf.Pid;
          st.PolisId = buf.Polisid;
        }

        // Сохраняем статус заявления
        if (isError)
        {
          st.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusStatement.Declined);
        }

        session.Save(st);
        context.JobDetail.JobDataMap["progress"] = 60 + (int)(++i / (double)count * 40);
      }
    }

    /// <summary>
    /// The export by query.
    /// </summary>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <param name="sessionAtl">
    /// The session atl.
    /// </param>
    /// <param name="log">
    /// The log.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    private void ExportByQuery(
      IQueryOver<StatementMaximum, StatementMaximum> query,
      ISession sessionAtl,
      Przlog log,
      IJobExecutionContext context)
    {
      var session = sessionFactoryPvp.GetCurrentSession();
      var count = query.RowCount();
      var step = (count / 5000) + 1;

      for (var i = 0; i < step; i++)
      {
        var list =
          query.OrderBy(x => x.DateFiling)
               .Asc.ThenBy(x => x.Id)
               .Asc.Skip(i * 5000)
               .Take(5000)
               .RootCriteria.SetTimeout(int.MaxValue)
               .List<StatementMaximum>();

        ExportByList(session, sessionAtl, log, list, context);
        sessionAtl.Flush();
      }
    }

    /// <summary>
    /// The export statement.
    /// </summary>
    /// <param name="st">
    /// The st.
    /// </param>
    /// <param name="log">
    /// The log.
    /// </param>
    /// <param name="sessionAtl">
    /// The session atl.
    /// </param>
    /// <param name="session">
    /// The session.
    /// </param>
    private void ExportStatement(StatementMaximum st, Przlog log, ISession sessionAtl, ISession session)
    {
      // Поиск временного свидетельства
      var tempCertificate =
        st.MedicalInsurances.Where(x => x.IsActive).FirstOrDefault(x => x.PolisType.Id == PolisType.В);

      // Экспорт временного свидетельства
      if (!st.IsExportTemp && tempCertificate != null)
      {
        var przbuffTemp = WritePrzBuff(st, log, sessionAtl, tempCertificate, GetTypeOperationTemp(st));
        st.PrzBuff = przbuffTemp;
        st.PrzBuffId = przbuffTemp.Id;
        st.IsExportTemp = true;
        st.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusStatement.Performed);
      }

      if (st.IsExportPolis)
      {
        return;
      }

      // Поиск полиса
      var polisCertificate =
        st.MedicalInsurances.Where(x => x.IsActive).SingleOrDefault(x => x.PolisType.Id != PolisType.В);

      Przbuf przbuffPolis = null;

      // Если временное свидетельство заполненно и полис найден, то синхронизируем операцию
      // Выдача на руки полиса единого образца после временного свидетельства
      if (tempCertificate != null && polisCertificate != null)
      {
        // Экспорт полиса
        przbuffPolis = WritePrzBuff(
                                    st,
                                    log,
                                    sessionAtl,
                                    polisCertificate,
                                    ObjectFactory.GetInstance<IConceptCacheManager>().GetById(TypeOperation.П060).Code);
      }
      else
      {
        // Экспорт полиса
        if (polisCertificate != null)
        {
          przbuffPolis = WritePrzBuff(st, log, sessionAtl, polisCertificate, GetTypeOperationTemp(st));
        }
      }

      if (przbuffPolis != null)
      {
        st.PrzBuffPolis = przbuffPolis;
        st.PrzBuffPolisId = przbuffPolis.Id;
        st.IsExportPolis = true;
        st.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusStatement.Exercised);
      }
    }

    /// <summary>
    /// The get okato rn.
    /// </summary>
    /// <param name="address">
    /// The address.
    /// </param>
    /// <param name="sessionAtl">
    /// The session atl.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetOkatoRn(address address, ISession sessionAtl)
    {
      var kl = address.Regulatory();
      while (kl != null)
      {
        if (sessionAtl.QueryOver<Okato>().Where(x => x.Code == kl.Okato).RowCount() > 0)
        {
          return kl.Okato;
        }

        if (kl.ParentId != null)
        {
          kl = ObjectFactory.GetInstance<IAddressService>().GetAddress(kl.ParentId.Value);
        }
        else
        {
          kl = null;
        }
      }

      return address.Okato;
    }

    /// <summary>
    /// The get polis vid.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    private int GetPolisType(int id)
    {
      switch (id)
      {
        case PolisType.С:
          return 1;
        case PolisType.В:
          return 2;
        case PolisType.К:
          return 3;
        case PolisType.Э:
          return 3;
        case PolisType.П:
          return 3;
      }

      return 0;
    }

    /// <summary>
    /// The get polis vid.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    private int? GetPolisVid(int id)
    {
      switch (id)
      {
        case PolisType.К:
          return 3;
        case PolisType.Э:
          return 2;
        case PolisType.П:
          return 1;
      }

      return null;
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
    private string GetTypeOperationTemp(StatementMaximum st)
    {
      var conceptCacheManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      if (st.AbsentPrevPolicy.HasValue && st.AbsentPrevPolicy.Value)
      {
        switch (st.CauseFiling.Id)
        {
          case CauseReinsurance.Choice:
            return conceptCacheManager.GetById(TypeOperation.П010).Code;
          case CauseReinsurance.ReinsuranceAtWill:
            return conceptCacheManager.GetById(TypeOperation.П034).Code;
          case CauseReinsurance.ReinsuranceWithTheMove:
            return conceptCacheManager.GetById(TypeOperation.П035).Code;
          case CauseReinsurance.ReinsuranceStopFinance:
            return conceptCacheManager.GetById(TypeOperation.П036).Code;
          case CauseReneval.RenevalChangePersonDetails:
          case CauseReneval.RenevalInaccuracy:
          case CauseReneval.RenevalUnusable:
            return conceptCacheManager.GetById(TypeOperation.П061).Code;
          case CauseReneval.RenevalLoss:
            return conceptCacheManager.GetById(TypeOperation.П062).Code;
          case CauseReneval.RenevalExpiration:
            return conceptCacheManager.GetById(TypeOperation.П063).Code;
        }
      }

      if (st.CauseFiling != null)
      {
        switch (st.CauseFiling.Id)
        {
          case CauseReinsurance.Choice:
            return conceptCacheManager.GetById(TypeOperation.П031).Code;
          case CauseReinsurance.ReinsuranceAtWill:
            return conceptCacheManager.GetById(TypeOperation.П031).Code;
          case CauseReinsurance.ReinsuranceWithTheMove:
            return conceptCacheManager.GetById(TypeOperation.П032).Code;
          case CauseReinsurance.ReinsuranceStopFinance:
            return conceptCacheManager.GetById(TypeOperation.П033).Code;
          case CauseReneval.Edit:
            return conceptCacheManager.GetById(TypeOperation.П040).Code;
        }
      }

      throw new Exception("Неизвестный или не обрабатываемый тип операции СРЗ");
    }

    /// <summary>
    /// The start load buff.
    /// </summary>
    /// <param name="session">
    /// The session.
    /// </param>
    /// <param name="lid">
    /// The lid.
    /// </param>
    private void StartLoadBuff(ISession session, int lid)
    {
      var query = session.GetNamedQuery("handlebuff");
      query.SetTimeout(int.MaxValue);
      query.SetParameter("LID", lid);
      query.SetParameter("ID1", 0);
      query.SetParameter("ID2", int.MaxValue);
      query.UniqueResult();
    }

    /// <summary>
    /// The write prz buff.
    /// </summary>
    /// <param name="st">
    /// The st.
    /// </param>
    /// <param name="log">
    /// The log.
    /// </param>
    /// <param name="sessionAtl">
    /// The session atl.
    /// </param>
    /// <param name="medicalInsurance">
    /// The medical insurance.
    /// </param>
    /// <param name="op">
    /// The op.
    /// </param>
    /// <returns>
    /// The <see cref="Przbuf"/>.
    /// </returns>
    private Przbuf WritePrzBuff(
      StatementMaximum st,
      Przlog log,
      ISession sessionAtl,
      MedicalInsurance medicalInsurance,
      string op)
    {
      var contentManager = ObjectFactory.GetInstance<IContentManager>();
      var personData = st.InsuredPersonData;
      var documentUdl = st.DocumentUdl;
      var address = st.Address;
      var address2 = st.Address2 ?? st.Address;
      var documentResidency = st.ResidencyDocument;

      ////if (documentResidency == null && (st.InsuredPersonData.Citizenship == null || st.InsuredPersonData.Citizenship.Id != Country.RUS))
      ////{
      ////  documentResidency = documentUdl;
      ////}
      var contactInfo = st.ContactInfo;
      var pvp = st.PointDistributionPolicy;
      var smo = pvp.Parent;

      var foto = contentManager.GetFoto(st.InsuredPersonData.Id);
      var signature = contentManager.GetSignature(st.InsuredPersonData.Id);
      var okatoRn = GetOkatoRn(address, sessionAtl) ?? string.Empty;
      var okatoPrn = GetOkatoRn(address2, sessionAtl) ?? string.Empty;
      var regulatoryAddress = address.Regulatory();
      var regulatoryAddress2 = address2.Regulatory();
      var przBuff = new Przbuf
                    {
                      Fam = personData.LastName,
                      Im = personData.FirstName,
                      Ot = personData.MiddleName,
                      W = int.Parse(personData.Gender.Code),
                      Dr = personData.Birthday,
                      Dra = personData.IsIncorrectDate,
                      Drt = personData.BirthdayType.HasValue ? personData.BirthdayType.Value : 1,
                      Mr = personData.Birthplace,
                      Ss =
                        !string.IsNullOrEmpty(personData.Snils)
                          ? SnilsChecker.SsToLong(personData.Snils)
                          : null,
                      Cn = personData.Citizenship != null ? personData.Citizenship.Code : "Б/Г",
                      Kateg = int.Parse(personData.Category.Code),
                      BirthOksm = personData.OldCountry != null ? personData.OldCountry.Code : null,

                      // Документ УДЛ
                      Doctp = documentUdl.DocumentType.Code,
                      Docs = documentUdl.Series,
                      Docn = documentUdl.Number,
                      Docdt = documentUdl.DateIssue,
                      Docend = documentUdl.DateExp,
                      Docorg = documentUdl.IssuingAuthority,

                      // Адрес регистрации
                      Subj =
                        !string.IsNullOrEmpty(address.Okato)
                          ? address.Okato.Substring(0, Math.Min(2, address.Okato.Length)) + "000"
                          : okatoRn.Substring(0, Math.Min(2, okatoRn.Length)) + "000",
                      Rn = okatoRn,
                      Indx = address.Postcode,
                      Rnname = address.Area,
                      City = address.City,
                      Np =
                        !string.IsNullOrEmpty(address.Town)
                          ? address.Town
                          : !string.IsNullOrEmpty(address.City) ? address.City : address.Subject,
                      Ul = address.Street,
                      Dom = address.House,
                      Kor = address.Housing,
                      Kv =
                        address.Room.HasValue
                          ? address.Room.Value.ToString(CultureInfo.InvariantCulture)
                          : string.Empty,
                      Dmj = address.DateRegistration,
                      Kladrg = regulatoryAddress != null ? regulatoryAddress.Code : null,

                      // Адрес проживания
                      Psubj =
                        !string.IsNullOrEmpty(address2.Okato)
                          ? address2.Okato.Substring(0, Math.Min(2, address2.Okato.Length)) + "000"
                          : okatoPrn.Substring(0, Math.Min(2, okatoPrn.Length)) + "000",
                      Prn = okatoPrn,
                      Pindx = address2.Postcode,
                      Prnname = address2.Area,
                      Pcity = address2.City,
                      Pnp =
                        !string.IsNullOrEmpty(address2.Town)
                          ? address2.Town
                          : !string.IsNullOrEmpty(address2.City) ? address2.City : address2.Subject,
                      Pul = address2.Street,
                      Pdom = address2.House,
                      Pkor = address2.Housing,
                      Pkv =
                        address2.Room.HasValue
                          ? address2.Room.Value.ToString(CultureInfo.InvariantCulture)
                          : string.Empty,
                      Pdmj = address2.DateRegistration,
                      Kladrp = regulatoryAddress2 != null ? regulatoryAddress2.Code : null,

                      // Документ регистрации 

                      // Контактная информация
                      Email = contactInfo.Email,
                      Phone = contactInfo.HomePhone,
                      Enp = medicalInsurance.Enp ?? st.NumberPolicy,
                      Q = smo.Code,
                      Qogrn = smo.Ogrn,
                      Prz = st.PointDistributionPolicy.Code,
                      Dviz = st.DateFiling,
                      Meth = int.Parse(st.ModeFiling.Code),
                      Sp = personData.Category.Code,
                      Petition = st.HasPetition,
                      Photo = foto ?? string.Empty,
                      Signat = signature ?? string.Empty,
                      PRZLOG = log,
                      Op = op,
                      Opdoc = GetPolisType(medicalInsurance.PolisType.Id),
                      Spol =
                        !string.IsNullOrEmpty(medicalInsurance.PolisSeria)
                          ? medicalInsurance.PolisSeria
                          : null,
                      Npol = medicalInsurance.PolisNumber,
                      Dbeg = medicalInsurance.DateFrom,
                      Dend = medicalInsurance.DateTo,
                      Dstop = medicalInsurance.DateStop
                    };

      var repr = st.Representative;
      if (repr != null)
      {
        przBuff.Fiopr = string.Format("{0} {1} {2}", repr.FirstName, repr.LastName, repr.MiddleName).Crop(60);
        przBuff.Contact = repr.HomePhone;
        przBuff.Zphone = repr.WorkPhone;
        przBuff.Zt = repr.RelationType.Id - 317;
        przBuff.Zfam = repr.LastName;
        przBuff.Zim = repr.FirstName;
        przBuff.Zot = repr.MiddleName;
        if (repr.Document != null && repr.Document.DocumentType != null)
        {
          var doc = repr.Document;
          przBuff.Zdoctp = doc.DocumentType.Code;
          przBuff.Zdocs = doc.Series;
          przBuff.Zdocn = doc.Number;
          przBuff.Zdocdt = doc.DateIssue;
          przBuff.Zdocorg = doc.IssuingAuthority;
          przBuff.Zdr = doc.DateExp;
        }
      }

      if (address.IsHomeless.HasValue && address.IsHomeless.Value)
      {
        przBuff.Bomj = address.IsHomeless;
        przBuff.Subj = smo.Parent.Okato.Substring(0, Math.Min(2, smo.Parent.Okato.Length)) + "000";
        przBuff.Psubj = smo.Parent.Okato.Substring(0, Math.Min(2, smo.Parent.Okato.Length)) + "000";
      }

      if (st.PreviousStatement != null)
      {
        if (st.PreviousStatement.InsuredPersonData != null)
        {
          var oldpersonData = st.PreviousStatement.InsuredPersonData;
          przBuff.Oldfam = oldpersonData.LastName;
          przBuff.Oldim = oldpersonData.FirstName;
          przBuff.Oldot = oldpersonData.MiddleName;
          przBuff.Oldw = int.Parse(oldpersonData.Gender.Code);
          przBuff.Olddr = oldpersonData.Birthday;

          przBuff.Oldmr = oldpersonData.Birthplace;
          przBuff.Oldss = !string.IsNullOrEmpty(oldpersonData.Snils) ? SnilsChecker.SsToLong(oldpersonData.Snils) : null;
        }

        // Документ УДЛ
        if (st.PreviousStatement.DocumentUdl != null)
        {
          var olddoc = st.PreviousStatement.DocumentUdl;
          przBuff.Olddoctp = olddoc.DocumentType.Code;
          przBuff.Olddocs = olddoc.Series;
          przBuff.Olddocn = olddoc.Number;
          przBuff.Olddocdt = olddoc.DateIssue;
          przBuff.Olddocend = olddoc.DateExp;
          przBuff.Olddocorg = olddoc.IssuingAuthority;
        }

        // Документ УДЛ
        if (st.PreviousStatement.ResidencyDocument != null
            && st.PreviousStatement.ResidencyDocument.DocumentType != null)
        {
          var olddoc = st.PreviousStatement.ResidencyDocument;
          przBuff.Oldrdoctp = olddoc.DocumentType.Code;
          przBuff.Oldrdocs = olddoc.Series;
          przBuff.Oldrdocn = olddoc.Number;
          przBuff.Oldrdocdt = olddoc.DateIssue;
          przBuff.Oldrdocend = olddoc.DateExp;
          przBuff.Oldrdocorg = olddoc.IssuingAuthority;
        }
      }

      if (przBuff.Dend == new DateTime(2200, 1, 1))
      {
        przBuff.Dend = null;
      }

      if (przBuff.Dstop == new DateTime(2200, 1, 1))
      {
        przBuff.Dstop = null;
      }

      if (st.FormManufacturing != null)
      {
        przBuff.Polvid = GetPolisVid(st.FormManufacturing.Id);
      }

      if (documentResidency != null && documentResidency.DocumentType != null)
      {
        // Документ на право проживания в РФ 
        przBuff.Rdoctp = documentResidency.DocumentType.Code;
        przBuff.Rdocs = documentResidency.Series;
        przBuff.Rdocn = documentResidency.Number;
        przBuff.Rdocdt = documentResidency.DateIssue;
        przBuff.Rdocorg = documentResidency.IssuingAuthority;
        przBuff.Rdocend = documentResidency.DateExp;
      }

      // Помечаем заявление созданные через нашу систему
      // Будет использовать впоследствии при импорте из СРЗ
      przBuff.Num = "-1";

      // Помечаем адреса структурированный/не структурированный
      var args = address.IsNotStructureAddress.HasValue
                   ? Convert.ToInt16(address.IsNotStructureAddress.Value)
                            .ToString(CultureInfo.InvariantCulture)
                   : "0";
      var s = address2.IsNotStructureAddress.HasValue
                ? Convert.ToInt16(address2.IsNotStructureAddress.Value)
                         .ToString(CultureInfo.InvariantCulture)
                : "0";
      przBuff.Zaddr = string.Format("{0}{1}", args, s);

      sessionAtl.Save(przBuff);

      log.Reccount++;
      logger.Trace(przBuff);
      return przBuff;
    }

    #endregion
  }
}